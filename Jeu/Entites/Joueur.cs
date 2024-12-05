using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Trivial_Pursuit.Jeu.Entites;

public class Joueur : Sprite
{
    private string _nom;
    private Case _case;
    private int _score;
    private List <Joker> _jokers;
    public bool Actif {  get; set; } // Permet d'activer ou désactiver les mouvements d'un joueur


    public Joueur(string nom,Texture2D texture, Vector2 position, Case caseD) : base(texture, position,80)
    {
        _nom = nom;
        _case = caseD; // case de départ
        _score = 0;
        _jokers = new List<Joker>();
        Actif = true; // innactifs à la création
    }

    public void update()
    {
        if (Actif)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                _position.Y -= 8; 
            }

            if (keyboardState.IsKeyDown(Keys.Down))
            {
                _position.Y += 8;
            }

            if (keyboardState.IsKeyDown(Keys.Right))
            {
                _position.X += 8;
            }

            if (keyboardState.IsKeyDown(Keys.Left))
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