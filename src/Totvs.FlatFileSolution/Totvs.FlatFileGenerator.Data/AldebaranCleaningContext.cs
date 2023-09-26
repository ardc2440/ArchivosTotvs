using Microsoft.EntityFrameworkCore;
using Totvs.FlatFileGenerator.Data.Configuration;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Data
{
    public class AldebaranCleaningContext : AldebaranShippingContext
    {
        public AldebaranCleaningContext(DbContextOptions<AldebaranShippingContext> options)
            : base(options)
        {
        }       
    }
}
