namespace Trivial_Pursuit.Jeu.Entites;

public class Reponse
{
    public string Texte { get; private set; }
    public bool EstCorrecte { get; private set; }
    public Reponse(string texte, bool correct)
    {
        Texte = texte;
        EstCorrecte = correct;
    }
}
