<%@ Page Language="C#" MasterPageFile = "~/masterMenu.Master" AutoEventWireup="true" CodeBehind="EdicionIngresoIncidente.aspx.cs" Inherits="RegistroIncidentes.EdicionIngresoIncidente" %>

<asp:Content ID = "contenido" runat = "server" ContentPlaceHolderID = "contenedorPrincipal">
    <br />
<div align="left">
<table width = "600px">
<tr>
<td> <label>Buscar por :</label></td>
<td colspan = "1"><asp:RadioButtonList ID = "rbgSeleccion" runat = "server" 
RepeatDirection = "Horizontal" AutoPostBack = "true" OnSelectedIndexChanged = "habilitar_datos_chbx">
    <asp:ListItem Selected = "True" Text = "código" Value = "codigo"></asp:ListItem>
    <asp:ListItem Text = "fechas" Value = "fechas"></asp:ListItem>
</asp:RadioButtonList></td>
<td><asp:Button ID = "btnBuscar" runat = "server" Text = "Buscar" Width="75px" 
        OnClick = "btn_busqueda_datosBy" TabIndex="4"/></td>
<td><asp:Button ID = "btnNuevo" runat = "server" Text = "Nuevo" Width="75px"
OnClick = "nuevo_registro_incidente" TabIndex="5"/></td>
</tr>
    <tr>
        <td colspan = "1"><asp:Label ID ="lblCodigo" runat = "server">Código Incidente </asp:Label></td>
        <td colspan = "3"><asp:TextBox ID = "txbxCodigoIncidente" runat = "server" 
                AutoCompleteType="Disabled" TabIndex="1"></asp:TextBox></td>
        
    </tr>
    <tr>
    <td><asp:Label ID ="lbFechaInicio" runat = "server" Visible="False">Fecha Incio : </asp:Label></td>
    <td>
        <asp:TextBox ID="txbxFechaInicio" runat="server" ReadOnly = "false" 
            Visible="False" AutoCompleteType="Disabled" TabIndex="2"></asp:TextBox>
        <asp:ImageButton ImageUrl = "~/Imagenes/calender.png" runat="server" 
        ID = "imgIni" Visible="False" OnClientClick="return false;" />
        </td>
    <td><asp:Label ID ="lblFechaFin" runat = "server" Visible="False">Fecha Fin </asp:Label></td>
    <td><asp:TextBox ID="txbxFechaFin" runat="server" ReadOnly = "false" 
            Visible="False" AutoCompleteType="Disabled" TabIndex="3"></asp:TextBox>
        <asp:ImageButton ImageUrl = "~/Imagenes/calender.png" runat="server" 
        ID = "imgFin" Visible="False" OnClientClick="return false;"/></td>
    </tr>
    <tr>
    <td colspan = "4"><asp:Label ID = "lblMensajeError" runat = "server" 
            ForeColor="Red"></asp:Label></td>
    </tr>
    
</table>
</div>
<br />
<div align="left">  
            <asp:GridView ID="GridViewIncidente" HeaderStyle-BackColor="#e94f31" HeaderStyle-ForeColor="White"  
                runat="server" AutoGenerateColumns="False" AllowPaging = "True" 
                PageSize="7" CellSpacing = "1"  OnRowCommand="edicion_fila_suceso" 
                onpageindexchanged="GridViewIncidente_PageIndexChanged_" 
                onpageindexchanging="GridViewIncidente_PageIndexChanging" Font-Size="Smaller" >  
                <Columns>  
                    <asp:BoundField DataField="codigoIncidente" HeaderText="# Incidente" 
                        ItemStyle-Width="100" >  
<ItemStyle Width="100px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="codigo_usuario" HeaderText="Usuario" ItemStyle-Width="150" >  
<ItemStyle Width="150px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="reporteIncidente" HeaderText="Fecha Incidente" 
                        ItemStyle-Width="150" DataFormatString="{0:MM-dd-yyyy HH:mm}" >
<ItemStyle Width="150px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="descripcionincidente" HeaderText="Descripción" 
                        ItemStyle-Width="150" >    
<ItemStyle Width="150px"></ItemStyle>
                    </asp:BoundField>
                    <asp:ButtonField ButtonType="Button" CommandName="Edicion" Text="Edición"/> 
                </Columns>  

                <HeaderStyle BackColor="#E94F31" ForeColor="White"></HeaderStyle>
            </asp:GridView>  
        </div>  

</asp:Content>