<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile = "~/masterMenu.Master" CodeBehind="ReporteAsignacionSuceso.aspx.cs" Inherits="RegistroIncidentes.ReporteAsignacionSuceso" %>

<asp:Content ID = "contenido" runat = "server" ContentPlaceHolderID = "contenedorPrincipal">
    <div>
<asp:Panel ID = "panelEdicion" runat = "server" DefaultButton ="btnBuscar">
<table width = "600px">
<tr>

<td colspan = "2"><asp:Button ID = "btnBuscar" runat = "server" Text = "Buscar" 
        Width="75px" OnClick = "btn_busqueda_datos" TabIndex="5"/></td>
<td colspan = "2"><asp:Button ID = "btnExportar" runat = "server" Text = "xls" 
        Width="75px" OnClick = "btn_exportar_datos" TabIndex="6"/></td>
</tr>
    
    <tr>
    <td><asp:Label ID ="lbFechaInicio" runat = "server" Visible="true">Fecha Incio : </asp:Label></td>
    <td>
        <asp:TextBox ID="txbxFechaInicio" runat="server"
            Visible="true" TabIndex="3"></asp:TextBox>
        <asp:ImageButton ImageUrl = "~/Imagenes/calender.png" runat="server" 
        ID = "imgIni" Visible="true" OnClientClick="return false;" />
        </td>
    <td><asp:Label ID ="lblFechaFin" runat = "server" Visible="true">Fecha Fin </asp:Label></td>
    <td><asp:TextBox ID="txbxFechaFin" runat="server" Visible="true" TabIndex="4"></asp:TextBox>
        <asp:ImageButton ImageUrl = "~/Imagenes/calender.png" runat="server" 
        ID = "imgFin" Visible="true" OnClientClick="return false;"/></td>
    </tr>
    <tr>
    <td colspan = "4"><asp:Label ID = "lblMensajeError" runat = "server" 
            ForeColor="Red"></asp:Label></td>
    </tr>
</table>
</asp:Panel>    
</div>
<div>
<asp:GridView ID="GridViewIncidente" HeaderStyle-BackColor="#e94f31" HeaderStyle-ForeColor="White"  
                runat="server" AutoGenerateColumns="False" AllowPaging = "True" 
                 CellSpacing = "1" onrowdatabound="pieDePaginaDataGrid"
        onpageindexchanging="GridViewIncidente_PageIndexChanging" Font-Size="Small" ShowFooter="True">
                <Columns>  
                    <asp:BoundField DataField="codigoIncidente" HeaderText="# Incidente" 
                        ItemStyle-Width="100" >  
<ItemStyle Width="100px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="usuarioAsigna" HeaderText="usuario Registra" />
                    <asp:BoundField DataField="codigo_usuario" HeaderText="Usuario Asignado" 
                        ItemStyle-Width="150">  
<ItemStyle Width="150px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="reporteIncidente" HeaderText="Fecha Incidente" 
                        DataFormatString="{0:MM-dd-yyyy HH:mm}">
                    <ItemStyle Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="descripcionincidente" HeaderText="Etiqueta" />
                </Columns>  

<HeaderStyle BackColor="#E94F31" ForeColor="White"></HeaderStyle>

            </asp:GridView>  
</div>
</asp:Content>