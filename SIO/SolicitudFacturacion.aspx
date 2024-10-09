<%@ Page Title="" Language="C#" MasterPageFile="~/General2.Master" AutoEventWireup="true" CodeBehind="SolicitudFacturacion.aspx.cs" Inherits="SIO.SolicitudFacturacion" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--evalua el estado de la sesion--%>
 <script language="javascript" type="text/javascript">

//     var sessionTimeoutWarning = '<%= System.Configuration.ConfigurationSettings.AppSettings["SessionWarning"]%>';
//     var sessionTimeout = "<%= Session.Timeout %>";
//     var timeOnPageLoad = new Date();
//     var sessionWarningTimer = null;
//     var redirectToWelcomePageTimer = null;
//     //For warning
//     var sessionWarningTimer = setTimeout('SessionWarning()',
//				parseInt(sessionTimeoutWarning) * 60 * 1000);
//     //To redirect to the welcome page
//     var redirectToWelcomePageTimer = setTimeout('RedirectToWelcomePage()',
//					parseInt(sessionTimeout) * 60 * 1000);

//     //Session Warning
//     function SessionWarning() {
//         //minutes left for expiry
//         var minutesForExpiry = (parseInt(sessionTimeout) -
//					parseInt(sessionTimeoutWarning));
//         var message = "La sesión expirará en " +
//		minutesForExpiry + " mins. Desea ampliar la sessión?";

//         //Confirm the user if he wants to extend the session
//         answer = confirm(message);

//         //if yes, extend the session.
//         if (answer) {
//             var img = new Image(1, 1);
//             img.src = 'SolicitudFacturacion.aspx?date=' + escape(new Date());

//             //Clear the RedirectToWelcomePage method
//             if (redirectToWelcomePageTimer != null) {
//                 clearTimeout(redirectToWelcomePageTimer);
//             }
//             //reset the time on page load
//             timeOnPageLoad = new Date();
//             sessionWarningTimer = setTimeout('SessionWarning()',
//				parseInt(sessionTimeoutWarning) * 60 * 1000);
//             //To redirect to the welcome page
//             redirectToWelcomePageTimer = setTimeout
//		('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
//         }

//         //*************************
//         //Even after clicking ok(extending session) or cancel button, 
//         //if the session time is over. Then exit the session.
//         var currentTime = new Date();
//         //time for expiry
//         var timeForExpiry = timeOnPageLoad.setMinutes(timeOnPageLoad.getMinutes() +
//				parseInt(sessionTimeout));

//         //Current time is greater than the expiry time
//         if (Date.parse(currentTime) > timeForExpiry) {
//             alert("La sesión ha expirado. será redirigido a la página de inicio");
//             window.location = "index.aspx";
//         }
//         //**************************
//     }

//     //Session timeout
//     function RedirectToWelcomePage() {
//         alert("La sesión ha expirado. será redirigido a la página de inicio");
//         window.location = "index.aspx";
//     }

