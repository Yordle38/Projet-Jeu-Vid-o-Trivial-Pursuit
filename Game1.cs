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
            new Case(new Vector2(290, 200), 130, TypeCase.QUESTION, _textureCase,sport),
            new Case(new Vector2(420, 200), 130, TypeCase.CHANCE, _textureCase, jeuxVideo),
            new Case(new Vector2(550, 200), 130, TypeCase.VIDE, _textureCase),
            new Case(new Vector2(680, 200), 130, TypeCase.JOKER, _textureCase),
            new Case(new Vector2(810, 200), 130, TypeCase.QUESTION, _textureCase, musique),
            new Case(new Vector2(940, 200), 130, TypeCase.QUESTION, _textureCase, sport),
            new Case(new Vector2(1070, 200), 130, TypeCase.VIDE, _textureCase),

            // Colonne de droite
            new Case(new Vector2(1070, 265), 130, TypeCase.JOKER, _textureCase),
            new Case(new Vector2(1070, 330), 130, TypeCase.QUESTION, _textureCase, histoire),
            new Case(new Vector2(1070, 395), 130, TypeCase.CHANCE, _textureCase, zytho),
            new Case(new Vector2(1070, 460), 130, TypeCase.VIDE, _textureCase),
            new Case(new Vector2(1070, 525), 130, TypeCase.QUESTION, _textureCase, jeuxVideo),
            new Case(new Vector2(1070, 590), 130, TypeCase.JOKER, _textureCase),
            new Case(new Vector2(1070, 655), 130, TypeCase.QUESTION, _textureCase, nature),
            new Case(new Vector2(1070, 720), 130, TypeCase.QUESTION, _textureCase, musique),

            // Ligne du bas
            new Case(new Vector2(940, 720), 130, TypeCase.VIDE, _textureCase),
            new Case(new Vector2(810, 720), 130, TypeCase.QUESTION, _textureCase, histoire),
            new Case(new Vector2(680, 720), 130, TypeCase.CHANCE, _textureCase, nature),
            new Case(new Vector2(550, 720), 130, TypeCase.VIDE, _textureCase),
            new Case(new Vector2(420, 720), 130, TypeCase.QUESTION, _textureCase, zytho),
            new Case(new Vector2(290, 720), 130, TypeCase.JOKER, _textureCase),
            new Case(new Vector2(160, 720), 130, TypeCase.VIDE, _textureCase),
            new Case(new Vector2(30, 720), 130, TypeCase.QUESTION, _textureCase, jeuxVideo),

            // Colonne de gauche
            new Case(new Vector2(30, 655), 130, TypeCase.CHANCE, _textureCase, sport),
            new Case(new Vector2(30, 590), 130, TypeCase.VIDE, _textureCase),
            new Case(new Vector2(30, 525), 130, TypeCase.QUESTION, _textureCase, musique),
            new Case(new Vector2(30, 460), 130, TypeCase.JOKER, _textureCase),
            new Case(new Vector2(30, 395), 130, TypeCase.QUESTION, _textureCase, zytho),
            new Case(new Vector2(30, 330), 130, TypeCase.QUESTION, _textureCase, nature),
            new Case(new Vector2(30, 265), 130, TypeCase.QUESTION, _textureCase, histoire),
        };
        
        // CREATION DES CARTESS, A FAIRE AUTOMATIQUEMENT EN AVEC UNE FONCTION
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
                Difficulte.Facile),
            new Carte(histoire, 
    "En quelle année a eu lieu la Révolution française ?", 
    new List<Reponse> 
            {
                new Reponse("1789", true),
                new Reponse("1776", false),
                new Reponse("1848", false),
                new Reponse("1917", false)
            }, 
            Difficulte.Facile),

            new Carte(histoire, 
            "Qui a découvert l'Amérique en 1492 ?", 
                new List<Reponse> 
                {
                    new Reponse("Christophe Colomb", true),
                    new Reponse("Amerigo Vespucci", false),
                    new Reponse("Ferdinand Magellan", false),
                    new Reponse("Vasco de Gama", false)
                }, 
                Difficulte.Facile),

            new Carte(histoire, 
                "Quel était le nom du premier président des États-Unis ?", 
                new List<Reponse> 
                {
                    new Reponse("George Washington", true),
                    new Reponse("Thomas Jefferson", false),
                    new Reponse("John Adams", false),
                    new Reponse("James Madison", false)
                }, 
                Difficulte.Facile),

            new Carte(histoire, 
                "Quel traité a mis fin à la Première Guerre mondiale ?", 
                new List<Reponse> 
                {
                    new Reponse("Traité de Versailles", true),
                    new Reponse("Traité de Trianon", false),
                    new Reponse("Traité de Sèvres", false),
                    new Reponse("Traité de Saint-Germain", false)
                }, 
                Difficulte.Moyen),

            new Carte(histoire, 
                "Qui était le leader de la révolution cubaine en 1959 ?", 
                new List<Reponse> 
                {
                    new Reponse("Fidel Castro", true),
                    new Reponse("Che Guevara", false),
                    new Reponse("Raúl Castro", false),
                    new Reponse("Camilo Cienfuegos", false)
                }, 
                Difficulte.Moyen),

            new Carte(histoire, 
                "Quel était le nom du premier empereur de Chine ?", 
                new List<Reponse> 
                {
                    new Reponse("Qin Shi Huang", true),
                    new Reponse("Mao Zedong", false),
                    new Reponse("Sun Yat-sen", false),
                    new Reponse("Kangxi", false)
                }, 
                Difficulte.Moyen),

            new Carte(histoire, 
                "En quelle année a eu lieu la bataille de Waterloo ?", 
                new List<Reponse> 
                {
                    new Reponse("1815", true),
                    new Reponse("1805", false),
                    new Reponse("1812", false),
                    new Reponse("1820", false)
                }, 
                Difficulte.Moyen),

            new Carte(histoire, 
                "Quel était le nom du premier pharaon de l'Égypte unifiée ?", 
                new List<Reponse> 
                {
                    new Reponse("Narmer", true),
                    new Reponse("Khéops", false),
                    new Reponse("Ramsès II", false),
                    new Reponse("Akhenaton", false)
                }, 
                Difficulte.Difficile),

            new Carte(histoire, 
                "Quel traité a mis fin à la guerre de Cent Ans ?", 
                new List<Reponse> 
                {
                    new Reponse("Traité de Picquigny", true),
                    new Reponse("Traité de Troyes", false),
                    new Reponse("Traité de Brétigny", false),
                    new Reponse("Traité de Calais", false)
                }, 
                Difficulte.Difficile),

            new Carte(histoire, 
                "Qui était le leader de la révolte des Boxers en Chine ?", 
                new List<Reponse> 
                {
                    new Reponse("Cixi", true),
                    new Reponse("Sun Yat-sen", false),
                new Reponse("Mao Zedong", false),
                new Reponse("Chiang Kai-shek", false)
            }, 
            Difficulte.Difficile),

        new Carte(histoire, 
            "En quelle année a eu lieu la bataille de Poitiers ?", 
            new List<Reponse> 
            {
                new Reponse("732", true),
                new Reponse("711", false),
                new Reponse("756", false),
                new Reponse("778", false)
            }, 
            Difficulte.Difficile),
        // Questions sur la Musique
