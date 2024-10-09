<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="SiatViajeIng.aspx.cs" Inherits="SIO.SiatViajeIng" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/ScriptSIAT.js" type="text/javascript"></script>
    <link href="Styles/StyleGeneral.css" rel="stylesheet" type="text/css" />
    <link href="Styles/StyleSIAT.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #brEsp
        {
            height: 2px;
            width: 124px;
        }
        .style13
        {
            text-align: right;
            width: 52px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelGeneral" runat="server" Height="74px" CssClass="Letra" Width="349px">
                <table style="height: 64px; width: 346px">
                    <tr>
                        <td class="TexDer">
                            Fecha Inicio Viaje :
                        </td>
                        <td class="TexIzq">
                            <asp:TextBox ID="txtFIniVia" runat="server" CssClass="TexboxP"></asp:TextBox>
                            <asp:CalendarExtender ID="txtFInicioViaCE" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFIniVia">
                            </asp:CalendarExtender>
                        </td>
                        <td class="TexDer">
                            Fecha Fin Viaje :
                        </td>
                        <td class="TexIzq">
                            <asp:TextBox ID="txtFFinVia" runat="server" CssClass="TexboxP"></asp:TextBox>
                            <asp:CalendarExtender ID="txtFFinViaCE" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFFinVia">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TexIzq" colspan="2">
                            <asp:RadioButton ID="rbForsa" Text="Forsa" runat="server" GroupName="rbTipo" AutoPostBack="true"
                                OnCheckedChanged="rbForsa_CheckedChanged" />
                            <asp:RadioButton ID="rbObra" Text="Otras Obras" runat="server" AutoPostBack="true"
                                GroupName="rbTipo" OnCheckedChanged="rbObra_CheckedChanged" />
                        </td>
                        <td class="TexCen" colspan="2">
                            <asp:Button ID="btnBuscarViajes" runat="server" Text="Buscar Viaje" CssClass="Botones"
                                OnClick="btnBuscarViajes_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="PanelActividad" runat="server" Height="325px" CssClass="Letra" Width="818px"
                GroupingText="Actividad">
                <table style="height: 61px; width: 744px">
                    <tr>
                        <td class="style13">
                            Pais :
                        </td>
                        <td class="TexIzq">
                            <asp:TextBox ID="txtPais" runat="server" CssClass="TexboxM" OnTextChanged="txtPais_TextChanged"
                                AutoPostBack="true"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txtPais_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                CompletionSetCount="15" EnableCaching="true" Enabled="True" MinimumPrefixLength="1"
                                ServiceMethod="GetListaPais" UseContextKey="true" ServicePath="~/WSSIO.asmx"
                                TargetControlID="txtPais" OnClientItemSelected="listaPais">
                            </asp:AutoCompleteExtender>
                            <input id="lblIdPais" runat="server" type="hidden" />
                        </td>
                        <td class="style13">
                            Ciudad :
                        </td>
                        <td>
                            <asp:TextBox ID="txtCiudad" runat="server" CssClass="TexboxM" AutoPostBack="true"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txtCiudad_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                CompletionSetCount="15" EnableCaching="true" Enabled="True" MinimumPrefixLength="1"
                                ServiceMethod="GetListaCiudad" UseContextKey="true" ServicePath="~/WSSIO.asmx"
                                TargetControlID="txtCiudad" OnClientItemSelected="listaCiu">
                            </asp:AutoCompleteExtender>
                            <input id="lblIdCiu" runat="server" type="hidden" />
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnAgreCiu" runat="server" Text=">" CssClass="stylebtnAggN" OnClick="btnAgreCiu_Click" />
                                        <br />
                                        <asp:Button ID="btnEliCiu" runat="server" Text="<" CssClass="stylebtnAggN" OnClick="btnEliCiu_Click" />
                                        <br />
                                    </td>
                                    <td>
                                        <asp:ListBox ID="listCiudad" runat="server" Width="242px" Font-Size="8pt" Height="46px"
                                            Style="overflow-x: auto;"></asp:ListBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table style="width: 445px">
                    <tr>
                        <td class="style13">
                            Cliente :
                        </td>
                        <td class="TexIzq" colspan="2">
                            <asp:TextBox ID="txtCliente" runat="server" CssClass="TexboxG" AutoPostBack="true"
                                OnTextChanged="txtCliente_TextChanged"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txtCliente_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                CompletionSetCount="15" EnableCaching="true" Enabled="True" MinimumPrefixLength="1"
                                ServiceMethod="GetListaClienteSIAT" UseContextKey="true" ServicePath="~/WSSIO.asmx"
                                TargetControlID="txtCliente" OnClientItemSelected="listaClienteSIAT">
                            </asp:AutoCompleteExtender>
                            <input id="lblIdCliente" runat="server" type="hidden" />
                            <span id="lblAggCli" onclick="abrirCliente()" runat="server">+ Cliente</span>
                    </tr>
                    <tr>
                        <td class="style13">
                            Contacto :
                        </td>
                        <td>
                            <asp:DropDownList ID="cboContacto" runat="server" CssClass="ComboM" AutoPostBack="true"
                                OnSelectedIndexChanged="cboContacto_SelectedIndexChanged">
                            </asp:DropDownList>
                            <input id="lblIdConta" runat="server" type="hidden" />
                            <input id="lblNomConta" runat="server" type="hidden" />
                        </td>
                        <td colspan="2">
                            <table style="height: 65px;">
                                <tr>
                                    <td>
                                        <asp:Button ID="btnAggCont" runat="server" Text=">" CssClass="stylebtnAggN" OnClick="btnAggCont_Click" />
                                        <br />
                                        <asp:Button ID="btnEliCont" runat="server" Text="<" CssClass="stylebtnAggN" OnClick="btnEliCont_Click" />
                                        <br />
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:ListBox ID="listCont" runat="server" Width="160px" Font-Size="8pt" Height="46px">
                                        </asp:ListBox>
                                        <br />
                                        <span id="lblAggCont" onclick="abrirContantos()" runat="server">+ Contacto</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table style="width: 798px">
                    <tr>
                        <td class="style13">
                            Obra :
                        </td>
                        <td>
                            <asp:DropDownList ID="cboObra" runat="server" CssClass="ComboM">
                            </asp:DropDownList>
                            <input id="lblIdObra" runat="server" type="hidden" />
                            <input id="lblNomObra" runat="server" type="hidden" />
                        </td>
                        <td class="TexDer">
                            Usos equipos :
                        </td>
                        <td>
                            <asp:TextBox ID="txtEqui" runat="server" CssClass="TexboxN"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="ftNoLetraEqu" runat="server" FilterType="Numbers"
                                TargetControlID="txtEqui">
                            </asp:FilteredTextBoxExtender>
                        </td>
                        <td>
                            <asp:Button ID="btnAggObra" runat="server" Text=">" CssClass="stylebtnAggN" OnClick="btnAggObra_Click" />
                            <br />
                            <asp:Button ID="btnEliObra" runat="server" Text="<" CssClass="stylebtnAggN" OnClick="btnEliObra_Click" />
                        </td>
                        <td>
                            <asp:ListBox ID="listObra" runat="server" Width="356px" Height="59px" Font-Size="8pt"
                                Style="overflow-x: auto;"></asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style13">
                            Eventos :
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtEventos" TextMode="MultiLine" runat="server" Height="44px" Width="205px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" class="TexDer">
                            <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="Botones" OnClick="btnNuevo_Click" />
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="Botones" OnClick="btnGuardar_Click" />
                            <asp:Button ID="btnAct" runat="server" Text="Actualizar" CssClass="Botones" OnClick="btnAct_Click" />
                            <asp:Button ID="btnCerr" runat="server" Text="Cerrar" CssClass="Botones" OnClick="btnCerr_Click"
                                OnClientClick="return confirm('Seguro que desea cerrar la visita?');" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Eliminar" CssClass="Botones" OnClientClick="return confirm('Seguro que desea eliminar la visita?');"
                                OnClick="btnCancelar_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="fondoOsc" class="fondoOscuro">
    </div>
    <div id="PopupBuscaVis" class="StPopupBVis" style="display: none;">
        <div class="close" style="height: 22px;">
            <a href="#" id="A2" onclick="cerrarPopup('PopupBuscaVis');">
                <img alt="Cerrar" src="iconosMetro/close.png" /></a></div>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <asp:Panel ID="PanelBuscarVis" runat="server" CssClass="Letra" GroupingText="Pais">
                    <table style="height: 266px; width: 210px">
                        <tr>
                            <td>
                                Pais :
                                <asp:TextBox ID="txtFiltroObra" runat="server" CssClass="TexboxN"></asp:TextBox>&nbsp;&nbsp;

                                
                                                    Fecha Inicial Act. :
                                                    <asp:TextBox ID="txtFIniAct" runat="server" CssClass="TexboxP" AutoPostBack="true"
                                                        ></asp:TextBox>
                                                    <asp:CalendarExtender ID="txtFIniActCE" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFIniAct">
                                                    </asp:CalendarExtender>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" FilterType="Custom, Numbers"
                                                        ValidChars="/" TargetControlID="txtFIniAct">
                                                    </asp:FilteredTextBoxExtender>
                                                
                                                
                                                    Fecha Fin Act. :
                                                    <asp:TextBox ID="txtFFinAct" runat="server" CssClass="TexboxP" AutoPostBack="true"                                                        ></asp:TextBox>
                                                    <asp:CalendarExtender ID="txtFFinActCE" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFFinAct">
                                                    </asp:CalendarExtender>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" FilterType="Custom, Numbers"
                                                        ValidChars="/" TargetControlID="txtFFinAct">
                                                    </asp:FilteredTextBoxExtender>
                                                
                            
                                <asp:Button ID="btnBuscarPais" runat="server" Text="Buscar" CssClass="Botones" OnClick="btnBuscarPais_Click" />
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <div style="overflow: auto; width: 585px; height: 200px">
                                    <asp:GridView ID="grdViajes" runat="server" BackColor="White" BorderColor="#999999"
                                        BorderStyle="None" BorderWidth="1px" DataKeyNames="idCiuViaje, idViaje" CellPadding="3"
                                        GridLines="Vertical" Width="563px" AutoGenerateColumns="False" Font-Size="8pt"
                                        Font-Names="arial" Height="16px">
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                        <Columns>
                                            <asp:BoundField DataField="idCiuViaje" HeaderText="idCiuViaje" Visible="false" />
                                            <asp:BoundField DataField="idViaje" HeaderText="idViaje" Visible="false" />
                                            <asp:TemplateField HeaderText="+" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnSelPais" runat="server" Height="24px" ImageUrl="~/iconosMetro/adicionar1.png"
                                                        Width="25px" OnClick="btnSelPais_Click" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="20pt" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="pais" HeaderText="Pais" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="60pt" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ciudad" HeaderText="Ciudad" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="60pt" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fechaIni" HeaderText="Incio Viaje" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="25pt" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fechaFin" HeaderText="Fin Viaje" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="25pt" />
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
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
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
