<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="ClienteLiteCargue.aspx.cs" Inherits="SIO.Prueba" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
                                                          <asp:LinkButton ID="lkrapidisimo" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                        
        OnClick="lkrapidisimo_Click1">Rapidisimo</asp:LinkButton>
    <asp:FileUpload ID="fileuploadExcel" runat="server" />

<asp:Button ID="btnSend" runat="server" Text="Export" onclick="btnSend_Click"  />

<asp:GridView ID="GridView1" runat="server" Visible="False">

</asp:GridView>
<table style="width: 800px; font-family: Arial; vertical-align: middle; text-align: center; border-bottom: 1px solid grey;" border="0" cellspacing="0" cellpadding="7">
    <tbody>
        <tr>
            <th style="background-color: #555; color: #fff; text-align: center; font-weight: bold; font-size: 1.1em; width: 120px; text-transform: uppercase;">Facturado</th>
            <th style="background-color: #555; color: #fff; text-align: center; font-weight: bold; font-size: 1.1em; width: 120px; text-transform: uppercase;">Fecha</th>
            <th style="background-color: #555; color: #fff; text-align: center; font-weight: bold; font-size: 1.1em; width: 80px; text-transform: uppercase;">Horas</th>
            <th style="background-color: #555; color: #fff; text-align: center; font-weight: bold; font-size: 1.1em; text-transform: uppercase;">Tarea realizada</th>
        </tr>
        <tr>
            <td style="background-color: #fafdf0; border-width: 1px; border-style: inset; border-color: gray; border-bottom: none;"></td>
            <td style="border-width: 1px; border-style: inset; border-color: gray; border-left: none; border-bottom: none;">13/11/12</td>
            <td style="border-width: 1px; border-style: inset; border-color: gray; border-left: none; border-bottom: none; background-color: #eafffb; font-weight: bold;"></td>
            <td style="border-width: 1px; border-style: inset; border-color: gray; border-left: none; border-bottom: none; text-align: left;"></td>
        </tr>
        <tr>
            <td style="background-color: #fafdf0; border-width: 1px; border-style: inset; border-color: gray; border-bottom: none;"></td>
            <td style="border-width: 1px; border-style: inset; border-color: gray; border-left: none; border-bottom: none;">13/11/12</td>
            <td style="border-width: 1px; border-style: inset; border-color: gray; border-left: none; border-bottom: none; background-color: #eafffb; font-weight: bold;"></td>
            <td style="border-width: 1px; border-style: inset; border-color: gray; border-left: none; border-bottom: none; text-align: left;"></td>
        </tr>
    </tbody>
</table>

    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    </asp:Content>
