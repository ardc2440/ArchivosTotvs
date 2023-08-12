using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Totvs.FlatFileGenerator.Business.Models
{
    public class DocumentType
    {
        public int Id { get; set; }
        public DateTime LastExecutionDate { get; set; }
        public DateTime LastCleaningDate { get; set; }

        public static implicit operator DocumentType(Data.Entities.DocumentType entity)
        {
            if (entity == null) return null!;
            return new DocumentType
            {
                Id = entity.Id,
                LastExecutionDate = entity.LastExecutionDate,   
                LastCleaningDate = entity.LastCleaningDate
            };
        }
    }
}
