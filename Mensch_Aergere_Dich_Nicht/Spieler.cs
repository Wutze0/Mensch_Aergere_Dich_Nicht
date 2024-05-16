namespace Mensch_Aergere_Dich_Nicht
{
    internal class Spieler
    {
        private double _anzSiege = 0;
        public string Name { get; private set; }
        public int Id { get; set; }

        public Spieler(string name, int id)
        {
            Name = name;
            Id = id;
        }
    }
}
