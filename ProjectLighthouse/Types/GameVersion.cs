using System.Xml.Serialization;

namespace LBPUnion.ProjectLighthouse.Types
{
    public enum GameVersion
    {
        [XmlEnum("0")]
        LittleBigPlanet1 = 0,

        [XmlEnum("1")]
        LittleBigPlanet2 = 1,

        [XmlEnum("2")]
        LittleBigPlanet3 = 2,

        [XmlEnum("3")]
        LittleBigPlanetVita = 3,

        [XmlEnum("4")]
        LittleBigPlanetPSP = 4,

        [XmlEnum("-1")]
        Unknown = -1,
    }

    public static class GameVersionExtensions
    {
        public static string ToPrettyString(this GameVersion gameVersion) => gameVersion.ToString().Replace("LittleBigPlanet", "LittleBigPlanet ");
    }
}