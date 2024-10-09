using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaControl;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using CapaDatos;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Microsoft.Reporting.WebForms;

namespace SIO
{
    public partial class SiatPlanVisita : System.Web.UI.Page
    {
        private ControlPoliticas CP = new ControlPoliticas();
        private ControlSIAT CS = new ControlSIAT();
        public ControlFUP controlfup = new ControlFUP();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cboContacto.Attributes.Add("onchange", "asignarId();");
                ocultarPaneles(false);
                ocultarBotones(false);
                limCampos();
                cargarCombos();
                btnGuardarVis.Visible = true;
                politicas(59, Session["usuario"].ToString());
                Session["dPend"] = "";
                //if (Session["SIATOrdenes"] != null)
                //{ // 0    -     1     -    2   -     3
                //    PanelVisita.Visible = true;     //idOfa - idCliente - idObra - fechaObra - cliente - moneda - valor
                //    btnActVis.Visible = false;
                //    String[] dato = Session["SIATOrdenes"].ToString().Split('|');
                //    llenarList(CS.cargarObras(" (Orden.Id_Ofa = " + dato[0] + ") "), listObra, 3, 0);
                //    llenarList(CS.cargarOf("", " AND (Orden.Id_Ofa = " + dato[0] + ") "), listOF, 1, 0);
                //    lblIdCliente.Value = dato[1];
                //    this.txtCliente_TextChanged(sender, e);
                //    cargarCombo(CS.cargarOf(" AND  (obra.obr_id = " + dato[2] + ") ", ""), cboOF, 0, 1);//Combo Ofs
                //    txtFLlegObra.Text = dato[3];
                //    txtCliente.Text = dato[4];
                //    cboActividad.SelectedIndex = 1;
                //    cboOF.SelectedValue = dato[0].ToString();
                //    int indexObra = buscarValueObra(int.Parse(dato[2]));
                //    cboObra.SelectedIndex = indexObra;
                //    //string a = dato[6].Replace(",", "");
                //    int dias = CS.consultarDiasTotales(int.Parse(dato[5]), Convert.ToDouble(dato[6]));
                //    txtDTol.Text = dias.ToString();
                //    txtDTol_TextChanged(sender, e);
                //    btnActVis.Visible = false;
                //    Session["SIATidViaje"] = "";
                //    chkCotizacion.Enabled = false;
                //    chkCotizacion.Checked = false;
                //    String[] actividad = cboActividad.SelectedValue.ToString().Split('|');//siat_act_id|siat_act_pantalla|siat_act_letra|siat_act_inven|siat_act_imple
                //    if (actividad[1].ToString() == "1")
                //    {
                //        PanelVisita.Visible = true;
                //        //PanelVisita.GroupingText = cboActividad.SelectedItem.ToString() + "(" + actividad[2] + ")";
                //        PanelOtraAct.Visible = false;
                //        btnBuscarVis.Visible = true;
                //        btnOrdenes.Visible = true;
                //        Session["SIATTipoActA"] = "";
                //        if (actividad[3].ToString() == "1")
                //        {
                //            Session["SIATTipoActA"] = "inv";
                //            txtDInvCom.Visible = false;
                //            lblDInv.Visible = true;
                //            btnGuardarVis.Visible = true;
                //        }
                //        else if (actividad[4].ToString() == "1")
                //        {
                //            Session["SIATTipoActA"] = "imp";
                //            txtDInvCom.Visible = false;
                //            lblDInv.Visible = true;
                //            btnGuardarVis.Visible = false;
                //        }
                //        else
                //        {
                //            Session["SIATTipoActA"] = "comp";
                //            txtDInvCom.Visible = true;
                //            lblDInv.Visible = false;
                //            btnGuardarVis.Visible = true;
                //            //txtDTol.Enabled = false;
                //        }
                //    }
                //    Session["SIATOrdenes"] = null;
                //}
            }
        }

        private int buscarValueObra(int id) 
        {
            int index = 0;
            for (int i = 1; i < cboObra.Items.Count; i++)
            {
                cboObra.SelectedIndex = i;
                String[] listado = cboObra.SelectedValue.Split('|');
                if (Convert.ToInt32(listado[0]) == id)
                {
                    index = i;
                    break;                
                }
            }
            return index;
        }

        #region METODOS GENERALES
        //Maneja las politicas dependiendo de la rutina y el rol del usuario
        private void politicas(int rutina, String usuario)
        {
            Boolean agregar = false;
            Boolean eliminar = false;
            Boolean imprimir = false;
            Boolean editar = false;
            DataTable politicas = null;
            politicas = CP.politicasBotones(rutina, usuario);
            foreach (DataRow row in politicas.Rows)
            {
                agregar = Boolean.Parse(row["agregar"].ToString());
                eliminar = Boolean.Parse(row["eliminar"].ToString());
                imprimir = Boolean.Parse(row["imprimir"].ToString());
                editar = Boolean.Parse(row["editar"].ToString());
            }

            if (agregar == true)
            {
                btnGuardarVis.Visible = true;
                btnGuardarCostos.Visible = true;
                btnGuardarNov.Visible = true;
            }
            else
            {
                btnGuardarVis.Visible = false;
                btnGuardarCostos.Visible = false;
                btnGuardarNov.Visible = false;
            }

            if (editar == true)
            {
                btnActVis.Visible = true;
            }
            else
            {
                btnActVis.Visible = false;
            }

        }
        //oculta o visualizan todos los paneles
        private void ocultarPaneles(Boolean boo)
        {
            PanelVisita.Visible = boo;
            PanelOtraAct.Visible = boo;
            //btnBuscarVis.Visible = boo;
            //btnOrdenes.Visible = boo;
        }
        //oculta o visualizan todos los botones
        private void ocultarBotones(Boolean boo)
        {
            btnGuardarVis.Visible = boo;
            btnActVis.Visible = boo;
            btnCostos.Visible = boo;
            btnNovedad.Visible = boo;
            btnCerrar.Visible = boo;
            btnCancelar.Visible = boo;
            btnConfi.Visible = boo;
            btnNuevo.Visible = boo;
        }
        //oculta o visualizan todos los campos de la vista general
        private void enableCampGeneral(Boolean boo)
        {
            cboActividad.Enabled = boo;
            //cboTecnico.Enabled = boo;
            txtFIniAct.Enabled = boo;
            txtFFinAct.Enabled = boo;
        }
        //oculta o visualizan todos los campos de una actividad
        private void enableCampActiv(Boolean boo)
        {
            //txtCliente.Enabled = boo;
            cboContacto.Enabled = boo;
            listCont.Enabled = boo;
            txtFLlegObra.Enabled = boo;
            txtFFinObra.Enabled = boo;
            txtHotel.Enabled = boo;
            txtDireccion.Enabled = boo;
            txtTelefono.Enabled = boo;
            txtObserva.Enabled = boo;
            //cboObra.Enabled = boo;
            //btnAggObra.Enabled = boo;
            //btnEliObra.Enabled = boo;
            //listObra.Enabled = boo;
            //cboOF.Enabled = boo;
            //btnAggOF.Enabled = boo;
            //btnEliOF.Enabled = boo;
            //listOF.Enabled = boo;
            btnAggCont.Enabled = boo;
            btnEliCont.Enabled = boo;
            listCont.Enabled = boo;
        }
        //limpia todos los campos de los datos generales
        private void limCamGeneral()
        {
            //General
            cboActividad.Items.Clear();
            cboTecnico.Items.Clear();
            txtFIniAct.Text = "";
            txtFFinAct.Text = "";
            lblNomTec.Text = "";
            lblTelTec.Text = "";
            lblCorrTec.Text = "";
            lblDViaje.Text = "";
        }
        //limpia todos los campos de los datos de visita o otra actividad
        private void limCamActiv()
        {
            //Visita
            lblDReal.Text = "0";
            lblDImp.Text = "0";
            //lblDInv.Text = "0";
            txtDTol.Text = "0";
            txtDConsumidos.Text = "0";
            txtDInvCom.Text = "0";
            lblEstado.Text = "";
            txtCliente.Text = "";
            txtHotel.Text = "";
            cboContacto.Items.Clear();
            txtDireccion.Text = "";
            txtFLlegObra.Text = "";
            txtFFinObra.Text = "";
            txtTelefono.Text = "";
            cboObra.Items.Clear();
            listObra.Items.Clear();
            cboOF.Items.Clear();
            listOF.Items.Clear();
            listCont.Items.Clear();
            //Otra actividad
            txtObsAct.Text = "";
            txtObserva.Text = "";
            lblIdMoneda.Value = null;
            lblIdPais.Value = null;
        }
        //limpia todos los campos de las novedades
        private void limCamNov()
        {
            //Novedad
            cboOrdenNov.Items.Clear();
            cboNovedad.Items.Clear();
            txtObsNov.Text = "";
            listOFNovObs.Items.Clear();
        }
        //limpia todos los campos de los costos
        private void limCamCos()
        {   //Costos
            lblNomPais.Text = "";
            lblPlanHotel.Text = "";
            lblPlanTiq.Text = "";
            lblPlanAli.Text = "";
            lblPlanTran.Text = "";
            //lblPlanTranAer.Text = "";
            lblPlanLLam.Text = "";
            lblPlanLav.Text = "";
            lblPlanPenal.Text = "";
            lblPlanOtros.Text = "";
            lblRealHotel.Text = "0";
            lblRealTiq.Text = "0";
            lblRealAli.Text = "0";
            lblRealTran.Text = "0";
            //lblRealTranAer.Text = "";
            lblRealLlam.Text = "0";
            lblRealLav.Text = "0";
            lblRealPenal.Text = "0";
            lblRealOtros.Text = "0";
            lblRealTrm.Text = "0";
            txtRealObs.Text = "";
            txtCliente.Text = "";
        }
        //limpia todos los campos
        private void limCampos()
        {
            limCamGeneral();
            limCamActiv();
            limCamNov();
            limCamCos();
        }
        //sarga todos los combos predeterminados
        private void cargarCombos()
        {
            cargarCombo(CS.cargarActividades(), cboActividad, 0, 1);//Combo actividad
            cargarCombo(CS.cargarTecnicos(""), cboTecnico, 0, 1);//Combo tecnico
        }
        //envia los mensajes en los alert
        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }
        //carga el combo
        private void cargarCombo(DataTable tabla, DropDownList combo, int value, int texto)
        {
            combo.Items.Clear();
            combo.Items.Add("Seleccionar");
            foreach (DataRow row in tabla.Rows)
            {   //posicion de las colmunas  0 = value / 1 = texto  --- se escoge el numero dependiendo de la columna que tenga en el query //siempre va el valor como id de primero, y despues el texto lo que se va mostrar en el combo / ,0,1
                combo.Items.Add(new ListItem(row[texto].ToString(), row[value].ToString()));
            }
        }
        //Carga los datos del tecnico
        private void cargarDatosTec(String idTec)
        {
            if (idTec != "Seleccionar")
            {
                DataTable tecnico = null;
                tecnico = CS.cargarTecnicos("AND (empleado.emp_usu_num_id = " + idTec + ")");
                foreach (DataRow row in tecnico.Rows)
                {
                    lblNomTec.Text = row["nomEmp"].ToString() + " " + row["apeEmp"].ToString();
                    lblTelTec.Text = row["celEmp"].ToString();
                    lblCorrTec.Text = row["correoEmp"].ToString();
                }
            }
            else
            {
                lblNomTec.Text = "";
                lblTelTec.Text = "";
                lblCorrTec.Text = "";
            }
        }
        //metodo general para agregar las listas ilimitada (Si permite repeticion)
        private void agregarListaLibre(ListBox list, String item, String value)
        {
            if (value != "")//verifico si hay algo en el id del combo
            {
                list.Items.Add(new ListItem(item.ToString(), value.ToString()));
            }
        }
        //metodo general para agregar las listas limitada (No permite repeticion)
        private void agregarLista(ListBox list, String item, String value)
        {
            if (value != "")//verifico si hay algo en el id del combo
            {
                if (list.Items.Count >= 1)//verifico si hay algo en la lista que ingresa en la variable list
                {
                    String existe = "NO";
                    foreach (ListItem li in list.Items)//lleno y recorro un list por medio del la lista que esta entrando
                    {
                        if (li.Value == value.ToString())//verifico si el id ya esta en la lista
                        {
                            existe = "SI";
                        }
                    }
                    if (existe == "NO")//agrego a la lista el item y el value si no existe en lista
                    {
                        list.Items.Add(new ListItem(item.ToString(), value.ToString()));
                    }
                }
                else//si no hay agrego inmediatamente
                {
                    list.Items.Add(new ListItem(item.ToString(), value.ToString()));
                }
            }
        }
        //elimina el item selecionado
        private void eliminarLista(ListBox list)
        {
            for (int i = 0; i < list.Items.Count; i++)
            {
                if (list.Items[i].Selected)
                {
                    list.Items.Remove(list.Items[i]);
                }
            }
        }
        //guarda los valores de la lista y la guarda en una lista string
        private String listaString(ListBox listaAgg)
        {
            String listaString = "";
            int conList = 0;
            foreach (ListItem lis in listaAgg.Items)
            {
                if (conList == 1)
                {
                    listaString = listaString + "," + lis.Value;
                }
                else
                {
                    listaString = listaString + lis.Value;
                    conList = 1;
                }
            }
            return listaString;
        }
        //guarda los valores de la lista y la guarda en una lista string si es una lista a la que se le va hacer un split
        private String listaStringSplit(ListBox listaAgg, int posicion)
        {
            String listaString = "";
            int conList = 0;
            foreach (ListItem list in listaAgg.Items)
            {
                String[] listado = list.Value.Split('|');
                if (conList == 1)
                {
                    listaString = listaString + "," + listado[posicion];
                }
                else
                {
                    listaString = listaString + listado[posicion];
                    conList = 1;
                }
            }
            return listaString;
        }
        //llena las listas
        private void llenarList(DataTable tabla, ListBox list, int text, int value)
        {   // 1/0  -- nom/id
            list.Items.Clear();
            foreach (DataRow row in tabla.Rows)
            {
                list.Items.Add(new ListItem(row[text].ToString(), row[value].ToString()));
            }
        }
        //valida todos los campos generales
        private String validaCamposGen()
        {
            String mensaje = "";
            if (cboTecnico.SelectedItem.ToString() == "Seleccionar")
            {
                mensaje = "Por favor seleccione el tecnico, gracias!";
            }
            else
            {
                if (txtFIniAct.Text == "" || txtFFinAct.Text == "")
                {
                    mensaje = "Por favor ingrese las fechas correspondientes, gracias!";
                }
                else
                {
                    if (DateTime.Parse(txtFFinAct.Text) < DateTime.Parse(txtFIniAct.Text))
                    {
                        mensaje = "La fecha fin no puede ser menor que la fecha de inicio, gracias!";
                    }
                    else
                    {
                        mensaje = "OK";
                    }
                }
            }
            return mensaje;
        }
        //valida todos los campos de una visita
        private String validaCamposVis()
        {
            String mensaje = "";
            if (txtFLlegObra.Text == "" || txtFFinObra.Text == "" || txtDTol.Text == "")
            {
                mensaje = "Por favor ingrese las fechas correspondientes de la obra, gracias!";
            }
            else
            {
                if (DateTime.Parse(txtFFinObra.Text) < DateTime.Parse(txtFLlegObra.Text) || DateTime.Parse(txtFFinObra.Text) > DateTime.Parse(txtFFinAct.Text))
                {
                    mensaje = "La fecha de llegada obra tiene que estar dentro del rango de la fecha de viaje, gracias!";
                }
                else
                {
                    if (DateTime.Parse(txtFLlegObra.Text) < DateTime.Parse(txtFIniAct.Text) || DateTime.Parse(txtFLlegObra.Text) > DateTime.Parse(txtFFinAct.Text))
                    {
                        mensaje = "La fecha de llegada obra tiene que estar dentro del rango de la fecha de viaje, gracias!";//"Las fechas de la obra, tienen que estar dentro del rango de las fechas del viaje, gracias!"
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(txtDTol.Text.Trim()) && txtDTol.Text == "0")
                        {
                            mensaje = "El campo de los días totales no puede ser cero (0), ni vacío. Gracias!";                           
                        }                        
                        else
                        {
                            int dias = Convert.ToInt32(txtDConsumidos.Text) + Convert.ToInt32(lblDReal.Text);

                            if (Convert.ToInt32(txtDTol.Text) < dias)
                            {
                                mensaje = "Los días disponibles no son suficientes para este viaje. Gracias!";
                            }
                            else
                            {
                                if (!chkCotizacion.Checked)
                                {
                                    if (listOF.Items.Count == 0)
                                    {
                                        mensaje = "Debe de relacionar al menos una OF/orden en el viaje, gracias!";
                                    }

                                    else
                                    {
                                        mensaje = "OK";
                                    }
                                }

                                else
                                {
                                    mensaje = "OK";
                                }
                            }                                            
                        }                        
                    }
                }
            }
            return mensaje;
        }
        //valida todos los campos de los costos
        private String validaCamposCos()
        {
            String mensaje = "";
            if (lblRealHotel.Text == "" || lblRealTiq.Text == "" || lblRealAli.Text == "" || lblRealTran.Text == "" || lblRealLlam.Text == "" || lblRealLav.Text == "" || lblRealOtros.Text == "" || lblRealPenal.Text == "" || lblRealTrm.Text == "")
            {
                mensaje = "Todos los campos deben de tener un valor";
            }
            else { mensaje = "OK"; }
            return mensaje;
        }
        //Carga los grids
        private void cargarTabla(GridView grid, DataTable tabla)
        {
            grid.DataSource = tabla;
            grid.DataBind();
        }
        //Me carga los datos para editar
        private void cargarDatos(String idViaje, Object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtOF.Text))
            {
                txtOF_TextChanged(sender, e);
            }
            DataTable viaje = null;
            Session["SIATidViaje"] = idViaje;

            viaje = CS.cargarViajeTEC(idViaje);            
            ocultarBotones(false); 
            PanelCotizacion.Enabled = false;
            enableCampGeneral(true);
            enableCampActiv(true);
            btnConfi.Visible = true;
            cboActividad.Enabled = false;
            btnCostos.Visible = false;
            btnNovedad.Visible = false;
            btnCerrar.Visible = false;
            btnActVis.Visible = false;
            btnGuardarVis.Visible = true;
            txtFFinAct.Enabled = true;
            txtFFinObra.Enabled = true;
            txtFIniAct.Enabled = true;
            txtFLlegObra.Enabled = true;
            btnConfi.Visible = false;
            btnNuevo.Visible = false;
            cboTecnico.Enabled = true;
            txtDInvCom.Enabled = true;
            btnCancelar.Visible = false;

            foreach (DataRow row in viaje.Rows)
            {
                if (row["estado"].ToString() == "1")
                {
                    enableCampGeneral(true);
                    enableCampActiv(true);
                    btnConfi.Visible = true;
                    cboActividad.Enabled = false;
                    btnCostos.Visible = false;
                    btnNovedad.Visible = false;
                    btnCerrar.Visible = false;
                    btnActVis.Visible = true;
                    txtFFinAct.Enabled = true;
                    txtFFinObra.Enabled = true;
                    txtFIniAct.Enabled = true;
                    txtFLlegObra.Enabled = true;
                    btnConfi.Visible = true;
                    btnNuevo.Visible = false;
                    cboTecnico.Enabled = true;
                    txtDInvCom.Enabled = true;
                    btnGuardarVis.Visible = false;
                    btnCancelar.Visible = true;

                }
                else if (row["estado"].ToString() == "2" || row["estado"].ToString() == "3")
                {
                    enableCampActiv(false);
                    btnCostos.Visible = true;
                    btnNovedad.Visible = true;
                    btnCerrar.Visible = true;
                    btnActVis.Visible = false;
                    txtFFinAct.Enabled = false;
                    txtFFinObra.Enabled = false;
                    txtFIniAct.Enabled = false;
                    txtFLlegObra.Enabled = false;
                    btnConfi.Visible = false;
                    btnNuevo.Visible = true;
                    cboTecnico.Enabled = false;
                    cboActividad.Enabled = false;
                    txtDInvCom.Enabled = false;
                    btnGuardarVis.Visible = false;
                    btnCancelar.Visible = true;
                }
                if (row["estado"].ToString() == "3")
                {
                    btnCancelar.Visible = false;
                }
                Session["SIATestadoViaje"] = row["estado"].ToString();
                Session["SIATTipoActA"] = row["tipoAlt"].ToString();//muestra el tipo con una logica diferente, para saber si es un inventario cerrado
                Session["SIATTipoActAReal"] = row["tipo"].ToString();//muestra el tipo real
                lblEstado.Text = row["estado"].ToString();
                cboActividad.SelectedIndex = cboActividad.Items.IndexOf(cboActividad.Items.FindByValue(row["idPanLetraAct"].ToString()));
                txtHotel.Text = row["hotel"].ToString();
                txtDireccion.Text = row["direc"].ToString();
                txtTelefono.Text = row["tel"].ToString();
                txtConsecutivo.Text = row["siat_cotizacion_id"].ToString();

                if (bool.Parse(row["bitCotizacion"].ToString()))
                {
                    txtDTol.Text = row["dias"].ToString();
                    ocultarParametrosCotizacion(true);
                }
                else
                {
                    ocultarParametrosCotizacion(false);
                    int diasPend = Convert.ToInt32(txtDTol.Text) - Convert.ToInt32(txtDConsumidos.Text);
                    if (diasPend > 0)
                        lblDPend.Text = diasPend.ToString();
                }
                
                txtObserva.Text = row["observacion"].ToString();
                lblEstado.Text = row["descripcion"].ToString();               
                llenarList(CS.cargarContaVia(idViaje), listCont, 1, 0);

                if (!String.IsNullOrEmpty(row["cotizacion"].ToString()) && Convert.ToInt32(row["cotizacion"].ToString()) != 0)
                {
                    DataTable cotizacion = new DataTable();
                    //chkCotizacion.Checked = true;

                    cboCotizacion.Items.Clear();
                    cboCotizacion.Items.Add(new ListItem(row["consecutivo"].ToString(), row["cotizacion"].ToString()));
                    cotizacion = CS.buscarCotizacion(Convert.ToInt32(row["cotizacion"].ToString()));
                    cboServicio.Items.Clear();
                    cboServicio.Items.Add(new ListItem(cotizacion.Rows[0]["servicio"].ToString(), cotizacion.Rows[0]["servicio_id"].ToString()));
                    lblServicio.Visible = true;
                    cboServicio.Visible = true;
                    lblCotizacion.Visible = true;
                    cboCotizacion.Visible = true;
                }

                // 0|1|2|3|4
                String[] actividad = cboActividad.SelectedValue.ToString().Split('|');//siat_act_id|siat_act_pantalla|siat_act_letra|siat_act_inven|siat_act_imple
                if (actividad[4].ToString() == "1")//si es un iventario cerrado, nos damos cuenta si el tipo alterado es imp
                {
                    if (Session["SIATTipoActAReal"].ToString() == "imp")
                    {
                        cboTecnico.SelectedIndex = cboTecnico.Items.IndexOf(cboTecnico.Items.FindByValue(row["idTec"].ToString()));
                        cargarDatosTec(row["idTec"].ToString());
                        txtFIniAct.Text = row["fechaIni"].ToString();
                        txtFFinAct.Text = row["fechaFin"].ToString();
                        Session["SIATfechaIniVia"] = row["fechaIni"].ToString();
                        Session["SIATfechaFinVia"] = row["fechaFin"].ToString();
                        calTodosDias(lblDViaje, txtFIniAct.Text, txtFFinAct.Text);
                        txtFLlegObra.Text = row["fechaObraIni"].ToString();
                        txtFFinObra.Text = row["fechaObraFin"].ToString();
                        //txtFFinObra_TextChanged(sender, e); 
                        fechas();                      
                    }
                    else
                    {
                        enableCampActiv(false);
                        //txtDTol.Enabled = true;
                        txtFIniAct.Enabled = true;
                        txtFFinAct.Enabled = true;
                        txtFLlegObra.Enabled = true;
                        txtFFinObra.Enabled = true;
                        btnActVis.Visible = false;
                        btnCancelar.Visible = false;
                        btnGuardarVis.Visible = true;
                        btnConfi.Visible = false;
                    }
                }
                else
                {
                    cboTecnico.SelectedIndex = cboTecnico.Items.IndexOf(cboTecnico.Items.FindByValue(row["idTec"].ToString()));
                    cargarDatosTec(row["idTec"].ToString());
                    txtFIniAct.Text = row["fechaIni"].ToString();
                    txtFFinAct.Text = row["fechaFin"].ToString();
                    Session["SIATfechaIniVia"] = row["fechaIni"].ToString();
                    Session["SIATfechaFinVia"] = row["fechaFin"].ToString();
                    calTodosDias(lblDViaje, txtFIniAct.Text, txtFFinAct.Text);
                    txtFLlegObra.Text = row["fechaObraIni"].ToString();
                    txtFFinObra.Text = row["fechaObraFin"].ToString();
                    //txtFFinObra_TextChanged(sender, e);
                    fechas();
                }
                txtDInvCom.Text = row["dInv"].ToString();
                txtDInvCom_TextChanged(sender, e);
                color();
            }
        }
        //Guarda los datos
        private void guardar(String diasInv, String idInv, String dPend, object sender, EventArgs e)
        {
            String[] activ = cboActividad.SelectedValue.ToString().Split('|');
            int clientes = 0;
            int contactos = 0;
            
            String listConts = listaStringSplit(listCont, 1);
            String listOFs = "";
            if (listOF.Items.Count > 0)
            {
                listOFs = listaString(listOF);
                clientes = CS.contarClientesOF(listOFs);
                contactos = listCont.Items.Count;
            }
                      
            if (clientes == contactos)
            {
                int servicio = 11; //id del servicio por defecto para implementacion obra
                double honorarios = 0;
                double tiquetes = 0;
                double total = 0;
                DataTable dt = CS.consultarValorPlaneado(Convert.ToInt32(lblIdCliente.Value));
                if (dt.Rows.Count > 0)
                {
                    honorarios = Convert.ToDouble(dt.Rows[0]["honorarios"]) * Convert.ToInt32(lblDReal.Text);
                    tiquetes = Convert.ToDouble(dt.Rows[0]["tiquete"]);
                    total = honorarios + tiquetes;
                }

                int moneda = 0;
                dt = CS.consultarMonedaCliente(Convert.ToInt32(lblIdCliente.Value));
                if(dt.Rows.Count > 0)
                {
                    lblIdMoneda.Value = dt.Rows[0]["mon_id"].ToString();
                    moneda = Convert.ToInt32(lblIdMoneda.Value);
                    lblIdPais.Value = dt.Rows[0]["pais"].ToString();
                }
                else
                {
                    lblIdMoneda.Value = null;
                    lblIdPais.Value = null;
                }

                int idCotizacion = 0;
                if (chkCotizacion.Checked && cboCotizacion.SelectedItem.ToString() != "Seleccionar")
                {
                    idCotizacion = Convert.ToInt32(cboCotizacion.SelectedItem.Value);
                }

                else
                {
                    idCotizacion = CS.insertarCotizacion(servicio, Convert.ToInt32(lblIdCliente.Value), 1, honorarios, tiquetes, total, Session["usuario"].ToString(), Convert.ToInt32(lblDReal.Text), moneda, 2, 0, "",true);
                }

                if (idCotizacion != -1)
                {
                    int estado = 1;

                    if (chkCotizacion.Checked)
                    {
                        estado = 2;
                    }

                    String menId = CS.insertarViajeTEC(Session["usuario"].ToString(), activ[0], cboTecnico.SelectedValue.ToString(), (txtTelefono.Text == "") ? "null" : txtTelefono.Text, txtHotel.Text, txtDireccion.Text, txtFIniAct.Text, txtFFinAct.Text, txtFLlegObra.Text, txtFFinObra.Text, Session["SIATTipoActA"].ToString(), txtDTol.Text, lblDReal.Text, diasInv, lblDImp.Text, idInv, dPend, txtObserva.Text, idCotizacion, estado);
                    if (menId.Substring(0, 1) != "E")//E <- de ERROR
                    {
                        if (!chkCotizacion.Checked && estado == 2)
                        {
                            CS.actualizarDiasConsumidos(Convert.ToInt32(lblFUP.Value), lblVersion.Value, Convert.ToInt32(lblDReal.Text), Convert.ToInt32(txtDConsumidos.Text));
                        }

                        if (listOF.Items.Count > 0)
                        {
                            CS.insertarOFs(menId, listOFs);
                        }

                        CS.insertarCont(menId, listConts);

                        Session["SIATidViaje"] = menId;//Id la visita
                        cboActividad.Enabled = false;
                        Session["SIATfechaIniVia"] = txtFIniAct.Text;
                        Session["SIATfechaFinVia"] = txtFFinAct.Text;
                        cargarDatos(Session["SIATidViaje"].ToString(), sender, e);
                        mensajeVentana("Se ha ingresado correctamente!!");
                        if (estado == 1)
                        {
                            btnConfi.Visible = true;
                            btnActVis.Visible = true;
                        }
                        else
                        {
                            btnConfi.Visible = false;
                            btnActVis.Visible = false;
                        }
                        btnGuardarVis.Visible = false; 
                        btnCancelar.Visible = true;
                        cboCotizacion.Enabled = false;
                        Session["SIATOrdenes"] = null;
                        txtConsecutivo.Text = idCotizacion.ToString();
                        txtConsecutivo_TextChanged(sender, e);
                    }
                    else { mensajeVentana(menId); }
                }
                else { mensajeVentana("Error insertando el viaje. Por favor intente de nuevo."); }
            }
            else { mensajeVentana("Hace falta contactos!! Cada cliente debe de tener un contanto relaccionado, gracias!"); }

        }
        //Actualiza los datos
        private void actualizar(String filtroCamp, object sender, EventArgs e)
        {
            String mensaje = validaCamposVis();
            if (mensaje == "OK")
            {
                int clientes = 0;
                int contactos = 0;

                String listConts = listaStringSplit(listCont, 1);
                String listOFs = "";
                if (listOF.Items.Count > 0)
                {
                    listOFs = listaString(listOF);
                    clientes = CS.contarClientesOF(listOFs);
                    contactos = listCont.Items.Count;
                }

                if (clientes == contactos)
                {
                    String menConf = CS.editarViajeTEC(Session["usuario"].ToString(), (txtTelefono.Text == "") ? "null" : txtTelefono.Text, txtHotel.Text, txtDireccion.Text, txtFLlegObra.Text, txtFFinObra.Text, Session["SIATidViaje"].ToString(), filtroCamp);
                    if (menConf == "OK")
                    {
                        int servicio = 11; //id del servicio por defecto para implementacion obra
                        double honorarios = 0;
                        double tiquetes = 0;
                        double total = 0;
                        DataTable dt = CS.consultarValorPlaneado(Convert.ToInt32(lblIdCliente.Value));
                        if (dt.Rows.Count > 0)
                        {
                            honorarios = Convert.ToDouble(dt.Rows[0]["honorarios"]);
                            tiquetes = Convert.ToDouble(dt.Rows[0]["tiquete"]);
                            total = honorarios + tiquetes;
                        }

                        int moneda = 0;
                        dt = CS.consultarMonedaCliente(Convert.ToInt32(lblIdCliente.Value));
                        if (dt.Rows.Count > 0)
                        {
                            lblIdMoneda.Value = dt.Rows[0]["mon_id"].ToString();
                            moneda = Convert.ToInt32(lblIdMoneda.Value);
                            lblIdPais.Value = dt.Rows[0]["pais"].ToString(); ;
                        }
                        else
                        {
                            lblIdMoneda.Value = null;
                            lblIdPais.Value = null;
                        }
                        CS.ActualizarCotizacion(Convert.ToInt32(cboCotizacion.SelectedItem.Value), servicio, Convert.ToInt32(lblIdCliente.Value),
                                                1, honorarios, tiquetes, total, Convert.ToInt32(txtDTol.Text), moneda, 2, "",
                                                Session["usuario"].ToString(), "",true);

                        if (listOF.Items.Count > 0)
                        {
                            CS.insertarOFs(Session["SIATidViaje"].ToString(), listOFs);
                        }

                        CS.insertarCont(Session["SIATidViaje"].ToString(), listConts);
                        if (!chkCotizacion.Checked)
                        {
                            if (lblEstado.Text != "Planeado")
                            {
                                CS.actualizarDiasConsumidos(Convert.ToInt32(lblFUP.Value), lblVersion.Value, Convert.ToInt32(lblDReal.Text), Convert.ToInt32(txtDConsumidos.Text));
                            }                            
                        }
                        cargarDatos(Session["SIATidViaje"].ToString(), sender, e);
                        mensajeVentana("Se actualizado correctamente!!");
                    }
                    else { mensajeVentana(menConf); }
                }
                else { mensajeVentana("Hace falta contactos!! Cada cliente debe de tener un contanto relaccionado, gracias!"); }
            }
            else
            {
                mensajeVentana(mensaje);
            }
        }
        //Restablece el color de los labes
        private void restaColor()
        {
            lblDReal.BackColor = Color.Transparent;
            lblDReal.ForeColor = Color.White;
            lblDReal.Font.Bold = false;
        }
        //Me cambia el color si es mayor los dias reales
        private void color()
        {
            restaColor();
            //lblDReal.Text = (int.Parse(lblDInv.Text) + int.Parse(lblDImp.Text)).ToString();
            if (int.Parse(lblDReal.Text) > int.Parse(txtDTol.Text))
            {
                lblDReal.BackColor = Color.Red; lblDReal.Font.Bold = true; lblDReal.ForeColor = Color.White;
            }
            else
            {
                lblDReal.BackColor = Color.Blue; lblDReal.Font.Bold = true; lblDReal.ForeColor = Color.White;
            }
        }
        //Calcula los dias de las fechas del tecnico
        private void calTodosDias(Label label, String fechaIni, String fechaFin)
        {
            try
            {
                if (fechaIni == "" || fechaFin == "")
                {
                    label.Text = "0";
                }
                else
                {
                    if (DateTime.Parse(fechaFin) < DateTime.Parse(fechaIni))
                    {
                        label.Text = "0";
                    }
                    else
                    {
                        label.Text = devuelveDias(fechaIni, fechaFin).ToString();
                    }
                }
            }
            catch
            {
                label.Text = "0";
            }
        }
        //Calcula todos los dias
        //private void calDiasInvImp()
        //{
        //    if (Session["SIATTipoActA"].ToString() == "inv")
        //    {
        //        calTodosDias(txtDInvCom, txtFLlegObra.Text, txtFFinObra.Text);
        //        color();
        //    }
        //    else if (Session["SIATTipoActA"].ToString() == "imp")
        //    {
        //        calTodosDias(txtDInvCom, txtFLlegObra.Text, txtFFinObra.Text);
        //        color();
        //    }
        //    else
        //    {
        //        try { lblDImp.Text = (int.Parse(lblDReal.Text) - int.Parse(txtDInvCom.Text)).ToString(); }
        //        catch { lblDImp.Text = "0"; }
        //    }
        //    calTodosDias(txtDInvCom, txtFIniAct.Text, txtFFinAct.Text);
        //}
        //Recarga el contacto despues de haber sido agregado en el modulo de contacto
        public String recargarCont(String cliente)
        {
            String combo = "<option value=Seleccionar>Seleccionar</option>";
            if (cliente != "")
            {
                DataTable tabla = CS.cargarContactos("AND (contacto_cliente.ccl_cli_id = " + cliente + ")", "");
                foreach (DataRow row in tabla.Rows)
                {
                    combo = combo + "<option value=" + row[0] + " selected>" + row[1] + "</option>";
                }
            }
            return "opener.window.recargarComboCont('" + combo + "'); ";//con el opener.window llamo al metodo del asp padre
        }
        //Devuelve los dias entre dos fechas
        private int devuelveDias(String ini, String fin)
        {
            try
            {
                DateTime fIni = DateTime.Parse(ini);
                DateTime fFin = DateTime.Parse(fin);
                TimeSpan ts = fFin - fIni;

                int differenceInDays = ts.Days + 1;
                return differenceInDays;
            }
            catch { return 0; }
        }
        #endregion

        #region VIAJE
        //combo de la actividad
        protected void cboActividad_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (Session["SIATOrdenes"] == null) { limCamActiv(); }
            btnActVis.Visible = false;
            Session["SIATidViaje"] = "";
            if (cboActividad.SelectedItem.ToString() != "Seleccionar")
            {
                chkCotizacion.Checked = false;
                                                                                      // 0|1|2|3|4
                String[] actividad = cboActividad.SelectedValue.ToString().Split('|');//siat_act_id|siat_act_pantalla|siat_act_letra|siat_act_inven|siat_act_imple
                if ((actividad[1].ToString() == "1") || (actividad[1].ToString() == "6"))
                {
                    ocultarPaneles(false);
                    lblOF.Visible = true;
                    txtOF.Visible = true;
                    lblConsecutivo.Visible = true;
                    txtConsecutivo.Visible = true;                 
                }
                else if (actividad[1].ToString() == "2")
                {
                    PanelOtraAct.Visible = true;
                    PanelOtraAct.GroupingText = cboActividad.SelectedItem.ToString() + "(" + actividad[2] + ")";
                    PanelVisita.Visible = false;
                    //btnBuscarVis.Visible = false;
                    //btnOrdenes.Visible = false;
                    lblOF.Visible = false;
                    txtOF.Visible = false;
                    lblConsecutivo.Visible = false;
                    txtConsecutivo.Visible = false;
                    lblFUP.Value = null;
                    lblMensajeFup.Text = "";
                    lblVersion.Value = null;
                    txtOF.Text = "";
                    txtConsecutivo.Text = "";
                }
            }
            else
            {
                ocultarPaneles(false);
                lblOF.Visible = false;
                txtOF.Visible = false;
                lblConsecutivo.Visible = false;
                txtConsecutivo.Visible = false;
                lblFUP.Value = null;
                lblMensajeFup.Text = "";
                lblVersion.Value = null;
                txtOF.Text = "";
                txtConsecutivo.Text = "";
            }
        }
        //combo del tecnico
        protected void cboTecnico_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarDatosTec(cboTecnico.SelectedValue.ToString());
        }
        //texbo del cliente
        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {
            if (lblIdCliente.Value != "")
            {   //AND (contacto_cliente.ccl_cli_id = 337) AND (contacto_cliente.ccl_id = 1)
                DataTable dt = new DataTable();                    
                dt = CS.consultarMonedaCliente(Convert.ToInt32(lblIdCliente.Value));
                if (dt.Rows.Count > 0)
                {
                    lblIdMoneda.Value = dt.Rows[0]["mon_id"].ToString();
                    lblIdPais.Value = dt.Rows[0]["pais"].ToString();
                }
                else
                {
                    lblIdMoneda.Value = null;
                    lblIdPais.Value = null; 
                }
                cboOF.Items.Clear();
                cargarCombo(CS.cargarContactos("AND (contacto_cliente.ccl_cli_id = " + lblIdCliente.Value + ")", ""), cboContacto, 0, 1);//Combo contacto
                //cargarCombo(CS.cargarObras(" (cliente.cli_id = " + lblIdCliente.Value + ")"), cboObra, 2, 1);//Combo obra
                listObra.Items.Clear();
                listOF.Items.Clear();
                listOFNovObs.Items.Clear();
                listCont.Items.Clear();
                //txtHotel.Text = "";
                //txtTelefono.Text = "";
                //txtDireccion.Text = "";
            }
            else if (lblIdCliente.Value == "" || txtCliente.Text == "") { cboObra.Items.Clear(); cboObra.Items.Add("Seleccionar"); }
        }
        //agrega obra
        protected void btnAggObra_Click(object sender, EventArgs e)
        {
            if (cboObra.SelectedItem.ToString() != "Seleccionar" && listObra.Items.Count <= 1)
            {
                String[] idObra = cboObra.SelectedValue.ToString().Split('|');
                agregarLista(listObra, idObra[1], idObra[0]);//obra
            }
            else
            {
                mensajeVentana("No es posible añadir más de una obra. Gracias");
            }
        }
        //elimina obra
        protected void btnEliObra_Click(object sender, EventArgs e)
        {
            int idObra = listObra.SelectedIndex;
            if (idObra >= 0)
            {
                DataTable ofs = CS.cargarOf("AND  (obra.obr_id = " + listObra.SelectedValue.ToString() + ")", "");
                ListBox listReal = new ListBox();
                foreach (ListItem li in listOF.Items)
                {
                    int agg = 0;//Inicio la variable en 0 que significa que no agrege
                    foreach (DataRow row in ofs.Rows)
                    {
                        if (li.Value == row["idOF"].ToString())//Si encuentra la of, para el ciclo y mando la variable agg en 0 para que no agrege
                        {
                            agg = 0;
                            break;
                        }
                        else
                        {
                            agg = agg + 1;//Si no existe, va contanto hasta que pare y sea mayor q cero o que pare y cambie la variable en 0
                        }
                    }
                    if (agg >= 1)//si agg esta en 0 no agrego, si mayor agrego solo una vez
                    {
                        listReal.Items.Add(new ListItem(li.Text, li.Value));
                    }
                }
                listOF.Items.Clear();//Elimino todas las ofs
                foreach (ListItem li2 in listReal.Items)//Vuelvo y agrego las ofs que son
                {
                    listOF.Items.Add(new ListItem(li2.Text, li2.Value));
                }
            }
            //Borro el item seleccionado
            eliminarLista(listObra);
        }
        //agrega of
        protected void btnAggOF_Click(object sender, EventArgs e)
        {
            if (cboOF.SelectedItem.ToString() != "Seleccionar")
            {
                agregarLista(listOF, cboOF.SelectedItem.ToString(), cboOF.SelectedValue.ToString());//OF
                DataTable obra = null;
                obra = CS.cargarObras(" (Orden.Id_Ofa = " + cboOF.SelectedValue.ToString() + ")");
                foreach (DataRow row in obra.Rows)
                {
                    agregarLista(listObra, row["datosObra"].ToString(), row["idObra"].ToString());//Obra
                }
            }
        }
        //elimiana of
        protected void btnEliOF_Click(object sender, EventArgs e)
        {   //Borro el item seleccionado
            eliminarLista(listOF);
        }
        //agrega contacto
        protected void btnAggCont_Click(object sender, EventArgs e)
        {
            if (lblIdConta.Value != "Seleccionar" || lblIdConta.Value != "")
            {
                if (listCont.Items.Count >= 1)//verifico si hay algo en la lista que ingresa en la variable list
                {
                    String existe = "NO";
                    foreach (ListItem li in listCont.Items)//lleno y recorro un list por medio del la lista que esta entrando
                    {
                        String[] idLista = li.Value.Split('|');
                        //0 =  id cliente / 1 = id del contacto
                        if (idLista[0] == lblIdCliente.Value)//verifico si el id ya esta en la lista
                        {
                            existe = "SI";
                        }
                    }
                    if (existe == "NO")//agrego a la lista el item y el value si no existe en lista
                    {
                        listCont.Items.Add(new ListItem(lblNomConta.Value, lblIdCliente.Value + "|" + lblIdConta.Value));
                    }
                }
                else//si no hay agrego inmediatamente
                {
                    listCont.Items.Add(new ListItem(lblNomConta.Value, lblIdCliente.Value + "|" + lblIdConta.Value));
                }
            }
            else { mensajeVentana("Por favor seleccione un contacto, gracias!"); }
        }
        //elimina contacto
        protected void btnEliCont_Click(object sender, EventArgs e)
        {
            eliminarLista(listCont);
        }
        //combo de obras
        protected void cboObra_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboObra.SelectedItem.ToString() != "Seleccionar")
            {
                String[] idObra = cboObra.SelectedValue.ToString().Split('|');
                cargarCombo(CS.cargarOf("AND  (obra.obr_id = " + idObra[0] + ")", ""), cboOF, 0, 1);//Combo Ofs
            }
            else
            {
                cboOF.Items.Clear();
            }
        }
        //busca las visitas para poder editar
        protected void btnBuscarVis_Click(object sender, EventArgs e)
        {
            //txtFiltroOF.Text = "";
            cargarTabla(grdViajes, null);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script41", "abrirPopup('PopupBuscaVis')", true);
        }
        //guarda la visita
        protected void btnGuardarVis_Click(object sender, EventArgs e)
        {
            String mensaje = validaCamposGen();
            if (mensaje == "OK")
            {
                mensaje = validaCamposVis();
                if (mensaje == "OK")
                {
                    Boolean existe = true;
                    existe = CS.existeViajeTec(cboTecnico.SelectedValue.ToString(), txtFIniAct.Text, txtFFinAct.Text, "");
                    if (!existe)
                    {
                        String diasInv = txtDInvCom.Text;
                        String idInv = "NULL";
                        String dPend = lblDPend.Text;
                        if (Session["SIATTipoActA"].ToString() == "comp")
                        {
                            diasInv = txtDInvCom.Text;
                        }                        
                        if (diasInv != "")
                        {
                            if (int.Parse(diasInv) >= 0)
                            {
                                if (Session["SIATTipoActA"].ToString() == "imp")
                                {
                                    existe = true;
                                    idInv = Session["SIATidViaje"].ToString();
                                    existe = CS.existeViajeTecInv(idInv, txtFIniAct.Text, txtFFinAct.Text);
                                    if (existe == false)
                                    {
                                        guardar(diasInv, idInv, dPend, sender, e);
                                    }
                                    else { mensajeVentana("No puede estar en el mismo rango de fechas del inventario!"); }
                                }
                                else
                                {
                                    guardar(diasInv, idInv, dPend, sender, e);
                                }
                            }
                            else { mensajeVentana("El campo de los días de inventario no puede estar en cero (0), gracias!"); }
                        }
                        else { mensajeVentana("El campo de los días de inventario no puede estar vacío, gracias!"); }
                    }
                    else { mensajeVentana("El técnico ya tiene un viaje asignado a ese rango de fechas, por favor ingrese otro rango, gracias!"); }
                }
                else { mensajeVentana(mensaje); }
            }
            else { mensajeVentana(mensaje); }
        }
        //edita la visita
        protected void btnActVis_Click(object sender, EventArgs e)
        {
            String mensaje = validaCamposGen();
            if (mensaje == "OK")
            {
                mensaje = validaCamposVis();
                if (mensaje == "OK")
                {
                    String filtroCamp = "";
                    String diasInv = txtDInvCom.Text;
                    if (Session["SIATTipoActA"].ToString() == "comp")
                    {
                        diasInv = txtDInvCom.Text;
                    }
                    if (diasInv != "")
                    {
                        if (int.Parse(diasInv) >= 0)
                        {
                            filtroCamp = ", siat_via_tec_id = " + cboTecnico.SelectedValue.ToString() + ", siat_via_fechaInicio = '" + txtFIniAct.Text + "', siat_via_fechaFin = '" + txtFFinAct.Text + "', siat_via_tipoAct = '" + Session["SIATTipoActA"].ToString() + "', siat_via_dTotal = " + txtDTol.Text + ", siat_via_dReal = " + lblDReal.Text + ", siat_via_dInv = " + diasInv + ", siat_via_dImp = " + lblDImp.Text + ", siat_via_dpend = " + lblDPend.Text + ", siat_via_observacion = '" + txtObserva.Text + "'";
                            //&& Session["SIATfechaFinVia"].ToString() == txtFFinAct.Text
                            if (Session["SIATfechaIniVia"].ToString() == txtFIniAct.Text)
                            {
                                actualizar(filtroCamp, sender, e);
                            }
                            else
                            {
                                Boolean existe = true;
                                existe = CS.existeViajeTec(cboTecnico.SelectedValue.ToString(), txtFIniAct.Text, txtFFinAct.Text, " AND (siat_via_id <> " + Session["SIATidViaje"].ToString() + ") ");
                                if (existe == false)
                                {
                                    actualizar(filtroCamp, sender, e);
                                }
                                else { mensajeVentana("El técnico ya tiene un viaje asignado a ese rango de fechas, por favor ingrese otro rango, gracias!"); }
                            }
                        }
                        else { mensajeVentana("El campo de los días de inventario no puede estar en cero (0), gracias!"); }
                    }
                    else { mensajeVentana("El campo de los días de inventario no puede estar vacío, gracias!"); }
                }
                else { mensajeVentana(mensaje); }
            }
            else
            {
                mensajeVentana(mensaje);
            }
        }
        //cancela la visita
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            String mensaje = CS.editarEstadoV(4, Session["SIATidViaje"].ToString(), ", siat_via_fecha_can = SYSDATETIME(), siat_via_usu_can = '" + Session["usuario"].ToString() + "' ");
            if (mensaje == "OK")
            {
                CS.actualizarDiasConsumidos(Convert.ToInt32(lblFUP.Value), lblVersion.Value, 0, 0);

                string version = lblVersion.Value;
                int evento = 39;
                string correos = "";
                CorreoSIAT(Convert.ToInt32(txtConsecutivo.Text), version, evento, correos, Convert.ToInt32(lblIdPais.Value), Convert.ToInt32(cboServicio.SelectedValue));
               
                ocultarPaneles(false);
                ocultarBotones(false);
                limCampos();
                cargarCombos();
                enableCampGeneral(true);
                enableCampActiv(true);
                btnGuardarVis.Visible = true;
                btnNuevo.Visible = false;                
                mensajeVentana("Se ha cancelado la visita correctamente!!");
            }
            else { mensajeVentana(mensaje); }
        }
        //confirma la visita
        protected void btnConfi_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(lblFUP.Value) && !String.IsNullOrEmpty(lblVersion.Value))
            {
                String mensaje = CS.editarEstadoV(2, Session["SIATidViaje"].ToString(), ", siat_via_fecha_confi = SYSDATETIME(), siat_via_usu_confi = '" + Session["usuario"].ToString() + "' ");
                if (mensaje == "OK")
                {
                    CS.actualizarDiasConsumidos(int.Parse(lblFUP.Value), lblVersion.Value, int.Parse(lblDReal.Text), int.Parse(txtDConsumidos.Text));
                    ocultarBotones(false);
                    enableCampGeneral(false);
                    enableCampActiv(false);
                    btnCerrar.Visible = true;
                    btnNovedad.Visible = true;
                    btnCostos.Visible = true;
                    btnCancelar.Visible = true;
                    btnNuevo.Visible = true;
                    btnActVis.Visible = false;
                    txtFFinAct.Enabled = false;
                    txtFFinObra.Enabled = false;
                    txtDInvCom.Enabled = false;
                    cargarDatos(Session["SIATidViaje"].ToString(), sender, e);

                    string version = lblVersion.Value;
                    int evento = 37;
                    string correos = "";
                    CorreoSIAT(Convert.ToInt32(txtConsecutivo.Text), version, evento, correos, Convert.ToInt32(lblIdPais.Value), Convert.ToInt32(cboServicio.SelectedValue));

                    mensajeVentana("Se ha confirmado la visita correctamente!!");
                    //**************   Inserta en tabla encuestas   ******************
                    string ContactoId = "";
                    string ContactoObra = "";
                    int TipoEncuesta = 2;
                    String[] actividad = cboActividad.SelectedValue.ToString().Split('|');//siat_act_id|siat_act_pantalla|siat_act_letra|siat_act_inven|siat_act_imple
                    TipoEncuesta = Convert.ToInt32(actividad[0].ToString());
                    // para reconocer la Actividad tipo 6 Implementación y usar la Encuesta Tipo 3 EII Abril 29 de 2022
                    if (TipoEncuesta == 6)
                    {
                        TipoEncuesta = 3;
                    }
                    else
                    {
                        TipoEncuesta = 2;
                    }
                    if (listCont.Items.Count > 0) {
                        listCont.SelectedIndex = 0;
                        ContactoId = "-1";  //listCont.Items[0].Value.ToString();
                        ContactoObra = listCont.Items[0].Text;
                        //enviar la fecha de txtFFinObra.Text y el primer contacto de la lista
                        mensaje = CS.CrearEncuestaPostVisita(int.Parse(lblFUP.Value), lblVersion.Value, Session["SIATidViaje"].ToString(), ContactoId, ContactoObra, TipoEncuesta);
                        mensajeVentana("Se ha Creado-Actualizado datos de la Encuesta Post-visita correctamente!!");
                    }
                    //*************   Fin Inserta en tabla encuestas   ****************
                }
                else { mensajeVentana(mensaje); }
            }
            else { mensajeVentana("Por favor cargue nuevamente el viaje. Gracias"); }
           
        }
        //cierra la visita
        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            DataTable costos = CS.consultaCosR(Session["SIATidViaje"].ToString());
            if (costos.Rows.Count > 0)
            {                
                String mensaje = CS.editarEstadoV(3, Session["SIATidViaje"].ToString(), ", siat_via_fecha_cerr = SYSDATETIME(), siat_via_usu_cerr = '" + Session["usuario"].ToString() + "' ");
                if (mensaje == "OK")
                {
                    string version = lblVersion.Value;
                    int evento = 38;
                    string correos = "";
                    CorreoSIAT(Convert.ToInt32(txtConsecutivo.Text), version, evento, correos, Convert.ToInt32(lblIdPais.Value), Convert.ToInt32(cboServicio.SelectedValue));

                    ocultarPaneles(false);
                    ocultarBotones(false);
                    enableCampActiv(true);
                    enableCampGeneral(true);
                    limCampos();
                    cargarCombos();
                    btnGuardarVis.Visible = true;
                    btnNuevo.Visible = true;
                    cargarDatos(Session["SIATidViaje"].ToString(), sender, e);
                    
                    mensajeVentana("Se ha cerrado la visita correctamente!!");
                }
                else { mensajeVentana(mensaje); }
            }
            else { mensajeVentana("Para cerrar debe de ingresar los costos reales, gracias!"); }
        }
        //limpia para crear un nuevo viaje dado una of
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            if(!String.IsNullOrEmpty(lblFUP.Value) && !String.IsNullOrEmpty(lblVersion.Value))
            {
                txtConsecutivo.Text = "";
                limCamActiv();
                limCamGeneral();
                cargarCombos();
                cboActividad.SelectedIndex = 1;
                lblMensajeFup.Text = "PROYECTO FUP: " + lblFUP.Value + " VRS: " + lblVersion.Value;
                poblarImplementacionObra(lblFUP.Value, lblVersion.Value, sender, e);
                ocultarBotones(false);
                btnGuardarVis.Visible = true;
                enableCampGeneral(true);
                enableCampActiv(true);
            }
            else { mensajeVentana("Seleccione nuevamente la OF. Gracias"); }            
        }
        //combo contacto
        protected void cboContacto_SelectedIndexChanged(object sender, EventArgs e)
        {
            String idCombo = cboContacto.SelectedValue.ToString();
            cargarCombo(CS.cargarContactos("AND (contacto_cliente.ccl_cli_id = " + lblIdCliente.Value + ")", ""), cboContacto, 0, 1);
            cboContacto.SelectedIndex = cboContacto.Items.IndexOf(cboContacto.Items.FindByValue(idCombo));
        }
        //Filtras las ofs para la busqueda
        protected void btnBuscarOF_Click(object sender, EventArgs e)
        {
            DataTable OFs = null;
            //String filtroBase = " (siat_viaje.siat_via_cargo = 'Tecnico')  AND (siat_of_viaje.siat_of_viaje_activo = 1) AND  (Orden.Numero + '-' + Orden.ano LIKE '%" + txtFiltroOF.Text + "%') ";
            String filtroBase = " (siat_viaje.siat_via_cargo = 'Tecnico')  AND (siat_of_viaje.siat_of_viaje_activo = 1) AND  (Orden.Numero + '-' + Orden.ano LIKE '%" + txtOF.Text + "%') AND (siat_viaje.siat_estado_viaje_id <> 4) ";

            String filtro = "";
            if (Session["SIATTipoActA"].ToString() == "inv")
            {
                filtro = filtroBase + "  AND  (siat_viaje.siat_estado_viaje_id = 1)  AND (siat_act_inven = 1) OR "
                + filtroBase + " AND (siat_viaje.siat_estado_viaje_id = 2)  AND (siat_act_inven = 1) ";
            }
            else if (Session["SIATTipoActA"].ToString() == "imp")
            {
                filtro = filtroBase + "  AND  (siat_viaje.siat_estado_viaje_id = 3)  AND (siat_act_inven = 1) AND (siat_via_idInv IS NULL) "
                + " AND (siat_viaje.siat_via_id NOT IN (SELECT  siat_via_idInv FROM siat_viaje WHERE (siat_via_idInv  IS NOT NULL))) OR "
                + filtroBase + " AND (siat_viaje.siat_estado_viaje_id = 1)  AND (siat_act_imple = 1) OR "
                + filtroBase + " AND (siat_viaje.siat_estado_viaje_id = 2)  AND (siat_act_imple = 1) ";
            }
            else
            {
                filtro = filtroBase + " AND (siat_act_comple = 1) ";
                //filtroBase + "  AND  (siat_viaje.siat_estado_viaje_id = 1) AND (siat_act_comple = 1)  OR "

            }
            OFs = CS.filtrarOF(" WHERE " + filtro);
            cargarTabla(grdViajes, OFs);
        }
        //Identifica que fila ha sido seleccionada
        protected void btnSelOrden_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = sender as ImageButton;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            String idViaje = this.grdViajes.DataKeys[row.RowIndex].Values["idViaje"].ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script41", "cerrarPopup('PopupBuscaVis')", true);
            cargarDatos(idViaje, sender, e);
        }
        //guarda otra actividad
        protected void btnGuardarAct_Click(object sender, EventArgs e)
        {
            String mensaje = validaCamposGen();
            if (mensaje == "OK")
            {
                if (txtObsAct.Text != "")
                {
                    Boolean existe = true;
                    existe = CS.existeViajeTec(cboTecnico.SelectedValue.ToString(), txtFIniAct.Text, txtFFinAct.Text, "");
                    if (existe == false)
                    {
                        String[] activ = cboActividad.SelectedValue.ToString().Split('|');
                        mensaje = CS.insertarAct(Session["usuario"].ToString(), activ[0], cboTecnico.SelectedValue.ToString(), txtFIniAct.Text, txtFFinAct.Text, txtObsAct.Text.Replace("\r\n", " ").Replace("\n", " "));
                        if (mensaje == "OK")
                        {
                            mensajeVentana("Se ha ingresado correctamente!!");
                            ocultarPaneles(false);
                            limCampos();
                            cargarCombos();
                            btnGuardarVis.Visible = true;
                        }
                        else { mensajeVentana(mensaje); }
                    }
                    else { mensajeVentana("El técnico ya tiene una actividad asignada a ese rango de fechas, por favor ingrese otro rango, gracias!"); }
                }
                else { mensajeVentana("Por favor ingrese una observarción, gracias"); }
            }
            else { mensajeVentana(mensaje); }
        }
        //boton para agregar las novedades
        protected void btnNovedad_Click(object sender, EventArgs e)
        {
            cargarCombo(CS.cargarOfViajes("AND (siat_of_viaje.siat_of_viaje_via_id = " + Session["SIATidViaje"].ToString() + ")"), cboOrdenNov, 0, 1);//Combo novedades
            cargarCombo(CS.cargarNovedades(""), cboNovedad, 0, 1);//Combo novedades
            llenarList(CS.consultaNovVia(Session["SIATidViaje"].ToString()), listOFNovObs, 1, 0);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script41", "abrirPopup('PopupNov')", true);
        }
        //boton para agregar los costos
        protected void btnCostos_Click(object sender, EventArgs e)
        {
            limCamCos();
            //DataTable costosR = CS.consultaCosR(Session["SIATidViaje"].ToString());
            if (!String.IsNullOrEmpty(txtConsecutivo.Text) && !String.IsNullOrEmpty(lblIdPais.Value))
            {
                PanelCostos.GroupingText = "COSTOS CP-" + txtConsecutivo.Text; 
                DataTable costosR = CS.buscarCostoRealErp(int.Parse(txtConsecutivo.Text), Convert.ToInt32(lblIdPais.Value));
                if (costosR.Rows.Count > 0)
                {
                    Session["SIATCostosReal"] = "SI";

                    foreach (DataRow row in costosR.Rows)
                    {
                        if (row["grupo"].ToString() == "1")
                        {
                            lblRealHotel.Text = (decimal.Parse(row["valor"].ToString())).ToString("N0");
                        }

                        else if (row["grupo"].ToString() == "2")
                        {
                            lblRealAli.Text = (decimal.Parse(row["valor"].ToString())).ToString("N0");
                        }

                        else if (row["grupo"].ToString() == "3")
                        {
                            lblRealTiq.Text = (decimal.Parse(row["valor"].ToString())).ToString("N0");
                        }

                        else if (row["grupo"].ToString() == "4")
                        {
                            lblRealPenal.Text = (decimal.Parse(row["valor"].ToString())).ToString("N0");
                        }

                        else if (row["grupo"].ToString() == "5")
                        {
                            lblRealTran.Text = (decimal.Parse(row["valor"].ToString())).ToString("N0");
                        }

                        else if (row["grupo"].ToString() == "6")
                        {
                            lblRealLlam.Text = (decimal.Parse(row["valor"].ToString())).ToString("N0");
                        }
                        else if (row["grupo"].ToString() == "7")
                        {
                            lblRealLav.Text = (decimal.Parse(row["valor"].ToString())).ToString("N0");
                        }
                        else if (row["grupo"].ToString() == "8")
                        {
                            lblRealOtros.Text = (decimal.Parse(row["valor"].ToString())).ToString("N0");
                        }
                        //txtRealHotel.Text = (decimal.Parse(row["hotel"].ToString()) * 1).ToString("N0");
                        //txtRealTiq.Text = (decimal.Parse(row["tiq"].ToString())).ToString("N0");
                        //txtRealAli.Text = (decimal.Parse(row["ali"].ToString())).ToString("N0");
                        //txtRealTranInt.Text = (decimal.Parse(row["transInt"].ToString())).ToString("N0");
                        //txtRealTranAer.Text = (decimal.Parse(row["transAereo"].ToString())).ToString("N0");
                        //txtRealLlam.Text = (decimal.Parse(row["llam"].ToString())).ToString("N0");
                        //txtRealLav.Text = (decimal.Parse(row["lav"].ToString())).ToString("N0");
                        //txtRealPenal.Text = (decimal.Parse(row["penal"].ToString())).ToString("N0");
                        //txtRealOtros.Text = (decimal.Parse(row["otros"].ToString())).ToString("N0");
                        //txtRealTrm.Text = (decimal.Parse(row["trm"].ToString())).ToString("N0");
                        //txtRealObs.Text = row["obs"].ToString();
                    }
                }
                else { Session["SIATCostosReal"] = "NO"; }

                //String listOFs = listaString(listOF);
                //DataTable paisCos = CS.cargarPaisCostos(listOFs);
                //if (paisCos.Rows.Count > 0)
                //{
                //    foreach (DataRow row0 in paisCos.Rows)
                //    {
                //        DataTable costosP = null;
                //        DataTable zonaC = CS.cargarZonasC(row0["idPais"].ToString());
                //        if (zonaC.Rows.Count > 0)
                //        {
                //            costosP = CS.consultaCosP(" WHERE  (siat_cp_zona_id = " + row0["idZCiu"].ToString() + ")");
                //            lblNomPais.Text = row0["nomPais"].ToString().ToUpper() + " - " + row0["nomZCiu"].ToString().ToUpper();
                //        }
                //        else
                //        {
                //            costosP = CS.consultaCosP(" WHERE  (siat_cp_pai_id = " + row0["idPais"].ToString() + ")");
                //            lblNomPais.Text = row0["nomPais"].ToString().ToUpper();
                //        }
                //        foreach (DataRow row1 in costosP.Rows)
                //        {
                //            lblPlanHotel.Text = (decimal.Parse(row1["hotel"].ToString()) * 1).ToString("N0");
                //            lblPlanTiq.Text = (decimal.Parse(row1["tiq"].ToString())).ToString("N0");
                //            lblPlanAli.Text = (decimal.Parse(row1["ali"].ToString())).ToString("N0");
                //            lblPlanTranInt.Text = (decimal.Parse(row1["transInt"].ToString())).ToString("N0");
                //            lblPlanTranAer.Text = (decimal.Parse(row1["transAereo"].ToString())).ToString("N0");
                //            lblPlanLLam.Text = (decimal.Parse(row1["llam"].ToString())).ToString("N0");
                //            lblPlanLav.Text = (decimal.Parse(row1["lav"].ToString())).ToString("N0");
                //            lblPlanPenal.Text = (decimal.Parse(row1["penal"].ToString())).ToString("N0");
                //            lblPlanOtros.Text = (decimal.Parse(row1["otros"].ToString())).ToString("N0");
                //        }
                //    }
                //}
                decimal trm = CS.consultarTRM(int.Parse(txtConsecutivo.Text));
                lblRealTrm.Text = trm.ToString("N0");
                DataTable costosP = new DataTable();
                costosP = CS.consultaCosP(" WHERE  (siat_cp_pai_id = " + int.Parse(lblIdPais.Value) + ")");

                lblNomPais.Text = CS.consultarPais(int.Parse(lblIdPais.Value)).ToString().ToUpper();

                foreach (DataRow row1 in costosP.Rows)
                {
                    lblPlanHotel.Text = ((decimal.Parse(row1["hotel"].ToString()) * trm) * decimal.Parse(lblDReal.Text)).ToString("N0");
                    lblPlanTiq.Text = (decimal.Parse(row1["tiq"].ToString()) * trm).ToString("N0");
                    lblPlanAli.Text = ((decimal.Parse(row1["ali"].ToString()) * trm) * decimal.Parse(lblDReal.Text)).ToString("N0");
                    decimal transporte = ((decimal.Parse(row1["transAereo"].ToString()) + decimal.Parse(row1["transInt"].ToString())) * trm) * decimal.Parse(lblDReal.Text);
                    lblPlanTran.Text = transporte.ToString("N0");
                    //lblPlanTranAer.Text = (decimal.Parse(row1["transAereo"].ToString())).ToString("N0");
                    lblPlanLLam.Text = ((decimal.Parse(row1["llam"].ToString()) * trm) * decimal.Parse(lblDReal.Text)).ToString("N0");
                    lblPlanLav.Text = ((decimal.Parse(row1["lav"].ToString()) * trm) * decimal.Parse(lblDReal.Text)).ToString("N0");
                    lblPlanPenal.Text = (decimal.Parse(row1["penal"].ToString()) * trm).ToString("N0");
                    lblPlanOtros.Text = (decimal.Parse(row1["otros"].ToString()) * trm).ToString("N0");
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script41", "abrirPopup('PopupCos')", true);
            }

            else
            {
                mensajeVentana("Por favor cargue nuevamente el viaje. Gracias");
            }            
        }
        //textbox de dias totales
        protected void txtDTol_TextChanged(object sender, EventArgs e)
        {
            if (chkCotizacion.Checked)
            {
                ocultarParametrosCotizacion(true);
            }
            else
            {
                //si dias consumidos es >0 haga esto 
           
                    int diasFi = devuelveDias(txtFLlegObra.Text, txtFFinObra.Text); //agregada
                    lblDReal.Text = diasFi.ToString(); //agregada

                    ocultarParametrosCotizacion(false);
                    int pendientes = Convert.ToInt32(txtDTol.Text) - Convert.ToInt32(txtDConsumidos.Text);
                   // int pendientes = Convert.ToInt32(txtDTol.Text) - Convert.ToInt32(lblDReal.Text);  //agregada
                    lblDPend.Text = pendientes.ToString();
                    Session["dPend"] = lblDPend.Text;
                              
                //if (!String.IsNullOrEmpty(txtFLlegObra.Text.Trim()) && !String.IsNullOrEmpty(txtFFinObra.Text.Trim()))
                //{
                //    int dpend = 0;
                //    if (!String.IsNullOrEmpty(Session["dPend"].ToString().Trim()))
                //        dpend = Convert.ToInt32(Session["dPend"]);

                //    int diasPendientes = dpend - int.Parse(lblDReal.Text);
                //    lblDPend.Text = diasPendientes.ToString();
                //}
            }
            
            int diasF = devuelveDias(txtFLlegObra.Text, txtFFinObra.Text);
            lblDReal.Text = diasF.ToString();
            lblDImp.Text = (int.Parse(lblDReal.Text) - int.Parse(txtDInvCom.Text)).ToString();

            //color();
            //if (Session["SIATTipoActA"].ToString() == "comp")
            //{                
            //    try
            //    {
            //        if (txtFLlegObra.Text != "" && txtFFinObra.Text != "")
            //        {                     
            //            if (txtDTol.Text != "")
            //            {
            //                int diasF = devuelveDias(txtFLlegObra.Text, txtFFinObra.Text);
            //                lblDPend.Text = txtDTol.Text;
            //                if (int.Parse(txtDTol.Text) > diasF)
            //                {
            //                    lblDReal.BackColor = Color.Red;
            //                    lblDReal.Font.Bold = true;
            //                    lblDReal.ForeColor = Color.White;
            //                }
            //                else
            //                {
            //                    lblDReal.BackColor = Color.Blue;
            //                    lblDReal.Font.Bold = true;
            //                    lblDReal.ForeColor = Color.White;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            lblDInv.Text = "";
            //            lblDImp.Text = "";
            //            lblDReal.Text = "";
            //            txtDTol.Text = "";
            //            //lblDReal.BackColor = Color.Red;
            //            //lblDReal.Font.Bold = true;
            //            //lblDReal.ForeColor = Color.White;
            //        }
            //    }
            //    catch {  }
            //}
        }
        //textbox de fecha llegada del tecnico 
        protected void txtFLlegObra_TextChanged(object sender, EventArgs e)
        {
            string mensaje = "";
            if (!String.IsNullOrEmpty(txtFIniAct.Text) && !String.IsNullOrEmpty(txtFLlegObra.Text))
            {
                DateTime d1 = Convert.ToDateTime(txtFIniAct.Text);
                DateTime d2 = Convert.ToDateTime(txtFLlegObra.Text);
                int result = DateTime.Compare(d2, d1);
                if (result >= 0)
                {
                    lblDReal.Text = result.ToString(); 
                }
                else
                {
                    txtFLlegObra.Text = "";
                    mensaje = "Verifique que la Fecha LLeg. Tec sea mayor o igual a la Fecha Inicial Act.";
                    mensajeVentana(mensaje);
                }
            }

            else
            {
                mensaje = "Verifique que los días totales y las fechas: Fecha Inicial Act. y Fecha LLeg. Tec, no estén vacías";
                mensajeVentana(mensaje);
            }          
        }

        private void fechas()
        {
            string mensaje = "";
            if (!String.IsNullOrEmpty(txtFFinAct.Text) && !String.IsNullOrEmpty(txtFFinObra.Text) && !String.IsNullOrEmpty(txtFLlegObra.Text) && !String.IsNullOrEmpty(txtDTol.Text))
            {
                DateTime d1 = Convert.ToDateTime(txtFFinAct.Text);
                DateTime d2 = Convert.ToDateTime(txtFFinObra.Text);
                int result = DateTime.Compare(d2, d1);
                if (result <= 0)
                {
                    int diasF = devuelveDias(txtFLlegObra.Text, txtFFinObra.Text);

                    lblDReal.Text = diasF.ToString();
                    lblDImp.Text = (int.Parse(lblDReal.Text) - int.Parse(txtDInvCom.Text)).ToString();
                    int diasPendientes = int.Parse(txtDTol.Text) - int.Parse(txtDConsumidos.Text); 
                   // int diasPendientes = int.Parse(txtDTol.Text) - int.Parse(lblDReal.Text); //agregada
                    if (diasPendientes < 0)
                    {                        
                        diasPendientes = 0;
                    }
                    lblDPend.Text = diasPendientes.ToString();

                    if (int.Parse(txtDTol.Text) > diasF)
                    {
                        lblDReal.BackColor = Color.Blue;
                        lblDReal.Font.Bold = true;
                        lblDReal.ForeColor = Color.White;
                    }
                    else
                    {
                        lblDReal.BackColor = Color.Red;
                        lblDReal.Font.Bold = true;
                        lblDReal.ForeColor = Color.White;
                    }                   
                }
                else
                {
                    txtFFinObra.Text = "";
                    mensaje = "Verifique que la Fecha Fin Tec. sea menor o igual a la Fecha Fin Act.";
                    mensajeVentana(mensaje);
                }
            }

            else
            {
                mensaje = "Verifique que los días totales y las fechas:  Fecha Fin Tec., Fecha LLeg. Tec y Fecha Fin Act., no estén vacías";
                mensajeVentana(mensaje);
            }
        }

        //textbox de fecha fin del tecnico 
        protected void txtFFinObra_TextChanged(object sender, EventArgs e)
        {
            string mensaje = "";
            if (!String.IsNullOrEmpty(txtFFinAct.Text) && !String.IsNullOrEmpty(txtFFinObra.Text) && !String.IsNullOrEmpty(txtFLlegObra.Text) && !String.IsNullOrEmpty(txtDTol.Text))
            {
                DateTime d1 = Convert.ToDateTime(txtFFinAct.Text);
                DateTime d2 = Convert.ToDateTime(txtFFinObra.Text);
                int result = DateTime.Compare(d2, d1);
                if (result <= 0)
                {
                    int diasF = devuelveDias(txtFLlegObra.Text, txtFFinObra.Text);
                    //if (diasF <= Convert.ToInt32(lblDPend.Text))
                    //{
                        //int dpend = Convert.ToInt32(lblDPend.Text);
                        // (!String.IsNullOrEmpty(Session["dPend"].ToString()))
                        //    dpend = Convert.ToInt32(Session["dPend"]);

                        lblDReal.Text = diasF.ToString();
                        lblDImp.Text = (int.Parse(lblDReal.Text) - int.Parse(txtDInvCom.Text)).ToString();
                        int diasPendientes = int.Parse(txtDTol.Text) - (int.Parse(lblDReal.Text) + int.Parse(txtDConsumidos.Text));
                        if (diasPendientes < 0)
                    {
                        mensaje = " La cantidad de días disponibles no es suficiente.";
                        mensajeVentana(mensaje);
                        diasPendientes = 0;
                    }
                        lblDPend.Text = diasPendientes.ToString();

                        if (int.Parse(txtDTol.Text) > diasF)
                        {

                        lblDReal.BackColor = Color.Blue;
                        lblDReal.Font.Bold = true;
                        lblDReal.ForeColor = Color.White;
                    }
                        else
                        {

                        lblDReal.BackColor = Color.Red;
                        lblDReal.Font.Bold = true;
                        lblDReal.ForeColor = Color.White;
                    }
                    //}
                    //else
                    //{
                    //    txtFFinObra.Text = "";
                    //    mensaje = "Verifique que la Fecha Fin Obra. Debe ser menor a los dias pendientes de la Obra.";
                    //    mensajeVentana(mensaje);
                    //}                    
                }
                else
                {
                    txtFFinObra.Text = "";
                    mensaje = "Verifique que la Fecha Fin Tec. sea menor o igual a la Fecha Fin Act.";
                    mensajeVentana(mensaje);
                }
            }

            else
            {
                mensaje = "Verifique que los días totales y las fechas:  Fecha Fin Tec., Fecha LLeg. Tec y Fecha Fin Act., no estén vacías";
                mensajeVentana(mensaje);
            }
        }
        protected void txtDInvCom_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtDInvCom.Text != "")
                {
                    if (int.Parse(txtDInvCom.Text) < 0)
                    {
                        mensajeVentana("Debe escribir un número natural. Gracias");
                    }
                    else
                    {
                        if (int.Parse(txtDInvCom.Text) > int.Parse(lblDReal.Text))
                        {
                            txtDInvCom.Text = "0";
                            lblDImp.Text = "0";
                            mensajeVentana("Debe escribir un número menor o igual al número de días del viaje. Gracias");
                        }
                        else { lblDImp.Text = (int.Parse(lblDReal.Text) - int.Parse(txtDInvCom.Text)).ToString(); }
                    }
                }
                else{ txtDInvCom.Text = "0"; }
            }
            catch { lblDImp.Text = "0"; txtDInvCom.Text = "0"; }
        }
        protected void txtFIniAct_TextChanged(object sender, EventArgs e)
        {
            calTodosDias(lblDViaje, txtFIniAct.Text, txtFFinAct.Text);
        }
        protected void txtFFinAct_TextChanged(object sender, EventArgs e)
        {
            calTodosDias(lblDViaje, txtFIniAct.Text, txtFFinAct.Text);
        }
        #endregion

        #region NOVEDADES
        //guarda la novedad
        protected void btnGuardarNov_Click(object sender, EventArgs e)
        {   //0|1|2|3  //idViaOf|idNov|correo|indice
            String listInsert = "";
            int conList = 0;
            foreach (ListItem li in listOFNovObs.Items)
            {
                String[] valor = li.Value.ToString().Split('|');
                String[] texto = li.Text.ToString().Split(':');
                if (conList == 1)
                {
                    listInsert = listInsert + ",(" + valor[0] + "," + valor[1] + ",'" + texto[1] + "', " + valor[2] + ", SYSDATETIME())";
                }
                else
                {
                    listInsert = listInsert + "(" + valor[0] + "," + valor[1] + ",'" + texto[1] + "', " + valor[2] + ", SYSDATETIME())";
                    conList = 1;
                }
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script41", "cerrarPopup('PopupNov')", true);
            String mensaje = CS.insertarNov(Session["SIATidViaje"].ToString(), listInsert);
            if (mensaje == "OK")
            {
                txtObsNov.Text = "";
                Boolean confi = CS.correoSiatNov(Session["SIATidViaje"].ToString());
                mensajeVentana("Se ha ingresado correctamente");
                if (confi == false) { mensajeVentana("Hubo un problema al enviar los correos"); } else { CS.actEstNovCorreo(Session["SIATidViaje"].ToString()); }
            }
        }
        //agregar novedad
        protected void btnAggNov_Click(object sender, EventArgs e)
        {//0|1|2|3  //idViaOf|idNov|correo|indice
            if (cboOrdenNov.SelectedItem.ToString() != "Seleccionar" && cboNovedad.SelectedItem.ToString() != "Seleccionar")
            {
                String idViaOf = cboOrdenNov.SelectedValue.ToString();                                                                                                                               //idViaOf|idNov|correo|indice
                agregarListaLibre(listOFNovObs, "(" + cboOrdenNov.SelectedItem.ToString() + ") - " + cboNovedad.SelectedItem.ToString().Replace(":", " ") + " :" + txtObsNov.Text.Replace(":", " "), idViaOf + "|" + cboNovedad.SelectedValue.ToString() + "|1|" + listOFNovObs.Items.Count.ToString());//novedad
            }
        }
        //eliminar novedad
        protected void btnEliNov_Click(object sender, EventArgs e)
        {   //Borro el item seleccionado
            eliminarLista(listOFNovObs);
        }

        #endregion

        #region COSTOS
        protected void btnGuardarCostos_Click(object sender, EventArgs e)
        {
            String mens = "";
            //mens = validaCamposCos();
            //if (mens == "OK")
            //{
                if (Session["SIATCostosReal"].ToString() == "NO")
                {
                    mens = CS.insertarCostosR(Session["SIATidViaje"].ToString(), lblRealHotel.Text, lblRealTiq.Text, lblRealAli.Text, lblRealTran.Text, lblRealLlam.Text, lblRealLav.Text, lblRealOtros.Text, lblRealPenal.Text, lblRealTrm.Text, Session["usuario"].ToString(), txtRealObs.Text.Replace("\r\n", " ").Replace("\n", " "));
                    if (mens == "OK")
                    {
                        mensajeVentana("Se ha ingresado correctamente!");
                        Session["SIATCostosReal"] = "SI";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script41", "cerrarPopup('PopupCos')", true);
                    }
                    else { mensajeVentana(mens); }
                }
                else if (Session["SIATCostosReal"].ToString() == "SI")
                {
                    mens = CS.editarCostosR(Session["SIATidViaje"].ToString(), lblRealHotel.Text, lblRealTiq.Text, lblRealAli.Text, lblRealTran.Text, lblRealLlam.Text, lblRealLav.Text, lblRealOtros.Text, lblRealPenal.Text, lblRealTrm.Text, Session["usuario"].ToString(), txtRealObs.Text.Replace("\r\n", " ").Replace("\n", " "));
                    if (mens == "OK")
                    {
                        mensajeVentana("Se ha actualizado correctamente!");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script41", "cerrarPopup('PopupCos')", true);
                    }
                    else { mensajeVentana(mens); }
                }

            //}
            //else { mensajeVentana(mens); }
        }
        #endregion

        protected void cboCotizacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            string msj = "";
            if (cboCotizacion.SelectedItem.ToString() != "Seleccionar")
            {

                PanelVisita.Visible = true;
                PanelOtraAct.Visible = false;
                DataTable cotizacion = new DataTable();
                DataTable obra = new DataTable();
                DataTable orden = new DataTable();
                cotizacion = CS.buscarCotizacion(Convert.ToInt32(cboCotizacion.SelectedItem.Value));
                cboServicio.Items.Clear();
                cboServicio.Items.Add(new ListItem(cotizacion.Rows[0]["servicio"].ToString(), cotizacion.Rows[0]["servicio_id"].ToString()));
                lblServicio.Visible = true;
                cboServicio.Visible = true;
                txtCliente.Text = cotizacion.Rows[0]["cliente"].ToString();
                lblIdCliente.Value = cotizacion.Rows[0]["cliente_id"].ToString();
                txtDTol.Text = cotizacion.Rows[0]["dias"].ToString();
                txtDTol_TextChanged(sender, e);
                txtCliente_TextChanged(sender, e);

                obra = CS.buscarCotizacionObra(Convert.ToInt32(cboCotizacion.SelectedItem.Value));
                if (obra.Rows.Count > 0)
                {
                    for (int i = 0; i < obra.Rows.Count; i++)
                    {
                        listObra.Items.Add(new ListItem(obra.Rows[i]["obr_nombre"].ToString(), obra.Rows[i]["siat_obra_id"].ToString()));
                    }
                }

                orden = CS.buscarCotizacionOrden(Convert.ToInt32(cboCotizacion.SelectedItem.Value));
                if (orden.Rows.Count > 0)
                {
                    for (int i = 0; i < orden.Rows.Count; i++)
                    {
                        listOF.Items.Add(new ListItem(orden.Rows[i]["siat_orden"].ToString(), orden.Rows[i]["siat_orden_id"].ToString()));
                    }
                }

                txtCliente.Enabled = false;
                cboObra.Enabled = false;
                btnAggObra.Enabled = false;
                btnEliObra.Enabled = false;
                listObra.Enabled = false;
                cboOF.Enabled = false;
                btnAggOF.Enabled = false;
                btnEliOF.Enabled = false;
                listOF.Enabled = false;
            }
            else
            {
                txtCliente.Enabled = true;
                cboObra.Enabled = true;
                btnAggObra.Enabled = true;
                btnEliObra.Enabled = true;
                listObra.Enabled = true;
                cboOF.Enabled = true;
                btnAggOF.Enabled = true;
                btnEliOF.Enabled = true;
                listOF.Enabled = true;

                lblServicio.Visible = false;
                cboServicio.Visible = false;
                msj = "Por favor seleccione nuevamente la cotización. Gracias";
                mensajeVentana(msj);
            }            
        }

        protected void chkCotizacion_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCotizacion.Checked)
            {
                PanelVisita.Visible = false;
                PanelOtraAct.Visible = false;
                limCamActiv();
                //btnBuscarVis.Visible = false;
                //btnOrdenes.Visible = false;
                cboActividad.Enabled = false;
                txtDInvCom.Visible = true;
                btnGuardarVis.Visible = true;
                btnActVis.Visible = false;
                lblCotizacion.Visible = true;
                cboCotizacion.Visible = true;
                cboActividad.Visible = false;
                lblActividad.Visible = false;
                Session["SIATTipoActA"] = "comp";
                cboActividad.SelectedIndex = 1;
                txtOF.Text = "";
                txtConsecutivo.Text = "";
                txtOF.Visible = false;
                lblOF.Visible = false;
                lblConsecutivo.Visible = false;
                txtConsecutivo.Visible = false;
                cargarCombo(CS.cargarCotizacionesAprobadas(), cboCotizacion, 0, 1);//Combo cotizaciones aprobadas
                ocultarParametrosCotizacion(true);
            }
            else
            {
                PanelVisita.Visible = false;
                PanelOtraAct.Visible = false;
                //btnBuscarVis.Visible = false;
                //btnOrdenes.Visible = false;
                cboActividad.Enabled = true;
                txtDInvCom.Visible = true;
                //lblDInv.Visible = false;
                btnGuardarVis.Visible = true;
                btnActVis.Visible = false;
                lblCotizacion.Visible = false;
                cboCotizacion.Visible = false;
                lblServicio.Visible = false;
                cboServicio.Visible = false;
                cboActividad.Visible = true;
                lblActividad.Visible = true;
                Session["SIATTipoActA"] = "";
                txtOF.Text = "";
                txtConsecutivo.Text = "";
                txtOF.Visible = false;
                lblOF.Visible = false;
                lblConsecutivo.Visible = false;
                txtConsecutivo.Visible = false;
                limCamActiv();
                cargarCombos();

                txtCliente.Enabled = true;
                cboObra.Enabled = true;
                btnAggObra.Enabled = true;
                btnEliObra.Enabled = true;
                listObra.Enabled = true;
                cboOF.Enabled = true;
                btnAggOF.Enabled = true;
                btnEliOF.Enabled = true;
                listOF.Enabled = true;
                ocultarParametrosCotizacion(false);
            }
        }

        protected void btnOrdenes_Click(object sender, EventArgs e)
        {
            Response.Redirect("SiatOrdenes.aspx");
        }

        protected void brnLimpiar_Click(object sender, EventArgs e)
        {
            Response.Redirect("SiatViajeTec.aspx");
        }

        protected void txtOF_TextChanged(object sender, EventArgs e)
        {
            ocultarPaneles(false);
            limCamActiv();
            txtConsecutivo.Text = "";
            if (!String.IsNullOrEmpty(txtOF.Text))
            {
                SqlDataReader reader = controlfup.consultarFUPOrdenFabricacionColombia(txtOF.Text);
                if (reader.Read() == false)
                {
                    lblFUP.Value = null;
                    lblVersion.Value = null;
                    lblMensajeFup.Text = "";
                    txtOF.Text = "";
                    string mensaje = "";
                    string idioma = (string)Session["Idioma"];

                    if (idioma == "Español")
                    {
                        mensaje = "El número de orden de fabricación ingresado no existe.";
                    }
                    if (idioma == "Ingles")
                    {
                        mensaje = "The production order number entered does not exist.";
                    }
                    if (idioma == "Portugues")
                    {
                        mensaje = "O número de ordem de produção entrou não existe.";
                    }
                    brnLimpiar_Click(sender, e);
                    mensajeVentana(mensaje);
                }
                else
                {
                    lblFUP.Value = reader.GetValue(0).ToString();
                    lblVersion.Value = reader.GetValue(1).ToString();
                    lblMensajeFup.Text = "PROYECTO FUP: " + lblFUP.Value + " VRS: " + lblVersion.Value;
                    poblarImplementacionObra(lblFUP.Value, lblVersion.Value, sender, e);
                    consultarViajeOf(sender, e);
                }
                reader.Close();
                reader.Dispose();
                BdDatos.desconectar();
            }
            else
            {
                brnLimpiar_Click(sender, e);
            }
        }

        private void poblarImplementacionObra(string fup, string version, object sender, EventArgs e)
        {        
            DataTable dt = new DataTable();
            dt = CS.cargarDatosImplementacionObraFup(fup,version);

            double total = 0;

            if (dt.Rows.Count > 0)
            {
                String[] actividad = cboActividad.SelectedValue.ToString().Split('|');//siat_act_id|siat_act_pantalla|siat_act_letra|siat_act_inven|siat_act_imple
                PanelVisita.Visible = true;
                PanelOtraAct.Visible = false;
                Session["SIATTipoActA"] = "";
                if (actividad[3].ToString() == "1")
                {
                    Session["SIATTipoActA"] = "inv";
                    //txtDInvCom.Visible = false;
                    //lblDInv.Visible = true;
                    txtDInvCom.Enabled = false;
                    btnGuardarVis.Visible = true;
                }
                else if (actividad[4].ToString() == "1")
                {
                    Session["SIATTipoActA"] = "imp";
                    //txtDInvCom.Visible = false;
                    //lblDInv.Visible = true;
                    txtDInvCom.Enabled = true;
                    btnGuardarVis.Visible = false;
                }
                else
                {
                    Session["SIATTipoActA"] = "comp";
                    //txtDInvCom.Visible = true;
                    //lblDInv.Visible = false;
                    txtDInvCom.Enabled = true;
                    btnGuardarVis.Visible = true;
                }
                try
                {
                    activarCampos(false);
                    lblIdCliente.Value = dt.Rows[0]["cli_id"].ToString();
                    this.txtCliente_TextChanged(sender, e);
                    txtCliente.Text = dt.Rows[0]["cli_nombre"].ToString();

                    if (!String.IsNullOrEmpty(dt.Rows[0]["obr_nombre"].ToString()))
                    {
                        cboObra.Items.Clear();
                        cboObra.Items.Add(new ListItem(dt.Rows[0]["obr_nombre"].ToString(), dt.Rows[0]["datosIdObra"].ToString()));
                        cboObra.SelectedValue = dt.Rows[0]["datosIdObra"].ToString();
                        cboObra_SelectedIndexChanged(sender, e);
                        btnAggObra_Click(sender, e);
                    }

                    if (!String.IsNullOrEmpty(dt.Rows[0]["Id_Ofa"].ToString()))
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            cboOF.SelectedValue = dt.Rows[i]["Id_Ofa"].ToString();                            
                            agregarLista(listOF, dt.Rows[i]["ordenes"].ToString(), dt.Rows[i]["Id_Ofa"].ToString());
                            //btnAggOF_Click(sender, e);

                            total += Convert.ToDouble(dt.Rows[i]["valor"].ToString());
                        }
                    }

                    txtDTol.Text = dt.Rows[0]["dtotal"].ToString();
                    //int dias = CS.consultarDiasTotales(Convert.ToInt32(lblIdMoneda.Value), total, Convert.ToInt32(lblFUP.Value), lblVersion.Value);
                    //txtDTol.Text = dias.ToString();
                    if (!String.IsNullOrEmpty(dt.Rows[0]["dconsumidos"].ToString()))
                    {
                        txtDConsumidos.Text = dt.Rows[0]["dconsumidos"].ToString();
                    }
                    else
                    {
                        txtDConsumidos.Text = "0";
                    }
                    txtDTol_TextChanged(sender, e);
                    //txtFFinObra_TextChanged(sender, e);
                }
                catch (Exception ex)
                {
                    mensajeVentana("Error intentando cargar la información, por favor comúnicarse con sistemas para resolver el problema. Gracias");
                }            
            }
            else
            {
                lblFUP.Value = "";
                lblVersion.Value = "";
                mensajeVentana("No hay datos para esta OF, por favor consulte de nuevo. Gracias");
            }
        }

        private void activarCampos(Boolean b)
        {
            txtCliente.Enabled = b;            
            cboObra.Enabled = b;
            cboOF.Enabled = b;
            btnAggObra.Enabled = b;
            btnAggOF.Enabled = b;
            btnEliObra.Enabled = b;
            btnEliOF.Enabled = b;
            listObra.Enabled = b;
            listOF.Enabled = b;
        }

        private void consultarViajeOf(Object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(lblFUP.Value) && !String.IsNullOrEmpty(lblVersion.Value))
            {
                DataTable dt1 = new DataTable();
                if (!String.IsNullOrEmpty(txtOF.Text))
                {
                    dt1 = CS.consultarViajeOf(txtOF.Text);

                    if (dt1.Rows.Count > 0)
                    {
                        btnBuscarVis_Click(sender, e);
                        btnBuscarOF_Click(sender, e);
                    }
                }
            }
        }

        protected void txtConsecutivo_TextChanged(object sender, EventArgs e)
            {
            if (!String.IsNullOrEmpty(txtConsecutivo.Text))
            {
                string msj = "";
                ocultarPaneles(false);
                limCamActiv();
                limCamGeneral();
                cargarCombos();
                cboActividad.SelectedIndex = 1;                

                txtOF.Text = "";
                DataTable cotizacion = new DataTable();
                cotizacion = CS.buscarCotizacionSiatViaje(int.Parse(txtConsecutivo.Text));
                if (cotizacion.Rows.Count > 0)
                {
                    PanelVisita.Visible = true;
                    PanelOtraAct.Visible = false;
                    DataTable obra = new DataTable();
                    DataTable orden = new DataTable();
                    cboCotizacion.Items.Clear();
                    cboCotizacion.Items.Add(new ListItem(cotizacion.Rows[0]["consecutivo"].ToString(), cotizacion.Rows[0]["siat_cotizacion_id"].ToString()));
                    cboServicio.Items.Clear();
                    cboServicio.Items.Add(new ListItem(cotizacion.Rows[0]["servicio"].ToString(), cotizacion.Rows[0]["servicio_id"].ToString()));
                    lblServicio.Visible = true;
                    cboServicio.Visible = true;
                    lblCotizacion.Visible = true;
                    cboCotizacion.Visible = true;
                    txtCliente.Text = cotizacion.Rows[0]["cliente"].ToString();
                    lblIdCliente.Value = cotizacion.Rows[0]["cliente_id"].ToString();
                    //lblDReal.Text = cotizacion.Rows[0]["dias"].ToString();
                    //txtDTol_TextChanged(sender, e);
                    ocultarParametrosCotizacion(bool.Parse(cotizacion.Rows[0]["bitCotizacion"].ToString()));
                    txtCliente_TextChanged(sender, e);
                    //String[] actividad = cboActividad.SelectedValue.ToString().Split('|');
                    cboActividad.SelectedIndex = Int16.Parse(cotizacion.Rows[0]["Actividad"].ToString());

                    txtCliente.Enabled = false;
                    cboObra.Enabled = false;
                    btnAggObra.Enabled = false;
                    btnEliObra.Enabled = false;
                    listObra.Enabled = false;
                    cboOF.Enabled = false;
                    btnAggOF.Enabled = false;
                    btnEliOF.Enabled = false;
                    listOF.Enabled = false;

                    enableCampGeneral(true);
                    enableCampActiv(true);
                    btnConfi.Visible = true;
                    cboActividad.Enabled = false;
                    btnCostos.Visible = false;
                    btnNovedad.Visible = false;
                    btnCerrar.Visible = false;
                    btnActVis.Visible = false;
                    btnGuardarVis.Visible = true;
                    txtFFinAct.Enabled = true;
                    txtFFinObra.Enabled = true;
                    txtFIniAct.Enabled = true;
                    txtFLlegObra.Enabled = true;
                    btnConfi.Visible = false;
                    btnNuevo.Visible = false;
                    cboTecnico.Enabled = true;
                    txtDInvCom.Enabled = true;
                    btnCancelar.Visible = false;
                    PanelCotizacion.Enabled = false;

                    String[] actividad = cboActividad.Text.Split('|');//siat_act_id|siat_act_pantalla|siat_act_letra|siat_act_inven|siat_act_imple
                    PanelVisita.Visible = true;
                    PanelOtraAct.Visible = false;
                    Session["SIATTipoActA"] = "";
                    if (actividad[3].ToString() == "1")
                    {
                        Session["SIATTipoActA"] = "inv";
                        btnGuardarVis.Visible = true;
                    }
                    else if (actividad[4].ToString() == "1")
                    {
                        Session["SIATTipoActA"] = "imp";
                        btnGuardarVis.Visible = false;
                    }
                    else
                    {
                        Session["SIATTipoActA"] = "comp";
                        btnGuardarVis.Visible = true;
                    }

                    DataTable viaje = new DataTable();
                    string idViaje = "";
                    if (!String.IsNullOrEmpty(cotizacion.Rows[0]["siat_via_id"].ToString()))
                    {
                        chkCotizacion.Checked = false;
                        idViaje = cotizacion.Rows[0]["siat_via_id"].ToString();
                        Session["SIATidViaje"] = idViaje;
                        viaje = CS.cargarViajeTEC(idViaje);
                        enableCampGeneral(false);
                        ocultarBotones(false);
                        //btnGuardarVis.Visible = true;
                        foreach (DataRow row in viaje.Rows)
                        {
                            if (row["estado"].ToString() == "1")
                            {
                                enableCampGeneral(true);
                                enableCampActiv(true);
                                btnConfi.Visible = true;
                                cboActividad.Enabled = false;
                                btnCostos.Visible = false;
                                btnNovedad.Visible = false;
                                btnCerrar.Visible = false;
                                btnActVis.Visible = true;
                                btnGuardarVis.Visible = false;
                                txtFFinAct.Enabled = true;
                                txtFFinObra.Enabled = true;
                                txtFIniAct.Enabled = true;
                                txtFLlegObra.Enabled = true;
                                btnConfi.Visible = true;
                                btnNuevo.Visible = false;
                                cboTecnico.Enabled = true;
                                txtDInvCom.Enabled = true;
                                btnCancelar.Visible = true;
                            }
                            else if (row["estado"].ToString() == "2" || row["estado"].ToString() == "3")
                            {
                                enableCampActiv(false);
                                btnCostos.Visible = true;
                                btnNovedad.Visible = true;
                                btnCerrar.Visible = true;
                                btnActVis.Visible = false;
                                btnGuardarVis.Visible = false;
                                txtFFinAct.Enabled = false;
                                txtFFinObra.Enabled = false;
                                txtFIniAct.Enabled = false;
                                txtFLlegObra.Enabled = false;
                                btnConfi.Visible = false;
                                btnNuevo.Visible = true;
                                cboTecnico.Enabled = false;
                                cboActividad.Enabled = false;
                                txtDInvCom.Enabled = false;
                                btnCancelar.Visible = true;
                            }
                            if (row["estado"].ToString() == "3") { btnCancelar.Visible = false; }

                            Session["SIATestadoViaje"] = row["estado"].ToString();
                            Session["SIATTipoActA"] = row["tipoAlt"].ToString();//muestra el tipo con una logica diferente, para saber si es un inventario cerrado
                            Session["SIATTipoActAReal"] = row["tipo"].ToString();//muestra el tipo real
                            lblEstado.Text = row["estado"].ToString();
                            txtHotel.Text = row["hotel"].ToString();
                            txtDireccion.Text = row["direc"].ToString();
                            txtTelefono.Text = row["tel"].ToString();
                           
                            if (bool.Parse(row["bitCotizacion"].ToString()))
                            {
                                ocultarParametrosCotizacion(true);
                            }
                            else
                            {
                                ocultarParametrosCotizacion(false);
                                //int diasPend = Convert.ToInt32(txtDTol.Text) - Convert.ToInt32(txtDConsumidos.Text);
                                //if (diasPend > 0)
                                //    lblDPend.Text = diasPend.ToString();
                            }

                            txtObserva.Text = row["observacion"].ToString();
                            lblEstado.Text = row["descripcion"].ToString();
                            txtConsecutivo.Text = row["siat_cotizacion_id"].ToString(); 
                            llenarList(CS.cargarContaVia(idViaje), listCont, 1, 0);

                            // 0|1|2|3|4
                            actividad = cboActividad.SelectedValue.ToString().Split('|');//siat_act_id|siat_act_pantalla|siat_act_letra|siat_act_inven|siat_act_imple
                            if (actividad[4].ToString() == "1")//si es un iventario cerrado, nos damos cuenta si el tipo alterado es imp
                            {
                                if (Session["SIATTipoActAReal"].ToString() == "imp")
                                {
                                    cboTecnico.SelectedIndex = cboTecnico.Items.IndexOf(cboTecnico.Items.FindByValue(row["idTec"].ToString()));
                                    cargarDatosTec(row["idTec"].ToString());
                                    txtFIniAct.Text = row["fechaIni"].ToString();
                                    txtFFinAct.Text = row["fechaFin"].ToString();
                                    Session["SIATfechaIniVia"] = row["fechaIni"].ToString();
                                    Session["SIATfechaFinVia"] = row["fechaFin"].ToString();
                                    calTodosDias(lblDViaje, txtFIniAct.Text, txtFFinAct.Text);
                                    txtFLlegObra.Text = row["fechaObraIni"].ToString();
                                    txtFFinObra.Text = row["fechaObraFin"].ToString();
                                    //txtFFinObra_TextChanged(sender, e);
                                }
                                else
                                {
                                    enableCampActiv(false);
                                    txtFIniAct.Enabled = true;
                                    txtFFinAct.Enabled = true;
                                    txtFLlegObra.Enabled = true;
                                    txtFFinObra.Enabled = true;
                                    btnActVis.Visible = false;
                                    btnCancelar.Visible = false;
                                    btnGuardarVis.Visible = true;
                                    btnConfi.Visible = false;
                                }
                            }
                            else
                            {
                                cboTecnico.SelectedIndex = cboTecnico.Items.IndexOf(cboTecnico.Items.FindByValue(row["idTec"].ToString()));
                                cargarDatosTec(row["idTec"].ToString());
                                txtFIniAct.Text = row["fechaIni"].ToString();
                                txtFFinAct.Text = row["fechaFin"].ToString();
                                Session["SIATfechaIniVia"] = row["fechaIni"].ToString();
                                Session["SIATfechaFinVia"] = row["fechaFin"].ToString();
                                calTodosDias(lblDViaje, txtFIniAct.Text, txtFFinAct.Text);
                                txtFLlegObra.Text = row["fechaObraIni"].ToString();
                                txtFFinObra.Text = row["fechaObraFin"].ToString();
                                //txtFFinObra_TextChanged(sender, e);
                            }
                            txtDInvCom.Text = row["dInv"].ToString();
                            txtDInvCom_TextChanged(sender, e);
                            color();
                        }

                        string of = CS.buscarOFPorViaje(int.Parse(idViaje));

                        if (!String.IsNullOrEmpty(of))
                        {
                            SqlDataReader reader = controlfup.consultarFUPOrdenFabricacionColombia(of);
                            if (reader.Read() == false)
                            {
                                lblFUP.Value = null;
                                lblVersion.Value = null;
                                lblMensajeFup.Text = "";
                                txtOF.Text = "";
                                string mensaje = "";
                                string idioma = (string)Session["Idioma"];

                                if (idioma == "Español")
                                {
                                    mensaje = "El número de orden de fabricación ingresado no existe.";
                                }
                                if (idioma == "Ingles")
                                {
                                    mensaje = "The production order number entered does not exist.";
                                }
                                if (idioma == "Portugues")
                                {
                                    mensaje = "O número de ordem de produção entrou não existe.";
                                }
                                mensajeVentana(mensaje);
                            }
                            else
                            {
                                lblFUP.Value = reader.GetValue(0).ToString();
                                lblVersion.Value = reader.GetValue(1).ToString();
                                DataTable dtFup = new DataTable();
                                lblMensajeFup.Text = "PROYECTO FUP: " + lblFUP.Value + " VRS: " + lblVersion.Value;
                                //dtFup = CS.cargarDatosImplementacionObraFup(lblFUP.Value, lblVersion.Value);

                                //if (dtFup.Rows.Count > 0)
                                //{
                                //    btnNuevo.Visible = true;
                                //    if (!String.IsNullOrEmpty(dtFup.Rows[0]["obr_nombre"].ToString()))
                                //    {
                                //        cboObra.Items.Clear();
                                //        cboObra.Items.Add(new ListItem(dtFup.Rows[0]["obr_nombre"].ToString(), dtFup.Rows[0]["datosIdObra"].ToString()));
                                //        cboObra.SelectedValue = dtFup.Rows[0]["datosIdObra"].ToString();
                                //        cboObra_SelectedIndexChanged(sender, e);
                                //        btnAggObra_Click(sender, e);
                                //    }

                                //    if (!String.IsNullOrEmpty(dtFup.Rows[0]["Id_Ofa"].ToString()))
                                //    {
                                //        for (int i = 0; i < dtFup.Rows.Count; i++)
                                //        {
                                //            cboOF.SelectedValue = dtFup.Rows[i]["Id_Ofa"].ToString();
                                //            agregarLista(listOF, dtFup.Rows[i]["ordenes"].ToString(), dtFup.Rows[i]["Id_Ofa"].ToString());
                                //            //btnAggOF_Click(sender, e);                                            
                                //        }
                                //    }

                                //    int dias = CS.consultarDiasTotales(Convert.ToInt32(lblIdMoneda.Value), total, Convert.ToInt32(lblFUP.Value), lblVersion.Value);
                                //    txtDTol.Text = dias.ToString();
                                //    if (!String.IsNullOrEmpty(dt.Rows[0]["dconsumidos"].ToString()))
                                //    {
                                //        txtDConsumidos.Text = dt.Rows[0]["dconsumidos"].ToString();
                                //    }
                                //    else
                                //    {
                                //        txtDConsumidos.Text = "0";
                                //    }
                                //    txtDTol_TextChanged(sender, e);
                                //}

                                poblarImplementacionObra(lblFUP.Value, lblVersion.Value, sender, e);
                                //txtFFinObra_TextChanged(sender, e);
                                fechas();
                            }
                            reader.Close();
                            reader.Dispose();
                            BdDatos.desconectar();
                        }
                        else
                        {
                            txtDConsumidos.Text = lblDReal.Text;
                            lblFUP.Value = "";
                            lblVersion.Value = "";
                            btnNuevo.Visible = false;
                        }
                    }

                    //Este es el caso en el que sea una cotizacion que no tenga viaje
                    else
                    {
                        txtDConsumidos.Text = "0";
                        chkCotizacion.Checked = true;
                        obra = CS.buscarCotizacionObra(Convert.ToInt32(txtConsecutivo.Text));
                        if (obra.Rows.Count > 0)
                        {
                            for (int i = 0; i < obra.Rows.Count; i++)
                            {
                                listObra.Items.Add(new ListItem(obra.Rows[i]["obr_nombre"].ToString(), obra.Rows[i]["siat_obra_id"].ToString()));
                            }
                        }

                        orden = CS.buscarCotizacionOrden(Convert.ToInt32(txtConsecutivo.Text));
                        if (orden.Rows.Count > 0)
                        {
                            for (int i = 0; i < orden.Rows.Count; i++)
                            {
                                listOF.Items.Add(new ListItem(orden.Rows[i]["siat_orden"].ToString(), orden.Rows[i]["siat_orden_id"].ToString()));
                            }
                        }
                    }
                }
                else
                {
                    txtCliente.Enabled = true;
                    cboObra.Enabled = true;
                    btnAggObra.Enabled = true;
                    btnEliObra.Enabled = true;
                    listObra.Enabled = true;
                    cboOF.Enabled = true;
                    btnAggOF.Enabled = true;
                    btnEliOF.Enabled = true;
                    listOF.Enabled = true;
                    lblServicio.Visible = false;
                    cboServicio.Visible = false;
                    msj = "No hay resultados para esta búsqueda. Gracias";
                    mensajeVentana(msj);
                    brnLimpiar_Click(sender, e);
                }
            }
            else
            {
                mensajeVentana("Por favor escriba nuevamente el Código de Cotización. Gracias");
                brnLimpiar_Click(sender, e);
            }
        }

        private void ocultarParametrosCotizacion(bool bitCotizacion)
        {
            if (bitCotizacion)
            {
                lblDConsumidos.Visible = false;
                txtDConsumidos.Visible = false;
                lblDPendDescripcion.Visible = false;
                lblDPend.Visible = false;
                lblDPend.Text = "0";
                txtDConsumidos.Text = "0";
            }
            else
            {
                lblDConsumidos.Visible = true;
                txtDConsumidos.Visible = true;
                lblDPendDescripcion.Visible = true;
                lblDPend.Visible = true;
            }
        }

        private void CorreoSIAT(int id, string version, int evento, string correosAdd, int pais, int servicio)
        {
            string Nombre = (string)Session["Nombre_Usuario"];
            string CorreoUsuario = (string)Session["rcEmail"];
            int parte = 0;

            string correoSistema = (string)Session["CorreoSistema"];
            string UsuarioAsunto = (string)Session["UsuarioAsunto"];

            string destinatarios = "";

            //int EncabezadoFUP = controlfup.EncabezadoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim());
            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[8];
            sqls[0] = new SqlParameter("@pID ", id);
            sqls[1] = new SqlParameter("@pVersion", version);
            sqls[2] = new SqlParameter("@pEvento", evento);
            sqls[3] = new SqlParameter("@pUsuario", UsuarioAsunto);
            sqls[4] = new SqlParameter("@pRemitente", CorreoUsuario);
            sqls[5] = new SqlParameter("@pParte", parte);
            sqls[6] = new SqlParameter("@paisCliente", pais);
            sqls[7] = new SqlParameter("@servicio", servicio);


            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_SIAT_notificaciones", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(sqls);

                    SqlParameter Asunto = new SqlParameter("@pAsun_mail", SqlDbType.VarChar, 200);
                    SqlParameter Destinatarios = new SqlParameter("@pLista", SqlDbType.VarChar, 12500);
                    SqlParameter Mensaje = new SqlParameter("@pMsg", SqlDbType.VarChar, 12500);
                    SqlParameter Anexo = new SqlParameter("@pAnexo", SqlDbType.Bit);
                    SqlParameter LinkAnexo = new SqlParameter("@pLink_anexo", SqlDbType.VarChar, 250);

                    Asunto.Direction = ParameterDirection.Output;
                    Destinatarios.Direction = ParameterDirection.Output;
                    Mensaje.Direction = ParameterDirection.Output;
                    Anexo.Direction = ParameterDirection.Output;
                    LinkAnexo.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(Asunto);
                    cmd.Parameters.Add(Destinatarios);
                    cmd.Parameters.Add(Mensaje);
                    cmd.Parameters.Add(Anexo);
                    cmd.Parameters.Add(LinkAnexo);

                    // Abrimos la conexión y ejecutamos el ExecuteReader
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //VALORES DEL ENCABEZADO 
                        string AsuntoMail = Convert.ToString(Asunto.Value);
                        string DestinatariosMail = Convert.ToString(Destinatarios.Value);
                        string MensajeMail = Convert.ToString(Mensaje.Value);
                        bool llevaAnexo = Convert.ToBoolean(Anexo.Value);
                        string EnlaceAnexo = Convert.ToString(LinkAnexo.Value);
                        string tipoAdjunto = "";

                        Byte[] correo = new Byte[0];
                        WebClient clienteWeb = new WebClient();
                        clienteWeb.Dispose();
                        clienteWeb.Credentials = new System.Net.NetworkCredential("forsa", "F0rUs3s4*", "FORSA");
                        // Adjunto
                        //DEFINIMOS LA CLASE DE MAILMESSAGE
                        MailMessage mail = new MailMessage();
                        //INDICAMOS EL EMAIL DE ORIGEN
                        mail.From = new MailAddress(correoSistema);

                        destinatarios =  DestinatariosMail;

                        if (!String.IsNullOrEmpty(correosAdd))
                        {
                            destinatarios += "," + correosAdd;
                        }

                        //AÑADIMOS LA DIRRECCIÓN DE CORREO DESTINATARIO
                        mail.To.Add(destinatarios);
                        //INCLUIMOS EL ASUNTO DEL MENSAJE
                        mail.Subject = AsuntoMail;
                        //AÑADIMOS EL CUERPO DEL MENSAJE
                        mail.Body = MensajeMail;
                        //INDICAMOS EL TIPO DE CODIFICACIÓN DEL MENSAJE
                        mail.BodyEncoding = System.Text.Encoding.UTF8;
                        //DEFINIMOS LA PRIORIDAD DEL MENSAJE
                        mail.Priority = System.Net.Mail.MailPriority.Normal;
                        //INDICAMOS SI EL CUERPO DEL MENSAJE ES HTML O NO
                        mail.IsBodyHtml = true;
                        //ADJUNTAMOS EL ARCHIVO
                        MemoryStream ms = new MemoryStream();
                        if (llevaAnexo == true)
                        {
                            string enlace = "";

                            if (evento == 34)
                            {
                                tipoAdjunto = "SIAT COTIZACIÓN No. ";
                                enlace = @"http://10.75.131.2:81/ReportServer?" + EnlaceAnexo.Trim() + "&rs:format=PDF&rs:command=render&rs:ClearSession=false&id=" + id.ToString();
                            }

                            correo = clienteWeb.DownloadData(enlace);
                            ms = new MemoryStream(correo);
                            mail.Attachments.Add(new Attachment(ms, tipoAdjunto + " " + id.ToString() + ".pdf"));
                        }
                        //DEFINIMOS NUESTRO SERVIDOR SMTP
                        //DECLARAMOS LA CLASE SMTPCLIENT
                        SmtpClient smtp = new SmtpClient();
                        //DEFINIMOS NUESTRO SERVIDOR SMTP
                        smtp.Host = "smtp.office365.com";
                        //INDICAMOS LA AUTENTICACION DE NUESTRO SERVIDOR SMTP
                        smtp.Credentials = new System.Net.NetworkCredential("monitoreo@forsa.net.co", "Those7953");
                        smtp.Port = 25;
                        smtp.EnableSsl = true;
                        //smtp.Timeout =

                        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain,
                            SslPolicyErrors sslPolicyErrors)
                        {
                            return true;
                        };
                        try
                        {
                            // smtp.SendAsync(mail, mail.To);
                            smtp.Send(mail);
                        }
                        catch (Exception ex)
                        {
                            string mensaje = "ERROR: " + ex.Message;
                        }
                        ms.Close();
                    }
                }
            }
        }

        public class CustomReportCredentials : IReportServerCredentials
        {
            private string _UserName;
            private string _PassWord;
            private string _DomainName;
            public CustomReportCredentials(string UserName, string PassWord, string DomainName)
            {
                _UserName = UserName;
                _PassWord = PassWord;
                _DomainName = DomainName;
            }

            public System.Security.Principal.WindowsIdentity ImpersonationUser
            {
                get { return null; }
            }
            public ICredentials NetworkCredentials
            {
                get { return new NetworkCredential(_UserName, _PassWord, _DomainName); }
            }
            public bool GetFormsCredentials
                (
                out Cookie authCookie,
                out string user,
                out string password,
                out string authority
                )
            { authCookie = null; user = password = authority = null; return false; }
        }
    }
}