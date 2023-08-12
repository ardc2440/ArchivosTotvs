using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Data.Entities;
using Totvs.FlatFileGenerator.Data.Repositories.Interface;

namespace Totvs.FlatFileGenerator.Data.Repositories.Implement
{
    public class SaleOrderRepository : ISaleOrderRepository
    {
        private readonly AldebaranContext _context;
        public SaleOrderRepository(AldebaranContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<SaleOrder>> Get(CancellationToken ct = default)
        {
            return await _context.SaleOrders.ToListAsync(ct);
        }
    }
}
