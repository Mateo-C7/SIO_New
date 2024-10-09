<%@ Page Title="Item Equivalente" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="itemEquivalente.aspx.cs" Inherits="SIO.itemEquivalente" %>
<%@ PreviousPageType VirtualPath="~/itemPiciz.aspx" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <div style="height: 190px">
        <asp:Label ID="lblMsg" runat="server" Text="No existen Item equivalentes." 
            Visible="False"></asp:Label>
        <asp:GridView ID="grdDataEquiv" runat="server" AllowSorting="True" 
            AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
            BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Font-Names="arial" 
            Font-Size="8pt" ForeColor="Black" GridLines="Vertical" Height="122px" 
            OnPageIndexChanging="grdDataEquiv_OnPageIndexChanging" 
            onselectedindexchanged="grdDataEquiv_SelectedIndexChanged" PageSize="15" 
            Width="741px">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <AlternatingRowStyle BackColor="Gainsboro" />
            <Columns>
                <asp:BoundField DataField="PZ_ITEM_EQUI_PICIZ" HeaderText="ITEM" />
                <asp:BoundField DataField="PZ_ITEM_EQUI_ORI" HeaderText="ORIGEN" />
                <asp:BoundField DataField="PZ_ITEM_EQUI_MED1" HeaderText="MEDIDA1" />
                <asp:BoundField DataField="PZ_ITEM_EQUI_MED2" HeaderText="MEDIDA2" />
                <asp:BoundField DataField="SALDO_PZ" HeaderText="SALDO_PZ" />
                <asp:TemplateField HeaderText="SELECCIONAR">
                    <ItemTemplate>
                        <asp:LinkButton ID="LkSelec" runat="server" CausesValidation="false" 
                            CommandName="Select">
                                        <asp:Image ID="Imagerite0" ImageUrl="Imagenes/write.png" ImageAlign="Middle" runat="server" 
                                                ToolTip="Editar" onclick="btEdita_Click" />
                                        </asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" 
                HorizontalAlign="Center" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView>
    </div>
</asp:Content>
