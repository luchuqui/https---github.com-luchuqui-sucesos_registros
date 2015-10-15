using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LibreriaControlador.com.ec.BeanObjetos;
using System.Collections.Generic;
using System.Globalization;

namespace RegistroIncidentes
{
    public partial class RegistroEdicionIncidente : System.Web.UI.Page
    {
        private static sucesoBean suceso;
        private static UsuarioBean sesionUsuario;
        private string seleccionTierTres;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                sesionUsuario = (UsuarioBean)Session[GlobalSistema.usuarioSesionSistema];
                //lblUsuarioRegistra.Text = sesionUsuario.getNombres()+" " + sesionUsuario.getApellido();
                lblUsuarioRegistra.Text = sesionUsuario.getNumeroDocumento();
                lblUsuarioAsigna.Text = sesionUsuario.getNumeroDocumento();
                cargar_combo_box();
                cargar_datos_sesion();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DateTime dob = DateTime.Parse(Request.Form[this.txbxFechaInicio.UniqueID]);
        }

        public void cargar_combo_box() {
            /*this.lsBxTipoSistemas.Items.Clear();
            this.lsBxGrupoAsignado.Items.Clear();
            this.lsBxDatoSeleccionado.Items.Clear();
            this.lsBxSOP.Items.Clear();*/
            this.lsBxGrupoAsignado.Items.Clear();
            List<TipSistemasBean> lsSistemas = GlobalSistema.sistema.obtenerListaTipoSistemas("A");
            List<GrupoAsignadoBean> lsgrupo = GlobalSistema.sistema.obtenerListGrupo("A");
            List<datoSeleccionAreaBean> lsdato = GlobalSistema.sistema.obtenerListDatoArea("A");
            List<sopBean> lsSoap = GlobalSistema.sistema.obtenerListSop("A");
            List<EstadoIncidenteBean> lsEstado = GlobalSistema.sistema.obtenerListEstadoIncidente("A");
            List<TierUnoBean> lsTierUno = GlobalSistema.sistema.obtenerListTierUno("A");
            foreach(TipSistemasBean tipoSistemas in lsSistemas){
                ListItem item = new ListItem(tipoSistemas.nombreTipoSistemas,tipoSistemas.codigoTipoSistemas.ToString());
                lsBxTipoSistemas.Items.Add(item);
            }
            foreach (GrupoAsignadoBean grupo in lsgrupo)
            {
                ListItem item = new ListItem(grupo.nombregrupoAsignado,grupo.codigoGrupo.ToString());
                lsBxGrupoAsignado.Items.Add(item);
            }
            foreach (datoSeleccionAreaBean grupo in lsdato)
            {
                ListItem item = new ListItem(grupo.nombreTipoSeleccion, grupo.codigoDatoSeleccion.ToString());
                lsBxDatoSeleccionado.Items.Add(item);
            }
            foreach (sopBean grupo in lsSoap)
            {
                ListItem item = new ListItem(grupo.nombreSop, grupo.codigoSop.ToString());
                lsBxSOP.Items.Add(item);
            }
            foreach (EstadoIncidenteBean grupo in lsEstado)
            {
                ListItem item = new ListItem(grupo.nombreEstadoIncidente, grupo.codigoEstadoIncidente.ToString());
                lsBxEstadoIncidente.Items.Add(item);
            }
            foreach (TierUnoBean grupo in lsTierUno)
            {
                ListItem item = new ListItem(grupo.nombreCategorizacion, grupo.codigoTierUno.ToString());
                lsBxTierUno.Items.Add(item);
            }

        }

        public void cargar_tier_dos(object sender, EventArgs e)
        {
            cargar_datos_tier_dos(Int16.Parse(lsBxTierUno.SelectedValue));
        }

