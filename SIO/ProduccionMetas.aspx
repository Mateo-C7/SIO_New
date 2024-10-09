<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="ProduccionMetas.aspx.cs" Inherits="SIO.ProduccionMetas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">  

        <script type="text/javascript">

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
            margin-left: 18px;
        }
        .auto-style4 {
            width: 4px;
        }
        .auto-style5 {
            width: 46px;
        }
        .auto-style7 {
            width: 78px;
        }
        </style>
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">

    <table id="TblMetaProd" class="fondoazul" width="100%" >
        <tr>
            <td>
                  <asp:Label ID="lblusumetas" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="12pt" ForeColor="White"   Text="Metas De Producción"></asp:Label>
            </td>
        </tr>
    </table>
  
    <asp:UpdatePanel UpdateMode="Conditional" ID="updpnlmaestro" runat="server">
        <ContentTemplate>

<table width="800">
    <tr>
        <td>

       <asp:Panel ID="pnlPlanta" runat="server"
                Font-Names="Arial" Font-Size="8pt" GroupingText="."
                Width="240px"  CssClass="auto-style2">
    <table>
        <tr>
            <td style="text-align: right">
                            <asp:Label ID="lblPlanta" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                Text="Planta:" Width="50px"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cboPlantas" 
                               AutoPostBack="true"
                                runat="server"
                                Width="160px" OnSelectedIndexChanged="cboPlantas_SelectedIndexChanged">
                            </asp:DropDownList>                       
                        </td>
            <td>
                 &nbsp;</td>
            <td>
                <asp:Label ID="lblMperId" runat="server" Text="Mepr_Id" Visible="false" ForeColor="#cccccc"></asp:Label>
            </td>
        </tr>
    </table>

