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

namespace SIO
{
    public partial class AyudantesLog : System.Web.UI.Page
    {
        public ControlLogistica CL = new ControlLogistica();
        public InfoAyudantes InfoAyu = new InfoAyudantes();
        static List<InfoAyudantes> LInfoAyu = new List<InfoAyudantes>();
        public InfoAyudantes InfoAyu2 = new InfoAyudantes();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblContenedor1.Text = "";
                txtCedula.Focus();
                if (Session["conteP1"] != null && Session["conte1"].ToString() != "")
                {
                    lblContenedor1.Text = Session["conteP1"].ToString();
                    int idCont = int.Parse(Session["conte1"].ToString());
                    GridView2.DataSource = CL.cargaGriedViewAyudantes(idCont);
                    GridView2.DataBind();
                }
                else
                {
                    lblMensaje.Text = ":: Por favor ingrese la Placa/Contenedor!! ::";
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('lblMensaje').style.color='red'; document.getElementById('lblMensaje').style.display = 'inline'; setTimeout(function(){   document.getElementById('lblMensaje').style.display = 'none';}, 3800) } , 100); ", true);
                    txtCedula.Visible = false;
                }
            }
        }
        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            int id = int.Parse(this.GridView2.DataKeys[row.RowIndex].Value.ToString());
            CL.anularAyudantes(id);
            GridView2.DataSource = CL.cargaGriedViewAyudantes(int.Parse(Session["conte1"].ToString()));
            GridView2.DataBind();
            txtCedula.Value = "";
            txtCedula.Focus();
            lblMensaje.Text = "";
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            lblNombre1.Text = "";
            lblCedula1.Text = "";
            if (txtCedula.Value != "")
            {
                int cedula = int.Parse(txtCedula.Value);
                String placaCont = Session["conteP1"].ToString();
                InfoAyu = CL.buscarDatosEmpleados(cedula);
                if (InfoAyu != null)
                {
                    if (InfoAyu.AyuCedula == cedula)
                    {
                        InfoAyu2 = CL.buscarDatosEmpleados2(cedula, int.Parse(Session["conte1"].ToString()));
                        if (InfoAyu2 != null)
                        {
                            if (InfoAyu2.AyuCedula == cedula && InfoAyu2.AyuActivo == "False")
                            {
                                lblMensaje.Text = "";
                                lblNombre1.Text = InfoAyu.AyuNombreEmp;
                                lblCedula1.Text = InfoAyu.AyuCedula.ToString();
                                int idConten = int.Parse(Session["conte1"].ToString());
                                CL.activarAyudantes(cedula);
                                GridView2.DataSource = CL.cargaGriedViewAyudantes(idConten);
                                GridView2.DataBind();
                                txtCedula.Value = "";
                                txtCedula.Focus();
                                lblMensaje.Text = ":: Actualizado!! ::";
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('lblMensaje').style.color='green'; document.getElementById('lblMensaje').style.display = 'inline'; setTimeout(function(){   document.getElementById('lblMensaje').style.display = 'none';}, 3200) } , 100); ", true);
                            }
                            else if (InfoAyu2.AyuCedula == cedula && InfoAyu2.AyuActivo == "True")
                            {
                                lblMensaje.Text = "";
                                txtCedula.Value = "";
                                txtCedula.Focus();
                                lblMensaje.Text = ":: El ayudante ya esta registrado!! ::";
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('lblMensaje').style.color='red'; document.getElementById('lblMensaje').style.display = 'inline'; setTimeout(function(){   document.getElementById('lblMensaje').style.display = 'none';}, 3200) } , 100); ", true);
                            }
                        }
                        else
                        {
                            lblMensaje.Text = "";
                            lblNombre1.Text = InfoAyu.AyuNombreEmp;
                            lblCedula1.Text = InfoAyu.AyuCedula.ToString();
                            int idConten = int.Parse(Session["conte1"].ToString());
                            CL.insertarAyudantes(placaCont, cedula, 1, idConten);
                            GridView2.DataSource = CL.cargaGriedViewAyudantes(idConten);
                            GridView2.DataBind();
                            txtCedula.Value = "";
                            txtCedula.Focus();
                            lblMensaje.Text = ":: Registrado!! ::";
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('lblMensaje').style.color='green'; document.getElementById('lblMensaje').style.display = 'inline'; setTimeout(function(){   document.getElementById('lblMensaje').style.display = 'none';}, 3200) } , 100); ", true);
                        }
                    }
                    else
                    {
                        lblMensaje.Text = "";
                        txtCedula.Value = "";
                        txtCedula.Focus();
                        lblMensaje.Text = ":: El empleado no existe!! ::";
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('lblMensaje').style.color='red'; document.getElementById('lblMensaje').style.display = 'inline'; setTimeout(function(){   document.getElementById('lblMensaje').style.display = 'none';}, 3200) } , 100); ", true);
                    }
                }
                else
                {
                    lblMensaje.Text = "";
                    txtCedula.Value = "";
                    txtCedula.Focus();
                    lblMensaje.Text = ":: El empleado no existe!! ::";
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('lblMensaje').style.color='red'; document.getElementById('lblMensaje').style.display = 'inline'; setTimeout(function(){   document.getElementById('lblMensaje').style.display = 'none';}, 3200) } , 100); ", true);
                }
            }
            else
            {
                lblMensaje.Text = "";
                txtCedula.Value = "";
                txtCedula.Focus();
                lblMensaje.Text = ":: Ingrese la cedula por favor!! ::";
                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('lblMensaje').style.color='red'; document.getElementById('lblMensaje').style.display = 'inline'; setTimeout(function(){   document.getElementById('lblMensaje').style.display = 'none';}, 3200) } , 100); ", true);
            }
        }
        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function () {document.getElementById('ContentPlaceHolder1_txtContenedor').focus()}, 1300);", true);
        }
    }
}