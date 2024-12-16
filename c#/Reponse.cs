
using System;
using System.Xml.Serialization;
namespace TrivialPursuit
{
    [Serializable]
    public class Reponse
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
        
        [XmlAttribute("texte")]
        public string Texte { get; set; }
        
        [XmlAttribute("correct")]
        public bool Correct { get; set; }

        public Reponse()
        {
            
        }

        public Reponse(int id, string texte, bool correct)
        {
            Id = id;
            Texte = texte;
            Correct = correct;
        }
    }
}

