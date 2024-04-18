namespace Mensch_Aergere_Dich_Nicht
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Haus blauesHaus = new Haus(Verfuegbare_Farben.Blau);
            Haus gruenesHaus = new Haus(Verfuegbare_Farben.Grün);
            Haus gelbesHaus = new Haus(Verfuegbare_Farben.Gelb);
            Haus rotesHaus = new Haus(Verfuegbare_Farben.Rot);
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

        public static int wuerfeln()
        {
            int ziehe = 0;
            bool erneutWuerfeln = true;
            Random r = new Random();
            
            while(erneutWuerfeln != false) //Logik für, wenn jemand eine 6 würfelt
            {
                ziehe = r.Next(6, 1);
                if (ziehe == 6)
                    erneutWuerfeln = true;
                else
                    erneutWuerfeln = false;
            }
            

            return ziehe;
        }
    }
}
