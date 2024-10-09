<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true"
    CodeBehind="SiatAprobacion.aspx.cs" Inherits="SIO.SiatAprobacion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/ScriptSIAT.js" type="text/javascript"></script>
    <link href="Styles/StyleGeneral.css" rel="stylesheet" type="text/css" />
    <link href="Styles/StyleSIAT.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style2
        {
            width: 229px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"> 
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelGeneral" runat="server" Height="130px" CssClass="Letra" Width="885px">
                <table style="height: 114px; width: 874px">
                    <tr>
                        <td class="style2">
                            <table style="height: 118px; width: 285px">
                                <tr>
                                    <td class="TexDer">
                                        Tecnico :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cboTecnico" runat="server" class="ComboM" AutoPostBack="true"
                                            OnSelectedIndexChanged="cboTecnico_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TexDer">
                                        Dias Viajes:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDiasV" runat="server" Text=""></asp:Label>
                                        &nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TexDer">
                                        Porcentaje :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPorce" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TexDer">
                                        Dias Compensatorio :
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDiasC" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="text-align: center">
                                        <asp:Button ID="btnApro" runat="server" Text="Aprobar" CssClass="Botones" OnClientClick="return confirm('Seguro que desea aprobar el compensatorio?');"
                                            OnClick="btnApro_Click" />
                                    </td>
                                </tr>
                    </tr>
                </table>
                </td>
                <td>
                    <div style="overflow: auto; width: 574px; height: 91px">
                        <asp:GridView ID="grdCompen" runat="server" BackColor="White" BorderColor="#999999"
                            BorderStyle="None" BorderWidth="1px" DataKeyNames="idViaje" CellPadding="3" GridLines="Vertical"
                            Width="537px" AutoGenerateColumns="False" Font-Size="8pt" Font-Names="arial"
                            Height="16px" Style="margin-left: 14px">
                            <AlternatingRowStyle BackColor="Gainsboro" />
                            <Columns>
                                <asp:BoundField DataField="idViaje" HeaderText="idViaje" Visible="false" />
                                <asp:TemplateField HeaderText="+" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton CommandArgument='<%# Container.DataItemIndex%>' ID="btnSelComp"
                                            runat="server" Height="24px" ImageUrl="~/iconosMetro/adicionar1.png" Width="25px"
                                            OnClick="btnSelComp_Click" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle Width="20pt" HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="fechaInicio" HeaderText="Incio Viaje" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="30pt" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fechaFin" HeaderText="Fin Viaje" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="30pt" />
                                </asp:BoundField>
                                <asp:BoundField DataField="diasViaje" HeaderText="Dias" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="25pt" />
                                </asp:BoundField>
                                <asp:BoundField DataField="cliente" HeaderText="Cliente" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="65pt" />
                                </asp:BoundField>
                                <asp:BoundField DataField="pais" HeaderText="Pais" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="40pt" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ciudad" HeaderText="Ciudad" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" Width="40pt" />
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
                </tr> </table>
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
