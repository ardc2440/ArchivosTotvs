using CsvHelper.Configuration;
using Totvs.FlatFileGenerator.Business.Models;

namespace Totvs.FlatFileGenerator.Engine.Mappers
{
    public class PurchaseOrderDetailMap : ClassMap<PurchaseOrderDetail>
    {
        public PurchaseOrderDetailMap()
        {
            Map(m => m.Type).Ignore();
            Map(m => m.PurchaseNumber).Ignore();
            Map(m => m.ProviderIdentificationType).Ignore();
            Map(m => m.ProviderIdentificationNumber).Ignore();
            Map(m => m.PurchaseId).Ignore();
            Map(m => m.PurchaseDetailId).Ignore();
            Map(m => m.ArrivingEstimatedDate).Ignore();
            Map(m => m.LineCode).Index(0);
            Map(m => m.ItemCode).Index(1);
            Map(m => m.ReferenceCode).Index(2);
            Map(m => m.Quantity).Index(3);            
        }
    }
}
