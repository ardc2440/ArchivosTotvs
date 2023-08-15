using System;

namespace Totvs.FlatFileGenerator.Business.Models
{
    public class PurchaseOrderDetail
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
        public string SaleState { get; set; }

        public static implicit operator PurchaseOrderDetail(Data.Entities.PurchaseOrderDataFile purchaseOrderDataFile)
        {
            if (purchaseOrderDataFile == null) return null!;
            return new PurchaseOrderDetail
            {
                Type = purchaseOrderDataFile.Type,
                PurchaseNumber = purchaseOrderDataFile.PurchaseNumber,
                ProviderIdentificationType = purchaseOrderDataFile.ProviderIdentificationType,
                ProviderIdentificationNumber = purchaseOrderDataFile.ProviderIdentificationNumber,
                ArrivingEstimatedDate = purchaseOrderDataFile.ArrivingEstimatedDate,
                ProformaNumber = purchaseOrderDataFile.ProformaNumber,
                PurchaseId = purchaseOrderDataFile.PurchaseId,
                PurchaseDetailId = purchaseOrderDataFile.PurchaseDetailId,
                LineCode = purchaseOrderDataFile.LineCode,
                ItemCode = purchaseOrderDataFile.ItemCode,
                ReferenceCode = purchaseOrderDataFile.ReferenceCode,
                Quantity = purchaseOrderDataFile.Quantity,                
            };
        }
    }
}
