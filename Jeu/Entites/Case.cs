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
    private Color Couleur { get; set; }
    private bool EstJoker { get; set; }

    public Case(Vector2 position, int taille, Color couleur, bool estJoker, Texture2D texture) 
        : base(texture, position, taille, couleur) // constructeur de Sprite
    {
        Couleur = couleur;
        EstJoker = estJoker;
    }
    
    // Redéfinition de Draw (dans Sprite) pour que le rectangle soit au bon format
    public new void Draw(SpriteBatch spriteBatch)
    {
        // Calcule les dimentions de l'image
        int longueur = _Size;
        int largeur = (int)((float)_texture.Height / _texture.Width * longueur);
        
        // Créé un rectangle avec les dimentions calculées
        Rectangle rectangle = new Rectangle((int)_position.X, (int)_position.Y, longueur, largeur);
        
        spriteBatch.Draw(_texture, rectangle, Couleur);
    }
}
    