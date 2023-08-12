using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Business.Models;

namespace Totvs.FlatFileGenerator.Business.Services.Interface
{
    public interface ISaleOrderService
    {
        Task<IEnumerable<SaleOrder>> Get(CancellationToken ct = default);
    }
}
