namespace Mensch_Aergere_Dich_Nicht
{
    internal class Menschlicher_Spieler : Spieler
    {
        public int Id { get; set; }



        public Menschlicher_Spieler(string name, int id) : base(name, false)
        {
            Id = id;
        }
    }
}
