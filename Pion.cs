using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct3D11;
using SharpDX.Direct3D9;

namespace Trivial_Pursuit.Jeu.Entites;

public class Pion
{
    public Case CaseActuelle { get; private set; }
    private Texture2D _texture;
    
    public Pion(Case caseInitiale, Texture2D texture)
    {
        CaseActuelle=caseInitiale;
        _texture=texture;
    }


    public void Deplacer(Case nouvelleCase)
    {
        CaseActuelle=nouvelleCase;
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        if (CaseActuelle != null)
        {
            Vector2 position = CaseActuelle.Position + new Vector2(25, 25);
            SpriteBatch.Draw(_texture, position, Color.White);
        }
    }
}
