using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Data.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> Find(string orderNumber, CancellationToken ct = default);
    }
}
