using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Business.Models;
using Totvs.FlatFileGenerator.Business.Services.Interface;
using Totvs.FlatFileGenerator.Data.Repositories.Interface;

namespace Totvs.FlatFileGenerator.Business.Services.Implement
{
    public class SaleOrderService : ISaleOrderService
    {
        private readonly ISaleOrderRepository _soRepository;
        private readonly ISaleOrderDataFileRepository _sodfRepository;
        public SaleOrderService(ISaleOrderRepository soRepository, ISaleOrderDataFileRepository sodfRepository)
        {
            _soRepository = soRepository ?? throw new ArgumentNullException(nameof(ISaleOrderRepository));
            _sodfRepository = sodfRepository ?? throw new ArgumentNullException(nameof(ISaleOrderDataFileRepository));
        }
        public async Task<IEnumerable<SaleOrder>> Get(CancellationToken ct = default)
        {
            var saleOrders = await _soRepository.Get(ct);
            
            return saleOrders.Select(async s =>
            {
                var sodts = await _sodfRepository.Get(s.Id, ct);
                var so = (SaleOrder)s;
                so.Details = sodts.Select(s => (SaleOrderDetail)s);
                return so;
            }).Select(s => s.Result);
        }
    }
}
