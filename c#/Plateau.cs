using System;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
using System.Xml.Serialization;

namespace TrivialPursuit
{
    [Serializable]
    public class Plateau
    {
        [XmlElement("Cases")] 
        public List<Case> Cases { get; set; } = new List<Case>();
        [XmlElement("Background")]
        public Background Background { get; set; }
    }
}

