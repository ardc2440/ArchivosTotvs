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
                .ToTable("PEDIDOS")
                .HasKey(k => k.Id);

            builder.Property(e => e.Id).HasColumnName("IDPEDIDO");
            builder.Property(e => e.ClientId).HasColumnName("IDCLIENTE");
            builder.Property(e => e.OrderNumber).HasColumnName("NUMPEDIDO").HasMaxLength(10);
            builder.Property(e => e.DeliveryDate).HasColumnName("FECHAESTENTREGA");
            builder.Property(e => e.AgreedDelivery).HasColumnName("ENTREGAPACTADA").HasColumnType("CHAR(1)");
            builder.Property(e => e.Comments).HasColumnName("OBSERVACIONES").HasMaxLength(250);
            builder.Property(e => e.StaffId).HasColumnName("IDFUNCIONARIO");
            builder.Property(e => e.Status).HasColumnName("ESTADO").HasColumnType("CHAR(1)");
            builder.Property(e => e.OrderDate).HasColumnName("FECHACREACION");
            builder.Property(e => e.CustomerNotes).HasColumnName("OBSERVACIONESCLIENTE").HasMaxLength(250);

            //builder.HasOne(d => d.Client).WithMany(p => p.Orders)
            //    .HasForeignKey(d => d.ClientId);
        }
    }
}
