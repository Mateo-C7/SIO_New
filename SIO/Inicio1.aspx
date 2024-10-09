<%@ Page Title="" Language="C#" MasterPageFile="~/Inicio.Master" AutoEventWireup="true" CodeBehind="Inicio1.aspx.cs" Inherits="SIO.Inicio11" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type='text/javascript' src='jquery-1.11.1.min.js'></script>
    <script type='text/javascript' src='jquery.modal.js'></script>
    <link href="jquery.modal.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">



$(document).ready(function(){

$('.modalLink').modal({

trigger: '.modalLink',

olay:'div.overlay',

modals:'div.modal',

animationEffect: 'slidedown',

animationSpeed: 400,

moveModalSpeed: 'slow',

background: '00c2ff',

opacity: 0.8,

openOnLoad: false,

docClose: true,

closeByEscape: true,

moveOnScroll: true,

resizeWindow: true,

video:'http://player.vimeo.com/video/9641036?color=eb5a3d',

close:'.closeBtn'

});

});
</script>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <a class="modalLink" href="#">Click Me </a>

<div class="overlay"></div>

<div class="modal">

<a href="#" class="closeBtn">Close Me</a>
 <asp:Panel ID="pnlLogo" runat="server">
          <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Imagenes/SIO.jpg" 
              Width="200px" />   
          <table class="style15">
              <tr>
                  <td>
                      <asp:Panel ID="pnlLogin" runat="server" BackColor="#D1D1D1" BorderStyle="None" 
                          Height="180px" Width="279px">
                          <table align="left" cellpadding="0" class="style5" style="border-style: none">
                              <tr>
                                  <td align="left" class="style20">
                                      &nbsp;</td>
                                  <td align="left" class="style14" style="border-style: none">
                                      <asp:Label ID="lblMSJInicio" runat="server" Font-Bold="True" Font-Names="Arial" 
                                          Font-Size="8pt" ForeColor="#CC3300" Visible="False"></asp:Label>
                                  </td>
                                  <td align="left" class="style14">
                                      &nbsp;</td>
                              </tr>
                              <tr>
                                  <td align="center" class="style18" style="background-color: #D1D1D1">
                                      &nbsp;</td>
                                  <td align="center" class="style8" style="border-style: none;">
                                      <asp:TextBox ID="txtUsuario" runat="server" BackColor="White" 
                                          BorderColor="#3399FF" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" 
                                          Font-Size="8pt" Width="250px"></asp:TextBox>
                                      
                                  </td>
                                  <td align="center" class="style8" style="background-color: #D1D1D1">
                                      &nbsp;</td>
                              </tr>
                              <tr>
                                  <td align="center" class="style21" style="background-color: #D1D1D1">
                                      &nbsp;</td>
                                  <td align="center" class="style7" style="border-style: none;">
                                      <asp:TextBox ID="txtContrasena" runat="server" BackColor="White" 
                                          BorderColor="#3399FF" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" 
                                          Font-Size="8pt" TextMode="Password" ToolTip="Contraseña" Width="250px"></asp:TextBox>
                                  </td>
                                  <td align="center" class="style7" style="background-color: #D1D1D1">
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
                                      <asp:Button ID="btnLogin" runat="server" BackColor="#1C5AB6" 
                                          BorderColor="#1C5AB6" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                          Font-Names="Arial" Font-Size="8pt" 
                                          Text="Iniciar Sesión" Width="100px" ForeColor="White" />
                                  </td>
                                  <td class="style7">
                                      &nbsp;</td>
                              </tr>
                              <tr>
                                  <td class="style20">
                                  </td>
                                  <td class="style14" style="border-style: none">
                                  </td>
                                  <td class="style14">
                                  </td>
                              </tr>
                          </table>
                      </asp:Panel>
                  </td>
              </tr>
              <tr>
                  <td>
                      <asp:Panel ID="Panel1" runat="server">
                          <table class="style15">
                              <tr>
                                  <td>
                                      <br />
                                      <asp:LinkButton ID="lkRecuperar" runat="server" Font-Bold="False" 
                                          Font-Italic="False" Font-Names="Arial" Font-Size="8pt" Font-Underline="False" 
                                          ForeColor="#1C5AB6" Width="146px">¿Olvidaste Tu Contraseña?</asp:LinkButton>
                                  </td>
                              </tr>
                              <tr>
                                  <td class="style16">
                                      <asp:LinkButton ID="lkCambiarContra" runat="server" Font-Bold="False" 
                                          Font-Names="Arial" Font-Size="8pt" Font-Underline="False" 
                                          ForeColor="#1C5AB6" Width="120px">Cambiar Contraseña</asp:LinkButton>
                                  </td>
                              </tr>
                          </table>
                      </asp:Panel>
                  </td>
              </tr>
          </table>
      </asp:Panel>
   
<!-- content here -->

</div>
    
</asp:Content>
