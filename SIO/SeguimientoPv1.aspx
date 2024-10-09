<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="SeguimientoPv1.aspx.cs" Inherits="SIO.WebForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
  
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <div>
        <table border="2">
            <tr>
                <td colspan="2" align="center">
                    <asp:Label ID="Label1" runat="server" Text="Datos de solicitud" Font-Bold="True"
                        Font-Underline="True" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Nombre:" />
                </td>
                <td>
                    <asp:TextBox ID="txtNombre" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="e-mail:" />
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Situación:" />
                </td>
                <td>
                    <asp:TextBox ID="txtSituacion" runat="server" Width="300" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnAsistente" runat="server" Text="Asistente de selección" Width="150" />
                </td>
                <td>
                    <asp:Button ID="btnGrabar" runat="server" Text="Grabar datos" Width="125" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <asp:Panel ID="pnlSeleccionarDatos" runat="server" CssClass="CajaDialogo" Style="display: none;">
            <div style="padding: 10px; background-color: #0033CC; color: #FFFFFF;">
                <asp:Label ID="Label4" runat="server" Text="Seleccionar datos" />
            </div>
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Text="Ciudad:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCiudades" runat="server">
                                <asp:ListItem>Zamora</asp:ListItem>
                                <asp:ListItem>Teruel</asp:ListItem>
                                <asp:ListItem>Salamanca</asp:ListItem>
                                <asp:ListItem>Sevilla</asp:ListItem>
                                <asp:ListItem>Lugo</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Mes:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMeses" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Text="Año:" />
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlAnualidades" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" />
                &nbsp;&nbsp;
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" />
            </div>
        </asp:Panel>
        <asp:ModalPopupExtender ID="mpeSeleccion" runat="server" TargetControlID="btnAsistente"
            PopupControlID="pnlSeleccionarDatos" OkControlID="btnAceptar" CancelControlID="btnCancelar"
            OnOkScript="mpeSeleccionOnOk()" OnCancelScript="mpeSeleccionOnCancel()" DropShadow="True"
            BackgroundCssClass="FondoAplicacion" />
    </div>
 
</asp:Content>
