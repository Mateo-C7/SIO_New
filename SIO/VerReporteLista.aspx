<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="VerReporteLista.aspx.cs" Inherits="SIO.VerReporteLista" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"  namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="1500px" Width="" 
        AsyncRendering="False" BackColor="#EBEBEB" BorderColor="#EBEBEB">
    </rsweb:ReportViewer>
</asp:Content>
