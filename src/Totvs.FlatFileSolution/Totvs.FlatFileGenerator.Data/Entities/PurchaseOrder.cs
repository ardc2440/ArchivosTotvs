using System;

namespace Totvs.FlatFileGenerator.Data.Entities
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime Date{ get; set; }
    }
}
