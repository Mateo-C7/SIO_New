using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CapaDatos;
using System.Web.UI.WebControls;
using CapaControl.Entity;
namespace CapaControl
{
    public class ControlOrden
    {

        public DataTable Validar_existe_Orden(string orden)
        {
            string sql = "SELECT TOP (1) REPLACE(Num_Of+'-'+Ano_Of,' ','') AS Orden, " +
                                               " convert(varchar(MAX),fup)  +'/'+vers as FUPVer,fup,Anulado,Id_Ofa " +
                                               " ,ISNULL((SELECT actseg_id FROM fup_acta_seguimiento " +
                                                         " WHERE actseg_idofp = Id_Ofa),0) AS actseg_id " +
                          " FROM         Orden_Seg" +
                          " WHERE        REPLACE(Num_Of+'-'+Ano_Of,' ','') ='" + orden + "'";

            return BdDatos.CargarTabla(sql);
        }

        public DataTable Validar_Comsumo_De_item(int idofap)
        {
            string sql = "SELECT  ISNULL(SUM(Req) ,0)   Piezas" +
                         " FROM SIF_ITEMS_ORDEN " +
                         " WHERE Id_Ofa = " + idofap + "";
            return BdDatos.CargarTabla(sql);
        }


        //-Observaciones de la orden
        public DataTable Obtener_Observaciones(int actsegId)
        {
            string sql = "SELECT asobs_Observacion, fecha_crea, usu_crea " +
                          " FROM fup_actaseg_Observaciones " +
                         " WHERE asobs_acta_seguimiento_id = " + actsegId + "";

            return BdDatos.CargarTabla(sql);
        }

        public DataTable Obtener_Datos_General_Orden(int fup)
        {
            string sql = "SELECT   pais.pai_nombre AS Pais, ciudad.ciu_nombre AS Ciudad, cliente.cli_nombre AS Cliente, obra.obr_nombre AS Obra " +
                         " FROM   formato_unico INNER JOIN cliente ON formato_unico.fup_cli_id = cliente.cli_id INNER JOIN " +
                                " obra ON formato_unico.fup_obr_id = obra.obr_id INNER JOIN " +
                                " pais ON cliente.cli_pai_id = pais.pai_id INNER JOIN " +
                                " ciudad ON cliente.cli_ciu_id = ciudad.ciu_id " +
                        " WHERE  (formato_unico.fup_id = "+fup+")";
            return BdDatos.CargarTabla(sql);
        }

        public DataTable Obtener_Listado_Ordenes(int fup,string orden)
        {
            string sql = "SELECT    ISNULL((SELECT  actseg_id FROM fup_acta_seguimiento WHERE actseg_idofp= Id_Ofa ),0) AS actseg_id , REPLACE(Orden_Seg.Num_Of + '-' + Orden_Seg.Ano_Of, ' ', '') AS Orden, " +
                                 " planta_forsa.planta_descripcion AS Planta, solicitud_facturacion.sf_m2 AS M2 " +
                         " FROM    Orden_Seg INNER JOIN solicitud_facturacion ON Orden_Seg.sf_id = solicitud_facturacion.Sf_id" +
                                 " INNER JOIN planta_forsa ON Orden_Seg.planta_id = planta_forsa.planta_id " +
                        " WHERE    (Orden_Seg.fup = "+fup+ ") AND REPLACE(Orden_Seg.Num_Of + '-' + Orden_Seg.Ano_Of, ' ', '')<> '"+orden+"'  " +
                                 " AND(Orden_Seg.Anulado = 0)";

            return BdDatos.CargarTabla(sql);
        }

        public int Migrar_Obs_Entre_Ordenes( int idActsegOrdenMigra, int idActsegOrdenBuscar)
        {
            string sql = "UPDATE fup_actaseg_Observaciones  SET asobs_acta_seguimiento_id = " + idActsegOrdenMigra + " " +
                         " WHERE asobs_acta_seguimiento_id =" + idActsegOrdenBuscar + "";
            return BdDatos.ejecutarSql(sql);
        }

        public int Anular_Seguimiento_Orden(int tipoAnula,int idofa)
        {
            string sql="";

            if (tipoAnula == 1)//se desea anular todo el seguimiento sin mantener el FUP
            {
                sql = "UPDATE fup_acta_seguimiento SET actseg_anulado=1  WHERE actseg_idofp= " + idofa + "";
            }
            else if (tipoAnula == 2)//se desea anular  el seguimiento  manteniendo el FUP
            {
                sql = "UPDATE fup_acta_seguimiento SET actseg_orden='NULL',actseg_idofp=0  WHERE actseg_idofp= "+ idofa+"";
            }                
            return BdDatos.ejecutarSql(sql);
        }

        public int Anular_Orden(int idofa)
        {
            string sql = "UPDATE ORDEN SET Anulada=1 WHERE Id_Ofa=" + idofa + "";
            return BdDatos.ejecutarSql(sql);
        }

        public int Anular_Orden_Seg(int idofa)
        {
            string sql = "UPDATE Orden_Seg SET Anulado=1,sf_id=17 WHERE Id_Ofa=" + idofa + "";
            return BdDatos.ejecutarSql(sql);
        }
    }
}
