#nullable enable
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LBPUnion.ProjectLighthouse.Helpers;
using LBPUnion.ProjectLighthouse.Serialization;
using LBPUnion.ProjectLighthouse.Types;
using LBPUnion.ProjectLighthouse.Types.Activity;
using LBPUnion.ProjectLighthouse.Types.Activity.Events;
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

    private async Task<List<LevelGroup>> getActivityInvolvingLevels()
    {
        List<ActivityEntry> activityEntries = await this.database.ActivityLog.Include(e => e.User).OrderByDescending(l => l.Timestamp).Take(20).ToListAsync();

        List<LevelGroup> levelGroups = new();

        foreach (ActivityEntry entry in activityEntries.Where(entry => entry.Type == EventType.NewScore))
        {
            Score? score = await this.database.Scores.FirstOrDefaultAsync(s => s.ScoreId == entry.RelatedId);
            if (score == null) continue;

            LevelGroup levelGroup = levelGroups.GetOrCreateLevelGroup(score.SlotId);
            UserGroup userGroup = levelGroup.GetOrCreateUserGroup(entry.User);

            userGroup.Events.Add
            (
                new NewScoreEvent
                {
                    Timestamp = entry.Timestamp,
                    Score = score,
                    User = entry.User,
                }
            );
        }

        return levelGroups;
    }

    [HttpGet("stream")]
    public async Task<IActionResult> GetStream([FromQuery] long timestamp = 0)
    {
        User? user = await this.database.UserFromGameRequest(this.Request);
        if (user == null) return this.StatusCode(403, "");

//        return this.Ok(IOFile.ReadAllText("/home/jvyden/.config/JetBrains/Rider2021.3/scratches/recent-activity.xml"));
        if (timestamp == 0) timestamp = TimestampHelper.TimestampMillis;

        List<LevelGroup> levelGroups = await this.getActivityInvolvingLevels();

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