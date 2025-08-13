using System;

namespace Totvs.FlatFileGenerator.Business.Models
{
    public class InProcessOrderDetail
    {
        // CAMPOS EXACTOS QUE RETORNA EL STORED PROCEDURE
        // GRUPO 1: Transfer Header Info
        public int NroProceso { get; set; }
        public string TipoDocumentOrigen { get; set; }
        public string CodTipoDocumentOrigen { get; set; } // Nuevo campo para el código del tipo de documento
        public int DocumentoOrigen { get; set; }
        public DateTime FechaDocumentoOrigen { get; set; }
        
        // GRUPO 2: Order Info
        public int CustomerOrderInProcessId { get; set; }
        public int CustomerOrderId { get; set; }
        public string OrderNumber { get; set; }
        public string ClientIdentity { get; set; }
        public string CustomerNotes { get; set; }
        public string InternalNotes { get; set; }
        
        // GRUPO 3: Detail Info
        public int CustomerOrderInProcessDetailId { get; set; } // Nuevo campo para el detalle del traslado
        public int CustomerOrderDetailId { get; set; }
        public int Quantity { get; set; }
        public string ActionType { get; set; } // Nuevo campo ActionType
        public string LineCode { get; set; }
        public string ItemCode { get; set; }
        public string ReferenceCode { get; set; }

        // Propiedades heredadas del modelo anterior (para compatibilidad con BackgroundShippingService)
        public string Type { get; set; } = "T";
        public string InProcessNumber => NroProceso.ToString();
        public DateTime ActionDate => FechaDocumentoOrigen;
        public int InProcessId => NroProceso;
        public int InProcessDetailId => CustomerOrderDetailId;

        public static implicit operator InProcessOrderDetail(Data.Entities.InProcessOrderDataFile inProcessOrderDataFile)
        {
            if (inProcessOrderDataFile == null) return null!;
            return new InProcessOrderDetail
            {
                // Grupo 1: Transfer Header
                NroProceso = inProcessOrderDataFile.NroProceso,
                TipoDocumentOrigen = inProcessOrderDataFile.TipoDocumentOrigen,
                CodTipoDocumentOrigen = inProcessOrderDataFile.CodTipoDocumentOrigen,
                DocumentoOrigen = inProcessOrderDataFile.DocumentoOrigen,
                FechaDocumentoOrigen = inProcessOrderDataFile.FechaDocumentoOrigen,
                
                // Grupo 2: Order Info
                CustomerOrderInProcessId = inProcessOrderDataFile.CustomerOrderInProcessId,
                CustomerOrderId = inProcessOrderDataFile.CustomerOrderId,
                OrderNumber = inProcessOrderDataFile.OrderNumber,
                ClientIdentity = inProcessOrderDataFile.ClientIdentity,
                CustomerNotes = inProcessOrderDataFile.CustomerNotes,
                InternalNotes = inProcessOrderDataFile.InternalNotes,
                
                // Grupo 3: Detail Info
                CustomerOrderInProcessDetailId = inProcessOrderDataFile.CustomerOrderInProcessDetailId, // Nuevo campo
                CustomerOrderDetailId = inProcessOrderDataFile.CustomerOrderDetailId,
                Quantity = inProcessOrderDataFile.Quantity,
                ActionType = inProcessOrderDataFile.ActionType, // Nuevo campo ActionType
                LineCode = inProcessOrderDataFile.LineCode,
                ItemCode = inProcessOrderDataFile.ItemCode,
                ReferenceCode = inProcessOrderDataFile.ReferenceCode
            };
        }
    }
}