<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="AnulacionOrdenes.aspx.cs" Inherits="SIO.AnulacionOrdenes" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms,Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <script type="text/javascript">

            function confirmarMigrarObs() {

                if (confirm("¿Desea migrar las observaciones a esta orden?")) {

                    return true;
                }
                else {
                    return false;
                }
            }      
    </script>
       <style type="text/css">
           .auto-style1 {
               height: 30px;
           }
       </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <br />     
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                 <ContentTemplate>
        <table class="fondoazul" width="100%">
        <tr>
            <td align="center">
                <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="10pt" ForeColor="White" Text="ANULACIÓN DE ORDENES"></asp:Label>
            </td>
        </tr>
    </table>
       <asp:Panel runat="server" ID="Pnl_Buscar" GroupingText=""
                       Font-Names="Arial" Font-Size="8pt" ForeColor="Black" 
                Width="287px" Height="100%">
           <table>
               <tr>
                   <td class="auto-style1">
                       <asp:Label ID="Label1" runat="server" Text="Orden:"></asp:Label>
                   </td>
                     <td class="auto-style1">
                         <asp:TextBox ID="txt_Orden" Width="60px" runat="server"></asp:TextBox>                             
                   </td>
                   
                       <td style="text-align: right" class="auto-style1">
                         <asp:Button ID="btn_Buscar" runat="server" Text="Buscar" Width="63px" OnClick="btn_Buscar_Click" />
                   </td>
               </tr>
               <tr>
                   <td>
                        <asp:Label ID="Label2" runat="server" Text="Estado:"></asp:Label>
                   </td>
                   <td>
                        <asp:Label ID="lbl_EstadoOrden" Width="40px" runat="server" Font-Bold="True" Font-Size="Medium" ></asp:Label>
                       
                   </td>

                   <td style="text-align: right" class="auto-style3">
                        <asp:Label ID="Label3" runat="server" Text="FUP/Versión:"></asp:Label>
                   </td>
                   <td>
                        <asp:Label ID="lbl_FupVersion" runat="server" Font-Bold="True" Font-Size="Medium" ></asp:Label>
                   </td>
                   <td>
                           <table class="auto-style4">
               <tr>
                    <td style="text-align: right" >
                       <asp:CheckBox ID="chk_AnularSgto"  Width="120px" Text="Anular Seguimiento" runat="server" />

                   </td>
                    <td>
                        <asp:Button ID="btn_Anular" runat="server" Text="Anular"   onclientclick="return confirm('¿Desea anular la orden?')" OnClick="btn_Anular_Click"  />
                   </td>
               </tr>
           </table>
                   </td>
               </tr>
           </table>       
           </asp:Panel>

      <asp:Panel runat="server" ID="Panel1" GroupingText=""
                       Font-Names="Arial" Font-Size="8pt" ForeColor="Black" 
                Width="510px" Height="100%">
          <table>
              <tr>
                   <td style="text-align: right">
                      <asp:Label ID="Label4" runat="server" Text="Pais:"></asp:Label>
                  </td>
                   <td>
                       <asp:TextBox ID="txt_Pais" Width="200px"  runat="server"></asp:TextBox>
                  </td>
                <td style="text-align: right">
                      <asp:Label ID="Label5" runat="server" Text="Ciudad:"></asp:Label>
                  </td>
                   <td>
                          <asp:TextBox ID="txt_Ciudad" Width="200px"  runat="server"></asp:TextBox>
                  </td>                
              </tr>
                <tr>
                   <td style="text-align: right">
                      <asp:Label ID="Label6" runat="server" Text="Cliente:"></asp:Label>
                  </td>
                   <td>
                          <asp:TextBox ID="txt_Cliente" Width="200px"  runat="server"></asp:TextBox>
                  </td>
              <td style="text-align: right">
                      <asp:Label ID="Label8" runat="server" Text="Obra:"></asp:Label>
                  </td>
                   <td>
                         <asp:TextBox ID="txt_Obra" Width="200px"  runat="server"></asp:TextBox>
                  </td>                
              </tr>
          </table>
          </asp:Panel>
                     <br />
      <asp:Panel runat="server" ID="Panel2" GroupingText="Observaciones"
                       Font-Names="Arial" Font-Size="8pt" ForeColor="Black" 
                Width="485px" Height="100%">

         <asp:GridView ID="GridObs" runat="server" BackColor="White" BorderColor="#999999"
                                  BorderStyle="Solid" BorderWidth="1px" ShowHeaderWhenEmpty="true"
                                  ShowHeader="true" CellPadding="3" Width="480px" AllowPaging="True" 
                                  AutoGenerateColumns="False" Font-Size="8pt" Font-Names="arial"
                                  Height="16px" PageSize="7" OnPageIndexChanging="GridObs_PageIndexChanging"  >
                                  <Columns>                                     
                                      <asp:TemplateField HeaderText="Observaciónes" HeaderStyle-Width="200px" >                                         
                                          <ItemTemplate>
                                              <asp:Label ID="Label1" runat="server" Text='<%# Bind("asobs_Observacion") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Fecha Crea" HeaderStyle-Width="70px">                                       
                                          <ItemTemplate>
                                              <asp:Label ID="Label2" runat="server" Text='<%# Bind("fecha_crea","{0:d}") %>'></asp:Label>
                                          </ItemTemplate>                                  
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Usuario Crea"  HeaderStyle-Width="70px">                                         
                                          <ItemTemplate>
                                              <asp:Label ID="Label3" runat="server" Text='<%# Bind("usu_crea") %>'></asp:Label>
                                          </ItemTemplate>
                                           <HeaderStyle Width="70px"></HeaderStyle>
                                             <ItemStyle HorizontalAlign="Right" />
                                      </asp:TemplateField>                                                                     
                                  </Columns>
                                  <EmptyDataRowStyle BorderStyle="Solid" />
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
     </asp:Panel>
                     <br />
     <asp:Panel runat="server" ID="Panel3" GroupingText="Listado de Ordenes"
                       Font-Names="Arial" Font-Size="8pt" ForeColor="Black" 
                Width="485px" Height="100%">
         <table>
             <tr>
                 <td>          
         <asp:GridView ID="GridListaOrdenes" runat="server" BackColor="White" BorderColor="#999999"
                                  BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="480px" DataKeyNames="actseg_id" AllowPaging="True"
                                  AutoGenerateColumns="False" Font-Size="8pt" Font-Names="arial" Height="16px" PageSize="7"
                                  ShowHeaderWhenEmpty ="True" OnRowDeleting="GridListaOrdenes_RowDeleting" OnPageIndexChanging="GridListaOrdenes_PageIndexChanging">
                                  <Columns>  
                                      <asp:TemplateField HeaderText="actseg_id" Visible="False">                                     
                                      <ItemTemplate>
                                          <asp:Label ID="Label8" runat="server" Text='<%# Bind("actseg_id") %>'></asp:Label>
                                      </ItemTemplate>
                                  </asp:TemplateField>                                   
                                      <asp:TemplateField HeaderText="Orden" HeaderStyle-Width="70px" >                                                                                 
                                           <ItemTemplate>
                                              <asp:Label ID="Label1" runat="server" Text='<%# Bind("Orden") %>'></asp:Label>
                                          </ItemTemplate>
                                           <ItemStyle HorizontalAlign="Right" />
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Planta" HeaderStyle-Width="70px">                                       
                                          <ItemTemplate>
                                              <asp:Label ID="Label2" runat="server" Text='<%# Bind("Planta") %>'></asp:Label>
                                          </ItemTemplate>                                  
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="M2"  HeaderStyle-Width="70px">                                         
                                          <ItemTemplate>
                                              <asp:Label ID="Label3" runat="server" Text='<%# Bind("M2") %>'></asp:Label>
                                          </ItemTemplate>
                                           <HeaderStyle Width="70px"></HeaderStyle>
                                             <ItemStyle HorizontalAlign="Right" />
                                      </asp:TemplateField>  
                                       <asp:TemplateField ShowHeader="False">
                                          <ItemTemplate>
                                              <asp:LinkButton ID="btn_MigrarObs" Text="Migrar_Obs"  runat="server" CommandName="Delete" OnClientClick="return confirmarMigrarObs();" ForeColor="Black"></asp:LinkButton>
                                          </ItemTemplate>
                                          <HeaderStyle Width="20px"></HeaderStyle>
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
                            </td>
             </tr>
         </table>
     </asp:Panel>
                         </ContentTemplate>
            </asp:UpdatePanel>
</asp:Content>
