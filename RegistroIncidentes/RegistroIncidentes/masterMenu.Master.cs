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

namespace RegistroIncidentes
{
    public partial class masterMenu : System.Web.UI.MasterPage
    {
        private UsuarioBean sesionU;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                sesionU = (UsuarioBean)Session[GlobalSistema.usuarioSesionSistema];
                try
                {
                    if (string.IsNullOrEmpty(sesionU.getApellido()))
                    {
                        Response.Redirect("Defult.aspx");
                    }
                    lblUsuario.Text = "Conectado : " + sesionU.getApellido() + " " + sesionU.getNombres();
                }
                catch (NullReferenceException ex)
                {
                    string ms = ex.Message;
                    Session.RemoveAll();
                    Response.Redirect("Default.aspx");
                }
            }
        }
        
        public void cerrar_sesion(Object sender,EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("Default.aspx");
        }

    }
}
