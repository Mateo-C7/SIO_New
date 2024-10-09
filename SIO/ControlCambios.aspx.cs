using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrillaExample
{
    public partial class ControlCambios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string fup = (string)Session["Fup"];
                Fup.Value = fup.ToString();

                string Nombre = (string)Session["Nombre_Usuario"];
                User.Value = Nombre.ToString();

                string Mensaje = (string)Session["Mensaje"];
                if (Mensaje == "1")
                {
                    string mensaje = "El tema se encuentra cerrado";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
            }
        }
    }
}