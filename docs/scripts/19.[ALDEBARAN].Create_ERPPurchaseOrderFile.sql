Create VIew ERPPurchaseOrderFile
AS
SELECT 'O' DocumentType, o.NUMORDEN PurchaseNumber, t.CODTIPIDENTIFICA ProviderIdentificationType, p.NUMIDENTIFICA ProviderIdentificationNumber, o.FECHAESTRECIBO ArrivingEstimatedDate, o.NROPROFORMA ProformaNumber, o.IDORDEN PurchaseId, io.IDITEMORDEN PurchaseDetailId, 
		i.CODLINEA lineCode, i.REFINTERNA ItemCode, i.REFITEMXCOLOR ReferenceCode, io.CANTIDADSOLIC Quantity
  FROM ORDENES o
  JOIN PROVEEDORES p ON p.IDPROVEEDOR = o.IDPROVEEDOR
  JOIN TIPIDENTIFICA t ON t.IDTIPIDENTIFICA = p.IDTIPIDENTIFICA
  JOIN ITEMORDEN io ON io.IDORDEN = o.IDORDEN
  JOIN ERPItems i ON i.IDITEMXCOLOR = io.IDITEMXCOLOR
 WHERE EXISTS (SELECT 1 FROM ERPPurchaseOrders eo WHERE eo.PurchaseId = o.IDORDEN)  
 ORDER BY PurchaseId, PurchaseDetailId;
