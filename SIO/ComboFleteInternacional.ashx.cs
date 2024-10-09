using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using dhtmlxConnectors;
using System.Configuration;
using CapaDatos;

namespace GrillaExample
{
    /// <summary>
    /// Descripción breve de ComboFleteNacional
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    public class ComboFleteInternacional : dhtmlxRequestHandler
    {
        public override IdhtmlxConnector CreateConnector(HttpContext context)
        {
            string vTableName, vIdName, vDescName;
            // Por defecto se asume que es el combo de area
            vTableName = "SELECT DISTINCT pai_id, pai_nombre FROM sie.puerto p " +
                "INNER JOIN pais pa on pa.pai_id = p.pue_pais_id " +
                "WHERE pai_id <> 8 AND pue_tipo_puerto_id = 1 ORDER BY pai_nombre ";
            vIdName = "pai_id";
            vDescName = "pai_nombre";

            if (context.Request.QueryString["Tipo"] == "Dest")
            {
                string pa_id = context.Request.QueryString["Pa_Id"];
                vTableName = "SELECT pue_id, pue_descripcion FROM sie.puerto " +
                  "WHERE pue_tipo_puerto_id = 1 AND pue_pais_id = '" + pa_id + "' ORDER BY pue_descripcion";
                vIdName = "pue_id";
                vDescName = "pue_descripcion";
            }

            if (context.Request.QueryString["Tipo"] == "Orig")
            {
                vTableName = "SELECT pue_id, pue_descripcion FROM sie.puerto " +
                    "WHERE pue_tipo_puerto_id = 1  AND pue_pais_id = 8";
                vIdName = "pue_id";
                vDescName = "pue_descripcion";
            }

            if (context.Request.QueryString["Tipo"] == "Agente")
            {
                vTableName = "SELECT agec_id, agec_nombre FROM sie.agente_carga " +
                    "ORDER BY agec_nombre ";
                vIdName = "agec_id";
                vDescName = "agec_nombre";               
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