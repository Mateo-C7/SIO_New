<%@ Page Title="Recuperar Contraseña" Language="C#" MasterPageFile="~/Contraseña.Master" AutoEventWireup="true" CodeBehind="PasswordRecovery.aspx.cs" Inherits="SIO.PasswordRecovery" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">  
        .style10
        {
            border-collapse: collapse;
            background-color: #1C5AB6;
            width: 1350px;
        }
        .style11
        {
            width: 726px;
        }
        .style12
        {
            width: 286px;
        }
        .style13
        {
            width: 104px;
        }
        .style14
        {
            width: 100%;
        }
        .style16
        {
            width: 204px;
        }
        .style20
        {
            width: 41px;
        }
        .style22
        {
            font-family: Arial, Helvetica, sans-serif;
            font-weight: bold;
            font-size: small;
            text-align: center;
        }
        .style23
        {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    
 <table class="style10">
        <tr>
            
            <td class="style11" height="30">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;<asp:Label ID="lblContraseña" runat="server" Font-Bold="True" 
                    Font-Names="Arial" Font-Size="10pt"  
                    Text="Recuperar Contraseña"></asp:Label>
            </td>
            
            <td align="right" class="style13">
              &nbsp;
              </td>
            <td class="style12">
                &nbsp;</td>
            <td>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="pnlPassword" runat="server" BorderColor="#999999" 
        Width="941px">
        <table class="style14">
            <tr>
                <td class="style16">
                    &nbsp;</td>
                <td>
                    <br />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style16">
                    &nbsp;</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table class="style14">
                                <tr>
                                    <td class="style20">
                                        &nbsp;</td>
                                    <td>
                                        <br />
                                        <asp:Label ID="lblOlvido" runat="server" Font-Bold="True" Font-Names="Arial" 
                                            Font-Size="10pt" Text="¿Olvidaste la contraseña?"></asp:Label>
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style20">
                                        &nbsp;</td>
                                    <td>
                                        <asp:Label ID="lblDescripcion" runat="server" Font-Names="Arial" 
                                            Font-Size="8pt" style="text-align: justify" 
                                            Text="Escriba su nombre de usuario :"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style20">
                                        &nbsp;</td>
                                    <td>
                                        <asp:TextBox ID="txtNombre" runat="server" AutoPostBack="True" 
                                            BackColor="White" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" 
                                            Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style20">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style20">
                                        &nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnEnviar" runat="server" BackColor="#1C5AB6" 
                                            BorderColor="#1C5AB6" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                            Font-Names="Arial" Font-Size="8pt"  onclick="btnLogin_Click" 
                                            Text="Enviar" Width="80px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style20">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:RoundedCornersExtender ID="UpdatePanel1_RoundedCornersExtender" 
                        runat="server" BorderColor="LightGray" Color="LightGray" Enabled="True" 
                        Radius="6" TargetControlID="UpdatePanel1">
                    </asp:RoundedCornersExtender>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style16">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style16">
                    &nbsp;</td>
                <td>
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <div class="style23">
                                &nbsp;<asp:Image ID="Image1" runat="server" Height="16px" 
                                    ImageUrl="~/Imagenes/Indicator.gif" Width="16px" />
                                <span class="style22">&nbsp;Updating Page...</span></div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>

    </asp:Panel>
   <asp:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" TargetControlID="pnlPassword" Radius="6"
    Corners="All"> </asp:RoundedCornersExtender>
</asp:Content>

