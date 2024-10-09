using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaControl.Entity;
using Newtonsoft.Json;
using System.Threading;
using CapaControl;
using System.Globalization;
using System.Threading.Tasks;

namespace SIO
{
    public partial class FormSimuladorComparativoModFin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scripManager = ScriptManager.GetCurrent(this.Page);
            scripManager.EnablePageMethods = true;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }

        private static float CalculatePercentage(float x, float y)
        {
            if(y == 0)
            {
                return float.NaN;
            } else
            {
                //float result = ((x * 100) / y);
                float result = ((y - x) / x) * 100;
                if (float.IsInfinity(result))
                {
                    return float.NaN;
                }
                return result;
            }
        }

        private static float CheckIfIsInfinity(float result)
        {
            if (float.IsInfinity(result) || float.IsNaN(result))
            {
                return 0;
            }
            return result;
        }

        [WebMethod]
        public static string GetComparative(int fupId, string version)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pFupID", fupId);
            parametros.Add("@pversion", version);
            List<SimuladorComparativoModFin> data = ControlDatos.EjecutarStoreProcedureConParametros<SimuladorComparativoModFin>("USP_SimuladorProyecto_ComparativoModFin", parametros);
            data.ForEach(x => x.diference_tasa_dolar = CheckIfIsInfinity(x.tasaCotizacion - x.tasaModFinal));
            data.ForEach(x => x.percentage_tasa_dolar = CalculatePercentage(x.tasaModFinal, x.tasaCotizacion));
            data.ForEach(x => x.diference_m2 = CheckIfIsInfinity(x.M2xItem - x.M2xItemMf));
            data.ForEach(x => x.percentage_m2 = CalculatePercentage(x.M2xItemMf, x.M2xItem));
            data.ForEach(x => x.diference_piezas = CheckIfIsInfinity(x.CantPiezas - x.CantPiezasMf));
            data.ForEach(x => x.percentage_piezas = CalculatePercentage(x.CantPiezasMf, x.CantPiezas));
            data.ForEach(x => x.diference_peso = CheckIfIsInfinity(x.PesoxItem - x.PesoxItemMf));
            data.ForEach(x => x.percentage_peso = CalculatePercentage (x.PesoxItemMf, x.PesoxItem));
            data.ForEach(x => x.diference_costo = CheckIfIsInfinity(x.CostoxItem - x.CostoxItemMf));
            data.ForEach(x => x.percentage_costo = CalculatePercentage( x.CostoxItemMf, x.CostoxItem));
            data.ForEach(x => x.diference_costo_kg = CheckIfIsInfinity((x.CostoxItem / x.PesoxItem) - (x.CostoxItemMf / x.PesoxItemMf)));
            data.ForEach(x => x.percentage_costo_kg = CalculatePercentage((x.CostoxItemMf / x.PesoxItemMf), (x.CostoxItem / x.PesoxItem)));
            data.ForEach(x => x.diference_piezas_m2 = CheckIfIsInfinity((x.CantPiezas / x.M2xItem) - (x.CantPiezasMf / x.M2xItemMf)));
            data.ForEach(x => x.percentage_piezas_m2 = CalculatePercentage((x.CantPiezasMf / x.M2xItemMf), (x.CantPiezas / x.M2xItem)));
            data.ForEach(x => x.diference_kg_m2 = CheckIfIsInfinity((x.PesoxItem / x.M2xItem) - (x.PesoxItemMf / x.M2xItemMf)));
            data.ForEach(x => x.percentage_kg_m2 = CalculatePercentage((x.PesoxItemMf / x.M2xItemMf), (x.PesoxItem / x.M2xItem)));
            data.ForEach(x => x.diference_costo_m2 = CheckIfIsInfinity((x.CostoxItem / x.M2xItem) - (x.CostoxItemMf / x.M2xItemMf)));
            data.ForEach(x => x.percentage_costo_m2 = CalculatePercentage ((x.CostoxItemMf / x.M2xItemMf), (x.CostoxItem / x.M2xItem)));

            data.ForEach(x => x.diference_costo_mp = CheckIfIsInfinity(x.CostoMpxItem - x.CostoMpxItemMf));
            data.ForEach(x => x.percentage_costo_mp = CalculatePercentage(x.CostoMpxItemMf, x.CostoMpxItem));
            data.ForEach(x => x.diference_costo_mp_kg = CheckIfIsInfinity((x.CostoMpxItem / x.PesoxItem) - (x.CostoMpxItemMf / x.PesoxItemMf)));
            data.ForEach(x => x.percentage_costo_mp_kg = CalculatePercentage((x.CostoMpxItemMf / x.PesoxItemMf), (x.CostoMpxItem / x.PesoxItem)));

            data.ForEach(x => x.diference_costo_mod = CheckIfIsInfinity(x.ValorMOD_item - x.ValorMOD_itemMf));
            data.ForEach(x => x.percentage_costo_mod = CalculatePercentage( x.ValorMOD_itemMf, x.ValorMOD_item));
            data.ForEach(x => x.diference_costo_mod_kg = CheckIfIsInfinity((x.ValorMOD_item / x.PesoxItem) - (x.ValorMOD_itemMf / x.PesoxItemMf)));
            data.ForEach(x => x.percentage_costo_mod_kg = CalculatePercentage((x.ValorMOD_itemMf / x.PesoxItemMf), (x.ValorMOD_item / x.PesoxItem)));

            data.ForEach(x => x.diference_costo_cif = CheckIfIsInfinity(x.ValorCIF_Item - x.ValorCIF_ItemMf));
            data.ForEach(x => x.percentage_costo_cif = CalculatePercentage(x.ValorCIF_ItemMf, x.ValorCIF_Item));
            data.ForEach(x => x.diference_costo_cif_kg = CheckIfIsInfinity((x.ValorCIF_Item / x.PesoxItem) - (x.ValorCIF_ItemMf / x.PesoxItemMf)));
            data.ForEach(x => x.percentage_costo_cif_kg = CalculatePercentage((x.ValorCIF_ItemMf / x.PesoxItemMf), (x.ValorCIF_Item / x.PesoxItem)));

            SimuladorComparativoModFin nivel1 = data.Where(x => x.NivelAccesorios == 1).FirstOrDefault();
            SimuladorComparativoModFin nivel2 = data.Where(x => x.NivelAccesorios == 2).FirstOrDefault();
            SimuladorComparativoModFin nivel3 = data.Where(x => x.NivelAccesorios == 3).FirstOrDefault();
            SimuladorComparativoModFin nivel4 = data.Where(x => x.NivelAccesorios == 4).FirstOrDefault();
            SimuladorComparativoModFin nivel5 = data.Where(x => x.NivelAccesorios == 5).FirstOrDefault();
            Dictionary<string, object> response = new Dictionary<string, object>();
            float costoTotalAcero = data.Where(x => x.NivelAccesorios != 1).Select(x => x.CostoxItem).Sum();
            float costoRealTotalAcero = data.Where(x => x.NivelAccesorios != 1).Select(x => x.CostoxItemMf).Sum();
            float piezasTotalAcero = data.Where(x => x.NivelAccesorios != 1).Select(x => x.CantPiezas).Sum();
            float piezasRealTotalAcero = data.Where(x => x.NivelAccesorios != 1).Select(x => x.CantPiezasMf).Sum();
            float kgTotalAcero = data.Where(x => x.NivelAccesorios != 1).Select(x => x.PesoxItem).Sum();
            float kgRealTotalAcero = data.Where(x => x.NivelAccesorios != 1).Select(x => x.PesoxItemMf).Sum();
            float costoKgTotalAcero = costoTotalAcero / kgTotalAcero;
            float costoKgRealTotalAcero = costoRealTotalAcero / kgRealTotalAcero;
            costoKgTotalAcero = CheckIfIsInfinity(costoKgTotalAcero);
            costoKgRealTotalAcero = CheckIfIsInfinity(costoKgRealTotalAcero);

            float diference_costoTotalAcero = CheckIfIsInfinity(costoTotalAcero - costoRealTotalAcero);
            float percentage_costoTotalAcero = CalculatePercentage(costoTotalAcero, costoRealTotalAcero);

            float diference_piezasTotalAcero = CheckIfIsInfinity(piezasTotalAcero - piezasRealTotalAcero);
            float percentage_piezasTotalAcero = CalculatePercentage(piezasTotalAcero, piezasRealTotalAcero);

            float diference_kgTotalAcero = CheckIfIsInfinity(kgTotalAcero - kgRealTotalAcero);
            float percentage_kgTotalAcero = CalculatePercentage(kgTotalAcero, kgRealTotalAcero);

            response.Add("FechaSimulacion", data.FirstOrDefault().FechaSimulacion.ToShortDateString());
            response.Add("FechaModFinal", data.FirstOrDefault().FechaModFinal.ToShortDateString());
            response.Add("nivel1", nivel1);
            response.Add("nivel2", nivel2);
            response.Add("nivel3", nivel3);
            response.Add("nivel4", nivel4);
            response.Add("nivel5", nivel5);
            Dictionary<string, object> responseTemp = new Dictionary<string, object>();
            responseTemp.Add("Costo", costoTotalAcero);
            responseTemp.Add("CostoReal", costoRealTotalAcero);
            responseTemp.Add("Piezas", piezasTotalAcero);
            responseTemp.Add("PiezasReal", piezasRealTotalAcero);
            responseTemp.Add("Kg", kgTotalAcero);
            responseTemp.Add("KgReal", kgRealTotalAcero);
            responseTemp.Add("CostoKg", costoKgTotalAcero);
            responseTemp.Add("CostoKgReal", costoKgRealTotalAcero);

            responseTemp.Add("DifecenteCostoTotal", diference_costoTotalAcero);
            responseTemp.Add("PercentageCostoTotal", percentage_costoTotalAcero);
            responseTemp.Add("DifecentePiezasTotal", diference_piezasTotalAcero);
            responseTemp.Add("PercentagePiezasTotal", percentage_piezasTotalAcero);
            responseTemp.Add("DifecenteKgTotal", diference_kgTotalAcero);
            responseTemp.Add("PercentageKgTotal", percentage_kgTotalAcero);

            response.Add("acero", responseTemp);
            return JsonConvert.SerializeObject(response);
        }
    }
}