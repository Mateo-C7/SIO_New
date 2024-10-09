using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using CapaControl;
using CapaDatos;

namespace SIO
{
    public partial class Inicio1 : System.Web.UI.Page
    {
        public SqlDataReader reader = null;
        public ControlInicio CI = new ControlInicio();
        public BdDatos BdDatos = new BdDatos();

        protected void Page_Load(object sender, EventArgs e)
        {
            //lblMSJInicio.Text = (string)Session["Mensaje"];
            //if (lblMSJInicio.Text == "")
            //{
            //    lblMSJInicio.Visible = false;
            //}
            //else
            //{
            //    lblMSJInicio.Visible = true;
            //}

            ////CARGO EL IDIOMA DESDE OTRA PAGINA
            //string idioma = (string)Session["Lengua"];
            //if (idioma != null)
            //{
            //    cboIdioma.Items.Clear();
            //    cboIdioma.Items.Add(new ListItem(idioma, idioma));
            //    cboIdioma.Items.Add(new ListItem("",""));
            //    cboIdioma.Items.Add(new ListItem("Español", "Español"));
            //    cboIdioma.Items.Add(new ListItem("Ingles", "Ingles"));
            //    cboIdioma.Items.Add(new ListItem("Portugues", "Portugues"));
                
            //    this.Idioma();
            //    this.IdiomaTextBox();
            //    Session.Remove("Lengua");
            //    Session.Remove("Mensaje");
            //    Session["Cultura"] = idioma;
            //}

            if (IsPostBack == true)
            {
                Session["Idioma"] = "Español";

                //lblConectadoA.Text = Application["activos"].ToString();
            }
            Session["Idioma"] = "Español";

            //lblConectadoA.Text = Application["activos"].ToString();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            Server.ScriptTimeout = 6000;
        }

        //private void Idioma()
        //{
        //    if (cboIdioma.SelectedItem.Text == "Español")
        //    {
        //        btnLogin.Text = "Iniciar Sesión";
        //        lkRecuperar.Text = "¿Olvidaste Tu Contraseña?";
        //        lkCambiarContra.Text = "Cambiar Contraseña";
        //        txtContrasena.ToolTip = "Contraseña";

        //        int posicion = 1;
        //        posicion = posicion;
        //        reader = CI.ConsultarIdiomaEspañol(posicion);
        //        reader.Read();
        //        if (posicion == 1)
        //        {
        //            lblIdioma.Text = reader.GetValue(0).ToString();
        //            posicion = posicion + 1;
        //        }
        //    }

        //    if (cboIdioma.SelectedItem.Text == "Ingles")
        //    {
        //        btnLogin.Text = "Sign In";
        //        lkRecuperar.Text = "Forgot Password?";
        //        lkCambiarContra.Text = "Change Password";
        //        txtContrasena.ToolTip = "Password";

        //        int posicion = 1;
        //        posicion = posicion;
        //        reader = CI.ConsultarIdiomaIngles(posicion);
        //        reader.Read();
        //        if (posicion == 1)
        //        {
        //            lblIdioma.Text = reader.GetValue(0).ToString();
        //            posicion = posicion + 1;
        //        }
        //    }

        //    if (cboIdioma.SelectedItem.Text == "Portugues")
        //    {
        //        btnLogin.Text = "Entrar";
        //        lkRecuperar.Text = "Esqueceu Sua Senha?";
        //        lkCambiarContra.Text = "Mudar Senha";
        //        txtContrasena.ToolTip = "Senha";

        //        int posicion = 1;
        //        posicion = posicion;
        //        reader = CI.ConsultarIdiomaPortugues(posicion);
        //        reader.Read();
        //        if (posicion == 1)
        //        {
        //            lblIdioma.Text = reader.GetValue(0).ToString();
        //            posicion = posicion + 1;
        //        }
        //    }
        //}

        private void IdiomaTextBox()
        {
            if (cboIdioma.SelectedItem.Text == "Español")
            {
                txtUsuario.Text = TextBoxWatermarkExtender1.WatermarkText = "Nombre De Usuario";
                txtContrasena.Text = TextBoxWatermarkExtender2.WatermarkText = "Contraseña";
            }

            if (cboIdioma.SelectedItem.Text == "Ingles")
            {
                txtUsuario.Text = TextBoxWatermarkExtender1.WatermarkText = "Username";
                txtContrasena.Text = TextBoxWatermarkExtender2.WatermarkText = "Password";
            }

            if (cboIdioma.SelectedItem.Text == "Portugues")
            {
                txtUsuario.Text = TextBoxWatermarkExtender1.WatermarkText = "Nome De Usuario";
                txtContrasena.Text = TextBoxWatermarkExtender2.WatermarkText = "Senha";                
            }
        }       

