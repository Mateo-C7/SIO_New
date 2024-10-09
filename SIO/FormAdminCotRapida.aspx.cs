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
    public partial class FormAdminCotRapida : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod(EnableSession = true)]
        public static string ObtenerItems()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            List<Cot_Rap_Items_Consulta> data = ControlDatos.EjecutarStoreProcedureConParametros<Cot_Rap_Items_Consulta>("USP_fup_SEL_Cotrap_Items", parametros).ToList();
            return JsonConvert.SerializeObject(data);
        }

        [WebMethod(EnableSession = true)]
        public static string ObtenerPrecios()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            List<Cot_Rap_Precio_Consulta> data = ControlDatos.EjecutarStoreProcedureConParametros<Cot_Rap_Precio_Consulta>("USP_fup_SEL_Cotrap_Precios", parametros).ToList();
            return JsonConvert.SerializeObject(data);
        }

        [WebMethod(EnableSession = true)]
        public static string ObtenerFactoresArmado()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            List<Cot_Rap_Factores_Armado_Consulta> data = ControlDatos.EjecutarStoreProcedureConParametros<Cot_Rap_Factores_Armado_Consulta>("USP_fup_SEL_Cotrap_Factores_Armado", parametros).ToList();
            return JsonConvert.SerializeObject(data);
        }

        [WebMethod(EnableSession = true)]
        public static string ObtenerTipoVivienda()
        {
            return "";
        }

        [WebMethod(EnableSession = true)]
        public static string ObtenerPaises()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            List<Cot_Rap_Paises_Consulta> data = ControlDatos.EjecutarStoreProcedureConParametros<Cot_Rap_Paises_Consulta>("USP_fup_SEL_Cotrap_Paises", parametros).ToList();
            return JsonConvert.SerializeObject(data);
        }

        [WebMethod(EnableSession = true)]
        public static string ActualizarItem(Cot_Rap_Items_UPD_Ins item)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string nombre_usuario = (string)HttpContext.Current.Session["Nombre_Usuario"];

            parametros.Add("@pId", item.Id);
            parametros.Add("@pCodItem", item.CodItem);
            parametros.Add("@pItemCotRapida", item.Item);
            parametros.Add("@pIdCodGrupo", item.CodGrupo);
            parametros.Add("@pGrupo", item.Grupo);
            parametros.Add("@pDescripcion", item.Descripcion);
            parametros.Add("@pUsuario", nombre_usuario);
            Cot_Rap_Items_Consulta response = ControlDatos.EjecutarStoreProcedureConParametros<Cot_Rap_Items_Consulta>("USP_fup_UPD_Cotrap_Items", parametros).FirstOrDefault();
            return JsonConvert.SerializeObject(response);
        }

        [WebMethod(EnableSession = true)]
        public static string ActualizarPrecio(Cot_Rap_Precio_UPD_INS item)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string nombre_usuario = (string)HttpContext.Current.Session["Nombre_Usuario"];

            parametros.Add("@pId", item.Id);
            parametros.Add("@pIdItemCotRapida", item.IdItemCotRapida);
            if (item.IdPais.HasValue) { parametros.Add("@pIdPais", item.IdPais.Value); }
            parametros.Add("@pPrecio", item.Precio);
            parametros.Add("@pUsuario", nombre_usuario);
            Cot_Rap_Precio_Consulta response = ControlDatos.EjecutarStoreProcedureConParametros<Cot_Rap_Precio_Consulta>("USP_fup_UPD_Cotrap_Precios", parametros).FirstOrDefault();
            return JsonConvert.SerializeObject(response);
        }

        [WebMethod(EnableSession = true)]
        public static string ActualizarFactorArmado(Cot_Rap_Factores_Armado_UPD_INS item)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string nombre_usuario = (string)HttpContext.Current.Session["Nombre_Usuario"];

            parametros.Add("@pId", item.Id);
            parametros.Add("@pIdItemCotRapida", item.IdItemCotRapida);
            parametros.Add("@pIdPais", item.IdPais);
            parametros.Add("@pIdTipoVivienda", item.IdTipoVivienda);
            parametros.Add("@pFactor", item.Factor);
            parametros.Add("@pUsuario", nombre_usuario);
            Cot_Rap_Factores_Armado_Consulta response = ControlDatos.EjecutarStoreProcedureConParametros<Cot_Rap_Factores_Armado_Consulta>("USP_fup_UPD_Cotrap_Factores_Armado", parametros).FirstOrDefault();
            return JsonConvert.SerializeObject(response);
        }
    }
}