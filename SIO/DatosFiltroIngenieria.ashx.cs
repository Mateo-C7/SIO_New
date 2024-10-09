using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using dhtmlxConnectors;
using System.Configuration;
using CapaDatos;

namespace dhtmlxConnector.Net_Samples.Combo

{
//        / <summary>
//        / Connector body
//        / </summary>
        [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ComboFiltroIngenieria : dhtmlxRequestHandler
    {
        public override IdhtmlxConnector CreateConnector(HttpContext context)
        {

            string vTableName, vIdName, vDescName;
            //Por defecto se asume que es el combo de estado
            //load the operation status
            vTableName = "SELECT  estoper_id, estoper_descripcion, estoper_activo FROM Inge_Estatus_Operacion WHERE estoper_activo=1 ORDER BY estoper_orden ";
            vIdName = "estoper_id";
            vDescName = "estoper_descripcion";

            if (context.Request.QueryString["Tipo"] == "TipoObs")
            {
                vTableName = "SELECT  asot_id, asot_descripcion  FROM  fup_actaseg_observac_tipo   WHERE  (asot_activo = 1)  AND (asot_id=9) ";
                vIdName = "asot_id";
                vDescName = "asot_descripcion";
            }

            if (context.Request.QueryString["Tipo"] == "ProbCi")
            {
                vTableName = "SELECT   aspc_id , aspc_descripcion FROM       fup_actaseg_probCierre  WHERE        (activo = 1)";
                vIdName = "aspc_id";
                vDescName = "aspc_descripcion";
            }

            if (context.Request.QueryString["Tipo"] == "Estatus")
            {
                vTableName = "SELECT      eacts_id,  eacts_descripcion  FROM            fup_actaseg_estado";       
                vIdName = "eacts_id";
                vDescName = "eacts_descripcion";
            }

            if (context.Request.QueryString["Tipo"] == "Pais")
            {
                vTableName = "select pai_id, pai_nombre  from pais order by pai_nombre ASC";
                vIdName = "pai_id";
                vDescName = "pai_nombre";
            }

            if (context.Request.QueryString["Tipo"] == "Ciudad")
            {
                string pai_id = context.Request.QueryString["pai_id"];
                vTableName = "SELECT ciu_id, ciu_nombre FROM CIUDAD WHERE ciu_pai_id= " + pai_id + " ORDER BY  ciu_nombre ASC";
                vIdName = "ciu_id";
                vDescName = "ciu_nombre";
            }
            if (context.Request.QueryString["Tipo"] == "Planta")
            {
                vTableName = "SELECT planta_id, planta_descripcion FROM planta_forsa WHERE planta_activo = 1";
                vIdName = "planta_id";
                vDescName = "planta_descripcion";
            }
         
            if (context.Request.QueryString["Tipo"] == "Material")
            {
                vTableName = "SELECT fup_tipo_venta_proy_id, descripcion FROM fup_tipo_venta_proyecto " +
                            " WHERE fup_tipo_venta_proyecto.activo = 1 AND fup_tipo_venta_proy_id <> 0";
                vIdName = "fup_tipo_venta_proy_id";
                vDescName = "descripcion";
            }


           


            dhtmlxComboConnector connector = new dhtmlxComboConnector(
                vTableName,
                vIdName,
                dhtmlxDatabaseAdapterType.SqlServer2005,
                BdDatos.conexionScope(),
                vDescName
            );

            if (context.Request.QueryString["dynamic"] != null)
                connector.SetDynamicLoading(2);
            return connector;
        }
    }
}
