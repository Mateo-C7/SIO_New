using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CapaControl
{
    public class InfoBaliza
    {
        public InfoBaliza() { }

        private String balTipoVal;
        public String BalTipoVal
        {
            get { return balTipoVal; }
            set { balTipoVal = value; }
        }


        private int balId;
        public int BalId
        {
            get { return balId; }
            set { balId = value; }
        }

        private int balNo;
        public int BalNo
        {
            get { return balNo; }
            set { balNo = value; }
        }

        private int balOfaId;
        public int BalOfaId
        {
            get { return balOfaId; }
            set { balOfaId = value; }
        }

        private String balTSol;
        public String BalTSol
        {
            get { return balTSol; }
            set { balTSol = value; }
        }

        private String balNoSol;
        public String BalNoSol
        {
            get { return balNoSol; }
            set { balNoSol = value; }
        }

        private String balEstV;
        public String BalEstV
        {
            get { return balEstV; }
            set { balEstV = value; }
        }

        private String balEstNV;
        public String BalEstNV
        {
            get { return balEstNV; }
            set { balEstNV = value; }
        }

        private String balActiva;
        public String BalActiva
        {
            get { return balActiva; }
            set { balActiva = value; }
        }

        private String balRegion;
        public String BalRegion
        {
            get { return balRegion; }
            set { balRegion = value; }
        }

        private String balCambio;
        public String BalCambio
        {
            get { return balCambio; }
            set { balCambio = value; }
        }

        private int balEstValId;
        public int BalEstValId
        {
            get { return balEstValId; }
            set { balEstValId = value; }
        }

        private int balEstNoValId;
        public int BalEstNoValId
        {
            get { return balEstNoValId; }
            set { balEstNoValId = value; }
        }

    }
}
