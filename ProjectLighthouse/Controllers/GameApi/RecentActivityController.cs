#nullable enable
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBPUnion.ProjectLighthouse.Helpers;
using LBPUnion.ProjectLighthouse.Serialization;
using LBPUnion.ProjectLighthouse.Types;
using LBPUnion.ProjectLighthouse.Types.Activity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IOFile = System.IO.File;

namespace LBPUnion.ProjectLighthouse.Controllers.GameApi;

[ApiController]
[Route("LITTLEBIGPLANETPS3_XML/")]
[Produces("text/plain")]
public class RecentActivityController : ControllerBase
{
    private readonly Database database;

    public RecentActivityController(Database database)
    {
        this.database = database;
    }

    [HttpGet("stream")]
    public async Task<IActionResult> GetStream([FromQuery] long timestamp = 0)
    {
        User? user = await this.database.UserFromGameRequest(this.Request);
        if (user == null) return this.StatusCode(403, "");

//        return this.Ok(IOFile.ReadAllText("/home/jvyden/.config/JetBrains/Rider2021.3/scratches/recent-activity.xml"));
        if (timestamp == 0) timestamp = TimestampHelper.TimestampMillis;

        List<LevelGroup> levelGroups = new();

        List<Score> scores = await this.database.Scores.OrderByDescending(s => s.ScoreId).Take(20).ToListAsync();
        foreach (Score score in scores)
        {
            LevelGroup? levelGroup = levelGroups.FirstOrDefault(l => l.SlotId == score.SlotId);
            if (levelGroup == null)
            {
                levelGroup = new LevelGroup
                {
                    SlotId = score.SlotId,
                    UserGroups = new List<UserGroup>(),
                };

                levelGroups.Add(levelGroup);
            }

            UserGroup? userGroup = levelGroup.UserGroups.FirstOrDefault(u => u.User.Username == score.PlayerIds[0]);
            User scoreUser = this.database.Users.First(u => u.Username == score.PlayerIds[0]);
            if (userGroup == null)
            {
                userGroup = new UserGroup
                {
                    User = scoreUser,
                    Events = new List<IEvent>(),
                };

                levelGroup.UserGroups.Add(userGroup);
            }

            userGroup.Events.Add
            (
                new NewScoreEvent
                {
                    Score = score,
                    User = scoreUser,
                }
            );
        }

        string groups = levelGroups.Aggregate(string.Empty, (current, levelGroup) => current + levelGroup.Serialize());

        return this.Ok
        (
            LbpSerializer.StringElement
            (
                "stream",
                LbpSerializer.StringElement("start_timestamp", timestamp - 604800000) + // Start timestamp is current timestamp minus 1 week
                LbpSerializer.StringElement("end_timestamp", timestamp) +
                LbpSerializer.StringElement("groups", groups)
            )
        );
    }
}