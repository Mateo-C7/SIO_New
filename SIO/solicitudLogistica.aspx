<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="solicitudLogistica.aspx.cs" Inherits="SIO.solicitudLogistica" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" 
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
            width: 250px;
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
                    <td align="right">
                        Area:                        
                    </td> 
                       <td valign="middle">
                        <asp:DropDownList ID="cboArea2" runat="server" CssClass="ComboM" AutoPostBack="false">
                        </asp:DropDownList>
                    </td>  
                    <td>
                        <asp:Label runat="server" ID="lblArea" Visible="false" Font-Bold="true" ForeColor="Blue"></asp:Label>
                        <input id="lblAreaId" runat="server" type="hidden" />
                    </td>                   
                </tr>
                <tr>
                    <td align="right">Motivo:</td>
                    <td valign="middle">
                        <asp:DropDownList ID="cboMotivo" runat="server" CssClass="ComboM" AutoPostBack="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">                        
                        Tipo Pallet:                                             
                    </td> 
                    <td>
                        <asp:DropDownList ID="cboTipoPallet" runat="server" CssClass="ComboP" AutoPostBack="true" OnSelectedIndexChanged="cboTipoPallet_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>                   
                </tr>
                </table>
            <table>
                <tr valign="top">
                    <td style="width: 60px" align="right">Orden:
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtOf" Width="80px" AutoPostBack="true" OnTextChanged="txtOf_TextChanged"></asp:TextBox>
                        <input id="lblOf" runat="server" type="hidden" />
                    </td>
                    <td>Pallet:
                        <asp:DropDownList ID="cboPallet" runat="server" CssClass="ComboP" AutoPostBack="false">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnAggProces" runat="server" CssClass="stylebtnAggN" OnClick="btnAggProces_Click" Text="&gt;" />
                        <br />
                        <asp:Button ID="btnEliProces" runat="server" CssClass="stylebtnAggN" OnClick="btnEliProces_Click" Text="&lt;" />
                        <br />
                    </td>
                    <td>
                        <asp:ListBox ID="listProces" runat="server" Font-Size="8pt" Height="46px" Width="100px"></asp:ListBox>
                    </td>
                </tr>                               
            </table>
            <table>
                <tr>
                    <td valign="middle">
                        <asp:Label runat="server" ID="lblComentario" Text="Comentario:"></asp:Label>                       
                    </td>
                    <td>
                         <asp:TextBox ID="txtComentario" TextMode="multiline" Columns="41" Rows="5" runat="server" />
                    </td>                 
                </tr>
            </table>
            <table>
                <tr>
                    <td style="width:244px"></td>
                       <td>
                        <asp:Button runat="server" ID="btnSolicitar" Text="Solicitar" CssClass="Botones" 
                               OnClick="btnSolicitar_Click" Visible="False" />
                        <asp:Button runat="server" ID="btnNuevo" Text="Nuevo" CssClass="Botones" 
                               OnClick="btnNuevo_Click" Visible="False"/>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <rsweb:reportviewer id="reporteSolicitud" runat="server" showparameterprompts="False" backcolor="White" width="1160" asyncrendering="true">
                                                        </rsweb:reportviewer>
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
