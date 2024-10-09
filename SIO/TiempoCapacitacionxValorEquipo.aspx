<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="TiempoCapacitacionxValorEquipo.aspx.cs" Inherits="SIO.TiempoCapacitacionxValorEquipo" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" 
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
           
     <script type="text/javascript">

         function valideKeyenteros(e, field) {
             key = e.keyCode ? e.keyCode : e.which
             // backspace ó tab
             if (key == 8 || key == 9 || key == 13 || key == 127) return true
             // 0-9 
             if (key > 47 && key < 58) {
                 if (field.value == "") return true
                 regexp = /[0-9]{20}?/
                 return !(regexp.test(field.value))
             }

             return false
         }


         function confirmarAnularConfigZona() {

                if (confirm("¿Realmente quieres eliminar este registro?")) {

                    return true;
                }
                else {
                    return false;
                }
            }      
    </script>

     <script type="text/javascript">

         function confirmarAnularZona() {

                if (confirm("¿Realmente quieres eliminar este registro?")) {

                    return true;
                }
                else {
                    return false;
                }
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
            border-left: 1px solid #1C5AB6;
            border-right: 1px solid #1C5AB6;
            border-top: 1px solid #1C5AB6;
            cursor: pointer;
            z-index: 2;
            font-weight: bold;
            text-decoration: none;
            color: #ffffff;
            text-shadow: 0 1px 1px rgba(0, 0, 0, 0.35);
            background: #1C5AB6;
            background: -webkit-linear-gradient(#1c5ab6, #1fa0e4);
            background: -moz-linear-gradient(#1c5ab6, #1fa0e4);
            background: -o-linear-gradient(#1c5ab6, #1fa0e4);
            background: -ms-linear-gradient(#1c5ab6, #1fa0e4);
            text-align: center;
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
            border-left: 1px solid #1C5AB6;
            border-right: 1px solid #1C5AB6;
            border-top: 1px solid #1C5AB6;
            cursor: pointer;
            z-index: 2;
            font-weight: bold;
            text-decoration: none;
            color: #ffffff;
            text-shadow: 0 1px 1px rgba(0, 0, 0, 0.35);
            background: #1C5AB6;
            background: -webkit-linear-gradient(#1c5ab6, #1fa0e4);
            background: -moz-linear-gradient(#1c5ab6, #1fa0e4);
            background: -o-linear-gradient(#1c5ab6, #1fa0e4);
            background: -ms-linear-gradient(#1c5ab6, #1fa0e4);
            text-align: center;
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
        .CustomComboBoxStyle .ajax__combobox_buttoncontainer button
        {
            background-image: url('http://app.forsa.com.co/SIOMaestros/Imagenes/toolkit-arrow.gif');
            border-style: none;
        }
        .CustomComboBoxStyle .ajax__combobox_textboxcontainer input
        {
            background-image: url('http://app.forsa.com.co/SIOMaestros/Imagenes/toolkit-bg.gif');
            border-style: none;
        }
        .CustomComboBoxStyle .ajax__combobox_itemlist li
        {
            color: Black;
            font-size: 8pt;
            font-family: Arial;
            background-color: #EBEBEB;
        }
        .izquierda
        {
            width: 108px;
            text-align: izquierda;
        }
        .derecha
        {
            width: 108px;
            text-align: derecha;
        }
        .centrado
        {
            width: 108px;
            text-align: centrado;
        }
        .Detalle
        {
            width: 749px;
            height: 145px;
        }
        .style26
        {
            text-align: right;
            margin-left: 40px;
        }
        .style81
        {
            height: 18px;
            text-align: right;
        }
        .style94
        {
            width: 130px;
            text-align: justify;
        }
        .style98
        {
            text-align: right;
            margin-left: 40px;
            height: 18px;
        }
        .style104
        {
            width: 243px;
            text-align: center;
        }
        .style110
        {
            width: 123px;
        }
        .style111
        {
            width: 120px;
        }
        .style114
        {
            width: 55%;
        }
        .style119
        {
            text-align: center;
            width: 185px;
        }
        .style121
        {
            width: 142px;
            text-align: right;
        }
        .style123
        {
            width: 18px;
            text-align: right;
        }
        .style124
        {
            width: 20px;
        }
        .style126
        {
            width: 81px;
        }
        
        .style127
        {
            width: 130px;
        }
        
        .style128
        {
            width: 18px;
        }
        .style130
        {
            text-align: center;
        }
        
        .style137
        {
            width: 116px;
        }
        
        .style138
        {
            width: 300px;
        }
        
        .style152
        {
            width: 900px;
        }
        .style154
        {
            width: 66px;
        }
        .style155
        {
            width: 24px;
        }
        .style156
        {
            width: 72px;
        }
        
        .CustomComboBoxStyle
        {
            text-align: right;
        }
        .CustomComboBoxStyle
        {
            text-align: left;
        }
        
        .style157
        {
            width: 100%;
        }
        
        .modalPopup
        {
            background-color: #FFFFC0;
            border-width: 1px;
            border-style: Solid;
            border-color: Gray;
            padding: 1px;
            width: 250px;
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
        .Grid td
        {
            background-color: #A1DCF2;
            color: black;
            font-size: 7pt;
            border-style:none;
        }
        .Grid th
        {
            background-color: #3AC0F2;
            color: White;
            font-size: 7pt;
        }
        .ChildGrid td
        {
            background-color: #eee !important;
            color: black;
            font-size: 7pt;
            line-height: 200%;
        }
        .ChildGrid th
        {
            background-color: #6C6C6C !important;
            color: White;
            font-size: 7pt;
            line-height: 200%;
        }
        .Nested_ChildGrid td
        {
            background-color: #fff !important;
            color: black;
            font-size: 7pt;
            line-height: 200%;
        }
        .Nested_ChildGrid th
        {
            background-color: #2B579A !important;
            color: White;
            font-size: 7pt;
            line-height: 200%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <table id="TblMetaProd" class="fondoazul" width="100%">
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="12pt"
                    ForeColor="White" Text="TIEMPO CAPACITACIÓN VS VALOR DE EQUIPO POR ZONAS"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <asp:UpdatePanel UpdateMode="Conditional" ID="updpnlmaestro" runat="server">
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
                            <asp:Label ID="Label1" runat="server"
                                Text="Zonas"></asp:Label>
                        </Header>
                        <Content>
                           <asp:Panel ID="pnlAgregarZona" runat="server"
                Font-Names="Arial" ForeColor="Black" Font-Size="8pt" GroupingText="Crear Nueva Zona"
                Width="650px" CssClass="auto-style2" Visible="false">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label2"
                                runat="server" Text="Descripcion:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDescriZona"
                                runat="server" Style="text-transform: uppercase" Width="600px"></asp:TextBox>
                        </td>
                        <td class="auto-style4">&nbsp;                                                                                                               
                                                            <asp:Button ID="btnAgregar"
                                                                runat="server" Text="Agregar"
                                                                BackColor="#1C5AB6"
                                                                ForeColor="White"
                                                                BorderColor="#999999"
                                                                Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btnAgregar_Click1" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />            
            <asp:GridView ID="grid_ZonaSiat" runat="server" BackColor="White"
                BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                CellPadding="4"
                Width="750px" AutoGenerateColumns="False" Font-Size="8pt"
                Font-Names="arial"
                ShowHeaderWhenEmpty="True">
                <AlternatingRowStyle BackColor="Gainsboro" />
                <Columns>
                    <asp:TemplateField HeaderText="idZonaSiat" Visible="false">
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_ZonaSiat" runat="server" ReadOnly="True"
                                Text='<%# Bind("zonsia_id") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_ZonaSiat" runat="server" Text='<%# Bind("zonsia_id") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="200px"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Zonas">
                        <EditItemTemplate>
                            <asp:TextBox ID="txt_Descrip_Zona" Width="700px" runat="server"
                                Style="text-transform: uppercase" Text='<%# Bind("zonsia_descripcion") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle Width="800px"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("zonsia_descripcion") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Activo" Visible="false">
                        <EditItemTemplate>
                            <asp:DropDownList ID="drop_Activo" runat="server"
                                Visible="false" AutoPostBack="false">
                                <asp:ListItem Text="SI">1</asp:ListItem>
                                <asp:ListItem Text="NO">0</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <HeaderStyle Width="40px"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="Label33" runat="server" Visible="false"
                                Text='<%# Bind("zonsia_activo") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>              
                </Columns>
                <EditRowStyle HorizontalAlign="Left" />
                <EmptyDataRowStyle BorderStyle="Solid" />
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#1C5AB6" Font-Bold="True" ForeColor="White"
                    HorizontalAlign="Left" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" HorizontalAlign="Left" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView>
                        </Content>
                    </asp:AccordionPane>                    
                       <asp:AccordionPane ID="AccordionPane3" runat="server" 
                                    ContentCssClass="accordionContent" Font-Bold="False" Font-Names="Arial" 
                                    Font-Size="8pt" HeaderCssClass="accordionHeader" 
                                    HeaderSelectedCssClass="accordionHeaderSelected">
                                    <Header>
                                        <asp:Label ID="Label6" runat="server" Text="Configuracion Detalle Zona "></asp:Label>
                                    </Header>
                                    <Content>
                                        <table>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lbldesczona"
                                            runat="server" Text="Zona:"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cboZonasiat"
                                            runat="server" AutoPostBack="true" OnTextChanged="cboZonasiat_TextChanged"
                                            Width="526PX">
                                            <asp:ListItem
                                                Value="0">Selecione</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblsol" runat="server"
                                            Width="69px" Text="Material:"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="cbotiposol"
                                            AutoPostBack="true" runat="server" Width="100PX"
                                            OnSelectedIndexChanged="cbotiposol_SelectedIndexChanged">
                                            <asp:ListItem
                                                Value="0">Selecione</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lbldesctiposol"
                                            runat="server"></asp:Label>
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                           &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                           &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                           &nbsp; &nbsp; &nbsp;
                                           <asp:TextBox ID="txtDiasAdicio" 
                                            runat="server" Width="15px"></asp:TextBox> &nbsp;
                                            <asp:Label ID="lbl_descripDias"
                                                runat="server" Text="Dias adicionales depues de "></asp:Label>
                                         <asp:Label ID="lbl_ValDiasAdic"                                      
                                                runat="server" Text="."></asp:Label>                                         
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblMoneda"
                                            runat="server" Text="Moneda:"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="cboMoneda"
                                            runat="server" Width="100px">
                                            <asp:ListItem
                                                Value="0">Selecione</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <asp:Panel ID="pnlconfigvalor" runat="server"
                                Font-Names="Arial" Font-Size="8pt"
                                GroupingText="."
                                Width="500px" CssClass="auto-style2">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblrangomin"
                                                runat="server" Text="Valor Minimo:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRangoMin"
                                                runat="server" Width="80px"></asp:TextBox>
                                        </td>

                                        <td>&nbsp; 
                                                        <asp:Label ID="lblrangomax"
                                                            runat="server" Text="Valor Maximo:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRangoMax"
                                                runat="server" Width="80px"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;
                                                        <asp:Label ID="lblDias"
                                                         runat="server" Text="Dias:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDias"
                                                runat="server" Width="30px"></asp:TextBox>
                                        </td>
                                        <td class="auto-style4">&nbsp;                                                                                                               
                                                            <asp:Button ID="btnGuardar"
                                                                runat="server" Text="Agregar"
                                                                BackColor="#1C5AB6"
                                                                ForeColor="White"
                                                                BorderColor="#999999"
                                                                Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btnGuardar_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>                                        
                                        <asp:GridView ID="GridConfigvalor" runat="server" BackColor="White"
                                            BorderColor="#999999"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="530px"
                                            AutoGenerateColumns="False" Font-Size="8pt" Font-Names="arial"
                                            Height="30px" DataKeyNames="siat_rangos_id"
                                            OnRowCommand="GridConfigvalor_RowCommand"
                                            OnRowUpdating="GridConfigvalor_RowUpdating"
                                            OnRowEditing="GridConfigvalor_RowEditing"
                                            OnRowCancelingEdit="GridConfigvalor_RowCancelingEdit" OnRowDeleting="GridConfigvalor_RowDeleting"
                                            
                                            ShowHeaderWhenEmpty="True">
                                            <AlternatingRowStyle BackColor="Gainsboro" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="siat_rangos_id" Visible="False">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txt_rangosid" AutoPostBack="false"
                                                            runat="server" Text='<%# Bind("siat_rangos_id") %>' BorderStyle="None"
                                                            ReadOnly="True"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("siat_rangos_id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tipo Moneda" HeaderStyle-Width="80px">
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="cboMoneda" Width="80px" Text='<%# Bind("monedaid") %>'
                                                            DataSource="<%#  Listar_TipoMoneda() %>" DataTextField="mon_descripcion"
                                                            DataValueField="mon_id" AutoPostBack="false" Enabled="false" runat="server">
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label Width="80px" ID="Label4" runat="server" Text='<%# Bind("mon_descripcion") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="80px"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Valor Minimo" HeaderStyle-Width="80px">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="Txt_ValorMin" Width="80px" runat="server"
                                                            AutoPostBack="false" Text='<%# Bind("rango_min","{0:0}") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Width="80px"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Width="80" Text='<%# Bind("rango_min","{0:0,0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" Valor Maximo" HeaderStyle-Width="80px"
                                                    ItemStyle-HorizontalAlign="Right">
                                                    <EditItemTemplate>
                                                        <asp:TextBox Width="80px" ID="txt_ValorMax"
                                                            AutoPostBack="false" runat="server" Text='<%# Bind("rango_max","{0:0}") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label Width="80px" ID="Label4" runat="server" Text='<%#Bind("rango_max","{0:0,0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="80px"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dias" HeaderStyle-Width="60px">
                                                    <EditItemTemplate>
                                                        <asp:TextBox Width="60px" MaxLength="7" ID="txt_Dias"
                                                            AutoPostBack="false" runat="server" Text='<%# Bind("dias") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label Width="60px" ID="lbldias" runat="server" Text='<%#Bind("dias") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="60px"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Tipo Orden" HeaderStyle-Width="80px">
                                                    <EditItemTemplate>
                                                        <asp:DropDownList ID="cboTipoOrden" DataSource="<%#Listar_TipoOrden() %>"
                                                            DataTextField="T_Sol_Materia" DataValueField="T_Sol_Id"
                                                            AutoPostBack="false" runat="server" Text='<%# Bind("TisolId") %>'>
                                                        </asp:DropDownList>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label Width="80px" ID="Label5" runat="server" Text='<%#Bind("T_Sol_Materia") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="80px"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dias Adicionales" HeaderStyle-Width="80px">
                                                    <EditItemTemplate>
                                                        <asp:TextBox Width="80px" ID="txt_DiasAdic"
                                                            AutoPostBack="false" runat="server" Text='<%# Bind("dias_adicionales") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label Width="80px" ID="lbldias" runat="server" Text='<%#Bind("dias_adicionales") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="80px"></HeaderStyle>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:CommandField HeaderText="Editar" ShowEditButton="True" />  
                                                 <asp:CommandField HeaderText="Anular" ButtonType="Link" ShowDeleteButton="true" />                                                                                              
                                            </Columns>
                                            <EditRowStyle HorizontalAlign="Right" />
                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                            <HeaderStyle BackColor="#1C5AB6" Font-Bold="True" ForeColor="White"
                                                HorizontalAlign="Center" />
                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" HorizontalAlign="Left" />
                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#000065" />
                                        </asp:GridView>                                                                                                                                                            
                                    </Content>
                                </asp:AccordionPane>
                    <asp:AccordionPane ID="AccordionPane7" runat="server" 
                                    ContentCssClass="accordionContent" Font-Bold="False" Font-Names="Arial" 
                                    Font-Size="8pt" HeaderCssClass="accordionHeader" 
                                    HeaderSelectedCssClass="accordionHeaderSelected">
                                    <Header>
                                        <asp:Label ID="lblCarta" runat="server" Text="Listado valor de equipos por zona"></asp:Label>
                                    </Header>
                                    <Content>
                                        <rsweb:ReportViewer ID="ReportViewer2" runat="server" ShowParameterPrompts="False" BackColor="White" Width="1110px" Height="1000" AsyncRendering="False">
                                        </rsweb:ReportViewer>
                                    </Content>
                                </asp:AccordionPane>
                </Panes>
            </asp:Accordion>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
