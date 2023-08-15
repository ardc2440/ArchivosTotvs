using System;
using System.Collections.Generic;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Business.Models
{
    public class ShippingProcess
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<ShippingProcessDetail> Details { get; set; }

        public static implicit operator ShippingProcess(Data.Entities.ShippingProcess shippingProcess)
        {
            if (shippingProcess == null) return null!;
            return new ShippingProcess
            {
                Id = shippingProcess.Id,
                Path = shippingProcess.Path,
                Date = shippingProcess.Date
            };
        }
    }
}

