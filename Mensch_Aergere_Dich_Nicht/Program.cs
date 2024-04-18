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

            haueser.ElementAt(1).ZugehoerigeFiguren.ElementAt(2).PrintPosition = 3;
            PrintSpielfeld(haueser);

        }

        static void PrintSpielfeld(List<Haus> haueser)
        {
            string zeilestandard = "\t\t\t\t\t#    |    |    #\t\t";
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
                    Console.WriteLine(zeilestandard);
                }
            }







            Console.WriteLine(rahmen);
        }
    }
}