</asp:Panel>

            <asp:Panel ID="pnlIngMetas" runat="server"
               Font-Names="Arial" Font-Size="8pt" ForeColor="Black" GroupingText="Asignacion De Metas"
                Width="960px" Height="52px" CssClass="auto-style2">


                <table class="style15">
                    <tr>
                        <td style="text-align: right">
                            <asp:Label runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black"
                                Text="Proceso:" Width="50px"></asp:Label>
                        </td>
                        <td style="text-align: right" class="altoRengInicio">

                            <asp:DropDownList ID="cboProcesos" runat="server" AutoPostBack="false"
                                Width="200px" OnSelectedIndexChanged="cboProcesos_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right">
                            <asp:Label runat="server" Font-Names="Arial" Font-Size="8pt"
                                Text="Observacion:" Width="50px"></asp:Label>
                        </td>

                        <td style="text-align: right" class="altoRengInicio">

                            <asp:TextBox ID="Txt_Observacionpnlmeta" MaxLength="300" AutoPostBack="false" runat="server" Width="400px" CssClass="auto-style3"> </asp:TextBox>                      
                        </td>
                         <td class="auto-style5">&nbsp&nbsp</td>
                            <td>
                                <asp:Button ID="btnAgregarMeta" runat="server" Text="Guardar"
                                    BackColor="#1C5AB6" ForeColor="White"
                                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                     OnClick="btnAgregarMeta_Click1" Height="26px" />
                            </td>
                         <td>
                                <asp:Button ID="btn_CanlarAgreMetas" runat="server" Text="Cancelar"
                                    BackColor="#1C5AB6" ForeColor="White"
                                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                     OnClick="btn_CanlarAgreMetas_Click" Height="26px" />
                            </td>
                          <td>&nbsp&nbsp&nbsp&nbsp</td>                     
                    </tr>
                </table>
            </asp:Panel>
            <br />
            <asp:Label ID="lblMsgPrincipal"  Font-Names="Arial" Font-Size="8pt" ForeColor="Black" runat="server"></asp:Label>

            <asp:GridView ID="GridView_Maestro_Metas"  runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None"
                BorderWidth="1px" CellPadding="4" Width="680px" AllowPaging="True"
                AutoGenerateColumns="False" Font-Size="8pt" Font-Names="arial" Height="30px" DataKeyNames="Mepr_Id"
                OnRowCommand="GridView_Maestro_Metas_RowCommand"  OnPageIndexChanging="GridView_Maestro_Metas_PageIndexChanging" 
                OnRowUpdating="GridView_Maestro_Metas_RowUpdating" OnRowEditing="GridView_Maestro_Metas_RowEditing" 
                OnRowDeleting="GridView_Maestro_Metas_RowDeleting1" OnRowCancelingEdit="GridView_Maestro_Metas_RowCancelingEdit1"
             
                 CssClass="auto-style2" >
                <AlternatingRowStyle BackColor="Gainsboro" />
                <Columns>
                    <asp:TemplateField HeaderText="ID" Visible="False">
                        <EditItemTemplate>
                            <asp:TextBox ID="Txt_Id_Mepr" runat="server" BorderStyle="None" ReadOnly="True" Text='<%# Bind("Mepr_Id") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Mepr_Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Proceso" HeaderStyle-Width="300px">
                        <EditItemTemplate>
                            <asp:DropDownList ID="cbo_IdProceso" runat="server" Width="190px" Text='<%# Bind("IDProceso") %>' DataSource="<%# ObtenerProcPlan() %>" DataTextField="Proceso"  DataValueField="Id_Proceso"  AutoPostBack="false"></asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblProceso" runat="server" Text='<%# Bind("Proceso") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="200px"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Observación">
                        <EditItemTemplate>
                            <asp:TextBox ID="Txt_Observacion" Width="380px" runat="server" AutoPostBack="false" Text='<%# Bind("Mepr_Observacion") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle Width="400px"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("Mepr_Observacion") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="Detalle"   HeaderStyle-Width="50px" ShowSelectButton="True">
                        <HeaderStyle Width="50px"></HeaderStyle>
                    </asp:CommandField>
                    <asp:CommandField HeaderText="Editar" ShowEditButton="True" />

                    <asp:TemplateField HeaderText="Eliminar">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinEliminar" Text="Eliminar"  runat="server" CommandName="delete" OnClientClick="return confirmarEliminarMetaProduccion();" ForeColor="Black"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                 <EditRowStyle HorizontalAlign="Left" />
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
    <asp:Label ID="lblGridMetas" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" ></asp:Label>
                <table >
        <tr>
            <td class="auto-style4">
                <asp:Button ID="btn_Habilitar_pnlMetas" runat="server" Text="Agregar Meta" 
                    BackColor="#1C5AB6" ForeColor="White"
                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    OnClick="btn_Habilitar_pnlMetas_Click" />
                </td>    
        </tr>
    </table>
            <br />
       <table>               
                <tr>
                    <td>
                        <asp:Label ID="lblDescripcionProce" Font-Names="Arial" Font-Size="11pt" ForeColor="Black" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>   
    
        <asp:Panel ID="pnlCamposDetaMaes"  runat="server"  
                Font-Names="Arial" Font-Size="8pt" ForeColor="Black"  GroupingText="Detalle_Metas"
                Width="674px" Height="80px" CssClass="auto-style2" BorderStyle="Groove" BorderColor="#3366cc">

            <table  class="style15">
                <tr>
                    <td align="right">
                        <asp:Label runat="server"  Text="Fecha_Inicial:" ></asp:Label>
                    </td>
                    <td >
                        <asp:TextBox  ID="Txt_FechIni" AutoPostBack="false" runat="server"  Width="70px"></asp:TextBox>
                               <asp:CalendarExtender ID="CalIngFecIni" runat="server" Format="dd/MM/yyyy"
                        TargetControlID="Txt_FechIni"></asp:CalendarExtender>
                    </td>

                     <td align="right">
                        <asp:Label runat="server" Text="Fecha_Final:" ></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Txt_FechFin" AutoPostBack="false" runat="server" Width="70px"></asp:TextBox>
                         <asp:CalendarExtender ID="CalIngFecFin" runat="server" Format="dd/MM/yyyy"
                        TargetControlID="Txt_FechFin"></asp:CalendarExtender>
                    </td>
                      <td align="right">
                        <asp:Label runat="server" Text="Operarios:" ></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Txt_NumOper" AutoPostBack="false" runat="server" Width="60px" MaxLength="3"></asp:TextBox>
                    </td>
                       <td align="right">
                        <asp:Label runat="server" Text="Unidades:" ></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Txt_Unidades" AutoPostBack="false" runat="server" Width="60px" MaxLength="7"></asp:TextBox>
                    </td>
                </tr>           
                <tr>
                           <td align="right">
                        <asp:Label runat="server" Text="Meta:" ></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Txt_Metas" AutoPostBack="false" runat="server" Width="69px" MaxLength="7"></asp:TextBox>
                    </td>
                          <td>
                        <asp:Label runat="server" Text="Tolerancia/Calidad:" ></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="Txt_Tolerancia" AutoPostBack="false" runat="server" Width="60px" MaxLength="4"></asp:TextBox>
                    </td>
                        <td align="right">
                        <asp:Label runat="server" Text="Tipo_Pieza:" ></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="cbo_TpoPza" AutoPostBack="false" Width="120px" runat="server"></asp:DropDownList>
                    </td>     
                         <td align="right">
                        <asp:Label runat="server" Width="74px" Text="Frec Inspecion:" Height="17px" ></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txt_Frecuencia_Inspe" AutoPostBack="false" runat="server" Width="60px" MaxLength="3"></asp:TextBox>
                    </td>   
                    <br />                                          
                </tr>
                
            </table>
    </asp:Panel>
             <table>
        <tr>
            <td>
                <asp:Button ID="btn_Insertar_DetaMeta" runat="server" Text="Guardar"
                    BackColor="#1C5AB6" ForeColor="White"
                    BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                     OnClick="btn_Insertar_DetaMeta_Click" />
            </td>
            <td>
                         <asp:Button ID="btn_CancelarDetaMeta" runat="server" Text="Cancelar"
                            BackColor="#1C5AB6" ForeColor="White"
                            BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" OnClick="btn_CancelarDetaMeta_Click"
                             />
                    </td>
        </tr>
    </table>
      
          

            <asp:GridView ID="Grid_Detalle_Maestro" runat="server" BackColor="White" BorderColor="#999999"
                BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="680px" AllowPaging="True"
                AutoGenerateColumns="False" Font-Size="8pt" Font-Names="arial" Height="16px"
                DataKeyNames="Mpde_Id" OnRowCancelingEdit="Grid_Detalle_Maestro_RowCancelingEdit"
                OnPageIndexChanging="GridView_Maestro_Metas_PageIndexChanging" OnRowEditing="Grid_Detalle_Maestro_RowEditing1"
                OnRowUpdating="Grid_Detalle_Maestro_RowUpdating" OnRowDeleting="Grid_Detalle_Maestro_RowDeleting">

                <%-- <AlternatingRowStyle BackColor="Gainsboro" />--%>
                <Columns>
                    <asp:TemplateField HeaderText="MpdeID" Visible="False">
                        <EditItemTemplate>
                            <asp:TextBox ID="Txt_MpdeID" AutoPostBack="false" runat="server" Text='<%# Bind("Mpde_Id") %>' BorderStyle="None" ReadOnly="True"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label8" runat="server" Text='<%# Bind("Mpde_Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fecha Inicial" HeaderStyle-Width="100px">
                        <EditItemTemplate>
                            <asp:TextBox Width="70px" ID="Txt_FechaIni" AutoPostBack="false" runat="server" Text='<%# Bind("FechIni","{0:d}") %>'></asp:TextBox>
                            <asp:CalendarExtender ID="CalFecIni" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="Txt_FechaIni">
                            </asp:CalendarExtender>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label Width="70px" ID="lblfechIni" runat="server" Text='<%# Bind("FechIni","{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="90px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fecha Final" HeaderStyle-Width="100px">
                        <EditItemTemplate>
                            <asp:TextBox Width="70px" ID="Txt_FechaFin" AutoPostBack="false" runat="server" Text='<%# Bind("Mpde_FechFin","{0:d}") %>'></asp:TextBox>
                            <asp:CalendarExtender ID="CalFecFin" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="Txt_FechaFin">
                            </asp:CalendarExtender>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label Width="70px" ID="lblFechFin" runat="server" Text='<%# Bind("Mpde_FechFin","{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="80px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText=" Operarios" HeaderStyle-Width="50px">
                        <EditItemTemplate>
                            <asp:TextBox Width="50px" ID="Txt_NumeOper" MaxLength="3"  AutoPostBack="false" runat="server" Text='<%# Bind("Mpde_NumOper") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label Width="50px"  MaxLength="3" ID="Label4" runat="server" Text='<%# Bind("Mpde_NumOper") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="30px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unidades" HeaderStyle-Width="100px">
                        <EditItemTemplate>
                            <asp:TextBox Width="100px" MaxLength="7" ID="Txt_Unid" AutoPostBack="false" runat="server" Text='<%# Bind("Mpde_Unidades") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label Width="70px" ID="Labeluni" runat="server" Text='<%# Bind("Mpde_Unidades","{0:0,0}") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="80px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Meta" HeaderStyle-Width="100px">
                        <EditItemTemplate>
                            <asp:TextBox Width="100px" MaxLength="7" AutoPostBack="false" ID="Txt_Meta" runat="server" Text='<%# Bind("Mpde_Meta") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label Width="70px" ID="Label5" runat="server" Text='<%# Bind("Mpde_Meta","{0:0,0}") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="80px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText=" % Calidad " HeaderStyle-Width="80px">
                        <EditItemTemplate>
                            <asp:TextBox Width="50px"  ID="Txt_ToleCali" MaxLength="4" AutoPostBack="false" runat="server" Text='<%# Bind("Mpde_ToleCali") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label Width="50px" ID="Label6" runat="server" Text='<%# Bind("Mpde_ToleCali") %>'>%</asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="80px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pieza" HeaderStyle-Width="50px">
                        <EditItemTemplate>
                            <asp:DropDownList ID="Cbo_TipPza" Width="120px" DataSource="<%# Obtener_TipoPieza() %>" DataTextField="TpoPza_dscrpcion" DataValueField="TpoPza_id" AutoPostBack="false" runat="server" Text='<%# Bind("pieza") %>'>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" Width="120" runat="server" Text='<%# Bind("TpoPza_dscrpcion") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle Width="20px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Frecuencia Inspeccion" HeaderStyle-Width="50px">
                        <EditItemTemplate>
                            <asp:TextBox ID="Txt_FrecCali" Width="50px" runat="server" MaxLength="3" Text='<%# Bind("Mpde_FrInCali") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Mpde_FrInCali") %>'></asp:Label>
                        </ItemTemplate>
                            <HeaderStyle Width="50px"></HeaderStyle>
                        <ItemStyle HorizontalAlign="right" />
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="Editar" ShowEditButton="True" />
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
            </td>
    </tr>
     <tr>
                    <td>
                        <asp:Button ID="btn_Agregar_DetaMeta" runat="server" Text="Agregar Detalle"
                            BackColor="#1C5AB6" ForeColor="White"
                            BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            OnClick="btn_Agregar_DetaMeta_Click" />
                    </td>
                </tr>
</table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
