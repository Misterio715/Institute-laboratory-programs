using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Practice_Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Bond> bonds;
            using (StreamReader r = new StreamReader("bonds.json"))
            {
                string json = r.ReadToEnd();
                bonds = JsonConvert.DeserializeObject<List<Bond>>(json);
            }
            int BondIndex = -1;
            for (int i = 0; i < bonds.Count; i++)
            {
                if (bonds[i].ISIN == args[0])
                    BondIndex = i;
            }
            if (BondIndex < 0)
            {
                Console.WriteLine("Данная облигаци не найдена");
                Console.ReadKey();
                return;
            }
            ICalculator Calculation = new BondCalculator();
            BondDescriptor CurrentBond = new BondDescriptor(bonds[BondIndex], Calculation);
            Console.WriteLine(CurrentBond.GetDescription() + CurrentBond.GetYTM() + CurrentBond.GetPrice() + CurrentBond.GetCoupons());
            Console.ReadKey();
            return;
        }
    }
}
