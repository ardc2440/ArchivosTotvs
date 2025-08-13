-- SCRIPT DE TEST DIRECTO DEL STORED PROCEDURE
-- Ejecutar en SQL Server Management Studio

-- Test 1: Verificar que el SP existe
SELECT name, create_date, modify_date 
FROM sys.procedures 
WHERE name = 'SP_GENERATE_INPROCESS_TOTVS_INTEGRATON_DATA';

-- Test 2: Ejecutar el SP y ver los datos que retorna
EXEC SP_GENERATE_INPROCESS_TOTVS_INTEGRATON_DATA;

-- Test 3: Verificar estructura de datos
SELECT 
    COLUMN_NAME,
    DATA_TYPE,
    IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME IN (
    'customer_order_in_process_details',
    'customer_orders_in_process', 
    'customer_orders',
    'automatic_in_process',
    'automatic_in_process_detail'
)
ORDER BY TABLE_NAME, ORDINAL_POSITION;

-- Test 4: Verificar datos de prueba disponibles
SELECT TOP 10 
    fd.DetailId,
    cpd.CUSTOMER_ORDER_IN_PROCESS_ID,
    co.CUSTOMER_ORDER_NUMBER,
    aip.AUTOMATIC_IN_PROCESS_ID
FROM customer_order_in_process_details cpd
JOIN customer_orders_in_process cip ON cip.CUSTOMER_ORDER_IN_PROCESS_ID = cpd.CUSTOMER_ORDER_IN_PROCESS_ID
JOIN customer_orders co ON co.CUSTOMER_ORDER_ID = cip.CUSTOMER_ORDER_ID
LEFT JOIN automatic_in_process_detail aipd ON aipd.CUSTOMER_ORDER_IN_PROCESS_DETAIL_ID = cpd.CUSTOMER_ORDER_IN_PROCESS_DETAIL_ID
LEFT JOIN automatic_in_process aip ON aip.AUTOMATIC_IN_PROCESS_ID = aipd.AUTOMATIC_IN_PROCESS_ID
WHERE aip.AUTOMATIC_IN_PROCESS_ID IS NOT NULL;