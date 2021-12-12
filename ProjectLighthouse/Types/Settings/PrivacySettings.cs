using System.Xml.Serialization;

namespace LBPUnion.ProjectLighthouse.Types.Settings
{
    [XmlRoot("privacySettings")]
    [XmlType("privacySettings")]
    public class PrivacySettings
    {
        [XmlElement("levelVisibility")]
        public string LevelVisibility { get; set; }

        [XmlElement("profileVisibility")]
        public string ProfileVisibility { get; set; }
    }
}