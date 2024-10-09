<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="prueba.aspx.cs" Inherits="SIO.prueba" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <style type="text/css">
         .watermarked 
        {
            padding:2px 0 0 2px;
            border:1px solid #BEBEBE;
            background-color:white;
            color:Gray;
            font-family:Arial;
            font-weight:lighter;    
        }
        .CustomComboBoxStyle .ajax__combobox_buttoncontainer button 
        {
            background-image:url('Imagenes/toolkit-arrow.png');
            border-style:none;            
        }
        .CustomComboBoxStyle .ajax__combobox_textboxcontainer input 
        {
            background-image:url('Imagenes/toolkit-bg.png');
            border-style:none;  
        }
        .CustomComboBoxStyle .ajax__combobox_itemlist li
        {
            color: Black;  
            font-size:8pt;  
            font-family:Arial; 
            background-color:#EBEBEB;  
        }        
        .A:hover { background:white } 
        .botonsio:hover 
        { 
           color: white; background:blue             
        } 
        .center
        {
            font-family: Arial;
            font-size: 8pt;
            Text-Align:Center; 
        }
        .sangria
        {
            word-spacing: 10pt;
            font-family: Tahoma;
            font-size: 11pt;
            color: #1C5AB6;
        text-align: right;
    }
        .style84
    {
        text-align: right;
        }
               
        .style109
        {
            width: 95px;
            text-align: right;
        }
        .style110
        {
            width: 268435520px;
        }
       
        .style115
        {
            width: 36px;
        }
       
        .style123
        {
            width: 100px;
            text-align: right;
        }
               
        </style>            

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
    <asp:TextBox ID="txt_to" runat="server"></asp:TextBox>
    <asp:TextBox ID="txt_msg" runat="server"></asp:TextBox>
    <asp:Label ID="proceso" runat="server" Text="Label"></asp:Label>
</asp:Content>
