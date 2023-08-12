using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Data.Entities;
using Totvs.FlatFileGenerator.Data.Repositories.Interface;

namespace Totvs.FlatFileGenerator.Data.Repositories.Implement
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AldebaranContext _context;
        public OrderRepository(AldebaranContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Order> Find(string orderNumber, CancellationToken ct = default)
        {
            return await _context.Orders.FirstOrDefaultAsync(w => w.OrderNumber == orderNumber, ct);
        }
    }
}
