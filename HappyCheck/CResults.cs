using System.Collections.Generic;
using System.Xml.Serialization;

namespace StaticCheck
{
    [XmlRoot("results")]
    public class CResults
    {
        [XmlElement("cppcheck")]
        public CCppCheck CppCheck { get; set; }

        [XmlElement("errors")]
        public List<CError> Errors { get; set; }
    }

    public class CError
    {
        [XmlAttribute("id")]
        public string Id { get; set; }

        [XmlAttribute("severity")]
        public string Severity { get; set; }

        [XmlAttribute("msg")]
        public string Msg { get; set; }

        [XmlAttribute("verbose")]
        public string Verbose { get; set; }

        [XmlAttribute("location")]
        public CLocation Location { get; set; }
    }

    public class CLocation
    {
        [XmlAttribute("file0")]
        public string file0 { get; set; }

        [XmlAttribute("file")]
        public string file { get; set; }

        [XmlAttribute("line")]
        public string line { get; set; }
    }

    public class CCppCheck
    {
        [XmlAttribute("version")]
        public string Version { get; set; }

    }
}
