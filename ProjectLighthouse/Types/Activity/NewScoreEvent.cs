using LBPUnion.ProjectLighthouse.Helpers;
using LBPUnion.ProjectLighthouse.Serialization;

namespace LBPUnion.ProjectLighthouse.Types.Activity;

public class NewScoreEvent : IEvent
{
    public long Timestamp { get; set; } = TimestampHelper.TimestampMillis;
    public User User { get; init; }
    public Score Score { get; init; }

    public string EventType => "score";
    public string Serialize()
    {
        string @event = LbpSerializer.StringElement("timestamp", Timestamp) +
                        LbpSerializer.StringElement("actor", User.Username) +
                        LbpSerializer.TaggedStringElement("object_slot_id", Score.SlotId, "type", "user") +
                        LbpSerializer.StringElement("score", Score.Points) +
                        LbpSerializer.StringElement("user_count", Score.Type);

        return LbpSerializer.TaggedStringElement("event", @event, "type", "score");
    }
}