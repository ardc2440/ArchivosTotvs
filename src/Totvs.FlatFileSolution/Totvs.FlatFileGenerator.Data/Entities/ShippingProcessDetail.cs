namespace Totvs.FlatFileGenerator.Data.Entities
{
    public class ShippingProcessDetail
    {
        public int Id { get; set; }        
        public int ShippingProcessId { get; set; }
        public short DocumentTypeId { get; set; }
        public int DocumentId { get; set; }
        public string FileName { get; set; }
        public virtual ShippingProcess ShippingProcess { get; set; } = null!;
        public virtual DocumentType DocumentType { get; set; } = null!;
    }
}
