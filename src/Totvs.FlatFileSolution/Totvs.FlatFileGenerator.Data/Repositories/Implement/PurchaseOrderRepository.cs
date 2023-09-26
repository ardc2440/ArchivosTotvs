using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Data.Entities;
using Totvs.FlatFileGenerator.Data.Repositories.Interface;

namespace Totvs.FlatFileGenerator.Data.Repositories.Implement
{
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private readonly AldebaranShippingContext _context;
        public PurchaseOrderRepository(AldebaranShippingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<PurchaseOrder>> Get(CancellationToken ct = default)
        {
            return await _context.PurchaseOrders.ToListAsync(ct);
        }
    }
}
