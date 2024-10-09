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
    /// Summary description for GrillaObservacionesSegPlanosFabri
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GrillaObservacionesSegPlanosFabri : dhtmlxRequestHandler
    {

        public override IdhtmlxConnector CreateConnector(HttpContext context)
        {
            string idOfa, rol = "", usu = "";
            if (context.Request.QueryString["IdOfa"] != null)
            {
                idOfa = context.Request.QueryString["idOfa"];
            }
            else
            {
                idOfa = "0";
            }

            if (context.Request.QueryString["Rol"] != null)
                rol = context.Request.QueryString["Rol"];

            if (context.Request.QueryString["Usu"] != null)
                usu = context.Request.QueryString["Usu"];

            //Configuración de la grilla
            dhtmlxGridConnector connector = new dhtmlxGridConnector(
                string.Format("EXECUTE USP_Seg_Plan_PlanosFabricacion_Observacion " + idOfa + ""),
                "segplanobs_id",
                dhtmlxDatabaseAdapterType.SqlServer2005,
                BdDatos.conexionScope()
            );

            if (idOfa == "0" || idOfa == "-1")
            {
                dhtmlxGridConfiguration config = new dhtmlxGridConfiguration();
                //config.SetColIds("fup,ver,fprogcomope,frealcomope,zona,estado,planta,paiszona,ciudad,vendedor,cliente,proyecto,tipo,material,probcierre,pago,planos,contrato,fecfact,m2,usdesw,formapago,motperd,mes,viscliente,fecdespplaneado,aprobdesp,fecdespreal,nofact,fecfact,fecestimllegobra");
                config.SetHeader("Of,Tipo,Fecha, Observación,IdActa,Usuario");
                //config.AttachHeader("&nbsp;,&nbsp;,&nbsp;,&nbsp;,#text_filter,#combo_filter,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;");
                config.SetInitWidths("100,160,70,860,0,100");

                config.SetColTypes("rotxt,rotxt,rotxt,edtxt,rotxt,rotxt");
                config.SetColColor(",,,#d5f1ff,,");
                config.SetColSorting("connector,left,date,center,center,left");

                connector.SetConfig(config);
            }

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
                        string.Format("EXECUTE Update_Insert_Seg_Planos_Fabricacion_Observacion '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}'",
                            Tools.EscapeQueryValue(id), Tools.EscapeQueryValue(form[id + "_c4"]), Tools.EscapeQueryValue(form[id + "_c2"]),
                            Tools.EscapeQueryValue(form[id + "_c3"]), Tools.EscapeQueryValue(usu), 1, "2"));
                }
                else if (type == ActionType.Inserted)
                {
                    connector.Request.CustomSQLs.Add(CustomSQLType.Insert,
                        string.Format("EXECUTE Update_Insert_Seg_Planos_Fabricacion_Observacion '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}'",
                            Tools.EscapeQueryValue(id), Tools.EscapeQueryValue(form[id + "_c4"]), Tools.EscapeQueryValue(form[id + "_c2"]),
                            Tools.EscapeQueryValue(form[id + "_c3"]), Tools.EscapeQueryValue(usu), 1, "1"));
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