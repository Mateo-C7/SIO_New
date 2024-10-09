using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Net;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using CapaControl;

namespace SIO
{
    public partial class MobilContacto : System.Web.UI.Page
    {
        public SqlDataReader reader = null;
        public SqlDataReader readerPaisObra = null;
        private DataSet dsMobileGeneral = new DataSet();
        private DataSet dsMobil = new DataSet();
        public ControlUbicacion controlUbi = new ControlUbicacion();
        public ControlCliente concli = new ControlCliente();
        public ControlInicio controlInicio = new ControlInicio();
        public SqlDataReader readerPais = null;
        public ControlMobileGeneral controlMobileG = new ControlMobileGeneral();
        public SqlDataReader readerCiudad = null;
        public SqlDataReader readerCargo = null;
        public SqlDataReader readerCiudadObra = null;
        public ControlContacto controlCont = new ControlContacto();
        public ControlObra controlObra = new ControlObra();

        string nomUsuario = "";
        string rcID = "0";
        string pais = "0";
        string correoUsu = "";
        string area = "0";

        protected void Page_Load(object sender, EventArgs e)
        {
            //string rcID = (string)Session["rcID"];
            //int rol = (int)Session["Rol"];
            //string pais = (string)Session["pais"];
            

            if (!IsPostBack)
            {
                //this.cargarDatosRep();
                //this.cargarPaisesRol();
                //this.Idioma();
                //this.cargarCombosIdiomas();                
            }
        }

        //CARGAMOS LOS DATOS DEL REPRESENTANTE
        private void cargarDatosRep()
        {
            string idioma = (string)Session["Idioma"];
            string usuconect = (string)Session["Usuario"];
            int rol = (int)Session["Rol"];

            if ((rol == 3) || (rol == 9) || (rol == 2) || (rol == 28))
            {
                reader = controlInicio.ObtenerRepresentante(usuconect);
                reader.Read();

                nomUsuario = reader.GetValue(1).ToString();
                rcID = reader.GetValue(0).ToString();
                correoUsu = reader.GetValue(2).ToString();
                reader.Close();
            }
            else
            {
                reader = controlInicio.consultarNombre(usuconect);
                reader.Read();

                nomUsuario = reader.GetValue(0).ToString();
                correoUsu = reader.GetValue(1).ToString();
                area = reader.GetValue(2).ToString();
                reader.Close();
            }

            reader = controlInicio.obtenerDatosPaisRepresentante(Convert.ToInt32(rcID));
            if (reader != null)
            {
                while (reader.Read())
                {
                    if (reader.GetInt32(1) == 8)
                        pais = "8";
                }
            }
            reader.Close();

            //CARGA LAS VARIABLES DE SESION PARA UTILIZARLAS EN TODAS LAS PAGINAS
            Session["Usuario"] = usuconect;
            Session["Nombre_Usuario"] = nomUsuario;
            Session["rcID"] = rcID;
            Session["Pais"] = pais;
            Session["rcEmail"] = correoUsu;
            Session["Rol"] = rol;
            Session["Area"] = area;
        }

