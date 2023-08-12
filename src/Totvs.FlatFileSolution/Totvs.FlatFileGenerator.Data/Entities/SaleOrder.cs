using System;

namespace Totvs.FlatFileGenerator.Data.Entities
{
    public class SaleOrder
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
    }
}
