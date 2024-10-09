<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TableroProduccion.aspx.cs" Inherits="SIO.TableroProduccion" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms,  Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"  namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script>
    addEventListener("load", function () { setTimeout(hideURLbar, 0); }, false);
    function hideURLbar() { window.scrollTo(0, 1); }
    </script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <rsweb:ReportViewer ID="ReporteTablero" runat="server" Height="1500px" Width="" 
            BackColor="#C2CEE7">
        </rsweb:ReportViewer>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    
    </div>
    </form>
</body>
</html>
