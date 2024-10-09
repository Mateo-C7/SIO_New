using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Net;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


namespace SIO
{
    public partial class VisorReportesPowerBy : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Obtiene el el valor que trae por referencia el modulo
                String url = Request.QueryString["Url"];
                cargarReporte(url);
            }
        }


        private void cargarReporte(string url)
        {
            visorReportesPowerBy.Attributes["src"] = url;
        }

    }
}