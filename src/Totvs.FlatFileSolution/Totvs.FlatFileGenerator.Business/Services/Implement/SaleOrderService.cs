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
            var saleOrderData = saleOrders.Where(s => s.Id == 552978); //temp

            var result = new List<SaleOrder>();
            foreach (var s in saleOrderData)
            {
                var sodts = await _sodfRepository.Get(s.Id);
                var so = (SaleOrder)s;
                so.Details = sodts.Select(s => (SaleOrderDetail)s).ToList();
                result.Add(so);
            }
            return result;
            //var xx= saleOrderData.Select(async s =>
            //{
            //    var sodts = await _sodfRepository.Get(s.Id, ct);
            //    var so = (SaleOrder)s;
            //    so.Details = sodts.Select(s => (Detail)s);
            //    return so;
            //});
            //return xx.Select(s => s.Result);
        }
    }
}
