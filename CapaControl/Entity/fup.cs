using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaControl.Entity
{
    public class formato_unico
    {
        public int fup_id { get; set; }
        public int pai_id { get; set; }
        public bool fup_ch_accesorios { get; set; }
        public int representanteId { get; set; }
    }

    public class fup_guardar
    {
        public int ID_Cliente { get; set; }
        public int ID_Moneda { get; set; }
        public int ID_Contacto { get; set; }
        public int ID_Obra { get; set; }
        public string Usuario { get; set; }
        public int? FupAnterior { get; set; }
        public int TipoNegociacion { get; set; }
        public int TipoCotizacion { get; set; }
        public int Producto { get; set; }
        public int TipoVaciado { get; set; }
        public int SistemaSeguridad { get; set; }
        public int NumeroEquipos { get; set; }
        public string AlturaLibre { get; set; }
        public string AlineacionVertical { get; set; }
        public int FormaConstructiva { get; set; }
        public decimal DistanciaEdifica { get; set; }
        public int DilatacionMuro { get; set; }
        public decimal EspesorJunta { get; set; }
        public int Desnivel { get; set; }
        public int MaxPisos { get; set; }
        public int FundicionPisos { get; set; }

        public string AlturaIntSugerida { get; set; }
        public string TipoFachada { get; set; }
        public string AlturaUnion { get; set; }
        public string TipoUnion { get; set; }
        public string DetalleUnion { get; set; }
        public string AlturaCAP1 { get; set; }
        public string AlturaCAP2 { get; set; }
        public string AlturaLibreMinima { get; set; }
        public string AlturaLibreMaxima { get; set; }
        public string AlturaLibreCual { get; set; }
        public bool? ReqCliente { get; set; }
        public string AlturaIntSugeridaCliente { get; set; }
        public string TipoFachadaCliente { get; set; }
        public string AlturaUnionCliente { get; set; }
        public string TipoUnionCliente { get; set; }
        public string DetalleUnionCliente { get; set; }
        public string AlturaCAP1Cliente { get; set; }
        public string AlturaCAP2Cliente { get; set; }
        public string TerminoNegociacion { get; set; }
        public int TotalUnidadesConstruir { get; set; }
        public int TotalUnidadesConstruirForsa { get; set; }
        public float MetrosCuadradosVivienda { get; set; }
        public int Estrato { get; set; }
        public int TipoVivienda { get; set; }
        public int ClaseCotizacion { get; set; }
        public int IdFUP { get; set; }
        public string Version { get; set; }
        public string Probabilidad { get; set; }
        public string otros { get; set; }
        public string CapPernado { get; set; }
        public string FecSolicitaCliente { get; set; }
        public List<fup_tablas> datos_tablas { get; set; }
        public Int16? EquipoCopia { get; set; }
        public int? FupCopia { get; set; }
        public string obra_link { get; set; }
        public string Obra_FecInicio { get; set; }
        public int? VendedorZona { get; set; }
        public int? RecomendacionTecnico { get; set; }
        public DateTime? FecCreaVersion { get; set; }
        public int? FupRefServicios { get; set; }
    }

    public class fup_tablas
    {
        public int cot_id { get; set; }
        public int tipo_tabla { get; set; }
        public int consecutivo { get; set; }
        public string valor { get; set; }
    }

    public class fup_tablas2 : fup_tablas
    {
        public string Comentario { get; set; }
    }

    public class fup_CodPago
    {
        public int TipoPago { get; set; }
        public int Consecutivo { get; set; }
        public DateTime Fecha { get; set; }
        public string Condicion { get; set; }
        public decimal Valor { get; set; }
        public int BoletosBancarios { get; set; }
    }

    public class AvalesFabricacion
    {
        public int IdFUP { get; set; }
        public string Version { get; set; }
        public int AutorizaJuridico { get; set; }
        public int AutorizaJuridicoDesp { get; set; }
        public int AutorizaTesoreria { get; set; }
        public int AutorizaTesoreria2 { get; set; }
        public int AutorizaGercom { get; set; }
        public int AutorizaGercomDesp { get; set; }
        public int AutorizaVicecom { get; set; }
        public int AutorizaVicecomDesp { get; set; }
        public string AutorizaJuridico_Observacion { get; set; }
        public string AutorizaJuridico_ObservacionDesp { get; set; }
        public string AutorizaTesoreria_Observacion { get; set; }
        public string AutorizaTesoreria2_Observacion { get; set; }
        public string AutorizaGercom_Observacion { get; set; }
        public string AutorizaGercom_ObservacionDesp { get; set; }
        public string AutorizaVicecom_Observacion { get; set; }
        public string AutorizaVicecom_ObservacionDesp { get; set; }
        public int? cmJuridico { get; set; }
        public int? cmJuridico2 { get; set; }
        public int? cmTesoreria { get; set; }
        public int? cmTesoreria2 { get; set; }
        public int? cmGercom { get; set; }
        public int? cmGercom2 { get; set; }
        public int? cmVicecom { get; set; }
        public int? cmVicecom2 { get; set; }
        public DateTime firmaContratoAproba { get; set; }
        public DateTime FormalizaPagoAproba { get; set; }
    }

    public class AvalesFabricacionDespliega
    {
        //public int IdFUP { get; set; }
        //public string Version { get; set; }
        public string Tipo { get; set; }
        public string Condicion { get; set; }
        public DateTime Fecha { get; set; }
        public string Observaciones { get; set; }
        public string Usuario { get; set; }
    }

    public class ItemDinamico
    {
        public int IdFUP { get; set; }
        public string Version { get; set; }
        public string Parte { get; set; }
        public int IdItemParte { get; set; }
        public string TipoRegistro { get; set; }
        public string TipoAyuda { get; set; }
        public string TextoAyuda { get; set; }
        public string CodItem { get; set; }
        public string DescipcionItem { get; set; }
        public bool? SiNoItem { get; set; }
        public bool VaLista { get; set; }
        public string DomLista { get; set; }
        public string TextoLista { get; set; }
        public bool VaAdicional { get; set; }
        public bool? SiNoAdicional { get; set; }
        public string CantAdicional { get; set; }
        public bool VaObservacion { get; set; }
        public string Observacion { get; set; }
        public bool Bloq_SINO_item { get; set; }
        public bool Bloq_SINO_Add { get; set; }
        public string UnidadMedida { get; set; }
        public bool ObsRequerida { get; set; }
        public int Orden { get; set; }
        public int fcp_OrdenParte { get; set; }
        public string TextoListaDesc { get; set; }
        public string Defecto_ItemTextoLista { get; set; }
        public List<Dominios> dominio { get; set; }
    }

    public class ObtenerItemDinamico
    {
        public int pFupID { get; set; }
        public string pVersion { get; set; }
        public int TipoCotizacion { get; set; }
        public int Parte { get; set; }
        public string Idioma { get; set; }
    }

    public class ItemDinamicoGuardar
    {
        public int? pFupID { get; set; }
        public string pVersion { get; set; }
        public int? pItemparte_id { get; set; }
        public bool? pItemSiNo { get; set; }
        public string pItemTextoLista { get; set; }
        public bool? pAdicionalSiNo { get; set; }
        public decimal? pAdicionalCantidad { get; set; }
        public string Descripcion { get; set; }
    }

    public class ItemEquipo
    {
        public int? pFupID { get; set; }
        public string pVersion { get; set; }
        public int? TipoEquipo { get; set; }
        public int? Consecutivo { get; set; }
        public int? Cantidad { get; set; }
        public string Descripcion { get; set; }
    }

    public class VersionFup
    {
        public int eect_id { get; set; }
        public string eect_vercot_id { get; set; }
        public int eect_fup_id { get; set; }
    }

    public class fup_consultar : fup_guardar
    {
        public int ID_pais { get; set; }
        public string Pais { get; set; }
        public string PaisObra { get; set; }
        public string Obra { get; set; }
        public string Cliente { get; set; }
        public string Moneda { get; set; }
        public int ID_ciudad { get; set; }
        public string Fecha_crea { get; set; }
        public string EstadoCli { get; set; }
        public string UsuarioCrea { get; set; }
        public string Cotizador { get; set; }
        public string EstadoProceso { get; set; }
        public string FecFacturar { get; set; }
        public string FecEntregaCliente { get; set; }
        public int ArmadoAluminio { get; set; }
        public int ArmadoEscalera { get; set; }
        public int ArmadoAccesorio { get; set; }
        public string Contacto { get; set; }
        public string Ciudad { get; set; }
        public string AlturaLibreDesc { get; set; }
        public string ProductoCopia { get; set; }
        public string TipoCotizaDesc { get; set; }
        public string ProductoDesc { get; set; }
        public string TipoNegociacionDesc { get; set; }
        public string TipoVaciadoDesc { get; set; }
        public string ClaseCotizacionDesc { get; set; }
        public string TipoFachadaDesc { get; set; }
        public string TerminoNegociacionDesc { get; set; }
        public string TipoViviendaDesc { get; set; }
        public string SistemaSegDesc { get; set; }
        public string AlinVertDesc { get; set; }
        public string DetUnionDesc { get; set; }
        public string FormaConstructivaDesc { get; set; }
        public string DesnivelDesc { get; set; }
        public string CriteriosEspecialesRuta { get; set; }
        public string GerenteZona { get; set; }
        public string ComercialZona { get; set; }
        public string VaMesaTecnicaDespacho { get; set; }
        public bool AutorizaVerPrecio { get; set; }
        public string FecReunionMesa { get; set; }
        public string FecSolicitaSimulacion { get; set; }
        public bool? NoEjecPreviaDespacho { get; set; }
        public string NoEjecPreviaDespachoCom { get; set; }
        public bool? NoEjecPostventa { get; set; }
        public string NoEjecPostventaCom { get; set; }
        public string EstrellaMes { get; set; }
        public bool AutorizaSubirPlanos { get; set; }
        public int VersionCarta { get; set; }
    }

    public class datosCombo
    {
        public int id { get; set; }
        public string descripcion { get; set; }
    }

    public class datosCombo2
    {
        public string id { get; set; }
        public string planta_id { get; set; }
        public string descripcion { get; set; }
        public string descripcionEN { get; set; }
        public string descripcionPO { get; set; }
    }

    public class datosCombo3
    {
        public string id { get; set; }
        public string descripcion { get; set; }
        public string descripcionEN { get; set; }
        public string descripcionPO { get; set; }
        public bool usoAlcance { get; set; }
    }
    public class datosProducto
    {
        public string id { get; set; }
        public string descripcion { get; set; }
        public string PrefijoListaAsistida { get; set; }
        public decimal AccsListaAsistida { get; set; }
        public decimal EspanListaAsistida { get; set; }
    }

    public class guardar_logros_destacados
    {
        public string data { get; set; }
        public int IdFUP { get; set; }
        public string Version { get; set; }
    }

    public class fup_aprobacion_guardar
    {
        public int IdFUP { get; set; }
        public string Version { get; set; }
        public int? Visto_bueno { get; set; }
        public int? NumeroModulaciones { get; set; }
        public int? NumeroCambios { get; set; }
        public int? MotivoRechazo { get; set; }
        public string ObservacionAprobacion { get; set; }
        public string estado { get; set; }
        public string AlturaFormaleta { get; set; }
        public int? NivelComplejidad { get; set; }
        public DateTime? FecPolitica { get; set; }
        public int? DiasPolitica { get; set; }
        public int? TipoProyectoApId { get; set; }
        public bool? ProyectoEnConstruccion { get; set; }
        public bool? PlanosCoordinados { get; set; }
        public bool? InicioCercano { get; set; }
        public bool? SoloPlanos { get; set; }
        public bool? PlanosNoCoordinados { get; set; }
        public bool? SinInformacionInicio { get; set; }
        public bool? PlanosDibujo { get; set; }
        public bool? VaPreventa { get; set; }
    }

    public class fup_aprobacion_consulta
    {
        public int IdFUP { get; set; }
        public string Version { get; set; }
        public string rct_fecha_rechazo { get; set; }
        public int VistoBueno { get; set; }
        public int Modulaciones { get; set; }
        public int Cambios { get; set; }
        public string tre_descripcion { get; set; }
        public string rct_observacion { get; set; }
        public string estado { get; set; }
        public string AlturaFormaleta { get; set; }
        public string EnAnalisis { get; set; }
        public float CtM2 { get; set; }
        public float CtPiezas { get; set; }
        public float CtM2Piezas { get; set; }
        public int NoReferencia { get; set; }
        public string DescSisSeguridad { get; set; }
        public string DescDetalles { get; set; }
        public string NivelComplejidad { get; set; }
        public int IdNivelComplejidad { get; set; }
        public string EstadoDft { get; set; }
        public DateTime FecAprobDft { get; set; }
        public DateTime FecAprobClienteDft { get; set; }
        public string VersionAprobDft { get; set; }
        public string RequiereRecotizaDft { get; set; }
        public string ObservacionDft { get; set; }
        public DateTime? FecVisto { get; set; }
        public float HorasDetalles { get; set; }
        public DateTime? FecProgramada { get; set; }
        public DateTime? FecPolitica { get; set; }
        public int? DiasPolitica { get; set; }
        public int? TipoProyectoApId { get; set; }
        public bool? ProyectoEnConstruccion { get; set; }
        public bool? PlanosCoordinados { get; set; }
        public bool? InicioCercano { get; set; }
        public bool? SoloPlanos { get; set; }
        public bool? PlanosNoCoordinados { get; set; }
        public bool? SinInformacionInicio { get; set; }
        public bool? PlanosDibujo { get; set; }
        public bool? VaPreventa { get; set; }
        public DateTime? FecPreventa { get; set; }
        public string UsuPreventa { get; set; }
        public int? ExisteMesaPreventa { get; set; }
    }

    public class fup_AlturaFormaleta_guardar
    {
        public int IdFUP { get; set; }
        public string Version { get; set; }
        public string EnAnalisis { get; set; }
    }


    public class planos
    {
        public int id_plano { get; set; }
        public string plano_nombre_real { get; set; }
        public int id_fup_plano { get; set; }
        public string plano_ruta_archivo { get; set; }
        public string plano_descripcion { get; set; }
        public DateTime fecha_crea { get; set; }
        public string usu_crea { get; set; }
        public int plano_tipo_anexo_id { get; set; }
    }

    public class anexos
    {
        public string tan_desc_esp { get; set; }
        public string tan_desc_eng { get; set; }
        public string tan_desc_por { get; set; }
        public string Anexo { get; set; }
        public string Ruta { get; set; }
        public string plano_descripcion { get; set; }
        public string fecha_crea { get; set; }
        public int id_plano { get; set; }
        public string estado { get; set; }
        public string Eventoptf { get; set; }
        public int id_tipoAnexo { get; set; }
    }

    public class DetalleCondicionesPago
    {
        public string Entradacotid { get; set; }
        public string TipoPago { get; set; }
        public string Ruta { get; set; }
        public string plano_descripcion { get; set; }
        public string fecha_crea { get; set; }
        public int id_plano { get; set; }
        public string estado { get; set; }
        public string Eventoptf { get; set; }
    }

    public class DuplicaFUP
    {
        public int IdFUP { get; set; }
        public string Version { get; set; }
        public string UsuarioCrea { get; set; }
        public string IdCliente { get; set; }
        public int IdContacto { get; set; }
        public string IdObra { get; set; }
    }

    public class referencia
    {
        public string OrdenReferencia { get; set; }
        public string hecha_en { get; set; }
    }

    public class referenciaCopia
    {
        public int Fup { get; set; }
        public string ProductoCopia { get; set; }
        public string OrdenReferencia { get; set; }
        public string hecha_en { get; set; }
    }

    public class ventaCierreComercial
    {
        public string observacion { get; set; }
        public string observacionVar_m2 { get; set; }
    }

    public class fup_salida_cotizacion
    {
        public decimal m2_equipo { get; set; }
        public decimal vlr_equipo { get; set; }
        public decimal m2_adicionales { get; set; }
        public decimal vlr_adicionales { get; set; }
        public decimal m2_Detalle_arquitectonico { get; set; }
        public decimal vlr_Detalle_arquitectonico { get; set; }
        public decimal m2_sis_seguridad { get; set; }
        public decimal vlr_sis_seguridad { get; set; }
        public decimal vlr_accesorios_basico { get; set; }
        public decimal vlr_accesorios_complementario { get; set; }
        public decimal vlr_accesorios_opcionales { get; set; }
        public decimal vlr_accesorios_adicionales { get; set; }
        public decimal vlr_otros_productos { get; set; }
        public string total_m2 { get; set; }
        public string total_valor { get; set; }
        public string fupid { get; set; }
        public string version { get; set; }
        public string total_propuesta_com { get; set; }
        public int? vlr_Contenedor20 { get; set; }
        public int? vlr_Contenedor40 { get; set; }
        public int tipoSalida { get; set; }
        public int NumeroModulacionesSC { get; set; }
        public int NumeroCambiosSC { get; set; }
        //public string FechaAvalCierre { get; set; }
        public string ConsideracionObservacionCliente { get; set; }
        public int? CartaDFTManual { get; set; }
        public DateTime? FechaSolicitudDFTManual { get; set; }
        public DateTime? FechaAutorizacionDFTManual { get; set; }
        public string UsuarioAutorizaDFTManual { get; set; }

        public decimal valorEXWBase { get; set; }
    }

    public class planos_tipo_forsa
    {
        public int spf_id { get; set; }
        public string Evento { get; set; }
        public string plano { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime FechaCierre { get; set; }
        public string Responsable { get; set; }
        public string Observacion { get; set; }
        public string Usuario { get; set; }
        public int Evento_id { get; set; }
        public DateTime FechaActa { get; set; }
        public int vaAnexo { get; set; }
    }

    public class PTF_guardar
    {
        public string Evento { get; set; }
        public int? Plano { get; set; }
        public DateTime? FechaCierre { get; set; }
        public string Responsable { get; set; }
        public string Observacion { get; set; }
        public int Recotiza { get; set; }
        public DateTime? FecProgramadaSCI { get; set; }

    }

    public class lista_recotizacion
    {
        public DateTime Fecha { get; set; }
        public string Version { get; set; }
        public string Motivo { get; set; }
        public string Observacion { get; set; }
    }

    public class Orden_Fabricacion
    {
        public int Id_Ofa { get; set; }
        public string TIPO { get; set; }
        public string ORDEN { get; set; }
        public string PRODUCIDO_EN { get; set; }
        public string VERSION { get; set; }
        public string PARTE { get; set; }
        public string FECHA { get; set; }
        public string RESPONSABLE { get; set; }
        public double M2 { get; set; }
        public double VALOR { get; set; }
        public double m2Prod { get; set; }
        public double m2Diferencia { get; set; }
        public double CantidadPiezas { get; set; }
        public double CantidadPiezasAcc { get; set; }
        public string Moduladores { get; set; }
        public string RutaListaEmpaque { get; set; }
       
    }

    public class Parte_Orden_Fabricacion
    {
        public int Sf_id { get; set; }
        public string Parte { get; set; }
        public string M2 { get; set; }
        public string VALOR { get; set; }
    }

    public class Planta_Orden_Fabricacion
    {
        public int planta_id { get; set; }
        public string planta_descripcion { get; set; }
    }

    public class FechaSolicitud
    {
        //        public int IdFUP { get; set; }
        //        public string Version { get; set; }
        public DateTime FechaFirmaContrato { get; set; }
        public DateTime FechaContractual { get; set; }
        public DateTime FechaAnticipado { get; set; }
        public int Plazo { get; set; }
        public int DiasAsistec { get; set; }
        public double m2Cerrados { get; set; }
        public double ValorFinal { get; set; }
        public double valorFacturacion { get; set; }
        public double m2Modulados { get; set; }
        public double valorFactModulado { get; set; }
        public double vlr_dcto_n1 { get; set; }
        public double vlr_dcto_n2 { get; set; }
        public double vlr_dcto_n3 { get; set; }
        public double vlr_dcto_n4 { get; set; }
        public double vlr_dcto_n5 { get; set; }
        public int AutorizaJuridico { get; set; }
        public int AutorizaTesoreria { get; set; }
        public int AutorizaTesoreria2 { get; set; }
        public int AutorizaGercom { get; set; }
        public int AutorizaVicecom { get; set; }
        public string AutorizaJuridico_Observacion { get; set; }
        public string AutorizaTesoreria_Observacion { get; set; }
        public string AutorizaTesoreria2_Observacion { get; set; }
        public string AutorizaGercom_Observacion { get; set; }
        public string AutorizaVicecom_Observacion { get; set; }
        public int? MetodoPago { get; set; }
        public string ObservaMayordscto { get; set; }
        public DateTime FechaAprobacionPlanos { get; set; }
        public DateTime FechaPactadaPlan { get; set; }
        public int AutorizaJuridicoDesp { get; set; }
        public int AutorizaTesoreriaDesp { get; set; }
        public int AutorizaGercomDesp { get; set; }
        public int AutorizaVicecomDesp { get; set; }
        public string AutorizaJuridicoDesp_Observacion { get; set; }
        public string AutorizaTesoreriaDesp_Observacion { get; set; }
        public string AutorizaGercomDesp_Observacion { get; set; }
        public string AutorizaVicecomDesp_Observacion { get; set; }
        public string ObservaFechas { get; set; }
        public double vlr_dcto_mf_n1 { get; set; }
        public double vlr_dcto_mf_n2 { get; set; }
        public double vlr_dcto_mf_n3 { get; set; }
        public double vlr_dcto_mf_n4 { get; set; }
        public double vlr_dcto_mf_n5 { get; set; }
        public string ObservaDscto { get; set; }
        public int DiasConsumidos { get; set; }
        public int? Plazo_Tdn { get; set; }
        public DateTime? FechaContractual_Tdn { get; set; }
        public int? Plazo_Neg { get; set; }
        public bool FacturaM2Modulados { get; set; }
        public double ValorM2Factura { get; set; }
    }

    public class FechaSolicitudGuarda
    {
        public int IdFUP { get; set; }
        public string Version { get; set; }
        public DateTime FechaFirmaContrato { get; set; }
        public DateTime FechaContractual { get; set; }
        public DateTime FechaFormalizaPago { get; set; }
        public int Plazo { get; set; }
        public double vlr_dcto_n1 { get; set; }
        public double vlr_dcto_n2 { get; set; }
        public double vlr_dcto_n3 { get; set; }
        public double vlr_dcto_n4 { get; set; }
        public double vlr_dcto_n5 { get; set; }
        public bool AutorizaJuridico { get; set; }
        public bool AutorizaTesoreria { get; set; }
        public bool AutorizaTesoreria2 { get; set; }
        public bool AutorizaGercom { get; set; }
        public bool AutorizaVicecom { get; set; }
        public string AutorizaJuridico_Observacion { get; set; }
        public string AutorizaTesoreria_Observacion { get; set; }
        public string AutorizaTesoreria2_Observacion { get; set; }
        public string AutorizaGercom_Observacion { get; set; }
        public string AutorizaVicecom_Observacion { get; set; }
        public int? MetodoPago { get; set; }
        public string ObservaMayordscto { get; set; }
        public DateTime FechaAprobacionPlanos { get; set; }
        public DateTime FechaPactadaPlan { get; set; }
        public string ObservaFechas { get; set; }
        public int? TerminoNeg { get; set; }
        public double vlr_dcto_mf_n1 { get; set; }
        public double vlr_dcto_mf_n2 { get; set; }
        public double vlr_dcto_mf_n3 { get; set; }
        public double vlr_dcto_mf_n4 { get; set; }
        public double vlr_dcto_mf_n5 { get; set; }
        public string ObservaDscto { get; set; }
        public int? Plazo_Tdn { get; set; }
        public DateTime? FechaContractual_Tdn { get; set; }
        public int? Plazo_Neg { get; set; }
        public bool FacturaM2Modulados { get; set; }
        public double ValorM2Factura { get; set; }
    }

    public class EstadoFup
    {
        public int IdEstadoActual { get; set; }
        public string EstadoActual { get; set; }
        public int RequiereElabora { get; set; }
        public int OrdenParte { get; set; }
        public string Parte { get; set; }
        public int CantidadGrabada { get; set; }
        public int RequiereCT { get; set; }
        public int ExisteCT { get; set; }
        public int Autogestion { get; set; }
        public string FecSimulacion { get; set; }
        public int ExisteCI { get; set; }
        public string FecSimulacionCI { get; set; }
        public int CotizacionRapida { get; set; }
    }

    public class NotificaFup
    {
        public string AsuntoMail { get; set; }
        public string Lista { get; set; }
        public string Msg { get; set; }
        public bool? Anexo { get; set; }
        public string LinkAnexo { get; set; }
    }

    public class Comentarios
    {
        public int IdFUP { get; set; }
        public string Version { get; set; }
        public int Idtipo { get; set; }
        public string comentario { get; set; }
        public int consecutivo { get; set; }
    }

    public class LinksSave
    {
        public int FupID { get; set; }
        public string Version { get; set; }
        public int Consecutivo { get; set; }
        public string Link { get; set; }
        public string Descripcion { get; set; }
    }

    public class LinksQuery
    {
        public int salcot_id { get; set; }
        public int consecutivo { get; set; }
        public string Link { get; set; }
        public string Descripcion { get; set; }
    }

    public class ListaComentarios
    {
        public int Consecutivo { get; set; }
        public DateTime Fecha { get; set; }
        public string usuario { get; set; }
        public string Comentario { get; set; }
    }

    public class CotizacionFUP
    {
        public int IdFUP { get; set; }
        public string Version { get; set; }
        public int? TipoRegistro { get; set; }
        public int? IdGrupo { get; set; }
        public int? item_id { get; set; }
        public string Grupo1 { get; set; }
        public string Grupo2 { get; set; }
        public int? Orden { get; set; }
        public string Item { get; set; }
        public string Item_det { get; set; }
        public string UnidadMedida { get; set; }
        public int? Consecutivo { get; set; }
        public bool Incluir { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? Valor { get; set; }
        public decimal? Descuento { get; set; }
        public string Observacion { get; set; }
        public int ItemCotiza_id { get; set; }
        public decimal? Unitario { get; set; }
        public decimal Impuesto { get; set; }
        public int TerminoNegociacion { get; set; }
        public decimal? ValorMinimo { get; set; }
        public decimal? DsctoMax { get; set; }
        public int? AutorizaVerPrecio { get; set; }
        public int? IdCartaCierre { get; set; }
        public decimal? ValorOrig { get; set; }
        public decimal? CantidadOrig { get; set; }
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

    public class ItemCosto
    {
        public int Id_Ofa { get; set; }
        public short? TipoRegistro { get; set; }
        public short? TipoItem { get; set; }
        public int? Item { get; set; }
        public string Descripcion { get; set; }
        public int? Mp_Cod_UnoEE { get; set; }
        public string Mp_Nombre { get; set; }
        public string Mp_Desc_Lib { get; set; }
        public string IdCentroTrabajo { get; set; }
        public int? IdSegmentoCosto { get; set; }
        public string SegmentoCosto { get; set; }
        public decimal? Tarifa { get; set; }
        public decimal? Unidades { get; set; }
        public decimal? peso { get; set; }
        public string Unidad { get; set; }
        public decimal? CostoERP { get; set; }
        public decimal? costo { get; set; }
        public bool? EnvioErp { get; set; }
        public int? CostoCero { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? PorcDesperdicio { get; set; }
    }

    public class LineasWs
    {
        public string Linea { get; set; }
    }

    public class fup_segcot_guardar
    {
        public int IdFUP { get; set; }
        public string Version { get; set; }
        public int? AlDias { get; set; }
        public DateTime? AlConfirma { get; set; }
        public DateTime? AlReal { get; set; }
        public DateTime? AlAprobado { get; set; }
        public int? AccDias { get; set; }
        public DateTime? AccConfirma { get; set; }
        public DateTime? AccReal { get; set; }
        public DateTime? AccAprobado { get; set; }
    }

    public class fup_segcot_consulta
    {
        public DateTime? FecAprobado { get; set; }
        public DateTime? FecSolicitaCliente { get; set; }
        public int? AlDias { get; set; }
        public DateTime? AlPlaneda { get; set; }
        public DateTime? AlConfirma { get; set; }
        public DateTime? AlReal { get; set; }
        public DateTime? AlAprobado { get; set; }
        public int? AccDias { get; set; }
        public DateTime? AccPlaneda { get; set; }
        public DateTime? AccConfirma { get; set; }
        public DateTime? AccReal { get; set; }
        public DateTime? AccAprobado { get; set; }
    }

    public class fup_cntrcam_guardar
    {
        public int? IdFUP { get; set; }
        public string Version { get; set; }
        public int? cons { get; set; }
        public int? padre { get; set; }
        public string Comentario { get; set; }
        public string Estado { get; set; }
        public string Titulo { get; set; }
        public int? EsDFT { get; set; }
        public int? EstadoDFT { get; set; }
        public int? SubprocesoDFT { get; set; }
    }
    public class ControlCambiosCierre_Guardar
    {
        public int? IdPQRS { get; set; }
        public int? cons { get; set; }
        public int? padre { get; set; }
        public string Comentario { get; set; }
        public string Estado { get; set; }
        public string Titulo { get; set; }
    }
    public class ControlCambiosCierre_Guardar_PQRS
    {
        public int? IdPQRS { get; set; }
        public int? cons { get; set; }
        public int? padre { get; set; }
        public string Comentario { get; set; }
        public string Estado { get; set; }
        public string Titulo { get; set; }
        public int EventoId { get; set; }
    }
    public class ControlCambiosCierre_Consultar
    {
        public int Id { get; set; }
        public int Cons { get; set; }
        public int Padre { get; set; }
        public DateTime? Fecha { get; set; }
        public string Estado { get; set; }
        public string Usuario { get; set; }
        public string Comentario { get; set; }
        public string Titulo { get; set; }
        public string Nivel { get; set; }
        public string Anexos { get; set; }
    }
    public class ControlCambiosCierre_ConsultarPQRS
    {
        public int Id { get; set; }
        public int Cons { get; set; }
        public int Padre { get; set; }
        public DateTime? Fecha { get; set; }
        public string Estado { get; set; }
        public string Usuario { get; set; }
        public string Comentario { get; set; }
        public string Titulo { get; set; }
        public string Nivel { get; set; }
        public string Anexos { get; set; }
        public string EventoDescripcion { get; set; }
    }
    public class fup_considerationobservation_guardar
    {
        public int? IdFUP { get; set; }
        public string Version { get; set; }
        public int? cons { get; set; }
        public int? padre { get; set; }
        public string Comentario { get; set; }
        public string Estado { get; set; }
        public string Titulo { get; set; }
        public int? EsDFT { get; set; }
        public int? EstadoDFT { get; set; }
        public int? SubprocesoDFT { get; set; }
        public DateTime? FecDespacho { get; set; }
        public DateTime? FecEntrega { get; set; }
        /* Valores tipoEntrada=1:Observacion 2:Seguimiento 3:Hallazgo 4:HojaDeVida */
        public int TipoEntrada { get; set; }
        /* Posibles valores = 1:Consideracion 2:Observacion */
        public int TipoConsideracionObservacion { get; set; }
        /* Campos para hallazgos */
        public string AreaSolicitada { get; set; }
        public int? SolucionadoEnObra { get; set; }
        public int? GeneroCosto { get; set; }
        public string HallazgoOrdenFabricacion { get; set; }
        public string DirectorObra { get; set; }
        public string ResponsableObra { get; set; }
        public string EmailResponsableObra { get; set; }
        public string TelefonoResponsableObra { get; set; }
        public string DireccionObra { get; set; }
    }
    public class fup_cntrcam_consulta
    {
        public int Id { get; set; }
        public int Cons { get; set; }
        public int Padre { get; set; }
        public DateTime? Fecha { get; set; }
        public string Estado { get; set; }
        public string Usuario { get; set; }
        public string Comentario { get; set; }
        public string Titulo { get; set; }
        public string Nivel { get; set; }
        public string Anexos { get; set; }
        public string EstadoDt { get; set; }
        public int EsDft { get; set; }
        public int EstadoDFT { get; set; }
        public int SubprocesoDFT { get; set; }
        public string EstadoDFT_Desc { get; set; }

    }
    public class fup_considerationobservation_consulta
    {
        public int Id { get; set; }
        public int Cons { get; set; }
        public int Padre { get; set; }
        public DateTime? Fecha { get; set; }
        public string Estado { get; set; }
        public string Usuario { get; set; }
        public string Comentario { get; set; }
        public string LogrosDestacados { get; set; }
        public string Titulo { get; set; }
        public string Nivel { get; set; }
        public string Anexos { get; set; }
        public string EstadoDt { get; set; }
        public int EsDft { get; set; }
        public int EstadoDFT { get; set; }
        public int SubprocesoDFT { get; set; }
        public string EstadoDFT_Desc { get; set; }
        public DateTime? FecDespacho { get; set; }
        public DateTime? FecEntrega { get; set; }
        public int TipoEntrada { get; set; }
        /* Posibles valores = 1:Consideracion 2:Observacion */
        public int TipoConsideracionObservacion { get; set; }
        /* Campos para hallazgos */
        public string AreaSolicitada { get; set; }
        public int? SolucionadoEnObra { get; set; }
        public int? GeneroCosto { get; set; }
        public string ResponsableObra { get; set; }
        public string HallazgoOrdenFabricacion { get; set; }
        public string EmailResponsableObra { get; set; }
        public string TelefonoResponsableObra { get; set; }
        public string DireccionObra { get; set; }
        public string DirectorObra { get; set; }
        public int? IdPQRS { get; set; }
        public string EstadoPQRS { get; set; }
        public bool? NoEsProcedente { get; set; }
    }

    public class SegLogistico_FUP_Fechas
    {
        public string IdOrden { get; set; }
        public string Orden { get; set; }
        public DateTime? F_EstimadoArriboMod { get; set; }
        public DateTime? F_EstimadaLlegaObraMod { get; set; }
        public DateTime? F_RealDespacho { get; set; }
        public DateTime? F_ZarpeReal { get; set; }
    }

    // Clase provisional para cargar el IdRepresentante y IdUsuario para Forline
    public class TempDataClassToForline
    {
        public string username { get; set; }
        public int id_user { get; set; }
        public int id_representative { get; set; }
        public int id_role { get; set; }
    }

    public class EstadoGarantias_Consulta
    {
        public string IdOrdenOrigen { get; set; }
        public string OrdenOrigen { get; set; }
        public string OrdenOgOm { get; set; }
        public string Estado { get; set; }
        public string DescripcionReclamo { get; set; }
        public DateTime? FechaDespacho { get; set; }
        public DateTime? F_EntregaObraEstimada { get; set; }
        public DateTime? F_EntregaObraReal { get; set; }
    }

    public class RespuestaFilasAfectadas
    {
        public int affectedRows { get; set; }
    }

    public class MonedaTRM_Consulta
    {
        public int MonedaTrmId { get; set; }
        public int MonedaId { get; set; }
        public string MonedaDescripcion { get; set; }
        public string MonedaAnio { get; set; }
        public string MonedaMes { get; set; }
        public double MonedaTrmValor { get; set; }
        public string MonedaTrmPeriodo { get; set; }
        public DateTime? MonedaFechaRegistro { get; set; }
        public string MonedaTrmUsuario { get; set; }
    }

    public class MonedaTRM_Create_Update
    {
        public int MonedaTrmId { get; set; }
        public int MonedaId { get; set; }
        public decimal MonedaTrmValor { get; set; }
        public int MonedaAnio { get; set; }
        public int MonedaMes { get; set; }
    }
    
    public class Moneda_DTO
    {
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public bool Activo { get; set; }

        public string Simbolo { get; set; }

        public string ERP { get; set; }

        public string SeparadorDecimal { get; set; }

        public string SeparadorMiles { get; set; }

        public int? CantidadDecimales { get; set; }
    }

    public class Moneda_Consulta
    {
        public int MonedaId { get; set; }
        public string MonedaDescripcion { get; set; }
    }

    public class Mes_Consulta
    {
        public int MesId { get; set; }
        public string MesNombre { get; set; }
        public int MesTrim { get; set; }
        public string MesPeriodo { get; set; }
    }

    public class Anio_Consulta
    {
        public int Anio { get; set; }
        public int PlanActual { get; set; }
    }
    public class lista_planTecnico
    {
        public int Fup { get; set; }
        public string Tecnico { get; set; }
        public string ServicioCp { get; set; }
        public string FechaLLegaTecnicoObra { get; set; }
        public string FechaFinTecnicoObra { get; set; }
        public int? DiasTotal { get; set; }
    }
    public class SolicitudCartaManual_Consulta
    {
        public DateTime? FecSolicitud { get; set; }
        public string UsuarioSolicitud { get; set; }
        public DateTime? FecCancela { get; set; }
        public string UsuarioCancel { get; set; }
        public DateTime? FecAprobacion { get; set; }
        public string UsuarioAprobacion { get; set; }
        public DateTime? FecNegacion { get; set; }
        public string UsuarioNegacion { get; set; }
    }
    public class DatosGeneralesFupPQRS
    {
        public string NroOrden { get; set; }
        public string FuenteDescripcion { get; set; }
        public string Detalle { get; set; }
        public string NombreRespuesta { get; set; }
        public string DireccionRespuesta { get; set; }
        public string EmailRespuesta { get; set; }
        public string TelefonoRespuesta { get; set; }
        public string FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public int? EstadoID { get; set; }
        public string EstadoDescripcion { get; set; }
        public bool? EsProcedente { get; set; }
        public int? IdFup { get; set; }
        public string IdOrden { get; set; }
        public string Version { get; set; }
        public int? TipoPQRSId { get; set; }
        public int? Id_Ofa { get; set; }
        public int? FupID { get; set; }
        public string OrdenVersion { get; set; }
        public string PaisNombre { get; set; }
        public int? PaisId { get; set; }
        public string CiudadNombre { get; set; }
        public int? CiudadId { get; set; }
        public string ClienteNombre { get; set; }
        public int? IdCliente { get; set; }
        public string Contacto { get; set; }
        public string ObraNombre { get; set; }
        public string DescripcionListados { get; set; }
        public string CorreosListado { get; set; }
        public string FechaCierre { get; set; }
        public string PlanAccion { get; set; }
        public string ClienteNombre2 { get; set; }
        public string OtroCliente { get; set; }
        public DateTime? FechaPlanAlum { get; set; }
        public DateTime? FechaReqAlum { get; set; }
        public DateTime? FechaPlanAcero { get; set; }
        public DateTime? FechaReqAcero { get; set; }
        public DateTime? FechaDespAcero { get; set; }
        public DateTime? FechaDespAlum { get; set; }
        public DateTime? FechaEntregaObra { get; set; }
        public string TipoPQRSDescripcion { get; set; }
        public string SubTipoPQRSDescripcion { get; set; }
        public string OrdenGarantiaOMejora { get; set; }
        public string DescripcionProcedencia { get; set; }
        public string IdOrdenProcedente { get; set; }
        public string OrdenProcedente { get; set; }
        public string DescripcionPlanAccion { get; set; }
        public string DescripcionPlanos { get; set; }
        public string Semaforo { get; set; }
        public bool IsInvolucred { get; set; }
        public int IdFuenteReclamo { get; set; }
        public bool PuedeSerCerrada { get; set; }
        public string DescripcionPlanoArmado { get; set; }
    }
    public class PQRSPosiblesEstados
    {
        public int? PQRSEstadosID { get; set; }
        public string Descripcion { get; set; }
        public string Color { get; set; }
        public string ColorClase { get; set; }
        public int OrdenEstado { get; set; }
    }
    public class PQRSProcesosAdmin
    {
        public int? PQRSProcesoAdminID { get; set; }
        public string Proceso { get; set; }
        public string EmailProceso { get; set; }
        public int? TipoPQRSId { get; set; }
        public string EmailProcesoCC { get; set; }
        public string Observacion { get; set; }
    }

    public class PQRSAsociadasAOrden
    {
        public int IdPQRS { get; set; }

        public string Detalle { get; set; }
    }
    public class PQRSAsignacionProcesos
    {
        public int? Id { get; set; }
        public string Proceso { get; set; }
        public string Email { get; set; }
        public string Observacion { get; set; }
        public bool IsNew { get; set; }
        public string EmailProcesoCC { get; set; }
    }

    public class PQRSArchivos
    {
        public int? IdArchivo { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public bool? CanBeDeleted { get; set; }
        public string UsuarioArchivo { get; set; }
    }

    public class PQRSBitacoraEventos
    {
        public int EventoId { get; set; }
        public string EventoDescripcion { get; set; }
        public string EventoColor { get; set; }
    }
    public class PQRSRespuestaProcesos : PQRSArchivos
    {
        public int? Id { get; set; }
        public string Email { get; set; }
        public int? IdProceso { get; set; }
        public bool? Aclaracion { get; set; }
        public string InformacionAclaracion { get; set; }
        public string UsuarioAclaracion { get; set; }
        public DateTime? FechaAclaracion { get; set; }
        public string Proceso { get; set; }
        public string Respuesta { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public int? IdPadre { get; set; }
        public List<PQRSArchivos> Archivos { get; set; }
        public List<PQRSRespuestaProcesos> hijos { get; set; }
    }
    public class PQRSNoConformidadesResumen
    {
        public int PQRSNoConformidadesID { get; set; }
        public string Proceso { get; set; }
        public string DescripcionNoConformidad { get; set; }
        public string Email { get; set; }
        public string Comentario { get; set; }
        public DateTime? PlanAccionFecha { get; set; }
        public string PlanAccion { get; set; }
        public string PlanAccionDescripcion { get; set; }
        public int? IdClasificacion { get; set; }
        public string Clasificacion { get; set; }
        public string UsuarioResponsable { get; set; }
        public int? IdFamiliaGarantia { get; set; }
        public string FamiliaGarantia { get; set; }
        public List<PQRSNoConformidadesProductosGarantia> ProductosGarantias { get; set; } =
            new List<PQRSNoConformidadesProductosGarantia>();
    }
    public class PQRSNoConformidadesProductosGarantia
    {
        public int NoConformidadId { get; set; }
        public string Nombre { get; set; }
        public string TextoOpcional { get; set; }
    }
    public class PQRSArchivosListados : PQRSArchivos
    {
        public string Mail { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }
    }
    public class PQRSComunicados : PQRSArchivos
    {
        public int IdPQRSComunicado { get; set; }
        public string MessageTo { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
        public DateTime Fecha { get; set; }
        public string Usuario { get; set; }
        public string MessageCc { get; set; }
        public int? SeEnvioEncuesta { get; set; }
        public List<PQRSArchivos> Archivos { get; set; }
    }
    public class PQRSFromOrdenBasicInfo
    {
        public int IdPQRS { get; set; }
        public string NroOrden { get; set; }
        public string Fuente { get; set; }
        public string Estado { get; set; }
        public string Detalle { get; set; }
        public string TipoPQRS { get; set; }
        public string Og_Om { get; set; }
        public string SolucionadoEnObra { get; set; }
    }
    public class PQRSFamiliasGarantias
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreEng { get; set; }
        public string NombrePor { get; set; }
        public List<PQRSProductosFamiliasGarantias> Productos { get; set; } = new List<PQRSProductosFamiliasGarantias>();
    }
    public class PQRSProductosFamiliasGarantias
    {
        public int Id { get; set; }
        public int IdFamilia { get; set; }
        public string Nombre { get; set; }
        public string NombreEng { get; set; }
        public string NombrePor { get; set; }
        public bool Selected { get; set; } = false;
    }
    public class PQRSClasificacionesPlanAccion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string NombreEng { get; set; }
        public string NombrePor { get; set; }
    }
    public class SimuladorProyecto_Resumen
    {
        public int Id_Ofa_P { get; set; }
        public string Orden { get; set; }
        public string Pais { get; set; }
        public string cli_nombre { get; set; }
        public int consecutivo { get; set; }
        public string tipoFormaleta { get; set; }
        public int GrupoPieza { get; set; }
        public int GrupoSimulador { get; set; }
        public int NivelAccesorios { get; set; }
        public string DesGrupoSimulador { get; set; }
        public int CantPiezas { get; set; }
        public double M2xItem { get; set; }
        public double PesoxItem { get; set; }
        public double PesoChxItem { get; set; }
        public double CostoxItem { get; set; }
        public double CostoMpxItem { get; set; }
        public double CostoChxItem { get; set; }
        public double InsertosxItem { get; set; }
        public double ValorCIF_Item { get; set; }
        public double ValorMOD_item { get; set; }
        public string monedaCotizacion { get; set; }
        public double tasaMonedaCotizacion { get; set; }
        public float tasaDolar { get; set; }
        public int tasaAno { get; set; }
        public int tasaMes { get; set; }
        public double Valor { get; set; }
        public double ValorConFlete { get; set; }
        public double valorConMargen { get; set; }
        public double ValorConImpuesto { get; set; }
        public double ValorMinimo { get; set; }
        public double ValorConMargenMinimo { get; set; }
        public double ValorConImpuestoMinimo { get; set; }
        public double porcImpuesto { get; set; }
        public double PorcMargen { get; set; }
        public double PorcMargenMinimo { get; set; }
        public double porcFlete { get; set; }
        public double ValorSugerido { get; set; }
        public double valorConMargenSugerido { get; set; }
        public double ValorConImpuestoSugerido { get; set; }
        public double PorcMargenSugerido { get; set; }
        public string FechaSimulacion { get; set; }
    }

    public class ListaAsistidaReferenciaConsulta
    {
        public int RefId { get; set; }
        public string RefCodigo { get; set; }
        public string RefDescripcion { get; set; }
        public int RefGrupoId { get; set; }
        public string RefGrupoDescripcion { get; set; }
        public int RequiereAncho1 { get; set; }
        public int RequiereAncho2 { get; set; }
        public int RequiereAlto1 { get; set; }
        public int RequiereAlto2 { get; set; }
        public int RequiereACCS { get; set; }
        public int RequiereESPAN { get; set; }
        public string RutaImagen { get; set; }
        public int RefEstado { get; set; }
        public string RefDescripcionCompuesta { get; set; }
        public string Formula { get; set; }
    }
    public class ItemsListaAsistida
    {
        public int ItemCantidad { get; set; }
        public int RefCodigoId { get; set; }
        public string RefCodigo { get; set; }
        public float ItemAncho1 { get; set; }
        public float ItemAlto1 { get; set; }
        public float ItemAncho2 { get; set; }
        public float ItemAlto2 { get; set; }
        public float ItemACCS { get; set; }
        public float ItemESPAN { get; set; }
        public string ItemDescAux { get; set; }
        public string RefGrupoDescripcion { get; set; }
        public float ItemM2 { get; set; }
        public float ItemTotalM2 { get; set; }
        public int PendienteGuardar { get; set; }
        public int? IdProducto { get; set; }
        public string Prefijo { get; set; }
        public int? IdPiezaForsa { get; set; }
    }
    public class ItemsListaAsistidaToExport
    {
        public int CANT { get; set; }
        public string REFERENCIA { get; set; }
        public float ANCHO { get; set; }
        public float ALTO { get; set; }
        public float ALTO2 { get; set; }
        public float ANCHO2 { get; set; }
        public string DESC_AUX { get; set; }
        public string FAMILIA { get; set; }
        public decimal AREA_ITEM_M2 { get; set; }
    }
    public class SimulacionEntradaCotizacion
    {
        public bool? eect_SimulacionListaAsistida { get; set; }
    }
    public class FleteNacional
    {
        public int Importar { get; set; }
        public int RegistroId { get; set; }
        public int TransportadorId { get; set; }
        public string TransportadorNombre { get; set; }
        public int CiudadOrigenId { get; set; }
        public string CiudadOrigenNombre { get; set; }
        public int CiudadDestinoId { get; set; }
        public string CiudadDestinoNombre { get; set; }
        public int VehiculoId { get; set; }
        public string VehiculoDescripcion { get; set; }
        public float ValorFlete { get; set; }
        public float ValorPrima { get; set; }
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string CreadoPor { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public string ActualizadoPor { get; set; }
    }
    public class Transportador
    {
        public int TransportadorId { get; set; }
        public string TransportadorNombre { get; set; }
    }
    public class TipoVehiculo
    {
        public int TipoVehiculoId { get; set; }
        public string TipoVehiculoNombre { get; set; }
    }
    public class CiudadesFletesNacionales
    {
        public int ciu_id { get; set; }
        public string ciudad { get; set; }
    }

    // USP_fup_SEL_Fletes_AgenteCarga
    public class FleteInternacional
    {
        public int Importar { get; set; }
        public int RegistroId { get; set; }
        public int AgenteCargaId { get; set; }
        public string AgenteCargaNombre { get; set; }
        public int PuertoOrigenId { get; set; }
        public string PuertoOrigenNombre { get; set; }
        public int PuertoDestinoId { get; set; }
        public string PuertoDestinoNombre { get; set; }
        public string PaisPuertoDestino { get; set; }
        public int TipoContenedorId { get; set; }
        public string TipoContenedorNombre { get; set; }
        public float DespachoAduanal { get; set; }
        public float GastosPuertoOrigen { get; set; }
        public float FleteInternacionalValor { get; set; }
        public float LeadTimeCIF { get; set; }
        public int Estado { get; set; }
    }

    // USP_fup_SEL_Contenedores_Fletes_AgenteCarga
    public class Contenedores_FleteInternacional
    {
        public int TipoContenedorId { get; set; }
        public string TipoContenedorNombre { get; set; }
    }

    // USP_fup_SEL_Puertos_Fletes_AgenteCarga
    public class Puertos_FleteInternacional
    {
        public int PuertoId { get; set; }
        public string PuertoDescripcion { get; set; }
    }

    // USP_fup_SEL_AgenteCarga_Flete_Internacional
    public class Agentes_FleteInternacional
    {
        public int AgenteId { get; set; }
        public string AgenteNombre { get; set; }
    }
}

public class ClasificaCli
{
    public int? IdCliente { get; set; }
    public string Clasificacion { get; set; }
    public string ClaseColor { get; set; }
}


public class PoliticaCot
{
    public int? IdNivelComplejidad { get; set; }
    public string NivelComplejidad { get; set; }
    public int? IdClasificacionCliente { get; set; }
    public string ClasificacionCliente { get; set; }
    public int? Horas { get; set; }
}


public class fup_diasTdn
{
    public int id { get; set; }
    public int PaisId { get; set; }
    public string Pais { get; set; }
    public int FOB { get; set; }
    public int CIP { get; set; }
    public int CFR { get; set; }
    public int CIF { get; set; }
    public int DAP { get; set; }
    public int DDP { get; set; }

}

public class PaisFup
{
    public int PaisId { get; set; }
}

public class SimuladorComparativo
{
    public int Id_Ofa_P { get; set; }
    public string Orden { get; set; }
    public string Pais { get; set; }
    public string cli_nombre { get; set; }
    public int consecutivo { get; set; }
    public string tipoFormaleta { get; set; }
    public int NivelAccesorios { get; set; }
    public string monedaCotizacion { get; set; }
    public float tasaMonedaCotizacion { get; set; }
    public float tasaDolar { get; set; }
    public int tasaAno { get; set; }
    public int tasaMes { get; set; }
    public int CantPiezas { get; set; }
    public float M2xItem { get; set; }
    public float PesoxItem { get; set; }
    public float PesoChxItem { get; set; }
    public float CostoxItem { get; set; }
    public float CostoMpxItem { get; set; }
    public float CostoChxItem { get; set; }
    public float InsertosxItem { get; set; }
    public float ValorCIF_Item { get; set; }
    public float ValorMOD_item { get; set; }
    public float Valor { get; set; }
    public DateTime FechaSimulacion { get; set; }
    public float USD { get; set; }
    public float CostoMP_Real { get; set; }
    public float ValorCIF_real { get; set; }
    public float ValorMOD_real { get; set; }
    public float Costo_real { get; set; }
    public float M2_real { get; set; }
    public float Peso_real { get; set; }
    public float Unidades_real { get; set; }
    public string Ordenes_real { get; set; }
    public float diference_tasa_dolar { get; set; }
    public float percentage_tasa_dolar { get; set; }
    public float diference_m2 { get; set; }
    public float percentage_m2 { get; set; }
    public float diference_piezas { get; set; }
    public float percentage_piezas { get; set; }
    public float diference_peso { get; set; }
    public float percentage_peso { get; set; }
    public float diference_costo { get; set; }
    public float percentage_costo { get; set; }
    public float diference_costo_kg { get; set; }
    public float percentage_costo_kg { get; set; }
    public float diference_piezas_m2 { get; set; }
    public float percentage_piezas_m2 { get; set; }
    public float diference_kg_m2 { get; set; }
    public float percentage_kg_m2 { get; set; }
    public float diference_costo_m2 { get; set; }
    public float percentage_costo_m2 { get; set; }
    public float diference_costo_mp { get; set; }
    public float percentage_costo_mp { get; set; }
    public float diference_costo_mp_kg { get; set; }
    public float percentage_costo_mp_kg { get; set; }
    public float diference_costo_mod { get; set; }
    public float percentage_costo_mod { get; set; }
    public float diference_costo_mod_kg { get; set; }
    public float percentage_costo_mod_kg { get; set; }
    public float diference_costo_cif { get; set; }
    public float percentage_costo_cif { get; set; }
    public float diference_costo_cif_kg { get; set; }
    public float percentage_costo_cif_kg { get; set; }
    public string FechaCargue { get; set; }
}

public class SimuladorComparativoModFin
{
    public int fup { get; set; }
    public string versid { get; set; }
    public string Pais { get; set; }
    public string cli_nombre { get; set; }
    public string tipoFormaleta { get; set; }
    public int NivelAccesorios { get; set; }
    public string monedaCotizacion { get; set; }
    public float tasaCotizacion { get; set; }
    public float tasaModFinal { get; set; }
    public int CantPiezas { get; set; }
    public float M2xItem { get; set; }
    public float PesoxItem { get; set; }
    public float PesoChxItem { get; set; }
    public float CostoxItem { get; set; }
    public float CostoMpxItem { get; set; }
    public float CostoChxItem { get; set; }
    public float InsertosxItem { get; set; }
    public float ValorCIF_Item { get; set; }
    public float ValorMOD_item { get; set; }
    public float Valor { get; set; }
    public DateTime FechaSimulacion { get; set; }
    public int CantPiezasMf { get; set; }
    public float M2xItemMf { get; set; }
    public float PesoxItemMf { get; set; }
    public float PesoChxItemMf { get; set; }
    public float CostoxItemMf { get; set; }
    public float CostoMpxItemMf { get; set; }
    public float CostoChxItemMf { get; set; }
    public float InsertosxItemMf { get; set; }
    public float ValorCIF_ItemMf { get; set; }
    public float ValorMOD_itemMf { get; set; }
    public float ValorMf { get; set; }
    public DateTime FechaModFinal { get; set; }
    public float diference_tasa_dolar { get; set; }
    public float percentage_tasa_dolar { get; set; }
    public float diference_m2 { get; set; }
    public float percentage_m2 { get; set; }
    public float diference_piezas { get; set; }
    public float percentage_piezas { get; set; }
    public float diference_peso { get; set; }
    public float percentage_peso { get; set; }
    public float diference_costo { get; set; }
    public float percentage_costo { get; set; }
    public float diference_costo_kg { get; set; }
    public float percentage_costo_kg { get; set; }
    public float diference_piezas_m2 { get; set; }
    public float percentage_piezas_m2 { get; set; }
    public float diference_kg_m2 { get; set; }
    public float percentage_kg_m2 { get; set; }
    public float diference_costo_m2 { get; set; }
    public float percentage_costo_m2 { get; set; }
    public float diference_costo_mp { get; set; }
    public float percentage_costo_mp { get; set; }
    public float diference_costo_mp_kg { get; set; }
    public float percentage_costo_mp_kg { get; set; }
    public float diference_costo_mod { get; set; }
    public float percentage_costo_mod { get; set; }
    public float diference_costo_mod_kg { get; set; }
    public float percentage_costo_mod_kg { get; set; }
    public float diference_costo_cif { get; set; }
    public float percentage_costo_cif { get; set; }
    public float diference_costo_cif_kg { get; set; }
    public float percentage_costo_cif_kg { get; set; }
}

public class StgCostosRealExcel
{
    public int? Anio { get; set; }

    public string Mes { get; set; }

    public string Tipo { get; set; }

    public string NoOrden { get; set; }

    public string Cliente { get; set; }

    public string Pais { get; set; }

    public string NacEx { get; set; }

    public double? M2Vend { get; set; }

    public double? PrecioM2USD { get; set; }

    public double? UndVend { get; set; }

    public double? KgVend { get; set; }

    public double? UndM2 { get; set; }

    public double? M2Cot { get; set; }

    public double? VrCot { get; set; }

    public double? USD { get; set; }

    public double? OtrosUSD { get; set; }

    public double? COP { get; set; }

    public double? MpAluminio { get; set; }

    public double? MpPlastico { get; set; }

    public double? Kanban { get; set; }

    public double? Stock { get; set; }

    public double? Consumible { get; set; }

    public double? MOD { get; set; }

    public double? CIF { get; set; }

    public double? TotalAluminio { get; set; }

    public double? Nivel1Almacen { get; set; }

    public double? Nivel2Almacen { get; set; }

    public double? Nivel3Almacen { get; set; }

    public double? Nivel4Almacen { get; set; }

    public double? Nivel5Almacen { get; set; }

    public double? ConsObra { get; set; }

    public double? TotalAccAlmacen { get; set; }

    public double? Nivel1Fabricados { get; set; }

    public double? Nivel2Fabricados { get; set; }

    public double? Nivel3Fabricados { get; set; }

    public double? Nivel4Fabricados { get; set; }

    public double? Nivel5Fabricados { get; set; }

    public double? TotalAccFabricados { get; set; }
    public double? Acero { get; set; }
    public double? CostoTotal { get; set; }
    public double? Porc { get; set; }
    public bool IsNew { get; set; }
}

public class recomendaccion
{
    public string IdRecompra { get; set; }
    public string Recompra { get; set; }
}

public class Cot_Rap_Items_Consulta
{
    public int Id { get; set; }
    public string CodItem { get; set; }
    public string Item { get; set; }
    public int CodGrupo { get; set; }
    public string Grupo { get; set; }
    public string Descripcion { get; set; }
}

public class Cot_Rap_Items_UPD_Ins
{
    public int? Id { get; set; }
    public string CodItem { get; set; }
    public string Item { get; set; }
    public int CodGrupo { get; set; }
    public string Grupo { get; set; }
    public string Descripcion { get; set; }
}

public class Cot_Rap_Precio_Consulta
{
    public int Id { get; set; }
    public int IdItemCotRapida { get; set; }
    public string ItemCotRapida { get; set; }
    public int? IdPais { get; set; }
    public string Pais { get; set; }
    public float Precio { get; set; }
}

public class Cot_Rap_Precio_UPD_INS
{
    public int? Id { get; set; }
    public int IdItemCotRapida { get; set; }
    public int? IdPais { get; set; }
    public float Precio { get; set; }
}

public class Cot_Rap_Factores_Armado_Consulta
{
    public int Id { get; set; }
    public int IdItemCotRapida { get; set; }
    public string ItemCotRapida { get; set; }
    public int IdPais { get; set; }
    public string Pais { get; set; }
    public int IdTipoVivienda { get; set; }
    public string TipoVivienda { get; set; }
    public float Factor { get; set; }
}

public class Cot_Rap_Factores_Armado_UPD_INS
{
    public int? Id { get; set; }
    public int IdItemCotRapida { get; set; }
    public int IdPais { get; set; }
    public int IdTipoVivienda { get; set; }
    public float Factor { get; set; }
}

public class Cot_Rap_Paises_Consulta
{
    public int Id { get; set; }

    public string Pais { get; set; }
}

public class Cot_Rap_Enc_Insertar
{
    public int IdFup { get; set; }
    public string VersionFup { get; set; }
    public int IdTipoObra { get; set; }
    public int IdTipoEscalera { get; set; }
    public int NroModulaciones { get; set; }
    public int NroCambios { get; set; }
    public float AreaM2 { get; set; }
}

public class Cot_Rap_Enc_Consultar
{
    public int IdTipoObra { get; set; }
    public string TipoObra { get; set; }
    public int IdTipoEscalera { get; set; }
    public string TipoEscalera { get; set; }
    public int NroModulaciones { get; set; }
    public int NroCambios { get; set; }
    public float AreaM2 { get; set; }
    public bool EntregadoCliente { get; set; }
    public DateTime FechaCreacion { get; set; }
    public int Id { get; set; }
}