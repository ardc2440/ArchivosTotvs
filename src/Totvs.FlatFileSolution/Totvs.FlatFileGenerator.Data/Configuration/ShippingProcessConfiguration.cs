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
    internal class ShippingProcessConfiguration : IEntityTypeConfiguration<ShippingProcess>
    {
        public void Configure(EntityTypeBuilder<ShippingProcess> builder)
        {
            builder
                .ToView("ERPERPShippingProcess")
                .HasKey(k => k.Id);

            builder.Property(e => e.Id).HasColumnName("Id");
            builder.Property(e => e.Path).HasColumnName("DestinationPath");
            builder.Property(e => e.Date).HasColumnName("ExecutionDate");
        }
    }
}
