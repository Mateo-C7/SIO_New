<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="PedidoVenta.aspx.cs" Inherits="SIO.PedidoVenta" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"  namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%--<%@ Register Src="ucMultiFileUpload.ascx" TagName="ucMultiFileUpload" TagPrefix="uc1" %>--%>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<%@ Register Src="ucMultiFileUpload.ascx" TagName="ucMultiFileUpload" TagPrefix="uc1" %>--%>
    <script type="text/javascript">

        function alerta(numero) {

            alert('Se ha presionado el boton: ' + numero);
        }
    </script>

    <%--<%@ Register Src="ucMultiFileUpload.ascx" TagName="ucMultiFileUpload" TagPrefix="uc1" %>--%><%--<%@ Register Src="ucMultiFileUpload.ascx" TagName="ucMultiFileUpload" TagPrefix="uc1" %>--%>
    <script type="text/javascript">
        function Navigate() {
            javascript: window.open("SolicitudFacturacionNew.aspx");
        }       
    </script>

     <script type="text/javascript">
        function NavigateEstadoPv() {
            javascript: window.open("VerEstadoPv.aspx");
        }       
    </script>

     <script type="text/javascript">
         function NavigateCartaPv() {
            javascript: window.open("VerCartaPv.aspx");
        }
    </script>

    <script type="text/javascript">
        function DisplayFullImage(ctrlimg) {
            txtCode = "<HTML><HEAD>"
            + "</HEAD><BODY><CENTER>"
            + "<IMG src='" + ctrlimg.src + "'NAME=FullImage "
            //+ "onload='window.resizeTo(document.FullImage.width,document.FullImage.height)'>"
            + "</CENTER>"
            + "</BODY></HTML>";
            mywindow = window.open('', 'image', "width=500,height=500,scrollbars=NO");
            mywindow.document.open();
            mywindow.document.write(txtCode);
            mywindow.document.close();
        }

    </script>

    <%--<%@ Register Src="ucMultiFileUpload.ascx" TagName="ucMultiFileUpload" TagPrefix="uc1" %>--%><%--<%@ Register Src="ucMultiFileUpload.ascx" TagName="ucMultiFileUpload" TagPrefix="uc1" %>--%><%--<script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script type="text/javascript" src="Scripts/jQuery.MultiFile.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery-1.4.1.min.js"></script>--%>

    <style type="text/css">
        /* Accordion */
        .accordionHeader {
            border: 2px Outset #EBEBEB;
            color: white;
            background-color: #1C5AB6;
            font-family: Arial, Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
        }

        #master_content .accordionHeader a {
            color: #FFFFFF;
            background: none;
            text-decoration: none;
        }

            #master_content .accordionHeader a:hover {
                background: none;
                text-decoration: underline;
            }

        .accordionHeaderSelected {
            border: 2px Outset #EBEBEB;
            color: white;
            background-color: #1C5AB6;
            font-family: Arial, Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
        }

        #master_content .accordionHeaderSelected a {
            color: #FFFFFF;
            background: none;
            text-decoration: none;
        }

            #master_content .accordionHeaderSelected a:hover {
                background: none;
                text-decoration: underline;
            }

        .accordionContent {
            border: 0px outset #2F4F4F;
            border-top: none;
            padding: 5px;
            padding-top: 10px;
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
            background-color: #EBEBEB;

        }
        .CustomComboBoxStyle {
            text-align: left;
        }
        
        .style9 {
            width: 152px;
        }

        .auto-style2 {
            width: 127px;
        }

        .auto-style3 {
            height: 18px;
        }
        .auto-style5 {
            text-align: right;
        }
        .auto-style7 {
            text-align: right;
            height: 26px;
        }
        .auto-style8 {
            width: 99px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server" >

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" enctype="multipart/form-data">
        <%--<Triggers>
            <asp:PostBackTrigger ControlID="FileUpload1" />        
        </Triggers>--%>
        <ContentTemplate>
            <table style="text-align: center" width="800">
                <tr>
                    <td >
                        <asp:Panel ID="pnlDatosGenerales" runat="server" 
                            Font-Names="Arial" Font-Size="8pt" GroupingText="Datos Generales" 
                            Width="540px" ForeColor="Black">
                            <table >
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblPlanta" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Planta" Width="50px" style="text-align: right" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                          <asp:ComboBox ID="cboPlanta" runat="server" AutoPostBack="True" 
                                             DropDownStyle="DropDownList" Font-Names="Arial" 
                                            Font-Size="8pt" Width="280px" BackColor="#FFFF66"
                                            AutoCompleteMode="SuggestAppend">
                                        </asp:ComboBox>
                                    </td>
                                    <td style="text-align: left" class="style9">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblPais" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Pais" Width="50px" style="text-align: right" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                            <asp:ComboBox ID="cboPais" runat="server" AutoPostBack="True" 
                                             DropDownStyle="DropDownList" Font-Names="Arial" 
                                            Font-Size="8pt" Width="280px"  
                                            onselectedindexchanged="cboPais_SelectedIndexChanged" 
                                            AutoCompleteMode="SuggestAppend">
                                        </asp:ComboBox>
                                    </td>
                                    <td style="text-align: left" class="style9">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                    <asp:Label ID="lblCiudad" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Ciudad" Width="42px" style="text-align: right" ForeColor="Black"></asp:Label>
                                    
                                        &nbsp;</td>
                                    <td style="text-align: left">
                                    <asp:ComboBox ID="cboCiudad" runat="server" AutoPostBack="True" 
                                             DropDownStyle="DropDownList" Font-Names="Arial" 
                                            Font-Size="8pt" Width="280px"  
                                            onselectedindexchanged="cboCiudad_SelectedIndexChanged" 
                                            AutoCompleteMode="SuggestAppend">
                                        </asp:ComboBox>
                                        &nbsp;</td>
                                    <td style="text-align: left" class="style9">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblCliente" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            ForeColor="Black" Text="Empresa" Width="50px"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:ComboBox ID="cboCliente" runat="server" AutoCompleteMode="SuggestAppend" 
                                            AutoPostBack="True" DropDownStyle="DropDownList" Font-Names="Arial" 
                                            Font-Size="8pt" onselectedindexchanged="cboCliente_SelectedIndexChanged" 
                                            Width="280px">
                                        </asp:ComboBox>
                                    </td>
                                    <td style="text-align: left" class="style9">
                                        <asp:Label ID="lblTipo" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            ForeColor="Black" Text="Tipo Cliente:" Width="60px"></asp:Label>
                                        <asp:Label ID="lblTipoCliente" runat="server" Font-Names="Arial" 
                                            Font-Size="8pt" ForeColor="#1C5AB6" style="text-align: left" Visible="False" 
                                            Width="50px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblContacto" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Contacto" Width="50px" ForeColor="Black" Height="16px"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:ComboBox ID="cboContacto" runat="server" AutoCompleteMode="SuggestAppend"  
                                            DropDownStyle="DropDownList" Font-Names="Arial" Font-Size="8pt" 
                                            Width="280px" >
                                        </asp:ComboBox>
                                    </td>
                                    <td style="text-align: left" class="style9">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblObra" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            Text="Obra" Width="50px" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:ComboBox ID="cboObra" runat="server" AutoCompleteMode="SuggestAppend" 
                                            AutoPostBack="True"  
                                            Font-Names="Arial" Font-Size="8pt" Width="280px"  
                                            onselectedindexchanged="cboObra_SelectedIndexChanged">
                                        </asp:ComboBox>
                                    </td>
                                    <td style="text-align: left" class="style9">
                                        <asp:Label ID="lblCiudadDespacho" runat="server" Font-Names="Arial" 
                                            Font-Size="8pt" ForeColor="Black" Text="Desp:" 
                                            Width="30px"></asp:Label>
                                        <asp:Label ID="LCiudadObra" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                            ForeColor="#1C5AB6" style="text-align: left; margin-left: 0px" Text="Sin" 
                                            Width="110px"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Panel ID="pnlBusqueda" runat="server" Font-Names="Arial" 
                                        Font-Size="8pt" GroupingText="Busqueda" Width="200px" 
                                        style="text-align: left" ForeColor="Black" Height="123px">
                                        <table align="left" style="width: 188px">
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblPV" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        Text="PV No" Width="40px" style="text-align: right" ForeColor="Black"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPV" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        Width="80px" AutoPostBack="True" ontextchanged="txtPV_TextChanged" 
                                                        MaxLength="7" style="text-align: right" BackColor="#FFFF66"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                <asp:Label ID="lblFUP" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        Text="FUP No" Width="40px" style="text-align: right" ForeColor="Black"></asp:Label>
                                               
                                                </td>
                                                <td >
                                                <asp:TextBox ID="txtFUP" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        Width="80px" AutoPostBack="True" ontextchanged="txtFUP_TextChanged" 
                                                        style="text-align: right" MaxLength="5" BackColor="#FFFF66"></asp:TextBox>
                                                
                                                </td>
                                            </tr>
                                            <tr >
                                                <td>
                                                    <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="#1C5AB6" Text="" Visible="true" Font-Bold="True" Font-Underline="true"></asp:Label>
                                                </td>
                                                <td style="text-align: right" >                                                 
                                                 <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" style="text-align: right">
                                                <ProgressTemplate>
                                                    <img alt="" src="Imagenes/Indicator.gif" height="20" style="text-align: center; float: right;" width="20"/>
                                                </ProgressTemplate>

                                                </asp:UpdateProgress>
                                                 <asp:ImageButton ID="ImageButton1" runat="server" 
                                                        ImageUrl="~/Imagenes/Arrow back123.png" onclick="ImageButton1_Click" 
                                                        style="text-align: right" ToolTip="Volver a Empresa" Visible="False" />
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblidofa" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="#1C5AB6" Font-Bold="True" Font-Underline="True" Visible="False"></asp:Label>
                                               
                                                    <asp:Button ID="btnSeguimiento" runat="server" BackColor="Silver" BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="#0066CC"   Text="Log" Width="32px" OnClick="btnSeguimiento_Click" Visible="False" />
                                               
                                                    
                                                 </td>
                                                <td style="text-align: right">
                                                 <asp:Button ID="btnNuevo" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                                        BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                                         onclick="btnNuevo_Click" Text="Nuevo" Width="70px" />                                               
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                </table>
            <table>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="Label10" runat="server" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Text="Tipo Pedido:" Visible="false"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="lblTipoPedido" runat="server" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="#1C5AB6" Text="" Visible="false"></asp:Label>
                    </td>
                    <td style="text-align: right">
                        <asp:Label ID="Label12" runat="server" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Text="Contenido:" Visible="false"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="lblContenido" runat="server" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="#1C5AB6" Text="" Visible="false"></asp:Label>
                    </td>
                     <td style="text-align: right" class="auto-style8">
                         <asp:Label ID="Label13" runat="server" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Text="Rechazado:" Visible="false"></asp:Label>
                         </td>
                    <td style="text-align: left">
                        <asp:Label ID="lblRechazado" runat="server" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="#1C5AB6" Text="" Visible="false"></asp:Label>
                    </td>
                    <td style="text-align: right">
                        <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Text="Fecha Desp:"></asp:Label>
                    </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtFechaDes" runat="server"
                            Font-Names="Arial" Font-Size="8pt"
                            Style="text-align: right" TabIndex="13" Width="60px" Enabled="false"></asp:TextBox>
                        <asp:Button ID="btnGuardarFechaDespacho" runat="server" BackColor="#1C5AB6" ForeColor="White"
                            BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            OnClick="btnGuardarFechaDespacho_Click" Text="Guardar Fecha" Width="100px" Enabled="false" Visible="false" />
                        <asp:MaskedEditExtender ID="txtFechaDes_MaskedEditExtender" runat="server"
                            AutoComplete="False" CultureAMPMPlaceholder=""
                            CultureCurrencySymbolPlaceholder="" CultureDateFormat=""
                            CultureDatePlaceholder="" CultureDecimalPlaceholder=""
                            CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True"
                            Mask="99/99/9999" MaskType="Date" TargetControlID="txtFechaDes"
                            UserDateFormat="DayMonthYear">
                        </asp:MaskedEditExtender>
                        <asp:CalendarExtender ID="txtFechaDes_CalendarExtender" runat="server"
                            Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtFechaDes">
                        </asp:CalendarExtender>
                    </td> <td style="text-align: right">
                        <asp:Label ID="lblOF" runat="server" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Text="Orden Asociada:" Visible="False"></asp:Label>
                        </td>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtOF" runat="server" AutoPostBack="True" Font-Names="Arial"
                            Font-Size="8pt" MaxLength="7" OnTextChanged="txtOF_TextChanged"
                            Style="text-align: right"
                            ToolTip="Ejemplo: 7012-13, si no desea asociar dijite 0" Width="50px" Visible="False">0</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" class="auto-style7" >
                        &nbsp;<asp:Label ID="lblIdRecompra0" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Text="." Visible="False"></asp:Label>
                        &nbsp;&nbsp;
                    </td>
                    
                    <td class="auto-style7" colspan="5">
                        <asp:Label ID="lblIdRecompra" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Text="ID Recomendacion del Tecnico:" Visible="False"></asp:Label>
                        <asp:TextBox ID="txtIdRecompra" runat="server" AutoPostBack="True" BackColor="#FFFF66" Font-Names="Arial" Font-Size="8pt" MaxLength="7" OnTextChanged="txtIdRecompra_TextChanged" Style="text-align: right" ToolTip="Ejemplo: si no desea asociar dijite 0" Visible="False" Width="34px">0</asp:TextBox>
                        <asp:Button ID="btnGuardarIdRecompra" runat="server" BackColor="#1C5AB6" BorderColor="#999999" Enabled="true" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White" OnClick="btnGuardarIdRecompra_Click" Text="Guardar IdRecompra" Visible="false" Width="124px" />
                    </td>
                    
                </tr>
                <tr>
                    <td colspan="10" class="auto-style5">
                        <asp:Label ID="lblMensajeRecompra" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" style="text-align: right" visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="10" style="text-align: right">
                        <asp:Label ID="lblmensajePedido" runat="server" Font-Names="Arial" Font-Size="9pt" ForeColor="Black" style="text-align: right" Text="" visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:Panel ID="pnlCotAcc" runat="server" Enabled="False"
                            Font-Names="Arial" Font-Size="8pt" GroupingText="Accesorios"
                            Width="780px" ForeColor="Black">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Panel ID="pnlBusAcc" runat="server" Font-Names="Arial"
                                            Font-Size="8pt" GroupingText="Busqueda" Width="200px">
                                            <table>
                                                <tr>
                                                    <td style="text-align: right">
                                                        <asp:Label ID="lblNombre" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Text="Nombre" Width="40px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:TextBox ID="txtNomAcc" runat="server" AutoPostBack="True"
                                                            Font-Names="Arial" Font-Size="8pt" OnTextChanged="txtNomAcc_TextChanged"
                                                            Width="120px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblCodigo" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            ForeColor="Black" Style="text-align: right" Text="Código" Width="45px"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:TextBox ID="txtCodigo" runat="server" AutoPostBack="True"
                                                            Font-Names="Arial" Font-Size="8pt" MaxLength="7"
                                                            OnTextChanged="txtCodigo_TextChanged" Width="40px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Panel ID="Panel4" runat="server" Font-Names="Arial" Visible="true"
                                            Font-Size="8pt" GroupingText="Selección" Width="400px">
                                            <table>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="lblCantidad0" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            ForeColor="Black" Text="Grupo" Width="60px"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:ComboBox ID="cboGrupo" runat="server" AutoCompleteMode="SuggestAppend"
                                                            AutoPostBack="True" CaseSensitive="True" DropDownStyle="DropDownList"
                                                            Font-Names="Arial" Font-Size="8pt"
                                                            OnSelectedIndexChanged="cboGrupo_SelectedIndexChanged" Width="240px">
                                                        </asp:ComboBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <asp:Label ID="lblCantidad" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            ForeColor="Black" Text="Accesorio" Width="60px"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left">
                                                        <asp:ComboBox ID="cboAccesorio" runat="server" AutoCompleteMode="SuggestAppend"
                                                            AutoPostBack="True" CaseSensitive="True" DropDownStyle="DropDownList"
                                                            Font-Names="Arial" Font-Size="8pt"
                                                            OnSelectedIndexChanged="cboAccesorio_SelectedIndexChanged" Width="240px">
                                                        </asp:ComboBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                    <td align="center">
                                        <asp:Image ID="imgItem" runat="server" Width="80px" ImageAlign="Middle" Height="100px" onclick="DisplayFullImage(this);" Visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="PanelEspecificaciones" runat="server" Enabled="True" Visible="false"
                            Font-Names="Arial" Font-Size="8pt" GroupingText="Especificaciones"
                            Width="780px" ForeColor="Black">
                            <table>
                                <tr>
                                    <td>
                                        <table style="width: 671px">
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right" Text="Peso Unitario:" Visible="true"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblPesoUni" runat="server" Font-Names="Arial" Width="40px" Font-Size="8pt" ForeColor="Black" Text="0" Visible="true" DataFormatString="{0:n}"></asp:Label>
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="LPrecioUni" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Text="Precio Unitario" Visible="true"></asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="LblPrecioUni" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right" Text="0" Visible="true" DataFormatString="{0:n}"></asp:Label>
                                                    <asp:Label ID="LblMoneda" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: left" Text="" Visible="true"></asp:Label>
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Cantidad" Visible="true"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCantidad" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="8pt" Width="40" MaxLength="7" OnTextChanged="txtCantidad_TextChanged" Style="text-align: right" Visible="true" onchange="" DataFormatString="{0:n}" Text="0"></asp:TextBox>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">

                                                    <asp:Label ID="Label11" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right" Text="Cant Empaque:" Visible="true"></asp:Label>

                                                </td>
                                                <td class="auto-style3">
                                                    <asp:Label ID="LEmpaque" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Text="0" Visible="true" DataFormatString="{0:n}"></asp:Label>
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Style="text-align: left" Text="Disponible" Visible="true"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDisponible" runat="server" AutoPostBack="True" Enabled="False" Width="40" Style="text-align: right" Font-Names="Arial" Font-Size="8pt" MaxLength="7" OnTextChanged="txtCantidad_TextChanged" Visible="true" DataFormatString="{0:n}" Text="0"></asp:TextBox>
                                                </td>

                                                <td style="text-align: right">
                                                    <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Text="Precio Total" Visible="true"></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPrecio" runat="server" AutoPostBack="True" Enabled="False" Font-Names="Arial" Width="80" Font-Size="8pt" MaxLength="20" OnTextChanged="txtPrecio_TextChanged" Style="text-align: right" Text="0" Visible="true" DataFormatString="{0:n}"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right" Text="Peso Empaque:" Visible="true"></asp:Label>
                                                </td>
                                                <td class="auto-style2">
                                                    <asp:Label ID="LPesoEmp" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Text="0" Visible="true" DataFormatString="{0:n}"></asp:Label>
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblMostrarTipo" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Text="Tipo Item" Visible="true"></asp:Label>

                                                </td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="lblMostrarTipoItem" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Style="text-align: left" Text="" Visible="true"></asp:Label>
                                                </td>
                                                <td style="text-align: right">
                                                    
                                                </td>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:Label ID="LblFabricado" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Text="" Visible="true"></asp:Label>

                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td colspan="2" style="text-align: left">
                                        <asp:ImageButton ID="btnGuardar" runat="server" Height="74px" 
                                            ImageUrl="~/Imagenes/ecar.png" OnClick="btnGuardar_Click" ToolTip="Guardar" 
                                            Width="81px" />
                                    </td>
                                    <tr>
                                        <td style="text-align: left">
                                            <asp:Label ID="Label9" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Observacion" Width="80px"></asp:Label>

                                            <asp:TextBox ID="txtObservaciones" runat="server" Font-Names="Arial" 
                                                Font-Size="8pt" Height="30px" Style="text-align: left" TextMode="MultiLine" 
                                                Width="554px"></asp:TextBox>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;      

                                           <%-- <asp:Button ID="btnGuardar1" runat="server" BackColor="#1C5AB6" 
                                                Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="White" 
                                                Height="20px" OnClick="btnGuardar1_Click" Style="font-size: small" 
                                                Text="Adicionar" ToolTip="Guardar" Width="80px" />--%>

                                               <%--  <asp:Button ID="btnGuardar1" runat="server" BackColor="#1C5AB6" 
                                                Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="White" 
                                                Height="20px" OnClick="btnGuardar1_Click" Style="font-size: small" 
                                                Text="Adicionar" ToolTip="Guardar" Width="80px" />
--%>
                                            </td>
                                            <td colspan="2" style="text-align: left"> 
                                            </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <table>
                                                <tr>
                                                    <td rowspan="2">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Panel ID="Panel1" runat="server" Font-Names="Arial" Font-Size="8pt" Visible="false" Width="200px">
                                                                        <table>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lblTipoItem" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Tipo" Visible="False" Width="20px"></asp:Label>

                                                                                </td>
                                                                                <td>
                                                                                    <asp:ComboBox ID="cboTipo" runat="server" AutoCompleteMode="SuggestAppend" CaseSensitive="True" DropDownStyle="DropDownList" Font-Names="Arial" Font-Size="8pt" Visible="False" Width="100px">
                                                                                    </asp:ComboBox>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lblModeloItem" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Modelo" Visible="False" Width="40px"></asp:Label>

                                                                                </td>
                                                                                <td>
                                                                                    <asp:ComboBox ID="cboModelo" runat="server" AutoCompleteMode="SuggestAppend" CaseSensitive="True" Font-Names="Arial" Font-Size="8pt" Visible="False" Width="100px">
                                                                                    </asp:ComboBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>

                                                                <td>
                                                                    <asp:Panel ID="Panel2" runat="server" Font-Names="Arial" Font-Size="8pt" Visible="false" Width="200px">
                                                                        <asp:GridView runat="server" ID="GridView1" BackColor="White" BorderColor="#999999"
                                                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="100%" PageSize="5" AllowPaging="true"
                                                                            OnPageIndexChanging="GridView1_PageIndexChanging" AutoGenerateColumns="False" Font-Size="7pt" Font-Names="arial" Height="16px" DataKeyNames="lblParametroId">
                                                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                                                            <Columns>
                                                                                <asp:BoundField DataField="lblParametroId" HeaderText="" ReadOnly="true" Visible="false" />
                                                                                <asp:BoundField DataField="lblParametro" HeaderText="Parámetros" ReadOnly="true" />
                                                                                <asp:TemplateField HeaderText="">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="textParametro" runat="server" Text='<%# Bind("txtParametro") %>' Style="text-transform: uppercase; text-align: right" MaxLength="100" Width="60" Enabled="true"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="rqParametro" runat="server" Display="Dynamic" ControlToValidate="textParametro" ErrorMessage="*" CssClass="msgvalidar" InitialValue=" " SetFocusOnError="true" ValidationGroup="grupIp"></asp:RequiredFieldValidator>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                                            <HeaderStyle BackColor="#1C5AB6" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                                            <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                                            <SortedDescendingHeaderStyle BackColor="#000065" />
                                                                        </asp:GridView>
                                                                    </asp:Panel>

                                                                </td>
                                                                <td>

                                                                    <asp:Panel ID="Panel3" runat="server" Font-Names="Arial" Font-Size="8pt" Visible="false" Width="300px">
                                                                        <table style="width: 300px">
                                                                                    <tr>
                                                                                        <td style="text-align: left">
                                                                                            <asp:Button ID="btnSubirPlano" runat="server" Font-Names="Arial" ForeColor="White"
                                                                                                Font-Size="8pt" OnClick="btnSubirPlano_Click" Text="Cargar Planos" Width="100px" BackColor="#1C5AB6"
                                                                                                Font-Bold="True" Visible="true" Enabled="false" />                                                                        
                                                                                            <br />
                                                                                            <asp:Label ID="lblPlano" runat="server" Text="Label"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                        </table>

                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
                <table>
                    <tr>
                        <td colspan="2" style="font-size: xx-small; text-align: right;">
                            <asp:Panel ID="pnlConfirmacion" runat="server"
                                Font-Names="Arial" Font-Size="8pt" GroupingText=""
                                Width="780px" Style="text-align: right" ForeColor="Black">
                                <table width="770">
                                    <tr>
                                        <td>
                                        <asp:Button ID="btnCargarPrecio" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                                BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                OnClick="btnCargarPrecio_Click"
                                                OnClientClick="return confirm('Desea calcular nuevamente los precios del pedido de venta?')" Text="Actualizar Precios"
                                                Width="114px" visible="true" Enabled="false"/>
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnAnular" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                                BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                OnClick="btnAnular_Click"
                                                OnClientClick="return confirm('Desea anular el pedido de venta')" Text="Anular"
                                                Width="61px" />
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnCarta" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                                BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                OnClientClick="NavigateCartaPv()" Visible ="true"
                                                Style="text-align: center; margin-left: 0px;" Text="Generar Carta"
                                                Width="102px" />

                                            &nbsp;                                            
                                        
                                             <asp:Button ID="btnEstado" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                                BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                OnClientClick="NavigateEstadoPv()" Visible ="false"
                                                Style="text-align: center; margin-left: 0px;" Text="Estado Pv"
                                                Width="81px" />
                                            &nbsp;&nbsp;<asp:Button ID="btnSolicFatura" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                                BorderColor="#999999" Enabled="false" Font-Bold="True" Font-Names="Arial" Visible ="false" 
                                                Font-Size="8pt" OnClientClick="Navigate()" Text="Solicitud De Facturación" Width="150px" />
                                            &nbsp;                                            
                                        <asp:Button ID="btnConfVenta" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                            BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                            OnClick="btnConfirmarPV_Click"
                                            OnClientClick="return confirm('Esta seguro de confirmar la venta? Recuerde que no es posible modificar despues de confirmar')"
                                            Style="text-align: center" Text="Confirmar Venta" Width="110px" Enabled="false" />
                                                                                    
                                        </td>
                                        <td>
                                            <asp:Button ID="btnCarga" runat="server" BackColor="#1C5AB6" BorderColor="#999999" Enabled="true" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White" OnClick="btnCarga_Click" OnClientClick="return confirm('Esta seguro de confirmar la venta? Recuerde que no es posible modificar despues de confirmar')" Style="text-align: center" Text="Carga" Width="39px" Visible="False" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="font-size: xx-small; text-align: right;">
                            <asp:Panel ID="Panel5" runat="server"
                                Font-Names="Arial" Font-Size="8pt" GroupingText="Confirmación"
                                Width="780px" Style="text-align: right" ForeColor="Black" Visible ="false">
                                <table width="770">
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:CheckBox ID="ChkConfComercial" runat="server" Font-Names="Arial"
                                                Font-Size="8pt" OnCheckedChanged="ChkConfComercial_CheckedChanged" Text="Confirmación Venta"
                                                Visible="false" Width="140px" AutoPostBack="True" ForeColor="Black" />
                                            <asp:CheckBox ID="chkConfAsistente" runat="server" Font-Names="Arial"
                                                Font-Size="8pt" OnCheckedChanged="chkConfAsistente_CheckedChanged" Text="Confirmación Asistente"
                                                Visible="false" Width="140px" AutoPostBack="True" ForeColor="Black" />
                                            <asp:Button ID="btnConfIngenieria" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                            BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                            OnClick="btnConfIngenieria_Click"
                                            OnClientClick="return confirm('Esta seguro de confirmar la venta? Recuerde que no es posible modificar después de confirmar')"
                                            Text="Confirmar Ingeniería" Width="140px" Enabled="false" Visible="false" />
                                                                                 
                                            <asp:CheckBox ID="chkLogisticaAlmacen" runat="server" Font-Names="Arial"
                                                Font-Size="8pt" OnCheckedChanged="chkLogisticaAlmacen_CheckedChanged" Text="Logist Recibe Almacen"
                                                Visible="False" Width="140px" AutoPostBack="True" ForeColor="Black" />
                                            <asp:CheckBox ID="chkLogisticaAccesorios" runat="server" AutoPostBack="True"
                                                Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                                OnCheckedChanged="chkLogisticaAccesorios_CheckedChanged" Text="Logist Recibe Accesorios"
                                                Visible="False" Width="150px" />
                                        </td>                                                                               
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                            <asp:TextBox ID="textRechazo" runat="server" Height="30px" Style="text-align: left" TextMode="MultiLine" Width="520px" Enabled="true" Visible="false"></asp:TextBox>
                                            <asp:Button ID="btnRechazarComercialPV" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                                BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                OnClick="btnRechazarComercialPV_Click"
                                                OnClientClick="return confirm('Esta seguro que desea rechazar la venta?')"
                                                Style="text-align: center" Text="Rechazar Comercial" Width="110px" Enabled="true" Visible="false" />
                                            <asp:Button ID="btnRechzarAsistentePV" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                                BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                OnClick="btnRechzarAsistentePV_Click"
                                                OnClientClick="return confirm('Esta seguro que desea rechazar la venta?')"
                                                Style="text-align: center" Text="Rechazar Asistente" Width="110px" Enabled="true" Visible="false" />
                                        </td>
                                    </tr>                                    
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                <tr>
                    <td colspan="2">
                        <asp:Accordion ID="Accordion1" runat="server" 
                            ContentCssClass="accordionContent" HeaderCssClass="accordionHeader"  RequireOpenedPane = "false" SelectedIndex =  "0"
                            HeaderSelectedCssClass="accordionHeaderSelected" Width="780px" AutoPostBack="True"  
                            > 
                            <Panes>
                                <asp:AccordionPane runat="server" ID="AccordPaneDetalle" ContentCssClass="accordionContent"
                                    Font-Bold="False" Font-Names="Arial" Font-Size="8pt" HeaderCssClass="accordionHeader"  
                                    HeaderSelectedCssClass="accordionHeaderSelected" Width="100%">
                                    <Header>
                                        <asp:Label runat="server" Text="DETALLE" ID="AccordDetalle"></asp:Label>
                                    </Header>                                    
                                    <Content>   
                                         <div style="float:right; color:blue";>                                
                                        <asp:Label runat="server" Text="Valor Total: $ " ID="Label2" Font-Names="Arial" Font-Size="8pt" Font-Bold="True" ></asp:Label>
                                        <asp:Label runat="server" Text="0" ID="ventaTotal" Font-Names="Arial" Font-Size="8pt" Font-Bold="True"></asp:Label>
                                             </div>
                                        <asp:GridView runat="server" ID="grdDetalle" BackColor="White" BorderColor="#999999"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="100%" PageSize="10" AllowPaging="true"
                                            AutoGenerateColumns="False" Font-Size="8pt" Font-Names="arial" Height="16px" OnRowEditing="grdDetalle_RowEditing1" OnRowDeleting="grdDetalle_RowDeleting1" OnRowCancelingEdit="grdDetalle_RowCancelingEdit1" OnRowUpdating="grdDetalle_RowUpdating1" OnSelectedIndexChanging="grdDetalle_SelectedIndexChanging"
                                            OnPageIndexChanging="grdDetalle_PageIndexChanging1" OnRowDataBound="grdDetalle_RowDataBound" DataKeyNames="cot_acc_id"
                                            >
                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                            <Columns>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="idItem" runat="server" Text='<%# Bind("cot_acc_id") %>' style="text-transform:uppercase; text-align: right" MaxLength="100" Enabled="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <%--<asp:BoundField ID="" DataField="" HeaderText="Id" ReadOnly="true" Visible="false"/>--%>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="cot_item" runat="server" Text='<%# Bind("cot_item") %>' style="text-transform:uppercase; text-align: right" MaxLength="100" Enabled="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="N°">
                                                    <ItemTemplate>
                                                        <asp:Label Width = "20px" runat="server" Text='<%#Container.DataItemIndex + 1%>' ID="consecutivo"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="cot_acc_id_acc" HeaderText="CodErp" ReadOnly="true" HeaderStyle-Width = "30px" HeaderStyle-HorizontalAlign ="right" ItemStyle-Width = "30px"  />
                                                <asp:BoundField DataField="descripcion" HeaderText="Descripcion"  ReadOnly="true" HeaderStyle-Width = "180px" />
                                                <asp:TemplateField HeaderText="Especial">
                                                    <ItemTemplate>
                                                        <asp:Label Width = "35px" runat="server" Text='<%# Bind("especial") %>' ID="especial"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cantidad" HeaderStyle-Width = "50px">
                                                    <ItemTemplate>      
                                                        <asp:TextBox ID="textCantidad"  Width = "50px"  runat="server" Text='<%# Bind("cot_cantidad") %>' style="text-transform:uppercase; text-align: right"  Enabled="false"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="rqftextDesc" runat="server" Display="Dynamic" ControlToValidate="textDesc" ErrorMessage="*" CssClass="msgvalidar" InitialValue=" " SetFocusOnError="true" ValidationGroup="grupIp"></asp:RequiredFieldValidator>--%>
                                                    </ItemTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="true" HeaderText="Precio Unit" HeaderStyle-Width = "90px" ItemStyle-HorizontalAlign ="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="cot_acc_precio_unitario" runat="server" Text='<%# Bind("cot_acc_precio_unitario") %>' style="text-transform:uppercase; text-align: right" MaxLength="100" Enabled="false"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>                                                
                                                <asp:BoundField DataField="cot_acc_precio_total" HeaderText="Precio Total" ItemStyle-HorizontalAlign ="Right" ReadOnly="true" />
                                                <asp:TemplateField HeaderText="Observacion">
                                                    <ItemTemplate>      
                                                        <asp:TextBox ID="textObservacion" runat="server" Text='<%# Bind("cot_observacion") %>' style="text-transform:uppercase" MaxLength="100" Enabled="false"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="rqftextDesc" runat="server" Display="Dynamic" ControlToValidate="textDesc" ErrorMessage="*" CssClass="msgvalidar" InitialValue=" " SetFocusOnError="true" ValidationGroup="grupIp"></asp:RequiredFieldValidator>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                
                                                <asp:TemplateField >
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LkB1" runat="server" CommandName="Edit" CausesValidation="false" >
                                                            <asp:Image ID="Image1" ImageUrl="Imagenes/write.png" ImageAlign="Middle"
                                                                runat="server" Visible="true"/>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="LB2" runat="server" CommandName="Update" CausesValidation="false" Visible="true">Actualizar</asp:LinkButton>
                                                        <asp:LinkButton ID="LkEliminar1" runat="server" CommandName="Delete" CausesValidation="false" Visible =" true">Eliminar</asp:LinkButton>                                                        
                                                        <asp:LinkButton ID="LB3" runat="server" CommandName="Cancel" CausesValidation="false">Cancelar</asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Select" CausesValidation="false">Seleccionar</asp:LinkButton>
                                                    </EditItemTemplate>
                                                     <ItemStyle  HorizontalAlign="Center"/>
                                                </asp:TemplateField>                                             
                                            </Columns>
                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                            <HeaderStyle BackColor="#1C5AB6" Font-Bold="True" ForeColor="White"  HorizontalAlign="Center"/>
                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#000065" />
                                        </asp:GridView>
                                    </Content>
                                </asp:AccordionPane>
                                <asp:AccordionPane ID="AcorEstado" runat="server"  Visible ="false"
                                    ContentCssClass="accordionContent" Font-Bold="False" Font-Names="Arial" 
                                    Font-Size="8pt" HeaderCssClass="accordionHeader" 
                                    HeaderSelectedCssClass="accordionHeaderSelected">
                                    <Header>
                                        <asp:Label ID="lblEstadopv" runat="server" Text="ESTADO PV"></asp:Label>
                                    </Header>
                                    <Content>

                                       <rsweb:ReportViewer ID="ReportViewer3" runat="server"  Visible ="false" ShowParameterPrompts="False" BackColor="White" Width="920" AsyncRendering="false">
                                        </rsweb:ReportViewer>
                                    </Content>
                                </asp:AccordionPane>
                                 <asp:AccordionPane ID="AccordionPane1" runat="server" 
                                    ContentCssClass="accordionContent" Font-Bold="False" Font-Names="Arial" 
                                    Font-Size="8pt" HeaderCssClass="accordionHeader"  Visible ="false"
                                    HeaderSelectedCssClass="accordionHeaderSelected">
                                    <Header>
                                        <asp:Label ID="lblCarta" runat="server" Text="CARTA COTIZACIÓN"></asp:Label>
                                    </Header>
                                    <Content>
                                        <rsweb:ReportViewer ID="ReportViewer2"  Visible="true" runat="server" ShowParameterPrompts="False" BackColor="White" Width="780" Height="1000" AsyncRendering="false">
                                        </rsweb:ReportViewer>
                                    </Content>
                                </asp:AccordionPane>
                            </Panes>
                        </asp:Accordion>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
