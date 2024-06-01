namespace Mensch_Aergere_Dich_Nicht
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            //julian, wenn du testen willst und nicht immer die Einführung durchmachen willst, kannst du Zeile 8 auskommentieren.
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
                //wuerfeln(blauesHaus, print, _haueser);
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
            bool botYesNo = haus.ZugehoerigerSpieler.BotYesNo;


            Random r = new Random();

            if (haus.FigurenImHaus <= haus.ZiehbareFiguren) //normaler Spielablauf, ziehbareFiguren speichert die Anzahl der Figuren, die sich noch bewegen könnte und somit nicht am Ende angelangt sind
            {
                int sechsCounter = 0;
                while (erneutWuerfeln == true && sechsCounter <= 3) //Logik für, wenn jemand eine 6 würfelt //Man darf nur maximal drei mal hintereinander eine 6 würfeln
                {
                    ziehe = r.Next(1, 7);
                    if (ziehe == 6)
                    {

                        erneutWuerfeln = true;
                        if (botYesNo)
                        {
                            Bot x = haus.ZugehoerigerSpieler as Bot;
                            x.Spielfigurbewegen(haus, haueser, ziehe);
                        }
                        else
                        {
                            if (haus.FigurenImHaus > 0)
                            {

                                Console.WriteLine("Sie dürfen eine Figur aus dem haus ziehen!");        //noch nicht fertig; muss nicht immer der Fall sein, dass man eine Figur rausziehen darf.
                                auswaehlen(haus, true, ziehe, print, haueser);


                            }
                            else
                            {

                                auswaehlen(haus, false, ziehe, print, haueser);

                            }
                        }
                        

                        print.PrintSpielfeld();
                        sechsCounter++;

                    }

                    else
                        erneutWuerfeln = false;

                }
                if (botYesNo)
                {
                    Bot x = haus.ZugehoerigerSpieler as Bot;
                    x.Spielfigurbewegen(haus, haueser, ziehe);
                }
                else
                {
                    auswaehlen(haus, false, ziehe, print, haueser);
                }
                
                
                
                
                
                
                
                
                
                
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
                    if(botYesNo)
                    {
                        Bot x = haus.ZugehoerigerSpieler as Bot;
                        x.Spielfigurbewegen(haus, haueser, ziehe);
                    }
                    else
                    {
                        //haus.ZugehoerigeFiguren.ElementAt(1).PrintPosition = haus.StartingPrintPosition;
                        //haus.ZugehoerigeFiguren.ElementAt(1)._position = 1;
                        auswaehlen(haus, true, ziehe, print, haueser);
                        print.PrintSpielfeld();
                    }
                    
                }
                print.PrintSpielfeld();


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
                eingabe = 4 - haus.FigurenImHaus;
            }

            if (rausziehen == true && jaNein == true && istFeldFrei(print, haus, eingabe, gewuerfelt))      //Hier kommt man hinein, wenn man eine Figur aus dem Haus ziehen darf und will
            {
                haus.FigurenImHaus--;
                bool einmal = true;
                int i = 0;
                Spielfigur aktuelleFigur = null;

                foreach (Spielfigur s in haus.ZugehoerigeFiguren)
                {
                    if (s.IsInHouse == true && einmal == true) //einmal sagt aus, dass man nur eine Figur aus dem Haus ziehen darf
                    {
                        haus.ZugehoerigeFiguren.ElementAt(i).PrintPosition = haus.StartingPrintPosition;
                        haus.ZugehoerigeFiguren.ElementAt(i).IsInHouse = false;
                        haus.ZugehoerigeFiguren.ElementAt(i).Position = 1;
                        einmal = false;
                        aktuelleFigur = haus.ZugehoerigeFiguren.ElementAt(i);
                    }
                    i++;
                }
                foreach (Haus h in haeuser)
                {
                    foreach (Spielfigur s in h.ZugehoerigeFiguren)
                    {
                        if (s.PrintPosition == aktuelleFigur.PrintPosition && s != aktuelleFigur)
                        {
                            s.PrintPosition = 0;
                            s.Position = 0;
                            s.IsInHouse = true;
                            h.FigurenImHaus++;
                        }
                    }
                }

            }
            else if (rausziehen == false && jaNein == false && istFeldFrei(print, haus, eingabe - 1, gewuerfelt))       //Hier kommt man hinein, wenn man eine Figur nur ziehen darf
            {
                int temp = eingabe - 1;

                if (haus.ZugehoerigeFiguren.ElementAt(temp).IsInHouse == false)
                {
                    Spielfigur aktuelleFigur = haus.ZugehoerigeFiguren.ElementAt(temp);
                    int letztesBefahrbaresFeld = 44;
                    if(aktuelleFigur.Position <= 44 && aktuelleFigur.Position >= 41)                    //Im Haus fahren
                    {
                        List<int> positionen = new List<int>();
                        foreach(Spielfigur s in haus.ZugehoerigeFiguren)
                        {
                            if(s != aktuelleFigur)
                            {
                                positionen.Add(s.Position);
                            }
                        }

                        for(int i = 0; i < 4; i++)
                        {
                            if(positionen.Contains(44 - i))
                            {
                                letztesBefahrbaresFeld--;
                            }
                            else
                            {
                                i = 4;
                            }
                        }

                        if(aktuelleFigur.Position > letztesBefahrbaresFeld)
                        {
                            Console.WriteLine("Diese Figur kann nicht gezogen werden.");                                            // Man könnte es so machen, dass immer wenn der Benutzer eine ungueltige Eingabe taetigt, eine Exception geworfen wird und am Ende ueberprueft wird, ob eine Exception geworfen worden ist.
                        }                                                                                                           //Wenn Ja darf der Benutzer nochmal auswaehlen
                        else
                        {
                            if(positionen.Contains(aktuelleFigur.Position + 1))
                            {
                                Console.WriteLine("Diese Figur kann nicht gezogen werden.");
                            }
                            else
                            {
                                int maximalZiehbareAnzahl;
                                int naehesteFigur = letztesBefahrbaresFeld;
                                foreach(int i in positionen)
                                {
                                    if(i > aktuelleFigur.Position && i < naehesteFigur)
                                    {
                                        naehesteFigur = i;
                                    }
                                }

                                maximalZiehbareAnzahl = naehesteFigur - aktuelleFigur.Position;

                                if(maximalZiehbareAnzahl >= gewuerfelt)
                                {
                                    aktuelleFigur.Position += gewuerfelt;
                                    aktuelleFigur.PrintPosition = 40 + gewuerfelt + (haus.HausID * 4);
                                }
                                else
                                {
                                    Console.WriteLine("Diese Figur kann nicht gezogen werden.");
                                }
                            }
                        }



                    }
                    else
                    {
                        aktuelleFigur.Position += gewuerfelt;
                        aktuelleFigur.PrintPosition += gewuerfelt;



                        foreach (Haus h in haeuser)
                        {
                            foreach (Spielfigur s in h.ZugehoerigeFiguren)
                            {
                                if (s.PrintPosition == aktuelleFigur.PrintPosition && s != aktuelleFigur)
                                {
                                    s.PrintPosition = 0;
                                    s.Position = 0;
                                    s.IsInHouse = true;
                                    h.FigurenImHaus++;
                                }
                            }
                        }

                    }

                    //aktuelleFigur.Position += gewuerfelt;
                    //if(aktuelleFigur.)
                    //if (aktuelleFigur.Position > haus.AktuellLetztesFelde)               //Diese Verzweigung wird betreten, wenn die Figur übers Haus hinauszieht
                    //{
                    //    aktuelleFigur.Position %= (haus.AktuellLetztesFelde + 1);
                    //    aktuelleFigur.Position++;
                    //}
                    //if (aktuelleFigur.Position <= 44 && aktuelleFigur.Position >= 41)
                    //{
                    //    aktuelleFigur.PrintPosition = aktuelleFigur.Position + (4 + (haus.HausID - 1));
                    //}
                    //else
                    //{
                    //    aktuelleFigur.PrintPosition += gewuerfelt;
                    //    if (aktuelleFigur.PrintPosition > 40)
                    //    {
                    //        aktuelleFigur.PrintPosition %= 41;
                    //        aktuelleFigur.PrintPosition++;

                    //    }
                    //}

                    
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
                    aktuelleFigur.Position += gewuerfelt;
                    aktuelleFigur.PrintPosition += gewuerfelt;
                    foreach (Haus h in haeuser)
                    {
                        foreach (Spielfigur s in h.ZugehoerigeFiguren)
                        {
                            if (s.PrintPosition == aktuelleFigur.PrintPosition && s != aktuelleFigur)
                            {
                                s.PrintPosition = 0;
                                s.Position = 0;
                                s.IsInHouse = true;
                                h.FigurenImHaus++;
                            }
                        }
                    }
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
                    if (x.Position == 1)
                    {
                        if (x.Farbe == gezogeneSpielfigur.Farbe)
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
                    if (x.Position == (gezogeneSpielfigur.Position + gewuerfelt) % 40)
                    {
                        if (x.Farbe == gezogeneSpielfigur.Farbe)
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
                    case 2: LadeSpiel(); break;
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
            bool bot = false;
            int botAnzahl = 0;
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

            if (spielerzahl != 0)
            {
                do
                {
                    Console.WriteLine($"Bitte geben Sie die Anzahl der Bots ein(0-{4 - spielerzahl}):");
                    string eingabe = Console.ReadLine();
                    try
                    {
                        botAnzahl = Convert.ToInt32(eingabe);

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Falsche Eingabe... erneuter Versuch:");
                    }
                } while (botAnzahl != 2 && botAnzahl != 3 && botAnzahl != 1 && botAnzahl != 0);

                if (botAnzahl > 0)
                    bot = true;
            }

            List<Spieler> spielerliste = new List<Spieler>();
            List<Haus> haeuser = new List<Haus>();

            for (int i = 0; i < spielerzahl; i++)
            {
                Console.WriteLine($"Bitte den Namen des {i + 1}. Spielers eingeben:");
                string name = Console.ReadLine();
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
                haeuser.ElementAt(i).ZugehoerigerSpieler = new Menschlicher_Spieler(name);
                spielerliste.Add(haeuser.ElementAt(i).ZugehoerigerSpieler);
            }
            for (int i = 0; i < botAnzahl; i++)
            {
                Random r = new Random();
                int farbeIndex = r.Next(0, 7); // 0 bis 7 weil es so viele Farben im Enum gibt.
                Verfuegbare_Farben farbe = (Verfuegbare_Farben)farbeIndex; //somit kann man auf die korrekte Stelle des Enums zugreifen.
                if (!getAvailableColors(haeuser).Contains(farbe.ToString()))
                {
                    haeuser.Add(new Haus(farbe));
                    haeuser.ElementAt(i + spielerzahl).ZugehoerigerSpieler = new Bot();
                    Console.WriteLine("haus zugefürgt");
                }
                else
                {
                    i--;
                }

            }
            for (int i = haeuser.Count; i < 4; i++)                                                                                         //Die leeren Haueser mit Pseudo bots auffuelen
            {
                Haus x = new Haus(Verfuegbare_Farben.Weiss);
                x.AuffuellHaus = true;
                haeuser.Add(x);
            }
            Thread.Sleep(1000);
            Console.Clear();

            int j = 0;
            foreach (Menschlicher_Spieler s in spielerliste)
            {
                Console.WriteLine($"Spieler {j + 1}: {s.Name} mit {haeuser.ElementAt(j).Farbe} als Farbe des Hauses.");
                j++;
            }


            Spielablauf(haeuser, spielerliste, bot);

        }

        private static void Spielablauf(List<Haus> haeuser, List<Spieler> spieler, bool bot)
        {
            Print p = new Print(haeuser);
            int abtauschen = 0;
            string timestamp = DateTime.Now.ToString("dd-MM-HH-mm");
            string path = $"SaveFile-{timestamp}.txt";

            for (int k = 0; k < 100; k++)
            {

                if (abtauschen < spieler.Count)
                {
                    Console.WriteLine($"Der Spieler {spieler.ElementAt(abtauschen).Name} ist dran!");
                }

                switch (abtauschen)
                {
                    case 0:

                        if (!haeuser.ElementAt(abtauschen).AuffuellHaus) 
                        {
                            wuerfeln(haeuser.ElementAt(abtauschen), p, haeuser);
                        } 
                        abtauschen++;
                        break;

                    case 1:

                        if (!haeuser.ElementAt(abtauschen).AuffuellHaus)
                        {
                            wuerfeln(haeuser.ElementAt(abtauschen), p, haeuser);
                        }
                        abtauschen++;
                        break;

                    case 2:

                        if (!haeuser.ElementAt(abtauschen).AuffuellHaus)
                        {
                            wuerfeln(haeuser.ElementAt(abtauschen), p, haeuser);
                        }
                        abtauschen++;
                        break;

                    case 3:
                        

                        if (!haeuser.ElementAt(abtauschen).AuffuellHaus)
                        {
                            wuerfeln(haeuser.ElementAt(abtauschen), p, haeuser);
                        }
                        
                        abtauschen = 0;

                        FileInfo[] list = GetAllSaveFiles();
                        if (list.Length <= 5)
                        {
                            Console.WriteLine("Wollen Sie das Spiel speichern? [y/n]");
                            char eingabe = '\0';
                            char.TryParse(Console.ReadLine(), out eingabe);

                            if (eingabe.Equals('y'))
                            {
                                SpielSpeichern(spieler, haeuser, path);
                            }
                            else if (eingabe.Equals('y'))
                            {
                                SpielSpeichern(spieler, haeuser, path);
                            }
                        }
                        
                        break;
                }

            }


        }
        private static string getAvailableColors(List<Haus> haeuser)
        {
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
                if (!verwendeteFarben.Contains(farbe) && farbe != Verfuegbare_Farben.Weiss)
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
        //für gewinnüberprüfung eventuell: jedes haus hat ja 4 felder wo eine figur reingehen muss, also für jedes haus einen positionsArray machen.

        public static void SpielSpeichern(List<Spieler> spielerliste, List<Haus> haeuser, string path)
        {
           
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);

                string header = "Spieler\tHausfarbe\tZugehörige Spielfiguren\n";
                sw.Write(header);

                for (int i = 0; i < spielerliste.Count; i++)
                {
                    Spieler spieler = spielerliste.ElementAt(i);
                    Haus haus = haeuser.ElementAt(i);
                    sw.Write($"{spieler.Name}\t{haus.Farbe}\t");

                    List<string> figurenPositionen = new List<string>();
                    for (int j = 0; j < haus.ZugehoerigeFiguren.Count; j++)
                    {
                        figurenPositionen.Add(haus.ZugehoerigeFiguren.ElementAt(j).Position.ToString());
                    }

                    sw.WriteLine(string.Join(";", figurenPositionen)); //zwischen jedem Element in figurenPositionen wird ein ; eingefügt
                }
            sw.Close();
            fs.Close();

            Console.WriteLine($"Spielstand wurde in {path} gespeichert.");
        }
        private static FileInfo[] GetAllSaveFiles()
        {
            string c = Directory.GetCurrentDirectory();
            DirectoryInfo d = new DirectoryInfo(c); 
            FileInfo[] files = d.GetFiles("*.txt");//alle files die die Dateiendung .txt haben.

            return files;
        }
        private static void LadeSpiel()
        {
            FileInfo[] f = GetAllSaveFiles();
            int i = 1;
            bool check = true;
            int eingabe = 0;

            do
            {
                try
                {
                    foreach (FileInfo fi in f)
                    {
                        Console.WriteLine($"{fi.Name}[{i}]");
                        i++;
                        
                    }
                    Console.WriteLine("Bitte geben Sie den Index des Savefiles ein: ");
                    eingabe = Convert.ToInt32(Console.ReadLine())-1;

                }
                
                
                catch (Exception ex)
                {
                    Console.WriteLine("Falsche Eingabe, erneuter Versuch!");
                    i = 1;
                    check = false;
                }

            } while (check == false);

            FileStream fs = f.ElementAt(eingabe).OpenRead();
            StreamReader sr = new StreamReader(fs);

            string[] zeilen = sr.ReadToEnd().Split('\n');
            string[] namen = {};
            string[] farben = { };
            int[] positionen = { };

            for (int j = 1; i < zeilen.Length; i++) 
            {
                namen[j] = zeilen[j].Split('\t')[0];
                farben[j] = zeilen[j].Split('\t')[1];
            }

        }

    }

}


