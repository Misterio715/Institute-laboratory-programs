using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_Task_1
{
    /// <summary>
    /// Купон
    /// </summary>
    public class Coupon
    {
        /// <summary>
        /// Дата купона
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Сумма купона в валюте облигации
        /// </summary>
        public decimal AmountInCurrency { get; set; }
    }
}
