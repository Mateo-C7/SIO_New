using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaControl;
using CapaDatos;
using Microsoft.Reporting.WebForms;

namespace SIO
{
    public partial class Cliente : System.Web.UI.Page
    {
        public SqlDataReader reader = null;
        public SqlDataReader readerevento = null;
        public SqlDataReader readerCliente = null;
        private DataSet dsCliente = new DataSet();
        public ControlUbicacion contubi = new ControlUbicacion();
        public ControlCliente concli = new ControlCliente();
        public BdDatos BdDatos = new BdDatos();
        public ControlObra contobra = new ControlObra();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Rol"] != null)
            {
                txtBusqueda.Focus();

                if (!IsPostBack)
                {
                    // verifico si el usuario puede eliminar                    
                    int idClienteUsuario = Convert.ToInt32(Session["IdClienteUsuario"]);
                    if (idClienteUsuario > 0)
                    {
                        btnEliminar.Visible = false;
                        lkCreaProyecto.Visible = false;
                        lkCreaPV.Visible = false;

                        lkCreaContacto.Visible = false;
                        lkCrearObra.Visible = false;
                    }


                    //  this.Idioma();
                    this.PoblarTipoContribuyente();
                    this.PoblarTipoApoyo();
                    //this.PoblarTipoClientePlanta();                
                    //this.PoblarBusqClientes();

                    string rcID = (string)Session["rcID"];
                    int rol = (int)Session["Rol"];
                    string pais = (string)Session["Pais"];

                    //250618--
                    if ((rol == 5))
                    {
                        chk_EmpreCompe.Visible = false;
                        lbl_EmpreCompete.Visible = false;
                        cbo_Fuente.Visible = false;
                        cbo_Origen.Visible = false;
                        lblFuente.Visible = false;
                        lblOrigen.Visible = false;
                    }
                    else
                    {
                        chk_EmpreCompe.Visible = false;
                        lbl_EmpreCompete.Visible = false;
                        cbo_Fuente.Visible = false;
                        cbo_Origen.Visible = false;
                        lblFuente.Visible = false;
                        lblOrigen.Visible = false;
                    }
                    //------

                    if (rol == 9) this.activarCamposAsistente();

                    this.cargarComboCiudadIdiomas();
                
                    if (Request.QueryString["idCliente"] != null)
                    {

                        //string cliente = Session["Cliente"].ToString();
                        string cliente = Request.QueryString["idCliente"];
                        Session["Cliente"] = cliente;
                        Session["idCliente"] = cliente;
                    
                        this.PoblarCliente();
                        this.PoblarPlanta(Convert.ToInt32(cliente));
                        this.activarLink();
                        //lkCreaContacto.Visible = true;
                        //LkverHV.Visible = true;
                        //lkCrearObra.Visible = true;
                        //lblCrear.Visible = true;
                        //lkCreaProyecto.Visible = true;
                        //lkCreaPV.Visible = true;
                         
                        if(idClienteUsuario == 0) this.CargarReporteCliente("Contacto");

                        if (Request.QueryString["Sucursal"] != null)
                        {
                            string sucursal = (string )Session["Sucursal"] ;
                            if (sucursal == "1")
                            { 
                                cboTipoCliente.Items.Add(new ListItem("Sucursal", "0"));
                                cboTipoCliente.SelectedValue = "0";
                                cli_nombre.Text = "";
                                cli_direccion.Text = "";
                                activarSucursal();
                                string paisMatriz = cboPais.SelectedValue;
                                poblarPaisRol();
                                CboPaisMatriz.SelectedValue = paisMatriz;                            
                                PoblarCiudadMatriz();
                                //PoblarListaPais();
                                string idCiudadC = cboCiudad.SelectedValue;
                                cboCiudadMatriz.SelectedValue = idCiudadC;
                            
                                cboCiudad.Items.Clear();

                                string a = cboCiudadMatriz.SelectedValue.ToString();
                                PoblarClienteMatriz();
                                cboClienteMatriz.SelectedValue = cliente;
                                CboPaisMatriz.Enabled = false;
                                cboCiudadMatriz.Enabled = false;
                                cboClienteMatriz.Enabled = false;
                                btnGuardar.Text = "Guardar";
                                cboTipoCliente.Enabled = false;
                                ImageButton1.Visible = true;
                            }
                    
                          }
                    }
                
                else
                {   poblarPaisRol();
                    this.PoblarPlantaIni();
                    List<ReportParameter> parametro = new List<ReportParameter>();
                    //250618
                    cbo_Fuente.Items.Clear();
                    cbo_Origen.Items.Clear();
                    contobra.LlenarComboOrigen(cbo_Origen);
                    contobra.LlenarComboFuente(cbo_Fuente, Convert.ToInt32(cbo_Origen.SelectedItem.Value));  
                    //-------
                    parametro.Add(new ReportParameter("idrepresentante", rcID, true));
                    parametro.Add(new ReportParameter("rol", rol.ToString(), true));
                    parametro.Add(new ReportParameter("pais", pais, true));
                    //this.CargarReporteCliente("Contacto");                    
                }

              } // fin postb


            } // fin de validacion session            
            else
            {
                string mensaje = "Sesion Expiro. Por favor inicie sesión nuevamente. Gracias!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                Response.Redirect("Inicio.aspx");
            }                

        } // fin load

