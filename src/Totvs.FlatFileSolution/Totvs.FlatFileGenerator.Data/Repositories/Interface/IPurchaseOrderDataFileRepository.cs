using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Data.Repositories.Interface
{
    public interface IPurchaseOrderDataFileRepository
    {
        Task<IEnumerable<PurchaseOrderDataFile>> Get(int purchaseId, CancellationToken ct = default);
    }
}
