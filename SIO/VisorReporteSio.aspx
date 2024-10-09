<%@ Page Title="" Language="C#" MasterPageFile="~/General.Master" AutoEventWireup="true" CodeBehind="VisorReporteSio.aspx.cs" Inherits="SIO.VisorReporteSio" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
            namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
<%--	<div class="row">
        <input name="b_print" type="button" onclick="printdiv();" value="Print Report" />
    </div>--%>
	<div class="row" id="Reporte">
        <asp:UpdatePanel ID="UpdatePreportes" runat="server" ><ContentTemplate>
            <div runat="server" id ="divPlanta"  style="overflow:auto;" >
                 <content>
                        <rsweb:ReportViewer ID="RViewer" runat="server"  Width="100%"  Height="920">
                        </rsweb:ReportViewer>
                </content>
             </div>
            </ContentTemplate>
        </asp:UpdatePanel>
     </div>            
</asp:Content>
