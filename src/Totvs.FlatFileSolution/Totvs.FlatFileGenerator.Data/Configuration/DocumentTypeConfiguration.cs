using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Data.Configuration
{
    internal class DocumentTypeConfiguration: IEntityTypeConfiguration<DocumentType>
    {
        public void Configure(EntityTypeBuilder<DocumentType> builder)
        {
            builder
                .ToTable("ERPDOCUMENTTYPE")
                .HasKey(k => k.Id);

            builder.Property(e => e.Id).HasColumnName("ID");
            builder.Property(e => e.Name).HasColumnName("NAME");
            builder.Property(e => e.CodeType).HasColumnName("CODETYPE").HasColumnType("CHAR(1)");
            builder.Property(e => e.LastExecutionDate).HasColumnName("LASTEXECUTIONDATE");
            builder.Property(e => e.LastCleaningDate).HasColumnName("LASTCLEANINGDATE");
        }
    }
}
