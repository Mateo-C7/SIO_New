using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaControl.Entity;
using Newtonsoft.Json;
using System.Threading;
using CapaControl;
using System.Globalization;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace SIO
{
    public partial class FormPQRSResumen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scripManager = ScriptManager.GetCurrent(this.Page);
            scripManager.EnablePageMethods = true;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }

        [WebMethod]
        public static string ObtenerDatosGeneralesFupPQRS(int idPQRS)
        {
            string email = (string)HttpContext.Current.Session["rcEmail"];
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@PQRSId", idPQRS);
            parametros.Add("@pEmail", email);
            List<DatosGeneralesFupPQRS> data = ControlDatos.EjecutarStoreProcedureConParametros<DatosGeneralesFupPQRS>("USP_fup_SEL_PQRSDatosGenerales", parametros);

            string response = JsonConvert.SerializeObject(data);
            return response;
        }

        [WebMethod]
        public static string ObtenerClasificacionesPlanAccion()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            List<PQRSClasificacionesPlanAccion> clasificaciones = ControlDatos.EjecutarStoreProcedureConParametros<PQRSClasificacionesPlanAccion>("USP_fup_SEL_PQRSClasificacionesPlanAccion", parametros);
            return JsonConvert.SerializeObject(clasificaciones);
        }

        [WebMethod]
        public static string ObtenerFamiliasGarantias()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            List<PQRSFamiliasGarantias> familiasGarantias = ControlDatos.EjecutarStoreProcedureConParametros<PQRSFamiliasGarantias>("USP_fup_SEL_PQRSFamiliasGarantias", parametros);
            List<PQRSProductosFamiliasGarantias> familiasProductosGarantias = ObtenerProductosFamiliasGarantias();
            familiasGarantias.ForEach(familiaGarantia =>
            {
                familiaGarantia.Productos = familiasProductosGarantias.Where(x => x.IdFamilia == familiaGarantia.Id).ToList();
            });
            return JsonConvert.SerializeObject(familiasGarantias);
        }

        private static List<PQRSProductosFamiliasGarantias> ObtenerProductosFamiliasGarantias()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            return ControlDatos.EjecutarStoreProcedureConParametros<PQRSProductosFamiliasGarantias>("USP_fup_SEL_PQRSFamiliasProductosGarantias", parametros);
        }

        [WebMethod]
        public static string ObtenerProcesosAdmin()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            List<PQRSProcesosAdmin> data = ControlDatos.EjecutarStoreProcedureConParametros<PQRSProcesosAdmin>("USP_fup_SEL_PQRSObtenerProcesosAdmin", parametros);
            string response = JsonConvert.SerializeObject(data);
            return response;
        }

        [WebMethod]
        public static void GuardarDatosGeneralesPQRS(int idpqrs, string detalle,
            string direccion, string email, string telefono, string nombre, string otroCliente,
            int idCiudad, int idPais, int idCliente)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pIdPQRS", idpqrs);
            parametros.Add("@pDetalle", detalle);
            parametros.Add("@pDireccion", direccion);
            parametros.Add("@pEmail", email);
            parametros.Add("@pTelefono", telefono);
            parametros.Add("@pNombre", nombre);
            parametros.Add("@pIdCliente", idCliente);
            if (otroCliente != "")
            {
                parametros.Add("@pOtroCliente", otroCliente);
                parametros.Add("@pCiudadId", idCiudad);
                parametros.Add("@pPaisId", idPais);

            }

            List<RespuestaFilasAfectadas> affectedRows = ControlDatos.EjecutarStoreProcedureConParametros<RespuestaFilasAfectadas>("USP_fup_UPD_PQRSDatosGenerales", parametros);
        }

        [WebMethod]
        public static string GetAsociatedPQRSToOrder(string OrderNumber)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Clear();
            parametros.Add("@pNroOrden", OrderNumber);
            List<PQRSAsociadasAOrden> lisPQRSAsociadas = ControlDatos.EjecutarStoreProcedureConParametros<PQRSAsociadasAOrden>("USP_fup_SEL_PQRSAsociadasAOrden", parametros);
            return JsonConvert.SerializeObject(lisPQRSAsociadas);
        }

        [WebMethod]
        public static string ObtenerPQRSTimeline(int idPQRS)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            #region ConsultarArchivosRadicado
            parametros.Add("@pIdPQRS", idPQRS);
            parametros.Add("@pTipo", 1);
            List<PQRSArchivos> lisRadicado = ControlDatos.EjecutarStoreProcedureConParametros<PQRSArchivos>("USP_fup_SEL_PQRS_ArchivosRadicado", parametros);
            foreach (PQRSArchivos archivoRadicado in lisRadicado)
            {
                archivoRadicado.CanBeDeleted = false;
                if (archivoRadicado.UsuarioArchivo == (string)HttpContext.Current.Session["Usuario"])
                {
                    archivoRadicado.CanBeDeleted = true;
                }
            }
            #endregion

            #region ConsultarProcesosAsignados
            parametros.Clear();
            parametros.Add("@pIdPQRS", idPQRS);
            List<PQRSAsignacionProcesos> lisAsignacionProcesos = ControlDatos.EjecutarStoreProcedureConParametros<PQRSAsignacionProcesos>("USP_fup_SEL_PQRS_ProcesosAsignados", parametros);
            #endregion

            #region ConsultarRespuestasProcesos
            parametros.Clear();
            parametros.Add("@pIdPQRS", idPQRS);
            List<PQRSRespuestaProcesos> dataFromSP = ControlDatos.EjecutarStoreProcedureConParametros<PQRSRespuestaProcesos>("USP_fup_SEL_PQRS_RespuestaProcesos", parametros);
            List<PQRSRespuestaProcesos> lisRespuestasProcesos = new List<PQRSRespuestaProcesos>();
            for (int c = 0; c < dataFromSP.Count; c++)
            {
                if (lisRespuestasProcesos.Exists(x => x.Id == dataFromSP[c].Id))
                {
                    PQRSArchivos archivos = new PQRSArchivos();
                    archivos.IdArchivo = dataFromSP[c].IdArchivo;
                    archivos.Path = dataFromSP[c].Path;
                    archivos.UsuarioArchivo = dataFromSP[c].UsuarioArchivo;
                    archivos.FileName = dataFromSP[c].FileName;
                    archivos.CanBeDeleted = false;
                    if (dataFromSP[c].UsuarioArchivo != ""
                            && archivos.UsuarioArchivo == (string)HttpContext.Current.Session["Usuario"])
                    {
                        archivos.CanBeDeleted = true;
                    }
                    lisRespuestasProcesos.Where(x => x.Id == dataFromSP[c].Id).FirstOrDefault()
                        .Archivos.Add(archivos);
                }
                else
                {
                    if (dataFromSP[c].Path != "")
                    {
                        PQRSArchivos archivos = new PQRSArchivos();
                        archivos.IdArchivo = dataFromSP[c].IdArchivo;
                        archivos.UsuarioArchivo = dataFromSP[c].UsuarioArchivo;
                        archivos.Path = dataFromSP[c].Path;
                        archivos.FileName = dataFromSP[c].FileName;
                        archivos.CanBeDeleted = false;
                        if (dataFromSP[c].UsuarioArchivo != ""
                            && archivos.UsuarioArchivo == (string)HttpContext.Current.Session["Usuario"])
                        {
                            archivos.CanBeDeleted = true;
                        }
                        dataFromSP[c].Archivos = new List<PQRSArchivos>();
                        dataFromSP[c].Archivos.Add(archivos);
                    }
                    lisRespuestasProcesos.Add(dataFromSP[c]);
                }
            }

            lisRespuestasProcesos.Reverse();
            List<PQRSRespuestaProcesos> newLisRespuestasProcesos = new List<PQRSRespuestaProcesos>();
            for (int c = 0; c < lisRespuestasProcesos.Count; c++)
            {
                if (lisRespuestasProcesos[c].IdPadre != null)
                {
                    newLisRespuestasProcesos.Add(BuscarPadresRecursivo(lisRespuestasProcesos, lisRespuestasProcesos[c], null));
                } else
                {
                    newLisRespuestasProcesos.Add(lisRespuestasProcesos[c]);
                }
            }
            #endregion

            #region ConsultarComunicados
            parametros.Clear();
            parametros.Add("@pIdPQRS", idPQRS);
            List<PQRSComunicados> lisComunicadosTemp = ControlDatos.EjecutarStoreProcedureConParametros<PQRSComunicados>("USP_fup_SEL_PQRS_Comunicados", parametros);
            List<PQRSComunicados> lisComunicados = new List<PQRSComunicados>();
            for (int c = 0; c < lisComunicadosTemp.Count; c++)
            {
                if (lisComunicados.Exists(x => x.IdPQRSComunicado == lisComunicadosTemp[c].IdPQRSComunicado))
                {
                    PQRSArchivos archivos = new PQRSArchivos();
                    archivos.IdArchivo = lisComunicadosTemp[c].IdArchivo;
                    archivos.Path = lisComunicadosTemp[c].Path;
                    archivos.FileName = lisComunicadosTemp[c].FileName;
                    lisComunicados.Where(x => x.IdPQRSComunicado == lisComunicadosTemp[c].IdPQRSComunicado).FirstOrDefault()
                        .Archivos.Add(archivos);
                }
                else
                {
                    if (lisComunicadosTemp[c].Path != "")
                    {
                        PQRSArchivos archivos = new PQRSArchivos();
                        archivos.IdArchivo = lisComunicadosTemp[c].IdArchivo;
                        archivos.Path = lisComunicadosTemp[c].Path;
                        archivos.FileName = lisComunicadosTemp[c].FileName;
                        lisComunicadosTemp[c].Archivos = new List<PQRSArchivos>();
                        lisComunicadosTemp[c].Archivos.Add(archivos);
                    }
                    lisComunicados.Add(lisComunicadosTemp[c]);
                }
            }
            #endregion

            #region ConsultarArchivosImplementacion
            parametros.Clear();
            parametros.Add("@pIdPQRS", idPQRS);
            parametros.Add("@pTipo", 2);
            List<PQRSArchivos> lisImplementacion = ControlDatos.EjecutarStoreProcedureConParametros<PQRSArchivos>("USP_fup_SEL_PQRS_ArchivosRadicado", parametros);
            foreach (PQRSArchivos archivoRadicado in lisImplementacion)
            {
                archivoRadicado.CanBeDeleted = false;
                if (archivoRadicado.UsuarioArchivo == (string)HttpContext.Current.Session["Usuario"])
                {
                    archivoRadicado.CanBeDeleted = true;
                }
            }
            #endregion

            #region ConsultarArchivosCierre
            parametros.Clear();
            parametros.Add("@pIdPQRS", idPQRS);
            parametros.Add("@pTipo", 3);
            List<PQRSArchivos> lisCierre = ControlDatos.EjecutarStoreProcedureConParametros<PQRSArchivos>("USP_fup_SEL_PQRS_ArchivosRadicado", parametros);
            foreach (PQRSArchivos archivoRadicado in lisCierre)
            {
                archivoRadicado.CanBeDeleted = false;
                if (archivoRadicado.UsuarioArchivo == (string)HttpContext.Current.Session["Usuario"])
                {
                    archivoRadicado.CanBeDeleted = true;
                }
            }
            #endregion

            #region ConsultarListados
            parametros.Clear();
            parametros.Add("@pIdPQRS", idPQRS);
            parametros.Add("@pTipo", 4);
            List<PQRSArchivosListados> lisListados = ControlDatos.EjecutarStoreProcedureConParametros<PQRSArchivosListados>("USP_fup_SEL_PQRS_ArchivosRadicado", parametros);
            foreach (PQRSArchivos archivoRadicado in lisListados)
            {
                archivoRadicado.CanBeDeleted = false;
                if (archivoRadicado.UsuarioArchivo == (string)HttpContext.Current.Session["Usuario"])
                {
                    archivoRadicado.CanBeDeleted = true;
                }
            }
            #endregion

            #region ConsultarPlanos
            parametros.Clear();
            parametros.Add("@pIdPQRS", idPQRS);
            parametros.Add("@pTipo", 6);
            List<PQRSArchivosListados> lisPlanos = ControlDatos.EjecutarStoreProcedureConParametros<PQRSArchivosListados>("USP_fup_SEL_PQRS_ArchivosRadicado", parametros);
            foreach (PQRSArchivos archivoRadicado in lisPlanos)
            {
                archivoRadicado.CanBeDeleted = false;
                if (archivoRadicado.UsuarioArchivo == (string)HttpContext.Current.Session["Usuario"])
                {
                    archivoRadicado.CanBeDeleted = true;
                }
            }
            #endregion

            #region ConsultarArmado
            parametros.Clear();
            parametros.Add("@pIdPQRS", idPQRS);
            parametros.Add("@pTipo", 7);
            List<PQRSArchivosListados> lisArmado = ControlDatos.EjecutarStoreProcedureConParametros<PQRSArchivosListados>("USP_fup_SEL_PQRS_ArchivosRadicado", parametros);
            foreach (PQRSArchivos archivoRadicado in lisArmado)
            {
                archivoRadicado.CanBeDeleted = false;
                if (archivoRadicado.UsuarioArchivo == (string)HttpContext.Current.Session["Usuario"])
                {
                    archivoRadicado.CanBeDeleted = true;
                }
            }
            #endregion

            #region ConsultarNoConformidades
            parametros.Clear();
            parametros.Add("@pIdPQRS", idPQRS);
            List<PQRSNoConformidadesResumen> lisNoConformidades = ControlDatos.EjecutarStoreProcedureConParametros<PQRSNoConformidadesResumen>("USP_fup_SEL_PQRS_NoConformidades", parametros);
            #endregion

            #region ConsultarProductosGarantias
            List<string> idsNoConformidades = lisNoConformidades.Select(x => x.PQRSNoConformidadesID.ToString()).ToList();
            parametros.Clear();
            parametros.Add("@idsNoConformidades", string.Join(",", idsNoConformidades));
            List<PQRSNoConformidadesProductosGarantia> lisProductosGarantia = ControlDatos.EjecutarStoreProcedureConParametros<PQRSNoConformidadesProductosGarantia>("USP_fup_SEL_PQRSNoConformidadesProductos", parametros);

            lisNoConformidades.ForEach(noConformidad =>
            {
                noConformidad.ProductosGarantias = lisProductosGarantia.Where(x => x.NoConformidadId ==
                    noConformidad.PQRSNoConformidadesID).ToList();
            });
            #endregion

            #region ConsultarArchivosProduccion
            parametros.Clear();
            parametros.Add("@pIdPQRS", idPQRS);
            parametros.Add("@pTipo", 5);
            List<PQRSArchivosListados> lisProduccion = ControlDatos.EjecutarStoreProcedureConParametros<PQRSArchivosListados>("USP_fup_SEL_PQRS_ArchivosRadicado", parametros);
            foreach (PQRSArchivos archivoRadicado in lisProduccion)
            {
                archivoRadicado.CanBeDeleted = false;
                if (archivoRadicado.UsuarioArchivo == (string)HttpContext.Current.Session["Usuario"])
                {
                    archivoRadicado.CanBeDeleted = true;
                }
            }
            #endregion

            #region ConsultarControlCambiosCierre
            parametros.Clear();
            parametros.Add("@pIdPQRS", idPQRS);
            List<ControlCambiosCierre_ConsultarPQRS> lisControlCambiosCierre = ControlDatos.EjecutarStoreProcedureConParametros<ControlCambiosCierre_ConsultarPQRS>("USP_fup_SEL_PQRSControlCambiosCierre", parametros);
            #endregion

            parametros.Clear();
            parametros.Add("lisRadicado", lisRadicado);
            parametros.Add("lisAsignacionProcesos", lisAsignacionProcesos);
            parametros.Add("lisRespuestaProcesos", newLisRespuestasProcesos);
            parametros.Add("lisNoConformidades", lisNoConformidades);
            parametros.Add("lisImplementacionObra", lisImplementacion);
            parametros.Add("lisCierreObra", lisCierre);
            parametros.Add("lisListados", lisListados);
            parametros.Add("lisPlanos", lisPlanos);
            parametros.Add("lisArmado", lisArmado);
            parametros.Add("lisComunicados", lisComunicados);
            parametros.Add("lisProduccion", lisProduccion);
            parametros.Add("lisControlCambiosCierre", lisControlCambiosCierre);

            string response = JsonConvert.SerializeObject(parametros);
            return response;
        }

        [WebMethod]
        public static void ActualizarNoConformidad(PQRSNoConformidadesResumen noConformidad)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pNoConformidadId", noConformidad.PQRSNoConformidadesID);
            parametros.Add("@pEmail", noConformidad.Email);
            parametros.Add("@pComentario", noConformidad.Comentario);
            parametros.Add("@pProceso", noConformidad.Proceso);
            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_NoConformidades", parametros);
        }

        [WebMethod]
        public static string ObtenerBitacoraEventos()
        {
            string response = "";
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            List<PQRSBitacoraEventos> lst = ControlDatos.EjecutarStoreProcedureConParametros<PQRSBitacoraEventos>("USP_fup_SEL_PQRSBitacoraEventos", parametros);
            response = JsonConvert.SerializeObject(lst);
            return response;
        }

        private static PQRSRespuestaProcesos BuscarPadresRecursivo(List<PQRSRespuestaProcesos> respuestas, PQRSRespuestaProcesos respuesta,
            List<PQRSRespuestaProcesos> respuestasTemp)
        {
            if (respuestasTemp == null)
            {
                respuestasTemp = new List<PQRSRespuestaProcesos>();
            }
            if (respuesta.IdPadre == null)
            {
                respuestasTemp.Reverse();
                respuesta.hijos = new List<PQRSRespuestaProcesos>(respuestasTemp);
                return respuesta;
            } else
            {
                respuestasTemp.Add(respuesta);
            }
            for (int c = 0; c < respuestas.Count; c++)
            {
                if (respuestas[c].Id == respuesta.IdPadre)
                {
                    PQRSRespuestaProcesos respuestaTemp = new PQRSRespuestaProcesos();
                    respuestaTemp = respuestas[c];
                    respuestas.Remove(respuestas[c]);
                    return BuscarPadresRecursivo(respuestas, respuestaTemp, respuestasTemp);
                }
            }
            return null;
        }

        [WebMethod]
        public static string ObtenerHallazgos(int idpqrs, string orden)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            FUPResumen fup = controlPQRS.obtenerFUPporOrdenFabricacion(orden);
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            #region ConsultarHallazgosObra
            parametros.Clear();
            parametros.Add("@pFupID", fup.IdFup);
            parametros.Add("@pVersion", fup.Version);
            parametros.Add("@pTipo", 3);
            List<fup_considerationobservation_consulta> lisHallazgosObra = ControlDatos.EjecutarStoreProcedureConParametros<fup_considerationobservation_consulta>("USP_fup_SEL_hvp_Observaciones", parametros);
            lisHallazgosObra = lisHallazgosObra.Where(x => x.IdPQRS == idpqrs).ToList();
            #endregion
            return JsonConvert.SerializeObject(lisHallazgosObra);
        }

        [WebMethod]
        public static bool GuardarProcesos(int idPQRS, List<PQRSAsignacionProcesos> procesos)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            List<PQRSProceso> procesosToEmail = new List<PQRSProceso>();

            if (procesos.Count() > 0)
            {
                foreach (PQRSAsignacionProcesos proceso in procesos)
                {
                    parametros.Clear();
                    parametros.Add("@pIdPQRS", idPQRS);
                    parametros.Add("@pProceso", proceso.Proceso);
                    parametros.Add("@pEmail", proceso.Email);
                    parametros.Add("@pObservacion", proceso.Observacion);
                    List<RespuestaFilasAfectadas> affectedRows = ControlDatos.EjecutarStoreProcedureConParametros<RespuestaFilasAfectadas>("USP_fup_UPD_PQRSAsignacionProcesos", parametros);
                    PQRSProceso tempProcessEmail = new PQRSProceso();
                    tempProcessEmail.EmailProceso = proceso.Email;
                    tempProcessEmail.Proceso = proceso.Proceso;
                    tempProcessEmail.EmailProcesoCC = proceso.EmailProcesoCC;
                    procesosToEmail.Add(tempProcessEmail);
                }

                string correoSistema = (string)HttpContext.Current.Session["CorreoSistema"];
                FormPQRSConsulta.SendEmailsProcesosAsignados(procesosToEmail, correoSistema, idPQRS);
                return true;
            }
            return false;
        }

        [WebMethod]
        public static void DeleteProcessAssigned(int idAssignedProcess, int pqrsId)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pIdAssignedProcess", idAssignedProcess);
            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_DEL_PQRSProcesos", parametros);
            VerifyResponsesAndProcessAmounts(pqrsId);
        }

        private static void VerifyResponsesAndProcessAmounts(int pqrsId)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            controlPQRS.VerifyResponsesAndProcessAmounts(pqrsId);
        }

        [WebMethod]
        public static void ModifyDeliveryDate(int pqrsId, DateTime deliveryDate)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            controlPQRS.ModifyDeliveryDate(pqrsId, deliveryDate);
        }

        [WebMethod]
        public static string ObtenerPosiblesEstados()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            List<PQRSPosiblesEstados> data = ControlDatos.EjecutarStoreProcedureConParametros<PQRSPosiblesEstados>("USP_fup_SEL_PQRSEstados", parametros);
            string response = JsonConvert.SerializeObject(data);
            return response;
        }

        public static List<PQRSPosiblesEstados> ObtenerPosiblesEstadosForConsulta()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            List<PQRSPosiblesEstados> data = ControlDatos.EjecutarStoreProcedureConParametros<PQRSPosiblesEstados>("USP_fup_SEL_PQRSEstados", parametros);
            return data;
        }

        [WebMethod]
        public static void ActualizarEstado(int IdPQRS, int IdNuevoEstado)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pIdPQRS", IdPQRS);
            parametros.Add("@pIdNuevoEstado", IdNuevoEstado);
            parametros.Add("@pUsuario", (string)HttpContext.Current.Session["Usuario"]);
            List<RespuestaFilasAfectadas> data = ControlDatos.EjecutarStoreProcedureConParametros<RespuestaFilasAfectadas>("USP_fup_UPD_PQRSActualizarEstado", parametros);
        }

        [WebMethod]
        public static string ObtenerRolUsuario()
        {
            int rol = (int)HttpContext.Current.Session["Rol"];
            string response = JsonConvert.SerializeObject(new Dictionary<string, int> { { "rol", rol } });
            return response;
        }

        [WebMethod(EnableSession = true)]
        public static void ProcesarCorreosSolicitudInformacionProcesos(string correos, string mensajeAdd, int idPQRS, string proceso, string nombreCliente, int idRespuesta, int idProceso)
        {
            try
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@pIdPQRS", idPQRS);
                parametros.Add("@pProceso", proceso);
                parametros.Add("@pEmailsProceso", correos);
                parametros.Add("@pIdRespuesta", idRespuesta);
                parametros.Add("@pUsuario", (string)HttpContext.Current.Session["Usuario"]);
                parametros.Add("@pAclaracion", mensajeAdd);
                parametros.Add("@pPQRSProcesoIdPadre", idProceso);
                HttpContext.Current.Server.MapPath("~");
                String rutaAplicacion = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath);//Toma la ruta del aplicativo
                rutaAplicacion = rutaAplicacion.Replace("\\\\", "\\");

                List<RespuestaFilasAfectadas> affectedRows = ControlDatos.EjecutarStoreProcedureConParametros<RespuestaFilasAfectadas>("USP_fup_UPD_PQRSProcesos", parametros);

                List<string> correosParticionados = correos.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                string asunto = "Solicitud mayor informacion PQRS - Id: " + idPQRS.ToString() + " Cliente " + nombreCliente + " : " + DateTime.Today.ToString("yyyy-MM-dd");
                string mensaje = "Estimado integrante, se le ha solicitado complementar la información de una PQRS, por favor consulte el módulo para su respectiva respuesta." 
                            +"<br><a href='http://app.forsa.com.co/siomaestros/FormPQRSConsulta.aspx?IdPQRS=" + idPQRS.ToString() + "'>  Ir PQRS  </a><br>"
                            + "<br><a href='http://app.forsa.com.co/siomaestros/FormPQRSResumen.aspx?IdPQRS=" + idPQRS.ToString() + "'>Ir a Resumen PQRS</a><br>";
                mensaje += "<br>";
                string correoSistema = (string)HttpContext.Current.Session["CorreoSistema"];
                List<string> archivos = HttpContext.Current.Session["solicitudInformacionArchivosGuardados"].ToString().Split(';').ToList();
                Task taskA = Task.Run(() =>
                {
                    foreach (string correo in correosParticionados)
                    {
                        EnviarCorreos(correoSistema, correo, asunto, mensaje, archivos, rutaAplicacion);
                    }
                });
                HttpContext.Current.Session["solicitudInformacionArchivosGuardados"] = "";
            }
            catch { }
        }

        [WebMethod(EnableSession = true)]
        public static void EnviarCorreos(string correoSistema, string destinatarioEmail, string asuntoMail, string mensaje, List<string> archivos, String rutaAplicacion)
        {
            List<PQRSFilesDTO> adjuntos = new List<PQRSFilesDTO>();
            FormPQRSConsulta.SendMail(correoSistema, destinatarioEmail, "", asuntoMail, mensaje, rutaAplicacion);
        }

        [WebMethod]
        public static void GuardarCierreReclamacion(PQRSNoConformidadesResumen noConformidadEdit, string productosGarantia, string textoOtro)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pIdNoConformidad", noConformidadEdit.PQRSNoConformidadesID);
            parametros.Add("@pFechaCierre", noConformidadEdit.PlanAccionFecha);
            parametros.Add("@pPlanAccion", noConformidadEdit.PlanAccion);
            parametros.Add("@pDescripcionPlanAccion", noConformidadEdit.PlanAccionDescripcion);
            parametros.Add("@pIdClasificacion", noConformidadEdit.IdClasificacion);
            parametros.Add("@pIdFamiliaGarantia", noConformidadEdit.IdFamiliaGarantia);
            parametros.Add("@pUsuarioResponsable", noConformidadEdit.UsuarioResponsable);
            parametros.Add("@pProductosGarantia", productosGarantia);
            parametros.Add("@pTextoOtro", textoOtro);
            parametros.Add("@pUsuario", (string)HttpContext.Current.Session["Usuario"]);
            List<RespuestaFilasAfectadas> data = ControlDatos.EjecutarStoreProcedureConParametros<RespuestaFilasAfectadas>("USP_fup_UPD_CierreReclamacion", parametros);
        }

        [WebMethod]
        public static void CrearPlanAccionQueja(PQRSNoConformidadesResumen noConformidadEdit, 
            string productosGarantia, string textoOtro, int pqrsId)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pFechaCierre", noConformidadEdit.PlanAccionFecha);
            parametros.Add("@pPlanAccion", noConformidadEdit.PlanAccion);
            parametros.Add("@pDescripcionPlanAccion", noConformidadEdit.PlanAccionDescripcion);
            parametros.Add("@pIdClasificacion", noConformidadEdit.IdClasificacion);
            parametros.Add("@pIdFamiliaGarantia", noConformidadEdit.IdFamiliaGarantia);
            parametros.Add("@pUsuarioResponsable", noConformidadEdit.UsuarioResponsable);
            parametros.Add("@pUsuario", (string)HttpContext.Current.Session["Usuario"]);
            parametros.Add("@pProductosGarantia", productosGarantia);
            parametros.Add("@pTextoOtro", textoOtro);
            parametros.Add("@pEmail", noConformidadEdit.Email);
            parametros.Add("@PQRSId", pqrsId);
            parametros.Add("@pProceso", "Procesos Forsa");
            parametros.Add("@pPncCodID", 124);
            parametros.Add("@pComentario", noConformidadEdit.Comentario);
            List<RespuestaFilasAfectadas> data = ControlDatos.EjecutarStoreProcedureConParametros<RespuestaFilasAfectadas>("USP_fup_INS_CierreReclamacion", parametros);
        }

        [WebMethod]
        public static string AdicionarRespuestaProceso(int idPQRS, int idProceso, string respuesta)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pIdPQRS", idPQRS);
            parametros.Add("@pIdProceso", idProceso);
            parametros.Add("@pRespuesta", respuesta);
            parametros.Add("@pUsuario", (string)HttpContext.Current.Session["Usuario"]);
            List<RespuestaFilasAfectadas> data = ControlDatos.EjecutarStoreProcedureConParametros<RespuestaFilasAfectadas>("USP_fup_UPD_PQRSRespuestaProceso", parametros);

            PQRSDTOConsulta pqrs = new PQRSDTOConsulta();
            pqrs.IdFup = 0;
            pqrs.Version = "";
            FormPQRSConsulta.SendEmailsGeneral(pqrs, 3, "");

            string response = JsonConvert.SerializeObject(data);
            return response;
        }

        [WebMethod]
        public static string EliminarArchivo(int PQRSId, string nameFile, string namedir, int idArchivo, int idTipoArchivo)
        {
            if (idTipoArchivo == 4 || idTipoArchivo == 7) {  // Se debe borrar el registro del que ya subio el plano o listado
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@pIdPQRS", PQRSId);
                parametros.Add("@pIdTipoArchivo", idTipoArchivo);
                parametros.Add("@pIdArchivo", idArchivo);
                parametros.Add("@pUsuario", (string)HttpContext.Current.Session["Usuario"]);
                List<RespuestaFilasAfectadas> data = ControlDatos.EjecutarStoreProcedureConParametros<RespuestaFilasAfectadas>("USP_fup_UPD_PQRSProcesoListadoPlano", parametros);

            }
            string response = ControlPQRS.DeleteFile(PQRSId, nameFile, namedir, idArchivo, idTipoArchivo);
            return JsonConvert.SerializeObject(response);
        }

        [WebMethod]
        public static void ActualizarFechasPlanProduccion(DateTime planAlum, int idPQRS)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pIdPQRS", idPQRS);
            parametros.Add("@pFechaPlanAlum", planAlum);
            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_PQRSFechasPlanProduccion", parametros);
        }

        [WebMethod(EnableSession = true)]
        public static string GuardarControlCambio(ControlCambiosCierre_Guardar_PQRS Item)
        {
            string response = string.Empty;
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];

            parametros.Add("@pIdPQRS", Item.IdPQRS);
            parametros.Add("@pUsuario", NombreUsu);
            parametros.Add("@pComentario", Item.Comentario);
            parametros.Add("@pEstado", Item.Estado);
            parametros.Add("@pCons", Item.cons);
            parametros.Add("@pPadre", Item.padre);
            parametros.Add("@pTitulo", Item.Titulo);
            parametros.Add("@pEventoId", Item.EventoId);
            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_PQRSControlCambiosCierre", parametros);

            return "OK";
        }

        [WebMethod(EnableSession = true)]
        public static string ObtenerControlCambio(int IdPQRS)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pIdPQRS", IdPQRS);
            List<ControlCambiosCierre_Consultar> lisControlCambios = ControlDatos.EjecutarStoreProcedureConParametros<ControlCambiosCierre_Consultar>("USP_fup_SEL_PQRSControlCambiosCierre", parametros);
            return JsonConvert.SerializeObject(lisControlCambios);
        }
    }
}