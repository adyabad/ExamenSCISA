create database ExamenSCISA
use ExamenSCISA

create table TipoUsuario(
	TipoUsuarioId int Primary Key Identity,
	Tipo varchar(100) not null
)
create table Usuario(
	UsuarioId int Primary Key Identity,
	Nombre varchar(200) not null,
	ApellidoPaterno varchar(200) not null,
	ApellidoMaterno varchar(200) not null,
	Eliminado bit default 0,
	TipoUsuarioId int Foreign key References TipoUsuario(TipoUsuarioId)
)

create table Cliente(
	ClienteId int Primary Key Identity,
	Nombre varchar(200) not null,
	ApellidoPaterno varchar(200) not null,
	ApellidoMaterno varchar(200) not null,
	Telefono varchar(10) not null,
	Domicilio varchar(1000) not null,
	Eliminado bit default 0
)

create table Vehiculo(
	VehiculoId int Primary key identity,
	Marca varchar(200) not null,
	Modelo varchar(200) not null,
	Placas varchar(15) not null,
	Color varchar(100) not null,
	Eliminado bit default 0,
	ClienteId int Foreign key References Cliente(ClienteId)
)

create table StatusCita(
	StatusCitaId int Primary key identity,
	Status varchar(200) not null
)

create table Cita(
	CitaId int Primary key identity,
	FechaCita datetime not null,	
	Comentarios varchar(1000),
	FechaTerminacion datetime,
	VehiculoId int Foreign key References Vehiculo(VehiculoId),
	StatusCitaId int Foreign key References StatusCita(StatusCitaId),
	AdminId int Foreign key References Usuario(UsuarioId),
	UsuarioId int Foreign key References Usuario(UsuarioId)
)

insert into TipoUsuario values('Administrador')
insert into TipoUsuario values('Usuario')
insert into StatusCita values('Creada')
insert into StatusCita values('Aprobada')
insert into StatusCita values('Rechazada')
insert into StatusCita values('Concluida')



