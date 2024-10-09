using CapaControl;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace SIO
{
    public partial class CostoItemPv : System.Web.UI.Page
    {
        public ControlCostos conCostos = new ControlCostos();
        public SqlDataReader reader = null;
        public ControlUbicacion contubi = new ControlUbicacion();
        private DataSet dsCostos = new DataSet();
        public FUP fup_clase = new FUP();

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Pagina"] = "Cargue Archivo Costo Items";
            ScriptManager scripManager = ScriptManager.GetCurrent(this.Page);
            scripManager.RegisterPostBackControl(btnValidador1);
            scripManager.RegisterPostBackControl(btnConfirmaCosto);

            if (!IsPostBack)
            {
                PoblarCombos();
                FileUpload31.Enabled = false;
                //eliminarMemoriaCache();

                //if (Session["MensajeCliente"] == null)
                //    lblMensaje.Text = "";
                //else
                //{
                //    lblMensaje.Text = Session["MensajeCliente"].ToString();
                //    cargarReporteLog("LogClienteLite");
                //}

                //if (Session["MensajeObra"] == null)
                //    lblObras.Text = "";
                //else
                //{
                //    lblObras.Text = Session["MensajeObra"].ToString();
                //    cargarReporteLog("LogObraLite");
                //}

                //if (Session["MensajeContacto"] == null)
                //    lblContacto.Text = "";
                //else
                //{
                //    lblContacto.Text = Session["MensajeContacto"].ToString();
                //    cargarReporteLog("logContactoLite");
                //}

                //if (Session["MensajeValidadorCliente"] != null)
                //{
                //    cargarReporteLog("LogValidadorCliente");
                //}

                //Session["MensajeCliente"] = null;
                //Session["MensajeObra"] = null;
                //Session["MensajeContacto"] = null;
                //Session["MensajeValidadorCliente"] = null;
            }
        }

        private void PoblarCombos()
        {
            poblarPlanta();
            poblarAnio();
            poblarTrimestre();
        }

        //Cargar combo planta
        private void poblarPlanta()
        {
            string usuario = (string)Session["Usuario"];
            cboPlanta.Items.Clear();

            cboPlanta.Items.Add(new ListItem("Seleccione la planta", "0"));
            reader = conCostos.poblarPlanta();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cboPlanta.Items.Add(new ListItem(reader.GetString(0), reader.GetInt32(1).ToString()));
                }
            }
            reader.Close();
            reader.Dispose();
            contubi.cerrarConexion();
        }

        private void poblarAnio()
        {
            cboAnio.Items.Clear();
            DateTime fechaActual = DateTime.Today;     
            cboAnio.Items.Add(new ListItem("Seleccione", "0"));
            cboAnio.Items.Add(new ListItem(fechaActual.Year.ToString(), fechaActual.Year.ToString()));
            cboAnio.Items.Add(new ListItem((fechaActual.Year + 1).ToString(), (fechaActual.Year + 1).ToString()));
                        
        }

        private void poblarTrimestre()
        {
            cboTrimestre.Items.Clear();
            cboTrimestre.Items.Add(new ListItem("Seleccione", "0"));
            cboTrimestre.Items.Add(new ListItem("1", "1"));
            cboTrimestre.Items.Add(new ListItem("2", "2"));
            cboTrimestre.Items.Add(new ListItem("3", "3"));
            cboTrimestre.Items.Add(new ListItem("4", "4"));

        }


        protected void btnValidador_Click1(object sender, EventArgs e)
        {
            
            DateTime fechaActual = DateTime.Now;            
            int year = fechaActual.Year;            
            int month = fechaActual.Month;
            int day = fechaActual.Day;
            int hour = fechaActual.Hour;
            int minute = fechaActual.Minute;
            int second = fechaActual.Second;
            int millisecond = fechaActual.Millisecond;
            string usuario = (string)Session["Usuario"];

            string actual = year.ToString() + month.ToString() + "_" + day.ToString() + "_" + hour.ToString()  + minute.ToString();
           
            if (!FileUpload31.HasFile || cboPlanta.SelectedItem.Value == "0" || cboAnio.SelectedItem.Value == "0" || cboTrimestre.SelectedItem.Value == "0")
            {
                string mensaje = "Seleccione todas las Opciones. Gracias!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);                        
            }
            else
            { 
                if (FileUpload31.HasFile)
                {
                    try
                    { 
                        //actualizarEstadoLogs("LogValidadorCliente");
                        string filename = Path.GetFileName(FileUpload31.FileName);
                        //string directorio = @"I:\VisitasTemp\";
                        //string directorio = @"C:\VisitasTemp\";
                        //string directorio = Server.MapPath(string.Format("~/Imagenes/VisitasTemp/"));
                        //if (!Directory.Exists(directorio)) { Directory.CreateDirectory(directorio); }
                        //string directorio = @"I:\Doc_Despachos\" + lbl_Desc_Id.Text + @"\";
                        //string dirweb = @"~/Doc_Despachos/" + lbl_Desc_Id.Text + @"/";

                        String rutaAplicacion = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);//Toma la ruta del aplicativo
                        rutaAplicacion = rutaAplicacion.Replace("\\\\", "\\");
                        String dlDir = @"/ArchivosICS\CostoItems\";
                        //String dlDir = @"~\ArchivosICS\CostoItems";
                        String directorio = "";
                        
                        directorio = rutaAplicacion + dlDir;                        

                        //directorio = rutaAplicacion + dlDir;
                        //directorio =  dlDir;
                        if (!Directory.Exists(directorio)) { Directory.CreateDirectory(directorio); }                        

                        HttpPostedFile postedFile = FileUpload31.PostedFile;
                        string NombreGuardar = actual +"_"+ filename;
                        postedFile.SaveAs(directorio + NombreGuardar);

                        string path = directorio + NombreGuardar;
                        string sql = "SELECT * FROM [Costos1$]";
                        string excelConnection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0;Persist Security Info=False";


                        //eliminar costos del trimestre seleccionado
                        conCostos.EliminarCostoTrimestre(Convert.ToInt32(cboPlanta.SelectedItem.Value), Convert.ToInt32(cboAnio.SelectedItem.Value), Convert.ToInt32(cboTrimestre.SelectedItem.Value), usuario);
                        
                        using (OleDbDataAdapter adaptor = new OleDbDataAdapter(sql, excelConnection))
                        {
                            DataSet ds = new DataSet();
                            adaptor.Fill(ds);
                            cargarArchivo(ds, filename);
                            adaptor.Dispose();
                        }

                        //Directory.Delete(directorio, true);
                        FileUpload31.Dispose();

                        // cargar reporte
                        cargargrilla();
                        


                    }
                    catch (Exception ex)
                    {
                        string mensaje = "verifique el nombre de la Hoja sea igual a Clientes. Gracias";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + ex.Message + ' ' + mensaje + "')", true);
                        //Response.Redirect("visitalite.aspx");
                    }
                }
                else
                {
                    string mensaje = "No tomo el archivo";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
            }
        
        }

        private void cargarArchivo(DataSet ds, string archivo)
        {
            bool valido = true;
            string msjError = "";

            string CodErp = "";
            string NombreItem = ""; 
            string Costo = "";
            string Observacion = "";

            string mensaje = ""; 
            int fila = 0;           
            int aciertos = 0;
            string filasError = "";            
            int error = 0;           

            string usuario = (string)Session["Usuario"];

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                valido = true;
                msjError = "";                
                
                fila = i + 2;

                try
                {
                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["CodErp"].ToString()))
                    {
                        CodErp = ds.Tables[0].Rows[i]["CodErp"].ToString().Trim();
                    }
                    else
                    {
                        valido = false;
                        msjError += "CodErp - ";
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["NombreItem"].ToString()))
                    {
                        NombreItem = ds.Tables[0].Rows[i]["NombreItem"].ToString().Trim();
                    }
                    else
                    {
                        valido = false;
                        msjError += "NombreItem - ";
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["Costo"].ToString()))
                    {
                        Costo = ds.Tables[0].Rows[i]["Costo"].ToString().Trim();
                    }
                    else
                    {
                        valido = false;
                        msjError += "Costo - ";
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["Observacion"].ToString()))
                    {
                        Observacion = ds.Tables[0].Rows[i]["Observacion"].ToString().Trim();
                    }
                    else
                    {
                        Observacion = "";
                    }

                    
                    if (valido)
                    {
                        int anio = Convert.ToInt32(cboAnio.SelectedValue.ToString());
                        int trimestre = Convert.ToInt32(cboTrimestre.SelectedValue.ToString());
                        int plantaId = Convert.ToInt32(cboPlanta.SelectedValue.ToString());

                        int costov = conCostos.insertarCostoItems(Convert.ToInt32(CodErp),NombreItem, Costo, Observacion,anio,trimestre,plantaId,usuario);

                        if (costov == 0)
                        {
                            aciertos++;
                        }
                        else
                        {
                            error++;
                            filasError += fila + " - ";
                        }
                    }
                    else
                    {
                        string observacion = "Error, campos obligatorios: " + msjError.Substring(0, msjError.Length - 3);
                        //concli.insertarLogValidadorCliente(cli_nombre, archivo, fila, cli_pai_id, cli_ciu_id, id_cli_sim, observacion);
                        filasError += fila + " - ";
                        error++;
                    }
                }
                catch (Exception exe)
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + exe.Message + "')", true);
                    string observacion = "Error, registro no insertado." + exe.Message.Replace('\'', ' ');
                    //concli.insertarLogValidadorCliente(cli_nombre, archivo, fila, cli_pai_id, cli_ciu_id, id_cli_sim, observacion);
                    filasError += fila + " - ";
                    error++;
                }
            }

            if (!String.IsNullOrEmpty(filasError))
                filasError = filasError.Substring(0, filasError.Length - 3);

            mensaje = "Registros ingresados: " + aciertos;
            Session["MensajeValidador"] = mensaje;
        }

        protected void cargargrilla()
        {
            int Planta = Convert.ToInt32(cboPlanta.SelectedItem.Value);
            int Anio = Convert.ToInt32(cboAnio.SelectedItem.Value);
            int Trimestre = Convert.ToInt32(cboTrimestre.SelectedItem.Value);

            dsCostos = null; 

            if (Planta != 0 && Anio != 0 && Trimestre != 0)
            {
                dsCostos = conCostos.ConsultarCostosTrimestre(Planta,Anio,Trimestre);
            }
            
            if (dsCostos != null)
            {
                GridView1.DataSource = dsCostos.Tables[0];
                GridView1.DataBind();
                GridView1.Visible = true;
            }
            else
            {
                GridView1.Dispose();
                GridView1.Visible = false;
            }
            //dsCostos.Reset();

        }

        protected void cboTrimestre_SelectedIndexChanged(object sender, EventArgs e)
        {
            consultarConfirmaCosto();
            cargargrilla();            
        }

        //Consulta confirmacion de costo del trimestre
        private void consultarConfirmaCosto()
        {
            bool confirmaCosto = false;    
                   
            reader = conCostos.ConsultarConfirmaCosto(Convert.ToInt32(cboPlanta.SelectedItem.Value), Convert.ToInt32(cboAnio.SelectedItem.Value), Convert.ToInt32(cboTrimestre.SelectedItem.Value));
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    confirmaCosto= reader.GetBoolean(0);
                }
            }
            reader.Close();
            reader.Dispose();
            contubi.cerrarConexion();

            if (confirmaCosto == true)
            {
                btnConfirmaCosto.Visible = false;
                btnValidador1.Visible = false;                
                FileUpload31.Enabled = false;
                lblEstado.Visible = true;
                lblEstado.Text = "Trimestre Confirmado";
            }
            else
            {
                btnConfirmaCosto.Visible = true;
                btnValidador1.Visible = true;
                lblEstado.Visible = false;
                FileUpload31.Enabled = true;
            }
        }

        protected void btnConfirmaCosto_Click(object sender, EventArgs e)
        {
            string usuario = (string)Session["Usuario"];
            string mensaje = "";

            if (cboPlanta.SelectedItem.Value == "0" || cboAnio.SelectedItem.Value == "0" || cboTrimestre.SelectedItem.Value == "0")
            {
                mensaje = "Seleccione todas las Opciones para confirmar el Costo. Gracias!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
            else
            {               
                String menId = conCostos.guardarConfirmaCosto(Convert.ToInt32(cboPlanta.SelectedItem.Value), Convert.ToInt32(cboAnio.SelectedItem.Value), Convert.ToInt32(cboTrimestre.SelectedItem.Value), usuario);
                if (menId.Substring(0, 1) != "E")//E <- de ERROR
                {
                    mensaje = "Guardado exitosamente!!";
                    btnConfirmaCosto.Visible = false;
                    Session["Evento"] = 71;
                    EnviarCorreoListaConfirmaCostos();
                    //cargargrilla();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    cboTrimestre_SelectedIndexChanged( sender,  e);
                    
                }
                else
                {
                    mensaje = "Hubo un error al guardar";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
            }
        }

        private void EnviarCorreoListaConfirmaCostos()
        {
            string mensaje;
            string Asunto = "SIO – CONFIRMACION CARGUE COSTOS DE ITEMS - PLANTA: "+cboPlanta.SelectedItem.ToString()+ " AÑO: " + cboAnio.SelectedItem.Value.ToString() + " TRIMESTRE: " + cboTrimestre.SelectedItem.Value.ToString();
            string usuario = (string)Session["Nombre_Usuario"];
            string Email = "",  Email2 = "", Cuerpo = "";
            string CorreoCostos = "costos@forsa.net.co";

            //Byte[] correo = new Byte[0];
            //WebClient clienteWeb = new WebClient();
            //clienteWeb.Dispose();
            //clienteWeb.Credentials = new System.Net.NetworkCredential("forsa", "F0rUs3s4*", "FORSA");
            //correo = clienteWeb.DownloadData(@"http://10.75.131.2:81/ReportServer?/InformesFUP/Com_ListaChequeo&rs:format=PDF&rs:command=render&rs:ClearSession=true&pNumOrden=" + txtOF.Text.Trim() + "");
            //MemoryStream ms = new MemoryStream(correo);
            string correoSistema = (string)Session["CorreoSistema"];
            string UsuarioAsunto = (string)Session["UsuarioAsunto"];
            string correoUsu = Session["rcEmail"].ToString() ;


            //OBTENGO CORREOS DE CALIDAD
            reader = conCostos.ConsultaCorreoConfirmaCostos();
            if (reader != null)
            {
                while (reader.Read())
                {
                    if (Email == "")
                    {
                        Email = reader.GetSqlString(0).ToString();
                        Session["EmailLista"] = Email;
                    }
                    else
                    {
                        Email = Email + "," + reader.GetSqlString(0).ToString();
                        Session["EmailLista"] = Email;
                    }
                }
                reader.Close();
            }            
            
            string EmailLista = (string)Session["EmailLista"];
            //DEFINIMOS LA CLASE DE MAILMESSAGE
            MailMessage mail = new MailMessage();
            //INDICAMOS EL EMAIL DE ORIGEN
            mail.From = new MailAddress(correoSistema);
            //AÑADIMOS LA DIRRECCIÓN DE CORREO DESTINATARIO            
            mail.To.Add(CorreoCostos+',' +EmailLista + ',' + correoUsu);
            
            //AÑADIMOS COPIA AL REPRESENTANTE
            mail.CC.Add(EmailLista);
            //INCLUIMOS EL ASUNTO DEL MENSAJE
            mail.Subject = Asunto;
            //AÑADIMOS EL CUERPO DEL MENSAJE
            mail.Body = Cuerpo + "\n\n" +
            "El Usuario: " +UsuarioAsunto  + " ha confirmado el cargue de costos de items del Año: " 
            + cboAnio.SelectedItem.Value.ToString() + " Trimestre: " + cboTrimestre.SelectedItem.Value.ToString() + "\n\n"+
            "Sitio Web. http://app.forsa.com.co" + "\n\n" + "SIO." + "\n\n" + correoUsu + "\n\n" +
            "Sistema Información Operacional." + "\n\n" + "Gestión Informática." + "\n\n" + "FORSA S.A.";  
            //INDICAMOS EL TIPO DE CODIFICACIÓN DEL MENSAJE
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            //DEFINIMOS LA PRIORIDAD DEL MENSAJE
            mail.Priority = System.Net.Mail.MailPriority.Normal;
            //INDICAMOS SI EL CUERPO DEL MENSAJE ES HTML O NO
            mail.IsBodyHtml = false;
            
            //DECLARAMOS LA CLASE SMTPCLIENT
            SmtpClient smtp = new SmtpClient();
            //DEFINIMOS NUESTRO SERVIDOR SMTP
            smtp.Host = "smtp.office365.com";
            //INDICAMOS LA AUTENTICACION DE NUESTRO SERVIDOR SMTP
            smtp.Credentials = new System.Net.NetworkCredential("informes@forsa.net.co", "Those7953");
            smtp.Port = 587;
            smtp.EnableSsl = true;

            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain,
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
                mensaje = "ERROR: " + ex.Message;
            }

        }


        protected void cboPlanta_SelectedIndexChanged(object sender, EventArgs e)
        {
            poblarAnio();
            poblarTrimestre();
        }

        protected void cboAnio_SelectedIndexChanged(object sender, EventArgs e)
        {
            poblarTrimestre();
        }
    }
}