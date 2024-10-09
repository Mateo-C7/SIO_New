<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralMobile.Master" AutoEventWireup="true" CodeBehind="VerOrden.aspx.cs" Inherits="SIO.VerOrden" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"  namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <table   __designer:mapid="208ab">
    <tr __designer:mapid="208ac">
        <td class="style13" __designer:mapid="208ad">
            <rsweb:ReportViewer ID="ReporteOrden" runat="server" Width="900px" 
                        BackColor="#EBEBEB" BorderColor="#EBEBEB" Height="900px" 
                AsyncRendering="False">
            </rsweb:ReportViewer>
        </td>
    </tr>
</table>
</asp:Content>
<asp:Content ID="Content4" runat="server" 
    contentplaceholderid="ContentPlaceHolder3">
                       
                      
        
                        
                    </asp:Content>

