using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaControl;
using System.Data;


namespace SIO
{
    
    public partial class MaestroMonedas : System.Web.UI.Page
    {
        Cls_MonedaTrm ctrlmoneda = new Cls_MonedaTrm();
        DataTable dtValida;
        int existePeriodo;

        protected void Page_Load(object sender, EventArgs e)
        {  
            if (!IsPostBack)
            {
                Session["filtro"] = 0;
                btn_Registrar.Text = "Guardar";
                //Listar monedas
                cbo_Moneda.Items.Add(new ListItem("Seleccione", "0"));
                ctrlmoneda.ListarMonedas(cbo_Moneda);  
                //Listar años         
                cbo_Año.Items.Add(new ListItem("Seleccione", "0"));
                ctrlmoneda.ListarAños(cbo_Año); 
                //Listar meses
                cbo_Mes.Items.Add(new ListItem("Seleccione", "0"));
                ctrlmoneda.ListarMes(cbo_Mes);

                //Valida decimales en el campo
                txt_Trm.Attributes.Add("onKeyPress", " return  validedecimal(event,this);");

                //Carga los registros de monedas en la grilla
                int filtro = (int)Session["filtro"];
                Consultar_Detalle_MonedasTrm(filtro);               
            }
        }

        //Permite utilizar mensajes como alert
        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }

        protected void btn_Registrar_Click(object sender, EventArgs e)
        {
            //Variables del Log------------------------------ 
            string tabla = "moneda_trm";
            String fecha = DateTime.Now.ToShortDateString();
            string evento = "Actuaización";
            string usuario = (string)Session["Usuario"];
            //-----------------------------------------------               
    
            string periodo = string.Concat(cbo_Año.SelectedValue, cbo_Mes.SelectedValue);

            if (btn_Registrar.Text == "Guardar")
            {

               

                   if (cbo_Moneda.SelectedIndex != 0 && cbo_Año.SelectedIndex != 0 &&
                    cbo_Mes.SelectedIndex != 0 && !String.IsNullOrEmpty(txt_Trm.Text))
                {
                    //Valida que no exista un periodo en la base de datos, igual al que se desea crear
                    dtValida = ctrlmoneda.Validar_Periodo(Convert.ToInt32(cbo_Moneda.SelectedValue), Convert.ToInt32(cbo_Año.SelectedValue), Convert.ToInt32(cbo_Mes.SelectedValue));
                    if (dtValida.Rows.Count == 0)
                    {
                        ctrlmoneda.Guardar_Registro(int.Parse(cbo_Moneda.SelectedValue), decimal.Parse(txt_Trm.Text),
                                                     int.Parse(cbo_Año.SelectedValue), int.Parse(cbo_Mes.SelectedValue), usuario);                   
                        limpiar_Campos();
                        Session["filtro"] = 0;
                        int filtro = (int)Session["filtro"];
                        mensajeVentana("Registro guardado con exito.");
                        Consultar_Detalle_MonedasTrm(filtro);
                    }
                    else
                    {
                        mensajeVentana("Ya existe un registro correspondiente al periodo " + periodo + ".");
                    }
                }
                else
                {
                    mensajeVentana("Debe diligenciar todos los campos");
                }
               
            }
            else if (btn_Registrar.Text == "Actualizar")
            {
                //Datos que tenian los combos, antes de cualquier modificación por el usuario                   
                int monedaAnt = (int)Session["moneda"];
                int añoAnt = (int)Session["año"];
                int mesAnt = (int)Session["mes"];
                //Datos actuales, despues de cualquier modificación por el usuario      
                int moneda = Convert.ToInt32(cbo_Moneda.SelectedValue);
                int año= Convert.ToInt32(cbo_Año.SelectedValue);
                int mes = Convert.ToInt32(cbo_Mes.SelectedValue);

               
                if (cbo_Moneda.SelectedIndex != 0 && cbo_Año.SelectedIndex != 0 &&
                cbo_Mes.SelectedIndex != 0 && !String.IsNullOrEmpty(txt_Trm.Text))
                {
                    //Si se ha realizado algun cambio en los combos, permite la validación.
                    if (moneda != monedaAnt || año != añoAnt || mes != mesAnt)
                    {
                        //Valida que no exista un periodo en la base de datos, igual al que se desea crear.
                        dtValida = ctrlmoneda.Validar_Periodo(Convert.ToInt32(cbo_Moneda.SelectedValue), Convert.ToInt32(cbo_Año.SelectedValue), Convert.ToInt32(cbo_Mes.SelectedValue));

                        if (dtValida.Rows.Count != 0)
                        {
                            existePeriodo = 1;
                        }
                        else
                        {
                            existePeriodo = 0;
                        }
                    }
                    else
                    {
                        existePeriodo = 0;
                    }

                    if (existePeriodo == 0)
                    {
                        string mon_trm_id = (string)Session["mon_trm_id"];


                        ctrlmoneda.Actualizar_Registro(int.Parse(cbo_Moneda.SelectedValue), decimal.Parse(txt_Trm.Text),
                                                                             int.Parse(cbo_Año.SelectedValue), int.Parse(cbo_Mes.SelectedValue),usuario,
                                                                             Convert.ToInt32(mon_trm_id));                   
                        mensajeVentana("Registro actualizado con exito");
                        Session["filtro"] = 0;
                        int filtro = (int)Session["filtro"];
                        Consultar_Detalle_MonedasTrm(filtro);
                        limpiar_Campos();
                        btn_Registrar.Text = "Guardar";
                    }
                    else
                    {
                        mensajeVentana("Ya existe un registro correspondiente al periodo " + periodo + ".");
                    }
                }
                else
                {
                    mensajeVentana("Debe establecer un valor en el campo TRM, o seleccionar una moneda, año y mes");
                }
               
            }
           
        }

