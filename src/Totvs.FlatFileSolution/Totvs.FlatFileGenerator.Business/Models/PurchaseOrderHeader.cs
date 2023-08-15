using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Infrastructure.Common.Extensions;

namespace Totvs.FlatFileGenerator.Business.Models
{
    public class PurchaseOrderHeader
    {
        public string Type { get; set; }
        public string ActionType { get; set; }
        public string PurchaseNumber { get; set; }
        public string ProviderIdentificationType { get; set; }
        public string ProviderIdentificationNumber { get; set; }
        public string ArrivingEstimatedDate { get; set; }
        public string ProformaNumber { get; set; }

        public static implicit operator PurchaseOrderHeader(Business.Models.PurchaseOrder purchaseOrder)
        {
            if (purchaseOrder == null) return null!;
            if (!purchaseOrder.Details.Any()) return null;

            var detail = purchaseOrder.Details.First();
            return new PurchaseOrderHeader
            {
                Type = detail.Type,
                ActionType = purchaseOrder.Type,
                PurchaseNumber = detail.PurchaseNumber,
                ProviderIdentificationType = detail.ProviderIdentificationType,
                ProviderIdentificationNumber = detail.ProviderIdentificationNumber,
                ArrivingEstimatedDate = detail.ArrivingEstimatedDate.ToString("yyyyMMdd"),
                ProformaNumber = detail.ProformaNumber
            };
        }
    }
}
