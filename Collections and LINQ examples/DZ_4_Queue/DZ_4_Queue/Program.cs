using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DZ_4_Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            // Queue<string> myQueue = new Queue<string>();
            // myQueue.Enqueue("Philip");
            // myQueue.Enqueue("Kirill");
            // myQueue.Enqueue("Ivan");
            // myQueue.Enqueue("Denis");
            // myQueue.Enqueue("Eugenia");
            // myQueue.Enqueue("Zhanna");
            // myQueue.Enqueue("Stephanie");
            // myQueue.Enqueue("Valentina");
            // HotPotato game = new HotPotato(myQueue);
            HotPotato game = new HotPotato(new MyQueue<string>(),
                "Philip", "Kirill", "Ivan", "Denis", "Eugenia", "Zhanna", "Stephanie", "Valentina");
            Console.WriteLine("Начало игры");
            Console.WriteLine("Игры окончена!\n" + "Побеждает " + game.PlayToEnd());
            Console.WriteLine(game.Winner);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedTicks);
        }
    }
}