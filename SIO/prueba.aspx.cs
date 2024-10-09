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
using WhatsAppApi;
using System.Web.Script.Serialization;
using System.Xml;
using System.Diagnostics;

namespace SIO
{
    public partial class prueba : System.Web.UI.Page
    {
        // No need to change the following two lines unless you have a premium account
        private static string CLIENT_ID = "FREE_TRIAL_ACCOUNT";
        private static string CLIENT_SECRET = "PUBLIC_SECRET";

        private static string API_URL = "http://api.whatsmate.net/v1/whatsapp/queue/message";

        protected void Page_Load(object sender, EventArgs e)
        {

        }      
        
                //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    string entro = "entro";
        //    bool success = true;
        //    string para = txt_to.Text;
        //    string mensaje = txt_msg.Text;
        //    try
        //    {
        //        using (WebClient client = new WebClient())
        //        {
        //            client.Headers[HttpRequestHeader.ContentType] = "application/json";
        //            client.Headers["X-WM-CLIENT-ID"] = CLIENT_ID;
        //            client.Headers["X-WM-CLIENT-SECRET"] = CLIENT_SECRET;

        //            Payload payloadObj = new Payload() { number = para, message = mensaje };
        //            string postData = (new JavaScriptSerializer()).Serialize(payloadObj);

        //            string response = client.UploadString(API_URL, postData);
        //            Console.WriteLine(response);
        //        }
        //    }
        //    catch (WebException webEx)
        //    {
        //        Console.WriteLine(((HttpWebResponse)webEx.Response).StatusCode);
        //        Stream stream = ((HttpWebResponse)webEx.Response).GetResponseStream();
        //        StreamReader reader = new StreamReader(stream);
        //        String body = reader.ReadToEnd();
        //        Console.WriteLine(body);
        //        success = false;
        //    }

        //   // return success;

        //}

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    string tipo = "FUP";
        //    string bandera1 = Convert.ToString(Session["Bandera"]);
        //    if (bandera1 != "1") tipo = "PV";
        //    string mensaje;
        //    string usuario = Convert.ToString(Session["Nombre_Usuario"]);
        //    string Email = "";
        //    string Pais = Convert.ToString(Session["Pais"]);
        //    string OF = Convert.ToString(Session["OFParte"]);

        //    Byte[] correo = new Byte[0];
        //    WebClient clienteWeb = new WebClient();
        //    clienteWeb.Dispose();
        //    clienteWeb.Credentials = new System.Net.NetworkCredential("forsa", "F0rUs3s4*", "FORSA");

        //    string enlace = @"http://app.forsa.com.co/siomaestros/ReportViewer.aspx?/Logistica/LOG_ListaEmpaqueLineaNuevaGeneral&idofa=10541517&idioma=1";

        //    //correo = clienteWeb.DownloadData(@"http://10.75.131.2:81/ReportServer?/InformesFup/COM_SolicitudFacturacionSeguimientoNew&rs:format=PDF&rs:command=render&rs:ClearSession=true&numfup=" + lblfup.Text.Trim() + ""+
        //    //    "&version=" + LVer.Text.Trim() + "&parte=" + cboParte.SelectedItem.Text);

        //    correo = clienteWeb.DownloadData(enlace);

        //    MemoryStream ms = new MemoryStream(correo);




        //    //string memString = "Memory test string !!";
        //    //// convert string to stream
        //    //byte[] buffer = Encoding.ASCII.GetBytes(memString);
        //    //MemoryStream ms = new MemoryStream(buffer);
        //    ////write to file
        //    //FileStream file = new FileStream("d:\\file.txt", FileMode.Create, FileAccess.Write);
        //    //ms.WriteTo(file);
        //    //file.Close();
        //    //ms.Close();
        //}

        public class Payload
        {
            public string number { get; set; }
            public string message { get; set; }
        }


        //    string desde = "573192408804"; //(Enter Your Mobile Number)
        //    string to = txt_to.Text;
        //    string msg = txt_msg.Text;
        //    WhatsApp wa = new WhatsApp(desde, "351967071037050", "Iv", false, false);
        //    wa.OnConnectSuccess += () =>
        //    {
        //        proceso.Text = "Connected to WhatsApp...";
        //        wa.OnLoginSuccess += (phonenumber, data) =>
        //        {
        //            wa.SendMessage(to, msg);
        //            proceso.Text = "Message Sent...";
        //        };
        //        wa.OnLoginFailed += (data) =>
        //        {
        //            proceso.Text = "Login Failed : {0} : " + data;
        //        };

        //        wa.Login();
        //    };
        //    wa.OnConnectFailed += (Exception) =>
        //    {
        //        proceso.Text = "Connection Failed...";
        //    };   
        //}

        protected void Button1_Click(object sender, EventArgs e)
        {
            //ChangeValueOfKeyConfig(txt_to.Text);

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = "cmd";
            //process.StartInfo.WorkingDirectory = @"C:\inetpub\CargaSif"; //"~/FUP.aspx"
            process.StartInfo.WorkingDirectory = "~/CargaSif";

            process.StartInfo.Arguments = "/c " + "CargaSIIF.exe" + "";
            process.Start();
        }

        private void ChangeValueOfKeyConfig(string ProcessName)
        {
            // I locate the ProcessKiller Console Application in E drive.
            string tAppPath = @"C:\inetpub\CargaSif";

            string tProcessName = txt_to.Text;

            string tAppPathWithConfig = @"" + tAppPath + "ProcessKiller.exe.config";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(tAppPathWithConfig);

            foreach (XmlElement element in xmlDoc.DocumentElement)
            {
                if (element.Name == "appSettings")
                {
                    foreach (XmlNode node in element.ChildNodes)
                    {
                        RenameItemConfig(node, "ApplicationName", ProcessName);
                        //RenameItemConfig(node, "Other Key", "Other Value");
                    }
                }
            }

            xmlDoc.Save(tAppPathWithConfig);
        }

        private void RenameItemConfig(XmlNode node, string Key, string value)
        {
            if (node.Attributes.Item(0).Value == Key)
            {
                node.Attributes.Item(1).Value = value;
            }
        }
    }
}