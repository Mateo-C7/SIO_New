<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="MaestroItemPlanta.aspx.cs" Inherits="SIO.MaestroItemPlanta"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Styles/MaestroItem.css" rel="stylesheet" />
    <link href="Styles/sweetalert.css" rel="stylesheet" />
    <script type="text/javascript" src="Scripts/sweetalert.min.js"></script>
    <script type="text/javascript" src="Scripts/jsMaetsroItemPlanta.js" language="javascript"></script>
    <script type="text/javascript">
        function ir() {
            javascript: window.open("item_planta_reporte.aspx");
        }
        function irLog() {
            javascript: window.open("VerReporteLogItem.aspx");
        }
        function MensajeError(x) {
            swal("Error!", x, "error");
        }
        function Mensajeaceptar(y,z) {
            sweetAlert(z, y, "success");
        }

        function MensajeAlerta(mensaje) {
            swal({ title: "", text: mensaje, type: "warning", timer: 4000, showConfirmButton: true });
        }

        function ValidListUso(source, args) {
            var chkListUso = document.getElementById('<%= chkListusoIp.ClientID %>');
            var chkListinputUso = chkListUso.getElementsByTagName("input");
            for (var i = 0; i < chkListinputUso.length; i++) {
                if (chkListinputUso[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }
        function ValidListOrigen(source, args) {
            var chkListOrigen = document.getElementById('<%= chkListorigen.ClientID %>');
            var chkListinputOrigen = chkListOrigen.getElementsByTagName("input");
            for (var i = 0; i < chkListinputOrigen.length; i++) {
                if (chkListinputOrigen[i].checked) {
                    args.IsValid = true;
                    return;
                }
            }
            args.IsValid = false;
        }

        function listaDescripcion(source, eventArgs) {
            document.getElementById('<%= lblDesc.ClientID %>').value = eventArgs.get_value();
        };

        function listadesciplanta(source, eventArgs) {

            var myvar = eventArgs.get_value();

            document.getElementById('<%= lblDesciplanta.ClientID %>').value = myvar;
            //alert(myvar);
        };

        //kp
        function soloLetras(e) {         
            key = e.keyCode || e.which;
            tecla = String.fromCharCode(key).toLowerCase();
            letras = " áéíóúabcdefghijklmnñopqrstuvwxyz";
            especiales = [8, 37, 39, 46];

            tecla_especial = false
            for (var i in especiales) {
                if (key == especiales[i]) {
                    tecla_especial = true;
                    break;
                }
            }

            if (letras.indexOf(tecla) == -1 && !tecla_especial)
                return false;
        }

     
        function limpia() {
            var val = document.getElementById("miInput").value;
            var tam = val.length;
            for (i = 0; i < tam; i++) {
                if (!isNaN(val[i]))
                    document.getElementById("miInput").value = '';
            }
        }




    </script>
    
    <script type="text/javascript" src="Scripts/jsMaetsroItemPlanta.js"></script>

    <style type="text/css">
        .auto-style1 {
            text-align: right;
            vertical-align: middle;
            height: 29px;
        }
        .auto-style2 {
            text-align: left;
            vertical-align: top;
            height: 29px;
        }
        .auto-style3 {
            height: 29px;
            width: 272px;
        }
        .auto-style4 {
            text-align: left;
            vertical-align: top;
            height: 29px;
            width: 350px;
        }
        .auto-style5 {
            text-align: left;
            vertical-align: top;
            width: 350px;
        }
        .auto-style6 {
            width: 350px;
            height: 30px;
        }
        .auto-style11 {
            text-align: right;
            vertical-align: middle;
            height: 30px;
        }
        .auto-style13 {
            text-align: right;
            vertical-align: middle;
            height: 30px;
            width: 272px;
        }
        .auto-style14 {
            text-align: right;
            vertical-align: middle;
            height: 29px;
            width: 244px;
        }
        .auto-style16 {
            height: 30px;
            width: 244px;
        }
        .auto-style17 {
            height: 179px;
        }
        .auto-style18 {
            text-align: left;
            vertical-align: top;
            width: 350px;
            height: 30px;
        }
        .auto-style19 {
            width: 272px;
            height: 30px;
            text-align: right;
        }
        .auto-style20 {
            text-align: right;
            vertical-align: middle;
            width: 244px;
            height: 30px;
        }
        .auto-style21 {
            text-align: left;
            vertical-align: top;
            height: 30px;
        }
        .auto-style23 {
            width: 272px;
            text-align: right;
        }
        .auto-style24 {
            text-align: left;
            vertical-align: middle;
            width: 244px;
        }
        .auto-style25 {
            height: 50px;
            width: 272px;
            text-align: right;
        }
    </style>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          <asp:Accordion ID="Accordion2" runat="server"
                ContentCssClass="accordionContent"
                HeaderCssClass="accordionHeader"
                HeaderSelectedCssClass="accordionHeaderSelected"
                Width="100%" Height="100px" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Black">
                <Panes>
                    <asp:AccordionPane ID="AccordionPane1" runat="server"
                        ContentCssClass="accordionContent"
                        Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        HeaderCssClass="accordionHeader"
                        HeaderSelectedCssClass="accordionHeaderSelected">
                        <Header>
                            <asp:Label ID="L" runat="server"
                                Text=""></asp:Label>
                        </Header>
                        <Content>
                 
                        </Content>
                    </asp:AccordionPane>   
                      <asp:AccordionPane ID="AccordionPane2" runat="server"
                        ContentCssClass="accordionContent"
                        Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        HeaderCssClass="accordionHeader"
                        HeaderSelectedCssClass="accordionHeaderSelected">
                        <Header>
                            <asp:Label ID="Acord2" runat="server"
                                Text=""></asp:Label>
                        </Header>
                        <Content>
            <table runat="server" id="TbItem" visible="true">
                <tr>
                    <td align="left">
                        <asp:Label ID="lbltituloitem" runat="server" Text="ITEM FORSA" Width="120px" CssClass="MI_titulo"></asp:Label>
                    </td>
                     <td align="right" id="trlblperfil">
                        <asp:Label Text="*Perfil:" runat="server" CssClass="MI_label" ID="lblPerfilIp"></asp:Label>
                    </td>
                     <td  colspan="2" align ="left" id="trcboperfil">
                          <asp:ComboBox ID="cboPerfilIp" runat="server" AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" Width="100px"  CausesValidation="false" AutoPostBack="true" OnSelectedIndexChanged="cboPerfilIp_SelectedIndexChanged">
                        </asp:ComboBox>
                         </td>
                       <td align="right"><asp:Label runat="server" ID="lblactivoI" CssClass="MI_label"></asp:Label></td>
                          <td align="right"><asp:Label runat="server" ID="Lbl_IdItemPlanta" Visible="false"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="5">&nbsp;
                    </td>
                </tr>
                <tr class="Mi_tbalto">
                    <td class="Mi_tbanchoD">
                        <asp:Label ID="lblGrupoIa" Text="*Grupo:" runat="server" CssClass="MI_label"></asp:Label>
                    </td>
                    <td class="Mi_tbanchoI" colspan="3">
                        <asp:Label runat="server" ID="lblmsjgropuIa" CssClass="label_red"></asp:Label>
                        <asp:ComboBox ID="cboGrupoIa" runat="server" AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" Width="220px" CausesValidation="false">
                        </asp:ComboBox>
                        <asp:RequiredFieldValidator runat="server" ID="rfvGrupoIa" ControlToValidate="cboGrupoIa" ErrorMessage="*" CssClass="msgvalidar" ValidationGroup="grupItem" InitialValue=" " Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                       <td>&nbsp;</td>
                </tr>
                <tr class="Mi_tbalto">
                    <td class="Mi_tbanchoD">
                        <asp:Label ID="lblDescripcionIa" Text="*Item Forsa:" runat="server" CssClass="MI_label"></asp:Label>
                    </td>
                    <td class="Mi_tbanchoI" colspan="3"><asp:TextBox runat="server" Enabled="true" ID="txtDescripcionIa" Width="308px"  style="text-transform:uppercase" AutoCompleteType="None" AutoPostBack="True" OnTextChanged="txtDescripcionIa_TextChanged" CausesValidation="false"></asp:TextBox>
                         <asp:AutoCompleteExtender ID="txtDescripcionIa_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                            CompletionSetCount="15" EnableCaching="true" Enabled="True" MinimumPrefixLength="1"
                                            ServiceMethod="GetListaDescripcion" UseContextKey="true" ServicePath="~/WSSIO.asmx"
                                            TargetControlID="txtDescripcionIa" OnClientItemSelected="listaDescripcion">
                            </asp:AutoCompleteExtender>
                         <input id="lblDesc" runat="server" type="hidden" />
                        <asp:RequiredFieldValidator runat="server" ID="rfvDescripcionIa" ControlToValidate="txtDescripcionIa" ErrorMessage="*" CssClass="msgvalidar" ValidationGroup="grupItem" SetFocusOnError="true"></asp:RequiredFieldValidator>                       
                    </td>
                      <td>&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="5" align="left" id="Lblobligatorio" class="Mi_tbalto">
                        <asp:Label runat="server" Text="* Este campo es obligatorio" CssClass="msgFinal"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="5" align="center" class="Mi_tbalto" id="trbotoneItem">                       
                        <asp:Button runat="server" CssClass="Mi_botones" Width="80px" Text="Guardar" ID="btnGuardarIa" OnClick="btnGuardarIa_Click" Visible="true"  />
                        <asp:Button runat="server" CssClass="Mi_botones" Width="80px" Text="Nuevo" ID="btnLimpiarIa" OnClick="btnLimpiarIa_Click" CausesValidation="false"  />
                       <asp:Button runat="server" CssClass="Mi_botones" Width="80px" Text="Ver Reporte" ID="btnReporte" OnClientClick="Navigate()" CausesValidation="false"  />
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <hr />
                    </td>
                </tr>
            </table>
                         </Content>
                    </asp:AccordionPane>   
                    </Panes>
            </asp:Accordion>
            <table runat="server">
                <tr>
                    <td colspan="4" align="left">
                        <asp:Label ID="lblClienteTituloIp" runat="server" Text="ITEM PLANTA" Width="120px" CssClass="MI_titulo"></asp:Label>
                    </td>
                     <td colspan="4" align="left"><asp:Button runat="server" CssClass="Mi_botones" Width="80px" Text="Ver Reporte" ID="btnreporteplanta2" OnClientClick="javascript:ir();" CausesValidation="false"  /></td>
                    <td colspan="4" align="right"><asp:Label runat="server" ID="lblactivoIp" CssClass="MI_label"></asp:Label></td>
                   
                </tr>
                <tr>
                    <td colspan="5">&nbsp;
                    </td>
                </tr>
                <tr class="Mi_tbalto" id="trGrupoIp">
                    <td class="auto-style1">
                        <asp:Label Text="*Grupo:" runat="server" CssClass="MI_label" ID="lblGrupoIp"></asp:Label>
                    </td>
                    <td class="auto-style4">
                       <asp:Label runat="server" ID="lblmsjgrupo" CssClass="label_red"></asp:Label>
                        <asp:ComboBox ID="cboGrupoIp" runat="server" AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" Width="220px" OnSelectedIndexChanged="cboGrupoIp_SelectedIndexChanged"  CausesValidation="false" AutoPostBack="true">
                        </asp:ComboBox>
                        <asp:RequiredFieldValidator ID="rqfGrupoIp" runat="server" Display="Dynamic" ErrorMessage="*" SetFocusOnError="true" ControlToValidate="cboGrupoIp" CssClass="msgvalidar" InitialValue=" " ValidationGroup="grupIp" Enabled="false"></asp:RequiredFieldValidator>
                    </td>
                    <td class="auto-style25">
                        <asp:Label ID="lblAgrupadorIp" runat="server" CssClass="MI_label" Text="*Item Forsa:"></asp:Label>
                    </td>
                    <td class="auto-style14">
                        <asp:Label runat="server" ID="lblmsjitemforsa" CssClass="label_red"></asp:Label>
                        <asp:ComboBox ID="cboAgrupadorIp" runat="server" AutoCompleteMode="SuggestAppend" AutoPostBack="true" DropDownStyle="DropDownList" OnSelectedIndexChanged="cboAgrupadorIp_SelectedIndexChanged" Width="320px">
                        </asp:ComboBox>
                        <asp:RequiredFieldValidator ID="rqfAgrupadorIp" runat="server" Display="Dynamic" SetFocusOnError="true" ControlToValidate="cboAgrupadorIp" ErrorMessage="*" CssClass="msgvalidar" InitialValue=" " ValidationGroup="grupIp" Enabled="false"></asp:RequiredFieldValidator>
                    </td>
                    <td class="auto-style2">
                         
                        
                    </td>
                </tr>
                 <tr class="Mi_tbalto"  id="trcreadoitem" >
                    <td class="auto-style11"> 
                         <asp:Label Text="*Planta:" runat="server" CssClass="MI_label" ID="txtPlantaIp"></asp:Label>
                    </td>
                    <td class="auto-style18">
                         <asp:ComboBox ID="cboPlantaIp" runat="server" AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" Width="220px" OnSelectedIndexChanged="cboPlantaIp_SelectedIndexChanged"  CausesValidation="false" AutoPostBack="true">
                        </asp:ComboBox>
                        <asp:RequiredFieldValidator ID="rqfcboPlantaIp" runat="server" Display="Dynamic" SetFocusOnError="true" ControlToValidate="cboPlantaIp" ErrorMessage="*" CssClass="msgvalidar" InitialValue=" " ValidationGroup="grupIp"></asp:RequiredFieldValidator>
                    </td>
                    <td class="auto-style19">
                        <asp:Label ID="lbliplantacreado" runat="server" CssClass="MI_label" Text="Item planta creado:" Visible="false"></asp:Label>
                     </td>
                    <td class="auto-style20">
                        <asp:ComboBox ID="cboiplantacreados" runat="server" AutoCompleteMode="SuggestAppend" AutoPostBack="true" DropDownStyle="DropDownList" OnSelectedIndexChanged="cboiplantacreados_SelectedIndexChanged" Visible="false" Width="320px">
                        </asp:ComboBox>
                    </td>

                    <td class="auto-style21">
                        &nbsp;</td>
                </tr>
                <tr class="Mi_tbalto">
                    <td class="auto-style11">
                       <asp:Label Text="Nombre ERP:" runat="server" CssClass="MI_label" ID="lblNombreERP" Visible="false" ForeColor="#1C5AB6" Font-Bold="true" Font-Italic="true"></asp:Label>
                    </td>
                    <td class="auto-style6">
                      <asp:Label Text="" runat="server" CssClass="MI_label" ID="lblNombreERPTxt" ForeColor="#1C5AB6" Font-Bold="true" Font-Italic="true" Visible="false"></asp:Label>
                    </td>
                     
                 
                    <td class="auto-style13" id="tdlblGruPlantaIp">
                        <asp:Label Text="*Grupo planta:" runat="server" CssClass="MI_label" ID="lblGruPlantaIp"></asp:Label></td>
                    <td id="tdGruPlantaIp" class="auto-style16">
                        <asp:TextBox runat="server" ID="txtGruPlantaIpId" Visible="false"></asp:TextBox>
                        <asp:TextBox runat="server" ID="txtGruPlantaIp" Width="220px" Enabled="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqftxtGruPlantaIp" runat="server" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtGruPlantaIp" ErrorMessage="*" CssClass="msgvalidar" InitialValue=" " ValidationGroup="grupIp" Enabled="false"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="Mi_tbalto">
                    <td class="Mi_tbanchoD">
                        <asp:Label Text="C&oacute;digo ERP:" runat="server" CssClass="MI_label"></asp:Label>
                    </td>
                    <td class="auto-style5">
                        <asp:TextBox runat="server" Enabled="false" ID="txtCodErpIp" Width="220px"  Style="text-align: right" OnTextChanged="txtCodErpIp_TextChanged" AutoPostBack="true"></asp:TextBox>
                        <asp:Button runat="server" Font-Size="XX-Small" Width="120px" ID="btnCodigoERP" OnClick="btnCodigoERP_Click" Text="Consulta a ERP" Visible="false" ToolTip="Consultar"/>
                    </td>
                    <td class="auto-style23"><asp:CheckBox runat="server" Checked="false" OnCheckedChanged="chkCodigoERP_CheckedChanged" ID="chkCodigoERP" Text="Existe?" Visible="false" AutoPostBack="true"/>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label Text="Referencia:" runat="server" CssClass="MI_label" ID="lblReferenciaIp"></asp:Label>
                    </td>
                    <td class="auto-style24">
                        
                        <asp:TextBox ID="txtReferenciaIp" runat="server" Enabled="false" Style="text-align: right" Width="220px"></asp:TextBox>
                        
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr class="Mi_tbalto">
                    <td class="Mi_tbanchoD">
                        <asp:Label Text="*Item Planta:" runat="server" CssClass="MI_label" ID="lblDscIp"></asp:Label>
                    </td>
                    <td class="auto-style5">
                        <asp:TextBox runat="server" ID="txtDscIp" Width="320px" MaxLength="40" OnTextChanged="txtDscIp_TextChanged" Style="text-transform: uppercase"  AutoPostBack="true" CausesValidation="false" AutoCompleteType="None" ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqftxtDscIp" runat="server" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtDscIp" ErrorMessage="*" CssClass="msgvalidar" InitialValue=" " ValidationGroup="grupIp"></asp:RequiredFieldValidator>
                         <asp:AutoCompleteExtender ID="txtDscIp_AutoCompleteExtender" runat="server" DelimiterCharacters=""
                                            CompletionSetCount="15" EnableCaching="true" Enabled="True" MinimumPrefixLength="1"
                                            ServiceMethod="GetDescIPlanta" UseContextKey="true" ServicePath="~/WSSIO.asmx"
                                            TargetControlID="txtDscIp" OnClientItemSelected="listadesciplanta">
                         </asp:AutoCompleteExtender>
                        <input id="lblDesciplanta" runat="server" type="hidden" />
                    </td>
                    <td class="auto-style23">
                         <asp:Label Text="*Desc.abreviada:" runat="server" CssClass="MI_label" ID="lblDscAbrvIp"></asp:Label>&nbsp;</td>
                    <td class="auto-style24">
                       
                        <asp:TextBox ID="txtDscAbrvIp" runat="server" MaxLength="20" Style="text-transform: uppercase" Width="220px"></asp:TextBox>
                       <asp:RequiredFieldValidator ID="rqftxtDscAbrvIp" runat="server" Display="Dynamic" SetFocusOnError="true" ControlToValidate="txtDscAbrvIp" ErrorMessage="*" CssClass="msgvalidar" InitialValue=" " ValidationGroup="grupIp"></asp:RequiredFieldValidator>
                    </td>
                    <td class="Mi_tbanchoI">
                        
                    </td>
                </tr>
                <tr>
                    <td colspan="5" class="auto-style17">
                        <asp:Accordion ID="AccordionPrincipalIp" runat="server" ContentCssClass="accordionContent" HeaderCssClass="accordionHeader"
                            HeaderSelectedCssClass="accordionHeaderSelected" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Style="margin-right: 0px; margin-top: 0px;" SelectedIndex="0" Width="860px">
                            <Panes>
                                <asp:AccordionPane runat="server" ID="AccordPaneIdiomaIp" ContentCssClass="accordionContent"
                                    Font-Bold="False" Font-Names="Arial" Font-Size="8pt" HeaderCssClass="accordionHeader"
                                    HeaderSelectedCssClass="accordionHeaderSelected">
                                    <Header>
                                        <asp:Label runat="server" Text="IDIOMA" ID="AccordIdiomaIp"></asp:Label>
                                    </Header>
                                    <Content>
                                        <center>
                                        <asp:GridView runat="server" ID="grdIdiomaIp" BackColor="White" BorderColor="#999999"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="850px" PageSize="5" AllowPaging="true"
                                            AutoGenerateColumns="False" Font-Size="8pt" Font-Names="arial" Height="16px" DataKeyNames="planta_idioma_id" 
                                            OnPageIndexChanging="grdIdiomaIp_PageIndexChanging">
                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="N°">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%#Container.DataItemIndex + 1%>' ID="lblnumIdioma"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ID PLANTA IDIOMA" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Bind("planta_idioma_id") %>' ID="lblplanta_idioma_id"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ID IDIOMA" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Bind("idioma_id") %>' ID="lblidioma_id"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Idioma">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# Bind("idioma") %>' ID="lblidioma"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Descripción" ItemStyle-Width="400px">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="textDesc" runat="server" Text='<%# Bind("descripcion") %>' Width="400px" Style="text-transform: uppercase" MaxLength="0" CausesValidation="false" AutoPostBack="true" OnTextChanged="textDesc_TextChanged"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rqftextDesc" runat="server" Display="Dynamic" ControlToValidate="textDesc" ErrorMessage="*" CssClass="msgvalidar" SetFocusOnError="true" ValidationGroup="grupIp"></asp:RequiredFieldValidator>
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
                                            </center>
                                    </Content>
                                </asp:AccordionPane>
                                <asp:AccordionPane runat="server" ID="AccordPaneUnddIp" ContentCssClass="accordionContent"
                                    Font-Bold="False" Font-Names="Arial" Font-Size="8pt" HeaderCssClass="accordionHeader"
                                    HeaderSelectedCssClass="accordionHeaderSelected">
                                    <Header>
                                        <asp:Label runat="server" Text="UNIDAD DE MEDIDA" ID="AccordUnddIp"></asp:Label>
                                    </Header>
                                    <Content>
                                        <table runat="server">
                                            <tr>
                                                <td colspan="2" >
                                                    <fieldset style="width: 235px;display:none;"  id="trlistuso" runat="server">
                                                        <legend><asp:Label ID="titleuso"  runat="server" Text="*Uso:"></asp:Label></legend>
                                                        <asp:CheckBoxList ID="chkListusoIp" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Table"  CausesValidation="false">
                                                            <asp:ListItem Text="Compra" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Venta" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="Manufactura" Value="3"></asp:ListItem>
                                                        </asp:CheckBoxList>
                                                        <asp:CustomValidator ID="cvchkListusoIp" runat="server" Display="Dynamic" SetFocusOnError="true" ErrorMessage="*" CssClass="msgvalidar" ValidationGroup="grupIp" ClientValidationFunction="ValidListUso" Enabled="false"></asp:CustomValidator>
                                                    </fieldset>
                                                </td>
                                                <td>
                                                    <fieldset style="width:260px;height:35px">
                                                        <legend>*Origen:</legend>
                                                        <asp:CheckBoxList ID="chkListorigen" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Table"  OnSelectedIndexChanged="chkListorigen_SelectedIndexChanged" AutoPostBack="true" CausesValidation="false" CellPadding="5">
                                                            <asp:ListItem Text="Nacional" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Importado" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="Nacionalizado" Value="3"></asp:ListItem>
                                                        </asp:CheckBoxList>
                                                        <asp:CustomValidator ID="cvchkListorigen" runat="server" Display="Dynamic" SetFocusOnError="true" ErrorMessage="*" CssClass="msgvalidar"  ClientValidationFunction="ValidListOrigen" ValidationGroup="grupIp" Enabled="false"></asp:CustomValidator>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                            <tr class="Mi_tbalto" id="trTipInvIp">
                                                <td class="Mi_tbanchoD">
                                                    <asp:Label runat="server" Text="*Tipo de inventario:" ID="lblTipInvIp"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="cboTipInvIp" Width="160px"  OnSelectedIndexChanged="cboTipInvIp_SelectedIndexChanged" AutoPostBack="true" CausesValidation="false"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rqfcboTipInvIp" runat="server" Display="Dynamic" ControlToValidate="cboTipInvIp" ErrorMessage="*" CssClass="msgvalidar" InitialValue=" " SetFocusOnError="true" ValidationGroup="grupIp" Enabled="false"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtTipInvIp" Width="400px" Enabled="false" MaxLength="0"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="Mi_tbalto" id="trGrupimpIp">
                                                <td class="Mi_tbanchoD">
                                                    <asp:Label runat="server" Text="*Grupo impositivo:" ID="lblGrupimpIp"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="cboGrupimpIp" Width="160px"  AutoPostBack="true" OnSelectedIndexChanged="cboGrupimpIp_SelectedIndexChanged" CausesValidation="false"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rqfcboGrupimpIp" runat="server" Display="Dynamic" ControlToValidate="cboGrupimpIp" ErrorMessage="*" CssClass="msgvalidar" InitialValue=" " SetFocusOnError="true" ValidationGroup="grupIp" Enabled="false"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtGrupimpIp" Width="400px" Enabled="false" MaxLength="0"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" align="center">
                                                    <asp:Label runat="server" Text="Unidad de Medida" ID="lblTitleUnddIp"></asp:Label>
                                                </td>
                                            </tr>                                            
                                            <tr class="Mi_tbalto" id="trBodega">
                                                <td class="Mi_tbanchoD">
                                                     <asp:Label ID="Lbl_Bodega" runat="server" Text="Bodega:"></asp:Label>
                                                </td>
                                                <td>
                                                     <asp:DropDownList runat="server" ID="cbo_Bodega" Width="80px"  AutoPostBack="true" CausesValidation="false"></asp:DropDownList>
                                                </td>
                                            </tr>

                                            <tr class="Mi_tbalto" id="trprincipalip">                                                 
                                                <td class="Mi_tbanchoD">
                                                    <asp:Label runat="server" Text="*Principal:" ID="lblPrincipalIp"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="cboPrincipalIp" Width="160px"  AutoPostBack="true" OnSelectedIndexChanged="cboPrincipalIp_SelectedIndexChanged" CausesValidation="false"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rqfcboPrincipalIp" runat="server" Display="Dynamic" ControlToValidate="cboPrincipalIp" ErrorMessage="*" CssClass="msgvalidar" InitialValue=" " SetFocusOnError="true" ValidationGroup="grupIp"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtPrincipalIp" Width="400px" Enabled="false" MaxLength="0"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="Mi_tbalto" id="trAdicionalIp">
                                                <td class="Mi_tbanchoD">
                                                    <asp:Label runat="server" Text="Adicional:" ID="lblAdicionalIp"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="cboAdicionalIp" Width="160px"  AutoPostBack="true" OnSelectedIndexChanged="cboAdicionalIp_SelectedIndexChanged" CausesValidation="false" ></asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtAdicionalIp" Width="400px" Enabled="false" MaxLength="0"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="Mi_tbalto" id="trOrdenIp">
                                                <td class="Mi_tbanchoD">
                                                    <asp:Label runat="server" Text="Orden:" ID="lblOrdenIp"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="cboOrdenIp" Width="160px"  AutoPostBack="true" OnSelectedIndexChanged="cboOrdenIp_SelectedIndexChanged" CausesValidation="false"></asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtOrdenIp" Width="400px" Enabled="false" MaxLength="0"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr id="titulofactor">
                                                <td colspan="3" align="center">
                                                    <asp:Label runat="server" Text="Factor Unidad de Medida" ID="lblfacunddIp"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="factor">
                                                <td class="Mi_tbanchoD">
                                                    <asp:Label runat="server" Text="*Factor:" ID="lblfactUnt"></asp:Label>
                                                </td>
                                                <td colspan="2" align="left">
                                                    <asp:TextBox runat="server" Enabled="false" Text="1" ID="txtfactorunitario" Width="20px"></asp:TextBox>
                                                </td>
                                            </tr>
                                             <tr id="trLongitudIp" class="Mi_tbalto">
                                                <td class="Mi_tbanchoD" style="width:93px">
                                                    <asp:Label runat="server" Text="*Factor:" ID="lblFactorLIp"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtFactorLIp" Width="90px" Style="text-align: right" CausesValidation="false"  OnTextChanged="txtFactorLIp_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rqftxtFactorLIp" runat="server" Display="Dynamic" ControlToValidate="txtFactorLIp" ErrorMessage="*" CssClass="msgvalidar" InitialValue=" " SetFocusOnError="true" ValidationGroup="grupIp" Enabled="false"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                              <tr id="trM21Ip" class="Mi_tbalto">
                                                <td class="Mi_tbanchoD" style="width:93px">
                                                    <asp:Label runat="server" Text="*Factor:" ID="lblFactorM2Ip"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtFactorM2Ip" Width="90px" Style="text-align: right" CausesValidation="false" OnTextChanged="txtFactorM2Ip_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rqftxtFactorM2Ip" runat="server" Display="Dynamic" ControlToValidate="txtFactorM2Ip" ErrorMessage="*" CssClass="msgvalidar" InitialValue=" " SetFocusOnError="true" ValidationGroup="grupIp" Enabled="false"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr id="trpeso" class="Mi_tbalto">
                                                 <td class="Mi_tbanchoD" style="width:93px">
                                                    <asp:Label runat="server" ID="lblPesoLIp" Text="Peso Unitario(kg):"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtPeso_unitario" Width="90px"  Style="text-align: right" OnTextChanged="txtPesoLIp_TextChanged" CausesValidation="false" AutoPostBack="true"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                        <table id="trundadiccionales" runat="server">
                                            <tr>
                                                <td class="Mi_tbanchoD"  style="width:93px">
                                                    <asp:Label ID="lblTipoOrdenIa" Text="Tipo Orden:" runat="server" CssClass="MI_label"></asp:Label>
                                                </td>
                                                <td class="Mi_tbanchoI" colspan="3">
                                                    <asp:Label runat="server" ID="lblmsjTipoOrdenIa" CssClass="label_red"></asp:Label>
                                                    <asp:DropDownList runat="server" ID="cboTipoOrdenIa" Width="210px" ></asp:DropDownList>
                                                </td>
                                                <td colspan="4">&nbsp;</td>
                                            </tr>
                                            <tr class="Mi_tbalto">
                                                  <td class="Mi_tbanchoD"><asp:Label ID="lblAncho1" Text="Ancho1(cm):" runat="server" CssClass="MI_label"></asp:Label></td>
                                                 <td ><asp:TextBox runat="server" Enabled="true" ID="txtAncho1" Width="70px"   Style="text-align: right"  OnTextChanged="txtAncho1_TextChanged" AutoPostBack="true" ></asp:TextBox>
                                                     </td>
                                                 <td class="Mi_tbanchoD"><asp:Label ID="lblAlto1" Text="Alto1(cm):" runat="server" CssClass="MI_label"></asp:Label></td>
                                                 <td ><asp:TextBox runat="server" Enabled="true" ID="txtAlto1" Width="70px"   Style="text-align: right"  OnTextChanged="txtAlto1_TextChanged" AutoPostBack="true" ></asp:TextBox>
                                                     </td>
                                                 <td class="Mi_tbanchoD"><asp:Label ID="lblAncho2" Text="Ancho2(cm):" runat="server" CssClass="MI_label"></asp:Label></td>
                                                 <td ><asp:TextBox runat="server" Enabled="true" ID="txtAncho2" Width="70px"   Style="text-align: right"  OnTextChanged="txtAncho2_TextChanged" AutoPostBack="true" ></asp:TextBox>
                                                     </td>
                                                 <td class="Mi_tbanchoD"><asp:Label ID="lblAlto2" Text="Alto2(cm):" runat="server" CssClass="MI_label"></asp:Label></td>
                                                 <td ><asp:TextBox runat="server" Enabled="true" ID="txtAlto2" Width="70px"   Style="text-align: right"  OnTextChanged="txtAlto2_TextChanged" AutoPostBack="true" ></asp:TextBox>
                                                     </td>
                                            </tr>
                                               <tr class="Mi_tbalto" >
                                                   <td class="Mi_tbanchoD" style="width: 93px">
                                                       <asp:Label ID="lblLargoIa" Text="Largo(cm):" runat="server" CssClass="MI_label"></asp:Label>
                                                   </td>
                                                   <td class="Mi_tbanchoI"><asp:TextBox runat="server" ID="txtLargo" Width="70px" Style="text-align: right" OnTextChanged="txtLargo_TextChanged" AutoPostBack="true" ></asp:TextBox>
                                                       </td>
                                                   <td class="Mi_tbanchoD" ><asp:Label ID="lblCantidadEmpaqueIa" Text="Cantidad empaque:" runat="server" CssClass="MI_label"></asp:Label></td>       
                                                    <td class="Mi_tbanchoI"><asp:TextBox runat="server" ID="txtCantidadEmpaqueIa" Width="70px"   Style="text-align: right" OnTextChanged="txtCantidadEmpaqueIa_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </td>
                                                   <td class="Mi_tbanchoD">
                                                       <asp:Label ID="lblPesoEmpaqueIa" Text="Peso empaque(kg):" runat="server" CssClass="MI_label"></asp:Label></td>
                                                   <td class="Mi_tbanchoI">
                                                      <asp:TextBox runat="server" Enabled="true" ID="txtPesoEmpaqueIa" Width="70px" Style="text-align: right" OnTextChanged="txtPesoEmpaqueIa_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                       </td>
                                               </tr>
                                             <tr class="Mi_tbalto" >
                                                 <td colspan="4" rowspan="2">
                                                     <fieldset>
                                                         <legend class="MI_label">Disponible Para:</legend>
                                                         <asp:CheckBoxList ID="chkListaDisponiblesIa" runat="server" CssClass="MI_label" RepeatColumns="3" RepeatLayout="Table" CausesValidation="false" OnSelectedIndexChanged="chkListaDisponiblesIa_SelectedIndexChanged" AutoPostBack="true" CellPadding="4" CellSpacing="3">
                                                             <asp:ListItem Text="Cotizaci&oacute;n" Value="1"></asp:ListItem>
                                                             <asp:ListItem Text="Comercial" Value="2"></asp:ListItem>
                                                             <asp:ListItem Text="Producci&oacute;n" Value="3"></asp:ListItem>
                                                             <asp:ListItem Text="Ingenieria" Value="4"></asp:ListItem>
                                                             <asp:ListItem Text="Almacen" Value="5"></asp:ListItem>
                                                         </asp:CheckBoxList>
                                                     </fieldset>
                                                 </td>
                                                  <td class="Mi_tbanchoD" colspan="4" ><asp:CheckBox  runat="server" ID="chkkamban" Text="Tipo Kamban" CssClass="MI_label"  TextAlign="Left" /></td>
                                            </tr>
                                            <tr class="Mi_tbalto">
                                                <td id="tdrequiereItem" colspan="4" >
                                                    <fieldset>
                                                        <legend class="MI_label">Requiere:</legend>
                                                        <asp:CheckBoxList ID="chkListaRequiereIa" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Table" CausesValidation="false" CssClass="MI_label"  CellSpacing="3" CellPadding="2">
                                                            <asp:ListItem Text="Plano" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Modelo" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="Tipo" Value="3"></asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </fieldset>
                                                </td>
                                            </tr>
                                                   <tr class="Mi_tbalto">
                                                <td id="tdrequiereCalidad" colspan="4" >
                                                    <fieldset>
                                                        <legend class="MI_label">Calidad:</legend>
                                                        <asp:CheckBox ID="chk_InspCalidad" Text=" Requiere inspecion de calidad" AutoPostBack="true" OnCheckedChanged="chk_InspCalidad_CheckedChanged1" runat="server" />
                                                        <asp:CheckBox ID="chk_InspObligatoria" Text=" Requiere inspecion obligatoria" runat="server" AutoPostBack="true" OnCheckedChanged="chk_InspObligatoria_CheckedChanged" />                                                        
                                                    </fieldset>
                                                </td>
                                                  
                                            </tr>
                                            <tr  class="Mi_tbalto">
                                                <td class="Mi_tbanchoD">
                                                    <asp:Label ID="lblClaseItem" Text="Clase Item:"  runat="server" CssClass="MI_label"  BorderStyle="NotSet"></asp:Label></td>
                                                <td>
                                                 <asp:DropDownList ID="cboClaseItem" runat="server" AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" Width="170px" CausesValidation="false" AutoPostBack="true" ListItemHoverCssClass="style1">
                                                    </asp:DropDownList> 
                                                      </td>
                                                <td colspan="5" align="left" class="Mi_tbalto">
                                                    <asp:Label runat="server" Text="Clasificaci&oacute;n del tipo de clase(Generico, Cuñas, Bushing PLus...)" CssClass="msgFinal"></asp:Label></td>
                                            </tr>
                                           

                                            <tr class="Mi_tbalto" id="trcomboparametros">
                                                <td class="Mi_tbanchoD">
                                                    <asp:Label ID="lblParametroIa" Text="Parametro:" runat="server" CssClass="MI_label" BorderStyle="NotSet"></asp:Label></td>
                                                <td colspan="2">
                                                       <asp:DropDownList runat="server" ID="cboParametroIa" Width="260px" ></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rqfParametroIa" runat="server" Display="Dynamic" ControlToValidate="cboParametroIa" ErrorMessage="*" CssClass="msgvalidar" InitialValue=" " Enable="false" ValidationGroup="grupparam" SetFocusOnError="true"></asp:RequiredFieldValidator></td>
                                                <td>
                                                    <asp:ImageButton ID="imgBotonAgregarIa" runat="server" Width="24px" Height="24px" CausesValidation="true" ValidationGroup="grupparam" ImageUrl="Imagenes/iconagregar.png" OnClick="btnAgregar" /></td>
                                            </tr>
                                            <tr id="trgridparametros">
                                                <td colspan="8">
                                                    <asp:GridView ID="grdParametrosIa" runat="server" BackColor="White" BorderColor="#999999"
                                                        BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="848px" PageSize="3 " AllowPaging="true"
                                                        AutoGenerateColumns="False" Font-Size="8pt" Font-Names="arial" Height="16px" OnPageIndexChanging="grdParametrosIa_PageIndexChanging"
                                                        OnRowDeleting="grdParametrosIa_RowDeleting">
                                                        <AlternatingRowStyle BackColor="Gainsboro" />
                                                        <Columns>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblparametroid" runat="server" Text='<%# Eval("item_parametro_id")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltipoparametroid" runat="server" Text='<%# Eval("item_tipo_parametro_id")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="N°">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNumeroIa" runat="server" Style="text-align: center" Text='<%#Container.DataItemIndex + 1%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Descripcion" HeaderText="Descripcion">
                                                                <ControlStyle Font-Names="Arial" />
                                                                <ItemStyle Font-Names="Arial" />
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Eliminar">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="Lkbteliminar" runat="server" CommandName="Delete" CausesValidation="false">
                                                                        <asp:Image ID="Image1" ImageUrl="~/imagenes/deletered.png" ImageAlign="Middle" Width="20px" Height="20px"
                                                                            runat="server" />
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
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
                                                </td>
                                            </tr>
                                        </table>
                                        <table runat="server">
                                            <tr>
                                                <td colspan="5" align="center" id="tdcriterio">
                                                    <asp:Label runat="server" Text="Criterio de Clasificaci&oacute;n" ID="lbltitlecriterioIp"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="Mi_tbalto" id="trplan1">
                                                <td class="Mi_tbanchoD">
                                                    <asp:Label runat="server" Text="Plan 1:" ID="lblPlan1Ip"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="cboPlan1Ip" Width="250px"  AutoPostBack="true" OnSelectedIndexChanged="cboPlan1Ip_SelectedIndexChanged" CausesValidation="false"></asp:DropDownList>
                                                </td>
                                                <td style="width: 6px">&nbsp;</td>
                                                <td class="Mi_tbanchoD">
                                                    <asp:Label runat="server" Text="Plan 2:" ID="lblPlan2Ip"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="cboPlan2Ip" Width="250px"  AutoPostBack="true" OnSelectedIndexChanged="cboPlan2Ip_SelectedIndexChanged" CausesValidation="false"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr class="Mi_tbalto" id="trplan2">
                                                <td class="Mi_tbanchoD">
                                                    <asp:Label runat="server" Text="Plan 3:" ID="lblPlan3Ip"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="cboPlan3Ip" Width="250px"  AutoPostBack="true" OnSelectedIndexChanged="cboPlan3Ip_SelectedIndexChanged" CausesValidation="false"></asp:DropDownList>
                                                </td>
                                                <td style="width: 6px">&nbsp;</td>
                                                <td class="Mi_tbanchoD">
                                                    <asp:Label runat="server" Text="Posici&oacute;n Arancelaria:" ID="lblPosAranIp"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtPosAranIp" Width="200px" Enabled="false" MaxLength="0" Style="text-align: right"></asp:TextBox>
                                                </td>
                                            </tr>

                                              <tr class="Mi_tbalto" id="trplan3">
                                                <td class="Mi_tbanchoD">
                                                    <asp:Label runat="server" Text="Plan 4:" ID="Label3"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="cboPlan4Ip" Width="250px"  AutoPostBack="true"  CausesValidation="false" OnSelectedIndexChanged="cboPlan4Ip_SelectedIndexChanged"></asp:DropDownList>
                                                </td>
                                                <td style="width: 6px">&nbsp;</td>
                                                <td class="Mi_tbanchoD">
                                                    <asp:Label runat="server" Text="Plan 5:" ID="Label18"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="cboPlan5Ip" Width="250px"  AutoPostBack="true"  CausesValidation="false"></asp:DropDownList>
                                                </td>
                                            </tr>

                                        </table>
                                    </Content>
                                </asp:AccordionPane>
                                <asp:AccordionPane runat="server" ID="AccordPanePrecioIp" ContentCssClass="accordionContent"
                                    Font-Bold="False" Font-Names="Arial" Font-Size="8pt" HeaderCssClass="accordionHeader"
                                    HeaderSelectedCssClass="accordionHeaderSelected" Visible="false">
                                    <Header>
                                        <asp:Label runat="server" Text="PRECIO" ID="AccordPrecioIp"></asp:Label>
                                    </Header>
                                    <Content>
                                        <table>
                                            <tr class="Mi_tbalto">
                                                <td style="width: 70px" class="Mi_tbanchoD">
                                                    <asp:Label Text="*Moneda:" runat="server" CssClass="MI_label" ID="lblMonedaIp"></asp:Label>
                                                </td>
                                                <td>
                                                   <asp:TextBox  Enabled="false" runat="server" ID="txtmonedaip"></asp:TextBox>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td class="Mi_tbanchoD">
                                                    <asp:Label runat="server" Text="*Costo:" ID="lblPrecioPlenoIp"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtPrecioPlenoIp" Width="110px"  MaxLength="19" Style="text-align: right" OnTextChanged="txtPrecioPlenoIp_TextChanged" CausesValidation="false"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rqftxtPrecioPlenoIp" runat="server" Display="Dynamic" ControlToValidate="txtPrecioPlenoIp" ErrorMessage="*" CssClass="msgvalidar" InitialValue=" " SetFocusOnError="true" ValidationGroup="grupPrecio"></asp:RequiredFieldValidator>
                                                </td>
                                                <td class="Mi_tbanchoD">
                                                    <asp:Label runat="server" Text="TRM:" ID="lbltrm"></asp:Label>
                                                </td>
                                                  <td>
                                                    <asp:TextBox runat="server" ID="txtTrm" Width="110px"  MaxLength="19" Style="text-align: right" OnTextChanged="txtTrm_TextChanged" CausesValidation="false"></asp:TextBox>
                                                </td>
                                                <td style="width: 22px">&nbsp;</td>
                                                <td>
                                                    <asp:ImageButton runat="server" Width="24px" Height="24px" ImageUrl="Imagenes/iconagregar.png" ID="btnAgregarIp"  OnClick="btnAgregarIp_Click" ValidationGroup="grupPrecio" />
                                                </td>
                                                <td>
                                                    <asp:ImageButton runat="server" Width="24px" Height="24px" ImageUrl="Imagenes/edit_clear.png" ID="btnlimpiarprecio"  OnClick="btnlimpiarprecio_Click" CausesValidation="false"/>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:GridView runat="server" ID="grdPrecioIp" BackColor="White" BorderColor="#999999"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="850px" PageSize="10" AllowPaging="true"
                                            AutoGenerateColumns="False" Font-Size="8pt" Font-Names="arial" Height="16px" OnPageIndexChanging="grdPrecioIp_PageIndexChanging" OnRowDataBound="grdPrecioIp_RowDataBound" > 
                                            <AlternatingRowStyle BackColor="Gainsboro"/>
                                            <Columns>
                                                <asp:TemplateField HeaderText="N°">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%#Container.DataItemIndex + 1%>' ID="lblnumPrecio"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblplantaprecio_id" runat="server" Text='<%# Eval("item_planta_precio_id")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmonedaid" runat="server" Text='<%# Eval("moneda_id")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField  HeaderText="Margen">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtporcentaje" runat="server" Text='<%# Eval("margen")%>'  CausesValidation="false"  style="text-align: right" AutoPostBack="true" OnTextChanged="txtporcentaje_TextChanged"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:BoundField DataField="cliente_tipo" HeaderText="Tipo Cliente"  ReadOnly="true" />
                                                 <asp:TemplateField HeaderText="Precio">
                                                    <ItemTemplate >
                                                          <asp:TextBox ID="txt_valor" runat="server" Text='<%# Eval("valor")%>'  CausesValidation="false"  style="text-align: right" AutoPostBack="true" OnTextChanged="txt_valor_TextChanged"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="178px" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="moneda" HeaderText="Moneda"  ReadOnly="true"/>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcliente_tipo_planta_id" runat="server" Text='<%# Eval("cliente_tipo_planta_id")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:BoundField DataField="Costo" HeaderText="Costo"  ReadOnly="true"/>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcliente_tipo_planta_Costo" runat="server" Text='<%# Eval("Costo")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="TRM" HeaderText="TRM"  ReadOnly="true"/>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcliente_tipo_planta_TRM" runat="server" Text='<%# Eval("TRM")%>'></asp:Label>
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
                                    </Content>
                                </asp:AccordionPane>

                                  <asp:AccordionPane runat="server" ID="AccordionPanelAcc" ContentCssClass="accordionContent"
                                    Font-Bold="False" Font-Names="Arial" Font-Size="8pt" HeaderCssClass="accordionHeader"
                                    HeaderSelectedCssClass="accordionHeaderSelected" Visible="TRUE">
                                    <Header>
                                        <asp:Label runat="server" Text="DIMENSIONES ACCESORIO" ID="AccordionPAcc"></asp:Label>
                                    </Header>
                                    <Content>
                                        <table>                                        
                                            <tr>                                               
                                                <td>                                                  
                                                  <asp:Label ID="LblAnulado" runat="server"  Visible="false"></asp:Label>                     
                                                     <asp:Label ID="LblIdAccesorio" runat="server"  Visible="false"></asp:Label>                       
                                                      <asp:Label ID="Label6" runat="server" Text="Accesorio:"></asp:Label>
                                                    <asp:TextBox ID="txt_NombAcce" runat="server" Width="300px" MaxLength="80" style="text-transform :uppercase"></asp:TextBox>
                                                       <asp:Label ID="Label20" runat="server"  Text="*" ForeColor="Red"></asp:Label>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                </td> 
                                                <td>
                                                    <asp:Label ID="Label2" runat="server" Text="Descripcion Abrev:"></asp:Label>
                                                <asp:TextBox ID="txt_Desc_Abrev" runat="server" Width="100px" MaxLength="80" style="text-transform :uppercase"></asp:TextBox> 
                                                     <asp:Label ID="LblEstadoAnula" runat="server" Text=""></asp:Label>                                              
                                                </td>                                                                                                                                           
                                            </tr>
                                        </table>                                                                                                        
                                        <table>
                                            <tr>
                                                <td>                                                     
                                                    <asp:Label ID="Label4" runat="server" Text="Dimension 1 Min:"></asp:Label>
                                                    <asp:TextBox ID="txt_Dim1min" runat="server" Width="70px"></asp:TextBox>                                                                                   
                                                   &nbsp;&nbsp;&nbsp;
                                               <asp:Label ID="Label5" runat="server" Text="Dimension 1 Max:"></asp:Label>
                                        <asp:TextBox ID="txt_Dim1max" runat="server"   Width="70px"></asp:TextBox>
                                                </td>
                                                </tr>
                                            <tr>
                                                <td>

                                                    <asp:Label ID="Label1" runat="server" Text="Dimension 2 Min:"></asp:Label>
                                                    <asp:TextBox ID="txt_Dim2min" runat="server" Width="70px"></asp:TextBox>
                                                    &nbsp;&nbsp;&nbsp;
                                               <asp:Label ID="Label7" runat="server" Text="Dimension 2 Max:"></asp:Label>
                                                    <asp:TextBox ID="txt_Dim2max" runat="server" Width="70px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label8" runat="server" Text="Dimension 3 Min:"></asp:Label>
                                                    <asp:TextBox ID="txt_Dim3min" runat="server" Width="70px"></asp:TextBox>
                                                    &nbsp;&nbsp;&nbsp;
                                               <asp:Label ID="Label9" runat="server" Text="Dimension 3 Max:"></asp:Label>
                                                    <asp:TextBox ID="txt_Dim3max" runat="server" Width="70px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label10" runat="server" Text="Dimension 4 Min:"></asp:Label>
                                                    <asp:TextBox ID="txt_Dim4min" runat="server" Width="70px"></asp:TextBox>
                                                    &nbsp;&nbsp;&nbsp;
                                               <asp:Label ID="Label11" runat="server" Text="Dimension 4 Max:"></asp:Label>
                                                    <asp:TextBox ID="txt_Dim4max" runat="server" Width="70px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label12" runat="server" Text="Dimension 5 Min:"></asp:Label>
                                                    <asp:TextBox ID="txt_Dim5min" runat="server" Width="70px"></asp:TextBox>
                                                    &nbsp;&nbsp;&nbsp;
                                               <asp:Label ID="Label13" runat="server" Text="Dimension 5 Max:"></asp:Label>
                                                    <asp:TextBox ID="txt_Dim5max" runat="server" Width="70px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label14" runat="server" Text="Dimension 6 Min:"></asp:Label>
                                                    <asp:TextBox ID="txt_Dim6min" runat="server" Width="70px"></asp:TextBox>
                                                    &nbsp;&nbsp;&nbsp;
                                               <asp:Label ID="Label15" runat="server" Text="Dimension 6 Max:"></asp:Label>
                                                    <asp:TextBox ID="txt_Dim6max" runat="server" Width="70px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label16" runat="server" Text="Dimension 7 Min:"></asp:Label>
                                                    <asp:TextBox ID="txt_Dim7min" runat="server" Width="70px"></asp:TextBox>
                                                    &nbsp;&nbsp;&nbsp;
                                               <asp:Label ID="Label17" runat="server" Text="Dimension 7 Max:"></asp:Label>
                                                    <asp:TextBox ID="txt_Dim7max" runat="server" Width="70px"></asp:TextBox>
                                                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                                                                               <asp:Button runat="server" CssClass="Mi_botones" Width="80px" Text="Anular" ID="btn_Anular" Visible="false" OnClick="btn_Anular_Click"/>       
                                                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                                                                                                                                                                                                     
                                                     <br />  <br />       
                                                </td>                                                
                                                                                                                                                                                   
                                            </tr>                                                                                                                                                                                                                  
                                        </table> 
                                        <table>
                                            <tr>
                                                <td>
   <asp:GridView ID="GridAccesorios" runat="server" BackColor="White" BorderColor="#999999"
                                  BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="520px"
                                  AutoGenerateColumns="False" Font-Size="8pt" Font-Names="arial" Height="16px" PageSize="8" 
                                  ShowHeaderWhenEmpty="True">
                                  <Columns>
                                            <asp:TemplateField HeaderText="Codigo">
                                          <EditItemTemplate>
                                              <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Id_UnoE") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label4" runat="server" Text='<%# Bind("Id_UnoE") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Nomenclatura">
                                          <EditItemTemplate>
                                              <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Nomenclatura") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label4" runat="server" Text='<%# Bind("Nomenclatura") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Descripcion Auxiliar"  HeaderStyle-Width="200px">
                                          <EditItemTemplate>
                                              <asp:TextBox ID="TextBox2" Width="250px" runat="server" Text='<%# Bind("Des_Aux") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label1" runat="server" Text='<%# Bind("Des_Aux") %>'></asp:Label>
                                          </ItemTemplate>
                                            <HeaderStyle Width="200px"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="left" />
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText=" Dimension 1 min"  HeaderStyle-Width="40px"> 
                                          <EditItemTemplate>
                                              <asp:TextBox ID="TextBox3"  Width="40PX" runat="server" Text='<%# Bind("Valor_1_Min") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label2" runat="server" Text='<%# Bind("Valor_1_Min") %>'></asp:Label>
                                          </ItemTemplate>
                                          <HeaderStyle Width="40px"></HeaderStyle>
                                          <ItemStyle HorizontalAlign="Right" />
                                      </asp:TemplateField>
                                               <asp:TemplateField HeaderText=" Dimension 1 max"  HeaderStyle-Width="40px"> 
                                          <EditItemTemplate>
                                              <asp:TextBox ID="TextBox4"  Width="40PX" runat="server" Text='<%# Bind("Valor_1_Max") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label2" runat="server" Text='<%# Bind("Valor_1_Max") %>'></asp:Label>
                                          </ItemTemplate>
                                          <HeaderStyle Width="40px"></HeaderStyle>
                                          <ItemStyle HorizontalAlign="Right" />
                                      </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" Dimension 2 min"  HeaderStyle-Width="40px"> 
                                          <EditItemTemplate>
                                              <asp:TextBox ID="TextBox5"  Width="40PX" runat="server" Text='<%# Bind("Valor_2_Min") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label2" runat="server" Text='<%# Bind("Valor_2_Min") %>'></asp:Label>
                                          </ItemTemplate>
                                          <HeaderStyle Width="40px"></HeaderStyle>
                                          <ItemStyle HorizontalAlign="Right" />
                                      </asp:TemplateField>
                                             <asp:TemplateField HeaderText=" Dimension 2 max"  HeaderStyle-Width="40px"> 
                                          <EditItemTemplate>
                                              <asp:TextBox ID="TextBox6"  Width="40PX" runat="server" Text='<%# Bind("Valor_2_Max") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label2" runat="server" Text='<%# Bind("Valor_2_Max") %>'></asp:Label>
                                          </ItemTemplate>
                                          <HeaderStyle Width="40px"></HeaderStyle>
                                          <ItemStyle HorizontalAlign="Right" />
                                      </asp:TemplateField>
                                                  <asp:TemplateField HeaderText=" Dimension 3 min"  HeaderStyle-Width="40px"> 
                                          <EditItemTemplate>
                                              <asp:TextBox ID="TextBox7"  Width="40PX" runat="server" Text='<%# Bind("Valor_3_Min") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label2" runat="server" Text='<%# Bind("Valor_3_Min") %>'></asp:Label>
                                          </ItemTemplate>
                                          <HeaderStyle Width="40px"></HeaderStyle>
                                          <ItemStyle HorizontalAlign="Right" />
                                      </asp:TemplateField>
                                                    <asp:TemplateField HeaderText=" Dimension 3 max"  HeaderStyle-Width="40px"> 
                                          <EditItemTemplate>
                                              <asp:TextBox ID="TextBox8"  Width="40PX" runat="server" Text='<%# Bind("Valor_3_Max") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label2" runat="server" Text='<%# Bind("Valor_3_Max") %>'></asp:Label>
                                          </ItemTemplate>
                                          <HeaderStyle Width="40px"></HeaderStyle>
                                          <ItemStyle HorizontalAlign="Right" />
                                      </asp:TemplateField>
                                       <asp:TemplateField HeaderText=" Dimension 4 min"  HeaderStyle-Width="40px"> 
                                          <EditItemTemplate>
                                              <asp:TextBox ID="TextBox9"  Width="40PX" runat="server" Text='<%# Bind("Valor_4_Min") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label2" runat="server" Text='<%# Bind("Valor_4_Min") %>'></asp:Label>
                                          </ItemTemplate>
                                          <HeaderStyle Width="40px"></HeaderStyle>
                                          <ItemStyle HorizontalAlign="Right" />
                                      </asp:TemplateField>
                                                       <asp:TemplateField HeaderText=" Dimension 4 max"  HeaderStyle-Width="40px"> 
                                          <EditItemTemplate>
                                              <asp:TextBox ID="TextBox10"  Width="40PX" runat="server" Text='<%# Bind("Valor_4_Max") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label2" runat="server" Text='<%# Bind("Valor_4_Max") %>'></asp:Label>
                                          </ItemTemplate>
                                          <HeaderStyle Width="40px"></HeaderStyle>
                                          <ItemStyle HorizontalAlign="Right" />
                                      </asp:TemplateField>
                                         <asp:TemplateField HeaderText=" Dimension 5 min"  HeaderStyle-Width="40px"> 
                                          <EditItemTemplate>
                                              <asp:TextBox ID="TextBox11"  Width="40PX" runat="server" Text='<%# Bind("Valor_5_Min") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label2" runat="server" Text='<%# Bind("Valor_5_Min") %>'></asp:Label>
                                          </ItemTemplate>
                                          <HeaderStyle Width="40px"></HeaderStyle>
                                          <ItemStyle HorizontalAlign="Right" />
                                      </asp:TemplateField>
                                                  <asp:TemplateField HeaderText=" Dimension 5 max"  HeaderStyle-Width="40px"> 
                                          <EditItemTemplate>
                                              <asp:TextBox ID="TextBox12"  Width="40PX" runat="server" Text='<%# Bind("Valor_5_Max") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label2" runat="server" Text='<%# Bind("Valor_5_Max") %>'></asp:Label>
                                          </ItemTemplate>
                                          <HeaderStyle Width="40px"></HeaderStyle>
                                          <ItemStyle HorizontalAlign="Right" />
                                      </asp:TemplateField>
                                                 <asp:TemplateField HeaderText=" Dimension 6 min"  HeaderStyle-Width="40px"> 
                                          <EditItemTemplate>
                                              <asp:TextBox ID="TextBox13"  Width="40PX" runat="server" Text='<%# Bind("Valor_6_Min") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label2" runat="server" Text='<%# Bind("Valor_6_Min") %>'></asp:Label>
                                          </ItemTemplate>
                                          <HeaderStyle Width="40px"></HeaderStyle>
                                          <ItemStyle HorizontalAlign="Right" />
                                      </asp:TemplateField>
                                                  <asp:TemplateField HeaderText=" Dimension 6 max"  HeaderStyle-Width="40px"> 
                                          <EditItemTemplate>
                                              <asp:TextBox ID="TextBox14"  Width="40PX" runat="server" Text='<%# Bind("Valor_6_Max") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label2" runat="server" Text='<%# Bind("Valor_6_Max") %>'></asp:Label>
                                          </ItemTemplate>
                                          <HeaderStyle Width="40px"></HeaderStyle>
                                          <ItemStyle HorizontalAlign="Right" />
                                      </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" Dimension 7 min"  HeaderStyle-Width="40px"> 
                                          <EditItemTemplate>
                                              <asp:TextBox ID="TextBox15"  Width="40PX" runat="server" Text='<%# Bind("Valor_7_Min") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label2" runat="server" Text='<%# Bind("Valor_7_Min") %>'></asp:Label>
                                          </ItemTemplate>
                                          <HeaderStyle Width="40px"></HeaderStyle>
                                          <ItemStyle HorizontalAlign="Right" />
                                      </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" Dimension 7 max"  HeaderStyle-Width="40px"> 
                                          <EditItemTemplate>
                                              <asp:TextBox ID="TextBox16"  Width="40PX" runat="server" Text='<%# Bind("Valor_7_Max") %>'></asp:TextBox>
                                          </EditItemTemplate>
                                          <ItemTemplate>
                                              <asp:Label ID="Label2" runat="server" Text='<%# Bind("Valor_7_Max") %>'></asp:Label>
                                          </ItemTemplate>
                                          <HeaderStyle Width="40px"></HeaderStyle>
                                          <ItemStyle HorizontalAlign="Right" />
                                      </asp:TemplateField>            

                                   
                                  </Columns>
                                  <EditRowStyle HorizontalAlign="Right" />
                                  <EmptyDataRowStyle BorderStyle="Solid" />
                                  <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                  <HeaderStyle BackColor="#1C5AB6" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                  <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                  <RowStyle BackColor="#EEEEEE" ForeColor="Black" HorizontalAlign="Left" />
                                  <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                  <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                  <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                  <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                  <SortedDescendingHeaderStyle BackColor="#000065" />
                              </asp:GridView>
                                                </td>
                                            </tr>
                                        </table>
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
                                      </Content>
                                </asp:AccordionPane>
                            </Panes>
                        </asp:Accordion>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" align="left" class="Mi_tbalto">
                        <asp:Label runat="server" Text="* Este campo es obligatorio" CssClass="msgFinal"></asp:Label></td>
                </tr>
                <tr id="trtxtobservacion">
                    <td colspan="5" align="left"><asp:Label Text="Observaci&oacuten:" runat="server" CssClass="MI_label" ID="lblobservestado"></asp:Label></td>
                </tr>
                 <tr id="trlblobservacion">
                    <td colspan="5" align="left" class="Mi_tbalto"><asp:TextBox  TextMode="multiline" Columns="50" Rows="5" runat="server"   ID="txtobservestado"></asp:TextBox> 
                    </td>
                </tr>                
                <tr>
                    <td colspan="5" align="center" class="Mi_tbalto">
                        <asp:Button runat="server" CssClass="Mi_botones" Width="80px" Text="Guardar" ID="btnGuardarIp" OnClick="btnGuardarIp_Click" />
                        <asp:Button runat="server" CssClass="Mi_botones" Width="80px" Text="Enviar" ID="btnEnviarIp" OnClick="btnEnviarIp_Click" Visible="false"/>
                        <asp:Button runat="server" CssClass="Mi_botones" Width="80px" Text="Nuevo" ID="btnLimpiarIp" OnClick="btnLimpiarIp_Click" CausesValidation="false"/>
                        <asp:Button runat="server" CssClass="Mi_botones" Width="80px" Text="Aprobar" ID="btnAprobarIp" Visible="false" OnClick="btnAprobarIp_Click"  />
                        <asp:Button runat="server" CssClass="Mi_botones" Width="80px" Text="Rechazar" ID="btnDevolverIp" Visible="false"  OnClick="btnDevolverIp_Click" />
                        <asp:Button runat="server" CssClass="Mi_botones" Width="80px" Text="Anular" ID="btnRechazarIp" Visible="false"  OnClick="btnRechazarIp_Click" />
                         <asp:Button runat="server" CssClass="Mi_botones" Width="80px" Text="Duplicar" ID="btnduplicar" Visible="false"  OnClick="btnduplicar_Click" />
                         <asp:Button runat="server" CssClass="Mi_botones" Width="80px" Text="Ver Reporte" ID="btnreporteplanta" OnClientClick="javascript:ir();" CausesValidation="false"  />
                        <asp:Button runat="server" CssClass="Mi_botones" Width="80px" Text="Ver Log Item" ID="btnVerLogItem" OnClientClick="javascript:irLog();" CausesValidation="false"  />
                    </td>
                </tr>
                 <tr>
                     <td colspan="5">
                        <hr />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="overlay" />
            <div class="overlayContent">
                <asp:Label ID="lblEnviando" runat="server" Text="Cargando..." Font-Names="Arial"
                    Font-Size="14pt"></asp:Label>
                <img src="Imagenes/ajax-loader.gif" alt="Loading" height="30" style="text-align: center"
                    width="30" />
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
