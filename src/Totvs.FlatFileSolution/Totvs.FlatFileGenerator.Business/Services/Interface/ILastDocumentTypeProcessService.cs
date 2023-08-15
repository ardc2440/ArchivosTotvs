using System.Threading;
using System.Threading.Tasks;

namespace Totvs.FlatFileGenerator.Business.Services.Interface
{
    public interface ILastDocumentTypeProcessService
    {
        Task<Models.LastDocumentTypeProcess> Find(string type, CancellationToken ct = default);
        Task Update(Models.LastDocumentTypeProcess entity, CancellationToken ct = default);
    }
}
