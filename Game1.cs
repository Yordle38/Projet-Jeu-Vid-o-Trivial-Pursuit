using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
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
    private List<Texture2D> _textureJoueurs;
    private Case _case1;
    private Case _case2;
    private Plateau _plateau;
    private List<Joueur> _joueurs;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _textureJoueurs = new List<Texture2D>();
        _joueurs = new List<Joueur>();

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

        // Load l'image des joueurs 
        _textureJoueurs.Add(Content.Load<Texture2D>("Images/pion-rouge"));
        _textureJoueurs.Add(Content.Load<Texture2D>("Images/pion-bleu"));

        // load Le font des cases
        _fontCase = Content.Load<SpriteFont>("font/FontCase");
        // met ce font par défaut à toute les cases
        Case.SetFontCase(_fontCase);

        Color clrSport  = new Color(255, 111, 97);
        Color clrNature = new Color(136, 212, 152);
        Color clrZytho = new Color(255, 204, 92);
        Color clrJeuxVideo = new Color(240, 147, 251);
        Color clrHistoire = new Color(178, 235, 242);
        Color clrMusique = new Color(106, 130, 251);

        Categorie sport = new Categorie("Sport", clrSport);
        Categorie nature = new Categorie("Nature", clrNature);
        Categorie zytho = new Categorie("Zythologie", clrZytho);
        Categorie jeuxVideo = new Categorie("Jeux vidéo", clrJeuxVideo);
        Categorie histoire = new Categorie("Histoire", clrHistoire);
        Categorie musique = new Categorie("Musique", clrMusique);
        // Créé la liste des cases
        
    var cases = new List<Case>
    {
        // Ligne du haut
        new Case(new Vector2(30, 200), 130, TypeCase.VIDE, _textureCase),
        new Case(new Vector2(160, 200), 130, TypeCase.VIDE, _textureCase),
        new Case(new Vector2(290, 200), 130, TypeCase.QUESTION, _textureCase),
        new Case(new Vector2(420, 200), 130, TypeCase.CHANCE, _textureCase, sport),
        new Case(new Vector2(550, 200), 130, TypeCase.VIDE, _textureCase, zytho),
        new Case(new Vector2(680, 200), 130, TypeCase.JOKER, _textureCase, nature),
        new Case(new Vector2(810, 200), 130, TypeCase.QUESTION, _textureCase, jeuxVideo),
        new Case(new Vector2(940, 200), 130, TypeCase.QUESTION, _textureCase, histoire),
        new Case(new Vector2(1070, 200), 130, TypeCase.VIDE, _textureCase, musique),

        // Colonne de gauche
        new Case(new Vector2(30, 265), 130, TypeCase.VIDE, _textureCase),
        new Case(new Vector2(30, 330), 130, TypeCase.VIDE, _textureCase),
        new Case(new Vector2(30, 395), 130, TypeCase.QUESTION, _textureCase),
        new Case(new Vector2(30, 460), 130, TypeCase.CHANCE, _textureCase),
        new Case(new Vector2(30, 525), 130, TypeCase.VIDE, _textureCase),
        new Case(new Vector2(30, 590), 130, TypeCase.JOKER, _textureCase),
        new Case(new Vector2(30, 655), 130, TypeCase.QUESTION, _textureCase),
        new Case(new Vector2(30, 720), 130, TypeCase.QUESTION, _textureCase),

        // Colonne de droite
        new Case(new Vector2(1070, 265), 130, TypeCase.VIDE, _textureCase),
        new Case(new Vector2(1070, 330), 130, TypeCase.VIDE, _textureCase),
        new Case(new Vector2(1070, 395), 130, TypeCase.QUESTION, _textureCase),
        new Case(new Vector2(1070, 460), 130, TypeCase.CHANCE, _textureCase),
        new Case(new Vector2(1070, 525), 130, TypeCase.VIDE, _textureCase),
        new Case(new Vector2(1070, 590), 130, TypeCase.JOKER, _textureCase),
        new Case(new Vector2(1070, 655), 130, TypeCase.QUESTION, _textureCase),
        new Case(new Vector2(1070, 720), 130, TypeCase.QUESTION, _textureCase),

        // Ligne du bas
        new Case(new Vector2(30, 720), 130, TypeCase.VIDE, _textureCase),
        new Case(new Vector2(160, 720), 130, TypeCase.VIDE, _textureCase),
        new Case(new Vector2(290, 720), 130, TypeCase.QUESTION, _textureCase),
        new Case(new Vector2(420, 720), 130, TypeCase.CHANCE, _textureCase),
        new Case(new Vector2(550, 720), 130, TypeCase.VIDE, _textureCase),
        new Case(new Vector2(680, 720), 130, TypeCase.JOKER, _textureCase),
        new Case(new Vector2(810, 720), 130, TypeCase.QUESTION, _textureCase),
        new Case(new Vector2(940, 720), 130, TypeCase.QUESTION, _textureCase),
        new Case(new Vector2(1070, 720), 130, TypeCase.VIDE, _textureCase),
};

    _plateau = new Plateau(_backgroundPlateau, cases);
    Joueur j1 = new Joueur("Lilian", _textureJoueurs[0], new Vector2(50, 50), cases[0]);
    Joueur j2 = new Joueur("Doriane", _textureJoueurs[0], new Vector2(50, 50), cases[0]);

    _joueurs.Add(j1);
    _joueurs.Add(j2);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        foreach (var joueur in _joueurs)
        {
            if (joueur.Actif) 
            {
                joueur.update();
            }
        }
        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        

        _plateau.Draw(_spriteBatch);
        _joueurs[0].Draw(_spriteBatch); // Determiner plus tard c'était à quel joueur de jouer et déterminer en fonction

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
