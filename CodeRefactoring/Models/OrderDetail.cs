namespace CodeRefactoring.Models
{
    public class OrderDetail
    {
        public string OrderId { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Iva { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }

        public OrderDetail() 
        {
            Product = new Product();
        }
    }
}
