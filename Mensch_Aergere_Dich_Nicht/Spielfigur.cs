namespace Mensch_Aergere_Dich_Nicht
{
    internal class Spielfigur
    {
        private int _id;
        private string _farbe;
        public double _position { get; set; }
        private double _printPosition { get; set; } = 0;

        public Spielfigur(int id, string farbe, double position)
        {
            _id = id;
            _farbe = farbe;
            _position = position;
        }
    }
}
