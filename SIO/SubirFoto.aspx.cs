using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using CapaControl;

namespace SIO
{
    public partial class SubirFoto : System.Web.UI.Page
    {
        public ControlFUP controlfup = new ControlFUP();
        public ControlListaChequeo cListaCheq = new ControlListaChequeo();
        public SqlDataReader reader = null;
        private DataSet dsLista = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scripManager = ScriptManager.GetCurrent(this.Page);
            //scripManager.RegisterPostBackControl(btnSubirPlano);

            Session["SubirFoto"] = "1";
            if (!IsPostBack)
            {
                CargarGrillaFotografia();
            }
        }

        private void CargarGrillaFotografia()
        {
            string FUP = (string)Session["FUP"];
            string VER = (string)Session["VER"];
            dsLista.Reset();
            dsLista = cListaCheq.ConsultarFotografiaLista(Convert.ToInt32(FUP), VER);
            if (dsLista != null)
            {
                grvArchivo.DataSource = dsLista.Tables[0];
                grvArchivo.DataBind();
                grvArchivo.Visible = true;
            }
            else
            {
                grvArchivo.Dispose();
                grvArchivo.Visible = false;
            }
            dsLista.Reset();
        }

        protected void anexo_OnClick(object sender, EventArgs e)
        {
            LinkButton link = (LinkButton)sender;
            string URL = link.Text;
            URL = Page.ResolveClientUrl(URL);
            link.OnClientClick = "window.open('" + URL + "'); return false;";
            //Response.Redirect(link.Text);
        }

        protected void grvArchivo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Borrar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grvArchivo.Rows[index];

                string id = Convert.ToString(grvArchivo.DataKeys[index].Value);
                string ruta = ((LinkButton)row.FindControl("simpa_anexoEditLink")).Text.ToString();
                ruta = ruta.Replace("/", @"\");
                ruta = ruta.Replace("~", @"I:");

                //BORRAR PLANO EN SISTEMA OPERATIVO
                if (File.Exists(ruta))
                {
                    File.Delete(ruta);
                }

                //BORRAR PLANO EN BASE DE DATOS
                reader = controlfup.BorrarPlano(Convert.ToInt32(id), (string)Session["Nombre_Usuario"]);

                CargarGrillaFotografia();
            }
        }

        protected void btnSubirPlano_Click(object sender, EventArgs e)
        {
            string OF = (string)Session["OF"];
            string Nombre = (string)Session["Nombre_Usuario"];
            string directorio = @"C:\ListaChequeo\" + OF + @"\";
            string dirweb = @"~/ListaChequeo/" + OF + @"/";
            int TAnexo = 99, TProy = 0;
            string FUP = (string)Session["FUP"];
            string VER = (string)Session["VER"];
            //string directorio = @"C:\Anexos_" + txtFUP.Text + cboVersion.SelectedItem.Text.Trim() + @"\";

            if (!(Directory.Exists(directorio)))
            {
                Directory.CreateDirectory(directorio);
            }

            FDocument.Enabled = true;
            string FileName = System.IO.Path.GetFileName(FDocument.FileName);

            if (FileName != "")
            {
                if (FDocument.HasFile && FDocument.PostedFile.ContentLength > 10485760)
                {
                    Archivo.Text = "Tamaño Maximo del Archivo 10 MB.";
                }
                else
                {
                    FDocument.SaveAs(directorio + FileName);

                    int IngFOTO = controlfup.IngDOCPLAN(Convert.ToInt32(FUP), VER, Nombre,
                    FileName, dirweb, TAnexo, Convert.ToInt32(TProy), OF);

                    string mensaje = "";
                    mensaje = "Archivo ingresado con exito.";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);

                    Session["SubirFoto"] = "1";
                    CargarGrillaFotografia();
                }
            }
            else
            {
                string mensaje = "";
                mensaje = "Archivo no seleccionado.";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Session["SubirFoto"] = "1";
            string OF = (string)Session["OF"];
            Session["OF"] = OF;
        }
    }
}