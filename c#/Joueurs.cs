using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TrivialPursuit
{
    [Serializable]
    public class Joueurs
    {
        [XmlElement("Joueur")]
        public List<Joueur> JoueursList { get; set; }

        public Joueurs()
        {
            JoueursList = new List<Joueur>();
        }

        public Joueurs(List<Joueur> joueursList)
        {
            JoueursList = joueursList;
        }
    }
}