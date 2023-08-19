SET TERM ^ ;
CREATE TRIGGER NEW_ERPShippingProcessDetail FOR ERPShippingProcessDetail
ACTIVE BEFORE INSERT POSITION 0
AS  
BEGIN   
    New.ID = GEN_ID(GEN_ERPShippingProcessDetail,1);
END^
SET TERM ; ^