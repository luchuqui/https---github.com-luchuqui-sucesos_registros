using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using LibreriaControlador.com.ec.Utilitario;
using LibreriaControlador.com.ec.Excepciones;
using System.Data;
using LibreriaControlador.com.ec.BeanObjetos;
using LibreriaControlador.com.ec.Interfaces;

namespace LibreriaControlador.com.ec.BaseDatos
{
    public class ConexionBDD : interfaceBDD
    {
        private string url = string.Empty;
        private SqlConnection conn; //Establecimiento de conexion a la base de datos.
        private LecturaEscrituraArchivo logs;

        public ConexionBDD(string url, string pathGuardar)
        {
            this.url = url;
            conn = new SqlConnection(this.url);
            logs = new LecturaEscrituraArchivo();
            logs.set_path_guardar(pathGuardar);
            logs.archivo_guardar("");
            logs.set_path_guardar("LOG_BDD");
        }

        public void set_archivo_path_guradar(string path)
        {
            logs.set_path_guardar(path);
        }

        #region Miembros de interfaceBDD

        public void abrir_conexion_base()
        {
            try
            {
                conn.Open();

            }
            catch (SqlException e)
            {
                logs.escritura_archivo_string(e.Message);
                throw new ExConexionBase(e.Message);
            }
        }

        public void cerrar_conexion_base()
        {
            try
            {
                conn.Close();
            }
            catch (SqlException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                //logs.cerrar_archivo();
                throw new ExConexionBase(ex.Message);
            }
        }

        public void cambiar_cadena_conexion(string urlNueva)
        {
            url = urlNueva;
            try
            {
                conn.Dispose();
                conn.Close();
                conn.ConnectionString = url;
            }
            catch (SqlException ex)
            {
                logs.escritura_archivo_string(ex.Message);
                //logs.cerrar_archivo();
                throw new ExConexionBase(ex.Message);
            }
        }

