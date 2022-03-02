using System;

namespace OrdersApi
{
    public class Order
    {
        public int OrderId { get; set; }

        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}