new Carte(musique, 
    "Qui est connu comme le \"Roi du Rock'n'Roll\" ?", 
    new List<Reponse> 
    {
        new Reponse("Elvis Presley", true),
        new Reponse("Michael Jackson", false),
        new Reponse("Freddie Mercury", false),
        new Reponse("Mick Jagger", false)
    }, 
    Difficulte.Facile),

new Carte(musique, 
    "Quel instrument de musique est souvent associé à Mozart ?", 
    new List<Reponse> 
    {
        new Reponse("Piano", true),
        new Reponse("Violon", false),
        new Reponse("Flûte", false),
        new Reponse("Guitare", false)
    }, 
    Difficulte.Facile),

new Carte(musique, 
    "Quel groupe est célèbre pour la chanson \"Bohemian Rhapsody\" ?", 
    new List<Reponse> 
    {
        new Reponse("Queen", true),
        new Reponse("The Beatles", false),
        new Reponse("The Rolling Stones", false),
        new Reponse("Pink Floyd", false)
    }, 
    Difficulte.Facile),

new Carte(musique, 
    "Quel est le nom du premier album des Beatles ?", 
    new List<Reponse> 
    {
        new Reponse("Please Please Me", true),
        new Reponse("Abbey Road", false),
        new Reponse("Sgt. Pepper's Lonely Hearts Club Band", false),
        new Reponse("The White Album", false)
    }, 
    Difficulte.Facile),

