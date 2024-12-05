using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Trivial_Pursuit.Jeu.Entites;

public class Plateau
{
    private List<Case> _cases;
    private Texture2D _background;

    public Plateau(Texture2D background, List<Case> cases)
    {
        _cases = cases;
        _background = background;
    }

    public void ajouterCase(Case nvCase)
    {
        _cases.Add(nvCase);
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_background, new Rectangle(0, 0, 1600, 900), Color.White);

        // Draw toute les cases
        foreach (var caseAct in _cases)
        {
            caseAct.Draw(spriteBatch);
        }
    }
}