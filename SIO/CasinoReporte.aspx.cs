using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using CapaControl;
using System.Data;
using CapaDatos;
using System.IO;
using System.Text;

namespace SIO
{
    public partial class ReporteCasinoUno : System.Web.UI.Page
    {
        public ControlPoliticas CP = new ControlPoliticas();
        private ControlCasino CC = new ControlCasino();
        private InfoCasino InfoCas = new InfoCasino();
        protected void Page_Load(object sender, EventArgs e)
        {   //Solucion del envio, para descargar archivos
            ScriptManager ScriptManage = ScriptManager.GetCurrent(this.Page);//para controlar el envio, con esto se soluciona los problemas
            ScriptManage.RegisterPostBackControl(this.btnNmUnoF);//para controlar el envio, con esto se soluciona los problemas, se agrega el boton en el cual ejecuta la operacion
            ScriptManage.RegisterPostBackControl(this.btnNmUnoA);//para controlar el envio, con esto se soluciona los problemas, se agrega el boton en el cual ejecuta la operacion
            ScriptManage.RegisterPostBackControl(this.btnERP);//para controlar el envio, con esto se soluciona los problemas, se agrega el boton en el cual ejecuta la operacion
            ScriptManage.RegisterPostBackControl(this.btnERPAS);//para controlar el envio, con esto se soluciona los problemas, se agrega el boton en el cual ejecuta la operacion
            //Solucion del envio, para descargar archivos
            if (!IsPostBack)
            {
                Session["UsuarioArchPlano"] = CC.usuarioCasinoArchPlano(Session["Usuario"].ToString());
                if (Session["UsuarioArchPlano"].ToString() == "True")
                {
                    tablaGen.Visible = true;
                    trERP.Visible = true;
                    trNmunos.Visible = true;
                }
                else
                {
                    tablaGen.Visible = false;
                    trERP.Visible = false;
                    trNmunos.Visible = false;
                }
                InfoCas = CC.usuarioActual(Session["Usuario"].ToString());//Me carga los datos generales
                Session["CodArea"] = InfoCas.CodArea;
                Session["NomEmp"] = InfoCas.NomEmp;
                rvReporteUno.Visible = false;
                rvReporteDos.Visible = false;
                Session["Accion"] = "Nuevo";
                politicas(28, Session["usuario"].ToString());
            }
        }
        //Carga el reporte
        private void cargarReporteCasinoUno(String fechaInicial, String fechaFinal)
        {
            rvReporteUno.Visible = true;
            List<ReportParameter> parametro = new List<ReportParameter>();
            parametro.Add(new ReportParameter("fechaInicial", fechaInicial, true));
            parametro.Add(new ReportParameter("fechaFinal", fechaFinal, true));
            rvReporteUno.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            rvReporteUno.ServerReport.ReportServerCredentials = irsc;
            rvReporteUno.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            rvReporteUno.ServerReport.ReportPath = "/GestionHumana/GH_ReporteCasino";
            this.rvReporteUno.ServerReport.SetParameters(parametro);
            rvReporteUno.ShowToolBar = true;
        }
        //Carga el reporte2
        private void cargarReporteCasinoDos(String fechaInicial, String fechaFinal, String area)
        {
            rvReporteDos.Visible = true;
            List<ReportParameter> parametro = new List<ReportParameter>();
            parametro.Add(new ReportParameter("fechaInicial", fechaInicial, true));
            parametro.Add(new ReportParameter("fechaFinal", fechaFinal, true));
            parametro.Add(new ReportParameter("area", area, true));
            rvReporteDos.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            rvReporteDos.ServerReport.ReportServerCredentials = irsc;
            rvReporteDos.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            rvReporteDos.ServerReport.ReportPath = "/GestionHumana/GH_ReporteCasinoDos";
            this.rvReporteDos.ServerReport.SetParameters(parametro);
            rvReporteDos.ShowToolBar = true;
        }
        //Crea y envia al correo los archivos planos para los NMUNOS
        private void crearArchivoPlano(DataTable tablaNMUNO, String nom)
        {
            // Se crea el archivo nuevo
            string filename = "NMBATCH.E" + nom + ""; //nombre de tu arhivo con su extención, ej: archivo.txt
            String rutaAplicacion = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);//Toma la ruta del aplicativo
            rutaAplicacion = rutaAplicacion.Replace("\\\\", "\\");
            String dlDir = @"/NMUNO\";
            String path = rutaAplicacion + dlDir + filename;//Crea la ruta completa
            using (FileStream flujoArchivo = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (StreamWriter escritor = new StreamWriter(flujoArchivo))
                {
                    foreach (DataRow row in tablaNMUNO.Rows)
                    {
                        escritor.Write(row[0].ToString());
                        escritor.Write(row["Log_suc"].ToString());
                        escritor.Write(row["Log_cpto"].ToString());
                        escritor.Write(row["Log_co_mov"].ToString());
                        escritor.Write(row["Log_costo"].ToString());
                        escritor.Write(row["Log_Fecha"].ToString());
                        escritor.Write(row["Log_Fech_tnl"].ToString());
                        escritor.Write(row["Log_Fech_fin_tnl"].ToString());
                        escritor.Write(row["Log_dias_tnl"].ToString());
                        escritor.Write(row["Log_act"].ToString());
                        escritor.Write(row["Log_ubicacion"].ToString());
                        escritor.Write(row["Log_horas"].ToString());
                        escritor.Write(row["valor"].ToString());
                        escritor.Write(row["Log_Cant"].ToString());
                        escritor.Write(row["Log_cuota_nro"].ToString());
                        escritor.Write(row["Log_fecha_pag_hasta"].ToString());
                        escritor.Write(row["Log_Cedula"].ToString());
                        escritor.Write(row["Log_proy"].ToString());
                        escritor.WriteLine(row["Log_ubic_laboral"].ToString());
                    }
                }
            }

