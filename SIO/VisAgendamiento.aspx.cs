using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaControl;
using System.Data;
using System.IO;
using System.Data.SqlClient;

namespace SIO
{
    public partial class AgendamientoVis : System.Web.UI.Page
    {
        public SqlDataReader reader = null;
        private ControlVisitaComercial CVC = new ControlVisitaComercial();
        private ControlEvento CE = new ControlEvento();
        public ControlPoliticas CP = new ControlPoliticas();
        public ControlUbicacion contubi = new ControlUbicacion();
        public ControlContacto controlCont = new ControlContacto();
        public ControlObra contobra = new ControlObra();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Rol"] != null)
            {
                // verifico si es un usuario de tipo cliente directo como MRV
                if (Convert.ToUInt32(Session["IdClienteUsuario"]) > 0) Response.Redirect("Home.aspx");

                String modo = Request.QueryString["Modo"];
            Session["CalendarioModo"] = modo;
            if (modo == "Ag") { lblTitulo.Text = "AGENDAMIENTO"; }
            else if (modo == "Eje") { lblTitulo.Text = "EJECUCION"; }
            else if (modo == "Cierr") { lblTitulo.Text = "CIERRE"; }
            else if (modo == "AgeCon") { lblTitulo.Text = "AGENDA CONSOLIDADA"; }
            else if (modo == "Eve") { lblTitulo.Text = "EVENTOS/FERIAS"; }
            Session["Rango"] = CVC.usuarioActual(Session["usuario"].ToString());

            if (!IsPostBack)
            {
                
                
                habilitarCampos(true);
                TextBox1_AutoCompleteExtender.ContextKey = Session["usuario"].ToString();
                trProcesos.Visible = false;
                trProcesosTec.Visible = false;
                trCliLITE.Visible = false;
                trCliFORSA.Visible = true;
                trFerias.Visible = false; //TextBox1_AutoCompleteExtender.ContextKey = Session["usuFiltroCli"].ToString();
                txtClientePros.Enabled = false;
                Session["rangoUsuPlan"] = CVC.usuarioActual(Session["usuario"].ToString());
                poblarPaisFiltro();
                if (Session["CalendarioModo"].ToString() == "AgeCon")
                {
                    if (Session["Rango"].ToString() == "VICE" || Session["Rango"].ToString() == "GERENTE")
                    {
                        trZona.Visible = true;
                        trComercial.Visible = true;
                        tblDetViaj.Visible = true;
                        cargarCombos();
                    }
                    else 
                    { 
                        trZona.Visible = false; 
                        trComercial.Visible = false; 
                        trAgenda.Visible = false; 
                        dviLinea.Visible = false; 
                        PanelCalendario.Visible = false; 
                        lblTitulo.Text = ""; 
                    }
                }
                else if (Session["CalendarioModo"].ToString() == "Eve")
                {
                    cargarCombos();
                    trZona.Visible = false;
                    trComercial.Visible = false;
                    tblDetViaj.Visible = false;
                }
                else
                {
                    cargarCombos();
                    trZona.Visible = false;
                    trComercial.Visible = false;
                    tblDetViaj.Visible = true;
                }

                if (Session["visitaCreada"] != null)
                {
                    lblVisitaCreada.Visible = true;
                    lblVisitaCreada.Text += Session["visitaCreada"].ToString();
                }

                Session["base"] = "SIO";
                cargarCombos2();
                limpiarCampos();
                cargarMesAno();

                politicas(29, Session["usuario"].ToString());
                //cargarMesAno();
                DataTable anoMes = CVC.anoMesActual();
                foreach (DataRow row in anoMes.Rows)
                {
                    cboAno.SelectedIndex = cboAno.Items.IndexOf(cboAno.Items.FindByValue(row["ano"].ToString()));
                    cboMes.SelectedIndex = cboMes.Items.IndexOf(cboMes.Items.FindByValue(row["mes"].ToString()));
                }

                if (Session["ClienteNombre"] != null)
                {
                    tblGeneral.Visible = true;
                    lblIdClienteVia.Value = Session["idCliente"].ToString();
                    txtCliente.Text = Session["ClienteNombre"].ToString();
                    txtCliente_TextChanged(sender, e);
                }

                if (Request.QueryString["idCliente"] != null)
                {
                    string cliente = Request.QueryString["idCliente"];
                    Session["Cliente"] = cliente;

                    reader = controlCont.consultarCliente(Convert.ToInt32(cliente));
                        if (reader.HasRows == true)
                        {
                            reader.Read();

                            lblIdClienteVia.Value = reader.GetInt32(5).ToString();
                            txtCliente.Text = reader.GetString(0);
                        }

                    reader.Close();
                    controlCont.cerrarConexion();

                    txtCliente_TextChanged(sender, e);

                    tblGeneral.Visible = true;
                    //lblIdClienteVia.Value = Session["idCliente"].ToString();
                    //txtCliente.Text = Session["ClienteNombre"].ToString();
                    //txtCliente_TextChanged(sender, e);
                }

                if (Request.QueryString["idObra"] != null)
                {
                    string obra = Request.QueryString["idObra"];
                    Session["Obra"] = obra;

                    //reader = contobra.ConsultarObra(Convert.ToInt32(obra));
                    //reader.Read();

                    cboProyec.SelectedValue = obra;
                    //reader.Close();
                }
            }
            String script = cargarVisitas(cboAno.SelectedItem.ToString(), cboMes.SelectedValue.ToString());//int.Parse(cboAgenda.SelectedValue.ToString())
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script", script, true);
            PanelCalendario.Visible = true;
            }
            else
            {
                string mensaje = "Sesion Expiro. Por favor inicie sesión nuevamente. Gracias!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                Response.Redirect("Inicio.aspx");
            }
        }

       

