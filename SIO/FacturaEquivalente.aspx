<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="FacturaEquivalente.aspx.cs" Inherits="SIO.FacturaEquivalente1" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" 
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style type="text/css">
        .watermarked 
        {
            padding:2px 0 0 2px;
            border:1px solid #BEBEBE;
            background-color:white;
            color:Gray;
            font-family:Arial;
            font-weight:lighter;    
        }
        .derecha
        {
            font-family: Arial;
            font-size: 8pt;
            Text-Align:Right; 
        }
        .centrar
        {
            font-family: Arial;
            font-size: 8pt;
            Text-Align:Center; 
        }
        .CustomComboBoxStyle .ajax__combobox_buttoncontainer button 
        {
            background-image:url('http://172.21.0.1/SIOMaestros/Imagenes/toolkit-arrow.gif');
            border-style:none;            
        }
        .CustomComboBoxStyle .ajax__combobox_textboxcontainer input 
        {
            background-image:url('http://172.21.0.1/SIOMaestros/Imagenes/toolkit-bg.gif');
            border-style:none;  
        }
        .CustomComboBoxStyle .ajax__combobox_itemlist li
        {
            color: Black;  
            font-size:8pt;  
            font-family:Arial; 
            background-color:#EBEBEB
        }
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
        .style73
    {
        width: 281px;
    }
    .style74
    {
        width: 100%;
    }
    .style75
    {
        width: 523px;
    }
    .style9
    {
        height: 93px;
    }
    .CustomComboBoxStyle
    {
        text-align: center;
    }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="style9">
            <tr>
                <td class="style13">
                    <asp:Panel ID="pnlCliente" runat="server" BackColor="White" 
                    GroupingText="Cliente" Font-Names="Arial" Font-Size="8pt" Height="120px" 
                    Width="800px">
                    <table class="style9">
                        <tr>
                            <td class="style10">
                                <asp:Label ID="lblForsa" runat="server" Text="Forsa" Width="60px" 
                                    Font-Names="Arial" Font-Size="8pt"></asp:Label>
                            </td>
                             <td class="style11">
                                 <asp:ComboBox ID="cboForsa" runat="server"  
                                     DropDownStyle="DropDownList" Font-Names="Arial" Font-Size="8pt" 
                                      Width="300px">
                                     <asp:ListItem>Elija</asp:ListItem>
                                     <asp:ListItem>Arrendadora Bogota</asp:ListItem>
                                     <asp:ListItem>Arrendadora Cali</asp:ListItem>
                                     <asp:ListItem>Forsa Caloto</asp:ListItem>
                                 </asp:ComboBox>
                            </td>
                             <td class="style42">
                                <asp:Label ID="lblNit" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                    Text="NIT" Width="40px"></asp:Label>
                                &nbsp;</td>
                            <td>
                                <asp:TextBox ID="txtNIT" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                    Width="200px"></asp:TextBox>
                            </td>
                         </tr>
                          <tr>
                            <td class="style10">
                                <asp:Label ID="lblPais" runat="server" Text="Pais" Width="60px" 
                                    Font-Names="Arial" Font-Size="8pt"></asp:Label>
                            </td>
                             <td class="style11">
                             <asp:ComboBox ID="cboPais" runat="server"  
                                    Font-Names="Arial" Font-Size="8pt" Width="300px" AutoPostBack="True" 
                                     DropDownStyle="DropDownList"  
                                     onselectedindexchanged="cboPais_SelectedIndexChanged">
                             </asp:ComboBox>
                             </td>
                              <td class="style42" rowspan="2">
                                <asp:Label ID="lblDireccion" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                    Text="Direccion" Width="60px"></asp:Label>
                            </td>
                            <td rowspan="2">
                                <asp:TextBox ID="txtDireccion" runat="server" Font-Names="Arial" 
                                    Font-Size="8pt" Height="30px" TextMode="MultiLine" Width="300px"></asp:TextBox>
                            </td>
                            </tr>
                            <tr>
                            <td class="style10">
                                <asp:Label ID="lblCiudad" runat="server" Text="Ciudad" Width="60px" 
                                    Font-Names="Arial" Font-Size="8pt"></asp:Label>
                            </td>
                            <td class="style11">
                                <asp:ComboBox ID="cboCiudad" runat="server"  
                                    Font-Names="Arial" Font-Size="8pt"  Width="300px">
                                </asp:ComboBox>
                            </td>
                            </tr>
                            <tr>
                            <td class="style22">
                                <asp:Label ID="lblRazonSocial" runat="server" Text="Nombre" Width="80px"></asp:Label>
                            </td>
                            <td class="style23">
                                <asp:TextBox ID="txtRazonSocial" runat="server" Font-Names="Arial" 
                                    Font-Size="8pt" Width="315px"></asp:TextBox>
                            </td>
                            <td class="style41">
                                <asp:Label ID="lblTelefono" runat="server" Text="Teléfono" Width="60px"></asp:Label>
                            </td>
                            <td class="style25">
                                <asp:TextBox ID="txtTelefono" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                    Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>                        
                    </asp:Panel>
                    </td>
                    <td>
                    <asp:Panel ID="pnlFacturaEquivalente" runat="server" BackColor="White" 
                    Font-Bold="False" Font-Names="Arial" Font-Size="10pt" 
                    GroupingText="DOCUMENTO EQUIVALENTE" Height="120px" Width="236px">
                        <table class="style14">
                        <tr>
                            <td class="style71">
                                <asp:Label ID="lblFecha" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                    Text="Fecha *" Width="40px"></asp:Label>
                            </td>
                            <td class="style16">
                                <asp:TextBox ID="txtFec" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                    Width="60px"></asp:TextBox>
                                <asp:CalendarExtender ID="txtFec_CalendarExtender" runat="server" 
                                    Enabled="True" TargetControlID="txtFec" Format="yyyy/MM/dd">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td class="style71">
                                <asp:Label ID="lblAno" runat="server" Font-Names="Arial" Font-Size="10pt" 
                                    Width="40px"></asp:Label>
                            </td>
                            <td class="style16">
                                <asp:TextBox ID="txtConsec" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                    Width="60px"></asp:TextBox>
                                <asp:TextBoxWatermarkExtender ID="txtConsec_TextBoxWatermarkExtender" 
                                    runat="server" Enabled="True" TargetControlID="txtConsec" WatermarkCssClass="watermarked" 
                                          WatermarkText="Factura No">
                                </asp:TextBoxWatermarkExtender>
                            </td>
                        </tr>
                        <tr>
                            <td class="style17">
                                <asp:Label ID="lblTipoImpuesto" runat="server" Font-Names="Arial" 
                                    Font-Size="8pt" Height="16px" Text="Impuesto" Width="70px"></asp:Label>
                            </td>
                            <td class="style18">
                                <p style="text-indent: 1px">
                                    <asp:ComboBox ID="cboImpuesto" runat="server" AutoPostBack="True" 
                                         Font-Names="Arial" Font-Size="8pt" 
                                         Width="30px">
                                    </asp:ComboBox>
                                    <asp:Label ID="lblPorc" runat="server" CssClass="centrar" Font-Names="Arial" 
                                        Font-Size="8pt" Text="%"></asp:Label>
                                </p>
                            </td>
                        </tr>
                    </table>
                    </asp:Panel>
                    </td>
            </tr>
            </table>
            <tr>
            <td class="style19" colspan="2">
                <asp:Panel ID="pnlDetalleFactura" runat="server" BackColor="White" 
                    Font-Names="Arial" Font-Size="8pt" GroupingText="Detalle De Facturación" 
                    Width="1050px" Height="260px">
                    <table class="style26">
                        <tr>
                            <td class="style51" colspan="6">
                                <table cellspacing="1" class="style43" 
                                    style="border-style: none; border-width: 0px">
                                    <tr>
                                        <td class="style44" style="border: 1px solid #808080; text-align: center;">
                                            <asp:Label ID="lblCant" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                style="border-style: none" Text="Cantidad *" Width="50px"></asp:Label>
                                        </td>
                                        <td class="style61" style="border: 1px solid #808080;">
                                            <asp:Label ID="lblDescrip" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                Text="Descripción" Width="440px"></asp:Label>
                                        </td>
                                        <td class="style52" style="border: 1px solid #808080;">
                                            <asp:Label ID="lblPrecio" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                Text="Precio Unitario" Width="100px"></asp:Label>
                                        </td>
                                        <td class="style49" style="border: 1px solid #808080;">
                                            <asp:Label ID="lblTotalUnitario" runat="server" Font-Names="Arial" 
                                                Font-Size="8pt" Text="Total Unitario" Width="100px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style44" style="border: 1px solid #808080; text-align: center;">
                                            <asp:TextBox ID="txtCant" runat="server" BorderStyle="None" Font-Names="Arial" 
                                                Font-Size="8pt" style="text-align: center" Width="40px"></asp:TextBox>
                                        </td>
                                        <td class="style61" style="border: 1px solid #808080;">
                                            <asp:TextBox ID="txtDescripcion" runat="server" BorderStyle="None" 
                                                Font-Names="Arial" Font-Size="8pt" Height="30px" TextMode="MultiLine" 
                                                Width="443px"></asp:TextBox>
                                        </td>
                                        <td class="style52" style="border: 1px solid #808080;">
                                            <asp:TextBox ID="txtPrecioUnitario" runat="server" BorderStyle="None" 
                                                Font-Names="Arial" Font-Size="8pt" Width="100px" AutoPostBack="True" 
                                                ontextchanged="txtPrecioUnitario_TextChanged"></asp:TextBox>
                                        </td>
                                        <td class="style49" style="border: 1px solid #808080; margin-left: 40px;">
                                            <asp:TextBox ID="txtTotalUni" runat="server" BorderStyle="None" 
                                                Font-Names="Arial" Font-Size="8pt" Width="100px" Enabled="False" 
                                                Font-Bold="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style57" style="border-style: none; border-width: 1px">
                                        </td>
                                        <td class="style58" style="border-style: none; border-width: 1px">
                                            &nbsp;</td>
                                        <td class="style59" style="border-style: none; border-width: 1px">
                                        </td>
                                        <td class="style60" style="border-style: none; border-width: 1px">
                                            <asp:Button ID="btnAdicionar" runat="server" BackColor="#1C5AB6" 
                                                BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                                 onclientclick="return confirm('Desea Adicionar El Item')" 
                                                Text="Adicionar" Width="70px" onclick="btnAdicionar_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="style51">
                                <asp:Panel ID="pnlCondiciones" runat="server" BackColor="White" 
                                    Font-Names="Arial" GroupingText="Condiciones " Width="300px">
                                    <table class="style62">
                                        <tr>
                                            <td class="style70">
                                                <asp:Label ID="lblSubTotal" runat="server" Text="Subtotal" 
                                                    Width="120px"></asp:Label>
                                            </td>
                                            <td class="style69">
                                                <asp:TextBox ID="txtSubpartida" runat="server" Font-Names="Arial" 
                                                    Font-Size="8pt" Width="120px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style70">
                                                <asp:Label ID="lblExpedicion" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    Text="Expedición Y Tramitación" Width="130px"></asp:Label>
                                            </td>
                                            <td class="style69">
                                                <asp:TextBox ID="txtExp" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    Width="120px">0</asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style70">
                                                <asp:Label ID="lblRetencion" runat="server" Font-Names="Arial" 
                                                    Font-Size="8pt" Text="Retención IVA" Width="130px"></asp:Label>
                                            </td>
                                            <td class="style69">
                                                <asp:TextBox ID="txtValor" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    Width="120px">0</asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style70">
                                                <asp:Label ID="lblTotal" runat="server" Text="TOTAL" Width="130px"></asp:Label>
                                            </td>
                                            <td class="style69">
                                                <asp:Label ID="lblTotalFin" runat="server" BorderStyle="Solid" 
                                                    BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Overline="False" 
                                                    Font-Size="8pt" Font-Underline="False" Text="0" Width="130px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                </td>
                        </tr>
                        <tr>
                            <td class="style10" colspan="7">
                                <table class="style74">
                                    <tr>
                                        <td class="style75">
                                            <asp:Panel ID="pnlNotas" runat="server" BackColor="White" Font-Names="Arial" 
                                                Font-Size="8pt" GroupingText="Notas" Width="511px">
                                                <asp:TextBox ID="txtNotas" runat="server" BorderStyle="None" Font-Names="Arial" 
                                                    Font-Size="8pt" Height="30px" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                            </asp:Panel>
                                        </td>
                                        <td>
                                            <asp:Panel ID="pnlPrecio" runat="server" BackColor="White" Font-Names="Arial" 
                                                Font-Size="8pt" GroupingText="Total En Letras" style="text-align: left" 
                                                Width="506px" Wrap="False">
                                                &nbsp;<asp:TextBox ID="txtPrecio" runat="server" BorderStyle="None" 
                                                    Font-Names="Arial" Font-Size="8pt" TextMode="MultiLine" Width="480px"></asp:TextBox>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="style65">
                                </td>
                            <td class="style73">
                                </td>
                            <td class="style65">
                                </td>
                            <td class="style37">
                                </td>
                            <td class="style33">
                                </td>
                            <td class="style55">
                                </td>
                            <td class="style66">
                                <asp:Button ID="btnGuardar" runat="server" BackColor="#1C5AB6" 
                                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                     Text="Finalizar Factura" Width="120px" 
                                    onclick="btnGuardar_Click" CssClass="centrar" 
                                    onclientclick="return confirm('Esta Seguro De Finalizar La Factura')" />
                                &nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnNuevo" runat="server" BackColor="#1C5AB6" 
                                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                     Text="Nuevo" Width="70px" onclick="btnNuevo_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                </td>
                </tr>
                  <tr>
            <td class="style36" colspan="2">
                <rsweb:ReportViewer ID="ReportDetalle" runat="server" ShowToolBar="False">

                </rsweb:ReportViewer>
            </td>
        </tr>
        <tr>
            <td class="style13">                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style13">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        
        </ContentTemplate>
        
    </asp:UpdatePanel>   
</asp:Content>