        //CARGAMOS EL IDIOMA DE TODA LA PAGINA
        private void Idioma()
        {
            int idiomaId = 0;
            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")
                idiomaId = 2;
            if (idioma == "Ingles")
                idiomaId = 3;
            if (idioma == "Portugues")
                idiomaId = 4;

            int posicion = 0;

            dsMobileGeneral = controlMobileG.ConsultarIdiomaMobile();

            foreach (DataRow fila in dsMobileGeneral.Tables[0].Rows)
            {
                posicion = posicion + 1;
                if (posicion == 1)
                    lblEncabPais.Text = fila[idiomaId].ToString();
                if (posicion == 2)
                    lblEncabCiudad.Text = fila[idiomaId].ToString();
                if (posicion == 3)
                    lblEncabCliente.Text = fila[idiomaId].ToString();
                if (posicion == 4)
                    lblEncabContacto.Text = fila[idiomaId].ToString();
                if (posicion == 5)
                    lblEncabObra.Text = fila[idiomaId].ToString();
                if (posicion == 6)
                    lblClienteNombre.Text = fila[idiomaId].ToString();
                if (posicion == 7)
                    lblDireccCliente.Text = fila[idiomaId].ToString();
                if (posicion == 8)
                    lblTelefoCliente.Text = fila[idiomaId].ToString();
                if (posicion == 9)
                    btnNuevoCliente.Text = fila[idiomaId].ToString();
                if (posicion == 10)
                    btnGuardarCliente.Text = fila[idiomaId].ToString();
                if (posicion == 11)
                    lblClienteCont.Text = fila[idiomaId].ToString();
                if (posicion == 12)
                    lblNombresCont.Text = fila[idiomaId].ToString();
                if (posicion == 13)
                    lblApellidosCont.Text = fila[idiomaId].ToString();
                if (posicion == 14)
                    lblCargoCont.Text = fila[idiomaId].ToString();
                if (posicion == 15)
                    lblTelefonoCont.Text = fila[idiomaId].ToString();
                if (posicion == 16)
                    lblCorreoCont.Text = fila[idiomaId].ToString();
                if (posicion == 17)
                    btnNuevoCont.Text = fila[idiomaId].ToString();
                if (posicion == 18)
                    btnGuardarCont.Text = fila[idiomaId].ToString();
                if (posicion == 19)
                    lblNombreObra.Text = fila[idiomaId].ToString();
                if (posicion == 20)
                    lblTipoObra.Text = fila[idiomaId].ToString();
                if (posicion == 21)
                    lblPaisObra.Text = fila[idiomaId].ToString();
                if (posicion == 22)
                    lblCiudadObra.Text = fila[idiomaId].ToString();
                if (posicion == 23)
                    lblEstratoObra.Text = fila[idiomaId].ToString();
                if (posicion == 24)
                    btnNuevoObra.Text = fila[idiomaId].ToString();
                if (posicion == 25)
                    btnGuardarObra.Text = fila[idiomaId].ToString();
                if (posicion == 26)
                    lblClienteObra.Text = fila[idiomaId].ToString();
            }
            dsMobileGeneral.Tables.Remove("Table");
            dsMobileGeneral.Dispose();
            dsMobileGeneral.Clear();
        }

        //CARGAMOS LOS PAISES 
        private void cargarPaisesRol()
        {
            int arRol = (int)Session["Rol"];
            if ((arRol == 3) || (arRol == 28) || (arRol == 29))
            {
                this.poblarListaPais();
            }
            else
            {
                this.poblarListaPais2();
            }
        }

        //METODO DONDE CARGAMOS TODOS LOS PAISES
        private void poblarListaPais2()
        {
            //CboPaisMatriz.Items.Clear();
            readerPais = controlUbi.poblarListaPais();
            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")
            {
                CboPaisMatrizm.Items.Add(new ListItem("Seleccione el Pais", "0"));
            }
            if (idioma == "Ingles")
            {
                CboPaisMatrizm.Items.Add(new ListItem("Select the Country", "0"));
            }
            if (idioma == "Portugues")
            {
                CboPaisMatrizm.Items.Add(new ListItem("Selecione o País", "0"));
            }

            if (readerPais != null)
            {
                while (readerPais.Read())
                {
                    CboPaisMatrizm.Items.Add(new ListItem(readerPais.GetString(1), readerPais.GetInt32(0).ToString()));
                }
            }
            else
            {
                //lblMensaje.Visible = true;
                //lblMensaje.Text = "Usted no posee paises asociados.";
            }

            readerPais.Close();
            controlUbi.cerrarConexion();
        }

