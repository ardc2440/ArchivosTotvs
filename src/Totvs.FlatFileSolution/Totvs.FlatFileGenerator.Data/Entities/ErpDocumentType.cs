using System;
using System.Collections.Generic;

namespace Totvs.FlatFileGenerator.Data.Entities
{
    public class ErpDocumentType
    {
        public short Id { get; set; }
        public DateTime LastExecutionDate { get; set; }
        public DateTime LastCleaningDate { get; set;}        
        public DocumentType DocumentType { get; set; }
    }
}
