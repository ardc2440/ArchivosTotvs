using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Data.Repositories.Interface
{
    public interface IDocumentTypeRepository
    {
        Task<DocumentType?> FindByCodeAsync(string code, CancellationToken ct = default);
    }

}
