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
    public class ShippingProcessService : IShippingProcessService
    {
        private readonly IShippingProcessRepository _spRepository;
        private readonly IShippingProcessDetailRepository _spdRepository;
        public ShippingProcessService(IShippingProcessRepository spRepository, IShippingProcessDetailRepository spdRepository)
        {
            _spRepository = spRepository ?? throw new ArgumentNullException(nameof(IShippingProcessRepository));
            _spdRepository = spdRepository ?? throw new ArgumentNullException(nameof(IShippingProcessDetailRepository));
        }

        public async Task<ShippingProcess> Add(ShippingProcess entity, CancellationToken ct = default)
        {
            var shippingProcess = new Data.Entities.ShippingProcess()
            {
                Path = entity.Path,
                Date = entity.Date                
            };
        
            var result = await _spRepository.Add(shippingProcess);

            return (ShippingProcess)result;
        }

        public async Task<ShippingProcessDetail> Add(ShippingProcessDetail entity, CancellationToken ct = default)
        {
            var shippingProcessDetail = new Data.Entities.ShippingProcessDetail()
            {
                ShippingProcessId = entity.ShippingProcessId,
                DocumentId = entity.DocumentId, 
                DocumentTypeId = entity.DocumentTypeId,
                FileName = entity.FileName                
            };

            var result = await _spdRepository.Add(shippingProcessDetail);

            return (ShippingProcessDetail)result;
        }

        public async Task<IEnumerable<ShippingProcess>> Get(CancellationToken ct = default)
        {
            var shippingProcess = await _spRepository.Get(ct);

            return shippingProcess.Select(async s =>
            {
                var sodts = await _spdRepository.Get(s.Id, ct);
                var so = (ShippingProcess)s;
                so.Details = sodts.Select(s => (ShippingProcessDetail)s);
                return so;
            }).Select(s => s.Result);
        }
    }
}
