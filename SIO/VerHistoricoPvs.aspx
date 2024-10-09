<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="VerHistoricoPvs.aspx.cs" Inherits="SIO.VerHistoricoPvs" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"  namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <table class="style9" __designer:mapid="208ab">
    <tr __designer:mapid="208ac">
        <td class="style13" __designer:mapid="208ad">
            <rsweb:ReportViewer ID="ReporteInformeOrdenes" runat="server" Width="1150px" 
                        BackColor="#EBEBEB" BorderColor="#EBEBEB" Height="1500px" 
                OnPreRender="Reporte_PreRender"
                AsyncRendering="False">
            </rsweb:ReportViewer>
        </td>
    </tr>
</table>
</asp:Content>
