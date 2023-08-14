using System;

namespace Totvs.FlatFileGenerator.Business.Models
{
    public class SaleOrderDetail
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
        public int Quantity { get; set; }
        public string SaleState { get; set; }

        public static implicit operator SaleOrderDetail(Data.Entities.SaleOrderDataFile saleOrderDataFile)
        {
            if (saleOrderDataFile == null) return null!;
            return new SaleOrderDetail
            {
                Type = saleOrderDataFile.Type,
                SaleNumber = saleOrderDataFile.SaleNumber,
                ClientIdentificationType = saleOrderDataFile.ClientIdentificationType,
                ClientIdentificationNumber = saleOrderDataFile.ClientIdentificationNumber,
                CustomerObservations = saleOrderDataFile.CustomerObservations,
                InternalObservations = saleOrderDataFile.InternalObservations,
                EstimatedDeliveryDate = saleOrderDataFile.EstimatedDeliveryDate,
                SaleId = saleOrderDataFile.SaleId,
                SaleDetailId = saleOrderDataFile.SaleDetailId,
                LineCode = saleOrderDataFile.LineCode,
                ItemCode = saleOrderDataFile.ItemCode,
                ReferenceCode = saleOrderDataFile.ReferenceCode,
                Quantity = saleOrderDataFile.SaleState == "R" ? saleOrderDataFile.DeliveryQuantity : saleOrderDataFile.RequestedQuantity,
                SaleState = saleOrderDataFile.SaleState
            };
        }
    }
}