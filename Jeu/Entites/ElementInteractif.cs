using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Trivial_Pursuit.Jeu.Entites;

// Créé un élément intéractif, c'est à dire un carré avec du texte et une couleur
public class ElementInteractif
{
    public Rectangle Rectangle { get; set; }
    public Color Couleur { get; set; }
    public string Texte { get; set; }

    public ElementInteractif(Rectangle rectangle, Color couleur, string texte)
    {
        Rectangle = rectangle;
        Couleur = couleur;
        Texte = texte;
    }

    public void Draw(SpriteBatch spriteBatch, Texture2D texture, SpriteFont font, Color textColor)
    {
        spriteBatch.Draw(texture, Rectangle, Couleur);
        
        Vector2 textSize = font.MeasureString(Texte);
        Vector2 textPosition = new Vector2(
            Rectangle.X + (Rectangle.Width / 2) - (textSize.X / 2),
            Rectangle.Y + (Rectangle.Height / 2) - (textSize.Y / 2)
        );

        spriteBatch.DrawString(font, Texte, textPosition, textColor);
    }
}