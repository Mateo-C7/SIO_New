using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using CapaControl;
using System.Data;
using CapaDatos;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;
using System.Diagnostics;
using System.Threading;

namespace SIO
{
    public partial class VerPedidos : System.Web.UI.Page
    {
        ControlCasino CC = new ControlCasino();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["superUsuario"].ToString() == "True")
                {
                    grdVerPedido.DataSource = CC.cargarGridVerPedidosTodos();
                    grdVerPedido.DataBind();
                }
                else
                {
                    grdVerPedido.DataSource = CC.cargarGridVerPedidos(int.Parse(Session["cedulaEmpCasino"].ToString()));
                    grdVerPedido.DataBind();
                }
            }
        }
        protected void btnSelecionar_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int numPed = int.Parse(this.grdVerPedido.DataKeys[row.RowIndex].Value.ToString());
            Session["numPed"] = numPed.ToString();
            Response.Write("<script>window.close(); opener.location.reload();</script>");
            
        }
        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Session["numPed"] = null;
            Response.Write("<script>window.close(); opener.location.reload();</script>");
        }
    }
}