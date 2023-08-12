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
using Totvs.FlatFileGenerator.Config;
using Totvs.FlatFileGenerator.Engine.Interface;
using Totvs.FlatFileGenerator.Engine.Mappers;

namespace Totvs.FlatFileGenerator.Engine.Implement
{
    internal class FlatFileProcessor : IFlatFileProcessor
    {
        private readonly FileSettings _settings;
        public FlatFileProcessor(IOptions<FileSettings> settings)
        {
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(IOptions<FileSettings>));
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
                string destinationPath = Path.Combine(_settings.DestinationFilePath, filename);
                using (var writer = new StreamWriter(destinationPath))
                using (var csv = new CsvWriter(writer, csvConfig))
                {
                    csv.Context.RegisterClassMap<SaleOrderHeaderMap>();
                    var header = (SaleOrderHeader)saleOrder;
                    csv.WriteRecord(header);
                    csv.Context.RegisterClassMap<SaleOrderDetailMap>();
                    csv.WriteRecords(saleOrder.Details);
                }
            }



        }
    }
}
