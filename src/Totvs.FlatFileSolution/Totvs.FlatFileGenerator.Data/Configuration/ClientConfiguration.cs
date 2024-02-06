using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Data.Configuration
{
    internal class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder
                .ToTable("customers","dbo")
                .HasKey(k => k.Id);

            builder.Property(e => e.Id).HasColumnName("CUSTOMER_ID");
            builder.Property(e => e.Name).HasColumnName("CUSTOMER_NAME");
        }
    }
}
