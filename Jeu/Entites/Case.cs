using System.Drawing;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.Xna.Framework;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Microsoft.Xna.Framework.Graphics;
using Trivial_Pursuit.Jeu.Enumeration;

namespace Trivial_Pursuit.Jeu.Entites;

// une case est un sprite et donc en hérite
public class Case : Sprite
{
    private Color Couleur { get; set; }

    private TypeCase Type { get; set; }
    private static SpriteFont _fontCase;

    public Case(Vector2 position, int taille, Color couleur, TypeCase typeCase, Texture2D texture) 
        : base(texture, position, taille, couleur) // constructeur de Sprite
    {
        Couleur = couleur;
        Type = typeCase;
    }
    
    public static void SetFontCase(SpriteFont font)
    {
        _fontCase = font;
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

        string text = "";
        
        if (Type == TypeCase.JOKER)
        {
            text = "joker";
            // Calcul la taille du texte
            Vector2 textSize = _fontCase.MeasureString(text);

            // Adapte la position du texte pour le centrer
            Vector2 textPosition = new Vector2(
                _position.X + (longueur / 2) - (textSize.X / 2),
                _position.Y + (largeur / 2) - (textSize.Y / 2)
            );
            
            spriteBatch.DrawString(_fontCase, text, textPosition, Color.Black); // Dessine le texte
        }
        
        else if (Type == TypeCase.CHANCE)
        {
            text = "chance";
            // Calcul la taille du texte
            Vector2 textSize = _fontCase.MeasureString(text);

            // Adapte la position du texte pour le centrer
            Vector2 textPosition = new Vector2(
                _position.X + (longueur / 2) - (textSize.X / 2),
                _position.Y + (largeur / 2) - (textSize.Y / 2)
            );
            
            spriteBatch.DrawString(_fontCase, text, textPosition, Color.Black); // Dessine le texte
        }
    }
}
    