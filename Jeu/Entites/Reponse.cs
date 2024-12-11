namespace Trivial_Pursuit.Jeu.Entites;

public class Reponse
{
    public string Texte { get; private set; }
    private bool _correct;

    public Reponse(string texte, bool correct)
    {
        Texte = texte;
        _correct = correct;
    }
}