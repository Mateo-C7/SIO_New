<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralGrande.Master" AutoEventWireup="true"
    CodeBehind="CargaEstibas2.aspx.cs" Inherits="SIO.CargaEstibas2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="http://code.jquery.com/jquery-1.4.1.min.js"></script>
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
        .style32
        {
            width: 100%;
            height: 302px;
            font-family: Arial, Helvetica, sans-serif;
        }
        .style82
        {
            width: 118%;
            height: 490px;
            margin-right: 0px;
        }
        .style83
        {
            height: 404px;
        }
        .style84
        {
            text-align: right;
        }
        .style120
        {
            width: 80px;
            font-family: Arial;
            font-size: 22px;
            height: 6px;
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
        .style280
        {
            font-size: 22px;
            color: #1C5AB6;
            font-family: Arial;
            text-align: right;
            height: 30px;
        }
        .style295
        {
            width: 695px;
            height: 44px;
            font-size: 22px;
            color: Black;
            font-family: Arial;
        }
        .style304
        {
            width: 770px;
            height: 44px;
            font-size: 22px;
            background-color: #1C5AB6;
            color: White;
            font-family: Arial;
        }
        .style305
        {
            width: 770px;
            height: 44px;
            font-size: 22px;
            color: Black;
            font-family: Arial;
        }
        .style306
        {
            height: 6px;
        }
        .style308
        {
            width: 780px;
            height: 43px;
        }
        .style309
        {
            width: 759px;
            height: 44px;
            font-size: 22px;
            background-color: #1C5AB6;
            color: White;
            font-family: Arial;
        }
        .style310
        {
            width: 759px;
            height: 44px;
            font-size: 22px;
            color: Black;
            font-family: Arial;
        }
        .style313
        {
            width: 663px;
            height: 43px;
        }
        .style314
        {
            width: 769px;
            height: 44px;
            font-size: 22px;
            color: Black;
            font-family: Arial;
        }
        .style316
        {
            width: 613px;
            height: 44px;
            font-size: 22px;
            color: Black;
            font-family: Arial;
        }
        .style317
        {
            width: 613px;
            height: 43px;
        }
        .style320
        {
            height: 6px;
            width: 129px;
        }
        .style321
        {
            width: 613px;
            height: 44px;
            font-size: 22px;
            background-color: #1C5AB6;
            color: White;
            font-family: Arial;
        }
        .style322
        {
            width: 704px;
            height: 44px;
            font-size: 22px;
            background-color: #1C5AB6;
            color: White;
            font-family: Arial;
        }
        .style323
        {
            width: 704px;
            height: 44px;
            font-size: 22px;
            color: Black;
            font-family: Arial;
        }
        .style324
        {
            width: 663px;
            height: 44px;
            font-size: 22px;
            background-color: #1C5AB6;
            color: White;
            font-family: Arial;
        }
        .style325
        {
            width: 663px;
            height: 44px;
            font-size: 22px;
            color: Black;
            font-family: Arial;
        }
        .Mystyle
        {
             width: 663px;
            height: 44px;
            font-size: 22px;
            color: Black;
            font-family: Arial;
        }
        .auto-style1 {
            width: 343px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="style82">
                <tr>
                    <td class="style120">
                        <br />
                    </td>
                    <td class="style306">
                        &nbsp;
                    </td>
                    <td class="style84">
                        &nbsp;
                    </td>
                    <td class="style320">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style83" colspan="4">
                        <asp:Label ID="lblClienteTitulo" runat="server" CssClass="sangria" Font-Bold="True"
                            Font-Names="Arial" ForeColor="#1C5AB6" Text="DESPACHO DE CARGA (CONTENEDOR)"
                            Width="350px"></asp:Label>
                        <asp:Panel ID="PanelCarga" runat="server" BackColor="White" Font-Bold="True" Font-Size="8pt"
                            GroupingText=" " Height="384px" Width="1218px" Style="margin-top: 2px">
                            <table class="style32">
                                <tr>
                                    <td class="style280" colspan="9">
                                        <a onclick="GridAyu();">Ayudantes</a>
                                    </td>
                                    <td class="style280">
                                        <a onclick="Grid();">Ver Estibas</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style324" colspan="2">
                                        <asp:Label ID="lblContenedor" runat="server" Font-Bold="False" Text="Contenedor:"
                                            Width="142px" Style="text-align: right"></asp:Label>
                                    </td>
                                    <td class="style321" colspan="2">
                                        <asp:Panel ID="Panel1" runat="server" DefaultButton="Button1" Width="208px" Height="33px">
                                            <asp:TextBox ID="txtContenedor" runat="server" Width="202px" Height="31px" Style="text-align: left"
                                                 Font-Size="20" BackColor="#1C5AB6" ForeColor="White" 
                                                ></asp:TextBox>
                                            <asp:Button ID="Button1" runat="server" Text="Button1" Style="display: none" OnClick="Button1_Click" />
                                        </asp:Panel>
                                    </td>
                                    <td class="style309" colspan="2">
                                        <asp:Label ID="lblOrden" runat="server" Font-Bold="False" Style="text-align: right"
                                            Text="Orden No.:" Width="120px"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:ComboBox ID="cboOrden" runat="server" AutoCompleteMode="Append" 
                                            DropDownStyle="DropDownList"  Font-Size="17" Width="190px" AutoPostBack="True"
                                            OnSelectedIndexChanged="cboOrden_SelectedIndexChanged">
                                        </asp:ComboBox>
                                    </td>
                                    <td class="style304">
                                        <asp:Label ID="lblDespacho1" runat="server" Font-Bold="False" Height="21px" Style="margin-left: 0px;
                                            text-align: right;" Text="Despacho:" Width="297px"></asp:Label>
                                    </td>
                                    <td class="style278">
                                        <asp:TextBox ID="txtDespacho" runat="server" BackColor="#1C5AB6" ForeColor="WhiteSmoke" Font-Size="20" 
                                            Height="30px" Style="text-align: left" Width="184px" Enabled="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style325" colspan="2">
                                        <asp:Label ID="lblPais1" runat="server" Font-Bold="False" Text="Pais:" Width="139px"
                                            Style="text-align: right"></asp:Label>
                                    </td>
                                    <td class="style316" colspan="2">
                                        <asp:Label ID="lblResulPais1" runat="server" Font-Bold="False" Text="" Width="40px"></asp:Label>
                                    </td>
                                    <td class="style310" colspan="2">
                                        <asp:Label ID="lblCiudad1" runat="server" Font-Bold="False" Style="margin-left: 2px;
                                            text-align: right;" Text="Ciudad:" Width="119px"></asp:Label>
                                    </td>
                                    <td class="style314" colspan="2">
                                        <asp:Label ID="lblResulCiudad1" runat="server" Font-Bold="False" Width="218px"></asp:Label>
                                    </td>
                                    <td class="style305">
                                        <asp:Label ID="lblPalletOrden" runat="server" Font-Bold="False" Height="29px" Text="Pallets de la Orden[Total]:"
                                            Width="298px" Style="text-align: right"></asp:Label>
                                    </td>
                                    <td class="style279">
                                        <asp:Label ID="lblPalletOrden2" runat="server" Font-Bold="False" Height="17px" Style="text-align: right"
                                            Width="61px"></asp:Label>
                                    </td>
                                </tr>
                               
                                <tr class="style278">
                                    <td class="style324" colspan="2">
                                        <asp:Label ID="lblCodEstiba1" runat="server" Font-Bold="False" Text="Pallet:" Width="139px"
                                            Style="text-align: right"></asp:Label>
                                    </td>
                                    <td class="style321" colspan="2">
                                        <asp:Panel ID="Panel2" runat="server" DefaultButton="Button2" Width="194px">
                                            <asp:TextBox ID="txtCodEstiba1" runat="server" Width="202px" Style="text-align: left"
                                                Height="41px"  Font-Size="28" onkeypress="return punto(this)"
                                                BackColor="#1C5AB6" ForeColor="White"></asp:TextBox>
                                            <asp:Button ID="Button2" runat="server" Text="Button2" Style="display: none" OnClick="Button2_Click" />
                                        </asp:Panel>
                                    </td>
                                    <td class="style278" colspan="4">
                                        <asp:Button ID="btnCargar" runat="server" BackColor="#1C5AB6" BorderColor="#999999"
                                            CssClass="botonsio" Font-Bold="True"  Text="Cargue Camion" Width="106px"
                                            Height="47px" Style="display: none" />
                                        &nbsp;&nbsp;<asp:Label ID="lblTP" runat="server" Text="Peso Total:" Font-Bold="False"
                                            Width="115px"></asp:Label>
                                        &nbsp;
                                        <asp:Label ID="lblTotaP" runat="server" Text="" Font-Bold="False"></asp:Label>
                                    </td>
                                    <td class="style304">
                                        <asp:Label ID="lblPalletCargadosOrden" runat="server" Font-Bold="False" Height="22px"
                                            Text="Pallet Cargados[Orden]:" Width="298px" Style="text-align: right"></asp:Label>
                                    </td>
                                    <td class="style278">
                                        <asp:Label ID="lblPalletCargadosOrden2" runat="server" Font-Bold="False" Height="20px"
                                            Style="text-align: right" Text="" Width="60px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style325" colspan="2">
                                        <asp:Label ID="lblPlaca1" runat="server" Font-Bold="False" Width="143px" Text="Placa/Precinto:"
                                            Style="text-align: right"></asp:Label>
                                    </td>
                                    <td class="style295" colspan="6">
                                        <asp:Label ID="lblResulPlaca1" runat="server" Font-Bold="True" Width="133px" Style="margin-left: 43px"></asp:Label>
                                    </td>
                                    <td class="style305">
                                        <asp:Label ID="lblPalletCargadosCont" runat="server" Font-Bold="False" Height="26px"
                                            Style="margin-left: 0px; text-align: right;" Text="Pallet Cargados[Contenedor]:"
                                            Width="297px"></asp:Label>
                                    </td>
                                    <td class="style279">
                                        <asp:Label ID="lblPalletCargadosCont2" runat="server" Font-Bold="False" Height="20px"
                                            Style="text-align: right" Text="" Width="60px"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="style278">
                                    <td class="style278" colspan="4">
                                        <asp:Button ID="btnAsociar" runat="server" BackColor="red" BorderColor="#999999"
                                            CssClass="botonsio" Font-Bold="True"  Height="55px" OnClick="btnAsociar_Click"
                                            OnClientClick="return confirm('Seguro que desea desasociar la Estiba?');" Text="Desasociar?" />
                                    </td>
                                    <td colspan="4">
                                        &nbsp;
                                        <asp:Label ID="lblCA" runat="server" Text=""></asp:Label>
                                        <asp:Button ID="btnCA" runat="server" Text="" Color="White" Font-Bold="True" ForeColor="Black"
                                            Height="31px" Width="130px" OnClick="btnCA_Click"  />
                                    </td>
                                    <td class="style304">
                                        <asp:Label ID="lblPalletTotalCont" runat="server" Font-Bold="False" Height="26px"
                                            Text="Pallet Total[Contenedores]:" Width="298px" Style="text-align: right"></asp:Label>
                                    </td>
                                    <td class="style278">
                                        <asp:Label ID="lblPalletTotalCont2" runat="server" Font-Bold="False" Height="20px"
                                            Style="text-align: right" Text="" Width="60px"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="style279">
                                    <td style="text-align: right" class="auto-style1">
                                        <asp:Label ID="lblPais2" runat="server" Font-Bold="False" 
                                            Style="text-align: right" Text="Piezas Faltantes:" Width="100px"></asp:Label>
                                    </td>
                                    <td colspan="2" style="text-align: right">
                                        <asp:Label ID="lblPais3" runat="server" Font-Bold="False" 
                                            Style="text-align: right" Text="Acc:" Width="50px"></asp:Label>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="lbAccesorios" runat="server" Font-Bold="False" 
                                            Style="text-align: center" Text="." Width="70px"></asp:Label>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblPais4" runat="server" Font-Bold="False" 
                                            Style="text-align: right" Text="Alum:" Width="70px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblAluminio" runat="server" Font-Bold="False" 
                                            Style="text-align: center" Text="." Width="70px"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        <asp:Label ID="lblPalletFaltantesCont" runat="server" Font-Bold="False" Height="26px"
                                            Style="text-align: right" Text="Pallet Faltantes [Contenedor]:" Width="299px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPalletFaltantesCont2" runat="server" Font-Bold="False" Height="20px"
                                            Style="text-align: right" Text="" Width="60px"></asp:Label>
                                    </td>
                                </tr>
                                <%--espacio para la descripcion=====================CRISTIAN SANCHEZ================================--%>
                                <tr>
                                    <td class="style324" colspan="4">
                                        <asp:Label ID="lblDescripcion" runat="server"  Height="31px"
                                            Style="text-align: left" Text="Observación del contenedor" Width="359px"></asp:Label>    
                                    </td>
                                     <td  colspan="6">
                                        <asp:Label ID="lblObservacion" runat="server" Width="949px" Style="text-align: left"
                                            Height="51px" Font-Size="13pt" BackColor="#1C5AB6" ForeColor="Red" ></asp:Label>
                                    </td>
                                   
                                </tr>
                                  <%--espacio para la descripcion=====================CRISTIAN SANCHEZ================================--%>
                                <tr>
                                    <td class="style313" colspan="2">
                                    </td>
                                    <td class="style317" colspan="2">
                                    </td>
                                    <td class="style308" colspan="5">
                                        <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Size="18pt" ForeColor="Black"
                                            Style="text-align: center" Width="343px"></asp:Label>                                                                            
                                    </td>
                                </tr>                               
                            </table>                                               
                        </asp:Panel>
                        <asp:Panel ID="Panel3" runat="server" BackColor="White" Font-Bold="True" Font-Size="8pt"
                            GroupingText=" "  Width="1320px">
                            <table style="vertical-align:top">
                                <tr>
                                    <td style="text-align: center">
                                       <marquee behavior="alternate" align="middle" bgcolor="red" scrollamount="5" 
                                           scrolldelay="10"  style="border:solid; font-size:20px; color:whitesmoke "
                                             direction="left" width="1275" height="30">PALLET SOLICITADO</marquee>                                         
                                    </td>
                                </tr>
                            </table>
                                </asp:Panel>
                           <asp:Panel ID="Panel4" runat="server" BackColor="White" Font-Bold="True" Font-Size="8pt"
                            GroupingText=" "  Width="1320px" Style="margin-top: 2px">
                               <table style="vertical-align:top">
                                   <tr style="align-content: center">
                                            <td style="width: 200PX"></td>
                                       <td class="auto-style1" style="vertical-align:top">
                                             <asp:GridView ID="GridPalletAlum" runat="server" BackColor="White" BorderColor="#999999"
                                               BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="300px"
                                               AutoGenerateColumns="False" Font-Size="8pt" Font-Names="arial" Height="16px" PageSize="8"
                                               ShowHeaderWhenEmpty="True">
                                               <Columns>
                                                   <asp:TemplateField HeaderText="Aluminio">
                                                       <EditItemTemplate>
                                                           <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Pallet") %>'></asp:TextBox>
                                                       </EditItemTemplate>
                                                       <ItemTemplate>
                                                           <asp:Label ID="Label4" runat="server" Text='<%# Bind("Pallet") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>                                              
                                               </Columns>
                                               <EditRowStyle HorizontalAlign="Right" />
                                               <EmptyDataRowStyle BorderStyle="Solid" />
                                               <FooterStyle BackColor="#CCCCCC" ForeColor="Black"/>
                                               <HeaderStyle BackColor="#1C5AB6" Font-Bold="True" Font-Size="20PX" ForeColor="White" HorizontalAlign="Center" />
                                               <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                               <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="18PX"  HorizontalAlign="Left" />
                                               <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                               <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                               <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                               <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                               <SortedDescendingHeaderStyle BackColor="#000065" />
                                           </asp:GridView>
                                       </td>
                                       <td style="width: 200PX"></td>

                                       <td class="auto-style1" style="vertical-align:top">
                                           <asp:GridView ID="GridPalletAcc" runat="server" BackColor="White" BorderColor="#999999"
                                               BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="300px"
                                               AutoGenerateColumns="False" Font-Size="8pt" Font-Names="arial" Height="16px" PageSize="8"
                                               ShowHeaderWhenEmpty="True">
                                               <Columns>
                                                   <asp:TemplateField HeaderText="Acero">
                                                       <EditItemTemplate>
                                                           <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Pallet") %>'></asp:TextBox>
                                                       </EditItemTemplate>
                                                       <ItemTemplate>
                                                           <asp:Label ID="Label4" runat="server" Text='<%# Bind("Pallet") %>'></asp:Label>
                                                       </ItemTemplate>
                                                   </asp:TemplateField>                                                
                                               </Columns>
                                               <EditRowStyle HorizontalAlign="Right" />
                                               <EmptyDataRowStyle BorderStyle="Solid" />
                                               <FooterStyle BackColor="#CCCCCC" ForeColor="Black"/>
                                               <HeaderStyle BackColor="#1C5AB6" Font-Bold="True" Font-Size="20PX" ForeColor="White" HorizontalAlign="Center" />
                                               <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                               <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="18PX"  HorizontalAlign="Left" />
                                               <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                               <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                               <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                               <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                               <SortedDescendingHeaderStyle BackColor="#000065" />
                                           </asp:GridView>
                                       </td>
                                   </tr>
                               </table>                        
                                </asp:Panel>    
                            <asp:Timer runat="server" Interval="180000" OnTick="Unnamed2_Tick"></asp:Timer>                                                             
        </ContentTemplate>
    </asp:UpdatePanel>   
</asp:Content>