<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true"
    CodeBehind="VisViajes.aspx.cs" Inherits="SIO.ViajesVis" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function viaSelCom(source, eventArgs) {
            document.getElementById('<%= lblIdCom.ClientID %>').value = eventArgs.get_value();
        }
        function viaSelPais(source, eventArgs) {
            document.getElementById('<%= lblIdPais.ClientID %>').value = eventArgs.get_value();
        }
        function viaSelCiu(source, eventArgs) {
            document.getElementById('<%= lblIdCiu.ClientID %>').value = eventArgs.get_value();
        }
    </script>
    <link href="Styles/StyleGeneral.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .covertura
        {
            -moz-border-radius-topleft: 16px;
            -moz-border-radius-topright: 16px;
            -moz-border-radius-bottomleft: 16px;
            -moz-border-radius-bottomright: 16px;
            -webkit-border-top-left-radius: 16px;
            -webkit-border-top-right-radius: 16px;
            -webkit-border-bottom-left-radius: 16px;
            -webkit-border-bottom-right-radius: 16px;
            border: 1px solid #333;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelGeneral" runat="server" Font-Names="Arial" Height="173px" GroupingText="VIAJES"
                CssClass="Letra" Width="540px">
                <table>
                    <tr>
                        <td rowspan="2">
                            <div class="covertura">
                                <table style="height: 101px; width: 296px">
                                    <tr>
                                        <td class="TexDer">
                                            Nombre del Comercial :
                                        </td>
                                        <td class="TexIzq">
                                            <asp:TextBox ID="txtComercial" runat="server" Height="12px" Width="157px" Font-Size="9pt"
                                                Font-Names="Arial"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="txtComercial_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                                CompletionSetCount="15" EnableCaching="true" Enabled="True" MinimumPrefixLength="1"
                                                ServiceMethod="GetListaComer" UseContextKey="true" ServicePath="~/WSSIO.asmx"
                                                TargetControlID="txtComercial" OnClientItemSelected="viaSelCom">
                                            </asp:AutoCompleteExtender>
                                            <input id="lblIdCom" runat="server" type="hidden" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TexDer">
                                            Pais Destino :
                                        </td>
                                        <td class="TexIzq">
                                            <asp:TextBox ID="txtPais" runat="server" Height="12px" Width="157px" Font-Size="9pt"
                                                Font-Names="Arial" OnTextChanged="txtPais_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="txtPais_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                                CompletionSetCount="15" EnableCaching="true" Enabled="True" MinimumPrefixLength="1"
                                                ServiceMethod="GetListaPais" UseContextKey="true" ServicePath="~/WSSIO.asmx"
                                                TargetControlID="txtPais" OnClientItemSelected="viaSelPais">
                                            </asp:AutoCompleteExtender>
                                            <input id="lblIdPais" runat="server" type="hidden" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TexDer">
                                            Ciudad Destino :
                                        </td>
                                        <td class="TexIzq">
                                            <asp:TextBox ID="txtCiudad" runat="server" Height="12px" Width="157px" Font-Size="9pt"
                                                Font-Names="Arial"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="txtCiudad_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                                CompletionSetCount="15" EnableCaching="true" Enabled="True" MinimumPrefixLength="1"
                                                ServiceMethod="GetListaCiudad" UseContextKey="true" ServicePath="~/WSSIO.asmx"
                                                TargetControlID="txtCiudad" OnClientItemSelected="viaSelCiu">
                                            </asp:AutoCompleteExtender>
                                            <input id="lblIdCiu" runat="server" type="hidden" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td>
                            <div class="covertura">
                                <table style="height: 23px; width: 220px">
                                    <tr>
                                        <td class="TexDer">
                                            Fecha Inicio :
                                        </td>
                                        <td class="TexIzq">
                                            <asp:TextBox ID="txtFechaIni" runat="server" Height="12px" Width="59px" Font-Size="8pt"
                                                Font-Names="Arial"></asp:TextBox>
                                            <asp:CalendarExtender ID="txtFechaIniCE" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaIni">
                                            </asp:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TexDer">
                                            <asp:DropDownList ID="cboHoraIni" runat="server" Height="14px" Width="40px" AutoPostBack="True"
                                                Font-Size="8pt">
                                            </asp:DropDownList>
                                            &nbsp;:
                                        </td>
                                        <td class="TexIzq">
                                            <asp:DropDownList ID="cboMinIni" runat="server" Height="14px" Width="40px" AutoPostBack="True"
                                                Font-Size="8pt">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="covertura">
                                <table style="height: 45px; width: 220px">
                                    <tr>
                                        <td class="TexDer">
                                            Fecha Fin :
                                        </td>
                                        <td class="TexIzq">
                                            <asp:TextBox ID="txtFechaFin" runat="server" Height="12px" Width="59px" Font-Size="8pt"
                                                Font-Names="Arial"></asp:TextBox>
                                            <asp:CalendarExtender ID="txtFechaFinCE" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaFin">
                                            </asp:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TexDer">
                                            <asp:DropDownList ID="cboHoraFin" runat="server" Height="14px" Width="40px" AutoPostBack="True"
                                                Font-Size="8pt">
                                            </asp:DropDownList>
                                            &nbsp;:
                                        </td>
                                        <td class="TexIzq">
                                            <asp:DropDownList ID="cboMinFin" runat="server" Height="14px" Width="40px" AutoPostBack="True"
                                                Font-Size="8pt">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td class="TexDer">
                            <asp:Button ID="btnAdicionar" runat="server" OnClick="btnAdicionar_Click" Text="Adicionar"
                                CssClass="Botones" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div style="width: 55px; height: 5px;">
            </div>
            <div style="width: 796px; height: 5px; border-top: 1px solid black;">
            </div>
            <table>
                <tr>
                    <td>
                        <div style="overflow: auto; width: 798px; height: 378px">
                            <asp:GridView ID="grdViajes" runat="server" BackColor="White" BorderColor="#999999"
                                BorderStyle="None" BorderWidth="1px" DataKeyNames="idViaje" CellPadding="3" GridLines="Vertical"
                                Width="794px" AutoGenerateColumns="False" Font-Size="8pt" Font-Names="arial"
                                Height="16px">
                                <AlternatingRowStyle BackColor="Gainsboro" />
                                <Columns>
                                    <asp:BoundField DataField="idViaje" HeaderText="idViaje" Visible="false" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="2pt" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="nomCom" HeaderText="Nombre Comercial" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="75pt" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="pais" HeaderText="Pais" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="65pt" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ciudad" HeaderText="Ciudad" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="85" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fechaIni" HeaderText="Fecha Inicio" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="40pt" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="horaIni" HeaderText="Hora Inicio" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="40pt" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="fechaFin" HeaderText="Fecha Fin" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="40pt" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="horaFin" HeaderText="Hora Fin" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="40pt" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Eliminar" meta:resourcekey="TemplateFieldResource1"
                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Button ID="BtnEliminar" runat="server" Style="background-image: url('iconosMetro/deleteimage.png');
                                                background-repeat: no-repeat;" Height="20px" Width="20px" OnClientClick="return confirm('Seguro que desea eliminar el viaje?');"
                                                OnClick="BtnEliminar_Click" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="8pt" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" >
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
