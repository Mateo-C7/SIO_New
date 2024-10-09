using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using CapaDatos;


namespace CapaControl
{
    public class ControlMetas
    {

        CapaDatos.BdDatos objconex = new BdDatos();
        // metodo para visualizar en una grilla los datos maestros de la tabla metas_produccion 

        public DataSet Consultar_Maestro_Metas_x_Planta(int planta)
        {
            string strconsulta1 = "select m.Mepr_Id, m.id_Proceso, p.proceso as proceso ,m.Mepr_Observacion  from meta_produccion m inner join Proceso p on p.Id_Proceso = m.Id_Proceso where Plan_Prod_Id = " + planta.ToString() + " order by Mepr_Id Desc ";

            return BdDatos.consultarConDataset(strconsulta1);
        }

        // Eliminar todo el detalle meta de una meta
        public int Met_Eliminar_TodoDetalleMeta_Produccion(int meprid)
        {
            string sqlelidetmetaprod = "delete from MetaProd_Detalle  where Mepr_Id=" + meprid.ToString() + "";
            return BdDatos.ejecutarSql(sqlelidetmetaprod);
        }

        public DataSet Consultar_Maestro_Metas()

        {
            string strconsulta3 = "select m.Mepr_Id, m.id_Proceso, p.proceso as proceso ,m.Mepr_Observacion from meta_produccion m inner join Proceso p on p.Id_Proceso = m.Id_Proceso";

            return BdDatos.consultarConDataset(strconsulta3);
        }

        //metodo para crear metas de produccion
        public int Met_Agregar_MetaProduccion(string Observ, int idPlanta, int idProceso)
        {
            string sqlIngMet = "insert into Meta_Produccion values('" + Observ + "'," + idPlanta.ToString() + "," + idProceso.ToString() + ")";

            return BdDatos.ejecutarSql(sqlIngMet);
        }

        public int Met_Agregar_DetalleMetas(int mepr, string fecini, string fehfin, int numopr,
                                            int unid, float meta, float toleca, int pieza, int frecuinspe, string usumod)
        {
            string sqlIngDetMet = "INSERT INTO MetaProd_Detalle VALUES(" + mepr.ToString() + ",'" + fecini + "','" + fehfin + "', " +
                                                                     " " + numopr.ToString() + "," + unid.ToString() + "," + meta.ToString() + ", " +
                                                                     " " + toleca.ToString() + "," + pieza.ToString() + ","+frecuinspe.ToString()+",'"+usumod+"')";
            return BdDatos.ejecutarSql(sqlIngDetMet);
        }

        public DataTable Consultar_Maestro_Metas_Prueba(int planta)
        {
            string strconsulta3 = "select mp.Mepr_Id, mp.Id_Proceso as Idproceso ,p.Proceso ,mp.Mepr_Observacion  from meta_produccion mp inner join proceso p on mp.id_proceso=p.Id_Proceso where plan_Prod_Id=" + planta.ToString() + " order by Mepr_Id Desc";

            return BdDatos.CargarTabla(strconsulta3);
        }

        //metodo para actualizar en la tabla Meta_Produccion
        public int Proc_Actualizar_Meta_Produccion(int id, string observ, int idProc)
        {

            string stractmetaprod = "update Meta_Produccion set Mepr_Observacion='" + observ + "', Id_Proceso='" + idProc.ToString() + "' where Mepr_Id='" + id.ToString() + "'";

            return BdDatos.ejecutarSql(stractmetaprod);
        }

