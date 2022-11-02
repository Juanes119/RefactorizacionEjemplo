using CodeRefactoring.Models;
using System.Collections.Generic;

namespace CodeRefactoring.Services
{
    public class ProductService
    {
        public IReadOnlyCollection<Product> GetAll() 
        {
            return new List<Product>
            {
                new Product {
                    ProductId = 1,
                    Name = "Guitarra Fender Squier",
                    Price = 420
                },
                new Product {
                    ProductId = 2,
                    Name = "Guitarra Suhr",
                    Price = 2200
                },
                new Product {
                    ProductId = 3,
                    Name = "AMP Mesa Boogie Rectifier",
                    Price = 1500
                },
                new Product {
                    ProductId = 4,
                    Name = "Cuerdas de guitarra Addario #9",
                    Price = 7
                }
            };
        }
    }
}
