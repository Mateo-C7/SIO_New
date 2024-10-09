using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Security;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using CapaControl;


namespace SIO
{
    public partial class Fletes : System.Web.UI.Page
    {
        public SqlDataReader reader = null;
        private DataSet ds = new DataSet();
        public ControlFlete controlFlete = new ControlFlete();
        public SqlDataReader readerCiudad = null;
        public ControlContacto controlCont = new ControlContacto();
        ControlUbicacion controlUbi = new ControlUbicacion();        

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int arRol = (int)Session["Rol"];
                this.poblarListaCiudad(8);
                this.cargarMinimos();
                this.CargarReporteFletes();

                if (arRol == 15)
                {
                    btnActFletMin.Visible = true;
                    btnActValorCiuda.Visible = true;
                    btnActValorCiuda0.Visible = true;
                }
            }            
        }

        private void CargarReporteFletes()
        {
            string Estado = (string)Session["EstadoCom"];

            this.ReportViewer2.KeepSessionAlive = true;
            this.ReportViewer2.AsyncRendering = true;
            ReportViewer2.Visible = true;

            ReportViewer2.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            ReportViewer2.ServerReport.ReportServerCredentials = irsc;
            ReportViewer2.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            ReportViewer2.ServerReport.ReportPath = "/Logistica/LOG_Fletes";           
        }

        public void cargarMinimos()
        {
            decimal fletemin = 0,seguromin=0, tarifamin;

            reader = controlFlete.ConsultarMinimos();
            reader.Read();

            lblIdFlete.Text = Convert.ToString(reader.GetValue(0));
            fletemin =Convert.ToDecimal( reader.GetValue(1));
            seguromin = Convert.ToDecimal(reader.GetValue(2));
            txtTolerancia.Text = reader.GetValue(4).ToString();

            tarifamin = fletemin + seguromin;

            txtFleteMin.Text = fletemin.ToString();
            txtSeguroMin.Text = seguromin.ToString();

            lblTarifaMinima.Text = Convert.ToString(tarifamin.ToString("#,##.##")); 
            
            reader.Close();
        }

        //CARGAMOS LAS CIUDADES DEL PAIS SELECCIONADO
        private void poblarListaCiudad(int pais_id)
        {
            cboCiudades.Items.Clear();          

            cboCiudades.Items.Add(new ListItem("Seleccione la Ciudad", "0"));

            readerCiudad = controlUbi.poblarListaCiudades(Convert.ToInt32(pais_id));

            if (readerCiudad.HasRows == true)
                {
                    while (readerCiudad.Read())
                    {
                        cboCiudades.Items.Add(new ListItem(readerCiudad.GetString(1), readerCiudad.GetInt32(0).ToString()));
                    }
                }
                readerCiudad.Close();
            }

        protected void btnActFletMin_Click(object sender, EventArgs e)
        {
            //decimal fleteminimo = Convert.ToDecimal(txtFleteMin.Text);
            //decimal segurominimo = Convert.ToDecimal(txtSeguroMin.Text);
            string mensaje = "";

            int actualizacion = controlFlete.ActualizarFleteMin(Convert.ToInt32(lblIdFlete.Text), txtFleteMin.Text, txtSeguroMin.Text);

            if (actualizacion > 0)
            {
                mensaje = "Se actualizaron los valores exitosamente";
                this.cargarMinimos();
                //guardo en el log de fletes
                controlFlete.InsertarLog("Flete Minimo", Convert.ToDecimal(txtFleteMin.Text), Session["Nombre_Usuario"].ToString());
                controlFlete.InsertarLog("Seguro Minimo", Convert.ToDecimal(txtSeguroMin.Text), Session["Nombre_Usuario"].ToString());
            }
            else
            {
                mensaje = "Se actualizaron los valores exitosamente";            
            }
         
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true); 
        }

        public static Boolean IsNumeric(string precio)
        {
            decimal result;
            return decimal.TryParse(precio, out result);
        }

        protected void txtFleteMin_TextChanged(object sender, EventArgs e)
        {
            string mensaje = "";
            bool numerico = IsNumeric(txtFleteMin.Text);

            if (numerico == false)
            {
                mensaje = "Digite el valor en números";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                txtFleteMin.Text = "0";
            }                
        }

        protected void txtSeguroMin_TextChanged(object sender, EventArgs e)
        {
            string mensaje = "";
            bool numerico = IsNumeric(txtSeguroMin.Text);

            if (numerico == false)
            {
                mensaje = "Digite el valor en números";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                txtSeguroMin.Text = "0";
            }               
        }

        protected void cboCiudades_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ciudad = Convert.ToInt32(cboCiudades.SelectedItem.Value);

            if (ciudad != 0)
            {
                reader = controlFlete.ConsultarFleteCiudad(ciudad);
                reader.Read();
                txtValorCiudad.Text = reader.GetValue(2).ToString();
                reader.Close();
            }
        }

        protected void txtValorCiudad_TextChanged(object sender, EventArgs e)
        {
            string mensaje = "";
            bool numerico = IsNumeric(txtValorCiudad.Text);

            if (numerico == true)
            {
                decimal valor = Convert.ToDecimal(txtValorCiudad.Text);

                if (valor < 0)
                {
                    mensaje = "Digite el valor correctamente";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    txtValorCiudad.Text = "0";
                }
            }
            else
            {
                mensaje = "Digite el valor correctamente";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                txtValorCiudad.Text = "0"; 
            }
        }

        protected void btnActValorCiuda_Click(object sender, EventArgs e)
        {
            string mensaje = "";
            int ciudad = Convert.ToInt32(cboCiudades.SelectedItem.Value);

            if (ciudad != 0)
            {
                int resultado = controlFlete.ActualizarValorCiudad(Convert.ToInt32(cboCiudades.SelectedItem.Value), txtValorCiudad.Text,Session["Nombre_Usuario"].ToString());

                if (resultado > 0)
                {
                    mensaje = "Valor actualizado exitosamente";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    this.cboCiudades_SelectedIndexChanged(sender,e);
                    //guardo en el log de fletes
                    controlFlete.InsertarLog(cboCiudades.SelectedItem.Text, Convert.ToDecimal(txtValorCiudad.Text), Session["Nombre_Usuario"].ToString());
                    this.CargarReporteFletes();
                }
                else
                {
                    mensaje = "No fue posible la actualizacion";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
            }
            else
            {
                mensaje = "Seleccione la ciudad";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);                
            }          
        }

        protected void btnActValorCiuda0_Click(object sender, EventArgs e)
        {
            string mensaje = "";
            decimal tolerancia = Convert.ToDecimal(txtTolerancia.Text);

            if (tolerancia >= 0)
            {
                int resultado = controlFlete.ActualizarTolerancia(tolerancia);

                if (resultado > 0)
                {
                    mensaje = "Tolerancia actualizada exitosamente";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    this.cboCiudades_SelectedIndexChanged(sender, e);
                    this.cargarMinimos();
                    //guardo en el log de fletes
                    controlFlete.InsertarLog("Tolerancia", Convert.ToDecimal(txtTolerancia.Text), Session["Nombre_Usuario"].ToString());
                
                }
                else
                {
                    mensaje = "No fue posible la actualizacion";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
            }
            else
            {
                mensaje = "El valor debe ser mayor o igual a 0";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }          
        }

        protected void txtTolerancia_TextChanged(object sender, EventArgs e)
        {
            string mensaje = "";
            bool numerico = IsNumeric(txtTolerancia.Text);

            if (numerico == true)
            {
                decimal valor = Convert.ToDecimal(txtTolerancia.Text);

                if (valor < 0)
                {
                    mensaje = "Digite el valor correctamente";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    txtTolerancia.Text = "0";
                }
            }
            else
            {
                mensaje = "Digite el valor correctamente";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                txtTolerancia.Text = "0";
            }
        }
        
    }          
    
}