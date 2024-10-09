<%@ Page Title="Obra" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="Obra.aspx.cs" Inherits="SIO.Obra" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms,Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .sangria {
            word-spacing: 10pt;
            font-family: Tahoma;
            font-size: 11pt;
            color: #1C5AB6;
            text-align: right;
        }

        .center {
            font-family: Arial;
            font-size: 8pt;
            Text-Align: Center;
        }

        .CustomComboBoxStyle .ajax__combobox_buttoncontainer button {
            background-image: url('http://app.forsa.com.co/SIOMaestros/Imagenes/toolkit-arrow.gif');
            border-style: none;
        }

        .CustomComboBoxStyle .ajax__combobox_textboxcontainer input {
            background-image: url('http://app.forsa.com.co/SIOMaestros/Imagenes/toolkit-bg.gif');
            border-style: none;
        }

        .CustomComboBoxStyle .ajax__combobox_itemlist li {
            color: Black;
            font-size: 8pt;
            font-family: Arial;
            background-color: #EBEBEB
        }

        .center {
            font-family: Arial;
            font-size: 8pt;
            Text-Align: Center;
        }

        .botonsio:hover {
            color: white;
            background: blue
        }

        .style95 {
            width: 216px;
        }

        .botonsio {
            font-size: 7pt;
        }

        .style96 {
            width: 77px;
        }

        .style100 {
            width: 185px;
        }

        .style106 {
            width: 137px;
        }

        .style107 {
            text-align: left;
        }

        .style108 {
            width: 37px;
        }

        .style109 {
            width: 21px;
        }

        .style110 {
            width: 54px;
        }

        .style111 {
            width: 17px;
        }

        .auto-style1 {
            width: 1px;
        }
    </style>
    <script type="text/javascript" src="Scripts/jquery.i18n.js"></script>
    <script type="text/javascript" src="Scripts/jquery.i18n.messagestore.js"></script>
    <%--<script type="text/javascript" src="Scripts/jquery.i18n.fallbacks.js"></script>
    <script type="text/javascript" src="Scripts/jquery.i18n.parser.js"></script>
    <script type="text/javascript" src="Scripts/jquery.i18n.emitter.js"></script>--%>
    <script type="text/javascript" src="Scripts/obra.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <table>
                <tr>
                    <td colspan="8">
                        <asp:Label ID="lblObraTitulo" runat="server" Font-Bold="True"
                            Font-Names="Tahoma" Font-Size="9pt" Text="OBRA - " CssClass="sangria"
                            ForeColor="#1C5AB6" Width="110px"></asp:Label>
                        <asp:Label ID="lblObrap" runat="server" Font-Bold="True" Font-Names="Tahoma"
                            Font-Size="9pt" ForeColor="#1C5AB6" Text="OBRA" Width="580px"></asp:Label>
                    </td>
                </tr>
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
                    <td style="text-align: right" class="style106">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblEstado" runat="server" data-i18n="estado_obra"
                            Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                            Style="text-align: left" Text="Estado Obra *" Width="95px"></asp:Label>
                        &nbsp;&nbsp; &nbsp; &nbsp;
                    </td>
                    <td class="style100" style="text-align: right">&nbsp;</td>
                    <td style="text-align: right" class="style111">
                        <asp:Label ID="lblOri" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Height="16px" Style="text-align: center"
                            Text="Origen*" Width="40px" data-i18n="origen"></asp:Label>
                    </td>
                    <td style="text-align: left" class="style110">
                        <asp:DropDownList ID="cbo_Origen" runat="server" AutoCompleteMode="Append" AppendDataBoundItems="true"
                            AutoPostBack="true" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" OnSelectedIndexChanged="cbo_Origen_SelectedIndexChanged"
                            Width="78px">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: right" class="style109">&nbsp;</td>
                    <td style="text-align: right" class="style108">
                        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" data-i18n="fuente"
                            Style="text-align: center; margin-bottom: 0px;" Text="Fuente*" Width="40px"></asp:Label>
                    </td>
                    <td class="style96" style="text-align: left">
                        <asp:DropDownList ID="cbo_Fuente" runat="server" AutoCompleteMode="Append" AppendDataBoundItems="true"
                            AutoPostBack="true" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" OnSelectedIndexChanged="cbo_Fuente_SelectedIndexChanged"
                            Width="300px">
                        </asp:DropDownList>
                    </td>
                    <td class="auto-style1" style="text-align: left">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="8" style="text-align: right">&nbsp;
                       <asp:ImageButton ID="ImageButton1" runat="server"
                           ImageUrl="~/Imagenes/Arrow back.png" OnClick="ImageButton1_Click"
                           Style="text-align: right" ToolTip="Volver a Empresa" Height="31px"
                           Width="31px" />
            </table>
            <table>
                <tr>
                    <td></td>
                    <td style="text-align: right">
                        <asp:Label ID="lblObra" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Obra *"
                            Width="70px" data-i18n="obra"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="obr_nombre" runat="server" Font-Bold="True" Font-Names="Arial"
                            Font-Size="8pt" Width="333px"></asp:TextBox>
                    </td>
                    <td colspan="3" style="text-align: left">
                        <asp:Label ID="lblPaisCliente0" runat="server" Font-Bold="False"
                            Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                            Text="Asociar a Empresa:" data-i18n="asociar_empresa" Width="150px" ForeColor="Black"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: right;">
                        <asp:Label ID="lblDireccion" runat="server" Font-Bold="False"
                            Font-Names="Arial" Font-Size="8pt" Style="text-align: right" Text="Dirección *"
                            Width="70px" ForeColor="Black" data-i18n="direccion"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="obr_direccion" runat="server" Font-Names="Arial"
                            Font-Size="8pt" Height="25px" TextMode="MultiLine" Width="329px"></asp:TextBox>
                    </td>
                    <td style="text-align: right;">
                        <asp:Label ID="lblPaisCliente" runat="server" Font-Bold="False"
                            Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                            Text="Pais " Width="70px" data-i18n="pais"></asp:Label>
                    </td>
                    <td colspan="2" style="text-align: left">
                        <asp:DropDownList ID="CboPaisMatriz" CssClass="cbotranslate" runat="server" AppendDataBoundItems="true"
                            AutoPostBack="True"
                            Font-Names="Arial" Font-Size="8pt"
                            OnSelectedIndexChanged="CboPaisMatriz_SelectedIndexChanged" Width="225px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: right;">
                        <asp:Label ID="lblPais" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Pais Obra *"
                            Width="90px" data-i18n="pais_obra"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="cboPais" runat="server" AppendDataBoundItems="true"
                            AutoPostBack="True" CssClass="cbotranslate"
                            Font-Names="Arial" Font-Size="8pt"
                            OnSelectedIndexChanged="cboPais_SelectedIndexChanged" Width="310px">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: right;">
                        <asp:Label ID="lblCiudadObra" runat="server" Font-Bold="False"
                            Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                            Text="Ciudad " data-i18n="ciudad" Width="70px"></asp:Label>
                    </td>
                    <td colspan="2" style="text-align: left">
                        <asp:DropDownList ID="cboCiudadMatriz" runat="server" AppendDataBoundItems="true"
                            AutoPostBack="True" CssClass="cbotranslate"
                            Font-Names="Arial" Font-Size="8pt"
                            OnSelectedIndexChanged="cboCiudadMatriz_SelectedIndexChanged"
                            Width="225px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: right;">
                        <asp:Label ID="lblCiudad" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                            Text="Ciudad Obra *" data-i18n="ciudad_obra" Width="70px"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:DropDownList ID="cboCiudad" runat="server" AppendDataBoundItems="true"
                            Font-Names="Arial" CssClass="cbotranslate"
                            Font-Size="8pt" Width="310px">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: right;">
                        <asp:Label ID="lblCliente" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" Font-Underline="False" ForeColor="Black"
                            Style="text-align: right" data-i18n="empresa" Text="Empresa" Width="70px"></asp:Label>
                    </td>
                    <td colspan="2" style="text-align: left">
                        <asp:DropDownList ID="cboClienteMatriz" runat="server" AppendDataBoundItems="true"
                            AutoPostBack="True" CssClass="cbotranslate"
                            Font-Names="Arial" Font-Size="8pt" Width="300px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:LinkButton ID="HyperLink2" runat="server" Visible="False" OnClick="HyperLink2_Click" Font-Size="13px">Crear Sucursal</asp:LinkButton>

                    </td>
                </tr>

                <tr>
                    <td>&nbsp;</td>
                    <td style="text-align: right">
                        <asp:Label ID="lblTelefono" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Height="16px" Style="text-align: right"
                            Text="Teléfono 1 *" data-i18n="telefono1" Width="70px"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="preTel" runat="server" CssClass="center" Text="0" Width="20px"></asp:Label>
                        <asp:TextBox ID="txtprefijo1" runat="server" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" TabIndex="15" Width="25px"></asp:TextBox>
                        <asp:TextBox ID="obr_telef" runat="server" Font-Names="Arial" Font-Size="8pt"
                            Width="140px"></asp:TextBox>
                    </td>


                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td style="text-align: right;">
                        <asp:Label ID="lblTeléfono2" runat="server" Font-Bold="False"
                            Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                            Text="Teléfono 2" data-i18n="telefono2" Width="60px"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="preTel2" runat="server" CssClass="center" Text="0" Width="20px"></asp:Label>
                        <asp:TextBox ID="txtprefijo2" runat="server" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" TabIndex="17" Width="25px"></asp:TextBox>
                        <asp:TextBox ID="obr_telef2" runat="server" Font-Names="Arial" Font-Size="8pt"
                            Width="140px"></asp:TextBox>
                    </td>
                    <td style="text-align: right;">
                        <asp:Label ID="lblFechaini" data-i18n="fecha_inicio" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Fecha Inicio"
                            Width="70px"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtFechaInicio" runat="server" Font-Names="Arial"
                            Font-Size="8pt" Style="margin-left: 0px" TabIndex="9" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="txtFechaInicio_CalendarExtender" runat="server"
                            Format="dd/MM/yyyy" TargetControlID="txtFechaInicio">
                        </asp:CalendarExtender>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="lblFechafin" data-i18n="finalizacion" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="text-align: left" Text="Finalizacion"
                            Width="61px"></asp:Label>

                        <asp:TextBox ID="txtFechafin" runat="server" Font-Names="Arial"
                            Font-Size="8pt" Style="margin-left: 0px" TabIndex="9" Width="80px"></asp:TextBox>
                        <asp:CalendarExtender ID="txtFechafin_CalendarExtender" runat="server"
                            Format="dd/MM/yyyy" TargetControlID="txtFechafin">
                        </asp:CalendarExtender>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td style="text-align: right;">&nbsp;</td>
                    <td style="text-align: left">&nbsp;</td>
                    <td>
                        <asp:Label ID="lblComentario" runat="server" Font-Bold="False"
                            Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                            Text="Descripcion:" data-i18n="descripcion" Width="70px"></asp:Label>
                    </td>
                    <td colspan="3" style="text-align: left">&nbsp;<asp:TextBox ID="txtComentario" runat="server" Font-Names="Arial"
                        Font-Size="8pt" Height="35px" TextMode="MultiLine" Width="400px"></asp:TextBox>


                        &nbsp;</td>
                    <td style="text-align: left">&nbsp;</td>
                </tr>

            </table>
            <table>
                <tr>
                    <td>&nbsp;</td>
                    <td style="text-align: right;">
                        <asp:Label ID="lblEstrato" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Estrato *"
                            Width="60px" data-i18n="estrato"></asp:Label>
                    </td>
                    <td style="text-align: left" class="style107">
                        <asp:DropDownList ID="cboEstrato" runat="server" Font-Names="Arial" Font-Size="8pt"
                            Width="125px" CssClass="cbotranslate" AppendDataBoundItems="true">
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td></td>
                    <td style="text-align: right;">
                        <asp:Label ID="lblM2" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                            Text="M² Vivienda *" data-i18n="m2vivienda" Width="80px"></asp:Label>
                        &nbsp;&nbsp;</td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtM2" runat="server" Font-Names="Arial" Font-Size="8pt"
                            Style="text-align: center" Text="0" Width="40px"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td style="text-align: right;">
                        <asp:Label ID="lblVivienda" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" ForeColor="Black" Height="16px"
                            Style="text-align: right; margin-right: 0px;" Text="Tipo Vivienda *"
                            Width="90px" data-i18n="tipo_vivienda"></asp:Label>
                    </td>
                    <td class="style107" style="text-align: left">
                        <asp:DropDownList ID="cboVivienda" runat="server" Font-Names="Arial"
                            Font-Size="8pt" Width="152px" Height="19px" AppendDataBoundItems="true">
                        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td colspan="1"></td>
                    <td style="text-align: right">
                        <asp:Label ID="lblUnd" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                            Text="Unidades A Construir *" data-i18n="unidades_construir" Width="125px"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtUnidades" runat="server" Font-Names="Arial" Font-Size="8pt"
                            Style="text-align: center" Text="0" Width="40px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td style="text-align: right;">
                        <asp:Label ID="Label2" runat="server" Text="Segmento:" data-i18n="segmento" Font-Bold="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" ForeColor="Black" Height="16px"></asp:Label>
                    </td>
                    <td class="style107" style="text-align: left">
                        <asp:DropDownList ID="cbo_Segmento" runat="server" Font-Names="Arial" Font-Size="8pt"
                            Width="153px" AutoPostBack="true" OnTextChanged="cbo_Segmento_TextChanged" AppendDataBoundItems="true">
                        </asp:DropDownList></td>
                    <td style="text-align: right">&nbsp;</td>
                    <td style="text-align: right">
                        <asp:Label ID="LblEstadoObra" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" runat="server" Text="Estado_Obra" data-i18n="estado_obra"></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:CheckBox ID="Chk_EstadoObra" Text="Sin iniciar" ForeColor="Black" runat="server" AutoPostBack="false" />
                        <asp:Label ID="lblEstObra" runat="server" Text="Label" Visible="false"></asp:Label>
                        <td style="text-align: left">&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td style="text-align: right;">
                        <asp:Label ID="Label3" runat="server" Text="Tipo Segmento:" Font-Bold="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" ForeColor="Black" Height="16px" data-i18n="tipo_segmento"></asp:Label></td>
                    <td class="style107" style="text-align: left">
                        <asp:DropDownList ID="cbo_TipoSegmento" runat="server" Font-Names="Arial" Font-Size="8pt"
                            Width="153px">
                        </asp:DropDownList></td>
                    <td style="text-align: right">&nbsp;</td>
                     <td style="text-align: right">
                        <asp:Label ID="LblTipoProyecto" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" runat="server" data-i18n="tipo" Text="Tipo"></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:CheckBox ID="chk_PotenForsa" Text="Potencial Forsa" ForeColor="Black" runat="server" AutoPostBack="true" OnCheckedChanged="chk_PotenForsa_CheckedChanged" />
                        <asp:Label ID="LblVenta" data-i18n="potencial_forsa" runat="server" Text="Label" Visible="false"></asp:Label>
                        &nbsp;
                        <asp:CheckBox ID="chk_PotenArrenda" Text="Potencial Forsa Ply" ForeColor="Black" AutoPostBack="true" OnCheckedChanged="chk_PotenArrenda_CheckedChanged" runat="server" />
                        <asp:Label ID="LblAlquiler" data-i18n="potencial_arr" runat="server" Text="Label" Visible="false"></asp:Label>                      
                    </td>                                    
                </tr>


                 <tr>
                    <td>&nbsp;</td>
                    <td style="text-align: right;">
                        </td>
                    <td class="style107" style="text-align: left">
                        </td>
                    <td style="text-align: right">&nbsp;</td>
                     <td style="text-align: right">            
                    </td>
                    <td style="text-align: left;">
                        <asp:CheckBox ID="chk_PotencialNewDesarrollo" Text="Potencial_N_Desarrollos" ForeColor="Black" runat="server" AutoPostBack="true" OnCheckedChanged="chk_PotencialNewDesarrollo_CheckedChanged" />
                        <asp:Label ID="LblDesarrollo" data-i18n="potencial_N_Desarrollos" runat="server" Text="Label" Visible="false"></asp:Label>
                  
                    </td>                                    
                </tr>
           
                <tr>
                    <td>&nbsp;</td>
                    <td style="text-align: right;">
                        <asp:HyperLink ID="lblLink" runat="server" data-i18n="[html]ficha_proyecto" Font-Names="Arial" Font-Size="14px">Ficha de Proyecto</asp:HyperLink>
                    </td>
                    <td class="style107" style="text-align: right" colspan="5">
                        <asp:Label ID="lblUsuarioact1" runat="server" Font-Bold="False"
                            Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Height="16px"
                            Style="margin-top: 3px" Text="Actualiza:" data-i18n="actualiza" Width="60px"></asp:Label>
                        <asp:Label ID="txtUsuarioAct0" runat="server" Font-Bold="False"
                            Font-Names="Arial" Font-Size="8pt" Style="margin-top: 3px; text-align: left;"
                            Width="184px"></asp:Label>
                        <asp:Label ID="txtFechaAct0" runat="server" Font-Bold="False"
                            Font-Names="Arial" Font-Size="8pt" Height="16px" Style="margin-top: 3px"
                            Width="85px"></asp:Label>
                    </td>
                    <td style="text-align: right">
                        <asp:Button ID="btnGuardar" runat="server" BackColor="#1C5AB6"
                            BorderColor="#999999" CssClass="botonsio" Font-Bold="True" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="White" OnClick="btnGuardar_Click"
                            OnClientClick="return confirm('Desea Enviar El Formulario')" Text="Guardar"
                            Width="70px" data-i18n="[value]guardar" Visible="False" />
                        &nbsp;&nbsp;
                    <asp:Button ID="btnNuevo" runat="server" BackColor="#1C5AB6"
                        BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="White" OnClick="btnNuevo_Click" Text="Nuevo" data-i18n="[value]nuevo" Width="70px" Visible="False" />
                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btn_AnularObra" runat="server" BackColor="#1C5AB6"
                                            BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                            Width="70px" ForeColor="White" Text="Anular" Visible="false"
                                            OnClientClick="return confirm('¿Desea Anular la obra?')"
                                            OnClick="btn_AnularObra_Click" data-i18n="[value]anular" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Label ID="lblUnd0" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                            Text="Descripcion Proyecto "   data-i18n="descripcion_proyecto" Width="110px"></asp:Label>
                    </td>
                    <td colspan="6" style="text-align: left">
                        <asp:Label ID="lblDescripcion" runat="server" Font-Bold="False"
                            Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Height="100%"
                            Style="text-align: left" Width="680px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Label ID="lblUnd1" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                            Text="." Width="89px"></asp:Label>
                    </td>
                    <td colspan="6" style="text-align: left">&nbsp;</td>
                </tr>
            </table>
            </table>
                                                <table>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td colspan="2" style="text-align: right">
                                                            <asp:Label ID="lbl_EmpreCompete" runat="server" Font-Bold="False"
                                                                Font-Names="Arial" Font-Overline="False" Font-Size="8pt" ForeColor="Black"
                                                                Height="16px" Style="text-align: right; margin-right: 0px;"
                                                                Text="Empresa Competencia:" data-i18n="empresa_comp" Visible="False" Width="120px"></asp:Label>
                                                            &nbsp;</td>

                                                        <td style="text-align: left">
                                                            <asp:DropDownList ID="cbo_EmpreCompete" runat="server" Font-Names="Arial" Font-Size="8pt" Visible="False" Width="230px">
                                                            </asp:DropDownList>

                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td colspan="2" style="text-align: right">
                                                            <asp:Label ID="lbl_Evernot" runat="server" Font-Bold="False"
                                                                Font-Names="Arial" Font-Overline="False" Font-Size="8pt" ForeColor="Black"
                                                                Height="16px" Style="text-align: right; margin-right: 0px;"
                                                                Text="Link Evernot:" data-i18n="link_evernot" Width="120px"></asp:Label>
                                                            &nbsp;</td>
                                                        <td style="text-align: left">
                                                            <asp:TextBox ID="txt_Link_Evernot" runat="server" Width="220px"></asp:TextBox>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <asp:Label ID="lbl_TipoSeg" runat="server" Font-Bold="False"
                                                                Font-Names="Arial" Font-Overline="False" Font-Size="8pt" ForeColor="Black"
                                                                Height="16px" Style="text-align: right; margin-right: 0px;"
                                                                Text="Tipo Seguimiento" data-i18n="tipo_seg" Visible="False" Width="90px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:DropDownList ID="cboTipoSeg" AutoPostBack="true" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Visible="False" Width="180px" OnSelectedIndexChanged="cboTipoSeg_SelectedIndexChanged" CssClass="cbotranslate">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="text-align: right">
                                                            <asp:Label ID="lbl_Comentario" runat="server" Font-Bold="False"
                                                                Font-Names="Arial" Font-Overline="False" Font-Size="8pt" ForeColor="Black"
                                                                Height="16px" Style="text-align: right; margin-right: 0px;" Text="Comentario"
                                                                Visible="False" Width="70px" data-i18n="comentario"></asp:Label>
                                                        </td>
                                                        <td colspan="7" style="text-align: left">
                                                            <asp:TextBox ID="txtObservacion" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" Height="51px" TextMode="MultiLine" Visible="False"
                                                                Width="443px"></asp:TextBox>
                                                            &nbsp;&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td></td>
                                                        <td colspan="3">&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td colspan="4" style="text-align: right">
                                                            <asp:Button ID="btnAdicionarSeg" runat="server" BackColor="#1C5AB6"
                                                                BorderColor="#999999" CssClass="botonsio" Font-Bold="True" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="White" OnClick="btnAdicionarSeg_Click"
                                                                OnClientClick="return confirm('Desea Enviar El Formulario')"
                                                                Text="Adicionar Seguimiento" data-i18n="[value]adic_seg" Visible="False" Width="140px" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>


                                                        <td colspan="9" style="text-align: left">
                                                            <rsweb:ReportViewer ID="ReporteVerSegObra" runat="server"
                                                                AsyncRendering="False" BackColor="#EBEBEB" BorderColor="#EBEBEB" Height=""
                                                                ShowFindControls="False" ShowPrintButton="False" ShowPromptAreaButton="False"
                                                                ShowZoomControl="False" SizeToReportContent="True" Width="">
                                                            </rsweb:ReportViewer>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: right;" rowspan="3"></td>
                                                        <td colspan="0" style="text-align: left" rowspan="0"></td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td colspan="3">&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td colspan="4" style="text-align: right">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td colspan="3">&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td colspan="4" style="text-align: right">&nbsp;</td>
                                                    </tr>
                                                </table>
        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>


