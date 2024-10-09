<%@ Page Title="" Language="C#" MasterPageFile="~/Mobil.Master" AutoEventWireup="true" CodeBehind="MobilGeneral.aspx.cs" Inherits="SIO.MobilContacto" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">    
 
          .CustomComboBoxStyle .ajax__combobox_textboxcontainer input 
        {
            background-image:url('http://172.21.0.1/ComercialTester/Imagenes/toolkit-bg.gif');
            border-style:none;  
        }
        .CustomComboBoxStyle .ajax__combobox_buttoncontainer button 
        {
            background-image:url('http://172.21.0.1/ComercialTester/Imagenes/toolkit-arrow.gif');
            border-style:none;            
        }
 
        /* Accordion */
.accordionHeader
{
    border: 2px Outset #EBEBEB;
    color: white;
    background-color: #1C5AB6;
      font-family: Arial, Sans-Serif;
      font-size: 12px;
      font-weight: bold;
    padding: 5px;
    margin-top: 5px;
    cursor: pointer;
}
 
#master_content .accordionHeader a
{
      color: #FFFFFF;
      background: none;
      text-decoration: none;
}
 
#master_content .accordionHeader a:hover
{
      background: none;
      text-decoration: underline;
}
 
.accordionHeaderSelected
{
    border: 2px Outset #EBEBEB;
    color: white;
    background-color: #1C5AB6;
      font-family: Arial, Sans-Serif;
      font-size: 12px;
      font-weight: bold;
    padding: 5px;
    margin-top: 5px;
    cursor: pointer;
}
 
#master_content .accordionHeaderSelected a
{
      color: #FFFFFF;
      background: none;
      text-decoration: none;
}
 
#master_content .accordionHeaderSelected a:hover
{
      background: none;
      text-decoration: underline;
}
 
.accordionContent
{
    background-color: #EBEBEB;
    border: 0px outset #2F4F4F;
    border-top: none;
    padding: 5px;
    padding-top: 10px;
}

        .lados
        {
            width: 320px;
        }

        @media screen and (min-width:480px) and (max-width:639px) {.lados {width: 250px;}}
        
 
     .style6
    {
        text-align: center;
            width: 320px;
        }
 
        .style8
        {
            width: 320px;
            height: 229px;
            margin-right: 0px;
        }
        .izquierdo
        {
            width: 45px;
        }
        .style9
        {
            height: 16px;
            text-align: left;
        }
         
        .style10
        {
            width: 238px;
        }
        .fuente
        {            
            font-Size:8pt;   
            font-family:Arial;  
        }
                 
        .style11
        {
            width: 90%;
        }
                 
        .style13
        {
            width: 92%;
        }
        .style14
        {
            text-align: left;
        }
        .style15
        {
            text-align: right;
            height: 26px;
        }
                 
        .style16
        {
            width: 91%;
        }
                 
     .style17
    {
        text-align: left;
    }
                 
        .style19
        {
            text-align: right;
            height: 26px;
        }
                 
        .style20
        {
            text-align: left;
            width: 45px;
        }
        .style21
        {
            height: 16px;
            text-align: left;
            width: 39px;
        }
                 
        .botonsio
        {
            height: 22px;
        }
                 
     </style>
