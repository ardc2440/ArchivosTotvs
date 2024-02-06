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
                .ToTable("erp_shipping_process_detail")
                .HasKey(k => k.Id);

            builder.Property(e => e.Id).HasColumnName("ERP_SHIPPING_PROCESS_DETAIL_ID");
            builder.Property(e => e.ShippingProcessId).HasColumnName("ERP_SHIPPING_PROCESS_ID");
            builder.Property(e => e.DocumentTypeId).HasColumnName("DOCUMENT_TYPE_ID").HasMaxLength(10);
            builder.Property(e => e.DocumentId).HasColumnName("DOCUMENT_ID");
            builder.Property(e => e.FileName).HasColumnName("DOCUMENT_FILE_NAME");
            
            builder.HasOne(d => d.ShippingProcess).WithMany(p => p.ProcessDetails)
                .HasForeignKey(d => d.ShippingProcessId);

            builder.HasOne(d => d.DocumentType).WithMany(p => p.ShippingProcessDetails)
                .HasForeignKey(d => d.DocumentTypeId);
        }
    }
}
