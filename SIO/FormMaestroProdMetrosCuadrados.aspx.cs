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
	public partial class FormMaestroProdMetrosCuadrados : System.Web.UI.Page
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

			Dictionary<string, object> parametros = new Dictionary<string, object>();

			parametros.Add("@pFecIni", "01/01/2022");
			var DateAndTime = DateTime.Now;
			var Date = DateAndTime.Date.ToString("dd/MM/yyyy");
			parametros.Add("@pFecFin", Date.ToString());
			List<listaMetrosTurnos> lstTurnos = ControlDatos.EjecutarStoreProcedureConParametros<listaMetrosTurnos>("USP_SEL_Prod_MetrosporTurno", parametros);

			queryResult.Add("varTurnos", lstTurnos);

			string response = JsonConvert.SerializeObject(queryResult);
			return response;
		}

        [WebMethod]
        public static string obtenerDatosporFecha(String FechaIni, String FechaFin)
        {
            Dictionary<string, object> queryResult = new Dictionary<string, object>();

            Dictionary<string, object> parametros = new Dictionary<string, object>();

            parametros.Add("@pFecIni", FechaIni);
            parametros.Add("@pFecFin", FechaFin);            
            List<listaMetrosTurnos> lstTurnos = ControlDatos.EjecutarStoreProcedureConParametros<listaMetrosTurnos>("USP_SEL_Prod_MetrosporTurno", parametros);

            queryResult.Add("varTurnos", lstTurnos);

            string response = JsonConvert.SerializeObject(queryResult);
            return response;
        }

        [WebMethod]
		public static string GuardarProdMetrosporTurnos(string pmt_id, string pmt_CantMetros, string pmt_CantMetrosBr, string pmt_porcMinimo, string pmt_Habilitado, string pmt_Fecha, string pmt_usuarioCrea)
		{
            string response = string.Empty;
			Dictionary<string, object> parametros = new Dictionary<string, object>();
			//string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];

			parametros.Add("@ppmt_id", Convert.ToInt32(pmt_id));
			parametros.Add("@ppmt_CantMetros", Convert.ToInt32(pmt_CantMetros));
            parametros.Add("@ppmt_CantMetrosBr", Convert.ToInt32(pmt_CantMetrosBr));
            parametros.Add("@ppmt_porcMinimo", Convert.ToDecimal(pmt_porcMinimo));
			parametros.Add("@ppmt_Habilitado", Convert.ToBoolean(pmt_Habilitado));
			parametros.Add("@ppmt_fecha", pmt_Fecha);
            parametros.Add("@ppmt_usuarioCrea", pmt_usuarioCrea);

        ControlDatos.GuardarStoreProcedureConParametros("USP_UPD_Prod_MetrosporTurno", parametros);
			return "OK";
		}

	}
}