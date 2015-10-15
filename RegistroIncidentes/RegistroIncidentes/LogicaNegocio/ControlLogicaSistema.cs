using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LibreriaControlador.com.ec.BaseDatos;
using LibreriaControlador.com.ec.BeanObjetos;
using LibreriaControlador.com.ec.Excepciones;
using System.Collections.Generic;

namespace RegistroIncidentes.LogicaNegocio
{
    public class ControlLogicaSistema
    {
        private string conexionBase;
        private ConexionBDD mibase;
        public ControlLogicaSistema() {
            System.Configuration.ConnectionStringSettingsCollection connections = ConfigurationManager.ConnectionStrings;
            conexionBase = connections[1].ConnectionString;
            mibase = new ConexionBDD(conexionBase, "LOG_BASE");
        }

        public string insertar_usuario_sistema(UsuarioBean usuario,bool insertar) {
            string mensaje = string.Empty;
            mibase.abrir_conexion_base();
            try
            {
                mibase.insertarActualizarUsuario(usuario, insertar);
                mensaje = "Proceso realizado con exito";
            }
            catch (ExInsertarRegistro ex)
            {
                mensaje = ex.Message;
            }
            catch (ExConexionBase ex) {
                mensaje = ex.Message;
            }
            finally {
                mibase.cerrar_conexion_base();
            }
            return mensaje;
        }

