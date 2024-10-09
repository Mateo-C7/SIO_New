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
    /// Descripción breve de GrillaFleteInternacional
    /// </summary>
    
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    public class GrillaFleteInternacional : dhtmlxRequestHandler
    {
        public override IdhtmlxConnector CreateConnector(HttpContext context)
        {
            string Pais = "0", Origen = "0", Destino = "0", Agente = "0", tipo = "0";

            if (context.Request.QueryString["Tipo"] != null)
                tipo = context.Request.QueryString["Tipo"];

            if (context.Request.QueryString["Pais"] != null)
                Pais = context.Request.QueryString["Pais"];

            if (context.Request.QueryString["Origen"] != null)
                Origen = context.Request.QueryString["Origen"];

            if (context.Request.QueryString["Destino"] != null)
                Destino = context.Request.QueryString["Destino"];

            if (context.Request.QueryString["Agente"] != null)
                Agente = context.Request.QueryString["Agente"];

            dhtmlxGridConnector connector = new dhtmlxGridConnector(
               string.Format("USP_SEL_fletes_mae_internacional " + Origen + "," + Destino + "," + Agente + "," +  tipo),
               "mfa_id",
               dhtmlxDatabaseAdapterType.SqlServer2005,
               BdDatos.conexionScope()
           );

            //Configuración de la grilla 
            dhtmlxGridConfiguration config = new dhtmlxGridConfiguration();
            //config.SetColIds("fup,ver,fprogcomope,frealcomope,zona,estado,planta,paiszona,ciudad,vendedor,cliente,proyecto,tipo,material,probcierre,pago,planos,contrato,fecfact,m2,usdesw,formapago,motperd,mes,viscliente,fecdespplaneado,aprobdesp,fecdespreal,nofact,fecfact,fecestimllegobra");
                       
            if (tipo == "2")
            {
                config.SetInitWidths("120,120,120,100,100,100,80,80,80,80,0,0,0,0");
                config.SetHeader("Agente De Carga, Contenedor,Despacho Aduanal (CUSTOM),Gastos Puerto Origen,FleteInternacional (Ocean Freight),Gastos Puerto Destino," +
             "Lead Time FOB,Lead Time CIF,Lead Time DDP, Estado,idgrilla,Origen,dest,agent");
                config.SetColTypes("rotxt,rotxt,ron,ron,ron,ron,ron,ron,ron,ch,rotxt,rotxt,rotxt,rotxt");
            }
            else
            {
                config.SetInitWidths("120,120,100,100,100,80,80,80,80,0,0,0,0");
                config.SetHeader("Contenedor,Despacho Aduanal (CUSTOM),Gastos Puerto Origen,FleteInternacional (Ocean Freight),Gastos Puerto Destino," +
             "Lead Time FOB,Lead Time CIF,Lead Time DDP,Estado,idgrilla,Origen,dest,agent");
                config.SetColTypes("rotxt,edn,edn,edn,edn,edn,edn,edn,ch,rotxt,rotxt,rotxt,rotxt");
            }
            //config.SetColColor(",#d5f1ff,#d5f1ff,#d5f1ff,#d5f1ff,#d5f1ff,#d5f1ff,#d5f1ff,#d5f1ff");
            config.SetColSorting("str,str,str,str,str,str,str,str,str,str,str,str,str");
            //config.SetColSorting("dyn,str,str,str");
            connector.SetConfig(config);

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
                    var va = string.Format("EXECUTE USP_UPD_fletes_mae_Internacional '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}','{13}'",
                            Tools.EscapeQueryValue(form[id + "_c10"]), Tools.EscapeQueryValue(form[id + "_c11"]), Tools.EscapeQueryValue(form[id + "_c12"]),
                            Tools.EscapeQueryValue(form[id + "_c0"]), Tools.EscapeQueryValue(form[id + "_c1"]), Tools.EscapeQueryValue(form[id + "_c2"]),
                            Tools.EscapeQueryValue(form[id + "_c2"]), Tools.EscapeQueryValue(form[id + "_c4"]), Tools.EscapeQueryValue(form[id + "_c5"]),
                            Tools.EscapeQueryValue(form[id + "_c6"]), Tools.EscapeQueryValue(form[id + "_c7"]), Tools.EscapeQueryValue(form[id + "_c8"]),
                            Tools.EscapeQueryValue(form[id + "_c9"]), Tools.EscapeQueryValue(usu));

                    connector.Request.CustomSQLs.Add(CustomSQLType.Update,


                         string.Format("EXECUTE USP_UPD_fletes_mae_Internacional '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}','{13}'",
                            Tools.EscapeQueryValue(form[id + "_c10"]), Tools.EscapeQueryValue(form[id + "_c11"]), Tools.EscapeQueryValue(form[id + "_c12"]),
                            Tools.EscapeQueryValue(form[id + "_c0"]), Tools.EscapeQueryValue(form[id + "_c1"]), Tools.EscapeQueryValue(form[id + "_c2"]),
                            Tools.EscapeQueryValue(form[id + "_c3"]), Tools.EscapeQueryValue(form[id + "_c4"]), Tools.EscapeQueryValue(form[id + "_c5"]),
                            Tools.EscapeQueryValue(form[id + "_c6"]), Tools.EscapeQueryValue(form[id + "_c7"]), Tools.EscapeQueryValue(form[id + "_c8"]),
                            Tools.EscapeQueryValue(form[id + "_c9"]), Tools.EscapeQueryValue(usu)));
                }
    
                else if (type == ActionType.Inserted)
                {
                    var va = string.Format("EXECUTE USP_fup_UPD_CntrlCam_Comentario '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'",
                            Tools.EscapeQueryValue(id), Tools.EscapeQueryValue(form[id + "_c4"]), Tools.EscapeQueryValue(form[id + "_c0"])
                            , Tools.EscapeQueryValue(form[id + "_c3"]), Tools.EscapeQueryValue(usu), "1");

                    connector.Request.CustomSQLs.Add(CustomSQLType.Insert,
                       string.Format("EXECUTE USP_fup_UPD_CntrlCam_Comentario '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'",
                            Tools.EscapeQueryValue(id), Tools.EscapeQueryValue(form[id + "_c4"]), Tools.EscapeQueryValue(form[id + "_c0"])
                            , Tools.EscapeQueryValue(form[id + "_c3"]), Tools.EscapeQueryValue(usu), "1"));
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