<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Grid.aspx.cs" Inherits="SIO.Grid" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function cerrar() {
         document.getElementById('btnCerrar').focus();
        }
     </script>
    <style type="text/css">
        .style278
        {
            width: 741px;
            height: 44px;
            font-size: 22px;
            background-color: #1C5AB6;
            color: white;
            font-family: Arial;
            
        }
        .style284
        {
            width: 282px;
            height: 44px;
            font-size: 22px;
            background-color: #1C5AB6;
            color: white;
            font-family: Arial;
        }
        .style285
        {
            width: 693px;
            height: 44px;
            font-size: 22px;
            background-color: #1C5AB6;
            color: white;
            font-family: Arial;
        }
        .style286
        {
            width: 274px;
            height: 44px;
            font-size: 22px;
            background-color: #1C5AB6;
            color: white;
            font-family: Arial;
        }
        .style287
        {
            width: 317px;
            height: 44px;
            font-size: 22px;
            background-color: #1C5AB6;
            color: white;
            font-family: Arial;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <table style="width: 1054px">

                <tr class="style278">
                <td class="style287">
                Orden: <asp:Label ID="lblOrden3" runat="server" Text="0"></asp:Label>
                </td>
                <td class="style284">
                Despacho: <asp:Label ID="lblDespacho3" runat="server" Text="0"></asp:Label>
                </td>
                <td class="style286">
                Contenedor: <asp:Label ID="lblContenedor3" runat="server" Text="0"></asp:Label>
                </td>

                 <td class="style285">
                Peso Total: <asp:Label ID="lblPesoT" runat="server" Text="0"></asp:Label>
                </td>
                </tr>

        </table>
                <table>
                    <tr>
                        <td class="style278">
                            <asp:GridView ID="GridView1" runat="server" CellPadding="3" GridLines="Vertical"
                                Width="1052px" HorizontalAlign="Right" AutoGenerateColumns="False" 
                                BorderStyle="None" BorderWidth="1px">
                                <AlternatingRowStyle BackColor="white" />
                                <Columns>
                                    <asp:BoundField DataField="Estiba" HeaderText="Estiba">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Pallet" HeaderText="Pallet">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Peso" HeaderText="Peso">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Volumen" HeaderText="Volumen">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Piezas" HeaderText="Piezas" ItemStyle-HorizontalAlign="Center"/>
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
                        <td>
                        
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<input id="btnCerrar" type="button" value="Cerrar"  onclick="window.close();" style="width: 79px; text-align: right; height: 45px; font-size: 22px; color: White; font-family: Arial; background-color:#1C5AB6"/></td>
                    </tr>
                </table>
    
    </div>
    </form>
</body>
</html>
