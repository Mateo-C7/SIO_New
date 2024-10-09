using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CapaDatos;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
namespace CapaControl
{
   public class ControlItinerarios
    {

        public void Listar_DatosPais(DropDownList myListaPais)
        {
            DataTable dTable = new DataTable();
            string sql = "SELECT pai_id, pai_nombre " +
                  "FROM pais " +
                  "ORDER BY pai_nombre";
            dTable = BdDatos.CargarTabla(sql);
            foreach (DataRow row in dTable.Rows)
            {
                ListItem lst = new ListItem(row["pai_nombre"].ToString(),
                                            row["pai_id"].ToString());
                myListaPais.Items.Insert(myListaPais.Items.Count, lst);
            }
        }
   
        public SqlDataReader consultarNombre(string nombre)
        {
            string sql;
            sql = "SELECT empleado.emp_nombre + ' ' + empleado.emp_apellidos AS Nombre, emp_correo_electronico, emp_area_id, emp_usu_num_id,usuario.usu_rap_id " +
                "FROM empleado INNER JOIN usuario ON empleado.emp_usu_num_id = usuario.usu_emp_usu_num_id " +
                "WHERE (usuario.usu_login = '" + nombre + "')";
            return BdDatos.ConsultarConDataReader(sql);
        }

        public void Listar_Puertos(DropDownList myListaPuertos)
        {
            DataTable dTable = new DataTable();
            string sql = "SELECT pto_id, pto_nombre FROM puertos ORDER BY pto_nombre";
            dTable = BdDatos.CargarTabla(sql);
            foreach (DataRow row in dTable.Rows)
            {
                ListItem lst = new ListItem(row["pto_nombre"].ToString(),
                                            row["pto_id"].ToString());
                myListaPuertos.Items.Insert(myListaPuertos.Items.Count, lst);
            }
        }

        public void Listar_Naviera(DropDownList myListaNaviera)
        {
            DataTable dTable = new DataTable();
            string sql = "SELECT nav_id, nav_nombre FROM naviera ORDER BY nav_nombre";
            dTable = BdDatos.CargarTabla(sql);
            foreach (DataRow row in dTable.Rows)
            {
                ListItem lst = new ListItem(row["nav_nombre"].ToString(),
                                            row["nav_id"].ToString());
                myListaNaviera.Items.Insert(myListaNaviera.Items.Count, lst);
            }
        }

            public int IngresarItinerario(int pais, int zarpe, int descargue, int tiempo, int naviera, string st20, string hc40, string buque,
            string FecCargue, string CierreNav, string EstZarpe, string Arribo, string nombre)
        {
            string sql;
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");

            sql = "INSERT INTO itinerarios (itn_pais, itn_puerto_zarpe, itn_puerto_descargue, itn_tiempo_transito, itn_naviera, " +
                 "itn_20ST, itn_40HC, itn_buque, itn_fecha_cargue, itn_cierre_naviera, itn_fecha_zarpe, itn_fecha_arribo, " +
                 "usu_crea, usu_actualiza, fecha_crea, fecha_actualiza)" +
                  "VALUES (" + pais + "," + zarpe + "," + descargue + "," + tiempo + "," + naviera + ",'" + st20 + "','" + hc40 +
                  "','" + buque + "','" + FecCargue + "','" + CierreNav + "','" + EstZarpe + "','" + Arribo + "','" + nombre +
                  "','" + nombre + "','" + fecha + "','" + fecha + "')";

            return BdDatos.ejecutarSql(sql);
        }

