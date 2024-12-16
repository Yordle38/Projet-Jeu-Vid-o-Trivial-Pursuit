

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TrivialPursuit
{
    [Serializable]
    public class Joueur
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
        
        [XmlAttribute("nom")]
        public string Nom { get; set; }
        
        [XmlAttribute("score")]
        public int Score { get; set; }
        
        [XmlAttribute("Pion")]
        public Pion Pion { get; set; }
        
        [XmlArray("Jokers")]
        [XmlArrayItem("Joker")]
        public List<Joker> Jokers { get; set; } = new List<Joker>();

        public Joueur()
        {
            
        }

        public Joueur(int id, string nom, int score, Pion pion, List<Joker> jokers)
        {
            Id = id;
            Nom = nom;
            Score = score;
            Pion = pion;
            Jokers = jokers;
        }
    }
}

