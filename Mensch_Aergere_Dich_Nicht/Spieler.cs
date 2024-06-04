namespace Mensch_Aergere_Dich_Nicht
{
    internal class Spieler
    {
        public bool BotYesNo { get; private set; }              //Wenn true ist der Spieler ein Bot
        public string Name { get; private set; }
        public static int NumberOfPlayers { get; private set; } = 0;    //Dient dazu, einem Spieler eine ID zu geben
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
