using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIO
{
    public partial class Acta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             if (!IsPostBack)
                {
                     int arRol = (int)Session["Rol"];
            
                    if ((arRol == 9) || (arRol == 30) || (arRol == 2))
                    {
                        this.frame1.Visible = true;
                    }
                    else
                    {
                        this.frame1.Visible = false;
                    }
                
                }
                 
            
        }
    }
}