<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile = "~/masterMenu.Master" CodeBehind="CambioContrasenia.aspx.cs" Inherits="RegistroIncidentes.CambioContrasenia" %>

<asp:Content ID = "contenido" runat = "server" ContentPlaceHolderID = "contenedorPrincipal">
    <br />
<br />
<br />
<table width = "60%">
<tr>
<td>
<asp:Label runat = "server" ID = "lblContraseniaActutal" Text = "Contraseña Actual :"></asp:Label>
</td>
<td>
<asp:TextBox ID = "txbxActual" runat ="server" TextMode="Password"></asp:TextBox>
</td>
</tr>
<tr>
<td>
<asp:Label runat = "server" ID = "LblNueva" Text = "Contraseña Nueva :"></asp:Label>
</td>
<td>
<asp:TextBox ID = "txbxNueva" runat ="server" TextMode="Password"></asp:TextBox>
</td>
</tr>
<tr>
<td>
<asp:Label runat = "server" ID = "lblConfirmar" Text = "Confirmar Contraseña : "></asp:Label>
</td>
<td>
<asp:TextBox ID = "txbxConfirmar" runat ="server" TextMode="Password"></asp:TextBox>
</td>
</tr>
<tr>
<td colspan = "2"><asp:Label ID = "lblMensaje" runat = "server" ForeColor="Red"></asp:Label></td>
</tr>
<tr>
<td colspan= "2" >
    <asp:Button ID ="btnAceptar" Text = "Cambiar" 
        runat = "server" Width="103px" OnClick ="btn_cambiarContraseña"/></td>
</tr>
</table>
</asp:Content>