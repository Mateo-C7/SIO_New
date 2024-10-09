<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true"
    CodeBehind="Roles.aspx.cs" Inherits="SIO.Roles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        .style4
        {
            width: 96px;
        }
        .style8
        {
            width: 40px;
            text-align: right;
        }
        .style21
        {
            width: 86px;
        }
        .style23
        {
            width: 1px;
        }
        .style24
        {
            font-size: 8pt;
            width: 66px;
            text-align: right;
        }
        .style26
        {
            font-family: Arial;
        }
        .style27
        {
            width: 63px;
        }
        .style28
        {
            width: 94px;
        }
        .style29
        {
            width: 53px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:Panel ID="PanelRol" runat="server" Font-Names="Arial" Height="690px" ScrollBars="Auto"
        Width="726px" GroupingText="Creacion de Roles" Style="margin-right: 0px">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="width: 680px; height: 34px;">
                    <tr>
                        <td class="style24">
                            Nombre Rol :
                        </td>
                        <td class="style21">
                            <asp:TextBox ID="txtNomRol" runat="server" Width="165px" CssClass="style26" Font-Size="8pt"></asp:TextBox>
                        </td>
                        <td class="style23">
                        </td>
                        <td class="style4">
                            <asp:CheckBox ID="chkVice" Text="Vicepresidente" runat="server" Font-Size="8pt" />
                        </td>
                        <td class="style27">
                            <asp:CheckBox ID="chkGer" Text="Gerente" runat="server" Font-Size="8pt" />
                        </td>
                        <td class="style28">
                            <asp:CheckBox ID="chkRep" Text="Representante" runat="server" Font-Size="8pt" />
                        </td>
                        <td class="style29">
                            <asp:CheckBox ID="chkVia" Text="Viajes" runat="server" Font-Size="8pt" />
                        </td>
                        <td class="style8">
                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" BackColor="#1C5AB6" BorderColor="#999999"
                                Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White" Height="20px"
                                Width="69px" OnClick="btnAgregar_Click" Style="text-align: center" />
                        </td>
                    </tr>
                </table>
                <div style="width: 677px; height: 10px; border-top: 1px solid black;">
                </div>
                <table style="width: 675px">
                    <tr>
                        <td>
                            <asp:GridView ID="grdRoles" runat="server" BackColor="White" BorderColor="#999999"
                                BorderStyle="None" BorderWidth="1px" DataKeyNames="idRol" CellPadding="3" GridLines="Vertical"
                                Width="674px" AutoGenerateColumns="False" Font-Size="8pt" Font-Names="arial"
                                Height="16px">
                                <AlternatingRowStyle BackColor="Gainsboro" />
                                <Columns>
                                    <asp:BoundField DataField="idRol" HeaderText="Codigo" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="30pt" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nomRol" HeaderText="Nombre" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Vicepresidente" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkActivoVice" Checked='<%# Bind("viceRol") %>' runat="server"
                                                AutoPostBack="true" OnCheckedChanged="chkActivoVice_CheckedChanged" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="40pt" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gerente" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkActivoGen" Checked='<%# Bind("genRol") %>' runat="server" AutoPostBack="true"
                                                OnCheckedChanged="chkActivoGen_CheckedChanged" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="40pt" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Representante" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkActivoRep" Checked='<%# Bind("repRol") %>' runat="server" AutoPostBack="true"
                                                OnCheckedChanged="chkActivoRep_CheckedChanged" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="40pt" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Viajes" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkActivoVia" Checked='<%# Bind("viaRol") %>' runat="server" AutoPostBack="true"
                                                OnCheckedChanged="chkActivoVia_CheckedChanged" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="40pt" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Eliminar" meta:resourcekey="TemplateFieldResource1"
                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Button ID="BtnEliminar" runat="server" Style="background-image: url('iconosMetro/deleteimage.png');
                                                background-repeat: no-repeat;" Height="20px" Width="20px" OnClick="BtnEliminar_Click"
                                                OnClientClick="return confirm('Seguro que desea eliminar el Rol?');" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="40pt" />
                                    </asp:TemplateField>
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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
    </asp:Panel>
</asp:Content>