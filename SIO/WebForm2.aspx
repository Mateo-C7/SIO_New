<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="SIO.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:FileUpload ID="filePlano" runat="server"/>
        <asp:Button runat="server" ID="UploadButton" Text="Subir" Width="70px" BackColor="#1C5AB6"
            Font-Bold="True" Visible="true" OnClick="UploadButton_Click" Font-Names="Arial" ForeColor="White" Font-Size="8pt" />
        <br />
        <br />
        
    <asp:GridView runat="server" ID="GridView1" BackColor="White" BorderColor="#999999"
        BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="40%" PageSize="10" AllowPaging="true"
        AutoGenerateColumns="False" Font-Size="7pt" Font-Names="arial" Height="16px" OnRowDeleting="GridView1_RowDeleting" OnPageIndexChanging="GridView1_PageIndexChanging">
        <AlternatingRowStyle BackColor="Gainsboro" />
        <Columns>
            <asp:TemplateField HeaderText="N°">
                <ItemTemplate>
                    <asp:Label runat="server" Text='<%#Container.DataItemIndex + 1%>' ID="consecutivo"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="cot_item" runat="server" Text='<%# Bind("cot_item") %>' Style="text-transform: uppercase; text-align: right" MaxLength="100" Enabled="false"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:Label ID="path_plano" runat="server" Text='<%# Bind("path_plano") %>' Style="text-transform: uppercase; text-align: right" MaxLength="100" Enabled="false"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField Visible="true" HeaderText="Planos">
                <ItemTemplate>
                    <asp:Label ID="name_plano" runat="server" Text='<%# Bind("name_plano") %>' Style="text-align: right" MaxLength="100" Enabled="false"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Eliminar">
                <ItemTemplate>
                    <asp:LinkButton ID="LkEliminar" runat="server" CommandName="Delete" CausesValidation="false" Visible="true">
                        <asp:Image ID="Image1" ImageUrl="Imagenes/iconeliminar.png" ImageAlign="Middle"
                            runat="server" />
                    </asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <HeaderStyle BackColor="#1C5AB6" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#0000A9" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#000065" />
    </asp:GridView>
    </form>
</body>
</html>
