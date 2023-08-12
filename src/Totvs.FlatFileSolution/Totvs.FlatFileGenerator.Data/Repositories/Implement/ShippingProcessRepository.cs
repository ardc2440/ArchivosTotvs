using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Data.Repositories.Interface;

namespace Totvs.FlatFileGenerator.Data.Repositories.Implement
{
    public class ShippingProcessRepository:IShippingProcessRepository
    {
        private readonly AldebaranContext _context;
        public ShippingProcessRepository(AldebaranContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }        
    }
}
