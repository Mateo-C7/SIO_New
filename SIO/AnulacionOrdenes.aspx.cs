using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using CapaControl;
using CapaDatos;
using System.Drawing;
using AjaxControlToolkit;

namespace SIO
{
    public partial class AnulacionOrdenes : System.Web.UI.Page
    {
        ControlOrden ctrlorden = new ControlOrden();
        ControlObra ctrlobra = new ControlObra();

        int actsegIdOrdenBuscada = 0;        
        DataTable dts= null, dts2=null;

        protected void Page_Load(object sender, EventArgs e)
        {
            txt_Ciudad.ReadOnly = true;
            txt_Cliente.ReadOnly = true;
            txt_Obra.ReadOnly = true;            
            txt_Pais.ReadOnly = true;
            txt_Orden.MaxLength = 7;
            txt_Orden.Enabled = false;
            btn_Buscar.Visible = false;

            if (!IsPostBack)
            {
                btn_Anular.Enabled = false;

                string Orden = (Request.QueryString["Orden"].ToString());
                txt_Orden.Text = Orden;

                DataTable dt = null, dt2 = null;
                int estado, fup = 0;
                if (!String.IsNullOrEmpty(Orden))
                {
                    dt = ctrlorden.Validar_existe_Orden(Orden);

                    if (dt.Rows.Count != 0)
                    {
                        btn_Anular.Enabled = true;
                        lbl_FupVersion.Text = dt.Rows[0]["FUPVer"].ToString();
                        estado = Convert.ToInt32(dt.Rows[0]["Anulado"]);
                        Session["actsegIdOrdenBuscada"] = Convert.ToInt32(dt.Rows[0]["actseg_id"]);
                        Session["idofa"] = Convert.ToInt32(dt.Rows[0]["Id_Ofa"]);
                        Session["fup"] = Convert.ToInt32(dt.Rows[0]["fup"]);
                        fup = Convert.ToInt32(dt.Rows[0]["fup"]);

                        if (estado == 1)
                        {
                            lbl_EstadoOrden.Text = "Anulada";
                        }
                        else
                        {
                            lbl_EstadoOrden.Text = "Activa";
                        }
                        //Almacenamos en dt2 los datos generales, para luego asignarlos a los texbox
                        dt2 = ctrlorden.Obtener_Datos_General_Orden(fup);
                        txt_Pais.Text = dt2.Rows[0]["Pais"].ToString();
                        txt_Ciudad.Text = dt2.Rows[0]["Ciudad"].ToString();
                        txt_Cliente.Text = dt2.Rows[0]["Cliente"].ToString();
                        txt_Obra.Text = dt2.Rows[0]["Obra"].ToString();
                        //----------------------------------------------------------------------------     
                        //Tooltip
                        txt_Cliente.ToolTip = txt_Cliente.Text;
                        txt_Obra.ToolTip = txt_Obra.Text;
                        //----------------------------------------------------

                        Obtener_Observaciones();
                        Obtener_Listado_ordenes();

                        //Valida si hay registros en en observaciones y listado ordenes, para mostrar el boton de migrar obs
                        //en el GridListaOrdenes 
                        if (dts.Rows.Count != 0 && dts2.Rows.Count != 0)
                        { GridListaOrdenes.Columns[4].Visible = true; }
                    }
                    else
                    {
                        Limpiar_Formulario();
                        mensajeVentana("No existen registro para la orden " + txt_Orden.Text);
                        btn_Anular.Enabled = false;
                        txt_Orden.Focus();
                    }
                }
                else
                {
                    Limpiar_Formulario();
                    mensajeVentana("Debe digitar una orden");
                    txt_Orden.Focus();
                }
            }
        }
     
        //Permite mostrar mensajes de alerta
        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }

