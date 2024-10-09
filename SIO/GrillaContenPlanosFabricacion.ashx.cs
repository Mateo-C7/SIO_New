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
using System.Data.SqlClient;
using System.Data;
using CapaControl;

namespace SIO
{
    /// <summary>
    /// Summary description for GrillaContenPlanosFabricacion
    /// </summary>
    /// 


    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]


    public class GrillaContenPlanosFabricacion : dhtmlxRequestHandler
    {

        public Gn Gn = new Gn();

        public override IdhtmlxConnector CreateConnector(HttpContext context)
        {

            string rol = "", usu = "";

            if (context.Request.QueryString["Rol"] != null)
                rol = context.Request.QueryString["Rol"];

            if (context.Request.QueryString["Usu"] != null)
                usu = context.Request.QueryString["Usu"];


            dhtmlxGridConnector connector = new dhtmlxGridConnector(
               string.Format("EXECUTE USP_Seg_Plan_PlanosFabricacion"),
                "Id_Ofa",
                dhtmlxDatabaseAdapterType.SqlServer2005,
                BdDatos.conexionScope()
            );

            //Configure the  header of the gridview
            dhtmlxGridConfiguration config = new dhtmlxGridConfiguration();
            config.SetHeader("OF-FP,Proyecto,Pais,Cliente,Tipo_Cotización,Modulador,Proyectista,Raya,Planos,M2, " + //posicion final 10
                            " Fech_Inicio,Fech_Entre_Plan,Complejidad,Ordenes_Ref,Fech_Validación,Fech_Despacho, " + //posicion final 6
                            " Observaciones,Fech_Entrega,Estado,Explosionado,Comentario"); //posicion final 5

            //Establishes the width of the columns
            config.SetInitWidths("100,150,100,150,100,150,150,50,50,50," +
                                "70,90,90,100,90,90," +
                                "150,90,150,70,150");
         
            //Configure the date type of the columns
            config.SetColTypes("rotxt,rotxt,rotxt,rotxt,rotxt,rotxt,edtxt,rotxt,rotxt,rotxt," +
                               "rotxt,dhxCalendarA,combo,rotxt,rotxt,rotxt," + //el ultimo es repcotiza
                              "rotxt,rotxt,combo,rotxt,edtxt");

            //Establishes the color of the columns
            config.SetColColor(",,,,,,#d5f1ff,,,," +
                              ",#d5f1ff,#d5f1ff,,,," +
                              ",,#d5f1ff,,#d5f1ff");

            //Configure the alingment of the columns
            config.SetColAlign("left,left,left,left,left,left,left,right,right,right, " +
                               "left,left,left,left,right,right, " +
                               "left,left,left,left,left");

            config.SetColSorting("str,str,str,str,str,str,str,int,int,int" +
                                 "date,date,str,str,date,date," +
                                 "str,date,str,str,str");


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

                    if (Tools.EscapeQueryValue(form[id + "_c6"]) == null)
                        try
                        {
                            string nombre = Tools.EscapeQueryValue(form[id + "_c6"]);
                        }
                        catch
                        {
                            string mensajeVentana = "La sesion ha Expirado, por favor inicie sesión nuevamente. Gracias!";
                            ProcessRequest(context);
                            //cerrarSesion();
                        }

                    if (type == ActionType.Updated)
                    {
                        string consulta = "EXECUTE Update_Insert_Seg_Planos_Fabricacion '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}'" + form.ToString();

                        string est = Tools.EscapeQueryValue(form[id + "_c18"]);
                        if (est == "Pend Por Asignar")
                        {
                            est = "1";
                        }
                        else if (est == "En Proceso")
                        {
                            est = "2";
                        }
                        else if (est == "Entregado A Producción")
                        {
                            est = "3";
                        }
                        else if (est == "En Pausa")
                        {
                            est = "4";
                        }


                        connector.Request.CustomSQLs.Add(CustomSQLType.Update,
                        //string.Format("EXECUTE UpdateEvents '{0}', '{1}', '{2}'",      
                        string.Format(
                        "EXECUTE Update_Insert_Seg_Planos_Fabricacion '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}'",
                        Tools.EscapeQueryValue(id), Tools.EscapeQueryValue(form[id + "_c6"]), Tools.EscapeQueryValue(form[id + "_c11"]),
                        Tools.EscapeQueryValue(form[id + "_c12"]), est, Tools.EscapeQueryValue(form[id + "_c20"]),                       
                        Tools.EscapeQueryValue(usu)));


                        a = string.Format(
                        "EXECUTE Update_Insert_Seg_Planos_Fabricacion '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}'",
                        Tools.EscapeQueryValue(id), Tools.EscapeQueryValue(form[id + "_c6"]), Tools.EscapeQueryValue(form[id + "_c11"]),
                        Tools.EscapeQueryValue(form[id + "_c12"]), est, Tools.EscapeQueryValue(form[id + "_c20"]),
                        Tools.EscapeQueryValue(usu));
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
                    Gn.proErrorException("Planeador Planos de Fabricación", e, a);
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