using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Business.Models;

namespace Totvs.FlatFileGenerator.Engine.Interface
{
    internal interface IFlatFileProcessor
    {
        Task BuildFlatFileAsync(IEnumerable<SaleOrder> saleOrders, CancellationToken ct = default);
    }
}
