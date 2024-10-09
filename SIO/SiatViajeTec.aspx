<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="SiatViajeTec.aspx.cs" Inherits="SIO.SiatPlanVisita" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/ScriptSIAT.js" type="text/javascript"></script>
    <link href="Styles/StyleGeneral.css" rel="stylesheet" type="text/css" />
    <link href="Styles/StyleSIAT.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style5
        {
            width: 182px;
        }
        .style28
        {
            height: 45px;
            width: 182px;
        }
        .style29
        {
            text-align: right;
            width: 86px;
        }
        .auto-style1 {
            text-align: left;
            width: 340px;
            height: 17px;
        }
        .auto-style2 {
            height: 17px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelGeneral" runat="server" CssClass="Letra" Width="740px">
                <table>
                    <tr valign="top">
                        <td style="width: 240px">
                            <asp:Panel ID="PanelCotizacion" runat="server" CssClass="Letra" GroupingText="Cotización">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkCotizacion" runat="server" Text="Cotización" Visible="true" Enabled="true" Checked="false" OnCheckedChanged="chkCotizacion_CheckedChanged" AutoPostBack="true" />
                                        </td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCotizacion" runat="server" Text="Cotizaciones: " Visible="false"></asp:Label>
                                        </td>
                                        <td class="TexIzq">
                                            <asp:DropDownList ID="cboCotizacion" AutoPostBack="true" runat="server" CssClass="ComboP" Visible="false" OnSelectedIndexChanged="cboCotizacion_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblServicio" runat="server" Text="Servicio: " Visible="false"></asp:Label>
                                        </td>
                                        <td class="TexIzq">
                                            <asp:DropDownList ID="cboServicio" runat="server" CssClass="ComboM" Visible="false" Enabled="false">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td  style="width: 500px">
                            <table style="width: 500px">
                                <tr>
                                    <td class="style5">
                                        <asp:Panel ID="PanelDatosTec" runat="server" GroupingText="Datos del Técnico" Width="500px">
                                            <table>
                                                <tr>
                                                    <td class="auto-style1">Nombre :
                                                        <asp:Label ID="lblNomTec" runat="server"></asp:Label>
                                                    </td>
                                                    <td class="auto-style2">Teléfono :
                                                        <asp:Label ID="lblTelTec" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Correo :
                                                        <asp:Label ID="lblCorrTec" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style28">
                                        <table>
                                            <tr>
                                                <td class="TexIzq">Fecha Inicial Act. :
                                                    <asp:TextBox ID="txtFIniAct" runat="server" CssClass="TexboxP" AutoPostBack="true"
                                                        OnTextChanged="txtFIniAct_TextChanged"></asp:TextBox>
                                                    <asp:CalendarExtender ID="txtFIniActCE" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFIniAct">
                                                    </asp:CalendarExtender>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" FilterType="Custom, Numbers"
                                                        ValidChars="/" TargetControlID="txtFIniAct">
                                                    </asp:FilteredTextBoxExtender>
                                                </td>
                                                <td class="TexIzq">Fecha Fin Act. :
                                                    <asp:TextBox ID="txtFFinAct" runat="server" CssClass="TexboxP" AutoPostBack="true"
                                                        OnTextChanged="txtFFinAct_TextChanged"></asp:TextBox>
                                                    <asp:CalendarExtender ID="txtFFinActCE" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFFinAct">
                                                    </asp:CalendarExtender>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" FilterType="Custom, Numbers"
                                                        ValidChars="/" TargetControlID="txtFFinAct">
                                                    </asp:FilteredTextBoxExtender>
                                                </td>
                                                <td class="TexIzq">Días Viaje :
                                                    <asp:Label ID="lblDViaje" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td>
                            <table style="width: 221px">
                                <tr>
                                    <td class="TexDer">Técnico :
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cboTecnico" AutoPostBack="true" runat="server" CssClass="ComboM"
                                            OnSelectedIndexChanged="cboTecnico_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TexDer">
                                        <asp:Label ID="lblActividad" runat="server" Text="Actividad: " Visible="true"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cboActividad" AutoPostBack="true" runat="server" CssClass="ComboM" Visible="true"
                                            OnSelectedIndexChanged="cboActividad_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblOF" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                            Text="OF" Width="40px" Visible="false"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtOF" runat="server" AutoPostBack="True" BackColor="#FFFF66" Font-Names="Arial"
                                            Font-Size="8pt" OnTextChanged="txtOF_TextChanged" Width="60px" Visible="false"></asp:TextBox>
                                    </td>
                                     <td style="text-align: right">
                                        <asp:Label ID="lblConsecutivo" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                            Text="CP-" Width="40px" Visible="false"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtConsecutivo" runat="server" AutoPostBack="True" BackColor="#FFFF66" Font-Names="Arial"
                                            Font-Size="8pt" OnTextChanged="txtConsecutivo_TextChanged" Width="50px" Visible="false"></asp:TextBox>
                                    </td>
                                    <input id="lblFUP" runat="server" type="hidden" />
                                    <input id="lblVersion" runat="server" type="hidden" />
                                    <td>
                                        <asp:Label runat="server" ID="lblMensajeFup" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    </table>
                <table>
                    <tr>
                        <td>
                            <asp:Panel ID="PanelVisita" runat="server" CssClass="Letra" GroupingText="Planeación Viaje Técnico">
                                <table>
                                    <tr>
                                        <td class="TexDer">Cliente :
                                        </td>
                                        <td class="TexIzq">
                                            <asp:TextBox ID="txtCliente" runat="server" CssClass="TexboxM" OnTextChanged="txtCliente_TextChanged"
                                                AutoPostBack="true"></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="txtCliente_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                                CompletionSetCount="15" EnableCaching="true" Enabled="True" MinimumPrefixLength="1"
                                                ServiceMethod="GetListaClienteSIAT" UseContextKey="true" ServicePath="~/WSSIO.asmx"
                                                TargetControlID="txtCliente" OnClientItemSelected="listaClienteSIAT">
                                            </asp:AutoCompleteExtender>
                                            <input id="lblIdCliente" runat="server" type="hidden" />
                                            <input id="lblIdMoneda" runat="server" type="hidden" />
                                            <input id="lblIdPais" runat="server" type="hidden" />
                                        </td>
                                        <td class="TexDer">Contacto :
                                        </td>
                                        <td class="TexIzq">
                                            <asp:DropDownList ID="cboContacto" runat="server" CssClass="ComboM" AutoPostBack="true"
                                                OnSelectedIndexChanged="cboContacto_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <input id="lblIdConta" runat="server" type="hidden" />
                                            <input id="lblNomConta" runat="server" type="hidden" />
                                        </td>
                                        <td class="TexIzq" colspan="2">
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
                                                        <asp:ListBox ID="listCont" runat="server" Width="160px" Font-Size="8pt" Height="46px"></asp:ListBox>
                                                        <br />
                                                        <span id="lblAggCont" onclick="abrirContantos()" runat="server">+ Contacto</span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TexDer">Días Total.:
                                        </td>
                                        <td class="TexIzq">
                                            <asp:TextBox ID="txtDTol" runat="server" CssClass="TexboxMP" AutoPostBack="true"
                                                OnTextChanged="txtDTol_TextChanged" Enabled="false"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" FilterType="Numbers"
                                                TargetControlID="txtDTol">
                                            </asp:FilteredTextBoxExtender>
                                        </td>
                                        <td class="TexDer">
                                            <asp:Label runat="server" Text="Días Consumidos.:" ID="lblDConsumidos"></asp:Label> 
                                        </td>
                                        <td class="TexIzq">
                                            <asp:TextBox ID="txtDConsumidos" runat="server" CssClass="TexboxMP" AutoPostBack="true" Text="0" Enabled="false"></asp:TextBox>                                          
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TexDer">Fecha Lleg. Tec.:
                                        </td>
                                        <td class="TexIzq">
                                            <asp:TextBox ID="txtFLlegObra" runat="server" CssClass="TexboxP" AutoPostBack="true"
                                                OnTextChanged="txtFLlegObra_TextChanged"></asp:TextBox>
                                            <asp:CalendarExtender ID="txtFLlegObraCE" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFLlegObra">
                                            </asp:CalendarExtender>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" FilterType="Custom, Numbers"
                                                ValidChars="/" TargetControlID="txtFLlegObra">
                                            </asp:FilteredTextBoxExtender>
                                        </td>
                                        <td class="TexDer">Fecha Fin Tec.:
                                        </td>
                                        <td class="TexIzq">
                                            <asp:TextBox ID="txtFFinObra" runat="server" CssClass="TexboxP" AutoPostBack="true"
                                                OnTextChanged="txtFFinObra_TextChanged"></asp:TextBox>
                                            <asp:CalendarExtender ID="txtFFinObraCE" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFFinObra">
                                            </asp:CalendarExtender>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" FilterType="Custom, Numbers"
                                                ValidChars="/" TargetControlID="txtFFinObra">
                                            </asp:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td class="TexDer">Días Reales :
                                        </td>
                                        <td class="TexIzq">
                                            <asp:Label ID="lblDReal" runat="server"></asp:Label>
                                        </td>
                                        <td class="TexDer">
                                            <asp:Label runat="server" Text="Días pend.:" ID="lblDPendDescripcion"></asp:Label> 
                                        </td>
                                        <td class="TexIzq">
                                            <asp:Label ID="lblDPend" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TexDer">Días Inv.:
                                        </td>
                                        <td class="TexIzq">
                                            <asp:TextBox ID="txtDInvCom" runat="server" CssClass="TexboxMP" AutoPostBack="true"
                                                OnTextChanged="txtDInvCom_TextChanged"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" FilterType="Numbers"
                                                TargetControlID="txtDInvCom">
                                            </asp:FilteredTextBoxExtender>
                                        </td>
                                        <td class="TexDer">Días Imp.:
                                        </td>
                                        <td class="TexIzq">
                                            <asp:Label ID="lblDImp" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                    </tr>
                                </table>
                                <table style="height: 74px; width: 725px">
                                    <tr>
                                        <td class="style29">Obra :
                                        </td>
                                        <td class="TexIzq">
                                            <asp:DropDownList ID="cboObra" runat="server" CssClass="ComboM" AutoPostBack="true"
                                                OnSelectedIndexChanged="cboObra_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnAggObra" runat="server" Text=">" CssClass="stylebtnAggN" OnClick="btnAggObra_Click" />
                                            <br />
                                            <asp:Button ID="btnEliObra" runat="server" Text="<" CssClass="stylebtnAggN" OnClick="btnEliObra_Click" />
                                        </td>
                                        <td class="TexIzq">
                                            <asp:ListBox ID="listObra" runat="server" Width="374px" Font-Size="8pt" Height="50px"
                                                 ></asp:ListBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style29">Orden :
                                        </td>
                                        <td class="TexIzq">
                                            <asp:DropDownList ID="cboOF" runat="server" CssClass="ComboP" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnAggOF" runat="server" Text=">" CssClass="stylebtnAggN" OnClick="btnAggOF_Click" />
                                            <br />
                                            <asp:Button ID="btnEliOF" runat="server" Text="<" CssClass="stylebtnAggN" OnClick="btnEliOF_Click" />
                                        </td>
                                        <td class="TexIzq">
                                            <asp:ListBox ID="listOF" runat="server" Width="104px" Height="50px" Font-Size="8pt"></asp:ListBox>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 676px; height: 16px;">
                                    <tr>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td class="TexDer">Hotel :
                                        </td>
                                        <td class="TexIzq">
                                            <asp:TextBox ID="txtHotel" runat="server" CssClass="TexboxM"></asp:TextBox>
                                        </td>
                                        <td class="TexDer">Dirección :
                                        </td>
                                        <td class="TexIzq">
                                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="TexboxM"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style29">Teléfono :
                                        </td>
                                        <td class="TexIzq">
                                            <asp:TextBox ID="txtTelefono" runat="server" CssClass="TexboxN"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="ftNoLetraTel" runat="server" FilterType="Numbers"
                                                TargetControlID="txtTelefono">
                                            </asp:FilteredTextBoxExtender>
                                        </td>
                                        <td class="TexDer">Estado :
                                        </td>
                                        <td class="TexIzq">
                                            <asp:Label ID="lblEstado" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TexDer">Observación :
                                        </td>
                                        <td class="TexIzq">
                                            <asp:TextBox ID="txtObserva" runat="server" TextMode="MultiLine" Height="61px" Width="298px" Visible="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width: 727px; text-align: right; height: 47px;">
                                    <tr>
                                        <td class="TexDer">
                                            <asp:Button ID="btnCostos" runat="server" Text="$Costos" CssClass="Botones" OnClick="btnCostos_Click" />
                                            <asp:Button ID="btnNovedad" runat="server" Text="+Novedades" CssClass="Botones" OnClick="btnNovedad_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TexDer">
                                            <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="Botones" OnClick="btnNuevo_Click" />
                                            <asp:Button ID="btnActVis" runat="server" Text="Actualizar" CssClass="Botones" OnClick="btnActVis_Click" />
                                            <asp:Button ID="btnGuardarVis" runat="server" Text="Guardar" CssClass="Botones" OnClick="btnGuardarVis_Click" />
                                            <asp:Button ID="btnConfi" runat="server" Text="Confirmar" CssClass="Botones" OnClick="btnConfi_Click" />
                                            <asp:Button ID="btnCerrar" runat="server" Text="Cerrar" CssClass="Botones" OnClick="btnCerrar_Click"
                                                OnClientClick="return confirm('Seguro que desea cerrar la visita?');" />
                                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="Botones" OnClick="btnCancelar_Click"
                                                OnClientClick="return confirm('Seguro que desea cancelar la visita?');" />
                                            <asp:Button ID="brnLimpiar" runat="server" Text="Limpiar" CssClass="Botones" OnClick="brnLimpiar_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="PanelOtraAct" runat="server" Height="115px" CssClass="Letra" GroupingText="OTRA ACTIVIDAD ()"
                                Width="398px">
                                <table style="width: 384px">
                                    <tr>
                                        <td class="TexDer">Observacion :
                                        </td>
                                        <td class="TexIzq">
                                            <asp:TextBox ID="txtObsAct" runat="server" TextMode="MultiLine" Height="61px" Width="298px" Visible="true"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TexDer" colspan="2">
                                            <asp:Button ID="btnGuardarAct" runat="server" Text="Guardar" CssClass="Botones" OnClick="btnGuardarAct_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div style="width: 82px; height: 19px;">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="fondoOsc" class="fondoOscuro">
    </div>
    <div id="PopupNov" class="StPopupNov" style="display: none;">
        <div class="close" style="height: 22px;">
            <a href="#" id="close" onclick="cerrarPopup('PopupNov');">
                <img alt="Cerrar" src="iconosMetro/close.png" /></a>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Panel ID="PanelNovedades" runat="server" CssClass="Letra" GroupingText="NOVEDADES">
                    <table style="width: 1100px; height: 75px;">
                        <tr>
                            <td class="TexDer">Orden :
                            </td>
                            <td class="TexIzq">
                                <asp:DropDownList ID="cboOrdenNov" runat="server" class="ComboM" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td class="TexDer">Novedad :
                            </td>
                            <td class="TexIzq">
                                <asp:DropDownList ID="cboNovedad" runat="server" class="ComboM" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td class="TexDer">Observacion :
                            </td>
                            <td class="TexIzq">
                                <asp:TextBox ID="txtObsNov" runat="server" TextMode="MultiLine" Height="61px" Width="254px"
                                    Font-Size="8pt"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnAggNov" runat="server" Text=">" CssClass="stylebtnAggN" OnClick="btnAggNov_Click" /><br />
                                <asp:Button ID="btnEliNov" runat="server" Text="<" CssClass="stylebtnAggN" OnClick="btnEliNov_Click" />
                            </td>
                            <td>
                                <asp:ListBox ID="listOFNovObs" runat="server" Width="295px" Font-Size="8pt"></asp:ListBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8" style="text-align: right">
                                <asp:Button ID="btnGuardarNov" runat="server" Text="Guardar" CssClass="Botones" OnClick="btnGuardarNov_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="PopupCos" class="StPopupCos" style="display: none;">
        <div class="close" style="height: 22px;">
            <a href="#" id="A1" onclick="cerrarPopup('PopupCos');">
                <img alt="Cerrar" src="iconosMetro/close.png" /></a>
        </div>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <style type="text/css">
                    #PanelCostos legend {
                        color: black;
                        font-size: 45px;
                    }
                </style>
                <asp:Panel ID="PanelCostos" runat="server" CssClass="legend">
                    <table style="height: 193px; width: 366px">
                        <tr>
                            <td class="TexDer">
                                <asp:Label ID="lblNomPais" runat="server" Font-Bold="true" Font-Size="10pt"></asp:Label>
                            </td>
                            <td class="TexDer">Costos Planeado
                            </td>
                            <td class="TexDer">Costo Real
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="TexDer">Hotel :
                            </td>
                            <td class="TexDer">$<asp:Label ID="lblPlanHotel" runat="server"></asp:Label>
                            </td>
                            <td class="TexDer">
                                <asp:Label ID="lblRealHotel" runat="server" CssClass="TexboxN"></asp:Label>
                               
                            </td>
                        </tr>
                        <tr>
                            <td class="TexDer">Tiquetes :
                            </td>
                            <td class="TexDer">$<asp:Label ID="lblPlanTiq" runat="server"></asp:Label>
                            </td>
                            <td class="TexDer">
                                <asp:Label ID="lblRealTiq" runat="server" CssClass="TexboxN"></asp:Label>
                               
                            </td>
                        </tr>
                        <tr>
                            <td class="TexDer">Alimentacion :
                            </td>
                            <td class="TexDer">$<asp:Label ID="lblPlanAli" runat="server"></asp:Label>
                            </td>
                            <td class="TexDer">
                                <asp:Label ID="lblRealAli" runat="server" CssClass="TexboxN"></asp:Label>
                              
                            </td>
                        </tr>
                        <tr>
                            <td class="TexDer">Transporte :
                            </td>
                            <td class="TexDer">$<asp:Label ID="lblPlanTran" runat="server"></asp:Label>
                            </td>
                            <td class="TexDer">
                                <asp:Label ID="lblRealTran" runat="server" CssClass="TexboxN"></asp:Label>
                                
                            </td>
                        </tr>
                       
                        <tr>
                            <td class="TexDer">Llamadas :
                            </td>
                            <td class="TexDer">$<asp:Label ID="lblPlanLLam" runat="server"></asp:Label>
                            </td>
                            <td class="TexDer">
                                <asp:Label ID="lblRealLlam" runat="server" CssClass="TexboxN"></asp:Label>
                               
                            </td>
                        </tr>
                        <tr>
                            <td class="TexDer">Lavanderia :
                            </td>
                            <td class="TexDer">$<asp:Label ID="lblPlanLav" runat="server"></asp:Label>
                            </td>
                            <td class="TexDer">
                                <asp:Label ID="lblRealLav" runat="server" CssClass="TexboxN"></asp:Label>
                               
                            </td>
                        </tr>
                        <tr>
                            <td class="TexDer">Penalidades :
                            </td>
                            <td class="TexDer">$<asp:Label ID="lblPlanPenal" runat="server"></asp:Label>
                            </td>
                            <td class="TexDer">
                                <asp:Label ID="lblRealPenal" runat="server" CssClass="TexboxN"></asp:Label>
                              
                            </td>
                        </tr>
                        <tr>
                            <td class="TexDer">Otros :
                            </td>
                            <td class="TexDer">$<asp:Label ID="lblPlanOtros" runat="server"></asp:Label>
                            </td>
                            <td class="TexDer">
                                <asp:Label ID="lblRealOtros" runat="server" CssClass="TexboxN"></asp:Label>
                             
                            </td>
                        </tr>
                        <tr>
                            <td class="TexDer">TRM :
                            </td>
                            <td class="TexDer"></td>
                            <td class="TexDer">
                                <asp:Label ID="lblRealTrm" runat="server" CssClass="TexboxN"></asp:Label>
                              
                            </td>
                        </tr>
                        <tr>
                            <td class="TexDer">Observacion :
                            </td>
                            <td class="TexIzq" colspan="2">
                                <asp:TextBox ID="txtRealObs" runat="server" Width="180px" Height="40px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: right">
                                <asp:Button ID="btnGuardarCostos" runat="server" Text="Guardar" CssClass="Botones"
                                    OnClick="btnGuardarCostos_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div id="PopupBuscaVis" class="StPopupBVis" style="display: none;">
        <div class="close" style="height: 22px;">
            <a href="#" id="A2" onclick="cerrarPopup('PopupBuscaVis');">
                <img alt="Cerrar" src="iconosMetro/close.png" /></a>
        </div>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <asp:Panel ID="PanelBuscarVis" runat="server" CssClass="Letra" GroupingText="OF"
                    Width="860px">
                    <table style="height: 266px; width: 819px">
                       
                       
                        <tr>
                            <td>
                                <div style="overflow: auto; display: inline-block; width: 830px; height: 200px">
                                    <asp:GridView ID="grdViajes" runat="server" BackColor="White" BorderColor="#999999"
                                        BorderStyle="None" BorderWidth="1px" DataKeyNames="idOfaxV, idViaje" CellPadding="3"
                                        GridLines="Vertical" Width="828px" AutoGenerateColumns="False" Font-Size="8pt"
                                        Font-Names="arial" Height="16px">
                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                        <Columns>
                                            <asp:BoundField DataField="idViaje" HeaderText="idViaje" Visible="false" />
                                            <asp:TemplateField HeaderText="+" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnSelOrden" runat="server" Height="24px" ImageUrl="~/iconosMetro/adicionar1.png"
                                                        Width="25px" OnClick="btnSelOrden_Click" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="20pt" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="fechaInicio" HeaderText="Incio Viaje" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="25pt" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fechaFin" HeaderText="Fin Viaje" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="25pt" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="orden" HeaderText="Orden" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="25pt" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="tecnico" HeaderText="Técnico" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="70pt" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cliente" HeaderText="Cliente" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="95pt" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="datosObra" HeaderText="Obra" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Width="165" />
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
