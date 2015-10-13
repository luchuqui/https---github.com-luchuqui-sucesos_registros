<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroUsuario.aspx.cs" Inherits="RegistroIncidentes.RegistroUsuario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Registro Usuario</title>
    <style type="text/css">
        .style1
        {
            width: 198px;
        }
        .style2
        {
            color: #FFFFFF;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width = "40%" align="center">
    <tr align = "center">
        <td colspan = "3">
            <h1 style="background-color: #e94f31" class="style2">Registro de Usuario</h1>
        </td>
        
    </tr>
    <tr>
        <td colspan = "3">
        <br />
        </td>
    </tr>
    <tr>
    <td class="style1">
        <label>Número Documento : </label>
    </td>
    <td>
        <asp:TextBox runat = "server" ID = "txbxNumeroDocumento"></asp:TextBox>
    </td>
    <td>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
        ValidationGroup = "GrValidacion" ControlToValidate = "txbxNumeroDocumento"
        ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
    </td>
    </tr>
    <tr>
    <td class="style1"><label>Nombre :</label></td>
    <td>
        <asp:TextBox runat = "server" ID = "txbxNombre"></asp:TextBox>
    </td>
        <td>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ValidationGroup = "GrValidacion" ControlToValidate = "txbxNombre"
        ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
    </td>

    </tr>
    <tr>
    <td class="style1"> <label>Apellido :</label></td>
        <td><asp:TextBox runat = "server" ID = "txbxApellido"></asp:TextBox></td>
        <td>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ValidationGroup = "GrValidacion" ControlToValidate = "txbxApellido"
        ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
    </td>
    </tr>
    <tr>
    <td class="style1"> <label>E-mail :</label></td>
        <td><asp:TextBox runat = "server" ID = "txbxEmail"></asp:TextBox></td>
        <td>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ValidationGroup = "GrValidacion" ControlToValidate = "txbxEmail"
        ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
    </td>
    </tr>
    <tr>
    <td class="style1">
    <label>Contraseña :</label>
    </td>
    <td><asp:TextBox runat = "server" ID = "txbxConsenia" TextMode="Password"></asp:TextBox></td>
    <td>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
        ValidationGroup = "GrValidacion" ControlToValidate = "txbxConsenia"
        ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
    </td>
    </tr>
    <tr>
    <td class="style1">
    <label>Confirmar Contraseña :</label>
    </td>
    <td><asp:TextBox runat = "server" ID = "txbxConfirmacion" TextMode="Password"></asp:TextBox></td>
    <td>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
        ValidationGroup = "GrValidacion" ControlToValidate = "txbxConsenia"
        ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
    </td>
    </tr>
    <tr>
    <td colspan = "3">
        <asp:Label ID= "lblMensaje" runat = "server" ForeColor="#CC3300"></asp:Label>
    </td>
    </tr>
    <tr>
    <td align = "right" class="style1" >
    <asp:Button ID = "btnRegistrar" runat = "server" Text = "Guardar" 
            ValidationGroup = "GrValidacion" Width="100px" OnClick = "registrar_usuario_nuevo"/>
    </td>
    <td colspan = "2">
    <asp:Button ID = "btnCancelar" runat = "server" Text = "Regresar" 
            PostBackUrl="~/Default.aspx" Width="100px"/>
    </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
