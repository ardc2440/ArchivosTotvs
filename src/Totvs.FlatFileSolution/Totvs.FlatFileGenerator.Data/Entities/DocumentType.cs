using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Totvs.FlatFileGenerator.Data.Entities
{
    public class DocumentType
    {
        public int Id { get; set; }
        public string Name { get; set; }   
        public string CodeType { get; set; } 
        public DateTime LastExecutionDate { get; set; }
        public DateTime LastCleaningDate { get; set;}

        public virtual ICollection<ShippingProcessDetail> ProcessDetails { get; } = new HashSet<ShippingProcessDetail>();

    }
}
