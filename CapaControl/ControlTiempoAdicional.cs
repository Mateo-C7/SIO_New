using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using CapaDatos;
using System.Web.UI.WebControls;

namespace CapaControl
{   
    public class ControlTiempoAdicional
    {
        CapaDatos.BdDatos objconex = new BdDatos();

        public void Listar_TipoPieza(DropDownList myListaTipoPieza)
        {
            DataTable dTable = new DataTable();
            string sql = "SELECT TpoPza_id, TpoPza_dscrpcion FROM Tipo_Pieza WHERE TpoPza_id <> 0";
            dTable = BdDatos.CargarTabla(sql);
            foreach (DataRow row in dTable.Rows)
            {
                ListItem lst = new ListItem(row["TpoPza_dscrpcion"].ToString(),
                                            row["TpoPza_id"].ToString());
                myListaTipoPieza.Items.Insert(myListaTipoPieza.Items.Count, lst);
            }
        }

    public DataTable ObtenerPlantaUsuario(int idusuario)
        {
            string sql = "SELECT  usuario.usu_planta_Id, planta_forsa.planta_descripcion " +
                         " FROM usuario INNER JOIN planta_forsa ON usuario.usu_planta_Id = planta_forsa.planta_id " +
                        " WHERE (usuario.usu_siif_id = " + idusuario + ")";
            return BdDatos.CargarTabla(sql);
        }

        public int GuardarTiempoAdicional(string tmde_Id, string descripcion, decimal tmedMetal, decimal tmedArm,
                                         decimal tmedSol, int tpopieza, int planta, decimal tmedPter)
        {
            string sql = "INSERT INTO Oee_Tiempo_DescAuxi (Tmde_Id, Tmde_Dscrpcion, Tmde_Metal, Tmde_Arma, Tmde_Solda, TpoPza_Id, Planta_Id,Tmde_Pter) " +
                                                 " VALUES('" + tmde_Id + "','" + descripcion + "'," + tmedMetal + "," + tmedArm + ", " +
                                                 " " + tmedSol + "," + tpopieza + "," + planta + "," + tmedPter + ")";
            return BdDatos.ejecutarSql(sql);
        }

        public int Actualizar_Detalle_Tiempos(string tmde_Id, string descripcion, decimal tmedMetal,
                                         decimal tmedSol, int tpopieza, decimal tmedPter,string idcodigo)
        {
            string sql = "UPDATE Oee_Tiempo_DescAuxi SET  Tmde_Id='" + tmde_Id + "',Tmde_Dscrpcion='" + descripcion + "',Tmde_Metal=" + tmedMetal + ", " +
                                                        " Tmde_Solda=" + tmedSol + ",TpoPza_Id=" + tpopieza + ",Tmde_Pter=" + tmedPter + " " +
                                                        " WHERE Tmde_Id= '" + idcodigo + "'";
            return BdDatos.ejecutarSql(sql);
        }


        public DataTable ValidarCodigo(string codigo)
        {
            string sql = "SELECT Tmde_Id from  Oee_Tiempo_DescAuxi WHERE Tmde_Id='" + codigo + "'";
            return BdDatos.CargarTabla(sql);
        }


        public DataTable Consultar_Detalle_Tiempos(int planta)
        {
            string sql;
            sql = "SELECT Tmde_Id, Tmde_Dscrpcion, Tmde_Metal, Tmde_Solda, Tmde_Pter,TpoPza_Id " +
                   " FROM Oee_Tiempo_DescAuxi " +
                   " WHERE Planta_Id="+planta+"";
            return BdDatos.CargarTabla(sql);
        }
        public DataTable Consultar_Detalle_Tiempos_X_Codigo(int planta, string codigo)
        {
            string sql;
            sql = "SELECT Tmde_Id, Tmde_Dscrpcion, Tmde_Metal, Tmde_Solda, Tmde_Pter,TpoPza_Id " +
                   " FROM Oee_Tiempo_DescAuxi " +
                   " WHERE Planta_Id=" + planta + " AND Tmde_Id='"+codigo+"'";
            return BdDatos.CargarTabla(sql);
        }


        public int Eliminar_Detalle_Tiempos(string Tmde_Id)
        {
            string sql = "Delete FROM Oee_Tiempo_DescAuxi WHERE Tmde_Id= '" + Tmde_Id + "'";
            return BdDatos.ejecutarSql(sql);
        }

   
    }
}
