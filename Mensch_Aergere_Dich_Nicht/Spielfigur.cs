namespace Mensch_Aergere_Dich_Nicht
{
    internal class Spielfigur
    {
        public int _id { get; set; }
        private string _farbe;
        private double _position;
        public double PrintPosition { get; set; } = 0;

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
                case "Rot": Console.ForegroundColor = ConsoleColor.Red; Console.Write($"P{_id}"); break;
                case "Gruen": Console.ForegroundColor = ConsoleColor.Green; Console.Write($"P{_id}"); break;
                case "Blau": Console.ForegroundColor = ConsoleColor.Blue; Console.Write($"P{_id}"); break;
                case "Gelb": Console.ForegroundColor = ConsoleColor.Yellow; Console.Write($"P{_id}"); break;
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
