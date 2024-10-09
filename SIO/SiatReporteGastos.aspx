<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true"
    CodeBehind="SiatReporteGastos.aspx.cs" Inherits="SIO.SiatReporteGastos" UICulture="es"
    Culture="es-CO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/ScriptSIAT.js" type="text/javascript"></script>
    <link href="Styles/StyleGeneral.css" rel="stylesheet" type="text/css" />
    <link href="Styles/StyleSIAT.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style3
        {
            text-align: center;
            height: 46px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <style type="text/css">
                #PanelCostos legend {
                    color: black;
                    font-size: 45px;
                }
            </style>
            <asp:Panel ID="PanelCostos" runat="server" CssClass="legend" Width="384px">
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblOF" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                            Text="OF" Width="40px" Visible="true"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtOF" runat="server" AutoPostBack="True" BackColor="#FFFF66" Font-Names="Arial"
                                            Font-Size="8pt" OnTextChanged="txtOF_TextChanged" Width="60px" Visible="true"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblConsecutivo" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                            Text="CP-" Width="40px" Visible="true"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:TextBox ID="txtConsecutivo" runat="server" AutoPostBack="True" BackColor="#FFFF66" Font-Names="Arial"
                                            Font-Size="8pt" OnTextChanged="txtConsecutivo_TextChanged" Width="50px" Visible="true"></asp:TextBox>
                                    </td>
                                    <input id="lblIdPais" runat="server" type="hidden" />
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <table style="height: 193px; width: 366px">
                        <tr>
                            <td class="TexDer">
                                <asp:Label ID="lblNomPais" runat="server" Font-Bold="true" Font-Size="10pt"></asp:Label>
                            </td>
                            <td class="TexDer">Costos Planeado </td>
                            <td class="TexDer">Costo Real </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td class="TexDer">Hotel : </td>
                            <td class="TexDer">$<asp:Label ID="lblPlanHotel" runat="server"></asp:Label>
                            </td>
                            <td class="TexDer">
                                <asp:Label ID="lblRealHotel" runat="server" CssClass="TexboxN"></asp:Label>
                                <%--<asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers"
                                    ValidChars="." TargetControlID="txtRealHotel">
                                </asp:FilteredTextBoxExtender>--%></td>
                        </tr>
                        <tr>
                            <td class="TexDer">Tiquetes : </td>
                            <td class="TexDer">$<asp:Label ID="lblPlanTiq" runat="server"></asp:Label>
                            </td>
                            <td class="TexDer">
                                <asp:Label ID="lblRealTiq" runat="server" CssClass="TexboxN"></asp:Label>
                                <%--<asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers"
                                    ValidChars="." TargetControlID="txtRealTiq">
                                </asp:FilteredTextBoxExtender>--%></td>
                        </tr>
                        <tr>
                            <td class="TexDer">Alimentacion : </td>
                            <td class="TexDer">$<asp:Label ID="lblPlanAli" runat="server"></asp:Label>
                            </td>
                            <td class="TexDer">
                                <asp:Label ID="lblRealAli" runat="server" CssClass="TexboxN"></asp:Label>
                                <%-- <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom, Numbers"
                                    ValidChars="." TargetControlID="txtRealAli">
                                </asp:FilteredTextBoxExtender>--%></td>
                        </tr>
                        <tr>
                            <td class="TexDer">Transporte : </td>
                            <td class="TexDer">$<asp:Label ID="lblPlanTran" runat="server"></asp:Label>
                            </td>
                            <td class="TexDer">
                                <asp:Label ID="lblRealTran" runat="server" CssClass="TexboxN"></asp:Label>
                                <%--<asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom, Numbers"
                                    ValidChars="." TargetControlID="txtRealTranInt">
                                </asp:FilteredTextBoxExtender>--%></td>
                        </tr>
                        <%-- <tr>
                            <td class="TexDer">Transporte Aeropuerto :
                            </td>
                            <td>$<asp:Label ID="lblPlanTranAer" runat="server"></asp:Label>
                            </td>
                            <td class="TexDer">
                                <asp:Label ID="lblRealTranAer" runat="server" CssClass="TexboxN"></asp:Label>
                                <%--<asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom, Numbers"
                                    ValidChars="." TargetControlID="txtRealTranAer">
                                </asp:FilteredTextBoxExtender>
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="TexDer">Llamadas : </td>
                            <td class="TexDer">$<asp:Label ID="lblPlanLLam" runat="server"></asp:Label>
                            </td>
                            <td class="TexDer">
                                <asp:Label ID="lblRealLlam" runat="server" CssClass="TexboxN"></asp:Label>
                                <%--<asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Custom, Numbers"
                                    ValidChars="." TargetControlID="txtRealLlam">
                                </asp:FilteredTextBoxExtender>--%></td>
                        </tr>
                        <tr>
                            <td class="TexDer">Lavanderia : </td>
                            <td class="TexDer">$<asp:Label ID="lblPlanLav" runat="server"></asp:Label>
                            </td>
                            <td class="TexDer">
                                <asp:Label ID="lblRealLav" runat="server" CssClass="TexboxN"></asp:Label>
                                <%--<asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Custom, Numbers"
                                    ValidChars="." TargetControlID="txtRealLav">
                                </asp:FilteredTextBoxExtender>--%></td>
                        </tr>
                        <tr>
                            <td class="TexDer">*Penalidades : </td>
                            <td class="TexDer">$<asp:Label ID="lblPlanPenal" runat="server"></asp:Label>
                            </td>
                            <td class="TexDer">
                                <asp:Label ID="lblRealPenal" runat="server" CssClass="TexboxN"></asp:Label>
                                <%--<asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Custom, Numbers"
                                    ValidChars="." TargetControlID="txtRealPenal">
                                </asp:FilteredTextBoxExtender>--%></td>
                        </tr>
                        <tr>
                            <td class="TexDer">*Otros : </td>
                            <td class="TexDer">$<asp:Label ID="lblPlanOtros" runat="server"></asp:Label>
                            </td>
                            <td class="TexDer">
                                <asp:Label ID="lblRealOtros" runat="server" CssClass="TexboxN"></asp:Label>
                                <%-- <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Custom, Numbers"
                                    ValidChars="." TargetControlID="txtRealOtros">
                                </asp:FilteredTextBoxExtender>--%></td>
                        </tr>
                         <tr>
                            <td class="TexDer">*Transporte Aereo : </td>
                            <td class="TexDer">$<asp:Label ID="lblPlanAereo" runat="server"></asp:Label>
                            </td>
                            <td class="TexDer">
                                <asp:Label ID="lblRealAereo" runat="server" CssClass="TexboxN"></asp:Label>
                                <%-- <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Custom, Numbers"
                                    ValidChars="." TargetControlID="txtRealOtros">
                                </asp:FilteredTextBoxExtender>--%></td>
                        </tr>
                        <tr>
                            <td class="TexDer">TRM : </td>
                            <td class="TexDer"></td>
                            <td class="TexDer">
                                <asp:Label ID="lblRealTrm" runat="server" CssClass="TexboxN"></asp:Label>
                                <%-- <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" FilterType="Custom, Numbers"
                                    ValidChars="." TargetControlID="txtRealTrm">
                                </asp:FilteredTextBoxExtender>--%></td>
                        </tr>
                        <%--<tr>
                            <td class="TexDer">Observacion : </td>
                            <td class="TexIzq" colspan="2">
                                <asp:TextBox ID="txtRealObs" runat="server" Height="40px" TextMode="MultiLine" Width="180px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: right">
                                <asp:Button ID="btnGuardarCostos" runat="server" CssClass="Botones" Text="Guardar" />
                            </td>
                        </tr>--%>
                         <tr>
                            <td colspan="4" style="text-align: right">
                                <asp:Button ID="btnLimpiar" runat="server" CssClass="Botones" Text="Limpiar" OnClick="btnLimpiar_Click" />
                            </td>
                        </tr>
                    </table>
                    <tr>
                    </tr>
                </table>
            </asp:Panel>

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
                                                BorderStyle="None" BorderWidth="1px" DataKeyNames="idOfaxV, idViaje, siat_cotizacion_id" CellPadding="3"
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
                                                    <asp:BoundField DataField="siat_cotizacion_id" HeaderText="siat_cotizacion_id" Visible="false" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
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
