using CapaControl;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIO
{
    public partial class solicitudLogistica : System.Web.UI.Page
    {
        public ControlLogistica CL = new ControlLogistica();
        public ELogcappeso log = new ELogcappeso();
        bool solicitapallet = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Rol"] != null)
                {
                    cargarComboArea();
                    lblAreaId.Value = Session["Area"].ToString();
                    consultarNombreArea(int.Parse(lblAreaId.Value));
                    cargarComboMotivo();
                    cargarComboTipoPallet();
                    solicitapallet = (bool)Session["solicitaPallet"];
                    if (solicitapallet == true)
                    {
                        btnSolicitar.Visible = true;
                        btnNuevo.Visible = true;
                    }
                    else
                    {
                        btnSolicitar.Visible = false;
                        btnNuevo.Visible = false;
                    }
                }
                else
                {
                    string mensaje = "Sesion Expiro. Por favor inicie sesión nuevamente. Gracias!";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    Response.Redirect("Inicio.aspx");
                }

            }
        }
        
        protected void btnSolicitar_Click(object sender, EventArgs e)
        {
            bool valido = validarCamposObligatorios();
            if (valido)
            {
                int rs = CL.insertarSolicitudLogistica(int.Parse(cboArea2.SelectedValue), int.Parse(lblOf.Value), int.Parse(cboTipoPallet.SelectedValue), int.Parse(cboMotivo.SelectedValue), txtComentario.Text, Session["Usuario"].ToString());
                if (rs == -1)
                {
                    mensajeVentana("Ha ocurrido un error, intente nuevamente. Gracias!");
                }
                else
                {
                    CL.insertarSolicitudPallets(rs, listProces);
                    CL.actualizarEstadoPallet(int.Parse(cboTipoPallet.SelectedValue), listProces, int.Parse(lblAreaId.Value));
                    string Nombre = (string)Session["Nombre_Usuario"];
                    string CorreoUsuario = (string)Session["rcEmail"];
                    string correoSistema = (string)Session["CorreoSistema"];
                    int planta = CL.consultarPlantaUsuario((string)Session["Usuario"]);
                    int area = int.Parse(lblAreaId.Value);
                    CL.CorreosLogistica(40, 0, int.Parse(cboTipoPallet.SelectedValue), area, Nombre, planta, CorreoUsuario, correoSistema, cboMotivo.SelectedItem.ToString(), txtComentario.Text, rs, txtOf.Text, "", "", "", area);
                    cargarReporte(txtOf.Text.Trim(), cboTipoPallet.SelectedValue);
                    mensajeVentana("Solicitud creada con éxito!");
                }
            }
            else
            {
                mensajeVentana("Valide la información nuevamente. Gracias!");
            }

            limpiar();          
        }

        private void limpiar()
        {
            cargarComboMotivo();
            cargarComboTipoPallet();
            txtOf.Text = "";
            cboPallet.Items.Clear();
            listProces.Items.Clear();
            txtComentario.Text = "";
            cboTipoPallet.Focus();
        }

        protected void txtOf_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtOf.Text))
            {
                cargarPallet();
                cargarReporte(txtOf.Text, cboTipoPallet.SelectedValue);
            }
            else
            {
                cboPallet.Items.Clear();
                mensajeVentana("Por favor escriba nuevamente la Orden. Gracias!");
            }
        }

        //envia los mensajes en los alert
        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }

        private void cargarComboMotivo()
        {
            DataTable dt = new DataTable();
            dt = CL.consultarMotivosSolicitud();
            if (dt.Rows.Count > 0)
            {
                cargarCombo(dt, cboMotivo, 0, 1);
            }
        }

        //carga el combo
        private void cargarCombo(DataTable tabla, DropDownList combo, int value, int texto)
        {
            combo.Items.Clear();
            combo.Items.Add(new ListItem("Seleccionar", "0"));
            foreach (DataRow row in tabla.Rows)
            {   //posicion de las colmunas  0 = value / 1 = texto  --- se escoge el numero dependiendo de la columna que tenga en el query //siempre va el valor como id de primero, y despues el texto lo que se va mostrar en el combo / ,0,1
                combo.Items.Add(new ListItem(row[texto].ToString(), row[value].ToString()));
            }
        }

        private void consultarNombreArea(int idArea)
        {
            string nombre = CL.consultarNombreArea(idArea);
            if (!String.IsNullOrEmpty(nombre))
            {
                lblArea.Text = nombre;
            }
        }

        private void cargarComboArea()
        {
            DataTable dt = new DataTable();
            dt = CL.consultarAreaUsuarios();
            if (dt.Rows.Count > 0)
            {
                cargarCombo(dt, cboArea2, 0, 1);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("solicitudLogistica.aspx");
        }

        private void cargarComboTipoPallet()
        {
            DataTable dt = new DataTable();
            dt = CL.consultarTipoPallet();
            if (dt.Rows.Count > 0)
            {
                cargarCombo(dt, cboTipoPallet, 0, 1);
            }
        }

        private void cargarPallet()
        {
            DataTable dt = new DataTable();
            if (!String.IsNullOrEmpty(txtOf.Text))
            {
                //Accesorios
                
                if (cboTipoPallet.SelectedValue == "2")
                {
                    dt = CL.consultarPalletAccOrden(txtOf.Text, int.Parse(lblAreaId.Value));
                    if (dt.Rows.Count > 0)
                    {
                        cargarCombo(dt, cboPallet, 0, 3);
                        lblOf.Value = dt.Rows[0][2].ToString();
                    }
                    else
                    {
                        mensajeVentana("No se encuentra el Pallet para esta Orden. Gracias!");
                        cboPallet.Items.Clear();
                    }
                }
                //Aluminio
                else if (cboTipoPallet.SelectedValue == "9")
                {
                    dt = CL.consultarPalletAluminioOrden(txtOf.Text, int.Parse(lblAreaId.Value));
                    if (dt.Rows.Count > 0)
                    {
                        cargarCombo(dt, cboPallet, 0, 3);
                        lblOf.Value = dt.Rows[0][2].ToString();
                    }
                    else
                    {
                        mensajeVentana("No se encuentra el Pallet para esta Orden. Gracias!");
                        cboPallet.Items.Clear();
                    }
                }
                else
                {
                    cboTipoPallet.Focus();
                }
            }
            else
            {
                txtOf.Focus();
            }            
        }
        protected void cboTipoPallet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipoPallet.SelectedIndex != 0)
            {
                if (!String.IsNullOrEmpty(txtOf.Text))
                {
                    cargarReporte(txtOf.Text, cboTipoPallet.SelectedValue);
                    cargarPallet();
                }
                else { txtOf.Focus(); }
            }
        }
        private bool validarCamposObligatorios()
        {
            bool valido = false;
            if (!String.IsNullOrEmpty(lblOf.Value))
            {
                if (cboTipoPallet.SelectedIndex != 0)
                {
                    if (listProces.Items.Count > 0)
                    {
                        if (cboMotivo.SelectedIndex != 0)
                        {
                            if (!String.IsNullOrEmpty(txtComentario.Text))
                            {
                                valido = true;
                            }
                            else
                            {
                                cboMotivo.Focus();
                                mensajeVentana("Por favor escriba el Comentario. Gracias!");
                            }
                        }
                        else
                        {
                            cboMotivo.Focus();
                            mensajeVentana("Por favor seleccione el Motivo. Gracias!");
                        }
                    }
                    else
                    {
                        cboPallet.Focus();
                        mensajeVentana("Por favor adicione a la lista al menos un Pallet. Gracias!");
                    }
                }
                else
                {
                    cboTipoPallet.Focus();
                    mensajeVentana("Por favor seleccione el Tipo de Pallet. Gracias!");
                }
            }
            else
            {
                txtOf.Focus();
                mensajeVentana("Por favor cargue de nuevo la Orden. Gracias!");
            }
            return valido;
        }
        protected void btnAggProces_Click(object sender, EventArgs e)
        {
            if (cboPallet.SelectedIndex != 0)
            {
                agregarLista(listProces, cboPallet.SelectedItem.ToString(), cboPallet.SelectedValue);               
            }
            else
            {
                mensajeVentana("Por favor seleccione el Pallet. Gracias!");
            }
        }
        protected void btnEliProces_Click(object sender, EventArgs e)
        {
            eliminarLista(listProces);
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
        private void cargarReporte(string of, string tipo)
        {
            reporteSolicitud.Visible = true;
            List<ReportParameter> parametro = new List<ReportParameter>();
            parametro.Add(new ReportParameter("of", of, true));
            parametro.Add(new ReportParameter("tipo", tipo, true));
            reporteSolicitud.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            reporteSolicitud.ServerReport.ReportServerCredentials = irsc;
            reporteSolicitud.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            reporteSolicitud.ServerReport.ReportPath = "/Logistica/SolicitudLogistica";
            this.reporteSolicitud.ServerReport.SetParameters(parametro);
            reporteSolicitud.ShowToolBar = true;
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
    }
}