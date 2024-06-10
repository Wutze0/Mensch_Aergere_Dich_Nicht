namespace Mensch_Aergere_Dich_Nicht
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Spiellogik start = new Spiellogik();
            Speicherrung speicherrung = new Speicherrung();
            start.Speicherung = speicherrung;
            speicherrung.Logik = start;
            start.Einführung();

        }
    }

}


