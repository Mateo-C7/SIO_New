<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="ItemReporte.aspx.cs" Inherits="SIO.ItemReporte" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- <script type="text/javascript" src="Scripts/jquery-ui.min.js"></script>
    <script type="text/javascript" src="Scripts/gridviewScroll.min.js"></script>--%>
       <link href="Styles/MaestroItem.css" rel="stylesheet" />
     <link href="Styles/sweetalert.css" rel="stylesheet" />
      <script type="text/javascript" src="Scripts/sweetalert.min.js"></script>
     <script type="text/javascript" language="javascript" >
//         jQuery(document).ready(function () {

//             scrollback();
//         });
//         function scrollback() {
//             jQuery('#<%=grdReporteIa.ClientID%>').gridviewScroll({
//                 width: 850,
//                 height: 600,
//                 arrowsize: 30,
//                 varrowtopimg: "images/arrowvt.png",
//                 varrowbottomimg: "images/arrowvb.png",
//                 harrowleftimg: "images/arrowhl.png",
//                 harrowrightimg: "images/arrowhr.png",
//             });
//         }
         function eliminar(numitem)
         {
             swal({ title: "Anular el Item Forsa", text: "¿Seguro que desea anular el Item Forsa  N°"+numitem+ "?", type: "warning", showCancelButton: true, confirmButtonColor: "#DD6B55", confirmButtonText: "Si, Anular Item Forsa!", closeOnConfirm: false},
               function (isConfirm) {
                   if (isConfirm) {
                       swal("Anulado satisfactoriamente!", "Item Forsa  N°" + numitem, "success"); document.getElementById('<%= txt_delete.ClientID %>').value = "OK";
                       var texto = document.getElementById('<%= txt_delete.ClientID %>');
                       texto.onchange();
                   }
                   else {
                       return false;
                   }
               });
         }
         function MensajeError(x) {
             swal("Error!", x, "error");
         }
         function Mensajeaceptar(y, z) {
             sweetAlert(z, y, "success");
         }
      
     </script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
     <asp:UpdateProgress ID="UpdateProgress1" runat="server"  AssociatedUpdatePanelID="Upanel1">
                        <ProgressTemplate>
                            <div class="style23">
                                &nbsp;<asp:Image ID="Image1" runat="server" Height="16px" 
                                    ImageUrl="~/Imagenes/Indicator.gif" Width="16px" />
                                <span class="style22">&nbsp;Cargando...</span></div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
      <asp:UpdatePanel ID="Upanel1" runat="server" >
          <ContentTemplate>
     <table runat="server">
         <tr>
             <td colspan="6" align="left"> <asp:Label ID="lbltitulo" runat="server" Text="ITEM FORSA--REPORTE" Width="150px" CssClass="MI_titulo"></asp:Label></td>
         </tr>
         <tr>
             <td colspan="6">&nbsp;
             </td>
         </tr>
         <tr>
             <td colspan="6"><asp:Button runat="server" CssClass="Mi_botones" Width="80px" Text="Regresar" ID="btnRegresar" OnClick="btnRegresar_Click"  /></td>
         </tr>
         <tr>
                <td align="left"><asp:Label Text="Item Forsa:" runat="server" CssClass="MI_label" ID="lblitemforsa"></asp:Label></td>
                <td align="left" ><asp:TextBox runat="server"  ID="txtfinditem"  Style="text-transform: uppercase"  Width="294px" onkeydown="return (event.keyCode!=13);" ></asp:TextBox></td>
               <td align="left" ><asp:Label Text="Grupo:" runat="server" CssClass="MI_label" ID="lblgrupo"></asp:Label></td>
                <td align="left"><asp:ComboBox runat="server" ID="cbofindgrupo" AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" Width="220px" CausesValidation="false"></asp:ComboBox></td>
                <td align="left"><asp:ImageButton runat="server" Width="28px" Height="28px" ImageUrl="Imagenes/find.png" ID="ImgbtnFiltrar"  OnClick="ImgbtnFiltrar_Click" ToolTip="Buscar"/></td>
             <td align="left"><asp:ImageButton runat="server" Width="28px" Height="28px" ImageUrl="Imagenes/edit_clear.png" ID="ImgbtnLimpiar" OnClick="ImgbtnLimpiar_Click"   ToolTip="Limpiar"/>
                    <asp:TextBox ID="txt_delete" runat="server" OnTextChanged="txt_delete_TextChanged" AutoPostBack="true" Style="display: none;"></asp:TextBox></td>
         </tr>
            <tr>
                <td colspan="6">
                      <asp:GridView ID="grdReporteIa" runat="server"  Width="100%"  BackColor="White" BorderColor="#999999" CellPadding="1" GridLines="Vertical"  Font-Size="9pt" Font-Names="arial" Height="16px" 
                                            AutoGenerateColumns="False" DataKeyNames="item_id"  OnSelectedIndexChanging="grdReporteIa_SelectedIndexChanging"  AllowSorting="true" OnSorting="grdReporteIa_Sorting" >
                   <AlternatingRowStyle BackColor="Gainsboro" />
                <AlternatingRowStyle BackColor="Gainsboro" />       
                    <Columns>
                        <asp:BoundField   DataField="item_id" SortExpression="item_id" Visible="false"/>
                        <asp:TemplateField HeaderText="N°">
                                <ItemTemplate>
                                <asp:label id="lblReporteIa" runat="server" style="text-align:center" text='<%#Container.DataItemIndex + 1%>'></asp:label>
                                 </ItemTemplate>
                                   <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField> 
                        <asp:BoundField DataField="Descripcion" HeaderText="Item Forsa" HtmlEncode="false" SortExpression="Descripcion"  ItemStyle-wrap="true"  />
                        <asp:BoundField DataField="nombre_grupo" SortExpression="nombre_grupo" HeaderText="Grupo" HtmlEncode="false"  />
                        <asp:TemplateField HeaderText="Editar">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LkverItem_planta" runat="server" CommandName="Select" CausesValidation="false">
                                            <asp:Image ID="Imagerite" ImageUrl="Imagenes/write.png" ImageAlign="Middle" runat="server" ToolTip="Editar" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="activo" HeaderText="Activo" ItemStyle-HorizontalAlign="Center" SortExpression="activo" />
                         <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lkactivo" runat="server"  CausesValidation="false" OnClick="lkactivo_Click" Text="Activar">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lkinactivo" runat="server"  CausesValidation="false" OnClick="lkinactivo_Click" Text="Inactivar">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                             </asp:TemplateField>
                        </Columns>     
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
               <HeaderStyle BackColor="#1C5AB6" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"/>
               <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
              <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
              <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
              <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                        <SortedAscendingHeaderStyle BackColor="#506C8C"/>
                                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                       </asp:GridView>
                </td>
            </tr>
        </table>
              </ContentTemplate>
          </asp:UpdatePanel>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    if (e.get_error() != null) {
                        var ex = e.get_error();
                        var mesg = "HttpStatusCode: " + ex.httpStatusCode;
                        mesg += "\n\nName: " + ex.name;
                        mesg += "\n\nMessage: " + ex.message;
                        mesg += "\n\nDescription: " + ex.description;
                        alert(mesg);
                        e.set_errorHandled(true);
                    }
                }
            });
        };
</script>
</asp:Content>
