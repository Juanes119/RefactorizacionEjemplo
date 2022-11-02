using System;
using System.Collections.Generic;

namespace CodeRefactoring.Models
{
    public class Order
    {
        public string OrderId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public ICollection<OrderDetail> Items { get; set; }
        public decimal Discount { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }

        public Order() 
        {
            Items = new List<OrderDetail>();
            Client = new Client();
        }
    }
}
