using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibreriaControlador.com.ec.BeanObjetos;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Globalization;

namespace RegistroIncidentes
{
    public partial class ReporteSucesos : System.Web.UI.Page
    {
        private static UsuarioBean usuarioSesion;
        private static List<SucesoReporteBean> lsSucesosReg;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                usuarioSesion = (UsuarioBean)Session[GlobalSistema.usuarioSesionSistema];
                this.txbxFechaInicio.Text = System.DateTime.Now.ToString("MM/dd/yyyy ") + "00:00";
                this.txbxFechaFin.Text = System.DateTime.Now.ToString("MM/dd/yyyy HH:mm");
            }
            ClientScript.RegisterStartupScript(GetType(), "", "mostrarDateTimePickerTxbxFin();mostrarDateTimePickerTxbxInicio();", true);
        }

        public void btn_exportar_datos(object sender, EventArgs e)
        {
            if (GridViewIncidente.Rows.Count == 0) {
                lblMensajeError.Text = "Debe de realizar una busqueda";
                return;
            }
            string inicio;
            string fin;
            string nombre = string.Empty;
            try
            {
                inicio = Convert.ToDateTime(this.txbxFechaInicio.Text).ToString("MMddyyyy");
                fin = Convert.ToDateTime(this.txbxFechaFin.Text).ToString("MMddyyyy");
                nombre = inicio + "_" + fin;
            }
            catch (FormatException ex) {
                fin = ex.Message;
                inicio = txbxCodigoIncidente.Text;
                nombre = inicio;
                //lblMensajeError.Text = "Formato de error "+ex.Message;
                
            }
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            Page page = new Page();
            HtmlForm form = new HtmlForm();

            

            // Deshabilitar la validación de eventos, sólo asp.net 2
            page.EnableEventValidation = false;

            // Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD.
            page.DesignerInitialize();

            page.Controls.Add(form);
            GridViewIncidente.AllowPaging = false;
            form.Controls.Add(GridViewIncidente);
            GridViewIncidente.DataSource = lsSucesosReg;
            GridViewIncidente.DataBind();
            page.RenderControl(htw);

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=reporte_" + nombre + ".xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.ASCII;
            Response.Write(sb.ToString());
            Response.End();
        }

        

        public void btn_busqueda_datos(object sender, EventArgs e)
        {
            string rbSeleccionado = this.rbgSeleccion.Text;
            if (rbSeleccionado.Equals("codigo"))
            {
                // Busqueda por codigo
                if (string.IsNullOrEmpty(this.txbxCodigoIncidente.Text))
                {
                    lblMensajeError.Text = "Ingrese un código de suceso";
                    return;
                }
                lsSucesosReg = GlobalSistema.sistema.obtenerIncidentesReportePorUsuario(txbxCodigoIncidente.Text, usuarioSesion);
            }
            else if (rbSeleccionado.Equals("fechas"))
            {
            // Busqueda por rango de fechas
            DateTime inicio;
            DateTime fin;
            try
            {
                //inicio = Convert.ToDateTime(this.txbxFechaInicio.Text);
                inicio = DateTime.ParseExact(this.txbxFechaInicio.Text, "MM/dd/yyyy HH:mm", CultureInfo.CreateSpecificCulture("en-US"));
                //fin = Convert.ToDateTime(this.txbxFechaFin.Text);
                fin = DateTime.ParseExact(this.txbxFechaFin.Text, "MM/dd/yyyy HH:mm", CultureInfo.CreateSpecificCulture("en-US"));
            }
            catch (FormatException ex)
            {
                lblMensajeError.Text = ex.Message;
                lblMensajeError.Text = "Ingrese el rango de fecha a buscar";
                return;
            }
            if (DateTime.Compare(inicio, fin) > 0)
            {
                this.lblMensajeError.Text = "Fecha inicial debe ser mayor que fecha final";
                return;
            }
            lsSucesosReg = GlobalSistema.sistema.obtenerReporteFechaUsuario(inicio, fin, usuarioSesion,false);

            }
            if (lsSucesosReg.Count == 0)
            {
                lblMensajeError.Text = "Registro no encontrado";
            }
            else
            {
                lblMensajeError.Text = "";
            }
            GridViewIncidente.DataSource = lsSucesosReg;
            GridViewIncidente.DataBind();
        }

        public void habilitar_datos_chbx(object sender, EventArgs e)
        {
            string rbSeleccionado = this.rbgSeleccion.Text;
            if (rbSeleccionado.Equals("codigo"))
            {
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

        protected void GridViewIncidente_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewIncidente.PageIndex = e.NewPageIndex;
            GridViewIncidente.DataSource = lsSucesosReg;
            GridViewIncidente.DataBind();
        }


    }
}
