using System;

namespace Totvs.FlatFileGenerator.Business.Models
{
    public class LastDocumentTypeProcess
    {
        public int Id { get; set; }
        public DateTime LastExecutionDate { get; set; }
        public DateTime LastCleaningDate { get; set; }

        public static implicit operator LastDocumentTypeProcess(Data.Entities.DocumentType entity)
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
