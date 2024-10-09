using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaControl;
using System.Data;
using CapaDatos;

namespace SIO
{
    public partial class ViajesVis : System.Web.UI.Page
    {
        public ControlPoliticas CP = new ControlPoliticas();
        private ControlVisitaComercial CVC = new ControlVisitaComercial();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["idPaisViaje"] = "NO";
                if (CVC.permisosViajes(Session["usuario"].ToString()).Rows.Count == 0)
                {
                    PanelGeneral.Visible = false;
                }
                else
                {
                    limpiarCampos();
                    cargarComboHorasMin();
                    cargarTabla();
                }
                politicas(31, Session["usuario"].ToString());
                // PRU politicas(46, Session["usuario"].ToString());
            }
        }
        //Boton de eliminar
        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            String mensaje = "";
            Button btn = sender as Button;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int idViaje = int.Parse(this.grdViajes.DataKeys[row.RowIndex].Value.ToString());
            String comercial = row.Cells[1].Text;
            String pais = row.Cells[2].Text;
            String ciudad = row.Cells[3].Text;
            String fechaIni = row.Cells[4].Text;
            String horaIni = row.Cells[5].Text;
            String fechaFin = row.Cells[6].Text;
            String horaFin = row.Cells[7].Text;
            String usuComer = CVC.consultaUsuComerViaje(idViaje);
            mensaje = CVC.anularViaje(idViaje);
            if (mensaje == "OK")
            {
                mensajeVentana("Se ha eliminado correctamente!!");
                CVC.correoViajes(comercial, usuComer, pais, ciudad, fechaIni, fechaFin, horaIni, horaFin, "VisitasCViajes", "Cancelacion del Viaje: ", Session["usuario"].ToString());
                cargarTabla();
            }
            else
            {
                mensajeVentana(mensaje);
            }
        }
        //Boton de adicionar
        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            String mensaje = "";
            if (validacionCampos() == true)
            {
                Boolean comfi = CVC.validarViajeExiste(txtFechaIni.Text, txtFechaFin.Text, lblIdCom.Value);//Me valida si el viaje ya exite con el usuario
                if (comfi == true)
                {
                    mensajeVentana("No puede programar un viaje existente al mismo usuario comercial!");
                }
                else
                {
                    mensaje = CVC.insertarViaje(lblIdCom.Value, txtFechaIni.Text, txtFechaFin.Text, lblIdPais.Value, lblIdCiu.Value, Session["usuario"].ToString(), cboHoraIni.SelectedItem.ToString() + ":" + cboMinIni.SelectedItem.ToString(), cboHoraFin.SelectedItem.ToString() + ":" + cboMinFin.SelectedItem.ToString());
                    if (mensaje == "OK")
                    {
                        mensajeVentana("Se ha ingresado correctamente!!");
                        CVC.correoViajes(txtComercial.Text, lblIdCom.Value, txtPais.Text, txtCiudad.Text, txtFechaIni.Text, txtFechaFin.Text, cboHoraIni.SelectedItem.ToString() + ":" + cboMinIni.SelectedItem.ToString(), cboHoraFin.SelectedItem.ToString() + ":" + cboMinFin.SelectedItem.ToString(), "VisitasCViajes", "Creacion del Viaje: ", Session["usuario"].ToString());
                        limpiarCampos();
                        cargarComboHorasMin();
                        cargarTabla();
                    }
                    else
                    {
                        mensajeVentana(mensaje);
                    }
                }
            }
            else
            {
                validacionCampos();
            }
        }
        //Validacion de todos los campos
        private Boolean validacionCampos()
        {
            Boolean comfi = false;//Confirmacion
            if (txtComercial.Text == "" || lblIdCom.Value == null || lblIdCom.Value == "")
            {
                mensajeVentana("Por favor seleccione el comercial encargado, gracias!!");
            }
            else
            {
                if (txtPais.Text == "" || lblIdPais.Value == null || lblIdPais.Value == "")
                {
                    mensajeVentana("Por favor seleccione el pais, gracias!!");
                }
                else
                {
                    if (txtCiudad.Text == "" || lblIdCiu.Value == null || lblIdCiu.Value == "")
                    {
                        mensajeVentana("Por favor seleccione la ciudad, gracias!!");
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
                                if (cboHoraIni.SelectedItem.ToString() == "--" || cboMinIni.SelectedItem.ToString() == "--")
                                {
                                    mensajeVentana("Por favor seleccione la hora y los minutos de la fecha inicial, gracias!");
                                }
                                else
                                {
                                    if (cboHoraFin.SelectedItem.ToString() == "--" || cboMinFin.SelectedItem.ToString() == "--")
                                    {
                                        mensajeVentana("Por favor seleccione la hora y los minutos de la fecha final, gracias!");
                                    }
                                    else { comfi = true; }
                                }
                            }
                        }
                    }
                }
            }
            return comfi;
        }
        //Maneja el mensaje por medio de script
        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }
        //Limpia todos los campos
        private void limpiarCampos()
        {
            txtComercial.Text = "";
            txtPais.Text = "";
            txtCiudad.Text = "";
            lblIdCom.Value = "";
            lblIdPais.Value = "";
            lblIdCiu.Value = "";
            txtFechaFin.Text = "";
            txtFechaIni.Text = "";
            cboHoraIni.Items.Clear();
            cboHoraFin.Items.Clear();
            cboMinIni.Items.Clear();
            cboMinFin.Items.Clear();
        }
        //Carga la tabla de viajes
        private void cargarTabla()
        {
            DataTable visitas = CVC.cargarTablaViajes(Session["usuario"].ToString());
            grdViajes.DataSource = visitas;
            grdViajes.DataBind();
        }
        //Carga las horas y minutos de los combos
        private void cargarComboHorasMin()
        {
            cboHoraIni.Items.Add(new ListItem("--", "--"));
            cboHoraFin.Items.Add(new ListItem("--", "--"));
            cboMinIni.Items.Add(new ListItem("--", "--"));
            cboMinFin.Items.Add(new ListItem("--", "--"));
            //Carga las horas
            for (var h = 0; h <= 23; h++)
            {
                if (h == 0)
                {
                    String ms = "00";
                    cboHoraIni.Items.Add(new ListItem(ms, ms));
                    cboHoraFin.Items.Add(new ListItem(ms, ms));
                }
                else
                {
                    if (h < 10)
                    {
                        cboHoraIni.Items.Add(new ListItem("0" + h.ToString(), "0" + h.ToString()));
                        cboHoraFin.Items.Add(new ListItem("0" + h.ToString(), "0" + h.ToString()));
                    }
                    else
                    {
                        cboHoraIni.Items.Add(new ListItem(h.ToString(), h.ToString()));
                        cboHoraFin.Items.Add(new ListItem(h.ToString(), h.ToString()));
                    }
                }
            }
            //Carga los minutos
            for (var m = 0; m <= 59; m++)
            {
                if (m == 0)
                {
                    String ms = "00";
                    cboMinIni.Items.Add(new ListItem(ms, ms));
                    cboMinFin.Items.Add(new ListItem(ms, ms));
                }
                else
                {
                    if (m < 10)
                    {
                        cboMinIni.Items.Add(new ListItem("0" + m.ToString(), "0" + m.ToString()));
                        cboMinFin.Items.Add(new ListItem("0" + m.ToString(), "0" + m.ToString()));
                    }
                    else
                    {
                        cboMinIni.Items.Add(new ListItem(m.ToString(), m.ToString()));
                        cboMinFin.Items.Add(new ListItem(m.ToString(), m.ToString()));
                    }
                }
            }
        }
        protected void txtPais_TextChanged(object sender, EventArgs e)
        {
            if (lblIdPais.Value != null || lblIdPais.Value != "" || txtPais.Text != "")
            {
                Session["idPaisViaje"] = lblIdPais.Value;
            }
            else
            {
                Session["idPaisViaje"] = "NO";
            }
            txtCiudad.Text = "";
            lblIdCiu.Value = "";
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

            if (agregar == true)
            { btnAdicionar.Visible = true; }
            else { btnAdicionar.Visible = false; }

            if (eliminar == false)
            { grdViajes.Columns[8].Visible = false; }
        }
    }
}