using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using Microsoft.Reporting.WebForms;
using CapaControl;

namespace SIO
{
    public partial class ConsultaBdGerbo : System.Web.UI.Page
    {
        public SqlDataReader reader = null;
        public ControlConexionBd controlConexion = new ControlConexionBd();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.consultarConexion();
        }

        public void consultarConexion()
        {
            ////lblResulEstado.Text = "";

            lblResulDato.Text = "";
            //lblResulHora.Text = "";

            reader = controlConexion.consultarConexion("gerbo");

            if (reader != null)
            {
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        //lblResulEstado.Text = reader.GetString(0).ToString();
                        lblResulDato.Text = reader.GetString(1).ToString();
                        //lblResulHora.Text = reader.GetString(2).ToString();
                    }

                    reader.Close();
                    controlConexion.cerrarConexion();
                }

                else
                {
                    //lblResulEstado.Text = "Error Conexion";
                    lblResulDato.Text = "Error";
                    //lblResulHora.Text = "Error Conexion";
                }
            }
            else
            {
                //lblResulEstado.Text = "Error Conexion";
                lblResulDato.Text = "Error";
                //lblResulHora.Text = "Error Conexion";
            }

        }
    }
}