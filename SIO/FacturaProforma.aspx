<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="FacturaProforma.aspx.cs" Inherits="SIO.FacturaProforma" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"  namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
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
        .style9
        {
            width: 82%;
            height: 100px;
        }
        .style10
        {
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
        .style11
        {
            width: 246px;
        }
        .style13
        {
        }
        .style22
        {
            width: 87px;
            height: 24px;
        }
        .style23
        {
            width: 246px;
            height: 24px;
        }
        .style25
        {
            height: 24px;
        }
        .style26
        {
            width: 101%;
            height: 2px;
        }
        .style33
        {
            width: 46px;
            height: 29px;
        }
        .style36
        {
            height: 23px;
        }
        .style37
        {
            width: 112px;
            height: 29px;
        }
        .style38
        {
            width: 310px;
            height: 29px;
        }
        .style41
        {
            width: 62px;
            height: 24px;
        }
        .style42
        {
            width: 62px;
        }
        .style43
        {
            width: 50%;
            border-collapse: collapse;
            border-style: solid;
            border-width: 1px;
        }
        .style44
        {
            width: 53px;
        }
        .style49
        {
            width: 81px;
        }
        .style52
        {
            width: 103px;
        }
        .style57
        {
            width: 53px;
            height: 25px;
        }
        .style58
        {
            width: 394px;
            height: 25px;
        }
        .style59
        {
            width: 103px;
            height: 25px;
        }
        .style60
        {
            font-family: Arial;
            font-size: 8pt;
            Text-Align: Right;
            height: 25px;
        }
        .style61
        {
            width: 398px;
        }
        .style62
        {
            width: 98%;
        }
        .style64
        {
        }
        .style65
        {
            height: 29px;
        }
        .style66
        {
            font-family: Arial;
            font-size: 8pt;
            Text-Align: Center;
            height: 29px;
        }
        .style69
        {
            width: 150px;
        }
        .style70
        {
            width: 166px;
        }
        .style74
        {
            width: 534px;
        }
        .style14
        {
            width: 100%;
            height: 100px;
        }
        .style71
        {
            width: 43px;
        }
        .style16
        {
            width: 73px;
        }
        .style17
        {
            width: 43px;
            height: 20px;
        }
        .style18
        {
            width: 73px;
            height: 20px;
        }
        .style75
        {
            height: 95px;
        }
        .style76
        {
            height: 206px;
        }
        .style77
        {
            width: 394px;
        }
        .style78
        {
            width: 1px;
            height: 29px;
        }
        .style79
        {
            width: 30px;
            height: 29px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
             <table class="style9">
        <tr>
            <td class="style74">
                <asp:Panel ID="pnlCliente" runat="server" BackColor="White" 
                    GroupingText="Cliente" Font-Names="Arial" Font-Size="8pt" Height="151px" 
                    Width="1040px">
                    <table class="style9">
                        <tr>
                            <td class="style10">
                                <asp:Label ID="lblPais" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                    Text="Pais" Width="60px"></asp:Label>
                            </td>
                            <td class="style11">
                                <asp:ComboBox ID="cboPais" runat="server" AutoPostBack="True" 
                                    DropDownStyle="DropDownList"  
                                    Font-Names="Arial" Font-Size="8pt" Width="300px"  
                                    onselectedindexchanged="cboPais_SelectedIndexChanged">
                                </asp:ComboBox>
                            </td>
                            <td class="style42">
                                <asp:Label ID="lblNit" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                    Text="NIT" Width="40px"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNIT" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                    Width="200px"></asp:TextBox>
                            </td>
                            <td rowspan="4">
                                <asp:Panel ID="pnlFacturaProforma" runat="server" BackColor="White" 
                                    Font-Bold="False" Font-Names="Arial" Font-Size="10pt" 
                                    GroupingText="FACTURA PROFORMA" Height="120px" Width="236px">
                                    <table class="style14">
                                        <tr>
                                            <td class="style71">
                                                <asp:Label ID="lblFecha" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    Text="Fecha" Width="40px"></asp:Label>
                                            </td>
                                            <td class="style16">
                                                <asp:TextBox ID="txtFec" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    Width="60px"></asp:TextBox>
                                                <asp:CalendarExtender ID="txtFec_CalendarExtender" runat="server" 
                                                    Enabled="True" TargetControlID="txtFec">
                                                </asp:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style71">
                                                <asp:Label ID="lblAno" runat="server" CssClass="centrar" Font-Names="Arial" 
                                                    Font-Size="10pt" Width="40px"></asp:Label>
                                            </td>
                                            <td class="style16">
                                                <asp:TextBox ID="txtConsec" runat="server" AutoPostBack="True" 
                                                    CssClass="centrar" Font-Names="Arial" Font-Size="8pt" 
                                                    ontextchanged="txtConsec_TextChanged" Width="60px"></asp:TextBox>
                                                <asp:TextBoxWatermarkExtender ID="txtConsec_TextBoxWatermarkExtender" 
                                                    runat="server" Enabled="True" TargetControlID="txtConsec" 
                                                    WatermarkCssClass="watermarked" WatermarkText="Factura No">
                                                </asp:TextBoxWatermarkExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style17">
                                            </td>
                                            <td class="style18">
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="style10">
                                <asp:Label ID="lblCiudad" runat="server" Text="Ciudad" Width="60px"></asp:Label>
                            </td>
                            <td class="style11">
                                <asp:ComboBox ID="cboCiudad" runat="server"  
                                    Font-Names="Arial" Font-Size="8pt" Width="300px"  
                                    AutoPostBack="True" 
                                    onselectedindexchanged="cboCiudad_SelectedIndexChanged" 
                                    DropDownStyle="DropDownList">
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
                                <asp:Label ID="lblRazonSocial" runat="server" Text="Razón Social" Width="80px"></asp:Label>
                            </td>
                            <td class="style11">
                                <asp:ComboBox ID="cboCliente" runat="server" AutoPostBack="True" 
                                     Font-Names="Arial" 
                                    Font-Size="8pt" Width="300px"  
                                    onselectedindexchanged="cboCliente_SelectedIndexChanged" 
                                    DropDownStyle="DropDownList">
                                </asp:ComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="style22">
                                <asp:Label ID="lblObra" runat="server" Text="Obra" Width="60px"></asp:Label>
                            </td>
                            <td class="style23">
                                <asp:ComboBox ID="cboObra" runat="server" AutoPostBack="True" 
                                     DropDownStyle="DropDownList" Width="300px" 
                                    Font-Names="Arial" Font-Size="8pt" >
                                </asp:ComboBox>
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
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style76" colspan="2">
                <asp:Panel ID="pnlDetalleFactura" runat="server" BackColor="White" 
                    Font-Names="Arial" Font-Size="8pt" GroupingText="Detalle De Facturación" 
                    Width="1040px" Height="208px">
                    <table class="style26">
                        <tr>
                            <td class="style75" colspan="6">
                                <table cellspacing="1" class="style43" 
                                    style="border-style: none; border-width: 0px">
                                    <tr>
                                        <td class="style44" style="border: 1px solid #808080; text-align: center;">
                                            <asp:Label ID="lblCant" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                style="border-style: none" Text="Cantidad" Width="50px"></asp:Label>
                                        </td>
                                        <td class="style61" style="border: 1px solid #808080;">
                                            <asp:Label ID="lblDescrip" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                Text="Descripción" Width="425px"></asp:Label>
                                        </td>
                                        <td class="style52" style="border: 1px solid #808080;">
                                            <asp:Label ID="lblPrecio" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                Text="Precio Unitario" Width="110px"></asp:Label>
                                        </td>
                                        <td class="style49" style="border: 1px solid #808080;">
                                            <asp:Label ID="lblTotalUnitario" runat="server" Font-Names="Arial" 
                                                Font-Size="8pt" Text="Total Unitario" Width="110px"></asp:Label>
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
                                                Width="423px"></asp:TextBox>
                                        </td>
                                        <td class="style52" style="border: 1px solid #808080;">
                                            <asp:TextBox ID="txtPrecioUnitario" runat="server" BorderStyle="None" 
                                                Font-Names="Arial" Font-Size="8pt" Width="100px" AutoPostBack="True" 
                                                ontextchanged="txtPrecioUnitario_TextChanged"></asp:TextBox>
                                        </td>
                                        <td class="style49" style="border: 1px solid #808080; margin-left: 40px;">
                                            <asp:TextBox ID="txtTotalUni" runat="server" BorderStyle="None" 
                                                Font-Names="Arial" Font-Size="8pt" Width="100px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style57" style="border-style: none; border-width: 1px">
                                        </td>
                                        <td class="style58" style="border-style: none; border-width: 1px">
                                        </td>
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
                            <td class="style75">
                                <asp:Panel ID="pnlCondiciones" runat="server" BackColor="White" 
                                    Font-Names="Arial" GroupingText="Condiciones " Width="300px">
                                    <table class="style62">
                                        <tr>
                                            <td class="style70">
                                                <asp:Label ID="lblSubPartida" runat="server" Text="Sub Partida Arancelaria" 
                                                    Width="120px"></asp:Label>
                                            </td>
                                            <td class="style69">
                                                <asp:TextBox ID="txtSubpartida" runat="server" Font-Names="Arial" 
                                                    Font-Size="8pt" Width="120px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style70">
                                                <asp:Label ID="lblTDN" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    Text="TDN" Width="30px"></asp:Label>
                                                <asp:ComboBox ID="cboTDN" runat="server" AutoPostBack="True" 
                                                     Font-Names="Arial" Font-Size="8pt" 
                                                    Width="80px" >
                                                </asp:ComboBox>
                                            </td>
                                            <td class="style69">
                                                <asp:CheckBox ID="chkDesc" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    Text="Aplica Descuento" Width="120px" 
                                                    oncheckedchanged="chkDesc_CheckedChanged" AutoPostBack="True" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style70">
                                                <asp:Label ID="lblSubTotal" runat="server" Text="Subtotal" Width="120px"></asp:Label>
                                            </td>
                                            <td class="style69">
                                                <asp:TextBox ID="txtSubTotal" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    Width="120px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style70">
                                                <asp:Label ID="lblPorcDesc" runat="server" Text="Porcentaje De Descuento" 
                                                    Width="130px"></asp:Label>
                                            </td>
                                            <td class="style69">
                                                <asp:TextBox ID="txtPorc" runat="server" CssClass="centrar" Enabled="False" 
                                                    Font-Names="Arial" Font-Size="8pt" ontextchanged="txtPorc_TextChanged" 
                                                    Width="40px" AutoPostBack="True">0</asp:TextBox>
                                                &nbsp;<asp:Label ID="lblPorc" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    Text="%"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style70">
                                                <asp:Label ID="lblValorDescuento" runat="server" Font-Names="Arial" 
                                                    Font-Size="8pt" Text="Valor Con Descuento" Width="118px"></asp:Label>
                                            </td>
                                            <td class="style69">
                                                <asp:TextBox ID="txtValor" runat="server" Enabled="False" Font-Names="Arial" 
                                                    Font-Size="8pt" ontextchanged="txtValor_TextChanged" Width="120px" 
                                                    AutoPostBack="True">0</asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style70">
                                                <asp:Label ID="lblIva" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    Text="IVA"></asp:Label>
                                            </td>
                                            <td class="style69">
                                                <asp:TextBox ID="txtIVA" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    Width="120px">0</asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style70">
                                                <asp:Label ID="lblTotal" runat="server" Text="TOTAL" Width="80px"></asp:Label>
                                            </td>
                                            <td class="style69">
                                                <asp:Label ID="lblTotalFin" runat="server" BorderStyle="Solid" 
                                                    BorderWidth="1px" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Text="0" 
                                                    Width="120px"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                </td>
                        </tr>
                        <tr>
                            <td class="style10" colspan="2">
                                <asp:Panel ID="pnlNotas" runat="server" BackColor="White" Font-Names="Arial" 
                                    Font-Size="8pt" GroupingText="Notas">
                                    <asp:TextBox ID="txtNotas" runat="server" BorderStyle="None" Font-Names="Arial" 
                                        Font-Size="8pt" Height="30px" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                </asp:Panel>
                            </td>
                            <td>
                                </td>
                            <td class="style64" colspan="4">
                                <asp:Panel ID="pnlPrecio" runat="server" BackColor="White" Font-Names="Arial" 
                                    Font-Size="8pt" GroupingText="Precio De Un Equipo Completo" 
                                    style="text-align: left" Width="500px" Wrap="False">
                                    &nbsp;<asp:TextBox ID="txtPrecio" runat="server" BorderStyle="None" 
                                        Font-Names="Arial" Font-Size="8pt" TextMode="MultiLine" Width="490px"></asp:TextBox>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="style65">
                                </td>
                            <td class="style38">
                                </td>
                            <td class="style65">
                                </td>
                            <td class="style37">
                                </td>
                            <td class="style33">
                                </td>
                            <td class="style79">
                                </td>
                            <td class="style66">
                                <asp:Button ID="btnGuardar" runat="server" BackColor="#1C5AB6" 
                                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                     Text="Finalizar Factura" Width="120px" 
                                    CssClass="centrar" 
                                    onclientclick="return confirm('Esta Seguro De Finalizar La Factura')" 
                                    onclick="btnGuardar_Click" />
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
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style74">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style74">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
                 <tr>
                     <td class="style13" colspan="2">
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td class="style13" colspan="2">
                         <rsweb:ReportViewer ID="RFactura" runat="server" ShowToolBar="False">
                         </rsweb:ReportViewer>
                     </td>
                 </tr>
                 <tr>
                     <td class="style74">
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td class="style74">
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                 </tr>
    </table>
      </ContentTemplate>
        </asp:UpdatePanel>
         </asp:Content>

