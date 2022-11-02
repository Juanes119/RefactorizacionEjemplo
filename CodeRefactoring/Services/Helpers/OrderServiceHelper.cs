using CodeRefactoring.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeRefactoring.Services
{
    public partial class OrderService
    {
        private List<Offer> GetOffers()
        {
            return new List<Offer>
            {
                new Offer {
                    DiscountId = 1,
                    Discount = 0.2m,
                    ProductId = 1
                },
                new Offer {
                    DiscountId = 2,
                    Discount = 0.1m,
                    ProductId = 2
                }
            };
        }

        private int GetNextOrderNumber()
        {
            var random = new Random();
            return random.Next(1, 1000);
        }

        private void SetNextNumber(Order order)
        {
            var nextNumber = GetNextOrderNumber();
            order.OrderId = string.Format($"{DateTime.UtcNow.Year}-{nextNumber.ToString().PadLeft(4, '0')}");
        }

        private void PrepareDetail(Order order)
        {
            // Impuesto
            var ivaRate = 0.18m;

            // Productos con descuento
            var offers = GetOffers();

            foreach (var item in order.Items)
            {
                // Calculos del detalle
                item.Total = item.Quantity * item.UnitPrice;
                item.Iva = item.Total * ivaRate;
                item.SubTotal = item.Total - item.Iva;

                // Verifica si el producto tiene descuento
                CalculateDiscount(item, offers, ivaRate);
            }
        }

        private void CalculateOrderAmounts(Order order)
        {
            // Suma los totales
            order.Total = order.Items.Sum(x => x.Total);
            order.Iva = order.Items.Sum(x => x.Iva);
            order.SubTotal = order.Items.Sum(x => x.SubTotal);
            order.Discount = order.Items.Sum(x => x.Discount);
        }

        private void CalculateDiscount(OrderDetail item, List<Offer> offers, decimal ivaRate)
        {
            // Verifica si el producto tiene descuento
            var offer = offers.SingleOrDefault(x => x.ProductId == item.ProductId);

            // Hace la lógica del descuento
            if (offer != null)
            {
                item.UnitPrice = (1 - offer.Discount) * item.UnitPrice;

                var originalTotal = item.Total;

                item.Total = Math.Round(item.Quantity * item.UnitPrice, 2);
                item.SubTotal = Math.Round(item.Total / (1 + ivaRate), 2);
                item.Iva = Math.Round(item.Total - item.SubTotal, 2);
                item.Discount = originalTotal - item.Total;
            }
        }
    }
}
