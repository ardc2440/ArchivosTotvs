using CsvHelper.Configuration;
using Totvs.FlatFileGenerator.Business.Models;

namespace Totvs.FlatFileGenerator.Engine.Mappers
{
    public class SaleOrderDetailMap : ClassMap<Detail>
    {
        public SaleOrderDetailMap()
        {
            Map(m => m.Type).Ignore();
            Map(m => m.SaleNumber).Ignore();
            Map(m => m.ClientIdentificationType).Ignore();
            Map(m => m.ClientIdentificationNumber).Ignore();
            Map(m => m.CustomerObservations).Ignore();
            Map(m => m.InternalObservations).Ignore();
            Map(m => m.EstimatedDeliveryDate).Ignore();
            Map(m => m.SaleId).Ignore();
            Map(m => m.SaleDetailId).Ignore();
            Map(m => m.LineCode).Index(0);
            Map(m => m.ItemCode).Index(1);
            Map(m => m.ReferenceCode).Index(2);
            Map(m => m.Quantity).Index(3);
            Map(m => m.SaleState).Ignore();
        }
    }
}
