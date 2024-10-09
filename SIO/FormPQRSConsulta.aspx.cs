using CapaControl;
using CapaControl.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Runtime.InteropServices;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Data.SqlClient;

namespace SIO
{
    public partial class FormPQRSConsulta : System.Web.UI.Page
    {
       

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scripManager = ScriptManager.GetCurrent(this.Page);
            scripManager.EnablePageMethods = true;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

        }

        [WebMethod]
        public static string ObtenerFuentes()
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            string response = string.Empty;
            List<PQRSFuente> fuentes = controlPQRS.ObtenerFuentesActivas();
            response = JsonConvert.SerializeObject(fuentes);
            return response;
        }

        [WebMethod]
        public static string ObtenerPQRS()

        {
            PQRSDTO pqrs = new PQRSDTO();
            ControlPQRS controlPQRS = new ControlPQRS();
            string response = string.Empty;
            List<PQRSDTO> listpqrss = controlPQRS.ObtenerPRQS(pqrs);
            response = JsonConvert.SerializeObject(listpqrss);
            return response;
        }

        [WebMethod]
        public static string ObtenerPQRSHistorico(string idpqrs)

        {
            ControlPQRS controlPQRS = new ControlPQRS();
            string response = string.Empty;
            List<PQRSDTOHistorico> listpqrss = controlPQRS.ObtenerPRQSHistorico(idpqrs);
            response = JsonConvert.SerializeObject(listpqrss);
            return response;
        }

        [WebMethod]
        public static string ObtenerPQRSArchivo(string idpqrs)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            string response = string.Empty;
            List<PQRSDTOArchivo> listpqrss = controlPQRS.ObtenerPQRSArchivo(idpqrs);
            response = JsonConvert.SerializeObject(listpqrss);
            return response;

        }


        [WebMethod(EnableSession = true)]
        public static string ObtenerPQRSpor(PQRSFiltros filtros)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            int usuarioId = Convert.ToInt32(HttpContext.Current.Session["usuId"]);
            int rolId = Convert.ToInt32(HttpContext.Current.Session["Rol"]);
            string NombreUsu = (string)HttpContext.Current.Session["Usuario"];
            string emailUsuario = HttpContext.Current.Session["rcEmail"].ToString();
            filtros.hasta = new DateTime(filtros.hasta.Year, filtros.hasta.Month, filtros.hasta.Day, 23, 59, 59);
            string response = string.Empty;
            List<PQRSDTOConsulta> listpqrss = controlPQRS.ObtenerPRQSpor(rolId, usuarioId, emailUsuario, NombreUsu, filtros);
            response = JsonConvert.SerializeObject(listpqrss);
            return response;
        }

        [WebMethod(EnableSession = true)]
        public static string ObtenerPermisos()
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            int usuarioId = Convert.ToInt32(HttpContext.Current.Session["usuId"]);
            int rolId = Convert.ToInt32(HttpContext.Current.Session["Rol"]);
            string response = string.Empty;
            List<PQRSFuente> fuentes = controlPQRS.ObtenerFuentesActivas();
            List<PQRSTipo> tipos = controlPQRS.ObtenerTiposPQRS();
            Permiso permiso = controlPQRS.ObtenerPermisos(rolId, usuarioId);
