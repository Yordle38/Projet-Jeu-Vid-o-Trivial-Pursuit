using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Trivial_Pursuit.Jeu.Enumeration;

namespace Trivial_Pursuit.Jeu.Entites;

public class Joueur : Sprite
{
    private string _nom;
    private Case _case;
    public int Score { get; private set; }
    private List <Joker> _jokers;
    public EtatJoueur Etat { get; set; }

    public Joueur(string nom,Texture2D texture, Vector2 position, Case caseD) : base(texture, position,80)
    {
        _nom = nom; 
        _case = caseD; // case de départ
        Score = 0;
        _jokers = new List<Joker>();
        Etat = EtatJoueur.Normal;
    }
    
    public string GetNom()
    {
        return _nom;
    }
    
    public Case GetCase()
    {
        return _case;
    }
    
    // Modifie la case du joueur et le place à la position de la case
    public void SetCase(Case nouvelleCase)
    {
        _case = nouvelleCase;
        _position = nouvelleCase.GetPosition();
        SetPositionSurCase();
    }

    public void SetPositionSurCase()
    {
        _position = _case.GetPosition();
        SetPosition(new Vector2(_position.X+40,_position.Y+40));
    }
    
    public void SetPosition(Vector2 position)
    {
        _position = position;
    }
    
    public Vector2 GetPosition()
    {
        return _position;
    }

    // Ajoute un joker à un joueur qu'il ne posséde pas encore
    public void AjouterRandomJoker()
    {

        // initialise la liste des joker
        var typesDeJokers = new List<Joker>
        {
            new Joker("50/50", "Supprime deux mauvaises réponses lors d’une question à choix multiple."),
            new Joker("Relance de question", "Permet de changer la question actuelle par une autre du même thème.")
        };
         // recupere un joker aléatoire
        Random rnd = new Random();
        Joker jokerAleatoire = typesDeJokers[rnd.Next(typesDeJokers.Count)];
    }
   
    // Active le mode ChoixDifficulte du joueur
    public void ActiverChoixDifficulte()
    {
        Etat = EtatJoueur.ChoixDifficulte;
        SetPosition(new Vector2(900, 400));
    }
    
    // FAIRE UNE FONCTION JOUER JOKER
    public void JouerJoker(Joker joker)
    {
        // Supprime le premier joker correspondant dans la liste des joker
        int i = 0;
        while (i < _jokers.Count && _jokers[i] != joker)
        {
            i++;
        }
        _jokers.Remove(_jokers[i]);
    }
    
    
    // Si la réponse est juste augmente le score du joueur et change son état
    // Si le joueur a juste et qu'il était sur une case chance, le fait rejouer
    public void JouerReponse(Reponse reponse)
    {
        Console.WriteLine(_nom + "joue la réponse");
        
        Etat = EtatJoueur.Normal;
        if (reponse.EstCorrecte)
        {
            if (_case.Type == TypeCase.CHANCE)
            {
                Etat = EtatJoueur.Rejouer;
            }
            Console.WriteLine("Il réussi");
            Score++;
        }
    }

    public void Update()
    {
        if (Etat==EtatJoueur.ChoixDifficulte || Etat==EtatJoueur.ChoixReponse)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Up)&& _position.Y>50)
            {
                _position.Y -= 8; 
            }

            if (keyboardState.IsKeyDown(Keys.Down) && _position.Y<850)
            {
                _position.Y += 8;
            }

            if (keyboardState.IsKeyDown(Keys.Right) && _position.X<1550)
            {
                _position.X += 8;
            }

            if (keyboardState.IsKeyDown(Keys.Left) && _position.X>50)
            {
                _position.X -= 8;
            }
        }
    }
    
    public new void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }
}
