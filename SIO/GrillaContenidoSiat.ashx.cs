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
    /// Summary description for GrillaContenidoSiat
    /// </summary>    
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    public class GrillaContenidoSiat : dhtmlxRequestHandler
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
                string.Format("EXECUTE USP_fup_SEL_ActaSeguimientoSiat '" + usu  + "'"),
                "IdFup",
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
                config.SetHeader("Zona,Pais,SubZona,Ciudad,Fup,Cliente,Proyecto,Vendedor,CiudadObra,Ordenes," + //10
                                 "M2Sf,ValorSf,Mda,TipoCotizacion,SisSeg,MtvoServicio,F.PlanDespacho,F.ArriboEstimado,F.EntObraEsti,DiasAsisTec," + //10
                                 "DiasAsig,DiasAdic,MtvoAdic,DiasPend,Tecnico,F.LLegaTecnObra,F.FinTecnObra,Cp,MesFact,ObsCallCenter," + //10
                                 "Indicad,ResulEncu,CalifTecnico"); //3

                //config.AttachHeader("&nbsp;,&nbsp;,&nbsp;,&nbsp;,#text_search,#select_filter,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;");
                config.SetInitWidths("70,60,60,100,55,120,120,100,90,90," + //10
                                     "60,80,30,90,45,90,60,60,60,60," + //10
                                     "60,60,90,60,120,60,60,60,60,60," + //10
                                     "60,60,60"); //3 

                if (rol == "1")
                {
                    config.SetColTypes("rotxt,rotxt,rotxt,rotxt,link,rotxt,rotxt,rotxt,rotxt,rotxt," + //10
                                       "ron,ron,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," + //10
                                       "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,edtxt," +//10
                                       "edtxt,edtxt,edtxt"); //3

                    config.SetColColor(",,,,,,,,,," + // 10
                                       ",,,,,,,,,," + // 10
                                       ",,,,,,,,,#d5f1ff," + // 10
                                       "#d5f1ff,#d5f1ff,#d5f1ff"); //3
                }
                else
                {
                    //Planeacion Producción
                    if ((rol == "38"))
                    {
                        config.SetColTypes("rotxt,rotxt,rotxt,rotxt,link,rotxt,rotxt,rotxt,rotxt,rotxt," + //10
                                       "ron,ron,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," + //10
                                       "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,edtxt," +//10
                                       "edtxt,edtxt,edtxt"); //3

                        config.SetColColor(",,,,,,,,,," + // 10
                                           ",,,,,,,,,," + // 10
                                           ",,,,,,,,,#d5f1ff," + // 10
                                           "#d5f1ff,#d5f1ff,#d5f1ff"); //3
                    }
                  
                    else
                    {

                        config.SetColTypes("rotxt,rotxt,rotxt,rotxt,link,rotxt,rotxt,rotxt,rotxt,rotxt," + //10
                                        "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," + //10
                                        "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,edtxt," +//10
                                        "rotxt,rotxt,rotxt"); //3

                        config.SetColColor(",,,,,,,,,," + // 10
                                           ",,,,,,,,,," + // 10
                                           ",,,,,,,,,," + // 10
                                           ",,"); //3
                    }
                                        
                }


                config.SetColAlign("left,left,left,left,left,left,left,left,left,left," + //10
                                   "right,right,left,left,left,right,right,right,right,right," + //10
                                   "left,left,left,left,left,left,left,left,left,left," + //10
                                   "right,left,left"); //3
                //config.SetColSorting("dyn,str,date,date,center,str,center,center,center,center,center,center,center,center,str,str,center,connector,date,int,int,center,connector,center,date,date,connector,date,center,date,date");
                config.SetColSorting("str,str,str,str,str,str,str,str,str,str," + //10
                                     "int,int,str,str,str,str,str,str,str,int," + //10
                                     "int,int,str,int,str,str,str,str,str,str," +//10
                                     "str,str,str"); //3
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

                    string a = string.Format(
                    "EXECUTE USP_fup_UPD_ActaSeguimiento_Siat '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'",
                    Tools.EscapeQueryValue(id), Tools.EscapeQueryValue(form[id + "_c29"]), Tools.EscapeQueryValue(form[id + "_c30"]), Tools.EscapeQueryValue(form[id + "_c31"]),
                    Tools.EscapeQueryValue(form[id + "_c32"]), Tools.EscapeQueryValue(usu));            



                    connector.Request.CustomSQLs.Add(CustomSQLType.Update,
                    //string.Format("EXECUTE UpdateEvents '{0}', '{1}', '{2}'",      
                    string.Format(
                    "EXECUTE USP_fup_UPD_ActaSeguimiento_Siat '{0}', '{1}', '{2}', '{3}', '{4}', '{5}'",
                    Tools.EscapeQueryValue(id), Tools.EscapeQueryValue(form[id + "_c29"]), Tools.EscapeQueryValue(form[id + "_c30"]), Tools.EscapeQueryValue(form[id + "_c31"]),
                    Tools.EscapeQueryValue(form[id + "_c32"]), Tools.EscapeQueryValue(usu)));

                    
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