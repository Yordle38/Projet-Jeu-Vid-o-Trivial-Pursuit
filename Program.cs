/*﻿using var game = new Trivial_Pursuit.Game1();
game.Run();*/

using System;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.IO;

namespace TrivialPursuit
{
    class Program
    {
        static void Main(string[] args)
        {
            var trivialPursuit = new TrivialPursuit
            {
                Plateau = new Plateau
                {
                    Background = new Background { Texture = "Background.png" },
                    Cases = new List<Case>
                    {
                        new Case
                        {
                            Type = Enum.TYPECASE.QUESTION,
                            Couleur = Enum.Couleur.BLEU,
                            Sprite = new Sprite { Type = "Sprite.png", PositionX = 15, PositionY = 10, Size = 20 },
                            Joker = new Joker { Id = 1, Nom = "50/50", Effet = "50/50" }
                        },
                        new Case
                        {
                            Type = Enum.TYPECASE.CHALLENGE,
                            Couleur = Enum.Couleur.ROUGE,
                            Sprite = new Sprite { Type = "Sprite.png", PositionX = 10, PositionY = 15, Size = 20 },
                            Joker = new Joker { Id = 1, Nom = "Relance question", Effet = "Relance question" }
                        }
                    }
                },
                Categories = new List<Categorie>
                {
                    new Categorie
                    {
                        Id = 1,
                        Nom = "Histoire",
                        Couleur = Enum.Couleur.JAUNE,
                        Cartes = new List<Carte>
                        {
                            new Carte
                            {
                                Id = 1,
                                Difficulte = Enum.Difficulte.Facile,
                                Question = "Qui a découvert l'Amérique en 1492 ?",
                                Responses = new List<Reponse>
                                {
                                    new Reponse { Id = 1, Texte = "Christophe Colomb", Correct = true },
                                    new Reponse { Id = 2, Texte = "Amerigo Vespucci", Correct = false },
                                    new Reponse { Id = 3, Texte = "Ferdinand Magellan", Correct = false },
                                    new Reponse { Id = 4, Texte = "Vasco de Gama", Correct = false },
                                }
                            }
                        }
                    }
                },
                Joueurs = new List<Joueur>
                {
                    new Joueur
                    {
                        Id = 1,
                        Nom = "Joueur 1",
                        Score = 10,
                        Pion = new Pion
                            { Nom = "Pion 1", ImageLink = "Pion 1.png", Description = "pion bleu", Case = "Case1" },
                        Jokers = new List<Joker>
                        {
                            new Joker { Id = 1, Nom = "50/50", Effet = "50/50" }
                        }
                    },
                    new Joueur
                    {
                        Id = 2,
                        Nom = "Joueur 2",
                        Score = 5,
                        Pion = new Pion
                        {
                            Nom = "Pion 2", ImageLink = "Pion 2.png", Description = "pion rouge", Case = "Case1"
                        },
                        Jokers = new List<Joker>
                        {
                            new Joker { Id = 2, Nom = "relance question", Effet = "relance question" }
                        }
                    }
                }
            };
            string filePath = @"..\..\xml\trivialpursuit.xml";
            XMLUtils.Serialization(trivialPursuit, filePath);
            Console.WriteLine($"Serialization reussie dans le fichier xml : {filePath}");
            
            
        }
    }

    public static class XMLUtils
    {
        public static void Serialization<T>(T obj, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (var writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, obj);
            }
        }
    }
}

