namespace Mensch_Aergere_Dich_Nicht
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Print print = new Print();
            Haus blauesHaus = new Haus(Verfuegbare_Farben.Tuerkis);
            Haus gruenesHaus = new Haus(Verfuegbare_Farben.Gruen);
            Haus gelbesHaus = new Haus(Verfuegbare_Farben.Dunkelgruen);
            Haus rotesHaus = new Haus(Verfuegbare_Farben.Rot);
            List<Haus> haueser = new List<Haus>();
            haueser.Add(blauesHaus);
            haueser.Add(gruenesHaus);
            haueser.Add(gelbesHaus);
            haueser.Add(rotesHaus);

            haueser.ElementAt(1).ZugehoerigeFiguren.ElementAt(3).PrintPosition = 1;
            haueser.ElementAt(2).ZugehoerigeFiguren.ElementAt(2).PrintPosition = 20;

            print.PrintSpielfeld(haueser);


            //Testen der Methode wuerfeln
            Spielfigur spielfigur = new Spielfigur(0, Verfuegbare_Farben.Blau.ToString(), 0);
            spielfigur._position += wuerfeln();
            Console.WriteLine(spielfigur._position);



        }



        public static int wuerfeln()
        {
            int ziehe = 0;
            bool erneutWuerfeln = true;
            Random r = new Random();

            while (erneutWuerfeln != false) //Logik für, wenn jemand eine 6 würfelt
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
