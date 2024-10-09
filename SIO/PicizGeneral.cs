using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace SIO
{
    public class PicizGeneral
    {
        private static string _codItem;
        private static string _codBusq;
        private static string _codItGen;
        private static string _codEquiv;
        private static string _nomPag;
        private static decimal _cdMerc;
        private static decimal _cdSubP;
        private static string _cdTipo;
        private static string _cdUnCom;
        private static string _cdUnMed;

        private static int _posItem;
        private static DataTable _dt;
        private static DataTable _dtComp;
        private static DataTable _dtEquiv;
        private static DataTable _dtGrupo;
        private static System.String[] _arGrupo = null;
        private static string[] _nomItem = null;

        public static decimal CDMERCANCIA
        {
            get { return _cdMerc; }
            set { _cdMerc = value; }
        }

        public static decimal CDSUBPARTIDA
        {
            get { return _cdSubP; }
            set { _cdSubP = value; }
        }

        public static string CDTIPO
        {
            get { return _cdTipo; }
            set { _cdTipo = value; }
        }

        public static string CDUNDCOM
        {
            get { return _cdUnCom; }
            set { _cdUnCom = value; }
        }

        public static string CDUNDMED
        {
            get { return _cdUnMed; }
            set { _cdUnMed = value; }
        }

        public static string CodItem
        {
            get { return _codItem; }
            set { _codItem = value; }
        }

        public static string CodBusqueda
        {
            get { return _codBusq; }
            set { _codBusq = value; }
        }

        public static string CodItemGen
        {
            get { return _codItGen; }
            set { _codItGen = value; }
        }

        public static int PosicItem
        {
            get { return _posItem; }
            set { _posItem = value; }
        }

        public static string CodEquivalente
        {
            get { return _codEquiv; }
            set { _codEquiv = value; }
        }

        public static DataTable itemDatos
        {
            get { return _dt; }
            set { _dt = value; }
        }

        public static DataTable CompoDatos
        {
            get { return _dtComp; }
            set { _dtComp = value; }
        }

        public static DataTable EquivDatos
        {
            get { return _dtEquiv; }
            set { _dtEquiv = value; }
        }

        public static string PaginaActual
        {
            get { return _nomPag; }
            set { _nomPag = value; }
        }

        public static DataTable DatosGrupos
        {
            get { return _dtGrupo; }
            set { _dtGrupo = value; }
        }

        public static string[] ItemEnGrupo
        {
            get { return _nomItem; }
            set { _nomItem = value; }
        }  
    }
}