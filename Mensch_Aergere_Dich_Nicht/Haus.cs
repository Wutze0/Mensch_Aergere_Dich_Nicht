namespace Mensch_Aergere_Dich_Nicht
{
    internal class Haus
    {
        private int _anzSpielfiguren;
        private string _farbe;
        public int figurenImHaus { get; set; } = 4;

        private static int numberOfHouses = 0;
        public bool IsStartingPositionFree { get; set; } = true;

        public int StartingPrintPosition {  get; set; }
        public List<Spielfigur> ZugehoerigeFiguren { get; set; } = new List<Spielfigur>();


        public Haus(Verfuegbare_Farben farbe)
        {
            _farbe = farbe.ToString();
            StartingPrintPosition = 1 + (numberOfHouses * 10);
            numberOfHouses++;
            _anzSpielfiguren = 4;
            for (int i = 1; i <= 4; i++)
            {
                ZugehoerigeFiguren.Add(new Spielfigur(i, farbe.ToString(), 100));
            }

        }

    }

}
