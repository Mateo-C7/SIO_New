<%@ Page Title="Cliente" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="Cliente.aspx.cs" Inherits="SIO.Cliente" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .watermarked {
            padding: 2px 0 0 2px;
            border: 1px solid #BEBEBE;
            background-color: white;
            color: Gray;
            font-family: Arial;
            font-weight: lighter;
        }

        .CustomComboBoxStyle .ajax__combobox_buttoncontainer button {
            background-image: url('Imagenes/toolkit-arrow.png');
            border-style: none;
        }

        .CustomComboBoxStyle .ajax__combobox_textboxcontainer input {
            background-image: url('Imagenes/toolkit-bg.png');
            border-style: none;
        }

        .CustomComboBoxStyle .ajax__combobox_itemlist li {
            color: Black;
            font-size: 8pt;
            font-family: Arial;
            background-color: #EBEBEB;
        }

        .A:hover {
            background: white
        }

        .botonsio:hover {
            color: white;
            background: blue
        }

        .center {
            font-family: Arial;
            font-size: 8pt;
            Text-Align: Center;
        }

        .sangria {
            word-spacing: 10pt;
            font-family: Tahoma;
            font-size: 11pt;
            color: #1C5AB6;
            text-align: right;
        }

        .style84 {
            text-align: right;
        }

        .style109 {
            width: 95px;
            text-align: right;
        }

        .style110 {
            width: 268435520px;
        }

        .style115 {
            width: 36px;
        }

        .style123 {
            width: 100px;
            text-align: right;
        }
    </style>

    <script type="text/javascript" src="Scripts/jquery.i18n.js"></script>
    <script type="text/javascript" src="Scripts/jquery.i18n.messagestore.js"></script>
    <%--<script type="text/javascript" src="Scripts/jquery.i18n.fallbacks.js"></script>
    <script type="text/javascript" src="Scripts/jquery.i18n.parser.js"></script>
    <script type="text/javascript" src="Scripts/jquery.i18n.emitter.js"></script>--%>
    <script type="text/javascript" src="Scripts/cliente.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td colspan="8">
                        <button type="button" class="btn btn-secondary langes">
                            <img alt="español" src="Imagenes/colombia.png" /></button>
                        <button type="button" class="btn btn-secondary langen">
                            <img alt="ingles" src="Imagenes/united-states.png" /></button>
                        <button type="button" class="btn btn-secondary langbr">
                            <img alt="portugues" src="Imagenes/brazil.png" /></button>
                    </td>
                </tr>
                <tr>
                    <td class="style123">
                        <asp:Label ID="lblBusq" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" data-i18n="busqueda" Height="16px" Text="Busqueda" Width="60px"></asp:Label>
                    </td>
                    <td style="text-align: right">
                        <asp:TextBox ID="txtBusqueda" runat="server" AutoPostBack="True"
                            BackColor="#FFFF66" Font-Names="Arial" Font-Size="8pt"
                            OnTextChanged="txtBusqueda_TextChanged"
                            Style="text-align: left; margin-left: 2px" Width="230px"></asp:TextBox>
                    </td>
                    <td>&nbsp;&nbsp;<asp:DropDownList ID="cboBusqClientes1" runat="server" AppendDataBoundItems="true"
                        AutoPostBack="True" BackColor="#DDE6F7" DataTextField="cli_nombre"
                        Font-Names="Arial" Font-Size="8pt" ForeColor="Black" MaxLength="0"
                        OnSelectedIndexChanged="cboBusqClientes1_SelectedIndexChanged"
                        OnTextChanged="cboBusqClientes1_TextChanged" Style="display: inline;"
                        Visible="False" Width="410px">
                    </asp:DropDownList>
                    </td>
                    <td>&nbsp;<asp:ImageButton ID="ImageButton1" runat="server" Visible="False"
                        ImageUrl="~/Imagenes/Arrow back.png" OnClick="ImageButton1_Click"
                        Style="text-align: right" ToolTip="Volver a Obra" /></td>
                </tr>
            </table>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:LinkButton ID="LkverHV" runat="server" BackColor="#E6E6E6"
                                    Font-Names="Arial" Font-Size="8pt" Font-Underline="False" ForeColor="Black"
                                    OnClick="LkverHV_Click" Style="text-align: center; text-decoration: underline;"
                                    ToolTip="Crea contacto de este cliente" data-i18n="[html]hoja" Visible="False" Width="85px">hoja</asp:LinkButton>
                            </td>
                            <td>&nbsp;</td>
                            <td class="style84">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lblClienteTitulo" runat="server" CssClass="sangria"
                                    Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" ForeColor="#1C5AB6"
                                    Text="EMPRESA - " Width="120px" data-i18n="empresa"></asp:Label>
                                <asp:Label ID="lblClientep" runat="server" Font-Bold="True" Font-Names="Tahoma"
                                    Font-Size="9pt" ForeColor="#1C5AB6" Width="600px"></asp:Label>
                                &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                            </td>
                            <td>&nbsp;</td>
                            <td class="style84">&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="6">

                                <table style="width: 865px;">
                                    <tr>
                                        <td style="text-align: right" colspan="4">
                                            <asp:Panel ID="pnlRegistrar" runat="server"
                                                Style="text-align: right" Visible="False" Width="810px">
                                                <asp:LinkButton ID="lkCreaContacto" runat="server" BackColor="#E6E6E6"
                                                    Font-Names="Arial" Font-Size="8pt" Font-Underline="False" ForeColor="Black"
                                                    OnClick="lkCreaContacto_Click" Style="text-align: center; text-decoration: underline;"
                                                    ToolTip="Crea contacto de este cliente" data-i18n="[html]contacto" Visible="False" Width="85px">CONTACTO</asp:LinkButton>
                                                <asp:LinkButton ID="lkCreaVisita" runat="server" BackColor="#E6E6E6"
                                                    Font-Names="Arial" Font-Size="8pt" Font-Underline="False" ForeColor="Black"
                                                    OnClick="lkCreaVisita_Click" Style="text-align: center; text-decoration: underline;"
                                                    ToolTip="Crea Visita de este cliente" data-i18n="[html]visita" Visible="False" Width="85px">VISITA</asp:LinkButton>
                                                <asp:LinkButton ID="lkCrearObra" runat="server" BackColor="#E6E6E6"
                                                    Font-Names="Arial" Font-Size="8pt" Font-Underline="False" ForeColor="Black"
                                                    OnClick="lkCrearObra_Click"
                                                    Style="text-align: center; text-decoration: underline;" Visible="False"
                                                    Width="85px" data-i18n="[html]obra">OBRA</asp:LinkButton>
                                                <asp:LinkButton ID="lkCreaProyecto" runat="server" BackColor="#E6E6E6"
                                                    Font-Names="Arial" Font-Size="8pt" Font-Underline="False" ForeColor="Black"
                                                    OnClick="lkCreaProyecto_Click" Style="text-align: center; text-decoration: underline;"
                                                    ToolTip="Crea contacto de este cliente" data-i18n="[html]fup" Visible="False" Width="85px">FUP</asp:LinkButton>
                                                <asp:LinkButton ID="lkCreaPV" runat="server" BackColor="#E6E6E6"
                                                    Font-Names="Arial" Font-Size="8pt" Font-Underline="False" ForeColor="Black"
                                                    OnClick="lkCreaProyecto0_Click" Style="text-align: center; text-decoration: underline;"
                                                    ToolTip="Crea contacto de este cliente" data-i18n="[html]pedido_venta" Visible="False" Width="85px">PEDIDO VENTA</asp:LinkButton>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">&nbsp;</td>
                                        <td colspan="3" style="text-align: left">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right" class="style111">
                                            <asp:Label ID="lblOrigen" runat="server" Font-Bold="False" Font-Names="Arial"
                                                Font-Size="8pt" ForeColor="Black" Height="16px" Style="text-align: center"
                                                Text="Origen*" data-i18n="origen" Width="40px"></asp:Label>
                                        </td>
                                        <td style="text-align: left" class="style110">
                                            <asp:DropDownList ID="cbo_Origen" runat="server" AutoCompleteMode="Append"
                                                AutoPostBack="true" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="Black"
                                                Width="100px" OnSelectedIndexChanged="cbo_Origen_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: right" class="style108">
                                            <asp:Label ID="lblFuente" runat="server" Font-Bold="False" Font-Names="Arial"
                                                Font-Size="8pt" ForeColor="Black" data-i18n="fuente"
                                                Style="text-align: center; margin-bottom: 0px;" Text="Fuente*" Width="40px"></asp:Label>
                                        </td>
                                        <td class="style96" style="text-align: left">
                                            <asp:DropDownList ID="cbo_Fuente" runat="server" AutoCompleteMode="Append"
                                                AutoPostBack="true" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="Black"
                                                Width="300px">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="auto-style1" style="text-align: left">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:Label ID="lblCliente" runat="server" Font-Bold="False"
                                                Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Text="Nombre Empresa *"
                                                Width="100px" Height="16px" data-i18n="nombre_empresa"></asp:Label>
                                        </td>
                                        <td class="style115">
                                            <asp:TextBox ID="cli_nombre" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="Black" OnTextChanged="cli_nombre_TextChanged" TabIndex="1"
                                                Width="342px"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lblNIT" runat="server" Font-Bold="False" Font-Names="Arial"
                                                Font-Size="8pt" ForeColor="Black" Text="NIT" data-i18n="nit" Width="40px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="cli_nit" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                TabIndex="3" Width="218px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:Label ID="lblDireccion" runat="server" Font-Bold="False"
                                                Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Text="Dirección Fiscal *"
                                                Width="85px" data-i18n="direccion_fiscal"></asp:Label>
                                            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial"
                                                Font-Size="8pt" Text="."></asp:Label>
                                        </td>
                                        <td class="style115">
                                            <asp:TextBox ID="cli_direccion" runat="server" Font-Names="Arial"
                                                Font-Size="8pt" ForeColor="Black" Height="30px"
                                                OnTextChanged="cli_direccion_TextChanged" TabIndex="2" TextMode="MultiLine"
                                                Width="340px"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right">
                                            <asp:Label ID="lblTelef2" runat="server" Font-Bold="False" Font-Names="Arial"
                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Teléfono 2"
                                                Width="60px" data-i18n="telefono2"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="preTel2" runat="server" CssClass="center" Font-Bold="True"
                                                Font-Names="Arial" Font-Size="8pt" Text="0" Width="20px"></asp:Label>
                                            <asp:TextBox ID="prefijo2" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="Black" OnTextChanged="cli_telefono_TextChanged" TabIndex="9"
                                                Width="25px"></asp:TextBox>
                                            <asp:TextBox ID="cli_telefono_2" runat="server" Font-Names="Arial"
                                                Font-Size="8pt" TabIndex="10" Width="163px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblTelef1" runat="server" Font-Bold="False" Font-Names="Arial"
                                                Font-Size="8pt" ForeColor="Black" Text="Teléfono 1 *" data-i18n="telefono1" Width="60px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="preTel" runat="server" CssClass="center" Font-Bold="True"
                                                Font-Names="Arial" Font-Size="8pt" Text="0" Width="20px"></asp:Label>
                                            <asp:TextBox ID="prefijo1" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="Black" OnTextChanged="cli_telefono_TextChanged" TabIndex="7"
                                                Width="25px"></asp:TextBox>
                                            <asp:TextBox ID="cli_telefono" runat="server" Font-Names="Arial"
                                                Font-Size="8pt" ForeColor="Black" OnTextChanged="cli_telefono_TextChanged"
                                                TabIndex="8" Width="190px"></asp:TextBox>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblFAX" runat="server" Font-Bold="False" Font-Names="Arial"
                                                Font-Size="8pt" ForeColor="Black" Height="16px"  data-i18n="fax" Text="FAX" Width="20px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="preTel3" runat="server" CssClass="center" Font-Bold="True"
                                                Font-Names="Arial" Text="0" Width="20px"></asp:Label>
                                            <asp:TextBox ID="prefijo3" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="Black" OnTextChanged="cli_telefono_TextChanged" TabIndex="11"
                                                Width="25px"></asp:TextBox>
                                            <asp:TextBox ID="cli_fax" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                TabIndex="12" Width="163px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblTipoCliente0" runat="server" Font-Bold="False"
                                                Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Text="Tipo Empresa *"  data-i18n="tipo_empresa"
                                                Width="100px"></asp:Label>
                                        </td>
                                        <td class="style115">
                                            <asp:DropDownList ID="cboTipoApoyo" runat="server" AutoCompleteMode="SuggestAppend"
                                                AutoPostBack="True" DropDownStyle="DropDownList" Font-Names="Arial" AppendDataBoundItems="true"
                                                Font-Size="8pt" TabIndex="5" Width="220px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblEmail" runat="server" Font-Bold="False" Font-Names="Arial"
                                                Font-Size="8pt" ForeColor="Black" Text="E - Mail"  data-i18n="email" Width="40px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="cli_mail" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                Style="margin-left: 0px" TabIndex="13" Width="218px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">&nbsp;</td>
                                        <td class="style115">&nbsp;</td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblWeb" runat="server" Font-Bold="False" Font-Names="Arial"
                                                Font-Size="8pt" ForeColor="Black" Height="16px" Text="Web Site"  data-i18n="website" Width="45px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="cli_web" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                Style="margin-left: 0px; margin-top: 0px" TabIndex="14" Width="218px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblPais" runat="server" Font-Bold="False" Font-Names="Arial"
                                                Font-Size="8pt" ForeColor="Black" Text="Pais *" data-i18n="pais" Width="50px"></asp:Label>
                                        </td>
                                        <td class="style115">
                                            <asp:DropDownList ID="cboPais" runat="server" AutoCompleteMode="SuggestAppend"
                                                AutoPostBack="True" DropDownStyle="DropDownList" Font-Names="Arial" AppendDataBoundItems="true"
                                                Font-Size="8pt" OnSelectedIndexChanged="cboPais_SelectedIndexChanged"
                                                TabIndex="5" Width="220px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblTipoContri" runat="server" Font-Bold="False" data-i18n="tipo_contribuyente"
                                                Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Height="16px"
                                                Style="margin-top: 3px" Text="Tipo Contribuyente" Width="100px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cli_tco_id" runat="server" AutoCompleteMode="SuggestAppend" AppendDataBoundItems="true"
                                                AutoPostBack="True" DropDownStyle="DropDownList" Font-Names="Arial"
                                                Font-Size="8pt" TabIndex="15" Width="205px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblCiudad" runat="server" Font-Bold="False" Font-Names="Arial" data-i18n="ciudad"
                                                Font-Size="8pt" ForeColor="Black" Text="Ciudad *" Width="50px"></asp:Label>
                                        </td>
                                        <td class="style115">
                                            <asp:DropDownList ID="cboCiudad" runat="server" AutoCompleteMode="SuggestAppend"
                                                AutoPostBack="True" DropDownStyle="DropDownList" Font-Names="Arial" AppendDataBoundItems="true"
                                                Font-Size="8pt" TabIndex="6" Width="220px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblTipoContri0" runat="server" Font-Bold="False"
                                                Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Height="16px"
                                                Style="margin-top: 3px" Text="Tipo Construccion" data-i18n="tipo_construccion" Width="90px"></asp:Label>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:CheckBox ID="chkVivienda" runat="server" Style="color: #000000"
                                                Text="" /> <label data-i18n="vivienda"></label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:CheckBox ID="chkInfra" runat="server" Style="color: #000000"
                                        Text="" /> <label data-i18n="Otras"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblTipoCliente" runat="server" Font-Bold="False"
                                                Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Text="Matriz/Sucursal *"
                                                Width="90px" data-i18n="matriz_sucursal"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="cboTipoCliente" runat="server"
                                                AutoCompleteMode="SuggestAppend" AutoPostBack="True"
                                                DropDownStyle="DropDownList" Font-Names="Arial" Font-Size="8pt"
                                                ItemInsertLocation="Append" AppendDataBoundItems="true"
                                                OnSelectedIndexChanged="cboTipoCliente_SelectedIndexChanged" TabIndex="4"
                                                Width="220px">
                                                <asp:ListItem>Matriz</asp:ListItem>
                                                <asp:ListItem>Sucursal</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: left;" colspan="1">
                                            <asp:CheckBox ID="chkReclamo" runat="server" AutoPostBack="True"
                                                OnCheckedChanged="chkReclamo_CheckedChanged" Style="color: #000000"
                                                Text="" TextAlign="Left"  />
                                            <label data-i18n="reclamo_curso"></label>
                                        </td>
                                        <td>&nbsp;<asp:TextBox ID="txtReclamo" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            ForeColor="Black" Height="30px" OnTextChanged="cli_direccion_TextChanged"
                                            TabIndex="2" TextMode="MultiLine" Visible="False" Width="200px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblPaiCliMat" runat="server" Font-Bold="true"
                                                Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Height="16px"
                                                Text="Pais Matriz" Visible="False" data-i18n="pais_matriz" Width="70px" Style="font-weight: 700"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="CboPaisMatriz" runat="server"
                                                AutoCompleteMode="SuggestAppend" AutoPostBack="True" AppendDataBoundItems="true"
                                                DropDownStyle="DropDownList" Font-Names="Arial" Font-Size="8pt"
                                                OnSelectedIndexChanged="CboPaisMatriz_SelectedIndexChanged" TabIndex="16"
                                                Visible="False" Width="220px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lbl_EmpreCompete" runat="server" Font-Bold="False"
                                                Font-Names="Arial" Font-Overline="False" Font-Size="8pt" ForeColor="Black"
                                                Style="text-align: right; margin-right: 0px;" Height="16px"
                                                Text="Empresa Competencia:" Width="120px" data-i18n="empresa_comp"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chk_EmpreCompe" AutoPostBack="true" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblCiuCliMat" runat="server" Font-Bold="True"
                                                Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                Text="Ciudad Matriz" Visible="False" data-i18n="ciudad_matriz" Width="80px"></asp:Label>
                                        </td>
                                        <td class="style115">
                                            <asp:DropDownList ID="cboCiudadMatriz" runat="server" AppendDataBoundItems="true"
                                                AutoCompleteMode="SuggestAppend" AutoPostBack="True"
                                                DropDownStyle="DropDownList" Font-Names="Arial" Font-Size="8pt"
                                                OnSelectedIndexChanged="cboCiudadMatriz_SelectedIndexChanged" TabIndex="17"
                                                Visible="False" Width="205px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: right">&nbsp;</td>
                                        <td>&nbsp;
                                    <asp:RegularExpressionValidator ID="REVMail1" runat="server"
                                        ControlToValidate="cli_mail" ErrorMessage="RegularExpressionValidator"
                                        Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="#CC3300"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        Width="300px">Formato e-mail incorrecto,ejemplo. email@dominio.xx</asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblClienteMat" runat="server" Font-Bold="True"
                                                Font-Names="Arial" Font-Size="8pt" Font-Underline="False" ForeColor="Black"
                                                Style="text-align: right" Text="Cliente Matriz" data-i18n="cliente_matriz" Visible="False" Width="80px"></asp:Label>
                                        </td>
                                        <td class="style115">
                                            <asp:DropDownList ID="cboClienteMatriz" runat="server" AppendDataBoundItems="true"
                                                AutoCompleteMode="SuggestAppend" AutoPostBack="True"
                                                DropDownStyle="DropDownList" Font-Names="Arial" Font-Size="8pt" TabIndex="18"
                                                Visible="False" Width="300px">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="text-align: right;">&nbsp;</td>
                                        <td>
                                            <asp:Button ID="btnGuardar" runat="server" BackColor="#1C5AB6"
                                                BorderColor="#999999" CssClass="botonsio" Font-Bold="True" Font-Names="Arial"
                                                Font-Size="8pt" ForeColor="White" OnClick="btnGuardar_Click"
                                                OnClientClick="return confirm('Desea Enviar El Formulario')" TabIndex="15"
                                                Text="Guardar" Width="80px" data-i18n="[value]guardar" Visible="False"/>
                                            &nbsp;&nbsp;<asp:Button ID="btnNuevo" runat="server" BackColor="#1C5AB6"
                                                BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="White" OnClick="btnNuevo_Click" Text="Nuevo" data-i18n="[value]nuevo" Width="80px" Visible="False" />
                                            &nbsp;
                                    <asp:Button ID="btnEliminar" runat="server" BackColor="#1C5AB6"
                                        BorderColor="#999999" CssClass="botonsio" Font-Bold="True" Font-Names="Arial"
                                        Font-Size="8pt" ForeColor="White" OnClick="btnEliminar_Click"
                                        OnClientClick="return confirm('Desea Enviar El Formulario')" TabIndex="15"
                                        Text="Eliminar" Width="80px"  data-i18n="[value]eliminar" Visible="False"/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:LinkButton ID="lkVerPanel" runat="server" OnClick="lkVerPanel_Click"
                                                Font-Overline="False" Font-Size="8pt" data-i18n="[html]tipo_cliente" Font-Underline="True">Tipo Cliente</asp:LinkButton>
                                            &nbsp;&nbsp;</td>
                                        <td class="style115">&nbsp;</td>
                                        <td class="style109">
                                            <asp:Label ID="lblUsuarioact1" runat="server" Font-Bold="False"
                                                Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Height="16px"
                                                Style="margin-top: 3px" Text="Actualiza:" data-i18n="actualiza"  Width="90px"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="txtUsuarioAct0" runat="server" Font-Bold="False"
                                                Font-Names="Arial" Font-Size="8pt" Style="margin-top: 3px" Width="120px"></asp:Label>
                                            <asp:Label ID="txtFechaAct0" runat="server" Font-Bold="False"
                                                Font-Names="Arial" Font-Size="8pt" Style="margin-top: 3px" Width="70px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center" colspan="4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    
                                        </td>
                                    </tr>
                                </table>
                                <asp:Panel ID="pnlTipoCli" runat="server" Width="430px" Visible="false"
                                    BorderStyle="None">
                                    <table>
                                        <tr>
                                            <td style="text-align: left" class="style110">
                                                <asp:Label ID="lblPaiCliMat1" runat="server" Font-Bold="False"
                                                    Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Height="16px"
                                                    Style="text-align: right" Text="Planta" data-i18n="planta" Visible="False" Width="70px"></asp:Label>
                                                &nbsp; &nbsp;
                                            </td>
                                            <td class="style110" style="text-align: left">
                                                <asp:DropDownList ID="cboPlanta" runat="server" AutoCompleteMode="SuggestAppend" AppendDataBoundItems="true"
                                                    AutoPostBack="True" DropDownStyle="DropDownList" Font-Names="Arial"
                                                    Font-Size="8pt" ItemInsertLocation="Append"
                                                    OnSelectedIndexChanged="cboPlanta_SelectedIndexChanged" TabIndex="4"
                                                    Visible="False" Width="60px">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="style110" style="text-align: left">
                                                <asp:Label ID="lblPaiCliMat0" runat="server" Font-Bold="False"
                                                    Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Height="16px"
                                                    Text="Tipo Cliente" Visible="False" data-i18n="tipo_cliente" Width="60px"></asp:Label>
                                            </td>
                                            <td class="style110" style="text-align: left">
                                                <asp:DropDownList ID="cboTipoClientePlanta" runat="server" AppendDataBoundItems="true"
                                                    AutoCompleteMode="SuggestAppend" AutoPostBack="True"
                                                    DropDownStyle="DropDownList" Font-Names="Arial" Font-Size="8pt"
                                                    ItemInsertLocation="Append" TabIndex="4" Visible="False" Width="80px">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="style110" style="text-align: left">
                                                <asp:Button ID="btnActualizarTipo" runat="server" BackColor="#1C5AB6"
                                                    BorderColor="#999999" CssClass="botonsio" Font-Bold="True" Font-Names="Arial"
                                                    Font-Size="8pt" ForeColor="White" OnClick="btnActualizarTipo_Click"
                                                    OnClientClick="return confirm('Desea Enviar El Formulario')" TabIndex="15"
                                                    Text="Guardar" Visible="False" Width="70px" data-i18n="[value]guardar"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style110" style="text-align: right" colspan="5">
                                                <asp:GridView ID="grvTipoCliente" runat="server" AllowSorting="True"
                                                    AutoGenerateColumns="False" CellPadding="4" DataKeyNames="id"
                                                    Font-Names="Arial" Font-Size="8pt" ForeColor="#333333" GridLines="None"
                                                    OnSelectedIndexChanged="GridView1_SelectedIndexChanged"
                                                    Style="text-align: left" Visible="False" Width="410px">
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <Columns>
                                                        <asp:CommandField ShowSelectButton="True" />
                                                        <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False"
                                                            ReadOnly="True" SortExpression="id" />
                                                        <asp:BoundField DataField="Planta" HeaderText="Planta"
                                                            SortExpression="Planta" />
                                                        <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" />
                                                    </Columns>
                                                    <EditRowStyle BackColor="#999999" />
                                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" />
                                                    <HeaderStyle BackColor="Silver" Font-Bold="True" Font-Names="Arial"
                                                        Font-Size="8pt" ForeColor="Black" />
                                                    <PagerStyle BackColor="#1C5AB6" ForeColor="White" HorizontalAlign="Center" />
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                </asp:GridView>


                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <contenttemplate>
            <table>
                <tr>
                <td style="text-align: right" class="style62">
                                                 <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                                <ProgressTemplate>
                                                    <img alt="" src="Imagenes/Indicator.gif" height="20" style="text-align: center; float: right;" width="20"/>
                                                </ProgressTemplate>

                                                </asp:UpdateProgress>
                                                    &nbsp;</td>
                                                <td style="text-align: right">
                    <td>
                        <rsweb:ReportViewer ID="ReporteVerHV" runat="server" Width="" 
                        BackColor="#EBEBEB" BorderColor="#EBEBEB" Height="" 
                        AsyncRendering="False" ShowFindControls="False" ShowPrintButton="False" 
                            ShowPromptAreaButton="False" ShowZoomControl="False" 
                            SizeToReportContent="True">
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
        </contenttemplate>

                            </td>
                        </tr>

                    </table>
        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>

