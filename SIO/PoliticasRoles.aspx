<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true"
    CodeBehind="PoliticasRoles.aspx.cs" Inherits="SIO.PoliticasRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/StyleGeneral.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style4
        {
            width: 10px;
        }
        .style5
        {
            width: 236px;
        }
        .style6
        {
            font-size: 8pt;
            text-align: right;
            width: 22px;
        }
        .style7
        {
            font-size: 8pt;
            text-align: right;
            width: 52px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:Panel ID="PanelRol" runat="server" Font-Names="Arial" Height="690px" ScrollBars="Auto"
        Width="605px" GroupingText="Politicas por Rol">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table style="height: 37px; width: 572px">
                    <tr>
                        <td class="style6">
                            Rol :
                        </td>
                        <td class="style5">
                            <asp:DropDownList ID="cboRoles" runat="server" Height="18px" Width="225px" Font-Size="8pt"
                                AutoPostBack="true" OnSelectedIndexChanged="cboRoles_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style4">
                        </td>
                        <td class="style7">
                            Modulo :
                        </td>
                        <td>
                            <asp:DropDownList ID="cboModulos" AutoPostBack="true" runat="server" Height="18px"
                                Width="225px" Font-Size="8pt" OnSelectedIndexChanged="cboModulos_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <div style="width: 565px; height: 10px; border-top: 1px solid black;">
                </div>
                <table class="table table-hover">
                    <tr>
                        <td>
                            <asp:GridView ID="grdRutinas" runat="server" BackColor="White" DataKeyNames="idRut"
                                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
                                Width="565px" AutoGenerateColumns="False" Font-Size="8pt" Font-Names="arial"
                                Height="16px">
                                <AlternatingRowStyle BackColor="Gainsboro" />
                                <Columns>
                                    <asp:TemplateField HeaderText="idRut" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="idRut" runat="server" Text='<%# Bind("idRut") %>' Font-Size="11pt">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="nomRut" HeaderText="Actividad" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Agregar" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkActivoAgr" Checked='<%# Bind("agrRut") %>' runat="server" AutoPostBack="true"
                                                OnCheckedChanged="chkActivoAgr_CheckedChanged" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="40pt" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Eliminar" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkActivoEli" Checked='<%# Bind("eliRut") %>' runat="server" AutoPostBack="true"
                                                OnCheckedChanged="chkActivoEli_CheckedChanged" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="40pt" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Imprimir" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkActivoImp" Checked='<%# Bind("impRut") %>' runat="server" AutoPostBack="true"
                                                OnCheckedChanged="chkActivoImp_CheckedChanged" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="40pt" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Editar" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkActivoEdi" Checked='<%# Bind("ediRut") %>' runat="server" AutoPostBack="true"
                                                OnCheckedChanged="chkActivoEdi_CheckedChanged" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="40pt" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Activar?" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkActivoRut" Checked='<%# Bind("activoRut") %>' runat="server"
                                                AutoPostBack="true" OnCheckedChanged="chkActivoMod_CheckedChanged" />
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