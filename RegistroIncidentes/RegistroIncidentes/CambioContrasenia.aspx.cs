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
    public partial class CambioContrasenia : System.Web.UI.Page
    {
        private static UsuarioBean usuarioSesion;

        protected void Page_Load(object sender, EventArgs e)
        {
            usuarioSesion = (UsuarioBean)Session[GlobalSistema.usuarioSesionSistema];
        }

        public void btn_cambiarContraseña(object sender, EventArgs e) {
            string passCifrado = GlobalSistema.seguridad.encriptar_informacion(this.txbxActual.Text);
            if (passCifrado.Equals(usuarioSesion.getPassword()))
            {
                if (txbxNueva.Text.Equals(txbxConfirmar.Text))
                {
                    usuarioSesion.setPassword(GlobalSistema.seguridad.encriptar_informacion(this.txbxNueva.Text));
                    usuarioSesion = GlobalSistema.sistema.actualizar_usuario_sistema(usuarioSesion);
                    if (string.IsNullOrEmpty(usuarioSesion.getApellido()))
                    {
                        lblMensaje.Text = usuarioSesion.getPassword();
                    }
                    else
                    {
                        Session[GlobalSistema.usuarioSesionSistema] = usuarioSesion;
                        lblMensaje.Text = "cambio de contraseña exitoso";
                    }
                }
                else {
                    lblMensaje.Text = "Contraseña nueva y confirmación no corresponden";
                }
            }
            else {
                lblMensaje.Text = "No es la contraseña actual";
            }
        }
    }
}
