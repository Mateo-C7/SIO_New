<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="ItinerariosLogistica.aspx.cs" Inherits="SIO.ItinerariosLogistica" %>
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

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <table class="fondoazul" width="930px">
        <tr>
            <td>
                <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="12pt"
                    ForeColor="White" Text="ITINERARIOS LOGISTICA"></asp:Label>
            </td>
            <td align="right">
                <asp:Label ID="lbl_usuario" Text="Usuario" runat="server" Visible="true"></asp:Label>
                &nbsp;&nbsp;
                <asp:Label ID="txtRepresentante" runat="server" Font-Bold="True" Font-Names="Arial"
                 Font-Size="8pt" ForeColor="White" Style="border-bottom: black 1px solid; text-align: left;" Width="160px"></asp:Label>
            </td>
        </tr>
    </table>  
        <div id="panel1" runat="server">
    <table width="925px">
        <tr>
            <td>    
    <table>
        <tr>
            <td colspan="1" class="auto-style1" align="right">
                <asp:Label ID="Label69" runat="server" Text="Pais Destino:" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="cboPaisDestino" runat="server" Width="150px"></asp:DropDownList>
            </td>
            <td colspan="1" class="auto-style1" style="text-align: right">
                <asp:Label ID="Label70" runat="server" Text="Puerto Zarpe:" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="cboPuertoZarpe" runat="server" Width="150px"></asp:DropDownList>
                  <asp:LinkButton ID="lkZarpe" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" ToolTip="Agregar Puerto De Zarpe" Width="20px" OnClick="lkZarpe_Click">>></asp:LinkButton>
            </td>
            <td colspan="1" class="auto-style1" style="text-align: right">
                <asp:Label ID="Label1" runat="server" Text="Puerto Descargue:" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="cboDescargue" runat="server" Width="150px"></asp:DropDownList>
                       <asp:LinkButton ID="lkDescargue" runat="server" Font-Bold="True" Font-Names="Arial"
                                        Font-Size="8pt" ForeColor="Black" ToolTip="Agregar Puerto De Descargue" OnClick="lkDescargue_Click">>></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="1" class="auto-style1" align="right">
                <asp:Label ID="Label" runat="server" Text="Tiempo De Transito:" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtTiempo" Width="30px" runat="server"  Style="text-align: right"></asp:TextBox>
            </td>
            <td colspan="1" class="auto-style1" style="text-align: right">
                <asp:Label ID="Label4" runat="server" Text="Naviera:" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="cboNaviera" runat="server" Width="150px"></asp:DropDownList>
                 <asp:LinkButton ID="lkNaviera" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" OnClick="lkNaviera_Click">>></asp:LinkButton></td>
            </td>
            <td colspan="1" class="auto-style1" align="right">
                <asp:Label ID="Label5" runat="server" Text="Buque:" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtBuque" runat="server" Font-Names="Arial" Font-Size="8pt"
                    Style="text-align: right" Width="140px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="1" class="auto-style1" align="right">
                <asp:Label ID="Label3" runat="server" Text="20 ST USD:" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt20ST" runat="server" Font-Names="Arial" Font-Size="8pt"
                    Style="text-align: right" Width="100px"></asp:TextBox>
            </td>
            <td colspan="1" class="auto-style4" align="right">
                <asp:Label ID="Label8" runat="server" Text="40 HC USD:" Width="85px" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txt40HC" runat="server" Font-Names="Arial" Font-Size="8pt"
                    Style="text-align: right" Width="100px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label ID="Label23" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Fecha De Cargue:"
                    Width="100px"></asp:Label>
            </td>
            <td style="text-align: left" class="style107">
                <asp:TextBox ID="txtFecCargue" runat="server" Font-Names="Arial"
                    Font-Size="8pt" Style="margin-left: 0px" 
                    TabIndex="9" Width="70px" OnTextChanged="txtFecCargue_TextChanged"  AutoPostBack ="true" AutoCompleteType="None"></asp:TextBox>     
                <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                    Format="dd/MM/yyyy"   TargetControlID="txtFecCargue" >
                </asp:CalendarExtender>           
            </td>
            <td style="text-align: right;">
                <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Estimado Zarpe:"
                    Width="100px"></asp:Label>
            </td>
            <td style="text-align: left" class="style107">
                <asp:TextBox ID="txtEstZarpe" runat="server" Font-Names="Arial"
                    Font-Size="8pt" Style="margin-left: 0px" 
                    TabIndex="9" Width="70px" AutoPostBack="true" OnTextChanged="txtEstZarpe_TextChanged" AutoCompleteType="Disabled" ></asp:TextBox>
                <asp:CalendarExtender ID="txtEstZarpe_Calendar" runat="server"
                    Format="dd/MM/yyyy" Animated="true"  TargetControlID="txtEstZarpe">
                </asp:CalendarExtender>
            </td>
            <td style="text-align: right;">
                <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Estimado Arribo:"
                    Width="100px"></asp:Label>
            </td>
            <td style="text-align: left" class="style107">
                <asp:TextBox ID="txtArribo" runat="server" Font-Names="Arial"
                    Font-Size="8pt" Style="margin-left: 0px"
                    TabIndex="9" Width="70px" OnTextChanged="txtArribo_TextChanged" AutoPostBack="true" AutoCompleteType="Disabled" ></asp:TextBox>
                <asp:CalendarExtender ID="txtArribo_Calendar" runat="server"
                    Format="dd/MM/yyyy"  Animated="true" TargetControlID="txtArribo">
                </asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td style="text-align: right;">
                <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="Black" Style="text-align: right" Text="Cierre Naviera:"
                    Width="100px"></asp:Label>
            </td>
            <td style="text-align: left" class="style107">
                <asp:TextBox ID="txtCierreNav" runat="server" Font-Names="Arial"
                    Font-Size="8pt" Style="margin-left: 0px" 
                    TabIndex="9" Width="70px" AutoCompleteType="Disabled" ></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender3" runat="server"
                    Format="dd/MM/yyyy" Animated="true" TargetControlID="txtCierreNav">
                </asp:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td class="auto-style2"></td>
            <td colspan="1" class="auto-style4" align="left">
                <asp:Button ID="btnAdicionar" runat="server" BackColor="#1C5AB6"
                    BorderColor="#999999" CssClass="botonsio" Font-Bold="True" Font-Names="Arial"
                    Font-Size="8pt" Visible="False" ToolTip="Adicionar Un Nuevo Registro" ForeColor="White" Text="Adicionar" OnClick="btnAdicionar_Click"
                    Width="70px" /> &nbsp; 
                   <asp:Button ID="btnGuardar" runat="server" Text="Guardar" BackColor="#1C5AB6"
                    BorderColor="#999999" CssClass="botonsio" Font-Bold="True" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="White" OnClick="btnGuardar_Click"
                    Width="70px" /> &nbsp;
                 <asp:Button ID="btn_Nuevo" runat="server" Text="Nuevo" BackColor="#1C5AB6"
                    BorderColor="#999999" CssClass="botonsio" Font-Bold="True" Font-Names="Arial"
                    Font-Size="8pt" ForeColor="White" OnClick="btn_Nuevo_Click"
                    Width="70px" />
            </td>
            <td colspan="1" class="auto-style4" align="center">
             
            </td>
            <td colspan="1" class="auto-style4" align="center">
               
            </td>                
        </tr>   
    </table>                                       
        </tr>
        <tr>
            <td>
                   <rsweb:ReportViewer ID="ReporteVerItinerarios" runat="server" Width="900px" 
                        BackColor="#EBEBEB" BorderColor="#EBEBEB" Height="" ShowToolBar="False"
                        AsyncRendering="True" ShowFindControls="False" ShowPrintButton="False" 
                            ShowPromptAreaButton="False" ShowZoomControl="False">
                        </rsweb:ReportViewer>
            </td>
        </tr>
    </table>
        </div>
          <asp:Accordion ID="Accordion1" runat="server" ContentCssClass="accordionContent"
                                HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                Width="930px" Height="8000px" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="text-align: right">
                                <Panes>
                                    <asp:AccordionPane ID="AcorInfoGeneral" runat="server" ContentCssClass="accordionContent"
                                        Font-Bold="False" Font-Names="Arial" Font-Size="8pt" HeaderCssClass="accordionHeader"
                                        HeaderSelectedCssClass="accordionHeaderSelected" Visible="false">
                                        <Header>
                                            <asp:Label ID="Label10" runat="server"
                                                Text="Maestro Itinerarios"></asp:Label>
                                        </Header>
                                        <Content>
                                             <table style="width: 349px; border-collapse: collapse">
                         <tr>
                             <td colspan="2" style="height: 18px; text-align: right">
                                 <asp:Label ID="lblTitulo" runat="server" BackColor="#3B5998" BorderColor="#3B5998"
                                     Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White" Style="text-align: center"
                                     Width="400px"></asp:Label></td>
                         </tr>
                         <tr>
                             <td style="text-align: right">
                                 <asp:Label ID="lblNombre" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                     Width="100px"></asp:Label></td>
                             <td style="width: 285px">
                                 <asp:TextBox ID="txtDetalle" runat="server" Font-Names="Arial" Font-Size="8pt" Width="220px"></asp:TextBox></td>
                         </tr>
                         <tr>
                             <td>
                             </td>
                             <td style="width: 285px; text-align: right">
                                 <asp:Button ID="btnGuardarMaestro" runat="server" BackColor="#3B5998" BorderColor="#3B5998"
                                     Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White"
                                     Text="Guardar" Width="70px" OnClick="btnGuardarMaestro_Click"/>
                                 <asp:ImageButton ID="ImgVolverCiudad" runat="server" Height="15px" ImageUrl="~/Imagenes/volver.png"
                                   ToolTip="Volver A Itinerarios" Width="15px" OnClick="ImgVolverCiudad_Click" /></td>
                         </tr>
                         <tr>
                             <td colspan="2" style="height: 21px">
                                 <asp:Label ID="lblMSJMaestro" runat="server" BackColor="#E0E0E0" BorderColor="#E0E0E0"
                                     Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Maroon" Visible="False"
                                     Width="400px"></asp:Label></td>
                         </tr>
                     </table>

                                     </Content>
                                    </asp:AccordionPane>
                                    </Panes>
                                    </asp:Accordion>      
    </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

