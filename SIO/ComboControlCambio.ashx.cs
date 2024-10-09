using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using dhtmlxConnectors;
using System.Configuration;

namespace GrillaExample
{
    /// <summary>
    /// Descripción breve de ComboControlCambio
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    public class ComboControlCambio : dhtmlxRequestHandler
    { 
        public override IdhtmlxConnector CreateConnector(HttpContext context)
        {
            string vTableName, vIdName, vDescName;
            // Por defecto se asume que es el combo de area
            vTableName = "SELECT are_id, are_nombre FROM area WHERE are_ControlCambio = 1";
            vIdName = "are_id";
            vDescName = "are_nombre";

            if (context.Request.QueryString["Tipo"] == "Emp")
            {
                string areid = context.Request.QueryString["Are_Id"];
                vTableName = "SELECT emp_usu_num_id, emp_nombre + ISNULL(' ' + emp_apellidos,'') AS Nombre " +
                    "FROM empleado WHERE emp_area_id = " + areid + "";
                vIdName = "emp_usu_num_id";
                vDescName = "Nombre";
            }          

            dhtmlxComboConnector connector = new dhtmlxComboConnector(
                vTableName,
                vIdName,
                dhtmlxDatabaseAdapterType.SqlServer2005,
                "data source=172.21.0.38;persist security info=False;initial catalog=Forsa; user id=forsa; password=forsa2006",
                vDescName
            );

            if (context.Request.QueryString["dynamic"] != null)
                connector.SetDynamicLoading(2);
            return connector;
        }    
    }
}