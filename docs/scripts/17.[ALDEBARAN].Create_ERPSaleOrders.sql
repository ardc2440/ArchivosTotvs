CREATE VIEW ERPSaleOrders
AS
SELECT IDPEDIDO SaleId, ActionType, FECHACREACION ActionDate 
  FROM (
        SELECT 'C' ActionType , p.IDPEDIDO, p.FECHACREACION
          FROM PEDIDOS p
         where Estado IN ('P', 'A', 'R', 'T', 'E')
        UNION  
        SELECT 'M', m.IDPEDIDO, MAX(m.FECHA) 
          FROM MODPEDIDOS m          
         GROUP BY m.IDPEDIDO
        union 
        SELECT 'X', c.IDPEDIDO, CAST(max(c.FECHACANC) as TIMESTAMP)
          FROM CANCELPEDIDO c
         GROUP BY c.IDPEDIDO
        union 
        select 'R', r.IDPEDIDO, MAX(r.FECHA) 
          FROM CierrePedido r
         GROUP BY r.IDPEDIDO) AS PEDIDOS		 
 WHERE FECHACREACION > (select a.LASTEXECUTIONDATE from ERPDOCUMENTTYPE A WHERE A.CODETYPE = 'P')
 ORDER BY SaleId, ActionType, ActionDate;
