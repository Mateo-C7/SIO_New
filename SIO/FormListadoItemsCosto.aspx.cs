using CapaControl;
using CapaControl.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Net;
using System.Web.UI.WebControls;

namespace SIO
{
    public partial class FormListadoItemsCosto : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scripManager = ScriptManager.GetCurrent(this.Page);
            scripManager.EnablePageMethods = true;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            
        }

       
        [WebMethod]
        public static string ObtenerLineasDinamicas(string pOrden)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pIdOfa", pOrden);

            List<ItemCosto> data = ControlDatos.EjecutarStoreProcedureConParametros<ItemCosto>("USP_SimuladorCosto_SEL_Items", parametros);

            string response = JsonConvert.SerializeObject(data);
            return response;
        }

        [WebMethod]
        public static string CargueRayasOrden(string pOrden)
        {
            string response = string.Empty;
            Dictionary<string, object> parametros = new Dictionary<string, object>() { { "@IdOfa", pOrden } };

            List<datosCombo2> lstOrden = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT distinct id_of_p id, convert(varchar(20),numero) +'-' +ano  descripcion
													FROM  orden where id_of_p = @IdOfa ", parametros);

            List<datosCombo2> lstRayas = ControlDatos.EjecutarConsulta<datosCombo2>(@"SELECT Id_Ofa id, letra descripcion
													FROM  orden where id_of_p = @IdOfa ORDER BY letra", parametros);


            var query = new
            {
                Orden = lstOrden.FirstOrDefault(),
                Rayas = lstRayas
            };

            response = JsonConvert.SerializeObject(query);

            return response;
        }

        [WebMethod]
        public static string CargueItemsCosto(string pOrden)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("@pIdOfa", pOrden);

            ControlDatos.GuardarStoreProcedureConParametros("USP_SimuladorCosto_CargueItems", parametros);            
            return "OK";
        }


        [WebMethod]
        public static string EnviarWebService(string[] items, string pOrden, int pDestino = 2)
        {
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            List<LineasWs> Lineas;
            string importar;
            string mensaje = "";
            //Se genera la fecha en formato de 8 char
            DateTime thisDate1 = DateTime.Now;
            string fecha = thisDate1.ToString("yyyyMMdd");
            string cia = "6";
            string stMsgErp = "";
            string stMsg = "";
            short x = (short)0;
            System.Data.DataSet nodes = null;
            Boolean Existoso = true;
            string CxERP = "FORSA_PRUEBAS";

            if (pDestino == 1) // ERP de Produccion
                CxERP = "FORSA";

            foreach (string item in items)
            {
                mensaje += "Item: " + item + "<br>";
                for (int i = 1; i < 4; i++)
                {
                    parametros.Clear();
                    parametros.Add("@pItem", item);
                    parametros.Add("@pTipoProceso", i);
                    parametros.Add("@pIdOfa", pOrden);
                    parametros.Add("@pDestino", pDestino);

                    Lineas = ControlDatos.EjecutarStoreProcedureConParametros<LineasWs>("USP_SimuladorPlanoErp", parametros);
                    if(Lineas.Count == 0)
                    {
                        mensaje += "Nada que borrar\n";
                        mensaje +=  "\r\n Proceso : " + i.ToString() + "\r\n<br>";
                        continue;
                    }
                    importar = "<?xml version='1.0' encoding='utf-8'?>"
                          + "<Importar>"
                          + "<NombreConexion>"+ CxERP + "</NombreConexion>"
                          + "<IdCia>" + cia + "</IdCia>"
                          + "<Usuario>siif</Usuario>"
                          + "<Clave>SiifErp</Clave>"
                          + "<Datos>"
                          + Lineas.FirstOrDefault().Linea
                          + "</Datos>"
                          + "</Importar>";
                    System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    com.siesacloud.wsforsa.WSUNOEE WSDL_PROD = new com.siesacloud.wsforsa.WSUNOEE();
                    com.siesacloud.wsforsapru.WSUNOEE WSDL = new com.siesacloud.wsforsapru.WSUNOEE();
                    try
                    {
                        if (pDestino == 1) // ERP de Produccion
                            nodes = WSDL_PROD.ImportarXML(importar, ref x);
                        else
                            nodes = WSDL.ImportarXML(importar, ref x);
                    }
                    catch (System.Net.WebException e)
                    {
                        mensaje += "Excepcion: " + e.Message + " Proceso: " + i.ToString() + "\r\n<br>";
                    }
                    //"Resultado del envio al web service  de item es:" + x;
                    if (x == 0)
                    {
                        //Encontrar el último consecutivo generado, se omite esta vez por petición de Fer
                        //De tal forma que el mensaje resultante sea más informativo
                        /* mensaje = mensaje + "\r\n Proceso : " + i.ToString() + " OK "; */

                    }
                    else
                    {
                        Existoso = false;
                        //Recuperar Mensaje del WS
                        if (nodes != null)
                        {
                            foreach (DataRow fila in nodes.Tables[0].Rows)
                            {
                                stMsgErp = stMsgErp + " - Linea " + fila[0].ToString() + " * " + fila[5].ToString() + " " + fila[6].ToString(); // + Environment.NewLine;
                            }
                        }
                        mensaje += "\r\n Proceso : " + i.ToString() + " " + stMsg + " " + stMsgErp + "<br>";
                    }

                }
                if (Existoso)
                {
                    parametros.Clear();
                    parametros.Add("@pItem", item);

                    Lineas = ControlDatos.EjecutarStoreProcedureConParametros<LineasWs>("USP_SimuladorActualizaEnvioErp", parametros);
                }
            }
            return mensaje;
        }

        }

}