        //metodo para actualizar en la tabla MetaProd_Detalle
        public int Proc_Actualizar_MetaProd_Detalle(string fechini, string fechfin, int numeoper, int unid, float meta, float tolecali, int tpopza, int idMetaDeta, int frecInspe,string usumod)
        {
            string stractdemetaprod = "UPDATE MetaProd_Detalle SET  Mpde_FechIni= '" + fechini + "',Mpde_FechFin='" + fechfin + "', " +
                                                                  " Mpde_NumOper=" + numeoper.ToString() + " ,Mpde_Unidades=" + unid.ToString() + ", " +
                                                                  " Mpde_Meta=" + meta.ToString() + " ,Mpde_ToleCali=" + tolecali.ToString() + ", " +
                                                                  " TpoPza_id=" + tpopza.ToString() + ",Mpde_FrInCali="+frecInspe.ToString()+ ", " +
                                                                  " Mpde_Usu_Mod='" + usumod + "' " +
                                                                  " WHERE Mpde_Id=" + idMetaDeta.ToString() + "";
            return BdDatos.ejecutarSql(stractdemetaprod);
        }

        //metodo para consultar el detalle_Maestro 
        public DataSet Proc_Consultar_Detalle_MetasProduccion(int Mepr_Id)
        {
            string strcondetametaprod = "SELECT Mpde_Id ,Mpde_FechIni  as FechIni ,Mpde_FechFin, Mpde_NumOper, " + 
                                               " Mpde_Unidades , Mpde_Meta , Mpde_ToleCali , mp.TpoPza_id as pieza, " + 
                                               " Mepr_Id ,tp.TpoPza_dscrpcion, Mpde_FrInCali " +
                                         " FROM MetaProd_Detalle mp INNER JOIN tipo_pieza Tp ON mp.TpoPza_id=Tp.TpoPza_id " +
                                         " WHERE Mepr_Id = " + Mepr_Id.ToString() + " ORDER BY Mpde_Id DESC ";

            return BdDatos.consultarConDataset(strcondetametaprod);
        }


        public DataTable Proc_Consultar_Detalle_MetasProduccion_prueba(int meprid)
        {
            string strcondetametaprod = "SELECT Mpde_Id, Mpde_FechIni  as FechIni ,Mpde_FechFin, Mpde_NumOper,Mpde_Unidades, " +
                                              " Mpde_Meta , Mpde_ToleCali , mp.TpoPza_id as pieza,  Mepr_Id ,tp.TpoPza_dscrpcion,Mpde_FrInCali  " +
                                        " FROM MetaProd_Detalle mp INNER JOIN tipo_pieza Tp ON mp.TpoPza_id=Tp.TpoPza_id " + 
                                        " WHERE  Mepr_Id =" + meprid.ToString() + "  ORDER BY Mpde_Id Desc ";

            return BdDatos.CargarTabla2(strcondetametaprod);
        }

        //metodo para consultar el detalle_Maestro 
        public DataSet Proc_Consultar_Detalle_MetasProduccion_()
        {
            string strcondetametaprod = "select  Mpde_Id,Mpde_FechIni as FechIni ,Mpde_FechFin  ,Mpde_NumOper ,Mpde_Unidades ,Mpde_Meta ,Mpde_ToleCali , TpoPza_id,Mepr_Id from MetaProd_Detalle";

            return BdDatos.consultarConDataset(strcondetametaprod);
        }

        public DataTable Met_Obtener_Descripcion_Proceso(int meprId)
        {
            string sqlobtdespro = "select Mepr_Observacion from Meta_Produccion where Mepr_Id=" + meprId.ToString() + "";

            return BdDatos.CargarTabla(sqlobtdespro);
        }

        //obtiene los procesos de planta para establecerlos en un combo
        public DataSet ObtenerProcesoPlanta()
        {
            string sqlobtpropla = "select id_proceso , proceso from proceso where Proceso_Activo = 1 and proc_are_id =18 or proc_are_id =10";

            return BdDatos.consultarConDataset(sqlobtpropla);
        }

        //devuelve el meprId maximo de la table  y filtrado por la planta 1
        public DataTable Met_Obtener_Max_MeprId(int planta)
        {
            string sqlobtmep = "select max(mepr_id) from Meta_Produccion where plan_Prod_Id =" + planta.ToString() + "";

            return BdDatos.CargarTabla(sqlobtmep);
        }

