using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Totvs.FlatFileGenerator.Data.Entities
{
    public class SaleOrder
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
    }
}
