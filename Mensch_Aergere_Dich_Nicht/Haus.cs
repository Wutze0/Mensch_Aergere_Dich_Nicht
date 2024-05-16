namespace Mensch_Aergere_Dich_Nicht
{
    internal class Haus
    {
        private int _anzSpielfiguren;
        private string _farbe;
        public int FigurenImHaus { get; set; } = 4;
        public int ZiehbareFiguren { get; set; } = 4;
        public int _aktuellLetztesFeld { get; set; } = 44;

        public int HausID { get; private set; }
        public static int _numberOfHouses { get; private set; } = 0;

        public int StartingPrintPosition {  get; set; }
        public List<Spielfigur> ZugehoerigeFiguren { get; set; } = new List<Spielfigur>();

        
        public Haus(Verfuegbare_Farben farbe)
        {
            _farbe = farbe.ToString();
            switch (_numberOfHouses)
            {
                case 0: StartingPrintPosition = 33; break;
                case 1: StartingPrintPosition = 3; break;
                case 2: StartingPrintPosition = 13; break;
                case 3: StartingPrintPosition = 23;break;
            }
            _numberOfHouses++;
            HausID = _numberOfHouses;
            _anzSpielfiguren = 4;
            for (int i = 1; i <= 4; i++)
            {
                ZugehoerigeFiguren.Add(new Spielfigur(i, farbe.ToString(), 100));
            }

        }

        public void changeColour()
        {

            switch (_farbe)
            {
                case "Rot": Console.ForegroundColor = ConsoleColor.Red; break;
                case "Gruen": Console.ForegroundColor = ConsoleColor.Green; break;
                case "Blau": Console.ForegroundColor = ConsoleColor.Blue; break;
                case "Gelb": Console.ForegroundColor = ConsoleColor.Yellow; break;
                case "Tuerkis": Console.ForegroundColor = ConsoleColor.Cyan; break;
                case "Dunkelrot": Console.ForegroundColor = ConsoleColor.DarkRed; break;
                case "Dunkelgruen": Console.ForegroundColor = ConsoleColor.DarkGreen; break;
                case "Magenta": Console.ForegroundColor = ConsoleColor.Magenta; break;
            }


        }

        

    }

}
