using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NCrontab;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Business.Models;
using Totvs.FlatFileGenerator.Business.Services.Interface;
using Totvs.FlatFileGenerator.Config;
using Totvs.FlatFileGenerator.Engine.Interface;

namespace Totvs.FlatFileGenerator.Services
{
    internal partial class BackgroundShippingService : BackgroundService
    {
        private readonly ILogger<BackgroundShippingService> _logger;
        private readonly IOrderService _orderService;
        private readonly ILastDocumentTypeProcessService _lastDocumentTypeProcessService;
        private readonly ISaleOrderService _saleOrderService;
        private readonly IPurchaseOrderService _purchaseOrderService;
        private readonly IInProcessOrderService _inProcessOrderService;
        private readonly IShippingProcessService _shippingProcessService;


        private readonly ScheduleSettings _scheduleSettings;
        private readonly IFlatFileProcessor _flatFileProcessor;
        // Cron settings
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        private DateTime _lastRun;

        internal IEnumerable<SaleOrder> _so { get; set; }
        internal IEnumerable<PurchaseOrder> _po { get; set; }
        internal IEnumerable<InProcessOrder> _ipo { get; set; }

        public BackgroundShippingService(ILogger<BackgroundShippingService> logger,
            IOptions<ScheduleSettings> scheduleSettings,
            IOrderService orderService,
            IFlatFileProcessor flatFileProcessor,
            ILastDocumentTypeProcessService lastDocumentTypeProcessService,
            ISaleOrderService saleOrderService,
            IPurchaseOrderService purchaseOrderService,
            IInProcessOrderService inProcessOrderService,
            IShippingProcessService shippingProcessService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(ILogger<BackgroundShippingService>));
            _scheduleSettings = scheduleSettings?.Value ?? throw new ArgumentNullException(nameof(IOptions<ScheduleSettings>));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(IOrderService));
            _flatFileProcessor = flatFileProcessor ?? throw new ArgumentNullException(nameof(IFlatFileProcessor));
            _saleOrderService = saleOrderService ?? throw new ArgumentNullException(nameof(ISaleOrderService));

            _schedule = CrontabSchedule.Parse(_scheduleSettings.ExecuteTiming, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
            _lastDocumentTypeProcessService = lastDocumentTypeProcessService;
            _purchaseOrderService = purchaseOrderService;
            _inProcessOrderService = inProcessOrderService;
            _shippingProcessService = shippingProcessService;
        }
        
        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                try
                {
                    var now = DateTime.Now;
                    var nextrun = _schedule.GetNextOccurrence(now);
                    if (now > _nextRun)
                    {
                        await ProcessAsync(ct);
                        _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                    }
                    await Task.Delay(TimeSpan.FromSeconds(_scheduleSettings.ExecuteDelayInSeconds), ct);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error al ejecutar BackgroundShippingService con excepción {ex.Message}.");
                }
            }
        }

        async Task<ShippingProcess> GetActualProcessAsync(CancellationToken ct)
        {
            _logger.LogInformation($"Valida existencia de información para envío");

            _lastRun = DateTime.Now;

            _so = await _saleOrderService.Get(ct);
            _po = await _purchaseOrderService.Get(ct);
            _ipo = await _inProcessOrderService.Get(ct);

            if (!_so.Any() && !_po.Any() && !_ipo.Any())
            {
                _logger.LogInformation($"no existe informacion para envío");

                return null;
            }

            var soIds = "Pedidos a enviar: " + string.Join(", ", _so.Select(o => $"{o.Type}:{o.Id}"));
            var poIds = "Ordenes a enviar: " + string.Join(", ", _po.Select(o => $"{o.Type}:{o.Id}"));
            var ipoIds = "Traslados en proceso a enviar: " + string.Join(", ", _ipo.Select(o => $"{o.Type}:{o.Id}"));

            _logger.LogInformation(soIds);
            _logger.LogInformation(poIds);
            _logger.LogInformation(ipoIds);
            
            _flatFileProcessor.ActualShippingProcess = await _shippingProcessService.Add(new ShippingProcess() { Date = _lastRun, Path = _flatFileProcessor.FileDirectory() });

            return _flatFileProcessor.ActualShippingProcess;
        }

        async Task ProcessAsync(CancellationToken ct)
        {
            if (await GetActualProcessAsync(ct) == null)
                return;

            await ProcessAsyncSalesOrders(ct);
            await ProcessAsyncPurchaseOrders(ct);
            await ProcessAsyncInProcessOrders(ct);
        }

        async Task ProcessAsyncSalesOrders(CancellationToken ct)
        {
            if (!_so.Any())
                return;

            _logger.LogInformation($"Generando archivos para Pedidos {DateTime.Now:MM/dd/yyyy HH:mm:ss}");

            var fechaUltProceso = _lastRun;// DateTime.Now;

            await _flatFileProcessor.BuildFlatFileAsync(_so, ct);

            var lastDocumentTypeProcess = await _lastDocumentTypeProcessService.Find("P", ct);
            lastDocumentTypeProcess.LastExecutionDate = fechaUltProceso;
            await _lastDocumentTypeProcessService.Update(lastDocumentTypeProcess, ct);

            _logger.LogInformation($"Finaliza generación archivos para pedidos {DateTime.Now:MM/dd/yyyy HH:mm:ss}");

        }
        async Task ProcessAsyncPurchaseOrders(CancellationToken ct)
        {
            if (!_po.Any())
                return;

            _logger.LogInformation($"Generando archivos para ordenes de compra {DateTime.Now:MM/dd/yyyy HH:mm:ss}");

            var fechaUltProceso = _lastRun;//DateTime.Now;

            await _flatFileProcessor.BuildFlatFileAsync(_po, ct);

            var lastDocumentTypeProcess = await _lastDocumentTypeProcessService.Find("O", ct);
            lastDocumentTypeProcess.LastExecutionDate = fechaUltProceso;
            await _lastDocumentTypeProcessService.Update(lastDocumentTypeProcess, ct);

            _logger.LogInformation($"Finaliza generación de archivos para ordenes de compra {DateTime.Now:MM/dd/yyyy HH:mm:ss}");

        }
        async Task ProcessAsyncInProcessOrders(CancellationToken ct)
        {
            if (!_ipo.Any())
                return;

            _logger.LogInformation($"Generando archivos para Traslados en Proceso {DateTime.Now:MM/dd/yyyy HH:mm:ss}");

            var fechaUltProceso = _lastRun;

            await _flatFileProcessor.BuildFlatFileAsync(_ipo, ct);

            var lastDocumentTypeProcess = await _lastDocumentTypeProcessService.Find("T", ct);
            lastDocumentTypeProcess.LastExecutionDate = fechaUltProceso;
            await _lastDocumentTypeProcessService.Update(lastDocumentTypeProcess, ct);

            _logger.LogInformation($"Finaliza generación de archivos para Traslados en Proceso {DateTime.Now:MM/dd/yyyy HH:mm:ss}");
        }
    }
}
