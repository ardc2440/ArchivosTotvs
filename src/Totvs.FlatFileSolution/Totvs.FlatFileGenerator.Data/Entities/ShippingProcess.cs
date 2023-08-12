using System;
using System.Collections.Generic;

namespace Totvs.FlatFileGenerator.Data.Entities
{
    public class ShippingProcess
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public DateTime Date { get; set; }
        public virtual ICollection<ShippingProcessDetail> ProcessDetails { get; } = new HashSet<ShippingProcessDetail>();

    }
}
