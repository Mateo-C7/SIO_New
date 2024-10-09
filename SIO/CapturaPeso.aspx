<%@ Page Title="CapturaPeso" Language="C#" MasterPageFile="~/GeneralGrande.Master" AutoEventWireup="true"
    CodeBehind="CapturaPeso.aspx.cs" Inherits="SIO.CapturaPeso" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.4.1.min.js"></script>
    <script type="text/javascript">
        function imprimir() {
            window.open('VistaP.aspx', this.target, 'top=100, left=100, toolbar=no, location=no, status=no, menubar=no, scrollbars=yes, resizable=no, width=700, height=500')
        }
        function OnEnter() {
            var pesaje = document.getElementById('<%= txtPeso.ClientID %>').value;
            if (pesaje == "-1") {
                applet(1);
                document.getElementById('<%= txtPeso.ClientID %>').value = "";
                document.getElementById('<%= txtPeso.ClientID %>').focus();
            } else if (document.getElementById('<%= txtPeso.ClientID %>') != null) {
                //window.setTimeout(function () { document.getElementById('<%= txtCodigoEstiba.ClientID %>').focus(); }, 900);
                // este esta activo: window.setTimeout(function () {  document.getElementById('<%= txtCedulaUsuario.ClientID %>').focus(); }, 900);
            }
        }
        function data(event) {
            var tecla = window.event.keyCode;
            if (tecla == "13") {
                var peso = document.getElementById('<%= txtPeso.ClientID %>').value;
                if (peso != "") {
                    applet(0);
                    //window.setTimeout(function () { document.getElementById('<%= txtCodigoEstiba.ClientID %>').focus(); }, 1700);
                    window.setTimeout(function () {  document.getElementById('<%= txtCedulaUsuario.ClientID %>').focus(); }, 1700);
                } else {
                    OnEnter();
                }
            }
        }
        function applet(valor) {
            var idapplet = document.getElementById('idapplet');
            if (valor == 1) {
                idapplet.iniciabat();                
            } else {
                idapplet.cerrarBat();
            }
        }

        function OnCheckedChangedMethod() {
            window.setTimeout(function () {  document.getElementById('<%= txtCedulaUsuario.ClientID %>').focus(); }, 500);
        }
    </script>
    <script type="text/javascript">
        function KeyPressAncho() {
            if (window.event.keyCode == 13) {
                window.setTimeout(function () { document.getElementById('<%= txtLargo.ClientID %>').focus(); }, 1000);
            }
        }
        function KeyPressLargo() {
            if (window.event.keyCode == 13) {
                window.setTimeout(function () { document.getElementById('<%= txtAlto.ClientID %>').focus(); }, 1000);
            }
        }
        function KeyPressAlto() {
            if (window.event.keyCode == 13) {
                window.setTimeout(function () { document.getElementById('<%= btnGuardar.ClientID %>').focus(); }, 1000);
            }
        }
        function KeyPressGuardar() {
            if (window.event.keyCode == 13) {
                //window.setTimeout(function () { document.getElementById('<%= txtCodigoEstiba.ClientID %>').focus(); }, 3000);
                
                 window.setTimeout(function () {  document.getElementById('<%= txtCedulaUsuario.ClientID %>').focus(); }, 3000);
            }
        }
        function puntoR(field) {
            field.value = field.value.replace('.', ',');
        }
    </script>
    <script src="Scripts/ScriptMetrolink.js" type="text/javascript"></script>
    <style type="text/css">
        .watermarked
        {
            padding: 1px 0 0 1px;
            border: 0px solid #BEBEBE;
            background-color: white;
            color: Gray;
            font-family: Arial;
            font-weight: lighter;
        }
        .CustomComboBoxStyle .ajax__combobox_buttoncontainer button
        {
            background-image: url('http://app.forsa.com.co/SIOMaestros/Imagenes/toolkit-arrow.gif');
            border-style: none;
        }
        .CustomComboBoxStyle .ajax__combobox_textboxcontainer input
        {
            background-image: url('http://app.forsa.com.co/SIOMaestros/Imagenes/toolkit-bg.gif');
            border-style: none;
        }
        .CustomComboBoxStyle .ajax__combobox_itemlist li
        {
            color: Black;
            font-size: 8pt;
            font-family: Arial;
            background-color: #EBEBEB;
        }
        .A:hover
        {
            background: white;
        }
        .botonsio:hover
        {
            color: white;
            background: blue;
        }
        .center
        {
            font-family: Arial;
            font-size: 8pt;
            text-align: Center;
        }
        .sangria
        {
            word-spacing: 10pt;
            font-family: Tahoma;
            font-size: 11pt;
            color: #1C5AB6;
        }
        .style32
        {
            width: 98%;
            height: 223px;
        }
        .botonsio
        {
        }
        #txtPeso
        {
            width: 58px;
        }
        #txtAncho
        {
            width: 80px;
        }
        #txtLargo
        {
            width: 83px;
        }
        .style278
        {
            width: 741px;
            height: 44px;
            font-size: 22px;
            background-color: #1C5AB6;
            color: White;
            font-family: Arial;
        }
        .style279
        {
            width: 741px;
            height: 44px;
            font-size: 22px;
            color: Black;
            font-family: Arial;
        }
        .style286
        {
            width: 811px;
            height: 44px;
            font-size: 22px;
            color: Black;
            font-family: Arial;
        }
        .style287
        {
            width: 729px;
            height: 44px;
            font-size: 22px;
            color: Black;
            font-family: Arial;
        }
        .style291
        {
            width: 707px;
            height: 44px;
            font-size: 22px;
            color: Black;
            font-family: Arial;
        }
        .style292
        {
            width: 686px;
            height: 44px;
            font-size: 22px;
            color: Black;
            font-family: Arial;
        }
        .auto-style1 {
            height: 152px;
        }
        .auto-style2 {
            width: 209px;
        }
        .auto-style3 {
            width: 191px;
        }
        .style296
        {
            width: 194px;
        }
        .auto-style4 {
            width: 86px;
            height: 31px;
        }
        .auto-style5 {
            width: 185px;
            height: 31px;
        }
        .auto-style6 {
            width: 159px;
        }
        .style297
        {
            width: 17px;
            height: 31px;
        }
        .style298
        {
            width: 116px;
            height: 31px;
        }
        .style299
        {
            width: 345px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">       
         <ContentTemplate> 
         <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="300000"></asp:Timer>
            <%--<asp:Timer ID="Timer2" runat="server" OnTick="Timer2_Tick" Interval="2000"></asp:Timer>--%>            
            <applet id="idapplet" code="LeerPuerto.class" archive="leerPuerto.jar" width="1"
                height="1" >
            </applet><asp:CheckBox ID="chkManual" runat="server" Font-Names="Arial" 
                 Font-Size="8pt" Text="Peso Manual" />
&nbsp;<table>
                <tr>
                    <td class="style297">
                        <br />
                        <asp:Label ID="lblClienteTitulo" runat="server" CssClass="sangria" Font-Bold="True"
                            ForeColor="#1C5AB6" Text="CAPTURA PESO" Font-Names="Arial" Width="160px">
                        </asp:Label>
                    </td>
                    <td class="style298">
                        <asp:Button ID="btnReporte0" runat="server" Font-Bold="True"
                            ForeColor="#1C5AB6" Text="Consulta Pallets" Font-Names="Arial" 
                            Width="150px" OnClick="btnReporte0_Click">
                        </asp:Button>
                    </td>
                    <td class="style299">
                        <asp:Button ID="btnReporte" runat="server" Font-Bold="True"
                            ForeColor="#1C5AB6" Text="Solic Devolucion" Font-Names="Arial" 
                            Width="150px" OnClick="btnReporte_Click">
                        </asp:Button> 
                        &nbsp; 
                        <asp:Image ID="imgGif" runat="server" BackColor="Red" ImageAlign="AbsMiddle" 
                            ImageUrl="Imagenes/gifLogistica.gif" Visible="true" />                        
                    </td>
                    <%--<td class="style281">
                        <asp:Panel ID="Panel7" runat="server" DefaultButton="Button7">
                            <asp:CheckBox runat="server" ID="chkDevolver" Text="Devolver Pallet" Width="200px" OnCheckedChanged="chkDevolver_CheckedChanged"/>
                            <asp:Button ID="Button7" runat="server" Text="Button" Style="display: none" />
                        </asp:Panel>
                    </td>--%>
                    <td class="style296">
                        <input type="checkbox" id="chkDevolver" name = "chkDevolver" Width="160px" 
                            Checked="False" runat="server" onfocus="OnCheckedChangedMethod()" 
                            title="Devolver Pallet">Devolver Pallet </input>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style1" colspan="4">
                        <asp:Panel ID="pnlDG" runat="server" BackColor="White" Font-Bold="True" Font-Names="Arial"
                            Font-Size="8pt" GroupingText=" " Height="400px" Width="1210px">
                            <table>
                                <tr>
                                    <td>
                                        <table>
                                            <tr class="style278">
                                                <td class="auto-style2" style="width: 200px">
                                                    <asp:Label ID="lblCedulaUsuario" runat="server" Font-Bold="False" Text="Carnet Usuario:"
                                                        Width="200px" Height="30px" Style="text-align: right"></asp:Label>
                                                </td>
                                                <td class="auto-style3">
                                                    <asp:Panel ID="Panel6" runat="server" DefaultButton="Button6">
                                                        <asp:TextBox ID="txtCedulaUsuario" runat="server" Width="190px"
                                                            Font-Size="20" BackColor="#1C5AB6" ForeColor="White"></asp:TextBox>
                                                        <asp:Button ID="Button6" runat="server" Text="Button" Style="display: none" OnClick="txtCedulaUsuario_TextChanged" />
                                                    </asp:Panel>
                                                </td>
                                                <td style="width: 500px">Nombre:
                                        <asp:Label ID="lblNombreUsuario" runat="server" Font-Bold="false"></asp:Label>
                                                </td>
                                                <td>Código:
                                        <asp:Label ID="lblCodigoUsuario" runat="server"></asp:Label>
                                                </td>
                                                <input id="lblFechaLog" runat="server" type="hidden" />
                                                <input id="lblCorreo" runat="server" type="hidden" />

                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table class="style32">
                                            <tr class="style278">
                                                <td class="style278">
                                                    <asp:Label ID="lblCodigoEstiba" runat="server" Font-Bold="False" Text="Codigo Remision:"
                                                        Width="199px" Height="30px" Style="text-align: right"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Panel ID="Panel1" runat="server" DefaultButton="Button1">
                                                        <asp:TextBox ID="txtCodigoEstiba" runat="server" Width="190px"
                                                            Font-Size="20" BackColor="#1C5AB6" ForeColor="White"></asp:TextBox>
                                                        <asp:Button ID="Button1" runat="server" Text="Button" Style="display: none" OnClick="Button1_Click1" />
                                                    </asp:Panel>
                                                </td>
                                                <td class="style278" colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chbImpEtiqueta" runat="server"
                                                    Checked="true" Width="170px" Text="        Imprimir?" Height="22px" />
                                                </td>
                                                <td colspan="4">&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="lblTipoP" runat="server" Text="Tipo de Pallet: "></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTipoAcAl" runat="server"
                                                        Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style279">
                                                    <asp:Label ID="lblNumOrden" runat="server" Font-Bold="False" Text="Orden No.:" Width="197px"
                                                        Style="text-align: right"></asp:Label>
                                                </td>
                                                <td class="style279">
                                                    <asp:Label ID="lblNumOrden1" runat="server" Width="148px"></asp:Label>
                                                </td>
                                                <td class="style286">
                                                    <asp:Label ID="lblNumPallet" runat="server" Font-Bold="False" Text="Pallet No.:"
                                                        Width="124px" Height="22px" Style="margin-left: 0px; text-align: right;"></asp:Label>
                                                </td>
                                                <td class="style287">
                                                    <asp:Label ID="lblNumPallet1" runat="server" Width="80px"></asp:Label>
                                                     <asp:Label ID="lblNumP" visible = "false" runat="server" Width="8px"></asp:Label>
                                                
                                                </td>
                                                <td class="style279">
                                                    <asp:Label ID="lblPais" runat="server" Font-Bold="False" Text="Pais:" Width="101px"
                                                        Style="text-align: right"></asp:Label>
                                                </td>
                                                <td class="style291">
                                                    <asp:Label ID="lblPais1" runat="server" Width="134px" Enabled="False"></asp:Label>
                                                </td>
                                                <td class="style292">
                                                    <asp:Label ID="lblCiudad" runat="server" Font-Bold="False" Text="Ciudad:" Width="149px"
                                                        Style="text-align: right"></asp:Label>
                                                </td>
                                                <td class="style279">
                                                    <asp:Label ID="lblCiudad1" runat="server" Width="165px" Enabled="False"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style278">
                                                    <asp:Label ID="lblPeso" runat="server" Font-Bold="False" Text="Peso(Kgs):" Width="192px"
                                                        Style="text-align: right"></asp:Label>
                                                </td>
                                                <td class="style278" colspan="7">
                                                    <asp:Panel ID="Panel2" runat="server" DefaultButton="Button2" Width="217px">
                                                        <asp:TextBox ID="txtPeso" runat="server" Width="130px" Style="text-align: right"
                                                            onfocus="OnEnter();" onkeypress="data(event);" OnTextChanged="txtPeso_TextChanged"
                                                            Font-Size="20" BackColor="#1C5AB6" ForeColor="White">0</asp:TextBox>
                                                        <asp:Button ID="Button2" runat="server" Text="Button2" Style="display: none" />
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style279">
                                                    <asp:Label ID="lblAncho" runat="server" Font-Bold="False" Text="Ancho(Mts):" Width="194px"
                                                        Style="text-align: right"></asp:Label>
                                                </td>
                                                <td class="style279">
                                                    <asp:Panel ID="Panel3" runat="server" DefaultButton="Button3" Width="172px">
                                                        <input id="txtAncho" type="text" style="width: 90px; text-align: right; height: 29px; font-size: 22px; color: Black; font-family: Arial;"
                                                            runat="server" onfocus="puntoR(this)"
                                                            onblur="puntoR(this)" />
                                                        <asp:Button ID="Button3" runat="server" Text="Button3" Style="display: none" OnClick="Button3_Click" />
                                                    </asp:Panel>
                                                </td>
                                                <td class="style286">
                                                    <asp:Label ID="lblLargo" runat="server" Font-Bold="False" Height="24px" Text="Largo (Mts):"
                                                        Width="125px" Style="text-align: right"></asp:Label>
                                                </td>
                                                <td class="style287">
                                                    <asp:Panel ID="Panel4" runat="server" DefaultButton="Button4">
                                                        <input id="txtLargo" type="text" runat="server" style="width: 90px; text-align: right; height: 29px; font-size: 22px; color: Black; font-family: Arial;"
                                                            onblur="puntoR(this)"
                                                            onfocus="puntoR(this)" />
                                                        <asp:Button ID="Button4" runat="server" Text="Button4" Style="display: none" OnClick="Button4_Click" />
                                                    </asp:Panel>
                                                </td>
                                                <td class="style279">
                                                    <asp:Label ID="lblAlto" runat="server" Font-Bold="False" Height="27px" Text="Alto (Mts):"
                                                        Width="103px" Style="text-align: right"></asp:Label>
                                                </td>
                                                <td class="style291">
                                                    <asp:Panel ID="Panel5" runat="server" DefaultButton="Button5" Width="100px">
                                                        <input id="txtAlto" type="text" runat="server" style="width: 90px; text-align: right; height: 29px; font-size: 22px; color: Black; font-family: Arial;"
                                                            onblur="puntoR(this)"
                                                            onfocus="puntoR(this)" />
                                                        <asp:Button ID="Button5" runat="server" Text="Button5" Style="display: none" OnClick="Button5_Click" />
                                                    </asp:Panel>
                                                </td>
                                                <td class="style292">
                                                    <asp:Label ID="lblVolumen" runat="server" Font-Bold="False" Height="26px" Text="Volumen (M3):"
                                                        Width="148px" Style="text-align: right"></asp:Label>
                                                </td>
                                                <td class="style279">
                                                    <input id="txtVolumen" type="text" runat="server" onblur="puntoR(this)" onfocus="puntoR(this)"
                                                        style="width: 90px; text-align: right; height: 29px; font-size: 22px; color: Black; font-family: Arial;" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnGuardar" runat="server" CssClass="botonsio"
                                                        Font-Bold="True" OnClick="btnGuardar_Click" Text="Guardar"
                                                        Width="113px" Height="20px" ForeColor="White" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="PanelReporte" runat="server" CssClass="Letra" Font-Names="Arial" Width="1200px" Visible="true">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <rsweb:ReportViewer ID="reportePeso" runat="server" 
                                                            ShowParameterPrompts="False" BackColor="White" Width="1200" 
                                                            AsyncRendering="true" ShowBackButton="False" ShowCredentialPrompts="False" 
                                                            ShowDocumentMapButton="False" ShowExportControls="False" 
                                                            ShowFindControls="False" ShowPageNavigationControls="False" 
                                                            ShowPrintButton="False" ShowPromptAreaButton="False" ShowRefreshButton="False" 
                                                            ShowReportBody="False" ShowToolBar="False" ShowWaitControlCancelLink="False" 
                                                            ShowZoomControl="False">
                                                        </rsweb:ReportViewer>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td class="auto-style1">
                        &nbsp;</td>
                </tr>
                 </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        if (document.getElementById('<%= txtCodigoEstiba.ClientID %>').value != "") {
            if (document.getElementById('<%= txtPeso.ClientID %>').value != "") {
                document.getElementById('<%= txtPeso.ClientID %>').focus();
            }
        }
    </script>
</asp:Content>