<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="VerNovedadesNomina.aspx.cs" Inherits="SIO.VerNovedadesNomina" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
     <rsweb:ReportViewer ID="ReporteProd" runat="server" Width="1150px" 
                        BackColor="#EBEBEB" BorderColor="#EBEBEB" Height="1500px" 
                        AsyncRendering="False">
    </rsweb:ReportViewer>
</asp:Content>
