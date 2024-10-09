<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="GrupoApoyo.aspx.cs" Inherits="SIO.GrupoApoyo" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

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
        .CustomComboBoxStyle .ajax__combobox_buttoncontainer button 
        {
            background-image:url('Imagenes/toolkit-arrow.png');
            border-style:none;            
        }
        .CustomComboBoxStyle .ajax__combobox_textboxcontainer input 
        {
            background-image:url('Imagenes/toolkit-bg.png');
            border-style:none;  
        }
        .CustomComboBoxStyle .ajax__combobox_itemlist li
        {
            color: Black;  
            font-size:8pt;  
            font-family:Arial; 
            background-color:#EBEBEB;  
        }        
        .A:hover { background:white } 
        .botonsio:hover 
        { 
           color: white; background:blue             
        } 
        .center
        {
            font-family: Arial;
            font-size: 8pt;
            Text-Align:Center; 
        }
        .sangria
        {
            word-spacing: 10pt;
            font-family: Tahoma;
            font-size: 11pt;
            color: #1C5AB6;
        margin-bottom: 0px;
    }
        .style84
    {
        text-align: right;
        }
        .style108
    {
        width: 130px;
    }
        .style112
        {
            height: 374px;
        }
        </style>            

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="lblClienteTitulo" runat="server" 
                        Font-Bold="True" Font-Names="Tahoma" Font-Size="9pt" ForeColor="#1C5AB6" 
                        Text="GRUPO DE APOYO - " Width="120px" style="text-align: right"></asp:Label>
                    <asp:Label ID="lblClientep" runat="server" Font-Bold="True" Font-Names="Tahoma" 
                        Font-Size="9pt" ForeColor="#1C5AB6" style="text-align: left" Width="600px"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
                <td class="style84">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="4" class="style112">
                    
                        <table style="height: 330px; width: 990px;">
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td rowspan="3" style="text-align: right">
                                    <asp:Label ID="lblBusq" runat="server" Font-Bold="False" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="Black" Height="16px" Text="Busqueda" Width="60px"></asp:Label>
                                </td>
                                <td colspan="5" style="text-align: right">
                                    <asp:LinkButton ID="LkverHV2" runat="server" BackColor="#1C5AB6" 
                                        Font-Names="Arial" Font-Size="8pt" Font-Underline="False" ForeColor="White" 
                                        PostBackUrl="~/VerGrupoApoyo.aspx" style="text-align: center;" 
                                        Width="83px">Consultar Grupos Apoyo</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtBusqueda" runat="server" AutoPostBack="True" 
                                        BackColor="#FFFF66" ontextchanged="txtBusqueda_TextChanged"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td colspan="3">
                                    <asp:ComboBox ID="cboBusqClientes1" runat="server" AutoCompleteMode="Suggest" 
                                        AutoPostBack="True" BackColor="#DDE6F7" DataTextField="cli_nombre" 
                                        Font-Names="Arial" Font-Size="8pt" ForeColor="Black" MaxLength="0" 
                                        onselectedindexchanged="cboBusqClientes1_SelectedIndexChanged" style="display: inline;" 
                                        Width="455px">
                                    </asp:ComboBox>
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
                                    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" 
                                        Font-Size="8pt" Text="."></asp:Label>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblCliente" runat="server" Font-Bold="False" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="Black" Height="16px" Text="Empresa *" 
                                        Width="60px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="cli_nombre" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        ForeColor="Black" ontextchanged="cli_nombre_TextChanged" TabIndex="1" 
                                        Width="340px"></asp:TextBox>
                                </td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblTelef1" runat="server" Font-Bold="False" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="Black" Text="Teléfono 1 *" Width="60px"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="preTel" runat="server" CssClass="center" Font-Bold="True" 
                                        Font-Names="Arial" Font-Size="8pt" Text="0" Width="20px"></asp:Label>
                                    <asp:TextBox ID="prefijo1" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        ForeColor="Black" ontextchanged="cli_telefono_TextChanged" TabIndex="7" 
                                        Width="25px"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="cli_telefono" runat="server" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="Black" ontextchanged="cli_telefono_TextChanged" 
                                        TabIndex="8" Width="160px"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblDireccion" runat="server" Font-Bold="False" 
                                        Font-Names="Arial" Font-Size="8pt" Text="Dirección Fiscal *" Width="85px" 
                                        ForeColor="Black"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="cli_direccion" runat="server" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="Black" Height="30px" 
                                        ontextchanged="cli_direccion_TextChanged" TextMode="MultiLine" 
                                        Width="340px" TabIndex="2"></asp:TextBox>
                                </td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblTelef2" runat="server" Font-Bold="False" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="Black" Text="Teléfono 2" Width="60px"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="preTel2" runat="server" CssClass="center" Font-Bold="True" 
                                        Font-Names="Arial" Font-Size="8pt" Text="0" Width="20px"></asp:Label>
                                    <asp:TextBox ID="prefijo2" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        ForeColor="Black" ontextchanged="cli_telefono_TextChanged" TabIndex="9" 
                                        Width="25px"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="cli_telefono_2" runat="server" Font-Names="Arial" 
                                        Font-Size="8pt" TabIndex="10" Width="160px"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td style="text-align: center">
                                    <asp:Label ID="lblCrear" runat="server" ForeColor="Gray" 
                                        style="text-align: center" Text="Crear:" Width="85px" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblNIT" runat="server" Font-Bold="False" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="Black" Text="NIT" Width="40px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="cli_nit" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        TabIndex="3" Width="232px"></asp:TextBox>
                                </td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblFAX" runat="server" Font-Bold="False" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="Black" Text="FAX" Width="20px"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="preTel3" runat="server" CssClass="center" Font-Bold="True" 
                                        Font-Names="Arial" Text="0" Width="20px"></asp:Label>
                                    <asp:TextBox ID="prefijo3" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        ForeColor="Black" TabIndex="11" 
                                        Width="25px"></asp:TextBox>
                                    &nbsp;<asp:TextBox ID="cli_fax" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        TabIndex="12" Width="160px"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <asp:LinkButton ID="lkCreaContacto" runat="server" BackColor="#1C5AB6" 
                                        Font-Names="Arial" Font-Size="8pt" Font-Underline="False" ForeColor="White" 
                                        onclick="lkCreaContacto_Click" style="text-align: center;" 
                                        ToolTip="Crea contacto de este cliente" Visible="False" Width="85px">CONTACTO</asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblPais0" runat="server" Font-Bold="False" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="Black" Text="Tipo Grupo *" Width="90px"></asp:Label>
                                </td>
                                <td>
                                    <asp:ComboBox ID="cboTipoApoyo" runat="server" AutoCompleteMode="SuggestAppend" 
                                        AutoPostBack="True" DropDownStyle="DropDownList" Font-Names="Arial" 
                                        Font-Size="8pt" TabIndex="5" Width="220px">
                                    </asp:ComboBox>
                                </td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblEmail" runat="server" Font-Bold="False" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="Black" Text="E - Mail" Width="40px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="cli_mail" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="margin-left: 0px" TabIndex="13" Width="218px"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblPais" runat="server" Font-Bold="False" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="Black" Text="Pais *" Width="50px"></asp:Label>
                                </td>
                                <td>
                                    <asp:ComboBox ID="cboPais" runat="server" AutoCompleteMode="SuggestAppend" 
                                        AutoPostBack="True" DropDownStyle="DropDownList" 
                                        Font-Names="Arial" Font-Size="8pt" 
                                        onselectedindexchanged="cboPais_SelectedIndexChanged" TabIndex="5" 
                                        Width="220px">
                                    </asp:ComboBox>
                                </td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblWeb" runat="server" Font-Bold="False" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="Black" Height="16px" Text="Web Site" Width="45px"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="cli_web" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                        style="margin-left: 0px; margin-top: 0px" TabIndex="14" Width="218px"></asp:TextBox>
                                </td>
                                <td style="text-align: right">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblCiudad" runat="server" Font-Bold="False" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="Black" Text="Ciudad *" Width="50px"></asp:Label>
                                </td>
                                <td>
                                    <asp:ComboBox ID="cboCiudad" runat="server" AutoCompleteMode="SuggestAppend" 
                                        AutoPostBack="True" DropDownStyle="DropDownList" 
                                        Font-Names="Arial" Font-Size="8pt" TabIndex="6" Width="220px">
                                    </asp:ComboBox>
                                </td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblTipoContri" runat="server" Font-Bold="False" 
                                        Font-Names="Arial" Font-Size="8pt" Height="16px" style="margin-top: 3px" 
                                        Text="Tipo Contribuyente" Width="90px" ForeColor="Black"></asp:Label>
                                </td>
                                <td>
                                    <asp:ComboBox ID="cli_tco_id" runat="server" AutoCompleteMode="SuggestAppend" 
                                        AutoPostBack="True" DropDownStyle="DropDownList" 
                                        Font-Names="Arial" Font-Size="8pt" Width="205px" 
                                        TabIndex="15">
                                    </asp:ComboBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    </td>
                                <td style="text-align: right;">
                                    <asp:Label ID="lblUsuarioact1" runat="server" Font-Bold="False" 
                                        Font-Names="Arial" Font-Size="8pt" Height="16px" style="margin-top: 3px" 
                                        Text="Usuario Actualiza:" Width="90px" ForeColor="Black"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="txtUsuarioAct0" runat="server" Font-Bold="False" 
                                        Font-Names="Arial" Font-Size="8pt" style="margin-top: 3px" Width="120px"></asp:Label>
                                </td>
                                <td class="style108">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td style="text-align: right">
                                    &nbsp;</td>
                                <td style="text-align: right">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td rowspan="2">
                                    &nbsp;</td>
                                <td style="text-align: right;" rowspan="2">
                                    <asp:Label ID="lblUsuarioact2" runat="server" Font-Bold="False" 
                                        Font-Names="Arial" Font-Size="8pt" ForeColor="Black" 
                                        style="margin-top: 3px; text-align: right;" Text="Fecha Act:" Width="90px"></asp:Label>
                                </td>
                                <td rowspan="2">
                                    <asp:Label ID="txtFechaAct0" runat="server" Font-Bold="False" 
                                        Font-Names="Arial" Font-Size="8pt" style="margin-top: 3px" Width="100px"></asp:Label>
                                </td>
                                <td style="text-align: right;" rowspan="2">
                                    <asp:Label ID="lblUsuarioact3" runat="server" Font-Bold="False" 
                                        Font-Names="Arial" Font-Size="8pt" ForeColor="Black" 
                                        style="margin-top: 3px; text-align: right;" Text="Estado" Width="90px"></asp:Label>
                                </td>
                                <td rowspan="2">
                                    <asp:Label ID="txtEstado" runat="server" Font-Bold="False" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="#0000CC" style="margin-top: 3px; font-weight: 700;" 
                                        Width="120px"></asp:Label>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    &nbsp;</td>
                                <td style="text-align: right">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td colspan="2" style="text-align: left">
                                    <asp:RegularExpressionValidator ID="REVMail1" runat="server" 
                                        ControlToValidate="cli_mail" ErrorMessage="RegularExpressionValidator" 
                                        Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="#CC3300" 
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" 
                                        Width="350px">Formato para e-mail incorrecto,ejemplo. email@dominio.xx</asp:RegularExpressionValidator>
                                </td>
                                <td colspan="2" style="text-align: right">
                                    <asp:Button ID="btnGuardar" runat="server" BackColor="#1C5AB6" 
                                        BorderColor="#999999" CssClass="botonsio" Font-Bold="True" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="White" onclick="btnGuardar_Click" 
                                        onclientclick="return confirm('Desea Enviar El Formulario')" TabIndex="15" 
                                        Text="Guardar" Width="80px" />
                                    &nbsp;&nbsp;
                                    <asp:Button ID="btnNuevo" runat="server" BackColor="#1C5AB6" 
                                        BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                        ForeColor="White" onclick="btnNuevo_Click" Text="Nuevo" Width="80px" />
                                </td>
                                <td style="text-align: right">
                                    &nbsp;</td>
                                <td style="text-align: right">
                                    &nbsp;</td>
                            </tr>
                        </table>
                         <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <rsweb:ReportViewer ID="ReporteVerHV" runat="server" Width="" 
                        BackColor="#EBEBEB" BorderColor="#EBEBEB" Height="" 
                        AsyncRendering="False" ShowBackButton="False" ShowFindControls="False" 
                            ShowPageNavigationControls="False" ShowPrintButton="False" 
                            ShowPromptAreaButton="False" ShowZoomControl="False" 
                            SizeToReportContent="True">
                        </rsweb:ReportViewer>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
                   
                </td>
            </tr>
            
        </table>
    </ContentTemplate>

        </asp:UpdatePanel>
    
</asp:Content>
