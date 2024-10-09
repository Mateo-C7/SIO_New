<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActaTrazabilidad.aspx.cs"
    Inherits="SIO.ActaTrazabilidad" %>

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
            padding-top: 1px;
            padding-right: 1px;
            padding-left: 1px;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: left;
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
        .style1
        {
            height: 45.0pt;
            width: 98pt;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: Calibri, sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
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
        .style5
        {
            width: 102px;
        }
        .style9
        {
            text-align: center;
            color: black;
            width: 144px;
        }
        .style12
        {
            width: 223px;
        }
        .style13
        {
            text-align: right;
            color: black;
            width: 223px;
        }
        .style14
        {
            width: 137px;
        }
        #form1
        {
            height: 950px;
            width: 982px;
            margin-left: 32px;
        }
        #form2
        {
            height: 24px;
            width: 982px;
            margin-left: 32px;
        }
        .style17
        {
            text-align: right;
            color: black;
            width: 101px;
        }
        .style19
        {
            width: 344px;
        }
        .style20
        {
            width: 953px;
        }
        .style21
        {
            width: 17px;
        }
        .style22
        {
            width: 144px;
        }
        .style23
        {
            text-align: center;
            color: black;
            width: 129px;
        }
        .style24
        {
            width: 129px;
        }
        .style26
        {
            width: 162px;
        }
        .style27
        {
            width: 130px;
            height: 77px;
            margin-top: 0px;
        }
        .style28
        {
            font-size: x-large;
        }
        .style29
        {
            height: 14px;
        }
        .style30
        {
            text-align: right;
            color: black;
            height: 1px;
        }
        .style31
        {
            text-align: center;
            color: black;
            height: 1px;
        }
        .style32
        {
            height: 134px;
        }
        .style33
        {
            text-align: right;
            color: black;
            height: 42px;
        }
        .style34
        {
            text-align: center;
            color: black;
            height: 42px;
        }
        .style35
        {
            border: .5pt solid windowtext;
            text-align: left;
        }
        .style37
        {
            width: 160px;
        }
        .style43
        {
            height: 24px;
        }
        .style45
        {
            width: 207px;
            height: 26px;
            text-align: right;
        }
        .style46
        {
            width: 115px;
            height: 26px;
        }
        .style51
        {
            width: 113px;
            text-align: left;
            height: 26px;
        }
        .style52
        {
            height: 24px;
            text-align: right;
            width: 207px;
        }
        .style53
        {
            width: 207px;
            text-align: right;
            height: 19px;
        }
        .style55
        {
            height: 26px;
        }
        .style58
        {
            width: 108px;
            height: 26px;
            text-align: right;
        }
        .style59
        {
            height: 24px;
            width: 108px;
        }
        .style61
        {
            height: 24px;
            width: 113px;
        }
        .style62
        {
            width: 115px;
            height: 19px;
        }
        .style64
        {
            width: 113px;
            height: 19px;
        }
        .style65
        {
            width: 108px;
            height: 19px;
        }
        .style66
        {
            height: 26px;
            text-align: right;
            width: 93px;
        }
        .style67
        {
            text-align: right;
            width: 93px;
        }
        .style68
        {
            height: 24px;
            width: 93px;
        }
        .style69
        {
            width: 207px;
        }
        .style70
        {
            width: 102px;
            text-align: right;
        }
        .style71
        {
            width: 223px;
            text-align: right;
        }
    </style>
</head>
<body style="height: 1064px; width: 1087px; text-align: left;">
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
            width: 712pt" width="945">
            <tr height="20" style="height: 15.0pt">
                <td class="style1" height="60" rowspan="4" width="130">
                    <img alt="forsa" class="style27" longdesc="forsa" src="iconosMetro/forsa.jpg" />
                </td>
                <td style="width: 85pt" width="113">
                </td>
                <td style="width: 20pt" width="27">
                </td>
                <td style="width: 61pt" width="81">
                </td>
                <td style="width: 101pt" width="134">
                </td>
                <td style="width: 20pt" width="26">
                </td>
                <td style="width: 133pt" width="177">
                </td>
                <td style="width: 88pt" width="117">
                </td>
                <td style="width: 20pt" width="26">
                </td>
                <td style="width: 86pt" width="114">
                </td>
            </tr>
            <tr height="20" style="height: 15.0pt">
                <td class="styleAlineacionCentro" colspan="9" height="20">
                    <strong><span class="style28">ACTA DE TRAZABILIDAD</span></strong>
                </td>
            </tr>
            <tr height="20" style="height: 15.0pt">
                <td height="20" style="height: 15.0pt">
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr height="20" style="height: 15.0pt">
                <td height="20" style="height: 15.0pt">
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr height="20" style="height: 15.0pt">
                <td height="20" style="height: 15.0pt">
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
            width: 434pt" width="579">
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr height="20" style="height: 15.0pt">
                <td class="xl65" height="20" width="96">
                    PAIS DESTINO:
                </td>
                <td class="xl66" width="207">
                    &nbsp;
                    <asp:Label ID="lblPais" runat="server" Style="font-weight: 700"></asp:Label>
                </td>
                <td width="31">
                </td>
                <td class="xl65" width="51">
                    FECHA:
                </td>
                <td class="xl66" width="194">
                    <asp:Label ID="lblFechaImp" runat="server" Style="font-weight: 700"></asp:Label>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:GridView ID="GridView2" runat="server" CellPadding="3" GridLines="Vertical"
                                    Width="570px" HorizontalAlign="Right" AutoGenerateColumns="False" BorderStyle="None"
                                    BorderWidth="1px" Height="82px">
                                    <AlternatingRowStyle BackColor="white" />
                                    <Columns>
                                        <asp:BoundField DataField="orden" HeaderText="Orden" ItemStyle-Width="80pt" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="cliente" HeaderText="Cliente" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <HeaderStyle BackColor="#000084" Font-Bold="True"  />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True"  />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#000065" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table style="height: 124px">
                        <tr>
                            <td class="style32">
                                <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
                                    width: 285pt; height: 122px;">
                                    <colgroup>
                                        <col width="163" />
                                        <col width="167" />
                                    </colgroup>
                                    <tr height="20">
                                        <td class="styleAlineacionDerecha" height="20">
                                            HORA LLEGADA CAMION:
                                        </td>
                                        <td class="styleAlineacionCentro">
                                            <asp:Label ID="lblHoraLlegada" runat="server" Style="font-weight: 700"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style33">
                                            HORA SALIDA CAMION:&nbsp;
                                        </td>
                                        <td class="style34" 
                                            style="border-bottom-style: dashed; border-bottom-width: 2px;">
                                            </td>
                                    </tr>
                                    <tr>
                                        <td class="style29">
                                        </td>
                                        <td class="style29">
                                        </td>
                                    </tr>
                                    <tr height="20">
                                        <td class="styleAlineacionDerecha" height="20">
                                            HORA INICIO CARGUE:
                                        </td>
                                        <td class="styleAlineacionCentro">
                                            <asp:Label ID="lblInicioCargue" runat="server" Style="font-weight: 700"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style30">
                                            HORA TERMINA CARGUE:
                                        </td>
                                        <td class="style31">
                                            <asp:Label ID="lblFinCargue" runat="server" Style="font-weight: 700"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr height="20">
                                        <td class="style30">
                                            TOTAL HORAS:</td>
                                        <td class="style31">
                                            <asp:Label ID="lblTH" runat="server" Style="font-weight: 700"></asp:Label>
                                            h.:<asp:Label ID="lblTM" runat="server" Style="font-weight: 700"></asp:Label>
                                            min.</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
            width: 734pt">
            <colgroup>
                <col />
                <col />
                <col />
                <col />
                <col />
                <col />
                <col width="114" />
                <col width="130" />
            </colgroup>
            <tr>
                <td class="style69">
                </td>
            </tr>
            <tr height="20">
                <td class="styleBordes" colspan="7" height="20" width="820">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <strong>TRAZABILIDAD </strong>
                </td>
            </tr>
            <tr height="20">
                <td height="20" class="style37" colspan="7">
                </td>
            </tr>

            <tr>
                <td class="style53">
                    CONTENEDOR No:</td>
                <td class="style62" colspan="2">
                    &nbsp;
                    <asp:Label ID="lblNumContenedor" runat="server" 
                        Style="font-weight: 700; text-align: left;"></asp:Label>
                </td>
                
                <td class="style67">
                    TRAILER No:</td>
                <td class="style64">
                    
                &nbsp;
                    <asp:Label ID="lblNumTrailer" runat="server" 
                        Style="font-weight: 700; "></asp:Label>
                    
                </td>
                <td class="style65">
                    PLACA DEL VEHICULO:</td>
                <td class="style65">
                    &nbsp;
                    <asp:Label ID="lblPlaca" runat="server" 
                        Style="font-weight: 700; text-align: left;"></asp:Label>
                </td>
            </tr>
           <tr>
                <td class="style52">EMPRESA DE TRANSPORTE:
                </td>
                <td colspan="2" class="style46" >
                    &nbsp;
                    <asp:Label ID="lblNomEm" runat="server" 
                        Style="font-weight: 700; text-align: left;" ></asp:Label>
                </td>
                <td class="style68">
                </td>
                <td class="style61">
                </td>
                <td class="style59">
                </td>
                <td class="style43">
                </td>
            </tr>
            <tr>
                <td class="style45">
                    CONDUCTOR:</td>
                <td class="style46" colspan="2">
                    &nbsp;
                    <asp:Label ID="lblNomCon" runat="server" Style="font-weight: 700"></asp:Label>
                </td>
                
                <td class="style66">
                    TELEFONO:</td>
                <td class="style51">
                    &nbsp;
                    <asp:Label ID="lblTel" runat="server" 
                        Style="font-weight: 700; text-align: left;"></asp:Label>
                </td>
                <td class="style58">
                    C.C.:</td>
                
                <td class="style55">
                    &nbsp;
                    <asp:Label ID="lblCC" runat="server" 
                        Style="font-weight: 700; text-align: left;"></asp:Label>
                    </td>
            </tr>
            <tr>
                <td colspan="7" height="20" width="784">
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
            width: 725pt">
            <tr>
                <td class="styleBordes" colspan="6" height="20" width="784">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <strong>DATOS DEL DESPACHO</strong>
                </td>
            </tr>
            <tr height="20">
                <td class="style35" colspan="4" height="20">
                    <strong>TIPO DESPACHO </strong>
                </td>
                <td class="styleBordes" colspan="2">
                    <strong>PESOS </strong>
                </td>
            </tr>
            <tr height="20">
                <td height="20" class="style24">
                </td>
                <td class="style5">
                </td>
                <td class="style26">
                </td>
                <td class="style22">
                </td>
                <td class="style12">
                </td>
                <td class="style14">
                </td>
            </tr>
            <tr height="20">
                <td class="style23">
                    <asp:Label ID="lblTipoExpo" runat="server" Style="font-weight: 700"></asp:Label>
                </td>
                <td class="styleAlineacionDerecha">
                    PRECINTO LLEGADA:
                </td>
                <td class="style26">
                    &nbsp;&nbsp;
                    <asp:Label ID="lblPrecintoL" runat="server" Style="font-weight: 700"></asp:Label>
                    &nbsp;
                </td>
                <td class="style9">
                    <asp:Label ID="lblNomLLegada" runat="server" Style="font-weight: 700"></asp:Label>
                </td>
                <td class="style13">
                    PESO BRUTO(Mercancia):
                </td>
                <td class="style14">
                    &nbsp;
                    <asp:Label ID="lblPesoB" runat="server" Style="font-weight: 700"></asp:Label>
                    &nbsp;
                </td>
            </tr>
            <tr height="20">
                <td height="20" class="style24">
                </td>
                <td class="style5">
                </td>
                <td class="style26">
                </td>
                <td class="style22">
                </td>
                <td class="style12">
                </td>
                <td class="style14">
                </td>
            </tr>
            <tr height="20">
                <td class="style23">
                    <asp:Label ID="lblTipoTrans" runat="server" Style="font-weight: 700"></asp:Label>
                </td>
                <td class="styleAlineacionDerecha">
                    PRECINTO SALIDA:
                </td>
                <td class="style26">
                    &nbsp;&nbsp;
                    <asp:Label ID="lblPrecintoS" runat="server" Style="font-weight: 700"></asp:Label>
                    &nbsp;
                </td>
                <td class="style9">
                    <asp:Label ID="lblNomSalida" runat="server" Style="font-weight: 700"></asp:Label>
                </td>
                <td class="style13">
                    PESO NETO (Mercancia):
                </td>
                <td class="style14">
                    &nbsp;
                    <asp:Label ID="lblPesoN" runat="server" Style="font-weight: 700"></asp:Label>
                    &nbsp;
                </td>
            </tr>
            <tr height="20">
                <td height="20" class="style24">
                </td>
                <td class="style5">
                </td>
                <td class="style26">
                </td>
                <td class="style22">
                </td>
                <td class="style12">
                </td>
                <td class="style14">
                </td>
            </tr>
            <tr height="20">
                <td height="20" class="style24">
                </td>
                <td class="style70">
                    OTROS PRECINTOS:</td>
                <td class="style26">
                    &nbsp;&nbsp;
                    <asp:Label ID="lblOtroPre" runat="server" Style="font-weight: 700"></asp:Label>
                &nbsp;</td>
                <td class="style22">
                </td>
                <td class="style13">
                    CANT. DE BULTOS Y/O PAQUETES:
                </td>
                <td class="style14">
                    &nbsp;
                    <asp:Label ID="lblCantByP" runat="server" Style="font-weight: 700"></asp:Label>
                    &nbsp;
                </td>
            </tr>
            <tr height="20">
                <td height="20" class="style24">
                </td>
                <td class="style5">
                </td>
                <td class="style26">
                </td>
                <td class="style22">
                </td>
                <td class="style12">
                </td>
                <td class="style14">
                </td>
            </tr>
            <tr height="20">
                <td height="20" class="style24">
                </td>
                <td class="style5">
                </td>
                <td class="style26">
                </td>
                <td class="style22">
                </td>
                <td class="style71">
                    VOLUMEN:</td>
                <td class="style14">
                    &nbsp;&nbsp;
                    <asp:Label ID="lblVol" runat="server" Style="font-weight: 700"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="6" height="20" width="784">
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
            width: 725pt">
            <tr height="20">
                <td class="styleBordes" colspan="7" height="20" width="820">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <strong>PERSONAS QUE PARTICIPAN EN EL CARGUE </strong>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table>
            <tr>
                <td>
                    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" BorderStyle="None"
                        BorderWidth="1px" CellPadding="3" GridLines="Vertical" Height="82px" HorizontalAlign="Right"
                        Width="963px">
                        <AlternatingRowStyle BackColor="white" />
                        <Columns>
                            <asp:BoundField DataField="nombre" HeaderText="Nombre" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Width="170pt">
                                <ItemStyle HorizontalAlign="Left" Width="160pt"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="cedula" HeaderText="Cedula" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="65pt">
                                <ItemStyle HorizontalAlign="Center" Width="80pt"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="area" HeaderText="Area" ItemStyle-Width="140pt" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle HorizontalAlign="Left" Width="140pt"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Firma" ItemStyle-Width="120pt" 
                                ItemStyle-HorizontalAlign="Center" DataField="firma" >
                                <ItemStyle HorizontalAlign="Center" Width="120pt"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Observaciones" ItemStyle-Width="140pt" 
                                ItemStyle-HorizontalAlign="Center" DataField="obser">
                                <ItemStyle HorizontalAlign="Center" Width="140pt"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True"  />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True"  />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#0000A9" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#000065" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="8" height="20" width="784">
                </td>
            </tr>
            <tr>
                <td colspan="8" height="20" width="784">
                </td>
            </tr>
        </table>
    </div>
    <div style="width: 966px">
        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
            width: 725pt">
            <tr height="20" style="height: 15.0pt">
                <td height="20" width="195">
                </td>
                <td class="style17">
                    <strong>REVISADO POR</strong>:
                </td>
                <td style="border-bottom-style: dashed; border-bottom-width: 2px; text-align: center;"
                    colspan="2" class="style20">
                    &nbsp;
                </td>
                <td class="style21">
                    <strong>CARGO</strong>:
                </td>
                <td style="border-bottom-style: dashed; border-bottom-width: 2px; text-align: center;"
                    colspan="2" class="style19">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td id="colum2" class="style305">
                    <input id="btnImprimir" runat="server" style="background-image: url('iconosMetro/imprimir.png');
                        width: 53pt; height: 30pt; background-repeat: no-repeat;" type="button" onclick="imprimir();" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                            ID="btnCerrar" runat="server" Style="background-image: url('iconosMetro/cerrar.jpg');
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