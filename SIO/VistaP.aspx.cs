using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Printing;

namespace SIO
{
    public partial class VistaP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int pesoB = int.Parse((string)Session["PesoB"]);
            if (pesoB > 0)
            {
                lblVolumen.Text = (string)Session["volum"].ToString().Replace(".", ",");
                lblCiudad.Text = (string)Session["ciudad1"];
                lblPais.Text = (string)Session["pais1"];
                //lblEstiba.Text = (string)Session["numpallet1"];
                lblEstiba.Text = (string)Session["nump"];
                lblOrden.Text = (string)Session["orden1"];
                lblCliente.Text = (string)Session["nombrecliente"];
                lblDireccion.Text = (string)Session["direccion"];
                lblAluAcc.Text = (string)Session["alumacceV"];
                lblCodigoBarras.Text = (string)Session["codigoBarr"];
                lblCantiPall.Text = (string)Session["cantidadV"];
                lblPesoBruto.Text = pesoB.ToString().Replace(".", ",");
                lblPesoNeto.Text = (string)Session["PesoN"].ToString().Replace(".", ",");
                lblCbs.Text = (string)Session["codigoBarrC"];
                lblusuario.Text = (string)Session["codigoUsuarioPeso"];
                lblTipoOrden.Text = (string)Session["tipoOrden"];
                btnImprimir.Focus();
                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){document.getElementById('btnImprimir').focus();document.getElementById('btnImprimir').style.display = 'none';window.print();document.getElementById('btnImprimir').style.display = 'inline'; window.close(); window.setTimeout(function () {document.getElementById('ContentPlaceHolder1_txtCodigoEstiba').focus() } , 2500) } , 800); ", true);
                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){document.getElementById('ContentPlaceHolder1_txtCodigoEstiba').focus()} , 2000)", true);
            }
            else if (pesoB <= 0)
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "function mensaje() {alert('El peso no puede ser igual o menor a Cero (0)') } ", true);
                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){document.getElementById('ContentPlaceHolder1_txtCodigoEstiba').focus()} , 2000)", true);
            }
        }
    }
}