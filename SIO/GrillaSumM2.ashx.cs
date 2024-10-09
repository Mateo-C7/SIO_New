using System;
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
    /// Descripción breve de GrillaSumM2
    /// </summary>

    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    public class GrillaSumM2 : dhtmlxRequestHandler
    {
        public override IdhtmlxConnector CreateConnector(HttpContext context)
        {
            string vTrm = "", usu = "";

            if (context.Request.QueryString["TRM"] != null)
                vTrm = context.Request.QueryString["TRM"];

            if (context.Request.QueryString["Usu"] != null)
                usu = context.Request.QueryString["Usu"];

            dhtmlxGridConnector connector = new dhtmlxGridConnector(
               string.Format("EXECUTE USP_fup_SEL_ActSeg_Resumen_fact '" + vTrm + "','" + usu + "'"),
               "ProbabilidadCierre",
               dhtmlxDatabaseAdapterType.SqlServer2005,
               BdDatos.conexionScope()
            );

            //Configuración de la grilla 
            dhtmlxGridConfiguration config = new dhtmlxGridConfiguration();
            config.SetHeader("Probabilidad, M2 MesActual, Vr MesActual, M2 2doMes, Vr 2doMes, M2 3erMes, Vr 3erMes");
            config.SetInitWidths("80,80,80,80,80,80,80");
            config.SetColTypes("rotxt,ron,ron,ron,ron,ron,ron");
            config.SetColAlign("str,str,str,str,str,str,str");
            connector.SetConfig(config);
            
            //to perform create/update/delete operations you need to define custom handlers
            //and parse request data for parameters
            var form = context.Request.Form;
            if (form["ids"] != null)
            {
                string[] ids = form["ids"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var id = ids[0];//updating one record at once;
                var type = connector.Request.ParseActionType(form[id + "_!nativeeditor_status"]);

                if (type == ActionType.Updated)
                {
                    connector.Request.CustomSQLs.Add(CustomSQLType.Update,
                        //string.Format("EXECUTE UpdateEvents '{0}', '{1}', '{2}'",
                        string.Format("EXECUTE UpdateEvent '{0}', '{1}'",
                            Tools.EscapeQueryValue(form[id + "_c0"]), Tools.EscapeQueryValue(form[id + "_c1"])));
                }
                else if (type == ActionType.Inserted)
                {
                    connector.Request.CustomSQLs.Add(CustomSQLType.Insert,
                        string.Format("EXECUTE InsertEvent '{0}', '{1}'",
                            Tools.EscapeQueryValue(form[id + "_c0"]), Tools.EscapeQueryValue(form[id + "_c1"])));
                }
                else if (type == ActionType.Deleted)
                {
                    connector.Request.CustomSQLs.Add(CustomSQLType.Delete,
                        string.Format("EXECUTE DeleteEvent '{0}'",
                            Tools.EscapeQueryValue(id)));
                }
            }

            return connector;
        }
    }
}