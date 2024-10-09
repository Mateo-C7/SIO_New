using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Drawing;
using System.IO;
using CapaControl;
using CapaDatos;

namespace SIO
{
    public partial class ListaChequeo : System.Web.UI.Page
    {
        public SqlDataReader reader = null;
        public ControlFUP controlfup = new ControlFUP();
        public ControlListaChequeo cListaCheq = new ControlListaChequeo();
        private DataSet dsLista = new DataSet();
        int cuentafilas;

        //ARMAMOS LA LISTA DE CHEQUEO
        public List<string> dynamicTabIDs;
        public List<AjaxControlToolkit.TabPanel> ListadeTabs;

        public ListaChequeo()
        {
            //Load += Page_Load;
            dynamicTabIDs = new List<string>();
            ListadeTabs = new List<AjaxControlToolkit.TabPanel>();
            cuentafilas = 0;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int rol = (int)Session["Rol"];

                PoblarTecnicos();

                if (rol == 16)
                {
                    btnFinalizar.Visible = true;
                    btnAprobar.Visible = false;
                }

                if (rol == 18)
                {
                    btnAprobar.Visible = true;
                    btnFinalizar.Visible = false;
                }

                if ((rol == 18) || (rol == 16))
                {
                    btnGrabaenCliente.Visible = true;
                    btnGuardar.Visible = true;
                    lkSubirFotos.Visible = true;
                    btnReporte.Visible = true;
                }
                else
                {
                    btnFinalizar.Visible = false;
                    btnAprobar.Visible = false;
                    btnGrabaenCliente.Visible = false;
                    btnGuardar.Visible = false;
                    btnReporte.Visible = false;
                    lkSubirFotos.Visible = false;
                }

                if (Session["SubirFoto"] != null)
                {
                    string foto = (string)Session["SubirFoto"];
                    if (foto == "1")
                    {
                        string OF = (string)Session["OF"];
                        txtOF.Text = OF;
                        CargarLista();
                        Session["SubirFoto"] = "0";
                        Accordion1.SelectedIndex = 2;
                    }
                }
            }
        }
                
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["dynamicTabIDs"] != null)
            {
                //retrieving the tab IDs from session:
                dynamicTabIDs = (List<string>)Session["dynamicTabIDs"];
                if (Session["ListadeTabs"] != null)
                {
                    ListadeTabs = (List<AjaxControlToolkit.TabPanel>)Session["ListadeTabs"];
                }
                else
                {
                    ListadeTabs = new List<AjaxControlToolkit.TabPanel>();
                }

                for (int tb = 0; tb < tabcEncuesta.Tabs.Count; )
                    tabcEncuesta.Tabs.RemoveAt(tb);

                tabcEncuesta.Tabs.Clear();
                foreach (AjaxControlToolkit.TabPanel tabGuarda in ListadeTabs)
                {
                    AjaxControlToolkit.TabPanel tab = new AjaxControlToolkit.TabPanel();
                    tab = tabGuarda;
                    try
                    {
                        tabcEncuesta.Tabs.Add(tab);
                    }
                    catch
                    {
                        string f = "Algo";
                    }
                }
            }
            else
            {
                dynamicTabIDs = new List<string>();
                ListadeTabs = new List<AjaxControlToolkit.TabPanel>();
            }

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            Session["dynamicTabIDs"] = dynamicTabIDs;
            Session["ListadeTabs"] = ListadeTabs;
        }

        private void PoblarTecnicos()
        {
            cboTecnico.Items.Clear();
            reader = cListaCheq.ConsultarTecnico();
            cboTecnico.Items.Add(new ListItem("Seleccione", "0"));

            while (reader.Read())
            {
                cboTecnico.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
            }

            reader.Close();
            cListaCheq.CerrarConexion();
        }

        protected void txtOF_TextChanged(object sender, EventArgs e)
        {
            CargarLista();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearContent();
            Response.Redirect("ListaChequeo.aspx");
        }

        private void CargarLista()
        {
            if (cboProducido.SelectedItem.Text == "Colombia")
            {
                string Nombre = (string)Session["Nombre_Usuario"];

                // Parametros de la BBDD
                SqlParameter[] sqls = new SqlParameter[2];
                sqls[0] = new SqlParameter("@pNumOrden ", txtOF.Text);
                sqls[1] = new SqlParameter("@pUsuario ", Nombre);

                string conexion = BdDatos.conexionScope();

                 // Creamos la conexión
                using (SqlConnection con = new SqlConnection(conexion))
                {
                    // Creamos el Comando
                    using (SqlCommand cmd = new SqlCommand("USP_fup_SEL_ListaChequeo", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddRange(sqls);

                        SqlParameter ST_FUP = new SqlParameter("@pFUP", SqlDbType.Int);
                        SqlParameter ST_Ver = new SqlParameter("@pVersion", SqlDbType.VarChar, 3);
                        SqlParameter ST_Cliente = new SqlParameter("@pCli_nombre", SqlDbType.VarChar, 200);
                        SqlParameter ST_Pais = new SqlParameter("@pCli_pais ", SqlDbType.VarChar, 200);
                        SqlParameter ST_Ciudad = new SqlParameter("@pCli_ciudad", SqlDbType.VarChar, 200);
                        SqlParameter ST_Contacto = new SqlParameter("@pContacto", SqlDbType.VarChar, 200);
                        SqlParameter ST_Obra = new SqlParameter("@pObra", SqlDbType.VarChar, 200);
                        SqlParameter ID_Lista = new SqlParameter("@pIDLista", SqlDbType.Int);
                        SqlParameter ST_Estado = new SqlParameter("@pEstado", SqlDbType.VarChar, 20);
                        SqlParameter ST_TipoArmado = new SqlParameter("@pTipoArmado", SqlDbType.VarChar, 200);
                        SqlParameter ID_Cedula = new SqlParameter("@pTec_cedula", SqlDbType.Int);
                        SqlParameter ST_Tecnico = new SqlParameter("@pTec_nombre", SqlDbType.VarChar, 200);
                        SqlParameter ID_Existe = new SqlParameter("@pExisteOrd", SqlDbType.Int);
                        SqlParameter ID_OFA = new SqlParameter("@pIdOrden", SqlDbType.Int);

                        ST_FUP.Direction = ParameterDirection.Output;
                        ST_Ver.Direction = ParameterDirection.Output;
                        ST_Cliente.Direction = ParameterDirection.Output;
                        ST_Pais.Direction = ParameterDirection.Output;
                        ST_Ciudad.Direction = ParameterDirection.Output;
                        ST_Contacto.Direction = ParameterDirection.Output;
                        ST_Obra.Direction = ParameterDirection.Output;
                        ID_Lista.Direction = ParameterDirection.Output;
                        ST_Estado.Direction = ParameterDirection.Output;
                        ST_TipoArmado.Direction = ParameterDirection.Output;
                        ID_Cedula.Direction = ParameterDirection.Output;
                        ST_Tecnico.Direction = ParameterDirection.Output;
                        ID_Existe.Direction = ParameterDirection.Output;
                        ID_OFA.Direction = ParameterDirection.Output;

                        cmd.Parameters.Add(ST_FUP);
                        cmd.Parameters.Add(ST_Ver);
                        cmd.Parameters.Add(ST_Cliente);
                        cmd.Parameters.Add(ST_Pais);
                        cmd.Parameters.Add(ST_Ciudad);
                        cmd.Parameters.Add(ST_Contacto);
                        cmd.Parameters.Add(ST_Obra);
                        cmd.Parameters.Add(ID_Lista);
                        cmd.Parameters.Add(ST_Estado);
                        cmd.Parameters.Add(ST_TipoArmado);
                        cmd.Parameters.Add(ID_Cedula);
                        cmd.Parameters.Add(ST_Tecnico);
                        cmd.Parameters.Add(ID_Existe);
                        cmd.Parameters.Add(ID_OFA);

                        // Abrimos la conexión y ejecutamos el ExecuteReader
                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            //VALORES DEL ENCABEZADO
                            int Existe = Convert.ToInt32(ID_Existe.Value);
                            if (Existe != 0)
                            {
                                LFUP.Text = Convert.ToString(ST_FUP.Value).ToString() + " " + Convert.ToString(ST_Ver.Value).ToString();
                                LCliente.Text = Convert.ToString(ST_Cliente.Value).ToString();
                                LPais.Text = Convert.ToString(ST_Pais.Value).ToString();
                                LCiudad.Text = Convert.ToString(ST_Ciudad.Value).ToString();
                                LContacto.Text = Convert.ToString(ST_Contacto.Value).ToString();
                                LObra.Text = Convert.ToString(ST_Obra.Value).ToString();
                                string idLista = Convert.ToInt32(ID_Lista.Value).ToString();
                                LEstado.Text = Convert.ToString(ST_Estado.Value).ToString();
                                txtArmado.Text = Convert.ToString(ST_TipoArmado.Value);
                                string idOFA = Convert.ToInt32(ID_OFA.Value).ToString();

                                string Tecnico = Convert.ToString(ST_Tecnico.Value);
                                string Cedula = Convert.ToInt32(ID_Cedula.Value).ToString();

                                if (Cedula.Trim().Length != 0)
                                    cboTecnico.SelectedValue = Cedula.Trim();
                                else
                                    cboTecnico.SelectedIndex = 0;

                                Session["IDLista"] = idLista;
                                Session["IDOFA"] = idOFA;
                                Session["FUP"] = Convert.ToString(ST_FUP.Value).ToString();
                                Session["VER"] = Convert.ToString(ST_Ver.Value).ToString();
                                Session["OF"] = txtOF.Text;

                                lblEstado.Visible = true;
                                LEstado.Visible = true;

                                if (LEstado.Text == "No Existe")
                                {
                                    btnFinalizar.Enabled = false;
                                    lkSubirFotos.Enabled = false;
                                    btnGrabaenCliente.Enabled = false;
                                }

                                if (LEstado.Text == "Aprobado")
                                {
                                    btnGuardar.Enabled = false;
                                    lkSubirFotos.Enabled = false;
                                    btnGrabaenCliente.Enabled = false;
                                    btnAprobar.Enabled = false;
                                }

                                if (LEstado.Text == "Finalizado")
                                {
                                    btnFinalizar.Enabled = false;
                                }

                                int rol = (int)Session["Rol"];
                                if ((rol == 18) || (rol == 16))
                                {
                                    btnGrabaenCliente.Visible = true;
                                }

                                MostrarLista(Convert.ToString(idLista));
                                tabcEncuesta.Visible = true;
                                CargarGrillaFotografia();
                            }
                            else
                            {
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
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                            }
                        }
                    }
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string lista = (string)Session["IDLista"];
            int estado = 2;
            string Nombre = (string)Session["Nombre_Usuario"];
            string rcEmail = (string)Session["rcEmail"];
            string ofa = (string)Session["IDOFA"];

            int IngEncabezadoLista = cListaCheq.IngEncabezadoLista(Convert.ToInt32(lista),
                txtOF.Text, estado, txtArmado.Text, cboTecnico.SelectedItem.Value,
                Nombre, rcEmail);

            EnviarCorreoListaChequeo();

            string mensaje = "Lista de chequeo ingresada correctamente.";
            LEstado.Text = "Guardado";
        }

        protected void btnReporte_Click(object sender, EventArgs e)
        {
            Session["OF"] = txtOF.Text.Trim();

        }

        protected void btnAprobar_Click(object sender, EventArgs e)
        {
            int estado = 4;
            string lista = (string)Session["IDLista"];
            string idofa = (string)Session["IDOFA"];

            if (LEstado.Text == "Aprobado")
            {
                string mensaje = "La lista de chequeo se encuentra ya aprobada.";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
            else
            {
                int actualizar = cListaCheq.ActualizarEstado(Convert.ToInt32(idofa), estado);
                string mensaje = "Lista de chequeo aprobada correctamente.";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);

                LEstado.Text = "Aprobado";
                btnFinalizar.Enabled = true;

                EnviarCorreoListaChequeo();
                btnAprobar.Enabled = false;
            }
        }

        private void EnviarCorreoListaChequeo()
        {
            string mensaje;
            string usuario = (string)Session["Nombre_Usuario"];

            string Email = "",Asunto = "", Email2 = "", Cuerpo = "";
           
            Byte[] correo = new Byte[0];
            WebClient clienteWeb = new WebClient();
            clienteWeb.Dispose();
            clienteWeb.Credentials = new System.Net.NetworkCredential("forsa", "F0rUs3s4*", "FORSA");
            correo = clienteWeb.DownloadData(@"http://10.75.131.2:81/ReportServer?/InformesFUP/Com_ListaChequeo&rs:format=PDF&rs:command=render&rs:ClearSession=true&pNumOrden=" + txtOF.Text.Trim() + "");
            MemoryStream ms = new MemoryStream(correo);
            string correoSistema = (string)Session["CorreoSistema"];
            string UsuarioAsunto = (string)Session["UsuarioAsunto"];


            string idOFA = (string)Session["IDOFA"];

            reader = cListaCheq.ConsultarEstado(Convert.ToInt32(idOFA));
            reader.Read();
            string Estado = reader.GetValue(0).ToString();
            string MailCrea = reader.GetValue(1).ToString();
            reader.Close();

            if (Estado == "2")
            {
                Asunto = "SIO – INGRESO LISTA DE CHEQUEO: "+ UsuarioAsunto  +" ORDEN: " + txtOF.Text.Trim() + "";
                Cuerpo = "EL USUARIO " + usuario + " INGRESO LISTA DE CHEQUEO ORDEN: " + txtOF.Text.Trim() + "";
            }

            if (Estado == "3")
            {
                Asunto = "SIO – FINALIZACION LISTA DE CHEQUEO ORDEN: " + txtOF.Text.Trim() + "";
                Cuerpo = "EL USUARIO " + usuario + " FINALIZÓ LISTA DE CHEQUEO ORDEN: " + txtOF.Text.Trim() + "";
            }

            if (Estado == "4")
            {
                Asunto = "SIO – APROBACION LISTA DE CHEQUEO ORDEN: " + txtOF.Text.Trim() + "";
                Cuerpo = "EL USUARIO " + usuario + " APROBÓ LISTA DE CHEQUEO ORDEN: " + txtOF.Text.Trim() + "";
            }

            //OBTENGO CORREOS DE CALIDAD
            reader = cListaCheq.CorreosCalidad();
            if (reader != null)
            {
                while (reader.Read())
                {
                    if (Email == "")
                    {
                        Email = reader.GetValue(1).ToString();
                        Session["CalEmail"] = Email;
                    }
                    else
                    {
                        Email = Email + "," + reader.GetValue(1).ToString();
                        Session["CalEmail"] = Email;
                    }
                }
                reader.Close();
            }

            //OBTENGO CORREOS POR DEFECTO PARA LISTA Y ADMINISTRADORES
            reader = cListaCheq.CorreosListaAdmin();
            if (reader != null)
            {
                while (reader.Read())
                {
                    if (Email2 == "")
                    {
                        Email2 = reader.GetValue(1).ToString();
                        Session["LAdminEmail"] = Email2;
                    }
                    else
                    {
                        Email2 = Email2 + "," + reader.GetValue(1).ToString();
                        Session["LAdminEmail"] = Email2;
                    }
                }
                reader.Close();
            }

            string rcEmail = (string)Session["rcEmail"];
            string EmailCalidad = (string)Session["CalEmail"];
            string EmailLista = (string)Session["LAdminEmail"];
            //DEFINIMOS LA CLASE DE MAILMESSAGE
            MailMessage mail = new MailMessage();
            //INDICAMOS EL EMAIL DE ORIGEN
            mail.From = new MailAddress(correoSistema);
            //AÑADIMOS LA DIRRECCIÓN DE CORREO DESTINATARIO            
            if (Estado == "4")
            {
                mail.To.Add(EmailCalidad + ',' + MailCrea);
            }
            else
            {
                if (Estado == "2")
                {
                    mail.To.Add(MailCrea);
                }
                else
                {
                    mail.To.Add(EmailCalidad);
                }
            }
            //AÑADIMOS COPIA AL REPRESENTANTE
            mail.CC.Add(EmailLista);
            //INCLUIMOS EL ASUNTO DEL MENSAJE
            mail.Subject = Asunto;            
            //AÑADIMOS EL CUERPO DEL MENSAJE
            mail.Body = Cuerpo + "\n\n" +
            "CLIENTE: " + LCliente.Text + "\n\n" + "PAIS: " + LPais.Text + "\n\n" +
            "CIUDAD: " + LCiudad.Text + "\n\n" + "CONTACTO: " + LContacto.Text + "\n\n" +
            "OBRA: " + LObra.Text + "\n\n" + "OF: " + txtOF.Text.Trim() + "\n\n" +
            "Sitio Web. http://app.forsa.com.co" + "\n\n" + "SIO." + "\n\n" +rcEmail+ "\n\n"+
            "Sistema Información Operacional." + "\n\n" + "Gestión Informática."+ "\n\n" + "FORSA S.A.";
            //INDICAMOS EL TIPO DE CODIFICACIÓN DEL MENSAJE
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            //DEFINIMOS LA PRIORIDAD DEL MENSAJE
            mail.Priority = System.Net.Mail.MailPriority.Normal;
            //INDICAMOS SI EL CUERPO DEL MENSAJE ES HTML O NO
            mail.IsBodyHtml = false;
            if (LEstado.Text == "Aprobado")
            {
                mail.Attachments.Add(new Attachment(ms, "LISTA DE CHEQUEO OF" + txtOF.Text.Trim() + ".pdf"));
            }
            //DECLARAMOS LA CLASE SMTPCLIENT
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
                mensaje = "ERROR: " + ex.Message;
            }
            
        }

        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            int estado = 3;
            string lista = (string)Session["IDLista"];
            string idofa = (string)Session["IDOFA"];

            if (LEstado.Text == "Finalizado")
            {
                string mensaje = "La lista de chequeo se encuentra ya finalizada.";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
            else
            {
                int actualizar = cListaCheq.ActualizarEstado(Convert.ToInt32(idofa), estado);

                EnviarCorreoListaChequeo();

                string mensaje = "Lista de chequeo finalizada correctamente.";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);

                LEstado.Text = "Finalizado";

                btnGuardar.Enabled = false;
                lkSubirFotos.Enabled = false;
                btnGrabaenCliente.Enabled = false;
                btnFinalizar.Enabled = false;
            }
        }

        protected void lkSubirFotos_Click(object sender, EventArgs e)
        {
            Session["OF"] = txtOF.Text;
            string fup = (string)Session["FUP"];
            string version = (string)Session["VER"];
            Session["VER"] = version;
        }

        protected void anexo_OnClick(object sender, EventArgs e)
        {
            LinkButton link = (LinkButton)sender;
            string URL = link.Text;
            URL = Page.ResolveClientUrl(URL);
            link.OnClientClick = "window.open('" + URL + "'); return false;";
            //Response.Redirect(link.Text);
        }

        private void CargarGrillaFotografia()
        {
            string FUP = (string)Session["FUP"];
            string VER = (string)Session["VER"];
            dsLista.Reset();
            dsLista = cListaCheq.ConsultarFotografiaLista(Convert.ToInt32(FUP), VER);
            if (dsLista != null)
            {
                grvArchivo.DataSource = dsLista.Tables[0];
                grvArchivo.DataBind();
                grvArchivo.Visible = true;
            }
            else
            {
                grvArchivo.Dispose();
                grvArchivo.Visible = false;
            }
            dsLista.Reset();
        }

        public void MostrarLista(string campoEntra)
        {
            AjaxControlToolkit.TabPanel primerCapitulo = new AjaxControlToolkit.TabPanel();
            bool esPrimero = false;
            DataTable dtLista = new DataTable();
            dynamicTabIDs = new List<string>();
            ListadeTabs = new List<AjaxControlToolkit.TabPanel>();

            //int cuentaPreguntas = 0;
            foreach (AjaxControlToolkit.TabPanel Tx in tabcEncuesta.Tabs)
            {
                foreach (Control Cx in Tx.Controls)
                {
                    Cx.Dispose();
                }
            }
            tabcEncuesta.Tabs.Clear();
            cListaCheq.CargaCapitulos();
            foreach (DataRow Cx in cListaCheq.tblCapitulos.Rows)
            {
                AjaxControlToolkit.TabPanel Cuaderno = new AjaxControlToolkit.TabPanel();
                Cuaderno.ID = Cx["idcapitulo"].ToString();
                if (!esPrimero)
                {
                    esPrimero = true;
                    primerCapitulo = Cuaderno;
                }
                Cuaderno.HeaderText = Cx["abreviatura"].ToString();
                cListaCheq.CargaTabla("", Cx["descripcion"].ToString(), Convert.ToInt32(campoEntra));
                dtLista = cListaCheq.tblEncuesta;
                Cuaderno.Controls.Add(construirTabla(ref dtLista, Cx["descripcion"].ToString()));

                cListaCheq.tblEncuesta.Clear();
                tabcEncuesta.Tabs.Add(Cuaderno);

                dynamicTabIDs.Add(Cuaderno.ID);
                ListadeTabs.Add(Cuaderno);
            }

            tabcEncuesta.OnClientActiveTabChanged = "TabChanged";
        }

        private Table construirTabla(ref DataTable tablaDatos, string desCapitulo)
        {
            Table tabla = new Table();
            TableRow fila = new TableRow();
            TableCell celda = new TableCell();
            Control WebCtrl = new Control();
            celda.HorizontalAlign = HorizontalAlign.Left;
            tabla.GridLines = GridLines.Both;

            tabla.Width = 830;
            WebCtrl = ConstruirLabel(cuentafilas.ToString(), desCapitulo, true, true);
            celda.ColumnSpan = 5;
            celda.Controls.Add(WebCtrl);
            celda.HorizontalAlign = HorizontalAlign.Center;
            fila.Cells.Add(celda);
            fila.BackColor = System.Drawing.ColorTranslator.FromHtml("#1C5AB6");
            fila.BorderColor = System.Drawing.ColorTranslator.FromHtml("#1C5AB6");
            tabla.Rows.Add(fila);

            foreach (DataRow rx in tablaDatos.Rows)
            {
                DataRow rxy = rx;
                TableRow f = ConstruirFila(ref rxy);
                tabla.Rows.Add(f);
            }

            return tabla;
        }

        private Label ConstruirLabel(string IDControl, string textoControl, bool Negrita = false, bool Titulo = false, bool Depende = false, bool Condicionada = false)
        {
            Label lblcontrol = new Label();
            lblcontrol.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            lblcontrol.ID = "lbl" + IDControl;
            lblcontrol.Text = textoControl;
            lblcontrol.Font.Name = "Arial";
            lblcontrol.Font.Size = 8;
            if (Negrita)
            {
                lblcontrol.Font.Bold = true;
                lblcontrol.Font.Size = 10;
            }
            if (Titulo)
            {
                lblcontrol.ForeColor = System.Drawing.Color.White;
                lblcontrol.Font.Size = 8;
            }
            return lblcontrol;
        }

        private TableRow ConstruirFila(ref DataRow rxy)
        {
            DataRow rx = rxy;
            cuentafilas += 1;
            TableRow fila = new TableRow();
            if (cuentafilas / 2 == cuentafilas / 2)
            {
                fila.BackColor = System.Drawing.ColorTranslator.FromHtml("#d4e3e5");
                fila.BorderColor = System.Drawing.ColorTranslator.FromHtml("#d4e3e5");
            }
            else
            {
                fila.BackColor = System.Drawing.ColorTranslator.FromHtml("#c3dde0");
                fila.BorderColor = System.Drawing.ColorTranslator.FromHtml("#c3dde0");
            }
            TableCell c = new TableCell();
            Control WebCtrl = new Control();
            //
            WebCtrl = ConstruirLabel(rx["nombreC1"].ToString(), rx["consecutivo"].ToString(), false, false, false, false);
            c.Controls.Add(WebCtrl);
            c.Width = 10;
            c.HorizontalAlign = HorizontalAlign.Justify;
            c.BorderStyle = BorderStyle.Outset;
            fila.Cells.Add(c);
            //
            TableCell d = new TableCell();
            WebCtrl = ConstruirLabel(rx["nombreC2"].ToString(), rx["descripcion"].ToString(), false, false, false, false);
            d.Controls.Add(WebCtrl);
            d.Width = 500;
            d.HorizontalAlign = HorizontalAlign.Justify;
            d.BorderStyle = BorderStyle.Outset;
            fila.Cells.Add(d);
            //
            TableCell e = new TableCell();
            WebCtrl = construirComboBox(rx["nombreC3"].ToString(), rx["conf"].ToString(), "");
            e.Controls.Add(WebCtrl);
            e.Width = 50;
            e.BorderStyle = BorderStyle.Outset;
            fila.Cells.Add(e);
            //
            TableCell f = new TableCell();
            WebCtrl = construirTextbox(rx["nombreC4"].ToString(), rx["memo"].ToString(), false, 1, 0, false, "", false);
            f.Controls.Add(WebCtrl);
            f.Width = 100;
            f.BorderStyle = BorderStyle.Outset;
            fila.Cells.Add(f);
            //
            TableCell g = new TableCell();
            WebCtrl = construirTextbox(rx["nombreC5"].ToString(), rx["observacion"].ToString(), false, 2, 0, false, "", false);
            g.Controls.Add(WebCtrl);
            g.Width = 200;
            g.BorderStyle = BorderStyle.Outset;
            fila.Cells.Add(g);
            //
            return fila;
        }

        private DropDownList construirComboBox(string IDControl, string NombreCampo, string ValorControl = "")
        {
            //Se carga objeto tabla con los datos de la tabla Respuestas de acuerdo con la pregunta
            DropDownList mylist = new DropDownList();
            mylist.Font.Name = "Arial";
            mylist.Font.Size = 8;
            mylist.ID = IDControl;
            mylist.AutoPostBack = true;
            mylist.Items.Add("Seleccione");
            mylist.Items[0].Value = "0";
            //se llena el dropdown, con las respuestas en el objeto tabla
            mylist.Items.Add("Si");
            mylist.Items[1].Value = "1";
            mylist.Items.Add("No");
            mylist.Items[2].Value = "2";
            mylist.Items.Add("N/A");
            mylist.Items[3].Value = "3";
            mylist.SelectedIndex = -1;
            if (NombreCampo.Trim().Length > 0)
                mylist.SelectedValue = NombreCampo.Trim();
            else
                mylist.SelectedIndex = -1;
            mylist.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            return mylist;
        }
        private RadioButtonList construirRadioButList(string IdControl, string NombreCampo, String ValorControl = "")
        {
            RadioButtonList mylist = new RadioButtonList();
            mylist.Font.Name = "Arial";
            mylist.Font.Size = 8;
            mylist.ID = IdControl;
            //se llena el RadioButtonList, con las respuestas en el objeto tabla
            mylist.Items.Add(new ListItem("Si","1"));
            //mylist.Items[1].Value = "1";
            mylist.Items.Add(new ListItem("No", "2"));
            //mylist.Items[2].Value = "2";
            mylist.Items.Add(new ListItem("N/A", "3"));
            //mylist.Items[3].Value = "3";
            mylist.SelectedIndex = 3;
            if (NombreCampo.Trim().Length > 0)
            switch (NombreCampo)
            {
                case "1":
                    mylist.SelectedIndex= 1;
                    break;
                case "2":
                    mylist.SelectedIndex= 2;
                    break;
                default:
                    mylist.SelectedIndex= 3;
                    break;
            };
            mylist.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            return mylist;
        }

        private TextBox construirTextbox(string IDControl, string textoControl = "", bool paracalculo = false, int filas = 1, int Largo = 0, bool Calculada = false, string Expresion = "", bool Condicionada = false)
        {
            //HtmlControl
            TextBox t = new TextBox();
            t.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            t.ID = IDControl;

            t.Font.Name = "Arial";
            t.Font.Size = 8;

            if (filas > 1)
            {
                t.TextMode = TextBoxMode.MultiLine;
                t.Rows = filas;
            }
            else
            {
                t.TextMode = TextBoxMode.SingleLine;
                t.MaxLength = Largo;
            }
            if (Largo > 0)
            {
                t.Width = 20;
                t.MaxLength = 2;
            }
            t.Text = "";
            if (textoControl.Trim().Length > 0)
                t.Text = textoControl.Trim();
            else
                t.Text = "";
            return t;
        }

        private TableCell construirCelda(string tipoControl, ref DataRow rx, int filas = 1, bool negrita = false, bool Titulo = false, bool Vacia = false, int Largo = 0)
        {
            TableCell celda = new TableCell();
            Control WebCtrl = new Control();
            celda.HorizontalAlign = HorizontalAlign.Left;
            switch (tipoControl)
            {
                case "Combo":
                    WebCtrl = construirComboBox(rx["idpregunta"].ToString(), rx["nombrecampo"].ToString(), "");
                    break;
                case "Label":
                    if (Vacia)
                    {
                        WebCtrl = ConstruirLabel(rx["nombrecampo"] + "vacio", "", negrita, Titulo, false, false);
                    }
                    else
                    {
                        WebCtrl = ConstruirLabel(rx["nombrecampo"].ToString(), rx["descripcion"].ToString(), negrita, Titulo, false, false);
                    }
                    break;
                case "TextBox":
                    WebCtrl = construirTextbox(rx["nombrecampo"].ToString(), "", false, filas, Largo, false, "", false);
                    break;
            }
            celda.Controls.Add(WebCtrl);
            return celda;
        }

        protected void btnGrabar_Click(object sender, EventArgs e)
        {
            string lista = (string)Session["IDLista"];
            string cadenacampos = null;
            string cadenavalores = null;
            string tipocontrol = "";
            cadenacampos = "";
            cadenavalores = "";
            foreach (AjaxControlToolkit.TabPanel eX in ListadeTabs)
            {
                foreach (Table tb in eX.Controls)
                {
                    foreach (TableRow rX in tb.Controls)
                    {
                        //cadenavalores = "(" & txtIdListaChequeo.Text.Trim & ""
                        foreach (TableCell cX in rX.Controls)
                        {
                            string xsalecampo = "";
                            bool hubo = false;
                            foreach (Control c in cX.Controls)
                            {
                                if (!string.IsNullOrEmpty(c.ID))
                                {
                                    if (c.ID.ToString().Contains("lblconsecutivo") == true)
                                    {
                                        xsalecampo = ((Label)c).Text;
                                        hubo = true;
                                        cadenavalores = "(" + lista + "";
                                    }
                                    
                                    tipocontrol = c.GetType().Name;
                                    if (tipocontrol == "TextBox")
                                    {
                                        xsalecampo = ((TextBox)c).Text;
                                        hubo = true;
                                    }
                                    else if (tipocontrol == "DropDownList")
                                    {
                                        DropDownList drpdwn = (DropDownList)c;
                                        if (drpdwn.SelectedIndex > 0)
                                        {
                                            xsalecampo = drpdwn.SelectedItem.Value.ToString();
                                            hubo = true;
                                        }
                                        else
                                        {
                                            xsalecampo = "0";
                                            hubo = true;
                                        }
                                    }
                                    else
                                    {
                                        //If Len(Trim(cadenavalores)) = 0 Then
                                        //    cadenavalores = c.ID
                                        //Else
                                        //    cadenavalores = cadenavalores + ", " + c.ID
                                        //End If
                                    }
                                }
                            }
                            if (hubo)
                                cadenavalores = cadenavalores + ", '" + xsalecampo + "'";
                            hubo = false;
                        }
                        if (cadenavalores.Trim().Length > 0)
                        {
                            cadenavalores = cadenavalores + ")";
                            if (cadenacampos.ToString().Trim().Length > 0)
                            {
                                cadenacampos = cadenacampos + "," + cadenavalores.Trim();
                            }
                            else
                            {
                                cadenacampos = cadenavalores.Trim();
                            }
                        }
                        cadenavalores = "";
                    }

                }
            }

            string Nombre = (string)Session["Nombre_Usuario"];
            cListaCheq.GrabarEncuesta(Nombre, cadenacampos);

            EnviarCorreoListaChequeo();
        }
        
    }
}