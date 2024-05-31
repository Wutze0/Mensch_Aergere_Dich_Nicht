namespace Mensch_Aergere_Dich_Nicht
{
    internal class UserFalscheEingabeException : Exception
    {
        public UserFalscheEingabeException(string message) : base(message) { }
    }
}
