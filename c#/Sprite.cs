using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TrivialPursuit
{
    [Serializable]
    public class Sprite
    {
        [XmlAttribute("texture")]
        public string Type { get; set; }
    
        [XmlAttribute("positionX")]
        public int PositionX { get; set; }
    
        [XmlAttribute("PositionY")]
        public int PositionY { get; set; }
    
        [XmlAttribute("size")]
        public int Size { get; set; }
    }
}


