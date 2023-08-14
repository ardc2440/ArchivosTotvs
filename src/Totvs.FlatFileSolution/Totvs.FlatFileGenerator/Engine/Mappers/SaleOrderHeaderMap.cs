using CsvHelper.Configuration;
using Totvs.FlatFileGenerator.Business.Models;

namespace Totvs.FlatFileGenerator.Engine.Mappers
{
    public class SaleOrderHeaderMap : ClassMap<SaleOrderHeader>
    {
        public SaleOrderHeaderMap()
        {
            Map(m => m.Type).Index(0);
            Map(m => m.ActionType).Index(1);
            Map(m => m.SaleNumber).Index(2);
            Map(m => m.ClientIdentificationType).Index(3);
            Map(m => m.ClientIdentificationNumber).Index(4);
            Map(m => m.CustomerObservations).Index(5);
            Map(m => m.InternalObservations).Index(6);
            Map(m => m.EstimatedDeliveryDate).Index(7);
        }
    }
}
