using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Data.Repositories.Interface
{
    public interface IDocumentTypeRepository
    {
        Task<DocumentType> Find(string type, CancellationToken ct = default);
        Task<DocumentType> Find(int id, CancellationToken ct = default);
        void Update(DocumentType entity);
    }
}
