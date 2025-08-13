using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Business.Services.Implement;
using Totvs.FlatFileGenerator.Business.Services.Interface;
using Totvs.FlatFileGenerator.Business.Models;
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
        // MODO TEST: Testing completo con servicios reales
        if (args.Length > 0 && args[0] == "--test")
        {
            await RunInProcessTest();
            return;
        }

        var hostBuilder = new HostBuilder()
           .ConfigureServices((hostContext, services) =>
           {
               ConfigureServices(services);
           });
        hostBuilder.UseWindowsService();
        var host = hostBuilder.Build();
        await host.RunAsync();
    }

    static async Task RunInProcessTest()
    {
        Console.WriteLine("=== MODO TEST - GENERACIÓN DE ARCHIVOS INPROCESS (SERVICIOS REALES) ===\n");

        try
        {
            var services = new ServiceCollection();
            ConfigureServicesForTest(services);
            var serviceProvider = services.BuildServiceProvider();

            var inProcessOrderService = serviceProvider.GetRequiredService<IInProcessOrderService>();
            var flatFileProcessor = serviceProvider.GetRequiredService<IFlatFileProcessor>();
            var shippingProcessService = serviceProvider.GetRequiredService<IShippingProcessService>();

            Console.WriteLine("1. Obteniendo datos del SP real...");
            var inProcessOrders = await inProcessOrderService.Get(CancellationToken.None);

            if (!inProcessOrders.Any())
            {
                Console.WriteLine("❌ No se encontraron datos. Verificar SP y datos de prueba.");
                Console.WriteLine("   Ejecutar en BD: EXEC SP_GENERATE_INPROCESS_TOTVS_INTEGRATON_DATA");
                return;
            }

            Console.WriteLine($"✅ Se encontraron {inProcessOrders.Count()} procesos automáticos");
            
            // Mostrar detalles de los procesos encontrados
            foreach (var order in inProcessOrders)
            {
                Console.WriteLine($"   📋 Proceso {order.Id}: {order.Details.Count()} detalles, Fecha: {order.Date:yyyy-MM-dd}");
            }

            // Crear ShippingProcess real usando el servicio real
            var shippingProcess = await shippingProcessService.Add(new ShippingProcess
            {
                Date = DateTime.Now,
                Path = @"C:\Temp\InProcessTest"
            }, CancellationToken.None);

            flatFileProcessor.ActualShippingProcess = shippingProcess;

            Directory.CreateDirectory(@"C:\Temp\InProcessTest\InProcess");

            Console.WriteLine("2. Generando archivos con servicios reales...");
            await flatFileProcessor.BuildFlatFileAsync(inProcessOrders, CancellationToken.None);

            Console.WriteLine("3. Verificando archivos generados...");
            var files = Directory.GetFiles(@"C:\Temp\InProcessTest\InProcess", "*.txt");
            
            if (files.Length == 0)
            {
                Console.WriteLine("⚠️  No se generaron archivos. Verificar permisos y configuración.");
                return;
            }

            Console.WriteLine($"✅ Se generaron {files.Length} archivos:");
            
            foreach (var file in files)
            {
                Console.WriteLine($"\n📄 {Path.GetFileName(file)}:");
                var content = await File.ReadAllTextAsync(file);
                Console.WriteLine("---CONTENIDO DEL ARCHIVO---");
                Console.WriteLine(content);
                Console.WriteLine("---------------------------");
                
                // Analizar estructura del archivo
                var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);
                Console.WriteLine($"📊 Análisis: {lines.Length} líneas");
                
                var groupA = lines.Where(l => l.StartsWith("A|")).Count();
                var groupP = lines.Where(l => l.StartsWith("P|")).Count();
                var groupD = lines.Where(l => l.StartsWith("D|")).Count();
                
                Console.WriteLine($"   - Automatización (A): {groupA} líneas");
                Console.WriteLine($"   - Pedido (P): {groupP} líneas");
                Console.WriteLine($"   - Detalle (D): {groupD} líneas");
                Console.WriteLine();
            }

            Console.WriteLine($"\n🎉 Test completado exitosamente!");
            Console.WriteLine($"📁 Archivos generados en: C:\\Temp\\InProcessTest\\InProcess");
            Console.WriteLine($"📝 ShippingProcess ID: {shippingProcess.Id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Error: {ex.Message}");
            Console.WriteLine($"📍 StackTrace: {ex.StackTrace}");
            
            if (ex.InnerException != null)
            {
                Console.WriteLine($"🔍 Inner Exception: {ex.InnerException.Message}");
            }
        }
    }

    static void ConfigureServicesForTest(IServiceCollection services)
    {
        var appsettingsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
        IConfiguration config = new ConfigurationBuilder()
           .AddJsonFile(appsettingsFilePath, true, true)
           .Build();

        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddConsole();
            loggingBuilder.SetMinimumLevel(LogLevel.Information);
        });

        services.AddSingleton(config);
        services.Configure<FileSettings>(options =>
        {
            options.DestinationFilePath = @"C:\Temp\InProcessTest";
            options.Delimiter = '|';
        });

        // Contextos de BD - REALES
        services.AddDbContext<AldebaranShippingContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("AldebaranDbConnection"));
        }, ServiceLifetime.Scoped);

        services.AddDbContext<AldebaranCleaningContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("AldebaranDbConnection"));
        }, ServiceLifetime.Scoped);

        // SERVICIOS REALES - NO MOCKS
        services.AddScoped<IInProcessOrderService, InProcessOrderService>();
        services.AddScoped<IFlatFileProcessor, FlatFileProcessor>();
        services.AddScoped<IShippingProcessService, ShippingProcessService>();
        services.AddScoped<ILastDocumentTypeProcessService, LastDocumentTypeProcessService>();

        // REPOSITORIES REALES - NO MOCKS
        services.AddScoped<IInProcessOrderRepository, InProcessOrderRepository>();
        services.AddScoped<IInProcessOrderDataFileRepository, InProcessOrderDataFileRepository>();
        services.AddScoped<IShippingProcessRepository, ShippingProcessRepository>();
        services.AddScoped<IShippingProcessDetailRepository, ShippingProcessDetailRepository>();
        services.AddScoped<IErpDocumentTypeRepository, ErpDocumentTypeRepository>();
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

        services.AddDbContext<AldebaranShippingContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("AldebaranDbConnection"));
        }, ServiceLifetime.Scoped, ServiceLifetime.Scoped);
       
        services.AddDbContext<AldebaranCleaningContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("AldebaranDbConnection"));
        }, ServiceLifetime.Scoped, ServiceLifetime.Scoped);
        // Engines
        services.AddSingleton<IFlatFileProcessor, FlatFileProcessor>();

        // Services
        services.AddSingleton<IOrderService, OrderService>();
        services.AddSingleton<ILastDocumentTypeProcessService, LastDocumentTypeProcessService>();
        services.AddSingleton<ISaleOrderService, SaleOrderService>();
        services.AddSingleton<IPurchaseOrderService, PurchaseOrderService>();
        services.AddSingleton<IInProcessOrderService, InProcessOrderService>();
        services.AddSingleton<IShippingProcessService, ShippingProcessService>();

        // Repositories
        services.AddSingleton<IOrderRepository, OrderRepository>();
        services.AddSingleton<IErpDocumentTypeRepository, ErpDocumentTypeRepository>();
        services.AddSingleton<ISaleOrderRepository, SaleOrderRepository>();
        services.AddSingleton<ISaleOrderDataFileRepository, SaleOrderDataFileRepository>();
        services.AddSingleton<IPurchaseOrderRepository, PurchaseOrderRepository>();
        services.AddSingleton<IPurchaseOrderDataFileRepository, PurchaseOrderDataFileRepository>();
        services.AddSingleton<IInProcessOrderRepository, InProcessOrderRepository>();
        services.AddSingleton<IInProcessOrderDataFileRepository, InProcessOrderDataFileRepository>();
        services.AddSingleton<IShippingProcessRepository, ShippingProcessRepository>();
        services.AddSingleton<IShippingProcessDetailRepository, ShippingProcessDetailRepository>();

        // HostedService
        services.AddHostedService<BackgroundShippingService>();
        services.AddHostedService<BackgroundCleaningService>();
    }
}
