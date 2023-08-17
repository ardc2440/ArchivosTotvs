using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Business.Services.Implement;
using Totvs.FlatFileGenerator.Business.Services.Interface;
using Totvs.FlatFileGenerator.Config;
using Totvs.FlatFileGenerator.Data;
using Totvs.FlatFileGenerator.Data.Repositories.Implement;
using Totvs.FlatFileGenerator.Data.Repositories.Interface;
using Totvs.FlatFileGenerator.Engine.Implement;
using Totvs.FlatFileGenerator.Engine.Interface;
using Totvs.FlatFileGenerator.Services;

internal class Program
{
    public static async Task Main(string[] args)
    {
        var hostBuilder = new HostBuilder()
           .ConfigureServices((hostContext, services) =>
           {
               ConfigureServices(services);
           });
        hostBuilder.UseWindowsService();
        var host = hostBuilder.Build();
        await host.RunAsync();
    }

    static void ConfigureServices(IServiceCollection services)
    {
        // Config
        var appsettingsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
        IConfiguration config = new ConfigurationBuilder()
           .AddJsonFile(appsettingsFilePath, true, true)
           .Build();
        // Logging
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.SetMinimumLevel(LogLevel.Trace);
            loggingBuilder.AddNLog(config);
        });

        services.AddSingleton(config);
        services.AddOptions();

        services.Configure<FileSettings>(config.GetSection("FileSettings"));
        services.Configure<ScheduleSettings>(config.GetSection("ScheduleSettings"));

        // Context
        services.AddDbContext<AldebaranContext>(opts =>
        {
            opts.UseFirebird(config.GetConnectionString("AldebaranConnection"));
        });

        // Engines
        services.AddSingleton<IFlatFileProcessor, FlatFileProcessor>();

        // Services
        services.AddSingleton<IOrderService, OrderService>();
        services.AddSingleton<ILastDocumentTypeProcessService, LastDocumentTypeProcessService>();
        services.AddSingleton<ISaleOrderService, SaleOrderService>();
        services.AddSingleton<IPurchaseOrderService, PurchaseOrderService>();
        services.AddSingleton<IShippingProcessService, ShippingProcessService>();

        // Repositories
        services.AddSingleton<IOrderRepository, OrderRepository>();
        services.AddSingleton<IDocumentTypeRepository, DocumentTypeRepository>();
        services.AddSingleton<ISaleOrderRepository, SaleOrderRepository>();
        services.AddSingleton<ISaleOrderDataFileRepository, SaleOrderDataFileRepository>();
        services.AddSingleton<IPurchaseOrderRepository, PurchaseOrderRepository>();
        services.AddSingleton<IPurchaseOrderDataFileRepository, PurchaseOrderDataFileRepository>();
        services.AddSingleton<IShippingProcessRepository, ShippingProcessRepository>();
        services.AddSingleton<IShippingProcessDetailRepository, ShippingProcessDetailRepository>();

        // HostedService
        services.AddHostedService<BackgroundShippingService>();
        services.AddHostedService<BackgroundCleaningService>();
    }
}
