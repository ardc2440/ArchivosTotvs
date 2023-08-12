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
    internal class SaleOrderConfiguration : IEntityTypeConfiguration<SaleOrder>
    {
        public void Configure(EntityTypeBuilder<SaleOrder> builder)
        {
            builder
                .ToView("ERPSaleOrders")
                .HasNoKey();//.HasKey(k => new { k.Id, k.Type });

            builder.Property(e => e.Id).HasColumnName("SaleId");
            builder.Property(e => e.Type).HasColumnName("ActionType");
            builder.Property(e => e.Date).HasColumnName("ActionDate");
        }
    }
}
