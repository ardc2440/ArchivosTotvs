using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Data.Repositories.Interface
{
    public interface IInProcessOrderDataFileRepository
    {
        Task<IEnumerable<InProcessOrderDataFile>> Get(int inProcessOrderId, CancellationToken ct = default);
        Task<IEnumerable<InProcessOrderDataFile>> GetAll(CancellationToken ct = default);
        Task<IEnumerable<InProcessOrderDataFile>> GetFromDate(DateTime lastExecutionDate, CancellationToken ct = default);
    }
}