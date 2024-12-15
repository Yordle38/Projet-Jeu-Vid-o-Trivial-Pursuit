using System;
using System.Collections.Generic;
using System.Linq;
using Trivial_Pursuit.Jeu.Enumeration;

namespace Trivial_Pursuit.Jeu.Entites;

public class Joker
{
    private string _nom;
    private string _effet;

    public Joker(string nom, string effet)
    {
        _nom = nom;
        _effet = effet;
    }

    public string getNom()
    {
        return _nom;
    }

    public string getEffet()
    {
        return _effet;
    }
    
    // Retourne la moitié des réponses dont une juste
    public List<Reponse> Jouer5050(Carte carte)
    {
        List<Reponse> reponses5050 = new List<Reponse>();

        // Recupere la premiere reponse juste et la première réponse fausse
        Reponse reponseJuste = carte.Reponses.FirstOrDefault(r => r.EstCorrecte);
        Reponse reponseMauvaise = carte.Reponses.FirstOrDefault(r => !r.EstCorrecte);

        // Si les réponses sont trouvées, ajoute-les à la liste
        if (reponseJuste != null)
            reponses5050.Add(reponseJuste);
        if (reponseMauvaise != null)
            reponses5050.Add(reponseMauvaise);
        return reponses5050;
    }
    
    public void JouerRelance(Partie partie)
    {
        partie.PiocherCarte(partie.CartePiochee.Categorie, partie.CartePiochee.GetDifficulte());
        Console.WriteLine("Nouvelle carte jouee: " + partie.CartePiochee.GetQuestion());

        
    }
    
}
