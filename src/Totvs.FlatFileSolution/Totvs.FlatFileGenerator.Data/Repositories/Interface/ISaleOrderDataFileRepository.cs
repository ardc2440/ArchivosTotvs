using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Data.Repositories.Interface
{
    public interface ISaleOrderDataFileRepository
    {
        Task<IEnumerable<SaleOrderDataFile>> Get(int saleId, CancellationToken ct = default);
    }
}
