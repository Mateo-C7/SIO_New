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
    public partial class FormSimuladorComparativoFiltros : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scripManager = ScriptManager.GetCurrent(this.Page);
            scripManager.EnablePageMethods = true;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }

        [WebMethod]
        public static string GetComparative(int countryId, int cityId, int clientId, int yearId,
            int monthId)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pPaisId", countryId);
            parametros.Add("@pCiudadId", cityId);
            parametros.Add("@pClienteId", clientId);
            parametros.Add("@pAnioId", yearId);
            parametros.Add("@pMesId", monthId);


            List<SimuladorComparativo> data = ControlDatos.EjecutarStoreProcedureConParametros<SimuladorComparativo>("USP_SimuladorProyecto_ComparativoReal_Acum", parametros);
            data.ForEach(x => x.diference_tasa_dolar = CheckIfIsInfinity(x.tasaMonedaCotizacion - x.tasaDolar));
            data.ForEach(x => x.percentage_tasa_dolar = CalculatePercentage(x.tasaDolar, x.tasaMonedaCotizacion));
            data.ForEach(x => x.diference_m2 = CheckIfIsInfinity(x.M2xItem - x.M2_real));
            data.ForEach(x => x.percentage_m2 = CalculatePercentage(x.M2_real, x.M2xItem));
            data.ForEach(x => x.diference_piezas = CheckIfIsInfinity(x.CantPiezas - x.Unidades_real));
            data.ForEach(x => x.percentage_piezas = CalculatePercentage(x.Unidades_real, x.CantPiezas));
            data.ForEach(x => x.diference_peso = CheckIfIsInfinity(x.PesoxItem - x.Peso_real));
            data.ForEach(x => x.percentage_peso = CalculatePercentage(x.Peso_real, x.PesoxItem));
            data.ForEach(x => x.diference_costo = CheckIfIsInfinity(x.CostoxItem - x.Costo_real));
            data.ForEach(x => x.percentage_costo = CalculatePercentage(x.Costo_real, x.CostoxItem));
            data.ForEach(x => x.diference_costo_kg = CheckIfIsInfinity((x.CostoxItem / x.PesoxItem) - (x.Costo_real / x.Peso_real)));
            data.ForEach(x => x.percentage_costo_kg = CalculatePercentage((x.Costo_real / x.Peso_real), (x.CostoxItem / x.PesoxItem)));
            data.ForEach(x => x.diference_piezas_m2 = CheckIfIsInfinity((x.CantPiezas / x.M2xItem) - (x.Unidades_real / x.M2_real)));
            data.ForEach(x => x.percentage_piezas_m2 = CalculatePercentage((x.Unidades_real / x.M2_real), (x.CantPiezas / x.M2xItem)));
            data.ForEach(x => x.diference_kg_m2 = CheckIfIsInfinity((x.PesoxItem / x.M2xItem) - (x.Peso_real / x.M2_real)));
            data.ForEach(x => x.percentage_kg_m2 = CalculatePercentage((x.Peso_real / x.M2_real), (x.PesoxItem / x.M2xItem)));
            data.ForEach(x => x.diference_costo_m2 = CheckIfIsInfinity((x.CostoxItem / x.M2xItem) - (x.Costo_real / x.M2_real)));
            data.ForEach(x => x.percentage_costo_m2 = CalculatePercentage((x.Costo_real / x.M2_real), (x.CostoxItem / x.M2xItem)));

            data.ForEach(x => x.diference_costo_mp = CheckIfIsInfinity(x.CostoMpxItem - x.CostoMP_Real));
            data.ForEach(x => x.percentage_costo_mp = CalculatePercentage(x.CostoMP_Real, x.CostoMpxItem));
            data.ForEach(x => x.diference_costo_mp_kg = CheckIfIsInfinity((x.CostoMpxItem / x.PesoxItem) - (x.CostoMP_Real / x.Peso_real)));
            data.ForEach(x => x.percentage_costo_mp_kg = CalculatePercentage((x.CostoMP_Real / x.Peso_real), (x.CostoMpxItem / x.PesoxItem)));

            data.ForEach(x => x.diference_costo_mod = CheckIfIsInfinity(x.ValorMOD_item - x.ValorMOD_real));
            data.ForEach(x => x.percentage_costo_mod = CalculatePercentage(x.ValorMOD_real, x.ValorMOD_item));
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

        [WebMethod(EnableSession = true)]
        public static string GetCountries()
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

        [WebMethod(EnableSession = true)]
        public static string GetCitiesFromCountry(string idPais)
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
            string response = JsonConvert.SerializeObject(lstCiudad);
            return response;
        }

        [WebMethod]
        public static string GetClientsFromCity(string idCiudad)
        {
            string IdClienteUsuario = "0";

            if (HttpContext.Current.Session["IdClienteUsuario"] != null)
            {
                IdClienteUsuario = HttpContext.Current.Session["IdClienteUsuario"].ToString();
            }

            ControlCliente control = new ControlCliente();
            Dictionary<string, object> queryResult = new Dictionary<string, object>();
            List<CapaControl.Entity.Cliente> lstObject = control.obtenerDatosCliente(int.Parse(idCiudad), int.Parse(IdClienteUsuario));
            queryResult.Add("varCliente", lstObject);
            string response = JsonConvert.SerializeObject(lstObject);
            return response;
        }

        private static float CalculatePercentage(float x, float y)
        {
            if (y == 0)
            {
                return float.NaN;
            }
            else
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
    }
}