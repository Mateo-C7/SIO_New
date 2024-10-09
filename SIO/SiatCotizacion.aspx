<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="SiatCotizacion.aspx.cs" Inherits="SIO.SiatCotizacion" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"  namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/ScriptSIAT.js" type="text/javascript"></script>
    <link href="Styles/StyleGeneral.css" rel="stylesheet" type="text/css" />
    <link href="Styles/StyleSIAT.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function listaPart(source, eventArgs) {
            document.getElementById('<%= lblIdPart.ClientID %>').value = eventArgs.get_value();
        }

    </script>
    <style type="text/css">
        .style5 {
            width: 182px;
        }

        .style28 {
            height: 45px;
            width: 182px;
        }

        .style29 {
            text-align: right;
            width: 86px;
        }

        .auto-style1 {
            text-align: right;
            height: 21px;
        }

        .auto-style2 {
            text-align: left;
            height: 21px;
        }
        .auto-style3 {
            width: 357px;
        }
        .auto-style4 {
            text-align: right;
            width: 357px;
        }
        .auto-style5 {
            width: 600px;
        }
        .auto-style6 {
            text-align: left;
            width: 319px;
        }
        .auto-style7 {
            width: 543px;
        }
        .auto-style8 {
            font-size: 8pt;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="1000px">
                <tr>
                    <td>
                        <table width="1000px">
                            <tr>
                                <td width="700px">
                                    <asp:Label ID="lblCotizacion" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label></td>
                                <td width="300px">
                                    <asp:Panel ID="PanelBuscar" runat="server" CssClass="Letra" GroupingText="Buscar">
                                        <table>
                                            <tr>
                                                <td class="auto-style1">CP-
                                                </td>
                                                <td class="auto-style2">
                                                    <asp:TextBox ID="txtCotizacion" runat="server" CssClass="TexboxP"
                                                        AutoPostBack="false" style="text-align: right"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="1000px">
                            <tr>
                                <td>
                                    <asp:Panel ID="PanelCliente" runat="server" CssClass="Letra" GroupingText="Cliente">
                                        <table>
                                            <tr>
                                                <td class="TexDer">(*) Servicio :
                                                </td>
                                                <td class="TexIzq">
                                                    <asp:DropDownList ID="cboServicio" runat="server" CssClass="auto-style8" AutoPostBack="true" OnTextChanged="cboServicio_TextChanged" Height="16px" Width="272px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="TexDer">(*) Cliente :
                                                </td>
                                                <td class="TexIzq">
                                                    <asp:TextBox ID="txtCliente" runat="server" CssClass="TexboxG" OnTextChanged="txtCliente_TextChanged"
                                                        AutoPostBack="true"></asp:TextBox>
                                                    <asp:AutoCompleteExtender ID="txtCliente_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                                        CompletionSetCount="15" EnableCaching="true" Enabled="True" MinimumPrefixLength="1"
                                                        ServiceMethod="GetListaClienteSIAT" UseContextKey="true" ServicePath="~/WSSIO.asmx"
                                                        TargetControlID="txtCliente" OnClientItemSelected="listaClienteSIAT">
                                                    </asp:AutoCompleteExtender>
                                                    <input id="lblIdCliente" runat="server" type="hidden" />
                                                    <input id="lblIdMoneda" runat="server" type="hidden" />
                                                    <input id="lblIdPais" runat="server" type="hidden" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style29">Obra :
                                                </td>
                                                <td class="TexIzq">
                                                    <asp:DropDownList ID="cboObra" runat="server" CssClass="ComboG" AutoPostBack="true"
                                                        OnSelectedIndexChanged="cboObra_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnAggObra" runat="server" Text=">" CssClass="stylebtnAggN" OnClick="btnAggObra_Click" />
                                                    <br />
                                                    <asp:Button ID="btnEliObra" runat="server" Text="<" CssClass="stylebtnAggN" OnClick="btnEliObra_Click" />
                                                </td>
                                                <td class="TexIzq">
                                                    <asp:ListBox ID="listObra" runat="server" Width="374px" Font-Size="8pt" Height="50px"
                                                        Style="overflow-x: auto;"></asp:ListBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style29">Orden :
                                                </td>
                                                <td class="TexIzq">
                                                    <asp:DropDownList ID="cboOF" runat="server" CssClass="ComboP" AutoPostBack="true" OnTextChanged="cboOF_TextChanged">
                                                    </asp:DropDownList>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="LblOfGarantia" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnAggOF" runat="server" Text=">" CssClass="stylebtnAggN" OnClick="btnAggOF_Click" />
                                                    <br />
                                                    <asp:Button ID="btnEliOF" runat="server" Text="<" CssClass="stylebtnAggN" OnClick="btnEliOF_Click" />
                                                </td>
                                                <td class="TexIzq">
                                                    <asp:ListBox ID="listOF" runat="server" Width="104px" Height="50px" Font-Size="8pt"></asp:ListBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="1000px">
                            <tr valign="top">
                                <td class="auto-style5">
                                    <asp:Panel ID="PanelTecnico" runat="server" CssClass="Letra" GroupingText="Técnico" Width="570px">
                                        <table style="width: 571px">
                                            <tr>                                                
                                                <td class="auto-style7">
                                                    <table>
                                                        <tr>
                                                            <td class="TexDer">(*) Días :
                                                            </td>
                                                            <td class="TexIzq">
                                                                <asp:TextBox ID="txtDias" runat="server" Width="20px" AutoPostBack="true" Text="0" OnTextChanged="txtDias_TextChanged" Style="text-align: right">
                                                                </asp:TextBox>
                                                            </td>
                                                            <td class="TexDer">(*) Cant. Téc. :
                                                            </td>
                                                            <td class="TexIzq">
                                                                <asp:TextBox ID="txtTecnicos" runat="server" Width="20px" AutoPostBack="true" Text="1" OnTextChanged="txtTecnicos_TextChanged" Style="text-align: right">
                                                                </asp:TextBox>
                                                            </td>                                                                                                                                                                                                                                                                                         
                                                                      <td>                                                 
                                                        <asp:CheckBox ID="chksFacturable" Text="Facturable" AutoPostBack="true" runat="server" />                                                                                                        
                                                </td>                                                                                           
                                                        </tr>
                                                        <tr>
                                                            <td class="TexDer">Serv. Día :
                                                            </td>
                                                            <td class="TexIzq">
                                                                <asp:TextBox ID="txtHonorarios" runat="server" Width="80px" AutoPostBack="true" Text="0" OnTextChanged="txtHonorarios_TextChanged" Style="text-align: right">
                                                                </asp:TextBox> 
                                                            </td>
                                                            <td class="TexDer">Tiq. Téc. :
                                                            </td>
                                                            <td class="TexIzq">
                                                                <asp:TextBox ID="txtTiquetes" runat="server" Width="80px" AutoPostBack="true" Text="0" OnTextChanged="txtTiquetes_TextChanged" Style="text-align: right">
                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="TexDer">Total Serv. Téc. :
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblTotalTecnico" runat="server" Text="0"></asp:Label>
                                                            </td>
                                                            <td class="TexDer">Total Tiq. :
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblTotalTiquete" runat="server" Text="0"></asp:Label>
                                                            </td>
                                                            <td class="TexDer">Total :
                                                            </td>
                                                            <td class="TexIzq">
                                                                <asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label>
                                                            </td>
                                                            <td class="TexIzq">
                                                                <asp:Label ID="lblMoneda" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr valign="top">
                                                <td class="auto-style7">
                                                    <asp:Label runat="server" ID="lblObservacion" Text="Observación: "></asp:Label>
                                                    <asp:TextBox runat="server" ID="txtObservacion" Width="480px" Height="40px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                                <td>
                                    <asp:Panel ID="panelCorreo" runat="server" CssClass="Letra" GroupingText="Correo" Visible="false" Width="350px">
                                        <table>
                                            <tr>
                                                <td class="auto-style3">
                                                    <table>
                                                        <tr id="trCorreos" runat="server">
                                                            <td class="TexDer">Correo: </td>
                                                            <td class="auto-style6">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="txtPart" runat="server" AutoPostBack="true" CssClass="TexboxM" OnTextChanged="txtPart_TextChanged"></asp:TextBox>
                                                                            <asp:AutoCompleteExtender ID="txtExtender_Part" runat="server" CompletionSetCount="15" DelimiterCharacters="" EnableCaching="true" Enabled="True" MinimumPrefixLength="1" OnClientItemSelected="listaPart" ServiceMethod="GetListaParticipantes" ServicePath="~/WSSIO.asmx" TargetControlID="txtPart" UseContextKey="true">
                                                                            </asp:AutoCompleteExtender>
                                                                            <input id="lblIdPart" runat="server" type="hidden" />
                                                                        </td>
                                                                        <td>
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Button ID="btnAggProces" runat="server" CssClass="stylebtnAggN" OnClick="btnAggProces_Click" Text="&gt;" />
                                                                                        <br />
                                                                                        <asp:Button ID="btnEliProces" runat="server" CssClass="stylebtnAggN" OnClick="btnEliProces_Click" Text="&lt;" />
                                                                                        <br />
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:ListBox ID="listProces" runat="server" Font-Size="8pt" Height="46px" Width="100px"></asp:ListBox>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style4">
                                                    <asp:Button ID="btnCorreo" runat="server" Text="Enviar" OnClick="btnCorreo_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="1000px">
                            <tr>
                                <td>
                                    <asp:Panel ID="PanelAprobacion" runat="server" CssClass="Letra" Visible="true" GroupingText="Aprobación" Enabled="false">
                                        <table>
                                            <tr>
                                                <td class="TexDer">
                                                    <asp:CheckBox ID="chkAprobar" runat="server" Text="Aprobar" AutoPostBack="true" Visible="true" OnCheckedChanged="chkAprobar_CheckedChanged" />
                                                </td>
                                                <td class="TexIzq">
                                                    <asp:CheckBox ID="chkRechazar" runat="server" Text="Rechazar" AutoPostBack="true" Visible="true" OnCheckedChanged="chkRechazar_CheckedChanged" />
                                                </td>
                                            </tr>
                                            <tr id="trMotivo">
                                                <td class="TexDer">
                                                    <asp:Label ID="lblMotivo" runat="server" Visible="false" Text="(*) Motivo: "></asp:Label>
                                                </td>
                                                <td class="TexDer">
                                                    <asp:TextBox ID="txtMotivo" runat="server" Visible="false" Wrap="true" Rows="4" Columns="6" Width="400px" Height="100px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="PanelBotones" runat="server" CssClass="Letra">
                                        <table>
                                            <tr>
                                                <td class="TexDer">
                                                    <asp:Button ID="btnCotizar" runat="server" Text="Cotizar" OnClick="btnCotizar_Click" />
                                                </td>
                                                <td class="TexDer">
                                                    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" OnClick="btnActualizar_Click" />
                                                </td>
                                                <td class="TexDer">
                                                    <asp:Button ID="btnLimpiar" runat="server" Text="Nuevo" OnClick="btnLimpiar_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                
                <tr>
                    <td>
                        <asp:Panel ID="PanelCarta" runat="server" CssClass="Letra" Font-Names="Arial" Width="1000px" Visible="false">
                            <table>
                                <tr>
                                    <td>
                                        <rsweb:ReportViewer ID="reporteCarta" runat="server" ShowParameterPrompts="False" BackColor="White" Width="1000" AsyncRendering="False">
                                        </rsweb:ReportViewer>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div class="overlay" />
            <div class="overlayContent">
                <asp:Label ID="lblEnviando" runat="server" Text="Cargando..." Font-Names="Arial"
                    Font-Size="14pt"></asp:Label>
                <img src="Imagenes/ajax-loader.gif" alt="Loading" height="30" style="text-align: center"
                    width="30" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
