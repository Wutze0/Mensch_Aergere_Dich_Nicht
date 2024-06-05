using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mensch_Aergere_Dich_Nicht
{
    internal class AngemeldeterSpieler : Menschlicher_Spieler
    {
        private string _email;
        private string _password;
        public int _wins { get; set; }
        public AngemeldeterSpieler(string name, string passwort, string email) : base(name)
        {
            _email = email;
            _password = passwort;
        }
    }
}
