using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using CapaControl.Entity;
using System.Web.Services;
using CapaControl;

namespace SIO
{
    public partial class FormMaestroMonedas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        public static string getDataMoney()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            List<MonedaTRM_Consulta> data = ControlDatos.EjecutarStoreProcedureConParametros<MonedaTRM_Consulta>("USP_fup_SEL_MonedasTRM", parametros).ToList();
            return JsonConvert.SerializeObject(data);
        }

        [WebMethod(EnableSession = true)]
        public static string getMonths()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            List<Mes_Consulta> data = ControlDatos.EjecutarStoreProcedureConParametros<Mes_Consulta>("USP_fup_SEL_Mes", parametros).ToList();
            return JsonConvert.SerializeObject(data);
        }

        [WebMethod(EnableSession = true)]
        public static string getYears()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            List<Anio_Consulta> data = ControlDatos.EjecutarStoreProcedureConParametros<Anio_Consulta>("USP_fup_SEL_Anio", parametros).ToList();
            return JsonConvert.SerializeObject(data);
        }

        [WebMethod(EnableSession = true)]
        public static string getActiveCurrencies()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            List<Moneda_Consulta> data = ControlDatos.EjecutarStoreProcedureConParametros<Moneda_Consulta>("USP_fup_SEL_Monedas", parametros).ToList();
            return JsonConvert.SerializeObject(data);
        }

        [WebMethod(EnableSession = true)]
        public static string CreateUpdateCurrency(MonedaTRM_Create_Update item)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string msgCambio = string.Empty;

            parametros.Add("@pMonedaTrmId", item.MonedaTrmId);
            parametros.Add("@pMonedaId", item.MonedaId);
            parametros.Add("@pMonedaAnio", item.MonedaAnio);
            parametros.Add("@pMonedaMes", item.MonedaMes);
            parametros.Add("@pMonedaTrmValor", item.MonedaTrmValor);
            parametros.Add("@pUsuario", (string)HttpContext.Current.Session["Nombre_Usuario"]);
            List<MonedaTRM_Consulta> data = ControlDatos.EjecutarStoreProcedureConParametros<MonedaTRM_Consulta>("USP_fup_UPD_MonedasTRM", parametros).ToList();

            msgCambio = "<table><tr><td colspan=2>Cambio de Tasa de TRM</td></tr>"
                      + "<tr><td>Fecha</td><td>" + DateTime.Now.ToString("yyyy-MM-dd") + "</td></tr>"
                      + "<tr><td>Usuario</td><td>" + (string)HttpContext.Current.Session["Nombre_Usuario"] + "</td></tr>"
                      + "<tr><td>Moneda</td><td>" + (string)data.FirstOrDefault().MonedaDescripcion + "</td></tr>"
                      + "<tr><td>Periodo</td><td>" + data.FirstOrDefault().MonedaTrmPeriodo + "</td></tr>"
                      + "<tr><td>Nueva Tasa</td><td>" + data.FirstOrDefault().MonedaTrmValor.ToString() + "</td></tr>"
                      + "</table>";

            FormFUP.CorreoFUP(3, "A", 110, msgCambio);

            return JsonConvert.SerializeObject(data);
        }
    }
}