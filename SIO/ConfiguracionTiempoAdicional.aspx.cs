using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaControl;

namespace SIO
{
    public partial class ConfiguracionTiempoAdicional : System.Web.UI.Page
    {
        ControlTiempoAdicional ctrltiempoadi = new ControlTiempoAdicional();
    
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txt_PlantaUsuario.ReadOnly = true;
                ObtenerPlantaUsuario();
                int idPlanta = (int)Session["idPlanta"];

                Consultar_Detalle_Tiempos();

                ctrltiempoadi.Listar_TipoPieza(cbo_TipoPieza);

                //Valida enteros en los campos 
                txt_TiempoMetalmeca.Attributes.Add("onKeyPress", " return  validedecimal(event,this);");
                txt_TiempoSolda.Attributes.Add("onKeyPress", " return  validedecimal(event,this);");
                txt_TiempoPeter.Attributes.Add("onKeyPress", " return  validedecimal(event,this);");
                //-------------------------------------------------------------------------------------
                             
                txt_TiempoPeter.ToolTip = "Digitar tiempo en minutos";
                txt_TiempoSolda.ToolTip = "Digitar tiempo en minutos";
                txt_TiempoMetalmeca.ToolTip = "Digitar tiempo en minutos";
            }
        }

        //Permite utilizar mensajes como alert
        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }


        public void ObtenerPlantaUsuario()
        {
            DataTable dt;
           string idusuario = (string)Session["idUsuario"];
           dt=ctrltiempoadi.ObtenerPlantaUsuario(Convert.ToInt32(idusuario));
           txt_PlantaUsuario.Text = dt.Rows[0]["planta_descripcion"].ToString();
           Session["idPlanta"] = Convert.ToInt32(dt.Rows[0]["usu_planta_Id"].ToString());
        }

        protected void Btn_Guardar_Click(object sender, EventArgs e)
        {
            //Definición de variables
            int respuesta;
            int idPlanta = (int)Session["idPlanta"];      
            //----------------------

            //Convierte el valor en mayuscula
            txt_Codigo.Text = txt_Codigo.Text.ToUpper();

            if (String.IsNullOrEmpty(txt_TiempoMetalmeca.Text)) { txt_TiempoMetalmeca.Text = "0"; }
            if (String.IsNullOrEmpty(txt_TiempoSolda.Text)) { txt_TiempoSolda.Text = "0"; }
            if (String.IsNullOrEmpty(txt_TiempoPeter.Text)) { txt_TiempoPeter.Text = "0"; }

            if (!String.IsNullOrEmpty(txt_Codigo.Text))
            {
                if (!String.IsNullOrEmpty(txt_Descripcion.Text))
                {
        
            if(Convert.ToDecimal(txt_TiempoMetalmeca.Text) < 120)
            {
                if (Convert.ToDecimal(txt_TiempoSolda.Text) < 120)
                {
                    if (Convert.ToDecimal(txt_TiempoPeter.Text) < 120)
                    {
                                    if (Btn_Guardar.Text == "Guardar")
                                    {

                                    if (ctrltiempoadi.ValidarCodigo(txt_Codigo.Text).Rows.Count == 0)
                                    {
                                        respuesta = ctrltiempoadi.GuardarTiempoAdicional(txt_Codigo.Text, txt_Descripcion.Text, Convert.ToDecimal(txt_TiempoMetalmeca.Text), 0,
                                                                                   Convert.ToDecimal(txt_TiempoSolda.Text), Convert.ToInt32(cbo_TipoPieza.SelectedValue),
                                                                                   Convert.ToInt32(idPlanta), Convert.ToDecimal(txt_TiempoPeter.Text));
                                        if (respuesta == 1)
                                        {
                                            mensajeVentana("Registro guardado con exito");
                                            Limpiar_Campos();
                                            Consultar_Detalle_Tiempos();
                                        }
                                        else
                                        {
                                            mensajeVentana("No fue posible guardar el registro");
                                            txt_TiempoMetalmeca.Text = "";
                                            txt_TiempoSolda.Text = "";
                                            txt_TiempoPeter.Text = "";
                                        }
                                    }
                                    else
                                    {
                                        mensajeVentana("El codigo ya existe, por favor ingrese uno diferente");
                                        txt_Codigo.Focus();
                                    }
                                }
                                    else if (Btn_Guardar.Text == "Actualizar")
                                    {
                                        string idCodigo = (string)Session["idCodigo"];


                                        if (idCodigo != txt_Codigo.Text)
                                        {
                                            if (ctrltiempoadi.ValidarCodigo(txt_Codigo.Text).Rows.Count == 0)
                                            {
                                                respuesta = ctrltiempoadi.Actualizar_Detalle_Tiempos(txt_Codigo.Text, txt_Descripcion.Text, Convert.ToDecimal(txt_TiempoMetalmeca.Text),
                                                                                                     Convert.ToDecimal(txt_TiempoSolda.Text), Convert.ToInt32(cbo_TipoPieza.SelectedValue),
                                                                                                     Convert.ToDecimal(txt_TiempoPeter.Text), idCodigo);
                                                if (respuesta == 1)
                                                {
                                                    mensajeVentana("Registro actualizado con exito");
                                                    Consultar_Detalle_Tiempos();
                                                    Limpiar_Campos();
                                                }
                                                else
                                                {
                                                    mensajeVentana("No fue posible actualizar el registro");
                                                }
                                            }
                                            else
                                            {
                                                mensajeVentana("El codigo ya existe, por favor ingrese uno diferente");
                                                txt_Codigo.Focus();
                                            }
                                        }
                                        else
                                        {
                                            respuesta = ctrltiempoadi.Actualizar_Detalle_Tiempos(txt_Codigo.Text, txt_Descripcion.Text, Convert.ToDecimal(txt_TiempoMetalmeca.Text),
                                                                                             Convert.ToDecimal(txt_TiempoSolda.Text), Convert.ToInt32(cbo_TipoPieza.SelectedValue),
                                                                                             Convert.ToDecimal(txt_TiempoPeter.Text), idCodigo);
                                            if (respuesta == 1)
                                            {
                                                mensajeVentana("Registro actualizado con exito");
                                                Consultar_Detalle_Tiempos();
                                            Limpiar_Campos();
                                            }
                                            else
                                            {
                                                mensajeVentana("No fue posible actualizar el registro");
                                            }
                                        }                                       
                                    }                                       
                    }
                    else
                    {
                        mensajeVentana("El campo Tiempo Pter debe ser menor a 120 minutos");
                        txt_TiempoPeter.Focus();                                  
                                    txt_TiempoPeter.Text = "";
                                }
                }
                else
                {
                    mensajeVentana("El campo Tiempo Soldadura debe ser menor a 120 minutos");
                    txt_TiempoSolda.Focus();                               
                                txt_TiempoSolda.Text = "";                           
                            }
            }
            else
            {
                mensajeVentana("El campo Tiempo Metalmecanica debe ser menor a 120 minutos");
                txt_TiempoMetalmeca.Focus();
                            txt_TiempoMetalmeca.Text = "";                     
                        }          
                }
                else
                {
                    mensajeVentana("Debe digitar una descripción");
                    txt_Descripcion.Focus();               
                }
            }
            else
            {
                mensajeVentana("Debe digitar un codigo");
                txt_Codigo.Focus();             
            }
        }

        public void Limpiar_Campos()
        {
            txt_Codigo.Text = "";
            txt_Descripcion.Text = "";
            txt_TiempoMetalmeca.Text = "";
            txt_TiempoSolda.Text = "";
            txt_TiempoPeter.Text = "";
            cbo_TipoPieza.SelectedIndex = 0;
            txt_Codigo.Focus();
        }


        public void Consultar_Detalle_Tiempos()
        {
            int idPlanta = (int)Session["idPlanta"];
            Grid_Detalle_ConfgTiempos.DataSource = ctrltiempoadi.Consultar_Detalle_Tiempos(idPlanta);
            Grid_Detalle_ConfgTiempos.DataMember = ctrltiempoadi.Consultar_Detalle_Tiempos(idPlanta).ToString();
            Grid_Detalle_ConfgTiempos.DataBind();
        }

        protected void Grid_Detalle_ConfgTiempos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Grid_Detalle_ConfgTiempos.EditIndex = -1;
            try
            {
                Consultar_Detalle_Tiempos();
                Grid_Detalle_ConfgTiempos.PageIndex = e.NewPageIndex;
                this.Grid_Detalle_ConfgTiempos.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btn_Cancelar_Click(object sender, EventArgs e)
        {
            Limpiar_Campos();
            Btn_Guardar.Text = "Guardar";
        }

        protected void Grid_Detalle_ConfgTiempos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Grid_Detalle_ConfgTiempos.EditIndex = -1;
            string Tmde_Id = Grid_Detalle_ConfgTiempos.DataKeys[e.RowIndex].Value.ToString();
            ctrltiempoadi.Eliminar_Detalle_Tiempos(Tmde_Id);         
            Consultar_Detalle_Tiempos();
            Limpiar_Campos();
            mensajeVentana("Registro eliminado con exito");
        }


        protected void Grid_Detalle_ConfgTiempos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int idPlanta = (int)Session["idPlanta"];
            Grid_Detalle_ConfgTiempos.EditIndex = -1;
            DataTable dt;

            if (e.CommandName == "Select")
            {
                // Se obtiene indice de la row seleccionada

                int index = Convert.ToInt32(e.CommandArgument);
                // Obtengo el id de la entidad que se esta en seleccion      
                string Tmde_Id = Grid_Detalle_ConfgTiempos.DataKeys[index].Value.ToString();

                //Se crea la variable de session y le asignamos valor
                Session["Tmde_Id"] = Tmde_Id;

                dt = ctrltiempoadi.Consultar_Detalle_Tiempos_X_Codigo(idPlanta,Tmde_Id);
                txt_Codigo.Text = dt.Rows[0]["Tmde_Id"].ToString();
                txt_Descripcion.Text = dt.Rows[0]["Tmde_Dscrpcion"].ToString();
                decimal TiempoMetalmeca = Convert.ToDecimal(dt.Rows[0]["Tmde_Metal"].ToString());
                txt_TiempoMetalmeca.Text = Convert.ToString(TiempoMetalmeca.ToString("#,##.#"));
                decimal TiempoSolda = Convert.ToDecimal(dt.Rows[0]["Tmde_Solda"].ToString());
                txt_TiempoSolda.Text = Convert.ToString(TiempoSolda.ToString("#,##.#"));
                decimal TiempoPeter = Convert.ToDecimal(dt.Rows[0]["Tmde_Pter"].ToString());
                txt_TiempoPeter.Text = Convert.ToString(TiempoPeter.ToString("#,##.#"));           
                cbo_TipoPieza.Text = dt.Rows[0]["TpoPza_Id"].ToString();
                Session["idCodigo"] = txt_Codigo.Text;
                Btn_Guardar.Text = "Actualizar";
            }
            //else if (e.CommandName == "Delete")
            //{
            //    Grid_Detalle_ConfgTiempos.EditIndex = -1;
            //    int index = Convert.ToInt32(e.CommandArgument);
            //    // Obtengo el id de la entidad que se esta en seleccion  
            //    string Tmde_Id = Grid_Detalle_ConfgTiempos.DataKeys[index].Value.ToString();
            //    ctrltiempoadi.Eliminar_Detalle_Tiempos(Tmde_Id);
            //    Consultar_Detalle_Tiempos();
            //    mensajeVentana("Registro eliminado con exito");
            //}
        }
    }
}