using System.Runtime.InteropServices.JavaScript;
using Microsoft.Xna.Framework;

namespace Trivial_Pursuit.Jeu.Entites;

public class Categorie
{
    private string Nom { get; set; }
    public Color Couleur { get; private  set; }
    
    // constructeur par défaut, couleur blanche et sans categorie
    public Categorie()
    {
        Nom = "Sans categorie";
        Couleur = Color.White;
    }

    public Categorie(string nom, Color couleur)
    {
        Nom = nom;
        Couleur = couleur;
    }

    public string GetNom()
    {
        return Nom;
    }
    
}
