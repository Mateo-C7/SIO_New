<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" 
    CodeBehind="VisitaLite.aspx.cs" Inherits="SIO.VisitaLite" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"  namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/StyleGeneral.css" rel="stylesheet" type="text/css" />   
    <style type="text/css">
hr {
    display: block;
    margin-top: 0.5em;
    margin-bottom: 0.5em;
    margin-left: 4px;
    margin-right: 0px;
    border-style: inset;
    border-width: 2px;
    color: blue;
    width: 900px;
}
        .auto-style1 {
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <asp:Panel ID="Panel4" runat="server" CssClass="Letra" Font-Names="Arial" GroupingText="Clientes Validador" Width="450px">
                            <table>
                                <tr valign="middle">
                                    <td>
                                        <asp:FileUpload ID="FileUpload3" runat="server" accept=".csv, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel" size="60px" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnValidador" runat="server" OnClick="btnValidador_Click" Text="Validar" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td>
                        <asp:Panel ID="Panel1" runat="server" CssClass="Letra" Font-Names="Arial" GroupingText="Clientes Cargue" Width="450px">
                            <table>
                                <tr valign="middle">
                                    <td>
                                        <asp:FileUpload ID="FileUploadControl" runat="server" accept=".csv, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel" size="60px" />
                                    </td>
                                    <td>
                                        <asp:Button runat="server" ID="UploadButton" Text="Cargar" OnClick="UploadButton_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <hr />
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:Panel ID="Panel2" runat="server" CssClass="Letra" Font-Names="Arial" GroupingText="Obras" Width="450px">
                            <table>
                                <tr valign="middle">
                                    <td>
                                        <asp:FileUpload ID="FileUpload1" runat="server" accept=".csv, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel" size="60px" />
                                    </td>
                                    <td>
                                        <asp:Button runat="server" ID="btnCargarObra" Text="Cargar" OnClick="btnCargarObra_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblObras" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td>
                        <asp:Panel ID="Panel3" runat="server" CssClass="Letra" Font-Names="Arial" GroupingText="Contactos" Width="450px">
                            <table>
                                <tr valign="middle">
                                    <td>
                                        <asp:FileUpload ID="FileUpload2" runat="server" accept=".csv, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel" size="60px" />
                                    </td>
                                    <td>
                                        <asp:Button runat="server" ID="btnCargarContacto" Text="Cargar" OnClick="btnCargarContacto_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblContacto" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:Panel ID="Panel5" runat="server" CssClass="Letra" Font-Names="Arial" Width="1000px">
                            <table>
                                <tr>
                                    <td>
                                        <rsweb:ReportViewer ID="ReporteVerLogLite" runat="server" Width="1000px" Height="1000px"
                                            BackColor="#EBEBEB" BorderColor="#EBEBEB"
                                            AsyncRendering="False">
                                        </rsweb:ReportViewer>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgres1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
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
