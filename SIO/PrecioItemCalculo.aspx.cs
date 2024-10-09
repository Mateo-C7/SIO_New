using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Security;
using System.Net.Mail;
using System.Net;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using CapaControl;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using dhtmlxConnectors;
using Microsoft.VisualBasic;

namespace SIO
{
    public partial class PrecioItemCalculo : System.Web.UI.Page
    {
        public ControlCostos conCostos = new ControlCostos();
        public SqlDataReader reader = null;
        public ControlUbicacion contubi = new ControlUbicacion();
        private DataSet dsCostos = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scripManager = ScriptManager.GetCurrent(this.Page);
            scripManager.RegisterPostBackControl(btnCalcularPrecio);
            scripManager.RegisterPostBackControl(btnConfirmaPrecio);
            Session["Pagina"] = "Calculo Precio Items";
            Session["usu_select"] = "Sistemas";            
            Session["estado_select"] = 5;
            Session["activo"] = true;


            if (!IsPostBack)
            {
                //cargargrilla();
                PoblarCombos();
                //eliminarMemoriaCache();

                //if (Session["MensajeCliente"] == null)
                //    lblMensaje.Text = "";
                //else
                //{
                //    lblMensaje.Text = Session["MensajeCliente"].ToString();
                //    cargarReporteLog("LogClienteLite");
                //}

                //if (Session["MensajeObra"] == null)
                //    lblObras.Text = "";
                //else
                //{
                //    lblObras.Text = Session["MensajeObra"].ToString();
                //    cargarReporteLog("LogObraLite");
                //}

                //if (Session["MensajeContacto"] == null)
                //    lblContacto.Text = "";
                //else
                //{
                //    lblContacto.Text = Session["MensajeContacto"].ToString();
                //    cargarReporteLog("logContactoLite");
                //}

                //if (Session["MensajeValidadorCliente"] != null)
                //{
                //    cargarReporteLog("LogValidadorCliente");
                //}

                //Session["MensajeCliente"] = null;
                //Session["MensajeObra"] = null;
                //Session["MensajeContacto"] = null;
                //Session["MensajeValidadorCliente"] = null;
            }

        }

        private void PoblarCombos()
        {
            poblarPlanta();
            poblarAnio();
            poblarTrimestre();
        }

