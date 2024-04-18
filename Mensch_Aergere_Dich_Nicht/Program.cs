namespace Mensch_Aergere_Dich_Nicht
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Haus blauesHaus = new Haus(Verfuegbare_Farben.Blau);
            Haus gruenesHaus = new Haus(Verfuegbare_Farben.Gruen);
            Haus gelbesHaus = new Haus(Verfuegbare_Farben.Gelb);
            Haus rotesHaus = new Haus(Verfuegbare_Farben.Rot);
            List<Haus> haueser = new List<Haus>();
            haueser.Add(blauesHaus);
            haueser.Add(gruenesHaus);
            haueser.Add(gelbesHaus);
            haueser.Add(rotesHaus);

            haueser.ElementAt(2).ZugehoerigeFiguren.ElementAt(1).PrintPosition = 3;

            PrintSpielfeld(haueser);
            //Testen der Methode wuerfeln
            Spielfigur spielfigur = new Spielfigur(0, Verfuegbare_Farben.Blau.ToString(), 0);
            spielfigur._position += wuerfeln();
            Console.WriteLine(spielfigur._position);

        }

        static void PrintSpielfeld(List<Haus> haueser)
        {
            string zeile = "\t\t\t\t\t#    |    |    #\t\t";
            string zeileMitSpieler = "\t\t\t\t\t  ";

            List<Spielfigur> figurenInZeile = new List<Spielfigur>();

            int x = 50;
            int j;

            string rahmen = "\t\t----------------------------------------------------------------";                 // ein \t entspricht 8 "-" Zeichen
            Console.WriteLine(rahmen);
            Console.WriteLine("\t\t\t\t\t################\t\t");

            for (int i = 0; i <= 4; i++)
            {
                if (i % 2 == 0)
                {
                    if (i == 2)
                    {

                        foreach (Haus h in haueser)
                        {
                            foreach (Spielfigur s in h.ZugehoerigeFiguren)
                            {
                                if (s.PrintPosition == 1 || s.PrintPosition == 2 || s.PrintPosition == 3)
                                {
                                    figurenInZeile.Add(s);
                                }
                            }
                        }
                        int printcounter = 0;
                        int check;
                        Console.Write(zeileMitSpieler);

                        check = figurenInZeile.FindIndex(i => i.PrintPosition == 1);
                        if (check == -1)
                        {
                            Console.Write("     ");
                        }
                        else
                        {
                            figurenInZeile.ElementAt(check).PrintFigur();
                            Console.Write("   ");
                        }


                        check = figurenInZeile.FindIndex(i => i.PrintPosition == 2);
                        if (check == -1)
                        {
                            Console.Write("     ");
                        }
                        else
                        {
                            figurenInZeile.ElementAt(check).PrintFigur();
                            Console.Write("   ");
                        }



                        check = figurenInZeile.FindIndex(i => i.PrintPosition == 3);
                        if (check == -1)
                        {
                            Console.Write("     ");
                        }
                        else
                        {
                            figurenInZeile.ElementAt(check).PrintFigur();
                            Console.Write("   ");
                        }


                        Console.Write('\n');
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


            if(haueser.ElementAt(0).figurenImHaus >= 2)
            {
                Console.Write("\t\t\t");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("MMM  MMM\t");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("#____XXXXXX____#");
                
            }
            else if(haueser.ElementAt(0))


            Console.Write('\n');




            Console.WriteLine(rahmen);
        }

        public static int wuerfeln()
        {
            int ziehe = 0;
            bool erneutWuerfeln = true;
            Random r = new Random();
            
            while(erneutWuerfeln != false) //Logik für, wenn jemand eine 6 würfelt
            {
                ziehe += r.Next(1, 7);
                if (ziehe == 6)
                    erneutWuerfeln = true;
                
                else
                    erneutWuerfeln = false;
            }
            

            return ziehe;
        }


        //Samuel wenn beim Wuerfeln eine Spielfigur aus dem Haus zieht, dies bitte in der figurenImHaus variable der Klasse haus vermerken


    }
}