        //Elimina una meta de produccion
        public int Met_Eliminar_Meta_Produccion(int meprId)
        {
            string sqlelimetaprod = "delete from Meta_Produccion where Mepr_Id=" + meprId.ToString() + "";

            return BdDatos.ejecutarSql(sqlelimetaprod);
        }

        // Eliminar detalle meta produccion
        public int Met_Eliminar_DetalleMeta_Produccion(int mpdeid)
        {
            string sqlelidetmetaprod = "delete from MetaProd_Detalle  where Mpde_Id=" + mpdeid.ToString() + "";

            return BdDatos.ejecutarSql(sqlelidetmetaprod);
        }

        /* -------------------METODOS PARA EL REGISTRO DE LOG------------------
         ======================================================================
         ======================================================================  
        */
        //Insertar el log update de la tabla metas
        public int Met_Insertar_Log_Update_Metas(string tabla, int idpais, string campo, string usuario, string fecha, string valor, string valorNuevo, string evento)
        {
            string sqlinsert = "insert into Log_transacciones values ('" + tabla + "'," + idpais.ToString() + ",'" + campo + "','" + usuario + "','" + fecha + "','" + valor + "','" + valorNuevo + "','" + evento + "')";

            return BdDatos.ejecutarSql(sqlinsert);
        }
        //Insertar el log delete de la tabla metas
        public int Met_Insertar_Log_Delete_Metas(string tabla, int meprid, string usuario, string fecha, string evento)
        {
            string sqlinsertlogdel = "insert into Log_transacciones  (genlog_tabla,genlog_id_registro,genlog_usuario,genlog_fecha,genlog_Evento) values('" + tabla + "'," + meprid.ToString() + ",'" + usuario + "','" + fecha + "','" + evento + "')";

            return BdDatos.ejecutarSql(sqlinsertlogdel);
        }

        /*consulta el detalle de una meta para despues
      verificar con los datos ingresados por el usuario*/
        public DataTable Met_Consultar_Metas_log(int meprid)
        {
            string sqlconmeta = "select Mepr_Id,Mepr_Observacion,Plan_Prod_Id,Id_Proceso from Meta_Produccion where Mepr_Id=" + meprid.ToString() + "";
            return BdDatos.CargarTabla(sqlconmeta);
        }
        //consulta el detalle de una meta para  realizar el log
        public DataTable Met_Consultar_Detalle_Metas_log(int mpdid)
        {
            string sqlcondetametalog = "select  Mpde_Id,Mpde_FechIni as FechIni ,Mpde_FechFin ,Mpde_NumOper ,Mpde_Unidades ,Mpde_Meta ,Mpde_ToleCali , TpoPza_id,Mepr_Id from MetaProd_Detalle where Mpde_Id=" + mpdid.ToString() + "";
            return BdDatos.CargarTabla(sqlcondetametalog);
        }
        /* ======================================================================
        ====================================================================== */
        //prueba
        public int Ejecutar_Proc_ActualizarDetaMeta(string fechini, string fechfin, int numeoper, int unid, float meta, float tolecali, int tpopza, int idMetaDeta, string usu)
        {
            string sql = " EXEC Proc_Actualizar_DetalleMeta'" + fechini + "','" + fechfin + "'," + numeoper.ToString() + " ," + unid.ToString() + "," + meta.ToString() + " ," + tolecali.ToString() + " ," + tpopza.ToString() + ","+idMetaDeta.ToString()+",'"+usu+"'";
            
            return BdDatos.ejecutarSql(sql);
        }


        //Obtiene el ultimo detalle meta produccion generado
        public DataTable Obtener_Ultimo_DetalleMeta()
        {
            string sql = "SELECT Mpde_NumOper,Mpde_Unidades," +
                         "  Mpde_Meta,Mpde_ToleCali,TpoPza_id,Mpde_FrInCali " +
                         "  FROM MetaProd_Detalle " +
                         "  WHERE Mpde_Id = (SELECT MAX(Mpde_Id) FROM MetaProd_Detalle)";
            return BdDatos.CargarTabla(sql);
        }
    }
}