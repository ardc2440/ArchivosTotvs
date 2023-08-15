using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Business.Models;
using Totvs.FlatFileGenerator.Business.Services.Interface;
using Totvs.FlatFileGenerator.Config;
using Totvs.FlatFileGenerator.Engine.Interface;
using Totvs.FlatFileGenerator.Engine.Mappers;

namespace Totvs.FlatFileGenerator.Engine.Implement
{
    internal class FlatFileProcessor : IFlatFileProcessor
    {
        private readonly FileSettings _settings;
        private readonly IShippingProcessService _shippingProcessService;
        private readonly ILastDocumentTypeProcessService _lastDocumentTypeProcessService;


        public ShippingProcess ActualShippingProcess { get; set; }

        public FlatFileProcessor(IOptions<FileSettings> settings, IShippingProcessService shippingProcessService, ILastDocumentTypeProcessService lastDocumentTypeProcessService)
        {
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(IOptions<FileSettings>));
            _shippingProcessService = shippingProcessService ?? throw new ArgumentNullException(nameof(IShippingProcessService));
            _lastDocumentTypeProcessService = lastDocumentTypeProcessService ?? throw new ArgumentNullException(nameof(ILastDocumentTypeProcessService));
        }

        public string FileDirectory()
        {
            return _settings.DestinationFilePath;
        }

        public async Task BuildFlatFileAsync(IEnumerable<SaleOrder> saleOrders, CancellationToken ct = default)
        {
            var csvConfig = new CsvConfiguration(cultureInfo: CultureInfo.InvariantCulture)
            {
                Delimiter = _settings.Delimiter.ToString(),
                HasHeaderRecord = false,
                ShouldQuote = (field) => false
            };

            foreach (var saleOrder in saleOrders)
            {  
                if (!saleOrder.Details.Any())
                    continue;
                var filename = $"{saleOrder.Details.First().SaleNumber}{saleOrder.Type}_{saleOrder.Date.ToString(Global.DateTimeFormat)}.txt";
                string destinationPath = Path.Combine(_settings.DestinationFilePath + "\\Sales", filename);

                using (var writer = new StreamWriter(destinationPath))
                using (var csv = new CsvWriter(writer, csvConfig))
                {
                    csv.Context.RegisterClassMap<SaleOrderHeaderMap>();
                    var header = (SaleOrderHeader)saleOrder;
                    csv.WriteRecord(header);
                    csv.NextRecord();
                    csv.Context.RegisterClassMap<SaleOrderDetailMap>();
                    await csv.WriteRecordsAsync(saleOrder.Details);
                }

                await _shippingProcessService.Add(new ShippingProcessDetail()
                {
                    ShippingProcessId = ActualShippingProcess.Id,
                    DocumentTypeId = _lastDocumentTypeProcessService.Find(saleOrder.Details.First().Type, ct).Result.Id,
                    DocumentId = saleOrder.Id,
                    FileName = filename
                });
            }
        }

        public async Task BuildFlatFileAsync(IEnumerable<PurchaseOrder> purchaseOrders, CancellationToken ct = default)
        {
            var csvConfig = new CsvConfiguration(cultureInfo: CultureInfo.InvariantCulture)
            {
                Delimiter = _settings.Delimiter.ToString(),
                HasHeaderRecord = false,
                ShouldQuote = (field) => false
            };
            foreach (var purchaseOrder in purchaseOrders)
            {
                if (!purchaseOrder.Details.Any())
                    continue;
                var filename = $"{purchaseOrder.Details.First().PurchaseNumber}{purchaseOrder.Type}_{purchaseOrder.Date.ToString(Global.DateTimeFormat)}.txt";
                string destinationPath = Path.Combine(_settings.DestinationFilePath + "\\Purchases", filename);

                using (var writer = new StreamWriter(destinationPath))
                using (var csv = new CsvWriter(writer, csvConfig))
                {
                    csv.Context.RegisterClassMap<PurchaseOrderHeaderMap>();
                    var header = (PurchaseOrderHeader)purchaseOrder;
                    csv.WriteRecord(header);
                    csv.NextRecord();
                    csv.Context.RegisterClassMap<PurchaseOrderDetailMap>();
                    await csv.WriteRecordsAsync(purchaseOrder.Details);
                }

                await _shippingProcessService.Add(new ShippingProcessDetail()
                {
                    ShippingProcessId = ActualShippingProcess.Id,
                    DocumentTypeId = _lastDocumentTypeProcessService.Find(purchaseOrder.Details.First().Type, ct).Result.Id,
                    DocumentId = purchaseOrder.Id,
                    FileName = filename
                });
            }
        }
    }
}
