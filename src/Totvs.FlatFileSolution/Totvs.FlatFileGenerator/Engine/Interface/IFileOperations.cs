using System.Threading;
using System.Threading.Tasks;

namespace Totvs.FlatFileGenerator.Engine.Interface
{
    internal interface IFileOperations
    {
        Task FileMoveAsync(string sourceFilePath, string targetFolderPath, CancellationToken ct = default);
    }
}
