using System;

namespace Totvs.FlatFileGenerator.Business.Models
{
    public class LastDocumentTypeProcess
    {
        public short Id { get; set; }
        public DateTime LastExecutionDate { get; set; }
        public DateTime LastCleaningDate { get; set; }

        public static implicit operator LastDocumentTypeProcess(Data.Entities.ErpDocumentType entity)
        {
            if (entity == null) return null!;
            return new LastDocumentTypeProcess
            {
                Id = entity.Id,
                LastExecutionDate = entity.LastExecutionDate,   
                LastCleaningDate = entity.LastCleaningDate
            };
        }
    }
}
