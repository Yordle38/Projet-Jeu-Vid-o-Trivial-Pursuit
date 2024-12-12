using System.Collections.Generic;
using Trivial_Pursuit.Jeu.Enumeration;

namespace Trivial_Pursuit.Jeu.Entites;

public class Carte
{
    public Categorie Categorie { get; private set; }
    private string _question;
    public List<Reponse> Reponses { get; private set; }
    private Difficulte _difficulte;

    public Carte(Categorie categorie, string question, List<Reponse> reponses, Difficulte difficulte)
    {
        Categorie = categorie;
        _question = question;
        Reponses = reponses;
        _difficulte = difficulte;
    }

    public string GetQuestion()
    {
        return _question;
    }
    public Difficulte GetDifficulte()
    {
        return _difficulte;
    }
}
