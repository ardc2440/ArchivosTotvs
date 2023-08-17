CREATE table CierrePedido(
Id INT NOT NULL Constraint PK_CierrePedido PRIMARY KEY,
IdPedido INT NOT NULL Constraint FK_CierrePedido_Pedido references PEDIDOS(IdPedido),
Fecha Timestamp NOT NULL)