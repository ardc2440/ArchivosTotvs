using System;
using System.Collections.Generic;

namespace Totvs.FlatFileGenerator.Business.Models
{
    public class InProcessOrder
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<InProcessOrderDetail> Details { get; set; }

        public static implicit operator InProcessOrder(Data.Entities.InProcessOrder inProcessOrder)
        {
            if (inProcessOrder == null) return null!;
            return new InProcessOrder
            {
                Id = inProcessOrder.Id,
                Type = inProcessOrder.Type,
                Date = inProcessOrder.Date
            };
        }
    }
}