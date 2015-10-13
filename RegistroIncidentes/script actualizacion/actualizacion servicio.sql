--Actualizacion servicio version 1.1
-- Falta los procedmientos almacenados
-- obtener registro incidente
-- insertar insertar_incidente_sp
-- actualizar actualizar_incidente_sp
-- obtener_registro_incidente_sp
-- tabla datos_seleccion_area
-- tabla sop
-- tabla estado_incidente
SET IDENTITY_INSERT [dbo].[sop] ON
INSERT [dbo].[sop] ([codigo_sop], [nombre_sop], [estado_sop]) VALUES (0, N'No asignado', N'E')
SET IDENTITY_INSERT [dbo].[sop] OFF
SET IDENTITY_INSERT [dbo].[estado_incidente] ON
INSERT [dbo].[estado_incidente] ([codigo_estado_incidente], [nombre_estado_incidente], [estado_incidente]) VALUES (0, N'NO ASIGNADO', N'E')
SET IDENTITY_INSERT [dbo].[estado_incidente] OFF
SET IDENTITY_INSERT [dbo].[datos_seleccion_area] ON
INSERT [dbo].[datos_seleccion_area] ([codigo_datos_seleccion], [nombre_tipo_seleccion], [estado_dato_seleccion]) VALUES (0, N'Seleccione', N'E')
SET IDENTITY_INSERT [dbo].[datos_seleccion_area] OFF
alter table registro_incidentes add codigo_usuario_registra int null

update registro_incidentes
set codigo_usuario_registra = codigo_usuario_asignado

---
go
create proc [dbo].[obtener_usuario_id_sp]
@codigo_usuario int
as
select u.codigo_usuario,
u.documento,
u.nombres,
u.apelidos,
u.estado_usuario,
u.fecha_creacion,
u.fecha_ultimo_acceso,
u.nivel_acceso,
u.password_usuario,
u.correo_usuario
from usuario u
where u.codigo_usuario = @codigo_usuario
go
ALTER proc [dbo].[obtener_registro_incidente_reporte_sp]
@codigo_incidente varchar(50),
@codigo_usuario int,
@nivel_acceso int
as
select top(100) u.documento,
u.nombres + ' ' + u.apelidos [nombres],
r.codigo_incidente,
r.recibido_sistema,
ts.nombre_tipo_sistemas,
r.fecha_registro_incidente,
r.fecha_reporte_incidente,
r.fecha_primera_intereaccion,
gr.nombre_grupo_asignado,
ds.nombre_tipo_seleccion,
so.nombre_sop,
ei.nombre_estado_incidente,
tI.nombre_categorizacion,
tII.nombre_categorizacion,
tIII.nombre_categorizacion,
r.enviado_sistemas,
r.fecha_cierre,
r.descripcion_incidente,
r.pais,
r.estadistica
from registro_incidentes r
, usuario u
,tipo_sistemas ts
,grupo_asignado gr
,datos_seleccion_area ds
,sop so
,estado_incidente ei
,tier_uno tI
,tier_dos tII
,tier_tres tIII
where codigo_incidente like @codigo_incidente
and ((@nivel_acceso = 0 and codigo_usuario_asignado like '%')
or (@nivel_acceso <> 0 and codigo_usuario_asignado like @codigo_usuario))
and r.codigo_usuario_asignado = u.codigo_usuario
and r.codigo_tipo_sistemas = ts.codigo_tipo_sistemas
and gr.codigo_grupo_asignado = r.codigo_grupo_asignado
and r.codigo_dato_seleccion = ds.codigo_datos_seleccion
and r.codigo_sop = so.codigo_sop
and r.codigo_estado_incidente = ei.codigo_estado_incidente
and r.codigo_tier_uno = tI.codigo_tier_uno
and r.codigo_tier_dos = tII.codigo_tier_dos
and r.codigo_tier_tres = tIII.codigo_tier_tres
order by fecha_registro_incidente desc

