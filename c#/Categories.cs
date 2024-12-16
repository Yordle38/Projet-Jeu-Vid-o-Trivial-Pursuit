
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TrivialPursuit
{
    [Serializable]
    public class Categories
    {
    [XmlElement("Categorie")]
    public List<Categorie> CategoriesList { get; set; }

    public Categories()
    {
        CategoriesList = new List<Categorie>();
    }

    public Categories(List<Categorie> categoriesList)
    {
        CategoriesList = categoriesList;
    }
    }
}

