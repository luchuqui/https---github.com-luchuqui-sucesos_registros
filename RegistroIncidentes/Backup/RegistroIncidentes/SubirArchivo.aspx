<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile = "~/masterMenu.Master" CodeBehind="SubirArchivo.aspx.cs" Inherits="RegistroIncidentes.SubirArchivo" %>
<asp:Content ID = "contenido" runat = "server" ContentPlaceHolderID = "contenedorPrincipal">
    <table width = "100%">
    <tr>
        <th><label>Dirección Archivo :</label></th>
        <td><asp:FileUpload runat="server" ID ="archivoUp"/></td>
    </tr>
    <tr>
    <td><asp:Label ID = "lblMensaje" runat = "server" ForeColor="#CC0000"></asp:Label></td>
    </tr>
    <tr><td align = "center" colspan = "1"><asp:Button ID="btnProcesar" runat = "server" OnClick = "subir_archivo" Text="Procesar" TabIndex = "1"/></td>
        <td align = "center" colspan = "1"><asp:Button ID="btnExportar" runat = "server" OnClick = "btn_exportar_datos" Text="Exportar" TabIndex = "1"/></td>
    </tr>
    <tr>
        <td colspan = "2">
            <table>
                <tr>
                <td colspan = "2">
                <label style="text-transform: uppercase; color: #000000; font-family: 'Arial Black'">DETALLE</label>
                </td>
                </tr>
                <tr>
                <td><label>#Registros encontrados:</label></td>
                <td><asp:Label ID="lblNumEncontrados" runat = "server"></asp:Label></td>
                </tr>
                <tr>
                <td><label>#Registros no encontrados:</label></td>
                <td><asp:Label ID="lblNoEncontrados" runat = "server"></asp:Label></td>
                </tr>
                <tr>
                <td><label>#Registros Totales:</label></td>
                <td><asp:Label ID="lblTotales" runat = "server"></asp:Label></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
    <td align="center" colspan = "2">
     <asp:GridView ID="DatosExcel" runat="server" OnPageIndexChanging = "PageIndexChanging" AllowPaging = "true" PageSize="10" Font-Size="Smaller">
     </asp:GridView>
    </td>
    </tr>
</table>
</asp:Content>
