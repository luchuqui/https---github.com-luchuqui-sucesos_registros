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
using LibreriaControlador.com.ec.Utilitario;

namespace RegistroIncidentes
{
    public partial class RegistroUsuario : System.Web.UI.Page
    {
        private UsuarioBean registroUsr;
        protected void Page_Load(object sender, EventArgs e)
        {
            registroUsr = new UsuarioBean();
        }

        public void registrar_usuario_nuevo(object sender, EventArgs e)
        {
            if (!this.txbxConsenia.Text.Equals(this.txbxConfirmacion.Text)) {
                this.lblMensaje.Text = "Contraseña debe ser iguales para crear el usuario";
                return;
            }
            registroUsr.setNombres(this.txbxNombre.Text);
            registroUsr.setApellido(this.txbxApellido.Text);
            registroUsr.setCorreo(this.txbxEmail.Text);
            registroUsr.setEstadoUsuario("I");
            registroUsr.setFechaCreacion(System.DateTime.Now);
            registroUsr.setNivelAcceso(1);
            registroUsr.setNumeroDocumento(this.txbxNumeroDocumento.Text);
            registroUsr.setPassword(GlobalSistema.seguridad.encriptar_informacion(this.txbxConsenia.Text));
            this.lblMensaje.Text = GlobalSistema.sistema.insertar_usuario_sistema(registroUsr, true);
            
        }
    }
}
