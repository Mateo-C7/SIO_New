using CapaControl;
using CapaControl.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace SIO
{
    public partial class FormCargueCostoReal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scripManager = ScriptManager.GetCurrent(this.Page);
            scripManager.EnablePageMethods = true;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }

        [WebMethod]
        public static string getSavedRows()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            List<StgCostosRealExcel> rows = ControlDatos.EjecutarStoreProcedureConParametros<StgCostosRealExcel>("USP_SEL_ExcelCostoRealOrden", parameters);
            return JsonConvert.SerializeObject(rows);
        }

        [WebMethod]
        public static bool SaveRows(List<StgCostosRealExcel> rows)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                foreach (StgCostosRealExcel row in rows)
                {
                    parameters.Add("@pAnio", row.Anio);
                    parameters.Add("@pMes", Convert.ToInt32(row.Mes));
                    parameters.Add("@pTipo", row.Tipo);
                    parameters.Add("@pNoOrden", row.NoOrden);
                    parameters.Add("@pCliente", row.Cliente);
                    parameters.Add("@pPais", row.Pais);
                    parameters.Add("@pNacEx", row.NacEx);
                    parameters.Add("@pM2Vend", row.M2Vend);
                    parameters.Add("@pPrecioM2USD", row.PrecioM2USD);
                    parameters.Add("@pUndVend", row.UndVend);
                    parameters.Add("@pKgVend", row.KgVend);
                    parameters.Add("@pUndM2", row.UndM2);
                    parameters.Add("@pM2Cot", row.M2Cot);
                    parameters.Add("@pVrCot", row.VrCot);
                    parameters.Add("@pUSD", row.USD);
                    parameters.Add("@pOtrosUSD", row.OtrosUSD);
                    parameters.Add("@pCOP", row.COP);
                    parameters.Add("@pMpAluminio", row.MpAluminio);
                    parameters.Add("@pMpPlastico", row.MpPlastico);
                    parameters.Add("@pKanban", row.Kanban);
                    parameters.Add("@pStock", row.Stock);
                    parameters.Add("@pConsumibles", row.Consumible);
                    parameters.Add("@pMOD", row.MOD);
                    parameters.Add("@pCIF", row.CIF);
                    parameters.Add("@pTotalAluminio", row.TotalAluminio);
                    parameters.Add("@pNivel1Almacen", row.Nivel1Almacen);
                    parameters.Add("@pNivel2Almacen", row.Nivel2Almacen);
                    parameters.Add("@pNivel3Almacen", row.Nivel3Almacen);
                    parameters.Add("@pNivel4Almacen", row.Nivel4Almacen);
                    parameters.Add("@pNivel5Almacen", row.Nivel5Almacen);
                    parameters.Add("@pConsObra", row.ConsObra);
                    parameters.Add("@pTotalAccAlmacen", row.TotalAccAlmacen);
                    parameters.Add("@pNivel1Fabricados", row.Nivel1Fabricados);
                    parameters.Add("@pNivel2Fabricados", row.Nivel2Fabricados);
                    parameters.Add("@pNivel3Fabricados", row.Nivel3Fabricados);
                    parameters.Add("@pNivel4Fabricados", row.Nivel4Fabricados);
                    parameters.Add("@pNivel5Fabricados", row.Nivel5Fabricados);
                    parameters.Add("@pTotalAccFabricados", row.TotalAccFabricados);
                    parameters.Add("@pAcero", row.Acero);
                    parameters.Add("@pCostoTotal", row.CostoTotal);
                    parameters.Add("@pPorc", row.Porc);

                    ControlDatos.GuardarStoreProcedureConParametros("USP_INS_ExcelCostoRealOrden", parameters);

                    parameters.Clear();
                }
                return true;
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}