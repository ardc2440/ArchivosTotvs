using CsvHelper.Configuration;
using Totvs.FlatFileGenerator.Business.Models;

namespace Totvs.FlatFileGenerator.Engine.Mappers
{
    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Map(m => m.Id).Name("Id");
            Map(m => m.OrderNumber).Name("Numero");
            Map(m => m.Status).Name("Estado");
        }
    }
}
