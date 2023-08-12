using System;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Business.Services.Interface;
using Totvs.FlatFileGenerator.Data.Repositories.Interface;

namespace Totvs.FlatFileGenerator.Business.Services.Implement
{
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly IDocumentTypeRepository _repository;
        public DocumentTypeService(IDocumentTypeRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<Models.DocumentType> Find(string type, CancellationToken ct = default)
        {
            var documentType = await _repository.Find(type);
            return (Models.DocumentType)documentType;
        }

        public async void Update(Models.DocumentType entity, CancellationToken ct = default)
        {
            var documentType = await _repository.Find(entity.Id, ct);

            documentType.LastExecutionDate = DateTime.Now;

            //_repository.Update(documentType);
        }
    }
}
