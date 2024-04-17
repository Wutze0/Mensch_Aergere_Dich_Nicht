﻿namespace Mensch_Aergere_Dich_Nicht
{
    internal class Haus
    {
        private int _anzSpielfiguren;
        private string _farbe;
        private List<Spielfigur> _zugehoerigeFiguren = new List<Spielfigur>();


        public Haus(Enum farbe)
        {
            _farbe = farbe.ToString();

            _anzSpielfiguren = 4;
            for (int i = 0; i < 4; i++)
            {
                _zugehoerigeFiguren.Add(new Spielfigur(i, _farbe, 100));
            }

        }

    }

}
