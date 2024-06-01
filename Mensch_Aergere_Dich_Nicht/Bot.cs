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
                            if ((s.Position + wieWeitZiehen) > 40 && (s.Position + wieWeitZiehen) <= hausDesBots.LetztesMoeglichesFeldBeimReinfahrenberechnen())                              //Ins Haus fahren
                            {
                                movefound = true;
                                s.Position += wieWeitZiehen;
                                s.PrintPosition = s.Position + ((hausDesBots.HausID - 1) * 4);
                            }
                        }

                    }
                }
                if (priority == 2)                                                                                                                                   //Im Haus fahren
                {
                    List<Spielfigur> moeglicheFiguren = new List<Spielfigur>();
                    foreach(Spielfigur s in hausDesBots.ZugehoerigeFiguren)
                    {
                        moeglicheFiguren.Add(s);
                    }
                    
                    List<Spielfigur> zuEntfernen = new List<Spielfigur>();
                    int letztesBefahrbaresFeld = hausDesBots.LetztesBefahrbaresFeldBerechnen();
                    Spielfigur? zuBewegen = null;

                    List<int> positionen = new List<int>();
                    foreach (Spielfigur s in hausDesBots.ZugehoerigeFiguren)
                    {
                        positionen.Add(s.Position);
                    }

                    foreach (Spielfigur s in moeglicheFiguren)
                    {
                        if (s.Position <= 40 || s.Position > letztesBefahrbaresFeld)
                        {
                           zuEntfernen.Add(s);
                        }
                    }
                    foreach (Spielfigur s in moeglicheFiguren)
                    {
                        if (positionen.Contains(s.Position + 1))
                        {
                            zuEntfernen.Add(s);
                        }
                    }
                    foreach (Spielfigur s in moeglicheFiguren)
                    {
                        int naehesteFigurFeld = letztesBefahrbaresFeld;

                        for (int i = 0; i < 4; i++)
                        {
                            if (positionen[i] != s.Position)
                            {
                                if (positionen[i] > s.Position && positionen[i] < naehesteFigurFeld)
                                {
                                    naehesteFigurFeld = positionen[i];
                                }
                            }

                        }
                        int maximaleAnz = naehesteFigurFeld - s.Position;

                        if (maximaleAnz < wieWeitZiehen)
                        {
                            zuEntfernen.Add(s);
                        }
                    }

                    foreach(Spielfigur s in zuEntfernen)
                    {
                        moeglicheFiguren.Remove(s);
                    }

                    if (moeglicheFiguren.Count() > 0)
                    {
                        if (moeglicheFiguren.Count() > 1)
                        {
                            foreach (Spielfigur s in moeglicheFiguren)
                            {
                                if (zuBewegen == null)
                                {
                                    zuBewegen = s;
                                }
                                else
                                {
                                    if (s.Position > zuBewegen.Position)
                                    {
                                        zuBewegen = s;
                                    }
                                }

                            }
                        }
                        else
                        {
                            zuBewegen = moeglicheFiguren[0];
                        }

                        zuBewegen.Position += wieWeitZiehen;
                        zuBewegen.PrintPosition = zuBewegen.Position + ((hausDesBots.HausID - 1) * 4);
                        movefound = true;
                    }

                }
                if (priority == 3)                                                                                                                                   //Aus dem Haus fahren
                {
                    bool feldFrei = true;
                    if (wieWeitZiehen == 6)
                    {
                        foreach (Spielfigur s in hausDesBots.ZugehoerigeFiguren)
                        {
                            if (s.Position == 1)
                            {
                                feldFrei = false;
                            }
                        }

                        if (feldFrei)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                if (hausDesBots.ZugehoerigeFiguren.ElementAt(i).IsInHouse && !movefound)
                                {
                                    movefound = true;
                                    hausDesBots.FigurenImHaus--;
                                    hausDesBots.ZugehoerigeFiguren.ElementAt(i).Position = 1;
                                    hausDesBots.ZugehoerigeFiguren.ElementAt(i).PrintPosition = hausDesBots.StartingPrintPosition;
                                    hausDesBots.ZugehoerigeFiguren.ElementAt(i).IsInHouse = false;
                                    foreach (Haus h in alleHaueser)
                                    {
                                        if (h != hausDesBots)
                                        {
                                            foreach (Spielfigur s in h.ZugehoerigeFiguren)
                                            {
                                                if (s.PrintPosition == hausDesBots.ZugehoerigeFiguren.ElementAt(i).PrintPosition)
                                                {
                                                    s.Position = 0;
                                                    s.PrintPosition = 0;
                                                    s.IsInHouse = true;
                                                    h.FigurenImHaus++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }


                        }

                    }
                }
                if (priority == 4)                                                                                                                                  //Gegner schlagen
                {
                    int temp;
                    foreach (Spielfigur s in hausDesBots.ZugehoerigeFiguren)
                    {
                        if (!movefound && !s.IsInHouse && s.PrintPosition <= 40)
                        {
                            temp = s.PrintPosition + wieWeitZiehen;
                            if (temp > 40)
                            {
                                temp %= 40;
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
                if (priority == 5)                                                                                                                                   //Vorderste Figur ziehen
                {
                    int amWeitestenVorne = 0;
                    foreach (Spielfigur s in hausDesBots.ZugehoerigeFiguren)
                    {
                        if (!s.IsInHouse)
                        {
                            if (s.Position > amWeitestenVorne && s.Position < 41)
                            {
                                amWeitestenVorne = s.Position;
                            }
                        }
                    }
                    if (amWeitestenVorne != 0)
                    {
                        foreach (Spielfigur s in hausDesBots.ZugehoerigeFiguren)
                        {
                            if (s.Position == amWeitestenVorne)
                            {
                                s.Position += wieWeitZiehen;
                                if (s.Position > 40)
                                {
                                    s.Position %= 40;
                                }
                                s.PrintPosition += wieWeitZiehen;
                                if (s.PrintPosition > 40)
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
            Thread.Sleep(5000);
        }




    }
}
