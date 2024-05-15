namespace Mensch_Aergere_Dich_Nicht
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Einführung();

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
            for (int j = 0; j < 100; j++)
            {
                wuerfeln(blauesHaus, print);
                //print.PrintSpielfeld();
            }
            //_haueser.ElementAt(0).ZugehoerigeFiguren.ElementAt(1)._position += wuerfeln(_haueser[0]);
            //Console.WriteLine(spielfigur._position);
            /*print.PrintSpielfeld(_haueser);
            
            _haueser.ElementAt(0).ZugehoerigeFiguren.ElementAt(1).PrintPosition += wuerfeln(_haueser[0]);
            print.PrintSpielfeld(_haueser);*/


        }



        private static void wuerfeln(Haus haus, Print print)
        {
            int ziehe = 0;
            bool erneutWuerfeln = true;

            Random r = new Random();

            if (haus.figurenImHaus != 4) //normaler Spielablauf
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

                for (int i = 0; i < 3 && ziehe != 6; i++) //Zu Beginn des Spiels darf man 3 mal würfeln, um eine Figur aus dem Haus zu bringen.
                {
                    ziehe = r.Next(1, 7);
                    Console.WriteLine(ziehe);

                }
                if (ziehe == 6)
                {

                    //haus.ZugehoerigeFiguren.ElementAt(1).PrintPosition = haus.StartingPrintPosition;
                    //haus.ZugehoerigeFiguren.ElementAt(1)._position = 1;
                    auswaehlen(haus, true, ziehe, print);
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
                eingabe = 4 - haus.figurenImHaus;
            }

            if (rausziehen == true && jaNein == true && istFeldFrei(print, haus, eingabe, gewuerfelt))
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
            else if (rausziehen == false && jaNein == false && istFeldFrei(print, haus, eingabe - 1, gewuerfelt))
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
            else if (istFeldFrei(print, haus, eingabe, gewuerfelt))
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


            if (gezogeneSpielfigur.PrintPosition == 0)
            {
                foreach (Spielfigur x in alleSpielfiguren)
                {
                    if (x._position == 1)
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

        private static void Einführung()
        {



            while (true)
            {
                Console.WriteLine(
                  "Mensch Ärgere Dich Nicht!" +
                  "\n" +
                  "\n" +
                  "\n[1]Neues Spiel" +
                  "\n[2]Spiel laden" +
                  "\n[3]Spiel löschen" +
                  "\n"
                  );

                string eingabe = string.Empty;
                eingabe = Console.ReadLine();

                //try
                //{
                int modus = Convert.ToInt32(eingabe);
                switch (modus)
                {
                    case 1: EinleitungNeuesSpiel(); break;
                    default: Console.WriteLine("Falsche Eingabe... erneuter Versuch: "); break;

                }
                //}
                /*catch (Exception e)
                {
                    Console.WriteLine("Falsche Eingabe... erneuter Versuch:");
                }*/

            }

        }
        private static void EinleitungNeuesSpiel()
        {
            Console.Clear();
            int spielerzahl = int.MinValue;
            do
            {
                Console.WriteLine("Bitte geben Sie die Anzahl der Spieler ein(2-4):");
                string eingabe = Console.ReadLine();
                try
                {
                    spielerzahl = Convert.ToInt32(eingabe);

                }
                catch (Exception e)
                {
                    Console.WriteLine("Falsche Eingabe... erneuter Versuch:");
                }
            } while (spielerzahl != 2 && spielerzahl != 3 && spielerzahl != 4);
            List<Spieler> spielerliste = new List<Spieler>();
            List<Haus> haeuser = new List<Haus>();

            for (int i = 0; i < spielerzahl; i++)
            {
                Console.WriteLine($"Bitte den Namen des {i + 1}. Spielers eingeben:");
                string name = Console.ReadLine();
                spielerliste.Add(new Spieler(name, i + 1));
                Console.WriteLine($"\n{name}, Bitte geben Sie Ihre gewünschte Hausfarbe ein\n" +
                    $"Verfügbar sind folgende:\n{getAvailableColors(haeuser)}");
                bool check = true;
                while (check == true)
                {

                    string gewuenschteFarbe = Console.ReadLine();
                    Verfuegbare_Farben farbe = Verfuegbare_Farben.Rot;
                    if (Enum.TryParse(gewuenschteFarbe, out farbe))
                    {
                        if (getAvailableColors(haeuser).Contains(gewuenschteFarbe))
                        {
                            Console.WriteLine($"Erfolgreich die Farbe {farbe} ausgewählt!");
                            haeuser.Add(new Haus(farbe));
                            check = false;
                        }
                        else
                        {
                            Console.WriteLine("Diese Farbe wird schon verwendet, bitte eine andere Farbe wählen");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Diese Farbe existiert nicht, bitte eine andere Farbe wählen:");
                    }
                }

            }
            for (int i = haeuser.Count; i < 4; i++)
            {
                haeuser.Add(new Haus(Verfuegbare_Farben.Weiss));
            }
            Thread.Sleep(1000);
            Console.Clear();

            int j = 0;
            foreach (Spieler s in spielerliste)
            {
                Console.WriteLine($"Spieler {j + 1}: {s.Name} mit {haeuser.ElementAt(j).Farbe} als Farbe des Hauses.");
                j++;
            }
            foreach (Haus h in haeuser)
            {
                Console.WriteLine("haus hier");
            }

            Spielablauf(haeuser, spielerliste);

        }

        private static void Spielablauf(List<Haus> haeuser, List<Spieler> spieler)
        {
            Print p = new Print(haeuser);

            int abtauschen = 0;
            for (int k = 0; k < 100; k++)
            {
                
                Console.WriteLine($"Der Spieler {spieler.ElementAt(abtauschen).Name} ist dran!");
                Console.WriteLine(abtauschen);
                Console.WriteLine(haeuser.ElementAt(abtauschen).Farbe);

                switch (abtauschen)
                {
                    case 0:
                        wuerfeln(haeuser.ElementAt(abtauschen), p); abtauschen++; break;
                    case 1:
                        wuerfeln(haeuser.ElementAt(abtauschen), p); abtauschen++; break;
                    case 2:
                        wuerfeln(haeuser.ElementAt(abtauschen), p); abtauschen++; break;
                    case 3:
                        wuerfeln(haeuser.ElementAt(abtauschen), p); abtauschen = 0; break;
                }

                //updaten: so machen dass für die weißen nicht gewürfelt werden darf.

            }
        }
        private static string getAvailableColors(List<Haus> haeuser)
        {
            //die weißen sollen nicht als farbe gelten
            List<Verfuegbare_Farben> liste = new List<Verfuegbare_Farben>();
            List<Verfuegbare_Farben> verwendeteFarben = new List<Verfuegbare_Farben>();
            int i = 0;

            foreach (Haus h in haeuser)
            {
                Verfuegbare_Farben farbe = 0;
                Enum.TryParse(h.Farbe.ToString(), out farbe);
                verwendeteFarben.Add(farbe);
                i++;
            }

            foreach (Verfuegbare_Farben farbe in Enum.GetValues(typeof(Verfuegbare_Farben)))
            {
                if (!verwendeteFarben.Contains(farbe))
                {
                    liste.Add(farbe);
                }
            }
            string alleFarben = string.Empty;
            foreach (Verfuegbare_Farben f in liste)
            {
                alleFarben += f.ToString() + "\n";
            }

            return alleFarben;
        }


    }
}
