using System;

namespace Totvs.FlatFileGenerator.Data.Entities
{
    public class InProcessOrderDataFile
    {
        // Campos básicos que definitivamente retorna el SP (según los datos que vimos)
        public int NroProceso { get; set; }
        public string TipoDocumentOrigen { get; set; }
        public string CodTipoDocumentOrigen { get; set; } // Nuevo campo para el código del tipo de documento
        public int DocumentoOrigen { get; set; }
        public DateTime FechaDocumentoOrigen { get; set; }
        public int CustomerOrderInProcessId { get; set; }
        public int CustomerOrderInProcessDetailId { get; set; } // Nuevo campo para el detalle del traslado
        public int CustomerOrderDetailId { get; set; }
        public int Quantity { get; set; }
        public string ActionType { get; set; } // Nuevo campo ActionType
        public int CustomerOrderId { get; set; }
        public string OrderNumber { get; set; }
        public string ClientIdentity { get; set; }
        public string CustomerNotes { get; set; }
        public string InternalNotes { get; set; }
        public string LineCode { get; set; }
        public string ItemCode { get; set; }
        public string ReferenceCode { get; set; }
    }
}