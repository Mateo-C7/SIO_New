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
    public partial class GeneralXX : System.Web.UI.MasterPage
    {
        public SqlDataReader reader = null;
        public ControlInicio controlInicio = new ControlInicio();
        public ControlContacto controlCont = new ControlContacto();
        private DataSet dsHome = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //para temporizar y cerrar la sesion
                // Page.RegisterRedirectOnSessionEndScript();

                string idioma = (string)Session["Idioma"];
                string usuconect = (string)Session["Usuario"];
                int arRol = (int)Session["Rol"];
                string nomUsuario = "";
                this.Idioma();

                //consula los datos del usuario
                if ((arRol == 3) || (arRol == 9) || (arRol == 2) || (arRol == 28) || (arRol == 33))
                {
                    reader = controlInicio.ObtenerRepresentante(usuconect);
                    reader.Read();
                    // CARGANDO MENU Y SUBMENU DINAMICAMENTE
                    MenuItem item = new MenuItem();
                    MenuItem subitem = new MenuItem();

                    subitem.Text = "Cerrar Sesion";
                    subitem.NavigateUrl = "Inicio.aspx";
                    item.ChildItems.Add(subitem);

                    nomUsuario = reader.GetValue(1).ToString();
                    item.Text = reader.GetValue(1).ToString();
                    Menu2.Items.Add(item);
                    reader.Close();                   

                }
                else
                {
                    reader = controlInicio.consultarNombre(usuconect);
                    reader.Read();
                    //CARGANDO MENU Y SUBMENU DINAMICAMENTE
                    MenuItem item = new MenuItem();
                    MenuItem subitem = new MenuItem();

                    subitem.Text = "Cerrar Sesion";
                    subitem.NavigateUrl = "Inicio.aspx";
                    item.ChildItems.Add(subitem);

                    nomUsuario = reader.GetValue(0).ToString();
                    item.Text = reader.GetValue(0).ToString();
                    Menu2.Items.Add(item);
                    reader.Close();
                }

                ////activo solo para los de cotizaciones
                //if ((arRol == 24) || (arRol == 26))
                //{
                //    Menu1.Items[1].ChildItems[1].Enabled = true;
                //}

                if ((arRol == 2) || (arRol == 9) || (arRol == 30))
                {
                    Menu1.Items[1].ChildItems[3].ChildItems[1].Enabled = true;
                    Menu1.Items[1].ChildItems[3].ChildItems[0].Enabled = true;
                    Menu1.Items[1].ChildItems[3].ChildItems[2].Enabled = true;
                }
            }
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
                if (posicion == 1)
                    Menu1.Items[0].Text = fila[idiomaId].ToString();
                //if (posicion == 2)
                //    //Menu1.Items[1].Text = fila[idiomaId].ToString();
                //if (posicion == 3)
                //    Menu1.Items[2].Text = fila[idiomaId].ToString();
                //if (posicion == 4)
                //    Menu1.Items[3].Text = fila[idiomaId].ToString();
                //if (posicion == 5)
                //    Menu1.Items[4].Text = fila[idiomaId].ToString();
                //if (posicion == 6)
                //    Menu1.Items[5].Text = fila[idiomaId].ToString();
                //if (posicion == 7)
                //    Menu1.Items[0].ChildItems[0].Text = fila[idiomaId].ToString();
                //if (posicion == 8)
                //    Menu1.Items[0].ChildItems[1].Text = fila[idiomaId].ToString();
                //if (posicion == 9)
                //    Menu1.Items[0].ChildItems[2].Text = fila[idiomaId].ToString();
                //if (posicion == 10)
                //    //Menu1.Items[0].ChildItems[3].Text = fila[idiomaId].ToString();
                //if (posicion == 11)
                //    Menu1.Items[0].ChildItems[0].ChildItems[0].Text = fila[idiomaId].ToString();
                //if (posicion == 12)
                //    Menu1.Items[0].ChildItems[0].ChildItems[1].Text = fila[idiomaId].ToString();
                //if (posicion == 13)
                //    Menu1.Items[0].ChildItems[1].ChildItems[0].Text = fila[idiomaId].ToString();
                //if (posicion == 14)
                //    Menu1.Items[0].ChildItems[1].ChildItems[1].Text = fila[idiomaId].ToString();
                //if (posicion == 15)
                //    Menu1.Items[0].ChildItems[2].ChildItems[0].Text = fila[idiomaId].ToString();
                //if (posicion == 16)
                //    Menu1.Items[0].ChildItems[2].ChildItems[1].Text = fila[idiomaId].ToString();
                //if (posicion == 17)
                //    //Menu1.Items[0].ChildItems[3].ChildItems[0].Text = fila[idiomaId].ToString();
                //if (posicion == 18)
                //    Menu1.Items[0].ChildItems[3].ChildItems[1].Text = fila[idiomaId].ToString();
                //if (posicion == 20)
                //    //Menu1.Items[1].ChildItems[0].Text = fila[idiomaId].ToString();
                //if (posicion == 21)
                //    //Menu1.Items[1].ChildItems[0].ChildItems[0].Text = fila[idiomaId].ToString();
                //if (posicion == 22)
                //    //Menu1.Items[1].ChildItems[0].ChildItems[1].Text = fila[idiomaId].ToString();
                //if (posicion == 23)
                //    Menu1.Items[2].ChildItems[0].Text = fila[idiomaId].ToString();
                //if (posicion == 24)
                //    Menu1.Items[2].ChildItems[1].Text = fila[idiomaId].ToString();
                //if (posicion == 25)
                //    //Menu1.Items[2].ChildItems[2].Text = fila[idiomaId].ToString();
                //if (posicion == 26)
                //    Menu1.Items[2].ChildItems[1].ChildItems[0].Text = fila[idiomaId].ToString();
                //if (posicion == 27)
                //    Menu1.Items[2].ChildItems[1].ChildItems[1].Text = fila[idiomaId].ToString();
                //if (posicion == 28)
                //    //Menu1.Items[2].ChildItems[1].ChildItems[2].Text = fila[idiomaId].ToString();
                //if (posicion == 29)
                //    Menu1.Items[2].ChildItems[1].ChildItems[3].Text = fila[idiomaId].ToString();
                //if (posicion == 30)
                //    //Menu1.Items[2].ChildItems[1].ChildItems[4].Text = fila[idiomaId].ToString();
                //if (posicion == 31)
                //    Menu1.Items[2].ChildItems[2].ChildItems[0].Text = fila[idiomaId].ToString();
                //if (posicion == 32)
                //    //Menu1.Items[2].ChildItems[2].ChildItems[1].Text = fila[idiomaId].ToString();
                //if (posicion == 33)
                //    Menu1.Items[3].ChildItems[0].Text = fila[idiomaId].ToString();
                //if (posicion == 34)
                //    Menu1.Items[3].ChildItems[1].Text = fila[idiomaId].ToString();
                //if (posicion == 35)
                //    //Menu1.Items[3].ChildItems[0].ChildItems[0].Text = fila[idiomaId].ToString();
                //if (posicion == 36)
                //    Menu1.Items[3].ChildItems[0].ChildItems[1].Text = fila[idiomaId].ToString();
                //if (posicion == 37)
                //    //Menu1.Items[3].ChildItems[0].ChildItems[2].Text = fila[idiomaId].ToString();
                //if (posicion == 38)
                //    Menu1.Items[3].ChildItems[1].ChildItems[0].Text = fila[idiomaId].ToString();
                //if (posicion == 39)
                //    //Menu1.Items[3].ChildItems[1].ChildItems[1].Text = fila[idiomaId].ToString();
                //if (posicion == 40)
                //    Menu1.Items[4].ChildItems[0].Text = fila[idiomaId].ToString();
                //if (posicion == 41)
                //    Menu1.Items[4].ChildItems[1].Text = fila[idiomaId].ToString();
                //if (posicion == 42)
                //    Menu1.Items[4].ChildItems[2].Text = fila[idiomaId].ToString();
                //if (posicion == 43)
                //    Menu1.Items[4].ChildItems[2].ChildItems[0].Text = fila[idiomaId].ToString();
                //if (posicion == 44)
                //    Menu1.Items[5].ChildItems[0].Text = fila[idiomaId].ToString();
                //if (posicion == 45)
                //    Menu1.Items[5].ChildItems[1].Text = fila[idiomaId].ToString();
                //if (posicion == 46)
                //    //Menu1.Items[5].ChildItems[2].Text = fila[idiomaId].ToString();
                //if (posicion == 47)
                //    Menu1.Items[5].ChildItems[2].ChildItems[0].Text = fila[idiomaId].ToString();
                //if (posicion == 51)
                //    //Menu1.Items[6].Text = fila[idiomaId].ToString();
                //if (posicion == 52)
                //    Menu1.Items[6].ChildItems[0].Text = fila[idiomaId].ToString();
                //if (posicion == 53)
                //    //Menu1.Items[6].ChildItems[0].ChildItems[0].Text = fila[idiomaId].ToString(); 
                //if (posicion == 54)
                //    Menu1.Items[6].ChildItems[0].ChildItems[1].Text = fila[idiomaId].ToString(); 
                //if (posicion == 55)
                //    //Menu1.Items[6].ChildItems[1].Text = fila[idiomaId].ToString(); 
                //if (posicion == 56)
                //    Menu1.Items[6].ChildItems[1].ChildItems[0].Text = fila[idiomaId].ToString();               
            }
            dsHome.Tables.Remove("Table"); 
            dsHome.Dispose();
            dsHome.Clear();

        }
    }
}