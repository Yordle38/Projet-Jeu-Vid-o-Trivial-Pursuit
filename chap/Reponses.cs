using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TrivialPursuit.chap
{
    public class Reponses
    {
        [XmlElement("Reponse")] 
        public List<Reponse> ResponsesList { get; set; } 

        public Reponses()
        {
            
        }

        public Reponses(List<Reponse> responsesList)
        {
            ResponsesList = responsesList;
        }
    }
}