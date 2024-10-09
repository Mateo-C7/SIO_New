using CapaControl;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIO
{
    public partial class VisitaLite : System.Web.UI.Page
    {
        public ControlCliente concli = new ControlCliente();
        public ControlObra contobra = new ControlObra();
        public ControlContacto controlCont = new ControlContacto();

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scripManager = ScriptManager.GetCurrent(this.Page);
            scripManager.RegisterPostBackControl(UploadButton);
            scripManager.RegisterPostBackControl(btnCargarObra);
            scripManager.RegisterPostBackControl(btnCargarContacto);
            scripManager.RegisterPostBackControl(btnValidador);

            if (!IsPostBack)
            {
                //eliminarMemoriaCache();

                if (Session["MensajeCliente"] == null)
                    lblMensaje.Text = "";
                else
                {
                    lblMensaje.Text = Session["MensajeCliente"].ToString();
                    cargarReporteLog("LogClienteLite");
                }

                if (Session["MensajeObra"] == null)
                    lblObras.Text = "";
                else
                {
                    lblObras.Text = Session["MensajeObra"].ToString();
                    cargarReporteLog("LogObraLite");
                }

                if (Session["MensajeContacto"] == null)
                    lblContacto.Text = "";
                else
                {
                    lblContacto.Text = Session["MensajeContacto"].ToString();
                    cargarReporteLog("logContactoLite");
                }

                if (Session["MensajeValidadorCliente"] != null)
                {
                    cargarReporteLog("LogValidadorCliente");
                }

                Session["MensajeCliente"] = null;
                Session["MensajeObra"] = null;
                Session["MensajeContacto"] = null;
                Session["MensajeValidadorCliente"] = null;
            }
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (FileUploadControl.HasFile)
            {
                try
                {
                    actualizarEstadoLogs("LogClienteLite");
                    string filename = Path.GetFileName(FileUploadControl.FileName);
                    //string directorio = @"I:\VisitasTemp\";
                    //string directorio = @"C:\VisitasTemp\";
                    //string directorio = Server.MapPath(string.Format("~/Imagenes/VisitasTemp/"));
                    //if (!Directory.Exists(directorio)) { Directory.CreateDirectory(directorio); }                  

                    String rutaAplicacion = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);//Toma la ruta del aplicativo
                    rutaAplicacion = rutaAplicacion.Replace("\\\\", "\\");
                    String dlDir = @"/ArchivosICS\VisitasTemp\";
                    String directorio = "";
                    directorio = rutaAplicacion + dlDir;
                    if (!Directory.Exists(directorio)) { Directory.CreateDirectory(directorio); }

                    HttpPostedFile postedFile = FileUploadControl.PostedFile;
                    postedFile.SaveAs(directorio + filename);

                    string path = directorio + filename;
                    string sql = "SELECT * FROM [Clientes$]";
                    string excelConnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0;";
                    //string excelConnection = "Provider = Microsoft.Jet.OLEDB.4.0; data source = + path +; Extended Properties = Excel 8.0; ";
                    using (OleDbDataAdapter adaptor = new OleDbDataAdapter(sql, excelConnection))
                    {
                        DataSet ds = new DataSet();
                        adaptor.Fill(ds);
                        cargarArchivoExcelClientes(ds, filename);
                        adaptor.Dispose();
                    }

                    Directory.Delete(directorio, true);
                    FileUploadControl.Dispose();
                    Response.Redirect("visitalite.aspx");
                }
                catch (Exception ex)
                {
                    string mensaje = "verifique el nombre de la Hoja sea igual a Clientes. Gracias";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + ex.Message + ' ' + mensaje + "')", true);
                    //Response.Redirect("visitalite.aspx");
                }
            }
        }

        private void cargarArchivoExcelClientes(DataSet ds, string archivo)
        {
            bool valido = true;
            string msjError = "";

            string mensaje = "";
            string cli_nombre = "";
            int cli_pai_id = 0;
            int cli_ciu_id = 0;
            string cli_direccion = "";
            string cli_telefono = "";
            string cli_telefono_2 = "";
            string cli_fax = "";
            string cli_mail = "";
            string cli_web = "";
            string cli_nit = "";
            int cli_tco_id = 0;
            string cli_prefijo = "";
            string cli_prefijo2 = "";
            string cli_prefijofax = "";
            int apoyo = 0;
            int tipoapoyo = 0;
            int vivienda = 0;
            int infra = 0;
            int fila = 0;
            string id_cli_sim = "";
            int origen = 0;
            int id = -1;

            int aciertos = 0;
            string filasError = "";
            string filasExistes = "";
            int error = 0;
            int existente = 0;

            string usuario = (string)Session["Nombre_Usuario"];

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                valido = true;
                msjError = "";

                cli_nombre = "";
                cli_pai_id = 0;
                cli_ciu_id = 0;
                cli_direccion = "";
                cli_telefono = "";
                cli_telefono_2 = "";
                cli_fax = "";
                cli_mail = "";
                cli_web = "";
                cli_nit = "";
                cli_prefijo = "";
                vivienda = 1;
                infra = 0;
                id_cli_sim = "";
                origen = 0;
                id = -1;

                fila = i + 2;

                try
                {
                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["cli nombre"].ToString()))
                    {
                        cli_nombre = ds.Tables[0].Rows[i]["cli nombre"].ToString().Trim();
                    }
                    else
                    {
                        valido = false;
                        msjError += "cli nombre - ";
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["pais id"].ToString()))
                    {
                        cli_pai_id = Convert.ToInt32(ds.Tables[0].Rows[i]["pais id"]);                        
                    }
                    else
                    {
                        valido = false;
                        msjError += "pais id - ";
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["ciudad id"].ToString()))
                    {
                        cli_ciu_id = Convert.ToInt32(ds.Tables[0].Rows[i]["ciudad id"]);
                        
                    }
                    else
                    {
                        valido = false;
                        msjError += "ciudad id - ";
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["cli direccion"].ToString()))
                    {
                        cli_direccion = ds.Tables[0].Rows[i]["cli direccion"].ToString().Trim();
                    }
                    else
                    {
                        valido = false;
                        msjError += "cli direccion - ";
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["cli telefono"].ToString()))
                    {
                        cli_telefono = ds.Tables[0].Rows[i]["cli telefono"].ToString().Trim();
                    }
                    else
                    {
                        valido = false;
                        msjError += "cli telefono - ";
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["cli telefono 2"].ToString()))
                        cli_telefono_2 = ds.Tables[0].Rows[i]["cli telefono 2"].ToString().Trim();

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["cli fax"].ToString()))
                        cli_fax = ds.Tables[0].Rows[i]["cli fax"].ToString().Trim();

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["cli mail"].ToString()))
                        cli_mail = ds.Tables[0].Rows[i]["cli mail"].ToString().Trim();

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["cli web"].ToString()))
                        cli_web = ds.Tables[0].Rows[i]["cli web"].ToString().Trim();

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["cli nit"].ToString()))
                        cli_nit = ds.Tables[0].Rows[i]["cli nit"].ToString().Trim();

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["prefijo"].ToString()))
                        cli_prefijo = ds.Tables[0].Rows[i]["prefijo"].ToString().Trim();

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["vivienda"].ToString()))
                        vivienda = Convert.ToInt32(ds.Tables[0].Rows[i]["vivienda"]);

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["infra"].ToString()))
                        infra = Convert.ToInt32(ds.Tables[0].Rows[i]["infra"]);

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["Cod_Cli_Sim"].ToString()))
                    {
                        id_cli_sim = ds.Tables[0].Rows[i]["Cod_Cli_Sim"].ToString();
                    }
                    //else
                    //{
                    //    valido = false;
                    //    msjError += "Cod_Cli_Sim - ";
                    //}

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["Fuente"].ToString()))
                    {
                        origen = Convert.ToInt32(ds.Tables[0].Rows[i]["Fuente"]);
                    }
                    else
                    {
                        valido = false;
                        msjError += "Fuente - ";
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["cliente id"].ToString()))
                        id = Convert.ToInt32(ds.Tables[0].Rows[i]["cliente id"]);

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["tipo grupo apoyo"].ToString()))
                    {
                        tipoapoyo = Convert.ToInt32(ds.Tables[0].Rows[i]["tipo grupo apoyo"]);
                        apoyo = 1;
                    }

                    if (valido)
                    {
                        int cliente = concli.MatrizLite(cli_nombre, cli_pai_id, cli_ciu_id, cli_direccion, cli_telefono, cli_telefono_2, cli_fax, cli_mail, cli_web, cli_nit, cli_tco_id,
                                            usuario, cli_prefijo, cli_prefijo2, cli_prefijofax, apoyo, tipoapoyo, vivienda, infra, fila, archivo, id_cli_sim, origen, id);

                        if (cliente == 0)
                        {
                            existente++;
                            filasExistes += fila + " - ";
                        }

                        else if (cliente == -1)
                        {
                            error++;
                            filasError += fila + " - ";
                        }

                        else
                        {
                            aciertos++;
                        }
                    }
                    else
                    {
                        string observacion = "Error, campos obligatorios: " + msjError.Substring(0,msjError.Length - 3);
                        concli.insertLogClienteLite(id, fila, archivo, usuario, observacion, cli_nombre, cli_pai_id, cli_ciu_id);
                        error++;
                        filasError += fila + " - ";
                    }                    
                }
                catch (Exception exe)
                {
                    string observacion = "Error, registro no insertado." + exe.Message.Replace('\'', ' ');
                    concli.insertLogClienteLite(id, fila, archivo, usuario, observacion, cli_nombre, cli_pai_id, cli_ciu_id);
                    filasError += fila + " - ";
                    error++;
                }
            }

            if (!String.IsNullOrEmpty(filasError))
                filasError = filasError.Substring(0, filasError.Length - 3);

            if (!String.IsNullOrEmpty(filasExistes))
                filasExistes = filasExistes.Substring(0, filasExistes.Length - 3);

            mensaje = "Registros ingresados: " + aciertos + " <br/> Filas no ingresadas: " + error + " <br/> Filas Actualizadas: " + existente;
            Session["MensajeCliente"] = mensaje;          
        }

        protected void btnCargarObra_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                //try
                {
                    actualizarEstadoLogs("LogObraLite");
                    string filename = Path.GetFileName(FileUpload1.FileName);
                    //string directorio = @"I:\VisitasTemp\";
                    //string directorio = @"C:\VisitasTemp\";
                    //string directorio = Server.MapPath(string.Format("~/Imagenes/VisitasTemp/"));
                    //if (!Directory.Exists(directorio)) { Directory.CreateDirectory(directorio); }

                    String rutaAplicacion = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);//Toma la ruta del aplicativo
                    rutaAplicacion = rutaAplicacion.Replace("\\\\", "\\");
                    String dlDir = @"/ArchivosICS\VisitasTemp\";
                    String directorio = "";
                    directorio = rutaAplicacion + dlDir;
                    if (!Directory.Exists(directorio)) { Directory.CreateDirectory(directorio); }

                    HttpPostedFile postedFile = FileUpload1.PostedFile;
                    postedFile.SaveAs(directorio + filename);

                    string path = directorio + filename;
                    string sql = "SELECT * FROM [Obras$]";
                    string excelConnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0;";

                    using (OleDbDataAdapter adaptor = new OleDbDataAdapter(sql, excelConnection))
                    {
                        DataSet ds = new DataSet();
                        adaptor.Fill(ds);
                        cargarArchivoExcelObras(ds, filename);
                        adaptor.Dispose();
                    }

                    Directory.Delete(directorio, true);
                    FileUpload1.Dispose();
                    Response.Redirect("visitalite.aspx");
                }
                //catch (Exception ex)
                //{
                //    string mensaje = "verifique la Hoja sea igual a Obras. Gracias";
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + ex.Message + ' ' + mensaje + "')", true);
                //    //Response.Redirect("visitalite.aspx");
                //}
            }
        }

        private void cargarArchivoExcelObras(DataSet ds, string archivo)
        {
            bool valido = true;
            string msjError = "";

            int pais = 0; int ciudad = 0; int cliente_id = 0; string obra_nombre = ""; string direccion = ""; string tele1 = ""; string tele2 = "";
            int unidad = 0; int estrato = 0; decimal m2 = 0; string tipovivienda = ""; string usuario = ""; string prefijo1 = ""; string prefijo2 = "";
            int estado = 0; int tecnico = 0; string idCliSim = ""; int id = -1; int fuente = 0; string id_sim_obra = ""; string periodo_sim = "";
            string descripcion = ""; string geo_latitud = ""; string geo_longitud = ""; string url_obra = "";
            int obr_alquiler = 0; int obr_venta = 0; int obr_n_desarrollo = 0;
            //32 es el id por default en Obra_Tipo_Segmento
            int tipo_segmento = 32;
            int sistemaConstru = 1;
            string fecha_ini = ""; string fecha_fin = "";

            int fila = 0;
            string mensaje = "";
            int aciertos = 0;
            string filasError = "";
            string filasExistes = "";
            int error = 0;
            int existente = 0;
            usuario = (string)Session["Nombre_Usuario"];

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                valido = true;
                msjError = "";

                pais = 0; ciudad = 0; cliente_id = -1; obra_nombre = ""; direccion = ""; tele1 = ""; tele2 = "";
                unidad = 0; estrato = 0; m2 = 0; tipovivienda = ""; prefijo1 = "";
                prefijo2 = ""; estado = 0; tecnico = 0;
                idCliSim = ""; id = -1;
                //139 es id fuente SIO
                fuente = 139;
                id_sim_obra = ""; periodo_sim = ""; descripcion = "";
                geo_latitud = ""; geo_longitud = ""; url_obra = ""; 
                //32 es el id por default en Obra_Tipo_Segmento
                tipo_segmento = 32;
                fecha_ini = "";
                fecha_fin = "";
                sistemaConstru = 1;
                obr_venta = 0; obr_alquiler = 0; obr_n_desarrollo = 0;

                fila = i + 2;

                try
                {
                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["pais id"].ToString()))
                    {
                        pais = Convert.ToInt32(ds.Tables[0].Rows[i]["pais id"]);
                    }
                    else
                    {
                        valido = false;
                        msjError += "pais id - ";
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["ciu id"].ToString()))
                    {
                        ciudad = Convert.ToInt32(ds.Tables[0].Rows[i]["ciu id"]);
                    }
                    else
                    {
                        valido = false;
                        msjError += "ciu id - ";
                    }

                    if (String.IsNullOrEmpty(ds.Tables[0].Rows[i]["cliente id"].ToString()) && String.IsNullOrEmpty(ds.Tables[0].Rows[i]["Cod_Cli_Sim"].ToString()))
                    {
                        valido = false;
                        msjError += "cliente id o Cod_Cli_Sim - ";
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["cliente id"].ToString()))
                    {
                        cliente_id = Convert.ToInt32(ds.Tables[0].Rows[i]["cliente id"]);
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["Cod_Cli_Sim"].ToString()))
                    {
                        idCliSim = ds.Tables[0].Rows[i]["Cod_Cli_Sim"].ToString();
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["obra nombre"].ToString()))
                    {
                        obra_nombre = ds.Tables[0].Rows[i]["obra nombre"].ToString().Trim();
                    }
                    else
                    {
                        valido = false;
                        msjError += "obra nombre - ";
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["obr direccion"].ToString()))
                        direccion = ds.Tables[0].Rows[i]["obr direccion"].ToString().Trim();

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["obr telef"].ToString()))
                        tele1 = ds.Tables[0].Rows[i]["obr telef"].ToString().Trim();

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["obr telef2"].ToString()))
                        tele2 = ds.Tables[0].Rows[i]["obr telef2"].ToString().Trim();

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["obr total unidades"].ToString()))
                        unidad = Convert.ToInt32(ds.Tables[0].Rows[i]["obr total unidades"]);

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["ese id"].ToString()))
                    {
                        estrato = Convert.ToInt32(ds.Tables[0].Rows[i]["ese id"]);
                    }
                    else
                    {
                        valido = false;
                        msjError += "ese id - ";
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["obr m2 vivienda"].ToString()))
                        m2 = Convert.ToInt32(ds.Tables[0].Rows[i]["obr m2 vivienda"]);

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["obr tipo vivienda"].ToString()))
                        tipovivienda = ds.Tables[0].Rows[i]["obr tipo vivienda"].ToString().Trim();

                    //if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["fecha crea"].ToString()))
                    //    fechaini = ds.Tables[0].Rows[i]["fecha crea"].ToString().Trim();

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["obr id"].ToString()))
                        id = Convert.ToInt32(ds.Tables[0].Rows[i]["obr id"]);

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["Fuente"].ToString()))
                        fuente = Convert.ToInt32(ds.Tables[0].Rows[i]["Fuente"]);

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["Periodo_SIM"].ToString()))
                        periodo_sim = ds.Tables[0].Rows[i]["Periodo_SIM"].ToString();

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["Descripcion_Obra"].ToString()))
                        descripcion = ds.Tables[0].Rows[i]["Descripcion_Obra"].ToString();

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["ID_SIM_Obra"].ToString()))
                        id_sim_obra = ds.Tables[0].Rows[i]["ID_SIM_Obra"].ToString().Trim();

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["Geo_Latitud"].ToString()))
                        geo_latitud = ds.Tables[0].Rows[i]["Geo_Latitud"].ToString().Trim();

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["Geo_Longitud"].ToString()))
                        geo_longitud = ds.Tables[0].Rows[i]["Geo_Longitud"].ToString().Trim();

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["URL_Obra"].ToString()))
                        url_obra = ds.Tables[0].Rows[i]["URL_Obra"].ToString().Trim();

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["TIPO SEG"].ToString().Trim()))
                        tipo_segmento = Convert.ToInt32(ds.Tables[0].Rows[i]["TIPO SEG"].ToString().Trim());

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["fecha Ini"].ToString()))

                        fecha_ini = ds.Tables[0].Rows[i]["fecha Ini"].ToString().Trim();

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["fecha Fin"].ToString()))
                    {
                        fecha_fin = ds.Tables[0].Rows[i]["fecha Fin"].ToString().Trim();
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["Id_sistema_constru"].ToString()))
                    
                        sistemaConstru = Convert.ToInt32(ds.Tables[0].Rows[i]["Id_sistema_constru"]);


                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["Pot_Forsa"].ToString()))
                    {
                        obr_venta = Convert.ToInt32(ds.Tables[0].Rows[i]["Pot_Forsa"]);
                    }
                    else
                    {
                        valido = false;
                        msjError += "Pot_Forsa - ";
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["Pot_Forsa_Meva"].ToString()))
                    {
                        obr_alquiler = Convert.ToInt32(ds.Tables[0].Rows[i]["Pot_Forsa_Meva"]);
                    }
                    else
                    {
                        valido = false;
                        msjError += "Pot_Forsa_Meva - ";
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["Pot_N_Desarrollo"].ToString()))
                    {
                        obr_n_desarrollo = Convert.ToInt32(ds.Tables[0].Rows[i]["Pot_N_Desarrollo"]);
                    }
                    else
                    {
                        valido = false;
                        msjError += "Pot_N_Desarrollo - ";
                    }

                    if (valido)
                    {
                        int obra = contobra.ObraLite(pais, ciudad, cliente_id, obra_nombre, direccion, tele1, tele2,
                                            unidad, estrato, m2, tipovivienda, usuario, prefijo1, prefijo2,
                                            estado, tecnico, idCliSim, fila, archivo, id, fuente, id_sim_obra, 
                                            periodo_sim, descripcion, geo_latitud, geo_longitud, url_obra,
                                            tipo_segmento, fecha_ini, fecha_fin,sistemaConstru, 
                                            obr_venta,obr_alquiler,obr_n_desarrollo);

                            if (obra == -1)
                        {
                            error++;
                            filasError += fila + " - ";
                        }
                        else
                        {
                            aciertos++;
                        }
                    }
                    else
                    {
                        string observacion = "Error, campos obligatorios: " + msjError.Substring(0, msjError.Length - 3);
                        contobra.insertLogObraLite(id, fila, archivo, usuario, observacion, obra_nombre, pais, ciudad);
                        error++;
                        filasError += fila + " - ";
                    }
                }
                catch (Exception exe)
                {
                    string observacion = "Error, registro no insertado." + exe.Message.Replace('\'', ' ');
                    contobra.insertLogObraLite(id, fila, archivo, usuario, observacion, obra_nombre, pais, ciudad);
                    error++;
                    filasError += fila + " - ";
                }
            }

            if (!String.IsNullOrEmpty(filasError))
                filasError = filasError.Substring(0, filasError.Length - 3);

            if (!String.IsNullOrEmpty(filasExistes))
                filasExistes = filasExistes.Substring(0, filasExistes.Length - 3);

            mensaje = "Registros ingresados: " + aciertos + " <br/> Filas no ingresadas: " + error;
            Session["MensajeObra"] = mensaje;
        }

        protected void btnCargarContacto_Click(object sender, EventArgs e)
        {
            if (FileUpload2.HasFile)
            {
                try
                {
                    actualizarEstadoLogs("LogContactoLite");
                    string filename = Path.GetFileName(FileUpload2.FileName);
                    //string directorio = @"I:\VisitasTemp\";
                    //string directorio = @"C:\VisitasTemp\";
                    //string directorio = Server.MapPath(string.Format("~/Imagenes/VisitasTemp/"));
                    //if (!Directory.Exists(directorio)) { Directory.CreateDirectory(directorio); }

                    String rutaAplicacion = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);//Toma la ruta del aplicativo
                    rutaAplicacion = rutaAplicacion.Replace("\\\\", "\\");
                    String dlDir = @"/ArchivosICS\VisitasTemp\";
                    String directorio = "";
                    directorio = rutaAplicacion + dlDir;
                    if (!Directory.Exists(directorio)) { Directory.CreateDirectory(directorio); }

                    HttpPostedFile postedFile = FileUpload2.PostedFile;
                    postedFile.SaveAs(directorio + filename);

                    string path = directorio + filename;
                    string sql = "SELECT * FROM [Contactos$]";
                    string excelConnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0;";

                    using (OleDbDataAdapter adaptor = new OleDbDataAdapter(sql, excelConnection))
                    {
                        DataSet ds = new DataSet();
                        adaptor.Fill(ds);
                        cargarArchivoExcelContactos(ds, filename);
                        adaptor.Dispose();
                    }

                    Directory.Delete(directorio, true);
                    FileUploadControl.Dispose();
                    Response.Redirect("visitalite.aspx");
                }
                catch (Exception ex)
                {
                    string mensaje = "verifique el nombre de la Hoja sea igual a Contactos. Gracias";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + ex.Message + ' ' + mensaje + "')", true);
                    //Response.Redirect("visitalite.aspx");
                }
            }
        }

        private void cargarArchivoExcelContactos(DataSet ds, string archivo)
        {
            bool valido = true;
            string msjError = "";

            int clienteId = -1; int obraId = 0; int tipoCargoId = 0; string nombre1 = ""; string nombre2 = ""; string apellido1 = ""; string apellido2 = "";
            string telefono1 = ""; string telefono2 = ""; string email1 = ""; string email2 = ""; string movil = ""; bool contCliente = false; bool contObra = false;
            bool contTecnico = false; string hobby = ""; string fechacumple = ""; int feriaId = 0; string usucrea = ""; string fechacrea = "";
            bool chtelefono = false; bool chemail = false; bool chreferencia = false; bool chferia = false; bool chtrabcampo = false; bool chvisita = false; bool chmedicomunic = false;
            bool chcharlas = false; bool chconferencia = false; bool chweb = false; bool chseminarios = false; bool chpublicidad = false; bool chpersonal = false;
            string comentarios = ""; int profesion = 0; string nombrecargo = ""; string prefijo1 = ""; string prefijo2 = ""; string prefijo3 = ""; int id = -1; string idCliSim = "";
            string linkedind = "";
            int pais = 0, ciudad = 0;

            int fila = 0; string mensaje = ""; int aciertos = 0; string filasError = ""; string filasExistes = "";
            int error = 0; int existente = 0;
            string usuario = (string)Session["Nombre_Usuario"];
            usucrea = usuario;
            fechacrea = System.DateTime.Now.ToString("dd/MM/yyyy");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                valido = true;
                msjError = "";

                clienteId = -1; tipoCargoId = 0; nombre1 = ""; apellido1 = ""; telefono1 = ""; telefono2 = "";
                email1 = ""; email2 = ""; movil = ""; hobby = ""; fechacumple = ""; chtelefono = false; chemail = false; chreferencia = false;
                chtrabcampo = false; chvisita = false; chmedicomunic = false; chconferencia = false; chweb = false; chpublicidad = false;
                chpersonal = false; comentarios = ""; profesion = 0; nombrecargo = ""; prefijo1 = ""; prefijo3 = ""; id = -1; pais = 0;
                ciudad = 0; idCliSim = ""; linkedind = "";

                fila = i + 2;

                try
                {
                    if (String.IsNullOrEmpty(ds.Tables[0].Rows[i]["cliente id"].ToString()) && String.IsNullOrEmpty(ds.Tables[0].Rows[i]["Cod_Cli_Sim"].ToString()))
                    {
                        valido = false;
                        msjError += "cliente id o Cod_Cli_Sim - ";
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["cliente id"].ToString()))
                    {
                        clienteId = Convert.ToInt32(ds.Tables[0].Rows[i]["cliente id"]);
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["Cod_Cli_Sim"].ToString()))
                    {
                        idCliSim = ds.Tables[0].Rows[i]["Cod_Cli_Sim"].ToString();
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["tipo cargo id"].ToString()))
                    {
                        tipoCargoId = Convert.ToInt32(ds.Tables[0].Rows[i]["tipo cargo id"]);
                    }
                    else
                    {
                        valido = false;
                        msjError += "tipo cargo id - ";
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["nombre cont"].ToString()))
                    {
                        nombre1 = ds.Tables[0].Rows[i]["nombre cont"].ToString();
                    }
                    else
                    {
                        valido = false;
                        msjError += "nombre cont - ";
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["apellido cont"].ToString()))
                    {
                        apellido1 = ds.Tables[0].Rows[i]["apellido cont"].ToString().Trim();
                    }
                    else
                    {
                        valido = false;
                        msjError += "apellido cont - ";
                    }


                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["ccl telefono"].ToString()))
                    {
                        telefono1 = ds.Tables[0].Rows[i]["ccl telefono"].ToString().Trim();
                    }
                    else
                    {
                        valido = false;
                        msjError += "ccl telefono cont - ";
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["ccl telefono2"].ToString()))
                        telefono2 = ds.Tables[0].Rows[i]["ccl telefono2"].ToString().Trim();

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["ccl email"].ToString()))
                    {
                        email1 = ds.Tables[0].Rows[i]["ccl email"].ToString().Trim();
                    }
                    else
                    {
                        valido = false;
                        msjError += "ccl email - ";
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["ccl email personal"].ToString()))
                    {
                        email1 = ds.Tables[0].Rows[i]["ccl email personal"].ToString().Trim();
                    }
                    else
                    {
                        valido = false;
                        msjError += "ccl email personal - ";
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["ccl cel"].ToString()))
                        movil = ds.Tables[0].Rows[i]["ccl cel"].ToString();

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["ccl hobby"].ToString()))
                        hobby = ds.Tables[0].Rows[i]["ccl hobby"].ToString();
                    
                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["cc telefono"].ToString()))
                    {                        
                        string temp = ds.Tables[0].Rows[i]["cc telefono"].ToString();
                        if (temp.Trim().ToUpper() == "X")
                            chtelefono = true;
                        else
                            chtelefono = false;
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["cc mail"].ToString()))
                    {
                        string temp = ds.Tables[0].Rows[i]["cc mail"].ToString();
                        if (temp.Trim().ToUpper() == "X")
                            chemail = true;
                        else
                            chemail = false;
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["cc referencia"].ToString()))
                    {
                        string temp = ds.Tables[0].Rows[i]["cc referencia"].ToString();
                        if (temp.Trim().ToUpper() == "X")
                            chreferencia = true;
                        else
                            chreferencia = false;
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["cc personal"].ToString()))
                    {
                        string temp = ds.Tables[0].Rows[i]["cc personal"].ToString();
                        if (temp.Trim().ToUpper() == "X")
                            chpersonal = true;
                        else
                            chpersonal = false;
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["cc publicidad"].ToString()))
                    {
                        string temp = ds.Tables[0].Rows[i]["cc publicidad"].ToString();
                        if (temp.Trim().ToUpper() == "X")
                            chpublicidad = true;
                        else
                            chpublicidad = false;
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["cc web"].ToString()))
                    {
                        string temp = ds.Tables[0].Rows[i]["cc web"].ToString();
                        if (temp.Trim().ToUpper() == "X")
                            chweb = true;
                        else
                            chweb = false;
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["cc conferencias"].ToString()))
                    {
                        string temp = ds.Tables[0].Rows[i]["cc conferencias"].ToString();
                        if (temp.Trim().ToUpper() == "X")
                            chconferencia = true;
                        else
                            chconferencia = false;
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["cc comunicacion"].ToString()))
                    {
                        string temp = ds.Tables[0].Rows[i]["cc comunicacion"].ToString();
                        if (temp.Trim().ToUpper() == "X")
                            chmedicomunic = true;
                        else
                            chmedicomunic = false;
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["cc trabajo campo"].ToString()))
                    {
                        string temp = ds.Tables[0].Rows[i]["cc trabajo campo"].ToString();
                        if (temp.Trim().ToUpper() == "X")
                            chtrabcampo = true;
                        else
                            chtrabcampo = false;
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["cc visita"].ToString()))
                    {
                        string temp = ds.Tables[0].Rows[i]["cc visita"].ToString();
                        if (temp.Trim().ToUpper() == "X")
                            chvisita = true;
                        else
                            chvisita = false;
                    }
                    
                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["cc comentarios"].ToString()))
                        comentarios = ds.Tables[0].Rows[i]["cc comentarios"].ToString().Trim();

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["tipo prof id"].ToString().Trim()))
                    {
                        profesion = Convert.ToInt32(ds.Tables[0].Rows[i]["tipo prof id"]);
                    }
                    else
                    {
                        valido = false;
                        msjError += "tipo prof id - ";
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["prefijo telefono"].ToString()))
                        prefijo1 = ds.Tables[0].Rows[i]["prefijo telefono"].ToString().Trim();

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["prefijo cel"].ToString()))
                        prefijo3 = ds.Tables[0].Rows[i]["prefijo cel"].ToString().Trim();

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["contClienteId id"].ToString()))
                        id = Convert.ToInt32(ds.Tables[0].Rows[i]["contClienteId id"]);

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["nombre cargo"].ToString()))
                        nombrecargo = ds.Tables[0].Rows[i]["nombre cargo"].ToString();

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["pais id"].ToString()))
                        pais = Convert.ToInt32(ds.Tables[0].Rows[i]["pais id"]);

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["ciudad id"].ToString()))
                        ciudad = Convert.ToInt32(ds.Tables[0].Rows[i]["ciudad id"]);

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["Fuente"].ToString()))
                        feriaId = Convert.ToInt32(ds.Tables[0].Rows[i]["Fuente"]);               

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["ccl_linkedIn"].ToString()))
                        linkedind = ds.Tables[0].Rows[i]["ccl_linkedIn"].ToString();


                    if (valido)
                    {
                        int insertadoCont = controlCont.guardarDatosContactoClienteLite(clienteId, obraId, tipoCargoId, nombre1, nombre2, apellido1, apellido2,
                                               telefono1, telefono2, email1, email2, movil, contCliente, contObra,
                                               contTecnico, hobby, fechacumple, feriaId, usucrea, fechacrea,
                                               chtelefono, chemail, chreferencia, chferia, chtrabcampo, chvisita, chmedicomunic,
                                               chcharlas, chconferencia, chweb, chseminarios, chpublicidad, chpersonal,
                                               comentarios, profesion, nombrecargo, prefijo1, prefijo2, prefijo3, fila, archivo, id, idCliSim,pais, ciudad, linkedind);

                        if (insertadoCont == -1)
                        {
                            error++;
                            filasError += fila + " - ";
                        }
                        else
                        {
                            aciertos++;
                        }
                    }
                    else
                    {
                        string observacion = "Error, campos obligatorios: " + msjError.Substring(0, msjError.Length - 3);
                        controlCont.insertLogContactoLite(id, fila, archivo, usuario, observacion, nombre1, pais, ciudad);
                        error++;
                        filasError += fila + " - ";
                    }
                }
                catch (Exception exe)
                {
                    string observacion = "Error, registro no insertado." + exe.Message.Replace('\'', ' ');
                    controlCont.insertLogContactoLite(id, fila, archivo, usuario, observacion, nombre1, pais, ciudad);
                    filasError += fila + " - ";
                    error++;
                }
            }

            if (!String.IsNullOrEmpty(filasError))
                filasError = filasError.Substring(0, filasError.Length - 3);

            if (!String.IsNullOrEmpty(filasExistes))
                filasExistes = filasExistes.Substring(0, filasExistes.Length - 3);

            mensaje = "Registros ingresados: " + aciertos + " <br/> Filas no ingresadas: " + error + "";
            Session["MensajeContacto"] = mensaje;
        }

        protected void btnValidador_Click(object sender, EventArgs e)
        {
            if (FileUpload3.HasFile)
            {
                //try
                {
                    actualizarEstadoLogs("LogValidadorCliente");
                    string filename = Path.GetFileName(FileUpload3.FileName);
                    //string directorio = @"I:\VisitasTemp\";
                    //string directorio = @"C:\VisitasTemp\";
                    //string directorio = Server.MapPath(string.Format("~/Imagenes/VisitasTemp/"));
                    //if (!Directory.Exists(directorio)) { Directory.CreateDirectory(directorio); }

                    String rutaAplicacion = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);//Toma la ruta del aplicativo
                    rutaAplicacion = rutaAplicacion.Replace("\\\\", "\\");
                    String dlDir = @"/ArchivosICS\VisitasTemp\";
                    String directorio = "";
                    directorio = rutaAplicacion + dlDir;
                    if (!Directory.Exists(directorio)) { Directory.CreateDirectory(directorio); }

                    HttpPostedFile postedFile = FileUpload3.PostedFile;
                    postedFile.SaveAs(directorio + filename);

                    string path = directorio + filename;
                    string sql = "SELECT * FROM [Clientes$]";
                    string excelConnection = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0;";

                    using (OleDbDataAdapter adaptor = new OleDbDataAdapter(sql, excelConnection))
                    {
                        DataSet ds = new DataSet();
                        adaptor.Fill(ds);
                        cargarArchivoValidadorCliente(ds, filename);
                        adaptor.Dispose();
                    }

                    Directory.Delete(directorio, true);
                    FileUpload3.Dispose();
                    Response.Redirect("visitalite.aspx");
                }
                //catch (Exception ex)
                //{
                //    string mensaje = "verifique el nombre de la Hoja sea igual a Clientes. Gracias";
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + ex.Message + ' ' + mensaje + "')", true);
                //    //Response.Redirect("visitalite.aspx");
                //}
            }
            else
            {
                string mensaje = "No tomo el archivo";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
        }

        private void cargarArchivoValidadorCliente(DataSet ds, string archivo)
        {
            bool valido = true;
            string msjError = "";

            string mensaje = "";
            string cli_nombre = "";
            int cli_pai_id = 0;
            int cli_ciu_id = 0;
           
            int fila = 0;
            string id_cli_sim = "";
            int id = -1;

            int aciertos = 0;
            string filasError = "";
            string filasExistes = "";
            int error = 0;
            int existente = 0;

            string usuario = (string)Session["Nombre_Usuario"];

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                valido = true;
                msjError = "";
                cli_nombre = "";
                cli_pai_id = 0;
                cli_ciu_id = 0;              
                id_cli_sim = "";
                id = -1;
                fila = i + 2;

                try
                {
                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["cli nombre"].ToString()))
                    {
                        cli_nombre = ds.Tables[0].Rows[i]["cli nombre"].ToString().Trim();
                    }
                    else
                    {
                        valido = false;
                        msjError += "cli nombre - ";
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["pais id"].ToString()))
                    {
                        cli_pai_id = Convert.ToInt32(ds.Tables[0].Rows[i]["pais id"]);
                    }
                    else
                    {
                        valido = false;
                        msjError += "pais id - ";
                    }

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["ciudad id"].ToString()))
                    {
                        cli_ciu_id = Convert.ToInt32(ds.Tables[0].Rows[i]["ciudad id"]);
                    }
                    else
                    {
                        valido = false;
                        msjError += "ciudad id - ";
                    } 

                    if (!String.IsNullOrEmpty(ds.Tables[0].Rows[i]["Cod_Cli_Sim"].ToString()))
                    {
                        id_cli_sim = ds.Tables[0].Rows[i]["Cod_Cli_Sim"].ToString();
                    }
                    //else
                    //{
                    //    valido = false;
                    //    msjError += "Cod_Cli_Sim - ";
                    //}

                    if (valido)
                    {
                        int cliente = concli.insertarValidadorCliente(cli_nombre, cli_pai_id, cli_ciu_id, fila, archivo, id_cli_sim, usuario);

                        if (cliente == 0)
                        {
                            aciertos++;
                        }
                        else
                        {
                            error++;
                            filasError += fila + " - ";                           
                        }
                    }
                    else
                    {
                        string observacion = "Error, campos obligatorios: " + msjError.Substring(0, msjError.Length - 3);                       
                        concli.insertarLogValidadorCliente(cli_nombre, archivo, fila, cli_pai_id, cli_ciu_id, id_cli_sim, observacion);
                        filasError += fila + " - ";
                        error++;
                    }
                }
                catch (Exception exe)
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + exe.Message + "')", true);
                    string observacion = "Error, registro no insertado." + exe.Message.Replace('\'', ' ');
                    concli.insertarLogValidadorCliente(cli_nombre, archivo, fila, cli_pai_id, cli_ciu_id, id_cli_sim, observacion);
                    filasError += fila + " - ";
                    error++;
                }
            }

            if (!String.IsNullOrEmpty(filasError))
                filasError = filasError.Substring(0, filasError.Length - 3);

            mensaje = "Registros ingresados: " + aciertos;
            Session["MensajeValidadorCliente"] = mensaje;
        }

        private void actualizarEstadoLogs(string tabla)
        {
            concli.actualizarEstadoLogs(tabla);
        }

        public void cargarReporteLog(string reporte)
        {
            //ReporteVerItinerarios.Width = 1000;
            //ReporteVerItinerarios.Height = 400;
            ReporteVerLogLite.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            ReporteVerLogLite.ServerReport.ReportServerCredentials = irsc;
            ReporteVerLogLite.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            ReporteVerLogLite.ServerReport.ReportPath = "/Mercadeo/" + reporte;
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

        private void eliminarMemoriaCache()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
            //for deleting files
            System.IO.DirectoryInfo di = new DirectoryInfo(path);
            foreach (FileInfo file in di.GetFiles())
            {
                try
                {
                    file.Delete();
                }
                catch { }
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                try
                {
                    dir.Delete(true); //delete subdirectories and files
                }
                catch { }
            }
        }
    }  
}