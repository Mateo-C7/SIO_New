using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.PowerBI.Api.V2;
using Microsoft.PowerBI.Api.V2.Models;
using Microsoft.Rest;
using System.Threading.Tasks;

namespace SIO
{
    public partial class ReporteSimulador : System.Web.UI.Page
    {
        string baseUri = "https://api.powerbi.com/v1.0/myorg/";

        protected void Page_Load(object sender, EventArgs e)
        {

            txtOrdenCot.Value = Session["ORDENCOT"].ToString(); 

                //After you get an AccessToken, you can call Power BI API operations such as Get Report
                Session["AccessToken"] = GetAccessToken(
                    "",
                    "a64353dd-547f-4fdb-8a0f-5e3820b8bc84",
                    "jVx5L3Q6CG+RJuwaBMdpwNNCmUyXpmyI6PBhFa1eN5M=",
                    "http://localhost:13526/");


            if (Session["AccessToken"] != null)
            {
                accessToken.Value = Session["AccessToken"].ToString();
                GetReport();
            }
        }

        protected void getReportButton_Click(object sender, EventArgs e)
        {
            var aaa = GetAccessToken(
                "",
                "a64353dd-547f-4fdb-8a0f-5e3820b8bc84",
                "jVx5L3Q6CG+RJuwaBMdpwNNCmUyXpmyI6PBhFa1eN5M=",
                "http://localhost:13526/");

            Session["AccessToken"] = aaa;

            GetReport();
        }


        // Gets a report based on the setting's ReportId and WorkspaceId.
        // If reportId or WorkspaceId are empty, it will get the first user's report.
        protected void GetReport()
        {
            var WorkspaceId = "54d77209-fb95-4784-a151-2898dd597d7e";
            var reportId = "7b28ef89-c95d-4ba2-a1f2-fb8e31e0c14f";

            var powerBiApiUrl = "https://api.powerbi.com/";

            using (var client = new PowerBIClient(new Uri(powerBiApiUrl), new TokenCredentials(accessToken.Value, "Bearer")))
            {
                Report report;

                // Settings' workspace ID is not empty
                if (!string.IsNullOrEmpty(WorkspaceId))
                {
                    // Gets a report from the workspace.
                    report = GetReportFromWorkspace(client, WorkspaceId, reportId);
                }
                // Settings' report and workspace Ids are empty, retrieves the user's first report.
                else if (string.IsNullOrEmpty(reportId))
                {
                    report = client.Reports.GetReports().Value.FirstOrDefault();
                    AppendErrorIfReportNull(report, "No reports found. Please specify the target report ID and workspace in the applications settings.");
                }
                // Settings contains report ID. (no workspace ID)
                else
                {
                    report = client.Reports.GetReports().Value.FirstOrDefault(r => r.Id == reportId);
                    AppendErrorIfReportNull(report, string.Format("Report with ID: '{0}' not found. Please check the report ID. For reports within a workspace with a workspace ID, add the workspace ID to the application's settings", reportId));
                }

                if (report != null)
                {
                    txtEmbedUrl.Value = report.EmbedUrl;
                    txtReportId.Value = report.Id;
                    txtReportName.Text = report.Name + " - No Orden Cotizacion : " + txtOrdenCot.Value;
                }
            }
        }

        public void GetAuthorizationCode()
        {
            var @params = new NameValueCollection
            {
                //Azure AD will return an authorization code. 
                {"response_type", "code"},

                //Client ID is used by the application to identify themselves to the users that they are requesting permissions from. 
                //You get the client id when you register your Azure app.
                {"client_id", "a64353dd-547f-4fdb-8a0f-5e3820b8bc84"},

                //Resource uri to the Power BI resource to be authorized
                //The resource uri is hard-coded for sample purposes
                {"resource", "https://analysis.windows.net/powerbi/api"},

                //After app authenticates, Azure AD will redirect back to the web app. In this sample, Azure AD redirects back
                //to Default page (Default.aspx).
                { "redirect_uri", "http://localhost:13526/"}
            };

            //Create sign-in query string
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString.Add(@params);

            Response.Redirect(String.Format("https://login.windows.net/common/oauth2/authorize/" + "?{0}", queryString));
        }

        public string GetAccessToken(string authorizationCode, string applicationID, string applicationSecret, string redirectUri)
        {

            // Create a user password cradentials.
            var credential = new UserPasswordCredential("monitoreo@forsa.net.co", "Those7953");

            // Authenticate using created credentials
            var authenticationContext = new AuthenticationContext("https://login.windows.net/common/oauth2/authorize/");
            var authenticationResult = authenticationContext.AcquireTokenAsync("https://analysis.windows.net/powerbi/api", applicationID, credential);

            if (authenticationResult == null)
            {
                return null;
            }

            return authenticationResult.Result.AccessToken;

        }

        // Gets the report with the specified ID from the workspace. If report ID is emty it will retrieve the first report from the workspace.
        private Report GetReportFromWorkspace(PowerBIClient client, string WorkspaceId, string reportId)
        {
            // Gets the workspace by WorkspaceId.
            var workspaces = client.Groups.GetGroups();
            var sourceWorkspace = workspaces.Value.FirstOrDefault(g => g.Id == WorkspaceId);

            // No workspace with the workspace ID was found.
            if (sourceWorkspace == null)
            {
                errorLabel.Text = string.Format("Workspace with id: '{0}' not found. Please validate the provided workspace ID.", WorkspaceId);
                return null;
            }

            Report report = null;
            if (string.IsNullOrEmpty(reportId))
            {
                // Get the first report in the workspace.
                report = client.Reports.GetReportsInGroup(sourceWorkspace.Id).Value.FirstOrDefault();
                AppendErrorIfReportNull(report, "Workspace doesn't contain any reports.");
            }

            else
            {
                try
                {
                    // retrieve a report by the workspace ID and report ID.
                    report = client.Reports.GetReportInGroup(WorkspaceId, reportId);
                }

                catch (HttpOperationException)
                {
                    errorLabel.Text = string.Format("Report with ID: '{0}' not found in the workspace with ID: '{1}', Please check the report ID.", reportId, WorkspaceId);

                }
            }

            return report;
        }

        private void AppendErrorIfReportNull(Report report, string errorMessage)
        {
            if (report == null)
            {
                errorLabel.Text = errorMessage;
            }
        }
    }
}
