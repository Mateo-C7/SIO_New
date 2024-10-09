using CapaControl;
using CapaControl.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;

namespace SIO
{
    /// <summary>
    /// Summary description for wsFUP
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/SecureWebService/SecureWebService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsFUP : WebService
    {

        #region Def Variables
        public Autenticacion Credencial;
        #endregion

        #region Clases para parametros Web Service
        public class Lfups
        {
            public int IdFUP { get; set; }
        }

        public class LCorreo
        {
            public string Correo { get; set; }
            public string NombreCorreo { get; set; }
        }

        public class fupGuardar
        {
            public int IdFUP { get; set; }
            public string Version { get; set; }
            public string Usuario { get; set; }
            public int rol { get; set; }
            public int ID_Cliente { get; set; }
            public int ID_Moneda { get; set; }
            public int ID_Contacto { get; set; }
            public int ID_Obra { get; set; }
            public int TipoCotizacion { get; set; }
            public int TerminoNegociacion { get; set; }
            public int Producto { get; set; }
            public int MaxPisos { get; set; }
            public int FundicionPisos { get; set; }
            public int NumeroEquipos { get; set; }
            public string AlturaLibre { get; set; }
            public string AlineacionVertical { get; set; }
            public int FormaConstructiva { get; set; }
            public string EspesorMuro { get; set; }
            public string EspesorLosa { get; set; }
            public string EnrasePuerta { get; set; }
            public string EnraseVentana { get; set; }
            public string TipoUnion { get; set; }
            public string escalera { get; set; }
            public int EnPrueba { get; set; }
        }

        public class fup_salida_cotizacion_WS
        {
            public decimal? m2_equipo { get; set; }
            public decimal? vlr_equipo { get; set; }
            public decimal? m2_adicionales { get; set; }
            public decimal? vlr_adicionales { get; set; }
            public decimal? m2_Detalle_arquitectonico { get; set; }
            public decimal? vlr_Detalle_arquitectonico { get; set; }
            public decimal? m2_sis_seguridad { get; set; }
            public decimal? vlr_sis_seguridad { get; set; }
            public decimal? vlr_accesorios_basico { get; set; }
            public decimal? vlr_accesorios_complementario { get; set; }
            public decimal? vlr_accesorios_opcionales { get; set; }
            public decimal? vlr_accesorios_adicionales { get; set; }
            public decimal? vlr_otros_productos { get; set; }
            public string total_m2 { get; set; }
            public string total_valor { get; set; }
            public string fupid { get; set; }
            public string version { get; set; }
            public string total_propuesta_com { get; set; }
            public int? vlr_Contenedor20 { get; set; }
            public int? vlr_Contenedor40 { get; set; }
            public int tipoSalida { get; set; }
        }

        public class ListaPrecio
        {
            public int? IdFUP { get; set; }
            public string IdPaisCliente { get; set; }
            public string IdGrupoPaisCliente { get; set; }
            public string GrupoPrecio { get; set; }
            public int? IdMoneda { get; set; }
            public string Moneda { get; set; }
            public decimal? Precio { get; set; }
        }

        public class PrecioItem
        {

            public string categoria { get; set; }
            public string moneda { get; set; }
            public decimal? valor { get; set; }
        }

        public class ProyectoTosc
        {
            public int? idPais { get; set; }
            public string pais { get; set; }
            public int? idCuidad { get; set; }
            public int? idContacto { get; set; }
            public string contacto { get; set; }
            public string ciudad { get; set; }
            public int? idcliente { get; set; }
            public string cliente { get; set; }
            public int? idProyecto { get; set; }
            public string Proyecto { get; set; }
            public int? fup { get; set; }
            public decimal? alturaLibre { get; set; }
            public decimal? espesorMuro { get; set; }
            public decimal? espesorLosa { get; set; }
            public decimal? enrasepuerta { get; set; }
            public decimal? enraseVentana { get; set; }
            public List<PrecioItem> Precios { get; set; }
        }

        public class AreaBase
        {
            public decimal? MUROS { get; set; }
            public decimal? UNION { get; set; }
            public decimal? LOSAS { get; set; }
        }

        public class Base
        {
            public AreaBase BASE { get; set; }
        }

        public class Pieza
        {
            public string Nombrepieza { get; set; }
            public decimal? cant { get; set; }
            public string descri { get; set; }
            public decimal? ancho { get; set; }
            public decimal? alto { get; set; }
            public string familia { get; set; }
            public decimal? areaItem { get; set; }
            public string id { get; set; }
        }

        public class Configuracion
        {
            public string modulacion { get; set; }
            public int? cantidad { get; set; }
            public string tipo_modulacion { get; set; }
        }

        public class Escalera
        {
            public string tipo { get; set; }
            public string ancho { get; set; }
            public string l1 { get; set; }
            public string l2 { get; set; }
            public string peldano { get; set; }
            public decimal? m2 { get; set; }
        }

        public class itemSC
        {
            public decimal? m2 { get; set; }
            public decimal? vr_unit { get; set; }
            public decimal? vr_total { get; set; }
        }

        public class alcanceSC
        {
            public itemSC equipo_base { get; set; }
            public itemSC adicionales { get; set; }
            public itemSC escalera { get; set; }
            public itemSC accesorios_basicos { get; set; }
            public itemSC accesorios_complementarios { get; set; }
            public itemSC equipo_seguridad { get; set; }
            public itemSC equipo_trepante { get; set; }
            public itemSC total_alcance_oferta { get; set; }
        }

        public class alcanceSCLis
        {
            public itemSC equipo_base { get; set; }
            public itemSC ag { get; set; }
            public itemSC agr { get; set; }
            public itemSC agrd { get; set; }
            public descuentos descuento { get; set; }
            public itemSC total_alcance_oferta { get; set; }
        }
 
        public class descuentos
        {
            public decimal? descuento_1 { get; set; }
            public decimal? descuento_2 { get; set; }
            public decimal? descuento_3 { get; set; }
            public decimal? descuento_seguridad { get; set; }
            public decimal? descuento_trepante { get; set; }
            public decimal? descuento_global { get; set; }
        }

        public class guardarToscana
        {
            public ProyectoTosc proyecto { get; set; }
            public Base areaTotal { get; set; }
            public List<Pieza> equipoBase { get; set; }
            public List<Pieza> adaptaciones { get; set; }
            public Configuracion configuracion { get; set; }
            public int? seguridad { get; set; }
            public int? trepante { get; set; }
            public descuentos descuento { get; set; }
            public decimal? impuesto { get; set; }
            public Escalera escalera { get; set; }
            public alcanceSC alcance { get; set; }

        }
        
        public class PiezaUV
        {
            public string Nombrepieza { get; set; }
            public decimal? cant { get; set; }
            public string descrip_aux { get; set; }
            public decimal? ancho { get; set; }
            public decimal? alto { get; set; }
            public decimal? ancho2 { get; set; }
            public decimal? alto2 { get; set; }
            public string familia { get; set; }
            public decimal? areaItem { get; set; }
            public string plano { get; set; }
            public string modulacion { get; set; }
            public int? consecutivo { get; set; }

        }

        public class ListaPiezas_UV
        {
            public int id_fup { get; set; }
            public string version_fup { get; set; }
            public string usuario { get; set; }
            public List<PiezaUV> piezas { get; set; }
        }

        public class AccesoriosUV
        {
            public string descripcion { get; set; }
            public decimal? cant { get; set; }
            public string descrip_aux { get; set; }
            public decimal? dim1 { get; set; }
            public decimal? dim2 { get; set; }
            public decimal? dim3 { get; set; }
            public decimal? dim4 { get; set; }
            public decimal? dim5 { get; set; }
            public decimal? dim6 { get; set; }
            public string plano { get; set; }
            public string modulacion { get; set; }
            public int? consecutivo { get; set; }

        }

        public class ListaAccesorios_UV
        {
            public int id_fup { get; set; }
            public string version_fup { get; set; }
            public string usuario { get; set; }
            public List<AccesoriosUV> piezas { get; set; }
        }

        public class ListaFup_UV
        {
            public int id_fup { get; set; }
            public string version_fup { get; set; }
            public int id_pais { get; set; }
            public string nombre_pais { get; set; }
            public int id_ciudad { get; set; }
            public string nombre_ciudad { get; set; }
            public int id_cliente { get; set; }
            public string nombre_cliente { get; set; }
            public int id_obra { get; set; }
            public string nombre_obra { get; set; }
            public int? id_contacto { get; set; }
            public string nombre_contacto { get; set; }
            public string fecha_creacion { get; set; }
        }

        public class ListaResumeForline_UV
        {
            public int id_fup { get; set; }
            public string version_fup { get; set; }
            public string usuario { get; set; }
            public List<ListaResForline_UV> piezas { get; set; }
        }

        public class ListaResForline_UV
        {
            public int idgrupoCotizacion { get; set; }
            public string grupoCotizacion { get; set; }
            public decimal? metros { get; set; }
            public decimal? metrosEsp { get; set; }
            public int modulacion { get; set; }
            public int? consecutivo { get; set; }

            //            public decimal? valor { get; set; }
            //            public decimal? metrosEsp { get; set; }
            //            public int modulacion { get; set; }
        }


        public class TopCliente_UV
        {
            public string nombre_cliente { get; set; }
            public int cantidad_fups { get; set; }
            public int comparacion_cantidad_fups { get; set; }
        }

        public class Estadisticas_UV
        {
            public int Anio { get; set; }
            public decimal ventas_totales { get; set; }
            public string comparacion_ventas_totales { get; set; }
            public int total_fups { get; set; }
            public int promedio_ventas { get; set; }
            public int cantidad_fups_probabilidad_cierrre_25 { get; set; }
            public int cantidad_fups_probabilidad_cierrre_50 { get; set; }
            public int cantidad_fups_probabilidad_cierrre_75 { get; set; }
            public int cantidad_fups_probabilidad_cierrre_100 { get; set; }
            public int ultimo_fup_id { get; set; }
            public string ultimo_fup_nombre_cliente { get; set; }
            public int cantidad_clientes { get; set; }
            public List<TopCliente_UV> top_clientes { get; set; }
        }
        public class usrLis
        {
            public string idUsuario { get; set; }
        }


        public class ProyectoToscLis
        {
            public int? idPais { get; set; }
            public int? idCuidad { get; set; }
            public int? idcliente { get; set; }
            public int? idProyecto { get; set; }
            public int? fup { get; set; }
            public usrLis user  { get; set; }
            public string version { get; set; }
            public string tipoCotizacion { get; set; }
            public string cliente { get; set; }
            public List<ListaPrecio> Precios { get; set; }
            public int? cotizado { get; set; }
            public alcanceSCLis alcance { get; set; }
            public List<Pieza> equipoBase { get; set; }

        }

        public class respCrm
        {
            public string crmdatos { get; set; }
        }

        public class CrmTotalVenta
        {
            public decimal ValorPpto { get; set; }
            public decimal ValorReal { get; set; }
        }

        public class CrmVentaMes
        {
            
            public int MesProyeccion { get; set; }
            public decimal ValorPpto { get; set; }
            public decimal ValorReal { get; set; }
        }

        public class Crmseguimiento
        {

            public string Seguimiento_cotiza { get; set; }
            public int CantClientes { get; set; }
            public int Cotizaciones { get; set; }
        }
        public class CrmProbabilidad
        {

            public string Probabilidad { get; set; }
            public int CantClientes { get; set; }
            public int Cotizaciones { get; set; }
        }
        
        public class crmdata
        {
            public List<CrmTotalVenta> TotalVenta { get; set; }
            public List<CrmVentaMes> VentaMes { get; set; }
            public List<Crmseguimiento> Seguimiento { get; set; }
            public List<CrmProbabilidad> Probabilidad { get; set; }
        }

        public class datosCombo2
        {
            public string id { get; set; }
            public string descripcion { get; set; }
            public string descripcionEN { get; set; }
            public string descripcionPO { get; set; }
        }
        #endregion

        #region Validacion Ingreso
        public static Boolean VerificarPermisos(Autenticacion validar)
        {
            if (validar == null)
            {
                return false;
            }
            else
            {
                //Verifica los permiso Ej. Consulta a BD
                if (validar.Usuario == "UsuarioForsa" && validar.Clave == "F0rs4_WS_2019GBI")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion

        [WebMethod, SoapHeader("Credencial")]
        public string getPaises(int IdRepresentante, int rol)
        {
            string response;

            if (VerificarPermisos(Credencial))
            {
                ControlUbicacion controlUbicacion = new ControlUbicacion();

                List<CapaControl.Entity.Pais> lstPais = null;
                if ((rol == 3) || (rol == 28) || (rol == 29) || (rol == 33) || (rol == 30))
                {
                    lstPais = controlUbicacion.obtenerListaPaisRepresentante(IdRepresentante);
                }
                else
                {
                    lstPais = controlUbicacion.obtenerListaPais();
                }
                response = JsonConvert.SerializeObject(lstPais);
            }
            else
            {
                response = "No Valido";
            }
            return response;
        }

        [WebMethod, SoapHeader("Credencial")]
        public string getCiudades(int idPais, int IdRepresentante, int rol)
        {
            string response;

            if (VerificarPermisos(Credencial))
            {
                ControlUbicacion controlUbicacion = new ControlUbicacion();
                Dictionary<string, object> queryResult = new Dictionary<string, object>();

                List<CapaControl.Entity.Ciudad> lstCiudad = null;

                if (((rol == 3) || (rol == 30) || (rol == 34) || (rol == 40)) && ((idPais == 8) || (idPais == 6) || (idPais == 21)))
                {
                    lstCiudad = controlUbicacion.obtenerCiudadesRepresentantesColombia(IdRepresentante);
                }
                else
                {
                    lstCiudad = controlUbicacion.obtenerListaCiudades(idPais);
                }

                response = JsonConvert.SerializeObject(lstCiudad);
            }
            else
            {
                response = "No Valido";
            }
            return response;
        }

        [WebMethod, SoapHeader("Credencial")]
        public string getClientesCiudad(string idCiudad, string IdCliente)
        {
            string response;

            if (VerificarPermisos(Credencial))
            {
                string IdClienteUsuario = "0";

                if (IdCliente != null)
                {
                    IdClienteUsuario = IdCliente.ToString();
                }

                ControlCliente control = new ControlCliente();
                Dictionary<string, object> queryResult = new Dictionary<string, object>();
                List<CapaControl.Entity.Cliente> lstObject = control.obtenerDatosCliente(int.Parse(idCiudad), int.Parse(IdClienteUsuario));
                response = JsonConvert.SerializeObject(lstObject);
            }
            else
            {
                response = "No Valido";
            }
            return response;
        }

        [WebMethod, SoapHeader("Credencial")]
        public string getContactosCliente(string idCliente)
        {
            string response;

            if (VerificarPermisos(Credencial))
            {
                ControlCliente controlCliente = new ControlCliente();
                List<CapaControl.Entity.ContactoCliente> lstContactoCliente = controlCliente.obtenerContactoCliente(int.Parse(idCliente));
                response = JsonConvert.SerializeObject(lstContactoCliente);
            }
            else
            {
                response = "No Valido";
            }
            return response;
        }

        [WebMethod, SoapHeader("Credencial")]
        public string getTipoEquipo()
        {
            string response;

            if (VerificarPermisos(Credencial))
            {
                List<datosCombo2> lstEquipos = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT        fdom_CodDominio as id, fdom_Descripcion as descripcion, fdom_DescripcionEN descripcionEN , fdom_DescripcionPO descripcionPO
																						FROM            fup_Dominios
																						WHERE        (fdom_Dominio = 'TIPO_EQUIPO')
																						ORDER BY fdom_OrdenDominio", new Dictionary<string, object>());
                response = JsonConvert.SerializeObject(lstEquipos);
            }
            else
            {
                response = "No Valido";
            }
            return response;
        }

        [WebMethod, SoapHeader("Credencial")]
        public string getObrasCliente(string idCliente)
        {
            string response;

            if (VerificarPermisos(Credencial))
            {
                ControlCliente controlCliente = new ControlCliente();
                ControlContacto controlContacto = new ControlContacto();
                List<CapaControl.Entity.ObraCliente> lstObraCliente = controlContacto.obtenerObrasDistPV(int.Parse(idCliente));
                response = JsonConvert.SerializeObject(lstObraCliente);
            }
            else
            {
                response = "No Valido";
            }
            return response;
        }

        [WebMethod, SoapHeader("Credencial")]
        public string getMonedas()
        {
            string response;

            if (VerificarPermisos(Credencial))
            {
                ControlUbicacion controlUbicacion = new ControlUbicacion();
                List<CapaControl.Entity.Moneda> lstMoneda = controlUbicacion.obtenerMoneda();
                response = JsonConvert.SerializeObject(lstMoneda);
            }
            else
            {
                response = "No Valido";
            }
            return response;
        }

        [WebMethod, SoapHeader("Credencial")]
        public string getTipoCotizacion()
        {
            string response;
            Dictionary<string, object> paramCotizacion = new Dictionary<string, object>() { { "@neg", 1 } };

            if (VerificarPermisos(Credencial))
            {
                List<datosCombo> TipoCotizacion = ControlDatos.EjecutarConsulta<datosCombo>(@"SELECT   ftco_id id, ftco_nombre descripcion
                                                                                          FROM [dbo].[fup_tipo_cotizacion]
                                                                                          WHERE ftco_uso_fup = 1 
                                                                                            and ftco_grupo_negociacion IN (SELECT ftne_grupo_negociacion
                                                                                                                           FROM [dbo].[fup_tipo_negociacion]
                                                                                                                           where ftne_id = 1)", paramCotizacion);

                response = JsonConvert.SerializeObject(TipoCotizacion);
            }
            else
            {
                response = "No Valido";
            }
            return response;
        }

        [WebMethod, SoapHeader("Credencial")]
        public string getTipoProducto()
        {
            string response;
            Dictionary<string, object> paramCotizacion = new Dictionary<string, object>() { { "@neg", 1 } };

            if (VerificarPermisos(Credencial))
            {
                List<datosCombo> TipoProducto = ControlDatos.EjecutarConsulta<datosCombo>(@"SELECT [fup_tipo_venta_proy_id] id
                                                                                              ,[descripcion] descripcion
                                                                                          FROM .[dbo].[fup_tipo_venta_proyecto]
                                                                                          where [activo] = 1  and Grupo_Negociacion IN (SELECT ftne_grupo_negociacion
                                                                                                                                                FROM [dbo].[fup_tipo_negociacion]
                                                                                                                                                where ftne_id = @neg) ", paramCotizacion);
                response = JsonConvert.SerializeObject(TipoProducto);
            }
            else
            {
                response = "No Valido";
            }
            return response;
        }

        [WebMethod, SoapHeader("Credencial")]
        public string getMotivoRecotizacion()
        {
            string response;

            if (VerificarPermisos(Credencial))
            {
                ControlFUP controlFup = new ControlFUP();
                List<Dominios> lstTipoRecotizacion = controlFup.obtenerTipoRecotizacionFUP();
                response = JsonConvert.SerializeObject(lstTipoRecotizacion);
            }
            else
            {
                response = "No Valido";
            }
            return response;
        }

        [WebMethod, SoapHeader("Credencial")]
        public string getVersionFup(string idFup, int IdRepresentante, int rol)
        {

            string response = string.Empty;
            int Consultar = -1;

            if (VerificarPermisos(Credencial))
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                ControlFUP controlFup = new ControlFUP();
                parametros.Add("@pFupID", idFup);
                parametros.Add("@pRepId", IdRepresentante);

                List<formato_unico> formatoUnico = ControlDatos.EjecutarStoreProcedureConParametros<formato_unico>("USP_fup_SEL_FUPporID", parametros);
                if (formatoUnico.Count > 0)
                {
                    Consultar = Convert.ToInt32(idFup);
                }
                List<VersionFup> lstVersion = controlFup.obtenerVersionesFUP(Consultar);
                response = JsonConvert.SerializeObject(lstVersion);
            }
            else
            {
                response = "No Valido";
            }
            return response;
        }

        [WebMethod, SoapHeader("Credencial")]
        public string getListaFup(string idObra, int IdRepresentante, int rol)
        {

            string response = string.Empty;

            if (VerificarPermisos(Credencial))
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                ControlFUP controlFup = new ControlFUP();
                parametros.Add("@pObraId", idObra);
                parametros.Add("@pRepId", IdRepresentante);

                List<Lfups> Lstfup = ControlDatos.EjecutarStoreProcedureConParametros<Lfups>("USP_fup_SEL_ListaFUPporObra", parametros);

                response = JsonConvert.SerializeObject(Lstfup);
            }
            else
            {
                response = "No Valido";
            }
            return response;
        }

        [WebMethod, SoapHeader("Credencial")]
        public string getFup(string idFup, string Version)
        {

            string response = string.Empty;

            if (VerificarPermisos(Credencial))
            {

                ControlFUP controlFup = new ControlFUP();
                ControlUbicacion controlUbicacion = new ControlUbicacion();
                ControlCliente control = new ControlCliente();
                ControlContacto controlContacto = new ControlContacto();
                Dictionary<string, object> parametros = new Dictionary<string, object>();

                #region ConsultarInformacionGeneral
                parametros.Clear();
                parametros.Add("@pFupID", Convert.ToInt32(idFup));
                parametros.Add("@pVersion", Version);
                List<fup_consultar> dataInfoGeneral = ControlDatos.EjecutarStoreProcedureConParametros<fup_consultar>("USP_fup_SEL_ent_cotizacion", parametros);
                fupGuardar DatosFup = new fupGuardar();

                DatosFup.IdFUP = Convert.ToInt32(idFup);
                DatosFup.Version = Version;
                DatosFup.Usuario = dataInfoGeneral.FirstOrDefault().Usuario;
                DatosFup.rol = 0;
                DatosFup.ID_Cliente = dataInfoGeneral.FirstOrDefault().ID_Cliente;
                DatosFup.ID_Moneda = dataInfoGeneral.FirstOrDefault().ID_Moneda;
                DatosFup.ID_Contacto = dataInfoGeneral.FirstOrDefault().ID_Contacto;
                DatosFup.ID_Obra = dataInfoGeneral.FirstOrDefault().ID_Obra;
                DatosFup.TipoCotizacion = dataInfoGeneral.FirstOrDefault().TipoCotizacion;
                //                DatosFup.TerminoNegociacion = dataInfoGeneral.FirstOrDefault().TerminoNegociacion;
                DatosFup.Producto = dataInfoGeneral.FirstOrDefault().Producto;
                DatosFup.MaxPisos = dataInfoGeneral.FirstOrDefault().MaxPisos;
                DatosFup.FundicionPisos = dataInfoGeneral.FirstOrDefault().FundicionPisos;
                DatosFup.NumeroEquipos = dataInfoGeneral.FirstOrDefault().NumeroEquipos;
                if (dataInfoGeneral.FirstOrDefault().AlturaLibre.ToString() == "23" || dataInfoGeneral.FirstOrDefault().AlturaLibre.ToString() == "-1")
                {
                    DatosFup.AlturaLibre = dataInfoGeneral.FirstOrDefault().AlturaLibreCual;
                }
                else
                {
                    DatosFup.AlturaLibre = dataInfoGeneral.FirstOrDefault().AlturaLibreDesc.Substring(0, 3);
                }
                DatosFup.AlineacionVertical = dataInfoGeneral.FirstOrDefault().AlineacionVertical;
                DatosFup.FormaConstructiva = dataInfoGeneral.FirstOrDefault().FormaConstructiva;
                DatosFup.TipoUnion = dataInfoGeneral.FirstOrDefault().TipoUnion;
                #endregion

                if (dataInfoGeneral.Count > 0)
                {

                    #region ConsultarTablasInformacionGeneral
                    parametros.Clear();
                    parametros.Add("@pFupID", Convert.ToInt32(idFup));
                    parametros.Add("@pVersion", Version);
                    List<fup_tablas2> dataTablas = ControlDatos.EjecutarStoreProcedureConParametros<fup_tablas2>("USP_fup_SEL_enc_ent_cot_tabla", parametros);
                    foreach (fup_tablas2 tabla in dataTablas)
                    {
                        if (tabla.tipo_tabla == 1) { DatosFup.EspesorMuro = tabla.valor; }
                        if (tabla.tipo_tabla == 2) { DatosFup.EspesorLosa = tabla.valor; }
                        if (tabla.tipo_tabla == 4) { DatosFup.EnrasePuerta = tabla.valor; }
                        if (tabla.tipo_tabla == 5) { DatosFup.EnraseVentana = tabla.valor; }
                    }
                    #endregion
                }
                response = JsonConvert.SerializeObject(DatosFup);
            }
            else
            {
                response = "No Valido";
            }
            return response;
        }

        [WebMethod, SoapHeader("Credencial")]
        public string CrearFUP(fupGuardar objFup)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string idVersion = "A", response = string.Empty;
            ControlObra control = new ControlObra();
            CapaControl.Entity.ObraInformacion obraInfo = control.obtenerDatosObra(objFup.ID_Obra);

            if (VerificarPermisos(Credencial))
            {

                if (objFup.Version == null)
                    idVersion = objFup.Version;

                parametros.Add("@pUsuario", objFup.Usuario);
                parametros.Add("@pFupAnterior", 0);
                parametros.Add("@pTipoNegociacion", 1);
                parametros.Add("@pTipoCotizacion", objFup.TipoCotizacion);
                parametros.Add("@pProducto", objFup.Producto);
                parametros.Add("@pTipoVaciado", 0);
                parametros.Add("@pSistemaSeguridad", 0);
                parametros.Add("@pNumeroEquipos", 1);
                parametros.Add("@pClaseCotizacion", 2);
                parametros.Add("@pEstrato", obraInfo.IdEstratoSocioEconomico);
                parametros.Add("@pTipoVivienda", obraInfo.ObraTipoVivienda);
                parametros.Add("@pTotalUnidades", obraInfo.TotalUnidades);
                parametros.Add("@pTotalUndForsa", obraInfo.TotalUnidades);
                parametros.Add("@pTerminoNegociacion", 1);
                parametros.Add("@pAlturaLibre", objFup.AlturaLibre);
                parametros.Add("@pAlineacionVertical", objFup.AlineacionVertical);
                parametros.Add("@pFormaConstructiva", objFup.FormaConstructiva);
                parametros.Add("@pCantMaxPisos", objFup.MaxPisos);
                parametros.Add("@pCantFundiciones", objFup.FundicionPisos);
                parametros.Add("@pTipoUnion", objFup.TipoUnion);
                parametros.Add("@pRapida", 1);
                parametros.Add("@pEnPrueba", Convert.ToBoolean(objFup.EnPrueba));
                
                int id = 0;

                if (objFup.IdFUP > 0)
                {
                    parametros.Add("@pFupID", objFup.IdFUP);
                    parametros.Add("@pVersion", idVersion);
                    parametros.Add("@rol", objFup.rol);
                    ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_ent_cotizacion", parametros);
                    id = objFup.IdFUP;
                }
                else
                {
                    parametros.Add("@pID_Cliente", objFup.ID_Cliente);
                    parametros.Add("@pID_Moneda", objFup.ID_Moneda);
                    parametros.Add("@pID_Contacto", objFup.ID_Contacto);
                    parametros.Add("@pID_Obra", objFup.ID_Obra);
                    idVersion = "A";
                    id = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_INS_ent_cotizacion", parametros);
                }

                Dictionary<string, object> parametros_tablas = new Dictionary<string, object>();
                parametros_tablas.Add("@pFupID", id);
                parametros_tablas.Add("@pVersion", idVersion);
                parametros_tablas.Add("@tipoTabla", 1);
                parametros_tablas.Add("@Consecutivo", 1);
                parametros_tablas.Add("@Valor", objFup.EspesorMuro);
                ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_enc_ent_cot_tabla", parametros_tablas);

                parametros_tablas.Clear();
                parametros_tablas.Add("@pFupID", id);
                parametros_tablas.Add("@pVersion", idVersion);
                parametros_tablas.Add("@tipoTabla", 2);
                parametros_tablas.Add("@Consecutivo", 1);
                parametros_tablas.Add("@Valor", objFup.EspesorLosa);
                ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_enc_ent_cot_tabla", parametros_tablas);

                parametros_tablas.Clear();
                parametros_tablas.Add("@pFupID", id);
                parametros_tablas.Add("@pVersion", idVersion);
                parametros_tablas.Add("@tipoTabla", 4);
                parametros_tablas.Add("@Consecutivo", 1);
                parametros_tablas.Add("@Valor", objFup.EnrasePuerta);
                ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_enc_ent_cot_tabla", parametros_tablas);

                parametros_tablas.Clear();
                parametros_tablas.Add("@pFupID", id);
                parametros_tablas.Add("@pVersion", idVersion);
                parametros_tablas.Add("@tipoTabla", 5);
                parametros_tablas.Add("@Consecutivo", 1);
                parametros_tablas.Add("@Valor", objFup.EnraseVentana);
                ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_enc_ent_cot_tabla", parametros_tablas);


                //--- Manejo de datos de tablas
                var respuesta = new
                {
                    IdFup = id,
                    Version = idVersion
                };

                response = JsonConvert.SerializeObject(respuesta);
            }
            else
            {
                response = "No Valido";
            }
            return response;
        }

        [WebMethod, SoapHeader("Credencial")]
        public string CrearFUP_Texto(string idFup, string Version, string objFup)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string idVersion = "A", response = string.Empty;
            string[] words = objFup.Split(',');
            var ClaseFup = new
            {
                IdFUP = idFup,
                Version = idVersion,
                Usuario = words[0],
                rol = words[1],
                ID_Cliente = words[2],
                ID_Moneda = words[3],
                ID_Contacto = words[4],
                ID_Obra = words[5],
                TipoCotizacion = words[6],
                TerminoNegociacion = words[7],
                Producto = words[8],
                MaxPisos = words[9],
                FundicionPisos = words[10],
                NumeroEquipos = words[11],
                AlturaLibre = words[12],
                AlineacionVertical = words[13],
                FormaConstructiva = words[14],
                EspesorMuro = words[15],
                EspesorLosa = words[16],
                EnrasePuerta = words[17],
                EnraseVentana = words[18],
                TipoUnion = words[19],
                escalera = words[20]
            };

            string Datosalida = JsonConvert.SerializeObject(ClaseFup);


            ControlObra control = new ControlObra();
            CapaControl.Entity.ObraInformacion obraInfo = control.obtenerDatosObra(Datosalida[7]);

            if (VerificarPermisos(Credencial))
            {

                if (!String.IsNullOrEmpty(Datosalida[1].ToString())) {
                    idVersion = Version;
                }


                parametros.Add("@pUsuario", Datosalida[2].ToString());
                parametros.Add("@pFupAnterior", 0);
                parametros.Add("@pTipoNegociacion", 1);
                parametros.Add("@pTipoCotizacion", Datosalida[8].ToString());
                parametros.Add("@pProducto", Datosalida[10].ToString());
                parametros.Add("@pTipoVaciado", 0);
                parametros.Add("@pSistemaSeguridad", 0);
                parametros.Add("@pNumeroEquipos", 1);
                parametros.Add("@pClaseCotizacion", 2);
                parametros.Add("@pEstrato", obraInfo.IdEstratoSocioEconomico);
                parametros.Add("@pTipoVivienda", obraInfo.ObraTipoVivienda);
                parametros.Add("@pTotalUnidades", obraInfo.TotalUnidades);
                parametros.Add("@pTotalUndForsa", obraInfo.TotalUnidades);
                parametros.Add("@pTerminoNegociacion", 1);
                parametros.Add("@pNumeroEquipos", 0);
                parametros.Add("@pAlturaLibre", Datosalida[14].ToString());
                parametros.Add("@pAlineacionVertical", Datosalida[15].ToString());
                parametros.Add("@pFormaConstructiva", Datosalida[16].ToString());
                parametros.Add("@pCantMaxPisos", Datosalida[11].ToString());
                parametros.Add("@pCantFundiciones", Datosalida[12].ToString());
                parametros.Add("@pTipoUnion", Datosalida[21].ToString());
                parametros.Add("@pRapida", 1);

                int id = 0;

                if (Datosalida[0] > 0)
                {
                    parametros.Add("@pFupID", Datosalida[0]);
                    parametros.Add("@pVersion", Datosalida[1].ToString());
                    parametros.Add("@rol", Datosalida[3].ToString());
                    ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_ent_cotizacion", parametros);
                    id = Datosalida[0];
                }
                else
                {
                    parametros.Add("@pID_Cliente", Datosalida[4].ToString());
                    parametros.Add("@pID_Moneda", Datosalida[5].ToString());
                    parametros.Add("@pID_Contacto", Datosalida[6].ToString());
                    parametros.Add("@pID_Obra", Datosalida[7].ToString());
                    id = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_INS_ent_cotizacion", parametros);
                }

                Dictionary<string, object> parametros_tablas = new Dictionary<string, object>();
                parametros_tablas.Add("@pFupID", id);
                parametros_tablas.Add("@pVersion", idVersion);
                parametros_tablas.Add("@tipoTabla", 1);
                parametros_tablas.Add("@Consecutivo", 1);
                parametros_tablas.Add("@Valor", Datosalida[17].ToString());
                ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_enc_ent_cot_tabla", parametros_tablas);

                parametros_tablas.Clear();
                parametros_tablas.Add("@pFupID", id);
                parametros_tablas.Add("@pVersion", idVersion);
                parametros_tablas.Add("@tipoTabla", 2);
                parametros_tablas.Add("@Consecutivo", 1);
                parametros_tablas.Add("@Valor", Datosalida[18].ToString());
                ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_enc_ent_cot_tabla", parametros_tablas);

                parametros_tablas.Clear();
                parametros_tablas.Add("@pFupID", id);
                parametros_tablas.Add("@pVersion", idVersion);
                parametros_tablas.Add("@tipoTabla", 4);
                parametros_tablas.Add("@Consecutivo", 1);
                parametros_tablas.Add("@Valor", Datosalida[19].ToString());
                ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_enc_ent_cot_tabla", parametros_tablas);

                parametros_tablas.Clear();
                parametros_tablas.Add("@pFupID", id);
                parametros_tablas.Add("@pVersion", idVersion);
                parametros_tablas.Add("@tipoTabla", 5);
                parametros_tablas.Add("@Consecutivo", 1);
                parametros_tablas.Add("@Valor", Datosalida[20].ToString());
                ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_enc_ent_cot_tabla", parametros_tablas);


                //--- Manejo de datos de tablas
                var respuesta = new
                {
                    IdFup = id,
                    Version = idVersion
                };

                response = JsonConvert.SerializeObject(respuesta);
            }
            else
            {
                response = "No Valido";
            }
            return response;
        }


        [WebMethod, SoapHeader("Credencial")]
        public string guardarRecotizacionFUP(int idFup, string Version, int idTipoRecotizacion, string observacion, string nombreUsuario)
        {

            string response = string.Empty;
            if (VerificarPermisos(Credencial))
            {
                ControlFUP controlFup = new ControlFUP();
                Dictionary<string, object> parametros = new Dictionary<string, object>();

                parametros.Add("@pFupID", Convert.ToInt32(idFup));
                parametros.Add("@pVersion", Version);
                parametros.Add("@pUsuario", nombreUsuario);
                parametros.Add("@pDescripcion", observacion);
                parametros.Add("@pTipoRecotizacion", Convert.ToInt32(idTipoRecotizacion));
                ControlDatos.GuardarStoreProcedureConParametros("USP_fup_RecotizacionN", parametros);

                List<VersionFup> lstVersion = controlFup.obtenerVersionesFUP(idFup);

                var respuesta = new
                {
                    Idfup = idFup,
                    Version = lstVersion.FirstOrDefault().eect_vercot_id
                };

                response = JsonConvert.SerializeObject(respuesta);
            }
            else
            {
                response = "No Valido";
            }
            return response;
        }

        [WebMethod, SoapHeader("Credencial")]
        public string GuardarProyectoObj(int idFup, string Version, string nombreUsuario, guardarToscana datos)
        {
            string response = string.Empty;
            if (VerificarPermisos(Credencial))
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                int sisseg = 0;

                #region DatosTablasEntcot
                // Borrar datos de tablas si existen
                parametros.Clear();
                parametros.Add("@pFupID", idFup);
                parametros.Add("@pVersion", Version);
                ControlDatos.GuardarStoreProcedureConParametros("USP_fup_DEL_enc_ent_cot_tabla", parametros);
                ControlDatos.GuardarStoreProcedureConParametros("USP_fup_DEL_Listado_Piezas", parametros);

                // Crear cada valor de tabla
                // Espesor muro
                parametros.Clear();
                parametros.Add("@pFupID", idFup);
                parametros.Add("@pVersion", Version);
                parametros.Add("@tipoTabla", 1);
                parametros.Add("@Consecutivo", 1);
                parametros.Add("@Valor", datos.proyecto.espesorMuro);
                ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_enc_ent_cot_tabla", parametros);

                // Espesor losa
                parametros.Clear();
                parametros.Add("@pFupID", idFup);
                parametros.Add("@pVersion", Version);
                parametros.Add("@tipoTabla", 2);
                parametros.Add("@Consecutivo", 1);
                parametros.Add("@Valor", datos.proyecto.espesorLosa);
                ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_enc_ent_cot_tabla", parametros);

                // Enrase puerta
                parametros.Clear();
                parametros.Add("@pFupID", idFup);
                parametros.Add("@pVersion", Version);
                parametros.Add("@tipoTabla", 4);
                parametros.Add("@Consecutivo", 1);
                parametros.Add("@Valor", datos.proyecto.enrasepuerta);
                ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_enc_ent_cot_tabla", parametros);

                // Enrase ventana
                parametros.Clear();
                parametros.Add("@pFupID", idFup);
                parametros.Add("@pVersion", Version);
                parametros.Add("@tipoTabla", 5);
                parametros.Add("@Consecutivo", 1);
                parametros.Add("@Valor", datos.proyecto.enraseVentana);
                ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_enc_ent_cot_tabla", parametros);
                #endregion

                #region AlcanceFUP
                int consecutivopadre = 0;
                #region EquipoBase
                if (datos.equipoBase.Count() > 0)
                {
                    parametros.Clear();
                    parametros.Add("@pFupID", idFup);
                    parametros.Add("@pVersion", Version);
                    parametros.Add("@TipoEquipo", 1);
                    parametros.Add("@Consecutivo", 1);
                    parametros.Add("@Cantidad", 1);
                    parametros.Add("@Descripcion", "Equipo Base " + datos.proyecto.Proyecto ?? "");
                    ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_Alcance_Equipos", parametros);
                    consecutivopadre = 1;
                }
                //Guardar Detalle Piezas
                if (datos.equipoBase.Count() > 0)
                {
                    datos.equipoBase.ForEach(delegate (Pieza ptra)
                    {
                        parametros.Clear();
                        parametros.Add("@pFupID", idFup);
                        parametros.Add("@pVersion", Version);
                        parametros.Add("@pOrigen", 1);
                        parametros.Add("@pId", ptra.id);
                        parametros.Add("@pNombrepieza", ptra.Nombrepieza);
                        parametros.Add("@pDescripcion", ptra.descri);
                        parametros.Add("@pFamilia", ptra.familia);
                        parametros.Add("@pCantidad", ptra.cant);
                        parametros.Add("@pAncho", ptra.ancho);
                        parametros.Add("@pAlto", ptra.alto);
                        parametros.Add("@pArea", ptra.areaItem);
                        ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_ListadoPiezas", parametros);
                    }
                    );

                }
                #endregion

                #region Adaptaciones
                if (datos.adaptaciones.Count() > 0)
                {
                    parametros.Clear();
                    parametros.Add("@pFupID", idFup);
                    parametros.Add("@pVersion", Version);
                    parametros.Add("@ConsecutivoPadre", consecutivopadre);
                    parametros.Add("@Descripcion", "Adaptaciones " + datos.proyecto.Proyecto ?? "");
                    ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_Alcance_Adaptacion", parametros);
                }

                //Guardar Detalle Piezas adaptaciones
                if (datos.adaptaciones.Count() > 0)
                {
                    datos.adaptaciones.ForEach(delegate (Pieza ptra)
                    {
                        parametros.Clear();
                        parametros.Add("@pFupID", idFup);
                        parametros.Add("@pVersion", Version);
                        parametros.Add("@pOrigen", 2);
                        parametros.Add("@pId", ptra.id);
                        parametros.Add("@pNombrepieza", ptra.Nombrepieza);
                        parametros.Add("@pDescripcion", ptra.descri);
                        parametros.Add("@pFamilia", ptra.familia);
                        parametros.Add("@pCantidad", ptra.cant);
                        parametros.Add("@pAncho", ptra.ancho);
                        parametros.Add("@pAlto", ptra.alto);
                        parametros.Add("@pArea", ptra.cant);
                        ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_ListadoPiezas", parametros);
                    }
                    );

                }
                #endregion

                #region Escalera
                if (Convert.ToInt32(datos.escalera.tipo) > 0)
                {
                    parametros.Clear();
                    parametros.Add("@pFupID", idFup);
                    parametros.Add("@pVersion", Version);
                    parametros.Add("@pItemparte_id", 3);
                    parametros.Add("@pItemSiNo", true);
                    parametros.Add("@pItemTextoLista", datos.escalera.tipo ?? "");
                    parametros.Add("@pAdicionalSiNo", false);
                    parametros.Add("@pAdicionalCantidad", 0);
                    parametros.Add("@Descripcion", datos.escalera.m2.ToString() ?? "");
                    ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_Alcance_Partes", parametros);
                }
                #endregion

                #endregion

                #region ActualizarEntradaCot
                if (datos.seguridad == 1) { sisseg = 1; }
                if (datos.trepante == 1) { sisseg = 2; }

                // Actualizar Valores de FUP
                parametros.Clear();
                parametros.Add("@pFupID", idFup);
                parametros.Add("@pVersion", Version);
                parametros.Add("@pUsuario", nombreUsuario);
                parametros.Add("@pSistemaSeguridad", sisseg);
                parametros.Add("@pAlturaLibre", datos.proyecto.alturaLibre);
                parametros.Add("@@pEstado", 5);  // Estado Comtizado
                ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_ent_cotizacionTS", parametros);
                #endregion

                #region ActualizarSalidaCot

                // Crear Salida Cotizacion 
                parametros.Clear();
                parametros.Add("@pFupID", idFup);
                parametros.Add("@pVersion", Version);
                parametros.Add("@pUsuario", nombreUsuario);
                parametros.Add("@pEquipo_m2", datos.alcance.equipo_base.m2 ?? 0);
                parametros.Add("@pEuipo_valor", datos.alcance.equipo_base.vr_total ?? 0);
                parametros.Add("@pAdicional_m2", datos.alcance.adicionales.m2 ?? 0);
                parametros.Add("@pAdicional_valor", datos.alcance.adicionales.vr_total ?? 0);
                parametros.Add("@pDetalleArq_m2", datos.alcance.escalera.m2 ?? 0);
                parametros.Add("@pDetalleArq_valor", datos.alcance.escalera.vr_total ?? 0);
                parametros.Add("@pSisSeguridad_per", datos.alcance.equipo_seguridad.m2 + datos.alcance.equipo_trepante.m2 ?? 0);
                parametros.Add("@pSisSeguridad_valor", datos.alcance.equipo_seguridad.vr_total + datos.alcance.equipo_trepante.vr_total ?? 0);
                parametros.Add("@pAcc_basico_valor", datos.alcance.accesorios_basicos.vr_total);
                parametros.Add("@pAcc_complementaio_valor", datos.alcance.accesorios_complementarios.vr_total);
                parametros.Add("@pAcc_opcional_valor", 0);
                parametros.Add("@pAcc_adicional_valor", 0);
                parametros.Add("@pOtros_productos_valor", 0);
                parametros.Add("@pCont20", 0);
                parametros.Add("@pCont40", 0);
                parametros.Add("@pTipoSalida", 1);

                ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_salida_cotizacionN", parametros);

                response = CorreoFUP(idFup, Version, 5, nombreUsuario);

                #endregion


            }
            else
            {
                response = "No Valido";
            }
            return response;
        }

