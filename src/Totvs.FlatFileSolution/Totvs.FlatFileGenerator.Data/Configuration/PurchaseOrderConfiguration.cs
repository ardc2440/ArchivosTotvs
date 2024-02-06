using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Data.Configuration
{
    internal class PurchaseOrderConfiguration : IEntityTypeConfiguration<PurchaseOrder>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
        {
            builder
                .ToView("erp_purchase_orders")
                .HasNoKey();

            builder.Property(e => e.Id).HasColumnName("PURCHASEID");
            builder.Property(e => e.Type).HasColumnName("ACTIONTYPE").HasColumnType("CHAR(1)");
            builder.Property(e => e.Date).HasColumnName("ACTIONDATE");
        }
    }
}
