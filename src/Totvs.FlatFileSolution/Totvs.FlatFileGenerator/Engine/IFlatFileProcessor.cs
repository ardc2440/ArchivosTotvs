using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Business.Models;

namespace Totvs.FlatFileGenerator.Engine
{
    internal interface IFlatFileProcessor
    {
        Task BuildFlatFileAsync(IEnumerable<Order> orders, CancellationToken ct = default);
    }
}
