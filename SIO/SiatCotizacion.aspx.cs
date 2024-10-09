using CapaControl;
using CapaDatos;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIO
{
    public partial class SiatCotizacion : System.Web.UI.Page
    {
        private ControlSIAT CS = new ControlSIAT();

        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!IsPostBack)
            {
                int rol = (int)Session["Rol"];
                if (rol == 9 || rol == 2 || rol == 39 ||
                    rol == 24 || rol == 26 || rol == 49
                    || rol == 35 || rol == 18 || rol == 30 || rol == 36 )
                {
                    PanelCliente.Enabled = false;
                    PanelTecnico.Enabled = false;
                    btnCotizar.Enabled = false;
                }
                else
                {
                    PanelAprobacion.Enabled = false;
                }
                cargarCombo(CS.cargarServicios(), cboServicio, 0, 1);//Combo               
            }
        }

        private void validarRol(int estado)
        {
            bool facturable;
            if (chksFacturable.Checked == true)
            {
                facturable = true;
            }
            else
            {
                facturable = false;
            }
            int rol = (int)Session["Rol"];
                                
                if (((rol == 9 || rol == 38) && cboServicio.SelectedValue.ToString() == "1" && facturable == false) ||
                                           (rol == 2 && cboServicio.SelectedValue.ToString() == "1" && facturable == false)||
                                           (rol == 36 && cboServicio.SelectedValue.ToString() == "1" && facturable == false) )
                {
                    if (estado == 1)
                {
                    PanelAprobacion.Enabled = true;
                    btnActualizar.Enabled = true;
                    panelCorreo.Visible = true;                       
                }
                else
                {
                    PanelAprobacion.Enabled = false;
                    btnActualizar.Enabled = false;
                    panelCorreo.Visible = true;
                }               
                PanelCliente.Enabled = false;
                PanelTecnico.Enabled = false;
                btnCotizar.Enabled = false;
            }

                else if (( (rol == 9 || rol == 38) && cboServicio.SelectedValue.ToString() == "1" && facturable == true) ||
                  (rol == 2 && cboServicio.SelectedValue.ToString() == "1" && facturable == true) ||
                  (rol == 36 && cboServicio.SelectedValue.ToString() == "1" && facturable == true))
                {
                    if (estado == 1)
                {
                    PanelAprobacion.Enabled = true;
                    btnActualizar.Enabled = true;
                    panelCorreo.Visible = true;
                }
                else
                {
                    PanelAprobacion.Enabled = false;
                    btnActualizar.Enabled = false;
                    panelCorreo.Visible = true;
                }
                PanelCliente.Enabled = false;
                PanelTecnico.Enabled = false;
                btnCotizar.Enabled = false;
            }
            else if (rol == 38 && cboServicio.SelectedValue.ToString() == "1" && facturable == true) 
            {
                if (estado == 1)
                {
                    PanelAprobacion.Enabled = false;
                    btnActualizar.Enabled = true;
                    panelCorreo.Visible = true;
                }
                else
                {
                    PanelAprobacion.Enabled = false;
                    btnActualizar.Enabled = false;
                    panelCorreo.Visible = true;
                }
                PanelCliente.Enabled = false;
                PanelTecnico.Enabled = false;
                btnCotizar.Enabled = false;
            }
            else if (rol == 38 && cboServicio.SelectedValue.ToString() == "12" && facturable == false)
            {
                if (estado == 1)
                {
                    PanelAprobacion.Enabled = true;
                    btnActualizar.Enabled = true;
                    panelCorreo.Visible = true;
                }
                else
                {
                    PanelAprobacion.Enabled = false;
                    btnActualizar.Enabled = false;
                    panelCorreo.Visible = true;
                }
                PanelCliente.Enabled = false;
                PanelTecnico.Enabled = false;
                btnCotizar.Enabled = false;
            }
            else if ((rol == 24 && cboServicio.SelectedValue.ToString() == "3") || (rol == 24 && cboServicio.SelectedValue.ToString() == "4") ||
                             (rol == 26 && cboServicio.SelectedValue.ToString() == "3") || (rol == 26 && cboServicio.SelectedValue.ToString() == "4") ||
                              (rol == 3 && cboServicio.SelectedValue.ToString() == "3") || (rol == 3 && cboServicio.SelectedValue.ToString() == "4") ||
                              (rol == 33 && cboServicio.SelectedValue.ToString() == "4") ||
                            (rol == 49 && cboServicio.SelectedValue.ToString() == "3") || (rol == 49 && cboServicio.SelectedValue.ToString() == "4"))
            {
                if (estado == 1)
                {
                    PanelAprobacion.Enabled = true;
                    btnActualizar.Enabled = true;
                    panelCorreo.Visible = true;
                }
                else
                {
                    PanelAprobacion.Enabled = false;
                    btnActualizar.Enabled = false;
                    panelCorreo.Visible = true;
                }
                PanelCliente.Enabled = false;
                PanelTecnico.Enabled = false;
                btnCotizar.Enabled = false;
            }
                else if ((rol == 35 && cboServicio.SelectedValue.ToString() == "6" && facturable == false) ||
                  (rol == 18 && cboServicio.SelectedValue.ToString() == "6" && facturable == false))
                {
                    if (estado == 1)
                {
                    PanelAprobacion.Enabled = true;
                    btnActualizar.Enabled = true;
                    panelCorreo.Visible = true;
                }
                else
                {
                    PanelAprobacion.Enabled = false;
                    btnActualizar.Enabled = false;
                    panelCorreo.Visible = true;
                }
                PanelCliente.Enabled = false;
                PanelTecnico.Enabled = false;
                btnCotizar.Enabled = false;
            }
            else if (rol == 38 && cboServicio.SelectedValue.ToString() == "5" && facturable == false) 
            {
                if (estado == 1)
                {
                    PanelAprobacion.Enabled = true;
                    btnActualizar.Enabled = true;
                    panelCorreo.Visible = true;
                }
                else
                {
                    PanelAprobacion.Enabled = false;
                    btnActualizar.Enabled = false;
                    panelCorreo.Visible = true;
                }
                PanelCliente.Enabled = false;
                PanelTecnico.Enabled = false;
                btnCotizar.Enabled = false;
            }
            else
            {
                if (estado == 1)
                {
                    PanelCliente.Enabled = true;
                    PanelTecnico.Enabled = true;
                    btnCotizar.Enabled = false;
                    btnActualizar.Enabled = true;
                    panelCorreo.Visible = true;
                }
                else
                {
                    PanelCliente.Enabled = false;
                    PanelTecnico.Enabled = false;
                    btnCotizar.Enabled = false;
                    btnActualizar.Enabled = false;
                    panelCorreo.Visible = true;
                }
                PanelAprobacion.Enabled = false;                
            }
                if ((rol == 38 && cboServicio.SelectedValue.ToString() == "1" && estado==1) || (rol == 38 && cboServicio.SelectedValue.ToString() == "6" && estado == 1)
                || (rol == 38 && cboServicio.SelectedValue.ToString() == "12" && estado == 1) || (rol == 38 && cboServicio.SelectedValue.ToString() == "3" && estado == 1)
                || (rol == 38 && cboServicio.SelectedValue.ToString() == "4" && estado == 1) || (rol == 38 && cboServicio.SelectedValue.ToString() == "5" && estado == 1)) 
            {                
                    PanelCliente.Enabled = true;
                    PanelTecnico.Enabled = true;                                   
                    panelCorreo.Visible = true;
            }
            else
            {
                PanelCliente.Enabled = false;
                PanelTecnico.Enabled = false;
                
                panelCorreo.Visible = false;
            }
                if (cboServicio.SelectedValue.ToString() == "6" || cboServicio.SelectedValue.ToString() == "12")
            {
                chksFacturable.Enabled = false;
            }
            else
            {
                chksFacturable.Enabled = true;
            }
           
        }

        private void cargarCombo(DataTable tabla, DropDownList combo, int value, int texto)
        {
            combo.Items.Clear();
            combo.Items.Add("Seleccionar");
            foreach (DataRow row in tabla.Rows)
            {   //posicion de las colmunas  0 = value / 1 = texto  --- se escoge el numero dependiendo de la columna que tenga en el query //siempre va el valor como id de primero, y despues el texto lo que se va mostrar en el combo / ,0,1
                combo.Items.Add(new ListItem(row[texto].ToString(), row[value].ToString()));
            }
        }
      
        protected void txtCliente_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            if (lblIdCliente.Value != "") { 
                cboOF.Items.Clear();
                cargarCombo(CS.cargarObras(" (cliente.cli_id = " + lblIdCliente.Value + ")"), cboObra, 2, 1);//Combo obra
                listObra.Items.Clear();
                listOF.Items.Clear();
                LblOfGarantia.Text = "";
                dt = CS.consultarMonedaCliente(Convert.ToInt32(lblIdCliente.Value));
                lblMoneda.Text = dt.Rows[0]["moneda"].ToString();
                lblIdMoneda.Value = dt.Rows[0]["mon_id"].ToString();
                lblIdPais.Value = dt.Rows[0]["pais"].ToString();
                                       
                if(cboServicio.SelectedValue.ToString() == "6")
                {
                    cargarCombo(CS.cargarOG(" (cliente.cli_id = " + lblIdCliente.Value + ")"), cboOF, 0, 1);//Combo OF
                }
                else
                {

                }              
            }
            else if (lblIdCliente.Value == "" || txtCliente.Text == "") { cboObra.Items.Clear(); cboObra.Items.Add("Seleccionar"); cboOF.Items.Clear(); }
        }

        protected void cboOF_TextChanged(object sender, EventArgs e)
        {
            DataTable dtof;
            listObra.Items.Clear();
            listOF.Items.Clear();
            if (cboOF.SelectedItem.ToString() != "Seleccionar")
            {        
                if (cboServicio.SelectedValue.ToString() == "6")
            {        
                dtof = CS.Obtener_OF_Garantia(cboOF.SelectedItem.ToString());
                LblOfGarantia.Text = dtof.Rows[0][3].ToString() + " " + dtof.Rows[0][2].ToString();
            }
            else
            {
                    LblOfGarantia.Text = "";
            }
            }
            else
            {
                LblOfGarantia.Text = "";
            }
        }

        protected void cboObra_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboObra.SelectedItem.ToString() != "Seleccionar")
            {
                if (cboServicio.SelectedValue.ToString() == "6")
                {
                   
                }
                else
                {
                    String[] idObra = cboObra.SelectedValue.ToString().Split('|');
                    cargarCombo(CS.cargarOf("AND  (obra.obr_id = " + idObra[0] + ")", ""), cboOF, 0, 1);//Combo Ofs
                }             
            }
            else
            {
                cboOF.Items.Clear();
            }
        }

        protected void btnAggObra_Click(object sender, EventArgs e)
        {
            if (cboObra.SelectedItem.ToString() != "Seleccionar" && listObra.Items.Count <= 1)
            {
                String[] idObra = cboObra.SelectedValue.ToString().Split('|');
                agregarLista(listObra, idObra[1], idObra[0]);//obra
            }
            else
            {
                mensajeVentana("No es posible añadir más de una obra. Gracias");
            }
        }

        protected void btnEliObra_Click(object sender, EventArgs e)
        {
            int idObra = listObra.SelectedIndex;
            if (idObra >= 0)
            {
                DataTable ofs = CS.cargarOf("AND  (obra.obr_id = " + listObra.SelectedValue.ToString() + ")", "");
                ListBox listReal = new ListBox();
                foreach (ListItem li in listOF.Items)
                {
                    int agg = 0;//Inicio la variable en 0 que significa que no agrege
                    foreach (DataRow row in ofs.Rows)
                    {
                        if (li.Value == row["idOF"].ToString())//Si encuentra la of, para el ciclo y mando la variable agg en 0 para que no agrege
                        {
                            agg = 0;
                            break;
                        }
                        else
                        {
                            agg = agg + 1;//Si no existe, va contanto hasta que pare y sea mayor q cero o que pare y cambie la variable en 0
                        }
                    }
                    if (agg >= 1)//si agg esta en 0 no agrego, si mayor agrego solo una vez
                    {
                        listReal.Items.Add(new ListItem(li.Text, li.Value));
                    }
                }
                listOF.Items.Clear();//Elimino todas las ofs
                foreach (ListItem li2 in listReal.Items)//Vuelvo y agrego las ofs que son
                {
                    listOF.Items.Add(new ListItem(li2.Text, li2.Value));
                }
            }
            //Borro el item seleccionado
            eliminarLista(listObra);
        }

        protected void btnAggOF_Click(object sender, EventArgs e)
        {
            if (cboOF.SelectedItem.ToString() != "Seleccionar")
            {
                agregarLista(listOF, cboOF.SelectedItem.ToString(), cboOF.SelectedValue.ToString());//OF
                DataTable obra = null;
                obra = CS.cargarObras(" (Orden.Id_Ofa = " + cboOF.SelectedValue.ToString() + ")");
                foreach (DataRow row in obra.Rows)
                {
                    agregarLista(listObra, row["datosObra"].ToString(), row["idObra"].ToString());//Obra
                }
            }
        }

        protected void btnEliOF_Click(object sender, EventArgs e)
        {
            //Borro el item seleccionado
            eliminarLista(listOF);
        }

        protected void txtTecnicos_TextChanged(object sender, EventArgs e)
        {
            calcular();           
        }

        protected void btnCotizar_Click(object sender, EventArgs e)
        {
            int idCotizacion = -1;
            string estado = "";
            string msj = "";
            bool facturable;
            if (chksFacturable.Checked == true)
            {
                facturable = true;
            }
            else
            {
                facturable = false;
            }
            if (cboServicio.SelectedIndex != 0)
            {
                if (!String.IsNullOrEmpty(lblIdCliente.Value) && !String.IsNullOrEmpty(txtCliente.Text))
                {
                    if (!String.IsNullOrEmpty(txtDias.Text) && txtDias.Text != "0")
                    {
                        if (!String.IsNullOrEmpty(txtTecnicos.Text) && txtTecnicos.Text != "0")
                        {
                            try
                            {
                                if (Convert.ToDouble(lblTotal.Text) > 0)
                                    idCotizacion = CS.insertarCotizacion(Convert.ToInt32(cboServicio.SelectedItem.Value), Convert.ToInt32(lblIdCliente.Value), 
                                                                         Convert.ToInt32(txtTecnicos.Text), Convert.ToDouble(txtHonorarios.Text),
                                                                         Convert.ToDouble(txtTiquetes.Text), Convert.ToDouble(lblTotal.Text), 
                                                                         Session["usuario"].ToString(), Convert.ToInt32(txtDias.Text),
                                                                         Convert.ToInt32(lblIdMoneda.Value), 1, 1, txtObservacion.Text,facturable);
                                else
                                {
                                    if (!String.IsNullOrEmpty(txtObservacion.Text))
                                        idCotizacion = CS.insertarCotizacion(Convert.ToInt32(cboServicio.SelectedItem.Value), Convert.ToInt32(lblIdCliente.Value),
                                                                             Convert.ToInt32(txtTecnicos.Text), Convert.ToDouble(txtHonorarios.Text),
                                                                             Convert.ToDouble(txtTiquetes.Text), Convert.ToDouble(lblTotal.Text),
                                                                             Session["usuario"].ToString(), Convert.ToInt32(txtDias.Text), 
                                                                             Convert.ToInt32(lblIdMoneda.Value), 1, 1, txtObservacion.Text, facturable);
                                    else
                                        mensajeVentana("Por favor ingrese en el campo Observación el motivo del costo total igual a 0. Gracias");
                                }

                                if (idCotizacion != -1)
                                {
                                    if (listObra.Items.Count > 0)
                                    {
                                        CS.insertarCotizacionObra(idCotizacion, listObra);
                                    }

                                    if (listOF.Items.Count > 0)
                                    {
                                        CS.insertarCotizacionOrden(idCotizacion, listOF);
                                    }

                                    txtCotizacion.Text = idCotizacion.ToString();

                                    string version = "A";
                                    int evento = 34;
                                    string correos = "";
                                    if (listProces.Items.Count > 0)
                                    {
                                        correos = CS.consultarCorreoUsuarios(listProces);
                                    }
                                    CorreoSIAT(Convert.ToInt32(txtCotizacion.Text), version, evento, correos, Convert.ToInt32(lblIdPais.Value), Convert.ToInt32(cboServicio.SelectedValue));
                                    msj = "Cotización creada con éxito.";
                                    btnBuscar_Click(sender, e);                                  
                                    mensajeVentana(msj);
                                }
                                else
                                {
                                    Session["CotizacionSIAT"] = null;
                                    Session["CotizacionSIATId"] = null;
                                    Session["EstadoCotizacion"] = null;
                                    Session["EstadoCotizacionId"] = null;
                                    msj = "Error creando la cotización. Por favor intente nuevamente. Gracias";
                                    mensajeVentana(msj);
                                }
                            }
                            catch
                            {
                                Session["CotizacionSIAT"] = null;
                                Session["CotizacionSIATId"] = null;
                                Session["EstadoCotizacion"] = null;
                                Session["EstadoCotizacionId"] = null;
                            }
                        }
                        else
                        {
                            msj = "La cantidad de tecnicos debe ser superior a 0. Gracias";
                            mensajeVentana(msj);
                        }
                    }
                    else
                    {
                        msj = "La cantidad de días debe ser superior a 0. Gracias";
                        mensajeVentana(msj);
                    }                    
                }
                else
                {
                    msj = "Debe seleccionar un Cliente. Gracias";
                    mensajeVentana(msj);
                }
            }
            else
            {
                msj = "Debe seleccionar un Servicio. Gracias";
                mensajeVentana(msj);
            }            
        }

        //metodo general para agregar las listas limitada (No permite repeticion)
        private void agregarLista(ListBox list, String item, String value)
        {
            if (value != "")//verifico si hay algo en el id del combo
            {
                if (list.Items.Count >= 1)//verifico si hay algo en la lista que ingresa en la variable list
                {
                    String existe = "NO";
                    foreach (ListItem li in list.Items)//lleno y recorro un list por medio del la lista que esta entrando
                    {
                        if (li.Value == value.ToString())//verifico si el id ya esta en la lista
                        {
                            existe = "SI";
                        }
                    }
                    if (existe == "NO")//agrego a la lista el item y el value si no existe en lista
                    {
                        list.Items.Add(new ListItem(item.ToString(), value.ToString()));
                    }
                }
                else//si no hay agrego inmediatamente
                {
                    list.Items.Add(new ListItem(item.ToString(), value.ToString()));
                }
            }
        }
        //elimina el item selecionado
        private void eliminarLista(ListBox list)
        {
            for (int i = 0; i < list.Items.Count; i++)
            {
                if (list.Items[i].Selected)
                {
                    list.Items.Remove(list.Items[i]);
                }
            }
        }

        protected void txtHonorarios_TextChanged(object sender, EventArgs e)
        {
            calcular();
        }

        protected void txtTiquetes_TextChanged(object sender, EventArgs e)
        {
            calcular();
        }

        private void calcular()
        {
            double totalTec = 0;
            double totalTiquete = 0;
            double total = 0;
            if (!String.IsNullOrEmpty(txtDias.Text) && txtDias.Text != "0")
            {
                if (!String.IsNullOrEmpty(txtTecnicos.Text) && txtTecnicos.Text != "0" && !String.IsNullOrEmpty(txtHonorarios.Text) && txtHonorarios.Text != "0")
                {
                    totalTec = Convert.ToInt32(txtTecnicos.Text) * Convert.ToDouble(txtHonorarios.Text) * Convert.ToInt32(txtDias.Text);
                    lblTotalTecnico.Text = totalTec.ToString();
                    total += totalTec;
                    lblTotal.Text = total.ToString();
                    if (!String.IsNullOrEmpty(txtTiquetes.Text) && txtTiquetes.Text != "0")
                    {
                        totalTiquete = Convert.ToDouble(txtTiquetes.Text) * Convert.ToInt32(txtTecnicos.Text);
                        lblTotalTiquete.Text = totalTiquete.ToString();
                        total += totalTiquete;
                        lblTotal.Text = total.ToString();
                    }
                    else
                    {
                        lblTotal.Text = total.ToString();
                    }
                }
                else
                {
                    lblTotalTecnico.Text = totalTec.ToString();
                    if (!String.IsNullOrEmpty(txtTiquetes.Text) && txtTiquetes.Text != "0")
                    {
                        totalTiquete = Convert.ToDouble(txtTiquetes.Text) * Convert.ToInt32(txtTecnicos.Text);
                        lblTotalTiquete.Text = totalTiquete.ToString();
                        total += totalTiquete;
                        lblTotal.Text = total.ToString();
                    }
                    else
                    {
                        lblTotal.Text = total.ToString();
                    }
                }
            }
            else
            {
                lblTotal.Text = total.ToString();
            }            
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            string msj = "";
            int id = -1;
            int estado = 1;
            bool facturable;

            if (chksFacturable.Checked== true)
            {
                facturable = true;
            }
            else
            {
                facturable =false;
            }

            if (!String.IsNullOrEmpty(txtCotizacion.Text))
            {
                if (cboServicio.SelectedIndex != 0)
                {
                    if (!String.IsNullOrEmpty(lblIdCliente.Value) && !String.IsNullOrEmpty(txtCliente.Text))
                    {
                        if (!String.IsNullOrEmpty(txtDias.Text) && txtDias.Text != "0")
                        {
                            if (!String.IsNullOrEmpty(txtTecnicos.Text) && txtTecnicos.Text != "0")
                            {
                                try
                                {
                                    if (chkAprobar.Checked)
                                        estado = 2;
                                    else if (chkRechazar.Checked)
                                        estado = 3;
                                    else
                                        estado = 1;

                                    if(estado == 3 && String.IsNullOrEmpty(txtMotivo.Text.Trim()))
                                    {
                                        msj = "Por favor escriba el motivo del rechazo. Gracias";
                                        mensajeVentana(msj);
                                    }

                                    else
                                    {
                                        id = CS.ActualizarCotizacion(Convert.ToInt32(txtCotizacion.Text), Convert.ToInt32(cboServicio.SelectedItem.Value),
                                                                     Convert.ToInt32(lblIdCliente.Value), Convert.ToInt32(txtTecnicos.Text), 
                                                                     Convert.ToDouble(txtHonorarios.Text), Convert.ToDouble(txtTiquetes.Text),
                                                                     Convert.ToDouble(lblTotal.Text), Convert.ToInt32(txtDias.Text),
                                                                     Convert.ToInt32(lblIdMoneda.Value), estado, txtMotivo.Text,
                                                                     Session["usuario"].ToString(), txtObservacion.Text,facturable);

                                        if (id != -1)
                                        {
                                            CS.actualizaCotizacionrObra(Convert.ToInt32(txtCotizacion.Text), listObra);
                                            CS.actualizarCotizacionOrden(Convert.ToInt32(txtCotizacion.Text), listOF);

                                            if (chkAprobar.Checked)
                                            {
                                                string version = "A";
                                                int evento = 35;
                                                string correos = "";
                                                if (listProces.Items.Count > 0)
                                                {
                                                    correos = CS.consultarCorreoUsuarios(listProces);
                                                }
                                                CorreoSIAT(Convert.ToInt32(txtCotizacion.Text), version, evento, correos, Convert.ToInt32(lblIdPais.Value),Convert.ToInt32(cboServicio.SelectedValue));                                               
                                            }

                                            else if (chkRechazar.Checked)
                                            {
                                                string version = "A";
                                                int evento = 36;
                                                string correos = "";
                                                if (listProces.Items.Count > 0)
                                                {
                                                    correos = CS.consultarCorreoUsuarios(listProces);
                                                }
                                                CorreoSIAT(Convert.ToInt32(txtCotizacion.Text), version, evento, correos, Convert.ToInt32(lblIdPais.Value),Convert.ToInt32(cboServicio.SelectedValue));
                                            }

                                            msj = "Cotización actualizada con éxito.";
                                            mensajeVentana(msj);
                                            btnBuscar_Click(sender, e);
                                        }

                                        else
                                        {
                                            Session["CotizacionSIAT"] = null;
                                            Session["CotizacionSIATId"] = null;
                                            Session["EstadoCotizacion"] = null;
                                            Session["EstadoCotizacionId"] = null;
                                            msj = "Error actualizando la cotización. Por favor intente nuevamente. Gracias";
                                            mensajeVentana(msj);
                                        }
                                    }                                   
                                }
                                catch (Exception ex)
                                {
                                    Session["CotizacionSIAT"] = null;
                                    Session["CotizacionSIATId"] = null;
                                    Session["EstadoCotizacion"] = null;
                                    Session["EstadoCotizacionId"] = null;
                                }
                            }
                            else
                            {
                                msj = "La cantidad de tecnicos debe ser superior a 0. Gracias";
                                mensajeVentana(msj);
                            }
                        }
                        else
                        {
                            msj = "La cantidad de días debe ser superior a 0. Gracias";
                            mensajeVentana(msj);
                        }
                    }
                    else
                    {
                        msj = "Debe seleccionar un Cliente. Gracias";
                        mensajeVentana(msj);
                    }
                }
                else
                {
                    msj = "Debe seleccionar un Servicio. Gracias";
                    mensajeVentana(msj);
                }
            }
            else
            {
                msj = "Por favor vuelve a cargar la cotización. Gracias";
                mensajeVentana(msj);
            }            
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Response.Redirect("SiatCotizacion.aspx");
          
        }

        private void limpiarCampos()
        {
            txtCliente.Text = "";
            txtCotizacion.Text = "";
            txtHonorarios.Text = "0";
            txtTecnicos.Text = "1";
            txtTiquetes.Text = "0";
            txtDias.Text = "0";
            lblIdCliente.Value = "";
            lblIdMoneda.Value = "";
            lblIdPais.Value = "";
            lblMoneda.Text = "";
            lblTotal.Text = "0";
            lblTotalTecnico.Text = "0";
            lblTotalTiquete.Text = "0";
            lblCotizacion.Text = "";
            cboObra.Items.Clear();
            cboOF.Items.Clear();
            listObra.Items.Clear();
            listOF.Items.Clear();
            chkAprobar.Checked = false;
            chkRechazar.Checked = false;
            txtMotivo.Text = "";
            txtMotivo.Visible = false;
            lblMotivo.Visible = false;
            PanelCarta.Visible = false;
            cargarCombo(CS.cargarServicios(), cboServicio, 0, 1);//Combo Servicio  
            txtPart.Text = "";
            listProces.Items.Clear();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            DataTable cotizacion = new DataTable();
            DataTable obra = new DataTable();
            DataTable orden = new DataTable();

            int estadoId = 1;
            if (!String.IsNullOrEmpty(txtCotizacion.Text))
            {
                cotizacion = CS.buscarCotizacion(Convert.ToInt32(txtCotizacion.Text));

                if (cotizacion.Rows.Count > 0)
                {
                    limpiarCampos();
                    btnCotizar.Enabled = false;
                    txtCotizacion.Text = cotizacion.Rows[0]["siat_cotizacion_id"].ToString();
                    txtCliente.Text = cotizacion.Rows[0]["cliente"].ToString();
                    lblIdCliente.Value = cotizacion.Rows[0]["cliente_id"].ToString();
                    DataTable dtof;
                    if (cboServicio.SelectedValue.ToString() == "6")
                    {
                        dtof = CS.Obtener_OF_Garantia(cboOF.SelectedValue.ToString());
                        LblOfGarantia.Text = dtof.Rows[0][2].ToString() + " " + dtof.Rows[0][1].ToString();
                    }
                    else
                    {
                        LblOfGarantia.Text = "";
                    }
                    txtCliente_TextChanged(sender, e);
                    try
                    {
                        cboServicio.SelectedValue = cotizacion.Rows[0]["servicio_id"].ToString();
                    }
                    catch
                    {
                        cargarCombo(CS.cargarServiciosTodos(), cboServicio, 0, 1);//Combo Servicio  


                        DataTable dt = new DataTable();
                        dt = CS.cargarOrdenesCotizacionViaje(Convert.ToInt32(txtCotizacion.Text));

                        if (dt.Rows.Count > 0)
                        {
                            DataTable dtObras = new DataTable();
                            DataTable dtOFs = new DataTable();
                            dtObras = CS.cargarObras(" (Orden.Id_Ofa IN(" + dt.Rows[0]["ordenes"].ToString() + "))");
                            if (dtObras.Rows.Count > 0)
                            {
                                llenarList(dtObras, listObra, 3, 0);
                            }
                            dtOFs = CS.cargarOf("", " AND (Orden.Id_Ofa IN(" + dt.Rows[0]["ordenes"].ToString() + "))");
                            if (dtOFs.Rows.Count > 0)
                            {
                                llenarList(dtOFs, listOF, 1, 0);
                            }
                        }
                    }

                    if (cboServicio.SelectedValue.ToString() == "6")
                    {
                        if (CS.Obtener_OF_Garantia(cboOF.SelectedValue.ToString()).Rows.Count != 0)
                        {
                            dtof = CS.Obtener_OF_Garantia(cboOF.SelectedValue.ToString());
                            LblOfGarantia.Text = dtof.Rows[0][2].ToString() + " " + dtof.Rows[0][1].ToString();
                        }
                        else
                        {
                            LblOfGarantia.Text = "";
                        }
                    }
                    else
                    {
                        LblOfGarantia.Text = "";
                    }

                    lblIdMoneda.Value = cotizacion.Rows[0]["moneda_id"].ToString();
                    txtTecnicos.Text = cotizacion.Rows[0]["tecnicos"].ToString();
                    bool facturable = Convert.ToBoolean(cotizacion.Rows[0]["siat_facturable"]);
                    chksFacturable.Checked = true;
                    if (facturable == true)
                    {
                        chksFacturable.Checked = true;
                    }
                    else
                    {
                        chksFacturable.Checked = false;
                    }
                    chksFacturable.Enabled = true;
                    txtHonorarios.Text = Convert.ToDouble(cotizacion.Rows[0]["honorarios"]).ToString("N2", new CultureInfo("en-US"));
                    txtTiquetes.Text = Convert.ToDouble(cotizacion.Rows[0]["tiquete"]).ToString("N2", new CultureInfo("en-US"));
                    txtDias.Text = cotizacion.Rows[0]["dias"].ToString();
                    double totalTec = Convert.ToInt32(txtDias.Text) * Convert.ToDouble(txtHonorarios.Text) * Convert.ToInt32(txtTecnicos.Text);
                    lblTotalTecnico.Text = totalTec.ToString("N2", new CultureInfo("en-US"));
                    double totalTiquete = Convert.ToDouble(txtTiquetes.Text) * Convert.ToInt32(txtTecnicos.Text);
                    lblTotalTiquete.Text = totalTiquete.ToString("N2", new CultureInfo("en-US"));
                    double total = totalTiquete + totalTec;
                    lblTotal.Text = total.ToString("N2", new CultureInfo("en-US"));
                    txtObservacion.Text = cotizacion.Rows[0]["observacion"].ToString();

                    if (!String.IsNullOrEmpty(cotizacion.Rows[0]["estado_id"].ToString()))
                    {
                        estadoId = Convert.ToInt32(cotizacion.Rows[0]["estado_id"]);



                        if (estadoId == 2)
                        {
                            chkAprobar.Checked = true;
                            chkAprobar_CheckedChanged(sender, e);
                            btnActualizar.Enabled = false;
                            PanelCliente.Enabled = false;
                            PanelTecnico.Enabled = false;
                            panelCorreo.Visible = true;
                        }

                        else if (estadoId == 3)
                        {
                            chkRechazar.Checked = true;
                            txtMotivo.Text = cotizacion.Rows[0]["motivo_rechazo"].ToString();
                            chkRechazar_CheckedChanged(sender, e);
                            btnActualizar.Enabled = false;
                            PanelCliente.Enabled = false;
                            PanelTecnico.Enabled = false;
                            panelCorreo.Visible = true;
                        }

                        else
                        {
                            cboServicio.SelectedValue = cotizacion.Rows[0]["servicio_id"].ToString();
                            if (chksFacturable.Checked == true)
                            {
                                facturable = true;
                            }
                            else
                            {
                                facturable = false;
                            }

                            int rol = (int)Session["Rol"];

                            //Implemen Obra Acicional no facturable aprueba asistente comercial o gerente regional
                            if (( (rol == 9 || rol == 38) && cboServicio.SelectedValue.ToString() == "1" && facturable == false) ||
                                (rol == 2 && cboServicio.SelectedValue.ToString() == "1" && facturable == false))
                            {
                                btnActualizar.Enabled = true;
                                chkAprobar.Enabled = true;
                                chkRechazar.Enabled = true;
                            }
                            //Implemen Obra Adicional  facturable aprueba asistente comercial o soporte tecnico
                            else if (((rol == 9 || rol == 38) && cboServicio.SelectedValue.ToString() == "1" && facturable == true) ||
                                (rol == 2 && cboServicio.SelectedValue.ToString() == "1" && facturable == true))
                            {
                                btnActualizar.Enabled = true;
                                chkAprobar.Enabled = true;
                                chkRechazar.Enabled = true;
                            }
                            //Implemen Obra Adicional  facturable aprueba soporte tecnico
                            else if (rol == 38 && cboServicio.SelectedValue.ToString() == "1" && facturable == true) 
                            {
                                btnActualizar.Enabled = false;
                                chkAprobar.Enabled = false;
                                chkRechazar.Enabled = true;
                            }
                            //Dias Adicionales no facturables aprueba soporte tecnico
                            else if (rol == 38 && cboServicio.SelectedValue.ToString() == "12" && facturable == false)
                            {
                                btnActualizar.Enabled = true;
                                chkAprobar.Enabled = true;
                                chkRechazar.Enabled = true;
                            }
                            //Apoyo a Infraestructura o Arrendadora aprueba Arrendadora
                            else if ((rol == 24 && cboServicio.SelectedValue.ToString() == "3") || (rol == 24 && cboServicio.SelectedValue.ToString() == "4") ||
                                (rol == 26 && cboServicio.SelectedValue.ToString() == "3") || (rol == 26 && cboServicio.SelectedValue.ToString() == "4") ||
                                 (rol == 3 && cboServicio.SelectedValue.ToString() == "3") || (rol == 3 && cboServicio.SelectedValue.ToString() == "4") ||
                                  (rol == 33 && cboServicio.SelectedValue.ToString() == "4") ||
                               (rol == 49 && cboServicio.SelectedValue.ToString() == "3") || (rol == 49 && cboServicio.SelectedValue.ToString() == ""))
                            {
                                btnActualizar.Enabled = true;
                                chkAprobar.Enabled = true;
                                chkRechazar.Enabled = true;
                            }
                            //Garantias no facturables aprueba gestion integral o gestion calidad
                            else if ((rol == 35 && cboServicio.SelectedValue.ToString() == "6" && facturable == false) ||
                               (rol == 18 && cboServicio.SelectedValue.ToString() == "6" && facturable == false))
                            {
                                btnActualizar.Enabled = true;
                                chkAprobar.Enabled = true;
                                chkRechazar.Enabled = true;
                            }
                            //Apoyo Innovacion y Desarrollo aprueba soporte tecnico o jefe regional de ventas
                            else if (rol == 38 && cboServicio.SelectedValue.ToString() == "5" && facturable == false) 
                            {
                                btnActualizar.Enabled = true;
                                chkAprobar.Enabled = true;
                                chkRechazar.Enabled = true;
                            }
                            else
                            {
                                PanelCliente.Enabled = true;
                                PanelTecnico.Enabled = true;
                                btnActualizar.Enabled = true;
                                panelCorreo.Visible = true;
                            }
                        }
                    }

                    obra = CS.buscarCotizacionObra(Convert.ToInt32(txtCotizacion.Text));
                    if (obra.Rows.Count > 0)
                    {
                        for (int i = 0; i < obra.Rows.Count; i++)
                        {
                            listObra.Items.Add(new ListItem(obra.Rows[i]["obr_nombre"].ToString(), obra.Rows[i]["siat_obra_id"].ToString()));
                        }
                    }

                    orden = CS.buscarCotizacionOrden(Convert.ToInt32(txtCotizacion.Text));
                    if (orden.Rows.Count > 0)
                    {
                        for (int i = 0; i < orden.Rows.Count; i++)
                        {
                            listOF.Items.Add(new ListItem(orden.Rows[i]["siat_orden"].ToString(), orden.Rows[i]["siat_orden_id"].ToString()));
                        }
                    }

                    if (CS.buscarCotizacionOrden(Convert.ToInt32(txtCotizacion.Text)).Rows.Count != 0)
                    {
                        if (cboServicio.SelectedValue.ToString() == "6")
                        {
                            if (CS.Obtener_OF_Garantia(orden.Rows[0][2].ToString()).Rows.Count != 0)
                            {
                                dtof = CS.Obtener_OF_Garantia(orden.Rows[0][2].ToString());
                                LblOfGarantia.Text = dtof.Rows[0][3].ToString() + " " + dtof.Rows[0][2].ToString();
                            }
                            else
                            {

                            }

                        }
                        else
                        {
                            LblOfGarantia.Text = "";
                        }
                    }

                    Session["CotizacionSIAT"] = cotizacion.Rows[0]["consecutivo"].ToString();
                    Session["CotizacionSIATId"] = txtCotizacion.Text;
                    Session["EstadoCotizacion"] = cotizacion.Rows[0]["estado"].ToString();
                    Session["EstadoCotizacionId"] = cotizacion.Rows[0]["estado_id"];
                    validarRol(estadoId);
                    setMensaje();
                    Session["CotizacionSIAT"] = null;
                    Session["CotizacionSIATId"] = null;
                    Session["EstadoCotizacion"] = null;
                    Session["EstadoCotizacionId"] = null;
                }
                else
                {
                    Session["CotizacionSIAT"] = null;
                    Session["CotizacionSIATId"] = null;
                    Session["EstadoCotizacion"] = null;
                    Session["EstadoCotizacionId"] = null;
                    string msj = "El número de cotización no es válido. Intente nuevamente. Gracias";
                    mensajeVentana(msj);
                }                
            }
            else
            {
                Session["CotizacionSIAT"] = null;
                Session["CotizacionSIATId"] = null;
                Session["EstadoCotizacion"] = null;
                Session["EstadoCotizacionId"] = null;
                btnLimpiar_Click(sender, e);
                string msj = "Por favor digite el número de cotización. Intente nuevamente. Gracias";
                mensajeVentana(msj);
            }            
        }

        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }

        protected void txtDias_TextChanged(object sender, EventArgs e)
        {
            calcular();
        }

        protected void chkAprobar_CheckedChanged(object sender, EventArgs e)
        {
            string msj = "";
            if (!String.IsNullOrEmpty(txtCotizacion.Text))
            {
                if (chkAprobar.Checked)
                {
                    chkRechazar.Checked = false;
                    txtMotivo.Text = "";
                    txtMotivo.Visible = false;
                    lblMotivo.Visible = false;
                }
            }
            else
            {
                chkAprobar.Checked = false;
                msj = "Por favor cargar la cotización. Gracias";
                mensajeVentana(msj);
            }            
        }

        protected void chkRechazar_CheckedChanged(object sender, EventArgs e)
        {
            string msj = "";
            if (!String.IsNullOrEmpty(txtCotizacion.Text))
            {
                if (chkRechazar.Checked)
                {
                    chkAprobar.Checked = false;
                    txtMotivo.Visible = true;
                    lblMotivo.Visible = true;
                }
                else
                {
                    txtMotivo.Text = "";
                    txtMotivo.Visible = false;
                    lblMotivo.Visible = false;
                }
            }
            else
            {
                chkRechazar.Checked = false;
                msj = "Por favor cargar la cotización. Gracias";
                mensajeVentana(msj);
            }
        }

        protected void txtPart_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtPart.Text.Trim()))
            { lblIdPart.Value = null; }
        }

        protected void btnAggProces_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtPart.Text.Trim()) && !String.IsNullOrEmpty(lblIdPart.Value.Trim()))
            {
                agregarLista(listProces, txtPart.Text, lblIdPart.Value);
                txtPart.Text = "";
                lblIdPart.Value = "";
            }

            else
            {
                txtPart.Text = "";
                lblIdPart.Value = "";
                mensajeVentana("Por favor seleccione el acompañante nuevamente.");
            }
        }

        protected void btnEliProces_Click(object sender, EventArgs e)
        {
            eliminarLista(listProces);
        }

        protected void btnCorreo_Click(object sender, EventArgs e)
        {
            string version = "A";
            int evento = 34;
            string msj = "";
            string correos = "";
            if (!String.IsNullOrEmpty(txtCotizacion.Text))
            {
                if (listProces.Items.Count > 0)
                {
                    correos = CS.consultarCorreoUsuarios(listProces);
                }

                CorreoSIAT(Convert.ToInt32(txtCotizacion.Text), version, evento, correos, Convert.ToInt32(lblIdPais.Value),Convert.ToInt32(cboServicio.SelectedValue));
            }
            else
            {
                msj = "Por favor cargue nuevamente la cotización. Gracias";
                mensajeVentana(msj);
            }
        }

        private void CorreoSIAT(int id, string version, int evento, string correosAdd, int pais, int servicio)
        {
            string Nombre = (string)Session["Nombre_Usuario"];
            string CorreoUsuario = (string)Session["rcEmail"];
            int parte = 0;

            string correoSistema = (string)Session["CorreoSistema"];
            string UsuarioAsunto = (string)Session["UsuarioAsunto"];
            
            string destinatarios = "";

           //int EncabezadoFUP = controlfup.EncabezadoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim());
           // Parametros de la BBDD
           SqlParameter[] sqls = new SqlParameter[8];
            sqls[0] = new SqlParameter("@pID ", id);
            sqls[1] = new SqlParameter("@pVersion", version);
            sqls[2] = new SqlParameter("@pEvento", evento);
            sqls[3] = new SqlParameter("@pUsuario", UsuarioAsunto);
            sqls[4] = new SqlParameter("@pRemitente", CorreoUsuario);
            sqls[5] = new SqlParameter("@pParte", parte);
            sqls[6] = new SqlParameter("@paisCliente", pais);
            sqls[7] = new SqlParameter("@servicio", servicio);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_SIAT_notificaciones", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(sqls);

                    SqlParameter Asunto = new SqlParameter("@pAsun_mail", SqlDbType.VarChar, 200);
                    SqlParameter Destinatarios = new SqlParameter("@pLista", SqlDbType.VarChar, 12500);                    
                    SqlParameter Mensaje = new SqlParameter("@pMsg", SqlDbType.VarChar, 12500);
                    SqlParameter Anexo = new SqlParameter("@pAnexo", SqlDbType.Bit);
                    SqlParameter LinkAnexo = new SqlParameter("@pLink_anexo", SqlDbType.VarChar, 250);

                    Asunto.Direction = ParameterDirection.Output;
                    Destinatarios.Direction = ParameterDirection.Output;
                    Mensaje.Direction = ParameterDirection.Output;
                    Anexo.Direction = ParameterDirection.Output;
                    LinkAnexo.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(Asunto);
                    cmd.Parameters.Add(Destinatarios);
                    cmd.Parameters.Add(Mensaje);
                    cmd.Parameters.Add(Anexo);
                    cmd.Parameters.Add(LinkAnexo);

                    // Abrimos la conexión y ejecutamos el ExecuteReader
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //VALORES DEL ENCABEZADO 
                        string AsuntoMail = Convert.ToString(Asunto.Value);
                        string DestinatariosMail = Convert.ToString(Destinatarios.Value);
                        string MensajeMail = Convert.ToString(Mensaje.Value);
                        bool llevaAnexo = Convert.ToBoolean(Anexo.Value);
                        string EnlaceAnexo = Convert.ToString(LinkAnexo.Value);
                        string tipoAdjunto = "";

                        Byte[] correo = new Byte[0];
                        WebClient clienteWeb = new WebClient();
                        clienteWeb.Dispose();
                        clienteWeb.Credentials = new System.Net.NetworkCredential("forsa", "F0rUs3s4*", "FORSA");
                        // Adjunto
                        //DEFINIMOS LA CLASE DE MAILMESSAGE
                        MailMessage mail = new MailMessage();
                        //INDICAMOS EL EMAIL DE ORIGEN
                        mail.From = new MailAddress(correoSistema);

                        destinatarios = DestinatariosMail;

                        if (!String.IsNullOrEmpty(correosAdd))
                        {
                            destinatarios += "," + correosAdd;
                        }

                        //AÑADIMOS LA DIRRECCIÓN DE CORREO DESTINATARIO
                        mail.To.Add(destinatarios);
                        //INCLUIMOS EL ASUNTO DEL MENSAJE
                        mail.Subject = AsuntoMail;
                        //AÑADIMOS EL CUERPO DEL MENSAJE
                        mail.Body = MensajeMail;
                        //INDICAMOS EL TIPO DE CODIFICACIÓN DEL MENSAJE
                        mail.BodyEncoding = System.Text.Encoding.UTF8;
                        //DEFINIMOS LA PRIORIDAD DEL MENSAJE
                        mail.Priority = System.Net.Mail.MailPriority.Normal;
                        //INDICAMOS SI EL CUERPO DEL MENSAJE ES HTML O NO
                        mail.IsBodyHtml = true;
                        //ADJUNTAMOS EL ARCHIVO
                        MemoryStream ms = new MemoryStream();
                        if (llevaAnexo == true)
                        {
                            string enlace = "";

                            if (evento == 34 || evento == 35)
                            {
                                tipoAdjunto = "SIAT COTIZACIÓN No. ";
                                enlace = @"http://10.75.131.2:81/ReportServer?" + EnlaceAnexo.Trim() + "&rs:format=PDF&rs:command=render&rs:ClearSession=false&id=" + id.ToString();

                                correo = clienteWeb.DownloadData(enlace);
                                ms = new MemoryStream(correo);
                                mail.Attachments.Add(new Attachment(ms, tipoAdjunto + " " + id.ToString() + ".pdf"));  
                            }                   
                        }
                        //DEFINIMOS NUESTRO SERVIDOR SMTP
                        //DECLARAMOS LA CLASE SMTPCLIENT
                        SmtpClient smtp = new SmtpClient();
                        //DEFINIMOS NUESTRO SERVIDOR SMTP
                        smtp.Host = "smtp.office365.com";
                        //INDICAMOS LA AUTENTICACION DE NUESTRO SERVIDOR SMTP
                        smtp.Credentials = new System.Net.NetworkCredential("monitoreo@forsa.net.co", "Those7953");
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        //smtp.Timeout =

                        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain,
                            SslPolicyErrors sslPolicyErrors)
                        {
                            return true;
                        };

                        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        try
                        {
                           // smtp.SendAsync(mail, mail.To);
                           smtp.Send(mail);
                        }
                        catch (Exception ex)
                        {
                            string mensaje = "ERROR: " + ex.Message;
                        }
                        ms.Close();
                    }
                }
            }
        }

        //llena las listas
        private void llenarList(DataTable tabla, ListBox list, int text, int value)
        {   // 1/0  -- nom/id
            list.Items.Clear();
            foreach (DataRow row in tabla.Rows)
            {
                list.Items.Add(new ListItem(row[text].ToString(), row[value].ToString()));
            }
        }

        private void setMensaje()
        {
            if (Session["EstadoCotizacion"] != null)
            {
                validarRol(Convert.ToInt32(Session["EstadoCotizacionId"].ToString()));

                if (Session["CotizacionSIAT"] != null)
                {
                    lblCotizacion.Text = "Cotización No. : " + Session["CotizacionSIAT"].ToString() + " / Estado: " + Session["EstadoCotizacion"].ToString();
                    cargarCarta(Session["CotizacionSIATId"].ToString());
                    Session["CotizacionSIAT"] = null;
                    Session["CotizacionSIATId"] = null;
                    Session["EstadoCotizacion"] = null;
                    Session["EstadoCotizacionId"] = null;
                }
            }              
        }

        public void cargarCarta(string idCotizacion)
        {
            PanelCarta.Visible = true;
            reporteCarta.Visible = true;
            List<ReportParameter> parametro = new List<ReportParameter>();
            parametro.Add(new ReportParameter("id", idCotizacion, true));

            reporteCarta.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            reporteCarta.ServerReport.ReportServerCredentials = irsc;
            reporteCarta.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            reporteCarta.ServerReport.ReportPath = "/PostVenta/SIAT_Carta_Cotizacion";
            this.reporteCarta.ServerReport.SetParameters(parametro);
            reporteCarta.ShowToolBar = true;
        }

        public class CustomReportCredentials : IReportServerCredentials
        {
            private string _UserName;
            private string _PassWord;
            private string _DomainName;
            public CustomReportCredentials(string UserName, string PassWord, string DomainName)
            {
                _UserName = UserName;
                _PassWord = PassWord;
                _DomainName = DomainName;
            }

            public System.Security.Principal.WindowsIdentity ImpersonationUser
            {
                get { return null; }
            }
            public ICredentials NetworkCredentials
            {
                get { return new NetworkCredential(_UserName, _PassWord, _DomainName); }
            }
            public bool GetFormsCredentials
                (
                out Cookie authCookie,
                out string user,
                out string password,
                out string authority
                )
            { authCookie = null; user = password = authority = null; return false; }
        }


        protected void cboServicio_TextChanged(object sender, EventArgs e)
        {
            int servicio = Convert.ToInt32(cboServicio.SelectedValue.ToString());
            LblOfGarantia.Text = "";
            cboOF.Items.Clear();
            listOF.Items.Clear();
            listObra.Items.Clear();
            cboObra.Items.Clear();
            txtCliente.Text = "";


            if (servicio == 6 || servicio == 12 || servicio == 3 || servicio == 4 || servicio == 5)
            {
                chksFacturable.Enabled = false;
                chksFacturable.Checked = false;
            }
            else
            {
                chksFacturable.Enabled = true;
                chksFacturable.Checked = true;
            }
        }       
    }
}