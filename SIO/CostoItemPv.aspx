<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" 
    CodeBehind="CostoItemPv.aspx.cs" Inherits="SIO.CostoItemPv" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"  namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
     <link href="Styles/StyleGeneral.css" rel="stylesheet" type="text/css" />   
    <style type="text/css">
hr {
    display: block;
    margin-top: 0.5em;
    margin-bottom: 0.5em;
    margin-left: 4px;
    margin-right: 0px;
    border-style: inset;
    border-width: 2px;
    color: blue;
    width: 900px;
}
        .auto-style2 {
            height: 29px;
        }
        .auto-style3 {
            width: 864px;
        }
        .auto-style4 {
            height: 29px;
            width: 119px;
        }
        .auto-style5 {
            width: 119px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">     
   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 878px">
                <tr>
                    <td class="auto-style3">
                         <asp:Panel ID="Panel4" runat="server" Font-Names="Arial" Width="871px" BorderStyle="None">
                            <table style="width: 864px">
                                <tr valign="middle">
                                     <td class="auto-style2">
                                         <asp:Label ID="Label1" runat="server" Text="Planta: "></asp:Label>
                                         <asp:DropDownList ID="cboPlanta" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cboPlanta_SelectedIndexChanged"></asp:DropDownList>
                                     </td>
                                     <td class="auto-style2">
                                         <asp:Label ID="Label2" runat="server" Text="Año: "></asp:Label>
                                         <asp:DropDownList ID="cboAnio" runat="server" Height="16px" Width="65px" AutoPostBack="True" OnSelectedIndexChanged="cboAnio_SelectedIndexChanged"></asp:DropDownList>
                                     </td>
                                    <td class="auto-style4">
                                         <asp:Label ID="Label3" runat="server" Text="Trimestre: "></asp:Label>
                                         <asp:DropDownList ID="cboTrimestre" runat="server" Width="39px" Height="17px" OnSelectedIndexChanged="cboTrimestre_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                     </td>
                                    <td class="auto-style2">
                                        <asp:FileUpload ID="FileUpload31" enable="false" runat="server" accept=".csv, application/vnd.openxmlformats-officedocument.spreadsheetml.sheet, application/vnd.ms-excel" size="60px" Width="282px" />
                                    </td>
                                    <td class="auto-style2">
                                        <asp:Button ID="btnValidador1" runat="server"  BackColor="#F1F2F3" ForeColor="Black"
                                                        BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                           onClientClick="return fnConfirmDelete();" OnClick="btnValidador_Click1" Text="Cargar" Height="22px" Width="69px" />
                                    </td>
                                </tr>
                                <tr >
                                    <td></td>
                                    <td></td>
                                    <td class="auto-style5"></td>
                                    <td>
                                        <asp:Label ID="lblEstado" runat="server" Visible ="False" BackColor ="Green" ForeColor="White" Text="Label" Height="16px" Width="139px"></asp:Label></td>
                                    <td>
                                        
                                         <asp:Button ID="btnConfirmaCosto" runat="server"  BackColor="#1C5AB6" ForeColor="White" Visible ="false"
                                                        BorderColor="#999999" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                                           onClientClick="return fnConfirmCosto();"  Text="Confirmar Costo" Height="22px" Width="106px" OnClick="btnConfirmaCosto_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    <td>
                 </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            CellPadding="4" DataKeyNames="CodErp" ForeColor="Black" GridLines="Vertical" 
                            Style="text-align: left" Width="893px" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField DataField="Planta" HeaderText="Planta"   >
                                <HeaderStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Center" />
                                <ItemStyle Font-Names="Arial" Font-Size="8pt" ForeColor="#666666" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Anio" HeaderText="Año" >
                                <HeaderStyle Font-Names="Arial" Font-Size="8pt" />
                                <ItemStyle Font-Names="Arial" Font-Size="8pt" ForeColor="#666666" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Trim" HeaderText="Trim" >
                                <HeaderStyle HorizontalAlign="Center" Font-Names="Arial" Font-Size="8pt" />
                                <ItemStyle HorizontalAlign="Center" ForeColor="#666666" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CodErp" HeaderText="CodERP" >
                                <HeaderStyle Font-Names="Arial" Font-Size="8pt" />
                                <ItemStyle Font-Names="Arial" Font-Size="8pt" ForeColor="#666666" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NombreItem" HeaderText="Nombre Item" > 
                                <HeaderStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Center" />
                                <ItemStyle Font-Names="Arial" Font-Size="8pt" ForeColor="#666666" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Costo" HeaderText="Costo" DataFormatString="{0:n2}" > 
                                <HeaderStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Center" />
                                <ItemStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Right" ForeColor="#666666" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Observacion" HeaderText="Observacion" > 
                                <HeaderStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Center" />
                                <ItemStyle Font-Names="Arial" Font-Size="8pt" ForeColor="#666666" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Usuario" HeaderText="Usuario" > 
                                <HeaderStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Center" />
                                <ItemStyle Font-Names="Arial" Font-Size="7pt" ForeColor="#666666" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Fecha" HeaderText="Fecha" > 

                                <HeaderStyle Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Center" />
                                <ItemStyle Font-Names="Arial" Font-Size="7pt" ForeColor="#666666" />
                                </asp:BoundField>

                            </Columns>
                            <FooterStyle BackColor="#CCCC99" />
                            <HeaderStyle BackColor="#D1E5FF"  ForeColor="Black" Font-Size = "20px"/>
                            <PagerStyle BackColor="#F7F7DE" HorizontalAlign="Right" ForeColor="Black" />
                            <RowStyle BackColor="#E3EFFF" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FBFBF2" />
                            <SortedAscendingHeaderStyle BackColor="#848384" />
                            <SortedDescendingCellStyle BackColor="#EAEAD3" />
                            <SortedDescendingHeaderStyle BackColor="#575357" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
         </ContentTemplate>            
    </asp:UpdatePanel>
    
     <asp:UpdateProgress ID="UpdateProgres1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="overlay" />
            <div class="overlayContent">
                <asp:Label ID="lblEnviando" runat="server" Text="Cargando..." Font-Names="Arial"
                    Font-Size="14pt"></asp:Label>
                <img src="Imagenes/ajax-loader.gif" alt="Loading" height="30" style="text-align: center"
                    width="30" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

     <script language="javascript" type="text/javascript">                 

       function fnConfirmDelete() {
        
        return confirm("Esta seguro de actualizar los Costos de la Planta, Año y Trimestre Seleccionados?, Recuerde que si existen registros con estos mismos parametros se reemplazaran con Costos del archivo seleccionado!");
       }

       function fnConfirmCosto() {

           return confirm("Esta seguro de Confirmar los Costos de la Planta, Año y Trimestre Seleccionados?, Recuerde que no podra Actualizarlo Nuevamente!");
       }

      </script>
        

</asp:Content>