        public void cargar_datos_tier_dos(int tierUno){
            lsBxTierII.Items.Clear();
            lsBxTierII.Items.Add(new ListItem("Seleccione TIER II", "0"));
            List<TierDosBean> lsTierDos = GlobalSistema.sistema.obtenerListTierDos("A", tierUno);
            foreach (TierDosBean tier in lsTierDos) { 
                ListItem item = new ListItem(tier.nombreCategorizacion,tier.codigoTierDos.ToString());
                lsBxTierII.Items.Add(item);
            }     
        }

        public void cargar_tier_tres(object sender, EventArgs e)
        {
            cargar_datos_tier_tres(Int16.Parse(lsBxTierII.SelectedValue));
        }
        
        public void tier_tres_seleccionado(object sender, EventArgs e)
        {
            seleccionTierTres = lsBxTierIII.SelectedValue;
        }

        public void cargar_datos_tier_tres(int tierDos) {
            lsBxTierIII.Items.Clear();
            lsBxTierIII.Items.Add(new ListItem("Seleccione TIER III", "0"));
            List<TiertresBean> lsTierTres = GlobalSistema.sistema.obtenerListTierTres("A", tierDos);
            foreach (TiertresBean tier in lsTierTres)
            {
                ListItem item = new ListItem(tier.nombreCategorizacion, tier.codigoTierTres.ToString());
                lsBxTierIII.Items.Add(item);
            }
        }

        public void cargar_datos_sesion() {
            suceso = (sucesoBean)Session[GlobalSistema.incidenteSesion];
            if (suceso != null) {
                
                if (suceso.fechaCierre != null)
                {
                    this.txbxFechaFin.Text = suceso.fechaCierre.ToString("MM/dd/yyyy hh:mm");
                    if (txbxFechaFin.Text.Contains("1900"))
                    {
                        this.txbxFechaFin.Text = "";
                    }
                }
                if (suceso.registroIncidente != null)
                {
                    this.txbxFechaIncidente.Text = suceso.registroIncidente.ToString("MM/dd/yyyy hh:mm");
                    if (txbxFechaIncidente.Text.Contains("1900"))
                    {
                        this.txbxFechaIncidente.Text = "";
                    }
                }
                if (suceso.primerInteraccion != null)
                {
                    this.txbxFechaInicio.Text = suceso.primerInteraccion.ToString("MM/dd/yyyy hh:mm");
                    if (this.txbxFechaInicio.Text.Contains("1900"))
                    {
                        this.txbxFechaInicio.Text = "";
                    }
                }
                UsuarioBean usuarioAtiende = GlobalSistema.sistema.obtenerDatosUsuario(suceso.codigo_usuario.ToString(),false);
                UsuarioBean usuarioAsigna = GlobalSistema.sistema.obtenerDatosUsuario(suceso.codigo_usuario_reporta.ToString(), false);
                this.lblUsuarioRegistra.Text = usuarioAtiende.getNumeroDocumento();
                this.lblUsuarioAsigna.Text = usuarioAsigna.getNumeroDocumento();
                this.txbxNumIncidente.Text = suceso.codigoIncidente;
                this.txbxObservacion.Text = suceso.descripcionincidente;
                this.txbxPais.Text = suceso.pais;
                this.lsBxDatoSeleccionado.SelectedValue = suceso.codigoDato.ToString();
                this.lsBxEstadoIncidente.SelectedValue = suceso.estadoIncidente.ToString();
                this.lsBxGrupoAsignado.SelectedValue = suceso.codigoGrupoAsignado.ToString();
                this.lsBxSOP.SelectedValue = suceso.codigoSop.ToString();
                this.lsBxTierUno.SelectedValue = suceso.codigoTierUno.ToString();
                cargar_datos_tier_dos(suceso.codigoTierUno);
                this.lsBxTierII.SelectedValue = suceso.codigoTierDos.ToString();
                cargar_datos_tier_tres(suceso.codigoTierDos);
                this.lsBxTierIII.SelectedValue = suceso.codigoTierTres.ToString();
                this.lsBxTipoSistemas.SelectedValue = suceso.codigoTipoSistemas.ToString();
                this.chbxRebido.Checked = suceso.recibidoSistemas;
                this.chbxEstadistica.Checked = suceso.estadistica;
                this.chBxEnviado.Checked = suceso.enviadoSistmas;
                chBox_analisis();
            }      
        }

