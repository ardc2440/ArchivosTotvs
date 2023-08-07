using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NCrontab;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Business.Models;
using Totvs.FlatFileGenerator.Business.Services;
using Totvs.FlatFileGenerator.Engine;
using Totvs.FlatFileGenerator.Models;

namespace Totvs.FlatFileGenerator.Services
{
    internal class BackgroundWorkerService : BackgroundService
    {
        private readonly ILogger<BackgroundWorkerService> _logger;
        private readonly IOrderService _orderService;
        private readonly ScheduleSettings _scheduleSettings;
        private readonly IFlatFileProcessor _flatFileProcessor;
        // Cron settings
        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        public BackgroundWorkerService(ILogger<BackgroundWorkerService> logger, 
            IOptions<ScheduleSettings> scheduleSettings, 
            IOrderService orderService,
            IFlatFileProcessor flatFileProcessor)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(ILogger<BackgroundWorkerService>));
            _scheduleSettings = scheduleSettings?.Value ?? throw new ArgumentNullException(nameof(IOptions<ScheduleSettings>));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(IOrderService));
            _flatFileProcessor = flatFileProcessor ?? throw new ArgumentNullException(nameof(IFlatFileProcessor));

            _schedule = CrontabSchedule.Parse(_scheduleSettings.Timing, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
        }
        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    var now = DateTime.Now;
                    var nextrun = _schedule.GetNextOccurrence(now);
                    if (now > _nextRun)
                    {
                        await ProcessAsync(ct);
                        _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                    }
                    await Task.Delay(TimeSpan.FromSeconds(_scheduleSettings.DelayInSeconds), ct);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al ejecutar BackgroundWorkerService con excepción {ex.Message}.");
            }
        }
        async Task ProcessAsync(CancellationToken ct)
        {
            var order = await _orderService.Find("0000476095", ct);
            var l = new List<Order> { order };
            await _flatFileProcessor.BuildFlatFileAsync(l, ct);
            Console.WriteLine("hello world " + DateTime.Now.ToString("F") + " Order: " + order.OrderNumber);
        }
    }
}
