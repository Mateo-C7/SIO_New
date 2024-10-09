using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Data.SqlClient;
using CapaControl;

namespace SIO
{
    public partial class CambiarContraseña : System.Web.UI.Page
    {
        public SqlDataReader reader = null;
        public ControlInicio CI = new ControlInicio();

        protected void Page_Load(object sender, EventArgs e)
        {
            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")
            {
                lblContraseña.Text = "Cambiar Contraseña";                
                lblDescripcion.Text = "Escriba su nombre de usuario :";
                lblContraAnt.Text = "Contraseña anterior :";
                lblContraNueva.Text = "Nueva contraseña :";
                lblContraConfirma.Text = "Confirmar nueva contraseña :";
                txtContraNueva_PasswordStrength.TextStrengthDescriptions = "Muy Pobre; Debil; Medio; Fuerte; Excelente";
                btnEnviar.Text = "Enviar";
            }
            if (idioma == "Ingles")
            {
                lblContraseña.Text = "Change Password";
                lblDescripcion.Text = "Enter your username :";
                lblContraAnt.Text = "Previus password :";
                lblContraNueva.Text = "New password :";
                txtContraNueva_PasswordStrength.TextStrengthDescriptions = "Very Poor; Weak; Average; Strong; Excellent";
                lblContraConfirma.Text = "Confirm new contraseña :";
                btnEnviar.Text = "Send";
            }
            if (idioma == "Portugues")
            {
                lblContraseña.Text = "Mudar A Senha";
                lblDescripcion.Text = "Digite seu nome de usuario:";
                lblContraAnt.Text = "Anterior senha :";
                lblContraNueva.Text = "Nova senha :";
                txtContraNueva_PasswordStrength.TextStrengthDescriptions = "Mau;  Fraco; Medio; Forte; Excelente";
                lblContraConfirma.Text = "Confirmar nova senha :";
                btnEnviar.Text = "Enviar";
            }

            if (IsPostBack == true)
            {

            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string idioma = (string)Session["Idioma"];
            string contra = "", msj = "", email = "", msj2 = "", msj3 = "";
            //VERIFICAMOS EL NOMBRE DE USUARIO
            int existeLogin = CI.verificarLogin(txtNombre.Text);
            if (existeLogin == 1)
            {
                if (idioma == "Español")
                {
                    lblMSJ.Text = "El nombre de usuario no existe.";
                    lblMSJ.Visible = true;
                }
                if (idioma == "Ingles")
                {
                    lblMSJ.Text = "The username does not exists.";
                    lblMSJ.Visible = true;
                }
                if (idioma == "Portugues")
                {
                    lblMSJ.Text = "O nome de usuario não existe.";
                    lblMSJ.Visible = true;
                }                 
             }
             else
             {
                 //VERIFICAMOS LA CONTRASEÑA
                 string passwdOk = CI.verificarContrasena(txtNombre.Text, txtContraAnt.Text);
                 if (passwdOk == txtContraAnt.Text)
                 {
                     if (txtContraNueva.Text == txtContraConfirma.Text)
                     {
                         reader = CI.recuperarPass(txtNombre.Text);
                         reader.Read();
                         string rol = reader.GetValue(2).ToString();
                         reader.Close();

                         int actualizar = CI.CambiarContrasena(txtNombre.Text, txtContraAnt.Text, txtContraConfirma.Text);
                         
                         if (idioma == "Español")
                         {
                             msj = "Su contraseña a sido cambiada. " + "\n\n" + "Su nueva contraseña es " + txtContraConfirma.Text + "";
                             msj2 = "Recuerde que estamos aquí para apoyar sus labores." + "\n\n\n" + "Cordialmente," + "\n\n" + "Gestión Informática";
                             msj3 = "Contraseña cambiada.";
                         }
                         if (idioma == "Ingles")
                         {
                             msj = "Your password has been changed. " + "\n\n" + "Your new password is " + txtContraConfirma.Text + "";
                             msj2 = "Remenber we're here to support their work." + "\n\n\n" + "Cordially," + "\n\n" + "Gestión Informática";
                             msj3 = "Password changed.";
                         }
                         if (idioma == "Portugues")
                         {
                             msj = "Sua senha foi mudada. " + "\n\n" + "Sua nova senha é " + txtContraConfirma.Text + "";
                             msj2 = "Lembre-se de que estamos aquí para apoiar seu trabalhio." + "\n\n\n" + "Cordialmente," + "\n\n" + "Gestión Informática";
                             msj3 = "Senha mudada.";
                         }
                         
                         
                         if (rol == "3")
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
                             lblMSJ.Text = "Contraseña incorrecta.";
                             lblMSJ.Visible = true;
                         }
                         if (idioma == "Ingles")
                         {
                             lblMSJ.Text = "Incorrect password.";
                             lblMSJ.Visible = true;
                         }
                         if (idioma == "Portugues")
                         {
                             lblMSJ.Text = "Senha errada.";
                             lblMSJ.Visible = true;
                         }
                     }
                 }
                 else
                 {
                     if (idioma == "Español")
                     {
                         lblMSJ.Text = "Contraseña incorrecta.";
                         lblMSJ.Visible = true;
                     }
                     if (idioma == "Ingles")
                     {
                         lblMSJ.Text = "Incorrect password.";
                         lblMSJ.Visible = true;
                     }
                     if (idioma == "Portugues")
                     {
                         lblMSJ.Text = "Senha errada.";
                         lblMSJ.Visible = true;
                     }
                 }
             }

        }
    }
}