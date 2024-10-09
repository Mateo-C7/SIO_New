<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProyectosCerrados.aspx.cs" Inherits="SIO.ProyectosCerrados" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    
        <rsweb:ReportViewer ID="Reporteproyectos" runat="server" Height="1500px" Width="" 
            BackColor="#C2CEE7">
        </rsweb:ReportViewer>
    
    </div>
    </form>
</body>
</html>
