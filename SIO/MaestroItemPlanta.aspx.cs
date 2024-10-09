using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaControl;
using System.Diagnostics;
using System.Data.OracleClient;
using System.Net;
using System.Net.Mail;
using System.Net.Security;

namespace SIO
{
    public partial class MaestroItemPlanta : System.Web.UI.Page
    {
        ControlMaestroItemPlanta cmIp = new ControlMaestroItemPlanta();
        SqlDataReader Oreader = null;
        SqlDataReader reader = null;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (Session["Rol"] != null)
                {

                    string itemPlantaRepId = Request.QueryString["item_planta_id_reporte"];
                    //200618
                    if (!String.IsNullOrEmpty(itemPlantaRepId))
                    {
                        Ocultar_Grupo_Item_Forsa(true);
                    }
                    else
                    {
                        Ocultar_Grupo_Item_Forsa(false);
                    }
                    //kp
                    txt_Dim1max.Attributes.Add("onKeyPress", " return validedecimal(event,this);");
                    txt_Dim1min.Attributes.Add("onKeyPress", " return validedecimal(event,this);");
                    txt_Dim2max.Attributes.Add("onKeyPress", " return validedecimal(event,this);");
                    txt_Dim2min.Attributes.Add("onKeyPress", " return validedecimal(event,this);");
                    txt_Dim3max.Attributes.Add("onKeyPress", " return validedecimal(event,this);");
                    txt_Dim3min.Attributes.Add("onKeyPress", " return validedecimal(event,this);");
                    txt_Dim4max.Attributes.Add("onKeyPress", " return validedecimal(event,this);");
                    txt_Dim4min.Attributes.Add("onKeyPress", " return validedecimal(event,this);");
                    txt_Dim5max.Attributes.Add("onKeyPress", " return validedecimal(event,this);");
                    txt_Dim5min.Attributes.Add("onKeyPress", " return validedecimal(event,this);");
                    txt_Dim6max.Attributes.Add("onKeyPress", " return validedecimal(event,this);");
                    txt_Dim6min.Attributes.Add("onKeyPress", " return validedecimal(event,this);");
                    txt_Dim7max.Attributes.Add("onKeyPress", " return validedecimal(event,this);");
                    txt_Dim7min.Attributes.Add("onKeyPress", " return validedecimal(event,this);");
                    Habilitar_Campos_Accesorio(false);
                    ;

                    trcboperfil.Visible = false;
                    trlblperfil.Visible = false;
                    CargarPerfil();
                    CargarGrupo();
                    CargarPlanta(e);
                    CargarClaseItem();
                    //CargarReporte();
                    CargarIdioma();
                    //CargarMoneda();
                    CargarPrecioIp();
                    CargarPrecioDelete();
                    titulofactor.Visible = false;
                    factor.Visible = false;
                    trLongitudIp.Visible = false;
                    trM21Ip.Visible = false;
                    cboAgrupadorIp.Items.Clear();
                    cboAgrupadorIp.Items.Add(new ListItem(" ", " "));
                    cboAgrupadorIp.SelectedIndex = 0;
                    cboiplantacreados.Items.Clear();
                    cboiplantacreados.Items.Add(new ListItem(" ", " "));
                    cboiplantacreados.SelectedIndex = 0;
                    cboTipInvIp.Items.Add(new ListItem(" ", " "));
                    cboGrupimpIp.Items.Add(new ListItem(" ", " "));
                    cboPrincipalIp.Items.Add(new ListItem(" ", " "));
                    cboAdicionalIp.Items.Add(new ListItem(" ", " "));
                    cboOrdenIp.Items.Add(new ListItem(" ", " "));
                    cboPlan1Ip.Items.Add(new ListItem(" ", " "));
                    cboPlan2Ip.Items.Add(new ListItem(" ", " "));
                    cboPlan3Ip.Items.Add(new ListItem(" ", " "));
                    // Debug.WriteLine("Inicia maestro item");
                    //ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(cboPerfilIp);
                    Session.Add("item_planta_id", "0");
                    Session.Add("item_id", "0");
                    Session.Add("PlantaID", "0");
                    chkListorigen.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
                    chkListusoIp.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
                    txtTrm.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
                    chkkamban.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
                    chkListaRequiereIa.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
                    txtPrecioPlenoIp.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
                    txtDscAbrvIp.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
                    cboGrupoIp.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
                    cboClaseItem.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
                    txtDscIp.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
                    chkListaDisponiblesIa.Attributes.Add("onkeydown", "return (event.keyCode!=13);");
                    trlblobservacion.Visible = false;
                    trtxtobservacion.Visible = false;
                    chkListusoIp.Items[0].Selected = true;
                    chkListusoIp.Items[1].Selected = true;
                    chkListusoIp.Items[2].Selected = true;
                    chk_InspCalidad.Checked = true;
                    CargarGrupoItem();
                    CargarTipoOrden();
                    CargarParametro();
                    CargarGridParametro();
                    trcomboparametros.Visible = true; tdrequiereItem.Visible = true; trgridparametros.Visible = true;

                    if (Request.QueryString["item_planta_id_reporte"] != null && !Request.QueryString["item_planta_id_reporte"].ToString().Equals("0"))
                    {

              

                        if (Request.QueryString["id"] != null && !Request.QueryString["id"].ToString().Equals("0"))
                    {
                        limpiarItem();
                        Session["item_id"] = Request.QueryString["id"];
                        CargarDatosItem(Convert.ToInt64(Session["item_id"].ToString()));
                        if (lblactivoI.Text.Equals("Activo"))
                        {
                            btnGuardarIa.Text = "Editar";
                            CargarItemForsa(e);
                        }
                        if (lblactivoI.Text.Equals("Inactivo"))
                        {
                            btnGuardarIa.Visible = false;
                        }
                    }

                    if (Request.QueryString["item_planta_id_reporte"] != null && !Request.QueryString["item_planta_id_reporte"].ToString().Equals("0"))
                        {
                            Session["item_planta_id"] = Request.QueryString["item_planta_id_reporte"];
                        cboPerfilIp_SelectedIndexChanged(cboPerfilIp, e);
                        Cargarseleccionado(e);
                        //kp
                  
                        bool disp_ingenieria = chkListaDisponiblesIa.Items[3].Selected;
                        if (disp_ingenieria == true)
                        {
                            Habilitar_Campos_Accesorio(true);
                        }
                        else
                        {
                            Habilitar_Campos_Accesorio(false);
                        }
                   
                        if (cmIp.Consultar_Detalle_Accesorio(int.Parse(Session["item_planta_id"].ToString()), int.Parse(cboPlantaIp.SelectedValue.ToString())).Rows.Count != 0)
                            {
                          
                                Consultar_Detalle_Accesorio();                        
                                Consultar_Accesorios();
                                if (LblAnulado.Text == "False")
                                {                                                       
                                    btn_Anular.Visible = Visible;
                                btn_Anular.Text = "Anular";
                                LblEstadoAnula.Text = "Activo";
                                }
                                else if (LblAnulado.Text == "True")
                                {
                                btn_Anular.Visible = Visible;
                                btn_Anular.Text = "Habilitar";
                                LblEstadoAnula.Text = "Inactivo";
                            }                      
                        }
                            else
                            {                                                
                                Consultar_Accesorios();
                                btn_Anular.Visible = false;
                        }
                        }
                    }

                    //if (Request.QueryString["id_planta"] != null && !Request.QueryString["id_planta"].ToString().Equals("0"))
                    //{

                    //    Session["item_planta_id"] = Request.QueryString["id_planta"];

                    //}
                    trundadiccionales.Visible = true;
                    trcomboparametros.Visible = true; tdrequiereItem.Visible = true; trgridparametros.Visible = true;
                }
                else
            {
                        string mensaje = "Sesion Expiro. Por favor inicie sesión nuevamente. Gracias!";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        Response.Redirect("Inicio.aspx");
                    }
                }

        }

        private void CargarPrecioDelete()
        {
            DataTable table = new DataTable();
            DataColumn column;
            // primero columna para item 1   
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "item1";
            table.Columns.Add(column);
            Session.Add("eliminados_precio", table);
        }


        private void Cargarseleccionado(EventArgs e)
        {
            System.Threading.Thread.Sleep(2000);
            string usu_select = Session["usu_select"].ToString();
            string estado_select = Session["estado_select"].ToString();
            bool activo = Convert.ToBoolean(Session["activo"].ToString());
            if (cboPerfilIp.SelectedItem.Value.Equals("3"))
            {
                if (estado_select.Equals("2") || estado_select.Equals("1") || estado_select.Equals("4") || estado_select.Equals("5"))
                {
                    CargarDatos((string)Session["item_planta_id"], e);
                    CargarIdiomaEditado(Session["item_planta_id"].ToString());
                    CargarPrecioEditado(Session["item_planta_id"].ToString());
                    if (estado_select.Equals("5"))
                    {
                        btnduplicar.Visible = true;
                    }
                    if (estado_select.Equals("4"))
                    {
                        btnGuardarIp.Visible = false;
                    }
                    trlblobservacion.Visible = true;
                    trtxtobservacion.Visible = true;
                    btnEnviarIp.Visible = true;
                    btnLimpiarIp.Visible = false;
                    btnGuardarIp.Text = "Editar";
                    btnGuardarIp.Visible = true;
                }
                if (!Session["usuario"].ToString().Equals(usu_select) && (estado_select.Equals("9")))
                {
                    CargarDatos((string)Session["item_planta_id"], e);
                    CargarIdiomaEditado(Session["item_planta_id"].ToString());
                    CargarPrecioEditado(Session["item_planta_id"].ToString());
                    btnGuardarIp.Visible = false;
                    btnLimpiarIp.Visible = false;
                    trlblobservacion.Visible = true;
                    txtobservestado.Enabled = false;
                    trtxtobservacion.Visible = true;
                }

            }
            if (cboPerfilIp.SelectedItem.Value.Equals("1"))
            {
                if (estado_select.Equals("9"))
                {
                   
                   CargarDatos((string)Session["item_planta_id"], e);
                    CargarIdiomaEditado(Session["item_planta_id"].ToString());
                    CargarPrecioEditado(Session["item_planta_id"].ToString());
                    AccordionPrincipalIp.SelectedIndex = 0;
                    trlblobservacion.Visible = true;
                    trtxtobservacion.Visible = true;
                    btnAprobarIp.Visible = true;
                    btnDevolverIp.Visible = true;
                    btnRechazarIp.Visible = true;
                    btnGuardarIp.Visible = true;
                    btnGuardarIp.Text = "Editar";

                }
                if ((estado_select.Equals("1") || estado_select.Equals("3")))
                {
                    CargarDatos((string)Session["item_planta_id"], e);
                    CargarIdiomaEditado(Session["item_planta_id"].ToString());
                    CargarPrecioEditado(Session["item_planta_id"].ToString());
                    AccordionPrincipalIp.SelectedIndex = 0;
                    if (estado_select.Equals("3"))
                    {
                        trlblobservacion.Visible = true;
                        txtobservestado.Enabled = false;
                        trtxtobservacion.Visible = true;
                    }
                    btnGuardarIp.Visible = true;
                    btnGuardarIp.Text = "Editar";
                    btnEnviarIp.Visible = true;

                }
                if ((estado_select.Equals("3") || estado_select.Equals("4") || estado_select.Equals("5")))
                {
                    CargarDatos((string)Session["item_planta_id"], e);
                    CargarIdiomaEditado(Session["item_planta_id"].ToString());
                    CargarPrecioEditado(Session["item_planta_id"].ToString());
                    AccordionPrincipalIp.SelectedIndex = 0;
                    btnGuardarIp.Text = "Editar";
                    btnGuardarIp.Visible = true;
                    if (estado_select.Equals("5"))
                    {
                        btnduplicar.Visible = true;
                    }
                    if (estado_select.Equals("4"))
                    {
                        btnGuardarIp.Visible = false;
                    }
                    trlblobservacion.Visible = true;
                    txtobservestado.Enabled = false;
                    trtxtobservacion.Visible = true;
                }
                if ((estado_select.Equals("2") || estado_select.Equals("4") || estado_select.Equals("5") || estado_select.Equals("9")))
                {
                    CargarDatos((string)Session["item_planta_id"], e);
                    CargarIdiomaEditado(Session["item_planta_id"].ToString());
                    CargarPrecioEditado(Session["item_planta_id"].ToString());
                    AccordionPrincipalIp.SelectedIndex = 0;
                  

                    if (estado_select.Equals("5"))
                    {
                        btnGuardarIp.Text = "Editar";
                        btnGuardarIp.Visible = true;
                        btnduplicar.Visible = true;
                    }
                    trlblobservacion.Visible = true;
                    txtobservestado.Enabled = false;
                    trtxtobservacion.Visible = true;
                    btnGuardarIp.Text = "Editar";

                }
            }
            if (cboPerfilIp.SelectedItem.Value.Equals("2") || cboPerfilIp.SelectedItem.Value.Equals("0"))
            {

                if (Session["usuario"].ToString().Equals(usu_select) && (estado_select.Equals("1") || estado_select.Equals("3")))
                {
                    CargarDatos((string)Session["item_planta_id"], e);
                    CargarIdiomaEditado(Session["item_planta_id"].ToString());
                    CargarPrecioEditado(Session["item_planta_id"].ToString());
                    AccordionPrincipalIp.SelectedIndex = 0;
                    if (estado_select.Equals("3"))
                    {
                        trlblobservacion.Visible = true;
                        txtobservestado.Enabled = false;
                        trtxtobservacion.Visible = true;
                    }
                    btnGuardarIp.Text = "Editar";
                    btnEnviarIp.Visible = true;
                }
                if (Session["usuario"].ToString().Equals(usu_select) && (estado_select.Equals("2") || estado_select.Equals("4") || estado_select.Equals("5") || estado_select.Equals("9")))
                {
                    CargarDatos((string)Session["item_planta_id"], e);
                    CargarIdiomaEditado(Session["item_planta_id"].ToString());
                    CargarPrecioEditado(Session["item_planta_id"].ToString());
                    AccordionPrincipalIp.SelectedIndex = 0;
                    btnGuardarIp.Visible = false;
                    trlblobservacion.Visible = true;
                    txtobservestado.Enabled = false;
                    trtxtobservacion.Visible = true;
                }
                if (!Session["usuario"].ToString().Equals(usu_select))
                {

                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mensaje", "MensajeError( 'No posee permisos para editar este Item Planta!')", true);
                }

            }
        }

        private void CargarGridParametro()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("item_parametro_id");
            dt.Columns.Add("item_tipo_parametro_id");
            dt.Columns.Add("N°");
            dt.Columns.Add("Descripcion");
            dt.Columns.Add("Valor");
            dt.Columns.Add("TRM");

            grdParametrosIa.DataSource = dt;
            grdParametrosIa.DataBind();
            Session.Add("Tb_Parametros", dt);

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("idborrarparametro", typeof(string));
            Session.Add("Tb_Parametros_Delete", dt2);
        }
        /***********************ITEM FORSA *********************/
        private void CargarGrupoItem()
        {
            cboGrupoIa.Items.Clear();
            reader = cmIp.PoblarGrupo();
            if (reader.HasRows == true)
            {
                cboGrupoIa.Items.Add(new ListItem("Seleccione el Grupo", " "));
                while (reader.Read())
                {
                    cboGrupoIa.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            cboGrupoIa.SelectedIndex = 0;
            reader.Close();
            reader.Dispose();
            cmIp.CerrarConexion();
            trundadiccionales.Visible = true;
        }

        /**************METODO PARA CARGAR EL COMBO DE PARAMETROS***************/
        protected void CargarParametro()
        {
            cboParametroIa.Items.Clear();
            reader = cmIp.CargarParametro();
            if (reader.HasRows == true)
            {
                cboParametroIa.Items.Add(new ListItem("Seleccione el Parametro", " "));
                while (reader.Read())
                {
                    cboParametroIa.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            cboParametroIa.SelectedIndex = 0;
            reader.Close();
            reader.Dispose();
            cmIp.CerrarConexion();
        }

        /***********************ITEM FORSA *********************/

        private void CargarUnidadMedida()
        {
            //UM  principal
            //se carga de oracle las unidades de medida para principal
            reader = cmIp.SugerirUP1E(int.Parse(cboPlantaIp.SelectedItem.Value));
            cboPrincipalIp.Items.Clear();
            cboPrincipalIp.Items.Add(new ListItem("Seleccione ", " "));
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboPrincipalIp.Items.Add(new ListItem(reader.GetString(0), reader.GetString(0)));
                }
            }
            cboPrincipalIp.SelectedIndex = 0;
            //se carga de oracle las unidades de medida para adicional
            reader.Close();
            reader.Dispose();
            cmIp.CerrarConexion();

            reader = cmIp.SugerirUP1E(int.Parse(cboPlantaIp.SelectedItem.Value));
            cboAdicionalIp.Items.Clear();
            cboAdicionalIp.Items.Add(new ListItem("Seleccione ", " "));
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboAdicionalIp.Items.Add(new ListItem(reader.GetString(0), reader.GetString(0)));
                }
            }
            cboAdicionalIp.SelectedIndex = 0;
            //se carga de oracle las unidades de medida para orden
            reader.Close();
            reader.Dispose();
            cmIp.CerrarConexion();

            reader = cmIp.SugerirUP1E(int.Parse(cboPlantaIp.SelectedItem.Value));
            cboOrdenIp.Items.Clear();
            cboOrdenIp.Items.Add(new ListItem("Seleccione ", " "));
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboOrdenIp.Items.Add(new ListItem(reader.GetString(0), reader.GetString(0)));
                }
            }
            cboOrdenIp.SelectedIndex = 0;
            txtPrincipalIp.Text = string.Empty;
            txtAdicionalIp.Text = string.Empty;
            txtOrdenIp.Text = string.Empty;
            reader.Close();
            reader.Dispose();
            cmIp.CerrarConexion();
        }

        /**************SE CARGA EL PERFIL ASOCIADOS AL ROL*********************/
        private void CargarPerfil()
        {
            cboPerfilIp.Items.Clear();
            cboPerfilIp.DataSource = cmIp.PoblarPerfil(Session["Usuario"].ToString());
            cboPerfilIp.DataValueField = "Key";
            cboPerfilIp.DataTextField = "Value";
            cboPerfilIp.DataBind();
            cmIp.CerrarConexion();
            cboPerfilIp.SelectedIndex = 0;
            if (cboPerfilIp.Items.Count > 1)
            {
                trcboperfil.Visible = true;
                trlblperfil.Visible = true;
            }
            if (cboPerfilIp.SelectedItem.Value.Equals("2") || cboPerfilIp.SelectedItem.Value.Equals("0"))
            {
                titleuso.Text = "Uso:";
                TbItem.Visible = true;
                cvchkListusoIp.Enabled = false;
                trTipInvIp.Visible = true;
                trGrupimpIp.Visible = false;
                trAdicionalIp.Visible = false;
                trOrdenIp.Visible = false;
                trplan1.Visible = false;
                trplan2.Visible = false;
                //trcreadoitem.Visible = false;
                tdGruPlantaIp.Visible = false;
                tdlblGruPlantaIp.Visible = false;
                tdcriterio.Visible = false;
                AccordPanePrecioIp.Visible = false;
                trundadiccionales.Visible = true;
                lblTitleUnddIp.Visible = true;
                trprincipalip.Visible = true;
                trGrupoIp.Visible = false;
                chkCodigoERP.Visible = false;

            }
            if (cboPerfilIp.SelectedItem.Value.Equals("1"))
            {

                titleuso.Text = "*Uso:";
                cvchkListusoIp.Enabled = true;
                TbItem.Visible = true;
                trTipInvIp.Visible = true;
                trGrupimpIp.Visible = true;
                trAdicionalIp.Visible = true;
                trOrdenIp.Visible = true;
                trplan1.Visible = true;
                trplan2.Visible = true;
                trGrupoIp.Visible = true;
                trcreadoitem.Visible = true;
                tdGruPlantaIp.Visible = true;
                tdlblGruPlantaIp.Visible = true;
                tdcriterio.Visible = true;
                AccordPanePrecioIp.Visible = true;
                trundadiccionales.Visible = true;
                trlistuso.Style.Add("display", "block");
                lblTitleUnddIp.Visible = true;
                trprincipalip.Visible = true;
                chkCodigoERP.Visible = true;

            }
            if (cboPerfilIp.SelectedItem.Value.Equals("3"))
            {
                AccordionPrincipalIp.SelectedIndex = 1;
                titleuso.Text = "Uso:";
                TbItem.Visible = true;
                cvchkListusoIp.Enabled = false;
                trTipInvIp.Visible = true;
                trGrupimpIp.Visible = true;
                lblTitleUnddIp.Visible = true;
                trprincipalip.Visible = true;
                trAdicionalIp.Visible = true;
                trOrdenIp.Visible = true;
                AccordPanePrecioIp.Visible = false;
                trundadiccionales.Visible = true;
                trplan1.Visible = false;
                trplan2.Visible = false;
                trcreadoitem.Visible = false;
                tdGruPlantaIp.Visible = true;
                tdlblGruPlantaIp.Visible = true;
                tdcriterio.Visible = false;
                AccordPaneIdiomaIp.Visible = false;
                trpeso.Visible = false;
                cboGrupoIp.Enabled = true;
                cboClaseItem.Enabled = true;
                cboAgrupadorIp.Enabled = true;
                cboPlantaIp.Enabled = true;
                txtDscAbrvIp.Enabled = true;
                txtDscIp.Enabled = true;
                chkListorigen.Enabled = true;
                if (btnGuardarIp.Text.Equals("Guardar"))
                {
                    btnGuardarIp.Visible = true;
                }
                btnLimpiarIp.Visible = false;
                trGrupoIp.Visible = true;
                chkCodigoERP.Visible = false;
            }
            Session["perfil_usu"] = cboPerfilIp.SelectedItem.Value;
        }

        /**************SE CARGA EL ITEM GRUPO*********************/
        private void CargarGrupo()
        {
            cboGrupoIp.Items.Clear();
            reader = cmIp.PoblarGrupo();
            cboGrupoIp.Items.Add(new ListItem("Seleccione el Grupo", " "));
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboGrupoIp.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            cboGrupoIp.SelectedIndex = 0;
            reader.Close();
            reader.Dispose();
            cmIp.CerrarConexion();
            trundadiccionales.Visible = true;

        }
        /**************SE CARGA LA CLASE ITEM**************************************/
        private void CargarClaseItem()
        {
            cboClaseItem.Items.Clear();
            reader = cmIp.PoblarClaseItem();
            cboClaseItem.Items.Add(new ListItem("Seleccione la Clase Item", " "));
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboClaseItem.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
           
            reader.Close();
            reader.Dispose();
            cmIp.CerrarConexion();
            cboClaseItem.SelectedIndex = 0;
        }
        /**************METODO CUANDO CAMBIA EL BALOR DE LA CLASE ITEM*************/
        protected void cboClaseItem_SelectedIndexChange(object sender, EventArgs e)
        {
            cboClaseItem.Items.Clear();
            cboClaseItem.Items.Add(new ListItem(" ", " "));
            cboClaseItem.SelectedIndex = 0;
            if(cboClaseItem.SelectedIndex != 0)
            {
                CargarClaseItem();
            }

        }
     
        /**************METODO CUANDO CAMBIA EL VALOR DE grupo*********************/
        protected void cboGrupoIp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //mensajeVentana(cbGrupo.SelectedItem.Value);
            cboAgrupadorIp.Items.Clear();
            cboAgrupadorIp.Items.Add(new ListItem(" ", " "));
            cboAgrupadorIp.SelectedIndex = 0;
            if (cboGrupoIp.SelectedIndex != 0)
            {
                CargarItemAgrupador();
            }
            txtGruPlantaIp.Text = string.Empty;
            if (cboPlantaIp.SelectedIndex != 0 && cboGrupoIp.SelectedIndex != 0)
            {
                CargarGrupoPlanta();
            }


        }
        /**************SE CARGAR EL ITEM GRUPADOR SOLO SI EL GRUPO TOMO UN VALOR*********************/
        private void CargarItemAgrupador()
        {
            cboAgrupadorIp.Items.Clear();
            reader = cmIp.PoblarItemAgrupador(Int32.Parse(cboGrupoIp.SelectedItem.Value));
            cboAgrupadorIp.Items.Add(new ListItem("Seleccione el Item Forsa", " "));
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboAgrupadorIp.Items.Add(new ListItem(reader.GetString(1), reader.GetInt64(0).ToString()));
                }
            }
            cboAgrupadorIp.SelectedIndex = 0;
            reader.Close();
            reader.Dispose();
            cmIp.CerrarConexion();
        }
        /**************SE CARGA LAS PLANTAS A SOCIADAS AL USUARIO LOGUEADO *********************/
        private void CargarPlanta(EventArgs e)
        {
            cboPlantaIp.Items.Clear();
            reader = cmIp.PoblarPlanta(Session["Usuario"].ToString());
            cboPlantaIp.Items.Add(new ListItem("Seleccione la Planta", "0"));
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboPlantaIp.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            cboPlantaIp.SelectedIndex = 0;
            if (cboPlantaIp.Items.Count == 2)
            {
                cboPlantaIp.SelectedIndex = 1;
                cboPlantaIp_SelectedIndexChanged(cboPlantaIp, e);
            }
            reader.Close();
            reader.Dispose();
            cmIp.CerrarConexion();

        }

        /**************METODO CUANDO CAMBIA EL VALOR DE planta*********************/
        protected void cboPlantaIp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //mensajeVentana(cbPlanta.SelectedItem.Value);
            txtGruPlantaIp.Text = string.Empty; string undpal = " ";
            if (cboPlantaIp.SelectedIndex != 0 && cboGrupoIp.SelectedIndex != 0)
            {
                CargarGrupoPlanta();
            }
            if (cboPlantaIp.SelectedIndex != 0)
            {
                if (cboPrincipalIp.SelectedIndex != 0)
                {
                    undpal = cboPrincipalIp.SelectedValue;
                }
                CargarUnidadMedida();
                cboPrincipalIp.SelectedValue = undpal;
                CargarMoneda();
                poblarGrupoImpositivo(int.Parse(cboPlantaIp.SelectedItem.Value));
                poblarInventario(int.Parse(cboPlantaIp.SelectedItem.Value));
            }

            Session["PlantaID"] = cboPlantaIp.SelectedValue;
            chkListorigen.ClearSelection();
            chkListorigen.Items[0].Enabled = true;
            chkListorigen.Items[1].Enabled = true;
            chkListorigen.Items[2].Enabled = true;
            //cboTipInvIp.Items.Clear();
            //cboTipInvIp.Items.Add(new ListItem(" ", " "));
            //txtTipInvIp.Text = string.Empty;
            //cboGrupimpIp.Items.Clear();
            //cboGrupimpIp.Items.Add(new ListItem(" ", " "));
            //txtGrupimpIp.Text = string.Empty;
            titulofactor.Visible = false;
            factor.Visible = false;
            trM21Ip.Visible = false;
            trLongitudIp.Visible = false;
            cboPlan1Ip.Items.Clear();
            cboPlan2Ip.Items.Clear();
            cboPlan3Ip.Items.Clear();
            cboPlan1Ip.Items.Add(new ListItem(" ", " "));
            cboPlan2Ip.Items.Add(new ListItem(" ", " "));
            cboPlan3Ip.Items.Add(new ListItem(" ", " "));
            cboGrupimpIp.Enabled = true;
            txtPosAranIp.Text = string.Empty;
            chkListusoIp.Items[0].Selected = true;
            chkListusoIp.Items[1].Selected = true;
            chkListusoIp.Items[2].Selected = true;

        }

        /**************SE CARGA  grupo asociado a planta *********************/
        private void CargarGrupoPlanta()
        {
            reader = cmIp.PoblarGrupoPlanta(Int32.Parse(cboPlantaIp.SelectedItem.Value), Int32.Parse(cboGrupoIp.SelectedItem.Value));

            if (reader.HasRows)
            {
                reader.Read();
                txtGruPlantaIpId.Text = "";
                txtGruPlantaIpId.Text = reader.GetValue(0).ToString();
                txtGruPlantaIp.Text = reader.GetValue(1).ToString();
            }

            reader.Close();
            reader.Dispose();
            cmIp.CerrarConexion();
        }
        /**************SE CARGA  LA TABLA IDIOMA *********************/
        private void CargarIdioma()
        {
            DataTable Tabla_fill = new DataTable();
            Tabla_fill = cmIp.PoblarIdioma();
            DataTable dt1 = Session["TbReporte"] as DataTable;
            Session.Add("TabIdioma", Tabla_fill);
            grdIdiomaIp.DataSource = Tabla_fill;
            grdIdiomaIp.DataBind();
            cmIp.CerrarConexion();

        }

        /**************METODO DE CAMBIAR DE PAGINA UTILIZADO PARA EL GRIDVIEW DE IDIOMA *********************/
        protected void grdIdiomaIp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdIdiomaIp.PageIndex = e.NewPageIndex;
            Reload_tbIdioma();


        }
        /**************METODO DE RECARGAR DATASOURCE DEL GRIDVIEW DE IDIOMA *********************/
        private void Reload_tbIdioma()
        {

            DataTable dt = Session["TabIdioma"] as DataTable;
            grdIdiomaIp.DataSource = dt;
            grdIdiomaIp.DataBind();

            // Debug.WriteLine(ViewState["TabIdioma"] as DataTable);
        }
        /**************SE CARGA  MONEDAS *********************/
        private void CargarMoneda()
        {
            if (cboPlantaIp.SelectedIndex != 0)
            {
                txtmonedaip.Text = string.Empty;
                reader = cmIp.PoblarMoneda(Convert.ToInt32(cboPlantaIp.SelectedItem.Value));
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        txtmonedaip.Text = reader.GetString(0);
                    }
                }

                reader.Close();
                reader.Dispose();
                cmIp.CerrarConexion();
                CargarPrecioIp();
                txtPrecioPlenoIp.Text = string.Empty;
                txtTrm.Text = string.Empty;
                DataTable dt = Session["TbPrecio"] as DataTable;
                DataTable Tabla_fill = new DataTable();
                Tabla_fill = cmIp.PoblarClientetipoNal(int.Parse(cboPlantaIp.SelectedItem.Value));
                foreach (DataRow r in Tabla_fill.Rows)
                {

                    DataRow row = dt.NewRow();
                    row["item_planta_precio_id"] = "";
                    row["moneda_id"] = r["nal"].ToString();
                    row["moneda"] = r["dsc1"].ToString();
                    row["margen"] = r["porcentaje"].ToString();
                    row["cliente_tipo"] = r["descripcion"].ToString();
                    row["cliente_tipo_planta_id"] = r["cliente_tipo_planta_id"].ToString();
                    dt.Rows.Add(row);

                }

                dt.AcceptChanges();
                Session.Add("TbPrecio", dt);
                Reload_tbPrecio();
                cmIp.CerrarConexion();
            }
        }
        /**************SE CARGA  LA TABLA DE PRECIO *********************/
        private void CargarPrecioIp()
        {
            DataTable dt = new DataTable();
            // ESTAS COLUMNAS SOLO LAS NOMBRO ASI PARA QUE MI DATATABLE SEA DE LA MISMA ESTRUCTURA QUE MI GRIDVIEW
            dt.Columns.Add("N°");
            dt.Columns.Add("item_planta_precio_id");
            dt.Columns.Add("moneda_id");
            dt.Columns.Add("moneda");
            dt.Columns.Add("valor");
            dt.Columns.Add("cliente_tipo_planta_id");
            dt.Columns.Add("margen");
            dt.Columns.Add("cliente_tipo");
            dt.Columns.Add("Costo");
            dt.Columns.Add("TRM");
            dt.AcceptChanges();
            Session.Add("TbPrecio", dt);
            grdPrecioIp.DataSource = dt;
            grdPrecioIp.DataBind();
        }

        /*************METODO QUE ADICIONA UNA NUEVA FILA AL GRIDVIEW PRECIO *********************/
        private void addPrecio()
        {
            string precio = ((TextBox)grdPrecioIp.Rows[0].FindControl("txt_valor")).Text;
            if (grdPrecioIp.Rows.Count != 0 && !precio.Equals(""))
            {
                mensajeVentana("Debe limpiar para ingresar un nuevo costo a calcular!!");
            }
            else
            {

                DataTable Tabla_fill = new DataTable();
                decimal trm = 0;
                string Costo = "";
                decimal z = 0;
                string CostoPleno = "";


                if (cboPlantaIp.SelectedIndex != 0)
                {
                    DataTable dt = Session["TbPrecio"] as DataTable;
                    int page = grdPrecioIp.PageIndex;
                    int cont = 0;
                    for (int i = 0; i < grdPrecioIp.PageCount; i++)
                    {
                        grdPrecioIp.SetPageIndex(i);
                        // se valida que que no se encuentre repetido el valor
                        foreach (GridViewRow row in grdPrecioIp.Rows)
                        {
                            Label lblcliente_tipo_planta_id = ((Label)grdPrecioIp.Rows[row.RowIndex].FindControl("lblcliente_tipo_planta_id"));
                            string cliente_tipo_planta_id = lblcliente_tipo_planta_id.Text;
                            string porcentaje = ((TextBox)grdPrecioIp.Rows[row.RowIndex].FindControl("txtporcentaje")).Text;

                            // CONSULTA EL PORCENTAJE DE PLENO PARA EL CALCULO DE DISTRIBUIDOR
                            if (cliente_tipo_planta_id == "1" || cliente_tipo_planta_id == "6" || cliente_tipo_planta_id == "11")
                            {
                                Session["porcPleno"] = ((TextBox)grdPrecioIp.Rows[row.RowIndex].FindControl("txtporcentaje")).Text; ;
                            }

                            // CALCULA PRECIO PARA DISTRIBUIDOR POR TENER CALCULO DIFERENTE
                            if (cliente_tipo_planta_id == "2" || cliente_tipo_planta_id == "7" || cliente_tipo_planta_id == "12")
                            {
                                z = CalcularPorcentajeDistribuidor(decimal.Parse(txtPrecioPlenoIp.Text), decimal.Parse(porcentaje),Convert.ToDecimal(Session["porcPleno"]));
                            }
                            else
                            {
                                z = CalcularPorcentaje(decimal.Parse(txtPrecioPlenoIp.Text), decimal.Parse(porcentaje));
                            }

                            

                            dt.Columns["valor"].ReadOnly = false;
                            dt.Columns["valor"].MaxLength = 200;
                            dt.Rows[cont]["valor"] = z;
                            dt.Rows[cont]["Costo"] = (txtPrecioPlenoIp.Text);
                            dt.Rows[cont]["TRM"] = "1.00";

                            cont++;

                        }

                    }
                    grdPrecioIp.SetPageIndex(page);
                    if (!txtTrm.Text.Equals(""))
                    {
                        trm = decimal.Parse(txtTrm.Text);

                    }
                    Tabla_fill = cmIp.PoblarClientetipoExt(int.Parse(cboPlantaIp.SelectedItem.Value));
                    int contador = 0;
                    foreach (DataRow r in Tabla_fill.Rows)
                    {

                        DataRow row = dt.NewRow();
                        if (trm != 0)
                        {
                            Costo = (txtPrecioPlenoIp.Text);
                            row["item_planta_precio_id"] = "";
                            row["moneda_id"] = r["ext"].ToString();
                            row["moneda"] = r["dsc2"].ToString();
                            string porcentaje = ((TextBox)grdPrecioIp.Rows[contador].FindControl("txtporcentaje")).Text;
                            row["margen"] = porcentaje;
                            row["cliente_tipo"] = r["descripcion"].ToString();
                            row["cliente_tipo_planta_id"] = r["cliente_tipo_planta_id"].ToString();
                            row["TRM"] = trm.ToString();
                            row["Costo"] = Costo;
                            string valor = ((TextBox)grdPrecioIp.Rows[contador].FindControl("txt_valor")).Text;

                            decimal x = decimal.Parse(valor);
                            x = x / trm;
                            string x1 = x.ToString("N2", new CultureInfo("en-US"));
                            row["valor"] = x1;
                            dt.Rows.Add(row);
                            contador++;
                        }

                    }
                    dt.AcceptChanges();
                    Session.Add("TbPrecio", dt);
                    Reload_tbPrecio();
                    cmIp.CerrarConexion();
                }
                else
                {
                    cboPlantaIp.Focus();
                    mensajeVentana("Debe seleccionar una Planta para el Item");
                }
            }
        }

        private decimal CalcularPorcentaje(Decimal p1, Decimal p2)
        {
            decimal resultado;
            resultado = Math.Round(p1 / (1 - p2), 2, MidpointRounding.ToEven);
            return resultado;
        }

        // METODO PARA CALCULAR DISTRIBUIDOR POR FORMULA DIFERENTE
        private decimal CalcularPorcentajeDistribuidor(Decimal p1, Decimal p2, Decimal pPleno)
        {
            decimal resultado;
            resultado = Math.Round((p1 / (1 - pPleno))* (1 - p2), 2, MidpointRounding.ToEven);
            return resultado;
        }

        /**************METODO DE CAMBIAR DE PAGINA UTILIZADO PARA EL GRIDVIEW DE PRECIO *********************/
        protected void grdPrecioIp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPrecioIp.PageIndex = e.NewPageIndex;
            Reload_tbPrecio();

        }

        /**************METODO DE RECARGAR DATASOURCE DEL GRIDVIEW DE PRECIO *********************/
        private void Reload_tbPrecio()
        {
            grdPrecioIp.DataSource = Session["TbPrecio"] as DataTable;
            grdPrecioIp.DataBind();
        }
        /**************EVENTO DEL TEXTBOX DESCRIPCION PARA GENERAR DESCRIPCION ABREVIADA DEL ITEM*********************/
        protected void txtDscIp_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cboPlantaIp.SelectedValue) && cboPlantaIp.SelectedValue != "0")
            {
                string cadena = "";
                cadena = txtDscIp.Text;
                if (cadena.Length <= 20)
                {
                    txtDscAbrvIp.Text = cadena;
                }
                else { txtDscAbrvIp.Text = cadena.Substring(0, 20); }

                ((TextBox)grdIdiomaIp.Rows[0].FindControl("textDesc")).Text = txtDscIp.Text;

                if (!lblDesciplanta.Value.Equals(""))
                {
                    Session["item_planta_id"] = lblDesciplanta.Value;
                    lblDesciplanta.Value = "";
                    Debug.WriteLine("Item planta=" + Session["item_planta_id"].ToString());
                    cboPerfilIp_SelectedIndexChanged(cboPerfilIp, e);
                    string usu_select = " ", estado_select = " ";
                    bool activo = false;
                    reader = cmIp.ConsultarUsuEditar(Convert.ToInt64(Session["item_planta_id"].ToString()));
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                        {
                            usu_select = reader.GetString(0);
                            estado_select = reader.GetInt32(1).ToString();
                            activo = reader.GetBoolean(2);
                        }
                    }
                    reader.Close();
                    reader.Dispose();
                    cmIp.CerrarConexion();

                    if (cboPerfilIp.SelectedItem.Value.Equals("3"))
                    {
                        if (!Session["usuario"].ToString().Equals(usu_select) && estado_select.Equals("2"))
                        {
                            CargarDatos((string)Session["item_planta_id"], e);
                            CargarIdiomaEditado(Session["item_planta_id"].ToString());
                            CargarPrecioEditado(Session["item_planta_id"].ToString());
                            trlblobservacion.Visible = true;
                            trtxtobservacion.Visible = true;
                            btnEnviarIp.Visible = true;
                            btnLimpiarIp.Visible = false;
                            btnGuardarIp.Text = "Editar";
                            btnGuardarIp.Visible = true;
                        }
                        if (!Session["usuario"].ToString().Equals(usu_select) && (estado_select.Equals("9")))
                        {
                            CargarDatos((string)Session["item_planta_id"], e);
                            CargarIdiomaEditado(Session["item_planta_id"].ToString());
                            CargarPrecioEditado(Session["item_planta_id"].ToString());
                            btnGuardarIp.Visible = false;
                            btnLimpiarIp.Visible = false;
                            trlblobservacion.Visible = true;
                            txtobservestado.Enabled = false;
                            trtxtobservacion.Visible = true;
                        }

                    }
                    if (cboPerfilIp.SelectedItem.Value.Equals("1"))
                    {
                        if (!Session["usuario"].ToString().Equals(usu_select) && estado_select.Equals("9"))
                        {
                            CargarDatos((string)Session["item_planta_id"], e);
                            CargarIdiomaEditado(Session["item_planta_id"].ToString());
                            CargarPrecioEditado(Session["item_planta_id"].ToString());
                            AccordionPrincipalIp.SelectedIndex = 0;
                            trlblobservacion.Visible = true;
                            trtxtobservacion.Visible = true;
                            btnAprobarIp.Visible = true;
                            btnDevolverIp.Visible = true;
                            btnRechazarIp.Visible = true;
                            btnGuardarIp.Text = "Editar";
                            //KP
                            bool disp_ingenieria = chkListaDisponiblesIa.Items[3].Selected;
                            if (disp_ingenieria == true)
                            {
                                Habilitar_Campos_Accesorio(true);
                            }
                            else
                            {
                                Habilitar_Campos_Accesorio(false);
                            }

                            if (cmIp.Consultar_Detalle_Accesorio(int.Parse(Session["item_planta_id"].ToString()), int.Parse(cboPlantaIp.SelectedValue.ToString())).Rows.Count != 0)
                            {

                                Consultar_Detalle_Accesorio();
                                Consultar_Accesorios();
                                if (LblAnulado.Text == "False")
                                {
                                    btn_Anular.Visible = Visible;
                                    btn_Anular.Text = "Anular";
                                    LblEstadoAnula.Text = "Activo";
                                }
                                else if (LblAnulado.Text == "True")
                                {
                                    Consultar_Accesorios();
                                    btn_Anular.Visible = Visible;
                                    btn_Anular.Text = "Habilitar";
                                    LblEstadoAnula.Text = "Inactivo";

                                }
                            }
                            else
                            {
                                Consultar_Accesorios();
                                btn_Anular.Visible = false;
                            }
                        }
                        if (Session["usuario"].ToString().Equals(usu_select) && (estado_select.Equals("1") || estado_select.Equals("3")))
                        {
                            CargarDatos((string)Session["item_planta_id"], e);
                            CargarIdiomaEditado(Session["item_planta_id"].ToString());
                            CargarPrecioEditado(Session["item_planta_id"].ToString());
                            AccordionPrincipalIp.SelectedIndex = 0;
                            if (estado_select.Equals("3"))
                            {
                                trlblobservacion.Visible = true;
                                txtobservestado.Enabled = false;
                                trtxtobservacion.Visible = true;
                            }
                            btnGuardarIp.Text = "Editar";
                            btnEnviarIp.Visible = true;
                            //KP
                            bool disp_ingenieria = chkListaDisponiblesIa.Items[3].Selected;
                            if (disp_ingenieria == true)
                            {
                                Habilitar_Campos_Accesorio(true);
                            }
                            else
                            {
                                Habilitar_Campos_Accesorio(false);
                            }

                            if (cmIp.Consultar_Detalle_Accesorio(int.Parse(Session["item_planta_id"].ToString()), int.Parse(cboPlantaIp.SelectedValue.ToString())).Rows.Count != 0)
                            {

                                Consultar_Detalle_Accesorio();
                                Consultar_Accesorios();
                                if (LblAnulado.Text == "False")
                                {
                                    btn_Anular.Visible = Visible;
                                    btn_Anular.Text = "Anular";
                                    LblEstadoAnula.Text = "Activo";
                                }
                                else if (LblAnulado.Text == "True")
                                {
                                    Consultar_Accesorios();
                                    btn_Anular.Visible = Visible;
                                    btn_Anular.Text = "Habilitar";
                                    LblEstadoAnula.Text = "Inactivo";
                                }
                            }
                            else
                            {
                                Consultar_Accesorios();
                                btn_Anular.Visible = false;
                            }
                        }
                        if (!Session["usuario"].ToString().Equals(usu_select) && (estado_select.Equals("3") || estado_select.Equals("4") || estado_select.Equals("5")))
                        {
                            CargarDatos((string)Session["item_planta_id"], e);
                            CargarIdiomaEditado(Session["item_planta_id"].ToString());
                            CargarPrecioEditado(Session["item_planta_id"].ToString());
                            AccordionPrincipalIp.SelectedIndex = 0;
                            btnGuardarIp.Text = "Editar";
                            btnGuardarIp.Visible = true;
                            if (estado_select.Equals("5"))
                            {
                                btnduplicar.Visible = true;
                            }
                            if (estado_select.Equals("4"))
                            {
                                btnGuardarIp.Visible = false;
                            }
                            trlblobservacion.Visible = true;
                            txtobservestado.Enabled = false;
                            trtxtobservacion.Visible = true;
                            //KP
                            bool disp_ingenieria = chkListaDisponiblesIa.Items[3].Selected;
                            if (disp_ingenieria == true)
                            {
                                Habilitar_Campos_Accesorio(true);
                            }
                            else
                            {
                                Habilitar_Campos_Accesorio(false);
                            }

                            if (cmIp.Consultar_Detalle_Accesorio(int.Parse(Session["item_planta_id"].ToString()), int.Parse(cboPlantaIp.SelectedValue.ToString())).Rows.Count != 0)
                            {
                                Consultar_Detalle_Accesorio();
                                Consultar_Accesorios();
                                if (LblAnulado.Text == "False")
                                {
                                    btn_Anular.Visible = Visible;
                                    btn_Anular.Text = "Anular";
                                    LblEstadoAnula.Text = "Activo";
                                }
                                else if (LblAnulado.Text == "True")
                                {
                                    Consultar_Accesorios();
                                    btn_Anular.Visible = Visible;
                                    btn_Anular.Text = "Habilitar";
                                    LblEstadoAnula.Text = "Inactivo";
                                }
                            }
                            else
                            {
                                Consultar_Accesorios();
                                btn_Anular.Visible = false;
                            }
                        }
                        if (Session["usuario"].ToString().Equals(usu_select) && (estado_select.Equals("2") || estado_select.Equals("4") || estado_select.Equals("5") || estado_select.Equals("9")))
                        {
                            CargarDatos((string)Session["item_planta_id"], e);
                            CargarIdiomaEditado(Session["item_planta_id"].ToString());
                            CargarPrecioEditado(Session["item_planta_id"].ToString());
                            AccordionPrincipalIp.SelectedIndex = 0;
                            btnGuardarIp.Visible = false;

                            if (estado_select.Equals("5"))
                            {
                                btnGuardarIp.Text = "Editar";
                                btnGuardarIp.Visible = true;
                                btnduplicar.Visible = true;
                            }
                            trlblobservacion.Visible = true;
                            txtobservestado.Enabled = false;
                            trtxtobservacion.Visible = true;
                            //KP
                            bool disp_ingenieria = chkListaDisponiblesIa.Items[3].Selected;
                            if (disp_ingenieria == true)
                            {
                                Habilitar_Campos_Accesorio(true);
                            }
                            else
                            {
                                Habilitar_Campos_Accesorio(false);
                            }

                            if (cmIp.Consultar_Detalle_Accesorio(int.Parse(Session["item_planta_id"].ToString()), int.Parse(cboPlantaIp.SelectedValue.ToString())).Rows.Count != 0)
                            {
                                Consultar_Detalle_Accesorio();
                                Consultar_Accesorios();
                                if (LblAnulado.Text == "False")
                                {
                                    btn_Anular.Visible = Visible;
                                    btn_Anular.Text = "Anular";
                                    LblEstadoAnula.Text = "Activo";
                                }
                                else if (LblAnulado.Text == "True")
                                {
                                    Consultar_Accesorios();
                                    btn_Anular.Visible = Visible;
                                    btn_Anular.Text = "Habilitar";
                                    LblEstadoAnula.Text = "Inactivo";
                                }
                            }
                            else
                            {
                                Consultar_Accesorios();
                                btn_Anular.Visible = false;
                            }

                        }
                    }
                    if (cboPerfilIp.SelectedItem.Value.Equals("2") || cboPerfilIp.SelectedItem.Value.Equals("0"))
                    {

                        if (Session["usuario"].ToString().Equals(usu_select) && (estado_select.Equals("1") || estado_select.Equals("3")))
                        {
                            CargarDatos((string)Session["item_planta_id"], e);
                            CargarIdiomaEditado(Session["item_planta_id"].ToString());
                            CargarPrecioEditado(Session["item_planta_id"].ToString());
                            AccordionPrincipalIp.SelectedIndex = 0;
                            if (estado_select.Equals("3"))
                            {
                                trlblobservacion.Visible = true;
                                txtobservestado.Enabled = false;
                                trtxtobservacion.Visible = true;
                            }
                            btnGuardarIp.Text = "Editar";
                            btnEnviarIp.Visible = true;
                            //KP
                            bool disp_ingenieria = chkListaDisponiblesIa.Items[3].Selected;
                            if (disp_ingenieria == true)
                            {
                                Habilitar_Campos_Accesorio(true);
                            }
                            else
                            {
                                Habilitar_Campos_Accesorio(false);
                            }
                            if (cmIp.Consultar_Detalle_Accesorio(int.Parse(Session["item_planta_id"].ToString()), int.Parse(cboPlantaIp.SelectedValue.ToString())).Rows.Count != 0)
                            {
                                Consultar_Detalle_Accesorio();
                                Consultar_Accesorios();
                                if (LblAnulado.Text == "False")
                                {
                                    btn_Anular.Visible = Visible;
                                    btn_Anular.Text = "Anular";
                                    LblEstadoAnula.Text = "Activo";
                                }
                                else if (LblAnulado.Text == "True")
                                {
                                    Consultar_Accesorios();
                                    btn_Anular.Visible = Visible;
                                    btn_Anular.Text = "Habilitar";
                                    LblEstadoAnula.Text = "Inactivo";
                                }
                            }
                            else
                            {
                                Consultar_Accesorios();
                                btn_Anular.Visible = false;
                            }
                        }
                        if (Session["usuario"].ToString().Equals(usu_select) && (estado_select.Equals("2") || estado_select.Equals("4") || estado_select.Equals("5") || estado_select.Equals("9")))
                        {
                            CargarDatos((string)Session["item_planta_id"], e);
                            CargarIdiomaEditado(Session["item_planta_id"].ToString());
                            CargarPrecioEditado(Session["item_planta_id"].ToString());
                            AccordionPrincipalIp.SelectedIndex = 0;
                            btnGuardarIp.Visible = false;
                            trlblobservacion.Visible = true;
                            txtobservestado.Enabled = false;
                            trtxtobservacion.Visible = true;
                            //KP
                            bool disp_ingenieria = chkListaDisponiblesIa.Items[3].Selected;
                            if (disp_ingenieria == true)
                            {
                                Habilitar_Campos_Accesorio(true);
                            }
                            else
                            {
                                Habilitar_Campos_Accesorio(false);
                            }

                            if (cmIp.Consultar_Detalle_Accesorio(int.Parse(Session["item_planta_id"].ToString()), int.Parse(cboPlantaIp.SelectedValue.ToString())).Rows.Count != 0)
                            {
                                Consultar_Detalle_Accesorio();
                                Consultar_Accesorios();
                                if (LblAnulado.Text == "False")
                                {
                                    btn_Anular.Visible = Visible;
                                    btn_Anular.Text = "Anular";
                                    LblEstadoAnula.Text = "Activo";
                                }
                                else if (LblAnulado.Text == "True")
                                {
                                    Consultar_Accesorios();
                                    btn_Anular.Visible = Visible;
                                    btn_Anular.Text = "Habilitar";
                                    LblEstadoAnula.Text = "Inactivo";
                                }
                            }
                            else
                            {
                                Consultar_Accesorios();
                                btn_Anular.Visible = false;
                            }
                        }
                        if (!Session["usuario"].ToString().Equals(usu_select))
                        {

                            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mensaje", "MensajeError( 'No posee permisos para editar este Item Planta!')", true);
                        }

                    }

                }
                Debug.WriteLine("Item planta=" + Session["item_planta_id"].ToString());

            }

            else
            {
                txtDscIp.Text = "";
                txtDscAbrvIp.Text = "";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mensaje", "MensajeError( 'Debe sleccionar una planta previamente. Gracias!')", true);

            }

        }
        public override void Validate(string validationGroup)
        {

            if (validationGroup.Equals("grupIp"))
            {
                cvchkListorigen.Enabled = true;
                if (cboPerfilIp.SelectedItem.Value.Equals("1"))
                {
                    rqfcboTipInvIp.Enabled = true;
                    rqfcboGrupimpIp.Enabled = true;
                    rqfGrupoIp.Enabled = true;
                    rqfAgrupadorIp.Enabled = true;
                    rqftxtGruPlantaIp.Enabled = true;
                    cvchkListusoIp.Enabled = true;

                }
                if (cboPerfilIp.SelectedItem.Value.Equals("2") || cboPerfilIp.SelectedItem.Value.Equals("0"))
                {
                    rqfcboTipInvIp.Enabled = true;
                    rqfcboGrupimpIp.Enabled = false;
                    rqfGrupoIp.Enabled = false;
                    rqfAgrupadorIp.Enabled = false;
                    rqftxtGruPlantaIp.Enabled = false;
                    cvchkListusoIp.Enabled = false;
                }
                if (cboPerfilIp.SelectedItem.Value.Equals("3"))
                {
                    rqfcboTipInvIp.Enabled = true;
                    rqfcboGrupimpIp.Enabled = true;
                    rqfGrupoIp.Enabled = false;
                    rqfAgrupadorIp.Enabled = false;
                    rqftxtGruPlantaIp.Enabled = false;
                    cvchkListusoIp.Enabled = false;
                }
            }

            base.Validate(validationGroup);
        }
        /**************EVENTO GENERADO POR EL BOTON GUARDAR***************/
        protected void btnGuardarIp_Click(object sender, EventArgs e)
        {
            btnGuardarIp.Enabled = false;
            Page.Validate("grupIp");
            System.Threading.Thread.Sleep(1000);
            string mensaje = "";
            //if (Page.IsValid)
            //{
            //    Ejecutar(e);
            //}
            //else
            //{
            mensaje = validarCamposObligatorios();
            if (String.IsNullOrEmpty(mensaje.Trim()))
            {
                Ejecutar(e);
                btnGuardarIp.Enabled = true;
            }
            else
            {
                mensajeVentana(mensaje);
                btnGuardarIp.Enabled = true;
            }
            //}            
            //if (Page.IsValid)
            //{
            //    if (validaIdiomas())
            //    {
            //        if (cboPerfilIp.SelectedItem.Value.Equals("1"))
            //        {
            //            if (cboAdicionalIp.SelectedItem.Value == " " && cboOrdenIp.SelectedItem.Value == " ")
            //            {
            //                mensajeVentana("Debe ingresar un factor para unidad de medida");
            //            }
            //            else
            //            {
            //                Ejecutar(e);
            //            }
            //        }
            //        else
            //        {
            //            Ejecutar(e);
            //        }
            //    }
            //    else
            //    {
            //        AccordionPrincipalIp.SelectedIndex = 0;
            //        mensajeVentana("Debe diligenciar todos los Idiomas");
            //    }
            //}
            //else
            //{
            //    mensajeVentana("Por favor diligenciar los campos obligatorios");
            //}
        }

        private string vacio(string p)
        {
            if (p.Equals(""))
            {
                p = "0";
            }
            return p;
        }
     
        public void Ejecutar(EventArgs e)
        {
            bool aprobar = false;
            bool resultado = false, resultado1 = false;
            bool req_inspCalidad, req_inspeObliga;
            //unidades 
            txtPesoEmpaqueIa.Text = vacio(txtPesoEmpaqueIa.Text);
            txtPeso_unitario.Text = vacio(txtPeso_unitario.Text);
            txtCantidadEmpaqueIa.Text = vacio(txtCantidadEmpaqueIa.Text);
            txtLargo.Text = vacio(txtLargo.Text);
            txtAncho1.Text = vacio(txtAncho1.Text);
            txtAncho2.Text = vacio(txtAncho2.Text);
            txtAlto1.Text = vacio(txtAlto1.Text);
            txtAlto2.Text = vacio(txtAlto2.Text);
            bool tipo_kamban = chkkamban.Checked;
            bool disp_cotizacion = chkListaDisponiblesIa.Items[0].Selected;
            bool disp_comercial = chkListaDisponiblesIa.Items[1].Selected;
            bool disp_ingenieria = chkListaDisponiblesIa.Items[3].Selected;
            bool disp_almacen = chkListaDisponiblesIa.Items[4].Selected;
            bool disp_produccion = chkListaDisponiblesIa.Items[2].Selected;
            bool req_plano = chkListaRequiereIa.Items[0].Selected;
            bool req_tipo = chkListaRequiereIa.Items[2].Selected;
            bool req_modelo = chkListaRequiereIa.Items[1].Selected;
            if (chk_InspCalidad.Checked == true)
            {
                 req_inspCalidad = true;
            }
            else
            {
                req_inspCalidad = false;
            }

            if (chk_InspObligatoria.Checked == true)
            {
                req_inspeObliga = true;
            }
            else
            {
                req_inspeObliga = false;
            }

            int tipo_orden_prod_id = int.Parse(cboTipoOrdenIa.SelectedValue);
           
                if (disp_ingenieria == true)
            {
                DataTable dt = null;
                DataTable dtitem;
                int respuesta;
                bool valida1 = false, valida2 = false, valida3 = false, valida4 = false, valida5 = false, valida6 = false, valida7 = false, PermitirTrasnsaccion = false;
            
                if (!String.IsNullOrEmpty(txt_NombAcce.Text.Trim()))
                {
                    #region Asignacion de valores 
                    if (txt_Desc_Abrev.Text != "")
                    {
                    }
                    else
                    {
                        txt_Desc_Abrev.Text = "";
                    }
                    if (!String.IsNullOrEmpty(txt_Dim1min.Text.Trim()))
                    {
                    }
                    else
                    {
                        txt_Dim1min.Text = "0";
                    }
                    if (!String.IsNullOrEmpty(txt_Dim1max.Text.Trim()))
                    {
                    }
                    else
                    {
                        txt_Dim1max.Text = "0";
                    }
                    if (!String.IsNullOrEmpty(txt_Dim2min.Text.Trim()))
                    {
                    }
                    else
                    {
                        txt_Dim2min.Text = "0";
                    }
                    if (!String.IsNullOrEmpty(txt_Dim2max.Text.Trim()))
                    {
                    }
                    else
                    {
                        txt_Dim2max.Text = "0";
                    }
                    if (!String.IsNullOrEmpty(txt_Dim3min.Text.Trim()))
                    {
                    }
                    else
                    {
                        txt_Dim3min.Text = "0";
                    }
                    if (!String.IsNullOrEmpty(txt_Dim3max.Text.Trim()))
                    {
                    }
                    else
                    {
                        txt_Dim3max.Text = "0";
                    }
                    if (!String.IsNullOrEmpty(txt_Dim4min.Text.Trim()))
                    {
                    }
                    else
                    {
                        txt_Dim4min.Text = "0";
                    }
                    if (!String.IsNullOrEmpty(txt_Dim4max.Text.Trim()))
                    {
                    }
                    else
                    {
                        txt_Dim4max.Text = "0";
                    }
                    if (!String.IsNullOrEmpty(txt_Dim5min.Text.Trim()))
                    {
                    }
                    else
                    {
                        txt_Dim5min.Text = "0";
                    }
                    if (!String.IsNullOrEmpty(txt_Dim5max.Text.Trim()))
                    {
                    }
                    else
                    {
                        txt_Dim5max.Text = "0";
                    }
                    if (!String.IsNullOrEmpty(txt_Dim6min.Text.Trim()))
                    {
                    }
                    else
                    {
                        txt_Dim6min.Text = "0";
                    }
                    if (!String.IsNullOrEmpty(txt_Dim6max.Text.Trim()))
                    {
                    }
                    else
                    {
                        txt_Dim6max.Text = "0";
                    }
                    if (!String.IsNullOrEmpty(txt_Dim7min.Text.Trim()))
                    {
                    }
                    else
                    {
                        txt_Dim7min.Text = "0";
                    }
                    if (!String.IsNullOrEmpty(txt_Dim7max.Text.Trim()))
                    {
                    }
                    else
                    {
                        txt_Dim7max.Text = "0";
                    }
                    #endregion                  

                    #region Valida que el valor minimo sea menor a el valor maximo

                    if (float.Parse(txt_Dim1min.Text) <= float.Parse(txt_Dim1max.Text))
                    {
                        if (float.Parse(txt_Dim2min.Text) <= float.Parse(txt_Dim2max.Text))
                        {
                            if (float.Parse(txt_Dim3min.Text) <= float.Parse(txt_Dim3max.Text))
                            {
                                if (float.Parse(txt_Dim4min.Text) <= float.Parse(txt_Dim4max.Text))
                                {
                                    if (float.Parse(txt_Dim5min.Text) <= float.Parse(txt_Dim5max.Text))
                                    {
                                        if (float.Parse(txt_Dim6min.Text) <= float.Parse(txt_Dim6max.Text))
                                        {
                                            if (float.Parse(txt_Dim7min.Text) <= float.Parse(txt_Dim7max.Text))
                                            {
                                                #region Metodo para guardar accesorio

                                                if (btnGuardarIp.Text == "Guardar")
                                                {
                                                    //Convierte el valor en mayuscula
                                                    txt_NombAcce.Text = txt_NombAcce.Text.ToUpper();
                                                    txt_Desc_Abrev.Text = txt_Desc_Abrev.Text.ToUpper();
                                                    txt_NombAcce.Text = txt_NombAcce.Text.Replace(" ", "");
                                                    txt_Desc_Abrev.Text = txt_Desc_Abrev.Text.Replace(" ", "");

                                                    if (!String.IsNullOrEmpty(txt_Desc_Abrev.Text.Trim()))
                                                    {
                                                    }
                                                    else
                                                    {
                                                        txt_Desc_Abrev.Text = " ";
                                                    }
                                                    if (cmIp.Consultar_AccesoriosValidaReg(txt_NombAcce.Text, txt_Desc_Abrev.Text, int.Parse(cboPlantaIp.SelectedValue.ToString())).Rows.Count != 0)
                                                    {
                                                        dt = cmIp.Consultar_AccesoriosValidaReg(txt_NombAcce.Text, txt_Desc_Abrev.Text, int.Parse(cboPlantaIp.SelectedValue.ToString()));

                                                        for (int i = 0; i < dt.Rows.Count; i++)
                                                        {

                                                            if (aprobar == false)
                                                            {

                                                                if (float.Parse(dt.Rows[i][3].ToString()) == 0 && float.Parse(dt.Rows[i][4].ToString()) == 0 && txt_Dim1min.Text != txt_Dim1max.Text)
                                                                {
                                                                    valida1 = true;
                                                                }
                                                                else if ((float.Parse(txt_Dim1min.Text) == 0 && float.Parse(txt_Dim1max.Text) == 0 && float.Parse(dt.Rows[i][3].ToString()) != 0 && float.Parse(dt.Rows[i][4].ToString()) != 0) ||
                                                                        (float.Parse(txt_Dim1min.Text) == 0 && float.Parse(txt_Dim1max.Text) == 0 && float.Parse(dt.Rows[i][3].ToString()) == 0 && float.Parse(dt.Rows[i][4].ToString()) != 0))
                                                                {
                                                                    valida1 = true;
                                                                }
                                                                else if (validar_Campos(float.Parse(dt.Rows[i][3].ToString()), float.Parse(dt.Rows[i][4].ToString()), float.Parse(txt_Dim1min.Text), float.Parse(txt_Dim1max.Text)) ||
                                                                         validar_Campos(float.Parse(txt_Dim1min.Text), float.Parse(txt_Dim1max.Text), float.Parse(dt.Rows[i][3].ToString()), float.Parse(dt.Rows[i][4].ToString()))
                                                                        )
                                                                {
                                                                    valida1 = true;
                                                                }
                                                                else
                                                                {
                                                                    valida1 = false;
                                                                }

                                                                if (float.Parse(dt.Rows[i][5].ToString()) == 0 && float.Parse(dt.Rows[i][6].ToString()) == 0 && txt_Dim2min.Text != txt_Dim2max.Text)
                                                                {
                                                                    valida2 = true;
                                                                }
                                                                else if ((float.Parse(txt_Dim2min.Text) == 0 && float.Parse(txt_Dim2max.Text) == 0 && float.Parse(dt.Rows[i][5].ToString()) != 0 && float.Parse(dt.Rows[i][6].ToString()) != 0) ||
                                                                        (float.Parse(txt_Dim2min.Text) == 0 && float.Parse(txt_Dim2max.Text) == 0 && float.Parse(dt.Rows[i][5].ToString()) == 0 && float.Parse(dt.Rows[i][6].ToString()) != 0))
                                                                {
                                                                    valida2 = true;
                                                                }
                                                                else if (validar_Campos(float.Parse(dt.Rows[i][5].ToString()), float.Parse(dt.Rows[i][6].ToString()), float.Parse(txt_Dim2min.Text), float.Parse(txt_Dim2max.Text)) ||
                                                                         validar_Campos(float.Parse(txt_Dim2min.Text), float.Parse(txt_Dim2max.Text), float.Parse(dt.Rows[i][5].ToString()), float.Parse(dt.Rows[i][6].ToString()))
                                                                        )
                                                                {
                                                                    valida2 = true;
                                                                }
                                                                else
                                                                {
                                                                    valida2 = false;
                                                                }

                                                                if (float.Parse(dt.Rows[i][7].ToString()) == 0 && float.Parse(dt.Rows[i][8].ToString()) == 0 && txt_Dim3min.Text != txt_Dim3max.Text)
                                                                {
                                                                    valida3 = true;
                                                                }
                                                                else if ((float.Parse(txt_Dim3min.Text) == 0 && float.Parse(txt_Dim3max.Text) == 0 && float.Parse(dt.Rows[i][7].ToString()) != 0 && float.Parse(dt.Rows[i][8].ToString()) != 0) ||
                                                                        (float.Parse(txt_Dim3min.Text) == 0 && float.Parse(txt_Dim3max.Text) == 0 && float.Parse(dt.Rows[i][7].ToString()) == 0 && float.Parse(dt.Rows[i][8].ToString()) != 0))
                                                                {
                                                                    valida3 = true;
                                                                }
                                                                else if (validar_Campos(float.Parse(dt.Rows[i][7].ToString()), float.Parse(dt.Rows[i][8].ToString()), float.Parse(txt_Dim3min.Text), float.Parse(txt_Dim3max.Text)) ||
                                                                         validar_Campos(float.Parse(txt_Dim3min.Text), float.Parse(txt_Dim3max.Text), float.Parse(dt.Rows[i][7].ToString()), float.Parse(dt.Rows[i][8].ToString()))
                                                                        )
                                                                {
                                                                    valida3 = true;
                                                                }
                                                                else
                                                                {
                                                                    valida3 = false;
                                                                }

                                                                if (float.Parse(dt.Rows[i][9].ToString()) == 0 && float.Parse(dt.Rows[i][10].ToString()) == 0 && txt_Dim4min.Text != txt_Dim4max.Text)
                                                                {
                                                                    valida4 = true;
                                                                }
                                                                else if ((float.Parse(txt_Dim4min.Text) == 0 && float.Parse(txt_Dim4max.Text) == 0 && float.Parse(dt.Rows[i][9].ToString()) != 0 && float.Parse(dt.Rows[i][10].ToString()) != 0) ||
                                                                        (float.Parse(txt_Dim4min.Text) == 0 && float.Parse(txt_Dim4max.Text) == 0 && float.Parse(dt.Rows[i][9].ToString()) == 0 && float.Parse(dt.Rows[i][10].ToString()) != 0))
                                                                {
                                                                    valida4 = true;
                                                                }
                                                                else if (validar_Campos(float.Parse(dt.Rows[i][9].ToString()), float.Parse(dt.Rows[i][10].ToString()), float.Parse(txt_Dim4min.Text), float.Parse(txt_Dim4max.Text)) ||
                                                                         validar_Campos(float.Parse(txt_Dim4min.Text), float.Parse(txt_Dim4max.Text), float.Parse(dt.Rows[i][9].ToString()), float.Parse(dt.Rows[i][10].ToString()))
                                                                        )
                                                                {
                                                                    valida4 = true;
                                                                }
                                                                else
                                                                {
                                                                    valida4 = false;
                                                                }

                                                                if (float.Parse(dt.Rows[i][11].ToString()) == 0 && float.Parse(dt.Rows[i][12].ToString()) == 0 && txt_Dim5min.Text != txt_Dim5max.Text)
                                                                {
                                                                    valida5 = true;
                                                                }
                                                                else if ((float.Parse(txt_Dim5min.Text) == 0 && float.Parse(txt_Dim5max.Text) == 0 && float.Parse(dt.Rows[i][11].ToString()) != 0 && float.Parse(dt.Rows[i][12].ToString()) != 0) ||
                                                                        (float.Parse(txt_Dim5min.Text) == 0 && float.Parse(txt_Dim5max.Text) == 0 && float.Parse(dt.Rows[i][11].ToString()) == 0 && float.Parse(dt.Rows[i][12].ToString()) != 0))
                                                                {
                                                                    valida5 = true;
                                                                }
                                                                else if (validar_Campos(float.Parse(dt.Rows[i][11].ToString()), float.Parse(dt.Rows[i][12].ToString()), float.Parse(txt_Dim5min.Text), float.Parse(txt_Dim5max.Text)) ||
                                                                         validar_Campos(float.Parse(txt_Dim5min.Text), float.Parse(txt_Dim5max.Text), float.Parse(dt.Rows[i][11].ToString()), float.Parse(dt.Rows[i][12].ToString()))
                                                                        )
                                                                {
                                                                    valida5 = true;
                                                                }
                                                                else
                                                                {
                                                                    valida5 = false;
                                                                }

                                                                if (float.Parse(dt.Rows[i][13].ToString()) == 0 && float.Parse(dt.Rows[i][14].ToString()) == 0 && txt_Dim6min.Text != txt_Dim6max.Text)
                                                                {
                                                                    valida6 = true;
                                                                }
                                                                else if ((float.Parse(txt_Dim6min.Text) == 0 && float.Parse(txt_Dim6max.Text) == 0 && float.Parse(dt.Rows[i][13].ToString()) != 0 && float.Parse(dt.Rows[i][14].ToString()) != 0) ||
                                                                        (float.Parse(txt_Dim6min.Text) == 0 && float.Parse(txt_Dim6max.Text) == 0 && float.Parse(dt.Rows[i][13].ToString()) == 0 && float.Parse(dt.Rows[i][14].ToString()) != 0))
                                                                {
                                                                    valida6 = true;
                                                                }
                                                                else if (validar_Campos(float.Parse(dt.Rows[i][13].ToString()), float.Parse(dt.Rows[i][14].ToString()), float.Parse(txt_Dim6min.Text), float.Parse(txt_Dim6max.Text)) ||
                                                                         validar_Campos(float.Parse(txt_Dim6min.Text), float.Parse(txt_Dim6max.Text), float.Parse(dt.Rows[i][13].ToString()), float.Parse(dt.Rows[i][14].ToString()))
                                                                        )
                                                                {
                                                                    valida6 = true;
                                                                }
                                                                else
                                                                {
                                                                    valida6 = false;
                                                                }

                                                                if (float.Parse(dt.Rows[i][15].ToString()) == 0 && float.Parse(dt.Rows[i][16].ToString()) == 0 && txt_Dim7min.Text != txt_Dim7max.Text)
                                                                {
                                                                    valida7 = true;
                                                                }
                                                                else if ((float.Parse(txt_Dim7min.Text) == 0 && float.Parse(txt_Dim7max.Text) == 0 && float.Parse(dt.Rows[i][15].ToString()) != 0 && float.Parse(dt.Rows[i][16].ToString()) != 0) ||
                                                                        (float.Parse(txt_Dim7min.Text) == 0 && float.Parse(txt_Dim7max.Text) == 0 && float.Parse(dt.Rows[i][15].ToString()) == 0 && float.Parse(dt.Rows[i][16].ToString()) != 0))
                                                                {
                                                                    valida7 = true;
                                                                }
                                                                else if (validar_Campos(float.Parse(dt.Rows[i][15].ToString()), float.Parse(dt.Rows[i][16].ToString()), float.Parse(txt_Dim7min.Text), float.Parse(txt_Dim7max.Text)) ||
                                                                         validar_Campos(float.Parse(txt_Dim7min.Text), float.Parse(txt_Dim7max.Text), float.Parse(dt.Rows[i][15].ToString()), float.Parse(dt.Rows[i][16].ToString()))
                                                                        )
                                                                {
                                                                    valida7 = true;
                                                                }
                                                                else
                                                                {
                                                                    valida7 = false;
                                                                }
                                                                if ((valida1 && valida2 && valida3 && valida4 &&
                                                               valida5 && valida6 && valida7) == false)
                                                                {
                                                                    aprobar = false;
                                                                }
                                                                else if (valida1 == true && valida2 == true && valida3 == true && valida4 == true &&
                                                               valida5 == true && valida6 == true && valida7 == true)
                                                                {
                                                                    aprobar = true;
                                                                }
                                                                //Si aprobar es true, ya no valida mas y se sale del ciclo
                                                            }   
                                                        }
                                                        if (aprobar == false)
                                                        {
                                                            #region capturan los datos necesarios para insertar en la tabla item_planta
                                                            //Se capturan los datos necesarios para insertar en la tabla item_planta
                                                            if (txtFactorLIp.Text != "")
                                                            {
                                                                if (btnGuardarIp.Text == "Guardar")
                                                                {
                                                                    resultado = InsertarItemPlanta(Convert.ToInt32(chkListorigen.SelectedValue), Convert.ToDecimal(txtPeso_unitario.Text), Convert.ToDecimal(txtFactorLIp.Text), 0, 
                                                                                                   Convert.ToDecimal(txtPesoEmpaqueIa.Text), Convert.ToInt32(txtCantidadEmpaqueIa.Text), Convert.ToDecimal(txtLargo.Text), 
                                                                                                   Convert.ToDecimal(txtAncho1.Text), Convert.ToDecimal(txtAncho2.Text), Convert.ToDecimal(txtAlto1.Text),
                                                                                                   Convert.ToDecimal(txtAlto2.Text), tipo_kamban, disp_cotizacion, disp_comercial, disp_ingenieria, disp_almacen, 
                                                                                                   disp_produccion, req_plano, req_tipo, req_modelo, tipo_orden_prod_id, req_inspCalidad,
                                                                                                   req_inspeObliga, Convert.ToInt32(cboClaseItem.SelectedValue));
                                                                }
                                                            }
                                                            if (txtFactorM2Ip.Text != "")
                                                            {
                                                                if (btnGuardarIp.Text == "Guardar")
                                                                {
                                                                    resultado = InsertarItemPlanta(Convert.ToInt32(chkListorigen.SelectedValue), Convert.ToDecimal(txtPeso_unitario.Text), 0,
                                                                                                   Convert.ToDecimal(txtFactorM2Ip.Text), Convert.ToDecimal(txtPesoEmpaqueIa.Text), Convert.ToInt32(txtCantidadEmpaqueIa.Text),
                                                                                                   Convert.ToDecimal(txtLargo.Text), Convert.ToDecimal(txtAncho1.Text), Convert.ToDecimal(txtAncho2.Text),
                                                                                                   Convert.ToDecimal(txtAlto1.Text), Convert.ToDecimal(txtAlto2.Text), tipo_kamban, disp_cotizacion,
                                                                                                   disp_comercial, disp_ingenieria, disp_almacen, disp_produccion, req_plano, req_tipo, req_modelo,
                                                                                                   tipo_orden_prod_id, req_inspCalidad, req_inspeObliga,Convert.ToInt32(cboClaseItem.SelectedValue));
                                                                }
                                                            }
                                                            if (cboAdicionalIp.Enabled == false && txtfactorunitario.Visible)
                                                            {
                                                                if (btnGuardarIp.Text == "Guardar")
                                                                {
                                                                    resultado = InsertarItemPlanta(Convert.ToInt32(chkListorigen.SelectedValue), Convert.ToDecimal(txtPeso_unitario.Text), 1, 0,
                                                                                                   Convert.ToDecimal(txtPesoEmpaqueIa.Text), Convert.ToInt32(txtCantidadEmpaqueIa.Text), Convert.ToDecimal(txtLargo.Text),
                                                                                                   Convert.ToDecimal(txtAncho1.Text), Convert.ToDecimal(txtAncho2.Text), Convert.ToDecimal(txtAlto1.Text),
                                                                                                   Convert.ToDecimal(txtAlto2.Text), tipo_kamban, disp_cotizacion, disp_comercial, disp_ingenieria, disp_almacen,
                                                                                                   disp_produccion, req_plano, req_tipo, req_modelo, tipo_orden_prod_id, req_inspCalidad,
                                                                                                   req_inspeObliga, Convert.ToInt32(cboClaseItem.SelectedValue));

                                                                }
                                                            }
                                                            if (!txtfactorunitario.Visible && txtFactorLIp.Text.Equals("") && txtFactorM2Ip.Text.Equals(""))
                                                            {
                                                                if (btnGuardarIp.Text == "Guardar")
                                                                {
                                                                    resultado = InsertarItemPlanta(Convert.ToInt32(chkListorigen.SelectedValue), Convert.ToDecimal(txtPeso_unitario.Text), 0, 0, 
                                                                                                   Convert.ToDecimal(txtPesoEmpaqueIa.Text), Convert.ToInt32(txtCantidadEmpaqueIa.Text),
                                                                                                   Convert.ToDecimal(txtLargo.Text), Convert.ToDecimal(txtAncho1.Text), Convert.ToDecimal(txtAncho2.Text),
                                                                                                   Convert.ToDecimal(txtAlto1.Text), Convert.ToDecimal(txtAlto2.Text), tipo_kamban, disp_cotizacion,
                                                                                                   disp_comercial, disp_ingenieria, disp_almacen, disp_produccion, req_plano, req_tipo, req_modelo, 
                                                                                                   tipo_orden_prod_id, req_inspCalidad, req_inspeObliga, Convert.ToInt32(cboClaseItem.SelectedValue));

                                                                }
                                                            }
                                                            if (resultado)
                                                            {
                                                                mensajeVentana("Item se ha insertado correctamente!!");
                                                                cboPerfilIp_SelectedIndexChanged(cboPerfilIp, e);
                                                                CargarDatos(Session["item_planta_id"].ToString(), e);

                                                                CargarIdiomaEditado(Session["item_planta_id"].ToString());
                                                                CargarPrecioEditado(Session["item_planta_id"].ToString());
                                                                AccordionPrincipalIp.SelectedIndex = 0;
                                                                btnGuardarIp.Text = "Editar";
                                                                btnEnviarIp.Visible = true;
                                                                Habilitar_Campos_Accesorio(true);
                                                            }
                                                            #endregion
                                                        
                                                            dtitem = cmIp.Recuperar_IdItemPlanta(txtDscIp.Text, txtDscAbrvIp.Text, int.Parse(cboPlantaIp.SelectedValue.ToString()));
                                                            string idItemPlanta = dtitem.Rows[0][0].ToString();

                                                            int coderp = 0;
                                                            if (!String.IsNullOrEmpty(txtCodErpIp.Text))
                                                            {
                                                                coderp = Convert.ToInt32(txtCodErpIp.Text);
                                                            }

                                                            respuesta = cmIp.Crear_Accesorio(coderp, txt_NombAcce.Text, txt_Desc_Abrev.Text, float.Parse(txt_Dim1min.Text), float.Parse(txt_Dim1max.Text)
                                            , float.Parse(txt_Dim2min.Text), float.Parse(txt_Dim2max.Text), float.Parse(txt_Dim3min.Text), float.Parse(txt_Dim3max.Text), float.Parse(txt_Dim4min.Text), float.Parse(txt_Dim4max.Text),
                                              float.Parse(txt_Dim5min.Text), float.Parse(txt_Dim5max.Text), float.Parse(txt_Dim6min.Text), float.Parse(txt_Dim6max.Text), float.Parse(txt_Dim7min.Text), float.Parse(txt_Dim7max.Text),
                                              int.Parse(cboPlantaIp.SelectedValue.ToString()), 0, int.Parse(idItemPlanta));

                                                            if (respuesta == 1)
                                                            {
                                                                mensajeVentana("Asesorio creado correctamente");                                                       
                                                                Consultar_Accesorios();

                                                                Consultar_Detalle_Accesorio();
                                                                if (LblAnulado.Text == "False")
                                                                {
                                                                    btn_Anular.Visible = Visible;
                                                                    LblEstadoAnula.Text = "Activo";
                                                                }
                                                                else if (LblAnulado.Text == "True")
                                                                {
                                                                    btn_Anular.Visible = false;
                                                                    LblEstadoAnula.Text = "Inactivo";
                                                                }
                                                            }
                                                            else
                                                            {
                                                                mensajeVentana("Error al intentar crear el accesorio");
                                                            }
                                                        }

                                                        else
                                                        {
                                                            if (valida1 & valida2 & valida3 & valida4 & valida5 & valida6 & valida7 == true)
                                                            {
                                                                mensajeVentana("Valide que las dimensiones a crear, no se cruzen con las de un accesorio existente");
                                                            }

                                                            Consultar_Accesorios();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        #region capturan los datos necesarios para insertar en la tabla item_planta
                                                        //Se capturan los datos necesarios para insertar en la tabla item_planta
                                                        if (txtFactorLIp.Text != "")
                                                        {
                                                            if (btnGuardarIp.Text == "Guardar")
                                                            {
                                                                resultado = InsertarItemPlanta(Convert.ToInt32(chkListorigen.SelectedValue), Convert.ToDecimal(txtPeso_unitario.Text), Convert.ToDecimal(txtFactorLIp.Text), 0,
                                                                                               Convert.ToDecimal(txtPesoEmpaqueIa.Text), Convert.ToInt32(txtCantidadEmpaqueIa.Text), Convert.ToDecimal(txtLargo.Text), 
                                                                                               Convert.ToDecimal(txtAncho1.Text), Convert.ToDecimal(txtAncho2.Text), Convert.ToDecimal(txtAlto1.Text), 
                                                                                               Convert.ToDecimal(txtAlto2.Text), tipo_kamban, disp_cotizacion, disp_comercial, disp_ingenieria, disp_almacen,
                                                                                               disp_produccion, req_plano, req_tipo, req_modelo, tipo_orden_prod_id,req_inspCalidad,
                                                                                               req_inspeObliga, Convert.ToInt32(cboClaseItem.SelectedValue));
                                                            }                                                        
                                                        }
                                                        if (txtFactorM2Ip.Text != "")
                                                        {
                                                            if (btnGuardarIp.Text == "Guardar")
                                                            {
                                                                resultado = InsertarItemPlanta(Convert.ToInt32(chkListorigen.SelectedValue), Convert.ToDecimal(txtPeso_unitario.Text), 0,
                                                                                               Convert.ToDecimal(txtFactorM2Ip.Text), Convert.ToDecimal(txtPesoEmpaqueIa.Text), Convert.ToInt32(txtCantidadEmpaqueIa.Text),
                                                                                               Convert.ToDecimal(txtLargo.Text), Convert.ToDecimal(txtAncho1.Text), Convert.ToDecimal(txtAncho2.Text),
                                                                                               Convert.ToDecimal(txtAlto1.Text), Convert.ToDecimal(txtAlto2.Text), tipo_kamban, disp_cotizacion, disp_comercial,
                                                                                               disp_ingenieria, disp_almacen, disp_produccion, req_plano, req_tipo, req_modelo, tipo_orden_prod_id, req_inspCalidad,
                                                                                               req_inspeObliga, Convert.ToInt32(cboClaseItem.SelectedValue));
                                                            }                                                           
                                                        }
                                                        if (cboAdicionalIp.Enabled == false && txtfactorunitario.Visible)
                                                        {
                                                            if (btnGuardarIp.Text == "Guardar")
                                                            {
                                                                resultado = InsertarItemPlanta(Convert.ToInt32(chkListorigen.SelectedValue), Convert.ToDecimal(txtPeso_unitario.Text), 1, 0,
                                                                                               Convert.ToDecimal(txtPesoEmpaqueIa.Text), Convert.ToInt32(txtCantidadEmpaqueIa.Text), 
                                                                                               Convert.ToDecimal(txtLargo.Text), Convert.ToDecimal(txtAncho1.Text), Convert.ToDecimal(txtAncho2.Text),
                                                                                               Convert.ToDecimal(txtAlto1.Text), Convert.ToDecimal(txtAlto2.Text), tipo_kamban, disp_cotizacion,
                                                                                               disp_comercial, disp_ingenieria, disp_almacen, disp_produccion, req_plano, req_tipo, req_modelo,
                                                                                               tipo_orden_prod_id, req_inspCalidad, req_inspeObliga, Convert.ToInt32(cboClaseItem.SelectedValue));

                                                            }                                                           
                                                        }
                                                        if (!txtfactorunitario.Visible && txtFactorLIp.Text.Equals("") && txtFactorM2Ip.Text.Equals(""))
                                                        {
                                                            if (btnGuardarIp.Text == "Guardar")
                                                            {
                                                                resultado = InsertarItemPlanta(Convert.ToInt32(chkListorigen.SelectedValue), Convert.ToDecimal(txtPeso_unitario.Text), 0, 0,
                                                                                               Convert.ToDecimal(txtPesoEmpaqueIa.Text), Convert.ToInt32(txtCantidadEmpaqueIa.Text), Convert.ToDecimal(txtLargo.Text),
                                                                                               Convert.ToDecimal(txtAncho1.Text), Convert.ToDecimal(txtAncho2.Text), Convert.ToDecimal(txtAlto1.Text),
                                                                                               Convert.ToDecimal(txtAlto2.Text), tipo_kamban, disp_cotizacion, disp_comercial, disp_ingenieria, disp_almacen,
                                                                                               disp_produccion, req_plano, req_tipo, req_modelo, tipo_orden_prod_id, req_inspCalidad,
                                                                                               req_inspeObliga, Convert.ToInt32(cboClaseItem.SelectedValue));

                                                            }                                                           
                                                        }
                                                        if (resultado)
                                                        {
                                                            mensajeVentana("Item se ha insertado correctamente!!");
                                                            cboPerfilIp_SelectedIndexChanged(cboPerfilIp, e);
                                                            CargarDatos(Session["item_planta_id"].ToString(), e);
                                                            CargarIdiomaEditado(Session["item_planta_id"].ToString());
                                                            CargarPrecioEditado(Session["item_planta_id"].ToString());
                                                            AccordionPrincipalIp.SelectedIndex = 0;
                                                            btnGuardarIp.Text = "Editar";
                                                            btnEnviarIp.Visible = true;                                                           
                                                             Habilitar_Campos_Accesorio(true);
                                                            
                                                        }
                                                        #endregion

                                                        dtitem = cmIp.Recuperar_IdItemPlanta(txtDscIp.Text, txtDscAbrvIp.Text, int.Parse(cboPlantaIp.SelectedValue.ToString()));
                                                        string idItemPlanta = dtitem.Rows[0][0].ToString();
                                        
                                                        respuesta = cmIp.Crear_Accesorio(0, txt_NombAcce.Text, txt_Desc_Abrev.Text, float.Parse(txt_Dim1min.Text), float.Parse(txt_Dim1max.Text)
, float.Parse(txt_Dim2min.Text), float.Parse(txt_Dim2max.Text), float.Parse(txt_Dim3min.Text), float.Parse(txt_Dim3max.Text), float.Parse(txt_Dim4min.Text), float.Parse(txt_Dim4max.Text),
float.Parse(txt_Dim5min.Text), float.Parse(txt_Dim5max.Text), float.Parse(txt_Dim6min.Text), float.Parse(txt_Dim6max.Text), float.Parse(txt_Dim7min.Text), float.Parse(txt_Dim7max.Text),
int.Parse(cboPlantaIp.SelectedValue.ToString()), 0, int.Parse(idItemPlanta));

                                                        if (respuesta == 1)
                                                        {
                                                            mensajeVentana("Asesorio creado correctamente");
                                                            // Limpiar_Campos_Accesorio();
                                                            Consultar_Accesorios();

                                                            Consultar_Detalle_Accesorio();
                                                            if (LblAnulado.Text == "False")
                                                            {
                                                                btn_Anular.Visible = Visible;
                                                                LblEstadoAnula.Text = "Activo";
                                                            }
                                                            else if (LblAnulado.Text == "True")
                                                            {
                                                                btn_Anular.Visible = false;
                                                                LblEstadoAnula.Text = "Inactivo";
                                                            }

                                                        }
                                                        else
                                                        {
                                                            mensajeVentana("Error al intentar crear el accesorio");
                                                        }
                                                        #endregion
                                                    }
                                                }
                                                #region Metodo para actualizar el accesorio
                                                else if (btnGuardarIp.Text == "Editar")
                                                {
                                                    //Convierte el valor en mayuscula
                                                    txt_NombAcce.Text = txt_NombAcce.Text.ToUpper();
                                                    txt_Desc_Abrev.Text = txt_Desc_Abrev.Text.ToUpper();
                                                    txt_NombAcce.Text = txt_NombAcce.Text.Replace(" ", "");
                                                    txt_Desc_Abrev.Text = txt_Desc_Abrev.Text.Replace(" ", "");

                                                    if (!String.IsNullOrEmpty(txt_Desc_Abrev.Text.Trim()))
                                                    {
                                                    }
                                                    else
                                                    {
                                                        txt_Desc_Abrev.Text = " ";
                                                    }

                                                    #region Si no existe la configuracion, la crea
                                                    if (cmIp.Consultar_Detalle_Accesorio(int.Parse(Session["item_planta_id"].ToString()), int.Parse(cboPlantaIp.SelectedValue.ToString())).Rows.Count == 0)
                                                    {
                                                        dt = cmIp.Consultar_AccesoriosDtt(txt_NombAcce.Text, txt_Desc_Abrev.Text, int.Parse(cboPlantaIp.SelectedValue.ToString()), int.Parse((Session["item_planta_id"].ToString())));

                                                        for (int i = 0; i < dt.Rows.Count; i++)
                                                        {

                                                            if (aprobar == false)
                                                            {

                                                                if (float.Parse(dt.Rows[i][3].ToString()) == 0 && float.Parse(dt.Rows[i][4].ToString()) == 0 && txt_Dim1min.Text != txt_Dim1max.Text)
                                                                {
                                                                    valida1 = true;
                                                                }
                                                                else if ((float.Parse(txt_Dim1min.Text) == 0 && float.Parse(txt_Dim1max.Text) == 0 && float.Parse(dt.Rows[i][3].ToString()) != 0 && float.Parse(dt.Rows[i][4].ToString()) != 0) ||
                                                                        (float.Parse(txt_Dim1min.Text) == 0 && float.Parse(txt_Dim1max.Text) == 0 && float.Parse(dt.Rows[i][3].ToString()) == 0 && float.Parse(dt.Rows[i][4].ToString()) != 0))
                                                                {
                                                                    valida1 = true;
                                                                }
                                                                else if (validar_Campos(float.Parse(dt.Rows[i][3].ToString()), float.Parse(dt.Rows[i][4].ToString()), float.Parse(txt_Dim1min.Text), float.Parse(txt_Dim1max.Text)) ||
                                                                         validar_Campos(float.Parse(txt_Dim1min.Text), float.Parse(txt_Dim1max.Text), float.Parse(dt.Rows[i][3].ToString()), float.Parse(dt.Rows[i][4].ToString()))
                                                                        )
                                                                {
                                                                    valida1 = true;
                                                                }
                                                                else
                                                                {
                                                                    valida1 = false;
                                                                }

                                                                if (float.Parse(dt.Rows[i][5].ToString()) == 0 && float.Parse(dt.Rows[i][6].ToString()) == 0 && txt_Dim2min.Text != txt_Dim2max.Text)
                                                                {
                                                                    valida2 = true;
                                                                }
                                                                else if ((float.Parse(txt_Dim2min.Text) == 0 && float.Parse(txt_Dim2max.Text) == 0 && float.Parse(dt.Rows[i][5].ToString()) != 0 && float.Parse(dt.Rows[i][6].ToString()) != 0) ||
                                                                        (float.Parse(txt_Dim2min.Text) == 0 && float.Parse(txt_Dim2max.Text) == 0 && float.Parse(dt.Rows[i][5].ToString()) == 0 && float.Parse(dt.Rows[i][6].ToString()) != 0))
                                                                {
                                                                    valida2 = true;
                                                                }
                                                                else if (validar_Campos(float.Parse(dt.Rows[i][5].ToString()), float.Parse(dt.Rows[i][6].ToString()), float.Parse(txt_Dim2min.Text), float.Parse(txt_Dim2max.Text)) ||
                                                                         validar_Campos(float.Parse(txt_Dim2min.Text), float.Parse(txt_Dim2max.Text), float.Parse(dt.Rows[i][5].ToString()), float.Parse(dt.Rows[i][6].ToString()))
                                                                        )
                                                                {
                                                                    valida2 = true;
                                                                }
                                                                else
                                                                {
                                                                    valida2 = false;
                                                                }

                                                                if (float.Parse(dt.Rows[i][7].ToString()) == 0 && float.Parse(dt.Rows[i][8].ToString()) == 0 && txt_Dim3min.Text != txt_Dim3max.Text)
                                                                {
                                                                    valida3 = true;
                                                                }
                                                                else if ((float.Parse(txt_Dim3min.Text) == 0 && float.Parse(txt_Dim3max.Text) == 0 && float.Parse(dt.Rows[i][7].ToString()) != 0 && float.Parse(dt.Rows[i][8].ToString()) != 0) ||
                                                                        (float.Parse(txt_Dim3min.Text) == 0 && float.Parse(txt_Dim3max.Text) == 0 && float.Parse(dt.Rows[i][7].ToString()) == 0 && float.Parse(dt.Rows[i][8].ToString()) != 0))
                                                                {
                                                                    valida3 = true;
                                                                }
                                                                else if (validar_Campos(float.Parse(dt.Rows[i][7].ToString()), float.Parse(dt.Rows[i][8].ToString()), float.Parse(txt_Dim3min.Text), float.Parse(txt_Dim3max.Text)) ||
                                                                         validar_Campos(float.Parse(txt_Dim3min.Text), float.Parse(txt_Dim3max.Text), float.Parse(dt.Rows[i][7].ToString()), float.Parse(dt.Rows[i][8].ToString()))
                                                                        )
                                                                {
                                                                    valida3 = true;
                                                                }
                                                                else
                                                                {
                                                                    valida3 = false;
                                                                }

                                                                if (float.Parse(dt.Rows[i][9].ToString()) == 0 && float.Parse(dt.Rows[i][10].ToString()) == 0 && txt_Dim4min.Text != txt_Dim4max.Text)
                                                                {
                                                                    valida4 = true;
                                                                }
                                                                else if ((float.Parse(txt_Dim4min.Text) == 0 && float.Parse(txt_Dim4max.Text) == 0 && float.Parse(dt.Rows[i][9].ToString()) != 0 && float.Parse(dt.Rows[i][10].ToString()) != 0) ||
                                                                        (float.Parse(txt_Dim4min.Text) == 0 && float.Parse(txt_Dim4max.Text) == 0 && float.Parse(dt.Rows[i][9].ToString()) == 0 && float.Parse(dt.Rows[i][10].ToString()) != 0))
                                                                {
                                                                    valida4 = true;
                                                                }
                                                                else if (validar_Campos(float.Parse(dt.Rows[i][9].ToString()), float.Parse(dt.Rows[i][10].ToString()), float.Parse(txt_Dim4min.Text), float.Parse(txt_Dim4max.Text)) ||
                                                                         validar_Campos(float.Parse(txt_Dim4min.Text), float.Parse(txt_Dim4max.Text), float.Parse(dt.Rows[i][9].ToString()), float.Parse(dt.Rows[i][10].ToString()))
                                                                        )
                                                                {
                                                                    valida4 = true;
                                                                }
                                                                else
                                                                {
                                                                    valida4 = false;
                                                                }

                                                                if (float.Parse(dt.Rows[i][11].ToString()) == 0 && float.Parse(dt.Rows[i][12].ToString()) == 0 && txt_Dim5min.Text != txt_Dim5max.Text)
                                                                {
                                                                    valida5 = true;
                                                                }
                                                                else if ((float.Parse(txt_Dim5min.Text) == 0 && float.Parse(txt_Dim5max.Text) == 0 && float.Parse(dt.Rows[i][11].ToString()) != 0 && float.Parse(dt.Rows[i][12].ToString()) != 0) ||
                                                                        (float.Parse(txt_Dim5min.Text) == 0 && float.Parse(txt_Dim5max.Text) == 0 && float.Parse(dt.Rows[i][11].ToString()) == 0 && float.Parse(dt.Rows[i][12].ToString()) != 0))
                                                                {
                                                                    valida5 = true;
                                                                }
                                                                else if (validar_Campos(float.Parse(dt.Rows[i][11].ToString()), float.Parse(dt.Rows[i][12].ToString()), float.Parse(txt_Dim5min.Text), float.Parse(txt_Dim5max.Text)) ||
                                                                         validar_Campos(float.Parse(txt_Dim5min.Text), float.Parse(txt_Dim5max.Text), float.Parse(dt.Rows[i][11].ToString()), float.Parse(dt.Rows[i][12].ToString()))
                                                                        )
                                                                {
                                                                    valida5 = true;
                                                                }
                                                                else
                                                                {
                                                                    valida5 = false;
                                                                }

                                                                if (float.Parse(dt.Rows[i][13].ToString()) == 0 && float.Parse(dt.Rows[i][14].ToString()) == 0 && txt_Dim6min.Text != txt_Dim6max.Text)
                                                                {
                                                                    valida6 = true;
                                                                }
                                                                else if ((float.Parse(txt_Dim6min.Text) == 0 && float.Parse(txt_Dim6max.Text) == 0 && float.Parse(dt.Rows[i][13].ToString()) != 0 && float.Parse(dt.Rows[i][14].ToString()) != 0) ||
                                                                        (float.Parse(txt_Dim6min.Text) == 0 && float.Parse(txt_Dim6max.Text) == 0 && float.Parse(dt.Rows[i][13].ToString()) == 0 && float.Parse(dt.Rows[i][14].ToString()) != 0))
                                                                {
                                                                    valida6 = true;
                                                                }
                                                                else if (validar_Campos(float.Parse(dt.Rows[i][13].ToString()), float.Parse(dt.Rows[i][14].ToString()), float.Parse(txt_Dim6min.Text), float.Parse(txt_Dim6max.Text)) ||
                                                                         validar_Campos(float.Parse(txt_Dim6min.Text), float.Parse(txt_Dim6max.Text), float.Parse(dt.Rows[i][13].ToString()), float.Parse(dt.Rows[i][14].ToString()))
                                                                        )
                                                                {
                                                                    valida6 = true;
                                                                }
                                                                else
                                                                {
                                                                    valida6 = false;
                                                                }

                                                                if (float.Parse(dt.Rows[i][15].ToString()) == 0 && float.Parse(dt.Rows[i][16].ToString()) == 0 && txt_Dim7min.Text != txt_Dim7max.Text)
                                                                {
                                                                    valida7 = true;
                                                                }
                                                                else if ((float.Parse(txt_Dim7min.Text) == 0 && float.Parse(txt_Dim7max.Text) == 0 && float.Parse(dt.Rows[i][15].ToString()) != 0 && float.Parse(dt.Rows[i][16].ToString()) != 0) ||
                                                                        (float.Parse(txt_Dim7min.Text) == 0 && float.Parse(txt_Dim7max.Text) == 0 && float.Parse(dt.Rows[i][15].ToString()) == 0 && float.Parse(dt.Rows[i][16].ToString()) != 0))
                                                                {
                                                                    valida7 = true;
                                                                }
                                                                else if (validar_Campos(float.Parse(dt.Rows[i][15].ToString()), float.Parse(dt.Rows[i][16].ToString()), float.Parse(txt_Dim7min.Text), float.Parse(txt_Dim7max.Text)) ||
                                                                         validar_Campos(float.Parse(txt_Dim7min.Text), float.Parse(txt_Dim7max.Text), float.Parse(dt.Rows[i][15].ToString()), float.Parse(dt.Rows[i][16].ToString()))
                                                                        )
                                                                {
                                                                    valida7 = true;
                                                                }
                                                                else
                                                                {
                                                                    valida7 = false;
                                                                }
                                                                if ((valida1 && valida2 && valida3 && valida4 &&
                                                               valida5 && valida6 && valida7) == false)
                                                                {
                                                                    aprobar = false;
                                                                }
                                                                else if (valida1 == true && valida2 == true && valida3 == true && valida4 == true &&
                                                               valida5 == true && valida6 == true && valida7 == true)
                                                                {
                                                                    aprobar = true;
                                                                }
                                                                //Si aprobar es true, ya no valida mas y se sale del ciclo
                                                            }
                                                        }
                                                        if (aprobar == false)
                                                        {
                                                            #region capturan los datos necesarios para insertar en la tabla item_planta
                                                            if (txtFactorLIp.Text != "")
                                                            {
                                                                if (btnGuardarIp.Text == "Editar")
                                                                {
                                                                    if (!Session["item_planta_id"].ToString().Equals("0"))
                                                                    {
                                                                        resultado1 = ActualizarItemPlanta(Convert.ToInt64(Session["item_planta_id"].ToString()), Convert.ToInt32(chkListorigen.SelectedValue), Convert.ToDecimal(txtPeso_unitario.Text), Convert.ToDecimal(txtFactorLIp.Text), 0, 
                                                                                                          Convert.ToDecimal(txtPesoEmpaqueIa.Text), Convert.ToInt32(txtCantidadEmpaqueIa.Text), Convert.ToDecimal(txtLargo.Text), Convert.ToDecimal(txtAncho1.Text), Convert.ToDecimal(txtAncho2.Text),
                                                                                                          Convert.ToDecimal(txtAlto1.Text), Convert.ToDecimal(txtAlto2.Text), tipo_kamban, disp_cotizacion, disp_comercial, disp_ingenieria, disp_almacen, disp_produccion, req_plano, req_tipo, req_modelo,
                                                                                                          tipo_orden_prod_id, req_inspCalidad, req_inspeObliga, Convert.ToInt32(cboClaseItem.SelectedValue));
                                                                    }               
                                                                }
                                                            }
                                                            if (txtFactorM2Ip.Text != "")
                                                            {
                                                                if (btnGuardarIp.Text == "Editar")
                                                                {
                                                                    if (!Session["item_planta_id"].ToString().Equals("0"))
                                                                    {
                                                                        resultado1 = ActualizarItemPlanta(Convert.ToInt64(Session["item_planta_id"].ToString()), Convert.ToInt32(chkListorigen.SelectedValue), Convert.ToDecimal(txtPeso_unitario.Text), 0,
                                                                                                          Convert.ToDecimal(txtFactorM2Ip.Text), Convert.ToDecimal(txtPesoEmpaqueIa.Text), Convert.ToInt32(txtCantidadEmpaqueIa.Text), Convert.ToDecimal(txtLargo.Text),
                                                                                                          Convert.ToDecimal(txtAncho1.Text), Convert.ToDecimal(txtAncho2.Text), Convert.ToDecimal(txtAlto1.Text), Convert.ToDecimal(txtAlto2.Text), tipo_kamban, 
                                                                                                          disp_cotizacion, disp_comercial, disp_ingenieria, disp_almacen, disp_produccion, req_plano, req_tipo, req_modelo, tipo_orden_prod_id,
                                                                                                          req_inspCalidad, req_inspeObliga, Convert.ToInt32(cboClaseItem.SelectedValue));
                                                                    }
                                                                }
                                                            }
                                                            if (cboAdicionalIp.Enabled == false && txtfactorunitario.Visible)
                                                            {
                                                                if (btnGuardarIp.Text == "Editar")
                                                                {
                                                                    if (!Session["item_planta_id"].ToString().Equals("0"))
                                                                    {
                                                                        resultado1 = ActualizarItemPlanta(Convert.ToInt64(Session["item_planta_id"].ToString()), Convert.ToInt32(chkListorigen.SelectedValue), Convert.ToDecimal(txtPeso_unitario.Text), 1, 0, 
                                                                                                          Convert.ToDecimal(txtPesoEmpaqueIa.Text), Convert.ToInt32(txtCantidadEmpaqueIa.Text), Convert.ToDecimal(txtLargo.Text), Convert.ToDecimal(txtAncho1.Text),
                                                                                                          Convert.ToDecimal(txtAncho2.Text), Convert.ToDecimal(txtAlto1.Text), Convert.ToDecimal(txtAlto2.Text), tipo_kamban, disp_cotizacion, disp_comercial,
                                                                                                          disp_ingenieria, disp_almacen, disp_produccion, req_plano, req_tipo, req_modelo, tipo_orden_prod_id, req_inspCalidad,
                                                                                                          req_inspeObliga, Convert.ToInt32(cboClaseItem.SelectedValue));
                                                                    }
                                                                }

                                                            }
                                                            if (!txtfactorunitario.Visible && txtFactorLIp.Text.Equals("") && txtFactorM2Ip.Text.Equals(""))
                                                            {
                                                                if (btnGuardarIp.Text == "Editar")
                                                                {
                                                                    if (!Session["item_planta_id"].ToString().Equals("0"))
                                                                    {
                                                                        resultado1 = ActualizarItemPlanta(Convert.ToInt64(Session["item_planta_id"].ToString()), Convert.ToInt32(chkListorigen.SelectedValue), Convert.ToDecimal(txtPeso_unitario.Text), 0, 0,
                                                                                                          Convert.ToDecimal(txtPesoEmpaqueIa.Text), Convert.ToInt32(txtCantidadEmpaqueIa.Text), Convert.ToDecimal(txtLargo.Text), Convert.ToDecimal(txtAncho1.Text),
                                                                                                          Convert.ToDecimal(txtAncho2.Text), Convert.ToDecimal(txtAlto1.Text), Convert.ToDecimal(txtAlto2.Text), tipo_kamban, disp_cotizacion, disp_comercial,
                                                                                                          disp_ingenieria, disp_almacen, disp_produccion, req_plano, req_tipo, req_modelo, tipo_orden_prod_id, req_inspCalidad,
                                                                                                          req_inspeObliga, Convert.ToInt32(cboClaseItem.SelectedValue));
                                                                    }
                                                                }
                                                            }
                                                            if (resultado1)
                                                            {
                                                                mensajeVentana("Item se ha editado correctamente!!");
                                                                if (!Session["item_planta_id"].ToString().Equals("0"))
                                                                {
                                                                    bool bandera = false, bandera2 = false;
                                                                    if (btnAprobarIp.Visible)
                                                                    {
                                                                        bandera = true;
                                                                    }
                                                                    if (btnduplicar.Visible)
                                                                    {
                                                                        bandera2 = true;
                                                                    }
                                                                    cboPerfilIp_SelectedIndexChanged(cboPerfilIp, e);
                                                                    CargarDatos(Session["item_planta_id"].ToString(), e);
                                                                    CargarIdiomaEditado(Session["item_planta_id"].ToString());
                                                                    CargarPrecioEditado(Session["item_planta_id"].ToString());
                                                                    AccordionPrincipalIp.SelectedIndex = 0;
                                                                    btnGuardarIp.Text = "Editar";
                                                                    btnGuardarIp.Visible = true;
                                                                    btnEnviarIp.Visible = true;
                                                                    if (bandera)
                                                                    {
                                                                        btnAprobarIp.Visible = true;
                                                                        btnDevolverIp.Visible = true;
                                                                        btnRechazarIp.Visible = true;
                                                                        trlblobservacion.Visible = true;
                                                                        trtxtobservacion.Visible = true;
                                                                        btnEnviarIp.Visible = false;
                                                                    }
                                                                    if (bandera2)
                                                                    {
                                                                        trlblobservacion.Visible = true;
                                                                        txtobservestado.Enabled = false;
                                                                        trtxtobservacion.Visible = true;
                                                                        btnduplicar.Visible = true;
                                                                        btnEnviarIp.Visible = false;
                                                                    }
                                                                }
                                                            }
                                                            dtitem = cmIp.Recuperar_IdItemPlanta(txtDscIp.Text, txtDscAbrvIp.Text, int.Parse(cboPlantaIp.SelectedValue.ToString()));
                                                            string idItemPlanta = dtitem.Rows[0][0].ToString();

                                                            respuesta = cmIp.Crear_Accesorio(0, txt_NombAcce.Text, txt_Desc_Abrev.Text, float.Parse(txt_Dim1min.Text), float.Parse(txt_Dim1max.Text)
    , float.Parse(txt_Dim2min.Text), float.Parse(txt_Dim2max.Text), float.Parse(txt_Dim3min.Text), float.Parse(txt_Dim3max.Text), float.Parse(txt_Dim4min.Text), float.Parse(txt_Dim4max.Text),
    float.Parse(txt_Dim5min.Text), float.Parse(txt_Dim5max.Text), float.Parse(txt_Dim6min.Text), float.Parse(txt_Dim6max.Text), float.Parse(txt_Dim7min.Text), float.Parse(txt_Dim7max.Text),
    int.Parse(cboPlantaIp.SelectedValue.ToString()), 0, int.Parse(idItemPlanta));                                                
                                                            Consultar_Accesorios();

                                                            Consultar_Detalle_Accesorio();
                                                            if (LblAnulado.Text == "False")
                                                            {
                                                                btn_Anular.Visible = Visible;
                                                                LblEstadoAnula.Text = "Activo";
                                                            }
                                                            else if (LblAnulado.Text == "True")
                                                            {
                                                                btn_Anular.Visible = false;
                                                                LblEstadoAnula.Text = "Inactivo";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (valida1 && valida2 && valida3 && valida4 && valida5 && valida6 && valida7 == true)
                                                            {
                                                                mensajeVentana("Valide que las dimensiones a crear, no se cruzen con las de un accesorio existente");
                                                            }
                                                            Consultar_Accesorios();
                                                        }
                                                    }

                                                    #endregion
                                                                                                           
                                                        #endregion
                                                    
                                                    else
                                                    {
                                                        dt = cmIp.Consultar_AccesoriosDtt(txt_NombAcce.Text, txt_Desc_Abrev.Text, int.Parse(cboPlantaIp.SelectedValue.ToString()), int.Parse((Session["item_planta_id"].ToString())));

                                                        for (int i = 0; i < dt.Rows.Count; i++)
                                                        {

                                                            if (aprobar == false)
                                                            {
                                                          
                                                            if (float.Parse(dt.Rows[i][3].ToString()) == 0 && float.Parse(dt.Rows[i][4].ToString()) == 0 && txt_Dim1min.Text != txt_Dim1max.Text)
                                                            {
                                                                valida1 = true;
                                                            }
                                                            else if ((float.Parse(txt_Dim1min.Text) == 0 && float.Parse(txt_Dim1max.Text) == 0 && float.Parse(dt.Rows[i][3].ToString()) != 0 && float.Parse(dt.Rows[i][4].ToString()) != 0) ||
                                                                    (float.Parse(txt_Dim1min.Text) == 0 && float.Parse(txt_Dim1max.Text) == 0 && float.Parse(dt.Rows[i][3].ToString()) == 0 && float.Parse(dt.Rows[i][4].ToString()) != 0))
                                                            {
                                                                valida1 = true;
                                                            }
                                                            else if (validar_Campos(float.Parse(dt.Rows[i][3].ToString()), float.Parse(dt.Rows[i][4].ToString()), float.Parse(txt_Dim1min.Text), float.Parse(txt_Dim1max.Text)) ||
                                                                     validar_Campos(float.Parse(txt_Dim1min.Text), float.Parse(txt_Dim1max.Text), float.Parse(dt.Rows[i][3].ToString()), float.Parse(dt.Rows[i][4].ToString()))
                                                                    )
                                                            {
                                                                valida1 = true;
                                                            }
                                                            else
                                                            {
                                                                valida1 = false;
                                                            }

                                                            if (float.Parse(dt.Rows[i][5].ToString()) == 0 && float.Parse(dt.Rows[i][6].ToString()) == 0 && txt_Dim2min.Text != txt_Dim2max.Text)
                                                            {
                                                                valida2 = true;
                                                            }
                                                            else if ((float.Parse(txt_Dim2min.Text) == 0 && float.Parse(txt_Dim2max.Text) == 0 && float.Parse(dt.Rows[i][5].ToString()) != 0 && float.Parse(dt.Rows[i][6].ToString()) != 0) ||
                                                                    (float.Parse(txt_Dim2min.Text) == 0 && float.Parse(txt_Dim2max.Text) == 0 && float.Parse(dt.Rows[i][5].ToString()) == 0 && float.Parse(dt.Rows[i][6].ToString()) != 0))
                                                            {
                                                                valida2 = true;
                                                            }
                                                            else if (validar_Campos(float.Parse(dt.Rows[i][5].ToString()), float.Parse(dt.Rows[i][6].ToString()), float.Parse(txt_Dim2min.Text), float.Parse(txt_Dim2max.Text)) ||
                                                                     validar_Campos(float.Parse(txt_Dim2min.Text), float.Parse(txt_Dim2max.Text), float.Parse(dt.Rows[i][5].ToString()), float.Parse(dt.Rows[i][6].ToString()))
                                                                    )
                                                            {
                                                                valida2 = true;
                                                            }
                                                            else
                                                            {
                                                                valida2 = false;
                                                            }

                                                            if (float.Parse(dt.Rows[i][7].ToString()) == 0 && float.Parse(dt.Rows[i][8].ToString()) == 0 && txt_Dim3min.Text != txt_Dim3max.Text)
                                                            {
                                                                valida3 = true;
                                                            }
                                                            else if ((float.Parse(txt_Dim3min.Text) == 0 && float.Parse(txt_Dim3max.Text) == 0 && float.Parse(dt.Rows[i][7].ToString()) != 0 && float.Parse(dt.Rows[i][8].ToString()) != 0) ||
                                                                    (float.Parse(txt_Dim3min.Text) == 0 && float.Parse(txt_Dim3max.Text) == 0 && float.Parse(dt.Rows[i][7].ToString()) == 0 && float.Parse(dt.Rows[i][8].ToString()) != 0))
                                                            {
                                                                valida3 = true;
                                                            }
                                                            else if (validar_Campos(float.Parse(dt.Rows[i][7].ToString()), float.Parse(dt.Rows[i][8].ToString()), float.Parse(txt_Dim3min.Text), float.Parse(txt_Dim3max.Text)) ||
                                                                     validar_Campos(float.Parse(txt_Dim3min.Text), float.Parse(txt_Dim3max.Text), float.Parse(dt.Rows[i][7].ToString()), float.Parse(dt.Rows[i][8].ToString()))
                                                                    )
                                                            {
                                                                valida3 = true;
                                                            }
                                                            else
                                                            {
                                                                valida3 = false;
                                                            }

                                                            if (float.Parse(dt.Rows[i][9].ToString()) == 0 && float.Parse(dt.Rows[i][10].ToString()) == 0 && txt_Dim4min.Text != txt_Dim4max.Text)
                                                            {
                                                                valida4 = true;
                                                            }
                                                            else if ((float.Parse(txt_Dim4min.Text) == 0 && float.Parse(txt_Dim4max.Text) == 0 && float.Parse(dt.Rows[i][9].ToString()) != 0 && float.Parse(dt.Rows[i][10].ToString()) != 0) ||
                                                                    (float.Parse(txt_Dim4min.Text) == 0 && float.Parse(txt_Dim4max.Text) == 0 && float.Parse(dt.Rows[i][9].ToString()) == 0 && float.Parse(dt.Rows[i][10].ToString()) != 0))
                                                            {
                                                                valida4 = true;
                                                            }
                                                            else if (validar_Campos(float.Parse(dt.Rows[i][9].ToString()), float.Parse(dt.Rows[i][10].ToString()), float.Parse(txt_Dim4min.Text), float.Parse(txt_Dim4max.Text)) ||
                                                                     validar_Campos(float.Parse(txt_Dim4min.Text), float.Parse(txt_Dim4max.Text), float.Parse(dt.Rows[i][9].ToString()), float.Parse(dt.Rows[i][10].ToString()))
                                                                    )
                                                            {
                                                                valida4 = true;
                                                            }
                                                            else
                                                            {
                                                                valida4 = false;
                                                            }

                                                            if (float.Parse(dt.Rows[i][11].ToString()) == 0 && float.Parse(dt.Rows[i][12].ToString()) == 0 && txt_Dim5min.Text != txt_Dim5max.Text)
                                                            {
                                                                valida5 = true;
                                                            }
                                                            else if ((float.Parse(txt_Dim5min.Text) == 0 && float.Parse(txt_Dim5max.Text) == 0 && float.Parse(dt.Rows[i][11].ToString()) != 0 && float.Parse(dt.Rows[i][12].ToString()) != 0) ||
                                                                    (float.Parse(txt_Dim5min.Text) == 0 && float.Parse(txt_Dim5max.Text) == 0 && float.Parse(dt.Rows[i][11].ToString()) == 0 && float.Parse(dt.Rows[i][12].ToString()) != 0))
                                                            {
                                                                valida5 = true;
                                                            }
                                                            else if (validar_Campos(float.Parse(dt.Rows[i][11].ToString()), float.Parse(dt.Rows[i][12].ToString()), float.Parse(txt_Dim5min.Text), float.Parse(txt_Dim5max.Text)) ||
                                                                     validar_Campos(float.Parse(txt_Dim5min.Text), float.Parse(txt_Dim5max.Text), float.Parse(dt.Rows[i][11].ToString()), float.Parse(dt.Rows[i][12].ToString()))
                                                                    )
                                                            {
                                                                valida5 = true;
                                                            }
                                                            else
                                                            {
                                                                valida5 = false;
                                                            }

                                                            if (float.Parse(dt.Rows[i][13].ToString()) == 0 && float.Parse(dt.Rows[i][14].ToString()) == 0 && txt_Dim6min.Text != txt_Dim6max.Text)
                                                            {
                                                                valida6 = true;
                                                            }
                                                            else if ((float.Parse(txt_Dim6min.Text) == 0 && float.Parse(txt_Dim6max.Text) == 0 && float.Parse(dt.Rows[i][13].ToString()) != 0 && float.Parse(dt.Rows[i][14].ToString()) != 0) ||
                                                                    (float.Parse(txt_Dim6min.Text) == 0 && float.Parse(txt_Dim6max.Text) == 0 && float.Parse(dt.Rows[i][13].ToString()) == 0 && float.Parse(dt.Rows[i][14].ToString()) != 0))
                                                            {
                                                                valida6 = true;
                                                            }
                                                            else if (validar_Campos(float.Parse(dt.Rows[i][13].ToString()), float.Parse(dt.Rows[i][14].ToString()), float.Parse(txt_Dim6min.Text), float.Parse(txt_Dim6max.Text)) ||
                                                                     validar_Campos(float.Parse(txt_Dim6min.Text), float.Parse(txt_Dim6max.Text), float.Parse(dt.Rows[i][13].ToString()), float.Parse(dt.Rows[i][14].ToString()))
                                                                    )
                                                            {
                                                                valida6 = true;
                                                            }
                                                            else
                                                            {
                                                                valida6 = false;
                                                            }

                                                            if (float.Parse(dt.Rows[i][15].ToString()) == 0 && float.Parse(dt.Rows[i][16].ToString()) == 0 && txt_Dim7min.Text != txt_Dim7max.Text)
                                                            {
                                                                valida7 = true;
                                                            }
                                                            else if ((float.Parse(txt_Dim7min.Text) == 0 && float.Parse(txt_Dim7max.Text) == 0 && float.Parse(dt.Rows[i][15].ToString()) != 0 && float.Parse(dt.Rows[i][16].ToString()) != 0) ||
                                                                    (float.Parse(txt_Dim7min.Text) == 0 && float.Parse(txt_Dim7max.Text) == 0 && float.Parse(dt.Rows[i][15].ToString()) == 0 && float.Parse(dt.Rows[i][16].ToString()) != 0))
                                                            {
                                                                valida7 = true;
                                                            }
                                                            else if (validar_Campos(float.Parse(dt.Rows[i][15].ToString()), float.Parse(dt.Rows[i][16].ToString()), float.Parse(txt_Dim7min.Text), float.Parse(txt_Dim7max.Text)) ||
                                                                     validar_Campos(float.Parse(txt_Dim7min.Text), float.Parse(txt_Dim7max.Text), float.Parse(dt.Rows[i][15].ToString()), float.Parse(dt.Rows[i][16].ToString()))
                                                                    )
                                                            {
                                                                valida7 = true;
                                                            }
                                                            else
                                                            {
                                                                valida7 = false;
                                                            }
                                                            if ((valida1 && valida2 && valida3 && valida4 &&
                                                           valida5 && valida6 && valida7) == false)
                                                            {
                                                                aprobar = false;
                                                            }
                                                            else if (valida1 == true && valida2 == true && valida3 == true && valida4 == true &&
                                                           valida5 == true && valida6 == true && valida7 == true)
                                                            {
                                                                aprobar = true;
                                                            }
                                                            //Si aprobar es true, ya no valida mas y se sale del ciclo
                                                            }
                                                        }
                                                        if (aprobar == false)
                                                        {
                                                            #region capturan los datos necesarios para insertar en la tabla item_planta
                                                            if (txtFactorLIp.Text != "")
                                                            {
                                                                if (btnGuardarIp.Text == "Editar")
                                                                {
                                                                    if (!Session["item_planta_id"].ToString().Equals("0"))
                                                                    {
                                                                        resultado1 = ActualizarItemPlanta(Convert.ToInt64(Session["item_planta_id"].ToString()), Convert.ToInt32(chkListorigen.SelectedValue), Convert.ToDecimal(txtPeso_unitario.Text),
                                                                                                          Convert.ToDecimal(txtFactorLIp.Text), 0, Convert.ToDecimal(txtPesoEmpaqueIa.Text), Convert.ToInt32(txtCantidadEmpaqueIa.Text), Convert.ToDecimal(txtLargo.Text),
                                                                                                          Convert.ToDecimal(txtAncho1.Text), Convert.ToDecimal(txtAncho2.Text), Convert.ToDecimal(txtAlto1.Text), Convert.ToDecimal(txtAlto2.Text), tipo_kamban, disp_cotizacion, 
                                                                                                          disp_comercial, disp_ingenieria, disp_almacen, disp_produccion, req_plano, req_tipo, req_modelo, tipo_orden_prod_id, req_inspCalidad,
                                                                                                          req_inspeObliga, Convert.ToInt32(cboClaseItem.SelectedValue));
                                                                    }
                                                                }
                                                            }
                                                            if (txtFactorM2Ip.Text != "")
                                                            {
                                                                if (btnGuardarIp.Text == "Editar")
                                                                {
                                                                    if (!Session["item_planta_id"].ToString().Equals("0"))
                                                                    {
                                                                        resultado1 = ActualizarItemPlanta(Convert.ToInt64(Session["item_planta_id"].ToString()), Convert.ToInt32(chkListorigen.SelectedValue), Convert.ToDecimal(txtPeso_unitario.Text), 0,
                                                                                                          Convert.ToDecimal(txtFactorM2Ip.Text), Convert.ToDecimal(txtPesoEmpaqueIa.Text), Convert.ToInt32(txtCantidadEmpaqueIa.Text), Convert.ToDecimal(txtLargo.Text),
                                                                                                          Convert.ToDecimal(txtAncho1.Text), Convert.ToDecimal(txtAncho2.Text), Convert.ToDecimal(txtAlto1.Text), Convert.ToDecimal(txtAlto2.Text), tipo_kamban, 
                                                                                                          disp_cotizacion, disp_comercial, disp_ingenieria, disp_almacen, disp_produccion, req_plano, req_tipo, req_modelo, tipo_orden_prod_id, req_inspCalidad,
                                                                                                          req_inspeObliga, Convert.ToInt32(cboClaseItem.SelectedValue));
                                                                    }
                                                                }
                                                            }
                                                            if (cboAdicionalIp.Enabled == false && txtfactorunitario.Visible)
                                                            {
                                                                if (btnGuardarIp.Text == "Editar")
                                                                {
                                                                    if (!Session["item_planta_id"].ToString().Equals("0"))
                                                                    {
                                                                        resultado1 = ActualizarItemPlanta(Convert.ToInt64(Session["item_planta_id"].ToString()), Convert.ToInt32(chkListorigen.SelectedValue), Convert.ToDecimal(txtPeso_unitario.Text), 1, 0,
                                                                                                          Convert.ToDecimal(txtPesoEmpaqueIa.Text), Convert.ToInt32(txtCantidadEmpaqueIa.Text), Convert.ToDecimal(txtLargo.Text), Convert.ToDecimal(txtAncho1.Text),
                                                                                                          Convert.ToDecimal(txtAncho2.Text), Convert.ToDecimal(txtAlto1.Text), Convert.ToDecimal(txtAlto2.Text), tipo_kamban, disp_cotizacion, disp_comercial,
                                                                                                          disp_ingenieria, disp_almacen, disp_produccion, req_plano, req_tipo, req_modelo, tipo_orden_prod_id, req_inspCalidad,
                                                                                                          req_inspeObliga, Convert.ToInt32(cboClaseItem.SelectedValue));
                                                                    }
                                                                }

                                                            }
                                                            if (!txtfactorunitario.Visible && txtFactorLIp.Text.Equals("") && txtFactorM2Ip.Text.Equals(""))
                                                            {
                                                                if (btnGuardarIp.Text == "Editar")
                                                                {
                                                                    if (!Session["item_planta_id"].ToString().Equals("0"))
                                                                    {
                                                                        resultado1 = ActualizarItemPlanta(Convert.ToInt64(Session["item_planta_id"].ToString()), Convert.ToInt32(chkListorigen.SelectedValue), Convert.ToDecimal(txtPeso_unitario.Text), 0, 0, 
                                                                                                          Convert.ToDecimal(txtPesoEmpaqueIa.Text), Convert.ToInt32(txtCantidadEmpaqueIa.Text), Convert.ToDecimal(txtLargo.Text), Convert.ToDecimal(txtAncho1.Text), 
                                                                                                          Convert.ToDecimal(txtAncho2.Text), Convert.ToDecimal(txtAlto1.Text), Convert.ToDecimal(txtAlto2.Text), tipo_kamban, disp_cotizacion, disp_comercial, 
                                                                                                          disp_ingenieria, disp_almacen, disp_produccion, req_plano, req_tipo, req_modelo, tipo_orden_prod_id, req_inspCalidad,
                                                                                                          req_inspeObliga, Convert.ToInt32(cboClaseItem.SelectedValue));
                                                                    }
                                                                }
                                                            }
                                                            if (resultado1)
                                                            {
                                                                mensajeVentana("Item se ha editado correctamente!!");
                                                                if (!Session["item_planta_id"].ToString().Equals("0"))
                                                                {
                                                                    bool bandera = false, bandera2 = false;
                                                                    if (btnAprobarIp.Visible)
                                                                    {
                                                                        bandera = true;
                                                                    }
                                                                    if (btnduplicar.Visible)
                                                                    {
                                                                        bandera2 = true;
                                                                    }
                                                                    cboPerfilIp_SelectedIndexChanged(cboPerfilIp, e);
                                                                    CargarDatos(Session["item_planta_id"].ToString(), e);
                                                                    CargarIdiomaEditado(Session["item_planta_id"].ToString());
                                                                    CargarPrecioEditado(Session["item_planta_id"].ToString());
                                                                    AccordionPrincipalIp.SelectedIndex = 0;
                                                                    btnGuardarIp.Text = "Editar";
                                                                    btnGuardarIp.Visible = true;
                                                                    btnEnviarIp.Visible = true;
                                                                    if (bandera)
                                                                    {
                                                                        btnAprobarIp.Visible = true;
                                                                        btnDevolverIp.Visible = true;
                                                                        btnRechazarIp.Visible = true;
                                                                        trlblobservacion.Visible = true;
                                                                        trtxtobservacion.Visible = true;
                                                                        btnEnviarIp.Visible = false;
                                                                    }
                                                                    if (bandera2)
                                                                    {
                                                                        trlblobservacion.Visible = true;
                                                                        txtobservestado.Enabled = false;
                                                                        trtxtobservacion.Visible = true;
                                                                        btnduplicar.Visible = true;
                                                                        btnEnviarIp.Visible = false;
                                                                    }
                                                                }
                                                            }
                                                            Obtener_CodigoId_Accesorio();                                                  
                                                            #endregion
                                                            respuesta = cmIp.Actualizar_Detalle_Accesorio(txt_NombAcce.Text, txt_Desc_Abrev.Text, float.Parse(txt_Dim1min.Text), float.Parse(txt_Dim1max.Text)
                                                                  , float.Parse(txt_Dim2min.Text), float.Parse(txt_Dim2max.Text), float.Parse(txt_Dim3min.Text), float.Parse(txt_Dim3max.Text), float.Parse(txt_Dim4min.Text), float.Parse(txt_Dim4max.Text),
                                                                    float.Parse(txt_Dim5min.Text), float.Parse(txt_Dim5max.Text), float.Parse(txt_Dim6min.Text), float.Parse(txt_Dim6max.Text), float.Parse(txt_Dim7min.Text), float.Parse(txt_Dim7max.Text),
                                                                    int.Parse(Session["item_planta_id"].ToString()), int.Parse(cboPlantaIp.SelectedValue.ToString()), int.Parse(LblIdAccesorio.Text));
                                                            Consultar_Accesorios();
                                                        }
                                                        else
                                                        {
                                                            if (valida1 && valida2 && valida3 && valida4 && valida5 && valida6 && valida7 == true)
                                                            {
                                                                mensajeVentana("Valide que las dimensiones a crear, no se cruzen con las de un accesorio existente");
                                                            }
                                                            Consultar_Accesorios();
                                                        }
                                                  
                                                    }
                                                    #endregion
                                                }
                                            }
                                            else
                                            {
                                                mensajeVentana("La dimension 7 minima debe ser menor a la dimension 7 maxima");
                                            }
                                        }
                                        else
                                        {
                                            mensajeVentana("La dimension 6 minima debe ser menor a la dimension 6 maxima");
                                        }
                                    }
                                    else
                                    {
                                        mensajeVentana("La dimension 5 minima debe ser menor a la dimension 5 maxima");
                                    }
                                }
                                else
                                {
                                    mensajeVentana("La dimension 4 minima debe ser menor a la dimension 4 maxima");
                                }
                            }
                            else
                            {
                                mensajeVentana("La dimension 3 minima debe ser menor a la dimension 3 maxima");
                            }
                        }
                        else
                        {
                            mensajeVentana("La dimension 2 minima debe ser menor a la dimension 2 maxima");
                        }
                    }
                    else
                    {
                        mensajeVentana("La dimension 1 minima debe ser menor a la dimension 1 maxima");
                    }
                }
                else
                {
                    mensajeVentana("Debe diligenciar el nombre del accesorio");
                }
                #endregion
            }
            else
            {
                Habilitar_Campos_Accesorio(false);
                #region capturan los datos necesarios para insertar en la tabla item_planta
                //Se capturan los datos necesarios para insertar en la tabla item_planta
                if (txtFactorLIp.Text != "")
                {
                    if (btnGuardarIp.Text == "Guardar")
                    {
                        resultado = InsertarItemPlanta(Convert.ToInt32(chkListorigen.SelectedValue), Convert.ToDecimal(txtPeso_unitario.Text), Convert.ToDecimal(txtFactorLIp.Text), 0, 
                                                       Convert.ToDecimal(txtPesoEmpaqueIa.Text), Convert.ToInt32(txtCantidadEmpaqueIa.Text), Convert.ToDecimal(txtLargo.Text), 
                                                       Convert.ToDecimal(txtAncho1.Text), Convert.ToDecimal(txtAncho2.Text), Convert.ToDecimal(txtAlto1.Text),
                                                       Convert.ToDecimal(txtAlto2.Text), tipo_kamban, disp_cotizacion, disp_comercial, disp_ingenieria, 
                                                       disp_almacen, disp_produccion, req_plano, req_tipo, req_modelo, tipo_orden_prod_id, req_inspCalidad,
                                                       req_inspeObliga, Convert.ToInt32(cboClaseItem.SelectedValue));
                    }
                    if (btnGuardarIp.Text == "Editar")
                    {
                        if (!Session["item_planta_id"].ToString().Equals("0"))
                        {
                            resultado1 = ActualizarItemPlanta(Convert.ToInt64(Session["item_planta_id"].ToString()), Convert.ToInt32(chkListorigen.SelectedValue),
                                                              Convert.ToDecimal(txtPeso_unitario.Text), Convert.ToDecimal(txtFactorLIp.Text), 0, Convert.ToDecimal(txtPesoEmpaqueIa.Text), 
                                                              Convert.ToInt32(txtCantidadEmpaqueIa.Text), Convert.ToDecimal(txtLargo.Text), Convert.ToDecimal(txtAncho1.Text),
                                                              Convert.ToDecimal(txtAncho2.Text), Convert.ToDecimal(txtAlto1.Text), Convert.ToDecimal(txtAlto2.Text), tipo_kamban,
                                                              disp_cotizacion, disp_comercial, disp_ingenieria, disp_almacen, disp_produccion, req_plano, req_tipo, req_modelo,
                                                              tipo_orden_prod_id, req_inspCalidad, req_inspeObliga, Convert.ToInt32(cboClaseItem.SelectedValue));
                        }
                    }
                }
                if (txtFactorM2Ip.Text != "")
                {
                    if (btnGuardarIp.Text == "Guardar")
                    {
                        resultado = InsertarItemPlanta(Convert.ToInt32(chkListorigen.SelectedValue), Convert.ToDecimal(txtPeso_unitario.Text), 0, 
                                                       Convert.ToDecimal(txtFactorM2Ip.Text), Convert.ToDecimal(txtPesoEmpaqueIa.Text), Convert.ToInt32(txtCantidadEmpaqueIa.Text),
                                                       Convert.ToDecimal(txtLargo.Text), Convert.ToDecimal(txtAncho1.Text), Convert.ToDecimal(txtAncho2.Text),
                                                       Convert.ToDecimal(txtAlto1.Text), Convert.ToDecimal(txtAlto2.Text), tipo_kamban, disp_cotizacion,
                                                       disp_comercial, disp_ingenieria, disp_almacen, disp_produccion, req_plano, req_tipo, req_modelo, 
                                                       tipo_orden_prod_id, req_inspCalidad, req_inspeObliga, Convert.ToInt32(cboClaseItem.SelectedValue));
                    }
                    if (btnGuardarIp.Text == "Editar")
                    {
                        if (!Session["item_planta_id"].ToString().Equals("0"))
                        {
                            resultado1 = ActualizarItemPlanta(Convert.ToInt64(Session["item_planta_id"].ToString()), Convert.ToInt32(chkListorigen.SelectedValue), Convert.ToDecimal(txtPeso_unitario.Text), 0,
                                                              Convert.ToDecimal(txtFactorM2Ip.Text), Convert.ToDecimal(txtPesoEmpaqueIa.Text), Convert.ToInt32(txtCantidadEmpaqueIa.Text), Convert.ToDecimal(txtLargo.Text),
                                                              Convert.ToDecimal(txtAncho1.Text), Convert.ToDecimal(txtAncho2.Text), Convert.ToDecimal(txtAlto1.Text), Convert.ToDecimal(txtAlto2.Text), tipo_kamban,
                                                              disp_cotizacion, disp_comercial, disp_ingenieria, disp_almacen, disp_produccion, req_plano, req_tipo, req_modelo, tipo_orden_prod_id, req_inspCalidad,
                                                              req_inspeObliga, Convert.ToInt32(cboClaseItem.SelectedValue));
                        }
                    }
                }
                if (cboAdicionalIp.Enabled == false && txtfactorunitario.Visible)
                {
                    if (btnGuardarIp.Text == "Guardar")
                    {
                        resultado = InsertarItemPlanta(Convert.ToInt32(chkListorigen.SelectedValue), Convert.ToDecimal(txtPeso_unitario.Text), 1, 0, 
                                                       Convert.ToDecimal(txtPesoEmpaqueIa.Text), Convert.ToInt32(txtCantidadEmpaqueIa.Text), 
                                                       Convert.ToDecimal(txtLargo.Text), Convert.ToDecimal(txtAncho1.Text), Convert.ToDecimal(txtAncho2.Text), 
                                                       Convert.ToDecimal(txtAlto1.Text), Convert.ToDecimal(txtAlto2.Text), tipo_kamban, disp_cotizacion,
                                                       disp_comercial, disp_ingenieria, disp_almacen, disp_produccion, req_plano, req_tipo, req_modelo,
                                                       tipo_orden_prod_id, req_inspCalidad, req_inspeObliga, Convert.ToInt32(cboClaseItem.SelectedValue));

                    }
                    if (btnGuardarIp.Text == "Editar")
                    {
                        if (!Session["item_planta_id"].ToString().Equals("0"))
                        {
                            resultado1 = ActualizarItemPlanta(Convert.ToInt64(Session["item_planta_id"].ToString()), Convert.ToInt32(chkListorigen.SelectedValue), Convert.ToDecimal(txtPeso_unitario.Text), 1, 0,
                                                              Convert.ToDecimal(txtPesoEmpaqueIa.Text), Convert.ToInt32(txtCantidadEmpaqueIa.Text), Convert.ToDecimal(txtLargo.Text), Convert.ToDecimal(txtAncho1.Text),
                                                              Convert.ToDecimal(txtAncho2.Text), Convert.ToDecimal(txtAlto1.Text), Convert.ToDecimal(txtAlto2.Text), tipo_kamban, disp_cotizacion, disp_comercial,
                                                              disp_ingenieria, disp_almacen, disp_produccion, req_plano, req_tipo, req_modelo, tipo_orden_prod_id, req_inspCalidad,
                                                              req_inspeObliga, Convert.ToInt32(cboClaseItem.SelectedValue));
                        }
                    }

                }
                if (!txtfactorunitario.Visible && txtFactorLIp.Text.Equals("") && txtFactorM2Ip.Text.Equals(""))
                {
                    if (btnGuardarIp.Text == "Guardar")
                    {
                        resultado = InsertarItemPlanta(Convert.ToInt32(chkListorigen.SelectedValue), Convert.ToDecimal(txtPeso_unitario.Text), 0, 0,
                                                       Convert.ToDecimal(txtPesoEmpaqueIa.Text), Convert.ToInt32(txtCantidadEmpaqueIa.Text), 
                                                       Convert.ToDecimal(txtLargo.Text), Convert.ToDecimal(txtAncho1.Text), Convert.ToDecimal(txtAncho2.Text), 
                                                       Convert.ToDecimal(txtAlto1.Text), Convert.ToDecimal(txtAlto2.Text), tipo_kamban, disp_cotizacion,
                                                       disp_comercial, disp_ingenieria, disp_almacen, disp_produccion, req_plano, req_tipo, req_modelo,
                                                       tipo_orden_prod_id, req_inspCalidad, req_inspeObliga, Convert.ToInt32(cboClaseItem.SelectedValue));

                    }
                    if (btnGuardarIp.Text == "Editar")
                    {
                        if (!Session["item_planta_id"].ToString().Equals("0"))
                        {
                            resultado1 = ActualizarItemPlanta(Convert.ToInt64(Session["item_planta_id"].ToString()), Convert.ToInt32(chkListorigen.SelectedValue), Convert.ToDecimal(txtPeso_unitario.Text), 0, 0,
                                                              Convert.ToDecimal(txtPesoEmpaqueIa.Text), Convert.ToInt32(txtCantidadEmpaqueIa.Text), Convert.ToDecimal(txtLargo.Text), Convert.ToDecimal(txtAncho1.Text), 
                                                              Convert.ToDecimal(txtAncho2.Text), Convert.ToDecimal(txtAlto1.Text), Convert.ToDecimal(txtAlto2.Text), tipo_kamban, disp_cotizacion, disp_comercial,
                                                              disp_ingenieria, disp_almacen, disp_produccion, req_plano, req_tipo, req_modelo, tipo_orden_prod_id, req_inspCalidad,
                                                              req_inspeObliga, Convert.ToInt32(cboClaseItem.SelectedValue));
                        }
                    }
                }
                if (resultado)
                {
                    mensajeVentana("Item se ha insertado correctamente!!");
                    cboPerfilIp_SelectedIndexChanged(cboPerfilIp, e);
                    CargarDatos(Session["item_planta_id"].ToString(), e);
                    CargarIdiomaEditado(Session["item_planta_id"].ToString());
                    CargarPrecioEditado(Session["item_planta_id"].ToString());
                    AccordionPrincipalIp.SelectedIndex = 0;
                    btnGuardarIp.Text = "Editar";
                    btnEnviarIp.Visible = true;                 
                }
                if (resultado1)
                {
                    mensajeVentana("Item se ha editado correctamente!!");
                    if (!Session["item_planta_id"].ToString().Equals("0"))
                    {
                        bool bandera = false, bandera2 = false;
                        if (btnAprobarIp.Visible)
                        {
                            bandera = true;
                        }
                        if (btnduplicar.Visible)
                        {
                            bandera2 = true;
                        }
                        cboPerfilIp_SelectedIndexChanged(cboPerfilIp, e);
                        CargarDatos(Session["item_planta_id"].ToString(), e);
                        CargarIdiomaEditado(Session["item_planta_id"].ToString());
                        CargarPrecioEditado(Session["item_planta_id"].ToString());
                        AccordionPrincipalIp.SelectedIndex = 0;
                        btnGuardarIp.Text = "Editar";
                        btnGuardarIp.Visible = true;
                        btnEnviarIp.Visible = true;
                        if (bandera)
                        {
                            btnAprobarIp.Visible = true;
                            btnDevolverIp.Visible = true;
                            btnRechazarIp.Visible = true;
                            trlblobservacion.Visible = true;
                            trtxtobservacion.Visible = true;
                            btnEnviarIp.Visible = false;

                        }
                        if (bandera2)
                        {
                            trlblobservacion.Visible = true;
                            txtobservestado.Enabled = false;
                            trtxtobservacion.Visible = true;
                            btnduplicar.Visible = true;
                            btnEnviarIp.Visible = false;
                        }

                    }

                }

            }
            #endregion
        }

      

        private void CargarPrecioEditado(string p)
        {
            int i = 0;
            DataTable Tabla_fill = new DataTable();
            DataTable Tabla_fill1 = new DataTable();
            Tabla_fill = cmIp.ConsultarPrecio(Convert.ToInt64(p));
            if (Tabla_fill.Rows.Count != 0)
            {
                Tabla_fill.Columns["valor"].ReadOnly = false;
                Tabla_fill.Columns["margen"].ReadOnly = false;
                foreach (DataRow r in Tabla_fill.Rows)
                {
                    decimal valor = Convert.ToDecimal(r["valor"].ToString());
                    decimal margen = Convert.ToDecimal(r["margen"].ToString());
                    r["valor"] = valor.ToString("N2", new CultureInfo("en-US"));
                    r["margen"] = margen.ToString("N3", new CultureInfo("en-US"));

                }
                Tabla_fill.AcceptChanges();
                Session.Add("TbPrecio", Tabla_fill);
                grdPrecioIp.DataSource = Tabla_fill;
                grdPrecioIp.DataBind();
            }

            cmIp.CerrarConexion();

        }

        //SE CARGA LA TABLA IDIOA RELACIONADA CON LOS DATOS DEL ITEM PLANTA INSERTADO
        private void CargarIdiomaEditado(string item_planta)
        {
            int i = 0;
            DataTable Tabla_fill = new DataTable();
            DataTable Tabla_fill1 = new DataTable();
            DataTable Tabla_fill2 = new DataTable();
            Boolean agregar = true;
            Tabla_fill = cmIp.ConsultarIdioma(Convert.ToInt64(item_planta));
            Tabla_fill2 = cmIp.PoblarIdioma();
            int j = Tabla_fill.Rows.Count;
            foreach (DataRow r in Tabla_fill2.Rows)
            {
                for (int y = 0; y < j; y++)
                {

                    if (r["idioma_id"].ToString() == Tabla_fill.Rows[y]["idioma_id"].ToString())
                    {
                        agregar = false;

                    }
                }
                if (agregar)
                {
                    DataRow row = Tabla_fill.NewRow();

                    row["idioma_id"] = Convert.ToInt32(r["idioma_id"]);
                    Tabla_fill.Columns["planta_idioma_id"].ReadOnly = false;
                    row["planta_idioma_id"] = "";
                    row["idioma"] = r["idioma"].ToString();
                    row["descripcion"] = r["descripcion"].ToString();
                    Tabla_fill.Rows.Add(row);
                }
                agregar = true;
            }
            Tabla_fill.AcceptChanges();
            Session.Add("TabIdioma", Tabla_fill);
            Reload_tbIdioma();
            cmIp.CerrarConexion();
        }


        private Boolean InsertarItemPlanta(int origen, decimal peso_unitario, decimal factor_orden, decimal factor_adicional,            
                                           decimal peso_empaque, int cant_empaque, decimal largo, decimal ancho1, decimal ancho2, 
                                           decimal alto1, decimal alto2, bool tipo_kamban, bool disp_cotizacion, bool disp_comercial,
                                           bool disp_ingenieria, bool disp_almacen, bool disp_produccion, bool req_plano, bool req_tipo,
                                           bool req_modelo, int tipo_orden_prod_id,bool insp_Calidad,bool insp_Obligatoria, int ClsItm_Id)
        {
            bool result = false; string mensaje1; bool ctrl_piciz = false; string agrupador;
            string DescIp = buscarespeciales(txtDscIp.Text.ToUpper());
            string abreviada = buscarespeciales(txtDscAbrvIp.Text.ToUpper());
            //string cia = cmIp.ConsultarCia(Convert.ToInt32(cboPlantaIp.SelectedItem.Value));
            agrupador = cboAgrupadorIp.SelectedValue;
            string planta = cboPlantaIp.SelectedItem.Value;

            if (cboOrdenIp.Enabled == false && cboAdicionalIp.Enabled == true)
            {
                string und_orden = cboPrincipalIp.SelectedItem.Value;
                // se compara que el tipo de inventario es 0027 si lo es se debe insertar 2 criterios de clasificacion de CONTROLADOR PICIZ
                if (cboGrupimpIp.SelectedItem.Value.Equals("0027"))
                {
                    ctrl_piciz = true;
                }
                if (cboAgrupadorIp.SelectedValue.Equals(" "))
                {
                    agrupador = "0";
                }
                if (txtGruPlantaIpId.Text.Equals(""))
                {
                    txtGruPlantaIpId.Text = "0";
                }

                //insert en tabla item _planta

                string mensaje = cmIp.insertarItemPlanta(Convert.ToInt64(agrupador), Convert.ToInt32(cboPlantaIp.SelectedItem.Value), Convert.ToInt32(txtGruPlantaIpId.Text),
                                                         DescIp, false, abreviada, chkListusoIp.Items[0].Selected, chkListusoIp.Items[1].Selected, chkListusoIp.Items[2].Selected, 
                                                         origen, cboTipInvIp.SelectedItem.Value.Trim(), cboGrupimpIp.SelectedItem.Value.Trim(), cboPrincipalIp.SelectedItem.Value,
                                                         cboAdicionalIp.SelectedItem.Value.Trim(), und_orden.Trim(), peso_unitario, factor_orden, factor_adicional, 
                                                         Session["usuario"].ToString(), Convert.ToInt32(cboPerfilIp.SelectedItem.Value), ctrl_piciz, peso_empaque, 
                                                         cant_empaque, largo, ancho1, ancho2, alto1, alto2, tipo_kamban, disp_cotizacion, disp_comercial, disp_ingenieria,
                                                         disp_almacen, disp_produccion, req_plano, req_tipo, req_modelo, tipo_orden_prod_id,insp_Calidad,
                                                         insp_Obligatoria, Convert.ToInt32(cboClaseItem.SelectedValue), cbo_Bodega.SelectedItem.Value);
                //si se captura el id se proecede a insertar en la demas tablas relacionadas a item planta
                if (mensaje.Substring(0, 1) != "E")
                {
                    Session["item_planta_id"] = mensaje;
                    result = true;
                    //Insertar en bitacora de item_planta
                    mensaje1 = cmIp.insertarBitacoraItemPlanta(Convert.ToInt64(mensaje), Convert.ToInt64(agrupador), Convert.ToInt32(cboPlantaIp.SelectedItem.Value), Convert.ToInt32(txtGruPlantaIpId.Text),
                                                               DescIp, false, abreviada, chkListusoIp.Items[0].Selected, chkListusoIp.Items[1].Selected, chkListusoIp.Items[2].Selected, origen,
                                                               cboTipInvIp.SelectedItem.Value, cboGrupimpIp.SelectedItem.Value, cboPrincipalIp.SelectedItem.Value, cboAdicionalIp.SelectedItem.Value, 
                                                               und_orden, peso_unitario, factor_orden, factor_adicional, Session["usuario"].ToString(), Convert.ToInt32(cboPerfilIp.SelectedItem.Value), 
                                                               ctrl_piciz, peso_empaque, cant_empaque, largo, ancho1, ancho2, alto1, alto2, tipo_kamban, disp_cotizacion, disp_comercial, disp_ingenieria,
                                                               disp_almacen, disp_produccion, req_plano, req_tipo, req_modelo, tipo_orden_prod_id);
                    if (mensaje1.Equals("OK"))
                    { Debug.WriteLine("Se inserto con exito en la tabla bitacora_item_planta"); }
                    else { Debug.WriteLine(mensaje1); }
                    //Insertar en itemplanta_rel_estado
                    mensaje1 = cmIp.InsertaRelEstado(Convert.ToInt64(mensaje), Session["usuario"].ToString());
                    if (mensaje1.Equals("OK"))
                    { Debug.WriteLine("Se inserto con exito en la tabla itemplanta_rel_estado"); }
                    else { Debug.WriteLine(mensaje1); }
                    //Insertar en bitacora_itemplanta_rel_estado
                    mensaje1 = cmIp.InsertarBitacoraEstado(Convert.ToInt64(mensaje), 1, Session["usuario"].ToString(), "");
                    if (mensaje1.Equals("OK"))
                    { Debug.WriteLine("Se inserto con exito en la tabla bitacora_itemplanta_rel_estado"); }
                    else { Debug.WriteLine(mensaje1); }
                    //Se recorre la grilla de idioma
                    int a = grdIdiomaIp.PageIndex;
                    for (int i = 0; i < grdIdiomaIp.PageCount; i++)
                    {
                        grdIdiomaIp.SetPageIndex(i);
                        foreach (GridViewRow row in grdIdiomaIp.Rows)
                        {
                            TextBox txt_descripIdiom = ((TextBox)grdIdiomaIp.Rows[row.RowIndex].FindControl("textDesc"));
                            string descripcion = txt_descripIdiom.Text.ToUpper();
                            descripcion = buscarespeciales(descripcion);
                            Label lbl_idioma_id = ((Label)grdIdiomaIp.Rows[row.RowIndex].FindControl("lblidioma_id"));
                            string idioma_id = lbl_idioma_id.Text;
                            //Insertar en item_planta_idioma
                            mensaje1 = cmIp.InsertarItemPlantaIdioma(Convert.ToInt64(mensaje), Convert.ToInt32(idioma_id), descripcion, true);
                            if (mensaje1.Equals("OK"))
                            { Debug.WriteLine("Se inserto con exito en la tabla item_planta_idioma"); }
                            else { Debug.WriteLine(mensaje1); }
                            //Insertar en bitacora item_planta_idioma
                            mensaje1 = cmIp.insertarBitacoraItemPlantaIdioma(Convert.ToInt64(mensaje), Convert.ToInt32(idioma_id), descripcion, true, Session["usuario"].ToString());
                            if (mensaje1.Equals("OK"))
                            { Debug.WriteLine("Se inserto con exito en la tabla bitacora_item_planta_idioma"); }
                            else { Debug.WriteLine(mensaje1); }

                        }
                    }
                    grdIdiomaIp.SetPageIndex(a);
                    if (grdPrecioIp.Rows.Count != 0)
                    {
                        //Se recorre la grilla de precio
                        int b = grdPrecioIp.PageIndex;
                        for (int i = 0; i < grdPrecioIp.PageCount; i++)
                        {
                            grdPrecioIp.SetPageIndex(i);
                            foreach (GridViewRow row in grdPrecioIp.Rows)
                            {
                                Label labelmoneda_id = ((Label)grdPrecioIp.Rows[row.RowIndex].FindControl("lblmonedaid"));
                                string id_moneda = labelmoneda_id.Text;
                                Label lblcliente_tipo_planta_id = ((Label)grdPrecioIp.Rows[row.RowIndex].FindControl("lblcliente_tipo_planta_id"));
                                string cliente_tipo_planta_id = lblcliente_tipo_planta_id.Text;
                                TextBox lblvalor = ((TextBox)grdPrecioIp.Rows[row.RowIndex].FindControl("txt_valor"));
                                string valor = lblvalor.Text;
                                Label lblcliente_tipo_planta_Costo = ((Label)grdPrecioIp.Rows[row.RowIndex].FindControl("lblcliente_tipo_planta_Costo"));
                                string Costo = lblcliente_tipo_planta_Costo.Text;
                                Label lblcliente_tipo_planta_TRM = ((Label)grdPrecioIp.Rows[row.RowIndex].FindControl("lblcliente_tipo_planta_TRM"));
                                string TRM = lblcliente_tipo_planta_TRM.Text;
                                string margen = ((TextBox)grdPrecioIp.Rows[row.RowIndex].FindControl("txtporcentaje")).Text;
                                if (!valor.Equals("") && !margen.Equals(""))
                                {
                                    //Insertar en item_planta_precio
                                    mensaje1 = cmIp.InsertarItemPlantaPrecio(Convert.ToInt64(mensaje), Convert.ToInt32(id_moneda), Convert.ToInt32(cliente_tipo_planta_id), Convert.ToDecimal(valor), true, Convert.ToDecimal(margen), Convert.ToDecimal(Costo), Convert.ToDecimal(TRM), Session["usuario"].ToString());
                                    if (mensaje1.Equals("OK"))
                                    { Debug.WriteLine("Se inserto con exito en la tabla item_planta_precio"); }
                                    else { Debug.WriteLine(mensaje1); }
                                    //Insertar en bitacora_item_planta_precio
                                    mensaje1 = cmIp.insertarBitacoraItemPlantaPrecio(Convert.ToInt64(mensaje), Convert.ToInt32(id_moneda), Convert.ToInt32(cliente_tipo_planta_id), Convert.ToDecimal(valor), true, Session["usuario"].ToString(), Convert.ToDecimal(margen), Convert.ToDecimal(Costo), Convert.ToDecimal(TRM));
                                    if (mensaje1.Equals("OK"))
                                    { Debug.WriteLine("Se inserto con exito en la tabla bitacora_item_planta_precio"); }
                                    else { Debug.WriteLine(mensaje1); }
                                }


                            }
                        }
                        grdPrecioIp.SetPageIndex(b);
                    }

                    //Insertar en tabla item_planta_parametro
                    DataTable dt = Session["Tb_Parametros"] as DataTable;

                    if (dt.Rows.Count != 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            mensaje1 = cmIp.InsertarItemParametro(Convert.ToInt64(mensaje), Convert.ToInt32(dt.Rows[i]["item_tipo_parametro_id"].ToString()));
                            if (mensaje1 == "OK")
                            { Debug.WriteLine("Se inserto con exito en la tabla item_parametro"); }
                            mensaje1 = cmIp.BitacoraItemParametro(Convert.ToInt64(mensaje), Convert.ToInt32(dt.Rows[i]["item_tipo_parametro_id"].ToString()), Session["usuario"].ToString(), true);
                            if (mensaje1 == "OK")
                            { Debug.WriteLine("Se inserto con exito en la tabla bitacora_item_planta_parametro"); }
                        }
                    }

                    //Insertar en tabla item_rel_criterio_iplanta
                    if (cboPlan1Ip.SelectedItem.Value != " " || cboPlan5Ip.SelectedItem.Value != " ")
                    {
                        mensaje1 = cmIp.InsertarRelCriterio(Convert.ToInt64(mensaje), cboPlan1Ip.SelectedItem.Value, cboPlan2Ip.SelectedItem.Value, cboPlan3Ip.SelectedItem.Value, txtPosAranIp.Text, true, cboPlan4Ip.SelectedItem.Value, cboPlan5Ip.SelectedItem.Value);
                        if (mensaje1.Equals("OK"))
                        { Debug.WriteLine("Se inserto con exito en la tabla item_rel_criterio_iplanta"); }
                        else { Debug.WriteLine(mensaje1); }
                        //Insertar en tabla bitacora item_rel_criterio_iplanta
                        mensaje1 = cmIp.insertarBitacoraRelCriterio(Convert.ToInt64(mensaje), cboPlan1Ip.SelectedItem.Value, cboPlan2Ip.SelectedItem.Value, cboPlan3Ip.SelectedItem.Value, txtPosAranIp.Text, true, Session["usuario"].ToString(), cboPlan4Ip.SelectedItem.Value, cboPlan5Ip.SelectedItem.Value);
                        if (mensaje1.Equals("OK"))
                        { Debug.WriteLine("Se inserto con exito en la tabla bitacora_criterio_iplanta"); }
                        else { Debug.WriteLine(mensaje1); }
                    }
                    btnEnviarIp.Visible = true;
                    btnEnviarIp.Enabled = true;

                }
                else
                {
                    mensajeVentana(mensaje);
                }
            }
            else
            {
                if (cboGrupimpIp.SelectedItem.Value.Equals("0027"))
                {
                    ctrl_piciz = true;
                }
                if (cboAgrupadorIp.SelectedValue.Equals(" "))
                {
                    agrupador = "0";
                }
                if (txtGruPlantaIpId.Text.Equals(""))
                {
                    txtGruPlantaIpId.Text = "0";
                }

                //insert en tabla item_planta
                string mensaje = cmIp.insertarItemPlanta(Convert.ToInt64(agrupador), Convert.ToInt32(cboPlantaIp.SelectedItem.Value), Convert.ToInt32(txtGruPlantaIpId.Text),
                                                         DescIp, false, abreviada, chkListusoIp.Items[0].Selected, chkListusoIp.Items[1].Selected, chkListusoIp.Items[2].Selected,
                                                         origen, cboTipInvIp.SelectedItem.Value.Trim(), cboGrupimpIp.SelectedItem.Value.Trim(), cboPrincipalIp.SelectedItem.Value,
                                                         cboAdicionalIp.SelectedItem.Value.Trim(), cboOrdenIp.SelectedItem.Value.Trim(), peso_unitario, factor_orden,
                                                         factor_adicional, Session["usuario"].ToString(), Convert.ToInt32(cboPerfilIp.SelectedItem.Value), ctrl_piciz,
                                                         peso_empaque, cant_empaque, largo, ancho1, ancho2, alto1, alto2, tipo_kamban, disp_cotizacion, disp_comercial, 
                                                         disp_ingenieria, disp_almacen, disp_produccion, req_plano, req_tipo, req_modelo, tipo_orden_prod_id, insp_Calidad,
                                                         insp_Obligatoria, Convert.ToInt32(cboClaseItem.SelectedValue),cbo_Bodega.SelectedItem.Value);
                //si se captura el id se proecede a insertar en la demas tablas relacionadas a item planta
                if (mensaje.Substring(0, 1) != "E")
                {
                    Session.Add("item_planta_id", mensaje);
                    result = true;
                    //Insertar en bitacora de item_planta
                    mensaje1 = cmIp.insertarBitacoraItemPlanta(Convert.ToInt64(mensaje), Convert.ToInt64(agrupador), Convert.ToInt32(cboPlantaIp.SelectedItem.Value), Convert.ToInt32(txtGruPlantaIpId.Text), DescIp,
                                                               false, abreviada, chkListusoIp.Items[0].Selected, chkListusoIp.Items[1].Selected, chkListusoIp.Items[2].Selected, origen, cboTipInvIp.SelectedItem.Value,
                                                               cboGrupimpIp.SelectedItem.Value, cboPrincipalIp.SelectedItem.Value, cboAdicionalIp.SelectedItem.Value, cboOrdenIp.SelectedItem.Value, peso_unitario, 
                                                               factor_orden, factor_adicional, Session["usuario"].ToString(), Convert.ToInt32(cboPerfilIp.SelectedItem.Value), ctrl_piciz, peso_empaque, cant_empaque,
                                                               largo, ancho1, ancho2, alto1, alto2, tipo_kamban, disp_cotizacion, disp_comercial, disp_ingenieria, disp_almacen, disp_produccion, req_plano,
                                                               req_tipo, req_modelo, tipo_orden_prod_id);
                    if (mensaje1.Equals("OK"))
                    { Debug.WriteLine("Se inserto con exito en la tabla bitacora_item_planta"); }
                    else { Debug.WriteLine(mensaje1); }
                    //Insertar en itemplanta_rel_estado
                    mensaje1 = cmIp.InsertaRelEstado(Convert.ToInt64(mensaje), Session["usuario"].ToString());
                    if (mensaje1.Equals("OK"))
                    { Debug.WriteLine("Se inserto con exito en la tabla itemplanta_rel_estado"); }
                    else { Debug.WriteLine(mensaje1); }
                    //Insertar en bitacora_itemplanta_rel_estado
                    mensaje1 = cmIp.InsertarBitacoraEstado(Convert.ToInt64(mensaje), 1, Session["usuario"].ToString(), "");
                    if (mensaje1.Equals("OK"))
                    { Debug.WriteLine("Se inserto con exito en la tabla bitacora_itemplanta_rel_estado"); }
                    else { Debug.WriteLine(mensaje1); }
                    //Se recorre la grilla de idioma
                    int a = grdIdiomaIp.PageIndex;
                    for (int i = 0; i < grdIdiomaIp.PageCount; i++)
                    {
                        grdIdiomaIp.SetPageIndex(i);
                        foreach (GridViewRow row in grdIdiomaIp.Rows)
                        {
                            TextBox txt_descripIdiom = ((TextBox)grdIdiomaIp.Rows[row.RowIndex].FindControl("textDesc"));
                            string descripcion = txt_descripIdiom.Text.ToUpper();
                            descripcion = buscarespeciales(descripcion);
                            Label lbl_idioma_id = ((Label)grdIdiomaIp.Rows[row.RowIndex].FindControl("lblidioma_id"));
                            string idioma_id = lbl_idioma_id.Text;
                            //Insertar en item_planta_idioma
                            mensaje1 = cmIp.InsertarItemPlantaIdioma(Convert.ToInt64(mensaje), Convert.ToInt32(idioma_id), descripcion, true);
                            if (mensaje1.Equals("OK"))
                            { Debug.WriteLine("Se inserto con exito en la tabla item_planta_idioma"); }
                            else { Debug.WriteLine(mensaje1); }
                            //Insertar en bitacora item_planta_idioma
                            mensaje1 = cmIp.insertarBitacoraItemPlantaIdioma(Convert.ToInt64(mensaje), Convert.ToInt32(idioma_id), descripcion, true, Session["usuario"].ToString());
                            if (mensaje1.Equals("OK"))
                            { Debug.WriteLine("Se inserto con exito en la tabla bitacora_item_planta_idioma"); }
                            else { Debug.WriteLine(mensaje1); }

                        }
                    }
                    grdIdiomaIp.SetPageIndex(a);
                    if (grdPrecioIp.Rows.Count != 0)
                    {
                        //Se recorre la grilla de precio
                        int b = grdPrecioIp.PageIndex;
                        for (int i = 0; i < grdPrecioIp.PageCount; i++)
                        {
                            grdPrecioIp.SetPageIndex(i);
                            foreach (GridViewRow row in grdPrecioIp.Rows)
                            {
                                Label labelmoneda_id = ((Label)grdPrecioIp.Rows[row.RowIndex].FindControl("lblmonedaid"));
                                string id_moneda = labelmoneda_id.Text;
                                Label lblcliente_tipo_id = ((Label)grdPrecioIp.Rows[row.RowIndex].FindControl("lblcliente_tipo_planta_id"));
                                string cliente_tipo_id = lblcliente_tipo_id.Text;
                                TextBox lblvalor = ((TextBox)grdPrecioIp.Rows[row.RowIndex].FindControl("txt_valor"));
                                string valor = lblvalor.Text;
                                Label lblcliente_tipo_planta_Costo = ((Label)grdPrecioIp.Rows[row.RowIndex].FindControl("lblcliente_tipo_planta_Costo"));
                                string Costo = lblcliente_tipo_planta_Costo.Text;
                                Label lblcliente_tipo_planta_TRM = ((Label)grdPrecioIp.Rows[row.RowIndex].FindControl("lblcliente_tipo_planta_TRM"));
                                string TRM = lblcliente_tipo_planta_TRM.Text;

                                string margen = ((TextBox)grdPrecioIp.Rows[row.RowIndex].FindControl("txtporcentaje")).Text;
                                if (!valor.Equals("") && !margen.Equals(""))
                                {
                                    //Insertar en item_planta_precio
                                    mensaje1 = cmIp.InsertarItemPlantaPrecio(Convert.ToInt64(mensaje), Convert.ToInt32(id_moneda), Convert.ToInt32(cliente_tipo_id), Convert.ToDecimal(valor), true, Convert.ToDecimal(margen), Convert.ToDecimal(Costo), Convert.ToDecimal(TRM), Session["usuario"].ToString());
                                    if (mensaje1.Equals("OK"))
                                    { Debug.WriteLine("Se inserto con exito en la tabla item_planta_precio"); }
                                    else { Debug.WriteLine(mensaje1); }
                                    //Insertar en bitacora_item_planta_precio
                                    mensaje1 = cmIp.insertarBitacoraItemPlantaPrecio(Convert.ToInt64(mensaje), Convert.ToInt32(id_moneda), Convert.ToInt32(cliente_tipo_id), Convert.ToDecimal(valor), true, Session["usuario"].ToString(), Convert.ToDecimal(margen), Convert.ToDecimal(Costo), Convert.ToDecimal(TRM));
                                    if (mensaje1.Equals("OK"))
                                    { Debug.WriteLine("Se inserto con exito en la tabla bitacora_item_planta_precio"); }
                                    else { Debug.WriteLine(mensaje1); }
                                }

                            }
                        }
                        grdPrecioIp.SetPageIndex(b);
                    }
                    //Insertar en tabla item_planta_parametro
                    DataTable dt = Session["Tb_Parametros"] as DataTable;

                    if (dt.Rows.Count != 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            mensaje1 = cmIp.InsertarItemParametro(Convert.ToInt64(mensaje), Convert.ToInt32(dt.Rows[i]["item_tipo_parametro_id"].ToString()));
                            if (mensaje1 == "OK")
                            { Debug.WriteLine("Se inserto con exito en la tabla item_parametro"); }
                            mensaje1 = cmIp.BitacoraItemParametro(Convert.ToInt64(mensaje), Convert.ToInt32(dt.Rows[i]["item_tipo_parametro_id"].ToString()), Session["usuario"].ToString(), true);
                            if (mensaje1 == "OK")
                            { Debug.WriteLine("Se inserto con exito en la tabla bitacora_item_planta_parametro"); }
                        }
                    }
                    //Insertar en tabla item_rel_criterio_iplanta
                    if (cboPlan1Ip.SelectedItem.Value != " ")
                    {
                        mensaje1 = cmIp.InsertarRelCriterio(Convert.ToInt64(mensaje), cboPlan1Ip.SelectedItem.Value, cboPlan2Ip.SelectedItem.Value, cboPlan3Ip.SelectedItem.Value, txtPosAranIp.Text, true, cboPlan4Ip.SelectedItem.Value, cboPlan5Ip.SelectedItem.Value);
                        if (mensaje1.Equals("OK"))
                        { Debug.WriteLine("Se inserto con exito en la tabla item_rel_criterio_iplanta"); }
                        else { Debug.WriteLine(mensaje1); }
                        //Insertar en tabla bitacora item_rel_criterio_iplanta
                        mensaje1 = cmIp.insertarBitacoraRelCriterio(Convert.ToInt64(mensaje), cboPlan1Ip.SelectedItem.Value, cboPlan2Ip.SelectedItem.Value, cboPlan3Ip.SelectedItem.Value, txtPosAranIp.Text, true, Session["usuario"].ToString(), cboPlan4Ip.SelectedItem.Value, cboPlan5Ip.SelectedItem.Value);
                        if (mensaje1.Equals("OK"))
                        { Debug.WriteLine("Se inserto con exito en la tabla bitacora_criterio_iplanta"); }
                        else { Debug.WriteLine(mensaje1); }
                    }

                }
                else
                {
                    mensajeVentana(mensaje);
                }
            }
            cmIp.CerrarConexion();
            return result;
        }


        private Boolean ActualizarItemPlanta(Int64 item_planta, int origen, decimal peso_unitario, decimal factor_orden, decimal factor_adicional, 
                                           decimal peso_empaque, int cant_empaque, decimal largo, decimal ancho1, decimal ancho2, decimal alto1,
                                           decimal alto2, bool tipo_kamban, bool disp_cotizacion, bool disp_comercial, bool disp_ingenieria,
                                           bool disp_almacen, bool disp_produccion, bool req_plano, bool req_tipo, bool req_modelo, int tipo_orden_prod_id,
                                           bool item_InspCal, bool item_InspObligatoria, int ClsItm_Id)
        {
            bool activo = false;
            if (lblactivoIp.Text.Equals("Activo"))
            {
                activo = true;
            }
            bool result = false; string mensaje1; bool ctrl_piciz = false; string agrupador;
            string DescIp = buscarespeciales(txtDscIp.Text.ToUpper());
            string abreviada = buscarespeciales(txtDscAbrvIp.Text.ToUpper());
            //string cia = cmIp.ConsultarCia(Convert.ToInt32(cboPlantaIp.SelectedItem.Value));
            agrupador = cboAgrupadorIp.SelectedValue;
            if (cboOrdenIp.Enabled == false && cboAdicionalIp.Enabled == true)
            {
                string und_orden = cboPrincipalIp.SelectedItem.Value;
                // se compara que el tipo de inventario es 0027 si lo es se debe insertar 2 criterios de clasificacion de CONTROLADOR PICIZ
                if (cboGrupimpIp.SelectedItem.Value.Equals("0027"))
                {
                    ctrl_piciz = true;
                }
                if (cboAgrupadorIp.SelectedValue.Equals(" "))
                {
                    agrupador = "0";
                }
                if (txtGruPlantaIpId.Text.Equals(""))
                {
                    txtGruPlantaIpId.Text = "0";
                }

                //insert en tabla item _planta
                string mensaje = cmIp.ActualizarItemPlanta(item_planta, Convert.ToInt64(agrupador), Convert.ToInt32(cboPlantaIp.SelectedItem.Value), Convert.ToInt32(txtGruPlantaIpId.Text),
                                                           DescIp, abreviada, chkListusoIp.Items[0].Selected, chkListusoIp.Items[1].Selected, chkListusoIp.Items[2].Selected, origen,
                                                           cboTipInvIp.SelectedItem.Value, cboGrupimpIp.SelectedItem.Value, cboPrincipalIp.SelectedItem.Value, cboAdicionalIp.SelectedItem.Value,
                                                           und_orden, peso_unitario, factor_orden, factor_adicional, ctrl_piciz, peso_empaque, cant_empaque, largo, ancho1, ancho2, alto1,
                                                           alto2, tipo_kamban, disp_cotizacion, disp_comercial, disp_ingenieria, disp_almacen, disp_produccion, req_plano, req_tipo, req_modelo,
                                                           tipo_orden_prod_id, item_InspCal, item_InspObligatoria, Convert.ToInt32(cboClaseItem.SelectedValue), cbo_Bodega.SelectedItem.Value);
                //si se captura el id se proecede a insertar en la demas tablas relacionadas a item planta
                if (mensaje.Equals("OK"))
                {

                    result = true;
                    //Insertar en bitacora itemplanta_
                    mensaje1 = cmIp.insertarBitacoraItemPlanta(item_planta, Convert.ToInt64(agrupador), Convert.ToInt32(cboPlantaIp.SelectedItem.Value), Convert.ToInt32(txtGruPlantaIpId.Text), DescIp,
                                                               activo, abreviada, chkListusoIp.Items[0].Selected, chkListusoIp.Items[1].Selected, chkListusoIp.Items[2].Selected, origen, cboTipInvIp.SelectedItem.Value,
                                                               cboGrupimpIp.SelectedItem.Value, cboPrincipalIp.SelectedItem.Value, cboAdicionalIp.SelectedItem.Value, und_orden, peso_unitario, factor_orden, factor_adicional,
                                                               Session["usuario"].ToString(), Convert.ToInt32(cboPerfilIp.SelectedItem.Value), ctrl_piciz, peso_empaque, cant_empaque, largo, ancho1, ancho2, alto1, alto2,
                                                               tipo_kamban, disp_cotizacion, disp_comercial, disp_ingenieria, disp_almacen, disp_produccion, req_plano, req_tipo, req_modelo, tipo_orden_prod_id);
                    if (mensaje1.Equals("OK"))
                    { Debug.WriteLine("Se inserto con exito en la tabla bitacora_item_planta"); }
                    else { Debug.WriteLine(mensaje1); }
                    //Insertar en bitacora_itemplanta_rel_estado
                    mensaje1 = cmIp.InsertarBitacoraEstado(item_planta, 6, Session["usuario"].ToString(), "");
                    if (mensaje1.Equals("OK"))
                    { Debug.WriteLine("Se inserto con exito en la tabla bitacora_itemplanta_rel_estado"); }
                    else { Debug.WriteLine(mensaje1); }
                    //Se recorre la grilla de idioma
                    int a = grdIdiomaIp.PageIndex;
                    for (int i = 0; i < grdIdiomaIp.PageCount; i++)
                    {
                        grdIdiomaIp.SetPageIndex(i);
                        foreach (GridViewRow row in grdIdiomaIp.Rows)
                        {
                            TextBox txt_descripIdiom = ((TextBox)grdIdiomaIp.Rows[row.RowIndex].FindControl("textDesc"));
                            string desc_idioma = txt_descripIdiom.Text.ToUpper();
                            desc_idioma = buscarespeciales(desc_idioma);
                            Label lbl_idioma_id = ((Label)grdIdiomaIp.Rows[row.RowIndex].FindControl("lblidioma_id"));
                            string idioma_id = lbl_idioma_id.Text;
                            Label lbl_planta_id = ((Label)grdIdiomaIp.Rows[row.RowIndex].FindControl("lblplanta_idioma_id"));
                            string planta_idioma = lbl_planta_id.Text;
                            if (planta_idioma != "")
                            {
                                //actualizar en item_planta_idioma
                                mensaje1 = cmIp.ActualizarItemPlantaIdioma(Convert.ToInt64(planta_idioma), desc_idioma, true);
                                if (mensaje1.Equals("OK"))
                                { Debug.WriteLine("Se actualizo con exito en la tabla item_planta_idioma"); }
                                else { Debug.WriteLine(mensaje1); }
                            }
                            else
                            {
                                //Insertar en item_planta_idioma
                                mensaje1 = cmIp.InsertarItemPlantaIdioma(item_planta, Convert.ToInt32(idioma_id), desc_idioma, true);
                                if (mensaje1.Equals("OK"))
                                { Debug.WriteLine("Se inserto con exito en la tabla item_planta_idioma"); }
                                else { Debug.WriteLine(mensaje1); }
                            }
                            //Insertar en bitacora item_planta_idioma
                            mensaje1 = cmIp.insertarBitacoraItemPlantaIdioma(item_planta, Convert.ToInt32(idioma_id), desc_idioma, true, Session["usuario"].ToString());
                            if (mensaje1.Equals("OK"))
                            { Debug.WriteLine("Se inserto con exito en la tabla bitacora_item_planta_idioma"); }
                            else { Debug.WriteLine(mensaje1); }
                        }
                    }
                    grdIdiomaIp.SetPageIndex(a);
                    if (grdPrecioIp.Rows.Count != 0)
                    {
                        //Se recorre la grilla de precio
                        int b = grdPrecioIp.PageIndex;
                        for (int i = 0; i < grdPrecioIp.PageCount; i++)
                        {
                            grdPrecioIp.SetPageIndex(i);
                            foreach (GridViewRow row in grdPrecioIp.Rows)
                            {
                                Label labelmoneda_id = ((Label)grdPrecioIp.Rows[row.RowIndex].FindControl("lblmonedaid"));
                                string id_moneda = labelmoneda_id.Text;
                                Label lblcliente_tipo_planta_id = ((Label)grdPrecioIp.Rows[row.RowIndex].FindControl("lblcliente_tipo_planta_id"));
                                string cliente_tipo_planta_id = lblcliente_tipo_planta_id.Text;
                                TextBox lblvalor = ((TextBox)grdPrecioIp.Rows[row.RowIndex].FindControl("txt_valor"));
                                string valor = lblvalor.Text;
                                Label lbl_precio_id = ((Label)grdPrecioIp.Rows[row.RowIndex].FindControl("lblplantaprecio_id"));
                                string planta_precio = lbl_precio_id.Text;
                                Label lblcliente_tipo_planta_Costo = ((Label)grdPrecioIp.Rows[row.RowIndex].FindControl("lblcliente_tipo_planta_Costo"));
                                string Costo = lblcliente_tipo_planta_Costo.Text;
                                Label lblcliente_tipo_planta_TRM = ((Label)grdPrecioIp.Rows[row.RowIndex].FindControl("lblcliente_tipo_planta_TRM"));
                                string TRM = lblcliente_tipo_planta_TRM.Text;
                                string margen = ((TextBox)grdPrecioIp.Rows[row.RowIndex].FindControl("txtporcentaje")).Text;
                                if (!valor.Equals("") && !margen.Equals(""))
                                {
                                    if (planta_precio != "")
                                    {
                                        //actualizar en item_planta_precio
                                        mensaje1 = cmIp.ActualizarItemPlantaPrecio(Convert.ToInt64(planta_precio), true, Convert.ToDecimal(valor), Convert.ToDecimal(margen), Convert.ToDecimal(Costo), Convert.ToDecimal(TRM));
                                        if (mensaje1.Equals("OK"))
                                        { Debug.WriteLine("Se actualizo con exito en la tabla item_planta_precio"); }
                                        else { Debug.WriteLine(mensaje1); }
                                    }
                                    else
                                    {
                                        //Insertar en item_planta_precio
                                        mensaje1 = cmIp.InsertarItemPlantaPrecio(item_planta, Convert.ToInt32(id_moneda), Convert.ToInt32(cliente_tipo_planta_id), Convert.ToDecimal(valor), true, Convert.ToDecimal(margen), Convert.ToDecimal(Costo), Convert.ToDecimal(TRM), Session["usuario"].ToString());
                                        if (mensaje1.Equals("OK"))
                                        { Debug.WriteLine("Se inserto con exito en la tabla item_planta_precio"); }
                                        else { Debug.WriteLine(mensaje1); }
                                    }
                                    //Insertar en bitacora_item_planta_precio
                                    mensaje1 = cmIp.insertarBitacoraItemPlantaPrecio(item_planta, Convert.ToInt32(id_moneda), Convert.ToInt32(cliente_tipo_planta_id), Convert.ToDecimal(valor), true, Session["usuario"].ToString(), Convert.ToDecimal(margen), Convert.ToDecimal(Costo), Convert.ToDecimal(TRM));
                                    if (mensaje1.Equals("OK"))
                                    { Debug.WriteLine("Se inserto con exito en la tabla bitacora_item_planta_precio"); }
                                    else { Debug.WriteLine(mensaje1); }
                                }

                            }
                        }
                        grdPrecioIp.SetPageIndex(b);
                    }
                    //editar tabla de parametros
                    DataTable dt = Session["Tb_Parametros"] as DataTable;
                    DataTable dtelim = Session["Tb_Parametros_Delete"] as DataTable;
                    if (dt.Rows.Count != 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["item_parametro_id"].ToString().Equals(""))
                            {
                                mensaje1 = cmIp.InsertarItemParametro(item_planta, Convert.ToInt32(dt.Rows[i]["item_tipo_parametro_id"].ToString()));
                                if (mensaje1 == "OK")
                                { Debug.WriteLine("Se inserto con exito en la tabla item_parametro"); }
                                mensaje1 = cmIp.BitacoraItemParametro(item_planta, Convert.ToInt32(dt.Rows[i]["item_tipo_parametro_id"].ToString()), Session["usuario"].ToString(), true);
                                if (mensaje1 == "OK")
                                { Debug.WriteLine("Se inserto con exito en la tabla bitacora_item_parametro"); }
                            }
                        }
                    }
                    for (int i = 0; i < dtelim.Rows.Count; i++)
                    {
                        mensaje1 = cmIp.editarItemParametroActivo(dtelim.Rows[i]["idborrarparametro"].ToString());
                        if (mensaje1 == "OK")
                        { Debug.WriteLine("Se actualizo con exito en la tabla item_planta_parametro"); }
                    }

                    //PRECIOS ELIMINADOS
                    DataTable dt_eliminados = Session["eliminados_precio"] as DataTable;
                    if (dt_eliminados.Rows.Count != 0)
                    {
                        for (int j = dt_eliminados.Rows.Count - 1; j >= 0; j--)
                        {
                            DataRow dr = dt_eliminados.Rows[j];
                            mensaje1 = cmIp.ActualizarestadoIpPrecio(Convert.ToInt64(dr["item1"].ToString()), false);
                            if (mensaje1.Equals("OK"))
                            { Debug.WriteLine("Se actualizo con exito en la tabla item_planta_precio"); }
                            else { Debug.WriteLine(mensaje1); }
                        }
                    }
                    //Actualizar en tabla item_rel_criterio_iplanta
                    if (cboPlan1Ip.SelectedItem.Value != " ")
                    {
                        Int64 criterio = cmIp.Consultarcriterio(item_planta);

                        if (!criterio.Equals(0))
                        {
                            mensaje1 = cmIp.ActualizarRelCriterio(item_planta, cboPlan1Ip.SelectedItem.Value, cboPlan2Ip.SelectedItem.Value, cboPlan3Ip.SelectedItem.Value, cboPlan4Ip.SelectedItem.Value, cboPlan5Ip.SelectedItem.Value, txtPosAranIp.Text, true);
                            if (mensaje1.Equals("OK"))
                            { Debug.WriteLine("Se actualizo con exito en la tabla item_rel_criterio_iplanta"); }
                            else { Debug.WriteLine(mensaje1); }
                        }
                        else
                        {
                            mensaje1 = cmIp.InsertarRelCriterio(item_planta, cboPlan1Ip.SelectedItem.Value, cboPlan2Ip.SelectedItem.Value, cboPlan3Ip.SelectedItem.Value, txtPosAranIp.Text, true, cboPlan4Ip.SelectedItem.Value, cboPlan5Ip.SelectedItem.Value);
                            if (mensaje1.Equals("OK"))
                            { Debug.WriteLine("Se inserto con exito en la tabla item_rel_criterio_iplanta"); }
                            else { Debug.WriteLine(mensaje1); }
                        }
                        //Insertar en tabla bitacora item_rel_criterio_iplanta
                        mensaje1 = cmIp.insertarBitacoraRelCriterio(item_planta, cboPlan1Ip.SelectedItem.Value, cboPlan2Ip.SelectedItem.Value, cboPlan3Ip.SelectedItem.Value, txtPosAranIp.Text, true, Session["usuario"].ToString(), cboPlan4Ip.SelectedItem.Value, cboPlan5Ip.SelectedItem.Value);
                        if (mensaje1.Equals("OK"))
                        { Debug.WriteLine("Se inserto con exito en la tabla bitacora_criterio_iplanta"); }
                        else { Debug.WriteLine(mensaje1); }
                    }
                }
                else
                {
                    mensajeVentana(mensaje);
                }
            }
            else
            {
                if (cboGrupimpIp.SelectedItem.Value.Equals("0027"))
                {
                    ctrl_piciz = true;
                }
                if (cboAgrupadorIp.SelectedValue.Equals(" "))
                {
                    agrupador = "0";
                }
                if (txtGruPlantaIpId.Text.Equals(""))
                {
                    txtGruPlantaIpId.Text = "0";
                }

                //insert en tabla item _planta
                string mensaje = cmIp.ActualizarItemPlanta(item_planta, Convert.ToInt64(agrupador), Convert.ToInt32(cboPlantaIp.SelectedItem.Value), Convert.ToInt32(txtGruPlantaIpId.Text), DescIp, abreviada,
                                                           chkListusoIp.Items[0].Selected, chkListusoIp.Items[1].Selected, chkListusoIp.Items[2].Selected, origen, cboTipInvIp.SelectedItem.Value,
                                                           cboGrupimpIp.SelectedItem.Value, cboPrincipalIp.SelectedItem.Value, cboAdicionalIp.SelectedItem.Value, cboOrdenIp.SelectedItem.Value, peso_unitario,
                                                           factor_orden, factor_adicional, ctrl_piciz, peso_empaque, cant_empaque, largo, ancho1, ancho2, alto1, alto2, tipo_kamban, disp_cotizacion, disp_comercial,
                                                           disp_ingenieria, disp_almacen, disp_produccion, req_plano, req_tipo, req_modelo, tipo_orden_prod_id, item_InspCal, item_InspObligatoria,
                                                           Convert.ToInt32(cboClaseItem.SelectedValue), cbo_Bodega.SelectedItem.Value);
                //si se captura el id se proecede a insertar en la demas tablas relacionadas a item planta
                if (mensaje.Equals("OK"))
                {
                    result = true;
                    //Insertar en bitacora itemplanta_
                    mensaje1 = cmIp.insertarBitacoraItemPlanta(item_planta, Convert.ToInt64(agrupador), Convert.ToInt32(cboPlantaIp.SelectedItem.Value), Convert.ToInt32(txtGruPlantaIpId.Text), DescIp, activo, abreviada,
                                                               chkListusoIp.Items[0].Selected, chkListusoIp.Items[1].Selected, chkListusoIp.Items[2].Selected, origen, cboTipInvIp.SelectedItem.Value, cboGrupimpIp.SelectedItem.Value,
                                                               cboPrincipalIp.SelectedItem.Value, cboAdicionalIp.SelectedItem.Value, cboOrdenIp.SelectedItem.Value, peso_unitario, factor_orden, factor_adicional,
                                                               Session["usuario"].ToString(), Convert.ToInt32(cboPerfilIp.SelectedItem.Value), ctrl_piciz, peso_empaque, cant_empaque, largo, ancho1, ancho2, alto1,
                                                               alto2, tipo_kamban, disp_cotizacion, disp_comercial, disp_ingenieria, disp_almacen, disp_produccion, req_plano, req_tipo, req_modelo, tipo_orden_prod_id);
                    if (mensaje1.Equals("OK"))
                    { Debug.WriteLine("Se inserto con exito en la tabla bitacora_item_planta"); }
                    else { Debug.WriteLine(mensaje1); }
                    //Insertar en bitacora_itemplanta_rel_estado
                    mensaje1 = cmIp.InsertarBitacoraEstado(item_planta, 6, Session["usuario"].ToString(), "");
                    if (mensaje1.Equals("OK"))
                    { Debug.WriteLine("Se inserto con exito en la tabla bitacora_itemplanta_rel_estado"); }
                    else { Debug.WriteLine(mensaje1); }
                    //Se recorre la grilla de idioma
                    int a = grdIdiomaIp.PageIndex;
                    for (int i = 0; i < grdIdiomaIp.PageCount; i++)
                    {
                        grdIdiomaIp.SetPageIndex(i);
                        foreach (GridViewRow row in grdIdiomaIp.Rows)
                        {
                            TextBox txt_descripIdiom = ((TextBox)grdIdiomaIp.Rows[row.RowIndex].FindControl("textDesc"));
                            string desc_idioma = txt_descripIdiom.Text.ToUpper();
                            desc_idioma = buscarespeciales(desc_idioma);
                            Label lbl_idioma_id = ((Label)grdIdiomaIp.Rows[row.RowIndex].FindControl("lblidioma_id"));
                            string idioma_id = lbl_idioma_id.Text;
                            Label lbl_planta_id = ((Label)grdIdiomaIp.Rows[row.RowIndex].FindControl("lblplanta_idioma_id"));
                            string planta_idioma = lbl_planta_id.Text;
                            if (planta_idioma != "")
                            {
                                //actualizar en item_planta_idioma
                                mensaje1 = cmIp.ActualizarItemPlantaIdioma(Convert.ToInt64(planta_idioma), desc_idioma, true);
                                if (mensaje1.Equals("OK"))
                                { Debug.WriteLine("Se actualizo con exito en la tabla item_planta_idioma"); }
                                else { Debug.WriteLine(mensaje1); }
                            }
                            else
                            {
                                //Insertar en item_planta_idioma
                                mensaje1 = cmIp.InsertarItemPlantaIdioma(item_planta, Convert.ToInt32(idioma_id), desc_idioma, true);
                                if (mensaje1.Equals("OK"))
                                { Debug.WriteLine("Se inserto con exito en la tabla item_planta_idioma"); }
                                else { Debug.WriteLine(mensaje1); }
                            }
                            //Insertar en bitacora item_planta_idioma
                            mensaje1 = cmIp.insertarBitacoraItemPlantaIdioma(item_planta, Convert.ToInt32(idioma_id), desc_idioma, true, Session["usuario"].ToString());
                            if (mensaje1.Equals("OK"))
                            { Debug.WriteLine("Se inserto con exito en la tabla bitacora_item_planta_idioma"); }
                            else { Debug.WriteLine(mensaje1); }
                        }
                    }
                    grdIdiomaIp.SetPageIndex(a);
                    if (grdPrecioIp.Rows.Count != 0)
                    {
                        //Se recorre la grilla de precio
                        int b = grdPrecioIp.PageIndex;
                        for (int i = 0; i < grdPrecioIp.PageCount; i++)
                        {
                            grdPrecioIp.SetPageIndex(i);
                            foreach (GridViewRow row in grdPrecioIp.Rows)
                            {
                                Label labelmoneda_id = ((Label)grdPrecioIp.Rows[row.RowIndex].FindControl("lblmonedaid"));
                                string id_moneda = labelmoneda_id.Text;
                                Label lblcliente_tipo_planta_id = ((Label)grdPrecioIp.Rows[row.RowIndex].FindControl("lblcliente_tipo_planta_id"));
                                string cliente_tipo_planta_id = lblcliente_tipo_planta_id.Text;
                                TextBox lblvalor = ((TextBox)grdPrecioIp.Rows[row.RowIndex].FindControl("txt_valor"));
                                string valor = lblvalor.Text;
                                Label lbl_precio_id = ((Label)grdPrecioIp.Rows[row.RowIndex].FindControl("lblplantaprecio_id"));
                                string planta_precio = lbl_precio_id.Text;
                                Label lblcliente_tipo_planta_Costo = ((Label)grdPrecioIp.Rows[row.RowIndex].FindControl("lblcliente_tipo_planta_Costo"));
                                string Costo = lblcliente_tipo_planta_Costo.Text;
                                Label lblcliente_tipo_planta_TRM = ((Label)grdPrecioIp.Rows[row.RowIndex].FindControl("lblcliente_tipo_planta_TRM"));
                                string TRM = lblcliente_tipo_planta_TRM.Text;
                                string margen = ((TextBox)grdPrecioIp.Rows[row.RowIndex].FindControl("txtporcentaje")).Text;
                                if (!valor.Equals("") && !margen.Equals(""))
                                {
                                    if (planta_precio != "")
                                    {
                                        //actualizar en item_planta_precio
                                        mensaje1 = cmIp.ActualizarItemPlantaPrecio(Convert.ToInt64(planta_precio), true, Convert.ToDecimal(valor), Convert.ToDecimal(margen), Convert.ToDecimal(Costo), Convert.ToDecimal(TRM));
                                        if (mensaje1.Equals("OK"))
                                        { Debug.WriteLine("Se actualizo con exito en la tabla item_planta_precio"); }
                                        else { Debug.WriteLine(mensaje1); }
                                    }
                                    else
                                    {
                                        //Insertar en item_planta_precio
                                        mensaje1 = cmIp.InsertarItemPlantaPrecio(item_planta, Convert.ToInt32(id_moneda), Convert.ToInt32(cliente_tipo_planta_id), Convert.ToDecimal(valor), true, Convert.ToDecimal(margen), Convert.ToDecimal(Costo), Convert.ToDecimal(TRM), Session["usuario"].ToString());
                                        if (mensaje1.Equals("OK"))
                                        { Debug.WriteLine("Se inserto con exito en la tabla item_planta_precio"); }
                                        else { Debug.WriteLine(mensaje1); }
                                    }
                                    //Insertar en bitacora_item_planta_precio
                                    mensaje1 = cmIp.insertarBitacoraItemPlantaPrecio(item_planta, Convert.ToInt32(id_moneda), Convert.ToInt32(cliente_tipo_planta_id), Convert.ToDecimal(valor), true, Session["usuario"].ToString(), Convert.ToDecimal(margen), Convert.ToDecimal(Costo), Convert.ToDecimal(TRM));
                                    if (mensaje1.Equals("OK"))
                                    { Debug.WriteLine("Se inserto con exito en la tabla bitacora_item_planta_precio"); }
                                    else { Debug.WriteLine(mensaje1); }
                                }
                            }
                        }
                        grdPrecioIp.SetPageIndex(b);
                    }
                    //editar tabla de parametros
                    DataTable dt = Session["Tb_Parametros"] as DataTable;
                    DataTable dtelim = Session["Tb_Parametros_Delete"] as DataTable;
                    if (dt.Rows.Count != 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["item_parametro_id"].ToString().Equals(""))
                            {
                                mensaje1 = cmIp.InsertarItemParametro(item_planta, Convert.ToInt32(dt.Rows[i]["item_tipo_parametro_id"].ToString()));
                                if (mensaje1 == "OK")
                                { Debug.WriteLine("Se inserto con exito en la tabla item_parametro"); }
                                mensaje1 = cmIp.BitacoraItemParametro(item_planta, Convert.ToInt32(dt.Rows[i]["item_tipo_parametro_id"].ToString()), Session["usuario"].ToString(), true);
                                if (mensaje1 == "OK")
                                { Debug.WriteLine("Se inserto con exito en la tabla bitacora_item_parametro"); }
                            }
                        }
                    }
                    for (int i = 0; i < dtelim.Rows.Count; i++)
                    {
                        mensaje1 = cmIp.editarItemParametroActivo(dtelim.Rows[i]["idborrarparametro"].ToString());
                        if (mensaje1 == "OK")
                        { Debug.WriteLine("Se actualizo con exito en la tabla item_planta_parametro"); }
                    }
                    //PRECIOS ELIMINADOS
                    DataTable dt_eliminados = Session["eliminados_precio"] as DataTable;
                    if (dt_eliminados.Rows.Count != 0)
                    {
                        for (int j = dt_eliminados.Rows.Count - 1; j >= 0; j--)
                        {

                            DataRow dr = dt_eliminados.Rows[j];
                            mensaje1 = cmIp.ActualizarestadoIpPrecio(Convert.ToInt64(dr["item1"].ToString()), false);
                            if (mensaje1.Equals("OK"))
                            { Debug.WriteLine("Se actualizo con exito en la tabla item_planta_precio"); }
                            else { Debug.WriteLine(mensaje1); }
                        }
                    }
                    //Actualizar en tabla item_rel_criterio_iplanta
                    if (cboPlan1Ip.SelectedItem.Value != " ")
                    {
                        Int64 criterio = cmIp.Consultarcriterio(item_planta);

                        if (!criterio.Equals(0))
                        {
                            mensaje1 = cmIp.ActualizarRelCriterio(item_planta, cboPlan1Ip.SelectedItem.Value, cboPlan2Ip.SelectedItem.Value, cboPlan3Ip.SelectedItem.Value, cboPlan4Ip.SelectedItem.Value, cboPlan5Ip.SelectedItem.Value,txtPosAranIp.Text, true);
                            if (mensaje1.Equals("OK"))
                            { Debug.WriteLine("Se actualizo con exito en la tabla item_rel_criterio_iplanta"); }
                            else { Debug.WriteLine(mensaje1); }
                        }
                        else
                        {
                            mensaje1 = cmIp.InsertarRelCriterio(item_planta, cboPlan1Ip.SelectedItem.Value, cboPlan2Ip.SelectedItem.Value, cboPlan3Ip.SelectedItem.Value, txtPosAranIp.Text, true, cboPlan4Ip.SelectedItem.Value, cboPlan5Ip.SelectedItem.Value);
                            if (mensaje1.Equals("OK"))
                            { Debug.WriteLine("Se inserto con exito en la tabla item_rel_criterio_iplanta"); }
                            else { Debug.WriteLine(mensaje1); }
                        }
                        //Insertar en tabla bitacora item_rel_criterio_iplanta
                        mensaje1 = cmIp.insertarBitacoraRelCriterio(item_planta, cboPlan1Ip.SelectedItem.Value, cboPlan2Ip.SelectedItem.Value, cboPlan3Ip.SelectedItem.Value, txtPosAranIp.Text, true, Session["usuario"].ToString(), cboPlan4Ip.SelectedItem.Value, cboPlan5Ip.SelectedItem.Value);
                        if (mensaje1.Equals("OK"))
                        { Debug.WriteLine("Se inserto con exito en la tabla bitacora_criterio_iplanta"); }
                        else { Debug.WriteLine(mensaje1); }

                    }

                }
                else
                {
                    mensajeVentana(mensaje);
                }

            }


            cmIp.CerrarConexion();
            return result;
        }


        private bool validaIdiomas()
        {
            bool result = true;
            int a = grdIdiomaIp.PageIndex;
            for (int i = 0; i < grdIdiomaIp.PageCount; i++)
            {
                grdIdiomaIp.SetPageIndex(i);
                foreach (GridViewRow row in grdIdiomaIp.Rows)
                {
                    TextBox txt_descripIdiom = ((TextBox)grdIdiomaIp.Rows[row.RowIndex].FindControl("textDesc"));
                    string cadena = txt_descripIdiom.Text;
                    if (cadena == "")
                    {
                        result = false;
                    }
                }
            }
            grdIdiomaIp.SetPageIndex(a);
            return result;
        }
        /**************METODO PARA VALIDAR MONEDAS INGRESADAS EN PLANTA FORSA COLOMBIA*************/
        private bool validamoneda()
        {
            bool cumple = false, mpesos = false, mdolar = false;
            int a = grdPrecioIp.PageIndex;
            for (int i = 0; i < grdPrecioIp.PageCount; i++)
            {
                grdPrecioIp.SetPageIndex(i);
                foreach (GridViewRow row in grdPrecioIp.Rows)
                {
                    Label labelmoneda_id = ((Label)grdPrecioIp.Rows[row.RowIndex].FindControl("lblmonedaid"));
                    string id_moneda = labelmoneda_id.Text;
                    if (id_moneda.Equals("1"))
                    {
                        mpesos = true;
                    }
                    if (id_moneda.Equals("2"))
                    {
                        mdolar = true;
                    }

                }
            }
            grdPrecioIp.SetPageIndex(a);
            if (mpesos && mdolar)
            {
                cumple = true;
            }
            return cumple;
        }
        /**************METODO PARA VALIDAR MONEDAS INGRESADAS EN PLANTA DIFERENTE A  FORSA COLOMBIA*************/
        private bool validasinpesosCol()
        {
            bool mpesos = false;
            int a = grdPrecioIp.PageIndex;
            for (int i = 0; i < grdPrecioIp.PageCount; i++)
            {
                grdPrecioIp.SetPageIndex(i);
                foreach (GridViewRow row in grdPrecioIp.Rows)
                {
                    Label labelmoneda_id = ((Label)grdPrecioIp.Rows[row.RowIndex].FindControl("lblmonedaid"));
                    string id_moneda = labelmoneda_id.Text;
                    if (id_moneda.Equals("1"))
                    {
                        mpesos = true;
                    }

                }
            }
            grdPrecioIp.SetPageIndex(a);

            return mpesos;
        }
        /**************METODO PARA MOSTRAR LOS ALERT QUE ARROJA LA PAGINA*************/
        private void mensajeVentana(string mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }
        /**************EVENTO GENERADO POR  EL TEXTBOX DE PRECIO***************/
        protected void txtPrecioPlenoIp_TextChanged(object sender, EventArgs e)
        {

            if (!IsNumeric(txtPrecioPlenoIp.Text))
            {
                txtPrecioPlenoIp.Text = "";
            }
            else
            {
                decimal n = decimal.Parse(txtPrecioPlenoIp.Text);
                n = Math.Round(n, 2, MidpointRounding.ToEven);
                txtPrecioPlenoIp.Text = n.ToString("N2", new CultureInfo("en-US"));
            }

        }
        /**************METODO PARA  COMPROMBAR QUE EL DATO ES NUMERICO*************/
        private bool IsNumeric(string s)
        {
            float output;
            return float.TryParse(s, out output);
        }
        /**************EVENTO PARA EL BOTON AGREGAR EN GRIDVIEW PRECIO *********************/
        protected void btnAgregarIp_Click(object sender, ImageClickEventArgs e)
        {
            Page.Validate("grupPrecio");
            if (IsValid)
            {
                addPrecio();
            }
            else
            {
                mensajeVentana("Por favor diligenciar los campos obligatorios");
            }

        }

        protected void chkListorigen_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (chkListorigen.Items[0].Selected && chkListorigen.Items[0].Enabled)
            {
                chkListorigen.Items[1].Enabled = false;
                chkListorigen.Items[2].Enabled = false;
            }
            if (!chkListorigen.Items[0].Selected && chkListorigen.Items[0].Enabled)
            {
                chkListorigen.Items[1].Enabled = true;
                chkListorigen.Items[2].Enabled = true;
            }
            if (chkListorigen.Items[1].Selected && chkListorigen.Items[1].Enabled)
            {
                chkListorigen.Items[0].Enabled = false;
                chkListorigen.Items[2].Enabled = false;
            }
            if (!chkListorigen.Items[1].Selected && chkListorigen.Items[1].Enabled)
            {
                chkListorigen.Items[0].Enabled = true;
                chkListorigen.Items[2].Enabled = true;
            }
            if (chkListorigen.Items[2].Selected && chkListorigen.Items[2].Enabled)
            {
                chkListorigen.Items[0].Enabled = false;
                chkListorigen.Items[1].Enabled = false;
            }
            if (!chkListorigen.Items[2].Selected && chkListorigen.Items[2].Enabled)
            {
                chkListorigen.Items[0].Enabled = true;
                chkListorigen.Items[1].Enabled = true;
            }

            //*****CONDICIONES PARA SUGERIR LOS TIPOS DE INVENTARIOS***///
            //if (cboPlantaIp.SelectedIndex != 0)
            //{
            //    if (chkListorigen.SelectedIndex != -1)
            //    {

            //        string x = chkListorigen.SelectedItem.Value;
            //        cboTipInvIp.Items.Clear();
            //        reader = cmIp.SugerirTipoInv(int.Parse(cboPlantaIp.SelectedItem.Value));
            //        cboTipInvIp.Items.Add(new ListItem("Seleccione ", " "));
            //        while (reader.Read())
            //        {
            //            cboTipInvIp.Items.Add(new ListItem(reader.GetString(0), reader.GetString(0)));
            //        }
            //        reader.Close();
            //        reader.Dispose();
            //        cmIp.CerrarConexion();
            //        //if (cboPerfilIp.SelectedItem.Value.Equals("2"))
            //        //{
            //        //    cboTipInvIp.Enabled = false;
            //        //}

            //    }
            //    else
            //    {
            //        limpiarUM();
            //    }
            //}
            //else
            //{
            //    cboPlantaIp.Focus();
            //    mensajeVentana("Debe seleccionar una Planta para el Item");
            //}


        }

        private void limpiarUM()
        {
            cboTipInvIp.Items.Clear();
            cboTipInvIp.Items.Add(new ListItem(" ", " "));
            txtTipInvIp.Text = string.Empty;
            cboGrupimpIp.Items.Clear();
            cboGrupimpIp.Items.Add(new ListItem(" ", " "));
            txtGrupimpIp.Text = string.Empty;
            titulofactor.Visible = false;
            factor.Visible = false;
            trM21Ip.Visible = false;
            trLongitudIp.Visible = false;
            cboPlan1Ip.Items.Clear();
            cboPlan2Ip.Items.Clear();
            cboPlan3Ip.Items.Clear();
            cboPlan1Ip.Items.Add(new ListItem(" ", " "));
            cboPlan2Ip.Items.Add(new ListItem(" ", " "));
            cboPlan3Ip.Items.Add(new ListItem(" ", " "));
            txtPosAranIp.Text = string.Empty;
            if (cboPerfilIp.SelectedItem.Value.Equals("2") || cboPerfilIp.SelectedItem.Value.Equals("0"))
            {
                cboGrupimpIp.Enabled = false;
                cboAdicionalIp.Enabled = false;
                cboOrdenIp.Enabled = false;
            }

        }

        /***EVENTO TEXTCHANGE PARA CONTROLAR LOS NUMEROS OCN PUNTOS QUE FUERON PEGADOS EN LOS TEXTBOX**/
        protected void txtPesoLIp_TextChanged(object sender, EventArgs e)
        {
            if (!IsNumeric(txtPeso_unitario.Text))
            {
                txtPeso_unitario.Text = "0";
                txtPeso_unitario.Focus();
            }
            else
            {

                string[] stringArray = txtPeso_unitario.Text.Split('.');
                int longitud = stringArray[0].Length;

                if (longitud <= 7)
                {
                    decimal pesoL = decimal.Parse(txtPeso_unitario.Text);
                    pesoL = Math.Round(pesoL, 3, MidpointRounding.ToEven);
                    txtPeso_unitario.Text = pesoL.ToString("N3", new CultureInfo("en-US"));
                }

                else
                {
                    mensajeVentana("Numero de enteros no puede exceder de 7");
                    txtPeso_unitario.Text = "0";
                    txtPeso_unitario.Focus();
                }

            }

        }




        protected void cboTipInvIp_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtTipInvIp.Text = string.Empty;
            if (cboTipInvIp.SelectedIndex != 0)
            {
                DataTable Dt;

                Dt=cmIp.SugerirDescripcionTipoInv(int.Parse(cboPlantaIp.SelectedItem.Value), cboTipInvIp.SelectedItem.Value);

                if (Dt.Rows.Count != 0)
                {
                    txtTipInvIp.Text = Dt.Rows[0]["descripcion"].ToString();
                    poblarBodega(int.Parse(cboPlantaIp.SelectedItem.Value), cboTipInvIp.SelectedItem.Value);


                }
                string cia = cmIp.ConsultarCia(Convert.ToInt32(cboPlantaIp.SelectedItem.Value));              
                /***Se carga el grupo impositivo***/
                //string x = chkListorigen.SelectedItem.Value;
                //cboGrupimpIp.Enabled = true;
                cboPrincipalIp.Enabled = true;
                //cboAdicionalIp.Enabled = true;
                cboOrdenIp.Enabled = true;
                //cboGrupimpIp.Items.Clear();
                //txtGrupimpIp.Text = string.Empty;
                cboPlan2Ip.Items.Clear();
                cboPlan3Ip.Items.Clear();
                txtPosAranIp.Text = string.Empty;
                lblPosAranIp.Text = "Posici&oacute;n Arancelaria:";
                //CargarUnidadMedida();
                //reader = cmIp.SugerirGrupoImp(int.Parse(cboPlantaIp.SelectedItem.Value), int.Parse(x), cboTipInvIp.SelectedItem.Value);
                //reader.Read();
                //string val = (reader.IsDBNull(0)) ? "" : reader.GetString(0);
                //if (val != "")
                //{
                //    cboGrupimpIp.Items.Add(new ListItem(reader.GetString(0), reader.GetString(0)));
                //    txtGrupimpIp.Text = cmIp.DescpGrupImp1E(Convert.ToInt32(cboPlantaIp.SelectedItem.Value), reader.GetString(0));
                //    if (cboPerfilIp.SelectedItem.Value.Equals("2"))
                //    {
                //        cboGrupimpIp.Enabled = false;
                //    }

                //}
                //else
                //{
                //    //No existe relacion  de planta + origen + tipo de inventario  = grupo impositivo
                //    //if (cboPerfilIp.SelectedItem.Value.Equals("1"))
                //    //{
                //    //se busca todos los grupos impositivos de oracle asociados a  la planta seleccionada
                //    reader.Close();
                //    reader.Dispose();

                //poblarGrupoImpositivo(int.Parse(cboPlantaIp.SelectedItem.Value));                 


                //}
                //if (cboPerfilIp.SelectedItem.Value.Equals("2"))
                //{
                //    cboGrupimpIp.Items.Add(new ListItem(" ", " "));
                //    cboGrupimpIp.Enabled = false;
                //}


                //}
                cboGrupimpIp.Focus();
                //se carga los criterios de clasificación 1
                CargarPlan1();
                CargarPlan4();
                cboPlan2Ip.Items.Add(new ListItem(" ", " "));
                cboPlan3Ip.Items.Add(new ListItem(" ", " "));
                reader.Close();
                reader.Dispose();
                cmIp.CerrarConexion();

            }
            else
            {
                //cboGrupimpIp.Items.Clear();
                //cboGrupimpIp.Items.Add(new ListItem(" ", " "));
                //txtGrupimpIp.Text = string.Empty;
                titulofactor.Visible = false;
                factor.Visible = false;
                trM21Ip.Visible = false;
                trLongitudIp.Visible = false;
                cboPlan1Ip.Items.Clear();
                cboPlan2Ip.Items.Clear();
                cboPlan3Ip.Items.Clear();
                cboPlan1Ip.Items.Add(new ListItem(" ", " "));
                cboPlan2Ip.Items.Add(new ListItem(" ", " "));
                cboPlan3Ip.Items.Add(new ListItem(" ", " "));
                txtPosAranIp.Text = string.Empty;
                //cboGrupimpIp.Enabled = true;
                cboPrincipalIp.Enabled = true;
                cboAdicionalIp.Enabled = true;
                cboOrdenIp.Enabled = true;
                //CargarUnidadMedida();

            }
        }

        public void poblarGrupoImpositivo(int planta)
        {
            cboGrupimpIp.Items.Clear();
            reader = cmIp.SugerirGrupoImp1E(planta);
            cboGrupimpIp.Items.Add(new ListItem("Seleccione ", "0"));
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboGrupimpIp.Items.Add(new ListItem(reader.GetString(1), reader.GetString(0)));
                }
            }
            cboGrupimpIp.SelectedValue = "0";
            reader.Close();
            reader.Dispose();
            cmIp.CerrarConexion();

            if (cboPerfilIp.SelectedItem.Value.Equals("2") || cboPerfilIp.SelectedItem.Value.Equals("0"))
            {
                trGrupimpIp.Visible = false;
                cboGrupimpIp.Items.Add(new ListItem(" ", " "));
                cboGrupimpIp.Enabled = false;
            }
        }

        public void poblarInventario(int planta)
        {
            cboTipInvIp.Items.Clear();
            reader = cmIp.SugerirTipoInv(planta);
            cboTipInvIp.Items.Add(new ListItem("Seleccione ", "0"));
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboTipInvIp.Items.Add(new ListItem(reader.GetString(1), reader.GetString(0)));
                }
            }
            reader.Close();
            reader.Dispose();
            cmIp.CerrarConexion();
        }

        public void poblarBodega(int planta, string tipoInventario)
        {
            cbo_Bodega.Items.Clear();
            reader = cmIp.SugerirBodega(planta, tipoInventario);
            cbo_Bodega.Items.Add(new ListItem("Seleccione ", "0"));
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cbo_Bodega.Items.Add(new ListItem(reader.GetString(0), reader.GetString(0)));
                }
            }
            reader.Close();
            reader.Dispose();
            cmIp.CerrarConexion();
        }

        private void CargarPlan1()
        {
            cboPlan1Ip.Items.Clear();
            reader = cmIp.SugerirPlan1(int.Parse(cboPlantaIp.SelectedItem.Value), cboTipInvIp.SelectedItem.Value);
            if (reader.HasRows)
            {
                cboPlan1Ip.Items.Add(new ListItem("Seleccione", " "));
                while (reader.Read())
                {
                    cboPlan1Ip.Items.Add(new ListItem(reader.GetString(0) + "  " + reader.GetString(1), reader.GetString(0)));
                }

            }
            else
            {

                cboPlan1Ip.Items.Add(new ListItem(" ", " "));
            }
            reader.Close();
            reader.Dispose();
            cmIp.CerrarConexion();
        }

        private void CargarPlan4()
        {
            cboPlan4Ip.Items.Clear();
            cboPlan5Ip.Items.Clear();
            reader = cmIp.SugerirPlan4(int.Parse(cboPlantaIp.SelectedItem.Value), cboTipInvIp.SelectedItem.Value);
            if (reader.HasRows)
            {
                cboPlan4Ip.Items.Add(new ListItem("Seleccione", " "));
                while (reader.Read())
                {
                    cboPlan4Ip.Items.Add(new ListItem(reader.GetString(0) + "  " + reader.GetString(1), reader.GetString(0)));
                }
            }
            else
            {

                cboPlan4Ip.Items.Add(new ListItem(" ", " "));
                cboPlan5Ip.Items.Add(new ListItem(" ", " "));
            }
            reader.Close();
            reader.Dispose();
            cmIp.CerrarConexion();
        }

        protected void cboGrupimpIp_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtGrupimpIp.Text = string.Empty;
            if (cboGrupimpIp.SelectedValue != " " && cboGrupimpIp.SelectedItem.Value != "0")
            {
                txtGrupimpIp.Text = cmIp.DescpGrupImp1E(int.Parse(cboPlantaIp.SelectedItem.Value), cboGrupimpIp.SelectedItem.Value);
                cmIp.CerrarConexion();
                titulofactor.Visible = false;
                factor.Visible = false;
                trM21Ip.Visible = false;
                trLongitudIp.Visible = false;
                cboPrincipalIp.Focus();

            }

        }

        protected void cboPrincipalIp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //limpiar todos las unidades de medida y sus campos para adicional y de orden
            txtPrincipalIp.Text = string.Empty;
            if (cboPrincipalIp.SelectedIndex != 0)
            {


                txtPrincipalIp.Text = cmIp.DescpUP1E(int.Parse(cboPlantaIp.SelectedItem.Value), cboPrincipalIp.SelectedItem.Value);
                cmIp.CerrarConexion();
                if (cboPerfilIp.SelectedItem.Value.Equals("1"))
                {
                    cboAdicionalIp.SelectedIndex = 0;
                    txtAdicionalIp.Text = string.Empty;
                    cboOrdenIp.SelectedIndex = 0;
                    txtOrdenIp.Text = string.Empty;
                    cboOrdenIp.Enabled = true;
                    cboAdicionalIp.Enabled = true;
                    titulofactor.Visible = false;
                    trM21Ip.Visible = false;
                    titulofactor.Visible = false;
                    factor.Visible = false;
                    trLongitudIp.Visible = false;
                    cleancontroles();
                }

                if (cboPerfilIp.SelectedItem.Value.Equals("2") || cboPerfilIp.SelectedItem.Value.Equals("0"))
                {
                    cboGrupimpIp.Enabled = false;
                    cboGrupimpIp.Visible = false;
                    lblGrupimpIp.Visible = false;
                    txtGrupimpIp.Visible = false;
                    cboAdicionalIp.Enabled = false;
                    cboOrdenIp.Enabled = false;
                }
            }
            else
            {
                cboAdicionalIp.SelectedIndex = 0;
                txtAdicionalIp.Text = string.Empty;
                cboOrdenIp.SelectedIndex = 0;
                txtOrdenIp.Text = string.Empty;
                cboOrdenIp.Enabled = true;
                cboAdicionalIp.Enabled = true;
                titulofactor.Visible = false;
                factor.Visible = false;
                trM21Ip.Visible = false;
                titulofactor.Visible = false;
                factor.Visible = false;
                trLongitudIp.Visible = false;
                if (cboPerfilIp.SelectedItem.Value.Equals("2") || cboPerfilIp.SelectedItem.Value.Equals("0"))
                {
                    cboGrupimpIp.Enabled = false;
                    cboAdicionalIp.Enabled = false;
                    cboOrdenIp.Enabled = false;
                }
            }

        }

        protected void cboAdicionalIp_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAdicionalIp.Text = string.Empty;
            if (cboAdicionalIp.SelectedIndex != 0)
            {
                if (cboPrincipalIp.SelectedIndex != 0)
                {
                    titulofactor.Visible = false;
                    factor.Visible = false;
                    trM21Ip.Visible = false;
                    trLongitudIp.Visible = false;
                    cleancontroles();
                    txtAdicionalIp.Text = cmIp.DescpUP1E(int.Parse(cboPlantaIp.SelectedItem.Value), cboAdicionalIp.SelectedItem.Value);
                    cboOrdenIp.Enabled = false;

                    if (!cboPrincipalIp.SelectedItem.Text.Equals(cboAdicionalIp.SelectedItem.Text))
                    {
                        if (cboPerfilIp.SelectedItem.Value.Equals("1"))
                        {
                            titulofactor.Visible = true;
                            trM21Ip.Visible = true;
                            txtFactorM2Ip.Focus();
                            rqftxtFactorM2Ip.Enabled = true;
                        }
                    }

                    if (cboPrincipalIp.SelectedItem.Text.Equals(cboAdicionalIp.SelectedItem.Text))
                    {

                        factor.Visible = false;
                        trM21Ip.Visible = false;
                        mensajeVentana("Relación entre unidad de medida no es valida");
                        //cboPrincipalIp.SelectedIndex = 0;
                        cboAdicionalIp.SelectedIndex = 0;
                        txtAdicionalIp.Text = string.Empty;
                        cboOrdenIp.SelectedIndex = 0;
                        txtOrdenIp.Text = string.Empty;
                        cboOrdenIp.Enabled = true;
                        cboAdicionalIp.Enabled = true;

                    }
                    cmIp.CerrarConexion();
                }
                else
                {
                    cboAdicionalIp.SelectedIndex = 0;
                    mensajeVentana("Debe seleccionar una unidad de medida para Principal");
                }
            }
            else
            {
                cboOrdenIp.Enabled = true; titulofactor.Visible = false; factor.Visible = false; trM21Ip.Visible = false;
            }


        }

        protected void cboOrdenIp_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtOrdenIp.Text = string.Empty;
            if (cboOrdenIp.SelectedIndex != 0)
            {
                if (cboPrincipalIp.SelectedIndex != 0)
                {
                    titulofactor.Visible = false;
                    factor.Visible = false;
                    trM21Ip.Visible = false;
                    trLongitudIp.Visible = false;
                    cleancontroles();
                    txtOrdenIp.Text = cmIp.DescpUP1E(int.Parse(cboPlantaIp.SelectedItem.Value), cboOrdenIp.SelectedItem.Value);
                    cboAdicionalIp.Enabled = false;
                    if (cboPrincipalIp.SelectedItem.Text.Equals(cboOrdenIp.SelectedItem.Text))
                    {
                        if (cboPerfilIp.SelectedItem.Value.Equals("1"))
                        {
                            titulofactor.Visible = true;
                            factor.Visible = true;
                        }

                    }
                    if (!cboPrincipalIp.SelectedItem.Text.Equals(cboOrdenIp.SelectedItem.Text))
                    {
                        titulofactor.Visible = false;
                        factor.Visible = false;
                        if (cboPerfilIp.SelectedItem.Value.Equals("1"))
                        {
                            trLongitudIp.Visible = true;
                            txtFactorLIp.Focus();
                            rqftxtFactorLIp.Enabled = true;
                        }

                    }
                    cmIp.CerrarConexion();
                }
                else
                {
                    cboOrdenIp.SelectedIndex = 0;
                    mensajeVentana("Debe seleccionar una unidad de medida para Principal");
                }

            }
            else
            {
                cboAdicionalIp.Enabled = true; titulofactor.Visible = false;
                factor.Visible = false; trLongitudIp.Visible = false;
            }

        }



        private void cleancontroles()
        {
            txtFactorLIp.Text = string.Empty;
            txtFactorM2Ip.Text = string.Empty;
            rqftxtFactorLIp.Enabled = false;
            rqftxtFactorM2Ip.Enabled = false;

        }


        protected void cboPlan1Ip_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboPlan2Ip.Items.Clear();
            cboPlan2Ip.Items.Add(new ListItem(" ", " "));
            cboPlan3Ip.Items.Clear();
            cboPlan3Ip.Items.Add(new ListItem(" ", " "));
            if (cboPlan1Ip.SelectedIndex != 0)
            {
                CargarPlan2();
                cboPlan2Ip.Focus();

            }

        }

        private void CargarPlan2()
        {
            cboPlan2Ip.Items.Clear();
            reader = cmIp.SugerirPlan2(int.Parse(cboPlantaIp.SelectedItem.Value), cboTipInvIp.SelectedItem.Value, cboPlan1Ip.SelectedItem.Value);
            if (reader.HasRows)
            {
                cboPlan2Ip.Items.Add(new ListItem("Seleccione", " "));
                while (reader.Read())
                {
                    cboPlan2Ip.Items.Add(new ListItem(reader.GetString(0) + "  " + reader.GetString(1), reader.GetString(0)));
                }

            }
            else
            {

                cboPlan2Ip.Items.Clear();
                cboPlan2Ip.Items.Add(new ListItem(" ", " "));
            }
            reader.Close();
            reader.Dispose();
            cmIp.CerrarConexion();

        }

        protected void cboPlan3Ip_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPosAranIp.Text = string.Empty;
            if (cboPlan3Ip.SelectedIndex != 0)
            {
                CargarPosA();
                txtPosAranIp.Focus();

            }

        }

        private void CargarPlan5()
        {
            cboPlan5Ip.Items.Clear();
            reader = cmIp.SugerirPlan5(int.Parse(cboPlantaIp.SelectedItem.Value), cboTipInvIp.SelectedItem.Value, cboPlan4Ip.SelectedItem.Value);
            if (reader.HasRows)
            {
                cboPlan5Ip.Items.Add(new ListItem("Seleccione", " "));
                while (reader.Read())
                {
                    cboPlan5Ip.Items.Add(new ListItem(reader.GetString(0) + "  " + reader.GetString(1), reader.GetString(0)));
                }

            }
            else
            {

                cboPlan5Ip.Items.Clear();
                cboPlan5Ip.Items.Add(new ListItem(" ", " "));
            }
            reader.Close();
            reader.Dispose();
            cmIp.CerrarConexion();
        }



        private void CargarPosA()
        {
            txtPosAranIp.Text = string.Empty;
            txtPosAranIp.Text = cmIp.PoscArancelaria(int.Parse(cboPlantaIp.SelectedItem.Value), cboTipInvIp.SelectedItem.Value, cboPlan1Ip.SelectedItem.Value, cboPlan2Ip.SelectedItem.Value, cboPlan3Ip.SelectedItem.Value);
            cmIp.CerrarConexion();
        }

        protected void cboPlan2Ip_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboPlan3Ip.Items.Clear();
            cboPlan3Ip.Items.Add(new ListItem(" ", " "));
            if (cboPlan2Ip.SelectedIndex != 0)
            {
                CargarPlan3();
                cboPlan3Ip.Focus();
            }
        }

        protected void cboPlan4Ip_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboPlan5Ip.Items.Clear();
            cboPlan5Ip.Items.Add(new ListItem(" ", " "));
            if (cboPlan4Ip.SelectedIndex != 0)
            {
                CargarPlan5();
                cboPlan5Ip.Focus();
            }
        }

        private void CargarPlan3()
        {
            cboPlan3Ip.Items.Clear();
            reader = cmIp.SugerirPlan3(int.Parse(cboPlantaIp.SelectedItem.Value), cboTipInvIp.SelectedItem.Value, cboPlan1Ip.SelectedItem.Value, cboPlan2Ip.SelectedItem.Value);

            if (reader.HasRows)
            {
                cboPlan3Ip.Items.Add(new ListItem("Seleccione", " "));
                while (reader.Read())
                {
                    cboPlan3Ip.Items.Add(new ListItem(reader.GetString(0) + "  " + reader.GetString(1), reader.GetString(0)));
                }

            }
            else
            {

                cboPlan3Ip.Items.Clear();
                cboPlan3Ip.Items.Add(new ListItem(" ", " "));
            }
            reader.Close();
            reader.Dispose();
            cmIp.CerrarConexion();
        }

        protected void cboPerfilIp_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpiarGrillas(e);
            chkListusoIp.Items[0].Selected = true;
            chkListusoIp.Items[1].Selected = true;
            chkListusoIp.Items[2].Selected = true;
            if (cboPerfilIp.SelectedItem.Value.Equals("2") || cboPerfilIp.SelectedItem.Value.Equals("0"))
            {

                titleuso.Text = "Uso:";
                TbItem.Visible = true;
                cvchkListusoIp.Enabled = false;
                trTipInvIp.Visible = true;
                trGrupimpIp.Visible = false;
                trAdicionalIp.Visible = false;
                trOrdenIp.Visible = false;
                trplan1.Visible = false;
                trplan2.Visible = false;
                trGrupoIp.Visible = false;
                trcreadoitem.Visible = false;
                tdGruPlantaIp.Visible = false;
                tdlblGruPlantaIp.Visible = false;
                tdcriterio.Visible = false;
                AccordPanePrecioIp.Visible = false;
                trundadiccionales.Visible = true;
                trGrupoIp.Visible = false;
                if (cboPerfilIp.Items.Count > 1)
                {
                    TbItem.Visible = true;
                    trcboperfil.Visible = true;
                    trlblperfil.Visible = true;
                }
            }
            if (cboPerfilIp.SelectedItem.Value.Equals("1"))
            {

                AccordPanePrecioIp.Visible = true;
                trundadiccionales.Visible = true;
                titleuso.Text = "*Uso:";
                cvchkListusoIp.Enabled = true;
                cboTipInvIp.Enabled = true;
                cboGrupimpIp.Enabled = true;
                cboGrupimpIp.Visible = true;
                lblGrupimpIp.Visible = true;
                cboPlan1Ip.Enabled = true;
                cboPlan2Ip.Enabled = true;
                cboPlan3Ip.Enabled = true;
                cboAdicionalIp.Enabled = true;
                cboOrdenIp.Enabled = true;
                TbItem.Visible = true;
                trTipInvIp.Visible = true;
                trGrupimpIp.Visible = true;
                trAdicionalIp.Visible = true;
                trOrdenIp.Visible = true;
                trplan1.Visible = true;
                trplan2.Visible = true;
                trGrupoIp.Visible = true;
                trcreadoitem.Visible = true;
                tdGruPlantaIp.Visible = true;
                tdlblGruPlantaIp.Visible = true;
                tdcriterio.Visible = true;
                trlistuso.Style.Add("display", "block");

            }
            if (cboPerfilIp.SelectedItem.Value.Equals("3"))
            {
                AccordionPrincipalIp.SelectedIndex = 1;
                titleuso.Text = "Uso:";
                TbItem.Visible = true;
                cvchkListusoIp.Enabled = false;
                trTipInvIp.Visible = true;
                trGrupimpIp.Visible = true;
                lblTitleUnddIp.Visible = false;
                trprincipalip.Visible = false;
                trAdicionalIp.Visible = false;
                trOrdenIp.Visible = false;
                AccordPanePrecioIp.Visible = false;
                trundadiccionales.Visible = true;
                trplan1.Visible = false;
                trplan2.Visible = false;
                trcreadoitem.Visible = false;
                tdGruPlantaIp.Visible = false;
                tdlblGruPlantaIp.Visible = false;
                tdcriterio.Visible = false;
                AccordPaneIdiomaIp.Visible = false;
                trpeso.Visible = false;
                cboGrupoIp.Enabled = false;
                //cboGrupimpIp.Enabled = false;
                //cboGrupimpIp.Visible = false;
                //lblGrupimpIp.Visible = false;

                cboAgrupadorIp.Enabled = false;
                cboPlantaIp.Enabled = false;
                txtDscAbrvIp.Enabled = false;
                txtDscIp.Enabled = false;
                chkListorigen.Enabled = false;
                if (btnGuardarIp.Text.Equals("Guardar"))
                {
                    btnGuardarIp.Visible = false;
                }
                btnLimpiarIp.Visible = false;
                trGrupoIp.Visible = false;
            }
            Session["perfil_usu"] = cboPerfilIp.SelectedItem.Value;

        }

        private void LimpiarGrillas(EventArgs e)
        {
            chkListorigen.ClearSelection();
            chkListusoIp.ClearSelection();
            chkListorigen.Items[0].Enabled = true;
            chkListorigen.Items[1].Enabled = true;
            chkListorigen.Items[2].Enabled = true;
            CargarGrupo();
            CargarClaseItem();
            CargarPlanta(e);
            CargarIdioma();
            CargarPrecioIp();
            CargarPrecioDelete();
            titulofactor.Visible = false;
            factor.Visible = false;
            trLongitudIp.Visible = false;
            trM21Ip.Visible = false;
            txtGruPlantaIpId.Text = string.Empty;
            txtGruPlantaIp.Text = string.Empty;
            cboTipInvIp.Items.Clear();
            cboTipInvIp.Items.Add(new ListItem(" ", " "));
            txtTipInvIp.Text = string.Empty;
            cboGrupimpIp.Items.Clear();
            cboGrupimpIp.Items.Add(new ListItem(" ", " "));
            txtGrupimpIp.Text = string.Empty;
            cboPrincipalIp.Items.Clear();
            txtPrincipalIp.Text = string.Empty;
            cboPrincipalIp.Items.Add(new ListItem(" ", " "));
            cboAdicionalIp.Items.Clear();
            txtAdicionalIp.Text = string.Empty;
            cboAdicionalIp.Items.Add(new ListItem(" ", " "));
            cboOrdenIp.Items.Clear();
            txtOrdenIp.Text = string.Empty;
            cboOrdenIp.Items.Add(new ListItem(" ", " "));
            cboPlan1Ip.Items.Clear();
            cboPlan2Ip.Items.Clear();
            cboPlan3Ip.Items.Clear();
            cboPlan4Ip.Items.Clear();
            cboPlan5Ip.Items.Clear();
            cboPlan1Ip.Items.Add(new ListItem(" ", " "));
            cboPlan2Ip.Items.Add(new ListItem(" ", " "));
            cboPlan3Ip.Items.Add(new ListItem(" ", " "));
            cboPlan4Ip.Items.Add(new ListItem(" ", " "));
            cboPlan5Ip.Items.Add(new ListItem(" ", " "));
            txtfactorunitario.Text = "1";
            btnGuardarIp.Text = "Guardar";
            btnEnviarIp.Visible = false;
            btnAprobarIp.Visible = false;
            btnduplicar.Visible = false;
            btnRechazarIp.Visible = false;
            btnDevolverIp.Visible = false;
            btnGuardarIp.Visible = true;
            cboAgrupadorIp.Items.Clear();
            cboAgrupadorIp.Items.Add(new ListItem(" ", " "));
            cboAgrupadorIp.SelectedIndex = 0;
            cboGrupimpIp.Enabled = true;
            cboPrincipalIp.Enabled = true;
            cboAdicionalIp.Enabled = true;
            cboOrdenIp.Enabled = true;
            cleancontroles();
            txtPosAranIp.Text = string.Empty;
            cboGrupoIp.Enabled = true;
            cboAgrupadorIp.Enabled = true;
            cboPlantaIp.Enabled = true;
            cboClaseItem.Enabled = true;
            AccordPanePrecioIp.Visible = false;
            trundadiccionales.Visible = true;
            txtCodErpIp.Text = string.Empty;
            btnCodigoERP.Visible = false;
            btnCodigoERP.Text = "Consulta a ERP";
            btnCodigoERP.Enabled = false;
            chkCodigoERP.Checked = false;
            lblNombreERP.Visible = false;
            lblNombreERPTxt.Visible = false;
            lblNombreERPTxt.Text = "";
            txtReferenciaIp.Text = string.Empty;
            txtDscIp.Text = string.Empty;
            txtDscAbrvIp.Text = string.Empty;
            txtPeso_unitario.Text = string.Empty;
            trlblobservacion.Visible = false;
            trtxtobservacion.Visible = false;
            TbItem.Visible = true;
            chkListusoIp.Items[0].Selected = true;
            chkListusoIp.Items[1].Selected = true;
            chkListusoIp.Items[2].Selected = true;
            lblmsjgrupo.Text = string.Empty;
            lblmsjitemforsa.Text = string.Empty;
            lblactivoIp.Text = string.Empty;
            cboiplantacreados.Items.Clear();
            cboiplantacreados.Items.Add(new ListItem(" ", " "));
            cboiplantacreados.SelectedIndex = 0;
            CargarPlanta(e);
            txtDscAbrvIp.Enabled = true;
            txtDscIp.Enabled = true;
            trlistuso.Style.Add("display", "none");
            txtTrm.Text = string.Empty;
            txtPrecioPlenoIp.Text = string.Empty;
            txtmonedaip.Text = string.Empty;
            CargarTipoOrden();
            CargarParametro();
            CargarGridParametro();
            trcomboparametros.Visible = true; tdrequiereItem.Visible = true; trgridparametros.Visible = true;
            txtAncho1.Text = string.Empty;
            txtAncho2.Text = string.Empty;
            txtAlto1.Text = string.Empty;
            txtAlto2.Text = string.Empty;
            txtLargo.Text = string.Empty;
            txtCantidadEmpaqueIa.Text = string.Empty;
            txtPesoEmpaqueIa.Text = string.Empty;
            lblmsjTipoOrdenIa.Text = string.Empty;
            chkkamban.Checked = false;
            chkListaDisponiblesIa.ClearSelection();
            chkListaRequiereIa.ClearSelection();
            chkListorigen.Enabled = true;
            btnLimpiarIp.Visible = true;
            txtobservestado.Enabled = true;
        }
        //evento clic en limpiar
        protected void btnLimpiarIp_Click(object sender, EventArgs e)
        {
            cboPerfilIp_SelectedIndexChanged(cboPerfilIp, e);
            lblDesciplanta.Value = "";
            //kp
            Limpiar_Campos_Accesorio();
            LblEstadoAnula.Text = "";
            btn_Anular.Visible = false;
            //200618
            Ocultar_Grupo_Item_Forsa(false);
            limpiarItem();
        }


        public void CargarItemForsa(EventArgs e)
        {
            if (cboGrupoIp.Items.FindByValue(cboGrupoIa.SelectedItem.Value) != null)
            {
                cboGrupoIp.SelectedValue = cboGrupoIa.SelectedItem.Value;
                cboGrupoIp_SelectedIndexChanged(cboGrupoIp, e);
            }
            if (cboAgrupadorIp.Items.FindByValue(Session["item_id"].ToString()) != null)
            {
                cboAgrupadorIp.SelectedValue = Session["item_id"].ToString();
                cboAgrupadorIp_SelectedIndexChanged(cboAgrupadorIp, e);
            }
            //txtDscIp.Text = txtDescripcionIa.Text;
            //string cadena = "";
            //cadena = txtDscIp.Text;
            //if (cadena.Length <= 20)
            //{
            //    txtDscAbrvIp.Text = cadena;
            //}
            //else { txtDscAbrvIp.Text = cadena.Substring(0, 20); }

        }

        private void CargarDatos(string p, EventArgs e)
        {
            DataTable TbLLenar = new DataTable();
            bool req_InspCalidad, req_InspObligatoria;
            TbLLenar = cmIp.ConsultarItemPrincipal(Convert.ToInt64(p));
            if (TbLLenar.Rows.Count != 0)
            { //Se LLenan los campos de acuerdo a los valores devuletos
                if (cboPlantaIp.Items.FindByValue(TbLLenar.Rows[0]["planta_id"].ToString()) != null)
                {
                    cboPlantaIp.SelectedValue = TbLLenar.Rows[0]["planta_id"].ToString();
                    cboPlantaIp_SelectedIndexChanged(cboPlantaIp, e);
                    chkListorigen.Items[(int)TbLLenar.Rows[0]["item_origen_id"] - 1].Selected = true;
                    chkListorigen_SelectedIndexChanged(chkListorigen, e);
                    if (!TbLLenar.Rows[0]["tipo_inventario"].ToString().Equals(""))
                    {
                        cboTipInvIp.SelectedValue = TbLLenar.Rows[0]["tipo_inventario"].ToString().Trim(); ;
                        cboTipInvIp_SelectedIndexChanged(cboTipInvIp, e);

                    }
                    if (!TbLLenar.Rows[0]["grupo_impositivo"].ToString().Equals("0") && !TbLLenar.Rows[0]["grupo_impositivo"].ToString().Equals(""))
                    {
                        cboGrupimpIp.SelectedValue = TbLLenar.Rows[0]["grupo_impositivo"].ToString().Trim();
                        cboGrupimpIp_SelectedIndexChanged(cboGrupimpIp, e);
                    }
                    if (!TbLLenar.Rows[0]["und_medida_principal"].ToString().Equals(""))
                    {
                        cboPrincipalIp.SelectedValue = TbLLenar.Rows[0]["und_medida_principal"].ToString().Trim();
                        cboPrincipalIp_SelectedIndexChanged(cboPrincipalIp, e);
                    }
                    if ((decimal)TbLLenar.Rows[0]["factor_orden"] != 0)
                    {
                        cboOrdenIp.SelectedValue = TbLLenar.Rows[0]["und_medida_orden"].ToString().Trim();
                        cboOrdenIp_SelectedIndexChanged(cboOrdenIp, e);
                        if (!cboPrincipalIp.SelectedItem.Text.Equals(cboOrdenIp.SelectedItem.Text))
                        {
                            decimal factor = (decimal)TbLLenar.Rows[0]["factor_orden"];
                            txtFactorLIp.Text = factor.ToString("N4", new CultureInfo("en-US"));
                        }

                    }
                    if ((decimal)TbLLenar.Rows[0]["factor_adicional"] != 0)
                    {
                        cboAdicionalIp.SelectedValue = TbLLenar.Rows[0]["und_medida_adicional"].ToString().Trim();
                        cboAdicionalIp_SelectedIndexChanged(cboAdicionalIp, e);
                        txtFactorM2Ip.Text = TbLLenar.Rows[0]["factor_adicional"].ToString();

                    }
                    if (TbLLenar.Rows[0]["item_ClasItem"].ToString().Equals(""))
                    {
                        cboClaseItem.SelectedValue = TbLLenar.Rows[0]["item_ClasItem"].ToString().Trim();
                        cboClaseItem_SelectedIndexChange(cboClaseItem, e);
                    }

                    if (!TbLLenar.Rows[0]["item_Bodega_Desc"].ToString().Equals("0") && !TbLLenar.Rows[0]["item_Bodega_Desc"].ToString().Equals(""))
                    {
                        cbo_Bodega.SelectedValue = TbLLenar.Rows[0]["item_Bodega_Desc"].ToString().Trim();
                       // cbo_Bodega_SelectedIndexChanged(cbo_Bodega, e);
                    }


                }
                else
                { cboPlantaIp.SelectedIndex = 0; }

                if (cboGrupoIp.Items.FindByValue(TbLLenar.Rows[0]["grupo"].ToString()) != null)
                {
                    cboGrupoIp.SelectedValue = TbLLenar.Rows[0]["grupo"].ToString().Trim();
                    cboGrupoIp_SelectedIndexChanged(cboGrupoIp, e);
                    cboClaseItem.SelectedValue = TbLLenar.Rows[0]["item_ClasItem"].ToString().Trim();
                    txtGruPlantaIpId.Text = TbLLenar.Rows[0]["item_grupo_planta_id"].ToString();
                    txtGruPlantaIp.Text = TbLLenar.Rows[0]["grupoplanta"].ToString();
                    if (!TbLLenar.Rows[0]["item_id"].ToString().Equals("0"))
                    {
                        if (cboAgrupadorIp.Items.FindByValue(TbLLenar.Rows[0]["item_id"].ToString()) != null)
                        {
                            cboAgrupadorIp.SelectedValue = TbLLenar.Rows[0]["item_id"].ToString().Trim();
                            lblDesc.Value = TbLLenar.Rows[0]["item_id"].ToString();
                            txtDescripcionIa_TextChanged(txtDescripcionIa, e);
                            CargarItemplanta();
                        }
                        else
                        {
                            lblmsjitemforsa.Text = TbLLenar.Rows[0]["descripcion"].ToString();
                        }

                    }
                }
                else
                {
                    lblmsjgrupo.Text = TbLLenar.Rows[0]["grupo_desc"].ToString();
                    txtGruPlantaIpId.Text = TbLLenar.Rows[0]["item_grupo_planta_id"].ToString();
                    txtGruPlantaIp.Text = TbLLenar.Rows[0]["grupoplanta"].ToString();
                    if (!TbLLenar.Rows[0]["item_id"].ToString().Equals("0"))
                    {
                        if (cboAgrupadorIp.Items.FindByValue(TbLLenar.Rows[0]["item_id"].ToString()) != null)
                        {
                            cboAgrupadorIp.SelectedValue = TbLLenar.Rows[0]["item_id"].ToString().Trim();
                            cboAgrupadorIp_SelectedIndexChanged(cboAgrupadorIp, e);
                        }
                        else
                        {
                            lblmsjitemforsa.Text = TbLLenar.Rows[0]["descripcion"].ToString();
                        }
                    }
                }
                txtCodErpIp.Text = TbLLenar.Rows[0]["cod_erp"].ToString();
                txtReferenciaIp.Text = TbLLenar.Rows[0]["referencia"].ToString().Trim();
                txtDscIp.Text = TbLLenar.Rows[0]["detalle"].ToString();
                txtDscIp.Text = LimitLength(txtDscIp.Text, 40);
                txtDscAbrvIp.Text = TbLLenar.Rows[0]["descripcion_corta"].ToString();
                chkListusoIp.Items[0].Selected = (bool)TbLLenar.Rows[0]["uso_compra"];
                chkListusoIp.Items[1].Selected = (bool)TbLLenar.Rows[0]["uso_venta"];
                chkListusoIp.Items[2].Selected = (bool)TbLLenar.Rows[0]["uso_manufactura"];
                txtPeso_unitario.Text = TbLLenar.Rows[0]["peso_unitario"].ToString();
                txtPesoEmpaqueIa.Text = TbLLenar.Rows[0]["peso_empaque"].ToString();
                txtCantidadEmpaqueIa.Text = TbLLenar.Rows[0]["cant_empaque"].ToString();
                txtLargo.Text = TbLLenar.Rows[0]["largo"].ToString();
                txtAncho1.Text = TbLLenar.Rows[0]["ancho1"].ToString();
                txtAncho2.Text = TbLLenar.Rows[0]["ancho2"].ToString();
                txtAlto1.Text = TbLLenar.Rows[0]["alto1"].ToString();
                txtAlto2.Text = TbLLenar.Rows[0]["alto2"].ToString();
                chkkamban.Checked = Convert.ToBoolean(TbLLenar.Rows[0]["tipo_kamban"]);
                chkListaDisponiblesIa.Items[0].Selected = Convert.ToBoolean(TbLLenar.Rows[0]["disp_cotizacion"]);
                chkListaDisponiblesIa.Items[1].Selected = Convert.ToBoolean(TbLLenar.Rows[0]["disp_comercial"]);
                chkListaDisponiblesIa.Items[2].Selected = Convert.ToBoolean(TbLLenar.Rows[0]["disp_produccion"]);
                chkListaDisponiblesIa.Items[3].Selected = Convert.ToBoolean(TbLLenar.Rows[0]["disp_ingenieria"]);
                chkListaDisponiblesIa.Items[4].Selected = Convert.ToBoolean(TbLLenar.Rows[0]["disp_almacen"]);
                chkListaRequiereIa.Items[0].Selected = Convert.ToBoolean(TbLLenar.Rows[0]["req_plano"]);
                chkListaRequiereIa.Items[1].Selected = Convert.ToBoolean(TbLLenar.Rows[0]["req_modelo"]);
                chkListaRequiereIa.Items[2].Selected = Convert.ToBoolean(TbLLenar.Rows[0]["req_tipo"]);
                req_InspCalidad = Convert.ToBoolean(TbLLenar.Rows[0]["item_InspCal"]);
                req_InspObligatoria = Convert.ToBoolean(TbLLenar.Rows[0]["item_InspObligatoria"]);
                if (req_InspCalidad == true)
                {
                    chk_InspCalidad.Checked = true;
                }
                else
                {
                    chk_InspCalidad.Checked = false;
                }
                if (req_InspObligatoria == true)
                {
                  chk_InspObligatoria.Checked = true;
                }
                else
                {
                    chk_InspObligatoria.Checked = false;
                }

                if (cboTipoOrdenIa.Items.FindByValue(TbLLenar.Rows[0]["tipo_orden_prod_id"].ToString()) != null)
                {
                    cboTipoOrdenIa.SelectedValue = TbLLenar.Rows[0]["tipo_orden_prod_id"].ToString().Trim();
                }
                else
                {
                    lblmsjTipoOrdenIa.Text = TbLLenar.Rows[0]["abreviatura"].ToString();
                }
                if (chkListaDisponiblesIa.Items[1].Selected)
                {
                    trcomboparametros.Visible = true; tdrequiereItem.Visible = true; trgridparametros.Visible = true;
                }
                DataTable parametros = new DataTable();
                parametros = cmIp.CargarTablaParametro(p);
                grdParametrosIa.DataSource = parametros;
                grdParametrosIa.DataBind();
                Session.Add("Tb_Parametros", parametros);
                DataTable TbLLenar2 = new DataTable();
                TbLLenar2 = cmIp.ConsultarItemcriterios(Convert.ToInt64(p));
                if (TbLLenar2.Rows.Count != 0)
                {
                    if (cboPlan1Ip.Items.FindByValue(TbLLenar2.Rows[0]["cod_plan1"].ToString()) != null && !String.IsNullOrEmpty(TbLLenar2.Rows[0]["cod_plan1"].ToString().Trim()))
                    {
                        cboPlan1Ip.SelectedValue = TbLLenar2.Rows[0]["cod_plan1"].ToString().Trim();
                        cboPlan1Ip_SelectedIndexChanged(cboPlan1Ip, e);
                    }
                    if (cboPlan2Ip.Items.FindByValue(TbLLenar2.Rows[0]["cod_plan2"].ToString()) != null && !String.IsNullOrEmpty(TbLLenar2.Rows[0]["cod_plan2"].ToString().Trim()))
                    {
                        cboPlan2Ip.SelectedValue = TbLLenar2.Rows[0]["cod_plan2"].ToString().Trim();
                        cboPlan2Ip_SelectedIndexChanged(cboPlan2Ip, e);
                    }
                    if (cboPlan3Ip.Items.FindByValue(TbLLenar2.Rows[0]["cod_plan3"].ToString()) != null && !String.IsNullOrEmpty(TbLLenar2.Rows[0]["cod_plan3"].ToString().Trim()))
                    {
                        cboPlan3Ip.SelectedValue = TbLLenar2.Rows[0]["cod_plan3"].ToString().Trim();
                        cboPlan3Ip_SelectedIndexChanged(cboPlan3Ip, e);
                        txtPosAranIp.Text = TbLLenar2.Rows[0]["pos_arancelaria"].ToString();
                    }
                    if (cboPlan4Ip.Items.FindByValue(TbLLenar2.Rows[0]["cod_plan4"].ToString()) != null && !String.IsNullOrEmpty(TbLLenar2.Rows[0]["cod_plan4"].ToString().Trim()))
                    {
                        cboPlan4Ip.SelectedValue = TbLLenar2.Rows[0]["cod_plan4"].ToString().Trim();
                        cboPlan4Ip_SelectedIndexChanged(cboPlan4Ip, e);
                    }
                    if (cboPlan5Ip.Items.FindByValue(TbLLenar2.Rows[0]["cod_plan5"].ToString()) != null && !String.IsNullOrEmpty(TbLLenar2.Rows[0]["cod_plan5"].ToString().Trim()))
                    {
                        string a = TbLLenar2.Rows[0]["cod_plan5"].ToString().Trim();
                        cboPlan5Ip.SelectedValue = TbLLenar2.Rows[0]["cod_plan5"].ToString().Trim();                    
                    }
                }
                txtobservestado.Text = cmIp.BuscarEstadoItem(Convert.ToInt64(p));
                bool activo = (bool)TbLLenar.Rows[0]["activo"];
                if (activo)
                {
                    lblactivoIp.Text = "Activo";
                }
                else
                {
                    lblactivoIp.Text = "Inactivo";
                }
            }

            cmIp.CerrarConexion();
        }



        protected void btnEnviarIp_Click(object sender, EventArgs e)
        {
            btnEnviarIp.Enabled = false;
            Page.Validate("grupIp");
            System.Threading.Thread.Sleep(1000);
            string mensaje = "";
            //if (Page.IsValid)
            //{
            //    if (!Session["item_planta_id"].ToString().Equals("0"))
            //    {
            //        ActualizarEstado(Session["item_planta_id"].ToString(), e);
            //    }
            //    else
            //    {
            //        mensajeVentana("Item no enviado. Por favor cargue nuevamente el item.");
            //    }
            //}
            //else 
            //{
            mensaje = validarCamposObligatorios();
            if (String.IsNullOrEmpty(mensaje.Trim()))
            {
                string ti = Session["item_planta_id"].ToString();
                if (!Session["item_planta_id"].ToString().Equals("0"))
                {
                    ActualizarEstado(Session["item_planta_id"].ToString(), e);
                    btnEnviarIp.Enabled = true;
                }
                else
                {
                    mensajeVentana("Item no enviado. Por favor cargue nuevamente el item.");
                    btnEnviarIp.Enabled = true;
                }
            }

            else
            {
                mensajeVentana(mensaje);
                btnEnviarIp.Enabled = true;
            }
            //}            
        }

        private string validarCamposObligatorios()
        {
            string mensaje = "", msjIni = "Para crear el item hacen falta los siguientes parámetros: ";
            if (cboPerfilIp.SelectedItem.Value.Equals("1"))
            {
                if (cboAdicionalIp.SelectedItem.Value == " " && cboOrdenIp.SelectedItem.Value == " ")
                {
                    mensaje += "Factor para unidad de medida, ";
                }
            }

            if (cboPerfilIp.SelectedItem.Value.Equals("1") || cboPerfilIp.SelectedItem.Value.Equals("3"))
            {
                if (cboGrupoIa.SelectedValue == " " || cboGrupoIa.SelectedIndex == 0)
                {
                    mensaje += "Grupo (Item Forsa), ";
                }

                if (String.IsNullOrEmpty(txtDescripcionIa.Text))
                {
                    mensaje += "Item Forsa (Item Forsa), ";
                }

                if (cboGrupoIp.SelectedValue == " " || cboGrupoIp.SelectedIndex == 0)
                {
                    mensaje += "Grupo (Item Planta), ";
                }
                if (cboClaseItem.SelectedItem.Value == " ")
                {
                    mensaje += "Clase Item, ";
                }

                if (cboAgrupadorIp.SelectedValue == " " || cboAgrupadorIp.SelectedIndex == 0)
                {
                    mensaje += "Item Forsa (Item Planta), ";
                }

                if (String.IsNullOrEmpty(txtGruPlantaIp.Text))
                {
                    mensaje += "Grupo Planta, ";
                }

                if (cboGrupimpIp.SelectedValue == " " || cboGrupimpIp.SelectedIndex == 0 || cboGrupimpIp.SelectedValue.Trim() == "0")
                {
                    mensaje += "Grupo impositivo, ";
                }
            }
            if (cboClaseItem.SelectedItem.Value == " ")
            {
                mensaje += "Clase Item, ";
            }

            if (cboPlantaIp.SelectedValue == " " || cboPlantaIp.SelectedIndex == 0)
            {
                mensaje += "Planta, ";
            }

            if (String.IsNullOrEmpty(txtDscIp.Text))
            {
                mensaje += "Item Planta, ";
            }

            if (String.IsNullOrEmpty(cbo_Bodega.SelectedValue) || cbo_Bodega.SelectedIndex == 0)
            {
                mensaje += "Bodega, ";
            }
                      
            if (String.IsNullOrEmpty(txtDscAbrvIp.Text))
            {
                mensaje += "Descripción abreviada, ";
            }

            if (!validaIdiomas())
            {
                AccordionPrincipalIp.SelectedIndex = 0;
                mensaje += "Idioma, ";
            }

            List<ListItem> selected = new List<ListItem>();
            foreach (ListItem item in chkListorigen.Items)
                if (item.Selected) selected.Add(item);

            if (selected.Count == 0)
            {
                mensaje += "Origen, ";
            }

            if (cboTipInvIp.SelectedValue == " " || cboTipInvIp.SelectedIndex == 0)
            {
                mensaje += "Tipo de inventario, ";
            }

            if (cboPrincipalIp.SelectedValue == " " || cboPrincipalIp.SelectedIndex == 0)
            {
                mensaje += "Principal, ";
            }

            bool disp_ingenieria = chkListaDisponiblesIa.Items[3].Selected;

            if (disp_ingenieria == true)
            {
                if (String.IsNullOrEmpty(txt_NombAcce.Text))
                {
                    mensaje += "Dimensiones Accesorio, ";
                }
            }

            if (cboTipInvIp.SelectedValue == "PT3200" && cboPlantaIp.SelectedValue != "2")
            {
                if ((cboPlan4Ip.SelectedValue == " " || cboPlan4Ip.SelectedIndex == 0) || cboPlan5Ip.SelectedValue == " " || cboPlan5Ip.SelectedIndex == 0)
                {
                    mensaje += "Plan 4 y Plan 5, ";
                }              
            }

            if (((cboPlan4Ip.SelectedValue == " " || cboPlan4Ip.SelectedIndex == 0) || cboPlan5Ip.SelectedValue == " " || cboPlan5Ip.SelectedIndex == 0) &&( cboPlantaIp.SelectedValue != "2" || cboPlantaIp.SelectedValue != "11"))
            {
                mensaje += "Plan 4 y Plan 5, ";
            }
            //if (cboPerfilIp.SelectedItem.Value.Equals("1"))
            //{
            //    if (cboAdicionalIp.SelectedItem.Value == " " && cboOrdenIp.SelectedItem.Value == " ")
            //    {
            //        mensaje += "Factor para unidad de medida, ";
            //    }
            //}

            //if (String.IsNullOrEmpty(txtPrecioPlenoIp.Text)) 
            //{
            //    mensaje += "Costo, ";
            //}

            if (!String.IsNullOrEmpty(mensaje))
            {
                mensaje = msjIni + mensaje;
                mensaje = mensaje.Substring(0, mensaje.Length - 2);
            }
         
            return mensaje;
        }

        private void ActualizarEstado(String iplanta, EventArgs e)
        {
            bool auto_aprob = false;
            string mensaje; string msj = "";
            string Nombre = (string)Session["Nombre_Usuario"];
            string CorreoUsuario = (string)Session["rcEmail"]; string correoSistema = (string)Session["CorreoSistema"];
            if (cboPerfilIp.SelectedItem.Value.Equals("1"))
            {
                reader = cmIp.ConsultarAutoAprobador((String)Session["usuario"]);
                if (reader.HasRows)
                {
                    reader.Read();
                    auto_aprob = Convert.ToBoolean(reader.GetValue(0));
                }
                reader.Close();
                reader.Dispose();
                cmIp.CerrarConexion();
                if (auto_aprob) // estado sera siempre aprobado 
                {
                    EnviarWebService(iplanta, txtDscIp.Text, txtDscAbrvIp.Text, e);
                }
                //En caso de no ser  auto aprobador
                else
                {
                    //Se actualiza estado de itemplanta_rel_estado
                    // OJO PREAPROBADO AUTOMATICO ESTABA EN 2 EL ESTADO, SE PASO A 9
                    mensaje = cmIp.ActualizarRelEstado(Convert.ToInt64(iplanta), 9, "", Session["usuario"].ToString());
                    if (mensaje.Equals("OK"))
                    {
                        Debug.WriteLine("Se actualizo con exito en la tabla itemplanta_rel_estado"); mensajeVentana("Item planta ha sido Solicitado!");
                        // se envia el correo                   
                        cmIp.enviarCorreo(21, Convert.ToInt32(iplanta), Nombre, correoSistema, out msj, CorreoUsuario);
                        if (String.IsNullOrEmpty(msj))
                        {
                            Debug.WriteLine("Correo Fue enviado");

                        }
                        else
                        {
                            Debug.WriteLine("Correo no fue enviado");

                        }

                    }
                    else { Debug.WriteLine(mensaje); }
                    //Insertar en bitacora_itemplanta_rel_estado
                    mensaje = cmIp.InsertarBitacoraEstado(Convert.ToInt64(iplanta), 2, Session["usuario"].ToString(), "");
                    if (mensaje.Equals("OK"))
                    { Debug.WriteLine("Se inserto con exito en la tabla bitacora_itemplanta_rel_estado"); }
                    else { Debug.WriteLine(mensaje); }

                    btnAprobarIp.Visible = false;
                    btnDevolverIp.Visible = false;
                    btnRechazarIp.Visible = false;
                    btnEnviarIp.Visible = false;
                    btnGuardarIp.Visible = false;

                }
            }
            //if (cboPerfilIp.SelectedItem.Value.Equals("2"))
            //{

            //    //Se actualiza estado de itemplanta_rel_estado
            //    mensaje = cmIp.ActualizarRelEstado(Convert.ToInt64(iplanta), 2, "", Session["usuario"].ToString());
            //    if (mensaje.Equals("OK"))
            //    {
            //        Debug.WriteLine("Se actualizo con exito en la tabla itemplanta_rel_estado"); mensajeVentana("Item planta ha sido Solicitado!");
            //        cmIp.enviarCorreo(21, Convert.ToInt32(iplanta), Nombre, correoSistema, out msj, CorreoUsuario);
            //        if (string.IsNullOrEmpty(msj))
            //        {
            //            Debug.WriteLine("Correo Fue enviado");
            //        }
            //        else
            //        {
            //            Debug.WriteLine("Correo no fue enviado");
            //        }
            //    }
            //    else { Debug.WriteLine(mensaje); }
            //    //Insertar en bitacora_itemplanta_rel_estado
            //    mensaje = cmIp.InsertarBitacoraEstado(Convert.ToInt64(iplanta), 2, Session["usuario"].ToString(), "");
            //    if (mensaje.Equals("OK"))
            //    { Debug.WriteLine("Se inserto con exito en la tabla bitacora_itemplanta_rel_estado"); }
            //    else { Debug.WriteLine(mensaje); }
            //    btnAprobarIp.Visible = false;
            //    btnDevolverIp.Visible = false;
            //    btnRechazarIp.Visible = false;
            //    btnEnviarIp.Visible = false;
            //    btnGuardarIp.Visible = false;

            //}
            //if (cboPerfilIp.SelectedItem.Value.Equals("3"))
            //{

            //    //Se actualiza estado de itemplanta_rel_estado
            //    mensaje = cmIp.ActualizarRelEstado(Convert.ToInt64(iplanta), 9, "", Session["usuario"].ToString());
            //    if (mensaje.Equals("OK"))
            //    {
            //        Debug.WriteLine("Se actualizo con exito en la tabla itemplanta_rel_estado"); mensajeVentana("Item planta ha sido Pre-Aprobado!");
            //        cmIp.enviarCorreo(22, Convert.ToInt32(iplanta), Nombre, correoSistema, out msj, CorreoUsuario);
            //        if (string.IsNullOrEmpty(msj))
            //        {
            //            Debug.WriteLine("Correo Fue enviado");
            //        }
            //        else
            //        {
            //            Debug.WriteLine("Correo no fue enviado");
            //        }
            //    }
            //    else { Debug.WriteLine(mensaje); }
            //    //Insertar en bitacora_itemplanta_rel_estado
            //    mensaje = cmIp.InsertarBitacoraEstado(Convert.ToInt64(iplanta), 9, Session["usuario"].ToString(), "");
            //    if (mensaje.Equals("OK"))
            //    { Debug.WriteLine("Se inserto con exito en la tabla bitacora_itemplanta_rel_estado"); }
            //    else { Debug.WriteLine(mensaje); }
            //    btnAprobarIp.Visible = false;
            //    btnDevolverIp.Visible = false;
            //    btnRechazarIp.Visible = false;
            //    btnEnviarIp.Visible = false;
            //    btnGuardarIp.Visible = false;
            //    btnLimpiarIp.Visible = false;

            //}

            else if (cboPerfilIp.SelectedItem.Value.Equals("2") || cboPerfilIp.SelectedItem.Value.Equals("3") || cboPerfilIp.SelectedItem.Value.Equals("0"))
            {
                //Se actualiza estado de itemplanta_rel_estado
                mensaje = cmIp.ActualizarRelEstado(Convert.ToInt64(iplanta), 9, "", Session["usuario"].ToString());
                if (mensaje.Equals("OK"))
                {
                    Debug.WriteLine("Se actualizo con exito en la tabla itemplanta_rel_estado"); mensajeVentana("Item planta ha sido Pre-Aprobado!");
                    cmIp.enviarCorreo(22, Convert.ToInt32(iplanta), Nombre, correoSistema, out msj, CorreoUsuario);
                    if (string.IsNullOrEmpty(msj))
                    {
                        Debug.WriteLine("Correo Fue enviado");
                    }
                    else
                    {
                        Debug.WriteLine("Correo no fue enviado");
                    }
                }
                else { Debug.WriteLine(mensaje); }
                //Insertar en bitacora_itemplanta_rel_estado
                mensaje = cmIp.InsertarBitacoraEstado(Convert.ToInt64(iplanta), 9, Session["usuario"].ToString(), "");
                if (mensaje.Equals("OK"))
                { Debug.WriteLine("Se inserto con exito en la tabla bitacora_itemplanta_rel_estado"); }
                else { Debug.WriteLine(mensaje); }
                btnAprobarIp.Visible = false;
                btnDevolverIp.Visible = false;
                btnRechazarIp.Visible = false;
                btnEnviarIp.Visible = false;
                btnGuardarIp.Visible = false;
                btnLimpiarIp.Visible = false;
            }

            else { mensajeVentana("El usuario no tiene permisos para realizar esta operación"); }
            //Se debe enviar el correo
            cmIp.CerrarConexion();
        }

        protected void btnAprobarIp_Click(object sender, EventArgs e)
        {
            Page.Validate("grupIp");
            System.Threading.Thread.Sleep(1000);
            if (Page.IsValid)
            {
                if (validaIdiomas())
                {
                    if (cboPerfilIp.SelectedItem.Value.Equals("1"))
                    {
                        if (cboAdicionalIp.SelectedItem.Value == " " && cboOrdenIp.SelectedItem.Value == " ")
                        {
                            mensajeVentana("Debe ingresar un factor para unidad de medida");
                        }
                        else
                        {
                            EnviarWebService(Session["item_planta_id"].ToString(), txtDscIp.Text, txtDscAbrvIp.Text, e);
                        }
                    }
                    else
                    {
                        EnviarWebService(Session["item_planta_id"].ToString(), txtDscIp.Text, txtDscAbrvIp.Text, e);
                    }
                }
                else
                {
                    AccordionPrincipalIp.SelectedIndex = 0;
                    mensajeVentana("Debe diligenciar todos los Idiomas");
                }
            }
            else
            {
                mensajeVentana("Por favor diligenciar los campos obligatorios");
            }
        }

        private void EnviarWebService(string item_planta, string descripcion, string descripcion_corta, EventArgs e)
        {
            string Mensaje = ""; string Nombre = (string)Session["Nombre_Usuario"];
            string usuario = (string)Session["Usuario"];
            string CorreoUsuario = (string)Session["rcEmail"];
            string correoSistema = (string)Session["CorreoSistema"];
            string Linea = "", mensaje = "", Linea1 = "", LineaGpc = "", Linea2 = "", Linea3 = "", UndOrden = "    ", UndAdc = "    ", factAdc = "000000.0000", PesoAdc = "000000.0000", VolAdc = "000000.0000", factOrden = "000001.0000", PesoOrden = "000000.0000", VolOrden = "000000.0000";
            Int64 row_id = 0;
            string coderp = "", cod, cod_plan1, cod_plan2, cod_plan3, cod_plan4, cod_plan5, cod_plan6, cod_plan7="", cod_plan8="", pos_aranc;
            //Se genera la fecha en formato de 8 char
            DateTime thisDate1 = DateTime.Now;
            string fecha = thisDate1.ToString("yyyyMMdd");
            // Se debe buscar la cia que esta relacionada a la planta seleccionada
            string cia = cmIp.ConsultarCia(Convert.ToInt32(cboPlantaIp.SelectedItem.Value));
            string ciacompleto = cia.PadLeft(3, '0');
            ciacompleto = LimitLength(ciacompleto, 3);
            //  //Para la referencia

            /*CONSULTO EL ULTIMO ID en oracle*/
            Oreader = cmIp.ConsultarIDOracle();
            if (Oreader.HasRows)
            {
                Oreader.Read();
                string F120_ROWID = Oreader["F120_ROWID"].ToString();
                row_id = Convert.ToInt64(F120_ROWID);
                Oreader.Close();
                Oreader.Dispose();
            }

            string referencia = row_id.ToString();
            referencia = referencia.PadRight(50);
            referencia = LimitLength(referencia, 50);
            //para la descripcion del item 
            descripcion = descripcion.PadRight(40);
            descripcion = LimitLength(descripcion, 40);
            //para la descripcion corta del item 
            descripcion_corta = descripcion_corta.PadRight(20);
            descripcion_corta = LimitLength(descripcion_corta, 20);
            //para el grupo impositivo 
            string grupoimpo = cboGrupimpIp.SelectedItem.Value.ToString();
            grupoimpo = grupoimpo.PadRight(4);
            grupoimpo = LimitLength(grupoimpo, 4);
            //para el tipo de inventario 
            string tipoinv = cboTipInvIp.SelectedItem.Value.ToString();
            tipoinv = tipoinv.PadRight(10);
            tipoinv = LimitLength(tipoinv, 10);
            //para el unidad principal
            string UndPcpal = cboPrincipalIp.SelectedItem.Value.ToString();
            UndPcpal = UndPcpal.PadRight(4);
            UndPcpal = LimitLength(UndPcpal, 4);
            //para la bodega
            string bodega = cbo_Bodega.SelectedItem.Value.ToString();
            bodega = bodega.PadRight(5);
            bodega = LimitLength(bodega, 5);

            //Se comprueba si se envia unidad de orden o adicional en conjunto con su respectivo factor
            if (txtFactorLIp.Text != "")
            {
                UndOrden = cboOrdenIp.SelectedItem.Value.ToString();
                UndOrden = UndOrden.PadRight(4);
                UndOrden = LimitLength(UndOrden, 4);
                factOrden = Convert.ToDecimal(txtFactorLIp.Text).ToString();
                factOrden = factOrden.PadLeft(11, '0');
                factOrden = LimitLength(factOrden, 11);
                VolOrden = "000001.0000";
            }
            if (txtfactorunitario.Visible)
            {
                UndOrden = cboOrdenIp.SelectedItem.Value.ToString();
                UndOrden = UndOrden.PadRight(4);
                UndOrden = LimitLength(UndOrden, 4);
            }
            if (txtFactorM2Ip.Text != "")
            {
                UndAdc = cboAdicionalIp.SelectedItem.Value.ToString();
                UndAdc = UndAdc.PadRight(4);
                UndAdc = LimitLength(UndAdc, 4);
                factAdc = Convert.ToDecimal(txtFactorM2Ip.Text).ToString();
                factAdc = factAdc.PadLeft(11, '0');
                factAdc = LimitLength(factAdc, 11);
                UndOrden = cboPrincipalIp.SelectedItem.Value.ToString();
                UndOrden = UndOrden.PadRight(4);
                UndOrden = LimitLength(UndOrden, 4);
            }

            string complt1 = "0001";
            complt1 = complt1.PadLeft(8);
            complt1 = LimitLength(complt1, 8);
            string complt2 = "000000";
            complt2 = complt2.PadRight(42);
            complt2 = LimitLength(complt2, 42);
            string complt3 = "000000.0000000000.0000000000.000001";
            complt3 = complt3.PadLeft(39);
            complt3 = complt3.PadRight(47);
            complt3 = LimitLength(complt3, 47);
            string complt4 = "ITEM CREADO DESDE EL SIO POR " + usuario;
            complt4 = complt4.PadRight(255);
            complt4 = complt4 + '0';
            complt4 = complt4.PadRight(266);
            complt4 = complt4 + "00";
            complt4 = LimitLength(complt4, 268);
            //Se une toda información necesaria para importar el item
            Linea = ciacompleto + "00000000" + referencia + descripcion + descripcion_corta + grupoimpo + tipoinv + complt1 + Convert.ToInt16(chkListusoIp.Items[0].Selected) + Convert.ToInt16(chkListusoIp.Items[1].Selected) + Convert.ToInt16(chkListusoIp.Items[2].Selected) + complt2 + UndPcpal + "000000.0000000000.0000" + UndAdc + factAdc + PesoAdc + VolAdc + UndOrden + factOrden + PesoOrden + VolOrden + complt3 + fecha + complt4;
            //////Se crea el archivo xml que sera enviado para ser importado
            // esta es la conexion pruebas
            //string importar = "<?xml version='1.0' encoding='utf-8'?>"
            //        + "<Importar>"
            //        + "<NombreConexion>prueba4</NombreConexion>"
            //        + "<IdCia>" + cia + "</IdCia>"
            //        + "<Usuario>samuelleon</Usuario>"
            //        + "<Clave>SL2070915</Clave>"
            //        + "<Datos>"
            //        + "<Linea>000000100000001" + ciacompleto + "</Linea>"
            //        + "<Linea>000000201200005" + Linea + "</Linea>"
            //        + "<Linea>000000399990001" + ciacompleto + "</Linea>"
            //        + "</Datos>"
            //        + "</Importar>";
            // esta es la conexion real
            string importar = "<?xml version='1.0' encoding='utf-8'?>"
                   + "<Importar>"
                   + "<NombreConexion>FORSA</NombreConexion>"
                   + "<IdCia>" + cia + "</IdCia>"
                   + "<Usuario>siif</Usuario>"
                   + "<Clave>SiifErp</Clave>"
                   + "<Datos>"
                   + "<Linea>000000100000001" + ciacompleto + "</Linea>"
                   + "<Linea>000000201200005" + Linea + "</Linea>"
                   + "<Linea>000000399990001" + ciacompleto + "</Linea>"
                   + "</Datos>"
                   + "</Importar>";

            //Validar errores-------------------
            System.Data.DataSet nodes = null;
            string stMsgErp = null;
            string origen1 = "Maestro_Item_";
            string origen2 = "Insertar_Item_ERP";
            //----------------------------------
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            short x = (short)0;
            com.siesacloud.wsforsa.WSUNOEE WSDL = new com.siesacloud.wsforsa.WSUNOEE();
            //UNOEE.WSUNOEE WSDL = new UNOEE.WSUNOEE();
            nodes = WSDL.ImportarXML(importar, ref x);


            Debug.WriteLine("Resultado del envio al web service  de item es:" + x);

            if (x != 0)
            {
                //Recuperar Mensaje del WS
                if (nodes != null)
                {
                    foreach (DataRow fila in nodes.Tables[0].Rows)
                    {
                        stMsgErp = stMsgErp + fila[6].ToString() + Environment.NewLine;
                    }

                    //Insertar en Logsiff el error
                    cmIp.RegistraError_Envio_WebServices(origen1 + origen2, stMsgErp);

                }
            }

            if (x == 0)
            {
                /*CONSULTO EL ULTIMO ID en oracle*/
                Oreader = cmIp.ConsultarIDOracle();
                if (Oreader.HasRows)
                {
                    Oreader.Read();
                    string F120_ROWID = Oreader["F120_ROWID"].ToString();
                    row_id = Convert.ToInt64(F120_ROWID);
                    Oreader.Close();
                    Oreader.Dispose();
                }
                ///*Consulta el  codigo  y referencia del item insertado  en oracle*/
                Oreader = cmIp.ConsultarItemOracle(row_id);
                if (Oreader.HasRows)
                {
                    Oreader.Read();
                    coderp = Oreader["F120_ID"].ToString().Trim();
                    //txtReferenciaIp.Text = Oreader["F120_REFERENCIA"].ToString().Trim();
                    Oreader.Close();
                    Oreader.Dispose();
                    //Captura de datos de ERP
                    coderp = coderp.PadRight(7);
                    coderp = LimitLength(coderp, 7);
                    referencia = coderp;
                    referencia = referencia.PadRight(50);
                    referencia = LimitLength(referencia, 50);
                    //Se une toda información necesaria para importar el item
                    Linea = ciacompleto + "1" + coderp + referencia + descripcion + descripcion_corta + grupoimpo + tipoinv + complt1 + Convert.ToInt16(chkListusoIp.Items[0].Selected) + Convert.ToInt16(chkListusoIp.Items[1].Selected) + Convert.ToInt16(chkListusoIp.Items[2].Selected) + complt2 + UndPcpal + "000000.0000000000.0000" + UndAdc + factAdc + PesoAdc + VolAdc + UndOrden + factOrden + PesoOrden + VolOrden + complt3 + fecha + complt4;
                    /******SE ACTUALIZA LA REFERENCIA****/
                    //////Se crea el archivo xml que sera enviado para ser importado
                    // conexion de pruebas
                    //string importupdate = "<?xml version='1.0' encoding='utf-8'?>"
                    //        + "<Importar>"
                    //        + "<NombreConexion>prueba4</NombreConexion>"
                    //        + "<IdCia>" + cia + "</IdCia>"
                    //        + "<Usuario>samuelleon</Usuario>"
                    //        + "<Clave>SL2070915</Clave>"
                    //        + "<Datos>"
                    //        + "<Linea>000000100000001" + ciacompleto + "</Linea>"
                    //        + "<Linea>000000201200005" + Linea + "</Linea>"
                    //        + "<Linea>000000399990001" + ciacompleto + "</Linea>"
                    //        + "</Datos>"
                    //        + "</Importar>";
                    // conexion real
                    string importupdate = "<?xml version='1.0' encoding='utf-8'?>"
                           + "<Importar>"
                           + "<NombreConexion>FORSA</NombreConexion>"
                           + "<IdCia>" + cia + "</IdCia>"
                           + "<Usuario>siif</Usuario>"
                           + "<Clave>SiifErp</Clave>"
                           + "<Datos>"
                           + "<Linea>000000100000001" + ciacompleto + "</Linea>"
                           + "<Linea>000000201200005" + Linea + "</Linea>"
                           + "<Linea>000000399990001" + ciacompleto + "</Linea>"
                           + "</Datos>"
                           + "</Importar>";
                    x = (short)0;
                    nodes = WSDL.ImportarXML(importupdate, ref x);
                    Debug.WriteLine("Resultado del envio al web service de  actualizar item es:" + x);

                    if (x != 0)
                    {
                        //Recuperar Mensaje del WS
                        if (nodes != null)
                        {
                            foreach (DataRow fila in nodes.Tables[0].Rows)
                            {
                                stMsgErp = stMsgErp + fila[6].ToString() + Environment.NewLine;
                            }

                            //Insertar en Logsiff el error
                            origen2 = "Actualizar_Item";
                            cmIp.RegistraError_Envio_WebServices(origen1 + origen2, stMsgErp);

                        }
                    }
                    if (x == 0)
                    {
                        txtCodErpIp.Text = coderp.Trim();
                        txtReferenciaIp.Text = referencia.Trim();
                        mensaje = cmIp.ActualizarCodErp(Convert.ToInt64(item_planta), txtCodErpIp.Text.Trim(), txtReferenciaIp.Text.Trim());
                        if (mensaje.Equals("OK"))
                        { Debug.WriteLine("Se actualizo con exito en la tabla item_planta"); }
                        else { Debug.WriteLine(mensaje); }
                        mensaje = cmIp.ActualizarRelEstado(Convert.ToInt64(item_planta), 5, txtobservestado.Text, Session["usuario"].ToString());
                        if (mensaje.Equals("OK"))
                        {
                            Debug.WriteLine("Se actualizo con exito en la tabla itemplanta_rel_estado");
                            //Se envia el correo 
                            cmIp.enviarCorreo(17, Convert.ToInt32(item_planta), Nombre, correoSistema, out Mensaje, CorreoUsuario); // item fue aprobado
                            if (string.IsNullOrEmpty(Mensaje))
                            {
                                Debug.WriteLine("Correo Fue enviado");
                            }
                            else
                            {
                                Debug.WriteLine("Correo no fue enviado");
                            }
                        }
                        else { Debug.WriteLine(mensaje); }
                        // se actualiza el activo  del item
                        string msg = cmIp.EstadoItemPlanta(Convert.ToInt64(item_planta), "", true);
                        if (msg.Equals("OK"))
                        { Debug.WriteLine("Se actualizo estado en la tabla item_planta"); }
                        else { Debug.WriteLine(msg); }
                        //Insertar en bitacora_itemplanta_rel_estado
                        mensaje = cmIp.InsertarBitacoraEstado(Convert.ToInt64(item_planta), 5, Session["usuario"].ToString(), txtobservestado.Text);
                        if (mensaje.Equals("OK"))
                        { Debug.WriteLine("Se inserto con exito en la tabla bitacora_itemplanta_rel_estado"); }
                        else { Debug.WriteLine(mensaje); }

                        // se trae la información necesaria para enviar los criterios relacionados al item

                        cod_plan1 = cboPlan1Ip.SelectedItem.Value;
                        cod_plan1 = cod_plan1.PadRight(3);
                        cod_plan1 = LimitLength(cod_plan1, 3);

                        cod_plan2 = cboPlan2Ip.SelectedItem.Value;
                        cod_plan2 = cod_plan2.PadRight(4);
                        cod_plan2 = LimitLength(cod_plan2, 4);

                        cod_plan3 = cboPlan2Ip.SelectedItem.Value;
                        cod_plan3 = cod_plan3.PadRight(3);
                        cod_plan3 = LimitLength(cod_plan3, 3);

                        cod_plan4 = cboPlan3Ip.SelectedItem.Value;
                        cod_plan4 = cod_plan4.PadRight(4);
                        cod_plan4 = LimitLength(cod_plan4, 4);

                        pos_aranc = txtPosAranIp.Text;
                        pos_aranc = pos_aranc.PadRight(14);
                        pos_aranc = LimitLength(pos_aranc, 14);
                        /* escribir los criterios que se van a enviar por debajo
                                              cuando*/
                        cod_plan5 = "EPO";
                        cod_plan6 = "ISO ";

                        //Evalua si existen item, en el combo
                        if (cboPlan4Ip.SelectedItem.Value != " ")                     
                        {
                            // cod_plan7 es realmente el plan4 y cod_plan8 es el plan5
                            cod_plan7 = cboPlan4Ip.SelectedItem.Value;
                            cod_plan7 = cod_plan7.PadRight(3);
                            cod_plan7 = LimitLength(cod_plan7, 3);

                            cod_plan8 = cboPlan5Ip.SelectedItem.Value;
                            cod_plan8 = cod_plan8.PadRight(4);
                            cod_plan8 = LimitLength(cod_plan8, 4);
                        }
                       
                        //============================================

                        //se comprueba si el item tiene relacionado un  crietio 95
                        if (cboGrupimpIp.SelectedItem.Value.Equals("0027"))
                        {
                            cod = "95";
                            cod = cod.PadRight(3);

                            tipoinv = tipoinv.TrimEnd();
                            if (tipoinv == "MP3102" || tipoinv == "MP3101" ||
                                tipoinv == "MP3201" || tipoinv == "MP3202")
                            {
                                if (cboPlan3Ip.SelectedItem.Value != " ")
                                {
                                    Linea1 = "<Linea>000000201250002" + ciacompleto + "0" + coderp + referencia + cod + "9501</Linea>";
                                    Linea1 += "<Linea>000000301250002" + ciacompleto + "0" + coderp + referencia + cod_plan1 + cod_plan2 + "</Linea>";
                                    Linea1 += "<Linea>000000401250002" + ciacompleto + "0" + coderp + referencia + cod_plan3 + cod_plan4 + "</Linea>";
                                    Linea1 += "<Linea>000000501250002" + ciacompleto + "0" + coderp + referencia + cod_plan5 + cod_plan6 + "</Linea>";
                                    Linea1 += "<Linea>000000699990001" + ciacompleto + "</Linea>";
                                }
                                else
                                {
                                    Linea1 = "<Linea>000000201250002" + ciacompleto + "0" + coderp + referencia + cod + "9501</Linea>";
                                    Linea1 += "<Linea>000000301250002" + ciacompleto + "0" + coderp + referencia + cod_plan1 + cod_plan2 + "</Linea>";
                                    Linea1 += "<Linea>000000401250002" + ciacompleto + "0" + coderp + referencia + cod_plan5 + cod_plan6 + "</Linea>";
                                    Linea1 += "<Linea>000000599990001" + ciacompleto + "</Linea>";
                                }
                            }
                            //else if (tipoinv == "AL3900" || tipoinv == "AT3800" || tipoinv == "CO3700" ||
                            //         tipoinv == "IF3500" || tipoinv == "IF3600" || tipoinv == "MP3104")
                            //{
                            //    Linea1 = "<Linea>000000201250002" + ciacompleto + "0" + coderp + referencia + cod + "9501</Linea>";
                            //    Linea1 += "<Linea>000000301250002" + ciacompleto + "0" + coderp + referencia + cod_plan5 + cod_plan6 + "</Linea>";
                            //    Linea1 += "<Linea>000000499990001" + ciacompleto + "</Linea>";
                            //}
                            else
                                if (cboPlan3Ip.SelectedItem.Value != " ")
                                {
                                    Linea1 = "<Linea>000000201250002" + ciacompleto + "0" + coderp + referencia + cod + "9501</Linea>";
                                    Linea1 += "<Linea>000000301250002" + ciacompleto + "0" + coderp + referencia + cod_plan1 + cod_plan2 + "</Linea>";
                                    Linea1 += "<Linea>000000401250002" + ciacompleto + "0" + coderp + referencia + cod_plan3 + cod_plan4 + "</Linea>";
                                    Linea1 += "<Linea>000000599990001" + ciacompleto + "</Linea>";
                                }
                                else
                                {
                                    Linea1 = "<Linea>000000201250002" + ciacompleto + "0" + coderp + referencia + cod + "9501</Linea>";
                                    Linea1 += "<Linea>000000301250002" + ciacompleto + "0" + coderp + referencia + cod_plan1 + cod_plan2 + "</Linea>";
                                    Linea1 += "<Linea>000000499990001" + ciacompleto + "</Linea>";
                                }
                        }
                        else
                        {
                            tipoinv = tipoinv.TrimEnd();
                            if (tipoinv == "MP3102" || tipoinv == "MP3101" ||
                                tipoinv == "MP3201" || tipoinv == "MP3202")
                            {
                                if (cboPlan3Ip.SelectedItem.Value != " ")
                                {
                                    //bueno
                                    Linea1 = "<Linea>000000201250002" + ciacompleto + "0" + coderp + referencia + cod_plan1 + cod_plan2 + "</Linea>";
                                    Linea1 += "<Linea>000000301250002" + ciacompleto + "0" + coderp + referencia + cod_plan3 + cod_plan4 + "</Linea>";
                                    Linea1 += "<Linea>000000401250002" + ciacompleto + "0" + coderp + referencia + cod_plan5 + cod_plan6 + "</Linea>";
                                    Linea1 += "<Linea>000000599990001" + ciacompleto + "</Linea>";
                                }
                                else
                                {
                                    Linea1 = "<Linea>000000201250002" + ciacompleto + "0" + coderp + referencia + cod_plan1 + cod_plan2 + "</Linea>";
                                    Linea1 += "<Linea>000000301250002" + ciacompleto + "0" + coderp + referencia + cod_plan5 + cod_plan6 + "</Linea>";
                                    Linea1 += "<Linea>000000499990001" + ciacompleto + "</Linea>";
                                }
                            }
                            else
                                tipoinv = tipoinv.TrimEnd();
                                if ((tipoinv == "PT3200" && cboPlantaIp.SelectedValue != "2") || 
                                     tipoinv == "AL3900" && cboPlantaIp.SelectedValue != "2")
                               {
                                if (cboPlan3Ip.SelectedItem.Value != " " && cboPlan5Ip.SelectedItem.Value != " ")
                                {
                                    //bueno
                                    Linea1 = "<Linea>000000201250002" + ciacompleto + "0" + coderp + referencia + cod_plan1 + cod_plan2 + "</Linea>";
                                    Linea1 += "<Linea>000000301250002" + ciacompleto + "0" + coderp + referencia + cod_plan3 + cod_plan4 + "</Linea>";
                                    Linea1 += "<Linea>000000401250002" + ciacompleto + "0" + coderp + referencia + cod_plan7 + cod_plan8 + "</Linea>";
                                    Linea1 += "<Linea>000000599990001" + ciacompleto + "</Linea>";
                                }                              
                            }
                            //else if (tipoinv == "AL3900" || tipoinv == "AT3800" || tipoinv == "CO3700" ||
                            //           tipoinv == "IF3500" || tipoinv == "IF3600" || tipoinv == "MP3104")
                            //{
                            //    Linea1 = "<Linea>000000201250002" + ciacompleto + "0" + coderp + referencia + cod_plan5 + cod_plan6 + "</Linea>";
                            //    Linea1 += "<Linea>000000399990001" + ciacompleto + "</Linea>";
                            //}
                            else
                                if (cboPlan3Ip.SelectedItem.Value != " ")
                                {

                                    Linea1 = "<Linea>000000201250002" + ciacompleto + "0" + coderp + referencia + cod_plan1 + cod_plan2 + "</Linea>";
                                    Linea1 += "<Linea>000000301250002" + ciacompleto + "0" + coderp + referencia + cod_plan3 + cod_plan4 + "</Linea>";
                                    Linea1 += "<Linea>000000499990001" + ciacompleto + "</Linea>";
                                }
                                else
                                {

                                    Linea1 = "<Linea>000000201250002" + ciacompleto + "0" + coderp + referencia + cod_plan1 + cod_plan2 + "</Linea>";
                                    Linea1 += "<Linea>000000399990001" + ciacompleto + "</Linea>";
                                }

                        }
                        //////Se crea el archivo xml que sera enviado para ser importado el criterio
                        //CONEXION PRUEBAS
                        //string importar_criterio = "<?xml version='1.0' encoding='utf-8'?>"
                        //        + "<Importar>"
                        //        + "<NombreConexion>prueba4</NombreConexion>"
                        //        + "<IdCia>" + cia + "</IdCia>"
                        //       + "<Usuario>samuelleon</Usuario>"
                        //       + "<Clave>SL2070915</Clave>"
                        //        + "<Datos>"
                        //        + "<Linea>000000100000001" + ciacompleto + "</Linea>"
                        //        + Linea1
                        //        + "</Datos>"
                        //        + "</Importar>";
                        //CONEXION REAL
                        string importar_criterio = "<?xml version='1.0' encoding='utf-8'?>"
                               + "<Importar>"
                               + "<NombreConexion>FORSA</NombreConexion>"
                               + "<IdCia>" + cia + "</IdCia>"
                              + "<Usuario>siif</Usuario>"
                              + "<Clave>SiifErp</Clave>"
                               + "<Datos>"
                               + "<Linea>000000100000001" + ciacompleto + "</Linea>"
                               + Linea1
                               + "</Datos>"
                               + "</Importar>";

                        x = (short)0;
                        nodes = WSDL.ImportarXML(importar_criterio, ref x);
                        Debug.WriteLine("Resultado del envio al web service  de criterio es:" + x);

                        if (x != 0)
                        {
                            //Recuperar Mensaje del WS
                            if (nodes != null)
                            {
                                foreach (DataRow fila in nodes.Tables[0].Rows)
                                {
                                    stMsgErp = stMsgErp + fila[6].ToString() + Environment.NewLine;
                                }

                                //Insertar en Logsiff el error
                                origen2 = "Envio_Criterios";
                                cmIp.RegistraError_Envio_WebServices(origen1 + origen2, stMsgErp);
                            }
                        }

                       
                        // ENVIO DE CRITERIO PLAN GPC
                        LineaGpc += "<Linea>000000201250002" + ciacompleto + "0" + coderp + referencia + cod_plan7 + cod_plan8 + "</Linea>";
                        LineaGpc += "<Linea>000000399990001" + ciacompleto + "</Linea>";

                        // ENVIO DE CRITERIO PLAN GPC
                        //CONEXION REAL
                        string importar_criterioGpc = "<?xml version='1.0' encoding='utf-8'?>"
                               + "<Importar>"
                               + "<NombreConexion>FORSA</NombreConexion>"
                               + "<IdCia>" + cia + "</IdCia>"
                              + "<Usuario>siif</Usuario>"
                              + "<Clave>SiifErp</Clave>"
                               + "<Datos>"
                               + "<Linea>000000100000001" + ciacompleto + "</Linea>"
                               + LineaGpc
                               + "</Datos>"
                               + "</Importar>";

                        x = (short)0;
                        nodes = WSDL.ImportarXML(importar_criterioGpc, ref x);
                        Debug.WriteLine("Resultado del envio al web service  de criterio es:" + x);

                        if (x != 0)
                        {
                            //Recuperar Mensaje del WS
                            if (nodes != null)
                            {
                                foreach (DataRow fila in nodes.Tables[0].Rows)
                                {
                                    stMsgErp = stMsgErp + fila[6].ToString() + Environment.NewLine;
                                }

                                //Insertar en Logsiff el error
                                origen2 = "Envio_Criterios";
                                cmIp.RegistraError_Envio_WebServices(origen1 + origen2, stMsgErp);
                            }
                        }


                        if (!txtPosAranIp.Text.Equals(""))
                        {

                            // se trae la información necesaria para enviar la posicion arancelaria del item
                            Linea2 = "<Linea>000000201330002" + ciacompleto + "0" + coderp + referencia + pos_aranc + "</Linea>";
                            //////Se crea el archivo xml que sera enviado para ser importado la posicion arancelaria
                            //CONEXION PRUEBAS
                            //string importar_pos = "<?xml version='1.0' encoding='utf-8'?>"
                            //        + "<Importar>"
                            //        + "<NombreConexion>prueba4</NombreConexion>"
                            //        + "<IdCia>" + cia + "</IdCia>"
                            //        + "<Usuario>samuelleon</Usuario>"
                            //          + "<Clave>SL2070915</Clave>"
                            //        + "<Datos>"
                            //        + "<Linea>000000100000001" + ciacompleto + "</Linea>"
                            //        + Linea2
                            //         + "<Linea>000000399990001" + ciacompleto + "</Linea>"
                            //        + "</Datos>"
                            //        + "</Importar>";
                            //CONEXION REAL
                            string importar_pos = "<?xml version='1.0' encoding='utf-8'?>"
                                   + "<Importar>"
                                   + "<NombreConexion>FORSA</NombreConexion>"
                                   + "<IdCia>" + cia + "</IdCia>"
                                   + "<Usuario>siif</Usuario>"
                                     + "<Clave>SiifErp</Clave>"
                                   + "<Datos>"
                                   + "<Linea>000000100000001" + ciacompleto + "</Linea>"
                                   + Linea2
                                    + "<Linea>000000399990001" + ciacompleto + "</Linea>"
                                   + "</Datos>"
                                   + "</Importar>";
                            x = (short)0;
                            nodes = WSDL.ImportarXML(importar_pos, ref x);
                            Debug.WriteLine("Resultado del envio al web service  de la pos arancelaria es:" + x);

                            if (x != 0)
                            {
                                //Recuperar Mensaje del WS
                                if (nodes != null)
                                {
                                    foreach (DataRow fila in nodes.Tables[0].Rows)
                                    {
                                        stMsgErp = stMsgErp + fila[6].ToString() + Environment.NewLine;
                                    }

                                    //Insertar en Logsiff el error
                                    origen2 = "Envio_Pos_Arancelaria";
                                    cmIp.RegistraError_Envio_WebServices(origen1 + origen2, stMsgErp);

                                }
                            }
                        }

                        string F_CAMPO = "", instalacion = "001", rotacion = "A", ciclo = "", parte = "000000000", parte1 = "", horizontal = "000", parte3 = "0000000000.00000000000000000000000.0000", parte4 = "000100.00000110000000000.00000000000000.0000000.000000", parte5 = "", parte6 = "011", parte7 = "", parte8 = "0000.000000.00", extension = "", tasa = "0000000000.000000";
                        F_CAMPO = F_CAMPO.PadRight(35);
                        F_CAMPO = LimitLength(F_CAMPO, 35);
                        ciclo = ciclo.PadRight(3);
                        ciclo = LimitLength(ciclo, 3);
                        parte1 = parte1.PadRight(8);
                        parte1 = LimitLength(parte1, 8);
                        parte5 = parte5.PadRight(44);
                        parte5 = LimitLength(parte5, 44);
                        parte7 = parte7.PadRight(36);
                        parte7 = LimitLength(parte7, 36);
                        extension = extension.PadRight(40);
                        extension = LimitLength(extension, 40);
                        // se trae la información necesaria para enviar la posicion arancelaria del item
                        Linea3 = "<Linea>000000201320003" + ciacompleto + "0" + F_CAMPO + instalacion + rotacion + rotacion + ciclo + UndPcpal + parte + parte1 + horizontal + parte3 + "0" + parte4 + parte5 + bodega + parte6 + parte7 + parte8 + coderp + referencia + extension + tasa + "</Linea>";
                        /*****SE ENVIA LA NUEVA CONEXION A ITEM PARAMETROS******/
                        //CONEXION DE PRUEBAS
                        //string importar_param = "<?xml version='1.0' encoding='utf-8'?>"
                        //           + "<Importar>"
                        //           + "<NombreConexion>prueba4</NombreConexion>"
                        //           + "<IdCia>" + cia + "</IdCia>"
                        //           + "<Usuario>samuelleon</Usuario>"
                        //             + "<Clave>SL2070915</Clave>"
                        //           + "<Datos>"
                        //           + "<Linea>000000100000001" + ciacompleto + "</Linea>"
                        //           + Linea3
                        //            + "<Linea>000000399990001" + ciacompleto + "</Linea>"
                        //           + "</Datos>"
                        //           + "</Importar>";
                        //CONEXION REAL
                        string importar_param = "<?xml version='1.0' encoding='utf-8'?>"
                                   + "<Importar>"
                                   + "<NombreConexion>FORSA</NombreConexion>"
                                   + "<IdCia>" + cia + "</IdCia>"
                                   + "<Usuario>siif</Usuario>"
                                     + "<Clave>SiifErp</Clave>"
                                   + "<Datos>"
                                   + "<Linea>000000100000001" + ciacompleto + "</Linea>"
                                   + Linea3
                                    + "<Linea>000000399990001" + ciacompleto + "</Linea>"
                                   + "</Datos>"
                                   + "</Importar>";
                        x = (short)0;
                        nodes = WSDL.ImportarXML(importar_param, ref x);
                        Debug.WriteLine("Resultado del envio al web service de los parametros es:" + x);

                        if (x != 0)
                        {
                            //Recuperar Mensaje del WS
                            if (nodes != null)
                            {
                                foreach (DataRow fila in nodes.Tables[0].Rows)
                                {
                                    stMsgErp = stMsgErp + fila[6].ToString() + Environment.NewLine;
                                }

                                //Insertar en Logsiff el error
                                origen2 = "Envio_Parametros";
                                cmIp.RegistraError_Envio_WebServices(origen1 + origen2, stMsgErp);

                            }
                        }
                        //cboPerfilIp_SelectedIndexChanged(cboPerfilIp, e);
                        //CargarDatos(item_planta, e);
                        lblactivoIp.Text = "Activo";
                        btnAprobarIp.Visible = false;
                        btnDevolverIp.Visible = false;
                        btnRechazarIp.Visible = false;
                        btnEnviarIp.Visible = false;
                        btnGuardarIp.Text = "Editar";
                        btnduplicar.Visible = true;
                        Consultar_Accesorios();
                        mensajeVentana("Item ha sido creado en el ERP");
                        //int result = cmIp.CrearItemBrasil(Convert.ToInt32(item_planta));
                    }
                }
            }

            else
            {
                Debug.WriteLine("No se logro insertar el item  en oracle");
                mensajeVentana("Item  no se logro crear en el ERP");
            }


            cmIp.CerrarConexion();

        }

        private string LimitLength(string source, int maxLength)
        {
            if (source.Length <= maxLength)
            {
                return source;
            }

            return source.Substring(0, maxLength);
        }


        private string buscarespeciales(string p)
        {

            if (p.Contains("'"))
            {
                p = p.Replace("'", "''");
            }
            return p;
        }


        protected void cboAgrupadorIp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboAgrupadorIp.SelectedIndex != 0)
            {
                CargarItemplanta();
                limpiarItem();
                CargarDatosItem(Convert.ToInt64(cboAgrupadorIp.SelectedValue));
                Session["item_id"] = cboAgrupadorIp.SelectedValue;
                lblDesc.Value = "";
                Debug.Write("item id" + Session["item_id"].ToString());
                if (lblactivoI.Text.Equals("Activo"))
                {
                    btnGuardarIa.Text = "Editar";
                }
                if (lblactivoI.Text.Equals("Inactivo"))
                {
                    btnGuardarIa.Visible = false;
                }
            }
            else
            {
                limpiarItem();
                Session["item_id"] = "0";
                lblDesc.Value = "";
                cboiplantacreados.Items.Clear();
                cboiplantacreados.Items.Add(new ListItem(" ", " "));
                cboiplantacreados.SelectedIndex = 0;
            }

        }

        private void CargarItemplanta()
        {
            cboiplantacreados.Items.Clear();
            if (!String.IsNullOrEmpty(cboAgrupadorIp.SelectedItem.Value) && cboAgrupadorIp.SelectedItem.Value != " ")
            {
                reader = cmIp.PoblarItemplanta(Convert.ToInt64(cboAgrupadorIp.SelectedItem.Value));
                cboiplantacreados.Items.Add(new ListItem("Seleccione el Item Planta", " "));
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        cboiplantacreados.Items.Add(new ListItem(reader.GetString(1), reader.GetInt64(0).ToString()));
                    }
                }

                cboiplantacreados.SelectedIndex = 0;
                reader.Close();
                reader.Dispose();
                cmIp.CerrarConexion();
            }
        }

        protected void txtFactorM2Ip_TextChanged(object sender, EventArgs e)
        {
            if (!IsNumeric(txtFactorM2Ip.Text))
            {
                txtFactorM2Ip.Text = "0";
                txtFactorM2Ip.Focus();
            }
            else
            {

                string[] stringArray = txtFactorM2Ip.Text.Split('.');
                int longitud = stringArray[0].Length;

                if (longitud <= 6)
                {
                    decimal factor = decimal.Parse(txtFactorM2Ip.Text);
                    factor = Math.Round(factor, 4, MidpointRounding.ToEven);
                    txtFactorM2Ip.Text = factor.ToString("N4", new CultureInfo("en-US"));
                    if (factor == 1 || factor == 0)
                    {
                        txtFactorM2Ip.Text = string.Empty;
                        mensajeVentana("Valor debe ser diferente de 0  y 1");
                    }
                }

                else
                {
                    mensajeVentana("Numero de enteros no puede exceder de 6");
                    txtFactorM2Ip.Text = "0";
                    txtFactorM2Ip.Focus();
                }


            }
        }

        protected void txtFactorLIp_TextChanged(object sender, EventArgs e)
        {
            if (!IsNumeric(txtFactorLIp.Text))
            {
                txtFactorLIp.Text = "";
                txtFactorLIp.Focus();
            }
            else
            {

                string[] stringArray = txtFactorLIp.Text.Split('.');
                int longitud = stringArray[0].Length;

                if (longitud <= 6)
                {
                    decimal factor = decimal.Parse(txtFactorLIp.Text);
                    factor = Math.Round(factor, 4, MidpointRounding.ToEven);
                    txtFactorLIp.Text = factor.ToString("N4", new CultureInfo("en-US"));
                    if (factor == 1 || factor == 0)
                    {
                        txtFactorLIp.Text = string.Empty;
                        mensajeVentana("Valor debe ser diferente de 0  y 1");
                    }
                }

                else
                {
                    mensajeVentana("Numero de enteros no puede exceder de 6");
                    txtFactorLIp.Text = "";
                    txtFactorLIp.Focus();
                }

            }
        }

        protected void btnDevolverIp_Click(object sender, EventArgs e)
        {
            string mensaje; string Msj; string Nombre = (string)Session["Nombre_Usuario"];
            string CorreoUsuario = (string)Session["rcEmail"]; string correoSistema = (string)Session["CorreoSistema"];
            System.Threading.Thread.Sleep(1000);
            mensaje = cmIp.ActualizarRelEstado(Convert.ToInt64(Session["item_planta_id"].ToString()), 3, txtobservestado.Text, Session["usuario"].ToString());
            if (mensaje.Equals("OK"))
            {
                Debug.WriteLine("Se actualizo con exito en la tabla itemplanta_rel_estado"); mensajeVentana("Item planta ha sido Rechazado!");
                btnAprobarIp.Visible = false;
                btnDevolverIp.Visible = false;
                btnRechazarIp.Visible = false;
                btnEnviarIp.Visible = false;
                btnGuardarIp.Visible = false;
                cmIp.enviarCorreo(18, Convert.ToInt32(Session["item_planta_id"].ToString()), Nombre, correoSistema, out Msj, CorreoUsuario);
                if (string.IsNullOrEmpty(Msj))
                {
                    Debug.WriteLine("Correo Fue enviado");
                }
                else
                {
                    Debug.WriteLine("Correo no fue enviado");
                }
            }
            else { Debug.WriteLine(mensaje); }
            //Insertar en bitacora_itemplanta_rel_estado
            mensaje = cmIp.InsertarBitacoraEstado(Convert.ToInt64(Session["item_planta_id"].ToString()), 3, Session["usuario"].ToString(), txtobservestado.Text);
            if (mensaje.Equals("OK"))
            { Debug.WriteLine("Se inserto con exito en la tabla bitacora_itemplanta_rel_estado"); }
            else { Debug.WriteLine(mensaje); }
        }

        protected void btnRechazarIp_Click(object sender, EventArgs e)
        {
            string Nombre = (string)Session["Nombre_Usuario"];
            string CorreoUsuario = (string)Session["rcEmail"]; string correoSistema = (string)Session["CorreoSistema"];
            string Msj = "";
            string mensaje;
            System.Threading.Thread.Sleep(1000);

            mensaje = cmIp.ActualizarRelEstado(Convert.ToInt64(Session["item_planta_id"].ToString()), 4, txtobservestado.Text, Session["usuario"].ToString());
            if (mensaje.Equals("OK"))
            {
                Obtener_CodigoId_Accesorio();
                cmIp.Actualizar_Estado_Accesorio(1, int.Parse(Session["item_planta_id"].ToString()), int.Parse(cboPlantaIp.SelectedValue.ToString()), int.Parse(LblIdAccesorio.Text));
                Debug.WriteLine("Se actualizo con exito en la tabla itemplanta_rel_estado"); mensajeVentana("Item planta ha sido Anulado!");
                btnAprobarIp.Visible = false;
                btnDevolverIp.Visible = false;
                btnRechazarIp.Visible = false;
                btnEnviarIp.Visible = false;
                btnGuardarIp.Visible = false;
                cmIp.enviarCorreo(19, Convert.ToInt32(Session["item_planta_id"].ToString()), Nombre, correoSistema, out Msj, CorreoUsuario);
                if (string.IsNullOrEmpty(Msj))
                {
                    Debug.WriteLine("Correo Fue enviado");
                }
                else
                {
                    Debug.WriteLine("Correo no fue enviado");
                }
            }
            else { Debug.WriteLine(mensaje); }
            //Insertar en bitacora_itemplanta_rel_estado
            mensaje = cmIp.InsertarBitacoraEstado(Convert.ToInt64(Session["item_planta_id"].ToString()), 4, Session["usuario"].ToString(), txtobservestado.Text);
            if (mensaje.Equals("OK"))
            { Debug.WriteLine("Se inserto con exito en la tabla bitacora_itemplanta_rel_estado"); }
            else { Debug.WriteLine(mensaje); }
        }

        /**************EVENTO PARA AUTOCOMPLETAR EL TEXBOX DESCRIPCION***************/
        protected void txtDescripcionIa_TextChanged(object sender, EventArgs e)
        {
            if (!lblDesc.Value.Equals(" ") && !String.IsNullOrEmpty(lblDesc.Value))
            {
                limpiarItem();
                CargarDatosItem(Convert.ToInt64(lblDesc.Value));
                Session["item_id"] = lblDesc.Value;
                lblDesc.Value = "";
                Debug.Write("item id" + Session["item_id"].ToString());
                if (lblactivoI.Text.Equals("Activo"))
                {
                    btnGuardarIa.Text = "Editar";
                    CargarItemForsa(e);
                }
                if (lblactivoI.Text.Equals("Inactivo"))
                {
                    btnGuardarIa.Visible = false;
                }
            }
            Debug.Write("item id" + Session["item_id"].ToString());
        }

        /**************EVENTO PARA AGREGAR PARAMETROS AL GRID***************/
        protected void btnAgregar(object sender, ImageClickEventArgs e)
        {
            if (IsValid)
            {

                bool existe = false;

                int a = grdParametrosIa.PageIndex;
                for (int i = 0; i < grdParametrosIa.PageCount; i++)
                {
                    grdParametrosIa.SetPageIndex(i);
                    //se valida que que no se encuentre repetido el valor
                    foreach (GridViewRow row in grdParametrosIa.Rows)
                    {
                        string lblparametroid = ((Label)grdParametrosIa.Rows[row.RowIndex].FindControl("lbltipoparametroid")).Text;
                        if (lblparametroid.Equals(cboParametroIa.SelectedItem.Value))
                        {
                            existe = true;
                        }

                    }
                }
                grdParametrosIa.SetPageIndex(a);
                if (existe)
                {
                    mensajeVentana("El Parametro " + cboParametroIa.SelectedItem.Text + " Ya existe");
                }
                else
                {
                    DataTable dt = Session["Tb_Parametros"] as DataTable;
                    string parametroid = cboParametroIa.SelectedItem.Value;
                    string Parametro = cboParametroIa.SelectedItem.Text;
                    DataRow row = dt.NewRow();
                    row["item_parametro_id"] = "";
                    row["item_tipo_parametro_id"] = parametroid;
                    row["Descripcion"] = Parametro;
                    dt.Rows.Add(row);
                    dt.AcceptChanges();
                    Session.Add("Tb_Parametros", dt);
                    Reload_tbParametros();
                    cmIp.CerrarConexion();
                }
                cboParametroIa.SelectedIndex = 0;

            }
            else
            {
                mensajeVentana("Por favor diligenciar los campos obligatorios");
            }

        }
        /**************EVENTO PARA CAMBIAR DE PAGINA EN EL GRIDVIEW PARAMETROS***************/
        protected void grdParametrosIa_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdParametrosIa.PageIndex = e.NewPageIndex;
            Reload_tbParametros();
        }
        /**************METODO PARA RECARGAR LA TABLA PARAMETROS***************/
        private void Reload_tbParametros()
        {
            grdParametrosIa.DataSource = Session["Tb_Parametros"] as DataTable;
            grdParametrosIa.DataBind();
        }
        /**************EVENTO PARA ELIMINAR FILAS DEL GRIDVIEW***************/
        protected void grdParametrosIa_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (btnGuardarIa.Text == "Guardar")
            {
                DataTable dt = Session["Tb_Parametros"] as DataTable;
                Label labelnum = ((Label)grdParametrosIa.Rows[e.RowIndex].FindControl("lblNumeroIa"));
                int num_consecutivo = int.Parse(labelnum.Text) - 1;
                DataRow dr = dt.Rows[num_consecutivo];
                dr.Delete();
                dt.AcceptChanges();
                Session.Add("Tb_Parametros", dt);
            }
            if (btnGuardarIa.Text == "Editar")
            {
                DataTable dt3 = Session["Tb_Parametros"] as DataTable;
                DataTable dt1 = Session["Tb_Parametros_Delete"] as DataTable;
                Label labelnum = ((Label)grdParametrosIa.Rows[e.RowIndex].FindControl("lblNumeroIa"));
                string parametro_id = ((Label)grdParametrosIa.Rows[e.RowIndex].FindControl("lblparametroid")).Text;
                string tipo_parametro = ((Label)grdParametrosIa.Rows[e.RowIndex].FindControl("lbltipoparametroid")).Text;

                int num_consecutivo = int.Parse(labelnum.Text) - 1;
                string msg = "";
                DataRow row = dt1.NewRow();
                if (!parametro_id.Equals(""))
                {
                    row["idborrarparametro"] = parametro_id;
                    dt1.Rows.Add(row);
                    msg = cmIp.BitacoraItemParametro(Convert.ToInt64(Session["item_id"].ToString()), Convert.ToInt32(tipo_parametro), Session["usuario"].ToString(), false);
                    if (msg == "OK")
                    { Debug.WriteLine("Se inserto con exito en la tabla bitacora_item_parametro"); }
                }
                DataRow dr = dt3.Rows[num_consecutivo];
                dr.Delete();

                dt3.AcceptChanges();
                Session.Add("Tb_Parametros_Delete", dt1);
                Session.Add("Tb_Parametros", dt3);
            }

            Reload_tbParametros();
            grdPrecioIp.EditIndex = -1;
        }
        /**************EVENTO PARA ALMACENAR LA INFORMACION***************/
        protected void btnGuardarIa_Click(object sender, EventArgs e)
        {
            bool result = false;
            Page.Validate("grupItem");
            if (Page.IsValid)
            {
                string descripcion = " ";
                string usu = Session["usuario"].ToString();
                descripcion = txtDescripcionIa.Text.ToUpper().Trim();
                descripcion = buscarespeciales(descripcion);
                //200618
               
                    if (btnGuardarIa.Text == "Guardar")
                    {
                        if (cmIp.Validar_Existencia_ItemForsa(descripcion).Rows.Count == 0)
                        {
                        string msj = " ", msj1 = "";
                        msj = cmIp.InsertarItemAgrupador(cboGrupoIa.SelectedItem.Value, descripcion);
                        if (msj.Substring(0, 1) != "E")
                        {
                            Debug.WriteLine("Se inserto con exito en la tabla item");
                            Session.Add("item_id", msj);
                            msj1 = cmIp.InsertarBitacoraItem(Convert.ToInt64(Session["item_id"].ToString()), cboGrupoIa.SelectedItem.Value, descripcion, usu, 1);
                            if (msj1 == "OK")
                            { Debug.WriteLine("Se inserto con exito en la tabla bitacora_item"); }
                            else { Debug.WriteLine(msj1); }
                            msj1 = cmIp.InsertarItemEstado(Convert.ToInt64(Session["item_id"].ToString()), usu);
                            if (msj1 == "OK")
                            { Debug.WriteLine("Se inserto con exito en la tabla item_rel_estado"); }
                            else { Debug.WriteLine(msj1); }
                            msj1 = cmIp.InsertarBitacoraItemEstado(Convert.ToInt64(Session["item_id"].ToString()), 5, usu);
                            if (msj1 == "OK")
                            { Debug.WriteLine("Se inserto con exito en la tabla bitacora_item_rel_estado"); }
                            else { Debug.WriteLine(msj1); }
                            mensajeVentana("Se ha creado correctamente!");
                            result = true;

                        }
                        else
                        {
                            mensajeVentana(msj);
                        }
                        }
                        else
                        {
                            mensajeVentana("ya existe un item con el mismo nombre, intente con uno diferente");
                            txtDescripcionIa.Focus();
                        }

                    }
              
                    if (btnGuardarIa.Text == "Editar")
                    {
                        string msj = " ", msj1 = "";
                        msj = cmIp.editarItem(Convert.ToInt64(Session["item_id"].ToString()), cboGrupoIa.SelectedItem.Value, descripcion);
                        if (msj == "OK")
                        {
                            Debug.WriteLine("Se inserto con exito en la tabla item");
                            msj1 = cmIp.InsertarBitacoraItem(Convert.ToInt64(Session["item_id"].ToString()), cboGrupoIa.SelectedItem.Value, descripcion, usu, 1);
                            if (msj1 == "OK")
                            { Debug.WriteLine("Se inserto con exito en la tabla bitacora_item"); }
                            else { Debug.WriteLine(msj1); }
                            msj1 = cmIp.EditarItemEstado(Convert.ToInt64(Session["item_id"].ToString()), usu, 5);
                            if (msj1 == "OK")
                            { Debug.WriteLine("Se inserto con exito en la tabla item_rel_estado"); }
                            else { Debug.WriteLine(msj1); }
                            msj1 = cmIp.InsertarBitacoraItemEstado(Convert.ToInt64(Session["item_id"].ToString()), 6, usu);
                            if (msj1 == "OK")
                            { Debug.WriteLine("Se inserto con exito en la tabla bitacora_item_rel_estado"); }
                            else { Debug.WriteLine(msj1); }
                            mensajeVentana("Se ha editado correctamente!");
                            result = true;
                        }
                        else
                        {
                            mensajeVentana(msj);
                        }
                    }
                    if (result)
                    {
                        limpiarItem();
                        CargarDatosItem(Convert.ToInt64(Session["item_id"]));
                        if (lblactivoI.Text.Equals("Activo"))
                        {
                            btnGuardarIa.Text = "Editar";
                            CargarItemForsa(e);
                        }
                        if (lblactivoI.Text.Equals("Inactivo"))
                        {
                            btnGuardarIa.Visible = false;
                        }
                    }
                    cmIp.CerrarConexion();
               
            }
            else
            {
                mensajeVentana("Por favor diligenciar los campos obligatorios");
            }                                
        }

        private void CargarDatosItem(long p)
        {
            DataTable item = cmIp.buscarItemAgrupador(p);
            DataTable parametros = new DataTable();
            txtDescripcionIa.Text = item.Rows[0]["descripcion"].ToString();
            if (cboGrupoIa.Items.FindByValue(item.Rows[0]["item_grupo_id"].ToString()) != null)
            {
                cboGrupoIa.SelectedValue = item.Rows[0]["item_grupo_id"].ToString();
            }
            else
            {
                lblmsjgropuIa.Text = item.Rows[0]["grupo_desc"].ToString();
            }
            bool activo = (bool)item.Rows[0]["activo"];
            if (activo)
            {
                lblactivoI.Text = "Activo";
            }
            else
            {
                lblactivoI.Text = "Inactivo";
            }
            cmIp.CerrarConexion();
        }

        private void limpiarItem()
        {
            txtDescripcionIa.Text = string.Empty;
            btnGuardarIa.Text = "Guardar";
            CargarGrupoItem();
            lblactivoI.Text = string.Empty;
        }
        public void CleanControl(ControlCollection controles)
        {
            foreach (Control control in controles)
            {
                if (control is CheckBoxList)
                    ((CheckBoxList)control).ClearSelection();
                else if (control is CheckBox)
                    ((CheckBox)control).Checked = false;
                CleanControl(control.Controls);
            }
        }


        /**************EVENTO PARA LIMPIAR LA PAGINA DE DATOS***************/
        protected void btnLimpiarIa_Click(object sender, EventArgs e)
        {
            limpiarItem();
            Session["item_id"] = "0";
            lblDesc.Value = "";
            btnGuardarIa.Visible = true;
        }

        protected void txtTrm_TextChanged(object sender, EventArgs e)
        {
            if (!IsNumeric(txtTrm.Text))
            {
                txtTrm.Text = "";
            }
            else
            {
                decimal n = decimal.Parse(txtTrm.Text);
                n = Math.Round(n, 2, MidpointRounding.ToEven);
                txtTrm.Text = n.ToString("N2", new CultureInfo("en-US"));
                if (n == 0)
                {
                    mensajeVentana("TRM deb ser diferente de cero");
                    txtTrm.Text = string.Empty;
                }
            }
        }


        protected void btnduplicar_Click(object sender, EventArgs e)
        {
            Page.Validate("grupIp");
            System.Threading.Thread.Sleep(1000);
            if (Page.IsValid)
            {
                if (validaIdiomas())
                {

                    if (cboPerfilIp.SelectedItem.Value.Equals("1"))
                    {
                        if (cboAdicionalIp.SelectedItem.Value == " " && cboOrdenIp.SelectedItem.Value == " ")
                        {
                            mensajeVentana("Debe ingresar un factor para unidad de medida");
                        }
                        else
                        {
                            Duplicar(e);
                        }
                    }
                    else
                    {
                        Duplicar(e);
                    }
                }
                else
                {
                    AccordionPrincipalIp.SelectedIndex = 0;
                    mensajeVentana("Debe diligenciar todos los Idiomas");

                }

            }
            else
            {
                mensajeVentana("Por favor diligenciar los campos obligatorios");
            }

        }
        private void Duplicar(EventArgs e)
        {
            bool resultado = false;
            bool req_inspCalidad, req_inspeObliga;
            txtPesoEmpaqueIa.Text = vacio(txtPesoEmpaqueIa.Text);
            txtPeso_unitario.Text = vacio(txtPeso_unitario.Text);
            txtCantidadEmpaqueIa.Text = vacio(txtCantidadEmpaqueIa.Text);
            txtLargo.Text = vacio(txtLargo.Text);
            txtAncho1.Text = vacio(txtAncho1.Text);
            txtAncho2.Text = vacio(txtAncho2.Text);
            txtAlto1.Text = vacio(txtAlto1.Text);
            txtAlto2.Text = vacio(txtAlto2.Text);
            bool tipo_kamban = chkkamban.Checked;
            bool disp_cotizacion = chkListaDisponiblesIa.Items[0].Selected;
            bool disp_comercial = chkListaDisponiblesIa.Items[1].Selected;
            bool disp_ingenieria = chkListaDisponiblesIa.Items[3].Selected;
            bool disp_almacen = chkListaDisponiblesIa.Items[4].Selected;
            bool disp_produccion = chkListaDisponiblesIa.Items[2].Selected;
            bool req_plano = chkListaRequiereIa.Items[0].Selected;
            bool req_tipo = chkListaRequiereIa.Items[2].Selected;
            bool req_modelo = chkListaRequiereIa.Items[1].Selected;

            if (chk_InspCalidad.Checked == true)
            {
                req_inspCalidad = true;
            }
            else
            {
                req_inspCalidad = false;
            }
            if (chk_InspObligatoria.Checked == true)
            {
                req_inspeObliga = true;
            }
            else
            {
                req_inspeObliga = true;
            }

            int tipo_orden_prod_id = int.Parse(cboTipoOrdenIa.SelectedValue);
            //Se capturan los datos necesarios para insertar en la tabla item_planta
            if (txtFactorLIp.Text != "")
            {
                resultado = InsertarItemPlanta(Convert.ToInt32(chkListorigen.SelectedValue), Convert.ToDecimal(txtPeso_unitario.Text), Convert.ToDecimal(txtFactorLIp.Text), 0,
                                               Convert.ToDecimal(txtPesoEmpaqueIa.Text), Convert.ToInt32(txtCantidadEmpaqueIa.Text), Convert.ToDecimal(txtLargo.Text),
                                               Convert.ToDecimal(txtAncho1.Text), Convert.ToDecimal(txtAncho2.Text), Convert.ToDecimal(txtAlto1.Text), 
                                               Convert.ToDecimal(txtAlto2.Text), tipo_kamban, disp_cotizacion, disp_comercial, disp_ingenieria, 
                                               disp_almacen, disp_produccion, req_plano, req_tipo, req_modelo, tipo_orden_prod_id, req_inspCalidad,
                                               req_inspeObliga, Convert.ToInt32(cboClaseItem.SelectedValue));
            }
            if (txtFactorM2Ip.Text != "")
            {
                resultado = InsertarItemPlanta(Convert.ToInt32(chkListorigen.SelectedValue), Convert.ToDecimal(txtPeso_unitario.Text), 0, 
                                               Convert.ToDecimal(txtFactorM2Ip.Text), Convert.ToDecimal(txtPesoEmpaqueIa.Text), Convert.ToInt32(txtCantidadEmpaqueIa.Text), 
                                               Convert.ToDecimal(txtLargo.Text), Convert.ToDecimal(txtAncho1.Text), Convert.ToDecimal(txtAncho2.Text), Convert.ToDecimal(txtAlto1.Text),
                                               Convert.ToDecimal(txtAlto2.Text), tipo_kamban, disp_cotizacion, disp_comercial, disp_ingenieria, disp_almacen, disp_produccion, req_plano,
                                               req_tipo, req_modelo, tipo_orden_prod_id, req_inspCalidad, req_inspeObliga, Convert.ToInt32(cboClaseItem.SelectedValue));
            }
            if (cboAdicionalIp.Enabled == false && txtfactorunitario.Visible)
            {
                resultado = InsertarItemPlanta(Convert.ToInt32(chkListorigen.SelectedValue), Convert.ToDecimal(txtPeso_unitario.Text), 1, 0,
                                               Convert.ToDecimal(txtPesoEmpaqueIa.Text), Convert.ToInt32(txtCantidadEmpaqueIa.Text),
                                               Convert.ToDecimal(txtLargo.Text), Convert.ToDecimal(txtAncho1.Text), Convert.ToDecimal(txtAncho2.Text),
                                               Convert.ToDecimal(txtAlto1.Text), Convert.ToDecimal(txtAlto2.Text), tipo_kamban, disp_cotizacion, disp_comercial, 
                                               disp_ingenieria, disp_almacen, disp_produccion, req_plano, req_tipo, req_modelo, tipo_orden_prod_id, req_inspCalidad,
                                               req_inspeObliga, Convert.ToInt32(cboClaseItem.SelectedValue));
            }

            if (!txtfactorunitario.Visible && txtFactorLIp.Text.Equals("") && txtFactorM2Ip.Text.Equals(""))
            {
                resultado = InsertarItemPlanta(Convert.ToInt32(chkListorigen.SelectedValue), Convert.ToDecimal(txtPeso_unitario.Text), 0, 0,
                                               Convert.ToDecimal(txtPesoEmpaqueIa.Text), Convert.ToInt32(txtCantidadEmpaqueIa.Text), Convert.ToDecimal(txtLargo.Text), 
                                               Convert.ToDecimal(txtAncho1.Text), Convert.ToDecimal(txtAncho2.Text), Convert.ToDecimal(txtAlto1.Text), 
                                               Convert.ToDecimal(txtAlto2.Text), tipo_kamban, disp_cotizacion, disp_comercial, disp_ingenieria, disp_almacen,
                                               disp_produccion, req_plano, req_tipo, req_modelo, tipo_orden_prod_id, req_inspCalidad,
                                               req_inspeObliga, Convert.ToInt32(cboClaseItem.SelectedValue));
            }

            if (resultado)
            {
                //mensajeVentana("Item se ha insertado correctamente!!");

                cboPerfilIp_SelectedIndexChanged(cboPerfilIp, e);
                CargarDatos(Session["item_planta_id"].ToString(), e);
                CargarIdiomaEditado(Session["item_planta_id"].ToString());
                CargarPrecioEditado(Session["item_planta_id"].ToString());
                ActualizarEstado(Session["item_planta_id"].ToString(), e);
                AccordionPrincipalIp.SelectedIndex = 0;
            }
        }

        protected void cboiplantacreados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboiplantacreados.SelectedIndex != 0)
            {
                Session["item_planta_id"] = cboiplantacreados.SelectedItem.Value;
                Debug.WriteLine("Item planta=" + Session["item_planta_id"].ToString());
                cboPerfilIp_SelectedIndexChanged(cboPerfilIp, e);
                string usu_select = " ", estado_select = " ";
                bool activo = false;
                reader = cmIp.ConsultarUsuEditar(Convert.ToInt64(Session["item_planta_id"].ToString()));
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        usu_select = reader.GetString(0);
                        estado_select = reader.GetInt32(1).ToString();
                        activo = reader.GetBoolean(2);
                    }
                }
                reader.Close();
                reader.Dispose();
                cmIp.CerrarConexion();
                if (cboPerfilIp.SelectedItem.Value.Equals("3"))
                {
                    if (!Session["usuario"].ToString().Equals(usu_select) && estado_select.Equals("2"))
                    {
                        CargarDatos((string)Session["item_planta_id"], e);
                        CargarIdiomaEditado(Session["item_planta_id"].ToString());
                        CargarPrecioEditado(Session["item_planta_id"].ToString());
                        trlblobservacion.Visible = true;
                        trtxtobservacion.Visible = true;
                        btnEnviarIp.Visible = true;
                        btnLimpiarIp.Visible = false;
                        btnGuardarIp.Text = "Editar";
                        btnGuardarIp.Visible = true;
                    }
                    if (!Session["usuario"].ToString().Equals(usu_select) && (estado_select.Equals("9")))
                    {
                        CargarDatos((string)Session["item_planta_id"], e);
                        CargarIdiomaEditado(Session["item_planta_id"].ToString());
                        CargarPrecioEditado(Session["item_planta_id"].ToString());
                        btnGuardarIp.Visible = false;
                        btnLimpiarIp.Visible = false;
                        trlblobservacion.Visible = true;
                        txtobservestado.Enabled = false;
                        trtxtobservacion.Visible = true;
                    }

                }
                if (cboPerfilIp.SelectedItem.Value.Equals("1"))
                {
                    if (!Session["usuario"].ToString().Equals(usu_select) && estado_select.Equals("9"))
                    {
                        CargarDatos((string)Session["item_planta_id"], e);
                        CargarIdiomaEditado(Session["item_planta_id"].ToString());
                        CargarPrecioEditado(Session["item_planta_id"].ToString());
                        AccordionPrincipalIp.SelectedIndex = 0;
                        trlblobservacion.Visible = true;
                        trtxtobservacion.Visible = true;
                        btnAprobarIp.Visible = true;
                        btnDevolverIp.Visible = true;
                        btnRechazarIp.Visible = true;
                        btnGuardarIp.Text = "Editar";
                    }
                    if (Session["usuario"].ToString().Equals(usu_select) && (estado_select.Equals("1") || estado_select.Equals("3")))
                    {
                        CargarDatos((string)Session["item_planta_id"], e);
                        CargarIdiomaEditado(Session["item_planta_id"].ToString());
                        CargarPrecioEditado(Session["item_planta_id"].ToString());
                        AccordionPrincipalIp.SelectedIndex = 0;
                        if (estado_select.Equals("3"))
                        {
                            trlblobservacion.Visible = true;
                            txtobservestado.Enabled = false;
                            trtxtobservacion.Visible = true;
                        }
                        btnGuardarIp.Text = "Editar";
                        btnEnviarIp.Visible = true;

                    }
                    if (!Session["usuario"].ToString().Equals(usu_select) && (estado_select.Equals("3") || estado_select.Equals("4") || estado_select.Equals("5")))
                    {
                        CargarDatos((string)Session["item_planta_id"], e);
                        CargarIdiomaEditado(Session["item_planta_id"].ToString());
                        CargarPrecioEditado(Session["item_planta_id"].ToString());
                        AccordionPrincipalIp.SelectedIndex = 0;
                        btnGuardarIp.Text = "Editar";
                        btnGuardarIp.Visible = true;
                        if (estado_select.Equals("5"))
                        {
                            btnduplicar.Visible = true;
                        }
                        if (estado_select.Equals("4"))
                        {
                            btnGuardarIp.Visible = false;
                        }
                        trlblobservacion.Visible = true;
                        txtobservestado.Enabled = false;
                        trtxtobservacion.Visible = true;
                    }
                    if (Session["usuario"].ToString().Equals(usu_select) && (estado_select.Equals("2") || estado_select.Equals("4") || estado_select.Equals("5") || estado_select.Equals("9")))
                    {
                        CargarDatos((string)Session["item_planta_id"], e);
                        CargarIdiomaEditado(Session["item_planta_id"].ToString());
                        CargarPrecioEditado(Session["item_planta_id"].ToString());
                        AccordionPrincipalIp.SelectedIndex = 0;
                        btnGuardarIp.Visible = false;

                        if (estado_select.Equals("5"))
                        {
                            btnGuardarIp.Text = "Editar";
                            btnGuardarIp.Visible = true;
                            btnduplicar.Visible = true;
                        }
                        trlblobservacion.Visible = true;
                        txtobservestado.Enabled = false;
                        trtxtobservacion.Visible = true;

                    }
                }
                if (cboPerfilIp.SelectedItem.Value.Equals("2") || cboPerfilIp.SelectedItem.Value.Equals("0"))
                {

                    if (Session["usuario"].ToString().Equals(usu_select) && (estado_select.Equals("1") || estado_select.Equals("3")))
                    {
                        CargarDatos((string)Session["item_planta_id"], e);
                        CargarIdiomaEditado(Session["item_planta_id"].ToString());
                        CargarPrecioEditado(Session["item_planta_id"].ToString());
                        AccordionPrincipalIp.SelectedIndex = 0;
                        if (estado_select.Equals("3"))
                        {
                            trlblobservacion.Visible = true;
                            txtobservestado.Enabled = false;
                            trtxtobservacion.Visible = true;
                        }
                        btnGuardarIp.Text = "Editar";
                        btnEnviarIp.Visible = true;
                    }
                    if (Session["usuario"].ToString().Equals(usu_select) && (estado_select.Equals("2") || estado_select.Equals("4") || estado_select.Equals("5") || estado_select.Equals("9")))
                    {
                        CargarDatos((string)Session["item_planta_id"], e);
                        CargarIdiomaEditado(Session["item_planta_id"].ToString());
                        CargarPrecioEditado(Session["item_planta_id"].ToString());
                        AccordionPrincipalIp.SelectedIndex = 0;
                        btnGuardarIp.Visible = false;
                        trlblobservacion.Visible = true;
                        txtobservestado.Enabled = false;
                        trtxtobservacion.Visible = true;
                    }
                    if (!Session["usuario"].ToString().Equals(usu_select))
                    {

                        ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "mensaje", "MensajeError( 'No posee permisos para editar este Item Planta!')", true);
                    }

                }
            }

        }

        protected void txt_valor_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = Session["TbPrecio"] as DataTable;
            try
            {
                TextBox thisTextBox = (TextBox)sender;
                GridViewRow currentRow = (GridViewRow)thisTextBox.Parent.Parent;
                int rowindex = 0;
                rowindex = currentRow.RowIndex;

                Label labelnum = (Label)currentRow.FindControl("lblnumPrecio");
                int num_consecutivo = int.Parse(labelnum.Text) - 1;
                string valor = ((TextBox)currentRow.FindControl("txt_valor")).Text;
                if (!IsNumeric(valor))
                {
                    valor = "";
                    dt.Columns["valor"].ReadOnly = false;
                    dt.Columns["valor"].MaxLength = 200;
                    dt.Rows[num_consecutivo]["valor"] = valor;
                }
                else
                {
                    decimal n = decimal.Parse(valor);
                    n = Math.Round(n, 2, MidpointRounding.ToEven);
                    valor = n.ToString("N2", new CultureInfo("en-US"));
                    dt.Columns["valor"].ReadOnly = false;
                    dt.Columns["valor"].MaxLength = 200;
                    dt.Rows[num_consecutivo]["valor"] = valor;

                }
                Session.Add("TbPrecio", dt);
                Reload_tbPrecio();

            }
            catch
            {
                Debug.WriteLine("Error no logro modificar valor de precio");
            }

        }


        protected void textDesc_TextChanged(object sender, EventArgs e)
        {
            TextBox thisTextBox = (TextBox)sender;
            if (thisTextBox.Text != "")
            {
                DataTable dt = Session["TabIdioma"] as DataTable;
                try
                {
                    foreach (GridViewRow row in grdIdiomaIp.Rows)
                    {

                        //string porcentaje = ((TextBox)grdPrecioIp.Rows[row.RowIndex].FindControl("txtporcentaje")).Text;
                        //decimal z = CalcularPorcentaje(decimal.Parse(txtPrecioPlenoIp.Text), decimal.Parse(porcentaje));
                        //dt.Columns["valor"].ReadOnly = false;
                        //dt.Columns["valor"].MaxLength = 200;
                        //dt.Rows[cont]["valor"] = z;
                        //cont++;

                        Label labelnum = (Label)row.FindControl("lblnumIdioma");
                        int num_consecutivo = int.Parse(labelnum.Text) - 1;
                        string strDescrp = ((TextBox)row.FindControl("textDesc")).Text;
                        dt.Columns["descripcion"].ReadOnly = false;
                        dt.Columns["descripcion"].MaxLength = 1000;
                        dt.Rows[num_consecutivo]["descripcion"] = strDescrp.ToUpper();
                        Session.Add("TabIdioma", dt);

                        TextBox texto = (TextBox)grdIdiomaIp.Rows[row.RowIndex].FindControl("textDesc");
                        texto.Focus();

                    }

                    Reload_tbIdioma();

                    //GridViewRow currentRow = (GridViewRow)thisTextBox.Parent.Parent;
                    //int rowindex = 0;
                    //rowindex = currentRow.RowIndex;
                    //Label labelnum = (Label)currentRow.FindControl("lblnumIdioma");
                    //int num_consecutivo = int.Parse(labelnum.Text) - 1;
                    //string strDescrp = ((TextBox)currentRow.FindControl("textDesc")).Text;
                    //dt.Columns["descripcion"].ReadOnly = false;
                    //dt.Columns["descripcion"].MaxLength = 1000;
                    //dt.Rows[num_consecutivo]["descripcion"] = strDescrp.ToUpper();
                    //Session.Add("TabIdioma", dt);
                    //Reload_tbIdioma();
                    //if (rowindex < grdIdiomaIp.Rows.Count - 1)
                    //{
                    //    rowindex += 1;
                    //}
                    //TextBox texto = (TextBox)grdIdiomaIp.Rows[rowindex].FindControl("textDesc");
                    //texto.Focus();

                }
                catch
                {
                    Debug.WriteLine("Error no logro modificar el idioma del item");
                }
            }
            else
            {
                thisTextBox.Focus();
            }

        }

        protected void txtAncho1_TextChanged(object sender, EventArgs e)
        {
            if (!IsNumeric(txtAncho1.Text))
            {
                txtAncho1.Text = "0";
                txtAncho1.Focus();
            }
            else
            {

                decimal n = decimal.Parse(txtAncho1.Text);
                n = Math.Round(n, 3, MidpointRounding.ToEven);
                txtAncho1.Text = n.ToString("N3", new CultureInfo("en-US"));
            }
        }
        /**************METODO PARA CARGAR EL COMBO DE UNIDAD DE TIPO ORDEN***************/
        protected void CargarTipoOrden()
        {
            cboTipoOrdenIa.Items.Clear();
            reader = cmIp.CargarTipoOrden();
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboTipoOrdenIa.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            cboTipoOrdenIa.SelectedIndex = 0;
            reader.Close();
            reader.Dispose();
            cmIp.CerrarConexion();
        }

        protected void txtAlto1_TextChanged(object sender, EventArgs e)
        {
            if (!IsNumeric(txtAlto1.Text))
            {
                txtAlto1.Text = "0";
                txtAlto1.Focus();
            }
            else
            {

                decimal n = decimal.Parse(txtAlto1.Text);
                n = Math.Round(n, 3, MidpointRounding.ToEven);
                txtAlto1.Text = n.ToString("N3", new CultureInfo("en-US"));
            }
        }

        protected void txtAncho2_TextChanged(object sender, EventArgs e)
        {
            if (!IsNumeric(txtAncho2.Text))
            {
                txtAncho2.Text = "0";
                txtAncho2.Focus();
            }
            else
            {

                decimal n = decimal.Parse(txtAncho2.Text);
                n = Math.Round(n, 3, MidpointRounding.ToEven);
                txtAncho2.Text = n.ToString("N3", new CultureInfo("en-US"));
            }
        }

        protected void txtAlto2_TextChanged(object sender, EventArgs e)
        {
            if (!IsNumeric(txtAlto2.Text))
            {
                txtAlto2.Text = "0";
                txtAlto2.Focus();
            }
            else
            {

                decimal n = decimal.Parse(txtAlto2.Text);
                n = Math.Round(n, 3, MidpointRounding.ToEven);
                txtAlto2.Text = n.ToString("N3", new CultureInfo("en-US"));
            }
        }

        protected void txtLargo_TextChanged(object sender, EventArgs e)
        {
            if (!IsNumeric(txtLargo.Text))
            {
                txtLargo.Text = "0";
                txtLargo.Focus();
            }
            else
            {

                decimal n = decimal.Parse(txtLargo.Text);
                n = Math.Round(n, 3, MidpointRounding.ToEven);
                txtLargo.Text = n.ToString("N3", new CultureInfo("en-US"));
            }
        }

        protected void txtCantidadEmpaqueIa_TextChanged(object sender, EventArgs e)
        {
            if (!IsNumeric(txtCantidadEmpaqueIa.Text))
            {
                txtCantidadEmpaqueIa.Text = "0";
            }
        }

        protected void txtPesoEmpaqueIa_TextChanged(object sender, EventArgs e)
        {
            if (!IsNumeric(txtPesoEmpaqueIa.Text))
            {
                txtPesoEmpaqueIa.Text = "0";
                txtPesoEmpaqueIa.Focus();
            }
            else
            {

                decimal n = decimal.Parse(txtPesoEmpaqueIa.Text);
                n = Math.Round(n, 3, MidpointRounding.ToEven);
                txtPesoEmpaqueIa.Text = n.ToString("N3", new CultureInfo("en-US"));
            }
        }

        protected void chkListaDisponiblesIa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkListaDisponiblesIa.Items[1].Selected)
            {
                trcomboparametros.Visible = true; tdrequiereItem.Visible = true; trgridparametros.Visible = true;
            }
            if (!chkListaDisponiblesIa.Items[1].Selected)
            {
                trcomboparametros.Visible = true; tdrequiereItem.Visible = true; trgridparametros.Visible = true;
            }
            if (!chkListaDisponiblesIa.Items[3].Selected)
            {
                Habilitar_Campos_Accesorio(false);
            }
            else
            {
                Habilitar_Campos_Accesorio(true);
            }


        }

        protected void btnlimpiarprecio_Click(object sender, ImageClickEventArgs e)
        {

            DataTable dt1 = Session["eliminados_precio"] as DataTable;
            DataTable dt3 = Session["TbPrecio"] as DataTable;
            for (int j = dt3.Rows.Count - 1; j >= 0; j--)
            {
                DataRow row = dt1.NewRow();
                DataRow dr = dt3.Rows[j];
                String iplanta_precio = dr["item_planta_precio_id"].ToString();
                if (iplanta_precio != "")
                {
                    row["item1"] = dr["item_planta_precio_id"];
                    dt1.Rows.Add(row);
                }

                dr.Delete();
            }
            dt3.AcceptChanges();
            Session.Add("eliminados_precio", dt1);
            Session.Add("TbPrecio", dt3);
            CargarMoneda();
        }

        protected void txtporcentaje_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = Session["TbPrecio"] as DataTable;
            try
            {
                TextBox thisTextBox = (TextBox)sender;
                GridViewRow currentRow = (GridViewRow)thisTextBox.Parent.Parent;
                int rowindex = 0;
                rowindex = currentRow.RowIndex;

                Label labelnum = (Label)currentRow.FindControl("lblnumPrecio");
                int num_consecutivo = int.Parse(labelnum.Text) - 1;
                string valor = ((TextBox)currentRow.FindControl("txtporcentaje")).Text;
                if (!IsNumeric(valor))
                {
                    valor = "";
                    dt.Columns["margen"].ReadOnly = false;
                    dt.Columns["margen"].MaxLength = 200;
                    dt.Rows[num_consecutivo]["margen"] = valor;
                }
                else
                {
                    decimal n = decimal.Parse(valor);
                    n = Math.Round(n, 3, MidpointRounding.ToEven);
                    valor = n.ToString("N3", new CultureInfo("en-US"));
                    dt.Columns["margen"].ReadOnly = false;
                    dt.Columns["margen"].MaxLength = 200;
                    dt.Rows[num_consecutivo]["margen"] = valor;

                }
                Session.Add("TbPrecio", dt);
                Reload_tbPrecio();

            }
            catch
            {
                Debug.WriteLine("Error no logro modificar valor de margen");
            }
        }

        protected void grdPrecioIp_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string valor = ((TextBox)e.Row.FindControl("txt_valor")).Text;
                if (!valor.Equals(""))
                {
                    e.Row.Cells[3].Enabled = false;
                }
                else
                {
                    e.Row.Cells[3].Enabled = true;
                }
            }

        }

        protected void btnCodigoERP_Click(object sender, EventArgs e)
        {
            if (btnCodigoERP.Text.Contains("Consulta"))
            {
                reader = cmIp.consultarNombreERP(txtCodErpIp.Text, Convert.ToInt32(cboPlantaIp.SelectedValue));
                if (reader.HasRows)
                {
                    reader.Read();
                    lblNombreERPTxt.Text = reader.GetValue(0).ToString();
                    btnCodigoERP.Text = "Actualizar de ERP";
                    btnCodigoERP.ToolTip = "Actualizar";
                    btnCodigoERP.Enabled = true;
                    lblNombreERP.Visible = true;
                    lblNombreERPTxt.Visible = true;
                }
                else
                {
                    lblNombreERPTxt.Text = "";
                    lblNombreERP.Visible = false;
                    lblNombreERPTxt.Visible = false;
                    txtCodErpIp.Text = "";
                    btnCodigoERP.Text = "Consulta a ERP";
                    btnCodigoERP.ToolTip = "Consultar";
                    btnCodigoERP.Enabled = true;
                    mensajeVentana("El código ERP no existe, intente nuevamente. Gracias!");
                }

                reader.Close();
                reader.Dispose();
                cmIp.CerrarConexion();
            }
            else if (btnCodigoERP.Text.Contains("Actualizar"))
            {
                if (Session["item_planta_id"] != null && !Session["item_planta_id"].ToString().Equals("0"))
                {
                    int item_planta_id = Convert.ToInt32(Session["item_planta_id"]);
                    string mensaje = "";
                    string Mensaje = ""; string Nombre = (string)Session["Nombre_Usuario"];
                    string CorreoUsuario = (string)Session["rcEmail"];
                    string correoSistema = (string)Session["CorreoSistema"];
                    mensaje = cmIp.actualizarDeERP(item_planta_id, txtCodErpIp.Text);
                    if (mensaje == "OK")
                    {
                        mensaje = cmIp.ActualizarRelEstado(Convert.ToInt64(item_planta_id), 5, txtobservestado.Text, Session["usuario"].ToString());
                        if (mensaje.Equals("OK"))
                        {
                            Debug.WriteLine("Se actualizo con éxito en la tabla itemplanta_rel_estado");
                            //Se envia el correo 
                            //cmIp.enviarCorreo(17, Convert.ToInt32(item_planta_id), Nombre, correoSistema, out Mensaje, CorreoUsuario); // item fue aprobado
                            //if (string.IsNullOrEmpty(Mensaje))
                            //{
                            //    Debug.WriteLine("Correo Fue enviado");
                            //}
                            //else
                            //{
                            //    Debug.WriteLine("Correo no fue enviado");
                            //}
                        }
                        else { Debug.WriteLine(mensaje); }
                        // se actualiza el activo  del item
                        string msg = cmIp.EstadoItemPlanta(Convert.ToInt64(item_planta_id), "", true);
                        if (msg.Equals("OK"))
                        { Debug.WriteLine("Se actualizo estado en la tabla item_planta"); }
                        else { Debug.WriteLine(msg); }
                        //Insertar en bitacora_itemplanta_rel_estado
                        mensaje = cmIp.InsertarBitacoraEstado(Convert.ToInt64(item_planta_id), 5, Session["usuario"].ToString(), txtobservestado.Text);
                        if (mensaje.Equals("OK"))
                        { Debug.WriteLine("Se inserto con exito en la tabla bitacora_itemplanta_rel_estado"); }
                        else { Debug.WriteLine(mensaje); }
                        CargarDatos((string)Session["item_planta_id"], e);
                        mensajeVentana("El item ha sido actualizado y aprobado de manera exitosa!");
                    }
                    else
                    {
                        lblNombreERPTxt.Text = "";
                        lblNombreERP.Visible = false;
                        lblNombreERPTxt.Visible = false;
                        txtCodErpIp.Text = "";
                        btnCodigoERP.Text = "Consulta a ERP";
                        btnCodigoERP.ToolTip = "Consultar";
                        btnCodigoERP.Enabled = true;
                        mensajeVentana("El item no pudo se actualizado, intente nuevamente. Gracias!");
                    }
                }
                else
                {
                    mensajeVentana("Ha ocurrido un error, vuelva a cargar el item desde el reporte. Gracias!");
                }
            }
        }

        protected void txtCodErpIp_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtCodErpIp.Text))
            {
                lblNombreERP.Visible = false;
                lblNombreERPTxt.Visible = false;
                lblNombreERPTxt.Text = "";
                btnCodigoERP.Text = "Consulta a ERP";
                btnCodigoERP.ToolTip = "Consultar";
                btnCodigoERP.Enabled = true;
            }
        }

        protected void chkCodigoERP_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCodigoERP.Checked)
            {
                txtCodErpIp.Enabled = true;
                btnCodigoERP.Visible = true;
                btnCodigoERP.Enabled = true;

                if (Session["PlantaID"].ToString() == "3")
                {
                    btnCodigoERP.Text = "Actualizar";
                    btnCodigoERP.ToolTip = "Actualizar";
                }
            }
            else
            {
                txtCodErpIp.Enabled = false;
                btnCodigoERP.Visible = false;
                btnCodigoERP.Enabled = false;
                lblNombreERP.Visible = false;
                lblNombreERPTxt.Visible = false;
                lblNombreERPTxt.Text = "";
                btnCodigoERP.Text = "Consulta a ERP";
                btnCodigoERP.ToolTip = "Consultar";
            }
        }

        public bool validar_Campos(float Campdim1min, float Campdim1max, float dim1rmin, float dim1rmax)
        {
            //dim1rmin = dimension minima que se va a crear
            //dim1rmax = dimension maxima que se va a crear
            //Campdim1min = dimension minima que recupera
            //Campdim1max = dimension maxima que recupera
            bool retorno = false;
            if ((Campdim1min <= dim1rmin && dim1rmin <= Campdim1max) ||
               (Campdim1min <= dim1rmax && dim1rmax <= Campdim1max))
            {
                retorno = true;
            }
            return retorno;
        }
       
        protected void btn_Anular_Click(object sender, EventArgs e)
        {
            int respuesta;
            Consultar_Detalle_Accesorio();

            if (btn_Anular.Text == "Anular")
            {
                respuesta = cmIp.Anular_Accesorio(int.Parse(cboPlantaIp.SelectedValue.ToString()), int.Parse(LblIdAccesorio.Text), 1);

                if (respuesta == 1)
                {
                    mensajeVentana("Configuracion anulada correctamente");
                    Consultar_Accesorios();
                    LblEstadoAnula.Text = "Inactivo";
                    btn_Anular.Visible = Visible;
                    btn_Anular.Text = "Habilitar";
                }
                else
                {
                    mensajeVentana("Error al anular la configuracion");
                }
            }
            else if (btn_Anular.Text == "Habilitar")
            {
                bool valida1 = false, valida2 = false, valida3 = false,
                    valida4 = false, valida5 = false, valida6 = false, 
                    valida7 = false;
                DataTable dt;
                #region Asignacion de valores            
                //if (!String.IsNullOrEmpty(txt_Dim1min.Text.Trim()))
                //{
                //}
                //else
                //{
                //    txt_Dim1min.Text = "0";
                //}
                //if (!String.IsNullOrEmpty(txt_Dim1max.Text.Trim()))
                //{
                //}
                //else
                //{
                //    txt_Dim1max.Text = "0";
                //}
                //if (!String.IsNullOrEmpty(txt_Dim2min.Text.Trim()))
                //{
                //}
                //else
                //{
                //    txt_Dim2min.Text = "0";
                //}
                //if (!String.IsNullOrEmpty(txt_Dim2max.Text.Trim()))
                //{
                //}
                //else
                //{
                //    txt_Dim2max.Text = "0";
                //}
                //if (!String.IsNullOrEmpty(txt_Dim3min.Text.Trim()))
                //{
                //}
                //else
                //{
                //    txt_Dim3min.Text = "0";
                //}
                //if (!String.IsNullOrEmpty(txt_Dim3max.Text.Trim()))
                //{
                //}
                //else
                //{
                //    txt_Dim3max.Text = "0";
                //}
                //if (!String.IsNullOrEmpty(txt_Dim4min.Text.Trim()))
                //{
                //}
                //else
                //{
                //    txt_Dim4min.Text = "0";
                //}
                //if (!String.IsNullOrEmpty(txt_Dim4max.Text.Trim()))
                //{
                //}
                //else
                //{
                //    txt_Dim4max.Text = "0";
                //}
                //if (!String.IsNullOrEmpty(txt_Dim5min.Text.Trim()))
                //{
                //}
                //else
                //{
                //    txt_Dim5min.Text = "0";
                //}
                //if (!String.IsNullOrEmpty(txt_Dim5max.Text.Trim()))
                //{
                //}
                //else
                //{
                //    txt_Dim5max.Text = "0";
                //}
                //if (!String.IsNullOrEmpty(txt_Dim6min.Text.Trim()))
                //{
                //}
                //else
                //{
                //    txt_Dim6min.Text = "0";
                //}
                //if (!String.IsNullOrEmpty(txt_Dim6max.Text.Trim()))
                //{
                //}
                //else
                //{
                //    txt_Dim6max.Text = "0";
                //}
                //if (!String.IsNullOrEmpty(txt_Dim7min.Text.Trim()))
                //{
                //}
                //else
                //{
                //    txt_Dim7min.Text = "0";
                //}
                //if (!String.IsNullOrEmpty(txt_Dim7max.Text.Trim()))
                //{
                //}
                //else
                //{
                //    txt_Dim7max.Text = "0";
                //}
                #endregion

                if (cmIp.Consultar_AccesoriosDtt(txt_NombAcce.Text, txt_Desc_Abrev.Text, int.Parse(cboPlantaIp.SelectedValue.ToString()), int.Parse((Session["item_planta_id"].ToString()))).Rows.Count !=0)
                {
                    dt = cmIp.Consultar_AccesoriosDtt(txt_NombAcce.Text, txt_Desc_Abrev.Text, int.Parse(cboPlantaIp.SelectedValue.ToString()), int.Parse((Session["item_planta_id"].ToString())));

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        if (float.Parse(dt.Rows[i][3].ToString()) == 0 && float.Parse(dt.Rows[i][4].ToString()) == 0 && txt_Dim1min.Text != txt_Dim1max.Text)
                        {
                            valida1 = true;
                        }
                        else if ((float.Parse(txt_Dim1min.Text) == 0 && float.Parse(txt_Dim1max.Text) == 0 && float.Parse(dt.Rows[i][3].ToString()) != 0 && float.Parse(dt.Rows[i][4].ToString()) != 0) ||
                                (float.Parse(txt_Dim1min.Text) == 0 && float.Parse(txt_Dim1max.Text) == 0 && float.Parse(dt.Rows[i][3].ToString()) == 0 && float.Parse(dt.Rows[i][4].ToString()) != 0))
                        {
                            valida1 = true;
                        }
                        else if (validar_Campos(float.Parse(dt.Rows[i][3].ToString()), float.Parse(dt.Rows[i][4].ToString()), float.Parse(txt_Dim1min.Text), float.Parse(txt_Dim1max.Text)) ||
                                 validar_Campos(float.Parse(txt_Dim1min.Text), float.Parse(txt_Dim1max.Text), float.Parse(dt.Rows[i][3].ToString()), float.Parse(dt.Rows[i][4].ToString()))
                                )
                        {
                            valida1 = true;
                        }

                        if (float.Parse(dt.Rows[i][5].ToString()) == 0 && float.Parse(dt.Rows[i][6].ToString()) == 0 && txt_Dim2min.Text != txt_Dim2max.Text)
                        {
                            valida2 = true;
                        }
                        else if ((float.Parse(txt_Dim2min.Text) == 0 && float.Parse(txt_Dim2max.Text) == 0 && float.Parse(dt.Rows[i][5].ToString()) != 0 && float.Parse(dt.Rows[i][6].ToString()) != 0) ||
                                (float.Parse(txt_Dim2min.Text) == 0 && float.Parse(txt_Dim2max.Text) == 0 && float.Parse(dt.Rows[i][5].ToString()) == 0 && float.Parse(dt.Rows[i][6].ToString()) != 0))
                        {
                            valida2 = true;
                        }
                        else if (validar_Campos(float.Parse(dt.Rows[i][5].ToString()), float.Parse(dt.Rows[i][6].ToString()), float.Parse(txt_Dim2min.Text), float.Parse(txt_Dim2max.Text)) ||
                                 validar_Campos(float.Parse(txt_Dim2min.Text), float.Parse(txt_Dim2max.Text), float.Parse(dt.Rows[i][5].ToString()), float.Parse(dt.Rows[i][6].ToString()))
                                )
                        {
                            valida2 = true;
                        }

                        if (float.Parse(dt.Rows[i][7].ToString()) == 0 && float.Parse(dt.Rows[i][8].ToString()) == 0 && txt_Dim3min.Text != txt_Dim3max.Text)
                        {
                            valida3 = true;
                        }
                        else if ((float.Parse(txt_Dim3min.Text) == 0 && float.Parse(txt_Dim3max.Text) == 0 && float.Parse(dt.Rows[i][7].ToString()) != 0 && float.Parse(dt.Rows[i][8].ToString()) != 0) ||
                                (float.Parse(txt_Dim3min.Text) == 0 && float.Parse(txt_Dim3max.Text) == 0 && float.Parse(dt.Rows[i][7].ToString()) == 0 && float.Parse(dt.Rows[i][8].ToString()) != 0))
                        {
                            valida3 = true;
                        }
                        else if (validar_Campos(float.Parse(dt.Rows[i][7].ToString()), float.Parse(dt.Rows[i][8].ToString()), float.Parse(txt_Dim3min.Text), float.Parse(txt_Dim3max.Text)) ||
                                 validar_Campos(float.Parse(txt_Dim3min.Text), float.Parse(txt_Dim3max.Text), float.Parse(dt.Rows[i][7].ToString()), float.Parse(dt.Rows[i][8].ToString()))
                                )
                        {
                            valida3 = true;
                        }

                        if (float.Parse(dt.Rows[i][9].ToString()) == 0 && float.Parse(dt.Rows[i][10].ToString()) == 0 && txt_Dim4min.Text != txt_Dim4max.Text)
                        {
                            valida4 = true;
                        }
                        else if ((float.Parse(txt_Dim4min.Text) == 0 && float.Parse(txt_Dim4max.Text) == 0 && float.Parse(dt.Rows[i][9].ToString()) != 0 && float.Parse(dt.Rows[i][10].ToString()) != 0) ||
                                (float.Parse(txt_Dim4min.Text) == 0 && float.Parse(txt_Dim4max.Text) == 0 && float.Parse(dt.Rows[i][9].ToString()) == 0 && float.Parse(dt.Rows[i][10].ToString()) != 0))
                        {
                            valida4 = true;
                        }
                        else if (validar_Campos(float.Parse(dt.Rows[i][9].ToString()), float.Parse(dt.Rows[i][10].ToString()), float.Parse(txt_Dim4min.Text), float.Parse(txt_Dim4max.Text)) ||
                                 validar_Campos(float.Parse(txt_Dim4min.Text), float.Parse(txt_Dim4max.Text), float.Parse(dt.Rows[i][9].ToString()), float.Parse(dt.Rows[i][10].ToString()))
                                )
                        {
                            valida4 = true;
                        }

                        if (float.Parse(dt.Rows[i][11].ToString()) == 0 && float.Parse(dt.Rows[i][12].ToString()) == 0 && txt_Dim5min.Text != txt_Dim5max.Text)
                        {
                            valida5 = true;
                        }
                        else if ((float.Parse(txt_Dim5min.Text) == 0 && float.Parse(txt_Dim5max.Text) == 0 && float.Parse(dt.Rows[i][11].ToString()) != 0 && float.Parse(dt.Rows[i][12].ToString()) != 0) ||
                                (float.Parse(txt_Dim5min.Text) == 0 && float.Parse(txt_Dim5max.Text) == 0 && float.Parse(dt.Rows[i][11].ToString()) == 0 && float.Parse(dt.Rows[i][12].ToString()) != 0))
                        {
                            valida5 = true;
                        }
                        else if (validar_Campos(float.Parse(dt.Rows[i][11].ToString()), float.Parse(dt.Rows[i][12].ToString()), float.Parse(txt_Dim5min.Text), float.Parse(txt_Dim5max.Text)) ||
                                 validar_Campos(float.Parse(txt_Dim5min.Text), float.Parse(txt_Dim5max.Text), float.Parse(dt.Rows[i][11].ToString()), float.Parse(dt.Rows[i][12].ToString()))
                                )
                        {
                            valida5 = true;
                        }

                        if (float.Parse(dt.Rows[i][13].ToString()) == 0 && float.Parse(dt.Rows[i][14].ToString()) == 0 && txt_Dim6min.Text != txt_Dim6max.Text)
                        {
                            valida6 = true;
                        }
                        else if ((float.Parse(txt_Dim6min.Text) == 0 && float.Parse(txt_Dim6max.Text) == 0 && float.Parse(dt.Rows[i][13].ToString()) != 0 && float.Parse(dt.Rows[i][14].ToString()) != 0) ||
                                (float.Parse(txt_Dim6min.Text) == 0 && float.Parse(txt_Dim6max.Text) == 0 && float.Parse(dt.Rows[i][13].ToString()) == 0 && float.Parse(dt.Rows[i][14].ToString()) != 0))
                        {
                            valida6 = true;
                        }
                        else if (validar_Campos(float.Parse(dt.Rows[i][13].ToString()), float.Parse(dt.Rows[i][14].ToString()), float.Parse(txt_Dim6min.Text), float.Parse(txt_Dim6max.Text)) ||
                                 validar_Campos(float.Parse(txt_Dim6min.Text), float.Parse(txt_Dim6max.Text), float.Parse(dt.Rows[i][13].ToString()), float.Parse(dt.Rows[i][14].ToString()))
                                )
                        {
                            valida6 = true;
                        }

                        if (float.Parse(dt.Rows[i][15].ToString()) == 0 && float.Parse(dt.Rows[i][16].ToString()) == 0 && txt_Dim7min.Text != txt_Dim7max.Text)
                        {
                            valida7 = true;
                        }
                        else if ((float.Parse(txt_Dim7min.Text) == 0 && float.Parse(txt_Dim7max.Text) == 0 && float.Parse(dt.Rows[i][15].ToString()) != 0 && float.Parse(dt.Rows[i][16].ToString()) != 0) ||
                                (float.Parse(txt_Dim7min.Text) == 0 && float.Parse(txt_Dim7max.Text) == 0 && float.Parse(dt.Rows[i][15].ToString()) == 0 && float.Parse(dt.Rows[i][16].ToString()) != 0))
                        {
                            valida7 = true;
                        }
                        else if (validar_Campos(float.Parse(dt.Rows[i][15].ToString()), float.Parse(dt.Rows[i][16].ToString()), float.Parse(txt_Dim7min.Text), float.Parse(txt_Dim7max.Text)) ||
                                 validar_Campos(float.Parse(txt_Dim7min.Text), float.Parse(txt_Dim7max.Text), float.Parse(dt.Rows[i][15].ToString()), float.Parse(dt.Rows[i][16].ToString()))
                                )
                        {
                            valida7 = true;
                        }
                    }
                    if ((valida1 & valida2 & valida3 & valida4 &
                        valida5 & valida6 & valida7) == false)
                    {
                        Obtener_CodigoId_Accesorio();
                        respuesta = cmIp.Activar_ConfiguracionAccesorio(0,int.Parse(Session["item_planta_id"].ToString()), int.Parse(cboPlantaIp.SelectedValue.ToString()), int.Parse(LblIdAccesorio.Text));
                        Consultar_Accesorios();                               
                        Consultar_Detalle_Accesorio();
                        if (LblAnulado.Text == "False")
                        {
                            btn_Anular.Visible = Visible;
                            btn_Anular.Text = "Anular";
                            LblEstadoAnula.Text = "Activo";
                        }
                        else if (LblAnulado.Text == "True")
                        {
                            btn_Anular.Visible = Visible;
                            btn_Anular.Text = "Habilitar";
                            LblEstadoAnula.Text = "Inactivo";
                        }
                    }
                    else
                    {
                        if (valida1 & valida2 & valida3 & valida4 & valida5 & valida6 & valida7 == true)
                        {
                            mensajeVentana("Valide que las dimensiones del item que desea activar, no se cruzen con las de un accesorio existente");                        
                        }
                        Consultar_Accesorios();
                    }
                }
                else
                {
                    Obtener_CodigoId_Accesorio();
                    respuesta = cmIp.Activar_ConfiguracionAccesorio(0, int.Parse(Session["item_planta_id"].ToString()), int.Parse(cboPlantaIp.SelectedValue.ToString()), int.Parse(LblIdAccesorio.Text));
                    Consultar_Accesorios();
                    Consultar_Detalle_Accesorio();
                    btn_Anular.Text = "Anular";
                    LblEstadoAnula.Text = "Activo";
                    mensajeVentana("Configuracion habilitada correctamente");
                }          
            }
        }

        //kp
        public void Limpiar_Campos_Accesorio()
        {
            txt_NombAcce.Text = "";
            txt_Desc_Abrev.Text = "";
            txt_Dim1min.Text = "";
            txt_Dim1max.Text = "";
            txt_Dim2min.Text = "";
            txt_Dim2max.Text = "";
            txt_Dim3min.Text = "";
            txt_Dim3max.Text = "";
            txt_Dim4min.Text = "";
            txt_Dim4max.Text = "";
            txt_Dim5min.Text = "";
            txt_Dim5max.Text = "";
            txt_Dim6min.Text = "";
            txt_Dim6max.Text = "";
            txt_Dim7min.Text = "";
            txt_Dim7max.Text = "";
            Habilitar_Campos_Accesorio(false);
            GridAccesorios.Visible = false;      
        }

        //kp
        public void Habilitar_Campos_Accesorio(bool Habilitado)
        {
            if (Habilitado == true)
            {
                txt_NombAcce.Enabled = true;
                txt_Desc_Abrev.Enabled = true;
                txt_Dim1min.Enabled = true;
                txt_Dim1max.Enabled = true;
                txt_Dim2min.Enabled = true;
                txt_Dim2max.Enabled = true;
                txt_Dim3min.Enabled = true;
                txt_Dim3max.Enabled = true;
                txt_Dim4min.Enabled = true;
                txt_Dim4max.Enabled = true;
                txt_Dim5min.Enabled = true;
                txt_Dim5max.Enabled = true;
                txt_Dim6min.Enabled = true;
                txt_Dim6max.Enabled = true;
                txt_Dim7min.Enabled = true;
                txt_Dim7max.Enabled = true;             
            }
            else
            {
                txt_NombAcce.Enabled = false;
                txt_Desc_Abrev.Enabled = false;
                txt_Dim1min.Enabled = false;
                txt_Dim1max.Enabled = false;
                txt_Dim2min.Enabled = false;
                txt_Dim2max.Enabled = false;
                txt_Dim3min.Enabled = false;
                txt_Dim3max.Enabled = false;
                txt_Dim4min.Enabled = false;
                txt_Dim4max.Enabled = false;
                txt_Dim5min.Enabled = false;
                txt_Dim5max.Enabled = false;
                txt_Dim6min.Enabled = false;
                txt_Dim6max.Enabled = false;
                txt_Dim7min.Enabled = false;
                txt_Dim7max.Enabled = false;         
            }
        }
        //kp
        public void Consultar_Accesorios()
        {
            GridAccesorios.DataSource = cmIp.Consultar_Accesorios(txt_NombAcce.Text, txt_Desc_Abrev.Text, int.Parse(cboPlantaIp.SelectedValue));
            GridAccesorios.DataMember = cmIp.Consultar_Accesorios(txt_NombAcce.Text, txt_Desc_Abrev.Text, int.Parse(cboPlantaIp.SelectedValue.ToString())).Tables[0].ToString();
            GridAccesorios.DataBind();
            GridAccesorios.Visible = true;
        }

        //kp
        public void Consultar_Detalle_Accesorio()
        {
            DataTable dt;
            dt = null;

            dt = cmIp.Consultar_Detalle_Accesorio(int.Parse(Session["item_planta_id"].ToString()), int.Parse(cboPlantaIp.SelectedValue.ToString()));

            if (dt.Rows.Count != 0)
            {
                txt_NombAcce.Text = dt.Rows[0]["Nomenclatura"].ToString();
                txt_Desc_Abrev.Text = dt.Rows[0]["Des_Aux"].ToString();
                txt_Dim1min.Text = dt.Rows[0]["Valor_1_Min"].ToString();
                txt_Dim1max.Text = dt.Rows[0]["Valor_1_Max"].ToString();
                txt_Dim2min.Text = dt.Rows[0]["Valor_2_Min"].ToString();
                txt_Dim2max.Text = dt.Rows[0]["Valor_2_Max"].ToString();
                txt_Dim3min.Text = dt.Rows[0]["Valor_3_Min"].ToString();
                txt_Dim3max.Text = dt.Rows[0]["Valor_3_Max"].ToString();
                txt_Dim4min.Text = dt.Rows[0]["Valor_4_Min"].ToString();
                txt_Dim4max.Text = dt.Rows[0]["Valor_4_Max"].ToString();
                txt_Dim5min.Text = dt.Rows[0]["Valor_5_Min"].ToString();
                txt_Dim5max.Text = dt.Rows[0]["Valor_5_Max"].ToString();
                txt_Dim6min.Text = dt.Rows[0]["Valor_6_Min"].ToString();
                txt_Dim6max.Text = dt.Rows[0]["Valor_6_Max"].ToString();
                txt_Dim7min.Text = dt.Rows[0]["Valor_7_Min"].ToString();
                txt_Dim7max.Text = dt.Rows[0]["Valor_7_Max"].ToString();
                cboPlantaIp.SelectedValue = dt.Rows[0]["planta_id"].ToString();
                LblIdAccesorio.Text = dt.Rows[0]["Codigos_Id"].ToString();
                LblAnulado.Text = dt.Rows[0]["Acc_Anulado"].ToString();
            }
            else
            {

            }           
        }
        public void Obtener_CodigoId_Accesorio()
        {
            DataTable dt;
            dt = null;
            dt = cmIp.Consultar_Detalle_Accesorio(int.Parse(Session["item_planta_id"].ToString()), int.Parse(cboPlantaIp.SelectedValue.ToString()));

            if (dt.Rows.Count != 0)
            { LblIdAccesorio.Text = dt.Rows[0]["Codigos_Id"].ToString(); }else{}                
        }
     
        protected void chk_InspObligatoria_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_InspObligatoria.Checked == true)
            {
                chk_InspObligatoria.Checked = true;
                chk_InspCalidad.Checked = true;
            }            
        }

        protected void chk_InspCalidad_CheckedChanged1(object sender, EventArgs e)
        {
            if (chk_InspObligatoria.Checked == true)
            {
                chk_InspObligatoria.Checked = true;
                chk_InspCalidad.Checked = true;
            }
        }
        ////200618
        public void Ocultar_Grupo_Item_Forsa(bool respuesta)
        {
            if (respuesta == false)
            {
                btnGuardarIa.Visible = true;             
                btnLimpiarIa.Visible = true;
                btnReporte.Visible = true;
                Lblobligatorio.Visible = true;
                txtDescripcionIa.Visible = true;
                lblDescripcionIa.Visible = true;
                cboGrupoIa.Visible = true;
                lblGrupoIa.Visible = true;
                lblactivoI.Visible = true;
                lbltituloitem.Visible = true;
                AccordionPane1.Visible = false;
                AccordionPane2.Visible = true;
            }
            else
            {
                btnGuardarIa.Visible = false;              
                btnLimpiarIa.Visible = false;
                btnReporte.Visible = false;
                Lblobligatorio.Visible = false;
                txtDescripcionIa.Visible = false;
                lblDescripcionIa.Visible = false;
                cboGrupoIa.Visible = false;
                lblGrupoIa.Visible = false;
                lblactivoI.Visible = false;
                lbltituloitem.Visible = false;
                AccordionPane1.Visible = true;
                AccordionPane2.Visible = false;
            }
        }

      
    }
}


