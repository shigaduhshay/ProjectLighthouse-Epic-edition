using System.Xml.Serialization;

namespace LBPUnion.ProjectLighthouse.Types
{
    /// <summary>
    ///     Response to POST /login
    /// </summary>
    [XmlRoot("loginResult")]
    [XmlType("loginResult")]
    public class LoginResult
    {
        [XmlElement("authTicket")]
        public string AuthTicket { get; set; }

        [XmlElement("lbpEnvVer")]
        public string LbpEnvVer { get; set; }
    }
}