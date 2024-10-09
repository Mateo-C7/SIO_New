<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="MaestroMonedas.aspx.cs" Inherits="SIO.MaestroMonedas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script>
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

</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
       <table  class="fondoazul" width="100%" >
        <tr>
            <td>           
                <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="12pt" ForeColor="White"   Text="Maestro Monedas - Conversion a Dolares"></asp:Label>
            </td>
        </tr>
    </table>
    
    <asp:UpdatePanel UpdateMode="Conditional" ID="updpnlmaestro" runat="server">
        <ContentTemplate>
                 <asp:Panel ID="pnlCampos"  runat="server"  
                Font-Names="Arial" Font-Size="8pt" ForeColor="Black"  GroupingText="Detalle"
                Width="545px" Height="100px" CssClass="auto-style2" BorderStyle="Groove" BorderColor="#3366cc">

            <table  class="style15">
                <tr>
                    <td align="right">
                        <asp:Label runat="server"  Text="Moneda:" ></asp:Label>
                    </td>
                    <td >                        
                        <asp:DropDownList ID="cbo_Moneda" runat="server"></asp:DropDownList>
                    </td>
                    <td>
                         &nbsp; &nbsp; &nbsp; &nbsp;
                    </td>
                     <td align="right">
                        <asp:Label runat="server" Text="Año:" ></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="cbo_Año" runat="server"></asp:DropDownList>                       
                    </td>
                    <td>
                         &nbsp; &nbsp; &nbsp; &nbsp;
                      <td align="right">
                        <asp:Label runat="server" Text="Mes:" ></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="cbo_Mes" runat="server"></asp:DropDownList>
                    </td>
                    <td>
                         &nbsp; &nbsp; &nbsp; &nbsp;
                    </td>
                       <td align="right">
                        <asp:Label runat="server" Text="TRM:" ></asp:Label>
                    </td>
                    <td align="right">
                        <asp:TextBox ID="txt_Trm" AutoPostBack="false" runat="server" Width="60px"  ></asp:TextBox>
                    </td>
                </tr>                                                          
            </table>
                   
                     <table>
                         <tr>
                             <td class="auto-style4">
                                 <asp:Button ID="btn_Registrar" runat="server" Text="Registrar"
                                     BackColor="#1C5AB6" ForeColor="White"
                                     BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btn_Registrar_Click" />
                             </td>
                             <td>&nbsp;&nbsp;&nbsp;</td>
                             <td class="auto-style4">
                                 <asp:Button ID="btn_Filtrar" runat="server" Text="Filtrar"
                                     BackColor="#1C5AB6" ForeColor="White"
                                     BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Width="75px" OnClick="btn_Filtrar_Click" />
                             </td>
                         </tr>
                     </table>
                     <br />                 
    </asp:Panel>
            
            <asp:GridView ID="grid_Maestro" runat="server" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="3"  PageSize="20" Width="500px" AllowPaging="True"
                OnPageIndexChanging="grid_Maestro_PageIndexChanging" 
                OnRowCommand="grid_Maestro_RowCommand"                                                   
                 AutoGenerateColumns="False" 
                Font-Size="8pt" Font-Names="arial" Height="16px" DataKeyNames="mon_trm_id">

                <AlternatingRowStyle BackColor="Gainsboro" />
                <Columns>
                    <asp:TemplateField HeaderText="mon_trm_id" Visible="false">
                        <EditItemTemplate>
                            <asp:DropDownList ID="DropDownList1" runat="server">
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="mon_trm_id"  runat="server" Text='<%# Bind("mon_trm_id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Moneda" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="Label2" Width="90" runat="server" Text='<%# Bind("mon_descripcion") %>'></asp:Label>
                        </ItemTemplate>
                         <EditItemTemplate>
                           <EditItemTemplate>
                                <asp:DropDownList ID="cbo_UpdMoneda" Width="120px" DataSource="<%# ListarMonedaDgv() %>" DataTextField="mon_descripcion" DataValueField="mon_id" AutoPostBack="false" runat="server" Text='<%# Bind("moneda") %>'>
                            </asp:DropDownList>                             
                        </EditItemTemplate>
                        </EditItemTemplate>                      
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Año" HeaderStyle-Width="90px" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="Label2" Width="30" runat="server" Text='<%# Bind("año") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                                 <asp:DropDownList ID="cbo_UpdAño" Width="55px" DataSource="<%# ListarAñoDgv() %>"
                                  DataTextField="anio_descripcion" DataValueField="anio_id"  AutoPostBack="false" 
                                  runat="server" Text='<%# Bind("año") %>'>
                            </asp:DropDownList>                          
                        </EditItemTemplate>                 
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Mes" HeaderStyle-Width="30px" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="Label2" Width="30" runat="server" Text='<%# Bind("mes") %>'></asp:Label>
                        </ItemTemplate>
                         <EditItemTemplate>
                            <asp:DropDownList ID="cbo_UpdMes" Width="40px" DataSource="<%# ListarMesDgv() %>"
                             DataTextField="mes_periodo" DataValueField="mes_id"  AutoPostBack="false"
                             runat="server" Text='<%# Bind("mes") %>'>
                            </asp:DropDownList>  
                        </EditItemTemplate>                  
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Trm" HeaderStyle-Width="90px" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label Width="70px" ID="Labeluni" runat="server" Text='<%# Bind("Trm","{0:0,0.00}") %>'></asp:Label>
                        </ItemTemplate>
                         <EditItemTemplate>
                            <asp:TextBox Width="60px" ID="txt_Trm"  AutoPostBack="false"  runat="server" Text='<%# Bind("Trm") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle Width="80px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" />                        
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Periodo" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="Label2" Width="50" runat="server" Text='<%# Bind("periodo") %>'></asp:Label>
                        </ItemTemplate>                                          
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Fecha_Registro" HeaderStyle-Width="100px">
                        <ItemTemplate>
                            <asp:Label Width="70px" ID="lbl_fechReg" runat="server" Text='<%# Bind("fecha_registro","{0:d}") %>'></asp:Label>
                        </ItemTemplate>                     
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Usuario" HeaderStyle-Width="100px">
                        <ItemTemplate>
                            <asp:Label ID="Label2" Width="93px" runat="server" Text='<%# Bind("usuario") %>'></asp:Label>
                        </ItemTemplate>                       
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="Actualizar" HeaderStyle-Width="50px" ShowSelectButton="True">
                        <HeaderStyle Width="50px"></HeaderStyle>
                    </asp:CommandField>  
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
