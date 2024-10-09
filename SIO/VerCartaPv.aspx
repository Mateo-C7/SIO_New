<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="VerCartaPv.aspx.cs" Inherits="SIO.VerCartaPv" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"  namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblCliente1" runat="server" CssClass="sangria" Font-Bold="True" 
                        Font-Names="Tahoma" Font-Size="9pt" ForeColor="#1C5AB6" Text="CARTA COTIZACION PV" 
                        Width="230px"></asp:Label>
                    <rsweb:ReportViewer ID="ReporteCartaPv" runat="server" Width="1150px" 
                        BackColor="#EBEBEB" BorderColor="#EBEBEB" Height="1500px" 
                        AsyncRendering="False" ShowParameterPrompts="False">
                    </rsweb:ReportViewer>
                </td>
            </tr>
         
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