        private void PoblarTipoApoyo()
        {
            cboTipoApoyo.Items.Clear();
            reader = concli.PoblarTipoApoyo();
            cboTipoApoyo.Items.Add(new ListItem("Seleccione", "0"));
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboTipoApoyo.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            reader.Close();
            concli.CerrarConexion();
            cboTipoApoyo.SelectedValue = "13";
        }

        private void poblarPaisRol()
        {
            int rol = (int)Session["Rol"];
            if ((rol == 3) || (rol == 28) || (rol == 29))
            {
                this.PoblarListaPais();
            }
            else
            {
                this.PoblarListaPais2();
            }
        }

        private void PoblarTipoContribuyente()
        {
            cli_tco_id.Items.Clear();
            reader = concli.PoblarTipoContribuyente();
            if (reader.HasRows == true)
            {

                while (reader.Read())
                {
                    cli_tco_id.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }

            reader.Close();
            concli.CerrarConexion();
        }

        private void CargarGrillaCliente(int cliente)
        {
            dsCliente.Reset();
            dsCliente = concli.ConsultarTipoCliente(cliente);
            if (dsCliente != null)
            {
                grvTipoCliente.DataSource = dsCliente.Tables[0];
                grvTipoCliente.DataBind();
                grvTipoCliente.Visible = true;
            }
            else
            {
                grvTipoCliente.Dispose();
                grvTipoCliente.Visible = false;
            }
            dsCliente.Reset(); 
            concli.CerrarConexion();
        }

        private void PoblarTipoClientePlanta(int planta)
        {
            cboTipoClientePlanta.Items.Clear();
            cboTipoClientePlanta.Items.Add(new ListItem("Seleccione", "0"));
            reader = concli.PoblarTipoClientePlanta(planta);
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboTipoClientePlanta.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }

            reader.Close();
            concli.CerrarConexion();
        }

        private void PoblarPlantaIni()
        {            
            cboPlanta.Items.Clear();
            cboPlanta.Items.Add(new ListItem("Seleccione", "0"));            
            
            reader = concli.PoblarPlantaIni();
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboPlanta.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }

