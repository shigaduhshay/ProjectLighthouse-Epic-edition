using LBPUnion.ProjectLighthouse.Serialization;
using LBPUnion.ProjectLighthouse.Types.Levels;

namespace LBPUnion.ProjectLighthouse.Types.Activity.Events;

public class PublishLevelEvent : IEvent
{
    public long Timestamp { get; set; }
    public User User { get; init; }
    public Slot Slot { get; init; }
    public string Serialize()
    {
        return LbpSerializer.TaggedStringElement("event", "", "type", "publish_level");
    }
}