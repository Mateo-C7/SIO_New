using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Web.UI;
using dhtmlxConnectors;
using System.Configuration;
using CapaDatos;

namespace SIO
{
    /// <summary>
    /// Descripción breve de GrillaContenido
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    public class GrillaContenidoPrecioItems : dhtmlxRequestHandler
    {
        public override IdhtmlxConnector CreateConnector(HttpContext context)
        {
            string vFecDesde = "", vFecHasta = "", rol = "", usu = "", trm = "", Perd = "";
            string vFecDesde1 = "", vFecHasta1 = "";

            if (context.Request.QueryString["fecDesde"] != null)
            {
                vFecDesde = context.Request.QueryString["fecDesde"];
                if (vFecDesde != "null" && vFecDesde != "")
                {
                    DateTime dt = Convert.ToDateTime(vFecDesde);
                    vFecDesde1 = dt.ToString("dd/MM/yyyy");
                }
            }

            if (context.Request.QueryString["fecHasta"] != null)
            {
                vFecHasta = context.Request.QueryString["fecHasta"];
                if (vFecHasta != "null" && vFecHasta != "")
                {
                    DateTime dt = Convert.ToDateTime(vFecHasta);
                    vFecHasta1 = dt.ToString("dd/MM/yyyy");
                }
            }

            if (context.Request.QueryString["Rol"] != null)
                rol = context.Request.QueryString["Rol"];

            if (context.Request.QueryString["Usu"] != null)
                usu = context.Request.QueryString["Usu"];

            if (context.Request.QueryString["TRM"] != null)
                trm = context.Request.QueryString["TRM"];

            if (context.Request.QueryString["Perd"] != null)
                Perd = context.Request.QueryString["Perd"];

            dhtmlxGridConnector connector = new dhtmlxGridConnector(
                string.Format("EXECUTE USP_SEL_PrecioItemsPv ") , // + vFecDesde1 + "', '" + vFecHasta1 + "',null,null,null,null,null,null,'" + trm + "','" + usu + "','" + Perd + "'"),
                "TempPrecios_Id",
                dhtmlxDatabaseAdapterType.SqlServer2005,
                BdDatos.conexionScope()
            );



            if (vFecDesde == "null")
            {
                //Configuración de la grilla 
                dhtmlxGridConfiguration config = new dhtmlxGridConfiguration();
                //config.SetColIds("fup,ver,fprogcomope,frealcomope,zona,estado,planta,paiszona,ciudad,vendedor,cliente,proyecto,tipo,material,probcierre,pago,planos,contrato,fecfact,m2,usdesw,formapago,motperd,mes,viscliente,fecdespplaneado,aprobdesp,fecdespreal,nofact,fecfact,fecestimllegobra");
                //config.SetHeader("Fup/Orden,Cliente,Proyecto,Zona,PaisZona,CiudadObra,Prob,M2,UsdExw,ValorCop," +
                //                 "FechFact,EntregaIng,Estatus,Forma De Pago,Pago,Ptf,Contrato,MotivoPerdida,MesPerdida,Tipo," +
                //                 "Material,Vendedor,Planta,F.Visita,F.DespPlan,AprobDesp,F.DespReal,No.Factura,F.Factura,F.LlegObra," +
                //                 "Zona Ciudad,Observaciones,FechaCrea,TipoForm");
                config.SetHeader("CodErp,NombreItem,Costo,Trm,Pleno,PlenoUsd,Distri,DistriUsd,Filial1,Filial1Usd," + //10
                                 "Filial2,Filial2Usd,Planta,Periodo,FechaGeneracion"); //5

                //config.AttachHeader("&nbsp;,&nbsp;,&nbsp;,&nbsp;,#text_search,#select_filter,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;");
                config.SetInitWidths("50,220,70,50,70,58,70,58,70,58," + //10
                                     "70,58,100,55,90"); //5 

                config.SetColAlign("left,left,right,right,right,right,right,right,right,right," + //10
                                   "right,right,right,right,right"); //5

                config.SetColSorting("str,str,str,int,str,str,str,str,date,date," + //10
                                     "int,str,date,date,date"); //5

                if (rol == "1")
                {
                    config.SetColTypes("rotxt,rotxt,ron,ron,edn,edn,edn,edn,edn,edn," + //10
                                       "edn,edn,rotxt,rotxt,rotxt"); //5

                    config.SetColColor(",,,,d5f1ff,d5f1ff,d5f1ff,#d5f1ff,#d5f1ff,d5f1ff," + // 10
                                       "d5f1ff,d5f1ff,,,"); //5
                }
                else
                {
                    config.SetColTypes("rotxt,rotxt,rotxt,link,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," + //10
                    "ron,ron,rotxt,rotxt,rotxt"); //5

                    config.SetColColor(",,,,,,,,,," + // 10
                                        ",,,,,,,,,," + //10
                                        ",,,,,,,,,," + //10
                                        ",,,,,,,,,," + //10
                                        ",,,,,,,,,,,,"); //12                                
                }                           
                

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

                if (Tools.EscapeQueryValue(form[id + "_c9"]) == null)
                    try
                    {
                        string nombre = Tools.EscapeQueryValue(form[id + "_c9"]);
                    }
                    catch
                    {
                        string mensajeVentana = "La sesion ha Expirado, por favor inicie sesión nuevamente. Gracias!";
                        ProcessRequest(context);
                        //cerrarSesion();
                    }

                if (type == ActionType.Updated)
                {
                    //     Zona,Pais,SubZona,Fup/Orden,Cliente,Proyecto,Frm,Prob,FechFact,M2," +
                    //    "UsdExw,Estatus,FechProd,PlanDesp,FechDesp,FechValid,FormaPago,MotivoPerdida,MesPerdida,PTF" +
                    //    "FechPTF,FechModu,Vendedor,Planta,FormaPago,F.DespPlan,AprobDesp,F.DespReal,No.Factura,F.Factura," +
                    //    "F.LlegObra,ZonaCiudad,Observaciones,FechaCrea,dato1,dato2,dato3,dato4    

                    // @actseg_id  int,
                    //@actseg_probabilidad  varchar(5),  7
                    //@actseg_fecFacturar varchar(10),  8
                    //@actseg_Estado  Varchar(50),  11
                    //@actseg_fecEntregaOper_Real varchar(10),   12
                    //@actseg_fecDespachoPlan varchar(10),      13
                    //@actseg_fecVisitaCliente  varchar(10),    15
                    //@actseg_FormaPago  Varchar(50),   16
                    //@actseg_motiv_perd  varchar(MAX), 17
                    //@actseg_fecPerdida  varchar(10),  18
                    //@actseg_fecPtf  varchar(10),  20
                    //@actseg_fecModulacion  varchar(10),   21
                    //@usu_actualiza VARCHAR(50)
                    string consulta = "EXECUTE USP_Act_PrecioItemsPv '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}','{9}' " + form.ToString();
                    //Tools.EscapeQueryValue(id).ToString() + Tools.EscapeQueryValue(form[id + "_c7"]).ToString() + Tools.EscapeQueryValue(form[id + "_c8"]).ToString() +
                    //Tools.EscapeQueryValue(form[id + "_c11"]).ToString() + Tools.EscapeQueryValue(form[id + "_c12"]).ToString() + Tools.EscapeQueryValue(form[id + "_c13"]).ToString() +
                    //Tools.EscapeQueryValue(form[id + "_c15"]).ToString() + Tools.EscapeQueryValue(form[id + "_c16"]).ToString() + Tools.EscapeQueryValue(form[id + "_c17"]).ToString() +
                    //Tools.EscapeQueryValue(form[id + "_c18"]).ToString() + Tools.EscapeQueryValue(form[id + "_c20"]).ToString() + Tools.EscapeQueryValue(form[id + "_c21"]).ToString() +
                    //Tools.EscapeQueryValue(usu).ToString();



                    connector.Request.CustomSQLs.Add(CustomSQLType.Update,
                    //string.Format("EXECUTE UpdateEvents '{0}', '{1}', '{2}'",      
                    string.Format(
                    "EXECUTE USP_Act_PrecioItemsPv '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}','{9}'",
                    Tools.EscapeQueryValue(id), Tools.EscapeQueryValue(form[id + "_c4"]), Tools.EscapeQueryValue(form[id + "_c5"]), Tools.EscapeQueryValue(form[id + "_c6"]),
                    Tools.EscapeQueryValue(form[id + "_c7"]), Tools.EscapeQueryValue(form[id + "_c8"]), Tools.EscapeQueryValue(form[id + "_c9"]),
                    Tools.EscapeQueryValue(form[id + "_c10"]), Tools.EscapeQueryValue(form[id + "_c11"]),Tools.EscapeQueryValue(usu)));

                    string a = string.Format(
                    "EXECUTE USP_Act_PrecioItemsPv '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}','{9}'",
                    Tools.EscapeQueryValue(id), Tools.EscapeQueryValue(form[id + "_c4"]), Tools.EscapeQueryValue(form[id + "_c5"]), Tools.EscapeQueryValue(form[id + "_c6"]),
                    Tools.EscapeQueryValue(form[id + "_c7"]), Tools.EscapeQueryValue(form[id + "_c8"]), Tools.EscapeQueryValue(form[id + "_c9"]),
                    Tools.EscapeQueryValue(form[id + "_c10"]), Tools.EscapeQueryValue(form[id + "_c11"]), Tools.EscapeQueryValue(usu));
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

        void ProcessRequest(HttpContext context)
        {
            context.Response.Redirect("Inicio.aspx");
        }
    }
}