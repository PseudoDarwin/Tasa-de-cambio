Create Database DB_TasaCambio
use DB_TasaCambio

Create Table TasaCambio
(
	idTasa Integer  IDENTITY(1,1) NOT NULL,
	valor float,
	fecha datetime
)

create procedure Agregar_Tasa
@valor float,
@fecha datetime
as
begin
	insert into TasaCambio (valor, fecha)  values (@valor,@fecha) 
end
go

select * from TasaCambio