</asp:Content>


                                
<asp:Content ID="Content3" runat="server"  contentplaceholderid="ContentPlaceHolder2">

                <asp:ScriptManager ID="ScriptManager" runat="server">
        </asp:ScriptManager>
           
       
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table class="interno" align="center" 
    style="text-align: center; width: 297px;">
                    <tr>
                        <td class="style6"  align="center" style="text-align: center" >
                            <asp:Panel ID="Panel2" runat="server" HorizontalAlign="Center" Width="293px">
                                <table align="center" class="style8" style="text-align: center; width: 282px;">
                                    <tr>
                                        <td class="style21">
                                            <asp:Label ID="lblEncabPais" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: center" Text="Pais"></asp:Label>
                                        </td>
                                        <td class="style9">
                                            <asp:DropDownList ID="CboPaisMatrizm" runat="server" AutoPostBack="True" 
                                        Font-Names="Arial" Font-Size="8pt" 
                                        onselectedindexchanged="CboPaisMatrizm_SelectedIndexChanged" Width="228px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style21">
                                            <asp:Label ID="lblEncabCiudad" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: center" Text="Ciudad"></asp:Label>
                                        </td>
                                        <td class="style9">
                                            <asp:DropDownList ID="cboCiudadMatrizm" runat="server" AutoPostBack="True" 
                                        Font-Names="Arial" Font-Size="8pt" Width="228px" 
                                        onselectedindexchanged="cboCiudadMatrizm_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style10" style="text-align: center" colspan="2">
                                            <asp:Accordion ID="Accordion1" runat="server" 
                                        ContentCssClass="accordionContent" FadeTransitions="True" FramesPerSecond="40" 
                                        HeaderCssClass="accordionHeader" 
                                        HeaderSelectedCssClass="accordionHeaderSelected" RequireOpenedPane="False" 
                                        SuppressHeaderPostbacks="True" TransitionDuration="240" Width="280px" 
                                        Font-Names="Arial" Font-Size="8pt" SelectedIndex="-1" Height="125px">
                                                <Panes>
                                                    <asp:AccordionPane ID="AcorCliente" runat="server" 
                                                ContentCssClass="accordionContent" Font-Bold="False" Font-Names="Arial" 
                                                Font-Size="8pt" HeaderCssClass="accordionHeader" 
                                                HeaderSelectedCssClass="accordionHeaderSelected">
                                                        <Header>
                                                            <asp:Label ID="lblEncabCliente" runat="server" Text="CLIENTE"></asp:Label>
                                                        </Header>
                                                        <Content>
                                                        <table class="style11">
                                    <tr>
                                        <td class="izquierdo" style="text-align: left">
                                            <asp:Label ID="lblClienteNombre" runat="server" CssClass="fuente" 
                                                Font-Names="Arial" Font-Size="8pt" Text="Cliente"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtClienteNombre" runat="server" CssClass="fuente" 
                                                Width="210px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblDireccCliente" runat="server" CssClass="fuente" 
                                                Text="Direccion"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtDireccCliente" runat="server" CssClass="fuente" 
                                                Width="210px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                            <asp:Label ID="lblTelefoCliente" runat="server" CssClass="fuente" 
                                                Text="Telefono"></asp:Label>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="txtTelefCliente" runat="server" CssClass="fuente" 
                                                Width="210px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style15" colspan="2">
                                            <span>
                                            <asp:Button ID="btnNuevoCliente" runat="server" BackColor="#1C5AB6" 
                                                BorderColor="#999999" CssClass="botonsio" Font-Bold="True" Font-Names="Arial" 
                                                Font-Size="8pt"  Text="Nuevo" Width="80px" 
                                                onclick="btnNuevoCliente_Click" />
                                            &nbsp;&nbsp; </span>
                                            <asp:Button ID="btnGuardarCliente" runat="server" BackColor="#1C5AB6" 
                                                BorderColor="#999999" CssClass="botonsio" Font-Bold="True" Font-Names="Arial" 
                                                Font-Size="8pt"  Text="Guardar" Width="80px" 
                                                onclick="btnGuardarCliente_Click" 
                                                onclientclick="return confirm('Desea guardar los datos?')" />
                                        </td>
                                    </tr>
                                </table>
                                                        </Content>
                                                    </asp:AccordionPane>
                                                    <asp:AccordionPane ID="AcorContacto" runat="server" ContentCssClass="" 
                                                HeaderCssClass="">
                                                        <Header>
                                                            <asp:Label ID="lblEncabContacto" runat="server"  Text="CONTACTO"></asp:Label>
                                                        </Header>
                                                        <Content>
                                                        <table class="style13">
                                    <tr>
                                        <td class="style20">
                                            <span class="izquierdo">
                                            <asp:Label ID="lblClienteCont" runat="server" CssClass="fuente" Text="Cliente"></asp:Label>
                                            </span>
                                        </td>
                                        <td class="style14">
                                            <asp:DropDownList ID="cboClienteCont" runat="server" Font-Names="Arial" 
                                                Font-Size="8pt" Width="217px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style20">
                                            <span>
                                            <asp:Label ID="lblNombresCont" runat="server" CssClass="fuente" Text="Nombres"></asp:Label>
                                            </span>
                                        </td>
                                        <td class="style14">
                                            <span>
                                            <asp:TextBox ID="txtNombresCont" runat="server" CssClass="fuente" Width="210px"></asp:TextBox>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style20">
                                            <span>
                                            <asp:Label ID="lblApellidosCont" runat="server" CssClass="fuente" 
                                                Text="Apellidos"></asp:Label>
                                            </span>
                                        </td>
                                        <td class="style14">
                                            <span>
                                            <asp:TextBox ID="txtApellidosCont" runat="server" CssClass="fuente" 
                                                Width="210px"></asp:TextBox>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style20">
                                            <span>
                                            <asp:Label ID="lblCargoCont" runat="server" CssClass="fuente" Text="Cargo"></asp:Label>
                                            </span>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:DropDownList ID="cboCargoCont" runat="server" Font-Names="Arial" 
                                                Font-Size="8pt" Width="217px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style20">
                                            <span>
                                            <asp:Label ID="lblTelefonoCont" runat="server" CssClass="fuente" 
                                                Text="Telefono"></asp:Label>
                                            </span>
                                        </td>
                                        <td class="style14">
                                            <span>
                                            <asp:TextBox ID="txtTelefonoCont" runat="server" CssClass="fuente" 
                                                Width="210px"></asp:TextBox>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style20">
                                            <span>
                                            <asp:Label ID="lblCorreoCont" runat="server" CssClass="fuente" Text="E-Mail"></asp:Label>
                                            <asp:RegularExpressionValidator ID="REVMailCont" runat="server" 
                                                ControlToValidate="txtCorreoCont" ErrorMessage="RegularExpressionValidator" 
                                                Font-Bold="True" Font-Names="Arial" Font-Size="9pt" ForeColor="#CC3300" 
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">X</asp:RegularExpressionValidator>
                                            </span>
                                        </td>
                                        <td class="style14">
                                            <span>
                                            <asp:TextBox ID="txtCorreoCont" runat="server" CssClass="fuente" Width="210px"></asp:TextBox>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style15" colspan="2">
                                            <span>
                                            <asp:Button ID="btnNuevoCont" runat="server" BackColor="#1C5AB6" 
                                                BorderColor="#999999" CssClass="botonsio" Font-Bold="True" Font-Names="Arial" 
                                                Font-Size="8pt"  Text="Nuevo" Width="80px" 
                                                onclick="btnNuevoCont_Click" />
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnGuardarCont" runat="server" BackColor="#1C5AB6" 
                                                BorderColor="#999999" CssClass="botonsio" Font-Bold="True" Font-Names="Arial" 
                                                Font-Size="8pt"  Text="Guardar" Width="80px" 
                                                onclick="btnGuardarCont_Click1" 
                                                onclientclick="return confirm('Desea guardar los datos?')" />
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                                                        </Content>
                                                    </asp:AccordionPane>
                                                    <asp:AccordionPane ID="AcorObra" runat="server" ContentCssClass="" 
                                                HeaderCssClass="">
                                                        <Header>
                                                            <asp:Label ID="lblEncabObra" runat="server"  Text="OBRA"></asp:Label>
                                                        </Header>
                                                        <Content>
                                                       <table class="style16">
                                    <tr>
                                        <td class="style20">
                                            <span>
                                            <asp:Label ID="lblClienteObra" runat="server" CssClass="fuente" Text="Cliente"></asp:Label>
                                            </span>
                                        </td>
                                        <td class="style14">
                                            <asp:DropDownList ID="cboClienteObra" runat="server" Font-Names="Arial" 
                                                Font-Size="8pt" Width="217px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style20">
                                            <span class="izquierdo">
                                            <asp:Label ID="lblNombreObra" runat="server" CssClass="fuente" Text="Obra"></asp:Label>
                                            </span>
                                        </td>
                                        <td class="style14">
                                            <span>
                                            <asp:TextBox ID="txtNombreObra" runat="server" CssClass="fuente" Width="210px"></asp:TextBox>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style20">
                                            <span>
                                            <asp:Label ID="lblTipoObra" runat="server" CssClass="fuente" Text="Tipo"></asp:Label>
                                            </span>
                                        </td>
                                        <td class="style14">
                                            <asp:DropDownList ID="cboTipoObra" runat="server" AutoPostBack="True" 
                                                Font-Names="Arial" Font-Size="8pt" Width="217px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style20">
                                            <asp:Label ID="lblPaisObra" runat="server" CssClass="fuente" Font-Names="Arial" 
                                                Font-Size="8pt" style="text-align: center" Text="Pais"></asp:Label>
                                        </td>
                                        <td class="style14">
                                            <asp:DropDownList ID="cboPaisObra" runat="server" AutoPostBack="True" 
                                                Font-Names="Arial" Font-Size="8pt" 
                                                onselectedindexchanged="cboPaisObra_SelectedIndexChanged" Width="217px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style20">
                                            <asp:Label ID="lblCiudadObra" runat="server" CssClass="fuente" 
                                                Font-Names="Arial" Font-Size="8pt" style="text-align: center" Text="Ciudad"></asp:Label>
                                        </td>
                                        <td class="style14">
                                            <asp:DropDownList ID="cboCiudadObra" runat="server" AutoPostBack="True" 
                                                Font-Names="Arial" Font-Size="8pt" Width="217px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style20">
                                            <span>
                                            <asp:Label ID="lblEstratoObra" runat="server" CssClass="fuente" Text="Estrato"></asp:Label>
                                            </span>
                                        </td>
                                        <td class="style14">
                                            <asp:DropDownList ID="cboEstratoObra" runat="server" AutoPostBack="True" 
                                                Font-Names="Arial" Font-Size="8pt" Width="217px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style19" colspan="2">
                                            <span>
                                            <asp:Button ID="btnNuevoObra" runat="server" BackColor="#1C5AB6" 
                                                BorderColor="#999999" CssClass="botonsio" Font-Bold="True" Font-Names="Arial" 
                                                Font-Size="8pt"  Text="Nuevo" Width="80px" 
                                                onclick="btnNuevoObra_Click1" />
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnGuardarObra" runat="server" BackColor="#1C5AB6" 
                                                BorderColor="#999999" CssClass="botonsio" Font-Bold="True" Font-Names="Arial" 
                                                Font-Size="8pt"  Text="Guardar" Width="80px" 
                                                onclientclick="return confirm('Desea guardar los datos?')" 
                                                onclick="btnGuardarObra_Click" />
                                            </span>
                                        </td>
                                    </tr>
                                </table>
                                                        </Content>
                                                    </asp:AccordionPane>
                                                </Panes>
                                            </asp:Accordion>
                                        </td>
                                    </tr>
                                </table>
                                
                                
                                
                            </asp:Panel>
                            <asp:DropShadowExtender ID="Panel2_DropShadowExtender" runat="server"  
                        Enabled="True" Opacity="2" Rounded="True" TargetControlID="Panel2">
                            </asp:DropShadowExtender>
                        </td>
                    </tr>
                </table>
                 
            </ContentTemplate>
        </asp:UpdatePanel>
           
       
</asp:Content>