        protected void btn_Buscar_Click(object sender, EventArgs e)
        {
            //DataTable dt=null,dt2=null;           
            //int estado,fup=0;
            //if (!String.IsNullOrEmpty(txt_Orden.Text))
            //{
           
            //dt = ctrlorden.Validar_existe_Orden(txt_Orden.Text);

            //if (dt.Rows.Count != 0)
            //{
            //    btn_Anular.Enabled = true;
            //    lbl_FupVersion.Text = dt.Rows[0]["FUPVer"].ToString();
            //    estado = Convert.ToInt32(dt.Rows[0]["Anulado"]);          
            //    Session["actsegIdOrdenBuscada"] = Convert.ToInt32(dt.Rows[0]["actseg_id"]);
            //    Session["idofa"]  = Convert.ToInt32(dt.Rows[0]["Id_Ofa"]);
            //    Session["fup"] = Convert.ToInt32(dt.Rows[0]["fup"]);
            //    fup= Convert.ToInt32(dt.Rows[0]["fup"]);

            //    if (estado == 1)
            //    {
            //        lbl_EstadoOrden.Text = "Anulada";
            //    }
            //    else
            //    {
            //        lbl_EstadoOrden.Text = "Activa";
            //    }
            //    //Almacenamos en dt2 los datos generales, para luego asignarlos a los texbox
            //    dt2 = ctrlorden.Obtener_Datos_General_Orden(fup);
            //    txt_Pais.Text = dt2.Rows[0]["Pais"].ToString();
            //    txt_Ciudad.Text = dt2.Rows[0]["Ciudad"].ToString();
            //    txt_Cliente.Text = dt2.Rows[0]["Cliente"].ToString();
            //    txt_Obra.Text = dt2.Rows[0]["Obra"].ToString();
            //    //----------------------------------------------------------------------------     
            //    //Tooltip
            //    txt_Cliente.ToolTip = txt_Cliente.Text;
            //    txt_Obra.ToolTip = txt_Obra.Text;
            //    //----------------------------------------------------

            //    Obtener_Observaciones();
            //    Obtener_Listado_ordenes();

            //    //Valida si hay registros en en observaciones y listado ordenes, para mostrar el boton de migrar obs
            //    //en el GridListaOrdenes 
            //    if (dts.Rows.Count != 0 && dts2.Rows.Count != 0)
            //    { GridListaOrdenes.Columns[4].Visible = true; }
            //}
            //else
            //{
            //    Limpiar_Formulario();
            //    mensajeVentana("No existen registro para la orden " + txt_Orden.Text);
            //        btn_Anular.Enabled = false;
            //        txt_Orden.Focus();
            //}
            //}
            //else
            //{
            //    Limpiar_Formulario();
            //    mensajeVentana("Debe digitar una orden");
            //    txt_Orden.Focus();               
            //}

        }

        public void Obtener_Listado_ordenes()
        {            
            int fup = (int)Session["fup"];

            //Almacenamos en dts2 el listado de ordenes,para luego asignarlo a la grilla
            dts2 = ctrlorden.Obtener_Listado_Ordenes(fup, txt_Orden.Text);

            if (dts2.Rows.Count != 0)
            {
                GridListaOrdenes.DataSource = dts2;
                GridListaOrdenes.DataMember = dts2.ToString();
                GridListaOrdenes.DataBind();
                GridListaOrdenes.Columns[4].Visible = false;
            }
            else
            {
                GridListaOrdenes.DataSource = null;
                GridListaOrdenes.DataBind();
            }
            //----------------------------------------------------------------------------
        }

        public void Limpiar_Formulario()
        {
            lbl_EstadoOrden.Text = "";
            lbl_FupVersion.Text = "";
            txt_Pais.Text = "";
            txt_Ciudad.Text = "";
            txt_Cliente.Text = "";
            txt_Obra.Text = "";
            chk_AnularSgto.Checked = false;
            GridObs.DataSource = null;
            GridObs.DataBind();
            GridListaOrdenes.DataSource = null;
            GridListaOrdenes.DataBind();
        }

