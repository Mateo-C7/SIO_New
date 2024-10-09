<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="OrdenDetalle.aspx.cs" Inherits="SIO.OrdenDetalle" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">

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
        .style2
        {
            width: 66px;
            text-align: right;
        }
        .style3
        {
            width: 99px;
        }
        .style4
        {
            width: 48px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <table style="height: 94px; width: 647px;" >
        <tr >
            <td style="text-align: right" >
                <asp:Label ID="lblPais1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Pais"
                                                        Width="45px" ForeColor="Black"></asp:Label>
            </td>
            <td style="text-align: left" colspan="2">
                <asp:Label ID="lblPais" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Pais"
                                                        Width="222px" ForeColor="#1C5AB6" 
                    style="font-weight: 700"></asp:Label>
            </td>
            <td class="style4">
                &nbsp;</td>
            <td style="text-align: right" class="style3" >
                <asp:Label ID="lblOrden2" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                        Text="Orden" Width="30px"></asp:Label>
            </td>
            <td _>
                <asp:Label ID="lblOrden" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="#1C5AB6"
                                              BackColor = "Yellow"          Text="Orden" 
                    Width="108px" 
                    style="font-weight: 700; text-align: center; font-size: 9pt;"></asp:Label>
            </td>
        </tr>
        <tr >
            <td style="text-align: right" >
                <asp:Label ID="lblCliente1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Empresa"
                                                        Width="70px" ForeColor="Black"></asp:Label>
            </td>
            <td colspan="3" style="text-align: left" >
                <asp:Label ID="lblCliente" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Empresa"
                                                        Width="445px" ForeColor="#1C5AB6" 
                    style="font-weight: 700"></asp:Label>
            </td>
            <td style="text-align: right" class="style3" >
                <asp:Label ID="lblPlanta1" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                        Text="Planta" Width="44px"></asp:Label>
            </td>
            <td style="text-align: left" >
                <asp:Label ID="lblPlanta" runat="server" Font-Names="Arial" Font-Size="8pt" 
                    ForeColor="#1C5AB6"             Text="Planta" 
                    Width="107px" style="font-weight: 700; text-align: left;"></asp:Label>
            </td>
        </tr>
        <tr _>
            <td style="text-align: right" >
                <asp:Label ID="lblObra1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                        Text="Obra" Width="50px" 
                    ForeColor="Black"></asp:Label>
            </td>
            <td colspan="3" style="text-align: left" >
                <asp:Label ID="lblObra" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                        Text="Obra" Width="445px" 
                    ForeColor="#1C5AB6" style="font-weight: 700"></asp:Label>
            </td>
            <td style="text-align: right" class="style3" >
                <asp:Label ID="lblParte1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                        ForeColor="Black" 
                    Text="Parte" Width="55px"></asp:Label>
            </td>
            <td style="text-align: left" >
                <asp:Label ID="lblParte" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="#1C5AB6"
                                                   Text="Parte" 
                    Width="107px" style="font-weight: 700"></asp:Label>
            </td>
        </tr>
        <tr >
            <td style="text-align: right" >
                <asp:Label ID="lblVivienda" runat="server" Font-Bold="False" Font-Names="Arial" Font-Overline="False"
                                                        Font-Size="8pt" ForeColor="Black" 
                    Height="16px" Style="text-align: right; margin-right: 0px;"
                                                        Text="Tipo Cotizacion " Width="90px"></asp:Label>
            </td>
            <td style="text-align: left" >
                <asp:Label ID="lblTipoCotizacion" runat="server" Font-Bold="False" 
                    Font-Names="Arial" Font-Overline="False"
                                                        Font-Size="8pt" ForeColor="#1C5AB6" 
                    Height="16px" Style="text-align: left; margin-right: 0px; font-weight: 700;"
                                                        Text="Tipo Cotizacion " Width="141px"></asp:Label>
            </td>
            <td style="text-align: right" class="style2" >
                <asp:Label ID="lblFormaleta1" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                    ForeColor="Black"    Text="Tipo Formaleta"  Width="80px"></asp:Label>
            </td>
            <td style="text-align: left" class="style4" >
                <asp:Label ID="lblFormaleta" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                           ForeColor="#1C5AB6"      Width="201px" 
                    style="font-weight: 700"></asp:Label>
            </td>
            <td style="text-align: right" class="style3" >
                <asp:Label ID="lblFup1" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                        Text="Fup/Ver" Width="53px"></asp:Label>
            </td>
            <td style="text-align: left" >
                                                    <asp:Label ID="lblFup" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="#1C5AB6"
                                                       Text="FupVersion" 
                    Width="105px" style="font-weight: 700"></asp:Label>
                                                </td>
        </tr>
        <tr >
            <td style="text-align: right" >
                <asp:Label ID="lblFecDespachoini" runat="server" Font-Bold="False" 
                    Font-Names="Arial" Font-Overline="False"
                                                        Font-Size="8pt" ForeColor="Black" 
                    Height="16px" Style="text-align: right; margin-right: 0px;"
                                                        Text="Fecha Desp Inicial " 
                    Width="88px"></asp:Label>
            </td>
            <td style="text-align: left" >
            <asp:TextBox ID="txtFecDespachoIni" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                        Visible="true" Width="80px"></asp:TextBox>
                                                                    <asp:CalendarExtender ID="txtFecDespacho_CalendarExtender" runat="server" Enabled="True"
                                                                        Format="yyyy-MM-dd" TargetControlID="txtFecDespachoIni">
                                                                    </asp:CalendarExtender>
                &nbsp;</td>
            <td style="text-align: left" class="style2" >
            </td>
            <td style="text-align: left" class="style4" >
            </td>
            <td style="text-align: right" class="style3" >
                <asp:Label ID="lblIdOfp" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Silver"
                                                        Text="idOfp" Width="68px"></asp:Label>
            </td>
            <td style="text-align: right" >
                <asp:Button ID="btnGuardar" runat="server" BackColor="#1C5AB6" BorderColor="#999999"
                                                        Font-Bold="True" Font-Names="Arial" 
                    Font-Size="8pt" ForeColor="White" Visible = "False"
                                                        Text="Guardar" Width="70px" 
                    onclick="btnGuardar_Click" />
            </td>
        </tr>
        <tr >
            <td style="text-align: right" ></td>
            <td style="text-align: right" class="style2" >
            </td>
            <td style="text-align: left" class="style4" >
                &nbsp;</td>
            <td style="text-align: left" class="style3" >
                                                          &nbsp;&nbsp;</td>
            <td style="text-align: right" >
                &nbsp;</td>
        </tr>
    </table>
</asp:Content>
