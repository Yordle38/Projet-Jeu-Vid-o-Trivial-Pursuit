using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TrivialPursuit
{
    public class Responses
    {
        [XmlElement("Reponse")] 
        public List<Reponse> ResponsesList { get; set; } = new List<Reponse>();

        public Responses()
        {
            
        }

        public Responses(List<Reponse> responsesList)
        {
            ResponsesList = responsesList;
        }
    }
}