<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="ReporteDespExterior.aspx.cs" Inherits="SIO.ReporteDespExterior" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
      <table __designer:mapid="208ab">
    <tr __designer:mapid="208ac">
        <td __designer:mapid="208ad">
            <rsweb:reportviewer id="ReportDespExterior" runat="server" width="1150px"
                backcolor="#EBEBEB" bordercolor="#EBEBEB" height="600"
                asyncrendering="False">
            </rsweb:reportviewer>
        </td>
    </tr>
          </table>
</asp:Content>
