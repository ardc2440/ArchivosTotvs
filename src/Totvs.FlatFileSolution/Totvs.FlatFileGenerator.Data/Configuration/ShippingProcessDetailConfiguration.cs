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

            builder.Property(e => e.Id).HasColumnName("ID");
            builder.Property(e => e.ShippingProcessId).HasColumnName("IDERPSHIPPINGPROCESS");
            builder.Property(e => e.DocumentTypeId).HasColumnName("IDERPDOCUMENTTYPE").HasMaxLength(10);
            builder.Property(e => e.DocumentId).HasColumnName("IDDOCUMENT");
            builder.Property(e => e.FileName).HasColumnName("FILENAME");
            
            builder.HasOne(d => d.ShippingProcess).WithMany(p => p.ProcessDetails)
                .HasForeignKey(d => d.ShippingProcessId);

            builder.HasOne(d => d.DocumentType).WithMany(p => p.ProcessDetails)
                .HasForeignKey(d => d.DocumentTypeId);
        }
    }
}
