namespace Mensch_Aergere_Dich_Nicht
{
    internal class Spieler
    {
        public bool BotYesNo { get; private set; }
        public string Name { get; private set; }
        public static int NumberOfPlayers { get; private set; } = 0;
        public int Id { get; set; }

        public Spieler(string name, bool bot)
        {
            NumberOfPlayers++;
            Id = NumberOfPlayers;
            Name = name;
            BotYesNo = bot;
        }
    }
}
