using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Data.Configuration
{
    internal class DocumentTypeConfiguration: IEntityTypeConfiguration<DocumentType>
    {
        public void Configure(EntityTypeBuilder<DocumentType> builder)
        {
            builder
                .ToTable("ERPDocumentType")
                .HasKey(k => k.Id);

            builder.Property(e => e.Id).HasColumnName("ID");
            builder.Property(e => e.Name).HasColumnName("Name");
            builder.Property(e => e.CodeType).HasColumnName("CodeType").HasColumnType("CHAR(1)");
            builder.Property(e => e.LastExecutionDate).HasColumnName("LastExecutionDate");
            builder.Property(e => e.LastCleaningDate).HasColumnName("LastCleaningDate");
        }
    }
}
