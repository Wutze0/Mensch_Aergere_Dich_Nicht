﻿namespace Mensch_Aergere_Dich_Nicht
{
    internal class Spieler
    {
        public bool BotYesNo { get; private set; }              //Wenn true ist der Spieler ein Bot
        public string Name { get; private set; }

        public Spieler(string name, bool bot)
        {
            Name = name;
            BotYesNo = bot;
        }
    }
}
