using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Trivial_Pursuit.Jeu.Entites;

public class Partie
{
    private Plateau _plateau;
    private List<Joueur> _joueurs;
    private int _tourJoueur;
    private bool _partieFinit;

    public Partie(Plateau plateau, List<Joueur> joueurs)
    {
        _plateau = plateau;
        _joueurs = joueurs;
        _tourJoueur = 0; // on commence par le premier joueur (le numero 0)
        _partieFinit = false;
    }

    public void jouerPartie()
    {
        while (_partieFinit == false)
        {
            jouerTour();
        }
    }
    // joue un tour
    public void jouerTour()
    {
        // LANCER DE DÉ ET UPDATE DE LA POSITION DU JOUEUR
        Joueur joueurActif = _joueurs[_tourJoueur];
        Case caseJoueur = joueurActif.GetCase();
        int valeurDe = lancerDe();
        
        int numeroCaseActuelle = _plateau.GetIndiceCase(caseJoueur);
        int numeroNouvelleCase = (numeroCaseActuelle + valeurDe) % _plateau.GetNombreCases();
        Case nouvelleCase = _plateau.GetCase(numeroNouvelleCase);

        Console.WriteLine($"{joueurActif.GetNom()} avance de {valeurDe} case(s) et se retrouve sur la case {numeroNouvelleCase}.");

        // Set la case du nouveau joueur et modifie la position du joueur
        joueurActif.SetCase(nouvelleCase);
        DecalageSiSuperposition(joueurActif);

    
        // LE JOUEUR REPONDS EVENTUELLEMENT A SA QUESTION
        
        // UPDATE DU SCORE DU JOUEUR
        
        // CHECK SI LE JOUEUR A GAGNÉ
        if (joueurActif.GetScore() == 6)
        {
            _partieFinit = true;
        }
        
        // Choisi le prochain joueur à jouer
        _tourJoueur = (_tourJoueur + 1) % _joueurs.Count;

    }

    // Retourne le lancé de Dé entre 1 et 6
    public int lancerDe()
    {
        Random rnd = new Random();
        return rnd.Next(1, 7);
    }

    
    // Décale la position des joueurs s'ils se superposent
    private void DecalageSiSuperposition(Joueur joueur)
    {
        // check si un autre joueur est sur la même case
        foreach (var autreJoueur in _joueurs)
        {
            if (joueur != autreJoueur && joueur.GetCase() == autreJoueur.GetCase())
            {
                joueur.SetPosition(new Vector2( joueur.GetPosition().X + 30,  joueur.GetPosition().Y));
            }
        }
    }
    
    
}