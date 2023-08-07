using System.Threading;
using System.Threading.Tasks;

namespace Totvs.FlatFileGenerator.Engine
{
    internal interface IFileOperations
    {
        Task FileMoveAsync(string sourceFilePath, string targetFolderPath, CancellationToken ct = default);
    }
}