        public void insertar_usuario(UsuarioBean usuario)
        {
            SqlCommand cmd = new SqlCommand("insertar_usuario_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@documento", usuario.getNumeroDocumento());
            cmd.Parameters.AddWithValue("@nombre", usuario.getNombres());
            cmd.Parameters.AddWithValue("@apellido", usuario.getApellido());
            cmd.Parameters.AddWithValue("@password", usuario.getPassword());
            cmd.Parameters.AddWithValue("@correo", usuario.getCorreo());
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i == 0)
                {
                    throw new ExInsertarRegistro("Usuario "+usuario.getNumeroDocumento()+ ", No se inserto revise datos");
                }
            }
            catch (ArgumentException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExInsertarRegistro("Error en  procesos");
            }
            catch (InvalidOperationException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExConexionBase(ex.Message);
            }
            catch (SqlException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExConexionBase(ex.Message);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExConexionBase(ex.Message);
            }
        }

        public List<UsuarioBean> obtenerUsurioByDocumento(string documento, bool tipoBusqueda)
        {
            SqlCommand cmd = null;
            if (tipoBusqueda) // true es documento
            {
                cmd = new SqlCommand("obtener_usuario_sp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@documento", documento);
            }
            else {
                cmd = new SqlCommand("obtener_usuario_id_sp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigo_usuario", documento);
            }
            List<UsuarioBean> usuarios = new List<UsuarioBean>();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable("UsuarioBean");
                da.Fill(tb);
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    UsuarioBean usuario = new UsuarioBean();
                    usuario.setCodigoUsuario(Convert.ToInt16(tb.Rows[i][0].ToString()));
                    usuario.setNumeroDocumento(tb.Rows[i][1].ToString());
                    usuario.setNombres(tb.Rows[i][2].ToString());
                    usuario.setApellido(tb.Rows[i][3].ToString());
                    usuario.setEstadoUsuario(tb.Rows[i][4].ToString());
                    usuario.setFechaCreacion(DateTime.Parse(tb.Rows[i][5].ToString()));
                    if (!string.IsNullOrEmpty(tb.Rows[i][6].ToString()))
                    usuario.setFechaUltimoAcceso(DateTime.Parse(tb.Rows[i][6].ToString()));
                    usuario.setNivelAcceso(Convert.ToInt16(tb.Rows[i][7].ToString()));
                    usuario.setPassword(tb.Rows[i][8].ToString());
                    usuario.setCorreo(tb.Rows[i][9].ToString());
                    usuarios.Add(usuario);
                } return usuarios;
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExpObtenerRegistro(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExpObtenerRegistro(ex.Message);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new Exception(ex.Message);
            }

        }

        public int insertarActualizarUsuario(UsuarioBean usuario, bool insertar)
        {
            SqlCommand cmd = null;
            string mensaje = "";
            if (insertar)
            {//True inseertar
                mensaje = "inserto";
                cmd = new SqlCommand("insertar_usuario_sp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@documento", usuario.getNumeroDocumento());
                cmd.Parameters.AddWithValue("@nombre", usuario.getNombres());
                cmd.Parameters.AddWithValue("@apellido", usuario.getApellido());
                cmd.Parameters.AddWithValue("@password", usuario.getPassword());
                cmd.Parameters.AddWithValue("@correo", usuario.getCorreo());
                
            }
            else { //false actuliza
                mensaje = "actualizo";
                cmd = new SqlCommand("actualizar_datos_usuario_sp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_usuario", usuario.getCodigoUsuario());
                cmd.Parameters.AddWithValue("@documento", usuario.getNumeroDocumento());
                cmd.Parameters.AddWithValue("@nombre", usuario.getNombres());
                cmd.Parameters.AddWithValue("@apellido", usuario.getApellido());
                cmd.Parameters.AddWithValue("@password", usuario.getPassword());
                cmd.Parameters.AddWithValue("@correo", usuario.getCorreo());
                cmd.Parameters.AddWithValue("@estado", usuario.getEstadoUsuario());
                cmd.Parameters.AddWithValue("@fecha_ingreso", usuario.getFechaUltimoAcceso());
            }  
            
            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i == 0)
                {
                    
                    throw new ExInsertarRegistro("Usuario " + usuario.getNumeroDocumento() + ", No se "+mensaje+" revise datos");
                }
                else {
                    return i;
                }
                
            }
            catch (ArgumentException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExInsertarRegistro("Error en  procesos");
            }
            catch (InvalidOperationException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExConexionBase(ex.Message);
            }
            catch (SqlException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                if (ex.Message.Contains("numero_documento_unique")) {
                    throw new ExInsertarRegistro("Número de documento ya existe en el sistema");
                }
                throw new ExConexionBase(ex.Message);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExConexionBase(ex.Message);
            }
        }

        public List<datoSeleccionAreaBean> obtenerListaAreaSeleccion(string estado)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand("obtener_datos_seleccion_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@estado_seleccion", estado);
            List<datoSeleccionAreaBean> selecciones = new List<datoSeleccionAreaBean>();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable("SeleccionAreaBean");
                da.Fill(tb);
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    datoSeleccionAreaBean seleccion = new datoSeleccionAreaBean();
                    seleccion.codigoDatoSeleccion = Convert.ToInt16(tb.Rows[i][0].ToString());
                    seleccion.nombreTipoSeleccion = tb.Rows[i][1].ToString();
                    seleccion.estadoSeleccion = tb.Rows[i][2].ToString();
                    selecciones.Add(seleccion);
                } return selecciones;
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExpObtenerRegistro(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExpObtenerRegistro(ex.Message);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new Exception(ex.Message);
            }

        }

        public List<EstadoIncidenteBean> obtenerListaEstadoIncidencia(string estado)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand("obtener_estado_incidente_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@estado_incidente", estado);
            List<EstadoIncidenteBean> selecciones = new List<EstadoIncidenteBean>();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable("EstadoIncidenteBean");
                da.Fill(tb);
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    EstadoIncidenteBean seleccion = new EstadoIncidenteBean();
                    seleccion.codigoEstadoIncidente = Convert.ToInt16(tb.Rows[i][0].ToString());
                    seleccion.nombreEstadoIncidente = tb.Rows[i][1].ToString();
                    seleccion.estadoIncidente = tb.Rows[i][2].ToString();
                    selecciones.Add(seleccion);
                } return selecciones;
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExpObtenerRegistro(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExpObtenerRegistro(ex.Message);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new Exception(ex.Message);
            }
        }

        public List<GrupoAsignadoBean> obtenerListaGrupoAsignado(string estado)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand("obtener_grupo_asignado_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@estado_incidente", estado);
            List<GrupoAsignadoBean> selecciones = new List<GrupoAsignadoBean>();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable("GrupoAsignadoBean");
                da.Fill(tb);
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    GrupoAsignadoBean seleccion = new GrupoAsignadoBean();
                    seleccion.codigoGrupo = Convert.ToInt16(tb.Rows[i][0].ToString());
                    seleccion.nombregrupoAsignado = tb.Rows[i][1].ToString();
                    seleccion.estadoGrupo = tb.Rows[i][2].ToString();
                    selecciones.Add(seleccion);
                } return selecciones;
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExpObtenerRegistro(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExpObtenerRegistro(ex.Message);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new Exception(ex.Message);
            }
        }

        public List<NivelIncidenteBean> obtenerListaNivelIncidente(string estado)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand("obtener_nivel_incidente_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@estado_nivel", estado);
            List<NivelIncidenteBean> selecciones = new List<NivelIncidenteBean>();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable("NivelIncidenteBean");
                da.Fill(tb);
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    NivelIncidenteBean seleccion = new NivelIncidenteBean();
                    seleccion.codigoNivelIncidente = Convert.ToInt16(tb.Rows[i][0].ToString());
                    seleccion.nombreIncidente = tb.Rows[i][1].ToString();
                    seleccion.estadoNivel = tb.Rows[i][2].ToString();
                    selecciones.Add(seleccion);
                } return selecciones;
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExpObtenerRegistro(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExpObtenerRegistro(ex.Message);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new Exception(ex.Message);
            }
        }

        public List<sopBean> obtenerListaSoap(string estado)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand("obtener_sop_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@estado_sop", estado);
            List<sopBean> selecciones = new List<sopBean>();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable("SopBean");
                da.Fill(tb);
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    sopBean seleccion = new sopBean();
                    seleccion.codigoSop = Convert.ToInt16(tb.Rows[i][0].ToString());
                    seleccion.nombreSop = tb.Rows[i][1].ToString();
                    seleccion.estadoSop = tb.Rows[i][2].ToString();
                    selecciones.Add(seleccion);
                } return selecciones;
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExpObtenerRegistro(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExpObtenerRegistro(ex.Message);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new Exception(ex.Message);
            }
        }

        public List<sucesoBean> obtenerListaSuceso(string codigoIncidente,UsuarioBean usuario)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand("obtener_registro_incidente_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@codigo_incidente", codigoIncidente);
            cmd.Parameters.AddWithValue("@codigo_usuario", usuario.getCodigoUsuario());
            cmd.Parameters.AddWithValue("@nivel_acceso", usuario.getNivelAcceso());
            List<sucesoBean> selecciones = new List<sucesoBean>();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable("SucesoBean");
                da.Fill(tb);
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    sucesoBean seleccion = new sucesoBean();
                    seleccion.codigo_usuario = Convert.ToInt16(tb.Rows[i][0].ToString());
                    seleccion.codigoIncidente = tb.Rows[i][1].ToString();
                    seleccion.recibidoSistemas = bool.Parse(tb.Rows[i][2].ToString());
                    seleccion.codigoTipoSistemas = Convert.ToInt16(tb.Rows[i][3].ToString());
                    seleccion.registroIncidente = DateTime.Parse(tb.Rows[i][4].ToString());
                    seleccion.reporteIncidente = DateTime.Parse(tb.Rows[i][5].ToString());
                    seleccion.primerInteraccion = DateTime.Parse(tb.Rows[i][6].ToString());
                    seleccion.codigoGrupoAsignado = Convert.ToInt16(tb.Rows[i][7].ToString());
                    seleccion.codigoDato = Convert.ToInt16(tb.Rows[i][8].ToString());
                    seleccion.codigoSop = Convert.ToInt16(tb.Rows[i][9].ToString());
                    seleccion.estadoIncidente = Convert.ToInt16(tb.Rows[i][10].ToString());
                    seleccion.codigoTierUno = Convert.ToInt16(tb.Rows[i][11].ToString());
                    seleccion.codigoTierDos = Convert.ToInt16(tb.Rows[i][12].ToString());
                    seleccion.codigoTierTres = Convert.ToInt16(tb.Rows[i][13].ToString());
                    seleccion.enviadoSistmas = bool.Parse(tb.Rows[i][14].ToString());
                    seleccion.fechaCierre = DateTime.Parse(tb.Rows[i][15].ToString());
                    seleccion.descripcionincidente = tb.Rows[i][16].ToString();
                    seleccion.pais = tb.Rows[i][17].ToString();
                    seleccion.estadistica = bool.Parse(tb.Rows[i][18].ToString());
                    seleccion.codigo_usuario_reporta = Convert.ToInt16(tb.Rows[i][19].ToString());
                    selecciones.Add(seleccion);
                } return selecciones;
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExpObtenerRegistro(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExpObtenerRegistro(ex.Message);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new Exception(ex.Message);
            }
        }

        public List<TierUnoBean> obtenerTierUno(string estado)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand("obtener_tier_uno_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@estado_tier", estado);
            List<TierUnoBean> selecciones = new List<TierUnoBean>();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable("TierUnoBean");
                da.Fill(tb);
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    TierUnoBean seleccion = new TierUnoBean();
                    seleccion.codigoTierUno = Convert.ToInt16(tb.Rows[i][0].ToString());
                    seleccion.nombreCategorizacion = tb.Rows[i][1].ToString();
                    seleccion.estadoCategorizacion = tb.Rows[i][2].ToString();
                    selecciones.Add(seleccion);
                } return selecciones;
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExpObtenerRegistro(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExpObtenerRegistro(ex.Message);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new Exception(ex.Message);
            }
        }

        public List<TierDosBean> obtenerTierDos(TierUnoBean tierUno, string estado)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand("obtener_tier_dos_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@estado_tier", estado);
            cmd.Parameters.AddWithValue("@codigo_tier_uno", tierUno.codigoTierUno);
            List<TierDosBean> selecciones = new List<TierDosBean>();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable("TierDosBean");
                da.Fill(tb);
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    TierDosBean seleccion = new TierDosBean();
                    seleccion.codigoTierDos = Convert.ToInt16(tb.Rows[i][0].ToString());
                    seleccion.nombreCategorizacion = tb.Rows[i][1].ToString();
                    seleccion.estadoCategorizacoin = tb.Rows[i][2].ToString();
                    seleccion.codigoTierUno = Convert.ToInt16(tb.Rows[i][3].ToString());
                    selecciones.Add(seleccion);
                } return selecciones;
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExpObtenerRegistro(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExpObtenerRegistro(ex.Message);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new Exception(ex.Message);
            }
        }

        public List<TiertresBean> obtenerTierTres(TierDosBean tierDos, string estado)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand("obtener_tier_tres_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@estado_tier", estado);
            cmd.Parameters.AddWithValue("@codigo_tier_dos", tierDos.codigoTierDos);
            List<TiertresBean> selecciones = new List<TiertresBean>();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable("TierDosBean");
                da.Fill(tb);
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    TiertresBean seleccion = new TiertresBean();
                    seleccion.codigoTierTres = Convert.ToInt16(tb.Rows[i][0].ToString());
                    seleccion.nombreCategorizacion = tb.Rows[i][1].ToString();
                    seleccion.estadoCategorizacoin = tb.Rows[i][2].ToString();
                    seleccion.codigoTierDos = Convert.ToInt16(tb.Rows[i][3].ToString());
                    selecciones.Add(seleccion);
                } return selecciones;
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExpObtenerRegistro(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExpObtenerRegistro(ex.Message);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Miembros de interfaceBDD


        public int insertarActulizarSuceso(sucesoBean suceso, bool insertar)
        {
            SqlCommand cmd = null;
            string mensaje = "";
            if (insertar)
            {//True inseertar
                mensaje = "inserto";
                cmd = new SqlCommand("insertar_incidente_sp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigo_usuario", suceso.codigo_usuario);
                cmd.Parameters.AddWithValue("@codigo_incidente", suceso.codigoIncidente.Trim());
                cmd.Parameters.AddWithValue("@recibido_sistemas", suceso.recibidoSistemas);
                cmd.Parameters.AddWithValue("@codigo_tipo_sistemas", suceso.codigoTipoSistemas);
                cmd.Parameters.AddWithValue("@fecha_reporte_incidente", suceso.registroIncidente);
                cmd.Parameters.AddWithValue("@fecha_primera_interaccion", suceso.primerInteraccion);
                cmd.Parameters.AddWithValue("@codigo_grupo_asignado", suceso.codigoGrupoAsignado);
                cmd.Parameters.AddWithValue("@codigo_dato_seleccion", suceso.codigoDato);
                cmd.Parameters.AddWithValue("@codigo_sop", suceso.codigoSop);
                cmd.Parameters.AddWithValue("@codigo_estado_incidente", suceso.estadoIncidente);
                cmd.Parameters.AddWithValue("@codigo_tier_uno", suceso.codigoTierUno);
                cmd.Parameters.AddWithValue("@codigo_tier_dos", suceso.codigoTierDos);
                cmd.Parameters.AddWithValue("@codigo_tier_tres", suceso.codigoTierTres);
                cmd.Parameters.AddWithValue("@enviado_sistemas", suceso.enviadoSistmas);
                cmd.Parameters.AddWithValue("@fecha_cierra", suceso.fechaCierre);
                cmd.Parameters.AddWithValue("@descripcion_incidente", suceso.descripcionincidente);
                cmd.Parameters.AddWithValue("@pais", suceso.pais);
                cmd.Parameters.AddWithValue("@estadistica", suceso.estadistica);
                cmd.Parameters.AddWithValue("@usuario_asigna", suceso.codigo_usuario_reporta);
            }
            else
            { //false actuliza
                mensaje = "actualizo";
                cmd = new SqlCommand("actualizar_incidente_sp", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@codigo_usuario", suceso.codigo_usuario);
                cmd.Parameters.AddWithValue("@codigo_incidente", suceso.codigoIncidente.Trim());
                cmd.Parameters.AddWithValue("@recibido_sistemas", suceso.recibidoSistemas);
                cmd.Parameters.AddWithValue("@codigo_tipo_sistemas", suceso.codigoTipoSistemas);
                cmd.Parameters.AddWithValue("@fecha_reporte_incidente", suceso.registroIncidente);
                cmd.Parameters.AddWithValue("@fecha_primera_interaccion", suceso.primerInteraccion);
                cmd.Parameters.AddWithValue("@codigo_grupo_asignado", suceso.codigoGrupoAsignado);
                cmd.Parameters.AddWithValue("@codigo_dato_seleccion", suceso.codigoDato);
                cmd.Parameters.AddWithValue("@codigo_sop", suceso.codigoSop);
                cmd.Parameters.AddWithValue("@codigo_estado_incidente", suceso.estadoIncidente);
                cmd.Parameters.AddWithValue("@codigo_tier_uno", suceso.codigoTierUno);
                cmd.Parameters.AddWithValue("@codigo_tier_dos", suceso.codigoTierDos);
                cmd.Parameters.AddWithValue("@codigo_tier_tres", suceso.codigoTierTres);
                cmd.Parameters.AddWithValue("@enviado_sistemas", suceso.enviadoSistmas);
                cmd.Parameters.AddWithValue("@fecha_cierra", suceso.fechaCierre);
                cmd.Parameters.AddWithValue("@descripcion_incidente", suceso.descripcionincidente);
                cmd.Parameters.AddWithValue("@pais", suceso.pais);
                cmd.Parameters.AddWithValue("@estadistica", suceso.estadistica);
                cmd.Parameters.AddWithValue("@usuario_asigna", suceso.codigo_usuario_reporta);
            }

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i == 0)
                {

                    throw new ExInsertarRegistro("Usuario " + suceso.codigoIncidente + ", No se " + mensaje + " revise datos");
                }
                else
                {
                    return i;
                }

            }
            catch (ArgumentException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExInsertarRegistro("Error en  procesos");
            }
            catch (InvalidOperationException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExConexionBase(ex.Message);
            }
            catch (SqlException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExConexionBase(ex.Message);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExConexionBase(ex.Message);
            }
        }

        #endregion

        #region Miembros de interfaceBDD


        public List<TipSistemasBean> obtenerTipoSistemas(string estado)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand("obtener_tipo_sistemas_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@estado_tipo", estado);
            
            List<TipSistemasBean> selecciones = new List<TipSistemasBean>();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable("TipoSistemaBean");
                da.Fill(tb);
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    TipSistemasBean seleccion = new TipSistemasBean();
                    seleccion.codigoTipoSistemas = Convert.ToInt16(tb.Rows[i][0].ToString());
                    seleccion.nombreTipoSistemas = tb.Rows[i][1].ToString();
                    seleccion.estadoTipoSisteas = tb.Rows[i][2].ToString();
                    selecciones.Add(seleccion);
                } return selecciones;
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExpObtenerRegistro(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExpObtenerRegistro(ex.Message);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Miembros de interfaceBDD


        public List<SucesoReporteBean> obtenerListaSucesoReporte(string codigoIncidente, UsuarioBean usuario)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand("obtener_registro_incidente_reporte_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@codigo_incidente", codigoIncidente);
            cmd.Parameters.AddWithValue("@codigo_usuario", usuario.getCodigoUsuario());
            cmd.Parameters.AddWithValue("@nivel_acceso", usuario.getNivelAcceso());
            List<SucesoReporteBean> selecciones = new List<SucesoReporteBean>();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable("SucesoBean");
                da.Fill(tb);
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    SucesoReporteBean seleccion = new SucesoReporteBean();
                    seleccion.documento = tb.Rows[i][0].ToString();
                    seleccion.codigo_usuario = tb.Rows[i][1].ToString();
                    seleccion.codigoIncidente = tb.Rows[i][2].ToString();
                    seleccion.recibidoSistemas = bool.Parse(tb.Rows[i][3].ToString());
                    seleccion.codigoTipoSistemas = tb.Rows[i][4].ToString();
                    seleccion.registroIncidente = DateTime.Parse(tb.Rows[i][5].ToString()).ToString("MM/dd/yyyy HH:mm");
                    seleccion.reporteIncidente = DateTime.Parse(tb.Rows[i][6].ToString()).ToString("MM/dd/yyyy HH:mm");
                    seleccion.primerInteraccion = DateTime.Parse(tb.Rows[i][7].ToString()).ToString("MM/dd/yyyy HH:mm");
                    seleccion.codigoGrupoAsignado = tb.Rows[i][8].ToString();
                    seleccion.codigoDato = tb.Rows[i][9].ToString();
                    seleccion.codigoSop = tb.Rows[i][10].ToString();
                    seleccion.estadoIncidente = tb.Rows[i][11].ToString();
                    seleccion.codigoTierUno = tb.Rows[i][12].ToString();
                    seleccion.codigoTierDos = tb.Rows[i][13].ToString();
                    seleccion.codigoTierTres = tb.Rows[i][14].ToString();
                    seleccion.enviadoSistmas = bool.Parse(tb.Rows[i][15].ToString());
                    seleccion.fechaCierre = DateTime.Parse(tb.Rows[i][16].ToString()).ToString("MM/dd/yyyy HH:mm");
                    seleccion.descripcionincidente = tb.Rows[i][17].ToString();
                    seleccion.pais = tb.Rows[i][18].ToString();
                    seleccion.estadistica = bool.Parse(tb.Rows[i][19].ToString());
                    selecciones.Add(seleccion);
                } return selecciones;
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExpObtenerRegistro(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExpObtenerRegistro(ex.Message);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Miembros de interfaceBDD


        public List<SucesoReporteBean> obtenerListaSucesoReportePorFecha(DateTime fechaInicio, DateTime fechaFin, UsuarioBean usuario,bool asignado)
        {
            SqlCommand cmd = null;
            if(asignado){
                cmd = new SqlCommand("obtener_incidente_reporte_fechas_asignados_sp", conn);
            }else{
            cmd = new SqlCommand("obtener_incidente_reporte_fechas_sp", conn);
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fechaInicio", fechaInicio);
            cmd.Parameters.AddWithValue("@fechaFin", fechaFin);
            cmd.Parameters.AddWithValue("@codigo_usuario", usuario.getCodigoUsuario());
            cmd.Parameters.AddWithValue("@nivel_acceso", usuario.getNivelAcceso());
            List<SucesoReporteBean> selecciones = new List<SucesoReporteBean>();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable("SucesoBean");
                da.Fill(tb);
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    SucesoReporteBean seleccion = new SucesoReporteBean();
                    seleccion.documento = tb.Rows[i][0].ToString();
                    seleccion.codigo_usuario = tb.Rows[i][1].ToString();
                    seleccion.codigoIncidente = tb.Rows[i][2].ToString();
                    seleccion.recibidoSistemas = bool.Parse(tb.Rows[i][3].ToString());
                    seleccion.codigoTipoSistemas = tb.Rows[i][4].ToString();
                    seleccion.registroIncidente = DateTime.Parse(tb.Rows[i][5].ToString()).ToString("MM/dd/yyyy HH:mm"); ;
                    seleccion.reporteIncidente = DateTime.Parse(tb.Rows[i][6].ToString()).ToString("MM/dd/yyyy HH:mm"); ;
                    seleccion.primerInteraccion = DateTime.Parse(tb.Rows[i][7].ToString()).ToString("MM/dd/yyyy HH:mm"); ;
                    seleccion.codigoGrupoAsignado = tb.Rows[i][8].ToString();
                    seleccion.codigoDato = tb.Rows[i][9].ToString();
                    seleccion.codigoSop = tb.Rows[i][10].ToString();
                    seleccion.estadoIncidente = tb.Rows[i][11].ToString();
                    seleccion.codigoTierUno = tb.Rows[i][12].ToString();
                    seleccion.codigoTierDos = tb.Rows[i][13].ToString();
                    seleccion.codigoTierTres = tb.Rows[i][14].ToString();
                    seleccion.enviadoSistmas = bool.Parse(tb.Rows[i][15].ToString());
                    seleccion.fechaCierre = DateTime.Parse(tb.Rows[i][16].ToString()).ToString("MM/dd/yyyy HH:mm");;
                    seleccion.descripcionincidente = tb.Rows[i][17].ToString();
                    seleccion.pais = tb.Rows[i][18].ToString();
                    seleccion.estadistica = bool.Parse(tb.Rows[i][19].ToString());
                    seleccion.usuarioAsigna = tb.Rows[i][20].ToString();
                    selecciones.Add(seleccion);
                } return selecciones;
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExpObtenerRegistro(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExpObtenerRegistro(ex.Message);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new Exception(ex.Message);
            }

        }

        #endregion

        #region Miembros de interfaceBDD


        public List<SucesoReporteBean> obtenerListaNoAtendidosReporte(string codigoIncidente, UsuarioBean usuario)
        {
            SqlCommand cmd = null;
            cmd = new SqlCommand("obtener_registro_asignado_reporte_sp", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@codigo_incidente", codigoIncidente);
            cmd.Parameters.AddWithValue("@codigo_usuario", usuario.getCodigoUsuario());
            cmd.Parameters.AddWithValue("@nivel_acceso", usuario.getNivelAcceso());
            List<SucesoReporteBean> selecciones = new List<SucesoReporteBean>();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable tb = new DataTable("SucesoBean");
                da.Fill(tb);
                for (int i = 0; i < tb.Rows.Count; i++)
                {
                    SucesoReporteBean seleccion = new SucesoReporteBean();
                    seleccion.documento = tb.Rows[i][0].ToString();
                    seleccion.codigo_usuario = tb.Rows[i][1].ToString();
                    seleccion.codigoIncidente = tb.Rows[i][2].ToString();
                    seleccion.recibidoSistemas = bool.Parse(tb.Rows[i][3].ToString());
                    seleccion.codigoTipoSistemas = tb.Rows[i][4].ToString();
                    seleccion.registroIncidente = DateTime.Parse(tb.Rows[i][5].ToString()).ToString("MM/dd/yyyy HH:mm");
                    seleccion.reporteIncidente = DateTime.Parse(tb.Rows[i][6].ToString()).ToString("MM/dd/yyyy HH:mm");
                    seleccion.primerInteraccion = DateTime.Parse(tb.Rows[i][7].ToString()).ToString("MM/dd/yyyy HH:mm");
                    seleccion.codigoGrupoAsignado = tb.Rows[i][8].ToString();
                    seleccion.codigoDato = tb.Rows[i][9].ToString();
                    seleccion.codigoSop = tb.Rows[i][10].ToString();
                    seleccion.estadoIncidente = tb.Rows[i][11].ToString();
                    seleccion.codigoTierUno = tb.Rows[i][12].ToString();
                    seleccion.codigoTierDos = tb.Rows[i][13].ToString();
                    seleccion.codigoTierTres = tb.Rows[i][14].ToString();
                    seleccion.enviadoSistmas = bool.Parse(tb.Rows[i][15].ToString());
                    seleccion.fechaCierre = DateTime.Parse(tb.Rows[i][16].ToString()).ToString("MM/dd/yyyy HH:mm");
                    seleccion.descripcionincidente = tb.Rows[i][17].ToString();
                    seleccion.pais = tb.Rows[i][18].ToString();
                    seleccion.estadistica = bool.Parse(tb.Rows[i][19].ToString());
                    selecciones.Add(seleccion);
                } return selecciones;
            }
            catch (IndexOutOfRangeException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExpObtenerRegistro(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new ExpObtenerRegistro(ex.Message);
            }
            catch (Exception ex)
            {
                logs.escritura_archivo_string_ex(ex);
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
