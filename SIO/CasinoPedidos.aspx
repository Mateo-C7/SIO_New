<%@ Page Title="Pedidos" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true"
    CodeBehind="CasinoPedidos.aspx.cs" Inherits="SIO.Pedidos" Culture="auto" UICulture="auto" %>
 
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        function verPedidos() {
            window.open('CasinoBuscarPedidos.aspx', this.target, 'top=50, left=30, toolbar=no, location=no, status=no, menubar=no, scrollbars=yes, resizable=no, width=1300, height=450')
        }
    </script>
    <style type="text/css">
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
        .Letra
        {
            font-family: Arial;
            font-size: 8pt;
            color: Black;
            margin-top: 0px;
        }
        .styleTextoDer
        {
            text-align: right;
        }
        .styleTextoIzq
        {
            text-align: left;
        }
        .styleTextoCen
        {
            text-align: center;
        }
        .styleTextoDerN
        {
            font-weight: bold;
            text-align: right;
        }
        .styleTextoIzqN
        {
            font-weight: bold;
            text-align: left;
        }
        .styleTextoCenN
        {
            font-weight: bold;
            text-align: center;
        }
        .ComboP
        {
            width: 60px;
            height: 15px;
            font-size: 8pt;
        }
        .ComboM
        {
            width: 150px;
            height: 15px;
            font-size: 8pt;
            margin-left: 0px;
        }
        .TexboxPMP
        {
            /*Pequeño mas Pequeño*/
            height: 12px;
            width: 40px;
            font-size: 8pt;
        }
        .TexboxP
        {
            /*Pequeño*/
            height: 12px;
            width: 60px;
            font-size: 8pt;
        }
        .TexboxN
        {
            /*Normal*/
            height: 12px;
            width: 100px;
            font-size: 8pt;
        }
        .TexboxM
        {
            /*Mediano*/
            height: 12px;
            width: 162px;
            font-size: 8pt;
            margin-left: 0px;
        }
        .TexboxG
        {
            /*Grande*/
            height: 12px;
            width: 290px;
            font-size: 8pt;
        }
        .style114
        {
            width: 191px;
        }
        .style120
        {
            width: 116px;
        }
        .style135
        {
            width: 93px;
        }
        
        .style157
        {
            height: 26px;
            text-align: right;
        }
        .style159
        {
            font-weight: bold;
            text-align: right;
            width: 170px;
            height: 41px;
        }
        .style182
        {
            text-align: right;
            width: 161px;
        }
        .style184
        {
            width: 228px;
        }
        .style185
        {
            text-align: right;
            width: 148px;
        }
        .style190
        {
            width: 359px;
        }
        .style193
        {
            width: 170px;
        }
        .style196
        {
            width: 327px;
        }
        .style200
        {
            width: 176px;
        }
        .style203
        {
            width: 359px;
            height: 41px;
        }
        .style204
        {
            width: 176px;
            height: 41px;
        }
        .style205
        {
            width: 327px;
            height: 41px;
        }
        .style206
        {
            width: 359px;
            height: 31px;
        }
        .style207
        {
            height: 31px;
        }
        .style208
        {
            width: 327px;
            height: 31px;
        }
        .style209
        {
            text-align: right;
            height: 17px;
        }
        .style210
        {
            height: 17px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelPedidos" runat="server" GroupingText="Pedidos" Height="391px"
                CssClass="Letra" Width="1026px">
                <table style="height: 107px; width: 1026px">
                    <tr>
                        <td class="style203">
                        </td>
                        <td class="style159">
                            Pedido N°&nbsp;&nbsp;
                            <asp:TextBox ID="txtNumPedido" runat="server" BorderStyle="Solid" AutoPostBack="True"
                                Font-Bold="True" Font-Names="Arial" Font-Size="25px" ForeColor="Blue" Width="89px"
                                Height="28px" Enabled="false"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="ftNoNumNumPedido" runat="server" FilterType="Numbers"
                                TargetControlID="txtNumPedido">
                            </asp:FilteredTextBoxExtender>
                        </td>
                        <td class="style204">
                            &nbsp; &nbsp;
                            <asp:Button ID="btnAdicionar" runat="server" Text="Nuevo" Width="71px" OnClick="btnAdicionar_Click"
                                BorderColor="#999999" BackColor="#1C5AB6" Font-Bold="True" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="White" />
                            &nbsp;
                            <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" Text="Buscar"
                                Width="60px" BorderColor="#999999" BackColor="#1C5AB6" Font-Bold="True" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="White" />
                        </td>
                        <td class="style205">
                        </td>
                    </tr>
                    <tr>
                        <td class="style206">
                            Empleado:&nbsp;<asp:Label ID="lblNomEmp" runat="server" Width="294px"></asp:Label>
                        </td>
                        <td colspan="2" style="text-align: center;" class="style207">
                            Area:&nbsp;<asp:Label ID="lblAreaEmp" runat="server"></asp:Label>
                        </td>
                        <td class="style208">
                            Centro de costos:&nbsp;<asp:Label ID="lblCentroEmp" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style190">
                        </td>
                        <td class="style193">
                        </td>
                        <td class="style200">
                        </td>
                        <td class="style196">
                            <asp:Label ID="lblCentroSupUsu" runat="server" Text="Centros de Costos Empleado:"></asp:Label>&nbsp;
                            <asp:DropDownList ID="cboCentroSupUsu" runat="server" OnSelectedIndexChanged="cboCentroSupUsu_SelectedIndexChanged"
                                AutoPostBack="True" CssClass="ComboM">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td class="style120" id="TipoSer" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="PanelTipoSer" runat="server" Width="192px" GroupingText="Tipo de Servicio"
                                        Height="61px" Style="top: -10px; width: 177px; height: 67px; text-align: center;
                                        position: relative; left: 0px;">
                                        <asp:DropDownList ID="cboTipoServicio" runat="server" OnSelectedIndexChanged="cboTipoServicio_SelectedIndexChanged"
                                            AutoPostBack="True" CssClass="ComboM">
                                        </asp:DropDownList>
                                        <br />
                                        <br />
                                        <asp:Label ID="lblTituloSubSer" runat="server" Text="Sub Servicio Especial:" Style="font-size: 8pt"></asp:Label>
                                        <br />
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="cboNomSerESP" runat="server" OnSelectedIndexChanged="cboNomSerESP_SelectedIndexChanged"
                                                    AutoPostBack="True" CssClass="ComboM">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="style135" id="PedidoNormal" runat="server">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="PanelPedidoNormal" runat="server" Width="255px" GroupingText="Pedido"
                                        Height="100px">
                                        <table style="width: 254px">
                                            <tr>
                                                <td class="styleTextoCenN" colspan="2">
                                                    <asp:Label ID="lblTextModTipoSer1" runat="server"></asp:Label>&nbsp;
                                                    <asp:Label ID="lblModTipoSer1" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="styleTextoDer">
                                                    Cantidad:
                                                </td>
                                                <td class="style76">
                                                    <asp:TextBox ID="txtCantidadNormal" runat="server" CssClass="TexboxP"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="ftNoNumCantidadNormal" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtCantidadNormal">
                                                    </asp:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="styleTextoDer">
                                                    Fecha de atención:
                                                </td>
                                                <td class="style76">
                                                    <asp:TextBox ID="txtFechaAtenNormal" runat="server" CssClass="TexboxP"></asp:TextBox>
                                                    <asp:CalendarExtender ID="txtFechaAtenNormalCE" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtFechaAtenNormal">
                                                    </asp:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style209">
                                                    Menu:
                                                </td>
                                                <td class="style210">
                                                    <asp:DropDownList ID="cboMenuNor" runat="server" AutoPostBack="True" CssClass="ComboM">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <%-- <tr>
                                                <td class="style150">
                                                    Lugar de atención:
                                                </td>
                                                <td class="style76">
                                                    <asp:TextBox ID="txtLugarNormal" runat="server" Width="160px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style151">
                                                    &nbsp;&nbsp;
                                                </td>
                                                <td class="style76">
                                                    &nbsp;&nbsp;
                                                </td>
                                            </tr>--%>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td runat="server" id="PedidoEspecial">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:Panel ID="PanelPedidoEspecial" runat="server" GroupingText="Pedido" Height="166px"
                                        Width="679px">
                                        <table style="width: 658px; height: 146px;">
                                            <tr>
                                                <td class="styleTextoCenN" colspan="4">
                                                    <asp:Label ID="lblTextModTipoSer" runat="server"></asp:Label>&nbsp;
                                                    <asp:Label ID="lblModTipoSer" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style182">
                                                    Motivo de evento:
                                                </td>
                                                <td class="style184">
                                                    <asp:TextBox ID="txtDesESP" runat="server" Width="210px" Wrap="true" Height="45px"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                                <td class="style185">
                                                    Menu:
                                                </td>
                                                <td class="style114">
                                                    <asp:TextBox ID="txtMenuESP" runat="server" Width="210px" Wrap="true" Height="45px"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style182">
                                                    Valor unitario:
                                                </td>
                                                <td class="style184">
                                                    <asp:TextBox ID="txtValorUniESP" runat="server" AutoPostBack="True" OnTextChanged="txtValorUniESP_TextChanged"
                                                        CssClass="TexboxN"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="ftNoNumValorUniESP" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtValorUniESP">
                                                    </asp:FilteredTextBoxExtender>
                                                    &nbsp;&nbsp;&nbsp;&nbsp; Cantidad:
                                                    <asp:TextBox ID="txtCantidadESP" runat="server" AutoPostBack="True" OnTextChanged="txtCantidadESP_TextChanged"
                                                        CssClass="TexboxPMP"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="ftNoNumCantidadESP" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtCantidadESP">
                                                    </asp:FilteredTextBoxExtender>
                                                </td>
                                                <td class="style185">
                                                    Valor total:
                                                </td>
                                                <td class="style114">
                                                    <asp:Label ID="lblValorTotESP" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style182">
                                                    Fecha de atención:
                                                </td>
                                                <td class="style184">
                                                    <asp:TextBox ID="txtFechaAtenPedESP" runat="server" CssClass="TexboxP"></asp:TextBox>
                                                    <asp:CalendarExtender ID="txtFechaAtenPedEspCE" runat="server" Format="dd/MM/yyyy"
                                                        TargetControlID="txtFechaAtenPedEsp">
                                                    </asp:CalendarExtender>
                                                    <asp:DropDownList ID="cboHorasAten" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboHorasAten_SelectedIndexChanged"
                                                        CssClass="ComboP">
                                                    </asp:DropDownList>
                                                    <asp:Label ID="lblHoraAten" runat="server" Width="46px" Font-Size="8pt" ForeColor="Blue"></asp:Label>
                                                </td>
                                                <td class="style185">
                                                    Lugar de atención:
                                                </td>
                                                <td class="style114">
                                                    <asp:DropDownList ID="cboLugarESP" runat="server" AutoPostBack="true" CssClass="ComboM">
                                                    </asp:DropDownList>
                                                    <%-- <asp:TextBox ID="txtLugarESP" runat="server" Width="160px"></asp:TextBox>--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" class="style157">
                            <asp:Button ID="btnGuardar" runat="server" BackColor="#1C5AB6" BorderColor="#999999"
                                Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White" OnClick="btnGuardar_Click"
                                Text="Guardar" Width="70px" />
                            <%--<asp:Button ID="btnLimpiar" runat="server" BackColor="#1C5AB6" BorderColor="#999999"
                                Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White" OnClick="btnLimpiar_Click"
                                Text="Limpiar" Width="60px" />--%>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div class="overlay" />
            <div class="overlayContent">
                <asp:Label ID="lblEnviando" runat="server" Text="Enviando..." Font-Names="Arial"
                    Font-Size="14pt"></asp:Label>
                <img src="Imagenes/ajax-loader.gif" alt="Loading" height="30" style="text-align: center"
                    width="30" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>