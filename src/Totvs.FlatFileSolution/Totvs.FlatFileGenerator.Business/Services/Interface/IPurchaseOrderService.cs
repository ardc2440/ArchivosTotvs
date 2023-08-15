using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Business.Models;

namespace Totvs.FlatFileGenerator.Business.Services.Interface
{
    public interface IPurchaseOrderService
    {
        Task<IEnumerable<PurchaseOrder>> Get(CancellationToken ct = default);
    }
}
