<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CotizacionesPotenciales.aspx.cs" Inherits="SIO.CotizacionesPotenciales" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <rsweb:ReportViewer ID="ReporteCotPoten" runat="server" BackColor="#C2CEE7" 
                Height="" Width="">
            </rsweb:ReportViewer>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div>
    
    </div>
    </form>
</body>
</html>
