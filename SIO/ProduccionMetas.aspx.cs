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
	public partial class ProduccionMetas : System.Web.UI.Page
	{
        ControlMetas objcontmeta = new ControlMetas();
        ControlPlantas ctrlPlantas = new ControlPlantas();
        ControlProceso ctrlProceso = new ControlProceso();
        ControlPiezas ctrlPieza = new ControlPiezas();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //obtiene el nombre de usuario y lo establece en un label
                lblusumetas.Text = (string)Session["Usuario"];
                String usuario =(string)Session["Nombre_Usuario"];
                ctrlPlantas.ListarPlantasProduccion(cboPlantas);
                ctrlProceso.ListarProcesosPlanta(cboProcesos);
                Grid_Detalle_Maestro.Visible = true;
                pnlCamposDetaMaes.Visible = false;
                //botones------------
                btn_Insertar_DetaMeta.Visible = false;
                btn_CanlarAgreMetas.Visible = false;
                btn_CancelarDetaMeta.Visible = false;
                //--------------------
                Met_DeshabilitarCampos_Maestro();
                Met_Cargar_Detalle_Maestro(int.Parse(cboPlantas.Text));
                Consultar_Maestro_Metas_x_Planta();
                //Habilitar_Botones_X_Rol();                
            }
        }

        public void Habilitar_Botones_X_Rol()

        {
            int rol = (int)Session["Rol"];
            if (rol == 36 || rol == 12 || rol == 13 || rol == 42 || rol == 19)
            {
                Grid_Detalle_Maestro.Columns[9].Visible = true;
                Grid_Detalle_Maestro.Columns[10].Visible = true;
                GridView_Maestro_Metas.Columns[4].Visible = true;
                GridView_Maestro_Metas.Columns[5].Visible = true;
                btn_Agregar_DetaMeta.Visible = true;
                btn_Habilitar_pnlMetas.Visible = true;
            }
            else
            {
                Grid_Detalle_Maestro.Columns[9].Visible = false;
                Grid_Detalle_Maestro.Columns[10].Visible = false;
                GridView_Maestro_Metas.Columns[4].Visible = false;
                GridView_Maestro_Metas.Columns[5].Visible = false;
                btn_Agregar_DetaMeta.Visible = false;
                btn_Habilitar_pnlMetas.Visible = false;
            }
        }

        public void Consultar_Maestro_Metas()
        {
            GridView_Maestro_Metas.DataSource = objcontmeta.Consultar_Maestro_Metas();
            GridView_Maestro_Metas.DataMember = objcontmeta.Consultar_Maestro_Metas().Tables[0].ToString();
            GridView_Maestro_Metas.DataBind();
        }

       public void Proc_Consultar_Detalle_MetasProduccion_SinParametro()
        {
            Grid_Detalle_Maestro.DataSource = objcontmeta.Proc_Consultar_Detalle_MetasProduccion_();
            Grid_Detalle_Maestro.DataMember = objcontmeta.Proc_Consultar_Detalle_MetasProduccion_().Tables[0].ToString();
            Grid_Detalle_Maestro.DataBind();
        }

        public void Met_DeshabilitarCampos_Maestro()
        {

            pnlIngMetas.Visible = false;
            btnAgregarMeta.Enabled = true;
        }

        protected void GridView_Maestro_Metas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            lblMsgPrincipal.Text = "";
            GridView_Maestro_Metas.EditIndex = -1;
            try
            {
                Consultar_Maestro_Metas_x_Planta();
                GridView_Maestro_Metas.PageIndex = e.NewPageIndex;
                this.GridView_Maestro_Metas.DataBind();
            }
            catch (Exception ex)
            {
                lblMsgPrincipal.Text = ex.Message.ToString();
            }
        }

        //muestra la consulta indexada Detalle_Maestro
        protected void Grid_Detalle_Maestro_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            lblMsgPrincipal.Text = "";
            Grid_Detalle_Maestro.EditIndex = -1;
            try
            {
                Proc_Consultar_Detalle_MetasProduccion_SinParametro();
                Grid_Detalle_Maestro.PageIndex = e.NewPageIndex;
                this.Grid_Detalle_Maestro.DataBind();
            }
            catch (Exception ex)
            {
                lblMsgPrincipal.Text = ex.Message.ToString();
            }
        }

        // metodo para selecionar una row y obtener su indice
        public void GridView_Maestro_Metas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Grid_Detalle_Maestro.EditIndex = -1;
            lblMsgPrincipal.Text = "";        
            btn_Agregar_DetaMeta.Visible = true;
            lblGridMetas.Text = "";
            int rol = (int)Session["Rol"];

            if (e.CommandName == "Select")
            {
                // Se obtiene indice de la row seleccionada

                int index = Convert.ToInt32(e.CommandArgument);
                // Obtengo el id de la entidad que se esta en seleccion
                // en este caso de la entidad Mepr_Id

                int Valor_MeprId = Convert.ToInt32(GridView_Maestro_Metas.DataKeys[index].Value);
                lblMperId.Text = Valor_MeprId.ToString();//Guarda el valor del MeprId en el label

                if (objcontmeta.Proc_Consultar_Detalle_MetasProduccion(Valor_MeprId).Tables[0].Rows.Count != 0)
                {
                    btn_Insertar_DetaMeta.Visible = false;
                    pnlCamposDetaMaes.Visible = false;
                    Grid_Detalle_Maestro.DataSource = objcontmeta.Proc_Consultar_Detalle_MetasProduccion(Valor_MeprId);
                    Grid_Detalle_Maestro.DataMember = objcontmeta.Proc_Consultar_Detalle_MetasProduccion(Valor_MeprId).Tables[0].ToString();
                    Grid_Detalle_Maestro.Visible = true;
                    btn_CancelarDetaMeta.Visible = false;
                    btn_Agregar_DetaMeta.Visible = true;
                    Grid_Detalle_Maestro.DataBind();
                    cbo_TpoPza.Items.Clear();
                    ctrlPieza.Listar_Tipo_Pieza(cbo_TpoPza);
                    Met_Obtener_Descripcion_Proceso();
                    lblDescripcionProce.Visible = true;
                    //Habilitar_Botones_X_Rol();
                }
                else
                {
                    Grid_Detalle_Maestro.Visible = false;
                    pnlCamposDetaMaes.Visible = true;
                    btn_Insertar_DetaMeta.Visible = true;
                    btn_CancelarDetaMeta.Visible = true;
                    btn_Agregar_DetaMeta.Visible = false;
                    lblGridMetas.Text = "";
                    Met_Obtener_Descripcion_Proceso();
                    cbo_TpoPza.Items.Clear();
                    ctrlPieza.Listar_Tipo_Pieza(cbo_TpoPza);
                    Txt_FechIni.Focus();
                }           
            }                                                                                                 
        }

        //consulta y lista el maestro en un gridview
        public void Consultar_Maestro_Metas_x_Planta()
        {
            String idPlanta = cboPlantas.SelectedValue.ToString();// recupero el valor del combo

            //ctrlProceso.ListarProcesosxPlanta(idPlanta);

            if (objcontmeta.Consultar_Maestro_Metas_x_Planta(int.Parse(idPlanta)).Tables[0].Rows.Count != 0)
            {
                GridView_Maestro_Metas.DataSource = objcontmeta.Consultar_Maestro_Metas_x_Planta(int.Parse(idPlanta));
                GridView_Maestro_Metas.DataMember = objcontmeta.Consultar_Maestro_Metas_x_Planta(int.Parse(idPlanta)).Tables[0].ToString();
                GridView_Maestro_Metas.Visible = true;
                GridView_Maestro_Metas.DataBind();               
            }
            else
            {

            }

        }
        public void Consultar_Maestro_Metas_Prueba()
        {
            String idPlanta = cboPlantas.SelectedValue.ToString();// recupero el valor del combo

            //ctrlProceso.ListarProcesosxPlanta(idPlanta);

            if (objcontmeta.Consultar_Maestro_Metas_x_Planta(int.Parse(idPlanta)).Tables[0].Rows.Count != 0)
            {
                GridView_Maestro_Metas.DataSource = objcontmeta.Consultar_Maestro_Metas_Prueba(int.Parse(idPlanta));
                GridView_Maestro_Metas.DataMember = objcontmeta.Consultar_Maestro_Metas_Prueba(int.Parse(idPlanta)).ToString();
                GridView_Maestro_Metas.Visible = true;
                GridView_Maestro_Metas.DataBind();
            }
            else
            {

            }
        }

        public void Proc_Consultar_Detalle_MetasProduccion()
        {
            if (objcontmeta.Proc_Consultar_Detalle_MetasProduccion(int.Parse(lblMperId.Text)).Tables[0].Rows.Count != 0)
            {
                Grid_Detalle_Maestro.DataSource = objcontmeta.Proc_Consultar_Detalle_MetasProduccion(int.Parse(lblMperId.Text));
                Grid_Detalle_Maestro.DataMember = objcontmeta.Proc_Consultar_Detalle_MetasProduccion(int.Parse(lblMperId.Text)).Tables[0].ToString();
                Grid_Detalle_Maestro.DataBind();
            }
            else
            {
                lblDescripcionProce.Visible = false;
                btn_Agregar_DetaMeta.Visible = false;
                Grid_Detalle_Maestro.DataSource = null;
                Grid_Detalle_Maestro.DataMember = null;
                Grid_Detalle_Maestro.DataBind();
            }
          
        }


        public void Proc_Consultar_Detalle_MetasProduccion_prueba()
        {
      
            Grid_Detalle_Maestro.DataSource = objcontmeta.Proc_Consultar_Detalle_MetasProduccion_prueba(int.Parse(lblMperId.Text));
            Grid_Detalle_Maestro.DataMember = objcontmeta.Proc_Consultar_Detalle_MetasProduccion_prueba(int.Parse(lblMperId.Text)).ToString();
            Grid_Detalle_Maestro.DataBind();
        }

        //metodo para llenar un combo con las plantas de produccion
        protected void cboPlantas_SelectedIndexChanged(object sender, EventArgs e)
        {

            int rol = (int)Session["Rol"];

          
                GridView_Maestro_Metas.EditIndex = -1;
            Consultar_Maestro_Metas_x_Planta();
            Grid_Detalle_Maestro.EditIndex = -1;
            //Proc_Consultar_Detalle_MetasProduccion();
            Grid_Detalle_Maestro.Visible = false;
            pnlCamposDetaMaes.Visible = false;         
            lblMsgPrincipal.Text = "";
            lblGridMetas.Text = "";
            lblDescripcionProce.Text = "";

            Met_Quemar_LabelMeta();

            if (cboPlantas.Text != "")
            {
                if (rol == 36 || rol == 12 || rol == 13 || rol == 42 || rol == 19)
                {               
                        if (pnlIngMetas.Visible == true)
                    {
                        GridView_Maestro_Metas.Visible = false;
                        btn_Habilitar_pnlMetas.Visible = false;
                    }
                    else
                    {
                        Met_Cargar_Detalle_Maestro(int.Parse(cboPlantas.Text));
                        btn_CancelarDetaMeta.Visible = false;
                        btn_Habilitar_pnlMetas.Visible = true;
                        Consultar_Maestro_Metas_x_Planta();
                    }                   
                }
                else
                {
                    if (pnlIngMetas.Visible == true)
                    {
                        GridView_Maestro_Metas.Visible = false;
                        btn_Habilitar_pnlMetas.Visible = false;
                      
                    }
                    else
                    {
                        Met_Cargar_Detalle_Maestro(int.Parse(cboPlantas.Text));
                        btn_CancelarDetaMeta.Visible = false;
                        btn_Agregar_DetaMeta.Visible = false;
                        Consultar_Maestro_Metas_x_Planta();
                    }
                }               
            }
            else
            {
                mensajeVentana("Debe Elegir una planta");
                cboPlantas.Focus();
                GridView_Maestro_Metas.DataSource = null;
            }
        }

        //asigna a el nombre de la columna metas si es M2 o Kg
        public  void Met_Quemar_LabelMeta()
        {
            if (cboPlantas.Text == "2")
            {
                Grid_Detalle_Maestro.Columns[5].HeaderText = "Meta/M2";
            }
            else if (cboPlantas.Text == "8")
            {
                {
                    Grid_Detalle_Maestro.Columns[5].HeaderText = "Meta/Kg";
                }
            }
        }


        //metodo para llenar un combo con los procesos de produccion
        protected void cboProcesoPlanta_SelectedIndexChanged(object sender, EventArgs e)
        {
            //String IdProceso = cboProcesoPlanta.SelectedValue.ToString();
        }

        // metodo de tipo objeto donde retorna una consulta dataset
        //para llenar combo en gridview
        public object ObtenerProcPlan()
        {
            DataSet ds;
            ds = objcontmeta.ObtenerProcesoPlanta();
            return ds;
        }

        public object Obtener_TipoPieza()
        {
            DataSet ds1;
            ds1 = ctrlPieza.Met_Obtener_TipoPieza();
            return ds1;
        }

        // habilita la fila seleccionada y se puede actualizar

        protected void cboProcesos_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        //metodo para actualizar maestro metas

        // boton para agrgar una nueva meta de produccion
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            pnlIngMetas.Visible = true;
            lblPlanta.Visible = true;
            cboPlantas.Visible = true;
            lblMsgPrincipal.Text = "";
            btnAgregarMeta.Enabled = true;
            GridView_Maestro_Metas.DataSource = null;
            cboPlantas.Focus();
        }

        //boton agregar metas produccion
        protected void btnAgregarMeta_Click1(object sender, EventArgs e)
        {
            lblGridMetas.Text = "";

            if (cboPlantas.Text != "" && cboProcesos.Text != "" && Txt_Observacionpnlmeta.Text != "")
            {
                objcontmeta.Met_Agregar_MetaProduccion(Txt_Observacionpnlmeta.Text, int.Parse(cboPlantas.Text), int.Parse(cboProcesos.Text));
                mensajeVentana("Registro Exitoso");   
                Txt_Observacionpnlmeta.Text = "";
                pnlIngMetas.Visible = false;
                GridView_Maestro_Metas.Visible = true;
                btn_Habilitar_pnlMetas.Visible = true;
                Consultar_Maestro_Metas_x_Planta();
            }
            else
            {
             mensajeVentana("No se permiten campos vacios");
            }
        }

        //metodo para agregar el detalle a una meta de produccion
        protected void btn_Insertar_DetaMeta_Click(object sender, EventArgs e)
        {
            //try
            //{
                string cadenaoperarios, cadenaunidades, cadenametas, cadenatolerancia, cadenafecIni, cadenafecFin, cadenapieaza, cadenafrecinsp;
                cadenametas = Txt_Metas.Text;
                cadenaoperarios = Txt_NumOper.Text;
                cadenapieaza = cbo_TpoPza.Text;
                cadenatolerancia = Txt_Tolerancia.Text;
                cadenaunidades = Txt_Unidades.Text;
                cadenafecIni = Txt_FechIni.Text;
                cadenafecFin = Txt_FechFin.Text;
                cadenafrecinsp = txt_Frecuencia_Inspe.Text;

                int resultado;
                Single resultado2;
                DateTime resultado3;

                //Este mensjae agrupa los errores
                string msj = "";

                if (Txt_FechIni.Text != "" && Txt_FechFin.Text != "" && Txt_NumOper.Text != "" && Txt_Unidades.Text != "" && Txt_Metas.Text != "" && Txt_Tolerancia.Text != "" && cbo_TpoPza.Text != "")
                {
                    if (DateTime.TryParse(cadenafecIni, out resultado3))
                    {
                        if (DateTime.TryParse(cadenafecFin, out resultado3))
                        {
                            if (int.TryParse(cadenaoperarios, out resultado))//evalua si el valor en el campo es del tipo correcto
                            {
                                if (int.TryParse(cadenaunidades, out resultado))//evalua si el valor en el campo es del tipo correcto
                                {
                                    if (Single.TryParse(cadenametas, out resultado2))//evalua si el valor en el campo es del tipo correcto
                                    {
                                        if (Single.TryParse(cadenatolerancia, out resultado2))//evalua si el valor en el campo es del tipo correcto
                                        {
                                            if (int.TryParse(cadenapieaza, out resultado))//evalua si el valor en el campo es del tipo correcto
                                            {
                                                if (Single.Parse(cadenatolerancia) >= 0 && Single.Parse(cadenatolerancia) <= 100)
                                                {
                                                    if (int.Parse(cadenapieaza) != 0)
                                                    {
                                                        if (int.TryParse(cadenafrecinsp, out resultado))
                                                        {                                                                                                              
                                                            objcontmeta.Met_Agregar_DetalleMetas(int.Parse(lblMperId.Text), Txt_FechIni.Text.ToString(), Txt_FechFin.Text.ToString(),
                                                                                                 int.Parse(Txt_NumOper.Text), int.Parse(Txt_Unidades.Text), float.Parse(Txt_Metas.Text),
                                                                                                 float.Parse(Txt_Tolerancia.Text), int.Parse(cbo_TpoPza.Text),int.Parse(txt_Frecuencia_Inspe.Text),
                                                                                                 lblusumetas.Text);
                                                        pnlCamposDetaMaes.Visible = false;
                                                        btn_Insertar_DetaMeta.Visible = false;
                                                        mensajeVentana("Registro Exitoso");
                                                        btn_CancelarDetaMeta.Visible = false;
                                                        Met_Limpiar_Campos_DetalleMeta();
                                                        btn_Agregar_DetaMeta.Visible = true;                                                       
                                                        lblDescripcionProce.Visible = true;
                                                        Consultar_Maestro_Metas_x_Planta();
                                                        Grid_Detalle_Maestro.DataSource = objcontmeta.Proc_Consultar_Detalle_MetasProduccion(int.Parse(lblMperId.Text));
                                                        Grid_Detalle_Maestro.DataMember = objcontmeta.Proc_Consultar_Detalle_MetasProduccion(int.Parse(lblMperId.Text)).Tables[0].ToString();
                                                        Grid_Detalle_Maestro.Visible = true;
                                                        Grid_Detalle_Maestro.DataBind();
                                                        }
                                                        else
                                                        {
                                                        mensajeVentana("El contenido del campo es incorrecto");
                                                            txt_Frecuencia_Inspe.Focus();
                                                        }
                                                    }
                                                    else
                                                    {
                                                    mensajeVentana("Debe elegir un tipo de pieza");
                                                        cbo_TpoPza.Focus();
                                                    }
                                                }
                                                else
                                                {
                                                mensajeVentana("La tolerancia de calidad, debe estar entre (0 y 100)");
                                                    Txt_Tolerancia.Focus();
                                                }
                                            }
                                            else
                                            {
                                            mensajeVentana("El contenido del campo es incorrecto");
                                                cbo_TpoPza.Focus();
                                            }
                                        }
                                        else
                                        {
                                        mensajeVentana("El contenido del campo es incorrecto");
                                            Txt_Tolerancia.Focus();
                                        }
                                    }
                                    else
                                    {
                                    mensajeVentana("El contenido del campo es incorrecto");
                                        Txt_Metas.Focus();
                                    }
                                }
                                else
                                {
                                mensajeVentana("El contenido del campo es incorrecto");
                                    Txt_Unidades.Focus();
                                }
                            }
                            else
                            {
                            mensajeVentana("El contenido del campo es incorrecto");
                                Txt_NumOper.Focus();
                            }

                        }
                        else
                        {
                        mensajeVentana("El contenido del campo Fecha final es incorrecto");
                        }
                    }
                    else
                    {
                    mensajeVentana("El contenido del campo Fecha inicial es incorrecto");
                    }
                }
                else
                {
                mensajeVentana("Debe digitar todos los campos");
                }
        //}
            //catch (System.Data.SqlClient.SqlException)
            //{
            //  lblMsgGridDetalle.Text = "Error Inesperado, pongase en contacto con el administrador";
            //}
}

        public void Met_Limpiar_Campos_DetalleMeta()
        {
            Txt_FechIni.Text = "";
            Txt_FechFin.Text = "";
            Txt_NumOper.Text = "";
            Txt_Unidades.Text = "";
            Txt_Metas.Text = "";
            Txt_Tolerancia.Text = "";  
            txt_Frecuencia_Inspe.Text="";

        }


        public void Met_Obtener_Descripcion_Proceso()
        {
            DataTable dt;
            dt = objcontmeta.Met_Obtener_Descripcion_Proceso(int.Parse(lblMperId.Text));
            lblDescripcionProce.Text = dt.Rows[0][0].ToString();
            lblDescripcionProce.Text = lblDescripcionProce.Text.ToUpper();
          
        }

        // habilita todos los controles para agregar una nueva meta
        protected void btn_Habilitar_pnlMetas_Click(object sender, EventArgs e)
        {
            btn_Insertar_DetaMeta.Visible = false;
            pnlCamposDetaMaes.Visible = false;
            Met_Limpiar_Campos_DetalleMeta();
            pnlIngMetas.Visible = true;
            Grid_Detalle_Maestro.Visible = false;
            GridView_Maestro_Metas.Visible = false;
            btn_Habilitar_pnlMetas.Visible = false;
            btn_Agregar_DetaMeta.Visible = false;
            btn_CanlarAgreMetas.Visible = true;
            btn_CancelarDetaMeta.Visible = false;
            Txt_Observacionpnlmeta.Text = "";
            lblDescripcionProce.Text = "";        
        }

        //consulta automaticamente el detallle de la primer meta que se  muestra en el gridview
        //detalle_Metas
        public void Met_Cargar_Detalle_Maestro(int planta)
        {
            try
            {
                Grid_Detalle_Maestro.Visible = true;
                GridView_Maestro_Metas.Visible = true;
                btn_Agregar_DetaMeta.Visible = true;

                int meprIdDetalle;
                DataTable dt;
                dt = objcontmeta.Met_Obtener_Max_MeprId(planta);
                meprIdDetalle = int.Parse(dt.Rows[0][0].ToString());
                lblMperId.Text = meprIdDetalle.ToString();

                if (objcontmeta.Proc_Consultar_Detalle_MetasProduccion(meprIdDetalle).Tables[0].Rows.Count != 0)
                {
                    btn_Agregar_DetaMeta.Visible = true;
                    Met_Obtener_Descripcion_Proceso();
                    Grid_Detalle_Maestro.DataSource = objcontmeta.Proc_Consultar_Detalle_MetasProduccion(meprIdDetalle);
                    Grid_Detalle_Maestro.DataMember = objcontmeta.Proc_Consultar_Detalle_MetasProduccion(meprIdDetalle).Tables[0].ToString();
                    Grid_Detalle_Maestro.Visible = true;
                    Grid_Detalle_Maestro.DataBind();        
                }
                else
                {
                    Grid_Detalle_Maestro.Visible = false;
                    btn_Agregar_DetaMeta.Visible = false;
                    lblDescripcionProce.Text = "";
                }
            }
            catch (System.FormatException)
            {
                mensajeVentana("No existen registros para esta planta");
                Grid_Detalle_Maestro.Visible = false;
                GridView_Maestro_Metas.Visible = false;
                btn_Agregar_DetaMeta.Visible = false;
                lblDescripcionProce.Text = "";
            }
        }
        protected void btn_Actualizar_Detalle_Click(object sender, EventArgs e)
        {
            pnlCamposDetaMaes.Visible = true;

        }

        protected void btn_CanlarAgreMetas_Click(object sender, EventArgs e)
        {
            lblMsgPrincipal.Text = "";
            pnlIngMetas.Visible = false;
            Consultar_Maestro_Metas_x_Planta();
            btnAgregarMeta.Visible = true;
            btn_Habilitar_pnlMetas.Visible = true;
            Met_Cargar_Detalle_Maestro(int.Parse(cboPlantas.Text));
        }

        //CRUD METAS PRODUCCION

        //Actualiza el maestro produccion metas
        protected void GridView_Maestro_Metas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string MepridUD, idProcesoUD, ObservUD;
            string campobservacion, campproceso;
            String fecha = DateTime.Now.ToShortDateString();
            string tabla = "Meta_Produccion";
            string evento = "U";
            int respuesta;
            string Meprid, idProceso, Observ;
            lblGridMetas.Text = "";
            TextBox txt = new TextBox();
            DropDownList cbo = new DropDownList();

            txt = (TextBox)GridView_Maestro_Metas.Rows[e.RowIndex].FindControl("Txt_Id_Mepr");
            Meprid = txt.Text;
            cbo = (DropDownList)GridView_Maestro_Metas.Rows[e.RowIndex].FindControl("cbo_IdProceso");
            idProceso = cbo.Text;
            txt = (TextBox)GridView_Maestro_Metas.Rows[e.RowIndex].FindControl("Txt_Observacion");
            Observ = txt.Text;

            //Se obtienen los valores de campos  de la tabla meta_produccion en el dt
            DataTable dt = null;
            dt = objcontmeta.Met_Consultar_Metas_log(int.Parse(Meprid));        
            idProcesoUD = dt.Rows[0][3].ToString();//valores antiguos para comparar con los ingresados por el usuario
            campproceso = dt.Columns[3].ColumnName.ToString();//obtiene el nombre del campo
            ObservUD = dt.Rows[0][1].ToString();//valores antiguos para comparar con los ingresados por el usuario
            campobservacion = dt.Columns[1].ColumnName.ToString();//obtiene el nombre del campo
            MepridUD = dt.Rows[0][0].ToString();//este valor solo es referencia para la insercion   


            if (Meprid != "" && idProceso != "" && Observ != "")
            {

                //Compara los valores rescatados de la BD con los digitados por el usuario
                if (idProcesoUD != idProceso)
                {
                    objcontmeta.Met_Insertar_Log_Update_Metas(tabla, int.Parse(MepridUD), campproceso, lblusumetas.Text, fecha, idProcesoUD, idProceso, evento);
                }
                else { }
                if (ObservUD != Observ)
                {
                    objcontmeta.Met_Insertar_Log_Update_Metas(tabla, int.Parse(MepridUD), campobservacion, lblusumetas.Text, fecha, ObservUD, Observ, evento);
                }
                else { }
                respuesta = objcontmeta.Proc_Actualizar_Meta_Produccion(int.Parse(Meprid), Observ, int.Parse(idProceso));
                GridView_Maestro_Metas.EditIndex = -1;
                Consultar_Maestro_Metas_x_Planta();
                btn_Habilitar_pnlMetas.Visible = true;
                btn_Agregar_DetaMeta.Visible = false;
                GridView_Maestro_Metas.Columns[3].Visible = true;// muestra la columna select
                GridView_Maestro_Metas.Columns[5].Visible = true;// muestra la columna delete

              mensajeVentana("Registro Actualizado Correctamente.");
            }
            else
            {
                mensajeVentana("No se aceptan campos vacios");
            }
        }
        //Habilita los campos para editar
   
        protected void GridView_Maestro_Metas_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
        {
            lblGridMetas.Text = "";
            btn_Agregar_DetaMeta.Visible = false;
            GridView_Maestro_Metas.Columns[3].Visible = true;// cuando se cancela la edicion se muestra la columna select
            GridView_Maestro_Metas.Columns[5].Visible = true;// cuando se cancela la edicion se muestra la columna delete
            btn_Habilitar_pnlMetas.Visible = true;
            btn_Agregar_DetaMeta.Visible = false;       
            try
            {
                GridView_Maestro_Metas.EditIndex = -1;
                Consultar_Maestro_Metas_x_Planta();
            }
            catch (Exception ex)
            {
                lblMsgPrincipal.Text = ex.Message.ToString();
            }
        }

        protected void GridView_Maestro_Metas_RowDeleting1(object sender, GridViewDeleteEventArgs e)
        {
            int meprId = Convert.ToInt32(GridView_Maestro_Metas.DataKeys[e.RowIndex].Value);   
            //metodo para guardar el log de eliminacion en la tabla meta_produccion--------------
            string fecha = DateTime.Now.ToShortDateString();
            string tabla = "Meta_Produccion";
            string evento = "D";            
            objcontmeta.Met_Insertar_Log_Delete_Metas(tabla, meprId, lblusumetas.Text, fecha, evento);
            //------------------------------------------------------------------------------------
            pnlCamposDetaMaes.Visible = false;
            btn_Insertar_DetaMeta.Visible = false;
            objcontmeta.Met_Eliminar_TodoDetalleMeta_Produccion(meprId);
            objcontmeta.Met_Eliminar_Meta_Produccion(meprId);
            Grid_Detalle_Maestro.DataSource = null;
            Grid_Detalle_Maestro.DataBind();
            Grid_Detalle_Maestro.Visible = false;
            btn_CanlarAgreMetas.Visible = false;
            btn_Habilitar_pnlMetas.Visible = true;
            btn_CancelarDetaMeta.Visible = false;
            btn_Agregar_DetaMeta.Visible = false;
            Grid_Detalle_Maestro.Visible = false;
            Consultar_Maestro_Metas_x_Planta();
            Grid_Detalle_Maestro.DataSource = null;
            lblDescripcionProce.Text = "";
            Grid_Detalle_Maestro.DataBind();
           GridView_Maestro_Metas.EditIndex = -1;
           mensajeVentana("Meta Eliminada Correctamente.");
        }

        //metodo que consulta y refresaca la gridview detalle maestro
        public void Met_Consultar_Detalle_MetasProduccion()
        {
            if (objcontmeta.Proc_Consultar_Detalle_MetasProduccion(int.Parse(lblMperId.Text)).Tables[0].Rows.Count != 0)
            {
                Grid_Detalle_Maestro.DataSource = objcontmeta.Proc_Consultar_Detalle_MetasProduccion(int.Parse(lblMperId.Text));
                Grid_Detalle_Maestro.DataMember = objcontmeta.Proc_Consultar_Detalle_MetasProduccion(int.Parse(lblMperId.Text)).Tables[0].ToString();
                Grid_Detalle_Maestro.Visible = true;
                Grid_Detalle_Maestro.DataBind();
                btn_Agregar_DetaMeta.Visible = true;
            }
            else
            {
                btn_Agregar_DetaMeta.Visible = false;
                Grid_Detalle_Maestro.Visible = false;
                Grid_Detalle_Maestro.DataSource = null;
            }
        }

        //CRUD DETALLE METAS PRODUCCION
        //Habilita los campos para editar
        protected void Grid_Detalle_Maestro_RowEditing1(object sender, GridViewEditEventArgs e)
        {       
                GridView_Maestro_Metas.Columns[3].Visible = false;
                GridView_Maestro_Metas.Columns[4].Visible = false;
                GridView_Maestro_Metas.Columns[5].Visible = false;
                Grid_Detalle_Maestro.Columns[10].Visible = false;
                btn_Habilitar_pnlMetas.Visible = false;
                btn_Agregar_DetaMeta.Visible = false;
                pnlIngMetas.Visible = false;           
                Grid_Detalle_Maestro.EditIndex = e.NewEditIndex;
                Proc_Consultar_Detalle_MetasProduccion_prueba();
                //objcontmeta.Proc_Consultar_Detalle_MetasProduccion(int.Parse(lblMperId.Text));                          
        }
        //cancela la edicion
        protected void Grid_Detalle_Maestro_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {              
            GridView_Maestro_Metas.Columns[3].Visible = true;
            GridView_Maestro_Metas.Columns[4].Visible = true;
            GridView_Maestro_Metas.Columns[5].Visible = true;
            Grid_Detalle_Maestro.Columns[10].Visible = true;
            btn_Habilitar_pnlMetas.Visible = true;
            btn_Agregar_DetaMeta.Visible = true;

            Grid_Detalle_Maestro.EditIndex = -1;
            Proc_Consultar_Detalle_MetasProduccion_prueba();      
        }

        //actualiza detalle metas producccion
        protected void Grid_Detalle_Maestro_RowUpdating(object sender, GridViewUpdateEventArgs f)
        {       
                //int respuesta;
                string fechIni, fechFin, numoper, unid, meta, tolecali, tippza, frecinspe, mpdeId;           

                TextBox txt = new TextBox();
                DropDownList cbo = new DropDownList();

                txt = (TextBox)Grid_Detalle_Maestro.Rows[f.RowIndex].FindControl("Txt_FechaIni");
                fechIni = txt.Text;
                txt = (TextBox)Grid_Detalle_Maestro.Rows[f.RowIndex].FindControl("Txt_FechaFin");
                fechFin = txt.Text;
                txt = (TextBox)Grid_Detalle_Maestro.Rows[f.RowIndex].FindControl("Txt_NumeOper");
                numoper = txt.Text;
                txt = (TextBox)Grid_Detalle_Maestro.Rows[f.RowIndex].FindControl("Txt_Unid");
                unid = txt.Text;
                txt = (TextBox)Grid_Detalle_Maestro.Rows[f.RowIndex].FindControl("Txt_Meta");
                meta = txt.Text;
                txt = (TextBox)Grid_Detalle_Maestro.Rows[f.RowIndex].FindControl("Txt_ToleCali");
                tolecali = txt.Text;
                cbo = (DropDownList)Grid_Detalle_Maestro.Rows[f.RowIndex].FindControl("Cbo_TipPza");
                tippza = cbo.Text;
                txt = (TextBox)Grid_Detalle_Maestro.Rows[f.RowIndex].FindControl("Txt_FrecCali");
                frecinspe = txt.Text;
                txt = (TextBox)Grid_Detalle_Maestro.Rows[f.RowIndex].FindControl("Txt_MpdeID");
                mpdeId = txt.Text;

                int resultado;
                float resultado2;
                Single resultado4;
                DateTime resultado3;

                if (fechIni != "" && fechFin != "" && numoper != "" && unid != "" && meta != "" && tolecali != "" && tippza != "")
                {
                    if (int.Parse(tippza) != 0)
                    {
                        if (DateTime.TryParse(fechIni, out resultado3))//evalua si el valor en el campo es del tipo correcto
                        {
                            if (DateTime.TryParse(fechFin, out resultado3))//evalua si el valor en el campo es del tipo correcto
                            {
                                if (int.TryParse(numoper, out resultado))//evalua si el valor en el campo es del tipo correcto
                                {
                                    if (int.TryParse(unid, out resultado))//evalua si el valor en el campo es del tipo correcto
                                    {
                                        if (float.TryParse(meta, out resultado2))//evalua si el valor en el campo es del tipo correcto
                                        {
                                            if (Single.TryParse(tolecali, out resultado4))//evalua si el valor en el campo es del tipo correcto
                                            {
                                                if (int.TryParse(tippza, out resultado))//evalua si el valor en el campo es del tipo correcto
                                                {
                                                    if (int.TryParse(frecinspe, out resultado))//evalua si el valor en el campo es del tipo correcto
                                                    {
                                                        if (float.Parse(tolecali) >= 0 && float.Parse(tolecali) <= 100)
                                                        {                                                           
                                                            objcontmeta.Proc_Actualizar_MetaProd_Detalle(fechIni.ToString(), fechFin.ToString(), int.Parse(numoper), int.Parse(unid), float.Parse(meta), float.Parse(tolecali), int.Parse(tippza), int.Parse(mpdeId), int.Parse(frecinspe), lblusumetas.Text);
                                                            Grid_Detalle_Maestro.EditIndex = -1;
                                                            btn_Habilitar_pnlMetas.Visible = true;
                                                            btn_Agregar_DetaMeta.Visible = true;
                                                            btn_Habilitar_pnlMetas.Visible = true;
                                                            GridView_Maestro_Metas.Columns[3].Visible = true;
                                                            GridView_Maestro_Metas.Columns[4].Visible = true;
                                                            GridView_Maestro_Metas.Columns[5].Visible = true;
                                                            Proc_Consultar_Detalle_MetasProduccion_prueba();
                                                            mensajeVentana("Registro Actualizado Correctamente.");
                                                            Grid_Detalle_Maestro.Columns[10].Visible = true;
                                                            lblDescripcionProce.Visible = true;
                                                        }
                                                        else
                                                        {
                                                        mensajeVentana("La torerancia debe estar entre 0% y 100%");
                                                        }
                                                    }
                                                    else
                                                    {
                                                    mensajeVentana("El contenido del campo Frecuencia Inspeccion es incorrecto");
                                                    }
                                                }
                                                else
                                                {
                                                mensajeVentana("El contenido del campo tipo Pieza es incorrecto");

                                                }
                                            }
                                            else
                                            {
                                            mensajeVentana("El contenido del campo tolerancia es incorrecto");
                                            }
                                        }
                                        else
                                        {
                                        mensajeVentana("El contenido del campo meta es incorrecto");
                                        }
                                    }
                                    else
                                    {
                                    mensajeVentana("El contenido del campo numero operario es incorrecto");
                                    }
                                }
                                else
                                {
                                mensajeVentana("El contenido del campo unidades es incorrecto");
                                }
                            }
                            else
                            {
                            mensajeVentana("El contenido del campo Fecha final es incorrecto");
                            }
                        }
                        else
                        {
                        mensajeVentana("El contenido del campo Fecha inicial es incorrecto");
                        }
                    }
                    else
                    {
                    mensajeVentana("Debe elegir un tipo de pieza");
                    }
                }
                else
                {
                   mensajeVentana("No se aceptan campos vacios");
                }
       
}
        protected void btn_Agregar_DetaMeta_Click(object sender, EventArgs e)
        {        
            pnlCamposDetaMaes.Visible = true;
            btn_Insertar_DetaMeta.Visible = true;
            btn_CancelarDetaMeta.Visible = true;
            Grid_Detalle_Maestro.Visible = false;
            lblDescripcionProce.Text = "";
            btn_Agregar_DetaMeta.Visible = false;           
            cbo_TpoPza.Items.Clear();
            ctrlPieza.Listar_Tipo_Pieza(cbo_TpoPza);
            Met_Obtener_Descripcion_Proceso();
            lblDescripcionProce.Visible = true;
            Obtener_Ultimo_DetalleMeta();
        }

        protected void Grid_Detalle_Maestro_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int mpdeID = Convert.ToInt32(Grid_Detalle_Maestro.DataKeys[e.RowIndex].Value);
            //metodo para guardar el log de eliminacion en la tabla meta_produccion--------------
            string fecha = DateTime.Now.ToShortDateString();
            string tabla = "Meta_Produccion";
            string evento = "D";
            objcontmeta.Met_Insertar_Log_Delete_Metas(tabla, mpdeID, lblusumetas.Text, fecha, evento);
            //------------------------------------------------------------------------------------
            objcontmeta.Met_Eliminar_DetalleMeta_Produccion(mpdeID);
            Proc_Consultar_Detalle_MetasProduccion();
            btn_Insertar_DetaMeta.Visible = false;
            Grid_Detalle_Maestro.EditIndex = -1;
            mensajeVentana("Detalle Meta Eliminado Correctamente.");         
        }

        protected void GridView_Maestro_Metas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            btn_Habilitar_pnlMetas.Visible = false;
            GridView_Maestro_Metas.Columns[3].Visible = false;
            GridView_Maestro_Metas.Columns[5].Visible = false;
            lblDescripcionProce.Text = "";
            GridView_Maestro_Metas.SelectedIndex = -1;
            GridView_Maestro_Metas.EditIndex = e.NewEditIndex;
            Consultar_Maestro_Metas_Prueba();
            Grid_Detalle_Maestro.Visible = false;
            btn_Agregar_DetaMeta.Visible = false;
            pnlCamposDetaMaes.Visible = false;
            btn_Insertar_DetaMeta.Visible = false;
            btn_CancelarDetaMeta.Visible = false;
        }

        protected void btn_CancelarDetaMeta_Click(object sender, EventArgs e)
        {
            btn_Insertar_DetaMeta.Visible = false;
            btn_CancelarDetaMeta.Visible = false;
            pnlCamposDetaMaes.Visible = false;
            Met_Limpiar_Campos_DetalleMeta();
            lblDescripcionProce.Text = "";
        }

       /*Recupera el ultimo detalle meta produccion registrado
        y lo establece en los campos de texto*/
        public void Obtener_Ultimo_DetalleMeta()
        {
            string fecha = DateTime.Now.ToString("dd/MM/yyyy");

            DataTable dt;
            dt = objcontmeta.Obtener_Ultimo_DetalleMeta();
            Txt_FechIni.Text = fecha;
            Txt_NumOper.Text = dt.Rows[0]["Mpde_NumOper"].ToString();
            Txt_Unidades.Text = dt.Rows[0]["Mpde_Unidades"].ToString();
            Txt_Metas.Text = dt.Rows[0]["Mpde_Meta"].ToString();
            Txt_Tolerancia.Text = dt.Rows[0]["Mpde_ToleCali"].ToString();
            cbo_TpoPza.Text = dt.Rows[0]["TpoPza_id"].ToString();
            txt_Frecuencia_Inspe.Text= dt.Rows[0]["Mpde_FrInCali"].ToString();
        }

        //Permite utilizar mensajes como alert
        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }

    }
}