        //METODO DONDE CARGAMOS LOS PAISES DE ACUERDO AL ID DEL REPRESENTANTE
        private void poblarListaPais()
        {
            string rcID = (string)Session["rcID"];

            //CboPaisMatriz.Items.Clear();
            //CboPaisMatriz.Items.Clear();
            readerPais = controlUbi.poblarListaPaisRepresentante(Convert.ToInt32(rcID));
            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")
            {
                CboPaisMatrizm.Items.Add(new ListItem("Seleccione el Pais", "0"));
            }
            if (idioma == "Ingles")
            {
                CboPaisMatrizm.Items.Add(new ListItem("Select the Country", "0"));
            }
            if (idioma == "Portugues")
            {
                CboPaisMatrizm.Items.Add(new ListItem("Selecione o País", "0"));
            }

            if (readerPais != null)
            {
                while (readerPais.Read())
                {
                    CboPaisMatrizm.Items.Add(new ListItem(readerPais.GetString(0), readerPais.GetInt32(1).ToString()));
                }
            }
            else
            {
                //lblMensaje.Visible = true;
                //lblMensaje.Text = "Usted no posee paises asociados.";
            }
            readerPais.Close();
            controlUbi.cerrarConexion();
        }

        //CARGAMOS LOS COMBOS CON EL IDIOMA INDICADO
        public void cargarCombosIdiomas()
        {
            string idioma = (string)Session["Idioma"];
            this.cargarComboClienteIdiomas();
            this.CargarCargo();
            this.poblarTipoVivienda();
            this.poblarListaPaisTodos();
            this.cargarComboCiudadIdiomas();
            this.poblarEstadoSocioeconomico();
        }

        //CARGAMOS EL COMBO DE CLIENTE INICIALMENTE POR IDIOMAS
        public void cargarComboClienteIdiomas()
        {
            cboClienteCont.Items.Clear();
            cboClienteObra.Items.Clear();
            string idioma = (string)Session["Idioma"];

            if (idioma == "Español")
            {
                cboClienteCont.Items.Add(new ListItem("Seleccione el Cliente", "0"));
                cboClienteObra.Items.Add(new ListItem("Seleccione el Cliente", "0"));
            }
            if (idioma == "Ingles")
            {
                cboClienteCont.Items.Add(new ListItem("Select Customer", "0"));
                cboClienteObra.Items.Add(new ListItem("Select Customer", "0"));
            }
            if (idioma == "Portugues")
            {
                cboClienteCont.Items.Add(new ListItem("Selecione Cliente", "0"));
                cboClienteObra.Items.Add(new ListItem("Selecione Cliente", "0"));
            }
        }

        //CARGAMOS EL COMBO DE CARGOS INICIALMENTE POR IDIOMAS
        private void CargarCargo()
        {
            cboCargoCont.Items.Clear();
            readerCargo = controlCont.obtenerCargo();
            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")
            {
                cboCargoCont.Items.Add(new ListItem("Seleccione el Cargo", "0"));
            }
            if (idioma == "Ingles")
            {
                cboCargoCont.Items.Add(new ListItem("Select the Charge", "0"));
            }
            if (idioma == "Portugues")
            {
                cboCargoCont.Items.Add(new ListItem("Selecione a Carga", "0"));
            }
            while (readerCargo.Read())
            {
                cboCargoCont.Items.Add(new ListItem(readerCargo.GetString(1), readerCargo.GetInt32(0).ToString()));
            }
            readerCargo.Close();
            controlCont.cerrarConexion();
        }

        //CARGAMOS EL COMBO DE CON LOS TIPOS DE VIVIENDA INICIALMENTE POR IDIOMAS
        private void poblarTipoVivienda()
        {
            cboTipoObra.Items.Clear();
            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")
            {
                cboTipoObra.Items.Add(new ListItem("Seleccione el Tipo", "0"));
            }
            if (idioma == "Ingles")
            {
                cboTipoObra.Items.Add(new ListItem("Select the Type", "0"));
            }
            if (idioma == "Portugues")
            {
                cboTipoObra.Items.Add(new ListItem("Selecione a Tipo", "0"));
            }
            cboTipoObra.Items.Add("Apartamento");
            cboTipoObra.Items.Add("Casa");
            cboTipoObra.Items.Add("Hotel");
            cboTipoObra.Items.Add("Carcel");
            cboTipoObra.Items.Add("Otro");
        }

