namespace Mensch_Aergere_Dich_Nicht
{
    internal class AngemeldeterSpieler : Menschlicher_Spieler
    {
        string _passwort;

        public AngemeldeterSpieler(string name, string passwort) : base(name)
        {
            _passwort = passwort;
        }

        public AngemeldeterSpieler(string name) : base(name)
        {
        }
    }
}
