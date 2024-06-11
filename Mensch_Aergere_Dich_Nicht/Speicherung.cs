namespace Mensch_Aergere_Dich_Nicht
{
    internal class Speicherung
    {
        public Spiellogik Logik { get; set; }
        public Speicherung()
        {
        }

        public void SpielSpeichern(List<Spieler> spielerliste, List<Haus> haeuser, string path)
        {

            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);

            string header = "Spieler\tHausfarbe\tZugehörige Spielfiguren\tPrintpositionen\n";
            sw.Write(header); //Den Header ins File schreiben.

            for (int i = 0; i < spielerliste.Count; i++) //Für jeden Spieler die Daten speichern.
            {
                Spieler spieler = spielerliste.ElementAt(i);
                Haus haus = haeuser.ElementAt(i);
                sw.Write($"{spieler.Name}\t{haus.Farbe}\t"); //Schreiben des Spielernamens und Hausfarbe.

                List<string> figurenPositionen = new List<string>(); //Liste für die Positionen der Figuren
                List<string> figurenPrintPositionen = new List<string>(); //Liste für die PrintPositionen der Figuren, wichtig für Print Klasse.
                for (int j = 0; j < haus.ZugehoerigeFiguren.Count; j++) //Durch alle Figuren durchiterieren
                {
                    figurenPositionen.Add(haus.ZugehoerigeFiguren.ElementAt(j).Position.ToString());
                    figurenPrintPositionen.Add(haus.ZugehoerigeFiguren.ElementAt(j).PrintPosition.ToString());
                }

                sw.Write(string.Join(";", figurenPositionen)); //zwischen jedem Element in figurenPositionen wird ein ; eingefügt
                sw.Write('\t');
                if (spieler is AngemeldeterSpieler)
                {
                    sw.Write(string.Join(";", figurenPrintPositionen));
                    sw.Write("\t");
                    sw.WriteLine("Angemeldet");
                }
                else
                {
                    sw.WriteLine(string.Join(";", figurenPrintPositionen));
                }


            }
            sw.Close();
            fs.Close();

            Console.WriteLine($"Spielstand wurde in {path} gespeichert.");
        }
        public FileInfo[] GetAllSaveFiles()
        {
            string c = Directory.GetCurrentDirectory(); //Holt das aktuelle Verzeichnis
            DirectoryInfo d = new DirectoryInfo(c);
            FileInfo[] files = d.GetFiles("SaveFile_*.txt");//alle files die die Dateiendung .txt haben.

            return files;
        }
        public void LadeSpiel(FileStream? fs = null, int cheat = 0)
        {
            FileInfo[] f = GetAllSaveFiles();
            int i = 1;
            bool check = true;
            int eingabe = 0;

            if (fs == null)
            {
                do//Auflistung der Save Files + Auswählen
                {
                    try
                    {
                        if (f.Length > 0)
                            Console.WriteLine("\nWieder zum Hauptmenü zurückkehren[0]");
                        foreach (FileInfo fi in f)
                        {

                            Console.WriteLine($"{fi.Name}[{i}]");
                            i++;

                        }
                        if (f.Length > 0)
                        {
                            Console.WriteLine("Bitte geben Sie den Index des Savefiles ein: ");
                            eingabe = Convert.ToInt32(Console.ReadLine()) - 1;
                            if (eingabe == -1)
                            {
                                Console.Clear();
                                return;
                            }
                            fs = f.ElementAt(eingabe).OpenRead();
                        }
                        else
                        {
                            Console.WriteLine("\nEs gibt keine Save Files!\n");
                            check = true;
                        }
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("\nDieser Index ist ungültig!!! Erneuter Versuch.\n");
                        i = 1;
                        check = false;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("\nFalsche Eingabe, erneuter Versuch!\n");
                        i = 1;
                        check = false;
                    }

                } while (check == false);
            }

            if (fs != null)
            {
                StreamReader sr = new StreamReader(fs);

                string[] zeilen = sr.ReadToEnd().Split('\n');
                string[] namen = new string[4];
                string[] farben = new string[4];
                int[,] positionen = new int[4, 4]; // 2d-Array für Positionen von (maximal) allen 4 Häusern
                int[,] printPositionen = new int[4, 4]; //auch 2d array
                bool[] angemeldet = new bool[4];

                for (int j = 1; j < zeilen.Length - 1; j++)//Durch jede Zeile gehen (außer Header und Ende, das Ende ist immer \n)
                {
                    string[] spalten = zeilen[j].Split('\t'); //aufteilen in Spalten
                    namen[j - 1] = spalten[0];
                    farben[j - 1] = spalten[1];
                    string[] posArray = spalten[2].Split(';');
                    string[] printPosArray = spalten[3].Split(";");
                    for (int k = 0; k < 4; k++) // 4, da jedes Haus 4 Figuren hat.
                    {
                        positionen[j - 1, k] = Convert.ToInt32(posArray[k]);
                        printPositionen[j - 1, k] = Convert.ToInt32(printPosArray[k]);
                    }
                    if (spalten.Count() == 5)
                    {
                        angemeldet[j - 1] = true;
                    }
                    else
                    {
                        angemeldet[j - 1] = false;
                    }
                }

                List<Spieler> spieler = new List<Spieler>();
                List<Haus> haeuser = new List<Haus>();
                for (int j = 0; j < zeilen.Length - 2; j++)  //-2, da sonst 1 Spieler / Haus zu viel hinzugefügt wird. Erstellung der Spieler, Häuser und Spielfiguren
                {
                    if (namen[j].Contains("bot")) //Wenn der Name bot enthält, wird ein Bot erstellt.
                    {
                        spieler.Add(new Bot());
                    }
                    else if (angemeldet[j])
                    {
                        spieler.Add(new AngemeldeterSpieler(namen[j]));
                    }
                    else
                    {
                        spieler.Add(new Menschlicher_Spieler(namen[j]));
                    }
                    Enum.TryParse(farben[j], out Verfuegbare_Farben farbe);
                    Haus h = new Haus(farbe);
                    h.ZugehoerigerSpieler = spieler.ElementAt(j);
                    for (int k = 0; k < 4; k++) //Weil es 4 Spielfiguren pro Haus gibt.
                    {
                        h.ZugehoerigeFiguren.ElementAt(k).Position = positionen[j, k];
                        h.ZugehoerigeFiguren.ElementAt(k).PrintPosition = printPositionen[j, k];
                        if (h.ZugehoerigeFiguren.ElementAt(k).PrintPosition != 0) //Wenn es eine andere PrintPosition als 0 hat, dann ist die Spielfigur nicht im Haus.
                        {
                            h.ZugehoerigeFiguren.ElementAt(k).IsInHouse = false;
                            h.FigurenImHaus--;
                        }


                    }
                    haeuser.Add(h);
                }

                for (int j = 0; j < 4 - haeuser.Count + 2; j++) //Erstellen der Häuser mit den bekommenen Daten 
                {
                    Haus h = new Haus(Verfuegbare_Farben.Weiss);
                    h.AuffuellHaus = true;
                    haeuser.Add(h);
                }
                sr.Close();
                fs.Close();
                if (cheat != 0)
                {
                    Logik.Spielablauf(haeuser, spieler, cheat);         //Dient dem Herzeigen von verschiedenen Szenarien
                }
                else
                {
                    Logik.Spielablauf(haeuser, spieler); //Mit den gerade Erstellten Spielern und Häusern das Spiel "starten"
                }
            }



        }
        public void SaveWins(AngemeldeterSpieler gewinner)
        {
            string d = Directory.GetCurrentDirectory();
            string path = d + @"/PlayerWins";
            Directory.CreateDirectory(path); //Kreieren eines neuen Verzeichnisses, für die Speicherung der Siege aller Spieler.
            FileStream fs = new FileStream(path + "/PlayerWins.txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            string inhalt = sr.ReadToEnd(); //Kurzerhand den derzeitigen Inhalt der File zu speichern.
            sr.Close();
            fs.Close();

            FileStream fs2 = new FileStream(path + "/PlayerWins.txt", FileMode.Open, FileAccess.Write); //Einen neuen Filestream erstellen, da sonst ein "Stream not writable" Fehler kommt.
            StreamWriter sw = new StreamWriter(fs2);

            if (inhalt == string.Empty) //Falls das File leer ist, dann den Header schreiben:
            {
                sw.WriteLine("Spieler\tSiege");
            }
            else //Wenn es einen Inhalt hat, dann leere das File.
            {
                fs2.SetLength(0);
            }
            string[] inhalt2 = inhalt.Split('\n'); //Alle Zeilen des Files
            string newInhalt = string.Empty;
            if (inhalt.Contains(gewinner.Name)) //Wenn der Inhalt den Namen des Gewinners schon beinhaltet, dann werden die Siege um 1 erhöht:
            {
                string line = string.Empty;
                foreach (string s in inhalt2)
                {

                    if (s.Contains(gewinner.Name)) //Falls man in der gewünschten Zeile ist (wo der Gewinner verzeichnet ist)
                    {
                        int siege = Convert.ToInt32(s.Split('\t')[1]); //die alten Siege ermitteln
                        line = s.Replace($"{siege}", $"{siege + 1}"); //Die alten Siege werden mit den neuen Siegen (alte Siege + 1) ersetzt
                    }
                    else //Sonst bleibt die Zeile gleich.
                    {
                        line = s;
                    }

                    newInhalt += line + '\n';

                }
            }
            else// Wenn der Sieger noch nicht im File ist, dann wird eine neue Zeile mit seinen Daten angelegt
            {
                newInhalt = inhalt + $"{gewinner.Name}\t1";
            }
            newInhalt = newInhalt.TrimEnd('\n');
            sw.WriteLine(newInhalt);//Das File wird neu beschrieben.

            sw.Close();
            fs2.Close();
        }

        public int GetWins(string name)
        {
            string d = Directory.GetCurrentDirectory();
            string path = d + @"/PlayerWins";
            FileStream fs = new FileStream(path + "/PlayerWins.txt", FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);

            string inhalt = sr.ReadToEnd();
            string[] inhalt2 = inhalt.Split('\n');

            foreach (string s in inhalt2)
            {
                if (s.Contains(name)) //Wenn die aktuelle Zeile den Namen beinhaltet, dann return die Anzahl der Siege
                {
                    sr.Close();
                    fs.Close();
                    return Convert.ToInt32(s.Split('\t')[1]); //[1] = Siege
                }
            }
            sr.Close();
            fs.Close();
            return 0;
        }
        public void LoescheSpielstand()
        {
            FileInfo[] f = GetAllSaveFiles();
            int i = 1;
            bool check = true;
            int eingabe = 0;

            do
            {
                try
                {
                    if (f.Length > 0)
                        Console.WriteLine("\nWieder zum Hauptmenü zurückkehren[0]");
                    foreach (FileInfo fi in f)
                    {
                        Console.WriteLine($"{fi.Name}[{i}]");
                        i++;

                    }
                    if (f.Length > 0)
                    {
                        Console.WriteLine("Bitte geben Sie den Index des Savefiles ein: ");
                        eingabe = Convert.ToInt32(Console.ReadLine()) - 1;
                        if (eingabe == -1)
                        {
                            Console.Clear();
                            return;
                        }
                    }



                }
                catch (Exception)
                {
                    Console.WriteLine("Falsche Eingabe, erneuter Versuch!");
                    i = 1;
                    check = false;
                }

            } while (check == false);
            try
            {
                File.Delete(f.ElementAt(eingabe).Name);
                Console.WriteLine($"Spielstand {f.ElementAt(eingabe).Name} erfolgreich gelöscht! Sie haben nun wieder Platz für {5 - f.Length + 1} Save Files!");
                Thread.Sleep(4000);
                Console.Clear();
            }
            catch (ArgumentOutOfRangeException) //Wenn es keine Files gibt / ungültiger Index.
            {
                Console.WriteLine("\nDieser Index ist nicht gültig oder es gibt keine Save Files!\n");

            }

        }

    }
}
