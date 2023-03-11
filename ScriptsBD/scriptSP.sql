CREATE PROCEDURE SP_GetUsuarios
AS begin
	Select 
		u.UsuarioId,
		u.Nombre,
		u.ApellidoPaterno,
		u.ApellidoMaterno,
		tu.TipoUsuarioId,
		tu.Tipo 
	from Usuario u
	inner join TipoUsuario tu on u.TipoUsuarioId = tu.TipoUsuarioId 
	where u.Eliminado = 0
	order by u.TipoUsuarioId, u.ApellidoPaterno	
end

CREATE PROCEDURE SP_GetClientes
AS begin
	Select 
		ClienteId,
		Nombre,
		ApellidoPaterno,
		ApellidoMaterno,
		Domicilio,
		Telefono 
	from Cliente 	
	where Eliminado = 0
	order by ApellidoPaterno	
end

CREATE PROCEDURE SP_GetVehiculosByCliente
	@clienteId int
AS begin
	Select 
		v.VehiculoId,
		v.Marca,
		v.Modelo,
		v.Placas,
		v.color,
		isnull((select top 1 CONVERT(VARCHAR, ct.FechaCita  , 22) from Cita ct where ct.VehiculoId = v.VehiculoId and ct.FechaCita < GETDATE()),''),
		isnull((select top 1 CONVERT(VARCHAR, ct.FechaCita  , 22)  from Cita ct where ct.VehiculoId = v.VehiculoId and ct.FechaCita > GETDATE()),'')
	from Vehiculo v
	inner join Cliente c on c.ClienteId = v.ClienteId 
	where v.ClienteId = @clienteId and v.Eliminado = 0
end

 CREATE PROCEDURE SP_GetCitas	
AS begin
	select 
	c.CitaId,
	c.FechaCita,
	c.Comentarios,
	c.FechaTerminacion,
	sc.Status,
	CONCAT(cl.Nombre, ' ', cl.ApellidoPaterno),
	CONCAT(v.Marca,' ', v.Modelo, ' ', v.Placas)
FROM Cita c 
inner join StatusCita sc on sc.StatusCitaId = c.StatusCitaId 
inner join Vehiculo v on v.VehiculoId = c.VehiculoId 
inner join Cliente cl on cl.ClienteId = v.ClienteId
end

CREATE PROCEDURE SP_EliminarVehiculo
	@vehiculoId int
AS begin
	update Vehiculo set Eliminado = 1 where VehiculoId = @vehiculoId
	delete from Cita where VehiculoId = @vehiculoId
end


