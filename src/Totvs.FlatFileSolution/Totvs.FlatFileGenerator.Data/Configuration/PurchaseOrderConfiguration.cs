using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Data.Configuration
{
    internal class PurchaseOrderConfiguration : IEntityTypeConfiguration<PurchaseOrder>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
        {
            builder
                .ToView("ERPPurchaseOrders")
                .HasNoKey();//.HasKey(k => new { k.Id, k.Type });

            builder.Property(e => e.Id).HasColumnName("PurchaseId");
            builder.Property(e => e.Type).HasColumnName("ActionType").HasColumnType("CHAR(1)");
            builder.Property(e => e.Date).HasColumnName("ActionDate");
        }
    }
}
