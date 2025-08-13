using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Business.Models;

namespace Totvs.FlatFileGenerator.Business.Services.Interface
{
    public interface IInProcessOrderService
    {
        Task<IEnumerable<InProcessOrder>> Get(CancellationToken ct = default);
    }
}