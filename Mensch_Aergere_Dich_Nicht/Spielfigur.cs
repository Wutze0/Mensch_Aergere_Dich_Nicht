namespace Mensch_Aergere_Dich_Nicht
{
    internal class Spielfigur
    {
        public int _id { get; set; }
        private string _farbe;
        public double _position { get; set; }
        public double PrintPosition { get; set; } = 0;
        public bool IsInHouse { get; set; } = true;

        public Spielfigur(int id, string farbe, double position)
        {
            _id = id;
            _farbe = farbe;
            _position = position;
        }

        public void PrintFigur()
        {
            switch (_farbe)
            {
                case "Rot": Console.ForegroundColor = ConsoleColor.Red; Console.Write($"F{_id}"); break;
                case "Gruen": Console.ForegroundColor = ConsoleColor.Green; Console.Write($"F{_id}"); break;
                case "Blau": Console.ForegroundColor = ConsoleColor.Blue; Console.Write($"F{_id}"); break;
                case "Gelb": Console.ForegroundColor = ConsoleColor.Yellow; Console.Write($"F{_id}"); break;
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
