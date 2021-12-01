using System.Xml.Serialization;

namespace LBPUnion.ProjectLighthouse.Types.News
{
    [XmlType("image")]
    public class NewsImage
    {
        [XmlElement("hash")]
        public string Hash { get; set; }

        [XmlElement("alignment")]
        public string Alignment { get; set; }
    }
}