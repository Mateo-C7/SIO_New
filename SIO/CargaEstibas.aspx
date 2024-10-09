<%@ Page Title="Carga de Estibas" Language="C#" MasterPageFile="~/General.Master"
    AutoEventWireup="true" CodeBehind="CargaEstibas.aspx.cs" Inherits="SIO.CargaEstibas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://code.jquery.com/jquery-1.4.1.min.js"></script>
    <script type="text/javascript">
        function Grid() {
            window.open('Grid.aspx', this.target, 'top=50, left=30, toolbar=no, location=no, status=no, menubar=no, scrollbars=yes, resizable=no, width=1200, height=500')
       }
    </script>
    <script src="Script/ScriptJorge.js" type="text/javascript"></script>
    <style type="text/css">
        .watermarked
        {
            padding: 2px 0 0 2px;
            border: 1px solid #BEBEBE;
            background-color: white;
            color: Gray;
            font-family: Arial;
            font-weight: lighter;
        }
        .CustomComboBoxStyle .ajax__combobox_buttoncontainer button
        {
            background-image: url('http://mail.forsa.com.co/SIOMaestros/Imagenes/toolkit-arrow.gif');
            background-repeat: no-repeat;
            border-style: none;
        }
        .CustomComboBoxStyle .ajax__combobox_textboxcontainer input
        {
            background-image: url('http://mail.forsa.com.co/SIOMaestros/Imagenes/toolkit-bg.gif');
            background-repeat: no-repeat;
            border-style: none;
        }
        .CustomComboBoxStyle .ajax__combobox_itemlist li
        {
            color: red;
            font-size: 11pt;
            font-family: Arial;
            background-color: #EBEBEB;
            
        }
        .A:hover
        {
            background: white;
        }
        .botonsio:hover
        {
            color: white;
            background: blue;
        }
        .center
        {
            font-family: Arial;
            font-size: 8pt;
            text-align: Center;
        }
        .sangria
        {
            word-spacing: 10pt;
            font-family: Tahoma;
            font-size: 11pt;
            color: #3B5998;
        }
        .style32
        {
            width: 99%;
            height: 302px;
            font-family: Arial, Helvetica, sans-serif;
        }
        .style82
        {
            width: 91%;
            height: 423px;
        }
        .style83
        {
            height: 404px;
        }
        .style84
        {
            text-align: right;
        }
        .botonsio
        {
        }
        .style120
        {
            width: 80px;
            font-family:Arial;
            font-size: 22px;
        }
        .style188
        {
            width: 87px;
            height: 49px;
        }
        .style190
        {
            width: 87px;
            }
        .style234
        {
            width: 87px;
            height: 44px;
        }
        .style249
        {
            width: 87px;
            height: 46px;
        }
        .style261
        {
            width: 87px;
            height: 53px;
        }
        .style264
        {
            width: 87px;
            height: 47px;
        }
        .style278
        {
            width: 741px;
            height: 44px;
            font-size: 22px;
            background-color: #3B5998;
            color: White;
            font-family: Arial;
            
        }
        .style279
        {
            width: 741px;
            height: 44px;
            font-size: 22px;
            color: Black;
            font-family: Arial;
        }
        .style280
        {
            font-size: 22px;
            color: #3B5998;
            font-family: Arial;
            text-align: right;
        }
        .style295
        {
            width: 695px;
            height: 44px;
            font-size: 22px;
            color: Black;
            font-family: Arial;
        }
        .style296
        {
            width: 761px;
            height: 44px;
            font-size: 22px;
            background-color: #3B5998;
            color: White;
            font-family: Arial;
        }
        .style297
        {
            width: 761px;
            height: 44px;
            font-size: 22px;
            color: Black;
            font-family: Arial;
        }
        .style299
        {
            width: 780px;
            height: 44px;
            font-size: 22px;
            background-color: #3B5998;
            color: White;
            font-family: Arial;
        }
        .style300
        {
            width: 780px;
            height: 44px;
            font-size: 22px;
            color: Black;
            font-family: Arial;
        }
        .style301
        {
            width: 780px;
        }
        .style302
        {
            width: 778px;
        }
        .style303
        {
            width: 778px;
            height: 44px;
            font-size: 22px;
            color: Black;
            font-family: Arial;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table class="style82">
                <tr>
                    <td class="style120">
                    <br />
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td class="style84">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style83" colspan="4">
                        <asp:Label ID="lblClienteTitulo" runat="server" CssClass="sangria" 
                            Font-Bold="True" Font-Names="Arial" ForeColor="#3B5998" Text="DESPACHO DE CARGA" 
                            Width="339px"></asp:Label>
                        <asp:Panel ID="PanelCarga" runat="server" BackColor="White" Font-Bold="True"  
                            Font-Size="8pt" GroupingText=" " Height="322px" Width="1232px"
                            Style="margin-top: 2px">
                            <table class="style32">
                            <tr>
                            <td class="style280" colspan="7"><a onclick="Grid();">Ver Estibas</a> </td>
                            </tr>
                                <tr>
                                    <td class="style234">
                                    </td>
                                    <td class="style278">
                                        <asp:Label ID="lblNumOrden1" runat="server" Font-Bold="False" 
                                           Text="Orden No.:" Width="139px" style="text-align: right"></asp:Label>
                                    </td>
                                    <td class="style278">
                                        <asp:Panel ID="Panel1" runat="server" DefaultButton="Button1" Width="174px" 
                                            Height="33px">
                                            <asp:TextBox ID="txtNumOrden1" runat="server" 
                                                Width="166px" Height="30px" Style="text-align: left" ForeColor="White" Font-Size="20"
                                                BackColor="#3B5998"></asp:TextBox>
                                            <asp:Button ID="Button1" runat="server" Text="Button1" Style="display: none" OnClick="Button1_Click" />
                                        </asp:Panel>
                                    </td>
                                    <td class="style299"  >
                                        <asp:Label ID="lblDespacho1" runat="server" Font-Bold="False" 
                                            Text="Despacho:" Width="130px" Height="21px" 
                                            Style="margin-left: 0px; text-align: right;"></asp:Label>
                                    </td>
                                    <td class="style302">
                                        <asp:ComboBox ID="cboDespacho1" runat="server" AutoCompleteMode="Append" CssClass="CustomComboBoxStyle"
                                            DropDownStyle="DropDownList" ForeColor="White" Font-Size="17"
                                            Width="190px" AutoPostBack="true" OnSelectedIndexChanged="cboDespacho1_SelectedIndexChanged">
                                        </asp:ComboBox>
                                    </td>
                                    <td class="style296"  >
                                        <asp:Label ID="lblContenedor1" runat="server" Font-Bold="False"
                                             Text="Contenedor:" Width="280px" style="text-align: right"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:ComboBox ID="cboContenedor1" runat="server" AutoCompleteMode="Append" AutoPostBack="True"
                                            CssClass="CustomComboBoxStyle" DropDownStyle="DropDownList" ForeColor="White" Font-Size="17"
                                            Width="190px" OnSelectedIndexChanged="cboContenedor1_SelectedIndexChanged1">
                                        </asp:ComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style249">
                                    </td>
                                    <td class="style279" >
                                        <asp:Label ID="lblPais1" runat="server" Font-Bold="False"   
                                            Text="Pais:" Width="139px" style="text-align: right"></asp:Label>
                                    </td>
                                    <td class="style295">
                                        <asp:Label ID="lblResulPais1" runat="server" Font-Bold="False"  
                                             Text="" Width="40px"></asp:Label>
                                    </td>
                                    <td class="style300">
                                        <asp:Label ID="lblCiudad1" runat="server" Font-Bold="False"   
                                            Style="margin-left: 2px; text-align: right;" Text="Ciudad:" Width="128px"></asp:Label>
                                    </td>
                                    <td class="style303">
                                        <asp:Label ID="lblResulCiudad1" runat="server" Font-Bold="False" Width="232px"></asp:Label>
                                    </td>
                                    <td class="style297">
                                        <asp:Label ID="lblContPallet1" runat="server" Font-Bold="False" 
                                              Height="29px" 
                                            Text="Pallets en este contenedor:" Width="280px" style="text-align: right"></asp:Label>
                                    </td>
                                    <td class="style279">
                                        <asp:Label ID="lblContPallet2" runat="server" Font-Bold="False" 
                                              Height="20px" Style="text-align: right" 
                                            Text="" Width="60px"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="style278" >
                                    <td class="style188">
                                    </td>
                                    <td class="style278"  >
                                        <asp:Label ID="lblCodEstiba1" runat="server" Font-Bold="False"
                                            Text="Pallet:" Width="137px" style="text-align: right"></asp:Label>
                                    </td>
                                    <td class="style278" >
                                        <asp:Panel ID="Panel2" runat="server" DefaultButton="Button2" Width="174px">
                                            <asp:TextBox ID="txtCodEstiba1" runat="server" 
                                                Width="170px" Style="text-align: left" Height="41px" ForeColor="White" Font-Size="20"
                                                onkeypress="return punto(this)" BackColor="#3B5998"></asp:TextBox>
                                            <asp:Button ID="Button2" runat="server" Text="Button2" Style="display: none" OnClick="Button2_Click" />
                                        </asp:Panel>
                                    </td>
                                    <td class="style278" colspan="2">
                                        <asp:Button ID="btnCargar" runat="server" BackColor="#3B5998" BorderColor="#999999"
                                            CssClass="botonsio" Font-Bold="True"  ForeColor="White"
                                            Text="Cargue Camion" Width="106px" Height="47px" Style="display: none" />
                                        &nbsp;&nbsp;<asp:Label ID="lblTP" runat="server" Text="Peso Total:" Font-Bold="False"></asp:Label>
                                        &nbsp;
                                        <asp:Label ID="lblTotaP" runat="server" Text="" Font-Bold="False"></asp:Label>
                                    </td>
                                    <td class="style296">
                                        <asp:Label ID="lblPalletCargados1" runat="server" Font-Bold="False" 
                                            Height="22px" Text="Pallet Cargados(total):" Width="280px" 
                                            style="text-align: right"></asp:Label>
                                       </td>
                                    <td class="style278">
                                        <asp:Label ID="lblPalletCargados2" runat="server" Font-Bold="False" 
                                            Height="20px" Style="text-align: right" Text="" Width="60px"></asp:Label>
                                      </td>
                                        
                                </tr>
                                <tr>
                                    <td class="style261">
                                    </td>
                                    <td class="style279">
                                        <asp:Label ID="lblPlaca1" runat="server" Font-Bold="False"  
                                            Width="141px" Text="Placa/Precinto:" style="text-align: right"></asp:Label>
                                    </td>
                                    <td class="style295" colspan="3">
                                        <asp:Label ID="lblResulPlaca1" runat="server" Font-Bold="True"  
                                            Width="133px" Style="margin-left: 43px"></asp:Label>
                                    </td>
                                    <td class="style297">
                                        <asp:Label ID="lblPalletTotal" runat="server" Font-Bold="False" Height="26px" 
                                            Style="margin-left: 0px; text-align: right;" Text="Pallet Total:" 
                                            Width="279px"></asp:Label>
                                    </td>
                                    <td class="style279">
                                        <asp:Label ID="lblResPalletTotal" runat="server" Font-Bold="False" 
                                            Height="20px" Style="text-align: right" Text="" Width="60px"> </asp:Label>
                                        </td>
                                    
                                </tr>
                                <tr class="style278">
                                    <td class="style264">
                                    </td>
                                    <td class="style278" colspan="2">
                                        <asp:Button ID="btnAsociar" runat="server" BackColor="red" 
                                            BorderColor="#999999" CssClass="botonsio" Font-Bold="True"  
                                            ForeColor="White" Height="55px" OnClick="btnAsociar_Click" 
                                            OnClientClick="return confirm('Seguro que desea desasociar la Estiba?');" 
                                            Text="Desasociar?" />
                                    </td>
                                    <td colspan="2">
                                        &nbsp;&nbsp;
                                        <asp:Label ID="lblCA" runat="server" Text=""></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnCA" runat="server" Text=""  Color="White" 
                                            Font-Bold="True"  
                                            ForeColor="Black" Height="31px" Width="130px" onclick="btnCA_Click" />
                                    </td>
                                    
                                    <td class="style296" >
                                        <asp:Label ID="lblPalletFaltantes1" runat="server" Font-Bold="False" 
                                            Height="26px" Text="Pallet Faltantes:" Width="285px" 
                                            style="text-align: right"></asp:Label>
                                    </td>
                                    <td class="style278">
                                       
                                        <asp:Label ID="lblPalletFaltantes2" runat="server" Font-Bold="False" 
                                            Height="20px" Style="text-align: right" Text="" Width="60px"> </asp:Label>
                                       
                                        </td>
                                   
                                  
                                </tr>
                                <tr>
                                    <td class="style190">
                                    </td>
                                    <td class="style190">
                                        &nbsp;</td>
                                        <td class="style190" >
                                            
                                             &nbsp;</td>
                                        <td class="style301" colspan="3">
                                            <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Size="18pt" 
                                                ForeColor="Black" style="text-align: center" Width="343px"></asp:Label>
                                    </td>
                                            <td class="style301" >
                                                &nbsp;</td>
                                            <td class="style301" >
                                            &nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
