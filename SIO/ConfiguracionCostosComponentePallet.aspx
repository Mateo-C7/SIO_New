<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="ConfiguracionCostosComponentePallet.aspx.cs" Inherits="SIO.ConfiguracionCostosComponentePallet" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

   
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">         
   
     <script type="text/javascript">

            function confirmarEliminacion() {

                if (confirm("¿Desea Eliminar Este Registro?")) {

                    return true;
                }
                else {
                    return false;
                }
            }      
    </script>

         <script type="text/javascript">

            function confirmarAgregarNuevoComponente() {

                if (confirm("¿Desea agregar un nuevo componente a este pallet?")) {

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
        </style>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">

    <table id="Fondo" class="fondoazul" width="100%" >
        <tr>
            <td>
                  <asp:Label ID="lblusuario" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="12pt" ForeColor="White"   Text="Configuracion Costos De Componentes De Pallet"></asp:Label>
            </td>
        </tr>
    </table>
  
        <asp:UpdatePanel UpdateMode="Conditional" ID="updpnlmaestro" runat="server">
        <ContentTemplate>

<table width="100%">
    <tr>
        <td>
            <table>
                <tr>
                    <td style="text-align: right">
                        <asp:Label ID="lbl_TipoPallet" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                            Text="Tipo Pallet:" Width="100px"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="cboTipoPallet"
                            AutoPostBack="true"
                            runat="server"
                            Width="160px" OnSelectedIndexChanged="cboTipoPallet_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>  
            
              <table>
                  <tr>
                      <td style="vertical-align:top">
                                <table width="550px" class="fondoazul" >
                   <tr>
                    <td >
                        <asp:Button ID="Btn_NuevoCompo" runat="server" Text="Nuevo Componente" Height="20px" OnClick="Btn_NuevoCompo_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                 
                        <asp:Label ID="Label4" Text="Detalle Pallet" Font-Names="Arial" Font-Size="10pt" ForeColor="white" runat="server"></asp:Label>
                    </td>
                </tr>
                </table>  
                           <asp:GridView ID="GridView_DetallePallet"  runat="server" BackColor="White"
                                BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="4"
                                Width="550px" AutoGenerateColumns="False" Font-Size="8pt" Font-Names="arial" 
                          DataKeyNames="tpocomp_id" ShowHeaderWhenEmpty="True"
                                OnRowEditing="GridView_DetallePallet_RowEditing" OnRowUpdating="GridView_DetallePallet_RowUpdating"
                               OnRowCancelingEdit="GridView_DetallePallet_RowCancelingEdit" OnRowDeleting="GridView_DetallePallet_RowDeleting">                                                              
                <AlternatingRowStyle BackColor="Gainsboro" />
                <Columns>                  
                    <asp:TemplateField HeaderText="Componente">
                        <EditItemTemplate>
                            <asp:DropDownList ID="cbo_tpocomp_id" runat="server" Width="190px" Text='<%# Bind("tpocomp_id") %>' Enabled="false"  DataSource="<%# ListarTipoComponente() %>" DataTextField="tpocomp_nombre"  DataValueField="tpocomp_id"  AutoPostBack="false"></asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_Componente" runat="server" Text='<%# Bind("tpocomp_nombre") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="200px"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Valor Unitario">
                        <EditItemTemplate>
                            <asp:TextBox ID="Txt_Valor_Unitario" Width="120px" runat="server" Enabled="false" AutoPostBack="false" Text='<%# Bind("tpocomp_valor") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle Width="90px"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("tpocomp_valor") %>'></asp:Label>
                        </ItemTemplate>
                          <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="Unidades">
                        <EditItemTemplate>
                            <asp:TextBox ID="Txt_Unidades" Width="120px" runat="server" AutoPostBack="false" Text='<%# Bind("compal_unidades") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle Width="90px"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="Label33" runat="server" Text='<%# Bind("compal_unidades") %>'></asp:Label>
                        </ItemTemplate>
                          <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>                  
                    <asp:CommandField HeaderText="Editar" ShowEditButton="True" ItemStyle-HorizontalAlign="Center"/>                                                                 
                  <asp:TemplateField ShowHeader="False" HeaderText="Eliminar">
                                          <ItemTemplate>
                                              <asp:LinkButton ID="btn_EliminarUnidadComponente" Text="Eliminar" runat="server" ForeColor="Black" CommandName="delete" OnClientClick="return confirmarEliminacion();"></asp:LinkButton>                                              
                                          </ItemTemplate>
                                          <HeaderStyle Width="20px"></HeaderStyle>
                                       <ItemStyle HorizontalAlign="Center"/>
                                      </asp:TemplateField>                                  
                </Columns>
                 <EditRowStyle HorizontalAlign="Left" />
                 <EmptyDataRowStyle BorderStyle="Solid" />
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#1C5AB6" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" HorizontalAlign="Left"/>
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView> 
                          <asp:DropDownList ID="cbo_NuevoCompo" runat="server" AutoPostBack="true" Width="210px" OnSelectedIndexChanged="cbo_NuevoCompo_SelectedIndexChanged"></asp:DropDownList>
                           <asp:TextBox ID="txt_NuevoValorCompo" runat="server"  Width="90px"></asp:TextBox>   
                           <asp:TextBox ID="txt_NuevaUnidadCompo" runat="server" Width="90px"></asp:TextBox>
                          <asp:Button ID="Btn_AgregarCompo" runat="server" Text="Agregar" OnClick="Btn_AgregarCompo_Click" OnClientClick="return confirmarAgregarNuevoComponente();"/>    
                          <asp:Button ID="Btn_CancelarCompo" runat="server" Text="Cancelar" OnClick="Btn_CancelarCompo_Click" />                            
                      </td>
                      <td>&nbsp;&nbsp;&nbsp;</td>
                      <td style="vertical-align:top">                          
            <table width="100%"  class="fondoazul"  >
                   <tr align="center">
                    <td>
                        <asp:Label ID="Lbl_NombreGrilla" Text="Tipos Componentes" Height="20px" Font-Names="Arial" Font-Size="10pt" ForeColor="white" runat="server"></asp:Label>
                    </td>
                </tr>
                </table>                         
                           <asp:GridView ID="GridMaestroComponente" runat="server"  BackColor="White" BorderColor="#999999"
                BorderStyle="Solid" BorderWidth="1px" CellPadding="3" Width="280px" 
                AutoGenerateColumns="False" Font-Size="8pt" Font-Names="arial" 
                DataKeyNames="tpocomp_id" ShowHeaderWhenEmpty="True" OnRowEditing="GridMaestroComponente_RowEditing" 
                OnRowCancelingEdit="GridMaestroComponente_RowCancelingEdit"
                OnRowUpdating="GridMaestroComponente_RowUpdating" OnSelectedIndexChanged="GridMaestroComponente_SelectedIndexChanged">                         
                 <AlternatingRowStyle BackColor="Gainsboro" />
                <Columns>                                                       
                     <asp:TemplateField HeaderText="Componente">
                        <EditItemTemplate>
                            <asp:DropDownList ID="cbo_idCompo" runat="server" Width="190px" Text='<%# Bind("tpocomp_id") %>' Enabled="false"  DataSource="<%# ListarTipoComponente() %>" DataTextField="tpocomp_nombre"  DataValueField="tpocomp_id"  AutoPostBack="false"></asp:DropDownList>                           
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lbl_Componente" runat="server" Text='<%# Bind("tpocomp_nombre") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="200px"></HeaderStyle>
                    </asp:TemplateField>                 
                    <asp:TemplateField HeaderText="Valor Unitario" HeaderStyle-Width="110px">
                        <EditItemTemplate>
                            <asp:TextBox ID="Txt_ValorUnitario" Width="110px"  runat="server" Text='<%# Bind("tpocomp_valor") %>'></asp:TextBox>                          
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("tpocomp_valor") %>'></asp:Label>
                        </ItemTemplate>
                            <HeaderStyle Width="110px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="Editar" ShowEditButton="True" ItemStyle-HorizontalAlign="Center"/>                                      
                </Columns>
                  <EditRowStyle HorizontalAlign="Left" />
                  <EmptyDataRowStyle BorderStyle="Solid" />
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#1C5AB6" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" HorizontalAlign="Left"/>
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView> 
                            <asp:Label ID="lbl_Msg_GridMaestroComponente" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" runat="server"></asp:Label>           
                      </td>
                  </tr>
                  <tr>
                      <td>
                          <asp:Label ID="lbl_Msg_Detalle_Pallet" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" ></asp:Label>
                      </td>
                  </tr>
              </table>                                           
</table>
          </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>