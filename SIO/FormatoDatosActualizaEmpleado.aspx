<%@ Page Title="" Language="C#" MasterPageFile="~/ReporteEmpleado.Master" AutoEventWireup="true" CodeBehind="FormatoDatosActualizaEmpleado.aspx.cs" Inherits="SIO.FormatoDatosActualizaEmpleado" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"  namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <link href="Styles/StyleGeneral.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">

        <ContentTemplate>
            <asp:Button ID="Button2" runat="server" Text="Regresar al inicio" OnClick="Button1_Click" />

            <table runat="server" width="100%">
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="reporteActualizaDatosEmpleado" runat="server" Width="800px" Height="800px" SizeToReportContent="True"></rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
        <ProgressTemplate>
            <div class="overlay" />
            <div class="overlayContent">
                <asp:Label ID="lblEnviando" runat="server" Text="Cargando..." Font-Names="Arial"
                    Font-Size="14pt"></asp:Label>
                <img src="Imagenes/ajax-loader.gif" alt="Loading" height="30" style="text-align: center"
                    width="30" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>


</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
   
        
</asp:Content>
