
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TrivialPursuit
{
    [Serializable]
    public class Carte
    {
        [XmlAttribute("id")]    
        public int Id { get; set; }

        [XmlAttribute("difficulte")]
            public Enum.Difficulte Difficulte { get; set; }
        
        [XmlElement("Question")]
        public string Question { get; set; }
        
        [XmlArray("Reponses")]
        [XmlArrayItem("Response")]
        public List<Reponse> Responses { get; set; } = new List<Reponse>();
        public Carte(){}

        public Carte(int id, Enum.Difficulte difficulte, string question, List<Reponse> responses)
        {
            Id = id;
            Difficulte = difficulte;
            Question = question;
            Responses = responses;
        }
    
    }
}