<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="VisorReporte.aspx.cs" Inherits="SIO.CalidadReporte" Culture="auto:es-CO" UICulture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/StyleGeneral.css" rel="Stylesheet" type="text/css" />
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <table id="TblDatosPers" class="fondoazul" width="100%">
        <tr>
            <td align="center">
                <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="10pt" ForeColor="White" Text="REPORTES"></asp:Label>
            </td>
        </tr>
    </table>
            <table border="1">
                <tr>
                    <td>
                        <asp:Label ID="lbTitulo" runat="server" 
                             Width="180px"
                            Enabled="false"
                            Font-Names="Arial" 
                            ForeColor="Black"
                            Font-Size="12pt"
                            Text="Visor Reportes"
                            Font-Bold="True"
                             Font-Italic="True"
                            ></asp:Label>                    
                    </td>
                    <td>
                      <asp:DropDownList ID="DdlReporte" 
                                        AutoPostBack ="true"    
                                        runat="server" 
                                        OnSelectedIndexChanged="cmdSelecRepo"                               
                                        Width="250px"  >
                                    </asp:DropDownList>
                    </td>
                </tr>
            </table>

            <table runat="server" width="100%">
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="visorReporte" runat="server" Width="100%" Height="750px"></rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
        </ContentTemplate>            
    </asp:UpdatePanel>
     <asp:UpdateProgress ID="UpdateProgress1" runat="server">
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