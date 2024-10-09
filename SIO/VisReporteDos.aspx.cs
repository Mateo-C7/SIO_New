using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using CapaControl;
using System.Data;
using CapaDatos;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;
using System.Diagnostics;
using System.Threading;
using System.Net.Mail;
using System.Text;
using System.Globalization;
using System.Configuration;
using System.Web.Services;
using System.Web.Script.Services;
using Microsoft.Reporting.WebForms;

namespace SIO
{
    public partial class ReporteVisDos : System.Web.UI.Page
    {
        private ControlVisitaComercial CVC = new ControlVisitaComercial();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cboTipoVis.SelectedIndex = 0;
                CargarTablaVisDet("");
            }
        }
        //Combo de tipo de visitas
        protected void cboTipoVis_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipoVis.SelectedItem.ToString() == "Seleccionar")
            {
                mensajeVentana("Por favor seleccione un tipo de visita, gracias!");
            }
            else if (cboTipoVis.SelectedItem.ToString() == "Todas")
            {
                CargarTablaVisDet(" ");
            }
            else if (cboTipoVis.SelectedItem.ToString() == "Realizadas")
            {
                CargarTablaVisDet("  AND (mvc.vis_dfecha_cierre IS NOT NULL)  ");
            }
            else if (cboTipoVis.SelectedItem.ToString() == "No Realizadas")
            {
                CargarTablaVisDet("  AND (mvc.vis_dfecha_cierre IS NULL)  AND (mvc.vis_cancelada = 0)  ");
            }
            else if (cboTipoVis.SelectedItem.ToString() == "Canceladas")
            {
                CargarTablaVisDet("  AND (mvc.vis_cancelada = 1)  ");
            }

            tablaDetallado.Visible = true;
        }
        //Carga la tabla detallada
        private void CargarTablaVisDet(String filtro)
        {
            DataTable detallado = CVC.cargarTablaDetVis(Session["fechaIni"].ToString(), Session["fechaFin"].ToString(), "  AND (mvc.vis_usu_ejecuta = '" + Session["usuVisitaPopUp"].ToString() + "')  ", filtro, Session["cliente"].ToString());
            grdDetallado.DataSource = detallado;
            grdDetallado.DataBind();
            int conReali = 0;
            int conNoReali = 0;
            int conCance = 0;
            foreach (DataRow row in detallado.Rows)
            {
                if (row["realizada"].ToString() == "S")
                {
                    conReali = conReali + 1;
                }
                else if (row["realizada"].ToString() == "N")
                {
                    conNoReali = conNoReali + 1;
                }
                else
                {
                    conCance = conCance + 1;
                }
            }
            String grafica = "var pieData = [{value: " + conReali + ",color:'#0b82e7',highlight: '#0c62ab',label: 'Realizados'},{value: " + conNoReali + ",color: '#e3e860',highlight: '#a9ad47',label: 'No Realizados'},{value: " + conCance + ",color: '#E02E35',highlight: '#B66DEA',label: 'Canceladas'},]; var ctx = document.getElementById('chart-area').getContext('2d'); window.myPie = new Chart(ctx).Pie(pieData)";
            ClientScript.RegisterStartupScript(GetType(), "script7", grafica, true);
            // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script3", grafica, true);
        }
        //Maneja el mensaje por medio de script
        private void mensajeVentana(String mensaje)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + mensaje + "')", true);
        }
        //Recorre la tabla Detallada para obtener el id de la visita y abrir el popup de la informacion
        protected void grdDetallado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "notAgen")//Identifico el comand de la fila
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdDetallado.Rows[index];
                String idVis = row.Cells[0].Text;
                String div = nota(idVis);
                ClientScript.RegisterStartupScript(GetType(), "script8", div, true);
            }
            else if (e.CommandName == "datosEje")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdDetallado.Rows[index];
                String idVis = row.Cells[0].Text;
                String div = datosEje(idVis);
                ClientScript.RegisterStartupScript(GetType(), "script9", div, true);
            }//datosCierre
            else if (e.CommandName == "datosCierre")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdDetallado.Rows[index];
                String idVis = row.Cells[0].Text;
                String div = datosCierre(idVis);
                ClientScript.RegisterStartupScript(GetType(), "script10", div, true);
            }
            else if (e.CommandName == "datosDoc")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdDetallado.Rows[index];
                String idVis = row.Cells[0].Text;
                datosDoc(int.Parse(idVis));
                // ClientScript.RegisterStartupScript(GetType(), "script15", "popupDoc();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script16", "popupDoc();", true);
            }
        }
        //Creacion del pop up para la nota
        private String nota(String idVisita)
        {
            String div = "";
            String obj = CVC.cargarNota(idVisita);
            String tr = "<div class=close><a href=# id=close><img src=iconosMetro/close.png></a></div><div><table><tr><td><div>Objetivo : </div> </td> <td><div>" + obj + "</div> </td> </tr></table><div>";
            div = "$(document).ready(function () { document.getElementById('popup').innerHTML  = '" + tr + "';   $('#popup').fadeIn('slow');  $('#fondoOsc').fadeIn('slow'); $('#close').click(function () {  $('#popup').fadeOut('slow'); $('#fondoOsc').fadeOut('slow'); return false; });   });";
            return div;
        }
        //Creacion del pop up para los datos del ejecutante
        private String datosEje(String idVisita)
        {
            String div = "";
            DataTable consulta = null;
            String tb = "<div class=close><a href=# id=close2><img src=iconosMetro/close.png></a></div><div><table>";
            String tr = "";
            String tr2 = "";
            String tbCom = "";
            consulta = CVC.cargarDatosEje(idVisita);
            foreach (DataRow row in consulta.Rows)
            {
                tr = tr + "<tr> <td><div>Actividad : " + row["nomAct"].ToString() + " </div></td> <td class=styleTd></td> <td><div>Responsable: " + row["nomRes"].ToString() + " </div> </td> </tr>";
                tr2 = " <tr><td align=center colspan=3><div>Conclucion : " + row["conclucion"].ToString() + "</div> </td> </tr> </table></div>";
            }
            tbCom = tb + tr + tr2;
            div = "$(document).ready(function () { document.getElementById('popup2').innerHTML  = '" + tbCom + "';   $('#popup2').fadeIn('slow'); $('#fondoOsc').fadeIn('slow'); $('#close2').click(function () {  $('#popup2').fadeOut('slow');  $('#fondoOsc').fadeOut('slow'); return false; });   });";
            return div;
        }
        //Creacion del pop up para los datos del cierre
        private String datosCierre(String idVisita)
        {
            String div = "";
            DataTable consulta = null;
            String tb = "<div class=close><a href=# id=close3><img src=iconosMetro/close.png></a></div><div><table>";
            String tr = "";
            String tr2 = "";
            String tbCom = "";
            consulta = CVC.cargarDatosRes(idVisita);
            foreach (DataRow row in consulta.Rows)
            {
                tr = tr + "<tr> <td><div>Actividad : " + row["nomAct"].ToString() + " </div></td> <td class=styleTd></td> <td><div>Responsable: " + row["nomRes"].ToString() + " </div> </td> <td class=styleTd></td><td><div>Respuesta: " + row["respuesta"].ToString() + " </div> </td> </tr>";
                tr2 = " <tr><td align=center colspan=5><div>Conclucion : " + row["conclucion"].ToString() + "</div> </td> </tr> </table></div>";
            }
            tbCom = tb + tr + tr2;
            div = "$(document).ready(function () {  document.getElementById('popup3').innerHTML ='" + tbCom + "';   $('#popup3').fadeIn('slow'); $('#fondoOsc').fadeIn('slow'); $('#close3').click(function () {  $('#popup3').fadeOut('slow');  $('#fondoOsc').fadeOut('slow');  return false; });   });";
            return div;
        }
        //Creacion del pop up para los datos del documentos
        private void datosDoc(int idVisita)
        {
            string Directorio = CVC.parametrosDoc();
            //Busco los archivos que empiecen por el id de la visita
            string[] files = System.IO.Directory.GetFiles(Directorio.Replace("\\\\", "\\"), "" + idVisita.ToString() + "_*");
            System.IO.FileInfo fi = null;
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[3] { new DataColumn("ruta", typeof(string)), new DataColumn("nombre", typeof(string)), new DataColumn("desc", typeof(string)) });
            foreach (string s in files)
            {
                fi = new System.IO.FileInfo(s);
                dt.Rows.Add(fi, fi.Name, "Descargar");
            }
            grdDoc.DataSource = dt;
            grdDoc.DataBind();
        }
        //Boton del grid del los documentos
        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgbtn = sender as ImageButton;
            GridViewRow row = (GridViewRow)imgbtn.NamingContainer;
            imgbtn.CausesValidation = false;
            String ruta = this.grdDoc.DataKeys[row.RowIndex].Value.ToString();
            String nomArc = row.Cells[1].Text;
            Session["ruta"] = ruta;
            Session["nombre"] = nomArc;
            ClientScript.RegisterStartupScript(GetType(), "script16", "abrirDes();", true);
        }
    }
}