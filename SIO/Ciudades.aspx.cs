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
    public partial class Ciudades : System.Web.UI.Page
    {
        public SqlDataReader reader = null;
        public SqlDataReader readerevento = null;
        private DataSet dsPedido = new DataSet();
        public ControlPedido contpv = new ControlPedido();
        public ControlUbicacion contubi = new ControlUbicacion();
        public ControlCliente concli = new ControlCliente();
        public ControlEvento controlEvento = new ControlEvento();
        public ControlVisitaComercial CVC = new ControlVisitaComercial();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.cargarPaises();
            
        }

        private void cargarPaises()
        {
            string idioma = (string)Session["Idioma"];
            //cboPais.Items.Clear();

            cboPais.Items.Add(new ListItem("Seleccione El Pais", "0"));


            reader = contubi.poblarListaPais();

            if (reader != null)
            {
                while (reader.Read())
                {
                    cboPais.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            else
            {
                string mensaje = "";
                if (idioma == "Español")
                {
                    mensaje = "Usted no posee paises asociados.";
                }
                if (idioma == "Ingles")
                {
                    mensaje = "You have no partner countries.";
                }
                if (idioma == "Portugues")
                {
                    mensaje = "Você não tem países parceiros.";
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
            reader.Close();
        }

        private void cargarSubzonaPais()
        {
            string idioma = (string)Session["Idioma"];
            cboSubzona.Items.Clear();

            cboSubzona.Items.Add(new ListItem("Seleccione la Subzona", "0"));

            reader = contubi.poblarListaSubzona(Convert.ToInt32( cboPais.SelectedItem.Value));

            if (reader != null)
            {
                while (reader.Read())
                {
                    cboSubzona.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                    Session["Subzona"] = 1;

                    //if (cboSubzona.SelectedItem.Text.ToString() == "0")
                    //{
                    //    cboSubzona.Items.Clear();
                    //    cboSubzona.Items.Add(new ListItem("N/A", "0"));
                    //}
                }                
            }
            else
            {
                string mensaje = "";
                if (idioma == "Español")
                {
                    mensaje = "Usted no posee paises asociados.";
                }
                if (idioma == "Ingles")
                {
                    mensaje = "You have no partner countries.";
                }
                if (idioma == "Portugues")
                {
                    mensaje = "Você não tem países parceiros.";
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
            reader.Close();
        }

        protected void cargargrilla()
        {
            dsPedido.Reset();

            if (cboSubzona.SelectedItem.Value != "0")
            {
                dsPedido = controlEvento.ConsultarCiudadesxSubzona(Convert.ToInt32(cboPais.SelectedItem.Value),Convert.ToInt32( cboSubzona.SelectedItem.Value));
            }
            else
            {
                dsPedido = controlEvento.ConsultarCiudades(Convert.ToInt32(cboPais.SelectedItem.Value));
            }

            if (dsPedido != null)
            {
                GridView1.DataSource = dsPedido.Tables[0];
                GridView1.DataBind();
                GridView1.Visible = true;
            }
            else
            {
                GridView1.Dispose();
                GridView1.Visible = false;
            }
            dsPedido.Reset();

        }
       

        protected void cboPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarSubzonaPais();
            cargargrilla();
        }

        protected void btnGuardarsf_Click(object sender, EventArgs e)
        {
            string mensaje = "";
            string usuconect = (string)Session["Usuario"];

            if (txtNombre.Text != "")
            {

                if (btnGuardar.Text == "Guardar")
                {
                    txtNombre.Text = txtNombre.Text.ToUpperInvariant();
                    String menId = contubi.guardarCiudad(Convert.ToInt32(cboPais.SelectedItem.Value), Convert.ToInt32(cboSubzona.SelectedItem.Value), txtNombre.Text, usuconect);
                    if (menId.Substring(0, 1) != "E")//E <- de ERROR
                    {
                        mensaje = "Ciudad guardada exitosamente!!";
                        btnGuardar.Text = "Actualizar";
                        cargargrilla();
                    }
                    else
                    {
                        mensaje = "Hubo un error al guardar";
                    }
                }
                else
                    if (btnGuardar.Text == "Actualizar")
                    {

                        txtNombre.Text = txtNombre.Text.ToUpperInvariant();
                        int menId = contubi.actualizarCiudad(Convert.ToInt32(lblIdC.Text), Convert.ToInt32(cboPais.SelectedItem.Value), Convert.ToInt32(cboSubzona.SelectedItem.Value), txtNombre.Text, usuconect);
                        if (menId != -1)//-1 <- de ERROR
                        {
                            mensaje = "Ciudad actualizada exitosamente!!";
                            btnGuardar.Text = "Actualizar";
                            cargargrilla();
                        }
                        else
                        {
                            mensaje = "Hubo un error al guardar";
                        }
                    }
            }
            else
            {
                mensaje = "Digite el nombre de la Ciudad";
            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true); 
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ciudades.aspx");
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblIdC.Text= GridView1.SelectedRow.Cells[1].Text;
            btnGuardar.Text = "Actualizar";
            txtNombre.Text = GridView1.SelectedRow.Cells[2].Text;
            cboPais.SelectedValue = GridView1.SelectedRow.Cells[5].Text;
            cboSubzona.SelectedValue = GridView1.SelectedRow.Cells[3].Text;
        }

        protected void cboSubzona_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSubzona.SelectedItem.Value != "0")
            {
                cargargrilla();
            }
        }

       
    }
}