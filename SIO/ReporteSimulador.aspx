<%@ Page Language="C#" MasterPageFile="~/General.Master"  AutoEventWireup="true" CodeBehind="ReporteSimulador.aspx.cs" Inherits="SIO.ReporteSimulador" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder4" runat="server">
    <script src="https://npmcdn.com/es6-promise@3.2.1"></script>
    <script type="text/javascript" src="scripts/powerbi.js"></script>

    <script type="text/javascript">

        //This code is for sample purposes only.

        //Configure IFrame for the Report after you have an Access Token. See Default.aspx.cs to learn how to get an Access Token
        window.onload = function () {
            var accessToken = document.getElementById('ContentPlaceHolder4_accessToken').value;

            if (!accessToken || accessToken == "")
            {
                return;
            }

            var embedUrl = document.getElementById('ContentPlaceHolder4_txtEmbedUrl').value;
            var reportId = document.getElementById('ContentPlaceHolder4_txtReportId').value;
            var Orden =  [document.getElementById('ContentPlaceHolder4_txtOrdenCot').value];

            const FiltroOrden = {
                $schema: "http://powerbi.com/product/schema#basic",
                target: {
                    table: "SimuladorProyectos",
                    column: "Orden"
                },
                operator: "In",
                values: Orden
            }

            var config = {
                type: 'report',
                accessToken: accessToken,
                embedUrl: embedUrl,
                filters: [FiltroOrden],
                id: reportId,
                settings: {
                    filterPaneEnabled: false,
                    navContentPaneEnabled: false
                }
            };

            // Grab the reference to the div HTML element that will host the report.
            var reportContainer = document.getElementById('reportContainer');

            // Embed the report and display it within the div container.
            var report = powerbi.embed(reportContainer, config);

        };
    </script>

    <asp:HiddenField ID="accessToken" runat="server" />
    <asp:HiddenField ID="txtReportId" runat="server" />
    <asp:HiddenField ID="txtEmbedUrl" runat="server" />
    <asp:HiddenField ID="txtOrdenCot" runat="server" />

    <div>
        <h3></h3>
        <asp:Button ID="getReportButton" Visible="false" runat="server" OnClick="getReportButton_Click" Text="Get Report" />
    </div>

    <div class="field">
        <asp:Textbox ID="txtReportName"  runat="server" Width="750px"></asp:Textbox>
    </div>

    <div class="error">
        <asp:Label ID="errorLabel" runat="server"></asp:Label>
    </div>
    <div>
        <br />
        <div ID="reportContainer" style="width: 1050px; height: 650px"></div>
    </div>

    <div>
        Log View
        <br />
        <div ID="logView" style="width: 880px;"></div>
    </div>
</asp:Content>
