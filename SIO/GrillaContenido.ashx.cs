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
    
    public class GrillaContenido : dhtmlxRequestHandler
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
                string.Format("EXECUTE USP_fup_SEL_ActaSeguimientoV3 '" + vFecDesde1 + "', '" + vFecHasta1 + "',null,null,null,null,null,null,'" + trm + "','" + usu + "','"+Perd+ "'"),
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
                config.SetHeader("Zona,Pais,SubZona,Fup/Orden,Cliente,Proyecto,Frm,Prob,MesFac,F.ExwNeg,F.ContractualTdn,F.EntVirtual,M2," + //12
                                 "UsdExw,Estatus,EstadoFup,LibDespa,PrdCom,MesProd,DespIni,DespPlan,DespReal,FechValid,FormaPago,MotivoPerdida,MesPerdida,EmpresaCompetencia,DFT," + //14
                                 "SolDFT,EstadoDFT,FechModu,Tipo,Material,Vendedor,FechaCrea,AprobDesp,F.DespReal,No.Factura,F.Factura," + //11
                                 "F.LlegObra,ZonaCiudad,Observaciones,FechaCrea,dato1,dato2,dato3,dato4,PlantaComercial,Planta,PlantaPlan,LVida,SistemaSeg,Kilos," +//14
                                 "ReqForsa,EntProvIni,EntProv,TipoNegociacion,Curado,MarcaQr,Reserva"); //6
                //config.AttachHeader("&nbsp;,&nbsp;,&nbsp;,&nbsp;,#text_search,#select_filter,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;");
                config.SetInitWidths("70,60,85,100,150,120,35,55,48,80,90,80,75," + //12
                                     "90,80,80,35,48,48,60,60,60,60,120,100,70,90,35," + //14
                                     "60,80,60,70,70,100,70,0,0,0,0," + //11
                                     "0,0,0,0,0,0,0,0,100,100,100,40,65,80," + //14
                                     "70,70,70,90,55,55,55"); //7 
                                
                if (rol == "1")
                {
                    config.SetColTypes("rotxt,rotxt,rotxt,link,link,link,link,combo,dhxCalendarA,rotxt,rotxt,rotxt,ron," + //12
                                       "ron,combo,rotxt,rotxt,dhxCalendarA,dhxCalendarA,rotxt,dhxCalendarA,rotxt,dhxCalendarA,edtxt,combo,dhxCalendarA,combo,rotxt," + //14
                                       "rotxt,combo,dhxCalendarA,rotxt,rotxt,rotxt,rotxt,rotxt,edtxt,dhxCalendarA,dhxCalendarA," +//11
                                       "ron,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,combo,rotxt,combo,rotxt,rotxt,ron," + //14
                                       "dhxCalendarA,rotxt,dhxCalendarA,rotxt,rotxt,combo,combo"); //7
                    
                    config.SetColColor(",,,,,,,#d5f1ff,#d5f1ff,,,,," + // 12
                                       ",#d5f1ff,,,#d5f1ff,,#d5f1ff,#d5f1ff,,#d5f1ff,#d5f1ff,#d5f1ff,#d5f1ff,#d5f1ff,," + //14
                                       ",#d5f1ff,#d5f1ff,,,,,#d5f1ff,#d5f1ff,,#d5f1ff," + //12
                                       ",,,,,,,,#d5f1ff,,#d5f1ff,,," + //14                                       
                                       ",#d5f1ff,,,,,#d5f1ff,#d5f1ff");  //7
                }
                else
                {
                    //asistente comercial
                    if( (rol == "9")  || (rol == "39"))
                    {
                        config.SetColTypes("rotxt,rotxt,rotxt,link,link,link,link,combo,dhxCalendarA,rotxt,rotxt,rotxt,ron," + //12
                                           "ron,combo,rotxt,rotxt,dhxCalendarA,rotxt,rotxt,rotxt,rotxt,rotxt,edtxt,combo,dhxCalendarA,combo,rotxt," + //14
                                           "rotxt,rotxt,dhxCalendarA,rotxt,rotxt,rotxt,rotxt,rotxt,edtxt,dhxCalendarA,dhxCalendarA," + //11
                                           "ron,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,combo,rotxt,rotxt,rotxt,rotxt,ron," + //14
                                           "dhxCalendarA,rotxt,dhxCalendarA,rotxt,rotxt,rotxt,combo"); //7
                        //config.SetColTypes("rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,combo,ron,ron,ron,dhxCalendarA,dhxCalendarA,combo,edtxt,co,co,co,combo,dhxCalendarA,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,co,rotxt,rotxt,rotxt,rotxt,rotxt");
                        config.SetColColor(",,,,,,,#d5f1ff,#d5f1ff,,,,," +//12
                                           ",#d5f1ff,,,#d5f1ff,,,,,,#d5f1ff,#d5f1ff,#d5f1ff,#d5f1ff,," +//14
                                           ",,#d5f1ff,,,,,#d5f1ff,#d5f1ff,,#d5f1ff," +//11
                                           ",,,,,,,,#d5f1ff,,,,," + //14  
                                           ",#d5f1ff,,,,,,#d5f1ff"); //   6


                    }
                    else
                    { 
                    //gerente comercial
                    if ((rol == "2") )
                    {
                        config.SetColTypes("rotxt,rotxt,rotxt,link,link,link,link,combo,dhxCalendarA,rotxt,rotxt,rotxt,ron," + //12
                                           "ron,combo,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,edtxt,combo,dhxCalendarA,combo,rotxt," + //14
                                           "rotxt,rotxt,dhxCalendarA,rotxt,rotxt,rotxt,rotxt,rotxt,edtxt,dhxCalendarA,dhxCalendarA," + //11
                                           "ron,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,ron," + //14
                                           "dhxCalendarA,rotxt,dhxCalendarA,rotxt,rotxt,rotxt,rotxt"); // 7
                        //config.SetColTypes("rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,combo,ron,ron,ron,dhxCalendarA,dhxCalendarA,combo,edtxt,co,co,co,combo,dhxCalendarA,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,co,rotxt,rotxt,rotxt,rotxt,rotxt");
                        config.SetColColor(",,,,,,,#d5f1ff,#d5f1ff,,,,," +//12
                                           ",#d5f1ff,,,,,,,,,#d5f1ff,#d5f1ff,#d5f1ff,#d5f1ff,," +//14
                                           ",,#d5f1ff,,,,,#d5f1ff,#d5f1ff,,#d5f1ff," +//11
                                           ",,,,,,,,,,,,,," + //14
                                           ",#d5f1ff,,,,,"); //   7
                    } 
                    else
                    {

                        //Comercial
                        if (  (rol == "30") || (rol == "39") || (rol == "5"))
                        {
                            config.SetColTypes("rotxt,rotxt,rotxt,link,link,link,link,combo,dhxCalendarA,rotxt,rotxt,rotxt,ron," + //12
                                               "ron,combo,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,edtxt,combo,dhxCalendarA,combo,rotxt," + //14
                                               "rotxt,rotxt,dhxCalendarA,rotxt,rotxt,rotxt,rotxt,rotxt,edtxt,dhxCalendarA,dhxCalendarA," + //11
                                               "ron,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,ron," + //14
                                               "dhxCalendarA,rotxt,dhxCalendarA,rotxt,rotxt,rotxt,combo"); //   6
                            //config.SetColTypes("rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,combo,ron,ron,ron,dhxCalendarA,dhxCalendarA,combo,edtxt,co,co,co,combo,dhxCalendarA,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,co,rotxt,rotxt,rotxt,rotxt,rotxt");
                            config.SetColColor(",,,,,,,#d5f1ff,#d5f1ff,,,,," +//12
                                               ",#d5f1ff,,,,,,,,,#d5f1ff,#d5f1ff,#d5f1ff,#d5f1ff,," +//14
                                               ",,#d5f1ff,,,,,#d5f1ff,#d5f1ff,,#d5f1ff," +//11
                                               ",,,,,,,,,,,,,," + //14
                                               ",#d5f1ff,,#d5f1ff,#d5f1ff,,#d5f1ff"); //   6
                        }
                        else
                        {
                        //Planeacion Producción
                        if ((rol == "36"))
                        {
                            config.SetColTypes("rotxt,rotxt,rotxt,link,link,link,link,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," +//12
                                               "rotxt,combo,rotxt,rotxt,rotxt,dhxCalendarA,rotxt,dhxCalendarA,rotxt,dhxCalendarA,rotxt,rotxt,rotxt,rotxt,rotxt," +//14
                                               "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," +//11
                                               "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,combo,rotxt,rotxt,ron," +//14
                                               "dhxCalendarA,rotxt,dhxCalendarA,rotxt,rotxt,combo,rotxt"); //7
                            //config.SetColTypes("rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,combo,ron,ron,ron,dhxCalendarA,dhxCalendarA,combo,edtxt,co,co,co,combo,dhxCalendarA,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,co,rotxt,rotxt,rotxt,rotxt,rotxt");
                            config.SetColColor(",,,,,,,,,,,,," +//12
                                               ",#d5f1ff,,,,#d5f1ff,,#d5f1ff,,#d5f1ff,,,,,," +//14
                                               ",,,,,,,,,,," + //11
                                               ",,,,,,,,,,#d5f1ff,,,,," +//14
                                               ",#d5f1ff,,,,#d5f1ff,");  //   7
                                }
                        else
                        {
                            //Cotizaciones e ingenieria
                            if  (rol == "26")
                            {
                                config.SetColTypes("rotxt,rotxt,rotxt,link,link,link,link,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," +//12
                                                    "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," +//14
                                                    "rotxt,combo,dhxCalendarA,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," +//11
                                                    "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,ron," +//14
                                                    "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt");//6
                                //config.SetColTypes("rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,combo,ron,ron,ron,dhxCalendarA,dhxCalendarA,combo,edtxt,co,co,co,combo,dhxCalendarA,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,co,rotxt,rotxt,rotxt,rotxt,rotxt");
                                config.SetColColor(",,,,,,,,,,,,," +//12
                                                   ",,,,,,,,,,,,,,," + //14
                                                   ",,#d5f1ff,#d5f1ff,,,,,,,,," +
                                                   ",,,,,,,,,,,,,," + //14
                                                   ",,,,,,"); //6
                                    }
                            else
                            {
                                //Logistica
                                if (rol == "15")
                                {

                                    config.SetColTypes("rotxt,rotxt,rotxt,link,link,link,link,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," +//12
                                                        "rotxt,combo,rotxt,rotxt,rotxt,rotxt,rotxt,dhxCalendarA,rotxt,dhxCalendarA,rotxt,rotxt,rotxt,rotxt,rotxt," +//14
                                                       "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,edtxt,dhxCalendarA,dhxCalendarA," +//11
                                                       "ron,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,ron," +//14
                                                       "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt");//6
                                    config.SetColColor(",,,,,,,,,,,,," + //12
                                                       ",#d5f1ff,,,,,,#d5f1ff,,#d5f1ff,,,,,,," + //14
                                                       ",,,,,,,,#d5f1ff,#d5f1ff,#d5f1ff,"  + //11
                                                       ",,,,,,,,,,,,,," + //14
                                                       ",,,,,,");  //6

                                }
                                else
                                {
                                    //Agentes Comerciales
                                    if (rol == "3")
                                    {
                                        config.SetColTypes("rotxt,rotxt,rotxt,link,link,link,link,combo,dhxCalendarA,rotxt,rotxt,rotxt,ron," +//12
                                                           "ron,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,combo,dhxCalendarA,combo,rotxt," +//14
                                                           "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," +//12
                                                           "ron,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,ron," +//14
                                                           "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt");//6
                                        //config.SetColTypes("rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,combo,ron,ron,ron,dhxCalendarA,dhxCalendarA,combo,edtxt,co,co,co,combo,dhxCalendarA,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,co,rotxt,rotxt,rotxt,rotxt,rotxt");
                                        config.SetColColor(",,,,,,,#d5f1ff,#d5f1ff,,,,," +
                                                           ",,,,,,,,,,#d5f1ff,#d5f1ff,#d5f1ff,," + // 14
                                                           ",,,,,,,,,,,,," +
                                                           ",,,,,,,,,,,,,,,,,,,,");
                                    }
                                    else
                                    {
                                        //Produccion
                                        if (rol == "13")
                                        {
                                            config.SetColTypes("rotxt,rotxt,rotxt,link,link,link,link,rotxt,rotxt,rotxt,rotxt,rotxt,ron," + //12
                                           "ron,combo,rotxt,,rotxt,rotxt,rotxt,rotxt,dhxCalendarA,rotxt,dhxCalendarA,rotxt,rotxt,rotxt,rotxt,rotxt," + //14
                                           "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," +//12
                                           "ron,rotxt,dhxCalendarA,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,ron,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt"); //15

                                            config.SetColColor(",,,,,,,,,,,,," + // 12
                                                               ",#d5f1ff,,,,,#d5f1ff,,#d5f1ff,,,,,," + //14
                                                               ",,,,,,,,,,," + //11
                                                               ",,#d5f1ff,,,,,,,,,,,,,,,,,");//15
                                        }

                                        else
                                        {
                                            //Gestion calidad para ogs y oms
                                            if (rol == "18" || rol == "35")
                                            {
                                                config.SetColTypes("rotxt,rotxt,rotxt,link,link,link,link,rotxt,rotxt,rotxt,rotxt,rotxt,ron," + //12
                                                "ron,combo,rotxt,rotxt,rotxt,dhxCalendarA,rotxt,dhxCalendarA,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," + //14
                                                "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," + //11
                                                "ron,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,ron,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt");//17
                                                //config.SetColTypes("rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,combo,ron,ron,ron,dhxCalendarA,dhxCalendarA,combo,edtxt,co,co,co,combo,dhxCalendarA,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,co,rotxt,rotxt,rotxt,rotxt,rotxt");
                                                config.SetColColor(",,,,,,,,,,,,," +//11
                                                                   ",#d5f1ff,,,,#d5f1ff,,#d5f1ff,,,,,,,," +//14
                                                                   ",,,,,,,,,,," +//12
                                                                   ",,,,,,,,,,,,,,,,,,,,"); //17   
                                            }

                                            else
                                            {

                                                config.SetColTypes("rotxt,rotxt,rotxt,link,link,link,link,rotxt,rotxt,rotxt,rotxt,rotxt,ron," +//12
                                                                   "ron,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," +//14
                                                                   "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," +
                                                                   "ron,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,ron,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt");
                                                //config.SetColTypes("rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,combo,ron,ron,ron,dhxCalendarA,dhxCalendarA,combo,edtxt,co,co,co,combo,dhxCalendarA,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,co,rotxt,rotxt,rotxt,rotxt,rotxt");
                                                config.SetColColor(",,,,,,,,,,,,," + //12
                                                                   ",,,,,,,,,,,,,," + //14
                                                                   ",,,,,,,,,,,,," +
                                                                   ",,,,,,,,,,,,,,,,,,");
                                            }
                                          }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
              }






                config.SetColAlign("left,left,left,left,left,left,left,right,right,left,left,left,right," +//12
                                   "right,left,left,left,right,right,right,right,right,right,left,left,right,left,left," +
                                   "left,left,left,left,left,left,left,right,right,left,left" +
                                   "left,left,left,left,left,left,left,left,left,left,left,left,left,left,left," +
                                   "right,left,left,left,left,left,left");
                //config.SetColSorting("dyn,str,date,date,center,str,center,center,center,center,center,center,center,center,str,str,center,connector,date,int,int,center,connector,center,date,date,connector,date,center,date,date");
                config.SetColSorting("str,str,str,str,str,str,str,str,date,date,date,date,int," + //12
                                     "int,str,str,str,date,date,date,date,date,date,str,str,str,str,str," + //12
                                     "str,str,str,str,str,str,date,date,str,date,date," +//10
                                     "str,str,date,str,str,str,str,str,str,str,str,str,str,str,int," +//12
                                     "str,date,date,str,str,str,str"); //5
                //config.SetColSorting("str,str,str,str,str,str,int,int,int,date,date,str,str,str,str,str,str,date,str, str,str,str,date,date,str, date, str, date, date, str");
                //config.SetColSorting("str,str,str,str,str,str,int,int,int,int,date,date,str,str,str,str,str,str,date,str,str,str,str,str,rotxt,co,rotxt,rotxt,rotxt,rotxt,rotxt");

                //config.SetColColor("x,x,x,x,x,x,x,x,x,x,x,x," + // 12
                //                       "x,x,x,x,x,x,x,x,x,x,x,x,x,x," + //14
                //                       "x,x,x,x,x,x,x,x,x,x,x," + //12
                //                       "x,x,x,x,x,x,x,x,x,x,x,x,x," + //14                                       
                //                       "x,x,x,x,x");  //5

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
                   string  mensajeVentana = "La sesion ha Expirado, por favor inicie sesión nuevamente. Gracias!";
                   ProcessRequest( context);
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
                    //string consulta = "EXECUTE USP_fup_UPD_ActaSeguimientoV3 '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}','{9}', " +
                    //"'{10}', '{11}','{12}', '{13}', '{14}', '{15}', '{16}','{17}','{18}','{19}" + form.ToString();
                    //Tools.EscapeQueryValue(id), Tools.EscapeQueryValue(form[id + "_c7"]), Tools.EscapeQueryValue(form[id + "_c8"]), Tools.EscapeQueryValue(form[id + "_c9"]),
                    //Tools.EscapeQueryValue(form[id + "_c12"]), Tools.EscapeQueryValue(form[id + "_c13"]), Tools.EscapeQueryValue(form[id + "_c14"]), Tools.EscapeQueryValue(form[id + "_c16"]),
                    //Tools.EscapeQueryValue(form[id + "_c18"]), Tools.EscapeQueryValue(form[id + "_c19"]), Tools.EscapeQueryValue(form[id + "_c20"]),
                    //Tools.EscapeQueryValue(form[id + "_c21"]), Tools.EscapeQueryValue(form[id + "_c22"]), Tools.EscapeQueryValue(form[id + "_c24"]), 
                    //Tools.EscapeQueryValue(form[id + "_c25"]), Tools.EscapeQueryValue(form[id + "_c26"]), Tools.EscapeQueryValue(form[id + "_c44"]), Tools.EscapeQueryValue(form[id + "_c47"]),
                    //Tools.EscapeQueryValue(form[id + "_c49"]), Tools.EscapeQueryValue(usu)));
                    

                    connector.Request.CustomSQLs.Add(CustomSQLType.Update,
                        //string.Format("EXECUTE UpdateEvents '{0}', '{1}', '{2}'",      
                    string.Format(
                    "EXECUTE USP_fup_UPD_ActaSeguimientoV3 '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}','{9}', " +
                    "'{10}', '{11}', '{12}','{13}', '{14}', '{15}', '{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}'",
                    Tools.EscapeQueryValue(id), Tools.EscapeQueryValue(form[id + "_c7"]), Tools.EscapeQueryValue(form[id + "_c8"]), Tools.EscapeQueryValue(form[id + "_c10"]),
                    Tools.EscapeQueryValue(form[id + "_c14"]), Tools.EscapeQueryValue(form[id + "_c17"]), Tools.EscapeQueryValue(form[id + "_c18"]), Tools.EscapeQueryValue(form[id + "_c20"]),
                    Tools.EscapeQueryValue(form[id + "_c22"]), Tools.EscapeQueryValue(form[id + "_c23"]), Tools.EscapeQueryValue(form[id + "_c24"]),
                    Tools.EscapeQueryValue(form[id + "_c25"]), Tools.EscapeQueryValue(form[id + "_c26"]), Tools.EscapeQueryValue(form[id + "_c28"]), 
                    Tools.EscapeQueryValue(form[id + "_c29"]), Tools.EscapeQueryValue(form[id + "_c30"]), Tools.EscapeQueryValue(form[id + "_c47"]), Tools.EscapeQueryValue(form[id + "_c49"]),
                    Tools.EscapeQueryValue(form[id + "_c53"]),
                    Tools.EscapeQueryValue(form[id + "_c55"]), Tools.EscapeQueryValue(form[id + "_c57"]), Tools.EscapeQueryValue(form[id + "_c58"]), Tools.EscapeQueryValue(form[id + "_c59"]), Tools.EscapeQueryValue(usu)));

                    string a = string.Format(
                    "EXECUTE USP_fup_UPD_ActaSeguimientoV3 '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}','{9}', " +
                      "'{10}', '{11}', '{12}','{13}', '{14}', '{15}', '{16}' ,'{17}','{18}','{19}','{20}','{21}','{22}','{23}",
                    Tools.EscapeQueryValue(id), Tools.EscapeQueryValue(form[id + "_c7"]), Tools.EscapeQueryValue(form[id + "_c8"]), Tools.EscapeQueryValue(form[id + "_c10"]),
                    Tools.EscapeQueryValue(form[id + "_c14"]), Tools.EscapeQueryValue(form[id + "_c17"]), Tools.EscapeQueryValue(form[id + "_c18"]), Tools.EscapeQueryValue(form[id + "_c20"]),
                    Tools.EscapeQueryValue(form[id + "_c22"]), Tools.EscapeQueryValue(form[id + "_c23"]), Tools.EscapeQueryValue(form[id + "_c24"]),
                    Tools.EscapeQueryValue(form[id + "_c25"]), Tools.EscapeQueryValue(form[id + "_c26"]), Tools.EscapeQueryValue(form[id + "_c28"]),
                    Tools.EscapeQueryValue(form[id + "_c29"]), Tools.EscapeQueryValue(form[id + "_c30"]), Tools.EscapeQueryValue(form[id + "_c47"]), Tools.EscapeQueryValue(form[id + "_c49"]), 
                    Tools.EscapeQueryValue(form[id + "_c53"]),
                    Tools.EscapeQueryValue(form[id + "_c55"]), Tools.EscapeQueryValue(form[id + "_c57"]), Tools.EscapeQueryValue(form[id + "_c58"]), Tools.EscapeQueryValue(form[id + "_c59"]), Tools.EscapeQueryValue(usu));
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