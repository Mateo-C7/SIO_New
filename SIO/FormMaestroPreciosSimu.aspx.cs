using CapaControl;
using CapaControl.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIO
{
	public partial class FormMaestroPreciosSimu : System.Web.UI.Page
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			ScriptManager scripManager = ScriptManager.GetCurrent(this.Page);
			scripManager.EnablePageMethods = true;

			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

		}

		[WebMethod]
		public static string obtenerDatosGenerales()
		{
			ControlObra controlObra = new ControlObra();
			ControlFUP controlFup = new ControlFUP();
			ControlUbicacion controlUbicacion = new ControlUbicacion();
			Dictionary<string, object> queryResult = new Dictionary<string, object>();

			// Planta
			List<datosCombo2> listaPlanta = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT planta_id id, planta_descripcion descripcion FROM planta_forsa WHERE planta_activo = 1
																						ORDER BY planta_id", new Dictionary<string, object>());
			// Grupo Pais
			List<datosCombo2> lstGRupospa = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT grpa_id id, grpa_gp1_nombre descripcion FROM FUP_GRUPO_PAIS WHERE activo = 1 
																						ORDER BY grpa_id ", new Dictionary<string, object>());

			// Pais
			List<datosCombo2> lstPais = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT pai_id id, pai_nombre descripcion FROM pais 
																						ORDER BY pai_nombre ", new Dictionary<string, object>());

			// Nivel Cotizaciones
			List<datosCombo2> lstNivel = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT fdom_CodDominio id, fdom_Descripcion descripcion
							FROM fup_Dominios WHERE fdom_Dominio ='NIVEL_SIMUL' ORDER BY fdom_OrdenDominio ", new Dictionary<string, object>());

			// Item Cotizaciones
			List<datosCombo2> lstItemsC = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT fic_id id, fic_ItemCotizacion descripcion
							FROM fup_ItemCotizacion_sim ", new Dictionary<string, object>());
			
			List<CapaControl.Entity.Moneda> lstMoneda = controlUbicacion.obtenerMoneda();

			Dictionary<string, object> parametros = new Dictionary<string, object>();

			List<listaitemcot> lstItemCot = ControlDatos.EjecutarStoreProcedureConParametros<listaitemcot>("USP_SIMU_SEL_ItemCotizacion", parametros);

			List<listamargen> lstMargen = ControlDatos.EjecutarStoreProcedureConParametros<listamargen>("USP_SIMU_SEL_MargenesNivel", parametros);

			List<listaprecio> lstPrecio = ControlDatos.EjecutarStoreProcedureConParametros<listaprecio>("USP_SIMU_SEL_PreciosItemCot", parametros);

			// Cliente
			List<listacliente> lstCliente = ControlDatos.EjecutarConsulta<listacliente>(@"select ClienteId, Cliente from vista_ClientesPreciosSIMU", parametros); 

			queryResult.Add("varMoneda", lstMoneda);
			queryResult.Add("varItemCot", lstItemCot);
			queryResult.Add("varNivel", lstNivel);
			queryResult.Add("varPlantas", listaPlanta);
			queryResult.Add("varGrupoPais", lstGRupospa);
			queryResult.Add("varPais", lstPais);
			queryResult.Add("varMargen", lstMargen);
			queryResult.Add("varPrecio", lstPrecio);
			queryResult.Add("varItemSCotiza", lstItemsC);
			queryResult.Add("varCliente", lstCliente);

			string response = JsonConvert.SerializeObject(queryResult);
			return response;
		}

		[WebMethod]
		public static string GuardarMargenesNivel(int Id, string NivelId, string GrupoPaId, string PaisId, string Margen, string MargenMinimo,
			bool insertarLog)
		{
			string response = string.Empty;
            string msgCambio = string.Empty;
            Dictionary<string, object> parametros = new Dictionary<string, object>();
			//string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];

			var culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
			culture.NumberFormat.NumberDecimalSeparator = ".";

			if (insertarLog)
			{
				parametros.Add("@pFim_id", Id);
				parametros.Add("@pFim_margen_nuevo", decimal.Parse(Margen, culture));
				parametros.Add("@pFim_margenMinimo_nuevo", decimal.Parse(MargenMinimo, culture));
				parametros.Add("@pUsuario_log", (string)HttpContext.Current.Session["Nombre_Usuario"]);
				ControlDatos.GuardarStoreProcedureConParametros("USP_SIMU_INS_MargenesNivel_Log", parametros);
			}

			parametros.Clear();
			parametros.Add("@pNivelId", NivelId);
			parametros.Add("@pGrupoPaId", GrupoPaId);
			parametros.Add("@pPaisId", PaisId);
			parametros.Add("@pMargen", decimal.Parse(Margen, culture));
			parametros.Add("@pMargenMinimo", decimal.Parse(MargenMinimo, culture));
			parametros.Add("@pUsuarioActualiza", (string)HttpContext.Current.Session["Nombre_Usuario"]);
			List<listamargen> data = ControlDatos.EjecutarStoreProcedureConParametros<listamargen>("USP_SIMU_UPD_MargenesNivel", parametros);

            msgCambio = "<table><tr><td colspan=2>Cambio de Precios Simulador Margenes</td>"
                      + "<tr><td>Fecha</td><td>" + DateTime.Now.ToString("yyyy-MM-dd") + "</td></tr>"
                      + "<tr><td>Usuario</td><td>" + (string)HttpContext.Current.Session["Nombre_Usuario"] + "</td></tr>"
                      + "<tr><td>Nivel Margen</td><td>" + data.FirstOrDefault().Nivel + "</td></tr>"
                      + "<tr><td>Grupo Pais</td><td>" + data.FirstOrDefault().GrupoPais + "</td></tr>"
                      + "<tr><td>Pais</td><td>" + data.FirstOrDefault().Pais + "</td></tr>"
                      + "<tr><td>Nuevo Margen</td><td>" + data.FirstOrDefault().Margen.ToString() + "</td></tr>"
                      + "<tr><td>Nuevo Margen Minimo</td><td>" + data.FirstOrDefault().MargenMinimo.ToString() + "</td></tr>"
                      + "</table>";			

            //FormFUP.CorreoFUP(4, "A", 110, msgCambio);

            return "OK";
		}

		[WebMethod]
		public static string GuardarPreciosVentaRegion(int Id, string ItemId, string GrupoPaId, string PaisId, string MonedaId, string Precio, 
			string ClienteId, bool insertarLog)
		{
			string response = string.Empty;
            string msgCambio = string.Empty;
			Dictionary<string, object> parametros = new Dictionary<string, object>();

			var culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
			culture.NumberFormat.NumberDecimalSeparator = ".";

			if (insertarLog)
			{
				parametros.Add("@pFpm_id", Id);
				parametros.Add("@pFpm_IdMoneda_nuevo", MonedaId);
				parametros.Add("@pFpm_Precio_nuevo", decimal.Parse(Precio, culture));
				parametros.Add("@pUsuario_log", (string)HttpContext.Current.Session["Nombre_Usuario"]);
				ControlDatos.GuardarStoreProcedureConParametros("USP_SIMU_INS_PreciosItemCot_Log", parametros);
			}

			parametros.Clear();
			parametros.Add("@pItemId", ItemId);
			parametros.Add("@pGrupoPaId", GrupoPaId);
			parametros.Add("@pPaisId", PaisId);
			parametros.Add("@pMonedaId", MonedaId);
			parametros.Add("@pPrecio", decimal.Parse(Precio, culture));
			parametros.Add("@pClienteId", ClienteId);
			parametros.Add("@pUsuarioActualiza", (string)HttpContext.Current.Session["Nombre_Usuario"]);
			List<listaprecio> data = ControlDatos.EjecutarStoreProcedureConParametros<listaprecio>("USP_SIMU_UPD_PreciosItemCot", parametros);

            msgCambio = "<table><tr><td colspan=2>Cambio de Precios Simulador</td>"
                      + "<tr><td>Fecha</td><td>" + DateTime.Now.ToString("yyyy-MM-dd") + "</td></tr>"
                      + "<tr><td>Usuario</td><td>" + (string)HttpContext.Current.Session["Nombre_Usuario"] + "</td></tr>"
                      + "<tr><td>Item Cotizacion</td><td>" + data.FirstOrDefault().ItemCotizacion + "</td></tr>"
                      + "<tr><td>Grupo Pais</td><td>" + data.FirstOrDefault().GrupoPais + "</td></tr>"
                      + "<tr><td>Pais</td><td>" + data.FirstOrDefault().Pais + "</td></tr>"
                      + "<tr><td>Moneda</td><td>" + data.FirstOrDefault().Moneda + "</td></tr>"
                      + "<tr><td>Nuevo Precio</td><td>" + data.FirstOrDefault().Precio.ToString() + "</td></tr>"
                      + "</table>";

            //FormFUP.CorreoFUP(5, "A", 110, msgCambio);

            return "OK";
		}

	}
}