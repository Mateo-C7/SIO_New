using CapaControl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SIO
{
    public partial class Planeador : System.Web.UI.Page
    {
        ControlDatos CD = new ControlDatos();

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {

                getPlanta(sender, e);
                getTipoSolicitud(sender, e);             
            }
        }

        private void getTipoSolicitud(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = CD.getTipoSolicitud();
            cargarCombo(dt, cboTipoSolicitud, 1, 0);
            //Se selecciona por default OF que corresponde al ID 7
            cboTipoSolicitud.SelectedValue = "7";
            cboTipoSolicitud_TextChanged(sender, e);
        }

        private void getPlanta(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = CD.getPlanta();
            cargarCombo(dt, cboPlanta, 1, 0);
            cboPlanta.SelectedValue = "1";
            //cboPlanta_TextChanged(sender, e);
        }

        protected void cboPlanta_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = CD.getOrdenesTipoSolicitud(cboTipoSolicitud.SelectedItem.ToString(), Convert.ToInt32(cboPlanta.SelectedValue));
            cargarCombo(dt, cboNoSolicitud, 1, 0);
            cboRaya.Items.Clear();
            cboFamilia.Items.Clear();
            listFm.Items.Clear();
        }


        protected void cboTipoSolicitud_TextChanged(object sender, EventArgs e)
        {

            if (cboTipoSolicitud.SelectedItem.Text != "Seleccione")
            {                   
                DataTable dt = new DataTable();
                dt = CD.getOrdenesTipoSolicitud(cboTipoSolicitud.SelectedItem.ToString(), Convert.ToInt32(cboPlanta.SelectedValue));
                cargarCombo(dt, cboNoSolicitud, 1, 0);
                cboRaya.Items.Clear();
                cboFamilia.Items.Clear();
                listFm.Items.Clear();
            }
        }

        private void cargarCombo(DataTable dt, DropDownList cbo, int text, int value)
        {
            cbo.Items.Clear();
            cbo.Items.Add(new ListItem("Seleccione", "0"));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cbo.Items.Add(new ListItem(dt.Rows[i].ItemArray[text].ToString(), dt.Rows[i].ItemArray[value].ToString()));
                }
            }
            cbo.SelectedIndex = 0;
        }

        protected void cboNoSolicitud_TextChanged(object sender, EventArgs e)
        {
            if (cboNoSolicitud.SelectedItem.Text != "Seleccione")
            {
                DataTable dt = new DataTable();
                dt = CD.getRayaOrden(cboTipoSolicitud.SelectedItem.ToString(), Convert.ToInt32(cboNoSolicitud.SelectedValue));
                cargarCombo(dt, cboRaya, 3, 5);
                if (dt.Rows.Count > 0)
                {
                    cboRaya.SelectedIndex = 1;
                    cboRaya_TextChanged(sender, e);
                }
                else
                {
                    cboFamilia.Items.Clear();
                }
                listFm.Items.Clear();
            }
        }

        protected void cboRaya_TextChanged(object sender, EventArgs e)
        {
            if (cboRaya.SelectedItem.Text != "Seleccione")
            {
                DataTable dt = new DataTable();
                dt = CD.getFamiliaOrdenRaya(Convert.ToInt32(cboRaya.SelectedValue), Convert.ToInt32(cboNoSolicitud.SelectedValue));
                cargarCombo(dt, cboFamilia, 1, 0);
                listFm.Items.Clear();
            }
        }

        protected void btnAggFm_Click(object sender, EventArgs e)
        {
            if (cboFamilia.SelectedItem.Text != "Seleccionar")
            {
                agregarLista(listFm, cboFamilia.SelectedItem.ToString(), cboFamilia.SelectedValue.ToString());
            }
        }

        protected void btnElmFm_Click(object sender, EventArgs e)
        {
            eliminarLista(listFm);
        }

        //metodo general para agregar las listas limitada (No permite repeticion)
        private void agregarLista(ListBox list, String item, String value)
        {
            if (value != "0")//verifico si hay algo en el id del combo
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

        private string listaFamilia(ListBox list)
        {
            string filtro = "";            
            if (list.Items.Count > 0)
            {
                filtro = " AND (Saldos.Gno = ";
                for (int i = 0; i < list.Items.Count; i++)
                {
                    string condicion = " OR Saldos.GNo =";

                    if (i == 0)
                        filtro += list.Items[i].Value;
                    else
                    {
                        condicion += list.Items[i].Value;
                        filtro += condicion;
                    }
                }
                filtro += ")";
            }
            return filtro;
        }

        // carga todos los items de la grilla
        protected void btnCargar_Click(object sender, EventArgs e)
        {
            

            //string filtro = listaFamilia(listFm);
            string filtro = "AND(Saldos.Gno = " + cboFamilia.SelectedValue.ToString() + ")";
            tblMP.Visible = true;
            tab2.Visible = true;
            hr1.Visible = true;
            DataTable dt = new DataTable();
            dt = null;
            //TENER EN CUENTA LA PLANTA        
            dt = CD.getMateriaPrimaCombo(Convert.ToInt32(cboPlanta.SelectedValue));
            cargarCombo(dt, cboMP, 0, 0);
            int idExploMpPrincipal = CD.getIdExploPrincipal(Convert.ToInt32(cboRaya.SelectedValue), Convert.ToInt32(cboFamilia.SelectedValue));
            lblIdExploPrincipal.Value = idExploMpPrincipal.ToString();
            DataTable estado = getEstadoExplosionador(Convert.ToInt32(cboRaya.SelectedValue), Convert.ToInt32(cboFamilia.SelectedValue));
            int kambam = 1;
            if (chkKambam.Checked)
                kambam = 1;
            else
                kambam = 0;
            setVisibleButtons(estado);
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "cargarPlaneador('" + filtro + "', '" + lblIdEstado.Value + "', '" + kambam + "')", true);
        }                
        
        protected void cboMP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMP.SelectedItem.Text != "Seleccione")
            {
                DataTable dt = CD.getInventarioMateriaPrima(cboMP.SelectedItem.Text,Convert.ToInt32(cboPlanta.SelectedValue));
                string contenidoPopUp = "";

                if (dt.Rows.Count > 0)
                {                   
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        try
                        {
                            string codigo = dt.Rows[i]["Id_Mp"].ToString();
                            string nombre = dt.Rows[i]["Mp_Nombre"].ToString();
                            string cantExistenteInventario = dt.Rows[i]["CANT_EXISTENCIA"].ToString();
                            string unidadExistenciaInventario = dt.Rows[i]["ID_UNIDAD_INVENTARIO"].ToString();
                            string cantExistenciaOrden = dt.Rows[i]["CANT_ORDEN"].ToString();
                            string unidadExistenciaOrden = dt.Rows[i]["UNIDAD_ORDEN"].ToString();
                            string bodega = dt.Rows[i]["BODEGA_DESCRIPCION"].ToString();
                            string idBodega = dt.Rows[i]["ID_BODEGA"].ToString();

                            double existenteInventario = Convert.ToDouble(cantExistenteInventario);
                            double existenteOrden = Math.Ceiling(Convert.ToDouble(cantExistenciaOrden));

                            contenidoPopUp += "<tr>";
                            contenidoPopUp += "<td align=\"center\">" + codigo + "</td>";
                            contenidoPopUp += "<td>" + nombre + "</td>";
                            contenidoPopUp += "<td align=\"right\">" + existenteInventario + "</td>";
                            contenidoPopUp += "<td align=\"center\">" + unidadExistenciaInventario + "</td>";
                            contenidoPopUp += "<td align=\"right\">" + existenteOrden + "</td>";
                            contenidoPopUp += "<td align=\"center\">" + unidadExistenciaOrden + "</td>";
                            contenidoPopUp += "<td align=\"center\">" + idBodega + "</td>";
                            contenidoPopUp += "<td align=\"left\">" + bodega + "</td>";
                            contenidoPopUp += "</tr>";
                        }
                        catch 
                        {

                        }                      
                        
                    }
                }
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "popupMateriaPrima('" + contenidoPopUp + "')", true);               
            }           
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("Planeador.aspx");
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            DataTable estado = getEstadoExplosionador(Convert.ToInt32(cboRaya.SelectedValue), Convert.ToInt32(cboFamilia.SelectedValue));
            setVisibleButtons(estado);
        }    

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            int idOfam = Convert.ToInt32(Session["idOfa"]);

            DataTable dt;      

            dt = CD.ValidarSaldosExplosionados(idOfam);

            //if (dt.Rows.Count > 0)
            //{
            //    mensajeVentana("No es posible CONFIRMAR la orden, aun faltan item´s por explosionar");
            //}
            //else
            //{
                //Estado Confirmar = 2
                int idExploMpPrincipal = 0;
                idExploMpPrincipal = Convert.ToInt32(lblIdExploPrincipal.Value);
            
                if (idExploMpPrincipal != 0)
                {                   
                    int estado2 = 2;                                      
                    string usuario = Session["Usuario"].ToString();
                    CD.updateEstadoExplo(idExploMpPrincipal, estado2, usuario);
                    CD.insertLogExploMp(idExploMpPrincipal, estado2, usuario);
                    DataTable estado = getEstadoExplosionador(Convert.ToInt32(cboRaya.SelectedValue), Convert.ToInt32(cboFamilia.SelectedValue));
                    btnCargar_Click(sender, e);
                
                    if (Convert.ToInt32(lblIdEstado.Value) == 2)
                    {
                        mensajeVentana("Se ha Confirmado la orden: " + cboNoSolicitud.SelectedItem + "-" + cboRaya.SelectedItem);
                        setVisibleButtons(estado);
                    }
                    else
                    {
                        mensajeVentana("No es posible Confirmar la orden: " + cboNoSolicitud.SelectedItem + "-" + cboRaya.SelectedItem);
                    }
                }
                else
                {
                    mensajeVentana("No es posible Confirmar la orden: " + cboNoSolicitud.SelectedItem + "-" + cboRaya.SelectedItem);
                }
            //}          
        }

        protected void btnAnular_Click(object sender, EventArgs e)
        {
            //Estado Anular = 3
            int idExploMpPrincipal = 0;
            idExploMpPrincipal = Convert.ToInt32(lblIdExploPrincipal.Value);

            if (idExploMpPrincipal != 0)
            {
                int estado = 3;
                string usuario = Session["Usuario"].ToString();
                CD.updateEstadoExplo(idExploMpPrincipal, estado, usuario);
                btnCargar_Click(sender, e);
                if (Convert.ToInt32(lblIdEstado.Value) == 3)
                {
                    mensajeVentana("Se ha ANULADO la orden: " + cboNoSolicitud.SelectedItem + "-" + cboRaya.SelectedItem);
                }
                else
                {
                    mensajeVentana("No es posible ANULAR la orden: " + cboNoSolicitud.SelectedItem + "-" + cboRaya.SelectedItem);
                }
            }
            else
            {
                mensajeVentana("No es posible ANULAR la orden: " + cboNoSolicitud.SelectedItem + "-" + cboRaya.SelectedItem);
            }
        }

        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }

        private void setVisibleButtons(DataTable dtEstado)
        {
            int estado = 0;
            string descEstado = "Nuevo";            
            if (dtEstado.Rows.Count > 0)
            {
                estado = Convert.ToInt32(dtEstado.Rows[0]["id_estado_explosionador"]);
                descEstado = dtEstado.Rows[0]["descripcion"].ToString();
            }
            lblEstado.Text = descEstado;             
            lblIdEstado.Value = estado.ToString();
            //SIN ESTADO
            if (estado == 0)
            {
                btnNuevo.Visible = true;
                btnGuardarExplosion.Visible = true;
                btnActualizar.Visible = false;
                btnConfirmar.Visible = false;
                btnAnular.Visible = false;
                btnSolicitudMateriaPrima.Visible = false;

                cboMP.Enabled = true;
                cboColumns.Disabled = false;
                btnEliminarRegitro.Enabled = true;
            }

            //GUARDADO
            else if (estado == 1)
            {
                btnNuevo.Visible = true;
                btnGuardarExplosion.Visible = true;
                btnActualizar.Visible = false;
                btnConfirmar.Visible = true;
                btnAnular.Visible = true;
                btnSolicitudMateriaPrima.Visible = false;

                cboMP.Enabled = true;
                cboColumns.Disabled = false;
                btnEliminarRegitro.Enabled = true;
            }

            //CONFIRMADO
            else if (estado == 2)
            {
                btnNuevo.Visible = true;
                btnGuardarExplosion.Visible = false;
                btnActualizar.Visible = false;
                btnConfirmar.Visible = false;
                btnAnular.Visible = true;
                btnSolicitudMateriaPrima.Visible = true;

                cboMP.Enabled = false;
                cboColumns.Disabled = true;
                btnEliminarRegitro.Enabled = false;
            }

            //ANULADO
            else if (estado == 3)
            {
                btnNuevo.Visible = true;
                btnGuardarExplosion.Visible = false;
                btnActualizar.Visible = false;
                btnConfirmar.Visible = false;
                btnAnular.Visible = false;
                btnSolicitudMateriaPrima.Visible = false;

                cboMP.Enabled = false;
                cboColumns.Disabled = true;
                btnEliminarRegitro.Enabled = false;
            }

            //SOLICITADO
            else if (estado == 4)
            {
                btnNuevo.Visible = true;
                btnGuardarExplosion.Visible = false;
                btnActualizar.Visible = false;
                btnConfirmar.Visible = false;
                btnAnular.Visible = false;
                btnSolicitudMateriaPrima.Visible = false;

                cboMP.Enabled = false;
                cboColumns.Disabled = true;
                btnEliminarRegitro.Enabled = false;
            }
        }

        private DataTable getEstadoExplosionador(int idOfa, int idFamilia)
        {
            Session["idOfa"] = idOfa;
            Session["idFamilia"] = idFamilia;

            DataTable dt = new DataTable();
            dt = CD.getEstadoExplosionador(idOfa, idFamilia);
            return dt;
        }

        protected void btnGuardarExplosion_Click(object sender, EventArgs e)
        {
            string filtro = "AND(Saldos.Gno = " + cboFamilia.SelectedValue.ToString() + ")";
            int kambam = 1;
            if (chkKambam.Checked)
                kambam = 1;
            else
                kambam = 0;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "guardarExplosion('" + filtro + "', '" + lblIdEstado.Value + "', '" + kambam + "')", true);
        }


        protected void btnSolicitudMateriaPrima_Click(object sender, EventArgs e)
        {
            string filtro = "AND(Saldos.Gno = " + cboFamilia.SelectedValue.ToString() + ")";
            int kambam = 1;
            if (chkKambam.Checked)
                kambam = 1;
            else
                kambam = 0;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "agregarSolicitudMateriaPrima('" + filtro + "', '" + lblIdEstado.Value + "', '" + kambam + "')", true);
        }

        protected void btnActualizar_Click1(object sender, EventArgs e)
        {

        }

        protected void btnSugiere_Click(object sender, EventArgs e)
        {
            // consulta si tiene explosugerido desde explosionador web
            bool explosugerido = CD.consultaExplosugiereRaya(Convert.ToInt32(cboRaya.SelectedValue));

            if(explosugerido)
            {
                mensajeVentana("No es posible Ejecutar, El ExploSugerido ya fue ejecutado previamente, para la Raya seleccionada");
            }
            else
            {
                CD.actualizaExploSugiere(Convert.ToInt32(cboRaya.SelectedValue));
                mensajeVentana("Se Ejecuta ExploSugerido Exitosamente para la Raya seleccionada");
            }
        }
    }
}