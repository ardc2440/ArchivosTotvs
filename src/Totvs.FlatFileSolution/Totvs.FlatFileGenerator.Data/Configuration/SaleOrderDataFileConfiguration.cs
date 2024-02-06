using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Data.Configuration
{
    internal class SaleOrderDataFileConfiguration : IEntityTypeConfiguration<SaleOrderDataFile>
    {
        public void Configure(EntityTypeBuilder<SaleOrderDataFile> builder)
        {
            builder
                .ToView("erp_sale_order_file")
                .HasNoKey();

            builder.Property(e => e.Type).HasColumnName("DOCUMENTTYPE");
            builder.Property(e => e.SaleNumber).HasColumnName("SALENUMBER");
            builder.Property(e => e.ClientIdentificationType).HasColumnName("CLIENTIDENTIFICATIONTYPE");
            builder.Property(e => e.ClientIdentificationNumber).HasColumnName("CLIENTIDENTIFICATIONNUMBER");
            builder.Property(e => e.CustomerObservations).HasColumnName("CUSTOMEROBSERVATIONS");
            builder.Property(e => e.InternalObservations).HasColumnName("INTERNALOBSERVATIONS");
            builder.Property(e => e.EstimatedDeliveryDate).HasColumnName("ESTIMATEDDELIVERYDATE");
            builder.Property(e => e.SaleId).HasColumnName("SALEID");
            builder.Property(e => e.SaleDetailId).HasColumnName("SALEDETAILID");
            builder.Property(e => e.LineCode).HasColumnName("LINECODE");
            builder.Property(e => e.ItemCode).HasColumnName("ITEMCODE");
            builder.Property(e => e.ReferenceCode).HasColumnName("REFERENCECODE"); 
            builder.Property(e => e.DeliveryQuantity).HasColumnName("DELIVERYQUANTITY");
            builder.Property(e => e.RequestedQuantity).HasColumnName("REQUESTEDQUANTITY");
            builder.Property(e => e.SaleState).HasColumnName("SALESTATE").HasColumnType("CHAR(1)"); 
        }
    }
}
