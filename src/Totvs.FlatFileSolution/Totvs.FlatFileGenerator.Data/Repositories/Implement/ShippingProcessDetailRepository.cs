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
    public class ShippingProcessDetailRepository : IShippingProcessDetailRepository
    {
        private readonly AldebaranShippingContext _context;
        public ShippingProcessDetailRepository(AldebaranShippingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ShippingProcessDetail> Add(ShippingProcessDetail entity)
        {
            _context.ShippingProcessDetails.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<ShippingProcessDetail>> GetAll(CancellationToken ct = default)
        {
            return await _context.ShippingProcessDetails.ToListAsync(ct);
        }

        public async Task<IEnumerable<ShippingProcessDetail>> Get(int processId, CancellationToken ct = default)
        {
            return await _context.ShippingProcessDetails.Where(x=> x.ShippingProcessId.Equals(processId)).ToListAsync(ct);
        }
    }
}
