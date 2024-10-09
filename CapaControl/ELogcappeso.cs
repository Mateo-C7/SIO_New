using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaControl
{
    public class ELogcappeso
    {
        public ELogcappeso()
        {}
        public int tamano;
        public int tipo;
        public int llave;
        public String validar;
        public String tabla;
        public String strCarga;

        public int getTamano()
        {
            return this.tamano;
        }

        public void setTamano(int tamano)
        {
            this.tamano = tamano;
        }

        public int setTipo()
        {
            return this.tipo;
        }

        public void setTipo(int tipo)
        {
            this.tipo = tipo;
        }

        public int getLlave()
        {
            return this.llave;
        }

        public void setLlave(int llave)
        {
            this.llave = llave;
        }

        public String getValidar()
        {
            return this.validar;
        }

        public void setValidar(String validar)
        {
            this.validar = validar;
        }

        public String getTabla()
        {
            return this.tabla;
        }

        public void setTabla(String tabla)
        {
            this.tabla = tabla;
        }
    }
}
