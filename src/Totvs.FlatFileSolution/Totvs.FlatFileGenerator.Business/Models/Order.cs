using System;

namespace Totvs.FlatFileGenerator.Business.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = null!;
        public string Status { get; set; } = null!;

        public static implicit operator Order(Data.Entities.Order order)
        {
            if (order == null) return null!;
            return new Order
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                Status = order.Status
            };
        }
    }

    public class SaleOrder
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }

        public static implicit operator SaleOrder(Data.Entities.SaleOrder saleOrder)
        {
            if (saleOrder == null) return null!;
            return new SaleOrder
            {
                Id = saleOrder.Id,
                Type = saleOrder.Type,
                Date = saleOrder.Date
            };
        }

        //public static implicit operator List<SaleOrder>(IEnumerable<Data.Entities.SaleOrder> saleOrder)
        //{
        //    if (saleOrder == null) return null!;
        //    var xx = saleOrder.Select(s => (SaleOrder)s);
        //    return xx.ToList();
        //}
    }
}