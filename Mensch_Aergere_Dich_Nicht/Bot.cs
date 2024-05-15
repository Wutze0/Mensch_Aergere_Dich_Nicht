using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mensch_Aergere_Dich_Nicht
{
    internal class Bot
    {
        private Haus _zugehoerigesHaus;

        public Bot(Haus zugehoerigesHaus)
        {
            _zugehoerigesHaus = zugehoerigesHaus;
        }


        public void Spielfigurbewegen(Haus hausDesBots, List<Haus> alleHaueser, int wieWeitZiehen)
        {
            bool movefound = true;
            int priority = 1;
            Spielfigur? zuBewegendeSpielfigur = null;
            while(movefound)
            {
                if(priority == 1)
                {
                    foreach(Spielfigur s in hausDesBots.ZugehoerigeFiguren)
                    {
                        if (movefound)
                        {
                            if ((s.Position + wieWeitZiehen) >= 41 && (s.Position + wieWeitZiehen) <= 44)
                            {
                                movefound = false;
                                zuBewegendeSpielfigur = s;
                            }
                        }
                       
                    }
                }
                /*if(priority == 2)
                {
                    foreach(Spielfigur sBot)
                    {
                        if(sBot.IsInHouse) { }
                        else
                        {
                            foreach(Haus h in alleHaueser)
                            {
                                foreach(Spielfigur s in h.ZugehoerigeFiguren)
                                {
                                    if(sBot.PrintPosition)
                                }
                            }
                        }
                    }
                }*/


            }
        }




    }
}
