<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Productividad.aspx.cs" Inherits="SIO.Productividad" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"  namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body content="width=device-width, initial-scale=1.0">
    <form id="form1" runat="server" content="width=device-width, initial-scale=1.0">
    <div>
    
        <rsweb:ReportViewer ID="ReporteProductividad" runat="server"  Width=""  Height = "800px"
            BackColor="#C2CEE7">
        </rsweb:ReportViewer>
    
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    </form>
</body>
</html>
