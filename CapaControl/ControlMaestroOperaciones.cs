using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CapaDatos;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace CapaControl
{
   public class ControlMaestroOperaciones
    {
        CapaDatos.BdDatos objconex = new BdDatos();

        public int CerrarConexion()
        {
            return BdDatos.desconectar();
        }

        public DataTable Consultar_Detalle_Operaciones()
        {
            string sql;
            sql = "SELECT   Maestro_Operacion_Id, Descripcion, Unidad, Aplica_En, Cen_Cos_Id, HorasxUnidad, " +
                          " Costo_Prom_Alum_Erp, Costo_Prom_Bush, Costo_Prom_Rem " +
                   " FROM Maestro_Operaciones";
            return BdDatos.CargarTabla(sql);
        }

        public DataSet Consultar_Detalle_OperacionesDTS()
        {
            string sql;
            sql = "SELECT   Maestro_Operacion_Id, Descripcion, Unidad, Aplica_En, Cen_Cos_Id, HorasxUnidad, " +
                          " Costo_Prom_Alum_Erp, Costo_Prom_Bush, Costo_Prom_Rem " +
                   " FROM Maestro_Operaciones";
            return BdDatos.consultarConDataset(sql);
        }


        public DataTable Consultar_Detalle_Operaciones2(int idOperaciones)
        {
            string sql;
            sql = "SELECT   Maestro_Operacion_Id, Descripcion, Unidad, Aplica_En, Cen_Cos_Id, HorasxUnidad, " +
                          " Costo_Prom_Alum_Erp, Costo_Prom_Bush, Costo_Prom_Rem " +
                   " FROM Maestro_Operaciones " +
                  " WHERE Maestro_Operacion_Id="+idOperaciones+" ";
            return BdDatos.CargarTabla(sql);
        }


        public int Guardar_Detalle_Operaciones(int idOperacion,string descrip,string und,string aplica,
                                                  int centrCosto, decimal horaxunid,decimal costPromAlum,
                                                  decimal costPromBush,decimal costPromRem)
        {
            string sql = "INSERT INTO Maestro_Operaciones(Maestro_Operacion_Id,Descripcion, Unidad, Aplica_En, Cen_Cos_Id, HorasxUnidad, " +
                                                       " Costo_Prom_Alum_Erp, Costo_Prom_Bush, Costo_Prom_Rem) " +
                            " VALUES("+idOperacion+",'"+descrip+"','"+und+"','"+aplica+"',"+centrCosto+", " +
                                    " "+horaxunid+","+costPromAlum+","+costPromBush+","+costPromRem+")";
            return BdDatos.ejecutarSql(sql);
        }


        public int Actualizar_Detalle_Operaciones( string descrip, string und, string aplica,
                                                int centrCosto, decimal horaxunid, decimal costPromAlum,
                                                decimal costPromBush, decimal costPromRem, int idOperacion)
        {
            string sql = "UPDATE Maestro_Operaciones SET    Descripcion='"+descrip+"',Unidad='"+und+"', Aplica_En='"+aplica+"', " +
                                                          " Cen_Cos_Id ="+centrCosto+", HorasxUnidad="+horaxunid+", " +
                                                          " Costo_Prom_Alum_Erp="+costPromAlum+ ", Costo_Prom_Bush=" +costPromBush+ ", Costo_Prom_Rem=" +costPromRem+" " +
                                                 " WHERE     Maestro_Operacion_Id="+idOperacion+"";
            return BdDatos.ejecutarSql(sql);
        }


        public DataTable Obterner_ultimo_IdOperacion()
        {
            string sql = "SELECT top (1) Maestro_Operacion_Id " +
                         " FROM Maestro_Operaciones " +
                     " ORDER BY Maestro_Operacion_Id desc";

            return BdDatos.CargarTabla(sql);
        }


        public int Eliminar_Operacion(int idOperacion)
        {
            string sql = "DELETE FROM  Maestro_Operaciones where Maestro_Operacion_Id = " + idOperacion + "";
            return BdDatos.ejecutarSql(sql);
        }
    }
}
