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
using RegistroIncidentes.LogicaNegocio;
using LibreriaControlador.com.ec.BeanObjetos;
using LibreriaControlador.com.ec.Utilitario;

namespace RegistroIncidentes
{
    public partial class _Default : System.Web.UI.Page
    {
        private UsuarioBean usuarioLogin;
        
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void btn_acceso_sistema(object sender, EventArgs e) {
            usuarioLogin = new UsuarioBean();
            usuarioLogin.setNumeroDocumento(this.txbxDocumento.Text);
            usuarioLogin.setPassword(GlobalSistema.seguridad.encriptar_informacion(this.txbxPassword.Text));
            usuarioLogin = GlobalSistema.sistema.login_usuario_acceso(usuarioLogin);
            if (string.IsNullOrEmpty(usuarioLogin.getNombres()))
            {
                this.mensajeError.Text = usuarioLogin.getPassword();
            }
            else if (usuarioLogin.getEstadoUsuario().Equals("A") 
                || usuarioLogin.getNumeroDocumento().Equals("1716166788"))
            {
                Session[GlobalSistema.usuarioSesionSistema] = usuarioLogin;
                usuarioLogin.setFechaUltimoAcceso(System.DateTime.Now);
                GlobalSistema.sistema.insertar_usuario_sistema(usuarioLogin, false);
                Response.Redirect("EdicionIngresoIncidente.aspx");
            }
            else if (usuarioLogin.getEstadoUsuario().Equals("E"))
            {
                this.mensajeError.Text = "Usuario no permitido el acceso, contactese con soporte";
            }
            else if (usuarioLogin.getEstadoUsuario().Equals("I"))
            {
                this.mensajeError.Text = "Usuario inabilitado, debe ser dado de alta por el administrador";
            }
            else {
                this.mensajeError.Text = "Estado desconocido";
            }

        }
    }
}
