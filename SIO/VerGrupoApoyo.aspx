<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="VerGrupoApoyo.aspx.cs" Inherits="SIO.VerGrupoApoyo" %>
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
                        Font-Names="Tahoma" Font-Size="9pt" ForeColor="#1C5AB6" Text="GRUPOS DE APOYO" 
                        Width="200px"></asp:Label>
                    <rsweb:ReportViewer ID="ReporteVerClientes" runat="server" Width="1150px" 
                        BackColor="#EBEBEB" BorderColor="#EBEBEB" Height="1500px" 
                        AsyncRendering="False">
                    </rsweb:ReportViewer>
                </td>
            </tr>
         
        </table>
    </ContentTemplate>
</asp:UpdatePanel>

</asp:Content>