        //Carga el mes y año
        private void cargarMesAno()
        {
            cargarCombo(CVC.cargarAños(true), cboAno, 0, 0);
            String[] meses = { "1:Enero", "2:Febrero", "3:Marzo", "4:Abril", "5:Mayo", "6:Junio", "7:Julio", "8:Agosto", "9:Septiembre", "10:Octubre", "11:Noviembre", "12:Diciembre" };
            foreach (String mes in meses)
            {
                String[] mesS = mes.Split(':');
                cboMes.Items.Add(new ListItem(mesS[1].ToString(), mesS[0].ToString()));
            }
        }
        //Carga los combos
        public String parametroruta()
        {
            //MANDAR EL QUERY DE LA RUTA
            String direccion = CVC.parametrosDoc();
            return direccion;
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
        //Carga los combos de ejecucion
        private void cargarCombos()
        {
            cargarCombo(CVC.cargarZonas(), cboZonas, 0, 1);
            /*---------------------------*/
            cboComercial.Items.Clear();
            String zona = "";
            if (cboZonas.SelectedItem.ToString() == "Seleccionar")
            {
                zona = "";
            }
            else
            {
                zona = " AND (pais.pai_grupopais_id = " + cboZonas.SelectedValue.ToString() + ")";
            }
            cargarCombo(CVC.cargarRepresentates("", zona, Session["usuario"].ToString(), Session["Rango"].ToString()), cboComercial, 0, 1);
            cboComercial.Items.Remove("Seleccionar");
            
        }

        //Carga los combos de planeacion
        private void cargarCombos2()
        {
            //cargarCombo(CVC.cargarAgentes(Session["usuario"].ToString()), cboGerComerial, 0, 1);
            //cboGerComerial.Items.Remove("Todos");
            cargarCombo(CVC.cargarMotivos(), cboMotivo, 0, 1);
            cargarCombo(CVC.cargarOrigen(), cboOrigen, 0, 1);
            cargarCombo(CVC.cargarTipoFuente(), cboFuente, 0, 1);
        }
        
        //Me carga todas las visitas agendadas
        private String cargarVisitasAgen(String ano, String mes)
        {
            String color = "";
            String seMueve = "";
            String visitas = "";
            DataTable cargaVis = null;
            if (Session["CalendarioModo"].ToString() == "AgeCon")
            {
                String zona = "";
                String comercial = "";
                
                if (cboZonas.SelectedItem.ToString() != "Seleccionar")
                {
                    zona = "  AND (vct.idZonaPais = " + cboZonas.SelectedValue.ToString() + ")  ";
                }
                else { zona = ""; }
                if (cboComercial.SelectedItem.ToString() != "Todos")
                {
                    comercial = cboComercial.SelectedValue.ToString();
                }
                else { comercial = ""; }
                cargaVis = CVC.cargarTodasVisitasAgen(Session["usuario"].ToString(), ano, mes, Session["Rango"].ToString(), zona, comercial);
            }
            else
            {
                cargaVis = CVC.cargarVisitasAgen(Session["usuario"].ToString(), ano, mes);
            }

            if (cargaVis.Rows.Count == 0)
            {
                visitas = "";
            }
            else
            {
                foreach (DataRow row in cargaVis.Rows)
                {
                    if (row["color"].ToString() == "3")//3 = es cerrado  // color verde
                    { color = "#37CA5C"; seMueve = "constraint: 'availableForMeeting',"; }
                    else if (row["color"].ToString() == "2")//2 = es ejecutado  // color amarillo
                    { color = "#CAC337"; seMueve = "constraint: 'availableForMeeting',"; } //F2F60E
                    else if (row["color"].ToString() == "1")//1 = es agendada  // color azul
                    { color = "#3a87ad"; seMueve = ""; }
                    else if (row["color"].ToString() == "4")//4 = es cancelada  // color rojo
                    { color = "#E13B3B"; seMueve = "constraint: 'availableForMeeting',"; }
                    visitas = visitas + ",{"
                         + "  title : '" + row["nomVisita"].ToString() + "',"
                         + "  start: '" + String.Format("{0:yyyy'-'MM'-'dd}", DateTime.Parse(row["fechaAgendada"].ToString())) + "', "
                        //   + "  end: '" + String.Format("{0:yyyy'-'MM'-'dd}", DateTime.Parse(row["fechaAgendada"].ToString())) + "', "
                         + seMueve
                         + "  color: '" + color + "',"
                         + "  estado: '" + row["estado"].ToString() +"'"
                         + "  }";
                }
            }
            return visitas;
        }
        
        //Me carga todos los viajes del usuario
        private String cargarViajes(String ano, String mes)
        {
            String viajes = "";
            String detViajes = "";
            DataTable fechasViajes = null;
            if (Session["CalendarioModo"].ToString() == "AgeCon")
            {
                //if (cboComercial.SelectedItem.ToString() != "Todos")
                //{
                    fechasViajes = CVC.fechaViajes(cboComercial.SelectedValue.ToString(), mes, ano);
                //}
                //else { fechasViajes = null; }
            }
            else if (Session["CalendarioModo"].ToString() == "Eve")
            {
                fechasViajes = null;
            }
            else
            {
                fechasViajes = CVC.fechaViajes(Session["usuario"].ToString(), mes, ano);
                if (fechasViajes.Rows.Count == 0) { fechasViajes = null; }
            }

            if (fechasViajes == null) { viajes = ""; }
            else
            {
                int idColor = 0;
                foreach (DataRow row in fechasViajes.Rows)
                {
                    idColor = idColor + 1;
                    String color = "#" + CVC.colorViajes(idColor);
                    viajes = viajes + " ,{ "
                    + "  start: '" + String.Format("{0:yyyy'-'MM'-'dd}", DateTime.Parse(row["fechaViaIni"].ToString())) + "', "
                    + "  end: '" + String.Format("{0:yyyy'-'MM'-'dd}", DateTime.Parse(row["fechaViaFin"].ToString()).AddDays(1)) + "', "
                    + "  overlap: true, "
                    + "  rendering: 'background', "
                    + "  color: '" + color + "'  "
                    + "  }";
                    detViajes = detViajes + "<tr style=background-color:" + color + "><td style=border-radius:5px >" + row["destino"].ToString() + "</td></tr>";
                }

                String div = "$(document).ready(function () {  document.getElementById('ContentPlaceHolder4_tblDetViaj').innerHTML ='" + detViajes + "';  });";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script70", div, true);
            }
            return viajes;
        }
        
        //Me carga todas las visitas agendadas
        private String cargarEventosF(String ano, String mes)
        {
            String eventos = "";
            DataTable cargaEve = CVC.cargarEventosF("AND (MONTH(eveFer.tifuente_fechaini) = '" + mes + "') AND (YEAR(eveFer.tifuente_fechaini) = '" + ano + "')");
            if (cargaEve.Rows.Count == 0)
            {
                eventos = "";
            }
            else
            {
                foreach (DataRow row in cargaEve.Rows)
                {
                    eventos = eventos + ",{"
                         + "  title : '" + row["idNomEve"].ToString() + "',"
                         + "  start: '" + String.Format("{0:yyyy'-'MM'-'dd}", DateTime.Parse(row["fechaIniEve"].ToString())) + "', "
                         + "  end: '" + String.Format("{0:yyyy'-'MM'-'dd}", DateTime.Parse(row["fechaFinEve"].ToString()).AddDays(1)) + "', "
                         + "  constraint: 'availableForMeeting',"
                         + "  color: '#3a87ad'"
                         + "  }";
                }
            }
            return eventos;
        }
        
        //Combo zona
        protected void cboZonas_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboComercial.Items.Clear();
            String zona = "";
            if (cboZonas.SelectedItem.ToString() == "Seleccionar")
            {
                zona = "";
            }
            else
            {
                zona = "AND (pais.pai_grupopais_id = " + cboZonas.SelectedValue.ToString() + ")";
            }
            DataTable cargaAgentes = CVC.cargarRepresentates("", zona, Session["usuario"].ToString(), Session["Rango"].ToString());//Carga el como cada vez que carga la pagina
            foreach (DataRow row in cargaAgentes.Rows)
            {
                cboComercial.Items.Add(new ListItem(row["nombre"].ToString(), row["usuario"].ToString()));
            }
        }
        
