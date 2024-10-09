using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaControl.Entity
{

    // Clases para Costos de Materia Prima
    public class guardarcostomp
    {
        public int PlantaId { get; set; }
        public int TipoMpId { get; set; }
        public int OrigenId { get; set; }
        public DateTime FechaVigencia { get; set; }
        public decimal costo { get; set; }
        public decimal costo2 { get; set; }
        public decimal Kilos { get; set; }
        public decimal ValorLme { get; set; }
        public decimal ValorLme2 { get; set; }
        public string Observaciones { get; set; }
        public string Usuario { get; set; }
    }

    public class listacostomp : guardarcostomp
    {
        public string Planta { get; set; }
        public string Origen { get; set; }
        public string TipoMateria { get; set; }
        public bool Vigente { get; set; }
        public decimal CostoMP_Kilo { get; set; }
    }

    // Clases para Costos de CIF y MOD
    public class guardarcostocifmod
    {
        public int PlantaId { get; set; }
        public DateTime FechaVigencia { get; set; }
        public decimal CostoMOD { get; set; }
        public decimal CostoCIF { get; set; }
        public decimal pChatarra { get; set; }
        public decimal pChatarra240 { get; set; }
        public int TipoFormaletaId { get; set; }
        public string Usuario { get; set; }
    }

    public class listacostocifmod : guardarcostocifmod
    {
        public string Planta { get; set; }
        public string TipoFormaleta { get; set; }
    }

    // Clases para Items Cotizacion
    public class guardaritemcot
    {
        public int Id { get; set; }
        public int NivelId { get; set; }
        public string Grupo { get; set; }
        public string Grupo_3d { get; set; }
        public bool Habilitado_3d { get; set; }
    }

    public class listaitemcot : guardaritemcot
    {
        public string Item { get; set; }
        public string Nivel { get; set; }
    }

    // Clases para Margenes de Precios
    public class guardarmargen
    {
        public int NivelId { get; set; }
        public int GrupoPaId { get; set; }
        public int PaisId { get; set; }
        public decimal Margen { get; set; }
        public decimal MargenMinimo { get; set; }
    }

    public class listamargen : guardarmargen
    {
        public string Nivel { get; set; }
        public string GrupoPais { get; set; }
        public string Pais { get; set; }
    }

    // Clases para Margenes de Precios
    public class guardarprecio
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int GrupoPaId { get; set; }
        public int PaisId { get; set; }
        public int MonedaId { get; set; }
        public decimal Precio { get; set; }
        public int ClienteId { get; set; }
        public string Cliente { get; set; }
    }

    public class listaprecio : guardarprecio
    {
        public string ItemCotizacion { get; set; }
        public string GrupoPais { get; set; }
        public string Pais { get; set; }
        public string Moneda { get; set; }
    }

    public class listaTurnos
    {
        public int pct_id { get; set; }
        public DateTime pct_Fecha { get; set; }
        public int pct_CantTurnos { get; set; }
        public int pct_CantTurnosBr { get; set; }
        public int pct_SemanaMes { get; set; }
        public string pct_Mes { get; set; }
        public string pct_usuarioCrea { get; set; }
        public DateTime pct_fechaCrea { get; set; }
    }

    public class listaMetrosTurnos
    {
        public int pmt_id { get; set; }
        public int pmt_CantMetros { get; set; }
        public int pmt_CantMetrosBr { get; set; }
        public decimal pmt_porcMinimo { get; set; }
        public Boolean pmt_Habilitado { get; set; }
        public DateTime pmt_Fecha { get; set; }
        public string pmt_usuarioCrea { get; set; }
        public DateTime pmt_fechaCrea { get; set; }
    }


    public class listacliente 
    {
        public int ClienteId { get; set; }
        public string Cliente { get; set; }

    }

    public class Itinerariol
    {
        public int pil_id { get; set; }
        public int pil_anio  { get; set; }
        public string pil_puerto_origen { get; set; }
        public int pil_pais_id_detino { get; set; }
        public string pai_nombre { get; set; }
        public int pil_dia_cargue_forsa { get; set; }
        public int pil_dia_cargue_eta { get; set; }
        public int pil_total_transito { get; set; }
        public string pil_ruta { get; set; }
        public string pil_naviera { get; set; }
    }

}
