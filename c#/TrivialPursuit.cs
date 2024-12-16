using System;
using System.Collections.Generic;
using System.Xml.Serialization;


namespace TrivialPursuit

{
    [Serializable]
    [XmlRoot("TrivialPursuit")]
    public class TrivialPursuit
    {
        [XmlElement("Plateau")]
        public Plateau Plateau { get; set; }

        [XmlArray("Categories")]
        [XmlArrayItem("Categorie")]
        public List<Categorie> Categories { get; set; } = new List<Categorie>();
        
        [XmlArray("Joueurs")]
        [XmlArrayItem("Joueur")]
        public List<Joueur> Joueurs { get; set; } = new List<Joueur>();

        public TrivialPursuit()
        {
            
        }

        public TrivialPursuit(Plateau plateau, List<Categorie> categories, List<Joueur> joueurs)
        {
            Plateau = plateau;
            Categories = categories;
            Joueurs = joueurs;
        }
    }
}