<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true"
    CodeBehind="VisReporteTres.aspx.cs" Inherits="SIO.ReporteVisTres" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" 
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style40
        {
            font-family: Arial;
            color: Black;
            height: 27px;
            font-weight: bold;
            font-size: 18px;
            width: 283px; 
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <div class="style40">
        <asp:Label ID="lblTitulo" runat="server"></asp:Label></div>
    <rsweb:ReportViewer ID="rvReporteVisitas" runat="server" Height="600px" Width="1144px"
        Font-Size="12px">
    </rsweb:ReportViewer>
    <rsweb:ReportViewer ID="rvReporteViajes" runat="server" Height="600px" Width="1144px"
        Font-Size="12px">
    </rsweb:ReportViewer>
</asp:Content>
