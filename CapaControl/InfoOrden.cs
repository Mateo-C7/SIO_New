using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaControl
{
    public class InfoOrden
    {
        public InfoOrden()
        {}
        private String orden;
        public String Orden
        {
            get { return orden; }
            set { orden = value; }
        }

        private int idofa;
        public int Idofa
        {
            get { return idofa; }
            set { idofa = value; }
        }

        private String pais;
        public String Pais
        {
            get { return pais; }
            set { pais = value; }
        }

        private String tipoOf;
        public String TipoOf
        {
            get { return tipoOf; }
            set { tipoOf = value; }
        }

        private String ciudad;
        public String Ciudad
        {
            get { return ciudad; }
            set { ciudad = value; }
        }

        private String cliente;
        public String Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }

        private String despacho;
        public String Despacho
        {
            get { return despacho; }
            set { despacho = value; }
        }

        private int idDespacho;
        public int IdDespacho
        {
            get { return idDespacho; }
            set { idDespacho = value; }
        }
    }
}
