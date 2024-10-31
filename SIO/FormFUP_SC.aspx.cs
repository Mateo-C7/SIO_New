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


namespace SIO
{
    public partial class FormFUP_SC : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scripManager = ScriptManager.GetCurrent(this.Page);
            scripManager.EnablePageMethods = true;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

        }

        [WebMethod]
        public static string search()
        {
            int proyecto = 1;
            ControlFUP controlfup = new ControlFUP();
            SqlDataReader reader = controlfup.PoblarTipoVentaProyecto(proyecto);
            return "worked";
        }

        [WebMethod(EnableSession = true)]
        public static string obtenerListadoPaises()
        {
            ControlUbicacion controlUbicacion = new ControlUbicacion();
            string representanteComercialId = (string)HttpContext.Current.Session["rcID"];
            int rol = (int)HttpContext.Current.Session["Rol"];

            List<CapaControl.Entity.Pais> lstPais = null;
            if ((rol == 3) || (rol == 28) || (rol == 29) || (rol == 33) || (rol == 30))
            {
                lstPais = controlUbicacion.obtenerListaPaisRepresentante(int.Parse(representanteComercialId));
            }
            else
            {
                lstPais = controlUbicacion.obtenerListaPais();
            }
            string response = JsonConvert.SerializeObject(lstPais);
            return response;
        }

        [WebMethod]
        public static string obtenerAlturaLibre()
        {
            List<AlturaLibre> lstAlturaLibre = ControlAlturaLibre.ObtenerAlturaLibre();
            string response = JsonConvert.SerializeObject(lstAlturaLibre);
            return response;
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
            int IdClienteUsuario = (int)HttpContext.Current.Session["IdClienteUsuario"];
            ControlCliente control = new ControlCliente();
            Dictionary<string, object> queryResult = new Dictionary<string, object>();
            List<CapaControl.Entity.Cliente> lstObject = control.obtenerDatosCliente(int.Parse(idCiudad), IdClienteUsuario);
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
            List<CapaControl.Entity.EstadoSocioEconomico> lstEstrato = controlObra.obtenerEstadoSocioEconomico();
            List<Dominios> lstTipVivienda = ControlDominios.obtener_dominios(CapaControl.Enumeradores.Dominios.TIPO_VIVIENDA.ToString());
            List<ClaseCotizacion> lstClaseCotizacion = controlFup.obtenerClaseCotizacion();
            List<Dominios> lstVoBo = ControlDominios.obtener_dominios(CapaControl.Enumeradores.Dominios.APROBACION.ToString());
            List<Dominios> lstMotivoRechazo = controlFup.obtenerMotivoRechazoFUP();
            List<Dominios> lstTipoRecotizacion = controlFup.obtenerTipoRecotizacionFUP();
            //List<Dominios> lstEventoPFT = controlFup.obtenerEventoPlanoTipoForsa();
            List<datosCombo2> lstEventoPFT = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT [tes_id] id,[tes_descripcion] descripcion
                                                    FROM [Forsa_Global].[dbo].[fup_evento_segpf]
                                                    ORDER BY [tes_orden]", new Dictionary<string, object>());
            List<datosCombo2> listaPlanptf = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT        fdom_CodDominio as id, fdom_Descripcion as descripcion
                                                                                        FROM            fup_Dominios
                                                                                        WHERE        (fdom_Dominio = 'PTF_PLANOS')
                                                                                        ORDER BY fdom_OrdenDominio", new Dictionary<string, object>());

            List<Dominios> lstResponsablePTF = controlFup.obtenerResponsablePlanoTipoForsa();
            List<datosCombo2> lstEquipos = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT        fdom_CodDominio as id, fdom_Descripcion as descripcion
                                                                                        FROM            fup_Dominios
                                                                                        WHERE        (fdom_Dominio = 'TIPO_EQUIPO')
                                                                                        ORDER BY fdom_OrdenDominio", new Dictionary<string, object>());

            List<datosCombo2> lstDevCom = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT [tre_id] id, [tre_descripcion] descripcion " +
                                                                                        "FROM fup_tipo_rechazo where tre_tipo = 9", new Dictionary<string, object>());
            List<CapaControl.Entity.Moneda> lstMoneda = controlUbicacion.obtenerMoneda();

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

            List<ItemDinamico> data = ControlDatos.EjecutarStoreProcedureConParametros<ItemDinamico>("USP_fup_SEL_Alcance_Parte", parametros).OrderBy(x => x.Orden).ToList();

            data.Where(x => !string.IsNullOrEmpty(x.DomLista)).ToList().ForEach(x =>
                x.dominio = ControlDominios.obtener_dominios(x.DomLista)
            );

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

                foreach (fup_tablas2 tabla in fup.datos_tablas)
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
            string response = string.Empty, ordenFabricacion = string.Empty;

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
                #endregion

                #region ConsultarTablasInformacionGeneral
                parametros.Clear();
                parametros.Add("@pFupID", Convert.ToInt32(idFup));
                parametros.Add("@pVersion", idVersion);
                List<fup_tablas2> dataTablas = ControlDatos.EjecutarStoreProcedureConParametros<fup_tablas2>("USP_fup_SEL_enc_ent_cot_tabla", parametros);
                #endregion

