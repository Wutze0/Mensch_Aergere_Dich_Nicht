using System.Text.RegularExpressions;

namespace Mensch_Aergere_Dich_Nicht
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Einf�hrung();

            Haus blauesHaus = new Haus(Verfuegbare_Farben.Blau);
            Haus gruenesHaus = new Haus(Verfuegbare_Farben.Gr�n);
            Haus gelbesHaus = new Haus(Verfuegbare_Farben.Gelb);
            Haus rotesHaus = new Haus(Verfuegbare_Farben.Rot);
            List<Haus> haueser = new List<Haus>();
            haueser.Add(blauesHaus);
            haueser.Add(gruenesHaus);
            haueser.Add(gelbesHaus);
            haueser.Add(rotesHaus);


            PrintSpielfeld(haueser);
            //Testen der Methode wuerfeln
            Spielfigur spielfigur = new Spielfigur(0, Verfuegbare_Farben.Blau.ToString(), 0);
            spielfigur._position += wuerfeln();
            Console.WriteLine(spielfigur._position);

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
                           //if (Spielfigur.) 
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

        private static int wuerfeln()
        {
            int ziehe = 0;
            bool erneutWuerfeln = true;
            Random r = new Random();
            
            while(erneutWuerfeln != false) //Logik f�r, wenn jemand eine 6 w�rfelt, dann darf er erneut w�rfelln
            {
                ziehe += r.Next(1, 7);
                if (ziehe == 6)
                    erneutWuerfeln = true;
                
                else
                    erneutWuerfeln = false;
            }
            

            return ziehe;
        }
        private static void Einf�hrung()
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
            

            while(true)
            {
                char eingabe = '\0';
                try
                {
                    eingabe = Convert.ToChar(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Falsche Eingabe... erneuter Versuch:");
                }
            }
            
        }

    }
}
