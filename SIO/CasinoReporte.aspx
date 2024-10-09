<%@ Page Title="ReporteCasino" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true"
    CodeBehind="CasinoReporte.aspx.cs" Inherits="SIO.ReporteCasinoUno" Culture="auto"
    UICulture="auto" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/StyleGeneral.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content5" runat="server" ContentPlaceHolderID="ContentPlaceHolder4">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="PanelDatos" runat="server" CssClass="Letra" Width="741px">
                <table id="tablaGen" runat="server" style="height: 88px; width: 723px">
                    <tr>
                        <td>
                        </td>
                        <td class="TexIzq">
                            Fecha Inicial :
                            <asp:TextBox ID="txtFechaInicial" runat="server" CssClass="TexboxP"></asp:TextBox>
                            <asp:CalendarExtender ID="txtFechaInicialCE" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaInicial">
                            </asp:CalendarExtender>
                        </td>
                        <td class="TexIzq">
                            Fecha Final:&nbsp;
                            <asp:TextBox ID="txtFechaFinal" runat="server" CssClass="TexboxP"></asp:TextBox>
                            <asp:CalendarExtender ID="txtFechaFinalCE" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaFinal">
                            </asp:CalendarExtender>
                        </td>
                        <td>
                            <asp:Button ID="btnVerReportes" runat="server" Text="Ver Reporte" OnClick="btnVerReportes_Click"
                                CssClass="Botones" />
                        </td>
                    </tr>
                    <tr runat="server" id="trNmunos">
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td style="background-color: #75BEF6">
                            <asp:Button ID="btnNmUnoF" runat="server" Text="NMUNOFR" OnClick="btnNmUnoF_Click"
                                CssClass="Botones" />
                            &nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnNmUnoA" runat="server" Text="NMUNOAS" OnClick="btnNmUnoA_Click"
                                CssClass="Botones" />
                        </td>
                    </tr>
                    <tr runat="server" id="trERP">
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnERP" runat="server" Text="ERP FR" OnClick="btnERP_Click" CssClass="Botones" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnERPAS" runat="server" Text="ERP AS" OnClick="btnERPAS_Click" CssClass="Botones" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnAprobado" runat="server" Text="Aprobado" OnClick="btnAprobado_Click"
                                CssClass="Botones" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div style="overflow: auto; width: 1100px; height: 555px;">
                <rsweb:ReportViewer ID="rvReporteUno" runat="server" Height="600px" Width="1500px">
                </rsweb:ReportViewer>
                <rsweb:ReportViewer ID="rvReporteDos" runat="server" Height="600px" Width="1500px">
                </rsweb:ReportViewer>
            </div>
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
