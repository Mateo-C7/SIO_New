<%@ Page Title="Matriz de Integración" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="itemPiciz.aspx.cs" Inherits="SIO.itemPiciz" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

        .style2
        {
            width: 2245px;
        }
        .style10
        {
            width: 816px;
        }
        .style18
        {
            width: 18px;
            }
        .style22
        {
            width: 47px;
            }
        .style15
        {
            width: 37px;
        }
        .style3
        {
            width: 630px;
        }
        .style5
        {
            width: 2245px;
            height: 232px;
        }
        .style6
        {
            width: 630px;
            height: 232px;
        }
        .style19
        {
            width: 2245px;
            height: 627px;
        }
        .style20
        {
            width: 630px;
            height: 627px;
        }
        .style23
        {
            width: 129px;
        }
        </style>

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
        <div class="clear hideSkiplink">
                <asp:Menu ID="NavigationMenu" runat="server" CssClass="menu" EnableViewState="false" IncludeStyleBlock="false" Orientation="Horizontal">
                    <DynamicHoverStyle Font-Size="X-Large" />
                    <Items>
                        <asp:MenuItem NavigateUrl="~/itemPiciz.aspx" Text="Matriz de Integración"/>
                        <asp:MenuItem  Text="                  "/>
                        <asp:MenuItem NavigateUrl="~/GrupoEquivalente.aspx" Text="Grupo Equivalente"/>
                    </Items>
                </asp:Menu>
            </div>
    <table align="left" style="width:100%;">
        <tr>
          
            <td class="style2">
                <ContentTemplate>
                <table class="style10">
                    <tr>
                        <td class="style18">
                            &nbsp;</td>
                        <td align="right" valign="top" class="style23">
                            <asp:Label ID="lblItem" runat="server" Font-Bold="True" Font-Size="Large" 
                                Text="ITEM:">
                                            </asp:Label>
                        </td>
                        <td align="left" class="style22" valign="middle">
                            <asp:TextBox ID="txtCDITEM" runat="server" Height="25px" Width="134px">OF 2385-17</asp:TextBox>
                            &nbsp;
                        </td>
                        <td align="left">
                            <asp:ImageButton ID="ImgbtnFiltrar" runat="server" Height="28px" 
                                ImageAlign="Left" ImageUrl="Imagenes/find.png" OnClick="ImgbtnFiltrar_Click" 
                                ToolTip="Buscar" Width="28px" />
                            &nbsp;&nbsp;
                            <asp:Button ID="btCrea" runat="server" onclick="btCrea_Click1" 
                                Text="Crear" Visible="False" BackColor="#1C5AB6" BorderColor="#999999" 
                                Font-Bold="True" Font-Names="Arial" Font-Size="9pt" ForeColor="White" 
                                Width="63px" />
                            &nbsp;&nbsp;
                            <asp:Label ID="lblItemEquiv" runat="server" Text="Label" Visible="False"></asp:Label>
                            &nbsp;
                            <asp:Label ID="lblPosItem" runat="server" Text="Label" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
                <p>
                    <asp:Label ID="Label1" runat="server" Text="Tipo de Mercancia: "></asp:Label>
                    <asp:DropDownList ID="cmbTipMerc" runat="server" AutoPostBack="True" 
                        Enabled="False" Height="24px" 
                        onselectedindexchanged="cmbTipMerc_SelectedIndexChanged" Width="60px">
                    </asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label2" runat="server" Text="Tipo de Item: "></asp:Label>
                    <asp:DropDownList ID="cmbTipoItem" runat="server" AutoPostBack="True" 
                        Enabled="False" Height="24px" 
                        onselectedindexchanged="cmbTipoItem_SelectedIndexChanged" Width="60px">
                    </asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label3" runat="server" Text="     Unidad Comercial:"></asp:Label>
                    <asp:DropDownList ID="cmbUndCom" runat="server" AutoPostBack="True" 
                        Enabled="False" Height="24px" 
                        onselectedindexchanged="DropDownList1_SelectedIndexChanged" Width="60px">
                    </asp:DropDownList>
