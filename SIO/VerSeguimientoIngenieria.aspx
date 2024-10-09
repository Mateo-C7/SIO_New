<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralAmplia.Master" AutoEventWireup="true" CodeBehind="VerSeguimientoIngenieria.aspx.cs" Inherits="SIO.VerSeguimientoIngenieria" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <rsweb:ReportViewer ID="ReporteSeguimientoIngenieria" runat="server" Width="1150px" 
                        BackColor="#EBEBEB" BorderColor="#EBEBEB" 
    Height="1200px" ShowExportControls="True" ShowPrintButton="True">
    </rsweb:ReportViewer>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
</asp:Content>
