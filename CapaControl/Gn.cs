using System;
using System.Xml;
using System.IO;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDatos;
using System.Configuration;


namespace CapaControl
{
    public class Gn
    {
        BdDatos bddata = new BdDatos();
        private BdDatos Bdata = new BdDatos();
        /***********************************************
         * Retorna Valor de Configuracion
        ***********************************************/
        public static string GetConf(string stLlave)
        {
            return ConfigurationManager.AppSettings[stLlave].ToString();
        }

        /***********************************************
        * Manejo de Excepcion         
       ***********************************************/
        public bool proErrorException(string stModulo, Exception e, string sqlErr = "")
        {
            String stMes;
            string idOper;
            idOper = "SIO";
            stMes = e.Message.Replace("'", "");
            Bdata.RegistraExcepcion(idOper, stModulo, stMes, sqlErr);
            return false;
        }
    }
}
