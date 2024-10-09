<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PackingList.aspx.cs" Inherits="SIO.PackingList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function cerrarpagina() {
            window.close();
            return false;
        }
        function imprimir() {
            document.getElementById("btnImprimir").style.display = 'none';
            document.getElementById("btnCerrar").style.display = 'none';
            //se imprime la pagina
            window.print();
            //reaparece el boton
            window.close();
        }
    </script>
    <style type="text/css">
        td
        {
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding: 0px;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: general;
            vertical-align: bottom;
            white-space: nowrap;
        }
        .boton1
        {
            background: url(../iconosMetro/imprimir.png) repeat;
            background-position: center;
            width: 30pt;
            height: 30pt;
        }
        .boton2
        {
            background: url(../iconosMetro/cerrar.jpg) repeat;
            background-position: center;
            width: 30pt;
            height: 30pt;
        }
        .boton
        {
            background: url(../iconosMetro/eliminar.jpg) repeat;
            background-position: center;
            width: 30pt;
            height: 30pt;
        }
        .styleAlineacionDerecha
        {
            text-align: right;
            color: black;
        }
        .styleAlineacionIzquierda
        {
            text-align: left;
            color: black;
        }
        .styleAlineacionCentro
        {
            text-align: center;
            color: black;
        }
        .styleBordes
        {
            border: .5pt solid windowtext;
            text-align: center;
        }
        .styleBordesDerecha
        {
            border: .5pt solid windowtext;
            text-align: right;
        }
        .styleBordesIzquierda
        {
            border: .5pt solid windowtext;
            text-align: left;
        }
        .styleBordesCentro
        {
            border: .5pt solid windowtext;
            text-align: center;
        }
        .style14
        {
            width: 77pt;
        }
        #form1
        {
            height: 754px;
            width: 1081px;
            margin-left: 34px;
        }
        #form2
        {
            height: 24px;
            width: 1081px;
            margin-left: 32px;
        }
        #TextArea1
        {
            width: 966px;
            margin-left: 5px;
            height: 70px;
        }
        .style16
        {
            text-align: left;
            color: black;
            width: 347px;
        }
        #txtEncomendante
        {
            width: 1031px;
            height: 49px;
            margin-left: 1px;
        }
        .style47
        {
            border: .5pt solid windowtext;
            text-align: right;
            width: 115px;
        }
        .style52
        {
            border: .5pt solid windowtext;
            text-align: center;
            width: 292px;
        }
        .style54
        {
            width: 116px;
        }
        .style55
        {
            width: 130px;
            height: 79px;
        }
        .style56
        {
            text-align: center;
            color: black;
            font-size: x-large;
        }
        .style58
        {
            text-align: left;
            color: black;
            width: 168px;
        }
        .style59
        {
            width: 168px;
        }
        .style60
        {
            width: 5px;
        }
        .style61
        {
            text-align: right;
            color: black;
            width: 168px;
        }
        .style62
        {
            width: 96px;
        }
        .style76
        {
            width: 177px;
        }
        .style87
        {
            border: .5pt solid windowtext;
            text-align: right;
            width: 174px;
        }
        .style88
        {
            width: 150px;
        }
        .style89
        {
            border: .5pt solid windowtext;
            text-align: right;
            width: 135px;
        }
    </style>
