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
    public partial class FormSalidaCotizacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scripManager = ScriptManager.GetCurrent(this.Page);
            scripManager.EnablePageMethods = true;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            if (Request.QueryString["fup"] != null)
            {
                string IdFUP = Request.QueryString["fup"];
                Session["IdFUP"] = IdFUP;
            }
            else
                Session["IdFUP"] = -1;

        }

        // Método copiado de FormFup.aspx.cs
        [WebMethod]
        public static string ObtenerFlete(string idFup, string idVersion)
        {
            ControlFUP controlFup = new ControlFUP();
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string response = string.Empty;

            parametros.Add("@pFupID", Convert.ToInt32(idFup));
            parametros.Add("@pVersion", idVersion);

            List<Calculoflete> data = ControlDatos.EjecutarStoreProcedureConParametros<Calculoflete>("USP_fup_SEL_det_fletes_N", parametros);

            response = JsonConvert.SerializeObject(data.FirstOrDefault());
            return response;
        }

        [WebMethod(EnableSession = true)]
        public static bool AutorizarVerPrecio(int fupId, string version)
        {
            version = version.Trim();
            string query = @"UPDATE [Forsa].[dbo].[fup_enc_entrada_cotizacion] SET [eect_AutorizaVerPrecio] = 1 
                            WHERE [eect_fup_id] = " + fupId.ToString() + " AND [eect_vercot_id] LIKE '" + version + "'";
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            List<RespuestaFilasAfectadas> affectedRows = ControlDatos.EjecutarConsulta<RespuestaFilasAfectadas>(query, new Dictionary<string, object>());
            if (affectedRows.Count > 0)
            {
                return true;
            }
            return false;
        }

        [WebMethod(EnableSession = true)]
        public static bool NegarVerPrecio(int fupId, string version)
        {
            version = version.Trim();
            string query = @"UPDATE [Forsa].[dbo].[fup_enc_entrada_cotizacion] SET [eect_AutorizaVerPrecio] = 0 
                            WHERE [eect_fup_id] = " + fupId.ToString() + " AND [eect_vercot_id] LIKE '" + version + "'";
            List<RespuestaFilasAfectadas> affectedRows = ControlDatos.EjecutarConsulta<RespuestaFilasAfectadas>(query, new Dictionary<string, object>());
            if (affectedRows.Count > 0)
            {
                return true;
            }
            return false;
        }

        [WebMethod(enableSession: true)]
        public static string obtenerVersionPorIdFup(string idFup)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            ControlFUP controlFup = new ControlFUP();
            string response = string.Empty;
            string representanteComercialId = (string)HttpContext.Current.Session["rcID"];
            int rol = (int)HttpContext.Current.Session["Rol"];
            int Consultar = -1;

            parametros.Add("@pFupID", Convert.ToInt32(idFup));
            parametros.Add("@pRepId", Convert.ToInt32(representanteComercialId));

            List<formato_unico> formatoUnico = ControlDatos.EjecutarStoreProcedureConParametros<formato_unico>("USP_fup_SEL_FUPporID", parametros);
            if (formatoUnico.Count > 0)
            {
                Consultar = Convert.ToInt32(idFup);
            }
            List<VersionFup> lstVersion = controlFup.obtenerVersionesFUP(Consultar);
            response = JsonConvert.SerializeObject(lstVersion);
            return response;
        }

        [WebMethod]
        public static string ObtenerCalculadoraComercialResumen(ObtenerItemDinamico item)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pFupID", item.pFupID);
            parametros.Add("@pVersion", item.pVersion);
            parametros.Add("@Idioma", item.Idioma);
            parametros.Add("@pImprime", 1);

            List<CotizacionFUP> data = ControlDatos.EjecutarStoreProcedureConParametros<CotizacionFUP>("USP_fup_SEL_CartaCierre_partes", parametros);

            string response = JsonConvert.SerializeObject(data.Where(x => x.TipoRegistro.HasValue 
                && new[] { 13, 6, 7, 11, 9 }.Contains(x.TipoRegistro.Value)));
            return response;
        }

        [WebMethod]
        public static string ObtenerLineasDinamicas(ObtenerItemDinamico item)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pFupID", item.pFupID);
            parametros.Add("@pVersion", item.pVersion);
            parametros.Add("@Idioma", item.Idioma);
            parametros.Add("@pImprime", 1);

