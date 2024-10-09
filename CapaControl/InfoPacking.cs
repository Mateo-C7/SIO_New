using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaControl
{
    public class InfoPacking
    {
        public InfoPacking()
        {}
        private String cliente;
        public String Cliente
        {
            get { return cliente; }
            set { cliente = value; }
        }

        private String direccion;
        public String Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        private String nit;
        public String Nit
        {
            get { return nit; }
            set { nit = value; }
        }

        private String telefono;
        public String Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        private String pais;
        public String Pais
        {
            get { return pais; }
            set { pais = value; }
        }

        private String tdn;
        public String Tdn
        {
            get { return tdn; }
            set { tdn = value; }
        }

        private String fecha;
        public String Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        private String puertoEmbarque;
        public String PuertoEmbarque
        {
            get { return puertoEmbarque; }
            set { puertoEmbarque = value; }
        }

        private String puertoDestino;
        public String PuertoDestino
        {
            get { return puertoDestino; }
            set { puertoDestino = value; }
        }

        private String encomendante;
        public String Encomendante
        {
            get { return encomendante; }
            set { encomendante = value; }
        }

        private String usuarioCreaDesp;
        public String UsuarioCreaDesp
        {
            get { return usuarioCreaDesp; }
            set { usuarioCreaDesp = value; }
        }

        private String factura;
        public String Factura
        {
            get { return factura; }
            set { factura = value; }
        }

        private String numContendor;
        public String NumContendor
        {
            get { return numContendor; }
            set { numContendor = value; }
        }

        private String precinto;
        public String Precinto
        {
            get { return precinto; }
            set { precinto = value; }
        }
    }
}
