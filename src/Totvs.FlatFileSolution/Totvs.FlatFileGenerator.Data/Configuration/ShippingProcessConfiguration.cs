using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Data.Configuration
{
    internal class ShippingProcessConfiguration : IEntityTypeConfiguration<ShippingProcess>
    {
        public void Configure(EntityTypeBuilder<ShippingProcess> builder)
        {
            builder
                .ToView("ERPERPSHIPPINGPROCESS")
                .HasKey(k => k.Id);

            builder.Property(e => e.Id).HasColumnName("ID");
            builder.Property(e => e.Path).HasColumnName("DESTINATIONPATH");
            builder.Property(e => e.Date).HasColumnName("EXECUTIONDATE");
        }
    }
}
