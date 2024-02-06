using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Data.Repositories.Interface
{
    public interface IErpDocumentTypeRepository
    {
        Task<ErpDocumentType> Find(string type, CancellationToken ct = default);
        Task<ErpDocumentType> Find(int id, CancellationToken ct = default);
        Task Update(ErpDocumentType entity);
    }
}
