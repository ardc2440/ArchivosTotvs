using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Data.Repositories.Interface
{
    public interface IInProcessOrderRepository
    {
        Task<IEnumerable<InProcessOrder>> Get(CancellationToken ct = default);
    }
}