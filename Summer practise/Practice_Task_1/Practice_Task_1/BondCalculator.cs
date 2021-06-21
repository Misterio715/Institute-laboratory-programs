using System;
using System.Collections.Generic;
using System.Text;

namespace Practice_Task_1
{
    class BondCalculator : ICalculator
    {
        public decimal YieldToMaturity(Bond bond)
        {
            Func<decimal, decimal> function = (ytm) =>
            {
                decimal sum, s;
                DateTime time = bond.BuyBackDate.HasValue & bond.BuyBackDate.Value > bond.SettlementDate ? bond.BuyBackDate.Value : bond.MaturityDate;
                //Console.WriteLine($"YTM = {ytm}");
                sum = -bond.Price - bond.AccumulateCouponIncome;
                //Console.WriteLine(sum);
                int i = 0;
                while ((i < bond.Coupons.Count) && (bond.Coupons[i].Date <= time))
                {
                    sum += bond.Coupons[i].AmountInCurrency / (decimal)Math.Pow(1 + (double)ytm, (bond.Coupons[i].Date - bond.SettlementDate).Days / 365);
                    i++;
                    //Console.WriteLine(sum);
                }
                s = bond.Nominal / (decimal)Math.Pow(1 + (double)ytm, (time - bond.SettlementDate).Days / 365);
                sum = sum + s;
                //Console.WriteLine($"s = {s}");
                //Console.WriteLine($"bond.Nominal = {bond.Nominal}");
                //Console.WriteLine($"days = {(time - bond.SettlementDate).Days}");
                //Console.WriteLine($"sum = {sum}");
                //Console.WriteLine("-------------------------");
                return sum;
            };
            return Solve(function);
        }
        private decimal Solve(Func<decimal, decimal> function)
        {
            decimal a = 0, b = 20, e = 0.001m, c;
            while (b - a > e)
            {
                c = (a + b) / 2;
                if (function(b) * function(c) < 0)
                    a = c;
                else
                    b = c;
            }
            return Math.Round((a + b) * 100 / 2, 2);
        }
    }
}
