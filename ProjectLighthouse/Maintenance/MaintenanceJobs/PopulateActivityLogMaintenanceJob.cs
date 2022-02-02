#nullable enable
using System.Threading.Tasks;
using LBPUnion.ProjectLighthouse.Helpers;
using LBPUnion.ProjectLighthouse.Types;
using LBPUnion.ProjectLighthouse.Types.Activity;
using Microsoft.EntityFrameworkCore;

namespace LBPUnion.ProjectLighthouse.Maintenance.MaintenanceJobs;

public class PopulateActivityLogMaintenanceJob : IMaintenanceJob
{
    private readonly Database database = new();

    public async Task Run()
    {
        // Populate with scores
        foreach (Score score in await this.database.Scores.ToListAsync())
        {
            User? scoreUser = await this.database.Users.FirstOrDefaultAsync(u => u.Username == score.MainPlayer);
            if (scoreUser == null) continue;

            this.database.ActivityLog.Add
            (
                new ActivityEntry
                {
                    User = scoreUser,
                    UserId = scoreUser.UserId,
                    RelatedId = score.ScoreId,
                    Type = EventType.NewScore,
                    Timestamp = TimestampHelper.TimestampMillis,
                }
            );
        }

        // Finally, save.
        await this.database.SaveChangesAsync();
    }
    public string Name() => "Populate Activity Log";
    public string Description() => "Adds entries to the activity log based on events that have happened in the past";
}