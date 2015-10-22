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
    public partial class EdicionIngresoIncidente : System.Web.UI.Page
    {
        private UsuarioBean usuarioSesion;
        private static List<SucesoReporteBean> lsSucesos;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            usuarioSesion = (UsuarioBean)Session[GlobalSistema.usuarioSesionSistema];
            GridViewIncidente.AutoGenerateColumns = false;
            if (!IsPostBack)
            {
                cargar_datos_sucesos_usuario();
                this.txbxFechaInicio.Text = System.DateTime.Now.ToString(GlobalSistema.formatoFecha.Substring(0,9)) + " 00:00:00 AM";
                this.txbxFechaFin.Text = System.DateTime.Now.ToString(GlobalSistema.formatoFecha, CultureInfo.InvariantCulture);
            }
            ClientScript.RegisterStartupScript(GetType(), "", "mostrarDateTimePickerTxbxFin();mostrarDateTimePickerTxbxInicio();", true);
            
        }

        public void btn_busqueda_datosBy(object sender, EventArgs e) {
            string rbSeleccionado = this.rbgSeleccion.Text;
            if (rbSeleccionado.Equals("codigo"))
            {
                // Busqueda por codigo
                if (string.IsNullOrEmpty(this.txbxCodigoIncidente.Text)) {
                    lblMensajeError.Text = "Ingrese un código de suceso";
                    return;
                }
                lsSucesos = GlobalSistema.sistema.obtenerIncidentesReportePorUsuario(txbxCodigoIncidente.Text, usuarioSesion);
            }
            else if (rbSeleccionado.Equals("fechas"))
            { 
                // Busqueda por rango de fechas
                DateTime inicio;
                DateTime fin;
                try
                {
                    if (this.txbxFechaInicio.Text.EndsWith("AM") || this.txbxFechaInicio.Text.EndsWith("PM"))
                    {
                        inicio = DateTime.ParseExact(this.txbxFechaInicio.Text, GlobalSistema.formatoFecha, CultureInfo.CreateSpecificCulture("en-US"));
                    }
                    else
                    {
                        inicio = DateTime.ParseExact(this.txbxFechaInicio.Text, "MM/dd/yyyy HH:mm", CultureInfo.CreateSpecificCulture("en-US"));
                    }
                    //fin = Convert.ToDateTime(this.txbxFechaFin.Text);
                    if (this.txbxFechaFin.Text.EndsWith("AM") || this.txbxFechaFin.Text.EndsWith("PM"))
                    {
                        fin = DateTime.ParseExact(this.txbxFechaFin.Text, GlobalSistema.formatoFecha, CultureInfo.CreateSpecificCulture("en-US"));

                    }
                    else
                    {
                        fin = DateTime.ParseExact(this.txbxFechaFin.Text, "MM/dd/yyyy HH:mm", CultureInfo.CreateSpecificCulture("en-US"));
                    }
                }
                catch (FormatException ex) {
                    lblMensajeError.Text = ex.Message;
                    lblMensajeError.Text = "Ingrese el rango de fecha a buscar";
                    return;
                }
                if (DateTime.Compare(inicio, fin) > 0) {
                    this.lblMensajeError.Text = "Fecha inicial debe ser mayor que fecha final";
                    return;
                }
                lsSucesos = GlobalSistema.sistema.obtenerReporteFechaUsuario(inicio,fin,usuarioSesion,false);
            }
            if (lsSucesos.Count == 0)
            {
                lblMensajeError.Text = "Registro no encontrado";
            }
            else {
                lblMensajeError.Text = "";
            }
            GridViewIncidente.DataSource = lsSucesos;
            GridViewIncidente.DataBind();
        }

        public void habilitar_datos_chbx(object sender, EventArgs e)
        {
            string rbSeleccionado = this.rbgSeleccion.Text;
            if (rbSeleccionado.Equals("codigo")) {
                this.lbFechaInicio.Visible = false;
                this.txbxFechaInicio.Visible = false;
                this.lblFechaFin.Visible = false;
                this.txbxFechaFin.Visible = false;
                this.imgIni.Visible = false;
                this.imgFin.Visible = false;
                this.lblCodigo.Visible = true;
                this.txbxCodigoIncidente.Visible = true;
                
            }
            else if (rbSeleccionado.Equals("fechas"))
            {
                this.lbFechaInicio.Visible = true;
                this.txbxFechaInicio.Visible = true;
                this.lblFechaFin.Visible = true;
                this.txbxFechaFin.Visible = true;
                this.imgFin.Visible = true;
                this.imgIni.Visible = true;
                this.lblCodigo.Visible = false;
                this.txbxCodigoIncidente.Visible = false;
            }
        }

        public void nuevo_registro_incidente(object sender,EventArgs e){
            Session[GlobalSistema.incidenteSesion] = null;
            Response.Redirect("RegistroEdicionIncidente.aspx");
        }

        public void cargar_datos_sucesos_usuario() {
            if (!IsPostBack)
            {
                lsSucesos = GlobalSistema.sistema.obtenerIncidentesAsignadosPorUsuario("%", usuarioSesion);
            }
            GridViewIncidente.DataSource = lsSucesos;
            GridViewIncidente.DataBind();
        }


        protected void edicion_fila_suceso(object sender, GridViewCommandEventArgs e)
        {
            string cmdName = e.CommandName;
            int fila =Convert.ToInt16(e.CommandArgument.ToString());
            if (cmdName.Equals("Edicion"))
            {
                string incidente = GridViewIncidente.Rows[fila].Cells[0].Text;
                List<sucesoBean> suceso = GlobalSistema.sistema.obtenerIncidentesPorUsuario(incidente, usuarioSesion);
                if (suceso.Count == 1)
                {
                    Session[GlobalSistema.incidenteSesion] = suceso[0];
                    Response.Redirect("RegistroEdicionIncidente.aspx");
                }
            }
            
        }

        protected void GridViewIncidente_PageIndexChanged_(object sender, EventArgs e)
        {
           
        }

        protected void GridViewIncidente_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewIncidente.PageIndex = e.NewPageIndex;
            cargar_datos_sucesos_usuario();
        }

                
    }
}