        //Metodo el cual me carga todo el script del calendario
        private String cargarVisitas(String ano, String mes)//int idAgenda, 
        {
            String viajes = "";
            String visYEven = "";
            if (Session["CalendarioModo"].ToString() == "Eve")
            {
                viajes = "";
                visYEven = cargarEventosF(ano, mes);
            }
            else
            {
                viajes = cargarViajes(ano, mes);
                visYEven = cargarVisitasAgen(ano, mes);
            }
            //proceso de ejecucion no se permite mover eventos
            var moverDragandDrop = "";
            if (Session["CalendarioModo"].ToString() == "Ag" || Session["CalendarioModo"].ToString() == "Eve")
            {
                moverDragandDrop = "  editable: true,  ";
            }
            else
            {
                moverDragandDrop = "  editable: true,  ";
            }

            int rol = (int)Session["Rol"];

            int bitEverNote = CVC.getBitEvernote(rol);

            String script = " $(document).ready(function ()  {   "
             + "  $('#calendar').css({ 'width': '60%' }); "
             /*+ "  $('#external-events .fc-event').each(function()  {  $(this).draggable  ({   zIndex: 999,  revert: true,  revertDuration: 0  });  });  "*/
             + "  $('#calendar').fullCalendar({     "
             + "  height : 650,"
             + "  defaultDate: '" + ano + "/" + mes + "/01', "
             + "  theme: true,   "
             + "  header: {    left: 'prev,next today',   center: 'title',  right: '', },    "
             + "  buttonText: {    today: 'Hoy',  month: 'Mes',   week: 'Semana',  day: 'Dia'  },    "
             + "  lazyFetching: true,    "
             + "  startParam: 'start',    "
             + "  endParam: 'end',    "
             + "  timezoneParam: 'timezone',    "
             + "  allDaySlot: false,    "
             + "  editable: true,  "
             + "  droppable: true,    "
             + "  eventLimit: false,    "
             + "  eventLimitText: 'more',    "
             + "  eventLimitClick: 'popover',    "
             + "  dayPopoverFormat: 'LL',    "
             + "  monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio','Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],    "
             + "  monthNameShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun','Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],    "
             + "  dayNames: ['Domingo', 'Lunes', 'Martes', 'Miercoles','Jueves', 'Viernes', 'Sabado'],    "
             + "  dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sab'],    "
             + "  selectable: true,   "
             + "  eventClick: function(calEvent, jsEvent, view)  {     "
             + "  var tituloC = calEvent.title;     "
             + "  tituloS = tituloC.split('.');  "
             + "  idvisita = tituloS[0]; "
             + "  editVisita(idvisita, $(this), tituloC, '" + Session["usuario"].ToString() + "', calEvent.start.format(), '" + Session["CalendarioModo"].ToString() + "', calEvent, calEvent.estado,  " + bitEverNote + ");   },  " // $.trim(separanota[1])
             + "  eventDragStop : function(calEvent, jsEvent, ui, view ) {  },"
             + "  eventDrop: function(event,dayDelta,minuteDelta,allDay,revertFunc,dte)  { "
             ///Funcion para colorear la visita a color azul #3a87ad 
             + "  event.title = $.trim($(this).text());  "
             + "  GuardarNotaajax($.trim($(this).text()),event.start.format(),'" + Session["usuario"].ToString() + "', 'SICorreo'); },  "
             + "  eventMouseover: function(event, jsEvent, view) {},  "
             /* + "  drop: function(date,event, element,allDay) { alert('drop');  "
             + "  GuardarNotaajax($.trim($(this).text()),date.format(),'" + Session["usuario"].ToString() + "', 'SICorreo');  }, "*/
             + "  events:  [ { "
             + "  start: '" + ano + "/" + mes + "/01', "
             + "  end: '" + ano + "/" + mes + "/31', "
             + "  overlap: true, "
             + "  rendering: 'background'   }"
             + viajes
             + visYEven
             + " ]  });  });  ";


            return script;
        }

        private void habilitarCampos(Boolean boo)
        {
            txtCliente.Enabled = boo;
            txtClientePros.Enabled = boo;
            ImageButton1.Enabled = boo;
            cboMotivo.Enabled = boo;
            cboFerias.Enabled = boo;
            txtDias.Enabled = boo;
            cboProyec.Enabled = boo;
            txtObjetivo.Enabled = boo;
            btnAdicionar.Enabled = boo;
            chkForsa.Enabled = true;
            chkLite.Enabled = true;
            txtFAgen.Enabled = boo;
            chkAcomp.Enabled = boo;
        }

