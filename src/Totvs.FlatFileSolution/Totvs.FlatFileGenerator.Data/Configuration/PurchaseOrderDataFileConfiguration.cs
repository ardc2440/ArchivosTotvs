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
    internal class PurchaseOrderDataFileConfiguration : IEntityTypeConfiguration<PurchaseOrderDataFile>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderDataFile> builder)
        {
            builder
                .ToView("ERPPurchaseOrderFile")
                .HasNoKey();//.HasKey(k => new { k.PurchaseId, k.PurchaseDetailId });

            builder.Property(e => e.Type).HasColumnName("DocumentType");
            builder.Property(e => e.PurchaseNumber).HasColumnName("PurchaseNumber");
            builder.Property(e => e.ProviderIdentificationType).HasColumnName("ProviderIdentificationType");
            builder.Property(e => e.ProviderIdentificationNumber).HasColumnName("ProviderIdentificationNumber");
            builder.Property(e => e.ArrivingEstimatedDate).HasColumnName("ArrivingEstimatedDate");
            builder.Property(e => e.ProformaNumber).HasColumnName("ProformaNumber");
            builder.Property(e => e.PurchaseId).HasColumnName("PurchaseId");
            builder.Property(e => e.PurchaseDetailId).HasColumnName("PurchaseDetailId");
            builder.Property(e => e.lineCode).HasColumnName("lineCode");
            builder.Property(e => e.ItemCode).HasColumnName("ItemCode");
            builder.Property(e => e.ReferenceCode).HasColumnName("ReferenceCode");
            builder.Property(e => e.Quantity).HasColumnName("Quantity");
        }
    }
}
