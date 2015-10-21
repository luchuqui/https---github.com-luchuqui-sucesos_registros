<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile = "~/masterMenu.Master" CodeBehind="RegistroEdicionIncidente.aspx.cs" Inherits="RegistroIncidentes.RegistroEdicionIncidente" %>

<asp:Content ID = "contenido" runat = "server" ContentPlaceHolderID = "contenedorPrincipal">
    <table align="center" width = "100%">
    <tr>
        <th colspan = "3"><label>REGISTRO - EDICIÓN DE INCIDENTES</label></th>
        
    </tr>
    <tr>
        <th style="text-align: left; width: 233px;"><label>Usuario Asigna :</label></th>
        <td colspan ="2"><asp:Label ID = "lblUsuarioAsigna" runat = "server"></asp:Label></td>
        <th style="text-align: left; width: 157px;"><label>Usuario Registra :</label></th>
        <td colspan ="1"><asp:Label ID = "lblUsuarioRegistra" runat = "server"></asp:Label></td>
    </tr>
   
    <tr>
    <th style="text-align: left; width: 233px;"><label>Código Incidente :</label></th>
    <td style="width: 209px"><asp:TextBox ID = "txbxNumIncidente" runat = "server" 
            AutoCompleteType="Disabled" TabIndex="1"></asp:TextBox></td>
    <td colspan = "3"><asp:RequiredFieldValidator ID="validadorNumIncidencia" runat="server" 
        ValidationGroup = "GrValidacion" ControlToValidate = "txbxNumIncidente"
        ErrorMessage="RequiredFieldValidator">Ingrese Código Incidente</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
    <th style="text-align: left; width: 233px;"><label>Recibido Sistemas :</label></th>
    <td colspan = "4"><asp:CheckBox ID = "chbxRebido" runat = "server" Text = ""
    AutoPostBack = "true" OnCheckedChanged = "onclick_datos_sistema" Checked = "false" 
            TabIndex="2"/></td>
    </tr>
    <tr>
    <th style="text-align: left; width: 233px;">
        <label>Tipo Sistemas :</label></th>
    <td colspan = "4"><asp:DropDownList ID = "lsBxTipoSistemas" runat = "server" 
            TabIndex="3" Enabled = "false" Width = "30%">
    <asp:ListItem Text = "Seleccione Tipo Sistemas" Value="0"></asp:ListItem>
    </asp:DropDownList></td>
    </tr>
    <tr>
    <th style="text-align: left; width: 233px;"><label>Fecha Reporte Incidente :</label></th>
    <td style="width: 209px"><asp:TextBox ID = "txbxFechaIncidente" runat = "server" 
            AutoCompleteType="Disabled" TabIndex="4"></asp:TextBox>
    <img src ="Imagenes/calender.png" onclick = "mostrarDateTimePickerTxbxRegistro()" alt=""/></td>
    <td colspan ="3"><asp:RequiredFieldValidator ID="validador" runat="server" 
        ValidationGroup = "GrValidacion" ControlToValidate = "txbxFechaIncidente"
        ErrorMessage="RequiredFieldValidator">Ingrese Fecha Incidente</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
    <th style="text-align: left; width: 233px;"><label>Fecha Primera Interacción :</label></th>
    <td colspan = "4"><asp:TextBox ID = "txbxFechaInicio" runat = "server" 
            ReadOnly = "false" TabIndex="5"></asp:TextBox>
    <img src = "Imagenes/calender.png" onclick = "mostrarDateTimePickerTxbxInicio()" alt = ""/>
    </td>
    </tr>
    <tr>
    <th style="text-align: left; width: 233px;"><label>Grupo Asignado :</label></th>
    <td colspan = "1" style="width: 209px"><asp:DropDownList ID = "lsBxGrupoAsignado" runat = "server" 
            TabIndex="6" Width = "100%">
    <asp:ListItem Text = "Seleccione Grupo Asignado" Value="0"></asp:ListItem>
    </asp:DropDownList></td>
    <td colspan ="3">
    <asp:RequiredFieldValidator ID="campoRequeridoCbx" runat="server" 
        ValidationGroup = "GrValidacion" InitialValue = "0" 
        ControlToValidate = "lsBxGrupoAsignado"
        ErrorMessage="RequiredFieldValidator">Seleccione un Grupo</asp:RequiredFieldValidator>
    </td>
    </tr>
    <tr>
    <th style="text-align: left; width: 233px;"><label>Aréa :</label></th>
    <td colspan = "1" style="width: 209px"><asp:DropDownList ID = "lsBxDatoSeleccionado" runat = "server" 
            TabIndex="7" Width = "100%">
    <asp:ListItem Text = "Seleccione Dato" Value="0"></asp:ListItem>
    </asp:DropDownList></td>
    <td colspan = "3">
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ValidationGroup = "GrValidacion" InitialValue = "0" 
        ControlToValidate = "lsBxDatoSeleccionado"
        ErrorMessage="RequiredFieldValidator">Seleccione Aréa</asp:RequiredFieldValidator>
    </td>
    </tr>
    <tr>
    <th style="text-align: left; width: 233px;">
        <label>SOP :</label></th>
    <td colspan = "1" style="width: 209px"><asp:DropDownList ID = "lsBxSOP" runat = "server" TabIndex="8" Width = "100%">
    <asp:ListItem Text = "Seleccione SOP" Value="0"></asp:ListItem>
    </asp:DropDownList></td>
    <td colspan = "3">
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ValidationGroup = "GrValidacion" InitialValue = "0" 
        ControlToValidate = "lsBxSOP"
        ErrorMessage="RequiredFieldValidator">Seleccione SOP</asp:RequiredFieldValidator>
    </td>
    </tr>
    <tr>
    <th style="text-align: left; width: 233px;"><label>Estado Incidente :</label></th>
    <td colspan = "1" style="width: 209px"><asp:DropDownList ID = "lsBxEstadoIncidente" runat = "server" 
            TabIndex="9" Width = "100%">
    <asp:ListItem Text = "Seleccione Estado Incidente" Value="0"></asp:ListItem>
    </asp:DropDownList></td>
    <td colspan = "1">
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
        ValidationGroup = "GrValidacion" InitialValue = "0" 
        ControlToValidate = "lsBxEstadoIncidente"
        ErrorMessage="RequiredFieldValidator">Seleccione estado incidente</asp:RequiredFieldValidator>
    </td>
    <th style="text-align: left; width: 157px;"><label>Estadistica :</label></th>
    <td colspan = "1"><asp:CheckBox ID = "chbxEstadistica" runat = "server" Text = ""/></td>
    </tr>
    
    <tr>
    <th style="text-align: left; width: 233px;"><label>TIER I :</label></th>
    <td colspan = "1" style="width: 209px">
        <asp:DropDownList ID = "lsBxTierUno" runat = "server"
    OnSelectedIndexChanged = "cargar_tier_dos" AutoPostBack = "true" TabIndex="10" 
            Enabled="true" Width = "100%">
    <asp:ListItem Text = "Seleccione TIER I" Value="0"></asp:ListItem>
    </asp:DropDownList></td>
    <td style="width: 140px">
    <asp:RequiredFieldValidator ID="validadorTierI" runat="server" 
        ValidationGroup = "GrValidacion" InitialValue = "0" 
        ControlToValidate = "lsBxTierUno"
        ErrorMessage="RequiredFieldValidator" Enabled="true">Seleccione Tier Uno</asp:RequiredFieldValidator>
    </td>
    <th style="text-align: left; width: 157px;"><label>Enviado Sistemas :</label></th>
    <td colspan = "2"><asp:CheckBox ID = "chBxEnviado" runat = "server" Checked = "false" 
            Text = "" TabIndex="13" OnCheckedChanged = "onclick_datos_sistema" AutoPostBack = "true"/></td>
    </tr>
    <tr>
    <th style="text-align: left; width: 233px;"><label>TIER II :</label></th>
    <td colspan = "1" style="width: 209px">
        <asp:DropDownList ID = "lsBxTierII" runat = "server"
    AutoPostBack = "true" OnSelectedIndexChanged = "cargar_tier_tres" TabIndex="11" 
            Enabled="true" Width = "100%">
    <asp:ListItem Text = "Seleccione TIER II" Value="0"></asp:ListItem>
    </asp:DropDownList></td>
    <td style="width: 140px">
    <asp:RequiredFieldValidator ID="validadorTierII" runat="server" 
        ValidationGroup = "GrValidacion" InitialValue = "0" 
        ControlToValidate = "lsBxTierII"
        ErrorMessage="RequiredFieldValidator" Enabled="true">Seleccione Tier Dos</asp:RequiredFieldValidator>
    </td>
    </tr>
    <tr>
    <th style="text-align: left; width: 233px;"><label>TIER III :</label></th>
    <td colspan = "1" style="width: 209px">
        <asp:DropDownList ID = "lsBxTierIII" runat = "server" TabIndex="12" 
            Enabled="true">
    <asp:ListItem Text = "Seleccione TIER III" Value="0"></asp:ListItem>
    </asp:DropDownList></td>
    <td colspan = "1" style="width: 140px">
    <asp:RequiredFieldValidator ID="validadorTierIII" runat="server" 
        ValidationGroup = "GrValidacion" InitialValue = "0" 
        ControlToValidate = "lsBxTierIII"
        ErrorMessage="RequiredFieldValidator" Enabled="true">Seleccione Tier tres</asp:RequiredFieldValidator>
    </td>
    <th style="text-align: left; width: 157px;"><label>País :</label></th>
    <td colspan = "2"><asp:TextBox ID = "txbxPais" runat = "server" TabIndex="12"></asp:TextBox></td>
    </tr>
    
    <tr>
    <th style="text-align: left; width: 233px;"><label>Fecha Cierre o Escalamiento :</label></th>
    <td colspan = "4"><asp:TextBox ID = "txbxFechaFin" runat = "server" TabIndex="14"></asp:TextBox>
    <img src = "Imagenes/calender.png" onclick = "mostrarDateTimePickerTxbxFin()" alt = ""/></td>
    
    </tr>
    <tr>
    <th style="text-align: left; width: 233px;"><label>Observación :</label></th>
    <td colspan = "2"> 
        <asp:TextBox ID = "txbxObservacion" runat = "server" TextMode="MultiLine" 
            Width="70%" TabIndex="15"></asp:TextBox></td>
            <th style="text-align: left; width: 157px;"><label>Etiqueta :</label></th>
    <td> 
        <asp:TextBox ID = "txbxEtiqueta" runat = "server" TextMode="MultiLine" 
            Width="100%" TabIndex="16"></asp:TextBox></td>
    </tr>
    <tr>
    <td colspan = "5"><asp:Label ID = "lblMensajeError" runat = "server" ForeColor="Red"></asp:Label></td>
    </tr>
    <tr>
        <td style="text-align: right; " colspan = "2">
            <asp:Button ID = "btnGuardar" Text = "Guardar" runat = "server" Width="100px"
            CausesValidation = "true" ValidationGroup = "GrValidacion"
            OnClick = "guardar_suceso_sistema" TabIndex="16"/>
        </td>
        <td colspan = "3">
        <asp:Button ID = "btnLimpiar" Text = "Limpiar" runat = "server" Width="100px"
        OnClick = "limpiar" TabIndex="17"/>
        </td>
    </tr>
</table>
</asp:Content>