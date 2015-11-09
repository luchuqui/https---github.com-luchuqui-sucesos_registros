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
using System.Text;

namespace RegistroIncidentes
{
    public partial class SubirArchivo : System.Web.UI.Page
    {
        private int regTotales;
        private int regEncontrados;
        private int regNoEncontrados;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void subir_archivo(object sender, EventArgs e)
        {
            bool archivoPermitido = false;
            string extension = string.Empty;
            string[] extensionesPermitidas = { ".xls", ".xlsx" };
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
                limpiar();
                this.lblMensaje.Text = "Seleccione el archivo para procesar";
                return;
            }
                
                if (archivoPermitido)
            {
                string FileName = Path.GetFileName(archivoUp.PostedFile.FileName);
                string Extension = Path.GetExtension(archivoUp.PostedFile.FileName);
                string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
                string FilePath = Server.MapPath(FolderPath + FileName);
                lblMensaje.Text = FilePath;
                archivoUp.SaveAs(FilePath);
                Import_To_Grid(FilePath, Extension);
            }
            else { 
            // extension no
                limpiar();
                this.lblMensaje.Text = "Archivo no permitido, solo se permiten archivos .xls";
            }
        }

        public void limpiar() {
            Session["dtExcel"] = new DataTable();
            regEncontrados = 0;
            regNoEncontrados = 0;
            regTotales = 0;
        }

        private void Import_To_Grid(string FilePath, string Extension)
        {
            string conStr = "";
            try { 
            switch (Extension)
            {
                case ".xls": //Excel 97-03
                    conStr = ConfigurationManager.ConnectionStrings["Excel03ConString"]
                             .ConnectionString;
                    break;
                case ".xlsx": //Excel 07
                    conStr = ConfigurationManager.ConnectionStrings["Excel07ConString"]
                              .ConnectionString;
                    break;
            }
            conStr = String.Format(conStr, FilePath, "No");
            OleDbConnection connExcel = new OleDbConnection(conStr);
            OleDbCommand cmdExcel = new OleDbCommand();
            OleDbDataAdapter oda = new OleDbDataAdapter();
            DataTable dt = new DataTable();
            cmdExcel.Connection = connExcel;
            //Get the name of First Sheet
            connExcel.Open();
            DataTable dtExcelSchema;
            dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            string SheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
            connExcel.Close();
            //Read Data from First Sheet
            connExcel.Open();
            cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
            oda.SelectCommand = cmdExcel;
            oda.Fill(dt);
            connExcel.Close();
            //Bind Data to GridView
            int ultima = dt.Columns.Count+1;
            DataColumn workCol = dt.Columns.Add("F"+ultima, typeof(string));
            workCol.AllowDBNull = true;
            workCol.Unique = false;
            ultima--;
            regTotales = dt.Rows.Count;
            for (int i = 0; i < dt.Rows.Count; i++) {
               string valor = dt.Rows[i][0].ToString();
               string val = GlobalSistema.sistema.validarRegistroExistente(valor);
               if (val.StartsWith("No"))
               {
                   regNoEncontrados++;
               }
               else {
                   regEncontrados++;
               }
               dt.Rows[i][ultima] = val;
            }
            DatosExcel.Caption = Path.GetFileName(FilePath);
            DatosExcel.DataSource = dt;
            DatosExcel.DataBind();
            Session["dtExcel"] = dt;
            lblNoEncontrados.Text = regNoEncontrados.ToString();
            lblNumEncontrados.Text = regEncontrados.ToString();
            lblTotales.Text = regTotales.ToString();
            lblMensaje.Text = "Proceso completado con exito";
            }
            catch (Exception e) {
                lblMensaje.Text = e.Message;
            }
        }

        public void btn_exportar_datos(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            Page page = new Page();
            HtmlForm form = new HtmlForm();



            // Deshabilitar la validación de eventos, sólo asp.net 2
            page.EnableEventValidation = false;

            // Realiza las inicializaciones de la instancia de la clase Page que requieran los diseñadores RAD.
            page.DesignerInitialize();
            DataTable lsdatos = (DataTable)Session["dtExcel"];
            page.Controls.Add(form);
            DatosExcel.AllowPaging = false;
            form.Controls.Add(DatosExcel);
            DatosExcel.DataSource = lsdatos;
            DatosExcel.DataBind();
            page.RenderControl(htw);

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=validar_" + DatosExcel.Caption);
            Response.Charset = "UTF-8";
            Response.ContentEncoding = Encoding.Default;
            Response.Write(sb.ToString());
            Response.End();
        }

        protected void PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string FolderPath = ConfigurationManager.AppSettings["FolderPath"];
            string FileName = DatosExcel.Caption;
            string Extension = Path.GetExtension(FileName);
            string FilePath = Server.MapPath(FolderPath + FileName);
            //Import_To_Grid(FilePath, Extension);
            DatosExcel.DataSource = (DataTable)Session["dtExcel"];
            DatosExcel.PageIndex = e.NewPageIndex;
            DatosExcel.DataBind();
        }

    }
}