//            List<CotizacionFUP> data = ControlDatos.EjecutarStoreProcedureConParametros<CotizacionFUP>("USP_fup_SEL_CartaCierre_partes", parametros).OrderBy(x => x.IdGrupo).OrderBy(x => x.Consecutivo).OrderBy(x => x.Orden).ToList();
            List<CotizacionFUP> data = ControlDatos.EjecutarStoreProcedureConParametros<CotizacionFUP>("USP_fup_SEL_CartaCierre_partes", parametros);

            //data.Where(x => !string.IsNullOrEmpty(x.DomLista)).ToList().ForEach(x =>
            //    x.dominio = ControlDominios.obtener_dominios(x.DomLista)
            //);

            string response = JsonConvert.SerializeObject(data);
            return response;
        }

        [WebMethod]
        public static string ObtenerSalidaCot(int idFup, string idVersion)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            #region ConsultarSalidaCotizacion
            parametros.Clear();
            parametros.Add("@pFupID", Convert.ToInt32(idFup));
            parametros.Add("@pVersion", idVersion.Trim());
            parametros.Add("@pTipoSalida", 1);
            List<fup_salida_cotizacion> listaSalidaCot = ControlDatos.EjecutarStoreProcedureConParametros<fup_salida_cotizacion>("USP_fup_SEL_salida_cotizacionN", parametros);
            listaSalidaCot.ForEach(t => t.total_m2 = Math.Round(t.m2_equipo + t.m2_adicionales + t.m2_Detalle_arquitectonico, 2).ToString());
            listaSalidaCot.ForEach(t => t.total_valor = Math.Round(t.vlr_equipo + t.vlr_adicionales + t.vlr_Detalle_arquitectonico, 2).ToString());
            listaSalidaCot.ForEach(t => t.total_propuesta_com = Math.Round(t.vlr_equipo + t.vlr_adicionales + t.vlr_sis_seguridad + t.vlr_Detalle_arquitectonico + t.vlr_accesorios_basico + t.vlr_accesorios_complementario + t.vlr_accesorios_opcionales + t.vlr_otros_productos, 2).ToString());
            #endregion

            string response = JsonConvert.SerializeObject(listaSalidaCot);
            return response;
        }

        [WebMethod]
        public static string GuardarLineasDinamicas(List<CotizacionFUP> data)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            int id = 0;
            string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];

            foreach (CotizacionFUP item in data)
            {
                parametros.Clear();
                parametros.Add("@pFupID", item.IdFUP);
                parametros.Add("@pVersion", item.Version);
                parametros.Add("@pItemCotiza_id", Convert.ToInt32(item.ItemCotiza_id));
                parametros.Add("@pItem_det", item.Item_det);
                parametros.Add("@pItem_id", item.item_id);
                parametros.Add("@pIncluir", item.Incluir);
                parametros.Add("@pCantidad", item.Cantidad);
                parametros.Add("@pValor", item.Valor ?? 0);
                parametros.Add("@pDescuento", item.Descuento ?? 0);
                parametros.Add("@pObservacion", item.Observacion ?? "");
                parametros.Add("@pUsuario", NombreUsu);
                parametros.Add("@pIdCartaCierre", item.IdCartaCierre);
                parametros.Add("@pConsecutivo", item.Consecutivo ?? 0);

                id = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_CartaCierre_partes", parametros);
            }
            string response = JsonConvert.SerializeObject(id.ToString());
            return response;
        }
   

        [WebMethod]
        public static string Cotizar(int IdFup, string Version)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            int id = 0;
            string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];

                parametros.Clear();
                parametros.Add("@pFupID", IdFup);
                parametros.Add("@pVersion", Version);
                parametros.Add("@pUsuario", NombreUsu);

                id = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_CartaCierre_cotizar", parametros);            
            string response = JsonConvert.SerializeObject(id.ToString());
            return response;
        }

    }

}