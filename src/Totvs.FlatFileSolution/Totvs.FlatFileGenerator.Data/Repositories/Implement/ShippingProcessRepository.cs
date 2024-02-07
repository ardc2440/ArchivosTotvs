using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Data.Entities;
using Totvs.FlatFileGenerator.Data.Repositories.Interface;

namespace Totvs.FlatFileGenerator.Data.Repositories.Implement
{
    public class ShippingProcessRepository : IShippingProcessRepository
    {
        private readonly AldebaranShippingContext _context;
        private readonly AldebaranShippingContext _cleaningContext;

        public ShippingProcessRepository(AldebaranShippingContext context, AldebaranCleaningContext cleaningContext)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _cleaningContext = cleaningContext ?? throw new ArgumentNullException(nameof(cleaningContext));
        }

        public async Task<ShippingProcess> Add(ShippingProcess entity)
        {
            _context.ShippingProcesses.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task CleaningShippingDataProcess(DateTime newCleaningDate, CancellationToken ct)
        {
            await _cleaningContext.Database.ExecuteSqlRawAsync($"EXEC SP_ERP_CLEANING_DATA_PROCESS '{newCleaningDate:yyyyMMdd HH:mm:ss}'", ct);
        }

        public async Task<IEnumerable<ShippingProcess>> Get(CancellationToken ct = default)
        {
            return await _context.ShippingProcesses.ToListAsync(ct);
        }
    }
}
