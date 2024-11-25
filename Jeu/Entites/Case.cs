using System.Drawing;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.Xna.Framework;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Microsoft.Xna.Framework.Graphics;

namespace Trivial_Pursuit.Jeu.Entites;

// une case est un sprite et donc en hérite
public class Case : Sprite
{
    private bool EstJoker { get; set; }
    public Case(Vector2 position, int taille, Color couleur, bool estJoker, Texture2D texture) : base(texture, position,taille, couleur) // constructeur de sprite
    {
        EstJoker = estJoker;
    }
}
    