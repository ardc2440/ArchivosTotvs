using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Data.Entities;
using Totvs.FlatFileGenerator.Data.Repositories.Interface;

namespace Totvs.FlatFileGenerator.Data.Repositories.Implement
{
    public class SaleOrderDataFileRepository : ISaleOrderDataFileRepository
    {
        private readonly AldebaranShippingContext _context;
        public SaleOrderDataFileRepository(AldebaranShippingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<SaleOrderDataFile>> Get(int saleId, CancellationToken ct = default)
        {
            return await _context.SaleOrderDataFiles
                    .Where(w => w.SaleId == saleId)
                    .Select(s=> new SaleOrderDataFile { 
                        Type= s.Type,
                        SaleId= s.SaleId,
                        ClientIdentificationNumber= s.ClientIdentificationNumber,   
                        ClientIdentificationType= s.ClientIdentificationType,   
                        CustomerObservations= s.CustomerObservations,   
                        DeliveryQuantity= s.DeliveryQuantity,   
                        EstimatedDeliveryDate= s.EstimatedDeliveryDate, 
                        InternalObservations= s.InternalObservations,   
                        ItemCode= s.ItemCode,   
                        LineCode= s.LineCode,   
                        ReferenceCode   = s.ReferenceCode,
                        RequestedQuantity= s.RequestedQuantity,
                        SaleDetailId= s.SaleDetailId,
                        SaleNumber= 'W'+s.SaleNumber,
                        SaleState= s.SaleState                        
                    }).ToListAsync(ct);
        }
    }
}
