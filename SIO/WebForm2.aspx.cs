using CapaControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIO
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        public ControlSolicitudFacturacion controlsf = new ControlSolicitudFacturacion();
        public ControlPedido contpv = new ControlPedido();
        public PedidoVenta pedidoventa = new PedidoVenta();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                cargarGrdPlanos();
                configurarControles();
            }
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            string fup = Session["FUP"].ToString();
            string id = Session["IdAcc"].ToString();
            string consecutivo = Session["ConsecutivoItem"].ToString();
            string erp = Session["ERP"].ToString();
            int estado_item = 0;

            if (!String.IsNullOrEmpty(fup)) 
            {
                string directorio = "";
                string ruta = "";
                DataTable dtp = new DataTable();
                dtp = contpv.consultarRutaPlano(Convert.ToInt32(id));

                if (dtp.Rows.Count > 0)
                {
                    ruta = dtp.Rows[0]["planos"].ToString();
                    if (!String.IsNullOrEmpty(ruta))
                    {
                        directorio = @ruta;
                    }
                    else
                    {
                        directorio = @"I:\PV_Planos\" + fup + @"\" + id +@"\" + erp + @"\";  
                    }
                }
                else { directorio = @"I:\PV_Planos\" + fup + @"\" + id + @"\" + erp + @"\"; }                

                //string dirweb = @"~/PV/" + txtFUP.Text + @"/";

                if (!(Directory.Exists(directorio)))
                {
                    Directory.CreateDirectory(directorio);
                }

                Session["directorioPlnao"] = directorio ;

                if (filePlano.HasFile)
                {
                    //foreach (HttpPostedFile postedFile in filePlano.PostedFile)
                    //{
                        HttpPostedFile postedFile = filePlano.PostedFile;
                        string fileName = Path.GetFileName(postedFile.FileName);
                        postedFile.SaveAs(directorio + fileName);

                        //Inserto en logPV planos
                        string Nombre = Session["Nombre_Usuario"].ToString();
                        string Estado = "Inserta Plano";
                        controlsf.IngresarDatosLOGpv(Convert.ToInt32(fup), 0, 0, "", "", Estado, Nombre, fileName, 0, 0, "");
                        contpv.actualizarEstadoPlano(Convert.ToInt32(id), 1, Session["directorioPlnao"].ToString());
                        
                    //}

                    cargarGrdPlanos();
                    configurarControles();
                } 
                else
                {
                    string mensaje = "";
                    string idioma = (string)Session["Idioma"];

                    if (idioma == "Español")
                    {
                        mensaje = "Archivo no seleccionado.";
                    }
                    if (idioma == "Ingles")
                    {
                        mensaje = "Unselected File.";
                    }
                    if (idioma == "Portugues")
                    {
                        mensaje = "Arquivo desmarcada.";
                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
            }                   
        }

        private void cargarGrdPlanos()
        {
            string fup = Session["FUP"].ToString();
            string id = Session["IdAcc"].ToString();
            string consecutivo = Session["ConsecutivoItem"].ToString();
            string erp = Session["ERP"].ToString();              

            if (!String.IsNullOrEmpty(fup))
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("cot_item");
                dt.Columns.Add("path_plano");
                dt.Columns.Add("name_plano");

                string directorio = "";
                string ruta = "";
                DataTable dtp = new DataTable();
                dtp = contpv.consultarRutaPlano(Convert.ToInt32(id));

                if (dtp.Rows.Count > 0)
                {
                    ruta = dtp.Rows[0]["planos"].ToString();
                    if (!String.IsNullOrEmpty(ruta))
                    {
                        directorio = @ruta;
                    }
                    else
                    {
                        directorio = @"I:\PV_Planos\" + fup + @"\" + id + @"\" + erp + @"\";
                    }
                }
                else { directorio = @"I:\PV_Planos\" + fup + @"\" + id + @"\" + erp + @"\"; }

                Session["directorioPlnao"] = directorio;

                //string directorio = @"I:\PV_Planos\" + fup + @"\" + id + @"\";
                if (Directory.Exists(directorio))
                {
                    string[] smFiles = Directory.GetFiles(directorio);
                    foreach (String fi in smFiles)
                    {
                        DataRow row = dt.NewRow();
                        row["cot_item"] = id;
                        row["path_plano"] = fi;
                        row["name_plano"] = Path.GetFileName(fi); ;
                        dt.Rows.Add(row);
                    }
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    Session["planos"] = dt;
                }                
            }
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            System.Web.UI.WebControls.Label labelPath = ((System.Web.UI.WebControls.Label)GridView1.Rows[e.RowIndex].FindControl("path_plano"));
            string path = labelPath.Text;
            File.Delete(path);

            //Inserto en logPV planos
            string fup = Session["FUP"].ToString();
            string Nombre = Session["Nombre_Usuario"].ToString();
            string Estado = "Elimina Plano";
            string id = Session["IdAcc"].ToString();
            controlsf.IngresarDatosLOGpv(Convert.ToInt32(fup), 0, 0, "", "", Estado, Nombre, path, 0, 0, "");

            string[] smFiles = Directory.GetFiles(@Session["directorioPlnao"].ToString());
            if (smFiles.Length == 0)
            {
                contpv.actualizarEstadoPlano(Convert.ToInt32(id), 0, "");
            }
            cargarGrdPlanos();
            configurarControles();
        }

        private void Reload_tbPlanos()
        {
            GridView1.DataSource = Session["planos"] as DataTable;
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            Reload_tbPlanos();
        }

        private void configurarControles()
        {
            int rol = (int)Session["Rol"];
            string fup = Session["FUP"].ToString();
            SqlDataReader reader = contpv.seleccionarConfirmaciones(Convert.ToInt32(fup));

            if (reader.HasRows)
            {
                reader.Read();
                int conf_comercial = Convert.ToInt32(reader.GetValue(0));
                int conf_asistente = Convert.ToInt32(reader.GetValue(1));
                int conf_ingenieria = Convert.ToInt32(reader.GetValue(2));
                int rechazo_asistente = Convert.ToInt32(reader.GetValue(3));
                int rechazo_ingenieria = Convert.ToInt32(reader.GetValue(4));   

                //Si es Asistente, Ing o  Comercial y está confirmado por comercial y está confirmado por asistente y esta confirmado por ingenieria
                if ((rol == 9 || rol == 4 || rol == 3 || rol == 30 || rol == 2) && conf_comercial == 1 && conf_asistente == 1 && conf_ingenieria == 1)
                {
                    filePlano.Enabled = false;
                    UploadButton.Enabled = false;
                    GridView1.Enabled = false;                    
                }

                //Si es Asistente y está confirmado por comercial y no está confirmado por asistente y no esta confirmado por ingenieria
                else if ((rol == 9 || rol == 2) && Convert.ToInt32(reader.GetValue(0)) == 1 && Convert.ToInt32(reader.GetValue(1)) == 0 && Convert.ToInt32(reader.GetValue(2)) == 0)
                {
                    filePlano.Enabled = true;
                    UploadButton.Enabled = true;
                    GridView1.Enabled = true;                      
                }

                //Si es Asistente y está confirmado por comercial y está confirmado por asistente y no esta confirmado por ingenieria
                else if ((rol == 9 || rol == 2) && Convert.ToInt32(reader.GetValue(0)) == 1 && Convert.ToInt32(reader.GetValue(1)) == 1 && Convert.ToInt32(reader.GetValue(2)) == 0)
                {
                    filePlano.Enabled = false;
                    UploadButton.Enabled = false;
                    GridView1.Enabled = false;  
                }

                //Si es Asistente y no está confirmado por comercial y no está confirmado por asistente y no esta confirmado por ingenieria
                else if ((rol == 9 || rol == 2) && Convert.ToInt32(reader.GetValue(0)) == 0 && Convert.ToInt32(reader.GetValue(1)) == 0 && Convert.ToInt32(reader.GetValue(2)) == 0)
                {
                    filePlano.Enabled = true;
                    UploadButton.Enabled = true;
                    GridView1.Enabled = true;  
                }

                //Si es Ingenieria y está confirmado por Asistente y está confirmado por Comercial y NO está confirmado por Ingenieria
                else if (rol == 4)
                {
                    filePlano.Enabled = false;
                    UploadButton.Enabled = false;
                    GridView1.Enabled = false;  
                }
                
                // Si es Comercial y NO está confirmado por Comercial
                else if ((rol == 3 || rol == 30) && Convert.ToInt32(reader.GetValue(0)) == 0)
                {
                    filePlano.Enabled = true;
                    UploadButton.Enabled = true;
                    GridView1.Enabled = true;  
                }
                // Si es Comercial y está confirmado por Comercial
                else if ((rol == 3 || rol == 30) && Convert.ToInt32(reader.GetValue(0)) == 1)
                {
                    filePlano.Enabled = false;
                    UploadButton.Enabled = false;
                    GridView1.Enabled = false;  
                }
                else
                {
                    filePlano.Enabled = false;
                    UploadButton.Enabled = false;
                    GridView1.Enabled = false;  
                }
                reader.Close();
                reader.Dispose();
                contpv.CerrarConexion();
            }

            else
            {
                if (rol == 3 || rol == 30 || rol == 9 || rol == 2)
                {
                    filePlano.Enabled = true;
                    UploadButton.Enabled = true;
                    GridView1.Enabled = true;  
                }
                else
                {
                    filePlano.Enabled = false;
                    UploadButton.Enabled = false;
                    GridView1.Enabled = false;  
                }
            }

            reader = contpv.consultarCirreVenta(Convert.ToInt32(fup));
            if (reader.HasRows)
            {
                reader.Read();
                if (!String.IsNullOrEmpty(reader.GetValue(0).ToString()))
                {
                    filePlano.Enabled = false;
                    UploadButton.Enabled = false;
                    GridView1.Enabled = false;  
                }
                reader.Close();
                reader.Dispose();
                contpv.CerrarConexion();
            }
        }
    }
}