namespace Mensch_Aergere_Dich_Nicht
{
    internal class Spielfigur
    {
        public int _id { get; set; }
        public string _farbe { get; private set; }
        public int Position { get; set; } = 0;
        public int PrintPosition { get; set; } = 0;      //Printposition standardmäßig 0
        public bool IsInHouse { get; set; } = true;

        public Spielfigur(int id, string farbe, int position)
        {
            _id = id;
            _farbe = farbe;
            Position = position;
        }

        public void PrintFigur()
        {
            switch (_farbe)
            {
                case "Rot": Console.ForegroundColor = ConsoleColor.Red; Console.Write($"F{_id}"); break;
                case "Gruen": Console.ForegroundColor = ConsoleColor.Green; Console.Write($"F{_id}"); break;
                case "Blau": Console.ForegroundColor = ConsoleColor.Blue; Console.Write($"F{_id}"); break;
                case "Gelb": Console.ForegroundColor = ConsoleColor.Yellow; Console.Write($"F{_id}"); break;
                case "Tuerkis": Console.ForegroundColor = ConsoleColor.Cyan; Console.Write($"F{_id}"); break;
                case "Dunkelrot": Console.ForegroundColor = ConsoleColor.DarkRed; Console.Write($"F{_id}"); break;
                case "Dunkelgruen": Console.ForegroundColor = ConsoleColor.DarkGreen; Console.Write($"F{_id}"); break;
                case "Magenta": Console.ForegroundColor = ConsoleColor.Magenta; Console.Write($"F{_id}"); break;

            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
