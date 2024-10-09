<%@ Page Title="" Language="C#"  MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="PruebaExc.aspx.cs" Inherits="SIO.PruebaExc" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>

<table>

<tr>



<td>

    &nbsp;</td>

</tr>

<tr>

<td></td>

<td>

    &nbsp;</td>
<td>

<asp:Button ID="btnGuardar" runat="server" BackColor="#1C5AB6" 
                                        BorderColor="#999999" CssClass="botonsio" Font-Bold="True" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="White"  onclick="enviar_Click"
                                         TabIndex="15" 
                                        Text="Guardar" Width="80px" />   
<asp:TextBox    ID="txt_to" runat="server"></asp:TextBox>
<asp:TextBox    ID="txt_msg" runat="server" AutoPostBack="True" 
        ontextchanged="txt_msg_TextChanged"></asp:TextBox>
<asp:Label ID="proceso"   runat="server" Text="Label"></asp:Label>

</td>

</tr>

</table>

</ContentTemplate>
</asp:UpdatePanel>

</asp:Content>
