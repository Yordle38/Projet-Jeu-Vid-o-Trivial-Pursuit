namespace Trivial_Pursuit.Jeu.Entites;

public class Joker
{
    private string _nom;
    private string _effet;

    public Joker(string nom, string effet)
    {
        _nom = nom;
        _effet = effet;
    }

    public string getNom()
    {
        return _nom;
    }

    public string getEffet()
    {
        return _effet;
    }
    
}