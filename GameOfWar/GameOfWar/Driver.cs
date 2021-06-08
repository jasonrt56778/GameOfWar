using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfWar
{
    class Driver
    {
        //Driver for GameOfWar
        public static void Main(String[] args)
        {
            GameOfWar game = new GameOfWar();
            game.Play();
            Console.Write("Press any key to exit command prompt.");
            Console.ReadKey();
        }
    }
}
