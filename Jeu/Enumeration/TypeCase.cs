namespace Trivial_Pursuit.Jeu.Enumeration;

public class TypeCase
{
    public string Effet { get; }

    private TypeCase(string effet)
    {
        Effet = effet;
    }
    
    public static TypeCase VIDE = new TypeCase("");
    public static TypeCase QUESTION = new TypeCase("Question classique de la catégorie liée à sa couleur");
    public static TypeCase JOKER = new TypeCase("Permet au joueur d'obtenir soit un 50/50 soit changement question");
    public static TypeCase CHANCE = new TypeCase("Le joueur joue une fois de plus");
}