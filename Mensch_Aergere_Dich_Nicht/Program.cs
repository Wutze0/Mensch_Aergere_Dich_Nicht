namespace Mensch_Aergere_Dich_Nicht
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //julian, wenn du testen willst und nicht immer die Einf�hrung durchmachen willst, kannst du Zeile 8 auskommentieren.
            Einf�hrung();

        }



        private static void wuerfeln(Haus haus, Print print, List<Haus> haueser)
        {
            int ziehe = 0;
            bool erneutWuerfeln = true;
            bool botYesNo = haus.ZugehoerigerSpieler.BotYesNo;


            Random r = new Random();

            if (haus.FigurenImHaus < haus.ZiehbareFiguren) //normaler Spielablauf, ziehbareFiguren speichert die Anzahl der Figuren, die sich noch bewegen k�nnte und somit nicht am Ende angelangt sind
            {
                int sechsCounter = 0;
                while (erneutWuerfeln == true && sechsCounter <= 3) //Logik f�r, wenn jemand eine 6 w�rfelt //Man darf nur maximal drei mal hintereinander eine 6 w�rfeln
                {
                    ziehe = r.Next(1, 7);
                    Console.WriteLine($"{ziehe} gewuerfelt!");
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
                            if (haus.FigurenImHaus > 0 )
                            {

                                Console.WriteLine("Sie d�rfen eine Figur aus dem haus ziehen!");        //noch nicht fertig; muss nicht immer der Fall sein, dass man eine Figur rausziehen darf.
                                auswaehlen(haus, true, ziehe, print, haueser);


                            }
                            else
                            {

                                auswaehlen(haus, false, ziehe, print, haueser);

                            }
                        }


                        sechsCounter++;

                    }
                    else
                    {
                        erneutWuerfeln = false;
                    }
                        

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

                //maximal drei mal w�rfeln und maximal drei mal 6 w�rfeln
                //ausw�hlen welche figur GEMACHT
                //Console.WriteLine(ziehe);

            }
            else
            {

                for (int i = 0; i < 3 && ziehe != 6; i++) //Zu Beginn des Spiels darf man 3 mal w�rfeln, um eine Figur aus dem Haus zu bringen.
                {
                    ziehe = r.Next(1, 7);
                    Console.WriteLine($"{i + 1}. Wurf : {ziehe}");

                }
                if (ziehe == 6)
                {
                    if (botYesNo)
                    {
                        Bot x = haus.ZugehoerigerSpieler as Bot;
                        x.Spielfigurbewegen(haus, haueser, ziehe);
                    }
                    else
                    {
                        //haus.ZugehoerigeFiguren.ElementAt(1).PrintPosition = haus.StartingPrintPosition;
                        //haus.ZugehoerigeFiguren.ElementAt(1)._position = 1;
                        auswaehlen(haus, true, ziehe, print, haueser);
                    }

                }
                else
                {
                    Console.WriteLine("Leider keine 6 gewuerfelt\n");
                    Thread.Sleep(3000);
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
                Console.WriteLine("Welche Figur m�chten Sie ziehen? Verf�gbar: [1, 2, 3, 4]");
                eingabe = Convert.ToInt16(Console.ReadLine());
            }
            else
            {
                Console.WriteLine("M�chten Sie eine Figur aus dem Haus ziehen?");
                jaNein = Convert.ToBoolean(Console.ReadLine());
                eingabe = 4 - haus.FigurenImHaus;
            }

            if (rausziehen == true && jaNein == true && istFeldFrei(print, haus, eingabe, gewuerfelt))      //Hier kommt man hinein, wenn man eine Figur aus dem Haus ziehen darf und will
            {
                List<Spielfigur> Figuren = print.GetAllSpielfiguren();
                bool check = true;
                foreach (Spielfigur s in Figuren)
                {
                    if (s.PrintPosition == haus.StartingPrintPosition)
                    {
                        check = false;
                    }
                }
                if (check)
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
                else
                {
                    Console.WriteLine("Diese Figur kann nicht gezogen werden.");
                }


            }
            else if (rausziehen == false && jaNein == false)      //Hier kommt man hinein, wenn man eine Figur nur ziehen darf
            {
                int temp = eingabe - 1;

                if (haus.ZugehoerigeFiguren.ElementAt(temp).IsInHouse == false)
                {
                    Spielfigur aktuelleFigur = haus.ZugehoerigeFiguren.ElementAt(temp);
                    int letztesBefahrbaresFeld = haus.letztesBefahrbaresFeldBerechnen();
                    if (aktuelleFigur.Position <= 44 && aktuelleFigur.Position >= 41)                    //Im Haus fahren
                    {
                        List<int> positionen = new List<int>();
                        foreach (Spielfigur s in haus.ZugehoerigeFiguren)
                        {
                            if (s != aktuelleFigur)
                            {
                                positionen.Add(s.Position);
                            }
                        }
                        if (aktuelleFigur.Position > letztesBefahrbaresFeld)
                        {
                            Console.WriteLine("Diese Figur kann nicht gezogen werden.");                                            // Man k�nnte es so machen, dass immer wenn der Benutzer eine ungueltige Eingabe taetigt, eine Exception geworfen wird und am Ende ueberprueft wird, ob eine Exception geworfen worden ist.
                        }                                                                                                           //Wenn Ja darf der Benutzer nochmal auswaehlen
                        else
                        {
                            if (positionen.Contains(aktuelleFigur.Position + 1))
                            {
                                Console.WriteLine("Diese Figur kann nicht gezogen werden.");
                            }
                            else
                            {
                                int maximalZiehbareAnzahl;
                                int naehesteFigur = letztesBefahrbaresFeld;
                                foreach (int i in positionen)
                                {
                                    if (i > aktuelleFigur.Position && i < naehesteFigur)
                                    {
                                        naehesteFigur = i;
                                    }
                                }

                                maximalZiehbareAnzahl = naehesteFigur - aktuelleFigur.Position;

                                if (maximalZiehbareAnzahl >= gewuerfelt)
                                {
                                    aktuelleFigur.Position += gewuerfelt;
                                    aktuelleFigur.PrintPosition = aktuelleFigur.Position + gewuerfelt + ((haus.HausID - 1) * 4);
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
                        bool check = true;
                        

                        if (aktuelleFigur.Position + gewuerfelt > 40)
                        {
                            if (aktuelleFigur.Position + gewuerfelt <= haus.letztesMoeglichesFeldBeimReinfahrenberechnen())
                            {
                                aktuelleFigur.Position += gewuerfelt;
                                aktuelleFigur.PrintPosition = 40 + (aktuelleFigur.Position % 40) + 4 * (haus.HausID - 1);
                            }
                            else
                            {
                                foreach (Spielfigur s in haus.ZugehoerigeFiguren)
                                {
                                    if (s.Position == aktuelleFigur.Position % 40 && s != aktuelleFigur)
                                    {
                                        check = false;
                                    }
                                }

                                if (check)
                                {
                                    aktuelleFigur.Position += gewuerfelt;
                                    if(aktuelleFigur.Position > 40)
                                    {
                                        aktuelleFigur.Position %= 40;
                                    }
                                    aktuelleFigur.PrintPosition += gewuerfelt;
                                    if (aktuelleFigur.PrintPosition > 40)
                                    {
                                        aktuelleFigur.PrintPosition %= 40;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Diese Figur kann nicht gezogen werden.");
                                }
                            }
                        }
                        else
                        {
                            aktuelleFigur.Position += gewuerfelt;
                            if (aktuelleFigur.Position > 40)
                            {
                                aktuelleFigur.Position %= 40;
                            }
                            aktuelleFigur.PrintPosition += gewuerfelt;
                            if (aktuelleFigur.PrintPosition > 40)
                            {
                                aktuelleFigur.PrintPosition %= 40;
                            }

                        }

                        if (check)
                        {
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
                else
                {
                    Console.WriteLine("Diese Figur kann nicht gezogen werden.");
                }


            }
            //Man kommt hier hin wenn man eine 6 w�rfelt und rausziehen K�NNTE, aber nicht will.
            else
            {
                Console.WriteLine($"Ziehe {gewuerfelt} Felder mit einer Figur!");
                Console.WriteLine("Welche Figur m�chten Sie ziehen? Verf�gbar: [1, 2, 3, 4]");
                eingabe = Convert.ToUInt16(Console.ReadLine());
                int temp = eingabe - 1;


                if (haus.ZugehoerigeFiguren.ElementAt(temp).IsInHouse == false)
                {
                    Spielfigur aktuelleFigur = haus.ZugehoerigeFiguren.ElementAt(temp);
                    int letztesBefahrbaresFeld = haus.letztesBefahrbaresFeldBerechnen();
                    if (aktuelleFigur.Position <= 44 && aktuelleFigur.Position >= 41)                    //Im Haus fahren
                    {
                        List<int> positionen = new List<int>();
                        foreach (Spielfigur s in haus.ZugehoerigeFiguren)
                        {
                            if (s != aktuelleFigur)
                            {
                                positionen.Add(s.Position);
                            }
                        }
                        if (aktuelleFigur.Position > letztesBefahrbaresFeld)
                        {
                            Console.WriteLine("Diese Figur kann nicht gezogen werden.");                                            // Man k�nnte es so machen, dass immer wenn der Benutzer eine ungueltige Eingabe taetigt, eine Exception geworfen wird und am Ende ueberprueft wird, ob eine Exception geworfen worden ist.
                        }                                                                                                           //Wenn Ja darf der Benutzer nochmal auswaehlen
                        else
                        {
                            if (positionen.Contains(aktuelleFigur.Position + 1))
                            {
                                Console.WriteLine("Diese Figur kann nicht gezogen werden.");
                            }
                            else
                            {
                                int maximalZiehbareAnzahl;
                                int naehesteFigur = letztesBefahrbaresFeld;
                                foreach (int i in positionen)
                                {
                                    if (i > aktuelleFigur.Position && i < naehesteFigur)
                                    {
                                        naehesteFigur = i;
                                    }
                                }

                                maximalZiehbareAnzahl = naehesteFigur - aktuelleFigur.Position;

                                if (maximalZiehbareAnzahl >= gewuerfelt)
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

                        if (aktuelleFigur.Position > 40)
                        {
                            if (aktuelleFigur.Position <= haus.letztesMoeglichesFeldBeimReinfahrenberechnen())
                            {
                                aktuelleFigur.PrintPosition = 40 + (aktuelleFigur.Position % 40) + 4 * (haus.HausID - 1);
                            }
                            else
                            {
                                aktuelleFigur.Position %= 40;
                                if (aktuelleFigur.PrintPosition > 40)
                                {
                                    aktuelleFigur.PrintPosition %= 40;
                                }
                            }
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
                    if (x.PrintPosition == (gezogeneSpielfigur.PrintPosition + gewuerfelt) % 40)
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

        private static void Einf�hrung()
        {
            while (true)
            {
                Console.WriteLine(
                  "Mensch �rgere Dich Nicht!" +
                  "\n" +
                  "\n" +
                  "\n[1]Neues Spiel" +
                  "\n[2]Spiel laden" +
                  "\n[3]Spiel l�schen" +
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
                Console.WriteLine("Bitte geben Sie die Anzahl der Spieler ein(1-4):");
                string eingabe = Console.ReadLine();
                try
                {
                    spielerzahl = Convert.ToInt32(eingabe);

                }
                catch (Exception e)
                {
                    Console.WriteLine("Falsche Eingabe... erneuter Versuch:");
                }
            } while (spielerzahl != 1 &&spielerzahl != 2 && spielerzahl != 3 && spielerzahl != 4);

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
                } while (botAnzahl < 0 || botAnzahl > (4 - spielerzahl));

                if (botAnzahl > 0)
                    bot = true;
            }

            List<Spieler> spielerliste = new List<Spieler>();
            List<Haus> haeuser = new List<Haus>();

            for (int i = 0; i < spielerzahl; i++)
            {
                Console.WriteLine($"Bitte den Namen des {i + 1}. Spielers eingeben:");
                string name = Console.ReadLine();
                Console.WriteLine($"\n{name}, Bitte geben Sie Ihre gew�nschte Hausfarbe ein\n" +
                    $"Verf�gbar sind folgende:\n{getAvailableColors(haeuser)}");
                bool check = true;
                while (check == true)
                {

                    string gewuenschteFarbe = Console.ReadLine();
                    Verfuegbare_Farben farbe = Verfuegbare_Farben.Rot;
                    if (Enum.TryParse(gewuenschteFarbe, out farbe))
                    {
                        if (getAvailableColors(haeuser).Contains(gewuenschteFarbe))
                        {
                            Console.WriteLine($"Erfolgreich die Farbe {farbe} ausgew�hlt!");
                            haeuser.Add(new Haus(farbe));
                            check = false;
                        }
                        else
                        {
                            Console.WriteLine("Diese Farbe wird schon verwendet, bitte eine andere Farbe w�hlen");
                        }

                    }
                    else
                    {
                        Console.WriteLine("Diese Farbe existiert nicht, bitte eine andere Farbe w�hlen:");
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
            Console.Clear();
            Print p = new Print(haeuser);
            p.PrintSpielfeld();
            bool win = false;
            int abtauschen = 0;
            string timestamp = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
            string path = $"SaveFile_{timestamp}.txt";
            while (!win)
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
                            p.PrintSpielfeld();
                        }
                        haeuser.ElementAt(abtauschen).ziehbareFigurenBerechnen();
                        if (haeuser.ElementAt(abtauschen).ZiehbareFiguren == 0)
                        {
                            win = true;
                        }
                        abtauschen++;
                        break;

                    case 1:

                        if (!haeuser.ElementAt(abtauschen).AuffuellHaus)
                        {
                            wuerfeln(haeuser.ElementAt(abtauschen), p, haeuser);
                            p.PrintSpielfeld();
                        }
                        haeuser.ElementAt(abtauschen).ziehbareFigurenBerechnen();
                        if (haeuser.ElementAt(abtauschen).ZiehbareFiguren == 0)
                        {
                            win = true;
                        }
                        abtauschen++;
                        break;
                    case 2:

                        if (!haeuser.ElementAt(abtauschen).AuffuellHaus)
                        {
                            wuerfeln(haeuser.ElementAt(abtauschen), p, haeuser);
                            p.PrintSpielfeld();
                        }
                        haeuser.ElementAt(abtauschen).ziehbareFigurenBerechnen();
                        if (haeuser.ElementAt(abtauschen).ZiehbareFiguren == 0)
                        {
                            win = true;
                        }
                        abtauschen++;
                        break;

                    case 3:
                        if (!haeuser.ElementAt(abtauschen).AuffuellHaus)
                        {
                            wuerfeln(haeuser.ElementAt(abtauschen), p, haeuser);
                            p.PrintSpielfeld();
                        }
                        haeuser.ElementAt(abtauschen).ziehbareFigurenBerechnen();
                        if (haeuser.ElementAt(abtauschen).ZiehbareFiguren == 0)
                        {
                            win = true;
                        }
                        abtauschen = 0;
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
                        Console.WriteLine("Aktuelles Spielfeld:");
                        p.PrintSpielfeld();
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
        //f�r gewinn�berpr�fung eventuell: jedes haus hat ja 4 felder wo eine figur reingehen muss, also f�r jedes haus einen positionsArray machen.

        public static void SpielSpeichern(List<Spieler> spielerliste, List<Haus> haeuser, string path)
        {


            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);

            string header = "Spieler\tHausfarbe\tZugeh�rige Spielfiguren\n";
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

                    sw.WriteLine(string.Join(";", figurenPositionen)); //zwischen jedem Element in figurenPositionen wird ein ; eingef�gt
                }
            sw.Close();
            fs.Close();

            Console.WriteLine($"Spielstand wurde in {path} gespeichert.");
        }

        private static void LadeSpiel()
        {
            DirectoryInfo d = new DirectoryInfo(""); //muss noch ge�ndert werden
            FileInfo[] Files = d.GetFiles("*.txt");
            string s = string.Empty;

            foreach(FileInfo f in Files )
            {
                s += f.Name;
            }
            Console.WriteLine(s);
        }

    }

}


