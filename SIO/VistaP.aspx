<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VistaP.aspx.cs" Inherits="SIO.VistaP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        function imprime() {
            //desaparece el boton
            document.getElementById("btnImprimir").style.display = 'none';
            //se imprime la pagina
            window.print();
            //reaparece el boton
            document.getElementById("btnImprimir").style.display = 'inline';
            var campo = window.opener.document.getElementById('ContentPlaceHolder1_txtCodigoEstiba');
            window.close();
            campo.focus();
        }
        function impr() {
            document.getElementById('btnImprimir').focus();
            //desaparece el boton
            document.getElementById("btnImprimir").style.display = 'none'
            //se imprime la pagina
            window.print();
            //reaparece el boton
            document.getElementById("btnImprimir").style.display = 'inline'
            window.close();
            var campo = window.opener.document.getElementById('ContentPlaceHolder1_txtCodigoEstiba');
            campo.focus();
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
            vertical-align: bottom;
            white-space: nowrap;
        }
        .style148
        {
            height: 87px;
        }
        .style234
        {
            height: 28px;
        }
        .style261
        {
            height: 60px;
        }
        .style262
        {
            height: 74px;
        }
        .style287
        {
            height: 60px;
            width: 66px;
        }
        .style289
        {
            height: 74px;
            width: 66px;
        }
        .style290
        {
            height: 28px;
            width: 66px;
        }
        .style291
        {
            height: 87px;
            width: 66px;
        }
        .style310
        {
            width: 27px;
            height: 50px;
        }
        .style320
        {
            width: 87px;
            height: 50px;
        }
        .style339
        {
            font-size: medium;
        }
        .style346
        {
            width: 87px;
            height: 48px;
        }
        .style347
        {
            text-align: center;
            height: 51px;
        }
        .style348
        {
            width: 214px;
        }
        .style349
        {
            width: 49px;
            height: 48px;
        }
        .style350
        {
            width: 36px;
            height: 48px;
        }
        .style352
        {
            text-align: right;
            width: 201px;
        }
        .style354
        {
            text-align: center;
            height: 51px;
            width: 49px;
        }
        .style355
        {
            width: 49px;
            height: 50px;
        }
        .style357
        {
            text-align: right;
            width: 182px;
        }
        .auto-style2 {
            width: 821px;
        }
        .style358
        {
            width: 209px;
        }
        .auto-style3 {
            width: 1034px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
            width: 1002pt; margin-right: 0px; height: 473px; margin-bottom: 0px;">
            <tr>
                <td>
                    <table style="width: 1401px">
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="lblCliente" runat="server" Text="Cl" Font-Italic="True" Font-Names="Arial"
                                    Font-Size="20pt" Style="font-weight: 700"></asp:Label>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="style287">
                            </td>
                            <td class="style261">
                            </td>
                        </tr>
                        <tr height="26">
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="lblCiudad" runat="server" Text="Ci" Font-Names="Arial" Font-Size="20pt"
                                    Style="font-weight: 700; font-style: italic;"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label1" runat="server" Text="-" Font-Names="Arial" Font-Size="20pt"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblPais" runat="server" Text="P" Font-Names="Arial" Font-Size="20pt"
                                    Style="font-weight: 700; font-style: italic;"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style289">
                            </td>
                            <td class="style262">
                            </td>
                        </tr>
                        <tr>
                            <td class="style290">
                            </td>
                            <td class="style234">
                                <asp:Label ID="lblDireccion" runat="server" Text="D" Font-Names="Arial" Font-Size="19pt"
                                    Style="font-weight: 700; font-style: italic;"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style291">
                            </td>
                            <td class="style148">
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label 
                                    ID="lblTipoOrden" runat="server" Text="O" Font-Names="Arial" Font-Size="40pt"
                                    Style="text-align: center; font-weight: 700;"></asp:Label>
                                &nbsp;&nbsp;
                                <asp:Label ID="lblOrden" runat="server" Text="O" Font-Names="Arial" Font-Size="40pt"
                                    Style="text-align: center; font-weight: 700;"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td class="style352">
                                <table style="width: 484px; height: 276px;">
                                    <tr>
                                        <td class="style357">
                                            <asp:Label ID="lblEstiba" runat="server" Text="E" Font-Names="Arial" Font-Bold="True"
                                                Font-Size="190pt" Width="474px" Height="265px" Style="text-align: center"></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="style348">
                            </td>
                            <td>
                                <table style="width: 332px; height: 274px; margin-left: 0px;">
                                    <tr>
                                        <td class="style354">
                                        </td>
                                        <td class="style347">
                                            <asp:Label ID="lblCbs" runat="server" Font-Size="14pt" Style="text-align: right;
                                                font-weight: 700;" Text="Cbs"></asp:Label>
                                            <br />
                                            <asp:Label ID="lblCodigoBarras" runat="server" Text="CB" Font-Names="bcw_code128c_3"
                                                Font-Size="28pt" Style="text-align: center" Height="79px"></asp:Label>
                                        </td>
                                        <td class="style347">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style349">
                                            <asp:Label ID="lblCantiPall" runat="server" Text="C" Font-Names="Arial" Font-Bold="True"
                                                Font-Size="50pt" Height="64px"></asp:Label>
                                            <br />
                                            <strong><span class="style339">Cant._Piezas</span></strong>
                                        </td>
                                        <td class="style346">
                                            &nbsp;&nbsp;&nbsp; &nbsp;
                                        </td>
                                        <td class="style350">
                                            <asp:Label ID="lblVolumen" runat="server" Text="V" Font-Names="Arial" Font-Bold="True"
                                                Font-Size="53pt" Height="64px"></asp:Label>
                                            <br />
                                            <strong><span class="style339">Volumen_M3</span></strong>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style355">
                                            <asp:Label ID="lblPesoBruto" runat="server" Text="PB" Font-Names="Arial" Font-Bold="True"
                                                Font-Size="50pt" Height="64px"></asp:Label>
                                            <br />
                                            <strong><span class="style339">P.Bruto_kg</span></strong>
                                            <br /><br />
                                        </td>
                                        <td class="style320">
                                        </td>
                                        <td class="style310">
                                            <asp:Label ID="lblPesoNeto" runat="server" Text="PN" Font-Names="Arial" Font-Bold="True"
                                                Font-Size="50pt" Height="64px"></asp:Label>
                                            <br />
                                            <strong><span class="style339">P.Neto_kg</span></strong>
                                            <br />
                                            <br />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <table>                        
                        <tr>
                            <td align="right" class="auto-style3">
                                <asp:Label ID="lblusuario" runat="server" Font-Names="Arial" Font-Bold="True"
                                                Font-Size="10pt"></asp:Label>
                                            <br />
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td class="auto-style2">
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblAluAcc" runat="server" Text="AA" Font-Names="Arial" Font-Bold="True"
                                    Font-Size="38pt"></asp:Label>

                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <input name="btnImprimir" runat="server" id="btnImprimir" type="button" class="button"
                        value="Imprimir" onclick="imprime()" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
