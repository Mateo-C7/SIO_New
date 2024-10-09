using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaControl;

namespace SIO
{
    public partial class ActaTrazabilidad : System.Web.UI.Page
    {
        public ControlLogistica CL = new ControlLogistica();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["idTrans"].ToString() != "" && Session["idTrans"].ToString() != null)
            {
                int pesoB = int.Parse((string)Session["pesoB"]);
                int pesoP = int.Parse((string)Session["pesoP"]);
                int pesoNeto = pesoB - pesoP;
                lblPesoB.Text = pesoB.ToString();
                lblPesoN.Text = pesoNeto.ToString();
                int idTrans = int.Parse(Session["idTrans"].ToString());
                GridView2.DataSource = CL.cargarGridTraza1(idTrans);
                GridView2.DataBind();
                GridView3.DataSource = CL.cargaGridTraza2(idTrans);
                GridView3.DataBind();
                if (Session["placa"].ToString() != null && Session["placa"].ToString() != "")
                {
                    lblPlaca.Text = (string)Session["placa"];
                    Session["1"] = "";
                }
                else
                {
                    form1.Visible = false;
                    Session["1"] = "(placa) ";
                }
                //
                if (Session["numConte"].ToString() != null && Session["numConte"].ToString() != "")
                {
                    lblNumContenedor.Text = (string)Session["numConte"];
                    Session["2"] = "";
                }
                else
                {
                    form1.Visible = false;
                    Session["2"] = "(numero contenedor) ";
                }
                //
                if (Session["inicioCargue"].ToString() != null && Session["inicioCargue"].ToString() != "")
                {
                    lblInicioCargue.Text = (string)Session["inicioCargue"];
                    Session["3"] = "";
                }
                else
                {
                    form1.Visible = false;
                    Session["3"] = "(hora inicio) ";
                }
                //
                if (Session["finCargue"].ToString() != null && Session["finCargue"].ToString() != "")
                {
                    lblFinCargue.Text = (string)Session["finCargue"];
                    Session["4"] = "";
                }
                else
                {
                    form1.Visible = false;
                    Session["4"] = "(hora fin) ";
                }
                //
                if (Session["pais"].ToString() != null && Session["pais"].ToString() != "")
                {
                    lblPais.Text = (string)Session["pais"];
                    Session["5"] = "";
                }
                else
                {
                    form1.Visible = false;
                    Session["5"] = "(pais) ";
                }
                //
                if (Session["fechaImp"].ToString() != null && Session["fechaImp"].ToString() != "")
                {
                    lblFechaImp.Text = (string)Session["fechaImp"];
                    Session["6"] = "";
                }
                else
                {
                    form1.Visible = false;
                    Session["6"] = "(fecha impresion) ";
                }
                //
                if (Session["llegadaC"].ToString() != null && Session["llegadaC"].ToString() != "")
                {
                    lblHoraLlegada.Text = (string)Session["llegadaC"];
                    Session["7"] = "";
                }
                else
                {
                    form1.Visible = false;
                    Session["7"] = "(hora de llegada) ";
                }
                //
                if (Session["salidaC"].ToString() != null && Session["salidaC"].ToString() != "")
                {
                    lblHoraSalida.Text = (string)Session["salidaC"];
                    Session["8"] = "";
                }
                else
                {
                    form1.Visible = false;
                    Session["8"] = "(hora de salida) ";
                }
                //
                if (Session["cantidadByP"].ToString() != null && Session["cantidadByP"].ToString() != "")
                {
                    lblCantByP.Text = (string)Session["cantidadByP"];
                    Session["9"] = "";
                }
                else
                {
                    form1.Visible = false;
                    Session["9"] = " (cantidad de pallets) ";
                }
                //
                if (Session["tipoExpo"].ToString() != null && Session["tipoExpo"].ToString() != "")
                {
                    String tipoExpo = (string)Session["tipoExpo"];
                    if (tipoExpo.ToString() == "False")
                    {
                        String Expo = "EXPORTACION";
                        lblTipoExpo.Text = Expo;
                        Session["10"] = "";
                    }
                    else
                    {
                        String Expo1 = "NACIONAL";
                        lblTipoExpo.Text = Expo1;
                        Session["10"] = "";
                    }
                }
                else
                {
                    form1.Visible = false;
                    Session["10"] = "(tipo de exportacion) ";
                }
                //
                if (Session["tipoTrans"].ToString() != null && Session["tipoTrans"].ToString() != "")
                {
                    String tipoTrans = (string)Session["tipoTrans"];
                    lblTipoTrans.Text = tipoTrans.ToUpper();
                    Session["11"] = "";
                }
                else
                {
                    form1.Visible = false;
                    Session["11"] = "(tipo de transporte) ";
                }
                //
                if (Session["numTrailer"].ToString() != null && Session["numTrailer"].ToString() != "")
                {
                    lblNumTrailer.Text = (string)Session["numTrailer"];
                    Session["12"] = "";
                }
                else
                {
                    form1.Visible = false;
                    Session["12"] = "(numero del trailer) ";
                }
                //
                if (Session["nombreLlegada"].ToString() != null && Session["nombreLlegada"].ToString() != "")
                {
                    lblNomLLegada.Text = (string)Session["nombreLlegada"];
                    Session["13"] = "";
                }
                else
                {
                    form1.Visible = false;
                    Session["13"] = "(nombre del precinto llegada) ";
                }
                //
                if (Session["nombreSalida"].ToString() != null && Session["nombreSalida"].ToString() != "")
                {
                    lblNomSalida.Text = (string)Session["nombreSalida"];
                    Session["14"] = "";
                }
                else
                {
                    form1.Visible = false;
                    Session["14"] = "(nombre del precinto salida) ";
                }
                //
                if (Session["precintoLlegada"].ToString() != null && Session["precintoLlegada"].ToString() != "")
                {
                    lblPrecintoL.Text = (string)Session["precintoLlegada"];
                    Session["15"] = "";
                }
                else
                {
                    form1.Visible = false;
                    Session["15"] = "(precinto llegada) ";
                }
                //
                if (Session["precintoSalida"].ToString() != null && Session["precintoSalida"].ToString() != "")
                {
                    lblPrecintoS.Text = (string)Session["precintoSalida"];
                    Session["16"] = "";
                }
                else
                {
                    form1.Visible = false;
                    Session["16"] = "(precinto salida) ";
                }
                //
                if (Session["1"].ToString() == "" && Session["2"].ToString() == "" && Session["3"].ToString() == "" && Session["4"].ToString() == "" && Session["5"].ToString() == "" && Session["6"].ToString() == "" && Session["7"].ToString() == "" && Session["8"].ToString() == "" && Session["9"].ToString() == "" && Session["10"].ToString() == "" && Session["11"].ToString() == "" && Session["12"].ToString() == "" && Session["13"].ToString() == "" && Session["14"].ToString() == "" && Session["15"].ToString() == "" && Session["16"].ToString() == "")
                {
                    form1.Visible = true;
                }
                else
                {
                    lblMensaje.Text = "No se puede mostrar hace falta los datos : " + Session["1"].ToString() + Session["2"].ToString() + Session["3"].ToString() + Session["4"].ToString() + Session["5"].ToString() + Session["6"].ToString() + Session["7"].ToString() + Session["8"].ToString() + Session["9"].ToString() + Session["10"].ToString() + Session["11"].ToString() + Session["12"].ToString() + Session["13"].ToString() + Session["14"].ToString() + Session["15"].ToString() + Session["16"].ToString();
                }
            }
            else
            {
                Session["17"] = "Por favor selecione la placa";
            }
        }
        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            mataSesiones();
        }
        public void mataSesiones()
        {
            Session["placa"] = "";
            Session["numConte"] = "";
            Session["idTrans"] = "";
            Session["inicioCargue"] = "";
            Session["finCargue"] = "";
            Session["llegadaC"] = "";
            Session["salidaC"] = "";
            Session["pais"] = "";
            Session["orden2"] = "";
            Session["tipoExpo"] = "";
            Session["tipoTrans"] = "";
            Session["numTrailer"] = "";
            Session["nombreLlegada"] = "";
            Session["nombreSalida"] = "";
            Session["precintoLlegada"] = "";
            Session["precintoSalida"] = "";
        }
    }
}