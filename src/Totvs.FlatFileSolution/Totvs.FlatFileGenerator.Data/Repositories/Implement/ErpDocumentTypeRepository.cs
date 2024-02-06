using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Data.Entities;
using Totvs.FlatFileGenerator.Data.Repositories.Interface;

namespace Totvs.FlatFileGenerator.Data.Repositories.Implement
{
    public class ErpDocumentTypeRepository: IErpDocumentTypeRepository
    {
        private readonly AldebaranShippingContext _context;
        public ErpDocumentTypeRepository(AldebaranShippingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ErpDocumentType> Find(string type, CancellationToken ct = default)
        {
            return await _context.ErpDocumentTypes.Include(i=>i.DocumentType).FirstOrDefaultAsync(dt => dt.DocumentType.DocumentTypeCode == type, ct);
        }
        public async Task<ErpDocumentType> Find(int id, CancellationToken ct = default)
        {
            return await _context.ErpDocumentTypes.FirstOrDefaultAsync(dt => dt.Id == id, ct);
        }

        public async Task Update(ErpDocumentType entity)
        {
            _context.ErpDocumentTypes.Update(entity);
            await _context.SaveChangesAsync() ;
        }        
    }
}
