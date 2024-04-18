namespace Mensch_Aergere_Dich_Nicht
{
    internal class Haus
    {
        private int _anzSpielfiguren;
        private string _farbe;
        public List<Spielfigur> ZugehoerigeFiguren { get; set; } = new List<Spielfigur>();


        public Haus(Verfuegbare_Farben farbe)
        {
            _farbe = farbe.ToString();

            _anzSpielfiguren = 4;
            for (int i = 1; i <= 4; i++)
            {
                ZugehoerigeFiguren.Add(new Spielfigur(i, farbe.ToString(), 100));
            }

        }

    }

}
