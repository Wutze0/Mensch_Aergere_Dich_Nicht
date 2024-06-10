using System.Text.RegularExpressions;

namespace Mensch_Aergere_Dich_Nicht
{

    internal class Spiellogik
    {
        public Speicherrung Speicherung { get; set; }
        public Spiellogik()
        {
        }

        private void wuerfeln(Haus haus, Print print, List<Haus> haueser)
        {
            int ziehe = 0;
            bool erneutWuerfeln = true;
            bool botYesNo = haus.ZugehoerigerSpieler.BotYesNo;


            Random r = new Random();

            if (haus.FigurenImHaus < haus.ZiehbareFiguren) //normaler Spielablauf, ziehbareFiguren speichert die Anzahl der Figuren, die sich noch bewegen könnte und somit nicht am Ende angelangt sind
            {
                int sechsCounter = 0;
                while (erneutWuerfeln == true && sechsCounter < 3) //Logik für, wenn jemand eine 6 würfelt //Man darf nur maximal drei mal hintereinander eine 6 würfeln
                {
                    ziehe = r.Next(1, 7);
                    Console.WriteLine($"{ziehe} gewuerfelt!");
                    if (ziehe == 6)
                    {

                        erneutWuerfeln = true;
                        if (botYesNo)                                                               //Wenn der Spieler ein Bot ist, dann wird eine andere Funktion aufgerufen
                        {
                            Thread.Sleep(3000);
                            Bot x = haus.ZugehoerigerSpieler as Bot;
                            x.Spielfigurbewegen(haus, haueser, ziehe);
                            print.PrintSpielfeld();                                                 //Nach jedem Zug wird das Spielfeld nochmal ausgegeben
                            Thread.Sleep(3000);
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
                    Thread.Sleep(3000);
                    Bot x = haus.ZugehoerigerSpieler as Bot;
                    x.Spielfigurbewegen(haus, haueser, ziehe);
                    print.PrintSpielfeld();
                    Thread.Sleep(3000);
                }
                else
                {
                    auswaehlen(haus, false, ziehe, print, haueser);
                    print.PrintSpielfeld();
                }

                //maximal drei mal würfeln und maximal drei mal 6 würfeln


            }
            else
            {

                for (int i = 0; i < 3 && ziehe != 6; i++) //Wenn man keine Figur mehr ziehen könnte, darf man 3 mal würfeln, um eine Figur aus dem Haus zu bringen.
                {
                    ziehe = r.Next(1, 7);
                    Console.WriteLine($"{i + 1}. Wurf : {ziehe}");

                }
                if (ziehe == 6)
                {
                    if (botYesNo)
                    {
                        Thread.Sleep(3000);
                        Bot x = haus.ZugehoerigerSpieler as Bot;
                        x.Spielfigurbewegen(haus, haueser, ziehe);
                        print.PrintSpielfeld();
                        wuerfeln(haus, print, haueser);
                        Thread.Sleep(3000);
                    }
                    else
                    {
                        auswaehlen(haus, true, ziehe, print, haueser);
                        print.PrintSpielfeld();
                        wuerfeln(haus, print, haueser);
                    }

                }
                else
                {
                    Console.WriteLine("Leider keine 6 gewuerfelt\n");
                    Thread.Sleep(3000);
                    print.PrintSpielfeld();
                }


            }

        }

        private void auswaehlen(Haus haus, bool rausziehen, int gewuerfelt, Print print, List<Haus> haeuser) //rausziehen bestimmt, ob man eine Figur rausziehen darf oder nicht
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
                        if (haus.FigurenImHaus > 0 && gewuerfelt == 6)                                                           //Sollte die Nichts Bewegbar Methode erkennen, dass keine Figur gezogen werden kann, wird noch gefragt ob vielleicht eine Figur aus dem haus ziehen könnte, wenn ja ist ein Zug möglich und es wird keine Exception geworfen
                        {

                        }
                        else
                        {
                            throw new KeinZugMoeglichException("Es kann keine Figur gezogen werden");                           //Wenn tatsächlich keine Figur gezogen werden kann, wird eine Exception geworfen
                        }

                    }

                    if (rausziehen)                                                                                              //Wenn der Benutzer eine Figur aus dem Haus ziehen könnte, wird er gefragt ob er das auch will
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
                        else if (temp == "win")
                        {
                            int temptemp = 41;
                            foreach (Spielfigur s in haus.ZugehoerigeFiguren)
                            {
                                s.Position = temptemp;
                                temptemp++;
                            }
                            break;
                        }
                        else
                        {
                            throw new UserFalscheEingabeException("Das ist keine gültige Eingabe");
                        }
                    }

