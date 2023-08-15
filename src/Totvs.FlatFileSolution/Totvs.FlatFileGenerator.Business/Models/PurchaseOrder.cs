using System;
using System.Collections.Generic;

namespace Totvs.FlatFileGenerator.Business.Models
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<PurchaseOrderDetail> Details { get; set; }

        public static implicit operator PurchaseOrder(Data.Entities.PurchaseOrder purchaseOrder)
        {
            if (purchaseOrder == null) return null!;
            return new PurchaseOrder
            {
                Id = purchaseOrder.Id,
                Type = purchaseOrder.Type,
                Date = purchaseOrder.Date,
            };
        }
    }
}