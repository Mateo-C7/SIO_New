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
    /// Summary description for GrillaContenidoDft
    /// </summary>
    /// 
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    public class GrillaContenidoDft : dhtmlxRequestHandler
    {
        ControlMaestroItemPlanta cmIp = new ControlMaestroItemPlanta();
        public Gn Gn = new Gn();

        public override IdhtmlxConnector CreateConnector(HttpContext context)
        {
            string rol = "", usu = "";
            string mensajeVentana = "";

            if (context.Request.QueryString["Rol"] != null)
                rol = context.Request.QueryString["Rol"];

            if (context.Request.QueryString["Usu"] != null)
                usu = context.Request.QueryString["Usu"];



            dhtmlxGridConnector connector = new dhtmlxGridConnector(
               string.Format("EXECUTE USP_fup_SEL_ActaSeguimientoV3_Dft"),
                "eect_id",
                dhtmlxDatabaseAdapterType.SqlServer2005,
                BdDatos.conexionScope()
            );

            //Configure the  header of the gridview
            dhtmlxGridConfiguration config = new dhtmlxGridConfiguration();
            config.SetHeader("Orden/Fup,Cliente,Proyecto,Pais,Ciudad_Cliente,Tipo_Cotizacion,Material,Prob,MesFac,MesProdCom," +//10
                                "MesProd,Obs,Sist_Seg,Escalera,M2_Cot,Resp_Cotización,F.SolDft,#ModuCambios,RespDft,#Tipologias," +//10
                                "#Detalles,F.InicioDft,F.EntFinalDft,F.EntregaProgram,F.PropReu,F.RealEntDft,"+ //25
                                "F.RespuCliente,F.ProgrRespu,F.RealEntrRespu," +
                                "EstadoDft,F.AprobDft,AlistamientoApc,RespApc," +//10
                                "F.InicioApc,F.EntreFinalApc,EstadoFup,Zona,Subzona"); // campos

            //Establishes the width of the columns
            config.SetInitWidths("100,150,150,100,100,70,70,40,60,60," +
                                 "60,30,60,60,70,90,80,90,120,60," +
                                 "80,70,80,80,70,70,85,85,85,70,70,90,120," +
                                 "70,90,80,80,80");

            ////Configure the date type of the columns
            //config.SetColTypes("rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt," +
            //                  "rotxt,rotxt,rotxt,rotxt,rotxt,ron,ron,ron,ron,edtxt,rotxt," + //el ultimo es repcotiza
            //                  "rotxt,edtxt,edtxt,edtxt,edtxt,edtxt,rotxt,dhxCalendarA,dhxCalendarA,dhxCalendarA,dhxCalendarA," +
            //                  "rotxt,rotxt,rotxt,combo");

            //Configure the date type of the columns
            config.SetColTypes("link,rotxt,link,rotxt,rotxt,link,rotxt,rotxt,rotxt,rotxt,"+
                                "rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,combo,edtxt," +
                                "edtxt,dhxCalendarA,dhxCalendarA,dhxCalendarA,dhxCalendarA,dhxCalendarA,dhxCalendarA,dhxCalendarA,dhxCalendarA,rotxt,rotxt,combo,combo," +
                                "dhxCalendarA,dhxCalendarA,rotxt,rotxt,rotxt");

            //Establishes the color of the columns
            config.SetColColor(",,,,,,,,,"+
                                ",,,,,,,,,#d5f1ff," +
                                "#d5f1ff,#d5f1ff,#d5f1ff,#d5f1ff,#d5f1ff,#d5f1ff,#d5f1ff,#d5f1ff,#d5f1ff,#d5f1ff,,," +
                                "#d5f1ff,#d5f1ff,#d5f1ff,#d5f1ff,,,");

            //Configure the alingment of the columns
            config.SetColAlign("left,left,left,left,left,left,left,left,left,center," +
                               "left,center,left,left,right,left,right,left,left,right," +
                               "right,center,left,center,left,left,left,left,left,left,left,left,left," +
                               "left,left,left,left,left");

            config.SetColSorting("str,str,str,str,str,str,str,str,str,str," +
                                 "str,str,str,str,ron,int,int,int,int,int," +
                                 "int,date,str,str,str,str,str,str,str,str,str,str,int," +
                                 "date,date,str,str,str");

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
                        

                        connector.Request.CustomSQLs.Add(CustomSQLType.Update,
                    //string.Format("EXECUTE UpdateEvents '{0}', '{1}', '{2}'",      
                    string.Format(
                    "EXECUTE USP_fup_UPD_ActaSeguimientoV3_Dft '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}','{9}', " +
                    "'{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}'",
                    Tools.EscapeQueryValue(id), Tools.EscapeQueryValue(form[id + "_c18"]), Tools.EscapeQueryValue(form[id + "_c19"]),
                    Tools.EscapeQueryValue(form[id + "_c20"]),
                    Tools.EscapeQueryValue(form[id + "_c21"]), Tools.EscapeQueryValue(form[id + "_c22"]),
                    Tools.EscapeQueryValue(form[id + "_c23"]), Tools.EscapeQueryValue(form[id + "_c24"]), Tools.EscapeQueryValue(form[id + "_c31"]),
                    Tools.EscapeQueryValue(form[id + "_c32"]), Tools.EscapeQueryValue(form[id + "_c33"]), Tools.EscapeQueryValue(form[id + "_c34"]),
                    Tools.EscapeQueryValue(form[id + "_c25"]), Tools.EscapeQueryValue(form[id + "_c26"]), Tools.EscapeQueryValue(form[id + "_c27"]),
                    Tools.EscapeQueryValue(form[id + "_c28"]),
                    Tools.EscapeQueryValue(usu)));


                        a = string.Format(
                        "EXECUTE USP_fup_UPD_ActaSeguimientoV3_Dft '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}','{9}', " +
                    "'{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}'",
                    Tools.EscapeQueryValue(id), Tools.EscapeQueryValue(form[id + "_c18"]), Tools.EscapeQueryValue(form[id + "_c19"]),
                    Tools.EscapeQueryValue(form[id + "_c20"]),
                    Tools.EscapeQueryValue(form[id + "_c21"]), Tools.EscapeQueryValue(form[id + "_c22"]),
                    Tools.EscapeQueryValue(form[id + "_c23"]), Tools.EscapeQueryValue(form[id + "_c24"]), Tools.EscapeQueryValue(form[id + "_c31"]),
                    Tools.EscapeQueryValue(form[id + "_c32"]), Tools.EscapeQueryValue(form[id + "_c33"]), Tools.EscapeQueryValue(form[id + "_c34"]),
                    Tools.EscapeQueryValue(form[id + "_c25"]), Tools.EscapeQueryValue(form[id + "_c26"]), Tools.EscapeQueryValue(form[id + "_c27"]),
                    Tools.EscapeQueryValue(form[id + "_c28"]),
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