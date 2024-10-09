using CapaControl;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using CapaControl.Entity;
using Microsoft.Reporting.WebForms;
using System.Security.Cryptography.X509Certificates;
using System.Globalization;
using System.Threading;
using System.Text.RegularExpressions;

namespace SIO
{
    public partial class FormPrototypeReport : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scripManager = ScriptManager.GetCurrent(this.Page);
            scripManager.EnablePageMethods = true;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

            if (Request.QueryString["idCliente"] != null)
            {
                string idCliente = Request.QueryString["idCliente"];
                Session["idCliente"] = idCliente;
            }
            else
                Session["idCliente"] = -1;

            if (Request.QueryString["fup"] != null)
            {
                string IdFUP = Request.QueryString["fup"];
                Session["IdFUP"] = IdFUP;
            }
            else
                Session["IdFUP"] = -1;
        }

        /*[WebMethod]
        public static void loadReporteFup(string fupId, string version)
        {
            Boolean haypar = false;
            ReporteFUPv = new Microsoft.Reporting.WebForms.ReportViewer();
            ReporteFUPv.KeepSessionAlive = false;
            ReporteFUPv.AsyncRendering = true;
            ReporteFUPv.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            ReporteFUPv.ServerReport.ReportServerCredentials = irsc;

            ReporteFUPv.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");

            List<ReportParameter> parametro = new List<ReportParameter>();

            parametro.Add(new ReportParameter("pFupID", fupId, true));
            haypar = true;
            parametro.Add(new ReportParameter("pVersion", version, true));
            haypar = true;

            ReporteFUPv.PromptAreaCollapsed = true;
            ReporteFUPv.ServerReport.ReportPath = "/Comercial/FUP_Impreso";

            if (haypar)
            {
                ReporteFUPv.ServerReport.SetParameters(parametro);
                ReporteFUPv.ServerReport.Refresh();
            }
        }*/

        [WebMethod]
        public static string search()
        {
            int proyecto = 1;
            ControlFUP controlfup = new ControlFUP();
            SqlDataReader reader = controlfup.PoblarTipoVentaProyecto(proyecto);
            return "worked";
        }

        [WebMethod(EnableSession = true)]
        public static string obtenerListadoCiudadesMoneda(string idPais)
        {
            ControlUbicacion controlUbicacion = new ControlUbicacion();
            Dictionary<string, object> queryResult = new Dictionary<string, object>();

            string representanteComercialId = (string)HttpContext.Current.Session["rcID"];
            int rol = (int)HttpContext.Current.Session["Rol"];
            int vPais = (int)Convert.ToInt32(idPais);

            List<CapaControl.Entity.Ciudad> lstCiudad = null;

            if (((rol == 3) || (rol == 30) || (rol == 34) || (rol == 40)) && ((vPais == 8) || (vPais == 6) || (vPais == 21)))
            {
                lstCiudad = controlUbicacion.obtenerCiudadesRepresentantesColombia(int.Parse(representanteComercialId));
            }
            else
            {
                lstCiudad = controlUbicacion.obtenerListaCiudades(int.Parse(idPais));
            }

            queryResult.Add("varCiudad", lstCiudad);
            string response = JsonConvert.SerializeObject(queryResult);
            return response;
        }

        [WebMethod]
        public static string obtenerListadoClientesCiudad(string idCiudad)
        {
            string IdClienteUsuario = "0";

            if (HttpContext.Current.Session["IdClienteUsuario"] != null)
            {
                IdClienteUsuario = HttpContext.Current.Session["IdClienteUsuario"].ToString();
            }

            ControlCliente control = new ControlCliente();
            Dictionary<string, object> queryResult = new Dictionary<string, object>();
            List<CapaControl.Entity.Cliente> lstObject = control.obtenerDatosCliente(int.Parse(idCiudad), int.Parse(IdClienteUsuario));
            queryResult.Add("varCliente", lstObject);
            string response = JsonConvert.SerializeObject(queryResult);
            return response;
        }

        [WebMethod]
        public static string obtenerListadoContactos_Obras_porCliente(string idCliente)
        {
            ControlCliente controlCliente = new ControlCliente();
            ControlContacto controlContacto = new ControlContacto();
            Dictionary<string, object> queryResult = new Dictionary<string, object>();
            List<CapaControl.Entity.ContactoCliente> lstContactoCliente = controlCliente.obtenerContactoCliente(int.Parse(idCliente));
            List<CapaControl.Entity.ObraCliente> lstObraCliente = controlContacto.obtenerObrasDistPV(int.Parse(idCliente));
            queryResult.Add("varContacto", lstContactoCliente);
            queryResult.Add("varObra", lstObraCliente);
            string response = JsonConvert.SerializeObject(queryResult);
            return response;
        }

        [WebMethod]
        public static string obtenerParametrosDatosGenerales()
        {
            ControlObra controlObra = new ControlObra();
            ControlFUP controlFup = new ControlFUP();
            ControlUbicacion controlUbicacion = new ControlUbicacion();
            Dictionary<string, object> queryResult = new Dictionary<string, object>();
            /*List<CapaControl.Entity.EstadoSocioEconomico> lstEstrato = controlObra.obtenerEstadoSocioEconomico();
            List<Dominios> lstTipVivienda = ControlDominios.obtener_dominios(CapaControl.Enumeradores.Dominios.TIPO_VIVIENDA.ToString());
            List<ClaseCotizacion> lstClaseCotizacion = controlFup.obtenerClaseCotizacion();
            List<Dominios> lstVoBo = ControlDominios.obtener_dominios(CapaControl.Enumeradores.Dominios.APROBACION.ToString());
            List<Dominios> lstMotivoRechazo = controlFup.obtenerMotivoRechazoFUP();
            List<Dominios> lstTipoRecotizacion = controlFup.obtenerTipoRecotizacionFUP();
            //List<Dominios> lstEventoPFT = controlFup.obtenerEventoPlanoTipoForsa();
            List<datosCombo2> lstEventoPFT = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT [tes_id] id,[tes_descripcion] descripcion, [tes_descripcion] descripcionEN, 
                                            [tes_descripcion] descripcionPO 
                                            FROM [fup_evento_segpf] ORDER BY [tes_orden]", new Dictionary<string, object>());
            List<datosCombo2> listaPlanptf = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT        fdom_CodDominio as id, fdom_Descripcion as descripcion , fdom_DescripcionEN descripcionEN , fdom_DescripcionPO descripcionPO
																						FROM            fup_Dominios
																						WHERE        (fdom_Dominio = 'PTF_PLANOS')
																						ORDER BY fdom_OrdenDominio", new Dictionary<string, object>());

            List<Dominios> lstResponsablePTF = controlFup.obtenerResponsablePlanoTipoForsa();
            List<datosCombo2> lstEquipos = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT        fdom_CodDominio as id, fdom_Descripcion as descripcion, fdom_DescripcionEN descripcionEN , fdom_DescripcionPO descripcionPO
																						FROM            fup_Dominios
																						WHERE        (fdom_Dominio = 'TIPO_EQUIPO')
																						ORDER BY fdom_OrdenDominio", new Dictionary<string, object>());

            List<datosCombo2> lstDevCom = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT [tre_id] id, [tre_descripcion] descripcion,  [tre_descripcion] descripcionEN, [tre_descripcion] descripcionPO  " +
                                                                                        "FROM fup_tipo_rechazo where tre_tipo = 9", new Dictionary<string, object>());
            List<CapaControl.Entity.Moneda> lstMoneda = controlUbicacion.obtenerMoneda();
            List<datosCombo2> listaEstadoDtf = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT        fdom_CodDominio as id, fdom_Descripcion as descripcion , fdom_DescripcionEN descripcionEN , fdom_DescripcionPO descripcionPO
																						FROM            fup_Dominios
																						WHERE        (fdom_Dominio = 'ESTADO_DFT')
																						ORDER BY fdom_OrdenDominio", new Dictionary<string, object>());
            // Este fragmento de código verifica si el rol del usuario loggeado es un rol comercial
            // De ser así sólo dejará las opciones de la lista correspondientes
            int rol = (int)HttpContext.Current.Session["Rol"];
            if (
                rol == 3 || rol == 9 || rol == 24 || rol == 31 || rol == 40 || rol == 54
                )
            {
                listaEstadoDtf = listaEstadoDtf.Where(x => (x.descripcion == "Pend SCI" || x.descripcion == "Solicitud Aval Tecnico")).ToList();
            }

            List<datosCombo2> listaSubprDtf = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT        fdom_CodDominio as id, fdom_Descripcion as descripcion , fdom_DescripcionEN descripcionEN , fdom_DescripcionPO descripcionPO
																						FROM            fup_Dominios
																						WHERE        (fdom_Dominio = 'SUBPROCESODFT')
																						ORDER BY fdom_OrdenDominio", new Dictionary<string, object>());

            queryResult.Add("varMoneda", lstMoneda);
            queryResult.Add("varEstrato", lstEstrato);
            queryResult.Add("varTipoVivienda", lstTipVivienda);
            queryResult.Add("varClaseCotizacion", lstClaseCotizacion);
            queryResult.Add("varVoBoFup", lstVoBo);
            queryResult.Add("varMotivoRechazoFUP", lstMotivoRechazo);
            queryResult.Add("varTipoRecotizacionFUP", lstTipoRecotizacion);
            queryResult.Add("varEventoPFT", lstEventoPFT);
            queryResult.Add("varPlanPFT", listaPlanptf);
            queryResult.Add("varResponsablePTF", lstResponsablePTF);
            queryResult.Add("varTipoEquipo", lstEquipos);
            queryResult.Add("varDevComer", lstDevCom);
            queryResult.Add("varEstadoDFT", listaEstadoDtf);
            queryResult.Add("varSubpDFT", listaSubprDtf);*/

            string response = JsonConvert.SerializeObject(queryResult);
            return response;
        }

        [WebMethod]
        public static string obtenerDatosObra(string idObra)
        {
            ControlObra control = new ControlObra();
            ControlFUP controlFup = new ControlFUP();

            Dictionary<string, object> queryResult = new Dictionary<string, object>();

            CapaControl.Entity.ObraInformacion obraInfo = control.obtenerDatosObra(int.Parse(idObra));
            List<datosCombo2> ptoOrigen = controlFup.CargarPuerto(8);
            List<datosCombo2> ptoDestino = controlFup.CargarPuerto(obraInfo.IdPais);

            queryResult.Add("obraInfo", obraInfo);
            queryResult.Add("ptoOrigen", ptoOrigen);
            queryResult.Add("ptoDestino", ptoDestino);

            string response = JsonConvert.SerializeObject(queryResult);
            return response;
        }

        [WebMethod]
        public static string ObtenerLineasDinamicas(ObtenerItemDinamico item)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pFupID", item.pFupID);
            parametros.Add("@pVersion", item.pVersion);
            parametros.Add("@Idioma", item.Idioma);

            List<ItemDinamico> data = ControlDatos.EjecutarStoreProcedureConParametros<ItemDinamico>("USP_fup_SEL_Alcance_ParteSoloConsumiblesActivos", parametros).ToList();

            /*data.Where(x => !string.IsNullOrEmpty(x.DomLista)).ToList().ForEach(x =>
                x.dominio = ControlDominios.obtener_dominios(x.DomLista)
            );*/

            string response = JsonConvert.SerializeObject(data);
            return response;
        }

        [WebMethod]
        public static string ObtenerEquipos(int pFupID, string pVersion)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pFupID", pFupID);
            parametros.Add("@pVersion", pVersion);

            List<ItemEquipo> data = ControlDatos.EjecutarStoreProcedureConParametros<ItemEquipo>("USP_fup_SEL_Alcance_Equipos", parametros).OrderBy(x => x.Consecutivo).ToList();

            string response = JsonConvert.SerializeObject(data);
            return response;
        }

        [WebMethod]
        public static string ObtenerAdaptacion(int pFupID, string pVersion)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pFupID", pFupID);
            parametros.Add("@pVersion", pVersion);

            List<ItemEquipo> data = ControlDatos.EjecutarStoreProcedureConParametros<ItemEquipo>("USP_fup_SEL_Alcance_Adaptacion", parametros).OrderBy(x => x.Consecutivo).ToList();

            string response = JsonConvert.SerializeObject(data);
            return response;
        }

        [WebMethod]
        public static string GuardarLineasDinamicas(List<ItemDinamicoGuardar> listaItem)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            int id = 0;

            foreach (ItemDinamicoGuardar item in listaItem)
            {
                parametros.Clear();
                parametros.Add("@pFupID", item.pFupID);
                parametros.Add("@pVersion", item.pVersion);
                parametros.Add("@pItemparte_id", item.pItemparte_id);
                parametros.Add("@pItemSiNo", item.pItemSiNo ?? false);
                parametros.Add("@pItemTextoLista", item.pItemTextoLista ?? "");
                parametros.Add("@pAdicionalSiNo", item.pAdicionalSiNo ?? false);
                parametros.Add("@pAdicionalCantidad", item.pAdicionalCantidad ?? 0);
                parametros.Add("@Descripcion", item.Descripcion ?? "");

                id = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_Alcance_Partes", parametros);
            }
            string response = JsonConvert.SerializeObject(id.ToString());
            return response;
        }

        [WebMethod]
        public static void GuardarEquiposAdaptacion(List<ItemEquipo> listaItem)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            int id;
            int paso = 0;

            foreach (ItemEquipo item in listaItem)
            {
                if (paso == 0)
                {
                    parametros.Clear();
                    parametros.Add("@pFupID", item.pFupID);
                    parametros.Add("@pVersion", item.pVersion);
                    ControlDatos.GuardarStoreProcedureConParametros("USP_fup_DEL_Alcance_Equipos", parametros);
                    ControlDatos.GuardarStoreProcedureConParametros("USP_fup_DEL_Alcance_Adaptacion", parametros);
                }
                paso += 1;

                parametros.Clear();
                if (item.TipoEquipo == 0 && item.Cantidad == 0)
                {
                    parametros.Add("@pFupID", item.pFupID);
                    parametros.Add("@pVersion", item.pVersion);
                    parametros.Add("@ConsecutivoPadre", item.Consecutivo ?? 0);
                    parametros.Add("@Descripcion", item.Descripcion ?? "");
                    id = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_Alcance_Adaptacion", parametros);
                }
                else if (item.Consecutivo > 0 && item.Cantidad != 0)
                {
                    parametros.Add("@pFupID", item.pFupID);
                    parametros.Add("@pVersion", item.pVersion);
                    parametros.Add("@TipoEquipo", item.TipoEquipo);
                    parametros.Add("@Consecutivo", item.Consecutivo ?? 0);
                    parametros.Add("@Cantidad", item.Cantidad ?? 0);
                    parametros.Add("@Descripcion", item.Descripcion ?? "");
                    id = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_Alcance_Equipos", parametros);
                }
            }
        }


        [WebMethod]
        public static void EliminarEquipos(ItemEquipo item)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            parametros.Add("@pFupID", item.pFupID);
            parametros.Add("@pVersion", item.pVersion);

            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_DEL_Alcance_Equipos", parametros);
            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_DEL_Alcance_Adaptacion", parametros);

        }

        [WebMethod]
        public static string GuardarEquipos(ItemEquipo item)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            parametros.Add("@pFupID", item.pFupID);
            parametros.Add("@pVersion", item.pVersion);
            parametros.Add("@TipoEquipo", item.TipoEquipo);
            parametros.Add("@Consecutivo", item.Consecutivo ?? 0);
            parametros.Add("@Cantidad", item.Cantidad ?? 0);
            parametros.Add("@Descripcion", item.Descripcion ?? "");
            int id = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_Alcance_Equipos", parametros);

            string response = JsonConvert.SerializeObject(id.ToString());
            return response;
        }

        [WebMethod]
        public static string GuardarAdaptacion(ItemEquipo item)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            parametros.Add("@pFupID", item.pFupID);
            parametros.Add("@pVersion", item.pVersion);
            parametros.Add("@ConsecutivoPadre", item.Consecutivo ?? 0);
            parametros.Add("@Descripcion", item.Descripcion ?? "");

            int id = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_Alcance_Adaptacion", parametros);

            string response = JsonConvert.SerializeObject(id.ToString());
            return response;
        }

        [WebMethod]
        public static string GuardaOrdenCliente(int pFupId, string pidVersion, List<fup_tablas2> ListaOrdenes)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            parametros.Add("@pFupID", pFupId);
            parametros.Add("@pVersion", pidVersion);
            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_DEL_enc_ent_OrdenCliente", parametros);

            foreach (fup_tablas2 tabla in ListaOrdenes)
            {
                parametros.Clear();
                parametros.Add("@pFupID", pFupId);
                parametros.Add("@pVersion", pidVersion);
                parametros.Add("@Consecutivo", tabla.consecutivo);
                parametros.Add("@OrdenCliente", tabla.valor);
                parametros.Add("@Comentario", tabla.Comentario);
                ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_enc_ent_OrdenCliente", parametros);
            }

            string response = JsonConvert.SerializeObject(pFupId);
            return response;
        }

        [WebMethod(enableSession: true)]
        public static string GuardarFUP(fup_guardar fup)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string idVersion = "A", response = string.Empty;
            idVersion = fup.Version;
            string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];

            parametros.Add("@pUsuario", NombreUsu);
            parametros.Add("@pFupAnterior", fup.FupAnterior);
            parametros.Add("@pTipoNegociacion", fup.TipoNegociacion);
            parametros.Add("@pTipoCotizacion", fup.TipoCotizacion);
            parametros.Add("@pProducto", fup.Producto);
            parametros.Add("@pTipoVaciado", fup.TipoVaciado);
            parametros.Add("@pSistemaSeguridad", fup.SistemaSeguridad);
            parametros.Add("@pNumeroEquipos", fup.NumeroEquipos);
            parametros.Add("@pAlturaLibre", fup.AlturaLibre);
            parametros.Add("@pAlineacionVertical", fup.AlineacionVertical);
            parametros.Add("@pFormaConstructiva", fup.FormaConstructiva);
            parametros.Add("@pDistanciaEdifica", fup.DistanciaEdifica);
            parametros.Add("@pDilatacionMuro", fup.DilatacionMuro);
            parametros.Add("@pEspesorJunta", fup.EspesorJunta);
            parametros.Add("@pDesnivel", fup.Desnivel);
            parametros.Add("@pCantMaxPisos", fup.MaxPisos);
            parametros.Add("@pCantFundiciones", fup.FundicionPisos);
            parametros.Add("@pAlturaIntSugerida", fup.AlturaIntSugerida);
            parametros.Add("@pTipoFachada", fup.TipoFachada);
            parametros.Add("@pAlturaUnion", fup.AlturaUnion);
            parametros.Add("@pTipoUnion", fup.TipoUnion);
            parametros.Add("@pDetalleUnion", fup.DetalleUnion);
            parametros.Add("@pAlturaCAP1", fup.AlturaCAP1);
            //parametros.Add("@pAlturaCAP2", string.IsNullOrWhiteSpace(fup.AlturaCAP2) ? string.Empty : fup.AlturaCAP2);
            parametros.Add("@pAlturaCAP2", fup.AlturaCAP2 ?? "");
            parametros.Add("@pAlturaLibreMinima", fup.AlturaLibreMinima ?? "");
            parametros.Add("@pAlturaLibreMaxima", fup.AlturaLibreMaxima ?? "");
            parametros.Add("@pAlturaLibreCual", fup.AlturaLibreCual ?? "");
            parametros.Add("@pReqCliente", fup.ReqCliente);
            parametros.Add("@pAlturaIntSugeridaCliente", fup.AlturaIntSugeridaCliente ?? "");
            parametros.Add("@pTipoFachadaCliente", fup.TipoFachadaCliente ?? "");
            parametros.Add("@pAlturaUnionCliente", fup.AlturaUnionCliente ?? "");
            parametros.Add("@pTipoUnionCliente", fup.TipoUnionCliente ?? "");
            parametros.Add("@pDetalleUnionCliente", fup.DetalleUnionCliente ?? "");
            //parametros.Add("@pAlturaCAP1Cliente", string.IsNullOrWhiteSpace(fup.AlturaCAP1Cliente) ? string.Empty : fup.AlturaCAP1Cliente);
            parametros.Add("@pAlturaCAP1Cliente", fup.AlturaCAP1Cliente ?? "");
            //parametros.Add("@pAlturaCAP2Cliente", string.IsNullOrWhiteSpace(fup.AlturaCAP2Cliente) ? string.Empty : fup.AlturaCAP2Cliente);
            parametros.Add("@pAlturaCAP2Cliente", fup.AlturaCAP2Cliente ?? "");
            parametros.Add("@pClaseCotizacion", fup.ClaseCotizacion);
            parametros.Add("@pEstrato", fup.Estrato);
            parametros.Add("@pTipoVivienda", fup.TipoVivienda);
            parametros.Add("@pTotalUnidades", fup.TotalUnidadesConstruir);
            parametros.Add("@pTotalUndForsa", fup.TotalUnidadesConstruirForsa);
            parametros.Add("@pM2", fup.MetrosCuadradosVivienda);
            parametros.Add("@pTerminoNegociacion", fup.TerminoNegociacion);
            parametros.Add("@pOtros", fup.otros);
            parametros.Add("@pCapPernado", fup.CapPernado);
            parametros.Add("@pFecSolicitaCliente", string.IsNullOrEmpty(fup.FecSolicitaCliente) ? (DateTime?)null : Convert.ToDateTime(fup.FecSolicitaCliente));
            parametros.Add("@pEquipoCopia", fup.EquipoCopia);
            parametros.Add("@pFupCopia", fup.FupCopia);

            int id = 0;

            if (fup.IdFUP > 0)
            {
                int rol = (int)HttpContext.Current.Session["Rol"];
                parametros.Add("@pFupID", fup.IdFUP);
                parametros.Add("@pVersion", fup.Version);
                parametros.Add("@rol", rol);
                ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_ent_cotizacion", parametros);
                id = fup.IdFUP;
            }
            else
            {
                parametros.Add("@pID_Cliente", fup.ID_Cliente);
                parametros.Add("@pID_Moneda", fup.ID_Moneda);
                parametros.Add("@pID_Contacto", fup.ID_Contacto);
                parametros.Add("@pID_Obra", fup.ID_Obra);
                id = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_INS_ent_cotizacion", parametros);
            }

            if (id > 0)
            {
                Dictionary<string, object> parametros_eliminar = new Dictionary<string, object>();
                parametros_eliminar.Add("@pFupID", id);
                parametros_eliminar.Add("@pVersion", idVersion);
                ControlDatos.GuardarStoreProcedureConParametros("USP_fup_DEL_enc_ent_cot_tabla", parametros_eliminar);

                foreach (fup_tablas tabla in fup.datos_tablas)
                {
                    Dictionary<string, object> parametros_tablas = new Dictionary<string, object>();
                    parametros_tablas.Add("@pFupID", id);
                    parametros_tablas.Add("@pVersion", idVersion);
                    parametros_tablas.Add("@tipoTabla", tabla.tipo_tabla);
                    parametros_tablas.Add("@Consecutivo", tabla.consecutivo);
                    parametros_tablas.Add("@Valor", tabla.valor);
                    ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_enc_ent_cot_tabla", parametros_tablas);
                }

                #region ConsultarInformacionGeneral
                parametros.Clear();
                parametros.Add("@pFupID", Convert.ToInt32(id));
                parametros.Add("@pVersion", idVersion);
                List<fup_consultar> dataInfoGeneral = ControlDatos.EjecutarStoreProcedureConParametros<fup_consultar>("USP_fup_SEL_ent_cotizacion", parametros);
                #endregion
                if (dataInfoGeneral.Count > 0)
                {
                    dataInfoGeneral.FirstOrDefault().IdFUP = id;
                    response = JsonConvert.SerializeObject(dataInfoGeneral.FirstOrDefault());
                }
            }

            return response;
        }

        [WebMethod(enableSession: true)]
        public static string obtenerVersionPorIdFup(string idFup)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            ControlFUP controlFup = new ControlFUP();
            string response = string.Empty;
            string representanteComercialId = (string)HttpContext.Current.Session["rcID"];
            int rol = (int)HttpContext.Current.Session["Rol"];
            int Consultar = -1;

            parametros.Add("@pFupID", Convert.ToInt32(idFup));
            parametros.Add("@pRepId", Convert.ToInt32(representanteComercialId));

            List<formato_unico> formatoUnico = ControlDatos.EjecutarStoreProcedureConParametros<formato_unico>("USP_fup_SEL_FUPporID", parametros);
            if (formatoUnico.Count > 0)
            {
                Consultar = Convert.ToInt32(idFup);
            }
            List<VersionFup> lstVersion = controlFup.obtenerVersionesFUP(Consultar);
            response = JsonConvert.SerializeObject(lstVersion);
            return response;
        }

        [WebMethod]
        public static string obtenerInformacionPorFupVersion(string idFup, string idVersion, string idioma)
        {
            string response = string.Empty, ordenFabricacion = string.Empty, ordenCotizacion = string.Empty;

            ControlFUP controlFup = new ControlFUP();
            ControlUbicacion controlUbicacion = new ControlUbicacion();
            ControlCliente control = new ControlCliente();
            ControlContacto controlContacto = new ControlContacto();
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            #region ConsultarInformacionGeneral
            parametros.Clear();
            parametros.Add("@pFupID", Convert.ToInt32(idFup));
            parametros.Add("@pVersion", idVersion);
            List<fup_consultar> dataInfoGeneral = ControlDatos.EjecutarStoreProcedureConParametros<fup_consultar>("USP_fup_SEL_ent_cotizacion", parametros);
            #endregion

            if (dataInfoGeneral.Count > 0)
            {
                #region ConsultarOrdenFabricacion
                ordenFabricacion = controlFup.obtenerOrdenFabricacionColombiaFUP(Convert.ToInt32(idFup), idVersion);
                ordenCotizacion = controlFup.obtenerOrdenCotizacionFUP(Convert.ToInt32(idFup), idVersion);
                #endregion

                #region ConsultarTablasInformacionGeneral
                parametros.Clear();
                parametros.Add("@pFupID", Convert.ToInt32(idFup));
                parametros.Add("@pVersion", idVersion);
                List<fup_tablas> dataTablas = ControlDatos.EjecutarStoreProcedureConParametros<fup_tablas>("USP_fup_SEL_enc_ent_cot_tabla", parametros);
                #endregion

                #region ConsultarParteOF
				parametros.Clear();
				parametros.Add("@pFupID", double.Parse(idFup));
				parametros.Add("@pVersion", idVersion);

				List<Orden_Fabricacion> dataOrdernes = ControlDatos.EjecutarStoreProcedureConParametros<Orden_Fabricacion>("USP_fup_SEL_OrdenesFab", parametros);
                #endregion

                #region ConsultarPQRSAsociados
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                List<PQRSFromOrdenBasicInfo> dataPQRSAsociados = ControlDatos.EjecutarStoreProcedureConParametros<PQRSFromOrdenBasicInfo>("USP_fup_SEL_PQRSFromFup", parametros);
                #endregion

                #region ConsultarPlaTecnicos
                parametros.Clear();
                parametros.Add("@pFupId", double.Parse(idFup));

                List<lista_planTecnico> dataPlanTec = ControlDatos.EjecutarStoreProcedureConParametros<lista_planTecnico>("USP_FUP_SEL_PlaneacionTecnico", parametros);
                #endregion

                #region ConsultarAnexoArmado
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@pTipoAnexo", -1);
                parametros.Add("@pGrupoSeg", 5);
                List<anexos> lisAnexosArm = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("USP_fup_SEL_Anexos", parametros);
                #endregion

                #region ConsultarAnexoListasEmpaque
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@pTipoAnexo", 32);
                parametros.Add("@pGrupoSeg", -1);
                List<anexos> lisAnexosListasEmpaque = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("USP_fup_SEL_Anexos", parametros);
                #endregion

                #region ConsultarAnexoActaDefinicionTecnica
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@pTipoAnexo", 33);
                parametros.Add("@pGrupoSeg", -1);
                List<anexos> lisAnexosActaDefinicionTecnica = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("USP_fup_SEL_Anexos", parametros);
                #endregion

                #region ConsultarAnexoMesaTecnica
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@pTipoAnexo", 36);
                parametros.Add("@pGrupoSeg", -1);
                List<anexos> lisAnexosMesaTecnica = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("USP_fup_SEL_Anexos", parametros);
                #endregion

                #region ConsultarAnexoFichaTecnica
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@pTipoAnexo", 37);
                parametros.Add("@pGrupoSeg", -1);
                List<anexos> lisAnexosFichaTecnica = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("USP_fup_SEL_Anexos", parametros);
                #endregion

                #region ConsultarAnexoMesaTecnicaPostventa
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@pTipoAnexo", 38);
                parametros.Add("@pGrupoSeg", -1);
                List<anexos> lisAnexosMesaTecnicaPostventa = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("USP_fup_SEL_Anexos", parametros);
                #endregion

                #region ConsultarAnexoValidacionEquipo
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@pTipoAnexo", 39);
                parametros.Add("@pGrupoSeg", -1);
                List<anexos> lisAnexosValidacionEquipo = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("USP_fup_SEL_Anexos", parametros);
                #endregion

                #region ConsultarCntrlCm
                parametros.Clear();
                parametros.Add("@pFupID", idFup);
                parametros.Add("@pVersion", idVersion);
                List<fup_cntrcam_consulta> lisCntrCmb = ControlDatos.EjecutarStoreProcedureConParametros<fup_cntrcam_consulta>("USP_fup_SEL_Enc_Ent_ControlCambio", parametros);
                #endregion
                
                #region ConsultarConsiderationObservation
                parametros.Clear();
                parametros.Add("@pFupID", idFup);
                parametros.Add("@pVersion", idVersion);
                List<fup_considerationobservation_consulta> lisConsiderationObservationCliente = ControlDatos.EjecutarStoreProcedureConParametros<fup_considerationobservation_consulta>("USP_fup_SEL_BuscarConsideracionesObservacionesCliente", parametros);
                #endregion

                #region ConsultarSegLogistico
                parametros.Clear();
                parametros.Add("@pFupID", idFup);
                parametros.Add("@pVersion", idVersion);
                List<SegLogistico_FUP_Fechas> lisSegLogistico = ControlDatos.EjecutarStoreProcedureConParametros<SegLogistico_FUP_Fechas>("USP_fup_SEL_FecDespacho_fup", parametros);
                #endregion

                #region ConsultarHallazgosObra
                parametros.Clear();
                parametros.Add("@pFupID", idFup);
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@pTipo", 3);
                List<fup_considerationobservation_consulta> lisHallazgosObra = ControlDatos.EjecutarStoreProcedureConParametros<fup_considerationobservation_consulta>("USP_fup_SEL_hvp_Observaciones", parametros);
                #endregion

                #region ConsultarBitacoraObra
                parametros.Clear();
                parametros.Add("@pFupID", idFup);
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@pTipo", 4);
                List<fup_considerationobservation_consulta> lisBitacoraObra = ControlDatos.EjecutarStoreProcedureConParametros<fup_considerationobservation_consulta>("USP_fup_SEL_hvp_Observaciones", parametros);
                #endregion

                #region ConsultarRecomendaciones
                parametros.Clear();
                parametros.Add("@pFupID", idFup);
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@pTipo", 2);
                List<fup_considerationobservation_consulta> lisRecomenda = ControlDatos.EjecutarStoreProcedureConParametros<fup_considerationobservation_consulta>("USP_fup_SEL_hvp_Observaciones", parametros);
                #endregion

                #region ConsultarEstadoGarantias
                parametros.Clear();
                parametros.Add("@pFup_id", idFup);
                parametros.Add("@pVersion", idVersion);
                List<EstadoGarantias_Consulta> lisEstadoGarantias = ControlDatos.EjecutarStoreProcedureConParametros<EstadoGarantias_Consulta>("USP_fup_SEL_ResumenGarantias_fup", parametros);
                #endregion

                #region ConsultarAnexoLogros
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@pTipoAnexo", 35);
                parametros.Add("@pGrupoSeg", -1);
                List<anexos> lisAnexosLogros = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("USP_fup_SEL_Anexos", parametros);
                #endregion


                parametros.Clear();
                parametros.Add("infoGeneral", dataInfoGeneral.FirstOrDefault());
                parametros.Add("ordenFabricacion", ordenFabricacion);
                parametros.Add("ordenCotizacion", ordenCotizacion);
                parametros.Add("infoGeneralTablas", dataTablas);

                if (lisCntrCmb.Count > 0)
                    parametros.Add("varControlCambio", lisCntrCmb);
                if (dataOrdernes.Count > 0)
                    parametros.Add("listaOF", dataOrdernes);
                if (lisAnexosArm.Count > 0)
                    parametros.Add("varArmado", lisAnexosArm);
                if (lisAnexosListasEmpaque.Count > 0)
                    parametros.Add("varListasEmpaque", lisAnexosListasEmpaque);
                if (lisAnexosActaDefinicionTecnica.Count > 0)
                    parametros.Add("varActaDefinicionTecnica", lisAnexosActaDefinicionTecnica);
                if (lisAnexosMesaTecnica.Count > 0)
                    parametros.Add("varMesaTecnica", lisAnexosMesaTecnica);
                if (lisAnexosFichaTecnica.Count > 0)
                    parametros.Add("varFichaTecnica", lisAnexosFichaTecnica);
                if (lisAnexosMesaTecnicaPostventa.Count > 0)
                    parametros.Add("varMesaTecnicaPostventa", lisAnexosMesaTecnicaPostventa);
                if (lisAnexosValidacionEquipo.Count > 0)
                    parametros.Add("varAnexosValidacionEquipo", lisAnexosValidacionEquipo);
                if (lisConsiderationObservationCliente.Count > 0)
                    parametros.Add("varConsiderationObservationCliente", lisConsiderationObservationCliente);
                if (lisSegLogistico.Count > 0)
                    parametros.Add("varSegLogistico", lisSegLogistico);
                if (lisHallazgosObra.Count > 0)
                    parametros.Add("varHallazgosObra", lisHallazgosObra);
                if (lisBitacoraObra.Count > 0)
                    parametros.Add("varBitacoraObra", lisBitacoraObra);
                if (lisRecomenda.Count > 0)
                    parametros.Add("varRecomenda", lisRecomenda);
                if (lisEstadoGarantias.Count > 0)
                    parametros.Add("varEstadoGarantias", lisEstadoGarantias);
                if (lisAnexosLogros.Count > 0)
                    parametros.Add("varAnexosLogros", lisAnexosLogros);
                if (dataPlanTec.Count > 0)
                    parametros.Add("varPlanTecnico", dataPlanTec);
                if (dataPQRSAsociados.Count > 0)
                    parametros.Add("varPQRSAsociados", dataPQRSAsociados);

                //parametros.Add("varEventoPFT", lstEventoPFT);

                response = JsonConvert.SerializeObject(parametros);
            }

            return response;
        }

        [WebMethod(EnableSession = true)]
        public static string guardarLogrosDestacados(guardar_logros_destacados item)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pFupID", item.IdFUP);
            parametros.Add("@pVersion", item.Version);
            parametros.Add("@pLogrosDestacados", item.data);
            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_LogrosDestacadosProyecto", parametros);
            return "Ok";
        }

        [WebMethod(EnableSession = true)]
        public static string obtenerInfoGeneral()
        {
            string response = string.Empty;

            List<datosCombo> ClaseCotizacion = ControlDatos.EjecutarConsulta<datosCombo>(@"SELECT [clase_cot_id] as id
																		,[clase_cot_sigla] + ' - ' + [clase_cot_descripcion]  descripcion
																		,[clase_cot_sigla] + ' - ' + [clase_cot_descripcion]  descripcionEN
																		,[clase_cot_sigla] + ' - ' + [clase_cot_descripcion]  descripcionPO
																		FROM [fup_clase_cotizacion_ent]
																		where [clase_cot_activo] = 1", new Dictionary<string, object>());

            List<datosCombo> TipoNegociacion = ControlDatos.EjecutarConsulta<datosCombo>(@"SELECT [ftne_id] id
																							,[ftne_nombre] descripcion
																							,[ftne_nombre] descripcionEN
																							,[ftne_nombre] descripcionPO
																							FROM [dbo].[fup_tipo_negociacion]
																							where [ftne_estado] = 1", new Dictionary<string, object>());

            List<datosCombo2> tipoVaciado = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT        fdom_CodDominio as id, fdom_Descripcion as descripcion
                                                                                            ,fdom_DescripcionEN as descripcionEN, fdom_DescripcionPO as descripcionPO
																						FROM            fup_Dominios
																						WHERE        (fdom_Dominio = 'TIPO_VACIADO')
																						ORDER BY fdom_OrdenDominio", new Dictionary<string, object>());

            List<datosCombo2> sistemaSeguridad = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT        fdom_CodDominio as id, fdom_Descripcion as descripcion
                                                                                            ,fdom_DescripcionEN as descripcionEN, fdom_DescripcionPO as descripcionPO
																						FROM            fup_Dominios
																						WHERE        (fdom_Dominio = 'SIS_SEGURIDAD')
																						ORDER BY fdom_OrdenDominio", new Dictionary<string, object>());

            List<datosCombo2> alinVertical = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT        fdom_CodDominio as id, fdom_Descripcion as descripcion
                                                                                            ,fdom_DescripcionEN as descripcionEN, fdom_DescripcionPO as descripcionPO
																						FROM            fup_Dominios
																						WHERE        (fdom_Dominio = 'ALIN_VERTICAL')
																						ORDER BY fdom_OrdenDominio", new Dictionary<string, object>());

            List<datosCombo2> TipoTMFachada = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT        fdom_CodDominio as id, fdom_Descripcion as descripcion
                                                                                            ,fdom_DescripcionEN as descripcionEN, fdom_DescripcionPO as descripcionPO
																						FROM            fup_Dominios
																						WHERE        (fdom_Dominio = 'TIPO_FM_FACHADA')
																						ORDER BY fdom_OrdenDominio", new Dictionary<string, object>());

            List<datosCombo2> tipoUnion = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT        fdom_CodDominio as id, fdom_Descripcion as descripcion
                                                                                            ,fdom_DescripcionEN as descripcionEN, fdom_DescripcionPO as descripcionPO
																						FROM            fup_Dominios
																						WHERE        (fdom_Dominio = 'TIPO_UNION')
																						ORDER BY fdom_OrdenDominio", new Dictionary<string, object>());

            List<datosCombo2> detUnion = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT        fdom_CodDominio as id, fdom_Descripcion as descripcion
                                                                                            ,fdom_DescripcionEN as descripcionEN, fdom_DescripcionPO as descripcionPO
																						FROM            fup_Dominios
																						WHERE        (fdom_Dominio = 'DET_UNION')
																						ORDER BY fdom_OrdenDominio", new Dictionary<string, object>());

            List<datosCombo2> formaConstruccion = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT        fdom_CodDominio as id, fdom_Descripcion as descripcion
                                                                                            ,fdom_DescripcionEN as descripcionEN, fdom_DescripcionPO as descripcionPO
																						FROM            fup_Dominios
																						WHERE        (fdom_Dominio = 'FORMA_CONSTRUCCION')
																						ORDER BY fdom_OrdenDominio", new Dictionary<string, object>());

            List<datosCombo2> terminoNegociacion = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT        fdom_CodDominio as id, fdom_Descripcion as descripcion
                                                                                            ,fdom_DescripcionEN as descripcionEN, fdom_DescripcionPO as descripcionPO
																						FROM            fup_Dominios
																						WHERE        (fdom_Dominio = 'INCOTERMS')
																						ORDER BY fdom_OrdenDominio", new Dictionary<string, object>());

            List<datosCombo2> TipoAnexo = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT [tan_id] as id, [tan_desc_esp] as descripcion
                                                                                            ,[tan_desc_esp] as descripcionEN, [tan_desc_esp] as descripcionPO
																						FROM            fup_tipo_anexo                                                                                        
																						ORDER BY tan_id ", new Dictionary<string, object>());

            List<datosCombo2> TipoAnexoArma = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT [tan_id] as id, [tan_desc_esp] as descripcion
                                                                                            ,[tan_desc_esp] as descripcionEN, [tan_desc_esp] as descripcionPO
																						FROM            fup_tipo_anexo     WHERE tan_Armado = 1                                                                                   
																						ORDER BY tan_id ", new Dictionary<string, object>());

            List<datosCombo2> CondicionesPago = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT        fdom_CodDominio as id, fdom_Descripcion as descripcion
                                                                                            ,fdom_DescripcionEN as descripcionEN, fdom_DescripcionPO as descripcionPO
																						FROM            fup_Dominios
																						WHERE        (fdom_Dominio = 'CONDICION_PAGO')
																						ORDER BY fdom_OrdenDominio", new Dictionary<string, object>());
            var query = new
            {
                listacot = ClaseCotizacion,
                listaneg = TipoNegociacion,
                listavac = tipoVaciado,
                listasg = sistemaSeguridad,
                listaav = alinVertical,
                listatf = TipoTMFachada,
                listatu = tipoUnion,
                listadu = detUnion,
                listafc = formaConstruccion,
                listatn = terminoNegociacion,
                listatax = TipoAnexo,
                listataxAr = TipoAnexoArma,
                listacondpago = CondicionesPago
            };

            response = JsonConvert.SerializeObject(query);

            return response;
        }

        [WebMethod(EnableSession = true)]
        public static string obtenerInfoGeneralPorNegociacion(int tipoNegociacion)
        {
            string response = string.Empty;

            Dictionary<string, object> paramCotizacion = new Dictionary<string, object>() { { "@neg", tipoNegociacion } };

            List<datosCombo2> TipoCotizacion = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT   ftco_id id, ftco_nombre descripcion, ftco_nombre descripcionEN, ftco_nombre descripcionPO
																						  FROM [dbo].[fup_tipo_cotizacion]
																						  WHERE ftco_uso_fup = 1 and ftco_grupo_negociacion IN (SELECT ftne_grupo_negociacion
																																				FROM [dbo].[fup_tipo_negociacion]
																																				where ftne_id = @neg)", paramCotizacion);
            List<datosCombo2> TipoProducto = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT [fup_tipo_venta_proy_id] id
																							  ,[descripcion] descripcion ,[descripcion] descripcionEN, [descripcion] descripcionPO
																						  FROM .[dbo].[fup_tipo_venta_proyecto]
																						  where [activo] = 1  and Grupo_Negociacion IN (SELECT ftne_grupo_negociacion
																																				FROM [dbo].[fup_tipo_negociacion]
																																				where ftne_id = @neg) ", paramCotizacion);

            var query = new
            {
                listatcot = TipoCotizacion,
                listaprod = TipoProducto
            };

            response = JsonConvert.SerializeObject(query);

            return response;
        }

        [WebMethod(EnableSession = true)]
        public static string guardarAprobacionFUP(fup_aprobacion_guardar item)
        {
            string response = string.Empty;
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];

            parametros.Add("@pFupID", item.IdFUP);
            parametros.Add("@pVersion", item.Version);
            parametros.Add("@pUsuario", NombreUsu);
            parametros.Add("@pAprobacion", item.Visto_bueno);
            parametros.Add("@pModulaciones", item.NumeroModulaciones);
            parametros.Add("@pCambios", item.NumeroCambios);
            parametros.Add("@ptipo_rechazo_id", item.MotivoRechazo);
            parametros.Add("@pobservacion", item.ObservacionAprobacion);
            parametros.Add("@AlturaFormaleta", item.AlturaFormaleta);
            parametros.Add("@pNivelComplejidad", item.NivelComplejidad);

            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_Aprobacion_cot", parametros);
            //Proceso de Notificacion de Evento
            int pEvento = 0;
            if (item.Visto_bueno == 1) pEvento = 3;          //Notificar Aprobacion
            else if (item.Visto_bueno == 2)
            {
                if (item.estado == "Cierre Comercial")
                {
                    pEvento = 45;     //Notificar Devolucion desde Cierre Comercial
                }
                else
                {
                    pEvento = 4;     //Notificar Devolucion
                }
            }
            response = CorreoFUP(item.IdFUP, item.Version, pEvento);
            return response;
        }

        [WebMethod(EnableSession = true)]
        public static string guardarEnAnalisis(fup_AlturaFormaleta_guardar item)
        {
            string response = string.Empty;
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];

            parametros.Add("@pFupID", item.IdFUP);
            parametros.Add("@pVersion", item.Version);
            parametros.Add("@pUsuario", NombreUsu);
            parametros.Add("@pEnAnalisis", item.EnAnalisis);

            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_CT_EnAnalisis", parametros);
            //Proceso de Notificacion de Evento
            response = JsonConvert.SerializeObject(item.IdFUP);
            return response;
        }

        [WebMethod(EnableSession = true)]
        public static string guardarFecSeg(fup_segcot_guardar item)
        {
            string response = string.Empty;
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];
            string CorreoUsuario = (string)HttpContext.Current.Session["rcEmail"];

            parametros.Add("@pFupID", item.IdFUP);
            parametros.Add("@pVersion", item.Version);
            parametros.Add("@pUsuario", NombreUsu);
            parametros.Add("@pAlDias", item.AlDias);
            parametros.Add("@pAlFecConfirma", item.AlConfirma ?? (DateTime)Convert.ToDateTime("1900-01-01"));
            parametros.Add("@pAlFecReal", item.AlReal ?? (DateTime)Convert.ToDateTime("1900-01-01"));
            parametros.Add("@pAlFecAprobada", item.AlAprobado ?? (DateTime)Convert.ToDateTime("1900-01-01"));
            parametros.Add("@pAccDias", item.AccDias);
            parametros.Add("@pAccFecConfirma", item.AccConfirma ?? (DateTime)Convert.ToDateTime("1900-01-01"));
            parametros.Add("@pAccFecReal", item.AccReal ?? (DateTime)Convert.ToDateTime("1900-01-01"));
            parametros.Add("@pAccFecAprobada", item.AccAprobado ?? (DateTime)Convert.ToDateTime("1900-01-01"));

            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_Fec_Seguimiento", parametros);

            //Proceso de Notificacion de Evento
            response = CorreoFUP(item.IdFUP, item.Version, 68);

            //            response = JsonConvert.SerializeObject(item.IdFUP);
            return response;
        }

        [WebMethod(EnableSession = true)]
        public static string SolicitudAvales(int idFup, string idVersion, decimal pValorCierre, string pMoneda)
        {
            string response = string.Empty;
            string mensaje = string.Empty;
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];

            mensaje = "Valor del Cierre " + pValorCierre.ToString() + " " + pMoneda;
            //Actualizar la fecha de Solicitud de Avales

            parametros.Add("@pFupID", idFup);
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@pUsuario", NombreUsu);
            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_SolicitudAval", parametros);
            //Proceso de Notificacion de Evento


            //Proceso de Notificacion de Evento
            response = CorreoFUP(idFup, idVersion, 56, mensaje);  // Aval Tesoreria
            response = CorreoFUP(idFup, idVersion, 57, mensaje);  // Aval Juridico
            return response;
        }

        [WebMethod]
        public static string obtenerAprobacionesFUP(string idFup, string idVersion)
        {
            string response = string.Empty;
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            parametros.Add("@pFupID", double.Parse(idFup));
            parametros.Add("@pVersion", idVersion);
            List<fup_aprobacion_consulta> dataAprobacion = ControlDatos.EjecutarStoreProcedureConParametros<fup_aprobacion_consulta>("USP_fup_SEL_Aprobacion_cot", parametros);
            parametros.Add("@pEscomercial", 0);
            List<fup_aprobacion_consulta> dataRechazo = ControlDatos.EjecutarStoreProcedureConParametros<fup_aprobacion_consulta>("USP_fup_SEL_rechazo_cot", parametros);

            #region ConsultarFecSeg
            parametros.Clear();
            parametros.Add("@pFupID", double.Parse(idFup));
            parametros.Add("@pVersion", idVersion);
            List<fup_segcot_consulta> dataFecSeg = ControlDatos.EjecutarStoreProcedureConParametros<fup_segcot_consulta>("USP_fup_SEL_Fec_Seguimiento", parametros);
            #endregion

            if (dataAprobacion.Count > 0)
                parametros.Add("varDataAprobacion", dataAprobacion.FirstOrDefault());

            if (dataRechazo.Count > 0)
                parametros.Add("varDataRechazo", dataRechazo);

            if (dataFecSeg.Count > 0)
                parametros.Add("varFecSeg", dataFecSeg.FirstOrDefault());

            if (parametros.Count > 0)
                response = JsonConvert.SerializeObject(parametros);

            return response;
        }

        [WebMethod]
        public static string obtenerAnexosFUP(string idFup, string idVersion, int TipoAnexo = 0)
        {
            string response = string.Empty;
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            parametros.Add("@pFupID", double.Parse(idFup));
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@pTipoAnexo", TipoAnexo);
            List<anexos> dataAnexos = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("USP_fup_SEL_Anexos", parametros);
            response = JsonConvert.SerializeObject(dataAnexos);
            return response;
        }


        [WebMethod]
        public static string obtenerAnexosCierre(string idFup, string idVersion, int TipoAnexo = 0)
        {
            string response = string.Empty;
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            parametros.Add("@pFupID", double.Parse(idFup));
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@pTipoAnexo", TipoAnexo);
            List<anexos> dataAnexos = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("USP_fup_SEL_Anexos", parametros);
            response = JsonConvert.SerializeObject(dataAnexos);
            return response;
        }

        [WebMethod]
        public static string obtenerDetalleCondicionesPago(string idFup, string idVersion, int TipoPago = 3)
        {
            string response = string.Empty;
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            parametros.Add("@pFupID", double.Parse(idFup));
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@pTipoAnexo", -1);
            parametros.Add("@pGrupoSeg", TipoPago);
            List<anexos> dataAnexos = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("USP_fup_SEL_Anexos", parametros);
            response = JsonConvert.SerializeObject(dataAnexos);
            return response;
        }

        [WebMethod]
        public static string obtenerDetalleDocumentosCierre(string idFup, string idVersion, int TipoPago = 4)
        {
            string response = string.Empty;
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            parametros.Add("@pFupID", double.Parse(idFup));
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@pTipoAnexo", -1);
            parametros.Add("@pGrupoSeg", TipoPago);
            List<anexos> dataAnexos = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("USP_fup_SEL_Anexos", parametros);
            response = JsonConvert.SerializeObject(dataAnexos);
            return response;
        }

        [WebMethod]
        public static string obtenerDetalleActaDefinicionTecnica(string idFup, string idVersion, int TipoPago = 4)
        {
            string response = string.Empty;
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            parametros.Add("@pFupID", double.Parse(idFup));
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@pTipoAnexo", -1);
            parametros.Add("@pGrupoSeg", TipoPago);
            //List<anexos> dataAnexos = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("", parametros);
            List<anexos> dataAnexos = new List<anexos>();
            response = JsonConvert.SerializeObject(dataAnexos);
            return response;
        }

        [WebMethod]
        public static string obtenerDetalleListasEmpaque(string idFup, string idVersion, int TipoPago = 4)
        {
            string response = string.Empty;
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            parametros.Add("@pFupID", double.Parse(idFup));
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@pTipoAnexo", -1);
            parametros.Add("@pGrupoSeg", TipoPago);
            //List<anexos> dataAnexos = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("", parametros);
            List<anexos> dataAnexos = new List<anexos>();
            response = JsonConvert.SerializeObject(dataAnexos);
            return response;
        }


        [WebMethod]
        public static string obtenerDetalleActaMesaTecnica(string idFup, string idVersion, int TipoPago = 4)
        {
            string response = string.Empty;
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            parametros.Add("@pFupID", double.Parse(idFup));
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@pTipoAnexo", -1);
            parametros.Add("@pGrupoSeg", TipoPago);
            //List<anexos> dataAnexos = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("", parametros);
            List<anexos> dataAnexos = new List<anexos>();
            response = JsonConvert.SerializeObject(dataAnexos);
            return response;
        }

        [WebMethod]
        public static string obtenerVersionPorOrdenFabricacion(string idOrdenFabricacion)
        {
            ControlFUP controlFup = new ControlFUP();
            string response = string.Empty;
            List<VersionFup> lstVersion = controlFup.obtenerFUPporOrdenFabricacion(idOrdenFabricacion);
            response = JsonConvert.SerializeObject(lstVersion);
            return response;
        }

        [WebMethod]
        public static string obtenerFUPporOrdenCliente(string idOrdenCliente)
        {
            ControlFUP controlFup = new ControlFUP();
            string response = string.Empty;
            List<VersionFup> lstVersion = controlFup.obtenerFUPporOrdenCliente(idOrdenCliente);
            response = JsonConvert.SerializeObject(lstVersion);
            return response;
        }

        [WebMethod]
        public static string ValidarReferencia(string referencia)
        {
            try
            {
                string sql = @"SELECT numero+'-'+ano OrdenReferencia, MAX(tipo_of+' '+hecha_en) hecha_en
							  from orden where numero+'-'+ano = @referencia Group by numero+'-'+ano ";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@referencia", referencia);

                List<referencia> listaref = ControlDatos.EjecutarConsulta<referencia>(sql, param);

                if (listaref.Count > 0)
                {
                    var query = new
                    {
                        id = 1,
                        descripcion = "Referencia Encontrada",
                    };

                    var res = new
                    {
                        conf = query,
                        referencia = (from a in listaref
                                      select a).FirstOrDefault()
                    };

                    string response = JsonConvert.SerializeObject(res);
                    return response;
                }
                else
                {
                    throw new Exception("No se encontro la referencia");
                }

            }
            catch (Exception ex)
            {
                var query = new
                {
                    id = 2,
                    descripcion = ex.Message,
                };

                var res = new
                {
                    conf = query
                };

                string response = JsonConvert.SerializeObject(res);
                return response;
            }
        }
        [WebMethod]
        public static string ValidarCopiaFup(string referencia)
        {
            try
            {
                string sql = @"SELECT e.eect_fup_id Fup, p.descripcion ProductoCopia, numero+'-'+ano OrdenReferencia, max(o.tipo_of+' '+hecha_en) hecha_en
                                FROM fup_enc_entrada_cotizacion e
	                                inner join fup_tipo_venta_proyecto p on p.fup_tipo_venta_proy_id = e.eect_tipo_venta_proy_id
	                                inner join Orden_Seg os on os.fup = e.eect_fup_id and os.vers = e.eect_vercot_id 
	                                inner join orden o on o.Id_Of_P = os.Id_Ofa
                                WHERE e.eect_fup_id = @referencia and e.eect_actual =1 and e.eect_activo = 1
                                GROUP BY e.eect_fup_id, p.descripcion, numero+'-'+ano ";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@referencia", referencia);

                List<referenciaCopia> listaref = ControlDatos.EjecutarConsulta<referenciaCopia>(sql, param);

                if (listaref.Count > 0)
                {


                    string response = JsonConvert.SerializeObject(listaref);
                    return response;
                }
                else
                {
                    throw new Exception("No se encontro la referencia");
                }

            }
            catch (Exception ex)
            {
                var query = new
                {
                    Fup = -1,
                    Producto = ex.Message,
                };

                string response = JsonConvert.SerializeObject(query);
                return response;
            }
        }
        [WebMethod(EnableSession = true)]
        public static string guardarRecotizacionFUP(string idFup, string idVersion, string idTipoRecotizacion, string observacion)
        {
            ControlFUP controlFup = new ControlFUP();
            string response = string.Empty;
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];

            parametros.Add("@pFupID", Convert.ToInt32(idFup));
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@pUsuario", NombreUsu);
            parametros.Add("@pDescripcion", observacion);
            parametros.Add("@pTipoRecotizacion", Convert.ToInt32(idTipoRecotizacion));
            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_RecotizacionN", parametros);
            return "OK";
        }

        [WebMethod]
        public static string ObtenerVentaCierreComercial(int pFupID, string pVersion)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pFupID", pFupID);
            parametros.Add("@pVersion", pVersion);

            ventaCierreComercial data = ControlDatos.EjecutarStoreProcedureConParametros<ventaCierreComercial>("USP_fup_SEL_Cierre_ComercialN", parametros).FirstOrDefault();

            string response = JsonConvert.SerializeObject(data);
            return response;
        }

        [WebMethod(EnableSession = true)]
        public static string guardarVentaCierreComercial(string idFup, string idVersion, string observacion, string observacionVar_m2)
        {
            ControlFUP controlFup = new ControlFUP();
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];


            parametros.Add("@pFupID", Convert.ToInt32(idFup));
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@pUsuario", NombreUsu);
            parametros.Add("@pObservacion", observacion);
            parametros.Add("@pObservacionVar_m2", observacionVar_m2);
            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_Cierre_ComercialN", parametros);
            return "OK";
        }

        [WebMethod(EnableSession = true)]
        public static string guardarSalidaCotizacion(fup_salida_cotizacion fupsal)
        {
            ControlFUP controlFup = new ControlFUP();
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];


            parametros.Add("@pFupID", Convert.ToInt32(fupsal.fupid));
            parametros.Add("@pVersion", fupsal.version);
            parametros.Add("@pUsuario", NombreUsu);
            parametros.Add("@pEquipo_m2", fupsal.m2_equipo);
            parametros.Add("@pEuipo_valor", fupsal.vlr_equipo);
            parametros.Add("@pAdicional_m2", fupsal.m2_adicionales);
            parametros.Add("@pAdicional_valor", fupsal.vlr_adicionales);
            parametros.Add("@pDetalleArq_m2", fupsal.m2_Detalle_arquitectonico);
            parametros.Add("@pDetalleArq_valor", fupsal.vlr_Detalle_arquitectonico);
            parametros.Add("@pSisSeguridad_per", fupsal.m2_sis_seguridad);
            parametros.Add("@pSisSeguridad_valor", fupsal.vlr_sis_seguridad);
            parametros.Add("@pAcc_basico_valor", fupsal.vlr_accesorios_basico);
            parametros.Add("@pAcc_complementaio_valor", fupsal.vlr_accesorios_complementario);
            parametros.Add("@pAcc_opcional_valor", fupsal.vlr_accesorios_opcionales);
            parametros.Add("@pAcc_adicional_valor", fupsal.vlr_accesorios_adicionales);
            parametros.Add("@pOtros_productos_valor", fupsal.vlr_otros_productos);
            parametros.Add("@pCont20", fupsal.vlr_Contenedor20 ?? 0);
            parametros.Add("@pCont40", fupsal.vlr_Contenedor40 ?? 0);
            parametros.Add("@pTipoSalida", fupsal.tipoSalida);

            parametros.Add("@pNumeroModulacionesSC", fupsal.NumeroModulacionesSC);
            parametros.Add("@pNumeroCambiosSC", fupsal.NumeroCambiosSC);
            //parametros.Add("@pFechaAvalCierre", fupsal.FechaAvalCierre);


            int ok = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_salida_cotizacionN", parametros);

            string response = string.Empty;
            int evento = 5;

            if (fupsal.tipoSalida == 2)
                evento = 48;

            // Notificar Salida Cotizacion
            if (ok != -1)
                response = CorreoFUP(Convert.ToInt32(fupsal.fupid), fupsal.version, evento);
            else
                response = "KO";
            return response;

        }

        [WebMethod]
        public static string ObtenerPlanosTipoForsa(int pFupID, string pVersion)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pFupID", pFupID);
            parametros.Add("@pVersion", pVersion);

            List<planos_tipo_forsa> data = ControlDatos.EjecutarStoreProcedureConParametros<planos_tipo_forsa>("USP_fup_SEL_seg_plano_tf", parametros);

            string response = JsonConvert.SerializeObject(data);
            return response;
        }

        /*      [WebMethod(EnableSession = true)]
                public static string guardarPlanoTipoForsa(string idFup, string idVersion, string idEvento, string idResponsable, string fechaCierre, string observacion)
                {
                    ControlFUP controlFup = new ControlFUP();
                    Dictionary<string, object> parametros = new Dictionary<string, object>();
                    string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];

                    parametros.Add("@pFupID", Convert.ToInt32(idFup));
                    parametros.Add("@pVersion", idVersion);
                    parametros.Add("@pUsuario", NombreUsu);
                    parametros.Add("@pResponsable_id", Convert.ToInt32(idResponsable));
                    parametros.Add("@pEvento_id", Convert.ToInt32(idEvento));
                    parametros.Add("@pObservacion", observacion);
                    parametros.Add("@pEnviado", 0);
                    parametros.Add("@pFecCierre", Convert.ToDateTime(fechaCierre));
                    ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_seg_plano_tf", parametros);
                    string response = string.Empty;

                    // Notificar Solicitud Plano tipo Forsa
                    if (Convert.ToInt32(idEvento) == 1)
                        response = CorreoFUP(Convert.ToInt32(idFup), idVersion, 7);
                    else
                        response = "OK";
                    return response;
                }
        */

        [WebMethod]
        public static string ObtenerOrdenFabricacion(int pFupID, string pVersion)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pFupID", pFupID);
            parametros.Add("@pVersion", pVersion);

            List<Orden_Fabricacion> Ordernes = ControlDatos.EjecutarStoreProcedureConParametros<Orden_Fabricacion>("USP_fup_SEL_OrdenesFab", parametros);

            string sql = "SELECT pv.pv_id id, planta_forsa.planta_descripcion descripcion  " +
                         "FROM pedido_venta AS pv INNER JOIN " +
                         " planta_forsa AS planta_forsa ON pv.planta_id = planta_forsa.planta_id " +
                         "WHERE (pv.pv_fup_id = " + @pFupID + ") " +
                         "ORDER BY planta_forsa.planta_id ";

            List<datosCombo2> Plantas = ControlDatos.EjecutarConsulta<datosCombo2>(sql, new Dictionary<string, object>());

            List<object> data = new List<object>();

            data.Add(Ordernes);
            data.Add(Plantas);

            string response = JsonConvert.SerializeObject(data);
            return response;
        }

        [WebMethod(EnableSession = true)]
        public static string obtenerPartePorPv(int PedidoVenta)
        {
            string response = string.Empty;

            Dictionary<string, object> parametros = new Dictionary<string, object>() { { "@pPedidoVenta", PedidoVenta } };

            List<Parte_Orden_Fabricacion> Partes = ControlDatos.EjecutarStoreProcedureConParametros<Parte_Orden_Fabricacion>("USP_fup_SEL_ParteOrdenesFab", parametros);

            response = JsonConvert.SerializeObject(Partes);
            return response;
        }

        [WebMethod]
        public static string ObtenerFechaSolicitud(int pFupID, string pVersion)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pFupID", pFupID);
            parametros.Add("@pVersion", pVersion);

            List<FechaSolicitud> data = ControlDatos.EjecutarStoreProcedureConParametros<FechaSolicitud>("USP_fup_SEL_CierreCom_Fechas", parametros);

            string response = JsonConvert.SerializeObject(data);
            return response;
        }

        [WebMethod]
        public static string ObtenerAvalesFabricacion(int pFupID, string pVersion)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pFupID", pFupID);
            parametros.Add("@pVersion", pVersion);

            List<AvalesFabricacionDespliega> data = ControlDatos.EjecutarStoreProcedureConParametros<AvalesFabricacionDespliega>("USP_fup_SEL_enc_ent_AvalesFabricacion", parametros);

            string response = JsonConvert.SerializeObject(data);
            return response;
        }

        [WebMethod(EnableSession = true)]
        public static string GuardarFechaSolicitud_Old(string idFup, string idVersion, string FechafirmaContrato, string Fechacontractual, string FechaFormalizaPago, string FechaPlazo)
        {
            ControlFUP controlFup = new ControlFUP();
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];

            parametros.Add("@pFupID", Convert.ToInt32(idFup));
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@pUsuario", NombreUsu);
            parametros.Add("@pFechafirmaContrato", Convert.ToDateTime(FechafirmaContrato));
            parametros.Add("@pFechacontractual", Convert.ToDateTime(Fechacontractual));
            parametros.Add("@pFechaAnticipado", Convert.ToDateTime(FechaFormalizaPago));
            parametros.Add("@pPlazo", Convert.ToInt32(FechaPlazo));

            ControlDatos.GuardarStoreProcedureConParametros("[USP_fup_UPD_Cierre_Comercial]", parametros);
            return "OK";
        }

        [WebMethod(EnableSession = true)]
        public static string GuardarAvalesFabricacion(AvalesFabricacion item)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];
            string response = string.Empty;
            string mensaje = "  ";

            if (item.AutorizaJuridico != 1 && item.cmJuridico == 1)
            {
                parametros.Add("@pFupID", Convert.ToInt32(item.IdFUP));
                parametros.Add("@pVersion", item.Version);
                parametros.Add("@pTipoAutoriza", 1);
                parametros.Add("@pAprobacion", item.AutorizaJuridico);
                parametros.Add("@pObservacion", item.AutorizaJuridico_Observacion ?? "");
                parametros.Add("@pUsuario", NombreUsu);
                parametros.Add("@pFecAval", item.firmaContratoAproba);
                ControlDatos.GuardarStoreProcedureConParametros("[USP_fup_UPD_enc_ent_AvalesFabricacion]", parametros);
                // Notificar de acuerdo con el proceso Solicitado
                if (item.AutorizaJuridico == 3) { mensaje = "No Aprobado"; }
                else { mensaje = "Aprobado"; }
                mensaje += "  Observacion : " + item.AutorizaJuridico_Observacion;

                response = CorreoFUP(Convert.ToInt32(item.IdFUP), item.Version, 58, mensaje);
            }
            parametros.Clear();
            if (item.AutorizaTesoreria != 1 && item.cmTesoreria == 1)
            {
                parametros.Clear();
                parametros.Add("@pFupID", Convert.ToInt32(item.IdFUP));
                parametros.Add("@pVersion", item.Version);
                parametros.Add("@pTipoAutoriza", 2);
                parametros.Add("@pAprobacion", item.AutorizaTesoreria);
                parametros.Add("@pObservacion", item.AutorizaTesoreria_Observacion ?? "");
                parametros.Add("@pUsuario", NombreUsu);
                parametros.Add("@pFecAval", item.FormalizaPagoAproba);
                ControlDatos.GuardarStoreProcedureConParametros("[USP_fup_UPD_enc_ent_AvalesFabricacion]", parametros);
                if (item.AutorizaTesoreria == 3) { mensaje = "No Aprobado"; }
                else { mensaje = "Aprobado"; }
                mensaje += "  Observacion : " + item.AutorizaTesoreria_Observacion;
                response = CorreoFUP(Convert.ToInt32(item.IdFUP), item.Version, 59, mensaje);
            }
            parametros.Clear();
            if (item.AutorizaGercom != 1 && item.cmGercom == 1)
            {
                parametros.Clear();
                parametros.Add("@pFupID", Convert.ToInt32(item.IdFUP));
                parametros.Add("@pVersion", item.Version);
                parametros.Add("@pTipoAutoriza", 3);
                parametros.Add("@pAprobacion", item.AutorizaGercom);
                parametros.Add("@pObservacion", item.AutorizaGercom_Observacion ?? "");
                parametros.Add("@pUsuario", NombreUsu);
                ControlDatos.GuardarStoreProcedureConParametros("[USP_fup_UPD_enc_ent_AvalesFabricacion]", parametros);
                if (item.AutorizaGercom == 3) { mensaje = "No Aprobado"; }
                else { mensaje = "Aprobado"; }
                mensaje += "  Observacion : " + item.AutorizaGercom_Observacion;
                response = CorreoFUP(Convert.ToInt32(item.IdFUP), item.Version, 61, mensaje);
            }
            parametros.Clear();
            if (item.AutorizaVicecom != 1 && item.cmVicecom == 1)
            {
                parametros.Clear();
                parametros.Add("@pFupID", Convert.ToInt32(item.IdFUP));
                parametros.Add("@pVersion", item.Version);
                parametros.Add("@pTipoAutoriza", 4);
                parametros.Add("@pAprobacion", item.AutorizaVicecom);
                parametros.Add("@pObservacion", item.AutorizaVicecom_Observacion ?? "");
                parametros.Add("@pUsuario", NombreUsu);
                ControlDatos.GuardarStoreProcedureConParametros("[USP_fup_UPD_enc_ent_AvalesFabricacion]", parametros);
                if (item.AutorizaVicecom == 3) { mensaje = "No Aprobado"; }
                else { mensaje = "Aprobado"; }
                mensaje += "  Observacion : " + item.AutorizaVicecom_Observacion;
                response = CorreoFUP(Convert.ToInt32(item.IdFUP), item.Version, 60, mensaje);
            }
            parametros.Clear();
            if (item.AutorizaJuridicoDesp != 1 && item.cmJuridico2 == 1)
            {
                parametros.Add("@pFupID", Convert.ToInt32(item.IdFUP));
                parametros.Add("@pVersion", item.Version);
                parametros.Add("@pTipoAutoriza", 5);
                parametros.Add("@pAprobacion", item.AutorizaJuridicoDesp);
                parametros.Add("@pObservacion", item.AutorizaJuridico_ObservacionDesp ?? "");
                parametros.Add("@pUsuario", NombreUsu);
                ControlDatos.GuardarStoreProcedureConParametros("[USP_fup_UPD_enc_ent_AvalesFabricacion]", parametros);
                if (item.AutorizaJuridicoDesp == 3) { mensaje = "No Aprobado"; }
                else { mensaje = "Aprobado"; }
                mensaje += "  Observacion : " + item.AutorizaJuridico_ObservacionDesp;
                response = CorreoFUP(Convert.ToInt32(item.IdFUP), item.Version, 58, mensaje);
            }
            parametros.Clear();
            if (item.AutorizaTesoreria2 != 1 && item.cmTesoreria2 == 1)
            {
                parametros.Clear();
                parametros.Add("@pFupID", Convert.ToInt32(item.IdFUP));
                parametros.Add("@pVersion", item.Version);
                parametros.Add("@pTipoAutoriza", 6);
                parametros.Add("@pAprobacion", item.AutorizaTesoreria2);
                parametros.Add("@pObservacion", item.AutorizaTesoreria2_Observacion ?? "");
                parametros.Add("@pUsuario", NombreUsu);
                ControlDatos.GuardarStoreProcedureConParametros("[USP_fup_UPD_enc_ent_AvalesFabricacion]", parametros);
                if (item.AutorizaTesoreria2 == 3) { mensaje = "No Aprobado"; }
                else { mensaje = "Aprobado"; }
                mensaje += "  Observacion : " + item.AutorizaTesoreria2_Observacion;
                response = CorreoFUP(Convert.ToInt32(item.IdFUP), item.Version, 59, mensaje);
            }
            parametros.Clear();
            if (item.AutorizaGercomDesp != 1 && item.cmGercom2 == 1)
            {
                parametros.Clear();
                parametros.Add("@pFupID", Convert.ToInt32(item.IdFUP));
                parametros.Add("@pVersion", item.Version);
                parametros.Add("@pTipoAutoriza", 7);
                parametros.Add("@pAprobacion", item.AutorizaGercomDesp);
                parametros.Add("@pObservacion", item.AutorizaGercom_ObservacionDesp ?? "");
                parametros.Add("@pUsuario", NombreUsu);
                ControlDatos.GuardarStoreProcedureConParametros("[USP_fup_UPD_enc_ent_AvalesFabricacion]", parametros);
                if (item.AutorizaGercomDesp == 3) { mensaje = "No Aprobado"; }
                else { mensaje = "Aprobado"; }
                mensaje += "  Observacion : " + item.AutorizaGercom_ObservacionDesp;
                response = CorreoFUP(Convert.ToInt32(item.IdFUP), item.Version, 61, mensaje);
            }
            parametros.Clear();
            if (item.AutorizaVicecomDesp != 1 && item.cmVicecom2 == 1)
            {
                parametros.Clear();
                parametros.Add("@pFupID", Convert.ToInt32(item.IdFUP));
                parametros.Add("@pVersion", item.Version);
                parametros.Add("@pTipoAutoriza", 8);
                parametros.Add("@pAprobacion", item.AutorizaVicecomDesp);
                parametros.Add("@pObservacion", item.AutorizaVicecom_ObservacionDesp ?? "");
                parametros.Add("@pUsuario", NombreUsu);
                ControlDatos.GuardarStoreProcedureConParametros("[USP_fup_UPD_enc_ent_AvalesFabricacion]", parametros);
                if (item.AutorizaVicecomDesp == 3) { mensaje = "No Aprobado"; }
                else { mensaje = "Aprobado"; }
                mensaje += "  Observacion : " + item.AutorizaVicecom_ObservacionDesp;
                response = CorreoFUP(Convert.ToInt32(item.IdFUP), item.Version, 60, mensaje);
            }
            return "OK";
        }

        [WebMethod]
        public static string GuardarCondicion(int pFupId, string pidVersion, List<fup_CodPago> ListaCondiciones)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            parametros.Add("@pFupID", pFupId);
            parametros.Add("@pVersion", pidVersion);
            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_DEL_enc_ent_condPago", parametros);

            foreach (fup_CodPago tabla in ListaCondiciones)
            {
                parametros.Clear();
                parametros.Add("@pFupID", pFupId);
                parametros.Add("@pVersion", pidVersion);
                parametros.Add("@pTipoPago", tabla.TipoPago);
                parametros.Add("@Consecutivo", tabla.Consecutivo);
                parametros.Add("@Fecha", tabla.Fecha);
                parametros.Add("@Condicion", tabla.Condicion);
                parametros.Add("@Valor", tabla.Valor);
                ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_enc_ent_condPago", parametros);
            }

            string response = JsonConvert.SerializeObject(pFupId);
            return response;
        }

        [WebMethod(EnableSession = true)]
        public static string ActualizarEstado(string idFup, string idVersion, string Estado, int pEvento)
        {
            ControlFUP controlFup = new ControlFUP();
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];
            string response = string.Empty;

            parametros.Add("@pFupID", Convert.ToInt32(idFup));
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@pUsuario", NombreUsu);
            parametros.Add("@pEstado", Estado);

            ControlDatos.GuardarStoreProcedureConParametros("[USP_fup_ProcesoEstado]", parametros);

            // Notificar de acuerdo con el proceso Solicitado
            response = CorreoFUP(Convert.ToInt32(idFup), idVersion, pEvento);
            return response;
        }

        [WebMethod(EnableSession = true)]
        public static string CorreoNotificacion(string idFup, string idVersion, int pEvento)
        {
            string response = string.Empty;
            // Notificar de acuerdo con el proceso Solicitado
            response = CorreoFUP(Convert.ToInt32(idFup), idVersion, pEvento);
            return response;
        }

        [WebMethod(EnableSession = true)]
        public static string ParamSF(string idFup, string idVersion, string pCliente, string pObra, string pProducido, string pPais, string pCiudad, string pMoneda)
        {
            HttpContext.Current.Session["CLIENTE"] = pCliente;
            HttpContext.Current.Session["OBRA"] = pObra;
            HttpContext.Current.Session["FUP"] = idFup;
            HttpContext.Current.Session["VER"] = idVersion;
            HttpContext.Current.Session["TIPO"] = "OF";
            HttpContext.Current.Session["PROD"] = pProducido;
            HttpContext.Current.Session["Pais"] = pPais;
            HttpContext.Current.Session["Ciudad"] = pCiudad;
            HttpContext.Current.Session["MONEDA"] = pMoneda;
            HttpContext.Current.Session["Bandera"] = "1";
            HttpContext.Current.Session["DescTipo"] = "ORDEN FABRICACION";

            return "OK";
        }

        [WebMethod]
        public static string ObtenerEstado(int pFupID, string pVersion)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pFupID", pFupID);
            parametros.Add("@pVersion", pVersion);

            List<EstadoFup> data = ControlDatos.EjecutarStoreProcedureConParametros<EstadoFup>("USP_fup_SEL_ObtenerEstado", parametros);

            string response = JsonConvert.SerializeObject(data);
            return response;
        }

        [WebMethod(EnableSession = true)]
        public static string ObtenerRol()
        {
            int rol = (int)HttpContext.Current.Session["Rol"];
            string Nombre = (string)HttpContext.Current.Session["Nombre_Usuario"];
            int CreaOF = Convert.ToInt32(HttpContext.Current.Session["CreaOf"]);

            var objeto = new
            {
                Rol = rol.ToString(),
                username = Nombre,
                CreaOF = CreaOF
            };

            string response = JsonConvert.SerializeObject(objeto);
            return response;
        }

        [WebMethod(EnableSession = true)]
        public static string ObtenerCliente()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string IdCliente = HttpContext.Current.Session["IdCliente"].ToString();

            parametros.Clear();
            parametros.Add("@pIdCliente", Convert.ToInt32(IdCliente));
            List<fup_consultar> dataCliente = ControlDatos.EjecutarStoreProcedureConParametros<fup_consultar>("USP_fup_SEL_DatosClientes", parametros);

            string response = JsonConvert.SerializeObject(dataCliente.FirstOrDefault());
            return response;
        }

        [WebMethod(EnableSession = true)]
        public static string ObtenerFupParam()
        {
            string response = string.Empty;

            string IdFUP = HttpContext.Current.Session["IdFUP"].ToString();
            if ((IdFUP != "-1") && (IdFUP != ""))
                response = JsonConvert.SerializeObject(Convert.ToInt32(IdFUP));
            return response;
        }

        [WebMethod(EnableSession = true)]
        public static string CorreoFUP(int fup, string version, int pEvento, string mensajeAdd = "")
        {
            ControlFUP controlFup = new ControlFUP();
            string sfId = "0";
            int Evento = (int)pEvento;
            string Nombre = (string)HttpContext.Current.Session["Nombre_Usuario"];
            string CorreoUsuario = (string)HttpContext.Current.Session["rcEmail"];
            int parte = 0;

            if (HttpContext.Current.Session["Parte"] == null)
                parte = 0;
            else
                parte = Convert.ToInt32(HttpContext.Current.Session["Parte"]);

            string correoSistema = (string)HttpContext.Current.Session["CorreoSistema"];
            string UsuarioAsunto = (string)HttpContext.Current.Session["UsuarioAsunto"];

            //int EncabezadoFUP = controlfup.EncabezadoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim());

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pFupID ", fup);
            parametros.Add("@pVersion", version);
            parametros.Add("@pEvento", pEvento);
            parametros.Add("@pUsuario", UsuarioAsunto);
            parametros.Add("@pRemitente", CorreoUsuario);
            parametros.Add("@pParte", parte);

            List<NotificaFup> data = ControlDatos.EjecutarStoreProcedureConParametros<NotificaFup>("USP_fup_notificacionesN", parametros);

            //VALORES DEL ENCABEZADO 
            string AsuntoMail = Convert.ToString(data.FirstOrDefault().AsuntoMail);
            string DestinatariosMail = Convert.ToString(data.FirstOrDefault().Lista);
            string MensajeMail = Convert.ToString(data.FirstOrDefault().Msg);
            bool llevaAnexo = Convert.ToBoolean(data.FirstOrDefault().Anexo);
            string EnlaceAnexo = Convert.ToString(data.FirstOrDefault().LinkAnexo);
            string tipoAdjunto = "";
            string enlaceCarta = "";
            string nombreCarta = "";

            Byte[] correo = new Byte[0];
            WebClient clienteWeb = new WebClient();
            clienteWeb.Dispose();
            clienteWeb.Credentials = new System.Net.NetworkCredential("forsa", "F0rUs3s4*", "FORSA");
            // Adjunto
            //DEFINIMOS LA CLASE DE MAILMESSAGE
            MailMessage mail = new MailMessage();
            //INDICAMOS EL EMAIL DE ORIGEN
            mail.From = new MailAddress(correoSistema);


            //AÑADIMOS LA DIRRECCIÓN DE CORREO DESTINATARIO
            mail.To.Add(DestinatariosMail);
            //mail.To.Add("ivanvidal@forsa.net.co");


            //INCLUIMOS EL ASUNTO DEL MENSAJE
            mail.Subject = AsuntoMail;
            //AÑADIMOS EL CUERPO DEL MENSAJE
            mail.Body = "  " + MensajeMail + "  " + System.Environment.NewLine + mensajeAdd;
            //INDICAMOS EL TIPO DE CODIFICACIÓN DEL MENSAJE
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            //DEFINIMOS LA PRIORIDAD DEL MENSAJE
            mail.Priority = System.Net.Mail.MailPriority.Normal;
            //INDICAMOS SI EL CUERPO DEL MENSAJE ES HTML O NO
            mail.IsBodyHtml = true;
            //ADJUNTAMOS EL ARCHIVO
            MemoryStream ms = new MemoryStream();
            if (llevaAnexo == true)
            {
                string enlace = "";

                if (Evento == 13)
                {
                    tipoAdjunto = "FUP";
                }
                else
                {
                    if ((Evento == 2) || (Evento == 4) || (Evento == 5) || (Evento == 7))
                    {
                        tipoAdjunto = "FUP";
                        enlace = @"http://10.75.131.2:81/ReportServer?" + EnlaceAnexo.Trim() + "&rs:format=PDF&rs:command=render&rs:ClearSession=true&pFupID=" + fup.ToString() + "" +
                                "&pVersion=" + version;
                    }
                    else
                    {
                        if (Evento == 9 || Evento == 23 || Evento == 24 || Evento == 25)
                        {
                            tipoAdjunto = "SF";
                            parte = Convert.ToInt32(HttpContext.Current.Session["Parte"]);
                            sfId = HttpContext.Current.Session["SfId"].ToString();

                            enlace = @"http://10.75.131.2:81/ReportServer?" + EnlaceAnexo.Trim() + "&rs:format=PDF&rs:command=render&rs:ClearSession=true&numfup=" + fup.ToString() + "" +
                                "&version=" + version + "&parte=" + parte.ToString() + "&sf_id=" + sfId;
                        }
                        else
                        {
                            if (Evento == 16 || Evento == 15)
                            {
                                tipoAdjunto = "SF";
                                parte = Convert.ToInt32(HttpContext.Current.Session["Parte"]);
                                sfId = HttpContext.Current.Session["SfId"].ToString();

                                enlace = @"http://10.75.131.2:81/ReportServer?" + EnlaceAnexo.Trim() + "&rs:format=PDF&rs:command=render&rs:ClearSession=true&numfup=" + fup + "" +
                                    "&version=" + version + "&parte=" + parte.ToString() + "&sf_id=" + sfId;
                            }
                        }
                    }
                }
                correo = clienteWeb.DownloadData(enlace);
                ms = new MemoryStream(correo);
                mail.Attachments.Add(new Attachment(ms, tipoAdjunto + " " + fup.ToString() + version + ".pdf"));

                // adjunto el archivo de la carta directamente desde la carpeta de planos
                if (pEvento == 5)
                {
                    controlFup.actualizarSalidaCotizacion(fup, version, Nombre);
                    //nombreCarta = Session["nombreCarta"].ToString();
                    //enlaceCarta = Session["rutaCarta"].ToString(); 
                    //tipoAdjunto = "CartaCotizacion";
                    //correo = clienteWeb.DownloadData(enlaceCarta);
                    //ms = new MemoryStream(correo);
                    //mail.Attachments.Add(new Attachment(ms, tipoAdjunto + fup.ToString() + version  +"_" + nombreCarta));

                }
            }
            //DEFINIMOS NUESTRO SERVIDOR SMTP
            //DECLARAMOS LA CLASE SMTPCLIENT
            SmtpClient smtp = new SmtpClient();
            //DEFINIMOS NUESTRO SERVIDOR SMTP
            smtp.Host = "smtp.office365.com";
            //INDICAMOS LA AUTENTICACION DE NUESTRO SERVIDOR SMTP
            smtp.Credentials = new System.Net.NetworkCredential("informes@forsa.net.co", "Those7953");
            // smtp.Port = 25;
            smtp.Port = 587;
            smtp.EnableSsl = true;
            //smtp.Timeout = 400;

            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain,
                SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                // smtp.SendAsync(mail, mail.To);
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                string mensaje = "ERROR: " + ex.Message;

                string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];
                parametros.Clear();
                parametros.Add("@pFupID", Convert.ToInt32(fup));
                parametros.Add("@pVersion", version);
                parametros.Add("@pUsuario", NombreUsu);
                parametros.Add("@pErrorNumber", "500");
                parametros.Add("@pErrorProcedure", "correofup");
                parametros.Add("@pErrorline", 1999);
                parametros.Add("@pMensaje", ex.Message);


                ControlDatos.GuardarStoreProcedureConParametros("[USP_FUP_ERROR]", parametros);
            }
            ms.Close();
            return "OK";
        }

        [WebMethod(EnableSession = true)]
        public static string DescargarArchivo(string NombreArchivo, string Ruta)
        {
            string actualFilePath = System.IO.Path.Combine(Ruta, NombreArchivo);
            string filename = Path.GetFileName(actualFilePath);

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ContentType = "application/octect-stream";
            HttpContext.Current.Response.AppendHeader("content-disposition",
                string.Format("attachment; filename = \"{0}\"", filename));
            HttpContext.Current.Response.Write(actualFilePath);
            HttpContext.Current.Response.End();
            return "";
        }

        //FLETE
        [WebMethod]
        public static string CalcularFlete(string idFup, string idVersion, int idPtoCargue, int idPtoDescargue, int idTerminoNegociacion, int Cant1, int Cant2, int Cant3, int Cant4, decimal ValorCot)
        {
            ControlFUP controlFup = new ControlFUP();
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];
            string response = string.Empty;

            parametros.Add("@pFupID", Convert.ToInt32(idFup));
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@puerto_origen_id", idPtoCargue);
            parametros.Add("@puerto_destino_id", idPtoDescargue);
            parametros.Add("@termino_negociacion_id", idTerminoNegociacion);
            parametros.Add("@cantidad_t1", Cant1);
            parametros.Add("@cantidad_t2", Cant2);
            parametros.Add("@cantidad_t3", Cant3);
            parametros.Add("@cantidad_t4", Cant4);
            parametros.Add("@ValorSalida", ValorCot);

            List<Calculoflete> data = ControlDatos.EjecutarStoreProcedureConParametros<Calculoflete>("USP_fup_Calcular_fletes_N", parametros);

            response = JsonConvert.SerializeObject(data.FirstOrDefault());
            return response;
        }

        [WebMethod]
        public static string ObtenerFlete(string idFup, string idVersion)
        {
            ControlFUP controlFup = new ControlFUP();
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string response = string.Empty;

            parametros.Add("@pFupID", Convert.ToInt32(idFup));
            parametros.Add("@pVersion", idVersion);

            List<Calculoflete> data = ControlDatos.EjecutarStoreProcedureConParametros<Calculoflete>("USP_fup_SEL_det_fletes_N", parametros);

            response = JsonConvert.SerializeObject(data.FirstOrDefault());
            return response;
        }

        [WebMethod]
        public static string GuardarFlete(string idFup, string idVersion, Guardarflete flete)
        {
            ControlFUP controlFup = new ControlFUP();
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];

            parametros.Add("@pFupID", Convert.ToInt32(idFup));
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@transportador_id", flete.transportador_id);
            parametros.Add("@agente_carga_id", flete.agente_carga_id);
            parametros.Add("@puerto_origen_id", flete.puerto_origen_id);
            parametros.Add("@puerto_destino_id", flete.puerto_destino_id);
            parametros.Add("@termino_negociacion_id", flete.termino_negociacion_id);
            parametros.Add("@leadTime", flete.leadTime);
            parametros.Add("@cantidad_t1", flete.cantidad_t1);
            parametros.Add("@cantidad_t2", flete.cantidad_t2);
            parametros.Add("@cantidad_t3", flete.cantidad_t3);
            parametros.Add("@cantidad_t4", flete.cantidad_t4);
            parametros.Add("@vr_origen_t1", flete.vr_origen_t1);
            parametros.Add("@vr_origen_t2", flete.vr_origen_t2);
            parametros.Add("@vr_origen_t3", flete.vr_origen_t3);
            parametros.Add("@vr_origen_t4", flete.vr_origen_t4);
            parametros.Add("@vr_gastos_origen", flete.vr_gastos_origen);
            parametros.Add("@vr_aduana_origen", flete.vr_aduana_origen);
            parametros.Add("@vr_internacional_t1", flete.vr_internacional_t1);
            parametros.Add("@vr_internacional_t2", flete.vr_internacional_t2);
            parametros.Add("@vr_gastos_destino", flete.vr_gastos_destino);
            parametros.Add("@vr_aduana_destino", flete.vr_aduana_destino);
            parametros.Add("@vr_destino_t1", flete.vr_destino_t1);
            parametros.Add("@vr_destino_t2", flete.vr_destino_t2);
            parametros.Add("@vr_seguro", flete.vr_seguro);
            parametros.Add("@pUsu_crea", NombreUsu);

            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_det_fletes", parametros);

            return "OK";
        }

        [WebMethod]
        public static string GuardarComentarios(List<Comentarios> listaComentario)
        {
            ControlFUP controlFup = new ControlFUP();
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];

            foreach (Comentarios item in listaComentario)
            {
                parametros.Clear();
                parametros.Add("@pFupID", Convert.ToInt32(item.IdFUP));
                parametros.Add("@pVersion", item.Version);
                parametros.Add("@pTipoComentario", item.Idtipo);
                parametros.Add("@pComentario", item.comentario);
                parametros.Add("@pConsecutivo", item.consecutivo);
                parametros.Add("@pusuario", NombreUsu);

                ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_comentario", parametros);
            }
            return "OK";
        }

        [WebMethod]
        public static string ObtenerComentarios(string idFup, string idVersion, int tipo)
        {
            ControlFUP controlFup = new ControlFUP();
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            string response = string.Empty;

            parametros.Add("@pFupID", Convert.ToInt32(idFup));
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@pTipoComentario", tipo);

            List<ListaComentarios> data = ControlDatos.EjecutarStoreProcedureConParametros<ListaComentarios>("USP_fup_SEL_Comentarios", parametros);

            response = JsonConvert.SerializeObject(data);
            return response;
        }

        [WebMethod(EnableSession = true)]
        public static string guardarDevComercial(int idFup, string idVersion, int TipoRechazo, string Observacion, string estado)
        {
            string response = string.Empty;
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];

            parametros.Add("@pFupID", idFup);
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@pUsuario", NombreUsu);
            parametros.Add("@ptipo_rechazo_id", TipoRechazo);
            parametros.Add("@pobservacion", Observacion);

            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_Devolucion_cot", parametros);
            //Proceso de Notificacion de Evento - Definir Evento de notificacion de rechazo Comercial
            int pEvento = 4;
            if (estado == "Cotizado") { pEvento = 46; }
            else if (estado == "Aval para SF Elaboracion") { pEvento = 47; }

            response = CorreoFUP(idFup, idVersion, pEvento);
            return response;
        }

        [WebMethod]
        public static string obtenerDevComercial(string idFup, string idVersion)
        {
            string response = string.Empty;
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            parametros.Add("@pFupID", double.Parse(idFup));
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@pEscomercial", 1);
            List<fup_aprobacion_consulta> dataRechazo = ControlDatos.EjecutarStoreProcedureConParametros<fup_aprobacion_consulta>("USP_fup_SEL_rechazo_cot", parametros);

            response = JsonConvert.SerializeObject(dataRechazo);

            return response;
        }

        // Este método cambia el estado del PTF (Cambio pendiente Aval)
        [WebMethod(EnableSession = true)]
        public static string guardarPlanoTipoForsa(string idFup, string idVersion, PTF_guardar planPTF)
        {
            ControlFUP controlFup = new ControlFUP();
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];

            DateTime FechaCierre = Convert.ToDateTime("1900/01/01");
            if (Convert.ToDateTime(planPTF.FechaCierre) != Convert.ToDateTime("1/1/0001 12:00:00 AM"))
            {
                FechaCierre = Convert.ToDateTime(planPTF.FechaCierre);
            };
            DateTime FecProgramadaSCI = Convert.ToDateTime("1900/01/01");
            if (Convert.ToDateTime(planPTF.FecProgramadaSCI) != Convert.ToDateTime("1/1/0001 12:00:00 AM"))
            {
                FecProgramadaSCI = Convert.ToDateTime(planPTF.FecProgramadaSCI);
            };
            parametros.Add("@pFupID", Convert.ToInt32(idFup));
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@pUsuario", NombreUsu);
            parametros.Add("@pResponsable_id", Convert.ToInt32(planPTF.Responsable));
            parametros.Add("@pEvento_id", planPTF.Evento);
            parametros.Add("@pObservacion", planPTF.Observacion);
            parametros.Add("@pEnviado", 0);
            parametros.Add("@pFecCierre", FechaCierre);
            parametros.Add("@ptipo_plano_id", planPTF.Plano ?? 0);
            parametros.Add("@pRecotiza", Convert.ToInt32(planPTF.Recotiza));
            parametros.Add("@pFecProgramadaSCI", FecProgramadaSCI);

            int ok = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_seg_plano_tf_N", parametros);
            string response = string.Empty;
            if (ok != -1)
            {
                int EstadoDft = 0;
                EstadoDft = Convert.ToInt32(planPTF.Evento);
                switch (planPTF.Evento)
                {
                    case "3":       //Cancelacion
                        EstadoDft = 8;
                        break;
                    case "4":       //Aprobacion
                        EstadoDft = 6;
                        break;
                }
                //Actualizar ActaSeg
                parametros.Clear();
                parametros.Add("@pFup", Convert.ToInt32(idFup));
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@pFecSolicitaDFT", FechaCierre);
                parametros.Add("@usu_actualiza", NombreUsu);
                parametros.Add("@pEstadoDft", EstadoDft);
                int ok2 = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_ActaSeg_DFT", parametros);

                //Crear Control Cambios
                switch (planPTF.Evento)
                {
                    case "1":       //SOLICITUD DFT
                        fup_cntrcam_guardar fdata = new fup_cntrcam_guardar();
                        fdata.IdFUP = Convert.ToInt32(idFup);
                        fdata.Version = idVersion;
                        fdata.cons = -1;
                        fdata.padre = 0;
                        fdata.Comentario = planPTF.Observacion;
                        fdata.Estado = "";
                        fdata.Titulo = "Solicitud DFT";
                        fdata.EsDFT = 1;
                        fdata.EstadoDFT = 1;
                        fdata.SubprocesoDFT = 1;
                        GuardarControlCambio(fdata, 1);
                        break;

                    case "4":      //AVAL TECNICO DFT
                        fup_cntrcam_guardar fdata3 = new fup_cntrcam_guardar();
                        fdata3.IdFUP = Convert.ToInt32(idFup);
                        fdata3.Version = idVersion;
                        fdata3.cons = -1;
                        fdata3.padre = 0;
                        fdata3.Comentario = planPTF.Observacion;
                        fdata3.Estado = "";
                        fdata3.Titulo = "DFT APROBADO";
                        fdata3.EsDFT = 1;
                        fdata3.EstadoDFT = 6;
                        fdata3.SubprocesoDFT = 2;
                        GuardarControlCambio(fdata3, 1);
                        break;
                    default:
                        break;
                }

                // Notificar Eventos Plano tipo Forsa
                string sqlNot = @"SELECT tes_notificacion_id id, tes_descripcion descripcion FROM fup_evento_segpf WHERE tes_id = " + planPTF.Evento.ToString();

                List<datosCombo2> NotificaPlanptf = ControlDatos.EjecutarConsulta<datosCombo2>(sqlNot, new Dictionary<string, object>());

                int Notificacion = Convert.ToInt32(NotificaPlanptf.FirstOrDefault().id);

                if (Notificacion != 0)
                    response = CorreoFUP(Convert.ToInt32(idFup), idVersion, Notificacion);
                else
                    response = "OK";
            }
            else
            {
                response = "KO";
            }
            return response;

        }

        [WebMethod(EnableSession = true)]
        public static string obtenerDuplicadoFUP(string idFup, string idVersion, int idCliente, int idContacto, int idObra, string FUsuario)
        {
            string response = string.Empty;
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            int id = 0;
            parametros.Add("@pFupID", double.Parse(idFup));
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@pUsuario", FUsuario);
            parametros.Add("@pID_Cliente", idCliente);
            parametros.Add("@pID_Contacto", idContacto);
            parametros.Add("@pID_Obra", idObra);

            id = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_Duplicar", parametros);

            #region ConsultarInformacionGeneral
            parametros.Clear();
            parametros.Add("@pFupID", Convert.ToInt32(id));
            parametros.Add("@pVersion", 'A');
            List<fup_consultar> dataInfoGeneral = ControlDatos.EjecutarStoreProcedureConParametros<fup_consultar>("USP_fup_SEL_ent_cotizacion", parametros);
            #endregion
            if (dataInfoGeneral.Count > 0)
            {
                dataInfoGeneral.FirstOrDefault().IdFUP = id;
                response = JsonConvert.SerializeObject(dataInfoGeneral.FirstOrDefault());
            }

            return response;
        }


        [WebMethod(EnableSession = true)]
        public static string guardarOrdenCotizacionFUP(string idFup, string idVersion, string FUsuario)
        {
            string ordenCotizacion = string.Empty, response = string.Empty;
            ControlFUP controlFup = new ControlFUP();

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            int id = 0;
            parametros.Add("@pFupID", double.Parse(idFup));
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@pUsuario", FUsuario);

            id = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_INS_Orden_Cotizacion", parametros);
            ordenCotizacion = controlFup.obtenerOrdenCotizacionFUP(Convert.ToInt32(idFup), idVersion);

            response = JsonConvert.SerializeObject(ordenCotizacion);

            return response;
        }

        [WebMethod(EnableSession = true)]
        public static string explosionarOrdenCotizacionFUP(string idFup, string idVersion)
        {
            string response = string.Empty;
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            int id = 5;
            parametros.Add("@pFupID", double.Parse(idFup));
            parametros.Add("@pVersion", idVersion);

            id = ControlDatos.GuardarStoreProcedureConParametros("USP_SimuladorProyecto_Sugiere", parametros);
            response = JsonConvert.SerializeObject(id.ToString());
            return response;
        }

        [WebMethod(EnableSession = true)]
        public static string ParamSimulador(string OrdenCotizacion)
        {
            HttpContext.Current.Session["ORDENCOT"] = OrdenCotizacion;

            return "OK";
        }

        [WebMethod(EnableSession = true)]
        public static string GrabaFechasCliente(int idFup, string idVersion, string FecSolicita, string FecEntrega)
        {
            string response = string.Empty;

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];


            //Actualizar la fecha de Solicitud

            parametros.Add("@pFupID", idFup);
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@pUsuario", NombreUsu);
            parametros.Add("@pFecSolicitaCliente", string.IsNullOrEmpty(FecSolicita) ? (DateTime?)null : Convert.ToDateTime(FecSolicita));
            parametros.Add("@pFecEntregaCliente", string.IsNullOrEmpty(FecEntrega) ? (DateTime?)null : Convert.ToDateTime(FecEntrega));
            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_enc_ent_FechasCliente", parametros);
            return "OK";
        }

        [WebMethod(EnableSession = true)]
        public static string GuardarControlCambio(fup_cntrcam_guardar Item, int flag = 0)
        {
            string response = string.Empty;

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];
            string CorreoUsuario = (string)HttpContext.Current.Session["rcEmail"];
            int EventoNoti;


            //Actualizar la fecha de Solicitud

            parametros.Add("@pFupID", Item.IdFUP);
            parametros.Add("@pVersion", Item.Version);
            parametros.Add("@pUsuario", NombreUsu);
            parametros.Add("@pComentario", Item.Comentario);
            parametros.Add("@pEstado", Item.Estado);
            parametros.Add("@pCons", Item.cons);
            parametros.Add("@pPadre", Item.padre);
            parametros.Add("@pTitulo", Item.Titulo);
            parametros.Add("@pEsDft", Item.EsDFT);
            parametros.Add("@pEstadoDft", Item.EstadoDFT);
            parametros.Add("@pSubproceso", Item.SubprocesoDFT);
            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_enc_ent_ControlCambio", parametros);

            if (Item.EsDFT == 1)  // para DFT
            {
                EventoNoti = 73;
                if (Item.padre != 0) { EventoNoti = 74; } // Evento de Respuesta DFT
            }
            else
            {
                EventoNoti = 66;
                if (Item.padre != 0) { EventoNoti = 67; } // Evento de Respuesta
            }
            if ((EventoNoti == 73 && flag != 1) || (EventoNoti != 73))
            {
                response = CorreoFUP((int)Item.IdFUP, Item.Version, EventoNoti);
            }
            return "OK";
        }

        [WebMethod(EnableSession = true)]
        public static string GuardarConsiderationObservation(fup_considerationobservation_guardar Item)
        {
            string response = string.Empty;
            string Mensaje = string.Empty;

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];
            string CorreoUsuario = (string)HttpContext.Current.Session["rcEmail"];

            parametros.Add("@pFupID", Item.IdFUP);
            parametros.Add("@pVersion", Item.Version);
            parametros.Add("@pUsuario", NombreUsu);
            parametros.Add("@pComentario", Item.Comentario);
            parametros.Add("@pEstado", Item.Estado);
            parametros.Add("@pCons", Item.cons);
            parametros.Add("@pPadre", Item.padre);
            parametros.Add("@pTitulo", Item.Titulo);
            //parametros.Add("@pEsDft", Item.EsDFT);
            //parametros.Add("@pEstadoDft", Item.EstadoDFT);
            //parametros.Add("@pSubproceso", Item.SubprocesoDFT);
            parametros.Add("@pFecDespacho", Item.FecDespacho);
            parametros.Add("@pFecEntrega", Item.FecEntrega);
            parametros.Add("@pTipoEntrada", Item.TipoEntrada);
            parametros.Add("@pTipoConsideracionObservacion", Item.TipoConsideracionObservacion);
            parametros.Add("@pAreaSolicitada", Item.AreaSolicitada);
            parametros.Add("@pSolucionadoEnObra", Item.SolucionadoEnObra);
            parametros.Add("@pGeneroCosto", Item.GeneroCosto);
            parametros.Add("@pHallazgoOrdenFabricacion", Item.HallazgoOrdenFabricacion);
            parametros.Add("@pDirectorObra", Item.DirectorObra);
            parametros.Add("@pResponsableObra", Item.ResponsableObra);
            parametros.Add("@pEmailResponsableObra", Item.EmailResponsableObra);
            parametros.Add("@pTelefonoResponsableObra", Item.TelefonoResponsableObra);
            parametros.Add("@pDireccionObra", Item.DireccionObra);
            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_hvp_Observaciones", parametros);

            //****************************
            // PENDIENTE POR REVISAR 
            //****************************
            int pEvento = 0;
            switch(Item.TipoEntrada)
            {
                case 3:
                    if(Item.padre == 0)
                    {
                        pEvento = 82;
                    } else
                    {
                        pEvento = 83;
                    }
                    break;
                case 4:
                    pEvento = 81;
                    break;
                case 2:
                    pEvento = 93;
                    Mensaje = Item.Comentario;
                    break;
            }
            CorreoFUP((int)Item.IdFUP, Item.Version, pEvento, Mensaje);

            return "OK";
        }

        [WebMethod]
        public static string ObtenerControlCambio(string idFup, string idVersion)
        {
            ControlFUP controlFup = new ControlFUP();
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            string response = string.Empty;

            parametros.Add("@pFupID", Convert.ToInt32(idFup));
            parametros.Add("@pVersion", idVersion);
            List<fup_cntrcam_consulta> lisCntrCmb = ControlDatos.EjecutarStoreProcedureConParametros<fup_cntrcam_consulta>("USP_fup_SEL_Enc_Ent_ControlCambio", parametros);

            response = JsonConvert.SerializeObject(lisCntrCmb);
            return response;
        }

        [WebMethod]
        public static string ObtenerBitacoraObra(string idFup, string idVersion)
        {
            ControlFUP controlFup = new ControlFUP();
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            string response = string.Empty;

            parametros.Add("@pFupID", Convert.ToInt32(idFup));
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@pTipo", 4);
            List<fup_considerationobservation_consulta> listBitacora = ControlDatos.EjecutarStoreProcedureConParametros<fup_considerationobservation_consulta>("USP_fup_SEL_hvp_Observaciones", parametros);

            response = JsonConvert.SerializeObject(listBitacora);
            return response;
        }

        [WebMethod]
        public static string ObtenerRecomendaciones(string idFup, string idVersion)
        {
            ControlFUP controlFup = new ControlFUP();
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            string response = string.Empty;

            parametros.Add("@pFupID", Convert.ToInt32(idFup));
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@pTipo", 2);
            List<fup_considerationobservation_consulta> listRecomenda = ControlDatos.EjecutarStoreProcedureConParametros<fup_considerationobservation_consulta>("USP_fup_SEL_hvp_Observaciones", parametros);

            response = JsonConvert.SerializeObject(listRecomenda);
            return response;
        }

        [WebMethod]
        public static string ObtenerHallazgos(string idFup, string idVersion)
        {
            ControlFUP controlFup = new ControlFUP();
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            string response = string.Empty;

            parametros.Add("@pFupID", Convert.ToInt32(idFup));
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@pTipo", 3);
            List<fup_considerationobservation_consulta> listHallazgos = ControlDatos.EjecutarStoreProcedureConParametros<fup_considerationobservation_consulta>("USP_fup_SEL_hvp_Observaciones", parametros);

            response = JsonConvert.SerializeObject(listHallazgos);
            return response;
        }

        [WebMethod(EnableSession = true)]
        public static string GrabaTablaArmado(int idFup, string idVersion, int ArmAlum, int ArmEsca, int ArmAcce)
        {
            string response = string.Empty;

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];


            //Actualizar la fecha de Solicitud

            parametros.Add("@pFupID", idFup);
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@pUsuario", NombreUsu);
            parametros.Add("@pArmadoAlumino", ArmAlum);
            parametros.Add("@pArmadoEscalera", ArmEsca);
            parametros.Add("@pArmadoAccesorio", ArmAcce);
            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_enc_ent_TablaArmado", parametros);
            return "OK";
        }

        [WebMethod(EnableSession = true)]
        public static string getListaPrecios(int idfup)
        {
            string response = string.Empty;

            Dictionary<string, object> parametros = new Dictionary<string, object>();

            ControlFUP controlFup = new ControlFUP();
            parametros.Add("@pFupId", idfup);

            List<ListaPrecio> ListaPrecios = ControlDatos.EjecutarStoreProcedureConParametros<ListaPrecio>("USP_fup_SEL_PrecioItem_Rap", parametros);

            response = JsonConvert.SerializeObject(ListaPrecios);
            return response;
        }

    }
}