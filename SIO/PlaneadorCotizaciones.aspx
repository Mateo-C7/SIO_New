<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="PlaneadorCotizaciones.aspx.cs" Inherits="SIO.PlaneadorCotizaciones" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <style type="text/css">
        .style14
        {
            width: 83%;
        }
        .style15
        {
            width: 58%;
        }       
    .style19
    {
        width: 100%;
    }
    .style20
    {
        width: 90px;
    }
    .style21
    {
        width: 116px;
    }
        .style22
        {
            width: 81px;
        }
        .style23
        {
        }
        .auto-style2 {
            height: 24px;
        }
        .auto-style3 {
            width: 74%;
            height: 85px;
        }
        .auto-style14 {
            width: 95%;
        }
        .auto-style16 {
            height: 27px;
            width: 118px;
        }
        .auto-style18 {
            width: 236px;
            height: 27px;
        }
        .auto-style21 {
            width: 372px;
            height: 27px;
        }
        .auto-style24 {
            width: 126px;
            height: 27px;
            text-align: right;
        }
        .auto-style29 {
            height: 27px;
            text-align: right;
        }
        .auto-style30 {
            text-align: right;
        }
        .auto-style33 {
            height: 27px;
            width: 57px;
        }
        .auto-style34 {
            width: 372px;
            height: 20px;
        }
        .auto-style35 {
            width: 126px;
            text-align: right;
            height: 20px;
        }
        .auto-style36 {
            height: 20px;
        }
        .auto-style37 {
            width: 57px;
            height: 20px;
        }
        .auto-style41 {
            width: 118px;
        }
        .auto-style42 {
            width: 57px;
        }
        .auto-style43 {
            width: 236px;
        }
        .auto-style46 {
            width: 372px;
        }
        .auto-style47 {
            width: 126px;
            text-align: right;
        }
        .auto-style48 {
            width: 126px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    
                    <td class="style23">
                        <asp:Panel ID="pnlProgramador" runat="server" Visible="False">
                            <table class="auto-style14">

                                <tr>
                                    <td class="auto-style46">
                                        <asp:Label ID="lblFUP" runat="server" Font-Names="Arial" Font-Size="8pt" Text="FUP" Width="40px"></asp:Label>
                                        <asp:TextBox ID="txtFUP" runat="server" AutoPostBack="True" Font-Names="Arial" Font-Size="8pt" ontextchanged="txtFUP_TextChanged" Width="80px" BackColor="#FFFF66"></asp:TextBox>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnNuevo" runat="server" BackColor="#1C5AB6" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White" OnClick="btnNuevo_Click" Text="Nuevo" Width="70px" />
                                    </td>
                                    <td class="auto-style47">
                                        <asp:Label ID="lblRespSTV" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Responsable STV" Width="100px"></asp:Label>
                                    </td>
                                    <td class="auto-style43">
                                        <asp:DropDownList ID="cboSTV" runat="server" CssClass="auto-style12" Font-Names="Arial" Font-Size="8pt" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="auto-style30">
                                        <asp:Label ID="lblHE" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Cant Modulaciones" Width="110px"></asp:Label>
                                    </td>


                                    <td class="auto-style41">
                                        <asp:TextBox ID="txtModulaciones" runat="server" Font-Names="Arial" Font-Size="8pt" style="text-align: center" Width="40px">0</asp:TextBox>
                                    </td>


                                    <td class="auto-style42">&nbsp;</td>


                               </tr>
                                 <tr>
                                    <td class="auto-style46">
                                        <asp:Label ID="lblVersion" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Versión" Width="40px"></asp:Label>
                                        <asp:DropDownList ID="cboVer" runat="server" Font-Names="Arial" Font-Size="8pt" onselectedindexchanged="cboVer_SelectedIndexChanged" Width="40px">
                                        </asp:DropDownList>
                                        <asp:Label ID="LTipo" runat="server" Font-Italic="True" Font-Names="Arial" Font-Size="8pt" ForeColor="#000066" Width="83px">Sin</asp:Label>
                                       </td>
                                    <td class="auto-style47">
                                        <asp:Label ID="lblRespCotizador" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Responsable Cotización" Width="119px"></asp:Label>
                                    </td>
                                    <td class="auto-style43">
                                        <asp:DropDownList ID="cboCotizador" runat="server" Font-Names="Arial" Font-Size="8pt" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="auto-style30">
                                        <asp:Label ID="lblCantRec" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Cant Recursos" Width="80px"></asp:Label>
                                    </td>
                                     <td class="auto-style41">
                                         <asp:TextBox ID="txtCantRec" runat="server" Font-Names="Arial" Font-Size="8pt" style="text-align: center" Width="40px">1</asp:TextBox>
                                     </td>
                                     <td class="auto-style42">&nbsp;</td>
                               </tr>
                                 <tr>
                                    <td class="auto-style21">
                                        
                                        <asp:Label ID="lblEstado" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Estado" Width="40px"></asp:Label>
                                        <asp:DropDownList ID="cboEstado" runat="server" Font-Names="Arial" Font-Size="8pt" Width="100px">
                                           <asp:ListItem>Pendiente</asp:ListItem>
                                           <asp:ListItem>Programado</asp:ListItem>                                            
                                        </asp:DropDownList>
                                        
                                    <td class="auto-style24">
                                        <asp:Label ID="lblFechaIni" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha Inicial" Width="100px"></asp:Label>
                                    </td>
                                    <td class="auto-style18">
                                        <asp:TextBox ID="txtFechaIni" runat="server" Font-Names="Arial" Font-Size="8pt" Width="60px"></asp:TextBox>
                                        <asp:CalendarExtender ID="txtFechaIni_CalendarExtender" runat="server" Enabled="True" Format="yyyy-MM-dd" TargetControlID="txtFechaIni">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td class="auto-style29">
                                        <asp:Label ID="lblFechaProg" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha Entrega" Width="100px"></asp:Label>
                                    </td>
                                        <td class="auto-style16">
                                            <asp:TextBox ID="txtFechaProg" runat="server" Font-Names="Arial" OnTextChanged="txtFechaProg_TextChanged"  Font-Size="8pt"  Width="60px" AutoPostBack="True"></asp:TextBox>
                                            <asp:CalendarExtender ID="txtFechaProg_CalendarExtender" runat="server" Enabled="True"  Format="yyyy-MM-dd"  TargetControlID="txtFechaProg">
                                            </asp:CalendarExtender>
                                        </td>
                                        <td class="auto-style33">&nbsp;</td>
                               </tr>
                                <tr>
                                    <td class="auto-style34"><td class="auto-style35">
                                        <asp:Label ID="lblFechaIni0" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Fecha Segun Politica" Width="114px"></asp:Label>
                                        </td>
                                        <td colspan="3" class="auto-style36">
                                            <asp:Label ID="lblfechapolitica" runat="server" Font-Names="Arial" Font-Size="10pt" Text="" Width="100px"></asp:Label>
                                            <asp:Label ID="lblSuperaPolitica" runat="server" Font-Names="Arial" Font-Size="9pt" Width="268px" BackColor="#FF3300" ForeColor="White"></asp:Label>
                                        </td>
                                    </td>
                                    <td class="auto-style37"></td>
                                </tr>
                                <tr>
                                    <td class="auto-style46">
                                        <asp:Label ID="lblCliente" runat="server" Font-Names="Arial" Font-Size="10pt" Width="286px" ></asp:Label>
                                        <asp:Label ID="lblobra" runat="server" Font-Names="Arial" Font-Size="10pt" Width="335px"></asp:Label>
                                        <td class="auto-style47">
                                        <asp:Label ID="lblFechaIni1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Observaciones" Width="100px"></asp:Label>
                                        </td>
                                    </td>
                                    <td colspan="4">
                                        <asp:TextBox ID="txtObservaciones" runat="server" Font-Names="Arial" Font-Size="8pt" Height="35px" TextMode="MultiLine" Width="471px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style46">
                                        &nbsp;<td class="auto-style47">
                                            <asp:Label ID="lblProceso" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Proceso Responsable" Width="113px" Visible="False"></asp:Label>
                                        </td>
                                        <td colspan="4">
                                            <asp:DropDownList ID="cboProceso" runat="server" Font-Names="Arial" Font-Size="8pt" Width="200px" Visible="False">
                                            </asp:DropDownList>
                                        </td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style46">
                                        <asp:Label ID="lblClasificacion" runat="server" Font-Names="Arial" Font-Size="10pt" Width="340px"></asp:Label>
                                        <td class="auto-style48">&nbsp;</td>
                                        <td colspan="3">
                                            <asp:Button ID="btnGuardar" runat="server" BackColor="#1C5AB6" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White" onclick="btnGuardar_Click" Text="Guardar" Width="96px" />
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnReprogramar0" runat="server" BackColor="#1C5AB6" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White" onclick="btnReprogramar_Click" Text="Reprogramar" visible="false" Width="16px" />
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnDarBaja" runat="server" BackColor="#1C5AB6" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White" onclick="btnDarBaja_Click" Text="Dar De Baja" Width="80px" />
                                            &nbsp;&nbsp;
                                            <asp:Button ID="btnVerReporte" runat="server" BackColor="#1C5AB6" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White" onclick="btnVerReporte_Click" Text="Ver Planeador" Width="90px" />
                                            &nbsp;</td>
                                    </td>
                                    <td class="auto-style42">&nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
             <table>
                 <tr>
                     <td>
                         <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                             <ContentTemplate>
                                 <table class="style9">
                                     <tr>
                                         <td class="style13">
                                             <rsweb:ReportViewer ID="ReportViewer1" runat="server" AsyncRendering="False" 
                                                 BackColor="#EBEBEB" BorderColor="#EBEBEB" Height="1500px" 
                                                 Width="1150px">
                                             </rsweb:ReportViewer>
                                         </td>
                                     </tr>
                                     <tr>
                                         <td>
                                             &nbsp;</td>
                                     </tr>
                                 </table>
                             </ContentTemplate>
                         </asp:UpdatePanel>
                     </td>
                 </tr>
            </table>
             <br />
        </ContentTemplate>
    </asp:UpdatePanel>
   
            </asp:Content>
