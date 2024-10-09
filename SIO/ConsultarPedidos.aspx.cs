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

namespace SIO
{
    public partial class ConsultarPedidos : System.Web.UI.Page
    {
        private ControlCasino CC = new ControlCasino();
        public ControlPoliticas CP = new ControlPoliticas();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cboOpcion.SelectedIndex = 0;
                trAccion.Visible = false;
                tablaOcultas(false);
                Session["usuarioCasino"] = CC.permisosCasino(Session["Usuario"].ToString());
                if (Session["usuarioCasino"].ToString() == "True")
                {
                    txtNomUsu.Visible = false;
                    lblNomUsu.Visible = false;
                    btnBuscar.Visible = false;
                    Session["superUsuario"] = CC.superUsuarioCasino(Session["Usuario"].ToString());
                    if (Session["superUsuario"].ToString() == "True")
                    {
                        cboServicio.SelectedIndex = 0;
                        trTipoServicio.Visible = false;
                        lblNomUsu.Visible = true;
                        txtNomUsu.Visible = true;
                        btnBuscar.Visible = true;
                    }
                }
                else
                {
                    UpdatePanel1.Visible = false;
                }
                politicas(27, Session["usuario"].ToString());
            }
        }
        //Combo del tipo de servicio
        protected void cboServicio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboServicio.SelectedValue.ToString() == "Seleccione")
            {
                tablaOcultas(false);
                cboOpcion.SelectedIndex = 0;
                trAccion.Visible = false;
            }
            else if (cboServicio.SelectedValue.ToString() == "especial")
            {
                btnActualizar.Visible = false;
                TablaNormal.Visible = false;
                cboOpcion.SelectedIndex = 0;
                trAccion.Visible = true;
            }
            else if (cboServicio.SelectedValue.ToString() == "normal")
            {
                tablaNormal();
                cboOpcion.SelectedIndex = 0;
                trAccion.Visible = false;
            }
        }
        //llena la tabla especial y manda la session en TablaEspecial para que el boton actualizar lo tome como especial
        private void tablaEspecial()
        {
            Session["Tabla"] = "";
            Session["Tabla"] = "TablaEspecial";
            TablaNormal.Visible = false;
            TablaEspecial.Visible = true;
            String fechaActual = CC.fechaLocalString().ToString();
            String horaActual = CC.horaLocalString().ToString();
            DataTable especial = new DataTable();
            if (Session["superUsuario"].ToString() == "True")
            {   
                String condicion = "AND (empleado.emp_nombre LIKE '%" + Session["usuarioFiltrado"].ToString() + "%') OR (empleado.emp_apellidos LIKE '%" + Session["usuarioFiltrado"].ToString() + "%') AND (Casino_Tipo_Servicio.id_serv_subserv > 0)  AND (Casino_Pedidos.casped_estado = 'Abierto')";
                especial = CC.llenarTablaEspecial(condicion);
            }
            else
            {
                if (cboOpcion.SelectedValue.ToString() == "Anulado")
                {
                    String condicion = "AND ('" + fechaActual + "' <= Casino_Pedidos.casped_fecha_aten) AND ('" + horaActual + "' <= Casino_Tipo_Servicio.castiposerv_hanula) AND (usuario.usu_login = '" + Session["Usuario"].ToString() + "')";
                    especial = CC.llenarTablaEspecial(condicion);
                }
                else if (cboOpcion.SelectedValue.ToString() == "Cerrado")
                {
                    String condicion = "AND (Casino_Pedidos.casped_fecha_aten < '" + fechaActual + "') AND (usuario.usu_login = '" + Session["Usuario"].ToString() + "')";
                    especial = CC.llenarTablaEspecial(condicion);
                }
            }
            grdEspecial.DataSource = especial;
            grdEspecial.DataBind();
            if (grdEspecial.Rows.Count == 0)
            {
                btnActualizar.Visible = false;
            }
            else
            {
                if (Session["btnActulizarPedi"].ToString() == "false") { btnActualizar.Visible = false; }
                else
                {
                    btnActualizar.Visible = true;
                }
            }
        }
        //llena la tabla normal y manda la session en TablaNormal para que el boton actualizar lo tome como normal
        private void tablaNormal()
        {
            Session["Tabla"] = "";
            Session["Tabla"] = "TablaNormal";
            TablaEspecial.Visible = false;
            cboOpcion.SelectedIndex = 0;
            trAccion.Visible = false;
            TablaNormal.Visible = true;
            DataTable normal = new DataTable();
            String fechaActual = CC.fechaLocalString().ToString();
            String horaActual = CC.horaLocalString().ToString();
            if (Session["superUsuario"].ToString() == "True")
            {
                String condicion = "AND (empleado.emp_nombre LIKE '%" + Session["usuarioFiltrado"].ToString() + "%') OR (empleado.emp_apellidos LIKE '%" + Session["usuarioFiltrado"].ToString() + "%') AND (Casino_Tipo_Servicio.id_serv_subserv = 0) AND (Casino_Pedidos.casped_estado = 'Abierto')";
                normal = CC.llenarTablaNormal(condicion);
            }
            else
            {
                String condicion = "AND ('" + fechaActual + "' <= Casino_Pedidos.casped_fecha_aten) AND ('" + horaActual + "' <= Casino_Tipo_Servicio.castiposerv_hanula) AND (usuario.usu_login = '" + Session["Usuario"].ToString() + "')";
                normal = CC.llenarTablaNormal(condicion);
            }
            grdNormal.DataSource = normal;
            grdNormal.DataBind();
            if (grdNormal.Rows.Count == 0)
            {
                btnActualizar.Visible = false;
            }
            else
            {
                if (Session["btnActulizarPedi"].ToString() == "false") { btnActualizar.Visible = false; }
                else
                {
                    btnActualizar.Visible = true;
                }
            }
        }
        //Ocultas las tablas dependiedo que orden le mande
        private void tablaOcultas(Boolean boo)
        {
            btnActualizar.Visible = boo;
            TablaEspecial.Visible = boo;
            TablaNormal.Visible = boo;
        }
        //Maneja el mensaje por medio de script
        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }
        //Boton de actualizar
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            String cboEstadoNuevo = "";
            String estadoActual = "";
            String numPedido = "";
            if (Session["Tabla"].ToString() == "TablaEspecial")//verifica que tabla es si es TablaEspecial o TablaNormal con la session que se manda desde los metodos de la tablas
            {
                int mensajeNum = 0;
                String mensaje = "";
                foreach (GridViewRow row in grdEspecial.Rows)
                {
                    cboEstadoNuevo = ((DropDownList)row.FindControl("cboEstadoTabla")).SelectedItem.Value;
                    estadoActual = row.Cells[12].Text;
                    numPedido = row.Cells[0].Text;
                    if (cboEstadoNuevo != "Seleccionar")
                    {
                        if (estadoActual != cboEstadoNuevo)
                        {
                            mensaje = CC.editarEstadoPedido(int.Parse(numPedido), cboEstadoNuevo);
                            if (mensaje == "OK")
                            {
                                CC.correoCasino("especial", int.Parse(numPedido), cboEstadoNuevo, "CasinoPedido");
                                mensajeNum = mensajeNum + 1;
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
                tablaEspecial();
            }
            else if (Session["Tabla"].ToString() == "TablaNormal")//verifica que tabla es si es TablaEspecial o TablaNormal con la session que se manda desde los metodos de la tablas
            {
                int mensajeNum = 0;
                String mensaje = "";
                foreach (GridViewRow row in grdNormal.Rows)
                {
                    cboEstadoNuevo = ((DropDownList)row.FindControl("cboEstadoTabla")).SelectedItem.Value;
                    estadoActual = row.Cells[8].Text;
                    numPedido = row.Cells[0].Text;
                    if (cboEstadoNuevo != "Seleccionar")
                    {
                        if (estadoActual != cboEstadoNuevo)
                        {
                            mensaje = CC.editarEstadoPedido(int.Parse(numPedido), cboEstadoNuevo);
                            if (mensaje == "OK")
                            {
                                CC.correoCasino("normal", int.Parse(numPedido), cboEstadoNuevo, "CasinoPedido");
                                mensajeNum = mensajeNum + 1;
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
                tablaNormal();
            }
        }
        //Este metodo me llena el combo que esta en la tabla de especiales
        public DataTable llenarComboSel()
        {
            DataTable tabla = new DataTable();
            tabla.Columns.Add("Nombre", typeof(String));
            tabla.Columns.Add("Valor", typeof(String));
            DataRow fila = tabla.NewRow();
            tabla.Rows.Add(new Object[] { "Seleccione", "Seleccionar" });
            tabla.Rows.Add(new Object[] { cboOpcion.SelectedItem.ToString(), cboOpcion.SelectedValue.ToString() });
            return tabla;
        }
        //Este comobo permite escoger una de las dos opciones "Cerrado" o "Anulado", para poder filtra la tabla
        protected void cboOpcion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboOpcion.SelectedItem.ToString() != "Selecionar")
            {
                TablaNormal.Visible = false;
                tablaEspecial();
            }
            else
            {
                tablaOcultas(false);
            }
        }
        //Busca al empleado que escribe en el textbox, lo guarda en una session y activa el combo
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Session["usuarioFiltrado"] = txtNomUsu.Text;
            tablaOcultas(false);
            cboOpcion.SelectedIndex = 0;
            trAccion.Visible = false;
            cboServicio.SelectedIndex = 0;
            trTipoServicio.Visible = true;
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
            { Session["btnActulizarPedi"] = "true"; }
            else { Session["btnActulizarPedi"] = "false"; }
        }
    }
}