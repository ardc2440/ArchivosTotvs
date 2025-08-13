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
    public class InProcessOrderDataFileRepository : IInProcessOrderDataFileRepository
    {
        private readonly AldebaranShippingContext _context;
        
        public InProcessOrderDataFileRepository(AldebaranShippingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<InProcessOrderDataFile>> Get(int inProcessOrderId, CancellationToken ct = default)
        {
            // Usar FromSqlRaw en lugar de SqlQueryRaw para mejor compatibilidad
            var result = await _context.Set<InProcessOrderDataFile>()
                .FromSqlRaw("EXEC SP_GENERATE_INPROCESS_TOTVS_INTEGRATON_DATA")
                .ToListAsync(ct);

            // Filtrar por el ID específico del traslado en proceso si es necesario
            if (inProcessOrderId > 0)
            {
                result = result.Where(x => x.NroProceso == inProcessOrderId).ToList();
            }

            return result;
        }

        // Método adicional para obtener todos los datos sin filtro de ID específico
        public async Task<IEnumerable<InProcessOrderDataFile>> GetAll(CancellationToken ct = default)
        {
            try
            {
                // Usar FromSqlRaw para ejecutar el stored procedure
                var result = await _context.Set<InProcessOrderDataFile>()
                    .FromSqlRaw("EXEC SP_GENERATE_INPROCESS_TOTVS_INTEGRATON_DATA")
                    .ToListAsync(ct);

                return result;
            }
            catch (Exception ex)
            {
                // Log para debugging
                Console.WriteLine($"Error en GetAll: {ex.Message}");
                throw;
            }
        }

        // Método adicional para obtener datos desde una fecha específica
        public async Task<IEnumerable<InProcessOrderDataFile>> GetFromDate(DateTime lastExecutionDate, CancellationToken ct = default)
        {
            // Como el SP ya maneja internamente la fecha desde ERP_DOCUMENT_TYPE,
            // este método retorna lo mismo que GetAll() pero está disponible para flexibilidad futura
            return await GetAll(ct);
        }
    }
}