//     var img = new Image(1, 1);
//     img.src = 'SolicitudFacturacion.aspx?date=' + escape(new Date()); 
</script>   

    <style type="text/css">
      /* Accordion */
        .accordionHeader
        {
            border: 2px Outset #EBEBEB;
            color: white;
            background-color: #1C5AB6;
            font-family: Arial, Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
        }
 
        #master_content .accordionHeader a
        {
            color: #FFFFFF;
            background: none;
            text-decoration: none;
        }
 
        #master_content .accordionHeader a:hover
        {
            background: none;
            text-decoration: underline;
        }
 
        .accordionHeaderSelected
        {
            border: 2px Outset #EBEBEB;
            color: white;
            background-color: #1C5AB6;
            font-family: Arial, Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
        }
 
        #master_content .accordionHeaderSelected a
        {
            color: #FFFFFF;
            background: none;
            text-decoration: none;
        }
 
        #master_content .accordionHeaderSelected a:hover
        {
            background: none;
            text-decoration: underline;
        }
 
        .accordionContent
        {
            background-color: #EBEBEB;
            border: 0px outset #2F4F4F;
            border-top: none;
            padding: 5px;
            padding-top: 10px;
        }

        .CustomComboBoxStyle .ajax__combobox_buttoncontainer button 
        {
            background-image:url('http://app.forsa.com.co/SIOMaestros/Imagenes/toolkit-arrow.gif');
            border-style:none;            
        }
        .CustomComboBoxStyle .ajax__combobox_textboxcontainer input 
        {
            background-image:url('http://app.forsa.com.co/SIOMaestros/Imagenes/toolkit-bg.gif');
            border-style:none;  
        }
        .CustomComboBoxStyle .ajax__combobox_itemlist li
        {
            color: Black;  
            font-size:8pt;  
            font-family:Arial; 
            background-color:#EBEBEB
        }
        .style15
        {
            width: 100%;
            height: 72px;
        }
        .style17
        {
            height: 18px;
            text-align: justify;
        }
        .style24
        {
        }
        .style48
        {
            width: 177px;
            height: 23px;
            text-align: right;
        }
        .style49
        {
            width: 94px;
        }
        .style56
        {
            text-align: left;
            width: 208px;
        }
        .style70
        {
            text-align: right;
        }
        .style72
        {
            width: 99px;
            text-align: right;
        }
        .style73
        {
            text-align: right;
            width: 124px;
        }
        .style74
        {
            text-align: right;
            width: 93px;
        }
        .style79
        {
            width: 100%;
        }
    .style82
    {
        font-size: 4pt;
    }
        .style83
        {
            height: 18px;
            text-align: justify;
            width: 208px;
        }
        .style88
        {
            height: 18px;
            text-align: justify;
            width: 200px;
        }
        .style97
        {
        }
        .style98
        {
            width: 174px;
        }
        .style99
        {
            width: 208px;
        }
        .style101
        {
            width: 169px;
        }
        .style104
        {
            width: 126px;
        }
        .style105
        {
            width: 199px;
        }
        .style106
        {
            width: 128px;
            text-align: right;
        }
        .style107
        {
            width: 192px;
        }
         .overlay  
        {
          position: fixed;
          z-index: 98;
          top: 0px;
          left: 0px;
          right: 0px;
          bottom: 0px;
            background-color: #aaa; 
            filter: alpha(opacity=80); 
            opacity: 0.8; 
        }
        .overlayContent
        {
          z-index: 99;
          margin: 250px auto;
          width: 80px;
          height: 80px;
        }
        .overlayContent h2
        {
            font-size: 18px;
            font-weight: bold;
            color: #000;
        }
        .overlayContent img
        {
          width: 80px;
          height: 80px;
        }
        .style108
        {
            width: 632px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server">
    </asp:ScriptManager>
   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">         
    <ContentTemplate>
        <table >
            <tr>
                <td>
                    &nbsp;</td>
                <td bgcolor="#1C5AB6">
                    <asp:Label ID="txtSol" runat="server" Font-Bold="True" Font-Names="Arial" 
                        Font-Size="10pt"  style="text-align: left; font-size: 10pt;" 
                        Text="SOLICITUD DE FACTURACION" Width="200px"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="lblRolusu" runat="server" Font-Italic="True" Font-Names="Arial" 
                        Font-Size="8pt" style="text-align: right" Text="-" Visible="False" Width="65px"></asp:Label>
                    &nbsp;<asp:Label ID="lblpais" runat="server" Font-Names="Arial" Font-Size="9pt" 
                        style="text-align: right" Text="-" Visible="False" Width="30px"></asp:Label>
                    &nbsp;<asp:Label ID="lbllogin" runat="server" Font-Names="Arial" Font-Size="9pt" 
                        style="text-align: right" Text="-" Visible="False" Width="30px"></asp:Label>
                    &nbsp;<asp:Label ID="lblCorreousu" runat="server" Font-Names="Arial" Font-Size="8pt" 
                        style="text-align: right" Text="-" Visible="False" Width="30px"></asp:Label>
                    &nbsp;<asp:Label ID="lblMensaje" runat="server" Font-Bold="False" Font-Italic="True" 
                        Font-Names="Arial" Font-Size="8pt"  
                        style="text-align: left; font-size: 10pt; color: #000099;" Width="150px"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <table class="style79">
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="lblCli" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                     style="text-align: right; color: #000000" Text="Cliente" 
                                    Width="60px"></asp:Label>
                            </td>
                            <td class="style107">
                                <asp:Label ID="lblClienteprincipal" runat="server" Font-Italic="True" 
                                    Font-Names="Arial" Font-Size="8pt" ForeColor="#1C5AB6" 
                                    style="color: #1C5AB6; text-align: left; font-weight: 700;" Width="320px"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lblOb" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                     style="text-align: right; color: #000000" Text="Obra" 
                                    Width="40px"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblObraPrincipal" runat="server" Font-Italic="True" 
                                    Font-Names="Arial" Font-Size="8pt" ForeColor="#1C5AB6" 
                                    style="color: #1C5AB6; font-weight: 700;" Width="315px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style106">
                                <asp:Label ID="lblCotizacion" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                    Text="FUP" Width="34px"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblfup" runat="server" Font-Italic="True" Font-Names="Arial" 
                                    Font-Size="8pt" ForeColor="#1C5AB6" Width="80px"></asp:Label>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="LVersion" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                    Text="Version" Width="40px"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LVer" runat="server" Font-Bold="False" Font-Italic="True" 
                                    Font-Names="Arial" Font-Size="8pt" ForeColor="#1C5AB6" Width="40px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                <asp:Label ID="LParte0" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                    Text="Parte" Width="40px"></asp:Label>
                            </td>
                            <td>
                                <span>
                                <asp:DropDownList ID="cboParte" runat="server" AutoPostBack="True" 
                                    Font-Names="Arial" Font-Size="8pt" Width="85px" 
                                    onselectedindexchanged="cboParte_SelectedIndexChanged">
                                </asp:DropDownList>
                                </span>
                            </td>
                            <td style="text-align: right">
                                <asp:Label ID="lblTipo" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                    Width="80px"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblnumeropv" runat="server" Font-Italic="True" 
                                    Font-Names="Arial" Font-Size="8pt" ForeColor="#1C5AB6" Text="Label" 
                                    Width="70px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td width="60"> 
                    &nbsp;</td>
                <td>
                    <asp:Panel ID="pnlDatosVenta" runat="server" BackColor="White" 
                                    Font-Names="Arial" Font-Size="8pt" 
            GroupingText="Datos De Venta" Height="320px" 
                                    Width="890px">
                                    <table>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblCondPago" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    style="text-align: right" Text="Condición De Pago" Width="100px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:ComboBox ID="cboCondPago" runat="server" AutoCompleteMode="Append" 
                                                    AutoPostBack="True"  DropDownStyle="DropDownList" 
                                                    Font-Names="Arial" Font-Size="8pt"  Width="165px">
                                                </asp:ComboBox>
                                            </td>
                                            <td style="text-align: right">
                                                &nbsp;</td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblPaisFactura" runat="server" Font-Names="Arial" 
                                                    Font-Size="8pt" style="text-align: right" Text="Pais A Facturar" Width="80px"></asp:Label>
                                            </td>
                                            <td style="text-align: justify">
                                                <asp:ComboBox ID="cboPaisFactura" runat="server" 
                                                    AutoCompleteMode="Append" AutoPostBack="True" 
                                                     Font-Names="Arial" Font-Size="8pt" 
                                                     onselectedindexchanged="cboPaisFactura_SelectedIndexChanged" 
                                                    TabIndex="4" Width="165px">
                                                </asp:ComboBox>
                                            </td>
                                            <td style="text-align: right">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblCentroOperacion0" runat="server" Font-Names="Arial" 
                                                    Font-Size="8pt" style="text-align: right" Text="Centro De Operación" 
                                                    Width="110px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:ComboBox ID="cboCentOpe" runat="server"  
                                                    Font-Names="Arial" Font-Size="8pt"  TabIndex="1" 
                                                    Width="165px" AutoCompleteMode="Append">
                                                </asp:ComboBox>
                                            </td>
                                            <td style="text-align: right">
                                                &nbsp;</td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblDepartfactura0" runat="server" Font-Names="Arial" 
                                                    Font-Size="8pt" style="text-align: right" Text="Departamento A Facturar" 
                                                    Width="125px"></asp:Label>
                                            </td>
                                            <td style="text-align: justify">
                                                <asp:ComboBox ID="cboDepfact" runat="server" AutoCompleteMode="Append" 
                                                    AutoPostBack="True"  Font-Names="Arial" 
                                                    Font-Size="8pt"  
                                                    onselectedindexchanged="cboDepfact_SelectedIndexChanged" TabIndex="5" 
                                                    Width="165px">
                                                </asp:ComboBox>
                                            </td>
                                            <td style="text-align: right">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblMotivo" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    style="text-align: right" Text="Motivo" Width="60px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:ComboBox ID="cboMotivo" runat="server" AutoCompleteMode="Append" 
                                                    AutoPostBack="True"  DropDownStyle="DropDownList" 
                                                    Font-Names="Arial" Font-Size="8pt"  TabIndex="2" 
                                                    Width="165px">
                                                </asp:ComboBox>
                                            </td>
                                            <td style="text-align: right">
                                                &nbsp;</td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblCiuFactura1" runat="server" Font-Names="Arial" 
                                                    Font-Size="8pt" style="text-align: right" Text="Ciudad A Facturar" 
                                                    Width="100px"></asp:Label>
                                            </td>
                                            <td style="text-align: justify">
                                                <asp:ComboBox ID="cboCiuFact" runat="server" AutoCompleteMode="Append" 
                                                    AutoPostBack="True"  Font-Names="Arial" 
                                                    Font-Size="8pt"  
                                                    onselectedindexchanged="cboCiuFact_SelectedIndexChanged" TabIndex="6" 
                                                    Width="165px">
                                                </asp:ComboBox>
                                            </td>
                                            <td style="text-align: right">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblTipoCliente" runat="server" Font-Names="Arial" 
                                                    Font-Size="8pt" style="text-align: right" Text="Tipo De Cliente" Width="100px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:ComboBox ID="cboTipoCliente" runat="server" 
                                                    AutoCompleteMode="Append" AutoPostBack="True" 
                                                     DropDownStyle="DropDownList" Font-Names="Arial" 
                                                    Font-Size="8pt"  TabIndex="3" Width="165px">
                                                </asp:ComboBox>
                                            </td>
                                            <td style="margin-left: 80px; text-align: right;">
                                                &nbsp;</td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblCliFactura" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    style="text-align: right" Text="Cliente A Facturar" Width="100px"></asp:Label>
                                            </td>
                                            <td colspan="2" style="text-align: justify">
                                                <asp:ComboBox ID="cboClienteFacturar" runat="server" 
                                                    AutoCompleteMode="Append" AutoPostBack="True" 
                                                     Font-Names="Arial" Font-Size="8pt" 
                                                     
                                                    onselectedindexchanged="cboClienteFacturar_SelectedIndexChanged" TabIndex="7" 
                                                    Width="350px">
                                                </asp:ComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td style="margin-left: 80px; text-align: right;">
                                                &nbsp;</td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblNIT" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    style="text-align: right" Text="NIT" Width="40px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblnitfact" runat="server" Font-Italic="True" Font-Names="Arial" 
                                                    Font-Size="8pt" ForeColor="#0000CC" Text="0" Width="190px"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                &nbsp;</td>
                                            <td colspan="3">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblPaiDesp" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    Text="Pais Despacho"></asp:Label>
                                            </td>
                                            <td class="style17">
                                                <asp:ComboBox ID="cboPaiDesp" runat="server" AutoCompleteMode="Append" 
                                                    AutoPostBack="True"  DropDownStyle="DropDownList" 
                                                    Font-Names="Arial" Font-Size="8pt"  
                                                    onselectedindexchanged="cboPaiDesp_SelectedIndexChanged" TabIndex="8" 
                                                    Width="165px">
                                                </asp:ComboBox>
                                            </td>
                                            <td style="margin-left: 80px; text-align: right;">
                                                &nbsp;</td>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label25" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    Text="# De Dias" Width="60px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtDias" runat="server" AutoPostBack="True" Font-Names="Arial" 
                                                    Font-Size="8pt" MaxLength="2" ontextchanged="txtDias_TextChanged" 
                                                    style="text-align: right" TabIndex="12" Width="40px"></asp:TextBox>
                                                &nbsp;
                                                <asp:Label ID="lblDias" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    Text="Dias"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblDepartdesp" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    style="text-align: right" Text="Departamento Despacho" Width="124px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:ComboBox ID="cboDepdesp" runat="server" AutoCompleteMode="Append" 
                                                    AutoPostBack="True"  Font-Names="Arial" 
                                                    Font-Size="8pt"  
                                                    onselectedindexchanged="cboDepdesp_SelectedIndexChanged" TabIndex="9" 
                                                    Width="165px">
                                                </asp:ComboBox>
                                            </td>
                                            <td style="margin-left: 80px; text-align: right;">
                                                &nbsp;</td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblFecDesp0" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    style="text-align: right" Text="Fecha De Despacho" Width="100px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFechaDes" runat="server" AutoPostBack="True" 
                                                    Font-Names="Arial" Font-Size="8pt" ontextchanged="txtFechaDes_TextChanged" 
                                                    style="text-align: right" TabIndex="13" Width="80px"></asp:TextBox>
                                                <asp:MaskedEditExtender ID="txtFechaDes_MaskedEditExtender" runat="server" 
                                                    AutoComplete="False" CultureAMPMPlaceholder="" 
                                                    CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                                    CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                                    CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                                    Mask="99/99/9999" MaskType="Date" TargetControlID="txtFechaDes" 
                                                    UserDateFormat="DayMonthYear">
                                                </asp:MaskedEditExtender>
                                                <asp:CalendarExtender ID="txtFechaDes_CalendarExtender" runat="server" 
                                                    Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaDes">
                                                </asp:CalendarExtender>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblCiuDesp" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    style="text-align: right" Text="Ciudad Despacho" Width="100px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:ComboBox ID="cboCiuDesp" runat="server" AutoCompleteMode="Append" 
                                                    AutoPostBack="True"  DropDownStyle="DropDownList" 
                                                    Font-Names="Arial" Font-Size="8pt"  
                                                    onselectedindexchanged="cboCiuDesp_SelectedIndexChanged" TabIndex="10" 
                                                    Width="165px">
                                                </asp:ComboBox>
                                            </td>
                                            <td style="margin-left: 80px; text-align: right;">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblCliDesp" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    Text="Cliente A Despachar" Width="100px"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:ComboBox ID="cboClienteDespachar" runat="server" 
                                                    AutoCompleteMode="Append" AutoPostBack="True" 
                                                     DropDownStyle="DropDownList" Font-Names="Arial" 
                                                    Font-Size="8pt"  TabIndex="11" Width="350px">
                                                </asp:ComboBox>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="lblDirecciondesp" runat="server" Font-Names="Arial" 
                                                    Font-Size="8pt" style="text-align: right" Text="Dirección De Despacho" 
                                                    Width="125px"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtDireccionDesp" runat="server" Font-Names="Arial" 
                                                    Font-Size="8pt" Height="48px" TabIndex="23" TextMode="MultiLine" Width="367px"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right">
                                                &nbsp;</td>
                                            <td colspan="3">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                    
                    </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Panel ID="pnlDF" runat="server" BackColor="White" Font-Names="Arial" 
                        Font-Size="8pt" GroupingText="Datos De Facturación" Height="100px" 
                        style="margin-bottom: 0px" Width="890px">
                        <table class="style15">
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblDirOfic" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right" Text="Director De Oficina" Width="100px"></asp:Label>
                                </td>
                                <td class="style83">
                                    <asp:ComboBox ID="cboDirector" runat="server" AutoCompleteMode="Append" 
                                        AutoPostBack="True"  DropDownStyle="DropDownList" 
                                        Font-Names="Arial" Font-Size="8pt"  TabIndex="14" 
                                        Width="165px">
                                    </asp:ComboBox>
                                </td>
                                <td class="style101" style="text-align: right">
                                    <asp:Label ID="lblGerente" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="margin-left: 24px; text-align: right;" Text="Gerente Comercial" 
                                        Width="125px"></asp:Label>
                                </td>
                                <td style="text-align: justify">
                                    <asp:ComboBox ID="cboGerente" runat="server" AutoCompleteMode="Append" 
                                        AutoPostBack="True"  Font-Names="Arial" 
                                        Font-Size="8pt"  TabIndex="15" Width="167px">
                                    </asp:ComboBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style49" style="text-align: right">
                                    <asp:Label ID="lblInstPago" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right" Text="Instrumento De Pago" Width="124px"></asp:Label>
                                </td>
                                <td class="style99">
                                    <asp:ComboBox ID="cboInsPago" runat="server" AutoCompleteMode="Append" 
                                        AutoPostBack="True"  DropDownStyle="DropDownList" 
                                        Font-Names="Arial" Font-Size="8pt"  TabIndex="16" 
                                        Width="165px">
                                    </asp:ComboBox>
                                </td>
                                <td class="style101" style="text-align: right">
                                    <asp:Label ID="lblFormaPago" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right" Text="Forma De Pago" Width="100px"></asp:Label>
                                </td>
                                <td style="text-align: justify">
                                    <asp:ComboBox ID="cboFormaPago" runat="server" AutoCompleteMode="Append" 
                                        AutoPostBack="True"  DropDownStyle="DropDownList" 
                                        Font-Names="Arial" Font-Size="8pt"  TabIndex="17" 
                                        Width="167px">
                                    </asp:ComboBox>
                                </td>
                                <td class="style88">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblTDN" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right" Text="TDN" Width="40px"></asp:Label>
                                </td>
                                <td class="style99">
                                    <asp:ComboBox ID="cboTDN" runat="server"  
                                        Font-Names="Arial" Font-Size="8pt"  TabIndex="18" 
                                        Width="165px" AutoCompleteMode="Append" AutoPostBack="True">
                                    </asp:ComboBox>
                                </td>
                                <td class="style101">
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td class="style49" style="text-align: right">
                                    <span class="style82"></span>
                                </td>
                                <td class="style56">
                                    </span>
                                </td>
                                <td class="style101">
                                    &nbsp;</td>
                                <td class="style98">
                                    &nbsp;</td>
                                <td class="style97">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Panel ID="pnlDV" runat="server" BackColor="White" Font-Names="Arial" 
                        Font-Size="8pt" GroupingText="Valores De Venta" Height="240px" 
                        Width="890px">
                        <table style="height: 172px; width: 859px;">
                            <tr>
                                <td style="text-align: right" class="style104">
                                    <asp:Label ID="lblVrVenta" runat="server" Font-Italic="True" Font-Names="Arial" 
                                        Font-Size="8pt" style="text-align: right" Text="Valor De Venta" Width="95px"></asp:Label>
                                </td>
                                <td class="style105">
                                    <asp:Label ID="lblvlrventa" runat="server" Font-Italic="True" 
                                        Font-Names="Arial" Font-Overline="True" Font-Size="8pt" Font-Underline="True" 
                                        ForeColor="#0000CC" style="text-align: right" Text="0" Width="130px"></asp:Label>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblVrComercial" runat="server" Font-Names="Arial" 
                                        Font-Size="8pt" style="text-align: right" Text="Valor Comercial" Width="100px"></asp:Label>
                                </td>
                                <td style="text-align: justify">
                                    <asp:TextBox ID="txtValorComercial" runat="server" AutoPostBack="True" 
                                        Font-Names="Arial" Font-Size="8pt" MaxLength="20" 
                                        ontextchanged="txtValorComercial_TextChanged" style="text-align: right" 
                                        TabIndex="19" Width="128px">0</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right" class="style104">
                                    <asp:Label ID="lblDescuento" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right" Text="% De Descuento" Width="100px"></asp:Label>
                                </td>
                                <td class="style105">
                                    <asp:TextBox ID="txtDscto" runat="server" AutoPostBack="True" 
                                        Font-Names="Arial" Font-Size="8pt" MaxLength="5" 
                                        ontextchanged="txtDscto_TextChanged" style="text-align: right" TabIndex="20" 
                                        Width="40px">0</asp:TextBox>
                                    &nbsp;<asp:Label ID="lblPorc" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        Text="%" Width="20px"></asp:Label>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lvdscto" runat="server" Font-Italic="True" Font-Names="Arial" 
                                        Font-Size="8pt" style="text-align: right" Text="Valor Descuento" Width="120px"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblValorDscto" runat="server" Font-Bold="False" 
                                        Font-Italic="True" Font-Names="Arial" Font-Size="8pt" ForeColor="#0000CC" 
                                        style="text-align: right" Text="0" Width="132px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right" class="style104">
                                    <asp:Label ID="Label21" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right" Text="Razón Descuento" Width="95px"></asp:Label>
                                </td>
                                <td class="style24" colspan="3">
                                    <asp:TextBox ID="txtRazonDescto" runat="server" Font-Names="Arial" 
                                        Font-Size="8pt" Height="42px" style="margin-left: 0px" TabIndex="21" 
                                        TextMode="MultiLine" Width="518px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style104" style="text-align: right">
                                    <asp:Label ID="lblComentarios0" runat="server" Font-Names="Arial" 
                                        Font-Size="8pt" style="text-align: right" Text="Comentarios" Width="70px"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtComentariosSF" runat="server" Font-Names="Arial" 
                                        Font-Size="8pt" Height="48px" TabIndex="24" TextMode="MultiLine" Width="518px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right" class="style104">
                                    <asp:Label ID="lblAlum" runat="server" Font-Bold="False" Font-Italic="True" 
                                        Font-Names="Arial" Font-Size="8pt" Text="Valor Forsa Alum" Width="100px"></asp:Label>
                                </td>
                                <td class="style105" style="text-align: left">
                                    <asp:Label ID="VrAlum" runat="server" Font-Italic="True" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="#0000CC" style="text-align: right" Text="0" 
                                        Width="130px"></asp:Label>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblForsaPlast" runat="server" Font-Italic="True" 
                                        Font-Names="Arial" Font-Size="8pt" Text="Valor Forsa Plast" Width="100px"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:Label ID="LPlast" runat="server" Font-Italic="True" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="#0000CC" Text="0" Width="132px" 
                                        style="text-align: right"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right" class="style104">
                                    <asp:Label ID="lblAcero" runat="server" Font-Italic="True" Font-Names="Arial" 
                                        Font-Size="8pt" Text="Valor Forsa Acero" Width="124px"></asp:Label>
                                </td>
                                <td class="style105" style="text-align: left">
                                    <asp:Label ID="LAcero" runat="server" Font-Bold="False" Font-Italic="True" 
                                        Font-Names="Arial" Font-Size="8pt" ForeColor="#0000CC" 
                                        style="text-align: right" Text="0" Width="130px"></asp:Label>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lbliv1" runat="server" Font-Italic="True" Font-Names="Arial" 
                                        Font-Size="8pt" style="text-align: right" Text="IVA" Width="30px"></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    <asp:Label ID="lblIVA" runat="server" Font-Italic="True" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="#0000CC" style="text-align: right" Text="0" 
                                        Width="132px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right" class="style104">
                                    <asp:Label ID="Label24" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right" Text="Valor Flete" Width="100px"></asp:Label>
                                </td>
                                <td class="style105">
                                    <asp:TextBox ID="txtValorflete" runat="server" AutoPostBack="True" 
                                        Font-Names="Arial" Font-Size="8pt" MaxLength="20" 
                                        ontextchanged="txtValorflete_TextChanged" style="text-align: right" 
                                        TabIndex="22" Width="110px">0</asp:TextBox>
                                </td>
                                <td bgcolor="#F3F3F1" class="style48">
                                    <asp:Label ID="lblVrTotalVenta" runat="server" Font-Bold="True" 
                                        Font-Italic="True" Font-Names="Arial" Font-Size="8pt" style="text-align: right" 
                                        Text="Valor Total Venta" Width="100px"></asp:Label>
                                </td>
                                <td style="text-align: left; margin-left: 80px;">
                                    <asp:Label ID="lblValorTotalVenta" runat="server" Font-Italic="True" 
                                        Font-Names="Arial" Font-Overline="True" Font-Size="8pt" Font-Underline="True" 
                                        ForeColor="#CC0000" Height="16px" style="text-align: right" Text="0" 
                                        Width="132px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <table class="style79">
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td class="style108" style="text-align: center">
                                <asp:CheckBox ID="chkQuitarConfirsf" runat="server" AutoPostBack="True" 
                                    Enabled="False" Font-Names="Arial" Font-Size="8pt" 
                                    oncheckedchanged="chkQuitarConfirsf_CheckedChanged" 
                                    Text="Quitar confirmación SF" TextAlign="Left" Visible="False" />
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td class="style108" style="text-align: right">
                                <asp:Button ID="btnGuardarsf" runat="server" BackColor="#1C5AB6" 
                                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                     onclick="btnGuardarsf_Click" 
                                    onclientclick="return confirm('Esta seguro de guardar la SF?')" TabIndex="25" 
                                    Text="Guardar" Width="100px" style="height: 22px" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnconfsf" runat="server" BackColor="#1C5AB6" 
                                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                     onclick="btnconfsf_Click" 
                                    onclientclick="return confirm('Esta seguro de confirmar la SF?')" 
                                    Text="Confirmar SF" Visible="False" Width="100px" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnconfPV" runat="server" BackColor="#1C5AB6" 
                                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                     onclick="btnconfPV_Click" 
                                    onclientclick="return confirm('Esta seguro de confirmar el PV?')" 
                                    Text="Confirmar PV" Visible="False" Width="100px" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnGenerarSF" runat="server" BackColor="#1C5AB6" 
                                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                     onclick="btnGenerarSF_Click" Text="Generar SF" 
                                    Width="100px" />
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Panel ID="Panel1" runat="server" BackColor="White" Enabled="False" 
                        Font-Names="Arial" Font-Size="8pt" GroupingText="Cuotas" Height="140px" 
                        Width="860px">
                        <table style="height: 78px">
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblCuota" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right" Text="Cuota" Width="50px"></asp:Label>
                                </td>
                                <td>
                                    <asp:ComboBox ID="cboCuota" runat="server" AutoCompleteMode="SuggestAppend" 
                                        AutoPostBack="True"  DropDownStyle="DropDownList" 
                                        Font-Names="Arial" Font-Size="8pt"  
                                        onselectedindexchanged="cboCuota_SelectedIndexChanged" Width="50px">
                                    </asp:ComboBox>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblpagar" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right; margin-bottom: 0px;" Text="A pagar" Width="50px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtaPagar" runat="server" AutoPostBack="True" 
                                        Font-Names="Arial" Font-Size="8pt" MaxLength="14" 
                                        ontextchanged="txtaPagar_TextChanged" Width="90px">0</asp:TextBox>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblporcpagar" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right; margin-bottom: 0px;" Text="% A pagar" Width="60px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtporcpagar" runat="server" AutoPostBack="True" 
                                        Font-Names="Arial" Font-Size="8pt" ontextchanged="txtporcpagar_TextChanged" 
                                        Width="30px">0</asp:TextBox>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblpagado" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right; margin-bottom: 0px;" Text="Pagado" Visible="False" 
                                        Width="50px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtpagado" runat="server" AutoPostBack="True" Enabled="False" 
                                        Font-Names="Arial" Font-Size="8pt" MaxLength="14" 
                                        ontextchanged="txtpagado_TextChanged" Visible="False" Width="90px">0</asp:TextBox>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblfechareal" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right; margin-bottom: 0px;" Text="Fecha Real" Width="80px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFechaReal" runat="server" AutoPostBack="True" 
                                        Font-Names="Arial" Font-Size="8pt" ontextchanged="txtFechaDes_TextChanged" 
                                        style="text-align: right" TabIndex="13" Width="80px"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="txtFechaReal_MaskedEditExtender" runat="server" 
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtFechaReal">
                                    </asp:MaskedEditExtender>
                                    <asp:CalendarExtender ID="txtFechaReal_CalendarExtender" runat="server" 
                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaReal">
                                    </asp:CalendarExtender>
                                </td>
                                <td style="text-align: right">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblcomentario0" runat="server" Font-Names="Arial" 
                                        Font-Size="8pt" style="text-align: right; margin-bottom: 0px;" 
                                        Text="Comentarios" Width="80px"></asp:Label>
                                </td>
                                <td colspan="9">
                                    <asp:TextBox ID="txtComentCuota" runat="server" Font-Names="Arial" 
                                        Font-Size="8pt" Height="28px" TextMode="MultiLine" Width="518px">Sin Comentarios</asp:TextBox>
                                </td>
                                <td style="text-align: right">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td style="text-align: right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td style="text-align: right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td style="text-align: right">
                                    &nbsp;</td>
                                <td style="text-align: right">
                                    <asp:Label ID="lsaldo0" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right; margin-bottom: 0px;" Text="Saldo" Width="50px"></asp:Label>
                                </td>
                                <td style="text-align: right" colspan="2">
                                    <asp:Label ID="lblsaldocuota" runat="server" Font-Italic="True" 
                                        Font-Names="Arial" Font-Size="8pt" ForeColor="#0000CC" 
                                        style="text-align: center" Text="0" Width="200px"></asp:Label>
                                </td>
                                <td style="text-align: right">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td style="text-align: right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td style="text-align: right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td style="text-align: right">
                                    &nbsp;</td>
                                <td style="text-align: right">
                                    &nbsp;</td>
                                <td style="text-align: right">
                                    &nbsp;</td>
                                <td>
                                    <asp:Button ID="btnGuardarCuota" runat="server" BackColor="#1C5AB6" 
                                        BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                         onclick="btnGuardarCuota_Click" 
                                        onclientclick="return confirm('Esta seguro de guardar la cuota?')" 
                                        Text="Guardar" Width="70px" style="height: 22px" />
                                    &nbsp;<asp:Button ID="btnEliminarCuota" runat="server" BackColor="#1C5AB6" 
                                        BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                         onclick="btnEliminarCuota_Click" 
                                        onclientclick="return confirm('Esta seguro de eliminar la cuota?')" 
                                        Text="Eliminar" Width="70px" />
                                </td>
                                <td style="text-align: right">
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:GridView ID="GridView1" runat="server" CellPadding="1" Font-Names="Arial" Font-Size="8pt" 
                        ForeColor="#333333" GridLines="None" PageSize="1" 
                        style="text-align: center" Width="860px" CellSpacing="4">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True"  />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True"  />
                        <PagerStyle BackColor="#284775"  HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:ForsaConnectionString %>" 
                        
                        
                        SelectCommand="SELECT cuo_num, cuo_valor_pagar, cuo_porcentaje, cuo_valor_pagado, cuo_fecha_real, cuo_estado, cuo_comentarios FROM cuota WHERE (cuo_fup_id = @fup) AND (cuo_version = @version)" 
                        ProviderName="<%$ ConnectionStrings:ForsaConnectionString.ProviderName %>">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="lblfup" DefaultValue="0" Name="fup" 
                                PropertyName="Text" />
                            <asp:Parameter DefaultValue="A" Name="version" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Panel ID="pnlRechazoSf" runat="server" Font-Names="Arial" Font-Size="8pt" 
                            GroupingText="Rechazo de Solicitud de Facturacion" Width="860px" 
                            Height="140px" BackColor="White">
                            <table>
                                <tr>
                                    <td class="style74">
                                        &nbsp;</td>
                                    <td class="style72" style="text-align: right">
                                        <asp:CheckBox ID="chkNitRech" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Nit" TextAlign="Left" />
                                    </td>
                                    <td style="text-align: right">
                                        <asp:CheckBox ID="chkDirecFiscalRech" runat="server" Font-Names="Arial" 
                                            Font-Size="8pt" Text="Direccion Fiscal" TextAlign="Left" />
                                    </td>
                                    <td class="style73">
                                        <asp:CheckBox ID="chkTelefRech" runat="server" Font-Names="Arial" 
                                            Font-Size="8pt" Text="Telefono" TextAlign="Left" />
                                    </td>
                                    <td class="style70">
                                        <asp:CheckBox ID="chkCondPagoRech" runat="server" Font-Names="Arial" 
                                            Font-Size="8pt" Text="Condicion de Pago" TextAlign="Left" />
                                    </td>
                                    <td style="text-align: right">
                                        <asp:CheckBox ID="chkTermNegRech" runat="server" Font-Names="Arial" 
                                            Font-Size="8pt" Text="Termino de Negociacion" TextAlign="Left" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style74">
                                        &nbsp;</td>
                                    <td class="style72">
                                        <asp:CheckBox ID="chkRazonsocialRech" runat="server" Font-Names="Arial" 
                                            Font-Size="8pt" style="text-align: left" Text="Razon Social" TextAlign="Left" />
                                    </td>
                                    <td style="text-align: right">
                                        <asp:CheckBox ID="chkValorComercialRech" runat="server" Font-Names="Arial" 
                                            Font-Size="8pt" Text="Valor Comercial" TextAlign="Left" />
                                    </td>
                                    <td class="style73">
                                        &nbsp;</td>
                                    <td class="style70">
                                        &nbsp;</td>
                                    <td style="text-align: right">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblComentarioRech" runat="server" Font-Names="Arial" 
                                            Font-Size="8pt" style="text-align: right; margin-bottom: 0px;" 
                                            Text="Comentarios" Width="50px"></asp:Label>
                                        &nbsp;
                                    </td>
                                    <td colspan="5">
                                        <asp:TextBox ID="txtComentRecha" runat="server" Font-Names="Arial" 
                                            Font-Size="8pt" Height="40px" TextMode="MultiLine" Width="750px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        &nbsp;</td>
                                    <td colspan="5" style="text-align: right">
                                        <asp:Button ID="btnRechazar" runat="server" BackColor="#1C5AB6" 
                                            BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                             onclick="btnRechazar_Click" 
                                            onclientclick="return confirm('Esta seguro de rechazar la SF?')" 
                                            Text="Rechazar" Visible="False" Width="100px" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Accordion ID="Accordion1" runat="server" 
                        ContentCssClass="accordionContent" HeaderCssClass="accordionHeader" 
                        HeaderSelectedCssClass="accordionHeaderSelected" Height="2000px" 
                        Visible="False" Width="939px">
                        <Panes>                           
                            <asp:AccordionPane ID="AcorCarta" runat="server" 
                                ContentCssClass="accordionContent" Font-Bold="False" Font-Names="Arial" 
                                Font-Size="8pt" HeaderCssClass="accordionHeader" 
                                HeaderSelectedCssClass="accordionHeaderSelected">
                                <Header>
                                    <asp:Label ID="lblCarta" runat="server" Text="SOLICITUD FACTURACION"></asp:Label>
                                </Header>
                                <Content>
                                    <rsweb:ReportViewer ID="ReportViewer4" runat="server" Height="1000" 
                                        ShowParameterPrompts="False" Width="900">
                                    </rsweb:ReportViewer>
                                </Content>
                            </asp:AccordionPane>
                        </Panes>
                    </asp:Accordion>
                    
                    </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>



    </ContentTemplate>
</asp:UpdatePanel> 


 <%-- <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
    <ProgressTemplate>
        <div class="overlay" />
        <div class="overlayContent">
            <asp:Label ID="lblEnviando" runat="server" Text="Enviando..." Font-Names="Arial" Font-Size="14pt"></asp:Label>
    <img src="Imagenes/ajax-loader.gif" alt="Loading" height="30" style="text-align: center" width="30"/>
     </div>
    </ProgressTemplate>
    </asp:UpdateProgress>
       --%>
</asp:Content>



