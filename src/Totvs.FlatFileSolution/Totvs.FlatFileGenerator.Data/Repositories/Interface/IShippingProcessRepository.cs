using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Data.Repositories.Interface
{
    public interface IShippingProcessRepository
    {
        Task<IEnumerable<ShippingProcess>> Get(CancellationToken ct = default);
        Task<ShippingProcess> Add(ShippingProcess entity);
        Task CleaningShippingDataProcess(DateTime newCleaningDate, CancellationToken ct);
    }
}
