using System;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Business.Models;
using Totvs.FlatFileGenerator.Data.Repositories;

namespace Totvs.FlatFileGenerator.Business.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        public OrderService(IOrderRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<Order> Find(string orderNumber, CancellationToken ct = default)
        {
            var order = await _repository.Find(orderNumber, ct);
            return (Order)order;
        }
    }
}