        //Cargar combo planta
        private void poblarPlanta()
        {
            string usuario = (string)Session["Usuario"];
            cboPlanta.Items.Clear();           

            cboPlanta.Items.Add(new ListItem("Seleccione la planta", "0"));
            reader = conCostos.poblarPlanta();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cboPlanta.Items.Add(new ListItem(reader.GetString(0), reader.GetInt32(1).ToString()));
                }
            }
            reader.Close();
            reader.Dispose();
            contubi.cerrarConexion();
        }

        //Cargar moneda trmplanta
        private void ConsultarMonedaTrm(int planta)
        {
            string usuario = (string)Session["Usuario"];
            
            reader = conCostos.ConsultarMonedaTrm(planta);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    lblmoneda.Text= reader.GetString(0).ToString();
                }
            }
            reader.Close();
            reader.Dispose();
            contubi.cerrarConexion();             
        }

        private void poblarAnio()
        {
            DateTime fechaActual = DateTime.Today;            
            cboAnio.Items.Clear();

            cboAnio.Items.Add(new ListItem("Seleccione", "0"));
            cboAnio.Items.Add(new ListItem(fechaActual.Year.ToString(), fechaActual.Year.ToString()));
            cboAnio.Items.Add(new ListItem((fechaActual.Year + 1).ToString(), (fechaActual.Year + 1).ToString()));            

        }

        private void poblarTrimestre()
        {            
            cboTrimestre.Items.Clear();

            cboTrimestre.Items.Add(new ListItem("Seleccione Trimestre", "0"));
            reader = conCostos.consultaTrimConfCostos(Convert.ToInt32(cboPlanta.SelectedItem.Value), Convert.ToInt32(cboAnio.SelectedItem.Value));
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cboTrimestre.Items.Add(new ListItem(reader.GetValue(0).ToString(), reader.GetValue(0).ToString()));
                }
            }
            reader.Close();
            reader.Dispose();
            contubi.cerrarConexion();

        }

        //Consulta el valor trm del trimestre
        private void consultarTrm()
        {
            reader = conCostos.consultarTrimestre(Convert.ToInt32(cboPlanta.SelectedItem.Value), Convert.ToInt32(cboAnio.SelectedItem.Value), Convert.ToInt32(cboTrimestre.SelectedItem.Value));
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    txtTrm.Text = reader.GetDecimal(0).ToString();
                }
            }
            else
            {
                txtTrm.Text = "";
            }

            reader.Close();
            reader.Dispose();
            contubi.cerrarConexion();
        }

        //Consulta la confirmacion de precio para activar el boton
        private void consultarConfirmaPrecio()
        {
            bool confirmaPrecio = false;

            reader = conCostos.consultaTrimConfPrecio(Convert.ToInt32(cboPlanta.SelectedItem.Value), Convert.ToInt32(cboAnio.SelectedItem.Value), Convert.ToInt32(cboTrimestre.SelectedItem.Value));
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    confirmaPrecio = reader.GetBoolean(0);
                    if (confirmaPrecio)
                    {
                        lblEstado.Visible = true;
                        lblEstado.Text = "Trimestre Confirmado";
                    }
                }
            }
            else
            {
                confirmaPrecio = false;
                lblEstado.Visible = false;
            }

            if (confirmaPrecio == true)
            {
                btnConfirmaPrecio.Visible = false;
                btnCalcularPrecio.Visible = false;
                GridView1.Columns[0].Visible = false;
                GridView1.Columns[14].Visible = true;
            }
            else
            {
                btnConfirmaPrecio.Visible = true;
                btnCalcularPrecio.Visible = true;
                GridView1.Columns[0].Visible = true;
                GridView1.Columns[14].Visible = false;
            }

            reader.Close();
            reader.Dispose();
            contubi.cerrarConexion();
        }




        protected void btnCalcularPrecio_Click(object sender, EventArgs e)
        {
            string usuario = (string)Session["Usuario"];
            string mensaje = "";

            if (cboPlanta.SelectedItem.Value == "0" || cboAnio.SelectedItem.Value == "0" || cboTrimestre.SelectedItem.Value == "0" || Convert.ToDecimal(txtTrm.Text) < 1 || txtTrm.Text == "")
            {
                mensaje = "Seleccione todas las opciones para Calcular el precio. Gracias!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
            else
            {
                int menId = conCostos.guardarTrm(Convert.ToInt32(cboPlanta.SelectedItem.Value), Convert.ToInt32(cboAnio.SelectedItem.Value), Convert.ToInt32(cboTrimestre.SelectedItem.Value),Convert.ToDecimal(txtTrm.Text));
                if (menId > 0) 
                {
                    int resul= conCostos.ProceCalculoPrecio(Convert.ToInt32(cboPlanta.SelectedItem.Value), Convert.ToInt32(cboAnio.SelectedItem.Value), Convert.ToInt32(cboTrimestre.SelectedItem.Value), usuario);
                    if (resul != -1)
                    {
                        mensaje = "Guardado exitosamente!!";
                        //btnConfirmaCosto.Visible = false;                        
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        cargargrilla();
                    }
                }
                else
                {
                    mensaje = "Hubo un error al guardar";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
            }
        }

        protected void cboPlanta_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConsultarMonedaTrm(Convert.ToInt32(cboPlanta.SelectedItem.Value));
            poblarAnio();
            cboTrimestre.Items.Clear();
            txtTrm.Text = "";
            GridView1.Dispose();
            GridView1.Visible = false;
            lblEstado.Visible = false;
        }

        protected void cboAnio_SelectedIndexChanged(object sender, EventArgs e)
        {
            poblarTrimestre();
            txtTrm.Text = "";
            GridView1.Dispose();
            GridView1.Visible = false;
            lblEstado.Visible = false;
        }

        protected void cboTrimestre_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPlanta.SelectedItem.Value != "0" && cboAnio.SelectedItem.Value != "0" && cboTrimestre.SelectedItem.Value != "0")
            {
                consultarTrm();
                cargargrilla();
                consultarConfirmaPrecio();
            }
        }

        protected void cargargrilla()
        {
            int Planta = Convert.ToInt32(cboPlanta.SelectedItem.Value);
            int Anio = Convert.ToInt32(cboAnio.SelectedItem.Value);
            int Trimestre = Convert.ToInt32(cboTrimestre.SelectedItem.Value);

            dsCostos = null;

            if (Planta != 0 && Anio != 0 && Trimestre != 0)
            {
                dsCostos = conCostos.ConsultarPrecioCalculado(Planta, Anio, Trimestre);
            }

            if (dsCostos != null)
            {
                GridView1.DataSource = dsCostos.Tables[0];
                GridView1.DataBind();
                GridView1.Visible = true;
                Session["DT"] = dsCostos.Tables[0];

            }
            else
            {
                GridView1.Dispose();
                GridView1.Visible = false;
            }            
                       

        }

        private void Reload_tbDetalle()
        {
            GridView1.DataSource = Session["DT"] as DataTable;
            GridView1.DataBind();
            //PanelEspecificaciones.Visible = false;
            //imgItem.Visible = false;
        }


        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {

            //GridViewRow. GridView1.EditIndex = e.NewEditIndex;
            ////this.BindGrid();
            //int fila = GridView1.Rows[e.RowIndex];


            //int rol = (int)Session["Rol"];
            //GridView1.EditIndex = e.NewEditIndex;
            //Reload_tbDetalle();
            //System.Web.UI.WebControls.TextBox txt_descrip = (System.Web.UI.WebControls.TextBox)GridView1.Rows[e.NewEditIndex].FindControl("textPleno");
            ////System.Web.UI.WebControls.TextBox txt_cantidadDetalle = (System.Web.UI.WebControls.TextBox)grdDetalle.Rows[e.NewEditIndex].FindControl("textCantidad");

        }

        protected void BindGrid()
        {
            GridView1.DataSource = ViewState["DT"] as DataTable;
            GridView1.DataBind();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            // Reset the edit index.
            cargargrilla();
            GridView1.EditIndex = -1;
            //BindGrid();
            
        }       

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

       

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int  CodigoErp = Convert.ToInt32( GridView1.Rows[e.RowIndex].Cells[5].Text.ToString());
            string plenoCop1 = ((System.Web.UI.WebControls.TextBox)GridView1.Rows[e.RowIndex].FindControl("textPleno")).Text;
            decimal  plenoCop = Convert.ToDecimal(plenoCop1);
            decimal trm = Convert.ToDecimal(GridView1.Rows[e.RowIndex].Cells[8].Text.ToString());

            DataTable dt = Session["DT"] as DataTable;
            int a = GridView1.PageIndex;
            int IdGrilla = Convert.ToInt32( GridView1.DataKeys[e.RowIndex].Value);

            decimal plenoUsd = plenoCop / trm;

            conCostos.actualizarPlenoCop(IdGrilla, CodigoErp, plenoCop, plenoUsd);


            //System.Web.UI.WebControls.Label labelnum = ((System.Web.UI.WebControls.Label)GridView1.Rows[e.RowIndex].FindControl("Id"));
            //int num_consecutivo = int.Parse(labelnum.Text) - 1;
            ////int index = Convert.ToInt32(e.RowIndex);
            ////int id = Convert.ToInt32(grdDetalle.Rows[index].Cells[0].Text.ToString());
            ////int id = Convert.ToInt32(grdDetalle.DataKeys[num_consecutivo].Value);
            //string b = ((System.Web.UI.WebControls.Label)GridView1.Rows[e.RowIndex].FindControl("CodErp")).Text;
            //int id = Convert.ToInt32(b);          

            //string precioUnitario = ((System.Web.UI.WebControls.Label)GridView1.Rows[e.RowIndex].FindControl("PrecioCop")).Text;
            ////double precio = Convert.ToDouble(precioUnitario) * Convert.ToDouble(strCantidad);
            //dt.Columns["cot_acc_precio_total"].ReadOnly = false;
            //dt.Columns["cot_acc_precio_total"].MaxLength = 100;
            ////dt.Rows[num_consecutivo]["cot_acc_precio_total"] = precio.ToString("N", new CultureInfo("en-US"));

            //contpv.ActulizarAccesorioFUP(id, strObservacion, Convert.ToDouble(strCantidad), precio, Convert.ToInt32(txtFUP.Text));

            //Session["IdAcc"] = id;
            //string parametros = "";
            //reader = contpv.cargarParametrosItem(id);
            //if (reader.HasRows == true)
            //{
            //    while (reader.Read())
            //    {
            //        parametros += reader.GetValue(2).ToString() + ": " + reader.GetValue(1).ToString() + "/ ";
            //    }
            //}
            //reader.Close();
            //reader.Dispose();
            //contpv.CerrarConexion();

            //reader = contpv.cargarCotizacionAccesorio(id);
            //if (reader.HasRows)
            //{
            //    reader.Read();
            //    string Estado = "Actualización";
            //    string Nombre = (string)Session["Nombre_Usuario"];
            //    controlsf.IngresarDatosLOGpv(Convert.ToInt32(txtFUP.Text), Convert.ToInt32(reader.GetValue(0)), Convert.ToInt32(reader.GetValue(1)), reader.GetValue(2).ToString(), reader.GetValue(3).ToString(), Estado, Nombre, reader.GetValue(4).ToString(), Convert.ToInt32(reader.GetValue(5)), Convert.ToInt32(reader.GetValue(6)), parametros);
            //}
            //reader.Close();
            //reader.Dispose();
            //contpv.CerrarConexion();

            //double total = 0;
            //reader = contpv.ConsultarDetalleFUP(Convert.ToInt32(txtFUP.Text), Convert.ToInt32(cboPlanta.SelectedValue));
            //if (reader.HasRows == true)
            //{
            //    while (reader.Read())
            //    {
            //        total += Convert.ToDouble(reader.GetValue(4).ToString());
            //    }
            //}
            //reader.Close();
            //reader.Dispose();
            //contpv.CerrarConexion();

            //ventaTotal.Text = total.ToString("N", new CultureInfo("en-US"));
            Session["DT"] = dt;
            GridView1.EditIndex = -1;
            cargargrilla();
            //Reload_tbDetalle();
        }

        protected void btnConfirmaPrecio_Click(object sender, EventArgs e)
        {
            string usuario = (string)Session["Usuario"];
            string mensaje = "";

            if (cboPlanta.SelectedItem.Value == "0" || cboAnio.SelectedItem.Value == "0" || cboTrimestre.SelectedItem.Value == "0")
            {
                mensaje = "Seleccione todas las Opciones para confirmar los Precios. Gracias!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
            else
            {
                int resul = conCostos.ProceInsertaPrecios(Convert.ToInt32(cboPlanta.SelectedItem.Value), Convert.ToInt32(cboAnio.SelectedItem.Value), Convert.ToInt32(cboTrimestre.SelectedItem.Value), usuario);
               
                int menId = conCostos.guardarConfirmaPrecios(Convert.ToInt32(cboPlanta.SelectedItem.Value), Convert.ToInt32(cboAnio.SelectedItem.Value), Convert.ToInt32(cboTrimestre.SelectedItem.Value), usuario);

                if (menId != -1 && resul != -1 )//-1 de ERROR
                {
                    mensaje = "Precios Confirmados exitosamente!!";
                    btnConfirmaPrecio.Visible = false;
                    cboTrimestre_SelectedIndexChanged(sender, e);
                    //cargargrilla();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
                else
                {
                    mensaje = "Hubo un error al guardar";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
            }
            
        }

        protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            string id = GridView1.DataKeys[e.NewSelectedIndex].Value.ToString();
            int CodigoErp = Convert.ToInt32(GridView1.Rows[e.NewSelectedIndex].Cells[5].Text.ToString());
            int idIplanta = 0;

            reader = conCostos.ConsultarIdItemPlanta(Convert.ToInt32( cboPlanta.SelectedItem.Value), CodigoErp);
             
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    idIplanta = Convert.ToInt32( reader.GetSqlValue(0).ToString());
                }
            }
            reader.Close();
            reader.Dispose();
            contubi.cerrarConexion();

            string script = "window.open('MaestroItemPlanta.aspx?item_planta_id_reporte=" + idIplanta.ToString().Trim() + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "script", script, true);
        }
    }
}