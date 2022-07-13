<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile = "~/masterMenu.Master" CodeBehind="AsignacionTicket.aspx.cs" Inherits="RegistroIncidentes.AsignacionTicket" %>

<asp:Content ID = "contenido" runat = "server" ContentPlaceHolderID = "contenedorPrincipal">
<br />
<div align="center">
<h2>Asignación de Ticks a Usuarios</h2>
</div>
<asp:Panel ID = "panelIngreso" runat = "server" DefaultButton ="btnGuardar">
<table>
    <tr>
    <td>
    <label>Usuario Reporta :</label>
    </td>
    <td colspan = "2">
    <asp:Label ID = "lblUsuarioReporta" runat = "server"></asp:Label>
    </td>
    </tr>
    <tr>
    <td><label>Usuario Asignar:</label></td>
    <td><asp:DropDownList runat = "server" ID = "lsBxUsuarios">
        <asp:ListItem Text = "Seleccione Usuario" Value="0"></asp:ListItem>
    </asp:DropDownList></td>
    <td>
    <asp:RequiredFieldValidator ID="campoRequeridoCbx" runat="server" 
        ValidationGroup = "GrValidacion" InitialValue = "0" 
        ControlToValidate = "lsBxUsuarios"
        ErrorMessage="RequiredFieldValidator">Seleccione un Usuario</asp:RequiredFieldValidator>
    </td>
    </tr>
    <tr>
    <td>
    <label>Código Incidente :</label>
    </td>
    <td>
        <asp:TextBox ID= "txbxCodigoIncidente" runat = "server"></asp:TextBox>
    </td>
    <td><asp:RequiredFieldValidator ID="validadorNumIncidencia" runat="server" 
        ValidationGroup = "GrValidacion" ControlToValidate = "txbxCodigoIncidente"
        ErrorMessage="RequiredFieldValidator">Ingrese Código Incidente</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
    <td>
    <label>Etiqueta :</label>
    </td>
    <td>
        <asp:TextBox ID= "txbxDescripcion" runat = "server" TextMode="MultiLine"></asp:TextBox>
    </td>
    <td><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ValidationGroup = "GrValidacion" ControlToValidate = "txbxDescripcion"
        ErrorMessage="RequiredFieldValidator">Una Etiqueta</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
    <td colspan = "3"><asp:Label runat = "server" ID = "lblMensaje" ForeColor="#990000"></asp:Label></td>
    </tr>
    <tr>
        <td><asp:Button  ID = "btnGuardar" runat = "server" Text = "Guardar"
        CausesValidation = "true" ValidationGroup = "GrValidacion"
        OnClick = "guardarSuceso"/> </td>
        <td colspan = "2"><asp:Button  ID = "btnLimpiar" runat = "server" Text = "Limpiar"/></td>
    </tr>
    
</table>
</asp:Panel>
</asp:Content>