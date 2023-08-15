using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Data.Entities;
using Totvs.FlatFileGenerator.Data.Repositories.Interface;

namespace Totvs.FlatFileGenerator.Data.Repositories.Implement
{
    public class DocumentTypeRepository: IDocumentTypeRepository
    {
        private readonly AldebaranContext _context;
        public DocumentTypeRepository(AldebaranContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<DocumentType> Find(string type, CancellationToken ct = default)
        {
            return await _context.DocumentTypes.FirstOrDefaultAsync(dt => dt.CodeType == type, ct);
        }
        public async Task<DocumentType> Find(int id, CancellationToken ct = default)
        {
            return await _context.DocumentTypes.FirstOrDefaultAsync(dt => dt.Id == id, ct);
        }

        public async Task Update(DocumentType entity)
        {
            _context.DocumentTypes.Update(entity);
            await _context.SaveChangesAsync() ;
        }        
    }
}
