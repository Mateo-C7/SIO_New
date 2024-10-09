<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="Pais.aspx.cs" Inherits="SIO.Pais" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
      <script type="text/javascript">

            function confirmarEliminarPais() {

                if (confirm("¿Realmente quieres eliminar este Registro?")) {

                    return true;
                }
                else {
                    return false;
                }
            }      
    </script>
    
    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }
    </style>
</asp:Content>


<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <table id="TblTituloPias" class="fondoazul" width="100%">
                <tr>
                    <td>
                        <asp:Label ID="lblusu" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="12pt" ForeColor="White" Text="Paises"></asp:Label>
                    </td>
                </tr>
            </table>
    
       <asp:Panel ID="pnlCamposDetaPias"  runat="server"  
                Font-Names="Arial" Font-Size="8pt" ForeColor="Black"  GroupingText="Detalle_Pais"
                Width="710px" Height="80px" HorizontalAlign="Left" CssClass="auto-style2" BorderStyle="Groove" BorderColor="#3366cc">

            <table  class="style15">
                <tr>
                    <td>
                        <asp:Label runat="server"  Text="Pais" ></asp:Label>
                    </td>
                    <td >
                       <asp:TextBox ID="Ctxt_NombrePais" runat="server" Width="150px" MaxLength="500"></asp:TextBox>
                    </td>
                     <td>
                        <asp:Label runat="server" Text="Impuesto" ></asp:Label>
                    </td>
                    <td>
                   <asp:TextBox ID="Ctxt_Impuesto" runat="server" MaxLength="2" Width="50px">0</asp:TextBox>
                    </td>
                      <td>
                        <asp:Label runat="server" Text="Zona" ></asp:Label>
                    </td>
                    <td>
                       <asp:DropDownList ID="Ccbo_Grup_Pais" runat="server" AutoPostBack="false">
                            </asp:DropDownList>
                    </td>
                       <td>
                        <asp:Label runat="server" Text="Moneda" ></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="Ccbo_Moneda" runat="server"  AutoPostBack="false"></asp:DropDownList>
                    </td>
                     <%--      <td>
                        <asp:Label runat="server" Text="Grupo Pais" ></asp:Label>
                    </td>
                    <td>
                         <asp:TextBox ID="Ctxt_Grupo" AutoPostBack="false" runat="server" Width="50px"></asp:TextBox>
                    </td>  --%>
                   
                </tr>           
                <tr>
                           <td class="auto-style1">
                        <asp:Label runat="server" Text="Longitud" ></asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:TextBox ID="Ctxt_Longitud" MaxLength="7"  AutoPostBack="false" runat="server" Width="69px">0</asp:TextBox>
                    </td>
                          <td class="auto-style1">
                        <asp:Label runat="server" Text="Latitud" ></asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:TextBox ID="Ctxt_Latitud"  MaxLength="7"  AutoPostBack="false" runat="server" Width="60px" >0</asp:TextBox>
                    </td>
                        <td class="auto-style1">
                        <asp:Label runat="server" Text="Zona Siat" ></asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:DropDownList ID="Ccbo_zona_siat" AutoPostBack="false" Width="120px" runat="server"></asp:DropDownList>
                    </td>                                             
                </tr>

            </table>
    </asp:Panel>
                  
      <table>
        <tr>
            <td>
                <asp:Button ID="btn_GuardarDetaPais" runat="server" Text="Guardar"
                    BackColor="#1C5AB6" ForeColor="White"
                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btn_GuardarDetaPais_Click"/>
                 
            </td>
            <td>
                <asp:Button ID="btn_CancelarDetaPais" runat="server" Text="Cancelar"
                    BackColor="#1C5AB6" ForeColor="White"
                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btn_CancelarDetaPais_Click"/>
                 <asp:Label ID="LblMsgRegistroPais" runat="server" ForeColor="Black" ></asp:Label>
            </td>
     
        </tr>
    </table>
        <asp:Button ID="btn_Habilitar_pnlcampdetapais" runat="server" Text="Agregar Pais"
                    BackColor="#1C5AB6" ForeColor="White"
                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btn_Habilitar_pnlcampdetapais_Click"/>
    <br/>
                 <asp:Label ID="LblMsgTotalItems" runat="server" Text="." ForeColor="Black"  ></asp:Label>
             <asp:GridView ID="GridView_Pais"  runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                BorderWidth="1px" CellPadding="4" Width="900px" AllowPaging="True"
                AutoGenerateColumns="False" Font-Size="8pt" Font-Names="arial" Height="30px" DataKeyNames="Pai_id"
                OnPageIndexChanging="GridView_Pais_PageIndexChanging" OnRowEditing="GridView_Pais_RowEditing1" 
                OnRowCancelingEdit="GridView_Pais_RowCancelingEdit1" OnRowUpdating="GridView_Pais_RowUpdating" 
                OnRowDeleting="GridView_Pais_RowDeleting"                                
                 CssClass="auto-style2" PageSize="28" style="margin-top: 0px" >
                <AlternatingRowStyle BackColor="Gainsboro" />
                <Columns>
                    <asp:TemplateField HeaderText="Pais" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Left" Visible="False">
                        <EditItemTemplate>
                         <asp:TextBox ID="cbo_Pais"  Width="150px" runat="server" ReadOnly="True" Text='<%# Bind("pai_id") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1"  runat="server" Text='<%# Bind("pai_id") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="120px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pais_" HeaderStyle-Width="150px">
                        <EditItemTemplate>
                            <asp:TextBox ID="Txt_NombrePais"  Width="150px" runat="server" ReadOnly="True" Text='<%# Bind("pai_nombre") %>' Enabled="False"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label11" runat="server" Text='<%# Bind("pai_nombre") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="150px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Impuesto" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Right">
                        <EditItemTemplate>
                            <asp:TextBox ID="Txt_Impuesto" MaxLength="2" runat="server" Text='<%# Bind("pai_impuesto") %>' Width="50px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("pai_impuesto") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="50px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Codigo Erp" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center" Visible="False">
                        <EditItemTemplate>
                            <asp:TextBox ID="Txt_Cod_Erp" runat="server" Text='<%# Bind("pai_cod_erp") %>' Width="70px"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("pai_cod_erp") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="70px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="pai_grupopais_id"  HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Right" Visible="False">
                        <EditItemTemplate>
                            <asp:TextBox ID="Txt_GrupoPais" runat="server" Text='<%# Bind("pai_grupopais_id") %>' Width="70"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("pai_grupopais_id") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="70px" />           
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Zona"  HeaderStyle-Width="90px" ItemStyle-HorizontalAlign="Left">
                        <EditItemTemplate>
                           <asp:DropDownList ID="cbo_Grup_Pais" runat="server"  DataSource="<%# ObtenerGrupoPais()%>" DataTextField="grpa_gp1_nombre" DataValueField="grpa_id" Text='<%# Bind("pai_zona_id") %>' AutoPostBack="false">
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("grpa_gp1_nombre") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="90px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Moneda"  HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Right">
                        <EditItemTemplate>
                           <asp:DropDownList ID="cbo_Moneda" runat="server"  DataSource="<%# ObtenerMoneda()%>" DataTextField="mon_descripcion" DataValueField="mon_id" Text='<%# Bind("pai_moneda") %>' AutoPostBack="false"></asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("mon_descripcion") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="70px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Longitud"  HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Right">
                        <EditItemTemplate>
                            <asp:TextBox ID="Txt_Longitud" MaxLength="7" runat="server" Text='<%# Bind("longitud") %>' Width="65px">0</asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("longitud","{0:0}") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="50px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Latitud"  HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Right">
                        <EditItemTemplate>
                            <asp:TextBox ID="Txt_Latitud" MaxLength="7" runat="server" Text='<%# Bind("latitud") %>' Width="65">0</asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label8" runat="server" Text='<%# Bind("latitud", "{0:0}") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="50px" />                   
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Zona Siat"  HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Left">
                        <EditItemTemplate>
                            <asp:DropDownList ID="cbo_Zona" runat="server"  DataSource="<%# ObtenerZona()%>" DataTextField="nombre_zona" DataValueField="siat_zona_id" Text='<%# Bind("zona_siat") %>' AutoPostBack="false">
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label9" runat="server" Text='<%# Bind("nombre_zona") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="70px" />                        
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SubZona"  HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" Visible="False">
                        <EditItemTemplate>
                            <asp:TextBox ID="Txt_SubZona" runat="server" Text='<%# Bind("pai_maneja_subzona") %>' Width="50"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label10" runat="server" Text='<%# Bind("pai_maneja_subzona") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="50px" />
                              
                        <ItemStyle HorizontalAlign="Center" />
                              
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="Editar" ShowEditButton="True" HeaderStyle-Width="40px">
                    <HeaderStyle Width="40px" />
                    </asp:CommandField>
                    <asp:TemplateField HeaderText="Eliminar" >
                        <ItemTemplate>
                            <asp:LinkButton ID="LinEliminar" Text="Eliminar" runat="server" CommandName="Delete" OnClientClick="return confirmarEliminarPais();" ForeColor="Black"></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="30px" />
                    </asp:TemplateField>
                </Columns>
                 <EditRowStyle HorizontalAlign="Left" />
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#1C5AB6" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black"/>
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
