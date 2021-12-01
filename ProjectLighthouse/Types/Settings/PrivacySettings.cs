using System.Xml.Serialization;

namespace LBPUnion.ProjectLighthouse.Types.Settings
{
    public class PrivacySettings
    {
        [XmlElement("levelVisibility")]
        public string LevelVisibility { get; set; }

        [XmlElement("profileVisibility")]
        public string ProfileVisibility { get; set; }
    }
}