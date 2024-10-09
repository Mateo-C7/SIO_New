using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using CapaControl;

namespace SIO
{
    public partial class MobilInicio : System.Web.UI.Page
    {
        public SqlDataReader reader = null;
        public ControlInicio CI = new ControlInicio();

        protected void Page_Load(object sender, EventArgs e)
        {
            string idioma = (string)Session["Lengua"];
            if (idioma != null)
            {
                cboIdioma.Items.Clear();
                cboIdioma.Items.Add(new ListItem(idioma, idioma));
                cboIdioma.Items.Add(new ListItem("", ""));
                cboIdioma.Items.Add(new ListItem("Español", "Español"));
                cboIdioma.Items.Add(new ListItem("Ingles", "Ingles"));
                cboIdioma.Items.Add(new ListItem("Portugues", "Portugues"));

                this.Idioma();
                this.IdiomaTextBox();
                Session.Remove("Lengua");
                Session.Remove("Mensaje");
                Session["Cultura"] = idioma;
            }

            if (IsPostBack == true)
            {
                this.Idioma();
            }
            Session["Idioma"] = cboIdioma.SelectedItem.Text;

        }

        private void Idioma()
        {
            if (cboIdioma.SelectedItem.Text == "Español")
            {
                btnLogin.Text = "Iniciar Sesión";
                txtContrasena.ToolTip = "Contraseña";

                int posicion = 1;

                reader = CI.ConsultarIdiomaEspañol(posicion);
                reader.Read();
                if (posicion == 1)
                {
                    lblIdioma.Text = reader.GetValue(0).ToString();
                    posicion = posicion + 1;
                }
            }

            if (cboIdioma.SelectedItem.Text == "Ingles")
            {
                btnLogin.Text = "Sign In";
                txtContrasena.ToolTip = "Password";

                int posicion = 1;
                posicion = posicion;
                reader = CI.ConsultarIdiomaIngles(posicion);
                reader.Read();
                if (posicion == 1)
                {
                    lblIdioma.Text = reader.GetValue(0).ToString();
                    posicion = posicion + 1;
                }
            }

            if (cboIdioma.SelectedItem.Text == "Portugues")
            {
                btnLogin.Text = "Entrar";
                txtContrasena.ToolTip = "Senha";

                int posicion = 1;
                posicion = posicion;
                reader = CI.ConsultarIdiomaPortugues(posicion);
                reader.Read();
                if (posicion == 1)
                {
                    lblIdioma.Text = reader.GetValue(0).ToString();
                    posicion = posicion + 1;
                }
            }
        }

        private void IdiomaTextBox()
        {
            if (cboIdioma.SelectedItem.Text == "Español")
            {
                txtUsuario.Text = txtUsuario_TextBoxWatermarkExtender.WatermarkText = "Nombre De Usuario";
                txtContrasena.Text = TextBoxWatermarkExtender2.WatermarkText = "Contraseña";
            }

            if (cboIdioma.SelectedItem.Text == "Ingles")
            {
                txtUsuario.Text = txtUsuario_TextBoxWatermarkExtender.WatermarkText = "Username";
                txtContrasena.Text = TextBoxWatermarkExtender2.WatermarkText = "Password";
            }

            if (cboIdioma.SelectedItem.Text == "Portugues")
            {
                txtUsuario.Text = txtUsuario_TextBoxWatermarkExtender.WatermarkText = "Nome De Usuario";
                txtContrasena.Text = TextBoxWatermarkExtender2.WatermarkText = "Senha";
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            int existeLogin = CI.verificarLogin(txtUsuario.Text);
            if (existeLogin == 1)
            {
                if (cboIdioma.Text == "Español")
                {
                    lblMSJInicio.Text = "El nombre de usuario no existe.";
                    lblMSJInicio.Visible = true;
                }
                if (cboIdioma.Text == "Ingles")
                {
                    lblMSJInicio.Text = "The username does not exists.";
                    lblMSJInicio.Visible = true;
                }
                if (cboIdioma.Text == "Portugues")
                {
                    lblMSJInicio.Text = "O nome de usuario não existe.";
                    lblMSJInicio.Visible = true;
                }
            }
            else
            {
                string passwdOk = CI.verificarContrasena(txtUsuario.Text, txtContrasena.Text);
                if (passwdOk == txtContrasena.Text)
                {
                    int RolID = CI.obtenerRolByUsuLogin(txtUsuario.Text);
                    string Rolnombre = CI.obtenerNombreRolByID(RolID);

                    Session["Usuario"] = txtUsuario.Text;
                    Session["Rol"] = RolID;

                    reader = CI.ObtenerFechaActualizacion(txtUsuario.Text);
                    if (reader.Read() == false)
                    {
                        if (cboIdioma.Text == "Español")
                        {
                            lblMSJInicio.Text = "El usuario no tiene fecha de actualización de la contraseña.";
                            lblMSJInicio.Visible = true;
                        }
                        if (cboIdioma.Text == "Ingles")
                        {
                            lblMSJInicio.Text = "The user has no date for updating the password.";
                            lblMSJInicio.Visible = true;
                        }
                        if (cboIdioma.Text == "Portugues")
                        {
                            lblMSJInicio.Text = "O usuario não tem data de atualização do senha";
                            lblMSJInicio.Visible = true;
                        }
                    }
                    else
                    {
                        lblMSJInicio.Visible = false;
                        string fecha_caducacion = reader.GetDateTime(0).ToShortDateString();
                        DateTime fecha_caducacion1 = Convert.ToDateTime(fecha_caducacion);
                        DateTime fecha_caducacion2 = fecha_caducacion1.AddDays(72);

                        string fecha_final_caducacion = fecha_caducacion2.ToShortDateString();
                        reader.Close();

                        if (fecha_final_caducacion == DateTime.Now.ToString("dd/MM/yyyy"))
                        {
                            //Response.Redirect("CambiarContraseña.aspx");
                        }
                        else
                        {
                            reader = CI.ObtenerAdminGasto(txtUsuario.Text);
                            reader.Read();
                            string gasto = reader.GetValue(0).ToString();
                            string cedula = reader.GetValue(1).ToString();
                            reader.Close();

                            Session["AdminGasto"] = gasto;
                            Session["cedula"] = cedula;
                            Response.Redirect("MobilCotizacion.aspx");
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