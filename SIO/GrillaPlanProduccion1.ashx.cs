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
    /// Connector body
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GrillaPlanProduccion1 : dhtmlxRequestHandler
    {

        public override IdhtmlxConnector CreateConnector(HttpContext context)
        {
            string idActa, rol = "", usu = "";
            if (context.Request.QueryString["IdActa"] != null)
            {
                idActa = context.Request.QueryString["idActa"];
            }
            else
            {
                idActa = "0";
            }

            if (context.Request.QueryString["Rol"] != null)
                rol = context.Request.QueryString["Rol"];

            if (context.Request.QueryString["Usu"] != null)
                usu = context.Request.QueryString["Usu"];

            //Configuración de la grilla
            dhtmlxGridConnector connector = new dhtmlxGridConnector(
                string.Format("EXECUTE USP_fup_SEL_ActSeg_PlanProduccion " + idActa + ""),
                "aspr_Id",
                dhtmlxDatabaseAdapterType.SqlServer2005,
                BdDatos.conexionScope()
            );

            if (idActa == "0" || idActa == "-1")
            {
                dhtmlxGridConfiguration config = new dhtmlxGridConfiguration();
                //config.SetColIds("fup,ver,fprogcomope,frealcomope,zona,estado,planta,paiszona,ciudad,vendedor,cliente,proyecto,tipo,material,probcierre,pago,planos,contrato,fecfact,m2,usdesw,formapago,motperd,mes,viscliente,fecdespplaneado,aprobdesp,fecdespreal,nofact,fecfact,fecestimllegobra");
                config.SetHeader("Orden,Año,Mes,Semana,M2Plan, Usuario,FechaActualiza,IdActa,M2Pend");
                //config.AttachHeader("&nbsp;,&nbsp;,&nbsp;,&nbsp;,#text_filter,#combo_filter,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;");
                config.SetInitWidths("90,60,100,70,70,90,90,0,0");
                if ((rol == "2") || (rol == "9") || (rol == "30") || (rol == "3") || (rol == "39")
                    || (rol == "13") || (rol == "38") || (rol == "15") || (rol == "18") || (rol == "35")
                    || (rol == "26"))
                {
                    //config.SetColTypes("rotxt,combo,rotxt,edtxt,rotxt,rotxt");
                    config.SetColTypes("rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt");
                    config.SetColColor(",,,,,,,,");
                }
                else
                {
                    if ((rol == "36") || (rol == "1"))
                    {
                        config.SetColTypes("rotxt,combo,combo,combo,edtxt,rotxt,rotxt,rotxt,rotxt");
                        config.SetColColor(",#d5f1ff,d5f1ff,#d5f1ff,d5f1ff,,,,");
                        // dhxCalendar
                    }
                    else
                    {
                        config.SetColTypes("rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt");
                    }
                }
                config.SetColSorting("connector,left,date,center,right,left,left,left,left");
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
                        string.Format("EXECUTE USP_fup_UPD_ActSeg_Prod '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}'",
                            Tools.EscapeQueryValue(id), Tools.EscapeQueryValue(form[id + "_c7"]), Tools.EscapeQueryValue(form[id + "_c6"]),
                            Tools.EscapeQueryValue(form[id + "_c1"]), Tools.EscapeQueryValue(form[id + "_c2"]), Tools.EscapeQueryValue(form[id + "_c3"]),
                            Tools.EscapeQueryValue(form[id + "_c4"]), Tools.EscapeQueryValue(usu), "2"));
                }
                else if (type == ActionType.Inserted)
                {
                    connector.Request.CustomSQLs.Add(CustomSQLType.Insert,
                        string.Format("EXECUTE USP_fup_UPD_ActSeg_Prod '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}'",
                            Tools.EscapeQueryValue(id), Tools.EscapeQueryValue(form[id + "_c7"]), Tools.EscapeQueryValue(form[id + "_c6"]),
                            Tools.EscapeQueryValue(form[id + "_c1"]), Tools.EscapeQueryValue(form[id + "_c2"]), Tools.EscapeQueryValue(form[id + "_c3"]),
                            Tools.EscapeQueryValue(form[id + "_c4"]), Tools.EscapeQueryValue(usu), "1"));
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