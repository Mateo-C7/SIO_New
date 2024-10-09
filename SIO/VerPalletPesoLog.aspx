<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="VerPalletPesoLog.aspx.cs" Inherits="SIO.VerPalletPesoLog" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" 
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
                <asp:Button ID="btnCapturaPeso1" runat="server" OnClick="btnCapturaPeso1_Click" Text="Volver Captura Peso" Font-Bold="true" ForeColor="#1C5AB6"/>
            </td>
        </tr>
        <tr>
            <td>
                <rsweb:ReportViewer ID="reportePesoLog" runat="server" Height="1000px" Width="780px"
                    Font-Size="10px">
                </rsweb:ReportViewer>
            </td>
        </tr>
    </table>   
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
