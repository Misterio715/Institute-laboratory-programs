namespace DZ_6_LINQ
{
    public class ClientMonth
    {
        public int id { get; set; }
        public Month month { get; set; }
        public int hours { get; set; }

        public ClientMonth(int id, int month, int hours)
        {
            this.id = id;
            this.month = (Month)month;
            this.hours = hours;
        }

        public override string ToString()
        {
            return $"Запись: ID клиента: {id}; месяц: {month}; количество часов: {hours}";
        }
    }
}