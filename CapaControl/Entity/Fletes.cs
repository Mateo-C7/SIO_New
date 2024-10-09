using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaControl.Entity
{
    public class puerto
    {
        public int pue_id  { get; set; }
        public string pue_descripcion { get; set; }
    }
    
    public class Calculoflete
    {
        public int transportador_id { get; set; }
        public int agente_carga_id { get; set; }
        public int leadTime { get; set; }
        public decimal vr_origen_t1 { get; set; }
        public decimal vr_origen_t2 { get; set; }
        public decimal vr_origen_t3 { get; set; }
        public decimal vr_origen_t4 { get; set; }
        public decimal vr_seguro { get; set; }
        public decimal vr_gastos_origen { get; set; }
        public decimal vr_aduana_origen { get; set; }
        public decimal vr_internacional_t1 { get; set; }
        public decimal vr_internacional_t2 { get; set; }
        public decimal vr_gastos_destino { get; set; }
        public decimal vr_aduana_destino { get; set; }
        public decimal vr_destino_t1 { get; set; }
        public decimal vr_destino_t2 { get; set; }
        public string transportador_texto { get; set; }
        public string agente_carga_texto { get; set; }
        public int NoValido { get; set; }
        public string MSG_Validacion { get; set; }
        public decimal vr_transint { get; set; }
        public decimal vr_internacional { get; set; }
        public decimal vr_transpaduanadest { get; set; }
        public decimal vr_flete { get; set; }
        public decimal vr_totalflete { get; set; }
        public int puerto_origen_id { get; set; }
        public int puerto_destino_id { get; set; }
        public int termino_negociacion_id { get; set; }
        public int cantidad_t1 { get; set; }
        public int cantidad_t2 { get; set; }
        public int cantidad_t3 { get; set; }
        public int cantidad_t4 { get; set; }
        public decimal vr_trm { get; set; }
        public decimal vr_impuestos { get; set; }

    }


    public class Guardarflete
    {
        public int transportador_id { get; set; }
        public int? agente_carga_id { get; set; }
        public int puerto_origen_id { get; set; }
        public int puerto_destino_id { get; set; }
        public int termino_negociacion_id { get; set; }
        public int leadTime { get; set; }
        public int cantidad_t1 { get; set; }
        public int cantidad_t2 { get; set; }
        public int cantidad_t3 { get; set; }
        public int cantidad_t4 { get; set; }
        public decimal vr_origen_t1 { get; set; }
        public decimal vr_origen_t2 { get; set; }
        public decimal vr_origen_t3 { get; set; }
        public decimal vr_origen_t4 { get; set; }
        public decimal vr_seguro { get; set; }
        public decimal vr_gastos_origen { get; set; }
        public decimal vr_aduana_origen { get; set; }
        public decimal vr_internacional_t1 { get; set; }
        public decimal vr_internacional_t2 { get; set; }
        public decimal vr_gastos_destino { get; set; }
        public decimal vr_aduana_destino { get; set; }
        public decimal vr_destino_t1 { get; set; }
        public decimal vr_destino_t2 { get; set; }
        public decimal vr_trm { get; set; }
        public decimal vr_impuestos { get; set; }
    }

}
