--Diego
use DALISA
go
create or alter  proc USP_ACTUALIZAR_PASS  (@idusuario varchar(10),@PASS varchar(15) )
AS
BEGIN
 update TBLUSUARIO set PASS=@PASS
   where [ID USUARIO]=@idusuario 
END
create or alter  proc USP_ACTUALIZAR_MIS_DATOS
   @idusuario varchar(10),@pais varchar(25),@Nombre varchar(25),@apellido varchar(25),@fechanaci date,@estadociv varchar(40),
  @sexo char(1), @dni char(8),@direccion varchar(105),@celular Char(9),@correo varchar(40),@idbanco int,@cuenta varchar(20)
  as
   begin
   update TBLUSUARIO set PAIS = @pais,[NOMBRE USE] = @Nombre,
   [APELLIDO USE] = @apellido, [FECHA NACI] = @fechanaci, [ESTADO CIVIL] = @estadociv,
   SEXO = @sexo, [DNI USE] = @dni, DIRECCION = @direccion,[CELULAR USE] = 
   @celular,[CORREO USE] = 
   @correo, [ID BANCO]=@idbanco,
   [CNT DEPOSITO]=@cuenta
   where [ID USUARIO]=@idusuario 
    end
   go
   update TBLUSUARIO set [FECHA NACI]='1/22/2000' where [ID USUARIO]='s0002'
   select * from TBLSOCIO,TBLUSUARIO
   Create or Alter proc usp_ProductoInsertar
   @idproducto varchar(10),
   @nombreproducto varchar(45),@descripcion varchar(400),@idcategoria varchar(10),
   @idmarca varchar(10),@precio decimal(6,2),@cantidad int,@puntoprod int,@fotoprod varchar(100),
   @fechareg DATE,@eliminado char(1)
   as
   begin
   insert dbo.TBLPRODUCTO([ID PRODUCTO], [NOMBRE PROD],[DESCRIP PROD],[ID CATEGORIA],[ID MARCA],[PRECIO PROD],
   [CANTIDAD PROD],[PUNTO PROD],[FOTO PROD],[FECHA REGISTRO],ELIMINADO)
   values(@idproducto,@nombreproducto,@descripcion,@idcategoria,@idmarca,@precio,@cantidad,@puntoprod,
   @fotoprod,@fechareg,@eliminado)
   end
   go


   Create or Alter proc usp_ProductoActualiza
   @idproducto varchar(10), @nombreproducto varchar(45),@descripcion varchar(400),@idcategoria varchar(10),
 @precio decimal(6,2),@cantidad int,@puntoprod int,@fotoprod varchar(100)
   as
   begin
   Update dbo.TBLPRODUCTO set [NOMBRE PROD] = @nombreproducto ,[DESCRIP PROD] = @descripcion,
   [ID CATEGORIA] = @idcategoria,[PRECIO PROD] = @precio,
   [CANTIDAD PROD] = @cantidad,[PUNTO PROD] = @puntoprod,[FOTO PROD] = @fotoprod
 where [ID PRODUCTO] = @idproducto
   end
   go

 

   Create or Alter proc usp_ListaCategoria
   as
   begin 
   select [ID CATEGORIA],[NOMBRE CATEGORIA] from dbo.TBLCATEGORIA
   end 
   go

   Create or Alter proc usp_BuscaProducto
   @idproducto varchar(10)
   as
   begin
   select * from dbo.TBLPRODUCTO where [ID PRODUCTO] = @idproducto
   end
   go
   
   exec usp_BuscaProducto P0001
   go

   create or alter proc usp_ProductoBaja
    @idproducto varchar(10)
	as
	begin
	update dbo.TBLPRODUCTO set ELIMINADO = 1
	where [ID PRODUCTO] = @idproducto
	end
	go


	Create or Alter proc usp_ActualizaEstado_Boleta
   @OPC CHAR(2),@BNOL char(10)
   as
   begin
   IF @OPC = 'F'
     begin
	 if (select ID_REPARTIDOR from TBLCABECERA_BOLETA where NUM_BOLETA=@BNOL ) !='R0001'
	   UPDATE TBLCABECERA_BOLETA set ESTADO = 'FACTURADO' where NUM_BOLETA=@BNOL
	   else  
	   UPDATE TBLCABECERA_BOLETA set ESTADO = 'FACTURADO',FECHA_ENTREGA=GETDATE() where NUM_BOLETA=@BNOL
	end
   ELSE IF @OPC = 'C'
    UPDATE TBLCABECERA_BOLETA set ESTADO = 'CANCELADO' where NUM_BOLETA=@BNOL
	 ELSE IF @OPC = 'E'
    UPDATE TBLCABECERA_BOLETA set ESTADO = 'EN CAMINO' where NUM_BOLETA=@BNOL
   END 
   GO

   create or alter proc USP_AFILIAR_SOCIO
    @idsosio varchar(10),@pais varchar(25),@Nombre varchar(25),@apellido varchar(25),@fechanaci date,@estadociv varchar(40),
  @sexo char(1), @dni char(8),@direccion varchar(105),@departamento varchar(50),@provincia varchar(50),@distrito varchar(50),
  @celular Char(9),@correo varchar(40),@pass varchar(15),
 @paquete varchar(25),@idsociop varchar(10)
  as
  begin
  insert TBLUSUARIO  values( @idsosio ,@pais ,@Nombre ,@apellido ,@fechanaci ,@estadociv,@sexo , @dni ,@direccion ,@departamento ,@provincia,@distrito ,
  @celular ,@correo ,@dni ,@pass ,null,null,null , getdate() ,0 )
  insert TBLSOCIO values(@idsosio ,0 ,2 ,'NINGUNO' ,@paquete,@idsosio,@idsociop,0,'0')
  end
  go


  SELECT*FROM TBLSOCIO
  SELECT*FROM TBLUSUARIO WHERE [DNI USE]='45235688'


  EXECUTE USP_AFILIAR_SOCIO 'S0020','PERU','DIEGO','ORTEGA','2000-08-09','SOLTERO','M',
  '41254782','MSQL','LIMA','LIMA','SJL','745878889','DIEGO@GMAIL.COM','123','PACK BASICO S/.205.00','S0007'