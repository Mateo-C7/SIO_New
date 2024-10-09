<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AyudantesLog.aspx.cs" Inherits="SIO.AyudantesLog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.4.1.min.js"></script>
    <script type="text/javascript">
        function EliminarConfirm() {
            if (confirm('Desea Realmente Eliminarlo?')) {
                return true;
            }
            else {
                return false;
            }
        }
        function cerrarpagina() {
            window.close();
            return false;
        }
        function imprimir() {
            document.getElementById("btnImprimir").style.display = 'none';
            document.getElementById("btnCerrar").style.display = 'none';
            document.getElementById("BtnEliminar").style.display = 'none';
            //se imprime la pagina
            window.print();
            //reaparece el boton
            window.close();
        }
        //solo Numeros isNumberKeyfalse //implementar onkeypress="return solonumeros(event);"
        function solonumeros(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>
    <style type="text/css">
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
        .watermarked
        {
            padding: 2px 0 0 2px;
            border: 1px solid #BEBEBE;
            background-color: white;
            color: Gray;
            font-family: Arial;
            font-weight: lighter;
        }
        .CustomComboBoxStyle button
        {
            background-image: url('http://app.forsa.com.co/SIOMaestros/Imagenes/toolkit-arrow.gif');
            background-repeat: no-repeat;
            border-style: none;
        }
        .CustomComboBoxStyle input
        {
            background-image: url('http://app.forsa.com.co/SIOMaestros/Imagenes/toolkit-bg.gif');
            background-repeat: no-repeat;
            border-style: none;
        }
        .CustomComboBoxStyle .ajax__combobox_itemlist li
        {
            color: red;
            font-size: 11pt;
            font-family: Arial;
            background-color: #EBEBEB;
        }
        .A:hover
        {
            background: white;
        }
        .botonsio:hover
        {
            color: white;
            background: blue;
        }
        .center
        {
            font-family: Arial;
            font-size: 8pt;
            text-align: Center;
        }
        .sangria
        {
            word-spacing: 10pt;
            font-family: Tahoma;
            font-size: 11pt;
            color: #1C5AB6;
        }
        #txtIdentificacion
        {
            width: 130px;
        }
        #Button1
        {
            width: 132px;
            height: 39px;
        }
        #btnCerrar1
        {
            width: 143px;
            height: 36px;
        }
        #btnCerrar2
        {
            width: 91px;
        }
        .CustomComboBoxStyle
        {
        }
        .style278
        {
            width: 741px;
            height: 44px;
            font-size: 22px;
            background-color: #1C5AB6;
            color: White;
            font-family: Arial;
        }
        .style279
        {
            width: 741px;
            height: 44px;
            font-size: 22px;
            color: Black;
            font-family: Arial;
        }
        .style284
        {
            width: 385px;
        }
        .style296
        {
            width: 89px;
        }
        #btnImrprimir
        {
            margin-bottom: 0px;
            width: 38px;
            height: 35px;
        }
        .style304
        {
            width: 957px;
        }
        .style305
        {
            width: 208px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="Panel1" runat="server">
            <table id="tablaAyu" style="width: 1055px">
                <tr id="fila1" class="style279">
                    <td id="colum1" class="style296">
                        <asp:Button ID="btnCerrar" runat="server" Style="background-image: url('iconosMetro/cerrar.jpg');
                            background-repeat: no-repeat;" OnClientClick="return cerrarpagina();" OnClick="btnCerrar_Click"
                            Height="57px" Width="60px" />
                    </td>
                    <td id="colum2" class="style305">
                        <input id="btnImprimir" runat="server" style="background-image: url('iconosMetro/imprimir.png');
                            width: 53pt; height: 30pt; background-repeat: no-repeat;" type="button" onclick="imprimir();" />
                    </td>
                    <td class="style304">
                        <asp:Label ID="lblContendor" runat="server" Text="Contendor: " Width="97px" Style="text-align: right"></asp:Label>
                        &nbsp;&nbsp;
                        <asp:Label ID="lblContenedor1" runat="server" Width="86px"></asp:Label>
                    </td>
                </tr>
                <tr id="fila2" class="style278">
                    <td class="style296">
                        <asp:Label ID="lblCedula" runat="server" Text="Cedula: " Width="74px" Style="text-align: right"></asp:Label>
                    </td>
                    <td class="style305">
                        <asp:Panel ID="Panel2" runat="server" DefaultButton="Button1" Width="195px" Height="33px">
                            <input id="txtCedula" style="color: White; font-size: 30px; background-color: #1C5AB6;
                                width: 190px; height: 30px; text-align: right" runat="server" type="text" onkeypress="return solonumeros(event)"></input>
                            <asp:Button ID="Button1" runat="server" Text="Button1" Style="display: none" OnClick="Button1_Click" />
                        </asp:Panel>
                    </td>
                    <td class="style304">
                        <asp:Label ID="lblNombre" runat="server" Text="Nombre: " Style="text-align: right;
                            margin-bottom: 0px;" Width="109px"></asp:Label>
                        &nbsp;<asp:Label ID="lblNombre1" runat="server" Width="373px"></asp:Label>
                    </td>
                    <td class="style284">
                        <asp:Label ID="lblceu" Text="CC.: " runat="server" Width="65px" Style="text-align: right"></asp:Label>
                        &nbsp;
                        <asp:Label ID="lblCedula1" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="style279">
                    <td class="style296">
                    </td>
                    <td colspan="2">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblMensaje" runat="server" Width="341px" Style="text-align: center"></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:GridView ID="GridView2" DataKeyNames="id" runat="server" CellPadding="3" GridLines="Vertical"
                            Width="1052px" HorizontalAlign="Right" AutoGenerateColumns="False" BorderStyle="None"
                            BorderWidth="1px">
                            <AlternatingRowStyle BackColor="white" />
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="Id" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="cedula" HeaderText="Cedula" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="nombre" HeaderText="Nombre" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="fecha" HeaderText="Fecha" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="placa" HeaderText="Placa" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="Eliminar" meta:resourcekey="TemplateFieldResource1"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Button ID="BtnEliminar" runat="server" Style="background-image: url('iconosMetro/eliminar.jpg');
                                            background-repeat: no-repeat;" Height="57px" Width="60px" OnClick="BtnEliminar_Click"
                                            OnClientClick="return confirm('Seguro que desea eliminar el Usuario?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
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
        </asp:Panel>
    </div>
    <div>
        <b></b>
    </div>
    </form>
</body>
</html>