            // Se abre el archivo para porderlo desacargar a el cliente    
            if (!String.IsNullOrEmpty(filename))//verifica si el archivo exite
            {
                System.IO.FileInfo toDownload = new System.IO.FileInfo(path);

                if (toDownload.Exists)
                {
                    Response.ClearContent();
                    Response.ClearHeaders();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
                    Response.ContentType = "application/octet-stream";
                    Response.WriteFile(path);//Se carga la ruta para la descarga
                    Response.Flush();
                    Response.Close();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
            //Se envia al correo
            CC.correoCasino2(path, txtFechaInicial.Text, txtFechaFinal.Text,  Session["NomEmp"].ToString(), filename, "CasinoNMUNO");
        }
        //Crea archivos planos para el ERP
        private void crearArchivoPlano2(DataTable tablaERP, String nomArchivo)
        {
            // Se crea el archivo nuevo
            string filename = "" + nomArchivo + ".txt"; //nombre de tu arhivo con su extención, ej: archivo.txt
            String rutaAplicacion = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);//Toma la ruta del aplicativo
            rutaAplicacion = rutaAplicacion.Replace("\\\\", "\\");
            String dlDir = @"/OrdenCompra\";
            String path = rutaAplicacion + dlDir + filename;//Crea la ruta completa
            using (FileStream flujoArchivo = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (StreamWriter escritor = new StreamWriter(flujoArchivo))
                {
                    foreach (DataRow row in tablaERP.Rows)
                    {
                        escritor.Write(row["A"].ToString());
                        escritor.Write(row["B"].ToString());
                        escritor.Write(row["C"].ToString());
                        escritor.Write(row["D"].ToString());
                        escritor.Write(row["E"].ToString());
                        escritor.Write(row["F"].ToString());
                        escritor.Write(row["G"].ToString());
                        escritor.Write(row["H"].ToString());
                        escritor.Write(row["I"].ToString());
                        escritor.Write(row["J"].ToString());
                        escritor.Write(row["K"].ToString());
                        escritor.Write(row["L"].ToString());
                        escritor.Write(row["M"].ToString());
                        escritor.Write(row["N"].ToString());
                        escritor.Write(row["ñ"].ToString());
                        escritor.Write(row["O"].ToString());
                        escritor.Write(row["P"].ToString());
                        escritor.Write(row["Q"].ToString());
                        escritor.Write(row["R"].ToString());
                        escritor.Write(row["S"].ToString());
                        escritor.Write(row["T"].ToString());
                        escritor.Write(row["U"].ToString());
                        escritor.Write(row["V"].ToString());
                        escritor.Write(row["W"].ToString());
                        escritor.Write(row["X"].ToString());
                        escritor.Write(row["Y"].ToString());
                        escritor.Write(row["Z"].ToString());
                        escritor.Write(row["AA"].ToString());
                        escritor.Write(row["BB"].ToString());
                        escritor.Write(row["CC"].ToString());
                        escritor.Write(row["DD"].ToString());
                        escritor.Write(row["EE"].ToString());
                        escritor.Write(row["FF"].ToString());
                        escritor.Write(row["GG"].ToString());
                        escritor.Write(row["HH"].ToString());
                        escritor.Write(row["II"].ToString());
                        escritor.Write(row["JJ"].ToString());
                        escritor.Write(row["KK"].ToString());
                        escritor.Write(row["LL"].ToString());
                        escritor.Write(row["MM"].ToString());
                        escritor.Write(row["NN"].ToString());
                        escritor.Write(row["ññ"].ToString());
                        escritor.Write(row["OO"].ToString());
                        escritor.WriteLine(row["PP"].ToString());
                    }
                }
            }

            if (Session["AprobadoEnvCorreo"] == null || Session["AprobadoEnvCorreo"].ToString() == "NO")
            {
                // Se abre el archivo para porderlo desacargar a el cliente    
                if (!String.IsNullOrEmpty(filename))//verifica si el archivo exite
                {
                    System.IO.FileInfo toDownload = new System.IO.FileInfo(path);

                    if (toDownload.Exists)
                    {
                        Response.ClearContent();
                        Response.ClearHeaders();
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + filename);
                        Response.ContentType = "application/octet-stream";
                        Response.WriteFile(path);//Se carga la ruta para la descarga
                        Response.Flush();
                        Response.Close();
                        HttpContext.Current.ApplicationInstance.CompleteRequest();
                    }
                }
            }
            else
            {
                CC.correoCasino2(path, txtFechaInicial.Text, txtFechaFinal.Text, Session["NomEmp"].ToString(), filename, "CasinoERP");
            }

        }
        //Botones 
        protected void btnVerReportes_Click(object sender, EventArgs e)
        {
            if (validarCamposFecha() == "OK")
            {
                if (Session["UsuarioArchPlano"].ToString() == "True")
                {
                    cargarReporteCasinoUno(txtFechaInicial.Text, txtFechaFinal.Text);
                }
                else
                {
                    cargarReporteCasinoDos(txtFechaInicial.Text, txtFechaFinal.Text, Session["NomEmp"].ToString());
                }
            }
            else
            {
                mensajeVentana(validarCamposFecha());
            }
        }
        protected void btnNmUnoF_Click(object sender, EventArgs e)
        {
            if (validarCamposFecha() == "OK")
            {
                DataTable tablaNMUNOfr = CC.crearNMUNO(txtFechaInicial.Text, txtFechaFinal.Text, "FR");
                crearArchivoPlano(tablaNMUNOfr, "FR");
            }
            else
            {
                mensajeVentana(validarCamposFecha());
            }
        }
        protected void btnNmUnoA_Click(object sender, EventArgs e)
        {
            if (validarCamposFecha() == "OK")
            {
                DataTable tablaNMUNOas = CC.crearNMUNO(txtFechaInicial.Text, txtFechaFinal.Text, "AS");
                crearArchivoPlano(tablaNMUNOas, "AS");
            }
            else
            {
                mensajeVentana(validarCamposFecha());
            }
        }
        //Me valida los campos si estan vacios o si la fecha final es menor que la inicial
        private String validarCamposFecha()
        {
            String mensaje = "";
            if (txtFechaFinal.Text != "" && txtFechaInicial.Text != "")
            {
                if (DateTime.Parse(txtFechaFinal.Text) < DateTime.Parse(txtFechaInicial.Text))
                {
                    mensaje = "La fecha final no puede ser menor a la inicial";
                }
                else
                {
                    mensaje = "OK";
                }
            }
            else
            {
                mensaje = "Los campos no pueden estar vacios";
            }
            return mensaje;
        }
        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }
        protected void btnERP_Click(object sender, EventArgs e)
        {
            Session["AprobadoEnvCorreo"] = "NO";
            if (validarCamposFecha() == "OK")
            {
                DataTable tablaERP = CC.crearERPFR(txtFechaInicial.Text, txtFechaFinal.Text, "00", 006);
                crearArchivoPlano2(tablaERP, "OCSFR");
            }
            else
            {
                mensajeVentana(validarCamposFecha());
            }

        }
        protected void btnERPAS_Click(object sender, EventArgs e)
        {
            Session["AprobadoEnvCorreo"] = "NO";
            if (validarCamposFecha() == "OK")
            {
                DataTable tablaERPAS = CC.crearERPAS(txtFechaInicial.Text, txtFechaFinal.Text, "00", "AS", 002, 2);
                crearArchivoPlano2(tablaERPAS, "OCSAS");
            }
            else
            {
                mensajeVentana(validarCamposFecha());
            }
        }
        protected void btnAprobado_Click(object sender, EventArgs e)
        {
            if (validarCamposFecha() == "OK")
            {
                Session["AprobadoEnvCorreo"] = "SI";
                DataTable tablaERP = CC.crearERPFR(txtFechaInicial.Text, txtFechaFinal.Text, "00", 006);
                crearArchivoPlano2(tablaERP, "OCSFR");
                DataTable tablaERPAS = CC.crearERPAS(txtFechaInicial.Text, txtFechaFinal.Text, "00", "AS", 002, 2);
                crearArchivoPlano2(tablaERPAS, "OCSAS");
                mensajeVentana("Enviados(ERP)");
            }
            else
            {
                mensajeVentana(validarCamposFecha());
            }
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

            if (imprimir == true)
            {
                btnVerReportes.Visible = true;
                btnNmUnoF.Visible = true;
                btnNmUnoA.Visible = true;
                btnERP.Visible = true;
                btnERPAS.Visible = true;
                btnAprobado.Visible = true;
            }
            else
            {
                btnVerReportes.Visible = false;
                btnNmUnoF.Visible = false;
                btnNmUnoA.Visible = false;
                btnERP.Visible = false;
                btnERPAS.Visible = false;
                btnAprobado.Visible = false;
            }
        }
    }
}