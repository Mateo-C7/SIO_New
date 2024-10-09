<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="ReportePlaneadorCotizaciones.aspx.cs" Inherits="SIO.ReportePlaneadorCotizaciones" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"  namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">        
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table class="style9">
            <tr>
                <td class="style13">
                     <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="1280px" 
                        BackColor="#EBEBEB" BorderColor="#EBEBEB" Height="" AsyncRendering="False" 
                        SizeToReportContent="True">
                    </rsweb:ReportViewer>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
       </ContentTemplate>        
    </asp:UpdatePanel>
</asp:Content>
