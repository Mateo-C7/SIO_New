using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.Mail;
using CapaControl;

namespace SIO
{
    public partial class PasswordRecovery : System.Web.UI.Page
    {
        public SqlDataReader reader = null;
        public ControlInicio CI = new ControlInicio();

        protected void Page_Load(object sender, EventArgs e)
        {
            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")
            {
                lblContraseña.Text = "Recuperar Contraseña";
                lblOlvido.Text = "¿Olvidaste la contraseña?";
                lblDescripcion.Text = "Escriba su nombre de usuario :";
                btnEnviar.Text = "Enviar";
            }
            if (idioma == "Ingles")
            {
                lblContraseña.Text = "Password Recovery";
                lblOlvido.Text = "Forgot your password?";
                lblDescripcion.Text = "Enter your username :";
                btnEnviar.Text = "Send";
            }
            if (idioma == "Portugues")
            {
                lblContraseña.Text = "Recuperar Senha";
                lblOlvido.Text = "Esqueceu a sua senha?";
                lblDescripcion.Text = "Digite seu nome de usuario:";
                btnEnviar.Text = "Enviar";
            }

            if (IsPostBack == true)
            {              
                
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string idioma = (string)Session["Idioma"];
            //BUSCAMOS LA CONTRASEÑA DEL USUARIO
            string contra = "", msj = "", email = "", msj2 = "", msj3 = "";
            reader = CI.recuperarPass(txtNombre.Text);
            if (reader.Read() == true)
            {
                contra = reader.GetValue(1).ToString();
                
                if (idioma == "Español")
                {
                    msj = "Su contraseña a sido recuperada. " + "\n\n" + "Su contraseña es " + contra + "";                    
                    msj2 = "Recuerde que estamos aquí para apoyar sus labores." + "\n\n\n" + "Cordialmente," + "\n\n" + "Gestión Informática";
                    msj3 = "Contraseña recuperada.";
                }
                if (idioma == "Ingles")
                {
                    msj = "Your password has been recovery. " + "\n\n" + "Your password is " + contra + "";
                    msj2 = "Remenber we're here to support their work." + "\n\n\n" + "Cordially," + "\n\n" + "Gestión Informática";
                    msj3 = "Password recovery.";
                }
                if (idioma == "Portugues")
                {
                    msj = "Sua senha foi recuperada. " + "\n\n" + "Sua senha é " + contra + "";
                    msj2 = "Lembre-se de que estamos aquí para apoiar seu trabalhio." + "\n\n\n" + "Cordialmente," + "\n\n" + "Gestión Informática";
                    msj3 = "Senha recuperada.";
                }

                string rol = reader.GetValue(2).ToString();
                if(rol == "3")
                {
                    reader = CI.obtenerMailRepresentante(txtNombre.Text);
                    reader.Read();
                    email = reader.GetValue(0).ToString();
                    reader.Close();
                }
                else
                {
                    reader = CI.obtenerMailUsuario(txtNombre.Text);
                    reader.Read();
                    email = reader.GetValue(0).ToString();
                    reader.Close();
                }

                //Definimos la clase MailMessage
                MailMessage mail = new MailMessage();
                //Indicamos Email De Origen
                mail.From = new MailAddress("informes@forsa.net.co");
                //Añadimos la direccion correo del  destinatario
                mail.To.Add(email);
                //Incluimos el asunto del mensaje
                string sujeto = lblContraseña.Text;
                mail.Subject = sujeto;
                //Añadimos el cuerpo del mensaje
                string solucion = msj;
                string cuerpo = msj2;
                mail.Body = solucion + "\n\n" + cuerpo;
                //Indicamos el tipo tipo de codificación del mensaje
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                //Defiimos la prioridad del mensaje
                mail.Priority = System.Net.Mail.MailPriority.Normal;
                //Indicamos si el cuerpo del mensaje es HTML o no
                mail.IsBodyHtml = false;

                //Declaramos la clase SmtpClient
                SmtpClient smtp = new SmtpClient();
                //DEFINIMOS NUESTRO SERVIDOR SMTP
                smtp.Host = "smtp.office365.com";
                //INDICAMOS LA AUTENTICACION DE NUESTRO SERVIDOR SMTP
                smtp.Credentials = new System.Net.NetworkCredential("informes@forsa.net.co", "Those7953");
                smtp.Port = 25;
                smtp.EnableSsl = true;

                try
                {
                    smtp.Send(mail);
                }
                catch (Exception ex)
                {

                }
                Session["Mensaje"] = msj3;
                Session["Lengua"] = idioma;

                Response.Redirect("Inicio.aspx");
        
            }
            else
            {
                if (idioma == "Español")
                {
                    msj = "El nombre de usuario no existe.";                    
                }
                if (idioma == "Ingles")
                {
                    msj = "The username does not exists.";                    
                }
                if (idioma == "Portugues")
                {
                    msj = "O nome de usuario não existe.";
                }
            }
            reader.Close();
        }
    }
}