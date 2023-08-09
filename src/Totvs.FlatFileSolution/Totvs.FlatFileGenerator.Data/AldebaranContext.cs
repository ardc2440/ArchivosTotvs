using FirebirdSql.Data.FirebirdClient;
using Microsoft.EntityFrameworkCore;
using Totvs.FlatFileGenerator.Data.Configuration;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Data
{
    public class AldebaranContext : DbContext
    {
        public AldebaranContext(DbContextOptions<AldebaranContext> options)
            : base(options)
        {
        }

        #region config
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
        }
        #endregion

        #region entities
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Order> Orders => Set<Order>();
        #endregion
    }
}
