using System;
using System.Collections.Generic;
using System.Text;

namespace Practice_Task_1
{
    class BondDescriptor
    {
        private Bond bond;
        private ICalculator calculator;
        public BondDescriptor(Bond bond, ICalculator calculator)
        {
            this.bond = bond;
            this.calculator = calculator;
        }
        public string GetDescription() =>
            $"Название: {bond.Name}; номинал {bond.Nominal:0} {bond.Currency}; купон {bond.CouponRate}%\n";
        public string GetYTM()
        {
            string time = bond.BuyBackDate.HasValue ? bond.BuyBackDate.Value.ToShortDateString() : bond.MaturityDate.ToShortDateString();
            return $"Доходность: {calculator.YieldToMaturity(bond)}% на {time}\n";
        }
        public string GetPrice() =>
            $"Текущая стоимость (включая НКД): {bond.Price + bond.AccumulateCouponIncome}\n";
        public string GetCoupons()
        {
            string text = "";
            if (bond.BuyBackDate.HasValue & bond.BuyBackDate.Value > bond.SettlementDate)
            {
                for (int i = 0; i < 2; i++)
                {
                    if (bond.Coupons[i].Date <= bond.BuyBackDate)
                    {
                        text += $"{bond.Coupons[i].Date.ToShortDateString()} Купон {bond.Coupons[i].AmountInCurrency}\n";
                    }
                }
                if (bond.Coupons.Count == 3)
                {
                    text += $"{"- - -",13}\n";
                    if (bond.Coupons[2].Date <= bond.BuyBackDate)
                    {
                        text += $"{bond.Coupons[2].Date.ToShortDateString()} Купон {bond.Coupons[2].AmountInCurrency}";
                    }
                    text += $"{bond.BuyBackDate.Value.ToShortDateString()} Байбек {bond.Nominal:0}\n";
                }
                if (bond.Coupons.Count > 3)
                {
                    text += $"{"- - -",13}\n";
                    string text2 = "";
                    for (int i = 3; i < bond.Coupons.Count; i++)
                    {
                        if (bond.Coupons[i].Date <= bond.BuyBackDate)
                            text2 = $"{bond.Coupons[i].Date.ToShortDateString()} Купон {bond.Coupons[i].AmountInCurrency}\n";
                    }
                    text += text2 + $"{bond.BuyBackDate.Value.ToShortDateString()} Байбек {bond.Nominal:0}\n";
                }
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    text += $"{bond.Coupons[i].Date.ToShortDateString()} Купон {bond.Coupons[i].AmountInCurrency}\n";
                }
                if (bond.Coupons.Count == 3)
                {
                    text += $"{"- - -",13}\n";
                    text += $"{bond.Coupons[2].Date.ToShortDateString()} Купон {bond.Coupons[2].AmountInCurrency}\n";
                }
                if (bond.Coupons.Count > 3)
                {
                    text += $"{"- - -",13}\n";
                    text += $"{bond.Coupons[bond.Coupons.Count - 2].Date.ToShortDateString()} Купон {bond.Coupons[bond.Coupons.Count - 2].AmountInCurrency}\n";
                    text += $"{bond.Coupons[bond.Coupons.Count - 1].Date.ToShortDateString()} Купон {bond.Coupons[bond.Coupons.Count - 1].AmountInCurrency}\n";
                }
            }
            return text;
        }
    }
}
