<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VisReporteDos.aspx.cs"
    Inherits="SIO.ReporteVisDos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE10" />
    <script type="text/javascript">
        function abrirDes() {
            window.open('DescargarDocs.aspx', this.target, 'top=50, left=30, toolbar=no, location=no, status=no, menubar=no, scrollbars=yes, resizable=no, width=10, height=30')
        }
        function popupDoc() {
            $('#popdoc').fadeIn('slow');
        }
        function cerrarPopupDoc() {
            $('#popdoc').fadeOut('slow');
        }
    </script>
    <link href="default.css" rel="stylesheet" type="text/css" media="all" />
    <title></title>
    <link href="Styles/StyleGeneral.css" rel="stylesheet" type="text/css" />
    <script src="CalendarioMetro/lib/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="CalendarioMetro/lib/Chart.js" type="text/javascript"></script>
    <style type="text/css">
        body
        {
            margin: 0px;
            padding: 0px; /**background: #D6D6D6 url(images/body-bg.png) repeat; */
            background-color: #F2F2F2;
            font-family: 'Arial' , serif;
            font-size: 13px;
            color: #837F7F;
            text-align: left; /*height: 5000px;*/
        }
        #tablaDetallado
        {
            height: 221px;
            width: 800px;
        }
        #popup
        {
            left: 191px;
            position: absolute;
            top: 95px;
            width: 36%;
            z-index: 1002;
        }
        
        #popup2
        {
            left: 221px;
            position: absolute;
            top: 38px;
            width: 33%;
            z-index: 1002;
        }
        #popup3
        {
            left: 221px;
            position: absolute;
            top: 38px;
            width: 48%;
            z-index: 1002;
        }
        #popdoc
        {
            left: 497px;
            position: absolute;
            top: 171px;
            overflow: auto;
            width: 29%;
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
        .styleTd
        {
            height: 36px;
            width: 46px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="fondoazul">
            <tr>
                <td style="border-width: 1px; text-align: center;" width="150">
                    <asp:ImageButton ID="logoHome" runat="server" Height="33px" ImageUrl="~/Imagenes/SIO1.png"
                        PostBackUrl="~/Home.aspx" Width="103px" />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table style="height: 52px; width: 395px" class="Letra">
            <tr>
                <td>
                    <asp:Label ID="lblTipo" runat="server" Text="Tipo de Visita : "></asp:Label>
                    &nbsp;
                    <asp:DropDownList ID="cboTipoVis" runat="server" CssClass="ComboM" AutoPostBack="true"
                        OnSelectedIndexChanged="cboTipoVis_SelectedIndexChanged">
                        <asp:ListItem>Seleccionar</asp:ListItem>
                        <asp:ListItem>Todas</asp:ListItem>
                        <asp:ListItem>Realizadas</asp:ListItem>
                        <asp:ListItem>No Realizadas</asp:ListItem>
                        <asp:ListItem>Canceladas</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table runat="server" id="tablaDetallado">
            <tr>
                <td>
                    <asp:Panel ID="PanelTablaDetalla" runat="server" Style="margin-bottom: 0px" GroupingText="Informacion de Visitas Detallada"
                        Width="752px" Height="210px" ScrollBars="Auto">
                        <asp:GridView ID="grdDetallado" runat="server" BackColor="White" DataKeyNames="numVis"
                            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
                            Width="737px" AutoGenerateColumns="False" Height="83px" Font-Size="9pt" OnRowCommand="grdDetallado_RowCommand">
                            <AlternatingRowStyle BackColor="Gainsboro" />
                            <Columns>
                                <asp:BoundField DataField="numVis" HeaderText="N°" ItemStyle-HorizontalAlign="Right"
                                    HeaderStyle-HorizontalAlign="Right">
                                    <ItemStyle Width="10pt" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cliente" HeaderText="Cliente" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemStyle Width="170pt" />
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
                                    <ItemStyle Width="10pt" />
                                </asp:BoundField>
                                <asp:ButtonField CommandName="notAgen" DataTextField="notAgen" HeaderText="Agenda"
                                    Text="Button" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10pt" />
                                </asp:ButtonField>
                                <asp:ButtonField ButtonType="Link" HeaderText="Ejecucion" Text="Button" CommandName="datosEje"
                                    DataTextField="datosEje" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10pt" />
                                </asp:ButtonField>
                                <asp:ButtonField CommandName="datosCierre" DataTextField="datosCierre" HeaderText="Cierre"
                                    Text="Button" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="10pt" />
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
                <td>
                    <div id='Div1' style="height: 310px; width: 751px;">
                        <div style="width: 239px">
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
    </div>
    <div id="fondoOsc" class="fondoOscuro">
    </div>
    <div id="popup" class="content-popup" style="display: none;">
    </div>
    <div id="popup2" class="content-popup2" style="display: none;">
    </div>
    <div id="popup3" class="content-popup3" style="display: none;">
    </div>
    <div id="popdoc" class="content-popup4" style="display: none;">
        <div class="close">
            <a href="#" id="close" onclick="cerrarPopupDoc();">
                <img alt="Cerrar" src="iconosMetro/close.png" /></a></div>
        <asp:GridView ID="grdDoc" runat="server" AutoGenerateColumns="False" DataKeyNames="ruta"
            Width="305px" CellPadding="4" ForeColor="#333333" GridLines="None" Height="16px">
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
    </div>
    </form>
</body>
</html>