new Carte(musique, 
    "Quel compositeur a écrit \"Les Quatre Saisons\" ?", 
    new List<Reponse> 
    {
        new Reponse("Antonio Vivaldi", true),
        new Reponse("Johann Sebastian Bach", false),
        new Reponse("Ludwig van Beethoven", false),
        new Reponse("Wolfgang Amadeus Mozart", false)
    }, 
    Difficulte.Moyen),

new Carte(musique, 
    "Quel est le nom du premier album de Michael Jackson en solo ?", 
    new List<Reponse> 
    {
        new Reponse("Got to Be There", true),
        new Reponse("Off the Wall", false),
        new Reponse("Thriller", false),
        new Reponse("Bad", false)
    }, 
    Difficulte.Moyen),

new Carte(musique, 
    "Quel groupe est célèbre pour la chanson \"Smoke on the Water\" ?", 
    new List<Reponse> 
    {
        new Reponse("Deep Purple", true),
        new Reponse("Led Zeppelin", false),
        new Reponse("Black Sabbath", false),
        new Reponse("AC/DC", false)
    }, 
    Difficulte.Moyen),

new Carte(musique, 
    "Quel est le nom du premier album de Madonna ?", 
    new List<Reponse> 
    {
        new Reponse("Madonna", true),
        new Reponse("Like a Virgin", false),
        new Reponse("True Blue", false),
        new Reponse("Like a Prayer", false)
    }, 
    Difficulte.Moyen),

new Carte(musique, 
    "Quel compositeur a écrit l'opéra \"Carmen\" ?", 
    new List<Reponse> 
    {
        new Reponse("Georges Bizet", true),
        new Reponse("Giuseppe Verdi", false),
        new Reponse("Giacomo Puccini", false),
        new Reponse("Richard Wagner", false)
    }, 
    Difficulte.Difficile),

new Carte(musique, 
    "Quel est le nom du premier album de Bob Dylan ?", 
    new List<Reponse> 
    {
        new Reponse("Bob Dylan", true),
        new Reponse("The Freewheelin' Bob Dylan", false),
        new Reponse("Highway 61 Revisited", false),
        new Reponse("Blonde on Blonde", false)
    }, 
    Difficulte.Difficile),

new Carte(musique, 
    "Quel groupe est célèbre pour la chanson \"In-A-Gadda-Da-Vida\" ?", 
    new List<Reponse> 
    {
        new Reponse("Iron Butterfly", true),
        new Reponse("The Doors", false),
        new Reponse("The Jimi Hendrix Experience", false),
        new Reponse("Cream", false)
    }, 
    Difficulte.Difficile),

new Carte(musique, 
    "Quel est le nom du premier album de Prince ?", 
    new List<Reponse> 
    {
        new Reponse("For You", true),
        new Reponse("Prince", false),
        new Reponse("Dirty Mind", false),
        new Reponse("Controversy", false)
    }, 
    Difficulte.Difficile),
// Questions sur le Sport
new Carte(sport, 
    "Quel sport est joué à Wimbledon ?", 
    new List<Reponse> 
    {
        new Reponse("Tennis", true),
        new Reponse("Football", false),
        new Reponse("Basketball", false),
        new Reponse("Golf", false)
    }, 
    Difficulte.Facile),

new Carte(sport, 
    "Quel pays a remporté la Coupe du Monde de football en 2018 ?", 
    new List<Reponse> 
    {
        new Reponse("France", true),
        new Reponse("Allemagne", false),
        new Reponse("Brésil", false),
        new Reponse("Espagne", false)
    }, 
    Difficulte.Facile),

new Carte(sport, 
    "Quel est le nom du célèbre joueur de basketball connu sous le surnom de \"Air Jordan\" ?", 
    new List<Reponse> 
    {
        new Reponse("Michael Jordan", true),
        new Reponse("Kobe Bryant", false),
        new Reponse("LeBron James", false),
        new Reponse("Magic Johnson", false)
    }, 
    Difficulte.Facile),

new Carte(sport, 
    "Quel sport est pratiqué lors du Tour de France ?", 
    new List<Reponse> 
    {
        new Reponse("Cyclisme", true),
        new Reponse("Athlétisme", false),
        new Reponse("Natation", false),
        new Reponse("Équitation", false)
    }, 
    Difficulte.Facile),

