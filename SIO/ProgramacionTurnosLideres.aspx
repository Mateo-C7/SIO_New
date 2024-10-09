<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="ProgramacionTurnosLideres.aspx.cs" Inherits="SIO.ProgramacionTurnosLideres" %>

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

            function confirmarEliminarMetaProduccion() {

                if (confirm("¿Realmente quieres eliminar este registro?")) {

                    return true;
                }
                else {
                    return false;
                }
            }      
    </script>
 
        <script type="text/javascript">

            function confirmarEliminarDetalleMetaProduccion() {

                if (confirm("¿Realmente quieres eliminar este registro?")) {

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
        .auto-style3 {
            width: 28px;
        }
        .auto-style5 {
            width: 11px;
        }
        .auto-style6 {
            width: 19px;
        }
        .auto-style7 {
            width: 32px;
        }
        .auto-style8 {
            width: 97px;
        }
        .auto-style9 {
            margin-left: 0px;
        }
        .auto-style10 {
            margin-left: 33px;
        }
        </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
          
    <asp:UpdatePanel UpdateMode="Conditional" ID="updpnlmaestro" runat="server">
        <ContentTemplate>
            <table class="fondoazul" width="100%">
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="12pt" ForeColor="White" Text="Registro Programación  Turnos De Lideres"></asp:Label>
                    </td>
                </tr>
            </table>   
            <asp:Panel ID="pnlCamposDetaMaes" runat="server"
                Font-Names="Arial" Font-Size="8pt" ForeColor="Black" GroupingText="Detalle Programación"
                Width="710px" Height="155px" CssClass="auto-style2" BorderStyle="Groove" BorderColor="#3366cc">
                <table class="style15">
                    <tr>
                         <td class="auto-style6">&nbsp;&nbsp;</td>

                         <td align="right"><asp:Label ID="Label9" runat="server" Text="Planta:"></asp:Label></td>
                        <td>
                        <asp:DropDownList ID="Cbo_PlantaOper" AutoPostBack="true" OnTextChanged="Cbo_PlantaOper_TextChanged" runat="server" Width="150px"></asp:DropDownList></td>
                        
                    </tr>
                    <tr>
                        <td class="auto-style6">&nbsp;&nbsp;</td>
                        <td align="right">
                            <asp:Label runat="server" Text="Cedula Lider:" Width="63px" CssClass="auto-style9"></asp:Label>
                        </td>
                        <td style="text-align: rigth" >
                            <asp:TextBox ID="Txt_IdOper" runat="server" Width="145px" AutoPostBack="true" OnTextChanged="Txt_IdOper_TextChanged"></asp:TextBox>                                                                      
                        </td> 
                          <td class="auto-style7" >
                            
                        </td>                 
                        <td style="text-align: right" >
                            <asp:TextBox ID="Txt_NombreOperario"  AutoPostBack="false" runat="server" Width="223px" CssClass="auto-style10"></asp:TextBox>                                         
                        </td>
                        <td class="auto-style3" >
                            &nbsp;
                        </td>
                        <td align="right">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>                                           
                    </tr>
                    </table>
                <table class="style15">
                    <tr>
                           <td class="auto-style5"></td>
                        <td align="right">
                            <asp:Label runat="server" Text="Proceso:" Width="55px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="Cbo_Proceso" runat="server" Width="150px"></asp:DropDownList>
                        </td>
                        
                        <td  align="right">
                            <asp:Label ID="Label2" runat="server" Text="Turno:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="Cbo_Turno" runat="server" Width="89px" ></asp:DropDownList>
                        </td>     
                        <td class="auto-style8">

                        </td>                                        
                         <td>
                               &nbsp;</td> 
                    </tr>
                    <tr>
                           <td class="auto-style5"></td>
                        <td class="auto-style5" align="right">
                            <asp:Label ID="Label1" runat="server" Text="Fecha Inicio:" Width="70px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="Txt_FechaIni" runat="server" Width="75px" AutoPostBack="true" OnTextChanged="Txt_FechaIni_TextChanged"></asp:TextBox>
                              <asp:CalendarExtender ID="CalIngFecIni" runat="server" Format="dd/MM/yyyy"
                        TargetControlID="Txt_FechaIni"></asp:CalendarExtender>                            
                        </td>                           
                        <td class="auto-style5" align="right">
                            <asp:Label ID="Label3" runat="server" Text="Fecha Final:" Width="70px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="Txt_FechaFinal" runat="server" Width="75px" AutoPostBack="true" OnTextChanged="Txt_FechaFinal_TextChanged"></asp:TextBox>
                              <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                        TargetControlID="Txt_FechaFinal"></asp:CalendarExtender>                            
                        </td>
                    </tr>  
                    <tr>
                           <td class="auto-style5"></td>
                        <td class="auto-style5" align="right">                            
                        </td>
                        <td>
                            <asp:Button ID="Btn_Guardar" runat="server" Text="Guardar"
                            BackColor="#1C5AB6" ForeColor="White"
                            BorderColor="#999999" Font-Bold="True" Font-Names="Arial" 
                            OnClientClick="return confirm('¿Desea Enviar los Datos?')"
                              OnClick="Btn_Guardar_Click"  Font-Size="8pt"/>           
                        </td> 
                    </tr>                  
                </table>                
            </asp:Panel>
              
              <asp:GridView ID="Grid_Detalle_Programacion" runat="server" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="2" Width="226px" AllowPaging="True" DataKeyNames="TurnEmp_Id" 
                AutoGenerateColumns="False" Font-Size="8pt" Font-Names="arial" Height="16px"  PageSize="15"
                   OnRowDeleting="Grid_Detalle_Programacion_RowDeleting" OnPageIndexChanging="Grid_Detalle_Programacion_PageIndexChanging" OnRowCommand="Grid_Detalle_Programacion_RowCommand">
                <%-- <AlternatingRowStyle BackColor="Gainsboro" />--%>
                <Columns>
                    <asp:TemplateField HeaderText="MpdeID" Visible="False">
                        <EditItemTemplate>
                            <asp:TextBox ID="Txt_TurnEmp_Id" AutoPostBack="false" runat="server" Text='<%# Bind("TurnEmp_Id") %>' BorderStyle="None" ReadOnly="True"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label8" runat="server" Text='<%# Bind("TurnEmp_Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                  <asp:CommandField HeaderText="Actualizar"   HeaderStyle-Width="50px" ShowSelectButton="True">
                        <HeaderStyle Width="50px"></HeaderStyle>
                    </asp:CommandField>
                    <asp:TemplateField HeaderText="Fecha Inicial"  FooterStyle-VerticalAlign="Middle" HeaderStyle-Width="60px">
                        <EditItemTemplate>
                            <asp:TextBox Width="60px" ID="Txt_FechaIni" AutoPostBack="false" runat="server" Text='<%# Bind("TurnEmp_FechIni","{0:d}") %>'></asp:TextBox>
                            <asp:CalendarExtender ID="CalFecIni" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="Txt_FechaIni">
                            </asp:CalendarExtender>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label Width="58px" ID="lblfechIni" runat="server" Text='<%# Bind("TurnEmp_FechIni","{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="60px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fecha Final" HeaderStyle-Width="60px">
                        <EditItemTemplate>
                            <asp:TextBox Width="60px" ID="Txt_FechaFin" AutoPostBack="false" runat="server" Text='<%# Bind("TurnEmp_FechFin","{0:d}") %>'></asp:TextBox>
                            <asp:CalendarExtender ID="CalFecFin" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="Txt_FechaFin">
                            </asp:CalendarExtender>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label Width="58px" ID="lblFechFin" runat="server" Text='<%# Bind("TurnEmp_FechFin","{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="60px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText=" Turno" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="40px">
                        <EditItemTemplate>
                              <asp:TextBox Width="100px"  AutoPostBack="false" ID="Txt_Turno" runat="server" Text='<%# Bind("TurnEmp_Turno") %>'></asp:TextBox>                     
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label Width="50px"  MaxLength="3" ID="Label4" runat="server" Text='<%# Bind("TurnEmp_Turno") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="30px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>             
                    <asp:TemplateField HeaderText="Proceso" HeaderStyle-Width="130px">
                        <EditItemTemplate>
                            <asp:TextBox Width="130px"  AutoPostBack="false" ID="Txt_Proceso" runat="server" Text='<%# Bind("Proceso") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label Width="130px" ID="Label5" runat="server" Text='<%# Bind("Proceso") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="130px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Lider" HeaderStyle-Width="180px">
                        <EditItemTemplate>
                            <asp:TextBox Width="180px"  ID="Txt_Lider" MaxLength="4" AutoPostBack="false" runat="server" Text='<%# Bind("Lider") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label Width="180px" ID="Label6" runat="server" Text='<%# Bind("Lider") %>'>%</asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="180px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>    
                       <asp:TemplateField HeaderText="Planta" HeaderStyle-Width="100px">                                             
                        <ItemTemplate>
                            <asp:Label Width="105px" ID="Planta" runat="server" Text='<%# Bind("Planta") %>'>%</asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="100px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>                                      
                     <asp:TemplateField HeaderText="Eliminar">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinEliminarDetalle" Text="Eliminar"  runat="server" CommandName="Delete" OnClientClick="return confirmarEliminarDetalleMetaProduccion();" ForeColor="Black"></asp:LinkButton>
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
