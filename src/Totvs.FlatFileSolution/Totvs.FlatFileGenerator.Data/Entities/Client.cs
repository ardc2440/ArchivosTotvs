using System.Collections.Generic;

namespace Totvs.FlatFileGenerator.Data.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; } = new HashSet<Order>();
    }
}
