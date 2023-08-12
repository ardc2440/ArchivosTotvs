using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Totvs.FlatFileGenerator.Data.Entities;

namespace Totvs.FlatFileGenerator.Business.Services.Interface
{
    public interface IDocumentTypeService
    {
        Task<Models.DocumentType> Find(string type, CancellationToken ct = default);
        void Update(Models.DocumentType entity, CancellationToken ct = default);
    }
}