                    if (!rausziehen || !jaNein)                                                                                  //Diese if wird betreten, wenn der Spieler entweder nicht rausziehen kann oder nicht will
                    {
                        if (rausziehen && haus.ZiehbareFiguren == haus.FigurenImHaus)                                           //Wenn der Spieler eine Figur rausziehen könnte und keine andere ziehen kann, wird diese Exception geworfen
                        {
                            throw new UserFalscheEingabeException("Es muss eine Figur aus dem Haus gezogen werden");
                        }
                        Console.WriteLine($"Ziehe {gewuerfelt} Felder mit einer Figur!");
                        Console.WriteLine("Welche Figur möchten Sie ziehen? Verfügbar: [1, 2, 3, 4]");
                        wiederholen = true;
                        while (wiederholen)                                                                                     //Der Spieler darf hier eine Figur auswählen die er bewegen möchte
                        {
                            wiederholen = false;
                            try
                            {
                                eingabe = Convert.ToInt16(Console.ReadLine());
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Bitte etwas eingeben");
                                wiederholen = true;
                            }
                            catch (OverflowException)
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

                    if (rausziehen == true && jaNein == true)      //Hier kommt man hinein, wenn man eine Figur aus dem Haus ziehen darf und will
                    {
                        List<Spielfigur> Figuren = print.GetAllSpielfiguren();
                        bool check = true;
                        foreach (Spielfigur s in Figuren)
                        {
                            if (s.PrintPosition == haus.StartingPrintPosition && s.Farbe == haus.Farbe)                                                  //Hier wird ueberprueft ob eine eigene Figur am Startfeld steht, wenn ja ist der Zug nämlich ungültig
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

                            foreach (Spielfigur s in haus.ZugehoerigeFiguren)                                                   //Hier wird die Figur ermittelt, die aus dem Haus gezogen werden soll(immer die mit der niedrigsten ID)
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
                                foreach (Haus h in haeuser)                                                                     //Wenn eine gegnerische Figur auf dem Startfeld dieses Hauses steht wird sie jetzt geschlagen
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
                                rausziehen = false;
                                throw new UserFalscheEingabeException("Es ist keine Figur mehr im Haus");
                            }

                        }
                        else
                        {
                            rausziehen = false;
                            throw new UserFalscheEingabeException("Es kann keine Figur aus dem Haus gezogen werden");
                        }


                    }
                    else      //Hier kommt man hinein, wenn man eine Figur nur ziehen darf
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
                                if (aktuelleFigur.Position > letztesBefahrbaresFeld)                                                        //Es wird geschaut ob die ausgewählte Figur schon ganz am Ende ist
                                {
                                    throw new UserFalscheEingabeException("Diese Figur kann nicht gezogen werden");
                                }                                                                                                           //Wenn Ja darf der Benutzer nochmal auswaehlen
                                else
                                {
                                    if (positionen.Contains(aktuelleFigur.Position + 1))                                                    //Es wird geschaut, ob eine Figur vor der Figur steht, die man ziehen will
                                    {
                                        throw new UserFalscheEingabeException("Diese Figur kann nicht gezogen werden");
                                    }
                                    else
                                    {                                                                                                       //Es wird geschaut, wie viele Felder diese Figur maximal ziehen
                                        int maximalZiehbareAnzahl;
                                        int naehesteFigur = letztesBefahrbaresFeld + 1;                                                         //Wenn das Haus leer ist, wird angenommen die naeheste Figur sei am Feld 45. Dies dient nur der Berechnung
                                        foreach (int i in positionen)
                                        {
                                            if (i > aktuelleFigur.Position && i < naehesteFigur)
                                            {
                                                naehesteFigur = i;
                                            }
                                        }

                                        maximalZiehbareAnzahl = naehesteFigur - aktuelleFigur.Position;
                                        maximalZiehbareAnzahl--;

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
                                    if (aktuelleFigur.Position + gewuerfelt <= haus.LetztesMoeglichesFeldBeimReinfahrenberechnen())         //Wenn die Figur nach dem Zug im Haus wäre(aktuelleFigur.Position + gewürfelt > 40) und das bezogene Feld kleiner ist als das Feld der Funktion dann wird die Figur ins Haus gezogen
                                    {
                                        aktuelleFigur.Position += gewuerfelt;
                                        aktuelleFigur.PrintPosition = 40 + (aktuelleFigur.Position % 40) + 4 * (haus.HausID - 1);
                                    }
                                    else
                                    {                                                                                                       //Sonst zieht die Figur am haus vorbei
                                        foreach (Spielfigur s in haus.ZugehoerigeFiguren)
                                        {
                                            if (s.Position == (aktuelleFigur.Position + gewuerfelt) % 40 && s != aktuelleFigur)             //Wenn das Feld wo man hinziehen will blockiert ist, dann wird eine exception geworfen     
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
                                {                                                                                                           //Hier kommt man hin, wenn die Figur weder ins Haus zieht, noch an ihm vorbei zieht
                                    foreach (Spielfigur s in haus.ZugehoerigeFiguren)
                                    {
                                        if (s.Position == (aktuelleFigur.Position + gewuerfelt) && s != aktuelleFigur)                      //Wenn das Feld wo man hinziehen will blockiert ist, dann wird eine exception geworfen
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

                                if (check)                                                                                      //Hier wird noch geschaut, ob sich auf dem Feld wo die Figur hingezogen ist eine Figur befindet
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
                }
                catch (KeinZugMoeglichException e)
                {
                    Console.WriteLine(e.Message);
                    Thread.Sleep(5000);
                }
                catch (UserFalscheEingabeException e)
                {
                    falscheEingabe = true;                                              //Wenn so eine Exception auftritt wird immer der ganze Zug wiederholt
                    Console.WriteLine(e.Message);
                }
            }


        }

        public void Einführung()
        {
            bool stop = false;

            while (!stop)
            {                                                                                               //Einführung. Hier wird der Benutzer gefragt, was er tun möchte
                Haus.NumberOfHouses = 0;
                Bot.ID = 0;
                Console.WriteLine(
                  "Mensch Ärgere Dich Nicht!" +
                  "\n" +
                  "\n" +
                  "\n[1]Neues Spiel" +
                  "\n[2]Spielstand laden" +
                  "\n[3]Spielstand löschen" +
                  "\n"
                  );

                string eingabe = string.Empty;
                eingabe = Console.ReadLine();

                try
                {
                    switch (eingabe)
                    {
                        case "1": EinleitungNeuesSpiel(false); break;
                        case "2": Speicherung.LadeSpiel(); break;
                        case "3": Speicherung.LoescheSpielstand(); break;
                        case "4": if (Console.ReadLine() == "cheat") { EinleitungNeuesSpiel(true); } else { Console.WriteLine("Falsche Eingabe... erneuter Versuch"); }; break;
                        case "": stop = true; break;
                        default: Console.WriteLine("Falsche Eingabe... erneuter Versuch: "); break;

                    }
                }
                catch (UserFalscheEingabeException)
                {
                    Console.WriteLine("Falsche Eingabe... erneuter Versuch:");
                }

            }

            Console.WriteLine("Vielen Dank fürs Spielen!!!");

        }
        private void EinleitungNeuesSpiel(bool cheat)                                                      //Hier wird ein Spiel "erzeugt"
        {
            Console.Clear();

            if (cheat)
            {
                int eingabe;


                Console.WriteLine("Szenarien:\n[1]Figur schlagen\n[2]Im Haus ziehen\n[3]Ins Haus ziehen\n[4]Sieg\n[5]Bot Funktionalität");
                eingabe = Convert.ToInt16(Console.ReadLine());
                switch (eingabe)
                {
                    case 1: eingabe = 3; Speicherung.LadeSpiel(new FileStream("TestFigurSchlagen.txt", FileMode.Open, FileAccess.Read), eingabe); break;          //Wurf: 3
                    case 2: eingabe = 2; Speicherung.LadeSpiel(new FileStream("TestImHausZiehen.txt", FileMode.Open, FileAccess.Read), eingabe); break;          //Wurf: 2
                    case 3: eingabe = 5; Speicherung.LadeSpiel(new FileStream("TestInsHausZiehen.txt", FileMode.Open, FileAccess.Read), eingabe); break;          //Wurf: 5
                    case 4: eingabe = 1; Speicherung.LadeSpiel(new FileStream("TestSieg.txt", FileMode.Open, FileAccess.Read), eingabe); break;          //Wurf: 1
                    case 5: eingabe = 4; Speicherung.LadeSpiel(new FileStream("TestBotFunktionalitaet.txt", FileMode.Open, FileAccess.Read), eingabe); break;          //Wurf: 4
                }




            }
            else
            {
                int spielerzahl = int.MinValue;
                int botAnzahl = 0;
                do
                {
                    Console.WriteLine("Bitte geben Sie die Anzahl der Spieler ein(0-4):");
                    string? eingabe = Console.ReadLine();
                    try
                    {
                        spielerzahl = Convert.ToInt32(eingabe);

                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Falsche Eingabe... erneuter Versuch:");
                    }
                } while (spielerzahl != 0 && spielerzahl != 1 && spielerzahl != 2 && spielerzahl != 3 && spielerzahl != 4);

                do
                {
                    Console.WriteLine($"Bitte geben Sie die Anzahl der Bots ein(0-{4 - spielerzahl}):");
                    string eingabe = Console.ReadLine();
                    try
                    {
                        botAnzahl = Convert.ToInt32(eingabe);

                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Falsche Eingabe... erneuter Versuch:");
                        botAnzahl = -1; //Damit erneut abgefragt wird
                    }

                    if (spielerzahl + botAnzahl < 2)
                    {
                        botAnzahl = -1;
                        Console.WriteLine("Es muss mindestens 2 Spielende geben");
                    }
                } while (botAnzahl < 0 || botAnzahl > (4 - spielerzahl)); //Wenn keine Exception geworfen wird UND die Botanzahl in diesem Bereich liegt, dann gültig.





                string d = Directory.GetCurrentDirectory();                                                                                     //Wenn noch nicht vorhanden Erstellung des Spielerfiles und Header hineinschreiben
                string path = d + @"/Spielerdaten";
                Directory.CreateDirectory(path);
                path += "/Spielerdaten.txt";
                FileStream fsR = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);

                if (fsR.Length == 0)
                {
                    fsR.Close();
                    FileStream fsW = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fsW);
                    sw.WriteLine("Name\tEmail\tPasswort");
                    sw.Close();
                    fsW.Close();
                }
                else
                {
                    fsR.Close();
                }


                Console.Clear();
                List<Spieler> spielerliste = new List<Spieler>();
                List<Haus> haeuser = new List<Haus>();
                Regex regexName = new Regex(@"^[A-Za-zÄäÖöÜüß_\ \d]{2,16}$"); //Regex für Spielername. 2-16 Zeichen mit _ und [ ] und Zahlen
                Regex regexEmail = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");


                for (int i = 0; i < spielerzahl; i++)                                                       //Erstellung der menschlichen Spieler
                {
                    bool justregistered = false;
                    string nutzername = string.Empty;
                    bool auswahlwiederholen = false;
                    string passwort = string.Empty;
                    do
                    {
                        Console.WriteLine($"Spieler {i + 1}, wollen Sie sich [1] Anmelden, [2] Registrieren oder [3] als Gast Spielen?");
                        bool wiederholen = false;
                        int auswahl = 0;
                        do
                        {
                            wiederholen = false;
                            try
                            {
                                auswahl = Convert.ToInt32(Console.ReadLine());
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Bitte etwas eingeben");
                                wiederholen = true;
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Bitte weniger eingeben");
                                wiederholen = true;
                            }

                            if (auswahl > 3 || auswahl < 0)
                            {
                                Console.WriteLine("Bitte eine gültige Eingabe machen");
                                wiederholen = true;
                            }
                        } while (wiederholen);


                        bool abbruch = false;
                        switch (auswahl)
                        {
                            case 1:
                                bool erneut = true;
                                while (erneut)
                                {
                                    try
                                    {
                                        Console.WriteLine("Zum abbrechen nichts eingeben und Enter druecken");
                                        Console.WriteLine("Bitte geben Sie Ihren Nutzernamen ein:");
                                        nutzername = Console.ReadLine();
                                        Console.WriteLine($"Bitte geben Sie das Passwort ein!");
                                        string pw = Passsworteinlesen();
                                        Console.WriteLine();

                                        if (pw == "" || nutzername == "")
                                        {
                                            throw new AbbruchException();
                                        }


                                        FileStream fss = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
                                        StreamReader srr = new StreamReader(fss);

                                        string inhlt = srr.ReadToEnd();
                                        string[] zeilen = inhlt.Split('\n');
                                        string[] zeileninhalt;

                                        for (int a = 1; a < zeilen.Count() - 1 && erneut; a++)
                                        {
                                            zeileninhalt = zeilen[a].Split('\t');
                                            if (zeileninhalt[0].StartsWith(nutzername) && zeileninhalt[2].StartsWith(pw))
                                            {
                                                Console.WriteLine("Willkommen zurück," + nutzername);
                                                Console.WriteLine($"Sie haben bis jetzt {Speicherung.GetWins(nutzername)} mal gewonnen");
                                                erneut = false;
                                                passwort = zeileninhalt[2];
                                            }
                                        }

                                        if (erneut)
                                        {
                                            Console.WriteLine("Benutzername oder Passwort falsch");
                                        }

                                        fss.Close();
                                        srr.Close();
                                    }
                                    catch (AbbruchException)
                                    {
                                        erneut = false;
                                        auswahlwiederholen = true;
                                    }

                                }

                                break;
                            case 2:

                                string passwort2 = string.Empty;
                                string email = string.Empty;


                                try
                                {
                                    Console.WriteLine("Zum abbrechen nichts eingeben und enter drücken");
                                    do
                                    {
                                        Console.WriteLine("Bitte geben Sie ihre Email-Adresse ein:");
                                        email = Console.ReadLine();
                                    } while (!regexEmail.IsMatch(email) && email != "");
                                    if (email == "")
                                    {
                                        throw new AbbruchException();
                                    }
                                    bool falscheEingabe = false;
                                    do
                                    {
                                        falscheEingabe = false;
                                        try
                                        {

                                            Console.WriteLine("Bitte geben Sie ihren Benutzernamen ein [2 - 16 Zeichen]:");
                                            nutzername = Console.ReadLine();

                                            if (nutzername == "")
                                            {
                                                throw new AbbruchException();
                                            }

                                            if (!regexName.IsMatch(nutzername) || nutzername.Contains("bot") || nutzername == "Spieler" || nutzername == "Siege")
                                            {
                                                falscheEingabe = true;
                                                throw new UserFalscheEingabeException("Dieser Nutzername ist ungueltig");
                                            }
                                            fsR = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
                                            StreamReader sr = new StreamReader(fsR);
                                            string inhalt = sr.ReadToEnd();
                                            string[] lines = inhalt.Split('\n');
                                            bool bereitsverwendeterNutzername = false;

                                            for (int k = 1; k < lines.Count(); k++)
                                            {
                                                if (nutzername == lines[k].Split("\t")[0])
                                                {
                                                    bereitsverwendeterNutzername = true;
                                                    if (bereitsverwendeterNutzername)
                                                    {
                                                        sr.Close();
                                                        fsR.Close();
                                                        throw new UserFalscheEingabeException("Dieser Benutzername wird bereits verwendet");
                                                    }
                                                }
                                            }
                                            sr.Close();
                                            fsR.Close();
                                        }
                                        catch (UserFalscheEingabeException e)
                                        {
                                            Console.WriteLine(e.Message);
                                            falscheEingabe = true;
                                        }
                                    } while (falscheEingabe);

                                    do
                                    {
                                        Console.WriteLine("Bitte geben Sie ein Passwort ein");
                                        passwort = Passsworteinlesen();
                                        Console.WriteLine();
                                        if (passwort == null)
                                        {
                                            throw new AbbruchException();
                                        }

                                        Console.WriteLine("Bitte bestätigen Sie ihr Passwort (nochmal eingeben)");
                                        passwort2 = Passsworteinlesen();
                                        Console.WriteLine();

                                        if (passwort != passwort2)
                                        {
                                            Console.WriteLine("Das Passwort stimmt nicht ueberein");
                                        }

                                    } while (passwort != passwort2);
                                }
                                catch (AbbruchException)
                                {
                                    auswahlwiederholen = true;
                                    abbruch = true;
                                }
                                if (!abbruch)
                                {
                                    FileStream fsR2 = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
                                    StreamReader sr = new StreamReader(fsR2);
                                    string copyto = sr.ReadToEnd();

                                    sr.Close();
                                    fsR2.Close();

                                    copyto += $"{nutzername}\t{email}\t{passwort}\n";
                                    FileStream fsW = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                                    StreamWriter sw = new StreamWriter(fsW);

                                    sw.Write(copyto);
                                    sw.Close();
                                    fsW.Close();

                                }
                                break;

                            case 3:
                                do
                                {
                                    Console.WriteLine("Bitte gib deinen Namen ein[2 bis 16 Zeichen]:");
                                    nutzername = Console.ReadLine();
                                    if (!regexName.IsMatch(nutzername))
                                    {
                                        Console.WriteLine("Name hat ungueltiges Format");
                                    }
                                } while (!regexName.IsMatch(nutzername));
                                break;

                        }
                    } while (auswahlwiederholen);







                    Console.WriteLine($"\n{nutzername}, Bitte geben Sie Ihre gewünschte Hausfarbe ein\n" +
                    $"Verfügbar sind folgende:\n{getAvailableColors(haeuser)}"); //Listet alle verwendete Farben der Häuser
                    bool check = true;
                    while (check == true)
                    {

                        string? gewuenschteFarbe = Console.ReadLine();
                        Verfuegbare_Farben farbe = Verfuegbare_Farben.Rot; //Platzhalter-Farbe
                        if (Enum.TryParse(gewuenschteFarbe, out farbe))//Versuchen, ob die gewünschte Farbe im Enum enthalten ist.
                        {
                            if (getAvailableColors(haeuser).Contains(farbe.ToString())) //Wenn die gewünschte Farbe verfügbar ist, dann gültig.
                            {
                                Console.WriteLine($"Erfolgreich die Farbe {farbe} ausgewählt!");
                                haeuser.Add(new Haus(farbe));
                                check = false;
                            }
                            else //Sonst das:
                            {
                                Console.WriteLine("Diese Farbe wird schon verwendet, bitte eine andere Farbe wählen");
                            }

                        }
                        else //Wenn nicht im Enum enthalten:
                        {
                            Console.WriteLine("Diese Farbe existiert nicht, bitte eine andere Farbe wählen:");
                        }
                    }

                    if (passwort == string.Empty)
                    {
                        haeuser.ElementAt(i).ZugehoerigerSpieler = new Menschlicher_Spieler(nutzername);
                    }
                    else
                    {
                        haeuser.ElementAt(i).ZugehoerigerSpieler = new AngemeldeterSpieler(nutzername, passwort);
                    }

                    spielerliste.Add(haeuser.ElementAt(i).ZugehoerigerSpieler); //Und er wird zur Spielerliste hinzugefügt.



                }
                for (int i = 0; i < botAnzahl; i++)//Erstellung der Bots:
                {
                    Random r = new Random();
                    int farbeIndex = r.Next(0, 7); // 0 bis 7 weil es so viele Farben im Enum gibt.
                    Verfuegbare_Farben farbe = (Verfuegbare_Farben)farbeIndex; //somit kann man auf die korrekte Stelle des Enums zugreifen.
                    if (getAvailableColors(haeuser).Contains(farbe.ToString()))//Wenn Farbe verwendbar, dann normaler Ablauf:
                    {
                        haeuser.Add(new Haus(farbe));
                        haeuser.ElementAt(i + spielerzahl).ZugehoerigerSpieler = new Bot();
                        spielerliste.Add(haeuser.ElementAt(i + spielerzahl).ZugehoerigerSpieler);
                    }
                    else //Sonst wird nochmal eine Zufallszahl generiert.
                    {
                        i--;
                    }

                }
                for (int i = haeuser.Count; i < 4; i++) //Platzhalter-Häuser (Wenn weniger Spieler als 4)                                                                                     //Die leeren Haueser mit Pseudo bots auffuelen
                {
                    Haus x = new Haus(Verfuegbare_Farben.Weiss); //Weiss ist die Platzhalter Farbe
                    x.AuffuellHaus = true; //Damit das Programm (wichtiger Print) weiß, dass es kein "normales" Haus ist.
                    haeuser.Add(x);
                }
                Thread.Sleep(1000);
                Console.Clear();

                int j = 0;
                foreach (Spieler s in spielerliste)   //Anzeige der Spielernamen mit der ausgewählten Farbe.                                                                                          //Ausgabe der Spielernamen mit zugehöriger Hausfarbe
                {
                    Console.WriteLine($"Spieler {j + 1}: {s.Name} mit {haeuser.ElementAt(j).Farbe} als Farbe des Hauses.");
                    j++;
                }

                Thread.Sleep(3000);
                Spielablauf(haeuser, spielerliste);
            }


        }

        public void Spielablauf(List<Haus> haeuser, List<Spieler> spieler, int cheat = 100)
        {
            Console.Clear();
            Print p = new Print(haeuser);
            Spieler? gewinner = null;
            p.PrintSpielfeld(); //Erste Ausgabe des Spielfelds
            bool win = false;
            int abtauschen = 0; //Die Züge werden damit abgetauscht.
            string timestamp = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss"); //Timestamp für die Speicherung
            string path = $"SaveFile_{timestamp}.txt"; //der Pfad des Save Files (falls erstellt)
            while (!win) //Solange kein Spieler gewonnen hat.
            {
                //Hier kommen die Spieler nacheinander zum Zug
                if (abtauschen < spieler.Count && cheat != 4)
                {
                    Console.WriteLine($"Der Spieler {spieler.ElementAt(abtauschen).Name} ist dran!");
                }

                switch (abtauschen)
                {
                    case 0:
                        if (cheat != 100 && cheat != 4)
                        {

                            auswaehlen(haeuser.ElementAt(abtauschen), false, cheat, p, haeuser);
                            p.PrintSpielfeld();
                            cheat = 90;
                        }
                        else if (!haeuser.ElementAt(abtauschen).AuffuellHaus && cheat != 4)                                                //Hier wird geschaut ob in diesem Haus ein Pseudo Bot wohnt
                        {
                            wuerfeln(haeuser.ElementAt(abtauschen), p, haeuser);                                        //Wenn nicht, wird für diesen Spieler oder Bot gewürfelt

                        }
                        haeuser.ElementAt(abtauschen).ZiehbareFigurenBerechnen();                                       //Nach jedem Zug werden noch die Ziehbaren Figuren berechnet
                        if (haeuser.ElementAt(abtauschen).ZiehbareFiguren == 0)                                         //Wenn keine Figur mehr gezogen werden kann, heißt das, dass alle Figuren am Ende angelangt sind und dieser Spieler gewonnen hat
                        {
                            win = true;
                            gewinner = haeuser.ElementAt(abtauschen).ZugehoerigerSpieler;
                        }
                        abtauschen++;
                        break;

                    case 1:
                        if (cheat == 4)
                        {
                            Console.WriteLine($"Der Spieler {spieler.ElementAt(abtauschen).Name} ist dran!");

                            p.PrintSpielfeld();
                            Thread.Sleep(10000);
                            Bot b = haeuser.ElementAt(abtauschen).ZugehoerigerSpieler as Bot;
                            for (int i = 0; i < 3; i++)
                            {
                                Console.WriteLine($"{cheat} gewürfelt");
                                b.Spielfigurbewegen(haeuser.ElementAt(abtauschen), haeuser, cheat);
                                p.PrintSpielfeld();
                                Thread.Sleep(5000);
                            }

                            cheat = 90;

                        }
                        else if (!haeuser.ElementAt(abtauschen).AuffuellHaus)
                        {
                            wuerfeln(haeuser.ElementAt(abtauschen), p, haeuser);

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
                        if (cheat == 90)
                        {
                            return;
                        }
                        if (!haeuser.ElementAt(abtauschen).AuffuellHaus)
                        {
                            wuerfeln(haeuser.ElementAt(abtauschen), p, haeuser);

                        }
                        haeuser.ElementAt(abtauschen).ZiehbareFigurenBerechnen();
                        if (haeuser.ElementAt(abtauschen).ZiehbareFiguren == 0)
                        {
                            win = true;
                            gewinner = haeuser.ElementAt(abtauschen).ZugehoerigerSpieler;
                        }

                        abtauschen = 0;
                        char eingabe = '\0';

                        FileInfo[] list = Speicherung.GetAllSaveFiles();                                                //Nachdem der letzte Spieler gezogen ist, wird gefragt ob das Spiel gespeichert werden soll
                        if (list.Length <= 5) //Man darf nur 5 Save Files insgesamt haben
                        {
                            Console.WriteLine("Wollen Sie das Spiel speichern? [y]");
                            char.TryParse(Console.ReadLine(), out eingabe);
                        }
                        if (eingabe.Equals('Y') || eingabe.Equals('y'))
                        {
                            Speicherung.SpielSpeichern(spieler, haeuser, path);
                        }
                        Console.WriteLine("Aktuelles Spielfeld:");
                        p.PrintSpielfeld(); //Spielfeld ausgeben (nur hier, da in der wuerfeln Methode schon das Feld ausgegeben wird.
                        break;
                }


            }
            Console.WriteLine("\n\n\n\n\n"); //Man kommt hier hin, wenn jemand gewonnen hat.

            Console.WriteLine("-------------------------------------\n\n");
            Console.WriteLine($"Der Spieler {gewinner.Name} hat Gewonnen!!!!\n\n");
            Console.WriteLine("-------------------------------------");
            if (gewinner is AngemeldeterSpieler) //Der Sieg sollte nur gespeichert werden, wenn der Sieger angemeldet ist.
            {
                Speicherung.SaveWins((AngemeldeterSpieler)gewinner);
            }

        }
        private string getAvailableColors(List<Haus> haeuser)                                        //Diese Funktion berechnet, welche Farben noch verfügbar sind
        {
            List<Verfuegbare_Farben> liste = new List<Verfuegbare_Farben>();
            List<Verfuegbare_Farben> verwendeteFarben = new List<Verfuegbare_Farben>();
            int i = 0;

            foreach (Haus h in haeuser) //Hier werden alle bereits verwendeten Farben ermmittelt.
            {
                Verfuegbare_Farben farbe = 0;
                Enum.TryParse(h.Farbe.ToString(), out farbe);
                verwendeteFarben.Add(farbe);
                i++;
            }

            foreach (Verfuegbare_Farben farbe in Enum.GetValues(typeof(Verfuegbare_Farben))) // Diese foreach geht durch jede Farbe einmal durch (außer weiß, da diese reserviert ist)
            {
                if (!verwendeteFarben.Contains(farbe) && farbe != Verfuegbare_Farben.Weiss) //Wenn die aktuelle Farbe nicht verwendet wird UND NICHT weiß ist, dann ist sie verfügbar
                {
                    liste.Add(farbe);
                }
            }
            string alleFarben = string.Empty;
            foreach (Verfuegbare_Farben f in liste) //Hier wird einfach der string zusammengebaut.
            {
                alleFarben += f.ToString() + "\n";
            }

            return alleFarben; //return alle verfügbaren Farben.
        }
        private string Passsworteinlesen()
        {
            string passwort = string.Empty;
            ConsoleKeyInfo taste;

            do
            {
                taste = Console.ReadKey(true);

                if (taste.Key == ConsoleKey.Backspace)
                {
                    if (passwort.Length > 0)
                    {
                        passwort = passwort[0..^1];

                        Console.Write("\b \b");
                    }
                }
                else if (Char.IsPunctuation(taste.KeyChar) || Char.IsLetterOrDigit(taste.KeyChar) || Char.IsSymbol(taste.KeyChar))
                {
                    Console.Write("*");
                    passwort += taste.KeyChar;
                }
            } while (taste.Key != ConsoleKey.Enter);

            return passwort;
        }
    }
}