new Carte(sport, 
    "Quel pays a accueilli les Jeux Olympiques d'été en 2016 ?", 
    new List<Reponse> 
    {
        new Reponse("Brésil", true),
        new Reponse("Japon", false),
        new Reponse("Chine", false),
        new Reponse("Russie", false)
    }, 
    Difficulte.Moyen),

new Carte(sport, 
    "Quel joueur de football est connu sous le surnom de \"El Fenómeno\" ?", 
    new List<Reponse> 
    {
        new Reponse("Ronaldo", true),
        new Reponse("Pelé", false),
        new Reponse("Lionel Messi", false),
        new Reponse("Cristiano Ronaldo", false)
    }, 
    Difficulte.Moyen),

new Carte(sport, 
    "Quel sport est pratiqué lors de la Ryder Cup ?", 
    new List<Reponse> 
    {
        new Reponse("Golf", true),
        new Reponse("Rugby", false),
        new Reponse("Cricket", false),
        new Reponse("Hockey", false)
    }, 
    Difficulte.Moyen),

new Carte(sport, 
    "Quel est le nom du célèbre joueur de baseball connu sous le surnom de \"The Babe\" ?", 
    new List<Reponse> 
    {
        new Reponse("Babe Ruth", true),
        new Reponse("Mickey Mantle", false),
        new Reponse("Hank Aaron", false),
        new Reponse("Willie Mays", false)
    }, 
    Difficulte.Moyen),

new Carte(sport, 
    "Quel pays a remporté la Coupe du Monde de rugby en 2019 ?", 
    new List<Reponse> 
    {
        new Reponse("Afrique du Sud", true),
        new Reponse("Nouvelle-Zélande", false),
        new Reponse("Australie", false),
        new Reponse("Angleterre", false)
    }, 
    Difficulte.Difficile),

new Carte(sport, 
    "Quel est le nom du célèbre joueur de hockey sur glace connu sous le surnom de \"The Great One\" ?", 
    new List<Reponse> 
    {
        new Reponse("Wayne Gretzky", true),
        new Reponse("Mario Lemieux", false),
        new Reponse("Sidney Crosby", false),
        new Reponse("Alexander Ovechkin", false)
    }, 
    Difficulte.Difficile),

new Carte(sport, 
    "Quel sport est pratiqué lors de la Coupe Davis ?", 
    new List<Reponse> 
    {
        new Reponse("Tennis", true),
        new Reponse("Badminton", false),
        new Reponse("Squash", false),
        new Reponse("Tennis de table", false)
    }, 
    Difficulte.Difficile),

new Carte(sport, 
    "Quel est le nom du célèbre joueur de football américain connu sous le surnom de \"Broadway Joe\" ?", 
    new List<Reponse> 
    {
        new Reponse("Joe Namath", true),
        new Reponse("Tom Brady", false),
        new Reponse("Peyton Manning", false),
        new Reponse("Brett Favre", false)
    }, 
    Difficulte.Difficile),
// Questions sur les Jeux Vidéo
new Carte(jeuxVideo, 
    "Quel est le nom du plombier célèbre de Nintendo ?", 
    new List<Reponse> 
    {
        new Reponse("Mario", true),
        new Reponse("Luigi", false),
        new Reponse("Sonic", false),
        new Reponse("Link", false)
    }, 
    Difficulte.Facile),

new Carte(jeuxVideo, 
    "Quel jeu vidéo met en scène un hérisson bleu ?", 
    new List<Reponse> 
    {
        new Reponse("Sonic the Hedgehog", true),
        new Reponse("Super Mario Bros.", false),
        new Reponse("The Legend of Zelda", false),
        new Reponse("Pac-Man", false)
    }, 
    Difficulte.Facile),

new Carte(jeuxVideo, 
    "Quel est le nom de la console de jeu portable de Nintendo sortie en 1989 ?", 
    new List<Reponse> 
    {
        new Reponse("Game Boy", true),
        new Reponse("GameCube", false),
        new Reponse("Nintendo 64", false),
        new Reponse("Wii", false)
    }, 
    Difficulte.Facile),

new Carte(jeuxVideo, 
    "Quel jeu vidéo met en scène un personnage qui doit sauver la Terre des envahisseurs extraterrestres ?", 
    new List<Reponse> 
    {
        new Reponse("Space Invaders", true),
        new Reponse("Pac-Man", false),
        new Reponse("Tetris", false),
        new Reponse("Pong", false)
    }, 
    Difficulte.Facile),

