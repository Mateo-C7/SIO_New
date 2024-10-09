<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="ListaChequeo.aspx.cs" Inherits="SIO.ListaChequeo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Navigate() {
        javascript: window.open("ReporteListaChequeo.aspx");
    }
    </script>
    <script type="text/javascript">
        function Foto() {
            javascript: window.showModalDialog("SubirFoto.aspx", '', 'dialogHeight:400 px;dialogWidth:350px;center:Yes;help:No;resizable: No;status:No;');
            window.location.href = window.location.href;
        }
    </script>
    <script type="text/javascript">
        function MGuardarIG() {
            alert("Información general ingresada con éxito.");
        }
    </script>
    <script type="text/javascript">
        function MAprobar() {
            alert("Lista de chequeo aprobada con éxito.");
        }
    </script>
    <script type="text/javascript">
        function MFinalizar() {
            alert("Lista de chequeo finalizada con éxito.");
        }
    </script>
    <script type="text/javascript">
        function MGuardarLista() {
            alert("Lista de chequeo ingresada con éxito.");
        }
    </script>
    <script type="text/javascript">
        function TabChanged(sender, args) {
            sender.get_clientStateField().value =
                sender.saveClientState();
        }
    </script>
    <style type="text/css">
    /* Accordion */
        .accordionHeader
        {
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
 
        #master_content .accordionHeader a
        {
            color: #FFFFFF;
            background: none;
            text-decoration: none;
        }
 
        #master_content .accordionHeader a:hover
        {
            background: none;
            text-decoration: underline;
        }
 
        .accordionHeaderSelected
        {
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
 
        #master_content .accordionHeaderSelected a
        {
            color: #FFFFFF;
            background: none;
            text-decoration: none;
        }
 
        #master_content .accordionHeaderSelected a:hover
        {
            background: none;
            text-decoration: underline;
        }
 
        .accordionContent
        {
            
            border: 0px outset #2F4F4F;
            border-top: none;
            padding: 5px;
            padding-top: 10px;
        }
        .modalPopup {
	        background-color:#FFFFC0;
	        border-width:1px;
	        border-style:Solid;
	        border-color:Gray;
	        padding:1px;
	        width:250px;
        }
        
        .overlay  
        {
          position: fixed;
          z-index: 98;
          top: 0px;
          left: 0px;
          right: 0px;
          bottom: 0px;
            background-color: #aaa; 
            filter: alpha(opacity=80); 
            opacity: 0.8; 
        }
        .overlayContent
        {
          z-index: 99;
          margin: 250px auto;
          width: 80px;
          height: 80px;
        }
        .overlayContent h2
        {
            font-size: 18px;
            font-weight: bold;
            color: #000;
        }
        .overlayContent img
        {
          width: 80px;
          height: 80px;
        }
        .style16
        {
            height: 4203px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellspacing="1" style="width: 1000px" width="1500">
                <tr>
                    <td>
                        <asp:Panel ID="pnlBusqueda" runat="server"  Font-Names="Arial" Font-Size="8pt"
                            GroupingText="Busqueda" Height="116px" Width="178px">
                            <table style="height: 95px">
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblProducido" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Producido En "
                                            Width="70px"></asp:Label>
                                    </td>
                                    <td style="text-align: justify">
                                        <asp:DropDownList ID="cboProducido" runat="server" AutoPostBack="True" DropDownStyle="DropDownList"
                                            Font-Names="Arial" Font-Size="8pt" Width="85px">
                                            <asp:ListItem>Colombia</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblOF" runat="server" Font-Names="Arial" Font-Size="8pt" Text="OF"
                                            Width="40px"></asp:Label>
                                    </td>
                                    <td style="text-align: justify">
                                        <asp:TextBox ID="txtOF" runat="server" BackColor="#FFFF66" Font-Names="Arial" Font-Size="8pt"
                                            Width="80px" AutoPostBack="True" OnTextChanged="txtOF_TextChanged"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblFUP" runat="server" Font-Names="Arial" Font-Size="8pt" Text="FUP"
                                            Width="30px"></asp:Label>
                                    </td>
                                    <td style="text-align: justify">
                                        <asp:Label ID="LFUP" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="#0000CC"
                                            Width="100px" Font-Italic="True" Text="0"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" BackColor="#1C5AB6" BorderColor="#999999"
                                            Font-Bold="True" Font-Names="Arial" Font-Size="8pt"  Width="70px" ForeColor="White"
                                            OnClick="btnNuevo_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td style="width: 20px">
                        &nbsp;
                        </td>
                    <td>
                        <asp:Panel ID="pnlDatosGenerales" runat="server" Font-Names="Arial"
                            Font-Size="8pt" GroupingText="Datos Generales" Height="116px" Width="604px">
                            <table style="height: 94px">
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblPais" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Pais"
                                            Width="45px">
                                        </asp:Label>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:Label ID="LPais" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="#0000CC"
                                            Width="200px" Font-Italic="True" Text="Sin"> 
                                        </asp:Label>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblCiudad" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Ciudad"
                                            Width="40px">
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="LCiudad" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="#0000CC"
                                            Width="200px" Font-Italic="True" Text="Sin"> 
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblCliente" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Empresa"
                                            Width="70px">
                                        </asp:Label>
                                    </td>
                                    <td colspan="3" style="text-align: Left">
                                        <asp:Label ID="LCliente" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="#0000CC"
                                            Width="500px" Font-Italic="True" Text="Sin"> 
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblContacto" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Contacto"
                                            Width="70px">
                                        </asp:Label>
                                    </td>
                                    <td colspan="3" style="text-align: Left">
                                        <asp:Label ID="LContacto" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="#0000CC"
                                            Width="500px" Font-Italic="True" Text="Sin"> 
                                        </asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblObra" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            Text="Obra" Width="50px">
                                        </asp:Label>
                                    </td>
                                    <td colspan="3" style="text-align: Left">
                                        <asp:Label ID="LObra" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="#0000CC"
                                            Width="500px" Font-Italic="True" Text="Sin"> 
                                        </asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center">
                        <asp:Label ID="lblEstado" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Text="ESTADO" Visible="False" Width="60px"></asp:Label>
                        <asp:Label ID="LEstado" runat="server" Font-Italic="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="#0000CC" Style="text-align: left" Visible="False" Width="180px"></asp:Label>
                        <asp:Label ID="lblodigo" runat="server" ForeColor="Black" 
                            Text="CC-FR-05 R7 2015-04-16"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" style="text-align: center">
                        
                        <asp:Accordion ID="Accordion1" runat="server" 
                            ContentCssClass="accordionContent" Font-Names="Arial" Font-Size="8pt" 
                            HeaderCssClass="accordionHeader" 
                            HeaderSelectedCssClass="accordionHeaderSelected" Width="850px">
                            <Panes>
                                <asp:AccordionPane ID="AcorInfoGeneral" runat="server" 
                                    ContentCssClass="accordionContent" Font-Bold="False" Font-Names="Arial" 
                                    Font-Size="8pt" HeaderCssClass="accordionHeader" 
                                    HeaderSelectedCssClass="accordionHeaderSelected">
                                    <Header>
                                        <asp:Label ID="lblEncabGeneral" runat="server" Text="INFORMACIÓN GENERAL">
                                        </asp:Label>
                                    </Header>
                                    <Content>
                                        <table>
                                            <tr>
                                                <td style="text-align: right">
                                                    <asp:Label ID="lblTipoArmado" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        Text="Tipo De Armado" Width="100px">
                                                    </asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:TextBox ID="txtArmado" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        Width="200px"> 
                                                    </asp:TextBox>
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Label ID="LTecnico" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                                        Text="Técnico Lider" Width="100px">
                                                    </asp:Label>
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:DropDownList ID="cboTecnico" runat="server" Font-Names="Arial" 
                                                        Font-Size="8pt" Width="200px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" style="text-align: right">
                                                    <asp:Button ID="btnGuardar" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                                        BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                                         OnClick="btnGuardar_Click" OnClientClick="MGuardarIG()" 
                                                        Text="Guardar" Width="70px" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnReporte" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                                        BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                                         OnClick="btnReporte_Click" OnClientClick="Navigate()" 
                                                        Text="Reporte Lista" Width="100px" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnAprobar" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                                        BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                                         OnClick="btnAprobar_Click" OnClientClick="MAprobar()" 
                                                        Text="Aprobar Lista" Width="100px" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btnFinalizar" runat="server" BackColor="#1C5AB6" ForeColor="White"
                                                        BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                                         OnClick="btnFinalizar_Click" OnClientClick="MFinalizar()" 
                                                        Text="Finalizar Lista " Width="100px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </Content>
                                </asp:AccordionPane>
                                <asp:AccordionPane ID="AccEncuesta" runat="server" 
                                    ContentCssClass="accordionContent" Font-Bold="False" Font-Names="Arial" 
                                    Font-Size="8pt" HeaderCssClass="accordionHeader" 
                                    HeaderSelectedCssClass="accordionHeaderSelected">
                                    <Header>
                                        <asp:Label ID="LChequeo" runat="server" Text="LISTA DE CHEQUEO"></asp:Label>
                                    </Header>
                                    <Content>
                                        <asp:TabContainer ID="tabcEncuesta" runat="server" ActiveTabIndex="0" 
                                            AutoPostBack="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" 
                                            Style="width: 835px;" VerticalStripWidth="150px" Visible="False">
                                        </asp:TabContainer>
                                        <asp:Button ID="btnGrabaenCliente" runat="server" BackColor="#1C5AB6" 
                                            BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                             OnClick="btnGrabar_Click" OnClientClick="MGuardarLista()" 
                                            Text="Grabar" Visible="false" Width="70px" />
                                    </Content>
                                </asp:AccordionPane>
                                <asp:AccordionPane ID="AccRegFoto" runat="server" 
                                    ContentCssClass="accordionContent" Font-Bold="False" Font-Names="Arial" 
                                    Font-Size="8pt" HeaderCssClass="accordionHeader" 
                                    HeaderSelectedCssClass="accordionHeaderSelected">
                                    <Header>
                                        <asp:Label ID="LRegFoto" runat="server" Text="REGISTRO FOTOGRAFICO"></asp:Label>
                                    </Header>
                                    <Content>
                                        <table>
                                            <tr>
                                                <td style="width: 20px">
                                                </td>
                                                <td style="text-align: left">
                                                    <asp:LinkButton ID="lkSubirFotos" runat="server" BackColor="#1C5AB6" 
                                                        BorderColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial" 
                                                        Font-Size="8pt"  Height="20px" OnClick="lkSubirFotos_Click" 
                                                        OnClientClick="Foto()" Style="text-align: center" Width="100px">Subir Fotos</asp:LinkButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:GridView ID="grvArchivo" runat="server" AutoGenerateColumns="False" 
                                                        CellPadding="1" CellSpacing="4" DataKeyNames="id_plano" Font-Names="Arial" 
                                                        Font-Size="8pt" ForeColor="#333333" GridLines="None">
                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="idFoto" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFoto" runat="server" Text='<%# Bind("ID_PLANO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtFoto" runat="server"></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Descripción">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDesc" runat="server" Text='<%# Bind("NOMBRE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Ruta Archivo">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="simpa_anexoEditLink" runat="server" 
                                                                        OnPreRender="anexo_OnClick" Text='<%# Eval("RUTA") %>' />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EditRowStyle BackColor="#999999" />
                                                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True"  />
                                                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True"  />
                                                        <PagerStyle BackColor="#284775"  HorizontalAlign="Center" />
                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
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
                    <td colspan="3" height="1000">
                        &nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
