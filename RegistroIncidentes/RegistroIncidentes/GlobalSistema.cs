using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LibreriaControlador.com.ec.Utilitario;
using LibreriaControlador.com.ec.BeanObjetos;
using RegistroIncidentes.LogicaNegocio;

namespace RegistroIncidentes
{
    public static class GlobalSistema
    {
        public static ControlLogicaSistema sistema = new ControlLogicaSistema();
        public static Seguridad seguridad = new Seguridad();
        public static string usuarioSesionSistema = "usuarioSesion";
        public static string incidenteSesion = "incidenteSesion";
        
    }
}
