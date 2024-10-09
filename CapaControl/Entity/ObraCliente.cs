using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaControl.Entity
{
    public class ObraCliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdCliente { get; set; }
        public int Seguimiento { get; set; }
    }
}
