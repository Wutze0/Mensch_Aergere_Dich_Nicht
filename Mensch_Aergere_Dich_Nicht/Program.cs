namespace Mensch_Aergere_Dich_Nicht
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Einführung();

            Haus blauesHaus = new Haus(Verfuegbare_Farben.Blau);
            Haus gruenesHaus = new Haus(Verfuegbare_Farben.Gruen);
            Haus gelbesHaus = new Haus(Verfuegbare_Farben.Gelb);
            Haus rotesHaus = new Haus(Verfuegbare_Farben.Rot);
            List<Haus> haueser = new List<Haus>();
            haueser.Add(blauesHaus);
            haueser.Add(gruenesHaus);
            haueser.Add(gelbesHaus);
            haueser.Add(rotesHaus);

            Print print = new Print(haueser);
            print.PrintSpielfeld();


            //Testen der Methode wuerfeln
            for(int j = 0; j < 25; j++)
            {
                wuerfeln(blauesHaus, print);
                //print.PrintSpielfeld();
            }
            //haueser.ElementAt(0).ZugehoerigeFiguren.ElementAt(1)._position += wuerfeln(haueser[0]);
            //Console.WriteLine(spielfigur._position);
            /*print.PrintSpielfeld(haueser);
            
            haueser.ElementAt(0).ZugehoerigeFiguren.ElementAt(1).PrintPosition += wuerfeln(haueser[0]);
            print.PrintSpielfeld(haueser);*/


        }



        private static void wuerfeln(Haus haus, Print print)
        {
            int ziehe = 0;
            bool erneutWuerfeln = true;
            
            Random r = new Random();

            if(haus.figurenImHaus != 4) //normaler Spielablauf
            {
                int sechsCounter = 0;
                while (erneutWuerfeln == true && sechsCounter <= 3) //Logik für, wenn jemand eine 6 würfelt //Man darf nur maximal drei mal hintereinander eine 6 würfeln
                {
                    ziehe = r.Next(1, 7);
                    if (ziehe == 6) 
                    {
                        Console.WriteLine("Sie dürfen eine Figur aus dem haus ziehen!"); //noch nicht fertig; muss nicht immer der Fall sein, dass man eine Figur rausziehen darf.
                        erneutWuerfeln = true;
                        auswaehlen(haus, true, ziehe, print);
                        print.PrintSpielfeld();
                        sechsCounter++;
                        
                    }

                    else
                        erneutWuerfeln = false;

                }
                auswaehlen(haus, false, ziehe, print);
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
                    auswaehlen(haus, true, ziehe,print);
                    //print.PrintSpielfeld();
                }
                print.PrintSpielfeld();


            }

        }

        private static void auswaehlen(Haus haus, bool rausziehen, int gewuerfelt, Print print) //rausziehen bestimmt, ob man eine Figur rausziehen darf oder nicht
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
            }

            if (rausziehen == true && jaNein == true && istFeldFrei(print,haus,eingabe,gewuerfelt))
            {
                haus.figurenImHaus--;
                bool einmal = true;
                int i = 0;

                foreach (Spielfigur s in haus.ZugehoerigeFiguren)
                {
                    if (s.IsInHouse == true && einmal == true) //einmal sagt aus, dass man nur eine Figur aus dem Haus ziehen darf
                    {
                        haus.ZugehoerigeFiguren.ElementAt(i).PrintPosition = haus.StartingPrintPosition;
                        haus.ZugehoerigeFiguren.ElementAt(i).IsInHouse = false;
                        haus.ZugehoerigeFiguren.ElementAt(i)._position = 1;
                        einmal = false;
                    }
                    i++;

                }

            }
            else if (rausziehen == false && jaNein == false && istFeldFrei(print, haus, eingabe, gewuerfelt))
            {

                switch (eingabe)
                {
                    case 0:
                        break;
                    case 1:
                        if (haus.ZugehoerigeFiguren.ElementAt(0).IsInHouse == false)
                        {
                            haus.ZugehoerigeFiguren.ElementAt(0)._position += gewuerfelt;
                            haus.ZugehoerigeFiguren.ElementAt(0).PrintPosition += gewuerfelt;
                        }
                        break;
                    case 2:
                        if (haus.ZugehoerigeFiguren.ElementAt(1).IsInHouse == false)
                        {
                            haus.ZugehoerigeFiguren.ElementAt(1)._position += gewuerfelt;
                            haus.ZugehoerigeFiguren.ElementAt(1).PrintPosition += gewuerfelt;
                        }
                        break;

                    case 3:
                        if (haus.ZugehoerigeFiguren.ElementAt(2).IsInHouse == false)
                        {
                            haus.ZugehoerigeFiguren.ElementAt(2)._position += gewuerfelt;
                            haus.ZugehoerigeFiguren.ElementAt(2).PrintPosition += gewuerfelt;
                        }
                        break;

                    case 4:
                        if (haus.ZugehoerigeFiguren.ElementAt(3).IsInHouse == false)
                        {
                            haus.ZugehoerigeFiguren.ElementAt(3)._position += gewuerfelt;
                            haus.ZugehoerigeFiguren.ElementAt(3).PrintPosition += gewuerfelt;
                        }
                        break;

                }

            }
            //Man kommt hier hin wenn man eine 6 würfelt und rausziehen KÖNNTE, aber nicht will.
            else
            {
                Console.WriteLine($"Ziehe {gewuerfelt} Felder mit einer Figur!");
                Console.WriteLine("Welche Figur möchten Sie ziehen? Verfügbar: [1, 2, 3, 4]");
                eingabe = Convert.ToUInt16(Console.ReadLine());

                switch (eingabe)
                {
                    case 0:
                        break;
                    case 1:
                        if (haus.ZugehoerigeFiguren.ElementAt(0).IsInHouse == false)
                        {
                            haus.ZugehoerigeFiguren.ElementAt(0)._position += gewuerfelt;
                            haus.ZugehoerigeFiguren.ElementAt(0).PrintPosition += gewuerfelt;
                        }
                        break;
                    case 2:
                        if (haus.ZugehoerigeFiguren.ElementAt(1).IsInHouse == false)
                        {
                            haus.ZugehoerigeFiguren.ElementAt(1)._position += gewuerfelt;
                            haus.ZugehoerigeFiguren.ElementAt(1).PrintPosition += gewuerfelt;
                        }
                        break;

                    case 3:
                        if (haus.ZugehoerigeFiguren.ElementAt(2).IsInHouse == false)
                        {
                            haus.ZugehoerigeFiguren.ElementAt(2)._position += gewuerfelt;
                            haus.ZugehoerigeFiguren.ElementAt(2).PrintPosition += gewuerfelt;
                        }
                        break;

                    case 4:
                        if (haus.ZugehoerigeFiguren.ElementAt(3).IsInHouse == false)
                        {
                            haus.ZugehoerigeFiguren.ElementAt(3)._position += gewuerfelt;
                            haus.ZugehoerigeFiguren.ElementAt(3).PrintPosition += gewuerfelt;
                        }
                        break;
                }
            }
        }

        private static bool istFeldFrei(Print print, Haus zieherHaus, int gezogeneFigur, int gewuerfelt) // idese methode funktioniert NICHT
        {
            List<Spielfigur> alleSpielfiguren = print.GetAllSpielfiguren();
            Spielfigur gezogeneSpielfigur = zieherHaus.ZugehoerigeFiguren.ElementAt(gezogeneFigur);
            
            if(gezogeneSpielfigur.IsInHouse == true)
            {
                foreach (Spielfigur s in alleSpielfiguren)
                {
                    Console.WriteLine(s.PrintPosition);
                    if (s.PrintPosition == zieherHaus.StartingPrintPosition)
                    {
                        Console.WriteLine("get nid");
                        //zieherHaus.IsStartingPositionFree = false;
                        return false;
                    }
                    
                }
            }

            

            
            //(gezogeneSpielfigur._position + gewuerfelt) == s._position
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
