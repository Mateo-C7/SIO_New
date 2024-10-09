using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaControl;

namespace SIO
{
    public partial class Grid : System.Web.UI.Page
    {
        public ControlLogistica CL = new ControlLogistica();
        protected void Page_Load(object sender, EventArgs e)
        {
            limpiarCampos();
            mostrarGrid();
            ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "cerrar(); ", true);
        }
        public void mostrarGrid() { 
            String orden = (string)Session["Orden3"];
            String desp = (string)Session["Desp3"];

            if (orden == null && orden == "")
            {
                lblOrden3.Text = "0";
            }
            else {
                lblOrden3.Text = orden;
            }
            /*------------------*/
            if (desp == null && desp == "")
            {
                lblDespacho3.Text = "0";
            }
            else
            {
                lblDespacho3.Text = desp;
            }
            /*------------------*/
            String contS = (string)Session["conte"];
            String contP = (string)Session["conteP"];
            if (contS == null)
            {
            contS = "0";
            int cero = int.Parse(contS);
                if(cero == 0){
                 GridView1.Visible = false;
                 lblContenedor3.Text = "";
                }
            }
            else if (contS != "Contenedor" && orden != "Orden")
            {
                int cont = int.Parse(contS);
                lblContenedor3.Text = contP;
                GridView1.Visible = true;
                GridView1.DataSource = CL.cargaGriedViewAlYACC(cont);
                GridView1.DataBind();
                double suma = GridView1.Rows.Cast<GridViewRow>().Sum(x => Convert.ToDouble(x.Cells[2].Text));
                lblPesoT.Text = Convert.ToString(suma);
            }
            else 
            {
                lblOrden3.Text = "0";
                lblDespacho3.Text = "0";
                lblContenedor3.Text = "0";
                GridView1.Visible = false;
            }
        }
        public void limpiarCampos() {
            lblContenedor3.Text = "0";
            lblDespacho3.Text = "0";
            lblOrden3.Text = "0";
        }

        public void totalPeso(){
        double suma= GridView1.Rows.Cast<GridViewRow>().Sum(x=> Convert.ToDouble(x.Cells[2].Text));
        lblPesoT.Text = Convert.ToString(suma);
        }        
    }
}