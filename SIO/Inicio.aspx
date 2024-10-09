<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Inicio.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="SIO.Inicio1" %>
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
        
        .nuevoEstilo2
        {          
            border-left: 1px solid #1C5AB6;
            border-right: 1px solid #1C5AB6;
            border-top: 1px solid #1C5AB6;
            cursor: pointer;
            z-index: 2;
            font-size: 14px;
            font-weight: bold;
            text-decoration: none;
            color: #ffffff;
            text-shadow: 0 1px 1px rgba(0, 0, 0, 0.35);
            background: #1C5AB6;
            background: -webkit-linear-gradient(#1c5ab6, #1fa0e4);
            background: -moz-linear-gradient(#1c5ab6, #1fa0e4);
            background: -o-linear-gradient(#1c5ab6, #1fa0e4);
            background: -ms-linear-gradient(#1c5ab6, #1fa0e4);
        }
        .style7
        {
            width: 280px;
        }
        .style8
        {
            height: 58px;
            width: 280px;
        }
        .style14
        {
            width: 280px;
            height: 19px;
            text-align: left;
        }
        .style15
        {
            width: 103%;
        }
        .style18
        {
            height: 58px;
            width: 270px;
        }
        .style20
        {
            width: 270px;
            height: 19px;
        }
        .style21
        {
            width: 270px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <asp:Panel ID="pnlLogo" runat="server" style="text-align: center" 
    Height="264px" BorderStyle="None" 
    >
    <asp:DropShadowExtender ID="Image1_DropShadowExtender" runat="server" 
                        Enabled="True" TargetControlID="pnlLogin">
                    </asp:DropShadowExtender>

          <table class="style15">
              <tr>
                  <td>
                      <asp:Panel ID="pnlLogin" runat="server" BorderStyle="None" 
                          Height="269px" Width="279px" CssClass="fondoazul" >
                          <table align="left" cellpadding="0"
                              >
                              <tr>
                                  <td align="left" class="style20">
                                      &nbsp;</td>
                                  <td align="left" style="text-align: center">
                                      <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Imagenes/SIO1.png" 
                                          Width="200px" />
                                          <asp:Label ID="lblConectadoA" runat="server" Text="Label" Font-Names="Arial"  Visible ="false" 
                             Font-Size="7pt"></asp:Label>
                                  </td>
                                  <td align="left" class="style14">
                                      &nbsp;</td>
                              </tr>
                              <tr>
                                  <td align="left" class="style20">
                                      &nbsp;</td>
                                  <td align="left" class="style14">
                                      <asp:Label ID="lblMSJInicio" runat="server" Font-Bold="True" Font-Names="Arial" 
                                          Font-Size="8pt" ForeColor="#FFFF66" Visible="False"></asp:Label>
                                  </td>
                                  <td align="left" class="style14">
                                      &nbsp;</td>
                              </tr>
                              <tr>
                                  <td align="center" class="style18">
                                      &nbsp;</td>
                                  <td align="center" class="style8">
                                      <asp:TextBox ID="txtUsuario" runat="server" BackColor="#EBEBEB" 
                                          BorderColor="#3399FF" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" 
                                          Font-Size="8pt" Width="250px"></asp:TextBox>
                                      <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" 
                                          TargetControlID="txtUsuario" WatermarkCssClass="watermarked" 
                                          WatermarkText="Nombre De Usuario">
                                      </asp:TextBoxWatermarkExtender>
                                  </td>
                                  <td align="center" class="style8">
                                      &nbsp;</td>
                              </tr>
                              <tr>
                                  <td align="center" class="style21">
                                      &nbsp;</td>
                                  <td align="center" class="style7" style="border-style: none;">
                                      <asp:TextBox ID="txtContrasena" runat="server" BackColor="#EBEBEB" 
                                          BorderColor="#3399FF" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" 
                                          Font-Size="8pt" TextMode="Password" ToolTip="Contraseña" Width="250px"></asp:TextBox>
                                  </td>
                                  <td align="center" class="style7">
                                      &nbsp;</td>
                              </tr>
                              <tr>
                                  <td class="style21">
                                      &nbsp;</td>
                                  <td class="style7" style="border-style: none">
                                      &nbsp;</td>
                                  <td class="style7">
                                      &nbsp;</td>
                              </tr>
                              <tr>
                                  <td class="style21">
                                      &nbsp;</td>
                                  <td align="center" class="style7" style="border-style: none">
                                      <asp:Button ID="btnLogin" runat="server" BackColor="#CCCCCC" 
                                          BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                          Font-Names="Arial" Font-Size="8pt"  onclick="btnLogin_Click" 
                                          Text="Iniciar Sesión" Width="100px" ForeColor="#0033CC" />
                                  </td>
                                  <td class="style7">
                                      &nbsp;</td>
                              </tr>
                              <tr>
                                  <td class="style20">
                                      &nbsp;</td>
                                  <td style="border-style: none">
                                      <asp:Label ID="lblIdioma" runat="server" Font-Bold="True" Font-Names="Arial" 
                                          Font-Size="8pt" style="text-align: right" Text="Idioma  " Visible="False" 
                                          Width="80px"></asp:Label>
                                      <asp:DropDownList ID="cboIdioma" runat="server" AutoPostBack="True" 
                                          Font-Names="Arial" Font-Size="8pt" 
                                          onselectedindexchanged="cboIdioma_SelectedIndexChanged" Visible="False" 
                                          Width="100px">
                                          <asp:ListItem>Español</asp:ListItem>
                                          <asp:ListItem>Ingles</asp:ListItem>
                                          <asp:ListItem>Portugues</asp:ListItem>
                                      </asp:DropDownList>
                                  </td>
                                  <td class="style14">
                                      &nbsp;</td>
                              </tr>
                              <tr>
                                  <td class="style20">
                                  </td>
                                  <td style="border-style: none">
                                      <asp:LinkButton ID="lkRecuperar" runat="server" Font-Bold="False" 
                                          Font-Italic="False" Font-Names="Arial" Font-Size="8pt" Font-Underline="False" 
                                          ForeColor="White" onclick="lkRecuperar_Click" Width="146px">¿Olvidaste Tu Contraseña?</asp:LinkButton>
                                      <asp:LinkButton ID="lkCambiarContra" runat="server" Font-Bold="False" 
                                          Font-Names="Arial" Font-Size="8pt" Font-Underline="False" ForeColor="White" 
                                          onclick="lkCambiarContra_Click" Width="100px">Cambiar Contraseña</asp:LinkButton>
                                  </td>
                                  <td class="style14">
                                  </td>
                              </tr>
                          </table>
                      </asp:Panel>
                  </td>
              </tr>
          </table>
      </asp:Panel>

    <asp:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" 
          TargetControlID="pnlLogin" Radius="6"
    Corners="All" BorderColor="209, 209, 209" Color="209, 209, 209">
    </asp:RoundedCornersExtender>
       
      <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" TargetControlID="txtContrasena"
    WatermarkText="Contraseña"
    WatermarkCssClass="watermarked" >
    </asp:TextBoxWatermarkExtender>
</asp:Content>


<asp:Content ID="Content3" runat="server" 
    contentplaceholderid="ContentPlaceHolder2">
                
         <table class="l">
             </table>
                
        </asp:Content>



