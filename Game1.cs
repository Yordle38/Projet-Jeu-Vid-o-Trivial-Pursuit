using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Trivial_Pursuit.Jeu.Entites;

namespace Trivial_Pursuit;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    private Texture2D _textureCase;
    private Texture2D _backgroundPlateau;
    private Case _case1;
    private Case _case2;
    private Plateau _plateau;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
    }

    protected override void Initialize()
    {
        
        // Réglage de la taille du tableau
        _graphics.PreferredBackBufferWidth = 1600;
        _graphics.PreferredBackBufferHeight = 900;
        _graphics.ApplyChanges();
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        // TODO: use this.Content to load your game content here

        // Load l'image des cases
        _textureCase = Content.Load<Texture2D>("Images/case_3");
        _backgroundPlateau = Content.Load<Texture2D>("Images/fond_plateau");

        //_case1 = new Case(new Vector2(300,300), 200, Color.Red, false, _texture); // case de test, normalement affichage de plateau
        //_case2 = new Case(new Vector2(300,400), 200, Color.Aqua, false, _texture); // case de test, normalement affichage de plateau
        
        // Créer une liste de cases
        List<Case> cases = new List<Case>
        {
            new Case(new Vector2(150, 300), 200, Color.Red, false, _textureCase),
            new Case(new Vector2(150, 400), 200, Color.Green, false, _textureCase),
            new Case(new Vector2(150, 500), 200, Color.Blue, false, _textureCase),
            new Case(new Vector2(150, 300), 200, Color.Red, false, _textureCase),
            new Case(new Vector2(150, 400), 200, Color.Green, false, _textureCase),
            new Case(new Vector2(150, 300), 200, Color.Red, false, _textureCase),
            new Case(new Vector2(150, 400), 200, Color.Green, false, _textureCase),
            new Case(new Vector2(150, 300), 200, Color.Red, false, _textureCase),
            new Case(new Vector2(150, 400), 200, Color.Green, false, _textureCase),
            new Case(new Vector2(150, 300), 200, Color.Red, false, _textureCase),
            new Case(new Vector2(150, 400), 200, Color.Green, false, _textureCase),
            new Case(new Vector2(150, 300), 200, Color.Red, false, _textureCase),
            new Case(new Vector2(150, 400), 200, Color.Green, false, _textureCase),
            new Case(new Vector2(150, 300), 200, Color.Red, false, _textureCase),
            new Case(new Vector2(150, 400), 200, Color.Green, false, _textureCase),
            new Case(new Vector2(150, 300), 200, Color.Red, false, _textureCase),
            new Case(new Vector2(150, 400), 200, Color.Green, false, _textureCase),
            new Case(new Vector2(150, 300), 200, Color.Red, false, _textureCase),
            new Case(new Vector2(150, 400), 200, Color.Green, false, _textureCase),
            new Case(new Vector2(150, 300), 200, Color.Red, false, _textureCase),
            new Case(new Vector2(150, 400), 200, Color.Green, false, _textureCase),
            new Case(new Vector2(150, 300), 200, Color.Red, false, _textureCase),
            new Case(new Vector2(150, 400), 200, Color.Green, false, _textureCase),
            new Case(new Vector2(150, 300), 200, Color.Red, false, _textureCase),
            new Case(new Vector2(150, 400), 200, Color.Green, false, _textureCase),
        };

        // Initialiser le plateau avec la liste de cases et l'image de fond
        _plateau = new Plateau(_backgroundPlateau, cases);
        
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        
        // Draw les sprite
  //      _case1.Draw(_spriteBatch);
//        _case2.Draw(_spriteBatch);
        _plateau.Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
