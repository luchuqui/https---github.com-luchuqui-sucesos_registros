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
    <tr><td align = "center" colspan = "2"><asp:Button ID="btnProcesar" runat = "server" OnClick = "subir_archivo" Text="Procesar" TabIndex = "1"/></td></tr>
</table>
</asp:Content>
