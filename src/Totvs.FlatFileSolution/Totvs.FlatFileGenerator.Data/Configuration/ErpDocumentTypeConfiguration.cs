using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Data.Configuration
{
    internal class ErpDocumentTypeConfiguration: IEntityTypeConfiguration<ErpDocumentType>
    {
        public void Configure(EntityTypeBuilder<ErpDocumentType> builder)
        {
            builder
                .ToTable("erp_document_type","dbo")
                .HasKey(k => k.Id).HasName("PK_ERP_DOCUMENT_TYPE");

            builder.Property(e => e.Id).HasColumnName(@"DOCUMENT_TYPE_ID").HasColumnType("smallint").IsRequired();
            builder.Property(e => e.LastExecutionDate).HasColumnName(@"LAST_EXECUTION_DATE").HasColumnType("datetime").IsRequired();
            builder.Property(e => e.LastCleaningDate).HasColumnName(@"LAST_CLEANING_DATE").HasColumnType("datetime").IsRequired();
            // Foreign keys
            builder.HasOne(a => a.DocumentType).WithMany(b => b.ErpDocumentTypes).HasForeignKey(c => c.Id).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_ERP_DOCUMENT_TYPE_DOCUMENT_TYPE");

        }
    }
}
