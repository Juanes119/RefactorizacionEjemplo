using CodeRefactoring.Models;
using System;

namespace CodeRefactoring.Services
{
    public partial class OrderService
    {
        public Order Create(Order order)
        {
            // Calcula el número de orden actual
            SetNextNumber(order);

            // Se recorre el detalle
            PrepareDetail(order);

            // Calcular totales
            CalculateOrderAmounts(order);

            // Se genera la fecha de creación
            order.CreatedAt = DateTime.UtcNow;

            return order;
        }
    }
}
