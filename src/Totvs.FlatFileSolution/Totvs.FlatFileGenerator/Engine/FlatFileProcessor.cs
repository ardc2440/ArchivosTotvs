using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Business.Models;
using Totvs.FlatFileGenerator.Engine.Mappers;
using Totvs.FlatFileGenerator.Models;

namespace Totvs.FlatFileGenerator.Engine
{
    internal class FlatFileProcessor : IFlatFileProcessor
    {
        private readonly FileSettings _settings;
        public FlatFileProcessor(IOptions<FileSettings> settings)
        {
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(IOptions<FileSettings>));
        }
        public async Task BuildFlatFileAsync(IEnumerable<Order> orders, CancellationToken ct = default)
        {
            var csvConfig = new CsvConfiguration(cultureInfo: CultureInfo.InvariantCulture)
            {
                Delimiter = _settings.Delimiter
            };
            string destinationPath = Path.Combine(_settings.DestinationFilePath, "file.txt");
            using (var writer = new StreamWriter(destinationPath))
            using (var csv = new CsvWriter(writer, csvConfig))
            {
                csv.Context.RegisterClassMap<OrderMap>();
                await csv.WriteRecordsAsync(orders, ct);
            }
        }
    }
}
