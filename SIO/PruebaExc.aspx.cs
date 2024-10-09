using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Security;
using System.Net.Mail;
using System.Net;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using CapaControl;
using System.Data.OleDb;
using System.Web.Script.Serialization; // requires the reference 'System.Web.Extensions'
using WhatsAppApi;
using System.Security.Policy;

namespace SIO
{
    public partial class PruebaExc : System.Web.UI.Page
    {
        //private static string API_URL = "http://api.whatsmate.net/v1/whatsapp/queue/message";
        
        // No need to change the following two lines unless you have a premium account
        private static string CLIENT_ID = "FREE_TRIAL_ACCOUNT";
        private static string CLIENT_SECRET = "PUBLIC_SECRET";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
            
        }       

         protected void enviar_Click(object sender, EventArgs e)
         {
            string tipo = "FUP";
            string bandera1 = Convert.ToString(Session["Bandera"]);
            if (bandera1 != "1") tipo = "PV";
            string mensaje;
            string usuario = Convert.ToString(Session["Nombre_Usuario"]);
            string Email = "";
            string Pais = Convert.ToString(Session["Pais"]);
            string OF = Convert.ToString(Session["OFParte"]);

            Byte[] correo = new Byte[0];
            WebClient clienteWeb = new WebClient();
            clienteWeb.Dispose();
            clienteWeb.Credentials = new System.Net.NetworkCredential("forsa", "F0rUs3s4*", "FORSA");

            string enlace = @"http://app.forsa.com.co/siomaestros/ReportViewer.aspx?/Logistica/LOG_ListaEmpaqueLineaNuevaGeneral&rs:format=PDF&idofa=10541517&idioma=1";

            //correo = clienteWeb.DownloadData(@"http://10.75.131.2:81/ReportServer?/InformesFup/COM_SolicitudFacturacionSeguimientoNew&rs:format=PDF&rs:command=render&rs:ClearSession=true&numfup=" + lblfup.Text.Trim() + ""+
            //    "&version=" + LVer.Text.Trim() + "&parte=" + cboParte.SelectedItem.Text);


            Response.ContentType = "text/xml";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.AppendHeader("NombreCabecera", "MensajeCabecera");
            Response.TransmitFile(Server.MapPath("~/tuRuta/TuArchivo.xml"));
            Response.End();


            //correo = clienteWeb.DownloadData(enlace);

            //MemoryStream ms = new MemoryStream(correo);
            //string ruta = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"c:\myfile.txt";

            //WebClient mywebClient = new WebClient();
            //clienteWeb.DownloadFileAsync(new Uri (enlace), @"\myfile.pdf");
        


        //string memString = "Memory test string !!";
        //// convert string to stream
        //byte[] buffer = Encoding.ASCII.GetBytes(memString);
        //MemoryStream ms = new MemoryStream(buffer);
        //write to file
        //FileStream file = new FileStream("c:\\file.xls", FileMode.Create, FileAccess.Write);
        //    ms.WriteTo(file);
        //    file.Close();
        //    ms.Close();

            //string from = "9199876543210"; //(Enter Your Mobile Number)
            //string to = txt_to.Text;
            //string msg = txt_msg.Text;
            //WhatsApp wa = new WhatsApp(from, "WhatsAppPassword", "NickName", false, false);
            //wa.OnConnectSuccess += () =>
            //{
            //    proceso.Text = "Connected to WhatsApp...";
            //    wa.OnLoginSuccess += (phonenumber, data) =>
            //    {
            //        wa.SendMessage(to, msg);
            //        proceso.Text = "Message Sent...";
            //    };
            //    wa.OnLoginFailed += (data) =>
            //    {
            //        proceso.Text = "Login Failed : {0} : " + data;
            //    };

            //    wa.Login();
            //};
            //wa.OnConnectFailed += (Exception) =>
            //{
            //    proceso.Text = "Connection Failed...";
            //};     
        }

        protected void txt_msg_TextChanged(object sender, EventArgs e)
         {
             string estado = "Entro";
         }

    
    }
}