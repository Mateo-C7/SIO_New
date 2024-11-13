using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaControl.Entity
{
    public class Pais
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdMoneda { get; set; }
        public int UsaImperial { get; set; }
    }
}
