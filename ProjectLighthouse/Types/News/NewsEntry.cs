using System.Xml.Serialization;

namespace LBPUnion.ProjectLighthouse.Types.News
{
    /// <summary>
    ///     Used on the info moon on LBP1. Broken for unknown reasons
    /// </summary>
    [XmlType("item")]
    public class NewsEntry
    {
        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("summary")]
        public string Summary { get; set; }

        [XmlElement("text")]
        public string Text { get; set; }

        [XmlElement("image")]
        public NewsImage Image { get; set; }

        [XmlElement("category")]
        public string Category { get; set; }

        [XmlElement("date")]
        public long Timestamp { get; set; }
    }
}