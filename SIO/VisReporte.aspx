<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="VisReporte.aspx.cs" Inherits="SIO.ReporteVis" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="CalendarioMetro/lib/Chart.js" type="text/javascript"></script>
    <link href="Styles/StyleGeneral.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function viaSeleccionada(source, eventArgs) {
            document.getElementById('<%= lblIdCliente.ClientID %>').value = eventArgs.get_value();
        }
        function verDetVisResp() {
            window.open('VisReporteDos.aspx', this.target, 'top=50, left=30, toolbar=no, location=no, status=no, menubar=no, scrollbars=yes, resizable=no, width=1100, height=700')//width=1170
        }
        function abrirDes() {
            window.open('DescargarDocs.aspx', this.target, 'top=50, left=30, toolbar=no, location=no, status=no, menubar=no, scrollbars=yes, resizable=no, width=10, height=30')
        }
        function popupDoc() {
            $('#popdoc').fadeIn('slow');
        }
        function cerrarPopupDoc() {
            $('#popdoc').fadeOut('slow');
        }
        function calendarShown(sender, e) {
            sender._switchMode("months", true);
        }
    </script>
    <style type="text/css">
        #tablaResumido
        {
            height: 213px;
            width: 690px;
        }
        #popup
        {
            left: 497px;
            position: absolute;
            top: 280px;
            width: 18%;
            z-index: 1002;
        }
        #popup2
        {
            left: 497px;
            position: absolute;
            top: 280px;
            width: 25%;
            z-index: 1002;
        }
        #popup3
        {
            left: 355px;
            position: absolute;
            top: 216px;
            width: 41%;
            z-index: 1002;
        }
        #popdoc
        {
            left: 497px;
            position: absolute;
            top: 280px;
            overflow: auto;
            width: 23%;
            z-index: 1002;
            height: 204px;
        }
        .content-popup
        {
            margin-top: 47px;
            padding: 10px;
            width: 777px;
            min-height: 62px;
            border-radius: 25px;
            background-color: #FFFFFF;
            box-shadow: 0 2px 5px #666666;
            margin-left: auto;
            margin-right: auto;
            margin-bottom: 0px;
        }
        .content-popup2
        {
            margin-top: 50px;
            padding: 10px;
            width: 777px;
            min-height: 112px;
            border-radius: 4px;
            background-color: #FFFFFF;
            box-shadow: 0 2px 5px #666666;
            margin-left: auto;
            margin-right: auto;
            margin-bottom: 0px;
        }
        .content-popup3
        {
            margin-top: 50px;
            padding: 10px;
            width: 777px;
            min-height: 112px;
            border-radius: 4px;
            background-color: #FFFFFF;
            box-shadow: 0 2px 5px #666666;
            margin-left: auto;
            margin-right: auto;
            margin-bottom: 0px;
        }
        .close
        {
            position: absolute;
            right: 15px;
        }
        .content-popup4
        {
            margin-top: 47px;
            padding: 10px;
            width: 777px;
            min-height: 62px;
            border-radius: 25px;
            background-color: #FFFFFF;
            box-shadow: 0 2px 5px #666666;
            margin-left: auto;
            margin-right: auto;
            margin-bottom: 0px;
        }
        #chartdiv
        {
            height: 137px;
        }
        .style67
        {
            width: 404px;
        }
        #grafi
        {
            height: 249px;
        }
        .style68
        {
            height: 19px;
        }
        #tablaDetallado
        {
            height: 180px;
        }
        .styleTd
        {
            height: 36px;
            width: 46px;
        }
        .style72
        {
            width: 189px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelGeneral" runat="server" Height="144px" Width="691px" CssClass="Letra"
                GroupingText=" ">
                <div style="font-weight: bold;">
                    REPORTE</div>
                <table style="height: 102px; width: 690px; margin-bottom: 0px;">
                    <tr>
                        <td class="TexDer">
                            Responsable Visita :
                        </td>
                        <td class="TexIzq">
                            <asp:DropDownList ID="cboGerComercial" runat="server" AutoPostBack="true" CssClass="ComboG">
                            </asp:DropDownList>
                        </td>
                        <td class="TexIzq">
                            Fecha Inicial :
                            <asp:TextBox ID="txtFechaIni" runat="server" CssClass="TexboxP"></asp:TextBox>
                            <asp:CalendarExtender ID="txtFechaIniCE" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaIni">
                            </asp:CalendarExtender>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Fecha Fin :
                            <asp:TextBox ID="txtFechaFin" runat="server" CssClass="TexboxP"></asp:TextBox>
                            <asp:CalendarExtender ID="txtFechaFinCE" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaFin">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TexDer">
                            Cliente :
                        </td>
                        <td class="style72">
                            <asp:TextBox ID="txtCliente" runat="server" CssClass="TexboxG"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="TextBox1_AutoCompleteExtender" runat="server" CompletionSetCount="15"
                                DelimiterCharacters="" EnableCaching="true" Enabled="True" MinimumPrefixLength="1"
                                OnClientItemSelected="viaSeleccionada" ServiceMethod="GetListaClientes" ServicePath="~/WSSIO.asmx"
                                TargetControlID="txtCliente" UseContextKey="true">
                            </asp:AutoCompleteExtender>
                            <input id="lblIdCliente" runat="server" type="hidden" />
                        </td>
                        <td class="TexDer" rowspan="3">
                            <asp:Button ID="btnDesExcel" runat="server"  Text="Descargar Excel"  OnClick="btnDesExcel_Click" CssClass="Botones" Width="98px" /> 
                            <br />
                            <asp:Button ID="btnVerReporte" runat="server" OnClick="btnVerReporte_Click" Text="Ver Reporte" CssClass="Botones"  />&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="TexDer">
                            Nivel :
                        </td>
                        <td class="TexIzq">
                            <asp:DropDownList ID="cboNivel" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboNivel_SelectedIndexChanged"
                                CssClass="ComboM">
                                <asp:ListItem>Seleccionar</asp:ListItem>
                                <asp:ListItem>Resumido</asp:ListItem>
                                <asp:ListItem>Detallado</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="TexDer">
                            <asp:Label ID="lblTipo" runat="server" Text="Tipo de Visita : "></asp:Label>
                        </td>
                        <td class="TexIzq">
                            <asp:DropDownList ID="cboTipoVis" runat="server" AutoPostBack="true" CssClass="ComboM">
                                <asp:ListItem>Seleccionar</asp:ListItem>
                                <asp:ListItem>Todas</asp:ListItem>
                                <asp:ListItem>Realizadas</asp:ListItem>
                                <asp:ListItem>No Realizadas</asp:ListItem>
                                <asp:ListItem>Canceladas</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <table runat="server" id="tablaResumido">
                <tr>
                    <td>
                        <asp:Panel ID="PanelTablaResumida" runat="server" Style="margin-bottom: 0px" GroupingText="Informacion de Visitas Resumida"
                            Width="495px" Height="175px" ScrollBars="Auto">
                            <asp:GridView ID="grdResumido" runat="server" BackColor="White" DataKeyNames="usuario"
                                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
                                Width="433px" AutoGenerateColumns="False" Font-Size="9pt" OnRowCommand="grdResumido_RowCommand"
                                Height="35px">
                                <AlternatingRowStyle BackColor="Gainsboro" />
                                <Columns>
                                    <asp:TemplateField HeaderText="nom" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="PriceLabel" runat="server" Text='<%# Bind("usuario") %>' Font-Size="11pt">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:ButtonField DataTextField="nomUsuario" ButtonType="Link" CommandName="Respon"
                                        HeaderText="Responsable" Text="Button" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="100pt" />
                                    </asp:ButtonField>
                                    <asp:BoundField DataField="visPlan" HeaderText="Agendadas" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="visEje" HeaderText="Cerradas" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="diferencia" HeaderText="Diferencia" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="porceCer" HeaderText="%" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="visCan" HeaderText="Canceladas" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="porceCan" HeaderText="%" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right" />
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <HeaderStyle BackColor="#1C5AB6" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#000065" />
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel2" runat="server" CssClass="Letra">
                            <div id='canvas-holder' style="height: 187px; width: 674px;">
                                <div style="width: 197px">
                                    <table style="height: 31px; width: 191px">
                                        <tr>
                                            <td class="style68">
                                                &nbsp;Aprobadas
                                            </td>
                                            <td class="style68">
                                                <div style="width: 42px; height: 1px; border-top: 14px solid rgb(107, 157, 250)">
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;Cerradas
                                            </td>
                                            <td>
                                                <div style="width: 42px; height: 2px; border-top: 14px solid rgb(233, 226, 37)">
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;Canceladas
                                            </td>
                                            <td>
                                                <div style="width: 42px; height: 2px; border-top: 14px solid rgb(224, 46, 53)">
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <canvas id='chart-area3' width='600' height='300'></canvas>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <table runat="server" id="tablaDetallado">
                <tr>
                    <td>
                        <asp:Panel ID="PanelTablaDetalla" runat="server" Style="margin-bottom: 0px" GroupingText="Informacion de Visitas Detallada"
                            Width="737px" Height="272px" ScrollBars="Auto">
                            <asp:GridView ID="grdDetallado" runat="server" BackColor="White" DataKeyNames="numVis"
                                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
                                Width="715px" AutoGenerateColumns="False" Height="35px" Font-Size="9pt" OnRowCommand="grdDetallado_RowCommand">
                                <AlternatingRowStyle BackColor="Gainsboro" />
                                <Columns>
                                    <asp:BoundField DataField="numVis" HeaderText="N°" ItemStyle-HorizontalAlign="Right"
                                        HeaderStyle-HorizontalAlign="Right">
                                        <ItemStyle Width="8pt" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="cliente" HeaderText="Cliente" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="120pt" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="pais" HeaderText="Pais" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="70pt" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ciudad" HeaderText="Ciudad" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Width="70pt" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="realizada" HeaderText="Realizada" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="center">
                                        <ItemStyle Width="8pt" />
                                    </asp:BoundField>
                                    <asp:ButtonField CommandName="notAgen" DataTextField="notAgen" HeaderText="Agenda"
                                        Text="Button" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
                                        <ItemStyle Width="20pt" />
                                    </asp:ButtonField>
                                    <asp:ButtonField ButtonType="Link" HeaderText="Ejecucion" Text="Button" CommandName="datosEje"
                                        DataTextField="datosEje" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
                                        <ItemStyle Width="20pt" />
                                    </asp:ButtonField>
                                    <asp:ButtonField CommandName="datosCierre" DataTextField="datosCierre" HeaderText="Cierre"
                                        Text="Button" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
                                        <ItemStyle Width="20pt" />
                                    </asp:ButtonField>
                                    <asp:ButtonField CommandName="datosDoc" DataTextField="datosDoc" HeaderText="Documentos"
                                        Text="Button" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
                                        <ItemStyle Width="20pt" />
                                    </asp:ButtonField>
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <HeaderStyle BackColor="#1C5AB6" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#000065" />
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="style67">
                        <div id='Div1' style="height: 187px; width: 750px;">
                            <div style="width: 194px">
                                <table style="height: 31px; width: 191px">
                                    <tr>
                                        <td class="style68">
                                            &nbsp;Realizadas
                                        </td>
                                        <td class="style68">
                                            <div style="width: 42px; height: 1px; border-top: 14px solid rgb(11, 130, 231)">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;No Realizadas
                                        </td>
                                        <td>
                                            <div style="width: 42px; height: 2px; border-top: 14px solid rgb(227, 232, 96)">
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;Canceladas
                                        </td>
                                        <td>
                                            <div style="width: 42px; height: 2px; border-top: 14px solid rgb(224, 46, 53)">
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <canvas id="chart-area" width="300" height="300"></canvas>
                        </div>
                    </td>
                </tr>
            </table>
            <div id="popup" class="content-popup" style="display: none;">
            </div>
            <div id="popup2" class="content-popup2" style="display: none;">
            </div>
            <div id="popup3" class="content-popup3" style="display: none;">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="fondoOsc" class="fondoOscuro">
    </div>
    <div id="popdoc" class="content-popup4" style="display: none;">
        <div class="close">
            <a href="#" id="close" onclick="cerrarPopupDoc();">
                <img alt="Cerrar" src="iconosMetro/close.png" /></a></div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server" Font-Names="Arial" Height="213px" Width="273px">
                    <asp:GridView ID="grdDoc" runat="server" AutoGenerateColumns="False" DataKeyNames="ruta"
                        Width="305px" CellPadding="4" ForeColor="#333333" GridLines="None" Height="16px"
                        OnRowCommand="grdDoc_RowCommand">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:BoundField DataField="ruta" HeaderText="Ruta" Visible="false" />
                            <asp:BoundField DataField="nombre" HeaderText="Nombre">
                                <ItemStyle Width="180px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Descargar" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center"
                                HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton2" CommandName="datosDoc" runat="server" Height="33px"
                                        ImageUrl="~/iconosMetro/descargar1.png" Width="36px" OnClick="ImageButton2_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div class="overlay" />
            <div class="overlayContent">
                <asp:Label ID="lblEnviando" runat="server" Text="Cargando..." Font-Names="Arial"
                    Font-Size="14pt"></asp:Label>
                <img src="Imagenes/ajax-loader.gif" alt="Loading" height="30" style="text-align: center"
                    width="30" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
