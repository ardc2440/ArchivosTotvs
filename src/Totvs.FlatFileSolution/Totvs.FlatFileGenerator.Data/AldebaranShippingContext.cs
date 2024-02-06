using Microsoft.EntityFrameworkCore;
using Totvs.FlatFileGenerator.Data.Configuration;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Data
{
    public class AldebaranShippingContext : DbContext
    {
        public AldebaranShippingContext(DbContextOptions<AldebaranShippingContext> options)
            : base(options)
        {
        }

        #region config
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ErpDocumentTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseOrderConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseOrderDataFileConfiguration());
            modelBuilder.ApplyConfiguration(new SaleOrderConfiguration());
            modelBuilder.ApplyConfiguration(new SaleOrderDataFileConfiguration());
            modelBuilder.ApplyConfiguration(new ShippingProcessConfiguration());
            modelBuilder.ApplyConfiguration(new ShippingProcessDetailConfiguration());
            modelBuilder.ApplyConfiguration(new DocumentTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StatusDocumentTypeConfiguration());
        }
        #endregion

        #region entities
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<ErpDocumentType> ErpDocumentTypes => Set<ErpDocumentType>();
        public DbSet<PurchaseOrder> PurchaseOrders => Set<PurchaseOrder>();
        public DbSet<SaleOrder> SaleOrders => Set<SaleOrder>();
        public DbSet<PurchaseOrderDataFile> PurchaseOrderDataFiles => Set<PurchaseOrderDataFile>();
        public DbSet<SaleOrderDataFile> SaleOrderDataFiles => Set<SaleOrderDataFile>();
        public DbSet<ShippingProcess> ShippingProcesses => Set<ShippingProcess>();
        public DbSet<ShippingProcessDetail> ShippingProcessDetails => Set<ShippingProcessDetail>();
        public DbSet<DocumentType> DocumentTypes => Set<DocumentType>();
        public DbSet<StatusDocumentType> StatusDocumentTypes => Set<StatusDocumentType>();

        #endregion
    }
}
