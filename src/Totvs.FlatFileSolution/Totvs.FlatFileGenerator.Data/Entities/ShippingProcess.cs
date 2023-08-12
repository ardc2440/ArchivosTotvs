using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
