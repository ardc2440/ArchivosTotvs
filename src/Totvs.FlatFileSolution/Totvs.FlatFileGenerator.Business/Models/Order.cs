namespace Totvs.FlatFileGenerator.Business.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; } = null!;
        public short Status { get; set; } 

        public static implicit operator Order(Data.Entities.Order order)
        {
            if (order == null) return null!;
            return new Order
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                Status = order.Status
            };
        }
    }
}