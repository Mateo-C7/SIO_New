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
    public partial class PlantasTurnos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string cargarPlan(int PlantaID, DateTime Inicio, DateTime Fin)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pFecIni", Inicio);
            parametros.Add("@pFecFin", Fin);
            parametros.Add("@pAreaprod", PlantaID);

            List<Plandia> data = ControlDatos.EjecutarStoreProcedureConParametros<Plandia>("USP_PROD_SEL_PlanProduccion", parametros);
            string response = JsonConvert.SerializeObject(data);
            return response;
        }

        [WebMethod]
        public static string cargarPlanta()
        {
            string sql = @"SELECT pap_id id, pap_Descripcion descripcion
                            FROM prod_AreaPlanta ";

            List<datosCombo2> Plantas = ControlDatos.EjecutarConsulta<datosCombo2>(sql, new Dictionary<string, object>());

            string response = JsonConvert.SerializeObject(Plantas);
            return response;
        }


        [WebMethod(EnableSession = true)]
        public static string guardarPlan(DateTime Inicio, int PlanId, decimal Cant, int IdEntrada, int IdOfa)
        {
            string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];

            //Obtenemos el id de la  programacion

            Dictionary<string, object> parametrosId = new Dictionary<string, object>();
            parametrosId.Add("@Id", PlanId);

            Plandia programacionFUP = ControlDatos.EjecutarStoreProcedureConParametros<Plandia>("USP_PROD_SEL_PlanIdProduccion", parametrosId).FirstOrDefault();
            if (programacionFUP != null)
            {
                if (programacionFUP.M2Producir == Cant)
                {
                    Dictionary<string, object> parametros = new Dictionary<string, object>();
                    parametros.Add("@pPlanid", PlanId);
                    parametros.Add("@penc_entrada_cot_id", IdEntrada);
                    parametros.Add("@pId_Ofa", IdOfa);
                    parametros.Add("@pfecha", Inicio);
                    parametros.Add("@pCantMetros", Cant);
                    parametros.Add("@pusuario", NombreUsu);

                    List<int> data = ControlDatos.EjecutarStoreProcedureConParametros<int>("USP_PROD_UPD_PlanProduccion", parametros);
                    string response = JsonConvert.SerializeObject(data);
                    return response;
                }
                else
                {
                    //El nuevo
                    Dictionary<string, object> parametros = new Dictionary<string, object>();
                    parametros.Add("@pPlanid", -1);
                    parametros.Add("@penc_entrada_cot_id", IdEntrada);
                    parametros.Add("@pId_Ofa", IdOfa);
                    parametros.Add("@pfecha", Inicio);
                    parametros.Add("@pCantMetros", Cant);
                    parametros.Add("@pusuario", NombreUsu);

                    List<int> data = ControlDatos.EjecutarStoreProcedureConParametros<int>("USP_PROD_UPD_PlanProduccion", parametros);

                    //Disminucion del otro
                    Dictionary<string, object> parametrosModify = new Dictionary<string, object>();
                    parametrosModify.Add("@pPlanid", PlanId);
                    parametrosModify.Add("@penc_entrada_cot_id", IdEntrada);
                    parametrosModify.Add("@pId_Ofa", IdOfa);
                    parametrosModify.Add("@pfecha", programacionFUP.Fecha);
                    parametrosModify.Add("@pCantMetros", programacionFUP.M2Producir - Cant);
                    parametrosModify.Add("@pusuario", NombreUsu);

                    List<int> dataModified = ControlDatos.EjecutarStoreProcedureConParametros<int>("USP_PROD_UPD_PlanProduccion", parametrosModify);


                    string response = JsonConvert.SerializeObject(data);
                    return response;
                }
            }
            return String.Empty;
        }
    }

}