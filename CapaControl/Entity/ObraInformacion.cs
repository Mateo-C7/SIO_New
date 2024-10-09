using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaControl.Entity
{
    public class ObraInformacion
    {
        public int IdObra { get; set; }
        public int IdPais { get; set; }
        public int IdCiudad { get; set; }
        public string NombreCiudad { get; set; }
        public int IdEstratoSocioEconomico { get; set; }
        public string ObraTipoVivienda { get; set; }
        public int TotalUnidades { get; set; }
        public decimal M2Vivienda { get; set; }
        public int TotalUnidadesForsa { get; set; }     
        public string TipoViviendaDominioDescripcion { get; set; }
        public string url { get; set; }
        public string FechaInicio { get; set; }
    }
}
