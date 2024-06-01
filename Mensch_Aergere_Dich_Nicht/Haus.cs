namespace Mensch_Aergere_Dich_Nicht
{
    internal class Haus
    {
        private int _anzSpielfiguren;
        public string Farbe { get; private set; }
        public Verfuegbare_Farben Farbe2 { get; private set; }
        public int FigurenImHaus { get; set; } = 4;
        public int ZiehbareFiguren { get; set; } = 4;
        public int AktuellLetztesFelde { get; set; } = 44;

        public int HausID { get; private set; }                         //Die HausID dient hauptsächlich dazu, die Printpositionen innerhalb des Hauses zu bestimmen, geht von 1 - 4
        public static int NumberOfHouses { get; private set; } = 0;

        public bool AuffuellHaus { get; set; } = false;
        public int StartingPrintPosition { get; set; }
        public List<Spielfigur> ZugehoerigeFiguren { get; set; } = new List<Spielfigur>();

        public Spieler? ZugehoerigerSpieler { get; set; }


        public Haus(Verfuegbare_Farben farbe)
        {
            Farbe = farbe.ToString();
            switch (NumberOfHouses)
            {
                case 0: StartingPrintPosition = 33; break;
                case 1: StartingPrintPosition = 3; break;
                case 2: StartingPrintPosition = 23; break;
                case 3: StartingPrintPosition = 13; break;
            }
            NumberOfHouses++;
            HausID = NumberOfHouses;
            _anzSpielfiguren = 4;
            for (int i = 1; i <= 4; i++)
            {
                ZugehoerigeFiguren.Add(new Spielfigur(i, farbe.ToString(), 0));
            }

        }

        public void ChangeColour()
        {

            switch (Farbe)
            {
                case "Rot": Console.ForegroundColor = ConsoleColor.Red; break;
                case "Gruen": Console.ForegroundColor = ConsoleColor.Green; break;
                case "Blau": Console.ForegroundColor = ConsoleColor.Blue; break;
                case "Gelb": Console.ForegroundColor = ConsoleColor.Yellow; break;
                case "Tuerkis": Console.ForegroundColor = ConsoleColor.Cyan; break;
                case "Dunkelrot": Console.ForegroundColor = ConsoleColor.DarkRed; break;
                case "Dunkelgruen": Console.ForegroundColor = ConsoleColor.DarkGreen; break;
                case "Magenta": Console.ForegroundColor = ConsoleColor.Magenta; break;
                case "Weiss": Console.ForegroundColor = ConsoleColor.White; break;

            }


        }
        public int LetztesBefahrbaresFeldBerechnen()
        {
            int letztesBefahrbaresFeld = 44;
            List<int> positionen = new List<int>();

            foreach (Spielfigur s in ZugehoerigeFiguren)
            {
                positionen.Add(s.Position);
            }


            for (int i = 0; i < 4; i++)
            {
                if (positionen.Contains(44 - i))
                {
                    letztesBefahrbaresFeld--;
                }
                else
                {
                    i = 4;
                }
            }

            return letztesBefahrbaresFeld;
        }

        public int LetztesMoeglichesFeldBeimReinfahrenberechnen()
        {
            int x = 45;

            foreach (Spielfigur s in ZugehoerigeFiguren)
            {
                if (s.Position < x && s.Position > 40)
                {
                    x = s.Position;
                }
            }
            x -= 1;
            return x;
        }

        public void ZiehbareFigurenBerechnen()
        {
            int x = 4;
            bool again = false;
            for (int i = 0; i < 4; i++)
            {
                foreach (Spielfigur s in ZugehoerigeFiguren)
                {
                    if (s.Position == 44 - i)
                    {
                        x--;
                        again = true;
                    }
                }
                if (again)
                {
                    again = false;
                }
                else
                {
                    i = 4;
                }

            }
            ZiehbareFiguren = x;

        }

        public bool NichtsBewegbar(int wieWeitZiehen)
        {
            int groesstesBefahrbaresfeld;
            int maximalZiehbareAnz;
            int i = 0;
            foreach (Spielfigur s in ZugehoerigeFiguren)
            {
                if (s.Position > 40)
                {
                    groesstesBefahrbaresfeld = 44;
                    foreach (Spielfigur s2 in ZugehoerigeFiguren)
                    {
                        if (s2.Position > s.Position && s2.Position < groesstesBefahrbaresfeld)
                        {
                            groesstesBefahrbaresfeld = s2.Position;
                        }
                    }
                    maximalZiehbareAnz = groesstesBefahrbaresfeld - s.Position;
                    if (maximalZiehbareAnz >= wieWeitZiehen)
                    {

                    }
                    else
                    {
                        i++;
                    }
                }
                else if (s.Position == 0 && wieWeitZiehen != 6)
                {
                    i++;
                }
            }
            if (i == 4)
            {
                return true;
            }
            else { return false; }
        }
    }

}
