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
using Microsoft.Reporting.WebForms;
using CapaControl.Entity;
using System.Linq;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;
using System.Globalization;
using System.Threading;


namespace SIO
{
    public partial class PlaneadorCotizaciones : System.Web.UI.Page
    {
        public SqlDataReader reader = null;
        public ControlPlaneadorCotizacion cpc = new ControlPlaneadorCotizacion();
        public FUP fup_clase = new FUP();
        private DataSet dsPlan = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPersonalSTV();
                CargarPersonalCotizacion();
                CargarReporteCotizaciones();
                CargarProceso();

                if (Session["Rol"] != null)
                {
                    int rol = (int)Session["Rol"];

                if (rol == 26 || rol ==  30)
                {
                    pnlProgramador.Visible = true;
                }
                
            }
            else
            {
                string mensaje = "Sesion Expiro. Por favor inicie sesión nuevamente. Gracias!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                Response.Redirect("Inicio.aspx");
            }
        }
        }

        public void CargarReporteCotizaciones()
        {
            this.ReportViewer1.KeepSessionAlive = true;
            this.ReportViewer1.AsyncRendering = true;
            ReportViewer1.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            ReportViewer1.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            ReportViewer1.ServerReport.ReportPath = "/Comercial/COM_InformeCotizaciones";
            ReportViewer1.ServerReport.Refresh();            
        }

        public void CargarPersonalSTV()
        {
            cboSTV.Items.Clear();
            reader = cpc.ConsultarPersonalSTV();
            cboSTV.Items.Add("Seleccione");

            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboSTV.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            else
            {

            }

            reader.Close();
            cpc.CerrarConexion();
        }

        public void CargarPersonalCotizacion()
        {
            cboCotizador.Items.Clear();

            reader = cpc.ConsultarPersonalCotizacion();
            cboCotizador.Items.Add("Seleccione");

            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboCotizador.Items.Add(new ListItem(reader.GetString(1).ToUpper(), reader.GetInt32(0).ToString()));
                }
            }
            else
            {

            }
            reader.Close();
            cpc.CerrarConexion();
        }

        public void CargarProceso()
        {
            cboProceso.Items.Clear();

            reader = cpc.ConsultarProceso();
            cboProceso.Items.Add(new ListItem("Seleccione","0"));

            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboProceso.Items.Add(new ListItem(reader.GetString(1).ToUpper(), reader.GetInt32(0).ToString()));
                }
            }
            else
            {

            }
            reader.Close();
            cpc.CerrarConexion();
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

        protected void txtFUP_TextChanged(object sender, EventArgs e)
        {
            Session["Version"] = 0;
            //VALIDO SI ES NUMERICO
            txtFUP.Text = IsNumeric(txtFUP.Text);
            //LO CONVIERTO A ENTERO
            txtFUP.Text = txtFUP.Text.Replace(",", "");

            PoblarVersion();

            if ((int)Session["Version"] == 1)
                this.cboVer_SelectedIndexChanged(sender, e);
            else
            {
                string mensaje = "No Se Encuentra Version con Visto Bueno, Verifique!!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
                    
        }

        public string IsNumeric(string valor, bool msg = true)
        {

            decimal result;
            bool valido;

            valido = decimal.TryParse(valor, out result);
            if (valor == "" || valido == false)
            {
                if (msg == true)
                { MensajeNumerico(); }
                result = 0;
            }

            return result.ToString("#,###.##");
        }

        private void MensajeNumerico()
        {
            string mensaje;
            mensaje = "Digite un valor numerico correctamente.";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }

        private void PoblarVersion()
        {
            if (txtFUP.Text == "")
            {
                cboVer.Items.Clear();
                cboVer.Items.Add("A");
            }
            else
            {
                //CONSULTAMOS LA VERSION CON EL FUP
                cboVer.Items.Clear();
                reader = cpc.PoblarVersion(Convert.ToInt32(txtFUP.Text.Trim()));
                if (reader.HasRows == true)
                {
                    if (reader.Read() == false)
                    {
                        string mensaje = "No Se Encuentra Version con Visto Bueno, Verifique!!";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        Session["Version"] = 0;
                        //cboVer.Items.Add("A");
                        reader.Close();
                        cpc.CerrarConexion();
                    }
                    else
                    {
                        reader.Close();
                        cpc.CerrarConexion();
                        reader = cpc.PoblarVersion(Convert.ToInt32(txtFUP.Text.Trim()));
                        if (reader.HasRows == true)
                        {
                            while (reader.Read())
                            {
                                cboVer.Items.Add(new ListItem(reader.GetString(1)));
                                txtObservaciones.Text = reader.GetString(2).ToString();
                                Session["Version"] = 1;
                            }

                        }
                        reader.Close();
                        cpc.CerrarConexion(); ;
                    }

                }
                else
                {
                    string mensaje = "Fup Sin Visto Bueno";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string tipo = "";

            if (cboCotizador.SelectedItem.Text == "Seleccione" || cboCotizador.SelectedItem == null )
            {
                string mensaje = "Debe seleccionar el Cotizador";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
            else
            {
                if (txtFechaIni.Text == "")
                {
                    string mensaje = "Debe ingresar una fecha de inicio.";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
                else
                {
                    if (txtFechaProg.Text == "")
                    {
                        string mensaje = "Debe ingresar una fecha de Programacion.";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    }
                    else
                    {
                        if (btnGuardar.Text == "Reprogramar" && cboProceso.SelectedItem.Text == "Seleccione")
                        {
                            string mensaje = "Debe Seleccionar Proceso responsable de la reprogramacion.";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        }
                    
                        else
                        {
                            int ingPlan = cpc.IngPlanCot(Convert.ToInt32(txtFUP.Text), cboVer.SelectedItem.Text.Trim(),
                        cboSTV.SelectedItem.Text, cboCotizador.SelectedItem.Text, txtModulaciones.Text,
                        Convert.ToInt32(txtCantRec.Text), txtFechaProg.Text, txtObservaciones.Text, Convert.ToInt32(cboCotizador.SelectedItem.Value),
                        txtFechaIni.Text, Convert.ToInt32(cboProceso.SelectedItem.Value));

                            string mensaje = "El FUP " + txtFUP.Text.Trim() + " " + cboVer.SelectedItem.Text.Trim() + " fue planeado correctamente.";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                            if (btnGuardar.Text == "Guardar")
                            { 
                                tipo = "Programado";
                                Session["Evento"] = 98;
                                fup_clase.CorreoFUP(Convert.ToInt32(txtFUP.Text), cboVer.SelectedItem.Text, 98);
                            }

                            if (btnGuardar.Text == "Reprogramar")
                            {
                                tipo = "Re-Programado";
                                Session["Evento"] = 99;
                                fup_clase.CorreoFUP(Convert.ToInt32(txtFUP.Text), cboVer.SelectedItem.Text, 99);
                            }

                            //correocot(tipo);
                            ReportViewer1.ServerReport.Refresh();
                            CargarReporteCotizaciones();
                        }
                    }
                }
            }
        }
        protected void btnReprogramar_Click(object sender, EventArgs e)
        {
            int ingreso = cpc.ReprogramarCotizacion(Convert.ToInt32(txtFUP.Text), cboVer.SelectedItem.Text.Trim());
            string mensaje;
            mensaje = "El FUP " + txtFUP.Text.Trim() + " " + cboVer.SelectedItem.Text.Trim() + " fue reprogramado correctamente.";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }

        protected void btnDarBaja_Click(object sender, EventArgs e)
        {
            int actualizar = cpc.InactivarCotizacion(Convert.ToInt32(txtFUP.Text), cboVer.SelectedItem.Text.Trim(), txtObservaciones.Text);

            string mensaje = "El FUP " + txtFUP.Text.Trim() + " " + cboVer.SelectedItem.Text.Trim() + " fue inactivado correctamente.";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }

        [WebMethod(EnableSession = true)]
        // Envio Correo de Planeador
        private void correocot(String tipo)
        {
            int Evento = 0;

            if (tipo == "Programado") Evento = 98;
            if (tipo == "Re-Programado") Evento = 99;

            ControlFUP controlFup = new ControlFUP();
            string sfId = "0";
            string Nombre = (string)Session["Nombre_Usuario"];
            string CorreoUsuario = (string)Session["rcEmail"];
            int parte = 0;
            
            string correoSistema = (string) Session["CorreoSistema"];
            string UsuarioAsunto = (string) Session["UsuarioAsunto"];

            //int EncabezadoFUP = controlfup.EncabezadoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim());

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pFupID ", txtFUP.Text);
            parametros.Add("@pVersion", cboVer.SelectedItem.ToString());
            parametros.Add("@pEvento", Evento);
            parametros.Add("@pUsuario", UsuarioAsunto);
            parametros.Add("@pRemitente", CorreoUsuario);
            parametros.Add("@pParte", parte);

            List<NotificaFup> data = ControlDatos.EjecutarStoreProcedureConParametros<NotificaFup>("USP_fup_notificacionesN", parametros);

            //VALORES DEL ENCABEZADO 
            string AsuntoMail = Convert.ToString(data.FirstOrDefault().AsuntoMail);
            string DestinatariosMail = Convert.ToString(data.FirstOrDefault().Lista);
            string MensajeMail = Convert.ToString(data.FirstOrDefault().Msg);
            bool llevaAnexo = Convert.ToBoolean(data.FirstOrDefault().Anexo);
            string EnlaceAnexo = Convert.ToString(data.FirstOrDefault().LinkAnexo);


            string cotizador = cboCotizador.SelectedItem.Text;

            //WebClient clienteWeb = new WebClient();
            //clienteWeb.Credentials = new System.Net.NetworkCredential("forsa", "F0rUs3s4*", "FORSA");
            //Byte[] correo = clienteWeb.DownloadData(@"http://10.75.131.2:81/ReportServer?/Ingenieria/ING_PlaneadorIngenieria&rs:format=EXCEL&rs:command=render&rs:ClearSession=true&respcot=" + cotizador + "");
            //MemoryStream ms = new MemoryStream(correo);
            string email = "";
            //CONSULTO EL E-MAIL DE EL COTIZADOR
            reader = cpc.consultarEmailCotizacion(Convert.ToInt32(cboCotizador.SelectedItem.Value));
            if (reader.HasRows == true)
            {
                reader.Read();
                email = reader.GetValue(3).ToString();
            }
            reader.Close();
            cpc.CerrarConexion(); 

            string correousu = (string)Session["rcEmail"];
            //string correoSistema = (string)Session["CorreoSistema"];
            //string UsuarioAsunto = (string)Session["UsuarioAsunto"];

            //mail para administradores
            string EmailAdmin = "";
            reader = cpc.ObtenerMailAdmin();
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    if (EmailAdmin == "")
                    {
                        EmailAdmin = reader.GetValue(0).ToString();
                    }
                    else
                    {
                        EmailAdmin = EmailAdmin + "," + reader.GetValue(0).ToString();
                    }
                }
            }
            reader.Close();
            cpc.CerrarConexion();

            //mail para administradores
            string EmailEspecialista = "";
            reader = cpc.ObtenerMailEspecialistas();
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    if (EmailEspecialista == "")
                    {
                        EmailEspecialista = reader.GetValue(0).ToString();
                    }
                    else
                    {
                        EmailEspecialista = EmailEspecialista + "," + reader.GetValue(0).ToString();
                    }
                }
            }
            reader.Close();
            cpc.CerrarConexion();

            string solucion, cuerpo;

            //ENVIO DE CORREO
            //Definimos la clase MailMessage
            MailMessage mail = new MailMessage();
            //Indicamos Email De Origen
            mail.From = new MailAddress(correoSistema);
            //Añadimos la direccion correo del  destinatario
            //mail.To.Add(email);
            mail.To.Add(DestinatariosMail+","+ email);           

            mail.Bcc.Add(EmailAdmin+","+ EmailEspecialista);
            mail.Subject = AsuntoMail;

            //mail.Subject = "SIO - Programacion Cotizacion Fup. "+ txtFUP.Text+ " Ver. " + cboVer.Text+" - " +" Asignado a: "+ cboCotizador.SelectedItem.Text +" - "+ UsuarioAsunto;
            //Añadimos el cuerpo del mensaje
            solucion = "Buen dia Estimado Comercial,";
            cuerpo = "\n\nSu solicitud " + txtFUP.Text + "-" + cboVer.Text + " fue "+tipo +" para Entrega por parte de SCI el día: " + txtFechaProg.Text;
                 
            string cuerpo2 = "\n\n\n Cordialmente. "+ "\n Soporte Comercial e Ingenieria. ";
            mail.Body = solucion + cuerpo + cuerpo2;
            //Indicamos el tipo tipo de codificación del mensaje
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            //Defiimos la prioridad del mensaje
            mail.Priority = System.Net.Mail.MailPriority.Normal;
            //Indicamos si el cuerpo del mensaje es HTML o no 
            mail.IsBodyHtml = false;
            //mail.Attachments.Add(new Attachment(ms, "SIO_PlaneadorIngenieria.xls"));
            //Declaramos la clase SmtpClient
            SmtpClient smtp = new SmtpClient();
            //DEFINIMOS NUESTRO SERVIDOR SMTP
            smtp.Host = "smtp.office365.com";
            //INDICAMOS LA AUTENTICACION DE NUESTRO SERVIDOR SMTP
            smtp.Credentials = new System.Net.NetworkCredential("informes@forsa.net.co", "Those7953");
            smtp.Port = 587;
            smtp.EnableSsl = true;

            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                //msjEntrada.Text = "ERROR: " + ex.Message;
            }
            reader.Close();
        }

        protected void btnGrilla_Click(object sender, EventArgs e)
        {

        }

        protected void btnVerReporte_Click(object sender, EventArgs e)
        {
            CargarReporteCotizaciones();
        }

        protected void cboVer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string estado = "";

            txtModulaciones.Text = "0";
            if (txtFUP.Text != "" && cboVer.SelectedItem.Text.Trim() != "")
            {
                reader = cpc.COnsultarModulaciones(Convert.ToInt32(txtFUP.Text), cboVer.SelectedItem.Text.Trim());

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        txtModulaciones.Text = reader.GetSqlInt32(0).ToString();
                        txtFechaIni.Text = reader.GetSqlString(1).ToString();
                        txtFechaProg.Text = reader.GetSqlString(2).ToString();
                        estado = reader.GetSqlString(3).ToString();
                        cboEstado.SelectedItem.Text = estado;
                        if (estado == "Programado")
                        {
                            btnGuardar.Text = "Reprogramar";
                            cboProceso.Visible = true;
                            lblProceso.Visible = true;
                        }
                        else
                        {
                            btnGuardar.Text = "Guardar";
                            cboProceso.Visible = false;
                            lblProceso.Visible = false;
                        }

                    }
                }
                reader.Close();
                cpc.CerrarConexion();

                ConsultarFechaPolitica();
            }
        }

        protected void ConsultarComparaPolitica()
        {
            if (txtFechaProg.Text != "" && lblfechapolitica.Text != ""  )
            { 
                TimeSpan Diff_dates = Convert.ToDateTime(lblfechapolitica.Text).Subtract(Convert.ToDateTime(txtFechaProg.Text));
                //Console.WriteLine("Difference in days: " + Diff_dates.Days);
                int DiasPoli = Convert.ToInt32(Diff_dates.Days);

                if (DiasPoli < 0)
                {
                    lblSuperaPolitica.Visible = true;
                    lblSuperaPolitica.Text = "Fecha Programada Supera Fecha Politica";
                    string mensaje = "La Fecha Programada es Mayor a la Fecha de Entrega Segun Politica Verifique!!";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
                else
                {
                    lblSuperaPolitica.Visible = false;
                    lblSuperaPolitica.Text = "";
                }

            }


        }

        public void ConsultarFechaPolitica()
        {
            lblfechapolitica.BackColor = Color.Transparent;
            lblCliente.BackColor = Color.Transparent;
            lblClasificacion.BackColor = Color.Transparent;

            if (txtFUP.Text != "" && cboVer.SelectedItem.Text.Trim() != "")
            {
                lblfechapolitica.Text = "";
                lblSuperaPolitica.Text = "";

                reader = cpc.ConsultarFechapolitica(Convert.ToInt32(txtFUP.Text), cboVer.SelectedItem.Text.Trim());

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        lblfechapolitica.Text = reader.GetString(0).ToString();                        
                        lblCliente.Text = reader.GetString(2).ToString();
                        lblClasificacion.Text = reader.GetString(1).ToString();
                        lblobra.Text = reader.GetString(3).ToString();

                        lblfechapolitica.BackColor = Color.Yellow;
                        lblCliente.BackColor = Color.Yellow;
                        lblClasificacion.BackColor = Color.Yellow;
                    }
                }
                reader.Close();
                cpc.CerrarConexion();
            }
        }

        protected void txtFechaProg_TextChanged(object sender, EventArgs e)
        {
            ConsultarComparaPolitica();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PlaneadorCotizaciones.aspx");
        }
    }
}