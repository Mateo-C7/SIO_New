<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="VerEmbalaje.aspx.cs" Inherits="SIO.VerEmbalaje" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"  namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <style type="text/css">

        .sangria
        {
            word-spacing: 10pt;
            font-family: Tahoma;
            font-size: 11pt;
            color: #1C5AB6;
        }
        </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblCliente1" runat="server" CssClass="sangria" Font-Bold="True" 
                        Font-Names="Tahoma" Font-Size="9pt" ForeColor="#1C5AB6" Text="EMBALAJE" 
                        Width="120px"></asp:Label>
                    <rsweb:ReportViewer ID="ReporteEmbalaje" runat="server" Width="100%" 
                        BackColor="#EBEBEB" BorderColor="#EBEBEB" Height="1500px" 
                        AsyncRendering="False">
                    </rsweb:ReportViewer>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

