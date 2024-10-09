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
    public partial class FormMaestroCostoSimu : System.Web.UI.Page
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

            // Tipo Materia
            List<datosCombo2> lstTipoMp = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT fdom_CodDominio id, fdom_Descripcion descripcion
                            FROM fup_Dominios WHERE fdom_Dominio ='TIPO_MP_SIMULCOSTO' ORDER BY fdom_OrdenDominio ", new Dictionary<string, object>());
            // Planta
            List<datosCombo2> listaPlanta = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT planta_id id, planta_descripcion descripcion FROM planta_forsa WHERE planta_activo = 1
																						ORDER BY planta_id", new Dictionary<string, object>());

            // Formaleta
            List<datosCombo2> lstTipoFormaleta = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT fdom_CodDominio id, fdom_Descripcion descripcion
                            FROM fup_Dominios WHERE fdom_Dominio ='TIPO_FORM_SIMUCOST' ORDER BY fdom_OrdenDominio ", new Dictionary<string, object>());

            // Origen
            List<datosCombo2> lstOrigenMp = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT fdom_CodDominio id, fdom_Descripcion descripcion
                            FROM fup_Dominios WHERE fdom_Dominio ='ORIGEN_COSTO_SIM' ORDER BY fdom_OrdenDominio ", new Dictionary<string, object>());

            List<CapaControl.Entity.Moneda> lstMoneda = controlUbicacion.obtenerMoneda();

            Dictionary<string, object> parametros = new Dictionary<string, object>();

            List<listacostomp> lstCostomp = ControlDatos.EjecutarStoreProcedureConParametros<listacostomp>("USP_SIMU_SEL_CostoMp", parametros);

            List<listacostocifmod> lstCostocifmod = ControlDatos.EjecutarStoreProcedureConParametros<listacostocifmod>("USP_SIMU_SEL_CostoCifMod", parametros);

            queryResult.Add("varMoneda", lstMoneda);
            queryResult.Add("varTipoMp", lstTipoMp);
            queryResult.Add("varPlantas", listaPlanta);
            queryResult.Add("varTipoFormaleta", lstTipoFormaleta);
            queryResult.Add("lstOrigenMp", lstOrigenMp);
            queryResult.Add("varCostoMp", lstCostomp);
            queryResult.Add("varCostoCifMod", lstCostocifmod);

            string response = JsonConvert.SerializeObject(queryResult);
            return response;
        }

        [WebMethod]
        public static string obtenerDatosporFechaMateriaPrima(String FechaIni, String FechaFin)
        {
            Dictionary<string, object> queryResult = new Dictionary<string, object>();

            Dictionary<string, object> parametros = new Dictionary<string, object>();

            parametros.Add("@pFecIni", FechaIni);
            parametros.Add("@pFecFin", FechaFin);
            List<listacostomp> lstTurnos = ControlDatos.EjecutarStoreProcedureConParametros<listacostomp>("USP_SIMU_SEL_CostoMp", parametros);

            queryResult.Add("varTurnos", lstTurnos);

            string response = JsonConvert.SerializeObject(queryResult);
            return response;
        }

        [WebMethod]
        public static string obtenerDatosporFechaManoObra(String FechaIni, String FechaFin)
        {
            Dictionary<string, object> queryResult = new Dictionary<string, object>();

            Dictionary<string, object> parametros = new Dictionary<string, object>();

            parametros.Add("@pFecIni", FechaIni);
            parametros.Add("@pFecFin", FechaFin);
            List<listacostocifmod> lstTurnos = ControlDatos.EjecutarStoreProcedureConParametros<listacostocifmod>("USP_SIMU_SEL_CostoCifMod", parametros);

            queryResult.Add("varTurnos", lstTurnos);

            string response = JsonConvert.SerializeObject(queryResult);
            return response;
        }

        [WebMethod]
        public static string GuardarCostosMateriaPrima(string FechaVigencia, string Planta, string Origen, string MateriaPrima, string CostoMp, string Costo2, string kilos, string ValorLme, string ValorLme2, string Observaciones)
        {
            string response = string.Empty;
            string msgCambio = string.Empty;
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            parametros.Add("@pFechaVigencia", FechaVigencia);
            parametros.Add("@pPlantaId", Planta);
            parametros.Add("@pOrigenId", Origen);
            parametros.Add("@pTipoMpId", MateriaPrima);
            parametros.Add("@pCosto", Convert.ToDecimal(CostoMp));
            parametros.Add("@pCosto2", Convert.ToDecimal(Costo2));
            parametros.Add("@pKilos", Convert.ToDecimal(kilos));
            parametros.Add("@pLme", Convert.ToDecimal(ValorLme));
            parametros.Add("@pLme2", Convert.ToDecimal(ValorLme2));
            parametros.Add("@pObservaciones", Observaciones);
            parametros.Add("@pUsuario", (string)HttpContext.Current.Session["Nombre_Usuario"] );

            List<listacostomp> data = ControlDatos.EjecutarStoreProcedureConParametros<listacostomp>("USP_SIMU_UPD_CostoMp", parametros);

            msgCambio = "<table><tr><td colspan=2>Cambio de Materia Prima</td>"
                      + "<tr><td>Fecha</td><td>" + DateTime.Now.ToString("yyyy-MM-dd") + "</td></tr>"
                      + "<tr><td>Usuario</td><td>" + (string)HttpContext.Current.Session["Nombre_Usuario"] + "</td></tr>"
                      + "<tr><td>Planta</td><td>" + data.FirstOrDefault().Planta + "</td></tr>"
                      + "<tr><td>Tipo Materia Prima</td><td>" + data.FirstOrDefault().TipoMateria + "</td></tr>"
                      + "<tr><td>Fecha Vigencia</td><td>" + data.FirstOrDefault().FechaVigencia + "</td></tr>"
                      + "<tr><td>Origen</td><td>" + data.FirstOrDefault().Origen + "</td></tr>"
                      + "<tr><td>Nuevo Costo</td><td>" + data.FirstOrDefault().costo.ToString() + "</td></tr>"
                      + "<tr><td>Observaciones</td><td>" + data.FirstOrDefault().Observaciones + "</td></tr>"
                      + "</table>";

            FormFUP.CorreoFUP(1,"A",110, msgCambio);

            return "OK";
        }

        [WebMethod]
        public static string GuardarCostosManoDeObra(string FechaVigencia, string PlantaId, string CostoMOD, string CostoCIF, string Chatarra, string Chatarra240, string TipoFormaleta)
        {
            string response = string.Empty;
            string msgCambio = string.Empty;
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            parametros.Add("@pFechaVigencia", FechaVigencia);
            parametros.Add("@pPlantaId", PlantaId);
            parametros.Add("@pCostoMOD", CostoMOD);
            parametros.Add("@pCostoCIF", CostoCIF);
            parametros.Add("@pChatarra", Convert.ToDecimal(Chatarra));
            parametros.Add("@pChatarra240", Convert.ToDecimal(Chatarra240));
            parametros.Add("@pTipoFormaleta", Convert.ToInt32(TipoFormaleta));
            parametros.Add("@pUsuario", (string)HttpContext.Current.Session["Nombre_Usuario"] );
            List<listacostocifmod> data = ControlDatos.EjecutarStoreProcedureConParametros<listacostocifmod>("USP_SIMU_UPD_CostoCifMod", parametros);

            msgCambio = "<table><tr><td colspan=2>Cambio de Costos MOD - CIF</td>"
                      + "<tr><td>Fecha</td><td>" + DateTime.Now.ToString("yyyy-MM-dd") + "</td></tr>"
                      + "<tr><td>Usuario</td><td>" + (string)HttpContext.Current.Session["Nombre_Usuario"] + "</td></tr>"
                      + "<tr><td>Planta</td><td>" + data.FirstOrDefault().Planta + "</td></tr>"
                      + "<tr><td>Tipo Formaleta</td><td>" + data.FirstOrDefault().TipoFormaleta + "</td></tr>"
                      + "<tr><td>Fecha Vigencia</td><td>" + data.FirstOrDefault().FechaVigencia + "</td></tr>"
                      + "<tr><td>Nuevo Costo MOD</td><td>" + data.FirstOrDefault().CostoMOD.ToString() + "</td></tr>"
                      + "<tr><td>Nuevo Costo CIF</td><td>" + data.FirstOrDefault().CostoCIF.ToString() + "</td></tr>"
                      + "<tr><td>Nuevo % Chatarra</td><td>" + data.FirstOrDefault().pChatarra.ToString() + "</td></tr>"
                      + "<tr><td>Nuevo % Chatarra > 240 </td><td>" + data.FirstOrDefault().pChatarra240.ToString() + "</td></tr>"
                      + "</table>";

            FormFUP.CorreoFUP(2, "A", 110, msgCambio);

            return "OK";
        }

    }
}