new Carte(jeuxVideo, 
    "Quel est le nom du premier jeu de la série \"The Legend of Zelda\" ?", 
    new List<Reponse> 
    {
        new Reponse("The Legend of Zelda", true),
        new Reponse("Ocarina of Time", false),
        new Reponse("Link's Awakening", false),
        new Reponse("Breath of the Wild", false)
    }, 
    Difficulte.Moyen),

new Carte(jeuxVideo, 
    "Quel jeu vidéo met en scène un archéologue aventurier ?", 
    new List<Reponse> 
    {
        new Reponse("Tomb Raider", true),
        new Reponse("Uncharted", false),
        new Reponse("Indiana Jones", false),
        new Reponse("Assassin's Creed", false)
    }, 
    Difficulte.Moyen),

new Carte(jeuxVideo, 
    "Quel est le nom de la console de jeu de Microsoft sortie en 2001 ?", 
    new List<Reponse> 
    {
        new Reponse("Xbox", true),
        new Reponse("PlayStation", false),
        new Reponse("Nintendo Switch", false),
        new Reponse("Sega Dreamcast", false)
    }, 
    Difficulte.Moyen),

new Carte(jeuxVideo, 
    "Quel jeu vidéo met en scène un agent secret britannique ?", 
    new List<Reponse> 
    {
        new Reponse("GoldenEye 007", true),
        new Reponse("Metal Gear Solid", false),
        new Reponse("Splinter Cell", false),
        new Reponse("Hitman", false)
    }, 
    Difficulte.Moyen),

new Carte(jeuxVideo, 
    "Quel est le nom du premier jeu de la série \"Final Fantasy\" ?", 
    new List<Reponse> 
    {
        new Reponse("Final Fantasy", true),
        new Reponse("Final Fantasy VII", false),
        new Reponse("Final Fantasy X", false),
        new Reponse("Final Fantasy XV", false)
    }, 
    Difficulte.Difficile),

new Carte(jeuxVideo, 
    "Quel jeu vidéo met en scène un chasseur de monstres géants ?", 
    new List<Reponse> 
    {
        new Reponse("Monster Hunter", true),
        new Reponse("God of War", false),
        new Reponse("Shadow of the Colossus", false),
        new Reponse("Horizon Zero Dawn", false)
    }, 
    Difficulte.Difficile),

new Carte(jeuxVideo, 
    "Quel est le nom de la console de jeu de Sega sortie en 1988 ?", 
    new List<Reponse> 
    {
        new Reponse("Sega Mega Drive", true),
        new Reponse("Sega Saturn", false),
        new Reponse("Sega Dreamcast", false),
        new Reponse("Sega Master System", false)
    }, 
    Difficulte.Difficile),

new Carte(jeuxVideo, 
    "Quel jeu vidéo met en scène un chevalier en armure bleue ?", 
    new List<Reponse> 
    {
        new Reponse("Mega Man", true),
        new Reponse("Metroid", false),
        new Reponse("Castlevania", false),
        new Reponse("Contra", false)
    }, 
    Difficulte.Difficile),
new Carte(nature, 
    "Quel est le plus grand mammifère terrestre ?", 
    new List<Reponse> 
    {
        new Reponse("Éléphant", true),
        new Reponse("Rhinocéros", false),
        new Reponse("Hippopotame", false),
        new Reponse("Girafe", false)
    }, 
    Difficulte.Facile),

new Carte(nature, 
    "Quel est le plus grand océan du monde ?", 
    new List<Reponse> 
    {
        new Reponse("Océan Pacifique", true),
        new Reponse("Océan Atlantique", false),
        new Reponse("Océan Indien", false),
        new Reponse("Océan Arctique", false)
    }, 
    Difficulte.Facile),

new Carte(nature, 
    "Quel est le plus haut sommet du monde ?", 
    new List<Reponse> 
    {
        new Reponse("Mont Everest", true),
        new Reponse("K2", false),
        new Reponse("Kangchenjunga", false),
        new Reponse("Lhotse", false)
    }, 
    Difficulte.Facile),

new Carte(nature, 
    "Quel est le plus long fleuve du monde ?", 
    new List<Reponse> 
    {
        new Reponse("Nil", true),
        new Reponse("Amazone", false),
        new Reponse("Yangtsé", false),
        new Reponse("Mississippi", false)
    }, 
    Difficulte.Facile),

