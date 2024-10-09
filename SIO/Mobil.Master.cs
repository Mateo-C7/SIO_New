using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Net;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using CapaControl;

namespace SIO
{
    public partial class WebGeneral: System.Web.UI.MasterPage
    {
        public SqlDataReader reader = null;
        public ControlInicio controlInicio = new ControlInicio();
        public ControlContacto controlCont = new ControlContacto();
        private DataSet dsHome = new DataSet();
      
        protected void Page_Load(object sender, EventArgs e)
        {
           // this.Idioma();

        }

        private void Idioma()
        {
            int idiomaId = 0;
            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")
                idiomaId = 2;
            if (idioma == "Ingles")
                idiomaId = 3;
            if (idioma == "Portugues")
                idiomaId = 4;

            int posicion = 0;
            dsHome = controlInicio.ConsultarIdiomaHome();

            foreach (DataRow fila in dsHome.Tables[0].Rows)
            {
                posicion = posicion + 1;
                
                //if (posicion == 48)
                //    MenuMobil.Items[0].ChildItems[0].Text = fila[idiomaId].ToString();
                //if (posicion == 49)
                //    MenuMobil.Items[0].ChildItems[1].Text = fila[idiomaId].ToString();
                //if (posicion ==50)
                //    MenuMobil.Items[0].ChildItems[2].Text = fila[idiomaId].ToString();

            }
            dsHome.Tables.Remove("Table");
            dsHome.Dispose();
            dsHome.Clear();

        }
  
    }
}