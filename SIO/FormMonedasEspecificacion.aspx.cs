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
    public partial class FormMonedasEspecificacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        public static string ObtenerMonedas()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            List<Moneda_DTO> data = ControlDatos.EjecutarStoreProcedureConParametros<Moneda_DTO>("USP_fup_SEL_Monedas_Especificacion", parametros).ToList();
            return JsonConvert.SerializeObject(data);
        }

        [WebMethod(EnableSession = true)]
        public static string ActualizarMonedas(Moneda_DTO item)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string msgCambio = string.Empty;

            parametros.Add("@pId", item.Id);
            parametros.Add("@pDescripcion", item.Descripcion);
            parametros.Add("@pActivo", item.Activo);
            parametros.Add("@pSimbolo", item.Simbolo);
            parametros.Add("@pERP", item.ERP);
            parametros.Add("@pSepDecimal", item.SeparadorDecimal);
            parametros.Add("@pSepMiles", item.SeparadorMiles);
            parametros.Add("@pDecimales", item.CantidadDecimales);
            try
            {
                // This always returns the last row, used for inserts
                Moneda_DTO data = ControlDatos.EjecutarStoreProcedureConParametros<Moneda_DTO>("USP_fup_UPD_Monedas_Especificacion", parametros).FirstOrDefault();
                return JsonConvert.SerializeObject(data);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}