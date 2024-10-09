<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true"
    CodeBehind="ReporteProyectosProduccion.aspx.cs" Inherits="SIO.ReporteProyectosProduccion" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" 
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <table class="style9" __designer:mapid="208ab">
        <tr __designer:mapid="208ac">
            <td __designer:mapid="208ad">
                <rsweb:ReportViewer ID="ReportProyProd" runat="server" Width="1150px" BackColor="#EBEBEB"
                    BorderColor="#EBEBEB" Height="1500px" AsyncRendering="False">
                </rsweb:ReportViewer>
            </td>
        </tr>
    </table>
</asp:Content>
