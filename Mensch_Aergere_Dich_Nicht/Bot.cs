using System.Data.SqlTypes;

namespace Mensch_Aergere_Dich_Nicht
{
    internal class Bot : Spieler
    {
        public Bot() : base("bot", true)
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
                        if (movefound)
                        {
                            if ((s.Position + wieWeitZiehen) >= 41 && (s.Position + wieWeitZiehen) <= hausDesBots.AktuellLetztesFelde)
                            {
                                movefound = true;
                                s.Position += wieWeitZiehen;
                                s.PrintPosition = (40 + (hausDesBots.HausID - 1) * 4) - (s.Position - 40);
                                hausDesBots.AktuellLetztesFelde = s.Position - 1;
                            }
                        }

                    }
                }
                if(priority == 2)
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
                if(priority == 3)
                {
                    if(wieWeitZiehen == 6)
                    {
                        for(int i = 0; i < 4; i++)
                        {
                            if (hausDesBots.ZugehoerigeFiguren.ElementAt(i).IsInHouse)
                            {
                                movefound = true;
                                hausDesBots.ZugehoerigeFiguren.ElementAt(i).Position = 1;
                                hausDesBots.ZugehoerigeFiguren.ElementAt(i).PrintPosition = hausDesBots.StartingPrintPosition;
                            }
                        }
                    }
                }
                //if(priority == 4)
                //{
                //    foreach(Spielfigur s in hausDesBots.ZugehoerigeFiguren)
                //    {
                //        for()
                //        foreach(Spielfigur sGegner in alleHaueser.ElementAt(i)
                //        {
                            
                //        }
                //    }
                //}


            }
        }




    }
}
