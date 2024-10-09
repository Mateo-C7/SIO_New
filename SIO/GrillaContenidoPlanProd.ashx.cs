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

    public class GrillaContenidoPlanProd : dhtmlxRequestHandler
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
                string.Format("EXECUTE USP_fup_SEL_ActaSeguimientoV3_Prod '" + vFecDesde1 + "', '" + vFecHasta1 + "',null,null,null,null,null,null,'" + trm + "','" + usu + "','" + Perd + "'"),
                "actseg_id",
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
                config.SetHeader("Zona,Pais,SubZona,Fup/Orden,Cliente,Proyecto,Frm,Prob,MesFac,FecEntExw," + //10
                                 "M2,UsdExw,Estatus,NvComplej,PrdCom,MesProd,DespIni,DespPlan,DespReal,FechValid,FormaPago,MotivoPerdida," + //12
                                 "MesPerdida,EmpresaCompetencia,DFT,SolDFT,EstadoDFT,FechModu,Tipo,Material,Vendedor,FechaCrea," + //10
                                 "AprobDesp,F.DespReal,No.Factura,F.Factura,F.LlegObra,ZonaCiudad,Observaciones,FechaCrea,dato1,dato2," +//10
                                 "dato3,dato4,Planta,PlantaPlan,SistemaSeg,Kilos,ReqForsa,EntProvIni,EntProv,TipoNegociacion,M2PendPlanProd,EstadoPlan"); //12

                //config.AttachHeader("&nbsp;,&nbsp;,&nbsp;,&nbsp;,#text_search,#select_filter,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;");
                config.SetInitWidths("70,60,60,100,150,120,70,55,48,70," + //10
                                     "75,90,80,55,48,48,60,60,60,60,120,100," + //12
                                     "70,90,35,60,80,60,70,70,100,70," + //10
                                     "0,0,0,0,0,0,0,0,0,0," + //10
                                     "0,0,100,100,60,60,70,70,70,90,0,90"); //12 

                if (rol == "1")
                {
                    config.SetColTypes("rotxt,rotxt,rotxt,link,rotxt,rotxt,rotxt,combo,dhxCalendarA,rotxt," + //10
                                       "ron,ron,combo,rotxt,dhxCalendarA,dhxCalendarA,rotxt,dhxCalendarA,rotxt,dhxCalendarA,edtxt,combo," + //12
                                       "dhxCalendarA,combo,rotxt,rotxt,combo,dhxCalendarA,rotxt,rotxt,rotxt,rotxt," +//10
                                       "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," +//10 
                                       "rotxt,rotxt,rotxt,combo,rotxt,rotxt,dhxCalendarA,rotxt,dhxCalendarA,rotxt,rotxt,rotxt"); //12

                    config.SetColColor(",,,,,,,#d5f1ff,#d5f1ff,," + // 10
                                       ",,#d5f1ff,,#d5f1ff,#d5f1ff,,#d5f1ff,,#d5f1ff,#d5f1ff,#d5f1ff," + //12
                                       "#d5f1ff,#d5f1ff,,,#d5f1ff,#d5f1ff,,,,," + //10
                                       ",,,,,,,,,," + //10
                                       ",,,#d5f1ff,,,#d5f1ff,,#d5f1ff,,,,"); //12
                }
                else
                {
                    //Planeacion Producción
                    if ((rol == "36"))
                    {
                        config.SetColTypes("rotxt,rotxt,rotxt,link,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," + //10
                                       "ron,ron,rotxt,rotxt,dhxCalendarA,dhxCalendarA,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," + //12
                                       "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," +//10
                                       "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," +//10 
                                       "rotxt,rotxt,rotxt,combo,rotxt,rotxt,dhxCalendarA,rotxt,dhxCalendarA,rotxt,rotxt,rotxt"); //12

                        config.SetColColor(",,,,,,,,,," + // 10
                                           ",,,,#d5f1ff,,,,,,,," + //12
                                           ",,,,,,,,,," + //10
                                           ",,,,,,,,,," + //10
                                           ",,,#d5f1ff,,,#d5f1ff,,#d5f1ff,,,,"); //11
                    }
                    else
                    {
                        //Comercial
                        if ((rol == "2") || (rol == "9") || (rol == "30") || (rol == "39") || (rol == "5") || (rol == "26"))
                        {
                            config.SetColTypes("rotxt,rotxt,rotxt,link,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," + //10
                                        "ron,ron,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," + //12
                                        "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," +//10
                                        "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," +//10 
                                        "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt"); //12

                            config.SetColColor(",,,,,,,,,," + // 10
                                               ",,,,,,,,,,,," + //12
                                               ",,,,,,,,,," + //10
                                               ",,,,,,,,,," + //10
                                               ",,,,,,,,,,,,"); //12
                        }
                        else
                        {
                            //Cotizaciones e ingenieria
                            if (rol == "26")
                            {
                                config.SetColTypes("rotxt,rotxt,rotxt,link,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," + //10
                                        "ron,ron,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," + //10
                                        "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," +//10
                                        "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," +//10 
                                        "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt"); //12

                                config.SetColColor(",,,,,,,,,," + // 10
                                                   ",,,,,,,,,,,," + //12
                                                   ",,,,,,,,,," + //10
                                                   ",,,,,,,,,," + //10
                                                   ",,,,,,,,,,,,"); //12
                            }
                            else
                            {
                                //Logistica
                                if (rol == "15")
                                {

                                    config.SetColTypes("rotxt,rotxt,rotxt,link,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," + //10
                                        "ron,ron,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," + //12
                                        "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," +//10
                                        "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," +//10 
                                        "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt"); //12

                                    config.SetColColor(",,,,,,,,,," + // 10
                                                       ",,,,,,,,,," + //10
                                                       ",,,,,,,,,," + //10
                                                       ",,,,,,,,,," + //10
                                                       ",,,,,,,,,,,,"); //11

                                }
                                else
                                {
                                    //Agentes Comerciales
                                    if (rol == "3")
                                    {
                                        config.SetColTypes("rotxt,rotxt,rotxt,link,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," + //10
                                        "ron,ron,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," + //12
                                        "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," +//10
                                        "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," +//10 
                                        "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt"); //12

                                        config.SetColColor(",,,,,,,,,," + // 10
                                                           ",,,,,,,,,,,," + //12
                                                           ",,,,,,,,,," + //10
                                                           ",,,,,,,,,," + //10
                                                           ",,,,,,,,,,,,"); //12
                                    }
                                    else
                                    {
                                        //Produccion
                                        if (rol == "13")
                                        {
                                            config.SetColTypes("rotxt,rotxt,rotxt,link,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," + //10
                                        "ron,ron,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," + //12
                                        "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," +//10
                                        "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," +//10 
                                        "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt"); //12

                                            config.SetColColor(",,,,,,,,,," + // 10
                                                               ",,,,,,,,,," + //12
                                                               ",,,,,,,,,," + //10
                                                               ",,,,,,,,,," + //10
                                                               ",,,,,,,,,,, "); //12
                                        }

                                        else
                                        {
                                            //Gestion calidad para ogs y oms
                                            if (rol == "18" || rol == "35")
                                            {
                                                config.SetColTypes("rotxt,rotxt,rotxt,link,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," + //10
                                                 "ron,ron,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," + //12
                                                 "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," +//10
                                                 "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," +//10 
                                                 "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt"); //12

                                                config.SetColColor(",,,,,,,,,," + // 10
                                                                   ",,,,,,,,,,,," + //12
                                                                   ",,,,,,,,,," + //10
                                                                   ",,,,,,,,,," + //10
                                                                   ",,,,,,,,,,,,"); //11
                                            }

                                            else
                                            {

                                                config.SetColTypes("rotxt,rotxt,rotxt,link,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," + //10
                                                "ron,ron,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," + //12
                                                "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," +//10
                                                "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," +//10 
                                                "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt"); //12

                                                config.SetColColor(",,,,,,,,,," + // 10
                                                                   ",,,,,,,,,,,," + //12
                                                                   ",,,,,,,,,," + //10
                                                                   ",,,,,,,,,," + //10
                                                                   ",,,,,,,,,,,,"); //12
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }


                config.SetColAlign("left,left,left,left,left,left,left,right,right,right," + //10
                                   "right,right,left,right,left,right,right,right,right,right,left,left," + //12
                                   "left,left,left,left,left,left,left,right,right,left," + //10
                                   "right,left,left,left,left,left,left,left,left,left" +//10
                                   "left,left,left,left,left,right,left,left,left,right,left,left"); //12
                //config.SetColSorting("dyn,str,date,date,center,str,center,center,center,center,center,center,center,center,str,str,center,connector,date,int,int,center,connector,center,date,date,connector,date,center,date,date");
                config.SetColSorting("str,str,str,str,str,str,str,str,date,date," + //10
                                     "int,int,str,str,date,date,date,date,date,date,str,str," + //12
                                     "date,str,str,date,str,date,str,str,str,date," +//10
                                     "str,str,str,str,date,date,date,str,str,str," +//10
                                     "str,str,str,str,str,str,date,date,date,str,str,str"); //12
                //config.SetColSorting("str,str,str,str,str,str,int,int,int,date,date,str,str,str,str,str,str,date,str, str,str,str,date,date,str, date, str, date, date, str");
                //config.SetColSorting("str,str,str,str,str,str,int,int,int,int,date,date,str,str,str,str,str,str,date,str,str,str,str,str,rotxt,co,rotxt,rotxt,rotxt,rotxt,rotxt");


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
                    //string consulta = "EXECUTE USP_fup_UPD_ActaSeguimientoV3_Prod '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}','{9}', " +
                    //"'{10}', '{11}','{12}', '{13}', '{14}', '{15}', '{16}','{17}','{18},'{19}'" + form.ToString();
                    //Tools.EscapeQueryValue(id).ToString() + Tools.EscapeQueryValue(form[id + "_c7"]).ToString() + Tools.EscapeQueryValue(form[id + "_c8"]).ToString() +
                    //Tools.EscapeQueryValue(form[id + "_c11"]).ToString() + Tools.EscapeQueryValue(form[id + "_c12"]).ToString() + Tools.EscapeQueryValue(form[id + "_c13"]).ToString() +
                    //Tools.EscapeQueryValue(form[id + "_c15"]).ToString() + Tools.EscapeQueryValue(form[id + "_c16"]).ToString() + Tools.EscapeQueryValue(form[id + "_c17"]).ToString() +
                    //Tools.EscapeQueryValue(form[id + "_c18"]).ToString() + Tools.EscapeQueryValue(form[id + "_c20"]).ToString() + Tools.EscapeQueryValue(form[id + "_c21"]).ToString() +
                    //Tools.EscapeQueryValue(usu).ToString();



                    connector.Request.CustomSQLs.Add(CustomSQLType.Update,
                    //string.Format("EXECUTE UpdateEvents '{0}', '{1}', '{2}'",      
                    string.Format(
                    "EXECUTE USP_fup_UPD_ActaSeguimientoV3_Prod '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}','{9}', " +
                    "'{10}', '{11}', '{12}','{13}', '{14}', '{15}', '{16}','{17}','{18}','{19}'",
                    Tools.EscapeQueryValue(id), Tools.EscapeQueryValue(form[id + "_c7"]), Tools.EscapeQueryValue(form[id + "_c8"]), Tools.EscapeQueryValue(form[id + "_c9"]),
                    Tools.EscapeQueryValue(form[id + "_c12"]), Tools.EscapeQueryValue(form[id + "_c14"]), Tools.EscapeQueryValue(form[id + "_c15"]), Tools.EscapeQueryValue(form[id + "_c17"]),
                    Tools.EscapeQueryValue(form[id + "_c19"]), Tools.EscapeQueryValue(form[id + "_c20"]), Tools.EscapeQueryValue(form[id + "_c21"]),
                    Tools.EscapeQueryValue(form[id + "_c22"]), Tools.EscapeQueryValue(form[id + "_c23"]), Tools.EscapeQueryValue(form[id + "_c25"]),
                    Tools.EscapeQueryValue(form[id + "_c26"]), Tools.EscapeQueryValue(form[id + "_c27"]), Tools.EscapeQueryValue(form[id + "_c45"]), Tools.EscapeQueryValue(form[id + "_c48"]),
                    Tools.EscapeQueryValue(form[id + "_c50"]), Tools.EscapeQueryValue(usu)));

                    string a = string.Format(
                    "EXECUTE USP_fup_UPD_ActaSeguimientoV3_Prod '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}','{9}', " +
                    "'{10}', '{11}', '{12}','{13}', '{14}', '{15}', '{16}','{17}','{18}','{19}'",
                    Tools.EscapeQueryValue(id), Tools.EscapeQueryValue(form[id + "_c7"]), Tools.EscapeQueryValue(form[id + "_c8"]), Tools.EscapeQueryValue(form[id + "_c9"]),
                    Tools.EscapeQueryValue(form[id + "_c12"]), Tools.EscapeQueryValue(form[id + "_c14"]), Tools.EscapeQueryValue(form[id + "_c15"]), Tools.EscapeQueryValue(form[id + "_c17"]),
                    Tools.EscapeQueryValue(form[id + "_c19"]), Tools.EscapeQueryValue(form[id + "_c20"]), Tools.EscapeQueryValue(form[id + "_c21"]),
                    Tools.EscapeQueryValue(form[id + "_c22"]), Tools.EscapeQueryValue(form[id + "_c23"]), Tools.EscapeQueryValue(form[id + "_c25"]),
                    Tools.EscapeQueryValue(form[id + "_c26"]), Tools.EscapeQueryValue(form[id + "_c27"]), Tools.EscapeQueryValue(form[id + "_c45"]), Tools.EscapeQueryValue(form[id + "_c48"]),
                    Tools.EscapeQueryValue(form[id + "_c50"]), Tools.EscapeQueryValue(usu));
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