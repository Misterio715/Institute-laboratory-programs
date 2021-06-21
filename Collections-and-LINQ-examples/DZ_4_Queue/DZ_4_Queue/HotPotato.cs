using System;
using System.Collections.Generic;

namespace DZ_4_Queue
{
    public class HotPotato
    {
        public bool GameOver { get; private set; }
        public string Winner { get; private set; }

        private IQueue<string> playerPool;

        public HotPotato(IQueue<string> collection, params string[] players)
        {
            playerPool = collection;
            for (int i = 0; i < players.Length; i++)
            {
                playerPool.Enqueue(players[i]);
            }
        }

        // public HotPotato(Queue<string> collection)
        // {
        //     playerPool = collection;
        // }

        public string Play(int n)
        {
            Console.WriteLine($"Новый ход с {n} числом шагов");
            for (int i = 0; i < n; i++)
            {
                playerPool.Enqueue(playerPool.Dequeue());
            }
            string ans;
            if (playerPool.Count > 1)
            {
                ans = playerPool.Dequeue();
            }
            else
            {
                ans = "";
                Winner = playerPool.Dequeue();
            }
            return ans;
        }

        public string Play()
        {
            Random random = new Random();
            return Play(random.Next(1, playerPool.Count + 1));
        }
        
        public string PlayToEnd()
        {
            for (int i = 0, pool = playerPool.Count - 1; i < pool; i++)
            {
                Console.WriteLine($"Игрок {Play()} выбывает");
            }
            Play();
            return Winner;
        }
        
    }
}