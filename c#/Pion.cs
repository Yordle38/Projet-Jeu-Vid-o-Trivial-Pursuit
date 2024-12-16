using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TrivialPursuit
{
    [Serializable]
    public class Pion
    {
        [XmlAttribute("nom")]
        public string Nom { get; set; }
        
        [XmlAttribute("imageLink")]
        public string ImageLink { get; set; }
        
        [XmlAttribute("description")]
        public string Description { get; set; }
        
        [XmlAttribute("case")]
        public string Case { get; set; }
    }
}

