using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaControl;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace SIO
{
    public partial class MaestroOperaciones : System.Web.UI.Page
    {
        ControlMaestroOperaciones ctrloperacion = new ControlMaestroOperaciones();
        public SqlDataReader reader = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int rol = (int)Session["Rol"];

                if (rol == 26 && BtnGuardar.Text == "Guardar")
                {
                    BtnGuardar.Enabled = false;
                }
                else if (rol == 26 && BtnGuardar.Text == "Actualizar")
                {
                    BtnGuardar.Enabled = true;
                }
                else if (rol == 1)
                {
                    BtnGuardar.Enabled = true;
                }
                else
                {
                    BtnGuardar.Enabled = false;
                }

                Cargar_Combo_Aplica();

                TxtCentroCosto.Attributes.Add("onKeyPress", " return  valideKeyenteros(event,this);");
                TxtHrsUnid.Attributes.Add("onKeyPress", " return  validedecimal(event,this);");
                TxtCostoPromAlum.Attributes.Add("onKeyPress", " return  validedecimal(event,this);");
                TxtCostoPromBush.Attributes.Add("onKeyPress", " return  validedecimal(event,this);");
                TxtCostoPromRem.Attributes.Add("onKeyPress", " return  validedecimal(event,this);");

                Consultar_Detalle_Operaciones();
          
            }      
        }
        //Permite utilizar mensajes como alert
        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }

        public void Cargar_Combo_Aplica()
        {
            Cbo_Aplica.Items.Add(new ListItem("Seleccione", "0"));
            Cbo_Aplica.Items.Add(new ListItem("Componente", "1"));
            Cbo_Aplica.Items.Add(new ListItem("Formaleta", "2"));
        }

        public void Consultar_Detalle_Operaciones()
        {
            Grid_DetalleOperaciones.DataSource = ctrloperacion.Consultar_Detalle_Operaciones();
            Grid_DetalleOperaciones.DataMember = ctrloperacion.Consultar_Detalle_Operaciones().ToString();
            Grid_DetalleOperaciones.DataBind();
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            int respuesta, idOperacion = 0;
            DataTable dt;

            if (String.IsNullOrEmpty(TxtCentroCosto.Text)){ TxtCentroCosto.Text = "0";}
            if (String.IsNullOrEmpty(TxtHrsUnid.Text)) { TxtHrsUnid.Text = "0"; }
            if (String.IsNullOrEmpty(TxtCostoPromAlum.Text)) { TxtCostoPromAlum.Text = "0"; }
            if (String.IsNullOrEmpty(TxtCostoPromBush.Text)) { TxtCostoPromBush.Text = "0"; }
            if (String.IsNullOrEmpty(TxtCostoPromRem.Text)) { TxtCostoPromRem.Text = "0"; }

            if (BtnGuardar.Text == "Guardar")
            {            
                dt = ctrloperacion.Obterner_ultimo_IdOperacion();


            if (!String.IsNullOrEmpty(TxtDescripcion.Text) && !String.IsNullOrEmpty(TxtUnidad.Text) && Cbo_Aplica.SelectedValue != "0")
            {
                           
            //Obtiene el ultimo id de la tabla operaciones
            idOperacion = Convert.ToInt32(dt.Rows[0][0].ToString());
            idOperacion += 1;



             respuesta =ctrloperacion.Guardar_Detalle_Operaciones(idOperacion, TxtDescripcion.Text, TxtUnidad.Text, Cbo_Aplica.SelectedItem.ToString(),
                                                                  Convert.ToInt32(TxtCentroCosto.Text), Convert.ToDecimal(TxtHrsUnid.Text),
                                                                  Convert.ToDecimal(TxtCostoPromAlum.Text), Convert.ToDecimal(TxtCostoPromBush.Text),
                                                                  Convert.ToDecimal(TxtCostoPromRem.Text));
            if (respuesta == 1)
            {
                mensajeVentana("Registo guardado correctamente.");
                Consultar_Detalle_Operaciones();
            }
            else
            {
                mensajeVentana("No se pudo guardar el registro.");
            }
            }
            else
            {
                mensajeVentana("Debe digitar los campos obligatorios");
            }
            }
            else if (BtnGuardar.Text == "Actualizar")
            {
                int idOperacion2 = (int)Session["idOperacion"];              

                if (!String.IsNullOrEmpty(TxtDescripcion.Text) && !String.IsNullOrEmpty(TxtUnidad.Text) && Cbo_Aplica.SelectedValue != "0")
                {                  
                    respuesta = ctrloperacion.Actualizar_Detalle_Operaciones(TxtDescripcion.Text, TxtUnidad.Text, Cbo_Aplica.SelectedItem.ToString(),
                                                                         Convert.ToInt32(TxtCentroCosto.Text), Convert.ToDecimal(TxtHrsUnid.Text),
                                                                         Convert.ToDecimal(TxtCostoPromAlum.Text), Convert.ToDecimal(TxtCostoPromBush.Text),
                                                                         Convert.ToDecimal(TxtCostoPromRem.Text), idOperacion2);
                    if (respuesta == 1)
                    {
                        mensajeVentana("Registo actualizado correctamente.");
                        Consultar_Detalle_Operaciones();
                    }
                    else
                    {
                        mensajeVentana("No se pudo actualizar el registro.");
                    }
                }
                else
                {
                    mensajeVentana("Debe digitar los campos obligatorios");
                }
            }
        }

        protected void Grid_DetalleOperaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Grid_DetalleOperaciones.EditIndex = -1;
            DataTable dt;

            // Se obtiene indice de la row seleccionada
            int index = Convert.ToInt32(e.CommandArgument);
            // Obtengo el id de la entidad que se esta en seleccion      
            int idOperacion = Convert.ToInt32(Grid_DetalleOperaciones.DataKeys[index].Value);
 
             if (e.CommandName == "Select")
            {
               

                //Se crea la variable de session y le asignamos valor
                Session["idOperacion"] = idOperacion;

                dt = ctrloperacion.Consultar_Detalle_Operaciones2(idOperacion);

                TxtDescripcion.Text = dt.Rows[0]["Descripcion"].ToString();
                TxtUnidad.Text = dt.Rows[0]["Unidad"].ToString();
                Cbo_Aplica.Items.Clear();
                Cargar_Combo_Aplica();

                string comboAplica = dt.Rows[0]["Aplica_En"].ToString();
                if (comboAplica == "Componente") { Cbo_Aplica.SelectedValue = 1.ToString(); }
                else if (comboAplica == "Formaleta") { Cbo_Aplica.SelectedValue = 2.ToString(); }

                TxtCentroCosto.Text = dt.Rows[0]["Cen_Cos_Id"].ToString();
                TxtHrsUnid.Text = dt.Rows[0]["HorasxUnidad"].ToString();
                TxtCostoPromAlum.Text = dt.Rows[0]["Costo_Prom_Alum_Erp"].ToString();
                TxtCostoPromBush.Text = dt.Rows[0]["Costo_Prom_Bush"].ToString();
                TxtCostoPromRem.Text = dt.Rows[0]["Costo_Prom_Rem"].ToString();
                BtnGuardar.Text = "Actualizar";

                int rol = (int)Session["Rol"];

                if (rol == 26 && BtnGuardar.Text == "Guardar")
                {
                    BtnGuardar.Enabled = false;
                }
                else if (rol == 26 && BtnGuardar.Text == "Actualizar")
                {
                    BtnGuardar.Enabled = true;
                }
                else if (rol == 1)
                {
                    BtnGuardar.Enabled = true;
                }
                else
                {
                    BtnGuardar.Enabled = false;
                }
            }
        }



        protected void Btn_Nuevo_Click(object sender, EventArgs e)
        {
            TxtDescripcion.Text = "";
            TxtUnidad.Text = "";
            Cbo_Aplica.Items.Clear();
            Cargar_Combo_Aplica();
            TxtCentroCosto.Text = "";
            TxtHrsUnid.Text = "";
            TxtCostoPromAlum.Text = "";
            TxtCostoPromBush.Text = "";
            TxtCostoPromRem.Text = "";
            TxtDescripcion.Focus();
            BtnGuardar.Text = "Guardar";

            int rol = (int)Session["Rol"];

            if (rol == 26 && BtnGuardar.Text == "Guardar")
            {
                BtnGuardar.Enabled = false;
            }
            else if (rol == 26 && BtnGuardar.Text == "Actualizar")
            {
                BtnGuardar.Enabled = true;
            }
            else if (rol == 1)
            {
                BtnGuardar.Enabled = true;
            }
            else
            {
                BtnGuardar.Enabled = false;
            }
        }

        protected void Grid_DetalleOperaciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Grid_DetalleOperaciones.EditIndex = -1;
            try
            {
                Consultar_Detalle_Operaciones();
                Grid_DetalleOperaciones.PageIndex = e.NewPageIndex;
                this.Grid_DetalleOperaciones.DataBind();
            }
            catch (Exception ex)
            {

            }
        }


       
    }         
    }

