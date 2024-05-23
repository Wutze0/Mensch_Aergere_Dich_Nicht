using System.Data.SqlTypes;

namespace Mensch_Aergere_Dich_Nicht
{
    internal class Bot : Spieler
    {
        public Bot() : base($"bot{NumberOfPlayers + 1}", true)
        {

        }


        public void Spielfigurbewegen(Haus hausDesBots, List<Haus> alleHaueser, int wieWeitZiehen)
        {
            bool movefound = false;
            int priority = 1;
            while (!movefound)
            {
                if (priority == 1)
                {
                    foreach (Spielfigur s in hausDesBots.ZugehoerigeFiguren)
                    {
                        if (!movefound)
                        {
                            if ((s.Position + wieWeitZiehen) >= 41 && (s.Position + wieWeitZiehen) <= hausDesBots.AktuellLetztesFelde)                              //Ins Haus fahren
                            {
                                movefound = true;
                                s.Position += wieWeitZiehen;
                                s.PrintPosition = (40 + (hausDesBots.HausID - 1) * 4) - (s.Position - 40);
                                hausDesBots.AktuellLetztesFelde = s.Position - 1;
                            }
                        }

                    }
                }
                if(priority == 2)                                                                                                                                   //Im Haus fahren
                {
                    foreach (Spielfigur s in hausDesBots.ZugehoerigeFiguren)
                    {
                        if (!movefound)
                        {
                            if((s.Position - 1) == hausDesBots.AktuellLetztesFelde)
                            {
                                if(s.Position + wieWeitZiehen <= 44 - (4 - hausDesBots.ZiehbareFiguren))
                                {
                                    movefound = true;
                                    s.Position += wieWeitZiehen;
                                    s.PrintPosition = (40 + (hausDesBots.HausID - 1) * 4) - (s.Position - 40);
                                    hausDesBots.AktuellLetztesFelde = s.Position - 1;
                                }
                            }
                        }
                    }
                }
                if(priority == 3)                                                                                                                                   //Aus dem Haus fahren
                {
                    if(wieWeitZiehen == 6)
                    {
                        for(int i = 0; i < 4; i++)
                        {
                            if (hausDesBots.ZugehoerigeFiguren.ElementAt(i).IsInHouse && !movefound)
                            {
                                movefound = true;
                                hausDesBots.FigurenImHaus--;
                                hausDesBots.ZugehoerigeFiguren.ElementAt(i).Position = 1;
                                hausDesBots.ZugehoerigeFiguren.ElementAt(i).PrintPosition = hausDesBots.StartingPrintPosition;
                            }
                        }
                    }
                }
                if (priority == 4)                                                                                                                                  //Gegner schlagen
                {
                    int temp;
                    foreach (Spielfigur s in hausDesBots.ZugehoerigeFiguren)
                    {
                        if (!movefound && !s.IsInHouse)
                        {
                            temp = s.PrintPosition;
                            if (temp > 40)
                            {
                                temp %= 41;
                                temp++;
                            }
                            for (int i = 0; i < 4 && !movefound; i++)
                            {
                                foreach (Spielfigur sGeg in alleHaueser.ElementAt(i).ZugehoerigeFiguren)
                                {
                                    if (s.Farbe != sGeg.Farbe && temp == sGeg.PrintPosition && !movefound)
                                    {
                                        movefound = true;
                                        s.PrintPosition = temp;
                                        s.Position += wieWeitZiehen;
                                        if (s.Position > 40)
                                        {
                                            s.Position %= 40;
                                        }
                                        sGeg.Position = 0;
                                        sGeg.PrintPosition = 0;
                                        sGeg.IsInHouse = true;
                                        alleHaueser.ElementAt(i).FigurenImHaus++;
                                    }
                                }
                            }
                        }     
                    }
                }
                if(priority == 5)                                                                                                                                   //Vorderste Figur ziehen
                {
                    int amWeitestenVorne = 0;
                    foreach(Spielfigur s in hausDesBots.ZugehoerigeFiguren)
                    {
                        if (!s.IsInHouse)
                        {
                            if(s.Position > amWeitestenVorne && s.Position < hausDesBots.AktuellLetztesFelde)
                            {
                                s.Position = amWeitestenVorne;
                            }
                        }
                    }
                    if(amWeitestenVorne != 0) 
                    {
                        foreach(Spielfigur s in hausDesBots.ZugehoerigeFiguren)
                        {
                            if (s.Position == amWeitestenVorne)
                            {
                                s.Position += wieWeitZiehen;
                                if(s.Position > 40)
                                {
                                    s.Position %= 40;
                                }
                                s.PrintPosition += wieWeitZiehen;
                                if(s.PrintPosition > 40)
                                {
                                    s.PrintPosition %= 40;
                                }
                            }
                        }
                    }

                    movefound = true;                                                                                                                           //Beendet die Schleife

                }
                priority++;
            }
        }




    }
}