        public int ActualizarItinerario(int cod, int pais, int zarpe, int descargue, int tiempo, int naviera, string st20, string hc40, string buque,
           string FecCargue, string CierreNav, string EstZarpe, string Arribo, string nombre)
        {
            string sql;
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");

            sql = "UPDATE itinerarios SET itn_pais = " + pais + ", itn_puerto_zarpe = " + zarpe + ", itn_puerto_descargue = " + descargue +
                ", itn_tiempo_transito = " + tiempo + ", itn_naviera = " + naviera + ", itn_20ST = '" + st20 + "', itn_40HC = '" + hc40 +
                "', itn_buque = '" + buque + "', itn_fecha_cargue = '" + FecCargue + "', itn_cierre_naviera = '" + CierreNav +
                "', itn_fecha_zarpe = '" + EstZarpe + "', itn_fecha_arribo = '" + Arribo + "', usu_actualiza = '" + nombre +
                "', fecha_actualiza = '" + fecha + "' " +
                  "WHERE itn_id = " + cod;

            return BdDatos.ejecutarSql(sql);
        }
        public int EliminarItinerario(int cod)
        {
            string sql;

            sql = "UPDATE itinerarios SET itn_activo = 0 WHERE itn_id = " + cod;

            return BdDatos.ejecutarSql(sql);
        }

        public SqlDataReader ConsultarItinerario(int codigo)
        {
            string sql;

            sql = "SELECT itinerarios.itn_id, itinerarios.itn_pais, itinerarios.itn_puerto_zarpe, itinerarios.itn_puerto_descargue, " +
                "itinerarios.itn_tiempo_transito, itinerarios.itn_naviera, itinerarios.itn_20ST, itinerarios.itn_40HC, itinerarios.itn_buque, " +
                "itinerarios.itn_fecha_cargue, itinerarios.itn_cierre_naviera, itinerarios.itn_fecha_zarpe, itinerarios.itn_fecha_arribo, " +
                "puertos.pto_nombre, puertos_1.pto_nombre AS Descargue, pais.pai_nombre, naviera.nav_nombre " +
                "FROM itinerarios INNER JOIN puertos ON itinerarios.itn_puerto_zarpe = puertos.pto_id INNER JOIN puertos AS puertos_1 ON " +
                "itinerarios.itn_puerto_descargue = puertos_1.pto_id INNER JOIN pais ON itinerarios.itn_pais = pais.pai_id INNER JOIN " +
                "naviera ON itinerarios.itn_naviera = naviera.nav_id " +
                "WHERE (itn_id = " + codigo + ") AND itn_activo = 1";

            return BdDatos.ConsultarConDataReader(sql);
        }
        public int IngresarPuerto(string puerto, string nombre)
        {
            string sql;
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");

            sql = "INSERT INTO puertos (pto_nombre, usu_crea, usu_actualiza, fecha_crea, fecha_actualiza)" +
                  "VALUES ('" + puerto + "','" + nombre + "','" + nombre + "','" + fecha + "','" + fecha + "')";

            return BdDatos.ejecutarSql(sql);
        }
        public int IngresarNaviera(string naviera, string nombre)
        {
            string sql;
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");

            sql = "INSERT INTO naviera (nav_nombre, usu_crea, usu_actualiza, fecha_crea, fecha_actualiza)" +
                  "VALUES ('" + naviera + "','" + nombre + "','" + nombre + "','" + fecha + "','" + fecha + "')";

            return BdDatos.ejecutarSql(sql);
        }
        public int actualizarPuerto(int pto, string puerto, string nombre)
        {
            string sql;
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");

            sql = "UPDATE puertos SET pto_nombre = '" + puerto + "', usu_actualiza = '" + nombre + "', fecha_actualiza = '" + fecha + "' " +
                  "WHERE pto_id = " + pto;

            return BdDatos.ejecutarSql(sql);
        }
        public int actualizarNaviera(int nav, string naviera, string nombre)
        {
            string sql;
            string fecha = System.DateTime.Now.ToString("dd/MM/yyyy");

            sql = "UPDATE naviera SET nav_nombre = '" + naviera + "', usu_actualiza = '" + nombre + "', fecha_actualiza = '" + fecha + "' " +
                  "WHERE nav_id = " + nav;

            return BdDatos.ejecutarSql(sql);
        }
    }
}
