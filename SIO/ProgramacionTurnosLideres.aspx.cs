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
    public partial class ProgramacionTurnosLideres : System.Web.UI.Page
    {
        ControlProgramTurnoLider ctrlprogturlider = new ControlProgramTurnoLider();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {         
            Cbo_Proceso.Items.Add(new ListItem("Seleccione", "0"));
            ctrlprogturlider.Listar_Procesos(Cbo_Proceso);
            ctrlprogturlider.Listar_Plantas(Cbo_PlantaOper);
            Cbo_Turno.Items.Add(new ListItem("Seleccione", "0"));
            Cbo_Turno.Items.Add(new ListItem("TURNO 1", "T1"));
            Cbo_Turno.Items.Add(new ListItem("TURNO 2", "T2"));
            Cbo_Turno.Items.Add(new ListItem("TURNO 3", "T3"));

            Txt_NombreOperario.ReadOnly = true;

            //Valida decimales en los campos 
           Txt_IdOper.Attributes.Add("onKeyPress", " return  valideKeyenteros(event,this);");

                Consultar_Detalle_Programacion();

                int rol = (int)Session["Rol"];
                if (rol == 13 || rol == 12 || rol == 19)
                {
                    Grid_Detalle_Programacion.Columns[1].Visible = true;
                    Grid_Detalle_Programacion.Columns[7].Visible = true;                          
                }
                else
                {
                    Grid_Detalle_Programacion.Columns[1].Visible = false;
                    Grid_Detalle_Programacion.Columns[7].Visible = false;
                }
            }
        }

        public void Obtener_InfoUsuario()
        {
            DataTable dt;

            if (!String.IsNullOrEmpty(Txt_IdOper.Text))
            {         
            dt = ctrlprogturlider.Obtener_InfoUsuario(Convert.ToInt32(Txt_IdOper.Text));

            if (dt.Rows.Count > 0)
            {
                Txt_NombreOperario.Text = dt.Rows[0]["Empleado"].ToString();           
                Btn_Guardar.Enabled = true;
            }
            else
            {
                Btn_Guardar.Enabled = false;
                Txt_NombreOperario.Text = "";                         
            }
            }
            else
            {
                Btn_Guardar.Enabled = false;
                Txt_NombreOperario.Text = "";            
            }
        }

        //Permite utilizar mensajes como alert
        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }

        protected void Txt_IdOper_TextChanged(object sender, EventArgs e)
        {
            Obtener_InfoUsuario();
        }

        protected void Txt_FechaIni_TextChanged(object sender, EventArgs e)
        {
            string fecIni;
            fecIni = Txt_FechaIni.Text;
            DateTime validaFecha;

            if (!String.IsNullOrEmpty(Txt_FechaIni.Text))
            {
                if (DateTime.TryParse(fecIni, out validaFecha))
                {
                }
                else
                {
                    mensajeVentana("Debe seleccionar una fecha desde el calendario");
                    Txt_FechaIni.Focus();
                    Txt_FechaIni.Text = "";
                }
            }
        }

        protected void Txt_FechaFinal_TextChanged(object sender, EventArgs e)
        {
            string fecFin;
            fecFin = Txt_FechaFinal.Text;
            DateTime validaFecha;

            if (!String.IsNullOrEmpty(Txt_FechaFinal.Text))
            {
                if (DateTime.TryParse(fecFin, out validaFecha))
                {
                }
                else
                {
                    mensajeVentana("Debe seleccionar una fecha desde el calendario");
                    Txt_FechaFinal.Focus();
                    Txt_FechaFinal.Text = "";
                }
            }
        }

        protected void Btn_Guardar_Click(object sender, EventArgs e)
        {
            int respuesta;
            int valida = 0;
            DateTime fecini, fecfin;

            if (Btn_Guardar.Text == "Guardar")
            {
                if (Cbo_Proceso.SelectedValue != "0")
                {
                    if (Cbo_Turno.SelectedValue != "0")
                    {                       
                            if (!String.IsNullOrEmpty(Txt_FechaIni.Text))
                            {
                                if (!String.IsNullOrEmpty(Txt_FechaFinal.Text))
                                {
                                    fecini = DateTime.Parse(Txt_FechaIni.Text);
                                    fecfin = DateTime.Parse(Txt_FechaFinal.Text);

                                    if (fecini < fecfin)
                                    {
                                        string fecfinal = Convert.ToDateTime(Txt_FechaFinal.Text).ToString("dd/MM/yyyy");
                                        string fecinicial = Convert.ToDateTime(Txt_FechaIni.Text).ToString("dd/MM/yyyy");

                                        Boolean existe = true;
                                        existe = ctrlprogturlider.Existe_Programa_Lider(Convert.ToInt32(Txt_IdOper.Text), Txt_FechaIni.Text, Txt_FechaFinal.Text);
                                        if (!existe)
                                        {                                       
                                            respuesta = ctrlprogturlider.GuardarProgramacion(Convert.ToInt32(Txt_IdOper.Text), Cbo_Turno.SelectedValue,
                                                                                 Convert.ToInt32(Cbo_Proceso.SelectedValue), fecinicial,
                                                                                 fecfinal, Convert.ToInt32(Cbo_PlantaOper.SelectedValue));
                                        if (respuesta != 0)
                                        {
                                            mensajeVentana("Datos enviados con exito");
                                            Limpiar_Detalle_Programacion();
                                            Consultar_Detalle_Programacion();
                                        }
                                        else
                                        {
                                            mensajeVentana("Envio de datos fallido");
                                        }
                                        }
                                        else
                                        {
                                            mensajeVentana("El lider ya tiene una programación asignada en ese rango de fechas, por favor ingrese otro rango, gracias!");
                                        }
                                    }
                                    else
                                    {
                                        mensajeVentana("La fecha de inicio debe ser menor a la fecha final");
                                    }
                                }
                                else
                                {
                                    mensajeVentana("Debe establecer la fecha final");
                                    Txt_FechaFinal.Focus();
                                }
                            }
                            else
                            {
                                mensajeVentana("Debe establecer la fecha de inicio");
                                Txt_FechaIni.Focus();
                            }                    
                    }
                    else
                    {
                        mensajeVentana("Debe seleccionar un turno");
                        Cbo_Turno.Focus();
                    }
                }
                else
                {
                    mensajeVentana("Debe seleccionar un proceso");
                    Cbo_Proceso.Focus();
                }
            }
            else if (Btn_Guardar.Text=="Actualizar")
            {
                int TurnEmp_Id = (int)Session["TurnEmp_Id"];

                if (!String.IsNullOrEmpty(Txt_FechaIni.Text))
                {
                    if (!String.IsNullOrEmpty(Txt_FechaFinal.Text))
                {
                        fecini = DateTime.Parse(Txt_FechaIni.Text);
                        fecfin = DateTime.Parse(Txt_FechaFinal.Text);

                      string fechainiactual = (string)Session["fechaini"];
                      string fechafinactual = (string)Session["fechafin"];

                        DateTime f1 = DateTime.Parse(fechainiactual);
                        DateTime f2 = DateTime.Parse(fechafinactual);


                        if (fecini!= f1 || fecfin != f2)
                        {                      
                            Boolean existe = true;
                            existe = ctrlprogturlider.Existe_Programa_Lider(Convert.ToInt32(Txt_IdOper.Text), Txt_FechaIni.Text, Txt_FechaFinal.Text);
                            if (!existe)
                            {
                                 valida = 1;
                            }
                            else
                            {
                                 valida = 2;                               
                            }
                        }

                     if(valida==0 || valida == 1)
                        {
                            if (fecini < fecfin)
                            {
                                string fecfinal = Convert.ToDateTime(Txt_FechaFinal.Text).ToString("dd/MM/yyyy");
                                string fecinicial = Convert.ToDateTime(Txt_FechaIni.Text).ToString("dd/MM/yyyy");

                                respuesta = ctrlprogturlider.ActualizarProgramacion(Convert.ToInt32(Txt_IdOper.Text), Cbo_Turno.SelectedValue,
                                                                                     Convert.ToInt32(Cbo_Proceso.SelectedValue), fecinicial,
                                                                                     fecfinal, TurnEmp_Id, Convert.ToInt32(Cbo_PlantaOper.SelectedValue));
                                if (respuesta == 1)
                                {
                                    mensajeVentana("Datos actualizados con exito");
                                    Consultar_Detalle_Programacion();
                                }
                                else
                                {
                                    mensajeVentana("Actualización de datos fallida");
                                }
                            }
                            else
                            {
                                mensajeVentana("La fecha de inicio debe ser menor a la fecha final");
                            }

                        }
                        else
                        {
                            mensajeVentana("El lider ya tiene una programación asignada en ese rango de fechas, por favor ingrese otro rango, gracias!");
                        }                     
                    }
                else
                {
                    mensajeVentana("Debe establecer la fecha final");
                    Txt_FechaFinal.Focus();
                }
                }
                else
                {
                    mensajeVentana("Debe establecer la fecha inicial");
                    Txt_FechaIni.Focus();
                }
            }
        }

        public void Limpiar_Detalle_Programacion()
        {
            Txt_IdOper.Text = "";
            Txt_NombreOperario.Text = "";       
            Cbo_Proceso.SelectedIndex = 0;
            Cbo_Turno.SelectedIndex = 0;
            Txt_FechaIni.Text = "";
            Txt_FechaFinal.Text = "";
            Cbo_PlantaOper.SelectedIndex = 0;
        }


        public void Consultar_Detalle_Programacion()
        {      
            Grid_Detalle_Programacion.DataSource = ctrlprogturlider.Consultar_Detalle_Programacion(Convert.ToInt32(Cbo_PlantaOper.SelectedValue));
            Grid_Detalle_Programacion.DataMember = ctrlprogturlider.Consultar_Detalle_Programacion(Convert.ToInt32(Cbo_PlantaOper.SelectedValue)).ToString();
            Grid_Detalle_Programacion.DataBind();
        }

        protected void Grid_Detalle_Programacion_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Grid_Detalle_Programacion.EditIndex = -1;
            DataTable dt;

            if (e.CommandName == "Select")
            {
                // Se obtiene indice de la row seleccionada

                int index = Convert.ToInt32(e.CommandArgument);
                // Obtengo el id de la entidad que se esta en seleccion      
                int TurnEmp_Id = Convert.ToInt32(Grid_Detalle_Programacion.DataKeys[index].Value);

                //Se crea la variable de session y le asignamos valor
               Session["TurnEmp_Id"] = TurnEmp_Id;
            
                dt = ctrlprogturlider.Obtener_Detalle_Programacion_X_Id(TurnEmp_Id);

                Txt_IdOper.Text = dt.Rows[0]["TurnEmp_IdOper"].ToString();
                Txt_IdOper.ReadOnly = true;
                Txt_NombreOperario.Text = dt.Rows[0]["Lider"].ToString();
                Cbo_PlantaOper.Items.Clear();
                ctrlprogturlider.Listar_Plantas(Cbo_PlantaOper);
                Cbo_PlantaOper.Text = dt.Rows[0]["TurnEmp_IdPlantaProd"].ToString();
                Cbo_Proceso.Items.Clear();
                ctrlprogturlider.Listar_Procesos(Cbo_Proceso);
                Cbo_Proceso.Text = dt.Rows[0]["TurnEmp_IdProceso"].ToString();
                Cbo_Turno.Items.Clear();
                Cbo_Turno.Items.Add(new ListItem("TURNO 1", "T1"));
                Cbo_Turno.Items.Add(new ListItem("TURNO 2", "T2"));
                Cbo_Turno.Items.Add(new ListItem("TURNO 3", "T3"));
                string turno = dt.Rows[0]["TurnEmp_Turno"].ToString();
                Cbo_Turno.Text = turno.ToString();
                Cbo_Turno.SelectedValue = dt.Rows[0]["TurnEmp_Turno"].ToString();
                DateTime fechaini = Convert.ToDateTime(dt.Rows[0]["TurnEmp_FechIni"].ToString());
                Txt_FechaIni.Text = fechaini.ToString("dd/MM/yyyy");
                Session["fechaini"] = Txt_FechaIni.Text;
                DateTime fechafinal = Convert.ToDateTime(dt.Rows[0]["TurnEmp_FechFin"].ToString());
                Txt_FechaFinal.Text = fechafinal.ToString("dd/MM/yyyy");
                Session["fechafin"] = Txt_FechaFinal.Text;
                Btn_Guardar.Text = "Actualizar";
            }
            }

        protected void Grid_Detalle_Programacion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int TurnEmp_Id = Convert.ToInt32(Grid_Detalle_Programacion.DataKeys[e.RowIndex].Value);                      
            ctrlprogturlider.EliminarProgramacion(TurnEmp_Id);          
            Limpiar_Detalle_Programacion();
            Consultar_Detalle_Programacion();
            Cbo_Proceso.Items.Clear();
            Cbo_Turno.Items.Clear();
            Cbo_Proceso.Items.Add(new ListItem("Seleccione", "0"));
            ctrlprogturlider.Listar_Procesos(Cbo_Proceso);
            Cbo_Turno.Items.Add(new ListItem("Seleccione", "0"));
            Cbo_Turno.Items.Add(new ListItem("TURNO 1", "T1"));
            Cbo_Turno.Items.Add(new ListItem("TURNO 2", "T2"));
            Cbo_Turno.Items.Add(new ListItem("TURNO 3", "T3"));
            Btn_Guardar.Text = "Guardar";
            mensajeVentana("Registro eliminado con exito");
        }

        protected void Grid_Detalle_Programacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
         
            Grid_Detalle_Programacion.EditIndex = -1;
            try
            {
                Consultar_Detalle_Programacion();
                Grid_Detalle_Programacion.PageIndex = e.NewPageIndex;
                this.Grid_Detalle_Programacion.DataBind();
            }
            catch (Exception ex)
            {
               
            }
        }

        protected void Cbo_PlantaOper_TextChanged(object sender, EventArgs e)
        {
            Consultar_Detalle_Programacion();
        }
    }
}