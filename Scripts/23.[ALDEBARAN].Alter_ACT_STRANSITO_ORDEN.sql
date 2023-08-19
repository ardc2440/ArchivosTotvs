SET TERM ^ ;
alter TRIGGER ACT_STRANSITO_ORDEN ACTIVE
AFTER insert OR update OR delete POSITION 0
AS  
    DECLARE TIPOACTUALIZACION CHAR(1);
    DECLARE FECHAESTRECIBO TIMESTAMP;    
    
    DECLARE FECHA TIMESTAMP;
    DECLARE ACTIVIDAD VARCHAR(200);
    DECLARE IDACTIVIDADORDEN INT;
BEGIN        
    IF (DELETING) THEN 
    BEGIN 
      INSERT INTO STRANSITO (IDITEMTRANSITO, Accion)
           SELECT IDITEMORDEN, 'D' 
             FROM ITEMORDEN a             
             JOIN ITEMSXCOLOR b ON b.IDITEMXCOLOR = a.IDITEMXCOLOR
             join ITEMS c ON c.IDITEM = a.IDITEM
            WHERE IDORDEN = Old.IDORDEN
              AND a.IDBODEGA IN (16,17)
              AND b.ACTIVO = 'S'
              AND c.ACTIVO = 'S'
              AND c.CATALOGOVISIBLE = 'S';
    END 
    ELSE
    BEGIN   
        SELECT COALESCE(MAX(IDACTIVIDADORDEN),0) 
          FROM ACTORDEN 
         WHERE IDORDEN = New.IDORDEN
          INTO :IDACTIVIDADORDEN;
        
        FECHA = current_timestamp;
        ACTIVIDAD = '';
        
        IF (:IDACTIVIDADORDEN > 0) THEN
        BEGIN
            SELECT FECHA, ACTIVIDAD 
              FROM ACTORDEN 
             WHERE IDACTIVIDADORDEN = :IDACTIVIDADORDEN
              INTO :FECHA, :ACTIVIDAD;        
        END 
        
        IF (New.ESTADO = 'I') THEN 
           TIPOACTUALIZACION = 'I';           
        ELSE
           TIPOACTUALIZACION = 'D';
           
        INSERT INTO STRANSITO (IDITEMTRANSITO, FECHAESTRECIBO, CANTIDADREC, FECHA, ACTIVIDAD, IDITEMXCOLOR, Accion)
             SELECT IDITEMORDEN, New.FECHAESTRECIBO, CANTIDADSOLIC, :FECHA, :ACTIVIDAD, a.IDITEMXCOLOR,:TIPOACTUALIZACION
               FROM ITEMORDEN a                
               JOIN ITEMSXCOLOR b ON b.IDITEMXCOLOR = a.IDITEMXCOLOR
               JOIN ITEMS c ON c.IDITEM = a.IDITEM
              WHERE a.IDORDEN = New.IDORDEN
                AND a.IDBODEGA IN (16,17)
                AND b.ACTIVO = 'S'
                AND c.ACTIVO = 'S'
                AND c.CATALOGOVISIBLE = 'S';
                
        if (updating and Old.NUMORDEN is not null and trim(Old.NUMORDEN) <> 'Temporal') THEN
            insert into MODORDENES (IDORDEN, IDFUNCIONARIO, FECHA)
                 values (New.IDORDEN, New.IdFuncionario, current_timestamp);
    END            
END
^
SET TERM ; ^