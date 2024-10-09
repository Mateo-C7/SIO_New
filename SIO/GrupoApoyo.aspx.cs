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
    public partial class GrupoApoyo : System.Web.UI.Page
    {
        public SqlDataReader reader = null;
        public SqlDataReader readerCliente = null;
        private DataSet dsCliente = new DataSet();
        public ControlUbicacion contubi = new ControlUbicacion();
        public ControlCliente concli = new ControlCliente();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                this.PoblarTipoContribuyente();
                this.PoblarTipoApoyo();

                string rcID = (string)Session["rcID"];
                int rol = (int)Session["Rol"];
                string pais = (string)Session["Pais"];

                if ((rol == 3) || (rol == 28) || (rol == 29))
                {
                    this.PoblarListaPais();
                }
                else
                {
                    this.PoblarListaPais2();
                }
                
                if (Request.QueryString["idCliente"] != null)
                {
                    string cliente = Request.QueryString["idCliente"];
                    Session["Cliente"] = cliente;
                    Session["idCliente"] = cliente;
                    this.PoblarCliente();
                    lkCreaContacto.Visible = true;                   
                    lblCrear.Visible = true;  
                    this.CargarReporteCliente();
                }
                else
                {
                    List<ReportParameter> parametro = new List<ReportParameter>();

                    parametro.Add(new ReportParameter("idrepresentante", rcID, true));
                    parametro.Add(new ReportParameter("rol", rol.ToString(), true));
                    parametro.Add(new ReportParameter("pais", pais, true));
                }
            }

        }

        public void CargarReporteCliente()
        {
            int idcliente = Convert.ToInt32(Session["Cliente"]);

            List<ReportParameter> parametro = new List<ReportParameter>();

            parametro.Add(new ReportParameter("idcliente", idcliente.ToString(), true));

            //ReporteVerClientes.Width = 1320;
            //ReporteVerClientes.Height = 1000;
            ReporteVerHV.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "forsa", "mail.forsa.com.co");
            ReporteVerHV.ServerReport.ReportServerCredentials = irsc;

            ReporteVerHV.ServerReport.ReportServerUrl = new Uri("http://si.forsa.com.co:81/ReportServer");
            ReporteVerHV.ServerReport.ReportPath = "/InformesCRM/COM_HomeApoyo";
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

        private void Prefijo()
        {
            reader = concli.ObtenerPrefijo(Convert.ToInt32(cboPais.SelectedItem.Value));

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

        private void PoblarCliente()
        {
            string cliente = (string)Session["Cliente"];
            this.LimpiarCliente();

            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")
                btnGuardar.Text = "Actualizar";
            if (idioma == "Ingles")
                btnGuardar.Text = "Update";
            if (idioma == "Portugues")
                btnGuardar.Text = "Atualizar";

            readerCliente = concli.ConsultarCliente(Convert.ToInt32(cliente));
            readerCliente.Read();
           
            cboPais.Items.Clear();
            cboPais.Items.Add(new ListItem(readerCliente.GetString(11), readerCliente.GetInt32(10).ToString()));
            cboCiudad.Items.Clear();
            cboCiudad.Items.Add(new ListItem(readerCliente.GetString(13), readerCliente.GetInt32(12).ToString()));
            cli_nombre.Text = readerCliente.GetValue(2).ToString();
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
            txtEstado.Text = readerCliente.GetValue(21).ToString();
            cboTipoApoyo.SelectedValue = readerCliente.GetValue(22).ToString();
            lblClientep.Text = readerCliente.GetValue(2).ToString();
            readerCliente.Close();

            reader = concli.PoblarTipoContribuyente();
            while (reader.Read())
            {
                cli_tco_id.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
            }
            reader.Close();

            this.Prefijo();
        }

        private void LimpiarCliente()
        {           
            cli_nombre.Text = "";
            cli_direccion.Text = "";
            cli_nit.Text = "";
            cli_telefono.Text = "";
            cli_telefono_2.Text = "";
            cli_fax.Text = "";
            cli_mail.Text = "";
            cli_web.Text = "";
        }


        private void PoblarListaPais2()
        {
            cboPais.Items.Clear();

            reader = contubi.poblarListaPais();
            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")
            {
                cboPais.Items.Add(new ListItem("Seleccione El Pais","0"));
            }
            if (idioma == "Ingles")
            {
                cboPais.Items.Add("Select The Country");
            }
            if (idioma == "Portugues")
            {
                cboPais.Items.Add("Selecione O País");
            }

            if (reader != null)
            {
                while (reader.Read())
                {
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


        private void PoblarListaPais()
        {
            string rcID = (string)Session["rcID"];

            cboPais.Items.Clear();
            reader = contubi.poblarListaPaisRepresentante(Convert.ToInt32(rcID));
            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")
            {
                cboPais.Items.Add("Seleccione El Pais");
            }
            if (idioma == "Ingles")
            {
                cboPais.Items.Add("Select The Country");
            }
            if (idioma == "Portugues")
            {
                cboPais.Items.Add("Selecione O País");
            }

            if (reader != null)
            {
                while (reader.Read())
                {
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

       
        
        private void PoblarTipoContribuyente()
        {
            cli_tco_id.Items.Clear();
            reader = concli.PoblarTipoContribuyente();

            while (reader.Read())
            {
                cli_tco_id.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
            }
            reader.Close();
            concli.CerrarConexion();
        }

        private void PoblarTipoApoyo()
        {
            cboTipoApoyo.Items.Clear();
            reader = concli.PoblarTipoApoyo();
            cboTipoApoyo.Items.Add(new ListItem("Seleccione", "0"));
            while (reader.Read())
            {
                cboTipoApoyo.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
            }
            reader.Close();
            concli.CerrarConexion();
        }

        protected void txtBusqueda_TextChanged(object sender, EventArgs e)
        {            
            if (txtBusqueda.Text != "")
            {
                this.PoblarBusqClientes();                
                this.ReporteVerHV.Visible = false;
            }
        
        }

        private void PoblarBusqClientes()
        {
            int rcID = Convert.ToInt32(Session["rcID"]);
            int rol = (int)Session["Rol"];
            int pais = Convert.ToInt32(Session["Pais"]);
            string nombre = txtBusqueda.Text;

            cboBusqClientes1.Items.Clear();
            reader = concli.PoblarBusqApoyo(rol, pais, rcID, nombre);
            cboBusqClientes1.Items.Add(new ListItem("Seleccione", "0"));

            while (reader.Read())
            {
                cboBusqClientes1.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
            }

            reader.Close();
            concli.CerrarConexion();
        }

        protected void cboBusqClientes1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = cboBusqClientes1.SelectedItem.Value;
            Session["Cliente"] = id;
            Response.Redirect("GrupoApoyo.aspx?idCliente=" + id);      
        }

        protected void cboPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PoblarCiudad();
            this.Prefijo();
            //this.ReporteCliente();
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
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string Representante = (string)Session["Nombre_Usuario"];
            string idioma = (string)Session["Idioma"];
            string mensajealerta = "Seleccione los datos obligatorios (*)";


            //Colocamos la información de los campos en mayuscula
            cli_nombre.Text = cli_nombre.Text.ToString().ToUpper();
            cli_direccion.Text = cli_direccion.Text.ToString().ToUpper();
            cli_telefono.Text = cli_telefono.Text.ToString().ToUpper();
            cli_telefono_2.Text = cli_telefono_2.Text.ToString().ToUpper();
            cli_fax.Text = cli_fax.Text.ToString().ToUpper();
            cli_mail.Text = cli_mail.Text.ToString().ToLowerInvariant();
            cli_web.Text = cli_web.Text.ToString().ToLowerInvariant();
            cli_nit.Text = cli_nit.Text.ToString().ToUpper();


            if ((btnGuardar.Text == "Guardar") || (btnGuardar.Text == "Save") || (btnGuardar.Text == "Salvar"))
            {
                if (cboPais.SelectedItem.Value == "0" || cboCiudad.SelectedItem.Value == "0")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensajealerta + "')", true);
                }
                else
                {
                    if (cli_nombre.Text == "" || cli_direccion.Text == "" || cli_telefono.Text == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensajealerta + "')", true);
                    }
                    else
                    {
                        int cliente = concli.Matriz(cli_nombre.Text, Convert.ToInt32(cboPais.SelectedItem.Value), Convert.ToInt32(cboCiudad.SelectedItem.Value),
                        cli_direccion.Text, cli_telefono.Text, cli_telefono_2.Text, cli_fax.Text, cli_mail.Text, cli_web.Text, cli_nit.Text,
                        Convert.ToInt32(cli_tco_id.SelectedItem.Value), Representante, prefijo1.Text, prefijo2.Text, prefijo3.Text,1,
                        Convert.ToInt32(cboTipoApoyo.SelectedItem.Value),1,0,0,"");

                        int actualizar = concli.ActualizarIDClienteMatriz(cliente);
                        this.btnGuardar.Enabled = false;
                        this.MensajeCliente();
                        Session["idCliente"] = cliente.ToString();
                        lkCreaContacto.Visible = true;
                        lblCrear.Visible = true;
                        lblClientep.Text = cli_nombre.Text;
                    }
                }
            }
            else
            {
                if ((btnGuardar.Text == "Actualizar") || (btnGuardar.Text == "Update") || (btnGuardar.Text == "Atualizar"))
                {
                    string id = (string)Session["Cliente"];
                    int actualizar = concli.ActualizarCliente(Convert.ToInt32(id), cli_nombre.Text, cli_direccion.Text, cli_nit.Text,
                        cli_telefono.Text, cli_telefono_2.Text, cli_fax.Text, cli_mail.Text, cli_web.Text, Convert.ToInt32(cli_tco_id.SelectedItem.Value),
                        Representante, prefijo1.Text, prefijo2.Text, prefijo3.Text, Convert.ToInt32(cboTipoApoyo.SelectedItem.Value), 1, 1, 0, 0, "");

                    this.MensajeCliente();
                    Session["idCliente"] = id.ToString();
                    lkCreaContacto.Visible = true;
                    lblCrear.Visible = true;
                    lblClientep.Text = cli_nombre.Text;
                }
                //this.ReporteCliente();
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

        protected void cli_nombre_TextChanged(object sender, EventArgs e)
        {
            cli_nombre.BackColor = System.Drawing.Color.White;
        }

        protected void cli_direccion_TextChanged(object sender, EventArgs e)
        {
            cli_nombre.BackColor = System.Drawing.Color.White;
        }

        protected void cli_telefono_TextChanged(object sender, EventArgs e)
        {
            cli_telefono.BackColor = System.Drawing.Color.White;
        }

        protected void lkCreaContacto_Click(object sender, EventArgs e)
        {
            string id = (string)Session["idCliente"];
            Session["Cliente"] = id;
            Response.Redirect("Contacto.aspx?idCliente=" + id);
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("GrupoApoyo.aspx");
        }
    }
}