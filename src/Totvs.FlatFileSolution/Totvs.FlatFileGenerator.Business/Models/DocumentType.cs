using System.Collections.Generic;
using Totvs.FlatFileGenerator.Business.Models;

namespace Aldebaran.Application.Services.Models
{
    public class DocumentType
    {
        public short DocumentTypeId { get; set; }
        public string DocumentTypeName { get; set; }
        public string DocumentTypeCode { get; set; }
        // Reverse navigation       
        public ICollection<StatusDocumentType> StatusDocumentTypes { get; set; }

        public DocumentType()
        {            
            StatusDocumentTypes = new List<StatusDocumentType>();
        }
    }
}
