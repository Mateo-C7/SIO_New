<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="MaestroOperaciones.aspx.cs" Inherits="SIO.MaestroOperaciones" %>
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

        .AlineadoDerecha{
                 text-align:right;
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

         .auto-style2 {
             height: 19px;
         }
         .auto-style4 {
             height: 19px;
             width: 134px;

         }
         .auto-style5 {
             width: 101%;
         }
    </style>
    <script type="text/javascript" src="Scripts/jquery.i18n.js"></script>
    <script type="text/javascript" src="Scripts/jquery.i18n.messagestore.js"></script>
    <%--<script type="text/javascript" src="Scripts/jquery.i18n.fallbacks.js"></script>
    <script type="text/javascript" src="Scripts/jquery.i18n.parser.js"></script>
    <script type="text/javascript" src="Scripts/jquery.i18n.emitter.js"></script>--%>
    <script type="text/javascript" src="Scripts/obra.js"></script>

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

            function confirmarEliminarDetalleoperacion() {

                if (confirm("¿Realmente quieres eliminar este registro?")) {

                    return true;
                }
                else {
                    return false;
                }
            }      
    </script>
 

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
              <table class="fondoazul" width="100%">
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="12pt" ForeColor="White" Text="Maestro De Operaciones"></asp:Label>
                    </td>
                </tr>
            </table> 
            <br /><br />
            <asp:Panel ID="Panel1" runat="server" Width="30%" BorderStyle="Inset">
            <table class="auto-style5">
                <tr>
                     <td class="auto-style4"  align="right">
                         <asp:Label ID="Label1" runat="server" Text="Operación:" Font-Bold="True" ForeColor="Black"></asp:Label>
                     </td>
                    <td class="auto-style2">
                        <asp:TextBox ID="TxtDescripcion" runat="server" Width="168px" MaxLength="50"></asp:TextBox>
                        <asp:Label ID="Label11" runat="server" Text="*" Font-Bold="True" ForeColor="RED"></asp:Label>
                    </td>   
                </tr>
              <tr>
                     <td class="auto-style4"  align="right">
                         <asp:Label ID="Label2" runat="server" Text="Unidad:" Font-Bold="True" ForeColor="Black"></asp:Label>
                     </td>
                    <td class="auto-style2">
                        <asp:TextBox ID="TxtUnidad" runat="server" Width="32px" MaxLength="10" text-transform="uppercase"></asp:TextBox>
                        <asp:Label ID="Label10" runat="server" Text="*" Font-Bold="True" ForeColor="RED"></asp:Label>
                    </td>   
                </tr>
                <tr>
                     <td class="auto-style4" align="right">
                         <asp:Label ID="Label3" runat="server" Text="Aplica en:" Font-Bold="True" ForeColor="Black"></asp:Label>
                     </td>
                    <td class="auto-style2">
                        <asp:DropDownList ID="Cbo_Aplica" Width="100" runat="server"></asp:DropDownList>   
                        <asp:Label ID="Label12" runat="server" Text="*" Font-Bold="True" ForeColor="RED"></asp:Label>                     
                    </td>   
                </tr>
                <tr>
                     <td class="auto-style4" align="right">
                         <asp:Label ID="Label4" runat="server" Text="Centro_Costo:" Font-Bold="True" ForeColor="Black"></asp:Label>
                     </td>
                    <td class="auto-style2" style="text-align" >
                        <asp:TextBox ID="TxtCentroCosto" runat="server" Width="32px" MaxLength="6" style="text-align:right" ></asp:TextBox>
                    </td>   
                </tr>
                <tr>
                     <td class="auto-style4" align="right">
                         <asp:Label ID="Label5" runat="server" Text="Horas_x_Unidad:" Font-Bold="True" ForeColor="Black"></asp:Label>
                     </td>
                    <td class="auto-style2" >
                        <asp:TextBox ID="TxtHrsUnid" runat="server" Width="80px" style="text-align:right"></asp:TextBox>
                    </td>   
                </tr>
                <tr>
                     <td class="auto-style4" align="right">
                         <asp:Label ID="Label6" runat="server" Text="Costo_Prom_Alum:" Font-Bold="True" ForeColor="Black"></asp:Label>
                     </td>
                    <td class="auto-style2">
                        <asp:TextBox ID="TxtCostoPromAlum" runat="server" Width="80px" style="text-align:right"></asp:TextBox>
                    </td>   
                </tr>
                <tr>
                     <td class="auto-style4" align="right">
                         <asp:Label ID="Label8" runat="server" Text="Costo_Prom_Bush:" Font-Bold="True" ForeColor="Black"></asp:Label>
                     </td>
                    <td class="auto-style2">
                        <asp:TextBox ID="TxtCostoPromBush" runat="server" Width="80px" style="text-align:right"></asp:TextBox>
                    </td>   
                </tr>
                <tr>
                     <td class="auto-style4" align="right">
                         <asp:Label ID="Label9" runat="server" Text="Costo_Prom_Rem:" Font-Bold="True" ForeColor="Black"></asp:Label>
                     </td>
                    <td class="auto-style2">
                        <asp:TextBox ID="TxtCostoPromRem" runat="server" Width="80px" style="text-align:right"></asp:TextBox>
                    </td>   
                </tr>
                 <tr>
                     <td class="auto-style4" align="right">
                         &nbsp;  &nbsp;
                     </td>
                     <td class="auto-style4" align="right">
                         &nbsp;  &nbsp;
                     </td>  
                </tr>               
            </table>
                <table>
                     <tr>
                     <td class="auto-style4" align="right">
                         <asp:Button ID="Btn_Nuevo"  runat="server" Text="Nuevo" BackColor="#0066FF" ForeColor="White" Width="88px" CssClass="fondoazul" OnClick="Btn_Nuevo_Click" />
                                               
                     </td>
                     <td>
                       &nbsp;  &nbsp; &nbsp; &nbsp;
                     </td>  
                          <td>
                       &nbsp;  &nbsp; &nbsp; &nbsp;
                     </td>  
                     <td class="auto-style4" >
                       <asp:Button ID="BtnGuardar"  runat="server" Text="Guardar" BackColor="#0066FF" ForeColor="White" Width="88px" CssClass="fondoazul" OnClick="BtnGuardar_Click" />   
                     </td> 
                </tr>
                </table>
                <br />                                            
            </asp:Panel>        
            
              <asp:GridView ID="Grid_DetalleOperaciones" runat="server" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="2" Width="226px" AllowPaging="True" DataKeyNames="Maestro_Operacion_Id" 
                AutoGenerateColumns="False" Font-Size="8pt" Font-Names="arial" Height="16px"  PageSize="15"
                OnRowCommand="Grid_DetalleOperaciones_RowCommand"           OnPageIndexChanging="Grid_DetalleOperaciones_PageIndexChanging">
                <%-- <AlternatingRowStyle BackColor="Gainsboro" />--%>
                  <Columns>
                      <asp:TemplateField HeaderText="Id_Operacion" Visible="False">
                          <EditItemTemplate>
                              <asp:TextBox ID="Txt_IdOperacion" AutoPostBack="false" runat="server" Text='<%# Bind("Maestro_Operacion_Id") %>' BorderStyle="None" ReadOnly="True"></asp:TextBox>
                          </EditItemTemplate>
                          <ItemTemplate>
                              <asp:Label ID="Label8" runat="server" Text='<%# Bind("Maestro_Operacion_Id") %>'></asp:Label>
                          </ItemTemplate>
                      </asp:TemplateField>  
                           <asp:CommandField HeaderText="Seleccionar"   HeaderStyle-Width="50px" ShowSelectButton="True">
                        <HeaderStyle Width="50px"></HeaderStyle>
                    </asp:CommandField>                           
                      <asp:TemplateField HeaderText=" Proceso" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                          <EditItemTemplate>
                              <asp:TextBox Width="120px" AutoPostBack="false" ID="Txt_Descripcion"  runat="server" Text='<%# Bind("Descripcion") %>'></asp:TextBox>
                          </EditItemTemplate>
                          <ItemTemplate>
                              <asp:Label Width="120px" MaxLength="50" ID="Label4" runat="server" Text='<%# Bind("Descripcion") %>'></asp:Label>
                          </ItemTemplate>
                          <ItemStyle HorizontalAlign="Left" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText=" Unidad" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                          <EditItemTemplate>
                              <asp:TextBox Width="40px" AutoPostBack="false" ID="Txt_Unidad" runat="server" Text='<%# Bind("Unidad") %>'></asp:TextBox>
                          </EditItemTemplate>
                          <ItemTemplate>
                              <asp:Label Width="40px" MaxLength="50" ID="Label4" runat="server" Text='<%# Bind("Unidad") %>'></asp:Label>
                          </ItemTemplate>
                          <ItemStyle HorizontalAlign="Center" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText=" Aplica En" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-Width="60px">
                          <EditItemTemplate>
                              <asp:DropDownList ID="Cbo_Aplica" runat="server"></asp:DropDownList>
                          </EditItemTemplate>
                          <ItemTemplate>
                              <asp:Label Width="100px" MaxLength="50" ID="Label4" runat="server" Text='<%# Bind("Aplica_En") %>'></asp:Label>
                          </ItemTemplate>                          
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText=" Centro_Costo" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="60px">
                          <EditItemTemplate>
                              <asp:TextBox Width="40px" AutoPostBack="false" ID="Txt_CentroCosto" runat="server" Text='<%# Bind("Cen_Cos_Id") %>'></asp:TextBox>
                          </EditItemTemplate>
                          <ItemTemplate>
                              <asp:Label Width="40px" MaxLength="10" ID="Label4" runat="server" Text='<%# Bind("Cen_Cos_Id") %>'></asp:Label>
                          </ItemTemplate>
                          <ItemStyle HorizontalAlign="Right" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Horas_x_Unidad" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="90px">
                          <EditItemTemplate>
                              <asp:TextBox Width="50px" AutoPostBack="false" ID="Txt_CentroCosto" runat="server" Text='<%# Bind("HorasxUnidad") %>'></asp:TextBox>
                          </EditItemTemplate>
                          <ItemTemplate>
                              <asp:Label Width="50px" MaxLength="10" ID="Label4" runat="server" Text='<%# Bind("HorasxUnidad") %>'></asp:Label>
                          </ItemTemplate>
                         <ItemStyle HorizontalAlign="Right" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Costo_Prom_Alum" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="90px">
                          <EditItemTemplate>
                              <asp:TextBox Width="50px" AutoPostBack="false" ID="Txt_CostoPromAlum" runat="server" Text='<%# Bind("Costo_Prom_Alum_Erp") %>'></asp:TextBox>
                          </EditItemTemplate>
                          <ItemTemplate>
                              <asp:Label Width="50px" MaxLength="10" ID="Label4" runat="server" Text='<%# Bind("Costo_Prom_Alum_Erp") %>'></asp:Label>
                          </ItemTemplate>
                       <ItemStyle HorizontalAlign="Right" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Costo_Prom_Bush" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-Width="90px">
                          <EditItemTemplate>
                              <asp:TextBox Width="50px" AutoPostBack="false" ID="Txt_CostoPromBush" runat="server" Text='<%# Bind("Costo_Prom_Bush") %>'></asp:TextBox>
                          </EditItemTemplate>
                          <ItemTemplate>
                              <asp:Label Width="50px" MaxLength="10" ID="Label4" runat="server" Text='<%# Bind("Costo_Prom_Bush") %>'></asp:Label>
                          </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" />
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Costo_Prom_Rem" HeaderStyle-HorizontalAlign="Center"  HeaderStyle-Width="90px">
                          <EditItemTemplate>
                              <asp:TextBox Width="50px" AutoPostBack="false" ID="Txt_CostoPromRem" runat="server" Text='<%# Bind("Costo_Prom_Rem") %>'></asp:TextBox>
                          </EditItemTemplate>
                          <ItemTemplate>
                              <asp:Label Width="50px" MaxLength="10" ID="Label4" runat="server" Text='<%# Bind("Costo_Prom_Rem") %>'></asp:Label>
                          </ItemTemplate>
                          <ItemStyle HorizontalAlign="Right" />
                      </asp:TemplateField>              
                  </Columns>
                <EditRowStyle HorizontalAlign="Right" />
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






            </ContentTemplate>
        </asp:UpdatePanel>

</asp:Content>
