using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Data.Configuration
{
    internal class SaleOrderConfiguration : IEntityTypeConfiguration<SaleOrder>
    {
        public void Configure(EntityTypeBuilder<SaleOrder> builder)
        {
            builder
                .ToView("ERPSALEORDERS")
                .HasNoKey();

            builder.Property(e => e.Id).HasColumnName("SALEID");
            builder.Property(e => e.Type).HasColumnName("ACTIONTYPE");
            builder.Property(e => e.Date).HasColumnName("ACTIONDATE");
        }
    }
}
