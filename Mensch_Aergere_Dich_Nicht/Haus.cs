using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mensch_Aergere_Dich_Nicht
{
    internal class Haus
    {
        private int _anzSpielfiguren;
        private string _farbe;
        private List<Spielfigur> _zugehoerigeFiguren = new List<Spielfigur>();


        public Haus(Verfuegbare_Farben farbe)
        {
            _farbe = farbe.ToString();
            _anzSpielfiguren = 4;
            for(int i = 0; i < 4; i++)
            {
                _zugehoerigeFiguren.Add(new Spielfigur(i, farbe.ToString(), 100));
            }

        }

    }

}
