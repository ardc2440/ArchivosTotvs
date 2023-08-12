using System;

namespace Totvs.FlatFileGenerator.Data.Entities
{
    public class SaleOrderDataFile
    {
        public string Type { get; set; }
        public string SaleNumber { get; set; }
        public string ClientIdentificationType { get; set; }
        public string ClientIdentificationNumber { get; set; }
        public string CustomerObservations { get; set; }
        public string InternalObservations { get; set; }
        public DateTime EstimatedDeliveryDate { get; set; }
        public int SaleId { get; set; }
        public int SaleDetailId { get; set; }
        public string LineCode { get; set; }
        public string ItemCode { get; set; }
        public string ReferenceCode { get; set; }
        public int DeliveryQuantity { get; set; }
        public int RequestedQuantity { get; set; }
        public string SaleState { get; set; }
    }
}
