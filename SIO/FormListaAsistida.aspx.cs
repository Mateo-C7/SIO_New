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
    public partial class FormListaAsistida : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scripManager = ScriptManager.GetCurrent(this.Page);
            scripManager.EnablePageMethods = true;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }

        [WebMethod]
        public static string ObtenerReferencias()
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            List<ListaAsistidaReferenciaConsulta> data = ControlDatos.EjecutarStoreProcedureConParametros<ListaAsistidaReferenciaConsulta>("USP_fup_SEL_ListaAsistidaReferencias", parametros);

            string response = JsonConvert.SerializeObject(data);
            return response;
        }

        [WebMethod]
        public static string ObtenerProductos(int fupId, string version)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pFupId", fupId);
            parametros.Add("@pVersion", version);

            string query = @"SELECT [fup_tipo_venta_proy_id] id ,[descripcion] descripcion 
	                        ,[PrefijoListaAsistida], AccsListaAsistida, EspanListaAsistida  
                        FROM fup_enc_entrada_cotizacion e 
                        INNER JOIN fup_tipo_venta_proyecto AS c ON c.fup_tipo_venta_proy_id = E.eect_tipo_venta_proy_id
                        WHERE eect_fup_id = @pFupId AND eect_vercot_id = @pVersion";

            List<datosProducto> TipoProducto = ControlDatos.EjecutarConsulta<datosProducto>(query, parametros);
            return JsonConvert.SerializeObject(TipoProducto);
        }

        [WebMethod]
        public static void Simular(int fupId, string version)
        {
            ControlFUP controlFup = new ControlFUP();
            string NombreUsu = (string)HttpContext.Current.Session["Nombre_Usuario"];
            int idCt;
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pFupId", fupId);
            parametros.Add("@pVersion", version);
            string query = @"UPDATE fup_enc_entrada_cotizacion SET eect_SimulacionListaAsistida = 1 WHERE 
                                eect_fup_id = @pFupId AND eect_vercot_id = @pVersion";
            ControlDatos.EjecutarConsulta<int>(query, parametros);

            #region CreaOrden CT
            // Borrar datos de tablas si existen
            parametros.Clear();
            parametros.Add("@pFupID", fupId);
            parametros.Add("@pversion", version);
            parametros.Add("@pUsuario", NombreUsu);
            idCt = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_INS_Orden_Cotizacion", parametros);
            #endregion

            #region Crear Saldos
            // Borrar datos de tablas si existen
            parametros.Clear();
            parametros.Add("@pFupID", fupId);
            parametros.Add("@pversion", version);
            parametros.Add("@pUsuario", NombreUsu);
            idCt = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_INS_PiezasSaldo_CT", parametros);
            #endregion

            #region Explosionar
            // Borrar datos de tablas si existen
            parametros.Clear();
            parametros.Add("@pFupID", fupId);
            parametros.Add("@pversion", version);
            idCt = ControlDatos.GuardarStoreProcedureConParametros("USP_SimuladorProyecto_Sugiere", parametros);
            #endregion

            #region ActualizarEntradaCot
            // Actualizar Valores de FUP
            parametros.Clear();
                parametros.Add("@pFupID", fupId);
                parametros.Add("@pVersion", version);
                parametros.Add("@pUsuario", NombreUsu);
                parametros.Add("@pSistemaSeguridad", "0");
                parametros.Add("@pAlturaLibre", "0");
                parametros.Add("@pEstado", 4);  // Estado Aprobado
                ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_ent_cotizacionTS", parametros);
            #endregion

            #region Actualizar Valores de Carta
            // Actualizar Valores de FUP
            parametros.Clear();
            parametros.Add("@pFupID", fupId);
            parametros.Add("@pVersion", version);
            idCt = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_CartaCierre_TEMP_V2", parametros);
            #endregion

            // Crear Salida Cotizacion 
            parametros.Clear();
            parametros.Add("@pFupID", fupId);
            parametros.Add("@pVersion", version);
            parametros.Add("@pUsuario", NombreUsu);
            parametros.Add("@pEquipo_m2", 0);
            parametros.Add("@pEuipo_valor", 0);
            parametros.Add("@pAdicional_m2", 0);
            parametros.Add("@pAdicional_valor", 0);
            parametros.Add("@pDetalleArq_m2", 0);
            parametros.Add("@pDetalleArq_valor", 0);
            parametros.Add("@pSisSeguridad_per", 0);
            parametros.Add("@pSisSeguridad_valor", 0);
            parametros.Add("@pAcc_basico_valor", 0);
            parametros.Add("@pAcc_complementaio_valor", 0);
            parametros.Add("@pAcc_opcional_valor", 0);
            parametros.Add("@pAcc_adicional_valor", 0);
            parametros.Add("@pOtros_productos_valor", 0);
            parametros.Add("@pCont20", 0);
            parametros.Add("@pCont40", 0);
            parametros.Add("@pTipoSalida", 1);

            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_salida_cotizacionN", parametros);

            #region Cotizar la Lista Asistida
            // Actualizar Valores de FUP
            parametros.Clear();
            parametros.Add("@pFupID", fupId);
            parametros.Add("@pVersion", version);
            parametros.Add("@pUsuario", NombreUsu);
            idCt = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_CartaCierre_cotizar", parametros);
            #endregion
            string response = string.Empty;
            int evento = 5;
            response = FormFUP.CorreoFUP(Convert.ToInt32(fupId), version, evento);

            }

        [WebMethod]
        public static string ConsultarSimulacion(int fupId, string version)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pFupId", fupId);
            parametros.Add("@pVersion", version);
            string query = @"SELECT eect_SimulacionListaAsistida FROM fup_enc_entrada_cotizacion WHERE 
                                eect_fup_id = @pFupId AND eect_vercot_id = @pVersion";
            return JsonConvert.SerializeObject(ControlDatos.EjecutarConsulta<SimulacionEntradaCotizacion>(query, parametros)[0].eect_SimulacionListaAsistida);
        }

        [WebMethod]
        public static string ObtenerItems(int fupId, string fupVersion)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pFupId", fupId);
            parametros.Add("@pVersion", fupVersion);
            List<ItemsListaAsistida> data = ControlDatos.EjecutarStoreProcedureConParametros<ItemsListaAsistida>("USP_fup_SEL_ItemsListaAsistida", parametros);

            string response = JsonConvert.SerializeObject(data);
            return response;
        }

        [WebMethod]
        public static string ObtenerItemsToExport(int fupId, string fupVersion)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pFupId", fupId);
            parametros.Add("@pVersion", fupVersion);
            List<ItemsListaAsistidaToExport> data = ControlDatos.EjecutarStoreProcedureConParametros<ItemsListaAsistidaToExport>("USP_fup_SEL_ItemsListaAsistidaToExport", parametros);

            string response = JsonConvert.SerializeObject(data);
            return response;
        }

        [WebMethod]
        public static void BorrarItems(int fupId, string fupVersion)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pFupId", fupId);
            parametros.Add("@pVersion", fupVersion);
            List<RespuestaFilasAfectadas> data = ControlDatos.EjecutarStoreProcedureConParametros<RespuestaFilasAfectadas>("USP_fup_DEL_ItemsListaAsistida", parametros);
        }

        [WebMethod]
        public static void InsertarItems(int fupId, string fupVersion, List<ItemsListaAsistida> items)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            List<RespuestaFilasAfectadas> data;

            // Limpiar Datos Anteriores
            parametros.Add("@pFupId", fupId);
            parametros.Add("@pVersion", fupVersion);
            data = ControlDatos.EjecutarStoreProcedureConParametros<RespuestaFilasAfectadas>("USP_fup_DEL_ItemsListaAsistida", parametros);

            foreach (ItemsListaAsistida item in items)
            {
                parametros.Clear();
                parametros.Add("@pFupId", fupId);
                parametros.Add("@pVersion", fupVersion);
                parametros.Add("@pCantidad", item.ItemCantidad);
                parametros.Add("@pReferenciaId", item.RefCodigoId);
                parametros.Add("@pAncho1", item.ItemAncho1);
                parametros.Add("@pAlto1", item.ItemAlto1);
                parametros.Add("@pAncho2", item.ItemAncho2);
                parametros.Add("@pAlto2", item.ItemAlto2);
                parametros.Add("@pDescAux", item.ItemDescAux);
                parametros.Add("@pM2", item.ItemM2);
                parametros.Add("@pTotalM2", item.ItemTotalM2);
                parametros.Add("@pIdProducto", item.IdProducto);

                data = ControlDatos.EjecutarStoreProcedureConParametros<RespuestaFilasAfectadas>("USP_fup_UPD_ItemsListaAsistida", parametros);
            }
        }
    }
}