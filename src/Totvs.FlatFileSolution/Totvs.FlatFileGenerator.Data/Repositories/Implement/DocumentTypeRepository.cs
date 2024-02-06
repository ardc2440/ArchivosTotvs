using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Data.Entities;
using Totvs.FlatFileGenerator.Data.Repositories.Interface;

namespace Totvs.FlatFileGenerator.Data.Repositories.Implement
{
    public class DocumentTypeRepository : IDocumentTypeRepository
    {
        private readonly AldebaranShippingContext _context;
        public DocumentTypeRepository(AldebaranShippingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<DocumentType?> FindByCodeAsync(string code, CancellationToken ct = default)
        {
            return await _context.DocumentTypes.AsNoTracking().FirstOrDefaultAsync(f => f.DocumentTypeCode == code, ct);
        }
    }

}