        protected void btnLogin_Click(object sender, EventArgs e)
        {
             
            int rol = 0, usuId = 0, ModificaPlazo = 0;
            // revisar reader
            int existeLogin = CI.verificarLogin(txtUsuario.Text);
            if (existeLogin == 0)
            {                
                lblMSJInicio.Text = "El nombre de usuario no existe.";
                lblMSJInicio.Visible = true;
                
            }
            else
            {
                //ok reader
                string passwdOk = CI.verificarContrasena(txtUsuario.Text, txtContrasena.Text);
                if (passwdOk == txtContrasena.Text)
                {   // ok reader                                         
                    reader= CI.obtenerRolByUsuLogin(txtUsuario.Text);

                    if (reader.HasRows)
                    {
                        reader.Read();
                        rol = reader.GetInt32(0);
                        usuId = reader.GetInt32(1);
                        ModificaPlazo = reader.GetInt32(2);
                    }
                    reader.Close();
                    reader.Dispose();
                    CI.CerrarConexion();

                    Session["usuId"] = usuId;
                    int RolID = rol;

                    // ok reader
                    string Rolnombre = CI.obtenerNombreRolByID(RolID);

                    Session["Usuario"] = txtUsuario.Text;
                    Session["Rol"] = RolID;
                    Session["ModificaPlazo"] = ModificaPlazo;



                    reader = CI.ObtenerFechaActualizacion(txtUsuario.Text);
                    if (reader.HasRows == false)
                    {
                        if (reader.Read() == false)
                        {
                            if (cboIdioma.Text == "Español")
                            {
                                lblMSJInicio.Text = "El usuario no tiene fecha de actualización de la contraseña.";
                                lblMSJInicio.Visible = true;
                            }
                             
                            reader.Close();
                            reader.Dispose();
                            BdDatos.desconectar();
                        }
                        
                    }
                    else
                    {
                        reader.Read();
                        lblMSJInicio.Visible = false;
                        string fecha_caducacion = reader.GetDateTime(0).ToShortDateString();
                        DateTime fecha_caducacion1 = Convert.ToDateTime(fecha_caducacion);
                        DateTime fecha_caducacion2 = fecha_caducacion1.AddDays(72);

                        string fecha_final_caducacion = fecha_caducacion2.ToShortDateString();
                        reader.Close();
                        reader.Dispose();
                        CI.CerrarConexion();

                        if (fecha_final_caducacion == DateTime.Now.ToString("dd/MM/yyyy"))
                        {
                            Response.Redirect("CambiarContraseña.aspx");
                        }
                        else
                        {
                            // ok reader
                            reader = CI.ObtenerAdminGasto(txtUsuario.Text);
                            string gasto = "";
                            string cedula = "";

                            if (reader.HasRows == true)
                            {
                                reader.Read();
                                gasto = reader.GetValue(0).ToString();
                                cedula = reader.GetValue(1).ToString();
                            }
                                reader.Close();
                                reader.Dispose();
                                CI.CerrarConexion();

                                //OnlineActiveUsers.OnlineUsersInstance.OnlineUsers.SetUserOnline(Session["Usuario"].ToString());
                                Session["AdminGasto"] = gasto;
                                Session["cedula"] = cedula;

                                Session["hora"] = DateTime.Now.ToString();

                                Session["linea"] = " <tr> <th>" + Session["Usuario"].ToString() + "</th> <th>" + Session["hora"].ToString() + "</th> </tr> ";

                            // Código que se ejecuta cuando se inicia una nueva sesión
                            //Application.Lock();
                            //Application["activos"] = (int)Application["activos"] + 1;
                            //Application["usuarios"] = Application["usuarios"] + " " + Session["linea"].ToString();
                            //Application.UnLock();
                            //CI.Guardar_Log_Sesion(Convert.ToInt32(cedula), txtUsuario.Text,);
                            Session["estadoSesion"] = "true";
                             
                            Response.Redirect("Home.aspx");
                           

                            
                        }
                    }
                }
                else
                {
                    if (cboIdioma.Text == "Español")
                    {
                        lblMSJInicio.Text = "Contraseña incorrecta.";
                        lblMSJInicio.Visible = true;
                    }
                    if (cboIdioma.Text == "Ingles")
                    {
                        lblMSJInicio.Text = "Incorrect password.";
                        lblMSJInicio.Visible = true;
                    }
                    if (cboIdioma.Text == "Portugues")
                    {
                        lblMSJInicio.Text = "Senha errada.";
                        lblMSJInicio.Visible = true;
                    }
                }
            }
        }

        protected void lkRecuperar_Click(object sender, EventArgs e)
        {
            Response.Redirect("PasswordRecovery.aspx");
        }

        protected void lkCambiarContra_Click(object sender, EventArgs e)
        {
            Response.Redirect("CambiarContraseña.aspx");
        }

        protected void cboIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session.Remove("Lengua");
            string idioma = (string)Session["Cultura"];
            this.IdiomaTextBox();
            Session["Idioma"] = cboIdioma.SelectedItem.Text;
            cboIdioma.Items.Remove(new ListItem(idioma, idioma));
            cboIdioma.Items.Remove(new ListItem("", ""));
        }
    }
}