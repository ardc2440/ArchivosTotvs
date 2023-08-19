SET TERM ^ ;
Alter TRIGGER ACT_STRANSITO_ITEMORDEN ACTIVE
AFTER insert OR update OR delete POSITION 0
AS  
    DECLARE ESTADO CHAR(1);
    DECLARE TIPOACTUALIZACION CHAR(1);
    DECLARE FECHAESTRECIBO TIMESTAMP;    
    
    DECLARE FECHA TIMESTAMP;
    DECLARE ACTIVIDAD VARCHAR(200);
    DECLARE IDACTIVIDADORDEN INT;
    
    declare IdFuncionario int;
    Declare IdOrden int ;
    declare numorden varchar(20) ;
BEGIN        
    IF ((DELETING) AND (Old.IDBODEGA in (16,17))) THEN 
    BEGIN 
      INSERT INTO STRANSITO (IDITEMTRANSITO, Accion)
           VALUES(Old.IDITEMORDEN, 'D');
           
      idorden = Old.IDORDEN;  
      
    END 
    ELSE
    BEGIN       
        idorden = New.IDORDEN; 
      
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
        
        SELECT ESTADO, FECHAESTRECIBO
          FROM ORDENES 
         WHERE IDORDEN = New.IDORDEN 
          INTO :ESTADO, :FECHAESTRECIBO;
          
        IF (ESTADO = 'I') THEN 
           TIPOACTUALIZACION = 'I';           
        ELSE
           TIPOACTUALIZACION = 'D';
           
        INSERT INTO STRANSITO (IDITEMTRANSITO, FECHAESTRECIBO, CANTIDADREC, FECHA, ACTIVIDAD, IDITEMXCOLOR, Accion)
             SELECT New.IDITEMORDEN, :FECHAESTRECIBO, New.CANTIDADSOLIC, :FECHA, :ACTIVIDAD, New.IDITEMXCOLOR,:TIPOACTUALIZACION
               FROM ITEMORDEN a                
               JOIN ITEMSXCOLOR b ON b.IDITEMXCOLOR = a.IDITEMXCOLOR
               JOIN ITEMS c ON c.IDITEM = a.IDITEM
              WHERE a.IDITEMORDEN = New.IDITEMORDEN
                AND a.IDBODEGA IN (16,17)
                AND b.ACTIVO = 'S'
                AND c.ACTIVO = 'S'
                AND c.CATALOGOVISIBLE = 'S';                
   
    END            
    
    select IDFUNCIONARIO, NumOrden FROm Ordenes WHERE IdOrden = :idorden into :IdFuncionario, :numorden;
      
    if (:numorden <> 'Temporal') Then
        if ((select Count(1) from MODORDENES where fecha > dateadd (millisecond, -300, current_timestamp) and IdOrden = :idorden) = 0) THEN
        begin
            insert into MODORDENES (IDORDEN, IDFUNCIONARIO, FECHA)
                 values (:idOrden, :IdFuncionario, current_timestamp);
        END
END
^
SET TERM ; ^