using CapaControl;
using CapaControl.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIO
{
    public partial class FormPQRS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scripManager = ScriptManager.GetCurrent(this.Page);
            scripManager.EnablePageMethods = true;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }

        [WebMethod]
        public static string obtenerVersionPorOrdenFabricacion(string idOrdenFabricacion)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            string response = string.Empty;
            FUPResumen fup = controlPQRS.obtenerFUPporOrdenFabricacion(idOrdenFabricacion);
            response = JsonConvert.SerializeObject(fup);
            return response;
        }

        [WebMethod]
        public static string ObtenerHallazgosPorOrdenFabricacion(string idOrdenFabricacion)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            FUPResumen fup = controlPQRS.obtenerFUPporOrdenFabricacion(idOrdenFabricacion);
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            #region ConsultarHallazgosObra
            parametros.Clear();
            parametros.Add("@pFupID", fup.IdFup);
            parametros.Add("@pVersion", fup.Version);
            parametros.Add("@pTipo", 3);
            List<fup_considerationobservation_consulta> lisHallazgosObra = ControlDatos.EjecutarStoreProcedureConParametros<fup_considerationobservation_consulta>("USP_fup_SEL_hvp_Observaciones", parametros);
            lisHallazgosObra = lisHallazgosObra.Where(x => x.IdPQRS == null).ToList();
            #endregion
            return JsonConvert.SerializeObject(lisHallazgosObra);
        }


        [WebMethod]
        public static string ObtenerFuentes()
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            string response = string.Empty;
            List<PQRSFuente> fuentes = controlPQRS.ObtenerFuentesActivas();
            List<PQRSTipo> tipos = controlPQRS.ObtenerTiposPQRS();
            List<PQRSTipoFuente> tipofuentes = controlPQRS.ObtenerTipoFuentesActivas();
            List<PQRSTipo> subTipoQuejas= controlPQRS.ObtenerSubTiposPQRS((int)TipoPQRS.Queja);
            List<PQRSTipo> subTipoSolicitudes = controlPQRS.ObtenerSubTiposPQRS((int)TipoPQRS.Solicitud);
            List<PQRSTipo> subTipoSolicitudesTH = controlPQRS.ObtenerSubTiposPQRS(6);

            var res = new
            {
                fuentes = fuentes,
                tipos = tipos,
                tipofuentes = tipofuentes,
                subTipoQuejas = subTipoQuejas,
                subTipoSolicitudes = subTipoSolicitudes,
                subTipoSolicitudesTH = subTipoSolicitudesTH
            };
            response = JsonConvert.SerializeObject(res);
            return response;

        }

        [WebMethod(EnableSession = true)]
        public static string GuardarPQRS(PQRSDTO pqrs, string ordenes)
        {
            string response = string.Empty;
            string NombreUsu = (string)HttpContext.Current.Session["Usuario"];
            string email = (string)HttpContext.Current.Session["rcEmail"];
            pqrs.UsuarioCreacion = NombreUsu;
            ControlPQRS controlPQRS = new ControlPQRS();
            int id = controlPQRS.GuardarPQRS(pqrs);
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pIdPQRS", id);
            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_PQRS_UPD_DatosOrden", parametros);

            parametros.Clear();
            parametros.Add("@pIdPQRS", id);
            parametros.Add("@pHallazgosIds", ordenes);
            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_PQRS_AsociarPQRSToHallazgos", parametros);
            response = id.ToString();
            if(pqrs.TipoPQRSId == (int)TipoPQRS.Reclamo)
            {
                EnviarCorreoReclamoEnElaboracion(id.ToString(), email);
            }
            return response;
        }

        private static void EnviarCorreoReclamoEnElaboracion(string idPQRS, string email)
        {
            ControlPQRS controlPQRS = new ControlPQRS();
            PQRSDTOConsulta pqrsDto = controlPQRS.ObtenerPQRSId(idPQRS, email);
            FormPQRSConsulta.SendEmailsGeneral(pqrsDto, (int)EstadosPQRS.Elaboracion);
        }

        [WebMethod(EnableSession = true)]
        public static string obtenerListadoPaises()
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
        [WebMethod]
        public static string obtenerListadoClientesCiudad(string idCiudad)
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

        [WebMethod(EnableSession = true)]
        public static string obtenerListadoCiudadesMoneda(string idPais)
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


    }
}