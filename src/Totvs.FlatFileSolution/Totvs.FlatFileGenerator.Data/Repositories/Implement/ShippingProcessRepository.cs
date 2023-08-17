using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Data.Entities;
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

        public async Task<ShippingProcess> Add(ShippingProcess entity)
        {
            _context.ShippingProcesses.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task CleaningShippingDataProcess(DateTime newCleaningDate)
        {
            await _context.Database.ExecuteSqlRawAsync($"EXECUTE PROCEDURE ERPCLEANINGDATAPROCESS (cast('{newCleaningDate:MM/dd/yyyy HH:mm:ss}' as timestamp));");
        }

        public async Task<IEnumerable<ShippingProcess>> Get(CancellationToken ct = default)
        {
            return await _context.ShippingProcesses.ToListAsync(ct);
        }
    }
}
