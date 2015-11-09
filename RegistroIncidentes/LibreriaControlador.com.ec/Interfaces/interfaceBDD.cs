using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibreriaControlador.com.ec.BeanObjetos;

namespace LibreriaControlador.com.ec.Interfaces
{
    interface interfaceBDD
    {
        void abrir_conexion_base();
        void cerrar_conexion_base();
        void cambiar_cadena_conexion(string urlNueva);
        void insertar_usuario(UsuarioBean usuario);
        List<UsuarioBean> obtenerUsurioByDocumento(string documento,bool tipoBusqueda);
        int insertarActualizarUsuario(UsuarioBean usuario,bool insertar);
        List<datoSeleccionAreaBean> obtenerListaAreaSeleccion(string estado);
        List<EstadoIncidenteBean> obtenerListaEstadoIncidencia(string estado);
        List<GrupoAsignadoBean> obtenerListaGrupoAsignado(string estado);
        List<NivelIncidenteBean> obtenerListaNivelIncidente(string estado);
        List<sopBean> obtenerListaSoap(string estado);
        List<sucesoBean> obtenerListaSuceso(string codigoIncidente, UsuarioBean usuario);
        List<SucesoReporteBean> obtenerListaSucesoReporte(string codigoIncidente, UsuarioBean usuario);
        List<SucesoReporteBean> obtenerListaNoAtendidosReporte(string codigoIncidente, UsuarioBean usuario);
        List<SucesoReporteBean> obtenerListaSucesoReportePorFecha(DateTime fechaInicio,DateTime fechaFin, UsuarioBean usuario,bool asignado);
        List<TierUnoBean> obtenerTierUno(string estado);
        List<TierDosBean> obtenerTierDos(TierUnoBean tierUno, string estado);
        List<TiertresBean> obtenerTierTres(TierDosBean tierDos, string estado);
        string insertarActulizarSuceso(sucesoBean suceso, bool insertar);
        List<TipSistemasBean> obtenerTipoSistemas(string estado);
        List<ParametroConfiguracion> obtenerParametroConfiguracion(int codigoParametro,string estado);
        string validarCodigoIndicedentee(string codigoIncidente);
    }

}
