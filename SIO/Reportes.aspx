<%@ Page Title="Carga de Estibas" Language="C#" MasterPageFile="~/General.Master"
    AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="SIO.CargaEstibas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script  type="text/javascript" src="http://code.jquery.com/jquery-1.4.1.min.js"></script>
    <script type="text/javascript">
        function Grid() {
            window.open('Grid.aspx', this.target, 'top=50, left=30, toolbar=no, location=no, status=no, menubar=no, scrollbars=yes, resizable=no, width=1200, height=500')
        }
        function GridAyu() {
            window.open('AyudantesLog.aspx', this.target, 'top=50, left=30, toolbar=no, location=no, status=no, menubar=no, scrollbars=yes, resizable=no, width=1200, height=500')
        }
    </script>
    <script src="Scripts/ScriptMetrolink.js" type="text/javascript"></script>
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
            background-image: url('http://app.forsa.com.co/SIOMaestros/Imagenes/toolkit-arrow.gif');
            background-repeat: no-repeat;
            border-style: none;
        }
        .CustomComboBoxStyle .ajax__combobox_textboxcontainer input
        {
            background-image: url('http://app.forsa.com.co/SIOMaestros/Imagenes/toolkit-bg.gif');
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
            color: #1C5AB6;
        }
        .style278
        {
            width: 741px;
            height: 44px;
            font-size: 22px;
            background-color: #1C5AB6;
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
        .botonsio
        {
        }
        #Button1
        {
            width: 110px;
            height: 39px;
        }
        #Button2
        {
            height: 41px;
            width: 201px;
        }
        #btnVer1
        {
            width: 79px;
        }
        #ActaTraza
        {
            width: 454px;
            height: 77px;
        }
        .style284
        {
            width: 728px;
        }
        .style288
        {
            width: 58px;
        }
        #Packing
        {
            width: 455px;
        }
        .style297
        {
            width: 107px;
        }
        .style298
        {
            width: 25px;
        }
        .style299
        {
            width: 97px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <br />
            <br />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblClienteTitulo" runat="server" CssClass="sangria" Font-Bold="True"
                            Font-Names="Arial" ForeColor="#1C5AB6" Text="REPORTES" Width="339px"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:Panel ID="PanelReporte" runat="server" BackColor="White" Font-Bold="True" Font-Size="8pt"
                GroupingText=" " Height="1000px" Width="476px" 
                Style="margin-top: 0px; margin-left: 2px;">
                <table style="width: 464px; height: 47px">
                    <tr>
                        <td>
                            <asp:Button ID="btnActaTraz" runat="server" Text="Acta de Trazabilidad" Height="45px"
                                Width="194px" OnClick="btnActaTraz_Click" />
                        </td>
                        <td>
                            <asp:Button ID="btnPacking" runat="server" Text="Packing List Summany" Height="45px"
                                Width="194px" OnClick="btnPacking_Click" />
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td class="style284">
                        </td>
                    </tr>
                </table>
                <table id="ActaTraza" runat="server">
                    <tr class="style278">
                        <td class="style297">
                            <asp:Label ID="lblOrden" runat="server" Text="No. Orden: " Width="211px" Style="text-align: right"></asp:Label>
                        </td>
                        <td class="style298">
                            <asp:TextBox ID="txtOrden" runat="server" Width="202px" Height="31px" Style="text-align: left"
                                 Font-Size="20" BackColor="#1C5AB6" OnTextChanged="txtOrden_TextChanged" 
                                ForeColor="White" AutoPostBack="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="style279">
                        <td class="style297">
                            <asp:Label ID="lblPlaca" runat="server" Style="text-align: right" Text="Placa: "
                                Width="211px"></asp:Label>
                        </td>
                        <td class="style288">
                            <asp:ComboBox ID="cboPlaca" runat="server" AutoCompleteMode="Append" AutoPostBack="True"
                                 DropDownStyle="DropDownList" Font-Size="17" 
                                OnSelectedIndexChanged="cboPlaca_SelectedIndexChanged" Width="179px">
                            </asp:ComboBox>
                        </td>
                    </tr>
                    <tr class="style279">
                        <td colspan="2">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblMensaje"
                                runat="server" Width="336px" Style="text-align: center; margin-left: 0px;"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table id="Packing" runat="server">
                    <tr class="style278">
                        <td class="style299">
                            <asp:Label ID="lblOrden2" runat="server" Text="No. Orden: " Width="211px" Style="text-align: right"></asp:Label>
                        </td>
                        <td class="style298">
                            <asp:TextBox ID="txtOrden2" runat="server" Width="202px" Height="31px" Style="text-align: left; margin-left: 0px;"
                                 Font-Size="20" BackColor="#1C5AB6" OnTextChanged="txtOrden2_TextChanged" 
                                ForeColor="White" AutoPostBack="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="style279">
                        <td class="style299">
                            <asp:Label ID="lblPlaca2" runat="server" Style="text-align: right" Text="Placa: "
                                Width="211px"></asp:Label>
                        </td>
                        <td class="style288">
                            <asp:ComboBox ID="cboPlaca2" runat="server" AutoCompleteMode="Append" AutoPostBack="True"
                                 DropDownStyle="DropDownList" Font-Size="17" 
                                 Width="179px" onselectedindexchanged="cboPlaca2_SelectedIndexChanged">
                            </asp:ComboBox>
                        </td>
                    </tr>
                    <tr class="style279">
                        <td colspan="2">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblMensaje2"
                                runat="server" Width="336px" Style="text-align: center; margin-left: 0px;"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>