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
    /// Connector body
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    public class GrillaControlCambioComentario : dhtmlxRequestHandler
    {
        public override IdhtmlxConnector CreateConnector(HttpContext context)
        {
            string fup = "", idCom;
            if (context.Request.QueryString["IdCom"] != null)
            {
                idCom = context.Request.QueryString["IdCom"];
            }
            else
            {
                idCom = "0";
            }

            if (context.Request.QueryString["fup"] != null)
                fup = context.Request.QueryString["fup"];

            dhtmlxGridConnector connector = new dhtmlxGridConnector(
                string.Format("USP_fup_SEL_CtrlCam_Comentarios '" + idCom + "'"),
                "ccc_id",
                dhtmlxDatabaseAdapterType.SqlServer2005,
                "data source=172.21.0.38;persist security info=False;initial catalog=Forsa; user id=forsa; password=forsa2006"
            );

            //Configuración de la grilla 
            dhtmlxGridConfiguration config = new dhtmlxGridConfiguration();
            //config.SetColIds("fup,ver,fprogcomope,frealcomope,zona,estado,planta,paiszona,ciudad,vendedor,cliente,proyecto,tipo,material,probcierre,pago,planos,contrato,fecfact,m2,usdesw,formapago,motperd,mes,viscliente,fecdespplaneado,aprobdesp,fecdespreal,nofact,fecfact,fecestimllegobra");
            config.SetHeader("Fecha,Area,Ingresado Por,Comentarios,papa");
            config.SetInitWidths("80,100,200,350,1");
            config.SetColTypes("dhxCalendar,rotxt,rotxt,edtxt,rotxt");
            //config.SetColColor(",,#d5f1ff,#d5f1ff,,#d5f1ff,,,,,,,,#d5f1ff,#d5f1ff,,#d5f1ff,#d5f1ff,,#d5f1ff,#d5f1ff,#d5f1ff,,#d5f1ff,#d5f1ff,#d5f1ff,#d5f1ff,#d5f1ff,#d5f1ff,#d5f1ff");
            //config.SetColSorting("connector,connector,date,date,center,str,center,center,center,center,center,center,center,center,str,str,center,connector,date,int,int,center,connector,center,date,date,connector,date,center,date,date");
            config.SetColSorting("date,str,str,str,str");
            connector.SetConfig(config);

            if (idCom == "0" || idCom == "-1")
            {
               
            }
            //to perform create/update/delete operations you need to define custom handlers
            //and parse request data for parameters
            var form = context.Request.Form;
            if (form["ids"] != null)
            {
                string[] ids = form["ids"].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var id = ids[0];//updating one record at once;
                
                var type = connector.Request.ParseActionType(form[id + "_!nativeeditor_status"]);
                var usu = "UsuPrueba";               
                
                if (type == ActionType.Updated)
                {
                    var va = string.Format("EXECUTE USP_fup_UPD_CntrlCam_Comentario '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'",
                            Tools.EscapeQueryValue(form[id + "_c4"]), Tools.EscapeQueryValue(id), Tools.EscapeQueryValue(form[id + "_c0"])
                            , Tools.EscapeQueryValue(form[id + "_c3"]), Tools.EscapeQueryValue(usu), "2");

                    connector.Request.CustomSQLs.Add(CustomSQLType.Update,
                        //string.Format("EXECUTE UpdateEvents '{0}', '{1}', '{2}'",
                         string.Format("EXECUTE USP_fup_UPD_CntrlCam_Comentario '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'",
                            Tools.EscapeQueryValue(id), Tools.EscapeQueryValue(form[id + "_c4"]), Tools.EscapeQueryValue(form[id + "_c0"])
                            , Tools.EscapeQueryValue(form[id + "_c3"]), Tools.EscapeQueryValue(usu), "2"));
                }
                else if (type == ActionType.Inserted)
                {
                    var va = string.Format("EXECUTE USP_fup_UPD_CntrlCam_Comentario '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'",
                            Tools.EscapeQueryValue(id), Tools.EscapeQueryValue(form[id + "_c4"]), Tools.EscapeQueryValue(form[id + "_c0"])
                            , Tools.EscapeQueryValue(form[id + "_c3"]),Tools.EscapeQueryValue(usu), "1");

                    connector.Request.CustomSQLs.Add(CustomSQLType.Insert,
                       string.Format("EXECUTE USP_fup_UPD_CntrlCam_Comentario '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'",
                            Tools.EscapeQueryValue(id), Tools.EscapeQueryValue(form[id + "_c4"]), Tools.EscapeQueryValue(form[id + "_c0"])
                            , Tools.EscapeQueryValue(form[id + "_c3"]),Tools.EscapeQueryValue(usu), "1"));
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