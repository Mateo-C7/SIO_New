<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="LogisticaReporteSolicitudDevolucion.aspx.cs" Inherits="SIO.LogisticaReporteSolicitudDevolucion" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" 
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
 <table>
        <tr>
            <td>
                <asp:Button ID="btnCapturaPeso" runat="server" OnClick="btnCapturaPeso_Click" Text="Volver Captura Peso" Font-Bold="true" ForeColor="#1C5AB6"/>
            </td>
        </tr>
        <tr>
            <td>
                <rsweb:ReportViewer ID="reporteSolicitud" runat="server" Height="1000px" Width="1200px"
                    Font-Size="10px">
                </rsweb:ReportViewer>
            </td>
        </tr>
    </table>   
</asp:Content>

