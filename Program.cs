using System;

namespace Blackjack
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Blackjack!");

            Game game = new Game();
            game.Start();
        }
    }
}
