<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true"
    CodeBehind="CasinoMenus.aspx.cs" Inherits="SIO.CasinoMenus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/StyleGeneral.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function abrirPopup(ventana) {
            $('#fondoOsc').fadeIn('slow');
            $('#' + ventana).fadeIn('slow');
        }
        function cerrarPopup(popup) {
            $('#' + popup).fadeOut('slow');
            $('#fondoOsc').fadeOut('slow');
        }
    </script>
    <style type="text/css">
        .StPopupBMenu
        {
            position: absolute;
            top: 35%;
            left: 40%;
            padding: 16px;
            background: #fff;
            color: #333;
            z-index: 1002;
            overflow: auto;
            border-radius: 25px;
            display: inline-block;
        }
        .style2
        {
            text-align: right;
            height: 55px;
        }
        .style3
        {
            height: 55px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelGeneral" runat="server" Height="152px" CssClass="Letra" Width="474px"
                GroupingText="Menu de Servicios">
                <table style="height: 134px; width: 465px">
                    <tr>
                        <td class="TexDer">
                            Servicio :
                        </td>
                        <td>
                            <asp:DropDownList ID="cboSerMenu" runat="server" CssClass="ComboM" AutoPostBack="true"
                                OnSelectedIndexChanged="cboSerMenu_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Descripción :
                        </td>
                        <td class="style3">
                            <asp:TextBox ID="txtDesMenu" runat="server" Width="246px" Wrap="true" Height="45px"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td class="style3">
                            Activo :
                            <asp:CheckBox ID="chkEstMenu" runat="server" AutoPostBack="true" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" class="TexCen">
                            <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="Botones" OnClick="btnNuevo_Click" />
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="Botones" OnClick="btnGuardar_Click" />
                            <asp:Button ID="btnAct" runat="server" Text="Actualizar" CssClass="Botones" OnClick="btnAct_Click" />
                            <asp:Button ID="btnBuscar" runat="server" Text="Consultar" CssClass="Botones" OnClick="btnBuscar_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="fondoOsc" class="fondoOscuro">
    </div>
    <div id="PopupBuscaMenu" class="StPopupBMenu" style="display: none;">
        <div class="close" style="height: 22px;">
            <a href="#" id="A2" onclick="cerrarPopup('PopupBuscaMenu');">
                <img alt="Cerrar" src="iconosMetro/close.png" /></a></div>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <asp:Panel ID="PanelFiltro" runat="server" Height="170px" CssClass="Letra" Width="412px"
                    GroupingText="Consulta Menus">
                    <table>
                        <tr>
                            <td class="TexDer">
                                Servicio :
                            </td>
                            <td>
                                <asp:DropDownList ID="cboSerBusMenu" runat="server" CssClass="ComboM" AutoPostBack="true"
                                    OnSelectedIndexChanged="cboSerBusMenu_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <div style="overflow: auto; display: inline-block; width: 393px; height: 109px">
                                    <asp:GridView ID="grdMenus" runat="server" BackColor="White" BorderColor="#999999"
                                        BorderStyle="None" BorderWidth="1px" DataKeyNames="idMenu" CellPadding="3" GridLines="Vertical"
                                        Width="384px" AutoGenerateColumns="False" Font-Size="8pt" Font-Names="arial"
                                        Height="16px">
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                        <Columns>
                                            <asp:BoundField DataField="idMenu" HeaderText="idMenu" Visible="false" />
                                            <asp:TemplateField HeaderText="+" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnSelMenu" CommandArgument='<%# Container.DataItemIndex%>'
                                                        runat="server" Height="24px" ImageUrl="~/iconosMetro/adicionar1.png" Width="25px"
                                                        OnClick="btnSelMenu_Click" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="15pt" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="nomMenu" HeaderText="Descripcion" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="80pt" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="actMenu" HeaderText="Activo ?" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="25pt" />
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
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
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
