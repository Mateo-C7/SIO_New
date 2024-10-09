using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaControl;

namespace SIO
{
    public partial class General2 : System.Web.UI.MasterPage
    {
        public ControlInicio controlInicio = new ControlInicio();
        protected void Page_Load(object sender, EventArgs e)
        {
            string pagina = "";
            pagina = Request.Url.Segments[Request.Url.Segments.Length - 1];
            controlInicio.Guardar_Log_Sesion(0, Convert.ToString(Session["Usuario"]), pagina);

        }
    }
}