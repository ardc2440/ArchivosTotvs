using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Data.Configuration
{
    internal class InProcessOrderDataFileConfiguration : IEntityTypeConfiguration<InProcessOrderDataFile>
    {
        public void Configure(EntityTypeBuilder<InProcessOrderDataFile> builder)
        {
            builder
                .HasNoKey(); // Resultado de stored procedure, no tiene key

            // Mapeo exacto de las 15 columnas que retorna el SP
            builder.Property(e => e.NroProceso).HasColumnName("NroProceso");
            builder.Property(e => e.TipoDocumentOrigen).HasColumnName("TipoDocumentOrigen");
            builder.Property(e => e.DocumentoOrigen).HasColumnName("DocumentoOrigen");
            builder.Property(e => e.FechaDocumentoOrigen).HasColumnName("FechaDocumentoOrigen");
            builder.Property(e => e.CustomerOrderInProcessId).HasColumnName("CustomerOrderInProcessId");
            builder.Property(e => e.CustomerOrderDetailId).HasColumnName("CustomerOrderDetailId");
            builder.Property(e => e.Quantity).HasColumnName("Quantity");
            builder.Property(e => e.CustomerOrderId).HasColumnName("CustomerOrderId");
            builder.Property(e => e.OrderNumber).HasColumnName("OrderNumber");
            builder.Property(e => e.ClientIdentity).HasColumnName("ClientIdentity");
            builder.Property(e => e.CustomerNotes).HasColumnName("CustomerNotes");
            builder.Property(e => e.InternalNotes).HasColumnName("InternalNotes");
            builder.Property(e => e.LineCode).HasColumnName("LineCode");
            builder.Property(e => e.ItemCode).HasColumnName("ItemCode");
            builder.Property(e => e.ReferenceCode).HasColumnName("ReferenceCode");
        }
    }
}