new Carte(nature, 
    "Quel est le nom de l'arbre le plus ancien du monde ?", 
    new List<Reponse> 
    {
        new Reponse("Methuselah", true),
        new Reponse("General Sherman", false),
        new Reponse("Hyperion", false),
        new Reponse("The President", false)
    }, 
    Difficulte.Moyen),

new Carte(nature, 
    "Quel est le nom du plus grand récif corallien du monde ?", 
    new List<Reponse> 
    {
        new Reponse("Grande Barrière de Corail", true),
        new Reponse("Récif de Tubbataha", false),
        new Reponse("Récif de la Mer Rouge", false),
        new Reponse("Récif de Belize", false)
    }, 
    Difficulte.Moyen),

new Carte(nature, 
    "Quel est le nom de la plus grande forêt tropicale du monde ?", 
    new List<Reponse> 
    {
        new Reponse("Forêt Amazonienne", true),
        new Reponse("Forêt du Congo", false),
        new Reponse("Forêt de Bornéo", false),
        new Reponse("Forêt de Sumatra", false)
    }, 
    Difficulte.Moyen),

new Carte(nature, 
    "Quel est le nom du plus grand désert du monde ?", 
    new List<Reponse> 
    {
        new Reponse("Désert de l'Antarctique", true),
        new Reponse("Sahara", false),
        new Reponse("Désert de Gobi", false),
        new Reponse("Désert d'Arabie", false)
    }, 
    Difficulte.Moyen),

new Carte(nature, 
    "Quel est le nom du plus grand volcan du monde ?", 
    new List<Reponse> 
    {
        new Reponse("Mauna Loa", true),
        new Reponse("Kilauea", false),
        new Reponse("Mont Fuji", false),
        new Reponse("Vésuve", false)
    }, 
    Difficulte.Difficile),

new Carte(nature, 
    "Quel est le nom de la plus grande cascade du monde ?", 
    new List<Reponse> 
    {
        new Reponse("Chutes d'Iguazu", true),
        new Reponse("Chutes Victoria", false),
        new Reponse("Chutes du Niagara", false),
        new Reponse("Chutes d'Angel", false)
    }, 
    Difficulte.Difficile),

new Carte(nature, 
    "Quel est le nom du plus grand lac du monde ?", 
    new List<Reponse> 
    {
        new Reponse("Lac Baïkal", true),
        new Reponse("Lac Supérieur", false),
        new Reponse("Lac Victoria", false),
        new Reponse("Lac Huron", false)
    }, 
    Difficulte.Difficile),

new Carte(nature, 
    "Quel est le nom de la plus grande île du monde ?", 
    new List<Reponse> 
    {
        new Reponse("Groenland", true),
        new Reponse("Nouvelle-Guinée", false),
        new Reponse("Bornéo", false),
        new Reponse("Madagascar", false)
    }, 
    Difficulte.Difficile)
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
                    
                    // Si le joueur a répondu juste à une case chance, il rejoue, sinon c'est au joueur suivant
                    if (joueurAJouer.Etat != EtatJoueur.Rejouer)
                    {
                        _partie.TourJoueur = (_partie.TourJoueur + 1) % _joueurs.Count;
                    }
                    else
                    {
                        joueurAJouer.Etat = EtatJoueur.Normal;
                    }
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
        Joueur joueurAJouer = _partie.Joueurs[_partie.TourJoueur];

        KeyboardState nouvelEtat = Keyboard.GetState();
        if (nouvelEtat.IsKeyDown(Keys.Space))
        {
            if (!ancienEtat.IsKeyDown(Keys.Space))
            {
                // _partie.JouerTour();
                // Console.WriteLine("On joue un tour");
                
                if (joueurAJouer.Etat == EtatJoueur.Normal)
                {
                    Console.WriteLine("Le joueur se déplace");
                    _partie.JouerTour();
                }

                else if (joueurAJouer.Etat == EtatJoueur.AttenteConfirmation)
                {
                    joueurAJouer.ActiverChoixDifficulte();
                    Console.WriteLine("Le joueur va jouer son tour.");
                }
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

                string categorie = joueurAJouer.GetCase().Categorie.GetNom();
                Vector2 tailleTexteDiff = _fontCase.MeasureString("Choix d'une difficulté pour " + categorie);
                float positionCentreeDiff = (1600 - tailleTexteDiff.X) / 2;
                _spriteBatch.DrawString(_fontCase, "Choix d'une difficulté pour " + categorie, new Vector2(positionCentreeDiff, 50), Color.Black);

                
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
