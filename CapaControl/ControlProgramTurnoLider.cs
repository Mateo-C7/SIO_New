using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CapaDatos;
using System.Web.UI.WebControls;

namespace CapaControl
{
  public  class ControlProgramTurnoLider
    {
        CapaDatos.BdDatos objconex = new BdDatos();


        public void Listar_Procesos(DropDownList myListaProceso)
        {
            DataTable dTable = new DataTable();
            string sql = "SELECT Id_Proceso,Proceso FROM PROCESO WHERE Proceso_Activo = 1 AND Id_Proceso IN(1, 3, 4)";
            dTable = BdDatos.CargarTabla(sql);
            foreach (DataRow row in dTable.Rows)
            {
                ListItem lst = new ListItem(row["Proceso"].ToString(),
                                            row["Id_Proceso"].ToString());
                myListaProceso.Items.Insert(myListaProceso.Items.Count, lst);
            }
        }

        public void Listar_Plantas(DropDownList myListaPlanta)
        {
            DataTable dTable = new DataTable();
            string sql = "SELECT Plan_Prod_Id,Plan_Prod_Nombre " +
                          " FROM PLANTAS_PRODUCCION " +
                         " WHERE PlantaId = 1 AND PlantaId <> 0 AND Plan_Prod_Id <> 0";
            dTable = BdDatos.CargarTabla(sql);
            foreach (DataRow row in dTable.Rows)
            {
                ListItem lst = new ListItem(row["Plan_Prod_Nombre"].ToString(),
                                            row["Plan_Prod_Id"].ToString());
                myListaPlanta.Items.Insert(myListaPlanta.Items.Count, lst);
            }
        }

        public DataTable Obtener_InfoUsuario(int idoper)
        {
            string sql = "SELECT Empleado,IdPlanProd,Plantas_Produccion.Plan_Prod_Nombre " +
                          " FROM VW_SM_USUARIOS  INNER JOIN Plantas_Produccion " +
                               " ON vw_sm_usuarios.IdPlanProd = Plantas_Produccion.Plan_Prod_Id " +
                        " WHERE Emp_Usu_Num_Id = "+idoper+" AND Usu_Activo = 1";
            return BdDatos.CargarTabla(sql);
        }

        public int GuardarProgramacion(int idOper,string turno,int idProceso,string fechaIni,string fechaFin,int idPlanProd)
        {
            string sql = "INSERT INTO  Turno_Empleado (TurnEmp_Fecha, TurnEmp_IdOper, TurnEmp_Turno, " +
                                    " TurnEmp_IdProceso, TurnEmp_FechIni, TurnEmp_FechFin, TurnEmp_IdPlantaProd) " +
                            " VALUES  (CONVERT(DATE, GETDATE(), 103)," + idOper + ",'" + turno + "'," + idProceso + ", " +
                                     " CONVERT(DATE, '" + fechaIni + " ', 103),CONVERT(DATE, '" + fechaFin + " ', 103)," + idPlanProd + ")";
            return BdDatos.ejecutarSql(sql);
        }

        public int EliminarProgramacion(int TurnEmpId)
        {
            string sql = "Delete FROM Turno_Empleado  WHERE TurnEmp_Id = " + TurnEmpId + "";
            return BdDatos.ejecutarSql(sql);
        }

        public int ActualizarProgramacion(int idOper, string turno, int idProceso, string fechaIni, string fechaFin, int TurnEmpId, int planta)
        {
            string sql = "UPDATE Turno_Empleado SET TurnEmp_IdOper=" + idOper + ",TurnEmp_Turno='" + turno + "',TurnEmp_IdProceso=" + idProceso + ", " +
                                                  " TurnEmp_FechIni=CONVERT(DATE, '" + fechaIni + " ', 103),TurnEmp_FechFin=CONVERT(DATE, '" + fechaFin + " ', 103) , " +
                                                  "TurnEmp_IdPlantaProd="+planta+" " +
                                            " WHERE TurnEmp_Id = " + TurnEmpId + "";
            return BdDatos.ejecutarSql(sql);
        }



