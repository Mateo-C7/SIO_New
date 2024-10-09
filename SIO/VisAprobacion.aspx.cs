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
namespace SIO
{
    public partial class AprobacionVis : System.Web.UI.Page
    {
        private ControlVisitaComercial CVC = new ControlVisitaComercial();
        public ControlPoliticas CP = new ControlPoliticas();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["Rango"] = CVC.usuarioActual(Session["usuario"].ToString());
                if (Session["Rango"].ToString() == "VICE" || Session["Rango"].ToString() == "GERENTE")
                {
                    cargarCombos();
                    limpiarCampos(); ;
                    Session["cbotodosValor"] = null;
                }
                else
                {
                    PanelGeneral.Visible = false;
                }

                politicas(30, Session["usuario"].ToString());
                // PRU politicas(45, Session["usuario"].ToString());
            }
        }
        //Carga todos los combos
        private void cargarCombos()
        {
            cboZonas.Items.Clear();
            DataTable cargaZonas = CVC.cargarZonas();//Carga el combo de zonas cada vez que carga la pagina
            cboZonas.Items.Add("Seleccionar");
            foreach (DataRow row in cargaZonas.Rows)
            {
                cboZonas.Items.Add(new ListItem(row["nombre"].ToString(), row["idZona"].ToString()));
            }
            /***********/
           /* cboAgenda.Items.Clear();
            DataTable cargaAgendas = CVC.cargarAgendas();//Carga el combo de periodos cada vez que carga la pagina
            cboAgenda.Items.Add("Seleccionar");
            foreach (DataRow row in cargaAgendas.Rows)
            {
                cboAgenda.Items.Add(new ListItem(row["nomAgenda"].ToString(), row["idAgenda"].ToString()));
            }*/
            /*************/
            cboTipoAgente.Items.Clear();
            DataTable cboTipoAge = CVC.cargarTiposAgentes();//Carga el combo de tipo cada vez que carga la pagina
            cboTipoAgente.Items.Add("Seleccionar");
            foreach (DataRow row in cboTipoAge.Rows)
            {
                cboTipoAgente.Items.Add(new ListItem(row["nomTipo"].ToString(), row["rol"].ToString()));
            }
            /**************/
            cboPersonaGenRep.Items.Clear();
            cboPersonaGenRep.Items.Add("Seleccionar");
        }
        //Combo de Agenda
        protected void cboAgenda_SelectedIndexChanged(object sender, EventArgs e)
        {
            recargarTabla();
        }
        //Combo del las zonas
        protected void cboZonas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboZonas.SelectedItem.ToString() == "Seleccionar")
            {
                cboPersonaGenRep.SelectedIndex = 0;
            }
            else if (cboTipoAgente.SelectedItem.ToString() == "Seleccionar")
            {
                cboPersonaGenRep.Items.Clear();
                DataTable cargaAgentes = CVC.cargarRepresentates("", " AND (pais.pai_zona_id = " + cboZonas.SelectedValue.ToString() + ")", Session["usuario"].ToString(), Session["Rango"].ToString());//Carga el como cada vez que carga la pagina
                cboPersonaGenRep.Items.Add("Seleccionar");
                foreach (DataRow row in cargaAgentes.Rows)
                {
                    cboPersonaGenRep.Items.Add(new ListItem(row["nombre"].ToString(), row["usuario"].ToString()));
                }
                cboPersonaGenRep.Visible = true;
            }
            else
            {
                cboPersonaGenRep.Items.Clear();
                DataTable cargaAgentes = CVC.cargarRepresentates("AND (" + cboTipoAgente.SelectedValue.ToString() + " = 1)", " AND (pais.pai_zona_id = " + cboZonas.SelectedValue.ToString() + ")", Session["usuario"].ToString(), Session["Rango"].ToString());//Carga el como cada vez que carga la pagina
                cboPersonaGenRep.Items.Add("Seleccionar");
                foreach (DataRow row in cargaAgentes.Rows)
                {
                    cboPersonaGenRep.Items.Add(new ListItem(row["nombre"].ToString(), row["usuario"].ToString()));
                }
                cboPersonaGenRep.Visible = true;
            }
            recargarTabla();
        }
        //Combo del tipo de Agente
        protected void cboTipoAgente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipoAgente.SelectedItem.ToString() == "Seleccionar")
            {
                cboPersonaGenRep.Items.Clear();
                cboPersonaGenRep.Items.Add("Seleccionar");
            }
            else
            {
                if (cboAgenda.SelectedItem.ToString() == "Seleccionar")
                {
                    mensajeVentana("Por favor seleccione una Agenda, gracias!");
                }
                else if (cboZonas.SelectedItem.ToString() == "Seleccionar")
                {
                    mensajeVentana("Por favor seleccione una Zona, gracias!");
                }
                else
                {
                    cboPersonaGenRep.Items.Clear();
                    cboPersonaGenRep.Visible = true;
                    DataTable cargaAgentes = CVC.cargarRepresentates("AND (" + cboTipoAgente.SelectedValue.ToString() + " = 1)", " AND (pais.pai_zona_id = " + cboZonas.SelectedValue.ToString() + ")", Session["usuario"].ToString(), Session["Rango"].ToString());//Carga el como cada vez que carga la pagina
                    cboPersonaGenRep.Items.Add("Seleccionar");
                    foreach (DataRow row in cargaAgentes.Rows)
                    {
                        cboPersonaGenRep.Items.Add(new ListItem(row["nombre"].ToString(), row["usuario"].ToString()));
                    }
                }
                recargarTabla();
            }
        }
        //Combo de los representantes
        protected void cboPersonaGenRep_SelectedIndexChanged(object sender, EventArgs e)
        {
            recargarTabla();
        }
        //Recarga la tabla en cualquier opcion
        private void recargarTabla()
        {
            String filUsuario = "";
            String filAgenda = "";
            String filTipo = "";
            String filZona = "";
            if (cboAgenda.SelectedItem.ToString() != "Seleccionar")
            {
                filAgenda = "  AND (mvm.vis_agenda_id = " + int.Parse(cboAgenda.SelectedValue.ToString()) + ")  ";
            }

            if (cboTipoAgente.SelectedItem.ToString() != "Seleccionar")
            {
                filTipo = "  AND (" + cboTipoAgente.SelectedValue.ToString() + " = 1)  ";
            }

            if (cboZonas.SelectedItem.ToString() != "Seleccionar")
            {
                filZona = "  AND (pais.pai_zona_id = " + int.Parse(cboZonas.SelectedValue.ToString()) + ")  ";
            }

            if (cboPersonaGenRep.SelectedItem.ToString() != "Seleccionar")
            {
                filUsuario = "  AND (mvm.vis_usu_ejecuta = '" + cboPersonaGenRep.SelectedValue.ToString() + "')  ";
                filZona = "";
            }
            DataTable visitas = CVC.recargarTablaVis(filUsuario, filAgenda, filTipo, filZona, Session["usuario"].ToString(), Session["Rango"].ToString());
            grdTablaVisitas.Visible = true;
            grdTablaVisitas.DataSource = visitas;
            grdTablaVisitas.DataBind();
        }
        //Limpia todos los campos
        private void limpiarCampos()
        {
            cboAgenda.SelectedIndex = 0;
            cboZonas.SelectedIndex = 0;
            cboTipoAgente.SelectedIndex = 0;
            cboTodosAprob.SelectedIndex = 0;
            cboPersonaGenRep.SelectedIndex = 0;
        }
        //Este metodo me llena el combo que esta en la tabla de visitas, dependiendo de una session[cboTodosValor] que me vefirica los que haya seleccionado en combo de seleccionar todos
        public DataTable llenarComboSel()
        {
            DataTable tabla = new DataTable();
            tabla.Columns.Add("Nombre", typeof(String));
            tabla.Columns.Add("Valor", typeof(String));
            DataRow fila = tabla.NewRow();
            if (Session["cbotodosValor"] == null || Session["cbotodosValor"].ToString() == "")//valido si la Session["cbotodosValor"] hay algo, si hay es que ha escogido una opcion del combo de escoger todos y si esta vacio es que puede selecionar varias opciones
            {
                tabla.Rows.Add(new Object[] { "Seleccionar", "Nada" });
                tabla.Rows.Add(new Object[] { "Aprobado", "1" });
                tabla.Rows.Add(new Object[] { "No Aprobado", "0" });
            }
            else
            {
                tabla.Rows.Add(new Object[] { Session["cbotodosText"].ToString(), Session["cbotodosValor"].ToString() });
            }
            return tabla;
        }
        //Combo para aprobar o no aprobarlo todos
        protected void cboTodosAprob_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTodosAprob.SelectedValue.ToString() == "Seleccionar")
            {
                cargarTablaCombo(null, null);
            }
            else if (cboTodosAprob.SelectedValue.ToString() == "AprobT")
            {
                cargarTablaCombo("Aprobado", "1");
            }
            else if (cboTodosAprob.SelectedValue.ToString() == "NoAprobT")
            {
                cargarTablaCombo("No Aprobado", "0");
            }
        }
        //Carga la tabla dependiendo lo que haya escogido en el combo
        public void cargarTablaCombo(String text, String valor)
        {
            Session["cbotodosValor"] = valor;
            Session["cbotodosText"] = text;
            recargarTabla();
        }
        //Valida todos los campos
        private Boolean validacionCampos()
        {
            Boolean comfi = false;//Confirmacion
            if (cboAgenda.SelectedItem.ToString() == "Seleccionar")
            {
                mensajeVentana("Por favor seleccione una Agenda, gracias");
                cboTodosAprob.Visible = false;
            }
            else
            {
                if (cboTipoAgente.SelectedItem.ToString() == "Seleccionar")
                {
                    mensajeVentana("Por favor seleccione un tipo de Agente, gracias");
                    cboTodosAprob.Visible = false;
                }
                else
                {
                    if (cboZonas.SelectedItem.ToString() == "Seleccionar")
                    {
                        mensajeVentana("Por favor seleccione una Zona, gracias");
                        cboTodosAprob.Visible = false;
                    }
                    else
                    {
                        if (cboPersonaGenRep.SelectedItem.ToString() == "Seleccionar")
                        {
                            mensajeVentana("Por favor seleccione un Agente, gracias");
                            cboTodosAprob.Visible = false;
                        }
                        else
                        {
                            comfi = true;
                            cboTodosAprob.Visible = true;
                        }
                    }
                }
            }
            return comfi;
        }
        //Maneja el mensaje por medio de script
        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }
        //Actualiza el estado
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            guardarCambio();
            recargarTabla();
        }
        //Realiza el cambio de estado de Aprobacion
        private void guardarCambio()
        {
            String cboAproNuevaText = "";
            String cboAproNuevaValor = "";
            String actualAprob = "";
            String numVisita = "";
            String cliente = "";
            String pais = "";
            String ciudad = "";
            String usuEje = "";
            int mensajeNum = 0;
            String mensaje = "";
            foreach (GridViewRow row in grdTablaVisitas.Rows)
            {
                cboAproNuevaText = ((DropDownList)row.FindControl("cboAprobacion")).SelectedItem.Text;
                if (cboAproNuevaText != "Seleccionar")
                {
                    cboAproNuevaValor = ((DropDownList)row.FindControl("cboAprobacion")).SelectedValue.ToString();
                    cliente = row.Cells[1].Text;
                    pais = row.Cells[2].Text;
                    ciudad = row.Cells[3].Text;
                    usuEje = row.Cells[6].Text;
                    actualAprob = row.Cells[8].Text;
                    numVisita = row.Cells[0].Text;
                    if (actualAprob != cboAproNuevaText)
                    {
                        if (cboAproNuevaText == "Aprobado")
                        {
                            mensaje = CVC.editarEstadoVisita(numVisita, cboAproNuevaValor, "SYSDATETIME()", Session["usuario"].ToString());
                            if (mensaje == "OK")
                            {
                                CVC.correoAprobacion(numVisita, cliente, pais, "Aprobacíon de la Visita: ", ciudad, usuEje, Session["usuario"].ToString(), "VisitasCApro");
                                mensajeNum = mensajeNum + 1;
                            }
                        }
                        else
                        {
                            mensaje = CVC.editarEstadoVisita(numVisita, cboAproNuevaValor, "NULL", Session["usuario"].ToString());
                            if (mensaje == "OK")
                            {
                                CVC.correoAprobacion(numVisita, cliente, pais, "No Aprobacíon de la Visita: ", ciudad, usuEje, Session["usuario"].ToString(), "VisitasCApro");
                                mensajeNum = mensajeNum + 1;
                            }
                        }
                    }
                    else
                    {
                        mensajeNum = mensajeNum + 0;
                    }
                }
                else
                {
                    mensajeNum = mensajeNum + 0;
                }
            }
            //
            if (mensajeNum > 0)
            {
                mensaje = "Cambios realizados!!";
            }
            else
            {
                mensaje = "No hubo ningun cambio!";
            }
            mensajeVentana(mensaje);
        }
        //Maneja las politicas dependiendo de la rutina y el rol del usuario
        private void politicas(int rutina, String usuario)
        {
            Boolean agregar = false;
            Boolean eliminar = false;
            Boolean imprimir = false;
            Boolean editar = false;
            DataTable politicas = null;
            politicas = CP.politicasBotones(rutina, usuario);
            foreach (DataRow row in politicas.Rows)
            {
                agregar = Boolean.Parse(row["agregar"].ToString());
                eliminar = Boolean.Parse(row["eliminar"].ToString());
                imprimir = Boolean.Parse(row["imprimir"].ToString());
                editar = Boolean.Parse(row["editar"].ToString());
            }
            if (editar == true)
            { btnActualizar.Visible = true; }
            else { btnActualizar.Visible = false; }
        }
    }
}