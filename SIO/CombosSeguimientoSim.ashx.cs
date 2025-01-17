﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using dhtmlxConnectors;
using System.Configuration;
using CapaDatos;

namespace SIO
{
    /// <summary>
    /// Descripción breve de CombosSeguimientoSim
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    public class CombosSeguimientoSim : dhtmlxRequestHandler
    {
        public override IdhtmlxConnector CreateConnector(HttpContext context)
        {
            string vTableName, vIdName, vDescName;
            // Por defecto se asume que es el combo de area
            vTableName = "SELECT  grpa_id, grpa_gp1_nombre  FROM  fup_grupo_pais   WHERE   (activo = 1)   ORDER BY grpa_gp1_nombre ";
            vIdName = "grpa_id";
            vDescName = "grpa_gp1_nombre";

            if (context.Request.QueryString["Tipo"] == "Ciu")
            {
                string pa_id = context.Request.QueryString["Pa_Id"];
                vTableName = "SELECT ciu_id, ciu_nombre FROM dbo.ciudad " +
                       "WHERE ciu_pai_id = '" + pa_id + "' ORDER BY ciu_nombre";
                vIdName = "ciu_id";
                vDescName = "ciu_nombre";
            }

            if (context.Request.QueryString["Tipo"] == "Pai")
            {
                vTableName = "SELECT pai_id, pai_nombre FROM pais ORDER BY pai_nombre ";
                vIdName = "pai_id";
                vDescName = "pai_nombre";
            }

            if (context.Request.QueryString["Tipo"] == "Veh")
            {
                vTableName = "SELECT tve_id, tve_descripcion FROM sie.tipo_vehiculo WHERE tve_uso_fup = 1 ORDER BY tve_orden_uso_fup ";
                vIdName = "tve_id";
                vDescName = "tve_descripcion";
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