        //METODO DONDE CARGAMOS TODOS LOS PAISES PARA LA OBRA POR IDIOMAS
        private void poblarListaPaisTodos()
        {
            cboPaisObra.Items.Clear();
            readerPaisObra = controlUbi.poblarListaPais();
            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")
            {
                cboPaisObra.Items.Add(new ListItem("Seleccione el Pais", "0"));
            }
            if (idioma == "Ingles")
            {
                cboPaisObra.Items.Add(new ListItem("Select the Country", "0"));
            }
            if (idioma == "Portugues")
            {
                cboPaisObra.Items.Add(new ListItem("Selecione o País", "0"));
            }

            if (readerPaisObra != null)
            {
                while (readerPaisObra.Read())
                {
                    cboPaisObra.Items.Add(new ListItem(readerPaisObra.GetString(1), readerPaisObra.GetInt32(0).ToString()));
                }
            }
            else
            {
                //lblMensaje.Visible = true;
                //lblMensaje.Text = "Usted no posee paises asociados.";
            }

            readerPaisObra.Close();
            controlUbi.cerrarConexion();
        }

        //CARGAMOS EL COMBO DE CIUDAD INICIALMENTE POR IDIOMAS
        public void cargarComboCiudadIdiomas()
        {
            cboCiudadObra.Items.Clear();
            cboCiudadMatrizm.Items.Clear();
            string idioma = (string)Session["Idioma"];

            if (idioma == "Español")
            {
                cboCiudadMatrizm.Items.Add(new ListItem("Seleccione la Ciudad", "0"));
                cboCiudadObra.Items.Add(new ListItem("Seleccione la Ciudad", "0"));
            }
            if (idioma == "Ingles")
            {
                cboCiudadMatrizm.Items.Add(new ListItem("Select the City", "0"));
                cboCiudadObra.Items.Add(new ListItem("Select the City", "0"));
            }
            if (idioma == "Portugues")
            {
                cboCiudadMatrizm.Items.Add(new ListItem("Selecione a Cidade", "0"));
                cboCiudadObra.Items.Add(new ListItem("Selecione a Cidade", "0"));
            }
        }

        //CARGAMOS EL COMBO DE CIUDAD OBRA INICIALMENTE POR IDIOMAS
        public void cargarComboCiudadObraIdiomas()
        {
            cboCiudadObra.Items.Clear();
            string idioma = (string)Session["Idioma"];

            if (idioma == "Español")
            {
                cboCiudadObra.Items.Add(new ListItem("Seleccione la Ciudad", "0"));
            }
            if (idioma == "Ingles")
            {
                cboCiudadObra.Items.Add(new ListItem("Select the City", "0"));
            }
            if (idioma == "Portugues")
            {
                cboCiudadObra.Items.Add(new ListItem("Selecione a Cidade", "0"));
            }
        }


        //CARGAMOS EL COMBO DE ESTRATO SOCIOECONOMICO INICIALMENTE POR IDIOMAS
        private void poblarEstadoSocioeconomico()
        {
            cboEstratoObra.Items.Clear();
            reader = controlMobileG.poblarEstadoSocioEconomico();
            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")
            {
                cboEstratoObra.Items.Add(new ListItem("Seleccione el Estrato", "-1"));
            }
            if (idioma == "Ingles")
            {
                cboEstratoObra.Items.Add(new ListItem("Select the Stratum", "-1"));
            }
            if (idioma == "Portugues")
            {
                cboEstratoObra.Items.Add(new ListItem("Selecione o Estrato", "-1"));
            }
            while (reader.Read())
            {
                cboEstratoObra.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
            }
            reader.Close();
            controlMobileG.cerrarConexion();
        }


        protected void CboPaisMatrizm_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cargarComboClienteIdiomas();
            if (CboPaisMatrizm.SelectedItem.Value == "0")
            {
                this.cargarComboCiudadIdiomas();
            }
            else
            {
                this.poblarListaCiudad(Convert.ToInt32(CboPaisMatrizm.SelectedValue));
            }
        }

