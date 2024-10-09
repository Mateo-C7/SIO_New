<%@ Page Title="" Language="C#" MasterPageFile="~/MobilInicioM.Master" AutoEventWireup="true" CodeBehind="MobilInicio.aspx.cs" Inherits="SIO.MobilInicio" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .watermarked 
        {
            padding:2px 0 0 2px;
            border:1px solid #BEBEBE;
            background-color:aqua;
            color:Gray;
            font-family:Arial;
            font-weight:lighter;    
        }
        .style3
        {
            height: 23px;
        }
        .style5
    {
        height: 159px;
    }
        .style6
        {
            height: 10px;
        }
        .style7
        {
            height: 12px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <table class="bueno">
        <tr>
            <td class="style5">
                <asp:Panel ID="pnlLogin" runat="server" CssClass="bueno" Height="292px">
                    <table class="bueno">
                        <tr>
                            <td class="style7" style="font-size: 3pt">
                                </td>
                            <td style="text-align: center; font-size: 1pt;" class="style7">
                            </td>
                            <td class="style7" style="font-size: 3pt">
                                </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td style="text-align: center">
                                <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Imagenes/SIO.jpg" 
                                    Width="200px" />
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td style="text-align: center">
                                <asp:Label ID="lblMSJInicio" runat="server" Font-Bold="True" Font-Names="Arial" 
                                    Font-Size="8pt" ForeColor="#CC3300" Visible="False"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtUsuario" runat="server" BackColor="White" 
                                    BorderColor="#3399FF" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" 
                                    Font-Size="8pt" Width="250px"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="txtUsuario_TextBoxWatermarkExtender" 
                                    runat="server" Enabled="True" TargetControlID="txtUsuario" 
                                    WatermarkCssClass="watermarked" WatermarkText="Nombre De Usuario">
                                </asp:TextBoxWatermarkExtender>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtContrasena" runat="server" BackColor="White" 
                                    BorderColor="#3399FF" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" 
                                    Font-Size="8pt" TextMode="Password" ToolTip="Contraseña" Width="250px"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" 
                                    TargetControlID="txtContrasena" WatermarkCssClass="watermarked" 
                                    WatermarkText="Contraseña">
                                </asp:TextBoxWatermarkExtender>
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:Button ID="btnLogin" runat="server" BackColor="#1C5AB6" 
                                    BorderColor="#1C5AB6" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                    Font-Names="Arial" Font-Size="8pt"  onclick="btnLogin_Click" 
                                    Text="Iniciar Sesión" Width="100px" />
                            </td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
        <asp:Panel ID="Panel1" runat="server" Height="37px" HorizontalAlign="Center" 
                style="background-color: #1C5AB6; margin-right: 0px;" CssClass="bueno">
                <table class="bueno">
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td class="style2">
                            <table class="style3">
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblIdioma" runat="server" Font-Bold="True" Font-Names="Arial" 
                                            Font-Size="8pt" ForeColor="Black" style="text-align: right; color: #FFFFFF;" 
                                            Text="Idioma  " Width="80px"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:DropDownList ID="cboIdioma" runat="server" AutoPostBack="True" 
                                            Font-Names="Arial" Font-Size="8pt" 
                                            onselectedindexchanged="cboIdioma_SelectedIndexChanged" Width="100px">
                                            <asp:ListItem>Español</asp:ListItem>
                                            <asp:ListItem>Ingles</asp:ListItem>
                                            <asp:ListItem>Portugues</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Content>


