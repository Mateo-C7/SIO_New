<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="SiatPlaneacion.aspx.cs" Inherits="SIO.SiatPlaneacion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="CalendarioMetro/lib/jquery-impromptu.js" type="text/javascript"></script>
    <script src="Scripts/ScriptSIAT.js" type="text/javascript"></script>
    <link href="Styles/StyleGeneral.css" rel="stylesheet" type="text/css" />
    <link href="Styles/StyleSIAT.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="CalendarioMetro/stylos/jquery-impromptu.css" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <div class="styleBotones">
                            <div class="styleYear">
                                <asp:ImageButton ID="btnImaAtras" runat="server" ImageUrl="~/iconosMetro/quitar1.png"
                                    OnClick="btnImaAtras_Click" />
                                &nbsp;
                                <asp:Label ID="lblAno" runat="server"></asp:Label>
                                &nbsp;<asp:ImageButton ID="btnImaSig" runat="server" ImageUrl="~/iconosMetro/next1.png"
                                    OnClick="btnImaSig_Click" /></div>
                        </div>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="actividad">
            </div>
            <div id="planGen">
            </div>
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
