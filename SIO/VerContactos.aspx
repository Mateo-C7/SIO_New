<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="VerContactos.aspx.cs" Inherits="SIO.VerContactos" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style9
    {
            width: 300px;
            height: 58px;
        }
        .style13
        {
            height: 51px;
        }
        .sangria
        {
            word-spacing: 10pt;
            font-family: Tahoma;
            font-size: 11pt;
            color: #1C5AB6;
        }
        </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table >
            <tr>
                <td>
                    <asp:Label ID="lblClienteTitulo" runat="server" CssClass="sangria" 
                        Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" ForeColor="#1C5AB6" 
                        Text="CONTACTOS" Width="120px"></asp:Label>
                    <rsweb:ReportViewer ID="ReportVerContactos" runat="server" Width="1150px" 
                        BackColor="#EBEBEB" BorderColor="#EBEBEB" Height="1500px" 
                        AsyncRendering="False">
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
