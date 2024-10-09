<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="VerListaAccesorios.aspx.cs" Inherits="SIO.VerListaAccesorios" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"  namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <table >
        <tr >
            <td >
                <rsweb:ReportViewer ID="ReporteListAcc" runat="server" Width="1150px" 
                        BackColor="#EBEBEB" BorderColor="#EBEBEB" Height="1200px" 
                    AsyncRendering="False">
                </rsweb:ReportViewer>
            </td>
        </tr>
    </table>
</asp:Content>
