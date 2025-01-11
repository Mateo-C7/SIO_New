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
	public partial class FormFUP : System.Web.UI.Page
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
			string IdClienteUsuario ="0";

			if (HttpContext.Current.Session["IdClienteUsuario"] != null) {
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
			List<CapaControl.Entity.EstadoSocioEconomico> lstEstrato = controlObra.obtenerEstadoSocioEconomico();
			List<Dominios> lstTipVivienda = ControlDominios.obtener_dominios(CapaControl.Enumeradores.Dominios.TIPO_VIVIENDA.ToString());
			List<Dominios> lstEscaleraCotRapida = ControlDominios.obtener_dominios(CapaControl.Enumeradores.Dominios.ESCALERA_COT_RAPIDA.ToString());
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
                rol == 3 || rol == 9 || rol == 31 || rol == 40 || rol == 54
                )
            {
                listaEstadoDtf = listaEstadoDtf.Where(x => (x.descripcion == "Pend SCI" || x.descripcion == "Solicitud Aval Tecnico")).ToList();
            }

            List<datosCombo2> listaSubprDtf = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT        fdom_CodDominio as id, fdom_Descripcion as descripcion , fdom_DescripcionEN descripcionEN , fdom_DescripcionPO descripcionPO
																						FROM            fup_Dominios
																						WHERE        (fdom_Dominio = 'SUBPROCESODFT')
																						ORDER BY fdom_OrdenDominio", new Dictionary<string, object>());

			queryResult.Add("varEscaleraCotRapida", lstEscaleraCotRapida);
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
            queryResult.Add("varSubpDFT", listaSubprDtf);

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
                if (item.pItemparte_id != null)
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
			}
			string response = JsonConvert.SerializeObject(id.ToString());
			return response;
		}

		[WebMethod]
		public static string InsertarEncabezadoCotizacionRapida(Cot_Rap_Enc_Insertar fila)
        {
			string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];
			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("@pFupId", fila.IdFup);
			parametros.Add("@pFupVersion", fila.VersionFup);
			parametros.Add("@pTipoObra", fila.IdTipoObra);
			parametros.Add("@pTipoEscalera", fila.IdTipoEscalera);
			parametros.Add("@pModulaciones", fila.NroModulaciones);
			parametros.Add("@pCambios", fila.NroCambios);
			parametros.Add("@pAreaM2", fila.AreaM2);
			parametros.Add("@pUsuario", NombreUsu);

			List<Cot_Rap_Enc_Consultar> encabezados = ControlDatos.EjecutarStoreProcedureConParametros<Cot_Rap_Enc_Consultar>("USP_fup_INS_Cotrap_Enc", parametros);
			return JsonConvert.SerializeObject(encabezados);
		}

        [WebMethod]
        public static void EntregarCotrapCliente(int encabezado_id, int fup_id, string fup_version)
        {
            string query = "UPDATE fup_CotizacionRapida_enc SET fcr_EntregadoCliente = 1 WHERE fcr_id = " + encabezado_id.ToString() + ";";
            query += "UPDATE fup_enc_entrada_cotizacion SET eect_estado_proc = 5 WHERE eect_fup_id = " + fup_id.ToString() + " AND eect_vercot_id = '" + fup_version + "';";
            ControlDatos.EjecutarConsulta<RespuestaFilasAfectadas>(query, new Dictionary<string, object>());
        }

		[WebMethod]
		public static string ObtenerEncabezadoCotizacionRapida(int FupId, string FupVersion)
        {
			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("@pFupId", FupId);
			parametros.Add("@pVersion", FupVersion);
			List<Cot_Rap_Enc_Consultar> encabezados = ControlDatos.EjecutarStoreProcedureConParametros<Cot_Rap_Enc_Consultar>("USP_fup_SEL_Cotrap_Enc", parametros);
			return JsonConvert.SerializeObject(encabezados);
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
		public static string GuardarFUP(fup_guardar fup, int Origen)
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
            parametros.Add("@pobra_link", fup.obra_link);
            parametros.Add("@pObra_FecInicio", string.IsNullOrEmpty(fup.Obra_FecInicio) ? (DateTime?)null : Convert.ToDateTime(fup.Obra_FecInicio));
            parametros.Add("@pVendedorZona", fup.VendedorZona);
            parametros.Add("@pRecomendacionTecnico", fup.RecomendacionTecnico);
			parametros.Add("@pFupRefServicios", fup.FupRefServicios);

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
                Dictionary<string, object> parametros_tablas = new Dictionary<string, object>();

                foreach (fup_tablas tabla in fup.datos_tablas)
				{
                    parametros_eliminar.Clear();
                    parametros_eliminar.Add("@pFupID", id);
                    parametros_eliminar.Add("@pVersion", idVersion);
                    parametros_eliminar.Add("@ptipoTabla", tabla.tipo_tabla);
                    ControlDatos.GuardarStoreProcedureConParametros("USP_fup_DEL_enc_ent_cot_tabla", parametros_eliminar);

				}

                foreach (fup_tablas tabla in fup.datos_tablas)
                {
                    parametros_tablas.Clear();
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

                if (Origen == 1)
                {
                    response = CorreoFUP(id, fup.Version, 107);
					if(fup.TipoNegociacion == 7)
                    {
						int evento_servicios = 0;
						if (fup.TipoCotizacion == 17) evento_servicios = 111;
						if (fup.TipoCotizacion == 6) evento_servicios = 112;
						if (fup.TipoCotizacion == 18) evento_servicios = 113;
						if (fup.TipoCotizacion == 19) evento_servicios = 114;
						if (fup.TipoCotizacion == 20) evento_servicios = 115;
						CorreoFUP(id, fup.Version, evento_servicios);
					}
                }

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
		public static bool AutorizarSubirPlanos(int idFup, string fupVersion)
        {
			string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];
			string sql = @"UPDATE dbo.fup_enc_entrada_cotizacion SET eect_AutorizaSubirPlanos = 1, 
				eect_UsuAutorizaSubirPlanos = '" + NombreUsu + "' WHERE eect_fup_id = " + idFup.ToString() + " " +
				"AND eect_vercot_id = '" + fupVersion.Trim() + "'";
			try
            {
				ControlDatos.EjecutarConsulta<RespuestaFilasAfectadas>(sql, new Dictionary<string, object>());
                CorreoFUP(idFup, fupVersion, 108);
                return true;
            } catch (Exception e)
            {
				Console.WriteLine(e.Message);
				return false;
            }
        }

		[WebMethod]
		public static string obtenerInformacionPorFupVersion(string idFup, string idVersion, string idioma)
		{
			string response = string.Empty, ordenFabricacion = string.Empty, ordenCotizacion= string.Empty, ordenCI = string.Empty;

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
                ordenCI = controlFup.obtenerOrdenCotizacionFUP(Convert.ToInt32(idFup), idVersion,"CI");
                #endregion

                #region ConsultarTablasInformacionGeneral
                parametros.Clear();
				parametros.Add("@pFupID", Convert.ToInt32(idFup));
				parametros.Add("@pVersion", idVersion);
				List<fup_tablas> dataTablas = ControlDatos.EjecutarStoreProcedureConParametros<fup_tablas>("USP_fup_SEL_enc_ent_cot_tabla", parametros);
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
				listaSalidaCot.ForEach(t => t.total_m2 = Math.Round(t.m2_equipo + t.m2_adicionales + t.m2_Detalle_arquitectonico, 2).ToString());
				listaSalidaCot.ForEach(t => t.total_valor = Math.Round(t.vlr_equipo + t.vlr_adicionales + t.vlr_Detalle_arquitectonico, 2).ToString());
				listaSalidaCot.ForEach(t => t.total_propuesta_com = Math.Round(t.vlr_equipo + t.vlr_adicionales + t.vlr_sis_seguridad + t.vlr_Detalle_arquitectonico + t.vlr_accesorios_basico + t.vlr_accesorios_complementario + t.vlr_accesorios_opcionales + t.vlr_accesorios_adicionales +  t.vlr_otros_productos, 2).ToString());
                #endregion

                #region OrganizarDatosPrecioSugerido
                /* Estas variables son utilizadas en FormSalidaCotizacion */
                Dictionary<string, string> valuesSalidaCotizacion = new Dictionary<string, string>();
                if (listaSalidaCot.Count > 0) {
                    valuesSalidaCotizacion.Add("total_m2", listaSalidaCot.FirstOrDefault().total_m2);
                }
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

                #region ConsultarAnexoActaPostventa
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@pTipoAnexo", 38);
                List<anexos> lisAnexosActaPOstventa = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("USP_fup_SEL_Anexos", parametros);
                #endregion

                #region ConsultarValidacionDeEquipo
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@pTipoAnexo", 39);
                List<anexos> lisAnexosValidacionEquipo = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("USP_fup_SEL_Anexos", parametros);
                #endregion

                #region ConsultarAnexoFichaPostventa
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@pTipoAnexo", 37);
                List<anexos> lisAnexosActaPreviaDespacho = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("USP_fup_SEL_Anexos", parametros);
                #endregion

                #region ConsultarComentariosSalidaCotizacion
                parametros.Clear();
				parametros.Add("@pFupID", Convert.ToInt32(idFup));
				parametros.Add("@pVersion", idVersion);
				parametros.Add("@pTipoComentario", 1);

				List<ListaComentarios> ListaComen = ControlDatos.EjecutarStoreProcedureConParametros<ListaComentarios>("USP_fup_SEL_Comentarios", parametros);
                #endregion

                #region ConsultarLinksSalidaCotizacion
                parametros.Clear();
                parametros.Add("@pFupID", Convert.ToInt32(idFup));
                parametros.Add("@pVersion", idVersion);

                List<LinksQuery> ListaLinks = ControlDatos.EjecutarStoreProcedureConParametros<LinksQuery>("USP_fup_SEL_salida_cot_tabla", parametros);
                #endregion


                #region ConsultarAnexos
                parametros.Clear();
				parametros.Add("@pFupID", double.Parse(idFup));
				parametros.Add("@pVersion", idVersion);
				parametros.Add("@pTipoAnexo", 0);
				List<anexos> dataAnexos = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("USP_fup_SEL_Anexos", parametros);
				#endregion

				#region ConsultarAnexoSalfinal
				parametros.Clear();
				parametros.Add("@pFupID", double.Parse(idFup));
				parametros.Add("@pVersion", idVersion);
				parametros.Add("@pTipoAnexo", 10);
				List<anexos> lisAnexosSalfin = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("USP_fup_SEL_Anexos", parametros);
				#endregion

				#region ConsultarAnexoDocumentosFacturacion
				parametros.Clear();
				parametros.Add("@pFupID", double.Parse(idFup));
				parametros.Add("@pVersion", idVersion);
				parametros.Add("@pTipoAnexo", 11);
				List<anexos> lisAnexosDocFac = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("USP_fup_SEL_Anexos", parametros);
				#endregion

				#region ConsultarAprobacion
				parametros.Clear();
				parametros.Add("@pFupID", double.Parse(idFup));
				parametros.Add("@pVersion", idVersion);
				List<fup_aprobacion_consulta> dataAprobacion = ControlDatos.EjecutarStoreProcedureConParametros<fup_aprobacion_consulta>("USP_fup_SEL_Aprobacion_cot", parametros);
				parametros.Add("@pEscomercial", 0);
				List<fup_aprobacion_consulta> dataRechazo = ControlDatos.EjecutarStoreProcedureConParametros<fup_aprobacion_consulta>("USP_fup_SEL_rechazo_cot", parametros);
                #endregion

                #region ConsultarFecSeg
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                List<fup_segcot_consulta> dataFecSeg = ControlDatos.EjecutarStoreProcedureConParametros<fup_segcot_consulta>("USP_fup_SEL_Fec_Seguimiento", parametros);
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

				//A nivel de pedido_Venta
				//string sql = "SELECT pv.pv_id id, planta_forsa.planta_descripcion descripcion , planta_forsa.planta_descripcion descripcionEN , planta_forsa.planta_descripcion descripcionPO " +
				//			 "FROM pedido_venta AS pv INNER JOIN " +
				//			 " planta_forsa AS planta_forsa ON pv.planta_id = planta_forsa.planta_id " +
				//			 "WHERE (pv.pv_fup_id = " + idFup + ") " +
				//			 "ORDER BY planta_forsa.planta_id ";

				//A nivel de SF
				string sql = "SELECT DISTINCT pv_id id, planta_id, " +
							 "planta_forsa.planta_descripcion descripcion, " +
							 "planta_forsa.planta_descripcion descripcionEN , " +
							 "planta_forsa.planta_descripcion descripcionPO " +
							 "FROM solicitud_facturacion AS sf " +
							 "INNER JOIN planta_forsa AS planta_forsa ON sf.sf_planprod_id = planta_forsa.planta_id " +
							 "WHERE (sf.sf_fup_id = "+idFup+") " +
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
				listamodfinal.ForEach(t => t.total_propuesta_com = Math.Round(t.vlr_equipo + t.vlr_adicionales + t.vlr_sis_seguridad + t.vlr_Detalle_arquitectonico  + t.vlr_accesorios_basico + t.vlr_accesorios_complementario + t.vlr_accesorios_opcionales + t.vlr_otros_productos, 2).ToString());

				#endregion

				#region ConsultarTablasOrdenCliente
				parametros.Clear();
				parametros.Add("@pFupID", Convert.ToInt32(idFup));
				parametros.Add("@pVersion", idVersion);
				List<fup_tablas2> dataOrdenCli = ControlDatos.EjecutarStoreProcedureConParametros<fup_tablas2>("USP_fup_SEL_enc_ent_OrdenCliente", parametros);
                #endregion

                #region ConsultarAnexoCondicionpago
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@pTipoAnexo", -1);
                parametros.Add("@pGrupoSeg", 3);
                
                List<anexos> lisAnexoscondpago = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("USP_fup_SEL_Anexos", parametros);
                #endregion

                #region ConsultarAnexoDocumentosCierre
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@pTipoAnexo", -1);
                parametros.Add("@pGrupoSeg", 4);

                List<anexos> lisAnexosdocierre = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("USP_fup_SEL_Anexos", parametros);
                #endregion

                #region ConsultarAnexoFinal
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@pTipoAnexo", 25);
                parametros.Add("@pGrupoSeg", -1);

                List<anexos> lisAnexosdofinal = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("USP_fup_SEL_Anexos", parametros);
                #endregion

                #region ConsultarAnexosActaDefinicionTecnica
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@pTipoAnexo", 33);
                parametros.Add("@pGrupoSeg", -1);

                List<anexos> lisAnexosActaDefinicionTecnica = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("USP_fup_SEL_Anexos", parametros);
                #endregion

                #region ConsultarAnexosMesaTecnica
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@pTipoAnexo", 34);
                parametros.Add("@pGrupoSeg", -1);

                List<anexos> lisAnexosMesaTecnica = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("USP_fup_SEL_Anexos", parametros);
                #endregion

                #region ConsultarAnexosPreviaTecnica
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@pTipoAnexo", 36);
                parametros.Add("@pGrupoSeg", -1);

                List<anexos> lisAnexosMesaTecnicaPrev = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("USP_fup_SEL_Anexos", parametros);
                #endregion

                #region ConsultarAnexosPreventa
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@pTipoAnexo", 41);
                parametros.Add("@pGrupoSeg", -1);

                List<anexos> lisAnexosMesaPreventa = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("USP_fup_SEL_Anexos", parametros);
                #endregion

                #region ConsultarAnexosListasEmpaque
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@pTipoAnexo", 32);
                parametros.Add("@pGrupoSeg", -1);

                List<anexos> lisAnexosListasEmpaque = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("USP_fup_SEL_Anexos", parametros);
                #endregion

                #region ConsultarAnexoArmado
                parametros.Clear();
                parametros.Add("@pFupID", double.Parse(idFup));
                parametros.Add("@pVersion", idVersion);
                parametros.Add("@pTipoAnexo", -1);
                parametros.Add("@pGrupoSeg", 5);

                List<anexos> lisAnexosArm = ControlDatos.EjecutarStoreProcedureConParametros<anexos>("USP_fup_SEL_Anexos", parametros);
                #endregion

                #region ConsultarAvales
                parametros.Clear();
                parametros.Add("@pFupID", idFup);
                parametros.Add("@pVersion", idVersion);
                List<AvalesFabricacionDespliega> lisAvales = ControlDatos.EjecutarStoreProcedureConParametros<AvalesFabricacionDespliega>("USP_fup_SEL_enc_ent_AvalesFabricacion", parametros);
                #endregion

                #region ConsultarCntrlCm
                parametros.Clear();
                parametros.Add("@pFupID", idFup);
                parametros.Add("@pVersion", idVersion);
                List<fup_cntrcam_consulta> lisCntrCmb = ControlDatos.EjecutarStoreProcedureConParametros<fup_cntrcam_consulta>("USP_fup_SEL_Enc_Ent_ControlCambio", parametros);
                #endregion

                #region ConsultarSolicitudesCartaManual
                parametros.Clear();
                parametros.Add("@pFupID", idFup);
                parametros.Add("@pVersion", idVersion);
                List<SolicitudCartaManual_Consulta> listSolCartaManual = ControlDatos.EjecutarStoreProcedureConParametros<SolicitudCartaManual_Consulta>("USP_fup_SEL_SolicitudCartaManual", parametros);
                #endregion

                #region ConsultarPrecioMinimo
                parametros.Clear();
                parametros.Add("@pFupID", Convert.ToInt32(idFup));
                parametros.Add("@pVersion", idVersion.Trim());
                parametros.Add("@Idioma", "ES");
                parametros.Add("@pImprime", 99);
                List<CotizacionFUP> listaMinimo = ControlDatos.EjecutarStoreProcedureConParametros<CotizacionFUP>("USP_fup_SEL_CartaCierre_partes", parametros);

                #endregion

                parametros.Clear();
				parametros.Add("infoGeneral", dataInfoGeneral.FirstOrDefault());
				parametros.Add("ordenFabricacion", ordenFabricacion);
				parametros.Add("ordenCotizacion", ordenCotizacion);
                parametros.Add("ordenCI", ordenCI);
                parametros.Add("infoGeneralTablas", dataTablas);
				parametros.Add("salcot", listaSalidaCot.FirstOrDefault());
				if (listaReCotizacion.Count > 0)
					parametros.Add("listaReCotizacion", listaReCotizacion);
				if (lisAnexosSalcot.Count > 0)
					parametros.Add("anexosalcot", lisAnexosSalcot);
                if (listamodfinal.Count > 0)
                    parametros.Add("varModfinal", listamodfinal.FirstOrDefault());
                if (lisAnexosSalfin.Count > 0)
					parametros.Add("anexosalfin", lisAnexosSalfin);
				if (lisAnexosDocFac.Count > 0)
					parametros.Add("anexoDocFac", lisAnexosDocFac);                
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
                if (ListaLinks.Count > 0)
                    parametros.Add("varLinksSC", ListaLinks);
                if (ListaComen.Count > 0)
					parametros.Add("varComentariosSC", ListaComen);
				if (dataFlete.Count > 0)
					parametros.Add("varFlete", dataFlete.FirstOrDefault());
				if (dataAnexosptf.Count > 0)
					parametros.Add("varAnexPTF", dataAnexosptf);
				if (dataOrdenCli.Count > 0)
					parametros.Add("varOrdenCliente", dataOrdenCli);
                if (lisAnexosdofinal.Count> 0)
                    parametros.Add("varAnexoFinal", lisAnexosdofinal);
                if (lisAnexosActaDefinicionTecnica.Count > 0)
                    parametros.Add("varAnexosActaDefinicionTecnica", lisAnexosActaDefinicionTecnica);
                if (lisAnexosMesaTecnica.Count > 0)
                    parametros.Add("varAnexosMesaTecnica", lisAnexosMesaTecnica);
                if (lisAnexosListasEmpaque.Count > 0)
                    parametros.Add("varAnexosListasEmpaque", lisAnexosListasEmpaque);
                if (lisAnexoscondpago.Count > 0)
                    parametros.Add("varAnexoCondPago", lisAnexoscondpago);
                if (lisAnexosdocierre.Count > 0)
                    parametros.Add("varAnexoDocumentosCierre", lisAnexosdocierre);
                if (lisAvales.Count > 0)
                    parametros.Add("varAvales", lisAvales);
                if (dataFecSeg.Count > 0)
                    parametros.Add("varFecSeg", dataFecSeg.FirstOrDefault());
                if (lisCntrCmb.Count > 0)
                    parametros.Add("varControlCambio", lisCntrCmb);
                if (lisAnexosArm.Count > 0)
                    parametros.Add("varArmado", lisAnexosArm);
                if (lisAnexosActaPOstventa.Count > 0)
                    parametros.Add("anexoActaPostventa", lisAnexosActaPOstventa);
                if (lisAnexosActaPreviaDespacho.Count > 0)
                    parametros.Add("anexosActaPreviaDespacho", lisAnexosActaPreviaDespacho);
                if (lisAnexosMesaTecnicaPrev.Count > 0)
                    parametros.Add("anexosMesaPreviaDespacho", lisAnexosMesaTecnicaPrev);
                if (lisAnexosValidacionEquipo.Count > 0)
                    parametros.Add("anexosValidacionDeEquipo", lisAnexosValidacionEquipo);
                if (listaMinimo.Count > 0)
                    parametros.Add("PrecioMinimo", listaMinimo);
                if (lisAnexosMesaPreventa.Count > 0)
                    parametros.Add("anexosMesaPreventa", lisAnexosMesaPreventa);

                parametros.Add("varSolCartaManual", listSolCartaManual);
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

            #region PoliticaCot
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            List<PoliticaCot> PoliticaCot = ControlDatos.EjecutarStoreProcedureConParametros<PoliticaCot>("USP_fup_SEL_TempoPoliticaCot", parametros);
            #endregion

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
                listacondpago = CondicionesPago,
                ListaPolitica = PoliticaCot
            };

			response = JsonConvert.SerializeObject(query);

			return response;
		}

		[WebMethod(EnableSession = true)]
		public static string obtenerInfoGeneralPorNegociacion(int tipoNegociacion)
		{
			string response = string.Empty;

			Dictionary<string, object> paramCotizacion = new Dictionary<string, object>() { { "@neg", tipoNegociacion } };

			List<datosCombo3> TipoCotizacion = ControlDatos.EjecutarConsulta<datosCombo3>(@"SELECT   ftco_id id, ftco_nombre descripcion, ftco_nombre descripcionEN, ftco_nombre descripcionPO, ftco_Uso_Alcance UsoAlcance
																						  FROM [dbo].[fup_tipo_cotizacion]
																						  WHERE ftco_estado = 1 and ftco_uso_fup = 1 
																							and ftco_grupo_negociacion IN (SELECT ftne_grupo_negociacion FROM [dbo].[fup_tipo_negociacion] where ftne_id = @neg) Order by ftco_grupo_orden_id", paramCotizacion);
			List<datosCombo2> TipoProducto = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT [fup_tipo_venta_proy_id] id ,[descripcion] descripcion ,[descripcion] descripcionEN, [descripcion] descripcionPO
																						  FROM .[dbo].[fup_tipo_venta_proyecto]
																						  where [activo] = 1  and t.Grupo_Negociacion = 0 ", paramCotizacion);

			var query = new
			{
				listatcot = TipoCotizacion,
				listaprod = TipoProducto
			};

			response = JsonConvert.SerializeObject(query);

			return response;
		}

		[WebMethod(EnableSession = true)]
		public static string obtenerInfoGeneralProducto(int tipoNegociacion, int tipoCotizacion)
		{
			string response = string.Empty;

			Dictionary<string, object> paramCotizacion = new Dictionary<string, object>() { { "@neg", tipoNegociacion } , { "@Cot", tipoCotizacion } };

			List<datosCombo2> TipoProducto = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT a.[fup_tipo_venta_proy_id] id ,[descripcion] descripcion ,[descripcion] descripcionEN, [descripcion] descripcionPO
																						FROM .[dbo].[fup_tipo_venta_proyecto] a  INNER JOIN fup_Tipo_VentaProy_TipoNeg B ON a.fup_tipo_venta_proy_id = b.fup_tipo_venta_proy_id 
																						WHERE a.[activo] = 1 and b.[activo]= 1 and b.fup_TipoNegociacion_id = @neg 
																						  AND CASE WHEN b.fup_TipoCotizacion_id = 0 THEN @Cot ELSE b.fup_TipoCotizacion_id END = @Cot ", paramCotizacion);

			var query = new
			{
				listaprod = TipoProducto
			};

			response = JsonConvert.SerializeObject(query);

			return response;
		}
		
		[WebMethod(EnableSession = true)]
        public static bool RegistrarCartaSolicitudManual(int idFup, string idVersion, int tipo)
        {
            try
            {
                string nombreUsuario = (string)HttpContext.Current.Session["Usuario"];
                DateTime fechaActual = DateTime.Now;
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                parametros.Add("@pFupID", idFup);
                parametros.Add("@pVersion", idVersion);
                int correoEvento = 0;
                switch (tipo)
                {
                    case 1:
                        // Caso en el que se pida la autorización
                        parametros.Add("@pFecSolicitud", fechaActual);
                        parametros.Add("@pUsuarioSolicitud", nombreUsuario);
                        correoEvento = 86;
                        break;
                    case 2:
                        // Caso en el que se autoriza la carta
                        parametros.Add("@pFecAprobacion", fechaActual);
                        parametros.Add("@pUsuarioAprobacion", nombreUsuario);
                        correoEvento = 88;
                        break;
                    case 3:
                        // Caso en el que se niega la carga
                        parametros.Add("@pFecNegacion", fechaActual);
                        parametros.Add("@pUsuarioNegacion", nombreUsuario);
                        correoEvento = 89;
                        break;
                    case 4:
                        // Caso en el que se cancela la solicitud de carta
                        parametros.Add("@pFecCancela", fechaActual);
                        parametros.Add("@pUsuarioCancel", nombreUsuario);
                        correoEvento = 87;
                        break;
                }
                ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_SolicitudCartaManual", parametros);
                CorreoFUP(idFup, idVersion, correoEvento);
            }
            catch (Exception exception)
            {
                return false;
            }
            return true;
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
			parametros.Add("@pAprobacion", item.Visto_bueno ?? -1);
			parametros.Add("@pModulaciones", item.NumeroModulaciones ?? 0);
			parametros.Add("@pCambios", item.NumeroCambios ?? 0);
			parametros.Add("@ptipo_rechazo_id", item.MotivoRechazo);
			parametros.Add("@pobservacion", item.ObservacionAprobacion);
            parametros.Add("@AlturaFormaleta", item.AlturaFormaleta);
            parametros.Add("@pNivelComplejidad", item.NivelComplejidad);
            parametros.Add("@pFecPolitica", item.FecPolitica);
            parametros.Add("@pDiasPolitica", item.DiasPolitica);
            parametros.Add("@pTipoProyectoApId", item.TipoProyectoApId);
            parametros.Add("@pProyectoEnConstruccion", item.ProyectoEnConstruccion);
            parametros.Add("@pPlanosCoordinados", item.PlanosCoordinados);
            parametros.Add("@pInicioCercano", item.InicioCercano);
            parametros.Add("@pSoloPlanos", item.SoloPlanos);
            parametros.Add("@pPlanosNoCoordinados", item.PlanosNoCoordinados);
            parametros.Add("@pSinInformacionInicio", item.SinInformacionInicio);
            parametros.Add("@pPlanosDibujo", item.PlanosDibujo);
            parametros.Add("@pVaPreventa", item.VaPreventa);

            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_Aprobacion_cot", parametros);
			//Proceso de Notificacion de Evento
			int pEvento = 0;

            if (item.VaPreventa == true && item.estado != "Cierre Comercial" 
                && (item.Visto_bueno.ToString() ==""  || item.Visto_bueno.ToString() == "-1") )
            {
                pEvento = 100; //Notificar Solicitud de Acta Previa a Despacho - 2023-08-29
            }
            else
            {
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
            }
            if (item.IdFUP != 41333)
            {
                response = CorreoFUP(item.IdFUP, item.Version, pEvento);
            }
            else { response = "OK"; }

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
            parametros.Add("@pConsideracionObservacionCliente", fupsal.ConsideracionObservacionCliente);
            //parametros.Add("@pFechaAvalCierre", fupsal.FechaAvalCierre);


            int ok = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_salida_cotizacionN", parametros);

			string response = string.Empty;
			int evento = 5;

			if (fupsal.tipoSalida == 2)
				evento = 48;

            // Notificar Salida Cotizacion
            if (ok != -1)
            {
                response = CorreoFUP(Convert.ToInt32(fupsal.fupid), fupsal.version, evento);
                if (controlFup.obtenerPaisCliente(fupsal.fupid) != 8)
                {
                    response = CorreoFUP(Convert.ToInt32(fupsal.fupid), fupsal.version, 104);  // Solicitud de Cotizar Flete
                }
            }
            else
                response = "KO";
			return response;

		}

        [WebMethod]
        public static void SolicitarCartaDFTManual(int fupId, string version)
        {
            string sqlQuery = "UPDATE dbo.fup_salida_cotizacion SET sct_FechaSolicitudDFTManual = '"
                + DateTime.Today.ToShortDateString() + "' WHERE sct_enc_entrada_cot_id = (SELECT eect_id FROM fup_enc_entrada_cotizacion WHERE "
                + "eect_fup_id = " + fupId + " AND eect_vercot_id = '" + version + "')";
            try
            {
                List<RespuestaFilasAfectadas> affectedRows = ControlDatos.EjecutarConsulta<RespuestaFilasAfectadas>(sqlQuery, new Dictionary<string, object>());
                //CorreoFUP(fupId, version, 3);
            }
            catch (Exception e) {}
        }

        [WebMethod]
        public static void CancelarSolicitudCartaDFTManual(int fupId, string version)
        {
            string sqlQuery = "UPDATE dbo.fup_salida_cotizacion SET sct_FechaSolicitudDFTManual = NULL"
                + " WHERE sct_enc_entrada_cot_id = (SELECT eect_id FROM fup_enc_entrada_cotizacion WHERE "
                + "eect_fup_id = " + fupId + " AND eect_vercot_id = '" + version + "')";
            try
            {
                List<RespuestaFilasAfectadas> affectedRows = ControlDatos.EjecutarConsulta<RespuestaFilasAfectadas>(sqlQuery, new Dictionary<string, object>());
            }
            catch (Exception e) {}
        }

        [WebMethod]
        public static void AutorizarCartaDFTManual(int fupId, string version)
        {
            string sqlQuery = "UPDATE dbo.fup_salida_cotizacion SET sct_CartaDFTManual = 1, sct_FechaAutorizacionDFTManual = '"
                + DateTime.Today.ToShortDateString() + "', sct_UsuarioAutorizaDFTManual = '" + (string)HttpContext.Current.Session["Nombre_Usuario"] + "' WHERE sct_enc_entrada_cot_id = (SELECT eect_id FROM fup_enc_entrada_cotizacion WHERE "
                + "eect_fup_id = " + fupId + " AND eect_vercot_id = '" + version + "')";
            try
            {
                List<RespuestaFilasAfectadas> affectedRows = ControlDatos.EjecutarConsulta<RespuestaFilasAfectadas>(sqlQuery, new Dictionary<string, object>());
                //CorreoFUP(fupId, version, 3);
            }
            catch (Exception e) {}
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
			//			 "FROM pedido_venta AS pv INNER JOIN " +
			//			 " planta_forsa AS planta_forsa ON pv.planta_id = planta_forsa.planta_id " +
			//			 "WHERE (pv.pv_fup_id = " + @pFupID + ") " +
			//			 "ORDER BY planta_forsa.planta_id ";
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
		public static string obtenerPartePorPv(int PedidoVenta, int Plantaid)
		{
			string response = string.Empty;

			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("@pPedidoVenta", PedidoVenta);
			parametros.Add("@pPlantaId", Plantaid);

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

		[WebMethod]
		public static string ObtenerCondicionesPago(int pFupID, string pVersion)
       
		{
			Dictionary<string, object> parametros = new Dictionary<string, object>();
			parametros.Add("@pFupID", pFupID);
			parametros.Add("@pVersion", pVersion);

			List<fup_CodPago> data = ControlDatos.EjecutarStoreProcedureConParametros<fup_CodPago>("USP_fup_SEL_enc_ent_condPago", parametros);

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
		public static string GuardarFechaSolicitudV2(FechaSolicitudGuarda item)
		{
			ControlFUP controlFup = new ControlFUP();
			Dictionary<string, object> parametros = new Dictionary<string, object>();
			string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];
			DateTime FechaContractual = Convert.ToDateTime("1900/01/01");
			if (Convert.ToDateTime(item.FechaContractual) != Convert.ToDateTime("1/1/0001 12:00:00 AM")) {
				FechaContractual = Convert.ToDateTime(item.FechaContractual);
			};
			DateTime FechaPactadaPlan = Convert.ToDateTime("1900/01/01");
			if (Convert.ToDateTime(item.FechaPactadaPlan) != Convert.ToDateTime("1/1/0001 12:00:00 AM"))
			{
				FechaPactadaPlan = Convert.ToDateTime(item.FechaPactadaPlan);
			};
			
				parametros.Add("@pFupID", Convert.ToInt32(item.IdFUP));
				parametros.Add("@pVersion", item.Version);
				parametros.Add("@pUsuario", NombreUsu);
				parametros.Add("@pFechafirmaContrato", Convert.ToDateTime(item.FechaFirmaContrato));
				parametros.Add("@pFechacontractual", FechaContractual);
				parametros.Add("@pFechaAnticipado", Convert.ToDateTime(item.FechaFormalizaPago));
				parametros.Add("@pPlazo", Convert.ToInt32(item.Plazo));
				parametros.Add("@pcieseg_dscto_n1", item.vlr_dcto_n1);
				parametros.Add("@pcieseg_dscto_n2", item.vlr_dcto_n2);
				parametros.Add("@pcieseg_dscto_n3", item.vlr_dcto_n3);
				parametros.Add("@pcieseg_dscto_n4", item.vlr_dcto_n4);
				parametros.Add("@pcieseg_dscto_n5", item.vlr_dcto_n5);
				parametros.Add("@cieseg_MetodoPago", item.MetodoPago ?? 0);
				parametros.Add("@cieseg_ObservaMayordscto", item.ObservaMayordscto ?? "");
				parametros.Add("@pfecha_planos_aprobados", Convert.ToDateTime(item.FechaAprobacionPlanos));
				parametros.Add("@pFechaPactadaPlan", FechaPactadaPlan);
                parametros.Add("@pciesegObservaFechas", item.ObservaFechas ?? "");
                parametros.Add("@pTerminoNegociacion", item.TerminoNeg ?? 0);
                parametros.Add("@pcieseg_dscto_mf_n1", item.vlr_dcto_mf_n1);
                parametros.Add("@pcieseg_dscto_mf_n2", item.vlr_dcto_mf_n2);
                parametros.Add("@pcieseg_dscto_mf_n3", item.vlr_dcto_mf_n3);
                parametros.Add("@pcieseg_dscto_mf_n4", item.vlr_dcto_mf_n4);
                parametros.Add("@pcieseg_dscto_mf_n5", item.vlr_dcto_mf_n5);
                parametros.Add("@pciesegObservaDscto", item.ObservaDscto);
                parametros.Add("@Plazo_Neg", Convert.ToInt32(item.Plazo_Neg));
                parametros.Add("@pFacturaM2Modulados", item.FacturaM2Modulados);
                parametros.Add("@pValorM2Factura", item.ValorM2Factura);

            ControlDatos.GuardarStoreProcedureConParametros("[USP_fup_UPD_Cierre_Comercial_V2]", parametros);
            // Notificar de acuerdo con el proceso Solicitado
            int pEvento = 8;
            string response;

            if ((item.vlr_dcto_mf_n1 + item.vlr_dcto_mf_n2 + item.vlr_dcto_mf_n3 + item.vlr_dcto_mf_n4 + item.vlr_dcto_mf_n5) != 0) { pEvento = 70; }

            response = CorreoFUP(Convert.ToInt32(item.IdFUP), item.Version, pEvento);
            return response;

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

        [WebMethod]
        public static string GuardadoEnvioBoletosBancarios(int pFupId, string pidVersion, List<int> Consecutivos)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            if (Consecutivos.Count > 0)
            {
                foreach (int consecutivo in Consecutivos)
                {
                    parametros.Clear();
                    parametros.Add("@pFupID", pFupId);
                    parametros.Add("@pVersion", pidVersion);
                    parametros.Add("@Consecutivo", consecutivo);
                    ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_BoletosBancariosCondPagos", parametros);
                }
                CorreoFUP(pFupId, pidVersion, 84);
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
			//response = CorreoFUP(Convert.ToInt32(idFup), idVersion, pEvento);
			return "";
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
        public static string getUserDataToForlineAPI()
        {
            TempDataClassToForline DatauserToForline = new TempDataClassToForline();
            string rcId = (string)HttpContext.Current.Session["rcID"];
            DatauserToForline.username = (string)HttpContext.Current.Session["Usuario"];
            DatauserToForline.id_user = (int)HttpContext.Current.Session["usuId"];
            DatauserToForline.id_representative = Int32.Parse(rcId);
            DatauserToForline.id_role = (int)HttpContext.Current.Session["Rol"];
            string response = JsonConvert.SerializeObject(DatauserToForline);
            return response;
        }

        [WebMethod(EnableSession = true)]
        public static string ObtenerIdUsuYIdRepresentativo()
        {
            return "";
        }

		[WebMethod(EnableSession = true)]
		public static string ObtenerRol()
		{
			int rol = (int)HttpContext.Current.Session["Rol"];
			string Nombre = (string)HttpContext.Current.Session["Usuario"];
			int CreaOF = Convert.ToInt32(HttpContext.Current.Session["CreaOf"]);
            int UserId = (int)HttpContext.Current.Session["usuId"];
            int ModificaPlazo = (int)HttpContext.Current.Session["ModificaPlazo"];

            var objeto = new
			{
				Rol = rol.ToString(),
				username = Nombre,
				CreaOF = CreaOF,
                UserId = UserId,
                ModificaPlazo = ModificaPlazo
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
			if ((IdFUP != "-1") &&(IdFUP != ""))
				response = JsonConvert.SerializeObject(Convert.ToInt32(IdFUP));
			return response;
		}

        [WebMethod(EnableSession = true)]
        public static string ObtenerSimuladorProyectoResumen(int fup, string version, string TipoOf = "CT")
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pFupID", fup);
            parametros.Add("@pversion", version.Trim());
            parametros.Add("@pTipoOf", TipoOf);

            List<SimuladorProyecto_Resumen> data = ControlDatos.EjecutarStoreProcedureConParametros<SimuladorProyecto_Resumen>("USP_SimuladorProyecto_Resumen", parametros);
            return JsonConvert.SerializeObject(data);
        }

        [WebMethod(EnableSession = true)]
        public static string SolicitarSimulacion(int fupId, string version)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string response = string.Empty;

            parametros.Add("@pFupId", fupId);
            parametros.Add("@pVersion", version.Trim());
            parametros.Add("@pUsuarioSolicitud", (string)HttpContext.Current.Session["Nombre_Usuario"]);
            ControlDatos.EjecutarStoreProcedureConParametros<RespuestaFilasAfectadas>("USP_fup_UPD_SolicitudSimulacion", parametros);
            response = CorreoFUP(fupId, version, 90);
            return response;
        }

        [WebMethod(EnableSession = true)]
		public static string CorreoFUP(int fup, string version, int pEvento, string mensajeAdd ="")
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
			mail.Body = "  "+mensajeAdd + "  "  + System.Environment.NewLine + MensajeMail  ;
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
					if ((Evento == 2) || (Evento == 4) || (Evento == 5) || (Evento == 7) || (Evento == 109))
					{
						tipoAdjunto = "FUP";
						enlace = @"http://si.forsa.com.co:81/reportServer?" + EnlaceAnexo.Trim() + "&rs:format=PDF&rs:command=render&rs:ClearSession=true&pFupID=" + fup.ToString() + "" +
								"&pVersion=" + version;
                        if (Evento == 109) { enlace = enlace + "&pIdioma=" + mensajeAdd.ToUpper(); }

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
			// smtp.Port = 25;
            smtp.Port = 587;
            smtp.EnableSsl = true;
			//smtp.Timeout = 400;

			ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain,
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
		[WebMethod(EnableSession = true)]
		public static string CalcularFlete(string idFup, string idVersion, int idPtoCargue, int idPtoDescargue, int idTerminoNegociacion, int Cant1, int Cant2, int Cant3, int Cant4, decimal ValorCot)
		{
			ControlFUP controlFup = new ControlFUP();
			Dictionary<string, object> parametros = new Dictionary<string, object>();
			string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];
			string response = string.Empty;
            int Evento = 102;

            if (idPtoCargue != 229) { Evento = 103;  }
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


            if (data.FirstOrDefault().NoValido != 0)  // Cuando ocurre un error de parametros para agente o Transportador
            {
                response = CorreoFUP(Convert.ToInt32(idFup), idVersion, Evento, data.FirstOrDefault().MSG_Validacion);
            }

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
            parametros.Add("@vr_trm", flete.vr_trm);
			parametros.Add("@vr_impuestos", flete.vr_impuestos);

            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_det_fletes", parametros);

            if (flete.puerto_origen_id == 229) // Notificacion Fletes Nacionales GUACHENE
            {
                string response = string.Empty;
                response = CorreoFUP(Convert.ToInt32(idFup), idVersion, 101);  
            }

            return "OK";
		}

        [WebMethod]
        public static Boolean SaveLinks(string fupId, string version, List<LinksSave> linksList)
        {
            ControlFUP controlFup = new ControlFUP();
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            if(linksList.Count == 0)
            {
                return false;
            }
            parametros.Add("@pFupID", Convert.ToInt32(fupId));
            parametros.Add("@pVersion", version);
            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_DEL_salida_cot_tabla", parametros);

            foreach (LinksSave link in linksList)
            {
                parametros.Clear();
                parametros.Add("@pFupID", Convert.ToInt32(link.FupID));
                parametros.Add("@pVersion", link.Version);
                parametros.Add("@Consecutivo", Convert.ToInt32(link.Consecutivo));
                parametros.Add("@Link", link.Link);
                parametros.Add("@Descripcion", link.Descripcion);
                ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_salida_cot_tabla", parametros);
            }
            
            return true;
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
        public static string ObtenerLinks(string idFup, string idVersion)
        {
            ControlFUP controlFup = new ControlFUP();
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            string response = string.Empty;

            parametros.Add("@pFupID", Convert.ToInt32(idFup));
            parametros.Add("@pVersion", idVersion);

            List<LinksQuery> data = ControlDatos.EjecutarStoreProcedureConParametros<LinksQuery>("USP_fup_SEL_salida_cot_tabla", parametros);

            response = JsonConvert.SerializeObject(data);
            return response;
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

				fup_cntrcam_guardar fdata = new fup_cntrcam_guardar();
				//Crear Control Cambios
				switch (planPTF.Evento){
                    case "1":       //SOLICITUD DFT
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
					case "2":       // PROGRAMAR DFT
						fdata.IdFUP = Convert.ToInt32(idFup);
						fdata.Version = idVersion;
						fdata.cons = -1;
						fdata.padre = 0;
						fdata.Comentario = planPTF.Observacion;
						fdata.Estado = "";
						fdata.Titulo = "Programar DFT";
						fdata.EsDFT = 1;
						fdata.EstadoDFT = 9;
						fdata.SubprocesoDFT = 0;
						GuardarControlCambio(fdata, 1);
						break;
					case "3":       // DEVOLUCION DFT
						fdata.IdFUP = Convert.ToInt32(idFup);
						fdata.Version = idVersion;
						fdata.cons = -1;
						fdata.padre = 0;
						fdata.Comentario = planPTF.Observacion;
						fdata.Estado = "";
						fdata.Titulo = "Devolucion DFT";
						fdata.EsDFT = 1;
						fdata.EstadoDFT = 10;
						fdata.SubprocesoDFT = 0;
						GuardarControlCambio(fdata, 1);
						break;
					case "4":      //AVAL TECNICO DFT
                        fdata.IdFUP = Convert.ToInt32(idFup);
                        fdata.Version = idVersion;
                        fdata.cons = -1;
                        fdata.padre = 0;
                        fdata.Comentario = planPTF.Observacion;
                        fdata.Estado = "";
                        fdata.Titulo = "DFT APROBADO";
                        fdata.EsDFT = 1;
                        fdata.EstadoDFT = 6;
                        fdata.SubprocesoDFT = 2;
                        GuardarControlCambio(fdata, 1);
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
			int id=0;
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
			ordenCotizacion = controlFup.obtenerOrdenCotizacionFUP(Convert.ToInt32(idFup), idVersion, "CT");

			response = JsonConvert.SerializeObject(ordenCotizacion);

			return response;
		}

        [WebMethod(EnableSession = true)]
        public static string guardarOrdenCIFUP(string idFup, string idVersion, string FUsuario)
        {
            string ordenCotizacion = string.Empty, response = string.Empty;
            ControlFUP controlFup = new ControlFUP();

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            int id = 0;
            parametros.Add("@pFupID", double.Parse(idFup));
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@pUsuario", FUsuario);

            id = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_INS_PiezasSaldo_CI_ModFinal", parametros);
            ordenCotizacion = controlFup.obtenerOrdenCotizacionFUP(Convert.ToInt32(idFup), idVersion, "CI");

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
        public static string explosionarOrdenCIFUP(string idFup, string idVersion, string IdOfaCi)
        {
            string response = string.Empty;
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            int id = 5;
            parametros.Add("@pFupID", double.Parse(idFup));
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@pIdOfa", double.Parse(IdOfaCi));
            parametros.Add("@ptipo", 2);

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
        public static string GrabaFichaTecnicaPreDespacho(int idFup, string idVersion, bool? vaFichaTecnica, string FecReunion )
        {
            string response = string.Empty;

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];


            //Actualizar la fecha de Solicitud

            parametros.Add("@pFupID", idFup);
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@pUsuario", NombreUsu);
            parametros.Add("@pVaMesaTecnicaDespacho", vaFichaTecnica);
            parametros.Add("@pFecReunion", string.IsNullOrEmpty(FecReunion) ? (DateTime?)null : Convert.ToDateTime(FecReunion));
            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_enc_ent_FichaTecica", parametros);
            return "OK";
        }

        [WebMethod(EnableSession = true)]
        public static string GrabaNoEjecutarMesa(int idFup, string idVersion, int Tipo, bool? Ejec, string comentario)
        {
            string response = string.Empty;

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];


            //Actualizar la fecha de Solicitud

            parametros.Add("@pFupID", idFup);
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@pUsuario", NombreUsu);
            parametros.Add("@pTipo", Tipo);
            parametros.Add("@pEjecutar", Ejec);
            parametros.Add("@pComentario", comentario);
            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_enc_ent_NoMesa", parametros);
            return "OK";
        }


        [WebMethod(EnableSession = true)]
        public static string GrabaFichaTecnicaPreVenta(int idFup, string idVersion, bool? vaFichaTecnica)
        {
            string response = string.Empty;

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];


            //Actualizar la Solicitud de Mesa Tecnica Preventa
            parametros.Add("@pFupID", idFup);
            parametros.Add("@pVersion", idVersion);
            parametros.Add("@pUsuario", NombreUsu);
            parametros.Add("@pVaMesaTecnicaPreventa", vaFichaTecnica);
            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_enc_ent_FichaPreventa", parametros);

            if (vaFichaTecnica == true)
            {
                CorreoFUP(idFup, idVersion, 100);
            }


            return "OK";
        }

        [WebMethod(EnableSession = true)]
        public static string GuardarControlCambio(fup_cntrcam_guardar Item, int flag =0)
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
                if (Item.EstadoDFT > 0)
                {
                    //Actualizar ActaSeg solo cuando tiene un estado valido
                    parametros.Clear();
                    parametros.Add("@pFup", Item.IdFUP);
                    parametros.Add("@pVersion", Item.Version);
                    parametros.Add("@pFecSolicitaDFT", DateTime.Today.ToShortDateString());
                    parametros.Add("@usu_actualiza", NombreUsu);
                    parametros.Add("@pEstadoDft", Item.EstadoDFT);
                    int ok2 = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_ActaSeg_DFT", parametros);
                }
                EventoNoti = 73;
                if (Item.padre != 0) { EventoNoti = 74; } // Evento de Respuesta DFT
            }
            else
            {
                EventoNoti = 66;
                if (Item.padre != 0) { EventoNoti = 67; } // Evento de Respuesta
            }
            if ((EventoNoti == 73 && flag != 1) || (EventoNoti != 73) )
            {
                response = CorreoFUP((int)Item.IdFUP, Item.Version, EventoNoti);
            }
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

        [WebMethod(EnableSession = true)]
        public static string obtenerClasificacionCliente(int idCliente)
        {
            string response = string.Empty;

            Dictionary<string, object> parametros = new Dictionary<string, object>();

            ControlFUP controlFup = new ControlFUP();
            parametros.Add("@pClienteId", idCliente);

            List<ClasificaCli> ClasificaCliente = ControlDatos.EjecutarStoreProcedureConParametros<ClasificaCli>("USP_fup_SEL_ClasificacionCliente", parametros);

            response = JsonConvert.SerializeObject(ClasificaCliente);
            return response;
        }

        [WebMethod(EnableSession = true)]
        public static string obtenerDiasTDN(int IdPais)
        {
            string response = string.Empty;

            Dictionary<string, object> parametros = new Dictionary<string, object>();

            ControlFUP controlFup = new ControlFUP();
            parametros.Add("@pIdPaid", IdPais);

            List<fup_diasTdn> DiasTND = ControlDatos.EjecutarStoreProcedureConParametros<fup_diasTdn>("USP_fup_SEL_DiasTdnPais", parametros);

            response = JsonConvert.SerializeObject(DiasTND);
            return response;
        }

        [WebMethod(EnableSession = true)]
        public static string obtenerListadoVendedorZona(int idPais)
        {
            string response = string.Empty;

            Dictionary<string, object> parametros = new Dictionary<string, object>();

            ControlFUP controlFup = new ControlFUP();
            parametros.Add("@pPaisId", idPais);

            List<datosCombo> ListaZonas = ControlDatos.EjecutarStoreProcedureConParametros<datosCombo>("USP_fup_SEL_Lista_VendZona", parametros);

            response = JsonConvert.SerializeObject(ListaZonas);
            return response;
        }

        [WebMethod(EnableSession = true)]
        public static string ValidarRecomendacion(string recomendacion)
        {
            try
            {
                string sql = @"SELECT fup_hvp_Observaciones.hvpo_id AS IdRecompra, fup_hvp_Observaciones.hvpo_UsuaActualiza + ' - ' + CONVERT(varchar(10), fup_hvp_Observaciones.hvpo_FecActualiza, 103) " +
                      " + ' : ' + fup_hvp_Observaciones.hvpo_Comentario AS Recompra  FROM fup_hvp_Observaciones INNER JOIN " +
                      " fup_enc_entrada_cotizacion ON fup_hvp_Observaciones.hvpo_enc_entrada_cot_id = fup_enc_entrada_cotizacion.eect_id " +
                      " WHERE (fup_hvp_Observaciones.hvpo_TipoEntrada = 2) AND(fup_hvp_Observaciones.hvpo_id = @referencia )";


                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@referencia", recomendacion);

                List<recomendaccion> listaref = ControlDatos.EjecutarConsulta<recomendaccion>(sql, param);

                if (listaref.Count > 0)
                {
                    var query = new
                    {
                        id = 1,
                        descripcion = "Recomendacion Encontrada",
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
                    throw new Exception("No se encontro la Recomendacion");
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

        #region Script de Python
		
        #endregion

    }
}