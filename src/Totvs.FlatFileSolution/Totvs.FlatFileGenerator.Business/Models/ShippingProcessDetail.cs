namespace Totvs.FlatFileGenerator.Business.Models
{
    public class ShippingProcessDetail
    {
        public int Id { get; set; }
        public int ShippingProcessId { get; set; }
        public short DocumentTypeId { get; set; }
        public int DocumentId { get; set; }        
        public string FileName { get; set; }
        
        public static implicit operator ShippingProcessDetail(Data.Entities.ShippingProcessDetail shippingProcessDetail)
        {
            if (shippingProcessDetail == null) return null!;
            return new ShippingProcessDetail
            {
                Id = shippingProcessDetail.Id,
                ShippingProcessId = shippingProcessDetail.ShippingProcessId,
                DocumentTypeId = shippingProcessDetail.DocumentTypeId,
                DocumentId = shippingProcessDetail.DocumentId,
                FileName = shippingProcessDetail.FileName
            };
        }
    }
}

