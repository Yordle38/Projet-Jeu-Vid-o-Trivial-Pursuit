using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Trivial_Pursuit.Jeu.Entites;
using Trivial_Pursuit.Jeu.Enumeration;

namespace Trivial_Pursuit;

enum Scenes
{
    SETUP,
    PLATEAU,
    CHOIX_DIFFICULTE,
    CHOIX_REPONSE,
    TOUR_TERMINE,
    FIN_PARTIE
};
public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont _fontCase;
    private KeyboardState ancienEtat;
    private Scenes SceneActive { get; set; }
    
    private Texture2D _textureCase;
    private Texture2D _backgroundPlateau;
    private List<Texture2D> _textureJoueurs;
    private Plateau _plateau;
    private List<Joueur> _joueurs;
    private Partie _partie;
    
    //private double _tmpDerniereAction = 0; // Temps écoulé depuis la dernière action
    //private const double _dureeAttente = 1.0; // attente en seconde avant la prochaine

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        _textureJoueurs = new List<Texture2D>();
        _joueurs = new List<Joueur>();
        SceneActive = Scenes.PLATEAU; // à changer en setup

    }

    protected override void Initialize()
    {
        // Réglage de la taille du tableau
        _graphics.PreferredBackBufferWidth = 1600;
        _graphics.PreferredBackBufferHeight = 900;
        _graphics.ApplyChanges();
        
        ancienEtat = Keyboard.GetState();
        
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
            new Case(new Vector2(420, 200), 130, TypeCase.CHANCE, _textureCase, zytho),
            new Case(new Vector2(550, 200), 130, TypeCase.VIDE, _textureCase, zytho),
            new Case(new Vector2(680, 200), 130, TypeCase.JOKER, _textureCase, zytho),
            new Case(new Vector2(810, 200), 130, TypeCase.QUESTION, _textureCase, zytho),
            new Case(new Vector2(940, 200), 130, TypeCase.QUESTION, _textureCase, histoire),
            new Case(new Vector2(1070, 200), 130, TypeCase.VIDE, _textureCase, zytho),

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
            new Case(new Vector2(1070, 720), 130, TypeCase.VIDE, _textureCase),
            new Case(new Vector2(940, 720), 130, TypeCase.QUESTION, _textureCase),
            new Case(new Vector2(810, 720), 130, TypeCase.QUESTION, _textureCase),
            new Case(new Vector2(680, 720), 130, TypeCase.JOKER, _textureCase),
            new Case(new Vector2(550, 720), 130, TypeCase.VIDE, _textureCase),
            new Case(new Vector2(420, 720), 130, TypeCase.CHANCE, _textureCase),
            new Case(new Vector2(290, 720), 130, TypeCase.QUESTION, _textureCase),
            new Case(new Vector2(160, 720), 130, TypeCase.VIDE, _textureCase),
            new Case(new Vector2(30, 720), 130, TypeCase.VIDE, _textureCase),
            
            // Colonne de gauche
            new Case(new Vector2(30, 720), 130, TypeCase.QUESTION, _textureCase),
            new Case(new Vector2(30, 655), 130, TypeCase.QUESTION, _textureCase),
            new Case(new Vector2(30, 590), 130, TypeCase.JOKER, _textureCase),
            new Case(new Vector2(30, 525), 130, TypeCase.VIDE, _textureCase),
            new Case(new Vector2(30, 460), 130, TypeCase.CHANCE, _textureCase),
            new Case(new Vector2(30, 395), 130, TypeCase.QUESTION, _textureCase),
            new Case(new Vector2(30, 330), 130, TypeCase.VIDE, _textureCase),
            new Case(new Vector2(30, 265), 130, TypeCase.VIDE, _textureCase),
        };
        
        // Créé un échantillon de cartes
        List<Carte> cartes = new List<Carte>
        {
            // Zythologie - Facile
            new Carte(zytho, "Quel est le principal ingrédient de la bière ?",
                new List<Reponse>
                {
                    new Reponse("Orge", true),
                    new Reponse("Blé", false),
                    new Reponse("Maïs", false),
                    new Reponse("Riz", false)
                },
                Difficulte.Facile),

            new Carte(zytho,
                "Quel est le nom du processus où les levures transforment le sucre en alcool dans la bière ?",
                new List<Reponse>
                {
                    new Reponse("Fermentation", true),
                    new Reponse("Distillation", false),
                    new Reponse("Macération", false),
                    new Reponse("Filtration", false)
                },
                Difficulte.Facile),

            new Carte(zytho,
                "Quel type de bière est généralement plus foncé et plus riche en saveur ?",
                new List<Reponse>
                {
                    new Reponse("Stout", true),
                    new Reponse("Lager", false),
                    new Reponse("Pilsner", false),
                    new Reponse("Blonde", false)
                },
                Difficulte.Facile),

            new Carte(zytho, "Quel pays est célèbre pour ses bières trappistes ?",
                new List<Reponse>
                {
                    new Reponse("Belgique", true),
                    new Reponse("Allemagne", false),
                    new Reponse("République Tchèque", false),
                    new Reponse("Pays-Bas", false)
                },
                Difficulte.Facile),

            // Zythologie - Moyen
            new Carte(zytho, "Quel type de levure est utilisé pour les bières de type ale ?",
                new List<Reponse>
                {
                    new Reponse("Levure haute", true),
                    new Reponse("Levure basse", false),
                    new Reponse("Levure sauvage", false),
                    new Reponse("Levure sèche", false)
                },
                Difficulte.Moyen),

            new Carte(zytho,
                "Quel est le nom du récipient utilisé pour la fermentation de la bière ?",
                new List<Reponse>
                {
                    new Reponse("Cuve de fermentation", true),
                    new Reponse("Alambic", false),
                    new Reponse("Fût", false),
                    new Reponse("Tonneau", false)
                },
                Difficulte.Moyen),

            new Carte(zytho, "Quel type de bière est souvent associé à la ville de Munich ?",
                new List<Reponse>
                {
                    new Reponse("Weizen", true),
                    new Reponse("Porter", false),
                    new Reponse("IPA", false),
                    new Reponse("Lambic", false)
                },
                Difficulte.Moyen),

            new Carte(zytho,
                "Quel est le nom du processus qui consiste à ajouter du houblon à la bière ?",
                new List<Reponse>
                {
                    new Reponse("Houblonnage", true),
                    new Reponse("Maltage", false),
                    new Reponse("Brassage", false),
                    new Reponse("Filtration", false)
                },
                Difficulte.Moyen),

            // Zythologie - Difficile
            new Carte(zytho,
                "Quel est le nom du composé chimique responsable de l'amertume dans la bière ?",
                new List<Reponse>
                {
                    new Reponse("Lupuline", true),
                    new Reponse("Maltose", false),
                    new Reponse("Dextrine", false),
                    new Reponse("Isomérisation", false)
                },
                Difficulte.Difficile),

            new Carte(zytho, "Quel type de bière est traditionnellement brassé avec des fruits ?",
                new List<Reponse>
                {
                    new Reponse("Lambic", true),
                    new Reponse("Pilsner", false),
                    new Reponse("Stout", false),
                    new Reponse("Lager", false)
                },
                Difficulte.Difficile),

            new Carte(zytho,
                "Quel est le nom du processus de vieillissement de la bière en fût de chêne ?",
                new List<Reponse>
                {
                    new Reponse("Vieillissement en fût", true),
                    new Reponse("Carbonatation", false),
                    new Reponse("Pasteurisation", false),
                    new Reponse("Clarification", false)
                },
                Difficulte.Difficile),

            new Carte(zytho, "Quel est le nom de la bière traditionnelle japonaise ?",
                new List<Reponse>
                {
                    new Reponse("Sake", true),
                    new Reponse("Asahi", false),
                    new Reponse("Kirin", false),
                    new Reponse("Sapporo", false)
                },
                Difficulte.Difficile),

            // Histoire - Facile
            new Carte(histoire, "Qui était le premier empereur romain ?",
                new List<Reponse>
                {
                    new Reponse("Auguste", true),
                    new Reponse("Jules César", false),
                    new Reponse("Néron", false),
                    new Reponse("Caligula", false)
                },
                Difficulte.Facile)
        };

        _plateau = new Plateau(_backgroundPlateau, cases);
        Joueur j1 = new Joueur("Lilian", _textureJoueurs[0], new Vector2(60, 240), cases[0]);
        Joueur j2 = new Joueur("Doriane", _textureJoueurs[1], new Vector2(80, 240), cases[0]);

        _joueurs.Add(j1);
        _joueurs.Add(j2);
        Console.WriteLine(j1.GetPosition());
        Console.WriteLine(j2.GetPosition());

        _partie = new Partie(_plateau, _joueurs, cartes);

    }

    protected override void Update(GameTime gameTime)
    {
        Joueur joueurAJouer = _partie.Joueurs[_partie.TourJoueur];

        switch (SceneActive)
        {
            case Scenes.SETUP:
                
                break;
            case Scenes.PLATEAU:
                UpdateInput();
                foreach (var joueur in _joueurs)
                {
                    if (joueur.Etat==EtatJoueur.ChoixDifficulte) 
                    {
                        SceneActive = Scenes.CHOIX_DIFFICULTE;
                    }
                }
                break;
            case Scenes.CHOIX_DIFFICULTE:
                List<ElementInteractif> difficultes = new List<ElementInteractif>
                {
                    new ElementInteractif(new Rectangle(200, 200, 200, 100), Color.White, "Facile"),
                    new ElementInteractif(new Rectangle(650, 200, 200, 100), Color.White, "Moyen"),
                    new ElementInteractif(new Rectangle(1100, 200, 200, 100), Color.White, "Difficile")
                };

                joueurAJouer.Update();
                // Check si le joueur est sure une case
                foreach (ElementInteractif difficulte in difficultes)
                {
                    if (joueurAJouer._Rect.Intersects(difficulte.Rectangle))
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Space))
                        {
                            // Convertit la valeur de l'enum en string et compare
                            if (Enum.TryParse<Difficulte>(difficulte.Texte, true, out Difficulte difficulteChoisie))
                            {
                                // Pioche une carte random
                                _partie.PiocherCarte(joueurAJouer.GetCase().Categorie, difficulteChoisie);

                                // Modifie l'état du joueur et de la scène
                                joueurAJouer.Etat=EtatJoueur.ChoixReponse;
                                SceneActive = Scenes.CHOIX_REPONSE;

                            }
                        }
                    }
                }
                
                break;
            
            case Scenes.CHOIX_REPONSE:
                _partie.Joueurs[_partie.TourJoueur].Update();
                
                List<ElementInteractif> elementReponses = new List<ElementInteractif>
                {
                    new ElementInteractif(new Rectangle(120, 200, 600, 200), Color.White, _partie.CartePiochee.Reponses[0].Texte),
                    new ElementInteractif(new Rectangle(820, 200, 600, 200), Color.White, _partie.CartePiochee.Reponses[1].Texte),
                    new ElementInteractif(new Rectangle(120, 500, 600, 200), Color.White, _partie.CartePiochee.Reponses[2].Texte),
                    new ElementInteractif(new Rectangle(820, 500, 600, 200), Color.White, _partie.CartePiochee.Reponses[3].Texte)
                };

                int numReponse = 0;
                foreach (ElementInteractif reponse in elementReponses)
                {
                    if (joueurAJouer._Rect.Intersects(reponse.Rectangle))
                    {
                        // Fait jouer la réponse au joueur
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        {
                            joueurAJouer.JouerReponse(_partie.CartePiochee.Reponses[numReponse]);
                            SceneActive = Scenes.TOUR_TERMINE;
                        }
                    }
                    numReponse ++;
                }
                break;
            
            case Scenes.TOUR_TERMINE:
                if (_partie.EstTerminee())
                {
                    Console.WriteLine("Fin de partie");
                    SceneActive = Scenes.FIN_PARTIE;
                }
                else
                {
                    SceneActive = Scenes.PLATEAU;
                    joueurAJouer.SetPositionSurCase();
                    _partie.TourJoueur = (_partie.TourJoueur + 1) % _joueurs.Count;
                }
                break;
            case Scenes.FIN_PARTIE:
                break;
        }
        base.Update(gameTime);
    }
    
    // Joue un tour lorsqu'un appuie sur espace en évitant le spam lors d'appuie prolongé
    private void UpdateInput()
    {
        KeyboardState nouvelEtat = Keyboard.GetState();
        if (nouvelEtat.IsKeyDown(Keys.Space))
        {
            if (!ancienEtat.IsKeyDown(Keys.Space))
            {
                _partie.JouerTour();
                Console.WriteLine("On joue un tour");
            }
        }
        ancienEtat = nouvelEtat;
    }

    protected override void Draw(GameTime gameTime)
    {
        Joueur joueurAJouer = _partie.Joueurs[_partie.TourJoueur];
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _spriteBatch.Begin();
        
        switch (SceneActive)
        {
            case Scenes.SETUP:
                
                break;
            case Scenes.PLATEAU:
                _plateau.Draw(_spriteBatch);
                _joueurs[0].Draw(_spriteBatch);
                _joueurs[1].Draw(_spriteBatch);
                // TODO: Add your update logic here

                break;
            case Scenes.CHOIX_DIFFICULTE:
                List<ElementInteractif> difficultes = new List<ElementInteractif>
                {
                    new ElementInteractif(new Rectangle(200, 200, 200, 100), Color.White, "Facile"),
                    new ElementInteractif(new Rectangle(650, 200, 200, 100), Color.White, "Moyen"),
                    new ElementInteractif(new Rectangle(1100, 200, 200, 100), Color.White, "Difficile")
                };

                foreach (ElementInteractif difficulte in difficultes)
                {
                    difficulte.Draw(_spriteBatch, _textureCase, _fontCase, Color.Black);
                    
                    // Vérifie si le joueur est au dessus d'un choix de difficulte
                    if (joueurAJouer._Rect.Intersects(difficulte.Rectangle))
                    {
                        // Place une indication au dessus du rectangle
                        Vector2 messagePosition = new Vector2(
                            difficulte.Rectangle.X + (difficulte.Rectangle.Width / 2) - (_fontCase.MeasureString("Appuyez sur ESPACE").X / 2),
                            difficulte.Rectangle.Y - 20
                        );
                        _spriteBatch.DrawString(_fontCase, "Appuyez sur ESPACE", messagePosition, Color.Black);
                    }
                }
                
                joueurAJouer.Draw(_spriteBatch);
                break;
            
            case Scenes.CHOIX_REPONSE:
                string question = _partie.CartePiochee.GetQuestion();
                
                // Positionne le texte de la question de manière centré
                Vector2 tailleTexte = _fontCase.MeasureString(question);
                float positionCentree = (1600 - tailleTexte.X) / 2;
                _spriteBatch.DrawString(_fontCase, question, new Vector2(positionCentree, 50), Color.Black);
                
                // Positionne les 4 réponses à la carte piochée
                List<ElementInteractif> elementReponses = new List<ElementInteractif>
                {
                    new ElementInteractif(new Rectangle(120, 200, 600, 200), Color.White, _partie.CartePiochee.Reponses[0].Texte),
                    new ElementInteractif(new Rectangle(820, 200, 600, 200), Color.White, _partie.CartePiochee.Reponses[1].Texte),
                    new ElementInteractif(new Rectangle(120, 500, 600, 200), Color.White, _partie.CartePiochee.Reponses[2].Texte),
                    new ElementInteractif(new Rectangle(820, 500, 600, 200), Color.White, _partie.CartePiochee.Reponses[3].Texte)
                };
                
                foreach (ElementInteractif reponse in elementReponses)
                {
                    reponse.Draw(_spriteBatch, _textureCase, _fontCase, Color.Black);
                    
                    // Vérifie si le joueur est au dessus d'un choix de réponse
                    if (joueurAJouer._Rect.Intersects(reponse.Rectangle))
                    {
                        // Place une indication au dessus du rectangle
                        Vector2 messagePosition = new Vector2(
                            reponse.Rectangle.X + (reponse.Rectangle.Width / 2) - (_fontCase.MeasureString("Appuyez sur ENTREE").X / 2),
                            reponse.Rectangle.Y - 20
                        );
                        _spriteBatch.DrawString(_fontCase, "Appuyez sur ENTREE", messagePosition, Color.Black);
                    }
                }
                
                joueurAJouer.Draw(_spriteBatch);
                break;
            
            case Scenes.FIN_PARTIE:
                break;
        }
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
