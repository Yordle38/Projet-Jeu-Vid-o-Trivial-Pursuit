using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TrivialPursuit
{
    [Serializable] 
    public class Case
    {
        [XmlAttribute("type")]
        public Enum.TYPECASE Type { get; set; }
    
        [XmlAttribute("couleur")]
        public Enum.Couleur Couleur { get; set; }
    
        [XmlAttribute("Sprite")]
        public Sprite Sprite { get; set; }
    
        [XmlAttribute("Joker")]
        public Joker Joker { get; set; }
        
        public Case(){}

        public Case(Enum.TYPECASE type, Enum.Couleur couleur, Sprite sprite, Joker joker)
        {
            Type = type;
            Couleur = couleur;
            Sprite = sprite;
            Joker = joker;
        }
    }
}

