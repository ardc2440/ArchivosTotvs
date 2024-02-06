using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Data.Configuration
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder
                .ToTable("customer_orders", "dbo")
                .HasKey(k => k.Id).HasName("PK_CUSTOMER_ORDER");

            builder.Property(e => e.Id).HasColumnName(@"CUSTOMER_ORDER_ID").HasColumnType("int").IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();
            builder.Property(e => e.ClientId).HasColumnName(@"CUSTOMER_ID").HasColumnType("int").IsRequired();
            builder.Property(e => e.OrderNumber).HasColumnName(@"ORDER_NUMBER").HasColumnType("varchar(10)").IsRequired().IsUnicode(false).HasMaxLength(10); ;
            builder.Property(e => e.DeliveryDate).HasColumnName(@"ESTIMATED_DELIVERY_DATE").HasColumnType("date").IsRequired();
            builder.Property(e => e.Comments).HasColumnName(@"INTERNAL_NOTES").HasColumnType("varchar(250)").IsRequired(false).IsUnicode(false).HasMaxLength(250);
            builder.Property(e => e.StaffId).HasColumnName(@"EMPLOYEE_ID").HasColumnType("int").IsRequired();
            builder.Property(e => e.Status).HasColumnName(@"STATUS_DOCUMENT_TYPE_ID").HasColumnType("smallint").IsRequired();
            builder.Property(e => e.OrderDate).HasColumnName(@"CREATION_DATE").HasColumnType("datetime").IsRequired();
            builder.Property(e => e.CustomerNotes).HasColumnName(@"CUSTOMER_NOTES").HasColumnType("varchar(250)").IsRequired(false).IsUnicode(false).HasMaxLength(250);

            builder.HasOne(d => d.Client).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ClientId);
            builder.HasOne(a => a.Client).WithMany(b => b.Orders).HasForeignKey(c => c.ClientId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_CUSTOMER_ORDER_CUSTOMER");
            builder.HasOne(a => a.StatusDocumentType).WithMany(b => b.CustomerOrders).HasForeignKey(c => c.Status).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_CUSTOMER_ORDER_STATUS_DOCUMENT_TYPE");

        }
    }
}
