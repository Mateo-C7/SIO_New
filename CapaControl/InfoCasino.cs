using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaControl
{
    public class InfoCasino
    {
        private int cedulaEmp;
        public int CedulaEmp
        {
            get { return cedulaEmp; }
            set { cedulaEmp = value; }
        }


        private int codArea;
        public int CodArea
        {
            get { return codArea; }
            set { codArea = value; }
        }


        private String nomEmp;
        public String NomEmp
        {
            get { return nomEmp; }
            set { nomEmp = value; }
        }


        private String nomArea;
        public String NomArea
        {
            get { return nomArea; }
            set { nomArea = value; }
        }


        private int centroCosto;
        public int CentroCosto
        {
            get { return centroCosto; }
            set { centroCosto = value; }
        }

    }
}
