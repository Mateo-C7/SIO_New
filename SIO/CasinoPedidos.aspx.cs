using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using CapaControl;
using System.Data;
using CapaDatos;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;
using System.Diagnostics;
using System.Threading;
using System.Net.Mail;
using System.Text;
using System.Globalization;

namespace SIO
{
    public partial class Pedidos : System.Web.UI.Page
    {
        public ControlPoliticas CP = new ControlPoliticas();
        private ControlCasino CC = new ControlCasino();
        private InfoCasino InfoCas = new InfoCasino();
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["FechaActualCasino"] = CC.fechaLocal();//Trae la fecha local y la guarda en esta session
            if (!IsPostBack)
            {
                InfoCas = CC.usuarioActual(Session["Usuario"].ToString());//Me carga los datos generales
                btnGuardar.Visible = false;
                Session["usuarioCasino"] = CC.permisosCasino(Session["Usuario"].ToString());
                if (Session["usuarioCasino"].ToString() == "True")
                {
                    if (InfoCas != null)
                    {
                        Session["cedulaEmpCasino"] = InfoCas.CedulaEmp.ToString();
                        lblNomEmp.Text = InfoCas.NomEmp.ToString();
                        lblAreaEmp.Text = InfoCas.NomArea.ToString();
                        lblCentroEmp.Text = InfoCas.CentroCosto.ToString();
                        Session["AreaActual"] = InfoCas.CodArea;//Esta variable siempre va estar llenar por el area del usuario actual
                    }

                    Session["superUsuario"] = CC.superUsuarioCasino(Session["Usuario"].ToString());//Con esta session me doy cuenta si es un super usuario, el cual puede realizar o modificar cualquier pedido
                    if (Session["superUsuario"].ToString() == "True")
                    {
                        lblCentroSupUsu.Visible = true;
                        cboCentroSupUsu.Visible = true;
                        cboCentroSupUsu.Items.Clear();
                        DataTable cargaCentrosCostos = CC.cargarComboCostos();//Carga el como cada vez que carga la pagina
                        cboCentroSupUsu.Items.Add("Seleccionar");
                        foreach (DataRow row in cargaCentrosCostos.Rows)
                        {
                            cboCentroSupUsu.Items.Add(new ListItem(row["nomCentro"].ToString(), row["idArea"].ToString()));
                        }
                    }
                    else
                    {
                        lblCentroSupUsu.Visible = false;
                        cboCentroSupUsu.Visible = false;
                    }
                    //Se inicializa las tablas en false
                    txtNumPedido.Enabled = false;
                    PedidoNormal.Visible = false;
                    PedidoEspecial.Visible = false;
                    cargarCombo(CC.cargarComboTipoSer(""), cboTipoServicio, 0, 1);
                    datosModificar();//Me verifica si es un pedido nuevo o si es un pedipo para editar
                    politicas(26, Session["usuario"].ToString());
                }
                else
                {
                    PanelPedidos.Visible = false;
                }

            }
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            TipoSer.Visible = false;
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
        //limpian los campos 
        private void limpiarPNormal()
        {
            txtCantidadNormal.Text = "";
            txtFechaAtenNormal.Text = "";
            lblTextModTipoSer1.Text = "";
            lblModTipoSer1.Text = "";
        }
        private void limpiarPEspecial()
        {
            txtDesESP.Text = "";
            txtValorUniESP.Text = "";
            txtFechaAtenPedESP.Text = "";
            txtCantidadESP.Text = "";
            lblValorTotESP.Text = "";
            lblTextModTipoSer.Text = "";
            lblModTipoSer.Text = "";
            lblHoraAten.Text = "";
            txtMenuESP.Text = "";
        }
        //Este metodo permite traer lo datos para modificarlos, depende si la persona ha escogido un pedido en la consulta del pedido
        private void datosModificar()
        {
            if (Session["numPed"] != null)//Cuando la persona escoge un pedido, el numero del pedido se guarda en una session(numPed), si esta session no trae nada significa que es un dato nuevo
            {
                btnGuardar.Visible = true;
                btnGuardar.Text = "Actualizar";
                lblCentroSupUsu.Visible = false;
                cboCentroSupUsu.Visible = false;
                int tipoSer = 0;
                int tipoSubSer = 0;
                String nomSer = "";
                DateTime hora = new DateTime();
                DateTime fecha = new DateTime();
                DateTime horaActual = CC.horaLocal();
                DateTime fechaActual = CC.fechaLocal();
                DataTable tipoSerEditar = CC.buscarTipoServiEditar(int.Parse(Session["numPed"].ToString()));//la variable de session contiene el numero del pedido seleccionado en el popup //busca la hora y la fecha para editar el pedido, este es solo un unico pedido
                foreach (DataRow row in tipoSerEditar.Rows)//recorre y me trae los datos de ese pedido, la hora, la fecha, el tipo servicio y el tipo sub servicio si tiene
                {
                    nomSer = row["nomSer"].ToString();
                    tipoSer = int.Parse(row["tipoSer"].ToString());
                    tipoSubSer = int.Parse(row["tipoSubSer"].ToString());
                    hora = DateTime.Parse(row["hora"].ToString());
                    fecha = DateTime.Parse(row["fecha"].ToString());
                }

                if (Session["superUsuario"].ToString() == "True")//verifica si es un super usuario, si lo es, le permite modificar en cualquier hora
                {
                    traerDatosModificar(tipoSer, tipoSubSer);
                }
                else
                {
                    if (horaActual > hora && fechaActual >= fecha)
                    {
                        mensajeVentana("No es posible modificar el pedido tipo " + nomSer + ", Recuerde que solo se permite modificar este pedido hasta las " + hora.ToString("hh:mm:ss tt", CultureInfo.InvariantCulture) + "");
                        lblTituloSubSer.Visible = false;
                        cboNomSerESP.Visible = false;
                        PedidoEspecial.Visible = false;
                        PedidoNormal.Visible = false;
                    }
                    else
                    {
                        traerDatosModificar(tipoSer, tipoSubSer);
                    }
                }
            }
            else//Si no hay nada en la session de numPed, es un nuevo pedido
            {
                if (!IsPostBack)
                {
                    ocultarPaneles();
                }
            }
        }
        //Me trae todos los datos dependiendo de que tipo de servicio sea, habilitando la pantalla
        private void traerDatosModificar(int tipoSer, int tipoSubSer)
        {
            cargarCombo(CC.cargarLugarP(), cboLugarESP, 0, 1);
            Session["Accion"] = "Editar";//Mandamos la session Accion en Editar para que el boton guardar lo tome como modificacion
            if (tipoSubSer == 0 || tipoSer == 6)//Depende del tipo de servicio muestra el panel correspondiente, este es para servicion normal 
            {
                Session["Operacion"] = "normal";//esta sesion me indica si es normal o especial
                TipoSer.Visible = false;
                PedidoNormal.Visible = true;
                limpiarPNormal();
                txtNumPedido.Text = "";
                DataTable pNormal = CC.buscarPedidoNormal(int.Parse(Session["numPed"].ToString()));
                foreach (DataRow row in pNormal.Rows)
                {
                    txtNumPedido.Text = Session["numPed"].ToString();
                    lblTextModTipoSer1.Text = "Tipo de Servicio:";
                    lblModTipoSer1.Text = row["tipoSer"].ToString();
                    txtCantidadNormal.Text = row["cantidad"].ToString();
                    txtFechaAtenNormal.Text = row["fechaAten"].ToString();
                    if (row["idMenu"].ToString() != "NULL")
                    {
                        cboMenuNor.Enabled = true;
                        cargarCombo(CC.cargarMenus(row["idServicio"].ToString(), "AND (Casino_menu.cas_menu_activo = 1)"), cboMenuNor, 0, 1);
                        cboMenuNor.Items.Remove(cboMenuNor.Items.FindByValue("Seleccionar"));
                        cboMenuNor.SelectedIndex = cboMenuNor.Items.IndexOf(cboMenuNor.Items.FindByValue(row["idMenu"].ToString()));
                    }
                    else { cboMenuNor.Enabled = false; cboMenuNor.Items.Clear(); }
                }
            }
            else if (tipoSubSer > 0)//Depende del tipo de servicio muestra el panel correspondiente, este es para servicio especial
            {
                Session["Operacion"] = "especial";//esta sesion me indica si es normal o especial
                TipoSer.Visible = false;
                PedidoEspecial.Visible = true;
                limpiarPEspecial();
                txtNumPedido.Text = "";
                DataTable pNormal = CC.buscarPedidoESP(int.Parse(Session["numPed"].ToString()));
                foreach (DataRow row in pNormal.Rows)
                {
                    txtNumPedido.Text = Session["numPed"].ToString();
                    lblTextModTipoSer.Text = "Tipo de Servicio:";
                    lblModTipoSer.Text = row["tipoSer"].ToString();
                    txtDesESP.Text = row["descrip"].ToString();
                    txtMenuESP.Text = row["menu"].ToString();
                    txtCantidadESP.Text = row["cantidad"].ToString();
                    lblValorTotESP.Text = row["vTotal"].ToString();
                    txtValorUniESP.Text = row["vUnitario"].ToString();
                    cboLugarESP.SelectedIndex = cboLugarESP.Items.IndexOf(cboLugarESP.Items.FindByValue(row["idLugar"].ToString()));
                    txtFechaAtenPedESP.Text = row["fechaAten"].ToString();
                    lblHoraAten.Text = row["horaAten"].ToString();
                    cboHorasAten.Items.Clear();//Limpiar el combo de seleccionar horas
                    horasCasino();//Crea el combo para seleccionar las horas
                }
            }
        }
        //hace la consulta del tipo de servicio con el id del servicio esto se obtiene por medio del combo de los tipos de servicio, consulta si tiene la hora vencida o si tiene sub servicios
        private void consultaTipoServicio(int idServicio)
        {
            DataTable servicio = null;
            servicio = CC.consultarServicio(idServicio);//consulta el servicio completo
            foreach (DataRow row in servicio.Rows)
            {
                Session["idServicio"] = row["idServicio"].ToString();
                Session["nombreSer"] = row["nombreSer"].ToString();
                Session["idSubServicio"] = row["idSubServicio"].ToString();
                Session["pantSer"] = row["pantSer"].ToString();
                Session["hcrea"] = row["hcrea"].ToString();
                Session["hmod"] = row["hmod"].ToString();
                Session["hanula"] = row["hanula"].ToString();
                Session["actMenu"] = row["actMenu"].ToString();
            }
            DateTime horaActual = CC.horaLocal();
            DataTable subServicio = null;
            subServicio = CC.consultarSubServicio(int.Parse(Session["idServicio"].ToString()));//consulta si el servicio tiene sub servicios
            if (subServicio.Rows.Count == 0)//si es 0, es un tipo de servicio normal que no tiene sub servicios
            {
                if (Session["superUsuario"].ToString() == "True")//verifica si es un super usuario, si lo es, le permite modificar en cualquier hora
                {
                    if (Session["pantSer"].ToString() == "1")
                    {
                        cboNomSerESP.Items.Clear();
                        lblTituloSubSer.Visible = false;
                        cboNomSerESP.Visible = false;
                        PedidoEspecial.Visible = false;
                        PedidoNormal.Visible = true;
                        btnGuardar.Visible = true;
                        if (Boolean.Parse(Session["actMenu"].ToString()) == true)
                        {
                            cboMenuNor.Enabled = true;
                            cargarCombo(CC.cargarMenus(Session["idServicio"].ToString(), " AND (Casino_menu.cas_menu_activo = 1)"), cboMenuNor, 0, 1);
                        }
                        else { cboMenuNor.Items.Clear(); cboMenuNor.Enabled = false; }
                    }
                    else if (Session["pantSer"].ToString() == "3")
                    {
                        lblTituloSubSer.Visible = true;
                        cboNomSerESP.Visible = true;
                        PedidoEspecial.Visible = true;
                        PedidoNormal.Visible = false;
                        btnGuardar.Visible = true;
                        cargarCombo(CC.cargarLugarP(), cboLugarESP, 0, 1);
                    }
                }
                else
                {
                    cboMenuNor.Enabled = false;
                    if (horaActual > DateTime.Parse(Session["hcrea"].ToString()))//consulta si tiene la hora vencida
                    {
                        mensajeVentana("No es posible crear el pedido tipo " + Session["nombreSer"] + ", Recuerde que solo se permite crear este pedido hasta las " + DateTime.Parse(Session["hcrea"].ToString()).ToString("hh:mm:ss tt", CultureInfo.InvariantCulture) + "");
                        lblTituloSubSer.Visible = false;
                        cboNomSerESP.Visible = false;
                        PedidoEspecial.Visible = false;
                        PedidoNormal.Visible = false;
                    }
                    else
                    {
                        if (Session["pantSer"].ToString() == "1")
                        {
                            cboNomSerESP.Items.Clear();
                            lblTituloSubSer.Visible = false;
                            cboNomSerESP.Visible = false;
                            PedidoEspecial.Visible = false;
                            PedidoNormal.Visible = true;
                            btnGuardar.Visible = true;
                            if (Boolean.Parse(Session["actMenu"].ToString()) == true)
                            {
                                cboMenuNor.Enabled = true;
                                cargarCombo(CC.cargarMenus(Session["idServicio"].ToString(), " AND (Casino_menu.cas_menu_activo = 1)"), cboMenuNor, 0, 1);
                            }
                            else { cboMenuNor.Items.Clear(); cboMenuNor.Enabled = false; }
                        }
                        else if (Session["pantSer"].ToString() == "3")
                        {
                            lblTituloSubSer.Visible = true;
                            cboNomSerESP.Visible = true;
                            PedidoEspecial.Visible = true;
                            PedidoNormal.Visible = false;
                            btnGuardar.Visible = true;
                            cargarCombo(CC.cargarLugarP(), cboLugarESP, 0, 1);
                        }
                    }
                }
            }
            else//si esta lleno es un sevicio padre que tiene sub servicios
            {
                lblTituloSubSer.Visible = true;
                cboNomSerESP.Visible = true;
                PedidoEspecial.Visible = false;
                PedidoNormal.Visible = false;
                btnGuardar.Visible = false;
                cboNomSerESP.Items.Clear();
                cboNomSerESP.Items.Add("Seleccionar");
                foreach (DataRow row in subServicio.Rows)
                {
                    cboNomSerESP.Items.Add(new ListItem(row["nombreSer"].ToString(), row["idServicio"].ToString()));
                }
                cboNomSerESP.SelectedValue = "Seleccionar";
            }
        }
        //Combo de servicio especial, me carga todos los sub serviciose especiales
        protected void cboNomSerESP_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnGuardar.Visible = true;
            if (Session["Accion"].ToString() == "Editar")
            {
                ocultarPaneles();
                btnGuardar.Text = "Guardar";
                btnGuardar.Visible = false;
            }
            else
            {
                if (cboNomSerESP.SelectedItem.ToString() != "Seleccionar")
                {
                    limpiarPEspecial();
                    limpiarPNormal();
                    consultaTipoServicio(int.Parse(cboNomSerESP.SelectedValue.ToString()));
                }
                else
                {
                    limpiarPEspecial();
                    limpiarPNormal();
                    PedidoNormal.Visible = false;
                    PedidoEspecial.Visible = false;
                    btnGuardar.Visible = false;
                }
                cboHorasAten.Items.Clear();//Limpiar el combo de seleccionar horas
                horasCasino();//Crea el combo para seleccionar las horas
            }

        }
        //Combo de todos los tipos de servicios
        protected void cboTipoServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["Accion"].ToString() == "Editar")
            {
                ocultarPaneles();
                btnGuardar.Visible = false;
            }
            else
            {
                if (cboTipoServicio.SelectedItem.ToString() != "Seleccionar")
                {
                    limpiarPEspecial();
                    limpiarPNormal();
                    consultaTipoServicio(int.Parse(cboTipoServicio.SelectedValue.ToString()));//hace la consulta del tipo de servicio con el id del servicio que esta en el combo de tipo de servicio, si tiene la hora vencida o si tiene sub servicios
                    Session["Accion"] = "Agregar";
                    btnGuardar.Text = "Guardar";
                }
                else
                {
                    limpiarPEspecial();
                    limpiarPNormal();
                    PedidoNormal.Visible = false;
                    PedidoEspecial.Visible = false;
                    Session["Accion"] = "Agregar";
                    btnGuardar.Text = "Guardar";
                    btnGuardar.Visible = false;
                }
            }
        }
        //Hace la operacion para el resultado del total
        private int valorTotal()
        {
            int valor = 0;
            if (txtCantidadESP.Text != "" && txtValorUniESP.Text != "")
            {
                valor = (int.Parse(txtCantidadESP.Text) * int.Parse(txtValorUniESP.Text));
            }
            return valor;
        }
        //Maneja el mensaje por medio de script
        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }
        //Oculta los paneles, para cada vez que cargue la pagina
        private void ocultarPaneles()
        {
            limpiarPEspecial();
            limpiarPNormal();
            txtNumPedido.Text = "";
            TipoSer.Visible = false;
            PedidoNormal.Visible = false;
            PedidoEspecial.Visible = false;
        }
        protected void txtCantidadESP_TextChanged(object sender, EventArgs e)
        {
            lblValorTotESP.Text = "";
            if (txtCantidadESP.Text != "" && txtValorUniESP.Text != "")
            {
                lblValorTotESP.Text = valorTotal().ToString();
            }
            else
            {
                lblValorTotESP.Text = "0";
            }
        }
        protected void txtValorUniESP_TextChanged(object sender, EventArgs e)
        {
            lblValorTotESP.Text = "";
            if (txtCantidadESP.Text != "" && txtValorUniESP.Text != "")
            {
                lblValorTotESP.Text = valorTotal().ToString();
            }
            else
            {
                lblValorTotESP.Text = "0";
            }
        }
        //Me valida que todos los campos no esten vacios, solo cuando va ingresar un nuevo pedido
        private String validacionTipo()
        {
            String operacion = "";
            if (cboTipoServicio.SelectedItem.ToString() != "" && cboTipoServicio.SelectedItem.ToString() != "Seleccionar")//se verifica que haya seleccionado por un tipo de servicio
            {
                if (Session["pantSer"].ToString() == "1")//si es pantalla "1" guarda con los campos de pedido normal
                {
                    if (txtCantidadNormal.Text != "0")
                    {
                        if (txtCantidadNormal.Text != "" && txtFechaAtenNormal.Text != "")// && txtLugarNormal.Text != "")
                        {
                            if (DateTime.Parse(txtFechaAtenNormal.Text) >= DateTime.Parse(Session["FechaActualCasino"].ToString()))
                            {
                                if (Boolean.Parse(Session["actMenu"].ToString()) == true)
                                {
                                    if (cboMenuNor.SelectedItem.ToString() != "Seleccionar")
                                    {
                                        operacion = "normal";
                                    }
                                    else { mensajeVentana("Por favor seleccione un menu, gracias!!"); }
                                }
                                else
                                {
                                    operacion = "normal";
                                }
                            }
                            else
                            {
                                mensajeVentana("Por favor seleccione una fecha mayor o igual a la actual, gracias!!");
                            }
                        }
                        else
                        {
                            mensajeVentana("Por favor llene todos los campos, gracias!!");
                        }
                    }
                    else
                    {
                        mensajeVentana("La cantidad no puede ser igual a 0, gracias!!");
                    }

                }
                else if (Session["pantSer"].ToString() == "3")//si es pantalla "3" guarda con los campos de pedido especial
                {
                    if (cboNomSerESP.SelectedItem.ToString() != "" && cboNomSerESP.SelectedItem.ToString() != "Seleccionar")//vefica que haya seleccionado el sub servicio
                    {
                        if (txtCantidadESP.Text != "0")
                        {
                            if (txtCantidadESP.Text != "" && txtFechaAtenPedESP.Text != "" && cboLugarESP.SelectedItem.ToString() != "Seleccionar" /*&& txtLugarESP.Text != ""*/ && txtDesESP.Text != "" && txtValorUniESP.Text != "" && lblHoraAten.Text != "" && txtMenuESP.Text != "")
                            {
                                if (DateTime.Parse(txtFechaAtenPedESP.Text) >= DateTime.Parse(Session["FechaActualCasino"].ToString()))
                                {
                                    DateTime horaActual = CC.horaLocal();
                                    DateTime horaCombo = DateTime.Parse(cboHorasAten.SelectedItem.ToString());
                                    if (horaCombo <= horaActual && DateTime.Parse(txtFechaAtenPedESP.Text) == DateTime.Parse(Session["FechaActualCasino"].ToString()))
                                    {
                                        mensajeVentana("Por favor seleccione una fecha mayor o una hora mayor a la actual, gracias!!");
                                    }
                                    else
                                    {
                                        operacion = "especial";
                                    }
                                }
                                else
                                {
                                    mensajeVentana("Por favor seleccione una fecha mayor o igual a la actual, gracias!!");
                                }
                            }
                            else
                            {
                                mensajeVentana("Por favor llene todos los campos, gracias!!");
                            }
                        }
                        else
                        {
                            mensajeVentana("La cantidad no puede ser igual a 0, gracias!!");
                        }
                    }
                    else
                    {
                        mensajeVentana("Por favor seleccione el sub servicio, gracias!!");
                    }
                }
                else if (Session["pantSer"].ToString() == "2")//si es pantalla "2" es el combo
                {
                    mensajeVentana("Por favor seleccione el sub servicio, gracias!!");
                }
            }
            else
            {
                mensajeVentana("Por favor seleccione un tipo de servicio, gracias!!");
            }
            return operacion;
        }
        //Realiza la operacion dependiendo si va a ingresar o editar un pedido
        private void operacion(String accion)
        {
            String mensaje = "";
            if (accion == "Agregar")//GUARDAR
            {
                mensaje = validacionTipo();
                if (mensaje == "normal")
                {
                    mensaje = "";
                    String idMenu = "NULL";
                    if (Boolean.Parse(Session["actMenu"].ToString()) == true)
                    {
                        idMenu = cboMenuNor.SelectedValue.ToString();
                    }
                    if (cboNomSerESP.SelectedItem == null || cboNomSerESP.SelectedItem.ToString() == "Seleccionar")
                    {
                        mensaje = CC.insertarPedido(int.Parse(cboTipoServicio.SelectedValue.ToString()), int.Parse(Session["cedulaEmpCasino"].ToString()), txtFechaAtenNormal.Text, int.Parse(txtCantidadNormal.Text), "", /* txtLugarNormal.Text,*/ int.Parse(Session["AreaActual"].ToString()), "normal", "", 0, 0, "", idMenu);
                    }
                    else
                    {
                        mensaje = CC.insertarPedido(int.Parse(cboNomSerESP.SelectedValue.ToString()), int.Parse(Session["cedulaEmpCasino"].ToString()), txtFechaAtenNormal.Text, int.Parse(txtCantidadNormal.Text), "", /*txtLugarNormal.Text,*/ int.Parse(Session["AreaActual"].ToString()), "normal", "", 0, 0, "", idMenu);
                    }
                    if (mensaje.Substring(0, 1) != "E")//E <- de ERROR
                    {
                        mensajeVentana("El pedido ha sido creado satisfactoriamente!!");
                        txtNumPedido.Text = mensaje;
                        CC.correoCasino("normal", int.Parse(mensaje), "solicitado", "CasinoPedido");
                        Session["Operacion"] = "normal";//esta sesion me indica si es normal o especial
                        btnGuardar.Text = "Actualizar";
                        Session["numPed"] = null;
                        Session["Accion"] = "Editar";
                        cboCentroSupUsu.Enabled = false;
                        cboMenuNor.Items.Remove(cboMenuNor.Items.FindByValue("Seleccionar"));
                    }
                    else
                    {
                        mensajeVentana(mensaje);
                        ocultarPaneles();
                    }
                }
                else if (mensaje == "especial")
                {
                    int valorT = valorTotal();
                    mensaje = CC.insertarPedido(int.Parse(cboNomSerESP.SelectedValue.ToString()), int.Parse(Session["cedulaEmpCasino"].ToString()), txtFechaAtenPedESP.Text, int.Parse(txtCantidadESP.Text), cboLugarESP.SelectedValue.ToString(), /*txtLugarESP.Text,*/ int.Parse(Session["AreaActual"].ToString()), "especial", txtDesESP.Text, int.Parse(txtValorUniESP.Text), valorT, cboHorasAten.SelectedItem.ToString(), txtMenuESP.Text);
                    if (mensaje.Substring(0, 1) != "E")//E <- de ERROR
                    {
                        mensajeVentana("El pedido ha sido creado satisfactoriamente!!");
                        txtNumPedido.Text = mensaje;
                        CC.correoCasino("especial", int.Parse(mensaje), "solicitado", "CasinoPedido");
                        Session["Operacion"] = "especial";//esta sesion me indica si es normal o especial
                        btnGuardar.Text = "Actualizar";
                        Session["numPed"] = null;
                        Session["Accion"] = "Editar";
                        cboCentroSupUsu.Enabled = false;
                    }
                    else
                    {
                        mensajeVentana(mensaje);
                        ocultarPaneles();
                    }
                }
                else
                {
                    mensajeVentana(mensaje);
                }
            }//EDITAR
            else if (accion == "Editar")
            {
                if (Session["Operacion"].ToString() == "normal")
                {
                    if (txtCantidadNormal.Text != "0")
                    {
                        if (txtCantidadNormal.Text != "" && txtFechaAtenNormal.Text != "")//&& txtLugarNormal.Text != "")
                        {
                            if (DateTime.Parse(txtFechaAtenNormal.Text) >= DateTime.Parse(Session["FechaActualCasino"].ToString()))
                            {
                                String menu = "";
                                if (cboMenuNor.Items.Count > 0)
                                {
                                    menu = ", Casino_Pedidos.casped_menu_id = " + cboMenuNor.SelectedValue.ToString() + " ";
                                }
                                mensaje = CC.editarPedido(int.Parse(txtNumPedido.Text), txtFechaAtenNormal.Text, int.Parse(txtCantidadNormal.Text), "", /*txtLugarNormal.Text,*/ "normal", "", 0, 0, "", menu);
                                if (mensaje == "OK")//si es ok, se realizo el insert correctamente
                                {
                                    mensajeVentana("El pedido ha sido modificado satisfactoriamente!!");
                                    CC.correoCasino("normal", int.Parse(txtNumPedido.Text), "modificado", "CasinoPedido");
                                    btnGuardar.Text = "Actualizar";
                                    Session["numPed"] = null;
                                    Session["Accion"] = "Editar";
                                }
                                else
                                {
                                    mensajeVentana(mensaje);
                                    ocultarPaneles();
                                }
                            }
                            else
                            {
                                mensajeVentana("Por favor seleccione una fecha mayor o igual a la actual, gracias!!");
                            }
                        }
                        else
                        {
                            mensajeVentana("Por favor llene todos los campos, gracias!!");
                        }
                    }
                    else
                    {
                        mensajeVentana("La cantidad no puede ser igual a 0, gracias!!");
                    }
                }
                else if (Session["Operacion"].ToString() == "especial")
                {
                    if (txtCantidadESP.Text != "0")
                    {
                        if (txtCantidadESP.Text != "" && txtFechaAtenPedESP.Text != "" && cboLugarESP.SelectedItem.ToString() != "Seleccionar" /* && txtLugarESP.Text != ""*/ && txtDesESP.Text != "" && txtValorUniESP.Text != "" && lblHoraAten.Text != "" && txtMenuESP.Text != "")
                        {
                            if (DateTime.Parse(txtFechaAtenPedESP.Text) >= DateTime.Parse(Session["FechaActualCasino"].ToString()))
                            {
                                DateTime horaActual = CC.horaLocal();
                                DateTime horaCombo = DateTime.Parse(cboHorasAten.SelectedItem.ToString());
                                if (horaCombo <= horaActual && DateTime.Parse(txtFechaAtenPedESP.Text) == DateTime.Parse(Session["FechaActualCasino"].ToString()))
                                {
                                    mensajeVentana("Por favor seleccione una fecha mayor o una hora mayor a la actual, gracias!!");
                                }
                                else
                                {
                                    int valorT = valorTotal();
                                    mensaje = CC.editarPedido(int.Parse(txtNumPedido.Text), txtFechaAtenPedESP.Text, int.Parse(txtCantidadESP.Text), cboLugarESP.SelectedValue.ToString(),/* txtLugarESP.Text,*/ "especial", txtDesESP.Text, int.Parse(txtValorUniESP.Text), valorT, cboHorasAten.SelectedItem.ToString(), txtMenuESP.Text);
                                    if (mensaje == "OK")//si es ok, se realizo el insert correctamente
                                    {
                                        mensajeVentana("El pedido ha sido modificado satisfactoriamente!!");
                                        CC.correoCasino("especial", int.Parse(txtNumPedido.Text), "modificado", "CasinoPedido");
                                        btnGuardar.Text = "Actualizar";
                                        Session["numPed"] = null;
                                        Session["Accion"] = "Editar";
                                    }
                                    else
                                    {
                                        mensajeVentana(mensaje);
                                        ocultarPaneles();
                                    }
                                }
                            }
                            else
                            {
                                mensajeVentana("Por favor seleccione una fecha mayor o igual a la actual, gracias!!");
                            }
                        }
                        else
                        {
                            mensajeVentana("Por favor llene todos los campos, gracias!!");
                        }
                    }
                    else
                    {
                        mensajeVentana("La cantidad no puede ser igual a 0, gracias!!");
                    }
                }
                else
                {
                    mensajeVentana(mensaje);
                }
            }
        }
        //Crea el horario en el combo
        private void horasCasino()
        {
            for (var h = 7; h < 20; h++)
            {
                for (var m = 00; m < 31; m++)
                {
                    if (m == 0)
                    {
                        String ms = "00";
                        cboHorasAten.Items.Add(new ListItem(h.ToString() + ":" + ms + ":00", h.ToString() + ":" + ms + ":00"));
                    }
                    else
                    {
                        cboHorasAten.Items.Add(new ListItem(h.ToString() + ":" + m.ToString() + ":00", h.ToString() + ":" + m.ToString() + ":00"));
                    }
                    m = m + 30 - 1;
                }
            }
        }
        //Pasa la hora seleccionada del combo al label
        protected void cboHorasAten_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblHoraAten.Text = "";
            lblHoraAten.Text = cboHorasAten.SelectedItem.ToString();
        }
        //Combo de los centro de costos, si el escoge uno diferente a Seleccionar, se llenara el area de la clase usuario dependiendo de que centro de costos ha escogido, si no vuelve a retomar el area del usuario actual
        protected void cboCentroSupUsu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCentroSupUsu.SelectedItem.ToString() != "Seleccionar")
            {
                Session["AreaActual"] = int.Parse(cboCentroSupUsu.SelectedValue.ToString());
            }
            else
            {
                InfoCas = CC.usuarioActual(Session["Usuario"].ToString());
                Session["AreaActual"] = InfoCas.CodArea;
            }
        }
        //botones
        //El boton Adicionar me cambia la session de Accion en Agregar para que el boton guardar lo tome como dato nuevo
        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            btnGuardar.Visible = false;
            btnGuardar.Text = "Guardar";
            lblTituloSubSer.Visible = false;
            cboNomSerESP.Visible = false;
            Session["Accion"] = "Agregar";
            Session["numPed"] = null;
            PedidoEspecial.Visible = false;
            PedidoNormal.Visible = false;
            txtNumPedido.Text = "";
            TipoSer.Visible = true;
            cboTipoServicio.SelectedValue = "Seleccionar";
            cboNomSerESP.Items.Clear();
            limpiarPEspecial();
            limpiarPNormal();
            if (Session["superUsuario"].ToString() == "True")
            { cboCentroSupUsu.Enabled = true; cboCentroSupUsu.Visible = true; cboCentroSupUsu.SelectedIndex = cboCentroSupUsu.Items.IndexOf(cboCentroSupUsu.Items.FindByValue("Seleccionar")); }
            else { cboCentroSupUsu.Enabled = false; cboCentroSupUsu.Visible = false; }
        }
        //Me abre el popup para escoger el numero del servicio para editarlo
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "verPedidos();", true);
        }
        //Boton Guardar y Editar
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Session["Accion"] != null)
            {
                if (Session["Accion"].ToString() == "Agregar")//si la session es Agregar, es un nuevo pedido
                {
                    operacion("Agregar");
                }
                else if (Session["Accion"].ToString() == "Editar")
                {
                    operacion("Editar");
                }
                else
                {
                    mensajeVentana("Por favor realice una accion(Agregar o Editar), gracias");
                }
            }
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
             {
                 btnAdicionar.Visible = true;
             }
             else
             {
                 btnAdicionar.Visible = false;
             }

             if (editar == true)
             {
                 btnBuscar.Visible = true;
             }
             else { btnBuscar.Visible = true; }
         }
    }
}