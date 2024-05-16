namespace Mensch_Aergere_Dich_Nicht
{
    internal class Print
    {
        private List<Haus> _haueser = new List<Haus>();

        public Print(List<Haus> haueser)
        {
            _haueser = haueser;
        }



        public void PrintSpielfeld()
        {
            string zeile = "\t\t\t\t\t\t\t#        |        |        #\t\t";
            string distanceLongRow = "\t\t    ";
            string rahmen = "\t------------------------------------------------------------------------------------------------------------------------------";                 // ein \t entspricht 8 Zeichen

            Console.WriteLine(rahmen);
            Console.WriteLine("\t\t\t\t\t\t\t############################\t\t");

            for (int i = 0; i <= 4; i++)
            {
                if (i % 2 == 0)
                {
                    if (i == 2)
                    {

                        PrintZeileMitSpieler(1, 2, 3);

                    }
                    else
                    {
                        Console.WriteLine("");
                    }

                }
                else if (i % 2 == 1)
                {
                    Console.WriteLine(zeile);
                }

            }

            string block = "\u25A0";
            zeile = "#________XXXXXXXXXX________#";
            for (int i = 0; i < 3; i++)
            {
                if (i % 2 == 0)
                {
                    //Ausgabe blaues Haus
                    if (_haueser.ElementAt(0).FigurenImHaus == 4)
                    {
                        Console.Write("\t\t\t\t");
                        _haueser.ElementAt(0).changeColour();
                        Console.Write("MMM    MMM\t\t");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(zeile);

                    }
                    else if (_haueser.ElementAt(0).FigurenImHaus == 3)
                    {
                        Console.Write("\t\t\t\t");
                        _haueser.ElementAt(0).changeColour();
                        Console.Write($"{block}{block}{block}    MMM\t\t");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(zeile);
                    }
                    else if (_haueser.ElementAt(0).FigurenImHaus <= 2)
                    {
                        Console.Write("\t\t\t\t");
                        _haueser.ElementAt(0).changeColour();
                        Console.Write($"{block}{block}{block}    {block}{block}{block}\t\t");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(zeile);
                    }


                    //Ausgabe gruenes Haus
                    if (_haueser.ElementAt(1).FigurenImHaus == 4)
                    {
                        Console.Write("\t\t");
                        _haueser.ElementAt(1).changeColour();
                        Console.Write("MMM    MMM\t\t");
                        Console.ForegroundColor = ConsoleColor.White;

                    }
                    else if (_haueser.ElementAt(1).FigurenImHaus == 3)
                    {
                        Console.Write("\t\t");
                        _haueser.ElementAt(1).changeColour();
                        Console.Write($"{block}{block}{block}    MMM\t\t");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (_haueser.ElementAt(1).FigurenImHaus <= 2)
                    {
                        Console.Write("\t\t");
                        _haueser.ElementAt(1).changeColour();
                        Console.Write($"{block}{block}{block}    {block}{block}{block}\t\t");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    zeile = "#        X        X        #";                    //Die Variable dient zur Ausgabe des Zeileninneren und kann hier geaendert werden, da dieser if Abzweigung nur 2 mal betreten wird.

                    Console.Write("\n");
                }
                else
                {
                    Console.WriteLine();
                }

            }

            PrintZeileMitSpieler(40, 45, 4);
            Console.WriteLine("\t\t\t\t\t\t\t#        X        X        #");
            Console.WriteLine("");
            Console.WriteLine("\t\t\t\t\t\t\t#________X........X________#");
            Console.WriteLine("");

            for (int i = 0; i < 3; i++)
            {
                if (i % 2 == 0)
                {
                    //Ausgabe blaues Haus
                    if (_haueser.ElementAt(0).FigurenImHaus >= 2)
                    {
                        Console.Write("\t\t\t\t");
                        _haueser.ElementAt(0).changeColour();
                        Console.Write("MMM    MMM\t\t");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(zeile);

                    }
                    else if (_haueser.ElementAt(0).FigurenImHaus == 1)
                    {
                        Console.Write("\t\t\t\t");
                        _haueser.ElementAt(0).changeColour();
                        Console.Write($"{block}{block}{block}    MMM\t\t");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(zeile);
                    }
                    else if (_haueser.ElementAt(0).FigurenImHaus == 0)
                    {
                        Console.Write("\t\t\t\t");
                        _haueser.ElementAt(0).changeColour();
                        Console.Write($"{block}{block}{block}    {block}{block}{block}\t\t");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(zeile);
                    }


                    //Ausgabe gruenes Haus
                    if (_haueser.ElementAt(1).FigurenImHaus >= 2)
                    {
                        Console.Write("\t\t");
                        _haueser.ElementAt(1).changeColour();
                        Console.Write("MMM    MMM\t");
                        Console.ForegroundColor = ConsoleColor.White;

                    }
                    else if (_haueser.ElementAt(1).FigurenImHaus == 1)
                    {
                        Console.Write("\t\t");
                        _haueser.ElementAt(1).changeColour();
                        Console.Write($"{block}{block}{block}    MMM\t");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (_haueser.ElementAt(1).FigurenImHaus == 0)
                    {
                        Console.Write("\t\t");
                        _haueser.ElementAt(1).changeColour();
                        Console.Write($"{block}{block}{block}    {block}{block}{block}\t");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    zeile = "#        X        X        #";
                    Console.Write("\n");
                }
                else
                {
                    PrintZeileMitSpieler(39, 46, 5);
                }

            }
            Console.WriteLine("");

            Console.WriteLine("\t\t\t\t\t\t\t#________X........X________#");
            Console.WriteLine("");
            zeile = "\t\t\t\t\t\t\t#        X        X        #";
            Console.WriteLine(zeile);
            PrintZeileMitSpieler(38, 47, 6);
            Console.WriteLine(zeile);
            Console.WriteLine("");


            Console.WriteLine(distanceLongRow + "#####################################--------X........X--------#####################################");
            Console.WriteLine(" ");
            zeile = "#        |        |        |        |        X        X        |        |        |        |        #";

            Console.Write(distanceLongRow + zeile + "\n");


            List<int> felderlangeZeile = new List<int>();
            for (int i = 33; i <= 37; i++)
            {
                felderlangeZeile.Add(i);
            }
            for (int i = 7; i <= 11; i++)
            {
                felderlangeZeile.Add(i);
            }

            PrintLangeZeileMitSpieler(_haueser, felderlangeZeile, 48);

            Console.WriteLine(distanceLongRow + zeile);
            Console.WriteLine("");


            Console.WriteLine(distanceLongRow + "#--------XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX--------#");
            Console.WriteLine("");
            Console.WriteLine(distanceLongRow + "#        X        :        :        :        X########X        :        :        :        X        #");

            felderlangeZeile.Clear();
            felderlangeZeile.Add(32);
            for (int i = 41; i <= 44; i++)
            {
                felderlangeZeile.Add(i);
            }

            for (int i = 56; i >= 53; i--)
            {
                felderlangeZeile.Add(i);
            }
            felderlangeZeile.Add(12);

            PrintLangeZeileMitSpieler(_haueser, felderlangeZeile);
            Console.WriteLine(distanceLongRow + "#        X        :        :        :        X########X        :        :        :        X        #");
            Console.WriteLine("");

            Console.WriteLine(distanceLongRow + "#--------XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX--------#");
            Console.WriteLine("");
            Console.WriteLine(distanceLongRow + zeile);




            felderlangeZeile.Clear();
            for (int i = 31; i >= 27; i--)
            {
                felderlangeZeile.Add(i);
            }
            for (int i = 17; i >= 13; i--)
            {
                felderlangeZeile.Add(i);
            }

            PrintLangeZeileMitSpieler(_haueser, felderlangeZeile, 52);
            Console.WriteLine(distanceLongRow + zeile);
            Console.WriteLine("");
            Console.WriteLine(distanceLongRow + "#####################################--------X........X--------#####################################");
            Console.WriteLine("");
            zeile = "\t\t\t\t\t\t\t#        X        X        #";
            Console.WriteLine(zeile);
            PrintZeileMitSpieler(26, 51, 18);
            Console.WriteLine(zeile);
            Console.WriteLine("");


            zeile = "#________X........X________#";
            for (int i = 0; i < 3; i++)
            {
                if (i % 2 == 0)
                {
                    //Ausgabe gelbes Haus
                    if (_haueser.ElementAt(2).FigurenImHaus == 4)
                    {
                        Console.Write("\t\t\t\t");
                        _haueser.ElementAt(2).changeColour();
                        Console.Write("MMM    MMM\t\t");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(zeile);

                    }
                    else if (_haueser.ElementAt(2).FigurenImHaus == 3)
                    {
                        Console.Write("\t\t\t\t");
                        _haueser.ElementAt(2).changeColour();
                        Console.Write($"{block}{block}{block}    MMM\t\t");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(zeile);
                    }
                    else if (_haueser.ElementAt(2).FigurenImHaus <= 2)
                    {
                        Console.Write("\t\t\t\t");
                        _haueser.ElementAt(2).changeColour();
                        Console.Write($"{block}{block}{block}    {block}{block}{block}\t\t");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(zeile);
                    }


                    //Ausgabe rotes Haus
                    if (_haueser.ElementAt(3).FigurenImHaus == 4)
                    {
                        Console.Write("\t\t");
                        _haueser.ElementAt(3).changeColour();
                        Console.Write("MMM    MMM\t\t");
                        Console.ForegroundColor = ConsoleColor.White;

                    }
                    else if (_haueser.ElementAt(3).FigurenImHaus == 3)
                    {
                        Console.Write("\t\t");
                        _haueser.ElementAt(3).changeColour();
                        Console.Write($"{block}{block}{block}    MMM\t\t");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (_haueser.ElementAt(3).FigurenImHaus <= 2)
                    {
                        Console.Write("\t\t");
                        _haueser.ElementAt(3).changeColour();
                        Console.Write($"{block}{block}{block}    {block}{block}{block}\t\t");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    zeile = "#        X        X        #";

                    Console.Write("\n");
                }
                else
                {
                    Console.WriteLine();
                }

            }

            PrintZeileMitSpieler(25, 50, 19);
            Console.WriteLine("\t\t\t\t\t\t\t" + zeile);
            Console.WriteLine("");

            zeile = "#________X........X________#";
            for (int i = 0; i < 3; i++)
            {
                if (i % 2 == 0)
                {
                    //Ausgabe gelbes Haus
                    if (_haueser.ElementAt(2).FigurenImHaus >= 2)
                    {
                        Console.Write("\t\t\t\t");
                        _haueser.ElementAt(2).changeColour();
                        Console.Write("MMM    MMM\t\t");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(zeile);

                    }
                    else if (_haueser.ElementAt(2).FigurenImHaus == 1)
                    {
                        Console.Write("\t\t\t\t");
                        _haueser.ElementAt(2).changeColour();
                        Console.Write($"{block}{block}{block}    MMM\t\t");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(zeile);
                    }
                    else if (_haueser.ElementAt(2).FigurenImHaus == 0)
                    {
                        Console.Write("\t\t\t\t");
                        _haueser.ElementAt(2).changeColour();
                        Console.Write($"{block}{block}{block}    {block}{block}{block}\t\t");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(zeile);
                    }


                    //Ausgabe rotes Haus
                    if (_haueser.ElementAt(3).FigurenImHaus >= 2)
                    {
                        Console.Write("\t\t");
                        _haueser.ElementAt(3).changeColour();
                        Console.Write("MMM    MMM\t\t");
                        Console.ForegroundColor = ConsoleColor.White;

                    }
                    else if (_haueser.ElementAt(3).FigurenImHaus == 1)
                    {
                        Console.Write("\t\t");
                        _haueser.ElementAt(3).changeColour();
                        Console.Write($"{block}{block}{block}    MMM\t\t");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (_haueser.ElementAt(3).FigurenImHaus == 0)
                    {
                        Console.Write("\t\t");
                        _haueser.ElementAt(3).changeColour();
                        Console.Write($"{block}{block}{block}    {block}{block}{block}\t\t");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    zeile = "#        X        X        #";
                    Console.Write("\n");
                }
                else
                {
                    Console.WriteLine("");
                }

            }
            PrintZeileMitSpieler(24, 49, 20);
            Console.WriteLine("\t\t\t\t\t\t\t#        X        X        #");
            Console.WriteLine("");
            Console.WriteLine("\t\t\t\t\t\t\t#________XXXXXXXXXX________#");
            Console.WriteLine("");
            Console.WriteLine("\t\t\t\t\t\t\t#        |        |        #");
            PrintZeileMitSpieler(23, 22, 21);
            Console.WriteLine("\t\t\t\t\t\t\t#        |        |        #");
            Console.WriteLine("");
            Console.WriteLine("\t\t\t\t\t\t\t##############################");


            Console.Write('\n');
            Console.WriteLine(rahmen);


            void PrintZeileMitSpieler(int erstesFeld, int zweitesFeld, int drittesFeld)
            {
                List<Spielfigur> figurenInZeile = new List<Spielfigur>();
                string zeileMitSpieler = "\t\t\t\t\t\t\t    ";
                foreach (Haus h in _haueser)
                {
                    foreach (Spielfigur s in h.ZugehoerigeFiguren)
                    {
                        if (s.PrintPosition == erstesFeld || s.PrintPosition == zweitesFeld || s.PrintPosition == drittesFeld)
                        {
                            figurenInZeile.Add(s);
                        }
                    }
                }
                int check;
                Console.Write(zeileMitSpieler);

                check = figurenInZeile.FindIndex(i => i.PrintPosition == erstesFeld);            //Es wird der Index des Elements zurueckgegeben, dessen Membervariable PrintPosition == 1 ist
                if (check == -1)
                {
                    Console.Write("         ");
                }
                else
                {
                    figurenInZeile.ElementAt(check).PrintFigur();
                    Console.Write("       ");
                }


                check = figurenInZeile.FindIndex(i => i.PrintPosition == zweitesFeld);
                if (check == -1)
                {
                    Console.Write("         ");
                }
                else
                {
                    figurenInZeile.ElementAt(check).PrintFigur();
                    Console.Write("       ");
                }



                check = figurenInZeile.FindIndex(i => i.PrintPosition == drittesFeld);
                if (check == -1)
                {
                    Console.Write("         ");
                }
                else
                {
                    figurenInZeile.ElementAt(check).PrintFigur();
                    Console.Write("       ");
                }


                Console.Write('\n');
            }

            void PrintLangeZeileMitSpieler(List<Haus> _haueser, List<int> felder, int sechstesFeld = 227)
            {
                felder.Insert(5, sechstesFeld);
                List<Spielfigur> figurenInZeile = new List<Spielfigur>();
                string langeZeileMitSpieler = "\t\t        ";
                foreach (Haus h in _haueser)
                {
                    foreach (Spielfigur s in h.ZugehoerigeFiguren)
                    {
                        for (int i = 0; i < felder.Count(); i++)
                        {
                            if (s.PrintPosition == felder[i])
                            {
                                figurenInZeile.Add(s);
                            }
                        }
                    }
                }
                int printcounter = 0;
                int check;

                Console.Write(langeZeileMitSpieler);

                for (int i = 0; i < felder.Count(); i++)
                {
                    PrintFeld(i);
                }
                Console.Write("\n");




                void PrintFeld(int j)
                {
                    check = figurenInZeile.FindIndex(i => i.PrintPosition == felder[j]);
                    if (check == -1)
                    {
                        Console.Write("         ");
                    }
                    else
                    {
                        figurenInZeile.ElementAt(check).PrintFigur();
                        Console.Write("       ");
                    }
                }
            }
        }
        public List<Spielfigur> GetAllSpielfiguren()
        {
            List<Spielfigur> allSpielfiguren = new List<Spielfigur>();
            foreach (Haus h in _haueser)
            {
                foreach (Spielfigur s in h.ZugehoerigeFiguren)
                {
                    allSpielfiguren.Add(s);
                }
            }
            return allSpielfiguren;
        }


    }
}