        public void chBox_analisis() {
            if (this.chBxEnviado.Checked)
            {
                this.lsBxTierUno.Enabled = false;
                this.lsBxTierUno.SelectedValue = "0";
                this.lsBxTierII.Enabled = false;
                this.lsBxTierII.SelectedValue = "0";
                this.lsBxTierIII.Enabled = false;
                this.lsBxTierIII.SelectedValue = "0";
                this.validadorTierI.Enabled = false;
                this.validadorTierII.Enabled = false;
                this.validadorTierIII.Enabled = false;
            }
            else {
                this.lsBxTierUno.Enabled = true;
                this.lsBxTierII.Enabled = true;
                this.lsBxTierIII.Enabled = true;
                this.validadorTierI.Enabled = true;
                this.validadorTierII.Enabled = true;
                this.validadorTierIII.Enabled = true;
            }

            if (this.chbxRebido.Checked)
            {
                this.lsBxTipoSistemas.Enabled = true;
                this.lsBxTipoSistemas.SelectedValue = "0";
            }
            else
            {
                this.lsBxTipoSistemas.Enabled = false;
            }
        }

        public void onclick_datos_sistema(object sender, EventArgs e)
        {
            chBox_analisis();
        }

        public void guardar_suceso_sistema(object sender, EventArgs e)
        {
            if (suceso == null) {
                suceso = new sucesoBean();
            }
            //Buscar  si código de suceso fue ingresado anteriormente
            UsuarioBean usrTmp = new UsuarioBean();
            usrTmp.setCodigoUsuario(0);
            List<SucesoReporteBean> lsSuceso = GlobalSistema.sistema.obtenerIncidentesReportePorUsuario(txbxNumIncidente.Text,usrTmp);
            if (lsSuceso.Count > 0 && sesionUsuario.getNivelAcceso() != 0)
            {
                suceso.codigo_usuario = GlobalSistema.sistema.obtenerDatosUsuario(lblUsuarioRegistra.Text, true).getCodigoUsuario();
                if (!sesionUsuario.getNumeroDocumento().Equals(lsSuceso[0].documento))
                {
                    lblMensajeError.Text = "Suceso registrado, con usuario " + lsSuceso[0].codigo_usuario + " solo este usuario puede editar";
                    return;
                }
            }
            else {
                suceso.codigo_usuario = sesionUsuario.getCodigoUsuario();
            }
            //DateTime fechaRegistro = Convert.ToDateTime(this.txbxFechaIncidente.Text);
            DateTime fechaRegistro = DateTime.ParseExact(this.txbxFechaIncidente.Text, "MM/dd/yyyy HH:mm", CultureInfo.CreateSpecificCulture("en-US"));
            suceso.registroIncidente = fechaRegistro;
            //DateTime fechaPrimeraInteraccion = Convert.ToDateTime("1900-01-01");
            DateTime fechaPrimeraInteraccion = DateTime.ParseExact("01/01/1900", "MM/dd/yyyy", CultureInfo.CreateSpecificCulture("en-US"));
            //DateTime fechaFin = Convert.ToDateTime("1900-01-01");
            DateTime fechaFin = DateTime.ParseExact("01/01/1900", "MM/dd/yyyy", CultureInfo.CreateSpecificCulture("en-US"));

            if (!string.IsNullOrEmpty(this.txbxFechaInicio.Text))
            {
                //fechaPrimeraInteraccion = Convert.ToDateTime(this.txbxFechaInicio.Text);
                fechaPrimeraInteraccion = DateTime.ParseExact(this.txbxFechaInicio.Text, "MM/dd/yyyy HH:mm", CultureInfo.CreateSpecificCulture("en-US"));
                if (DateTime.Compare(fechaPrimeraInteraccion, fechaRegistro) < 0)
                {
                    this.lblMensajeError.Text = "Fecha de Primera Interacción debe ser mayor que la fecha registro";
                    return;
                }
                suceso.primerInteraccion = fechaPrimeraInteraccion;
            }
            else {
                suceso.primerInteraccion = fechaPrimeraInteraccion;
            }
            if (!string.IsNullOrEmpty(this.txbxFechaFin.Text))
            {
                //fechaFin = Convert.ToDateTime(this.txbxFechaFin.Text);
                fechaFin = DateTime.ParseExact(this.txbxFechaFin.Text, "MM/dd/yyyy HH:mm", CultureInfo.CreateSpecificCulture("en-US"));
                if (DateTime.Compare(fechaPrimeraInteraccion, Convert.ToDateTime("1900-01-01")) == 0)
                {
                    this.lblMensajeError.Text = "Seleccione una fecha de primera iteracción para cerrar";
                    return;
                }
                if (DateTime.Compare(fechaPrimeraInteraccion, fechaFin) > 0)
                {
                    this.lblMensajeError.Text = "Fecha de cierre debe ser mayor que la Primera Interacción";
                    return;
                }
                suceso.fechaCierre = fechaFin;
            }
            else {
                suceso.fechaCierre = fechaFin;
            }
            suceso.codigoIncidente = this.txbxNumIncidente.Text;
            suceso.descripcionincidente = this.txbxObservacion.Text;
            suceso.pais = this.txbxPais.Text;
            suceso.codigoDato = Convert.ToInt16(this.lsBxDatoSeleccionado.SelectedValue);
            suceso.estadoIncidente = Convert.ToInt16(this.lsBxEstadoIncidente.SelectedValue);
            suceso.codigoGrupoAsignado = Convert.ToInt16(this.lsBxGrupoAsignado.SelectedValue);
            suceso.codigoSop = Convert.ToInt16(this.lsBxSOP.SelectedValue);
            suceso.codigoTierUno = Convert.ToInt16(this.lsBxTierUno.SelectedValue);
            suceso.codigoTierDos = Convert.ToInt16(this.lsBxTierII.SelectedValue);
            suceso.codigoTierTres = Convert.ToInt16(this.lsBxTierIII.SelectedValue);
            suceso.codigoTipoSistemas = Convert.ToInt16(this.lsBxTipoSistemas.SelectedValue);
            suceso.codigo_usuario_reporta = GlobalSistema.sistema.obtenerDatosUsuario(lblUsuarioAsigna.Text, true).getCodigoUsuario();
            suceso.enviadoSistmas = chBxEnviado.Checked;
            suceso.recibidoSistemas = chbxRebido.Checked;
            suceso.estadistica = chbxEstadistica.Checked;
            lblMensajeError.Text = GlobalSistema.sistema.insertarActualizarSucesoUsuario(suceso);
        }

        public void limpiar(object sender, EventArgs e) {
            this.txbxFechaFin.Text = "";
            this.txbxFechaIncidente.Text = "";
            this.txbxFechaInicio.Text = "";
            this.txbxNumIncidente.Text = "";
            this.txbxObservacion.Text = "";
            this.txbxPais.Text = "";
            this.lsBxDatoSeleccionado.SelectedValue = "0";
            this.lsBxEstadoIncidente.SelectedValue = "0";
            this.lsBxGrupoAsignado.SelectedValue = "1";
            this.lsBxSOP.SelectedValue = "0";
            this.lsBxTierII.SelectedValue = "0";
            this.lsBxTierIII.SelectedValue = "0";
            this.lsBxTierUno.SelectedValue = "0";
            this.lsBxTipoSistemas.SelectedValue = "0";
            this.lblMensajeError.Text = "";
        }
    }
}
