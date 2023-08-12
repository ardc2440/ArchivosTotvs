using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Engine.Interface;

namespace Totvs.FlatFileGenerator.Engine.Implement
{
    internal class FileOperations : IFileOperations
    {
        public async Task FileMoveAsync(string sourceFilePath, string targetFolderPath, CancellationToken ct = default)
        {
            string destinationPath = Path.Combine(targetFolderPath, sourceFilePath);
            await Task.Run(() =>
            {
                File.Move(sourceFilePath, destinationPath, true);
            });
        }
    }
}
