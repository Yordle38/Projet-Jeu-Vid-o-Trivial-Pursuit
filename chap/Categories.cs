
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TrivialPursuit.chap
{
    [Serializable]
    public class Categories
    {
    [XmlElement("Categorie")]
    public List<Categorie> CategoriesList { get; set; }

    public Categories()
    {
        
    }

    public Categories(List<Categorie> categoriesList)
    {
        CategoriesList = categoriesList;
    }
    }
}

