<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true"
    CodeBehind="PoliticasModulo.aspx.cs" Inherits="SIO.PoliticasModulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <link href="Styles/StyleGeneral.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style2
        {
            font-size: 8pt;
            text-align: right;
            width: 39px;
        }
        .style3
        {
            width: 265px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="ContentPlaceHolder4">
    <asp:Panel ID="PanelRol" runat="server" Font-Names="Arial" Height="681px" ScrollBars="Auto"
        Width="352px" GroupingText="Politicas por Modulo">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="height: 38px; width: 285px">
                    <tr>
                        <td class="style2">
                            Rol :
                        </td>
                        <td class="style3">
                            <asp:DropDownList ID="cboRoles" runat="server" Height="18px" Width="248px" Font-Size="8pt"
                                AutoPostBack="true" OnSelectedIndexChanged="cboRoles_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <div style="width: 318px; height: 10px; border-top: 1px solid black;">
                </div>
                <table style="width: 321px">
                    <tr>
                        <td>
                            <asp:GridView ID="grdModulos" runat="server" BackColor="White" BorderColor="#999999"
                                BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="309px"
                                AutoGenerateColumns="False" Font-Size="8pt" Font-Names="arial" 
                                Height="16px">
                                <AlternatingRowStyle BackColor="Gainsboro" />
                                <Columns>
                                    <asp:BoundField DataField="idMod" HeaderText="N#" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="20pt" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nomMod" HeaderText="Modulo" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="120pt" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="principal" HeaderText="Sub Modulo" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="120pt" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Activar?" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkActivoMod" Checked='<%# Bind("activoMod") %>' runat="server"
                                                AutoPostBack="true" OnCheckedChanged="chkActivoMod_CheckedChanged" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="20pt" />
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