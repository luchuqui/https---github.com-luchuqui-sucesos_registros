<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile = "~/masterMenu.Master" CodeBehind="ReporteSucesos.aspx.cs" Inherits="RegistroIncidentes.ReporteSucesos" %>

<asp:Content ID = "contenido" runat = "server" ContentPlaceHolderID = "contenedorPrincipal">
    <div>
<asp:Panel ID ="panelEdicion" runat = "server" DefaultButton="btnBuscar">
<table width = "600px">
<tr>
<td> <label>Buscar por :</label></td>
<td colspan = "1"><asp:RadioButtonList ID = "rbgSeleccion" runat = "server" 
RepeatDirection = "Horizontal" AutoPostBack = "true" 
        OnSelectedIndexChanged = "habilitar_datos_chbx" TabIndex="1">
    <asp:ListItem Selected = "True" Text = "código" Value = "codigo"></asp:ListItem>
    <asp:ListItem Text = "fechas" Value = "fechas"></asp:ListItem>
</asp:RadioButtonList></td>
<td colspan = "1"><asp:Button ID = "btnBuscar" runat = "server" Text = "Buscar" 
        Width="75px" OnClick = "btn_busqueda_datos" TabIndex="5"/></td>
<td colspan = "1"><asp:Button ID = "btnExportar" runat = "server" Text = "Exportar" 
        Width="75px" OnClick = "btn_exportar_datos" TabIndex="6"/></td>
</tr>
    <tr>
        <td colspan = "1"><asp:Label ID ="lblCodigo" runat = "server">Código Incidente </asp:Label></td>
        <td colspan = "3"><asp:TextBox ID = "txbxCodigoIncidente" runat = "server" 
                TabIndex="2"></asp:TextBox></td>
        
    </tr>
    <tr>
    <td><asp:Label ID ="lbFechaInicio" runat = "server" Visible="False">Fecha Incio : </asp:Label></td>
    <td>
        <asp:TextBox ID="txbxFechaInicio" runat="server"
            Visible="False" TabIndex="3"></asp:TextBox>
        <asp:ImageButton ImageUrl = "~/Imagenes/calender.png" runat="server" 
        ID = "imgIni" Visible="False" OnClientClick="return false;" />
        </td>
    <td><asp:Label ID ="lblFechaFin" runat = "server" Visible="False">Fecha Fin </asp:Label></td>
    <td><asp:TextBox ID="txbxFechaFin" runat="server" Visible="False" TabIndex="4"></asp:TextBox>
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
<div>
<asp:GridView ID="GridViewIncidente" HeaderStyle-BackColor="#e94f31" HeaderStyle-ForeColor="White"  
                runat="server" AutoGenerateColumns="False" AllowPaging = "True" 
                 CellSpacing = "1" 
        onpageindexchanging="GridViewIncidente_PageIndexChanging" Font-Size="X-Small">
                <Columns>  
                    <asp:BoundField DataField="codigo_usuario" HeaderText="Usuario" ItemStyle-Width="150">  
<ItemStyle Width="150px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="codigoIncidente" HeaderText="# Incidente" 
                        ItemStyle-Width="100" >  
<ItemStyle Width="100px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="codigoTipoSistemas" HeaderText="Código Sistemas" 
                        ItemStyle-Width="150" >
<ItemStyle Width="150px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="recibidoSistemas" HeaderText="Recibido Sisemas" 
                        DataFormatString="{0:MM-dd-yyyy HH:mm}" />
                    <asp:BoundField DataField="reporteIncidente" HeaderText="Fecha Incidente" 
                        DataFormatString="{0:MM-dd-yyyy HH:mm}">
                    <ItemStyle Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="primerInteraccion" HeaderText="Fec. primera interacción" 
                        ItemStyle-Width="150" DataFormatString="{0:MM-dd-yyyy HH:mm}" >    
<ItemStyle Width="150px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="fechaCierre" HeaderText="Fecha Cierre" 
                        DataFormatString="{0:MM-dd-yyyy HH:mm}" />
                    <asp:BoundField DataField="codigoGrupoAsignado" HeaderText="Grupo Asignado" />
                    <asp:BoundField DataField="codigoDato" HeaderText="Dato Seleccion" />
                    <asp:BoundField DataField="codigoSop" HeaderText="SOP" />
                    <asp:BoundField DataField="estadoIncidente" HeaderText="Estado Incidente" />
                    <asp:BoundField DataField="codigoTierUno" HeaderText="TIER I" />
                    <asp:BoundField DataField="codigoTierDos" HeaderText="TIER II" />
                    <asp:BoundField DataField="codigoTierTres" HeaderText="TIER III" />
                    <asp:BoundField DataField="enviadoSistmas" HeaderText="Enviado Sistemas" />
                    <asp:BoundField DataField="estadistica" HeaderText="Estadistica" />
                    <asp:BoundField DataField="sla_respuesta" HeaderText="sla respuesta" />
                    <asp:BoundField DataField="sls_solucion" HeaderText="sla solución" />
                    <asp:BoundField DataField="etiqueta" HeaderText="etiqueta" />
                    <asp:BoundField DataField="descripcionincidente" HeaderText="descripcion" />
                </Columns>  

<HeaderStyle BackColor="#E94F31" ForeColor="White"></HeaderStyle>
            </asp:GridView>  
</div>
</asp:Content>