        public UsuarioBean login_usuario_acceso(UsuarioBean acceso) {
            UsuarioBean usuario = new UsuarioBean();
            mibase.abrir_conexion_base();
            try
            {
                List<UsuarioBean> lsUsuario = mibase.obtenerUsurioByDocumento(acceso.getNumeroDocumento(),true);
                if (lsUsuario[0].getPassword().CompareTo(acceso.getPassword()) == 0)
                {
                    usuario = lsUsuario[0];
                }
                else
                {
                    usuario.setPassword("Contraseña incorrecta");
                }

            }
            catch (ExpObtenerRegistro ex)
            {
                usuario.setPassword(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                string ms = ex.Message;
                usuario.setPassword("usuario no existe");
            }
            finally {
                mibase.cerrar_conexion_base();
            }
            return usuario;
        }

        public UsuarioBean obtenerDatosUsuario(string numeroDocumento,bool tipoBusqueda) {
            mibase.abrir_conexion_base();
            List<UsuarioBean> lsUsuario = mibase.obtenerUsurioByDocumento(numeroDocumento,tipoBusqueda);
            mibase.cerrar_conexion_base();
            UsuarioBean usuario = null;
            try
            {
                usuario = lsUsuario[0];
            }
            catch (ArgumentOutOfRangeException ex) {
                string mensaje = ex.Message;
            }
            return usuario;
        }

        public List<UsuarioBean> obtenerDatosUsuarioActivos(string numeroDocumento, bool tipoBusqueda)
        {
            mibase.abrir_conexion_base();
            List<UsuarioBean> lsUsuario = mibase.obtenerUsurioByDocumento(numeroDocumento, tipoBusqueda);
            List<UsuarioBean> lsUsuarioActivos = new List<UsuarioBean>();
            mibase.cerrar_conexion_base();
            foreach(UsuarioBean usr in lsUsuario){
                if (usr.getEstadoUsuario().Equals("A"))
                {
                    lsUsuarioActivos.Add(usr);
                }
            }
            return lsUsuarioActivos;
        }

        public UsuarioBean actualizar_usuario_sistema(UsuarioBean actualizar) {
            UsuarioBean usuario = new UsuarioBean();
            mibase.abrir_conexion_base();
            try
            {
                mibase.insertarActualizarUsuario(actualizar, false);
                usuario = actualizar;
            }
            catch (ExpObtenerRegistro ex)
            {
                usuario.setApellido(null);
                usuario.setPassword(ex.Message);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                string ms = ex.Message;
                usuario.setApellido(null);
                usuario.setPassword("usuario no actualizado");
            }
            catch (ExInsertarRegistro ex)
            {
                string ms = ex.Message;
                usuario.setApellido(null);
                usuario.setPassword(ex.Message);
            }
            catch (Exception ex)
            {
                string ms = ex.Message;
                usuario.setApellido(null);
                usuario.setPassword("Error en actualizacion, contactese con soporte");
            }
            finally {
                mibase.cerrar_conexion_base();
            }
            return usuario;        
        }

        public List<sucesoBean> obtenerIncidentesPorUsuario(string codigoIncidente,UsuarioBean usuario) {
            mibase.abrir_conexion_base();
            List<sucesoBean> sucesos = null;
            try
            {
                sucesos = mibase.obtenerListaSuceso(codigoIncidente, usuario);
            }
            catch (ExpObtenerRegistro ex)
            {
                string msstr = ex.Message;
            }
            finally {
                mibase.cerrar_conexion_base();
            }
            return sucesos;
        }

        public List<SucesoReporteBean> obtenerIncidentesReportePorUsuario(string codigoIncidente,UsuarioBean usuario)
        {
            mibase.abrir_conexion_base();
            List<SucesoReporteBean> sucesos = null;
            try
            {
                sucesos = mibase.obtenerListaSucesoReporte(codigoIncidente, usuario);
            }
            catch (ExpObtenerRegistro ex)
            {
                string msstr = ex.Message;
            }
            finally
            {
                mibase.cerrar_conexion_base();
            }
            return sucesos;
        }

        public List<SucesoReporteBean> obtenerIncidentesAsignadosPorUsuario(string codigoIncidente, UsuarioBean usuario)
        {
            mibase.abrir_conexion_base();
            List<SucesoReporteBean> sucesos = null;
            try
            {
                sucesos = mibase.obtenerListaNoAtendidosReporte(codigoIncidente, usuario);
            }
            catch (ExpObtenerRegistro ex)
            {
                string msstr = ex.Message;
            }
            finally
            {
                mibase.cerrar_conexion_base();
            }
            return sucesos;
        }

        public List<SucesoReporteBean> obtenerReporteFechaUsuario(DateTime inicio,DateTime fin, UsuarioBean usuario,bool isFecha)
        {
            mibase.abrir_conexion_base();
            List<SucesoReporteBean> sucesos = null;
            try
            {
                sucesos = mibase.obtenerListaSucesoReportePorFecha(inicio, fin, usuario,isFecha);
            }
            catch (ExpObtenerRegistro ex)
            {
                string msstr = ex.Message;
            }
            finally
            {
                mibase.cerrar_conexion_base();
            }
            return sucesos;
        }

        public List<SucesoReporteBean> obtenerReporteFechaUsuarioAsignados(DateTime inicio, DateTime fin, UsuarioBean usuario)
        {
            mibase.abrir_conexion_base();
            List<SucesoReporteBean> sucesos = null;
            try
            {
                sucesos = mibase.obtenerListaSucesoReportePorFecha(inicio, fin, usuario,true);
            }
            catch (ExpObtenerRegistro ex)
            {
                string msstr = ex.Message;
            }
            finally
            {
                mibase.cerrar_conexion_base();
            }
            return sucesos;
        }

        public List<TipSistemasBean> obtenerListaTipoSistemas(string estado) {
            mibase.abrir_conexion_base();
            List<TipSistemasBean> lstipoSistemas = null;
            try
            {
                lstipoSistemas = mibase.obtenerTipoSistemas(estado);
            }
            catch (ExpObtenerRegistro e)
            {
                string ms = e.Message;
            }
            finally {
                mibase.cerrar_conexion_base();
            }
            return lstipoSistemas;
        }

        public string insertarActualizarSucesoUsuario(sucesoBean sucesoUsr) {
            mibase.abrir_conexion_base();
            UsuarioBean usr = new UsuarioBean();
            usr.setCodigoUsuario(0);
            string mensaje = string.Empty;
            try
            {
                List<sucesoBean> sucesos = mibase.obtenerListaSuceso(sucesoUsr.codigoIncidente, usr);
                if (sucesos.Count > 0)
                {
                    //actualizamos
                    mibase.insertarActulizarSuceso(sucesoUsr, false);
                }
                else
                {
                    // insertamos
                    mibase.insertarActulizarSuceso(sucesoUsr, true);
                }
                mensaje = "Proceso realizado con éxito";
            }
            catch (ExInsertarRegistro ex)
            {
                mensaje = ex.Message;
            }
            catch (ExConexionBase ex)
            {
                mensaje = ex.Message;
            }
            finally { 
            mibase.cerrar_conexion_base();
            }
            return mensaje;
        }

        public List<GrupoAsignadoBean> obtenerListGrupo(string estado) {
            List<GrupoAsignadoBean> grupos = null;
            mibase.abrir_conexion_base();
            try
            {
                grupos = mibase.obtenerListaGrupoAsignado(estado);
            }
            catch (ExpObtenerRegistro e)
            {
                string ms = e.Message;
            }
            finally {
                mibase.cerrar_conexion_base();
            }
            return grupos;
        }

        public List<datoSeleccionAreaBean> obtenerListDatoArea(string estado)
        {
            List<datoSeleccionAreaBean> grupos = null;
            mibase.abrir_conexion_base();
            try
            {
                grupos = mibase.obtenerListaAreaSeleccion(estado);
            }
            catch (ExpObtenerRegistro e)
            {
                string ms = e.Message;
            }
            finally
            {
                mibase.cerrar_conexion_base();
            }
            return grupos;
        }

        public List<sopBean> obtenerListSop(string estado)
        {
            List<sopBean> grupos = null;
            mibase.abrir_conexion_base();
            try
            {
                grupos = mibase.obtenerListaSoap(estado);
            }
            catch (ExpObtenerRegistro e)
            {
                string ms = e.Message;
            }
            finally
            {
                mibase.cerrar_conexion_base();
            }
            return grupos;
        }

        public List<EstadoIncidenteBean> obtenerListEstadoIncidente(string estado)
        {
            List<EstadoIncidenteBean> grupos = null;
            mibase.abrir_conexion_base();
            try
            {
                grupos = mibase.obtenerListaEstadoIncidencia(estado);
            }
            catch (ExpObtenerRegistro e)
            {
                string ms = e.Message;
            }
            finally
            {
                mibase.cerrar_conexion_base();
            }
            return grupos;
        }

        public List<TierUnoBean> obtenerListTierUno(string estado)
        {
            List<TierUnoBean> grupos = null;
            mibase.abrir_conexion_base();
            try
            {
                grupos = mibase.obtenerTierUno("A");
            }
            catch (ExpObtenerRegistro e)
            {
                string ms = e.Message;
            }
            finally
            {
                mibase.cerrar_conexion_base();
            }
            return grupos;
        }

        public List<TierDosBean> obtenerListTierDos(string estado,int codigoTierUno)
        {
            List<TierDosBean> grupos = null;
            mibase.abrir_conexion_base();
            try
            {
                TierUnoBean tierU = new TierUnoBean();
                tierU.codigoTierUno = codigoTierUno;
                grupos = mibase.obtenerTierDos(tierU,"A");
            }
            catch (ExpObtenerRegistro e)
            {
                string ms = e.Message;
            }
            finally
            {
                mibase.cerrar_conexion_base();
            }
            return grupos;
        }

        public List<TiertresBean> obtenerListTierTres(string estado, int codigoTierDos)
        {
            List<TiertresBean> grupos = null;
            mibase.abrir_conexion_base();
            try
            {
                TierDosBean tierD = new TierDosBean();
                tierD.codigoTierDos = codigoTierDos;
                grupos = mibase.obtenerTierTres(tierD, "A");
            }
            catch (ExpObtenerRegistro e)
            {
                string ms = e.Message;
            }
            finally
            {
                mibase.cerrar_conexion_base();
            }
            return grupos;
        }
    }
}
