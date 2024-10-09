using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text;

namespace SIO
{

    public static class Helper
    {

        /// <summary>

        /// Registers the redirect on session end script.

        /// </summary>

        /// <param name="page">The page.</param>

        public static void RegisterRedirectOnSessionEndScript(this Page page)
        {

            /// Login Page, We can retrieve for configuration file (Web.Config)

            string loginPage = "index.aspx";

            /// Session Timeout (Default 20 minutes)

            int sessionTimeout = HttpContext.Current.Session.Timeout;

            /// Timeout for Redirect to Login Page (10 milliseconds before)

            int redirectTimeout = (sessionTimeout * 60000) - 10;



            /// JavaScript Code

            StringBuilder javascript = new StringBuilder();

            javascript.Append("var redirectTimeout;");

            javascript.Append("clearTimeout(redirectTimeout);");

            javascript.Append(String.Format("setTimeout(\"window.location.href='{0}'\",{1});", loginPage, redirectTimeout));



            /// Register JavaScript Code on WebPage

            page.ClientScript.RegisterStartupScript(page.GetType(),

                                                    "RegisterRedirectOnSessionEndScript",

                                                    javascript.ToString(),

                                                    true);

        }

    }
}


