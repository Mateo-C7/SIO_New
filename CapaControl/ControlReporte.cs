using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using CapaDatos;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapaControl
{
  public  class ControlReporte
    {
        /****************************************
         * Carga la lista de reportes
        ****************************************/
        public void InitListaReportes(DropDownList myLista, string grupo)
        {   //Recuperar lista de plantas
            DataTable dTable = new DataTable();
            string sql = " SELECT Repo_Titulo, Repo_NomRepo " +
                         " FROM REPORTES " +
                         " WHERE Repo_Activo = 1 " +
                         " AND Rut_Id IN (" + grupo + ")" +
                         " ORDER BY Repo_Titulo ";
            dTable =BdDatos.CargarTabla(sql);
            foreach (DataRow row in dTable.Rows)
            {
                ListItem lst = new ListItem(row["Repo_Titulo"].ToString(),
                                            row["Repo_NomRepo"].ToString());
                myLista.Items.Insert(myLista.Items.Count, lst);
            }
        }

        public DataTable Obtener_Nombre_Grupo(int grupo)
        {
            string sql = "SELECT rut_nom FROM poli_rutinas  WHERE rut_id=" + grupo.ToString() + "";

            return BdDatos.CargarTabla(sql);
        }
    }
}
