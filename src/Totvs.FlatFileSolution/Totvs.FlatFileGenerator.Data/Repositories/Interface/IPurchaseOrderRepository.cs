using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Data.Repositories.Interface
{
    public interface IPurchaseOrderRepository
    {
        Task<IEnumerable<PurchaseOrder>> Get(CancellationToken ct = default);
    }
}