/*
                #region ConsultarParteDinamica
                parametros.Clear();
                parametros.Add("@pFupID", Convert.ToInt32(idFup));
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@TipoCotizacion", "0");
                parametros.Add("@Parte", "2");
                parametros.Add("@Idioma", idioma);
                List<ItemDinamico> data = ControlDatos.EjecutarStoreProcedureConParametros<ItemDinamico>("USP_fup_SEL_Alcance_Parte", parametros);
                #endregion
*/
                #region ConsultarEquipo
                parametros.Clear();
                parametros.Add("@pFupID", Convert.ToInt32(idFup));
                parametros.Add("@pVersion", idVersion);
                List<ItemEquipo> dataEquipo = ControlDatos.EjecutarStoreProcedureConParametros<ItemEquipo>("USP_fup_SEL_Alcance_Equipos", parametros).OrderBy(x => x.Consecutivo).ToList();
                #endregion

                #region ConsultarAdaptacion
                parametros.Clear();
                parametros.Add("@pFupID", Convert.ToInt32(idFup));
                parametros.Add("@pVersion", idVersion);
                List<ItemEquipo> dataAdicion = ControlDatos.EjecutarStoreProcedureConParametros<ItemEquipo>("USP_fup_SEL_Alcance_Adaptacion", parametros).OrderBy(x => x.Consecutivo).ToList();
                #endregion

                #region ConsultarSalidaCotizacion
                parametros.Clear();
                parametros.Add("@pFupID", Convert.ToInt32(idFup));
                parametros.Add("@pVersion", idVersion.Trim());
                parametros.Add("@pTipoSalida", 1);
                List<fup_salida_cotizacion> listaSalidaCot = ControlDatos.EjecutarStoreProcedureConParametros<fup_salida_cotizacion>("USP_fup_SEL_salida_cotizacionN", parametros);
                listaSalidaCot.ForEach(t => t.total_m2 = Math.Round(t.m2_equipo + t.m2_adicionales + t.m2_Detalle_arquitectonico, 2).ToString("N2", new CultureInfo("en-US")));
                listaSalidaCot.ForEach(t => t.total_valor = Math.Round(t.vlr_equipo + t.vlr_adicionales + t.vlr_Detalle_arquitectonico, 2).ToString("N2", new CultureInfo("en-US")));
                listaSalidaCot.ForEach(t => t.total_propuesta_com = Math.Round(t.vlr_equipo + t.vlr_adicionales + t.vlr_sis_seguridad + t.vlr_Detalle_arquitectonico + t.vlr_accesorios_adicionales + t.vlr_accesorios_basico + t.vlr_accesorios_complementario + t.vlr_accesorios_opcionales + t.vlr_otros_productos, 2).ToString("N2", new CultureInfo("en-US")));

                #endregion

                #region ConsultarListaRecotizacion
                parametros.Clear();
                parametros.Add("@pFupID", Convert.ToInt32(idFup));
                List<lista_recotizacion> listaReCotizacion = ControlDatos.EjecutarStoreProcedureConParametros<lista_recotizacion>("USP_fup_SEL_Lista_Recotizacion", parametros);
                #endregion

                #region ConsultarAnexoSalcot
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@pTipoAnexo", 6);
                List<anexos> lisAnexosSalcot = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("USP_fup_SEL_Anexos", parametros);
                #endregion

                #region ConsultarComentariosSalidaCotizacion
                parametros.Clear();
                parametros.Add("@pFupID", Convert.ToInt32(idFup));
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@pTipoComentario", 1);

                List<ListaComentarios> ListaComen = ControlDatos.EjecutarStoreProcedureConParametros<ListaComentarios>("USP_fup_SEL_Comentarios", parametros);
                #endregion


                #region ConsultarAnexos
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@pTipoAnexo", 0);
                List<anexos> dataAnexos = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("USP_fup_SEL_Anexos", parametros);
                #endregion

                #region ConsultarAprobacion
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                List<fup_aprobacion_consulta> dataAprobacion = ControlDatos.EjecutarStoreProcedureConParametros<fup_aprobacion_consulta>("USP_fup_SEL_Aprobacion_cot", parametros);
                parametros.Add("@pEscomercial", 0);
                List<fup_aprobacion_consulta> dataRechazo = ControlDatos.EjecutarStoreProcedureConParametros<fup_aprobacion_consulta>("USP_fup_SEL_rechazo_cot", parametros);
                #endregion

                #region ConsultarPrecierre
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                List<ventaCierreComercial> dataPrecierre = ControlDatos.EjecutarStoreProcedureConParametros<ventaCierreComercial>("USP_fup_SEL_Cierre_ComercialN", parametros);
                #endregion

                #region EventoPTF
                int rol = (int)HttpContext.Current.Session["Rol"];
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@pRol", rol);
                List<datosCombo2> lstEventoPFT = ControlDatos.EjecutarStoreProcedureConParametros<datosCombo2>("USP_fup_SEL_evento_seg_plano_tf", parametros);
                #endregion

                #region ConsultarPTF
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                List<planos_tipo_forsa> dataPTF = ControlDatos.EjecutarStoreProcedureConParametros<planos_tipo_forsa>("USP_fup_SEL_seg_plano_tf", parametros);
                #endregion

                #region ConsultarAnexoPTF
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@pTipoAnexo", -1);
                List<anexos> dataAnexosptf = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("USP_fup_SEL_Anexos", parametros);
                #endregion

                #region ConsultarFecSol
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                List<FechaSolicitud> dataFecsol = ControlDatos.EjecutarStoreProcedureConParametros<FechaSolicitud>("USP_fup_SEL_CierreCom_Fechas", parametros);
                #endregion

                #region ConsultarParteOF
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);

                List<Orden_Fabricacion> dataOrdernes = ControlDatos.EjecutarStoreProcedureConParametros<Orden_Fabricacion>("USP_fup_SEL_OrdenesFab", parametros);

                //string sql = "SELECT pv.pv_id id, planta_forsa.planta_descripcion descripcion  " +
                //             "FROM pedido_venta AS pv INNER JOIN " +
                //             " planta_forsa AS planta_forsa ON pv.planta_id = planta_forsa.planta_id " +
                //             "WHERE (pv.pv_fup_id = " + idFup + ") " +
                //             "ORDER BY planta_forsa.planta_id ";
                //A nivel de SF
                string sql = "SELECT DISTINCT pv_id id, planta_id, " +
                             "planta_forsa.planta_descripcion descripcion, " +
                             "planta_forsa.planta_descripcion descripcionEN , " +
                             "planta_forsa.planta_descripcion descripcionPO " +
                             "FROM solicitud_facturacion AS sf " +
                             "INNER JOIN planta_forsa AS planta_forsa ON sf.sf_planprod_id = planta_forsa.planta_id " +
                             "WHERE (sf.sf_fup_id = " + idFup + ") " +
                             "ORDER BY " +
                             "planta_forsa.planta_id, pv_id, planta_descripcion";

                List<datosCombo2> dataPlantasOF = ControlDatos.EjecutarConsulta<datosCombo2>(sql, new Dictionary<string, object>());
                #endregion

                #region ConsultarRechazoCom
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@pEscomercial", 1);
                List<fup_aprobacion_consulta> dataDevComer = ControlDatos.EjecutarStoreProcedureConParametros<fup_aprobacion_consulta>("USP_fup_SEL_rechazo_cot", parametros);
                #endregion

                #region flete
                parametros.Clear();
                parametros.Add("@pFupID", Convert.ToInt32(idFup));
                parametros.Add("@pVersion", idVersion);
                List<Calculoflete> dataFlete = ControlDatos.EjecutarStoreProcedureConParametros<Calculoflete>("USP_fup_SEL_det_fletes_N", parametros);
                #endregion

                #region Consultarmodulacionfinal
                parametros.Clear();
                parametros.Add("@pFupID", Convert.ToInt32(idFup));
                parametros.Add("@pVersion", idVersion.Trim());
                parametros.Add("@pTipoSalida", 2);
                List<fup_salida_cotizacion> listamodfinal = ControlDatos.EjecutarStoreProcedureConParametros<fup_salida_cotizacion>("USP_fup_SEL_salida_cotizacionN", parametros);
                listamodfinal.ForEach(t => t.total_m2 = Math.Round(t.m2_equipo + t.m2_adicionales + t.m2_Detalle_arquitectonico, 2).ToString());
                listamodfinal.ForEach(t => t.total_valor = Math.Round(t.vlr_equipo + t.vlr_adicionales + t.vlr_Detalle_arquitectonico, 2).ToString());
                listamodfinal.ForEach(t => t.total_propuesta_com = Math.Round(t.vlr_equipo + t.vlr_adicionales + t.vlr_sis_seguridad + t.vlr_Detalle_arquitectonico + t.vlr_accesorios_adicionales + t.vlr_accesorios_basico + t.vlr_accesorios_complementario + t.vlr_accesorios_opcionales + t.vlr_otros_productos, 2).ToString());

                #endregion

                parametros.Clear();
                parametros.Add("infoGeneral", dataInfoGeneral.FirstOrDefault());
                parametros.Add("ordenFabricacion", ordenFabricacion);
                parametros.Add("infoGeneralTablas", dataTablas);
                parametros.Add("salcot", listaSalidaCot.FirstOrDefault());
                if (listaReCotizacion.Count > 0)
                    parametros.Add("listaReCotizacion", listaReCotizacion);
                if (lisAnexosSalcot.Count > 0)
                    parametros.Add("anexosalcot", lisAnexosSalcot);
                if (dataAnexos.Count > 0)
                    parametros.Add("listaAnexos", dataAnexos);
                if (dataAprobacion.Count > 0)
                    parametros.Add("varDataAprobacion", dataAprobacion.FirstOrDefault());
                if (dataRechazo.Count > 0)
                    parametros.Add("varDataRechazo", dataRechazo);
                if (dataPrecierre.Count > 0)
                    parametros.Add("listaPrecierre", dataPrecierre.FirstOrDefault());
                if (dataPTF.Count > 0)
                    parametros.Add("listaPTF", dataPTF);
                if (dataFecsol.Count > 0)
                    parametros.Add("listaFecSol", dataFecsol);
                if (dataOrdernes.Count > 0)
                    parametros.Add("listaOF", dataOrdernes);
                if (dataPlantasOF.Count > 0)
                    parametros.Add("listaPlantaOF", dataPlantasOF);
                if (dataEquipo.Count > 0)
                    parametros.Add("listaEqui", dataEquipo);
                if (dataAdicion.Count > 0)
                    parametros.Add("listaAdd", dataAdicion);
                if (dataDevComer.Count > 0)
                    parametros.Add("varDevComer", dataDevComer);
                if (ListaComen.Count > 0)
                    parametros.Add("varComentariosSC", ListaComen);
                if (dataFlete.Count > 0)
                    parametros.Add("varFlete", dataFlete.FirstOrDefault());
                if (listamodfinal.Count > 0)
                    parametros.Add("varModfinal", listamodfinal.FirstOrDefault());
                if (dataAnexosptf.Count > 0)
                    parametros.Add("varAnexPTF", dataAnexosptf);
                
                parametros.Add("varEventoPFT", lstEventoPFT);
                

                response = JsonConvert.SerializeObject(parametros);
            }

            return response;
        }


        [WebMethod(EnableSession = true)]
        public static string obtenerInfoGeneral()
        {
            string response = string.Empty;

            List<datosCombo> ClaseCotizacion = ControlDatos.EjecutarConsulta<datosCombo>(@"SELECT [clase_cot_id] as id
                                                                        ,[clase_cot_sigla] + ' - ' + [clase_cot_descripcion]  descripcion
                                                                        FROM [fup_clase_cotizacion_ent]
                                                                        where [clase_cot_activo] = 1", new Dictionary<string, object>());

            List<datosCombo> TipoNegociacion = ControlDatos.EjecutarConsulta<datosCombo>(@"SELECT [ftne_id] id
                                                                                            ,[ftne_nombre] descripcion
                                                                                            FROM [dbo].[fup_tipo_negociacion]
                                                                                            where [ftne_estado] = 1", new Dictionary<string, object>());

            List<datosCombo2> tipoVaciado = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT        fdom_CodDominio as id, fdom_Descripcion as descripcion
                                                                                        FROM            fup_Dominios
                                                                                        WHERE        (fdom_Dominio = 'TIPO_VACIADO')
                                                                                        ORDER BY fdom_OrdenDominio", new Dictionary<string, object>());

            List<datosCombo2> sistemaSeguridad = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT        fdom_CodDominio as id, fdom_Descripcion as descripcion
                                                                                        FROM            fup_Dominios
                                                                                        WHERE        (fdom_Dominio = 'SIS_SEGURIDAD')
                                                                                        ORDER BY fdom_OrdenDominio", new Dictionary<string, object>());

            List<datosCombo2> alinVertical = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT        fdom_CodDominio as id, fdom_Descripcion as descripcion
                                                                                        FROM            fup_Dominios
                                                                                        WHERE        (fdom_Dominio = 'ALIN_VERTICAL')
                                                                                        ORDER BY fdom_OrdenDominio", new Dictionary<string, object>());

            List<datosCombo2> TipoTMFachada = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT        fdom_CodDominio as id, fdom_Descripcion as descripcion
                                                                                        FROM            fup_Dominios
                                                                                        WHERE        (fdom_Dominio = 'TIPO_FM_FACHADA')
                                                                                        ORDER BY fdom_OrdenDominio", new Dictionary<string, object>());

            List<datosCombo2> tipoUnion = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT        fdom_CodDominio as id, fdom_Descripcion as descripcion
                                                                                        FROM            fup_Dominios
                                                                                        WHERE        (fdom_Dominio = 'TIPO_UNION')
                                                                                        ORDER BY fdom_OrdenDominio", new Dictionary<string, object>());

            List<datosCombo2> detUnion = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT        fdom_CodDominio as id, fdom_Descripcion as descripcion
                                                                                        FROM            fup_Dominios
                                                                                        WHERE        (fdom_Dominio = 'DET_UNION')
                                                                                        ORDER BY fdom_OrdenDominio", new Dictionary<string, object>());

            List<datosCombo2> formaConstruccion = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT        fdom_CodDominio as id, fdom_Descripcion as descripcion
                                                                                        FROM            fup_Dominios
                                                                                        WHERE        (fdom_Dominio = 'FORMA_CONSTRUCCION')
                                                                                        ORDER BY fdom_OrdenDominio", new Dictionary<string, object>());

            List<datosCombo2> terminoNegociacion = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT        fdom_CodDominio as id, fdom_Descripcion as descripcion
                                                                                        FROM            fup_Dominios
                                                                                        WHERE        (fdom_Dominio = 'INCOTERMS')
                                                                                        ORDER BY fdom_OrdenDominio", new Dictionary<string, object>());

            List<datosCombo2> TipoAnexo = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT [tan_id] as id, [tan_desc_esp] as descripcion
                                                                                        FROM            fup_tipo_anexo                                                                                        
                                                                                        ORDER BY tan_id ", new Dictionary<string, object>());

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
                listatax = TipoAnexo
            };

            response = JsonConvert.SerializeObject(query);

            return response;
        }

        [WebMethod(EnableSession = true)]
        public static string obtenerInfoGeneralPorNegociacion(int tipoNegociacion)
        {
            string response = string.Empty;

            Dictionary<string, object> paramCotizacion = new Dictionary<string, object>() { { "@neg", tipoNegociacion } };

            List<datosCombo> TipoCotizacion = ControlDatos.EjecutarConsulta<datosCombo>(@"SELECT   ftco_id id, ftco_nombre descripcion
                                                                                          FROM [dbo].[fup_tipo_cotizacion]
                                                                                          WHERE ftco_uso_fup = 1 and ftco_grupo_negociacion IN (SELECT ftne_grupo_negociacion
                                                                                                                                                FROM [dbo].[fup_tipo_negociacion]
                                                                                                                                                where ftne_id = @neg)", paramCotizacion);

            List<datosCombo> TipoProducto = ControlDatos.EjecutarConsulta<datosCombo>(@"SELECT [fup_tipo_venta_proy_id] id
                                                                                              ,[descripcion] descripcion
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
                else{
                    pEvento = 4;     //Notificar Devolucion
                }
            }
            response = CorreoFUP(item.IdFUP, item.Version, pEvento);
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

            parametros.Clear();
            if (dataAprobacion.Count > 0)
                parametros.Add("varDataAprobacion", dataAprobacion.FirstOrDefault());

            if (dataRechazo.Count > 0)
                parametros.Add("varDataRechazo", dataRechazo);

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
        public static string obtenerVersionPorOrdenFabricacion(string idOrdenFabricacion)
        {
            ControlFUP controlFup = new ControlFUP();
            string response = string.Empty;
            List<VersionFup> lstVersion = controlFup.obtenerFUPporOrdenFabricacion(idOrdenFabricacion);
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

            //string sql = "SELECT pv.pv_id id, planta_forsa.planta_descripcion descripcion  " +
            //             "FROM pedido_venta AS pv INNER JOIN " +
            //             " planta_forsa AS planta_forsa ON pv.planta_id = planta_forsa.planta_id " +
            //             "WHERE (pv.pv_fup_id = " + @pFupID + ") " +
            //             "ORDER BY planta_forsa.planta_id ";

            //A nivel de SF
            string sql = "SELECT DISTINCT pv_id id, planta_id, " +
                        "planta_forsa.planta_descripcion descripcion, " +
                        "planta_forsa.planta_descripcion descripcionEN , " +
                        "planta_forsa.planta_descripcion descripcionPO " +
                        "FROM solicitud_facturacion AS sf " +
                        "INNER JOIN planta_forsa AS planta_forsa ON sf.sf_planprod_id = planta_forsa.planta_id " +
                        "WHERE (sf.sf_fup_id = " + @pFupID + ") " +
                        "ORDER BY " +
                        "planta_forsa.planta_id, pv_id, planta_descripcion";

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

        [WebMethod(EnableSession = true)]
        public static string guardarOrdenFabricacion(string idFup, string idVersion, string FabricadoEn, string idParte, string sf_id)
        {
            ControlFUP controlFup = new ControlFUP();
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];

            parametros.Add("@pFupID", Convert.ToInt32(idFup));
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@pUsuario", NombreUsu);
            parametros.Add("@pFabricadoEn", FabricadoEn);
            parametros.Add("@pParte", Convert.ToInt32(idParte));
            parametros.Add("@sf_id", Convert.ToInt32(sf_id));

            int ok = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_INS_Orden_Fab", parametros);
            string response = string.Empty;

            // Notificar Creacion Orden Fabricacion
            if (ok != -1)
                response = CorreoFUP(Convert.ToInt32(idFup), idVersion, 10);
            else
                response = "KO";
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


        [WebMethod(EnableSession = true)]
        public static string GuardarFechaSolicitud(string idFup, string idVersion, string FechafirmaContrato, string Fechacontractual, string FechaFormalizaPago, string FechaPlazo)
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

            var objeto = new
            {
                Rol = rol.ToString(),
                username = Nombre
            };

            string response = JsonConvert.SerializeObject(objeto);
            return response;
        }

        [WebMethod(EnableSession = true)]
        public static string CorreoFUP(int fup, string version, int pEvento)
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
            clienteWeb.Credentials = new System.Net.NetworkCredential("forsa", "F0rs4*", "FORSA");
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
            mail.Body = MensajeMail;
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
                        enlace = @"http://si.forsa.com.co:81/reportServer?" + EnlaceAnexo.Trim() + "&rs:format=PDF&rs:command=render&rs:ClearSession=true&pFupID=" + fup.ToString() + "" +
                                "&pVersion=" + version;
                    }
                    else
                    {
                        if (Evento == 9 || Evento == 23 || Evento == 24 || Evento == 25)
                        {
                            tipoAdjunto = "SF";
                            parte = Convert.ToInt32(HttpContext.Current.Session["Parte"]);
                            sfId = HttpContext.Current.Session["SfId"].ToString();

                            enlace = @"http://si.forsa.com.co:81/reportServer?" + EnlaceAnexo.Trim() + "&rs:format=PDF&rs:command=render&rs:ClearSession=true&numfup=" + fup.ToString() + "" +
                                "&version=" + version + "&parte=" + parte.ToString() + "&sf_id=" + sfId;
                        }
                        else
                        {
                            if (Evento == 16 || Evento == 15)
                            {
                                tipoAdjunto = "SF";
                                parte = Convert.ToInt32(HttpContext.Current.Session["Parte"]);
                                sfId = HttpContext.Current.Session["SfId"].ToString();

                                enlace = @"http://si.forsa.com.co:81/reportServer?" + EnlaceAnexo.Trim() + "&rs:format=PDF&rs:command=render&rs:ClearSession=true&numfup=" + fup + "" +
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
            smtp.Port = 25;
            smtp.EnableSsl = true;
            //smtp.Timeout = 400;

            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain,
                SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
            try
            {
                // smtp.SendAsync(mail, mail.To);
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                string mensaje = "ERROR: " + ex.Message;
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

            response = JsonConvert.SerializeObject(data);
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
            parametros.Add("@leadTime",flete.leadTime);
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
            parametros.Add("@pUsu_crea",NombreUsu);

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
            if (estado == "Cotizado") { pEvento = 46;}
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

        [WebMethod(EnableSession = true)]
        public static string guardarPlanoTipoForsa(string idFup, string idVersion, PTF_guardar planPTF)
        {
            ControlFUP controlFup = new ControlFUP();
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];

            parametros.Add("@pFupID", Convert.ToInt32(idFup));
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@pUsuario", NombreUsu);
            parametros.Add("@pResponsable_id", Convert.ToInt32(planPTF.Responsable));
            parametros.Add("@pEvento_id", planPTF.Evento);
            parametros.Add("@pObservacion", planPTF.Observacion);
            parametros.Add("@pEnviado", 0);
            parametros.Add("@pFecCierre", planPTF.FechaCierre ?? (DateTime)Convert.ToDateTime("1900-01-01"));
            parametros.Add("@ptipo_plano_id", planPTF.Plano ?? 0);
            int ok = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_seg_plano_tf_N", parametros);
            string response = string.Empty;
            if (ok != -1)
            {
                // Notificar Solicitud Plano tipo Forsa
                if ((Convert.ToInt32(planPTF.Evento) == 1) || (Convert.ToInt32(planPTF.Evento) == 8))
                    response = CorreoFUP(Convert.ToInt32(idFup), idVersion, 7);
                else
                    response = "OK";
            }
            else
            {
                response = "KO";
            }
            return response;

        }
    
    }
}