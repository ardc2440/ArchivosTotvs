using CsvHelper.Configuration;
using Totvs.FlatFileGenerator.Business.Models;

namespace Totvs.FlatFileGenerator.Engine.Mappers
{
    public class PurchaseOrderHeaderMap : ClassMap<PurchaseOrderHeader>
    {
        public PurchaseOrderHeaderMap()
        {
            Map(m => m.Type).Index(0);
            Map(m => m.ActionType).Index(1);
            Map(m => m.PurchaseNumber).Index(2);
            Map(m => m.ProviderIdentificationType).Index(3);
            Map(m => m.ProviderIdentificationNumber).Index(4);
            Map(m => m.ArrivingEstimatedDate).Index(5);
            Map(m => m.ProformaNumber).Index(6);
        }
    }
}
