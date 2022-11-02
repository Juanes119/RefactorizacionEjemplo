using CodeRefactoring.Models;
using CodeRefactoring.Services;
using System;
using System.Linq;

namespace CodeRefactoring
{
    class Program
    {
        private static ProductService productService = new ProductService();
        private static OrderService orderService = new OrderService();
        private static ClientService clientService = new ClientService();

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("KODOTI Store");

            var order = new Order();

            // Choose a client
            ClientSelection(order);

            // Add products to detail
            ProductSelection(order);

            // Create Order
            CreateOrder(order);

            // Check test
            Test(order);

            Console.Read();
        }

        static void ClientSelection(Order order)
        {
            var clients = clientService.GetAll();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Seleccione un cliente:");

            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var c in clients)
            {
                Console.WriteLine($"{c.ClientId}) {c.Name}");
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("¿Qué cliente desea seleccionar?");

            var answer = Console.ReadLine();

            order.Client = clients.Single(x => x.ClientId == Convert.ToInt32(answer));
            order.ClientId = order.Client.ClientId;

            Console.Clear();
        }

        static void ProductSelection(Order order)
        {
            var products = productService.GetAll();
            var addOtherProduct = false;

            do
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Seleccione un producto:");

                Console.ForegroundColor = ConsoleColor.Cyan;
                foreach (var p in products)
                {
                    Console.WriteLine($"{p.ProductId}) {p.Name} - US$ {p.Price}");
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("¿Qué producto desea seleccionar?");

                var selectedProduct = Console.ReadLine();
                var product = products.Single(x => x.ProductId == Convert.ToInt32(selectedProduct));

                var orderDetail = new OrderDetail
                {
                    ProductId = product.ProductId,
                    Product = product,
                    UnitPrice = product.Price
                };

                Console.WriteLine("¿Cuantas unidades desea seleccionar?");
                var selectedQuantity = Console.ReadLine();

                orderDetail.Quantity = Convert.ToInt32(selectedQuantity);

                order.Items.Add(orderDetail);

                Console.WriteLine("¿Desea agregar otro producto? [Si/No]");
                addOtherProduct = Console.ReadLine().ToLower().Equals("si");
            }
            while (addOtherProduct);
        }

        static void CreateOrder(Order order) 
        {
            orderService.Create(order);

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Orden número: #{order.OrderId}");
            Console.WriteLine($"Cliente: {order.Client.Name}");

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Detalle de la orden:");

            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (var item in order.Items) 
            {
                Console.WriteLine($"{item.Product.Name.PadRight(35)} C: {item.Quantity.ToString().PadLeft(2)} | ST: US$ {item.SubTotal.ToString().PadLeft(8)} | T: US$ {item.Total.ToString().PadLeft(8)} | D: US$ {item.Discount.ToString().PadLeft(8)}");
            }

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"SubTotal: {order.SubTotal}");
            Console.WriteLine($"Iva: {order.Iva}");
            Console.WriteLine($"Total: {order.Total}");
            Console.WriteLine($"Descuento: {order.Discount}");

            Console.WriteLine();

            Console.WriteLine($"Creada el: {order.CreatedAt.Value.ToShortDateString()}");

            Console.ReadLine();
        }

        static void Test(Order order) 
        {
            Console.Clear();

            if (order.SubTotal == 4141 && order.Iva == 909 && order.Total == 5050 && order.Discount == 220)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Test passed");
            }
            else 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Test failed");
            }
        }
    }
}
