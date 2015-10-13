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
    public partial class AsignacionTicket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                cargarUsuarioSistemas();
                UsuarioBean usr = (UsuarioBean)Session[GlobalSistema.usuarioSesionSistema];
                lblUsuarioReporta.Text = usr.getNumeroDocumento();
            }
        }

        public void cargarUsuarioSistemas() {
            List<UsuarioBean> lsUsuarios = GlobalSistema.sistema.obtenerDatosUsuarioActivos("%", true);
            lsBxUsuarios.Items.Clear();
            lsBxUsuarios.Items.Add(new ListItem("Seleccione Usuario", "0"));
            foreach (UsuarioBean usr in lsUsuarios) {
                ListItem item = new ListItem(usr.getNumeroDocumento()+" - " +usr.getNombres() + " "+ usr.getApellido(), usr.getCodigoUsuario().ToString());
                lsBxUsuarios.Items.Add(item);
            }
        }

        public void guardarSuceso(object sender,EventArgs e) {
            sucesoBean suceso = new sucesoBean();
            suceso.codigo_usuario_reporta = GlobalSistema.sistema.obtenerDatosUsuario(lblUsuarioReporta.Text,true).getCodigoUsuario();
            suceso.codigo_usuario = Int16.Parse(lsBxUsuarios.SelectedValue);
            suceso.codigoIncidente = txbxCodigoIncidente.Text;
            UsuarioBean usrTmp = new UsuarioBean();
            usrTmp.setCodigoUsuario(0);
            List<SucesoReporteBean> lsSuceso = GlobalSistema.sistema.obtenerIncidentesReportePorUsuario(txbxCodigoIncidente.Text, usrTmp);
            if (lsSuceso.Count > 0)
            {
                lblMensaje.Text = "Suceso ya registrado ";
                return;
            }
            DateTime fechaRegistro = DateTime.ParseExact("01/01/1900", "MM/dd/yyyy", CultureInfo.CreateSpecificCulture("en-US"));
            suceso.registroIncidente = fechaRegistro;
            suceso.fechaCierre = fechaRegistro;
            suceso.primerInteraccion = fechaRegistro;
            suceso.descripcionincidente = txbxDescripcion.Text;
            suceso.pais = "";
            suceso.codigoGrupoAsignado = 1;
            lblMensaje.Text = GlobalSistema.sistema.insertarActualizarSucesoUsuario(suceso);
        }

        public void limpiar(object sender, EventArgs e)
        {
            this.txbxDescripcion.Text = "";
            this.txbxCodigoIncidente.Text = "";
        }
    }
}
