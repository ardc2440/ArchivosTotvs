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
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IPurchaseOrderRepository _poRepository;
        private readonly IPurchaseOrderDataFileRepository _podfRepository;
        public PurchaseOrderService(IPurchaseOrderRepository poRepository, IPurchaseOrderDataFileRepository podfRepository)
        {
            _poRepository = poRepository ?? throw new ArgumentNullException(nameof(IPurchaseOrderRepository));
            _podfRepository = podfRepository ?? throw new ArgumentNullException(nameof(IPurchaseOrderDataFileRepository));
        }
        public async Task<IEnumerable<PurchaseOrder>> Get(CancellationToken ct = default)
        {
            var purchaseOrders = await _poRepository.Get(ct);
            
            return purchaseOrders.Select(async s =>
            {
                var sodts = await _podfRepository.Get(s.Id, ct);
                var so = (PurchaseOrder)s;
                so.Details = sodts.Select(s => (PurchaseOrderDetail)s);
                return so;
            }).Select(s => s.Result);
        }
    }
}
