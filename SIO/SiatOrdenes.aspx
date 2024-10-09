<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true"
    CodeBehind="SiatOrdenes.aspx.cs" Inherits="SIO.SiatOrdenes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/StyleGeneral.css" rel="stylesheet" type="text/css" />
    <link href="Styles/StyleSIAT.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $('#ContentPlaceHolder4_txtOrden').keypress(function (e) {
                var code = null;
                code = (e.keyCode ? e.keyCode : e.which);
                return (code == 32) ? false : true;
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelBuscarOrd" runat="server" CssClass="Letra" GroupingText="OF"
                Width="1056px">
                <table style="width: 400px">
                    <tr>
                        <td>
                            <asp:Label ID="lblYear" runat="server" Text="Año :"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtYear" runat="server" CssClass="TexboxP"></asp:TextBox>
                        </td>
                        <td class="TexIzq">Orden : &nbsp;&nbsp;
                            <asp:TextBox ID="txtOrden" runat="server" CssClass="TexboxP"></asp:TextBox>
                            &nbsp;&nbsp;
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="Botones" OnClick="btnBuscar_Click" />
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <div style="overflow: auto; width: 1036px; height: 513px">
                                <asp:GridView ID="grdOrdenes" runat="server" BackColor="White" BorderColor="#999999"
                                    BorderStyle="None" BorderWidth="1px" DataKeyNames="idOf, idCliente, idObra, moneda, valor" CellPadding="3"
                                    GridLines="Vertical" Width="1245px" AutoGenerateColumns="False" Font-Size="8pt"
                                    Font-Names="arial" Height="36px">
                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                    <Columns>
                                        <asp:BoundField DataField="idOf" HeaderText="idOf" Visible="false" />
                                        <asp:BoundField DataField="idCliente" HeaderText="idCliente" Visible="false" />
                                        <asp:BoundField DataField="idObra" HeaderText="idObra" Visible="false" />
                                        <asp:TemplateField HeaderText="+" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnSelOrden" runat="server" Height="24px" ImageUrl="~/iconosMetro/adicionar1.png"
                                                    Width="25px" OnClick="btnSelOrden_Click" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="20pt" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="orden" HeaderText="Orden" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="Left" Width="30pt" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="valor" HeaderText="Valor" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="Right" Width="30pt" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="mon_descripcion" HeaderText="Moneda" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="Left" Width="30pt" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fechaDesPlan" HeaderText="Fecha desp. plan" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="Center" Width="55pt" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fechaDesReal" HeaderText="Fecha desp. real" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="Center" Width="55pt" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fechaLlegObra" HeaderText="Fecha lleg. Obra" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="Center" Width="55pt" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="cliente" HeaderText="Cliente" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="Left" Width="165pt" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="datosObra" HeaderText="Obra" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemStyle HorizontalAlign="Left" Width="280" />
                                        </asp:BoundField>                                        
                                        <asp:BoundField DataField="moneda" HeaderText="moneda" Visible="false" />                                     
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
