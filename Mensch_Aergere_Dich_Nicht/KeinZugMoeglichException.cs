namespace Mensch_Aergere_Dich_Nicht
{
    internal class KeinZugMoeglichException : Exception
    {
        public KeinZugMoeglichException(string message) : base(message) { }
    }
}
