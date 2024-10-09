using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CapaDatos;

namespace CapaControl
{
    public class ControlMobileGeneral
    {
        //CONSULTAR IDIOMA GENERAL
        public DataSet ConsultarIdiomaMobile()
        {
            string sql;
            sql = "SELECT     idioma_mobile.* FROM idioma_mobile";
            DataSet ds_idiomaMobile = BdDatos.consultarConDataset(sql);
            return ds_idiomaMobile;
        }

        //CONSULTAR EL ESTRATO SOCIOECONOMICO 
        public SqlDataReader poblarEstadoSocioEconomico()
        {
            string sql;

            sql = "SELECT ese_id, ese_descripcion, ese_activo, ese_orden FROM estado_socioeconomico " +
            "WHERE (ese_activo = 1) ORDER BY ese_orden";

            return BdDatos.ConsultarConDataReader(sql);
        }

        //CERRAR LA CONEXION
        public int cerrarConexion()
        {
            return BdDatos.desconectar();
        }
    }
}
