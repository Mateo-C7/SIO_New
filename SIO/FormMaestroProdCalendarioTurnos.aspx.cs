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
	public partial class FormMaestroProdCalendarioTurnos : System.Web.UI.Page
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
			List<listaTurnos> lstTurnos = ControlDatos.EjecutarStoreProcedureConParametros<listaTurnos>("USP_SEL_Prod_CalendarioTurnos", parametros);

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
            List<listaTurnos> lstTurnos = ControlDatos.EjecutarStoreProcedureConParametros<listaTurnos>("USP_SEL_Prod_CalendarioTurnos", parametros);

            queryResult.Add("varTurnos", lstTurnos);

            string response = JsonConvert.SerializeObject(queryResult);
            return response;
        }

        [WebMethod]
		public static string GuardarProdCalendarioTurnos(string pct_id, string pct_Fecha, string pct_CantTurnos, string pct_CantTurnosBr, string pct_SemanaMes, string pct_Mes, string pct_usuarioCrea)
		{
            string response = string.Empty;
			Dictionary<string, object> parametros = new Dictionary<string, object>();
			//string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];

			parametros.Add("@ppct_id", Convert.ToInt32(pct_id));
			parametros.Add("@ppct_Fecha", pct_Fecha);
			parametros.Add("@ppct_CantTurnos", Convert.ToInt32(pct_CantTurnos));
            parametros.Add("@ppct_CantTurnosBr", Convert.ToInt32(pct_CantTurnosBr));
            parametros.Add("@ppct_SemanaMes", Convert.ToDecimal(pct_SemanaMes));
			parametros.Add("@ppct_Mes", pct_Mes);
            parametros.Add("@ppct_usuarioCrea", pct_usuarioCrea);

            ControlDatos.GuardarStoreProcedureConParametros("USP_UPD_Prod_CalendarioTurnos", parametros);
			return "OK";
		}

	}
}