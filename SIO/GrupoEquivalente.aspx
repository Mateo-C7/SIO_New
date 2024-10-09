<%@ Page Title="Grupo Equivalente" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="GrupoEquivalente.aspx.cs" Inherits="SIO.GrupoEquivalente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
        <div class="clear hideSkiplink">
                <asp:Menu ID="NavigationMenu" runat="server" CssClass="menu" EnableViewState="false" IncludeStyleBlock="false" Orientation="Horizontal">
                    <Items>
                        <asp:MenuItem NavigateUrl="~/itemPiciz.aspx" Text="Matriz de Integración"/>
                         <asp:MenuItem  Text="                  "/>
                        <asp:MenuItem NavigateUrl="~/GrupoEquivalente.aspx" Text="Grupo Equivalente"/>
                    </Items>
                </asp:Menu>
            </div>
    <asp:Panel ID="Panel1" runat="server" BorderColor="Silver" BorderStyle="Solid">
       
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="10pt" 
                Text="Grupo: "></asp:Label>
            <asp:DropDownList ID="lstEquiv" runat="server" AutoPostBack="True" 
                Font-Names="Arial" Font-Size="10pt" Height="27px" 
                onselectedindexchanged="lstEquiv_SelectedIndexChanged" Width="267px">
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btAdiciona" runat="server" onclick="btAdiciona_Click" 
                Text="Adicionar" BackColor="#1C5AB6" BorderColor="#999999" 
                Font-Bold="True" Font-Names="Arial" Font-Size="9pt" ForeColor="White" />
      
        <p>
            <asp:Label ID="lblDesc" runat="server" Text="Descripción: " Font-Names="Arial" 
                Font-Size="10pt"></asp:Label>
            <asp:TextBox ID="txtDesc" runat="server" Width="274px" Font-Names="Arial" 
                Font-Size="10pt"></asp:TextBox>
            &nbsp;<asp:CheckBox ID="chkGrpActivo" runat="server" Font-Names="Arial" 
                Font-Size="8pt" oncheckedchanged="chkActivo_CheckedChanged" Text="ACTIVO" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btEditar" runat="server" BackColor="#1C5AB6" 
                BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="9pt" 
                ForeColor="White" onclick="btEditar_Click" Text="Editar" Width="63px" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblError" runat="server" Text="lblError" Visible="False"></asp:Label>
        </p>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" BorderColor="Silver" BorderStyle="Solid" 
        Height="288px" ScrollBars="Vertical" Visible="False">
        <br />
        <asp:Label ID="lblEqui" runat="server" Text="ITEM_EQUI_PICIZ: "></asp:Label>
        <asp:TextBox ID="txtEquiv" runat="server" Width="79px"></asp:TextBox>
        &nbsp;
        <asp:Label ID="lblOri" runat="server" Text="ITEM_EQUI_ORIGEN: "></asp:Label>
        <asp:TextBox ID="txtOrig" runat="server" Width="79px"></asp:TextBox>
        &nbsp;
        <asp:Label ID="lblMed1" runat="server" Text="MEDIDA 1: "></asp:Label>
        <asp:TextBox ID="txtMed1" runat="server" Width="51px"></asp:TextBox>
        <asp:Label ID="lblMed2" runat="server" Text="MEDIDA 2: "></asp:Label>
        <asp:TextBox ID="txtMed2" runat="server" Width="50px"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox ID="chkActivo" runat="server" Font-Names="Arial" Font-Size="8pt" 
            oncheckedchanged="chkActivo_CheckedChanged" Text="ACTIVO" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btEditaItem" runat="server" onclick="btEditaItem_Click" 
            Text="Editar" BackColor="#1C5AB6" BorderColor="#999999" Font-Bold="True" 
            Font-Names="Arial" Font-Size="9pt" ForeColor="White" Width="60px" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btNuevo" runat="server" BackColor="#1C5AB6" 
            BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="9pt" 
            ForeColor="White" onclick="btNuevoItem_Click" Text="Nuevo" Width="60px" />
        <br />
        <br />
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
                <asp:BoundField DataField="ESTADO" HeaderText="Activo" />
                <asp:BoundField DataField="PZ_ITEMS_EQUI_ID" HeaderText="ID" />
                <asp:TemplateField HeaderText="SELECCIONAR">
                    <ItemTemplate>
                        <asp:LinkButton ID="LkSelec" runat="server" CausesValidation="false" 
                            CommandName="Select">
                                        <asp:Image ID="Imagerite0" ImageUrl="Imagenes/write.png" 
                            ImageAlign="Middle" runat="server" 
                                                ToolTip="Editar" onclick="btEdita_Click" />
                                        </asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="#1C5AB6" Font-Bold="True" ForeColor="White" 
                HorizontalAlign="Center" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView>
    </asp:Panel>
    <p>
        &nbsp;</p>
    <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </p>
    <asp:Label ID="lblMsg" runat="server" Text="No existen Item equivalentes." 
        Visible="False"></asp:Label>
</asp:Content>
