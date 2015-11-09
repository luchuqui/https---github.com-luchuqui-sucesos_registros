<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RegistroIncidentes._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Sistema Registro Incidentes</title>
    <style type="text/css">
        .style1
        {
            color: #FFFFFF;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server" >
    <br />
    <br />
    <br />
    <br />
    <table width = "50%" align="center" >
    <tr>
        <td colspan = "2" style="background-color: #e94f31">
        <h2 style="text-align: center; background-color: #e94f31;" class="style1">Acceso al Sistema</h2>
        </td>
    </tr>
    <tr>
    <th> <label> Documento : </label></th>
    <td> <asp:TextBox ID = "txbxDocumento" runat ="server" TabIndex="1" 
            AutoCompleteType="Disabled"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <th>
        <label>Contraseña : </label>
    </th>
    <td>
    <asp:TextBox runat = "server"  ID = "txbxPassword" TextMode = "Password" 
            TabIndex="2"></asp:TextBox>
    
    </td>
    </tr>
    
    <tr>
    <td colspan = "2">
        <asp:Label ID = "mensajeError" runat = "server" ForeColor="#CC3300" ></asp:Label>
    </td>
    </tr>
    <tr>
    <td colspan = "2">
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ValidationGroup = "grValidacion" ControlToValidate = "txbxPassword"
        ErrorMessage="RequiredFieldValidator">Debe ingresar su contraseña</asp:RequiredFieldValidator>
        
    </td>
    
    </tr>
    <tr>
    <td colspan = "2">
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
        ControlToValidate = "txbxDocumento"
        ValidationGroup = "grValidacion" ErrorMessage="RequiredFieldValidator">Debe ingresar el número de documento</asp:RequiredFieldValidator>
    </td>
    
    </tr>
    <tr align = "center">
    <td colspan = "2">
        <asp:Button ID = "btnAccesoSistema" runat = "server" Text = "Acceso Sistema" 
            CausesValidation = "true" ValidationGroup = "grValidacion" 
            OnClick = "btn_acceso_sistema" TabIndex="3"/>
    </td>
    </tr>
    <tr align = "right">
        <td colspan = "2">
        <asp:LinkButton ID = "registroUsuario" runat = "server" Text = "Registro de Usuario" PostBackUrl ="~/RegistroUsuario.aspx" ></asp:LinkButton>
        </td>
    </tr>
    </table>
    </form>
</body>
</html>
