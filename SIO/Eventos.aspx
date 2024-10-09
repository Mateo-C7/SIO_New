<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true"
    CodeBehind="Eventos.aspx.cs" Inherits="SIO.Eventos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        function alerta(numero) {

            alert('Se ha presionado el boton: ' + numero);
        }
    </script>
<%--evalua el estado de la sesion--%>
    <script language="javascript" type="text/javascript">

        var sessionTimeoutWarning = '<%= System.Configuration.ConfigurationSettings.AppSettings["SessionWarning"]%>';
        var sessionTimeout = "<%= Session.Timeout %>";
        var timeOnPageLoad = new Date();
        var sessionWarningTimer = null;
        var redirectToWelcomePageTimer = null;
        //For warning
        var sessionWarningTimer = setTimeout('SessionWarning()',
				parseInt(sessionTimeoutWarning) * 60 * 1000);
        //To redirect to the welcome page
        var redirectToWelcomePageTimer = setTimeout('RedirectToWelcomePage()',
					parseInt(sessionTimeout) * 60 * 1000);

        //Session Warning
        function SessionWarning() {
            //minutes left for expiry
            var minutesForExpiry = (parseInt(sessionTimeout) -
					parseInt(sessionTimeoutWarning));
            var message = "La sesión expirará en " +
		minutesForExpiry + " mins. Desea ampliar la sessión?";

            //Confirm the user if he wants to extend the session
            answer = confirm(message);

            //if yes, extend the session.
            if (answer) {
                var img = new Image(1, 1);
                img.src = 'PedidoVenta.aspx?date=' + escape(new Date());

                //Clear the RedirectToWelcomePage method
                if (redirectToWelcomePageTimer != null) {
                    clearTimeout(redirectToWelcomePageTimer);
                }
                //reset the time on page load
                timeOnPageLoad = new Date();
                sessionWarningTimer = setTimeout('SessionWarning()',
				parseInt(sessionTimeoutWarning) * 60 * 1000);
                //To redirect to the welcome page
                redirectToWelcomePageTimer = setTimeout
		('RedirectToWelcomePage()', parseInt(sessionTimeout) * 60 * 1000);
            }

            //*************************
            //Even after clicking ok(extending session) or cancel button, 
            //if the session time is over. Then exit the session.
            var currentTime = new Date();
            //time for expiry
            var timeForExpiry = timeOnPageLoad.setMinutes(timeOnPageLoad.getMinutes() +
				parseInt(sessionTimeout));

            //Current time is greater than the expiry time
            if (Date.parse(currentTime) > timeForExpiry) {
                alert("La sesión ha expirado. será redirigido a la página de inicio");
                window.location = "index.aspx";
            }
            //**************************
        }

        //Session timeout
        function RedirectToWelcomePage() {
            alert("La sesión ha expirado. será redirigido a la página de inicio");
            window.location = "index.aspx";
        }

        var img = new Image(1, 1);
        img.src = 'PedidoVenta.aspx?date=' + escape(new Date());


        function listaPart(source, eventArgs) {
            document.getElementById('ContentPlaceHolder4_lblIdPart').value = eventArgs.get_value();
        }
    </script>
    <link href="Styles/StyleGeneral.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .CustomComboBoxStyle
        {
            text-align: left;
            top: 0px;
        }
        .CustomComboBoxStyle
        {
            text-align: left;
            top: 0px;
        }
        .CustomComboBoxStyle .ajax__combobox_textboxcontainer input
        {
            background-image: url('http://app.forsa.com.co/SIOMaestros/Imagenes/toolkit-bg.gif');
            border-style: none;
            top: 0px;
        }
        .CustomComboBoxStyle .ajax__combobox_buttoncontainer button
        {
            background-image: url('http://app.forsa.com.co/SIOMaestros/Imagenes/toolkit-arrow.gif');
            border-style: none;
            top: 0px;
        }
        .ajax__combobox_buttoncontainer button
        {
            background-image: url(mvwres://AjaxControlToolkit, Version=4.1.60501.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e/ComboBox.arrow-down.gif);
            background-position: center;
            background-repeat: no-repeat;
            border-color: ButtonFace;
            height: 15px;
            width: 15px;
            top: 0px;
        }
        .sangria
        {
            word-spacing: 10pt;
            font-family: Tahoma;
            font-size: 11pt;
            color: #1C5AB6;
        }
        .comboAlt
        {
            top: 0px;
        }
        .style13
        {
    }
        .style17
        {
            height: 36px;
        }
        .style18
        {
            width: 326px;
            height: 36px;
        }
        .style19
        {
            height: 24px;
        }
        .style31
    {
        height: 36px;
        width: 82px;
    }
    .style32
    {
        width: 82px;
    }
    .style33
    {
        text-align: right;
        width: 82px;
    }
    .style36
    {
        height: 36px;
        width: 144px;
    }
    .style37
    {
        width: 128px;
    }
    .style38
    {
        width: 144px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="Letra">
                <tr>
                    <td>
                        <asp:Label ID="lblObra1" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt"
                            Text="EVENTO" CssClass="sangria" ForeColor="#1C5AB6" Width="120px"></asp:Label>
                        <table style="height: 163px; width: 699px; margin-right: 39px;">
                            <tr>
                                <td style="text-align: right" class="style32">
                                    <br />
                                    Pais :
                                </td>
                                <td style="text-align: left" class="style13">
                                    <asp:ComboBox ID="cboPais" runat="server" AutoPostBack="True" Font-Names="Arial"
                                        Font-Size="8pt" Width="230px" OnSelectedIndexChanged="cboPais_SelectedIndexChanged"
                                        AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList">
                                    </asp:ComboBox>
                                </td>
                                <td style="text-align: left" class="style38">
                                    &nbsp;
                                </td>
                                <td style="text-align: left">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right" class="style32">
                                    Ciudad :
                                </td>
                                <td style="text-align: left" class="style13">
                                    <asp:ComboBox ID="cboCiudad" runat="server" AutoCompleteMode="SuggestAppend" 
                                        AutoPostBack="True" DropDownStyle="DropDownList" Font-Names="Arial" 
                                        Font-Size="8pt" OnSelectedIndexChanged="cboCiudad_SelectedIndexChanged" 
                                        Width="230px">
                                    </asp:ComboBox>
                                </td>
                                <td style="text-align: left" class="style38">
                                    Tipo Origen :
                                </td>
                                <td style="text-align: left">
                                    <asp:ComboBox ID="cboOrigen" runat="server" AutoCompleteMode="SuggestAppend" 
                                        AutoPostBack="True" DropDownStyle="DropDownList" Font-Names="Arial" 
                                        Font-Size="8pt" OnSelectedIndexChanged="cboCiudad_SelectedIndexChanged" 
                                        Width="150px">
                                    </asp:ComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style32" style="text-align: right">
                                    &nbsp;</td>
                                <td class="style13" style="text-align: left">
                                    &nbsp;</td>
                                <td class="style38" style="text-align: left">
                                    &nbsp;</td>
                                <td style="text-align: left">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style32" style="text-align: right">
                                    Nombre :
                                </td>
                                <td class="style13" style="text-align: left">
                                    <asp:TextBox ID="txtNombre" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        Width="320px"></asp:TextBox>
                                </td>
                                <td class="style38" style="text-align: left">
                                    &nbsp;</td>
                                <td style="text-align: left">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style32" style="text-align: right">
                                    Fecha Inicio :
                                </td>
                                <td class="style13" style="text-align: left">
                                    <asp:TextBox ID="txtFechaIni" runat="server" CssClass="TexboxP"></asp:TextBox>
                                    <asp:CalendarExtender ID="txtFechaIniCE" runat="server" Format="dd/MM/yyyy" 
                                        TargetControlID="txtFechaIni">
                                    </asp:CalendarExtender>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Fecha Fin &nbsp;<asp:TextBox ID="txtFechaFin" runat="server" 
                                        CssClass="TexboxP"></asp:TextBox>
                                    <asp:CalendarExtender ID="txtFechaFinCE" runat="server" Format="dd/MM/yyyy" 
                                        TargetControlID="txtFechaFin">
                                    </asp:CalendarExtender>
                                </td>
                                <td class="style38" style="text-align: left">
                                    &nbsp;</td>
                                <td style="text-align: left">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style32" style="text-align: right">
                                    &nbsp;</td>
                                <td class="style13" style="text-align: right">
                                    Participantes :&nbsp;
                                    <asp:TextBox ID="txtPart" runat="server" CssClass="TexboxM"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="txtCliente_AutoCompleteExtender" runat="server" 
                                        CompletionSetCount="15" DelimiterCharacters="" EnableCaching="true" 
                                        Enabled="True" MinimumPrefixLength="1" OnClientItemSelected="listaPart" 
                                        ServiceMethod="GetListaParticipantes" ServicePath="~/WSSIO.asmx" 
                                        TargetControlID="txtPart" UseContextKey="true">
                                    </asp:AutoCompleteExtender>
                                </td>
                                <td class="style38" style="text-align: center">
                                    <asp:Button ID="btnAggPart" runat="server" CssClass="stylebtnAggN" 
                                        OnClick="btnAggPart_Click" Text="&gt;" />
                                    <br />
                                    <br />
                                    <asp:Button ID="btnEliPart" runat="server" CssClass="stylebtnAggN" 
                                        OnClick="btnEliPart_Click" Text="&lt;" />
                                </td>
                                <td style="text-align: left">
                                    <asp:ListBox ID="listPart" runat="server" Font-Size="8pt" Width="200px">
                                    </asp:ListBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style32" style="text-align: right">
                                    &nbsp;</td>
                                <td class="style13" style="text-align: left">
                                    &nbsp;</td>
                                <td class="style38" style="text-align: left">
                                    &nbsp;</td>
                                <td style="text-align: left">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style32" style="text-align: right">
                                    <asp:Label ID="Label1" runat="server" Text="Objetivo:"></asp:Label>
                                </td>
                                <td class="style13" colspan="2" style="text-align: left">
                                    <asp:TextBox ID="txtObjetivo" runat="server" Height="38px" TextMode="MultiLine" 
                                        Width="350px"></asp:TextBox>
                                </td>
                                <td style="text-align: left">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: right" class="style31">
                                    <asp:Label ID="Label2" runat="server" Text="Conclusion:"></asp:Label>
                                </td>
                                <td style="text-align: left" class="style18">
                                    &nbsp;<asp:TextBox ID="txtConclusion" runat="server" Height="38px" 
                                        TextMode="MultiLine" Width="350px"></asp:TextBox>
                                </td>
                                <td style="text-align: left" class="style36">
                                    &nbsp;</td>
                                <td class="style17" style="text-align: left">
                                    &nbsp;&nbsp;&nbsp; &nbsp;
                                    <asp:Button ID="btnGuardar" runat="server" BackColor="#1C5AB6" 
                                        BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                        ForeColor="White" OnClick="btnGuardarsf_Click" 
                                        OnClientClick="return confirm('Esta seguro de guardar el evento?')" 
                                        TabIndex="25" Text="Guardar" Width="67px" />
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnNuevo" runat="server" BackColor="#1C5AB6" 
                                        BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                        ForeColor="White" OnClick="btnNuevo_Click" Text="Nuevo" Width="64px" />
                                </td>
                            </tr>
                            <tr>
                                <td class="style33">
                                    &nbsp;</td>
                                <td colspan="2">
                                    <table>
                                        <tr>
                                            <td class="TexIzq">
                                                <input id="lblIdPart" runat="server" type="hidden" />
                                            </td>
                                            <td>
                                                <br />
                                            </td>
                                            <td class="style8">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="style19">
                                    &nbsp;&nbsp;&nbsp;&nbsp;</td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        CellPadding="4" DataKeyNames="id" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                        Style="text-align: left" Width="860px">
                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="True" />
                                            <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" ReadOnly="True"
                                                SortExpression="id" />
                                            <asp:BoundField DataField="nombre" HeaderText="Nombre" SortExpression="nombre" />
                                            <asp:BoundField DataField="pais" HeaderText="Pais" SortExpression="pais" />
                                            <asp:BoundField DataField="ciudad" HeaderText="Ciudad" SortExpression="ciudad" />
                                            <asp:BoundField DataField="usuario" HeaderText="Usuario" SortExpression="usuario" />
                                            <asp:BoundField DataField="fecha" HeaderText="Fecha" SortExpression="fecha" />
                                            <asp:BoundField DataField="fecha_ini" HeaderText="FechaIni" />
                                            <asp:BoundField DataField="fecha_fin" HeaderText="FechaFin" />
                                        </Columns>
                                        <EditRowStyle BackColor="#999999" />
                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" />
                                        <HeaderStyle BackColor="#1C5AB6" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#1C5AB6" HorizontalAlign="Center" ForeColor="White" />
                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div class="open" />
            <div class="last">
                <asp:Label ID="lblEnviando" runat="server" Text="Cargando..." Font-Names="Arial"
                    Font-Size="14pt"></asp:Label>
                <img src="Imagenes/ajax-loader.gif" alt="Loading" height="30" style="text-align: center"
                    width="30" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
