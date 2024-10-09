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
    public partial class ReporteVis : System.Web.UI.Page
    {
        private ControlVisitaComercial CVC = new ControlVisitaComercial();
        protected void Page_Load(object sender, EventArgs e)
        {
            //// HyperLink1.Attributes.Add("onclick", "window.open('ProductsPopUp.aspx',null,'left=400, top=100, height=450, width= 450, status=no, resizable=no, scrollbars=no, toolbar=yes,location= no, menubar=yes');");
            if (!IsPostBack)
            {
                TextBox1_AutoCompleteExtender.ContextKey = Session["usuario"].ToString();
                Session["rango"] = CVC.usuarioActual(Session["usuario"].ToString());
                if (Session["rango"].ToString() == "NOROL")
                {
                    PanelGeneral.Visible = false;
                }
                else
                {
                    PanelGeneral.Visible = true;
                    cargarCombos();
                    limpiarCampos();
                }
                lblTipo.Visible = false;
                cboTipoVis.Visible = false;
                tablaDetallado.Visible = false;
                tablaResumido.Visible = false;
            }
        }
        //carga el combo
        private void cargarCombo(DataTable tabla, DropDownList combo, int value, int texto)
        {
            combo.Items.Clear();
            combo.Items.Add("Seleccionar");
            foreach (DataRow row in tabla.Rows)
            {   //posicion de las colmunas  0 = value / 1 = texto  --- se escoge el numero dependiendo de la columna que tenga en el query //siempre va el valor como id de primero, y despues el texto lo que se va mostrar en el combo / ,0,1
                combo.Items.Add(new ListItem(row[texto].ToString(), row[value].ToString()));
            }
        }
        //Carga todos los combos
        private void cargarCombos()
        {
            cargarCombo(CVC.cargarAgentes(Session["usuario"].ToString()), cboGerComercial, 0, 1);
            /*DataTable tabla = CVC.cargarAgentes(Session["usuario"].ToString());
            cboGerComercial.Items.Clear();
            cboGerComercial.Items.Add("Seleccionar");
            if (Session["rango"].ToString() == "VICE" || Session["rango"].ToString() == "GERENTE")
            {
                cboGerComercial.Items.Add("Todos");
            }
            foreach (DataRow row in tabla.Rows)
            {
                cboGerComercial.Items.Add(new ListItem(row[1].ToString(), row[0].ToString()));
            }*/
        }
        //Limpia todos los campos
        private void limpiarCampos()
        {
            cboNivel.SelectedIndex = 0;
            cboGerComercial.SelectedIndex = 0;
            cboTipoVis.SelectedIndex = 0;
        }
        //Maneja el mensaje por medio de script
        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }
        //Boton de Ver el reporte
        protected void btnVerReporte_Click(object sender, EventArgs e)
        {
            String cliente = "";
            String usuario = "";
            if (cboGerComercial.SelectedItem.ToString() == "Seleccionar")
            {
                mensajeVentana("Por favor seleccione un resposable, gracias!");
            }
            else
            {
                if (txtFechaIni.Text == "" || txtFechaFin.Text == "")
                {
                    mensajeVentana("Por favor seleccione un rango de fechas, gracias!");
                }
                else
                {
                    if (DateTime.Parse(txtFechaFin.Text) < DateTime.Parse(txtFechaIni.Text))
                    {
                        mensajeVentana("La fecha fin no puede ser menor que la  fecha de inicio, gracias!");
                    }
                    else
                    {
                        if (cboNivel.SelectedItem.ToString() == "Seleccionar")
                        {
                            mensajeVentana("Por favor seleccione un nivel, gracias!");
                        }
                        else
                        {
                            if (lblIdCliente.Value == "" || txtCliente.Text == "")
                            {
                                cliente = "";
                                Session["cliente"] = " ";
                            }
                            else
                            {
                                cliente = "AND (mvc.vis_cli_id = " + lblIdCliente.Value.ToString() + ")";
                                Session["cliente"] = cliente;
                            }

                            if (cboGerComercial.SelectedItem.ToString() == "Todos")
                            {
                                if (Session["rango"].ToString() == "VICE")
                                {
                                    usuario = " ";
                                }
                                else
                                {
                                    usuario = " AND (mvc.vis_usu_ejecuta IN ( "
                                    + " SELECT  usuario.usu_login AS usuario "
                                    + " FROM    usuario INNER JOIN   representantes_comerciales ON usuario.usu_siif_id = representantes_comerciales.rc_usu_siif_id INNER JOIN "
                                    + " pais_representante ON pais_representante.pr_id_representante = representantes_comerciales.rc_id  "
                                    + " WHERE   (usuario.usu_activo = 1) AND (pais_representante.pr_activo = 1) AND (representantes_comerciales.rc_activo = 1) "
                                    + " AND (pais_representante.pr_id_pais IN (SELECT  pais_representante.pr_id_pais  "
                                    + " FROM  usuario INNER JOIN   representantes_comerciales ON usuario.usu_siif_id = representantes_comerciales.rc_usu_siif_id INNER JOIN "
                                    + " pais_representante ON pais_representante.pr_id_representante = representantes_comerciales.rc_id "
                                    + " WHERE  (pais_representante.pr_activo = 1) AND (usuario.usu_login = '" + Session["usuario"].ToString() + "'))) "
                                    + " GROUP BY usuario.usu_login)) ";
                                }
                            }
                            else
                            {
                                usuario = " AND (mvc.vis_usu_ejecuta = '" + cboGerComercial.SelectedValue.ToString() + "')";
                            }

                            Session["fechaIni"] = txtFechaIni.Text;
                            Session["fechaFin"] = txtFechaFin.Text;

                            if (cboNivel.SelectedItem.ToString() == "Resumido")
                            {
                                tablaResumido.Visible = true;
                                tablaDetallado.Visible = false;
                                CargarTablaVisRes(usuario, txtFechaIni.Text, txtFechaFin.Text, cliente);
                            }
                            else
                            {
                                tablaResumido.Visible = false;
                                tablaDetallado.Visible = true;
                                if (cboTipoVis.SelectedItem.ToString() == "Seleccionar")
                                {
                                    mensajeVentana("Por favor seleccione un tipo de visita, gracias!");
                                }
                                else if (cboTipoVis.SelectedItem.ToString() == "Todas")
                                {
                                    CargarTablaVisDet(usuario, txtFechaIni.Text, txtFechaFin.Text, " ", cliente);
                                }
                                else if (cboTipoVis.SelectedItem.ToString() == "Realizadas")
                                {
                                    CargarTablaVisDet(usuario, txtFechaIni.Text, txtFechaFin.Text, "AND (mvc.vis_dfecha_cierre IS NOT NULL) ", cliente);
                                }
                                else if (cboTipoVis.SelectedItem.ToString() == "No Realizadas")
                                {
                                    CargarTablaVisDet(usuario, txtFechaIni.Text, txtFechaFin.Text, "AND (mvc.vis_dfecha_cierre IS NULL) AND (mvc.vis_cancelada = 0)", cliente);
                                }
                                else if (cboTipoVis.SelectedItem.ToString() == "Canceladas")
                                {
                                    CargarTablaVisDet(usuario, txtFechaIni.Text, txtFechaFin.Text, "AND (mvc.vis_cancelada = 1) ", cliente);
                                }
                            }
                        }
                    }
                }
            }
        }
        //Carga la tabla Detallada y la Grafica
        private void CargarTablaVisDet(String usuario, String fechaIni, String fechaFin, String filtro, String cliente)
        {
            DataTable detallado = CVC.cargarTablaDetVis(fechaIni, fechaFin, usuario, filtro, cliente);
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
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel2, this.GetType(), "script1", grafica, true);
        }
        //Carga la tabla Resumida y la Grafica
        private void CargarTablaVisRes(String usuario, String fechaIni, String fechaFin, String cliente)
        {
            DataTable resumido = CVC.cargarTablaResVis(fechaIni, fechaFin, usuario, cliente);
            grdResumido.DataSource = resumido;
            grdResumido.DataBind();
            String comIni = "var barChartData = {";
            String comFin = "};";
            String titIni = "labels: [";
            String titVal = "";
            String titFin = "],";
            String cueIni = "datasets: [ ";
            String cueFin = "]";
            String planIni = "{ fillColor: '#6b9dfa', strokeColor: '#ffffff',  highlightFill: '#1864f2', highlightStroke: '#ffffff', data: [";
            String planVal = "";
            String planFin = "]},";
            String ejeIni = "{ fillColor: '#e9e225', strokeColor: '#ffffff',  highlightFill: '#ee7f49', highlightStroke: '#ffffff', data: [";
            String ejeVal = "";
            String ejeFin = "]},";
            String canIni = "{ fillColor: '#E02E35', strokeColor: '#ffffff',  highlightFill: '#B66DEA', highlightStroke: '#ffffff', data: [";
            String canVal = "";
            String canFin = "]},";
            foreach (DataRow row in resumido.Rows)
            {
                titVal = titVal + "'" + row["nomUsuario"].ToString() + "',";
                planVal = planVal + "" + row["visPlan"].ToString() + ",";
                ejeVal = ejeVal + "" + row["visEje"].ToString() + ",";
                canVal = canVal + "" + row["visCan"].ToString() + ",";
            }
            String grafica = comIni + titIni + titVal + titFin + cueIni + planIni + planVal + planFin + ejeIni + ejeVal + ejeFin + canIni + canVal + canFin + cueFin + comFin + " var ctx3 = document.getElementById('chart-area3').getContext('2d'); window.myPie = new Chart(ctx3).Bar(barChartData, { responsive: true });";
            ScriptManager.RegisterClientScriptBlock(this.UpdatePanel2, this.GetType(), "script2", grafica, true);
        }
        //Recorre la tabla Resumida para obtener el usuario y abrir el popup de la tabla Detallada
        protected void grdResumido_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Respon")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdResumido.Rows[index];
                Label listPriceTextBox = (Label)row.FindControl("PriceLabel");
                Session["usuVisitaPopUp"] = listPriceTextBox.Text;
                ScriptManager.RegisterStartupScript(Page, GetType(), "Script4", "verDetVisResp();", true);
            }
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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script5", div, true);
            }
            else if (e.CommandName == "datosEje")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdDetallado.Rows[index];
                String idVis = row.Cells[0].Text;
                String div = datosEje(idVis);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script6", div, true);
            }
            else if (e.CommandName == "datosCierre")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdDetallado.Rows[index];
                String idVis = row.Cells[0].Text;
                String div = datosCierre(idVis);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script13", div, true);
            }
            else if (e.CommandName == "datosDoc")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grdDetallado.Rows[index];
                String idVis = row.Cells[0].Text;
                datosDoc(int.Parse(idVis));
                // ClientScript.RegisterStartupScript(GetType(), "script15", "popupDoc();", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script15", "popupDoc();", true);
            }
        }
        //Combo de seleccionar el nivel
        protected void cboNivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNivel.SelectedItem.ToString() == "Seleccionar")
            {
                cboTipoVis.Visible = false;
                lblTipo.Visible = false;
            }
            else if (cboNivel.SelectedItem.ToString() == "Resumido")
            {
                cboTipoVis.Visible = false;
                lblTipo.Visible = false;
            }
            else if (cboNivel.SelectedItem.ToString() == "Detallado")
            {
                cboTipoVis.Visible = true;
                lblTipo.Visible = true;
            }
        }
        //Creacion del pop up para la nota
        private String nota(String idVisita)
        {
            String div = "";
            String obj = CVC.cargarNota(idVisita);
            String tr = "<div class=close><a href=# id=close><img src=iconosMetro/close.png></a></div><div><table><tr><td><div>Objetivo : </div> </td> <td><div>" + obj + "</div> </td> </tr></table><div>";
            div = "$(document).ready(function () { document.getElementById('popup').innerHTML  = '" + tr + "';   $('#popup').fadeIn('slow'); $('#fondoOsc').fadeIn('slow'); $('#close').click(function () {  $('#popup').fadeOut('slow'); $('#fondoOsc').fadeOut('slow'); return false; });   });";
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
                tr = tr + "<tr> <td><div>Actividad : " + row["nomAct"].ToString() + " </div></td> <td  class=styleTd></td> <td><div>Responsable: " + row["nomRes"].ToString() + " </div> </td> </tr>";
                tr2 = " <tr><td align=center colspan=3><div>Conclucion : " + row["conclucion"].ToString() + "</div> </td> </tr> </table></div>";
            }
            tbCom = tb + tr + tr2;
            div = "$(document).ready(function () { document.getElementById('popup2').innerHTML  = '" + tbCom + "';   $('#popup2').fadeIn('slow'); $('#fondoOsc').fadeIn('slow');  $('#close2').click(function () {  $('#popup2').fadeOut('slow'); $('#fondoOsc').fadeOut('slow');  return false; });   });";
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
            div = "$(document).ready(function () {  document.getElementById('popup3').innerHTML ='" + tbCom + "';   $('#popup3').fadeIn('slow'); $('#fondoOsc').fadeIn('slow');  $('#close3').click(function () {  $('#popup3').fadeOut('slow'); $('#fondoOsc').fadeOut('slow'); return false; });   });";
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
            ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(imgbtn);
            String ruta = this.grdDoc.DataKeys[row.RowIndex].Value.ToString();
            String nomArc = row.Cells[1].Text;
            Session["ruta"] = ruta;
            Session["nombre"] = nomArc;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script15", "abrirDes();", true);
        }
        protected void grdDoc_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }
        //Boton para descargar archivo en Excel
        protected void btnDesExcel_Click(object sender, EventArgs e)
        {
            if (txtFechaIni.Text != "" || txtFechaFin.Text != "")
            {
                if (cboGerComercial.SelectedItem.ToString() != "Seleccionar")
                {
                    String usuario = "";
                    String accion = "";
                    if (cboGerComercial.SelectedItem.ToString() == "Todos")
                    {
                        usuario = Session["usuario"].ToString();
                        accion = "Todos";
                    }
                    else
                    {
                        usuario = cboGerComercial.SelectedValue.ToString();
                        accion = "uno";
                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "window.open('" + "http://10.75.131.2:81/ReportServer?/Comercial/COM_Visitas&rs:format=EXCEL&rs:command=render&rs:ClearSession=true&fechaIni=" + txtFechaIni.Text + "&fechaFin=" + txtFechaFin.Text + "&usuario=" + usuario + "&rol=" + Session["rango"].ToString() + "&accion=" + accion + "', this.target, 'top=450, left=900, toolbar=no, location=no, status=no, menubar=no, scrollbars=yes, resizable=no, width=170, height=30')", true);
                }
                else { mensajeVentana("Por favor seleccione un comercial o todos, gracias!!"); }
            }
            else
            {
                mensajeVentana("Se necesita un rango de fechas, gracias!!");
            }
        }
    }
}