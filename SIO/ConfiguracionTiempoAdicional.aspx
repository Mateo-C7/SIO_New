<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="ConfiguracionTiempoAdicional.aspx.cs" Inherits="SIO.ConfiguracionTiempoAdicional" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

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
                        regexp = /[0-9]{1}$/
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

            function confirmarEliminar() {

                if (confirm("¿Desea eliminar este registro?")) {

                    return true;
                }
                else {
                    return false;
                }
            }      
    </script>
 
       

    <style type="text/css">
        .auto-style2 {
            margin-top: 0px;
        }
        .auto-style7 {
            width: 32px;
        }
        .auto-style9 {
            margin-left: 0px;
        }
        .auto-style15 {
            width: 830px;
        }
        .auto-style16 {
            width: 71px;
        }
        .style2
        {
            width: 266px;
        }
        .style3
        {
            width: 112px;
        }
        .style4
        {
            width: 23px;
        }
        </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel UpdateMode="Conditional" ID="updpnlmaestro" runat="server">
        <ContentTemplate>
            <table class="fondoazul" width="100%">
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="12pt" ForeColor="White" Text="Tiempos Adicionales De Fabricación"></asp:Label>
                    </td>
                </tr>
            </table>  
            <asp:Panel ID="pnlCamposDetalle" runat="server"
                Font-Names="Arial" Font-Size="8pt" ForeColor="Black" GroupingText=""
                Width="710px" Height="134px" CssClass="auto-style2" BorderStyle="Groove" BorderColor="#3366cc">
                <table class="style15">
                    <tr>
                        <td class="style4">&nbsp;&nbsp;</td>
                        <td align="right" class="style3">
                            <asp:Label ID="Label4" runat="server" Text="Planta:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_PlantaUsuario" runat="server" Width="150px"></asp:TextBox>
                        </td>
                        <td class="auto-style7"></td>

                        <td style="text-align: right">&nbsp;</td>
                        <td align="right">
                            <asp:Label runat="server" Text="Tipo Pieza:" Width="63px" CssClass="auto-style9"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cbo_TipoPieza" AutoPostBack="true" runat="server" Width="150px"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td class="style4">&nbsp;&nbsp;</td>
                        <td align="right" class="style3">
                            <asp:Label ID="Label5" runat="server" Text="Codigo:"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_Codigo" runat="server" MaxLength="50" Width="150px" Style="text-transform: uppercase"></asp:TextBox>
                        </td>
                        <td class="auto-style7"></td>

                        <td style="text-align: right">&nbsp;</td>
                        <td align="right">
                            <asp:Label runat="server" Text="Descripción:" Width="63px" CssClass="auto-style9"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_Descripcion" runat="server" Width="200px" MaxLength="250" Style="text-transform: uppercase"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style4">&nbsp;&nbsp;</td>
                        <td align="right" class="style3">
                            <asp:Label ID="Label6" runat="server" Text="Tiempo Metalmecanica:"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txt_TiempoMetalmeca" runat="server" Width="50px" MaxLength="5"></asp:TextBox>
                        </td>
                        <td class="auto-style7"></td>

                        <td style="text-align: right">&nbsp;</td>
                        <td align="right">&nbsp;</td>
                        <td style="text-align: left">
                            <asp:Button ID="Btn_Guardar" runat="server" Text="Guardar"
                                BackColor="#1C5AB6" ForeColor="White"
                                BorderColor="#999999" Font-Bold="True" Font-Names="Arial"
                                OnClientClick="return confirm('¿Desea Enviar los Datos?')"
                                Font-Size="8pt" OnClick="Btn_Guardar_Click" />
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style4">&nbsp;&nbsp;</td>
                        <td align="right" class="style3">
                            <asp:Label ID="Label1" runat="server" Text="Tiempo P.Terminado:"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txt_TiempoPeter" runat="server" Width="50px" MaxLength="5"></asp:TextBox>
                        </td>
                        <td class="auto-style7"></td>

                        <td style="text-align: right">&nbsp;</td>
                        <td align="right">&nbsp;</td>
                        <td style="text-align: left">
                            <asp:Button ID="btn_Cancelar" runat="server" Text="Cancelar"
                                BackColor="#1C5AB6" ForeColor="White" OnClick="btn_Cancelar_Click"
                                BorderColor="#999999" Font-Bold="True" Font-Names="Arial"
                                OnClientClick="return confirm('¿Operación cancelada?')"
                                Font-Size="8pt" />
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class="style4">&nbsp;</td>
                        <td align="right" class="style3">
                            <asp:Label runat="server" Text="Tiempo Soldadura:" Width="91px" CssClass="auto-style9"></asp:Label>
                        </td>
                        <td style="text-align: left">
                            <asp:TextBox ID="txt_TiempoSolda" runat="server" Width="50px" MaxLength="5"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

              <asp:GridView ID="Grid_Detalle_ConfgTiempos" runat="server" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="2" Width="200px" AllowPaging="True" DataKeyNames="Tmde_Id"
                AutoGenerateColumns="False" Font-Size="8pt" Font-Names="arial" Height="16px"  PageSize="34" 
                  OnPageIndexChanging="Grid_Detalle_ConfgTiempos_PageIndexChanging" OnRowDeleting="Grid_Detalle_ConfgTiempos_RowDeleting"
                   OnRowCommand="Grid_Detalle_ConfgTiempos_RowCommand">
                <%-- <AlternatingRowStyle BackColor="Gainsboro" />--%>
                <Columns>                 
                  <asp:CommandField HeaderText="Actualizar"   HeaderStyle-Width="50px" ShowSelectButton="True">
                        <HeaderStyle Width="50px"></HeaderStyle>
                    </asp:CommandField>                   
                    <asp:TemplateField HeaderText="Codigo" HeaderStyle-HorizontalAlign="center"  HeaderStyle-Width="200px">
                        <EditItemTemplate>
                              <asp:TextBox Width="200px"  AutoPostBack="false" ID="txt_Tmde_Id" runat="server" Text='<%# Bind("Tmde_Id") %>'></asp:TextBox>                     
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label Width="160px"  MaxLength="3" ID="Label4" runat="server" Text='<%# Bind("Tmde_Id") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="200px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>             
                    <asp:TemplateField HeaderText="Descripción" HeaderStyle-Width="115px">
                        <EditItemTemplate>
                            <asp:TextBox Width="115px"  AutoPostBack="false" ID="Txt_Dscrpcion" runat="server" Text='<%# Bind("Tmde_Dscrpcion") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label Width="205px" ID="Label5" runat="server"  style="text-transform :uppercase" Text='<%# Bind("Tmde_Dscrpcion") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="115px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Metalmecanica" HeaderStyle-Width="70px">
                        <EditItemTemplate>
                            <asp:TextBox Width="70px"  ID="Txt_Metal" MaxLength="4" AutoPostBack="false" runat="server" Text='<%# Bind("Tmde_Metal") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label Width="70px" ID="Label67" runat="server" Text='<%# Bind("Tmde_Metal","{0:0.0}") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="70px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>    
                         <asp:TemplateField HeaderText="Soldadura" HeaderStyle-Width="30px">
                        <EditItemTemplate>
                            <asp:TextBox Width="50px"  ID="Txt_Solda" MaxLength="4" AutoPostBack="false" runat="server" Text='<%# Bind("Tmde_Solda") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label Width="50px" ID="Label65" runat="server" Text='<%# Bind("Tmde_Solda","{0:0.0}") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="50"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>   
                            <asp:TemplateField HeaderText="Pro_Terminado" HeaderStyle-Width="70px">
                        <EditItemTemplate>
                            <asp:TextBox Width="70px"  ID="txt_Pter" MaxLength="4" AutoPostBack="false" runat="server" Text='<%# Bind("Tmde_Pter") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label Width="70px" ID="Label64" runat="server"  Text='<%# Bind("Tmde_Pter","{0:0.0}") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="70px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right"/>
                    </asp:TemplateField>                                                        
                     <asp:TemplateField HeaderText="Eliminar">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinEliminarDetalle" Text="Eliminar"  runat="server" 
                                CommandName="Delete" OnClientClick="return confirmarEliminar();" ForeColor="Black"></asp:LinkButton>
                        </ItemTemplate>
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
