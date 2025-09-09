using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<FlatFileProcessor> _logger;
        private readonly FileSettings _settings;
        private readonly IShippingProcessService _shippingProcessService;
        private readonly ILastDocumentTypeProcessService _lastDocumentTypeProcessService;


        public ShippingProcess ActualShippingProcess { get; set; }

        public FlatFileProcessor(ILogger<FlatFileProcessor> logger,IOptions<FileSettings> settings, IShippingProcessService shippingProcessService, ILastDocumentTypeProcessService lastDocumentTypeProcessService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(ILogger<FlatFileProcessor>)); ;
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

                try
                {
                    _logger.LogInformation($"Generando archivo {filename}. {DateTime.Now:MM/dd/yyyy HH:mm:ss}");

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

                    _logger.LogInformation($"Fin generación archivo {filename}. {DateTime.Now:MM/dd/yyyy HH:mm:ss}");
                }
                catch (Exception ex)
                {
                    if (File.Exists(destinationPath))
                        File.Delete(destinationPath);
                    _logger.LogError(ex, $"Error al ejecutar FlatFileProcessor.BuildFlatFileAsync con excepción '{ex.Message}' para la orden de venta {saleOrder.Details.First().SaleNumber}.");
                }

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

                try
                {
                    _logger.LogInformation($"Generando archivo {filename}. {DateTime.Now:MM/dd/yyyy HH:mm:ss}");

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

                    _logger.LogInformation($"Fin generación archivo {filename}. {DateTime.Now:MM/dd/yyyy HH:mm:ss}");
                }
                catch (Exception ex)
                {
                    if (File.Exists(destinationPath))
                        File.Delete(destinationPath);
                    _logger.LogError(ex, $"Error al ejecutar FlatFileProcessor.BuildFlatFileAsync con excepción '{ex.Message}' para la orden de compra {purchaseOrder.Details.First().PurchaseNumber}.");
                }
            }
        }

        public async Task BuildFlatFileAsync(IEnumerable<InProcessOrder> inProcessOrders, CancellationToken ct = default)
        {
            foreach (var inProcessOrder in inProcessOrders)
            {
                if (!inProcessOrder.Details.Any())
                    continue;
                
                // Usar CodTipoDocumentOrigen y DocumentoOrigen del primer detalle para nombrar el archivo
                var firstDetail = inProcessOrder.Details.First();
                var filename = $"INPROCESS_{firstDetail.CodTipoDocumentOrigen}_{firstDetail.DocumentoOrigen}_{inProcessOrder.Date.ToString(Global.DateTimeFormat)}.txt";
                string destinationPath = Path.Combine(_settings.DestinationFilePath + "\\Backorder", filename);

                try
                {
                    _logger.LogInformation($"Generando archivo de traslado en proceso {filename}. {DateTime.Now:MM/dd/yyyy HH:mm:ss}");

                    using (var writer = new StreamWriter(destinationPath))
                    {
                        // Línea A: Información general del proceso automático de traslado (SOLO UNA VEZ)
                        var group1Data = firstDetail;
                        await writer.WriteLineAsync($"A|{group1Data.NroProceso}|{group1Data.TipoDocumentOrigen}|{group1Data.DocumentoOrigen}|{group1Data.FechaDocumentoOrigen:yyyyMMdd}");
                        
                        // Agrupar los detalles por CustomerOrderInProcessId para escribir una línea P y sus D asociadas
                        var pedidosAgrupados = inProcessOrder.Details
                            .Where(d => d.CustomerOrderId > 0 && !string.IsNullOrEmpty(d.OrderNumber))
                            .GroupBy(d => d.CustomerOrderInProcessId);
                        
                        foreach (var grupoPedido in pedidosAgrupados)
                        {
                            var pedidoData = grupoPedido.First();
                            string cleanCustomerNotes = (pedidoData.CustomerNotes ?? "").Replace("\r", "").Replace("\n", "");
                            string cleanInternalNotes = (pedidoData.InternalNotes ?? "").Replace("\r", "").Replace("\n", "");
                            await writer.WriteLineAsync($"P|{pedidoData.CustomerOrderInProcessId}|{pedidoData.CustomerOrderId}|{pedidoData.OrderNumber}|{pedidoData.ClientIdentity ?? ""}|{cleanCustomerNotes}|{cleanInternalNotes}");
                            // Líneas D asociadas a este pedido
                            foreach (var detail in grupoPedido)
                            {
                                if (detail.CustomerOrderDetailId > 0)
                                {
                                    await writer.WriteLineAsync($"D|{detail.ActionType}|{detail.CustomerOrderInProcessDetailId}|{detail.CustomerOrderDetailId}|{detail.Quantity}|{detail.LineCode ?? ""}|{detail.ItemCode ?? ""}|{detail.ReferenceCode ?? ""}");
                                }
                            }
                        }
                    }

                    await _shippingProcessService.Add(new ShippingProcessDetail()
                    {
                        ShippingProcessId = ActualShippingProcess.Id,
                        DocumentTypeId = _lastDocumentTypeProcessService.Find("T", ct).Result.Id,
                        DocumentId = inProcessOrder.Id,
                        FileName = filename
                    });

                    _logger.LogInformation($"Fin generación archivo de traslado en proceso {filename}. {DateTime.Now:MM/dd/yyyy HH:mm:ss}");
                }
                catch (Exception ex)
                {
                    if (File.Exists(destinationPath))
                        File.Delete(destinationPath);
                    _logger.LogError(ex, $"Error al ejecutar FlatFileProcessor.BuildFlatFileAsync con excepción '{ex.Message}' para el traslado en proceso {firstDetail.NroProceso}.");
                }
            }
        }
    }
}
