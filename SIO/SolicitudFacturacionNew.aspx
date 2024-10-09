<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="SolicitudFacturacionNew.aspx.cs" Inherits="SIO.SolicitudFacturacionGBI" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" 
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
         function NavigateCartaPv() {
            javascript: window.open("VerSolFacturacion.aspx");
        }
    </script>

    <style type="text/css">
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
            border-left: 1px solid #1C5AB6;
            border-right: 1px solid #1C5AB6;
            border-top: 1px solid #1C5AB6;
            cursor: pointer;
            z-index: 2;
            font-weight: bold;
            text-decoration: none;
            color: #ffffff;
            text-shadow: 0 1px 1px rgba(0, 0, 0, 0.35);
            background: #1C5AB6;
            background: -webkit-linear-gradient(#1c5ab6, #1fa0e4);
            background: -moz-linear-gradient(#1c5ab6, #1fa0e4);
            background: -o-linear-gradient(#1c5ab6, #1fa0e4);
            background: -ms-linear-gradient(#1c5ab6, #1fa0e4);
            text-align: center
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
            border-left: 1px solid #1C5AB6;
            border-right: 1px solid #1C5AB6;
            border-top: 1px solid #1C5AB6;
            cursor: pointer;
            z-index: 2;
            font-weight: bold;
            text-decoration: none;
            color: #ffffff;
            text-shadow: 0 1px 1px rgba(0, 0, 0, 0.35);
            background: #1C5AB6;
            background: -webkit-linear-gradient(#1c5ab6, #1fa0e4);
            background: -moz-linear-gradient(#1c5ab6, #1fa0e4);
            background: -o-linear-gradient(#1c5ab6, #1fa0e4);
            background: -ms-linear-gradient(#1c5ab6, #1fa0e4);
            text-align: center
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
            
            border: 0px outset #2F4F4F;
            border-top: none;
            padding: 5px;
            padding-top: 10px;
        }
        .style1
        {
            width: 5px;
        }
        
        .CustomComboBoxStyle .ajax__combobox_buttoncontainer button 
        {
            background-image:url('http://app.forsa.com.co/SIOMaestros/Imagenes/toolkit-arrow.gif');
            border-style:none;            
        }
        .ajax__combobox_buttoncontainer button
{
    background-image: url(mvwres://AjaxControlToolkit, Version=4.1.60501.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e/ComboBox.arrow-down.gif);
    background-position: center;
    background-repeat: no-repeat;
    border-color: ButtonFace;
    height: 15px;
    width: 15px;
}
.ajax__combobox_buttoncontainer button
{
    background-image: url(mvwres://AjaxControlToolkit, Version=4.1.60501.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e/ComboBox.arrow-down.gif);
    background-position: center;
    background-repeat: no-repeat;
    border-color: ButtonFace;
    height: 15px;
    width: 15px;
}
.ajax__combobox_buttoncontainer button
{
    background-image: url(mvwres://AjaxControlToolkit, Version=4.1.60501.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e/ComboBox.arrow-down.gif);
    background-position: center;
    background-repeat: no-repeat;
    border-color: ButtonFace;
    height: 15px;
    width: 15px;
}
.ajax__combobox_buttoncontainer button
{
    background-image: url(mvwres://AjaxControlToolkit, Version=4.1.60501.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e/ComboBox.arrow-down.gif);
    background-position: center;
    background-repeat: no-repeat;
    border-color: ButtonFace;
    height: 15px;
    width: 15px;
}
.ajax__combobox_buttoncontainer button
{
    background-image: url(mvwres://AjaxControlToolkit, Version=4.1.60501.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e/ComboBox.arrow-down.gif);
    background-position: center;
    background-repeat: no-repeat;
    border-color: ButtonFace;
    height: 15px;
    width: 15px;
}
.ajax__combobox_buttoncontainer button
{
    background-image: url(mvwres://AjaxControlToolkit, Version=4.1.60501.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e/ComboBox.arrow-down.gif);
    background-position: center;
    background-repeat: no-repeat;
    border-color: ButtonFace;
    height: 15px;
    width: 15px;
}
.ajax__combobox_buttoncontainer button
{
    background-image: url(mvwres://AjaxControlToolkit, Version=4.1.60501.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e/ComboBox.arrow-down.gif);
    background-position: center;
    background-repeat: no-repeat;
    border-color: ButtonFace;
    height: 15px;
    width: 15px;
}
.ajax__combobox_buttoncontainer button
{
    background-image: url(mvwres://AjaxControlToolkit, Version=4.1.60501.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e/ComboBox.arrow-down.gif);
    background-position: center;
    background-repeat: no-repeat;
    border-color: ButtonFace;
    height: 15px;
    width: 15px;
}
.ajax__combobox_buttoncontainer button
{
    background-image: url(mvwres://AjaxControlToolkit, Version=4.1.60501.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e/ComboBox.arrow-down.gif);
    background-position: center;
    background-repeat: no-repeat;
    border-color: ButtonFace;
    height: 15px;
    width: 15px;
}
.ajax__combobox_buttoncontainer button
{
    background-image: url(mvwres://AjaxControlToolkit, Version=4.1.60501.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e/ComboBox.arrow-down.gif);
    background-position: center;
    background-repeat: no-repeat;
    border-color: ButtonFace;
    height: 15px;
    width: 15px;
}
.ajax__combobox_buttoncontainer button
{
    background-image: url(mvwres://AjaxControlToolkit, Version=4.1.60501.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e/ComboBox.arrow-down.gif);
    background-position: center;
    background-repeat: no-repeat;
    border-color: ButtonFace;
    height: 15px;
    width: 15px;
}
.ajax__combobox_buttoncontainer button
{
    background-image: url(mvwres://AjaxControlToolkit, Version=4.1.60501.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e/ComboBox.arrow-down.gif);
    background-position: center;
    background-repeat: no-repeat;
    border-color: ButtonFace;
    height: 15px;
    width: 15px;
}
.ajax__combobox_buttoncontainer button
{
    background-image: url(mvwres://AjaxControlToolkit, Version=4.1.60501.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e/ComboBox.arrow-down.gif);
    background-position: center;
    background-repeat: no-repeat;
    border-color: ButtonFace;
    height: 15px;
    width: 15px;
}
.ajax__combobox_buttoncontainer button
{
    background-image: url(mvwres://AjaxControlToolkit, Version=4.1.60501.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e/ComboBox.arrow-down.gif);
    background-position: center;
    background-repeat: no-repeat;
    border-color: ButtonFace;
    height: 15px;
    width: 15px;
}
.ajax__combobox_buttoncontainer button
{
    background-image: url(mvwres://AjaxControlToolkit, Version=4.1.60501.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e/ComboBox.arrow-down.gif);
    background-position: center;
    background-repeat: no-repeat;
    border-color: ButtonFace;
    height: 15px;
    width: 15px;
}
.ajax__combobox_buttoncontainer button
{
    background-image: url(mvwres://AjaxControlToolkit, Version=4.1.60501.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e/ComboBox.arrow-down.gif);
    background-position: center;
    background-repeat: no-repeat;
    border-color: ButtonFace;
    height: 15px;
    width: 15px;
}
.ajax__combobox_buttoncontainer button
{
    background-image: url(mvwres://AjaxControlToolkit, Version=4.1.60501.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e/ComboBox.arrow-down.gif);
    background-position: center;
    background-repeat: no-repeat;
    border-color: ButtonFace;
    height: 15px;
    width: 15px;
}
        .style4
        {
            width: 54%;
        }
        .style5
        {
            height: 17px;
            text-align: left;
            width: 126px;
        }
        .style8
        {
            width: 126px;
        }
        .style9
        {
        }
        .style10
        {
            width: 170px;
        }
        .style44
        {
            height: 17px;
        }
        .style94
        {
            height: 46px;
        }
        .style98
        {
            width: 347px;
            height: 28px;
        }
        .style99
        {
            width: 94px;
            height: 28px;
        }
        .style100
        {
            width: 208px;
            height: 28px;
        }
        .style101
        {
            width: 126px;
            height: 33px;
        }
        .style102
        {
            width: 208px;
            height: 33px;
        }
        .style103
        {
            width: 94px;
            height: 33px;
        }
        .style104
        {
            height: 33px;
            width: 347px;
        }
        .style105
        {
            height: 33px;
        }
        .style108
        {
            height: 23px;
        }
        .style109
        {
            height: 28px;
        }
        .style110
        {
            height: 28px;
            width: 126px;
        }
        .style111
        {
            height: 23px;
            }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">   
    

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="style1">
                <tr>
                    <td>
                        &nbsp;</td>
                    <td style="text-align: left" class="fondoazul">
                        <asp:Label ID="txtSol" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="White"
                            Font-Size="9pt"  style="text-align: center; font-size: 10pt;" 
                            Text="SOLICITUD DE FACTURACION" Width="200px" ></asp:Label>
                            <asp:Label ID="lblClienteprincipal" runat="server" Font-Italic="True" 
                                        Font-Names="Arial" Font-Size="8pt" ForeColor="White" 
                                        style="text-align: center; font-weight: 700;" Width="350px"></asp:Label>
                             
                                       
                        <asp:Label ID="lblCotizacion" runat="server" Font-Names="Arial" Font-Size="8pt" 
                            ForeColor="White" Text="FUP" Width="34px"></asp:Label>
                        <asp:Label ID="lblfup" runat="server" Font-Italic="True" Font-Names="Arial" 
                            Font-Size="8pt" ForeColor="White" Width="80px"></asp:Label>
                        <asp:Label ID="LVersion" runat="server" Font-Names="Arial" Font-Size="8pt" 
                            ForeColor="White" Text="Version" Width="40px"></asp:Label>
                        <asp:Label ID="LVer" runat="server" Font-Bold="False" Font-Italic="True" 
                            Font-Names="Arial" Font-Size="8pt" ForeColor="White" Width="40px" 
                            style="text-align: right"></asp:Label>
                             
                                       
                        <asp:Label ID="lblTipo" runat="server" Font-Names="Arial" Font-Size="8pt" 
                            ForeColor="White" style="text-align: right" Width="80px" Visible="True"></asp:Label>
                        <asp:Label ID="lblnumeropv" runat="server" Font-Italic="True" 
                            Font-Names="Arial" Font-Size="8pt" ForeColor="White" Text="Label" 
                            Width="70px" Visible="True"></asp:Label>
                             
                                       
                    </td>
                    <td>
                    
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style94">
                        </td>
                    <td class="style94">
                        <table class="style1">
                           
                            <tr>
                                <td style="text-align: right" class="style101">
                                <asp:Label ID="Label9" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" 
                                        style="text-align: right; margin-right: 0px;" Text="Pedido Planta" 
                                        Width="90px"></asp:Label>
                                    </td>
                                <td style="text-align: left" class="style102">
                                <asp:DropDownList ID="cboPartePv" runat="server" AutoPostBack="true" 
                                        Font-Names="Arial" Font-Size="8pt" Width="40px" OnSelectedIndexChanged="cboPartePv_SelectedIndexChanged">
                                    </asp:DropDownList>
                                   
                                    
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                    <asp:Button ID="btnGenerarPartePv" runat="server" BackColor="#1C5AB6" 
                                        BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                        ForeColor="White" OnClick="btnGenerarPartePv_Click" 
                                        Text="SF A Otra Planta" Visible="false" Width="103px" />
                                </td>
                                <td class="style103">
                                    
                                   <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel1">
      <ProgressTemplate>
                    <img alt="" src="Imagenes/Indicator.gif"  style="text-align: center; float: left;" width="20"/>
               
          &nbsp;&nbsp;
               
    </ProgressTemplate>

    </asp:UpdateProgress>
                                                                        
                                </td>
                                <td style="text-align: left" class="style104">
                                    &nbsp;</td>
                                <td class="style105">
                                    </td>
                            </tr>
                            <tr>
                                <td  style="text-align: right" class="style110">
                                <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right; color: #000000;" Text="Planta Facturar" 
                                        Width="100px"></asp:Label>
                                    </td>
                                <td class="style100" style="text-align: left">
                                <asp:DropDownList ID="cboPlantaFact" runat="server" AutoPostBack="true" 
                                        Font-Names="Arial" Font-Size="8pt" Width="220px" OnSelectedIndexChanged="cboPlantaFact_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    </td>
<%--                                <td class="style99">
                                    <asp:Label ID="Label26" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right; color: #000000;" Text="Planta Produccion" 
                                        Width="109px"></asp:Label>
                                    </td>
                                <td style="text-align: left" class="style98">
                                
                                    <asp:DropDownList ID="cboPlantaProd" runat="server" AutoPostBack="true" 
                                        Font-Names="Arial" Font-Size="8pt" Width="220px" 
                                        onselectedindexchanged="cboPlantaProd_SelectedIndexChanged">
                                    </asp:DropDownList>
                                
                                    </td>--%>
                                <td class="style109">
                                    </td>
                            </tr>
<%--                            <tr>
                                <td style="text-align: right" class="style111">
                                    &nbsp;</td>
                                <td class="style108" style="text-align: left">
                                    <asp:Label ID="lblIdClienteInterno" runat="server" Visible= "false" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right; color: #000000;" Text="" 
                                        Width="100px"></asp:Label>
                                </td>
                                
                                <td class="style108">
                                
                                    <asp:Label ID="lblinterno" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right; color: #000000;" Text="Cliente Fact Interno:" 
                                        Width="109px"></asp:Label>
                                
                                   </td>
                                   <td>
                                       <asp:Label ID="lblClienteInterno" runat="server" Font-Names="Arial" 
                                           Font-Size="8pt" style="text-align: left; color: #000000;" Text="" Width="327px"></asp:Label>
                                   </td>
                            </tr>--%>
                            <tr>
                                <td class="style111" colspan="4" style="text-align: right">
                                  <table>
                                        <tr>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    style="margin-bottom: 0px; font-weight: 700;" Text="M2 Cierre:" Width="60px">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LM2Cierre" runat="server" Font-Bold="False" Font-Italic="True" 
                                                    Font-Names="Arial" Font-Size="10pt" ForeColor="#000066" style="text-align: left" 
                                                    Text="0" Width="60px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    style="margin-bottom: 0px; font-weight: 700;" Text="M2 SF:" Width="60px">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LM2Proy" runat="server" Font-Bold="False" Font-Italic="True" 
                                                    Font-Names="Arial" Font-Size="10pt" ForeColor="#000066" style="text-align: left" 
                                                    Text="0" Width="60px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    style="margin-bottom: 0px; font-weight: 700;" Text="M2 Modu Final:" Width="90px">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblM2Modulados" runat="server" Font-Bold="False" Font-Italic="True" 
                                                    Font-Names="Arial" Font-Size="10pt" ForeColor="#000066" style="text-align: left" 
                                                    Text="0" Width="60px"></asp:Label>
                                                <asp:Label ID="lblM2Actual" runat="server" Font-Bold="False" Font-Italic="True" visible="false"
                                                    Font-Names="Arial" Font-Size="10pt" ForeColor="#000066" style="text-align: left" 
                                                    Text="0" Width="5px"></asp:Label>

                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="LValorCierre" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    style="margin-bottom: 0px; font-weight: 700;" Text="Total Cierre:" Width="80px">
                                                </asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LVrCierre" runat="server" Font-Bold="False" Font-Italic="True" 
                                                    Font-Names="Arial" Font-Size="10pt" ForeColor="#000066" style="text-align: left" 
                                                    Text="0" Width="100px"></asp:Label>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="LProy" runat="server" Font-Bold="True" Font-Names="Arial" 
                                                    Font-Size="8pt" Text="Total Proyecto:" Width="100px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LVrProy" runat="server" Font-Italic="True" Font-Names="Arial" 
                                                    Font-Size="10pt" ForeColor="#000066" style="text-align: left" Text="0" 
                                                    Width="100px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                   </td>
                            </tr>
                        </table>
                    </td>
                    <td class="style94">
                        &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        
                        <asp:Accordion ID="Accordion1" runat="server" 
                          ContentCssClass="accordionContent" HeaderCssClass="accordionHeader" 
                            HeaderSelectedCssClass="accordionHeaderSelected" Width="850px" 
                            Height="5200px" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" 
                            style="text-align: left">
                          <Panes>
                                <asp:AccordionPane ID="AcorDatosVenta" runat="server"
                                    ContentCssClass="accordionContent" Font-Bold="False" Font-Names="Arial" 
                                    Font-Size="8pt" HeaderCssClass="accordionHeader" 
                                    HeaderSelectedCssClass="accordionHeaderSelected">
                                    <Header>
                                        <asp:Label ID="LEncabGeneral" runat="server" Text="Datos de Venta"></asp:Label>
                                    </Header>
                                    <Content>
                                    <table>
                                   <tr>
                                    <td style="text-align: right" >
                                    
                                    </td>
                                    <td style="text-align: center" >
                                      
                                    </td>
                                    <td style="text-align: center" >
                                       
                                    </td>
                                    <td style="text-align: right" >
                                    
                                    </td>
                                    </tr>
                                    <tr>
                                    <td style="text-align: right">
                                    <asp:Label ID="lblCentroOperacion" runat="server" Font-Names="Arial" 
                                        Font-Size="8pt" style="text-align: right" Text="Centro De Operación" 
                                        Width="110px"></asp:Label>
                                    </td>
                                    <td>
                                    <asp:DropDownList ID="cboCentOpe" runat="server" AutoPostBack="false" 
                                        Font-Names="Arial" Font-Size="8pt" Width="220px">
                                    </asp:DropDownList>
                                    </td>
                                    <td style="text-align: right">
                                    <asp:Label ID="lblCondPago" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right" Text="Condición De Pago" Width="100px"></asp:Label>
                                    </td>
                                    <td>
                                    <asp:DropDownList ID="cboCondPago" runat="server" AutoPostBack="false" 
                                        Font-Names="Arial" Font-Size="8pt" Width="220px">
                                    </asp:DropDownList>
                                         </td>
                                    </tr>
                                    <tr>
                                    <td style="text-align: right">
                                    <asp:Label ID="lblMotivo" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right" Text="Motivo" Width="60px"></asp:Label>
                                    </td>
                                    <td>
                                    <asp:DropDownList ID="cboMotivo" runat="server" AutoPostBack="false" 
                                        Font-Names="Arial" Font-Size="8pt" Width="220px">
                                    </asp:DropDownList>
                                    </td>
                                    <td style="text-align: right">
                                    <asp:Label ID="lblTipoCliente" runat="server" Font-Names="Arial" 
                                        Font-Size="8pt" style="text-align: right" Text="Tipo De Cliente" Width="100px"></asp:Label>
                                    </td>
                                    <td>
                                    <asp:DropDownList ID="cboTipoCliente" runat="server" AutoPostBack="false" 
                                        Font-Names="Arial" Font-Size="8pt" Width="220px">
                                    </asp:DropDownList>
                                    </td>
                                   
                                     <td>
                                     </td>
                                     <td>
                                     </td>
                                     <td>
                                     </td>
                                     <td>
                                     </td>
                                      </tr>
                                    <tr>
                                    <td style="text-align: right">
                                    <asp:Label ID="lblPaisFactura" runat="server" Font-Names="Arial" 
                                        Font-Size="8pt" style="text-align: right" Text="Pais Facturar" Width="80px"></asp:Label>
                                    </td>
                                    <td>
                                    <asp:DropDownList ID="cboPaisFactura" runat="server" AutoPostBack="True" 
                                        Font-Names="Arial" Font-Size="8pt" Width="220px" 
                                        OnSelectedIndexChanged="cboPaisFactura_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    </td>
                                    <td style="text-align: right">
                                    <asp:Label ID="Label17" runat="server" Font-Names="Arial" 
                                        Font-Size="8pt" style="text-align: right" Text="Vendedor" Width="100px"></asp:Label>
                                    
                                    </td>
                                    <td>
                                    <asp:DropDownList ID="cboVendedor" runat="server" AutoPostBack="false" 
                                        Font-Names="Arial" Font-Size="8pt" Width="220px">
                                    </asp:DropDownList>
                                    </td>
                                    </tr>

                                     <tr>
                                     <td>
                                     <asp:Label ID="lblDepartfactura" runat="server" Font-Names="Arial" 
                                        Font-Size="8pt" style="text-align: right" Text="Departamento Facturar" 
                                        Width="125px"></asp:Label>
                                     </td>
                                     <td>
                                     <asp:DropDownList ID="cboDepfact" runat="server" AutoPostBack="True" 
                                        Font-Names="Arial" Font-Size="8pt" Width="220px"
                                        OnSelectedIndexChanged="cboDepfact_SelectedIndexChanged">
                                    </asp:DropDownList>
                                     </td>
                                     <td style="text-align: right">
                                     
                                     </td>
                                     <td>
                                     
                                     </td>
                                      </tr>
                                    <tr>
                                    <td style="text-align: right">
                                    <asp:Label ID="lblCiuFactura" runat="server" Font-Names="Arial" 
                                        Font-Size="8pt" style="text-align: right" Text="Ciudad Facturar" 
                                        Width="100px"></asp:Label>
                                    </td>
                                    <td>
                                    <asp:DropDownList ID="cboCiuFact" runat="server" AutoPostBack="True" 
                                        Font-Names="Arial" Font-Size="8pt" Width="220px"
                                        OnSelectedIndexChanged="cboCiuFact_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    </td>
                                    
                                    </tr>
                                    
                                     
                                     <tr>
                                     <td style="text-align: right">
                                    <asp:Label ID="lblCliFactura" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right" Text="Cliente Facturar" Width="100px"></asp:Label>
                                    </td>
                                    <td colspan="4">
                                    <asp:DropDownList ID="cboClienteFacturar" runat="server" AutoPostBack="True" 
                                        Font-Names="Arial" Font-Size="8pt" Width="550px"
                                        OnSelectedIndexChanged="cboClienteFacturar_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    </td>
                                     </tr>
                                    
                                    </td>
                                    <td>
                                    
                                   </table>
                                     <table>
                                    <tr>
                                     <td>
                                    </td>
                                   
                                    <td>
                                        
                                        <asp:CheckBox ID="chkBloqueado" Text="Bloqueado" runat="server" Enabled="false"/>&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkCupo" Text="Bloqueo X Cupo" runat="server" Enabled= "false"/>&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkMora" Text="Bloqueo X Mora" runat="server" Enabled="false"/>
                                        &nbsp;&nbsp;
                                        &nbsp;&nbsp;
                                        &nbsp; 
                                        <asp:Label ID="lblNIT" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right" Text="NIT ." Width="45px"></asp:Label>
                                        <asp:Label ID="lblnitfact" runat="server" Font-Italic="True" Font-Names="Arial" 
                                           Font-Size="8pt" ForeColor="#0000CC" Text="0" Width="190px"></asp:Label>
                                  
                                    
                                    
                                    </td>
                                       </tr>
                                     <tr>
                                    <td style="text-align: right">
                                     <asp:Label ID="lblDirFactura" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right" Text="Dirección De Factura" Width="120px"></asp:Label>
                                    
                                    </td>
                                    <td>
                                    <asp:Label ID="LDirFactura" runat="server" Font-Italic="True" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="#0000CC" Text=" " Width="300px"></asp:Label>
                                    &nbsp;
                                    <asp:Label ID="lNit2" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right" Text="Nit2 ." Width="45px"></asp:Label>
                                        <asp:Label ID="lblNit2" runat="server" Font-Italic="True" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="#0000CC" Text="0" Width="190px"></asp:Label>
                                    </td>
                                    
                                    </tr>
                                   
                                    <tr>
                                    <td style="text-align: right">
                                     <asp:Label ID="Label15" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right" Text=" " Width="120px"></asp:Label>
                                    
                                    </td>
                                    <td>
                                    <asp:Label ID="Label13" runat="server"  Font-Italic="True" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="#0000CC"  Text=" " Width="300px"></asp:Label>                                   
                                                                   
                                  &nbsp;
                                    <asp:Label ID="lnit3" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right" Text="Nit3 ." Width="45px"></asp:Label>
                                        <asp:Label ID="lblNit3" runat="server" Font-Italic="True" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="#0000CC" Text="0" Width="190px"></asp:Label>
                                    </td>
                                    
                                    </tr>
                                
                                    <tr>
                                    <td style="text-align: right">
                                    <asp:Label ID="Labelfechaofac" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right" Text="Fecha Verif Ofac" Width="120px"></asp:Label>
                                    </td>
                                    <td>
                                    <asp:TextBox ID="txtFechaOfac" runat="server" AutoPostBack="True"  ForeColor="#0000CC"
                                        Font-Names="Arial" Font-Size="8pt"  ontextchanged="txtFechaOfac_TextChanged"  
                                        style="text-align: right" TabIndex="13" Width="70px" Enabled="False"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="MaskedEditExtender2" runat="server" 
                                        AutoComplete="False" CultureAMPMPlaceholder="" 
                                        CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                        CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtFechaDes" 
                                        UserDateFormat="DayMonthYear">
                                    </asp:MaskedEditExtender>
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" 
                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaOfac">
                                    </asp:CalendarExtender>
                                      <asp:Label ID="Labelestado" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right" Text="Estado" Width="40px"></asp:Label>
                                         <asp:DropDownList ID="cboEstado" runat="server" AutoPostBack="false" 
                                        Font-Names="Arial" Font-Size="8pt" Width="95px" Enabled="False" >
                                            <asp:ListItem>-</asp:ListItem>
                                            <asp:ListItem>Reportado</asp:ListItem>
                                            <asp:ListItem>Sin Reportar</asp:ListItem>
                                    </asp:DropDownList>
                                     <asp:Button ID="btnOfac" runat="server" Text="Actualizar Ofac" Width="83px" Height="20" 
                                   Visible = "false"  Font-Size = "8pt" OnClick="btnOfac_Click" />
                                   <asp:Label ID="lblBloqueo" runat="server" Font-Italic="True" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="#0000CC" Text="" Width="190px"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                  
                                       
                                    </td>
                                    <td>
                                                                          
                                        </td>
                                    </tr>
                                    </table>
                                    <table>
                                    <tr>
                                    <td>
                                        <asp:Label ID="Label4" runat="server" Text="_"></asp:Label>
                                        
                                    </td>
                                    </tr>

                                    <tr>
                                    <td style="text-align: right">
                                    <asp:Label ID="lblPaiDesp" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        Text="Pais Despacho"></asp:Label>
                                    </td>
                                    <td>
                                    <asp:DropDownList ID="cboPaiDesp" runat="server" AutoPostBack="True" 
                                        Font-Names="Arial" Font-Size="8pt" Width="220px"
                                         OnSelectedIndexChanged="cboPaiDesp_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    </td>
                                    <td style="text-align: right">
                                    
                                    </td>
                                    <td>
                                    
                                    </td>
                                    </tr>
                                    <tr>
                                     <td>
                                     <asp:Label ID="lblDepartdesp" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right" Text="Departamento Despacho" Width="124px"></asp:Label>
                                      </td>
                                      <td>
                                      <asp:DropDownList ID="cboDepdesp" runat="server" AutoPostBack="True" 
                                        Font-Names="Arial" Font-Size="8pt" Width="220px"
                                        OnSelectedIndexChanged="cboDepdesp_SelectedIndexChanged">
                                    </asp:DropDownList>
                                      </td>
                                      <td>
                                      </td>
                                      <td>
                                      </td>
                                    </tr>
                                    <tr>
                                    <td style="text-align: right">
                                    <asp:Label ID="lblCiuDesp" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right" Text="Ciudad Despacho" Width="100px"></asp:Label>
                                    </td>
                                    <td>
                                    <asp:DropDownList ID="cboCiuDesp" runat="server" AutoPostBack="True" 
                                        Font-Names="Arial" Font-Size="8pt" Width="220px"
                                        OnSelectedIndexChanged="cboCiuDesp_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    </td>
                                    </table>
                                    <table>
                                    <td style="text-align: right">
                                    <asp:Label ID="lblCliDesp" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        Text="Cliente A Despachar" Width="100px"></asp:Label>
                                    </td>
                                    <td>
                                    <asp:DropDownList ID="cboClienteDespachar" runat="server" AutoPostBack="True" 
                                        Font-Names="Arial" Font-Size="8pt" Width="550px" 
                                        OnSelectedIndexChanged="cboClienteDespachar_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td style="text-align: right">
                                    <asp:Label ID="lblDirecciondesp" runat="server" Font-Names="Arial" 
                                        Font-Size="8pt" style="text-align: right" Text="Dirección De Despacho" 
                                        Width="125px"></asp:Label>
                                    </td>
                                    <td>
                                    <asp:Label ID="LDirDespacho" runat="server" Font-Italic="True" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="#0000CC" Text=" " Width="300px"></asp:Label>
                                    &nbsp;</td>                                   
                                    </tr>

                                    <tr>
                                        <td colspan="3">
                                            <asp:Panel ID="Panel1" runat="server" 
                                                Font-Names="Arial" Font-Size="8pt" GroupingText="Dirección De Despacho" 
                                                Width="700px">
                                                <table >
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtDireccionDespVentas" runat="server" Font-Names="Arial" 
                                                                Font-Size="8pt" Height="48px" TabIndex="23" TextMode="MultiLine" Width="635px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:Panel ID="Panel2" runat="server"  
                                                Font-Names="Arial" Font-Size="8pt" GroupingText="Dirección De Envio De Documentos" 
                                                Width="700px">
                                                <table >
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtDocumentosEnvioVentas" runat="server" Font-Names="Arial" 
                                                                Font-Size="8pt" Height="48px" TabIndex="23" TextMode="MultiLine" Width="635px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>


                                    <tr>
                                        <td style="text-align: right">
                                        <asp:Label ID="Label25" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="# De Dias" Width="60px"></asp:Label>
                                        </td>
                                        <td>
                                        <asp:TextBox ID="txtDias" runat="server" AutoPostBack="True" Font-Names="Arial" 
                                            Font-Size="8pt" MaxLength="2" ontextchanged="txtDias_TextChanged" 
                                            style="text-align: right" TabIndex="12" Width="40px" Text = "1"></asp:TextBox>
                                        &nbsp;<asp:Label ID="lblDias" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="__"></asp:Label>
                                   
                                        <asp:Label ID="lblFecDesp" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            style="text-align: right" Text="Fecha De Despacho" Width="100px"></asp:Label>
                                    
                                        <asp:TextBox ID="txtFechaDes" runat="server" AutoPostBack="False" 
                                            Font-Names="Arial" Font-Size="8pt" ontextchanged="txtFechaDes_TextChanged" 
                                            style="text-align: right" TabIndex="13" Width="60px"></asp:TextBox>
                                        <asp:MaskedEditExtender ID="txtFechaDes_MaskedEditExtender" runat="server" 
                                            AutoComplete="False" CultureAMPMPlaceholder="" 
                                            CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                            CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                            Mask="99/99/9999" MaskType="Date" TargetControlID="txtFechaDes" 
                                            UserDateFormat="DayMonthYear">
                                        </asp:MaskedEditExtender>
                                        <asp:CalendarExtender ID="txtFechaDes_CalendarExtender" runat="server" 
                                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaDes">
                                        </asp:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                        &nbsp;</td>
                                        <td>
    
                                        <asp:Label ID="lblEstadoClifacturar" runat="server" Font-Italic="True" 
                                                    Font-Names="Arial" Font-Size="8pt" ForeColor="#B40404" 
                                                    style="color: #DF0101; font-weight: 700;" Width="100px"></asp:Label>        
         
                                        &nbsp;

                                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" 
        AssociatedUpdatePanelID="UpdatePanel1">
      <ProgressTemplate>
                    <img alt="" src="Imagenes/Indicator.gif"  style="text-align: center; float: left;" width="20"/>
               
          &nbsp;&nbsp;
               
    </ProgressTemplate>

    </asp:UpdateProgress>
                                        </td>
                                    
                                        <td style="text-align: center">
                                        <asp:Button ID="btnGuardarVenta" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                            BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                             Text="Guardar" Width="70px" OnClick="btnGuardarVenta_Click" />
                                        </td>
                                        </tr>
                                    </table>
                                    </Content>
                                </asp:AccordionPane>
                                <asp:AccordionPane ID="AcorPartesSolicitud" runat="server"
                                    ContentCssClass="accordionContent" Font-Bold="False" Font-Names="Arial" 
                                    Font-Size="8pt" HeaderCssClass="accordionHeader" 
                                    HeaderSelectedCssClass="accordionHeaderSelected">
                                    <Header>
                                        <asp:Label ID="LParteSol" runat="server" Text="Partes de Solicitud"></asp:Label>
                                    </Header>
                                    <Content>
                                    <table>
                            <tr>
                                <td>
                                    <table >
                                        <tr>                                   
                                            <td style="text-align: right">
                                                <asp:Label ID="LParte" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                   Text="Parte" Width="40px"></asp:Label>
                                            </td>
                                            <td>
                                                <span>
                                                    <asp:DropDownList ID="cboParte" runat="server" AutoPostBack="True" 
                                                        Font-Names="Arial" Font-Size="8pt"  Font-Bold ="true"
                                                        onselectedindexchanged="cboParte_SelectedIndexChanged" Width="40px">
                                                    </asp:DropDownList>
                                                
                                                 <asp:Label ID="lblOrden" runat="server" Font-Names="Arial" Font-Size="8pt"  Font-Bold="True"
                                                    Style="text-align: right"     Text="" Width="100px"></asp:Label>                                          
                                            </span>
                                            </td>
                                            <td style="text-align: right">
                                                <asp:Label ID="lm2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="M2"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtM2" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    Width="50px" OnTextChanged="txtM2_TextChanged" AutoPostBack="true">
                                                    </asp:TextBox>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                         <tr>
                                             <td>
                                                 <asp:Button ID="btnGenerar" runat="server" Text="Generar Parte" Font-Names="Arial" 
                                                    Font-Size="8pt" Width="100px" BorderColor="#999999" Font-Bold="True" ForeColor="White"
                                                    BackColor="#1C5AB6"  OnClick="btnGenerar_Click"/>
                                             </td>

                                         </tr>
                                        <tr>
                                            <td style="text-align: left">
                                                <asp:Label ID="Label26" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    style="text-align: right; color: #000000;" Text="Planta Produccion" 
                                                    Width="109px"></asp:Label>
                                            </td>
                                            <td style="text-align: left" class="style98">                                
                                                <asp:DropDownList ID="cboPlantaProd" runat="server" AutoPostBack="true" 
                                                    Font-Names="Arial" Font-Size="8pt" Width="220px" 
                                                    onselectedindexchanged="cboPlantaProd_SelectedIndexChanged">
                                                </asp:DropDownList>                                
                                            </td>
                                            <td style="text-align: left">
                                               
                                            </td>                               
                                            <td style="text-align: left">                                
                                                <asp:Label ID="lblinterno" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    style="text-align: left; color: #000000;" Text="Cliente Fact Interno:" 
                                                    Width="109px"></asp:Label>                                
                                            </td>
                                            <td>
                                                <asp:Label ID="lblClienteInterno" runat="server" Font-Names="Arial" 
                                                    Font-Size="8pt" style="text-align: left; color: #000000;" Text="" Width="327px"></asp:Label>
                                             <asp:Label ID="lblIdClienteInterno" runat="server" Visible= "false" Font-Names="Arial" Font-Size="8pt" 
                                                    style="text-align: left; color: #000000;" Text="" 
                                                    Width="100px"></asp:Label>

                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label10" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    Style="text-align: right"     Text="Facturar:" Width="109px"></asp:Label>  
                                            </td>
                                                                                               
                                            <td> 
                                                   <asp:DropDownList ID="cboTipoSf" runat="server" AutoPostBack="true" 
                                                        Font-Names="Arial" onselectedindexchanged="cboTipoSf_SelectedIndexChanged"  
                                                        Font-Size="8pt" Width="100px">
                                                    </asp:DropDownList>
                                                </span>
                                               
                                            </td>
                                             <td> 
                                                   <asp:Label ID="Label11" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    Style="text-align: right"     Text="Compañia:" Width="50px"></asp:Label>  
                                            </td>
                                            <td>
                                                   <span>
                                                    <asp:DropDownList ID="cboFactParte" runat="server" AutoPostBack="False" 
                                                        Font-Names="Arial" 
                                                        Font-Size="8pt" Width="120px">
                                                    </asp:DropDownList>
                                                </span>                                    
                                            </td>
                                            <td> 
                                                
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Label16" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right" Visible="false"
                                                     Text="Puerto" Width="109px"></asp:Label>
                                            </td>
                                             <td> 
                                                <span>

                                                    <asp:DropDownList ID="cboPuerto" runat="server" AutoPostBack="true"  Visible="false"
                                                        Font-Names="Arial" onselectedindexchanged="cboTipoSf_SelectedIndexChanged"  
                                                        Font-Size="8pt" Width="100px">
                                                    </asp:DropDownList>
                                                    <asp:Button ID="btnGuardaPuerto" runat="server" BackColor="#1C5AB6" 
                                                                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                                                     onclick="btnGuardaPuerto_Click" ForeColor="White"
                                                                    onclientclick="return confirm('Esta seguro de guardar?')" 
                                                                    Text="Guardar Puerto" Visible="false" Width="90px" />
                                                </span>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                            <td>
                                            </td>

                                        </tr>
                                        
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlDatosFacturacion" runat="server" 
                                        Font-Names="Arial" Font-Size="8pt" GroupingText="Datos De Facturación" 
                                        Width="700px">
                                        <table class="style4">
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblDirOfic" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        style="text-align: right" Text="Director De Oficina" Width="100px" Visible ="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cboDirector" runat="server" AutoPostBack="False" Visible ="false"
                                                        Font-Names="Arial" Font-Size="8pt" Width="170px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td></td>
                                                <td>
                                                    <asp:Label ID="lblEstadoSf" runat="server" Font-Italic="True" 
                                                Font-Names="Arial" Font-Size="8pt" ForeColor="#1C5AB6" 
                                                 style="color: #1C5AB6; font-weight: 700; text-align: right;" Width="150px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblInstPago" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        style="text-align: right" Text="Instrumento De Pago" Width="124px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cboInsPago" runat="server" AutoPostBack="False" 
                                                        Font-Names="Arial" Font-Size="8pt" Width="170px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblFormaPago" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        style="text-align: right" Text="Forma De Pago" Width="100px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cboFormaPago" runat="server" AutoPostBack="False" 
                                                        Font-Names="Arial" Font-Size="8pt" Width="170px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblTDN" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        style="text-align: right" Text="TDN" Width="40px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cboTDN" runat="server" AutoPostBack="false" 
                                                        Font-Names="Arial" Font-Size="8pt" Width="170px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: right">
                                                <asp:Label ID="lblGerente" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        style="margin-left: 24px; text-align: right;" Text="Gerente Comercial" 
                                                        Width="125px" Visible ="false"></asp:Label>
                                                    &nbsp;</td>
                                                <td>
                                                <asp:DropDownList ID="cboGerente" runat="server" AutoPostBack="False" 
                                                        Font-Names="Arial" Font-Size="8pt" Width="170px" Visible ="false">
                                                    </asp:DropDownList>
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Panel ID="pnlDirDespacho" runat="server" 
                                        Font-Names="Arial" Font-Size="8pt" GroupingText="Dirección De Despacho" 
                                        Width="700px">
                                        <table >
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtDireccionDesp" runat="server" Font-Names="Arial" 
                                                        Font-Size="8pt" Height="48px" TabIndex="23" TextMode="MultiLine" Width="635px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Panel ID="pnlEnvioDocumentos" runat="server"  
                                        Font-Names="Arial" Font-Size="8pt" GroupingText="Dirección De Envio De Documentos" 
                                        Width="700px">
                                        <table >
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtDocumentosEnvio" runat="server" Font-Names="Arial" 
                                                        Font-Size="8pt" Height="48px" TabIndex="23" TextMode="MultiLine" Width="635px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlDscto" runat="server"  Font-Names="Arial" 
                                        Font-Size="8pt" GroupingText="Valores De Descuento" Width="700px">
                                        <table>
                                            <tr >
                                                <td>
                                                    <asp:Label ID="lblVrVenta1" runat="server" Font-Names="Arial" 
                                                        Font-Size="8pt" style="text-align: right" Text="Valor De Venta" Width="100px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblvlr_vnta1" runat="server" Font-Italic="false" 
                                                        Font-Names="Arial"  Font-Size="8pt" 
                                                        ForeColor="#0000CC" Height="16px" style="text-align: right" Text="0" Width="170px"></asp:Label>
                                                </td>
                                                <td >
                                                    <asp:Label ID="Label12" runat="server" Font-Names="Arial" 
                                                        Font-Size="8pt" style="text-align: right" Text="Valor Comercial" Width="100px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtVlrComer" runat="server" AutoPostBack="True" 
                                                        Font-Names="Arial" Font-Size="8pt" MaxLength="20" 
                                                        ontextchanged="txtVlrComer_TextChanged" style="text-align: right" 
                                                        TabIndex="19" Width="165px">0</asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td >
                                                    <asp:Label ID="lblDescuento" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        style="text-align: left" Text="% De Descuento" Width="100px"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                   <asp:TextBox ID="porcDscto" runat="server" AutoPostBack="True" 
                                                        Font-Names="Arial" Font-Size="8pt" MaxLength="20" 
                                                        ontextchanged="porcDscto_TextChanged" style="text-align: right" 
                                                        TabIndex="19" Width="165px">0</asp:TextBox>
                                                    &nbsp;<asp:Label ID="lblPorc" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        Text="%" Width="20px"></asp:Label>
                                                </td>
                                                <td >
                                                    <asp:Label ID="lvdscto" runat="server" Font-Names="Arial" 
                                                        Font-Size="8pt" style="text-align: right" Text="Valor Descuento" Width="120px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblValorDscto" runat="server" Font-Bold="False" 
                                                        Font-Italic="false" Font-Names="Arial" Font-Size="8pt" ForeColor="#0000CC" 
                                                        style="text-align: right" Text="0" Width="132px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td >
                                                    <asp:Label ID="Label21" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        style="text-align: right" Text="Razón Descuento" Width="95px"></asp:Label>
                                                </td>
                                                <td colspan="3" style="text-align: justify">
                                                    <asp:TextBox ID="txtRazonDescto" runat="server" Font-Names="Arial" 
                                                        Font-Size="8pt" Height="42px" style="margin-left: 0px" TabIndex="21" 
                                                        TextMode="MultiLine" Width="518px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        <table>

                                    </asp:Panel>

                                </td>

                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlVenta" runat="server"  Font-Names="Arial" 
                                        Font-Size="8pt" GroupingText="Valores De Venta" Width="700px">
                                        <table class="style4">
                                            <tr>
                                                <td class="style10" style="text-align: right">
                                                    <asp:Label ID="lblVrComercial" runat="server" Font-Names="Arial" 
                                                        Font-Size="8pt" style="text-align: right" Text="Valor EXW de Cierre" Width="100px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtValorComercial" runat="server" AutoPostBack="True" 
                                                        Font-Names="Arial" Font-Size="8pt" MaxLength="20" 
                                                        ontextchanged="txtValorComercial_TextChanged" style="text-align: right" 
                                                        TabIndex="19" Width="165px">0</asp:TextBox>
                                                </td>
                                                <td class="style5" style="text-align: right"  >
                                                </td>
                                                <td class="style9"  >
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style5" style="text-align: right">
                                                 <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        style="text-align: right" Text="Tipo Flete*" Width="100px"></asp:Label>
                                                </td>
                                                <td>
                                                 <asp:DropDownList ID="cboTipoFlete" runat="server" AutoPostBack="True" 
                                                        Font-Names="Arial" Font-Size="8pt" Width="170px" 
                                                        OnSelectedIndexChanged="cboTipoFlete_SelectedIndexChanged" >
                                                    </asp:DropDownList>     
                                                </td>
                                                <td class="style10" style="text-align: right">
                                                        <asp:Label ID="lSubt" runat="server" Font-Bold="false" 
                                                        Font-Italic="False" Font-Names="Arial" Font-Size="8pt" 
                                                        style="text-align: right" Text="Subtotal" Width="100px"></asp:Label>                                            
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSubtotal" runat="server" Font-Italic="False" 
                                                        Font-Names="Arial"  Font-Size="8pt" 
                                                        ForeColor="#0000CC" Height="16px" style="text-align: right" Text="0" 
                                                        Width="170px"></asp:Label>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td class="style5" style="text-align: right">
                                                  <asp:Label ID="Label5" runat="server" Font-Bold="false" 
                                                        Font-Italic="False" Font-Names="Arial" Font-Size="8pt" 
                                                        style="text-align: right" Text="Modo Facturar Flete" Width="100px"></asp:Label>
                                                
                                                      </td>
                                               
                                                <td class="style5" style="text-align: right">
                                                    <asp:DropDownList ID="cboModoFactFlete" runat="server" AutoPostBack="True" 
                                                        Font-Names="Arial" Font-Size="8pt" Width="170px">
                                                    </asp:DropDownList>   
                                                </td>
                                                <td class="style10" style="text-align: right">
                                                   <asp:Label ID="lbliv" runat="server" Font-Italic="False" Font-Names="Arial" 
                                                        Font-Size="8pt" style="text-align: right" Text="IVA" Width="30px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblIVA" runat="server" Font-Italic="False" Font-Names="Arial" 
                                                        Font-Size="8pt" ForeColor="#0000CC" style="text-align: right" Text="0" 
                                                        Width="170px"></asp:Label>
                                                   </td>
                                            </tr>
                                            <tr>
                                                <td class="style5" style="text-align: right">
                                                  <asp:Label ID="lblFlete" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        style="text-align: right" Text="Valor Flete Inc. IVA" Width="100px"></asp:Label>    
                                                </td>
                                                <td>
                                                  <asp:TextBox ID="txtValorflete" runat="server" AutoPostBack="True" 
                                                        Font-Names="Arial" Font-Size="8pt" MaxLength="20" Text ="0" 
                                                        ontextchanged="txtValorflete_TextChanged" style="text-align: right" 
                                                        TabIndex="22" Width="165px" Enabled= "false"></asp:TextBox>   
                                                </td>
                                                <td class="style10" style="text-align: right">
                                                 <asp:Label ID="lblVrTotalVenta" runat="server" Font-Bold="True" 
                                                        Font-Italic="False" Font-Names="Arial" Font-Size="8pt" 
                                                        style="text-align: right" Text="Valor Total Final" Width="100px"></asp:Label>
                                                
                                                   </td>
                                                    
                                                <td>
                                                   <asp:Label ID="lblValorTotalVenta" runat="server" Font-Italic="False" 
                                                        Font-Names="Arial"  Font-Size="10pt"  Font-Bold ="true"
                                                        ForeColor="#0000CC" Height="16px" style="text-align: right" Text="0" 
                                                        Width="170px"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style8" style="text-align: right">
                                               
                                                
                                                    &nbsp;</td>
                                                <td class="style9">
                                                
                                                    &nbsp;</td>
                                                <td class="style10" style="text-align: right">
                                                  
                                                     
                                                </td>
                                                <td>
                                                   
                                               
                                                </td>
                                            </tr>
                                                                                      
                                            <tr>
                                                <td class="style5" style="text-align: right">
                                                    <asp:Label ID="lblComentarios" runat="server" Font-Names="Arial" visible ="false" 
                                                        Font-Size="8pt" style="text-align: right" Text="Comentarios Generales" Width="70px"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtComentariosSF" runat="server" Font-Names="Arial" visible ="false"
                                                        Font-Size="8pt" Height="70px" TabIndex="24" TextMode="MultiLine" Width="510px"></asp:TextBox>
                                                </td>
                                                
                                            </tr>

                                            r>
                                                <td class="style5" style="text-align: right">
                                                    <asp:Label ID="Label14" runat="server" Font-Names="Arial" 
                                                        Font-Size="8pt" style="text-align: right" Text="Observaciones en Factura" Width="70px"></asp:Label>
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtObservaFactura" runat="server" Font-Names="Arial" 
                                                        Font-Size="8pt" Height="70px" TabIndex="24" TextMode="MultiLine" Width="510px"></asp:TextBox>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    &nbsp;</td>
                                                <td colspan="3">
                                                    <table class="style4" style="text-align: right">
                                                    <tr>
                                                       <td style="text-align: right">   &nbsp;</td> 
                                                    </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="btnGuardarsf" runat="server" BackColor="#1C5AB6" 
                                                                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                                                     onclick="btnGuardarsf_Click" ForeColor="White"
                                                                    onclientclick="return confirm('Esta seguro de guardar la SF?')" TabIndex="25" 
                                                                    Text="Guardar" Width="70px" Visible="False" />
                                                            </td>
                                                            <td> </td>
                                                            <td>
                                                                <asp:Button ID="btnconfsf" runat="server" BackColor="#1C5AB6" 
                                                                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                                                     onclick="btnconfsf_Click" ForeColor="White"
                                                                    onclientclick="return confirm('Esta seguro de confirmar la SF?')" 
                                                                    Text="Confirmar SF" Visible="False" Width="90px" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnQuitarConfirmacion" runat="server" BackColor="#1C5AB6" 
                                                                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                                                     onclick="btnQuitarConfirmacion_Click" ForeColor="White"
                                                                    onclientclick="return confirm('Esta seguro de quitar la confirmarción de la SF?')" 
                                                                    Text="Quitar Confirmacion SF" Width="150px"  Visible="False" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnGenerarSF" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                                                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                                                     OnClientClick="NavigateCartaPv()" Text="Imprimir SF" Width="80px"  />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnApruebaFinanc" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                                                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                                                     onclick="btnApruebaFinanc_Click" Text="Aprueba Financiero" Width="120px" Visible= "False" />
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnprocesarSF" runat="server" BackColor="#1C5AB6" 
                                                                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                                                     onclick="btnprocesarSF_Click" ForeColor="White"
                                                                    onclientclick="return confirm('Esta seguro de Procesar la SF?')" 
                                                                    Text="Procesar SF" Width="90px" />                                                                
                                                            </td>
                                                        </tr>
                                                        
                                                    </table>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td> </td>
                                            </tr>
                                            <tr>
                                                <td class="style8" style="text-align: right">
                                                  <asp:UpdateProgress ID="UpdateProgress3" runat="server" 
                                                        AssociatedUpdatePanelID="UpdatePanel1">
                                                      <ProgressTemplate>
                                                       &nbsp;<table class="style44">
                                                            <tr>
                                                                <td>
                                                                    &nbsp;</td>
                                                                <td>
                                                                    <img alt="" src="Imagenes/Indicator.gif" height="20" style="text-align: center; float: right;" width="20"/>  
                                                                </td>
                                                                <td>
                     
                                                                    &nbsp;</td>
                                                                <td>
                                                                    &nbsp;</td>
                                                            </tr>
                                                        </table>

                                                    </ProgressTemplate>
 
                                                    </asp:UpdateProgress>

                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                                <tr>
                                <td style="text-align: center">
                                  
                                </td>
                            </tr>
                         
                            <tr>
                                <td>
                                    <asp:GridView ID="grvPartes" runat="server" AutoGenerateColumns="False" 
                                        CellPadding="1" CellSpacing="4" Font-Names="Arial" Font-Size="8pt" 
                                        ForeColor="#333333" GridLines="None" Width="669px">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="SF Num" ItemStyle-HorizontalAlign="Center" 
                                            Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="LSFNum" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        Text='<%# Bind("Sf_num") %>' Width="40px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Planta" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="LPlanta" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        Text='<%# Bind("planta") %>' Width="40px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Parte" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="LParte" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        Text='<%# Bind("Sf_parte") %>' Width="40px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="M2" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="LM2" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        Text='<%# Bind("sf_m2") %>' Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vr Venta" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="LVenta" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        Text='<%# Bind("Sf_vlr_venta") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vr Comercial" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="LCome" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        Text='<%# Bind("Sf_vlr_comercial") %>' Width="100px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="% Dcto" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="LDcto" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        Text='<%# Bind("Sf_porcdesc") %>' Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vr Dcto" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="LVrDcto" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        Text='<%# Bind("Sf_vlr_dscto") %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vr Flete" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="LFlete" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        Text='<%# Bind("Sf_transporte") %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vr IVA" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="LValorIVA" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        Text='<%# Bind("Sf_iva") %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vr Total " ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="LTotalVta" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        Text='<%# Bind("Sf_tltiva") %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Tipo SF " ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="LTotalVta" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        Text='<%# Bind("TipoSf") %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True"  />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True"  />
                                        <PagerStyle BackColor="#2461BF"  HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>
                                </td>
                            </tr>
                           <tr>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                                    </Content>
                                </asp:AccordionPane>
                                <asp:AccordionPane ID="AcorCuotas" runat="server"
                                    ContentCssClass="accordionContent" Font-Bold="False" Font-Names="Arial" 
                                    Font-Size="8pt" HeaderCssClass="accordionHeader" 
                                    HeaderSelectedCssClass="accordionHeaderSelected" Visible = "false">
                                    <Header>
                                        <asp:Label ID="LCuotas" runat="server" Text="Cuotas Proyecto General"></asp:Label>
                                    </Header>
                                    <Content>
                                    <table class="style4">
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblCuota" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right" Text="Cuota" Width="50px"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cboCuota" runat="server" AutoPostBack="True" 
                                        Font-Names="Arial" Font-Size="8pt" Width="80px" 
                                        OnSelectedIndexChanged="cboCuota_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblpagar" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right; margin-bottom: 0px;" Text="A Pagar" Width="50px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtaPagar" runat="server" AutoPostBack="True" 
                                        Font-Names="Arial" Font-Size="8pt" MaxLength="14" 
                                        ontextchanged="txtaPagar_TextChanged" Width="90px">0</asp:TextBox>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblporcpagar" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right; margin-bottom: 0px;" Text="% A Pagar" Width="60px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtporcpagar" runat="server" AutoPostBack="True" 
                                        Font-Names="Arial" Font-Size="8pt" ontextchanged="txtporcpagar_TextChanged" 
                                        style="text-align: center" Width="30px">0</asp:TextBox>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblpagado" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right; margin-bottom: 0px;" Text="Pagado" Visible="False" 
                                        Width="50px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtpagado" runat="server" AutoPostBack="True" Enabled="False" 
                                        Font-Names="Arial" Font-Size="8pt" MaxLength="14" 
                                        Visible="False" Width="90px">0</asp:TextBox>
                                </td>
                                <td style="text-align: right">
                                    <asp:Label ID="lblfechareal" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right; margin-bottom: 0px;" Text="Fecha Real" Width="80px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFechaReal" runat="server" AutoPostBack="True" 
                                        Font-Names="Arial" Font-Size="8pt" ontextchanged="txtFechaDes_TextChanged" 
                                        style="text-align: right" TabIndex="13" Width="80px"></asp:TextBox>
                                    <asp:MaskedEditExtender ID="txtFechaReal_MaskedEditExtender" runat="server" 
                                        CultureAMPMPlaceholder="" CultureCurrencySymbolPlaceholder="" 
                                        CultureDateFormat="" CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                        CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True" 
                                        Mask="99/99/9999" MaskType="Date" TargetControlID="txtFechaReal">
                                    </asp:MaskedEditExtender>
                                    <asp:CalendarExtender ID="txtFechaReal_CalendarExtender" runat="server" 
                                        Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaReal">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:Label ID="lblcomentario0" runat="server" Font-Names="Arial" 
                                        Font-Size="8pt" style="text-align: right; margin-bottom: 0px;" 
                                        Text="Comentarios" Width="80px"></asp:Label>
                                </td>
                                <td colspan="9">
                                    <asp:TextBox ID="txtComentCuota" runat="server" Font-Names="Arial" 
                                        Font-Size="8pt" Height="30px" TextMode="MultiLine" Width="730px">Sin Comentarios</asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td style="text-align: right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td style="text-align: right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td style="text-align: right">
                                    <asp:Label ID="lsaldo0" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="text-align: right; margin-bottom: 0px;" Text="Saldo" Width="50px"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:Label ID="lblsaldocuota" runat="server" Font-Italic="False" 
                                        Font-Names="Arial" Font-Size="8pt" ForeColor="#0000CC" 
                                        style="text-align: center" Text="0" Width="300px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td style="text-align: right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td style="text-align: right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td style="text-align: right">
                                    &nbsp;</td>
                                <td colspan="3">
                                    <table class="style4">
                                        <tr>
                                            <td>
                                             <asp:Button ID="btnGuardarCuota" runat="server" BackColor="#1C5AB6" 
                                                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                                     onclick="btnGuardarCuota_Click" ForeColor="White"
                                                    onclientclick="return confirm('Esta seguro de guardar la cuota?')" 
                                                    Text="Guardar" Width="70px" />                                               
                                            </td>
                                            <td>
                                             <asp:Button ID="btnGenerarCuota" runat="server" BackColor="#1C5AB6" 
                                                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                                     onclick="btnGenerarCuota_Click" ForeColor="White"
                                                    onclientclick="return confirm('Esta seguro de generar la cuota?')" 
                                                    Text="Generar Cuota" Width="100px" />
                                               
                                            </td>
                                            <td>
                                                <asp:Button ID="btnEliminarCuota" runat="server" BackColor="#1C5AB6" 
                                                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                                     onclick="btnEliminarCuota_Click" ForeColor="White"
                                                    onclientclick="return confirm('Esta seguro de eliminar la cuota?')" 
                                                    Text="Eliminar" Width="70px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="10" style="text-align: left">
                                    <asp:GridView ID="grvCuota" runat="server" CellPadding="1" CellSpacing="4" 
                                        Font-Names="Arial" Font-Size="8pt" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True"  />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True"  />
                                        <PagerStyle BackColor="#2461BF"  HorizontalAlign="Center" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                                    </Content>
                                </asp:AccordionPane>
                              <asp:AccordionPane ID="AcorSF" runat="server" Visible ="false"
                                    ContentCssClass="accordionContent" Font-Bold="False" Font-Names="Arial" 
                                    Font-Size="8pt" HeaderCssClass="accordionHeader" 
                                    HeaderSelectedCssClass="accordionHeaderSelected">
                                    <Header>
                                        <asp:Label ID="LSF" runat="server" Text="Formato Solicitud de Facturacion"></asp:Label>
                                    </Header>
                                    <Content>
                                        <rsweb:ReportViewer ID="rpSF" runat="server" Width="800px" Height="600px" Visible = "false" ShowParameterPrompts ="false" >
                                        
                                        </rsweb:ReportViewer>
                                    </Content>
                                </asp:AccordionPane>
                        </Panes>
                        </asp:Accordion>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" 
                            Text=" " Visible="False" Width="39px" Height="16px"></asp:Label>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
</asp:Content>
