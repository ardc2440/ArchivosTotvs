using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Business.Services;
using Totvs.FlatFileGenerator.Data;
using Totvs.FlatFileGenerator.Data.Repositories;
using Totvs.FlatFileGenerator.Engine;
using Totvs.FlatFileGenerator.Models;
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
        var appsettingsDevFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.Development.json");
        IConfiguration config = new ConfigurationBuilder()
           .AddJsonFile(appsettingsFilePath, true, true)
           .AddJsonFile(appsettingsDevFilePath, optional: true, reloadOnChange: true)
           .Build();
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
        services.AddSingleton<IFileOperations, FileOperations>();
        services.AddSingleton<IFlatFileProcessor, FlatFileProcessor>();

        // Repositories
        services.AddSingleton<IOrderRepository, OrderRepository>();
        // Services
        services.AddSingleton<IOrderService, OrderService>();
        // HostedService
        services.AddHostedService<BackgroundWorkerService>();
    }
}