        //CARGAMOS LAS CIUDADES DEL PAIS SELECCIONADO
        private void poblarListaCiudad(int pais_id)
        {

            cboCiudadMatrizm.Items.Clear();

            string idioma = (string)Session["Idioma"];
            string rcID = (string)Session["rcID"];
            int arRol = (int)Session["Rol"];
            int seleccionado, rp_id;

            if (idioma == "Español")
            {
                cboCiudadMatrizm.Items.Add(new ListItem("Seleccione la Ciudad", "0"));
            }
            if (idioma == "Ingles")
            {
                cboCiudadMatrizm.Items.Add(new ListItem("Select the City", "0"));
            }
            if (idioma == "Portugues")
            {
                cboCiudadMatrizm.Items.Add(new ListItem("Selecione a Cidade", "0"));
            }

            if ((arRol == 3) && (Convert.ToInt32(CboPaisMatrizm.SelectedItem.Value) == 8))
            {
                readerCiudad = controlUbi.poblarCiudadesRepresentantesColombia(Convert.ToInt32(rcID));

                if (readerCiudad.HasRows == true)
                {
                    while (readerCiudad.Read())
                    {
                        cboCiudadMatrizm.Items.Add(new ListItem(readerCiudad.GetString(1), readerCiudad.GetInt32(0).ToString()));
                    }
                }
                readerCiudad.Close();
            }
            else
            {
                readerCiudad = controlUbi.poblarListaCiudades(Convert.ToInt32(CboPaisMatrizm.SelectedItem.Value));
                if (readerCiudad.HasRows == true)
                {
                    while (readerCiudad.Read())
                    {
                        cboCiudadMatrizm.Items.Add(new ListItem(readerCiudad.GetString(1), readerCiudad.GetInt32(0).ToString()));
                    }
                }
            }
        }

        //CARGAMOS LAS CIUDADES DEL PAIS SELECCIONADO
        private void poblarListaCiudadObra(int pais_id)
        {
            cboCiudadObra.Items.Clear();

            string idioma = (string)Session["Idioma"];
            string rcID = (string)Session["rcID"];
            int arRol = (int)Session["Rol"];
            int seleccionado, rp_id;

            if (idioma == "Español")
            {
                cboCiudadObra.Items.Add(new ListItem("Seleccione la Ciudad", "0"));
            }
            if (idioma == "Ingles")
            {
                cboCiudadObra.Items.Add(new ListItem("Select the City", "0"));
            }
            if (idioma == "Portugues")
            {
                cboCiudadObra.Items.Add(new ListItem("Selecione a Cidade", "0"));
            }

            if ((arRol == 3) && (Convert.ToInt32(cboPaisObra.SelectedItem.Value) == 8))
            {
                readerCiudadObra = controlUbi.poblarCiudadesRepresentantesColombia(Convert.ToInt32(rcID));

                if (readerCiudadObra.HasRows == true)
                {
                    while (readerCiudadObra.Read())
                    {
                        cboCiudadObra.Items.Add(new ListItem(readerCiudadObra.GetString(1), readerCiudadObra.GetInt32(0).ToString()));
                    }
                }
                readerCiudadObra.Close();
            }
            else
            {
                readerCiudadObra = controlUbi.poblarListaCiudades(Convert.ToInt32(cboPaisObra.SelectedItem.Value));
                if (readerCiudadObra.HasRows == true)
                {
                    while (readerCiudadObra.Read())
                    {
                        cboCiudadObra.Items.Add(new ListItem(readerCiudadObra.GetString(1), readerCiudadObra.GetInt32(0).ToString()));
                    }
                }
            }
        }

