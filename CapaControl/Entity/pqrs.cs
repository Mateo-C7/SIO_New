using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaControl.Entity
{
    public class PQRS
    {
    }


    public class PQRSDTO
    {
        public string NroOrden { get; set; }
        public int? IdFuenteReclamo { get; set; }

        public int? TipoPQRSId { get; set; }
        public string Detalle { get; set; }
        public string NombreRespuesta { get; set; }
        public string DireccionRespuesta { get; set; }
        public string EmailRespuesta { get; set; }
        public string TelefonoRespuesta { get; set; }
        public string UsuarioCreacion { get; set; }
        public List<PQRSFilesDTO> archivos { get; set; }
        public int IdPQRS { get; set; }
        public int? IdFup { get; set; }
        public int? IdOrden { get; set; }
        public string Version { get; set; }
        public string Colaborador { get; set; }
        public int? IdTipoFuenteReclamo { get; set; }

        public int? IdPais { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdCliente { get; set; }
        public string OtroCliente { get; set; }
        public int? TipoSubPQRSId { get; set; }
    }

    public class PQRSFilesDTO
    {
        public string base64 { get; set; }
        public string nameFile { get; set; }
        public string type { get; set; }
    }

    public class PQRSFuente
    {
        public int PQRSFuenteID { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }


    public class FUPResumen
    {
        public int IdFup { get; set; }
        public int IdOrden { get; set; }
        public string Version { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public string Cliente { get; set; }
        public string Contacto { get; set; }
        public string Obra { get; set; }
        public string NombreRespuesta { get; set; }
        public string DireccionRespuesta { get; set; }
        public string EmailRespuesta { get; set; }
        public string TelefonoRespuesta { get; set; }
    }

    public enum eEstadoPQRS
    {
        Radicada = 1
    }

    public class PQRSDTOHistorico
    {
        public int Id { get; set; }
        public int PQRS { get; set; }

        public int EstadoDespuesID { get; set; }
        public string EstadoAntes { get; set; }
        public string EstadoDespues { get; set; }
        public DateTime Fecha { get; set; }
        public string Usuario { get; set; }

        public string FechaFormat { get; set; }

    }

    public class PQRSDTOArchivo
    {
        public int Id { get; set; }
        public int PQRS { get; set; }
        public string FilePATH { get; set; }
        public string FileName { get; set; }
    }

    public class PQRSFiltros
    {
        public string nombre { get; set; }
        public string orden { get; set; }

        public int fuente { get; set; }

        public int TipoPQRSId { get; set; }

        public string idpqrs { get; set; }
        public int estado { get; set; }

        public DateTime desde { get; set; }

        public DateTime hasta { get; set; }
    }

    public class PQRSDTOConsulta
    {
        public string NroOrden { get; set; }
        public string Fuente { get; set; }
        public string Detalle { get; set; }
        public string NombreRespuesta { get; set; }
        public string DireccionRespuesta { get; set; }
        public string EmailRespuesta { get; set; }
        public string TelefonoRespuesta { get; set; }
        public string UsuarioCreacion { get; set; }
        public int IdPQRS { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int EstadoID { get; set; }
        public int Responde { get; set; }
        public string TipoPQRS { get; set; }
        public int TipoPQRSId { get; set; }
        public string OrdenProcedente { get; set; }
        public int? IdFup { get; set; }
        public string Version { get; set; }
        public int? IdOrden { get; set; }
        public string Cliente { get; set; }
	    public string Pais { get; set; }
        public string ColorEstado { get; set; }
        public int IdOrdenOrigen { get; set; }
        public string ColorClase { get; set; }
        public bool EsProcedente { get; set; }
        public string EsProcedenteDescripcion { get; set; }
        public string DescripcionProcedencia { get; set; }
        public int CantidadComunicados { get; set; }
        public int IdTipoFuente { get; set; }
        public int? IdFuenteReclamo { get; set; }
        public string Colaborador { get; set; }
        public string Semaforo { get; set; }
        public bool IsInvolucred { get; set; }
        public string TipoFuente { get; set; }
        public string OrdenGarantiaOMejora { get; set; }
        public int SeEnvioEncuesta { get; set; }
        public bool PuedeSerCerrada { get; set; }
    }

    public class Permiso
    {
        public bool VerTodos { get; set; }
        public bool SinPermiso { get; set; }
        public bool AsignaProceso { get; set; }
        public bool RepuestaProceso { get; set; }
        public bool ReclamoProcedente { get; set; }
        public bool OrdenGarantia { get; set; }
        public bool RespuestaCliente { get; set; }
        public bool Produccion { get; set; }
        public bool CargaListados { get; set; }
        public bool ImplementacionObra { get; set; }

    }


    public class PQRSFlujoAprobacion
    {
        public int? RolID { get; set; }
        public int? UsuarioID { get; set; }
        public int? EstadoAntesID { get; set; }
        public int? EstadoDespuesID { get; set; }
        public bool MostrarTodosEstados { get; set; }
    }

    public class PQRSTipo
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }

    public class PQRSProceso
    {
        public string Proceso { get; set; }
        public string EmailProceso { get; set; }
        public string EmailProcesoCC { get; set; }
        public int TipoNC { get; set; }
        public string Comentario { get; set; }
        public bool Seleccionado { get; set; }
    }

    public class PQRSPlanta
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }

    public class PQRSListadosPlanos {
        public int Id { get; set; }
        public int IdPQRS { get; set; }
        public string Correo { get; set; }
        public bool Completos { get; set; }
        public string Tipo { get; set; }
        public string TipoCargue { get; set; }
        public bool PuedeEditar { get; set; }
        public string Comentario { get; set; }
    }

    public class PQRSProcesoAsignado
    {
        public string Proceso { get; set; }
        public string EmailProceso { get; set; }
        public int PQRSProcesoId { get; set; }
        public string InformacionAclaracion { get; set; }
    }

    public class PQRSProcesoSave
    {
        public int PQRSId { get; set; }
        public List<PQRSProceso> procesos { get; set; }
    }

    public enum EstadosPQRS
    {
        Elaboracion = 0,
        Radicado = 1,
        Asignacion = 2,
        RespuestaProceso = 3,
        ReclamoProcedente = 4,
        Ingenieria = 5,
        RespuestaCliente = 6,
        CargarListado = 7,
        ImplementacionObra = 8,
        Final = 9,
        Produccion = 10,
        Archivada = 11,
        Despachado = 12,
        Anulada = 13,
        SolucionadoEnObra = 14,
        NoProcedente = 15,
        Embajale = 16
    }

    public enum TipoPQRS
    {
        Queja = 1,
        Reclamo = 2,
        Solicitud = 3,
        Sugerencia = 4,
        Felicitacion = 5,
        TalentoHumano = 6
    }

    public class PQRSRespuesta
    {
        public int PQRSId { get; set; }
        public int PQRSIdproceso { get; set; }
        public string Proceso { get; set; }
        public string Mensaje { get; set; }
        public string Usuario { get; set; }
        public List<PQRSFilesDTO> archivos { get; set; }
    }

    public class PQRSRespuestaHistorico
    {
        public int Id { get; set; }
        public string Mensaje { get; set; }
        public string Proceso { get; set; }
        public List<PQRSDTOArchivo> archivos { get; set; }
    }


    public class TipoNC
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Proc { get; set; }
        public string Comentario { get; set; }

    }

    public class pqrsProcedente
    {
        public int IdPQRS { get; set; }
        public bool EsProcedente { get; set; }
        public List<PQRSProceso> Procesos { get; set; }
        public string DescripcionNoProcedente { set; get; }
    }

    public class pqrsGenerarOrden
    {
        public int IdPQRS { get; set; }
        public bool ExisteOrden { get; set; }
        public bool solucionadoEnObra { get; set; }
        public string Idordenprocedente { get; set; }
        public bool Requierelistados { get; set; }
        public bool RequierelistadosAcero { get; set; }
        public bool Requiereplanos { get; set; }
        public bool Requierearmado { get; set; }
        public bool RequiereplanosAcero { get; set; }
        public bool RequierearmadoAcero { get; set; }
        public bool RequierearmadoAluminio { get; set; }
        public bool RequierelistadosAluminio { get; set; }
        public bool RequiereplanosAluminio { get; set; }
        public string RequierelistadosAceroCorreos { get; set; }
        public string RequierelistadosAluminioCorreos { get; set; }
        public string RequiereplanosAceroCorreos { get; set; }
        public string RequiereplanosAluminioCorreos { get; set; }
        public string RequierearmadoAceroCorreos { get; set; }
        public string RequierearmadoAluminioCorreos { get; set; }
        public string RequierelistadosDescripcion { get; set; }
        public string RequierePlanosDescripcion { get; set; }
        public string RequiereArmadoDescripcion { get; set; }
        public string OrdenGarantiaOMejora { get; set; }
        public int IdPlanta { get; set; }
    }

    public class PQRSNoConformidades
    {
        public string Proceso { get; set; }
        public string TipoNC { get; set; }
    }

    public class pqrsProcedenteHistorico
    {
        public int IdPQRS { get; set; }
        public bool EsProcedente { get; set; }
        public string Descripcion { get; set; }
        public int IdOrdenProcedente { get; set; }
        public string OrdenGarantiaOMejora { get; set; }
        public string SEsProcedente { get; set; }
        public string DescripcionOrden { get; set; }
        public List<PQRSNoConformidades> procesos { get; set; }
    }

    public class Comunicado
    {
        public int Id { get; set; }
        public string Cc { get; set; }
        public string De { get; set; }
        public string Para { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
        public List<PQRSFilesDTO> archivos { get; set; }
        public bool incluirEncuesta { get; set; }
        public List<EnviarArchivosComunicado> archivosRadicadoEnviar { get; set; }
        public List<EnviarArchivosComunicado> archivosComprobanteEnviar { get; set; }
    }

    public class EnviarArchivosComunicado
    {
        public int Id { get; set; }
        public bool Enviar { get; set; }
    }

    public class PQRSDTORDEN
    {
        public int IdOfa { get; set; }
        public string Orden { get; set; }
        public int? Fup { get; set; }
    }

    public class PQRSListadosRequeridos
    {
        public int IdPQRS { get; set; }
        public List<PQRSListadosPlanos> listadosPlanos { get; set; }
        public List<PQRSFilesDTO> archivos { get; set; }
        public string TipoCargueArchivos { get; set; }
        public int TablaCargueArchivos { get; set; }
    }
    public class PQRSImplementacionObra
    {
        public int IdPQRS { get; set; }
        public List<PQRSFilesDTO> archivos { get; set; }
    }

    public class PQRSPComprobante
    {
        public int IdPQRS { get; set; }
        public List<PQRSFilesDTO> archivos { get; set; }
    }

    public class PQRSProduccion
    {
        public int IdPQRS { get; set; }
        public DateTime fecha_plan_alum { get; set; }
        public DateTime fecha_plan_acero { get; set; }
        public DateTime fecha_req_alum { get; set; }
        public DateTime fecha_req_acero { get; set; }
        public DateTime fecha_desp_alum { get; set; }
        public DateTime fecha_desp_acero { get; set; }
        public DateTime fecha_ent_obra { get; set; }
        public List<PQRSFilesDTO> archivos { get; set; }
    }

    public class UsuarioRequiereListadoDTO
    {
        public string usuario { get; set; }
        public string mail { get; set; }
        public string valido { get; set; }
    }

    public class PQRSComunicado
    {
        public string MessageTo { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
        public int? SeEnvioEncuesta { get; set; }
    }

    public class PQRSProduccionHistorico
    {
        public string FechaPlanAlum { get; set; }
        public string FechaPlanAcero { get; set; }
        public string FechaReqAlum { get; set; }
        public string FechaReqAcero { get; set; }
        public string FechaDespAlum { get; set; }
        public string FechaDespAcero { get; set; }
        public string FechaEntObra { get; set; }
    }

    public class PQRSTipoFuente
    {
        public int PQRSTipoFuenteID { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }

    public class PQRSDetalleHistorico
     {
       public string Detalle { get; set; }
       public string NombreRespuesta { get; set; }
       public string UsuarioCreacion { get; set; }
       public string FechaCreacion { get; set; }                  
    }
}
