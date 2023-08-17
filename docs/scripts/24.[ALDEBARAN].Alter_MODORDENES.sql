alter table modordenes drop constraint PK_MODORDENES;
alter table modordenes add idmodorden int not null constraint PK_MODORDENES primary key;
create generator GEN_MODORDENES;

SET TERM ^ ;
CREATE TRIGGER NEW_ModOrdenes FOR MODORDENES
ACTIVE BEFORE INSERT POSITION 0
AS  
BEGIN   
    New.IDMODORDEN = GEN_ID(GEN_MODORDENES,1);
END^
SET TERM ; ^