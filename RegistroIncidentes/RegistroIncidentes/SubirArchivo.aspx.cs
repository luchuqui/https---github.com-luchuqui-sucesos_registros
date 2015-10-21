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
using System.Data.OleDb;
using System.IO;

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
                OleDbConnectionStringBuilder cb = new OleDbConnectionStringBuilder();
                cb.DataSource = archivoUp.FileName;
                archivoUp.PostedFile.SaveAs("c:\\temp\\"+archivoUp.FileName);
                string rutaExcel = "c:\\temp\\" + archivoUp.FileName;
                if (Path.GetExtension(rutaExcel).ToUpper() == ".XLS")
                {
                    cb.Provider = "Microsoft.Jet.OLEDB.4.0";
                    cb.Add("Extended Properties", "Excel 8.0;HDR=YES;IMEX=0;");
                }
                else if (Path.GetExtension(rutaExcel).ToUpper() == ".XLSX")
                {
                    cb.Provider = "Microsoft.ACE.OLEDB.12.0";
                    cb.Add("Extended Properties", "Excel 12.0 Xml;HDR=YES;IMEX=0;");
                }


                DataTable dt = new DataTable("Datos");

                using (OleDbConnection conn = new OleDbConnection(cb.ConnectionString))
                {

                    //Abrimos la conexión
                    conn.Open();
                    using (OleDbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "SELECT * FROM [Hoja1$]";
                        //Guardamos los datos en el DataTable
                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        da.Fill(dt);
                    }
                    //Cerramos la conexión
                    conn.Close();

                }
            }
            else { 
            // extension no
                this.lblMensaje.Text = "Archivo no permitido, solo se permiten archivos .xls";
            }
        }
    }
}
