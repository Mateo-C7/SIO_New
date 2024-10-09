<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SubirFoto.aspx.cs" Inherits="SIO.SubirFoto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function Navigate() {
            javascript: window.close("SubirFoto.aspx");
        }
    </script> 
    <style type="text/css">
        .style1
        {
            height: 29px;
        }
        .style2
        {
            width: 239px;
        }
        .style3
        {
            height: 29px;
            width: 239px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table style="width: 340px">
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left" class="style2">
                     <asp:FileUpload ID="FDocument" runat="server" />
                </td>
                <td class="style138">
                    <asp:Label ID="Archivo" runat="server" Font-Names="Arial" Font-Size="8pt" 
                        Text="Fotos" Width="30px" Font-Italic="True" 
                        ForeColor="#0000CC" Visible="False"></asp:Label>
                </td>
            </tr>
                       <tr>
                           <td style="text-align: right" class="style3">
                               <asp:Button ID="btnSubirPlano" runat="server" Font-Names="Arial" 
                                   Font-Size="8pt" onclick="btnSubirPlano_Click" Text="Subir" Width="70px" 
                                   BackColor="#1C5AB6" Font-Bold="True"  />
                               &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                               <asp:Button ID="btnCancelar" runat="server" BackColor="#1C5AB6" 
                                   Font-Bold="True" Font-Names="Arial" Font-Size="8pt"  
                                   Text="Cerrar" Width="70px" onclick="btnCancelar_Click" onclientclick="Navigate()"/>
                           </td>
                           <td class="style1">
                               </td>
                       </tr>
                       <tr>
                           <td>
                           <asp:GridView ID="grvArchivo" runat="server" CellPadding="1" CellSpacing="4" 
                                    ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" 
                                    Font-Names="Arial" Font-Size="8pt" DataKeyNames="id_plano" 
                                    onrowcommand="grvArchivo_RowCommand" >
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Width="15px" ControlStyle-Width="15px">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDeleteFile" runat="server" 
                                                    ImageUrl="~/Imagenes/editdelete.gif" Width="15px" 
                                                    onclientclick="return confirm('Esta seguro de eliminar el archivo?')" 
                                                    CommandArgument="<%#((GridViewRow) Container).RowIndex %>" 
                                                    CommandName="Borrar"/>
                                            </ItemTemplate>
                                            <ControlStyle Width="15px" />
                                            <HeaderStyle Width="15px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="idFoto" Visible="false">
                                           <ItemTemplate>
                                               <asp:Label ID="lblFoto" runat="server" Text='<%# Bind("ID_PLANO") %>'></asp:Label>
                                           </ItemTemplate>
                                           <EditItemTemplate>
                                               <asp:TextBox ID="txtFoto" runat="server"></asp:TextBox>
                                           </EditItemTemplate>
                                       </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Descripción">
                                           <ItemTemplate>
                                           <asp:Label ID="lblDesc" runat="server" Text='<%# Bind("NOMBRE") %>'></asp:Label>
                                                  
                                           </ItemTemplate>
                                           <EditItemTemplate>
                                          
                                           </EditItemTemplate>
                                       </asp:TemplateField>                                       
                                       <asp:TemplateField HeaderText="Ruta Archivo" Visible="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="simpa_anexoEditLink" runat="server" Text='<%# Eval("RUTA") %>' 
                                            OnPreRender="anexo_OnClick" Visible="false"/>
                                        </ItemTemplate>
                                       </asp:TemplateField>
                                   </Columns>
                                   <EditRowStyle BackColor="#999999" />
                                   <FooterStyle BackColor="#5D7B9D" Font-Bold="True"  />
                                   <HeaderStyle BackColor="#5D7B9D" Font-Bold="True"  />
                                   <PagerStyle BackColor="#284775"  HorizontalAlign="Center" />
                                   <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                   <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                   <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                   <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                   <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                   <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                               </asp:GridView>    
                           </td>
                           <td>
                               &nbsp;</td>
                       </tr>
                </table>
    </div>
    </form>
</body>
</html>
