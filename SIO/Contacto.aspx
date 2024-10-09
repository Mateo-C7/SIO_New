<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="Contacto.aspx.cs" Inherits="SIO.Contacto" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">

        .sangria
        {
            word-spacing: 10pt;
            font-family: Tahoma;
            font-size: 11pt;
        color: #1C5AB6;
        text-align: right;
    }
                        
        .CustomComboBoxStyle .ajax__combobox_textboxcontainer input 
        {
            background-image:url('http://app.forsa.com.co/SIOMaestros/Imagenes/toolkit-bg.gif');
            border-style:none;  
        }
        .CustomComboBoxStyle .ajax__combobox_buttoncontainer button 
        {
            background-image:url('http://app.forsa.com.co/SIOMaestros/Imagenes/toolkit-arrow.gif');
            border-style:none;            
        }
        .center
        {
            font-family: Arial;
            font-size: 8pt;
            Text-Align:Center; 
        }
        .style105
        {
            height: 23px;
        }
        .style178
        {            text-align: right;
        }
        .style143
        {
            height: 19px;
            width: 472px;
            margin-left: 40px;
        }
        .style133
        {
            height: 19px;
            width: 1045px;
            margin-left: 40px;
        }
        .style165
        {
            height: 19px;
            width: 403px;
            margin-left: 40px;
        }
        .style144
        {
            width: 472px;
            margin-left: 40px;
        }
        .style134
        {
            width: 1045px;
            margin-left: 40px;
        }
        .style132
        {
            width: 403px;
            margin-left: 40px;
        }
        .style124
        {
            height: 31px;
            margin-left: 40px;
        }
        .style227
        {
            height: 19px;
            width: 1359px;
            margin-left: 40px;
        }
        .style228
        {
            width: 1359px;
            margin-left: 40px;
        }
        .style237
        {
            height: 15px;
            width: 200px;
            margin-left: 40px;
            font-size: 5pt;
        }
        .style238
        {
            width: 200px;
        }
        .style245
        {
            width: 127px;
        }
        .style248
        {
            height: 15px;
            width: 238px;
        }
        .style249
        {
            width: 238px;
        }
        .style250
        {
            height: 19px;
            width: 1152px;
            margin-left: 40px;
        }
        .style251
        {
            width: 1152px;
            margin-left: 40px;
        }
        .style252
        {
            height: 19px;
            width: 1375px;
            margin-left: 40px;
        }
        .style253
        {
            width: 1375px;
            margin-left: 40px;
        }
        .style256
        {
            height: 19px;
            width: 366px;
            margin-left: 40px;
        }
        .style257
        {
            width: 366px;
            margin-left: 40px;
        }
        .style259
        {
            width: 244px;
        }
        .style263
        {
            width: 387px;
        }
        .style264
        {
            width: 12px;
        }
        .style265
        {
            width: 36px;
        }
        .style266
        {
            height: 34px;
        }
        .style267
        {
            width: 12px;
            text-align: right;
        }
        </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblContactoTitulo" runat="server" 
                Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" ForeColor="#1C5AB6" 
                Text="CONTACTO - " Width="200px" style="text-align: right"></asp:Label>
       
            <asp:Label ID="lblClienteP" runat="server" Font-Bold="True" Font-Names="Tahoma" 
                Font-Size="9pt" ForeColor="#1C5AB6" Width="600px"></asp:Label>

                <asp:Label ID="lblIdContacto" runat="server" Font-Bold="True" Font-Names="Tahoma" 
                Font-Size="9pt" ForeColor="Silver" Width="40px"></asp:Label>
       
            <table style="height: 357px; margin-bottom: 0px; width: 981px;">
                <tr>
                    
                    <td class="style105" style="text-align: right; word-spacing: 2pt">
                        &nbsp;&nbsp;<asp:ImageButton ID="ImageButton1" runat="server" 
                            ImageUrl="~/Imagenes/Arrow back.png" onclick="ImageButton1_Click" 
                            style="text-align: right" ToolTip="Volver a Empresa" />
                    </td>
                    <tr>
                        <td style="text-align: left; word-spacing: 2pt">
                            
                            <table style="width: 992px">
                                <tr>
                                    <td class="style264">&nbsp;</td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblNombre1" runat="server" Font-Bold="False" Font-Names="Arial"
                                            Font-Size="8pt" Text="Nombres (*)" Width="100px" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td class="style248">
                                        <asp:TextBox ID="txtNombre1" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            OnTextChanged="txtNombre1_TextChanged" TabIndex="1" Width="200px"></asp:TextBox>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="lblPaiCliMat0" runat="server" Font-Bold="False"
                                            Font-Names="Arial" Font-Size="8pt" Style="text-align: left"
                                            Text="Asociar a la Empresa:" Width="300px" ForeColor="Black"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style264">&nbsp;</td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblApellido1" runat="server" Font-Bold="False"
                                            Font-Names="Arial" Font-Size="8pt" Text="Apellidos (*)" Width="100px"
                                            ForeColor="Black"></asp:Label>
                                    </td>
                                    <td class="style248">
                                        <asp:TextBox ID="txtApellido1" runat="server" Font-Names="Arial"
                                            Font-Size="8pt" OnTextChanged="txtApellido1_TextChanged" TabIndex="2"
                                            Width="200px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblPaiCliMat" runat="server" Font-Bold="False"
                                            Font-Names="Arial" Font-Size="8pt" Text="Pais" Width="50px"
                                            ForeColor="Black"></asp:Label>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:ComboBox ID="CboPaisMatriz" runat="server"
                                            AutoCompleteMode="SuggestAppend" AutoPostBack="True"
                                            DropDownStyle="DropDownList" Font-Names="Arial"
                                            Font-Size="8pt"
                                            OnSelectedIndexChanged="CboPaisMatriz_SelectedIndexChanged" TabIndex="11"
                                            Width="200px">
                                        </asp:ComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style264">&nbsp;</td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblCorreo1" runat="server" Font-Bold="False" Font-Names="Arial"
                                            Font-Size="8pt" Text="E – Mail Corporativo (*)" Width="160px" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td class="style248">
                                        <asp:TextBox ID="txtCorreo1" runat="server" AutoPostBack="True"
                                            Font-Names="Arial" Font-Size="8pt" OnTextChanged="txtCorreo1_TextChanged"
                                            TabIndex="3" Width="200px">correo@com.co</asp:TextBox>
                                    </td>


                                    <td style="text-align: right">
                                        <asp:Label ID="lblCiuCliMat" runat="server" Font-Bold="False"
                                            Font-Names="Arial" Font-Size="8pt" Style="text-align: right" Text="Ciudad "
                                            Width="100px" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:ComboBox ID="cboCiudadMatriz" runat="server"
                                            AutoCompleteMode="SuggestAppend" AutoPostBack="True"
                                            DropDownStyle="DropDownList" Font-Names="Arial"
                                            Font-Size="8pt"
                                            OnSelectedIndexChanged="cboCiudadMatriz_SelectedIndexChanged" TabIndex="12"
                                            Width="200px">
                                        </asp:ComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style264">&nbsp;</td>

                                    <td style="text-align: right;">
                                        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial"
                                            Font-Size="8pt" Text="E – Mail Personal (*)" Width="160px" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td class="style248">
                                        <asp:TextBox ID="txtCorreoPersonal" runat="server" AutoPostBack="True"
                                            Font-Names="Arial" Font-Size="8pt" OnTextChanged="txtCorreoPersonal_TextChanged"
                                            TabIndex="3" Width="200px">correoPersonal@com.co</asp:TextBox>
                                    </td>

                                    <td style="text-align: right">
                                        <asp:Label ID="lblClienteMat" runat="server" Font-Bold="False"
                                            Font-Names="Arial" Font-Size="8pt" Font-Underline="False" ForeColor="Black"
                                            Style="text-align: right" Text="Empresa (*)" Width="70px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:ComboBox ID="cboClienteMatriz" runat="server"
                                            AutoCompleteMode="SuggestAppend" AutoPostBack="True"
                                            DropDownStyle="DropDownList" Font-Names="Arial"
                                            Font-Size="8pt"
                                            OnSelectedIndexChanged="cboClienteMatriz_SelectedIndexChanged" TabIndex="13"
                                            Width="300px">
                                        </asp:ComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style264">&nbsp;</td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblProfesion" runat="server" Font-Bold="False"
                                            Font-Names="Arial" Font-Size="8pt" Style="margin-top: 3px; text-align: right;"
                                            Text="Profesión (*)" Width="100px" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td class="style248">
                                        <asp:ComboBox ID="cboTipoProfesion" runat="server" AutoCompleteMode="SuggestAppend"
                                            DropDownStyle="DropDownList" Font-Names="Arial"
                                            Font-Size="8pt" ItemInsertLocation="Append" TabIndex="4"
                                            Width="185px">
                                        </asp:ComboBox>
                                    </td>

                                    <td style="text-align: right">
                                        <asp:Label ID="lblObra" runat="server" Font-Bold="False" Font-Names="Arial"
                                            Font-Size="8pt" Font-Underline="False" ForeColor="Black" Text="Obra"
                                            Width="70px" Style="margin-bottom: 0px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:ComboBox ID="cboObra" runat="server" AutoCompleteMode="SuggestAppend"
                                            AutoPostBack="True" DropDownStyle="DropDownList"
                                            Font-Names="Arial" Font-Size="8pt" TabIndex="14"
                                            Width="300px">
                                        </asp:ComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style264">&nbsp;</td>
                                    <td class="style245">&nbsp;</td>
                                    <td class="style237">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style245">&nbsp;</td>

                                    <td style="text-align: right;">
                                        <asp:Label ID="lblTipoContacto" runat="server" Font-Bold="False"
                                            Font-Names="Arial" Font-Size="8pt" Text="Tipo De Contacto (*)"
                                            Width="120px" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td class="style248">
                                        <asp:ComboBox ID="cboTipoContacto" runat="server" AutoCompleteMode="SuggestAppend"
                                            AutoPostBack="True" DropDownStyle="DropDownList"
                                            Font-Names="Arial" Font-Size="8pt"
                                            ItemInsertLocation="Append"
                                            OnSelectedIndexChanged="cboTipoContacto_SelectedIndexChanged"
                                            Width="185px" TabIndex="5">
                                        </asp:ComboBox>
                                    </td>
                                   
                                </tr>
                                <tr>
                                    <td class="style264">&nbsp;</td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblCargo" runat="server" Font-Bold="False" Font-Names="Arial"
                                            Font-Size="8pt" Text="Cargo  (*)" Width="100px" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td class="style248">
                                        <asp:ComboBox ID="cboCargo" runat="server" AutoCompleteMode="SuggestAppend"
                                            AutoPostBack="True" DropDownStyle="DropDownList"
                                            Font-Names="Arial" Font-Size="8pt" TabIndex="6"
                                            Width="185px">
                                        </asp:ComboBox>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblTelefono1" runat="server" Font-Bold="False"
                                            Font-Names="Arial" Font-Size="8pt" Text="Teléfono Trabajo (*)"
                                            Width="110px" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="indicativo1" runat="server" CssClass="center" Font-Bold="True"
                                            Font-Names="Arial" Font-Size="8pt" Text="0" Width="20px" ForeColor="Black"></asp:Label>
                                        <asp:TextBox ID="txtprefijo1" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" TabIndex="15"
                                            Width="25px"></asp:TextBox>
                                        &nbsp;<asp:TextBox ID="txtTelefono1" runat="server" Font-Names="Arial"
                                            Font-Size="8pt" OnTextChanged="txtTelefono1_TextChanged" TabIndex="16"
                                            Width="160px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style264">&nbsp;</td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblCargo0" runat="server" Font-Bold="False" Font-Names="Arial"
                                            Font-Size="8pt" Text="Nombre Cargo  (*)" Width="130px" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td class="style248">
                                        <asp:TextBox ID="txtNombreCargo" runat="server" Font-Names="Arial"
                                            Font-Size="8pt" OnTextChanged="txtTelefono1_TextChanged" TabIndex="7"
                                            Width="200px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblTelefono2" runat="server" Font-Bold="False"
                                            Font-Names="Arial" Font-Size="8pt" Text="Teléfono Particular"
                                            Width="110px" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="indicativo2" runat="server" CssClass="center" Font-Bold="True"
                                            Font-Names="Arial" Font-Size="8pt" Text="0" Width="20px" ForeColor="Black"></asp:Label>
                                        <asp:TextBox ID="txtprefijo2" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" TabIndex="17"
                                            Width="25px"></asp:TextBox>
                                        &nbsp;<asp:TextBox ID="txtTelefono2" runat="server" Font-Names="Arial"
                                            Font-Size="8pt" TabIndex="18" Width="160px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style264">&nbsp;</td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblFeria" runat="server" Font-Bold="False" Font-Names="Arial"
                                            Font-Size="8pt" Style="margin-top: 3px" Text="Evento(Feria,Conferenc) *"
                                            Width="140px" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td class="style249">
                                        <asp:ComboBox ID="cboFeria" runat="server" AutoCompleteMode="Append"
                                            DropDownStyle="DropDownList" Font-Names="Arial"
                                            Font-Size="8pt" TabIndex="8" Width="185px">
                                        </asp:ComboBox>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblTelefono3" runat="server" Font-Bold="False"
                                            Font-Names="Arial" Font-Size="8pt" Text="Teléfono Movil" Width="110px"
                                            ForeColor="Black"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="indicativo3" runat="server" CssClass="center" Font-Bold="True"
                                            Font-Names="Arial" Font-Size="8pt" Text="0" Width="20px" ForeColor="Black"></asp:Label>
                                        <asp:TextBox ID="txtprefijo3" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" TabIndex="19"
                                            Width="25px"></asp:TextBox>
                                        &nbsp;<asp:TextBox ID="txtTelefMovil" runat="server" Font-Names="Arial"
                                            Font-Size="8pt" TabIndex="20" Width="160px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style264">&nbsp;</td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblCumple" runat="server" Font-Bold="False" Font-Names="Arial"
                                            Font-Size="8pt" Text="Fecha Cumpleaños" Width="101px" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td class="style249">
                                        <asp:TextBox ID="txtFechaCumple" runat="server" Font-Names="Arial"
                                            Font-Size="8pt" Style="margin-left: 0px" TabIndex="9" Width="80px"></asp:TextBox>
                                        <asp:CalendarExtender ID="txtFechaCumple_CalendarExtender" runat="server"
                                            Format="dd/MM/yyyy" TargetControlID="txtFechaCumple">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblUsuActualiza" runat="server" Font-Bold="False"
                                            Font-Names="Arial" Font-Size="8pt" Text="Usuario Actualiza" Width="100px"
                                            ForeColor="Black"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtUsuActualiza" runat="server" Font-Bold="False"
                                            Font-Names="Arial" Font-Size="8pt" Width="100px"></asp:Label>
                                        <asp:Label ID="txtFechaActualizacion" runat="server" Font-Bold="False"
                                            Font-Names="Arial" Font-Size="8pt" Width="110px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style264">&nbsp;</td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblHobby" runat="server" Font-Bold="False" Font-Names="Arial"
                                            Font-Size="8pt" Height="16px" Text="Hobby" Width="40px" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td class="style249">
                                        <asp:TextBox ID="txtHobby" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Style="margin-left: 0px; margin-top: 0px" TabIndex="10" Width="200px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right">&nbsp;</td>
                                    <td class="style238">
                                        <asp:CheckBox ID="chkNotificacion" runat="server" Font-Bold="False"
                                            Font-Names="Arial" Font-Size="8pt" ForeColor="Black" TabIndex="25"
                                            Text="No Enviar Notificaciones" />
                                    </td>
                                </tr>
                                     <tr>
                                    <td class="style264">&nbsp;</td>
                                      <td style="text-align: right;">
                                        <asp:Label ID="Label2" runat="server" Font-Bold="False"
                                            Font-Names="Arial" Font-Size="8pt" Text="LinkedIn" Width="100px"
                                            ForeColor="Black"></asp:Label>
                                    </td>
                                    <td class="style248">
                                        <asp:TextBox ID="txtLinkedIn" runat="server" Font-Names="Arial"
                                            Font-Size="8pt" TabIndex="2"
                                            Width="218px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style267">&nbsp;</td>
                                    <td class="style178" colspan="4">&nbsp;</td>
                                </tr>
                            </table>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="style266" style="text-align: left; word-spacing: 2pt">
                            <table style="margin-right: 0px">
                                <tr>
                                    <td rowspan="2" class="style263">
                                        &nbsp;</td>
                                    <td class="style259" rowspan="2">
                                        <asp:Label ID="lblComoContacto" runat="server" Font-Bold="False" 
                                            Font-Names="Arial" Font-Size="8pt" Text="Como Se Contacto? (*)" 
                                            Width="120px" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td class="style250">
                                        <asp:CheckBox ID="chkTelefono" runat="server" Font-Bold="False" 
                                            Font-Names="Arial" Font-Size="8pt" TabIndex="21" Text="Telefono" 
                                            ForeColor="Black" />
                                    </td>
                                    <td class="style252">
                                        <asp:CheckBox ID="chkTrabCampo" runat="server" Font-Bold="False" 
                                            Font-Names="Arial" Font-Size="8pt" TabIndex="23" Text="Trabajo de Campo" 
                                            ForeColor="Black" Width="120px" />
                                    </td>
                                    <td class="style227">
                                        <asp:CheckBox ID="chkPersonal" runat="server" Font-Bold="False" 
                                            Font-Names="Arial" Font-Size="8pt" TabIndex="25" Text="Personal" 
                                            ForeColor="Black" />
                                    </td>
                                    <td class="style143">
                                        <asp:CheckBox ID="chkEmail" runat="server" Font-Bold="False" Font-Names="Arial" 
                                            Font-Size="8pt" TabIndex="27" Text="E-Mail" ForeColor="Black" />
                                    </td>
                                    <td class="style133">
                                        <asp:CheckBox ID="chkVisita" runat="server" Font-Bold="False" 
                                            Font-Names="Arial" Font-Size="8pt" TabIndex="29" Text="Visita" 
                                            ForeColor="Black" />
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkCharlas" runat="server" Font-Bold="False" 
                                            Font-Names="Arial" Font-Size="8pt" TabIndex="31" Text="Charlas" 
                                            visible="false" ForeColor="Black" />
                                    </td>
                                    <td class="style165" style="backgrond-color: white;">
                                        &nbsp;</td>
                                    <td class="style165">
                                        &nbsp;</td>
                                    <td class="style256">
                                        &nbsp;</td>
                                    <tr>
                                        <td class="style251">
                                            <asp:CheckBox ID="chkConferencias" runat="server" Font-Bold="False" 
                                                Font-Names="Arial" Font-Size="8pt" TabIndex="22" 
                                                Text="Evento(Feria) *" ForeColor="Black" Width="120px" />
                                        </td>
                                        <td class="style253">
                                            <asp:CheckBox ID="chkReferencia" runat="server" Font-Bold="False" 
                                                Font-Names="Arial" Font-Size="8pt" TabIndex="24" Text="Referencia" 
                                                ForeColor="Black" />
                                        </td>
                                        <td class="style228">
                                            <asp:CheckBox ID="chkWeb" runat="server" Font-Bold="False" Font-Names="Arial" 
                                                Font-Size="8pt" TabIndex="26" Text="Web" ForeColor="Black" />
                                        </td>
                                        <td class="style144">
                                            <asp:CheckBox ID="chkMedComunicacion" runat="server" Font-Bold="False" 
                                                Font-Names="Arial" Font-Size="8pt" TabIndex="28" Text="Medio Comunicacion" 
                                                Width="140px" ForeColor="Black" />
                                        </td>
                                        <td class="style134">
                                            <asp:CheckBox ID="chkPublicImpresa" runat="server" Font-Bold="False" 
                                                Font-Names="Arial" Font-Size="8pt" TabIndex="30" Text="Public Impresa" 
                                                ForeColor="Black" Width="120px" />
                                        </td>
                                        <td class="style132">
                                            <asp:CheckBox ID="chkSeminarios" runat="server" Font-Bold="False" 
                                                Font-Names="Arial" Font-Size="8pt" TabIndex="32" Text="Seminario" 
                                                visible="false" Width="80px" ForeColor="Black" />
                                        </td>
                                        <td class="style132">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td class="style257">
                                            &nbsp;</td>
                                    </tr>
                                </tr>
                                <tr>
                                    <td class="style263">
                                        &nbsp;</td>
                                    <td class="style259">
                                        <asp:Label ID="lblComentario" runat="server" Font-Bold="False" 
                                            Font-Names="Arial" Font-Size="8pt" Text="Comentarios" Width="115px" 
                                            ForeColor="Black"></asp:Label>
                                    </td>
                                    <td colspan="9">
                                        <asp:TextBox ID="txtComentarios" runat="server" Font-Names="Arial" 
                                            Font-Size="8pt" TabIndex="33" TextMode="MultiLine" Width="720px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style263">
                                        &nbsp;</td>
                                    <td class="style259">
                                        &nbsp;</td>
                                    <td class="style124" colspan="9">
                                        <asp:RegularExpressionValidator ID="REVMail" runat="server" 
                                            ControlToValidate="txtCorreo1" ErrorMessage="RegularExpressionValidator" 
                                            Font-Bold="True" Font-Names="Arial" Font-Size="9pt" ForeColor="#CC3300" 
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Formato para e-mail incorrecto, asegurese que sea como el ejemplo. email@dominio.xx</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style263">
                                        &nbsp;</td>
                                    <td>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="Arial" 
                                            Font-Size="Medium" NavigateUrl="~/VerContactos.aspx" Font-Bold="False">Ver Contactos</asp:HyperLink>
                                    </td>
                                    <td colspan="9" style="text-align: right">
                                        <asp:Button ID="btnGuardarCont" runat="server" BackColor="#1C5AB6" 
                                            BorderColor="#999999" CssClass="botonsio" Font-Bold="True" Font-Names="Arial" 
                                            Font-Size="8pt"  onclick="btnGuardarCont_Click" 
                                            onclientclick="return confirm('Desea guardar los datos?')" TabIndex="34" 
                                            Text="Guardar" Width="80px" ForeColor="White" />
                                        &nbsp;
                                        <asp:Button ID="bntNuevo" runat="server" BackColor="#1C5AB6" 
                                            BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                             onclick="bntNuevo_Click" Text="Nuevo" Width="80px" ForeColor="White" />
                                        &nbsp;
                                        <asp:Button ID="btnEliminarCont" runat="server" BackColor="#1C5AB6" 
                                            BorderColor="#999999" CssClass="botonsio" Font-Bold="True" Font-Names="Arial" 
                                            Font-Size="8pt"  onclick="btnEliminarCont_Click" 
                                            onclientclick="return confirm('Desea eliminar el contacto?')" Text="Eliminar" 
                                            Visible="False" Width="80px" ForeColor="White" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style263">
                                        &nbsp;</td>
                                    <td class="style259">
                                        &nbsp;</td>
                                    <td colspan="9" style="text-align: right">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    
                </tr>
            </table>
            </td>
            </tr>
            <tr>
                <td style="font-family: Arial; font-size: 2pt">
                </td>
            </tr>
            </table>
             
        </ContentTemplate>
    </asp:UpdatePanel>

                                                
</asp:Content>
