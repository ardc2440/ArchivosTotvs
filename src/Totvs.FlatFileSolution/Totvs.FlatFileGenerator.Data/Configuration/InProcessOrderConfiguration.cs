using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Data.Configuration
{
    internal class InProcessOrderConfiguration : IEntityTypeConfiguration<InProcessOrder>
    {
        public void Configure(EntityTypeBuilder<InProcessOrder> builder)
        {
            builder
                .ToView("erp_in_process_orders")
                .HasNoKey();

            builder.Property(e => e.Id).HasColumnName("INPROCESSID");
            builder.Property(e => e.Type).HasColumnName("ACTIONTYPE");
            builder.Property(e => e.Date).HasColumnName("ACTIONDATE");
        }
    }
}