SET TERM ^ ;
CREATE TRIGGER Cerrar_Pedido FOR PEDIDOS
ACTIVE AFTER Update POSITION 1
AS  
    declare variable idCierrePedido int;
BEGIN
    if (New.ESTADO = 'R') THEN
    BEGIN 

        SELECT Id FROM CierrePedido WHERE IdPedido = New.IdPedido INTO :idCierrePedido;

        If (:idCierrePedido is null) then
            INSERT INTO CierrePedido (IdPedido, Fecha)         
                 Values (New.IdPedido, CURRENT_TIMESTAMP);        
    end
END^
SET TERM ; ^