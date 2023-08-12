using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Totvs.FlatFileGenerator.Data.Entities
{
    public class ShippingProcessDetail
    {
        public int Id { get; set; }        
        public int ShippingProcessId { get; set; }
        public int DocumentTypeId { get; set; }
        public int DocumentId { get; set; }
        public string FileName { get; set; }
        public virtual ShippingProcess ShippingProcess { get; set; } = null!;
        public virtual DocumentType DocumentType { get; set; } = null!;
    }
}