        //Realiza la migracion de observaciones entre ordenes
        protected void GridListaOrdenes_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
          int actseg_id = Convert.ToInt32(GridListaOrdenes.DataKeys[e.RowIndex].Value);
          int actsegIdOrdenBuscada = (int)Session["actsegIdOrdenBuscada"];

            if (lbl_EstadoOrden.Text == "Anulada")
            {
                ctrlorden.Migrar_Obs_Entre_Ordenes(actseg_id, actsegIdOrdenBuscada);
                GridObs.DataSource = null;
                GridObs.DataBind();
                GridListaOrdenes.Columns[4].Visible = false;
                mensajeVentana("Migracion realizada satisfactoriamente");
            }
            else
            {
                mensajeVentana("Para realizar esta migración, la orden " +txt_Orden.Text + "  debe estar anulada");
            }          
        }

        public void Obtener_Observaciones()
        {
            // actsegIdOrdenBuscada = 21342; //linea de prueba, eliminar cuando termine la validacion
            int actsegIdOrdenBuscada = (int)Session["actsegIdOrdenBuscada"];
            dts = ctrlorden.Obtener_Observaciones(actsegIdOrdenBuscada);

            if (dts.Rows.Count != 0)
            {
                GridObs.DataSource = dts;
                GridObs.DataMember = dts.ToString();
                GridObs.DataBind();
            }
            else
            {
                GridObs.DataSource = null;
                GridObs.DataBind();
            }
        }

        protected void btn_Anular_Click(object sender, EventArgs e)
        {
            int Piezas = 0;
            DataTable dt = null;
            int idofa = (int)Session["idofa"];

            //Variables para almacenar el Log
            string fecha = DateTime.Now.ToShortDateString();
            string tabla = "Orden_Seg";
            string evento = "Anulación";
            string usuarioMod = (string)Session["Usuario"];
            //----------------------------------------------

            if (lbl_EstadoOrden.Text != "Anulada")
            {
                dt = ctrlorden.Validar_Comsumo_De_item(idofa);

            Piezas=Convert.ToInt32(dt.Rows[0]["Piezas"].ToString());

            if (Piezas>0)
            {
                mensajeVentana("Esta orden no se puede anular, ya cuenta con un cargue de ingenieria.");
            }
            else
            {
                    ctrlorden.Anular_Orden(idofa);
                    ctrlorden.Anular_Orden_Seg(idofa);

                if (chk_AnularSgto.Checked == true)
                {
                        //Si el chek anular seguimiento esta marcado 
                        //Cuando se desea anular el seguimiento sin mantener el FUP
                        ctrlorden.Anular_Seguimiento_Orden(1, idofa);
                        lbl_EstadoOrden.Text = "Anulada";
                        mensajeVentana("Orden anulada satisfactoriamente.");
                 }
                else
                {
                        //Si el chek anular seguimiento no esta marcado
                        //Cuando se desea anular el seguimiento manteniendo el FUP
                        ctrlorden.Anular_Seguimiento_Orden(2, idofa);
                        lbl_EstadoOrden.Text = "Anulada";
                        mensajeVentana("Orden anulada satisfactoriamente.");
                 }

                 ctrlobra.Met_Insertar_Log_AnulaObra(tabla, idofa, usuarioMod, fecha, evento);
              }

            }
            else
            {
                mensajeVentana("Esta orden ya se encuentra anulada.");
            }
        }

        protected void GridObs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridObs.EditIndex = -1;
            Obtener_Observaciones();
            GridObs.PageIndex = e.NewPageIndex;
            this.GridObs.DataBind();
        }

        protected void GridListaOrdenes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridListaOrdenes.EditIndex = -1;
            Obtener_Listado_ordenes();
            GridListaOrdenes.PageIndex = e.NewPageIndex;
            this.GridListaOrdenes.DataBind();
        }
    }
}