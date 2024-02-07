using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Data.Configuration
{
    internal class ShippingProcessConfiguration : IEntityTypeConfiguration<ShippingProcess>
    {
        public void Configure(EntityTypeBuilder<ShippingProcess> builder)
        {
            builder
                .ToTable("erp_shipping_process")
                .HasKey(k => k.Id);

            builder.Property(e => e.Id).HasColumnName("ERP_SHIPPING_PROCESS_ID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(e => e.Path).HasColumnName("DESTINATIONPATH");
            builder.Property(e => e.Date).HasColumnName("EXECUTIONDATE");
        }
    }
}
