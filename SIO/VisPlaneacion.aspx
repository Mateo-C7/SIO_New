<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true"
    CodeBehind="VisPlaneacion.aspx.cs" Inherits="SIO.PlaneacionVis" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE10" />
    <link href="CalendarioMetro/fullcalendar.css" rel='stylesheet' />
    <link href="CalendarioMetro/fullcalendar.print.css" rel='stylesheet' media='print' />
    <script type="text/javascript" src="CalendarioMetro/lib/moment.min.js"></script>
    <script type="text/javascript" src="CalendarioMetro/lib/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="CalendarioMetro/lib/jquery-impromptu.js"></script>
    <script type="text/javascript" src="CalendarioMetro/lib/jquery-ui.js"></script>
    <script type="text/javascript" src="CalendarioMetro/fullcalendar.min.js"></script>
    <script src="CalendarioMetro/lib/jquery.fileupload.js" type="text/javascript"></script>
    <script src="CalendarioMetro/lib/jquery.fileupload-ui.js" type="text/javascript"></script>
    <link href="CalendarioMetro/lib/jquery.fileupload-ui.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href="CalendarioMetro/lib/cupertino/jquery-ui.min.css" />
    <link rel="stylesheet" href="CalendarioMetro/stylos/jquery-impromptu.css" />
    <link href="CalendarioMetro/stylos/stylosCalendarioMetro.css" rel="stylesheet" type="text/css" />
    <script src="CalendarioMetro/lib/logicaCalendarioMetro.js" type="text/javascript"></script>
    <link href="CalendarioMetro/stylos/stylosCalendarioMetro2.css" rel="stylesheet" type="text/css" />
    <link href="Styles/StyleGeneral.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function viaSeleccionada(source, eventArgs) {
            document.getElementById('<%= lblIdClienteVia.ClientID %>').value = eventArgs.get_value();
        }
        function abrirPopup(ventana) {
            $('#fondoOsc').fadeIn('slow');
            $('#' + ventana).fadeIn('slow');
        }
        function cerrarPopup(popup) {
            $('#' + popup).fadeOut('slow');
            $('#fondoOsc').fadeOut('slow');
        }

        function viaSelPais(source, eventArgs) {
            document.getElementById('<%= lblIdPais.ClientID %>').value = eventArgs.get_value();
        }
        function viaSelCiu(source, eventArgs) {
            document.getElementById('<%= lblIdCiu.ClientID %>').value = eventArgs.get_value();
        }

        function listaPart(source, eventArgs) {
            document.getElementById('<%= lblIdPart.ClientID %>').value = eventArgs.get_value();
        }

    </script>
    <style type="text/css">
        .auto-style1 {
            text-align: right;
            height: 26px;
        }
        .auto-style2 {
            text-align: left;
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <asp:Panel ID="PanelGeneral" runat="server" CssClass="Letra" Font-Names="Arial" GroupingText="PLANEACION" Width="669px">
                            <table id="tblGeneral" runat="server" style="height: 246px; width: 663px" visible="false">
                                <tr>
                                    <td></td>
                                    <td class="TexIzq">
                                        <asp:CheckBox ID="chkRemoto" runat="server" Checked="false" Text="Reunión Remota?" />
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                        <asp:CheckBox ID="chkForsa" runat="server" AutoPostBack="True" OnCheckedChanged="chkForsa_CheckedChanged" Text="SIO" TextAlign="Left" />
                                        <asp:CheckBox ID="chkLite" runat="server" AutoPostBack="True" Enabled="false" OnCheckedChanged="chkLite_CheckedChanged" Text="SIM" TextAlign="Left" Visible="true" />
                                    </td>
                                    <%-- <td class="TexIzq">
                                    <asp:DropDownList ID="cboGerComerial" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboGerComerial_SelectedIndexChanged"
                                        CssClass="ComboMG">
                                    </asp:DropDownList>
                                </td>--%>
                                </tr>
                                <tr id="trCliFORSA">
                                    <td class="TexDer">Cliente : </td>
                                    <td class="TexIzq">
                                        <asp:TextBox ID="txtCliente" runat="server" AutoPostBack="true" CssClass="TexboxMG" OnTextChanged="txtCliente_TextChanged" Width="266px"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="TextBox1_AutoCompleteExtender" runat="server" CompletionSetCount="15" DelimiterCharacters="" EnableCaching="true" Enabled="True" MinimumPrefixLength="1" OnClientItemSelected="viaSeleccionada" ServiceMethod="GetListaClientes" ServicePath="~/WSSIO.asmx" TargetControlID="txtCliente" UseContextKey="true">
                                        </asp:AutoCompleteExtender>
                                        <input id="lblIdClienteVia" runat="server" type="hidden" />
                                    </td>
                                </tr>
                                <tr id="trCliLITE">
                                    <td class="TexDer">Lite : </td>
                                    <td>
                                        <asp:TextBox ID="txtClientePros" runat="server" AutoPostBack="true" CssClass="TexboxMG" OnTextChanged="txtClientePros_TextChanged" Width="266px"></asp:TextBox>
                                        &nbsp;&nbsp;&nbsp;
                                        <asp:ImageButton ID="ImageButton1" runat="server" Height="18px" ImageUrl="~/iconosMetro/buscar1.png" OnClientClick="abrirPopup('PopupClientesLite');" Width="20px" />
                                    </td>
                                </tr>
                                <tr id="trContacto">
                                    <td class="auto-style1">Contacto : </td>
                                    <td class="auto-style2">
                                        <asp:DropDownList ID="cboContacto" runat="server" AutoPostBack="True" CssClass="ComboMG" Width="270px">
                                        </asp:DropDownList>
                                        <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="true" Height="15px" ImageUrl="Imagenes/iconagregar.png" OnClick="lnkbtnContacto_Click" Width="15px" />
                   
                                       <%-- <asp:LinkButton ID="lnkbtnContacto" runat="server" OnClick="lnkbtnContacto_Click" Text="
                                        "></asp:LinkButton>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TexDer">Motivo : </td>
                                    <td class="TexIzq">
                                        <asp:DropDownList ID="cboMotivo" runat="server" AutoPostBack="True" CssClass="ComboMG" OnSelectedIndexChanged="cboMotivo_SelectedIndexChanged" Width="270px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="trFerias" runat="server">
                                    <td class="TexDer">Ferias : </td>
                                    <td class="TexIzq">
                                        <asp:DropDownList ID="cboFerias" runat="server" AutoPostBack="True" CssClass="ComboMG" OnSelectedIndexChanged="cboFerias_SelectedIndexChanged" Width="270px">
                                        </asp:DropDownList>
                                        &nbsp;&nbsp;&nbsp; Dias:&nbsp;
                                        <asp:TextBox ID="txtDias" runat="server" Height="12px" Width="24px"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="ftNoDias" runat="server" FilterType="Numbers" TargetControlID="txtDias">
                                        </asp:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                                <tr id="trProyec" runat="server">
                                    <td class="TexDer">Proyecto : </td>
                                    <td class="TexIzq">
                                        <asp:DropDownList ID="cboProyec" runat="server" AutoPostBack="True" CssClass="ComboMG" Width="270px">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TexDer">Objetivos : </td>
                                    <td class="TexIzq">
                                        <asp:TextBox ID="txtObjetivo" runat="server" Height="38px" TextMode="MultiLine" Width="382px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TexDer">Fecha de Visita : </td>
                                    <td class="TexIzq">
                                        <asp:TextBox ID="txtFAgen" runat="server" CssClass="TexboxP"></asp:TextBox>
                                        <asp:CalendarExtender ID="txtFAgenCE" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFAgen">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TexDer">Pais Destino : </td>
                                    <td class="TexIzq">
                                        <asp:TextBox ID="txtPais" runat="server" AutoPostBack="true" CssClass="TexboxM" OnTextChanged="txtPais_TextChanged"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="txtPais_AutoCompleteExtender" runat="server" CompletionSetCount="15" DelimiterCharacters="" EnableCaching="true" Enabled="True" MinimumPrefixLength="1" OnClientItemSelected="viaSelPais" ServiceMethod="GetListaPais" ServicePath="~/WSSIO.asmx" TargetControlID="txtPais" UseContextKey="true">
                                        </asp:AutoCompleteExtender>
                                        <input id="lblIdPais" runat="server" type="hidden" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TexDer">Ciudad Destino : </td>
                                    <td class="TexIzq">
                                        <asp:TextBox ID="txtCiudad" runat="server" CssClass="TexboxM" OnTextChanged="txtCiudad_TextChanged"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="txtCiudad_AutoCompleteExtender" runat="server" CompletionSetCount="15" DelimiterCharacters="" EnableCaching="true" Enabled="True" MinimumPrefixLength="1" OnClientItemSelected="viaSelCiu" ServiceMethod="GetListaCiudad" ServicePath="~/WSSIO.asmx" TargetControlID="txtCiudad" UseContextKey="true">
                                        </asp:AutoCompleteExtender>
                                        <input id="lblIdCiu" runat="server" type="hidden" />
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td class="TexIzq">
                                        <asp:CheckBox ID="chkAcomp" runat="server" AutoPostBack="true" Checked="false" OnCheckedChanged="chkAcomp_CheckedChanged" Text="Acompañamiento?" />
                                    </td>
                                </tr>
                                <tr id="trProcesos" runat="server">
                                    <td class="TexDer">Acompañante: </td>
                                    <td class="TexIzq">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtPart" runat="server" AutoPostBack="true" CssClass="TexboxM" OnTextChanged="txtPart_TextChanged"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="txtExtender_Part" runat="server" CompletionSetCount="15" DelimiterCharacters="" EnableCaching="true" Enabled="True" MinimumPrefixLength="1" OnClientItemSelected="listaPart" ServiceMethod="GetListaParticipantes" ServicePath="~/WSSIO.asmx" TargetControlID="txtPart" UseContextKey="true">
                                                    </asp:AutoCompleteExtender>
                                                    <input id="lblIdPart" runat="server" type="hidden" />
                                                    <%-- <asp:DropDownList ID="cboProcesos" runat="server" CssClass="ComboG">
                                        </asp:DropDownList>--%></td>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="btnAggProces" runat="server" CssClass="stylebtnAggN" OnClick="btnAggProces_Click" Text="&gt;" />
                                                                <br />
                                                                <asp:Button ID="btnEliProces" runat="server" CssClass="stylebtnAggN" OnClick="btnEliProces_Click" Text="&lt;" />
                                                                <br />
                                                            </td>
                                                            <td>
                                                                <asp:ListBox ID="listProces" runat="server" Font-Size="8pt" Height="46px" Width="160px"></asp:ListBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="trProcesosTec" runat="server">
                                    <td></td>
                                    <td class="TexIzq">
                                        <asp:CheckBox ID="chkSoporteTecnico" runat="server" Checked="false" Text="Soporte Técnico?" />
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td class="TexDer">
                                        <asp:Button ID="btnAdicionar" runat="server" CssClass="Botones" OnClick="btnAdicionar_Click" Text="Adicionar" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td align="right" valign="top">
                        <asp:ImageButton ID="imgBotonNuevoPlan" runat="server" CausesValidation="true" Height="24px" ImageUrl="Imagenes/iconagregar.png" OnClick="imgBotonNuevoPlan_Click" Width="24px" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="height: 18px; width: 667px;">
    </div>
    <asp:UpdatePanel ID="UpdatePanel4" runat="server" Width="802px" CssClass="Letra">
        <ContentTemplate>
            <div class="Letra" style="display: inline-block;">
            <%--style="height: 28px; width: 252px"--%>
            <table>
                  <tr>
                  <td>
                  <table>
                    <tr>
                        <td class="TexDer">Año :
                            <asp:DropDownList ID="cboAno" runat="server" AutoPostBack="true" CssClass="ComboP">
                            </asp:DropDownList>
                        </td>
                        <td class="TexDer">Mes :
                            <asp:DropDownList ID="cboMes" runat="server" AutoPostBack="true" CssClass="ComboP">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                                  </td>
                                  <td style="width:400px">
                                  
                                        <asp:Label runat="server" ID="lblVisitaCreada" Font-Bold="true" Text="Visita Creada: "></asp:Label>
                                  </td>
                                    <td align="right">
                                        <asp:Label runat="server" ID="lblGuiaColor" Font-Bold="true">Gu&iacute;a de color Visitas</asp:Label>
                                        <table id="guiaColor" runat="server">
                                            <tr>                                                
                                                <td align="center" bgcolor="#3a87ad" style="width: auto; height: auto; color:#fff;font-weight: bold">Planeada</td>                                                
                                                <td align="center" bgcolor="#CAC337" style="width: auto; height: auto; color:#fff;font-weight: bold">Ejecutando</td>
                                                <td align="center" bgcolor="#37CA5C" style="width: auto; height: auto; color:#fff;font-weight: bold">Cerrada</td>
                                                <td align="center" bgcolor="#E13B3B" style="width: auto; height: auto; color:#fff;font-weight: bold">Cancelada</td>
                                            </tr>
                                        </table>
                                    </td>
                                                   
                  
                  </tr>
            </table>                
                <table id="tblDetViaj" runat="server" class="styleDivDetViaj">
                </table>
                <div id="contenidoAjax" style="height: 11px;">
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div style="height: 18px; width: 667px;">
    </div>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelCalendario" runat="server" Font-Names="Arial">
                <div id='wrap'>
                    <div id='calendar'>
                    </div>
                    <div style='clear: both'>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="fondoOsc" class="fondoOscuro">
    </div>
    <div id="PopupClientesLite" class="StPopupClientesLite" style="display: none;">
        <div class="close">
            <a href="#" id="close" onclick="cerrarPopup('PopupClientesLite');">
                <img alt="Cerrar" src="iconosMetro/close.png" /></a>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server" Font-Names="Arial" Height="252px" Width="635px"
                    CssClass="Letra">
                    <table style="height: 251px; width: 629px">
                        <tr>
                            <td>
                                <table style="width: 622px; height: 37px;">
                                    <tr>
                                        <td class="TexIzq">Cliente :
                                        <asp:TextBox ID="txtFilCliente" runat="server" CssClass="TexboxN"></asp:TextBox>
                                        </td>
                                        <td class="TexIzq">Origen :
                                        <asp:DropDownList ID="cboOrigen" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboOrigen_SelectedIndexChanged"
                                            CssClass="ComboM">
                                        </asp:DropDownList>
                                        </td>
                                        <td class="TexIzq">Fuente :
                                        <asp:DropDownList ID="cboFuente" runat="server" AutoPostBack="True" CssClass="ComboM">
                                        </asp:DropDownList>
                                        </td>
                                        <td class="style46">
                                            <asp:Button ID="Button1" runat="server" Text="Filtrar" OnClick="Filtrar_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td colspan="4">
                                            <asp:GridView ID="grdClientes" runat="server" DataKeyNames="idClienteL, idPaisClieL"
                                                CellPadding="4" GridLines="None" Width="621px" Font-Size="8pt" AutoGenerateColumns="False"
                                                ForeColor="#333333" Height="16px">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="idClienteL" HeaderText="idCliente" Visible="false" />
                                                    <asp:BoundField DataField="idPaisClieL" HeaderText="idPaisClieL" Visible="false" />
                                                    <asp:BoundField HeaderText="Cliente" DataField="nomClieL" HeaderStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="130px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Pais" DataField="paisClieL" HeaderStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Ciudad" DataField="ciudadClieL" HeaderStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="origenClieL" HeaderText="Origen" HeaderStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fuenteClieL" HeaderText="Fuente" HeaderStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="proyClieL" HeaderText="Proyecto" HeaderStyle-HorizontalAlign="Left">
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Agregar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnSelCliLite" runat="server" Height="24px" ImageUrl="~/iconosMetro/adicionar1.png"
                                                                Width="25px" OnClick="btnSelCliLite_Click" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EFF3FB" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                                <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
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
