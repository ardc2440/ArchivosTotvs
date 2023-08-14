using System;
using System.Collections.Generic;

namespace Totvs.FlatFileGenerator.Business.Models
{
    public class SaleOrder
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<SaleOrderDetail> Details { get; set; }

        public static implicit operator SaleOrder(Data.Entities.SaleOrder saleOrder)
        {
            if (saleOrder == null) return null!;
            return new SaleOrder
            {
                Id = saleOrder.Id,
                Type = saleOrder.Type,
                Date = saleOrder.Date,
            };
        }
    }
}