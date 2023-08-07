using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Business.Models;

namespace Totvs.FlatFileGenerator.Business.Services
{
    public interface IOrderService
    {
        Task<Order> Find(string orderNumber, CancellationToken ct = default);
    }
}
