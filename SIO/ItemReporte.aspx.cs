using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaControl;
using System.Diagnostics;
using System.Data.SqlClient;


namespace SIO
{
    public partial class ItemReporte : System.Web.UI.Page
    {
        ControlMaestroItemPlanta cmip = new ControlMaestroItemPlanta();
        SqlDataReader reader = null;
         string srch_word;
         //static string prevPage = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                
                Session["grupo"] = "";
                Session["item"] = "";
                CargarReporte();
                CargarGrupo();
                //prevPage = Request.UrlReferrer.ToString();
            }

        }

private void CargarReporte()
{
 	 DataTable Tabla_fill = new DataTable();
           Tabla_fill = cmip.reporteItem();
           grdReporteIa.DataSource = Tabla_fill;
           grdReporteIa.DataBind();
           Session["dtreporte"] = Tabla_fill;
           //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "scroll", "scrollback()", true);
}

private void CargarGrupo()
{
            cbofindgrupo.Items.Clear();
            reader = cmip.PoblarGrupo();
            cbofindgrupo.Items.Add(new ListItem("Seleccione un Grupo", ""));
            while (reader.Read())
            {
                cbofindgrupo.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
            }
            cbofindgrupo.SelectedIndex = 0;
            reader.Close();
            reader.Dispose();
            cmip.CerrarConexion();
}
     
        

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
           
            Response.Redirect("MaestroItemPlanta.aspx?id=" + Session["item_id"].ToString());
            //Response.Write("<script>window.close();</script>");
        }

       
        protected void grdReporteIa_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
              string id = grdReporteIa.DataKeys[e.NewSelectedIndex].Value.ToString();
              ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "salir", "cerrar()", true);
              Response.Redirect("MaestroItemPlanta.aspx?id=" + id);
              e.NewSelectedIndex = -1; 
        }

        protected void txt_delete_TextChanged(object sender, EventArgs e)
        {
            if(txt_delete.Text.Equals("OK"))
            {
                if (Session["id"] != null)
                {
                    if (Session["id_delete"].ToString().Equals(Session["id"].ToString()))
                    {
                        Session["id"] = null;
                    }
                }
               
                string msg = cmip.editarActivoItem(Convert.ToInt64((String)Session["id_delete"]),false);
                if (msg.Equals("OK"))
                { Debug.WriteLine("Se actualizo estado en la tabla item"); }
                else { Debug.WriteLine(msg); }
                //Insertar en bitacora_itemplanta_rel_estado
                string mensaje1 = cmip.InsertarBitacoraItemEstado(Convert.ToInt64((String)Session["id_delete"]), 7, Session["usuario"].ToString());
                if (mensaje1.Equals("OK"))
                { Debug.WriteLine("Se inserto con exito en la tabla bitacora_itemp_rel_estado"); }
                else { Debug.WriteLine(mensaje1); }
                CargarReporte();
                Reload_reporte();
                txt_delete.Text = "";
            }
           
        }
        private string buscarespeciales(string p)
        {

            if (p.Contains("'"))
            {
                p = p.Replace("'", "''");
            }
            return p;
        }


      
        private void Reload_reporte()
        {
            grdReporteIa.DataSource = Session["dtreporte"] as DataTable;
            grdReporteIa.DataBind();
            DataTable dtreporte = Session["dtreporte"] as DataTable;
            DataView dv = dtreporte.DefaultView;
            String cadena = "", item = Session["item"].ToString(), grupo = Session["grupo"].ToString();
            if (!item.Equals(""))
            {
                cadena = "Descripcion LIKE '%" + item + "%'";
                
            }
            if (!grupo.Equals(""))
            {
                if (!cadena.Equals(""))
                {

                    cadena += " AND nombre_grupo LIKE '%" + grupo + "%'";
                }
                else
                {
                    cadena = "nombre_grupo LIKE '%" + grupo + "%'";
                }

            }
            if (!cadena.Equals(""))
            {
                dv.RowFilter = cadena;
                Session["dtreporte"] = dv.ToTable();
            }
            grdReporteIa.DataSource = Session["dtreporte"] as DataTable;
            grdReporteIa.DataBind();
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "scroll", "scrollback()", true);
        }

     
        protected void ImgbtnLimpiar_Click(object sender, ImageClickEventArgs e)
        {
            CargarReporte();
            cbofindgrupo.SelectedIndex = 0;
            Session["grupo"] = "";
            Session["item"] = "";
            txtfinditem.Text = "";
        }

        protected void ImgbtnFiltrar_Click(object sender, ImageClickEventArgs e)
        {
            CargarReporte();
            if (!txtfinditem.Text.Equals(""))
            {

                srch_word = txtfinditem.Text.Trim().ToUpper();
                srch_word = buscarespeciales(srch_word);
                Session["item"] = srch_word;
               
            }
            if (txtfinditem.Text.Equals(""))
            {
                Session["item"] = "";
                
            }
            if (cbofindgrupo.SelectedIndex != 0)
            {
                Session["grupo"] = cbofindgrupo.SelectedItem.Text.Trim().ToUpper();
              
            }
            if (cbofindgrupo.SelectedIndex == 0)
            {

                Session["grupo"] = "";
                
            }
            Reload_reporte();
        }


        protected void grdReporteIa_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dtreporte = Session["dtreporte"] as DataTable;
            if (dtreporte != null)
            {

              
                dtreporte.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                Session["dtreporte"] = dtreporte;
                Reload_reporte();
            }
        }
        private string GetSortDirection(string column)
        {
          
            string sortDirection = "ASC";

          
            string sortExpression = ViewState["SortExpression"] as string;

            if (sortExpression != null)
            {
                
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }

         
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }

        protected void lkactivo_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            String id = grdReporteIa.DataKeys[grdrow.RowIndex].Value.ToString();
            Session["id_delete"] = id;
            String lblconsecutivo = ((Label)grdReporteIa.Rows[grdrow.RowIndex].FindControl("lblReporteIa")).Text;
                String descripcion = grdReporteIa.Rows[grdrow.RowIndex].Cells[2].Text;
                descripcion = buscarespeciales(descripcion);
                    string msg = cmip.editarActivoItem(Convert.ToInt64(id), true);
                    if (msg.Equals("OK"))
                    { Debug.WriteLine("Se actualizo estado en la tabla item"); }
                    else { Debug.WriteLine(msg); }
                    //Insertar en bitacora_itemplanta_rel_estado
                    string mensaje1 = cmip.InsertarBitacoraItemEstado(Convert.ToInt64(id), 8, Session["usuario"].ToString());
                    if (mensaje1.Equals("OK"))
                    { Debug.WriteLine("Se inserto con exito en la tabla bitacora_itemp_rel_estado"); }
                    else { Debug.WriteLine(mensaje1); }
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alerta", "Mensajeaceptar( 'Item planta N°" + lblconsecutivo + "!','Activado')", true);
                    CargarReporte();
                    Reload_reporte();
             
    
        }

        protected void lkinactivo_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            String id = grdReporteIa.DataKeys[grdrow.RowIndex].Value.ToString();
            Session["id_delete"] = id;
            String lblconsecutivo = ((Label)grdReporteIa.Rows[grdrow.RowIndex].FindControl("lblReporteIa")).Text;
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "llamado", "eliminar(" + lblconsecutivo + ")", true);
            Reload_reporte();
        }


 

        }
    }
