using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;
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

    public class GrillaPv : dhtmlxRequestHandler
    {
        public override IdhtmlxConnector CreateConnector(HttpContext context)
        {
            string vFecDesde = "", vFecHasta = "", rol = "", usu = "", trm = "";

            if (context.Request.QueryString["fecDesde"] != null)
                vFecDesde = context.Request.QueryString["fecDesde"];

            if (context.Request.QueryString["fecHasta"] != null)
                vFecHasta = context.Request.QueryString["fecHasta"];

            if (context.Request.QueryString["Rol"] != null)
                rol = context.Request.QueryString["Rol"];

            if (context.Request.QueryString["Usu"] != null)
                usu = context.Request.QueryString["Usu"];

            if (context.Request.QueryString["TRM"] != null)
                trm = context.Request.QueryString["TRM"];

            dhtmlxGridConnector connector = new dhtmlxGridConnector(
                string.Format("EXECUTE PvSeguimiento '" + vFecDesde + "', '" + vFecHasta + "'"),
                "pv_id",
                dhtmlxDatabaseAdapterType.SqlServer2005,
                BdDatos.conexionScope()
            );



            if (vFecDesde == "null")
            {
                //Configuración de la grilla 
                dhtmlxGridConfiguration config = new dhtmlxGridConfiguration();
                
                //config.SetColIds("fup,ver,fprogcomope,frealcomope,zona,estado,planta,paiszona,ciudad,vendedor,cliente,proyecto,tipo,material,probcierre,pago,planos,contrato,fecfact,m2,usdesw,formapago,motperd,mes,viscliente,fecdespplaneado,aprobdesp,fecdespreal,nofact,fecfact,fecestimllegobra");
                config.SetHeader("Fup/Orden,Cliente,Proyecto,Zona,Pais-Zona,Ciudad Obra,Prob,M2,Usd Exw,Valor Cop,Mes/Año,F.Com-Ope,Estado,Forma De Pago,Pago,Planos,Contrato,MotivoPerdida,MesPerdida,Tipo, Material,Vendedor,Planta,F.Visita,F.DespPlan, " +
                    "AprobDesp, F.DespReal, No.Factura, F.Factura, F.LlegObra, Zona Ciudad");
                //config.AttachHeader("&nbsp;,&nbsp;,&nbsp;,&nbsp;,#text_filter,#combo_filter,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;,&nbsp;");
                config.SetInitWidths("110,200,150,100,90,120,50,50,100,100,60,70,120,100,50,50,50,100,70,100,100,100,70,70,70,70,70,70,70,70,0");

                if (rol == "1")
                {
                    config.SetColTypes("rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,co,rotxt,edtxt,edtxt,dhxCalendarA,dhxCalendarA,co,edtxt,co,rotxt,co,co,rotxt,rotxt,rotxt,rotxt,rotxt,dhxCalendarA,dhxCalendarA,co,dhxCalendarA,edtxt,dhxCalendarA,dhxCalendarA,rotxt");
                }
                //Comercial
                if ((rol == "2") || (rol == "9") || (rol == "30"))
                {
                    config.SetColTypes("rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,combo,ron,ron,ron,dhxCalendarA,dhxCalendarA,combo,edtxt,co,co,co,combo,dhxCalendarA,rotxt,rotxt,rotxt,co,dhxCalendarA,dhxCalendarA,co,rotxt,edtxt,dhxCalendarA,dhxCalendarA,rotxt");
                    //config.SetColTypes("rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,combo,ron,ron,ron,dhxCalendarA,dhxCalendarA,combo,edtxt,co,co,co,combo,dhxCalendarA,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,co,rotxt,rotxt,rotxt,rotxt,rotxt");
                    config.SetColColor(",,,,,,#d5f1ff,,,,#d5f1ff,#d5f1ff,#d5f1ff,#d5f1ff,#d5f1ff,#d5f1ff,#d5f1ff,#d5f1ff,#d5f1ff,,,,#d5f1ff,#d5f1ff,#d5f1ff,#d5f1ff,,#d5f1ff,#d5f1ff,#d5f1ff");
                }
                //Planeacion Producción
                if ((rol == "36") || (rol == "13"))
                {
                    config.SetColTypes("rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,ron,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,co,dhxCalendarA,dhxCalendarA,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt");
                    config.SetColColor(",,,,,,,,,,,,,,,,,,,,,,#d5f1ff,#d5f1ff,#d5f1ff,,,,,");
                }
                //Logistica
                if (rol == "15")
                {
                    config.SetColTypes("rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,ron,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,edtxt,dhxCalendarA,dhxCalendarA,rotxt");
                    config.SetColColor(",,,,,,,,,,,,,,,,,,,,,,,,,,,#d5f1ff,#d5f1ff,#d5f1ff");
                }
                //Agentes Comerciales
                if (rol == "3")
                {
                    config.SetColTypes("rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,combo,rotxt,ron,ron,dhxCalendarA,rotxt,co,rotxt,rotxt,rotxt,rotxt,co,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt");
                    config.SetColColor(",,,,,,#d5f1ff,,,,#d5f1ff,,,,,,,,,,,,,,,,,,,,");
                }

                config.SetColAlign("left,left,left,left,left,left,right,right,right,right,left,left,left,left,left,left,left,right,left, left,left,left,rigth,right,right,right,left,right,right,left");
                //config.SetColSorting("dyn,str,date,date,center,str,center,center,center,center,center,center,center,center,str,str,center,connector,date,int,int,center,connector,center,date,date,connector,date,center,date,date");
                config.SetColSorting("str,str,str,str,str,str,int,int,int,int,date,date,str,str,str,str,str,str,date,str,str,str,str,date,date,str,date,str,date,date,str");
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

                if (type == ActionType.Updated)
                {
                    connector.Request.CustomSQLs.Add(CustomSQLType.Update,
                        //string.Format("EXECUTE UpdateEvents '{0}', '{1}', '{2}'",
                       string.Format("EXECUTE USP_fup_UPD_ActaSeguimientoV2 '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', " +
                       "'{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}'",
                           Tools.EscapeQueryValue(id), Tools.EscapeQueryValue(form[id + "_c11"]), Tools.EscapeQueryValue(form[id + "_c12"]),
                           Tools.EscapeQueryValue(form[id + "_c6"]), Tools.EscapeQueryValue(form[id + "_c14"]), Tools.EscapeQueryValue(form[id + "_c16"]),
                           Tools.EscapeQueryValue(form[id + "_c10"]), Tools.EscapeQueryValue(form[id + "_c13"]), Tools.EscapeQueryValue(form[id + "_c17"]),
                           Tools.EscapeQueryValue(form[id + "_c23"]), Tools.EscapeQueryValue(form[id + "_c24"]), Tools.EscapeQueryValue(form[id + "_c25"]),
                           Tools.EscapeQueryValue(form[id + "_c27"]), Tools.EscapeQueryValue(form[id + "_c28"]), Tools.EscapeQueryValue(form[id + "_c29"]),
                           Tools.EscapeQueryValue(usu), Tools.EscapeQueryValue(form[id + "_c22"]),
                           Tools.EscapeQueryValue(form[id + "_c18"])));
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