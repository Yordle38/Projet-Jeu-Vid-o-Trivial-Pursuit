using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Trivial_Pursuit.Jeu.Entites;
using Trivial_Pursuit.Jeu.Enumeration;

namespace Trivial_Pursuit;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont _fontCase;
    
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

        // load Le font des cases
        _fontCase = Content.Load<SpriteFont>("font/FontCase");
        // met ce font par défaut à toute les cases
        Case.SetFontCase(_fontCase);

        
        // Créé la liste des cases
var cases = new List<Case>
{
    // Ligne du haut
    new Case(new Vector2(30, 200), 130, Color.White, TypeCase.VIDE, _textureCase),
    new Case(new Vector2(160, 200), 130, Color.White, TypeCase.VIDE, _textureCase),
    new Case(new Vector2(290, 200), 130, Color.Pink, TypeCase.QUESTION, _textureCase),
    new Case(new Vector2(420, 200), 130, Color.Orange, TypeCase.CHANCE, _textureCase),
    new Case(new Vector2(550, 200), 130, Color.White, TypeCase.VIDE, _textureCase),
    new Case(new Vector2(680, 200), 130, Color.White, TypeCase.JOKER, _textureCase),
    new Case(new Vector2(810, 200), 130, Color.Purple, TypeCase.QUESTION, _textureCase),
    new Case(new Vector2(940, 200), 130, Color.Pink, TypeCase.QUESTION, _textureCase),
    new Case(new Vector2(1070, 200), 130, Color.White, TypeCase.VIDE, _textureCase),

    // Colonne de gauche
    new Case(new Vector2(30, 265), 130, Color.White, TypeCase.VIDE, _textureCase),
    new Case(new Vector2(30, 330), 130, Color.White, TypeCase.VIDE, _textureCase),
    new Case(new Vector2(30, 395), 130, Color.Pink, TypeCase.QUESTION, _textureCase),
    new Case(new Vector2(30, 460), 130, Color.Orange, TypeCase.CHANCE, _textureCase),
    new Case(new Vector2(30, 525), 130, Color.White, TypeCase.VIDE, _textureCase),
    new Case(new Vector2(30, 590), 130, Color.White, TypeCase.JOKER, _textureCase),
    new Case(new Vector2(30, 655), 130, Color.Purple, TypeCase.QUESTION, _textureCase),
    new Case(new Vector2(30, 720), 130, Color.Pink, TypeCase.QUESTION, _textureCase),

    // Colonne de droite
    new Case(new Vector2(1070, 265), 130, Color.White, TypeCase.VIDE, _textureCase),
    new Case(new Vector2(1070, 330), 130, Color.White, TypeCase.VIDE, _textureCase),
    new Case(new Vector2(1070, 395), 130, Color.Pink, TypeCase.QUESTION, _textureCase),
    new Case(new Vector2(1070, 460), 130, Color.Orange, TypeCase.CHANCE, _textureCase),
    new Case(new Vector2(1070, 525), 130, Color.White, TypeCase.VIDE, _textureCase),
    new Case(new Vector2(1070, 590), 130, Color.White, TypeCase.JOKER, _textureCase),
    new Case(new Vector2(1070, 655), 130, Color.Purple, TypeCase.QUESTION, _textureCase),
    new Case(new Vector2(1070, 720), 130, Color.Pink, TypeCase.QUESTION, _textureCase),

    // Ligne du bas
    new Case(new Vector2(30, 720), 130, Color.White, TypeCase.VIDE, _textureCase),
    new Case(new Vector2(160, 720), 130, Color.White, TypeCase.VIDE, _textureCase),
    new Case(new Vector2(290, 720), 130, Color.Pink, TypeCase.QUESTION, _textureCase),
    new Case(new Vector2(420, 720), 130, Color.Orange, TypeCase.CHANCE, _textureCase),
    new Case(new Vector2(550, 720), 130, Color.White, TypeCase.VIDE, _textureCase),
    new Case(new Vector2(680, 720), 130, Color.White, TypeCase.JOKER, _textureCase),
    new Case(new Vector2(810, 720), 130, Color.Purple, TypeCase.QUESTION, _textureCase),
    new Case(new Vector2(940, 720), 130, Color.Pink, TypeCase.QUESTION, _textureCase),
    new Case(new Vector2(1070, 720), 130, Color.White, TypeCase.VIDE, _textureCase),
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
        

        _plateau.Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