</head>
<body style="height: 856px; width: 1142px">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
            width: 797pt">
            <tr height="20">
                <td height="20" width="24" colspan="4">
                </td>
            </tr>
            <tr height="20">
                <td height="20" rowspan="3" class="style54">
                    <img alt="forsa" class="style55" longdesc="forsa" src="iconosMetro/forsa.jpg" />
                </td>
                <td class="xl65">
                </td>
                <td>
                </td>
                <td class="style56"> 
                    <strong>PACKING LIST SUMMARY </strong>
                </td>
            </tr>
            <tr height="20">
                <td height="20">
                </td>
                <td>
                </td>
                <td class="styleAlineacionCentro">
                    <strong>EXPORTADOR/FORNECEDOR/FABRICANTE: FORSA S.A. </strong>
                </td>
            </tr>
            <tr height="20">
                <td height="20">
                </td>
                <td>
                </td>
                <td class="styleAlineacionCentro">
                    <strong>Parque Industrial y comercial del Cauca Et. 1 - Caloto - Cauca - Colombia
                    </strong>
                </td>
            </tr>
            <tr height="20">
                <td height="20" width="24" colspan="4">
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
            width: 797pt">
            <colgroup>
                <col width="24" />
                <col />
                <col />
                <col />
                <col />
                <col width="152" />
                <col width="144" />
                <col width="26" />
            </colgroup>
            <tr height="20">
                <td height="20" width="24">
                </td>
                <td width="134">
                </td>
                <td class="style62">
                </td>
                <td class="style60">
                </td>
                <td class="style61">
                    FECHA:
                </td>
                <td class="styleAlineacionIzquierda">
                    &nbsp;&nbsp;
                    <asp:Label ID="lblFecha" runat="server" Style="font-weight: 700" Width="128pt"></asp:Label>
                </td>
            </tr>
            <tr height="20">
                <td class="xl66" colspan="7" height="20">
                </td>
            </tr>
            <tr height="20">
                <td class="styleAlineacionIzquierda">
                    IMPORTADOR:
                </td>
                <td>
                </td>
                <td class="style62">
                </td>
                <td class="style60">
                </td>
                <td class="style58">
                    INCOTERM:
                </td>
                <td>
                </td>
            </tr>
            <tr height="20">
                <td class="styleAlineacionIzquierda" colspan="3">
                    <asp:Label ID="lblCliente" runat="server" Style="font-weight: 700" Width="307pt"></asp:Label>
                </td>
                <td class="style60">
                </td>
                <td class="styleAlineacionIzquierda" colspan="2">
                    <asp:Label ID="lblTdn" runat="server" Style="font-weight: 700"></asp:Label>
                </td>
            </tr>
            <tr height="20">
                <td class="styleAlineacionIzquierda" colspan="3">
                    <asp:Label ID="lblDireccion" runat="server" Style="font-weight: 700" Width="434pt"></asp:Label>
                </td>
                <td class="style60">
                </td>
                <td class="style58">
                    PUERTO DE EMBARQUE:
                </td>
                <td>
                </td>
            </tr>
            <tr height="20">
                <td class="styleAlineacionIzquierda" colspan="3">
                    <asp:Label ID="lblNit" runat="server" Style="font-weight: 700"></asp:Label>
                </td>
                <td class="style60">
                </td>
                <td class="styleAlineacionIzquierda" colspan="2">
                    <asp:Label ID="lblPuertoE" runat="server" Style="font-weight: 700"></asp:Label>
                </td>
            </tr>
            <tr height="20">
                <td class="styleAlineacionIzquierda" colspan="3">
                    <asp:Label ID="lblTelefono" runat="server" Style="font-weight: 700"></asp:Label>
                </td>
                <td class="style60">
                </td>
                <td class="style59">
                    PUERTO DE DESTINO:
                </td>
                <td>
                </td>
            </tr>
            <tr height="20">
                <td class="styleAlineacionIzquierda" colspan="3">
                    <asp:Label ID="lblPais" runat="server" Style="font-weight: 700"></asp:Label>
                </td>
                <td class="style60">
                </td>
                <td class="style58">
                    <asp:Label ID="lblPuertoD" runat="server" Style="font-weight: 700"></asp:Label>
                </td>
                <td>
                </td>
            </tr>
            <tr height="20">
                <td>
                </td>
                <td>
                </td>
                <td class="style62">
                </td>
                <td class="style60">
                </td>
                <td class="styleAlineacionIzquierda" colspan="2">
                    ORDEN:</td>
            </tr>
            <tr height="20">
                <td class="styleAlineacionIzquierda">
                    ENCOMENDANTE:
                </td>
                <td>
                </td>
                <td class="style62">
                </td>
                <td class="style60">
                </td>
                <td class="style59">
                    <asp:Label ID="lblOrden" runat="server" Style="font-weight: 700"></asp:Label>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table style="width: 1062px; height: 42px;">
            <tr height="20">
                <td class="style16" colspan="3" rowspan="4">
                    <textarea id="txtEncomendante" runat="server" readonly="readonly"></textarea>
                </td>
            </tr>
        </table>
    </div>
    <div>
    </div>
    <div>
        <table>
            <tr>
                <td class="style14">
                    <asp:GridView ID="GridView1" runat="server" CellPadding="3" GridLines="Vertical"
                        Width="538px" HorizontalAlign="Left" AutoGenerateColumns="False" BorderStyle="None"
                        BorderWidth="1px" BackColor="White" BorderColor="#1C5AB6">
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:BoundField HeaderText="Contenedor No." ItemStyle-Width="80pt" DataField="contenedor"
                                ItemStyle-HorizontalAlign="Left">
                                <ItemStyle HorizontalAlign="Left" Width="80pt"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Placa" ItemStyle-Width="74pt" DataField="placa" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle HorizontalAlign="Left" Width="74pt"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Precinto" ItemStyle-Width="80pt" DataField="precinto"
                                ItemStyle-HorizontalAlign="Left">
                                <ItemStyle HorizontalAlign="Left" Width="80pt"></ItemStyle>
                            </asp:BoundField>
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
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
            width: 797pt">
            <tr height="20" style="height: 15.0pt" class="styleBordes">
                <td class="styleBordes" colspan="4">
                </td>
                <td class="style52">
                    Dimensiones
                </td>
                <td class="style47">
                    FACTURA:
                </td>
                <td class="styleBordesIzquierda" width="162">
                    &nbsp;
                    <asp:Label ID="lblFactura" runat="server" Style="font-weight: 700"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table>
            <tr>
                <td class="style14">
                    <asp:GridView ID="GridView2" runat="server" CellPadding="3" GridLines="Vertical"
                        Width="1063px" HorizontalAlign="Right" AutoGenerateColumns="False" BorderStyle="None"
                        BorderWidth="1px" BackColor="White" BorderColor="#1C5AB6">
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:BoundField HeaderText="Pallet No." ItemStyle-Width="90pt" DataField="palletN">
                                <ItemStyle Width="90pt"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Contenido" DataField="contenido" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" Width="180"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Largo" ItemStyle-Width="74pt" DataField="largo" ItemStyle-HorizontalAlign="Right">
                                <ItemStyle Width="74pt"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Ancho" ItemStyle-Width="74pt" DataField="ancho" ItemStyle-HorizontalAlign="Right">
                                <ItemStyle Width="74pt"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Alto" ItemStyle-Width="74pt" DataField="alto" ItemStyle-HorizontalAlign="Right">
                                <ItemStyle Width="74pt"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="M3" ItemStyle-Width="74pt" DataField="m3" ItemStyle-HorizontalAlign="Right">
                                <ItemStyle Width="74pt"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Peso Bruto" ItemStyle-Width="67pt" DataField="pesoB"
                                ItemStyle-HorizontalAlign="Right">
                                <ItemStyle Width="67pt"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Peso Neto" ItemStyle-Width="67pt" DataField="pesoN" ItemStyle-HorizontalAlign="Right">
                                <ItemStyle Width="67pt"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle BackColor="#1C5AB6" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#1C5AB6" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#0000A9" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#000065" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    <table style="width: 1066px">
    <tr>
    <td class="style76" style="border: .5pt solid windowtext">
                    <strong style="text-align: left">Totales </strong>
                </td>
    <td class="style87">Cantidad de Pallets:</td>
    <td colspan="3" class="style88" style="border: .5pt solid windowtext">
                    &nbsp;&nbsp;
                    <asp:Label ID="lblCantPallets" runat="server" Style="font-weight: 700;"></asp:Label>
                </td>
    <td class="style89">
                    <asp:Label ID="lblTotalV" runat="server" Style="font-weight: 700"></asp:Label>
                </td>
    <td class="styleBordesDerecha">
                    <asp:Label ID="lblTotalB" runat="server" Style="font-weight: 700"></asp:Label>
                </td>
    <td class="styleBordesDerecha">
                    <asp:Label ID="lblTotalN" runat="server" 
                        Style="font-weight: 700;"></asp:Label>
                </td>
    </tr>
    </table>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
            width: 796pt">
            <colgroup>
                <col width="24" />
                <col width="134" />
                <col width="127" />
                <col width="145" />
                <col width="155" />
                <col width="152" />
                <col width="144" />
                <col width="26" />
            </colgroup>
            <tr height="20">
                <td class="xl66" colspan="8" height="20" width="907">
                </td>
            </tr>
            <tr height="20">
                <td height="20">
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td class="xl67" colspan="3">
                    &nbsp;
                </td>
                <td>
                </td>
            </tr>
            <tr height="20">
                <td height="20">
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td style="border-bottom-width: 2px; border-top-color: black; border-top-style: solid;
                    border-top-width: 1px; text-align: center;" colspan="3">
                    <asp:Label ID="lblUsuarioCreaDes" runat="server" Style="font-weight: 700"></asp:Label>
                </td>
                <td>
                </td>
            </tr>
            <tr height="20">
                <td height="20">
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td class="styleAlineacionCentro">
                    COMERCIO EXTERIOR
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr height="20">
                <td height="20">
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td class="styleAlineacionCentro">
                    FORSA S.A.
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr height="20">
                <td class="xl66" colspan="8" height="20">
                </td>
            </tr>
        </table>
    </div>
    <div style="width: 1060px">
        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
            width: 796pt">
            <tr>
                <td id="colum2" class="style305">
                    <input id="btnImprimir" runat="server" style="background-image: url('iconosMetro/imprimir.png');
                        width: 53pt; height: 30pt; background-repeat: no-repeat;" type="button" onclick="imprimir();" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnCerrar" runat="server" Style="background-image: url('iconosMetro/cerrar.jpg');
                        background-repeat: no-repeat;" OnClientClick="return cerrarpagina();" OnClick="btnCerrar_Click"
                        Height="56px" Width="60px" />
                </td>
            </tr>
        </table>
    </div>
    </form>
    <form id="form2">
    <asp:Label ID="lblMensaje" runat="server"></asp:Label>
    </form>
</body>
</html>
