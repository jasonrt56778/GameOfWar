using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfWar
{
    class Driver
    {
        public static void Main(String[] args)
        {
            GameOfWar game = new GameOfWar();
            game.Play();
            Console.Write("Press any key to exit command prompt.");
            Console.ReadKey();
        }
    }
}
