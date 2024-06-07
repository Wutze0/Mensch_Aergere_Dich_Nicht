namespace Mensch_Aergere_Dich_Nicht
{
    internal class Haus
    {
        public string Farbe { get; private set; }
        public Verfuegbare_Farben Farbe2 { get; private set; }
        public int FigurenImHaus { get; set; } = 4;                     //Wie viele Figuren noch am Start sind
        public int ZiehbareFiguren { get; set; } = 4;                   //Wie viele Figuren theoretisch noch bewegt werden können

        public int HausID { get; private set; }                         //Die HausID dient hauptsächlich dazu, die Printpositionen innerhalb des Hauses zu bestimmen, geht von 1 - 4
        public static int NumberOfHouses { get; set; } = 0;     //Statische Variable um den Häusern IDs zu geben

        public bool AuffuellHaus { get; set; } = false;                 //Speichert, ob das Haus einen zugehörigen Spieler hat oder nicht. Dient dazu, dass die Ausgabe funktioniert
        public int StartingPrintPosition { get; set; }                  //Speichert, wo die Figuren beim Rausziehen hingesetzt werden sollen
        public List<Spielfigur> ZugehoerigeFiguren { get; set; } = new List<Spielfigur>();      //Speichert alle Figuren, die zu diesem Haus gehören

        public Spieler? ZugehoerigerSpieler { get; set; }               //Speichert den Spieler des Hauses


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
            for (int i = 1; i <= 4; i++)
            {
                ZugehoerigeFiguren.Add(new Spielfigur(i, farbe.ToString(), 0)); //Erstellen der 4 Spielfiguren
            }

        }

        public void ChangeColour()                                                              //Diese Methode ändert die Konsolenfarbe auf die des Hauses. Dies ist dazu da, um die zugehörigen Häuser oder die Figuren auszugeben
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
        public int LetztesBefahrbaresFeldBerechnen()                                            //Diese Methode berechnet das letzte Freie Feld, dass eine Figur theoretisch beziehen könnte
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

        public int LetztesMoeglichesFeldBeimReinfahrenberechnen()                               //Diese Methode berechnet das weitmöglichste Feld, welches man beim Reinfahren erreichen kann
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

        public void ZiehbareFigurenBerechnen()                                                  //Diese Methode berechnet, wie viele Figuren noch nicht ganz am Ende angelangt sind
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

        public bool NichtsBewegbar(int wieWeitZiehen)                                               //Diese Methode gibt true zurück, wenn keine Figur bewegt werden kann. Figuren im Haus zählen auch als unbewegbar
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
                else if (s.Position == 0)
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
