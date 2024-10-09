<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="Ciudades.aspx.cs" Inherits="SIO.Ciudades" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style3
        {
            width: 82px;
        }
        .style4
        {
            width: 328px;
        }
        .style5
        {
            width: 82px;
            height: 23px;
        }
        .style6
        {
            width: 328px;
            height: 23px;
        }
        .style7
        {
            height: 23px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="Letra">
                <tr>
                    <td>
                        <asp:Label ID="lblObra1" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt"
                            Text="EVENTO" CssClass="sangria" ForeColor="#1C5AB6" Width="120px"></asp:Label>
                        <table style="height: 147px; width: 699px; margin-right: 39px;">
                            <tr>
                                <td style="text-align: right" class="style3">
                                    <br />
                                    <asp:Label ID="Label5" runat="server" Text="Pais:"></asp:Label>
                                </td>
                                <td style="text-align: left" class="style4">
                                    <asp:ComboBox ID="cboPais" runat="server" AutoPostBack="True" Font-Names="Arial"
                                        Font-Size="8pt" Width="230px" OnSelectedIndexChanged="cboPais_SelectedIndexChanged"
                                        AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList">
                                    </asp:ComboBox>
                                </td>
                                <td style="text-align: left" class="style38">
                                    &nbsp;
                                </td>
                                <td style="text-align: left">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right" class="style3">
                                    <asp:Label ID="Label3" runat="server" Text="Subzona:"></asp:Label>
                                </td>
                                <td style="text-align: left" class="style4">
                                    <asp:ComboBox ID="cboSubzona" runat="server" AutoCompleteMode="SuggestAppend" 
                                        AutoPostBack="True" DropDownStyle="DropDownList" Font-Names="Arial" 
                                        Font-Size="8pt"  
                                        Width="230px" onselectedindexchanged="cboSubzona_SelectedIndexChanged">
                                    </asp:ComboBox>
                                </td>
                                <td style="text-align: left" class="style38">
                                    &nbsp;</td>
                                <td style="text-align: left">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style3" style="text-align: right">
                                    <asp:Label ID="Label4" runat="server" Text="Ciudad:"></asp:Label>
                                </td>
                                <td class="style4" style="text-align: left">
                                    <asp:TextBox ID="txtNombre" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        Width="320px" Height="20px"></asp:TextBox>
                                </td>
                                <td class="style38" style="text-align: left">
                                    <asp:Label ID="lblIdC" runat="server" Text=""></asp:Label>
                                </td>
                                <td style="text-align: left">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style5" style="text-align: right">
                                    :
                                </td>
                                <td class="style6" style="text-align: right">
                                    <asp:Button ID="btnGuardar" runat="server" BackColor="#1C5AB6" 
                                        BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                        ForeColor="White" OnClick="btnGuardarsf_Click" 
                                        OnClientClick="return confirm('Esta seguro de guardar la Ciudad?')" 
                                        TabIndex="25" Text="Guardar" Width="67px" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnNuevo" runat="server" BackColor="#1C5AB6" 
                                        BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                        ForeColor="White" OnClick="btnNuevo_Click" Text="Nuevo" Width="64px" />
                                </td>
                                <td class="style7" style="text-align: left">
                                    </td>
                                <td style="text-align: left" class="style7">
                                    </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        CellPadding="3" DataKeyNames="IdCiudad" OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                        Style="text-align: left" Width="860px" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" Font-Size="Smaller">
                                        <Columns>
                                            <asp:CommandField ShowSelectButton="True" />
                                            <asp:BoundField DataField="IdCiudad" HeaderText="IdCiudad" InsertVisible="False" ReadOnly="True"
                                                SortExpression="IdCiudad" />
                                            <asp:BoundField DataField="Ciudad" HeaderText="Ciudad" SortExpression="Ciudad" />
                                            <asp:BoundField DataField="IdZonaCiu" HeaderText="IdZonaCiu" SortExpression="IdZonaCiu" />
                                            <asp:BoundField DataField="ZonaCiudad" HeaderText="ZonaCiudad" SortExpression="ZonaCiudad" />
                                            <asp:BoundField DataField="IdPais" HeaderText="IdPais" SortExpression="IdPais" />
                                            <asp:BoundField DataField="Pais" HeaderText="Pais" SortExpression="Pais" /> 
                                        </Columns>
                                        <FooterStyle BackColor="#1b5a8f" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#1b5a8f" Font-Bold="True" ForeColor="White" Font-Size = "20px"/>
                                        <PagerStyle BackColor="White" HorizontalAlign="Left" ForeColor="#000066" />
                                        <RowStyle ForeColor="#000066" />
                                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div class="open" />
            <div class="last">
                <asp:Label ID="lblEnviando" runat="server" Text="Cargando..." Font-Names="Arial"
                    Font-Size="14pt"></asp:Label>
                <img src="Imagenes/ajax-loader.gif" alt="Loading" height="30" style="text-align: center"
                    width="30" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

</asp:Content>
