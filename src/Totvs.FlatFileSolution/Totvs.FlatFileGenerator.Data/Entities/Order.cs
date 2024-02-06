using System;

namespace Totvs.FlatFileGenerator.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string OrderNumber { get; set; } = null!;
        public DateTime DeliveryDate { get; set; }
        public string Comments { get; set; }
        public int StaffId { get; set; }
        public int Status { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerNotes { get; set; }

        public virtual Client Client { get; set; } = null!;
        public virtual StatusDocumentType StatusDocumentType { get; set; } = null;

    }
}
