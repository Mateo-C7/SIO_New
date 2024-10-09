using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIO
{
    public partial class VideoCrm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // verifico si es un usuario de tipo cliente directo como MRV
            if (Convert.ToUInt32(Session["IdClienteUsuario"]) > 0) Response.Redirect("Home.aspx");
        }
    }
}