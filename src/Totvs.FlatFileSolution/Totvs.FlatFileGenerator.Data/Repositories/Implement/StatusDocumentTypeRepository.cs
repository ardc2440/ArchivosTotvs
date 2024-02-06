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
    public class StatusDocumentTypeRepository : IStatusDocumentTypeRepository
    {
        private readonly AldebaranShippingContext _context;
        public StatusDocumentTypeRepository(AldebaranShippingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<StatusDocumentType?> FindByDocumentAndOrderAsync(int documentTypeId, int order, CancellationToken ct = default)
        {
            return await _context.StatusDocumentTypes.AsNoTracking().FirstOrDefaultAsync(f => f.DocumentTypeId == documentTypeId && f.StatusOrder == order, ct);
        }

        public async Task<IEnumerable<StatusDocumentType>> GetByDocumentTypeIdAsync(int documentTypeId, CancellationToken ct = default)
        {
            return await _context.StatusDocumentTypes.AsNoTracking()
                .Where(f => f.DocumentTypeId == documentTypeId)
                .ToListAsync(ct);
        }

        public async Task<StatusDocumentType?> FindAsync(int statusDocumentTypeId, CancellationToken ct = default)
        {
            return await _context.StatusDocumentTypes.AsNoTracking().FirstOrDefaultAsync(f => f.StatusDocumentTypeId == statusDocumentTypeId, ct);
        }
    }

}
