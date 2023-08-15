using System;

namespace Totvs.FlatFileGenerator.Data.Entities
{
    public class PurchaseOrderDataFile
    {
        public string Type { get; set; }
        public string PurchaseNumber { get; set; }
        public string ProviderIdentificationType { get; set; }
        public string ProviderIdentificationNumber { get; set; }         
        public DateTime ArrivingEstimatedDate { get; set; }
        public string ProformaNumber { get; set; } 
        public int PurchaseId { get; set; } 
        public int PurchaseDetailId { get; set; } 
        public string LineCode { get; set; }
        public string ItemCode { get; set; }
        public string ReferenceCode { get; set; }
        public int Quantity { get; set; }
    }
}
