namespace Mensch_Aergere_Dich_Nicht
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Haus blauesHaus = new Haus("blau");
            Haus gruenesHaus = new Haus("gruen");
            Haus gelbesHaus = new Haus("gelb");
            Haus rotesHaus = new Haus("rot");
            List<Haus> haueser = new List<Haus>();
            haueser.Add(blauesHaus);
            haueser.Add(gruenesHaus);
            haueser.Add(gelbesHaus);
            haueser.Add(rotesHaus);

            PrintSpielfeld(haueser);
            
        }

        static void PrintSpielfeld(List<Haus> haueser)
        {
            string zeilestandard = "\t\t\t\t\t#    |    |    #\t\t";

            int x = 50;
            string rahmen = "\t\t----------------------------------------------------------------";                 // ein \t entspricht 8 "-" Zeichen
            Console.WriteLine(rahmen);
            Console.WriteLine("\t\t\t\t\t################\t\t");

            for (int i = 0; i <= 4;  i++)
            {
                if(i % 2 == 0)
                {
                    foreach(Haus h in haueser)
                    {
                        foreach(Spielfigur s in h._zugehoerigeFiguren)
                        {
                            if (Spielfigur.) 
                        }
                    }
                    Console.WriteLine("");
                }
                else if(i % 2 == 1)
                {
                     Console.WriteLine(zeilestandard);
                }
            }



            



            Console.WriteLine(rahmen);
        }
    }
}