go
ALTER proc [dbo].[obtener_incidente_reporte_fechas_sp]
@fechaInicio datetime,
@fechaFin datetime,
@codigo_usuario int,
@nivel_acceso int
as
select u.documento,
u.nombres + ' ' + u.apelidos [nombres],
r.codigo_incidente,
r.recibido_sistema,
ts.nombre_tipo_sistemas,
r.fecha_registro_incidente,
r.fecha_reporte_incidente,
r.fecha_primera_intereaccion,
gr.nombre_grupo_asignado,
ds.nombre_tipo_seleccion,
so.nombre_sop,
ei.nombre_estado_incidente,
tI.nombre_categorizacion,
tII.nombre_categorizacion,
tIII.nombre_categorizacion,
r.enviado_sistemas,
r.fecha_cierre,
r.descripcion_incidente,
r.pais,
r.estadistica,
usr.nombres + ' ' + usr.apelidos [nombres_registra]
from registro_incidentes r
, usuario u
,tipo_sistemas ts
,grupo_asignado gr
,datos_seleccion_area ds
,sop so
,estado_incidente ei
,tier_uno tI
,tier_dos tII
,tier_tres tIII
,usuario usr
where ((@nivel_acceso = 0 and r.codigo_usuario_asignado like '%')
or (@nivel_acceso <> 0 and r.codigo_usuario_asignado like @codigo_usuario))
and r.codigo_usuario_asignado = u.codigo_usuario
and r.codigo_usuario_registra = usr.codigo_usuario
and r.codigo_tipo_sistemas = ts.codigo_tipo_sistemas
and gr.codigo_grupo_asignado = r.codigo_grupo_asignado
and r.codigo_dato_seleccion = ds.codigo_datos_seleccion
and r.codigo_sop = so.codigo_sop
and r.codigo_estado_incidente = ei.codigo_estado_incidente
and r.codigo_tier_uno = tI.codigo_tier_uno
and r.codigo_tier_dos = tII.codigo_tier_dos
and r.codigo_tier_tres = tIII.codigo_tier_tres
and r.fecha_reporte_incidente between @fechaInicio and @fechaFin
order by fecha_reporte_incidente desc
go

create proc [dbo].[obtener_incidente_reporte_fechas_asignados_sp]
@fechaInicio datetime,
@fechaFin datetime,
@codigo_usuario int,
@nivel_acceso int
as
select u.documento,
u.nombres + ' ' + u.apelidos [nombres],
r.codigo_incidente,
r.recibido_sistema,
ts.nombre_tipo_sistemas,
r.fecha_registro_incidente,
r.fecha_reporte_incidente,
r.fecha_primera_intereaccion,
gr.nombre_grupo_asignado,
ds.nombre_tipo_seleccion,
so.nombre_sop,
ei.nombre_estado_incidente,
tI.nombre_categorizacion,
tII.nombre_categorizacion,
tIII.nombre_categorizacion,
r.enviado_sistemas,
r.fecha_cierre,
r.descripcion_incidente,
r.pais,
r.estadistica,
usr.nombres + ' ' + usr.apelidos [nombres_registra]
from registro_incidentes r
, usuario u
,tipo_sistemas ts
,grupo_asignado gr
,datos_seleccion_area ds
,sop so
,estado_incidente ei
,tier_uno tI
,tier_dos tII
,tier_tres tIII
,usuario usr
where ((@nivel_acceso = 0 and r.codigo_usuario_asignado like '%')
or (@nivel_acceso <> 0 and r.codigo_usuario_asignado like @codigo_usuario))
and r.codigo_usuario_asignado = u.codigo_usuario
and r.codigo_usuario_registra = usr.codigo_usuario
and r.codigo_tipo_sistemas = ts.codigo_tipo_sistemas
and gr.codigo_grupo_asignado = r.codigo_grupo_asignado
and r.codigo_dato_seleccion = ds.codigo_datos_seleccion
and r.codigo_sop = so.codigo_sop
and r.codigo_estado_incidente = ei.codigo_estado_incidente
and r.codigo_tier_uno = tI.codigo_tier_uno
and r.codigo_tier_dos = tII.codigo_tier_dos
and r.codigo_tier_tres = tIII.codigo_tier_tres
and r.fecha_registro_incidente between @fechaInicio and @fechaFin
order by fecha_reporte_incidente desc
go
ALTER proc [dbo].[obtener_registro_incidente_sp]
@codigo_incidente varchar(50),
@codigo_usuario int,
@nivel_acceso int
as
select r.codigo_usuario_asignado,
r.codigo_incidente,
r.recibido_sistema,
r.codigo_tipo_sistemas,
r.fecha_registro_incidente,
r.fecha_reporte_incidente,
r.fecha_primera_intereaccion,
r.codigo_grupo_asignado,
r.codigo_dato_seleccion,
r.codigo_sop,
r.codigo_estado_incidente,
r.codigo_tier_uno,
r.codigo_tier_dos,
r.codigo_tier_tres,
r.enviado_sistemas,
r.fecha_cierre,
r.descripcion_incidente,
r.pais,
r.estadistica,
r.codigo_usuario_registra
from registro_incidentes r
where codigo_incidente like @codigo_incidente
and ((@nivel_acceso = 0 and codigo_usuario_asignado like '%')
or (@nivel_acceso <> 0 and codigo_usuario_asignado like @codigo_usuario))
order by fecha_reporte_incidente desc
go
ALTER proc [dbo].[insertar_incidente_sp]
@codigo_usuario int,
@codigo_incidente varchar(50),
@recibido_sistemas bit,
@codigo_tipo_sistemas int,
@fecha_reporte_incidente datetime,
@fecha_primera_interaccion datetime,
@codigo_grupo_asignado int,
@codigo_dato_seleccion int,
@codigo_sop int,
@codigo_estado_incidente int,
@codigo_tier_uno int,
@codigo_tier_dos int,
@codigo_tier_tres int,
@enviado_sistemas bit,
@fecha_cierra datetime,
@descripcion_incidente varchar(500),
@pais varchar(100),
@estadistica bit,
@usuario_asigna int
as
insert into registro_incidentes(
[codigo_usuario_asignado],
[codigo_incidente],
[recibido_sistema],
[codigo_tipo_sistemas],
[fecha_registro_incidente],
[fecha_reporte_incidente],
[fecha_primera_intereaccion],
[codigo_grupo_asignado],
[codigo_dato_seleccion],
[codigo_sop],
[codigo_estado_incidente],
[codigo_tier_uno],
[codigo_tier_dos],
[codigo_tier_tres],
[enviado_sistemas],
[fecha_cierre],
[descripcion_incidente],
[pais],
[estadistica],
[codigo_usuario_registra])
values (@codigo_usuario
	,@codigo_incidente
	,@recibido_sistemas
	,@codigo_tipo_sistemas
	,GETDATE()
	,@fecha_reporte_incidente
	,@fecha_primera_interaccion
	,@codigo_grupo_asignado
	,@codigo_dato_seleccion
	,@codigo_sop
	,@codigo_estado_incidente
	,@codigo_tier_uno
	,@codigo_tier_dos
	,@codigo_tier_tres
	,@enviado_sistemas
	,@fecha_cierra
	,@descripcion_incidente
	,@pais
	,@estadistica
	,@usuario_asigna)
	
