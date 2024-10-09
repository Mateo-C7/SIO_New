<%@ Page Title="" Language="C#" MasterPageFile="~/GeneralGrande.Master" AutoEventWireup="true" CodeBehind="Acta.aspx.cs" Inherits="SIO.Acta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<IFRAME id="frame1" src="http://mail.forsa.com.co/acta/Acta.aspx" scrolling="auto"  
        runat="server" frameborder="0" height="800" width="1150">> 

</IFRAME> 
</asp:Content>
<asp:Content ID="Content4" runat="server" 
    contentplaceholderid="ContentPlaceHolder2">
                
             <table class="style1" bgcolor="#1C5AB6" frame="border">
                 <tr>
                     <td style="background-color: #FFFFFF" class="style2">
                         <asp:ImageButton ID="logoHome" runat="server" Height="33px" 
                             ImageUrl="~/Imagenes/SIO.jpg" PostBackUrl="~/Home.aspx" Width="103px" />
                     </td>
                     <td style="border: 1px solid #1C5AB6; background-color: #1C5AB6; " 
                class="style6">
                         &nbsp;</td>
                     <td style="border: 1px solid #1C5AB6; background-color: #1C5AB6" 
                class="style8">
                         &nbsp;</td>
                 </tr>
             </table>
                
        </asp:Content>

