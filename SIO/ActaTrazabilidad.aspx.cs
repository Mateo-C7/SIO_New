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
                lblPesoB.Text = (string)Session["pesoB"];
                lblPesoN.Text = (string)Session["pesoP"];
                lblVol.Text = (string)Session["volumen"];
                int idTrans = int.Parse(Session["idTrans"].ToString());
                GridView2.DataSource = CL.cargarGridTraza1(idTrans);
                GridView2.DataBind();
                GridView3.DataSource = CL.cargaGridTraza2(idTrans);
                GridView3.DataBind();
                String placa = validar((string)Session["placa"], "(placa) ", lblPlaca);
                String numConte = validar((string)Session["numConte"], "numero contenedor) ", lblNumContenedor);
                String inicioCargue = validar((string)Session["inicioCargue"], "(hora inicio) ", lblInicioCargue);
                String finCargue = validar((string)Session["finCargue"], "(hora fin) ", lblFinCargue);
                String pais = validar((string)Session["pais"], "(pais) ", lblPais);
                String fechaImp = validar((string)Session["fechaImp"], "(fecha impresion) ", lblFechaImp);
                String llegadaC = validar((string)Session["llegadaC"], "(hora de llegada) ", lblHoraLlegada);
                String cantidadByP = validar((string)Session["cantidadByP"], "(cantidad de pallets) ", lblCantByP);
                String tipoTranss = validar((string)Session["tipoTrans"].ToString().ToUpper(), "(tipo de transporte) ", lblTipoTrans);
                String numTrailer = validar((string)Session["numTrailer"], "(numero del trailer) ", lblNumTrailer);
                String nombreL = validar((string)Session["nombreLlegada"], "(nombre del precinto llegada) ", lblNomLLegada);
                String nombreS = validar((string)Session["nombreSalida"], "(nombre del precinto salida) ", lblNomSalida);
                String precintoL = validar((string)Session["precintoLlegada"], "(precinto llegada) ", lblPrecintoL);
                String precintoS = validar((string)Session["precintoSalida"], "(precinto salida) ", lblPrecintoS);
                String nomCond = validar((string)Session["nomCond"], "(nombre del conductor) ", lblNomCon);
                String nomEmp = validar((string)Session["nomEmp"], "(nombre de la empresa) ", lblNomEm);
                String ccCod = validar((string)Session["ccCond"], "(cedula del conductor) ", lblCC);
                String tel = validar((string)Session["tel"], "(telefono del conductor) ", lblTel);
                String otroPre = validar((string)Session["otroPre"], "(otros precintos) ", lblOtroPre);
                //
                if (lblInicioCargue.Text != "" && lblFinCargue.Text != "")
                {
                    DateTime dt1, dt2;
                    dt1 = DateTime.Parse(lblInicioCargue.Text);
                    dt2 = DateTime.Parse(lblFinCargue.Text);
                    TimeSpan ts = dt2 - dt1;
                    lblTH.Text = ts.Hours.ToString();
                    lblTM.Text = ts.Minutes.ToString();
                    Session["1"] = "";
                }
                else
                {
                    form1.Visible = false;
                    Session["1"] = "(total horas ) ";
                }
                //
                if (Session["tipoExpo"].ToString() != null && Session["tipoExpo"].ToString() != "")
                {
                    String tipoExpo = (string)Session["tipoExpo"];
                    if (tipoExpo.ToString() == "False")
                    {
                        String Expo = "EXPORTACION";
                        lblTipoExpo.Text = Expo;
                        Session["2"] = "";
                    }
                    else
                    {
                        String Expo1 = "NACIONAL";
                        lblTipoExpo.Text = Expo1;
                        Session["2"] = "";
                    }
                }
                else
                {
                    form1.Visible = false;
                    Session["2"] = "(tipo de exportacion) ";
                }
                //---------------------------------
                if (placa == "" && numConte == "" && inicioCargue == "" && finCargue == "" && pais == "" && fechaImp == "" && llegadaC == "" && Session["1"].ToString() == "" && cantidadByP == "" && Session["2"].ToString() == "" && tipoTranss == "" && numTrailer == "" && nombreL == "" && nombreS == "" && precintoL == "" && precintoS == "" && nomCond == "" && nomEmp == "" && ccCod == "" && tel == "" && otroPre == "")
                {
                    form1.Visible = true;
                }
                else
                {
                    lblMensaje.Text = "No se puede mostrar hace falta los datos : " + placa + numConte + inicioCargue + finCargue + pais + fechaImp + llegadaC + Session["1"].ToString() + cantidadByP + Session["2"].ToString() + tipoTranss + numTrailer + nombreL + nombreS + precintoL + precintoS + nomCond + nomEmp + ccCod + tel + otroPre;
                }
            }
            else
            {
                lblMensaje.Text = "Por favor selecione la placa";
            }
        }

        public String validar(String var, String retorna, Label lbl)  {
            String men;
            if (var != null && var != "")
            {
                lbl.Text = var;
                men = "";
            }
            else
            {
                form1.Visible = false;
                men = retorna;
            }
            return men;
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