using System.Collections.Generic;

namespace Totvs.FlatFileGenerator.Data.Entities
{
    public class StatusDocumentType
    {
        public short StatusDocumentTypeId { get; set; }
        public string StatusDocumentTypeName { get; set; }
        public short DocumentTypeId { get; set; }
        public string Notes { get; set; }
        public bool EditMode { get; set; }
        public short StatusOrder { get; set; }
        public DocumentType DocumentType { get; set; }

        // Reverse navigation
        public ICollection<Order> CustomerOrders { get; set; }
        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        
        public StatusDocumentType()
        {
            EditMode = true;
            CustomerOrders = new List<Order>();
            PurchaseOrders = new List<PurchaseOrder>();
        }
    }
}
