<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="VerObras.aspx.cs" Inherits="SIO.VerObras" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"  namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .style14
    {
        width: 100%;
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

    &nbsp;<asp:Label ID="lblClienteTitulo" runat="server" CssClass="sangria" 
                        Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" ForeColor="#1C5AB6" 
                        Text="OBRAS" Width="120px"></asp:Label>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table >
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="ReportVerContactos" runat="server" 
                            AsyncRendering="False" BackColor="#EBEBEB" BorderColor="#EBEBEB" 
                            Height="1500px" Width="1100px">
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
