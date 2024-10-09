using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CapaDatos;

namespace CapaControl
{
    public class ControlFlete
    {
        public SqlDataReader ConsultarMinimos()
        {
            string sql;

            sql = "SELECT fle_id,fle_pv_flete_minimo,fle_pv_seguro_minimo,fle_pv_seguro_calc,fle_pv_porc_tolerancia FROM Fletes";

            return BdDatos.ConsultarConDataReader(sql);
        }

        public int ActualizarFleteMin(int idflete, string fletemin, string seguromin)
        {
            string sql;
            decimal flete, seguro;
            flete = Convert.ToDecimal(fletemin);
            seguro = Convert.ToDecimal(seguromin);

            sql = "UPDATE Fletes SET fle_pv_flete_minimo =" + flete + ", fle_pv_seguro_minimo =" + seguro +
                    " WHERE   fle_id=" + idflete;

            return BdDatos.Actualizar(sql);
        }

        //actualizo el valor a la ciudad seleccionada
        public int ActualizarValorCiudad(int idCiudad, string valorCiudad,string usuario)
        {
            string sql;
            decimal vlrciudad;
            vlrciudad = Convert.ToDecimal(valorCiudad);

            sql = "update ciudad set ciu_flete =" + vlrciudad + ", ciu_usu_actualiza = '"+ usuario +"' where ciu_id = " + idCiudad;

            return BdDatos.Actualizar(sql);
        }

        //actualizo el valor del porcentaje de tolerancia
        public int ActualizarTolerancia(decimal tolerancia)
        {
            string sql;

            sql = "update Fletes set fle_pv_porc_tolerancia = " + tolerancia;

            return BdDatos.Actualizar(sql);
        }

        //consulto el valor de flete de la ciudad
        public SqlDataReader ConsultarFleteCiudad(int ciudad)
        {
            string sql;

            sql = "SELECT     ciu_id, ciu_nombre, ciu_flete FROM   ciudad  WHERE     (ciu_id = "+ciudad+")";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //actualizo el valor a la ciudad seleccionada
        public int InsertarLog(string motivo , decimal valor,string usuario)
        {
            string sql;

            sql = "insert into log_fletes (log_flete_motivo,log_flete_valor,log_flete_usuario) "+
	               " values('"+motivo+"',"+valor+",'"+usuario+"')";

            return BdDatos.Actualizar(sql);
        }


    }
}
