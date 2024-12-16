using System;
using System.Xml.Serialization;

namespace TrivialPursuit
{
    [Serializable]
    public class Background
    {
        [XmlAttribute("texture")]
        public string Texture { get; set; }
    }
}

