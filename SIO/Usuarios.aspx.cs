using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaControl;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.SqlClient;

namespace SIO
{
    public partial class Usuarios : System.Web.UI.Page
    {
        public ControlPoliticas CP = new ControlPoliticas();
        public SqlDataReader reader = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            txtPassUsu.Attributes.Add("value", txtPassUsu.Text);
            if (!IsPostBack)
            {
                limpiar();
                cargarTabla();
                cargarCombos();
                txtLoginUsu.Attributes.Add("onkeypress", "javascript:if(event.keyCode==32){return false;}");
                txtPassUsu.Attributes.Add("onkeypress", "javascript:if(event.keyCode==32){return false;}");
                txtCedula.Attributes.Add("onkeypress", "javascript:if(event.keyCode==32){return false;}");
            }
            politicas(42, Session["usuario"].ToString());
        }
        protected void txtLoginUsu_TextChanged(object sender, EventArgs e)
        {
            int plantaDefecto = 0;
            if (lblIdUsu.Value != "")
            {
                DataTable usuario = CP.buscarUsuId(int.Parse(lblIdUsu.Value));//Datos del usuario
                DataTable usuPlanta = CP.buscarUsuPlanta(int.Parse(lblIdUsu.Value));//Plantas del usuario
                DataTable usuComer = CP.buscarUsuComer(int.Parse(lblIdUsu.Value));//Datos del comercial
                
                reader = CP.consultarPlantaDefecto(int.Parse(lblIdUsu.Value));
                if (reader.HasRows == true)
                {
                    reader.Read();
                    cboPais.Items.Clear();
                    plantaDefecto = Convert.ToInt32(reader.GetValue(0).ToString());
                }
                reader.Close();

                if (usuario.Rows.Count > 0)
                {
                    limpiar();
                    txtLoginUsu.Enabled = false;
                   
                    //Permisos/Politicas
                    if (Session["btnEditarUsu"].ToString() == "true")
                    {   //Usuario
                        Session["Accion"] = "Editar";
                        btnGuardarUsu.Visible = true;
                        btnGuardarUsu.Text = "Actualizar";
                    }
                    else { btnGuardarUsu.Visible = false; }
                    //Datos del usuario
                    foreach (DataRow row in usuario.Rows)
                    {
                        txtLoginUsu.Text = row["login"].ToString();
                       // txtPassUsu.Text = row["pass"].ToString();
                        txtPassUsu.Attributes.Add("value", row["pass"].ToString());
                        if (row["cedula"].ToString() == "0")
                        {
                            chkEmpleadoF.Checked = false;
                            txtCedula.Text = "";
                            txtCedula.Enabled = false;
                            accorCasino.Enabled = false;
                            accorSiif.Enabled = false;
                        }
                        else
                        {
                            buscaEmpleado(row["cedula"].ToString());
                            chkEmpleadoF.Checked = true;
                            txtCedula.Text = row["cedula"].ToString();
                            txtCedula.Enabled = true;
                            accorCasino.Enabled = true;
                            accorSiif.Enabled = true;
                            DataTable usuCasinoSiif = CP.buscarUsuCas(row["login"].ToString());//Datos casino
                            foreach (DataRow row2 in usuCasinoSiif.Rows)
                            {
                                chkAdmiCas.Checked = Boolean.Parse(row2["admiCas"].ToString());
                                chkArchivoP.Checked = Boolean.Parse(row2["arcPlanoCas"].ToString());
                                chkPedArea.Checked = Boolean.Parse(row2["pedidoCas"].ToString());
                                chkFormaleta.Checked = Boolean.Parse(row2["formaleta"].ToString());
                                chkAccesorios.Checked = Boolean.Parse(row2["accesorios"].ToString());
                                cboProPer.SelectedIndex = cboProPer.Items.IndexOf(cboProPer.Items.FindByValue(row2["idProceso"].ToString()));
                            }
                        }
                        chkActivo.Checked = Boolean.Parse(row["activo"].ToString());
                        cboRoles.SelectedIndex = cboRoles.Items.IndexOf(cboRoles.Items.FindByValue(row["rolId"].ToString()));
                    }
                    //llena la lista de la planta
                    llenarListas(listaPlantaAgg, usuPlanta);
                    listaPlantaAgg.SelectedValue =  plantaDefecto.ToString();
                    if (usuComer.Rows.Count > 0)
                    {
                        //Comercial
                        accorComercial.Enabled = true;
                        Session["AccionComercial"] = "Editar";
                        if (Session["btnEditarComer"].ToString() == "true")
                        {
                            btnGuardarComer.Visible = true;
                            btnGuardarPC.Visible = true;
                            btnGuardarComer.Text = "Actualizar";
                            btnGuardarPC.Text = "Actualizar";
                        }
                        else
                        {
                            btnGuardarComer.Visible = false;
                            btnGuardarPC.Visible = false;
                        }
                        foreach (DataRow row1 in usuComer.Rows)
                        {
                            Session["idUsuComercial"] = row1["idComer"].ToString();
                            txtNombre.Text = row1["nomComer"].ToString();
                            txtCorreo.Text = row1["correoComer"].ToString();
                            txtCelular.Text = row1["celComer"].ToString();
                            txtTel.Text = row1["telComer"].ToString();
                            txtOficina.Text = row1["oficina"].ToString();
                            txtDireccion.Text = row1["dirreOfi"].ToString();
                            cboPaisOfi.SelectedIndex = cboPaisOfi.Items.IndexOf(cboPaisOfi.Items.FindByValue(row1["idOfiPais"].ToString()));
                            chkActivoRepre.Checked = Boolean.Parse(row1["actComer"].ToString());
                            chkDirOfi.Checked = Boolean.Parse(row1["actDirOfiComer"].ToString());
                        }
                        DataTable paisComer = CP.buscarPaisComer(Session["idUsuComercial"].ToString());
                        DataTable ciuComer = CP.buscarCiuComer(Session["idUsuComercial"].ToString());
                        llenarListas(listaPaisAgg, paisComer);
                        llenarListas(listaCiudadAgg, ciuComer);
                    }
                    else
                    {
                        Session["AccionComercial"] = "Guardar";
                        accorComercial.Enabled = false;
                    }
                }
                else
                {
                    Session["Accion"] = "Guardar";
                }
            }
            else
            {
                Session["Accion"] = "Guardar";
            }
        }
        protected void chkEmpleadoF_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEmpleadoF.Checked == true)
            {
                txtCedula.Enabled = true;
                if (lblIdUsu.Value != "")
                {
                    accorCasino.Enabled = true;
                    accorSiif.Enabled = true;
                }
            }
            else
            {
                txtCedula.Enabled = false;
                accorCasino.Enabled = false;
                accorSiif.Enabled = false;
            }
        }
        protected void chkActivo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActivo.Checked == true)
            {
                if (lblIdUsu.Value != "")
                {
                    accorComercial.Enabled = true;
                }
            }
            else
            {
                accorComercial.Enabled = false;
            }
        }
        protected void cboZonaP_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboPais.Items.Clear();
            cboZonaC.Items.Clear();
            cboCiudad.Items.Clear();
            if (cboZonaP.SelectedItem.ToString() != "Seleccionar")
            {
                DataTable cargaPais = CP.cargarPaises("WHERE  (pai_grupopais_id = " + cboZonaP.SelectedValue.ToString() + ")");
                cboPais.Items.Add("Seleccionar");
                foreach (DataRow row in cargaPais.Rows)
                {
                    cboPais.Items.Add(new ListItem(row["nomPais"].ToString(), row["idPais"].ToString()));
                }
            }
            else { cboPais.Items.Clear(); }
        }
        protected void cboPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPais.SelectedItem.ToString() != "Seleccionar")
            {
                DataTable ciudades = CP.cargarCiudades(cboPais.SelectedValue.ToString(), "");
                cargaCiudad(ciudades);
                cargaZonaC(cboPais.SelectedValue.ToString());
            }
            else
            {
                cboZonaC.Enabled = false;
                cboCiudad.Enabled = false;
                cboZonaC.Items.Clear();
                cboCiudad.Items.Clear();
            }
        }
        protected void cboZonaC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboZonaC.SelectedItem.ToString() != "Seleccionar" || cboPais.SelectedItem.ToString() != "Seleccionar")
            {
                DataTable ciudades = CP.cargarCiudades(cboPais.SelectedValue.ToString(), "AND (ciu_zona = " + cboZonaC.SelectedValue.ToString() + ")");
                cargaCiudad(ciudades);
            }
            else
            {
                cboCiudad.Enabled = false;
            }
        }
        protected void txtCedula_TextChanged(object sender, EventArgs e)
        {
            buscaEmpleado(txtCedula.Text);
        }
        protected void txtCorreo_TextChanged(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script21", "ValEmail();", true);
        }
        //LISTA PLANTA
        protected void btnAgrPlanta_Click(object sender, EventArgs e)
        {
            if (cboPlanta.SelectedItem.ToString() != "Seleccionar")
            {
                agregarLista(listaPlantaAgg, cboPlanta.SelectedItem.ToString(), int.Parse(cboPlanta.SelectedValue.ToString()));//Pais
            }
        }
        protected void btnEliPlanta_Click(object sender, EventArgs e)
        {
            eliminarLista(listaPlantaAgg);
        }
        //LISTA PAIS
        protected void btnAgrTodosPais_Click(object sender, EventArgs e)
        {
            foreach (ListItem lis in cboPais.Items)
            {
                if (lis.Text != "Seleccionar")
                {
                    agregarLista(listaPaisAgg, lis.Text, int.Parse(lis.Value));//Pais
                }
            }
        }
        protected void btnEliTodosPais_Click(object sender, EventArgs e)
        {
            listaPaisAgg.Items.Clear();
            listaCiudadAgg.Items.Clear();
        }
        protected void btnAgrPais_Click(object sender, EventArgs e)
        {
            if (cboPais.SelectedItem.ToString() != "Seleccionar")
            {
                agregarLista(listaPaisAgg, cboPais.SelectedItem.ToString(), int.Parse(cboPais.SelectedValue.ToString()));//Pais
            }
        }
        protected void btnEliPais_Click(object sender, EventArgs e)
        {
            int idPais = listaPaisAgg.SelectedIndex;
            if (idPais >= 0)
            {
                DataTable ciudadesP = CP.cargarCiudades(listaPaisAgg.SelectedValue.ToString(), "");
                ListBox listReal = new ListBox();
                foreach (ListItem li in listaCiudadAgg.Items)
                {
                    int agg = 0;//Inicio la variable en 0 que significa que no agrege
                    foreach (DataRow row in ciudadesP.Rows)
                    {
                        if (li.Value == row["idCiudad"].ToString())//Si encuentra la ciudad, para el ciclo y mando la variable agg en 0 para que no agrege
                        {
                            agg = 0;
                            break;
                        }
                        else
                        {
                            agg = agg + 1;//Si no existe, va contanto hasta que pare y sea mayor q sero o que pare y cambie la variable en 0
                        }
                    }
                    if (agg >= 1)//si agg esta en 0 no agrego, si mayor agrego solo una vez
                    {
                        listReal.Items.Add(new ListItem(li.Text, li.Value));
                    }
                }
                listaCiudadAgg.Items.Clear();//Elimino todas las ciudades
                foreach (ListItem li2 in listReal.Items)//Vuelvo y agrego las ciudades que son
                {
                    listaCiudadAgg.Items.Add(new ListItem(li2.Text, li2.Value));
                }
            }
            //Borro el pais seleccionado
            eliminarLista(listaPaisAgg);
        }
        //LISTA CIUDAD
        protected void btnAgreTodosCiu_Click(object sender, EventArgs e)
        {
            foreach (ListItem lis in cboCiudad.Items)
            {
                if (lis.Text != "Seleccionar")
                {
                    agregarLista(listaCiudadAgg, lis.Text, int.Parse(lis.Value));//Pais
                }
            }
        }
        protected void btnEliTodosCiu_Click(object sender, EventArgs e)
        {
            listaCiudadAgg.Items.Clear();
        }
        protected void btnAgreCiu_Click(object sender, EventArgs e)
        {
            if (cboCiudad.SelectedItem.ToString() != "Seleccionar")
            {
                agregarLista(listaCiudadAgg, cboCiudad.SelectedItem.ToString(), int.Parse(cboCiudad.SelectedValue.ToString()));//Ciudad
                DataTable paisCiu = null;
                paisCiu = CP.cargarPaisCiudad(cboCiudad.SelectedValue.ToString());
                foreach (DataRow row in paisCiu.Rows)
                {
                    agregarLista(listaPaisAgg, row["nomPais"].ToString(), int.Parse(row["idPais"].ToString()));//Pais
                }
            }
        }
        protected void btnEliCiu_Click(object sender, EventArgs e)
        {
            eliminarLista(listaCiudadAgg);
        }
        //Crear un nuevo usuario
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            limpiar();
            cargarCombos();
            cargarTabla();
            lblIdUsu.Value = "";
        }
        //Guarda el usuario
        protected void btnGuardarUsu_Click(object sender, EventArgs e)
        {
            int defecto = 0;
            if (listaPlantaAgg.SelectedItem == null)
            {
                listaPlantaAgg.SelectedIndex = 0;
            }            

            defecto = Convert.ToInt32(listaPlantaAgg.SelectedItem.Value);

            String modCedula = "";
            String cedula = "";
            String mensaje = "";
            if (chkEmpleadoF.Checked == true)
            {
                modCedula = ", usu_emp_usu_num_id = " + txtCedula.Text;
                cedula = txtCedula.Text;
            }
            else
            {
                cedula = "0";
                modCedula = "";
            }
            if (validaCamposUsu() == true)
            {
                String listPlanta = listaString(listaPlantaAgg);
                if (Session["Accion"].ToString() == "Guardar")
                {
                    Boolean existe = CP.buscarUsuLogin(txtLoginUsu.Text);
                    if (existe == true)
                    {
                        mensajeVentana("El usuario ya existe! ingrese otro por favor, gracias!");
                    }
                    else
                    {
                        mensaje = CP.insertaUsu(txtLoginUsu.Text, txtPassUsu.Text, cboRoles.SelectedValue.ToString(), chkActivo.Checked, Session["usuario"].ToString(), cedula);
                        if (mensaje.Substring(0, 1) != "E")
                        {
                            mensajeVentana("Se ha insertado correctamente!");
                            cargarTabla();
                            Session["Accion"] = "Editar";
                            Session["AccionComercial"] = "Guardar";
                            lblIdUsu.Value = mensaje;
                            txtLoginUsu.Enabled = false;
                            btnGuardarUsu.Text = "Actualizar";
                            accorComercial.Enabled = true;
                            CP.insertarPlantaUsu(lblIdUsu.Value, listPlanta,defecto);
                            chkActivo.Checked = true;
                        }
                        else
                        {
                            mensajeVentana(mensaje);
                        }
                    }
                }
                else if (Session["Accion"].ToString() == "Editar")
                {
                    mensaje = CP.editarUsu(lblIdUsu.Value, txtPassUsu.Text, cboRoles.SelectedValue.ToString(), chkActivo.Checked, Session["usuario"].ToString(), modCedula);
                    if (mensaje == "OK")
                    {
                        mensajeVentana("Se ha modificado correctamente!");
                        cargarTabla();
                        Session["Accion"] = "Editar";
                        Session["AccionComercial"] = "Editar";
                        txtLoginUsu.Enabled = false;
                        btnGuardarUsu.Text = "Actualizar";
                        accorComercial.Enabled = true;
                        CP.insertarPlantaUsu(lblIdUsu.Value, listPlanta,defecto);
                    }
                    else { mensajeVentana(mensaje); }
                }
            }
            else
            {
                validaCamposUsu();
            }
        }
        //Guarda el comercial
        protected void btnGuardarComer_Click(object sender, EventArgs e)
        {
            String mensaje = "";
            if (validaCamposComer() == true)
            {
                if (Session["AccionComercial"].ToString() == "Guardar")
                {
                    mensaje = CP.insertaComer(lblIdUsu.Value, txtNombre.Text, txtCorreo.Text, chkDirOfi.Checked, txtOficina.Text, txtDireccion.Text, txtTel.Text, txtCelular.Text, cboPaisOfi.SelectedValue.ToString());
                    if (mensaje.Substring(0, 1) != "E")
                    {
                        mensajeVentana("Se ha insertado correctamente!");
                        Session["idUsuComercial"] = mensaje;
                        Session["AccionComercial"] = "Editar";
                        chkActivoRepre.Checked = true;
                        btnGuardarComer.Text = "Actualizar";
                        btnGuardarPC.Text = "Guardar";
                    }
                    else
                    {
                        mensajeVentana(mensaje);
                    }
                }
                else if (Session["AccionComercial"].ToString() == "Editar")
                {
                    mensaje = CP.editarComer(lblIdUsu.Value, txtNombre.Text, txtCorreo.Text, chkActivoRepre.Checked, chkDirOfi.Checked, txtOficina.Text, txtDireccion.Text, txtTel.Text, txtCelular.Text, cboPaisOfi.SelectedValue.ToString());
                    if (mensaje == "OK")
                    {
                        mensajeVentana("Se ha modificado correctamente!");
                        btnGuardarComer.Text = "Actualizar";
                        btnGuardarPC.Text = "Actualizar";
                        Session["AccionComercial"] = "Editar";
                    }
                    else { mensajeVentana(mensaje); }
                }
            }
            else
            {
                validaCamposComer();
            }
        }
        //Guarda los paises y ciudades
        protected void btnGuardarPC_Click(object sender, EventArgs e)
        {
            if (Session["idUsuComercial"].ToString() != "")
            {
                if (chkActivoRepre.Checked == true)
                {
                    if (listaPaisAgg.Items.Count >= 1)
                    {
                        String listPlanta = listaString(listaPaisAgg);
                        String listCiudad = listaString(listaCiudadAgg);
                        String men1 = CP.insertarPaisesComer(Session["idUsuComercial"].ToString(), listPlanta);
                        String men2 = "";
                        if (listaCiudadAgg.Items.Count >= 1)
                        {
                            men2 = CP.insertarCiudadesComer(Session["idUsuComercial"].ToString(), listCiudad);
                        }
                        else
                        {
                            men2 = CP.anulaTodosCiuComer(Session["idUsuComercial"].ToString());
                        }
                        if (men1 == "OK")
                        {
                            if (men2 == "OK")
                            {
                                if (btnGuardarPC.Text == "Actualizar")
                                {
                                    mensajeVentana("Se ha actualizado correctamente!");
                                }
                                else
                                {
                                    mensajeVentana("Se ha guardado correctamente!");
                                }
                            }
                            else { mensajeVentana(men2); }
                        }
                        else { mensajeVentana(men1); }
                    }
                    else
                    {
                        mensajeVentana("Debe de tener al menos un pais relacionado, gracias!");
                    }
                }
                else { mensajeVentana("El comercial tiene que estar activo, gracias!"); }
            }
            else
            {
                mensajeVentana("Tiene que crear el comercial, gracias!");
            }
            btnGuardarPC.Text = "Actualizar";
        }
        //Guarda los permisos de casino
        protected void btnGuardarCas_Click(object sender, EventArgs e)
        {
            String mensaje = CP.editarCasino(txtLoginUsu.Text, chkPedArea.Checked, chkAdmiCas.Checked, chkArchivoP.Checked);
            if (mensaje == "OK")
            {
                mensajeVentana("Se ha guardado los permisos correctamente!");
            }
            else { mensajeVentana(mensaje); }
        }
        //Guarda los permisos de siif
        protected void btnGuardarPlanta_Click(object sender, EventArgs e)
        {
            String filtroIdProper = "";
            if (cboProPer.SelectedItem.ToString() != "Seleccionar")
            {
                filtroIdProper = ", empleado.emp_reg_proceso = " + cboProPer.SelectedValue.ToString();
            }
            else {
                filtroIdProper = "";
            }

            String mensaje = CP.editarSiif(txtLoginUsu.Text, filtroIdProper, chkFormaleta.Checked, chkAccesorios.Checked);
            if (mensaje == "OK")
            {
                mensajeVentana("Se ha guardado los permisos correctamente!");
            }
            else { mensajeVentana(mensaje); }
        }
        //Guarda los valores de la lista y la guarda en una lista string
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
        //Llena los listBox con los datos
        private void llenarListas(ListBox lista, DataTable tabla)
        {
            foreach (DataRow row in tabla.Rows)
            {
                lista.Items.Add(new ListItem(row[1].ToString(), row[0].ToString()));
            }            
        }
        //Valida todos los campos
        private Boolean validaCamposUsu()
        {
            Boolean confi = false;
            if (Session["Accion"] == null)
            {
                mensajeVentana("Por favor realice una accion, gracias!");
            }
            else
            {
                if (txtLoginUsu.Text == "")
                {
                    mensajeVentana("Por favor ingrese el usuario, gracias!");
                }
                else
                {
                    if (listaPlantaAgg.Items.Count >= 1)
                    {
                        if (txtPassUsu.Text == "")
                        {
                            mensajeVentana("Por favor ingrese la contraseña, gracias!");
                        }
                        else
                        {
                            if (cboRoles.SelectedItem.ToString() == "Seleccionar")
                            {
                                mensajeVentana("Por favor seleccione el rol, gracias!");
                            }
                            else
                            {
                                if (chkEmpleadoF.Checked == true)
                                {
                                    if (txtCedula.Text == "")
                                    {
                                        mensajeVentana("Por favor ingrese la cedula, gracias!");
                                    }
                                    else
                                    {
                                        if (lblNomEmp.Text == "No existe!")
                                        {
                                            mensajeVentana("El empleado no existe!");
                                        }
                                        else
                                        {
                                            confi = true;
                                        }
                                    }
                                }
                                else
                                {
                                    confi = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        mensajeVentana("Debe de tener al menos una planta relacionada, gracias!");
                    }
                }
            }
            return confi;
        }
        private Boolean validaCamposComer()
        {
            Boolean confi = false;
            if (txtNombre.Text == "")
            {
                mensajeVentana("Por favor ingrese el nombre, gracias!");
            }
            else
            {
                if (txtCorreo.Text == "")
                {
                    mensajeVentana("Por favor ingrese el correo, gracias!");
                }
                else
                {
                    if (txtCelular.Text == "")
                    {
                        mensajeVentana("Por favor ingrese el celular, gracias!");
                    }
                    else
                    {
                        if (txtTel.Text == "")
                        {
                            mensajeVentana("Por favor ingrese el telefono, gracias!");
                        }
                        else
                        {
                            if (txtOficina.Text == "")
                            {
                                mensajeVentana("Por favor ingrese la oficina, gracias!");
                            }
                            else
                            {
                                if (txtDireccion.Text == "")
                                {
                                    mensajeVentana("Por favor ingrese la direccion, gracias!");
                                }
                                else
                                {
                                    if (cboPaisOfi.SelectedItem.ToString() == "Seleccionar")
                                    {
                                        mensajeVentana("Por favor seleccione el pais de la oficina, gracias!");
                                    }
                                    else
                                    {
                                        Boolean email = validaCorreo(txtCorreo.Text);
                                        if (email == false)
                                        {
                                            mensajeVentana("La dirección del correo es incorrecta!!");
                                            txtCorreo.Text = "";
                                        }
                                        else
                                        {
                                            confi = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return confi;
        }
        //Busca el nombre y area del empleado
        private void buscaEmpleado(String cedula)
        {
            DataTable empleado = null;
            empleado = CP.consultaEmpleado(cedula);
            if (empleado.Rows.Count >= 1)
            {
                txtCedula.BackColor = System.Drawing.ColorTranslator.FromHtml("#95FF89");
                foreach (DataRow row in empleado.Rows)
                {
                    lblNomEmp.Text = row["nomEmp"].ToString().ToLower();
                }
            }
            else
            {
                txtCedula.BackColor = System.Drawing.ColorTranslator.FromHtml("#FF7272");
                lblNomEmp.Text = "No existe!";
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
                btnGuardarUsu.Visible = true;
                btnGuardarComer.Visible = true;
                btnGuardarPC.Visible = true;
                btnGuardarPlanta.Visible = true;
                btnGuardarCas.Visible = true;
                btnNuevo.Visible = true;
            }
            else
            {
                btnGuardarUsu.Visible = false;
                btnGuardarComer.Visible = false;
                btnGuardarPC.Visible = false;
                btnGuardarPlanta.Visible = false;
                btnGuardarCas.Visible = false;
                btnNuevo.Visible = false;
            }

            if (editar == true)
            {
                Session["btnEditarUsu"] = "true";
                Session["btnEditarComer"] = "true";
            }
            else
            {
                Session["btnEditarUsu"] = "false";
                Session["btnEditarComer"] = "false";
            }
        }
        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }
        private void limpiar()
        {
            txtLoginUsu.Enabled = true;
            txtLoginUsu.Text = "";
            txtPassUsu.Text = "";
            txtPassUsu.Attributes.Add("value", "");
            txtCedula.Text = "";
            txtCedula.Enabled = false;
            txtCedula.BackColor = System.Drawing.Color.White;
            lblNomEmp.Text = "";
            btnGuardarUsu.Text = "Guardar";
            chkActivo.Checked = false;
            chkEmpleadoF.Checked = false;
            //
            accorCasino.Enabled = false;
            accorSiif.Enabled = false;
            accorComercial.Enabled = false;
            //
            txtNombre.Text = "";
            txtCorreo.Text = "";
            txtCelular.Text = "";
            txtTel.Text = "";
            txtOficina.Text = "";
            txtDireccion.Text = "";
            btnGuardarComer.Text = "Guardar";
            // 
            chkDirOfi.Checked = false;
            chkActivoRepre.Checked = false;
            // 
            cboPais.Items.Clear();
            cboCiudad.Items.Clear();
            listaCiudadAgg.Items.Clear();
            listaPaisAgg.Items.Clear();
            listaPlantaAgg.Items.Clear();
            btnGuardarPC.Text = "Guardar";
            // 
            chkFormaleta.Checked = false;
            chkAccesorios.Checked = false;
            //
            chkAdmiCas.Checked = false;
            chkPedArea.Checked = false;
            chkArchivoP.Checked = false;
            //
            Session["Accion"] = "Guardar";
            Session["idUsuComercial"] = "";
            Session["AccionComercial"] = "Guardar";
        }
        private void cargarTabla()
        {
            DataTable usuario = null;
            usuario = CP.cargarTablaUsu();
            grdUsuarios.DataSource = usuario;
            grdUsuarios.DataBind();
        }
        private void cargarCombos()
        {
            cboRoles.Items.Clear();
            DataTable cargaRoles = CP.cargarListaRoles();//Carga el combo cada vez que carga la pagina
            cboRoles.Items.Add("Seleccionar");
            foreach (DataRow row in cargaRoles.Rows)
            {
                cboRoles.Items.Add(new ListItem(row["nomRol"].ToString(), row["rolId"].ToString()));
            }
            /*********/
            cboZonaP.Items.Clear();
            DataTable cboZonaPais = CP.cargarZonasP();//Carga el combo cada vez que carga la pagina
            cboZonaP.Items.Add("Seleccionar");
            foreach (DataRow row in cboZonaPais.Rows)
            {
                cboZonaP.Items.Add(new ListItem(row["nomZonaP"].ToString(), row["idZonaP"].ToString()));
            }
            /******/
            cboPaisOfi.Items.Clear();
            DataTable cboPaisOficina = CP.cargarPaises("");//Carga el combo cada vez que carga la pagina
            cboPaisOfi.Items.Add("Seleccionar");
            foreach (DataRow row in cboPaisOficina.Rows)
            {
                cboPaisOfi.Items.Add(new ListItem(row["nomPais"].ToString(), row["idPais"].ToString()));
            }
            /******/
            cboPlanta.Items.Clear();
            DataTable cboPlantas = CP.cargarPlantas();//Carga el combo cada vez que carga la pagina
            cboPlanta.Items.Add("Seleccionar");
            foreach (DataRow row in cboPlantas.Rows)
            {
                cboPlanta.Items.Add(new ListItem(row["nomPlanta"].ToString(), row["idPlanta"].ToString()));
            }
            /******/
            cboProPer.Items.Clear();
            DataTable cboProPermiso = CP.cargarProPer();//Carga el combo cada vez que carga la pagina
            cboProPer.Items.Add("Seleccionar");
            foreach (DataRow row in cboProPermiso.Rows)
            {
                cboProPer.Items.Add(new ListItem(row["nomProc"].ToString(), row["idProc"].ToString()));
            }
        }
        //Metodo general para agregar las listas del pais y ciudad
        private void agregarLista(ListBox list, String item, int value)
        {
            if (value >= 0)//verifico si hay algo en el id del combo
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
        //Elimina el item selecionado
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
        //Carga las ciudades dependidendo del pais y tambien para filtrar con la zona de la ciudad
        private void cargaCiudad(DataTable cargaCiudad)
        {
            cboCiudad.Enabled = true;
            cboCiudad.Items.Clear();
            cboCiudad.Items.Add("Seleccionar");
            foreach (DataRow row in cargaCiudad.Rows)
            {
                cboCiudad.Items.Add(new ListItem(row["nomCiu"].ToString(), row["idCiudad"].ToString()));
            }
        }
        //Carga las zonas del las ciudades por medio del pais
        private void cargaZonaC(String idPais)
        {
            cboZonaC.Enabled = true;
            cboZonaC.Items.Clear();
            DataTable cargaZonasC = CP.cargarZonasCPais(idPais);
            cboZonaC.Items.Add("Seleccionar");
            foreach (DataRow row in cargaZonasC.Rows)
            {
                cboZonaC.Items.Add(new ListItem(row["nomZonaC"].ToString(), row["idZonaC"].ToString()));
            }
        }
        //Me valida si el correo esta bien escrito
        private Boolean validaCorreo(String correo)
        {
            String expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(correo, expresion))
            {
                if (Regex.Replace(correo, expresion, String.Empty).Length == 0)
                {  return true; }
                else  {  return false; }
            }
            else {  return false; }
        }
    }
}