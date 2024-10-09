using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Linq;
using System.Text;
using System.Data;
using CapaDatos;

namespace CapaControl
{
  public  class Cls_MonedaTrm
    {
        CapaDatos.BdDatos objconex = new BdDatos();

        #region LLenar combos
        public void ListarMonedas(DropDownList myListaMoneda)
        {
            DataTable dTable = new DataTable();
            string sql = "SELECT mon_id,mon_descripcion " +
                          " FROM moneda " +
                          " WHERE mon_activo=1";
            dTable = BdDatos.CargarTabla(sql);
            foreach (DataRow row in dTable.Rows)
            {

                ListItem lst = new ListItem(row["mon_descripcion"].ToString(),
                                            row["mon_id"].ToString());
                myListaMoneda.Items.Insert(myListaMoneda.Items.Count, lst);
            }

        }

        public void ListarAños(DropDownList myListaAños)
        {
            DataTable dTable = new DataTable();
            string sql = "SELECT  anio AS anio_id, anio AS anio_descripcion " +
                          " FROM  anio WHERE activo=1";
            dTable = BdDatos.CargarTabla(sql);
            foreach (DataRow row in dTable.Rows)
            {
                ListItem lst = new ListItem(row["anio_descripcion"].ToString(),
                                            row["anio_id"].ToString());
                myListaAños.Items.Insert(myListaAños.Items.Count, lst);
            }
        }

        public void ListarMes(DropDownList ListarMes)
        {
            DataTable dTable = new DataTable();
            string sql = "SELECT mes_id, mes_periodo " +
                          " FROM mes";
            dTable = BdDatos.CargarTabla(sql);
            foreach (DataRow row in dTable.Rows)
            {
                ListItem lst = new ListItem(row["mes_periodo"].ToString(),
                                            row["mes_id"].ToString());
                ListarMes.Items.Insert(ListarMes.Items.Count, lst);
            }
        }

        public DataSet ListarMonedaDgv()
        {
            string sql = "SELECT mon_id,mon_descripcion " +
                          " FROM moneda " +
                          " WHERE mon_activo=1";
            return BdDatos.consultarConDataset(sql);
        }

        public DataSet ListarAñoDgv()
        {
            string sql = "SELECT  anio AS anio_id, anio AS anio_descripcion " +
                          " FROM  anio WHERE activo=1";
            return BdDatos.consultarConDataset(sql);
        }

        public DataSet ListarMesDgv()
        {
            string sql = "SELECT mes_id, mes_periodo " +
                          " FROM mes";
            return BdDatos.consultarConDataset(sql);
        }
        #endregion

        #region Metodos insert,update,delete

        public int Guardar_Registro(int monedaId, decimal trm, int año, int mes,  string usuario)
        {
            string sql = "INSERT INTO moneda_trm(moneda_id, mon_trm_valor, mon_trm_anio, mon_trm_mes, " +
                                               " mon_trm_periodo, mon_trm_fecha_registro, mon_trm_usuario) " +
                                       " VALUES (" + monedaId + "," + trm + "," + año + "," + mes + ", " +
                                       " CAST("+ año+" AS char(4)) + CASE WHEN len("+mes+ ") < 2 THEN '0' + CAST(" + mes + " AS char(1)) ELSE CAST(" + mes + " AS char(2)) END " +
                                       " ,GETDATE(),'" + usuario + "')";
            return BdDatos.ejecutarSql(sql);
        }

        public int Actualizar_Registro(int monedaId, decimal trm, int año, int mes, string usuario, int mon_trm_id)
        {
            string sql = "UPDATE moneda_trm SET moneda_id= " + monedaId + ",mon_trm_valor=" + trm + ", " +
                                                                  " mon_trm_anio=" + año + " , mon_trm_mes=" + mes + ", " +
                                                                  " mon_trm_periodo=CAST("+ año+" AS char(4)) + CASE WHEN len("+mes+ ") < 2 THEN '0' + CAST(" + mes + " AS char(1)) ELSE CAST(" + mes + " AS char(2)) END, " +
                                                                  " mon_trm_usuario='"+usuario+"' " +                                                               
                                                                  " WHERE mon_trm_id=" + mon_trm_id + "";
            return BdDatos.ejecutarSql(sql);
        }
        //Insertar el log 
        //public int Guardar_Log_MonedaTrm(string tabla, int meprid, string usuario, string fecha, string evento)
        //{
        //    string sqlinsertlogdel = "INSERT INTO Log_transacciones (genlog_tabla, genlog_id_registro, genlog_campo, genlog_usuario, genlog_fecha, genlog_valor_ant, genlog_valor_act, genlog_Evento)  
        //                                                      VALUES(genlog_tabla,genlog_id_registro,genlog_usuario,genlog_fecha,genlog_Evento) values('" + tabla + "'," + meprid.ToString() + ",'" + usuario + "','" + fecha + "','" + evento + "')";

        //    return BdDatos.ejecutarSql(sqlinsertlogdel);
        //}
        #endregion

        #region Consultas

        public DataSet Consultar_Detalle_MonedasTrm()
        {
            string sql = "SELECT mon_trm_id,mon_id AS moneda, mon_descripcion, mon_trm_anio AS año, mon_trm_mes AS mes, mon_trm_valor  AS Trm, " +
                               " mon_trm_periodo AS periodo,mon_trm_fecha_registro AS fecha_registro, mon_trm_usuario AS usuario " +
                          " FROM moneda_trm  INNER JOIN moneda on moneda.mon_id=moneda_trm.moneda_id " +
                      " ORDER BY mon_trm_id DESC";
            return BdDatos.consultarConDataset(sql);
        }

        public DataTable Consultar_Detalle_MonedasTrmDt()
        {
            string sql = "SELECT mon_trm_id,mon_id AS moneda, mon_descripcion, mon_trm_anio AS año, mon_trm_mes AS mes, mon_trm_valor  AS Trm, " +
                               " mon_trm_periodo AS periodo,mon_trm_fecha_registro AS fecha_registro, mon_trm_usuario AS usuario " +
                          " FROM moneda_trm  INNER JOIN moneda on moneda.mon_id=moneda_trm.moneda_id " +
                      " ORDER BY mon_trm_id DESC";
            return BdDatos.CargarTabla2(sql);
        }

        public DataTable Consultar_Detalle_Moneda(int mon_trm_id)
        {
            string sql = "SELECT mon_trm_id,mon_id AS moneda, mon_descripcion, mon_trm_anio AS año, mon_trm_mes AS mes, mon_trm_valor  AS Trm, " +
                               " mon_trm_periodo AS periodo,mon_trm_fecha_registro AS fecha_registro, mon_trm_usuario AS usuario " +
                          " FROM moneda_trm  INNER JOIN moneda on moneda.mon_id=moneda_trm.moneda_id " +
                          " WHERE mon_trm_id= "+ mon_trm_id+" " +
                      " ORDER BY mon_trm_id DESC";
            return BdDatos.CargarTabla2(sql);
        }

        public DataTable Validar_Periodo(int moneda,int año, int mes)
        {
            string sql = "SELECT mon_trm_periodo " +
                          " FROM moneda_trm " +
                         " WHERE mon_trm_periodo = CAST(" + año + " AS char(4)) + CASE WHEN " +
                         " len(" + mes + ") < 2 THEN '0' + CAST(" + mes + " AS char(1)) ELSE CAST " +
                         " (" + mes + " AS char(2)) END AND moneda_id=" + moneda +"  ";
            return BdDatos.CargarTabla2(sql);
        }      
        #endregion
    }
}
