<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CasinoBuscarPedidos.aspx.cs"
    Inherits="SIO.VerPedidos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function cerrarpagina() {
            window.close();
        }
    </script>
</head>
<body style="height: 275px; width: 1168px">
    <form id="form1" runat="server">
    <div>
        <table style="width: 1165px">
            <tr>
                <td style="text-align: right">
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/iconosMetro/cerrar.jpg"
                        Width="30px" Style="text-align: right" OnClick="ImageButton1_Click" Height="28px" />
                </td>
            </tr>
        </table>
        <div style="overflow: auto;">
            <asp:Panel ID="panelVerPedidos" runat="server" GroupingText="Pedidos del Usuario"
                Font-Names="arial" Font-Size="9pt" Height="227px" Width="1161px">
                <asp:GridView ID="grdVerPedido" DataKeyNames="numPedido" runat="server" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                    Font-Names="arial" CellPadding="1" GridLines="Vertical" Width="1151px" Height="16px"
                    Font-Size="9pt">
                    <AlternatingRowStyle BackColor="Gainsboro" />
                    <Columns>
                        <asp:BoundField DataField="numPedido" HeaderText="N.Pedido">
                            <ItemStyle HorizontalAlign="Center" Width="50pt" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipoSer" HeaderText="Tipo de Servicio">
                            <ItemStyle HorizontalAlign="Left" Width="100pt" />
                        </asp:BoundField>
                        <asp:BoundField DataField="area" HeaderText="Area">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaCrea" HeaderText="Fecha Creacion">
                            <ItemStyle HorizontalAlign="Center" Width="70pt" />
                        </asp:BoundField>
                        <asp:BoundField DataField="horaCrea" HeaderText="Hora Creacion">
                            <ItemStyle HorizontalAlign="Center" Width="70pt" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechaAten" HeaderText="Fecha Atencion">
                            <ItemStyle HorizontalAlign="Center" Width="70pt" />
                        </asp:BoundField>
                        <asp:BoundField DataField="cantidad" HeaderText="Cant.">
                            <ItemStyle HorizontalAlign="Right" Width="30pt" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion" HeaderText="Motivo del Evento">
                            <ItemStyle HorizontalAlign="Left" Width="150pt" />
                        </asp:BoundField>
                        <asp:BoundField DataField="lugar" HeaderText="Lugar">
                            <ItemStyle HorizontalAlign="Left" Width="90pt" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Selecionar">
                            <ItemTemplate>
                                <asp:Button ID="btnSelecionar" runat="server" Height="32px" Style="background-image: url('/iconosMetro/guardar2.png');
                                    background-repeat: no-repeat;" Width="35px" OnClick="btnSelecionar_Click" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="50pt" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#000065" />
                </asp:GridView>
            </asp:Panel>
        </div>
    </div>
    </form>
</body>
</html>