//            List<PQRSDTORDEN> numordenes = controlPQRS.ObtenerOrdenesActivas();
            List<PQRSPosiblesEstados> estados = FormPQRSResumen.ObtenerPosiblesEstadosForConsulta();

            var res = new { fuentes = fuentes,
                permiso = permiso,
//                numordenes = numordenes,
                tipos = tipos,
                estados = estados, 
                usuarioId = usuarioId,
                rolId = rolId
            };
            response = JsonConvert.SerializeObject(res);
            return response;
        }

        [WebMethod]
        public static string ObtenerProcesos(string tipopqrs)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            string response = string.Empty;
            List<PQRSProceso> procesos = controlPQRS.ObtenerProcesos(tipopqrs);
            response = JsonConvert.SerializeObject(procesos);
            return response;
        }

        [WebMethod]
        public static string ObtenerOrdenesActivas(string IdPqrs)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            List<PQRSDTORDEN> numordenes = controlPQRS.ObtenerOrdenesActivas(IdPqrs);
            string response = string.Empty;
            response = JsonConvert.SerializeObject(numordenes);
            return response;
        }

        [WebMethod(EnableSession = true)]
        public static void ClosePQRS(int pqrsId)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            string NombreUsu = (string)HttpContext.Current.Session["Usuario"];
            string emailUsuario = HttpContext.Current.Session["rcEmail"].ToString();

            controlPQRS.ClosePQRS(pqrsId, NombreUsu, emailUsuario);
        }

        [WebMethod(EnableSession = true)]
        public static string GuardarProcesos(PQRSProcesoSave procesos)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            string NombreUsu = (string)HttpContext.Current.Session["Usuario"];
            string response = string.Empty;
            bool resultado = controlPQRS.GuardarProcesos(NombreUsu, procesos);
            string correoSistema = (string)HttpContext.Current.Session["CorreoSistema"];

            SendEmailsProcesosAsignados(procesos.procesos, correoSistema, procesos.PQRSId);

            PQRSDTOConsulta pqrs = controlPQRS.ObtenerPQRSId(procesos.PQRSId.ToString(), string.Empty);
            if (pqrs.IdFuenteReclamo == 6)
            {
                HttpContext.Current.Server.MapPath("~");
                String rutaAplicacion = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);//Toma la ruta del aplicativo
                rutaAplicacion = rutaAplicacion.Replace("\\\\", "\\");
                NotificarInternoForsa(pqrs.IdPQRS, pqrs.Cliente, 1, "Asignacion de Procesos", correoSistema, rutaAplicacion, pqrs.Colaborador);
            }
            response = JsonConvert.SerializeObject(true);
            return response;
        }

        [WebMethod(EnableSession = true)]
        public static string ObtenerProcesosAsignadosEmail(string idpqrs)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            string response = string.Empty;
            string emailUsuario = HttpContext.Current.Session["rcEmail"].ToString();
            List<PQRSProcesoAsignado> listProcesos = controlPQRS.ObtenerProcesosAsignadosEmail(idpqrs, emailUsuario);
            response = JsonConvert.SerializeObject(listProcesos);
            return response;
        }


        [WebMethod]
        public static string ObtenerProcesosAsignados(string idpqrs)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            string response = string.Empty;
            List<PQRSProceso> listProcesos = controlPQRS.ObtenerProcesosAsignados(idpqrs);
            response = JsonConvert.SerializeObject(listProcesos);
            return response;
        }


        [WebMethod]
        public static string GuardarPQRSRespuesta(PQRSRespuesta pqrsRespuestaproceso)
        {
            PQRSDTOHistorico pqrsHistorico = new PQRSDTOHistorico();
            string NombreUsu = (string)HttpContext.Current.Session["Usuario"];
            string email = (string)HttpContext.Current.Session["rcEmail"];
            pqrsRespuestaproceso.Usuario = NombreUsu;
            ControlPQRS controlPQRS = new ControlPQRS();
            string response = string.Empty;
            Dictionary<string, int> responseControl = controlPQRS.GuadarRespuestaProceso(pqrsRespuestaproceso, email);
            // Notificar Respuesta
            PQRSDTOConsulta pqrs = controlPQRS.ObtenerPQRSId(pqrsRespuestaproceso.PQRSId.ToString(), email);
            string correoSistema = (string)HttpContext.Current.Session["CorreoSistema"]; HttpContext.Current.Server.MapPath("~");
            String rutaAplicacion = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);//Toma la ruta del aplicativo
            rutaAplicacion = rutaAplicacion.Replace("\\\\", "\\");
            List<PQRSProcesosAdmin> procesosAdmin = JsonConvert.DeserializeObject<List<PQRSProcesosAdmin>>(FormPQRSResumen.ObtenerProcesosAdmin());
            PQRSProcesosAdmin procesoAdmin = procesosAdmin.Where(x => x.Proceso == pqrsRespuestaproceso.Proceso).FirstOrDefault();
            SendEmailsGeneral(pqrs, (int)EstadosPQRS.RespuestaProceso, pqrsRespuestaproceso.Mensaje, Copias: procesoAdmin.EmailProcesoCC);
            if (pqrs.IdFuenteReclamo == 6 && responseControl["estadoActual"] == 2)
            {
                NotificarInternoForsa(pqrs.IdPQRS, pqrs.Cliente, 1, "Análisis de Respuestas", correoSistema, rutaAplicacion, pqrs.Colaborador);
            }
            NotificarInternoForsa(pqrs.IdPQRS, pqrs.Cliente, 2, "Análisis de Respuestas", correoSistema, rutaAplicacion, pqrs.Colaborador);
            //response = JsonConvert.SerializeObject(listProcesos);
            return responseControl["Id"].ToString();
        }


        [WebMethod]
        public static string ObtenerRespuestaProcesos(string idLog)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            string response = string.Empty;
            PQRSRespuestaHistorico respuesta = controlPQRS.ObtenerRespuestasProcesos(idLog);
            respuesta.archivos = controlPQRS.ObtenerPRQSRespuestaArchivo(respuesta.Id.ToString());
            response = JsonConvert.SerializeObject(respuesta);
            return response;
        }

        [WebMethod]
        public static string ObtenerListadosPlanos(string idpqrs)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            string response = string.Empty;
            string email = (string)HttpContext.Current.Session["rcEmail"];
            List<PQRSListadosPlanos> datos = controlPQRS.ObtenerListadosPlanos(idpqrs, email);
            //bool cargaLisAcero = datos.Where(v => v.TipoCargue == "Listados" && v.Tipo == "Acero").Any(x => x.Correo == email);
            //bool cargaLisAlum = datos.Where(v => v.TipoCargue == "Listados" && v.Tipo == "Aluminio").Any(x => x.Correo == email);
            //bool cargaPlanAcero = datos.Where(v => v.TipoCargue == "Planos" && v.Tipo == "Acero").Any(x => x.Correo == email);
            //bool cargaPlanAlum = datos.Where(v => v.TipoCargue == "Planos" && v.Tipo == "Aluminio").Any(x => x.Correo == email);

            bool cargaLisAcero = datos.Where(v => v.TipoCargue == "Listados" && v.Tipo == "Acero").Any(x => x.Correo == email);
            bool cargaLisAlum = datos.Where(v => v.TipoCargue == "Listados" && v.Tipo == "Aluminio").Any(x => x.Correo == email);
            bool cargaPlanAcero = datos.Where(v => v.TipoCargue == "Planos" && v.Tipo == "Acero").Any(x => x.Correo == email);
            bool cargaPlanAlum = datos.Where(v => v.TipoCargue == "Planos" && v.Tipo == "Aluminio").Any(x => x.Correo == email);
            bool cargaPlanArmadoAcero = datos.Where(v => v.TipoCargue == "Armado" && v.Tipo == "Acero").Any(x => x.Correo == email);
            bool cargaPlanArmadoAlum = datos.Where(v => v.TipoCargue == "Armado" && v.Tipo == "Aluminio").Any(x => x.Correo == email);
            var debeCargar = new
            {
                listados = new { acero = cargaLisAcero, aluminio = cargaLisAlum },
                planos = new { acero = cargaPlanAcero, aluminio = cargaPlanAlum },
                armado = new { acero = cargaPlanArmadoAcero, aluminio = cargaPlanArmadoAlum }
            };
            Dictionary<string, object> vars = new Dictionary<string, object>();
            vars.Add("datos", datos);
            vars.Add("debeCargar", debeCargar);
            response = JsonConvert.SerializeObject(vars);
            return response;
        }

        [WebMethod]
        public static string ObtenerDatosProcedencia()
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            string response = string.Empty;
            List<PQRSProceso> procesos = controlPQRS.ObtenerProcesos("2");
            List<TipoNC> tipoNC = controlPQRS.ObtenerTIPONC();
            var res = new
            {
                procesos = procesos,
                tiponc = tipoNC
            };
            response = JsonConvert.SerializeObject(res);
            return response;
        }

        public static void NotificarInternoForsa(int idpqrs, string nombreCliente, 
            int evento, string mensajeAdicional, string correoSistema, string rutaAplicacion,
            string correoDestinatario)
        {
            string mensaje = "";
            string asunto = "Notificacion Interno Forsa PQRS #" + idpqrs.ToString() + " - Cliente: " + nombreCliente;
            switch (evento)
            {
                case 1: // Si es 1 significa que cambió el estado
                    mensaje = "La PQRS ha avanzado hacia el estado: " + mensajeAdicional;
                    break;
                case 2: // Si es 2 significa que se cargaron archivos
                    mensaje = "Se han cargado nuevos archivos y/o respuestas en en la etapa de: " + mensajeAdicional;
                    break;
                default: // Caso general para enviar cualquier mensaje:
                    mensaje = mensajeAdicional;
                    break;
            }
            mensaje += "<br>Puedes consultar el modulo para su revisión"
                    + "<br><a href='http://app.forsa.com.co/siomaestros/FormPQRSConsulta.aspx?IdPQRS=" + idpqrs.ToString() + "'>  Ir PQRS  </a><br>"
                    + "<br><a href='http://app.forsa.com.co/siomaestros/FormPQRSResumen.aspx?IdPQRS=" + idpqrs.ToString() + "'>Ir a Resumen PQRS</a><br>";
            SendMail(correoSistema, correoDestinatario, "", asunto, mensaje, rutaAplicacion, Firma: 0);
        }

        [WebMethod(EnableSession = true)]
        public static void AdicionarProcesos(int idPqrs, PQRSProceso[] Procesos)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            string NombreUsu = (string)HttpContext.Current.Session["Usuario"];
            controlPQRS.GuardarProcesosAdicionalesProcedencia(idPqrs, NombreUsu, Procesos);
        }

        [WebMethod(EnableSession = true)]
        public static string GuardarReclamoProcedente(pqrsProcedente pqrs, string hallazgosNoProcedentes)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            string NombreUsu = (string)HttpContext.Current.Session["Usuario"];
            string correoSistema = (string)HttpContext.Current.Session["CorreoSistema"];
            HttpContext.Current.Server.MapPath("~");
            String rutaAplicacion = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);//Toma la ruta del aplicativo
            rutaAplicacion = rutaAplicacion.Replace("\\\\", "\\");
            controlPQRS.GuardarReclamoProcedente(NombreUsu, pqrs, hallazgosNoProcedentes);
            PQRSDTOConsulta pqrsConsultado = controlPQRS.ObtenerPQRSId(pqrs.IdPQRS.ToString(), string.Empty);
            if (!pqrs.EsProcedente)
            {
                SendEmailsGeneral(pqrsConsultado, 4, "");
            }

            string response = JsonConvert.SerializeObject(true);
            return response;
        }

        [WebMethod(EnableSession = true)]
        public static string GenerarOrden(pqrsGenerarOrden pqrs)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            string email = (string)HttpContext.Current.Session["rcEmail"];
            string NombreUsu = (string)HttpContext.Current.Session["Usuario"];
            string correoSistema = (string)HttpContext.Current.Session["CorreoSistema"];
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@PQRSId", (int)pqrs.IdPQRS);
            List<DatosGeneralesFupPQRS> data = ControlDatos.EjecutarStoreProcedureConParametros<DatosGeneralesFupPQRS>("USP_fup_SEL_PQRSDatosGenerales", parametros);

            int idNuevoEstado = controlPQRS.GenerarOrden(NombreUsu, pqrs);
            HttpContext.Current.Server.MapPath("~");
            String rutaAplicacion = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);//Toma la ruta del aplicativo
            rutaAplicacion = rutaAplicacion.Replace("\\\\", "\\");

            if (pqrs.RequierelistadosAcero || pqrs.RequierelistadosAluminio
                || pqrs.RequiereplanosAcero || pqrs.RequiereplanosAluminio
                || pqrs.RequierearmadoAcero || pqrs.RequierearmadoAluminio)
            {
                string asunto = "Se generó la " + data[0].OrdenProcedente + " - Orden de fabricación: " + data[0].NroOrden + " por favor generar listados y/o planos de fabricación";
                string mensaje = "Se le ha asignado una PQRS, por favor revise su bandeja y cargue los listados y/o planos requeridos";
                mensaje += "<br><a href='http://app.forsa.com.co/siomaestros/FormPQRSConsulta.aspx?IdPQRS=" + pqrs.IdPQRS.ToString() + "'>  Ir PQRS  </a><br>"
                               + "<br><a href='http://app.forsa.com.co/siomaestros/FormPQRSResumen.aspx?IdPQRS=" + pqrs.IdPQRS.ToString() + "'>Ir a Resumen PQRS</a><br>";
                List<string> listCorreos = new List<string>();
                if(pqrs.RequierelistadosAceroCorreos != null && pqrs.RequierelistadosAceroCorreos.Length > 1 )
                { listCorreos.AddRange(pqrs.RequierelistadosAceroCorreos.Split(';').ToList()); }
                if(pqrs.RequierelistadosAluminioCorreos != null && pqrs.RequierelistadosAluminioCorreos.Length > 1)
                { listCorreos.AddRange(pqrs.RequierelistadosAluminioCorreos.Split(';').ToList()); }
                if(pqrs.RequiereplanosAceroCorreos != null && pqrs.RequiereplanosAceroCorreos.Length > 1)
                { listCorreos.AddRange(pqrs.RequiereplanosAceroCorreos.Split(';').ToList()); }
                if(pqrs.RequiereplanosAluminioCorreos != null && pqrs.RequiereplanosAluminioCorreos.Length > 1)
                { listCorreos.AddRange(pqrs.RequiereplanosAluminioCorreos.Split(';').ToList()); }
                if (pqrs.RequierearmadoAceroCorreos != null && pqrs.RequierearmadoAceroCorreos.Length > 1)
                { listCorreos.AddRange(pqrs.RequierearmadoAceroCorreos.Split(';').ToList()); }
                if (pqrs.RequierearmadoAluminioCorreos != null && pqrs.RequierearmadoAluminioCorreos.Length > 1)
                { listCorreos.AddRange(pqrs.RequierearmadoAluminioCorreos.Split(';').ToList()); }
                listCorreos = listCorreos.Distinct().ToList();
                string totalCorreos = String.Join(";", listCorreos.ToArray());
                if (listCorreos.Count != 0)
                {

                    Task taskA = Task.Run(() =>
                    {
                        SendMail(correoSistema, totalCorreos, "", asunto, mensaje, rutaAplicacion);
                    });
                }
            }

            PQRSDTOConsulta pqrsConsultado = controlPQRS.ObtenerPQRSId(pqrs.IdPQRS.ToString(), string.Empty);
            if (pqrsConsultado.IdFuenteReclamo == 6)
            {
                if (idNuevoEstado == 5)
                {
                    NotificarInternoForsa(pqrsConsultado.IdPQRS, pqrsConsultado.Cliente, 1, "Ingenieria", correoSistema, rutaAplicacion, pqrsConsultado.Colaborador);
                }
                else if (idNuevoEstado == 9)
                {
                    NotificarInternoForsa(pqrsConsultado.IdPQRS, pqrsConsultado.Cliente, 1, "Final PQRS", correoSistema, rutaAplicacion, pqrsConsultado.Colaborador);
                }
            }

            string response = JsonConvert.SerializeObject(true);
            return response;
        }

        [WebMethod]
        public static string ObtenerProcedenciaHistorico(string idPQRS)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            string response = string.Empty;
            pqrsProcedenteHistorico res = controlPQRS.ObtenerHistoricoProcedente(idPQRS);
            response = JsonConvert.SerializeObject(res);
            return response;
        }

        [WebMethod]
        public static string ObtenerPlantas()
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            string response = string.Empty;
            List<PQRSPlanta> plantas = controlPQRS.ObtenerPlantas();
            response = JsonConvert.SerializeObject(plantas);
            return response;
        }


        public static void SendEmailsProcesosAsignados(List<PQRSProceso> procesos, string correoSistema, int PQRSId)
        {
            try
            {
                ControlPQRS controlPQRS = new ControlPQRS();
                string emailb = string.Empty;
                PQRSDTOConsulta pqrs = controlPQRS.ObtenerPQRSId(PQRSId.ToString(), emailb);


                string allemails = string.Join(";", procesos.Select(x => x.EmailProceso).ToList());
                string allemailsCC = string.Join(";", procesos.Select(x => x.EmailProcesoCC).ToList());
                List<string> emails = allemails.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                string asunto = string.Empty;
                string mensaje = string.Empty;

                switch (pqrs.TipoPQRSId)
                {
                    case (int)TipoPQRS.Reclamo:
                        asunto = "Nueva Reclamación PQRS Id: " + PQRSId.ToString() + " - Orden " + pqrs.NroOrden.ToString() + " Cliente: "+ pqrs.Cliente;
                        mensaje = "Estimado Integrante, "
                                + "<br>Se ha generado una nueva reclamación que requiere su atención: SIO/PQRS No " + PQRSId.ToString()
                                + "<br><a href='http://app.forsa.com.co/siomaestros/FormPQRSConsulta.aspx?IdPQRS=" + PQRSId.ToString() + "'>  Ir PQRS  </a><br>"
                                +"<br><a href='http://app.forsa.com.co/siomaestros/FormPQRSResumen.aspx?IdPQRS=" + PQRSId.ToString() + "'> Ir a Resumen PQRS </a><br>";
                        break;
                    default:
                        asunto = "Asignación de " + pqrs.TipoPQRS+ " PQRS # " + PQRSId.ToString();
                        mensaje = "Estimado Integrante, <br>"
                                + "Se le ha asignado una PQRS, por favor consulte el módulo para su respectiva respuesta.<br><br>"
                                + "<br><a href='http://app.forsa.com.co/siomaestros/FormPQRSConsulta.aspx?IdPQRS=" + PQRSId.ToString() + "'>  Ir PQRS  </a><br>"
                                + "<br><a href='http://app.forsa.com.co/siomaestros/FormPQRSResumen.aspx?IdPQRS=" + PQRSId.ToString() + "'>Ir a Resumen PQRS</a><br>";
                        break;
                }

                HttpContext.Current.Server.MapPath("~");
                String rutaAplicacion = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);//Toma la ruta del aplicativo
                rutaAplicacion = rutaAplicacion.Replace("\\\\", "\\");
                
                // Se le notifica a los responsables de dar respuesta en cada proceso
                Task taskA = Task.Run(() =>
                {
                    SendMail(correoSistema, allemails, allemailsCC, asunto, mensaje, rutaAplicacion);
                });

                // Se le envia correos a los 
            }
            catch (Exception ex ) {
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                string NombreUsu = "SIO";
                parametros.Clear();
                parametros.Add("@pFupID", 0);
                parametros.Add("@pVersion", "");
                parametros.Add("@pUsuario", NombreUsu);
                parametros.Add("@pErrorNumber", "600");
                parametros.Add("@pErrorProcedure", "correoPQRS - Asignado");
                parametros.Add("@pErrorline", 1999);
                parametros.Add("@pMensaje", ex.Message);

                ControlDatos.GuardarStoreProcedureConParametros("[USP_FUP_ERROR]", parametros);
            }
        }
        public static void SendMail(string correoSistema, string destinatarioEmail, string Cc, string asuntoMail, string mensaje, String rutaLogo, [Optional] List<PQRSFilesDTO> adjuntos, [Optional] int Firma, [Optional] bool incluirEncuesta)
        {
            try
            {
                MailMessage mail = new MailMessage();
                //INDICAMOS EL EMAIL DE ORIGEN
                mail.From = new MailAddress(correoSistema);

                //AÑADIMOS LA DIRRECCIÓN DE CORREO DESTINATARIO
                string[] listCorreosDestinanarios = destinatarioEmail.Split(';');
                if (listCorreosDestinanarios.Length != 0)
                {
                    foreach (string email in listCorreosDestinanarios)
                    {
                        mail.To.Add(email);
                    }

                }
                
                //mail.To.Add("ivanvidal@forsa.net.co");


                if (!string.IsNullOrEmpty(Cc))
                {
                    string[] listCorreos = Cc.Split(';');
                    if (listCorreos.Length != 0) { 
                        foreach (string email in listCorreos)
                        {
                            if (Firma == 1)
                            {
                                mail.Bcc.Add(email);
                            }
                            else
                            {
                                mail.CC.Add(email);
                            }
                        }
                    
                    }

                }

                string tabla_html = "<table><tr><td>" + mensaje + "</td></tr>" +
                    "<tr><td>Cordialmente;</td></tr>";

                    switch (Firma) {
                        case 1:
                        tabla_html += "<tr><td><h2>Equipo Experiencia del Cliente</h2></td></tr>";
                        tabla_html += "<tr><td><h2></h2></td></tr>";
                        tabla_html += "<tr><td>Por favor no responda este email, para dudas sobre la información enviada por favor comunicarse al email reclamosysugerencias@forsa.net.co</td></tr>";
                        tabla_html += "<tr><td>Por favor não retorne este e-mail, dúvidas sobre a informação enviada envie para este e - mail sac@forsa.net.br</td></tr>";
                        tabla_html += "<tr><td>Please don’t reply to this email, if you have any questions about the information sent please contact us at contact@forsausa.com</td></tr>";

                        
   
                        // Abril 2024 -- Se envian las encuestas de PQRS desde el modulo de encuestas.
                        //if (incluirEncuesta)
                        //{
                        //    tabla_html += "<tr><td><img src=\"http://app.forsa.com.co/siomaestros/Imagenes/colombia.png\">&nbsp;&nbsp;<a href=\"https://forms.office.com/r/gtgzPHWbXW\" > Link de Encuesta de Satisfacción</a></td></tr>" +
                        //        "<tr><td><img src=\"http://app.forsa.com.co/siomaestros/Imagenes/united-states.png\">&nbsp;&nbsp;<a href=\"https://forms.office.com/r/Vn8GeyB7AK\" > Satisfaction Survey Link</a></td></tr>" +
                        //        "<tr><td><img src=\"http://app.forsa.com.co/siomaestros/Imagenes/brazil.png\">&nbsp;&nbsp;<a href=\"https://forms.office.com/r/uX6p43M4ku\" > Link da pesquisa de satisfação</a></td></tr>";
                        //}
                        break;
                    case 2:
                        tabla_html += "<tr><td>Cordialmente/ Atenciosamente / Best Regards</td></tr>";
                        tabla_html += "<tr><td></td></tr>";
                        tabla_html += "<tr><td><h2>Experiencia del Cliente/Experiência do Cliente/Customer Experience</h2></td></tr>";
                            break;
                    case 3:
                        tabla_html += "<tr><td><h2>Equipo Experiencia del Cliente</h2></td></tr>";
                        break;
                    default:
                        tabla_html += "<tr><td><h2>Equipo PQRS</h2></td></tr>";
                        break;
                }
                tabla_html += "<tr><td></td></tr><tr><td><img src=\"http://app.forsa.com.co/siomaestros/Imagenes/LogoForsaComunicado.png\" ></td></tr>" + "</table>";


                //INCLUIMOS EL ASUNTO DEL MENSAJE
                mail.Subject = asuntoMail;
                //AÑADIMOS EL CUERPO DEL MENSAJE
                mail.Body = tabla_html.Replace("\n", "<br>"); //mensaje + Environment.NewLine  + "\r\n" + " <img src=\"cid:companylogo\">";
                //INDICAMOS EL TIPO DE CODIFICACIÓN DEL MENSAJE
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                //DEFINIMOS LA PRIORIDAD DEL MENSAJE
                mail.Priority = System.Net.Mail.MailPriority.Normal;
                //INDICAMOS SI EL CUERPO DEL MENSAJE ES HTML O NO
                mail.IsBodyHtml = true; 
                SmtpClient smtp = new SmtpClient();
                //DEFINIMOS NUESTRO SERVIDOR SMTP
                smtp.Host = "smtp.office365.com";
                //INDICAMOS LA AUTENTICACION DE NUESTRO SERVIDOR SMTP
                smtp.Credentials = new System.Net.NetworkCredential("informes@forsa.net.co", "Those7953");
                // smtp.Port = 25;
                smtp.Port = 587;
                smtp.EnableSsl = true;
                //smtp.Timeout = 400;

                //add the image
                string imageFilePath = "";
               // String dlDir = @"PQRSImagenComunicado\";
               // String directorio = rutaLogo + dlDir;
               // string pathFile = directorio + "LogoForsaComunicado.png";
               // imageFilePath = pathFile;
               // LinkedResource inlineLogo = new LinkedResource(imageFilePath, MediaTypeNames.Image.Jpeg);
               // inlineLogo.ContentId = "companylogo";
               AlternateView avHtml = AlternateView.CreateAlternateViewFromString(mail.Body, null, MediaTypeNames.Text.Html);
               // avHtml.LinkedResources.Add(inlineLogo);
                mail.AlternateViews.Add(avHtml);

                if (adjuntos != null && adjuntos.Count > 0)
                {
                    foreach (var file in adjuntos)
                    {
                       
                        byte[] stemp = Convert.FromBase64String(file.base64); 
                        Stream stream = new MemoryStream(stemp);
                        mail.Attachments.Add(new Attachment(stream, Path.GetFileName(file.nameFile)));
                    }
                   
                }

                

                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain,
                    SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Clear();
                parametros.Add("@pFupID", 0);
                parametros.Add("@pVersion", "");
                parametros.Add("@pUsuario", destinatarioEmail);
                parametros.Add("@pErrorNumber", "600");
                parametros.Add("@pErrorProcedure", "correoPQRS");
                parametros.Add("@pErrorline", 1999);
                parametros.Add("@pMensaje", ex.Message);

                ControlDatos.GuardarStoreProcedureConParametros("[USP_FUP_ERROR]", parametros);
            }
        }

        private static List<PQRSFilesDTO> CreateFilesDTOSendComunicado(List<PQRSDTOArchivo> loadedFiles, 
            List<EnviarArchivosComunicado> selectedFilesToSend)
        {
            List<PQRSFilesDTO> filesDTOs = new List<PQRSFilesDTO>();
            foreach (var archivo in loadedFiles)
            {
                bool add = false;
                String rutaFile = "I:/";
                foreach (EnviarArchivosComunicado enviar in selectedFilesToSend)
                {
                    if (enviar.Id == archivo.Id)
                    {
                        add = true;
                    }
                }
                if (add)
                {
                    string pathFile = rutaFile + archivo.FilePATH;
                    byte[] archivoBytes = File.ReadAllBytes(pathFile);
                    string archivoBase64 = Convert.ToBase64String(archivoBytes);
                    filesDTOs.Add(new PQRSFilesDTO()
                    {
                        base64 = archivoBase64,
                        nameFile = archivo.FileName,
                        type = "",
                    });
                }
            }
            return filesDTOs;
        }

        [WebMethod]
        public static void EnviarComunicadoCliente(Comunicado comunicadoCliente)
        {
            string correoSistema = (string)HttpContext.Current.Session["CorreoSistema"];
            string email = (string)HttpContext.Current.Session["rcEmail"];
            string NombreUsu = (string)HttpContext.Current.Session["Usuario"];
            ControlPQRS controlPQRS = new ControlPQRS();
            HttpContext.Current.Server.MapPath("~");
            String rutaAplicacion = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);//Toma la ruta del aplicativo
            rutaAplicacion = rutaAplicacion.Replace("\\\\", "\\");
            PQRSDTOConsulta pqrs = controlPQRS.ObtenerPQRSId(comunicadoCliente.Id.ToString(), email);
            string Asunto = comunicadoCliente.Asunto +" "+ pqrs.Cliente;

            controlPQRS.GuardarEstadoProcesoRespuesta(comunicadoCliente.Id, email, NombreUsu, comunicadoCliente);
            List<PQRSDTOArchivo> listpqrss = controlPQRS.ObtenerPQRSArchivo(comunicadoCliente.Id.ToString());
            if(comunicadoCliente.archivosRadicadoEnviar != null)
            {
                comunicadoCliente.archivosRadicadoEnviar = comunicadoCliente.archivosRadicadoEnviar.Where(x => x.Enviar == true).ToList();
            }

            List<PQRSDTOArchivo> listArchivosComprobante = controlPQRS.ObtenerPQRSArchivosComprobante(comunicadoCliente.Id.ToString());
            if(comunicadoCliente.archivosComprobanteEnviar != null)
            {
                comunicadoCliente.archivosComprobanteEnviar = comunicadoCliente.archivosComprobanteEnviar.Where(x => x.Enviar == true).ToList();
            }

            if (comunicadoCliente.archivos == null)
            {
                comunicadoCliente.archivos = new List<PQRSFilesDTO>();
            }

            // Add radicado files
            comunicadoCliente.archivos.AddRange(CreateFilesDTOSendComunicado(listpqrss,
                comunicadoCliente.archivosRadicadoEnviar));

            // Add comprobante files
            comunicadoCliente.archivos.AddRange(CreateFilesDTOSendComunicado(listArchivosComprobante,
                comunicadoCliente.archivosComprobanteEnviar));

            // Cuando se trate del envío de comunicado utilizar el evento #105 y adicionar el proceso para BCC
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            if (comunicadoCliente.incluirEncuesta)
            {
                parametros.Add("@pEvento", 106);
            } else
            {
                parametros.Add("@pEvento", 105);
            }
            parametros.Add("@pFupID ", pqrs.IdFup ?? 0);
            parametros.Add("@pVersion", pqrs.Version);
            
            parametros.Add("@pUsuario", NombreUsu);
            parametros.Add("@pRemitente", email);
            parametros.Add("@pParte", 0);

            List<NotificaFup> data = ControlDatos.EjecutarStoreProcedureConParametros<NotificaFup>("USP_fup_notificacionesN", parametros);

            string DestinatariosMail = Convert.ToString(data.FirstOrDefault().Lista);
            comunicadoCliente.Cc = comunicadoCliente.Cc + ' ' ;
            if ( comunicadoCliente.Cc.Length > 2) { comunicadoCliente.Cc = comunicadoCliente.Cc + ';'; }
            comunicadoCliente.Cc = comunicadoCliente.Cc + DestinatariosMail;


            if(comunicadoCliente.incluirEncuesta)
            {
                parametros.Clear();
                parametros.Add("@pTipo", 2);
                parametros.Add("@pIdEntidad", pqrs.IdPQRS);
                parametros.Add("@pNombreContacto", pqrs.NombreRespuesta);
                parametros.Add("@pEmailContacto", comunicadoCliente.Para);

                List< int > Encuesta = ControlDatos.EjecutarStoreProcedureConParametros <int>("USP_INS_RegistroEncuesta", parametros);

            }

            SendMail(correoSistema, comunicadoCliente.Para, comunicadoCliente.Cc,Asunto , comunicadoCliente.Mensaje, rutaAplicacion,comunicadoCliente.archivos, 1, comunicadoCliente.incluirEncuesta);
        }

        //[WebMethod]
        //public static string ObtenerOrdenesActivas()
        //{
        //    //ControlPQRS controlPQRS = new ControlPQRS();
        //    string response = string.Empty;
        //    //List<PQRSDTORDEN> Ordenes = controlPQRS.ObtenerOrdenesActivas();
        //    //response = "";// JsonConvert.SerializeObject(Ordenes);
        //    return response;
        //}

        [WebMethod]
        public static void ListadosCompletos(string idpqrs)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            string NombreUsu = (string)HttpContext.Current.Session["Usuario"];
            controlPQRS.ListadosCompletos(idpqrs, NombreUsu);
        }

        [WebMethod]
        public static void Produccion(string idpqrs)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            string NombreUsu = (string)HttpContext.Current.Session["Usuario"];
            controlPQRS.Produccion(idpqrs, NombreUsu);
        }

        [WebMethod]
        public static string GuardarListadosRequeridos(PQRSListadosRequeridos pqrsListadoReq)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            string response = string.Empty;
            string NombreUsu = (string)HttpContext.Current.Session["Usuario"];
            string emailUsuario = HttpContext.Current.Session["rcEmail"].ToString();
            int id = controlPQRS.GuadarListadosRequeridos(pqrsListadoReq, NombreUsu, emailUsuario);
            //response = JsonConvert.SerializeObject(listProcesos);
            PQRSDTOConsulta pqrs = controlPQRS.ObtenerPQRSId(pqrsListadoReq.IdPQRS.ToString(), string.Empty);
            SendEmailsGeneral(pqrs, 5, NombreUsu);
            if (pqrs.IdFuenteReclamo == 6)
            {
                string correoSistema = (string)HttpContext.Current.Session["CorreoSistema"];
                HttpContext.Current.Server.MapPath("~");
                String rutaAplicacion = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);//Toma la ruta del aplicativo
                rutaAplicacion = rutaAplicacion.Replace("\\\\", "\\");
                NotificarInternoForsa(pqrs.IdPQRS, pqrs.Cliente, 2, "Listados", correoSistema, rutaAplicacion, pqrs.Colaborador);
            }
            return id.ToString();
        }

        [WebMethod]
        public static string GuardarimplmentacionObra(PQRSImplementacionObra pqrsObra)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            string response = string.Empty;
            int id = controlPQRS.GuardarimplmentacionObra(pqrsObra, (string)HttpContext.Current.Session["Usuario"]);
            //response = JsonConvert.SerializeObject(listProcesos);
            PQRSDTOConsulta pqrs = controlPQRS.ObtenerPQRSId(pqrsObra.IdPQRS.ToString(), string.Empty);
            if (pqrs.IdFuenteReclamo == 6)
            {
                string correoSistema = (string)HttpContext.Current.Session["CorreoSistema"];
                HttpContext.Current.Server.MapPath("~");
                String rutaAplicacion = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);//Toma la ruta del aplicativo
                rutaAplicacion = rutaAplicacion.Replace("\\\\", "\\");
                NotificarInternoForsa(pqrs.IdPQRS, pqrs.Cliente, 1, "Implementación en Obra", correoSistema, rutaAplicacion, pqrs.Colaborador);
            }
            return id.ToString();
        }

        [WebMethod]
        public static string GuardarCierreReclamacion(string planAccion, string fechaCierre, string idpqrs, string descripcionPlanAccion)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            string response = string.Empty;
            int id = controlPQRS.guardarCierreReclamacion(planAccion, fechaCierre, int.Parse(idpqrs), descripcionPlanAccion);
            PQRSDTOConsulta pqrs = controlPQRS.ObtenerPQRSId(idpqrs, string.Empty);
            if (pqrs.IdFuenteReclamo == 6)
            {
                string correoSistema = (string)HttpContext.Current.Session["CorreoSistema"];
                HttpContext.Current.Server.MapPath("~");
                String rutaAplicacion = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);//Toma la ruta del aplicativo
                rutaAplicacion = rutaAplicacion.Replace("\\\\", "\\");
                NotificarInternoForsa(pqrs.IdPQRS, pqrs.Cliente, 1, "Plan de Accion y Cierre", correoSistema, rutaAplicacion, pqrs.Colaborador);
            }
            return id.ToString();
        }
        
        [WebMethod(EnableSession = true)]
        public static string GuardarPQRSProduccion(PQRSProduccion prod)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            //string response = string.Empty;
            string NombreUsu = (string)HttpContext.Current.Session["Usuario"];
            controlPQRS.guardarPQRSProduccion(prod, NombreUsu);
            PQRSDTOConsulta pqrs = controlPQRS.ObtenerPQRSId(prod.IdPQRS.ToString(), string.Empty);
            if (pqrs.IdFuenteReclamo == 6)
            {
                string correoSistema = (string)HttpContext.Current.Session["CorreoSistema"];
                HttpContext.Current.Server.MapPath("~");
                String rutaAplicacion = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);//Toma la ruta del aplicativo
                rutaAplicacion = rutaAplicacion.Replace("\\\\", "\\");
                NotificarInternoForsa(pqrs.IdPQRS, pqrs.Cliente, 1, "Producción", correoSistema, rutaAplicacion, pqrs.Colaborador);
            }
            return string.Empty;
        }

        [WebMethod(EnableSession = true)]
        public static string GuardarPQRSComprobante(PQRSPComprobante prod)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            //string response = string.Empty;
            string NombreUsu = (string)HttpContext.Current.Session["Usuario"];
            controlPQRS.guardarPQRSComprobante(prod, NombreUsu);
            PQRSDTOConsulta pqrs = controlPQRS.ObtenerPQRSId(prod.IdPQRS.ToString(), string.Empty);
            if (pqrs.IdFuenteReclamo == 6)
            {
                string correoSistema = (string)HttpContext.Current.Session["CorreoSistema"];
                HttpContext.Current.Server.MapPath("~");
                String rutaAplicacion = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);//Toma la ruta del aplicativo
                rutaAplicacion = rutaAplicacion.Replace("\\\\", "\\");
                NotificarInternoForsa(pqrs.IdPQRS, pqrs.Cliente, 1, "Comprobante de Entrega en Obra", correoSistema, rutaAplicacion, pqrs.Colaborador);
            }
            return string.Empty;
        }

        [WebMethod(EnableSession = true)]
        public static string ObtenerUsuariosListados(string idPQRS)
        {
            string response;
            ControlPQRS controlPQRS = new ControlPQRS();
            string NombreUsu = (string)HttpContext.Current.Session["Usuario"];
            string emailUsuario = HttpContext.Current.Session["rcEmail"].ToString();
            List<UsuarioRequiereListadoDTO> listusuariolistados = controlPQRS.ObtenerUsuariosListados(idPQRS, NombreUsu, emailUsuario);
            response = JsonConvert.SerializeObject(listusuariolistados);
            return response;
        }

        [WebMethod]
        public static string RadicarPQRS(string idpqrs)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            string response = string.Empty;
            string email = string.Empty;
            string NombreUsu = (string)HttpContext.Current.Session["Usuario"];
            controlPQRS.RadicarPQRS(idpqrs, NombreUsu);
            PQRSDTOConsulta pqrs = controlPQRS.ObtenerPQRSId(idpqrs, email);
            SendEmailsGeneral(pqrs, (int)EstadosPQRS.Radicado);
            if (pqrs.IdFuenteReclamo == 6)
            {
                string correoSistema = (string)HttpContext.Current.Session["CorreoSistema"];
                HttpContext.Current.Server.MapPath("~");
                String rutaAplicacion = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);//Toma la ruta del aplicativo
                rutaAplicacion = rutaAplicacion.Replace("\\\\", "\\");
                NotificarInternoForsa(pqrs.IdPQRS, pqrs.Cliente, 1, "Radicado", correoSistema, rutaAplicacion, pqrs.Colaborador);
            }
            return response;
        }

        [WebMethod]
        public static string AnularPQRS(string idpqrs)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            string response = string.Empty;
            string NombreUsu = (string)HttpContext.Current.Session["Usuario"];
            controlPQRS.AnularPQRS(idpqrs, NombreUsu);
            PQRSDTOConsulta pqrs = controlPQRS.ObtenerPQRSId(idpqrs, string.Empty);
            if (pqrs.IdFuenteReclamo == 6)
            {
                string correoSistema = (string)HttpContext.Current.Session["CorreoSistema"];
                HttpContext.Current.Server.MapPath("~");
                String rutaAplicacion = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);//Toma la ruta del aplicativo
                rutaAplicacion = rutaAplicacion.Replace("\\\\", "\\");
                NotificarInternoForsa(pqrs.IdPQRS, pqrs.Cliente, 1, "Anulada", correoSistema, rutaAplicacion, pqrs.Colaborador);
            }
            return response;
        }

        public static void SendEmailsGeneral(PQRSDTOConsulta pqrs, int Proceso, [Optional] string MensajeAdd,
            [Optional] string Copias)
        {
            try
            {
                ControlFUP controlFup = new ControlFUP();
                string UsuarioAsunto = (string)HttpContext.Current.Session["Usuario"];
                string Nombre = (string)HttpContext.Current.Session["Nombre_Usuario"];
                string CorreoUsuario = (string)HttpContext.Current.Session["rcEmail"];
                string correoSistema = (string)HttpContext.Current.Session["CorreoSistema"];

                int parte = 0;
                int firma = 0;
                int evento = 91;

                if (Proceso == (int)EstadosPQRS.RespuestaProceso) { evento = 92; } 
                if (Proceso == (int)EstadosPQRS.Elaboracion) { evento = 95; }
                if (Proceso == (int)EstadosPQRS.ReclamoProcedente && !pqrs.EsProcedente) { evento = 96; }
                if (Proceso == (int)EstadosPQRS.Ingenieria) { evento = 97; }

                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@pFupID ", pqrs.IdFup ?? 0);
                parametros.Add("@pVersion", pqrs.Version);
                parametros.Add("@pEvento", evento);
                parametros.Add("@pUsuario", UsuarioAsunto);
                parametros.Add("@pRemitente", CorreoUsuario);
                parametros.Add("@pParte", parte);

                List<NotificaFup> data = ControlDatos.EjecutarStoreProcedureConParametros<NotificaFup>("USP_fup_notificacionesN", parametros);

                string DestinatariosMail = Convert.ToString(data.FirstOrDefault().Lista);
                DestinatariosMail.Replace(",", ";");
                List<string> emails = DestinatariosMail.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                string ccs = "";

                if(Copias != null)
                {
                    ccs = Copias;
                }

                HttpContext.Current.Server.MapPath("~");
                String rutaAplicacion = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);//Toma la ruta del aplicativo
                rutaAplicacion = rutaAplicacion.Replace("\\\\", "\\");

                string asunto = string.Empty;
                string mensaje = string.Empty;

                if (Proceso == (int)EstadosPQRS.Radicado)
                {
                    switch (pqrs.TipoPQRSId)
                    {
                        case (int)TipoPQRS.Felicitacion:
                            asunto = "Respuesta a sus Comentarios de Felicitación! ";
                            mensaje = "Estimad@ " + pqrs.NombreRespuesta + ", Nos alegramos al recibir tan valiosos elogios de nuestros clientes, estos nos impulsan a seguir mejorando y a comprometernos aún más con la mejora continua."
                                    + "<br><br>Lo invitamos a visitar nuestra página web, donde encontrará contenido de altísimo valor para su obra."
                                    + "<a href='https://www.forsa.com.co'> www.forsa.com.co</a><br>";
                            if (pqrs.IdTipoFuente == 3)
                            {
                                mensaje = "Estimad@ " + pqrs.NombreRespuesta + ", Nos alegramos al recibir tan valiosos elogios de nuestros aliados, estos nos impulsan a seguir mejorando y a comprometernos aún más con la mejora continua."
                                   + "<br><br>Lo invitamos a visitar nuestra página web, donde encontrará contenido de altísimo valor para su obra."
                                   + "<a href='https://www.forsa.com.co'> www.forsa.com.co</a><br>";
                            }
                            firma = 3;
                            break;
                        case (int)TipoPQRS.Reclamo:
                            ControlPQRS cprs = new ControlPQRS();
                            string obrNombre = cprs.ObtenerObraPorFup((int)pqrs.IdFup);
                            asunto = "Creación Reclamo PQRS # " + pqrs.IdPQRS.ToString() + " FP " + pqrs.NroOrden + " OBRA: " + obrNombre;
                            mensaje = "Sr. Cliente, su reclamo ha sido generado con el número " + pqrs.IdPQRS.ToString()
                                    + "<br><br>Estaremos trabajando para solucionarlo en el mejor tiempo posible.<br><br>";
                            mensaje = mensaje + "*******************************************************************************************<br>";
                            mensaje = mensaje + "Mr. Customer, your claim has been generated with the number " + pqrs.IdPQRS.ToString() + " para atender sua solicitação. "
                                    + "<br><br>Trabalhamos para solucionar no menor tempo possível.<br><br>";
                            mensaje = mensaje + "*******************************************************************************************<br>";
                            mensaje = mensaje + "Sr. Cliente, sua reclamação foi gerada com o número " + pqrs.IdPQRS.ToString()
                                    + "<br><br>We will be working to solve it as soon as possible. <br><br>";
                            break;
                        default:
                            asunto = "Creación "+ pqrs.TipoPQRS+" PQRS # " + pqrs.IdPQRS.ToString();
                            mensaje = "Estimad@ " + pqrs.NombreRespuesta + ", para atender su Solicitud hemos generado el PQRS # " + pqrs.IdPQRS.ToString()
                                    + "<br><br>Estaremos trabajando para solucionarlo en el mejor tiempo posible.<br><br>";
                            mensaje = mensaje + "*******************************************************************************************<br>";
                            mensaje = mensaje + "Prezad@ " + pqrs.NombreRespuesta + ", foi a gerado o SAC # " + pqrs.IdPQRS.ToString() + " para atender sua solicitação. "
                                    + "<br><br>Trabalhamos para solucionar no menor tempo possível.<br><br>";
                            mensaje = mensaje + "*******************************************************************************************<br>";
                            mensaje = mensaje + "Dear " + pqrs.NombreRespuesta + ", in order to attend your request we have generated the PQRS # " + pqrs.IdPQRS.ToString() 
                                    + "<br><br>We will be working to solve it as soon as possible. <br><br>";
                            firma = 2;
                            break;
                    }

                    // Lógica temporal pendiente por definir, en caso de que sea una reclamacion y su fuente sea HV, se envia el correo es al técnico
                    if(pqrs.IdFuenteReclamo == 5)
                    {
                        ControlInicio ci = new ControlInicio();
                        SqlDataReader reader = ci.obtenerMailUsuario(pqrs.UsuarioCreacion);
                        reader.Read();
                        string emailCreador = reader.GetValue(0).ToString();
                        reader.Close();
                        pqrs.EmailRespuesta = emailCreador;
                    }
                    // Fin lógica temporal pendiente por definir

                    // Se notifica Primero el cliente  -- Para los casos de Reclamos via Hoja de vida se notifica al tecnico que esta en HV
                    //if(pqrs.IdFuenteReclamo != 5 )
                    //{
                    if (!(pqrs.TipoPQRSId == (int)TipoPQRS.Reclamo && pqrs.IdFuenteReclamo == 5))
                    {
                        SendMail(correoSistema, pqrs.EmailRespuesta, "", asunto, mensaje, rutaAplicacion, null, firma);
                    }
                    //}
                }

                if (Proceso == (int)EstadosPQRS.RespuestaProceso)
                {
                    asunto = "Respuesta " + pqrs.TipoPQRS + " PQRS # " + pqrs.IdPQRS.ToString() + " - " + pqrs.TipoFuente +" : " + pqrs.Cliente;
                    mensaje = "Se dio respuesta al PQRS. número " + pqrs.IdPQRS.ToString()
                            + "<br><a href='http://app.forsa.com.co/siomaestros/FormPQRSConsulta.aspx?IdPQRS=" + pqrs.IdPQRS.ToString() + "'>  Ir PQRS  </a><br>"
                            + "<br><a href='http://app.forsa.com.co/siomaestros/FormPQRSResumen.aspx?IdPQRS=" + pqrs.IdPQRS.ToString() + "'>Ir a Resumen PQRS</a><br>";
                }

                if(Proceso == (int)EstadosPQRS.Elaboracion)
                {
                    if(Convert.ToInt32(pqrs.TipoPQRSId) == (int)TipoPQRS.Reclamo)
                    {
                        asunto = "Se ha generado un nuevo consecutivo  " + pqrs.TipoPQRS + " PQRS # " + pqrs.IdPQRS.ToString() + " - " + pqrs.TipoFuente + " : " + pqrs.Cliente;
                        mensaje = "Estimado Integrante" 
                            + "<br>Por favor consultar el módulo para su respectiva revisión y radicación"
                                + "<br><a href='http://app.forsa.com.co/siomaestros/FormPQRSConsulta.aspx?IdPQRS=" + pqrs.IdPQRS.ToString() + "'>  Ir PQRS  </a><br>"
                                + "<br><a href='http://app.forsa.com.co/siomaestros/FormPQRSResumen.aspx?IdPQRS=" + pqrs.IdPQRS.ToString() + "'>Ir a Resumen PQRS</a><br>";
                        firma = 2;
                    }
                }

                if(Proceso == (int)EstadosPQRS.ReclamoProcedente)
                {
                    asunto = "Reclamo no procedente PQRS # " + pqrs.IdPQRS.ToString() + " - " + pqrs.TipoFuente + " : " + pqrs.Cliente;
                    mensaje = "Este reclamo ha sido marcado como no procedente, por favor indicarle la respuesta al cliente."
                            + "<br><a href='http://app.forsa.com.co/siomaestros/FormPQRSConsulta.aspx?IdPQRS=" + pqrs.IdPQRS.ToString() + "'>  Ir PQRS  </a><br>"
                            + "<br><a href='http://app.forsa.com.co/siomaestros/FormPQRSResumen.aspx?IdPQRS=" + pqrs.IdPQRS.ToString() + "'>Ir a Resumen PQRS</a><br>";
                }

                if(Proceso == (int)EstadosPQRS.Ingenieria)
                {
                    asunto = "Se han cargado y/o actualizado la información de Planos/Listados requeridos PQRS # " + pqrs.IdPQRS.ToString() + " - " + pqrs.TipoFuente + " : " + pqrs.Cliente;
                    mensaje = "Revise el módulo para su respectivo análisis y consideraciones."
                            + "<br><a href='http://app.forsa.com.co/siomaestros/FormPQRSConsulta.aspx?IdPQRS=" + pqrs.IdPQRS.ToString() + "'>  Ir PQRS  </a><br>"
                            + "<br><a href='http://app.forsa.com.co/siomaestros/FormPQRSResumen.aspx?IdPQRS=" + pqrs.IdPQRS.ToString() + "'>Ir a Resumen PQRS</a><br>";
                }

                // Se notifica la lista de Interesados
                Task taskA = Task.Run(() =>
                {
                    //foreach (string email in emails)
                    //{
                    //    SendMail(correoSistema, email, "", asunto, mensaje, rutaAplicacion, null,firma);
                    //}
                    if(Proceso == (int)EstadosPQRS.Radicado)
                    {
                        mensaje += "<br><a href='http://app.forsa.com.co/siomaestros/FormPQRSConsulta.aspx?IdPQRS=" + pqrs.IdPQRS.ToString() + "'>  Ir PQRS  </a><br>"
                                + "<br><a href='http://app.forsa.com.co/siomaestros/FormPQRSResumen.aspx?IdPQRS=" + pqrs.IdPQRS.ToString() + "'>Ir a Resumen PQRS</a><br>";
                    }

                    // Se controla que si es un reclamo y la fuente es hoja de vida no sé envíe
                    
                    SendMail(correoSistema, DestinatariosMail, ccs, asunto, mensaje, rutaAplicacion, null);
                    
                });
            }
            catch { }
        }
        [WebMethod]
        public static string RespuestaClienteHistorico(string idLog)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            PQRSComunicado comunidado = controlPQRS.ObtenerRespuestasClienteHistorico(idLog);
            return JsonConvert.SerializeObject(comunidado);
        }

        [WebMethod]
        public static string RespuestaProduccionHistorico(string idPQRS)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            PQRSProduccionHistorico comunidado = controlPQRS.ObtenerProduccionHistorico(idPQRS);
            return JsonConvert.SerializeObject(comunidado);
        }
        [WebMethod]
        public static string RespuestaElaboracionHistorico(string idPQRS)
        {
            string email = (string)HttpContext.Current.Session["rcEmail"];
            ControlPQRS controlPQRS = new ControlPQRS();
            PQRSDTOConsulta pqrs = controlPQRS.ObtenerPQRSId(idPQRS, email);
            return JsonConvert.SerializeObject(pqrs);
        }
       

    }

}