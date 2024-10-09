using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaControl.Entity
{
    public class Plandia
    {
        public string Mes { get; set; }
        public int SemanaMes { get; set; }
        public string DiaSemana { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Metro { get; set; }
        public decimal MetrosMin { get; set; }
        public int? ppf_id { get; set; }
        public int? EntradaCotId { get; set; }
        public int? Idofa { get; set; }
        public int? Fup { get; set; }
        public string Vers { get; set; }
        public string ofa { get; set; }
        public string TipoCotizacion { get; set; }
        public string TipoProyecto { get; set; }
        public string Cliente { get; set; }
        public string Obra { get; set; }
        public decimal? m2Cerrados { get; set; }
        public decimal? M2Producir { get; set; }
        public int qdias { get; set; }
        public string Color { get; set; }
        public int Validacion { get; set; }
        public int Prioridad { get; set; }
        public DateTime? ComFechaEntregaCliente { get; set; }
        public DateTime? FecPlanDespacho { get; set; }
        public DateTime? IngFechaIniModulacion { get; set; }
        public DateTime? IngFechaFinalEntrega { get; set; }
        public decimal? M2Sf { get; set; }
        public decimal? IngM2Modulados { get; set; }
        public decimal? IngPiezas { get; set; }
        public decimal? ProdM2Produccion { get; set; }
        public decimal? ProdPiezas { get; set; }
        public decimal? ProdM2Emba { get; set; }
        public decimal? ProdPiezasEmba { get; set; }
        public DateTime? ProdFechaIni { get; set; }
        public DateTime? ProdFechaUlt { get; set; }
        public string Escalera_Desc { get; set; }
        public string NivelComplejidad { get; set; }
        public string CostaSisSeguridad { get; set; }
        public string DescSisSeguridad { get; set; }
        public string PaisObra { get; set; }
        public string CerradoIngenieria { get; set; }
        public string EstadoActa { get; set; }
        //public decimal? IngDias { get; set; }
        public decimal? PorcInge { get; set; }
        public decimal? PendIeng { get; set; }
        public decimal? PorcProdTer { get; set; }
        public decimal? PendProdTer { get; set; }
        public decimal? MixPiezasInge { get; set; }
        public decimal? MixPiezasPter { get; set; }
        public decimal? MixPiezasEmba { get; set; }
        public decimal? PendEmba { get; set; }
        public decimal? PorcOrden { get; set; }
        public DateTime? FechaValidacion { get; set; }
        public int? Curado { get; set; }
        public int? MarcaQR { get; set; }

    }

    public class PlanResumen
    {
        public string Grupo { get; set; }
        public int? Proyectos { get; set; }
        public decimal? M2Sf { get; set; }
        public decimal? PorcTotal { get; set; }
    }

    public class PlanOrden
    {
        public int IdOfa { get; set; }
        public string Orden { get; set; }
        public string TipoCotizacion { get; set; }
        public string TipoProyecto { get; set; }
        public decimal? M2Cerrados { get; set; }
        public decimal? M2Sf { get; set; }
        public string Proyectos { get; set; }

    }

}
