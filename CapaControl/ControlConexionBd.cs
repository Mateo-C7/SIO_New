using System;
using System.Data.SqlClient;
using CapaDatos;
using System.Data;
using System.Collections.Generic;
using CapaControl.Entity;
using System.Linq;

namespace CapaControl
{
    public class ControlConexionBd
    {
        public SqlDataReader consultarConexion(string bd)
        {
            string sql = "";

            if ( bd == "forsa")
            {
                sql = " SELECT        TOP (1) CASE WHEN fup_id > 0 THEN 'Conexion Exitosa' ELSE 'Error Conexion ' END AS Expr1, "+
                    " CAST(fup_id AS varchar(20)) AS Expr2, CONVERT(varchar,GETDATE(),113) as Fecha FROM formato_unico ORDER BY fup_id DESC";
            }

            if (bd == "unoee")
            {
                sql = "SELECT TOP (1) CASE WHEN f120_id > 0 THEN 'Conexion Exitosa' ELSE 'Error Conexion ' END AS EstadoConexion, " +
                        " CAST(f120_id AS varchar(20)) AS Dato , CONVERT(varchar,GETDATE(),113) as Fecha FROM t120_mc_items ORDER BY f120_id DESC";
            }

            if (bd == "gerbo")
            {
                sql = "SELECT  TOP (1) CASE WHEN COD_PRODUTO > 0 THEN 'Conexion Exitosa' ELSE 'Error Conexion ' END AS Conexion,   " +
                       " CAST(COD_PRODUTO AS varchar(20)) AS Expr1, CONVERT(varchar,GETDATE(),113) as Fecha  FROM TPRODUTO ORDER BY DAT_CADASTRO DESC";
            }

            return BdDatos.ConsultarConexionBases(sql, bd);
        }             

        public int cerrarConexion()
        {
            return BdDatos.desconectar();
        }

    }
}
