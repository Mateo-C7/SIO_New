using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using CapaControl;
using System.Data;
using System.Web.Script.Services;
using System.Web.Services;

namespace SIO
{
    public partial class SiatPlaneacion : System.Web.UI.Page
    {
        private ControlPoliticas CP = new ControlPoliticas();
        private ControlSIAT CS = new ControlSIAT();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String ano = "";
                ano = CS.anoActual();
                lblAno.Text = ano;
                cargaPlan(lblAno.Text);
            }
        }
        //crea el calendario del los tenicos
        private String creaCalen(DateTime inicio, DateTime final)
        {
            String divD = "";//dias
            String divL = "";//letras
            String divT = "";//tecnico
            String divN = "";//numeros
            String tablaT = "<table class=styleTabla>";
            String tablaD = "<table class=styleTabla>";
            String tablaM = "";
            String trD = "";
            String trL = "";
            String trN = "";
            DataTable tec = CS.cargarTecnicos("");
            foreach (DataRow row in tec.Rows)
            {
                //  trD = trD + "<tr>";
                DateTime ini = inicio;
                DateTime fin = final;
                divD = "";
                divN = "";
                divL = "";
                trN = "";
                trL = "";
                while (ini <= fin)
                {
                    String espacio = "";
                    String dia = ini.Day.ToString().PadLeft(2, '0');
                    String mes = ini.Month.ToString().PadLeft(2, '0');
                    String ano = ini.Year.ToString();
                    String letra = (ini.ToString("dddd", new CultureInfo("es-ES"))).Substring(0, 1).ToUpper();
                    if (dia == "01") { espacio = "<div class=styleBarraMes> </div>"; }
                    divN = divN + "" + espacio + "<div class=styleDias id=" + ano + "." + mes + "." + dia + ".dia> " + dia + "</div> ";
                    divD = divD + "" + espacio + "<div class=styleDias id=" + row["cedulaEmp"].ToString() + "." + ano + "." + mes + "." + dia + ">&nbsp;</div> ";
                    divL = divL + "" + espacio + "<div class=styleDias id=" + ano + "." + mes + "." + dia + "." + letra + "> " + letra + "</div> ";
                    ini = ini.AddDays(1);
                }
                divT = "<td class=styleTdTecnico><div div class=styleTecnicos id=" + row["cedulaEmp"].ToString() + ">" + row["nomEmpComp"].ToString() + "</div></td>";
                trL = "<tr><td class=styleTdTecnico rowspan=2><div class=styleTitulo> TECNICOS </div></td><td>" + divL + "</td></tr>";
                trN = "<tr><td>" + divN + "</td></tr>";
                trD = trD + "<tr id=" + "tr." + row["cedulaEmp"].ToString() + "  title=" + row["nomEmpComp"].ToString().Replace(" ", "_") + " class=FilaCamColor onclick=selFilaCamColor(this); >" + divT + "<td>" + divD + "</td></tr>";
            }
            tablaT = "<div id=planTecnico>" + tablaT + trD + "</table></div>";
            tablaD = "<div id=planDias>" + tablaD + trL + trN + "</table></div>";
            tablaM = "<table class=styleTablaMeses> <tr> <td> <div class=styleMeses> <div class=styleMesesTitulo> </div> <div class=styleMes id=enero><span>Enero</span></div>  <div class=styleMes id=febrero><span>Febrero</span></div> "
            + " <div class=styleMes id=marzo><span>Marzo</span></div> <div class=styleMes id=abril><span>Abril</span></div>  <div class=styleMes id=mayo><span>Mayo</span></div>  <div class=styleMes id=junio><span>Junio</span></div> "
            + " <div class=styleMes id=julio><span>Julio</span></div> <div class=styleMes id=agosto><span>Agosto</span></div> <div class=styleMes id=septiembre><span>Septiembre</span></div> <div class=styleMes id=octubre><span>Octubre</span></div> "
            + " <div class=styleMes id=noviembre><span>Noviembre</span></div>  <div class=styleMes id=diciembre><span>Diciembre</span></div>  </div> </td> </tr> </table>";
            return tablaM + tablaD + tablaT;
        }
        //llena los datos de la planeacion
        private String llenarPlan(String anoCal)
        {
            String plan = "";
            String colorD = "";//color del dia
            String titleD = "";//titulo del dia, osea la observacion
            String letraD = "";//letra del dia, osea el valor=value
            String idD = "";//id del dia, osea el id=value, para cambiar el id del div
            String onclick = "";
            DataTable tec = CS.cargarPlaneacionAct(anoCal);
            foreach (DataRow row in tec.Rows)
            {
                DateTime inicio = DateTime.Parse(row["fechaIni"].ToString());
                DateTime fin = DateTime.Parse(row["fechaFin"].ToString());
                String color = row["color"].ToString();
                String title = row["obs"].ToString();
                String letra = row["letra"].ToString();
                String idAct = row["idAct"].ToString();
                String tipoAct = row["tipoAct"].ToString();
                while (inicio <= fin)
                {
                    String dia = inicio.Day.ToString().PadLeft(2, '0');
                    String mes = inicio.Month.ToString().PadLeft(2, '0');
                    String ano = inicio.Year.ToString();
                    String datoD = row["tec"].ToString() + "." + ano + "." + mes + "." + dia;
                    colorD = colorD + "document.getElementById('" + datoD + "').style.background ='" + color + "';";
                    titleD = titleD + "document.getElementById('" + datoD + "').title ='" + title + "';";
                    letraD = letraD + "document.getElementById('" + datoD + "').innerHTML ='<span>" + letra + "</<span>';";
                    idD = idD + "document.getElementById('" + datoD + "').id = '" + tipoAct + "." + idAct + "." + datoD + "';";
                    onclick = onclick + "document.getElementById('" + tipoAct + "." + idAct + "." + datoD + "').setAttribute('onClick', 'abrirVentana(this);');";
                    inicio = inicio.AddDays(1);
                }
            }
            plan = colorD + titleD + letraD + idD + onclick;
            return plan;
        }
        //carga las actividades
        private String cargaAct()
        {
            String divA = "";
            String tablaA = "<table class=styleTablaAct><tr><td>";
            DataTable act = CS.cargarActividades();
            foreach (DataRow row in act.Rows)
            {
                divA = divA + "<div class=styleAct id=" + row["idAct"].ToString() + " style=background:" + row["color"].ToString() + ";>&nbsp;&nbsp;" + row["nomAct"].ToString() + ":" + row["letra"].ToString().ToUpper() + "&nbsp;&nbsp;</div>";
            }
            tablaA = tablaA + divA + "</td></tr></table>";
            return tablaA;
        }
        //carga todo la planeacion por completo
        private void cargaPlan(String ano)
        {
            String tabla = creaCalen(DateTime.Parse("01/01/" + ano + ""), DateTime.Parse("31/12/" + ano + ""));
            String activ = cargaAct();
            String datos = llenarPlan(ano);
            String div = "";
            div = " $(document).ready(function () { document.getElementById('actividad').innerHTML ='" + activ + "'; document.getElementById('planGen').innerHTML ='" + tabla + "'; " + datos + "  });";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script800", div, true);
        }
        //cambia el año a hacia atras
        protected void btnImaAtras_Click(object sender, ImageClickEventArgs e)
        {
            int ano = int.Parse(lblAno.Text);
            ano = ano - 1;
            lblAno.Text = ano.ToString();
            cargaPlan(ano.ToString());
        }
        //cambia el año a hacia delante
        protected void btnImaSig_Click(object sender, ImageClickEventArgs e)
        {
            int ano = int.Parse(lblAno.Text);
            ano = ano + 1;
            lblAno.Text = ano.ToString();
            cargaPlan(ano.ToString());
        }
    }
}