        [WebMethod, SoapHeader("Credencial")]
        public string GuardarProyectoObj2(int idFup, string Version, string nombreUsuario, ProyectoToscLis datos)
        {
            string response = string.Empty;
            if (VerificarPermisos(Credencial))
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                int sisseg = 0;
                int idCt = 0;

                #region DatosTablasEntcot
                // Borrar datos de tablas si existen
                parametros.Clear();
                parametros.Add("@pFupID", idFup);
                parametros.Add("@pVersion", Version);
                ControlDatos.GuardarStoreProcedureConParametros("USP_fup_DEL_Listado_Piezas", parametros);
                #endregion

                // Crear cada valor de tabla
                #region AlcanceFUP

                //Guardar Detalle Piezas
                if (datos.equipoBase.Count() > 0)
                {
                    datos.equipoBase.ForEach(delegate (Pieza ptra)
                    {
                        parametros.Clear();
                        parametros.Add("@pFupID", idFup);
                        parametros.Add("@pVersion", Version);
                        parametros.Add("@pOrigen", 1);
                        parametros.Add("@pId", ptra.id);
                        parametros.Add("@pNombrepieza", ptra.Nombrepieza);
                        parametros.Add("@pDescripcion", ptra.descri);
                        parametros.Add("@pFamilia", ptra.familia);
                        parametros.Add("@pCantidad", ptra.cant);
                        parametros.Add("@pAncho", ptra.ancho);
                        parametros.Add("@pAlto", ptra.alto);
                        parametros.Add("@pArea", ptra.areaItem);
                        ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_ListadoPiezas", parametros);
                    }
                    );

                    #region CrearOrdenCT
                    // Borrar datos de tablas si existen
                    parametros.Clear();
                    parametros.Add("@pFupID", idFup);
                    parametros.Add("@pversion", Version);
                    parametros.Add("@pUsuario", Version);
                    idCt = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_INS_Orden_Cotizacion", parametros);
                    #endregion
                }
                #endregion


                #region ActualizarEntradaCot

                // Actualizar Valores de FUP
                parametros.Clear();
                parametros.Add("@pFupID", idFup);
                parametros.Add("@pVersion", Version);
                parametros.Add("@pUsuario", nombreUsuario);
                parametros.Add("@pSistemaSeguridad", sisseg);
                parametros.Add("@pAlturaLibre", "0");
                parametros.Add("@pEstado", 4);  // Estado Comtizado
                ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_ent_cotizacionTS", parametros);
                #endregion

                #region ActualizarSalidaCot


                //parametros.Clear();
                //parametros.Add("@pFupID", idFup);
                //parametros.Add("@pVersion", Version);
                //parametros.Add("@pUsuario", nombreUsuario);
                //parametros.Add("@pEquipo_m2", datos.alcance.total_alcance_oferta.m2 ?? 0);
                //parametros.Add("@pEuipo_valor", datos.alcance.total_alcance_oferta.vr_total ?? 0);
                //parametros.Add("@pAdicional_m2", 0);
                //parametros.Add("@pAdicional_valor", 0);
                //parametros.Add("@pDetalleArq_m2",  0);
                //parametros.Add("@pDetalleArq_valor",  0);
                //parametros.Add("@pSisSeguridad_per",  0);
                //parametros.Add("@pSisSeguridad_valor",  0);
                //parametros.Add("@pAcc_basico_valor", 0);
                //parametros.Add("@pAcc_complementaio_valor", 0);
                //parametros.Add("@pAcc_opcional_valor", 0);
                //parametros.Add("@pAcc_adicional_valor", 0);
                //parametros.Add("@pOtros_productos_valor", 0);
                //parametros.Add("@pCont20", 0);
                //parametros.Add("@pCont40", 0);
                //parametros.Add("@pTipoSalida", 1);

//                ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_salida_cotizacionN", parametros);

                response = CorreoFUP(idFup, Version, 85, nombreUsuario);

                #endregion


            }
            else
            {
                response = "No Valido";
            }
            return response;
        }
        [WebMethod, SoapHeader("Credencial")]
        public string GuardarListaPiezas(int idFup, string Version, string nombreUsuario, guardarToscana datos)
        {
            string response = string.Empty;
            if (VerificarPermisos(Credencial))
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>();

                #region DatosTablasEntcot
                // Borrar datos de tablas si existen
                parametros.Clear();
                parametros.Add("@pFupID", idFup);
                parametros.Add("@pVersion", Version);

                ControlDatos.GuardarStoreProcedureConParametros("USP_fup_DEL_Listado_PiezasUV", parametros);

                //Guardar Detalle Piezas
                if (datos.equipoBase.Count() > 0)
                {
                    datos.equipoBase.ForEach(delegate (Pieza ptra)
                    {
                        parametros.Clear();
                        parametros.Add("@pFupID", idFup);
                        parametros.Add("@pVersion", Version);
                        parametros.Add("@pOrigen", 1);
                        parametros.Add("@pId", ptra.id);
                        parametros.Add("@pNombrepieza", ptra.Nombrepieza);
                        parametros.Add("@pDescripcion", ptra.descri);
                        parametros.Add("@pFamilia", ptra.familia);
                        parametros.Add("@pCantidad", ptra.cant);
                        parametros.Add("@pAncho", ptra.ancho);
                        parametros.Add("@pAlto", ptra.alto);
                        parametros.Add("@pArea", ptra.cant);
                        ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_ListadoPiezasUV", parametros);
                    }
                    );

                }
                #endregion

               
                
            }
            else
            {
                response = "No Valido";
            }
            return response;
        }


        [WebMethod, SoapHeader("Credencial")]
        public string GuardarProyecto(int idFup, string Version, string nombreUsuario, string datos)
        {
            string response = string.Empty;
            if (VerificarPermisos(Credencial))
            {
                try
                {
                    if (datos.IndexOf("tipoCotizacion") > 0)
                    {
                       var ms = new MemoryStream(Encoding.UTF8.GetBytes(datos));
 
                        DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(ProyectoToscLis), null, int.MaxValue, true, null, false);
                        ProyectoToscLis dato = (ProyectoToscLis)deserializer.ReadObject(ms);
                        response = GuardarProyectoObj2(idFup, Version, nombreUsuario, dato);
                    }
                    else
                    {
                        var ms = new MemoryStream(Encoding.UTF8.GetBytes(datos));
                        DataContractJsonSerializer deserializer = new DataContractJsonSerializer(typeof(guardarToscana));
                        guardarToscana dato = (guardarToscana)deserializer.ReadObject(ms);
                        response = GuardarProyectoObj(idFup, Version, nombreUsuario, dato);
                    }

                }
                catch (Exception ex)
                {
                    response = "ERROR: " + ex.Message;
                }
            }
            else
            {
                response = "No Valido";
            }
            return response;
        }

        [WebMethod, SoapHeader("Credencial")]
        public string GuardarFup(int idFup, string Version, string nombreUsuario, List<ItemEquipo> listaItem, fup_salida_cotizacion fupsal)
        {
            string response = string.Empty;
            if (VerificarPermisos(Credencial))
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                ControlFUP controlFup = new ControlFUP();
                int id = 0;
                int paso = 0;

                // Verificar Estado.

                foreach (ItemEquipo item in listaItem)
                {
                    if (paso == 0)
                    {
                        parametros.Clear();
                        parametros.Add("@pFupID", item.pFupID);
                        parametros.Add("@pVersion", item.pVersion);
                        ControlDatos.GuardarStoreProcedureConParametros("USP_fup_DEL_Alcance_Equipos", parametros);
                        ControlDatos.GuardarStoreProcedureConParametros("USP_fup_DEL_Alcance_Adaptacion", parametros);
                    }
                    paso += 1;

                    parametros.Clear();
                    if (item.TipoEquipo == 0 && item.Cantidad == 0)
                    {
                        parametros.Add("@pFupID", item.pFupID);
                        parametros.Add("@pVersion", item.pVersion);
                        parametros.Add("@ConsecutivoPadre", item.Consecutivo ?? 0);
                        parametros.Add("@Descripcion", item.Descripcion ?? "");
                        id = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_Alcance_Adaptacion", parametros);
                    }
                    else if (item.Consecutivo > 0 && item.Cantidad != 0)
                    {
                        parametros.Add("@pFupID", item.pFupID);
                        parametros.Add("@pVersion", item.pVersion);
                        parametros.Add("@TipoEquipo", item.TipoEquipo);
                        parametros.Add("@Consecutivo", item.Consecutivo ?? 0);
                        parametros.Add("@Cantidad", item.Cantidad ?? 0);
                        parametros.Add("@Descripcion", item.Descripcion ?? "");
                        id = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_Alcance_Equipos", parametros);
                    }
                }
                if (!fupsal.Equals(null))
                {
                    parametros.Clear();
                    parametros.Add("@pFupID", Convert.ToInt32(fupsal.fupid));
                    parametros.Add("@pVersion", fupsal.version);
                    parametros.Add("@pUsuario", nombreUsuario);
                    parametros.Add("@pEquipo_m2", fupsal.m2_equipo);
                    parametros.Add("@pEuipo_valor", fupsal.vlr_equipo);
                    parametros.Add("@pAdicional_m2", fupsal.m2_adicionales);
                    parametros.Add("@pAdicional_valor", fupsal.vlr_adicionales);
                    parametros.Add("@pDetalleArq_m2", fupsal.m2_Detalle_arquitectonico);
                    parametros.Add("@pDetalleArq_valor", fupsal.vlr_Detalle_arquitectonico);
                    parametros.Add("@pSisSeguridad_per", fupsal.m2_sis_seguridad);
                    parametros.Add("@pSisSeguridad_valor", fupsal.vlr_sis_seguridad);
                    parametros.Add("@pAcc_basico_valor", fupsal.vlr_accesorios_basico);
                    parametros.Add("@pAcc_complementaio_valor", fupsal.vlr_accesorios_complementario);
                    parametros.Add("@pAcc_opcional_valor", fupsal.vlr_accesorios_opcionales);
                    parametros.Add("@pAcc_adicional_valor", fupsal.vlr_accesorios_adicionales);
                    parametros.Add("@pOtros_productos_valor", fupsal.vlr_otros_productos);
                    parametros.Add("@pCont20", fupsal.vlr_Contenedor20 ?? 0);
                    parametros.Add("@pCont40", fupsal.vlr_Contenedor40 ?? 0);
                    parametros.Add("@pTipoSalida", fupsal.tipoSalida);

                    int ok = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_salida_cotizacionN", parametros);
                }

                List<VersionFup> lstVersion = controlFup.obtenerVersionesFUP(idFup);

                var respuesta = new
                {
                    Idfup = idFup,
                    Version = lstVersion.FirstOrDefault().eect_vercot_id
                };

                response = JsonConvert.SerializeObject(respuesta);
            }
            else
            {
                response = "No Valido";
            }
            return response;
        }

        //Para revisar desde acá //////////////////////////////////////////////////
        [WebMethod, SoapHeader("Credencial")]
        public string GuardarEquiposAdaptacion(List<ItemEquipo> listaItem)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            int id;
            int paso = 0;
            string response = string.Empty;

            foreach (ItemEquipo item in listaItem)
            {
                if (paso == 0)
                {
                    parametros.Clear();
                    parametros.Add("@pFupID", item.pFupID);
                    parametros.Add("@pVersion", item.pVersion);
                    ControlDatos.GuardarStoreProcedureConParametros("USP_fup_DEL_Alcance_Equipos", parametros);
                    ControlDatos.GuardarStoreProcedureConParametros("USP_fup_DEL_Alcance_Adaptacion", parametros);
                }
                paso += 1;

                if (!item.pFupID.Equals(null))
                {
                    parametros.Clear();
                    if (item.TipoEquipo == 0 && item.Cantidad == 0)
                    {
                        parametros.Add("@pFupID", item.pFupID);
                        parametros.Add("@pVersion", item.pVersion);
                        parametros.Add("@ConsecutivoPadre", item.Consecutivo ?? 0);
                        parametros.Add("@Descripcion", item.Descripcion ?? "");
                        id = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_Alcance_Adaptacion", parametros);
                    }
                    else if (item.Consecutivo > 0 && item.Cantidad != 0)
                    {
                        parametros.Add("@pFupID", item.pFupID);
                        parametros.Add("@pVersion", item.pVersion);
                        parametros.Add("@TipoEquipo", item.TipoEquipo);
                        parametros.Add("@Consecutivo", item.Consecutivo ?? 0);
                        parametros.Add("@Cantidad", item.Cantidad ?? 0);
                        parametros.Add("@Descripcion", item.Descripcion ?? "");
                        id = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_Alcance_Equipos", parametros);
                    }

                    var respuesta = new
                    {
                        Idfup = item.pFupID,
                        Version = item.pVersion
                    };

                    response = JsonConvert.SerializeObject(respuesta);
                }
                else
                {
                    response = "No Valido";
                }
                return response;
            }
            return response;
        }

        [WebMethod, SoapHeader("Credencial")]
        public string GuardarEquipos(ItemEquipo item)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            parametros.Add("@pFupID", item.pFupID);
            parametros.Add("@pVersion", item.pVersion);
            parametros.Add("@TipoEquipo", item.TipoEquipo);
            parametros.Add("@Consecutivo", item.Consecutivo ?? 0);
            parametros.Add("@Cantidad", item.Cantidad ?? 0);
            parametros.Add("@Descripcion", item.Descripcion ?? "");
            int id = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_Alcance_Equipos", parametros);

            string response = JsonConvert.SerializeObject(id.ToString());
            return response;
        }

        [WebMethod, SoapHeader("Credencial")]
        public string GuardarAdaptacion(ItemEquipo item)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            parametros.Add("@pFupID", item.pFupID);
            parametros.Add("@pVersion", item.pVersion);
            parametros.Add("@ConsecutivoPadre", item.Consecutivo ?? 0);
            parametros.Add("@Descripcion", item.Descripcion ?? "");

            int id = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_Alcance_Adaptacion", parametros);

            string response = JsonConvert.SerializeObject(id.ToString());
            return response;
        }

        [WebMethod, SoapHeader("Credencial")]
        public static string guardarSalidaCotizacion(fup_salida_cotizacion_WS fupsal, string nombreUsuario)
        {
            ControlFUP controlFup = new ControlFUP();
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            string NombreUsu = nombreUsuario;


            parametros.Add("@pFupID", Convert.ToInt32(fupsal.fupid));
            parametros.Add("@pVersion", fupsal.version);
            parametros.Add("@pUsuario", NombreUsu);
            parametros.Add("@pEquipo_m2", fupsal.m2_equipo);
            parametros.Add("@pEuipo_valor", fupsal.vlr_equipo);
            parametros.Add("@pAdicional_m2", fupsal.m2_adicionales);
            parametros.Add("@pAdicional_valor", fupsal.vlr_adicionales);
            parametros.Add("@pDetalleArq_m2", fupsal.m2_Detalle_arquitectonico);
            parametros.Add("@pDetalleArq_valor", fupsal.vlr_Detalle_arquitectonico);
            parametros.Add("@pSisSeguridad_per", fupsal.m2_sis_seguridad);
            parametros.Add("@pSisSeguridad_valor", fupsal.vlr_sis_seguridad);
            parametros.Add("@pAcc_basico_valor", fupsal.vlr_accesorios_basico);
            parametros.Add("@pAcc_complementaio_valor", fupsal.vlr_accesorios_complementario);
            parametros.Add("@pAcc_opcional_valor", fupsal.vlr_accesorios_opcionales);
            parametros.Add("@pAcc_adicional_valor", fupsal.vlr_accesorios_adicionales);
            parametros.Add("@pOtros_productos_valor", fupsal.vlr_otros_productos);
            parametros.Add("@pCont20", fupsal.vlr_Contenedor20 ?? 0);
            parametros.Add("@pCont40", fupsal.vlr_Contenedor40 ?? 0);
            parametros.Add("@pTipoSalida", fupsal.tipoSalida);

            int ok = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_salida_cotizacionN", parametros);

            string response = string.Empty;
            int evento = 5;

            if (fupsal.tipoSalida == 2)
                evento = 48;

            // Notificar Salida Cotizacion
            if (ok != -1)
                response = CorreoFUP(Convert.ToInt32(fupsal.fupid), fupsal.version, evento, NombreUsu);
            else
                response = "KO";
            return response;

        }
        [WebMethod, SoapHeader("Credencial")]
        public string pruebaCorreo(int idFup, string Version, string nombreUsuario ) {
            string response = string.Empty;
            int evento = 5;

            response = CorreoFUP(idFup, Version, evento, nombreUsuario);

            return response;

        }

    public static string CorreoFUP(int fup, string version, int pEvento, string nombreUsuario)
        {
            ControlFUP controlFup = new ControlFUP();
            string sfId = "0";
            int Evento = (int)pEvento;
            string Nombre = nombreUsuario;
            int parte = 0;
            string mensaje = "OK";

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@usu", nombreUsuario);

            string sql = @"select par_correo_sistema correo, '' NombreCorreo from Parametros";
            List<LCorreo> correoSist = ControlDatos.EjecutarConsulta<LCorreo>(sql, param);


            string sql2 = @"SELECT rc.rc_email correo, rc.rc_descripcion NombreCorreo  FROM usuario u inner join representantes_comerciales rc on rc.rc_usu_siif_id = u.usu_siif_id WHERE usu_login = @usu OR rc.rc_usu_siif_id = @usu";
            List<LCorreo> correoUsu = ControlDatos.EjecutarConsulta<LCorreo>(sql, param);

            string correoSistema = Convert.ToString(correoSist.FirstOrDefault().Correo);
            string correoUsuario = Convert.ToString(correoUsu.FirstOrDefault().Correo);
            string UsuarioAsunto = Convert.ToString(correoUsu.FirstOrDefault().NombreCorreo);

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pFupID ", fup);
            parametros.Add("@pVersion", version);
            parametros.Add("@pEvento", pEvento);
            parametros.Add("@pUsuario", UsuarioAsunto);
            parametros.Add("@pRemitente", correoUsuario);
            parametros.Add("@pParte", parte);

            List<NotificaFup> data = ControlDatos.EjecutarStoreProcedureConParametros<NotificaFup>("USP_fup_notificacionesN", parametros);

            if (data.Count() > 0)
            {
                //VALORES DEL ENCABEZADO 
                string AsuntoMail = Convert.ToString(data.FirstOrDefault().AsuntoMail) + " - Aproximación  ";
                string DestinatariosMail = Convert.ToString(data.FirstOrDefault().Lista);
                string MensajeMail = Convert.ToString(data.FirstOrDefault().Msg);
                bool llevaAnexo = Convert.ToBoolean(data.FirstOrDefault().Anexo);
                string EnlaceAnexo = Convert.ToString(data.FirstOrDefault().LinkAnexo);
                string tipoAdjunto = "";
                string enlaceCarta = "";
                string nombreCarta = "";

                Byte[] correo = new Byte[0];
                WebClient clienteWeb = new WebClient();
                clienteWeb.Dispose();
                clienteWeb.Credentials = new System.Net.NetworkCredential("forsa", "F0rUs3s4*", "FORSA");
                // Adjunto
                //DEFINIMOS LA CLASE DE MAILMESSAGE
                MailMessage mail = new MailMessage();
                //INDICAMOS EL EMAIL DE ORIGEN
                mail.From = new MailAddress(correoSistema);


                //AÑADIMOS LA DIRRECCIÓN DE CORREO DESTINATARIO
                mail.To.Add(DestinatariosMail);
                //mail.To.Add("ivanvidal@forsa.net.co");


                //INCLUIMOS EL ASUNTO DEL MENSAJE
                mail.Subject = AsuntoMail;
                //AÑADIMOS EL CUERPO DEL MENSAJE
                mail.Body = MensajeMail;
                //INDICAMOS EL TIPO DE CODIFICACIÓN DEL MENSAJE
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                //DEFINIMOS LA PRIORIDAD DEL MENSAJE
                mail.Priority = System.Net.Mail.MailPriority.Normal;
                //INDICAMOS SI EL CUERPO DEL MENSAJE ES HTML O NO
                mail.IsBodyHtml = true;
                //ADJUNTAMOS EL ARCHIVO
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                if (llevaAnexo == true)
                {
                    string enlace = "";

                    if (Evento == 13)
                    {
                        tipoAdjunto = "FUP";
                    }
                    else
                    {
                        if ((Evento == 2) || (Evento == 4) || (Evento == 5) || (Evento == 7))
                        {
                            tipoAdjunto = "FUP";
                            enlace = @"http://10.75.131.2:81/ReportServer?" + EnlaceAnexo.Trim() + "&rs:format=PDF&rs:command=render&rs:ClearSession=true&pFupID=" + fup.ToString() + "" +
                                    "&pVersion=" + version;
                        }
                        else
                        {
                            if (Evento == 9 || Evento == 23 || Evento == 24 || Evento == 25)
                            {
                                tipoAdjunto = "SF";
                                parte = Convert.ToInt32(HttpContext.Current.Session["Parte"]);
                                sfId = HttpContext.Current.Session["SfId"].ToString();

                                enlace = @"http://10.75.131.2:81/ReportServer?" + EnlaceAnexo.Trim() + "&rs:format=PDF&rs:command=render&rs:ClearSession=true&numfup=" + fup.ToString() + "" +
                                    "&version=" + version + "&parte=" + parte.ToString() + "&sf_id=" + sfId;
                            }
                            else
                            {
                                if (Evento == 16 || Evento == 15)
                                {
                                    tipoAdjunto = "SF";
                                    parte = Convert.ToInt32(HttpContext.Current.Session["Parte"]);
                                    sfId = HttpContext.Current.Session["SfId"].ToString();

                                    enlace = @"http://10.75.131.2:81/ReportServer?" + EnlaceAnexo.Trim() + "&rs:format=PDF&rs:command=render&rs:ClearSession=true&numfup=" + fup + "" +
                                        "&version=" + version + "&parte=" + parte.ToString() + "&sf_id=" + sfId;
                                }
                            }
                        }
                    }
                    correo = clienteWeb.DownloadData(enlace);
                    ms = new System.IO.MemoryStream(correo);
                    mail.Attachments.Add(new Attachment(ms, tipoAdjunto + " " + fup.ToString() + version + ".pdf"));

                    // adjunto el archivo de la carta directamente desde la carpeta de planos
                    if (pEvento == 5)
                    {
                        controlFup.actualizarSalidaCotizacion(fup, version, Nombre);
                    }
                }
                //DEFINIMOS NUESTRO SERVIDOR SMTP
                //DECLARAMOS LA CLASE SMTPCLIENT

                SmtpClient smtp = new SmtpClient();
                //DEFINIMOS NUESTRO SERVIDOR SMTP
                smtp.Host = "smtp.office365.com";
                smtp.Port = 25;
                smtp.EnableSsl = true;
                
                //smtp.Timeout = 400;
                //INDICAMOS LA AUTENTICACION DE NUESTRO SERVIDOR SMTP
                smtp.Credentials = new System.Net.NetworkCredential("informes@forsa.net.co", "Those7953");



                //ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain,
                //    SslPolicyErrors sslPolicyErrors)
                //{
                //    return true;
                //};
                System.Net.ServicePointManager.SecurityProtocol =  SecurityProtocolType.Tls12;
                try
                {
                    // smtp.SendAsync(mail, mail.To);
                    smtp.Send(mail);
                }
                catch (Exception ex)
                {
                    mensaje = "ERROR: " + ex.Message +" - " + ex.InnerException + " ** " + System.Environment.NewLine;
                }
                ms.Close();
            }
            return mensaje;
        }

        [WebMethod, SoapHeader("Credencial")]
        public string getListaPrecios(int fup, string version)
        {

            string response = string.Empty;

            if (VerificarPermisos(Credencial))
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                ControlFUP controlFup = new ControlFUP();
                parametros.Add("@pFupId", fup);

                List<ListaPrecio> ListaPrecios = ControlDatos.EjecutarStoreProcedureConParametros<ListaPrecio>("USP_fup_SEL_PrecioItem_Rap", parametros);

                response = JsonConvert.SerializeObject(ListaPrecios);
            }
            else
            {
                response = "No Valido";
            }
            return response;
        }

        [WebMethod, SoapHeader("Credencial")]
        public string getFups(int IdRepresentante, int rol, int idPais =0, int idCiudad = 0, int idCliente = 0, int idObra = 0, int idContacto = 0, int idFup = 0)
        {

            string response = string.Empty;

            if (VerificarPermisos(Credencial))
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                ControlFUP controlFup = new ControlFUP();
                parametros.Add("@pRepId", IdRepresentante);
                parametros.Add("@pPaisId", idPais);
                parametros.Add("@pCiudadId", idCiudad);
                parametros.Add("@pClienteId", idCliente);
                parametros.Add("@pObraId", idObra);
                parametros.Add("@pContactoId", idContacto);
                parametros.Add("@pFupId", idFup);

                List<ListaFup_UV> ListaFup_UV = ControlDatos.EjecutarStoreProcedureConParametros<ListaFup_UV>("USP_fup_SEL_ListaFUPs_Univalle", parametros);

                response = JsonConvert.SerializeObject(ListaFup_UV);
            }
            else
            {
                response = "No Valido";
            }
            return response;
        }

        [WebMethod, SoapHeader("Credencial")]
        public string getEstadisticas(int IdRepresentante, int rol)
        {

            string response = string.Empty;

            if (VerificarPermisos(Credencial))
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                ControlFUP controlFup = new ControlFUP();
                parametros.Add("@pRepId", IdRepresentante);
                parametros.Add("@pRolId", rol);

                List<TopCliente_UV> LstTop = ControlDatos.EjecutarStoreProcedureConParametros<TopCliente_UV>("USP_fup_SEL_EstadisticasTop_Univalle", parametros);
                List<Estadisticas_UV> Lstfup = ControlDatos.EjecutarStoreProcedureConParametros<Estadisticas_UV>("USP_fup_SEL_Estadisticas_Univalle", parametros);

                Estadisticas_UV esta = new Estadisticas_UV();

                esta = Lstfup.FirstOrDefault();
                esta.top_clientes = LstTop;

                response = JsonConvert.SerializeObject(esta);
            }
            else
            {
                response = "No Valido";
            }
            return response;
        }

        [WebMethod, SoapHeader("Credencial")]
        public string getEstadisticasCrm(int IdRepresentante, int rol, int anio)
        {

            string response = string.Empty;

            if (VerificarPermisos(Credencial))
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>();

                // Tipo 1
                parametros.Add("@pRepId", IdRepresentante);
                parametros.Add("@pRolId", rol);
                parametros.Add("@pANIO", anio);
                parametros.Add("@pTipo", 1);

                List<CrmTotalVenta> Lst1 = ControlDatos.EjecutarStoreProcedureConParametros<CrmTotalVenta>("USP_fup_SEL_Estadisticas_CRM", parametros);

                // Tipo 2
                parametros.Clear();
                parametros.Add("@pRepId", IdRepresentante);
                parametros.Add("@pRolId", rol);
                parametros.Add("@pANIO", anio);
                parametros.Add("@pTipo", 2);

                List<CrmVentaMes> Lst2 = ControlDatos.EjecutarStoreProcedureConParametros<CrmVentaMes>("USP_fup_SEL_Estadisticas_CRM", parametros);

                // Tipo 3
                parametros.Clear();
                parametros.Add("@pRepId", IdRepresentante);
                parametros.Add("@pRolId", rol);
                parametros.Add("@pANIO", anio);
                parametros.Add("@pTipo", 3);

                List<Crmseguimiento> Lst3 = ControlDatos.EjecutarStoreProcedureConParametros<Crmseguimiento>("USP_fup_SEL_Estadisticas_CRM", parametros);

                // Tipo 4
                parametros.Clear();
                parametros.Add("@pRepId", IdRepresentante);
                parametros.Add("@pRolId", rol);
                parametros.Add("@pANIO", anio);
                parametros.Add("@pTipo", 4);

                List<CrmProbabilidad> Lst4 = ControlDatos.EjecutarStoreProcedureConParametros<CrmProbabilidad>("USP_fup_SEL_Estadisticas_CRM", parametros);


                Dictionary<string, object> queryResult = new Dictionary<string, object>();
                queryResult.Add("totalVenta", Lst1);
                queryResult.Add("VentaMes", Lst2);
                queryResult.Add("Seguimiento", Lst3);
                queryResult.Add("Probabilidad", Lst4);

                response = JsonConvert.SerializeObject(queryResult);
                return response;
            }
            else
            {
                response = "No Valido";
            }
            return response;
        }

        [WebMethod, SoapHeader("Credencial")]
        public string GuardarListadoPiezas_UV(ListaPiezas_UV datos)
        {
            
            Dictionary<string, object> parametros = new Dictionary<string, object>();

            // Borrar datos de tablas si existen
            parametros.Clear();
            parametros.Add("@pFupID", datos.id_fup);
            parametros.Add("@pVersion", datos.version_fup);
            
            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_DEL_Listado_Piezas", parametros);

            #region ProcesoListado
            //Guardar Detalle Piezas
            if (datos.piezas.Count() > 0)
            {
                datos.piezas.ForEach(delegate (PiezaUV ptra)
                {
                    parametros.Clear();
                    parametros.Add("@pFupID", datos.id_fup);
                    parametros.Add("@pVersion", datos.version_fup);
                    parametros.Add("@pOrigen", 10);
                    parametros.Add("@pId", "UV_"+ ptra.Nombrepieza);
                    parametros.Add("@pNombrepieza", ptra.Nombrepieza);
                    parametros.Add("@pDescripcion", ptra.descrip_aux);
                    parametros.Add("@pFamilia", ptra.familia);
                    parametros.Add("@pCantidad", ptra.cant);
                    parametros.Add("@pAncho", ptra.ancho);
                    parametros.Add("@pAlto", ptra.alto);
                    parametros.Add("@pAncho2", ptra.ancho2);
                    parametros.Add("@pAlto2", ptra.alto2);
                    parametros.Add("@pArea", ptra.areaItem);
                    parametros.Add("@pPlano", ptra.plano);
                    parametros.Add("@pUsuario", datos.usuario);
                    parametros.Add("@pModulacion", ptra.modulacion);
                    parametros.Add("@pConsecutivo", ptra.consecutivo);

                    ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_ListadoPiezas", parametros);
                }
                );

            }
            #endregion

            string response = "Terminado";
            return response;
        }

        [WebMethod, SoapHeader("Credencial")]
        public string getListadoPiezas_UV(int fup, string version)
        {

            string response = string.Empty;

            if (VerificarPermisos(Credencial))
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                ControlFUP controlFup = new ControlFUP();
                parametros.Add("@pFupID", fup);
                parametros.Add("@pVersion", version);

                List<PiezaUV> ListaPiezas = ControlDatos.EjecutarStoreProcedureConParametros<PiezaUV>("USP_fup_SEL_Listado_Piezas", parametros);

                response = JsonConvert.SerializeObject(ListaPiezas);
            }
            else
            {
                response = "No Valido";
            }
            return response;
        }

        [WebMethod, SoapHeader("Credencial")]
        public string GuardarListaAccesorios_UV(ListaAccesorios_UV datos)
        {

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            int id = 0;

            // Borrar datos de tablas si existen
            parametros.Clear();
            parametros.Add("@pFupID", datos.id_fup);
            parametros.Add("@pVersion", datos.version_fup);

            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_DEL_Listado_Accesorios", parametros);

            #region ProcesoListado
            //Guardar Detalle Piezas
            if (datos.piezas.Count() > 0)
            {
                datos.piezas.ForEach(delegate (AccesoriosUV ptra)
                {
                    parametros.Clear();
                    parametros.Add("@pFupID", datos.id_fup);
                    parametros.Add("@pVersion", datos.version_fup);
                    parametros.Add("@pDescripcion", ptra.descripcion);
                    parametros.Add("@pDescripaux", ptra.descrip_aux);
                    parametros.Add("@pCantidad", ptra.cant);
                    parametros.Add("@pDim1", ptra.dim1);
                    parametros.Add("@pDim2", ptra.dim2);
                    parametros.Add("@pDim3", ptra.dim3);
                    parametros.Add("@pDim4", ptra.dim4);
                    parametros.Add("@pDim5", ptra.dim5);
                    parametros.Add("@pDim6", ptra.dim6);
                    parametros.Add("@pPlano", ptra.plano);
                    parametros.Add("@pUsuario", datos.usuario);
                    parametros.Add("@pOrigen", 10);
                    parametros.Add("@pModulacion", ptra.modulacion);
                    parametros.Add("@pConsecutivo", ptra.consecutivo);

                    id = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_UPD_ListadoAccesorios", parametros);
                }
                );

            }
            #endregion

            string response;
            if (id == -1)
                response = "Terminado con error";
            else
                response = "Terminado";
            return response;
        }

        [WebMethod, SoapHeader("Credencial")]
        public string getListadoAccesorios_UV(int fup, string version)
        {

            string response = string.Empty;

            if (VerificarPermisos(Credencial))
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                ControlFUP controlFup = new ControlFUP();
                parametros.Add("@pFupID", fup);
                parametros.Add("@pVersion", version);

                List<AccesoriosUV> ListaAccesorios = ControlDatos.EjecutarStoreProcedureConParametros<AccesoriosUV>("USP_fup_SEL_Listado_Accesorios", parametros);

                response = JsonConvert.SerializeObject(ListaAccesorios);
            }
            else
            {
                response = "No Valido";
            }
            return response;
        }

        [WebMethod, SoapHeader("Credencial")]
        public string GuardarResumenForline_UV(ListaResumeForline_UV datos)
        {

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            int id = 0;

            // Borrar datos de tablas si existen
            parametros.Clear();
            parametros.Add("@pFupID", datos.id_fup);
            parametros.Add("@pVersion", datos.version_fup);
            parametros.Add("@pOrigen", 2); // Cargue de Datos UV

            ControlDatos.GuardarStoreProcedureConParametros("USP_fup_DEL_tmp_SalidaCotizacion", parametros);


            //Guardar Detalle Piezas
            if (datos.piezas.Count() > 0)
            {
                datos.piezas.ForEach(delegate (ListaResForline_UV ptra)
                {
                    parametros.Clear();
                    parametros.Add("@pFupID", datos.id_fup);
                    parametros.Add("@pVersion", datos.version_fup);
                    parametros.Add("@pidgrupoCotizacion", ptra.idgrupoCotizacion);
                    parametros.Add("@pgrupoCotizacion", ptra.grupoCotizacion);
                    parametros.Add("@pmetros", ptra.metros);
                    parametros.Add("@pvalor", 0);
                    parametros.Add("@pmetrosEsp", ptra.metrosEsp);
                    parametros.Add("@pConsecutivo", ptra.consecutivo);
                    parametros.Add("@pUsuario", datos.usuario);
                    parametros.Add("@pHora", -1);
                    parametros.Add("@pOrigen", 2); // Cargue de Datos UV
                    parametros.Add("@pModulacion", ptra.modulacion);

                    id = ControlDatos.GuardarStoreProcedureConParametros("USP_fup_INS_tmp_SalidaCotizacion", parametros);
                }
                );
            }

                string response;
            if (id == -1)
                response = "Terminado con error";
            else
                response = "Terminado";
            return response;
        }

        [WebMethod, SoapHeader("Credencial")]
        public string getResumenForline_UV(int fup, string version)
        {

            string response = string.Empty;

            if (VerificarPermisos(Credencial))
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                ControlFUP controlFup = new ControlFUP();
                parametros.Add("@pFupID", fup);
                parametros.Add("@pVersion", version);

                List<ListaResForline_UV> ListaResumen = ControlDatos.EjecutarStoreProcedureConParametros<ListaResForline_UV>("USP_fup_SEL_ResumenForline_UV", parametros);

                response = JsonConvert.SerializeObject(ListaResumen);
            }
            else
            {
                response = "No Valido";
            }
            return response;
        }

        [WebMethod, SoapHeader("Credencial")]
        public string getInventarioPiezasOrden(string pOrden)
        {

            string response = string.Empty;

            if (VerificarPermisos(Credencial))
            {
                Dictionary<string, object> parametros = new Dictionary<string, object>();
                ControlFUP controlFup = new ControlFUP();
                parametros.Add("@pOrden", pOrden);

                List<PiezaUV> LstPza = ControlDatos.EjecutarStoreProcedureConParametros<PiezaUV>("USP_fup_SEL_InvListadoPiezasOrden", parametros);

                response = JsonConvert.SerializeObject(LstPza);
            }
            else
            {
                response = "No Valido";
            }
            return response;
        }
        //Hasta acá //////////////////////////////////////////////////

        [WebMethod, SoapHeader("Credencial")]
        public string ValidarLogin(string Usuario, string Password)
        {
            string response;
            int rol = -1, usuId = -1;
            string rcID = "0", nomUsuario = "";

            SqlDataReader reader = null, rdRC = null;

            if (VerificarPermisos(Credencial))
            {
                ControlInicio CI = new ControlInicio();

                // revisar reader
                int existeLogin = CI.verificarLogin(Usuario);
                if (existeLogin != 0)
                {
                    string passwdOk = CI.verificarContrasena(Usuario, Password);
                    if (passwdOk == Password)
                    {
                        reader = CI.obtenerRolByUsuLogin(Usuario);

                        if (reader.HasRows)
                        {
                            reader.Read();
                            rol = reader.GetInt32(0);
                            usuId = reader.GetInt32(1);

                            //consula los datos del usuario
                            if ((rol == 3) || (rol == 9) || (rol == 2) || (rol == 28) || (rol == 33) || (rol == 30) || (rol == 46))
                            {
                                rdRC = CI.ObtenerRepresentante(Usuario);
                                if (rdRC.HasRows == true)
                                {
                                    rdRC.Read();

                                    nomUsuario = rdRC.GetValue(1).ToString();
                                    rcID = rdRC.GetValue(0).ToString();
                                }
                                rdRC.Close();
                                rdRC.Dispose();
                            }
                            else
                            { nomUsuario = "No es Comercial"; }

                            reader.Close();
                            reader.Dispose();
                            CI.CerrarConexion();
                        }
                        else
                        { nomUsuario = "Rol No encontrado"; }

                    }
                    else
                    { nomUsuario = "Password No Coincide"; }

                }
                else
                { nomUsuario = "Login No existe"; }

            }
            else
            { nomUsuario = "No Autorizado"; }

            var respuesta = new
            {
                rol = rol,
                IdUsuario = usuId,
                Usuario = nomUsuario,
                IdRepresentante = rcID
            };

            response = JsonConvert.SerializeObject(respuesta);
            return response;
        }

        public class Autenticacion : SoapHeader
        {
            public string Clave;
            public string Usuario;

        }

}

    internal class function
    {
    }
}
