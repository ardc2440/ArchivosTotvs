using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Business.Models;

namespace Totvs.FlatFileGenerator.Engine.Interface
{
    public interface IFlatFileProcessor
    {
        ShippingProcess ActualShippingProcess { get; set; }
        Task BuildFlatFileAsync(IEnumerable<SaleOrder> saleOrders, CancellationToken ct = default);
        Task BuildFlatFileAsync(IEnumerable<PurchaseOrder> purchaseOrders, CancellationToken ct = default);
        string FileDirectory();
    }
}
