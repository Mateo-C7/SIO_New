  <%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="SeguimientoDespachos.aspx.cs" Inherits="SIO.SeguimientoDespachos" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
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
    
      
        function dateSelectionChanged(x) {
            debugger;
            javascript: __doPostBack('txt_TiempoEnvioDtos', '')
        }
         function validedecimal(e, field) {
             key = e.keyCode ? e.keyCode : e.which
             // backspace ó tab
             if (key == 8 || key == 9 || key == 13 || key == 127) return true

             // 0-9 a partir del .decimal  
             if (field.value != "") {
                 if ((field.value.indexOf(".")) > 0) {
                     //si tiene un punto valida dos digitos en la parte decimal
                     if (key > 47 && key < 58) {
                         if (field.value == "") return true
                         regexp = /[0-9]{4}$/
                         return !(regexp.test(field.value))
                     }
                 }
             }
             // 0-9  
             ///[0-9]{8}?/ se pueden ingresar los dijitos del 0 al 9 y 8 posiciones enteras antes del punto decimal
             if (key > 47 && key < 58) {
                 if (field.value == "") return true
                 regexp = /[0-9]{18}?/
                 return !(regexp.test(field.value))
             }
             // .
             if (key == 46) {
                 if (field.value == "") return false
                 regexp = /^[0-9]+$/
                 return regexp.test(field.value)
             }
             // other key
             return false
         }


  

            function confirmarEliminarObservacion() {

                if (confirm("¿Realmente quiere eliminar este registro?")) {

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
        .FixedHeader {
            position:absolute;          
            font-weight: bold;
        }     
   
        .auto-style1 {
            margin-right: 142px;
        }
   
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">

    <br />

    <table id="TblSeg" class="fondoazul" width="900px">
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="12pt"
                    ForeColor="White" Text="SEGUIMIENTO DESPACHOS"></asp:Label>
                <asp:Label ID="lbl_Desc_Id" runat="server" Visible="false"></asp:Label>
                       <asp:Label ID="lbl_tdn" runat="server" Visible="false"></asp:Label>
                 <asp:Label ID="lbl_idTdn" runat="server" Visible="false"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel UpdateMode="Conditional" ID="updpnlmaestro" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        <table style="width: 895px">
                            <table>
                            <div ID="Tbl_Filtros" runat="server">                          
                                  <tr>
                                    <td colspan="1" class="auto-style1" align="right">
                                        <asp:Label ID="Label69" runat="server" Text="Año_Creacion:" Font-Bold="False" Font-Names="Arial"
                                            Font-Size="8pt" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cbo_FiltroAñoCrea" runat="server" Width="80px"></asp:DropDownList>
                                    </td>
                                    <td colspan="1" class="auto-style1" style="text-align: right">
                                        <asp:Label ID="Label70" runat="server" Text="Mes_Creacion:" Font-Bold="False" Font-Names="Arial"
                                            Font-Size="8pt" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cbo_FiltroMesCrea" runat="server" Width="112px"></asp:DropDownList>
                                    </td>                                  
                                </tr>
                                <tr>
                                    <td colspan="1" class="auto-style1" align="right">
                                        <asp:Label ID="Label" runat="server" Text="Año_Despacho:" Font-Bold="False" Font-Names="Arial"
                                            Font-Size="8pt" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cbo_FiltAño" runat="server" Width="80px"></asp:DropDownList>
                                    </td>
                                    <td colspan="1" class="auto-style1" style="text-align: right">
                                        <asp:Label ID="Label4" runat="server" Text="Mes_Despacho:" Font-Bold="False" Font-Names="Arial"
                                            Font-Size="8pt" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="cbo_FiltMes" runat="server" Width="112px"></asp:DropDownList>
                                    </td>
                                    <td colspan="1" class="auto-style1" align="right">
                                        <asp:Label ID="Label5" runat="server" Text="Orden:" Font-Bold="False" Font-Names="Arial"
                                            Font-Size="8pt" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_FiltOrden" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Style="text-align: right" Width="99px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="1" class="auto-style1" align="right">
                                        <asp:Label ID="Label3" runat="server" Text="Factura:" Font-Bold="False" Font-Names="Arial"
                                            Font-Size="8pt" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txt_FiltFactura" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Style="text-align: right" Width="70px"></asp:TextBox>
                                    </td>
                                    <td colspan="1" class="auto-style4" align="right">
                                        <asp:Label ID="Label8" runat="server" Text="Estatus Carga:" Width="85px" Font-Bold="False" Font-Names="Arial"
                                            Font-Size="8pt" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td class="auto-style2">
                                        <asp:DropDownList ID="cbo_FiltEstatusCarga" runat="server" Width="112px"></asp:DropDownList>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td colspan="1" class="auto-style4" align="right">
                                        <asp:Label ID="Label15" runat="server" Text="Tipo Despacho:" Width="85px" Font-Bold="False" Font-Names="Arial"
                                            Font-Size="8pt" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td class="auto-style2">
                                        <asp:DropDownList ID="cbo_TipoDespacho" runat="server" Width="100px"></asp:DropDownList>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>                                  
                                </tr>
                                   <tr>
                                    <td colspan="1" class="auto-style1" align="right">
                                        <asp:Label ID="Label68" runat="server" Text="Planta:" Font-Bold="False" Font-Names="Arial"
                                            Font-Size="8pt" ForeColor="Black"></asp:Label>
                                    </td>
                                    <td>
                                       <asp:DropDownList ID="cbo_Planta" runat="server" Width="150px"></asp:DropDownList>
                                    </td>
                                    <td >
                                         &nbsp;&nbsp;
                                    </td>
                                    <td class="auto-style2">
                                      <asp:CheckBox ID="chk_SinDespacho" runat="server" Text="Sin Despachar"
                                           AutoPostBack="true" ForeColor="Black" OnCheckedChanged="chk_SinDespacho_CheckedChanged" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td colspan="1" class="auto-style4" align="right">
                                       &nbsp;&nbsp;
                                    </td>
                                    <td class="auto-style2">
                                  <asp:Button ID="btn_Filtrar" runat="server" BackColor="#1C5AB6"
                                            BorderColor="#999999" CssClass="botonsio" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="8pt" ForeColor="White" Text="Filtrar"
                                            Width="70px" OnClick="btn_Filtrar_Click" />
                                    </td>  
                                        <td colspan="1" class="auto-style4" align="right">
                                       &nbsp;&nbsp;
                                    </td>
                                    <td class="auto-style2">
                                 <asp:Button ID="btn_CancelaBusqueda" runat="server" Text="Limpiar Filtros" BackColor="#1C5AB6"
                                            BorderColor="#999999" CssClass="botonsio" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="8pt" ForeColor="White"
                                            Width="100px" OnClick="btn_CancelaBusqueda_Click" />
                                    </td>                                 
                                </tr>
                                </div>
                            </table>
                            <br />
                            <div id="Tbl_datosp" runat="server" style="width:  880px; height: 200px; overflow: auto">
                                <asp:GridView ID="GridDatosOrden" runat="server" BackColor="White"
                                    BorderColor="#999999" BorderStyle="None" BorderWidth="1px"  AllowPaging="true"
                                    CellPadding="3" Width="895px" AutoGenerateColumns="False" PageIndex="20"
                                    Font-Size="8pt" Font-Names="arial" Height="30px" DataKeyNames="DesC_Id"
                                    OnRowCommand="GridDatosOrden_RowCommand" OnPageIndexChanging="GridDatosOrden_PageIndexChanging"
                                    ShowHeaderWhenEmpty="True" AllowSorting="True">
                                    <AlternatingRowStyle BackColor="Gainsboro" />
                                    <Columns>
                                        <asp:CommandField HeaderText="Seleccionar" HeaderStyle-Width="50px" ShowSelectButton="True">
                                            <HeaderStyle Width="50px"></HeaderStyle>
                                        </asp:CommandField>
                                        <asp:TemplateField HeaderText="Año_Despacho" HeaderStyle-Width="40px">
                                            <ItemTemplate>
                                                <asp:Label ID="Label8" Width="30px" runat="server" Text='<%# Bind("Año_Despacho") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mes_Despacho" HeaderStyle-Width="70px">
                                            <ItemTemplate>
                                                <asp:Label ID="Label8" Width="70px" runat="server" Text='<%# Bind("Mes_Despacho") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Despacho" HeaderStyle-Width="80px" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="Label8" Width="50px" runat="server" Text='<%# Bind("DesC_Id") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="NumDespacho">
                                            <ItemTemplate>
                                                <asp:Label Width="50px" ID="Label4" runat="server" Text='<%# Bind("NumDespacho") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="80px"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FechaCreaDespacho" HeaderStyle-Width="80px">
                                            <HeaderStyle Width="50px"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Width="50" Text='<%# Bind("FechaCreaDespacho","{0:d}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="FechaDespacho" HeaderStyle-Width="80px"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label Width="50px" ID="Label4" runat="server" Text='<%#Bind("FechaDespacho","{0:d}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cliente">
                                            <ItemTemplate>
                                                <asp:Label Width="230px" ID="lbl_Cliente" runat="server" Text='<%#Bind("Cliente") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Orden">
                                            <ItemTemplate>
                                                <asp:Label Width="80px" ID="lbl_Obra" runat="server" Text='<%#Bind("Orden") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Orden/Obra">
                                            <ItemTemplate>
                                                <asp:Label Width="400px" ID="lbl_Ordenes" runat="server" Text='<%#Bind("Ordenes") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Facturas">
                                            <ItemTemplate>
                                                <asp:Label Width="120px" ID="lbl_Ordenes" runat="server" Text='<%#Bind("Facturas") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ZonaCliente">
                                            <ItemTemplate>
                                                <asp:Label Width="120px" ID="lbl_ZonaCliente" runat="server" Text='<%#Bind("ZonaCliente") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PaisCliente">
                                            <ItemTemplate>
                                                <asp:Label Width="120px" ID="lbl_PaisCliente" runat="server" Text='<%#Bind("PaisCliente") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CiudadCliente">
                                            <ItemTemplate>
                                                <asp:Label Width="120px" ID="lbl_CiudadCliente" runat="server" Text='<%#Bind("CiudadCliente") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tipo_Despacho" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label Width="40px" ID="tipoDespacho" runat="server" Text='<%#Bind("DesC_Nal") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Estatus_Carga" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label Width="20px" ID="estatusCarga" runat="server" Text='<%#Bind("DesDt_Estatus_Carga") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Planta_Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label Width="20px" ID="planta" runat="server" Text='<%#Bind("planta_id") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField> 
                                                   <asp:TemplateField HeaderText="Año_Crea" HeaderStyle-Width="40px" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="Label8" Width="30px" runat="server" Text='<%# Bind("año_Crea") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Mes_Crea" HeaderStyle-Width="70px" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="Label8" Width="70px" runat="server" Text='<%# Bind("mes_Crea") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>                                    
                                    </Columns>
                                    <EditRowStyle HorizontalAlign="Right" />
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White"
                                        HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#000065" />
                                </asp:GridView>
                            </div>
                            <asp:Accordion ID="Accordion1" runat="server" ContentCssClass="accordionContent"
                                HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                Width="900px" Height="8000px" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="text-align: right">
                                <Panes>
                                    <asp:AccordionPane ID="AcorInfoGeneral" runat="server" ContentCssClass="accordionContent"
                                        Font-Bold="False" Font-Names="Arial" Font-Size="8pt" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected">
                                        <Header>
                                            <asp:Label ID="Label1" runat="server"
                                                Text="Datos Generales"></asp:Label>
                                        </Header>
                                        <Content>
                                            <table width="830px">
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td style="text-align: left;">
                                                                    <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                        Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Mes Despacho:"
                                                                        Width="120px"></asp:Label>
                                                                </td>
                                                                <td style="text-align: left" class="style107">
                                                                    <asp:TextBox ID="txt_MesDespacho" runat="server" Font-Names="Arial"
                                                                        Font-Size="8pt" Width="70px" Style="text-align: left"></asp:TextBox>                                                                                                                                
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="DespaCliente" runat="server" Text="Despacho:"></asp:Label>                                                                   
                                                                </td>
                                                                  <td style="text-align: left" class="style107">
                                                                    <asp:Label ID="lbl_IdDespacho" runat="server"></asp:Label>                                                                   
                                                                </td>                                                                                                                                  
                                                            </tr>
                                                            <td style="text-align: right;">
                                                                <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                    Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Zona:"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left" class="style107">
                                                                <asp:TextBox ID="txt_Zona" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                    Style="text-align: left" Width="150px"></asp:TextBox>
                                                            </td>
                                                            <td style="text-align: right;">
                                                                <asp:Label ID="Label63" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                    Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Pais Destino:"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left" class="style107">
                                                                <asp:TextBox ID="txt_PaisDestino" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                    Style="text-align: left" Width="150px"></asp:TextBox>
                                                            </td>
                                                            <td style="text-align: right;">
                                                                <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                    Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="M2:"></asp:Label>
                                                            </td>
                                                            <td style="text-align: left" class="style107">
                                                                <asp:TextBox ID="txt_M2" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                    Style="text-align: left" Width="50px"></asp:TextBox>
                                                            </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="Label66" runat="server" Font-Bold="False" Font-Names="Arial"
                                                            Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Ciudad:"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txt_Ciudad" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Style="text-align: left" Width="150px"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial"
                                                            Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                            Text="Medio Trasnporte:" Width="90px"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left" class="style107">
                                                        <asp:DropDownList ID="cbo_MedioTrasnporte" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Width="90px" AutoPostBack="true" OnTextChanged="cbo_MedioTrasnporte_TextChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="lbl_Tipo_Vehiculo" runat="server" Font-Bold="False" Font-Names="Arial"
                                                            Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Tipo Vehiculo:"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:DropDownList ID="cbo_TipoVehiculo" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Width="90px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial"
                                                            Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                            Text="Valor EXW US$:" Width="80px"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txt_ValorExw" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Style="text-align: right" Width="80px" AutoPostBack="true"
                                                            OnTextChanged="txt_ValorExw_TextChanged"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="Label64" runat="server" Font-Bold="False" Font-Names="Arial"
                                                            Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                            Text="Valor FOB US$:" Width="120px"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txt_ValorFob" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Style="text-align: right" Width="80px" AutoPostBack="true" OnTextChanged="txt_ValorFob_TextChanged"></asp:TextBox>
                                                    </td>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="Label65" runat="server" Font-Bold="False" Font-Names="Arial"
                                                            Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                            Text="Valor Total Factura US$:" Width="120px"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:TextBox ID="txt_ValorTolFactura" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Style="text-align: right" Width="80px" AutoPostBack="true" OnTextChanged="txt_ValorTolFactura_TextChanged"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="Label67" runat="server" Font-Bold="False" Font-Names="Arial"
                                                            Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Facturas:"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:ListBox ID="lst_Facturas" runat="server" Width="150px" Height="50px"></asp:ListBox>
                                                    </td>
                                                    <td style="text-align: right;">
                                                                     <asp:Label ID="Label152" runat="server" Font-Bold="False" Font-Names="Arial"
                                                            Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Puerto_Origen:"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left;">
                                                            <asp:TextBox ID="txt_Puerto_Origen" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Style="text-align: right" Width="80px" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                    </td>
                                                     <td style="text-align: right;">
                                                                     <asp:Label ID="label1231" runat="server" Font-Bold="False" Font-Names="Arial"
                                                            Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Puerto_Destino:"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left;">
                                                            <asp:TextBox ID="txt_Puerto_Destino" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Style="text-align: right" Width="80px" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                    </td>
                                                </tr>

                                                         <tr>
                                                    <td style="text-align: right;">
                                                      &nbsp;
                                                    </td>
                                                    <td style="text-align: left;">
                                                         &nbsp;
                                                    </td>
                                                    <td style="text-align: right;">
                                                      <asp:Label ID="lbl_tdn_1" runat="server" Font-Bold="False" Font-Names="Arial"
                                                            Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Tdn:"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left;">
                                                       <asp:DropDownList ID="cbo_Tdn" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Width="60px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="text-align: right;">
                                                     <asp:Label ID="lbl_MsjValidaTdn" Width="100%" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left;">
                                                        <asp:Button ID="btn_DtsGeneralAct" runat="server" BackColor="#1C5AB6"
                                                            BorderColor="#999999" CssClass="botonsio" Font-Bold="True" Font-Names="Arial"
                                                            Font-Size="8pt" ForeColor="White" OnClick="btn_DtsGeneralAct_Click"
                                                            OnClientClick="return confirm('¿Desea actualizar los registros?')" Text="Actualizar"
                                                            Width="70px" />
                                                    </td>
                                                </tr>


                                            </table>
                                            </td>                                                          
                                                            </tr>                                                                                                                                                                                                                                   
                                            </table>                                          
                                                 <div style="width: 890px; overflow: auto">
                                                     <asp:GridView ID="grid_DtsOrden" runat="server" BackColor="White"
                                                         BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                         CellPadding="3" Width="890px" AutoGenerateColumns="False"
                                                         Font-Size="8pt" Font-Names="arial" OnRowCommand="grid_DtsOrden_RowCommand"
                                                         AllowSorting="True" ShowHeaderWhenEmpty="true" ShowHeader="true">
                                                         <AlternatingRowStyle BackColor="Gainsboro" />
                                                         <Columns>
                                                             <asp:CommandField HeaderText="Seleccionar" HeaderStyle-Width="50px" ShowSelectButton="True">
                                                                 <HeaderStyle Width="50px"></HeaderStyle>
                                                             </asp:CommandField>
                                                             <asp:TemplateField HeaderText="Cliente" HeaderStyle-Width="120px">
                                                                 <ItemTemplate>
                                                                     <asp:Label ID="Label8" Width="250px" runat="server" Text='<%# Bind("Cliente") %>'></asp:Label>
                                                                 </ItemTemplate>
                                                                 <ItemStyle HorizontalAlign="Left" />
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Orden" HeaderStyle-Width="40px">
                                                                 <ItemTemplate>
                                                                     <asp:Label ID="Label8" Width="60px" runat="server" Text='<%# Bind("Orden") %>'></asp:Label>
                                                                 </ItemTemplate>
                                                                 <ItemStyle HorizontalAlign="Left" />
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Obra" HeaderStyle-Width="120px">
                                                                 <ItemTemplate>
                                                                     <asp:Label ID="Label8" Width="250px" runat="server" Text='<%# Bind("Obra") %>'></asp:Label>
                                                                 </ItemTemplate>
                                                                 <ItemStyle HorizontalAlign="Left" />
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="M2_Cotizados" HeaderStyle-Width="40px">
                                                                 <ItemTemplate>
                                                                     <asp:Label Width="50px" ID="Label4" runat="server" Text='<%# Bind("M2") %>'></asp:Label>
                                                                 </ItemTemplate>
                                                                 <ItemStyle HorizontalAlign="Right" />
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Valor EXW" HeaderStyle-Width="90px">
                                                                 <HeaderStyle Width="70px"></HeaderStyle>
                                                                 <ItemStyle HorizontalAlign="Right" />                                                                
                                                                 <ItemTemplate>
                                                                     <asp:Label ID="Label3" runat="server" Width="90" Text='<%# Bind("Valor_Comercial","{0:c}")%>'  ></asp:Label>                                                                     
                                                                 </ItemTemplate >                                                                 
                                                             </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Tdn" HeaderStyle-Width="20px"
                                                                 ItemStyle-HorizontalAlign="Right">
                                                                 <ItemTemplate>
                                                                     <asp:Label Width="35px" ID="Label4" runat="server" Text='<%#Bind("Tdn") %>'></asp:Label>
                                                                 </ItemTemplate>
                                                             </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Destino" HeaderStyle-Width="20px"
                                                                 ItemStyle-HorizontalAlign="Right" Visible="false">
                                                                 <ItemTemplate>
                                                                     <asp:Label Width="35px" ID="Label4" runat="server" Text='<%#Bind("Destino") %>'></asp:Label>
                                                                 </ItemTemplate>
                                                             </asp:TemplateField>
                                                                     <asp:TemplateField HeaderText="Flete_Cotizado" HeaderStyle-Width="20px"
                                                                 ItemStyle-HorizontalAlign="Right" Visible="false">
                                                                 <ItemTemplate>
                                                                     <asp:Label Width="35px" ID="Label4" runat="server" Text='<%#Bind("Flete_Cotizado") %>'></asp:Label>
                                                                 </ItemTemplate>
                                                             </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Peso" HeaderStyle-Width="20px"
                                                                 ItemStyle-HorizontalAlign="Right" Visible="false">
                                                                 <ItemTemplate>
                                                                     <asp:Label Width="35px" ID="Label4" runat="server" Text='<%#Bind("Peso") %>'></asp:Label>
                                                                 </ItemTemplate>
                                                             </asp:TemplateField>   
                                                                 <asp:TemplateField HeaderText="ciu_id" HeaderStyle-Width="20px"
                                                                 ItemStyle-HorizontalAlign="Right" Visible="false">
                                                                 <ItemTemplate>
                                                                     <asp:Label Width="35px" ID="Label4" runat="server" Text='<%#Bind("ciu_id") %>'></asp:Label>
                                                                 </ItemTemplate>
                                                             </asp:TemplateField> 
                                                                <asp:TemplateField HeaderText="cli_id" HeaderStyle-Width="20px"
                                                                 ItemStyle-HorizontalAlign="Right" Visible="false">
                                                                 <ItemTemplate>
                                                                     <asp:Label Width="35px" ID="Label4" runat="server" Text='<%#Bind("cli_id") %>'></asp:Label>
                                                                 </ItemTemplate>
                                                             </asp:TemplateField>                                                                                                                                                                                            
                                                         </Columns>
                                                         <EditRowStyle HorizontalAlign="Right" />
                                                         <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                         <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White"
                                                             HorizontalAlign="Center" />
                                                         <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                         <RowStyle BackColor="#EEEEEE" ForeColor="Black" HorizontalAlign="Left" />
                                                         <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                         <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                         <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                                         <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                         <SortedDescendingHeaderStyle BackColor="#000065" />
                                                     </asp:GridView>
                                                 </div>
                                            <br />
                                            <asp:GridView ID="GridTransporte" runat="server" BackColor="White"
                                                BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" Width="400px" AutoGenerateColumns="False"
                                                Font-Size="8pt" Font-Names="arial" Height="16px"
                                                AllowSorting="True" ShowHeaderWhenEmpty="true" ShowHeader="true"
                                                CssClass="auto-style2">
                                                <AlternatingRowStyle BackColor="Gainsboro" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Trasnporte" HeaderStyle-Width="30px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label8" Width="60px" runat="server" Text='<%# Bind("Transporte") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Contenedores" HeaderStyle-Width="30px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label8" Width="80px" runat="server" Text='<%# Bind("Contenedor") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cap Contenedor" HeaderStyle-Width="40px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label8" Width="50px" runat="server" Text='<%# Bind("Cap_Contenedor") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Orden" HeaderStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label8" Width="60px" runat="server" Text='<%# Bind("Orden") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="Empresa_Transportadora" HeaderStyle-Width="100px" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label8" Width="100px" runat="server" Text='<%# Bind("Empresa_Transportadora") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EditRowStyle HorizontalAlign="Right" />
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White"
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
                                    <asp:AccordionPane ID="Acc_DetalleDespa" runat="server" 
                                        ContentCssClass="accordionContent" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected">
                                        <Header>
                                            <asp:Label ID="Label6" runat="server" Text="Detalle Despacho Exterior"></asp:Label>
                                        </Header>
                                        <Content>
                                            <table style="width: 895px;">
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial"
                                                            Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Documento Trasnporte/No_Guia:"
                                                            Width="120px"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left" class="style107">
                                                        <asp:TextBox ID="txt_DocumentTrasnGuia" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Style="text-transform: uppercase" Width="115px"></asp:TextBox>
                                                        &nbsp;   &nbsp;  &nbsp;
                                           <asp:Label ID="Label17" runat="server" Font-Bold="False" Font-Names="Arial"
                                               Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                               Text="No. DEX y F.M.M.:" Width="120px"></asp:Label>
                                                        <asp:TextBox ID="txt_DexFmm" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Style="text-align: right" Width="100px" ></asp:TextBox>
                                                        &nbsp;   &nbsp;  &nbsp; 
                                         <asp:Label ID="Label18" runat="server" Font-Bold="False" Font-Names="Arial"
                                             Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                             Text="No_Guia Dtos:" Width="120px"></asp:Label>
                                                        <asp:TextBox ID="txt_NoGuiaDtos" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Style="text-align: right" Width="100px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial"
                                                            Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Estatus Carga:"
                                                            Width="100px"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left" class="style107">
                                                        <asp:DropDownList ID="cbo_EstatusCargaDet" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Width="115px">
                                                        </asp:DropDownList>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;                                                                                                        
                                                    <asp:Label ID="Label19" runat="server" Font-Bold="False" Font-Names="Arial"
                                                        Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                        Text="Embarcador:" Width="120px"></asp:Label>
                                                        <asp:TextBox ID="txt_Embarcador" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Style="text-transform: uppercase" Width="140px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial"
                                                            Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Fecha Envio Dtos:"
                                                            Width="100px"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left" class="style107">
                                                        <asp:TextBox ID="txtFechaEnvioDtos" runat="server" Font-Names="Arial"
                                                            Font-Size="8pt" Style="margin-left: 0px" TabIndex="9" Width="80px" AutoPostBack="true"
                                                            OnTextChanged="txtFechaEnvioDtos_TextChanged"></asp:TextBox>
                                                        <asp:CalendarExtender ID="txtFechaEnvioDtos_CalendarExtender" runat="server"
                                                            Format="dd/MM/yyyy" TargetControlID="txtFechaEnvioDtos">
                                                        </asp:CalendarExtender>
                                                        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                                        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                                    <asp:Label ID="Label21" runat="server" Font-Bold="False" Font-Names="Arial"
                                                        Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                        Text="Dias Envio Dtos:" Width="120px"></asp:Label>
                                                        <asp:TextBox ID="txt_TiempoEnvioDtos" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Style="text-align: right" Width="30px"
                                                            AutoPostBack="true" OnTextChanged="txt_TiempoEnvioDtos_TextChanged"></asp:TextBox>
                                                        &nbsp; &nbsp; &nbsp;   &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                     &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp;  
                                                    <asp:Label ID="Label22" runat="server" Font-Bold="False" Font-Names="Arial"
                                                        Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                        Text="Efectividad Envio Dtos:" Width="120px"></asp:Label>
                                                        <asp:TextBox ID="txt_EfectividadEnvioDtos" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Style="text-align: right" Width="30px"></asp:TextBox>
                                                        &nbsp; &nbsp;
                                                     <asp:Label ID="lbl_Inspeccion" Font-Bold="False" Font-Names="Arial"
                                                         Font-Size="8pt" ForeColor="Black" runat="server" Text="Inspección Narcoticos:"></asp:Label>
                                                        <asp:CheckBox ID="chk_InspeccionSi" ForeColor="Black" runat="server" AutoPostBack="true" />
                                                    </td>
                                                </tr>



                                                 <tr>
                                                    <td>&nbsp;</td>
                                                    <td style="text-align: right;">
                                                            <asp:Label ID="Label151" runat="server" Font-Bold="False" Font-Names="Arial"
                                                            Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                            Text="Modo Transporte Despacho:" Width="100px"></asp:Label>        
                                                    </td>
                                                    <td style="text-align: left" class="style107">
                                                       <asp:TextBox ID="txt_ModTransDespa" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Style="text-align:left" Width="150px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                                        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                                   
                                                </tr>

                                               
                                            </table>
                                            <asp:Panel runat="server" ID="grp_FechasDetDespa" Font-Names="Arial" ForeColor="Black" Font-Size="8pt"
                                                GroupingText="Fechas" BorderColor="Black" HorizontalAlign="Left" Width="608px">
                                                <table style="width: 812px;">
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="Label23" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Estimada Despacho:"
                                                                Width="100px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left" class="style107">
                                                            <asp:TextBox ID="txt_FechaEstimaDespa" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" Style="margin-left: 0px" AutoPostBack="true"
                                                                OnTextChanged="txt_FechaEstimaDespa_TextChanged" TabIndex="9" Width="80px"></asp:TextBox>
                                                            <asp:CalendarExtender ID="txt_FechaEstimaDespa_Calendar" runat="server"
                                                                Format="dd/MM/yyyy" TargetControlID="txt_FechaEstimaDespa">
                                                            </asp:CalendarExtender>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    
                                           <asp:Label ID="Label24" runat="server" Font-Bold="False" Font-Names="Arial"
                                               Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                               Text="Real Despacho:" Width="100px"></asp:Label>
                                                            <asp:TextBox ID="txt_FechaRealDespa" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" Width="80px" AutoPostBack="true" OnTextChanged="txt_FechaRealDespa_TextChanged"></asp:TextBox>
                                                            <asp:CalendarExtender ID="txt_FechaRealDespa_CalendarExtender" runat="server"
                                                                Format="dd/MM/yyyy" TargetControlID="txt_FechaRealDespa">
                                                            </asp:CalendarExtender>
                                                            &nbsp;&nbsp; 
                                                        <asp:Label ID="Label55" runat="server" Font-Bold="False" Font-Names="Arial"
                                                            Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="T.T (Planta A Cliente):"
                                                            Width="160px"></asp:Label>
                                                            <asp:TextBox ID="txt_TtPlantaCliente" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="30px" AutoPostBack="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="Label25" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Estimada Zarpe:"
                                                                Width="100px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left" class="style107">
                                                            <asp:TextBox ID="txt_FechaEstimaZarpe" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" Width="80px" OnTextChanged="txt_FechaEstimaZarpe_TextChanged"
                                                                 AutoPostBack="true"></asp:TextBox>
                                                            <asp:CalendarExtender ID="txt_FechaEstimaZarpe_CalendarExtender"  runat="server"
                                                                Format="dd/MM/yyyy" TargetControlID="txt_FechaEstimaZarpe">
                                                            </asp:CalendarExtender>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
                                           <asp:Label ID="Label26" runat="server" Font-Bold="False" Font-Names="Arial"
                                               Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                               Text="Real Zarpe:" Width="100px"></asp:Label>
                                                            <asp:TextBox ID="txt_FechaRealZarpe" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" Style="margin-left: 0px" TabIndex="9" Width="80px" AutoPostBack="true"
                                                                OnTextChanged="txt_FechaRealZarpe_TextChanged"></asp:TextBox>
                                                            <asp:CalendarExtender ID="txt_FechaRealZarpe_CalendarExtender" runat="server"
                                                                Format="dd/MM/yyyy" TargetControlID="txt_FechaRealZarpe">
                                                            </asp:CalendarExtender>
                                                            &nbsp;&nbsp;
                                                          <asp:Label ID="Label58" runat="server" Font-Bold="False" Font-Names="Arial"
                                                              Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                              Text="Efectividad De Entrega:" Width="160px"></asp:Label>
                                                            <asp:TextBox ID="txt_EfectiviEntrega" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: center" Width="30px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="Label76" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                Text="Estimada Arribo:" Width="100px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left" class="style107">
                                                            <asp:TextBox ID="txt_EstimadaArribo" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" Width="80px" OnTextChanged="txt_EstimadaArribo_TextChanged"
                                                                 AutoPostBack="true"></asp:TextBox>
                                                            <asp:CalendarExtender ID="txt_EstimadaArribo_CalendarExtender" runat="server"
                                                                Format="dd/MM/yyyy" TargetControlID="txt_EstimadaArribo">
                                                            </asp:CalendarExtender>
                                                            
                                                    <asp:Label ID="Label57" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                    Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                    Text="Lead Time Esperado:" Width="160px"></asp:Label>
                                                            <asp:TextBox ID="txt_LeadTimeEspera" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="30px"></asp:TextBox>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;     
                                                                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:Label ID="lbl_diasCumple" runat="server"  Font-Bold="False" Font-Names="Arial"
                                                                    Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                    Text="Dias Cumplimiento Cliente:" Width="150px"></asp:Label>
                                                            <asp:TextBox ID="txt_DiasCumpleCliente" ReadOnly="true" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="30px"></asp:TextBox>                                                
                                            
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td>&nbsp;</td>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="Label163" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                Text="Estimada Arribo Mod:" Width="110px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left" class="style107">
                                                            <asp:TextBox ID="txt_EstimadaArriboMod" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" Width="80px" 
                                                                 AutoPostBack="true"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender18" runat="server"
                                                                Format="dd/MM/yyyy" TargetControlID="txt_EstimadaArriboMod">
                                                            </asp:CalendarExtender>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="Label30" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Estimada Llegada Obra:"
                                                                Width="120px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left" class="style107">
                                                            <asp:TextBox ID="txt_FechaEstLlegaObra" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" Width="80px" OnTextChanged="txt_FechaEstLlegaObra_TextChanged"
                                                                 AutoPostBack="true"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender9" runat="server"
                                                                Format="dd/MM/yyyy" TargetControlID="txt_FechaEstLlegaObra">
                                                            </asp:CalendarExtender>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;  &nbsp;     
                                           <asp:Label ID="Label31" runat="server" Font-Bold="False" Font-Names="Arial"
                                               Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                               Text="T.T Puerto Destino A Cliente:" Width="138px"></asp:Label>
                                                            <asp:TextBox ID="txt_PuertoDestCliente" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" Width="30px" Style="text-align: right"></asp:TextBox>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
                                                            &nbsp;&nbsp;&nbsp;&nbsp;   &nbsp;&nbsp;  
                                                               <asp:Label ID="lbl_PlanEntregaCliente" runat="server" Font-Bold="False" Font-Names="Arial"
                                               Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                               Text="Plan Entrega Cliente:" Width="140px"></asp:Label>
                                                               <asp:Label ID="lbl_FecEntreCliente" runat="server" Font-Bold="False" Font-Names="Arial"
                                               Font-Size="8pt" ForeColor="Black" Style="text-align: left"
                                               ></asp:Label>                                                                                                                                                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="Label164" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Estimada Llegada Obra Mod:"
                                                                Width="140px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left" class="style107">
                                                            <asp:TextBox ID="txt_FechaEstLlegaObraMod" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" Width="80px" 
                                                                 AutoPostBack="true"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender19" runat="server"
                                                                Format="dd/MM/yyyy" TargetControlID="txt_FechaEstLlegaObraMod">
                                                            </asp:CalendarExtender>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="Label27" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="T.T Internacional:"
                                                                Width="100px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left" class="style107">
                                                            <asp:TextBox ID="txt_TtInternacional" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" Width="30px" Style="text-align: right"></asp:TextBox>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   
                                                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;                                                        
                                                   <asp:Label ID="Label95" runat="server" Font-Bold="False" Font-Names="Arial"
                                               Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                               Text="Fecha Documentado:" Width="110px"></asp:Label>
                                                            <asp:TextBox ID="txt_fechaDocumentado" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" Style="margin-left: 0px" TabIndex="9" Width="80px" AutoPostBack="true"
                                                            ></asp:TextBox>   <asp:CalendarExtender ID="CalendarExtender15" runat="server"
                                                                Format="dd/MM/yyyy" TargetControlID="txt_fechaDocumentado">
                                                            </asp:CalendarExtender>
                                                            
                                                            
                                                         
                                                                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
                                                            &nbsp;&nbsp;&nbsp;&nbsp;   &nbsp;&nbsp;                                                                                                      
                                        <asp:Label ID="Label29" runat="server" Font-Bold="False" Font-Names="Arial"
                                               Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                               Text="Real Llegada Obra:" Width="100px"></asp:Label>
                                                            <asp:TextBox ID="txt_FechaRealLlegaObra" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" Style="margin-left: 0px" TabIndex="9" Width="80px"
                                                                AutoPostBack="true" OnTextChanged="txt_FechaRealLlegaObra_TextChanged"></asp:TextBox>
                                                            <asp:CalendarExtender ID="txt_FechaRealLlegaObra_CalendarExtender" runat="server"
                                                                Format="dd/MM/yyyy" TargetControlID="txt_FechaRealLlegaObra">
                                                            </asp:CalendarExtender>
                                                        
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="Label14" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Width="160px" Style="text-align: right" runat="server" Text="Cerrado:"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left" class="style107">
                                                            <asp:CheckBox ID="chk_DetalleCerrado" ForeColor="Black" runat="server" AutoPostBack="true" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                                                                                                                                                                                                                                                              
                                                               <asp:Button ID="btnGuardarDetDesp" runat="server" BackColor="#1C5AB6"
                                                                   BorderColor="#999999" CssClass="botonsio" Font-Bold="True" Font-Names="Arial"
                                                                   Font-Size="8pt" ForeColor="White" OnClick="btnGuardarDetDesp_Click"
                                                                   OnClientClick="return confirm('¿Desea Enviar los Datos?')" Text="Guardar"
                                                                   Width="70px" />
                                                            &nbsp;&nbsp;  
                                                                              <asp:Label ID="Label28" runat="server" Font-Bold="False" Font-Names="Arial"
                                              Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                              Text="Confirmada Arribo:" Width="100px"></asp:Label>
                                                            <asp:TextBox ID="txt_FechaConfirmaArribo" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" Width="80px" AutoPostBack="true" OnTextChanged="txt_FechaConfirmaArribo_TextChanged"></asp:TextBox>
                                                            <asp:CalendarExtender ID="txt_FechaConfirmaArribo_CalendarExtender" runat="server"
                                                                Format="dd/MM/yyyy" TargetControlID="txt_FechaConfirmaArribo">
                                                            </asp:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                </table>                             
                                            </asp:Panel>
                                            



                                              <asp:Panel runat="server" ID="Panel_Tramites" Font-Names="Arial" ForeColor="Black" Font-Size="8pt"
                                                GroupingText="Tramite Pais Destino" BorderColor="Black" HorizontalAlign="Left" Width="608px">
                                                <table style="width: 812px;">
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td style="text-align: right;">
                                                         <asp:Label ID="Label94" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="F Arribo (Finalizacion):"
                                                                Width="120px"></asp:Label>   
                                                        </td>
                                                        <td style="text-align: left" class="style107">
                                                            <asp:TextBox ID="txt_Finalizacion" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" Style="margin-left: 0px" AutoPostBack="true"
                                                                OnTextChanged="txt_Finalizacion_TextChanged" TabIndex="9" Width="80px"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender3" runat="server"
                                                                Format="dd/MM/yyyy" TargetControlID="txt_Finalizacion">
                                                            </asp:CalendarExtender>    
                                                             
                                          <asp:Label ID="Label106" runat="server" Font-Bold="False" Font-Names="Arial"
                                                              Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                              Text="F Facturac Prov a Forsa:" Width="160px"></asp:Label> 
                                                           <asp:TextBox ID="txt_FacturacionProveed" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                               Width="80px" OnTextChanged="txt_FacturacionProveed_TextChanged"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender10" runat="server"
                                                                Format="dd/MM/yyyy" TargetControlID="txt_FacturacionProveed">
                                                            </asp:CalendarExtender> 
                                                            &nbsp;&nbsp;
                                                             <asp:Label ID="Label112" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="F Devol contenedores:"
                                                                Width="150px"></asp:Label>
                                                           <asp:TextBox ID="txt_Devolucion_Conten" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" Style="margin-left: 0px" TabIndex="9" Width="80px"
                                                                AutoPostBack="true" OnTextChanged="txt_Devolucion_Conten_TextChanged"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender12" runat="server"
                                                                Format="dd/MM/yyyy" TargetControlID="txt_Devolucion_Conten">
                                                            </asp:CalendarExtender>  
                                                   
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td style="text-align: right;">
                                                              <asp:Label ID="Label97" runat="server" Font-Bold="False" Font-Names="Arial"
                                               Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                               Text="Dias libres de demoras (cont):" Width="150px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left" class="style107">
                                                             <asp:TextBox ID="txt_Dias_libres" runat="server" Font-Names="Arial"
                                                               Style="text-align: right"  Font-Size="8pt" Width="30px" MaxLength="2" AutoPostBack="true"></asp:TextBox> 
                                                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                           <asp:Label ID="Label107" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                Text="F Pago de CI Impuestos:" Width="118px"></asp:Label>
                                                             <asp:TextBox ID="txt_CI_impuestos" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" Width="80px" OnTextChanged="txt_CI_impuestos_TextChanged"
                                                                AutoPostBack="true"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender11" runat="server"
                                                                Format="dd/MM/yyyy" TargetControlID="txt_CI_impuestos">
                                                            </asp:CalendarExtender>
                                                            &nbsp;&nbsp;
                                                             <asp:Label ID="Label110" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="F de Almacenam(Cargo):"
                                                                Width="150px"></asp:Label>
                                                         <asp:TextBox ID="txt_Almacenamiento" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" Width="80px" OnTextChanged="txt_Almacenamiento_TextChanged"
                                                                AutoPostBack="true"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender13" runat="server"
                                                                Format="dd/MM/yyyy" TargetControlID="txt_Almacenamiento">
                                                            </asp:CalendarExtender>                                                      
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td style="text-align: right;">
                                               <asp:Label ID="Label75" runat="server" Font-Bold="False" Font-Names="Arial"
                                                            Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="F Inspeccion:"
                                                            Width="160px"></asp:Label> 
                                                        </td>
                                                        <td style="text-align: left" class="style107">
                                                                      <asp:TextBox ID="txt_Inspeccion" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" Style="margin-left: 0px" AutoPostBack="true"
                                                                OnTextChanged="txt_Inspeccion_TextChanged" TabIndex="9" Width="80px"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender2" runat="server"
                                                                Format="dd/MM/yyyy" TargetControlID="txt_Inspeccion">
                                                            </asp:CalendarExtender> 
                                                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                             <asp:Label ID="Label111" runat="server" Font-Bold="False" Font-Names="Arial"
                                               Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                               Text="F Notificación a Cliente:" Width="140px"></asp:Label>
                                                             <asp:TextBox ID="txt_Notifica_Cliente" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" Style="margin-left: 0px" TabIndex="9" Width="80px"
                                                                AutoPostBack="true" OnTextChanged="txt_Notifica_Cliente_TextChanged"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender5" runat="server"
                                                                Format="dd/MM/yyyy" TargetControlID="txt_Notifica_Cliente">
                                                            </asp:CalendarExtender>
                                                            &nbsp;&nbsp;
                                                                <asp:Label ID="Label113" runat="server" Font-Bold="False" Font-Names="Arial"
                                               Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                               Text="F Facturac Forsa a Cliente:" Width="149px"></asp:Label>
                                                            <asp:TextBox ID="txt_Facturacion_ForsaCliente" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" Style="margin-left: 0px" TabIndex="9" Width="80px"
                                                                AutoPostBack="true" OnTextChanged="txt_Facturacion_ForsaCliente_TextChanged"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender8" runat="server"
                                                                Format="dd/MM/yyyy" TargetControlID="txt_Facturacion_ForsaCliente">
                                                            </asp:CalendarExtender> 
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td style="text-align: right;">
                                                       <asp:Label ID="Label100" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Canal:"
                                                                Width="100px"></asp:Label> 
                                                        </td>
                                                        <td style="text-align: left" class="style107">
                                                             <asp:DropDownList ID="cbo_Canal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Width="88px">
                                                            </asp:DropDownList> 
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                                             
                <asp:Label ID="Label108" runat="server" Font-Bold="False" Font-Names="Arial"
                                              Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                              Text="F Retiro Contenedores:" Width="135px"></asp:Label>  
                                                         <asp:TextBox ID="txt_Retiro_Conten" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" Width="80px" AutoPostBack="true" OnTextChanged="txt_Retiro_Conten_TextChanged"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender4" runat="server"
                                                                Format="dd/MM/yyyy" TargetControlID="txt_Retiro_Conten">
                                                            </asp:CalendarExtender>  
                                                            &nbsp;&nbsp; &nbsp;  
                                                            <asp:Label ID="Label92" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                   Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                   Text="F de carga cliente:" Width="143px"></asp:Label>
                                                             <asp:TextBox ID="txt_Fecha_Carga_Cliente" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" Style="margin-left: 0px" TabIndex="9" Width="80px"
                                                                AutoPostBack="true" OnTextChanged="txt_Fecha_Carga_Cliente_TextChanged"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender14" runat="server"
                                                                Format="dd/MM/yyyy" TargetControlID="txt_Fecha_Carga_Cliente">
                                                            </asp:CalendarExtender>                                                                                                                                                                                               
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td style="text-align: right;">
                                                        <asp:Label ID="Label103" runat="server" Font-Bold="False" Font-Names="Arial"
                                               Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                               Text="F de CI Nacionalización:" Width="140px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left" class="style107">
                                                        <asp:TextBox ID="txt_Nacionalizacion" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" Style="margin-left: 0px" TabIndex="9" Width="80px" AutoPostBack="true"
                                                                OnTextChanged="txt_Nacionalizacion_TextChanged"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender6" runat="server"
                                                                Format="dd/MM/yyyy" TargetControlID="txt_Nacionalizacion">
                                                            </asp:CalendarExtender>
                                                            &nbsp;&nbsp;&nbsp; 
                                                                                                                                                            
                                              <asp:Label ID="Label109" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                    Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                    Text="F Vaciado Conten (Desove):" Width="150px"></asp:Label> 
                                                             <asp:TextBox ID="txt_Desove" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" Width="80px" AutoPostBack="true" OnTextChanged="txt_Desove_TextChanged"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender7" runat="server"
                                                                Format="dd/MM/yyyy" TargetControlID="txt_Desove">
                                                            </asp:CalendarExtender>
                                                             &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Label ID="Label93" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                                                  Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                                                  Text="F F Entrega en obra:" Width="140px"></asp:Label>
                                                             <asp:TextBox ID="txt_Entrega_Obra" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" Width="80px" AutoPostBack="true" OnTextChanged="txt_Entrega_Obra_TextChanged"></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender16" runat="server"
                                                                Format="dd/MM/yyyy" TargetControlID="txt_Entrega_Obra">
                                                            </asp:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="Label119" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Width="160px" Style="text-align: right" runat="server" Text="Cerrado:"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left" class="style107">
                                                            <asp:CheckBox ID="chk_Tramite_Cerrado" ForeColor="Black" runat="server" AutoPostBack="true" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    
                                                            &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;                                                                                                                                                                                                                                                            
                                                               <asp:Button ID="btn_Guardar_Tramite" runat="server" BackColor="#1C5AB6"
                                                                   BorderColor="#999999" CssClass="botonsio" Font-Bold="True" Font-Names="Arial"
                                                                   Font-Size="8pt" ForeColor="White" OnClick="btn_Guardar_Tramite_Click"
                                                                   OnClientClick="return confirm('¿Desea Enviar los Datos?')" Text="Guardar"
                                                                   Width="70px" />
                                                        </td>
                                                    </tr>
                                                </table>                             
                                            </asp:Panel>                                         
                                        </Content>
                                    </asp:AccordionPane>















                                    <asp:AccordionPane ID="Acc_GastosOperaciones" runat="server"
                                        ContentCssClass="accordionContent" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected">
                                        <Header>
                                            <asp:Label ID="lbl_capi2" runat="server" Text="Gastos De Operacion"></asp:Label>
                                        </Header>
                                        <Content>
                                            <asp:Panel runat="server" Font-Names="Arial" ForeColor="Black" Font-Size="8pt"
                                                BorderColor="Black" GroupingText=".">
                                                <table style="width: 800px;">
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="Label45" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                Text="Gastos Y Fletes Facturados:" Width="160px"></asp:Label>

                                                        </td>
                                                        <td style="text-align: left" class="style107">
                                                            <asp:TextBox ID="txt_GastFletFacturados" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="100px" AutoPostBack="true"></asp:TextBox>
                                                            <br />
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <br />
                                                        <td>&nbsp;</td>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="Label73" runat="server" Font-Bold="true" Font-Names="Arial"
                                                                Font-Size="9pt" ForeColor="Black" Style="text-align: right" Text="(Provision)"
                                                                Width="100px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left" class="style107">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                                                                                                              
                                                                 <asp:Label ID="Label74" runat="server" Font-Bold="true" Font-Names="Arial"
                                                                     Font-Size="9pt" ForeColor="Black" Style="text-align: right"
                                                                     Text="(Real)" Width="100px"></asp:Label>
                                                                                          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                                                                                   
                                                                 <asp:Label ID="Label43" runat="server" Font-Bold="true" Font-Names="Arial"
                                                                     Font-Size="9pt" ForeColor="Black" Style="text-align: right"
                                                                     Text="(Real)" Width="100px"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="Label46" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Flete Nacional:"
                                                                Width="160px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left" class="style107">
                                                            <asp:TextBox ID="txt_FleteNalProvi" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="100px" AutoPostBack="true" OnTextChanged="txt_FleteNalProvi_TextChanged"></asp:TextBox>
                                                            <asp:Label ID="Label47" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                Text="Flete Nacional:" Width="160px"></asp:Label>
                                                            <asp:TextBox ID="txt_FletNalReal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="100px" AutoPostBack="true" OnTextChanged="txt_FletNalReal_TextChanged"></asp:TextBox>
                                                            <asp:Label ID="Label48" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                Text="Stand By US$:" Width="120px"></asp:Label>
                                                            <asp:TextBox ID="txt_StandBy" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="80px" OnTextChanged="txt_StandBy_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="Label49" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Agradecimiento Aduanero US$:"
                                                                Width="160px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left" class="style107">
                                                            <asp:TextBox ID="txt_AgradeAduanProvi" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="100px" AutoPostBack="true" OnTextChanged="txt_AgradeAduanProvi_TextChanged"></asp:TextBox>
                                                            <asp:Label ID="Label50" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                Text="Agradecimiento Aduanero US$:" Width="160px"></asp:Label>
                                                            <asp:TextBox ID="txt_AgradeAduanReal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="100px" AutoPostBack="true" OnTextChanged="txt_AgradeAduanReal_TextChanged"></asp:TextBox>
                                                            <asp:Label ID="Label51" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                Text="Trm (Fecha Factura):" Width="120px"></asp:Label>
                                                            <asp:TextBox ID="txt_TrmFechafact" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="80px"></asp:TextBox>
                                                        </td>
                                                    </tr>


                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="Label35" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Gastos En Puerto US$:"
                                                                Width="160px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left" class="style107">
                                                            <asp:TextBox ID="txt_GastPuertoProvision" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="100px" AutoPostBack="true" OnTextChanged="txt_GastPuertoProvision_TextChanged"></asp:TextBox>
                                                            <asp:Label ID="Label36" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                Text="Gastos En Puerto US$:" Width="160px"></asp:Label>
                                                            <asp:TextBox ID="txt_GastPuertoReal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="100px" AutoPostBack="true" OnTextChanged="txt_GastPuertoReal_TextChanged"></asp:TextBox>
                                                            <asp:Label ID="Label37" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                Text="Bodegaje US$:" Width="120px"></asp:Label>
                                                            <asp:TextBox ID="txt_BodegajeReal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="80px" OnTextChanged="txt_BodegajeReal_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="Label38" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Flete Inter US$:"
                                                                Width="160px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left" class="style107">
                                                            <asp:TextBox ID="txt_FleteInterProvision" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="100px" AutoPostBack="true" OnTextChanged="txt_FleteInterProvision_TextChanged"></asp:TextBox>
                                                            <asp:Label ID="Label39" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                Text="Flete Inter US$:" Width="160px"></asp:Label>
                                                            <asp:TextBox ID="txt_FleteInterReal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="100px" AutoPostBack="true" OnTextChanged="txt_FleteInterReal_TextChanged"></asp:TextBox>
                                                           <asp:Label ID="Label157" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                Text="Aduana Destino US$:" Width="120px"></asp:Label>
                                                            <asp:TextBox ID="txt_AduanaDestino" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="80px" AutoPostBack="true" OnTextChanged="txt_AduanaDestino_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="Label32" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Gastos Destino US$:"
                                                                Width="160px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left" class="style107">
                                                            <asp:TextBox ID="txt_GastDestinoProvision" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="100px" AutoPostBack="true" OnTextChanged="txt_GastDestinoProvision_TextChanged"></asp:TextBox>
                                                            <asp:Label ID="Label33" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                Text="Gastos Destino US$:" Width="160px"></asp:Label>
                                                            <asp:TextBox ID="txt_GastDestinoReal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="100px" AutoPostBack="true" OnTextChanged="txt_GastDestinoReal_TextChanged"></asp:TextBox>
                                                             <asp:Label ID="Label161" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                Text="Demoras Destino US$:" Width="120px"></asp:Label>
                                                            <asp:TextBox ID="txt_DemoraDestino" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="80px" AutoPostBack="true" OnTextChanged="txt_DemoraDestino_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="Label34" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Sello Satelital US$:"
                                                                Width="160px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left" class="style107">
                                                            <asp:TextBox ID="txt_SelloSateliProvi" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="100px" AutoPostBack="true" OnTextChanged="txt_SelloSateliProvi_TextChanged"></asp:TextBox>
                                                            <asp:Label ID="Label40" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                Text="Sello Satelital US$:" Width="160px"></asp:Label>
                                                            <asp:TextBox ID="txt_SelloSateliReal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="100px" AutoPostBack="true" OnTextChanged="txt_SelloSateliReal_TextChanged"></asp:TextBox>
                                                              <asp:Label ID="Label156" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                Text="Roll Over US$:" Width="120px"></asp:Label>
                                                            <asp:TextBox ID="txt_RollOver" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="80px" AutoPostBack="true" OnTextChanged="txt_RollOver_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                       <td>&nbsp;</td>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="Label153" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Seguro US$:"
                                                                Width="160px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left" class="style107">
                                                            <asp:TextBox ID="txt_Seguro" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="100px" AutoPostBack="true" OnTextChanged="txt_Seguro_TextChanged"></asp:TextBox>                        
                                                                 <asp:Label ID="Label154" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                Text="Inspección Antinarcoticos US$:" Width="160px"></asp:Label>
                                                            <asp:TextBox ID="txt_inspAntiNarcot" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="100px" AutoPostBack="true" OnTextChanged="txt_inspAntiNarcot_TextChanged"></asp:TextBox> 
                                                              <asp:Label ID="Label159" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                Text="Bodegaje Destino US$:" Width="120px"></asp:Label>
                                                            <asp:TextBox ID="txt_BodegajeDestino" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="80px" AutoPostBack="true" OnTextChanged="txt_BodegajeDestino_TextChanged"></asp:TextBox>                                                                                                                                                                                                                                         
                                                        </td>
                                                    </tr>                                                                                                                                                                                                                   
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td style="text-align: left" class="style107">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                                                                                                                                                                               
                                                                 <asp:Label ID="Label158" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                     Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                     Text="Menejo Fletes US$:" Width="160px"></asp:Label>
                                                            <asp:TextBox ID="txt_ManejoFlete" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="100px" AutoPostBack="true" OnTextChanged="txt_ManejoFlete_TextChanged"></asp:TextBox>
                                                            <asp:Label ID="Label162" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                Text="Impuestos Destino US$:" Width="120px"></asp:Label>
                                                            <asp:TextBox ID="txt_ImpuestoDestino" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="80px" AutoPostBack="true" OnTextChanged="txt_ImpuestoDestino_TextChanged"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;                                                                                                                         
                                                        </td>                                                                                                          
                                                        <td style="text-align: left" class="style107">&nbsp;&nbsp;
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                                                                                                                                                                                 
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                                                                                                                                                                                                                                                                                                                                                                                                             
                                                             <asp:Label ID="Label160" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                Text="Transporte Destino US$:" Width="120px"></asp:Label>
                                                            <asp:TextBox ID="txt_TranspDestino" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="100px" AutoPostBack="true" OnTextChanged="txt_TranspDestino_TextChanged"></asp:TextBox>  
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <hr />
                                                        </td>
                                                        <td>
                                                            <hr />
                                                        </td>
                                                        <td>
                                                            <hr />
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                       <td>&nbsp;</td>
                                                         <td style="text-align: right;">
                                                            <asp:Label ID="Label41" runat="server" Font-Bold="True" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Total Gastos Y Fletes US$:"
                                                                Width="160px"></asp:Label>
                                                        </td>                                                       
                                                        <td style="text-align: left" class="style107">
                                                                                         <asp:TextBox ID="txt_TotalProvi" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="100px" AutoPostBack="true" OnTextChanged="txt_TotalProvi_TextChanged"></asp:TextBox>   
                                                             <asp:Label ID="Label42" runat="server" Font-Bold="True" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                Text="Total Gastos Y Fletes US$:" Width="160px"></asp:Label>
                                                            <asp:TextBox ID="txt_TotalGastFletreal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="100px"></asp:TextBox>
                                                               
                                                        </td>                                                       
                                                    </tr>
                                                      <tr>
                                                       <td>&nbsp;</td>
                                                            <td style="text-align: right;">
                                                            <asp:Label ID="Label155" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Dif Provisiones:"
                                                                Width="160px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left" class="style107">
                                                         <asp:TextBox ID="txt_Difprovi" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="100px"></asp:TextBox>
                                                               <asp:Label ID="Label44" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                Text="% Ahorro:" Width="160px"></asp:Label>
                                                            <asp:TextBox ID="txt_PorcentajeAhorro" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="50px"></asp:TextBox>
                                                        </td>                                                       
                                                    </tr>
                                                         

                                                         <tr>
                                                       <td>&nbsp;</td>
                                                           <td>&nbsp;</td>
                                                        <td style="text-align: left" class="style107">
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                                                        
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td>
                                                            <hr />
                                                        </td>
                                                        <td>
                                                            <hr />
                                                        </td>
                                                        <td>
                                                            <hr />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="Label52" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Peso Factura:"
                                                                Width="160px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left" class="style107">
                                                            <asp:TextBox ID="txt_PesoFactura" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="100px"></asp:TextBox>
                                                            <asp:Label ID="Label54" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                Text="Envio Control Empaque:" Width="160px"></asp:Label>
                                                            <asp:TextBox ID="txt_EnvioCtrlEmpaque" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Width="100px"></asp:TextBox>
                                                            <asp:CalendarExtender ID="txt_EnvioCtrlEmpaque_CalendarExtender" runat="server"
                                                                Format="dd/MM/yyyy" TargetControlID="txt_EnvioCtrlEmpaque">
                                                            </asp:CalendarExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="Label56" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                Text="Concatenar:" Width="160px"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left" class="style107">
                                                            <asp:TextBox ID="txt_Concatenar" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: left" Width="150px" OnTextChanged="txt_Concatenar_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>&nbsp;</td>
                                                        <td style="text-align: right;">
                                                            <asp:Label ID="Label53" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Width="160px" Style="text-align: right" runat="server" Text="Cerrado:"></asp:Label>
                                                        </td>
                                                        <td style="text-align: left" class="style107">
                                                            <asp:CheckBox ID="chk_Cerrado" ForeColor="Black" runat="server" AutoPostBack="true" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   
                                                                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;   
                                                                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                   &nbsp;&nbsp;&nbsp;                                                                                                                                                                                                        
                                                               <asp:Button ID="btn_GuardarGast" runat="server" BackColor="#1C5AB6"
                                                                   BorderColor="#999999" CssClass="botonsio" Font-Bold="True" Font-Names="Arial"
                                                                   Font-Size="8pt" ForeColor="White" OnClick="btn_GuardarGast_Click"
                                                                   OnClientClick="return confirm('Desea Enviar El Formulario')" Text="Guardar"
                                                                   Width="70px"/>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </Content>
                                    </asp:AccordionPane>




                                          <asp:AccordionPane ID="Acc_GastosDestinoBrasil" runat="server"
                                        ContentCssClass="accordionContent" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected">
                                        <Header>
                                            <asp:Label ID="Label96" runat="server" Text="Gastos En Destino Brasil"></asp:Label>
                                        </Header>
                                              <Content>
                                                  <asp:Panel runat="server" Font-Names="Arial" ForeColor="Black" Font-Size="8pt"
                                                      BorderColor="Black" GroupingText=".">
                                                      <table style="width: 600px;">
                                                            <tr>
                                                              <td>
                                                                   <asp:Label ID="Label145" runat="server"  Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="No_Proceso:"
                                                               ></asp:Label>
                                                                   <asp:Label ID="Label149" runat="server"  Font-Names="Arial"
                                                                Font-Size="10pt" ForeColor="RED" Style="text-align: right" Text="*"
                                                               ></asp:Label>
                                                                   <asp:TextBox ID="txt_noProceso" Style="text-transform: uppercase" runat="server" MaxLength="30" Font-Names="Arial" Font-Size="8pt"
                                                                Width="100px"></asp:TextBox>
                                                                  </td>
                                                                  <td>
                                                                       <asp:Label ID="Label146" runat="server"  Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Fecha Planilla:"
                                                                ></asp:Label>
                                                                           <asp:Label ID="Label150" runat="server"  Font-Names="Arial"
                                                                Font-Size="10pt" ForeColor="RED" Style="text-align: right" Text="*"
                                                               ></asp:Label>
                                                                  <asp:TextBox ID="txt_fechPlanilla" runat="server" OnTextChanged="txt_fechPlanilla_TextChanged" Font-Names="Arial" Font-Size="8pt"
                                                                Width="60px"></asp:TextBox>
                                                                        <asp:CalendarExtender ID="CalendarExtender17"  runat="server"
                                                                Format="dd/MM/yyyy" TargetControlID="txt_fechPlanilla">
                                                            </asp:CalendarExtender>
                                                              </td>                                                                
                                                          </tr>
                                                            <tr>
                                                              <td>
                                                                   <asp:Label ID="Label147" runat="server"  Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="TRM:"
                                                               ></asp:Label>
                                                                   <asp:TextBox ID="txt_trm" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                  Style="text-align: right" Width="100px"></asp:TextBox>
                                                                  </td>
                                                                  <td>
                                                                &nbsp;
                                                              </td>                                                                
                                                          </tr>
                                                            <tr>
                                                              <td>
                                                                  &nbsp;
                                                                  <td>
                                                                      &nbsp;
                                                                  </td>
                                                              </td>
                                                          </tr>
                                                          <tr>
                                                              <td>
                                                                   <asp:Label ID="Label98" runat="server" Font-Bold="true" Font-Names="Arial"
                                                                Font-Size="9pt" ForeColor="Black" Style="text-align: right" Text="(Provision)"
                                                                Width="100px"></asp:Label>
                                                                  <td>
                                                                       <asp:Label ID="Label99" runat="server" Font-Bold="true" Font-Names="Arial"
                                                                Font-Size="9pt" ForeColor="Black" Style="text-align: right" Text="(Real)"
                                                                Width="100px"></asp:Label>
                                                                  </td>
                                                              </td>
                                                          </tr>
                                                             <tr>
                                                              <td>
                                                                   <asp:Label ID="Label101" runat="server" Font-Bold="true" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="GASTO AGENTE DESTINO"
                                                                Width="150px"></asp:Label>
                                                                  <td>
                                                                       <asp:Label ID="Label102" runat="server" Font-Bold="true" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="GASTO AGENTE DESTINO"
                                                                Width="150px"></asp:Label>
                                                                  </td>
                                                              </td>
                                                          </tr>
                                                             <tr>
                                                              <td>
                                                                   <asp:Label ID="Label104" runat="server"  Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Liberación HBL:"></asp:Label>
                                                                  <asp:TextBox ID="txt_liberaHblProv" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="100px"></asp:TextBox>
                                                                  </td>
                                                                  <td>
                                                                       <asp:Label ID="Label105" runat="server"  Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Liberación HBL:"></asp:Label>
                                                                   <asp:TextBox ID="txt_liberaHblReal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                 Style="text-align: right" Width="100px"></asp:TextBox>                                                           
                                                              </td>
                                                          </tr>
                                                           <tr>
                                                              <td>
                                                                   <asp:Label ID="Label114" runat="server"  Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Dropp Off:"
                                                               ></asp:Label>
                                                                   <asp:TextBox ID="txt_droppOffProvi" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                              Style="text-align: right"   Width="100px"></asp:TextBox>
                                                                  </td>
                                                                  <td>
                                                                       <asp:Label ID="Label115" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Dropp Off:"
                                                                ></asp:Label>
                                                                  <asp:TextBox ID="txt_droppOffReal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="100px"></asp:TextBox>
                                                              </td>
                                                          </tr>
                                                             <tr>
                                                              <td>
                                                                   <asp:Label ID="Label116" runat="server"  Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Taxa Administrativa:"
                                                               ></asp:Label>
                                                                   <asp:TextBox ID="txt_taxaAdminProv" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                               Style="text-align: right"  Width="100px"></asp:TextBox>
                                                                  </td>
                                                                  <td>
                                                                       <asp:Label ID="Label117" runat="server"  Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Taxa Administrativa:"
                                                                ></asp:Label>
                                                                  <asp:TextBox ID="txt_taxaAdminReal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                              Style="text-align: right"   Width="100px"></asp:TextBox>
                                                              </td>
                                                          </tr>
                                                              <tr>
                                                              <td>
                                                                   <asp:Label ID="Label118" runat="server"  Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="ISPS:"
                                                               ></asp:Label>
                                                                   <asp:TextBox ID="txt_ispsProv" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                               Style="text-align: right"  Width="100px"></asp:TextBox>
                                                                  </td>
                                                                  <td>
                                                                       <asp:Label ID="Label120" runat="server"  Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="ISPS:"
                                                                ></asp:Label>
                                                                  <asp:TextBox ID="txt_ispsReal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                               Style="text-align: right"  Width="100px"></asp:TextBox>
                                                              </td>
                                                          </tr>
                                                              <tr>
                                                              <td>
                                                                   <asp:Label ID="Label121" runat="server"  Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Impuestos y Otros:"
                                                               ></asp:Label>
                                                                   <asp:TextBox ID="txt_otrosGastosProv" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                               Style="text-align: right"  Width="100px"></asp:TextBox>
                                                                  </td>
                                                                  <td>
                                                                       <asp:Label ID="Label122" runat="server"  Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Impuestos y Otros:"
                                                                ></asp:Label>
                                                                  <asp:TextBox ID="txt_otrosGastosReal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                               Style="text-align: right"  Width="100px"></asp:TextBox>
                                                              </td>
                                                          </tr>
                                                          <tr>
                                                              <td>&nbsp
                                                                  <td>&nbsp
                                                                  </td>
                                                              </td>
                                                          </tr>
                                                           <tr>
                                                              <td>
                                                                   <asp:Label ID="Label123" runat="server" Font-Bold="true" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="BODEGAJES"
                                                                Width="150px"></asp:Label>
                                                                  <td>
                                                                       <asp:Label ID="Label124" runat="server" Font-Bold="true" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="BODEGAJES"
                                                                Width="150px"></asp:Label>
                                                                  </td>
                                                              </td>
                                                          </tr>
                                                            <tr>
                                                              <td>
                                                                   <asp:Label ID="Label125" runat="server"  Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="1er Periodo:"
                                                               ></asp:Label>
                                                                   <asp:TextBox ID="txt_1erPeriodoProv" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                               Style="text-align: right"  Width="100px"></asp:TextBox>
                                                                  </td>
                                                                  <td>
                                                                       <asp:Label ID="Label126" runat="server"  Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="1er Periodo:"
                                                                ></asp:Label>
                                                                  <asp:TextBox ID="txt_1erPeriodoReal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                              Style="text-align: right"   Width="100px"></asp:TextBox>
                                                              </td>
                                                          </tr>
                                                            <tr>
                                                              <td>
                                                                   <asp:Label ID="Label127" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="2do Periodo:"
                                                               ></asp:Label>
                                                                   <asp:TextBox ID="txt_2doPeriodoProv" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                              Style="text-align: right"   Width="100px"></asp:TextBox>
                                                                  </td>
                                                                  <td>
                                                                       <asp:Label ID="Label128" runat="server"  Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="2do Periodo:"
                                                                ></asp:Label>
                                                                  <asp:TextBox ID="txt_2doPeriodoReal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                              Style="text-align: right"   Width="100px"></asp:TextBox>
                                                              </td>
                                                          </tr>
                                                            <tr>
                                                              <td>
                                                                   <asp:Label ID="Label129" runat="server"  Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="3er Periodo:"
                                                               ></asp:Label>
                                                                   <asp:TextBox ID="txt_3erPeriodoProv" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                             Style="text-align: right"    Width="100px"></asp:TextBox>
                                                                  </td>
                                                                  <td>
                                                                       <asp:Label ID="Label130" runat="server"  Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="3er Periodo:"
                                                                ></asp:Label>
                                                                  <asp:TextBox ID="txt_3erPeriodoReal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                              Style="text-align: right"   Width="100px"></asp:TextBox>
                                                              </td>
                                                          </tr>
                                                            <tr>
                                                              <td>
                                                                   <asp:Label ID="Label131" runat="server"  Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Escaneo de Contenedor:"
                                                               ></asp:Label>
                                                                   <asp:TextBox ID="txt_escanContenProv" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Style="text-align: right"     Width="100px"></asp:TextBox>
                                                                  </td>
                                                                  <td>
                                                                       <asp:Label ID="Label132" runat="server"  Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Escaneo de Contenedor:"
                                                                ></asp:Label>
                                                                  <asp:TextBox ID="txt_escanContenReal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                             Style="text-align: right"    Width="100px"></asp:TextBox>
                                                              </td>
                                                          </tr>
                                                            <tr>
                                                              <td>
                                                                   <asp:Label ID="Label133" runat="server"  Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Inspeccion de Mapa:"
                                                               ></asp:Label>
                                                                   <asp:TextBox ID="txt_insoMapaProv" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Style="text-align: right"     Width="100px"></asp:TextBox>
                                                                  </td>
                                                                  <td>
                                                                       <asp:Label ID="Label134" runat="server"  Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Inspeccion de Mapa:"
                                                                ></asp:Label>
                                                                  <asp:TextBox ID="txt_insoMapaReal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Style="text-align: right"     Width="100px"></asp:TextBox>
                                                              </td>
                                                          </tr>
                                                            <tr>
                                                              <td>
                                                                   <asp:Label ID="Label135" runat="server"  Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Taxa de Corretagem:"
                                                               ></asp:Label>
                                                                   <asp:TextBox ID="txt_corretagenProv" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                               Style="text-align: right"  Width="100px"></asp:TextBox>
                                                                  </td>
                                                                  <td>
                                                                       <asp:Label ID="Label136" runat="server"  Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Taxa de Corretagem:"
                                                                ></asp:Label>
                                                                  <asp:TextBox ID="txt_corretagenReal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                              Style="text-align: right"   Width="100px"></asp:TextBox>
                                                              </td>
                                                          </tr>
                                                            <tr>
                                                              <td>
                                                                   <asp:Label ID="Label137" runat="server"  Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Demurrage:"
                                                               ></asp:Label>
                                                                   <asp:TextBox ID="txt_demurrageProv" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                               Style="text-align: right"  Width="100px"></asp:TextBox>
                                                                  </td>
                                                                  <td>
                                                                       <asp:Label ID="Label138" runat="server"  Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Demurrage:"
                                                                ></asp:Label>
                                                                  <asp:TextBox ID="txt_demurrageReal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                               Style="text-align: right"  Width="100px"></asp:TextBox>
                                                              </td>
                                                          </tr>
                                                            <tr>
                                                              <td>
                                                                   <asp:Label ID="Label139" runat="server"  Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Despachante(Honorários):"
                                                               ></asp:Label>
                                                                   <asp:TextBox ID="txt_despaHonoraProv" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                             Style="text-align: right"    Width="100px"></asp:TextBox>
                                                                  </td>
                                                                  <td>
                                                                       <asp:Label ID="Label140" runat="server"  Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Despachante(Honorários):"
                                                                ></asp:Label>
                                                                  <asp:TextBox ID="txt_despaHonoraReal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                               Style="text-align: right"  Width="100px"></asp:TextBox>
                                                              </td>
                                                          </tr>
                                                            <tr>
                                                              <td>
                                                                   <asp:Label ID="Label141" runat="server"  Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Margen2% Timbro:"
                                                               ></asp:Label>
                                                                   <asp:TextBox ID="txt_margenTimboProv" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                              Style="text-align: right"   Width="100px"></asp:TextBox>
                                                                  </td>
                                                                  <td>
                                                                       <asp:Label ID="Label142" runat="server"  Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Margen2% Timbro:"
                                                                ></asp:Label>
                                                                  <asp:TextBox ID="txt_margenTimboReal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                             Style="text-align: right"    Width="100px"></asp:TextBox>
                                                              </td>
                                                          </tr>
                                                            <tr>
                                                              <td>
                                                                   <asp:Label ID="Label143" runat="server"  Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Flete Terrestre:"
                                                               ></asp:Label>
                                                                   <asp:TextBox ID="txt_fleteTerresProv" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                             Style="text-align: right"    Width="100px"></asp:TextBox>
                                                                  </td>
                                                                  <td>
                                                                       <asp:Label ID="Label144" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Flete Terrestre:"
                                                                ></asp:Label>
                                                                  <asp:TextBox ID="txt_fleteTerresReal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Style="text-align: right" Width="100px"></asp:TextBox>
                                                              </td>
                                                          </tr>
                                                           <tr>
                                                              <td>
                                                                 &nbsp;
                                                                  </td>
                                                                  <td>
                                                                       &nbsp;
                                                              </td>
                                                          </tr>
                                                           <tr>
                                                              <td> <asp:Label ID="Label148" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" ForeColor="Black" Style="text-align: right" Width="100px" Text="Cerrado:"
                                                                ></asp:Label>
                                                                   <asp:CheckBox ID="chk_Cerrado_DespaDestBrasil" ForeColor="Black" runat="server" AutoPostBack="true" />
                                                                  </td>
                                                                  <td style="align-items:flex-start">                                                                                                                                                                                                                                                                                                                                  
                                                               <asp:Button ID="btn_GuardarGastDespDestBrasil" OnClick="btn_GuardarGastDespDestBrasil_Click" runat="server" BackColor="#1C5AB6"
                                                                   BorderColor="#999999" CssClass="botonsio" Font-Bold="True" Font-Names="Arial"
                                                                   Font-Size="8pt" ForeColor="White" 
                                                                   OnClientClick="return confirm('Desea Enviar El Formulario')" Text="Guardar"
                                                                   Width="70px"/>
                                                              </td>
                                                          </tr>
                                                      </table>
                                                  </asp:Panel>
                                              </Content>
                                    </asp:AccordionPane>

                               









                                      <asp:AccordionPane ID="Acc_DetalleDespaNacional" runat="server" 
                                        ContentCssClass="accordionContent" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected">
                                        <Header>
                                            <asp:Label ID="Label71" runat="server" Text="Detalle Despacho Nacional"></asp:Label>
                                        </Header>
                                        <Content>
                                            <table style="width: 895px;">
                                              <tr>
                                                    <td>&nbsp;</td>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="Label72" runat="server" Font-Bold="False" Font-Names="Arial"
                                                            Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Orden:"
                                                            Width="120px"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left" class="style107">
                                                        <asp:TextBox ID="txt_OrdenNal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Style="text-transform: uppercase" Width="100px"></asp:TextBox>
                                                      &nbsp;   &nbsp;  &nbsp;&nbsp;   &nbsp;  &nbsp;&nbsp;  
                                                      &nbsp;  &nbsp;&nbsp;   &nbsp;  &nbsp;   &nbsp;   &nbsp; 
                                                      &nbsp;   &nbsp;  &nbsp;&nbsp; &nbsp;                                                      
                                           <asp:Label ID="Label80" runat="server" Font-Bold="False" Font-Names="Arial"
                                                            Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="No_Factura:"
                                                            Width="120px"></asp:Label>
                                                        <asp:TextBox ID="txt_facturaNal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                             Style="text-align: right" Width="100px"></asp:TextBox>                                                                                                                                
                                                      &nbsp;  &nbsp;&nbsp;   &nbsp;  &nbsp;&nbsp;  &nbsp;
                                         <asp:Label ID="Label79" runat="server" Font-Bold="False" Font-Names="Arial"
                                             Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                             Text="Empresa_Transporta:" Width="100px" Visible="false"></asp:Label>
                                                        <asp:TextBox ID="txt_EmpresaTrasnpNal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Style="text-align: left" Width="100px" Visible="false"></asp:TextBox>                                                                                  
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td>&nbsp;</td>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="Label77" runat="server" Font-Bold="False" Font-Names="Arial"
                                                            Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Cliente:"
                                                            Width="120px"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left" class="style107">
                                                        <asp:TextBox ID="txt_ClienteNal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Style="text-transform: uppercase" Width="200px"></asp:TextBox>                                                      
                                           <asp:Label ID="Label78" runat="server" Font-Bold="False" Font-Names="Arial"
                                               Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                               Text="Destino:" Width="120px"></asp:Label>
                                                        <asp:TextBox ID="txt_CiudadDestinoNal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Style="text-align: left" Width="100px" ></asp:TextBox> 
                                                            &nbsp;  &nbsp;&nbsp;                                                   
                                         <asp:Label ID="Label59" runat="server" Font-Bold="False" Font-Names="Arial"
                                             Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                             Text="Fecha_Despacho:" Width="115px"></asp:Label>
                                                               &nbsp;
                                                        <asp:TextBox ID="txt_fechaDespacho" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Style="text-align: left" Width="100px" ReadOnly="True"></asp:TextBox>                                            
                                                    </td>
                                                </tr>
                                                       <tr>
                                                    <td>&nbsp;</td>
                                                           <td style="text-align: right;">
                                                               <asp:Label ID="Label81" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                   Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                                   Text="Fecha Entrega:" Width="120px"></asp:Label>
                                                           </td>
                                                           <td style="text-align: left" class="style107">
                                                               <asp:TextBox ID="txt_FechaEntregaNal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                   Style="text-align: left" Width="100px" AutoPostBack="true" OnTextChanged="txt_FechaEntregaNal_TextChanged"></asp:TextBox>
                                                               <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                                                                   Format="dd/MM/yyyy" TargetControlID="txt_FechaEntregaNal">
                                                               </asp:CalendarExtender>
                                                               &nbsp;   &nbsp;  &nbsp;&nbsp;   &nbsp;  &nbsp;&nbsp;  
                                                      &nbsp;  &nbsp;&nbsp;   &nbsp;  &nbsp;   &nbsp;   &nbsp; 
                                                      &nbsp;   &nbsp;  &nbsp;&nbsp; &nbsp;  
                                           <asp:Label ID="Label82" runat="server" Font-Bold="False" Font-Names="Arial"
                                               Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                               Text="No_Guia:" Width="120px"></asp:Label>
                                                               <asp:TextBox ID="txt_NoGuiaNal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                   Style="text-align: right" Width="100px"></asp:TextBox>
                                                               &nbsp;  &nbsp;&nbsp;                                                   
                                         <asp:Label ID="Label83" runat="server" Font-Bold="False" Font-Names="Arial"
                                             Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                             Text="Responsable_Transporte:" Width="115px"></asp:Label>
                                                               &nbsp;
                                                        <asp:TextBox ID="txt_RespTranspNal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Style="text-align: left" Width="100px"></asp:TextBox>
                                                           </td>
                                                </tr>
                                                           <tr>
                                                    <td>&nbsp;</td>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="Label84" runat="server" Font-Bold="False" Font-Names="Arial"
                                                            Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Valor EXW:"
                                                            Width="120px"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left" class="style107">
                                                        <asp:TextBox ID="txt_ValorExwNal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Style="text-align: right" Width="100px"></asp:TextBox>
                                                           &nbsp;   &nbsp;  &nbsp;&nbsp;   &nbsp;  &nbsp;&nbsp;  
                                                      &nbsp;  &nbsp;&nbsp;   &nbsp;  &nbsp;   &nbsp;   &nbsp; 
                                                      &nbsp;   &nbsp;  &nbsp;&nbsp; &nbsp;  
                                           <asp:Label ID="Label85" runat="server" Font-Bold="False" Font-Names="Arial"
                                               Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                               Text="Flete Cotizado:" Width="120px"></asp:Label>
                                                        <asp:TextBox ID="txt_FleteCotiNal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Style="text-align: right" Width="100px" ></asp:TextBox>                                                              
                                                      &nbsp;  &nbsp;&nbsp;  
                                         <asp:Label ID="Label86" runat="server" Font-Bold="False" Font-Names="Arial"
                                             Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                             Text="Flete_Real:" Width="121px"></asp:Label>
                                                        <asp:TextBox ID="txt_FleteRealNal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Style="text-align: right" Width="100px" AutoPostBack="true" OnTextChanged="txt_FleteRealNal_TextChanged"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                               <tr>
                                                    <td>&nbsp;</td>
                                                    <td style="text-align: right;">
                                                        <asp:Label ID="Label87" runat="server" Font-Bold="False" Font-Names="Arial"
                                                            Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Relación Flete/Valor:"
                                                            Width="120px"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left" class="style107">
                                                        <asp:TextBox ID="txt_RelFleTValorNal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Style="text-align: right" Width="100px"></asp:TextBox>
                                                         &nbsp;   &nbsp;  &nbsp;&nbsp;   &nbsp;  &nbsp;&nbsp;  
                                                      &nbsp;  &nbsp;&nbsp;   &nbsp;  &nbsp;   &nbsp;   &nbsp; 
                                                      &nbsp;   &nbsp;  &nbsp;&nbsp; &nbsp;  
                                           <asp:Label ID="Label88" runat="server" Font-Bold="False" Font-Names="Arial"
                                               Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                               Text="Peso(kg):" Width="120px"></asp:Label>
                                                        <asp:TextBox ID="txt_PesoNal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Style="text-align: right" Width="100px" ></asp:TextBox>
                                                        &nbsp;   &nbsp;  &nbsp; 
                                         <asp:Label ID="Label89" runat="server" Font-Bold="False" Font-Names="Arial"
                                             Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                             Text="Stan By:" Width="120px"></asp:Label>
                                                        <asp:TextBox ID="txt_StanByNal" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Style="text-align: right" Width="100px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td style="text-align: right;">
                                                                <asp:Label ID="Label90" runat="server" Font-Bold="False" Font-Names="Arial"
                                               Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                               Text="Indicador_Dias:" Width="120px"></asp:Label>
                                                    </td>
                                                    <td style="text-align: left" class="style107">
                                                      <asp:TextBox ID="txt_Indicador" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Style="text-align: right" Width="50px" AutoPostBack="true" OnTextChanged="txt_Indicador_TextChanged"></asp:TextBox>
                                                         &nbsp;   &nbsp;  &nbsp;&nbsp;   &nbsp;  &nbsp;&nbsp;  
                                                      &nbsp;  &nbsp;&nbsp;   &nbsp;  &nbsp;   &nbsp;   &nbsp; 
                                                      &nbsp;   &nbsp;  &nbsp;&nbsp; &nbsp;     &nbsp;   &nbsp;  &nbsp; 
                                                           &nbsp;   &nbsp;  &nbsp;    &nbsp;  &nbsp;  
                                       <asp:Label ID="Label91" runat="server" Font-Bold="False" Font-Names="Arial"
                                             Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                             Text="Cumple Entrega:" Width="120px"></asp:Label>
                                   <asp:CheckBox ID="chk_CumpleEntregaNal" runat="server" />
                                                      &nbsp;   &nbsp;  &nbsp;&nbsp;   &nbsp;  &nbsp;&nbsp;  
                                                      &nbsp;  &nbsp;&nbsp;   &nbsp;  &nbsp;   &nbsp;   &nbsp; 
                                                      &nbsp;   &nbsp;  &nbsp;&nbsp; &nbsp;     &nbsp;   &nbsp;  &nbsp; 
                                                           &nbsp;   &nbsp;  &nbsp;    &nbsp;  &nbsp;     
                                                          &nbsp;  &nbsp;    &nbsp;  &nbsp;      &nbsp;  &nbsp;    &nbsp;  &nbsp;    
                                                         &nbsp;  &nbsp;    &nbsp;  &nbsp;      &nbsp;    &nbsp;                                               
                               <asp:Button ID="btn_GuardarDetalleDespaNal" runat="server" BackColor="#1C5AB6"
                                                                   BorderColor="#999999" CssClass="botonsio" Font-Bold="True" Font-Names="Arial"
                                                                   Font-Size="8pt" ForeColor="White" 
                                                                   OnClientClick="return confirm('Desea Enviar El Formulario')" Text="Guardar"
                                                                   Width="70px" OnClick="btn_GuardarDetalleDespaNal_Click"/>                                                     
                                                    </td>                                                                  
                                                </tr>                                                                                                           
                                                </table>                                      
                                        </Content>
                                    </asp:AccordionPane>
                                    <asp:AccordionPane ID="Acc_Observacion" runat="server"
                                        ContentCssClass="accordionContent" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected">
                                        <Header>
                                            <asp:Label ID="lbl_Acc_Observacion" runat="server" Text="Observaciones/Despacho"></asp:Label>
                                        </Header>
                                        <Content>
                                            <table width="500px">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label62" runat="server" Font-Bold="False" Font-Names="Arial"
                                                            Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                                            Text="Observaciones:" Width="100px"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txt_Observacion" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Style="text-align:left" Width="600px" Height="50px" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btn_Guardarobserva" runat="server" BackColor="#1C5AB6"
                                                            BorderColor="#999999" CssClass="botonsio" Font-Bold="True" Font-Names="Arial"
                                                            Font-Size="8pt" ForeColor="White"
                                                            OnClientClick="return confirm('Desea Enviar El Formulario')" Text="Agregar"
                                                            Width="70px" OnClick="btn_Guardarobserva_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <asp:GridView ID="grid_Observaciones" runat="server" BackColor="White"
                                                BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" Width="400px" AutoGenerateColumns="False"
                                                Font-Size="8pt" Font-Names="arial" Height="16px" DataKeyNames="DesObs_Id"
                                                AllowSorting="True" ShowHeaderWhenEmpty="true" ShowHeader="true" OnRowCommand="grid_Observaciones_RowCommand"
                                                CssClass="auto-style2"  OnRowDeleting="grid_Observaciones_RowDeleting">
                                                <AlternatingRowStyle BackColor="Gainsboro" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="IdObserva" HeaderStyle-Width="30px" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label8" Width="40px" runat="server" Text='<%# Bind("DesObs_Id") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Observacion" HeaderStyle-Width="30px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label8" Width="400px" runat="server" Text='<%# Bind("Observacion") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Usuario_Crea" HeaderStyle-Width="30px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label8" Width="80px" runat="server" Text='<%# Bind("Usu_Crea") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fecha" HeaderStyle-Width="30px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label8" Width="50px" runat="server" Text='<%# Bind("Fecha","{0:d}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Orden" HeaderStyle-Width="30px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label8" Width="80px" runat="server" Text='<%# Bind("Orden") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Estatus" HeaderStyle-Width="30px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label8" Width="80px" runat="server" Text='<%# Bind("Estatus") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:CommandField HeaderStyle-Width="50px" ShowDeleteButton="true">
                                                        <HeaderStyle Width="50px"></HeaderStyle>
                                                    </asp:CommandField>                                                 
                                                </Columns>
                                                <EditRowStyle HorizontalAlign="Right" />
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White"
                                                    HorizontalAlign="Center" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EEEEEE" ForeColor="Black" HorizontalAlign="Left" />
                                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#000065" />
                                            </asp:GridView>
                                            <br />
                                        </Content>
                                    </asp:AccordionPane>
                                    <asp:AccordionPane ID="AccEvento" runat="server"
                                        ContentCssClass="accordionContent" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected">
                                        <Header>
                                            <asp:Label ID="Label60" runat="server" Text="Eventos"></asp:Label>
                                        </Header>
                                        <Content>
                                        </Content>
                                    </asp:AccordionPane>
                                    <asp:AccordionPane ID="Acc_Datos_Generales" runat="server"
                                        ContentCssClass="accordionContent" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected">
                                        <Header>
                                            <asp:Label ID="Label61" runat="server" Text="Archivos Adjuntos"></asp:Label>
                                        </Header>
                                        <Content>
                                            <asp:Panel ID="Panel1" runat="server" GroupingText=".">
                                                <table style="width: 738px">

                                                    <tr>
                                                              
                                                        <td style="text-align: left">                                                                    
                                                            <asp:Label ID="lblTipoAnexo" runat="server" Font-Bold="False" Font-Names="Arial"
                                                                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Text="Tipo Anexo" Width="65px"></asp:Label>
                                                            <asp:DropDownList ID="cboTipoAnexo" runat="server" Width="140px" Font-Names="Arial"
                                                                Font-Size="8pt" >
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <asp:FileUpload ID="FDocument" runat="server" />
                                                        </td>
                                                        <td class="style138">
                                                            <asp:Label ID="lblArchivo" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Guia / Listado / Documentación"
                                                                Width="374px" Font-Italic="True" ForeColor="#0000CC" Visible="True"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <asp:Button ID="btnSubirArchivos" runat="server" Font-Names="Arial" ForeColor="White"
                                                                Font-Size="8pt" Text="Subir" Width="70px" BackColor="#1C5AB6"
                                                                Font-Bold="True" Visible="True" OnClick="btnSubirArchivos_Click" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCancelar" runat="server" BackColor="#1C5AB6" ForeColor="White"
                            Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Text="Cancelar" Width="70px"
                            Visible="True" OnClick="btnCancelar_Click" />
                                                        </td>
                                                        <td>&nbsp;
                                                            <asp:Label ID="lbl_IdMedTrasnp" runat="server" Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_IdPais" runat="server" Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_rutaArchivo" runat="server" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: left">
                                                            <asp:GridView ID="grid_Document_Despacho" runat="server" AutoGenerateColumns="False"
                                                                BackColor="White" OnRowCommand="grid_Document_Despacho_RowCommand"
                                                                CellPadding="1" CellSpacing="4" ForeColor="#333333"
                                                                GridLines="None" Font-Names="Arial" Font-Size="8pt" AllowSorting="false">
                                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Nombre_Archivo" ShowHeader="False">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="LinkButton1" Width="300px" runat="server"
                                                                                CausesValidation="False"
                                                                                CommandArgument='<%# Eval("Nombre_Archivo") %>' PostBackUrl="~/SeguimientoDespachos.aspx"
                                                                                CommandName="Download" Text='<%# Eval("Nombre_Archivo") %>'></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EditRowStyle BackColor="#999999" />
                                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" />
                                                                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White"
                                                                    HorizontalAlign="Center" />
                                                                <PagerStyle BackColor="#284775" HorizontalAlign="Center" />
                                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                            </asp:GridView>
                                                        </td>
                                                        <td style="text-align: left">
                                                            <asp:GridView ID="Grid_DocAnexo" runat="server" AutoGenerateColumns="False"
                                                                BackColor="White" AllowSorting="false"
                                                                CellPadding="1" CellSpacing="2" ForeColor="#333333"
                                                                GridLines="None" Font-Names="Arial" Font-Size="8pt">
                                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Usuario" ShowHeader="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label Width="100px" ID="lbl_UsuDoc" runat="server" Text='<%#Bind("UsuCrea") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Fecha" ShowHeader="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label Width="70px" ID="lbl_Fecha" runat="server" Text='<%#Bind("FechaCrea","{0:d}") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                          <asp:TemplateField HeaderText="Tipo Anexo" ShowHeader="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label Width="100px" ID="lbl_TipoAnexo" runat="server" Text='<%#Bind("TipoAnexo") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>                                                           
                                                                </Columns>
                                                                <EditRowStyle BackColor="#999999" />
                                                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" />
                                                                <HeaderStyle BackColor="Gray" Font-Bold="True" ForeColor="White"
                                                                    HorizontalAlign="Center" />
                                                                <PagerStyle BackColor="#284775" HorizontalAlign="Center" />
                                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style123">&nbsp;
                                                        </td>
                                                        <td class="style130">&nbsp;
                                                        </td>
                                                        <td class="style104">&nbsp;
                                                            <asp:Label ID="lbl_FechaDespacho" runat="server" Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_ciu_id" runat="server" Visible="false"></asp:Label>
                                                            <asp:Label ID="lbl_cli_id" runat="server" Visible="false"></asp:Label>
                                                        </td>
                                                        <td>&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </Content>
                                    </asp:AccordionPane>
                                </Panes>
                            </asp:Accordion>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
