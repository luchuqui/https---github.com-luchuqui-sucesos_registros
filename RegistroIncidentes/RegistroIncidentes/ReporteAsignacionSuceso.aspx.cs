﻿using System;
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
using System.Globalization;
using System.Collections.Generic;
using LibreriaControlador.com.ec.BeanObjetos;
using System.Text;
using System.IO;

namespace RegistroIncidentes
{
    public partial class ReporteAsignacionSuceso : System.Web.UI.Page
    {
        private static List<SucesoReporteBean> lsSucesosReg;
        private static UsuarioBean usuarioSesion;
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
        public void btn_busqueda_datos(object sender, EventArgs e)
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
            lsSucesosReg = GlobalSistema.sistema.obtenerReporteFechaUsuario(inicio, fin, usuarioSesion,true);
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

        public void btn_exportar_datos(object sender, EventArgs e)
        {
            if (GridViewIncidente.Rows.Count == 0)
            {
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
            catch (FormatException ex)
            {
                fin = ex.Message;
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
            Response.AddHeader("Content-Disposition", "attachment;filename=report_asigna_" + nombre + ".xls");
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.ASCII;
            Response.Write(sb.ToString());
            Response.End();
        }

        protected void GridViewIncidente_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewIncidente.PageIndex = e.NewPageIndex;
            GridViewIncidente.DataSource = lsSucesosReg;
            GridViewIncidente.DataBind();
        }
    }
}