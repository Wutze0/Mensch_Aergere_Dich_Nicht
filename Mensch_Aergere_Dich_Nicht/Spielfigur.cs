using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mensch_Aergere_Dich_Nicht
{
    internal class Spielfigur
    {
        private int _id;
        private string _farbe;
        private double _position;

        public Spielfigur(int id,  string farbe, double position)
        {
            _id = id;
            _farbe = farbe;
            _position = position;
        }
    }
}
