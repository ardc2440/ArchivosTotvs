using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Data.Configuration
{
    internal class PurchaseOrderDataFileConfiguration : IEntityTypeConfiguration<PurchaseOrderDataFile>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderDataFile> builder)
        {
            builder
                .ToView("ERPPURCHASEORDERFILE")
                .HasNoKey();

            builder.Property(e => e.Type).HasColumnName("DOCUMENTTYPE");
            builder.Property(e => e.PurchaseNumber).HasColumnName("PURCHASENUMBER");
            builder.Property(e => e.ProviderIdentificationType).HasColumnName("PROVIDERIDENTIFICATIONTYPE");
            builder.Property(e => e.ProviderIdentificationNumber).HasColumnName("PROVIDERIDENTIFICATIONNUMBER");
            builder.Property(e => e.ArrivingEstimatedDate).HasColumnName("ARRIVINGESTIMATEDDATE");
            builder.Property(e => e.ProformaNumber).HasColumnName("PROFORMANUMBER");
            builder.Property(e => e.PurchaseId).HasColumnName("PURCHASEID");
            builder.Property(e => e.PurchaseDetailId).HasColumnName("PURCHASEDETAILID");
            builder.Property(e => e.LineCode).HasColumnName("LINECODE");
            builder.Property(e => e.ItemCode).HasColumnName("ITEMCODE");
            builder.Property(e => e.ReferenceCode).HasColumnName("REFERENCECODE");
            builder.Property(e => e.Quantity).HasColumnName("QUANTITY");
        }
    }
}
