using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TrivialPursuit
{
    [Serializable]
    public class Categorie
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
        
        [XmlAttribute("nom")]
        public string Nom { get; set; }
        
        [XmlAttribute("couleur")]
        public  Enum.Couleur Couleur { get; set; }
        
        [XmlArray("Cartes")]
        [XmlArrayItem("Carte")]
        public List<Carte> Cartes { get; set; } = new List<Carte>();

        public Categorie()
        {
            
        }

        public Categorie(int id, string nom, Enum.Couleur couleur, List<Carte> cartes)
        {
            Id = id;
            Nom = nom;
            Couleur = couleur;
            Cartes = new List<Carte>();
        }
    }
}

