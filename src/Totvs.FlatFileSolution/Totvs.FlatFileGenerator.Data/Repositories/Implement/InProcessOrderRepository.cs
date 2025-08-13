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
    public class InProcessOrderRepository : IInProcessOrderRepository
    {
        private readonly AldebaranShippingContext _context;
        private readonly IInProcessOrderDataFileRepository _dataFileRepository;
        
        public InProcessOrderRepository(AldebaranShippingContext context, IInProcessOrderDataFileRepository dataFileRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dataFileRepository = dataFileRepository ?? throw new ArgumentNullException(nameof(dataFileRepository));
        }
        
        public async Task<IEnumerable<InProcessOrder>> Get(CancellationToken ct = default)
        {
            // Obtener todos los datos del stored procedure
            var allData = await _dataFileRepository.GetAll(ct);
            
            // Agrupar por NroProceso para crear un InProcessOrder por cada proceso automático
            var groupedByProcess = allData.GroupBy(x => x.NroProceso);
            
            var inProcessOrders = new List<InProcessOrder>();
            
            foreach (var processGroup in groupedByProcess)
            {
                var firstRecord = processGroup.First();
                
                // Crear un InProcessOrder por cada NroProceso (Grupo 1)
                var inProcessOrder = new InProcessOrder
                {
                    Id = firstRecord.NroProceso,
                    Type = "T", // Tipo de documento para traslados
                    Date = firstRecord.FechaDocumentoOrigen
                };
                
                inProcessOrders.Add(inProcessOrder);
            }
            
            return inProcessOrders;
        }
    }
}