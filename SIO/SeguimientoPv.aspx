<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="SeguimientoPv.aspx.cs" Inherits="SIO.SeguimientoPv" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"  namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <table __designer:mapid="208ab">
    <tr __designer:mapid="208ac">
        <td __designer:mapid="208ad">
            <rsweb:ReportViewer ID="ReporteSeguimietoPv" runat="server" Width="1150px" 
                        BackColor="#EBEBEB" BorderColor="#EBEBEB" Height="600" 
                OnPreRender="Reporte_PreRender"
                AsyncRendering="False">
            </rsweb:ReportViewer>
        </td>
    </tr>
</table>
</asp:Content>