<%--<asp:GridView ID="GridView_DetalleSeguimiento" runat="server" BackColor="White"
                                                                BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="4"
                                                                Width="930px" AutoGenerateColumns="False" Font-Size="8pt" Font-Names="arial"
                                                                ShowHeaderWhenEmpty="True" DataKeyNames="Id_Seg" OnRowDeleting="GridView_DetalleSeguimiento_RowDeleting" >
                                                                <AlternatingRowStyle BackColor="Gainsboro" />
                                                                <Columns>
                                                                        <asp:TemplateField HeaderText="Id_Seg" HeaderStyle-HorizontalAlign="Center" Visible="false">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txt_IdSeg" runat="server" Width="190px" Text='<%# Bind("Id_Seg") %>' ></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_IdSeg" runat="server" Text='<%# Bind("Id_Seg") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="200px"></HeaderStyle>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Tipo" HeaderStyle-HorizontalAlign="Center">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="txt_Tipo" runat="server" Width="190px" Text='<%# Bind("tipo") %>'></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Tipo" runat="server" Text='<%# Bind("tipo") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="200px"></HeaderStyle>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Observacion" HeaderStyle-HorizontalAlign="Center">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="Txt_Observacion" Width="280px" runat="server" Text='<%# Bind("observacion") %>'></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <HeaderStyle Width="900px"></HeaderStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("observacion") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Fecha" HeaderStyle-HorizontalAlign="Center">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="Txt_Fecha" Width="90px" runat="server" AutoPostBack="false" Text='<%# Bind("fecha","{0:d}") %>'></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <HeaderStyle Width="90px"></HeaderStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label53" runat="server" Text='<%# Bind("fecha","{0:d}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Usuario"  HeaderStyle-HorizontalAlign="Center">
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="Txt_Usuario" Width="140px" runat="server" AutoPostBack="false" Text='<%# Bind("usuario") %>'></asp:TextBox>
                                                                        </EditItemTemplate>
                                                                        <HeaderStyle Width="140px"></HeaderStyle>
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="Label33" runat="server" Text='<%# Bind("usuario") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle HorizontalAlign="left" />                                                                        
                                                                    </asp:TemplateField>                                                           
                                                                    <asp:TemplateField ShowHeader="False" HeaderText="Eliminar">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btn_EliminarSeguimiento" Text="Eliminar" runat="server" ForeColor="Black" CommandName="delete" OnClientClick="return confirmarEliminacion();"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="20px"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EditRowStyle HorizontalAlign="Left" />
                                                                <EmptyDataRowStyle BorderStyle="Solid" />
                                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                <HeaderStyle BackColor="#1C5AB6" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                                <RowStyle BackColor="#EEEEEE" ForeColor="Black" HorizontalAlign="Left" />
                                                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                                <SortedDescendingHeaderStyle BackColor="#000065" />
                                                            </asp:GridView> --%>       
