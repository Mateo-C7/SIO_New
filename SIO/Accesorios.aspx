<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="Accesorios.aspx.cs" Inherits="SIO.Accesorios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <div id = "dvGrid" style ="padding:10px;width:550px">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
<asp:GridView ID="GridView1" runat="server"  Width = "550px"
AutoGenerateColumns = "False" Font-Names = "Arial"
Font-Size = "11pt" AlternatingRowStyle-BackColor = "#C2D69B" 
HeaderStyle-BackColor = "green" AllowPaging ="True"  ShowFooter = "True" 
OnPageIndexChanging = "OnPaging" onrowediting="EditCustomer"
onrowupdating="UpdateCustomer"  onrowcancelingedit="CancelEdit" 
        onselectedindexchanged="GridView1_SelectedIndexChanged" >
<Columns>
<asp:TemplateField ItemStyle-Width = "30px"  HeaderText = "CustomerID">
    <ItemTemplate>
        <asp:Label ID="lblCustomerID" runat="server"
        Text='<%# Eval("acc_id")%>'></asp:Label>
    </ItemTemplate>
    <FooterTemplate>
        <asp:TextBox ID="txtCustomerID" Width = "40px"
            MaxLength = "5" runat="server"></asp:TextBox>
    </FooterTemplate>
    <ItemStyle Width="30px" />
</asp:TemplateField>
<asp:TemplateField ItemStyle-Width = "100px"  HeaderText = "Name">
    <ItemTemplate>
        <asp:Label ID="lblContactName" runat="server"
                Text='<%# Eval("acc_desc_esp")%>'></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:TextBox ID="txtContactName" runat="server"
            Text='<%# Eval("acc_desc_esp")%>'></asp:TextBox>
    </EditItemTemplate> 
    <FooterTemplate>
        <asp:TextBox ID="txtContactName" runat="server"></asp:TextBox>
    </FooterTemplate>
    <ItemStyle Width="100px" />
</asp:TemplateField>
<asp:TemplateField ItemStyle-Width = "150px"  HeaderText = "Company">
    <ItemTemplate>
        <asp:Label ID="lblCompany" runat="server"
            Text='<%# Eval("acc_desc_esp")%>'></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:TextBox ID="txtCompany" runat="server"
            Text='<%# Eval("acc_desc_esp")%>'></asp:TextBox>
    </EditItemTemplate> 
    <FooterTemplate>
        <asp:TextBox ID="txtCompany" runat="server"></asp:TextBox>
    </FooterTemplate>
    <ItemStyle Width="150px" />
</asp:TemplateField>
<asp:TemplateField>
    <ItemTemplate>
        <asp:LinkButton ID="lnkRemove" runat="server"
            CommandArgument = '<%# Eval("acc_id")%>'
         OnClientClick = "return confirm('Do you want to delete?')"
        Text = "Delete" OnClick = "DeleteCustomer"></asp:LinkButton>
    </ItemTemplate>
    <FooterTemplate>
        <asp:Button ID="btnAdd" runat="server" Text="Add"
            OnClick = "AddNewCustomer" />
    </FooterTemplate>
</asp:TemplateField>
<asp:CommandField  ShowEditButton="True" ShowSelectButton="True" />
    <asp:ButtonField CommandName="Select" Text="Botón" />
    <asp:CommandField ShowSelectButton="True" />
</Columns>
<AlternatingRowStyle BackColor="#C2D69B"  />
    <HeaderStyle BackColor="Green" />
</asp:GridView>
</ContentTemplate>
<Triggers>
<asp:AsyncPostBackTrigger ControlID = "GridView1" />
</Triggers>
</asp:UpdatePanel>
</div>
</asp:Content>
