using System;
using System.Xml.Serialization;

namespace LBPUnion.ProjectLighthouse.Types.Profiles
{
    /// <summary>
    ///     The location of a slot on a planet.
    /// </summary>
    [XmlType("location")]
    [Serializable]
    public class Location
    {
        [XmlIgnore]
        public int Id { get; set; }

        [XmlElement("x")]
        public int X { get; set; }

        [XmlElement("y")]
        public int Y { get; set; }
    }
}