<%@ Page Title="Home" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="SIO.Home" %><%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <style type="text/css">
        .style1
        {
            width: 100%;
            height: 20px;
            margin-top: 0px;
        }
        .style11
        {
            height: 139px;
            text-align: right;
            width: 70px;
        }
        .style14
        {
            width: 872px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <table class="style1">
        <tr>
            <td>
            <asp:Label ID="lblConectados" runat="server" Text="Label" Font-Names="Arial"  Visible="false"
                             Font-Size="7pt"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
           
        </tr>
        <tr>
            <td class="style11">
               <%-- <rsweb:ReportViewer ID="ReportViewer1" runat="server" BackColor="#E4E4E4" 
                    ShowToolBar="False" style="margin-left: 0px" WaitControlDisplayAfter="1200" 
                    Width="1150px" Height="777px" AsyncRendering="False">
                </rsweb:ReportViewer>--%>
                </td>
           
        </tr>
        </table>
    <table class="style1">
        <tr>
            <td class="style14">

                &nbsp;</td>
        
        </tr>
    </table>
</asp:Content>



