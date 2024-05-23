﻿namespace Mensch_Aergere_Dich_Nicht
{
    internal class Spielfigur
    {
        public int ID { get; set; }
        public string Farbe { get; private set; }
        public int Position { get; set; } = 0;
        public int PrintPosition { get; set; } = 0;      //Printposition standardmäßig 0
        public bool IsInHouse { get; set; } = true;

        public Spielfigur(int id, string farbe, int position)
        {
            ID = id;
            Farbe = farbe;
            Position = position;
        }

        public void PrintFigur()
        {
            switch (Farbe)
            {
                case "Rot": Console.ForegroundColor = ConsoleColor.Red; Console.Write($"F{ID}"); break;
                case "Gruen": Console.ForegroundColor = ConsoleColor.Green; Console.Write($"F{ID}"); break;
                case "Blau": Console.ForegroundColor = ConsoleColor.Blue; Console.Write($"F{ID}"); break;
                case "Gelb": Console.ForegroundColor = ConsoleColor.Yellow; Console.Write($"F{ID}"); break;
                case "Tuerkis": Console.ForegroundColor = ConsoleColor.Cyan; Console.Write($"F{ID}"); break;
                case "Dunkelrot": Console.ForegroundColor = ConsoleColor.DarkRed; Console.Write($"F{ID}"); break;
                case "Dunkelgruen": Console.ForegroundColor = ConsoleColor.DarkGreen; Console.Write($"F{ID}"); break;
                case "Magenta": Console.ForegroundColor = ConsoleColor.Magenta; Console.Write($"F{ID}"); break;

            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
