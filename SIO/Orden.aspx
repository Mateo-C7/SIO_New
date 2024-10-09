<%@ Page Title="" Language="C#" MasterPageFile="~/Mobil.Master" AutoEventWireup="true" CodeBehind="Orden.aspx.cs" Inherits="SIO.MobilCotizacion" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"  namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <style type="text/css">    
 
          .CustomComboBoxStyle .ajax__combobox_textboxcontainer input 
        {
            background-image:url('http://172.21.0.1/ComercialTester/Imagenes/aqua-bg.gif');
            border-style:none;  
        }
        .CustomComboBoxStyle .ajax__combobox_buttoncontainer button 
        {
            background-image:url('http://172.21.0.1/ComercialTester/Imagenes/aqua-arrow.gif');
            border-style:none;            
        }
        .CustomComboBoxStyle .ajax__combobox_itemlist li
        {
            color: Black;  
            font-size:8pt;  
            font-family:Arial; 
            background-color:#EBEBEB; 
            position: relative; 
        }        
 
        /* Accordion */
        .accordionHeader
        {
            border: 2px Outset #EBEBEB;
            color: white;
            background-color: #1C5AB6;
            font-family: Arial, Sans-Serif;
            font-size: 12px;
            font-weight: bold;
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
        }
 
#master_content .accordionHeader a
{
      color: #FFFFFF;
      background: none;
      text-decoration: none;
}
 
#master_content .accordionHeader a:hover
{
      background: none;
      text-decoration: underline;
}
 
.accordionHeaderSelected
{
    border: 2px Outset #EBEBEB;
    color: white;
    background-color: #1C5AB6;
      font-family: Arial, Sans-Serif;
      font-size: 12px;
      font-weight: bold;
    padding: 5px;
    margin-top: 5px;
    cursor: pointer;
}
 
#master_content .accordionHeaderSelected a
{
      color: #FFFFFF;
      background: none;
      text-decoration: none;
}
 
#master_content .accordionHeaderSelected a:hover
{
      background: none;
      text-decoration: underline;
}
 
.accordionContent
{
    background-color: #EBEBEB;
    border: 0px outset #2F4F4F;
    border-top: none;
    padding: 5px;
    padding-top: 10px;
}

        .lados
        {
            width: 350px;
        }

        @media screen and (min-width:480px) and (max-width:639px) {.lados {width: 250px;}}
        
 
        .centrar
        {
            text-align: right;
        }
 
        .fuente
        {            
            font-Size:8pt;   
            font-family:Arial;
            text-align: justify;
        }
                 
        .derecha
        {
            text-align: right;
        }
                         
     </style>
</asp:Content>

<asp:Content ID="Content3" runat="server"  
    contentplaceholderid="ContentPlaceHolder2">
    <rsweb:ReportViewer ID="ReporteOrden" runat="server" Width="350px" 
                        BackColor="#EBEBEB" BorderColor="#EBEBEB" Height="500px" 
                AsyncRendering="False" ShowBackButton="False" 
        ShowCredentialPrompts="False" ShowDocumentMapButton="False" 
        ShowExportControls="False" ShowFindControls="False" 
        ShowPageNavigationControls="False" ShowToolBar="False" 
        ShowWaitControlCancelLink="False" ShowZoomControl="False" 
        SizeToReportContent="True" ShowPrintButton="False" 
    ShowRefreshButton="False">
            </rsweb:ReportViewer>
                      
</asp:Content>


<asp:Content ID="Content4" runat="server" 
    contentplaceholderid="ContentPlaceHolder1">
        
           
        </asp:Content>