        public DataTable Consultar_Detalle_Programacion(int planta)
        {
            string sql;


            sql = "SELECT  Turno_Empleado.TurnEmp_Id,Turno_Empleado.TurnEmp_Fecha, Turno_Empleado.TurnEmp_IdOper, " +
                                         " empleado.emp_apellidos_mayusculas + ' ' + empleado.emp_nombre_mayusculas AS Lider, " +
                                         " CASE WHEN Turno_Empleado.TurnEmp_Turno ='T1' THEN 'TURNO 1'  ELSE " +
                                         " CASE WHEN Turno_Empleado.TurnEmp_Turno ='T2' THEN 'TURNO 2' ELSE " +
                                         " CASE WHEN Turno_Empleado.TurnEmp_Turno ='T3' THEN 'TURNO 3' END END END AS TurnEmp_Turno, " +
                                         " Turno_Empleado.TurnEmp_IdProceso, Proceso.Proceso," +
                                         " Turno_Empleado.TurnEmp_FechIni, Turno_Empleado.TurnEmp_FechFin, " +
                                         " Turno_Empleado.TurnEmp_IdPlantaProd, Plantas_Produccion.Plan_Prod_Nombre AS Planta " +
                                   " FROM  Turno_Empleado INNER JOIN empleado ON Turno_Empleado.TurnEmp_IdOper = empleado.emp_usu_num_id INNER JOIN " +
                                         " Proceso ON Turno_Empleado.TurnEmp_IdProceso = Proceso.Id_Proceso INNER JOIN " +
                                         " Plantas_Produccion ON Turno_Empleado.TurnEmp_IdPlantaProd = Plantas_Produccion.Plan_Prod_Id " +
                                "   WHERE (Turno_Empleado.TurnEmp_FechIni > DATEADD(DD, - 30, GETDATE())) AND TurnEmp_IdPlantaProd = "+planta+" " +
                                "  ORDER BY TurnEmp_Fecha DESC";                                                 
            return BdDatos.CargarTabla(sql);
        }

        public DataTable Obtener_Detalle_Programacion_X_Id(int TurnEmp_Id)
        {      
            string sql= "SELECT  Turno_Empleado.TurnEmp_Id,Turno_Empleado.TurnEmp_Fecha, Turno_Empleado.TurnEmp_IdOper, " +
                                         " empleado.emp_apellidos_mayusculas + ' ' + empleado.emp_nombre_mayusculas AS Lider, " +
                                         " Turno_Empleado.TurnEmp_Turno, Turno_Empleado.TurnEmp_IdProceso, Proceso.Proceso," +
                                         " CONVERT(DATE,Turno_Empleado.TurnEmp_FechIni,103) AS TurnEmp_FechIni,  " +
                                         " CONVERT(DATE, Turno_Empleado.TurnEmp_FechFin,103) AS TurnEmp_FechFin, " +
                                         " Turno_Empleado.TurnEmp_IdPlantaProd, Plantas_Produccion.Plan_Prod_Nombre AS Planta " +
                                   " FROM  Turno_Empleado INNER JOIN empleado ON Turno_Empleado.TurnEmp_IdOper = empleado.emp_usu_num_id INNER JOIN " +
                                         " Proceso ON Turno_Empleado.TurnEmp_IdProceso = Proceso.Id_Proceso INNER JOIN " +
                                         " Plantas_Produccion ON Turno_Empleado.TurnEmp_IdPlantaProd = Plantas_Produccion.Plan_Prod_Id " +
                                         " WHERE TurnEmp_Id="+TurnEmp_Id+"";
            return BdDatos.CargarTabla(sql);
        }

        //Consulta si ya existe una programación para el lider en la fecha dada
        public Boolean Existe_Programa_Lider(int turnempid, String fechaIni, String fechaFin)
        {
            Boolean existe = true;
            DataTable consulta = null;
            String sql = " SELECT  TurnEmp_Id FROM Turno_Empleado WHERE TurnEmp_IdOper="+turnempid+" " +
                                                 " AND (CONVERT(DATETIME, '" + fechaIni + "', 103) <= TurnEmp_FechFin) " +
                                                 " AND (CONVERT(DATETIME, '" + fechaFin + "', 103) >= TurnEmp_FechIni)";
            consulta = BdDatos.CargarTabla(sql);
            if (consulta.Rows.Count > 0) { existe = true; } else { existe = false; }
            return existe;
        }
    }
}
