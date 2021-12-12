#nullable enable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using LBPUnion.ProjectLighthouse.Types.Levels;

namespace LBPUnion.ProjectLighthouse.Types.Reviews
{
    [XmlRoot("review")]
    [XmlType("review")]
    public class Review
    {
        // ReSharper disable once UnusedMember.Global
        [Key]
        public int ReviewId { get; set; }

        [XmlIgnore]
        public int ReviewerId { get; set; }

        [ForeignKey(nameof(ReviewerId))]
        public User Reviewer { get; set; }

        [XmlElement("slot_id")]
        public int SlotId { get; set; }

        [ForeignKey(nameof(SlotId))]
        public Slot Slot { get; set; }

        [XmlElement("timestamp")]
        public long Timestamp { get; set; }

        [XmlElement("labels")]
        public string LabelCollection { get; set; }

        [NotMapped]
        [XmlIgnore]
        public string[] Labels {
            get => this.LabelCollection.Split(",");
            set => this.LabelCollection = string.Join(',', value);
        }

        [XmlElement("deleted")]
        public bool Deleted { get; set; }

        [XmlElement("deleted_by")]
        public DeletedBy DeletedBy { get; set; }

        [XmlElement("text")]
        public string Text { get; set; }

        [XmlElement("thumb")]
        public int Thumb { get; set; }

        [XmlElement("thumbsup")]
        public int ThumbsUp { get; set; }

        [XmlElement("thumbsdown")]
        public int ThumbsDown { get; set; }
    }
}