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
    public partial class SiatViajeIng : System.Web.UI.Page
    {
        private ControlPoliticas CP = new ControlPoliticas();
        private ControlSIAT CS = new ControlSIAT();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["idPaisViaje"] = "NO";
                cboContacto.Attributes.Add("onchange", "asignarId();");
                limCampos();
                PanelActividad.Visible = false;
                ocultarBotones(false);
                politicas(65, Session["usuario"].ToString());
            }
        }
        #region METODOS
        //limpia todos campos 
        private void limCampos()
        {
            limCamGen();
            limCamAct();
        }
        //limpia campos generales
        private void limCamGen()
        {
            txtFIniVia.Text = "";
            txtFFinVia.Text = "";
            rbForsa.Checked = false;
            rbObra.Checked = false;
        }
        //limpia campos otra actividad
        private void limCamAct()
        {
            txtEventos.Text = "";
            txtPais.Text = "";
            txtCiudad.Text = "";
            txtCliente.Text = "";
            cboContacto.Items.Clear();
            cboObra.Items.Clear();
            txtEqui.Text = "";
            listCiudad.Items.Clear();
            listObra.Items.Clear();
            listCont.Items.Clear();
        }
        //oculta o visualizan todos los botones
        private void ocultarBotones(Boolean boo)
        {
            btnGuardar.Visible = boo;
            btnAct.Visible = boo;
            btnCerr.Visible = boo;
            btnCancelar.Visible = boo;
        }
        //valida todos los campos generales
        private String validaCamposGen()
        {
            String mensaje = "";
            if (txtFIniVia.Text == "" || txtFFinVia.Text == "")
            {
                mensaje = "Por favor ingrese las fechas correspondientes, gracias!";
            }
            else
            {
                if (DateTime.Parse(txtFFinVia.Text) < DateTime.Parse(txtFIniVia.Text))
                {
                    mensaje = "La fecha fin no puede ser menor que la fecha de inicio del viaje, gracias!";
                }
                else
                {
                    if (listCiudad.Items.Count == 0)
                    {
                        mensaje = "Debe de relacionar al menos una ciudad en el viaje, gracias!";
                    }
                    else
                    {
                        mensaje = "OK";
                    }
                }
            }
            return mensaje;
        }
        //carga el combo
        private void cargarCombo(DataTable tabla, DropDownList combo, int value, int texto)
        {
            combo.Items.Clear();
            combo.Items.Add("Seleccionar");
            foreach (DataRow row in tabla.Rows)
            {   //posicion de las colmunas  0 = value / 1 = texto  --- se escoge el numero dependiendo de la columna que tenga en el query
                //siempre va el valor como id de primero, y despues el texto lo que se va mostrar en el combo / ,0,1
                combo.Items.Add(new ListItem(row[texto].ToString(), row[value].ToString()));
            }
        }
        //envia los mensajes en los alert
        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "');", true);
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
        //inserta las obras
        private String listaObrasUsos(String idVia, ListBox lista)
        {
            String listInsert = "";
            int conList = 0;
            foreach (ListItem li in lista.Items)
            {
                String[] valor = li.Value.ToString().Split('|');
                if (conList == 1)
                {
                    listInsert = listInsert + ",(" + idVia + "," + valor[0] + "," + valor[1] + ")";
                }
                else
                {
                    listInsert = listInsert + "(" + idVia + "," + valor[0] + "," + valor[1] + ")";
                    conList = 1;
                }
            }
            return listInsert;
        }
        //Carga los grids
        private void cargarTabla(GridView grid, DataTable tabla)
        {
            grid.DataSource = tabla;
            grid.DataBind();
        }
        //Me carga los datos para editar
        private void cargarDatos(String id)
        {
            String[] ids = id.Split('|');
            String idCiuViaje = ids[0];
            String idViaje = ids[1];
            PanelActividad.Visible = true;
            limCamAct();
            DataTable viaje = null;
            DataTable ciudades = null;
            Session["SIATidViaje"] = idViaje;
            ciudades = CS.cargarCiuViajes(idViaje);
            viaje = CS.cargarViajeING(idViaje);
            ocultarBotones(false);
            btnAct.Visible = true;
            btnCancelar.Visible = true;
            btnCerr.Visible = true;
            btnNuevo.Visible = true;
            llenarList(CS.cargarCiuViajes(idViaje), listCiudad, 2, 3);
            foreach (DataRow row in viaje.Rows)
            {
                txtFIniVia.Text = row["fechaIni"].ToString();
                txtFFinVia.Text = row["fechaFin"].ToString();
                txtEventos.Text = row["evento"].ToString();
                llenarList(CS.filtrarObra("AND (siat_viaje.siat_via_id = " + idViaje + ")"), listObra, 5, 6);
                llenarList(CS.cargarContaVia(idViaje), listCont, 1, 0);
            }
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
        #endregion

        #region GENERAL
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
                btnGuardar.Visible = true;
                btnNuevo.Visible = true;
            }
            else
            {
                btnGuardar.Visible = false;
                btnNuevo.Visible = false;
            }
        }
        //radio button de forsa
        protected void rbForsa_CheckedChanged(object sender, EventArgs e)
        {
            PanelActividad.Visible = true;
            lblAggCli.Visible = false;
            //lblAggObra.Visible = false;
            //brEspa.Visible = true;
        }
        //radio button de otra obra
        protected void rbObra_CheckedChanged(object sender, EventArgs e)
        {
            PanelActividad.Visible = true;
            lblAggCli.Visible = true;
            //lblAggObra.Visible = true;
            //brEspa.Visible = false;
        }
        //boton de buscar viajes
        protected void btnBuscarViajes_Click(object sender, EventArgs e)
        {
            txtFiltroObra.Text = "";
            cargarTabla(grdViajes, null);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script41", "abrirPopup('PopupBuscaVis')", true);
        }
        //boton de guardar viaje
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            String mensaje = validaCamposGen();
            if (mensaje == "OK")
            {
                Boolean existe = true;
                existe = CS.existeViajeIng(Session["usuario"].ToString(), txtFIniVia.Text, txtFFinVia.Text, "");
                if (existe == false)
                {
                    String listConts = listaStringSplit(listCont, 1);
                    String listObras = listaStringSplit(listObra, 0);
                    String listCiu = listaStringSplit(listCiudad, 1);
                    String menId = CS.insertarViajeING(Session["usuario"].ToString(), txtFIniVia.Text, txtFFinVia.Text, txtEventos.Text.Replace("\r\n", " ").Replace("\n", " "));
                    if (menId.Substring(0, 1) != "E")//E <- de ERROR
                    {
                        Session["SIATidViaje"] = menId;//Id la visita
                        btnGuardar.Visible = false;
                        btnAct.Visible = true;
                        btnCerr.Visible = true;
                        btnCancelar.Visible = true;
                        CS.insertarCiu(menId, listCiu);//inserto las ciudades 
                        CS.insertarCont(menId, listConts);
                        CS.insertarObras(menId, listaObrasUsos(menId, listObra));
                        mensajeVentana("Se ha ingresado correctamente!!");
                    }
                    else
                    {
                        mensajeVentana(menId);
                    }
                }
                else { mensajeVentana("Ya tiene un viaje asignado a ese rango de fechas, por favor ingrese otro rango, gracias!"); }
            }
            else
            {
                mensajeVentana(mensaje);
            }
        }
        //boton de actualizar viaje
        protected void btnAct_Click(object sender, EventArgs e)
        {
            String mensaje = validaCamposGen();
            if (mensaje == "OK")
            {
                Boolean existe = true;
                existe = CS.existeViajeIng(Session["usuario"].ToString(), txtFIniVia.Text, txtFFinVia.Text, " AND (siat_via_id <> " + Session["SIATidViaje"].ToString() + ") ");
                if (existe == false)
                {
                    String listConts = listaStringSplit(listCont, 1);
                    String listObras = listaStringSplit(listObra, 0);
                    String listCiu = listaStringSplit(listCiudad, 1);
                    String menConf = CS.editarViajeING(Session["usuario"].ToString(), txtFIniVia.Text, txtFFinVia.Text, Session["SIATidViaje"].ToString(), txtEventos.Text.Replace("\r\n", " ").Replace("\n", " "));
                    if (menConf == "OK")
                    {
                        CS.insertarCiu(Session["SIATidViaje"].ToString(), listCiu);//inserto las ciudades
                        CS.insertarCont(Session["SIATidViaje"].ToString(), listConts);
                        CS.insertarObras(Session["SIATidViaje"].ToString(), listaObrasUsos(Session["SIATidViaje"].ToString(), listObra));
                        mensajeVentana("Se actualizado correctamente!!");
                    }
                    else
                    {
                        mensajeVentana(menConf);
                    }
                }
                else { mensajeVentana("Ya tiene un viaje asignado a ese rango de fechas, por favor ingrese otro rango, gracias!"); }
            }
            else
            {
                mensajeVentana(mensaje);
            }
        }
        //boton de cerrar viaje
        protected void btnCerr_Click(object sender, EventArgs e)
        {
            String mensaje = CS.editarEstadoV(3, Session["SIATidViaje"].ToString(), ", siat_via_fecha_cerr = SYSDATETIME(), siat_via_usu_cerr = '" + Session["usuario"].ToString() + "' ");
            if (mensaje == "OK")
            {
                limCampos();
                PanelActividad.Visible = false;
                ocultarBotones(false);
                btnGuardar.Visible = true;
                mensajeVentana("Se ha cerrado la visita correctamente!!");
            }
            else { mensajeVentana(mensaje); }
        }
        //boton de agregar obra
        protected void btnAggObra_Click(object sender, EventArgs e)
        {
            if (cboObra.SelectedItem.ToString() != "Seleccionar")
            {
                String[] idObra = cboObra.SelectedValue.ToString().Split('|');//(txtTelefono.Text == "") ? "null" : txtTelefono.Text
                agregarLista(listObra, "(Usos: " + ((txtEqui.Text == "") ? "0" : txtEqui.Text) + ") - " + idObra[1], idObra[0] + "|" + ((txtEqui.Text == "") ? "0" : txtEqui.Text));//obra
            }
        }
        //boton de elminar obra
        protected void btnEliObra_Click(object sender, EventArgs e)
        {  //Borro el item seleccionado
            eliminarLista(listObra);
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
            else { mensajeVentana("Por favor seleccione un contacto, gracia!"); }
        }
        //elimina contacto
        protected void btnEliCont_Click(object sender, EventArgs e)
        {
            eliminarLista(listCont);
        }
        //textbox de cliente
        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {
            String listCiu = listaStringSplit(listCiudad, 1);
            String filtroCiu = "";
            if (listCiu != "")
            {
                filtroCiu = " AND (obra.obr_ciu_id IN (" + listCiu + ")) ";
            }
            if (lblIdCliente.Value.Trim() == "" || txtCliente.Text.Trim() == "" || lblIdCliente.Value == null)
            {
                cboObra.Items.Clear();
                cboObra.Items.Add("Seleccionar");
                lblIdCliente.Value = "";
                txtCliente.Text = "";
            }
            else
            {
                cargarCombo(CS.cargarContactos(" AND (contacto_cliente.ccl_cli_id = " + lblIdCliente.Value + ") ", ""), cboContacto, 0, 1);//Combo contacto
                cargarCombo(CS.cargarObras(" (cliente.cli_id = " + lblIdCliente.Value + ") " + filtroCiu), cboObra, 2, 1);//Combo obra
            }
        }
        //combo de contactos
        protected void cboContacto_SelectedIndexChanged(object sender, EventArgs e)
        {
            String idCombo = cboContacto.SelectedValue.ToString();
            cargarCombo(CS.cargarContactos("AND (contacto_cliente.ccl_cli_id = " + lblIdCliente.Value + ")", ""), cboContacto, 0, 1);
            cboContacto.SelectedIndex = cboContacto.Items.IndexOf(cboContacto.Items.FindByValue(idCombo));
        }
        //busca el viaje por pais
        protected void btnBuscarPais_Click(object sender, EventArgs e)
        {            
            DataTable pais = null;
            string obraPais = "";
            if (!String.IsNullOrEmpty(txtFiltroObra.Text))
            {
                obraPais = "AND (pais.pai_nombre LIKE '%" + txtFiltroObra.Text + "%')";
            }
            pais = CS.filtrarPais("(siat_viaje.siat_estado_viaje_id = 1) " + obraPais + "", txtFIniAct.Text, txtFFinAct.Text);
            cargarTabla(grdViajes, pais);
        }
        //Identifica que fila ha sido seleccionada
        protected void btnSelPais_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = sender as ImageButton;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            String idCiuVia = this.grdViajes.DataKeys[row.RowIndex].Values["idCiuViaje"].ToString();
            String idViaje = this.grdViajes.DataKeys[row.RowIndex].Values["idViaje"].ToString();
            cargarDatos(idCiuVia + "|" + idViaje);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script41", "cerrarPopup('PopupBuscaVis')", true);
        }
        //cancela la visita
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            String mensaje = CS.editarEstadoV(4, Session["SIATidViaje"].ToString(), ", siat_via_fecha_can = SYSDATETIME(), siat_via_usu_can = '" + Session["usuario"].ToString() + "' ");
            if (mensaje == "OK")
            {
                PanelActividad.Visible = false;
                limCampos();
                ocultarBotones(false);
                btnGuardar.Visible = true;
                txtFIniVia.Enabled = true;
                txtFFinVia.Enabled = true;
                mensajeVentana("Se ha cancelado la visita correctamente!!");
            }
            else { mensajeVentana(mensaje); }
        }
        //limpia para crear un nuevo viaje
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            limCampos();
            PanelActividad.Visible = false;
            ocultarBotones(false);
            txtFIniVia.Enabled = true;
            txtFFinVia.Enabled = true;
            btnGuardar.Visible = true;
        }
        //texbox de pais
        protected void txtPais_TextChanged(object sender, EventArgs e)
        {
            if (lblIdPais.Value == null || lblIdPais.Value.Trim() == "" || txtPais.Text.Trim() == "")
            {
                Session["idPaisViaje"] = "NO";
                lblIdPais.Value = "";
                txtPais.Text = "";
            }
            else
            {
                Session["idPaisViaje"] = lblIdPais.Value;
            }
            txtCiudad.Text = "";
            lblIdCiu.Value = "";
        }
        //agrega ciudad
        protected void btnAgreCiu_Click(object sender, EventArgs e)
        {
            if (lblIdCiu.Value != "")
            {
                if (listCiudad.Items.Count >= 1)//verifico si hay algo en la lista que ingresa en la variable list
                {
                    String noagre = "NO";//no agregar
                    foreach (ListItem li in listCiudad.Items)//lleno y recorro un list por medio del la lista que esta entrando
                    {
                        String[] idLista = li.Value.Split('|');  //0 =  id pais / 1 = id ciudad
                        if (idLista[0] == lblIdPais.Value)//verifico si el id ya esta en la lista
                        {
                            noagre = "SI";
                        }
                    }
                    if (noagre == "SI")//agrego a la lista el item y el value si no existe en lista
                    {
                        listCiudad.Items.Add(new ListItem(txtPais.Text + " - " + txtCiudad.Text, lblIdPais.Value + "|" + lblIdCiu.Value));
                        btnGuardar.Visible = true;
                    }
                    else { mensajeVentana("Recuerde que solo puede adicionar ciudades de un mismo pais!"); }
                }
                else//si no hay agrego inmediatamente
                {
                    listCiudad.Items.Add(new ListItem(txtPais.Text + " - " + txtCiudad.Text, lblIdPais.Value + "|" + lblIdCiu.Value));
                    btnGuardar.Visible = true;
                }
                lblIdCiu.Value = "";
                txtCiudad.Text = "";
            }
            else { mensajeVentana("Por favor seleccione una ciudad, gracia!"); }
        }
        //elimina ciudad
        protected void btnEliCiu_Click(object sender, EventArgs e)
        { //Borro el item seleccionado
            eliminarLista(listCiudad);
            if (listCiudad.Items.Count > 0)
            {
                btnGuardar.Visible = true;
            }
            else
            {
                btnGuardar.Visible = false;
            }
        }
        #endregion

    }
}