&nbsp;&nbsp;&nbsp;
                </p>
                <p>
                    <asp:Label ID="Label4" runat="server" Text="Unidad de Medida:"></asp:Label>
                    &nbsp;
                    <asp:DropDownList ID="cmbUndMed" runat="server" AutoPostBack="True" 
                        Enabled="False" Height="24px" 
                        onselectedindexchanged="cmbUndMed_SelectedIndexChanged" Width="60px">
                    </asp:DropDownList>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label5" runat="server" Text="Codigo Subpartida:"></asp:Label>
                    &nbsp;<asp:DropDownList ID="cmbSubP" runat="server" AutoPostBack="True" 
                        Enabled="False" Height="24px" 
                        onselectedindexchanged="cmbSubP_SelectedIndexChanged" Width="149px">
                    </asp:DropDownList>
                </p>
                <table class="style10">
                    <tr>
                        <td align="left" class="style15">
                            <br />
                            <asp:Label ID="lblCreado" runat="server" Font-Bold="True" Text="...." 
                                Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h1>
                                <asp:Label ID="lblOrden" runat="server" Font-Bold="False" Font-Size="10pt" 
                                    Text="ORDENES DE PRODUCCIÓN Y ESTADO ABUELO" Visible="False" 
                                    Font-Names="Arial"></asp:Label>
                            </h1>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="grdDataItem" runat="server" AllowSorting="True" 
                    AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
                    BorderStyle="None" BorderWidth="1px" CellPadding="1" Font-Names="arial" 
                    Font-Size="8pt" GridLines="Vertical" Height="16px" Width="741px">
                    <AlternatingRowStyle BackColor="Gainsboro" />
                    <AlternatingRowStyle BackColor="Gainsboro" />
                    <Columns>
                        <asp:BoundField DataField="T_DOC_OP" HeaderText="TIPO_DOC" />
                        <asp:BoundField DataField="NO_DOC_OP" HeaderText="ORDEN" />
                        <asp:BoundField DataField="ESTADO" HeaderText="ESTADO" />
                        <asp:BoundField DataField="DES_ITEM" HeaderText="DESCRIPCION" />
                        <asp:BoundField DataField="TIPO" HeaderText="TIPO" />
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <HeaderStyle BackColor="#1C5AB6" Font-Bold="True" ForeColor="White" 
                        HorizontalAlign="Center" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#000065" />
                </asp:GridView>
                </ContentTemplate>
            </td>
            <td class="style3">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="left" class="style5" 
                style="top: inherit; clip: rect(inherit, auto, auto, auto); vertical-align: top; text-align: left;">
                <h1>
                    <asp:Label ID="lblComp" runat="server" Font-Bold="False" Font-Size="10pt" 
                        Text="COMPONENTES Y MATERIALES DEL PROYECTO" Visible="False" 
                        Font-Names="Arial"></asp:Label>
                    &nbsp;</h1>
                <h1>
                    <asp:Button ID="btSaldo" runat="server" onclick="btSaldo_Click" 
                        Text="Buscar_Saldos" Visible="False" />
                    <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Size="Smaller" 
                        Text="....." Visible="False"></asp:Label>
                </h1>
                <asp:Panel ID="Panel1" runat="server" Height="520px" ScrollBars="Vertical" 
                    Visible="False">
                    <asp:GridView ID="grdDataCompon" runat="server" AllowSorting="True" 
                        AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" 
                        BorderStyle="None" BorderWidth="1px" CellPadding="1" Font-Names="arial" 
                        Font-Size="8pt" GridLines="Vertical" Height="122px" 
                        OnPageIndexChanging="grdDataCompon_OnPageIndexChanging" 
                        onselectedindexchanged="grdDataCompon_SelectedIndexChanged" PageSize="15" 
                        Width="741px">
                        <AlternatingRowStyle BackColor="Gainsboro" />
                        <AlternatingRowStyle BackColor="Gainsboro" />
                        <Columns>
                            <asp:BoundField DataField="COD_ITEM_MP" HeaderText="ITEM" />
                            <asp:BoundField DataField="DES_ITEM_MP" HeaderText="DESC_ITEM" />
                            <asp:BoundField DataField="UND" HeaderText="UND_COM" />
                            <asp:BoundField DataField="CANT_MP" HeaderText="CANT">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FACTOR" HeaderText="CONV" />
                            <asp:TemplateField HeaderText="Buscar">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" ToolTip="Buscar" />
                                </ItemTemplate>
                                <headertemplate>
                                    <asp:CheckBox ID="BuscaTodoCheckBox" runat="server" autopostback="true" 
                                        checked="false" oncheckedchanged="SelectAllCheckBox_CheckedChanged" 
                                        text="Buscar" />
                                </headertemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="CANT_P" HeaderText="CANT_P">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="SALDO_P">
                                 <ItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" ToolTip="Saldo" autopostback="true" 
                                        width="50px" OnTextChanged="EditAllTextBox_TextChanged"/>
                                </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cargar">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox2" runat="server" ToolTip="Cargar" checked="true"/>
                                </ItemTemplate>
                                <headertemplate>
                                    <asp:CheckBox ID="CargaTodoCheckBox" runat="server" autopostback="true" 
                                        checked="true" oncheckedchanged="CargaAllCheckBox_CheckedChanged" 
                                        text="Cargar" />
                                </headertemplate>
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Editar">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LkverItem_planta0" runat="server" CausesValidation="false" 
                                        CommandName="Select">
                                <asp:Image ID="Imagerite0" ImageUrl="Imagenes/write.png" ImageAlign="Middle" runat="server" 
                                                ToolTip="Editar" onclick="btEdita_Click" />
                            </asp:LinkButton>
                                    <asp:LinkButton ID="Lkocultar0" runat="server" CausesValidation="false" 
                                        CommandName="Delete" Visible="false">
                                <asp:Image ID="Imagestop0" ImageUrl="Imagenes/stop.png" ImageAlign="Middle" Width="24px" 
                                                Height="24px" runat="server" ToolTip="Anular" />
                            </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle BackColor="#1C5AB6" Font-Bold="True" ForeColor="White" 
                            HorizontalAlign="Center" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#0000A9" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#000065" />
                    </asp:GridView>
                </asp:Panel>
                <p>
                    &nbsp;</p>
                <h2>
                </h2>
                <p>
                    &nbsp;</p>
                <p>
&nbsp;&nbsp;&nbsp; &nbsp;</p>
                <p>
                    &nbsp;</p>
                <p>
                    &nbsp;</p>
                <p>
                    &nbsp;</p>
                <p>
                    &nbsp;</p>
                <p>
                    <br />
                    <br />
                </p>
            </td>
            <td align="left" class="style6" 
                style="top: inherit; clip: rect(inherit, auto, auto, auto); vertical-align: top; text-align: left;">
            </td>
        </tr>
        <tr>
            <td class="style19">
                <h2>
                    &nbsp;</h2>
            </td>
            <td class="style20">
            </td>
        </tr>
    </table>
</asp:Content>
