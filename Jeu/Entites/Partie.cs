using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Trivial_Pursuit.Jeu.Enumeration;

namespace Trivial_Pursuit.Jeu.Entites;

public class Partie
{
    private Plateau _plateau;
    public List<Joueur> Joueurs { get; private set; }
    private List<Carte> _cartes;
    private List<Carte> _cartesJouees;
    public Carte CartePiochee { get; set; }
    public int TourJoueur { get; set; }

    public Partie(Plateau plateau, List<Joueur> joueurs,List<Carte> cartes)
    {
        _plateau = plateau;
        Joueurs = joueurs;
        _cartes = cartes;
        _cartesJouees = new List<Carte>();
        CartePiochee = null;
        TourJoueur = 0; // on commence par le premier joueur (le numero 0)
    }

    // joue un tour
    public void JouerTour()
    {
        // LANCER DE DÉ ET UPDATE DE LA POSITION DU JOUEUR
        Joueur joueurActif = Joueurs[TourJoueur];
        Case caseJoueur = joueurActif.GetCase();
        int valeurDe = LancerDe();
        
        int numeroCaseActuelle = _plateau.GetIndiceCase(caseJoueur);
        int numeroNouvelleCase = (numeroCaseActuelle + valeurDe) % _plateau.GetNombreCases();
        Case nouvelleCase = _plateau.GetCase(numeroNouvelleCase);

        Console.WriteLine($"{joueurActif.GetNom()} avance de {valeurDe} case(s) et se retrouve sur la case {numeroNouvelleCase}.");

        // Set la case du nouveau joueur et modifie la position du joueur
        joueurActif.SetCase(nouvelleCase);
        DecalageSiSuperposition(joueurActif);

        switch (joueurActif.GetCase().Type)
        {
            case TypeCase.JOKER:
                joueurActif.AjouterRandomJoker();
                // Choisi le prochain joueur à jouer
                TourJoueur = (TourJoueur + 1) % Joueurs.Count;
                break;
            
            case TypeCase.CHANCE:
                joueurActif.Etat = EtatJoueur.AttenteConfirmation;
                break;
            
            case  TypeCase.QUESTION:
                joueurActif.Etat = EtatJoueur.AttenteConfirmation;

                break;
            case TypeCase.VIDE:
                // Choisi le prochain joueur à jouer
                TourJoueur = (TourJoueur + 1) % Joueurs.Count;
                break;
        }
    }
    
    // Choisi une carte non jouée dans le jeu de la catégorie et difficulté demandée de manière aléatoire puis set CartePiochee
    public void PiocherCarte(Categorie categorie, Difficulte difficulte)
    {
        // Recupere les cartes de la bonne difficulté et bonne catégorie
        var cartesFiltrees = _cartes.Where(c => c.Categorie == categorie && c.GetDifficulte() == difficulte && !_cartesJouees.Contains(c)).ToList();

        Console.WriteLine("Difficulte: " + difficulte + " Categorie: " + categorie.GetNom());
        Console.WriteLine("Nombre de carte filtrees: " + cartesFiltrees.Count);
        
        // Si toute les cartes de la catégorie et difficulté déjà jouées, on autorise à rejouer les cartes
        if (cartesFiltrees.Count == 0)
        {
            Console.WriteLine("Toute les cartes de la catégorie {0} et de difficulté {1} déjà jouées, on rejoue des cartes",categorie,difficulte);
            cartesFiltrees = _cartes.Where(c => c.Categorie == categorie && c.GetDifficulte() == difficulte).ToList();
        }
        
        // Choisit une carte aléatoire dans le lot
        Random rnd = new Random();
        
        CartePiochee = cartesFiltrees[rnd.Next(cartesFiltrees.Count)];
        _cartesJouees.Add(CartePiochee);
    }

    // Retourne un chiffre entre 1 et 6
    public int LancerDe()
    {
        Random rnd = new Random();
        return rnd.Next(1, 7);
    }

    
    // Décale la position des joueurs s'ils se superposent
    private void DecalageSiSuperposition(Joueur joueur)
    {
        // check si un autre joueur est sur la même case
        foreach (var autreJoueur in Joueurs)
        {
            if (joueur != autreJoueur && joueur.GetCase() == autreJoueur.GetCase())
            {
                joueur.SetPosition(new Vector2( joueur.GetPosition().X + 30,  joueur.GetPosition().Y));
            }
        }
    }

    public bool EstTerminee()
    {
        foreach (var joueur in Joueurs)
        {
            if (joueur.Score == 6)
            {
                return true;
            }
        }
        return false;
    }
    
    
}
