using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Data.Configuration
{
    internal class SaleOrderDataFileConfiguration : IEntityTypeConfiguration<SaleOrderDataFile>
    {
        public void Configure(EntityTypeBuilder<SaleOrderDataFile> builder)
        {
            builder
                .ToView("ERPSaleOrderFile")
                .HasNoKey();//.HasKey(k => new { k.SaleId, k.SaleDetailId });

            builder.Property(e => e.Type).HasColumnName("DocumentType");
            builder.Property(e => e.SaleNumber).HasColumnName("SaleNumber");
            builder.Property(e => e.ClientIdentificationType).HasColumnName("ClientIdentificationType");
            builder.Property(e => e.ClientIdentificationNumber).HasColumnName("ClientIdentificationNumber");
            builder.Property(e => e.CustomerObservations).HasColumnName("CustomerObservations");
            builder.Property(e => e.InternalObservations).HasColumnName("InternalObservations");
            builder.Property(e => e.EstimatedDeliveryDate).HasColumnName("EstimatedDeliveryDate");
            builder.Property(e => e.SaleId).HasColumnName("SaleId");
            builder.Property(e => e.LineCode).HasColumnName("LineCode");
            builder.Property(e => e.ItemCode).HasColumnName("ItemCode");
            builder.Property(e => e.ReferenceCode).HasColumnName("ReferenceCode"); 
            builder.Property(e => e.DeliveryQuantity).HasColumnName("DeliveryQuantity");
            builder.Property(e => e.RequestedQuantity).HasColumnName("RequestedQuantity");
            builder.Property(e => e.SaleState).HasColumnName("SaleState").HasColumnType("CHAR(1)"); 
        }
    }
}
