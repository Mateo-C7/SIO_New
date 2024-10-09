<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true"
    CodeBehind="SiatGastos.aspx.cs" Inherits="SIO.SiatGastos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/StyleGeneral.css" rel="stylesheet" type="text/css" />
    <link href="Styles/StyleSIAT.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style5
        {
            width: 74px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelCostos" runat="server" Height="379px" CssClass="Letra" GroupingText="COSTOS"
                Width="277px">
                <table style="height: 77px; width: 259px">
                    <tr>
                        <td class="TexDer">
                            Zona :
                        </td>
                        <td>
                            <asp:DropDownList ID="cboZona" runat="server" CssClass="ComboM" OnSelectedIndexChanged="cboZona_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="TexDer">
                            Pais :
                        </td>
                        <td>
                            <asp:DropDownList ID="cboPais" runat="server" CssClass="ComboM" OnSelectedIndexChanged="cboPais_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="trZonaC" runat="server">
                        <td class="TexDer">
                            Zona Ciudad :
                        </td>
                        <td>
                            <asp:DropDownList ID="cboZonaC" runat="server" CssClass="ComboM" AutoPostBack="true"
                                OnSelectedIndexChanged="cboZonaC_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style5">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <table style="height: 193px; width: 253px">
                    <tr>
                        <td class="TexCen">
                            Costos Planeado
                        </td>
                        <td class="TexCen">
                            Costo Plan
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="TexDer">
                            Hotel :
                        </td>
                        <td class="TexDer">
                            <asp:TextBox ID="txtPlanHotel" runat="server" CssClass="TexboxN"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Custom, Numbers"
                                ValidChars="." TargetControlID="txtPlanHotel">
                            </asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TexDer">
                            Tiquetes :
                        </td>
                        <td class="TexDer">
                            <asp:TextBox ID="txtPlanTiq" runat="server" CssClass="TexboxN"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Custom, Numbers"
                                ValidChars="." TargetControlID="txtPlanTiq">
                            </asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TexDer">
                            Alimentacion :
                        </td>
                        <td class="TexDer">
                            <asp:TextBox ID="txtPlanAli" runat="server" CssClass="TexboxN"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Custom, Numbers"
                                ValidChars="." TargetControlID="txtPlanAli">
                            </asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TexDer">
                            Transporte Interno :
                        </td>
                        <td class="TexDer">
                            <asp:TextBox ID="txtPlanTranInt" runat="server" CssClass="TexboxN"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Custom, Numbers"
                                ValidChars="." TargetControlID="txtPlanTranInt">
                            </asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TexDer">
                            Transporte Aeropuerto :
                        </td>
                        <td class="TexDer">
                            <asp:TextBox ID="txtPlanTranAer" runat="server" CssClass="TexboxN"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Custom, Numbers"
                                ValidChars="." TargetControlID="txtPlanTranAer">
                            </asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TexDer">
                            Llamadas :
                        </td>
                        <td class="TexDer">
                            <asp:TextBox ID="txtPlanLlam" runat="server" CssClass="TexboxN"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Custom, Numbers"
                                ValidChars="." TargetControlID="txtPlanLlam">
                            </asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TexDer">
                            Lavanderia :
                        </td>
                        <td class="TexDer">
                            <asp:TextBox ID="txtPlanLav" runat="server" CssClass="TexboxN"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Custom, Numbers"
                                ValidChars="." TargetControlID="txtPlanLav">
                            </asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TexDer">
                            Penalidades :
                        </td>
                        <td class="TexDer">
                            <asp:TextBox ID="txtPlanPen" runat="server" CssClass="TexboxN"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" FilterType="Custom, Numbers"
                                ValidChars="." TargetControlID="txtPlanPen">
                            </asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td class="TexDer">
                            Otros :
                        </td>
                        <td class="TexDer">
                            <asp:TextBox ID="txtPlanOtros" runat="server" CssClass="TexboxN"></asp:TextBox>
                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" FilterType="Custom, Numbers"
                                ValidChars="." TargetControlID="txtPlanOtros">
                            </asp:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: center">
                            <asp:Button ID="btnGuardarC" runat="server" Text="Guardar" CssClass="Botones" OnClick="btnGuardarC_Click" />
                            <asp:Button ID="btnActC" runat="server" Text="Actualizar" CssClass="Botones" OnClick="btnActC_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
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
