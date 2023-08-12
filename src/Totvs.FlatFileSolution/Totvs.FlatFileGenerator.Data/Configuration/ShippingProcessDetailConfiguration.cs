using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Data.Configuration
{
    internal class ShippingProcessDetailConfiguration : IEntityTypeConfiguration<ShippingProcessDetail>
    {
        public void Configure(EntityTypeBuilder<ShippingProcessDetail> builder)
        {
            builder
                .ToTable("ERPSHIPPINGPROCESSDETAIL")
                .HasKey(k => k.Id);

            builder.Property(e => e.Id).HasColumnName("IDPEDIDO");
            builder.Property(e => e.ShippingProcessId).HasColumnName("IDCLIENTE");
            builder.Property(e => e.DocumentTypeId).HasColumnName("NUMPEDIDO").HasMaxLength(10);
            builder.Property(e => e.DocumentId).HasColumnName("FECHAESTENTREGA");
            builder.Property(e => e.FileName).HasColumnName("ENTREGAPACTADA").HasColumnType("CHAR(1)");
            
            builder.HasOne(d => d.ShippingProcess).WithMany(p => p.ProcessDetails)
                .HasForeignKey(d => d.ShippingProcessId);

            builder.HasOne(d => d.DocumentType).WithMany(p => p.ProcessDetails)
                .HasForeignKey(d => d.DocumentTypeId);
        }
    }
}
