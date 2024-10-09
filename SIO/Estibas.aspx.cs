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
    public partial class Estibas : System.Web.UI.Page
    {
        public ControlBalizas CB = new ControlBalizas();
        protected void Page_Load(object sender, EventArgs e)
        { 
            limpiar();
            Session["nomEq"] = Request.QueryString["PC"];
            Session["id"] = Session["nomEq"].ToString().Substring(5, 2);
            cargaBaliza();
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
        }

        public void limpiar() {
            Session["nomEq"] = "";
            Session["id"] = "";
            Session["balizaId"] = "";
            Session["balizaNo"] = "";
            Session["balizaOfaId"] = "";
            Session["balizaTSol"] = "";
            Session["balizaNoSol"] = "";
            Session["balizaEstibaV"] = "";
            Session["balizaEstibaNV"] = "";
            Session["balizaActiva"] = "";
            Session["balizaRegion"] = "";
            Session["balizaCambio"] = "";
            Session["balizaEstValid"] = "";
            Session["balizaEstNoValid"] = "";
            lblNumBal.Text = "";
            lblEstibaV.Text = "";
            lblEstibaNV.Text = "";
            lblOrden.Text = "";
            lblCantV.Text = "";
            lblCantFV.Text = "";
            lblPesoV.Text = "";
            lblCantNV.Text = "";
            lblCantFNV.Text = "";
            lblPesoNV.Text = "";
            lblTotalF.Text = "";
        }

        public void cargaBaliza() {

            buscarEstiba();
            lblNumBal.Text = Session["id"].ToString();
            lblEstibaV.Text = Session["balizaEstibaV"].ToString();
            lblEstibaNV.Text = Session["balizaEstibaNV"].ToString();
            lblOrden.Text = Session["balizaNoSol"].ToString();
            //Tabla Validada
            grdV.DataSource = CB.cargarTablaVal(int.Parse(Session["id"].ToString()), int.Parse(Session["balizaOfaId"].ToString()), int.Parse(Session["balizaEstibaV"].ToString().Substring(5, 4)));
            grdV.DataBind();
            lblCantV.Text = CB.totalP(int.Parse(Session["id"].ToString()), int.Parse(Session["balizaOfaId"].ToString()), int.Parse(Session["balizaEstibaV"].ToString().Substring(5, 4))).ToString();
            lblCantFV.Text = CB.totalF(int.Parse(Session["id"].ToString()), int.Parse(Session["balizaOfaId"].ToString()), int.Parse(Session["balizaEstibaV"].ToString().Substring(5, 4))).ToString();
            lblPesoV.Text = CB.sumasPesos(int.Parse(Session["id"].ToString()), int.Parse(Session["balizaOfaId"].ToString()), int.Parse(Session["balizaEstibaV"].ToString().Substring(5, 4))).ToString();
            //Tabla No Validada
            grdNoV.DataSource = CB.cargarTablaVal(int.Parse(Session["id"].ToString()), int.Parse(Session["balizaOfaId"].ToString()), int.Parse(Session["balizaEstibaNV"].ToString().Substring(5, 4)));
            grdNoV.DataBind();
            lblCantNV.Text = CB.totalP(int.Parse(Session["id"].ToString()), int.Parse(Session["balizaOfaId"].ToString()), int.Parse(Session["balizaEstibaNV"].ToString().Substring(5, 4))).ToString();
            lblCantFNV.Text = CB.totalF(int.Parse(Session["id"].ToString()), int.Parse(Session["balizaOfaId"].ToString()), int.Parse(Session["balizaEstibaNV"].ToString().Substring(5, 4))).ToString();
            lblPesoNV.Text = CB.sumasPesos(int.Parse(Session["id"].ToString()), int.Parse(Session["balizaOfaId"].ToString()), int.Parse(Session["balizaEstibaNV"].ToString().Substring(5, 4))).ToString();
            //Total faltantes
            int bl = int.Parse(Session["id"].ToString());
            lblBorrar2.Text = bl.ToString();
            lblTotalF.Text = CB.totalOrden(int.Parse(Session["balizaOfaId"].ToString())).ToString();
            lblBorrar.Text = Session["id"].ToString();
        }

        public void buscarEstiba()
        {
            String proc = "EXEC ConsultaBaliza_Metro " + Session["id"].ToString() + ", 0 , 0 ," + 2 + "";
            DataTable consulta = BdDatos.CargarTablaProccedure(proc);
            foreach (DataRow row in consulta.Rows)
            {
                Session["balizaId"] = row["balizaId"].ToString();
                Session["balizaNo"] = row["balizaNo"].ToString();
                Session["balizaOfaId"] = row["balizaOfaId"].ToString();
                Session["balizaTSol"] = row["balizaTSol"].ToString();
                Session["balizaNoSol"] = row["balizaNoSol"].ToString();
                Session["balizaEstibaV"] = row["balizaEstibaV"].ToString();
                Session["balizaEstibaNV"] = row["balizaEstibaNV"].ToString();
                Session["balizaActiva"] = row["balizaActiva"].ToString();
                Session["balizaRegion"] = row["balizaRegion"].ToString();
                Session["balizaCambio"] = row["balizaCambio"].ToString();
                Session["balizaEstValid"] = row["balizaEstValid"].ToString();
                Session["balizaEstNoValid"] = row["balizaEstNoValid"].ToString();
            }
          
        }
        
    }
}