go
ALTER proc [dbo].[actualizar_incidente_sp]
@codigo_usuario int,
@codigo_incidente varchar(50),
@recibido_sistemas bit,
@codigo_tipo_sistemas int,
@fecha_reporte_incidente datetime,
@fecha_primera_interaccion datetime,
@codigo_grupo_asignado int,
@codigo_dato_seleccion int,
@codigo_sop int,
@codigo_estado_incidente int,
@codigo_tier_uno int,
@codigo_tier_dos int,
@codigo_tier_tres int,
@enviado_sistemas bit,
@fecha_cierra datetime,
@descripcion_incidente varchar(500),
@pais varchar(100),
@estadistica bit,
@usuario_asigna int
as
update registro_incidentes
set [codigo_usuario_asignado] = @codigo_usuario,
[recibido_sistema] = @recibido_sistemas,
[codigo_tipo_sistemas] = @codigo_tipo_sistemas,
[fecha_reporte_incidente] = @fecha_reporte_incidente,
[fecha_primera_intereaccion] = @fecha_primera_interaccion,
[codigo_grupo_asignado] = @codigo_grupo_asignado,
[codigo_dato_seleccion] = @codigo_dato_seleccion,
[codigo_sop] = @codigo_sop,
[codigo_estado_incidente] = @codigo_estado_incidente,
[codigo_tier_uno] = @codigo_tier_uno,
[codigo_tier_dos] = @codigo_tier_dos,
[codigo_tier_tres] = @codigo_tier_tres,
[enviado_sistemas] = @enviado_sistemas,
[fecha_cierre] =@fecha_cierra,
[descripcion_incidente] = @descripcion_incidente,
[pais] = @pais,
[estadistica] = @estadistica,
[codigo_usuario_registra] = @usuario_asigna
where [codigo_incidente] = @codigo_incidente
go
ALTER proc [dbo].[obtener_registro_incidente_sp]
@codigo_incidente varchar(50),
@codigo_usuario int,
@nivel_acceso int
as
select r.codigo_usuario_asignado,
r.codigo_incidente,
r.recibido_sistema,
r.codigo_tipo_sistemas,
r.fecha_registro_incidente,
r.fecha_reporte_incidente,
r.fecha_primera_intereaccion,
r.codigo_grupo_asignado,
r.codigo_dato_seleccion,
r.codigo_sop,
r.codigo_estado_incidente,
r.codigo_tier_uno,
r.codigo_tier_dos,
r.codigo_tier_tres,
r.enviado_sistemas,
r.fecha_cierre,
r.descripcion_incidente,
r.pais,
r.estadistica,
r.codigo_usuario_registra
from registro_incidentes r
where codigo_incidente like @codigo_incidente
and ((@nivel_acceso = 0 and codigo_usuario_asignado like '%')
or (@nivel_acceso <> 0 and codigo_usuario_asignado like @codigo_usuario))
order by fecha_reporte_incidente desc
