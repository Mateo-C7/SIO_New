<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true"
    CodeBehind="CasinoVerPedidos.aspx.cs" Inherits="SIO.ConsultarPedidos" %>

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
            font-size: small;
            font-weight: bold;
            text-align: right;
        }
        .styleTextoIzq
        {
            font-family: Arial, Helvetica, sans-serif;
            color: #000000;
            font-size: small;
            font-weight: bold;
            text-align: left;
        }
        .styleTextoCen
        {
            font-family: Arial, Helvetica, sans-serif;
            color: #000000;
            font-size: small;
            font-weight: bold;
            text-align: center;
        }
        .style8
        {
            width: 116px;
            height: 10px;
        }
        .style11
        {
            width: 762px;
        }
        .style12
        {
            width: 771px;
        }
        .style15
        {
            height: 3px;
        }
        .style23
        {
            width: 120px;
            height: 2px;
        }
        .style30
        {
            width: 269px;
            height: 18px;
        }
        .style31
        {
            height: 10px;
            width: 331px;
        }
        .style32
        {
            width: 269px;
            height: 10px;
        }
        .style33
        {
            font-family: Arial, Helvetica, sans-serif;
            color: #000000;
            font-size: small;
            font-weight: bold;
            text-align: right;
            width: 331px;
            height: 18px;
        }
        .style34
        {
            font-family: Arial, Helvetica, sans-serif;
            color: #000000;
            font-size: small;
            font-weight: bold;
            text-align: right;
            width: 331px;
            height: 3px;
        }
        .style35
        {
            width: 269px;
            height: 12px;
        }
        .style36
        {
            font-family: Arial, Helvetica, sans-serif;
            color: #000000;
            font-size: small;
            font-weight: bold;
            text-align: right;
            width: 331px;
            height: 2px;
        }
        .style37
        {
            width: 269px;
            height: 2px;
        }
        .style40
        {
            height: 2px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelConsulPedidos" runat="server" Style="margin-bottom: 0px" Width="355px"
                Height="76px">
                <table style="height: 63px; width: 363px;">
                    <tr>
                        <td class="style36">
                            <asp:Label ID="lblNomUsu" runat="server" Text="Usuario:" Font-Size="8pt"></asp:Label>
                        </td>
                        <td class="style37">
                            <asp:TextBox ID="txtNomUsu" runat="server" Width="164px" Font-Size="8pt" Height="12px"></asp:TextBox>
                        </td>
                        <td class="style23">
                            <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" Text="Buscar"
                                Width="57px" BorderColor="#999999" BackColor="#1C5AB6" Font-Bold="True" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="White" />
                        </td>
                    </tr>
                    <tr id="trTipoServicio" runat="server">
                        <td class="style36">
                            <asp:Label ID="lblTipoServicio" runat="server" Text="Tipo de Servicio:" Font-Size="8pt"></asp:Label>
                        </td>
                        <td class="style37">
                            <asp:DropDownList ID="cboServicio" runat="server" AutoPostBack="true" Height="16px"
                                Width="130px" OnSelectedIndexChanged="cboServicio_SelectedIndexChanged" Font-Size="8pt">
                                <asp:ListItem Value="Seleccione">Seleccione</asp:ListItem>
                                <asp:ListItem Value="normal">Invitado</asp:ListItem>
                                <asp:ListItem Value="especial">Especiales</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="style40">
                        </td>
                    </tr>
                    <tr id="trAccion" runat="server">
                        <td class="style36">
                            <asp:Label ID="lblTituloAccion" runat="server" Text="Accion:" Font-Size="8pt"></asp:Label>
                        </td>
                        <td class="style37">
                            <asp:DropDownList ID="cboOpcion" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cboOpcion_SelectedIndexChanged"
                                Height="16px" Width="130px" Font-Size="8pt">
                                <asp:ListItem Value="Selecionar">Selecionar</asp:ListItem>
                                <asp:ListItem Value="Anulado">Anular</asp:ListItem>
                                <%--<asp:ListItem Value="Cerrado">Cerrar</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                        <td class="style40">
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div style="height: 12px">
            </div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Panel ID="PanelInfoPedidos" runat="server" Style="margin-bottom: 0px" GroupingText="Informacion de Pedidos"
                        Width="1130px">
                        <table id="TablaEspecial" runat="server">
                            <tr>
                                <td class="style11">
                                    <asp:GridView ID="grdEspecial" runat="server" BackColor="White" BorderColor="#999999"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="1109px"
                                        AutoGenerateColumns="False" Font-Size="8pt" Font-Names="Arial">
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                        <Columns>
                                            <asp:BoundField DataField="numPedido" HeaderText="# Pedido" ItemStyle-HorizontalAlign="Left"
                                                FooterStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <FooterStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="45px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="usuario" HeaderText="Usuario" ItemStyle-HorizontalAlign="Left"
                                                FooterStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <FooterStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="130px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fechaCrea" HeaderText="Fecha de Creacion" ItemStyle-HorizontalAlign="Left"
                                                FooterStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <FooterStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="40px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="horaCrea" HeaderText="Hora de Creacion" ItemStyle-HorizontalAlign="Left"
                                                FooterStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <FooterStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="40px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fechaAten" HeaderText="Fecha de Atencion" ItemStyle-HorizontalAlign="Left"
                                                FooterStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <FooterStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="40px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="horaAten" HeaderText="Hora de Atencio" ItemStyle-HorizontalAlign="Left"
                                                FooterStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <FooterStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="40px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="lugar" HeaderText="Lugar de Atencio" ItemStyle-HorizontalAlign="Left"
                                                FooterStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <FooterStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="60px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="tipoServicio" HeaderText="Nombre del Servicio" ItemStyle-HorizontalAlign="Left"
                                                FooterStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <FooterStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="140px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cantidad" HeaderText="Cantidad" ItemStyle-HorizontalAlign="Right"
                                                FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle Width="36px" HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Descripcion" ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="descripcion" Text='<%# Eval("descripcion") %>' runat="server" Enabled="false"
                                                        TextMode="MultiLine" Width="146px" Height="29px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="95px" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="valorUni" HeaderText="Valor Unitario" ItemStyle-HorizontalAlign="Right"
                                                FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle Width="40px" HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="valorTotal" HeaderText="Valor Total" ItemStyle-HorizontalAlign="Right"
                                                FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle Width="40px" HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="estado" HeaderText="Estado" ItemStyle-HorizontalAlign="Left"
                                                FooterStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <FooterStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="36px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Cambiar Estado" ItemStyle-HorizontalAlign="Center"
                                                FooterStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="cboEstadoTabla" runat="server" DataSource='<%# llenarComboSel() %>'
                                                        DataTextField="Nombre" DataValueField="Valor">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="40px" HorizontalAlign="Center" />
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
                                    <br />
                                </td>
                            </tr>
                        </table>
                        <table id="TablaNormal" runat="server">
                            <tr>
                                <td class="style12">
                                    <asp:GridView ID="grdNormal" runat="server" BackColor="White" BorderColor="#999999"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="1022px"
                                        AutoGenerateColumns="False" Font-Size="8pt" Font-Names="Arial">
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                        <Columns>
                                            <asp:BoundField DataField="numPedido" HeaderText="# Pedido" ItemStyle-HorizontalAlign="Left"
                                                FooterStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="25px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="usuario" HeaderText="Usuario" ItemStyle-HorizontalAlign="Left"
                                                FooterStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="170px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fechaCrea" HeaderText="Fecha de Creacion" ItemStyle-HorizontalAlign="Left"
                                                FooterStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="45px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="horaCrea" HeaderText="Hora de Creacion" ItemStyle-HorizontalAlign="Left"
                                                FooterStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="45px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fechaAten" HeaderText="Fecha de Atencion" ItemStyle-HorizontalAlign="Left"
                                                FooterStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="45px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="lugar" HeaderText="Lugar de Atencio" ItemStyle-HorizontalAlign="Left"
                                                FooterStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="60px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="tipoServicio" HeaderText="Nombre del Servicio" ItemStyle-HorizontalAlign="Left"
                                                FooterStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="140px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cantidad" HeaderText="Cantidad" ItemStyle-HorizontalAlign="Right"
                                                FooterStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Right">
                                                <ItemStyle Width="40px" HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="estado" HeaderText="Estado" ItemStyle-HorizontalAlign="Left"
                                                FooterStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemStyle Width="38px" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Cambiar Estado" ItemStyle-HorizontalAlign="Center"
                                                FooterStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="cboEstadoTabla" runat="server">
                                                        <asp:ListItem Value="Seleccionar">Seleccionar</asp:ListItem>
                                                        <asp:ListItem Value="Anulado">Anular</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <ItemStyle Width="50px" HorizontalAlign="Center" />
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
                                    <br />
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" Width="77px" BorderColor="#999999"
                                        BackColor="#1C5AB6" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White"
                                        Height="26px" OnClick="btnActualizar_Click" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
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