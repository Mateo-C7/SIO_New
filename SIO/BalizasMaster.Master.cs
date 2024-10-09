using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Net;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using CapaControl;

namespace SIO
{
    public partial class BalizasMaster : System.Web.UI.MasterPage
    {
        public SqlDataReader reader = null;
        public ControlInicio controlInicio = new ControlInicio();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                string pagina = "";
                pagina = Request.Url.Segments[Request.Url.Segments.Length - 1];
                controlInicio.Guardar_Log_Sesion(0, Convert.ToString(Session["Usuario"]), pagina);

                // CARGANDO MENU Y SUBMENU DINAMICAMENTE
                MenuItem item = new MenuItem();
                MenuItem subitem = new MenuItem();
                subitem.NavigateUrl = "Estibas.aspx";
                item.ChildItems.Add(subitem);

            }
        }
    }
}
             