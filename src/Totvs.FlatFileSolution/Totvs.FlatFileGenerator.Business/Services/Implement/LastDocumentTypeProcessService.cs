using System;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Business.Services.Interface;
using Totvs.FlatFileGenerator.Data.Repositories.Interface;

namespace Totvs.FlatFileGenerator.Business.Services.Implement
{
    public class LastDocumentTypeProcessService : ILastDocumentTypeProcessService
    {
        private readonly IDocumentTypeRepository _repository;
        public LastDocumentTypeProcessService(IDocumentTypeRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<Models.LastDocumentTypeProcess> Find(string type, CancellationToken ct = default)
        {
            var documentType = await _repository.Find(type);
            return (Models.LastDocumentTypeProcess)documentType;
        }

        public async Task Update(Models.LastDocumentTypeProcess entity, CancellationToken ct = default)
        {
            var documentType = await _repository.Find(entity.Id, ct);

            documentType.LastExecutionDate = entity.LastExecutionDate;
            documentType.LastCleaningDate = entity.LastCleaningDate;
            await _repository.Update(documentType);
        }
    }
}
