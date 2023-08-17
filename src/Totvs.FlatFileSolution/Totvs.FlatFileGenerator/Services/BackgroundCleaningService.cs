using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NCrontab;
using System;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Business.Services.Interface;
using Totvs.FlatFileGenerator.Config;

namespace Totvs.FlatFileGenerator.Services
{
    internal class BackgroundCleaningService : BackgroundService
    {
        private readonly ILogger<BackgroundCleaningService> _logger;
        private readonly IShippingProcessService _shippingProcessService;

        private readonly ScheduleSettings _scheduleSettings;

        // Cron settings
        private CrontabSchedule _schedule;
        private DateTime _nextRun;

        public BackgroundCleaningService(ILogger<BackgroundCleaningService> logger,
            IOptions<ScheduleSettings> scheduleSettings,
            IShippingProcessService shippingProcessService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(ILogger<BackgroundCleaningService>));
            _scheduleSettings = scheduleSettings?.Value ?? throw new ArgumentNullException(nameof(IOptions<ScheduleSettings>));

            _schedule = CrontabSchedule.Parse(_scheduleSettings.CleaningTiming, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
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
                    await Task.Delay(TimeSpan.FromDays(_scheduleSettings.CleaningDelayInDays), ct);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error al ejecutar BackgroundCleaningService con excepción {ex.Message}.");
                }
            }
        }

        async Task ProcessAsync(CancellationToken ct)
        {
            await _shippingProcessService.CleaningShippingDataProcess(DateTime.Now, ct);
        }
    }
}
