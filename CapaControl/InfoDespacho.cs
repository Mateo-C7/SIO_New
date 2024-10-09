using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaControl
{
    public class InfoDespacho
    {
        public InfoDespacho()
        { }
        private String orden;
        public String Orden
        {
            get { return orden; }
            set { orden = value; }
        }

        private String numDespacho;
        public String NumDespacho
        {
            get { return numDespacho; }
            set { numDespacho = value; }
        }

        private int idDespacho;
        public int IdDespacho
        {
            get { return idDespacho; }
            set { idDespacho = value; }
        }
    }
}
