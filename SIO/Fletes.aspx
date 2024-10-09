<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="Fletes.aspx.cs" Inherits="SIO.Fletes" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style9
    {
        width: 100%;
            height: 95px;
        }
    .CustomComboBoxStyle .ajax__combobox_buttoncontainer button 
        {
            background-image:url('http://app.forsa.com.co/SIOMaestros/Imagenes/toolkit-arrow.gif');
            border-style:none;            
        }
        .CustomComboBoxStyle .ajax__combobox_textboxcontainer input 
        {
            background-image:url('http://app.forsa.com.co/SIOMaestros/Imagenes/toolkit-bg.gif');
            border-style:none;  
        }
        .CustomComboBoxStyle .ajax__combobox_itemlist li
        {
            color: Black;  
            font-size:8pt;  
            font-family:Arial; 
            background-color:#EBEBEB;  
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
        .style20
        {
            width: 432px;
        }
        .style21
        {
            width: 103px;
            text-align: left;
        }
        .style24
        {
            width: 202px;
        text-align: right;
    }
        .style26
        {
            width: 100%;
        }
        .style30
    {
        width: 359px;
    }
    .style31
    {
        width: 135px;
    }
    .style33
    {
        height: 10px;
    }
    .style34
    {
        width: 149px;
        text-align: right;
    }
    .style35
    {
        width: 149px;
        text-align: right;
        height: 29px;
    }
    .style36
    {
        height: 29px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
            <table class="style9">
                <tr>
                    <td rowspan="2" class="style30">
                        <asp:Panel ID="pnlValoresMin" runat="server" Font-Names="Arial" Font-Size="8pt" 
                            GroupingText="VALORES MINIMOS" Height="167px" Width="383px" 
                            ForeColor="Black">
                            <table class="style9">
                                <tr>
                                    <td style="text-align: right">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Flete Minimo:" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFleteMin" runat="server" AutoPostBack="True" 
                                            Font-Names="Arial" Font-Size="10pt" ontextchanged="txtFleteMin_TextChanged" 
                                            style="text-align: right" Width="130px"></asp:TextBox>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style34">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style34">
                                        <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Seguro Minimo:" style="text-align: right" Width="100px" 
                                            ForeColor="Black"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSeguroMin" runat="server" Width="130px" Font-Names="Arial" 
                                            Font-Size="10pt" style="text-align: right" AutoPostBack="True" 
                                            ontextchanged="txtSeguroMin_TextChanged"></asp:TextBox>
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
                                        <asp:Label ID="lblIdFlete" runat="server" Font-Names="Arial" Font-Size="10pt" 
                                            style="text-align: right" Text="Label" Visible="False" Width="30px" 
                                            ForeColor="Black"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style34">
                                        <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Tarifa Minima:" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblTarifaMinima" runat="server" Font-Names="Arial" Font-Size="11pt" 
                                            Text="Label" Width="130px" style="text-align: right" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnActFletMin" runat="server" BackColor="#1C5AB6" 
                                            BorderColor="#999999" CssClass="botonsio" Font-Bold="True" Font-Names="Arial" 
                                            Font-Size="8pt"  onclick="btnActFletMin_Click" 
                                            onclientclick="return confirm('Desea Enviar El Formulario')" Text="Actualizar" 
                                            Width="80px" Visible="False" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </asp:Panel>
                    </td>
                    <td>
                        <asp:Panel ID="pnlValoresCiudad" runat="server" Font-Names="Arial" 
                            Font-Size="8pt" GroupingText="VALORES A CIUDADES" Height="117px" 
                            Width="444px" ForeColor="Black">
                            <table class="style9">
                                <tr>
                                    <td style="text-align: right" class="style31">
                                        <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Ciudad Destino:" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td class="style24">
                                        <asp:ComboBox ID="cboCiudades" runat="server" AutoCompleteMode="SuggestAppend" 
                                            AutoPostBack="True" BackColor="#DDE6F7" Font-Names="Arial" Font-Size="8pt" 
                                            ForeColor="Black" ontextchanged="cboCiudades_SelectedIndexChanged" 
                                            Width="180px" onselectedindexchanged="cboCiudades_SelectedIndexChanged">
                                        </asp:ComboBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right" class="style31">
                                        <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Valor x Kg:" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtValorCiudad" runat="server" Width="113px" 
                                            Font-Names="Arial" Font-Size="10pt" AutoPostBack="True" 
                                            ontextchanged="txtValorCiudad_TextChanged" style="text-align: right"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnActValorCiuda" runat="server" BackColor="#1C5AB6" 
                                            BorderColor="#999999" CssClass="botonsio" Font-Bold="True" Font-Names="Arial" 
                                            Font-Size="8pt"  onclick="btnActValorCiuda_Click" 
                                            onclientclick="return confirm('Desea Enviar El Formulario')" Text="Actualizar" 
                                            Visible="False" Width="80px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style33">
                                        </td>
                                    <td style="text-align: right; font-size: xx-small;" class="style33">
                                    </td>
                                    <td style="text-align: right; font-size: xx-small;" class="style33">
                                        </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td rowspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="Panel1" runat="server" Font-Names="Arial" Font-Size="8pt" 
                            GroupingText="TOLERANCIA" Height="51px" Width="442px" ForeColor="Black">
                            <table>
                                <tr>
                                    <td class="style20" style="text-align: right">
                                        <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="% De Tolerancia" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td class="style21">
                                        <asp:TextBox ID="txtTolerancia" runat="server" Width="45px" AutoPostBack="True" 
                                            ontextchanged="txtTolerancia_TextChanged" style="text-align: right"></asp:TextBox>
                                        <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            style="font-size: 12pt" Text="%" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td style="text-align: right">
                                        &nbsp;</td>
                                    <td style="text-align: right">
                                        <asp:Button ID="btnActValorCiuda0" runat="server" BackColor="#1C5AB6" 
                                            BorderColor="#999999" CssClass="botonsio" Font-Bold="True" Font-Names="Arial" 
                                            Font-Size="8pt"  onclick="btnActValorCiuda0_Click" 
                                            onclientclick="return confirm('Desea Enviar El Formulario')" Text="Actualizar" 
                                            Visible="False" Width="80px" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td class="style30">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                            <table class="style26">
        <tr>
            <td>
             
            <span>
            <rsweb:ReportViewer ID="ReportViewer2" runat="server" AsyncRendering="False" 
                BackColor="White" Height="" SizeToReportContent="True" Width="">
            </rsweb:ReportViewer>
            </span>
                            
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    
    </ContentTemplate>
                        </asp:UpdatePanel>   
</asp:Content>