        public void limpiar_Campos()
        {
            cbo_Moneda.SelectedIndex = 0;
            cbo_Año.SelectedIndex = 0;
            cbo_Mes.SelectedIndex = 0;
            txt_Trm.Text = "";
        }


        public void Consultar_Detalle_MonedasTrm(int filtro)
        {
            DataTable dt;
          
            dt = ctrlmoneda.Consultar_Detalle_MonedasTrm().Tables[0];
         

            if (dt.Rows.Count != 0)
            {
                //Si es 1, se aplican los filtros
                if (filtro == 1)
                {                    
                    string filtroMoneda = cbo_Moneda.SelectedItem.Text;
                    string filtroAño = cbo_Año.SelectedItem.Text;
                    string filtroMes = cbo_Mes.SelectedItem.Text;
                    string filtros = "";
                    DataView dv = new DataView(dt);

                    if (filtroMoneda != "Seleccione" && filtroAño != "Seleccione" && filtroMes != "Seleccione")
                    {
                        filtros = "mon_descripcion='" + filtroMoneda + "'  AND año = " + filtroAño + " AND mes = " + filtroMes + "";
                    }
                    else if (filtroMoneda != "Seleccione" && filtroAño == "Seleccione" && filtroMes == "Seleccione")
                    {
                        filtros = "mon_descripcion='" + filtroMoneda + "'";
                    }
                    else if (filtroMoneda == "Seleccione" && filtroAño != "Seleccione" && filtroMes == "Seleccione")
                    {
                        filtros = "año = " + filtroAño + " ";
                    }
                    else if (filtroMoneda == "Seleccione" && filtroAño == "Seleccione" && filtroMes != "Seleccione")
                    {
                        filtros = "mes = " + filtroMes + " ";
                    }
                    else if (filtroMoneda != "Seleccione" && filtroAño != "Seleccione")
                    {
                        filtros = "mon_descripcion='" + filtroMoneda + "'  AND año = " + filtroAño + "";
                    }
                    else if (filtroMoneda != "Seleccione" && filtroMes != "Seleccione")
                    {
                        filtros = "mon_descripcion='" + filtroMoneda + "'  AND mes = " + filtroMes + "";
                    }
                    else if (filtroAño != "Seleccione" && filtroMes != "Seleccione")
                    {
                        filtros = "año = " + filtroAño + "  AND mes = " + filtroMes + "";
                    }
                    //filtro                                                       
                    dv.RowFilter = filtros;                    
                    grid_Maestro.DataSource = dv.ToTable();
                    grid_Maestro.DataMember = dv.ToTable().ToString();
                    grid_Maestro.DataBind();
                    dv.Dispose();
                }
                else
                {
                    grid_Maestro.DataSource = dt;
                    grid_Maestro.DataMember = dt.ToString();
                    grid_Maestro.DataBind();
                    grid_Maestro.PageIndex = 0;
                }               
            }
            else
            {
                grid_Maestro.DataSource = null;
                grid_Maestro.DataMember = null;
                grid_Maestro.DataBind();
            }
        }

        protected void grid_Maestro_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            int filtro = (int)Session["filtro"];

