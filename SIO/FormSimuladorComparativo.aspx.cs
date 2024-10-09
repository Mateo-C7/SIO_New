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
    public partial class FormSimuladorComparativo : System.Web.UI.Page
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
            List<SimuladorComparativo> data = ControlDatos.EjecutarStoreProcedureConParametros<SimuladorComparativo>("USP_SimuladorProyecto_ComparativoReal", parametros);
            data.ForEach(x => x.diference_tasa_dolar = CheckIfIsInfinity(x.tasaMonedaCotizacion - x.tasaDolar));
            data.ForEach(x => x.percentage_tasa_dolar = CalculatePercentage(x.tasaDolar, x.tasaMonedaCotizacion));
            data.ForEach(x => x.diference_m2 = CheckIfIsInfinity(x.M2xItem - x.M2_real));
            data.ForEach(x => x.percentage_m2 = CalculatePercentage(x.M2_real, x.M2xItem));
            data.ForEach(x => x.diference_piezas = CheckIfIsInfinity(x.CantPiezas - x.Unidades_real));
            data.ForEach(x => x.percentage_piezas = CalculatePercentage(x.Unidades_real, x.CantPiezas));
            data.ForEach(x => x.diference_peso = CheckIfIsInfinity(x.PesoxItem - x.Peso_real));
            data.ForEach(x => x.percentage_peso = CalculatePercentage (x.Peso_real, x.PesoxItem));
            data.ForEach(x => x.diference_costo = CheckIfIsInfinity(x.CostoxItem - x.Costo_real));
            data.ForEach(x => x.percentage_costo = CalculatePercentage( x.Costo_real, x.CostoxItem));
            data.ForEach(x => x.diference_costo_kg = CheckIfIsInfinity((x.CostoxItem / x.PesoxItem) - (x.Costo_real / x.Peso_real)));
            data.ForEach(x => x.percentage_costo_kg = CalculatePercentage((x.Costo_real / x.Peso_real), (x.CostoxItem / x.PesoxItem)));
            data.ForEach(x => x.diference_piezas_m2 = CheckIfIsInfinity((x.CantPiezas / x.M2xItem) - (x.Unidades_real / x.M2_real)));
            data.ForEach(x => x.percentage_piezas_m2 = CalculatePercentage((x.Unidades_real / x.M2_real), (x.CantPiezas / x.M2xItem)));
            data.ForEach(x => x.diference_kg_m2 = CheckIfIsInfinity((x.PesoxItem / x.M2xItem) - (x.Peso_real / x.M2_real)));
            data.ForEach(x => x.percentage_kg_m2 = CalculatePercentage((x.Peso_real / x.M2_real), (x.PesoxItem / x.M2xItem)));
            data.ForEach(x => x.diference_costo_m2 = CheckIfIsInfinity((x.CostoxItem / x.M2xItem) - (x.Costo_real / x.M2_real)));
            data.ForEach(x => x.percentage_costo_m2 = CalculatePercentage ((x.Costo_real / x.M2_real), (x.CostoxItem / x.M2xItem)));

            data.ForEach(x => x.diference_costo_mp = CheckIfIsInfinity(x.CostoMpxItem - x.CostoMP_Real));
            data.ForEach(x => x.percentage_costo_mp = CalculatePercentage(x.CostoMP_Real, x.CostoMpxItem));
            data.ForEach(x => x.diference_costo_mp_kg = CheckIfIsInfinity((x.CostoMpxItem / x.PesoxItem) - (x.CostoMP_Real / x.Peso_real)));
            data.ForEach(x => x.percentage_costo_mp_kg = CalculatePercentage((x.CostoMP_Real / x.Peso_real), (x.CostoMpxItem / x.PesoxItem)));

            data.ForEach(x => x.diference_costo_mod = CheckIfIsInfinity(x.ValorMOD_item - x.ValorMOD_real));
            data.ForEach(x => x.percentage_costo_mod = CalculatePercentage( x.ValorMOD_real, x.ValorMOD_item));
            data.ForEach(x => x.diference_costo_mod_kg = CheckIfIsInfinity((x.ValorMOD_item / x.PesoxItem) - (x.ValorMOD_real / x.Peso_real)));
            data.ForEach(x => x.percentage_costo_mod_kg = CalculatePercentage((x.ValorMOD_real / x.Peso_real), (x.ValorMOD_item / x.PesoxItem)));

            data.ForEach(x => x.diference_costo_cif = CheckIfIsInfinity(x.ValorCIF_Item - x.ValorCIF_real));
            data.ForEach(x => x.percentage_costo_cif = CalculatePercentage(x.ValorCIF_real, x.ValorCIF_Item));
            data.ForEach(x => x.diference_costo_cif_kg = CheckIfIsInfinity((x.ValorCIF_Item / x.PesoxItem) - (x.ValorCIF_real / x.Peso_real)));
            data.ForEach(x => x.percentage_costo_cif_kg = CalculatePercentage((x.ValorCIF_real / x.Peso_real), (x.ValorCIF_Item / x.PesoxItem)));

            SimuladorComparativo nivel1 = data.Where(x => x.NivelAccesorios == 1).FirstOrDefault();
            SimuladorComparativo nivel2 = data.Where(x => x.NivelAccesorios == 2).FirstOrDefault();
            SimuladorComparativo nivel3 = data.Where(x => x.NivelAccesorios == 3).FirstOrDefault();
            SimuladorComparativo nivel4 = data.Where(x => x.NivelAccesorios == 4).FirstOrDefault();
            SimuladorComparativo nivel5 = data.Where(x => x.NivelAccesorios == 5).FirstOrDefault();
            Dictionary<string, object> response = new Dictionary<string, object>();
            float costoTotalAcero = data.Where(x => x.NivelAccesorios != 1).Select(x => x.CostoxItem).Sum();
            float costoRealTotalAcero = data.Where(x => x.NivelAccesorios != 1).Select(x => x.Costo_real).Sum();
            float piezasTotalAcero = data.Where(x => x.NivelAccesorios != 1).Select(x => x.CantPiezas).Sum();
            float piezasRealTotalAcero = data.Where(x => x.NivelAccesorios != 1).Select(x => x.Unidades_real).Sum();
            float kgTotalAcero = data.Where(x => x.NivelAccesorios != 1).Select(x => x.PesoxItem).Sum();
            float kgRealTotalAcero = data.Where(x => x.NivelAccesorios != 1).Select(x => x.Peso_real).Sum();
            float costoKgTotalAcero = data.Where(x => x.NivelAccesorios != 1).Select(x => (x.CostoxItem / x.PesoxItem)).Sum();
            float costoKgRealTotalAcero = data.Where(x => x.NivelAccesorios != 1).Select(x => (x.Costo_real / x.Peso_real)).Sum();
            costoKgTotalAcero = CheckIfIsInfinity(costoKgTotalAcero);
            costoKgRealTotalAcero = CheckIfIsInfinity(costoKgRealTotalAcero);

            float diference_costoTotalAcero = CheckIfIsInfinity(costoTotalAcero - costoRealTotalAcero);
            float percentage_costoTotalAcero = CalculatePercentage(costoTotalAcero, costoRealTotalAcero);

            float diference_piezasTotalAcero = CheckIfIsInfinity(piezasTotalAcero - piezasRealTotalAcero);
            float percentage_piezasTotalAcero = CalculatePercentage(piezasTotalAcero, piezasRealTotalAcero);

            float diference_kgTotalAcero = CheckIfIsInfinity(kgTotalAcero - kgRealTotalAcero);
            float percentage_kgTotalAcero = CalculatePercentage(kgTotalAcero, kgRealTotalAcero);

            response.Add("FechaSimulacion", data.FirstOrDefault().FechaSimulacion.ToShortDateString());
            response.Add("FechaCargue", data.FirstOrDefault().FechaCargue);
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