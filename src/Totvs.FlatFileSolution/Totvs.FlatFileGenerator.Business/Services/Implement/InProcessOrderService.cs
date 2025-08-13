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
    public class InProcessOrderService : IInProcessOrderService
    {
        private readonly IInProcessOrderDataFileRepository _ipodfRepository;
        
        public InProcessOrderService(IInProcessOrderDataFileRepository ipodfRepository)
        {
            _ipodfRepository = ipodfRepository ?? throw new ArgumentNullException(nameof(IInProcessOrderDataFileRepository));
        }
        
        public async Task<IEnumerable<InProcessOrder>> Get(CancellationToken ct = default)
        {
            // Obtener todos los datos del stored procedure
            var allData = await _ipodfRepository.GetAll(ct);
            
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
                    Date = firstRecord.FechaDocumentoOrigen,
                    Details = processGroup.Select(d => (InProcessOrderDetail)d).ToList()
                };
                
                inProcessOrders.Add(inProcessOrder);
            }
            
            return inProcessOrders;
        }
    }
}