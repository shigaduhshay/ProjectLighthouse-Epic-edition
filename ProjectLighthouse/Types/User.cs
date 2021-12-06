using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Xml.Serialization;
using LBPUnion.ProjectLighthouse.Helpers;
using LBPUnion.ProjectLighthouse.Types.Profiles;
using LBPUnion.ProjectLighthouse.Types.Settings;

namespace LBPUnion.ProjectLighthouse.Types
{
    [XmlRoot("user")]
    [XmlType("user")]
    [SuppressMessage("ReSharper", "ValueParameterNotUsed")]
    public class User
    {
        [XmlIgnore]
        public int UserId { get; set; }

        [XmlElement("npHandle")]
        public string Username { get; set; }

        [XmlIgnore]
        public string Password { get; set; }

        [XmlElement("icon")]
        public string IconHash { get; set; }

        [XmlElement("game")]
        public int Game { get; set; }

        [NotMapped]
        public int Lists => 0;

        /// <summary>
        ///     A user-customizable biography shown on the profile card
        /// </summary>
        [XmlElement("biography")]
        public string Biography { get; set; }

        [NotMapped]
        [XmlElement("reviewCount")]
        public int Reviews => 0;

        [NotMapped]
        [XmlElement("commentCount")]
        public int Comments {
            get {
                using Database database = new();
                return database.Comments.Count(c => c.TargetUserId == this.UserId);
            }
            set {}
        }

        [NotMapped]
        [XmlElement("photosByMeCount")]
        public int PhotosByMe {
            get {
                using Database database = new();
                return database.Photos.Count(p => p.CreatorId == this.UserId);
            }
            set {}
        }

        [NotMapped]
        [XmlElement("photosWithMeCount")]
        public int PhotosWithMe {
            get {
                using Database database = new();
                return Enumerable.Sum(database.Photos, photo => photo.Subjects.Count(subject => subject.User.UserId == this.UserId));
            }
            set {}
        }

        [XmlIgnore]
        public int LocationId { get; set; }

        /// <summary>
        ///     The location of the profile card on the user's earth
        /// </summary>
        [ForeignKey("LocationId")]
        [XmlElement("location")]
        public Location Location { get; set; }

        [NotMapped]
        [XmlElement("favouriteSlotCount")]
        public int HeartedLevels {
            get {
                using Database database = new();
                return database.HeartedLevels.Count(p => p.UserId == this.UserId);
            }
            set {}
        }

        [NotMapped]
        [XmlElement("favouriteUserCount")]
        public int HeartedUsers {
            get {
                using Database database = new();
                return database.HeartedProfiles.Count(p => p.UserId == this.UserId);
            }
            set {}
        }

        [NotMapped]
        [XmlElement("lolcatftwCount")]
        public int QueuedLevels {
            get {
                using Database database = new();
                return database.QueuedLevels.Count(p => p.UserId == this.UserId);
            }
            set {}
        }

        [XmlElement("pins")]
        public string Pins { get; set; } = "";

        [XmlElement("planets")]
        public string PlanetHash { get; set; } = "";

        [NotMapped]
        [XmlElement("heartCount")]
        public int Hearts {
            get {
                using Database database = new();

                return database.HeartedProfiles.Count(s => s.HeartedUserId == this.UserId);
            }
            set {}
        }

        [XmlIgnore]
        public bool IsAdmin { get; set; } = false;

        [XmlIgnore]
        public bool PasswordResetRequired { get; set; }

        [XmlElement("yay2")]
        public string YayHash { get; set; } = "";

        [XmlElement("boo2")]
        public string BooHash { get; set; } = "";

        [XmlElement("meh2")]
        public string MehHash { get; set; } = "";

        [XmlElement("commentsEnabled")]
        public bool CommentsEnabled { get; set; } = true;

        #nullable enable
        [NotMapped]
        [XmlIgnore]
        public string Status {
            get {
                using Database database = new();
                LastContact? lastMatch = database.LastContacts.Where
                        (l => l.UserId == this.UserId)
                    .FirstOrDefault(l => TimestampHelper.Timestamp - l.Timestamp < 300);

                if (lastMatch == null) return "Offline";

                return "Currently online on " + lastMatch.GameVersion.ToPrettyString();
            }
        }
        #nullable disable

        #region Slots

        /// <summary>
        ///     The number of used slots on the earth
        /// </summary>
        [NotMapped]
        [XmlElement("usedSlots")]
        public int UsedSlots {
            get {
                using Database database = new();
                return database.Slots.Count(s => s.CreatorId == this.UserId);
            }
            set {}
        }

        public int GetUsedSlotsForGame(GameVersion version)
        {
            using Database database = new();
            return database.Slots.Count(s => s.CreatorId == this.UserId && s.GameVersion == version);
        }

        [NotMapped]
        [XmlElement("lbp1UsedSlots")]
        public int UsedSlotsLBP1 {
            get => GetUsedSlotsForGame(GameVersion.LittleBigPlanet1);
            set {}
        }

        [NotMapped]
        [XmlElement("lbp2UsedSlots")]
        public int UsedSlotsLBP2 {
            get => GetUsedSlotsForGame(GameVersion.LittleBigPlanet2);
            set {}
        }

        [NotMapped]
        [XmlElement("lbp3UsedSlots")]
        public int UsedSlotsLBP3 {
            get => GetUsedSlotsForGame(GameVersion.LittleBigPlanet3);
            set {}
        }

        [NotMapped]
        [XmlElement("entitledSlots")]
        public int EntitledSlots {
            get => ServerSettings.Instance.EntitledSlots;
            set {}
        }

        [NotMapped]
        [XmlElement("lbp2EntitledSlots")]
        public int EntitledSlotsLBP2 {
            get => EntitledSlots;
            set {}
        }

        [NotMapped]
        [XmlElement("lbp3EntitledSlots")]
        public int EntitledSlotsLBP3 {
            get => EntitledSlots;
            set {}
        }

        /// <summary>
        ///     The number of slots remaining on the earth
        /// </summary>
        [NotMapped]
        public int FreeSlots {
            get => ServerSettings.Instance.EntitledSlots - this.UsedSlots;
            set {}
        }

        [NotMapped]
        [XmlElement("lbp1FreeSlots")]
        public int FreeSlotsLBP1 {
            get => ServerSettings.Instance.EntitledSlots - this.UsedSlotsLBP1;
            set {}
        }

        [NotMapped]
        [XmlElement("lbp2FreeSlots")]
        public int FreeSlotsLBP2 {
            get => ServerSettings.Instance.EntitledSlots - this.UsedSlotsLBP2;
            set {}
        }

        [NotMapped]
        [XmlElement("lbp3FreeSlots")]
        public int FreeSlotsLBP3 {
            get => ServerSettings.Instance.EntitledSlots - this.UsedSlotsLBP3;
            set {}
        }

        #endregion Slots

        #nullable enable
        public override bool Equals(object? obj)
        {
            if (obj is User user) return user.UserId == UserId;

            return false;
        }

        [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
        public static bool operator ==(User? user1, User? user2)
        {
            if (ReferenceEquals(user1, user2)) return true;
            if ((object?)user1 == null || (object?)user2 == null) return false;

            return user1.UserId == user2.UserId;
        }
        public static bool operator !=(User? user1, User? user2) => !(user1 == user2);

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
        public override int GetHashCode() => this.UserId;
        #nullable disable
    }
}