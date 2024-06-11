namespace Mensch_Aergere_Dich_Nicht
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Spiellogik start = new Spiellogik();
            Speicherung speicherrung = new Speicherung();
            start.Speicherungsmodul = speicherrung;
            speicherrung.Logik = start;
            start.Einführung();

        }
    }

}


