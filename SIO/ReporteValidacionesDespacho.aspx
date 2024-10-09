<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true"
    CodeBehind="ReporteValidacionesDespacho.aspx.cs" Inherits="SIO.ReporteValidacionesDespacho" %>
    <%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"  namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
<table>
<tr><td style="text-align: left">
                                          <asp:ComboBox ID="cboPlanta" runat="server" 
                                             DropDownStyle="DropDownList" Font-Names="Arial" 
                                            Font-Size="8pt" Width="80px"
                                            AutoCompleteMode="SuggestAppend">
                                        </asp:ComboBox>
                                    </td>
<td class="style249">
    <asp:TextBox ID="txtFechaIni" runat="server" Font-Names="Arial" 
        Font-Size="8pt" style="margin-left: 0px" TabIndex="9" Width="80px"></asp:TextBox>
    <asp:CalendarExtender ID="txtFechaIni_CalendarExtender" runat="server" 
        Format="yyyy/MM/dd" TargetControlID="txtFechaIni">
    </asp:CalendarExtender>
</td>
<td class="style249">
    <asp:TextBox ID="txtFechaFin" runat="server" Font-Names="Arial" 
        Font-Size="8pt" style="margin-left: 0px" TabIndex="9" Width="80px"></asp:TextBox>
    <asp:CalendarExtender ID="txtFechaFin_CalendarExtender" runat="server" 
        Format="yyyy/MM/dd" TargetControlID="txtFechaFin">
    </asp:CalendarExtender>
</td>
<td><asp:Button ID="btnConsultar" runat="server" BackColor="#1C5AB6" 
                                        BorderColor="#999999" CssClass="botonsio" 
        Font-Bold="True" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="White"  
                                        
         TabIndex="15" 
                                        Text="Consultar" Width="70px" onclick="btnConsultar_Click" 
         />
    
</td>
<td><asp:Button ID="btnGuardar" runat="server" BackColor="#1C5AB6" visible = "True"
                                        BorderColor="#999999" CssClass="botonsio" 
        Font-Bold="True" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="White"  
                                        
        onclientclick="return confirm('Desea Enviar')" TabIndex="15" 
                                        Text="Enviar Planeacion" Width="110px" 
        onclick="btnGuardar_Click" />&nbsp;</td>
</tr>
</table>
    <table class="style9" __designer:mapid="208ab">
        <tr __designer:mapid="208ac">
            <td __designer:mapid="208ad">
                <rsweb:ReportViewer ID="ReportDespValid" runat="server" Width="1150px" BackColor="#EBEBEB"
                    BorderColor="#EBEBEB" Height="1500px" AsyncRendering="False" ShowParameterPrompts="False">
                </rsweb:ReportViewer>
            </td>
        </tr>
    </table>
</asp:Content>
