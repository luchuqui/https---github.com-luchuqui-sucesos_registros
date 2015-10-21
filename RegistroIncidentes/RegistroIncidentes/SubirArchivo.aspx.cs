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

namespace RegistroIncidentes
{
    public partial class SubirArchivo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void subir_archivo(object sender, EventArgs e)
        {
            bool archivoPermitido = false;
            string extension = string.Empty;
            string [] extensionesPermitidas = { ".xls"};
            if (archivoUp.HasFile)
            {
                extension = System.IO.Path.GetExtension(archivoUp.FileName).ToLower();
                foreach (string exten in extensionesPermitidas)
                {
                    if (exten.Equals(extension))
                    {
                        archivoPermitido = true;
                        break;
                    }
                }
            }
            else {
                this.lblMensaje.Text = "Seleccione el archivo para procesar";
                return;
            }
                
                if (archivoPermitido)
            {
                archivoUp.SaveAs("c:\\temp\\"+archivoUp.FileName);
            }
            else { 
            // extension no
                this.lblMensaje.Text = "Archivo no permitido, solo se permiten archivos .xls";
            }
        }
    }
}