            grid_Maestro.EditIndex = -1;
            try
            {
                Consultar_Detalle_MonedasTrm(filtro);
                grid_Maestro.PageIndex = e.NewPageIndex;
                this.grid_Maestro.DataBind();
            }
            catch (Exception ex)
            {
                string error = ex.Message.ToString();
                mensajeVentana(error); 
            }
        }

        protected void btn_Filtrar_Click(object sender, EventArgs e)
        {
            btn_Registrar.Text = "Guardar";
            Session["filtro"] = 1;
            int filtro = (int)Session["filtro"];
            Consultar_Detalle_MonedasTrm(filtro);
        }

        public object ListarMonedaDgv()
        {
            DataSet ds1;
            ds1 = ctrlmoneda.ListarMonedaDgv();
            return ds1;
        }

        public object ListarAñoDgv()
        {
            DataSet ds1;
            ds1 = ctrlmoneda.ListarAñoDgv();
            return ds1;
        }


        public object ListarMesDgv()
        {
            DataSet ds1;
            ds1 = ctrlmoneda.ListarMesDgv();
            return ds1;
        }


        //protected void grid_Maestro_RowEditing(object sender, GridViewEditEventArgs e)
        //{          
        //    grid_Maestro.EditIndex = e.NewEditIndex;          
        //    int filtro = (int)Session["filtro"];
        //    Consultar_Detalle_MonedasTrmDt(filtro);
        //}

        //protected void grid_Maestro_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    grid_Maestro.EditIndex = -1;
        //    int filtro = (int)Session["filtro"];
        //    Consultar_Detalle_MonedasTrmDt(filtro);
        //}

        //Metodo para que pueda funcionar el RowEditing
        public void Consultar_Detalle_MonedasTrmDt(int filtro)
        {
            DataTable dt;

            dt = ctrlmoneda.Consultar_Detalle_MonedasTrmDt();

            if (dt.Rows.Count != 0)
            {
                //Si es 1, se aplican los filtros
                if (filtro == 1)
                {
                    string filtroMoneda = cbo_Moneda.SelectedItem.Text;
                    string filtroAño = cbo_Año.SelectedItem.Text;
                    string filtroMes = cbo_Mes.SelectedItem.Text;
                    string filtros = "";
                    DataView dv = new DataView(dt);

                    if (filtroMoneda != "Seleccione" && filtroAño != "Seleccione" && filtroMes != "Seleccione")
                    {
                        filtros = "mon_descripcion='" + filtroMoneda + "'  AND año = " + filtroAño + " AND mes = " + filtroMes + "";
                    }
                    else if (filtroMoneda != "Seleccione" && filtroAño == "Seleccione" && filtroMes == "Seleccione")
                    {
                        filtros = "mon_descripcion='" + filtroMoneda + "'";
                    }
                    else if (filtroMoneda == "Seleccione" && filtroAño != "Seleccione" && filtroMes == "Seleccione")
                    {
                        filtros = "año = " + filtroAño + " ";
                    }
                    else if (filtroMoneda == "Seleccione" && filtroAño == "Seleccione" && filtroMes != "Seleccione")
                    {
                        filtros = "mes = " + filtroMes + " ";
                    }
                    else if (filtroMoneda != "Seleccione" && filtroAño != "Seleccione")
                    {
                        filtros = "mon_descripcion='" + filtroMoneda + "'  AND año = " + filtroAño + "";
                    }
                    else if (filtroMoneda != "Seleccione" && filtroMes != "Seleccione")
                    {
                        filtros = "mon_descripcion='" + filtroMoneda + "'  AND mes = " + filtroMes + "";
                    }
                    else if (filtroAño != "Seleccione" && filtroMes != "Seleccione")
                    {
                        filtros = "año = " + filtroAño + "  AND mes = " + filtroMes + "";
                    }
                    //filtro                                                       
                    dv.RowFilter = filtros;

                    grid_Maestro.DataSource = dv.ToTable();
                    grid_Maestro.DataMember = dv.ToTable().ToString();
                    grid_Maestro.DataBind();
                }
                else
                {
                    grid_Maestro.DataSource = dt;
                    grid_Maestro.DataMember = dt.ToString();
                    grid_Maestro.DataBind();
                    grid_Maestro.PageIndex = 0;
                }
            }
            else
            {
                grid_Maestro.DataSource = null;
                grid_Maestro.DataMember = null;
                grid_Maestro.DataBind();
            }
        }

        protected void grid_Maestro_RowCommand(object sender, GridViewCommandEventArgs e)
        {
         
            grid_Maestro.EditIndex = -1;
            DataTable dt;

                if (e.CommandName == "Select")
                {
                    // Se obtiene indice de la row seleccionada

                    int index = Convert.ToInt32(e.CommandArgument);
                    // Obtengo el id de la entidad que se esta en seleccion      
                    string mon_trm_id = grid_Maestro.DataKeys[index].Value.ToString();

                    //Se crea la variable de session y le asignamos valor
                    Session["mon_trm_id"] = mon_trm_id;

                dt = ctrlmoneda.Consultar_Detalle_Moneda(Convert.ToInt32(mon_trm_id));

                cbo_Moneda.Text= dt.Rows[0]["moneda"].ToString();
                cbo_Año.Text = dt.Rows[0]["Año"].ToString();
                cbo_Mes.Text = dt.Rows[0]["Mes"].ToString();
                txt_Trm.Text = dt.Rows[0]["Trm"].ToString();
                       
                Session["moneda"] =Convert.ToInt32(cbo_Moneda.SelectedValue);
                Session["año"] = Convert.ToInt32(cbo_Año.SelectedValue);
                Session["mes"] = Convert.ToInt32(cbo_Mes.SelectedValue);
                

                //decimal Trm = Convert.ToDecimal(dt.Rows[0]["Trm"].ToString());
                //txt_Trm.Text = Convert.ToString(Trm.ToString("#,##.##"));
                btn_Registrar.Text = "Actualizar";
                }
        }       

    }
}