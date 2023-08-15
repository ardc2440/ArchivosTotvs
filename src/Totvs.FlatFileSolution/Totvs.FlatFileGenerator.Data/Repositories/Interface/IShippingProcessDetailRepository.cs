using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Data.Repositories.Interface
{
    public interface IShippingProcessDetailRepository
    {
        Task<IEnumerable<ShippingProcessDetail>> GetAll(CancellationToken ct = default);
        Task<IEnumerable<ShippingProcessDetail>> Get(int processId, CancellationToken ct = default);
        Task<ShippingProcessDetail> Add(ShippingProcessDetail entity);
    }
}
