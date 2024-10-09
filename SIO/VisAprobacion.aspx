<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true"
    CodeBehind="VisAprobacion.aspx.cs" Inherits="SIO.AprobacionVis" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
        .styleTextoDer
        {
            font-family: Arial, Helvetica, sans-serif;
            color: #000000;
            font-size: 8pt;
            text-align: right;
        }
        .styleTextoIzq
        {
            font-family: Arial, Helvetica, sans-serif;
            color: #000000;
            font-size: 8pt;
            text-align: right;
        }
        .styleTextoCen
        {
            font-family: Arial, Helvetica, sans-serif;
            color: #000000;
            font-size: 8pt;
            text-align: center;
        }
        .styleTextoDerN
        {
            font-family: Arial, Helvetica, sans-serif;
            color: #000000;
            font-size: 8pt;
            font-weight: bold;
            text-align: right;
        }
        .styleTextoIzqN
        {
            font-family: Arial, Helvetica, sans-serif;
            color: #000000;
            font-size: 8pt;
            font-weight: bold;
            text-align: left;
        }
        .styleTextoCenN
        {
            font-family: Arial, Helvetica, sans-serif;
            color: #000000;
            font-size: 8pt;
            font-weight: bold;
            text-align: center;
        }
        .style6
        {
            width: 83px;
        }
        .style12
        {
            width: 447px;
            text-align: right;
            height: 37px;
        }
        .style14
        {
            height: 28px;
        }
        .style15
        {
            height: 37px;
            width: 290px;
        }
        .style16
        {
            height: 37px;
            width: 193px;
            text-align: right;
        }
        .style17
        {
            height: 37px;
            width: 11px;
        }
        .style23
        {
            width: 355px;
        }
        .style24
        {
            width: 89px;
            text-align: right;
        }
        .style25
        {
            font-family: Arial, Helvetica, sans-serif;
            color: #000000;
            font-size: 8pt;
            text-align: left;
            width: 246px;
        }
        .style26
        {
            width: 246px;
        }
        .style40
        {
            font-family: Arial, Helvetica, sans-serif;
            color: Black;
            height: 27px;
            font-weight: bold;
            font-size: 18px;
            width: 283px;
        }
        .style42
        {
            font-family: Arial, Helvetica, sans-serif;
            color: #000000;
            font-size: 8pt;
            text-align: right;
            width: 48px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelGeneral" runat="server" Font-Names="Arial" Height="597px">
                <div class="style40">
                    APROBACION</div>
                <table style="height: 71px; width: 1120px; margin-bottom: 3px;">
                    <tr>
                        <td class="style42">
                            Periodos :
                        </td>
                        <td class="style25">
                            <asp:DropDownList ID="cboAgenda" runat="server" AutoPostBack="True" Height="16px"
                                Width="234px" Font-Size="9pt" OnSelectedIndexChanged="cboAgenda_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style6">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="style42">
                            &nbsp;Zona :
                        </td>
                        <td class="style23">
                            <asp:DropDownList ID="cboZonas" runat="server" Width="234px" Font-Size="9pt" Height="16px"
                                AutoPostBack="true" OnSelectedIndexChanged="cboZonas_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="style42">
                            Tipo :
                        </td>
                        <td>
                            <asp:DropDownList ID="cboTipoAgente" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboTipoAgente_SelectedIndexChanged"
                                Height="16px" Width="116px" Font-Size="9pt">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="style42">
                            Nombre :
                        </td>
                        <td class="style23">
                            <asp:DropDownList ID="cboPersonaGenRep" runat="server" Height="16px" Width="183px"
                                Font-Size="9pt" AutoPostBack="true" OnSelectedIndexChanged="cboPersonaGenRep_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td class="style6">
                        </td>
                        <td class="style26">
                        </td>
                        <td class="style24">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <div style="width: 1125px; height: 9px; border-top: 1px solid black;">
                </div>
                <table style="height: 2px; width: 1128px">
                    <tr>
                        <td class="style15">
                        </td>
                        <td class="style12">
                        </td>
                        <td class="style16">
                            <asp:Button ID="btnActualizar" runat="server" BackColor="#1C5AB6" BorderColor="#999999"
                                Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White" Height="25px"
                                Style="text-align: center; margin-right: 0px;" Text="Actualizar" Width="69px"
                                OnClick="btnActualizar_Click" />
                        </td>
                        <td class="style17">
                        </td>
                        <td class="style14">
                            <asp:DropDownList ID="cboTodosAprob" runat="server" AutoPostBack="true" Height="16px"
                                Width="183px" Font-Size="9pt" Style="margin-left: 0px" OnSelectedIndexChanged="cboTodosAprob_SelectedIndexChanged">
                                <asp:ListItem Value="Seleccionar">Seleccionar</asp:ListItem>
                                <asp:ListItem Value="AprobT">Aprobar Todos</asp:ListItem>
                                <asp:ListItem Value="NoAprobT">No Aprobar Todos</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <div style="overflow: auto; width: 1148px; height: 400px">
                                <asp:GridView ID="grdTablaVisitas" runat="server" BackColor="White" BorderColor="#999999"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="1122px"
                                    Font-Size="8pt" AutoGenerateColumns="False">
                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                    <Columns>
                                        <asp:BoundField DataField="numVisita" HeaderText="N° Visita">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="20px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Cliente" DataField="cliente" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="130px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Pais" DataField="pais" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="65px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Ciudad" DataField="ciudad" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="65px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Telefono" DataField="telefono" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="60px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Email" DataField="email" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="20px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Ejecuta Visita" DataField="ejeVisita" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="90px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="BD Origen" DataField="bdOrigen" HeaderStyle-HorizontalAlign="Left">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="30px" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Aprobacion Actual" DataField="aprobacion" HeaderStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Cambiar Aprobacion" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="cboAprobacion" runat="server" DataSource='<%# llenarComboSel() %>'
                                                    DataTextField="Nombre" DataValueField="Valor">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="nom" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCorreoComer" runat="server" Text='<%# Bind("correoComer") %>' Font-Size="11pt">
                                                </asp:Label>
                                            </ItemTemplate>
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
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
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
</asp:Content>