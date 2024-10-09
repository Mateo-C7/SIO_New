using CapaControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace SIO
{
    public partial class item_planta_reporte : System.Web.UI.Page
    {
        ControlMaestroItemPlanta cmIp = new ControlMaestroItemPlanta();
        SqlDataReader reader = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                txtbuscarCodErp.Attributes.Add("onkeypress", "return valideKeyenteros(event,this);");
                cbogrupoIpbuscar.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
                cboplantaIpbuscar.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
                txtbuscarCodErp.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
                txtbuscardescrp.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
                cboEstadoBuscar.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
                CargarGrupo();
                CargarPlanta();
                CargarEstado();            
            }

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 6000;
        }

        private void CargarPlanta()
        {
            cboplantaIpbuscar.Items.Clear();
            reader = cmIp.PoblarPlanta(Session["Usuario"].ToString());
            cboplantaIpbuscar.Items.Add(new ListItem("Seleccione la Planta", "0"));
            while (reader.Read())
            {
                cboplantaIpbuscar.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
            }
            cboplantaIpbuscar.SelectedIndex = 0;
            reader.Close();
            reader.Dispose();
            cmIp.CerrarConexion();
        }

        /**************SE CARGA EL ITEM GRUPO*********************/
        private void CargarGrupo()
        {
            cbogrupoIpbuscar.Items.Clear();
            reader = cmIp.PoblarGrupo();
            cbogrupoIpbuscar.Items.Add(new ListItem("Seleccione el Grupo", " "));
            while (reader.Read())
            {
                cbogrupoIpbuscar.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
            }
            cbogrupoIpbuscar.SelectedIndex = 0;
            reader.Close();
            reader.Dispose();
            cmIp.CerrarConexion();

        }

        /**************SE CARGA EL ITEM ESTADO*********************/
        private void CargarEstado()
        {
            cboEstadoBuscar.Items.Clear();
            reader = cmIp.PoblarEstado();
            cboEstadoBuscar.Items.Add(new ListItem("Seleccione el Estado", " "));
            while (reader.Read())
            {
                cboEstadoBuscar.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
            }
            cboEstadoBuscar.SelectedIndex = 0;
            reader.Close();
            reader.Dispose();
            cmIp.CerrarConexion();

        }
        private void CargarReporte()
        {
            string mensaje = "";
            if (Session["perfil_usu"] != null) 
            {
                if (!String.IsNullOrEmpty(cboplantaIpbuscar.SelectedValue) && cboplantaIpbuscar.SelectedValue != "0")
                {
                    string perfil = Session["perfil_usu"].ToString();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("N°");
                    dt.Columns.Add("item_planta_id");
                    dt.Columns.Add("num_perfil");
                    dt.Columns.Add("grupo_des");
                    dt.Columns.Add("cod_erp");
                    dt.Columns.Add("itemplanta_desc");
                    dt.Columns.Add("origen_desc");
                    dt.Columns.Add("planta_id");
                    dt.Columns.Add("planta_descripcion");
                    dt.Columns.Add("disp_cotizacion");
                    dt.Columns.Add("disp_comercial");
                    dt.Columns.Add("disp_ingenieria");
                    dt.Columns.Add("disp_almacen");
                    dt.Columns.Add("disp_produccion");
                    dt.Columns.Add("origen");
                    dt.Columns.Add("Pleno");
                    dt.Columns.Add("Distribuidor");
                    dt.Columns.Add("Filial1");
                    dt.Columns.Add("Filial2");
                    dt.Columns.Add("estado_id");
                    dt.Columns.Add("estado_desc");
                    dt.Columns.Add("usuario");
                    dt.Columns.Add("activo");
                    dt.Columns.Add("fechaSolicitud");
                    dt.Columns.Add("fechaCreacion");
                    dt.Columns.Add("horas");
                    int moneda = 0;
                    reader = cmIp.obtenerMonedaPlanta(Convert.ToInt32(cboplantaIpbuscar.SelectedValue));
                    if (reader.HasRows) 
                    {
                        reader.Read();
                        moneda = Convert.ToInt32(reader.GetValue(0));                        
                    }
                    reader.Close();
                    reader.Dispose();
                    cmIp.CerrarConexion();

                    if (moneda != 0)
                    {
                        reader = cmIp.poblarReporteItemPlanta(moneda, Convert.ToInt32(perfil), Convert.ToInt32(cboplantaIpbuscar.SelectedValue));
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                DataRow row = dt.NewRow();
                                row["item_planta_id"] = reader.GetValue(0).ToString();
                                row["num_perfil"] = reader.GetValue(1).ToString();
                                row["grupo_des"] = reader.GetValue(2).ToString();
                                row["cod_erp"] = reader.GetValue(3).ToString();
                                row["itemplanta_desc"] = reader.GetValue(4).ToString();
                                row["origen_desc"] = reader.GetValue(5).ToString();
                                row["planta_id"] = reader.GetValue(6).ToString();
                                row["planta_descripcion"] = reader.GetValue(7).ToString();
                                row["disp_cotizacion"] = reader.GetValue(8).ToString();
                                row["disp_comercial"] = reader.GetValue(9).ToString();
                                row["disp_ingenieria"] = reader.GetValue(10).ToString();
                                row["disp_almacen"] = reader.GetValue(11).ToString();
                                row["disp_produccion"] = reader.GetValue(12).ToString();
                                row["origen"] = reader.GetValue(13).ToString();
                                row["Pleno"] = Convert.ToDecimal(reader.GetValue(14)).ToString("N2", new CultureInfo("en-US"));
                                row["Distribuidor"] = Convert.ToDecimal(reader.GetValue(15)).ToString("N2", new CultureInfo("en-US"));
                                row["Filial1"] = Convert.ToDecimal(reader.GetValue(16)).ToString("N2", new CultureInfo("en-US"));
                                row["Filial2"] = Convert.ToDecimal(reader.GetValue(17)).ToString("N2", new CultureInfo("en-US"));
                                row["estado_id"] = reader.GetValue(18).ToString();
                                row["estado_desc"] = reader.GetValue(19).ToString();
                                row["usuario"] = reader.GetValue(20).ToString();
                                row["activo"] = reader.GetValue(21).ToString();                                
                                string fechaSolicitud = reader.GetValue(22).ToString();
                                if (String.IsNullOrEmpty(fechaSolicitud))
                                {
                                    row["fechaSolicitud"] = reader.GetValue(22).ToString();
                                }
                                else
                                {
                                    row["fechaSolicitud"] = Convert.ToDateTime(reader.GetValue(22)).ToString("yyyy/MM/dd");
                                }

                                string fechaCreacion= reader.GetValue(23).ToString();
                                if (String.IsNullOrEmpty(fechaCreacion))
                                {
                                    row["fechaCreacion"] = reader.GetValue(23).ToString();
                                }
                                else
                                {
                                    row["fechaCreacion"] = Convert.ToDateTime(reader.GetValue(23)).ToString("yyyy/MM/dd");
                                }                                
                                row["horas"] = reader.GetValue(24).ToString();
                                dt.Rows.Add(row);
                            }
                        }
                        grdReportPlanta.Dispose();
                        grdReportPlanta.DataSource = dt;
                        grdReportPlanta.DataBind();
                        Session["TbReporte"] = dt;
                        reader.Close();
                        reader.Dispose();
                        cmIp.CerrarConexion();

                        if (perfil.Equals("2"))
                        {
                            dt.Columns["Pleno"].ReadOnly = false;
                            dt.Columns["Pleno"].MaxLength = 100;
                            dt.Columns["Distribuidor"].ReadOnly = false;
                            dt.Columns["Distribuidor"].MaxLength = 100;
                            dt.Columns["Filial1"].ReadOnly = false;
                            dt.Columns["Filial1"].MaxLength = 100;
                            dt.Columns["Filial2"].ReadOnly = false;
                            dt.Columns["Filial2"].MaxLength = 100;                        
                        }

                        else if (perfil.Equals("3"))
                        {
                            grdReportPlanta.Columns[21].Visible = true;
                        }

                        else if (perfil.Equals("1"))
                        {
                            dt.Columns["Pleno"].ReadOnly = false;
                            dt.Columns["Pleno"].MaxLength = 100;
                            dt.Columns["Distribuidor"].ReadOnly = false;
                            dt.Columns["Distribuidor"].MaxLength = 100;
                            dt.Columns["Filial1"].ReadOnly = false;
                            dt.Columns["Filial1"].MaxLength = 100;
                            dt.Columns["Filial2"].ReadOnly = false;
                            dt.Columns["Filial2"].MaxLength = 100;

                            grdReportPlanta.Columns[15].Visible = true;
                            grdReportPlanta.Columns[16].Visible = true;
                            grdReportPlanta.Columns[17].Visible = true;
                            grdReportPlanta.Columns[18].Visible = true;
                            grdReportPlanta.Columns[21].Visible = true;
                            grdReportPlanta.Columns[24].Visible = true;
                            grdReportPlanta.Columns[25].Visible = true;                         
                        }
                    }
                    else
                    {
                        Session["TbReporte"] = "";
                        mensaje = "Verifique que la planta escogida tenga una moneda asociada. Gracias!";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    }  
                }
                else
                {
                    Session["TbReporte"] = "";
                    mensaje = "Debe seleccionar alguna planta para cargar el reporte. Gracias!";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }            
            }
            else
            {
                Session["TbReporte"] = "";
                mensaje = "Usuario no logueado. Por favor inicie sesión nuevamente. Gracias!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }

            //DataTable Tabla_fill = new DataTable();
            //DataTable Tabla_fill1 = new DataTable();
            //DataTable Precio = new DataTable();
            //DataTable Precio2 = new DataTable();
            //int i = 0, j = 0;
            //String perfil = "0";
            //if (Session["perfil_usu"] != null)
            //{
            //    //perfil = Session["perfil_usu"].ToString();
            //    //if (perfil.Equals("2"))
            //    //{
            //    //    Tabla_fill = cmIp.PoblarReporteUsuario(Session["Usuario"].ToString(), Convert.ToInt32(perfil));
            //    //    // se llena con los precios para los usuarios solicitantes
            //    //    Precio = cmIp.PoblarUsuarioPrecio(Session["Usuario"].ToString(), Convert.ToInt32(perfil));
            //    //    Tabla_fill.Columns["Pleno"].ReadOnly = false;
            //    //    Tabla_fill.Columns["Pleno"].MaxLength = 100;
            //    //    Tabla_fill.Columns["Distribuidor"].ReadOnly = false;
            //    //    Tabla_fill.Columns["Distribuidor"].MaxLength = 100;
            //    //    Tabla_fill.Columns["Filial1"].ReadOnly = false;
            //    //    Tabla_fill.Columns["Filial1"].MaxLength = 100;
            //    //    Tabla_fill.Columns["Filial2"].ReadOnly = false;
            //    //    Tabla_fill.Columns["Filial2"].MaxLength = 100;
            //    //    foreach (DataRow r in Precio.Rows)
            //    //    {
            //    //        Tabla_fill.Rows[i]["Pleno"] = Convert.ToDecimal(r["Pleno"]).ToString("N2", new CultureInfo("en-US"));
            //    //        Tabla_fill.Rows[i]["Distribuidor"] = Convert.ToDecimal(r["Distribuidor"]).ToString("N2", new CultureInfo("en-US"));
            //    //        Tabla_fill.Rows[i]["Filial1"] = Convert.ToDecimal(r["Filial1"]).ToString("N2", new CultureInfo("en-US"));
            //    //        Tabla_fill.Rows[i]["Filial2"] = Convert.ToDecimal(r["Filial2"]).ToString("N2", new CultureInfo("en-US"));
            //    //        i++;
            //    //    }

            //    }

            //    if (perfil.Equals("3"))
            //    {
            //        Tabla_fill.Clear();
            //        Tabla_fill = cmIp.PoblarReporteUsuarioPreAprobador();
            //        grdReportPlanta.Columns[21].Visible = true;
            //    }


            //    if (perfil.Equals("1"))
            //    {
            //        Tabla_fill.Clear();
            //        Tabla_fill = cmIp.PoblarReporteUsuario(Session["Usuario"].ToString(), Convert.ToInt32(perfil));
            //        // se llena con los precios para los usuarios solicitantes
            //        Precio = cmIp.PoblarUsuarioPrecio(Session["Usuario"].ToString(), Convert.ToInt32(perfil));
            //        Tabla_fill.Columns["Pleno"].ReadOnly = false;
            //        Tabla_fill.Columns["Pleno"].MaxLength = 100;
            //        Tabla_fill.Columns["Distribuidor"].ReadOnly = false;
            //        Tabla_fill.Columns["Distribuidor"].MaxLength = 100;
            //        Tabla_fill.Columns["Filial1"].ReadOnly = false;
            //        Tabla_fill.Columns["Filial1"].MaxLength = 100;
            //        Tabla_fill.Columns["Filial2"].ReadOnly = false;
            //        Tabla_fill.Columns["Filial2"].MaxLength = 100;
            //        foreach (DataRow r in Precio.Rows)
            //        {

            //            Tabla_fill.Rows[i]["Pleno"] = Convert.ToDecimal(r["Pleno"]).ToString("N2", new CultureInfo("en-US"));
            //            Tabla_fill.Rows[i]["Distribuidor"] = Convert.ToDecimal(r["Distribuidor"]).ToString("N2", new CultureInfo("en-US"));
            //            Tabla_fill.Rows[i]["Filial1"] = Convert.ToDecimal(r["Filial1"]).ToString("N2", new CultureInfo("en-US"));
            //            Tabla_fill.Rows[i]["Filial2"] = Convert.ToDecimal(r["Filial2"]).ToString("N2", new CultureInfo("en-US"));
            //            i++;
            //        }

            //        Tabla_fill1 = cmIp.PoblarReporteUsuarioAprobador();

            //        // se llena con los precios para los usuarios aprobadores
            //        Precio2 = cmIp.PoblarAprobadorPrecio();
            //        Tabla_fill1.Columns["Pleno"].ReadOnly = false;
            //        Tabla_fill1.Columns["Pleno"].MaxLength = 100;
            //        Tabla_fill1.Columns["Distribuidor"].ReadOnly = false;
            //        Tabla_fill1.Columns["Distribuidor"].MaxLength = 100;
            //        Tabla_fill1.Columns["Filial1"].ReadOnly = false;
            //        Tabla_fill1.Columns["Filial1"].MaxLength = 100;
            //        Tabla_fill1.Columns["Filial2"].ReadOnly = false;
            //        Tabla_fill1.Columns["Filial2"].MaxLength = 100;
            //        foreach (DataRow r in Precio2.Rows)
            //        {

            //            Tabla_fill1.Rows[j]["Pleno"] = Convert.ToDecimal(r["Pleno"]).ToString("N2", new CultureInfo("en-US"));
            //            Tabla_fill1.Rows[j]["Distribuidor"] = Convert.ToDecimal(r["Distribuidor"]).ToString("N2", new CultureInfo("en-US"));
            //            Tabla_fill1.Rows[j]["Filial1"] = Convert.ToDecimal(r["Filial1"]).ToString("N2", new CultureInfo("en-US"));
            //            Tabla_fill1.Rows[j]["Filial2"] = Convert.ToDecimal(r["Filial2"]).ToString("N2", new CultureInfo("en-US"));
            //            j++;
            //        }
            //        Tabla_fill.Merge(Tabla_fill1);
            //        Tabla_fill.AcceptChanges();
            //        Tabla_fill.AcceptChanges();
            //        grdReportPlanta.Columns[15].Visible = true;
            //        grdReportPlanta.Columns[16].Visible = true;
            //        grdReportPlanta.Columns[17].Visible = true;
            //        grdReportPlanta.Columns[18].Visible = true;
            //        grdReportPlanta.Columns[21].Visible = true;
            //        grdReportPlanta.Columns[24].Visible = true;
            //        grdReportPlanta.Columns[25].Visible = true;
            //        }
            //        DataTable distinctTable = Tabla_fill.DefaultView.ToTable( /*distinct*/ true);
            //        DataView view = distinctTable.DefaultView;
            //        view.Sort = "item_planta_id DESC";
            //        DataTable dt = view.ToTable();

            //        Session.Add("TbReporte", dt);
            //        grdReportPlanta.DataSource = dt;
            //        grdReportPlanta.DataBind();
            //        cmIp.CerrarConexion();
            //    }
            //    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "scroll", "scrollback()", true);
            //}
            //else
            //{
            //    Session["TbReporte"] = "";
            //    mensaje = "Debe seleccionar alguna planta para cargar el reporte. Gracias!";
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            //}
        }
        private void Reload_Reporte()
        {
            DataTable dt = Session["TbReporte"] as DataTable;
            //DataView view = dtprecio.DefaultView;
            //view.Sort = "item_planta_id DESC";
            //DataTable dt = view.ToTable();
            grdReportPlanta.DataSource = dt;
            grdReportPlanta.DataBind();

            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "scroll", "scrollback()", true);
        }


        protected void ImgbtnLimpiar_Click(object sender, ImageClickEventArgs e)
        {            
            txtbuscarCodErp.Text = string.Empty;
            txtbuscardescrp.Text = string.Empty;
            cbogrupoIpbuscar.SelectedIndex = 0;
            cboplantaIpbuscar.SelectedIndex = 0;
            cboEstadoBuscar.SelectedIndex = 0;
            Session["TbReporte"] = "";
            Reload_Reporte();

        }
        private string buscarespeciales(string p)
        {

            if (p.Contains("'"))
            {
                p = p.Replace("'", "''");
            }
            return p;
        }
        protected void ImgbtnFiltrar_Click(object sender, EventArgs e)
        {
            CargarReporte();
            DataTable dtreporte = Session["TbReporte"] as DataTable;
            if (dtreporte != null) 
            {
                DataView dv = new DataView(dtreporte);
                String cadena = "";
                if (txtbuscarCodErp.Text.Equals("") && txtbuscardescrp.Text.Equals("") && cbogrupoIpbuscar.SelectedValue.Equals(" ") && cboplantaIpbuscar.SelectedValue.Equals(" ") && cboEstadoBuscar.SelectedValue.Equals(" "))
                {
                    cadena = "";
                }
                if (!txtbuscarCodErp.Text.Equals(""))
                {
                    cadena = "cod_erp LIKE '%" + txtbuscarCodErp.Text.Trim() + "%'";
                }
                if (!txtbuscardescrp.Text.Equals(""))
                {
                    string buscar = buscarespeciales(txtbuscardescrp.Text);
                    if (!cadena.Equals(""))
                    {
                        cadena += " AND  itemplanta_desc LIKE '%" + buscar.Trim().ToUpper() + "%'";
                    }
                    else
                    {
                        cadena = "itemplanta_desc LIKE '%" + buscar.Trim().ToUpper() + "%'";
                    }

                }
                if (!cboActivo.Text.Equals("Seleccione el Estado"))
                {
                    if (!cadena.Equals(""))
                    {
                        cadena += " AND  activo LIKE '%" + cboActivo.Text + "%'";
                    }
                    else
                    {
                        cadena = "activo LIKE '%" + cboActivo.Text + "%'";
                    }
                }
                if (!cbogrupoIpbuscar.SelectedItem.Text.Equals(" ") && cbogrupoIpbuscar.SelectedIndex != 0)
                {
                    if (!cadena.Equals(""))
                    {
                        cadena += " AND  grupo_des = '" + cbogrupoIpbuscar.SelectedItem.Text.Trim().ToUpper() + "'";
                    }
                    else
                    {
                        cadena = " grupo_des = '" + cbogrupoIpbuscar.SelectedItem.Text.Trim().ToUpper() + "'";
                    }
                }
                if (!cboEstadoBuscar.SelectedItem.Text.Equals(" ") && cboEstadoBuscar.SelectedIndex != 0)
                {
                    if (!cadena.Equals(""))
                    {
                        cadena += " AND  estado_desc = '" + cboEstadoBuscar.SelectedItem.Text.Trim().ToUpper() + "'";
                    }
                    else
                    {
                        cadena = " estado_desc = '" + cboEstadoBuscar.SelectedItem.Text.Trim().ToUpper() + "'";
                    }

                }
                if (ChkDispComercial.Checked == true)
                {
                    if (!cadena.Equals(""))
                    {
                        cadena += " AND  disp_comercial= 'SI'";
                    }
                    else
                    {
                        cadena = "disp_comercial= 'SI'";
                    }
                }             
                if (!cadena.Equals(""))

                {                   
                    dv.RowFilter = cadena;
                    Session.Add("TbReporte", dv.ToTable());
                }

                Reload_Reporte();
            }            
        }

        private void EstadoItemPlanta(EventArgs e)
        {

            if (Session["id_delete"].ToString().Equals(Session["item_planta_id"].ToString()))
            {
                Session["item_planta_id"] = "0";
                //cboPerfilIp_SelectedIndexChanged(cboPerfilIp, e);
                //lblDesciplanta.Value = "";
            }

            string msg = cmIp.EstadoItemPlanta(Convert.ToInt64((String)Session["id_delete"]), text_observdelte.Text.ToUpper(), false);
            if (msg.Equals("OK"))
            { Debug.WriteLine("Se actualizo estado en la tabla item_planta"); }
            else { Debug.WriteLine(msg); }
            grdReportPlanta.EditIndex = -1;
            //Insertar en bitacora_itemplanta_rel_estado
            string mensaje1 = cmIp.InsertarBitacoraEstado(Convert.ToInt64((String)Session["id_delete"]), 7, Session["usuario"].ToString(), text_observdelte.Text.ToUpper());
            if (mensaje1.Equals("OK"))
            { Debug.WriteLine("Se inserto con exito en la tabla bitacora_itemplanta_rel_estado"); }
            else { Debug.WriteLine(mensaje1); }
            CargarReporte();

        }
        protected void text_observdelte_TextChanged(object sender, EventArgs e)
        {
            EstadoItemPlanta(e);

        }

        protected void grdReportPlanta_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            bool activo = false;
            string id = grdReportPlanta.DataKeys[e.NewSelectedIndex].Value.ToString();
            String usu_select = ((Label)grdReportPlanta.Rows[e.NewSelectedIndex].FindControl("lblusuario")).Text;
            String estado_select = ((Label)grdReportPlanta.Rows[e.NewSelectedIndex].FindControl("lblestadoid")).Text;
            String enabled = grdReportPlanta.Rows[e.NewSelectedIndex].Cells[24].Text;
            if (enabled.Equals("ACTIVO"))
            {
                activo = true;
            }
            Session["usu_select"] = usu_select;
            Session["estado_select"] = estado_select;
            Session["activo"] = activo;

            //Session.Add("item_planta_id_reporte", "0");
            //if (!String.IsNullOrEmpty(id))
            //    Session["item_planta_id_reporte"] = id;

            string script = "window.open('MaestroItemPlanta.aspx?item_planta_id_reporte=" + id.Trim() + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "script", script, true);
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "salir", "cerrar()", true);
            //Response.Redirect("MaestroItemPlanta.aspx?id_planta=" + id);
            //e.NewSelectedIndex = -1; 
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "prompt", "prompt('Motivo:')", true);
        }

        protected void grdReportPlanta_Sorting(object sender, GridViewSortEventArgs e)
        {

            DataTable dtprecio = Session["TbReporte"] as DataTable;
            if (dtprecio != null)
            {

                //Sort the data.
                dtprecio.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                Session["TbReporte"] = dtprecio;
                Reload_Reporte();
            }
        }

        private string GetSortDirection(string column)
        {
            // By default, set the sort direction to ascending.
            string sortDirection = "ASC";

            // Retrieve the last column that was sorted.
            string sortExpression = Session["SortExpression"] as string;

            if (sortExpression != null)
            {
                // Check if the same column is being sorted.
                // Otherwise, the default value can be returned.
                if (sortExpression == column)
                {
                    string lastDirection = Session["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }

            // Save new values in Session.
            Session["SortDirection"] = sortDirection;
            Session["SortExpression"] = column;

            return sortDirection;
        }
        protected void btnRegresar_Click(object sender, EventArgs e)
        {

            Response.Redirect("MaestroItemPlanta.aspx?id_planta=" + Session["item_planta_id"].ToString());
            //Response.Write("<script>window.close();</script>");
        }

        protected void lkactivo_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            String id = grdReportPlanta.DataKeys[grdrow.RowIndex].Value.ToString();
            Session["id_delete"] = id;
            Label lblconsecutivo = (Label)grdReportPlanta.Rows[grdrow.RowIndex].FindControl("lblnumitemplanta");
                //String origen = ((Label)grdReportPlanta.Rows[grdrow.RowIndex].FindControl("lblorigen")).Text;
                //String coderp = grdReportPlanta.Rows[grdrow.RowIndex].Cells[4].Text; ;
                //String descripcion = grdReportPlanta.Rows[grdrow.RowIndex].Cells[5].Text;
                //String planta = ((Label)grdReportPlanta.Rows[grdrow.RowIndex].FindControl("lblplanta_id")).Text;
                //string cia = cmIp.ConsultarCia(Convert.ToInt32(planta));
                    if (id.Equals(Session["item_planta_id"].ToString()))
                    {
                        Session["item_planta_id"] = "0";
                    }
                    string msg = cmIp.EstadoItemPlanta(Convert.ToInt64(id), text_observdelte.Text.ToUpper(), true);
                    if (msg.Equals("OK"))
                    { Debug.WriteLine("Se actualizo estado en la tabla item_planta"); }
                    else { Debug.WriteLine(msg); }
                    //Insertar en bitacora_itemplanta_rel_estado
                    string mensaje1 = cmIp.InsertarBitacoraEstado(Convert.ToInt64(id), 8, Session["usuario"].ToString(), text_observdelte.Text.ToUpper());
                    if (mensaje1.Equals("OK"))
                    { Debug.WriteLine("Se inserto con exito en la tabla bitacora_itemplanta_rel_estado"); }
                    else { Debug.WriteLine(mensaje1); }
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alerta", "Mensajeaceptar( 'Item planta N°" + lblconsecutivo.Text + "!','Activado')", true);
                    //CargarReporte();
                    ImgbtnFiltrar_Click(sender, e);
               


        }

        protected void lkinactivo_Click(object sender, EventArgs e)
        {
            GridViewRow grdrow = (GridViewRow)((LinkButton)sender).NamingContainer;
            String id = grdReportPlanta.DataKeys[grdrow.RowIndex].Value.ToString();

            //CargarReporte();
            DataTable dt, dt2;

            dt = cmIp.Consultar_CodErp_ItemAnular(int.Parse(id));
            dt2 = cmIp.Validacion_Anular__Item_NoEmbalado(int.Parse(dt.Rows[0][0].ToString()), int.Parse(cboplantaIpbuscar.SelectedValue.ToString()));

            if (cmIp.Validacion_Anular__Item_NoEmbalado(int.Parse(dt.Rows[0][0].ToString()), int.Parse(cboplantaIpbuscar.SelectedValue.ToString())).Rows.Count == 0)
            {

                Session["id_delete"] = id;
                Label lblconsecutivo = (Label)grdReportPlanta.Rows[grdrow.RowIndex].FindControl("lblnumitemplanta");
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "llamado", "motivo(" + lblconsecutivo.Text + ")", true);

                if (id.Equals(Session["item_planta_id"].ToString()))
                {
                    Session["item_planta_id"] = "0";
                }
                string msg = cmIp.EstadoItemPlanta(Convert.ToInt64(id), text_observdelte.Text.ToUpper(), false);
                if (msg.Equals("OK"))
                { Debug.WriteLine("Se actualizo estado en la tabla item_planta"); }
                else { Debug.WriteLine(msg); }
                //Insertar en bitacora_itemplanta_rel_estado
                string mensaje1 = cmIp.InsertarBitacoraEstado(Convert.ToInt64(id), 8, Session["usuario"].ToString(), text_observdelte.Text.ToUpper());
                if (mensaje1.Equals("OK"))
                { Debug.WriteLine("Se inserto con exito en la tabla bitacora_itemplanta_rel_estado"); }
                else { Debug.WriteLine(mensaje1); }
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alerta", "Mensajeaceptar( 'Item planta N°" + lblconsecutivo.Text + "!','Desactivado')", true);
                //CargarReporte();
                ImgbtnFiltrar_Click(sender, e);
            }
            else
            {
                mensajeVentana("No se puede inactivar el ítem " + dt.Rows[0][0].ToString() + "  " +
                " porque está asociado a la orden" + dt2.Rows[0][5].ToString() + "  " +
                " que está en producción  y aun no se han despachado,  " +
                " por favor contactarse con planeación de producción");
            }
        }

        protected void grdReportPlanta_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdReportPlanta.PageIndex = e.NewPageIndex;
            Reload_Reporte();
        }

        //protected void cboplantaIpbuscar_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    CargarReporte();
        //}
        /**************METODO PARA MOSTRAR LOS ALERT QUE ARROJA LA PAGINA*************/
        private void mensajeVentana(string mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }
       
    }
}