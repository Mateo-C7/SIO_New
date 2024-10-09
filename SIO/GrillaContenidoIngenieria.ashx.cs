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
using CapaControl;
using System.Diagnostics;

namespace SIO
{
    /// <summary>
    /// Summary description for GrillaContenidoIngenieria
    /// </summary>
    /// 
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    public class GrillaContenidoIngenieria : dhtmlxRequestHandler
    {
        ControlMaestroItemPlanta cmIp = new ControlMaestroItemPlanta();
        public Gn Gn = new Gn();

        public override IdhtmlxConnector CreateConnector(HttpContext context)
        {
            string rol = "", usu = "";
            string mensajeVentana="";

            if (context.Request.QueryString["Rol"] != null)
                rol = context.Request.QueryString["Rol"];

            if (context.Request.QueryString["Usu"] != null)
                usu = context.Request.QueryString["Usu"];

       

            dhtmlxGridConnector connector = new dhtmlxGridConnector(
               string.Format("EXECUTE USP_Seg_Planeacion_Ingenieria"),
                "actseg_id",
                dhtmlxDatabaseAdapterType.SqlServer2005,
                BdDatos.conexionScope()
            );

            //Configure the  header of the gridview
            dhtmlxGridConfiguration config = new dhtmlxGridConfiguration();
            config.SetHeader("Planta,Orden/Fup,Cliente,Proyecto,Pais,Ciudad_Cliente,Tipo_Cotizacion,Material,Prob,MesFac,F.EntEXW,Obs," + // campos del 1 al 12
                             "Sist_Seg,Escalera,Acc_Nv2,Acc_Nv3,Acc_Nv4,M2_Cot,M2_Ent,M2_Desv,M2_Pend,Ton.AccPlan,Ton.AccAlm," + // campos del 12 al 23
                             "Ton.AccTotal,.,Resp_Cotización,Ptf,Resp_Ptf,Resp_Modulación,Resp_Escalera,Resp_PlanoFabric,Resp_Acc,#Mod_#Cambios,F.ReciboInfo," + // campos del 22 al 31
                             "F.InicioMod,F.EntreFinAcc,F.EntreFinAlum,F.EntreFinal,VisitaCliente,F.EntrePlaneada,F.PlanDespacho,%AvanceProyecto,Estado_Operación,Mes_Producción,Zona," +  // campos del 32 al 41
                             "ZonaCiudad,Mes_Ingenieria,Coordinador,Mes_EntregaCoor"); // campos del 42 al 46

            //Establishes the width of the columns
            config.SetInitWidths("90,110,150,150,100,90,90,60,40,60,63,40," +
                                 "80,80,60,60,60,80,80,80,80,80," +
                                 "80,80,80,100,30,100,100,100,100,100,100," +
                                 "80,80,80,80,80,80,80,80,80,100,80," +
                                 "0,0,90,100,100");

            ////Configure the date type of the columns
            //config.SetColTypes("rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," +
            //                  "rotxt,rotxt,rotxt,rotxt,rotxt,ron,ron,ron,ron,edtxt,rotxt," + //el ultimo es repcotiza
            //                  "rotxt,edtxt,edtxt,edtxt,edtxt,edtxt,rotxt,dhxCalendarA,dhxCalendarA,dhxCalendarA,dhxCalendarA," +
            //                  "rotxt,rotxt,rotxt,combo");

            //Configure the date type of the columns
            config.SetColTypes("rotxt,link,rotxt,link,rotxt,rotxt,link,rotxt,rotxt,rotxt,rotxt,rotxt," +
                               "rotxt,rotxt,rotxt,rotxt,rotxt,ron,ron,ron,ron,rotxt,rotxt," +
                               "rotxt,dhxCalendarA,rotxt,rotxt,rotxt,edtxt,edtxt,edtxt,edtxt,rotxt,rotxt," +
                               "dhxCalendarA,dhxCalendarA,dhxCalendarA,rotxt,dhxCalendarA,dhxCalendarA,rotxt,rotxt,combo,rotxt,rotxt," +
                               "rotxt,dhxCalendarA,rotxt,dhxCalendarA");

            //Establishes the color of the columns
            config.SetColColor(",,,,,,,,,,,," +
                               ",,,,,,,,,,," +
                               ",#d5f1ff,,,,#d5f1ff,#d5f1ff,#d5f1ff,#d5f1ff,,," +
                               "#d5f1ff,#d5f1ff,#d5f1ff,,,#d5f1ff,#d5f1ff,,#d5f1ff,,," +
                               ",#d5f1ff,,#d5f1ff");

            //Configure the alingment of the columns
            config.SetColAlign("left,left,left,left,left,left,left,left,left,center,center,center," +
                               "left,left,left,left,left,right,right,right,right,right,right," +
                               "right,center,left,center,left,left,left,left,left,right,center," +
                               "center,center,center,center,center,center,center,center,right,left,center," +
                               "left,left,center,center");

            config.SetColSorting("str,str,str,str,str,str,str,str,str,str,str,str," +
                                 "str,str,str,str,str,int,int,int,int,int,int," +
                                 "int,date,str,str,str,str,str,str,str,int,date," +
                                 "date,date,date,date,date,date,date,str,int,date,str," +
                                 "str,date,str,date");

            connector.SetConfig(config);

            //to perform create/update/delete operations you need to define custom handlers
            //and parse request data for parameters
            var form = context.Request.Form;
            string a = "";
            if (form["ids"] != null)
            {
                try
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
                        mensajeVentana = "La sesion ha Expirado, por favor inicie sesión nuevamente. Gracias!";
                        ProcessRequest(context);
                        //cerrarSesion();
                    }

               if (type == ActionType.Updated)
                {
                    string consulta = "EXECUTE USP_fup_UPD_PlanIngenieria '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}','{9}', " +
                  "'{10}', '{11}', '{12}', '{13}', '{14}', '{15}','{16}'" + form.ToString();               

                    string est = Tools.EscapeQueryValue(form[id + "_c42"]);
                    if (est == "Ingeni/ Pend Asignar")
                    {
                        est = "1";
                    }
                    else if (est == "Ingeni/ En Proceso")
                    {
                        est = "2";
                    }
                    else if (est == "Entregado Producción")
                    {
                        est = "3";
                    }
                    else if (est == "En Pausa")
                    {
                        est = "4";
                    }
                    else if (est == "Ingeni/")
                    {
                        est = "5";
                    }

                        connector.Request.CustomSQLs.Add(CustomSQLType.Update,
                    //string.Format("EXECUTE UpdateEvents '{0}', '{1}', '{2}'",      
                    string.Format(
                    "EXECUTE USP_fup_UPD_PlanIngenieria '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}','{9}', " +
                    "'{10}', '{11}', '{12}', '{13}', '{14}', '{15}','{16}'",
                    Tools.EscapeQueryValue(id), Tools.EscapeQueryValue(form[id + "_c24"]), Tools.EscapeQueryValue(form[id + "_c27"]),
                    Tools.EscapeQueryValue(form[id + "_c28"]), Tools.EscapeQueryValue(form[id + "_c29"]), Tools.EscapeQueryValue(form[id + "_c30"]),
                    Tools.EscapeQueryValue(form[id + "_c31"]), Tools.EscapeQueryValue(form[id + "_c33"]),
                    Tools.EscapeQueryValue(form[id + "_c34"]), Tools.EscapeQueryValue(form[id + "_c35"]), Tools.EscapeQueryValue(form[id + "_c36"]),
                    Tools.EscapeQueryValue(form[id + "_c37"]), Tools.EscapeQueryValue(form[id + "_c39"]), est, Tools.EscapeQueryValue(form[id + "_c46"]),
                    Tools.EscapeQueryValue(form[id + "_c48"]),
                    Tools.EscapeQueryValue(usu)));


                     a = string.Format(
                    "EXECUTE USP_fup_UPD_PlanIngenieria '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}','{9}', " +
                    "'{10}', '{11}', '{12}', '{13}', '{14}', '{15}','{16}'",
                Tools.EscapeQueryValue(id), Tools.EscapeQueryValue(form[id + "_c24"]), Tools.EscapeQueryValue(form[id + "_c27"]),
                    Tools.EscapeQueryValue(form[id + "_c28"]), Tools.EscapeQueryValue(form[id + "_c29"]), Tools.EscapeQueryValue(form[id + "_c30"]),
                    Tools.EscapeQueryValue(form[id + "_c31"]), Tools.EscapeQueryValue(form[id + "_c33"]),
                    Tools.EscapeQueryValue(form[id + "_c34"]), Tools.EscapeQueryValue(form[id + "_c35"]), Tools.EscapeQueryValue(form[id + "_c36"]),
                    Tools.EscapeQueryValue(form[id + "_c37"]), Tools.EscapeQueryValue(form[id + "_c39"]), est, Tools.EscapeQueryValue(form[id + "_c46"]),
                    Tools.EscapeQueryValue(form[id + "_c48"]),
                    Tools.EscapeQueryValue(usu));

                        string Msj;
                        System.Threading.Thread.Sleep(1000);

                        int actseg_id = Convert.ToInt32(Tools.EscapeQueryValue(id));
                        string fecEntrega_Fin_Acc = Tools.EscapeQueryValue(form[id + "_c35"]);
                        string fecEntrega_Fin_Alum = Tools.EscapeQueryValue(form[id + "_c36"]);
                        string fecEntrega_Final = Tools.EscapeQueryValue(form[id + "_c37"]);


                        if (String.IsNullOrEmpty(fecEntrega_Final))
                        {
                            if (!String.IsNullOrEmpty(fecEntrega_Fin_Acc) || !String.IsNullOrEmpty(fecEntrega_Fin_Alum))
                            {
                                //Se envia el correo 
                                cmIp.enviarCorreoPrueba(actseg_id, out Msj, fecEntrega_Fin_Acc, fecEntrega_Fin_Alum); // item fue aprobado
                                if (string.IsNullOrEmpty(Msj))
                                {
                                    Debug.WriteLine("Correo Fue enviado");
                                }
                                else
                                {
                                    Debug.WriteLine("Correo no fue enviado");
                                }
                                try
                                {
                                    //string nombre = Session["Nombre_Usuario"].ToString();
                                    //string usuario = Session["Usuario"].ToString();
                                }
                                catch
                                {
                                    mensajeVentana = "La sesion ha Expirado, por favor inicie sesión nuevamente. Gracias!";

                                }

                            }
                        }

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
                catch (Exception e)
            {//Manejo de excepcion               
                Gn.proErrorException("Planeador Ingenieria", e, a);
                return null;
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