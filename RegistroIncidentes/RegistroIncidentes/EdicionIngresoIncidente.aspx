<%@ Page Language="C#" MasterPageFile = "~/masterMenu.Master" AutoEventWireup="true" CodeBehind="EdicionIngresoIncidente.aspx.cs" Inherits="RegistroIncidentes.EdicionIngresoIncidente" %>

<asp:Content ID = "contenido" runat = "server" ContentPlaceHolderID = "contenedorPrincipal">
    <br />
<div align="center">
<asp:Panel ID = "panelBusqueda" runat= "server" DefaultButton = "btnBuscar">
<table style="width: 636px">
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
        <td colspan = "3">
            <asp:TextBox ID = "txbxCodigoIncidente" runat = "server" 
                AutoCompleteType="Disabled" TabIndex="1" Width="156px"></asp:TextBox></td>
        
    </tr>
    <tr>
    <td><asp:Label ID ="lbFechaInicio" runat = "server" Visible="False">Fecha Incio : </asp:Label></td>
    <td>
        <asp:TextBox ID="txbxFechaInicio" runat="server" ReadOnly = "false" 
            Visible="False" AutoCompleteType="Disabled" TabIndex="2" Width="156px"></asp:TextBox>
        <asp:ImageButton ImageUrl = "~/Imagenes/calender.png" runat="server" 
        ID = "imgIni" Visible="False" OnClientClick="return false;" />
        </td>
    <td><asp:Label ID ="lblFechaFin" runat = "server" Visible="False">Fecha Fin </asp:Label></td>
    <td><asp:TextBox ID="txbxFechaFin" runat="server" ReadOnly = "false" 
            Visible="False" AutoCompleteType="Disabled" TabIndex="3" Width="155px"></asp:TextBox>
        <asp:ImageButton ImageUrl = "~/Imagenes/calender.png" runat="server" 
        ID = "imgFin" Visible="False" OnClientClick="return false;"/></td>
    </tr>
    <tr>
    <td colspan = "4"><asp:Label ID = "lblMensajeError" runat = "server" 
            ForeColor="Red"></asp:Label></td>
    </tr>
    
</table>
</asp:Panel>
</div>
<br />
<div align="center" style="width: auto; height: auto" >  
            <asp:GridView ID="GridViewIncidente" HeaderStyle-BackColor="#e94f31" HeaderStyle-ForeColor="White"  
                runat="server" AutoGenerateColumns="False" AllowPaging = "True" 
                CellSpacing = "1"  OnRowCommand="edicion_fila_suceso" 
                onpageindexchanged="GridViewIncidente_PageIndexChanged_" 
                onpageindexchanging="GridViewIncidente_PageIndexChanging" 
                Font-Size="Smaller" Width="100%">  
                <Columns>  
                    <asp:BoundField DataField="codigoIncidente" HeaderText="# Incidente" 
                        ItemStyle-Width="100" >  
<ItemStyle Width="11%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="codigo_usuario" HeaderText="Usuario" ItemStyle-Width="150" >  
<ItemStyle Width="15%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="reporteIncidente" HeaderText="Fecha Incidente" 
                        ItemStyle-Width="150" DataFormatString="{0:MM-dd-yyyy HH:mm}" >
<ItemStyle Width="12%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="primerInteraccion" 
                        ItemStyle-Width="150" DataFormatString="{0:MM-dd-yyyy HH:mm}" 
                        HeaderText="Primera Interacción" >
<ItemStyle Width="12%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="fechaCierre" DataFormatString="{0:MM-dd-yyyy HH:mm}" 
                        ItemStyle-Width="150" HeaderText="Fecha Cierre" >
<ItemStyle Width="12%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="etiqueta" HeaderText="Etiqueta" 
                        ItemStyle-Width="150" >    
<ItemStyle Width="40%"></ItemStyle>
                    </asp:BoundField>
                    <asp:ButtonField ButtonType="Button" CommandName="Edicion" Text="Edición"> 
                        <ItemStyle Width="5%" />
                    </asp:ButtonField>
                </Columns>  

                <HeaderStyle BackColor="#E94F31" ForeColor="White"></HeaderStyle>
            </asp:GridView>  
        </div>  

</asp:Content>