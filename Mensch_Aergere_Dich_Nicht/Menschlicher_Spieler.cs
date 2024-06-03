namespace Mensch_Aergere_Dich_Nicht
{
    internal class Menschlicher_Spieler : Spieler
    {
        public int Siege {get; set; }
        public Menschlicher_Spieler(string name) : base(name, false)
        {

        }
    }
}