        //CARGAMOS LOS CLIENTES DE LA CIUDAD SELECCIONADA
        public bool poblarListaCliente()
        {
            int seleccionado;
            cboClienteCont.Items.Clear();
            cboClienteObra.Items.Clear();

            seleccionado = Convert.ToInt32(cboCiudadMatrizm.SelectedItem.Value);
            reader = controlCont.consultarDatosClientexCiudad(seleccionado);
            string idioma = (string)Session["Idioma"];
            bool respuesta = false;

            if (idioma == "Español")
            {
                cboClienteObra.Items.Add(new ListItem("Seleccione el Cliente", "0"));
                cboClienteCont.Items.Add(new ListItem("Seleccione el Cliente", "0"));
            }
            if (idioma == "Ingles")
            {
                cboClienteObra.Items.Add(new ListItem("Seleccione el Cliente", "0"));
                cboClienteCont.Items.Add(new ListItem("Select Customer", "0"));
            }
            if (idioma == "Portugues")
            {
                cboClienteObra.Items.Add(new ListItem("Seleccione el Cliente", "0"));
                cboClienteCont.Items.Add(new ListItem("Selecione Cliente", "0"));
            }
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboClienteObra.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                    cboClienteCont.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
                respuesta = true;
            }
            else
            {
                respuesta = false;
            }
            return respuesta;
        }

        //CARGAMOS LAS CIUDADES DEL PAIS SELECCIONADO
        protected void cboPaisObra_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPaisObra.SelectedItem.Value == "0")
            {
                this.cargarComboCiudadObraIdiomas();
            }
            else
            {
                this.poblarListaCiudadObra(Convert.ToInt32(cboPaisObra.SelectedValue));
            }
        }

        protected void cboCiudadMatrizm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCiudadMatrizm.SelectedItem.Value == "0")
            {
                this.cargarComboClienteIdiomas();
            }
            else
            {
                this.poblarListaCliente();
            }
        }

        //---GUARDAMOS  EL CLIENTE
        protected void btnGuardarCliente_Click(object sender, EventArgs e)
        {
            string mensaje = "";
            string idioma = (string)Session["Idioma"];
            string usuario = (string)Session["Usuario"];

            if (CboPaisMatrizm.SelectedItem.Value != "0" && cboCiudadMatrizm.SelectedItem.Value != "0")
            {
                if (txtClienteNombre.Text == "" || txtDireccCliente.Text == "" || txtTelefCliente.Text == "")
                {
                    if (idioma == "Español")
                    {
                        mensaje = "Digite todos los campos";
                    }
                    if (idioma == "Ingles")
                    {
                        mensaje = "Enter all fields";
                    }
                    if (idioma == "Portugues")
                    {
                        mensaje = "Digite todos os campos";
                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
                else
                {
                    txtClienteNombre.Text = txtClienteNombre.Text.ToString().ToUpperInvariant();
                    txtDireccCliente.Text = txtDireccCliente.Text.ToString().ToUpperInvariant();
                    txtTelefCliente.Text = txtTelefCliente.Text.ToString().ToUpperInvariant();

                    int insertadoCliente = concli.Matriz(txtClienteNombre.Text, Convert.ToInt32(CboPaisMatrizm.SelectedItem.Value), Convert.ToInt32(cboCiudadMatrizm.SelectedItem.Value),
                            txtDireccCliente.Text, txtTelefCliente.Text, "0", "0", "@", "W", "0",
                            1, usuario, "0", "0", "0", 0, 0, 1, 0, 0, "",0,0);

                    int actualizar = concli.ActualizarIDClienteMatriz(insertadoCliente);

                    if (insertadoCliente != -1 && actualizar != -1)
                    {
                        btnGuardarCliente.Enabled = false;
                        if (idioma == "Español")
                        {
                            mensaje = "Cliente creado exitosamente";
                        }
                        if (idioma == "Ingles")
                        {
                            mensaje = "Customer servant satisfactorily";
                        }
                        if (idioma == "Portugues")
                        {
                            mensaje = "Cliente Creado satisfactoriamente";
                        }
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        this.poblarListaCliente();
                    }
                    else
                    {
                        if (idioma == "Español")
                        {
                            mensaje = "No fue posible crear el Cliente";
                        }
                        if (idioma == "Ingles")
                        {
                            mensaje = "Unable to create the Customer";
                        }
                        if (idioma == "Portugues")
                        {
                            mensaje = "Incapaz de criar o Cliente";
                        }
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    }
                }
            }
            else
            {
                if (idioma == "Español")
                {
                    mensaje = "Seleccione Pais y Ciudad";
                }
                if (idioma == "Ingles")
                {
                    mensaje = "Select Country  and City";
                }
                if (idioma == "Portugues")
                {
                    mensaje = "Seleccione Pais y Cidade";
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
        }

        //LIMPIAMOS LOS CAMPOS PARA UN NUEVO CLIENTE
        protected void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            CboPaisMatrizm.Items.Clear();
            cargarCombosIdiomas();
            this.cargarPaisesRol();
            txtClienteNombre.Text = "";
            txtDireccCliente.Text = "";
            txtTelefCliente.Text = "";
            btnGuardarCliente.Enabled = true;
        }

        //GUARDAMOS EL CONTACTO
        protected void btnGuardarCont_Click1(object sender, EventArgs e)
        {
            string mensaje = "";
            string idioma = (string)Session["Idioma"];
            string usuario = (string)Session["Usuario"];
            string usucrea = (string)Session["Nombre_Usuario"];
            string fechacrea = DateTime.Now.ToString("dd/MM/yyyy");

            if (CboPaisMatrizm.SelectedItem.Value != "0" && cboCiudadMatrizm.SelectedItem.Value != "0")
            {
                if (cboClienteCont.SelectedItem.Value=="0" ||  txtNombresCont.Text == "" || txtApellidosCont.Text == ""
                    || cboCargoCont.SelectedItem.Value == "0" || txtTelefonoCont.Text == "" || txtCorreoCont.Text == "")
                {
                    if (idioma == "Español")
                    {
                        mensaje = "Digite todos los campos";
                    }
                    if (idioma == "Ingles")
                    {
                        mensaje = "Enter all fields";
                    }
                    if (idioma == "Portugues")
                    {
                        mensaje = "Digite todos os campos";
                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
                else
                {
                    int clienteIdCont = Convert.ToInt32( cboClienteCont.SelectedItem.Value);
                    string nombresCont = txtNombresCont.Text.ToString().ToUpperInvariant();
                    string apellidosCont = txtApellidosCont.Text.ToString().ToUpperInvariant();
                    int cardoIdCont = Convert.ToInt32(cboCargoCont.SelectedItem.Value);
                    string telCont = txtTelefonoCont.Text;
                    string correoCont = txtCorreoCont.Text;


                    int insertadoCont = controlCont.guardarDatosContactoCliente(clienteIdCont, 0, cardoIdCont, nombresCont, "", apellidosCont, "",
                                    telCont, "", correoCont, "", "", true, false, false, "", "01/01/1900", 0, usucrea, fechacrea,
                                    false, false, false, false, false, true, false, false, false, false, false, false, false, "",6,"","0","0","0",false,"");
                    if (insertadoCont != -1)
                    {
                        if (idioma == "Español")
                        {
                            mensaje = "Contacto creado exitosamente";
                        }
                        if (idioma == "Ingles")
                        {
                            mensaje = "Contact servant satisfactorily";
                        }
                        if (idioma == "Portugues")
                        {
                            mensaje = "Contacto Creado satisfactoriamente";
                        }
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        btnGuardarCont.Enabled = false;
                    }
                    else
                    {
                        if (idioma == "Español")
                        {
                            mensaje = "No fue posible crear el contacto";
                        }
                        if (idioma == "Ingles")
                        {
                            mensaje = "Unable to create the contact";
                        }
                        if (idioma == "Portugues")
                        {
                            mensaje = "Incapaz de criar o contato";
                        }
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    }
                }

            }
            else 
            {
                if (idioma == "Español")
                {
                    mensaje = "Seleccione Pais y Ciudad";
                }
                if (idioma == "Ingles")
                {
                    mensaje = "Select Country  and City";
                }
                if (idioma == "Portugues")
                {
                    mensaje = "Seleccione Pais y Cidade";
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
        }

        //GUARDAMOS LA OBRA
        protected void btnGuardarObra_Click(object sender, EventArgs e)
        {
            string mensaje = "";
            string idioma = (string)Session["Idioma"];
            string usuario = (string)Session["Usuario"];
            string fechacrea = DateTime.Now.ToString("dd/MM/yyyy");

            if (CboPaisMatrizm.SelectedItem.Value != "0" && cboCiudadMatrizm.SelectedItem.Value != "0")
            {
                if (cboClienteObra.SelectedItem.Value == "0" || txtNombreObra.Text == "" || cboTipoObra.SelectedItem.Value == "0"
                    || cboPaisObra.SelectedItem.Value == "0" || cboCiudadObra.SelectedItem.Value == "0" || cboEstratoObra.SelectedItem.Value == "-1")
                {
                    if (idioma == "Español")
                    {
                        mensaje = "Digite todos los campos";
                    }
                    if (idioma == "Ingles")
                    {
                        mensaje = "Enter all fields";
                    }
                    if (idioma == "Portugues")
                    {
                        mensaje = "Digite todos os campos";
                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);                
                }
                else
                {
                    int clienteIdObra = Convert.ToInt32(cboClienteObra.SelectedItem.Value);
                    string nombreObra = txtNombreObra.Text.ToString().ToUpperInvariant();
                    string tipoObra = cboTipoObra.SelectedItem.Text;
                    int paisObra = Convert.ToInt32(cboPaisObra.SelectedItem.Value);
                    int ciudadObra = Convert.ToInt32(cboCiudadObra.SelectedItem.Value);
                    int estratoObra = Convert.ToInt32(cboEstratoObra.SelectedItem.Value);

                    int insertadoObra = controlObra.guardarDatosObra(Convert.ToInt32(cboPaisObra.SelectedValue), Convert.ToInt32(cboCiudadObra.SelectedValue),
                    clienteIdObra, nombreObra, "Pendiente", "", "", 0, estratoObra, 0, usuario, tipoObra);

                    if (insertadoObra != -1)
                    {
                        if (idioma == "Español")
                        {
                            mensaje = "Obra creada exitosamente";
                        }
                        if (idioma == "Ingles")
                        {
                            mensaje = "Work servant satisfactorily";
                        }
                        if (idioma == "Portugues")
                        {
                            mensaje = "Obra Creada satisfactoriamente";
                        }
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        btnGuardarObra.Enabled = false;
                    }
                    else
                    {
                        if (idioma == "Español")
                        {
                            mensaje = "No fue posible crear la Obra";
                        }
                        if (idioma == "Ingles")
                        {
                            mensaje = "Unable to create the Work";
                        }
                        if (idioma == "Portugues")
                        {
                            mensaje = "Incapaz de criar o Obra";
                        }
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    }

                }
            }
            else 
            {

                if (idioma == "Español")
                {
                    mensaje = "Seleccione Pais y Ciudad";
                }
                if (idioma == "Ingles")
                {
                    mensaje = "Select Country  and City";
                }
                if (idioma == "Portugues")
                {
                    mensaje = "Seleccione Pais y Cidade";
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);           
            }
        }

        //LIMPIAMOS LOS CAMPOS PARA UN NUEVO CONTACTO
        protected void btnNuevoCont_Click(object sender, EventArgs e)
        {
            CboPaisMatrizm.Items.Clear();
            cargarCombosIdiomas();
            this.cargarPaisesRol();
            txtNombresCont.Text="";
            txtApellidosCont.Text="";
            txtTelefonoCont.Text="";
            txtCorreoCont.Text="";
            btnGuardarCont.Enabled = true;
        }

        //LIMPIAMOS LOS CAMPOS PARA UNA NUEVA OBRA
        protected void btnNuevoObra_Click1(object sender, EventArgs e)
        {
            CboPaisMatrizm.Items.Clear();
            cargarCombosIdiomas();
            this.cargarPaisesRol();
            txtNombreObra.Text = "";
            btnGuardarObra.Enabled = true;
        }
    }
}