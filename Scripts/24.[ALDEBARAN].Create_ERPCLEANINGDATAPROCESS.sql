SET TERM ^ ;

CREATE PROCEDURE ERPCLEANINGDATAPROCESS 
 (LimitDate timestamp) 
AS 
BEGIN
    DELETE from ERPSHIPPINGPROCESSDETAIL
     where exists (select 1 
                   from ERPSHIPPINGPROCESS 
                  where ERPSHIPPINGPROCESSDETAIL.IDERPSHIPPINGPROCESS = ERPSHIPPINGPROCESS.ID
                    and ERPSHIPPINGPROCESS.EXECUTIONDATE < :LimitDate);
    
    delete 
      from ERPSHIPPINGPROCESS 
     where EXECUTIONDATE < :LimitDate ;
                    
    update ERPDOCUMENTTYPE 
       set LastCleaningDate = :LimitDate ;
END^

SET TERM ; ^
