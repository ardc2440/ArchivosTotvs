using System;

namespace Totvs.FlatFileGenerator.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string OrderNumber { get; set; } = null!;
        public DateTime DeliveryDate { get; set; }
        public string AgreedDelivery { get; set; } = null!;
        public string Comments { get; set; }
        public int StaffId { get; set; }
        public string Status { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public string CustomerNotes { get; set; }
        
        public virtual Client Client { get; set; } = null!;
    }
}
