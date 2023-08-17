using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Business.Models;

namespace Totvs.FlatFileGenerator.Business.Services.Interface
{
    public interface IShippingProcessService
    {
        Task<ShippingProcess> Add(ShippingProcess entity, CancellationToken ct = default);
        Task<ShippingProcessDetail> Add(ShippingProcessDetail entity, CancellationToken ct = default);
        Task<IEnumerable<ShippingProcess>> Get(CancellationToken ct = default);
        Task CleaningShippingDataProcess(DateTime newCleaningDate, CancellationToken ct = default);
    }
}