            reader.Close();
            concli.CerrarConexion();
        }

        private void PoblarPlanta(int cliente)
        {
            cboPlanta.Items.Clear();
            cboPlanta.Items.Add(new ListItem("Seleccione", "0"));
            reader = concli.PoblarPlanta(cliente);
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboPlanta.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }

            reader.Close();
            concli.CerrarConexion();
        }

        private void PoblarBusqClientes()
        {
            int rcID = Convert.ToInt32( Session["rcID"]);
            int rol = (int)Session["Rol"];
            int pais = Convert.ToInt32( Session["Pais"]);            
            string nombre = txtBusqueda.Text;
            int IdClienteUsuario =Convert.ToInt32( Session["IdClienteUsuario"]);

            cboBusqClientes1.Items.Clear();
            reader = concli.PoblarBusqCliente(rol,pais,rcID,nombre);
            cboBusqClientes1.Items.Add(new ListItem("Seleccione", "0"));
            if (reader.HasRows == true)
            {
               
                    while (reader.Read())
                    {
                        cboBusqClientes1.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                    }
                
            }

            reader.Close();
            concli.CerrarConexion();
        }

        private void PoblarListaPais()
        {
            string rcID = (string)Session["rcID"];

            CboPaisMatriz.Items.Clear();
            cboPais.Items.Clear();
            reader = contubi.poblarListaPaisRepresentante(Convert.ToInt32(rcID));
            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")
            {
                CboPaisMatriz.Items.Add(new ListItem("Seleccione El Pais","0"));
                cboPais.Items.Add("Seleccione El Pais");
            }
            if (idioma == "Ingles")
            {
                CboPaisMatriz.Items.Add("Select The Country");
                cboPais.Items.Add("Select The Country");
            }
            if (idioma == "Portugues")
            {
                CboPaisMatriz.Items.Add("Selecione O País");
                cboPais.Items.Add("Selecione O País");
            }

            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    CboPaisMatriz.Items.Add(new ListItem(reader.GetString(0), reader.GetInt32(1).ToString()));
                    cboPais.Items.Add(new ListItem(reader.GetString(0), reader.GetInt32(1).ToString()));
                }
            }
            else
            {
                string mensaje = "";
                if (idioma == "Español")
                {
                    mensaje = "Usted no posee paises asociados.";
                }
                if (idioma == "Ingles")
                {
                    mensaje = "You have no partner countries.";
                }
                if (idioma == "Portugues")
                {
                    mensaje = "Você não tem países parceiros.";
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);                
            }
            reader.Close();
            contubi.cerrarConexion();
        }

        private void PoblarListaPais2()
        {
            CboPaisMatriz.Items.Clear();
            cboPais.Items.Clear();

            reader = contubi.poblarListaPais();
            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")
            {
                CboPaisMatriz.Items.Add("Seleccione El Pais");
                cboPais.Items.Add("Seleccione El Pais");
            }
            if (idioma == "Ingles")
            {
                CboPaisMatriz.Items.Add("Select The Country");
                cboPais.Items.Add("Select The Country");
            }
            if (idioma == "Portugues")
            {
                CboPaisMatriz.Items.Add("Selecione O País");
                cboPais.Items.Add("Selecione O País");
            }

            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    CboPaisMatriz.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                    cboPais.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            else
            {
                string mensaje = "";
                if (idioma == "Español")
                {
                    mensaje = "Usted no posee paises asociados.";
                }
                if (idioma == "Ingles")
                {
                    mensaje = "You have no partner countries.";
                }
                if (idioma == "Portugues")
                {
                    mensaje = "Você não tem países parceiros.";
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);                
            }

            reader.Close();
            contubi.cerrarConexion();
        }

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

            dsCliente = concli.ConsultarIdiomaCliente();

            foreach (DataRow fila in dsCliente.Tables[0].Rows)
            {
                posicion = posicion + 1;
                if (posicion == 1)
                    lblTipoCliente.Text = fila[idiomaId].ToString();
                if (posicion == 2)
                    lblPaiCliMat.Text = fila[idiomaId].ToString();
                if (posicion == 3)
                    lblCiuCliMat.Text = fila[idiomaId].ToString();
                if (posicion == 4)
                    lblClienteMat.Text = fila[idiomaId].ToString();
                if (posicion == 5)
                    lblPais.Text = fila[idiomaId].ToString();
                if (posicion == 6)
                    lblCiudad.Text = fila[idiomaId].ToString();
                if (posicion == 7)
                    lblCliente.Text = fila[idiomaId].ToString();
                if (posicion == 8)
                    lblDireccion.Text = fila[idiomaId].ToString();
                if (posicion == 9)
                    lblTelef1.Text = fila[idiomaId].ToString();
                if (posicion == 10)
                    lblTelef2.Text = fila[idiomaId].ToString();
                if (posicion == 11)
                    lblNIT.Text = fila[idiomaId].ToString();
                if (posicion == 12)
                    lblTipoContri.Text = fila[idiomaId].ToString();                
                if (posicion == 17)
                    btnGuardar.Text = fila[idiomaId].ToString();
                if (posicion == 18)
                    btnNuevo.Text = fila[idiomaId].ToString();
                if (posicion == 19)
                    btnGuardar.OnClientClick = fila[idiomaId].ToString();                          
            }
            dsCliente.Tables.Remove("Table");
            dsCliente.Dispose();
            dsCliente.Clear(); 
            concli.CerrarConexion();
        }    

       protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string Representante = (string)Session["Nombre_Usuario"];
            string idioma = (string)Session["Idioma"];
            string mensajealerta ="Seleccione los datos obligatorios (*)";
            int infra= 0, vivienda= 0, reclamo = 0, competencia = 0;

            if (chkVivienda.Checked == true) vivienda = 1 ; else vivienda = 0;
            if (chkInfra.Checked == true) infra = 1 ; else infra = 0;
            if (chkReclamo.Checked == true) reclamo = 1; else reclamo = 0;
            if (chk_EmpreCompe.Checked == true) competencia = 1; else competencia = 0;

            //Colocamos la información de los campos en mayuscula
            cli_nombre.Text = cli_nombre.Text.ToString().ToUpper();
            cli_direccion.Text = cli_direccion.Text.ToString().ToUpper();
            cli_telefono.Text = cli_telefono.Text.ToString().ToUpper();
            cli_telefono_2.Text = cli_telefono_2.Text.ToString().ToUpper();
            cli_fax.Text = cli_fax.Text.ToString().ToUpper();
            cli_mail.Text = cli_mail.Text.ToString().ToLowerInvariant();
            cli_web.Text = cli_web.Text.ToString().ToLowerInvariant();
            cli_nit.Text = cli_nit.Text.ToString().ToUpper();

           //250618
            int vValido = 1;
            if (cbo_Fuente.SelectedIndex == -1) { vValido = 0; }
            else if (cbo_Fuente.SelectedItem.Value.ToString() == "0") { vValido = 0; }


            if (vValido == 0)
            {
                mensajealerta = "Seleccione los datos obligatorios (*)";
            }
            else
            {
                if ((btnGuardar.Text == "Actualizar") || (btnGuardar.Text == "Update") || (btnGuardar.Text == "Atualizar"))
                {
                    string id = (string)Session["Cliente"];
                    int actualizar = concli.ActualizarCliente(Convert.ToInt32(id), cli_nombre.Text, cli_direccion.Text, cli_nit.Text,
                        cli_telefono.Text, cli_telefono_2.Text, cli_fax.Text, cli_mail.Text, cli_web.Text, Convert.ToInt32(cli_tco_id.SelectedItem.Value),
                        Representante, prefijo1.Text, prefijo2.Text, prefijo3.Text, Convert.ToInt32(cboTipoApoyo.SelectedItem.Value), 1, vivienda, infra, reclamo, txtReclamo.Text.ToString(),
                        Convert.ToInt32(cboPais.SelectedItem.Value), Convert.ToInt32(cboCiudad.SelectedItem.Value), competencia, Convert.ToInt32(cbo_Fuente.SelectedItem.Value));

                    this.MensajeCliente();
                    Session["idCliente"] = id.ToString();

                    this.activarLink();
                }
                //this.ReporteCliente();

                else
                {
                    if ((btnGuardar.Text == "Guardar") || (btnGuardar.Text == "Save") || (btnGuardar.Text == "Salvar"))
                    {
                        if (cboPais.SelectedItem.Value == "0" || cboCiudad.SelectedItem.Value == "0")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensajealerta + "')", true);
                        }
                        else
                        {
                            if (cboTipoCliente.SelectedItem.Text == "Matriz")
                            {
                                if (cli_nombre.Text == "" || cli_direccion.Text == "" || cli_telefono.Text == "")
                                {
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensajealerta + "')", true);
                                }
                                else
                                {
                                    int cliente = concli.Matriz(cli_nombre.Text, Convert.ToInt32(cboPais.SelectedItem.Value), Convert.ToInt32(cboCiudad.SelectedItem.Value),
                                    cli_direccion.Text, cli_telefono.Text, cli_telefono_2.Text, cli_fax.Text, cli_mail.Text, cli_web.Text, cli_nit.Text,
                                    Convert.ToInt32(cli_tco_id.SelectedItem.Value), Representante, prefijo1.Text, prefijo2.Text, prefijo3.Text, 0, Convert.ToInt32(cboTipoApoyo.SelectedItem.Value), vivienda, infra, reclamo, txtReclamo.Text.ToString(),
                                    competencia, Convert.ToInt32(cbo_Fuente.SelectedItem.Value));

                                    int actualizar = concli.ActualizarIDClienteMatriz(cliente);
                                    this.btnGuardar.Enabled = false;
                                    this.MensajeCliente();
                                    Session["idCliente"] = cliente.ToString();
                                    this.activarLink();
                                }
                            }
                            else
                            {
                                if (cboTipoCliente.SelectedItem.Text == "Sucursal")
                                {
                                    if (cboClienteMatriz.SelectedItem.Value == "0")
                                    {
                                        string mensaje = "";
                                        if (idioma == "Español")
                                        {
                                            mensaje = "Empresa matriz no seleccionada.";
                                        }

                                        if (idioma == "Ingles")
                                        {
                                            mensaje = "Company matrix unselected.";
                                        }

                                        if (idioma == "Portugues")
                                        {
                                            mensaje = "Companhia matriz não selecionado.";
                                        }
                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                                    }
                                    else
                                    {
                                        int cliente = concli.Sucursal(cli_nombre.Text, Convert.ToInt32(cboClienteMatriz.SelectedItem.Value), Convert.ToInt32(cboPais.SelectedItem.Value),
                                            Convert.ToInt32(cboCiudad.SelectedItem.Value), cli_direccion.Text, cli_telefono.Text, cli_telefono_2.Text, cli_fax.Text, cli_mail.Text, cli_web.Text,
                                            cli_nit.Text, Convert.ToInt32(cli_tco_id.SelectedItem.Value), Representante, prefijo1.Text, prefijo2.Text, prefijo3.Text, vivienda, infra, reclamo, txtReclamo.Text.ToString(), Convert.ToInt32(cbo_Fuente.SelectedItem.Value));

                                        //this.btnGuardar.Enabled = false;
                                        this.MensajeCliente();
                                        this.btnGuardar.Text = "Actualizar";                                     
                                        Session["idCliente"] = cliente.ToString();
                                        CargarGrillaCliente(cliente);

                                        this.activarLink();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void MensajeCliente()
        {
            string idioma = (string)Session["Idioma"];
            string mensaje = "";

            if ((btnGuardar.Text == "Guardar") || (btnGuardar.Text == "Save") || (btnGuardar.Text == "Salvar"))
            {
                if (idioma == "Español")
                {
                    mensaje = "Empresa creada satisfactoriamente.";
                }

                if (idioma == "Ingles")
                {
                    mensaje = "Company created successfully.";
                }

                if (idioma == "Portugues")
                {
                    mensaje = "Companhia criado com êxito.";
                }     
            }

            if ((btnGuardar.Text == "Actualizar") || (btnGuardar.Text == "Update") || (btnGuardar.Text == "Atualizar"))
            {
                if (idioma == "Español")
                {
                    mensaje = "Empresa actualizada satisfactoriamente.";
                }

                if (idioma == "Ingles")
                {
                    mensaje = "Company updated successfully.";
                }

                if (idioma == "Portugues")
                {
                    mensaje = "Companhia atualizado com êxito.";
                }
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true); 
        }

        protected void cboPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PoblarCiudad();
            this.Prefijo();
            //this.ReporteCliente();
        }

        private void Prefijo()
        {
            reader = concli.ObtenerPrefijo(Convert.ToInt32(cboPais.SelectedItem.Value));
            if (reader.HasRows == true)
            {
                if (reader.Read() == true)
                {
                    preTel.Text = reader.GetValue(1).ToString();
                    preTel2.Text = preTel.Text;
                    preTel3.Text = preTel.Text;
                }
                else
                {
                    preTel.Text = "0";
                    preTel2.Text = preTel.Text;
                    preTel3.Text = preTel.Text;
                }
            }
            reader.Close();
            concli.CerrarConexion();
        }

        //CARGAMOS EL COMBO DE CIUDAD INICIALMENTE POR IDIOMAS
        public void cargarComboCiudadIdiomas()
        {
            cboCiudad.Items.Clear();
            string idioma = (string)Session["Idioma"];

            if (idioma == "Español")
            {
                cboCiudad.Items.Add(new ListItem("Seleccione la Ciudad", "0"));
            }
            if (idioma == "Ingles")
            {
                cboCiudad.Items.Add(new ListItem("Select the City", "0"));
            }
            if (idioma == "Portugues")
            {
                cboCiudad.Items.Add(new ListItem("Selecione a Cidade", "0"));
            }
        }

       
        private void PoblarCiudad()
        {
            int rol = (int)Session["Rol"];
            string rcID = (string)Session["rcID"];
            string idioma = (string)Session["Idioma"];

            cboCiudad.Items.Clear();
            if (idioma == "Español")
            {
                cboCiudad.Items.Add("Seleccione La Ciudad");
            }
            if (idioma == "Ingles")
            {
                cboCiudad.Items.Add("Select The City");
            }
            if (idioma == "Portugues")
            {
                cboCiudad.Items.Add("Selecione A Cidade");
            }

            if ((rol == 3) && (Convert.ToInt32(cboPais.SelectedItem.Value) == 8))
            {
                reader = contubi.poblarCiudadesRepresentantesColombia(Convert.ToInt32(rcID));              

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
            }
            else
            {
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
            }
        }

        private void PoblarCiudadMatriz()
        {
            int rol = (int)Session["Rol"];
            string rcID = (string)Session["rcID"];
            string idioma = (string)Session["Idioma"];
                       
            cboCiudadMatriz.Items.Clear();
            if (idioma == "Español")
            {
                cboCiudadMatriz.Items.Add("Seleccione La Ciudad");
            }
            if (idioma == "Ingles")
            {
                cboCiudadMatriz.Items.Add("Select The City");
            }
            if (idioma == "Portugues")
            {
                cboCiudadMatriz.Items.Add("Selecione A Cidade");
            }

            if ((rol == 3) && (Convert.ToInt32(CboPaisMatriz.SelectedItem.Value) == 8))
            {
                reader = contubi.poblarCiudadesRepresentantesColombia(Convert.ToInt32(rcID));

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        cboCiudadMatriz.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                    }
                }
                reader.Close();
                reader.Dispose();
                contubi.cerrarConexion();
            }
            else
            {
                reader = contubi.poblarListaCiudades(Convert.ToInt32(CboPaisMatriz.SelectedItem.Value));
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        cboCiudadMatriz.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                    }
                }
                reader.Close();
                reader.Dispose();
                contubi.cerrarConexion();
            }
        }

        protected void CboPaisMatriz_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PoblarCiudadMatriz();
        }

        protected void cboCiudadMatriz_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PoblarClienteMatriz();
        }

        private void PoblarClienteMatriz()
        {
            string idioma = (string)Session["Idioma"];

            cboClienteMatriz.Items.Clear();
            reader = concli.ConsultarDatosClienteMatriz(Convert.ToInt32(cboCiudadMatriz.SelectedItem.Value));
            if (idioma == "Español")
            {
                cboClienteMatriz.Items.Add("Seleccione La Empresa Matriz");
            }
            if (idioma == "Ingles")
            {
                cboClienteMatriz.Items.Add("Select Company Matrix");
            }
            if (idioma == "Portugues")
            {
                cboClienteMatriz.Items.Add("Selecione Companhia Matriz");
            }
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboClienteMatriz.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
               
            }
            reader.Close();
            reader.Dispose();
            contubi.cerrarConexion();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cliente.aspx");
        }

        protected void cli_nombre_TextChanged(object sender, EventArgs e)
        {
            cli_nombre.BackColor = System.Drawing.Color.White;
        }

        protected void cli_direccion_TextChanged(object sender, EventArgs e)
        {
            cli_direccion.BackColor = System.Drawing.Color.White;
        }

        protected void cli_telefono_TextChanged(object sender, EventArgs e)
        {
            cli_telefono.BackColor = System.Drawing.Color.White;
        }

        protected void cboTipoCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipoCliente.SelectedItem.Text == "Sucursal")
            {
               CboPaisMatriz.Visible = true;
                cboCiudadMatriz.Visible = true;
                cboClienteMatriz.Visible = true;
                
                lblPaiCliMat.Visible = true;
                lblClienteMat.Visible = true;
                lblCiuCliMat.Visible = true;
            }
            else
            {
                CboPaisMatriz.Visible= false;
                cboCiudadMatriz.Visible = false;
                cboClienteMatriz.Visible = false;

                lblPaiCliMat.Visible = false;
                lblClienteMat.Visible = false;
                lblCiuCliMat.Visible = false;
            }
        }

        protected void activarSucursal()
        {
            if (cboTipoCliente.SelectedItem.Text == "Sucursal")
            {
                CboPaisMatriz.Visible = true;
                cboCiudadMatriz.Visible = true;
                cboClienteMatriz.Visible = true;

                lblPaiCliMat.Visible = true;
                lblClienteMat.Visible = true;
                lblCiuCliMat.Visible = true;
            }
            else
            {
                CboPaisMatriz.Visible = false;
                cboCiudadMatriz.Visible = false;
                cboClienteMatriz.Visible = false;

                lblPaiCliMat.Visible = false;
                lblClienteMat.Visible = false;
                lblCiuCliMat.Visible = false;
            }
        }

        private void PoblarCliente()
        {
            poblarPaisRol();
            int rol = (int)Session["Rol"];
            string cliente = (string)Session["Cliente"];
            this.LimpiarCliente();

            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")
                btnGuardar.Text = "Actualizar";
            if (idioma == "Ingles")
                btnGuardar.Text = "Update";
            if (idioma == "Portugues")
                btnGuardar.Text = "Actualizar";
           
            readerCliente = concli.ConsultarCliente(Convert.ToInt32(cliente));
            readerCliente.Read();

            bool vivienda = readerCliente.GetSqlBoolean(23).Value;
            bool infra = readerCliente.GetSqlBoolean(24).Value;
            bool reclamo = readerCliente.GetSqlBoolean(25).Value;
            string reclamo_comentario = readerCliente.GetString(26).ToString();
            bool competencia = readerCliente.GetSqlBoolean(27).Value;

            if (vivienda == true) chkVivienda.Checked = true; else chkVivienda.Checked = false;
            if (infra == true) chkInfra.Checked = true; else chkInfra.Checked = false;
            if (competencia == true) chk_EmpreCompe.Checked = true; else chk_EmpreCompe.Checked = false;
            if (reclamo == true)
            {
                chkReclamo.Checked = true;
                txtReclamo.Visible = true;
            }
            else
                chkReclamo.Checked = false;

            txtReclamo.Text = reclamo_comentario;

            cboTipoCliente.Items.Clear();
            bool tipo = readerCliente.GetSqlBoolean(0).Value;
            if (tipo == true)
            {
                cboTipoCliente.Items.Add(new ListItem("Matriz", "Matriz"));               
            }
            else
            {
                cboTipoCliente.Items.Add(new ListItem("Sucursal", "Sucursal"));
                this.CargarSucursal();
            }
            //cboPais.Items.Clear();
            //cboPais.Items.Add(new ListItem(readerCliente.GetString(11), readerCliente.GetInt32(10).ToString()));
            cboPais.SelectedValue = readerCliente.GetInt32(10).ToString();
            //cboCiudad.Items.Clear();
            cboCiudad.Items.Add(new ListItem(readerCliente.GetString(13), readerCliente.GetInt32(12).ToString()));
            cboCiudad.SelectedValue = readerCliente.GetInt32(12).ToString();

            cbo_Fuente.Items.Clear();
            cbo_Origen.Items.Clear();
            contobra.LlenarComboOrigen(cbo_Origen);
            cbo_Origen.Text = readerCliente.GetInt32(29).ToString();
            contobra.LlenarComboFuente(cbo_Fuente, Convert.ToInt32(cbo_Origen.SelectedItem.Value));
            cbo_Fuente.Text = readerCliente.GetInt32(28).ToString();            
            cli_nombre.Text = readerCliente.GetValue(2).ToString();
            lblClientep.Text = readerCliente.GetValue(2).ToString() + " /  " + readerCliente.GetValue(21).ToString() + " / ID:" + cliente; 
            cli_direccion.Text = readerCliente.GetValue(3).ToString();
            cli_nit.Text = readerCliente.GetValue(9).ToString();
            cli_telefono.Text = readerCliente.GetValue(4).ToString();
            cli_telefono_2.Text = readerCliente.GetValue(5).ToString();
            cli_fax.Text = readerCliente.GetValue(6).ToString();
            cli_tco_id.Items.Clear();
            cli_tco_id.Items.Add(new ListItem(readerCliente.GetString(15), readerCliente.GetInt32(14).ToString()));
            cli_tco_id.Items.Add(new ListItem("---", "---"));
            cli_mail.Text = readerCliente.GetValue(7).ToString();
            cli_web.Text = readerCliente.GetValue(8).ToString();
            txtUsuarioAct0.Text = readerCliente.GetValue(16).ToString();
            txtFechaAct0.Text = readerCliente.GetSqlDateTime(17).Value.ToString("dd/MM/yyyy");
            if (txtFechaAct0.Text == "01/01/1900") txtFechaAct0.Text = "";
            prefijo1.Text = readerCliente.GetValue(18).ToString();
            prefijo2.Text = readerCliente.GetValue(19).ToString();
            prefijo3.Text = readerCliente.GetValue(20).ToString();
            //txtEstado.Text = readerCliente.GetValue(21).ToString();
            cboTipoApoyo.SelectedValue = readerCliente.GetInt32(22).ToString();
            readerCliente.Close(); 
            readerCliente.Dispose();
            concli.CerrarConexion();
          
            reader = concli.PoblarTipoContribuyente();
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cli_tco_id.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            reader.Close();
            reader.Dispose();
            concli.CerrarConexion();

            reader = concli.PoblarTipoClienteGrilla(Convert.ToInt32(cliente));

            this.Prefijo();
            CargarGrillaCliente(Convert.ToInt32(cliente));

            if (rol == 3)
            {
                cboCiudad.Enabled = false;
                cboPais.Enabled = false;
            }
            else
            {
                cboCiudad.Enabled = true;
                cboPais.Enabled = true;
            }

            reader.Close();
            reader.Dispose();
            concli.CerrarConexion();
        }

        private void CargarSucursal()
        {
            string cliente = (string)Session["Cliente"];
            reader = concli.ConsultarClienteSucursal(Convert.ToInt32(cliente));
            if (reader.HasRows == true)
            {
                reader.Read();
                //CboPaisMatriz.Items.Clear();
                CboPaisMatriz.Items.Add(new ListItem(reader.GetString(2), reader.GetInt32(3).ToString()));
                CboPaisMatriz.SelectedValue = reader.GetInt32(3).ToString();

                //cboCiudadMatriz.Items.Clear();
                cboCiudadMatriz.Items.Add(new ListItem(reader.GetString(4), reader.GetInt32(5).ToString()));
                cboCiudadMatriz.SelectedValue = reader.GetInt32(5).ToString();

                cboClienteMatriz.Items.Clear();
                cboClienteMatriz.Items.Add(new ListItem(reader.GetString(0), reader.GetInt32(1).ToString()));
            }
            reader.Close();
            reader.Dispose();
            concli.CerrarConexion();
        }

        private void LimpiarCliente()
        {
            //this.Idioma();

            cli_nombre.Text = "";
            cli_direccion.Text = "";
            cli_nit.Text = "";
            cli_telefono.Text = "";
            cli_telefono_2.Text = "";
            cli_fax.Text = "";
            cli_mail.Text = "";
            cli_web.Text = "";
        }

        protected void lkCreaContacto_Click(object sender, EventArgs e)
        {
            string id = (string)Session["idCliente"];
            Session["Cliente"] = id;
            Response.Redirect("Contacto.aspx?idCliente="+id);

        }

        protected void cboBusqClientes1_TextChanged(object sender, EventArgs e)
        {
            if(cboBusqClientes1.Text != "")
            this.PoblarBusqClientes();
        }

        protected void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            if (txtBusqueda.Text != "")
            {
                this.PoblarBusqClientes();
                this.ReporteVerHV.Visible = false;
                cboBusqClientes1.Visible = true;
            }
            else
            {
                cboBusqClientes1.Visible = false;
            }
        }

        protected void cboBusqClientes1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = cboBusqClientes1.SelectedItem.Value;
            Session["Cliente"] = id;            
            Response.Redirect("Cliente.aspx?idCliente="+id);
            //LkverHV.Visible = true;            
        }

        public void CargarReporteCliente(string tipo)
        {
            int idcliente = Convert.ToInt32(Session["Cliente"]);

            List<ReportParameter> parametro = new List<ReportParameter>();

            parametro.Add(new ReportParameter("idcliente", idcliente.ToString(), true));

            //ReporteVerClientes.Width = 1320;
            //ReporteVerClientes.Height = 1000;
            this.ReporteVerHV.KeepSessionAlive = true;
            this.ReporteVerHV.AsyncRendering = true;
            ReporteVerHV.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            ReporteVerHV.ServerReport.ReportServerCredentials = irsc;

            ReporteVerHV.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            if (tipo == "HojaVida")
            {
                ReporteVerHV.ServerReport.ReportPath = "/InformesCRM/COM_HomeCliente";
                
            }
            else
            {
                ReporteVerHV.ServerReport.ReportPath = "/InformesCRM/COM_HomeCliente";
                //ReporteVerHV.ServerReport.ReportPath = "/InformesCRM/COM_ContactosCliente";
            }

            this.ReporteVerHV.ServerReport.SetParameters(parametro);                        
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


        protected void LkverHV_Click(object sender, EventArgs e)
        {
            //string id = (string) Session["idCliente"];
            //Session["Cliente"] = id;
            //Response.Redirect("VerHojaVida.aspx?idCliente=" + id);
            ReporteVerHV.Visible = true;
            this.CargarReporteCliente("HojaVida");
        }

        protected void lkCrearObra_Click(object sender, EventArgs e)
        {
            string id = (string)Session["idCliente"];
            Session["Cliente"] = id;
            Response.Redirect("Obra.aspx?idCliente=" + id);
        }

        protected void lkCreaProyecto_Click(object sender, EventArgs e)
        {
            string id = (string)Session["idCliente"];
            Session["Cliente"] = id;
            //Response.Redirect("FormFup.aspx");
            Response.Redirect("formfup.aspx?idCliente=" + id);
        }

        protected void lkCreaProyecto0_Click(object sender, EventArgs e)
        {
            string id = (string)Session["idCliente"];
            Session["Cliente"] = id;
            Response.Redirect("PedidoVenta.aspx?idCliente=" + id);
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            //Guarda el Log de actualizacion de la tabla obra
            string tabla = "Cliente";
            String fecha = DateTime.Now.ToShortDateString();
            string evento = "Anulacion";
            string usuarioMod = (string)Session["Usuario"];
            string NombreCampo = "cli_activo";
         

            string Representante = (string)Session["Nombre_Usuario"];
            string mensaje = "";
            string id = (string)Session["Cliente"];

            int infra = 0, vivienda = 0, reclamo = 0, competencia=0;

            if (chkVivienda.Checked == true) vivienda = 1; else vivienda = 0;
            if (chkInfra.Checked == true) infra = 1; else infra = 0;
            if (chkReclamo.Checked == true) reclamo = 1; else reclamo = 0;
            if (chk_EmpreCompe.Checked == true) competencia = 1; else competencia = 0;

            if (id != null)
            {                
                int actualizar = concli.ActualizarCliente(Convert.ToInt32(id), cli_nombre.Text, cli_direccion.Text, cli_nit.Text,
                    cli_telefono.Text, cli_telefono_2.Text, cli_fax.Text, cli_mail.Text, cli_web.Text, Convert.ToInt32(cli_tco_id.SelectedItem.Value),
                    Representante, prefijo1.Text, prefijo2.Text, prefijo3.Text, 0,0,vivienda,infra, reclamo, txtReclamo.ToString(),
                    Convert.ToInt32(cboPais.SelectedItem.Value), Convert.ToInt32(cboCiudad.SelectedItem.Value), competencia, Convert.ToInt32(cbo_Fuente.SelectedItem.Value));

                contobra.Met_Insertar_Log_AnulaObra(tabla, int.Parse(id), usuarioMod, fecha, evento);

                mensaje = "Cliente Anulado";                
            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true); 
                Session["idCliente"] = id.ToString();
                
            }
        

        protected void activarLink ()
        {
            pnlRegistrar.Visible = true;
            //LkverHV.Visible = true; 

            int rol = (int)Session["Rol"];

            if ((rol == 1) || (rol == 2) || (rol == 5) ||  (rol == 9) || (rol == 3)  || (rol == 30) || (rol == 31) || (rol == 33) || (rol == 34) || (rol == 39) || (rol == 40) || (rol == 41) || (rol == 38))
            {
                lkCreaContacto.Visible = false;
                lkCrearObra.Visible = false;                
                lkCreaProyecto.Visible = false;
                lkCreaPV.Visible = false;
                lkCreaVisita.Visible = false;
            }
            else
            {
                if (rol == 46)
                {
                    lkCreaContacto.Visible = false; 
                }
 
            }
            
        }

        protected void btnActualizarTipo_Click(object sender, EventArgs e)
        {
            string mensaje = "";
            int resultado = -1;
            int cliente = Convert.ToInt32( Session["idCliente"]);
            int idClientePlanta = Convert.ToInt32(Session["IdClientePlanta"]);

            if (cboPlanta.SelectedItem.Value == "0" || cboTipoClientePlanta.SelectedItem.Value == "0")
            {
                mensaje = "Seleccione la Planta y el Tipo de Cliente !!";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
            else
            {
                if (btnActualizarTipo.Text == "Guardar")
                {
                    resultado = concli.InsertarTipoCliente(cliente, Convert.ToInt32(cboTipoClientePlanta.SelectedItem.Value));
                    CargarGrillaCliente(cliente);
                    btnActualizarTipo.Text = "Actualizar";
                }
                else
                {
                    if (btnActualizarTipo.Text == "Actualizar")
                    {
                        resultado = concli.ActualizarTipoCliente(Convert.ToInt32(cboTipoClientePlanta.SelectedItem.Value), idClientePlanta);
                        CargarGrillaCliente(cliente);
                    }
                }

                if (resultado != -1)
                {
                    mensaje = "Tipo de Cliente Registrado Exitosamente !!";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
                else
                {
                    mensaje = "ERROR!! No se Registro";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
            }
        }

       
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int fila = grvTipoCliente.SelectedRow.RowIndex;
            btnActualizarTipo.Enabled = true;
            String idClientePlanta = grvTipoCliente.SelectedRow.Cells[1].Text;
            //llenarList(controlEvento.cargarPartiEvento(" AND (part.part_tifuente_id = " + idEvento + ")"), listPart, 1, 0);
            readerevento = concli.ConsultarTipoClientePlanta(Convert.ToInt32(idClientePlanta));
            if (readerevento.HasRows == true)
            {
                readerevento.Read();
                cboPlanta.Items.Clear();
                cboPlanta.Items.Add(new ListItem(readerevento.GetString(2), readerevento.GetInt32(0).ToString()));
                
                PoblarTipoClientePlanta(Convert.ToInt32(cboPlanta.SelectedItem.Value));
               
                cboTipoClientePlanta.SelectedValue = readerevento.GetInt32(1).ToString();

                Session["IdClientePlanta"] = idClientePlanta;
                btnActualizarTipo.Text = "Actualizar";

                readerevento.Close();
                readerevento.Dispose();
                concli.CerrarConexion();
            }

        }

        protected void cboPlanta_SelectedIndexChanged(object sender, EventArgs e)
        {
            PoblarTipoClientePlanta(Convert.ToInt32(cboPlanta.SelectedItem.Value));
        }

        public void activarCamposAsistente()
        {
            lblPaiCliMat1.Visible = true;
            cboPlanta.Visible = true;
            lblPaiCliMat0.Visible = true;
            cboTipoClientePlanta.Visible = true;
            btnActualizarTipo.Visible = true;
            grvTipoCliente.Visible = true;
            cbo_Fuente.Visible = true;
            cbo_Origen.Visible = true;
            lblFuente.Visible = true;
            lblOrigen.Visible = true;
        }

        protected void chkReclamo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReclamo.Checked == true)
                txtReclamo.Visible = true;
            else
            {
                txtReclamo.Visible = false;
                txtReclamo.Text = "";
            }
        }

        protected void lkCreaVisita_Click(object sender, EventArgs e)
        {
            Session["ClienteNombre"] = cli_nombre.Text;
            Response.Redirect("VisAgendamiento.aspx?Modo=Eje");
        }

        protected void lkVerPanel_Click(object sender, EventArgs e)
        {
            if (pnlTipoCli.Visible == false)
            {
                pnlTipoCli.Visible = true;
            }
            else
            {
                pnlTipoCli.Visible = false;
            }
            
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            string clienteObr = (string)Session["Obra"];
            Response.Redirect("Obra.aspx?idObra="+clienteObr);
        }

        //250618
        protected void cbo_Origen_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbo_Fuente.Items.Clear();
            contobra.LlenarComboFuente(cbo_Fuente, int.Parse(cbo_Origen.Text));
        }

        
    }
}