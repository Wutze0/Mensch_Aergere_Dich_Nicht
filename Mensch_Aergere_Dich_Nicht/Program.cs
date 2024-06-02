namespace Mensch_Aergere_Dich_Nicht
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //julian, wenn du testen willst und nicht immer die Einführung durchmachen willst, kannst du Zeile 8 auskommentieren.
            Einführung();

        }



        private static void wuerfeln(Haus haus, Print print, List<Haus> haueser)
        {
            int ziehe = 0;
            bool erneutWuerfeln = true;
            bool botYesNo = haus.ZugehoerigerSpieler.BotYesNo;


            Random r = new Random();

            if (haus.FigurenImHaus < haus.ZiehbareFiguren) //normaler Spielablauf, ziehbareFiguren speichert die Anzahl der Figuren, die sich noch bewegen könnte und somit nicht am Ende angelangt sind
            {
                int sechsCounter = 0;
                while (erneutWuerfeln == true && sechsCounter <= 3) //Logik für, wenn jemand eine 6 würfelt //Man darf nur maximal drei mal hintereinander eine 6 würfeln
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
                            print.PrintSpielfeld();
                        }
                        else
                        {
                            if (haus.FigurenImHaus > 0)
                            {

                                auswaehlen(haus, true, ziehe, print, haueser);
                                print.PrintSpielfeld();


                            }
                            else
                            {

                                auswaehlen(haus, false, ziehe, print, haueser);
                                print.PrintSpielfeld();

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
                    print.PrintSpielfeld();
                }

                //maximal drei mal würfeln und maximal drei mal 6 würfeln
                //auswählen welche figur GEMACHT
                //Console.WriteLine(ziehe);

            }
            else
            {

                for (int i = 0; i < 3 && ziehe != 6; i++) //Zu Beginn des Spiels darf man 3 mal würfeln, um eine Figur aus dem Haus zu bringen.
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
                        print.PrintSpielfeld();
                        wuerfeln(haus, print, haueser);
                    }
                    else
                    {
                        //haus.ZugehoerigeFiguren.ElementAt(1).PrintPosition = haus.StartingPrintPosition;
                        //haus.ZugehoerigeFiguren.ElementAt(1)._position = 1;
                        auswaehlen(haus, true, ziehe, print, haueser);
                        print.PrintSpielfeld();
                        wuerfeln(haus, print, haueser);
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
            bool falscheEingabe = true;
            bool wiederholen;
            while (falscheEingabe)
            {
                bool jaNein = false;
                int eingabe = 0;
                falscheEingabe = false;
                try
                {
                    if (haus.NichtsBewegbar(gewuerfelt))
                    {
                        throw new KeinZugMoeglichException("Es kann keine Figur gezogen werden");
                    }
                    if (!rausziehen)
                    {
                        Console.WriteLine($"Ziehe {gewuerfelt} Felder mit einer Figur!");
                        Console.WriteLine("Welche Figur möchten Sie ziehen? Verfügbar: [1, 2, 3, 4]");
                        wiederholen = true;
                        while (wiederholen)
                        {
                            wiederholen = false;
                            try
                            {
                                eingabe = Convert.ToInt16(Console.ReadLine());
                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine("Bitte etwas eingeben");
                                wiederholen = true;
                            }
                            catch (OverflowException e)
                            {
                                Console.WriteLine("Bitte weniger eingeben");
                                wiederholen = true;
                            }
                        }

                        if (eingabe > 4 || eingabe < 1)
                        {
                            throw new UserFalscheEingabeException("Diese Figur gibt es nicht");
                        }
                    }
                    else
                    {

                        Console.WriteLine("Möchten Sie eine Figur aus dem Haus ziehen?[Ja oder Nein]");
                        string? temp = Console.ReadLine();
                        if (temp == "Ja" || temp == "ja")
                        {
                            jaNein = true;
                        }
                        else if (temp == "Nein" || temp == "nein")
                        {
                            jaNein = false;
                        }
                        else
                        {
                            throw new UserFalscheEingabeException("Das ist keine gültige Eingabe");
                        }


                    }

                    if (rausziehen == true && jaNein == true)      //Hier kommt man hinein, wenn man eine Figur aus dem Haus ziehen darf und will
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
                            Spielfigur? aktuelleFigur = null;

                            foreach (Spielfigur s in haus.ZugehoerigeFiguren)
                            {
                                if (s.IsInHouse == true && einmal == true)
                                {
                                    haus.ZugehoerigeFiguren.ElementAt(i).PrintPosition = haus.StartingPrintPosition;
                                    haus.ZugehoerigeFiguren.ElementAt(i).IsInHouse = false;
                                    haus.ZugehoerigeFiguren.ElementAt(i).Position = 1;
                                    einmal = false;
                                    aktuelleFigur = haus.ZugehoerigeFiguren.ElementAt(i);
                                }
                                i++;
                            }
                            if (aktuelleFigur != null)
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
                            else
                            {
                                haus.FigurenImHaus++;
                                throw new UserFalscheEingabeException("Es ist keine Figur mehr im Haus");
                            }

                        }
                        else
                        {
                            throw new UserFalscheEingabeException("Es kann keine Figur aus dem Haus gezogen werden");
                        }


                    }
                    else if (rausziehen == false && jaNein == false)      //Hier kommt man hinein, wenn man eine Figur nur ziehen darf
                    {
                        int temp = eingabe - 1;

                        if (haus.ZugehoerigeFiguren.ElementAt(temp).IsInHouse == false)
                        {
                            Spielfigur aktuelleFigur = haus.ZugehoerigeFiguren.ElementAt(temp);
                            int letztesBefahrbaresFeld = haus.LetztesBefahrbaresFeldBerechnen();
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
                                    throw new UserFalscheEingabeException("Diese Figur kann nicht gezogen werden");
                                }                                                                                                           //Wenn Ja darf der Benutzer nochmal auswaehlen
                                else
                                {
                                    if (positionen.Contains(aktuelleFigur.Position + 1))
                                    {
                                        throw new UserFalscheEingabeException("Diese Figur kann nicht gezogen werden");
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
                                            aktuelleFigur.PrintPosition = aktuelleFigur.Position + ((haus.HausID - 1) * 4);
                                        }
                                        else
                                        {
                                            throw new UserFalscheEingabeException("Diese Figur kann nicht gezogen werden");
                                        }
                                    }
                                }
                            }
                            else                                                                                                            //Ins Haus fahren oder normal fahren
                            {
                                bool check = true;


                                if (aktuelleFigur.Position + gewuerfelt > 40)
                                {
                                    if (aktuelleFigur.Position + gewuerfelt <= haus.LetztesMoeglichesFeldBeimReinfahrenberechnen())
                                    {
                                        aktuelleFigur.Position += gewuerfelt;
                                        aktuelleFigur.PrintPosition = 40 + (aktuelleFigur.Position % 40) + 4 * (haus.HausID - 1);
                                    }
                                    else
                                    {
                                        foreach (Spielfigur s in haus.ZugehoerigeFiguren)
                                        {
                                            if (s.Position == (aktuelleFigur.Position + gewuerfelt) % 40 && s != aktuelleFigur)
                                            {
                                                check = false;
                                            }
                                        }

                                        if (check)
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
                                        else
                                        {
                                            throw new UserFalscheEingabeException("Diese Figur kann nicht gezogen werden");
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (Spielfigur s in haus.ZugehoerigeFiguren)
                                    {
                                        if (s.Position == (aktuelleFigur.Position + gewuerfelt) && s != aktuelleFigur)
                                        {
                                            check = false;
                                        }
                                    }
                                    if (check)
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
                                    else
                                    {
                                        throw new UserFalscheEingabeException("Diese Figur kann nicht gezogen werden");
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
                            throw new UserFalscheEingabeException("Diese Figur kann nicht gezogen werden");
                        }


                    }
                    //Man kommt hier hin wenn man eine 6 würfelt und rausziehen KÖNNTE, aber nicht will.
                    else
                    {
                        Console.WriteLine($"Ziehe {gewuerfelt} Felder mit einer Figur!");
                        Console.WriteLine("Welche Figur möchten Sie ziehen? Verfügbar: [1, 2, 3, 4]");
                        eingabe = Convert.ToUInt16(Console.ReadLine());
                        if (eingabe > 4 || eingabe < 1)
                        {
                            throw new UserFalscheEingabeException("Diese Figur gibt es nicht");
                        }
                        int temp = eingabe - 1;


                        if (haus.ZugehoerigeFiguren.ElementAt(temp).IsInHouse == false)
                        {
                            Spielfigur aktuelleFigur = haus.ZugehoerigeFiguren.ElementAt(temp);
                            int letztesBefahrbaresFeld = haus.LetztesBefahrbaresFeldBerechnen();
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
                                    throw new UserFalscheEingabeException("Diese Figur kann nicht gezogen werden");
                                }
                                else
                                {
                                    if (positionen.Contains(aktuelleFigur.Position + 1))
                                    {
                                        throw new UserFalscheEingabeException("Diese Figur kann nicht gezogen werden");
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
                                            throw new UserFalscheEingabeException("Diese Figur kann nicht gezogen werden");
                                        }
                                    }
                                }
                            }
                            else
                            {
                                bool check = true;


                                if (aktuelleFigur.Position + gewuerfelt > 40)
                                {
                                    if (aktuelleFigur.Position + gewuerfelt <= haus.LetztesMoeglichesFeldBeimReinfahrenberechnen())
                                    {
                                        aktuelleFigur.Position += gewuerfelt;
                                        aktuelleFigur.PrintPosition = 40 + (aktuelleFigur.Position % 40) + 4 * (haus.HausID - 1);
                                    }
                                    else
                                    {
                                        foreach (Spielfigur s in haus.ZugehoerigeFiguren)
                                        {
                                            if (s.Position == (aktuelleFigur.Position + gewuerfelt) % 40 && s != aktuelleFigur)
                                            {
                                                check = false;
                                            }
                                        }

                                        if (check)
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
                                        else
                                        {
                                            throw new UserFalscheEingabeException("Diese Figur kann nicht gezogen werden");
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (Spielfigur s in haus.ZugehoerigeFiguren)
                                    {
                                        if (s.Position == (aktuelleFigur.Position + gewuerfelt) && s != aktuelleFigur)
                                        {
                                            check = false;
                                        }
                                    }
                                    if (check)
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
                                    else
                                    {
                                        throw new UserFalscheEingabeException("Diese Figur kann nicht gezogen werden");
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

                    }
                }
                catch (KeinZugMoeglichException e)
                {
                    Console.WriteLine(e.Message);
                    Thread.Sleep(5000);
                }
                catch (UserFalscheEingabeException e)
                {
                    falscheEingabe = true;
                    Console.WriteLine(e.Message);
                }
            }


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
                Console.WriteLine("Bitte geben Sie die Anzahl der Spieler ein(1-4):");
                string? eingabe = Console.ReadLine();
                try
                {
                    spielerzahl = Convert.ToInt32(eingabe);

                }
                catch (Exception e)
                {
                    Console.WriteLine("Falsche Eingabe... erneuter Versuch:");
                }
            } while (spielerzahl != 1 && spielerzahl != 2 && spielerzahl != 3 && spielerzahl != 4);

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

                    string? gewuenschteFarbe = Console.ReadLine();
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
                if (getAvailableColors(haeuser).Contains(farbe.ToString()))
                {
                    Console.WriteLine(farbe);
                    haeuser.Add(new Haus(farbe));
                    haeuser.ElementAt(i + spielerzahl).ZugehoerigerSpieler = new Bot();
                    spielerliste.Add(haeuser.ElementAt(i + spielerzahl).ZugehoerigerSpieler);
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
            foreach (Spieler s in spielerliste)
            {
                Console.WriteLine($"Spieler {j + 1}: {s.Name} mit {haeuser.ElementAt(j).Farbe} als Farbe des Hauses.");
                j++;
            }

            Thread.Sleep(3000);
            Spielablauf(haeuser, spielerliste);

        }

        private static void Spielablauf(List<Haus> haeuser, List<Spieler> spieler)
        {
            Console.Clear();
            Print p = new Print(haeuser);
            Spieler? gewinner = null;
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
                        haeuser.ElementAt(abtauschen).ZiehbareFigurenBerechnen();
                        if (haeuser.ElementAt(abtauschen).ZiehbareFiguren == 0)
                        {
                            win = true;
                            gewinner = haeuser.ElementAt(abtauschen).ZugehoerigerSpieler;
                        }
                        abtauschen++;
                        break;

                    case 1:

                        if (!haeuser.ElementAt(abtauschen).AuffuellHaus)
                        {
                            wuerfeln(haeuser.ElementAt(abtauschen), p, haeuser);
                            p.PrintSpielfeld();
                        }
                        haeuser.ElementAt(abtauschen).ZiehbareFigurenBerechnen();
                        if (haeuser.ElementAt(abtauschen).ZiehbareFiguren == 0)
                        {
                            win = true;
                            gewinner = haeuser.ElementAt(abtauschen).ZugehoerigerSpieler;
                        }
                        abtauschen++;
                        break;
                    case 2:

                        if (!haeuser.ElementAt(abtauschen).AuffuellHaus)
                        {
                            wuerfeln(haeuser.ElementAt(abtauschen), p, haeuser);
                            p.PrintSpielfeld();
                        }
                        haeuser.ElementAt(abtauschen).ZiehbareFigurenBerechnen();
                        if (haeuser.ElementAt(abtauschen).ZiehbareFiguren == 0)
                        {
                            win = true;
                            gewinner = haeuser.ElementAt(abtauschen).ZugehoerigerSpieler;
                        }
                        abtauschen++;
                        break;

                    case 3:
                        if (!haeuser.ElementAt(abtauschen).AuffuellHaus)
                        {
                            wuerfeln(haeuser.ElementAt(abtauschen), p, haeuser);
                            //p.PrintSpielfeld();
                        }
                        haeuser.ElementAt(abtauschen).ZiehbareFigurenBerechnen();
                        if (haeuser.ElementAt(abtauschen).ZiehbareFiguren == 0)
                        {
                            win = true;
                            gewinner = haeuser.ElementAt(abtauschen).ZugehoerigerSpieler;
                        }
                        abtauschen = 0;
                        Console.WriteLine("Wollen Sie das Spiel speichern? [y/n]");
                        char eingabe = '/0';                                             
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
            Console.WriteLine("\n\n\n\n\n");

            Console.WriteLine("-------------------------------------\n\n");
            Console.WriteLine($"Der Spieler {gewinner.Name} hat Gewonnen!!!!\n\n");
            Console.WriteLine("-------------------------------------");


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

        private static void LadeSpiel()
        {
            DirectoryInfo d = new DirectoryInfo(""); //muss noch geändert werden
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


