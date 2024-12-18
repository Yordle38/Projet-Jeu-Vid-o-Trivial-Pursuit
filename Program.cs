using System;
using System.Collections.Generic;
using System.IO;
using TrivialPursuit.chap;

namespace TrivialPursuit.chap
{
    class Program
    {
        static void Main(string[] args)
        {

            Sprite sprite1 = new Sprite("a", 7, 10, 20,4,Enum.Couleur.BLEU);
            Sprite sprite2 = new Sprite("case1_texture.png", 13, 10, 20,4,Enum.Couleur.BLEU);
            Sprite sprite3 = new Sprite("case2_texture.png", 10, 15, 25,4,Enum.Couleur.BLEU);

            Case case1 = new Case(Enum.TYPECASE.VIDE, Enum.Couleur.BLEU, sprite1);
            Case case2 = new Case(Enum.TYPECASE.VIDE, Enum.Couleur.BLEU, sprite2);
            Case case3 = new Case(Enum.TYPECASE.QUESTION, Enum.Couleur.ROUGE, sprite3);

            Background background = new Background("plateau_texture.png");

            Cases c = new Cases(new List<Case>() { case1, case2, case3 });
            Plateau plateau = new Plateau(background, c);

            Reponse reponse1 = new Reponse(1, "Louis-Napoléon Bonaparte", true);
            Reponse reponse2 = new Reponse(2, "Gaston Doumergue", false);
            Reponses r = new Reponses(new List<Reponse>() { reponse1, reponse2 });
            Carte carte1 = new Carte(101, Enum.Difficulte.Facile, "Le premier président de la France ?", r);
            Cartes cartesList1 = new Cartes(new List<Carte>() { carte1 });
            Categorie categorie1 = new Categorie(1, "Histoire", Enum.Couleur.ROUGE, cartesList1);
            Categories categories = new Categories(new List<Categorie>() { categorie1 });
            Joker joker1 = new Joker(1, "50/50", "Supprimer deux mauvaises réponses");
            Jokers jokersList1 = new Jokers(new List<Joker>() { joker1 });

            Pion pion1 = new Pion("pion1", "pion1.png", "pion bleu", 1);
            Joueur joueur1 = new Joueur(1, "Bob", 4, pion1, jokersList1);
            Joueurs js = new Joueurs(new List<Joueur>() { joueur1 });
            string shema = "http://www.univ-grenoble-alpes.fr/l3miage/trivialpursuit ../xsd/game.xsd";
            TrivialPursuit t = new TrivialPursuit(plateau, categories, js,shema);

            // Serialisation 
            string filePath = Path.Combine("..", "..", "..", "xml", "trivialpursuit.xml");
            XMLUtils.Serialization(t, filePath);
            Console.WriteLine($"Fichier XML généré avec succès : {filePath}");

            // Désérialisation
            TrivialPursuit loadedGame = XMLUtils.Deserialization<TrivialPursuit>(filePath);
            Console.WriteLine("Fichier XML désérialisé avec succès !");
            
            string path= Path.Combine("..", "..", "..", "xml", "game.xml");
            Dom_Xpath xpath = new Dom_Xpath(path);
            int jo=xpath.CountElemnts("//tp:Joueurs//tp:Joueur");
            Console.WriteLine($"le nombre de joueur {jo}");
            xpath.CaseVide(path);
            int co=xpath.CountElemnts("//tp:Categories//tp:Categorie");
            Console.WriteLine($"le nombre de Categorie {co}");
            string pathC= Path.Combine("..", "..", "..", "xml", "questionDesCategories.xml"); 
            Dom_Xpath xpath1 = new Dom_Xpath(pathC);
            xpath1.AfficherQuestionsHistoire(pathC);
            
        }
    }
}
 



