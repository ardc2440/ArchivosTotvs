using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Data.Entities;
using Totvs.FlatFileGenerator.Data.Repositories.Interface;

namespace Totvs.FlatFileGenerator.Data.Repositories.Implement
{
    public class PurchaseOrderDataFileRepository : IPurchaseOrderDataFileRepository
    {
        private readonly AldebaranShippingContext _context;
        public PurchaseOrderDataFileRepository(AldebaranShippingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<PurchaseOrderDataFile>> Get(int purchaseId, CancellationToken ct = default)
        {
            return await _context.PurchaseOrderDataFiles.Where(w => w.PurchaseId == purchaseId).ToListAsync(ct);
        }
    }
}
