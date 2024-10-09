<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="item_planta_reporte.aspx.cs" Inherits="SIO.item_planta_reporte" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link href="Styles/MaestroItem.css" rel="stylesheet" />
    <link href="Styles/sweetalert.css" rel="stylesheet" />
    <script type="text/javascript" src="Scripts/sweetalert.min.js"></script>
    <script type="text/javascript" src="Scripts/jsMaetsroItemPlanta.js" language="javascript"></script>
<%--        <script type="text/javascript" src="Scripts/jquery-ui.min.js"></script>
    <script type="text/javascript" src="Scripts/gridviewScroll.min.js"></script>--%>
    <script type="text/javascript">
//        jQuery(document).ready(function () {

//            scrollback();
//        });
//        function scrollback() {
//            jQuery('#<%=grdReportPlanta.ClientID%>').gridviewScroll({
//                width: 1135,
//                height: 600,
//                arrowsize: 30,
//                varrowtopimg: "images/arrowvt.png",
//                varrowbottomimg: "images/arrowvb.png",
//                harrowleftimg: "images/arrowhl.png",
//                harrowrightimg: "images/arrowhr.png",
//                IsInUpdatePanel: true
//            });
//        }

        function MensajeError(x) {
            swal("Error!", x, "error");
        }
        function Mensajeaceptar(y, z) {
            sweetAlert(z, y, "success");
        }


        function motivo(numitem) {
            sweetAlert({
                title: "Anular Item Planta",
                text: "Motivo por el cual se anula el item planta N°" + numitem + ":",
                type: "input",
                html: true,
                showCancelButton: true,
                closeOnConfirm: false,
                animation: "slide-from-top",
                inputPlaceholder: "Ingresar el motivo"
            },
                function (inputValue) {
                    if (inputValue === false) {
                        return false;
                    }

                    if (inputValue === "")
                    { sweetAlert.showInputError("Escribir el motivo!"); return false }

                    //console.log(msgmotivo);
                    document.getElementById('<%= text_observdelte.ClientID %>').value = inputValue;
                    var texto = document.getElementById('<%= text_observdelte.ClientID %>');
                    texto.onchange();
                    sweetAlert("Anulado satisfactoriamente!", "Item planta N°" + numitem + " por el motivo: \n" + inputValue, "success");

                });
        }

    </script>

      <style type="text/css">
          .style24
          {
              width: 350px;
          }
      </style>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server" >
     <asp:UpdateProgress ID="UpdateProgress2" runat="server"   AssociatedUpdatePanelID="Upanel2">
                        <ProgressTemplate>
                            <div class="style23">
                                &nbsp;<asp:Image ID="Image1" runat="server" Height="16px" 
                                    ImageUrl="~/Imagenes/Indicator.gif" Width="16px" />
                                <span class="style22">&nbsp;Cargando....</span></div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
        <asp:UpdatePanel ID="Upanel2" runat="server" >
        <ContentTemplate>
            <table width="1000px">
                <tr>
                    <td colspan="6" align="left">
                        <asp:Label ID="lbltitulo" runat="server" Text="ITEM PLANTA--REPORTE" Width="150px" CssClass="MI_titulo"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button runat="server" CssClass="Mi_botones" Text="Regresar" ID="btnRegresar" OnClick="btnRegresar_Click" /></td>
                    <td align="right" colspan="2">
                        <asp:Label Text="Planta:" runat="server" CssClass="MI_label" ID="lblplantabuscar"></asp:Label></td>
                    <td align="left">
                        <asp:ComboBox ID="cboplantaIpbuscar" runat="server"
                            AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" Width="220px"
                            AutoPostBack="false">
                        </asp:ComboBox>
                    </td>
                    <td colspan="4" align="right">
                        <asp:ImageButton runat="server" Width="28px" Height="28px" ImageUrl="Imagenes/find.png" ID="ImgbtnFiltrar" OnClick="ImgbtnFiltrar_Click" ToolTip="Buscar" /></td>
                    <td align="left">
                        <asp:ImageButton runat="server" Width="28px" Height="28px" ImageUrl="Imagenes/edit_clear.png" ID="ImgbtnLimpiar" OnClick="ImgbtnLimpiar_Click" ToolTip="Limpiar" /></td>

                </tr>
                <tr>
                    <td align="left">
                        <asp:Label Text="C&oacutedigo ERP:" runat="server" CssClass="MI_label" ID="lblbuscarCodErp"></asp:Label></td>
                    <td align="left">
                        <asp:TextBox ID="txtbuscarCodErp" runat="server" AutoPostBack="false" Width="180px"></asp:TextBox></td>
                    <td align="left">
                        <asp:Label Text="Item Planta:" runat="server" CssClass="MI_label" ID="lblbuscardescrp"></asp:Label></td>
                    <td align="left">
                        <asp:TextBox ID="txtbuscardescrp" runat="server" Style="text-transform: uppercase" AutoPostBack="false" Width="180px"></asp:TextBox></td>
                    <td align="left">
                        <asp:Label Text="Grupo:" runat="server" CssClass="MI_label" ID="lblgrupobuscar"></asp:Label></td>
                    <td align="left" class="style24">
                        <asp:ComboBox ID="cbogrupoIpbuscar" runat="server" AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" Width="180px" AutoPostBack="false">
                        </asp:ComboBox>
                    </td>
                    <td align="left">
                        <asp:Label Text="Estado:" runat="server" CssClass="MI_label" ID="lblEstadoBuscar"></asp:Label></td>
                    <td align="left" class="style24">
                        <asp:ComboBox ID="cboEstadoBuscar" runat="server" AutoCompleteMode="SuggestAppend" DropDownStyle="DropDownList" Width="180px" AutoPostBack="false">
                        </asp:ComboBox>
                    </td>
                </tr>
                  <tr>
                      <td align="left">
                        <asp:Label Text="Activo:" runat="server" CssClass="MI_label" ID="Label1"></asp:Label></td>
                    <td align="left" class="style24">
                        <asp:DropDownList ID="cboActivo" runat="server" AutoPostBack="false">
                             <asp:ListItem Text="Seleccione el Estado"></asp:ListItem>
                            <asp:ListItem Text="SI"></asp:ListItem>
                            <asp:ListItem Text="NO"></asp:ListItem>
                        </asp:DropDownList>
                         <td align="left">
                            <asp:Label Text="Disp_Comercial:" runat="server" CssClass="MI_label" ID="Label2"></asp:Label>
                        </td>
                        <td align="left" class="style24">
                             <asp:CheckBox ID="ChkDispComercial" runat="server" />
                        </td>
                    
                    </td>
                </tr>
            </table>
            <asp:TextBox ID="text_observdelte" runat="server" OnTextChanged="text_observdelte_TextChanged" AutoPostBack="true" Style="display: none;"></asp:TextBox>
            <div style="overflow-x: scroll;height: 100%; width: 100%;">
                         <asp:GridView runat="server" ID="grdReportPlanta" BackColor="White" BorderColor="#999999"   PageSize="30" AllowPaging="true" 
                                            BorderStyle="None" BorderWidth="1px" CellPadding="1" GridLines="Vertical" OnPageIndexChanging="grdReportPlanta_PageIndexChanging" 
                                            AutoGenerateColumns="False" Font-Size="8pt" Font-Names="arial" Height="16px" DataKeyNames="item_planta_id"
                                            OnSelectedIndexChanging="grdReportPlanta_SelectedIndexChanging"  AllowSorting="true" OnSorting="grdReportPlanta_Sorting" Width="1113">
                                            <AlternatingRowStyle BackColor="Gainsboro" />
                <AlternatingRowStyle BackColor="Gainsboro" />
                <Columns>
                    <asp:TemplateField HeaderText="N°">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%#Container.DataItemIndex + 1%>' ID="lblnumitemplanta"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblitem_planta_id" runat="server" Text='<%# Eval("item_planta_id")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblnum_perfil" runat="server" Text='<%# Eval("num_perfil")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Grupo" 
                  DataField="grupo_des" SortExpression="grupo_des">
                </asp:BoundField>
                     <asp:BoundField  HeaderText="C&oacutedigo<br/>ERP" SortExpression="cod_erp" DataField="cod_erp" HtmlEncode="false"/>
                    <asp:BoundField  HeaderText="Item Planta" SortExpression="itemplanta_desc" DataField="itemplanta_desc"/>
                    <asp:BoundField HeaderText="Origen" 
                  DataField="origen_desc" SortExpression="origen_desc" HtmlEncode="false">
                </asp:BoundField>
                    <asp:TemplateField HeaderText="Planta_id" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblplanta_id" runat="server" Text='<%# Eval("planta_id")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Planta" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblplanta_descripcion" runat="server" Text='<%# Eval("planta_descripcion")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:BoundField HeaderText="Cot" 
                  DataField="disp_cotizacion"  HtmlEncode="false" ItemStyle-HorizontalAlign="Center" ControlStyle-Font-Size="9px">
                </asp:BoundField>
                <asp:BoundField HeaderText="Com" 
                  DataField="disp_comercial"  HtmlEncode="false" ItemStyle-HorizontalAlign="Center" ControlStyle-Font-Size="9px">
                </asp:BoundField>
                    <asp:BoundField HeaderText="Ing" 
                  DataField="disp_ingenieria"  HtmlEncode="false" ItemStyle-HorizontalAlign="Center" ControlStyle-Font-Size="9px">
                </asp:BoundField>
                    <asp:BoundField HeaderText="Alm" 
                  DataField="disp_almacen"  HtmlEncode="false" ItemStyle-HorizontalAlign="Center" ControlStyle-Font-Size="9px">
                </asp:BoundField>
                     <asp:BoundField HeaderText="Pn" 
                  DataField="disp_produccion"  HtmlEncode="false" ItemStyle-HorizontalAlign="Center" ControlStyle-Font-Size="9px">
                </asp:BoundField>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblorigen" runat="server" Text='<%# Eval("origen")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Precio<br/>Pleno" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblpleno" runat="server" Text='<%# Eval("Pleno")%>'></asp:Label>
                        </ItemTemplate>
                         <ItemStyle  HorizontalAlign="Right"/>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Precio<br/>Distribuidor" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblDistribuidor" runat="server" Text='<%# Eval("Distribuidor")%>'></asp:Label>
                        </ItemTemplate>
                         <ItemStyle  HorizontalAlign="Right"/>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Precio<br/>Filial 1" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblFilial1" runat="server" Text='<%# Eval("Filial1")%>'></asp:Label>
                        </ItemTemplate>
                         <ItemStyle  HorizontalAlign="Right"/>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Precio<br/>Filial 2" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblFilial2" runat="server" Text='<%# Eval("Filial2")%>'></asp:Label>
                        </ItemTemplate>
                         <ItemStyle  HorizontalAlign="Right"/>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Estado" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblestadoid" runat="server" Text='<%# Eval("estado_id")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:BoundField HeaderText="Estado" 
                  DataField="estado_desc" SortExpression="estado_desc" HtmlEncode="false">
                </asp:BoundField>
                    <asp:BoundField HeaderText="Usuario" Visible="false"
                  DataField="usuario" SortExpression="usuario" HtmlEncode="false">
                </asp:BoundField>
                      <asp:TemplateField HeaderText="Usuario" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblusuario" runat="server" Text='<%# Eval("usuario")%>'></asp:Label>
                        </ItemTemplate>
                          <ItemStyle HorizontalAlign="Left" Font-Size="9px" />
                    </asp:TemplateField>
                   
                    <asp:TemplateField HeaderText="Editar">
                        <ItemTemplate>
                            <asp:LinkButton ID="LkverItem_planta" runat="server" CommandName="Select" CausesValidation="false">
                                <asp:Image ID="Imagerite" ImageUrl="Imagenes/write.png" ImageAlign="Middle" runat="server" ToolTip="Editar" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="Lkocultar" runat="server" CommandName="Delete" CausesValidation="false" Visible="false">
                                <asp:Image ID="Imagestop" ImageUrl="Imagenes/stop.png" ImageAlign="Middle" Width="24px" Height="24px" runat="server" ToolTip="Anular" />
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                       <asp:BoundField DataField="activo" HeaderText="Activo" ItemStyle-HorizontalAlign="Center"  SortExpression="activo" Visible="false"/>
                         <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lkactivo" runat="server"  CausesValidation="false" OnClick="lkactivo_Click" Text="Activar">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="lkinactivo" runat="server"  CausesValidation="false" OnClick="lkinactivo_Click" Text="Inactivar">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                             </asp:TemplateField>
                               <asp:TemplateField HeaderText="Fecha Solicitud" Visible="true" SortExpression="fechaSolicitud">
                        <ItemTemplate>
                            <asp:Label ID="lblFechSoli" runat="server" Text='<%# Eval("fechaSolicitud","{0:d}")%>'></asp:Label>
                        </ItemTemplate>
                         <ItemStyle  HorizontalAlign="Center"/>
                    </asp:TemplateField>
                             <asp:TemplateField HeaderText="Fecha Creación" Visible="true" SortExpression="fechaCreacion">
                        <ItemTemplate>
                            <asp:Label ID="lblFechCrea" runat="server" Text='<%# Eval("fechaCreacion","{0:d}")%>'></asp:Label>
                        </ItemTemplate>
                         <ItemStyle  HorizontalAlign="Center"/>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Horas" Visible="true">
                        <ItemTemplate>
                            <asp:Label ID="lblDias" runat="server" Text='<%# Eval("horas")%>'></asp:Label>
                        </ItemTemplate>
                         <ItemStyle  HorizontalAlign="Center"/>
                    </asp:TemplateField>
                </Columns>
               <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
               <HeaderStyle BackColor="#1C5AB6" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
               <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
              <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
              <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
              <SortedAscendingCellStyle BackColor="#F1F1F1" />
              <SortedAscendingHeaderStyle BackColor="#0000A9" />
              <SortedDescendingCellStyle BackColor="#CAC9C9" />
              <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView>  
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


