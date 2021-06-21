using System;
using System.Collections.Generic;
using System.Linq;

namespace DZ_6_LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ClientMonth> clientMonthsList = new List<ClientMonth>();
            foreach (var c in GenerateClientMonths(100))
            {
                clientMonthsList.Add(c);
                Console.WriteLine(c);
            }
            Console.WriteLine("----------------------------------------");

            
            
            Console.WriteLine("Клиент с минимальным количеством часов занятий в месяц");
            var res1 = clientMonthsList.Last(client => client.hours == clientMonthsList.Min(client => client.hours));
            
            Console.WriteLine(res1);
            Console.WriteLine("----------------------------------------");

            
            
            Console.WriteLine("Месяц с наибольшим числом занятий");
            int i = 0;
            var res2 = clientMonthsList.GroupBy(client => client.month).Select(clientMonths =>
                new {month = clientMonths.Key, sum = clientMonths.Sum(month => month.hours)}).OrderByDescending(arg => arg.month);
            
            // foreach (var c in res2)
            // {
            //     Console.WriteLine(c.month + ": " + c.sum);
            // }
            var res3 = res2.Last(arg => arg.sum == res2.Max(arg => arg.sum));
            
            Console.WriteLine($"Месяц {res3.month}: {res3.sum} часов");
            Console.WriteLine("----------------------------------------");

            
            
            Console.WriteLine("Суммарное количество часов для каждого клиента");
            var res4 = clientMonthsList.GroupBy(clinet => clinet.id).Select(clientMonth =>
                new {id = clientMonth.Key, hours = clientMonth.Sum(arg => arg.hours)}).OrderByDescending(arg => arg.hours);
            
            foreach (var c in res4)
            {
                Console.WriteLine($"Клиент {c.id}: {c.hours} часов");
            }
            Console.WriteLine("----------------------------------------");


            
            Console.WriteLine("Количество клиентов в каждом месяце");
            var res5 = clientMonthsList.GroupBy(client => client.month).Select(clientMonth =>
                new {month = clientMonth.Key, clients = clientMonth.Count(arg => arg.hours > 0)}).OrderBy(arg => arg.clients);
            
            var res6 = res5.Join(res2, arg1 => arg1.month, arg2 => arg2.month, (arg1, arg2) =>
                new {arg1.month, arg1.clients, hours = arg2.sum});
            
            foreach (var c in res6)
            {
                Console.WriteLine($"Месяц {c.month}: клиентов - {c.clients}, часов - {c.hours}");
            }
            Console.WriteLine("----------------------------------------");
        }

        private static IEnumerable<ClientMonth> GenerateClientMonths(int count)
        {
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    yield return new ClientMonth(i, j, random.Next(121));
                }
            }
        }
    }
}