        //Limpia todos los campos
        private void limpiarCampos()
        {
            txtObjetivo.Text = "";
            txtCliente.Text = "";
            txtClientePros.Text = "";
            txtCiudad.Text = "";
            txtPais.Text = "";
            lblIdCiu.Value = null;
            lblIdPais.Value = null;
            //cboGerComerial.SelectedIndex = 0;
            cboMotivo.SelectedIndex = 0;
            cboProyec.Items.Clear();
            cboProyec.Items.Add("Seleccionar");
            cboProyec.SelectedIndex = 0;
            cboFerias.Items.Clear();
            cboFerias.Items.Add("Seleccionar");
            cboFerias.SelectedIndex = 0;
            chkForsa.Checked = true;
            chkLite.Checked = false;
            chkAcomp.Checked = false;
            txtFAgen.Text = "";
        }

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
            { btnAdicionar.Visible = true; }
            else { btnAdicionar.Visible = false; }
        }

        #region METODOS GENERALES
        //Maneja el mensaje por medio de script
        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }

        //Valida todos los campos
        private Boolean validacionCampos()
        {
            Boolean comfi = false;//Confirmacion
            if (txtObjetivo.Text.Trim() == "" || txtFAgen.Text.Trim() == "")
            {
                mensajeVentana("Por favor llene todos los campos, gracias");
            }
            else
            {
                if (txtCliente.Text.Trim() == "" && txtClientePros.Text.Trim() == "")
                {
                    mensajeVentana("Por favor seleccione el cliente correcto, gracias");
                }
                else
                {
                    if (lblIdClienteVia.Value == "" || lblIdClienteVia.Value == null)
                    {
                        mensajeVentana("Por favor seleccione el cliente correcto, gracias");
                    }
                    else
                    {
                        if (lblIdCiu.Value == "" || lblIdCiu.Value == null || txtCiudad.Text.Trim() == "")
                        {
                            mensajeVentana("Por favor seleccione la ciudad, gracias");
                        }
                        else
                        {
                            if (chkLite.Checked == false && chkForsa.Checked == false)
                            {
                                mensajeVentana("Por favor seleccione el tipo de cliente, gracias");
                            }
                            else
                            {
                                //if (cboGerComerial.SelectedItem.ToString() == "Seleccionar")
                                //{
                                //    mensajeVentana("Por favor seleccione un Agente, gracias");
                                //}
                                //else
                                //{
                                if (cboMotivo.SelectedItem.ToString() == "Seleccionar")
                                {
                                    mensajeVentana("Por favor seleccione un Motivo, gracias");
                                }
                                else
                                {
                                    if (Session["feria"].ToString() == "true")
                                    {
                                        if (cboFerias.SelectedItem.ToString() == "Seleccionar")
                                        {
                                            mensajeVentana("Por favor seleccione una feria, gracias");
                                        }
                                        else
                                        {
                                            if (txtDias.Text.Trim() == "" || txtDias.Text == null)
                                            {
                                                mensajeVentana("Por favor ingrese los dias de la feria, gracias");
                                            }
                                            else
                                            {
                                                if (int.Parse(txtDias.Text) >= 1)
                                                {
                                                    comfi = true;
                                                }
                                                else
                                                {
                                                    mensajeVentana("El dia tiene que ser mayor a cero(0), gracias");
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        comfi = true;
                                    }
                                }
                                //}
                            }
                        }
                    }
                }
            }
            return comfi;
        }

        //Crea los datos para el correo
        public string enviarCorreo(String fechaAg, String idVisita, String obra)
        {
            String cliente = "";
            String pais = "";
            //String ciudad = "";
            String dias = "";
            String uid = "";
            String correoPro = "";
            String procesos = "";

            String ciudad_nombre_cliente = "";
            String ciudad_nombre_visita = "";
            String ciudad_id_cliente = "";
            String ciudad_id_visita = "";

            string msj = "";
            DataTable datosIcs = CVC.cargarDatosICS(idVisita);
            foreach (DataRow row in datosIcs.Rows)
            {
                cliente = row["cliente"].ToString();
                pais = row["pais"].ToString();
                //ciudad = row["ciudad"].ToString();

                ciudad_nombre_cliente = row["ciudad_nombre_cliente"].ToString();
                ciudad_nombre_visita = row["ciudad_nombre_visita"].ToString();
                ciudad_id_cliente = row["ciudad_id_cliente"].ToString();
                ciudad_id_visita = row["ciudad_id_visita"].ToString();

                uid = row["fechaAct"].ToString();
                correoPro = row["correoPro"].ToString();
                procesos = row["procesos"].ToString();
                if (Session["feria"].ToString() == "true")
                { dias = "<br /><br /> DIAS : <strong>" + txtDias.Text + "</strong>"; }
            }
            //CVC.correoPlanea(cboGerComerial.SelectedItem.ToString(), cboGerComerial.SelectedValue.ToString(), cliente, pais, ciudad, Session["usuario"].ToString(), cboMotivo.SelectedItem.ToString(), txtObjetivo.Text, dias, "VisitasCPlan");
            msj = crearArchivoICS("VisitaCal", Session["usuario"].ToString(), cliente, txtObjetivo.Text, String.Format("{0:yyyyMMdd}", DateTime.Parse(fechaAg)).Replace("/", "").Replace("-", ""), ciudad_nombre_visita, pais, uid.Trim().Replace("/", "").Replace("-", "").Replace(":", "").Replace(".", ""), cboMotivo.SelectedItem.ToString(), correoPro, procesos, Session["usuario"].ToString(), chkSoporteTecnico.Checked ,idVisita, obra);
            return msj;
        }

        public void enviarCorreoEverNote(String fechaAg, String idVisita, String obra)
        {
            String cliente = "";
            String pais = "";
            String[] etiqueta = null;
            bool infra = (bool) Session["Infra"];
            bool posventa = (bool) Session["Posventa"];


            String ciudad_nombre_cliente = "";
            String ciudad_nombre_visita = "";
            String ciudad_id_cliente = "";
            String ciudad_id_visita = "";


            DataTable datosIcs = CVC.cargarDatosICS(idVisita);
            
            foreach (DataRow row in datosIcs.Rows)
            {
                cliente = row["cliente"].ToString();
                pais = row["pais"].ToString();
                etiqueta = row["etiqueta"].ToString().Split(',');

                ciudad_nombre_cliente = row["ciudad_nombre_cliente"].ToString();
                ciudad_nombre_visita = row["ciudad_nombre_visita"].ToString();
                ciudad_id_cliente = row["ciudad_id_cliente"].ToString();
                ciudad_id_visita = row["ciudad_id_visita"].ToString();
            }

            String[] datos = CVC.correoEjecutor(Session["usuario"].ToString()).Split('|');
            String cuerpo = CVC.cuerpoCorreoAgenEverNote(idVisita, datos[0], datos[1], "", obra);

            string correo = datos[2];
            string nombre = "";

            string correoEvernote = "";
            string pathEvernote = "";
            DataTable parametrosEvernote = CVC.consultarParametrosEverNote();
            if (parametrosEvernote.Rows.Count > 0)
            {
                correoEvernote = parametrosEvernote.Rows[0]["correo"].ToString();
                correo = datos[2] + "," + correoEvernote;
            }

            if (!cboMotivo.SelectedItem.Text.Contains("Feria"))
            {
                pathEvernote = CVC.getPathCarpetaEvernote(Convert.ToInt32(ciudad_id_cliente));

                if (infra == true) 
                    pathEvernote = pathEvernote + " " + "ASC";
                else
                    if (posventa == true)
                        pathEvernote = "@CRM Soporte Tecnico";
            }
            else
            {
                pathEvernote = "@CRM Feria";
            }
            nombre = pathEvernote + " " + "#" + Session["usuario"].ToString();

            for (int i = 0; i < etiqueta.Length; i++)
            {
                if(!String.IsNullOrEmpty(etiqueta[i].Trim()))
                    nombre += " #" + etiqueta[i].Trim();
            }

            CVC.crearCorreo(cuerpo, "VisitasCAge", nombre, idVisita + " " + cliente + " " + fechaAg + " " , correo, true, false, "", true);
        }

        public string crearArchivoICS(String nomArchi, String usuPlan, String titulo, String objetivo, String fecha, String ciu, String pais, String uid, String motivo, String correoAdicio, String proceItems, String usuSolicita, bool soporteTec ,String idVisita, String obra)
        {
            string msj = "";
            // Se crea el archivo nuevo  1
            string filename = "" + nomArchi + ".ics"; //nombre de tu arhivo con su extención, ej: archivo.txt
            String rutaAplicacion = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);//Toma la ruta del aplicativo
            rutaAplicacion = rutaAplicacion.Replace("\\\\", "\\");
            String dlDir = @"/ArchivosICS\";
            String path = "";
            path = rutaAplicacion + dlDir + filename;//Crea la ruta completa
            using (FileStream flujoArchivo = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (StreamWriter escritor = new StreamWriter(flujoArchivo))
                {
                    escritor.WriteLine("BEGIN:VCALENDAR");
                    escritor.WriteLine("VERSION:2.0");
                    escritor.WriteLine("PRODID:-//Microsoft Corporation//Outlook 12.0 MIMEDIR//EN");
                    escritor.WriteLine("BEGIN:VEVENT");
                    escritor.WriteLine("DTSTART:" + fecha + "T080000");
                    escritor.WriteLine("DTEND:" + fecha + "T180000");
                    escritor.WriteLine("UID:" + uid + "");
                    escritor.WriteLine("STATUS:CONFIRMED");
                    escritor.WriteLine("CATEGORIES:APPOINTMENT");
                    escritor.WriteLine("DTSTAMP:" + fecha + "T000000");
                    escritor.WriteLine("LOCATION:" + ciu + "," + pais + "");
                    escritor.WriteLine("DESCRIPTION: Motivo: " + motivo + " -- Objetivo: " + objetivo + " -- Acompañamiento: " + proceItems + " -- Solicitado por: " + usuSolicita + "");
                    escritor.WriteLine("SUMMARY;LANGUAGE=es-co: Visita: " + titulo + "");
                    escritor.WriteLine("END:VEVENT");
                    escritor.Write("END:VCALENDAR");

                    escritor.Dispose();
                    escritor.Close();
                }

                flujoArchivo.Dispose();
                flujoArchivo.Close();
            }

            msj = CVC.correoArchiICSacomp(usuPlan, "VisitasCAge", path, proceItems, soporteTec, idVisita, "Age", obra);
            return msj;
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
        //guarda los valores o items de la lista y la guarda en una lista string -- 1=items(texto) - 2=value(id)
        //private string listaString(ListBox listaAgg, int tipo)
        //{
        //    String listaString = "";
        //    int conList = 0;
        //    foreach (ListItem lis in listaAgg.Items)
        //    {
        //        if (conList == 1)
        //        {
        //            if (tipo == 1)
        //            {
        //                listaString = listaString + "," + lis.Text;
        //            }
        //            else if (tipo == 2)
        //            {
        //                listaString = listaString + "," + lis.Value;
        //            }
        //        }
        //        else
        //        {
        //            if (tipo == 1)
        //            {
        //                listaString = listaString + lis.Text;
        //            }
        //            else if (tipo == 2)
        //            {
        //                listaString = listaString + lis.Value;
        //            }
        //            conList = 1;
        //        }
        //    }
        //    return listaString;
        //}
        #endregion

        #region PLANEACION
        //Boton adiccionar
        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            String menId = "";
            if (validacionCampos())
            {
                String filtroProy = "";
                String idProy = "";
                if (cboProyec.SelectedItem.ToString() != "Seleccionar")
                {
                    filtroProy = ",vis_proyecto";
                    idProy = "," + cboProyec.SelectedValue.ToString();
                }
                String filtroFeria = "";
                String idFeria = "";
                if (cboFerias.SelectedItem.ToString() != "Seleccionar")
                {
                    filtroFeria = ",vis_feria, vis_dias";
                    idFeria = "," + cboFerias.SelectedValue.ToString() + "," + txtDias.Text;
                }

                List<string> lisProcesValue = new List<string>();
                foreach (ListItem list in listProces.Items)
                {
                    lisProcesValue.Add(list.Value);
                }
                
                string contacto = "";
                if (String.IsNullOrEmpty(cboContacto.SelectedValue.ToString().Trim()))
                    contacto = "NULL";
                else
                    contacto = cboContacto.SelectedValue.ToString();

                menId = CVC.insertMovVisMerca(Session["usuario"].ToString(), Session["usuario"].ToString(), lblIdClienteVia.Value.ToString(), cboMotivo.SelectedValue.ToString(), Session["base"].ToString(), filtroFeria, idFeria, filtroProy, idProy, txtObjetivo.Text.Replace("\r\n", " ").Replace("\n", " ").Replace(":", " "), txtFAgen.Text, lblIdCiu.Value, contacto, lisProcesValue, Convert.ToInt32(chkRemoto.Checked), Convert.ToInt32(chkSoporteTecnico.Checked));
                if (menId.Substring(0, 1) != "E")//E <- de ERROR
                {
                    string obra = cboProyec.SelectedItem.ToString();
                    trFerias.Visible = false;
                    enviarCorreo(txtFAgen.Text, menId, obra);

                    enviarCorreoEverNote(txtFAgen.Text, menId, obra);
                   
                    mensajeVentana("Se ha ingresado correctamente");
                    mostrarMensajeVisita(Convert.ToInt32(menId));
                    Session["base"] = "SIO";
                    cargarCombos();
                    limpiarCampos();
                    Session["Borrar"] = "NO";
                    trFerias.Visible = false;
                    habilitarCampos(false);
                    Session["VCconfiGuarda"] = "OK";
                    Response.Redirect("VisAgendamiento.aspx?Modo=Eje");
                }
                else
                {
                    //mensajeVentana(menId);
                    lblVisitaCreada.Text += "Error no se agendo la visita";
                    Session["visitaCreada"] = null;
                    lblVisitaCreada.Visible = true;
                }
            }
            else
            {
                //mensajeVentana("Por favor seleccione un proceso, gracias!");
                lblVisitaCreada.Text += "Error no se agendo la visita, Faltó adicionar motivo";
                Session["visitaCreada"] = null;
                lblVisitaCreada.Visible = true;
            }
        }
        //Combo motivos
        protected void cboMotivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMotivo.SelectedItem.ToString() == "Seleccionar")
            {
                Session["feria"] = "false";
                trFerias.Visible = false;
                Session["Borrar"] = "NO";
                txtDias.Text = "";
                txtCliente.Text = "";
                lblIdClienteVia.Value = "";
                cboContacto.Items.Clear();
                cboProyec.Items.Clear();
                cboProyec.Items.Add("Seleccionar");
            }
            else
            {
                if (CVC.consultaMotivo(cboMotivo.SelectedValue.ToString()) == true)
                {
                    if (Session["base"].ToString() == "LITE")
                    {
                        Session["feria"] = "false";
                        trFerias.Visible = false;
                        cboMotivo.SelectedIndex = 0;
                        mensajeVentana("Las ferias son solo para los clientes SIO!");
                        Session["Borrar"] = "NO";
                    }
                    else
                    {
                        Session["feria"] = "true";
                        trProyec.Visible = false;
                        trFerias.Visible = true;
                        chkForsa.Checked = true;
                        chkLite.Checked = false;
                        trCliFORSA.Visible = true;
                        trCliLITE.Visible = false;
                        txtDias.Text = "";
                        cargarCombo(CVC.cargarEventosF(" AND (YEAR(eveFer.tifuente_fechaini) = YEAR(SYSDATETIME())) "), cboFerias, 0, 2);
                        DataTable cliFeria = CVC.cargarCliFeria();
                        foreach (DataRow row2 in cliFeria.Rows)
                        {
                            lblIdClienteVia.Value = row2["idCliente"].ToString();
                            txtCliente.Text = row2["nomCliente"].ToString();
                        }
                        Session["Borrar"] = "OK";
                        cboProyec.Items.Clear();
                        cboProyec.Items.Add("Seleccionar");
                        cboContacto.Items.Clear();
                        txtCliente_TextChanged(sender, e);
                    }
                }
                else
                {
                    trProyec.Visible = true;
                    if (Session["Borrar"] != null)
                    {
                        if (Session["Borrar"].ToString() == "OK")
                        {
                            txtCliente.Text = "";
                            cboContacto.Items.Clear();
                            lblIdClienteVia.Value = "";
                            Session["Borrar"] = "NO";
                            cboProyec.Items.Clear();
                            cboProyec.Items.Add("Seleccionar");
                        }
                    }
                    cboFerias.Items.Clear();
                    cboFerias.Items.Add("Seleccionar");
                    cboFerias.SelectedIndex = 0;
                    trFerias.Visible = false;
                    txtDias.Text = "";
                    Session["feria"] = "false";
                }
            }
        }
        //Check para activar el texbox de los clientes de LITE
        protected void chkLite_CheckedChanged(object sender, EventArgs e)
        {
            chkForsa.Checked = false;
            trCliFORSA.Visible = false;
            trCliLITE.Visible = true;
            txtClientePros.Text = "";
            txtCliente.Text = "";
            lblIdClienteVia.Value = "";
            Session["feria"] = "false";
            Session["base"] = "SIM";
            trFerias.Visible = false;
            cboMotivo.SelectedIndex = 0;
            cboProyec.Items.Clear();
            cboProyec.Items.Add("Seleccionar");
        }
        //Boton que agrega el id y nombre del cliente lite
        protected void btnSelCliLite_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = sender as ImageButton;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            String idCliL = this.grdClientes.DataKeys[row.RowIndex].Values["idClienteL"].ToString();
            String idPaisCliL = this.grdClientes.DataKeys[row.RowIndex].Values["idPaisClieL"].ToString();
            String nomPaisCliL = row.Cells[3].Text;
            String nomCliL = row.Cells[2].Text;
            Session["idPaisViaje"] = idPaisCliL;
            lblIdClienteVia.Value = idCliL;
            lblIdPais.Value = idPaisCliL;
            txtPais.Text = nomPaisCliL;
            txtCiudad.Text = "";
            lblIdCiu.Value = "";
            txtClientePros.Text = nomCliL;
            Session["base"] = "SIM";
            cboMotivo.SelectedIndex = 0;
            cboFerias.Items.Clear();
            cboFerias.Items.Add("Seleccionar");
            trFerias.Visible = false;
            cboProyec.Items.Clear();
            cboProyec.Items.Add("Seleccionar");
            Session["Borrar"] = "NO";
            txtCliente.Text = "";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script41", "cerrarPopup('PopupClientesLite')", true);
        }
        //Boton de filtra los clientes lite
        protected void Filtrar_Click(object sender, EventArgs e)
        {
            String origen = "";
            String fuente = "";
            String usu = "";
            String cliente = "";
            String ciudad = "";
            String periodo = "";
            //if (cboOrigen.SelectedItem.ToString() != "Seleccionar")
            //{
            //    origen = "AND (lite_tipo_origen.tiorigen_id = " + int.Parse(cboOrigen.SelectedValue.ToString()) + ")";
            //}
            //if (cboFuente.SelectedItem.ToString() != "Seleccionar")
            //{
            //    fuente = "AND (lite_tipo_fuente.tifuente_id = " + int.Parse(cboFuente.SelectedValue.ToString()) + ")";
            //}

            if (Session["rangoUsuPlan"].ToString() == "VICE")
            {
                usu = "";
            }
            else
            {
                DataTable ciuRepre = CVC.cargarCiuRepre(Session["usuario"].ToString());
                if (ciuRepre.Rows.Count > 0)
                {
                    usu = "AND (cliente.cli_ciu_id IN (SELECT  ciudad_representante.cr_ciu_id FROM    usuario INNER JOIN "
                    + " representantes_comerciales  ON "
                    + " usuario.usu_siif_id = representantes_comerciales.rc_usu_siif_id INNER JOIN "
                    + " pais_representante ON "
                    + " representantes_comerciales.rc_id = pais_representante.pr_id_representante FULL OUTER JOIN "
                    + " ciudad_representante ON representantes_comerciales.rc_id = ciudad_representante.cr_rc_id "
                    + " WHERE  (usuario.usu_login = '" + Session["usuario"].ToString() + "') AND (pais_representante.pr_activo = 1) AND (representantes_comerciales.rc_activo = 1)))";

                }
                else
                {
                    usu = "AND (cliente.cli_pai_id IN (SELECT  pais_representante.pr_id_pais FROM  usuario INNER JOIN "
                        + " representantes_comerciales ON usuario.usu_siif_id = representantes_comerciales.rc_usu_siif_id INNER JOIN "
                        + " pais_representante ON representantes_comerciales.rc_id = pais_representante.pr_id_representante "
                        + " WHERE  (usuario.usu_login = '" + Session["usuario"].ToString() + "') AND (pais_representante.pr_activo = 1) AND (representantes_comerciales.rc_activo = 1))) ";
                }
            }
            //grdClientes.DataSource = CVC.cargarTablaCliLite(usu, "AND (cliente_lite.clite_empresa LIKE '%" + txtFilCliente.Text + "%') ", origen, fuente);

            if (cboOrigen.SelectedItem.ToString() != "Seleccionar")
            {
                origen = " AND (lite_tipo_origen.tiorigen_id = " + int.Parse(cboOrigen.SelectedValue.ToString()) + ")";
            }
            if (cboFuente.SelectedItem.ToString() != "Seleccionar")
            {
                fuente = " AND (lite_tipo_fuente.tifuente_id = " + int.Parse(cboFuente.SelectedValue.ToString()) + ")";
            }
            if (!String.IsNullOrEmpty(txtFilCliente.Text))
            {
                cliente = " AND (cli_nombre LIKE '%" + txtFilCliente.Text + "%') ";
            }

            if(cboPais.SelectedIndex != 0 && cboCiudad.SelectedIndex != 0)
            {
                ciudad = " AND (ciudad.ciu_id = "+cboCiudad.SelectedValue+") ";
            }

            if (!String.IsNullOrEmpty(periodo))
            {
                periodo = " AND obra.obr_periodo_sim = '"+ txtPeriodo.Text+ "'";
            }
            grdClientes.DataSource = CVC.cargarTablaCliLite(usu, cliente, origen, fuente, ciudad, periodo);
            grdClientes.DataBind();
        }
        //Vacia el campo del cliente sio
        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {
            if (txtCliente.Text != "") { txtClientePros.Text = ""; }
            if (lblIdClienteVia.Value != null && lblIdClienteVia.Value != "")
            {
                if (Session["base"].ToString() == "SIO")
                {
                    //DataTable bd = new DataTable();
                    //bd = CVC.ValidarBaseD(lblIdClienteVia.Value);

                    cboContacto.Items.Clear();
                    cboContacto.Items.Add(new ListItem("Seleccione el contacto", ""));
                    DataTable db = CVC.contatosSIObuscar(lblIdClienteVia.Value, "");
                    foreach (DataRow rowC in db.Rows)
                    {
                        cboContacto.Items.Add(new ListItem(rowC[1].ToString(), rowC[0].ToString()));
                    }

                    if (cboMotivo.SelectedItem.ToString().Contains("Feria"))
                    {
                        cboContacto.SelectedIndex = 1;
                    }

                    else
                    {
                        cargarCombo(CVC.cargarProyec(lblIdClienteVia.Value), cboProyec, 0, 1);
                        cboMotivo.SelectedIndex = 0;
                        cboFerias.Items.Clear();
                        cboFerias.Items.Add("Seleccionar");
                        trFerias.Visible = false;
                        Session["Borrar"] = "NO";
                        DataTable paisCli = CVC.cargarPaisCli(lblIdClienteVia.Value);
                        foreach (DataRow row in paisCli.Rows)
                        {
                            txtPais.Text = row["nomCliPais"].ToString();
                            lblIdPais.Value = row["idCliPais"].ToString();
                            Session["idPaisViaje"] = lblIdPais.Value;
                        }
                    }

                    //string nombreBd = "";
                    //for (int i = 0; i < bd.Rows.Count; i++)
                    //{   
                    //    nombreBd = bd.Rows[0]["bdOrigen"].ToString();
                    //    if (nombreBd == "SIO")
                    //    {
                    //        DataTable Contactolite = CVC.contatosSIObuscar(lblIdClienteVia.Value, "");
                    //        foreach (DataRow rowC in Contactolite.Rows)
                    //        {
                    //            cboContacto.Items.Add(new ListItem(rowC[1].ToString(), rowC[0].ToString()));
                    //        }
                    //    }
                    //    else if (nombreBd == "LITE")
                    //    {
                    //        DataTable Contactolite = CVC.contatosLitebuscar(lblIdClienteVia.Value, "");
                    //        foreach (DataRow rowC in Contactolite.Rows)
                    //        {
                    //            cboContacto.Items.Add(new ListItem(rowC[1].ToString(), rowC[0].ToString()));
                    //        }
                    //    }
                    //}                   
                }
            }
            else
            {
                cboProyec.Items.Clear();
                cboProyec.Items.Add("Seleccionar");
                txtPais.Text = "";
                lblIdPais.Value = null;
                Session["idPaisViaje"] = "NO";
            }
            txtCiudad.Text = "";
            lblIdCiu.Value = null;
        }
        //Vacia el campo de cliente prospecto
        protected void txtClientePros_TextChanged(object sender, EventArgs e)
        {
            if (txtClientePros.Text != "") { txtCliente.Text = ""; }
        }
        protected void cboOrigen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboOrigen.SelectedItem.ToString() != "Seleccionar")
            {
                cargarCombo(CVC.cargarFuente(cboOrigen.SelectedValue.ToString()), cboFuente, 0, 1);
            }
        }
        //protected void cboGerComerial_SelectedIndexChanged(object sender, EventArgs e)
        //{   //Paso el valor del combo que es el usuario al textbox que me filtra los usuario, con la propiedad ContextKey que despues me lo resive el metodo que esta en el web services
        //    TextBox1_AutoCompleteExtender.ContextKey = cboGerComerial.SelectedValue.ToString();
        //    if (cboGerComerial.SelectedValue.ToString() == "Seleccionar")
        //    { habilitarCampos(false); }
        //    else { habilitarCampos(true); txtClientePros.Enabled = false; }
        //}
        //Check para activar el texbox de los clientes de FORSA
        protected void chkForsa_CheckedChanged(object sender, EventArgs e)
        {
            chkLite.Checked = false;
            trCliFORSA.Visible = true;
            trCliLITE.Visible = false;
            txtClientePros.Text = "";
            txtCliente.Text = "";
            lblIdClienteVia.Value = "";
            Session["feria"] = "true";
            Session["base"] = "SIO";
        }
        //Textbox del pais
        protected void txtPais_TextChanged(object sender, EventArgs e)
        {
            if (lblIdPais.Value == null || lblIdPais.Value == "" || txtPais.Text.Trim() == "")
            {
                Session["idPaisViaje"] = "NO";
                lblIdPais.Value = null;
            }
            else
            {
                Session["idPaisViaje"] = lblIdPais.Value;
            }
            txtCiudad.Text = "";
            lblIdCiu.Value = null;
        }
        //Textbox de la ciudad del pais
        protected void txtCiudad_TextChanged(object sender, EventArgs e)
        {
            if (txtCiudad.Text.Trim() == "")
            { lblIdCiu.Value = null; }
        }
        //Combo de ferias
        protected void cboFerias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFerias.SelectedItem.ToString() == "Seleccionar")
            {
                txtPais.Text = "";
                lblIdPais.Value = "";
                Session["idPaisViaje"] = "";
            }
            else
            {
                DataTable datosFeria = null;
                datosFeria = CVC.cargarEventosF(" AND (eveFer.tifuente_id = " + cboFerias.SelectedValue.ToString() + ")");
                foreach (DataRow row in datosFeria.Rows)
                {
                    txtPais.Text = row["paisEve"].ToString();
                    lblIdPais.Value = row["idPaisEve"].ToString();
                    Session["idPaisViaje"] = lblIdPais.Value;
                }
            }
        }
        //Checke
        protected void chkAcomp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAcomp.Checked == true)
            {
                trProcesos.Visible = true;
                trProcesosTec.Visible = true;
                //cargarCombo(CVC.cargarProcesos(""), cboProcesos, 0, 1);
            }
            else
            {
                trProcesos.Visible = false;
                trProcesosTec.Visible = false;
            }
        }
        //Agrega el proceso
        protected void btnAggProces_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtPart.Text.Trim()) && !String.IsNullOrEmpty(lblIdPart.Value.Trim()))
            {
                agregarLista(listProces, txtPart.Text, lblIdPart.Value);
                txtPart.Text = "";
                lblIdPart.Value = "";
            }

            else
            {
                txtPart.Text = "";
                lblIdPart.Value = "";
                mensajeVentana("Por favor seleccione el acompañante nuevamente.");
            }
        }
        //Elimina el proceso
        protected void btnEliProces_Click(object sender, EventArgs e)
        {
            eliminarLista(listProces);
        }
        #endregion

        protected void txtPart_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtPart.Text.Trim()))
            { lblIdPart.Value = null; }
        }

        protected void imgBotonNuevoPlan_Click(object sender, ImageClickEventArgs e)
        {
            if (tblGeneral.Visible)
            {
                tblGeneral.Visible = false;
            }
            else
            {
                tblGeneral.Visible = true;
            }
        }

        protected void lnkbtnContacto_Click(object sender, EventArgs e)
        {
            string script = "";
            if (!String.IsNullOrEmpty(lblIdClienteVia.Value))
            {
                Session["ClientePlaneacion"] = lblIdClienteVia.Value;
                script = "window.open('Contacto.aspx', '_blank');";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "script", script, true);
            }
            else
            {
                mensajeVentana("Debe seleccionar un cliente. Gracias");
            }
        }

        private void mostrarMensajeVisita(int visita)
        {
            string visitaCreada = CVC.cargarUltimaVisita(visita);
            Session["visitaCreada"] = visitaCreada;
            lblVisitaCreada.Text += visitaCreada;
            lblVisitaCreada.Visible = true;
        }

        protected void cboPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPais.SelectedIndex != 0)
            {
                PoblarCiudad();
            }
        }

        private void PoblarCiudad()
        {
            SqlDataReader reader = null;           
            cboCiudad.Items.Clear();     
            cboCiudad.Items.Add(new ListItem("Seleccionar", "0"));
            cboCiudad.SelectedIndex = 0;
            
            reader = contubi.poblarListaCiudades(Convert.ToInt32(cboPais.SelectedItem.Value));
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                        cboCiudad.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }

            reader.Close();
            reader.Dispose();
            contubi.cerrarConexion();
            
            Session["CiudadFiltro"] = cboCiudad.SelectedValue;
        }

        private void poblarPaisFiltro()
        {
            SqlDataReader reader = null;
            cboPais.Items.Clear();
            cboPais.Items.Add(new ListItem("Seleccionar", "0"));
            cboPais.SelectedIndex = 0;

            reader = contubi.poblarListaPais();

            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboPais.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }

            reader.Close();
            reader.Dispose();
            contubi.cerrarConexion();

            Session["PaisFiltro"] = cboPais.SelectedValue;

        }
    }
}