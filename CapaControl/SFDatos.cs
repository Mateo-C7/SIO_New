using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaControl
{
    class SFDatos
    {
    }

    public class SFEncabezado
    {
        private string sFId;
        public string SFId
        {
            get { return sFId; }
            set { sFId = value; }
        }
        public string Consecutivo { get; set; }
        public string TipoReg { get; set; }
        public string SubTipoReg { get; set; }
        public string VersionReg { get; set; }
        public string Compania { get; set; }
        public int LiquidaImpuesto { get; set; }
        public int ConsecutivoAutoreg { get; set; }
        public int IndicadorContacto { get; set; }
        public string CentroOperacionDocumento { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string FechaDocumento { get; set; }
        public string ClaseInternaDocumento { get; set; }
        public int EstadoDocumento { get; set; }
        public int BackorderDocumento { get; set; }
        public string TerceroFacturar { get; set; }
        public string SucursalFacturar { get; set; }
        public string TerceroDespachar { get; set; }
        public string SucursalDespachar { get; set; }
        public string TipoTercero { get; set; }
        public string CentroOperacionFactura { get; set; }
        public string FechaEntregaPedido { get; set; }
        public string DiasEntregaPedido { get; set; }
        public string OrdenCompraDocumento { get; set; }
        public string ReferenciaDocumento { get; set; }
        public string CodigoCargueDocumento { get; set; }
        public string MonedaDocumento { get; set; }
        public string MonedaConversion { get; set; }
        public string TasaConversion { get; set; }
        public string MonedaLocal { get; set; }
        public string TasaLocal { get; set; }
        public string CondicionPago { get; set; }
        public int EstadoImpresion { get; set; }
        public string Observaciones { get; set; }
        public string ClientedeContado { get; set; }
        public string PuntodeEnvio { get; set; }
        public string VendedorPedido { get; set; }
        public string Contacto { get; set; }
        public string Direccion1 { get; set; }
        public string Direccion2 { get; set; }
        public string Direccion3 { get; set; }
        public string Pais { get; set; }
        public string DepEstado { get; set; }
        public string Ciudad { get; set; }
        public string Barrio { get; set; }
        public string Telefono { get; set; }
        public string Fax { get; set; }
        public string CodigoPostal { get; set; }
        public string email { get; set; }
        public int Descuento { get; set; }
    }

    public class SFDetalle
    {
        private string sFId;
        public string SFId
        {
            get { return sFId; }
            set { sFId = value; }
        }
        public string Consecutivo { get; set; }
        public string TipoReg { get; set; }
        public string SubTipoReg { get; set; }
        public string VersionReg { get; set; }
        public string Compania { get; set; }
        public string CentroOperacion { get; set; }
        public string TipoDocumento { get; set; }
        public string ConsecDocumento { get; set; }
        public string NumeroReg { get; set; }
        public string IdItem { get; set; }
        public string ReferenciaItem { get; set; }
        public string CodigoBarras { get; set; }
        public string Extension1 { get; set; }
        public string Extension2 { get; set; }
        public string Bodega { get; set; }
        public string Concepto { get; set; }
        public string Motivo { get; set; }
        public int IndicadorObsequio { get; set; }
        public string CentroOperacionMovimiento { get; set; }
        public string UnidadNegocioMovimiento { get; set; }
        public string CentroCostoMovimiento { get; set; }
        public string Proyecto { get; set; }
        public string FechaEntregaPedido { get; set; }
        public string DiasEntregaPedido { get; set; }
        public string ListaPrecio { get; set; }
        public string UnidadMedida { get; set; }
        public string CantidadBase { get; set; }
        public string CantidadAdicional { get; set; }
        public string PrecioUnitario { get; set; }
        public string Impuestos { get; set; }
        public string Notas { get; set; }
        public string Detalle { get; set; }
        public int Backorder { get; set; }
        public int IndicadorPrecio { get; set; }
    }
}
