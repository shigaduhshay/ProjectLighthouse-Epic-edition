using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace LBPUnion.ProjectLighthouse.Types.Profiles
{
    [XmlRoot("comment")]
    [XmlType("comment")]
    public class Comment
    {
        [Key]
        [XmlElement("id")]
        public int CommentId { get; set; }

        [XmlIgnore]
        public int PosterUserId { get; set; }

        [XmlIgnore]
        public int TargetUserId { get; set; }

        [XmlIgnore]
        [ForeignKey(nameof(PosterUserId))]
        public User Poster { get; set; }

        [XmlIgnore]
        [ForeignKey(nameof(TargetUserId))]
        public User Target { get; set; }

        [XmlElement("timestamp")]
        public long Timestamp { get; set; }

        [XmlElement("message")]
        public string Message { get; set; }

        [XmlElement("thumbsup")]
        public int ThumbsUp { get; set; }

        [XmlElement("thumbsdown")]
        public int ThumbsDown { get; set; }

        [NotMapped]
        [XmlElement("npHandle")]
        [SuppressMessage("ReSharper", "ValueParameterNotUsed")]
        public string PosterUsername {
            get => this.Poster.Username;
            set {}
        }
    }
}