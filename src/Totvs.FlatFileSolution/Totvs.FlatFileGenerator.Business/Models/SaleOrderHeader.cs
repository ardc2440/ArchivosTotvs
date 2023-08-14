using System;
using System.Linq;
using Totvs.FlatFileGenerator.Infrastructure.Common.Extensions;

namespace Totvs.FlatFileGenerator.Business.Models
{
    public class SaleOrderHeader
    {
        public string Type { get; set; }
        public string ActionType { get; set; }
        public string SaleNumber { get; set; }
        public string ClientIdentificationType { get; set; }
        public string ClientIdentificationNumber { get; set; }
        public string CustomerObservations { get; set; }
        public string InternalObservations { get; set; }
        public string EstimatedDeliveryDate { get; set; }

        public static implicit operator SaleOrderHeader(Business.Models.SaleOrder saleOrder)
        {
            if (saleOrder == null) return null!;
            if (!saleOrder.Details.Any()) return null;

            var detail = saleOrder.Details.First();
            return new SaleOrderHeader
            {
                Type = detail.Type,
                ActionType = saleOrder.Type,
                SaleNumber = detail.SaleNumber,
                ClientIdentificationType = detail.ClientIdentificationType,
                ClientIdentificationNumber = detail.ClientIdentificationNumber,
                CustomerObservations = detail.CustomerObservations.StripNewLines(),
                InternalObservations = detail.InternalObservations.StripNewLines(),
                EstimatedDeliveryDate = detail.EstimatedDeliveryDate.ToString("yyyyMMdd")
            };
        }
    }
}
