using System.ComponentModel.Design;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;


namespace Mensch_Aergere_Dich_Nicht
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Haus blauesHaus = new Haus(Verfuegbare_Farben.Blau);
            Haus gruenesHaus = new Haus(Verfuegbare_Farben.Gruen);
            Haus gelbesHaus = new Haus(Verfuegbare_Farben.Magenta);
            Haus rotesHaus = new Haus(Verfuegbare_Farben.Rot);
            List<Haus> _haueser = new List<Haus>();
            _haueser.Add(blauesHaus);
            _haueser.Add(gruenesHaus);
            _haueser.Add(gelbesHaus);
            _haueser.Add(rotesHaus);

            Print print = new Print(_haueser);
            print.PrintSpielfeld();


            //Testen der Methode wuerfeln
            for(int j = 0; j < 25; j++)
            {
                wuerfeln(blauesHaus, print, _haueser);
                //print.PrintSpielfeld();
            }
            //_haueser.ElementAt(0).ZugehoerigeFiguren.ElementAt(1)._position += wuerfeln(_haueser[0]);
            //Console.WriteLine(spielfigur._position);
            /*print.PrintSpielfeld(_haueser);
            
            _haueser.ElementAt(0).ZugehoerigeFiguren.ElementAt(1).PrintPosition += wuerfeln(_haueser[0]);
            print.PrintSpielfeld(_haueser);*/


        }



        private static void wuerfeln(Haus haus, Print print, List<Haus> haueser)
        {
            int ziehe = 0;
            bool erneutWuerfeln = true;
            
            Random r = new Random();

            if(haus.figurenImHaus != 4) //normaler Spielablauf
            {
                int sechsCounter = 0;
                while (erneutWuerfeln != false && sechsCounter < 3) //Logik für, wenn jemand eine 6 würfelt //Man darf nur maximal drei mal hintereinander eine 6 würfeln
                {
                    ziehe = r.Next(1, 7);
                    if (ziehe == 6) 
                    {

                        erneutWuerfeln = true;
                        if(haus.figurenImHaus > 0)
                        {
                            Console.WriteLine("Sie dürfen eine Figur aus dem haus ziehen!");        //noch nicht fertig; muss nicht immer der Fall sein, dass man eine Figur rausziehen darf.
                            auswaehlen(haus, true, ziehe, print, haueser);
                        }
                        else
                        {
                            auswaehlen(haus, false, ziehe, print, haueser);
                        }
                        print.PrintSpielfeld();
                        sechsCounter++;
                        
                    }

                    else
                        erneutWuerfeln = false;

                }
                auswaehlen(haus, false, ziehe, print, haueser);
                print.PrintSpielfeld();
                //maximal drei mal würfeln und maximal drei mal 6 würfeln
                //auswählen welche figur GEMACHT
                //Console.WriteLine(ziehe);

            }
            else
            {
                
                for(int i = 0; i < 3 && ziehe != 6; i++) //Zu Beginn des Spiels darf man 3 mal würfeln, um eine Figur aus dem Haus zu bringen.
                {
                    ziehe = r.Next(1, 7);
                    Console.WriteLine(ziehe);

                }
                if(ziehe == 6)
                {

                    //haus.ZugehoerigeFiguren.ElementAt(1).PrintPosition = haus.StartingPrintPosition;
                    //haus.ZugehoerigeFiguren.ElementAt(1)._position = 1;
                    auswaehlen(haus, true, ziehe,print, haueser);
                    print.PrintSpielfeld();
                }


            }


        }

        private static void auswaehlen(Haus haus, bool rausziehen, int gewuerfelt, Print print, List<Haus> haeuser) //rausziehen bestimmt, ob man eine Figur rausziehen darf oder nicht
        {
            bool jaNein = false;
            int eingabe = 0;
            if (!rausziehen)
            {
                Console.WriteLine($"Ziehe {gewuerfelt} Felder mit einer Figur!");
                Console.WriteLine("Welche Figur möchten Sie ziehen? Verfügbar: [1, 2, 3, 4]");
                eingabe = Convert.ToInt16(Console.ReadLine());
            }
            else
            {
                Console.WriteLine("Möchten Sie eine Figur aus dem Haus ziehen?");
                jaNein = Convert.ToBoolean(Console.ReadLine());
                eingabe = 4 - haus.figurenImHaus;
            }

            if (rausziehen == true && jaNein == true && istFeldFrei(print,haus,eingabe,gewuerfelt))
            {
                haus.figurenImHaus--;
                bool einmal = true;
                int i = 0;
                Spielfigur aktuelleFigur = null;

                foreach (Spielfigur s in haus.ZugehoerigeFiguren)
                {
                    if (s.IsInHouse == true && einmal == true) //einmal sagt aus, dass man nur eine Figur aus dem Haus ziehen darf
                    {
                        haus.ZugehoerigeFiguren.ElementAt(i).PrintPosition = haus.StartingPrintPosition;
                        haus.ZugehoerigeFiguren.ElementAt(i).IsInHouse = false;
                        haus.ZugehoerigeFiguren.ElementAt(i)._position = 1;
                        einmal = false;
                        aktuelleFigur = haus.ZugehoerigeFiguren.ElementAt(i);
                    }
                    i++;
                }
                foreach(Haus h in haeuser)
                {
                    foreach(Spielfigur s in h.ZugehoerigeFiguren)
                    {
                        if(s.PrintPosition == aktuelleFigur.PrintPosition && s != aktuelleFigur)
                        {
                            s.PrintPosition = 0;
                            s._position = 0;
                            s.IsInHouse = true;
                            h.figurenImHaus++;
                        }
                    }
                }

            }
            else if (rausziehen == false && jaNein == false && istFeldFrei(print, haus, eingabe - 1, gewuerfelt))
            {
                int temp = eingabe - 1;

                if (haus.ZugehoerigeFiguren.ElementAt(temp).IsInHouse == false)
                {
                    Spielfigur aktuelleFigur = haus.ZugehoerigeFiguren.ElementAt(temp);
                    aktuelleFigur._position += gewuerfelt;
                    aktuelleFigur.PrintPosition += gewuerfelt;
                    foreach (Haus h in haeuser)
                    {
                        foreach (Spielfigur s in h.ZugehoerigeFiguren)
                        {
                            if (s.PrintPosition == aktuelleFigur.PrintPosition && s != aktuelleFigur)
                            {
                                s.PrintPosition = 0;
                                s._position = 0;
                                s.IsInHouse = true;
                                h.figurenImHaus++;
                            }
                        }
                    }
                }

                
            }
            //Man kommt hier hin wenn man eine 6 würfelt und rausziehen KÖNNTE, aber nicht will.
            else
            {
                Console.WriteLine($"Ziehe {gewuerfelt} Felder mit einer Figur!");
                Console.WriteLine("Welche Figur möchten Sie ziehen? Verfügbar: [1, 2, 3, 4]");
                eingabe = Convert.ToUInt16(Console.ReadLine());
                int temp = eingabe - 1;

                if (haus.ZugehoerigeFiguren.ElementAt(temp).IsInHouse == false)
                {
                    Spielfigur aktuelleFigur = haus.ZugehoerigeFiguren.ElementAt(temp);
                    aktuelleFigur._position += gewuerfelt;
                    aktuelleFigur.PrintPosition += gewuerfelt;
                    foreach (Haus h in haeuser)
                    {
                        foreach (Spielfigur s in h.ZugehoerigeFiguren)
                        {
                            if (s.PrintPosition == aktuelleFigur.PrintPosition && s != aktuelleFigur)
                            {
                                s.PrintPosition = 0;
                                s._position = 0;
                                s.IsInHouse = true;
                                h.figurenImHaus++;
                            }
                        }
                    }
                }

            }
            
        }

        private static bool istFeldFrei(Print print, Haus zieherHaus, int gezogeneFigur, int gewuerfelt)
        {
            List<Spielfigur> alleSpielfiguren = print.GetAllSpielfiguren();
            Spielfigur gezogeneSpielfigur = zieherHaus.ZugehoerigeFiguren.ElementAt(gezogeneFigur);


            if(gezogeneSpielfigur.PrintPosition == 0)
            {
                foreach(Spielfigur x in alleSpielfiguren)
                {
                    if(x._position == 1)
                    {
                        if(x._farbe == gezogeneSpielfigur._farbe)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            else
            {
                foreach (Spielfigur x in alleSpielfiguren)
                {
                    if (x._position == gezogeneSpielfigur._position + gewuerfelt)
                    {
                        if (x._farbe == gezogeneSpielfigur._farbe)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }

            }
            return true;
        }

        //private static void Einführung()
        //{
        //    Console.WriteLine(
        //          "Mensch Ärgere Dich Nicht!" +
        //          "\n" +
        //          "\n" +
        //          "\n[1]Neues Spiel" +
        //          "\n[2]Spiel laden" +
        //          "\n[3]Spiel löschen" +
        //          "\n"
        //          );


        //    while (true)
        //    {
        //        string eingabe = string.Empty;
        //        try
        //        {
        //            eingabe = Console.ReadLine();
        //            Regex eingabeRegex = new Regex("1?2?3?"); // weitermachen geht noch nicht
        //            if (eingabeRegex.IsMatch(eingabe))
        //            {
        //                Convert.ToUInt32(eingabe);
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine("Falsche Eingabe... erneuter Versuch:");
        //        }
        //    }

        //}

        //Samuel wenn beim Wuerfeln eine Spielfigur aus dem Haus zieht, dies bitte in der figurenImHaus variable der Klasse haus vermerken


    }
}
